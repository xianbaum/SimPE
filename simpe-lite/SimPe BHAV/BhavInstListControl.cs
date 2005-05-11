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
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.ContextMenu cmcopy;
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
		public event EventHandler SelectedIndexChanged;
		/// <summary>
		/// Indicates this control updated the Bhav wrapper
		/// </summary>
		public event EventHandler WrapperChanged;
		private int csel = -1;
		private Bhav wrapper = null;
		private InstructionItem[] flowitems;
		private PictureBox pnflow = null;

		/// <summary>
		/// Returns or sets the currently selected BhavInstruction for editing
		/// </summary>
		public int SelectedIndex
		{
			get { return csel; }
			set 
			{
				if (csel == value) return;
				if (value >= flowitems.Length || value < -1) throw(new Exception("Out Of Range"));
				if ((csel >= 0) && (csel < flowitems.Length))
					flowitems[csel].pnInstr.BackColor = System.Drawing.Color.White;
				csel = value;
				if ((csel >= 0) && (csel < flowitems.Length))
				{
					flowitems[csel].pnInstr.BackColor = System.Drawing.Color.PowderBlue;
					bhavInstListPanel.AutoScrollPosition = flowitems[csel].pnInstr.Location;
				}
				DrawConnectors();
				SelectedIndexChanged(this, new EventArgs());
			}
		}

		public void UpdateGUI(Bhav wrp)
		{
			wrapper = wrp;
			int oldCsel = csel;
			BhavInstList instructions;
			if (wrapper != null)
				instructions = wrapper.Instructions;
			else
				instructions = new BhavInstList();

			FlipPanel();
			pnflow.Controls.Clear();
//			pnflow.Location = new System.Drawing.Point(0, 0);

			flowitems = new InstructionItem[instructions.Count];
			csel = 0;
			foreach (Instruction i in instructions) 
			{
				flowitems[csel] = new InstructionItem();
				flowitems[csel].index = csel;
				flowitems[csel].instruction = i;
				Panel pn = flowitems[csel].pnInstr = new Panel();

				pn.Parent = pnflow;
				pn.Height = InstructionItem.Height;
				pn.Top = csel*(pn.Height+4);				
				pn.Left = 0;
				pn.Width = pnflow.ClientRectangle.Width - 120;
				pn.Visible = true;
				pn.BorderStyle = BorderStyle.FixedSingle;
				if (csel == oldCsel) 
					pn.BackColor = System.Drawing.Color.PowderBlue;
				else
					pn.BackColor = System.Drawing.Color.White;
				pn.ContextMenu = this.cmcopy;
				
				UpdateFlowPanel();	// pn getting updated

				csel++;
			}
			if (oldCsel < flowitems.Length)
				csel = oldCsel;
			else
				SelectedIndex = -1; // raise the event

			DrawConnectors();
			ShowActivePanel();
		}


		private void UpdateFlowPanel()
		{
			int ct = csel;
			InstructionItem item = flowitems[ct];
			Instruction i = item.instruction;
			Panel pn = item.pnInstr;
			pn.Controls.Clear();

			LinkLabel lb = new LinkLabel();
			lb.Parent = pn;
			lb.Left = 0;
			lb.Top = 0;
			lb.Width = pn.Width - 40;
			lb.Height = pn.Height;
			lb.Text = ct.ToString("X") + ": " + i.ToString();
			lb.BackColor = System.Drawing.Color.Transparent;
			lb.Visible = true;
			lb.Tag = ct;
			lb.Click += new EventHandler(InstructionPanelClick);
			lb.LinkClicked += new LinkLabelLinkClickedEventHandler(LinkInstructionPanelClick);
			lb.MouseMove += new MouseEventHandler(ItemMouseMove);

			if (ct>0) 
			{
				LinkLabel llup = new LinkLabel();
				llup.Parent = pn;								
				llup.AutoSize = true;
				llup.Text = "up";
				llup.Top = 1;
				llup.Left = pn.Width - (llup.Width + 1);		
				llup.Visible = true;
				llup.Tag = ct;

				llup.LinkClicked += new LinkLabelLinkClickedEventHandler(MoveItemUp);
				llup.Click += new EventHandler(InstructionPanelClick);	
			}

			if (ct<flowitems.Length-1) 
			{
				LinkLabel lldn = new LinkLabel();
				lldn.Parent = pn;					
				lldn.AutoSize = true;
				lldn.Text = "down";
				lldn.Left = pn.Width - (lldn.Width + 1);					
				lldn.Top = pn.Height - (lldn.Height + 1);
				lldn.Visible = true;
				lldn.Tag = ct;

				lldn.LinkClicked += new LinkLabelLinkClickedEventHandler(MoveItemDown);
				lldn.Click += new EventHandler(InstructionPanelClick);	
			}

			LinkLabel llt = new LinkLabel();
			llt.Parent = lb;					
			llt.AutoSize = true;
			llt.Text = "true: "+i.Target1.ToString("X");
			llt.Left = lb.Left;					
			llt.Top = pn.Height - (llt.Height + 1);
			llt.Visible = true;
			llt.Tag = i.Target1;
			if (i.Target1<flowitems.Length)
			{
				llt.LinkArea = new LinkArea(6, llt.Text.Length-6);
				llt.LinkClicked += new LinkLabelLinkClickedEventHandler(SelectTagItem);
			}
			else
				llt.LinkArea = new LinkArea(0, 0);

			LinkLabel llf = new LinkLabel();
			llf.Parent = lb;					
			llf.AutoSize = true;
			llf.Text = "false: "+i.Target2.ToString("X");
			llf.Left = llt.Left + llt.Width + 4;
			llf.Top = pn.Height - (llf.Height + 1);
			llf.Visible = true;
			llf.Tag = i.Target2;
			if (i.Target2<flowitems.Length)
			{
				llf.LinkArea = new LinkArea(7, llf.Text.Length-7);
				llf.LinkClicked += new LinkLabelLinkClickedEventHandler(SelectTagItem);
			}
			else
				llf.LinkArea = new LinkArea(0, 0);
		}

		private void FlipPanel()
		{			
			if (pnflow==null) 
			{
				pnflow2.Visible = false;
				pnflow=pnflow1;
			} 
			else 
			{
				if (pnflow.Name == "pnflow1") pnflow = pnflow2;
				else pnflow = pnflow1;
			}
			this.Update();
		}

		private void ShowActivePanel() 
		{	
			
			bhavInstListPanel.AutoScroll = false;
			if (pnflow.Name == "pnflow1") 
			{
//				pnflow1.Top = bhavInstListPanel.AutoScrollPosition.Y;
				pnflow1.Visible = true;
				pnflow2.Visible = false;
//				pnflow2.Top = 0;
			} 
			else 
			{
//				pnflow2.Top = bhavInstListPanel.AutoScrollPosition.Y;
				pnflow2.Visible = true;
				pnflow1.Visible = false;
//				pnflow1.Top = 0;
			}
			if (csel >= 0 && csel < flowitems.Length)
				bhavInstListPanel.AutoScrollPosition = flowitems[csel].pnInstr.Location;
			else
				bhavInstListPanel.AutoScrollPosition = new System.Drawing.Point(0, 0);
			bhavInstListPanel.AutoScroll = true;
		}

		private void DrawConnectors()
		{			
			try 
			{
				Connector[] connectors = InstructionItem.Connectors(flowitems);			
				Connector.ResolveCollisions(connectors);

				Bitmap img = new Bitmap(bhavInstListPanel.ClientRectangle.Width-24, Math.Max(10, flowitems.Length * (InstructionItem.Height + 4)));
				Graphics gr = Graphics.FromImage(img);
				gr.SmoothingMode =  System.Drawing.Drawing2D.SmoothingMode.HighQuality;
				gr.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
				gr.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

				Pen tpen = new Pen(Color.DarkGreen, 1);
				Pen fpen = new Pen(Color.Maroon, 1);
				Pen tpens = new Pen(Color.LawnGreen, 2);
				Pen fpens = new Pen(Color.LightCoral, 2);
				Pen pen;

				string [] resNames = this.GetType().Assembly.GetManifestResourceNames();

				Bitmap bmpok = new Bitmap(this.GetType().Assembly.GetManifestResourceStream("SimPe.Plugin.button_ok.png"));
				Bitmap bmpcancel = new Bitmap(this.GetType().Assembly.GetManifestResourceStream("SimPe.Plugin.button_cancel.png"));
				Point[] points;
				foreach (Connector c in connectors) 
				{
					if (c==null) continue;
					if (c.truerule) pen = tpen; else pen = fpen;
					if (c.start == csel) if (c.truerule) pen = tpens; else pen = fpens;
					if (c.stop == csel) pen = new Pen(pen.Brush, 2);
					int offset = 4;
					if (c.truerule && (c.stop - c.start == 1)) continue;
					if (c.truerule) offset+=4; 

					if (c.start >= flowitems.Length) continue;
					Control startlabel = (Control)flowitems[c.start].pnInstr;

					if (c.stop >= flowitems.Length) 
					{
						if ( (c.stop  == 0xFFFD) || (c.stop == 0x00FF)  )
						{
							gr.DrawLine(	
								pen, 
								startlabel.Right, 
								startlabel.Top + (startlabel.Height / 2) + offset,
								img.Width-16,
								startlabel.Top + (startlabel.Height / 2) + offset
								);
							if (bmpok!=null) 
							{
								gr.DrawImageUnscaled(
									bmpok,
									img.Width-16,
									startlabel.Top + (startlabel.Height / 2) + offset - 8
									);
							}
							points = new Point[3];
							points[0] = new Point(img.Width-16, startlabel.Top + (startlabel.Height / 2) + offset);
							points[1] = new Point(points[0].X - 4, points[0].Y - 4);
							points[2] = new Point(points[0].X - 4, points[0].Y + 4);
							gr.FillPolygon(pen.Brush, points);
						} 
						else if ( (c.stop == 0xFFFC) || (c.stop == 0xFFFE) || (c.stop == 0x00FE) || (c.stop == 0x00FD))
						{
							int sub = 40;
							if ((c.stop == 0xFFFE) || (c.stop == 0x00FD)) sub = 16;
							gr.DrawLine(	
								pen, 
								startlabel.Right, 
								startlabel.Top + (startlabel.Height / 2) + offset,
								img.Width-sub,
								startlabel.Top + (startlabel.Height / 2) + offset
								);
							if (bmpcancel!=null) 
							{
								gr.DrawImageUnscaled(
									bmpcancel,
									img.Width-sub,
									startlabel.Top + (startlabel.Height / 2) + offset - 8
									);
							}
							points = new Point[3];
							points[0] = new Point(img.Width-sub, startlabel.Top + (startlabel.Height / 2) + offset);
							points[1] = new Point(points[0].X - 4, points[0].Y - 4);
							points[2] = new Point(points[0].X - 4, points[0].Y + 4);
							gr.FillPolygon(pen.Brush, points);
						} 
					
						continue;
					}
				
					Control stoplabel = (Control)flowitems[c.stop].pnInstr;
					gr.DrawLine(	
						pen, 
						startlabel.Right, 
						startlabel.Top + (startlabel.Height / 2) + offset,
						startlabel.Right + (c.lane * 4) + offset,
						startlabel.Top + (startlabel.Height / 2) + offset
						);

					gr.DrawLine(	
						pen, 
						startlabel.Right + (c.lane * 4) + offset, 
						startlabel.Top + (startlabel.Height / 2) + offset,
						stoplabel.Right + (c.lane * 4) + offset,
						stoplabel.Top + (stoplabel.Height / 2) - offset 
						);

					gr.DrawLine(	
						pen, 
						stoplabel.Right + (c.lane * 4) + offset, 
						stoplabel.Top + (stoplabel.Height / 2) - offset,
						stoplabel.Right,
						stoplabel.Top + (stoplabel.Height / 2) - offset
						);
			
				
					points = new Point[3];
					points[0] = new Point(stoplabel.Right, stoplabel.Top + (stoplabel.Height / 2) - offset);
					points[1] = new Point(points[0].X + 4, points[0].Y - 4);
					points[2] = new Point(points[0].X + 4, points[0].Y + 4);
					gr.FillPolygon(pen.Brush, points);
				}

				pnflow.Image = img;
			} 
			catch (Exception) 
			{
			}
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
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.cmcopy = new System.Windows.Forms.ContextMenu();
			this.pnflow1 = new System.Windows.Forms.PictureBox();
			this.pnflow2 = new System.Windows.Forms.PictureBox();
			this.bhavInstListPanel = new System.Windows.Forms.Panel();
			this.SuspendLayout();
			// 
			// menuItem1
			// 
			this.menuItem1.Enabled = ((bool)(resources.GetObject("menuItem1.Enabled")));
			this.menuItem1.Index = 0;
			this.menuItem1.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("menuItem1.Shortcut")));
			this.menuItem1.ShowShortcut = ((bool)(resources.GetObject("menuItem1.ShowShortcut")));
			this.menuItem1.Text = resources.GetString("menuItem1.Text");
			this.menuItem1.Visible = ((bool)(resources.GetObject("menuItem1.Visible")));
			this.menuItem1.Click += new System.EventHandler(this.CopyInstruction);
			// 
			// menuItem2
			// 
			this.menuItem2.Enabled = ((bool)(resources.GetObject("menuItem2.Enabled")));
			this.menuItem2.Index = 1;
			this.menuItem2.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("menuItem2.Shortcut")));
			this.menuItem2.ShowShortcut = ((bool)(resources.GetObject("menuItem2.ShowShortcut")));
			this.menuItem2.Text = resources.GetString("menuItem2.Text");
			this.menuItem2.Visible = ((bool)(resources.GetObject("menuItem2.Visible")));
			this.menuItem2.Click += new System.EventHandler(this.InsertInstruction);
			// 
			// cmcopy
			// 
			this.cmcopy.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																				   this.menuItem1,
																				   this.menuItem2});
			this.cmcopy.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cmcopy.RightToLeft")));
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
			this.bhavInstListPanel.Text = resources.GetString("bhavInstListPanel.Text");
			this.bhavInstListPanel.Visible = ((bool)(resources.GetObject("bhavInstListPanel.Visible")));
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
			this.Resize += new System.EventHandler(this.FlowResize);
			this.ResumeLayout(false);

		}

		private void InstructionPanelClick(object sender, System.EventArgs e)
		{
			SelectedIndex = (int)((Label)sender).Tag;
		}

		private void MoveItemUp(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			int sel = (int)((LinkLabel)sender).Tag;
			wrapper.Instructions.Move(sel, sel-1);
			wrapper.Changed = true;
			WrapperChanged(this, new EventArgs());

			SelectedIndex = sel - 1;
			UpdateGUI(wrapper);
		}

		private void MoveItemDown(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			int sel = (int)((LinkLabel)sender).Tag;
			wrapper.Instructions.Move(sel, sel+1);
			wrapper.Changed = true;
			WrapperChanged(this, new EventArgs());

			SelectedIndex = sel + 1;
			UpdateGUI(wrapper);
		}

		private void RepaintFlow(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			//DrawConnectors();
		}

		private void FlowResize(object sender, System.EventArgs e)
		{
			UpdateGUI(wrapper);
		}

		private void CopyInstruction(object sender, System.EventArgs e)
		{
			if (csel<0) return;
			DataObject m_data = new DataObject("Instruction", this.flowitems[csel]);
			Clipboard.SetDataObject(m_data);
		}

		private void InsertInstruction(object sender, System.EventArgs e)
		{
			try 
			{
				InstructionItem i = (InstructionItem)Clipboard.GetDataObject().GetData("Instruction") ;
				if (i==null) return;
				wrapper.Instructions.Add(i.instruction);
				wrapper.Changed = true;
				WrapperChanged(this, new EventArgs());
				DrawConnectors();
			} 
			catch (Exception) {}
			
		}

		private void LinkInstructionPanelClick(object sender, LinkLabelLinkClickedEventArgs e)
		{
			this.InstructionPanelClick(sender, e);
		}
		private void ItemMouseMove(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left) 
			{
				((Label)sender).DoDragDrop(((Label)sender).Tag, DragDropEffects.Link);
			}
		}

		private void SelectTagItem(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			object o = ((LinkLabel)sender).Tag;
			if (o!=null) 
			{
				ushort t = (ushort)o;
				if ((t>=0) && (t<flowitems.Length)) 
					SelectedIndex = t;
			}
		}

		#endregion
	}

	internal class InstructionItem 
	{
		public int index;
		public const int Height = 32;
		public Instruction instruction;
		public Panel pnInstr;
		public Connector TrueConnect 
		{
			get 
			{
				Connector c = new Connector();
				c.start = index;
				c.stop = instruction.Target1;
				c.lane = 0;
				c.truerule = true;
				return c;
			}
		}
		public Connector FalseConnect
		{
			get 
			{
				Connector c = new Connector();
				c.start = index;
				c.stop = instruction.Target2;
				c.lane = 0;
				c.truerule = false;
				return c;
			}
		}

		public static Connector[] Connectors(InstructionItem[] items)
		{
			if (items==null) return new Connector[0];
			Connector[] cs = new Connector[items.Length*2];
			for (int i=0; i<items.Length; i++)
			{
				cs[i*2] = items[i].TrueConnect;
				cs[i*2+1] = items[i].FalseConnect;
			}

			return cs;
		}

	}
	/// <summary>
	/// Used for Instruction Connectors
	/// </summary>
	internal class Connector 
	{
		public int start;
		public int stop;
		public int lane;
		public bool truerule;

		/// <summary>
		/// Returns the Distance of start and stop
		/// </summary>
		public int Distance 
		{
			get { return Math.Abs(stop - start); }
		}

		/// <summary>
		/// Returns the Upper EdgePOint (smaler number)
		/// </summary>
		public int Top 
		{
			get { return Math.Min(start, stop); }
		}

		/// <summary>
		/// Returns the Lower EdgePOint (bigger number)
		/// </summary>
		public int Bottom
		{
			get { return Math.Max(start, stop); }
		}

		/// <summary>
		/// True if the passed Connector has a collision with this one
		/// </summary>
		/// <param name="c">The Connector to check against</param>
		/// <returns>true on Collision</returns>
		public bool HasCollisionWith(Connector c)
		{
			return !((Bottom <= c.Top ) || (Top >= c.Bottom));
		}

		/// <summary>
		/// Resolves all lane Collisions
		/// </summary>
		/// <param name="connectors">List of connectors</param>
		public static void ResolveCollisions(Connector[] connectors) 
		{
			foreach (Connector c in connectors) c.lane = 3;

			foreach (Connector c1 in connectors) 
			{
				int countsub = 0;
				foreach (Connector c2 in connectors) 
				{
					if ((c2.Top>=c1.Top) && (c2.Bottom<=c1.Bottom)) countsub ++;					
				}
				c1.lane += countsub - 1;
			}
		}
	}
}
