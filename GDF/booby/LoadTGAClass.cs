using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace booby;

public class LoadTGAClass
{
	private struct tgaColorMap
	{
		public ushort FirstEntryIndex;

		public ushort Length;

		public byte EntrySize;

		public void Read(BinaryReader br)
		{
			FirstEntryIndex = br.ReadUInt16();
			Length = br.ReadUInt16();
			EntrySize = br.ReadByte();
		}
	}

	private struct tgaImageSpec
	{
		public ushort XOrigin;

		public ushort YOrigin;

		public ushort Width;

		public ushort Height;

		public byte PixelDepth;

		public byte Descriptor;

		public byte AlphaBits
		{
			get
			{
				return (byte)(Descriptor & 0xF);
			}
			set
			{
				Descriptor = (byte)((Descriptor & -16) | (value & 0xF));
			}
		}

		public bool BottomUp
		{
			get
			{
				return (Descriptor & 0x20) == 32;
			}
			set
			{
				Descriptor = (byte)((Descriptor & -33) | (value ? 32 : 0));
			}
		}

		public void Read(BinaryReader br)
		{
			XOrigin = br.ReadUInt16();
			YOrigin = br.ReadUInt16();
			Width = br.ReadUInt16();
			Height = br.ReadUInt16();
			PixelDepth = br.ReadByte();
			Descriptor = br.ReadByte();
		}
	}

	private struct tgaHeader
	{
		public byte IdLength;

		public byte ColorMapType;

		public byte ImageType;

		public tgaColorMap ColorMap;

		public tgaImageSpec ImageSpec;

		public bool RleEncoded => ImageType >= 9;

		public void Read(BinaryReader br)
		{
			br.BaseStream.Seek(0L, SeekOrigin.Begin);
			IdLength = br.ReadByte();
			ColorMapType = br.ReadByte();
			ImageType = br.ReadByte();
			ColorMap = default(tgaColorMap);
			ImageSpec = default(tgaImageSpec);
			ColorMap.Read(br);
			ImageSpec.Read(br);
		}
	}

	private struct tgaCD
	{
		public uint RMask;

		public uint GMask;

		public uint BMask;

		public uint AMask;

		public byte RShift;

		public byte GShift;

		public byte BShift;

		public byte AShift;

		public uint FinalOr;

		public bool NeedNoConvert;
	}

	private static uint UnpackColor(uint sourceColor, ref tgaCD cd)
	{
		uint num = (sourceColor << (int)cd.RShift) | (sourceColor >> 32 - cd.RShift);
		uint num2 = (sourceColor << (int)cd.GShift) | (sourceColor >> 32 - cd.GShift);
		uint num3 = (sourceColor << (int)cd.BShift) | (sourceColor >> 32 - cd.BShift);
		uint num4 = (sourceColor << (int)cd.AShift) | (sourceColor >> 32 - cd.AShift);
		return (num & cd.RMask) | (num2 & cd.GMask) | (num3 & cd.BMask) | (num4 & cd.AMask) | cd.FinalOr;
	}

	private unsafe static void decodeLine(BitmapData b, int line, int byp, byte[] data, ref tgaCD cd)
	{
		if (cd.NeedNoConvert)
		{
			uint* ptr = (uint*)((byte*)b.Scan0.ToPointer() + (nint)line * (nint)b.Stride);
			fixed (byte* ptr2 = data)
			{
				uint* ptr3 = (uint*)ptr2;
				for (int i = 0; i < b.Width; i++)
				{
					ptr[i] = ptr3[i];
				}
			}
			return;
		}
		byte* ptr4 = (byte*)b.Scan0.ToPointer() + (nint)line * (nint)b.Stride;
		uint* ptr5 = (uint*)ptr4;
		int num = 0;
		fixed (byte* ptr6 = data)
		{
			for (int j = 0; j < b.Width; j++)
			{
				uint num2 = 0u;
				for (int k = 0; k < byp; k++)
				{
					num2 |= (uint)(ptr6[num] << (k << 3));
					num++;
				}
				ptr5[j] = UnpackColor(num2, ref cd);
			}
		}
	}

