using System;
using System.Collections.Generic;
using System.Text;

namespace Floaters
{
    class DockOverlay : ManagedLayeredForm
    {
        public DockOverlay(DockManager manager)
            : base(manager, manager.Renderer.ColorTable.DockHintOverlayColor, new System.Drawing.Size(1, 1))
        {
        }
    }
}
