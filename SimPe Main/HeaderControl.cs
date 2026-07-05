using System.Windows.Forms;

namespace SimPe.Windows.Forms
{
    /// <summary>
    /// Style for <see cref="HeaderControl" />.
    /// </summary>
    public enum HeaderStyle
    {
        Standard,
        Heading,
        SubHeading
    }

    /// <summary>
    /// Replacement for legacy proprietary control.
    /// </summary>
    public class HeaderControl : GroupBox
    {
        public HeaderControl()
        {
            SetStyle(ControlStyles.Selectable, true);
            TabStop = true;
        }

        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public HeaderStyle HeaderStyle { get; set; } = HeaderStyle.SubHeading;
    }
}
