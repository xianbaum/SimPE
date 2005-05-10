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
using System.Drawing.Imaging;

namespace Ambertation.Graph
{
	/// <summary>
	/// An item that can be Displayed in a Graph
	/// </summary>
	public class GraphItem
	{
		Image store;
		ArrayList gps;
		Font font;

		string caption;
		public string Caption 
		{
			get { return caption; }
			set { caption = value; }
		}

		System.Drawing.Point location;
		public System.Drawing.Point Location
		{
			get { return location; }
			set 
			{ 
				location = value;	
			}
		}

		System.Drawing.Size size;
		public System.Drawing.Size Size
		{
			get { return size; }
			set 
			{ 
				size = value;
				UpdateGraphics();
			}
		}

		EventHandler click;
		public EventHandler Click 
		{
			get { return click; }
			set { click = value; }
		}

		object tag;
		public object Tag 
		{
			get { return tag; }
			set { tag = value; }
		}

		Hashtable properties;		
		public Hashtable Properties 
		{
			get { return properties;}
		}

		Color background;		
		public Color BackgroundColor
		{
			get { return background; }
			set 
			{ 
				background = value; 
				UpdateGraphics();
			}
		}

		Color foreground;		
		public Color ForegroundColor
		{
			get { return foreground; }
			set 
			{ 
				foreground = value; 
				UpdateGraphics();
			}
		}

		Color linkcolor;
		public Color LinkColor
		{
			get { return linkcolor; }
			set 
			{ 
				linkcolor = value; 
			}
		}	
	
		Color selectedcolor;
		public Color SelectedLinkColor
		{
			get { return selectedcolor; }
			set 
			{ 
				selectedcolor = value; 
			}
		}	

		bool selected;
		internal bool Selected 
		{
			get { return selected; }
			set 
			{
				selected = value;
				UpdateGraphics(); 
			}
		}

		Image thumb;
		public Image Thumbnail
		{
			get { return thumb; }
			set 
			{
				thumb = value;
				UpdateGraphics(); 
			}
		}

		GraphControl parent;		
		public GraphControl Parent
		{
			get { return parent; }
			set { parent = value; }
		}

		static ImageAttributes SetupImageAttr(float alpha)
		{
			float[][] ptsArray ={ 
									new float[] {1, 0, 0, 0, 0},
									new float[] {0, 1, 0, 0, 0},
									new float[] {0, 0, 1, 0, 0},
									new float[] {0, 0, 0, alpha, 0}, 
									new float[] {0, 0, 0, 0, 1}}; 
			ColorMatrix clrMatrix = new ColorMatrix(ptsArray);
			ImageAttributes imgAttributes = new ImageAttributes();
			imgAttributes.SetColorMatrix(clrMatrix,
				ColorMatrixFlag.Default,
				ColorAdjustType.Bitmap);

			return imgAttributes;
		}

		public GraphItem(GraphControl parent, string caption, Hashtable properties)
		{		
			this.parent = parent;
			if (properties==null) properties = new Hashtable();
			this.properties = properties;
			this.caption = caption;
			size = new System.Drawing.Size(200, 64);
			location = new System.Drawing.Point(0, 0);
			childs = new ArrayList();
			parents = new ArrayList();

			background = Color.WhiteSmoke;
			foreground = Color.FromArgb(200, Color.Black);
			linkcolor = Color.FromArgb(100, Color.SteelBlue);
			selectedcolor =  Color.SteelBlue;
			font = new Font("Verdana", 8, FontStyle.Bold);	
			updating=false;

			selected = false;
			gps = new ArrayList();
		
			UpdateGraphics();
		}

		#region RoundRect Routines
		public static void DrawRoundRect(Graphics g, Pen p, Rectangle rect, int radius)
		{
			DrawRoundRect(g, p, rect.X, rect.Y, rect.Width, rect.Height, radius);
		}

		public static void FillRoundRect(Graphics g, Brush b, Rectangle rect, int radius)
		{
			FillRoundRect(g, b, rect.X, rect.Y, rect.Width, rect.Height, radius);
		}

