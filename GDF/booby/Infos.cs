using System.Drawing;
using System.Drawing.Text;

namespace booby;

public class Infos
{
	public static bool IsFontinstalled(string fonte)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Expected O, but got Unknown
		InstalledFontCollection val = new InstalledFontCollection();
		FontFamily[] families = ((FontCollection)val).Families;
		int num = families.Length;
		for (int i = 0; i < num; i++)
		{
			if (families[i].Name == fonte)
			{
				return true;
			}
		}
		return false;
	}
}
