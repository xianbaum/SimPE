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

namespace Ambertation.Windows.Forms
{
    public abstract class BaseDockPanelRenderer : BaseControlRenderer, System.IDisposable
    {
        #region Helper classes
        public class Dimensions
        {
            public Dimensions(int capt, int but, int bord, int dbarspace, int pad, int iconsize, int gripsz)
            {
                caption = capt;
                buttons = but;
                border = bord;
                this.dbarspace = dbarspace;
                padding = pad;
                iconsz = iconsize;
                this.gripsz = gripsz;
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

            int gripsz;
            public int GripSize
            {
                get { return gripsz; }
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

            int padding;
            public int ButtonTextPadding
            {
                get { return padding; }
            }

            int iconsz;
            public int IconSize
            {
                get { return iconsz; }
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

        #region Grip
        public virtual System.Windows.Forms.Padding GetGripSize(System.Windows.Forms.DockStyle dock)
        {
            if (dock == System.Windows.Forms.DockStyle.Right) return new System.Windows.Forms.Padding(Dimension.GripSize, 0, 0, 0);
            if (dock == System.Windows.Forms.DockStyle.Bottom) return new System.Windows.Forms.Padding(0, Dimension.GripSize, 0, 0);
            if (dock == System.Windows.Forms.DockStyle.Left) return new System.Windows.Forms.Padding(0, 0, Dimension.GripSize, 0);
            if (dock == System.Windows.Forms.DockStyle.Top) return new System.Windows.Forms.Padding(0, 0, 0, Dimension.GripSize);
            return new System.Windows.Forms.Padding(0);
        }

        public virtual Rectangle GetGripRectangle(NCPaintEventArgs e, System.Windows.Forms.DockStyle dock)
        {
            if (dock == System.Windows.Forms.DockStyle.Right) return new Rectangle(-1, -1, Dimension.GripSize + 1, e.WindowRectangle.Height + 1);
            if (dock == System.Windows.Forms.DockStyle.Bottom) return new Rectangle(-1, -1, e.WindowRectangle.Width + 1, Dimension.GripSize + 1);
            if (dock == System.Windows.Forms.DockStyle.Left) return new Rectangle(e.WindowRectangle.Width - Dimension.GripSize, -1, Dimension.GripSize + 1, e.WindowRectangle.Height + 1);
            if (dock == System.Windows.Forms.DockStyle.Top) return new Rectangle(-1, e.WindowRectangle.Height - Dimension.GripSize, e.WindowRectangle.Width + 1, Dimension.GripSize + 1);

            return Rectangle.Empty;
        }
        #endregion

        #region Border Size
        public virtual System.Windows.Forms.Padding GetPanelBorderSize(DockContainer dc, DockPanel dp, ButtonOrientation orient)
        {
            Rectangle wnd = new Rectangle(0, 0, 100, 100);
            Rectangle brect = GetButtonsRectangle(orient, new NCPaintEventArgs(null, wnd, wnd, null), dc);
            int dbord = Dimension.Border;
            int dcapt = Dimension.Caption;
            if (dp != null)
            {
                if (dp.Floating && !dp.FloatContainer) return new System.Windows.Forms.Padding(0);
                if (dp.FloatContainer)
                {
                    dbord = 0;
                    dcapt = 0;
                }
            }

            //Console.WriteLine(dc.Name + " " + brect+" "+dc.OneChild);
            if (orient == ButtonOrientation.Bottom)
                return new System.Windows.Forms.Padding(dbord, dcapt + dbord, dbord, dbord + brect.Height);

            if (orient == ButtonOrientation.Right)
                return new System.Windows.Forms.Padding(dbord, dcapt + dbord, dbord + brect.Width, dbord);

            if (orient == ButtonOrientation.Top)
                return new System.Windows.Forms.Padding(dbord, dcapt + dbord + brect.Height, dbord, dbord);

            return new System.Windows.Forms.Padding(dbord + brect.Width, dcapt + dbord, dbord, dbord);
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
            if (orient == ButtonOrientation.Bottom || orient == ButtonOrientation.Top) sf = new StringFormat(StringFormatFlags.NoWrap | StringFormatFlags.NoClip );
            else sf = new StringFormat(StringFormatFlags.DirectionVertical | StringFormatFlags.NoWrap | StringFormatFlags.NoClip);

            SizeF res = g.MeasureString(caption, font, int.MaxValue, sf);

            //Console.WriteLine("Getting Caption Size " + caption + "(" + res + "=" + (int)res.Width + "x" + (int)res.Height + ", "+orient+")");
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
            //Console.WriteLine("Fitting Text " + caption + "(" + sz + ") to " + msz);
            bool elips = false;
            while (sz > msz && caption.Length>0)
            {
                if (caption.Length <= 1) caption = "";
                else caption = caption.Substring(0, caption.Length - 1);
                elips = true;
                sz = GetButtonCaptionWidth(font, g, caption + "...", orient, msz);
                //Console.WriteLine("Fitting Text " + caption + "...(" + sz + ") to " + msz);
            }

            if (caption.Length > 0 && elips) caption += "...";
            return caption;
        }
        #endregion

        public virtual Rectangle GetPanelClientRectangle(DockPanel dp, ButtonOrientation orient)
        {            
            System.Windows.Forms.Padding pad = GetPanelBorderSize(dp.DockContainer, dp, orient);
            return new Rectangle(pad.Left, pad.Top, dp.Width - pad.Horizontal, dp.Height - pad.Vertical);
        }

        public virtual Rectangle GetPanelClientRectangle(DockContainer dc, NCPaintEventArgs e, ButtonOrientation orient)
        {
            System.Windows.Forms.Padding pad = GetPanelBorderSize(dc, null, orient);
            return new Rectangle(pad.Left, pad.Top, e.WindowRectangle.Width - pad.Horizontal, e.WindowRectangle.Height - pad.Vertical);
        }

        #region Caption Rectangle
        public System.Drawing.Rectangle GetCaptionRect(DockPanel dp)
        {
            return GetCaptionRect(dp, dp.BestOrientation);
        }

        public virtual System.Drawing.Rectangle GetCaptionRect(DockPanel dp, ButtonOrientation orient)
        {
            NCPaintEventArgs e = new NCPaintEventArgs(null, dp.ClientRectangle, dp.Bounds, null);
            Rectangle buts = GetButtonsRectangle(orient, e, dp.DockContainer);
            Rectangle client = GetPanelClientRectangle(dp, orient);


            return new Rectangle(client.Left, client.Top - Dimension.Caption , client.Width, Dimension.Caption);
            
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
            Rectangle colbr = GetCollapseButtonRect(dp, caprect);
            Rectangle clobr = GetCloseButtonRect(dp, caprect);
            int mleft = 0;
            if (colbr != Rectangle.Empty) mleft = caprect.Width - colbr.Left + 4;
            if (clobr != Rectangle.Empty) mleft = Math.Max(mleft, caprect.Width - clobr.Left + 4);
            wd = Math.Max(1, wd-mleft);
            return new Rectangle(caprect.Left, caprect.Top, wd, caprect.Height);
        }
        #endregion

        #region Caption Buttons
        protected virtual void DrawButtonImage(Graphics g, string name, Rectangle r)
        {
            name = "Ambertation.Windows.Forms." + name + ".png";
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
        protected void SetupButtonColors(System.Drawing.Rectangle r, Color c, Color fontc, ButtonOrientation orient, ButtonState state, out SolidBrush fontbrush, out SolidBrush linebackgroundbrush, out System.Drawing.Drawing2D.LinearGradientBrush backgroundbrush, out Pen borderpen)
        {
            fontbrush = new SolidBrush(fontc);
            Color c1 = ColorTable.DockButtonBackgroundTop;
            Color c2 = ColorTable.DockButtonBackgroundBottom;
            if (state == ButtonState.Highlight)
            {
                c1 = ColorTable.DockButtonHighlightBackgroundTop;
                c2 = ColorTable.DockButtonHighlightBackgroundBottom;
            }

            linebackgroundbrush = new SolidBrush(ColorTable.DockButtonHighlightBackgroundTop);
            if (orient == ButtonOrientation.Top || orient == ButtonOrientation.Left)
            {
                Color dum = c1; c1 = c2; c2 = dum;
            }

            System.Drawing.Drawing2D.LinearGradientMode mode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            if (orient == ButtonOrientation.Left || orient == ButtonOrientation.Right)
                mode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;



            backgroundbrush = new System.Drawing.Drawing2D.LinearGradientBrush(
                    r,
                    c1,
                    c2,
                    mode
                );
            borderpen = new Pen(c);
        }

        protected System.Drawing.Rectangle SetupButtonRectangles(System.Drawing.Rectangle r, Font f, ButtonOrientation orient, out Rectangle linerectangle, out Point linept1, out Point linept2, out Rectangle textrect, out Rectangle imgrect)
        {
            if (orient == ButtonOrientation.Bottom)
            {
                linerectangle = new Rectangle(r.Left, r.Top - 1, r.Width, 3);
                linept1 = new Point(linerectangle.Left, linerectangle.Bottom);
                linept2 = new Point(linerectangle.Right, linerectangle.Bottom);


                imgrect = new Rectangle(
                    r.Left + Dimension.ButtonTextPadding,
                    linerectangle.Bottom + 1,
                    Dimension.IconSize,
                    Dimension.IconSize);

                textrect = new Rectangle(
                    imgrect.Right + 1,
                    imgrect.Top + (imgrect.Height - f.Height) / 2 + 1,
                    r.Width - imgrect.Width - 3 * Dimension.ButtonTextPadding,
                    f.Height);

                r = new Rectangle(r.Left, r.Top, r.Width, r.Height - 2);
            }
            else if (orient == ButtonOrientation.Top)
            {
                linerectangle = new Rectangle(r.Left, r.Bottom - 4, r.Width, 4);
                linept1 = new Point(linerectangle.Left, linerectangle.Top);
                linept2 = new Point(linerectangle.Right, linerectangle.Top);

                imgrect = new Rectangle(
                    r.Left + Dimension.ButtonTextPadding,
                    r.Top + 1,
                    Dimension.IconSize,
                    Dimension.IconSize);

                textrect = new Rectangle(
                    imgrect.Right + 1,
                    imgrect.Top + (imgrect.Height - f.Height) / 2 + 1,
                    r.Width - imgrect.Width - 3 * Dimension.ButtonTextPadding,
                    f.Height);

                r = new Rectangle(r.Left, r.Top + 1, r.Width, r.Height - 2);
            }
            else if (orient == ButtonOrientation.Right)
            {
                linerectangle = new Rectangle(r.Left - 1, r.Top, 4, r.Height);
                linept1 = new Point(linerectangle.Right, linerectangle.Top);
                linept2 = new Point(linerectangle.Right, linerectangle.Bottom);

                imgrect = new Rectangle(
                    linerectangle.Right + (r.Width - linerectangle.Width - Dimension.IconSize) / 2,
                    r.Top + Dimension.ButtonTextPadding,
                    Dimension.IconSize,
                    Dimension.IconSize);

                textrect = new Rectangle(
                    imgrect.Left,
                    imgrect.Bottom + Dimension.ButtonTextPadding,
                    f.Height,
                    r.Height - imgrect.Height - 3 * Dimension.ButtonTextPadding);

                r = new Rectangle(r.Left, r.Top, r.Width - 2, r.Height);
            }
            else
            {
                //Console.WriteLine(r);
                linerectangle = new Rectangle(r.Right - 3, r.Top, 4, r.Height);
                linept1 = new Point(linerectangle.Left, linerectangle.Top);
                linept2 = new Point(linerectangle.Left, linerectangle.Bottom);

                imgrect = new Rectangle(
                    (r.Width - linerectangle.Width - Dimension.IconSize) / 2 + 1,
                    r.Top + Dimension.ButtonTextPadding,
                    Dimension.IconSize,
                    Dimension.IconSize);

                textrect = new Rectangle(
                    imgrect.Left,
                    imgrect.Bottom + Dimension.ButtonTextPadding,
                    f.Height,
                    r.Height - imgrect.Height - 3 * Dimension.ButtonTextPadding);

                r = new Rectangle(r.Left + 1, r.Top, r.Width - 2, r.Height);
            }
            return r;
        }

        public void RenderButton(System.Drawing.Graphics g, System.Drawing.Rectangle r, string caption, Image img, ButtonOrientation orient, ButtonState state, bool renderbackgroundbar)
        {
            if (r.Height == 0 || r.Width == 0) return;

            Color c = ColorTable.DockButtonBorderColor;
            Color fc = ColorTable.DockButtonTextColor;
            Font f = Parent.FontTable.ButtonFont;
            if (state == ButtonState.Highlight)
            {
                c = ColorTable.DockButtonHighlightBorderColor;
                fc = ColorTable.DockButtonHighlightTextColor;
                f = Parent.FontTable.ButtonHighlightFont;
            }
            StringFormat sf;
            if (orient == ButtonOrientation.Bottom || orient == ButtonOrientation.Top) sf = new StringFormat();
            else sf = new StringFormat(StringFormatFlags.DirectionVertical);
            caption = GetFittingString(f, caption, orient, new Size(r.Width, r.Height));


            RenderButton(g, r, caption, img, c, fc, f, sf, orient, state, renderbackgroundbar);
        }
        protected abstract void RenderButton(System.Drawing.Graphics g, System.Drawing.Rectangle r, string caption, Image img, Color c, Color fontc, Font f, StringFormat sf, ButtonOrientation orient, ButtonState state, bool renderbackgroundbar);


        public void RenderCaption(DockPanel dp, NCPaintEventArgs e)
        {
            Rectangle caprect = GetCaptionRect(dp);
            Rectangle txtrect = GetCaptionTextRect(dp, caprect);

            string caption = GetFittingString(Parent.FontTable.CaptionFont, dp.CaptionText, ButtonOrientation.Top, new Size(txtrect.Width, txtrect.Height));

            if (caprect.Width>0 && caprect.Height>0) 
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

        public void RenderButtonBackground(DockPanel dp, NCPaintEventArgs e)
        {
            if (dp.DockContainer == null) return;
            Rectangle pad = GetButtonsRectangle(dp.BestOrientation, e, dp.DockContainer);

            Rectangle r; 
            Point pt1, pt2;
            ButtonOrientation orient = dp.BestOrientation;
            Size sz = GetButtonSize(dp, orient);

            if (orient == ButtonOrientation.Top)
            {
                r = new Rectangle(pad.Left, pad.Top + sz.Height - 4, e.WindowRectangle.Width, 3);
                pt1 = new Point(r.Left, r.Top);
                pt2 = new Point(r.Right, r.Top);
            }
            else if (orient == ButtonOrientation.Bottom)
            {
                r = new Rectangle(pad.Left, e.WindowRectangle.Height-sz.Height-1, e.WindowRectangle.Width, 3);
                pt1 = new Point(r.Left, r.Bottom);
                pt2 = new Point(r.Right, r.Bottom);
            }
            else if (orient == ButtonOrientation.Right)
            {
                r = new Rectangle(pad.Left, pad.Top , 3, pad.Height);
                pt1 = new Point(r.Right, r.Top);
                pt2 = new Point(r.Right, r.Bottom);
            }
            else 
            {
                r = new Rectangle(pad.Right-3,  pad.Top, 3, pad.Height);
                pt1 = new Point(r.Left, r.Top);
                pt2 = new Point(r.Left, r.Bottom);
            }

            SolidBrush brush = new SolidBrush(ColorTable.DockButtonHighlightBackgroundTop);
            Pen pen = new Pen(ColorTable.DockButtonHighlightBorderColor);


            RenderButtonBackground(e, r, pt1, pt2, brush, pen);
        }
        protected abstract void RenderButtonBackground(NCPaintEventArgs e, Rectangle r, Point pt1, Point pt2, SolidBrush brush, Pen pen);
        
        #endregion

        #region ButtonSize
        public Rectangle GetButtonsRectangle(ButtonOrientation orient, NCPaintEventArgs e, DockContainer dc)
        {
            bool one = false;
            if (dc != null) one = dc.OneChild && dc.HideSingleButton;

            int butsub = Dimension.Buttons;
            if (one) butsub = 0;

            if (orient == ButtonOrientation.Bottom)
                return new Rectangle(0, e.WindowRectangle.Height - butsub, e.WindowRectangle.Width, butsub);
            else if (orient == ButtonOrientation.Top)
                return new Rectangle(0, 0, e.WindowRectangle.Width, butsub);
            else if (orient == ButtonOrientation.Left)
                return new Rectangle(0, 0, butsub, e.WindowRectangle.Height);
            else //if (orient == ButtonOrientation.Right)
                return new Rectangle(e.WindowRectangle.Width - butsub, 0, butsub, e.WindowRectangle.Height);
        }

        public Size GetButtonSize(DockPanel dp)
        {
            return GetButtonSize(dp, dp.BestOrientation);
        }

        public virtual Size GetButtonSize(DockPanel dp, ButtonOrientation orient)
        {
            if (orient == ButtonOrientation.Top || orient==ButtonOrientation.Bottom)
                return new Size(GetButtonCaptionWidth(GetFont(dp), dp.ButtonText, orient) + Dimension.IconSize + 5*Dimension.ButtonTextPadding, Dimension.Buttons);
            else
                return new Size(Dimension.Buttons, GetButtonCaptionWidth(GetFont(dp), dp.ButtonText, orient) + Dimension.IconSize + 5 * Dimension.ButtonTextPadding);
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
