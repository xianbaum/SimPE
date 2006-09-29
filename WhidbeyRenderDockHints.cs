using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Floaters
{    
    public class WhidbeyRenderDockHints : BaseControlRenderer, IRenderDockHints
    {        
        Size sz;
        static Bitmap hintcenter, hintleft, hinttop, hintright, hintbottom;
        Rectangle rl, rt, rr, rb, rc;
        public WhidbeyRenderDockHints(BaseRenderer parent)
            : base(parent)
        {
            sz = new Size(88, 88);

            rl = new Rectangle(0, 29, 31, 28);
            rt = new Rectangle(29, 0, 28, 31);
            rr = new Rectangle(56, 29, 31, 28);
            rb = new Rectangle(29, 56, 28, 31);
            rc = new Rectangle(29, 29, 30, 30);
        }

        #region IRenderDockHints Member

        public System.Drawing.Size HintSize
        {
            get { return sz; }
        }

        protected virtual System.Drawing.Image AllHintsImage
        {
            get
            {
                if (hintcenter == null)
                    hintcenter = Image.FromStream(typeof(DockHint).Assembly.GetManifestResourceStream("Floaters.dockhint.PNG")) as Bitmap;

                return hintcenter;
            }
        }

        protected virtual System.Drawing.Image LeftHintImage
        {
            get
            {
                if (hintleft == null)
                    hintleft = Image.FromStream(typeof(DockHint).Assembly.GetManifestResourceStream("Floaters.left.png")) as Bitmap;
                return hintleft;
            }
        }

        protected virtual System.Drawing.Image TopHintImage
        {
            get
            {
                if (hinttop == null)
                    hinttop = Image.FromStream(typeof(DockHint).Assembly.GetManifestResourceStream("Floaters.top.png")) as Bitmap;
                return hinttop;
            }
        }

        protected virtual System.Drawing.Image RightHintImage
        {
            get
            {
                if (hintright == null)
                    hintright = Image.FromStream(typeof(DockHint).Assembly.GetManifestResourceStream("Floaters.right.png")) as Bitmap;
                return hintright;
            }
        }

        protected virtual System.Drawing.Image BottomHintImage
        {
            get
            {
                if (hintbottom == null)
                    hintbottom = Image.FromStream(typeof(DockHint).Assembly.GetManifestResourceStream("Floaters.bottom.png")) as Bitmap;

                return hintbottom;
            }
        }

        public virtual System.Drawing.Rectangle LeftRectangle
        {
            get { return rl; }
        }

        public virtual System.Drawing.Rectangle TopRectangle
        {
            get { return rt; }
        }

        public virtual System.Drawing.Rectangle RightRectangle
        {
            get { return rr; }
        }

        public virtual System.Drawing.Rectangle BottomRectangle
        {
            get { return rb; }
        }

        public virtual System.Drawing.Rectangle CenterRectangle
        {
            get { return rc; }
        }


        System.Drawing.Drawing2D.GraphicsPath pl, pt, pr, pb, pc;
        public virtual void InitHints(bool l, bool t, bool r, bool b, bool c)
        {
            pl = new System.Drawing.Drawing2D.GraphicsPath();
            pt = new System.Drawing.Drawing2D.GraphicsPath();
            pr = new System.Drawing.Drawing2D.GraphicsPath();
            pb = new System.Drawing.Drawing2D.GraphicsPath();
            pc = new System.Drawing.Drawing2D.GraphicsPath();

            if (l && !t && !r && !b && !c) InitJustLeft();
            else if (!l && t && !r && !b && !c) InitJustTop();
            else if (!l && !t && r && !b && !c) InitJustRight();
            else if (!l && !t && !r && b && !c) InitJustBottom();
            else InitCenter();
        }

        protected virtual void InitCenter()
        {
            pl.AddLine(rl.Left + 21, rl.Top, rl.Left, rl.Top);
            pl.AddLine(rl.Left, rl.Top, rl.Left, rl.Bottom);
            pl.AddLine(rl.Left, rl.Bottom, rl.Left + 21, rl.Bottom);

            pt.AddLine(rt.Left, rt.Top + 21, rt.Left, rt.Top);
            pt.AddLine(rt.Left, rt.Top, rt.Right, rt.Top);
            pt.AddLine(rt.Right, rt.Top, rt.Right, rt.Top + 21);

            pr.AddLine(rr.Right - 22, rr.Top, rr.Right, rr.Top);
            pr.AddLine(rr.Right, rr.Top, rr.Right, rr.Bottom);
            pr.AddLine(rr.Right, rr.Bottom, rr.Right - 22, rr.Bottom);

            pb.AddLine(rb.Left, rb.Bottom - 22, rb.Left, rb.Bottom);
            pb.AddLine(rb.Left, rb.Bottom, rb.Right, rb.Bottom);
            pb.AddLine(rb.Right, rb.Bottom, rb.Right, rb.Bottom - 22);

            pc.AddLine(rt.Left, rt.Top + 21, rl.Left + 21, rl.Top);
            pc.StartFigure();
            pc.AddLine(rl.Left + 21, rl.Bottom, rb.Left, rb.Bottom - 22);
            pc.StartFigure();
            pc.AddLine(rb.Right, rb.Bottom - 22, rr.Right - 22, rr.Bottom);
            pc.StartFigure();
            pc.AddLine(rr.Right - 22, rr.Top, rt.Right, rt.Top + 21);
        }

        protected virtual void InitJustBottom()
        {
            pb.AddRectangle(rb);
        }

        protected virtual void InitJustRight()
        {
            pr.AddRectangle(rr);
        }

        protected virtual void InitJustTop()
        {
            pt.AddRectangle(rt);
        }

        protected virtual void InitJustLeft()
        {
            pl.AddRectangle(rl);
        }

        public virtual void RenderHint(Graphics g, bool l, bool t, bool r, bool b, bool c, SelectedHint sel)
        {
            InitHints(l, t, r, b, c);
            DrawUnselectedHint(g, l, t, r, b, c);
            //Console.WriteLine(sel);
            DrawSelection(g, sel);
        }

        protected virtual void DrawUnselectedHint(Graphics g,  bool l, bool t, bool r, bool b, bool c)
        {           
            if (l && !t && !r && !b && !c) g.DrawImage(this.LeftHintImage, 0, 0, sz.Width, sz.Height);
            else if (!l && t && !r && !b && !c) g.DrawImage(this.TopHintImage, 0, 0, sz.Width, sz.Height);
            else if (!l && !t && r && !b && !c) g.DrawImage(this.RightHintImage, 0, 0, sz.Width, sz.Height);
            else if (!l && !t && !r && b && !c) g.DrawImage(this.BottomHintImage, 0, 0, sz.Width, sz.Height);
            else g.DrawImage(this.AllHintsImage, 0, 0, sz.Width, sz.Height);            
        }

        protected virtual void DrawSelection(Graphics g, SelectedHint sel)
        {
            Pen p = new Pen(ColorTable.DockHintHightlightColor, 1);

            if (sel == SelectedHint.Center)
            {
                g.DrawPath(p, pc);
            }
            else if (sel == SelectedHint.Left)
            {
                g.DrawPath(p, pl);
            }
            else if (sel == SelectedHint.Top)
            {
                g.DrawPath(p, pt);
                //Console.WriteLine(pt.GetBounds());
            }
            else if (sel == SelectedHint.Right)
            {
                g.DrawPath(p, pr);
            }
            else if (sel == SelectedHint.Bottom)
            {
                g.DrawPath(p, pb);
            }
        }

        #endregion
    }
}