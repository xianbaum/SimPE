using System;

namespace Microsoft.DirectX.Direct3D
{
	public enum Cull
	{
		Default = 0,
		None = 1,
		Clockwise = 2,
		CounterClockwise = 3
	}

	public enum FillMode
	{
		Point = 1,
		WireFrame = 2,
		Solid = 3
	}

	public enum ShadeMode
	{
		Flat = 1,
		Gouraud = 2,
		Phong = 3
	}

	public enum Blend
	{
		Zero = 1,
		One = 2,
		SourceColor = 3,
		InvSourceColor = 4,
		SourceAlpha = 5,
		InvSourceAlpha = 6,
		DestinationAlpha = 7,
		InvDestinationAlpha = 8,
		DestinationColor = 9,
		InvDestinationColor = 10,
		SourceAlphaSat = 11,
		BothSourceAlpha = 12,
		BothInvSourceAlpha = 13,
		BlendFactor = 14,
		InvBlendFactor = 15
	}

	public enum BlendOperation
	{
		Add = 1,
		Subtract = 2,
		RevSubtract = 3,
		Min = 4,
		Max = 5
	}

	public enum TextureOperation
	{
		Disable = 1,
		SelectArg1 = 2,
		SelectArg2 = 3,
		Modulate = 4,
		Modulate2X = 5,
		Modulate4X = 6,
		Add = 7,
		AddSigned = 8,
		AddSigned2X = 9,
		Subtract = 10,
		AddSmooth = 11,
		BlendDiffuseAlpha = 12,
		BlendTextureAlpha = 13,
		BlendFactorAlpha = 14,
		BlendTextureAlphaPM = 15,
		BlendCurrentAlpha = 16,
		PreModulate = 17,
		ModulateAlphaAddColor = 18,
		ModulateColorAddAlpha = 19,
		ModulateInvAlphaAddColor = 20,
		ModulateInvColorAddAlpha = 21,
		BumpEnvironmentMap = 22,
		BumpEnvironmentMapLuminance = 23,
		DotProduct3 = 24,
		MultiplyAdd = 25,
		Lerp = 26
	}

	public enum TextureArgument
	{
		Diffuse = 0,
		Current = 1,
		TextureColor = 2,
		TFactor = 3,
		Specular = 4,
		Temp = 5,
		Constant = 6,
		SelectMask = 15,
		Complement = 16,
		AlphaReplicate = 32
	}

	public enum LightType
	{
		Point = 1,
		Spot = 2,
		Directional = 3
	}

	public enum SwapEffect
	{
		Discard = 1,
		Flip = 2,
		Copy = 3
	}

	public enum DeviceType
	{
		Hardware = 1,
		Reference = 2,
		Software = 3,
		NullReference = 4
	}

	[Flags]
	public enum CreateFlags
	{
		FpuPreserve = 2,
		MultiThreaded = 4,
		PureDevice = 0x10,
		SoftwareVertexProcessing = 0x20,
		HardwareVertexProcessing = 0x40,
		MixedVertexProcessing = 0x80,
		DisableDriverManagement = 0x100,
		AdapterGroupDevice = 0x200,
		DisableDriverManagementEx = 0x400,
		NoWindowChanges = 0x800
	}

	[Flags]
	public enum LockFlags
	{
		None = 0,
		ReadOnly = 0x10,
		NoSystemLock = 0x800,
		NoOverwrite = 0x1000,
		Discard = 0x2000,
		DoNotWait = 0x4000,
		NoDirtyUpdate = 0x8000
	}

	public enum DepthFormat
	{
		Unknown = 0,
		D16Lockable = 70,
		D32 = 71,
		D15S1 = 73,
		D24S8 = 75,
		D24X8 = 77,
		D24X4S4 = 79,
		D16 = 80,
		L16 = 81,
		D32SingleLockable = 82,
		D24SingleS8 = 83
	}

	[Flags]
	public enum ClearFlags
	{
		Target = 1,
		ZBuffer = 2,
		Stencil = 4
	}

	public enum BackBufferType
	{
		Mono = 0,
		Left = 1,
		Right = 2
	}

	public enum MultiSampleType
	{
		None = 0,
		NonMaskable = 1,
		TwoSamples = 2,
		ThreeSamples = 3,
		FourSamples = 4,
		FiveSamples = 5,
		SixSamples = 6,
		SevenSamples = 7,
		EightSamples = 8,
		NineSamples = 9,
		TenSamples = 10,
		ElevenSamples = 11,
		TwelveSamples = 12,
		ThirteenSamples = 13,
		FourteenSamples = 14,
		FifteenSamples = 15,
		SixteenSamples = 16
	}

	public enum Format
	{
		Unknown = 0,
		R8G8B8 = 20,
		A8R8G8B8 = 21,
		X8R8G8B8 = 22,
		R5G6B5 = 23,
		X1R5G5B5 = 24,
		A1R5G5B5 = 25,
		A4R4G4B4 = 26,
		A8 = 28,
		A2B10G10R10 = 31,
		A8B8G8R8 = 32,
		X8B8G8R8 = 33,
		G16R16 = 34,
		A2R10G10B10 = 35,
		A16B16G16R16 = 36,
		D16Lockable = 70,
		D32 = 71,
		D15S1 = 73,
		D24S8 = 75,
		D24X8 = 77,
		D24X4S4 = 79,
		D16 = 80,
		L16 = 81,
		D32SingleLockable = 82,
		D24SingleS8 = 83
	}

	[Flags]
	public enum VertexFormats
	{
		None = 0,
		Position = 2,
		Transformed = 4,
		Normal = 0x10,
		PointSize = 0x20,
		Diffuse = 0x40,
		Specular = 0x80,
		Texture0 = 0,
		Texture1 = 0x100,
		Texture2 = 0x200,
		Texture3 = 0x300,
		Texture4 = 0x400,
		PositionNormal = 0x12
	}

	[Flags]
	public enum MeshFlags
	{
		Use32Bit = 1,
		DoNotClip = 2,
		Points = 4,
		RtPatches = 8,
		VbSystemMem = 0x10,
		VbManaged = 0x20,
		VbWriteOnly = 0x40,
		VbDynamic = 0x80,
		IbSystemMem = 0x100,
		IbManaged = 0x200,
		IbWriteOnly = 0x400,
		IbDynamic = 0x800,
		VbShare = 0x1000,
		UseHardwareOnly = 0x2000,
		SystemMemory = 0x110,
		Managed = 0x220,
		WriteOnly = 0x440,
		Dynamic = 0x880,
		OptimizeCompact = 0x1000000,
		OptimizeAttributeSort = 0x2000000,
		OptimizeVertexCache = 0x4000000,
		OptimizeStripeReorder = 0x8000000,
		OptimizeIgnoreVerts = 0x10000000
	}

	public enum ImageFileFormat
	{
		Bmp = 0,
		Jpg = 1,
		Tga = 2,
		Png = 3,
		Dds = 4,
		Ppm = 5,
		Dib = 6,
		Hdr = 7,
		Pfm = 8
	}
}
