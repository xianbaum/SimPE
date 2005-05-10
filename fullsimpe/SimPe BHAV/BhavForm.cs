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
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using SimPe.Interfaces.Plugin;
using SimPe.PackedFiles.Wrapper;

namespace SimPe.PackedFiles.UserInterface
{
	/// <summary>
	/// Summary description for BconForm.
	/// </summary>
	public class BhavForm : System.Windows.Forms.Form, IPackedFileUI
	{
		#region Form variables
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btcommit;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox tbformat;
		private System.Windows.Forms.TextBox tbtype;
		private System.Windows.Forms.TextBox tbargc;
		private System.Windows.Forms.TextBox tbflags;
		private System.Windows.Forms.TextBox tblocals;
		private System.Windows.Forms.TextBox tbzero;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.TextBox tbopcode;
		private System.Windows.Forms.TextBox tbres;
		private System.Windows.Forms.TextBox tbo0;
		private System.Windows.Forms.TextBox tbo1;
		private System.Windows.Forms.TextBox tbo2;
		private System.Windows.Forms.TextBox tbo3;
		private System.Windows.Forms.TextBox tbo7;
		private System.Windows.Forms.TextBox tbo6;
		private System.Windows.Forms.TextBox tbo5;
		private System.Windows.Forms.TextBox tbo4;
		private System.Windows.Forms.TextBox tbu7;
		private System.Windows.Forms.TextBox tbu6;
		private System.Windows.Forms.TextBox tbu5;
		private System.Windows.Forms.TextBox tbu4;
		private System.Windows.Forms.TextBox tbu3;
		private System.Windows.Forms.TextBox tbu2;
		private System.Windows.Forms.TextBox tbu1;
		private System.Windows.Forms.TextBox tbu0;
		private System.Windows.Forms.LinkLabel llcommit;
		private System.Windows.Forms.GroupBox gbopcodes;
		private System.Windows.Forms.LinkLabel lladd;
		private System.Windows.Forms.Panel pnflowcontainer;
		private System.Windows.Forms.LinkLabel lldel;
		private System.Windows.Forms.ComboBox tba1;
		private System.Windows.Forms.ComboBox tba2;
		private System.Windows.Forms.PictureBox pnflow1;
		private System.Windows.Forms.PictureBox pnflow2;
		private System.Windows.Forms.LinkLabel llopenbhav;
		private System.Windows.Forms.TextBox lbbhav;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.ContextMenu cmcopy;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.LinkLabel llsort;
		private System.Windows.Forms.Button btwiz;
		private System.Windows.Forms.TextBox lbtext;
		private System.Windows.Forms.LinkLabel llmove;
		private System.Windows.Forms.Label label45;
		private System.Windows.Forms.TextBox tbmv;
		private System.Windows.Forms.Panel bhavPanel;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion
       
		public BhavForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			FlipPanel();
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

		
		#region Bhav
		private Bhav wrapper = null;
		private int csel = -1;
		private bool internalchg;
		private InstructionItem[] flowitems;
		private PictureBox pnflow;

		private void UpdateInstructionDetails(Instruction inst)
		{
			this.tbopcode.Text = "0x"+Helper.HexString(inst.OpCode);
			this.tbres.Text = "0x"+Helper.HexString(inst.Reserved0);
			this.tba1.SelectedIndex = -1;
			this.tba1.Text = "0x"+Helper.HexString(inst.Target1);
			this.tba2.SelectedIndex = -1;
			this.tba2.Text = "0x"+Helper.HexString(inst.Target2);

			this.tbo0.Text = Helper.HexString(inst.Operands[0]);
			this.tbo1.Text = Helper.HexString(inst.Operands[1]);
			this.tbo2.Text = Helper.HexString(inst.Operands[2]);
			this.tbo3.Text = Helper.HexString(inst.Operands[3]);
			this.tbo4.Text = Helper.HexString(inst.Operands[4]);
			this.tbo5.Text = Helper.HexString(inst.Operands[5]);
			this.tbo6.Text = Helper.HexString(inst.Operands[6]);
			this.tbo7.Text = Helper.HexString(inst.Operands[7]);

			this.tbu0.Text = Helper.HexString(inst.Reserved1[0]);
			this.tbu1.Text = Helper.HexString(inst.Reserved1[1]);
			this.tbu2.Text = Helper.HexString(inst.Reserved1[2]);
			this.tbu3.Text = Helper.HexString(inst.Reserved1[3]);
			this.tbu4.Text = Helper.HexString(inst.Reserved1[4]);
			this.tbu5.Text = Helper.HexString(inst.Reserved1[5]);
			this.tbu6.Text = Helper.HexString(inst.Reserved1[6]);
			this.tbu7.Text = Helper.HexString(inst.Reserved1[7]);

			this.lbtext.Text = inst.ToString();
		}
		
		private void SetReadOnly(bool state) 
		{
			this.tbo0.ReadOnly = state;
			this.tbo1.ReadOnly = state;
			this.tbo2.ReadOnly = state;
			this.tbo3.ReadOnly = state;
			this.tbo4.ReadOnly = state;
			this.tbo5.ReadOnly = state;
			this.tbo6.ReadOnly = state;
			this.tbo7.ReadOnly = state;
			
			this.tbu0.ReadOnly = state;
			this.tbu1.ReadOnly = state;
			this.tbu2.ReadOnly = state;
			this.tbu3.ReadOnly = state;
			this.tbu4.ReadOnly = state;
			this.tbu5.ReadOnly = state;
			this.tbu6.ReadOnly = state;
			this.tbu7.ReadOnly = state;

			tbopcode.ReadOnly = state;
			tbres.ReadOnly = state;

			tba1.Enabled = !state;
			tba2.Enabled = !state;

			tbmv.ReadOnly = state;

			btwiz.Enabled = !state;
			button4.Enabled = !state;

			llmove.Enabled = !state;
		}

		private int MoveItem(int index, bool up)
		{
			int sel = csel;
			if (up) 
			{
				if (sel==index) sel --;
				else if (sel==index-1) sel++;

				if (sel<0 || sel>=flowitems.Length) return csel;

				SortSwap((ushort)index, (ushort)(index-1));
			} 
			else 
			{
				if (sel==index+1) sel--;
				else if (sel==index) sel++;

				if (sel<0 || sel>=flowitems.Length) return csel;
				SortSwap((ushort)index, (ushort)(index+1));
			}

			return sel;
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
		}

		private void ShowActivePanel() 
		{	
			
			pnflowcontainer.AutoScroll = false;
			if (pnflow.Name == "pnflow1") 
			{
				pnflow1.Top = pnflowcontainer.AutoScrollPosition.Y;
				pnflow1.Visible = true;
				pnflow2.Visible = false;
				pnflow2.Top = 0;
			} 
			else 
			{
				pnflow2.Top = pnflowcontainer.AutoScrollPosition.Y;
				pnflow2.Visible = true;
				pnflow1.Visible = false;
				pnflow1.Top = 0;
			}
			
			pnflowcontainer.AutoScroll = true;
		}

		private void UpdateFlowPanel(Panel pn, Instruction i, int ct) 
		{
			pn.Controls.Clear();

			LinkLabel lb = new LinkLabel();
			lb.Parent = pn;
			lb.Left = 0;
			lb.Top = 0;
			lb.Width = pn.Width - 40;
			lb.Height = pn.Height;
			lb.Text = ct.ToString("X") + ": " + i.ToString();				
			if (ct==csel) lb.BackColor = System.Drawing.Color.PowderBlue;
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
				//llup.LinkColor = Color.Yellow;
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
				//lldn.LinkColor = Color.Yellow;
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
			//llt.LinkColor = Color.Yellow;
			llt.Tag = i.Target1;
			if (i.Target1<flowitems.Length) llt.LinkArea = new LinkArea(6, llt.Text.Length-6);
			else llt.LinkArea = new LinkArea(0, 0);
			llt.LinkClicked += new LinkLabelLinkClickedEventHandler(SelectTagItem);

			LinkLabel llf = new LinkLabel();
			llf.Parent = lb;					
			llf.AutoSize = true;
			llf.Text = "false: "+i.Target2.ToString("X");
			llf.Left = llt.Left + llt.Width + 4;
			llf.Top = pn.Height - (llf.Height + 1);
			llf.Visible = true;
			//llf.LinkColor = Color.Yellow;
			llf.Tag = i.Target2;
			if (i.Target2<flowitems.Length) llf.LinkArea = new LinkArea(7, llf.Text.Length-7);
			else llf.LinkArea = new LinkArea(0, 0);
			llf.LinkClicked += new LinkLabelLinkClickedEventHandler(SelectTagItem);

			flowitems[ct].lable = lb;
			flowitems[ct].instruction = i;
				
			Connector cnt = new Connector();
			cnt.start = ct;
			cnt.stop = i.Target1;
			cnt.lane = 1;
			cnt.truerule = true;

			Connector cnf = new Connector();
			cnf.start = ct;
			cnf.stop = i.Target2;
			cnf.lane = 1;
			cnf.truerule = false;

			flowitems[ct].index = ct;		
		}

		private void CreateFlowPanel(Instruction[] instructions) 
		{			
			FlipPanel();

			//csel = -1;
			int ct = 0;
			flowitems = new InstructionItem[instructions.Length];		
			//pnflow.Height = Math.Max(10, flowitems.Length * (height + 4));
			pnflow.Controls.Clear();
			foreach (Instruction i in instructions) 
			{
				flowitems[ct] = new InstructionItem();

				Panel pn = new Panel();
				pn.Parent = pnflow;
				pn.Height = InstructionItem.Height;
				pn.Top = ct*(pn.Height+4);				
				pn.Left = 0;
				pn.Width = pnflow.ClientRectangle.Width - 120;
				pn.Visible = true;
				pn.BorderStyle = BorderStyle.FixedSingle;
				pn.BackColor = System.Drawing.Color.White;
				/*pn.BackColor = System.Drawing.Color.SteelBlue;				
				pn.ForeColor = System.Drawing.SystemColors.HighlightText;*/
				//pn.ContextMenu = this.cmcopy;
				
				UpdateFlowPanel(pn, i, ct);		

				ct++;
			}

			DrawConnectors();
			ShowActivePanel();
		}

