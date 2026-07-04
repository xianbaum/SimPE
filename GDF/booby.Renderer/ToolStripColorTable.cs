using System.Drawing;
using System.Windows.Forms;

namespace booby.Renderer;

internal class ToolStripColorTable : ProfessionalColorTable
{
	public override Color CheckBackground => Color.FromArgb(225, 230, 232);

	public override Color CheckPressedBackground => Color.FromArgb(49, 106, 197);

	public override Color CheckSelectedBackground => ((ProfessionalColorTable)this).CheckPressedBackground;

	public override Color SeparatorDark => Color.FromArgb(197, 194, 184);

	public override Color SeparatorLight => Color.FromArgb(252, 252, 249);

	public override Color MenuStripGradientBegin => Color.FromArgb(229, 229, 215);

	public override Color MenuStripGradientEnd => Color.White;

	public override Color MenuBorder => Color.FromArgb(138, 134, 122);

	public override Color ToolStripGradientBegin => Color.FromArgb(253, 253, 251);

	public override Color ToolStripGradientMiddle => Color.FromArgb(236, 236, 229);

	public override Color ToolStripGradientEnd => Color.FromArgb(190, 190, 167);

	public override Color ToolStripBorder => Color.FromArgb(163, 163, 124);

	public override Color OverflowButtonGradientBegin => Color.FromArgb(239, 238, 235);

	public override Color OverflowButtonGradientMiddle => Color.FromArgb(225, 225, 218);

	public override Color OverflowButtonGradientEnd => Color.FromArgb(146, 146, 118);

	public override Color ImageMarginGradientBegin => Color.FromArgb(254, 254, 251);

	public override Color ImageMarginGradientEnd => Color.FromArgb(196, 195, 172);

	public override Color ImageMarginGradientMiddle => Color.FromArgb(237, 233, 226);

	public override Color ImageMarginRevealedGradientBegin => ((ProfessionalColorTable)this).ImageMarginGradientBegin;

	public override Color ImageMarginRevealedGradientEnd => ((ProfessionalColorTable)this).ImageMarginGradientEnd;

	public override Color ImageMarginRevealedGradientMiddle => ((ProfessionalColorTable)this).ImageMarginGradientMiddle;

	public override Color MenuItemPressedGradientBegin => Color.FromArgb(251, 251, 249);

	public override Color MenuItemPressedGradientEnd => Color.FromArgb(247, 245, 239);

	public override Color MenuItemPressedGradientMiddle => Color.FromArgb(249, 248, 244);

	public override Color MenuItemBorder => ((ProfessionalColorTable)this).ButtonSelectedBorder;

	public override Color MenuItemSelected => ((ProfessionalColorTable)this).ButtonSelectedGradientBegin;

	public override Color MenuItemSelectedGradientBegin => ((ProfessionalColorTable)this).ButtonSelectedGradientBegin;

	public override Color MenuItemSelectedGradientEnd => ((ProfessionalColorTable)this).ButtonSelectedGradientBegin;

	public override Color ButtonSelectedGradientBegin => Color.FromArgb(193, 210, 238);

	public override Color ButtonSelectedGradientMiddle => ((ProfessionalColorTable)this).ButtonSelectedGradientBegin;

	public override Color ButtonSelectedGradientEnd => ((ProfessionalColorTable)this).ButtonSelectedGradientBegin;

	public override Color ButtonSelectedBorder => Color.FromArgb(49, 106, 197);

	public override Color ButtonPressedGradientBegin => Color.FromArgb(152, 181, 226);

	public override Color ButtonPressedGradientMiddle => ((ProfessionalColorTable)this).ButtonPressedGradientBegin;

	public override Color ButtonPressedGradientEnd => ((ProfessionalColorTable)this).ButtonPressedGradientBegin;

	public override Color ButtonPressedBorder => Color.FromArgb(75, 75, 111);

	public override Color ButtonCheckedGradientBegin => Color.FromArgb(225, 230, 232);

	public override Color ButtonCheckedGradientMiddle => ((ProfessionalColorTable)this).ButtonCheckedGradientBegin;

	public override Color ButtonCheckedGradientEnd => ((ProfessionalColorTable)this).ButtonCheckedGradientBegin;

	public override Color ButtonCheckedHighlightBorder => Color.FromArgb(75, 75, 111);

	public override Color ButtonCheckedHighlight => Color.FromArgb(152, 181, 226);

	public override Color ToolStripPanelGradientBegin => ((ProfessionalColorTable)this).MenuStripGradientBegin;

	public override Color ToolStripPanelGradientEnd => ((ProfessionalColorTable)this).MenuStripGradientEnd;
}
