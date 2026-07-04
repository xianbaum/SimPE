using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using V = Vortice.Direct3D9;

namespace Microsoft.DirectX.Direct3D
{
	public abstract class BaseTexture : IDisposable
	{
		internal abstract V.IDirect3DBaseTexture9 NativeBaseTexture { get; }
		public abstract bool Disposed { get; }
		public abstract void Dispose();
	}

	public sealed class Texture : BaseTexture
	{
		private readonly V.IDirect3DTexture9 tex;
		private bool disposed;

		internal Texture(V.IDirect3DTexture9 t) { tex = t; }

		internal override V.IDirect3DBaseTexture9 NativeBaseTexture => tex;
		public override bool Disposed => disposed;

		public override void Dispose()
		{
			if (!disposed)
			{
				tex?.Dispose();
				disposed = true;
			}
		}
	}

	public sealed class Surface : IDisposable
	{
		private readonly V.IDirect3DSurface9 surface;
		private bool disposed;

		internal Surface(V.IDirect3DSurface9 s) { surface = s; }

		internal V.IDirect3DSurface9 Native => surface;
		public bool Disposed => disposed;

		public void Dispose()
		{
			if (!disposed)
			{
				surface?.Dispose();
				disposed = true;
			}
		}
	}

	public sealed class VertexBufferDescription
	{
		public VertexFormats VertexFormat { get; }
		internal VertexBufferDescription(VertexFormats format) { VertexFormat = format; }
	}

	public sealed class VertexBuffer
	{
		public VertexBufferDescription Description { get; }
		internal VertexBuffer(VertexFormats format) { Description = new VertexBufferDescription(format); }
	}

	public static class TextureLoader
	{
		public static Texture FromStream(Device device, Stream stream)
		{
			using Bitmap bmp = new Bitmap(stream);
			int w = bmp.Width;
			int h = bmp.Height;
			V.IDirect3DTexture9 tex = device.Native.CreateTexture((uint)w, (uint)h, 1, (V.Usage)0, V.Format.A8R8G8B8, V.Pool.Managed);
			V.LockedRectangle lr = tex.LockRect(0, V.LockFlags.None);
			BitmapData data = bmp.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
			try
			{
				unsafe
				{
					byte* dst = (byte*)lr.DataPointer;
					byte* src = (byte*)data.Scan0;
					int rowBytes = w * 4;
					for (int y = 0; y < h; y++)
					{
						Buffer.MemoryCopy(src + (long)y * data.Stride, dst + (long)y * lr.Pitch, rowBytes, rowBytes);
					}
				}
			}
			finally
			{
				bmp.UnlockBits(data);
				tex.UnlockRect(0);
			}
			return new Texture(tex);
		}
	}

	public static class SurfaceLoader
	{
		// Stub.
		public static Stream SaveToStream(ImageFileFormat format, Surface surface)
		{
			MemoryStream ms = new MemoryStream();
			using (Bitmap bmp = new Bitmap(1, 1))
			{
				bmp.Save(ms, ToImageFormat(format));
			}
			ms.Position = 0L;
			return ms;
		}

		private static ImageFormat ToImageFormat(ImageFileFormat format)
		{
			switch (format)
			{
			case ImageFileFormat.Bmp:
			case ImageFileFormat.Dib:
				return ImageFormat.Bmp;
			case ImageFileFormat.Jpg:
				return ImageFormat.Jpeg;
			case ImageFileFormat.Tga:
			case ImageFileFormat.Png:
				return ImageFormat.Png;
			default:
				return ImageFormat.Png;
			}
		}
	}

	public static class CustomVertex
	{
		[StructLayout(LayoutKind.Sequential)]
		public struct PositionColored
		{
			public float X;
			public float Y;
			public float Z;
			public int Color;

			public PositionColored(float xvalue, float yvalue, float zvalue, int color)
			{
				X = xvalue;
				Y = yvalue;
				Z = zvalue;
				Color = color;
			}

			public PositionColored(Vector3 pos, int color)
			{
				X = pos.X;
				Y = pos.Y;
				Z = pos.Z;
				Color = color;
			}

			public static readonly VertexFormats Format = VertexFormats.Position | VertexFormats.Diffuse;
			public static int StrideSize => 16;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct PositionNormal
		{
			public float X;
			public float Y;
			public float Z;
			public float Nx;
			public float Ny;
			public float Nz;

			public PositionNormal(Vector3 pos, Vector3 nor)
			{
				X = pos.X; Y = pos.Y; Z = pos.Z;
				Nx = nor.X; Ny = nor.Y; Nz = nor.Z;
			}

			public PositionNormal(float xvalue, float yvalue, float zvalue, float nxvalue, float nyvalue, float nzvalue)
			{
				X = xvalue; Y = yvalue; Z = zvalue;
				Nx = nxvalue; Ny = nyvalue; Nz = nzvalue;
			}

			public static readonly VertexFormats Format = VertexFormats.PositionNormal;
			public static int StrideSize => 24;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct PositionNormalColored
		{
			public float X;
			public float Y;
			public float Z;
			public float Nx;
			public float Ny;
			public float Nz;
			public int Color;

			public PositionNormalColored(Vector3 pos, Vector3 nor, int color)
			{
				X = pos.X; Y = pos.Y; Z = pos.Z;
				Nx = nor.X; Ny = nor.Y; Nz = nor.Z;
				Color = color;
			}

			public static readonly VertexFormats Format = VertexFormats.Position | VertexFormats.Normal | VertexFormats.Diffuse;
			public static int StrideSize => 28;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct PositionNormalTextured
		{
			public float X;
			public float Y;
			public float Z;
			public float Nx;
			public float Ny;
			public float Nz;
			public float Tu;
			public float Tv;

			public PositionNormalTextured(Vector3 pos, Vector3 nor, float tuvalue, float tvvalue)
			{
				X = pos.X; Y = pos.Y; Z = pos.Z;
				Nx = nor.X; Ny = nor.Y; Nz = nor.Z;
				Tu = tuvalue; Tv = tvvalue;
			}

			public PositionNormalTextured(float xvalue, float yvalue, float zvalue, float nxvalue, float nyvalue, float nzvalue, float tuvalue, float tvvalue)
			{
				X = xvalue; Y = yvalue; Z = zvalue;
				Nx = nxvalue; Ny = nyvalue; Nz = nzvalue;
				Tu = tuvalue; Tv = tvvalue;
			}

			public static readonly VertexFormats Format = VertexFormats.PositionNormal | VertexFormats.Texture1;
			public static int StrideSize => 32;
		}
	}
}