		private void DrawConnectors()
		{			
			try 
			{
				Connector[] connectors = InstructionItem.Connectors(flowitems);			
				Connector.ResolveCollisions(connectors);

				Bitmap img = new Bitmap(pnflowcontainer.ClientRectangle.Width-24, Math.Max(10, flowitems.Length * (InstructionItem.Height + 4)));
				Graphics gr = Graphics.FromImage(img);
				gr.SmoothingMode =  System.Drawing.Drawing2D.SmoothingMode.HighQuality;
				gr.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
				gr.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

				Pen tpen = new Pen(Color.YellowGreen, 1);
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
					if (c.truerule) offset+=4; 

					if (c.start >= flowitems.Length) continue;
					Control startlabel = (Control)flowitems[c.start].lable.Parent;

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
				
					Control stoplabel = (Control)flowitems[c.stop].lable.Parent;
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

		private void SortSwap(ushort a, ushort b) 
		{
			if (a>=flowitems.Length) return;
			if (b>=flowitems.Length) return;
			if (a<0) return;
			if (b<0) return;

			InstructionItem i = flowitems[a];
			flowitems[a] = flowitems[b];
			flowitems[b] = i;

			foreach (InstructionItem item in flowitems)
			{
				if (item.instruction.Target1 == a) item.instruction.Target1 = (ushort)b;
				else if (item.instruction.Target1 == b) item.instruction.Target1 = (ushort)a;

				if (item.instruction.Target2 == a) item.instruction.Target2 = (ushort)b;
				else if (item.instruction.Target2 == b) item.instruction.Target2 = (ushort)a;
			}
		}

#if PLJxx
		private class treeNode
		{
			public Instruction instruction;
			public treeNode parent;
//			private bool refTrueTarget;
			private treeNode trueChild;
//			private bool refFalseTarget;
			private treeNode falseChild;

			public treeNode(treeNode p, Instruction i)
			{
				instruction = i;
				parent = p;
				TrueChild = null;
				FalseChild = null;
			}

			public treeNode TrueChild
			{
				get
				{
//					if (refTrueTarget) return null;
					if (instruction.Target1 >= 0xfffc) return null;
					return trueChild;
				}
				set
				{
					trueChild = value;
//					refTrueTarget = (trueChild == null);
				}
			}
			public treeNode FalseChild
			{
				get
				{
//					if (refFalseTarget) return null;
					if (instruction.Target2 >= 0xfffc) return null;
					return falseChild;
				}
				set
				{
					falseChild = value;
//					refFalseTarget = (falseChild == null);
				}
			}


			protected void removeChild(treeNode child)
			{
				if ((trueChild != null) && trueChild.Equals(child))
					TrueChild = null;
				if ((falseChild != null) && falseChild.Equals(child))
					FalseChild = null;
				child.parent = null;
			}

			protected treeNode findInTree(ushort target, treeNode root, InstructionItem[] flowitems)
			{
				if (root == null)
					return null;
				if (root.instruction.Index == target)
					return root;
				treeNode child;
				child = findInTree(target, root.TrueChild, flowitems);
				if (child != null) return child;
				child = findInTree(target, root.FalseChild, flowitems);
				if (child != null) return child;
				return null;
			}

			public void fillTree(treeNode treeRoot, InstructionItem[] flowitems)
			{
				if (instruction == null) return;

				if (instruction.Target1 < 0xfffc)
				{
					treeNode child = findInTree(instruction.Target1, treeRoot, flowitems);
					// Not found:
					if (child == null)
					{
						TrueChild = new treeNode(this, flowitems[instruction.Target1].instruction);
						TrueChild.fillTree(treeRoot, flowitems);
					}
					else
					{
						TrueChild = child;
						/*
						// Found, not this node and not root (this loops!!):
						if (!child.Equals(this) && (child.parent != null))
						{
							child.parent.removeChild(child);
							TrueChild = child;
							TrueChild.parent = this;
						}
						*/
					}
				}

				if (instruction.Target2 < 0xfffc)
				{
					treeNode child = findInTree(instruction.Target2, treeRoot, flowitems);
					// Not found:
					if (child == null)
					{
						FalseChild = new treeNode(this, flowitems[instruction.Target2].instruction);
						FalseChild.fillTree(treeRoot, flowitems);
					}
					else
					{
						FalseChild = child;
						/*
						// Found, not this node and not root (this loops!!):
						if (!child.Equals(this) && (child.parent != null))
						{
							child.parent.removeChild(child);
							FalseChild = child;
							FalseChild.parent = this;
						}
						*/
					}
				}
			}
		}

		private bool findInstruction(InstructionItem[] tree, ushort last, ushort target, ushort current)
		{
			for (ushort i = 0; i <= last; i++)
				if ((i != current) && (tree[i].index == target))
					return true;
			for (ushort i = 0; i <= last; i++)
				if (i != current)
				{
					if (tree[i].instruction.Target1 == target)
						return true;
					if (tree[i].instruction.Target2 == target)
						return true;
				}
			return false;
		}

		private ushort treeWalk(InstructionItem[] newFlowItems, ushort last, treeNode root)
		{
			if (root == null) return last;

#if DEBUG
			if (last > newFlowItems.Length) throw(new Exception("last: " + last.ToString() + "; newFlowItems.Length: " + newFlowItems.Length.ToString()));
#endif
			newFlowItems[last] = new InstructionItem();
			newFlowItems[last].instruction = root.instruction;
			if (root.TrueChild != null)
				last = treeWalk(newFlowItems, (ushort)(last+1), root.TrueChild);
			if (root.FalseChild != null)
				last = treeWalk(newFlowItems, (ushort)(last+1), root.FalseChild);

			return last;
		}

		private ushort findItem(InstructionItem[] iis, ushort target)
		{
			for(ushort i = 0; i < iis.Length; i++)
				if (iis[i].instruction.Index == target) return i;
			return (ushort)iis.Length;
		}

#endif
		#endregion

		#region IPackedFileUI Member
		/// <summary>
		/// Returns the Panel that will be displayed within SimPe
		/// </summary>
		public System.Windows.Forms.Panel GUIHandle
		{
			get
			{
				return bhavPanel;
			}
		}

		/// <summary>
		/// Is called by SimPe (through the Wrapper) when the Panel is going to be displayed, so
		/// you should updatet the Data displayed by the Panel with the Attributes stored in the
		/// passed Wrapper.
		/// </summary>
		/// <remarks>attr.Tag is used to let TextChanged event handlers know the change is being
		/// made internally rather than by the users.</remarks>
		/// <param name="wrapper">The Attributes of this Wrapper have to be displayed</param>
		public void UpdateGUI(IFileWrapper wrp)
		{
			wrapper = (Bhav) wrp;

			internalchg = true;
			SetReadOnly(true);
			lbbhav.Text = wrapper.FileName;
			csel = -1;
			pnflowcontainer.AutoScrollPosition = new System.Drawing.Point(0, 0);
			llcommit.Enabled = false;
			lldel.Enabled = false;
			llopenbhav.Enabled = false;
			CreateFlowPanel(wrapper.Instructions);
			btwiz.Enabled = false;
			tbargc.Text = wrapper.Header.ArgumentCount.ToString();
			tbflags.Text = "0x"+Helper.HexString(wrapper.Header.Flags);
			tbformat.Text = "0x"+Helper.HexString(wrapper.Header.Format);
			tblocals.Text = wrapper.Header.LocalVarCount.ToString();
			tbtype.Text = "0x"+Helper.HexString(wrapper.Header.Type);
			tbzero.Text = "0x"+Helper.HexString(wrapper.Header.Zero);
			internalchg = false;
		}		

		#endregion

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(BhavForm));
			this.llsort = new System.Windows.Forms.LinkLabel();
			this.label16 = new System.Windows.Forms.Label();
			this.lbbhav = new System.Windows.Forms.TextBox();
			this.pnflowcontainer = new System.Windows.Forms.Panel();
			this.pnflow1 = new System.Windows.Forms.PictureBox();
			this.pnflow2 = new System.Windows.Forms.PictureBox();
			this.gbopcodes = new System.Windows.Forms.GroupBox();
			this.label45 = new System.Windows.Forms.Label();
			this.llmove = new System.Windows.Forms.LinkLabel();
			this.tbmv = new System.Windows.Forms.TextBox();
			this.btwiz = new System.Windows.Forms.Button();
			this.llopenbhav = new System.Windows.Forms.LinkLabel();
			this.tba2 = new System.Windows.Forms.ComboBox();
			this.tba1 = new System.Windows.Forms.ComboBox();
			this.lldel = new System.Windows.Forms.LinkLabel();
			this.lladd = new System.Windows.Forms.LinkLabel();
			this.llcommit = new System.Windows.Forms.LinkLabel();
			this.label14 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.tbu7 = new System.Windows.Forms.TextBox();
			this.tbu6 = new System.Windows.Forms.TextBox();
			this.tbu5 = new System.Windows.Forms.TextBox();
			this.tbu4 = new System.Windows.Forms.TextBox();
			this.tbu3 = new System.Windows.Forms.TextBox();
			this.tbu2 = new System.Windows.Forms.TextBox();
			this.tbu1 = new System.Windows.Forms.TextBox();
			this.tbu0 = new System.Windows.Forms.TextBox();
			this.tbo7 = new System.Windows.Forms.TextBox();
			this.tbo6 = new System.Windows.Forms.TextBox();
			this.tbo5 = new System.Windows.Forms.TextBox();
			this.tbo4 = new System.Windows.Forms.TextBox();
			this.tbo3 = new System.Windows.Forms.TextBox();
			this.tbo2 = new System.Windows.Forms.TextBox();
			this.tbo1 = new System.Windows.Forms.TextBox();
			this.tbo0 = new System.Windows.Forms.TextBox();
			this.tbres = new System.Windows.Forms.TextBox();
			this.tbopcode = new System.Windows.Forms.TextBox();
			this.label12 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.button4 = new System.Windows.Forms.Button();
			this.lbtext = new System.Windows.Forms.TextBox();
			this.tbzero = new System.Windows.Forms.TextBox();
			this.tblocals = new System.Windows.Forms.TextBox();
			this.tbflags = new System.Windows.Forms.TextBox();
			this.tbargc = new System.Windows.Forms.TextBox();
			this.tbtype = new System.Windows.Forms.TextBox();
			this.tbformat = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.btcommit = new System.Windows.Forms.Button();
			this.panel3 = new System.Windows.Forms.Panel();
			this.label1 = new System.Windows.Forms.Label();
			this.cmcopy = new System.Windows.Forms.ContextMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.bhavPanel = new System.Windows.Forms.Panel();
			this.pnflowcontainer.SuspendLayout();
			this.gbopcodes.SuspendLayout();
			this.panel3.SuspendLayout();
			this.bhavPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// llsort
			// 
			this.llsort.AccessibleDescription = resources.GetString("llsort.AccessibleDescription");
			this.llsort.AccessibleName = resources.GetString("llsort.AccessibleName");
			this.llsort.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("llsort.Anchor")));
			this.llsort.AutoSize = ((bool)(resources.GetObject("llsort.AutoSize")));
			this.llsort.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("llsort.Dock")));
			this.llsort.Enabled = ((bool)(resources.GetObject("llsort.Enabled")));
			this.llsort.Font = ((System.Drawing.Font)(resources.GetObject("llsort.Font")));
			this.llsort.Image = ((System.Drawing.Image)(resources.GetObject("llsort.Image")));
			this.llsort.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("llsort.ImageAlign")));
			this.llsort.ImageIndex = ((int)(resources.GetObject("llsort.ImageIndex")));
			this.llsort.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("llsort.ImeMode")));
			this.llsort.LinkArea = ((System.Windows.Forms.LinkArea)(resources.GetObject("llsort.LinkArea")));
			this.llsort.Location = ((System.Drawing.Point)(resources.GetObject("llsort.Location")));
			this.llsort.Name = "llsort";
			this.llsort.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("llsort.RightToLeft")));
			this.llsort.Size = ((System.Drawing.Size)(resources.GetObject("llsort.Size")));
			this.llsort.TabIndex = ((int)(resources.GetObject("llsort.TabIndex")));
			this.llsort.TabStop = true;
			this.llsort.Text = resources.GetString("llsort.Text");
			this.llsort.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("llsort.TextAlign")));
			this.llsort.Visible = ((bool)(resources.GetObject("llsort.Visible")));
			this.llsort.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.SortInstructions);
			// 
			// label16
			// 
			this.label16.AccessibleDescription = resources.GetString("label16.AccessibleDescription");
			this.label16.AccessibleName = resources.GetString("label16.AccessibleName");
			this.label16.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label16.Anchor")));
			this.label16.AutoSize = ((bool)(resources.GetObject("label16.AutoSize")));
			this.label16.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label16.Dock")));
			this.label16.Enabled = ((bool)(resources.GetObject("label16.Enabled")));
			this.label16.Font = ((System.Drawing.Font)(resources.GetObject("label16.Font")));
			this.label16.Image = ((System.Drawing.Image)(resources.GetObject("label16.Image")));
			this.label16.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label16.ImageAlign")));
			this.label16.ImageIndex = ((int)(resources.GetObject("label16.ImageIndex")));
			this.label16.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label16.ImeMode")));
			this.label16.Location = ((System.Drawing.Point)(resources.GetObject("label16.Location")));
			this.label16.Name = "label16";
			this.label16.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label16.RightToLeft")));
			this.label16.Size = ((System.Drawing.Size)(resources.GetObject("label16.Size")));
			this.label16.TabIndex = ((int)(resources.GetObject("label16.TabIndex")));
			this.label16.Text = resources.GetString("label16.Text");
			this.label16.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label16.TextAlign")));
			this.label16.Visible = ((bool)(resources.GetObject("label16.Visible")));
			// 
			// lbbhav
			// 
			this.lbbhav.AccessibleDescription = resources.GetString("lbbhav.AccessibleDescription");
			this.lbbhav.AccessibleName = resources.GetString("lbbhav.AccessibleName");
			this.lbbhav.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbbhav.Anchor")));
			this.lbbhav.AutoSize = ((bool)(resources.GetObject("lbbhav.AutoSize")));
			this.lbbhav.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("lbbhav.BackgroundImage")));
			this.lbbhav.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbbhav.Dock")));
			this.lbbhav.Enabled = ((bool)(resources.GetObject("lbbhav.Enabled")));
			this.lbbhav.Font = ((System.Drawing.Font)(resources.GetObject("lbbhav.Font")));
			this.lbbhav.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbbhav.ImeMode")));
			this.lbbhav.Location = ((System.Drawing.Point)(resources.GetObject("lbbhav.Location")));
			this.lbbhav.MaxLength = ((int)(resources.GetObject("lbbhav.MaxLength")));
			this.lbbhav.Multiline = ((bool)(resources.GetObject("lbbhav.Multiline")));
			this.lbbhav.Name = "lbbhav";
			this.lbbhav.PasswordChar = ((char)(resources.GetObject("lbbhav.PasswordChar")));
			this.lbbhav.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbbhav.RightToLeft")));
			this.lbbhav.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("lbbhav.ScrollBars")));
			this.lbbhav.Size = ((System.Drawing.Size)(resources.GetObject("lbbhav.Size")));
			this.lbbhav.TabIndex = ((int)(resources.GetObject("lbbhav.TabIndex")));
			this.lbbhav.Text = resources.GetString("lbbhav.Text");
			this.lbbhav.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("lbbhav.TextAlign")));
			this.lbbhav.Visible = ((bool)(resources.GetObject("lbbhav.Visible")));
			this.lbbhav.WordWrap = ((bool)(resources.GetObject("lbbhav.WordWrap")));
			this.lbbhav.TextChanged += new System.EventHandler(this.AutoChangeBhav);
			// 
			// pnflowcontainer
			// 
			this.pnflowcontainer.AccessibleDescription = resources.GetString("pnflowcontainer.AccessibleDescription");
			this.pnflowcontainer.AccessibleName = resources.GetString("pnflowcontainer.AccessibleName");
			this.pnflowcontainer.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("pnflowcontainer.Anchor")));
			this.pnflowcontainer.AutoScroll = ((bool)(resources.GetObject("pnflowcontainer.AutoScroll")));
			this.pnflowcontainer.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("pnflowcontainer.AutoScrollMargin")));
			this.pnflowcontainer.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("pnflowcontainer.AutoScrollMinSize")));
			this.pnflowcontainer.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnflowcontainer.BackgroundImage")));
			this.pnflowcontainer.Controls.Add(this.pnflow1);
			this.pnflowcontainer.Controls.Add(this.pnflow2);
			this.pnflowcontainer.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("pnflowcontainer.Dock")));
			this.pnflowcontainer.Enabled = ((bool)(resources.GetObject("pnflowcontainer.Enabled")));
			this.pnflowcontainer.Font = ((System.Drawing.Font)(resources.GetObject("pnflowcontainer.Font")));
			this.pnflowcontainer.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("pnflowcontainer.ImeMode")));
			this.pnflowcontainer.Location = ((System.Drawing.Point)(resources.GetObject("pnflowcontainer.Location")));
			this.pnflowcontainer.Name = "pnflowcontainer";
			this.pnflowcontainer.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("pnflowcontainer.RightToLeft")));
			this.pnflowcontainer.Size = ((System.Drawing.Size)(resources.GetObject("pnflowcontainer.Size")));
			this.pnflowcontainer.TabIndex = ((int)(resources.GetObject("pnflowcontainer.TabIndex")));
			this.pnflowcontainer.Text = resources.GetString("pnflowcontainer.Text");
			this.pnflowcontainer.Visible = ((bool)(resources.GetObject("pnflowcontainer.Visible")));
			this.pnflowcontainer.Resize += new System.EventHandler(this.FlowResize);
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
			// gbopcodes
			// 
			this.gbopcodes.AccessibleDescription = resources.GetString("gbopcodes.AccessibleDescription");
			this.gbopcodes.AccessibleName = resources.GetString("gbopcodes.AccessibleName");
			this.gbopcodes.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("gbopcodes.Anchor")));
			this.gbopcodes.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("gbopcodes.BackgroundImage")));
			this.gbopcodes.Controls.Add(this.label45);
			this.gbopcodes.Controls.Add(this.llmove);
			this.gbopcodes.Controls.Add(this.tbmv);
			this.gbopcodes.Controls.Add(this.btwiz);
			this.gbopcodes.Controls.Add(this.llopenbhav);
			this.gbopcodes.Controls.Add(this.tba2);
			this.gbopcodes.Controls.Add(this.tba1);
			this.gbopcodes.Controls.Add(this.lldel);
			this.gbopcodes.Controls.Add(this.lladd);
			this.gbopcodes.Controls.Add(this.llcommit);
			this.gbopcodes.Controls.Add(this.label14);
			this.gbopcodes.Controls.Add(this.label13);
			this.gbopcodes.Controls.Add(this.tbu7);
			this.gbopcodes.Controls.Add(this.tbu6);
			this.gbopcodes.Controls.Add(this.tbu5);
			this.gbopcodes.Controls.Add(this.tbu4);
			this.gbopcodes.Controls.Add(this.tbu3);
			this.gbopcodes.Controls.Add(this.tbu2);
			this.gbopcodes.Controls.Add(this.tbu1);
			this.gbopcodes.Controls.Add(this.tbu0);
			this.gbopcodes.Controls.Add(this.tbo7);
			this.gbopcodes.Controls.Add(this.tbo6);
			this.gbopcodes.Controls.Add(this.tbo5);
			this.gbopcodes.Controls.Add(this.tbo4);
			this.gbopcodes.Controls.Add(this.tbo3);
			this.gbopcodes.Controls.Add(this.tbo2);
			this.gbopcodes.Controls.Add(this.tbo1);
			this.gbopcodes.Controls.Add(this.tbo0);
			this.gbopcodes.Controls.Add(this.tbres);
			this.gbopcodes.Controls.Add(this.tbopcode);
			this.gbopcodes.Controls.Add(this.label12);
			this.gbopcodes.Controls.Add(this.label11);
			this.gbopcodes.Controls.Add(this.label10);
			this.gbopcodes.Controls.Add(this.label9);
			this.gbopcodes.Controls.Add(this.button4);
			this.gbopcodes.Controls.Add(this.lbtext);
			this.gbopcodes.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("gbopcodes.Dock")));
			this.gbopcodes.Enabled = ((bool)(resources.GetObject("gbopcodes.Enabled")));
			this.gbopcodes.Font = ((System.Drawing.Font)(resources.GetObject("gbopcodes.Font")));
			this.gbopcodes.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("gbopcodes.ImeMode")));
			this.gbopcodes.Location = ((System.Drawing.Point)(resources.GetObject("gbopcodes.Location")));
			this.gbopcodes.Name = "gbopcodes";
			this.gbopcodes.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("gbopcodes.RightToLeft")));
			this.gbopcodes.Size = ((System.Drawing.Size)(resources.GetObject("gbopcodes.Size")));
			this.gbopcodes.TabIndex = ((int)(resources.GetObject("gbopcodes.TabIndex")));
			this.gbopcodes.TabStop = false;
			this.gbopcodes.Text = resources.GetString("gbopcodes.Text");
			this.gbopcodes.Visible = ((bool)(resources.GetObject("gbopcodes.Visible")));
			// 
			// label45
			// 
			this.label45.AccessibleDescription = resources.GetString("label45.AccessibleDescription");
			this.label45.AccessibleName = resources.GetString("label45.AccessibleName");
			this.label45.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label45.Anchor")));
			this.label45.AutoSize = ((bool)(resources.GetObject("label45.AutoSize")));
			this.label45.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label45.Dock")));
			this.label45.Enabled = ((bool)(resources.GetObject("label45.Enabled")));
			this.label45.Font = ((System.Drawing.Font)(resources.GetObject("label45.Font")));
			this.label45.Image = ((System.Drawing.Image)(resources.GetObject("label45.Image")));
			this.label45.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label45.ImageAlign")));
			this.label45.ImageIndex = ((int)(resources.GetObject("label45.ImageIndex")));
			this.label45.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label45.ImeMode")));
			this.label45.Location = ((System.Drawing.Point)(resources.GetObject("label45.Location")));
			this.label45.Name = "label45";
			this.label45.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label45.RightToLeft")));
			this.label45.Size = ((System.Drawing.Size)(resources.GetObject("label45.Size")));
			this.label45.TabIndex = ((int)(resources.GetObject("label45.TabIndex")));
			this.label45.Text = resources.GetString("label45.Text");
			this.label45.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label45.TextAlign")));
			this.label45.Visible = ((bool)(resources.GetObject("label45.Visible")));
			// 
			// llmove
			// 
			this.llmove.AccessibleDescription = resources.GetString("llmove.AccessibleDescription");
			this.llmove.AccessibleName = resources.GetString("llmove.AccessibleName");
			this.llmove.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("llmove.Anchor")));
			this.llmove.AutoSize = ((bool)(resources.GetObject("llmove.AutoSize")));
			this.llmove.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("llmove.Dock")));
			this.llmove.Enabled = ((bool)(resources.GetObject("llmove.Enabled")));
			this.llmove.Font = ((System.Drawing.Font)(resources.GetObject("llmove.Font")));
			this.llmove.Image = ((System.Drawing.Image)(resources.GetObject("llmove.Image")));
			this.llmove.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("llmove.ImageAlign")));
			this.llmove.ImageIndex = ((int)(resources.GetObject("llmove.ImageIndex")));
			this.llmove.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("llmove.ImeMode")));
			this.llmove.LinkArea = ((System.Windows.Forms.LinkArea)(resources.GetObject("llmove.LinkArea")));
			this.llmove.Location = ((System.Drawing.Point)(resources.GetObject("llmove.Location")));
			this.llmove.Name = "llmove";
			this.llmove.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("llmove.RightToLeft")));
			this.llmove.Size = ((System.Drawing.Size)(resources.GetObject("llmove.Size")));
			this.llmove.TabIndex = ((int)(resources.GetObject("llmove.TabIndex")));
			this.llmove.TabStop = true;
			this.llmove.Text = resources.GetString("llmove.Text");
			this.llmove.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("llmove.TextAlign")));
			this.llmove.Visible = ((bool)(resources.GetObject("llmove.Visible")));
			this.llmove.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llmove_LinkClicked);
			// 
			// tbmv
			// 
			this.tbmv.AccessibleDescription = resources.GetString("tbmv.AccessibleDescription");
			this.tbmv.AccessibleName = resources.GetString("tbmv.AccessibleName");
			this.tbmv.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbmv.Anchor")));
			this.tbmv.AutoSize = ((bool)(resources.GetObject("tbmv.AutoSize")));
			this.tbmv.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbmv.BackgroundImage")));
			this.tbmv.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbmv.Dock")));
			this.tbmv.Enabled = ((bool)(resources.GetObject("tbmv.Enabled")));
			this.tbmv.Font = ((System.Drawing.Font)(resources.GetObject("tbmv.Font")));
			this.tbmv.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbmv.ImeMode")));
			this.tbmv.Location = ((System.Drawing.Point)(resources.GetObject("tbmv.Location")));
			this.tbmv.MaxLength = ((int)(resources.GetObject("tbmv.MaxLength")));
			this.tbmv.Multiline = ((bool)(resources.GetObject("tbmv.Multiline")));
			this.tbmv.Name = "tbmv";
			this.tbmv.PasswordChar = ((char)(resources.GetObject("tbmv.PasswordChar")));
			this.tbmv.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbmv.RightToLeft")));
			this.tbmv.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbmv.ScrollBars")));
			this.tbmv.Size = ((System.Drawing.Size)(resources.GetObject("tbmv.Size")));
			this.tbmv.TabIndex = ((int)(resources.GetObject("tbmv.TabIndex")));
			this.tbmv.Text = resources.GetString("tbmv.Text");
			this.tbmv.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbmv.TextAlign")));
			this.tbmv.Visible = ((bool)(resources.GetObject("tbmv.Visible")));
			this.tbmv.WordWrap = ((bool)(resources.GetObject("tbmv.WordWrap")));
			this.tbmv.TextChanged += new System.EventHandler(this.tbmv_TextChanged);
			// 
			// btwiz
			// 
			this.btwiz.AccessibleDescription = resources.GetString("btwiz.AccessibleDescription");
			this.btwiz.AccessibleName = resources.GetString("btwiz.AccessibleName");
			this.btwiz.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btwiz.Anchor")));
			this.btwiz.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btwiz.BackgroundImage")));
			this.btwiz.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btwiz.Dock")));
			this.btwiz.Enabled = ((bool)(resources.GetObject("btwiz.Enabled")));
			this.btwiz.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btwiz.FlatStyle")));
			this.btwiz.Font = ((System.Drawing.Font)(resources.GetObject("btwiz.Font")));
			this.btwiz.Image = ((System.Drawing.Image)(resources.GetObject("btwiz.Image")));
			this.btwiz.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btwiz.ImageAlign")));
			this.btwiz.ImageIndex = ((int)(resources.GetObject("btwiz.ImageIndex")));
			this.btwiz.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btwiz.ImeMode")));
			this.btwiz.Location = ((System.Drawing.Point)(resources.GetObject("btwiz.Location")));
			this.btwiz.Name = "btwiz";
			this.btwiz.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btwiz.RightToLeft")));
			this.btwiz.Size = ((System.Drawing.Size)(resources.GetObject("btwiz.Size")));
			this.btwiz.TabIndex = ((int)(resources.GetObject("btwiz.TabIndex")));
			this.btwiz.Text = resources.GetString("btwiz.Text");
			this.btwiz.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btwiz.TextAlign")));
			this.btwiz.Visible = ((bool)(resources.GetObject("btwiz.Visible")));
			this.btwiz.Click += new System.EventHandler(this.OpenOperandWiz);
			// 
			// llopenbhav
			// 
			this.llopenbhav.AccessibleDescription = resources.GetString("llopenbhav.AccessibleDescription");
			this.llopenbhav.AccessibleName = resources.GetString("llopenbhav.AccessibleName");
			this.llopenbhav.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("llopenbhav.Anchor")));
			this.llopenbhav.AutoSize = ((bool)(resources.GetObject("llopenbhav.AutoSize")));
			this.llopenbhav.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("llopenbhav.Dock")));
			this.llopenbhav.Enabled = ((bool)(resources.GetObject("llopenbhav.Enabled")));
			this.llopenbhav.Font = ((System.Drawing.Font)(resources.GetObject("llopenbhav.Font")));
			this.llopenbhav.Image = ((System.Drawing.Image)(resources.GetObject("llopenbhav.Image")));
			this.llopenbhav.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("llopenbhav.ImageAlign")));
			this.llopenbhav.ImageIndex = ((int)(resources.GetObject("llopenbhav.ImageIndex")));
			this.llopenbhav.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("llopenbhav.ImeMode")));
			this.llopenbhav.LinkArea = ((System.Windows.Forms.LinkArea)(resources.GetObject("llopenbhav.LinkArea")));
			this.llopenbhav.Location = ((System.Drawing.Point)(resources.GetObject("llopenbhav.Location")));
			this.llopenbhav.Name = "llopenbhav";
			this.llopenbhav.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("llopenbhav.RightToLeft")));
			this.llopenbhav.Size = ((System.Drawing.Size)(resources.GetObject("llopenbhav.Size")));
			this.llopenbhav.TabIndex = ((int)(resources.GetObject("llopenbhav.TabIndex")));
			this.llopenbhav.TabStop = true;
			this.llopenbhav.Text = resources.GetString("llopenbhav.Text");
			this.llopenbhav.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("llopenbhav.TextAlign")));
			this.llopenbhav.Visible = ((bool)(resources.GetObject("llopenbhav.Visible")));
			this.llopenbhav.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.OpenBhavClicked);
			// 
			// tba2
			// 
			this.tba2.AccessibleDescription = resources.GetString("tba2.AccessibleDescription");
			this.tba2.AccessibleName = resources.GetString("tba2.AccessibleName");
			this.tba2.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tba2.Anchor")));
			this.tba2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tba2.BackgroundImage")));
			this.tba2.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tba2.Dock")));
			this.tba2.Enabled = ((bool)(resources.GetObject("tba2.Enabled")));
			this.tba2.Font = ((System.Drawing.Font)(resources.GetObject("tba2.Font")));
			this.tba2.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tba2.ImeMode")));
			this.tba2.IntegralHeight = ((bool)(resources.GetObject("tba2.IntegralHeight")));
			this.tba2.ItemHeight = ((int)(resources.GetObject("tba2.ItemHeight")));
			this.tba2.Location = ((System.Drawing.Point)(resources.GetObject("tba2.Location")));
			this.tba2.MaxDropDownItems = ((int)(resources.GetObject("tba2.MaxDropDownItems")));
			this.tba2.MaxLength = ((int)(resources.GetObject("tba2.MaxLength")));
			this.tba2.Name = "tba2";
			this.tba2.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tba2.RightToLeft")));
			this.tba2.Size = ((System.Drawing.Size)(resources.GetObject("tba2.Size")));
			this.tba2.TabIndex = ((int)(resources.GetObject("tba2.TabIndex")));
			this.tba2.Text = resources.GetString("tba2.Text");
			this.tba2.Visible = ((bool)(resources.GetObject("tba2.Visible")));
			this.tba2.DragDrop += new System.Windows.Forms.DragEventHandler(this.ItemDrop);
			this.tba2.TextChanged += new System.EventHandler(this.AutoChangePoiningInst);
			this.tba2.DragEnter += new System.Windows.Forms.DragEventHandler(this.ItemDragEnter);
			// 
			// tba1
			// 
			this.tba1.AccessibleDescription = resources.GetString("tba1.AccessibleDescription");
			this.tba1.AccessibleName = resources.GetString("tba1.AccessibleName");
			this.tba1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tba1.Anchor")));
			this.tba1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tba1.BackgroundImage")));
			this.tba1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tba1.Dock")));
			this.tba1.Enabled = ((bool)(resources.GetObject("tba1.Enabled")));
			this.tba1.Font = ((System.Drawing.Font)(resources.GetObject("tba1.Font")));
			this.tba1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tba1.ImeMode")));
			this.tba1.IntegralHeight = ((bool)(resources.GetObject("tba1.IntegralHeight")));
			this.tba1.ItemHeight = ((int)(resources.GetObject("tba1.ItemHeight")));
			this.tba1.Location = ((System.Drawing.Point)(resources.GetObject("tba1.Location")));
			this.tba1.MaxDropDownItems = ((int)(resources.GetObject("tba1.MaxDropDownItems")));
			this.tba1.MaxLength = ((int)(resources.GetObject("tba1.MaxLength")));
			this.tba1.Name = "tba1";
			this.tba1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tba1.RightToLeft")));
			this.tba1.Size = ((System.Drawing.Size)(resources.GetObject("tba1.Size")));
			this.tba1.TabIndex = ((int)(resources.GetObject("tba1.TabIndex")));
			this.tba1.Text = resources.GetString("tba1.Text");
			this.tba1.Visible = ((bool)(resources.GetObject("tba1.Visible")));
			this.tba1.DragOver += new System.Windows.Forms.DragEventHandler(this.ItemDragEnter);
			this.tba1.DragDrop += new System.Windows.Forms.DragEventHandler(this.ItemDrop);
			this.tba1.TextChanged += new System.EventHandler(this.AutoChangePoiningInst);
			this.tba1.QueryContinueDrag += new System.Windows.Forms.QueryContinueDragEventHandler(this.ItemQueryContinueDragTarget);
			this.tba1.DragEnter += new System.Windows.Forms.DragEventHandler(this.ItemDragEnter);
			// 
			// lldel
			// 
			this.lldel.AccessibleDescription = resources.GetString("lldel.AccessibleDescription");
			this.lldel.AccessibleName = resources.GetString("lldel.AccessibleName");
			this.lldel.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lldel.Anchor")));
			this.lldel.AutoSize = ((bool)(resources.GetObject("lldel.AutoSize")));
			this.lldel.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lldel.Dock")));
			this.lldel.Enabled = ((bool)(resources.GetObject("lldel.Enabled")));
			this.lldel.Font = ((System.Drawing.Font)(resources.GetObject("lldel.Font")));
			this.lldel.Image = ((System.Drawing.Image)(resources.GetObject("lldel.Image")));
			this.lldel.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lldel.ImageAlign")));
			this.lldel.ImageIndex = ((int)(resources.GetObject("lldel.ImageIndex")));
			this.lldel.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lldel.ImeMode")));
			this.lldel.LinkArea = ((System.Windows.Forms.LinkArea)(resources.GetObject("lldel.LinkArea")));
			this.lldel.Location = ((System.Drawing.Point)(resources.GetObject("lldel.Location")));
			this.lldel.Name = "lldel";
			this.lldel.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lldel.RightToLeft")));
			this.lldel.Size = ((System.Drawing.Size)(resources.GetObject("lldel.Size")));
			this.lldel.TabIndex = ((int)(resources.GetObject("lldel.TabIndex")));
			this.lldel.TabStop = true;
			this.lldel.Text = resources.GetString("lldel.Text");
			this.lldel.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lldel.TextAlign")));
			this.lldel.Visible = ((bool)(resources.GetObject("lldel.Visible")));
			this.lldel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.DeleteOpcodeClicked);
			// 
			// lladd
			// 
			this.lladd.AccessibleDescription = resources.GetString("lladd.AccessibleDescription");
			this.lladd.AccessibleName = resources.GetString("lladd.AccessibleName");
			this.lladd.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lladd.Anchor")));
			this.lladd.AutoSize = ((bool)(resources.GetObject("lladd.AutoSize")));
			this.lladd.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lladd.Dock")));
			this.lladd.Enabled = ((bool)(resources.GetObject("lladd.Enabled")));
			this.lladd.Font = ((System.Drawing.Font)(resources.GetObject("lladd.Font")));
			this.lladd.Image = ((System.Drawing.Image)(resources.GetObject("lladd.Image")));
			this.lladd.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lladd.ImageAlign")));
			this.lladd.ImageIndex = ((int)(resources.GetObject("lladd.ImageIndex")));
			this.lladd.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lladd.ImeMode")));
			this.lladd.LinkArea = ((System.Windows.Forms.LinkArea)(resources.GetObject("lladd.LinkArea")));
			this.lladd.Location = ((System.Drawing.Point)(resources.GetObject("lladd.Location")));
			this.lladd.Name = "lladd";
			this.lladd.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lladd.RightToLeft")));
			this.lladd.Size = ((System.Drawing.Size)(resources.GetObject("lladd.Size")));
			this.lladd.TabIndex = ((int)(resources.GetObject("lladd.TabIndex")));
			this.lladd.TabStop = true;
			this.lladd.Text = resources.GetString("lladd.Text");
			this.lladd.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lladd.TextAlign")));
			this.lladd.Visible = ((bool)(resources.GetObject("lladd.Visible")));
			this.lladd.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.AddOpcodeClicked);
			// 
			// llcommit
			// 
			this.llcommit.AccessibleDescription = resources.GetString("llcommit.AccessibleDescription");
			this.llcommit.AccessibleName = resources.GetString("llcommit.AccessibleName");
			this.llcommit.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("llcommit.Anchor")));
			this.llcommit.AutoSize = ((bool)(resources.GetObject("llcommit.AutoSize")));
			this.llcommit.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("llcommit.Dock")));
			this.llcommit.Enabled = ((bool)(resources.GetObject("llcommit.Enabled")));
			this.llcommit.Font = ((System.Drawing.Font)(resources.GetObject("llcommit.Font")));
			this.llcommit.Image = ((System.Drawing.Image)(resources.GetObject("llcommit.Image")));
			this.llcommit.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("llcommit.ImageAlign")));
			this.llcommit.ImageIndex = ((int)(resources.GetObject("llcommit.ImageIndex")));
			this.llcommit.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("llcommit.ImeMode")));
			this.llcommit.LinkArea = ((System.Windows.Forms.LinkArea)(resources.GetObject("llcommit.LinkArea")));
			this.llcommit.Location = ((System.Drawing.Point)(resources.GetObject("llcommit.Location")));
			this.llcommit.Name = "llcommit";
			this.llcommit.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("llcommit.RightToLeft")));
			this.llcommit.Size = ((System.Drawing.Size)(resources.GetObject("llcommit.Size")));
			this.llcommit.TabIndex = ((int)(resources.GetObject("llcommit.TabIndex")));
			this.llcommit.TabStop = true;
			this.llcommit.Text = resources.GetString("llcommit.Text");
			this.llcommit.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("llcommit.TextAlign")));
			this.llcommit.Visible = ((bool)(resources.GetObject("llcommit.Visible")));
			this.llcommit.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.CommitOpcodeClicked);
			// 
			// label14
			// 
			this.label14.AccessibleDescription = resources.GetString("label14.AccessibleDescription");
			this.label14.AccessibleName = resources.GetString("label14.AccessibleName");
			this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label14.Anchor")));
			this.label14.AutoSize = ((bool)(resources.GetObject("label14.AutoSize")));
			this.label14.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label14.Dock")));
			this.label14.Enabled = ((bool)(resources.GetObject("label14.Enabled")));
			this.label14.Font = ((System.Drawing.Font)(resources.GetObject("label14.Font")));
			this.label14.Image = ((System.Drawing.Image)(resources.GetObject("label14.Image")));
			this.label14.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label14.ImageAlign")));
			this.label14.ImageIndex = ((int)(resources.GetObject("label14.ImageIndex")));
			this.label14.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label14.ImeMode")));
			this.label14.Location = ((System.Drawing.Point)(resources.GetObject("label14.Location")));
			this.label14.Name = "label14";
			this.label14.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label14.RightToLeft")));
			this.label14.Size = ((System.Drawing.Size)(resources.GetObject("label14.Size")));
			this.label14.TabIndex = ((int)(resources.GetObject("label14.TabIndex")));
			this.label14.Text = resources.GetString("label14.Text");
			this.label14.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label14.TextAlign")));
			this.label14.Visible = ((bool)(resources.GetObject("label14.Visible")));
			// 
			// label13
			// 
			this.label13.AccessibleDescription = resources.GetString("label13.AccessibleDescription");
			this.label13.AccessibleName = resources.GetString("label13.AccessibleName");
			this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label13.Anchor")));
			this.label13.AutoSize = ((bool)(resources.GetObject("label13.AutoSize")));
			this.label13.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label13.Dock")));
			this.label13.Enabled = ((bool)(resources.GetObject("label13.Enabled")));
			this.label13.Font = ((System.Drawing.Font)(resources.GetObject("label13.Font")));
			this.label13.Image = ((System.Drawing.Image)(resources.GetObject("label13.Image")));
			this.label13.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label13.ImageAlign")));
			this.label13.ImageIndex = ((int)(resources.GetObject("label13.ImageIndex")));
			this.label13.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label13.ImeMode")));
			this.label13.Location = ((System.Drawing.Point)(resources.GetObject("label13.Location")));
			this.label13.Name = "label13";
			this.label13.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label13.RightToLeft")));
			this.label13.Size = ((System.Drawing.Size)(resources.GetObject("label13.Size")));
			this.label13.TabIndex = ((int)(resources.GetObject("label13.TabIndex")));
			this.label13.Text = resources.GetString("label13.Text");
			this.label13.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label13.TextAlign")));
			this.label13.Visible = ((bool)(resources.GetObject("label13.Visible")));
			// 
			// tbu7
			// 
			this.tbu7.AccessibleDescription = resources.GetString("tbu7.AccessibleDescription");
			this.tbu7.AccessibleName = resources.GetString("tbu7.AccessibleName");
			this.tbu7.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbu7.Anchor")));
			this.tbu7.AutoSize = ((bool)(resources.GetObject("tbu7.AutoSize")));
			this.tbu7.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbu7.BackgroundImage")));
			this.tbu7.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbu7.Dock")));
			this.tbu7.Enabled = ((bool)(resources.GetObject("tbu7.Enabled")));
			this.tbu7.Font = ((System.Drawing.Font)(resources.GetObject("tbu7.Font")));
			this.tbu7.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbu7.ImeMode")));
			this.tbu7.Location = ((System.Drawing.Point)(resources.GetObject("tbu7.Location")));
			this.tbu7.MaxLength = ((int)(resources.GetObject("tbu7.MaxLength")));
			this.tbu7.Multiline = ((bool)(resources.GetObject("tbu7.Multiline")));
			this.tbu7.Name = "tbu7";
			this.tbu7.PasswordChar = ((char)(resources.GetObject("tbu7.PasswordChar")));
			this.tbu7.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbu7.RightToLeft")));
			this.tbu7.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbu7.ScrollBars")));
			this.tbu7.Size = ((System.Drawing.Size)(resources.GetObject("tbu7.Size")));
			this.tbu7.TabIndex = ((int)(resources.GetObject("tbu7.TabIndex")));
			this.tbu7.Text = resources.GetString("tbu7.Text");
			this.tbu7.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbu7.TextAlign")));
			this.tbu7.Visible = ((bool)(resources.GetObject("tbu7.Visible")));
			this.tbu7.WordWrap = ((bool)(resources.GetObject("tbu7.WordWrap")));
			this.tbu7.TextChanged += new System.EventHandler(this.AutoChangeInst);
			// 
			// tbu6
			// 
			this.tbu6.AccessibleDescription = resources.GetString("tbu6.AccessibleDescription");
			this.tbu6.AccessibleName = resources.GetString("tbu6.AccessibleName");
			this.tbu6.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbu6.Anchor")));
			this.tbu6.AutoSize = ((bool)(resources.GetObject("tbu6.AutoSize")));
			this.tbu6.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbu6.BackgroundImage")));
			this.tbu6.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbu6.Dock")));
			this.tbu6.Enabled = ((bool)(resources.GetObject("tbu6.Enabled")));
			this.tbu6.Font = ((System.Drawing.Font)(resources.GetObject("tbu6.Font")));
			this.tbu6.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbu6.ImeMode")));
			this.tbu6.Location = ((System.Drawing.Point)(resources.GetObject("tbu6.Location")));
			this.tbu6.MaxLength = ((int)(resources.GetObject("tbu6.MaxLength")));
			this.tbu6.Multiline = ((bool)(resources.GetObject("tbu6.Multiline")));
			this.tbu6.Name = "tbu6";
			this.tbu6.PasswordChar = ((char)(resources.GetObject("tbu6.PasswordChar")));
			this.tbu6.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbu6.RightToLeft")));
			this.tbu6.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbu6.ScrollBars")));
			this.tbu6.Size = ((System.Drawing.Size)(resources.GetObject("tbu6.Size")));
			this.tbu6.TabIndex = ((int)(resources.GetObject("tbu6.TabIndex")));
			this.tbu6.Text = resources.GetString("tbu6.Text");
			this.tbu6.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbu6.TextAlign")));
			this.tbu6.Visible = ((bool)(resources.GetObject("tbu6.Visible")));
			this.tbu6.WordWrap = ((bool)(resources.GetObject("tbu6.WordWrap")));
			this.tbu6.TextChanged += new System.EventHandler(this.AutoChangeInst);
			// 
			// tbu5
			// 
			this.tbu5.AccessibleDescription = resources.GetString("tbu5.AccessibleDescription");
			this.tbu5.AccessibleName = resources.GetString("tbu5.AccessibleName");
			this.tbu5.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbu5.Anchor")));
			this.tbu5.AutoSize = ((bool)(resources.GetObject("tbu5.AutoSize")));
			this.tbu5.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbu5.BackgroundImage")));
			this.tbu5.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbu5.Dock")));
			this.tbu5.Enabled = ((bool)(resources.GetObject("tbu5.Enabled")));
			this.tbu5.Font = ((System.Drawing.Font)(resources.GetObject("tbu5.Font")));
			this.tbu5.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbu5.ImeMode")));
			this.tbu5.Location = ((System.Drawing.Point)(resources.GetObject("tbu5.Location")));
			this.tbu5.MaxLength = ((int)(resources.GetObject("tbu5.MaxLength")));
			this.tbu5.Multiline = ((bool)(resources.GetObject("tbu5.Multiline")));
			this.tbu5.Name = "tbu5";
			this.tbu5.PasswordChar = ((char)(resources.GetObject("tbu5.PasswordChar")));
			this.tbu5.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbu5.RightToLeft")));
			this.tbu5.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbu5.ScrollBars")));
			this.tbu5.Size = ((System.Drawing.Size)(resources.GetObject("tbu5.Size")));
			this.tbu5.TabIndex = ((int)(resources.GetObject("tbu5.TabIndex")));
			this.tbu5.Text = resources.GetString("tbu5.Text");
			this.tbu5.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbu5.TextAlign")));
			this.tbu5.Visible = ((bool)(resources.GetObject("tbu5.Visible")));
			this.tbu5.WordWrap = ((bool)(resources.GetObject("tbu5.WordWrap")));
			this.tbu5.TextChanged += new System.EventHandler(this.AutoChangeInst);
			// 
			// tbu4
			// 
			this.tbu4.AccessibleDescription = resources.GetString("tbu4.AccessibleDescription");
			this.tbu4.AccessibleName = resources.GetString("tbu4.AccessibleName");
			this.tbu4.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbu4.Anchor")));
			this.tbu4.AutoSize = ((bool)(resources.GetObject("tbu4.AutoSize")));
			this.tbu4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbu4.BackgroundImage")));
			this.tbu4.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbu4.Dock")));
			this.tbu4.Enabled = ((bool)(resources.GetObject("tbu4.Enabled")));
			this.tbu4.Font = ((System.Drawing.Font)(resources.GetObject("tbu4.Font")));
			this.tbu4.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbu4.ImeMode")));
			this.tbu4.Location = ((System.Drawing.Point)(resources.GetObject("tbu4.Location")));
			this.tbu4.MaxLength = ((int)(resources.GetObject("tbu4.MaxLength")));
			this.tbu4.Multiline = ((bool)(resources.GetObject("tbu4.Multiline")));
			this.tbu4.Name = "tbu4";
			this.tbu4.PasswordChar = ((char)(resources.GetObject("tbu4.PasswordChar")));
			this.tbu4.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbu4.RightToLeft")));
			this.tbu4.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbu4.ScrollBars")));
			this.tbu4.Size = ((System.Drawing.Size)(resources.GetObject("tbu4.Size")));
			this.tbu4.TabIndex = ((int)(resources.GetObject("tbu4.TabIndex")));
			this.tbu4.Text = resources.GetString("tbu4.Text");
			this.tbu4.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbu4.TextAlign")));
			this.tbu4.Visible = ((bool)(resources.GetObject("tbu4.Visible")));
			this.tbu4.WordWrap = ((bool)(resources.GetObject("tbu4.WordWrap")));
			this.tbu4.TextChanged += new System.EventHandler(this.AutoChangeInst);
			// 
			// tbu3
			// 
			this.tbu3.AccessibleDescription = resources.GetString("tbu3.AccessibleDescription");
			this.tbu3.AccessibleName = resources.GetString("tbu3.AccessibleName");
			this.tbu3.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbu3.Anchor")));
			this.tbu3.AutoSize = ((bool)(resources.GetObject("tbu3.AutoSize")));
			this.tbu3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbu3.BackgroundImage")));
			this.tbu3.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbu3.Dock")));
			this.tbu3.Enabled = ((bool)(resources.GetObject("tbu3.Enabled")));
			this.tbu3.Font = ((System.Drawing.Font)(resources.GetObject("tbu3.Font")));
			this.tbu3.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbu3.ImeMode")));
			this.tbu3.Location = ((System.Drawing.Point)(resources.GetObject("tbu3.Location")));
			this.tbu3.MaxLength = ((int)(resources.GetObject("tbu3.MaxLength")));
			this.tbu3.Multiline = ((bool)(resources.GetObject("tbu3.Multiline")));
			this.tbu3.Name = "tbu3";
			this.tbu3.PasswordChar = ((char)(resources.GetObject("tbu3.PasswordChar")));
			this.tbu3.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbu3.RightToLeft")));
			this.tbu3.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbu3.ScrollBars")));
			this.tbu3.Size = ((System.Drawing.Size)(resources.GetObject("tbu3.Size")));
			this.tbu3.TabIndex = ((int)(resources.GetObject("tbu3.TabIndex")));
			this.tbu3.Text = resources.GetString("tbu3.Text");
			this.tbu3.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbu3.TextAlign")));
			this.tbu3.Visible = ((bool)(resources.GetObject("tbu3.Visible")));
			this.tbu3.WordWrap = ((bool)(resources.GetObject("tbu3.WordWrap")));
			this.tbu3.TextChanged += new System.EventHandler(this.AutoChangeInst);
			// 
			// tbu2
			// 
			this.tbu2.AccessibleDescription = resources.GetString("tbu2.AccessibleDescription");
			this.tbu2.AccessibleName = resources.GetString("tbu2.AccessibleName");
			this.tbu2.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbu2.Anchor")));
			this.tbu2.AutoSize = ((bool)(resources.GetObject("tbu2.AutoSize")));
			this.tbu2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbu2.BackgroundImage")));
			this.tbu2.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbu2.Dock")));
			this.tbu2.Enabled = ((bool)(resources.GetObject("tbu2.Enabled")));
			this.tbu2.Font = ((System.Drawing.Font)(resources.GetObject("tbu2.Font")));
			this.tbu2.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbu2.ImeMode")));
			this.tbu2.Location = ((System.Drawing.Point)(resources.GetObject("tbu2.Location")));
			this.tbu2.MaxLength = ((int)(resources.GetObject("tbu2.MaxLength")));
			this.tbu2.Multiline = ((bool)(resources.GetObject("tbu2.Multiline")));
			this.tbu2.Name = "tbu2";
			this.tbu2.PasswordChar = ((char)(resources.GetObject("tbu2.PasswordChar")));
			this.tbu2.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbu2.RightToLeft")));
			this.tbu2.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbu2.ScrollBars")));
			this.tbu2.Size = ((System.Drawing.Size)(resources.GetObject("tbu2.Size")));
			this.tbu2.TabIndex = ((int)(resources.GetObject("tbu2.TabIndex")));
			this.tbu2.Text = resources.GetString("tbu2.Text");
			this.tbu2.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbu2.TextAlign")));
			this.tbu2.Visible = ((bool)(resources.GetObject("tbu2.Visible")));
			this.tbu2.WordWrap = ((bool)(resources.GetObject("tbu2.WordWrap")));
			this.tbu2.TextChanged += new System.EventHandler(this.AutoChangeInst);
			// 
			// tbu1
			// 
			this.tbu1.AccessibleDescription = resources.GetString("tbu1.AccessibleDescription");
			this.tbu1.AccessibleName = resources.GetString("tbu1.AccessibleName");
			this.tbu1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbu1.Anchor")));
			this.tbu1.AutoSize = ((bool)(resources.GetObject("tbu1.AutoSize")));
			this.tbu1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbu1.BackgroundImage")));
			this.tbu1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbu1.Dock")));
			this.tbu1.Enabled = ((bool)(resources.GetObject("tbu1.Enabled")));
			this.tbu1.Font = ((System.Drawing.Font)(resources.GetObject("tbu1.Font")));
			this.tbu1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbu1.ImeMode")));
			this.tbu1.Location = ((System.Drawing.Point)(resources.GetObject("tbu1.Location")));
			this.tbu1.MaxLength = ((int)(resources.GetObject("tbu1.MaxLength")));
			this.tbu1.Multiline = ((bool)(resources.GetObject("tbu1.Multiline")));
			this.tbu1.Name = "tbu1";
			this.tbu1.PasswordChar = ((char)(resources.GetObject("tbu1.PasswordChar")));
			this.tbu1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbu1.RightToLeft")));
			this.tbu1.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbu1.ScrollBars")));
			this.tbu1.Size = ((System.Drawing.Size)(resources.GetObject("tbu1.Size")));
			this.tbu1.TabIndex = ((int)(resources.GetObject("tbu1.TabIndex")));
			this.tbu1.Text = resources.GetString("tbu1.Text");
			this.tbu1.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbu1.TextAlign")));
			this.tbu1.Visible = ((bool)(resources.GetObject("tbu1.Visible")));
			this.tbu1.WordWrap = ((bool)(resources.GetObject("tbu1.WordWrap")));
			this.tbu1.TextChanged += new System.EventHandler(this.AutoChangeInst);
			// 
			// tbu0
			// 
			this.tbu0.AccessibleDescription = resources.GetString("tbu0.AccessibleDescription");
			this.tbu0.AccessibleName = resources.GetString("tbu0.AccessibleName");
			this.tbu0.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbu0.Anchor")));
			this.tbu0.AutoSize = ((bool)(resources.GetObject("tbu0.AutoSize")));
			this.tbu0.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbu0.BackgroundImage")));
			this.tbu0.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbu0.Dock")));
			this.tbu0.Enabled = ((bool)(resources.GetObject("tbu0.Enabled")));
			this.tbu0.Font = ((System.Drawing.Font)(resources.GetObject("tbu0.Font")));
			this.tbu0.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbu0.ImeMode")));
			this.tbu0.Location = ((System.Drawing.Point)(resources.GetObject("tbu0.Location")));
			this.tbu0.MaxLength = ((int)(resources.GetObject("tbu0.MaxLength")));
			this.tbu0.Multiline = ((bool)(resources.GetObject("tbu0.Multiline")));
			this.tbu0.Name = "tbu0";
			this.tbu0.PasswordChar = ((char)(resources.GetObject("tbu0.PasswordChar")));
			this.tbu0.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbu0.RightToLeft")));
			this.tbu0.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbu0.ScrollBars")));
			this.tbu0.Size = ((System.Drawing.Size)(resources.GetObject("tbu0.Size")));
			this.tbu0.TabIndex = ((int)(resources.GetObject("tbu0.TabIndex")));
			this.tbu0.Text = resources.GetString("tbu0.Text");
			this.tbu0.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbu0.TextAlign")));
			this.tbu0.Visible = ((bool)(resources.GetObject("tbu0.Visible")));
			this.tbu0.WordWrap = ((bool)(resources.GetObject("tbu0.WordWrap")));
			this.tbu0.TextChanged += new System.EventHandler(this.AutoChangeInst);
			// 
			// tbo7
			// 
			this.tbo7.AccessibleDescription = resources.GetString("tbo7.AccessibleDescription");
			this.tbo7.AccessibleName = resources.GetString("tbo7.AccessibleName");
			this.tbo7.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbo7.Anchor")));
			this.tbo7.AutoSize = ((bool)(resources.GetObject("tbo7.AutoSize")));
			this.tbo7.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbo7.BackgroundImage")));
			this.tbo7.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbo7.Dock")));
			this.tbo7.Enabled = ((bool)(resources.GetObject("tbo7.Enabled")));
			this.tbo7.Font = ((System.Drawing.Font)(resources.GetObject("tbo7.Font")));
			this.tbo7.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbo7.ImeMode")));
			this.tbo7.Location = ((System.Drawing.Point)(resources.GetObject("tbo7.Location")));
			this.tbo7.MaxLength = ((int)(resources.GetObject("tbo7.MaxLength")));
			this.tbo7.Multiline = ((bool)(resources.GetObject("tbo7.Multiline")));
			this.tbo7.Name = "tbo7";
			this.tbo7.PasswordChar = ((char)(resources.GetObject("tbo7.PasswordChar")));
			this.tbo7.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbo7.RightToLeft")));
			this.tbo7.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbo7.ScrollBars")));
			this.tbo7.Size = ((System.Drawing.Size)(resources.GetObject("tbo7.Size")));
			this.tbo7.TabIndex = ((int)(resources.GetObject("tbo7.TabIndex")));
			this.tbo7.Text = resources.GetString("tbo7.Text");
			this.tbo7.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbo7.TextAlign")));
			this.tbo7.Visible = ((bool)(resources.GetObject("tbo7.Visible")));
			this.tbo7.WordWrap = ((bool)(resources.GetObject("tbo7.WordWrap")));
			this.tbo7.TextChanged += new System.EventHandler(this.AutoChangeInst);
			// 
			// tbo6
			// 
			this.tbo6.AccessibleDescription = resources.GetString("tbo6.AccessibleDescription");
			this.tbo6.AccessibleName = resources.GetString("tbo6.AccessibleName");
			this.tbo6.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbo6.Anchor")));
			this.tbo6.AutoSize = ((bool)(resources.GetObject("tbo6.AutoSize")));
			this.tbo6.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbo6.BackgroundImage")));
			this.tbo6.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbo6.Dock")));
			this.tbo6.Enabled = ((bool)(resources.GetObject("tbo6.Enabled")));
			this.tbo6.Font = ((System.Drawing.Font)(resources.GetObject("tbo6.Font")));
			this.tbo6.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbo6.ImeMode")));
			this.tbo6.Location = ((System.Drawing.Point)(resources.GetObject("tbo6.Location")));
			this.tbo6.MaxLength = ((int)(resources.GetObject("tbo6.MaxLength")));
			this.tbo6.Multiline = ((bool)(resources.GetObject("tbo6.Multiline")));
			this.tbo6.Name = "tbo6";
			this.tbo6.PasswordChar = ((char)(resources.GetObject("tbo6.PasswordChar")));
			this.tbo6.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbo6.RightToLeft")));
			this.tbo6.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbo6.ScrollBars")));
			this.tbo6.Size = ((System.Drawing.Size)(resources.GetObject("tbo6.Size")));
			this.tbo6.TabIndex = ((int)(resources.GetObject("tbo6.TabIndex")));
			this.tbo6.Text = resources.GetString("tbo6.Text");
			this.tbo6.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbo6.TextAlign")));
			this.tbo6.Visible = ((bool)(resources.GetObject("tbo6.Visible")));
			this.tbo6.WordWrap = ((bool)(resources.GetObject("tbo6.WordWrap")));
			this.tbo6.TextChanged += new System.EventHandler(this.AutoChangeInst);
			// 
			// tbo5
			// 
			this.tbo5.AccessibleDescription = resources.GetString("tbo5.AccessibleDescription");
			this.tbo5.AccessibleName = resources.GetString("tbo5.AccessibleName");
			this.tbo5.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbo5.Anchor")));
			this.tbo5.AutoSize = ((bool)(resources.GetObject("tbo5.AutoSize")));
			this.tbo5.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbo5.BackgroundImage")));
			this.tbo5.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbo5.Dock")));
			this.tbo5.Enabled = ((bool)(resources.GetObject("tbo5.Enabled")));
			this.tbo5.Font = ((System.Drawing.Font)(resources.GetObject("tbo5.Font")));
			this.tbo5.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbo5.ImeMode")));
			this.tbo5.Location = ((System.Drawing.Point)(resources.GetObject("tbo5.Location")));
			this.tbo5.MaxLength = ((int)(resources.GetObject("tbo5.MaxLength")));
			this.tbo5.Multiline = ((bool)(resources.GetObject("tbo5.Multiline")));
			this.tbo5.Name = "tbo5";
			this.tbo5.PasswordChar = ((char)(resources.GetObject("tbo5.PasswordChar")));
			this.tbo5.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbo5.RightToLeft")));
			this.tbo5.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbo5.ScrollBars")));
			this.tbo5.Size = ((System.Drawing.Size)(resources.GetObject("tbo5.Size")));
			this.tbo5.TabIndex = ((int)(resources.GetObject("tbo5.TabIndex")));
			this.tbo5.Text = resources.GetString("tbo5.Text");
			this.tbo5.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbo5.TextAlign")));
			this.tbo5.Visible = ((bool)(resources.GetObject("tbo5.Visible")));
			this.tbo5.WordWrap = ((bool)(resources.GetObject("tbo5.WordWrap")));
			this.tbo5.TextChanged += new System.EventHandler(this.AutoChangeInst);
			// 
			// tbo4
			// 
			this.tbo4.AccessibleDescription = resources.GetString("tbo4.AccessibleDescription");
			this.tbo4.AccessibleName = resources.GetString("tbo4.AccessibleName");
			this.tbo4.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbo4.Anchor")));
			this.tbo4.AutoSize = ((bool)(resources.GetObject("tbo4.AutoSize")));
			this.tbo4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbo4.BackgroundImage")));
			this.tbo4.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbo4.Dock")));
			this.tbo4.Enabled = ((bool)(resources.GetObject("tbo4.Enabled")));
			this.tbo4.Font = ((System.Drawing.Font)(resources.GetObject("tbo4.Font")));
			this.tbo4.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbo4.ImeMode")));
			this.tbo4.Location = ((System.Drawing.Point)(resources.GetObject("tbo4.Location")));
			this.tbo4.MaxLength = ((int)(resources.GetObject("tbo4.MaxLength")));
			this.tbo4.Multiline = ((bool)(resources.GetObject("tbo4.Multiline")));
			this.tbo4.Name = "tbo4";
			this.tbo4.PasswordChar = ((char)(resources.GetObject("tbo4.PasswordChar")));
			this.tbo4.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbo4.RightToLeft")));
			this.tbo4.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbo4.ScrollBars")));
			this.tbo4.Size = ((System.Drawing.Size)(resources.GetObject("tbo4.Size")));
			this.tbo4.TabIndex = ((int)(resources.GetObject("tbo4.TabIndex")));
			this.tbo4.Text = resources.GetString("tbo4.Text");
			this.tbo4.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbo4.TextAlign")));
			this.tbo4.Visible = ((bool)(resources.GetObject("tbo4.Visible")));
			this.tbo4.WordWrap = ((bool)(resources.GetObject("tbo4.WordWrap")));
			this.tbo4.TextChanged += new System.EventHandler(this.AutoChangeInst);
			// 
			// tbo3
			// 
			this.tbo3.AccessibleDescription = resources.GetString("tbo3.AccessibleDescription");
			this.tbo3.AccessibleName = resources.GetString("tbo3.AccessibleName");
			this.tbo3.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbo3.Anchor")));
			this.tbo3.AutoSize = ((bool)(resources.GetObject("tbo3.AutoSize")));
			this.tbo3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbo3.BackgroundImage")));
			this.tbo3.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbo3.Dock")));
			this.tbo3.Enabled = ((bool)(resources.GetObject("tbo3.Enabled")));
			this.tbo3.Font = ((System.Drawing.Font)(resources.GetObject("tbo3.Font")));
			this.tbo3.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbo3.ImeMode")));
			this.tbo3.Location = ((System.Drawing.Point)(resources.GetObject("tbo3.Location")));
			this.tbo3.MaxLength = ((int)(resources.GetObject("tbo3.MaxLength")));
			this.tbo3.Multiline = ((bool)(resources.GetObject("tbo3.Multiline")));
			this.tbo3.Name = "tbo3";
			this.tbo3.PasswordChar = ((char)(resources.GetObject("tbo3.PasswordChar")));
			this.tbo3.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbo3.RightToLeft")));
			this.tbo3.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbo3.ScrollBars")));
			this.tbo3.Size = ((System.Drawing.Size)(resources.GetObject("tbo3.Size")));
			this.tbo3.TabIndex = ((int)(resources.GetObject("tbo3.TabIndex")));
			this.tbo3.Text = resources.GetString("tbo3.Text");
			this.tbo3.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbo3.TextAlign")));
			this.tbo3.Visible = ((bool)(resources.GetObject("tbo3.Visible")));
			this.tbo3.WordWrap = ((bool)(resources.GetObject("tbo3.WordWrap")));
			this.tbo3.TextChanged += new System.EventHandler(this.AutoChangeInst);
			// 
			// tbo2
			// 
			this.tbo2.AccessibleDescription = resources.GetString("tbo2.AccessibleDescription");
			this.tbo2.AccessibleName = resources.GetString("tbo2.AccessibleName");
			this.tbo2.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbo2.Anchor")));
			this.tbo2.AutoSize = ((bool)(resources.GetObject("tbo2.AutoSize")));
			this.tbo2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbo2.BackgroundImage")));
			this.tbo2.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbo2.Dock")));
			this.tbo2.Enabled = ((bool)(resources.GetObject("tbo2.Enabled")));
			this.tbo2.Font = ((System.Drawing.Font)(resources.GetObject("tbo2.Font")));
			this.tbo2.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbo2.ImeMode")));
			this.tbo2.Location = ((System.Drawing.Point)(resources.GetObject("tbo2.Location")));
			this.tbo2.MaxLength = ((int)(resources.GetObject("tbo2.MaxLength")));
			this.tbo2.Multiline = ((bool)(resources.GetObject("tbo2.Multiline")));
			this.tbo2.Name = "tbo2";
			this.tbo2.PasswordChar = ((char)(resources.GetObject("tbo2.PasswordChar")));
			this.tbo2.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbo2.RightToLeft")));
			this.tbo2.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbo2.ScrollBars")));
			this.tbo2.Size = ((System.Drawing.Size)(resources.GetObject("tbo2.Size")));
			this.tbo2.TabIndex = ((int)(resources.GetObject("tbo2.TabIndex")));
			this.tbo2.Text = resources.GetString("tbo2.Text");
			this.tbo2.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbo2.TextAlign")));
			this.tbo2.Visible = ((bool)(resources.GetObject("tbo2.Visible")));
			this.tbo2.WordWrap = ((bool)(resources.GetObject("tbo2.WordWrap")));
			this.tbo2.TextChanged += new System.EventHandler(this.AutoChangeInst);
			// 
			// tbo1
			// 
			this.tbo1.AccessibleDescription = resources.GetString("tbo1.AccessibleDescription");
			this.tbo1.AccessibleName = resources.GetString("tbo1.AccessibleName");
			this.tbo1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbo1.Anchor")));
			this.tbo1.AutoSize = ((bool)(resources.GetObject("tbo1.AutoSize")));
			this.tbo1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbo1.BackgroundImage")));
			this.tbo1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbo1.Dock")));
			this.tbo1.Enabled = ((bool)(resources.GetObject("tbo1.Enabled")));
			this.tbo1.Font = ((System.Drawing.Font)(resources.GetObject("tbo1.Font")));
			this.tbo1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbo1.ImeMode")));
			this.tbo1.Location = ((System.Drawing.Point)(resources.GetObject("tbo1.Location")));
			this.tbo1.MaxLength = ((int)(resources.GetObject("tbo1.MaxLength")));
			this.tbo1.Multiline = ((bool)(resources.GetObject("tbo1.Multiline")));
			this.tbo1.Name = "tbo1";
			this.tbo1.PasswordChar = ((char)(resources.GetObject("tbo1.PasswordChar")));
			this.tbo1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbo1.RightToLeft")));
			this.tbo1.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbo1.ScrollBars")));
			this.tbo1.Size = ((System.Drawing.Size)(resources.GetObject("tbo1.Size")));
			this.tbo1.TabIndex = ((int)(resources.GetObject("tbo1.TabIndex")));
			this.tbo1.Text = resources.GetString("tbo1.Text");
			this.tbo1.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbo1.TextAlign")));
			this.tbo1.Visible = ((bool)(resources.GetObject("tbo1.Visible")));
			this.tbo1.WordWrap = ((bool)(resources.GetObject("tbo1.WordWrap")));
			this.tbo1.TextChanged += new System.EventHandler(this.AutoChangeInst);
			// 
			// tbo0
			// 
			this.tbo0.AccessibleDescription = resources.GetString("tbo0.AccessibleDescription");
			this.tbo0.AccessibleName = resources.GetString("tbo0.AccessibleName");
			this.tbo0.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbo0.Anchor")));
			this.tbo0.AutoSize = ((bool)(resources.GetObject("tbo0.AutoSize")));
			this.tbo0.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbo0.BackgroundImage")));
			this.tbo0.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbo0.Dock")));
			this.tbo0.Enabled = ((bool)(resources.GetObject("tbo0.Enabled")));
			this.tbo0.Font = ((System.Drawing.Font)(resources.GetObject("tbo0.Font")));
			this.tbo0.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbo0.ImeMode")));
			this.tbo0.Location = ((System.Drawing.Point)(resources.GetObject("tbo0.Location")));
			this.tbo0.MaxLength = ((int)(resources.GetObject("tbo0.MaxLength")));
			this.tbo0.Multiline = ((bool)(resources.GetObject("tbo0.Multiline")));
			this.tbo0.Name = "tbo0";
			this.tbo0.PasswordChar = ((char)(resources.GetObject("tbo0.PasswordChar")));
			this.tbo0.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbo0.RightToLeft")));
			this.tbo0.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbo0.ScrollBars")));
			this.tbo0.Size = ((System.Drawing.Size)(resources.GetObject("tbo0.Size")));
			this.tbo0.TabIndex = ((int)(resources.GetObject("tbo0.TabIndex")));
			this.tbo0.Text = resources.GetString("tbo0.Text");
			this.tbo0.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbo0.TextAlign")));
			this.tbo0.Visible = ((bool)(resources.GetObject("tbo0.Visible")));
			this.tbo0.WordWrap = ((bool)(resources.GetObject("tbo0.WordWrap")));
			this.tbo0.TextChanged += new System.EventHandler(this.AutoChangeInst);
			// 
			// tbres
			// 
			this.tbres.AccessibleDescription = resources.GetString("tbres.AccessibleDescription");
			this.tbres.AccessibleName = resources.GetString("tbres.AccessibleName");
			this.tbres.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbres.Anchor")));
			this.tbres.AutoSize = ((bool)(resources.GetObject("tbres.AutoSize")));
			this.tbres.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbres.BackgroundImage")));
			this.tbres.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbres.Dock")));
			this.tbres.Enabled = ((bool)(resources.GetObject("tbres.Enabled")));
			this.tbres.Font = ((System.Drawing.Font)(resources.GetObject("tbres.Font")));
			this.tbres.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbres.ImeMode")));
			this.tbres.Location = ((System.Drawing.Point)(resources.GetObject("tbres.Location")));
			this.tbres.MaxLength = ((int)(resources.GetObject("tbres.MaxLength")));
			this.tbres.Multiline = ((bool)(resources.GetObject("tbres.Multiline")));
			this.tbres.Name = "tbres";
			this.tbres.PasswordChar = ((char)(resources.GetObject("tbres.PasswordChar")));
			this.tbres.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbres.RightToLeft")));
			this.tbres.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbres.ScrollBars")));
			this.tbres.Size = ((System.Drawing.Size)(resources.GetObject("tbres.Size")));
			this.tbres.TabIndex = ((int)(resources.GetObject("tbres.TabIndex")));
			this.tbres.Text = resources.GetString("tbres.Text");
			this.tbres.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbres.TextAlign")));
			this.tbres.Visible = ((bool)(resources.GetObject("tbres.Visible")));
			this.tbres.WordWrap = ((bool)(resources.GetObject("tbres.WordWrap")));
			this.tbres.TextChanged += new System.EventHandler(this.AutoChangeInst);
			// 
			// tbopcode
			// 
			this.tbopcode.AccessibleDescription = resources.GetString("tbopcode.AccessibleDescription");
			this.tbopcode.AccessibleName = resources.GetString("tbopcode.AccessibleName");
			this.tbopcode.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbopcode.Anchor")));
			this.tbopcode.AutoSize = ((bool)(resources.GetObject("tbopcode.AutoSize")));
			this.tbopcode.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbopcode.BackgroundImage")));
			this.tbopcode.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbopcode.Dock")));
			this.tbopcode.Enabled = ((bool)(resources.GetObject("tbopcode.Enabled")));
			this.tbopcode.Font = ((System.Drawing.Font)(resources.GetObject("tbopcode.Font")));
			this.tbopcode.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbopcode.ImeMode")));
			this.tbopcode.Location = ((System.Drawing.Point)(resources.GetObject("tbopcode.Location")));
			this.tbopcode.MaxLength = ((int)(resources.GetObject("tbopcode.MaxLength")));
			this.tbopcode.Multiline = ((bool)(resources.GetObject("tbopcode.Multiline")));
			this.tbopcode.Name = "tbopcode";
			this.tbopcode.PasswordChar = ((char)(resources.GetObject("tbopcode.PasswordChar")));
			this.tbopcode.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbopcode.RightToLeft")));
			this.tbopcode.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbopcode.ScrollBars")));
			this.tbopcode.Size = ((System.Drawing.Size)(resources.GetObject("tbopcode.Size")));
			this.tbopcode.TabIndex = ((int)(resources.GetObject("tbopcode.TabIndex")));
			this.tbopcode.Text = resources.GetString("tbopcode.Text");
			this.tbopcode.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbopcode.TextAlign")));
			this.tbopcode.Visible = ((bool)(resources.GetObject("tbopcode.Visible")));
			this.tbopcode.WordWrap = ((bool)(resources.GetObject("tbopcode.WordWrap")));
			this.tbopcode.TextChanged += new System.EventHandler(this.AutoChangeInst);
			// 
			// label12
			// 
			this.label12.AccessibleDescription = resources.GetString("label12.AccessibleDescription");
			this.label12.AccessibleName = resources.GetString("label12.AccessibleName");
			this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label12.Anchor")));
			this.label12.AutoSize = ((bool)(resources.GetObject("label12.AutoSize")));
			this.label12.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label12.Dock")));
			this.label12.Enabled = ((bool)(resources.GetObject("label12.Enabled")));
			this.label12.Font = ((System.Drawing.Font)(resources.GetObject("label12.Font")));
			this.label12.Image = ((System.Drawing.Image)(resources.GetObject("label12.Image")));
			this.label12.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label12.ImageAlign")));
			this.label12.ImageIndex = ((int)(resources.GetObject("label12.ImageIndex")));
			this.label12.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label12.ImeMode")));
			this.label12.Location = ((System.Drawing.Point)(resources.GetObject("label12.Location")));
			this.label12.Name = "label12";
			this.label12.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label12.RightToLeft")));
			this.label12.Size = ((System.Drawing.Size)(resources.GetObject("label12.Size")));
			this.label12.TabIndex = ((int)(resources.GetObject("label12.TabIndex")));
			this.label12.Text = resources.GetString("label12.Text");
			this.label12.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label12.TextAlign")));
			this.label12.Visible = ((bool)(resources.GetObject("label12.Visible")));
			// 
			// label11
			// 
			this.label11.AccessibleDescription = resources.GetString("label11.AccessibleDescription");
			this.label11.AccessibleName = resources.GetString("label11.AccessibleName");
			this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label11.Anchor")));
			this.label11.AutoSize = ((bool)(resources.GetObject("label11.AutoSize")));
			this.label11.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label11.Dock")));
			this.label11.Enabled = ((bool)(resources.GetObject("label11.Enabled")));
			this.label11.Font = ((System.Drawing.Font)(resources.GetObject("label11.Font")));
			this.label11.Image = ((System.Drawing.Image)(resources.GetObject("label11.Image")));
			this.label11.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label11.ImageAlign")));
			this.label11.ImageIndex = ((int)(resources.GetObject("label11.ImageIndex")));
			this.label11.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label11.ImeMode")));
			this.label11.Location = ((System.Drawing.Point)(resources.GetObject("label11.Location")));
			this.label11.Name = "label11";
			this.label11.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label11.RightToLeft")));
			this.label11.Size = ((System.Drawing.Size)(resources.GetObject("label11.Size")));
			this.label11.TabIndex = ((int)(resources.GetObject("label11.TabIndex")));
			this.label11.Text = resources.GetString("label11.Text");
			this.label11.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label11.TextAlign")));
			this.label11.Visible = ((bool)(resources.GetObject("label11.Visible")));
			// 
			// label10
			// 
			this.label10.AccessibleDescription = resources.GetString("label10.AccessibleDescription");
			this.label10.AccessibleName = resources.GetString("label10.AccessibleName");
			this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label10.Anchor")));
			this.label10.AutoSize = ((bool)(resources.GetObject("label10.AutoSize")));
			this.label10.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label10.Dock")));
			this.label10.Enabled = ((bool)(resources.GetObject("label10.Enabled")));
			this.label10.Font = ((System.Drawing.Font)(resources.GetObject("label10.Font")));
			this.label10.Image = ((System.Drawing.Image)(resources.GetObject("label10.Image")));
			this.label10.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label10.ImageAlign")));
			this.label10.ImageIndex = ((int)(resources.GetObject("label10.ImageIndex")));
			this.label10.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label10.ImeMode")));
			this.label10.Location = ((System.Drawing.Point)(resources.GetObject("label10.Location")));
			this.label10.Name = "label10";
			this.label10.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label10.RightToLeft")));
			this.label10.Size = ((System.Drawing.Size)(resources.GetObject("label10.Size")));
			this.label10.TabIndex = ((int)(resources.GetObject("label10.TabIndex")));
			this.label10.Text = resources.GetString("label10.Text");
			this.label10.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label10.TextAlign")));
			this.label10.Visible = ((bool)(resources.GetObject("label10.Visible")));
			// 
			// label9
			// 
			this.label9.AccessibleDescription = resources.GetString("label9.AccessibleDescription");
			this.label9.AccessibleName = resources.GetString("label9.AccessibleName");
			this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label9.Anchor")));
			this.label9.AutoSize = ((bool)(resources.GetObject("label9.AutoSize")));
			this.label9.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label9.Dock")));
			this.label9.Enabled = ((bool)(resources.GetObject("label9.Enabled")));
			this.label9.Font = ((System.Drawing.Font)(resources.GetObject("label9.Font")));
			this.label9.Image = ((System.Drawing.Image)(resources.GetObject("label9.Image")));
			this.label9.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label9.ImageAlign")));
			this.label9.ImageIndex = ((int)(resources.GetObject("label9.ImageIndex")));
			this.label9.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label9.ImeMode")));
			this.label9.Location = ((System.Drawing.Point)(resources.GetObject("label9.Location")));
			this.label9.Name = "label9";
			this.label9.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label9.RightToLeft")));
			this.label9.Size = ((System.Drawing.Size)(resources.GetObject("label9.Size")));
			this.label9.TabIndex = ((int)(resources.GetObject("label9.TabIndex")));
			this.label9.Text = resources.GetString("label9.Text");
			this.label9.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label9.TextAlign")));
			this.label9.Visible = ((bool)(resources.GetObject("label9.Visible")));
			// 
			// button4
			// 
			this.button4.AccessibleDescription = resources.GetString("button4.AccessibleDescription");
			this.button4.AccessibleName = resources.GetString("button4.AccessibleName");
			this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("button4.Anchor")));
			this.button4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button4.BackgroundImage")));
			this.button4.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("button4.Dock")));
			this.button4.Enabled = ((bool)(resources.GetObject("button4.Enabled")));
			this.button4.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("button4.FlatStyle")));
			this.button4.Font = ((System.Drawing.Font)(resources.GetObject("button4.Font")));
			this.button4.Image = ((System.Drawing.Image)(resources.GetObject("button4.Image")));
			this.button4.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("button4.ImageAlign")));
			this.button4.ImageIndex = ((int)(resources.GetObject("button4.ImageIndex")));
			this.button4.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("button4.ImeMode")));
			this.button4.Location = ((System.Drawing.Point)(resources.GetObject("button4.Location")));
			this.button4.Name = "button4";
			this.button4.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("button4.RightToLeft")));
			this.button4.Size = ((System.Drawing.Size)(resources.GetObject("button4.Size")));
			this.button4.TabIndex = ((int)(resources.GetObject("button4.TabIndex")));
			this.button4.Text = resources.GetString("button4.Text");
			this.button4.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("button4.TextAlign")));
			this.button4.Visible = ((bool)(resources.GetObject("button4.Visible")));
			this.button4.Click += new System.EventHandler(this.GetOpcode);
			// 
			// lbtext
			// 
			this.lbtext.AccessibleDescription = resources.GetString("lbtext.AccessibleDescription");
			this.lbtext.AccessibleName = resources.GetString("lbtext.AccessibleName");
			this.lbtext.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbtext.Anchor")));
			this.lbtext.AutoSize = ((bool)(resources.GetObject("lbtext.AutoSize")));
			this.lbtext.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("lbtext.BackgroundImage")));
			this.lbtext.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbtext.Dock")));
			this.lbtext.Enabled = ((bool)(resources.GetObject("lbtext.Enabled")));
			this.lbtext.Font = ((System.Drawing.Font)(resources.GetObject("lbtext.Font")));
			this.lbtext.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbtext.ImeMode")));
			this.lbtext.Location = ((System.Drawing.Point)(resources.GetObject("lbtext.Location")));
			this.lbtext.MaxLength = ((int)(resources.GetObject("lbtext.MaxLength")));
			this.lbtext.Multiline = ((bool)(resources.GetObject("lbtext.Multiline")));
			this.lbtext.Name = "lbtext";
			this.lbtext.PasswordChar = ((char)(resources.GetObject("lbtext.PasswordChar")));
			this.lbtext.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbtext.RightToLeft")));
			this.lbtext.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("lbtext.ScrollBars")));
			this.lbtext.Size = ((System.Drawing.Size)(resources.GetObject("lbtext.Size")));
			this.lbtext.TabIndex = ((int)(resources.GetObject("lbtext.TabIndex")));
			this.lbtext.Text = resources.GetString("lbtext.Text");
			this.lbtext.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("lbtext.TextAlign")));
			this.lbtext.Visible = ((bool)(resources.GetObject("lbtext.Visible")));
			this.lbtext.WordWrap = ((bool)(resources.GetObject("lbtext.WordWrap")));
			// 
			// tbzero
			// 
			this.tbzero.AccessibleDescription = resources.GetString("tbzero.AccessibleDescription");
			this.tbzero.AccessibleName = resources.GetString("tbzero.AccessibleName");
			this.tbzero.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbzero.Anchor")));
			this.tbzero.AutoSize = ((bool)(resources.GetObject("tbzero.AutoSize")));
			this.tbzero.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbzero.BackgroundImage")));
			this.tbzero.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbzero.Dock")));
			this.tbzero.Enabled = ((bool)(resources.GetObject("tbzero.Enabled")));
			this.tbzero.Font = ((System.Drawing.Font)(resources.GetObject("tbzero.Font")));
			this.tbzero.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbzero.ImeMode")));
			this.tbzero.Location = ((System.Drawing.Point)(resources.GetObject("tbzero.Location")));
			this.tbzero.MaxLength = ((int)(resources.GetObject("tbzero.MaxLength")));
			this.tbzero.Multiline = ((bool)(resources.GetObject("tbzero.Multiline")));
			this.tbzero.Name = "tbzero";
			this.tbzero.PasswordChar = ((char)(resources.GetObject("tbzero.PasswordChar")));
			this.tbzero.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbzero.RightToLeft")));
			this.tbzero.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbzero.ScrollBars")));
			this.tbzero.Size = ((System.Drawing.Size)(resources.GetObject("tbzero.Size")));
			this.tbzero.TabIndex = ((int)(resources.GetObject("tbzero.TabIndex")));
			this.tbzero.Text = resources.GetString("tbzero.Text");
			this.tbzero.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbzero.TextAlign")));
			this.tbzero.Visible = ((bool)(resources.GetObject("tbzero.Visible")));
			this.tbzero.WordWrap = ((bool)(resources.GetObject("tbzero.WordWrap")));
			this.tbzero.TextChanged += new System.EventHandler(this.AutoChangeBhav);
			// 
			// tblocals
			// 
			this.tblocals.AccessibleDescription = resources.GetString("tblocals.AccessibleDescription");
			this.tblocals.AccessibleName = resources.GetString("tblocals.AccessibleName");
			this.tblocals.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tblocals.Anchor")));
			this.tblocals.AutoSize = ((bool)(resources.GetObject("tblocals.AutoSize")));
			this.tblocals.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tblocals.BackgroundImage")));
			this.tblocals.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tblocals.Dock")));
			this.tblocals.Enabled = ((bool)(resources.GetObject("tblocals.Enabled")));
			this.tblocals.Font = ((System.Drawing.Font)(resources.GetObject("tblocals.Font")));
			this.tblocals.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tblocals.ImeMode")));
			this.tblocals.Location = ((System.Drawing.Point)(resources.GetObject("tblocals.Location")));
			this.tblocals.MaxLength = ((int)(resources.GetObject("tblocals.MaxLength")));
			this.tblocals.Multiline = ((bool)(resources.GetObject("tblocals.Multiline")));
			this.tblocals.Name = "tblocals";
			this.tblocals.PasswordChar = ((char)(resources.GetObject("tblocals.PasswordChar")));
			this.tblocals.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tblocals.RightToLeft")));
			this.tblocals.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tblocals.ScrollBars")));
			this.tblocals.Size = ((System.Drawing.Size)(resources.GetObject("tblocals.Size")));
			this.tblocals.TabIndex = ((int)(resources.GetObject("tblocals.TabIndex")));
			this.tblocals.Text = resources.GetString("tblocals.Text");
			this.tblocals.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tblocals.TextAlign")));
			this.tblocals.Visible = ((bool)(resources.GetObject("tblocals.Visible")));
			this.tblocals.WordWrap = ((bool)(resources.GetObject("tblocals.WordWrap")));
			this.tblocals.TextChanged += new System.EventHandler(this.AutoChangeBhav);
			// 
			// tbflags
			// 
			this.tbflags.AccessibleDescription = resources.GetString("tbflags.AccessibleDescription");
			this.tbflags.AccessibleName = resources.GetString("tbflags.AccessibleName");
			this.tbflags.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbflags.Anchor")));
			this.tbflags.AutoSize = ((bool)(resources.GetObject("tbflags.AutoSize")));
			this.tbflags.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbflags.BackgroundImage")));
			this.tbflags.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbflags.Dock")));
			this.tbflags.Enabled = ((bool)(resources.GetObject("tbflags.Enabled")));
			this.tbflags.Font = ((System.Drawing.Font)(resources.GetObject("tbflags.Font")));
			this.tbflags.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbflags.ImeMode")));
			this.tbflags.Location = ((System.Drawing.Point)(resources.GetObject("tbflags.Location")));
			this.tbflags.MaxLength = ((int)(resources.GetObject("tbflags.MaxLength")));
			this.tbflags.Multiline = ((bool)(resources.GetObject("tbflags.Multiline")));
			this.tbflags.Name = "tbflags";
			this.tbflags.PasswordChar = ((char)(resources.GetObject("tbflags.PasswordChar")));
			this.tbflags.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbflags.RightToLeft")));
			this.tbflags.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbflags.ScrollBars")));
			this.tbflags.Size = ((System.Drawing.Size)(resources.GetObject("tbflags.Size")));
			this.tbflags.TabIndex = ((int)(resources.GetObject("tbflags.TabIndex")));
			this.tbflags.Text = resources.GetString("tbflags.Text");
			this.tbflags.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbflags.TextAlign")));
			this.tbflags.Visible = ((bool)(resources.GetObject("tbflags.Visible")));
			this.tbflags.WordWrap = ((bool)(resources.GetObject("tbflags.WordWrap")));
			this.tbflags.TextChanged += new System.EventHandler(this.AutoChangeBhav);
			// 
			// tbargc
			// 
			this.tbargc.AccessibleDescription = resources.GetString("tbargc.AccessibleDescription");
			this.tbargc.AccessibleName = resources.GetString("tbargc.AccessibleName");
			this.tbargc.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbargc.Anchor")));
			this.tbargc.AutoSize = ((bool)(resources.GetObject("tbargc.AutoSize")));
			this.tbargc.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbargc.BackgroundImage")));
			this.tbargc.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbargc.Dock")));
			this.tbargc.Enabled = ((bool)(resources.GetObject("tbargc.Enabled")));
			this.tbargc.Font = ((System.Drawing.Font)(resources.GetObject("tbargc.Font")));
			this.tbargc.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbargc.ImeMode")));
			this.tbargc.Location = ((System.Drawing.Point)(resources.GetObject("tbargc.Location")));
			this.tbargc.MaxLength = ((int)(resources.GetObject("tbargc.MaxLength")));
			this.tbargc.Multiline = ((bool)(resources.GetObject("tbargc.Multiline")));
			this.tbargc.Name = "tbargc";
			this.tbargc.PasswordChar = ((char)(resources.GetObject("tbargc.PasswordChar")));
			this.tbargc.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbargc.RightToLeft")));
			this.tbargc.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbargc.ScrollBars")));
			this.tbargc.Size = ((System.Drawing.Size)(resources.GetObject("tbargc.Size")));
			this.tbargc.TabIndex = ((int)(resources.GetObject("tbargc.TabIndex")));
			this.tbargc.Text = resources.GetString("tbargc.Text");
			this.tbargc.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbargc.TextAlign")));
			this.tbargc.Visible = ((bool)(resources.GetObject("tbargc.Visible")));
			this.tbargc.WordWrap = ((bool)(resources.GetObject("tbargc.WordWrap")));
			this.tbargc.TextChanged += new System.EventHandler(this.AutoChangeBhav);
			// 
			// tbtype
			// 
			this.tbtype.AccessibleDescription = resources.GetString("tbtype.AccessibleDescription");
			this.tbtype.AccessibleName = resources.GetString("tbtype.AccessibleName");
			this.tbtype.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbtype.Anchor")));
			this.tbtype.AutoSize = ((bool)(resources.GetObject("tbtype.AutoSize")));
			this.tbtype.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbtype.BackgroundImage")));
			this.tbtype.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbtype.Dock")));
			this.tbtype.Enabled = ((bool)(resources.GetObject("tbtype.Enabled")));
			this.tbtype.Font = ((System.Drawing.Font)(resources.GetObject("tbtype.Font")));
			this.tbtype.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbtype.ImeMode")));
			this.tbtype.Location = ((System.Drawing.Point)(resources.GetObject("tbtype.Location")));
			this.tbtype.MaxLength = ((int)(resources.GetObject("tbtype.MaxLength")));
			this.tbtype.Multiline = ((bool)(resources.GetObject("tbtype.Multiline")));
			this.tbtype.Name = "tbtype";
			this.tbtype.PasswordChar = ((char)(resources.GetObject("tbtype.PasswordChar")));
			this.tbtype.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbtype.RightToLeft")));
			this.tbtype.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbtype.ScrollBars")));
			this.tbtype.Size = ((System.Drawing.Size)(resources.GetObject("tbtype.Size")));
			this.tbtype.TabIndex = ((int)(resources.GetObject("tbtype.TabIndex")));
			this.tbtype.Text = resources.GetString("tbtype.Text");
			this.tbtype.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbtype.TextAlign")));
			this.tbtype.Visible = ((bool)(resources.GetObject("tbtype.Visible")));
			this.tbtype.WordWrap = ((bool)(resources.GetObject("tbtype.WordWrap")));
			this.tbtype.TextChanged += new System.EventHandler(this.AutoChangeBhav);
			// 
			// tbformat
			// 
			this.tbformat.AccessibleDescription = resources.GetString("tbformat.AccessibleDescription");
			this.tbformat.AccessibleName = resources.GetString("tbformat.AccessibleName");
			this.tbformat.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbformat.Anchor")));
			this.tbformat.AutoSize = ((bool)(resources.GetObject("tbformat.AutoSize")));
			this.tbformat.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbformat.BackgroundImage")));
			this.tbformat.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbformat.Dock")));
			this.tbformat.Enabled = ((bool)(resources.GetObject("tbformat.Enabled")));
			this.tbformat.Font = ((System.Drawing.Font)(resources.GetObject("tbformat.Font")));
			this.tbformat.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbformat.ImeMode")));
			this.tbformat.Location = ((System.Drawing.Point)(resources.GetObject("tbformat.Location")));
			this.tbformat.MaxLength = ((int)(resources.GetObject("tbformat.MaxLength")));
			this.tbformat.Multiline = ((bool)(resources.GetObject("tbformat.Multiline")));
			this.tbformat.Name = "tbformat";
			this.tbformat.PasswordChar = ((char)(resources.GetObject("tbformat.PasswordChar")));
			this.tbformat.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbformat.RightToLeft")));
			this.tbformat.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbformat.ScrollBars")));
			this.tbformat.Size = ((System.Drawing.Size)(resources.GetObject("tbformat.Size")));
			this.tbformat.TabIndex = ((int)(resources.GetObject("tbformat.TabIndex")));
			this.tbformat.Text = resources.GetString("tbformat.Text");
			this.tbformat.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbformat.TextAlign")));
			this.tbformat.Visible = ((bool)(resources.GetObject("tbformat.Visible")));
			this.tbformat.WordWrap = ((bool)(resources.GetObject("tbformat.WordWrap")));
			this.tbformat.TextChanged += new System.EventHandler(this.AutoChangeBhav);
			// 
			// label7
			// 
			this.label7.AccessibleDescription = resources.GetString("label7.AccessibleDescription");
			this.label7.AccessibleName = resources.GetString("label7.AccessibleName");
			this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label7.Anchor")));
			this.label7.AutoSize = ((bool)(resources.GetObject("label7.AutoSize")));
			this.label7.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label7.Dock")));
			this.label7.Enabled = ((bool)(resources.GetObject("label7.Enabled")));
			this.label7.Font = ((System.Drawing.Font)(resources.GetObject("label7.Font")));
			this.label7.Image = ((System.Drawing.Image)(resources.GetObject("label7.Image")));
			this.label7.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label7.ImageAlign")));
			this.label7.ImageIndex = ((int)(resources.GetObject("label7.ImageIndex")));
			this.label7.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label7.ImeMode")));
			this.label7.Location = ((System.Drawing.Point)(resources.GetObject("label7.Location")));
			this.label7.Name = "label7";
			this.label7.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label7.RightToLeft")));
			this.label7.Size = ((System.Drawing.Size)(resources.GetObject("label7.Size")));
			this.label7.TabIndex = ((int)(resources.GetObject("label7.TabIndex")));
			this.label7.Text = resources.GetString("label7.Text");
			this.label7.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label7.TextAlign")));
			this.label7.Visible = ((bool)(resources.GetObject("label7.Visible")));
			// 
			// label6
			// 
			this.label6.AccessibleDescription = resources.GetString("label6.AccessibleDescription");
			this.label6.AccessibleName = resources.GetString("label6.AccessibleName");
			this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label6.Anchor")));
			this.label6.AutoSize = ((bool)(resources.GetObject("label6.AutoSize")));
			this.label6.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label6.Dock")));
			this.label6.Enabled = ((bool)(resources.GetObject("label6.Enabled")));
			this.label6.Font = ((System.Drawing.Font)(resources.GetObject("label6.Font")));
			this.label6.Image = ((System.Drawing.Image)(resources.GetObject("label6.Image")));
			this.label6.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label6.ImageAlign")));
			this.label6.ImageIndex = ((int)(resources.GetObject("label6.ImageIndex")));
			this.label6.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label6.ImeMode")));
			this.label6.Location = ((System.Drawing.Point)(resources.GetObject("label6.Location")));
			this.label6.Name = "label6";
			this.label6.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label6.RightToLeft")));
			this.label6.Size = ((System.Drawing.Size)(resources.GetObject("label6.Size")));
			this.label6.TabIndex = ((int)(resources.GetObject("label6.TabIndex")));
			this.label6.Text = resources.GetString("label6.Text");
			this.label6.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label6.TextAlign")));
			this.label6.Visible = ((bool)(resources.GetObject("label6.Visible")));
			// 
			// label5
			// 
			this.label5.AccessibleDescription = resources.GetString("label5.AccessibleDescription");
			this.label5.AccessibleName = resources.GetString("label5.AccessibleName");
			this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label5.Anchor")));
			this.label5.AutoSize = ((bool)(resources.GetObject("label5.AutoSize")));
			this.label5.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label5.Dock")));
			this.label5.Enabled = ((bool)(resources.GetObject("label5.Enabled")));
			this.label5.Font = ((System.Drawing.Font)(resources.GetObject("label5.Font")));
			this.label5.Image = ((System.Drawing.Image)(resources.GetObject("label5.Image")));
			this.label5.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label5.ImageAlign")));
			this.label5.ImageIndex = ((int)(resources.GetObject("label5.ImageIndex")));
			this.label5.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label5.ImeMode")));
			this.label5.Location = ((System.Drawing.Point)(resources.GetObject("label5.Location")));
			this.label5.Name = "label5";
			this.label5.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label5.RightToLeft")));
			this.label5.Size = ((System.Drawing.Size)(resources.GetObject("label5.Size")));
			this.label5.TabIndex = ((int)(resources.GetObject("label5.TabIndex")));
			this.label5.Text = resources.GetString("label5.Text");
			this.label5.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label5.TextAlign")));
			this.label5.Visible = ((bool)(resources.GetObject("label5.Visible")));
			// 
			// label4
			// 
			this.label4.AccessibleDescription = resources.GetString("label4.AccessibleDescription");
			this.label4.AccessibleName = resources.GetString("label4.AccessibleName");
			this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label4.Anchor")));
			this.label4.AutoSize = ((bool)(resources.GetObject("label4.AutoSize")));
			this.label4.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label4.Dock")));
			this.label4.Enabled = ((bool)(resources.GetObject("label4.Enabled")));
			this.label4.Font = ((System.Drawing.Font)(resources.GetObject("label4.Font")));
			this.label4.Image = ((System.Drawing.Image)(resources.GetObject("label4.Image")));
			this.label4.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label4.ImageAlign")));
			this.label4.ImageIndex = ((int)(resources.GetObject("label4.ImageIndex")));
			this.label4.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label4.ImeMode")));
			this.label4.Location = ((System.Drawing.Point)(resources.GetObject("label4.Location")));
			this.label4.Name = "label4";
			this.label4.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label4.RightToLeft")));
			this.label4.Size = ((System.Drawing.Size)(resources.GetObject("label4.Size")));
			this.label4.TabIndex = ((int)(resources.GetObject("label4.TabIndex")));
			this.label4.Text = resources.GetString("label4.Text");
			this.label4.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label4.TextAlign")));
			this.label4.Visible = ((bool)(resources.GetObject("label4.Visible")));
			// 
			// label3
			// 
			this.label3.AccessibleDescription = resources.GetString("label3.AccessibleDescription");
			this.label3.AccessibleName = resources.GetString("label3.AccessibleName");
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label3.Anchor")));
			this.label3.AutoSize = ((bool)(resources.GetObject("label3.AutoSize")));
			this.label3.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label3.Dock")));
			this.label3.Enabled = ((bool)(resources.GetObject("label3.Enabled")));
			this.label3.Font = ((System.Drawing.Font)(resources.GetObject("label3.Font")));
			this.label3.Image = ((System.Drawing.Image)(resources.GetObject("label3.Image")));
			this.label3.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label3.ImageAlign")));
			this.label3.ImageIndex = ((int)(resources.GetObject("label3.ImageIndex")));
			this.label3.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label3.ImeMode")));
			this.label3.Location = ((System.Drawing.Point)(resources.GetObject("label3.Location")));
			this.label3.Name = "label3";
			this.label3.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label3.RightToLeft")));
			this.label3.Size = ((System.Drawing.Size)(resources.GetObject("label3.Size")));
			this.label3.TabIndex = ((int)(resources.GetObject("label3.TabIndex")));
			this.label3.Text = resources.GetString("label3.Text");
			this.label3.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label3.TextAlign")));
			this.label3.Visible = ((bool)(resources.GetObject("label3.Visible")));
			// 
			// label2
			// 
			this.label2.AccessibleDescription = resources.GetString("label2.AccessibleDescription");
			this.label2.AccessibleName = resources.GetString("label2.AccessibleName");
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label2.Anchor")));
			this.label2.AutoSize = ((bool)(resources.GetObject("label2.AutoSize")));
			this.label2.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label2.Dock")));
			this.label2.Enabled = ((bool)(resources.GetObject("label2.Enabled")));
			this.label2.Font = ((System.Drawing.Font)(resources.GetObject("label2.Font")));
			this.label2.Image = ((System.Drawing.Image)(resources.GetObject("label2.Image")));
			this.label2.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label2.ImageAlign")));
			this.label2.ImageIndex = ((int)(resources.GetObject("label2.ImageIndex")));
			this.label2.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label2.ImeMode")));
			this.label2.Location = ((System.Drawing.Point)(resources.GetObject("label2.Location")));
			this.label2.Name = "label2";
			this.label2.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label2.RightToLeft")));
			this.label2.Size = ((System.Drawing.Size)(resources.GetObject("label2.Size")));
			this.label2.TabIndex = ((int)(resources.GetObject("label2.TabIndex")));
			this.label2.Text = resources.GetString("label2.Text");
			this.label2.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label2.TextAlign")));
			this.label2.Visible = ((bool)(resources.GetObject("label2.Visible")));
			// 
			// btcommit
			// 
			this.btcommit.AccessibleDescription = resources.GetString("btcommit.AccessibleDescription");
			this.btcommit.AccessibleName = resources.GetString("btcommit.AccessibleName");
			this.btcommit.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btcommit.Anchor")));
			this.btcommit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btcommit.BackgroundImage")));
			this.btcommit.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btcommit.Dock")));
			this.btcommit.Enabled = ((bool)(resources.GetObject("btcommit.Enabled")));
			this.btcommit.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btcommit.FlatStyle")));
			this.btcommit.Font = ((System.Drawing.Font)(resources.GetObject("btcommit.Font")));
			this.btcommit.Image = ((System.Drawing.Image)(resources.GetObject("btcommit.Image")));
			this.btcommit.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btcommit.ImageAlign")));
			this.btcommit.ImageIndex = ((int)(resources.GetObject("btcommit.ImageIndex")));
			this.btcommit.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btcommit.ImeMode")));
			this.btcommit.Location = ((System.Drawing.Point)(resources.GetObject("btcommit.Location")));
			this.btcommit.Name = "btcommit";
			this.btcommit.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btcommit.RightToLeft")));
			this.btcommit.Size = ((System.Drawing.Size)(resources.GetObject("btcommit.Size")));
			this.btcommit.TabIndex = ((int)(resources.GetObject("btcommit.TabIndex")));
			this.btcommit.Text = resources.GetString("btcommit.Text");
			this.btcommit.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btcommit.TextAlign")));
			this.btcommit.Visible = ((bool)(resources.GetObject("btcommit.Visible")));
			this.btcommit.Click += new System.EventHandler(this.CommitClick);
			// 
			// panel3
			// 
			this.panel3.AccessibleDescription = resources.GetString("panel3.AccessibleDescription");
			this.panel3.AccessibleName = resources.GetString("panel3.AccessibleName");
			this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("panel3.Anchor")));
			this.panel3.AutoScroll = ((bool)(resources.GetObject("panel3.AutoScroll")));
			this.panel3.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("panel3.AutoScrollMargin")));
			this.panel3.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("panel3.AutoScrollMinSize")));
			this.panel3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel3.BackgroundImage")));
			this.panel3.Controls.Add(this.label1);
			this.panel3.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("panel3.Dock")));
			this.panel3.Enabled = ((bool)(resources.GetObject("panel3.Enabled")));
			this.panel3.Font = ((System.Drawing.Font)(resources.GetObject("panel3.Font")));
			this.panel3.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("panel3.ImeMode")));
			this.panel3.Location = ((System.Drawing.Point)(resources.GetObject("panel3.Location")));
			this.panel3.Name = "panel3";
			this.panel3.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("panel3.RightToLeft")));
			this.panel3.Size = ((System.Drawing.Size)(resources.GetObject("panel3.Size")));
			this.panel3.TabIndex = ((int)(resources.GetObject("panel3.TabIndex")));
			this.panel3.Text = resources.GetString("panel3.Text");
			this.panel3.Visible = ((bool)(resources.GetObject("panel3.Visible")));
			// 
			// label1
			// 
			this.label1.AccessibleDescription = resources.GetString("label1.AccessibleDescription");
			this.label1.AccessibleName = resources.GetString("label1.AccessibleName");
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label1.Anchor")));
			this.label1.AutoSize = ((bool)(resources.GetObject("label1.AutoSize")));
			this.label1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label1.Dock")));
			this.label1.Enabled = ((bool)(resources.GetObject("label1.Enabled")));
			this.label1.Font = ((System.Drawing.Font)(resources.GetObject("label1.Font")));
			this.label1.Image = ((System.Drawing.Image)(resources.GetObject("label1.Image")));
			this.label1.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label1.ImageAlign")));
			this.label1.ImageIndex = ((int)(resources.GetObject("label1.ImageIndex")));
			this.label1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label1.ImeMode")));
			this.label1.Location = ((System.Drawing.Point)(resources.GetObject("label1.Location")));
			this.label1.Name = "label1";
			this.label1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label1.RightToLeft")));
			this.label1.Size = ((System.Drawing.Size)(resources.GetObject("label1.Size")));
			this.label1.TabIndex = ((int)(resources.GetObject("label1.TabIndex")));
			this.label1.Text = resources.GetString("label1.Text");
			this.label1.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label1.TextAlign")));
			this.label1.Visible = ((bool)(resources.GetObject("label1.Visible")));
			// 
			// cmcopy
			// 
			this.cmcopy.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																				   this.menuItem1,
																				   this.menuItem2});
			this.cmcopy.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cmcopy.RightToLeft")));
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
			// bhavPanel
			// 
			this.bhavPanel.AccessibleDescription = resources.GetString("bhavPanel.AccessibleDescription");
			this.bhavPanel.AccessibleName = resources.GetString("bhavPanel.AccessibleName");
			this.bhavPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("bhavPanel.Anchor")));
			this.bhavPanel.AutoScroll = ((bool)(resources.GetObject("bhavPanel.AutoScroll")));
			this.bhavPanel.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("bhavPanel.AutoScrollMargin")));
			this.bhavPanel.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("bhavPanel.AutoScrollMinSize")));
			this.bhavPanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("bhavPanel.BackgroundImage")));
			this.bhavPanel.Controls.Add(this.llsort);
			this.bhavPanel.Controls.Add(this.label16);
			this.bhavPanel.Controls.Add(this.lbbhav);
			this.bhavPanel.Controls.Add(this.pnflowcontainer);
			this.bhavPanel.Controls.Add(this.gbopcodes);
			this.bhavPanel.Controls.Add(this.tbzero);
			this.bhavPanel.Controls.Add(this.tblocals);
			this.bhavPanel.Controls.Add(this.tbflags);
			this.bhavPanel.Controls.Add(this.tbargc);
			this.bhavPanel.Controls.Add(this.tbtype);
			this.bhavPanel.Controls.Add(this.tbformat);
			this.bhavPanel.Controls.Add(this.label7);
			this.bhavPanel.Controls.Add(this.label6);
			this.bhavPanel.Controls.Add(this.label5);
			this.bhavPanel.Controls.Add(this.label4);
			this.bhavPanel.Controls.Add(this.label3);
			this.bhavPanel.Controls.Add(this.label2);
			this.bhavPanel.Controls.Add(this.btcommit);
			this.bhavPanel.Controls.Add(this.panel3);
			this.bhavPanel.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("bhavPanel.Dock")));
			this.bhavPanel.Enabled = ((bool)(resources.GetObject("bhavPanel.Enabled")));
			this.bhavPanel.Font = ((System.Drawing.Font)(resources.GetObject("bhavPanel.Font")));
			this.bhavPanel.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("bhavPanel.ImeMode")));
			this.bhavPanel.Location = ((System.Drawing.Point)(resources.GetObject("bhavPanel.Location")));
			this.bhavPanel.Name = "bhavPanel";
			this.bhavPanel.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("bhavPanel.RightToLeft")));
			this.bhavPanel.Size = ((System.Drawing.Size)(resources.GetObject("bhavPanel.Size")));
			this.bhavPanel.TabIndex = ((int)(resources.GetObject("bhavPanel.TabIndex")));
			this.bhavPanel.Text = resources.GetString("bhavPanel.Text");
			this.bhavPanel.Visible = ((bool)(resources.GetObject("bhavPanel.Visible")));
			// 
			// BhavForm
			// 
			this.AccessibleDescription = resources.GetString("$this.AccessibleDescription");
			this.AccessibleName = resources.GetString("$this.AccessibleName");
			this.AutoScaleBaseSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScaleBaseSize")));
			this.AutoScroll = ((bool)(resources.GetObject("$this.AutoScroll")));
			this.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMargin")));
			this.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMinSize")));
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.ClientSize = ((System.Drawing.Size)(resources.GetObject("$this.ClientSize")));
			this.Controls.Add(this.bhavPanel);
			this.Enabled = ((bool)(resources.GetObject("$this.Enabled")));
			this.Font = ((System.Drawing.Font)(resources.GetObject("$this.Font")));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("$this.ImeMode")));
			this.Location = ((System.Drawing.Point)(resources.GetObject("$this.Location")));
			this.MaximumSize = ((System.Drawing.Size)(resources.GetObject("$this.MaximumSize")));
			this.MinimumSize = ((System.Drawing.Size)(resources.GetObject("$this.MinimumSize")));
			this.Name = "BhavForm";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.StartPosition = ((System.Windows.Forms.FormStartPosition)(resources.GetObject("$this.StartPosition")));
			this.Text = resources.GetString("$this.Text");
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.pnflowcontainer.ResumeLayout(false);
			this.gbopcodes.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			this.bhavPanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		/// <summary>
		/// Issued when the Commit Button was clicked
		/// </summary>
		/// <param name="sender">The Button that sended the Event</param>
		/// <param name="e">Event Parameters</param>
		private void GetOpcode(object sender, System.EventArgs e)
		{
			try 
			{
				Bhav wrp = (Bhav)wrapper;
				Bhav bhav = new Bhav(wrp.Opcodes);
				bhav.Package = wrp.Package;
				bhav.FileDescriptor = wrp.FileDescriptor;
				
				ushort opcode = SimPe.Plugin.WrapperFactory.BhavWizardForm.Execute(bhav, this);

				tbopcode.Text = "0x"+Helper.HexString(opcode);
			} 
			catch (Exception ex) 
			{
				
				Helper.ExceptionMessage(Localization.Manager.GetString("errconvert"), ex);
			}
		}

		private void AutoChangePoiningInst(object sender, System.EventArgs e)
		{
			if (internalchg) return;
			AutoChangeInst(sender, e);
			DrawConnectors();
		}

		private void AutoChangeInst(object sender, System.EventArgs e)
		{
			try 
			{
				this.btwiz.Enabled = BhavWizardForm.Available(Convert.ToUInt16(this.tbopcode.Text, 16));
			} 
			catch (Exception) 
			{
				btwiz.Enabled = false;
			}

			if (internalchg) return;
			if (csel>-1) CommitOpcodeClicked(null, null);
			
		}

		private void CommitClick(object sender, System.EventArgs e)
		{
			try 
			{
				if (csel>=0) CommitOpcodeClicked(null, null);
				Bhav wrp = (Bhav)wrapper;				

				/*wrp.Header.Format = Convert.ToUInt16(tbformat.Text, 16);
				wrp.Header.ArgumentCount = Convert.ToByte(tbargc.Text, 10);
				wrp.Header.LocalVarCount = Convert.ToUInt16(tblocals.Text, 10);
				wrp.Header.Type = Convert.ToByte(tbtype.Text, 16);
				wrp.Header.Flags = Convert.ToUInt16(tbflags.Text, 16);
				wrp.Header.Zero = Convert.ToUInt16(tbzero.Text, 16);

				wrp.FileName = lbbhav.Text;*/
				wrp.Instructions = InstructionItem.Instructions(flowitems);

				wrapper.SynchronizeUserData();
				MessageBox.Show(Localization.Manager.GetString("commited"));
			} 
			catch (Exception ex) 
			{
				Helper.ExceptionMessage(Localization.Manager.GetString("errwritingfile"), ex);
			}			
		}

		private void DeleteOpcodeClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			lldel.Enabled = false;
			if (csel<0) return;
			if (csel>flowitems.Length-1) return;
			
			try 
			{
				ArrayList list = new ArrayList();

				foreach (InstructionItem item in flowitems) 
				{
					if (item.index!=csel) list.Add(item.instruction);
				}

				Instruction[] instructions = new Instruction[list.Count];
				list.CopyTo(instructions);

				if (csel>=0) 
				{
					int i = 0;
					while (i<flowitems.Length) 
					{
						if ((flowitems[i].instruction.Target1>csel) && (flowitems[i].instruction.Target1<0xfffc) && (flowitems[i].instruction.Target1!=0xfd) && (flowitems[i].instruction.Target1!=0xfe) && (flowitems[i].instruction.Target1!=0xff))
							flowitems[i].instruction.Target1 --;
						if ((flowitems[i].instruction.Target2>csel) && (flowitems[i].instruction.Target2<0xfffc) && (flowitems[i].instruction.Target2!=0xfd) && (flowitems[i].instruction.Target2!=0xfe) && (flowitems[i].instruction.Target2!=0xff))
							flowitems[i].instruction.Target2 --;

						i++;
					}
				}
				internalchg = true;
				if (csel>=instructions.Length) csel--;
				lldel.Enabled = (instructions.Length>0);
				if (csel>=0) UpdateInstructionDetails(instructions[csel]);
				CreateFlowPanel(instructions);
				wrapper.Changed = true;
			} 
			catch (Exception ex) 
			{
				Helper.ExceptionMessage(Localization.Manager.GetString("errconvert"), ex);
			} 
			finally 
			{
				internalchg = false;
			}
		}

		private void AddOpcodeClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			csel = -1;
			CommitOpcodeClicked(sender, e);
			csel = flowitems.Length-1;
			InstructionPanelClick(flowitems[csel].lable, null);
			llcommit.Enabled = (csel>=0);
		}

		private void CommitOpcodeClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			llcommit.Enabled = false;
			//creat new Instruction if none is selected
			Instruction inst = null;
			try 
			{
				if (csel <0) 
				{
					Bhav wrp = (Bhav)wrapper;
					inst = new Instruction(flowitems.Length, wrp);
				}
				else  
				{
					inst = flowitems[csel].instruction;
					llcommit.Enabled = true;
				}
			} 
			catch (Exception) 
			{
				//Helper.ExceptionMessage(Localization.Manager.GetString("errconvert"), ex);
				return;
			}
			

			try 
			{
				Bhav wrp = (Bhav)wrapper;
				wrp.Changed = true;
				inst.OpCode = Convert.ToUInt16(tbopcode.Text, 16);
				inst.Reserved0 = Convert.ToByte(this.tbres.Text, 16);
				
				if (tba1.SelectedIndex != -1) inst.Target1 = (ushort)(0xFFFC + tba1.SelectedIndex);
				else inst.Target1 = Convert.ToUInt16(tba1.Text, 16);

				if (tba2.SelectedIndex != -1) inst.Target2 = (ushort)(0xFFFC + tba2.SelectedIndex);
				else inst.Target2 = Convert.ToUInt16(tba2.Text, 16);				

				inst.Operands[0] = Convert.ToByte(tbo0.Text, 16);
				inst.Operands[1] = Convert.ToByte(tbo1.Text, 16);
				inst.Operands[2] = Convert.ToByte(tbo2.Text, 16);
				inst.Operands[3] = Convert.ToByte(tbo3.Text, 16);
				inst.Operands[4] = Convert.ToByte(tbo4.Text, 16);
				inst.Operands[5] = Convert.ToByte(tbo5.Text, 16);
				inst.Operands[6] = Convert.ToByte(tbo6.Text, 16);
				inst.Operands[7] = Convert.ToByte(tbo7.Text, 16);

				inst.Reserved1[0] = Convert.ToByte(tbu0.Text, 16);
				inst.Reserved1[1] = Convert.ToByte(tbu1.Text, 16);
				inst.Reserved1[2] = Convert.ToByte(tbu2.Text, 16);
				inst.Reserved1[3] = Convert.ToByte(tbu3.Text, 16);
				inst.Reserved1[4] = Convert.ToByte(tbu4.Text, 16);
				inst.Reserved1[5] = Convert.ToByte(tbu5.Text, 16);
				inst.Reserved1[6] = Convert.ToByte(tbu6.Text, 16);
				inst.Reserved1[7] = Convert.ToByte(tbu7.Text, 16);

				if (csel<0) 
				{					
					Instruction[] ins = new Instruction[flowitems.Length+1];
					InstructionItem.Instructions(flowitems).CopyTo(ins, 0);
					ins[ins.Length-1] = inst;
					CreateFlowPanel(ins);
					
					return;
				} 
				else 
				{
					flowitems[csel].lable.Text = csel.ToString("X")+": "+inst.ToString();
					//CreateFlowPanel(InstructionItem.Instructions(flowitems));
					this.UpdateFlowPanel((Panel)flowitems[csel].lable.Parent, inst, csel);
				}
				
			} 
			catch (Exception) 
			{
				//Helper.ExceptionMessage(Localization.Manager.GetString("errconvert"), ex);
			}
		}

		private void OpenBhavClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{		
			if (llopenbhav.Tag != null) 
			{
				Bhav b = (Bhav)llopenbhav.Tag;				
				
				btcommit.Visible = false;
				lladd.Visible = false;
				lldel.Visible = false;
				llcommit.Visible = false;
				Text = b.FileName;				
				/*				Controls.Remove(bconPanel);*/ // I've no idea what that used to do! PLJ
				bhavPanel.Dock = DockStyle.Fill;

				b.UpdateUI();					
				Show();
			}
		}

		private void AutoChangeBhav(object sender, System.EventArgs e)
		{
			if (this.internalchg) return;
			try 
			{
				this.internalchg = true;
				Bhav wrp = (Bhav)wrapper;				

				wrp.Header.Format = Convert.ToUInt16(tbformat.Text, 16);
				wrp.Header.ArgumentCount = Convert.ToByte(tbargc.Text, 10);
				wrp.Header.LocalVarCount = Convert.ToUInt16(tblocals.Text, 10);
				wrp.Header.Type = Convert.ToByte(tbtype.Text, 16);
				wrp.Header.Flags = Convert.ToUInt16(tbflags.Text, 16);
				wrp.Header.Zero = Convert.ToUInt16(tbzero.Text, 16);

				wrp.FileName = lbbhav.Text;
				wrp.Changed = true;
			} 
			catch (Exception) {}
			finally 
			{
				this.internalchg = false;
			}
		}

		private void LinkInstructionPanelClick(object sender, LinkLabelLinkClickedEventArgs e)
		{
			this.InstructionPanelClick(sender, e);
		}

		private void llmove_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			try 
			{
				int mv = Convert.ToInt32(tbmv.Text);
				int sel = csel;
				for (int i=0; i<Math.Abs(mv); i++) 
				{
					sel = this.MoveItem(sel, (mv<0));
					csel = sel;
				}

				CreateFlowPanel(InstructionItem.Instructions(flowitems));
				if (sel!=-1) InstructionPanelClick(flowitems[sel].lable, null);
			} 
			catch (Exception ex)
			{
				Helper.ExceptionMessage("", ex);
			}
		}

		private void tbmv_TextChanged(object sender, System.EventArgs e)
		{
			try 
			{
				int mv = Convert.ToInt32(tbmv.Text);
				if (mv<0) label45.Text = "lines up";
				else label45.Text = "lines down";
			} 
			catch {}
		}

		private void InstructionPanelClick(object sender, System.EventArgs e)
		{
			llcommit.Enabled = false;
			lldel.Enabled = false;
			SetReadOnly(true);
			try 
			{
				internalchg = true;
				if (csel!=-1) flowitems[csel].lable.BackColor = Color.Transparent;
				Label lb = ((Label)sender);
				csel = (int)lb.Tag;
				Instruction inst = flowitems[csel].instruction;
				UpdateInstructionDetails(inst);		
				flowitems[csel].lable.BackColor =  System.Drawing.Color.PowderBlue;
				DrawConnectors();
				llcommit.Enabled = (csel!=-1);
				lldel.Enabled = (csel!=-1);

				SetReadOnly(!lldel.Enabled);

				//load referenced Bhav
				Bhav b = null;
				if (inst.GlobalBhav) b = Instruction.LoadGlobalBHAV(inst.OpCode);
				llopenbhav.Enabled = (b!=null);
				llopenbhav.Tag = b;
				
			} 
			catch (Exception ex) 
			{
				csel = -1;
				Helper.ExceptionMessage(Localization.Manager.GetString("errconvert"), ex);
			}
			finally 
			{
				internalchg = false;
			}
		}

		private void MoveItemUp(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			int ct = (int)((LinkLabel)sender).Tag;
			int sel = MoveItem(ct, true);
			CreateFlowPanel(InstructionItem.Instructions(flowitems));
			if (sel!=-1) InstructionPanelClick(flowitems[sel].lable, null);
		}

		private void MoveItemDown(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			int ct = (int)((LinkLabel)sender).Tag;
			int sel = MoveItem(ct, false);
			CreateFlowPanel(InstructionItem.Instructions(flowitems));
			if (sel!=-1) InstructionPanelClick(flowitems[sel].lable, null);
		}

		private void SelectTagItem(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			object o = ((LinkLabel)sender).Tag;
			if (o!=null) 
			{
				ushort t = (ushort)o;
				if ((t>=0) && (t<flowitems.Length)) 
				{
					InstructionPanelClick(flowitems[t].lable, null);
				}
			}
		}

		private void ItemQueryContinueDragTarget(object sender, QueryContinueDragEventArgs e)
		{
			if (e.KeyState==0) e.Action = DragAction.Drop;
			else e.Action = DragAction.Continue;
		}

		private void ItemDragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(typeof(int))) 
			{
				e.Effect = DragDropEffects.Link;		
			}
			else 
			{
				e.Effect = DragDropEffects.None;
			}					
		}

		private void ItemDrop(object sender, System.Windows.Forms.DragEventArgs e)
		{
			int sel = 0;
			sel = (int)e.Data.GetData(sel.GetType());
			ComboBox cb = ((ComboBox)sender);
			cb.SelectedIndex = -1;
			cb.Text = "0x"+Helper.HexString((ushort)sel);
		}

		private void ItemMouseMove(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left) 
			{
				((Label)sender).DoDragDrop(((Label)sender).Tag, DragDropEffects.Link);
			}
		}

		private void RepaintFlow(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			//DrawConnectors();
		}

		private void FlowResize(object sender, System.EventArgs e)
		{
			CreateFlowPanel(InstructionItem.Instructions(flowitems));
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
				csel = -1;
				CommitOpcodeClicked(null, null);
				csel = flowitems.Length-1;
				InstructionPanelClick(flowitems[csel].lable, null);
				llcommit.Enabled = (csel>=0);
				flowitems[csel] = i;
			} 
			catch (Exception) {}
			
		}

		private void OpenOperandWiz(object sender, System.EventArgs e)
		{
			try 
			{
				
				if (BhavWizardForm.Available(Convert.ToUInt16(this.tbopcode.Text, 16))) 
				{
					BhavWizardForm bwf = new BhavWizardForm();
					byte[] ret = new byte[0x10];

					ret[0] = Convert.ToByte(tbo0.Text, 16);
					ret[1] = Convert.ToByte(tbo1.Text, 16);
					ret[2] = Convert.ToByte(tbo2.Text, 16);
					ret[3] = Convert.ToByte(tbo3.Text, 16);
					ret[4] = Convert.ToByte(tbo4.Text, 16);
					ret[5] = Convert.ToByte(tbo5.Text, 16);
					ret[6] = Convert.ToByte(tbo6.Text, 16);
					ret[7] = Convert.ToByte(tbo7.Text, 16);

					ret[8] = Convert.ToByte(tbu0.Text, 16);
					ret[9] = Convert.ToByte(tbu1.Text, 16);
					ret[10] = Convert.ToByte(tbu2.Text, 16);
					ret[11] = Convert.ToByte(tbu3.Text, 16);
					ret[12] = Convert.ToByte(tbu4.Text, 16);
					ret[13] = Convert.ToByte(tbu5.Text, 16);
					ret[14] = Convert.ToByte(tbu6.Text, 16);
					ret[15] = Convert.ToByte(tbu7.Text, 16);

					ret = bwf.Execute(Convert.ToUInt16(this.tbopcode.Text, 16), ret);

					if (ret!=null) 
					{
						internalchg = true;
						this.tbo0.Text = Helper.HexString(ret[0]);
						this.tbo1.Text = Helper.HexString(ret[1]);
						this.tbo2.Text = Helper.HexString(ret[2]);
						this.tbo3.Text = Helper.HexString(ret[3]);
						this.tbo4.Text = Helper.HexString(ret[4]);
						this.tbo5.Text = Helper.HexString(ret[5]);
						this.tbo6.Text = Helper.HexString(ret[6]);
						this.tbo7.Text = Helper.HexString(ret[7]);

						this.tbu0.Text = Helper.HexString(ret[8]);
						this.tbu1.Text = Helper.HexString(ret[9]);
						this.tbu2.Text = Helper.HexString(ret[10]);
						this.tbu3.Text = Helper.HexString(ret[11]);
						this.tbu4.Text = Helper.HexString(ret[12]);
						this.tbu5.Text = Helper.HexString(ret[13]);
						this.tbu6.Text = Helper.HexString(ret[14]);
						this.tbu7.Text = Helper.HexString(ret[15]);

						internalchg = false;
						this.AutoChangeInst(sender, e);
					}
				}
			} 
			catch (Exception) 
			{
				btwiz.Enabled = false;
			} 
			finally 
			{
				internalchg = false;
			}
		}
		