		public static void DrawRoundRect(Graphics g, Pen p, int x, int y, int width, int height, int radius)
		{			
			g.DrawPath(p, RoundRectPath(x, y, width, height, radius));
		}

		public static void FillRoundRect(Graphics g, Brush b, int x, int y, int width, int height, int radius)
		{			
			g.FillPath(b, RoundRectPath(x, y, width, height, radius));
		}

		static GraphicsPath RoundRectPath(int x, int y, int width, int height, int radius)
		{
			GraphicsPath gp = new GraphicsPath();
			gp.AddLine(x + radius, y, x + width - radius, y);
			gp.AddArc(x + width - radius, y, radius, radius, 270, 90);
			gp.AddLine(x + width, y + radius, x + width, y + height - radius);
			gp.AddArc(x + width - radius, y + height - radius, radius, radius, 0, 90);
			gp.AddLine(x + width - radius, y + height, x + radius, y + height);
			gp.AddArc(x, y + height - radius, radius, radius, 90, 90);
			gp.AddLine(x, y + height - radius, x, y + radius);
			gp.AddArc(x, y, radius, radius, 180, 90);
			gp.CloseFigure();
			return gp;
		}
		#endregion
        
		#region Painting
		bool updating;
		public void BeginUpdate() 
		{
			updating = true;
		}

		public void EndUpdate() 
		{
			updating=false;
			UpdateGraphics();
		}

		void UpdateGraphics()
		{
			if (updating) return;
			Image img = new Bitmap(Size.Width+1, Size.Height+1);
			Graphics gr = Graphics.FromImage(img);
			gr.SmoothingMode = SmoothingMode.HighQuality;

			Update(gr);

			if (store!=null) this.store.Dispose();			
			this.store = img;
		}

		/// <summary>
		/// Redraw the Shape
		/// </summary>
		protected void Update(Graphics gr)
		{
			if (gr!=null)
			{
				Rectangle srect = new Rectangle(new Point(0,0), size);

				Pen linepen = new Pen(Color.FromArgb(75, Color.Black));
				LinearGradientBrush linGrBrush = new LinearGradientBrush(
					new Point(0, 0),
					new Point(0, size.Height),					
					this.background,
					Color.Black); 

				float[] relativeIntensities = {0.0f, 0.05f, 0.2f};
				float[] relativePositions   = {0.0f, 0.7f, 1.0f};

				//Create a Blend object and assign it to linGrBrush.
				Blend blend = new Blend();
				blend.Factors = relativeIntensities;
				blend.Positions = relativePositions;
				linGrBrush.Blend = blend;


				
				//FillRoundRect(gr, new Pen(this.background).Brush, srect, 20);
				FillRoundRect(gr, linGrBrush, srect, 20);
				DrawRoundRect(gr, linepen, srect, 20);
				gr.DrawLine(linepen, new Point(0, 20), new Point(size.Width, 20));
				
				StringFormat sf = new StringFormat();
				sf.FormatFlags = StringFormatFlags.NoWrap;
				gr.DrawString(caption, font, new Pen(foreground).Brush, new RectangleF(new PointF(4, 4), new SizeF(size.Width-8, size.Height-8)), sf);
				
				int top = 24;
				Size indent = new Size(0,0);
				if (thumb!=null) 
				{
					gr.DrawImageUnscaled(thumb, 4, top, thumb.Width, thumb.Height);
					indent = new Size(thumb.Width+4, top+thumb.Height+4);
				}
				
				Hashtable ht = new Hashtable();
				foreach (string k in properties.Keys) 
				{
					object o = properties[k];
					ht[k] = o;

					string val = "";
					GraphProperty gp = null;

					if (o.GetType()==typeof(string)) 
					{
						val = (string)o;
					} 
					else 
					{
						gp = (GraphProperty)o;
						val = gp.Value;
					}
					
					if (val!=null) 
					{
						int indentx = 0;
						if (top<indent.Height) indentx = indent.Width;

						gr.DrawString(
							k+":", 
							new Font(font.FontFamily, font.Size, FontStyle.Italic), 
							new Pen(Color.FromArgb(160, foreground)).Brush, 
							new RectangleF(new PointF(indentx+10, top), new SizeF(size.Width-(24+indentx), top+16)), 
							sf);
						SizeF sz = gr.MeasureString(
							k+":", 
							new Font(font.FontFamily, font.Size, FontStyle.Italic));

						gr.DrawString(
							val, 
							new Font(font.FontFamily, font.Size), 
							new Pen(Color.FromArgb(140, foreground)).Brush, 
							new RectangleF(new PointF(indentx+12+sz.Width, top), new SizeF(size.Width-(24+sz.Width+indentx), top+16)), 
							sf);
						SizeF sz2 = gr.MeasureString(
							val, 
							new Font(font.FontFamily, font.Size));
						
						Rectangle rect = new Rectangle(new Point((int)(indentx+12+sz.Width), top), new Size((int)(size.Width-(24+sz.Width+indentx)), top+16));
						if (gp==null) 
						{
							/*gp = new GraphProperty(GraphPropertyType.TextBox, k, val, rect);
							gps.Add(gp);
							ht[k] = gp;*/
						}
						else gp.BoundingRect = rect;

						top += (int)Math.Max(sz.Height, sz2.Height);
					}
				}
				properties = ht;

				Point p1 = new Point(Size.Width / 2, 0);
				Point p2 = new Point(Size.Width / 2, Size.Height);

				Color cl;
				if (!selected) cl = linkcolor;
				else cl = selectedcolor;
				if (parents.Count>0) gr.FillEllipse(new Pen(cl).Brush, p1.X-6, p1.Y-6, 12, 12);
				if (childs.Count>0) gr.FillEllipse(new Pen(cl).Brush, p2.X-6, p2.Y-6, 12, 12);
			}
		}

