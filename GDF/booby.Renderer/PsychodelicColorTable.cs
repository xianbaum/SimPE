using System.Drawing;
using System.Windows.Forms;

namespace booby.Renderer;

public class PsychodelicColorTable : ProfessionalColorTable
{
	private static PsychodelicColorTable global = new PsychodelicColorTable();

	public static PsychodelicColorTable Global => global;

	public override Color CheckBackground => Color.Transparent;

	public override Color CheckPressedBackground => Color.Transparent;

	public override Color CheckSelectedBackground => ((ProfessionalColorTable)this).CheckPressedBackground;

	public override Color SeparatorDark => Color.FromArgb(228, 184, 92);

	public override Color SeparatorLight => Color.FromArgb(138, 238, 247);

	public override Color MenuStripGradientBegin => Color.FromArgb(0, 255, 0);

	public override Color MenuStripGradientEnd => Color.FromArgb(255, 0, 0);

	public override Color MenuBorder => ((ProfessionalColorTable)this).ToolStripBorder;

	public override Color ToolStripGradientBegin => Color.FromArgb(255, 0, 0);

	public override Color ToolStripGradientMiddle => Color.FromArgb(255, 255, 0);

	public Color ToolStripGradientMiddleEnd => Color.FromArgb(64, 120, 255);

	public override Color ToolStripGradientEnd => Color.FromArgb(128, 0, 255);

	public override Color ToolStripBorder => Color.FromArgb(0, 255, 0);

	public override Color OverflowButtonGradientBegin => Color.FromArgb(255, 0, 0);

	public override Color OverflowButtonGradientMiddle => Color.FromArgb(255, 255, 0);

	public override Color OverflowButtonGradientEnd => Color.FromArgb(0, 0, 255);

	public override Color ImageMarginGradientBegin => Color.FromArgb(255, 0, 0);

	public override Color ImageMarginGradientEnd => Color.FromArgb(0, 0, 255);

	public override Color ImageMarginGradientMiddle => Color.FromArgb(255, 255, 0);

	public override Color ImageMarginRevealedGradientBegin => ((ProfessionalColorTable)this).ImageMarginGradientBegin;

	public override Color ImageMarginRevealedGradientEnd => ((ProfessionalColorTable)this).ImageMarginGradientEnd;

	public override Color ImageMarginRevealedGradientMiddle => ((ProfessionalColorTable)this).ImageMarginGradientMiddle;

	public override Color MenuItemPressedGradientBegin => Color.FromArgb(0, 0, 255);

	public override Color MenuItemPressedGradientEnd => Color.FromArgb(255, 255, 0);

	public override Color MenuItemPressedGradientMiddle => Color.FromArgb(255, 0, 0);

	public override Color MenuItemBorder => ((ProfessionalColorTable)this).ButtonSelectedBorder;

	public override Color MenuItemSelected => ((ProfessionalColorTable)this).ButtonSelectedGradientBegin;

	public override Color MenuItemSelectedGradientBegin => ((ProfessionalColorTable)this).ButtonSelectedGradientBegin;

	public override Color MenuItemSelectedGradientEnd => ((ProfessionalColorTable)this).ButtonSelectedGradientBegin;

	public override Color ButtonSelectedGradientBegin => Color.FromArgb(0, 0, 255);

	public override Color ButtonSelectedGradientMiddle => Color.FromArgb(255, 0, 0);

	public override Color ButtonSelectedGradientEnd => Color.FromArgb(0, 255, 0);

	public override Color ButtonSelectedBorder => Color.FromArgb(100, 60, 200);

	public override Color ButtonPressedGradientBegin => Color.FromArgb(128, 0, 255);

	public override Color ButtonPressedGradientMiddle => ((ProfessionalColorTable)this).ButtonPressedGradientBegin;

	public override Color ButtonPressedGradientEnd => ((ProfessionalColorTable)this).ButtonPressedGradientBegin;

	public override Color ButtonPressedBorder => Color.FromArgb(255, 128, 0);

	public override Color ButtonCheckedGradientBegin => Color.FromArgb(0, 255, 0);

	public override Color ButtonCheckedGradientMiddle => Color.FromArgb(0, 0, 255);

	public override Color ButtonCheckedGradientEnd => Color.FromArgb(255, 0, 0);

	public override Color ButtonCheckedHighlightBorder => Color.FromArgb(255, 128, 0);

	public override Color ButtonCheckedHighlight => Color.FromArgb(128, 0, 255);

	public override Color ToolStripPanelGradientBegin => Color.FromArgb(255, 0, 0);

	public override Color ToolStripPanelGradientEnd => Color.FromArgb(0, 0, 255);
}
