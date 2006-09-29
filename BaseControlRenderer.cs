using System;
using System.Collections.Generic;
using System.Text;

namespace Floaters
{
    
    public abstract class BaseControlRenderer
    {
        public BaseControlRenderer(BaseRenderer parent)
        {
            this.parent = parent;
        }

        BaseRenderer parent;
        public BaseRenderer Parent
        {
            get
            {
                return parent;
            }
        }

        public IColorTable ColorTable
        {
            get
            {
                if (parent == null) return null;
                return parent.ColorTable;
            }
        }
    }
}
