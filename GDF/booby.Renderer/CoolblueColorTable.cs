using System.Drawing;
using System.Windows.Forms;

namespace booby.Renderer;

public class CoolblueColorTable : ProfessionalColorTable
{
	private static CoolblueColorTable global = new CoolblueColorTable();

	public static CoolblueColorTable Global => global;

	public override Color CheckBackground => Color.Transparent;

	public override Color CheckPressedBackground => Color.Transparent;

	public override Color CheckSelectedBackground => ((ProfessionalColorTable)this).CheckPressedBackground;

	public override Color SeparatorDark => Color.FromArgb(120, 120, 200);

	public override Color SeparatorLight => Color.FromArgb(240, 248, 254);

	public override Color MenuStripGradientBegin => Color.FromArgb(220, 235, 255);

	public override Color MenuStripGradientEnd => ((ProfessionalColorTable)this).ToolStripGradientEnd;

	public override Color MenuBorder => ((ProfessionalColorTable)this).ToolStripBorder;

	public override Color ToolStripGradientBegin => Color.FromArgb(220, 230, 255);

	public override Color ToolStripGradientMiddle => Color.FromArgb(255, 255, 255);

	public Color ToolStripGradientMiddleEnd => Color.FromArgb(230, 250, 255);

	public override Color ToolStripGradientEnd => Color.FromArgb(50, 80, 160);

	public override Color ToolStripBorder => Color.FromArgb(180, 190, 197);

	public override Color OverflowButtonGradientBegin => Color.FromArgb(248, 252, 255);

	public override Color OverflowButtonGradientMiddle => Color.FromArgb(230, 240, 253);

	public override Color OverflowButtonGradientEnd => Color.FromArgb(180, 200, 240);

	public override Color ImageMarginGradientBegin => Color.FromArgb(78, 94, 106);

	public override Color ImageMarginGradientEnd => ((ProfessionalColorTable)this).ImageMarginGradientBegin;

	public override Color ImageMarginGradientMiddle => ((ProfessionalColorTable)this).ImageMarginGradientBegin;

	public override Color ImageMarginRevealedGradientBegin => ((ProfessionalColorTable)this).ImageMarginGradientBegin;

	public override Color ImageMarginRevealedGradientEnd => ((ProfessionalColorTable)this).ImageMarginGradientEnd;

	public override Color ImageMarginRevealedGradientMiddle => ((ProfessionalColorTable)this).ImageMarginGradientMiddle;

	public override Color MenuItemPressedGradientBegin => Color.FromArgb(200, 250, 252);

	public override Color MenuItemPressedGradientEnd => Color.FromArgb(228, 242, 250);

	public override Color MenuItemPressedGradientMiddle => Color.FromArgb(244, 248, 249);

	public override Color MenuItemBorder => ((ProfessionalColorTable)this).ButtonSelectedBorder;

	public override Color MenuItemSelected => ((ProfessionalColorTable)this).ButtonSelectedGradientBegin;

	public override Color MenuItemSelectedGradientBegin => ((ProfessionalColorTable)this).ButtonSelectedGradientBegin;

	public override Color MenuItemSelectedGradientEnd => ((ProfessionalColorTable)this).ButtonSelectedGradientBegin;

	public override Color ButtonSelectedGradientBegin => Color.FromArgb(202, 222, 252);

	public override Color ButtonSelectedGradientMiddle => ((ProfessionalColorTable)this).ButtonSelectedGradientBegin;

	public override Color ButtonSelectedGradientEnd => ((ProfessionalColorTable)this).ButtonSelectedGradientBegin;

	public override Color ButtonSelectedBorder => Color.FromArgb(60, 100, 200);

	public override Color ButtonPressedGradientBegin => Color.FromArgb(120, 160, 240);

	public override Color ButtonPressedGradientMiddle => ((ProfessionalColorTable)this).ButtonPressedGradientBegin;

	public override Color ButtonPressedGradientEnd => ((ProfessionalColorTable)this).ButtonPressedGradientBegin;

	public override Color ButtonPressedBorder => Color.FromArgb(75, 100, 114);

	public override Color ButtonCheckedGradientBegin => Color.FromArgb(204, 222, 244);

	public override Color ButtonCheckedGradientMiddle => ((ProfessionalColorTable)this).ButtonCheckedGradientBegin;

	public override Color ButtonCheckedGradientEnd => ((ProfessionalColorTable)this).ButtonCheckedGradientBegin;

	public override Color ButtonCheckedHighlightBorder => Color.FromArgb(72, 109, 112);

	public override Color ButtonCheckedHighlight => Color.FromArgb(152, 194, 248);

	public override Color ToolStripPanelGradientBegin => Color.FromArgb(227, 240, 255);

	public override Color ToolStripPanelGradientEnd => Color.FromArgb(120, 160, 255);
}
