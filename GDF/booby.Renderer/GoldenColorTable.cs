using System.Drawing;
using System.Windows.Forms;

namespace booby.Renderer;

public class GoldenColorTable : ProfessionalColorTable
{
	private static GoldenColorTable global = new GoldenColorTable();

	public static GoldenColorTable Global => global;

	public override Color CheckBackground => Color.Transparent;

	public override Color CheckPressedBackground => Color.Transparent;

	public override Color CheckSelectedBackground => ((ProfessionalColorTable)this).CheckPressedBackground;

	public override Color SeparatorDark => Color.FromArgb(242, 165, 40);

	public override Color SeparatorLight => Color.FromArgb(254, 247, 233);

	public override Color MenuStripGradientBegin => SystemColors.ControlLightLight;

	public override Color MenuStripGradientEnd => ((ProfessionalColorTable)this).ToolStripGradientEnd;

	public override Color MenuBorder => ((ProfessionalColorTable)this).ToolStripBorder;

	public override Color ToolStripGradientBegin => Color.FromArgb(80, 40, 0);

	public override Color ToolStripGradientMiddle => Color.FromArgb(255, 255, 200);

	public Color ToolStripGradientMiddleEnd => Color.FromArgb(180, 130, 16);

	public override Color ToolStripGradientEnd => Color.FromArgb(80, 40, 0);

	public override Color ToolStripBorder => Color.FromArgb(242, 165, 40);

	public override Color OverflowButtonGradientBegin => Color.FromArgb(254, 247, 233);

	public override Color OverflowButtonGradientMiddle => Color.FromArgb(254, 231, 184);

	public override Color OverflowButtonGradientEnd => Color.FromArgb(180, 130, 16);

	public override Color ImageMarginGradientBegin => Color.FromArgb(242, 165, 40);

	public override Color ImageMarginGradientEnd => ((ProfessionalColorTable)this).ImageMarginGradientBegin;

	public override Color ImageMarginGradientMiddle => ((ProfessionalColorTable)this).ImageMarginGradientBegin;

	public override Color ImageMarginRevealedGradientBegin => ((ProfessionalColorTable)this).ImageMarginGradientBegin;

	public override Color ImageMarginRevealedGradientEnd => ((ProfessionalColorTable)this).ImageMarginGradientEnd;

	public override Color ImageMarginRevealedGradientMiddle => ((ProfessionalColorTable)this).ImageMarginGradientMiddle;

	public override Color MenuItemPressedGradientBegin => Color.FromArgb(255, 255, 255);

	public override Color MenuItemPressedGradientEnd => Color.FromArgb(180, 130, 16);

	public override Color MenuItemPressedGradientMiddle => Color.FromArgb(242, 165, 40);

	public override Color MenuItemBorder => ((ProfessionalColorTable)this).ButtonSelectedBorder;

	public override Color MenuItemSelected => ((ProfessionalColorTable)this).ButtonSelectedGradientBegin;

	public override Color MenuItemSelectedGradientBegin => ((ProfessionalColorTable)this).ButtonSelectedGradientBegin;

	public override Color MenuItemSelectedGradientEnd => ((ProfessionalColorTable)this).ButtonSelectedGradientBegin;

	public override Color ButtonSelectedGradientBegin => Color.FromArgb(254, 247, 233);

	public override Color ButtonSelectedGradientMiddle => ((ProfessionalColorTable)this).ButtonSelectedGradientBegin;

	public override Color ButtonSelectedGradientEnd => ((ProfessionalColorTable)this).ButtonSelectedGradientBegin;

	public override Color ButtonSelectedBorder => Color.FromArgb(242, 165, 40);

	public override Color ButtonPressedGradientBegin => Color.FromArgb(254, 247, 233);

	public override Color ButtonPressedGradientMiddle => ((ProfessionalColorTable)this).ButtonPressedGradientBegin;

	public override Color ButtonPressedGradientEnd => ((ProfessionalColorTable)this).ButtonPressedGradientBegin;

	public override Color ButtonPressedBorder => Color.FromArgb(242, 165, 40);

	public override Color ButtonCheckedGradientBegin => Color.FromArgb(242, 165, 40);

	public override Color ButtonCheckedGradientMiddle => ((ProfessionalColorTable)this).ButtonCheckedGradientBegin;

	public override Color ButtonCheckedGradientEnd => ((ProfessionalColorTable)this).ButtonCheckedGradientBegin;

	public override Color ButtonCheckedHighlightBorder => Color.FromArgb(242, 165, 40);

	public override Color ButtonCheckedHighlight => Color.FromArgb(254, 247, 233);

	public override Color ToolStripPanelGradientBegin => Color.FromArgb(140, 100, 10);

	public override Color ToolStripPanelGradientEnd => Color.FromArgb(220, 180, 64);
}
