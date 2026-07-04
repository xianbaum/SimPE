using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Resources;
using System.Runtime.CompilerServices;

namespace booby.Properties;

[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0")]
[CompilerGenerated]
[DebuggerNonUserCode]
internal class Resources
{
	private static ResourceManager resourceMan;

	private static CultureInfo resourceCulture;

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	internal static ResourceManager ResourceManager
	{
		get
		{
			if (object.ReferenceEquals(resourceMan, null))
			{
				ResourceManager resourceManager = new ResourceManager("booby.Properties.Resources", typeof(Resources).Assembly);
				resourceMan = resourceManager;
			}
			return resourceMan;
		}
	}

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	internal static CultureInfo Culture
	{
		get
		{
			return resourceCulture;
		}
		set
		{
			resourceCulture = value;
		}
	}

	// Audio streams are embedded as raw manifest resources (see GDF.csproj) rather than
	// through the ResourceManager, because the legacy .resx stored them as BinaryFormatter
	// payloads which cannot be deserialized on .NET 10. GetManifestResourceStream returns an
	// UnmanagedMemoryStream over the mapped assembly image, matching the original return type.
	private static UnmanagedMemoryStream WavStream(string name) =>
		(UnmanagedMemoryStream)typeof(Resources).Assembly.GetManifestResourceStream("booby.audio." + name + ".wav");

	internal static UnmanagedMemoryStream Aahh => WavStream("Aahh");

	internal static Bitmap Butblue
	{
		get
		{
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_001c: Expected O, but got Unknown
			object obj = ResourceManager.GetObject("Butblue", resourceCulture);
			return (Bitmap)obj;
		}
	}

	internal static Bitmap Butdef
	{
		get
		{
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_001c: Expected O, but got Unknown
			object obj = ResourceManager.GetObject("Butdef", resourceCulture);
			return (Bitmap)obj;
		}
	}

	internal static Bitmap Butgolden
	{
		get
		{
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_001c: Expected O, but got Unknown
			object obj = ResourceManager.GetObject("Butgolden", resourceCulture);
			return (Bitmap)obj;
		}
	}

	internal static Bitmap Butgreen
	{
		get
		{
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_001c: Expected O, but got Unknown
			object obj = ResourceManager.GetObject("Butgreen", resourceCulture);
			return (Bitmap)obj;
		}
	}

	internal static Bitmap Butlilac
	{
		get
		{
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_001c: Expected O, but got Unknown
			object obj = ResourceManager.GetObject("Butlilac", resourceCulture);
			return (Bitmap)obj;
		}
	}

	internal static Bitmap Butpink
	{
		get
		{
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_001c: Expected O, but got Unknown
			object obj = ResourceManager.GetObject("Butpink", resourceCulture);
			return (Bitmap)obj;
		}
	}

	internal static Bitmap Butpsycho
	{
		get
		{
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_001c: Expected O, but got Unknown
			object obj = ResourceManager.GetObject("Butpsycho", resourceCulture);
			return (Bitmap)obj;
		}
	}

	internal static Bitmap Butpurple
	{
		get
		{
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_001c: Expected O, but got Unknown
			object obj = ResourceManager.GetObject("Butpurple", resourceCulture);
			return (Bitmap)obj;
		}
	}

	internal static UnmanagedMemoryStream Cancel => WavStream("Cancel");

	internal static UnmanagedMemoryStream Hammer => WavStream("Hammer");

	internal static UnmanagedMemoryStream Newitem => WavStream("Newitem");

	internal static UnmanagedMemoryStream Ooh => WavStream("Ooh");

	internal Resources()
	{
	}
}