#if PLJxx
		private void SortInstructions(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{




			treeNode treeRoot = null;
			try
			{
				treeRoot = new treeNode(null, flowitems[0].instruction);
				treeRoot.fillTree(treeRoot, flowitems);
			}
			catch (Exception ex) 
			{
				Helper.ExceptionMessage(Localization.Manager.GetString("errconvert"), ex);
			}

			InstructionItem[] newFlowItems = new InstructionItem[flowitems.Length];
			ushort last = treeWalk(newFlowItems, 0, treeRoot);

			// That may have missed some entries because they're not used, so...
			foreach(InstructionItem item in flowitems)
			{
				bool found = false;
				if (item != null)
					for (ushort j = 0; !found && (j < newFlowItems.Length); j++)
					{
						InstructionItem jtem = newFlowItems[j];
						if ((jtem != null) && (item.instruction.Equals(jtem.instruction))) found = true;
					}
				if (!found)
				{
					last++;
					newFlowItems[last] = new InstructionItem();
					newFlowItems[last].instruction = item.instruction;
				}
			}
			// Now, if (last+1) != newFlowItems.Length we have a problem!
			for(ushort i = 0; i < newFlowItems.Length; i++)
			{
				if (newFlowItems[i].instruction.Target1 < 0xfffc)
					newFlowItems[i].instruction.Target1 = findItem(newFlowItems, newFlowItems[i].instruction.Target1);
				if (newFlowItems[i].instruction.Target2 < 0xfffc)
					newFlowItems[i].instruction.Target2 = findItem(newFlowItems, newFlowItems[i].instruction.Target2);
				Instruction x = newFlowItems[i].instruction;
				newFlowItems[i] = new InstructionItem();
				newFlowItems[i].instruction = x;
			}

			for(ushort i = 0; i < newFlowItems.Length; i++)
			{
				newFlowItems[i].instruction.Index = (ushort)i;
				newFlowItems[i].index = (ushort)i;
			}


			csel = -1;
			this.llcommit.Enabled = false;
			this.lldel.Enabled = false;
			CreateFlowPanel(InstructionItem.Instructions(newFlowItems));
			((Bhav)wrapper).Instructions = InstructionItem.Instructions(newFlowItems);
			wrapper.Changed = true;		 
		}
#else
		private void SortInstructions(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			ushort last = 0;
			for (ushort i=0; i<flowitems.Length-1; i++) 
			{
				if (flowitems[i].instruction.Target1 > i+1) 
				{
					/*for (ushort k=0; k<flowitems.Length-1; k++) 
					{
						if (flowitems[k].instruction.Target2 > last) 
						{
							if (flowitems[k].instruction.Target2 < 0xfffc) SortSwap(flowitems[k].instruction.Target2, last);
							last++;
						}
					}*/

					if (flowitems[i].instruction.Target1 < 0xfffc) SortSwap(flowitems[i].instruction.Target1, (ushort)(i+1));
					else 
					{
						if (flowitems[i].instruction.Target2 > i+1) 
						{
							if (flowitems[i].instruction.Target2 < 0xfffc) SortSwap(flowitems[i].instruction.Target2, (ushort)(i+1));
						}
					}
					last = i;
				}
			}

			for (ushort i=0; i<flowitems.Length-1; i++) 
			{
				if (flowitems[i].instruction.Target2 > last) 
				{
					if (flowitems[i].instruction.Target2 < 0xfffc) SortSwap(flowitems[i].instruction.Target2, last);
					last++;
				}
			}

			csel = -1;
			this.llcommit.Enabled = false;
			this.lldel.Enabled = false;
			CreateFlowPanel(InstructionItem.Instructions(flowitems));
			wrapper.Changed = true;
		}

#endif
		#endregion
	}
}
