using System.Drawing;
using System.Windows.Forms;

namespace booby.Renderer;

public class GlossyColorTable : ProfessionalColorTable
{
	private static GlossyColorTable global = new GlossyColorTable();

	public static GlossyColorTable Global => global;

	public override Color CheckBackground => Color.Transparent;

	public override Color CheckPressedBackground => Color.Transparent;

	public override Color CheckSelectedBackground => ((ProfessionalColorTable)this).CheckPressedBackground;

	public override Color SeparatorDark => Color.FromArgb(197, 194, 184);

	public override Color SeparatorLight => Color.FromArgb(252, 252, 249);

	public override Color MenuStripGradientBegin => Color.FromArgb(229, 229, 215);

	public override Color MenuStripGradientEnd => ((ProfessionalColorTable)this).ToolStripGradientEnd;

	public override Color MenuBorder => ((ProfessionalColorTable)this).ToolStripBorder;

	public override Color ToolStripGradientBegin => Color.White;

	public override Color ToolStripGradientMiddle => Color.FromArgb(241, 241, 241);

	public Color ToolStripGradientMiddleEnd => Color.FromArgb(233, 233, 233);

	public override Color ToolStripGradientEnd => Color.FromArgb(254, 255, 255);

	public override Color ToolStripBorder => Color.FromArgb(181, 193, 193);

	public override Color OverflowButtonGradientBegin => Color.FromArgb(239, 238, 235);

	public override Color OverflowButtonGradientMiddle => Color.FromArgb(225, 225, 218);

	public override Color OverflowButtonGradientEnd => Color.FromArgb(146, 146, 118);

	public override Color ImageMarginGradientBegin => Color.FromArgb(94, 106, 121);

	public override Color ImageMarginGradientEnd => ((ProfessionalColorTable)this).ImageMarginGradientBegin;

	public override Color ImageMarginGradientMiddle => ((ProfessionalColorTable)this).ImageMarginGradientBegin;

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

	public override Color ToolStripPanelGradientBegin => Color.White;

	public override Color ToolStripPanelGradientEnd => Color.FromArgb(239, 239, 239);
}
