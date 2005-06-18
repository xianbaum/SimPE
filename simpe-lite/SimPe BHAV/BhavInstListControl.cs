/***************************************************************************
 *   Copyright (C) 2005 by Peter L Jones                                   *
 *   peter@drealm.info                                                     *
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
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using SimPe.Interfaces.Plugin;
using SimPe.PackedFiles.Wrapper;

namespace SimPe.PackedFiles.UserInterface
{
	/// <summary>
	/// Summary description for UserControl1.
	/// </summary>
	public class BhavInstListControl : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.PictureBox pnflow1;
		private System.Windows.Forms.PictureBox pnflow2;
		private System.Windows.Forms.Panel bhavInstListPanel;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public BhavInstListControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}


		#region BhavInstListControl
		/// <summary>
		/// Indicates the value of SelectedIndex has changed
		/// </summary>
		public event EventHandler SelectedInstChanged;

		private int csel = -1;
		private Bhav wrapper = null;
		private BhavInstListItemUI[] flowitems;
		private PictureBox pnflow = null;

		public void UpdateGUI(Bhav wrp)
		{
			wrapper = wrp;
			csel = (wrapper.Instructions.Count > 0) ? 0 : -1;
			pnflow1.Visible = false;
			pnflow1.Controls.Clear();
			pnflow2.Visible = false;
			pnflow2.Controls.Clear();
			pnflow = pnflow1;

			myrepaint();
			OnSelectedInstChanged(new EventArgs());
		}


		protected virtual void OnSelectedInstChanged(EventArgs e) { if (SelectedInstChanged != null) { SelectedInstChanged(this, e); } }


		/// <summary>
		/// Returns or sets the index of the currently selected instruction
		/// </summary>
		public int SelectedIndex
		{
			get { return csel; }
			set 
			{
				if (csel == value) return;
				if (value >= flowitems.Length || value < -1) throw new Exception("Internal failure: csel out of range: " + value.ToString());

				if ((csel >= 0) && (csel < flowitems.Length))
					flowitems[csel].MakeUnselected();

				csel = value;

				if ((csel >= 0) && (csel < flowitems.Length))
				{
					flowitems[csel].MakeSelected();
					flowitems[csel].Focus();
					int newY = flowitems[csel].Top;
					int currentYmin = -this.AutoScrollPosition.Y;
					int currentYmax = currentYmin + this.Height - (BhavInstListItemUI.rowHeight+4);
					if (newY < currentYmin)
					{
						this.AutoScrollPosition = new Point(this.AutoScrollPosition.X, newY);
					}
					else if (newY > currentYmax)
					{
						newY = currentYmin + newY - currentYmax;
						this.AutoScrollPosition = new Point(this.AutoScrollPosition.X, newY);
					}
				}
				pnflow.Image = DrawConnectors();
				Update();
				OnSelectedInstChanged(new EventArgs());
			}
		}

		/// <summary>
		/// Returns or sets the values of the currently selected instruction
		/// </summary>
		public Instruction SelectedInst
		{
			get 
			{ 
				if (csel >= 0) return ((Instruction)wrapper.Instructions[csel]).Clone(); else return null; 
			}
			set 
			{
				if (csel < 0 && value == null) return;
				if (csel < 0) throw new Exception("No current instruction");
				if (value == null) throw new Exception("Invalid value");
				if (csel >= wrapper.Instructions.Count) throw new Exception("Internal failure: csel out of range" + value.ToString());

				Instruction oldInst = wrapper.Instructions[csel].Clone();
				wrapper.Instructions[csel] = value.Clone();

				pnflow.Controls.RemoveAt(csel);
				flowitems[csel] = makeBhavInstListItemUI(csel);
				if (value.Target1 != oldInst.Target1)
				{
					if (oldInst.Target1 < wrapper.Instructions.Count)
					{
						pnflow.Controls.RemoveAt(oldInst.Target1);
						flowitems[oldInst.Target1] = makeBhavInstListItemUI(oldInst.Target1);
					}
					if (value.Target1 < wrapper.Instructions.Count)
					{
						pnflow.Controls.RemoveAt(value.Target1);
						flowitems[value.Target1] = makeBhavInstListItemUI(value.Target1);
					}
				}
				if (value.Target2 != oldInst.Target2)
				{
					if (oldInst.Target2 < wrapper.Instructions.Count)
					{
						pnflow.Controls.RemoveAt(oldInst.Target2);
						flowitems[oldInst.Target2] = makeBhavInstListItemUI(oldInst.Target2);
					}
					if (value.Target2 < wrapper.Instructions.Count)
					{
						pnflow.Controls.RemoveAt(value.Target2);
						flowitems[value.Target2] = makeBhavInstListItemUI(value.Target2);
					}
				}
				flowitems[csel].MakeSelected();
				pnflow.Image = DrawConnectors();
				Update();
				OnSelectedInstChanged(new EventArgs());
			}
		}


		public void Add()
		{
			if (csel >= wrapper.Instructions.Count) throw new Exception("Internal failure: csel out of range");

			int newIndex = csel;
			if (csel < 0)
				wrapper.Instructions.Insert(0, new Instruction(wrapper));
			else
				wrapper.Instructions.Insert(csel + 1, SelectedInst);

			csel = -1;
			myrepaint();

			csel = wrapper.Instructions.Count;
			if (newIndex >= csel)
				newIndex = csel - 1;
			SelectedIndex = newIndex;
		}

		public void Delete()
		{
			if (csel < 0) throw new Exception("No current instruction");
			if (csel >= wrapper.Instructions.Count) throw new Exception("Internal failure: csel out of range");

			int newIndex = csel;
			wrapper.Instructions.RemoveAt(csel);

			csel = -1;
			myrepaint();

			csel = wrapper.Instructions.Count;
			if (newIndex >= csel)
				newIndex = csel - 1;
			SelectedIndex = newIndex;
		}

		public void MoveInst(int delta)
		{
			if (csel < 0) throw new Exception("No current instruction");
			if (csel >= wrapper.Instructions.Count) throw new Exception("Internal failure: csel out of range");

			int to = csel + delta;
			if (to < 0) to = 0;
			if (to >= wrapper.Instructions.Count) to = wrapper.Instructions.Count - 1;
			if (csel == to) return;

			wrapper.Instructions.Move(csel, to);

			csel = -1;
			myrepaint();
			SelectedIndex = to;
		}

		public void Sort()
		{
			Instruction inst = null;
			if (csel > -1)
				inst = wrapper.Instructions[csel];

			wrapper.Instructions.Sort();

			csel = -1;
			myrepaint();
			if (inst != null)
				SelectedIndex = wrapper.Instructions.IndexOf(inst);
		}


		private void myrepaint()
		{
			Point currentLoc = this.AutoScrollPosition;
			int oldCsel = csel;

			if (pnflow.Name == "pnflow1") // indicates which is currently visible
			{
				pnflow = pnflow2;
			} 
			else 
			{
				pnflow = pnflow1;
			}

			pnflow.Controls.Clear();
			flowitems = new BhavInstListItemUI[wrapper.Instructions.Count];

			for (int i = 0; i < wrapper.Instructions.Count; i++)
				flowitems[i] = makeBhavInstListItemUI(i);

			pnflow.Image = DrawConnectors();

			if (pnflow.Name == "pnflow1") // indicates which we just updated
			{
				pnflow2.Visible = false;
				pnflow1.Visible = true;
			} 
			else 
			{
				pnflow1.Visible = false;
				pnflow2.Visible = true;
			}
			if (csel != -1)
				flowitems[csel].MakeSelected();

			this.AutoScrollPosition = currentLoc;
			Update();
		}

		private BhavInstListItemUI makeBhavInstListItemUI(int ct)
		{
			bool isTarget = false;
			for (int j = 0; j < wrapper.Instructions.Count && !isTarget; j++)
				if (ct == 0 || (j != ct && (
					(wrapper.Instructions[j].Target1 == ct) ||
					(wrapper.Instructions[j].Target2 == ct)
					)))
					isTarget = true;

			BhavInstListItemUI i = new BhavInstListItemUI(ct, wrapper.Instructions[ct], wrapper.Instructions.Count - 1, pnflow, isTarget);

			i.Top = ct*(BhavInstListItemUI.rowHeight+4);
			i.Width = bhavInstListPanel.Width - 120;
			i.MoveUp += new EventHandler(bhavInst_MoveUp);
			i.MoveDown += new EventHandler(bhavInst_MoveDown);
			i.Selected += new EventHandler(bhavInst_Selected);
			i.Unselected += new EventHandler(bhavInst_Unselected);
			i.TargetClick += new LinkLabelLinkClickedEventHandler(bhavInst_TargetClick);
			i.KeyDown += new KeyEventHandler(bhavInst_KeyDown);

			return i;
		}

		private void AddUnlinked(Bitmap img)
		{
			Graphics gr = Graphics.FromImage(img);
			gr.SmoothingMode =  System.Drawing.Drawing2D.SmoothingMode.HighQuality;
			gr.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
			gr.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

			Pen pen = new Pen(Color.FromArgb(64, 64, 64), 1);
			for (int ct = 1; ct < flowitems.Length; ct++)
			{
				if (flowitems[ct].IsTarget) continue;

				int xPosLeft = flowitems[ct].Width;
				int xPosRight = img.Width - 1;
				int yPos = (BhavInstListItemUI.rowHeight + 4) * ct + (BhavInstListItemUI.rowHeight / 4);

				gr.DrawLine(
					pen, 
					xPosLeft, yPos,
					xPosRight, yPos
					);
				Point[] points = new Point[3];
				points[0] = new Point(xPosLeft, yPos);
				points[1] = new Point(points[0].X + 4, points[0].Y - 4);
				points[2] = new Point(points[0].X + 4, points[0].Y + 4);
				gr.FillPolygon(pen.Brush, points);
			}
		}

		private Bitmap DrawConnectors()
		{
			if (flowitems.Length == 0)
				return null;
			if (bhavInstListPanel.ClientRectangle.Width <= 24)
				return null;

			Bitmap img = new Bitmap(bhavInstListPanel.ClientRectangle.Width-24, flowitems.Length * (BhavInstListItemUI.rowHeight + 4));
			Graphics gr = Graphics.FromImage(img);
			gr.SmoothingMode =  System.Drawing.Drawing2D.SmoothingMode.HighQuality;
			gr.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
			gr.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

			Pen tpen = new Pen(Color.FromArgb(0, 128, 0), 1);
			Pen fpen = new Pen(Color.FromArgb(128, 0, 0), 1);
			Pen tpeno = new Pen(Color.FromArgb(0, 220, 0), 3);
			Pen fpeno = new Pen(Color.FromArgb(220, 0, 0), 3);
			Pen tpeni = tpen;
			Pen fpeni = fpen;
			Pen pen;

			Point[] points;

			int yUnit = BhavInstListItemUI.rowHeight / 8;

			foreach (Connector c in Connector.Connectors(wrapper.Instructions)) 
			{
				if (c==null) continue;
				if (c.start == c.stop) continue; // skip go to self
				if (c.start >= flowitems.Length) continue;

				if (c.truerule) pen = tpen; else pen = fpen;
				if (c.stop == csel)  if (c.truerule) pen = tpeni; else pen = fpeni;
				if (c.start == csel) if (c.truerule) pen = tpeno; else pen = fpeno;

				Control startlabel = (Control)flowitems[c.start];

				int yPosStart = startlabel.Top + (yUnit * 4) + (yUnit * (c.truerule ? 3 : 1));
				int xPosLeft = startlabel.Right;
				int xPosRight;

				if (c.stop < flowitems.Length)
				{
					if (c.stop - c.start == 1)
					{
						int xPos = startlabel.Right - 144 + (c.truerule ? 8 : 72);

						gr.DrawLine(	
							new Pen(pen.Brush, (c.start == csel) ? 4 : 2),
							xPos, startlabel.Bottom,
							xPos, startlabel.Bottom + 5
							);
					}
					else 
					{
						const int laneWidth = 5;
						xPosRight = startlabel.Right + 7 + (c.lane * laneWidth);

						Control stoplabel = (Control)flowitems[c.stop];
						int yPosStop = stoplabel.Top + (yUnit * (c.truerule ? 1 : 3));

						gr.DrawLine(	
							pen, 
							xPosLeft, yPosStart,
							xPosRight, yPosStart
							);

						gr.DrawLine(	
							pen, 
							xPosRight, yPosStart,
							xPosRight, yPosStop
							);

						gr.DrawLine(	
							pen, 
							xPosRight, yPosStop,
							xPosLeft, yPosStop
							);
				
						points = new Point[3];
						points[0] = new Point(xPosLeft, yPosStop);
						points[1] = new Point(points[0].X + 4, points[0].Y - 4);
						points[2] = new Point(points[0].X + 4, points[0].Y + 4);
						gr.FillPolygon(pen.Brush, points);
					}
				}
				else
				{
					xPosRight = img.Width - (c.truerule ? 20 : 10);
					string glyph;
					string font;
					float pts;

					switch (c.stop)
					{
						case 0xFFFC: // Error
							glyph = "E"; font = "Verdana"; pts = 8.25F; break;
						case 0xFFFD: // True
							glyph = "T"; font = "Verdana"; pts = 8.25F; break;
						case 0xFFFE: // False
							glyph = "F"; font = "Verdana"; pts = 8.25F; break;
						default: // Off the end
							glyph = "?"; font = "Verdana"; pts = 8.25F; break;
					}

					gr.DrawLine(
						pen, 
						xPosLeft, yPosStart,
						xPosRight, yPosStart
						);
					gr.DrawString(
						glyph,
						new System.Drawing.Font(font, pts),
						pen.Brush,
						xPosRight, yPosStart - 8
						);
				}
			}
			AddUnlinked(img);
			return img;
		} 

		#endregion

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(BhavInstListControl));
			this.pnflow1 = new System.Windows.Forms.PictureBox();
			this.pnflow2 = new System.Windows.Forms.PictureBox();
			this.bhavInstListPanel = new System.Windows.Forms.Panel();
			this.SuspendLayout();
			// 
			// pnflow1
			// 
			this.pnflow1.AccessibleDescription = resources.GetString("pnflow1.AccessibleDescription");
			this.pnflow1.AccessibleName = resources.GetString("pnflow1.AccessibleName");
			this.pnflow1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("pnflow1.Anchor")));
			this.pnflow1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnflow1.BackgroundImage")));
			this.pnflow1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("pnflow1.Dock")));
			this.pnflow1.Enabled = ((bool)(resources.GetObject("pnflow1.Enabled")));
			this.pnflow1.Font = ((System.Drawing.Font)(resources.GetObject("pnflow1.Font")));
			this.pnflow1.Image = ((System.Drawing.Image)(resources.GetObject("pnflow1.Image")));
			this.pnflow1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("pnflow1.ImeMode")));
			this.pnflow1.Location = ((System.Drawing.Point)(resources.GetObject("pnflow1.Location")));
			this.pnflow1.Name = "pnflow1";
			this.pnflow1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("pnflow1.RightToLeft")));
			this.pnflow1.Size = ((System.Drawing.Size)(resources.GetObject("pnflow1.Size")));
			this.pnflow1.SizeMode = ((System.Windows.Forms.PictureBoxSizeMode)(resources.GetObject("pnflow1.SizeMode")));
			this.pnflow1.TabIndex = ((int)(resources.GetObject("pnflow1.TabIndex")));
			this.pnflow1.TabStop = false;
			this.pnflow1.Text = resources.GetString("pnflow1.Text");
			this.pnflow1.Visible = ((bool)(resources.GetObject("pnflow1.Visible")));
			// 
			// pnflow2
			// 
			this.pnflow2.AccessibleDescription = resources.GetString("pnflow2.AccessibleDescription");
			this.pnflow2.AccessibleName = resources.GetString("pnflow2.AccessibleName");
			this.pnflow2.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("pnflow2.Anchor")));
			this.pnflow2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnflow2.BackgroundImage")));
			this.pnflow2.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("pnflow2.Dock")));
			this.pnflow2.Enabled = ((bool)(resources.GetObject("pnflow2.Enabled")));
			this.pnflow2.Font = ((System.Drawing.Font)(resources.GetObject("pnflow2.Font")));
			this.pnflow2.Image = ((System.Drawing.Image)(resources.GetObject("pnflow2.Image")));
			this.pnflow2.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("pnflow2.ImeMode")));
			this.pnflow2.Location = ((System.Drawing.Point)(resources.GetObject("pnflow2.Location")));
			this.pnflow2.Name = "pnflow2";
			this.pnflow2.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("pnflow2.RightToLeft")));
			this.pnflow2.Size = ((System.Drawing.Size)(resources.GetObject("pnflow2.Size")));
			this.pnflow2.SizeMode = ((System.Windows.Forms.PictureBoxSizeMode)(resources.GetObject("pnflow2.SizeMode")));
			this.pnflow2.TabIndex = ((int)(resources.GetObject("pnflow2.TabIndex")));
			this.pnflow2.TabStop = false;
			this.pnflow2.Text = resources.GetString("pnflow2.Text");
			this.pnflow2.Visible = ((bool)(resources.GetObject("pnflow2.Visible")));
			// 
			// bhavInstListPanel
			// 
			this.bhavInstListPanel.AccessibleDescription = resources.GetString("bhavInstListPanel.AccessibleDescription");
			this.bhavInstListPanel.AccessibleName = resources.GetString("bhavInstListPanel.AccessibleName");
			this.bhavInstListPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("bhavInstListPanel.Anchor")));
			this.bhavInstListPanel.AutoScroll = ((bool)(resources.GetObject("bhavInstListPanel.AutoScroll")));
			this.bhavInstListPanel.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("bhavInstListPanel.AutoScrollMargin")));
			this.bhavInstListPanel.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("bhavInstListPanel.AutoScrollMinSize")));
			this.bhavInstListPanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("bhavInstListPanel.BackgroundImage")));
			this.bhavInstListPanel.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("bhavInstListPanel.Dock")));
			this.bhavInstListPanel.Enabled = ((bool)(resources.GetObject("bhavInstListPanel.Enabled")));
			this.bhavInstListPanel.Font = ((System.Drawing.Font)(resources.GetObject("bhavInstListPanel.Font")));
			this.bhavInstListPanel.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("bhavInstListPanel.ImeMode")));
			this.bhavInstListPanel.Location = ((System.Drawing.Point)(resources.GetObject("bhavInstListPanel.Location")));
			this.bhavInstListPanel.Name = "bhavInstListPanel";
			this.bhavInstListPanel.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("bhavInstListPanel.RightToLeft")));
			this.bhavInstListPanel.Size = ((System.Drawing.Size)(resources.GetObject("bhavInstListPanel.Size")));
			this.bhavInstListPanel.TabIndex = ((int)(resources.GetObject("bhavInstListPanel.TabIndex")));
			this.bhavInstListPanel.TabStop = true;
			this.bhavInstListPanel.Text = resources.GetString("bhavInstListPanel.Text");
			this.bhavInstListPanel.Visible = ((bool)(resources.GetObject("bhavInstListPanel.Visible")));
			this.bhavInstListPanel.Resize += new System.EventHandler(this.bhavInstListPanel_Resize);
			// 
			// BhavInstListControl
			// 
			this.AccessibleDescription = resources.GetString("$this.AccessibleDescription");
			this.AccessibleName = resources.GetString("$this.AccessibleName");
			this.AutoScroll = ((bool)(resources.GetObject("$this.AutoScroll")));
			this.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMargin")));
			this.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMinSize")));
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.Controls.Add(this.pnflow1);
			this.Controls.Add(this.pnflow2);
			this.Controls.Add(this.bhavInstListPanel);
			this.Enabled = ((bool)(resources.GetObject("$this.Enabled")));
			this.Font = ((System.Drawing.Font)(resources.GetObject("$this.Font")));
			this.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("$this.ImeMode")));
			this.Location = ((System.Drawing.Point)(resources.GetObject("$this.Location")));
			this.Name = "BhavInstListControl";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.Size = ((System.Drawing.Size)(resources.GetObject("$this.Size")));
			this.ResumeLayout(false);

		}

		#endregion

		private void bhavInst_MoveUp(object sender, System.EventArgs e) { MoveInst(-1); }
		private void bhavInst_MoveDown(object sender, System.EventArgs e) { MoveInst(1); }
		private void bhavInst_Selected(object sender, System.EventArgs e) { SelectedIndex = ((BhavInstListItemUI)sender).findUI(flowitems); }
		private void bhavInst_Unselected(object sender, System.EventArgs e) {/* SelectedIndex = -1; */}
		private void bhavInst_TargetClick(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e) { SelectedIndex = (UInt16)e.Link.LinkData; }
		private void bhavInst_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			// SelectedIndex must already indicate the current selected instruction
			switch (e.KeyCode)
			{
				case System.Windows.Forms.Keys.Up:
					if (csel > 0) SelectedIndex--;
					break;
				case System.Windows.Forms.Keys.Down:
					if (csel < flowitems.Length - 1) SelectedIndex++;
					break;
				case System.Windows.Forms.Keys.Delete:
					if (csel > -1 && flowitems.Length > 1)
						Delete();
					break;
				case System.Windows.Forms.Keys.Home:
					SelectedIndex = 0;
					break;
				case System.Windows.Forms.Keys.End:
					SelectedIndex = flowitems.Length - 1;
					break;
			}
		}


		private void bhavInstListPanel_Resize(object sender, System.EventArgs e)
		{
			if (wrapper != null && pnflow != null && ((System.Windows.Forms.Panel)sender).ClientRectangle.Width > 24)
			{
				myrepaint();
			}
		}

		private void TagItem_Click(object sender, System.EventArgs e)
		{
			SelectedIndex = (int)((Control)sender).Tag;
		}

		private void LinkLabelLink_Click(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			SelectedIndex = (UInt16)((LinkLabel)sender).Tag;
		}

		private void LinkLabel_Click(object sender, System.EventArgs e)
		{
			TagItem_Click(((LinkLabel)sender).Parent, e);
		}


	}


	#region Connector
	/// <summary>
	/// Used for Instruction Connectors
	/// </summary>
	internal class Connector 
	{
		/// <summary>
		/// Instruction number for start of connector
		/// </summary>
		public int start;
		/// <summary>
		/// Instruction number for end of connector
		/// </summary>
		public int stop;
		/// <summary>
		/// avoid collisions by keeping to lane
		/// </summary>
		public int lane = -1;
		/// <summary>
		/// True if this is connection from a True link
		/// </summary>
		public bool truerule;

		protected Connector(int start, int stop, bool truerule)
		{
			this.start = start;
			this.stop = stop;
			this.lane = lane;
			this.truerule = truerule;
		}


		/// <summary>
		/// Returns an array of pairs of Connector()s for the true and false targets of each instruction
		/// </summary>
		/// <param name="items">BhavInstList from wrapper</param>
		/// <returns></returns>
		public static Connector[] Connectors(BhavInstList items)
		{
			if (items==null) return new Connector[0];

			Connector[] cs = new Connector[items.Count*2];
			for (int i=0; i<items.Count; i++)
			{
				cs[i*2] = new Connector(i, items[i].Target1, true);
				cs[i*2+1] = new Connector(i, items[i].Target2, false);
			}

			Connector.ResolveCollisions(cs);
			return cs;
		}


		#region ResolveCollisions
		/// <summary>
		/// Returns "connector number" for inwards connector (to stop)
		/// </summary>
		private int InOffset
		{
			get
			{
				return 0 + (truerule ? 0 : 1);
			}
		}

		/// <summary>
		/// Returns "connector number" for outwards connector (from start)
		/// </summary>
		private int OutOffset
		{
			get
			{
				return 2 + (truerule ? 1 : 0);
			}
		}

		/// <summary>
		/// Which of 'start' and 'stop' is the earlier instruction
		/// </summary>
		private int Top 
		{
			get { return Math.Min(start * 4 + OutOffset, stop * 4 + InOffset); }
		}

		/// <summary>
		/// Which of 'start' and 'stop' is the later instruction
		/// </summary>
		private int Bottom
		{
			get { return Math.Max(start * 4 + OutOffset, stop * 4 + InOffset); }
		}

		/// <summary>
		/// Resolves all lane Collisions
		/// </summary>
		/// <param name="connectors">List of connectors</param>
		private static void ResolveCollisions(Connector[] connectors) 
		{
			foreach (Connector c1 in connectors) 
			{
				c1.lane = -1;
				if (c1.stop * 2 > connectors.Length) continue; // off end, doesn't use a lane
				if (c1.stop == c1.start + 1) continue; // next line, doesn't use a lane
				if (c1.stop == c1.start) continue; // same line, doesn't use a lane

				ArrayList used = new ArrayList();
				foreach (Connector c2 in connectors)
				{
					if (c2.lane == -1) continue; // it's not using a lane

					if (c2.Top > c1.Bottom) continue; // c1 completely before c2 - skip
					if (c2.Bottom < c1.Top) continue; // c1 completely after c2 - skip
					if (c2.stop == c1.stop) continue; // same target - skip

					// At this point c2 could be using a lane c1 wants to use
					used.Add((Int16) c2.lane);
				}
				used.Sort();
				c1.lane = 0;
				foreach (Int16 i in used)
					if (c1.lane == i) c1.lane++;
			}
		}

		#endregion
	}
	#endregion
}
