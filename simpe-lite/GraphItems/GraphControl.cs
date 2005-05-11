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
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Ambertation.Graph
{
	/// <summary>
	/// An item that can be Displayed in a Graph
	/// </summary>
	public class GraphControl
	{
		/// <summary>
		/// Set / Read the Canvas
		/// </summary>
		protected void SetupPictureBox()
		{
			if (pb!=null) 
			{
				pb.MouseDown += new MouseEventHandler(pb_MouseDown);
				pb.MouseUp += new MouseEventHandler(pb_MouseUp);
				pb.MouseMove += new MouseEventHandler(pb_MouseMove);
			}
		}

		PictureBox pb;			
		public PictureBox Parent
		{
			get { return pb; }
		}

		GraphItem selected;
		public GraphItem Selected 
		{
			get { return selected; }
		}

		ArrayList items;		

		public void Add(GraphItem gi)
		{
			if (items.Count==0) items.Add(gi);
			else items.Insert(0, gi);
		}

		public GraphControl(PictureBox pbox)
		{		
			pb = pbox;
			SetupPictureBox();

			items = new ArrayList();
		}

		#region Drawing
		void DrawLinks(Graphics gr) 
		{
			foreach (GraphItem gi in items) gi.PaintLinks(gr, false);

			if (selected!=null) 
			{
				selected.PaintLinks(gr, true);				
			}
		}

		void DrawLinks(Graphics gr, Rectangle cliprect) 
		{
			foreach (GraphItem gi in items) 
			{
				if (!gi.IsWithin(cliprect)) continue;
				gi.PaintLinks(gr, false);
			}

			if (selected!=null) 
			{
				if (!selected.IsWithin(cliprect)) return;
				selected.PaintLinks(gr, true);				
			}
		}

		void DrawItems(Graphics gr)
		{
			for (int i=items.Count-1; i>=0; i--)
			{
				GraphItem gi = (GraphItem)items[i]; 
				if (gi!=selected) gi.Paint(gr, 0.6f);
				else 
				{
					gi.Paint(gr);
					//gr.DrawRectangle(new Pen(Color.Black), gi.BoundingRect);
				}				
			}
		}

		void DrawItems(Graphics gr, Rectangle cliprect)
		{
			for (int i=items.Count-1; i>=0; i--)
			{				
				GraphItem gi = (GraphItem)items[i]; 
				if (!gi.IsWithin(cliprect)) continue;

				if (gi!=selected) gi.Paint(gr, 0.6f);
				else 
				{
					gi.Paint(gr);
					//gr.DrawRectangle(new Pen(Color.Black), gi.BoundingRect);
				}				
			}
		}

		public static Graphics MakeGraphics(Image img, bool high) 
		{
			Graphics gr = Graphics.FromImage(img);			
			if (high) 
			{
				gr.CompositingQuality = CompositingQuality.HighQuality;
				gr.SmoothingMode = SmoothingMode.HighQuality;
			} 
			else 
			{
				gr.CompositingQuality = CompositingQuality.HighSpeed;
				gr.SmoothingMode = SmoothingMode.HighSpeed;
			}

			return gr;
		}

		protected Image Paint() 
		{
			Image img = new Bitmap(pb.Width, pb.Height);
			Graphics gr = MakeGraphics(img, false);

			DrawLinks(gr);
			DrawItems(gr);

			return img;
		}

		public void Update()
		{
			Image old = pb.Image;
			pb.Image = Paint();
			if (old!=null) old.Dispose();
			old=null;
		}

		public void Update(Rectangle cliprect)
		{
			Image img = new Bitmap(pb.Width, pb.Height);
			Graphics gr = MakeGraphics(img, false);

			gr.FillRectangle(new Pen(Color.White).Brush, cliprect);

			DrawLinks(gr, cliprect);
			DrawItems(gr, cliprect);

			Graphics tgr = MakeGraphics(pb.Image, false);
			tgr.DrawImage(img, cliprect, cliprect, GraphicsUnit.Pixel);

			pb.Refresh();
		}
		#endregion

		public static Rectangle Max(Rectangle s1, Rectangle s2) 
		{
			int x1 = Math.Min(s1.Left-1, s2.Left-1);
			int x2 = Math.Max(s1.Right+1, s2.Right+1);

			int y1 = Math.Min(s1.Top-1, s2.Top-1);
			int y2 = Math.Max(s1.Bottom+1, s2.Bottom+1);

			return new Rectangle(x1, y1, x2, y2);
		}

		#region Mouse Control		
		private void pb_MouseDown(object sender, MouseEventArgs e)
		{
			Rectangle orect = new Rectangle(0,0,0,0);
			if (selected!=null) orect = selected.BoundingRect;

			foreach (GraphItem gi in items) 
				if (gi.IsWithin(e)) 
				{
					gi.pb_MouseDown(this, e);
					if (selected!=null) selected.Selected = false;
					selected = gi;
					selected.Selected = true;

					Update(orect);
					Update(selected.BoundingRect);
					break;
				}
		}		

		private void pb_MouseUp(object sender, MouseEventArgs e)
		{
			foreach (GraphItem gi in items) gi.pb_MouseUp(this, e);
			//Update();
		}
		
		private void pb_MouseMove(object sender, MouseEventArgs e)
		{			
			bool redraw = false;
			Rectangle cliprect = new Rectangle(0, 0, 0, 0);
			foreach (GraphItem gi in items) 
			{		
				Rectangle orect = gi.BoundingRect;
				redraw |= gi.pb_MouseMove(this, e);	
				if (redraw) 
				{
					if (selected!=null) selected.Selected = false;
					selected = gi;
					selected.Selected = true;
					cliprect = Max(orect, gi.BoundingRect);

					Update(cliprect);
					break;
				}
			}

			if (redraw) 
			{
				
			}
		}
		#endregion
	}
}
