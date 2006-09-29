using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Floaters
{
    public class DockPanelCollapseButton : DockPanelCaptionButton
    {
        internal DockPanelCollapseButton(DockPanel dp)
            : base(dp)
        {
        }

        protected override Rectangle GetBounds(Rectangle captionrect)
        {
            return Renderer.GetCollapseButtonRect(Parent, captionrect);
        }

        protected override string GetImageName()
        {
            return "pinV";
        }


        protected override void OnClick()
        {
            Parent.Collapse();
        }
    }
}
