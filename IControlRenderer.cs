using System;
using System.Collections.Generic;
using System.Text;

namespace Floaters
{
    public interface IControlRenderer
    {
        BaseRenderer Parent { get;}
        IColorTable ColorTable { get; }
    }
}
