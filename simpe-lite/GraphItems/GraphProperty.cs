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
using System.Windows.Forms;

namespace Ambertation.Graph
{
	/// <summary>
	/// List of available Types
	/// </summary>
	public enum GraphPropertyType : byte
	{
		Static = 0,
		TextBox = 1
	}

	/// <summary>
	/// Class for editable GraphItem Properties
	/// </summary>
	public class GraphProperty
	{
		string val, caption;
		public string Value 
		{
			get { return val;}
			set { val = value; }
		}
		public string Caption
		{
			get { return caption;}
			set { caption = value; }
		}

		GraphPropertyType type;
		public GraphPropertyType Type 
		{
			get { return type;}
			set { type = value; }
		}

		Rectangle rect;
		public Rectangle BoundingRect
		{
			get { return rect; }
			set { rect = value; }
		}

		Point offset;

		public bool Active
		{
			get {return ctrl!=null; }
		}

		~GraphProperty()
		{
			HideControl();
		}

		public GraphProperty(string caption, string val)
		{
			this.type = GraphPropertyType.Static;
			this.val = val;
			this.caption = caption;

			rect = new Rectangle(0,0,0,0);
			ctrlpos = new Point(0);
			ctrl = null;
		}

		public GraphProperty(GraphPropertyType type, string caption, string val)
		{
			this.type = type;
			this.val = val;
			this.caption = caption;

			rect = new Rectangle(0,0,0,0);
			ctrlpos = new Point(0);
			ctrl = null;
		}

		internal GraphProperty(GraphPropertyType type, string caption, string val, Rectangle location)
		{
			this.type = type;
			this.val = val;
			this.caption = caption;

			rect = location;
			ctrlpos = new Point(0);
			ctrl = null;
		}

		#region input Control
		Control ctrl;
		Point ctrlpos;

		public void ShowControl(Point offset, Control parent)
		{			
			if (ctrl!=null) return;

			this.offset = offset;
			ctrl = new TextBox();
			ctrl.Parent = parent;
			ctrl.Left = offset.X + rect.Left;
			ctrl.Top = offset.Y + rect.Top;

			ctrl.Height = rect.Height;
			ctrl.Width = rect.Width;

			ctrl.Text = this.val;
			((TextBox)ctrl).BorderStyle = BorderStyle.None;

			ctrlpos.X = ctrl.Left;
			ctrlpos.Y = ctrl.Top;
		}

		public bool HideControl()
		{
			if (ctrl==null) return false;

			ctrl.Visible = false;	
			try 
			{
				ctrl.Parent.Controls.Remove(ctrl);
				ctrl.Dispose();
			} 
			catch {}
			
			ctrl = null;

			return true;
		}

		internal void MoveControl(int dx, int dy)
		{
			if (ctrl==null) return;

			ctrlpos.X += dx;
			ctrlpos.Y += dy;
		}

		public void MoveControl()
		{	
			if (ctrl==null) return;

			ctrl.Left = ctrlpos.X;
			ctrl.Top = ctrlpos.Y;
		}
		#endregion

		public bool IsWithin(Point offset, System.Windows.Forms.MouseEventArgs e)
		{
			this.offset = offset;
			if ((e.X-offset.X>this.rect.Left) && (e.Y-offset.Y>this.rect.Top) && (e.X-offset.X<this.rect.Right) && (e.Y-offset.Y<this.rect.Bottom)) return true;
			return false;
		}
	}
}