		public void Paint(Graphics gr)
		{
			Size outer = new Size(size.Width+1, size.Height+1);
			Rectangle srect = new Rectangle(new Point(0,0), outer);
			Rectangle drect = new Rectangle(location, outer);
			gr.DrawImage(store, drect, srect, GraphicsUnit.Pixel);	
			MoveControls();
		}

		public void Paint(Graphics gr, float alpha)
		{
			ImageAttributes imgAttributes = GraphItem.SetupImageAttr(alpha);
			Size outer = new Size(size.Width+1, size.Height+1);
			Rectangle srect = new Rectangle(new Point(0,0), outer);
			Rectangle drect = new Rectangle(location, outer);
			gr.DrawImage(store, drect, srect.X, srect.Y, srect.Width, srect.Height, GraphicsUnit.Pixel, imgAttributes );			
			MoveControls();
		}

		public void PaintLinks(Graphics gr,  bool child)
		{		
			Color cl;
			if (!selected) cl = linkcolor;
			else cl = selectedcolor;
			
			Point p1 = new Point(location.X+Size.Width / 2, location.Y);
			Point p2 = new Point(location.X+Size.Width / 2, location.Y+Size.Height);

			foreach (GraphItem gi in parents) 
			{
					Point p3 = new Point(gi.Location.X + gi.Size.Width / 2, gi.Location.Y + gi.Size.Height);
					gr.DrawLine(new Pen(cl, 2), p1, p3);					
			}

			if (child) 
			{
				foreach (GraphItem gi in childs) 
				{
					Point p4 = new Point(gi.Location.X + gi.Size.Width / 2, gi.Location.Y);
					gr.DrawLine(new Pen(cl, 2), p2, p4);					
				}
			}
		}

