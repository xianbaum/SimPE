/***************************************************************************
*   Copyright (C) 2005 by Ambertation                                     *
*   quaxi@ambertation.de                                                  *
*                                                                         *
*   This program is free software; you can redistribute it and/or modify  *
*   it under the terms of the GNU General Public License as published by  *
*   the Free Software Foundation; either version 2 of the License, or     *
*   (at your option) any later version.                                   *
*                                                                         *
*   This program is distributed in the hope that it will be useful,       *
*   but WITHOUT ANY WARRANTY; without even the implied warranty of        *
*   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the         *
*   GNU General Public License for more details.                          *
*                                                                         *
*   You should have received a copy of the GNU General Public License     *
*   along with this program; if not, write to the                         *
*   Free Software Foundation, Inc.,                                       *
*   59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.             *
***************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Floaters
{
    public abstract class BaseDockPanelRenderer : BaseControlRenderer, System.IDisposable
    {
        #region Helper classes
        public class Dimensions
        {
            public Dimensions(int capt, int but, int bord, int dbarspace)
            {
                caption = capt;
                buttons = but;
                border = bord;
                this.dbarspace = dbarspace;
            }

            int caption;
            public int Caption
            {
                get { return caption; }
            }

            int buttons;
            public int Buttons
            {
                get { return buttons; }
            }

            int border;
            public int Border
            {
                get { return border; }
            }

            int dbarspace;
            public int DockBarButtonSpacing
            {
                get { return dbarspace; }
            }
        }

        
        #endregion

        Graphics dg = Graphics.FromImage(new Bitmap(1, 1));
        public BaseDockPanelRenderer(BaseRenderer parent)
            : base(parent)
        {
        }

        public abstract Dimensions Dimension
        {
            get;
        }

        public virtual DockPanelButtonManager ConstructButtonData(IButtonContainer cnt, NCPaintEventArgs e)
        {
            return new DockPanelButtonManager(this.Parent, e, cnt);
        }

        #region Border Size
        public virtual System.Windows.Forms.Padding GetPanelBorderSize(ButtonOrientation orient)
        {
            if (orient == ButtonOrientation.Bottom)
                return new System.Windows.Forms.Padding(Dimension.Border, Dimension.Caption + Dimension.Border, Dimension.Border, Dimension.Buttons + Dimension.Border);

            if (orient == ButtonOrientation.Right)
                return new System.Windows.Forms.Padding(Dimension.Border, Dimension.Caption + Dimension.Border, Dimension.Buttons + Dimension.Border, Dimension.Border);

            if (orient == ButtonOrientation.Top)
                return new System.Windows.Forms.Padding(Dimension.Border, Dimension.Caption + Dimension.Border + Dimension.Buttons, Dimension.Border, Dimension.Border);

            return new System.Windows.Forms.Padding(Dimension.Border + Dimension.Buttons, Dimension.Caption + Dimension.Border, Dimension.Border, Dimension.Border);
        }

        public virtual System.Windows.Forms.Padding GetBarBorderSize(ButtonOrientation orient)
        {
           return new System.Windows.Forms.Padding(0,0,0,0);
        }

        public System.Windows.Forms.Padding GetBorderSize(IButtonContainer c)
        {
            return c.GetBorderSize(c.BestOrientation);
        }
        #endregion

        
        #region ButtonCaptionWidth
        protected int GetButtonCaptionWidth(System.Drawing.Font font, string caption)
        {
            return GetButtonCaptionWidth(font, dg, caption, ButtonOrientation.Bottom,  int.MaxValue);
        }

        protected int GetButtonCaptionWidth(System.Drawing.Font font, string caption, ButtonOrientation orient)
        {
            return GetButtonCaptionWidth(font, dg, caption, orient, int.MaxValue);
        }

        protected int GetButtonCaptionWidth(System.Drawing.Font font, string caption, ButtonOrientation orient, int maxwd)
        {
            return GetButtonCaptionWidth(font, dg, caption, orient, maxwd);
        }

        protected virtual int GetButtonCaptionWidth(System.Drawing.Font font, System.Drawing.Graphics g, string caption, ButtonOrientation orient, int maxwd)
        {
            StringFormat sf;
            if (orient == ButtonOrientation.Bottom || orient == ButtonOrientation.Top) sf = new StringFormat();
            else sf = new StringFormat(StringFormatFlags.DirectionVertical);

            SizeF res = g.MeasureString(caption, font, maxwd, sf);

            ////Console.WriteLine("Getting Caption Size " + caption + "(" + res + "=" + (int)res.Width + "x" + (int)res.Height + ", "+orient+")");
            if (orient == ButtonOrientation.Bottom || orient == ButtonOrientation.Top) return (int)res.Width;
            else return (int)res.Height;
        }
        #endregion

        #region Fit String
        public  string GetFittingString(System.Drawing.Font font, string caption, ButtonOrientation orient, Size maxsz)
        {
            return GetFittingString(font, dg, caption, orient, maxsz);
        }

        protected virtual string GetFittingString(System.Drawing.Font font, System.Drawing.Graphics g, string caption, ButtonOrientation orient, Size maxsz)
        {
            int msz = maxsz.Width;
            if (orient == ButtonOrientation.Left || orient == ButtonOrientation.Right) msz = maxsz.Height;
            int sz = GetButtonCaptionWidth(font, g, caption, orient, msz);
            ////Console.WriteLine("Fitting Text " + caption + "(" + sz + ") to " + msz);
            bool elips = false;
            while (sz > msz && caption.Length>0)
            {
                if (caption.Length <= 1) caption = "";
                else caption = caption.Substring(0, caption.Length - 1);
                elips = true;
                sz = GetButtonCaptionWidth(font, g, caption + "...", orient, msz);
                ////Console.WriteLine("Fitting Text " + caption + "...(" + sz + ") to " + msz);
            }

            if (caption.Length > 0 && elips) caption += "...";
            return caption;
        }
        #endregion

        #region Caption Rectangle
        public System.Drawing.Rectangle GetCaptionRect(DockPanel dp)
        {
            return GetCaptionRect(dp, dp.BestOrientation);
        }

        public virtual System.Drawing.Rectangle GetCaptionRect(DockPanel dp, ButtonOrientation orient)
        {
            return new Rectangle(Dimension.Border, Dimension.Border, dp.Width - 2 * Dimension.Border, Dimension.Caption);
        }
        
        public virtual System.Drawing.Rectangle GetCloseButtonRect(DockPanel dp, Rectangle caprect){
            if (!dp.ShowCloseButton) return Rectangle.Empty;
            int left = caprect.Right - Dimension.Caption - Dimension.Border;
            return new Rectangle(left, caprect.Top , Dimension.Caption - 2, Dimension.Caption - 2);
        }

        public virtual System.Drawing.Rectangle GetCollapseButtonRect(DockPanel dp, Rectangle caprect)
        {
            if (!dp.ShowCollapseButton) return Rectangle.Empty;
            int left = caprect.Right - Dimension.Caption - Dimension.Border;
            if (dp.ShowCloseButton) left -= (Dimension.Caption + 1);
            return new Rectangle(left, caprect.Top , Dimension.Caption - 2, Dimension.Caption - 2);
        }

        public virtual System.Drawing.Rectangle GetCaptionTextRect(DockPanel dp, Rectangle caprect)
        {
            int wd = caprect.Width;
            return new Rectangle(caprect.Left, caprect.Top, wd, caprect.Height);
        }
        #endregion

        #region Caption Buttons
        protected virtual void DrawButtonImage(Graphics g, string name, Rectangle r)
        {
            name = "Floaters."+name+".png";
            System.IO.Stream s = this.GetType().Assembly.GetManifestResourceStream(name);
            if (s != null)
            {
                Image i = Image.FromStream(s);
                int left = (r.Width - i.Width) / 2 + r.Left;
                int top = (r.Height - i.Height) / 2 + r.Top;

                g.DrawImage(i, left+1, top+1);
            }
        }

        protected virtual CaptionState GetCaptionState(DockPanel dp)
        {
            return dp.CaptionState;
        }
        #endregion

        #region Render base instructions
        public void RenderButton(System.Drawing.Graphics g, System.Drawing.Rectangle r, string caption, ButtonOrientation orient, ButtonState state)
        {
            Color c = ColorTable.DockButtonBorderColor;
            Font f = Parent.FontTable.ButtonFont;
            if (state == ButtonState.Highlight)
            {
                c = ColorTable.DockButtonHighlightBorderColor;
                f = Parent.FontTable.ButtonHighlightFont;
            }
            StringFormat sf;
            if (orient == ButtonOrientation.Bottom || orient == ButtonOrientation.Top) sf = new StringFormat();
            else sf = new StringFormat(StringFormatFlags.DirectionVertical);
            caption = GetFittingString(f, caption, orient, new Size(r.Width, r.Height));

            RenderButton(g, r, caption, c, f, sf);
        }
        protected abstract void RenderButton(System.Drawing.Graphics g, System.Drawing.Rectangle r, string caption, Color c, Font f, StringFormat sf);


        public void RenderCaption(DockPanel dp, NCPaintEventArgs e)
        {
            Rectangle caprect = GetCaptionRect(dp);
            Rectangle txtrect = GetCaptionTextRect(dp, caprect);

            string caption = GetFittingString(Parent.FontTable.CaptionFont, dp.Text, ButtonOrientation.Top, new Size(txtrect.Width, txtrect.Height));

            RenderCaptionBackground(dp.CaptionState, e, caprect);


            RenderCaptionText(dp.CaptionState, e, txtrect, caption);
        }

        public void RenderCaptionButton(DockPanel dp, DockPanelCaptionButton but, NCPaintEventArgs e)
        {
            if (but.Visible)
                RenderCaptionButton(dp, but, but.ImageName, e);
        }

        protected abstract void RenderCaptionButton(DockPanel dp, DockPanelCaptionButton but, string iname, NCPaintEventArgs e);        
        protected abstract void RenderCaptionText(CaptionState state, NCPaintEventArgs e, Rectangle txtrect, string caption);
        protected abstract void RenderCaptionBackground(CaptionState state, NCPaintEventArgs e, Rectangle caprect);
        #endregion

        #region ButtonSize
        public Size GetButtonSize(DockPanel dp)
        {
            return GetButtonSize(dp, dp.BestOrientation);
        }

        public virtual Size GetButtonSize(DockPanel dp, ButtonOrientation orient)
        {
            if (orient == ButtonOrientation.Top || orient==ButtonOrientation.Bottom)
                return new Size(GetButtonCaptionWidth(GetFont(dp), dp.Text, orient), Dimension.Buttons);
            else
                return new Size(Dimension.Buttons, GetButtonCaptionWidth(GetFont(dp), dp.Text, orient));
        }
        #endregion

        public System.Drawing.Font GetFont(DockPanel dp)
        {
            if (dp==null) return Parent.FontTable.ButtonFont;
            if (dp.DockContainer == null) return Parent.FontTable.ButtonFont;
            if (dp.DockContainer.Highlight == dp) return Parent.FontTable.ButtonHighlightFont;
            return  Parent.FontTable.ButtonFont;
        }

        #region IDisposable Member

        public void Dispose()
        {
            dg.Dispose();
        }

        #endregion
    }
}