	private static void decodeRle(BitmapData b, int byp, tgaCD cd, BinaryReader br, bool bottomUp)
	{
		try
		{
			int width = b.Width;
			byte[] array = new byte[(width + 128) * byp];
			int num = width * byp;
			int num2 = 0;
			for (int i = 0; i < b.Height; i++)
			{
				while (num2 < num)
				{
					byte b2 = br.ReadByte();
					int num3;
					int num4;
					if (b2 >= 128)
					{
						num3 = byp;
						num4 = byp * (b2 - 128);
					}
					else
					{
						num3 = byp * (b2 + 1);
						num4 = 0;
					}
					br.Read(array, num2, num3);
					num2 += num3;
					for (int j = 0; j != num4; j++)
					{
						array[num2 + j] = array[num2 + j - num3];
					}
					num2 += num4;
				}
				if (!bottomUp)
				{
					decodeLine(b, b.Height - i - 1, byp, array, ref cd);
				}
				else
				{
					decodeLine(b, i, byp, array, ref cd);
				}
				if (num2 > num)
				{
					Array.Copy(array, num, array, 0, num2 - num);
					num2 -= num;
				}
				else
				{
					num2 = 0;
				}
			}
		}
		catch (EndOfStreamException)
		{
		}
	}

	private static void decodePlain(BitmapData b, int byp, tgaCD cd, BinaryReader br, bool bottomUp)
	{
		int width = b.Width;
		byte[] array = new byte[width * byp];
		for (int i = 0; i < b.Height; i++)
		{
			br.Read(array, 0, width * byp);
			if (!bottomUp)
			{
				decodeLine(b, b.Height - i - 1, byp, array, ref cd);
			}
			else
			{
				decodeLine(b, i, byp, array, ref cd);
			}
		}
	}

	private static void decodeStandard8(BitmapData b, tgaHeader hdr, BinaryReader br)
	{
		tgaCD cd = new tgaCD
		{
			RMask = 255u,
			GMask = 65280u,
			BMask = 16711680u,
			AMask = 0u,
			RShift = 0,
			GShift = 8,
			BShift = 16,
			AShift = 0,
			FinalOr = 4278190080u
		};
		if (hdr.RleEncoded)
		{
			decodeRle(b, 1, cd, br, hdr.ImageSpec.BottomUp);
		}
		else
		{
			decodePlain(b, 1, cd, br, hdr.ImageSpec.BottomUp);
		}
	}

	private static void decodeSpecial16(BitmapData b, tgaHeader hdr, BinaryReader br)
	{
		tgaCD cd = new tgaCD
		{
			RMask = 15728640u,
			GMask = 61440u,
			BMask = 240u,
			AMask = 4026531840u,
			RShift = 12,
			GShift = 8,
			BShift = 4,
			AShift = 16,
			FinalOr = 0u
		};
		if (hdr.RleEncoded)
		{
			decodeRle(b, 2, cd, br, hdr.ImageSpec.BottomUp);
		}
		else
		{
			decodePlain(b, 2, cd, br, hdr.ImageSpec.BottomUp);
		}
	}

	private static void decodeStandard16(BitmapData b, tgaHeader hdr, BinaryReader br)
	{
		tgaCD cd = new tgaCD
		{
			RMask = 16252928u,
			GMask = 64512u,
			BMask = 248u,
			AMask = 0u,
			RShift = 8,
			GShift = 5,
			BShift = 3,
			AShift = 0,
			FinalOr = 4278190080u
		};
		if (hdr.RleEncoded)
		{
			decodeRle(b, 2, cd, br, hdr.ImageSpec.BottomUp);
		}
		else
		{
			decodePlain(b, 2, cd, br, hdr.ImageSpec.BottomUp);
		}
	}

	private static void decodeSpecial24(BitmapData b, tgaHeader hdr, BinaryReader br)
	{
		tgaCD cd = new tgaCD
		{
			RMask = 16252928u,
			GMask = 64512u,
			BMask = 248u,
			AMask = 4278190080u,
			RShift = 8,
			GShift = 5,
			BShift = 3,
			AShift = 8,
			FinalOr = 0u
		};
		if (hdr.RleEncoded)
		{
			decodeRle(b, 3, cd, br, hdr.ImageSpec.BottomUp);
		}
		else
		{
			decodePlain(b, 3, cd, br, hdr.ImageSpec.BottomUp);
		}
	}