		public Rectangle BoundingRect
		{
			get 
			{
				int x1 = location.X;
				int y1 = location.Y;
				int x2 = x1+size.Width;
				int y2 = y1+size.Height;

				foreach (GraphItem gi in parents) 
				{
					Point p3 = new Point(gi.Location.X + gi.Size.Width / 2, gi.Location.Y + gi.Size.Height);
					if (p3.X<x1) x1 = p3.X;
					if (p3.Y<y1) y1 = p3.Y;

					if (p3.X>x2) x2 = p3.X;
					if (p3.Y>y2) y2 = p3.Y;
				}

				foreach (GraphItem gi in childs) 
				{
					Point p3 = new Point(gi.Location.X + gi.Size.Width / 2, gi.Location.Y + gi.Size.Height);
					if (p3.X<x1) x1 = p3.X;
					if (p3.Y<y1) y1 = p3.Y;

					if (p3.X>x2) x2 = p3.X;
					if (p3.Y>y2) y2 = p3.Y;
				}

				return new Rectangle(x1, y1, x2-x1, y2-y1);
			}
		}

		public Rectangle Rectangle
		{
			get 
			{
				int x1 = location.X;
				int y1 = location.Y;
				int x2 = x1+size.Width;
				int y2 = y1+size.Height;				

				return new Rectangle(x1, y1, x2-x1, y2-y1);
			}
		}
		#endregion		

		#region References
		ArrayList childs, parents;
		public void AddChild(GraphItem cl)
		{
			if (!childs.Contains(cl)) 
			{
				cl.AddParent(this);
				childs.Add(cl);
				UpdateGraphics();
			}
		}

		public void RemoveChild(GraphItem cl)
		{
			if (childs.Contains(cl)) 
			{
				cl.RemoveParent(this);
				childs.Remove(cl);
				UpdateGraphics();
			}
		}

		internal void AddParent(GraphItem pn)
		{
			if (!parents.Contains(pn))
			{
				parents.Add(pn);
				UpdateGraphics();
			}
		}

		internal void RemoveParent(GraphItem pn)
		{
			if (parents.Contains(pn))
			{
				parents.Remove(pn);
				UpdateGraphics();
			}
		}
		#endregion

		#region GraphProperties
		void MoveControls(int dx, int dy)
		{
			foreach (GraphProperty gp in gps) gp.MoveControl(dx, dy);
		}

		public void MoveControls()
		{
			foreach (GraphProperty gp in gps) gp.MoveControl();
		}
		#endregion

		#region Mouse Control
		public bool IsWithin(System.Windows.Forms.MouseEventArgs e)
		{
			if ((e.X>this.location.X) && (e.Y>this.location.Y) && (e.X<this.location.X+this.size.Width) && (e.Y<this.location.Y+this.size.Height)) return true;
			return false;
		}

		public bool IsWithin(Rectangle rect)
		{
			return rect.IntersectsWith(this.BoundingRect);
		}
		
		internal void pb_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			lastpos = new Point(e.X, e.Y);
			down = true;
			bool update = false;

			foreach (GraphProperty gp in gps) 
			{
				if (gp.IsWithin(location, e)) {
					gp.ShowControl(location, this.parent.Parent);
				} 
				else 
				{
					if (gp.HideControl()) update=true;
				}
			}

			if (update) UpdateGraphics();
		}

		internal void pb_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (down) 
			{
				down = false;
				if (click!=null) click(this, null);
			}
		}		

		Point lastpos;
		bool down = false;
		internal bool WasMoved(System.Windows.Forms.MouseEventArgs e) 
		{
			bool ret = false;
			if (down)
			{
				int dx = e.X - lastpos.X;
				int dy = e.Y - lastpos.Y;
				if ((dx!=0) || (dy!=0)) ret = true;
			}
			return ret;
		}

		internal bool pb_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			bool ret = false;
			if (down)
			{								
				int dx = e.X - lastpos.X;
				int dy = e.Y - lastpos.Y;

				location.X += dx;
				location.Y += dy;	
				if ((dx!=0) || (dy!=0)) 
				{
					ret = true;
					MoveControls(dx, dy);
				}

				#region make sure the inputs are not active when moving!
				bool update = false;
				foreach (GraphProperty gp in gps) 
				{
					if (gp.HideControl()) update=true;					
				}
				if (update) UpdateGraphics();
				#endregion
			}

			lastpos = new Point(e.X, e.Y);

			

			return ret;
		}		
		
		#endregion
	}
}
