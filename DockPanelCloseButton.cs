using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Floaters
{
    public class DockPanelCloseButton : DockPanelCaptionButton
    {
        internal DockPanelCloseButton(DockPanel dp)
            : base(dp)
        {
        }

        protected override Rectangle GetBounds(Rectangle captionrect)
        {
            return Renderer.GetCloseButtonRect(Parent, captionrect);
        }

        protected override string GetImageName()
        {
            return "closeX";
        }

        protected override void OnClick()
        {
            Parent.Close();
        }
    }
}
