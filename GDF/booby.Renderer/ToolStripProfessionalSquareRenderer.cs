using System.Windows.Forms;

namespace booby.Renderer;

public class ToolStripProfessionalSquareRenderer : AdvancedToolStripProfessionalRenderer
{
	public ToolStripProfessionalSquareRenderer(ProfessionalColorTable ct)
		: base(ct)
	{
		((ToolStripProfessionalRenderer)this).RoundedEdges = false;
	}

	public ToolStripProfessionalSquareRenderer()
	{
		((ToolStripProfessionalRenderer)this).RoundedEdges = false;
	}
}
