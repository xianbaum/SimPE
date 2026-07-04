using System.Drawing;
using System.Windows.Forms;

namespace booby.Renderer;

public class GreenyColorTable : ProfessionalColorTable
{
	private static GreenyColorTable global = new GreenyColorTable();

	public static GreenyColorTable Global => global;

	public override Color CheckBackground => Color.Transparent;

	public override Color CheckPressedBackground => Color.Transparent;

	public override Color CheckSelectedBackground => ((ProfessionalColorTable)this).CheckPressedBackground;

	public override Color SeparatorDark => Color.FromArgb(176, 197, 160);

	public override Color SeparatorLight => Color.FromArgb(249, 253, 240);

	public override Color MenuStripGradientBegin => Color.FromArgb(250, 255, 220);

	public override Color MenuStripGradientEnd => ((ProfessionalColorTable)this).ToolStripGradientEnd;

	public override Color MenuBorder => ((ProfessionalColorTable)this).ToolStripBorder;

	public override Color ToolStripGradientBegin => Color.FromArgb(252, 255, 248);

	public override Color ToolStripGradientMiddle => Color.FromArgb(240, 253, 230);

	public Color ToolStripGradientMiddleEnd => Color.FromArgb(200, 240, 180);

	public override Color ToolStripGradientEnd => Color.FromArgb(250, 253, 230);

	public override Color ToolStripBorder => Color.FromArgb(193, 197, 180);

	public override Color OverflowButtonGradientBegin => Color.FromArgb(252, 255, 248);

	public override Color OverflowButtonGradientMiddle => Color.FromArgb(240, 253, 230);

	public override Color OverflowButtonGradientEnd => Color.FromArgb(200, 240, 180);

	public override Color ImageMarginGradientBegin => Color.FromArgb(94, 106, 78);

	public override Color ImageMarginGradientEnd => ((ProfessionalColorTable)this).ImageMarginGradientBegin;

	public override Color ImageMarginGradientMiddle => ((ProfessionalColorTable)this).ImageMarginGradientBegin;

	public override Color ImageMarginRevealedGradientBegin => ((ProfessionalColorTable)this).ImageMarginGradientBegin;

	public override Color ImageMarginRevealedGradientEnd => ((ProfessionalColorTable)this).ImageMarginGradientEnd;

	public override Color ImageMarginRevealedGradientMiddle => ((ProfessionalColorTable)this).ImageMarginGradientMiddle;

	public override Color MenuItemPressedGradientBegin => Color.FromArgb(252, 252, 200);

	public override Color MenuItemPressedGradientEnd => Color.FromArgb(242, 250, 228);

	public override Color MenuItemPressedGradientMiddle => Color.FromArgb(248, 249, 244);

	public override Color MenuItemBorder => ((ProfessionalColorTable)this).ButtonSelectedBorder;

	public override Color MenuItemSelected => ((ProfessionalColorTable)this).ButtonSelectedGradientBegin;

	public override Color MenuItemSelectedGradientBegin => ((ProfessionalColorTable)this).ButtonSelectedGradientBegin;

	public override Color MenuItemSelectedGradientEnd => ((ProfessionalColorTable)this).ButtonSelectedGradientBegin;

	public override Color ButtonSelectedGradientBegin => Color.FromArgb(202, 252, 222);

	public override Color ButtonSelectedGradientMiddle => ((ProfessionalColorTable)this).ButtonSelectedGradientBegin;

	public override Color ButtonSelectedGradientEnd => ((ProfessionalColorTable)this).ButtonSelectedGradientBegin;

	public override Color ButtonSelectedBorder => Color.FromArgb(80, 200, 80);

	public override Color ButtonPressedGradientBegin => Color.FromArgb(160, 240, 120);

	public override Color ButtonPressedGradientMiddle => ((ProfessionalColorTable)this).ButtonPressedGradientBegin;

	public override Color ButtonPressedGradientEnd => ((ProfessionalColorTable)this).ButtonPressedGradientBegin;

	public override Color ButtonPressedBorder => Color.FromArgb(111, 114, 75);

	public override Color ButtonCheckedGradientBegin => Color.FromArgb(222, 244, 204);

	public override Color ButtonCheckedGradientMiddle => ((ProfessionalColorTable)this).ButtonCheckedGradientBegin;

	public override Color ButtonCheckedGradientEnd => ((ProfessionalColorTable)this).ButtonCheckedGradientBegin;

	public override Color ButtonCheckedHighlightBorder => Color.FromArgb(109, 112, 72);

	public override Color ButtonCheckedHighlight => Color.FromArgb(224, 228, 152);

	public override Color ToolStripPanelGradientBegin => Color.FromArgb(255, 255, 207);

	public override Color ToolStripPanelGradientEnd => Color.FromArgb(180, 242, 207);
}