	private static void decodeStandard24(BitmapData b, tgaHeader hdr, BinaryReader br)
	{
		tgaCD cd = new tgaCD
		{
			RMask = 16711680u,
			GMask = 65280u,
			BMask = 255u,
			AMask = 0u,
			RShift = 0,
			GShift = 0,
			BShift = 0,
			AShift = 0,
			FinalOr = 4278190080u
		};
		if (hdr.RleEncoded)
		{
			decodeRle(b, 3, cd, br, hdr.ImageSpec.BottomUp);
		}
		else
		{
			decodePlain(b, 3, cd, br, hdr.ImageSpec.BottomUp);
		}
	}

	private static void decodeStandard32(BitmapData b, tgaHeader hdr, BinaryReader br)
	{
		tgaCD cd = new tgaCD
		{
			RMask = 16711680u,
			GMask = 65280u,
			BMask = 255u,
			AMask = 4278190080u,
			RShift = 0,
			GShift = 0,
			BShift = 0,
			AShift = 0,
			FinalOr = 0u,
			NeedNoConvert = true
		};
		if (hdr.RleEncoded)
		{
			decodeRle(b, 4, cd, br, hdr.ImageSpec.BottomUp);
		}
		else
		{
			decodePlain(b, 4, cd, br, hdr.ImageSpec.BottomUp);
		}
	}

	public static Size GetTGASize(string filename)
	{
		FileStream input = File.OpenRead(filename);
		BinaryReader binaryReader = new BinaryReader(input);
		tgaHeader tgaHeader2 = default(tgaHeader);
		tgaHeader2.Read(binaryReader);
		binaryReader.Close();
		return new Size(tgaHeader2.ImageSpec.Width, tgaHeader2.ImageSpec.Height);
	}

	public static Bitmap LoadTGA(Stream source)
	{
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Expected O, but got Unknown
		byte[] array = new byte[source.Length];
		source.Read(array, 0, array.Length);
		MemoryStream input = new MemoryStream(array);
		BinaryReader binaryReader = new BinaryReader(input);
		tgaHeader hdr = default(tgaHeader);
		hdr.Read(binaryReader);
		if (hdr.ImageSpec.PixelDepth != 8 && hdr.ImageSpec.PixelDepth != 16 && hdr.ImageSpec.PixelDepth != 24 && hdr.ImageSpec.PixelDepth != 32)
		{
			throw new ArgumentException("Not a supported tga file. (Pixeldepth=" + hdr.ImageSpec.PixelDepth + " AlphaBits=" + hdr.ImageSpec.AlphaBits + ")");
		}
		if (hdr.ImageSpec.AlphaBits > 8)
		{
			throw new ArgumentException("Not a supported tga file.");
		}
		if (hdr.ImageSpec.Width > 4096 || hdr.ImageSpec.Height > 4096)
		{
			throw new ArgumentException("Image too large. (Width=" + hdr.ImageSpec.Width + " Height=" + hdr.ImageSpec.Height + ")");
		}
		Bitmap val = new Bitmap((int)hdr.ImageSpec.Width, (int)hdr.ImageSpec.Height, (PixelFormat)2498570);
		BitmapData val2 = val.LockBits(new Rectangle(0, 0, ((Image)val).Width, ((Image)val).Height), (ImageLockMode)2, (PixelFormat)925707);
		switch (hdr.ImageSpec.PixelDepth)
		{
		case 8:
			if (hdr.ImageSpec.AlphaBits > 0)
			{
				decodeStandard8(val2, hdr, binaryReader);
			}
			else
			{
				decodeStandard8(val2, hdr, binaryReader);
			}
			break;
		case 16:
			if (hdr.ImageSpec.AlphaBits > 0)
			{
				decodeSpecial16(val2, hdr, binaryReader);
			}
			else
			{
				decodeStandard16(val2, hdr, binaryReader);
			}
			break;
		case 24:
			if (hdr.ImageSpec.AlphaBits > 0)
			{
				decodeSpecial24(val2, hdr, binaryReader);
			}
			else
			{
				decodeStandard24(val2, hdr, binaryReader);
			}
			break;
		case 32:
			decodeStandard32(val2, hdr, binaryReader);
			break;
		default:
			val.UnlockBits(val2);
			((Image)val).Dispose();
			return null;
		}
		val.UnlockBits(val2);
		binaryReader.Close();
		return val;
	}

	public static Bitmap LoadTGA(string filename)
	{
		try
		{
			using FileStream source = File.OpenRead(filename);
			return LoadTGA(source);
		}
		catch (DirectoryNotFoundException)
		{
			return null;
		}
		catch (FileNotFoundException)
		{
			return null;
		}
	}
}
