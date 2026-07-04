using System.Drawing;
using System.Windows.Forms;

namespace booby.Renderer;

public class PinkyColorTable : ProfessionalColorTable
{
	private static PinkyColorTable global = new PinkyColorTable();

	public static PinkyColorTable Global => global;

	public override Color CheckBackground => Color.Transparent;

	public override Color CheckPressedBackground => Color.Transparent;

	public override Color CheckSelectedBackground => ((ProfessionalColorTable)this).CheckPressedBackground;

	public override Color SeparatorDark => Color.FromArgb(197, 176, 184);

	public override Color SeparatorLight => Color.FromArgb(253, 244, 249);

	public override Color MenuStripGradientBegin => Color.FromArgb(229, 212, 223);

	public override Color MenuStripGradientEnd => ((ProfessionalColorTable)this).ToolStripGradientEnd;

	public override Color MenuBorder => ((ProfessionalColorTable)this).ToolStripBorder;

	public override Color ToolStripGradientBegin => Color.FromArgb(255, 248, 254);

	public override Color ToolStripGradientMiddle => Color.FromArgb(253, 240, 250);

	public Color ToolStripGradientMiddleEnd => Color.FromArgb(252, 232, 246);

	public override Color ToolStripGradientEnd => Color.FromArgb(250, 200, 240);

	public override Color ToolStripBorder => Color.FromArgb(197, 181, 193);

	public override Color OverflowButtonGradientBegin => Color.FromArgb(255, 224, 235);

	public override Color OverflowButtonGradientMiddle => Color.FromArgb(232, 209, 225);

	public override Color OverflowButtonGradientEnd => Color.FromArgb(162, 118, 150);

	public override Color ImageMarginGradientBegin => Color.FromArgb(106, 78, 94);

	public override Color ImageMarginGradientEnd => ((ProfessionalColorTable)this).ImageMarginGradientBegin;

	public override Color ImageMarginGradientMiddle => ((ProfessionalColorTable)this).ImageMarginGradientBegin;

	public override Color ImageMarginRevealedGradientBegin => ((ProfessionalColorTable)this).ImageMarginGradientBegin;

	public override Color ImageMarginRevealedGradientEnd => ((ProfessionalColorTable)this).ImageMarginGradientEnd;

	public override Color ImageMarginRevealedGradientMiddle => ((ProfessionalColorTable)this).ImageMarginGradientMiddle;

	public override Color MenuItemPressedGradientBegin => Color.FromArgb(251, 249, 251);

	public override Color MenuItemPressedGradientEnd => Color.FromArgb(250, 229, 242);

	public override Color MenuItemPressedGradientMiddle => Color.FromArgb(249, 244, 248);

	public override Color MenuItemBorder => ((ProfessionalColorTable)this).ButtonSelectedBorder;

	public override Color MenuItemSelected => ((ProfessionalColorTable)this).ButtonSelectedGradientBegin;

	public override Color MenuItemSelectedGradientBegin => ((ProfessionalColorTable)this).ButtonSelectedGradientBegin;

	public override Color MenuItemSelectedGradientEnd => ((ProfessionalColorTable)this).ButtonSelectedGradientBegin;

	public override Color ButtonSelectedGradientBegin => Color.FromArgb(239, 193, 238);

	public override Color ButtonSelectedGradientMiddle => ((ProfessionalColorTable)this).ButtonSelectedGradientBegin;

	public override Color ButtonSelectedGradientEnd => ((ProfessionalColorTable)this).ButtonSelectedGradientBegin;

	public override Color ButtonSelectedBorder => Color.FromArgb(197, 49, 176);

	public override Color ButtonPressedGradientBegin => Color.FromArgb(230, 152, 226);

	public override Color ButtonPressedGradientMiddle => ((ProfessionalColorTable)this).ButtonPressedGradientBegin;

	public override Color ButtonPressedGradientEnd => ((ProfessionalColorTable)this).ButtonPressedGradientBegin;

	public override Color ButtonPressedBorder => Color.FromArgb(114, 75, 111);

	public override Color ButtonCheckedGradientBegin => Color.FromArgb(234, 224, 232);

	public override Color ButtonCheckedGradientMiddle => ((ProfessionalColorTable)this).ButtonCheckedGradientBegin;

	public override Color ButtonCheckedGradientEnd => ((ProfessionalColorTable)this).ButtonCheckedGradientBegin;

	public override Color ButtonCheckedHighlightBorder => Color.FromArgb(112, 72, 109);

	public override Color ButtonCheckedHighlight => Color.FromArgb(228, 152, 224);

	public override Color ToolStripPanelGradientBegin => Color.White;

	public override Color ToolStripPanelGradientEnd => Color.FromArgb(242, 207, 239);
}
