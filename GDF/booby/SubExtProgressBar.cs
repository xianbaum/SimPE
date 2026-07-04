using System.ComponentModel;

namespace booby;

internal class SubExtProgressBar : ExtProgressBar
{
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public override int TokenCount
	{
		get
		{
			return base.TokenCount;
		}
		set
		{
			base.TokenCount = value;
		}
	}
}
