using System.Drawing;
using System.Windows.Forms;

namespace booby.Renderer;

public class PurpleColorTable : ProfessionalColorTable
{
	private static PurpleColorTable global = new PurpleColorTable();

	public static PurpleColorTable Global => global;

	public override Color CheckBackground => Color.Transparent;

	public override Color CheckPressedBackground => Color.Transparent;

	public override Color CheckSelectedBackground => ((ProfessionalColorTable)this).CheckPressedBackground;

	public override Color SeparatorDark => Color.FromArgb(140, 120, 200);

	public override Color SeparatorLight => Color.FromArgb(248, 240, 254);

	public override Color MenuStripGradientBegin => Color.FromArgb(235, 220, 255);

	public override Color MenuStripGradientEnd => ((ProfessionalColorTable)this).ToolStripGradientEnd;

	public override Color MenuBorder => ((ProfessionalColorTable)this).ToolStripBorder;

	public override Color ToolStripGradientBegin => Color.FromArgb(230, 220, 255);

	public override Color ToolStripGradientMiddle => Color.FromArgb(255, 255, 255);

	public Color ToolStripGradientMiddleEnd => Color.FromArgb(250, 230, 255);

	public override Color ToolStripGradientEnd => Color.FromArgb(110, 50, 150);

	public override Color ToolStripBorder => Color.FromArgb(193, 180, 197);

	public override Color OverflowButtonGradientBegin => Color.FromArgb(252, 248, 255);

	public override Color OverflowButtonGradientMiddle => Color.FromArgb(240, 230, 253);

	public override Color OverflowButtonGradientEnd => Color.FromArgb(200, 180, 240);

	public override Color ImageMarginGradientBegin => Color.FromArgb(94, 78, 106);

	public override Color ImageMarginGradientEnd => ((ProfessionalColorTable)this).ImageMarginGradientBegin;

	public override Color ImageMarginGradientMiddle => ((ProfessionalColorTable)this).ImageMarginGradientBegin;

	public override Color ImageMarginRevealedGradientBegin => ((ProfessionalColorTable)this).ImageMarginGradientBegin;

	public override Color ImageMarginRevealedGradientEnd => ((ProfessionalColorTable)this).ImageMarginGradientEnd;

	public override Color ImageMarginRevealedGradientMiddle => ((ProfessionalColorTable)this).ImageMarginGradientMiddle;

	public override Color MenuItemPressedGradientBegin => Color.FromArgb(252, 200, 252);

	public override Color MenuItemPressedGradientEnd => Color.FromArgb(242, 228, 250);

	public override Color MenuItemPressedGradientMiddle => Color.FromArgb(248, 244, 249);

	public override Color MenuItemBorder => ((ProfessionalColorTable)this).ButtonSelectedBorder;

	public override Color MenuItemSelected => ((ProfessionalColorTable)this).ButtonSelectedGradientBegin;

	public override Color MenuItemSelectedGradientBegin => ((ProfessionalColorTable)this).ButtonSelectedGradientBegin;

	public override Color MenuItemSelectedGradientEnd => ((ProfessionalColorTable)this).ButtonSelectedGradientBegin;

	public override Color ButtonSelectedGradientBegin => Color.FromArgb(222, 202, 252);

	public override Color ButtonSelectedGradientMiddle => ((ProfessionalColorTable)this).ButtonSelectedGradientBegin;

	public override Color ButtonSelectedGradientEnd => ((ProfessionalColorTable)this).ButtonSelectedGradientBegin;

	public override Color ButtonSelectedBorder => Color.FromArgb(100, 60, 200);

	public override Color ButtonPressedGradientBegin => Color.FromArgb(160, 120, 240);

	public override Color ButtonPressedGradientMiddle => ((ProfessionalColorTable)this).ButtonPressedGradientBegin;

	public override Color ButtonPressedGradientEnd => ((ProfessionalColorTable)this).ButtonPressedGradientBegin;

	public override Color ButtonPressedBorder => Color.FromArgb(111, 75, 114);

	public override Color ButtonCheckedGradientBegin => Color.FromArgb(222, 204, 244);

	public override Color ButtonCheckedGradientMiddle => ((ProfessionalColorTable)this).ButtonCheckedGradientBegin;

	public override Color ButtonCheckedGradientEnd => ((ProfessionalColorTable)this).ButtonCheckedGradientBegin;

	public override Color ButtonCheckedHighlightBorder => Color.FromArgb(109, 72, 112);

	public override Color ButtonCheckedHighlight => Color.FromArgb(194, 152, 248);

	public override Color ToolStripPanelGradientBegin => Color.FromArgb(248, 227, 255);

	public override Color ToolStripPanelGradientEnd => Color.FromArgb(96, 64, 128);
}
