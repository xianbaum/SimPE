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
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Floaters{
    public class ManagedLayeredForm : LayeredForm
    {
        DockManager manager;
        internal DockManager Manager
        {
            get { return manager; }
        }
        protected ManagedLayeredForm(DockManager manager)
            : base()
        {
            this.manager = manager; 
        }
        internal ManagedLayeredForm(DockManager manager, Bitmap bitmap)
            : base(bitmap)
        {
            this.manager = manager;
        }

        internal ManagedLayeredForm(DockManager manager, Color cl, Size sz)
            : base(cl, sz)
        {
            this.manager = manager;
        }

        internal virtual void MouseOver(Point pt, bool hit)
        {
            //if (hit) //Console.WriteLine("hit");
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
        }
    }

    public class LayeredForm:Form
    {
        protected LayeredForm() 
            : this(null)
        {
            
        }

        public LayeredForm(Bitmap bitmap)
		{
			// Window settings
            Init(bitmap);
		}

        protected virtual void Init(Bitmap bitmap)
        {
            this.TopMost = true;
            this.FormBorderStyle = FormBorderStyle.None;
            this.ControlBox = false;
            this.ShowInTaskbar = false;
            dorefresh = false;
            
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;

            if (bitmap != null)
            {
                this.Size = bitmap.Size;
                this.Show();	// Must be called before setting bitmap
                this.SelectBitmap(bitmap);
                colored = false;
            } else colored = true;
        }

        bool colored;
        Color cl;
        static Bitmap CreateBitmap(Color cl, Size sz)
        {
            Bitmap ret = new Bitmap(sz.Width, sz.Height);
            Graphics g = Graphics.FromImage(ret);
            SolidBrush b = new SolidBrush(cl);
            g.FillRectangle(b, 0, 0, sz.Width - 1, sz.Height - 1);
            b.Dispose();
            g.Dispose();

            return ret;
        }

        public LayeredForm(Color cl, Size sz)
            :this(CreateBitmap(cl, sz))
        {

            colored = true;
            this.cl = cl;
        }

        bool dorefresh;
        public void RefreshBitmap()
        {
            if (!Visible) dorefresh = true;
            else
            {
                SelectBitmap(CreateBitmap(cl, Size));
                dorefresh = false;
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            if (colored) RefreshBitmap();
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            if (dorefresh) RefreshBitmap();
        }

        
		// Sets the current bitmap
		public void SelectBitmap(Bitmap bitmap) 
		{
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
				blend.BlendOp             = APIHelp.AC_SRC_OVER;						// Only works with a 32bpp bitmap
				blend.BlendFlags          = 0;											// Always 0
				blend.SourceConstantAlpha = 255;										// Set to 255 for per-pixel alpha values
				blend.AlphaFormat         = APIHelp.AC_SRC_ALPHA;						// Only works when the bitmap contains an alpha channel

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
				createParams.ExStyle |= APIHelp.WS_EX_LAYERED;
                createParams.ExStyle |= APIHelp.WS_EX_TOOLWINDOW;
				return createParams;
			}
		}

		// Let Windows drag this window for us (thinks its hitting the title bar of the window)
		/*protected override void WndProc(ref Message message) 
		{
			if (message.Msg == APIHelp.WM_NCHITTEST) 
			{
				// Tell Windows that the user is on the title bar (caption)
				message.Result= (IntPtr)APIHelp.HTCAPTION;
			}
			else
			{
				base.WndProc(ref message);
			}
		}*/

        internal Point ScreenLocation
        {
            get { return Location; }
        }

        internal bool Hit(Point scrpt)
        {
            if (!Visible) return false;

            Point l = ScreenLocation;
            if (scrpt.X > l.X && scrpt.X < l.X + Width)
                if (scrpt.Y > l.Y && scrpt.Y < l.Y + Height)
                    return true;

            return false;
        }        
	}
}
