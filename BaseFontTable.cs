using System;
using System.Collections.Generic;
using System.Text;

namespace Floaters
{
    public class BaseFontTable : IFontTable
    {
        System.Drawing.Font fnt;
        public BaseFontTable()
        {
            fnt = new System.Drawing.Font("Tahoma", 8);
        }

        protected System.Drawing.Font DefaultFont
        {
            get { return fnt; }
        }

        #region IFontTable Member

        public virtual System.Drawing.Font ButtonFont
        {
            get { return fnt; }
        }

        public virtual System.Drawing.Font ButtonHighlightFont
        {
            get { return fnt; }
        }

        public virtual System.Drawing.Font CaptionFont
        {
            get { return fnt; }
        }

        #endregion
    }
}
