using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using Ambertation.Windows.Forms;

namespace Ambertation.Windows.Forms
{
    public partial class TransparentForm : Form //Ambertation.Windows.Forms.LayeredForm
    {                
        Rectangle headrect;

        public TransparentForm()
            : base()//Color.Transparent, new Size(781, 475))
        {            
            this.TopMost = true;
            this.ShowInTaskbar = false;
            headrect = Rectangle.Empty;

            InitializeComponent();
            
            if (!DesignMode) CreateHelperForm();
            
        }
             
        Form tf;
        void CreateHelperForm()
        {

            if (tf != null) return;
            if (DesignMode) return;

            tf = new Form();
            tf.SetBounds(Left, Top, Width, Height);
            tf.FormBorderStyle = FormBorderStyle.None;
            tf.TransparencyKey = tf.BackColor;
            foreach (Control c in Controls)
                tf.Controls.Add(c);

            tf.ShowInTaskbar = false;
            tf.TopMost = true;
            tf.FormClosed += new FormClosedEventHandler(tf_FormClosed);
            if (Visible)
            {
                tf.Show(this);
                tf.SetBounds(Left, Top, Width, Height);
                tf.Activate();
            }
        }

        void tf_FormClosed(object sender, FormClosedEventArgs e)
        {
            Close();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            if (tf != null)
            {
                tf.FormClosed -= new FormClosedEventHandler(tf_FormClosed);
                tf.Close();
            }
        }

        

        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);
            if (tf != null && !DesignMode) tf.Controls.Add(e.Control);
        }

        protected virtual Rectangle HeaderRect
        {
            get { return headrect; }
        }

        
        
        

        // Sets the current bitmap
        protected void SelectBitmap()
        {
            if (bitmap == null) CreateBitmap();
            if (DesignMode) return;
            // Does this bitmap contain an alpha channel?
            if (bitmap.PixelFormat != PixelFormat.Format32bppArgb)
            {
                throw new ApplicationException("The bitmap must be 32bpp with alpha-channel.");
            }

            // Get device contexts
            IntPtr screenDc = APIHelp.GetDC(IntPtr.Zero);
            IntPtr memDc = APIHelp.CreateCompatibleDC(screenDc);
            IntPtr hBitmap = IntPtr.Zero;
            IntPtr hOldBitmap = IntPtr.Zero;

            try
            {
                // Get handle to the new bitmap and select it into the current device context
                hBitmap = bitmap.GetHbitmap(Color.FromArgb(0));
                hOldBitmap = APIHelp.SelectObject(memDc, hBitmap);

                // Set parameters for layered window update
                APIHelp.Size newSize = new APIHelp.Size(bitmap.Width, bitmap.Height);	// Size window to match bitmap
                APIHelp.Point sourceLocation = new APIHelp.Point(0, 0);
                APIHelp.Point newLocation = new APIHelp.Point(this.Left, this.Top);		// Same as this window
                APIHelp.BLENDFUNCTION blend = new APIHelp.BLENDFUNCTION();
                blend.BlendOp = APIHelp.AC_SRC_OVER;						// Only works with a 32bpp bitmap
                blend.BlendFlags = 0;											// Always 0
                blend.SourceConstantAlpha = 255;										// Set to 255 for per-pixel alpha values
                blend.AlphaFormat = APIHelp.AC_SRC_ALPHA;						// Only works when the bitmap contains an alpha channel                


                /*Graphics g = Graphics.FromHdc(memDc, hBitmap);
                Bitmap b = new Bitmap(Width, Height);                
                g.DrawLine(new Pen(Color.Red, 4), 0, 0, 100, 100);
                foreach (Control c in Controls)
                {
                    c.DrawToBitmap(b, new Rectangle(c.Left, c.Top, c.Width, c.Height));
                }
                if (tf!=null) tf.DrawToBitmap(b, new Rectangle(0, 0, tf.Width, tf.Height));
                g.DrawImage(b, 0, 0);
                b.Dispose();
                g.Dispose();*/

                // Update the window
                APIHelp.UpdateLayeredWindow(Handle, screenDc, ref newLocation, ref newSize,
                    memDc, ref sourceLocation, 0, ref blend, APIHelp.ULW_ALPHA);

                
            }
            finally
            {
                // Release device context
                APIHelp.ReleaseDC(IntPtr.Zero, screenDc);
                if (hBitmap != IntPtr.Zero)
                {
                    APIHelp.SelectObject(memDc, hOldBitmap);
                    APIHelp.DeleteObject(hBitmap);										// Remove bitmap resources
                }
                APIHelp.DeleteDC(memDc);
            }           
        }

        protected override CreateParams CreateParams
        {
            get
            {
                // Add the layered extended style (WS_EX_LAYERED) to this window
                CreateParams createParams = base.CreateParams;
                if (!DesignMode)
                {
                    createParams.ExStyle |= APIHelp.WS_EX_LAYERED;
                    createParams.ExStyle |= APIHelp.WS_EX_TOOLWINDOW;
                }
                return createParams;
            }
        }

        protected override void OnLocationChanged(EventArgs e)
        {
            if (tf != null && !DesignMode) tf.SetBounds(Left, Top, Width, Height);

            base.OnLocationChanged(e);
            SelectBitmap();
        }

        protected override void OnResize(EventArgs e)
        {
            if (tf != null && !DesignMode) tf.SetBounds(Left, Top, Width, Height);
            UpdateBitmap();
            
            base.OnResize(e);
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            if (!DesignMode)
            {
                if (Visible)
                {
                    if (tf != null)
                    {
                        tf.SetBounds(Left, Top, Width, Height);
                        tf.Show(this);
                        tf.SetBounds(Left, Top, Width, Height);
                        tf.Activate();
                    }
                }
                else
                {
                    if (tf != null) tf.Hide();
                }
            }
            SelectBitmap();
        }

        Bitmap bitmap;
        protected void UpdateBitmap()
        {
            if (bitmap != null) bitmap.Dispose();
            bitmap = CreateBitmap();
        }
        
        Bitmap CreateBitmap()
        {
            Bitmap bmp = new Bitmap(this.Width, this.Height);
            Graphics g = Graphics.FromImage(bmp);

            OnCreateBitmap(g, bmp);
            g.Dispose();

            return bmp;
        }

        protected virtual void OnCreateBitmap(Graphics g, Bitmap b)
        {
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == Ambertation.Windows.Forms.APIHelp.WM_NCHITTEST)
            {
                int lparam = m.LParam.ToInt32();
                Point screenPoint = new Point(lparam & 0xFFFF, lparam >> 16);
                Point pt = PointToClient(screenPoint);
                if (HeaderRect.Contains(pt))
                {
                    m.Result = new IntPtr(Ambertation.Windows.Forms.APIHelp.HTCAPTION);
                    return;
                }
            }
            base.WndProc(ref m);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            
            SelectBitmap();
            if (DesignMode)
            {
                base.OnPaintBackground(e);
                if (bitmap!=null) e.Graphics.DrawImage(bitmap, 0, 0);
            }
        }
    }
}