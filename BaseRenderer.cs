using System;
using System.Collections.Generic;
using System.Text;

namespace Floaters
{
    public abstract class BaseRenderer 
    {
        
        public BaseRenderer(IColorTable ct, IFontTable fnt)
        {
            table = ct;
            this.fnt = fnt;
        }


        IColorTable table;
        public IColorTable ColorTable {
            get
            {
                return table;
            }
        }

        IFontTable fnt;
        public IFontTable FontTable
        {
            get
            {
                return fnt;
            }
        }

        IRenderDockHints dock;
        public  IRenderDockHints DockRenderer {
            get
            {
                if (dock == null) CreateDockRenderer(out dock);
                return dock;
            }
        }
        protected abstract void CreateDockRenderer(out IRenderDockHints rnd);



        IDockPanelRenderer panel;
        public IDockPanelRenderer DockPanelRenderer
        {
            get
            {
                if (panel == null) CreateDockPanelRenderer(out panel);
                return panel;
            }
        }

        protected abstract void CreateDockPanelRenderer(out IDockPanelRenderer rnd);

        
    }
}
