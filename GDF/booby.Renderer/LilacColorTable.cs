using System.Drawing;
using System.Windows.Forms;

namespace booby.Renderer;

public class LilacColorTable : ProfessionalColorTable
{
	private static LilacColorTable global = new LilacColorTable();

	public static LilacColorTable Global => global;

	public override Color CheckBackground => Color.Transparent;

	public override Color CheckPressedBackground => Color.Transparent;

	public override Color CheckSelectedBackground => ((ProfessionalColorTable)this).CheckPressedBackground;

	public override Color SeparatorDark => Color.FromArgb(160, 160, 210);

	public override Color SeparatorLight => Color.FromArgb(240, 240, 255);

	public override Color MenuStripGradientBegin => SystemColors.ControlLightLight;

	public override Color MenuStripGradientEnd => ((ProfessionalColorTable)this).ToolStripGradientEnd;

	public override Color MenuBorder => ((ProfessionalColorTable)this).ToolStripBorder;

	public override Color ToolStripGradientBegin => Color.FromArgb(208, 204, 255);

	public override Color ToolStripGradientMiddle => Color.FromArgb(250, 248, 255);

	public Color ToolStripGradientMiddleEnd => Color.FromArgb(240, 240, 255);

	public override Color ToolStripGradientEnd => Color.FromArgb(192, 192, 255);

	public override Color ToolStripBorder => Color.FromArgb(192, 192, 255);

	public override Color OverflowButtonGradientBegin => Color.FromArgb(252, 248, 255);

	public override Color OverflowButtonGradientMiddle => Color.FromArgb(240, 240, 255);

	public override Color OverflowButtonGradientEnd => Color.FromArgb(192, 192, 255);

	public override Color ImageMarginGradientBegin => Color.FromArgb(148, 148, 190);

	public override Color ImageMarginGradientEnd => ((ProfessionalColorTable)this).ImageMarginGradientBegin;

	public override Color ImageMarginGradientMiddle => ((ProfessionalColorTable)this).ImageMarginGradientBegin;

	public override Color ImageMarginRevealedGradientBegin => ((ProfessionalColorTable)this).ImageMarginGradientBegin;

	public override Color ImageMarginRevealedGradientEnd => ((ProfessionalColorTable)this).ImageMarginGradientEnd;

	public override Color ImageMarginRevealedGradientMiddle => ((ProfessionalColorTable)this).ImageMarginGradientMiddle;

	public override Color MenuItemPressedGradientBegin => Color.FromArgb(255, 255, 255);

	public override Color MenuItemPressedGradientEnd => Color.FromArgb(192, 192, 255);

	public override Color MenuItemPressedGradientMiddle => Color.FromArgb(248, 248, 255);

	public override Color MenuItemBorder => ((ProfessionalColorTable)this).ButtonSelectedBorder;

	public override Color MenuItemSelected => ((ProfessionalColorTable)this).ButtonSelectedGradientBegin;

	public override Color MenuItemSelectedGradientBegin => ((ProfessionalColorTable)this).ButtonSelectedGradientBegin;

	public override Color MenuItemSelectedGradientEnd => ((ProfessionalColorTable)this).ButtonSelectedGradientBegin;

	public override Color ButtonSelectedGradientBegin => Color.FromArgb(220, 220, 255);

	public override Color ButtonSelectedGradientMiddle => ((ProfessionalColorTable)this).ButtonSelectedGradientBegin;

	public override Color ButtonSelectedGradientEnd => ((ProfessionalColorTable)this).ButtonSelectedGradientBegin;

	public override Color ButtonSelectedBorder => Color.FromArgb(192, 192, 255);

	public override Color ButtonPressedGradientBegin => Color.FromArgb(208, 200, 255);

	public override Color ButtonPressedGradientMiddle => ((ProfessionalColorTable)this).ButtonPressedGradientBegin;

	public override Color ButtonPressedGradientEnd => ((ProfessionalColorTable)this).ButtonPressedGradientBegin;

	public override Color ButtonPressedBorder => Color.FromArgb(154, 148, 200);

	public override Color ButtonCheckedGradientBegin => Color.FromArgb(154, 148, 200);

	public override Color ButtonCheckedGradientMiddle => ((ProfessionalColorTable)this).ButtonCheckedGradientBegin;

	public override Color ButtonCheckedGradientEnd => ((ProfessionalColorTable)this).ButtonCheckedGradientBegin;

	public override Color ButtonCheckedHighlightBorder => Color.FromArgb(154, 148, 200);

	public override Color ButtonCheckedHighlight => Color.FromArgb(192, 192, 255);

	public override Color ToolStripPanelGradientBegin => SystemColors.ControlLightLight;

	public override Color ToolStripPanelGradientEnd => Color.FromArgb(192, 192, 255);
}
