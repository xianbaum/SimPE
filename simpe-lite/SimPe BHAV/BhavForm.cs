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
	/// Summary description for BhavForm.
	/// </summary>
	public class BhavForm : System.Windows.Forms.Form, IPackedFileUI
	{
		#region Form variables
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Panel pnHeading;
		private BhavInstListControl pnflowcontainer;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox tbFilename;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox tbFormat;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox tbType;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox tbArgC;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox tbLocalC;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox tbFlags;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox tbReserved;
		private System.Windows.Forms.ComboBox tba1;
		private System.Windows.Forms.ComboBox tba2;
		private System.Windows.Forms.LinkLabel llopenbhav;
		private System.Windows.Forms.LinkLabel llmove;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Label lbUpDown;
		private System.Windows.Forms.TextBox tbInst_OpCode;
		private System.Windows.Forms.TextBox tbInst_Reserved;
		private System.Windows.Forms.TextBox tbInst_Op7;
		private System.Windows.Forms.TextBox tbInst_Op6;
		private System.Windows.Forms.TextBox tbInst_Op5;
		private System.Windows.Forms.TextBox tbInst_Op4;
		private System.Windows.Forms.TextBox tbInst_Op3;
		private System.Windows.Forms.TextBox tbInst_Op2;
		private System.Windows.Forms.TextBox tbInst_Op1;
		private System.Windows.Forms.TextBox tbInst_Op0;
		private System.Windows.Forms.TextBox tbInst_Unk7;
		private System.Windows.Forms.TextBox tbInst_Unk6;
		private System.Windows.Forms.TextBox tbInst_Unk5;
		private System.Windows.Forms.TextBox tbInst_Unk4;
		private System.Windows.Forms.TextBox tbInst_Unk3;
		private System.Windows.Forms.TextBox tbInst_Unk2;
		private System.Windows.Forms.TextBox tbInst_Unk1;
		private System.Windows.Forms.TextBox tbInst_Unk0;
		private System.Windows.Forms.TextBox tbInst_Instruction;
		private System.Windows.Forms.LinkLabel lladd;
		private System.Windows.Forms.LinkLabel lldel;
		private System.Windows.Forms.TextBox tbLines;
		private System.Windows.Forms.GroupBox gbInstruction;
		private System.Windows.Forms.Panel bhavPanel;
		private System.Windows.Forms.Button btnCommit;
		private System.Windows.Forms.LinkLabel llcancel;
		private System.Windows.Forms.Button btnOpCode;
		private System.Windows.Forms.Button btnOperandWiz;
		private System.Windows.Forms.Button btnSort;
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

		
		#region BhavForm
		private Bhav wrapper;
		private Instruction origInst;
		private bool internalchg;

		private void SetReadOnly(bool state) 
		{
			if (!btnCommit.Visible) state = true;

			tbInst_OpCode.ReadOnly = state;
			btnOpCode.Enabled = !state;
			tbInst_Reserved.ReadOnly = state;
			tba1.Enabled = !state;
			tba2.Enabled = !state;

			this.tbInst_Op0.ReadOnly = state;
			this.tbInst_Op1.ReadOnly = state;
			this.tbInst_Op2.ReadOnly = state;
			this.tbInst_Op3.ReadOnly = state;
			this.tbInst_Op4.ReadOnly = state;
			this.tbInst_Op5.ReadOnly = state;
			this.tbInst_Op6.ReadOnly = state;
			this.tbInst_Op7.ReadOnly = state;
			
			this.tbInst_Unk0.ReadOnly = state;
			this.tbInst_Unk1.ReadOnly = state;
			this.tbInst_Unk2.ReadOnly = state;
			this.tbInst_Unk3.ReadOnly = state;
			this.tbInst_Unk4.ReadOnly = state;
			this.tbInst_Unk5.ReadOnly = state;
			this.tbInst_Unk6.ReadOnly = state;
			this.tbInst_Unk7.ReadOnly = state;

			//btnOperandWiz.Enabled = !state;

			llmove.Enabled = !state;
			tbLines.ReadOnly = state;
		}

		private void UpdateInstPanel(Instruction inst)
		{
			internalchg = true;
			if (inst != null)
			{
				//load referenced Bhav
				Bhav b = null;
				if (inst.GlobalBhav)
					b = Instruction.LoadGlobalBHAV(inst.OpCode);
				llopenbhav.Enabled = (b!=null);

				this.tbInst_OpCode.Text = "0x"+Helper.HexString(inst.OpCode);

				this.tbInst_Reserved.Text = "0x"+Helper.HexString(inst.Reserved0);
				if (inst.Target1 >= 0xFFFC)
				{
					this.tba1.SelectedIndex = inst.Target1 - 0xFFFC;
				}
				else
				{
					this.tba1.SelectedIndex = -1;
					this.tba1.Text = "0x"+Helper.HexString(inst.Target1);
				}
				if (inst.Target2 >= 0xFFFC)
				{
					this.tba2.SelectedIndex = inst.Target2 - 0xFFFC;
				}
				else
				{
					this.tba2.SelectedIndex = -1;
					this.tba2.Text = "0x"+Helper.HexString(inst.Target2);
				}
				this.tbInst_Op0.Text = Helper.HexString(inst.Operands[0]);
				this.tbInst_Op1.Text = Helper.HexString(inst.Operands[1]);
				this.tbInst_Op2.Text = Helper.HexString(inst.Operands[2]);
				this.tbInst_Op3.Text = Helper.HexString(inst.Operands[3]);
				this.tbInst_Op4.Text = Helper.HexString(inst.Operands[4]);
				this.tbInst_Op5.Text = Helper.HexString(inst.Operands[5]);
				this.tbInst_Op6.Text = Helper.HexString(inst.Operands[6]);
				this.tbInst_Op7.Text = Helper.HexString(inst.Operands[7]);

				this.btnOperandWiz.Enabled = BhavOperandWiz.Available(inst);

				this.tbInst_Unk0.Text = Helper.HexString(inst.Reserved1[0]);
				this.tbInst_Unk1.Text = Helper.HexString(inst.Reserved1[1]);
				this.tbInst_Unk2.Text = Helper.HexString(inst.Reserved1[2]);
				this.tbInst_Unk3.Text = Helper.HexString(inst.Reserved1[3]);
				this.tbInst_Unk4.Text = Helper.HexString(inst.Reserved1[4]);
				this.tbInst_Unk5.Text = Helper.HexString(inst.Reserved1[5]);
				this.tbInst_Unk6.Text = Helper.HexString(inst.Reserved1[6]);
				this.tbInst_Unk7.Text = Helper.HexString(inst.Reserved1[7]);

				this.tbInst_Instruction.Text = inst.ToString();
				SetReadOnly(false);
				lldel.Enabled = true;
			}
			else
			{
				llopenbhav.Enabled = false;
				this.tbInst_OpCode.Text = "";
				this.tbInst_Reserved.Text = "";
				this.tba1.SelectedIndex = 0;
				this.tba2.SelectedIndex = 0;
				this.tbInst_Op0.Text = "";
				this.tbInst_Op1.Text = "";
				this.tbInst_Op2.Text = "";
				this.tbInst_Op3.Text = "";
				this.tbInst_Op4.Text = "";
				this.tbInst_Op5.Text = "";
				this.tbInst_Op6.Text = "";
				this.tbInst_Op7.Text = "";
				this.btnOperandWiz.Enabled = false;
				this.tbInst_Unk0.Text = "";
				this.tbInst_Unk1.Text = "";
				this.tbInst_Unk2.Text = "";
				this.tbInst_Unk3.Text = "";
				this.tbInst_Unk4.Text = "";
				this.tbInst_Unk5.Text = "";
				this.tbInst_Unk6.Text = "";
				this.tbInst_Unk7.Text = "";

				this.tbInst_Instruction.Text = "";
				SetReadOnly(true);
				lldel.Enabled = false;
			}
			llcancel.Enabled = false;
			internalchg = false;
		}

		private void SendInst(Instruction currentInst)
		{
			this.tbInst_Instruction.Text = currentInst.ToString();
			this.llcancel.Enabled = true;
			bool origstate = internalchg;
			internalchg = true;
			this.pnflowcontainer.SelectedInst = currentInst;
			internalchg = origstate;
		}
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
			wrapper.WrapperChanged += new System.EventHandler(this.WrapperChanged);
			origInst = null;

			internalchg = true;
			tbFilename.Text = wrapper.FileName;
			tbArgC.Text = wrapper.Header.ArgumentCount.ToString();
			tbFlags.Text = "0x"+Helper.HexString(wrapper.Header.Flags);
			tbFormat.Text = "0x"+Helper.HexString(wrapper.Header.Format);
			tbLocalC.Text = wrapper.Header.LocalVarCount.ToString();
			tbType.Text = "0x"+Helper.HexString(wrapper.Header.Type);
			tbReserved.Text = "0x"+Helper.HexString(wrapper.Header.Zero);

			this.btnCommit.Enabled = wrapper.Changed;

			internalchg = false;
			this.pnflowcontainer.UpdateGUI(wrapper);
		}

		private void WrapperChanged(object sender, System.EventArgs e)
		{
			this.btnCommit.Enabled = true;
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
			this.pnflowcontainer = new SimPe.PackedFiles.UserInterface.BhavInstListControl();
			this.label1 = new System.Windows.Forms.Label();
			this.gbInstruction = new System.Windows.Forms.GroupBox();
			this.lbUpDown = new System.Windows.Forms.Label();
			this.llmove = new System.Windows.Forms.LinkLabel();
			this.tbLines = new System.Windows.Forms.TextBox();
			this.btnOperandWiz = new System.Windows.Forms.Button();
			this.llopenbhav = new System.Windows.Forms.LinkLabel();
			this.tba2 = new System.Windows.Forms.ComboBox();
			this.tba1 = new System.Windows.Forms.ComboBox();
			this.lldel = new System.Windows.Forms.LinkLabel();
			this.lladd = new System.Windows.Forms.LinkLabel();
			this.llcancel = new System.Windows.Forms.LinkLabel();
			this.label14 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.tbInst_Unk7 = new System.Windows.Forms.TextBox();
			this.tbInst_Unk6 = new System.Windows.Forms.TextBox();
			this.tbInst_Unk5 = new System.Windows.Forms.TextBox();
			this.tbInst_Unk4 = new System.Windows.Forms.TextBox();
			this.tbInst_Unk3 = new System.Windows.Forms.TextBox();
			this.tbInst_Unk2 = new System.Windows.Forms.TextBox();
			this.tbInst_Unk1 = new System.Windows.Forms.TextBox();
			this.tbInst_Unk0 = new System.Windows.Forms.TextBox();
			this.tbInst_Op7 = new System.Windows.Forms.TextBox();
			this.tbInst_Op6 = new System.Windows.Forms.TextBox();
			this.tbInst_Op5 = new System.Windows.Forms.TextBox();
			this.tbInst_Op4 = new System.Windows.Forms.TextBox();
			this.tbInst_Op3 = new System.Windows.Forms.TextBox();
			this.tbInst_Op2 = new System.Windows.Forms.TextBox();
			this.tbInst_Op1 = new System.Windows.Forms.TextBox();
			this.tbInst_Op0 = new System.Windows.Forms.TextBox();
			this.tbInst_Reserved = new System.Windows.Forms.TextBox();
			this.tbInst_OpCode = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.btnOpCode = new System.Windows.Forms.Button();
			this.tbInst_Instruction = new System.Windows.Forms.TextBox();
			this.tbFilename = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.tbReserved = new System.Windows.Forms.TextBox();
			this.tbLocalC = new System.Windows.Forms.TextBox();
			this.tbFlags = new System.Windows.Forms.TextBox();
			this.tbArgC = new System.Windows.Forms.TextBox();
			this.tbType = new System.Windows.Forms.TextBox();
			this.tbFormat = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.pnHeading = new System.Windows.Forms.Panel();
			this.bhavPanel = new System.Windows.Forms.Panel();
			this.btnSort = new System.Windows.Forms.Button();
			this.btnCommit = new System.Windows.Forms.Button();
			this.gbInstruction.SuspendLayout();
			this.pnHeading.SuspendLayout();
			this.bhavPanel.SuspendLayout();
			this.SuspendLayout();
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
			this.pnflowcontainer.SelectedInst = null;
			this.pnflowcontainer.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("pnflowcontainer.Dock")));
			this.pnflowcontainer.Enabled = ((bool)(resources.GetObject("pnflowcontainer.Enabled")));
			this.pnflowcontainer.Font = ((System.Drawing.Font)(resources.GetObject("pnflowcontainer.Font")));
			this.pnflowcontainer.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("pnflowcontainer.ImeMode")));
			this.pnflowcontainer.Location = ((System.Drawing.Point)(resources.GetObject("pnflowcontainer.Location")));
			this.pnflowcontainer.Name = "pnflowcontainer";
			this.pnflowcontainer.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("pnflowcontainer.RightToLeft")));
			this.pnflowcontainer.SelectedIndex = -1;
			this.pnflowcontainer.Size = ((System.Drawing.Size)(resources.GetObject("pnflowcontainer.Size")));
			this.pnflowcontainer.TabIndex = ((int)(resources.GetObject("pnflowcontainer.TabIndex")));
			this.pnflowcontainer.Visible = ((bool)(resources.GetObject("pnflowcontainer.Visible")));
			this.pnflowcontainer.SelectedInstChanged += new System.EventHandler(this.pnflowcontainer_SelectedInstChanged);
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
			this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
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
			// gbInstruction
			// 
			this.gbInstruction.AccessibleDescription = resources.GetString("gbInstruction.AccessibleDescription");
			this.gbInstruction.AccessibleName = resources.GetString("gbInstruction.AccessibleName");
			this.gbInstruction.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("gbInstruction.Anchor")));
			this.gbInstruction.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("gbInstruction.BackgroundImage")));
			this.gbInstruction.Controls.Add(this.lbUpDown);
			this.gbInstruction.Controls.Add(this.llmove);
			this.gbInstruction.Controls.Add(this.tbLines);
			this.gbInstruction.Controls.Add(this.btnOperandWiz);
			this.gbInstruction.Controls.Add(this.llopenbhav);
			this.gbInstruction.Controls.Add(this.tba2);
			this.gbInstruction.Controls.Add(this.tba1);
			this.gbInstruction.Controls.Add(this.lldel);
			this.gbInstruction.Controls.Add(this.lladd);
			this.gbInstruction.Controls.Add(this.llcancel);
			this.gbInstruction.Controls.Add(this.label14);
			this.gbInstruction.Controls.Add(this.label13);
			this.gbInstruction.Controls.Add(this.tbInst_Unk7);
			this.gbInstruction.Controls.Add(this.tbInst_Unk6);
			this.gbInstruction.Controls.Add(this.tbInst_Unk5);
			this.gbInstruction.Controls.Add(this.tbInst_Unk4);
			this.gbInstruction.Controls.Add(this.tbInst_Unk3);
			this.gbInstruction.Controls.Add(this.tbInst_Unk2);
			this.gbInstruction.Controls.Add(this.tbInst_Unk1);
			this.gbInstruction.Controls.Add(this.tbInst_Unk0);
			this.gbInstruction.Controls.Add(this.tbInst_Op7);
			this.gbInstruction.Controls.Add(this.tbInst_Op6);
			this.gbInstruction.Controls.Add(this.tbInst_Op5);
			this.gbInstruction.Controls.Add(this.tbInst_Op4);
			this.gbInstruction.Controls.Add(this.tbInst_Op3);
			this.gbInstruction.Controls.Add(this.tbInst_Op2);
			this.gbInstruction.Controls.Add(this.tbInst_Op1);
			this.gbInstruction.Controls.Add(this.tbInst_Op0);
			this.gbInstruction.Controls.Add(this.tbInst_Reserved);
			this.gbInstruction.Controls.Add(this.tbInst_OpCode);
			this.gbInstruction.Controls.Add(this.label10);
			this.gbInstruction.Controls.Add(this.label9);
			this.gbInstruction.Controls.Add(this.label12);
			this.gbInstruction.Controls.Add(this.label11);
			this.gbInstruction.Controls.Add(this.btnOpCode);
			this.gbInstruction.Controls.Add(this.tbInst_Instruction);
			this.gbInstruction.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("gbInstruction.Dock")));
			this.gbInstruction.Enabled = ((bool)(resources.GetObject("gbInstruction.Enabled")));
			this.gbInstruction.Font = ((System.Drawing.Font)(resources.GetObject("gbInstruction.Font")));
			this.gbInstruction.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("gbInstruction.ImeMode")));
			this.gbInstruction.Location = ((System.Drawing.Point)(resources.GetObject("gbInstruction.Location")));
			this.gbInstruction.Name = "gbInstruction";
			this.gbInstruction.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("gbInstruction.RightToLeft")));
			this.gbInstruction.Size = ((System.Drawing.Size)(resources.GetObject("gbInstruction.Size")));
			this.gbInstruction.TabIndex = ((int)(resources.GetObject("gbInstruction.TabIndex")));
			this.gbInstruction.TabStop = false;
			this.gbInstruction.Text = resources.GetString("gbInstruction.Text");
			this.gbInstruction.Visible = ((bool)(resources.GetObject("gbInstruction.Visible")));
			// 
			// lbUpDown
			// 
			this.lbUpDown.AccessibleDescription = resources.GetString("lbUpDown.AccessibleDescription");
			this.lbUpDown.AccessibleName = resources.GetString("lbUpDown.AccessibleName");
			this.lbUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbUpDown.Anchor")));
			this.lbUpDown.AutoSize = ((bool)(resources.GetObject("lbUpDown.AutoSize")));
			this.lbUpDown.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbUpDown.Dock")));
			this.lbUpDown.Enabled = ((bool)(resources.GetObject("lbUpDown.Enabled")));
			this.lbUpDown.Font = ((System.Drawing.Font)(resources.GetObject("lbUpDown.Font")));
			this.lbUpDown.Image = ((System.Drawing.Image)(resources.GetObject("lbUpDown.Image")));
			this.lbUpDown.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbUpDown.ImageAlign")));
			this.lbUpDown.ImageIndex = ((int)(resources.GetObject("lbUpDown.ImageIndex")));
			this.lbUpDown.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbUpDown.ImeMode")));
			this.lbUpDown.Location = ((System.Drawing.Point)(resources.GetObject("lbUpDown.Location")));
			this.lbUpDown.Name = "lbUpDown";
			this.lbUpDown.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbUpDown.RightToLeft")));
			this.lbUpDown.Size = ((System.Drawing.Size)(resources.GetObject("lbUpDown.Size")));
			this.lbUpDown.TabIndex = ((int)(resources.GetObject("lbUpDown.TabIndex")));
			this.lbUpDown.Text = resources.GetString("lbUpDown.Text");
			this.lbUpDown.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbUpDown.TextAlign")));
			this.lbUpDown.Visible = ((bool)(resources.GetObject("lbUpDown.Visible")));
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
			// tbLines
			// 
			this.tbLines.AccessibleDescription = resources.GetString("tbLines.AccessibleDescription");
			this.tbLines.AccessibleName = resources.GetString("tbLines.AccessibleName");
			this.tbLines.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbLines.Anchor")));
			this.tbLines.AutoSize = ((bool)(resources.GetObject("tbLines.AutoSize")));
			this.tbLines.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbLines.BackgroundImage")));
			this.tbLines.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbLines.Dock")));
			this.tbLines.Enabled = ((bool)(resources.GetObject("tbLines.Enabled")));
			this.tbLines.Font = ((System.Drawing.Font)(resources.GetObject("tbLines.Font")));
			this.tbLines.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbLines.ImeMode")));
			this.tbLines.Location = ((System.Drawing.Point)(resources.GetObject("tbLines.Location")));
			this.tbLines.MaxLength = ((int)(resources.GetObject("tbLines.MaxLength")));
			this.tbLines.Multiline = ((bool)(resources.GetObject("tbLines.Multiline")));
			this.tbLines.Name = "tbLines";
			this.tbLines.PasswordChar = ((char)(resources.GetObject("tbLines.PasswordChar")));
			this.tbLines.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbLines.RightToLeft")));
			this.tbLines.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbLines.ScrollBars")));
			this.tbLines.Size = ((System.Drawing.Size)(resources.GetObject("tbLines.Size")));
			this.tbLines.TabIndex = ((int)(resources.GetObject("tbLines.TabIndex")));
			this.tbLines.Text = resources.GetString("tbLines.Text");
			this.tbLines.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbLines.TextAlign")));
			this.tbLines.Visible = ((bool)(resources.GetObject("tbLines.Visible")));
			this.tbLines.WordWrap = ((bool)(resources.GetObject("tbLines.WordWrap")));
			this.tbLines.TextChanged += new System.EventHandler(this.tbmv_TextChanged);
			// 
			// btnOperandWiz
			// 
			this.btnOperandWiz.AccessibleDescription = resources.GetString("btnOperandWiz.AccessibleDescription");
			this.btnOperandWiz.AccessibleName = resources.GetString("btnOperandWiz.AccessibleName");
			this.btnOperandWiz.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnOperandWiz.Anchor")));
			this.btnOperandWiz.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOperandWiz.BackgroundImage")));
			this.btnOperandWiz.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnOperandWiz.Dock")));
			this.btnOperandWiz.Enabled = ((bool)(resources.GetObject("btnOperandWiz.Enabled")));
			this.btnOperandWiz.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnOperandWiz.FlatStyle")));
			this.btnOperandWiz.Font = ((System.Drawing.Font)(resources.GetObject("btnOperandWiz.Font")));
			this.btnOperandWiz.Image = ((System.Drawing.Image)(resources.GetObject("btnOperandWiz.Image")));
			this.btnOperandWiz.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnOperandWiz.ImageAlign")));
			this.btnOperandWiz.ImageIndex = ((int)(resources.GetObject("btnOperandWiz.ImageIndex")));
			this.btnOperandWiz.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnOperandWiz.ImeMode")));
			this.btnOperandWiz.Location = ((System.Drawing.Point)(resources.GetObject("btnOperandWiz.Location")));
			this.btnOperandWiz.Name = "btnOperandWiz";
			this.btnOperandWiz.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnOperandWiz.RightToLeft")));
			this.btnOperandWiz.Size = ((System.Drawing.Size)(resources.GetObject("btnOperandWiz.Size")));
			this.btnOperandWiz.TabIndex = ((int)(resources.GetObject("btnOperandWiz.TabIndex")));
			this.btnOperandWiz.Text = resources.GetString("btnOperandWiz.Text");
			this.btnOperandWiz.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnOperandWiz.TextAlign")));
			this.btnOperandWiz.Visible = ((bool)(resources.GetObject("btnOperandWiz.Visible")));
			this.btnOperandWiz.Click += new System.EventHandler(this.btnOperandWiz_Clicked);
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
			this.llopenbhav.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llopenbhav_LinkClicked);
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
			this.tba2.Items.AddRange(new object[] {
													  resources.GetString("tba2.Items"),
													  resources.GetString("tba2.Items1"),
													  resources.GetString("tba2.Items2")});
			this.tba2.Location = ((System.Drawing.Point)(resources.GetObject("tba2.Location")));
			this.tba2.MaxDropDownItems = ((int)(resources.GetObject("tba2.MaxDropDownItems")));
			this.tba2.MaxLength = ((int)(resources.GetObject("tba2.MaxLength")));
			this.tba2.Name = "tba2";
			this.tba2.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tba2.RightToLeft")));
			this.tba2.Size = ((System.Drawing.Size)(resources.GetObject("tba2.Size")));
			this.tba2.TabIndex = ((int)(resources.GetObject("tba2.TabIndex")));
			this.tba2.Text = resources.GetString("tba2.Text");
			this.tba2.Visible = ((bool)(resources.GetObject("tba2.Visible")));
			this.tba2.DragOver += new System.Windows.Forms.DragEventHandler(this.ItemDragEnter);
			this.tba2.DragDrop += new System.Windows.Forms.DragEventHandler(this.ItemDrop);
			this.tba2.TextChanged += new System.EventHandler(this.Target_TextChanged);
			this.tba2.SelectedIndexChanged += new System.EventHandler(this.Target_SelectedIndexChanged);
			this.tba2.QueryContinueDrag += new System.Windows.Forms.QueryContinueDragEventHandler(this.ItemQueryContinueDragTarget);
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
			this.tba1.Items.AddRange(new object[] {
													  resources.GetString("tba1.Items"),
													  resources.GetString("tba1.Items1"),
													  resources.GetString("tba1.Items2")});
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
			this.tba1.TextChanged += new System.EventHandler(this.Target_TextChanged);
			this.tba1.SelectedIndexChanged += new System.EventHandler(this.Target_SelectedIndexChanged);
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
			this.lldel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lldel_LinkClicked);
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
			this.lladd.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lladd_LinkClicked);
			// 
			// llcancel
			// 
			this.llcancel.AccessibleDescription = resources.GetString("llcancel.AccessibleDescription");
			this.llcancel.AccessibleName = resources.GetString("llcancel.AccessibleName");
			this.llcancel.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("llcancel.Anchor")));
			this.llcancel.AutoSize = ((bool)(resources.GetObject("llcancel.AutoSize")));
			this.llcancel.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("llcancel.Dock")));
			this.llcancel.Enabled = ((bool)(resources.GetObject("llcancel.Enabled")));
			this.llcancel.Font = ((System.Drawing.Font)(resources.GetObject("llcancel.Font")));
			this.llcancel.Image = ((System.Drawing.Image)(resources.GetObject("llcancel.Image")));
			this.llcancel.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("llcancel.ImageAlign")));
			this.llcancel.ImageIndex = ((int)(resources.GetObject("llcancel.ImageIndex")));
			this.llcancel.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("llcancel.ImeMode")));
			this.llcancel.LinkArea = ((System.Windows.Forms.LinkArea)(resources.GetObject("llcancel.LinkArea")));
			this.llcancel.Location = ((System.Drawing.Point)(resources.GetObject("llcancel.Location")));
			this.llcancel.Name = "llcancel";
			this.llcancel.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("llcancel.RightToLeft")));
			this.llcancel.Size = ((System.Drawing.Size)(resources.GetObject("llcancel.Size")));
			this.llcancel.TabIndex = ((int)(resources.GetObject("llcancel.TabIndex")));
			this.llcancel.TabStop = true;
			this.llcancel.Text = resources.GetString("llcancel.Text");
			this.llcancel.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("llcancel.TextAlign")));
			this.llcancel.Visible = ((bool)(resources.GetObject("llcancel.Visible")));
			this.llcancel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llcancel_LinkClicked);
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
			// tbInst_Unk7
			// 
			this.tbInst_Unk7.AccessibleDescription = resources.GetString("tbInst_Unk7.AccessibleDescription");
			this.tbInst_Unk7.AccessibleName = resources.GetString("tbInst_Unk7.AccessibleName");
			this.tbInst_Unk7.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbInst_Unk7.Anchor")));
			this.tbInst_Unk7.AutoSize = ((bool)(resources.GetObject("tbInst_Unk7.AutoSize")));
			this.tbInst_Unk7.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbInst_Unk7.BackgroundImage")));
			this.tbInst_Unk7.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbInst_Unk7.Dock")));
			this.tbInst_Unk7.Enabled = ((bool)(resources.GetObject("tbInst_Unk7.Enabled")));
			this.tbInst_Unk7.Font = ((System.Drawing.Font)(resources.GetObject("tbInst_Unk7.Font")));
			this.tbInst_Unk7.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbInst_Unk7.ImeMode")));
			this.tbInst_Unk7.Location = ((System.Drawing.Point)(resources.GetObject("tbInst_Unk7.Location")));
			this.tbInst_Unk7.MaxLength = ((int)(resources.GetObject("tbInst_Unk7.MaxLength")));
			this.tbInst_Unk7.Multiline = ((bool)(resources.GetObject("tbInst_Unk7.Multiline")));
			this.tbInst_Unk7.Name = "tbInst_Unk7";
			this.tbInst_Unk7.PasswordChar = ((char)(resources.GetObject("tbInst_Unk7.PasswordChar")));
			this.tbInst_Unk7.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbInst_Unk7.RightToLeft")));
			this.tbInst_Unk7.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbInst_Unk7.ScrollBars")));
			this.tbInst_Unk7.Size = ((System.Drawing.Size)(resources.GetObject("tbInst_Unk7.Size")));
			this.tbInst_Unk7.TabIndex = ((int)(resources.GetObject("tbInst_Unk7.TabIndex")));
			this.tbInst_Unk7.Text = resources.GetString("tbInst_Unk7.Text");
			this.tbInst_Unk7.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbInst_Unk7.TextAlign")));
			this.tbInst_Unk7.Visible = ((bool)(resources.GetObject("tbInst_Unk7.Visible")));
			this.tbInst_Unk7.WordWrap = ((bool)(resources.GetObject("tbInst_Unk7.WordWrap")));
			this.tbInst_Unk7.TextChanged += new System.EventHandler(this.Byte_TextChanged);
			// 
			// tbInst_Unk6
			// 
			this.tbInst_Unk6.AccessibleDescription = resources.GetString("tbInst_Unk6.AccessibleDescription");
			this.tbInst_Unk6.AccessibleName = resources.GetString("tbInst_Unk6.AccessibleName");
			this.tbInst_Unk6.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbInst_Unk6.Anchor")));
			this.tbInst_Unk6.AutoSize = ((bool)(resources.GetObject("tbInst_Unk6.AutoSize")));
			this.tbInst_Unk6.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbInst_Unk6.BackgroundImage")));
			this.tbInst_Unk6.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbInst_Unk6.Dock")));
			this.tbInst_Unk6.Enabled = ((bool)(resources.GetObject("tbInst_Unk6.Enabled")));
			this.tbInst_Unk6.Font = ((System.Drawing.Font)(resources.GetObject("tbInst_Unk6.Font")));
			this.tbInst_Unk6.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbInst_Unk6.ImeMode")));
			this.tbInst_Unk6.Location = ((System.Drawing.Point)(resources.GetObject("tbInst_Unk6.Location")));
			this.tbInst_Unk6.MaxLength = ((int)(resources.GetObject("tbInst_Unk6.MaxLength")));
			this.tbInst_Unk6.Multiline = ((bool)(resources.GetObject("tbInst_Unk6.Multiline")));
			this.tbInst_Unk6.Name = "tbInst_Unk6";
			this.tbInst_Unk6.PasswordChar = ((char)(resources.GetObject("tbInst_Unk6.PasswordChar")));
			this.tbInst_Unk6.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbInst_Unk6.RightToLeft")));
			this.tbInst_Unk6.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbInst_Unk6.ScrollBars")));
			this.tbInst_Unk6.Size = ((System.Drawing.Size)(resources.GetObject("tbInst_Unk6.Size")));
			this.tbInst_Unk6.TabIndex = ((int)(resources.GetObject("tbInst_Unk6.TabIndex")));
			this.tbInst_Unk6.Text = resources.GetString("tbInst_Unk6.Text");
			this.tbInst_Unk6.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbInst_Unk6.TextAlign")));
			this.tbInst_Unk6.Visible = ((bool)(resources.GetObject("tbInst_Unk6.Visible")));
			this.tbInst_Unk6.WordWrap = ((bool)(resources.GetObject("tbInst_Unk6.WordWrap")));
			this.tbInst_Unk6.TextChanged += new System.EventHandler(this.Byte_TextChanged);
			// 
			// tbInst_Unk5
			// 
			this.tbInst_Unk5.AccessibleDescription = resources.GetString("tbInst_Unk5.AccessibleDescription");
			this.tbInst_Unk5.AccessibleName = resources.GetString("tbInst_Unk5.AccessibleName");
			this.tbInst_Unk5.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbInst_Unk5.Anchor")));
			this.tbInst_Unk5.AutoSize = ((bool)(resources.GetObject("tbInst_Unk5.AutoSize")));
			this.tbInst_Unk5.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbInst_Unk5.BackgroundImage")));
			this.tbInst_Unk5.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbInst_Unk5.Dock")));
			this.tbInst_Unk5.Enabled = ((bool)(resources.GetObject("tbInst_Unk5.Enabled")));
			this.tbInst_Unk5.Font = ((System.Drawing.Font)(resources.GetObject("tbInst_Unk5.Font")));
			this.tbInst_Unk5.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbInst_Unk5.ImeMode")));
			this.tbInst_Unk5.Location = ((System.Drawing.Point)(resources.GetObject("tbInst_Unk5.Location")));
			this.tbInst_Unk5.MaxLength = ((int)(resources.GetObject("tbInst_Unk5.MaxLength")));
			this.tbInst_Unk5.Multiline = ((bool)(resources.GetObject("tbInst_Unk5.Multiline")));
			this.tbInst_Unk5.Name = "tbInst_Unk5";
			this.tbInst_Unk5.PasswordChar = ((char)(resources.GetObject("tbInst_Unk5.PasswordChar")));
			this.tbInst_Unk5.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbInst_Unk5.RightToLeft")));
			this.tbInst_Unk5.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbInst_Unk5.ScrollBars")));
			this.tbInst_Unk5.Size = ((System.Drawing.Size)(resources.GetObject("tbInst_Unk5.Size")));
			this.tbInst_Unk5.TabIndex = ((int)(resources.GetObject("tbInst_Unk5.TabIndex")));
			this.tbInst_Unk5.Text = resources.GetString("tbInst_Unk5.Text");
			this.tbInst_Unk5.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbInst_Unk5.TextAlign")));
			this.tbInst_Unk5.Visible = ((bool)(resources.GetObject("tbInst_Unk5.Visible")));
			this.tbInst_Unk5.WordWrap = ((bool)(resources.GetObject("tbInst_Unk5.WordWrap")));
			this.tbInst_Unk5.TextChanged += new System.EventHandler(this.Byte_TextChanged);
			// 
			// tbInst_Unk4
			// 
			this.tbInst_Unk4.AccessibleDescription = resources.GetString("tbInst_Unk4.AccessibleDescription");
			this.tbInst_Unk4.AccessibleName = resources.GetString("tbInst_Unk4.AccessibleName");
			this.tbInst_Unk4.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbInst_Unk4.Anchor")));
			this.tbInst_Unk4.AutoSize = ((bool)(resources.GetObject("tbInst_Unk4.AutoSize")));
			this.tbInst_Unk4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbInst_Unk4.BackgroundImage")));
			this.tbInst_Unk4.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbInst_Unk4.Dock")));
			this.tbInst_Unk4.Enabled = ((bool)(resources.GetObject("tbInst_Unk4.Enabled")));
			this.tbInst_Unk4.Font = ((System.Drawing.Font)(resources.GetObject("tbInst_Unk4.Font")));
			this.tbInst_Unk4.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbInst_Unk4.ImeMode")));
			this.tbInst_Unk4.Location = ((System.Drawing.Point)(resources.GetObject("tbInst_Unk4.Location")));
			this.tbInst_Unk4.MaxLength = ((int)(resources.GetObject("tbInst_Unk4.MaxLength")));
			this.tbInst_Unk4.Multiline = ((bool)(resources.GetObject("tbInst_Unk4.Multiline")));
			this.tbInst_Unk4.Name = "tbInst_Unk4";
			this.tbInst_Unk4.PasswordChar = ((char)(resources.GetObject("tbInst_Unk4.PasswordChar")));
			this.tbInst_Unk4.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbInst_Unk4.RightToLeft")));
			this.tbInst_Unk4.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbInst_Unk4.ScrollBars")));
			this.tbInst_Unk4.Size = ((System.Drawing.Size)(resources.GetObject("tbInst_Unk4.Size")));
			this.tbInst_Unk4.TabIndex = ((int)(resources.GetObject("tbInst_Unk4.TabIndex")));
			this.tbInst_Unk4.Text = resources.GetString("tbInst_Unk4.Text");
			this.tbInst_Unk4.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbInst_Unk4.TextAlign")));
			this.tbInst_Unk4.Visible = ((bool)(resources.GetObject("tbInst_Unk4.Visible")));
			this.tbInst_Unk4.WordWrap = ((bool)(resources.GetObject("tbInst_Unk4.WordWrap")));
			this.tbInst_Unk4.TextChanged += new System.EventHandler(this.Byte_TextChanged);
			// 
			// tbInst_Unk3
			// 
			this.tbInst_Unk3.AccessibleDescription = resources.GetString("tbInst_Unk3.AccessibleDescription");
			this.tbInst_Unk3.AccessibleName = resources.GetString("tbInst_Unk3.AccessibleName");
			this.tbInst_Unk3.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbInst_Unk3.Anchor")));
			this.tbInst_Unk3.AutoSize = ((bool)(resources.GetObject("tbInst_Unk3.AutoSize")));
			this.tbInst_Unk3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbInst_Unk3.BackgroundImage")));
			this.tbInst_Unk3.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbInst_Unk3.Dock")));
			this.tbInst_Unk3.Enabled = ((bool)(resources.GetObject("tbInst_Unk3.Enabled")));
			this.tbInst_Unk3.Font = ((System.Drawing.Font)(resources.GetObject("tbInst_Unk3.Font")));
			this.tbInst_Unk3.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbInst_Unk3.ImeMode")));
			this.tbInst_Unk3.Location = ((System.Drawing.Point)(resources.GetObject("tbInst_Unk3.Location")));
			this.tbInst_Unk3.MaxLength = ((int)(resources.GetObject("tbInst_Unk3.MaxLength")));
			this.tbInst_Unk3.Multiline = ((bool)(resources.GetObject("tbInst_Unk3.Multiline")));
			this.tbInst_Unk3.Name = "tbInst_Unk3";
			this.tbInst_Unk3.PasswordChar = ((char)(resources.GetObject("tbInst_Unk3.PasswordChar")));
			this.tbInst_Unk3.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbInst_Unk3.RightToLeft")));
			this.tbInst_Unk3.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbInst_Unk3.ScrollBars")));
			this.tbInst_Unk3.Size = ((System.Drawing.Size)(resources.GetObject("tbInst_Unk3.Size")));
			this.tbInst_Unk3.TabIndex = ((int)(resources.GetObject("tbInst_Unk3.TabIndex")));
			this.tbInst_Unk3.Text = resources.GetString("tbInst_Unk3.Text");
			this.tbInst_Unk3.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbInst_Unk3.TextAlign")));
			this.tbInst_Unk3.Visible = ((bool)(resources.GetObject("tbInst_Unk3.Visible")));
			this.tbInst_Unk3.WordWrap = ((bool)(resources.GetObject("tbInst_Unk3.WordWrap")));
			this.tbInst_Unk3.TextChanged += new System.EventHandler(this.Byte_TextChanged);
			// 
			// tbInst_Unk2
			// 
			this.tbInst_Unk2.AccessibleDescription = resources.GetString("tbInst_Unk2.AccessibleDescription");
			this.tbInst_Unk2.AccessibleName = resources.GetString("tbInst_Unk2.AccessibleName");
			this.tbInst_Unk2.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbInst_Unk2.Anchor")));
			this.tbInst_Unk2.AutoSize = ((bool)(resources.GetObject("tbInst_Unk2.AutoSize")));
			this.tbInst_Unk2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbInst_Unk2.BackgroundImage")));
			this.tbInst_Unk2.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbInst_Unk2.Dock")));
			this.tbInst_Unk2.Enabled = ((bool)(resources.GetObject("tbInst_Unk2.Enabled")));
			this.tbInst_Unk2.Font = ((System.Drawing.Font)(resources.GetObject("tbInst_Unk2.Font")));
			this.tbInst_Unk2.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbInst_Unk2.ImeMode")));
			this.tbInst_Unk2.Location = ((System.Drawing.Point)(resources.GetObject("tbInst_Unk2.Location")));
			this.tbInst_Unk2.MaxLength = ((int)(resources.GetObject("tbInst_Unk2.MaxLength")));
			this.tbInst_Unk2.Multiline = ((bool)(resources.GetObject("tbInst_Unk2.Multiline")));
			this.tbInst_Unk2.Name = "tbInst_Unk2";
			this.tbInst_Unk2.PasswordChar = ((char)(resources.GetObject("tbInst_Unk2.PasswordChar")));
			this.tbInst_Unk2.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbInst_Unk2.RightToLeft")));
			this.tbInst_Unk2.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbInst_Unk2.ScrollBars")));
			this.tbInst_Unk2.Size = ((System.Drawing.Size)(resources.GetObject("tbInst_Unk2.Size")));
			this.tbInst_Unk2.TabIndex = ((int)(resources.GetObject("tbInst_Unk2.TabIndex")));
			this.tbInst_Unk2.Text = resources.GetString("tbInst_Unk2.Text");
			this.tbInst_Unk2.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbInst_Unk2.TextAlign")));
			this.tbInst_Unk2.Visible = ((bool)(resources.GetObject("tbInst_Unk2.Visible")));
			this.tbInst_Unk2.WordWrap = ((bool)(resources.GetObject("tbInst_Unk2.WordWrap")));
			this.tbInst_Unk2.TextChanged += new System.EventHandler(this.Byte_TextChanged);
			// 
			// tbInst_Unk1
			// 
			this.tbInst_Unk1.AccessibleDescription = resources.GetString("tbInst_Unk1.AccessibleDescription");
			this.tbInst_Unk1.AccessibleName = resources.GetString("tbInst_Unk1.AccessibleName");
			this.tbInst_Unk1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbInst_Unk1.Anchor")));
			this.tbInst_Unk1.AutoSize = ((bool)(resources.GetObject("tbInst_Unk1.AutoSize")));
			this.tbInst_Unk1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbInst_Unk1.BackgroundImage")));
			this.tbInst_Unk1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbInst_Unk1.Dock")));
			this.tbInst_Unk1.Enabled = ((bool)(resources.GetObject("tbInst_Unk1.Enabled")));
			this.tbInst_Unk1.Font = ((System.Drawing.Font)(resources.GetObject("tbInst_Unk1.Font")));
			this.tbInst_Unk1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbInst_Unk1.ImeMode")));
			this.tbInst_Unk1.Location = ((System.Drawing.Point)(resources.GetObject("tbInst_Unk1.Location")));
			this.tbInst_Unk1.MaxLength = ((int)(resources.GetObject("tbInst_Unk1.MaxLength")));
			this.tbInst_Unk1.Multiline = ((bool)(resources.GetObject("tbInst_Unk1.Multiline")));
			this.tbInst_Unk1.Name = "tbInst_Unk1";
			this.tbInst_Unk1.PasswordChar = ((char)(resources.GetObject("tbInst_Unk1.PasswordChar")));
			this.tbInst_Unk1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbInst_Unk1.RightToLeft")));
			this.tbInst_Unk1.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbInst_Unk1.ScrollBars")));
			this.tbInst_Unk1.Size = ((System.Drawing.Size)(resources.GetObject("tbInst_Unk1.Size")));
			this.tbInst_Unk1.TabIndex = ((int)(resources.GetObject("tbInst_Unk1.TabIndex")));
			this.tbInst_Unk1.Text = resources.GetString("tbInst_Unk1.Text");
			this.tbInst_Unk1.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbInst_Unk1.TextAlign")));
			this.tbInst_Unk1.Visible = ((bool)(resources.GetObject("tbInst_Unk1.Visible")));
			this.tbInst_Unk1.WordWrap = ((bool)(resources.GetObject("tbInst_Unk1.WordWrap")));
			this.tbInst_Unk1.TextChanged += new System.EventHandler(this.Byte_TextChanged);
			// 
			// tbInst_Unk0
			// 
			this.tbInst_Unk0.AccessibleDescription = resources.GetString("tbInst_Unk0.AccessibleDescription");
			this.tbInst_Unk0.AccessibleName = resources.GetString("tbInst_Unk0.AccessibleName");
			this.tbInst_Unk0.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbInst_Unk0.Anchor")));
			this.tbInst_Unk0.AutoSize = ((bool)(resources.GetObject("tbInst_Unk0.AutoSize")));
			this.tbInst_Unk0.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbInst_Unk0.BackgroundImage")));
			this.tbInst_Unk0.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbInst_Unk0.Dock")));
			this.tbInst_Unk0.Enabled = ((bool)(resources.GetObject("tbInst_Unk0.Enabled")));
			this.tbInst_Unk0.Font = ((System.Drawing.Font)(resources.GetObject("tbInst_Unk0.Font")));
			this.tbInst_Unk0.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbInst_Unk0.ImeMode")));
			this.tbInst_Unk0.Location = ((System.Drawing.Point)(resources.GetObject("tbInst_Unk0.Location")));
			this.tbInst_Unk0.MaxLength = ((int)(resources.GetObject("tbInst_Unk0.MaxLength")));
			this.tbInst_Unk0.Multiline = ((bool)(resources.GetObject("tbInst_Unk0.Multiline")));
			this.tbInst_Unk0.Name = "tbInst_Unk0";
			this.tbInst_Unk0.PasswordChar = ((char)(resources.GetObject("tbInst_Unk0.PasswordChar")));
			this.tbInst_Unk0.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbInst_Unk0.RightToLeft")));
			this.tbInst_Unk0.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbInst_Unk0.ScrollBars")));
			this.tbInst_Unk0.Size = ((System.Drawing.Size)(resources.GetObject("tbInst_Unk0.Size")));
			this.tbInst_Unk0.TabIndex = ((int)(resources.GetObject("tbInst_Unk0.TabIndex")));
			this.tbInst_Unk0.Text = resources.GetString("tbInst_Unk0.Text");
			this.tbInst_Unk0.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbInst_Unk0.TextAlign")));
			this.tbInst_Unk0.Visible = ((bool)(resources.GetObject("tbInst_Unk0.Visible")));
			this.tbInst_Unk0.WordWrap = ((bool)(resources.GetObject("tbInst_Unk0.WordWrap")));
			this.tbInst_Unk0.TextChanged += new System.EventHandler(this.Byte_TextChanged);
			// 
			// tbInst_Op7
			// 
			this.tbInst_Op7.AccessibleDescription = resources.GetString("tbInst_Op7.AccessibleDescription");
			this.tbInst_Op7.AccessibleName = resources.GetString("tbInst_Op7.AccessibleName");
			this.tbInst_Op7.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbInst_Op7.Anchor")));
			this.tbInst_Op7.AutoSize = ((bool)(resources.GetObject("tbInst_Op7.AutoSize")));
			this.tbInst_Op7.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbInst_Op7.BackgroundImage")));
			this.tbInst_Op7.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbInst_Op7.Dock")));
			this.tbInst_Op7.Enabled = ((bool)(resources.GetObject("tbInst_Op7.Enabled")));
			this.tbInst_Op7.Font = ((System.Drawing.Font)(resources.GetObject("tbInst_Op7.Font")));
			this.tbInst_Op7.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbInst_Op7.ImeMode")));
			this.tbInst_Op7.Location = ((System.Drawing.Point)(resources.GetObject("tbInst_Op7.Location")));
			this.tbInst_Op7.MaxLength = ((int)(resources.GetObject("tbInst_Op7.MaxLength")));
			this.tbInst_Op7.Multiline = ((bool)(resources.GetObject("tbInst_Op7.Multiline")));
			this.tbInst_Op7.Name = "tbInst_Op7";
			this.tbInst_Op7.PasswordChar = ((char)(resources.GetObject("tbInst_Op7.PasswordChar")));
			this.tbInst_Op7.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbInst_Op7.RightToLeft")));
			this.tbInst_Op7.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbInst_Op7.ScrollBars")));
			this.tbInst_Op7.Size = ((System.Drawing.Size)(resources.GetObject("tbInst_Op7.Size")));
			this.tbInst_Op7.TabIndex = ((int)(resources.GetObject("tbInst_Op7.TabIndex")));
			this.tbInst_Op7.Text = resources.GetString("tbInst_Op7.Text");
			this.tbInst_Op7.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbInst_Op7.TextAlign")));
			this.tbInst_Op7.Visible = ((bool)(resources.GetObject("tbInst_Op7.Visible")));
			this.tbInst_Op7.WordWrap = ((bool)(resources.GetObject("tbInst_Op7.WordWrap")));
			this.tbInst_Op7.TextChanged += new System.EventHandler(this.Byte_TextChanged);
			// 
			// tbInst_Op6
			// 
			this.tbInst_Op6.AccessibleDescription = resources.GetString("tbInst_Op6.AccessibleDescription");
			this.tbInst_Op6.AccessibleName = resources.GetString("tbInst_Op6.AccessibleName");
			this.tbInst_Op6.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbInst_Op6.Anchor")));
			this.tbInst_Op6.AutoSize = ((bool)(resources.GetObject("tbInst_Op6.AutoSize")));
			this.tbInst_Op6.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbInst_Op6.BackgroundImage")));
			this.tbInst_Op6.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbInst_Op6.Dock")));
			this.tbInst_Op6.Enabled = ((bool)(resources.GetObject("tbInst_Op6.Enabled")));
			this.tbInst_Op6.Font = ((System.Drawing.Font)(resources.GetObject("tbInst_Op6.Font")));
			this.tbInst_Op6.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbInst_Op6.ImeMode")));
			this.tbInst_Op6.Location = ((System.Drawing.Point)(resources.GetObject("tbInst_Op6.Location")));
			this.tbInst_Op6.MaxLength = ((int)(resources.GetObject("tbInst_Op6.MaxLength")));
			this.tbInst_Op6.Multiline = ((bool)(resources.GetObject("tbInst_Op6.Multiline")));
			this.tbInst_Op6.Name = "tbInst_Op6";
			this.tbInst_Op6.PasswordChar = ((char)(resources.GetObject("tbInst_Op6.PasswordChar")));
			this.tbInst_Op6.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbInst_Op6.RightToLeft")));
			this.tbInst_Op6.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbInst_Op6.ScrollBars")));
			this.tbInst_Op6.Size = ((System.Drawing.Size)(resources.GetObject("tbInst_Op6.Size")));
			this.tbInst_Op6.TabIndex = ((int)(resources.GetObject("tbInst_Op6.TabIndex")));
			this.tbInst_Op6.Text = resources.GetString("tbInst_Op6.Text");
			this.tbInst_Op6.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbInst_Op6.TextAlign")));
			this.tbInst_Op6.Visible = ((bool)(resources.GetObject("tbInst_Op6.Visible")));
			this.tbInst_Op6.WordWrap = ((bool)(resources.GetObject("tbInst_Op6.WordWrap")));
			this.tbInst_Op6.TextChanged += new System.EventHandler(this.Byte_TextChanged);
			// 
			// tbInst_Op5
			// 
			this.tbInst_Op5.AccessibleDescription = resources.GetString("tbInst_Op5.AccessibleDescription");
			this.tbInst_Op5.AccessibleName = resources.GetString("tbInst_Op5.AccessibleName");
			this.tbInst_Op5.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbInst_Op5.Anchor")));
			this.tbInst_Op5.AutoSize = ((bool)(resources.GetObject("tbInst_Op5.AutoSize")));
			this.tbInst_Op5.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbInst_Op5.BackgroundImage")));
			this.tbInst_Op5.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbInst_Op5.Dock")));
			this.tbInst_Op5.Enabled = ((bool)(resources.GetObject("tbInst_Op5.Enabled")));
			this.tbInst_Op5.Font = ((System.Drawing.Font)(resources.GetObject("tbInst_Op5.Font")));
			this.tbInst_Op5.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbInst_Op5.ImeMode")));
			this.tbInst_Op5.Location = ((System.Drawing.Point)(resources.GetObject("tbInst_Op5.Location")));
			this.tbInst_Op5.MaxLength = ((int)(resources.GetObject("tbInst_Op5.MaxLength")));
			this.tbInst_Op5.Multiline = ((bool)(resources.GetObject("tbInst_Op5.Multiline")));
			this.tbInst_Op5.Name = "tbInst_Op5";
			this.tbInst_Op5.PasswordChar = ((char)(resources.GetObject("tbInst_Op5.PasswordChar")));
			this.tbInst_Op5.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbInst_Op5.RightToLeft")));
			this.tbInst_Op5.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbInst_Op5.ScrollBars")));
			this.tbInst_Op5.Size = ((System.Drawing.Size)(resources.GetObject("tbInst_Op5.Size")));
			this.tbInst_Op5.TabIndex = ((int)(resources.GetObject("tbInst_Op5.TabIndex")));
			this.tbInst_Op5.Text = resources.GetString("tbInst_Op5.Text");
			this.tbInst_Op5.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbInst_Op5.TextAlign")));
			this.tbInst_Op5.Visible = ((bool)(resources.GetObject("tbInst_Op5.Visible")));
			this.tbInst_Op5.WordWrap = ((bool)(resources.GetObject("tbInst_Op5.WordWrap")));
			this.tbInst_Op5.TextChanged += new System.EventHandler(this.Byte_TextChanged);
			// 
			// tbInst_Op4
			// 
			this.tbInst_Op4.AccessibleDescription = resources.GetString("tbInst_Op4.AccessibleDescription");
			this.tbInst_Op4.AccessibleName = resources.GetString("tbInst_Op4.AccessibleName");
			this.tbInst_Op4.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbInst_Op4.Anchor")));
			this.tbInst_Op4.AutoSize = ((bool)(resources.GetObject("tbInst_Op4.AutoSize")));
			this.tbInst_Op4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbInst_Op4.BackgroundImage")));
			this.tbInst_Op4.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbInst_Op4.Dock")));
			this.tbInst_Op4.Enabled = ((bool)(resources.GetObject("tbInst_Op4.Enabled")));
			this.tbInst_Op4.Font = ((System.Drawing.Font)(resources.GetObject("tbInst_Op4.Font")));
			this.tbInst_Op4.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbInst_Op4.ImeMode")));
			this.tbInst_Op4.Location = ((System.Drawing.Point)(resources.GetObject("tbInst_Op4.Location")));
			this.tbInst_Op4.MaxLength = ((int)(resources.GetObject("tbInst_Op4.MaxLength")));
			this.tbInst_Op4.Multiline = ((bool)(resources.GetObject("tbInst_Op4.Multiline")));
			this.tbInst_Op4.Name = "tbInst_Op4";
			this.tbInst_Op4.PasswordChar = ((char)(resources.GetObject("tbInst_Op4.PasswordChar")));
			this.tbInst_Op4.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbInst_Op4.RightToLeft")));
			this.tbInst_Op4.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbInst_Op4.ScrollBars")));
			this.tbInst_Op4.Size = ((System.Drawing.Size)(resources.GetObject("tbInst_Op4.Size")));
			this.tbInst_Op4.TabIndex = ((int)(resources.GetObject("tbInst_Op4.TabIndex")));
			this.tbInst_Op4.Text = resources.GetString("tbInst_Op4.Text");
			this.tbInst_Op4.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbInst_Op4.TextAlign")));
			this.tbInst_Op4.Visible = ((bool)(resources.GetObject("tbInst_Op4.Visible")));
			this.tbInst_Op4.WordWrap = ((bool)(resources.GetObject("tbInst_Op4.WordWrap")));
			this.tbInst_Op4.TextChanged += new System.EventHandler(this.Byte_TextChanged);
			// 
			// tbInst_Op3
			// 
			this.tbInst_Op3.AccessibleDescription = resources.GetString("tbInst_Op3.AccessibleDescription");
			this.tbInst_Op3.AccessibleName = resources.GetString("tbInst_Op3.AccessibleName");
			this.tbInst_Op3.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbInst_Op3.Anchor")));
			this.tbInst_Op3.AutoSize = ((bool)(resources.GetObject("tbInst_Op3.AutoSize")));
			this.tbInst_Op3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbInst_Op3.BackgroundImage")));
			this.tbInst_Op3.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbInst_Op3.Dock")));
			this.tbInst_Op3.Enabled = ((bool)(resources.GetObject("tbInst_Op3.Enabled")));
			this.tbInst_Op3.Font = ((System.Drawing.Font)(resources.GetObject("tbInst_Op3.Font")));
			this.tbInst_Op3.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbInst_Op3.ImeMode")));
			this.tbInst_Op3.Location = ((System.Drawing.Point)(resources.GetObject("tbInst_Op3.Location")));
			this.tbInst_Op3.MaxLength = ((int)(resources.GetObject("tbInst_Op3.MaxLength")));
			this.tbInst_Op3.Multiline = ((bool)(resources.GetObject("tbInst_Op3.Multiline")));
			this.tbInst_Op3.Name = "tbInst_Op3";
			this.tbInst_Op3.PasswordChar = ((char)(resources.GetObject("tbInst_Op3.PasswordChar")));
			this.tbInst_Op3.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbInst_Op3.RightToLeft")));
			this.tbInst_Op3.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbInst_Op3.ScrollBars")));
			this.tbInst_Op3.Size = ((System.Drawing.Size)(resources.GetObject("tbInst_Op3.Size")));
			this.tbInst_Op3.TabIndex = ((int)(resources.GetObject("tbInst_Op3.TabIndex")));
			this.tbInst_Op3.Text = resources.GetString("tbInst_Op3.Text");
			this.tbInst_Op3.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbInst_Op3.TextAlign")));
			this.tbInst_Op3.Visible = ((bool)(resources.GetObject("tbInst_Op3.Visible")));
			this.tbInst_Op3.WordWrap = ((bool)(resources.GetObject("tbInst_Op3.WordWrap")));
			this.tbInst_Op3.TextChanged += new System.EventHandler(this.Byte_TextChanged);
			// 
			// tbInst_Op2
			// 
			this.tbInst_Op2.AccessibleDescription = resources.GetString("tbInst_Op2.AccessibleDescription");
			this.tbInst_Op2.AccessibleName = resources.GetString("tbInst_Op2.AccessibleName");
			this.tbInst_Op2.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbInst_Op2.Anchor")));
			this.tbInst_Op2.AutoSize = ((bool)(resources.GetObject("tbInst_Op2.AutoSize")));
			this.tbInst_Op2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbInst_Op2.BackgroundImage")));
			this.tbInst_Op2.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbInst_Op2.Dock")));
			this.tbInst_Op2.Enabled = ((bool)(resources.GetObject("tbInst_Op2.Enabled")));
			this.tbInst_Op2.Font = ((System.Drawing.Font)(resources.GetObject("tbInst_Op2.Font")));
			this.tbInst_Op2.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbInst_Op2.ImeMode")));
			this.tbInst_Op2.Location = ((System.Drawing.Point)(resources.GetObject("tbInst_Op2.Location")));
			this.tbInst_Op2.MaxLength = ((int)(resources.GetObject("tbInst_Op2.MaxLength")));
			this.tbInst_Op2.Multiline = ((bool)(resources.GetObject("tbInst_Op2.Multiline")));
			this.tbInst_Op2.Name = "tbInst_Op2";
			this.tbInst_Op2.PasswordChar = ((char)(resources.GetObject("tbInst_Op2.PasswordChar")));
			this.tbInst_Op2.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbInst_Op2.RightToLeft")));
			this.tbInst_Op2.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbInst_Op2.ScrollBars")));
			this.tbInst_Op2.Size = ((System.Drawing.Size)(resources.GetObject("tbInst_Op2.Size")));
			this.tbInst_Op2.TabIndex = ((int)(resources.GetObject("tbInst_Op2.TabIndex")));
			this.tbInst_Op2.Text = resources.GetString("tbInst_Op2.Text");
			this.tbInst_Op2.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbInst_Op2.TextAlign")));
			this.tbInst_Op2.Visible = ((bool)(resources.GetObject("tbInst_Op2.Visible")));
			this.tbInst_Op2.WordWrap = ((bool)(resources.GetObject("tbInst_Op2.WordWrap")));
			this.tbInst_Op2.TextChanged += new System.EventHandler(this.Byte_TextChanged);
			// 
			// tbInst_Op1
			// 
			this.tbInst_Op1.AccessibleDescription = resources.GetString("tbInst_Op1.AccessibleDescription");
			this.tbInst_Op1.AccessibleName = resources.GetString("tbInst_Op1.AccessibleName");
			this.tbInst_Op1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbInst_Op1.Anchor")));
			this.tbInst_Op1.AutoSize = ((bool)(resources.GetObject("tbInst_Op1.AutoSize")));
			this.tbInst_Op1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbInst_Op1.BackgroundImage")));
			this.tbInst_Op1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbInst_Op1.Dock")));
			this.tbInst_Op1.Enabled = ((bool)(resources.GetObject("tbInst_Op1.Enabled")));
			this.tbInst_Op1.Font = ((System.Drawing.Font)(resources.GetObject("tbInst_Op1.Font")));
			this.tbInst_Op1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbInst_Op1.ImeMode")));
			this.tbInst_Op1.Location = ((System.Drawing.Point)(resources.GetObject("tbInst_Op1.Location")));
			this.tbInst_Op1.MaxLength = ((int)(resources.GetObject("tbInst_Op1.MaxLength")));
			this.tbInst_Op1.Multiline = ((bool)(resources.GetObject("tbInst_Op1.Multiline")));
			this.tbInst_Op1.Name = "tbInst_Op1";
			this.tbInst_Op1.PasswordChar = ((char)(resources.GetObject("tbInst_Op1.PasswordChar")));
			this.tbInst_Op1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbInst_Op1.RightToLeft")));
			this.tbInst_Op1.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbInst_Op1.ScrollBars")));
			this.tbInst_Op1.Size = ((System.Drawing.Size)(resources.GetObject("tbInst_Op1.Size")));
			this.tbInst_Op1.TabIndex = ((int)(resources.GetObject("tbInst_Op1.TabIndex")));
			this.tbInst_Op1.Text = resources.GetString("tbInst_Op1.Text");
			this.tbInst_Op1.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbInst_Op1.TextAlign")));
			this.tbInst_Op1.Visible = ((bool)(resources.GetObject("tbInst_Op1.Visible")));
			this.tbInst_Op1.WordWrap = ((bool)(resources.GetObject("tbInst_Op1.WordWrap")));
			this.tbInst_Op1.TextChanged += new System.EventHandler(this.Byte_TextChanged);
			// 
			// tbInst_Op0
			// 
			this.tbInst_Op0.AccessibleDescription = resources.GetString("tbInst_Op0.AccessibleDescription");
			this.tbInst_Op0.AccessibleName = resources.GetString("tbInst_Op0.AccessibleName");
			this.tbInst_Op0.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbInst_Op0.Anchor")));
			this.tbInst_Op0.AutoSize = ((bool)(resources.GetObject("tbInst_Op0.AutoSize")));
			this.tbInst_Op0.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbInst_Op0.BackgroundImage")));
			this.tbInst_Op0.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbInst_Op0.Dock")));
			this.tbInst_Op0.Enabled = ((bool)(resources.GetObject("tbInst_Op0.Enabled")));
			this.tbInst_Op0.Font = ((System.Drawing.Font)(resources.GetObject("tbInst_Op0.Font")));
			this.tbInst_Op0.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbInst_Op0.ImeMode")));
			this.tbInst_Op0.Location = ((System.Drawing.Point)(resources.GetObject("tbInst_Op0.Location")));
			this.tbInst_Op0.MaxLength = ((int)(resources.GetObject("tbInst_Op0.MaxLength")));
			this.tbInst_Op0.Multiline = ((bool)(resources.GetObject("tbInst_Op0.Multiline")));
			this.tbInst_Op0.Name = "tbInst_Op0";
			this.tbInst_Op0.PasswordChar = ((char)(resources.GetObject("tbInst_Op0.PasswordChar")));
			this.tbInst_Op0.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbInst_Op0.RightToLeft")));
			this.tbInst_Op0.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbInst_Op0.ScrollBars")));
			this.tbInst_Op0.Size = ((System.Drawing.Size)(resources.GetObject("tbInst_Op0.Size")));
			this.tbInst_Op0.TabIndex = ((int)(resources.GetObject("tbInst_Op0.TabIndex")));
			this.tbInst_Op0.Text = resources.GetString("tbInst_Op0.Text");
			this.tbInst_Op0.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbInst_Op0.TextAlign")));
			this.tbInst_Op0.Visible = ((bool)(resources.GetObject("tbInst_Op0.Visible")));
			this.tbInst_Op0.WordWrap = ((bool)(resources.GetObject("tbInst_Op0.WordWrap")));
			this.tbInst_Op0.TextChanged += new System.EventHandler(this.Byte_TextChanged);
			// 
			// tbInst_Reserved
			// 
			this.tbInst_Reserved.AccessibleDescription = resources.GetString("tbInst_Reserved.AccessibleDescription");
			this.tbInst_Reserved.AccessibleName = resources.GetString("tbInst_Reserved.AccessibleName");
			this.tbInst_Reserved.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbInst_Reserved.Anchor")));
			this.tbInst_Reserved.AutoSize = ((bool)(resources.GetObject("tbInst_Reserved.AutoSize")));
			this.tbInst_Reserved.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbInst_Reserved.BackgroundImage")));
			this.tbInst_Reserved.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbInst_Reserved.Dock")));
			this.tbInst_Reserved.Enabled = ((bool)(resources.GetObject("tbInst_Reserved.Enabled")));
			this.tbInst_Reserved.Font = ((System.Drawing.Font)(resources.GetObject("tbInst_Reserved.Font")));
			this.tbInst_Reserved.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbInst_Reserved.ImeMode")));
			this.tbInst_Reserved.Location = ((System.Drawing.Point)(resources.GetObject("tbInst_Reserved.Location")));
			this.tbInst_Reserved.MaxLength = ((int)(resources.GetObject("tbInst_Reserved.MaxLength")));
			this.tbInst_Reserved.Multiline = ((bool)(resources.GetObject("tbInst_Reserved.Multiline")));
			this.tbInst_Reserved.Name = "tbInst_Reserved";
			this.tbInst_Reserved.PasswordChar = ((char)(resources.GetObject("tbInst_Reserved.PasswordChar")));
			this.tbInst_Reserved.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbInst_Reserved.RightToLeft")));
			this.tbInst_Reserved.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbInst_Reserved.ScrollBars")));
			this.tbInst_Reserved.Size = ((System.Drawing.Size)(resources.GetObject("tbInst_Reserved.Size")));
			this.tbInst_Reserved.TabIndex = ((int)(resources.GetObject("tbInst_Reserved.TabIndex")));
			this.tbInst_Reserved.Text = resources.GetString("tbInst_Reserved.Text");
			this.tbInst_Reserved.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbInst_Reserved.TextAlign")));
			this.tbInst_Reserved.Visible = ((bool)(resources.GetObject("tbInst_Reserved.Visible")));
			this.tbInst_Reserved.WordWrap = ((bool)(resources.GetObject("tbInst_Reserved.WordWrap")));
			this.tbInst_Reserved.TextChanged += new System.EventHandler(this.Byte_TextChanged);
			// 
			// tbInst_OpCode
			// 
			this.tbInst_OpCode.AccessibleDescription = resources.GetString("tbInst_OpCode.AccessibleDescription");
			this.tbInst_OpCode.AccessibleName = resources.GetString("tbInst_OpCode.AccessibleName");
			this.tbInst_OpCode.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbInst_OpCode.Anchor")));
			this.tbInst_OpCode.AutoSize = ((bool)(resources.GetObject("tbInst_OpCode.AutoSize")));
			this.tbInst_OpCode.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbInst_OpCode.BackgroundImage")));
			this.tbInst_OpCode.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbInst_OpCode.Dock")));
			this.tbInst_OpCode.Enabled = ((bool)(resources.GetObject("tbInst_OpCode.Enabled")));
			this.tbInst_OpCode.Font = ((System.Drawing.Font)(resources.GetObject("tbInst_OpCode.Font")));
			this.tbInst_OpCode.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbInst_OpCode.ImeMode")));
			this.tbInst_OpCode.Location = ((System.Drawing.Point)(resources.GetObject("tbInst_OpCode.Location")));
			this.tbInst_OpCode.MaxLength = ((int)(resources.GetObject("tbInst_OpCode.MaxLength")));
			this.tbInst_OpCode.Multiline = ((bool)(resources.GetObject("tbInst_OpCode.Multiline")));
			this.tbInst_OpCode.Name = "tbInst_OpCode";
			this.tbInst_OpCode.PasswordChar = ((char)(resources.GetObject("tbInst_OpCode.PasswordChar")));
			this.tbInst_OpCode.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbInst_OpCode.RightToLeft")));
			this.tbInst_OpCode.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbInst_OpCode.ScrollBars")));
			this.tbInst_OpCode.Size = ((System.Drawing.Size)(resources.GetObject("tbInst_OpCode.Size")));
			this.tbInst_OpCode.TabIndex = ((int)(resources.GetObject("tbInst_OpCode.TabIndex")));
			this.tbInst_OpCode.Text = resources.GetString("tbInst_OpCode.Text");
			this.tbInst_OpCode.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbInst_OpCode.TextAlign")));
			this.tbInst_OpCode.Visible = ((bool)(resources.GetObject("tbInst_OpCode.Visible")));
			this.tbInst_OpCode.WordWrap = ((bool)(resources.GetObject("tbInst_OpCode.WordWrap")));
			this.tbInst_OpCode.TextChanged += new System.EventHandler(this.UShort_TextChanged);
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
			// btnOpCode
			// 
			this.btnOpCode.AccessibleDescription = resources.GetString("btnOpCode.AccessibleDescription");
			this.btnOpCode.AccessibleName = resources.GetString("btnOpCode.AccessibleName");
			this.btnOpCode.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnOpCode.Anchor")));
			this.btnOpCode.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOpCode.BackgroundImage")));
			this.btnOpCode.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnOpCode.Dock")));
			this.btnOpCode.Enabled = ((bool)(resources.GetObject("btnOpCode.Enabled")));
			this.btnOpCode.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnOpCode.FlatStyle")));
			this.btnOpCode.Font = ((System.Drawing.Font)(resources.GetObject("btnOpCode.Font")));
			this.btnOpCode.Image = ((System.Drawing.Image)(resources.GetObject("btnOpCode.Image")));
			this.btnOpCode.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnOpCode.ImageAlign")));
			this.btnOpCode.ImageIndex = ((int)(resources.GetObject("btnOpCode.ImageIndex")));
			this.btnOpCode.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnOpCode.ImeMode")));
			this.btnOpCode.Location = ((System.Drawing.Point)(resources.GetObject("btnOpCode.Location")));
			this.btnOpCode.Name = "btnOpCode";
			this.btnOpCode.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnOpCode.RightToLeft")));
			this.btnOpCode.Size = ((System.Drawing.Size)(resources.GetObject("btnOpCode.Size")));
			this.btnOpCode.TabIndex = ((int)(resources.GetObject("btnOpCode.TabIndex")));
			this.btnOpCode.Text = resources.GetString("btnOpCode.Text");
			this.btnOpCode.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnOpCode.TextAlign")));
			this.btnOpCode.Visible = ((bool)(resources.GetObject("btnOpCode.Visible")));
			this.btnOpCode.Click += new System.EventHandler(this.btnOpCode_Clicked);
			// 
			// tbInst_Instruction
			// 
			this.tbInst_Instruction.AccessibleDescription = resources.GetString("tbInst_Instruction.AccessibleDescription");
			this.tbInst_Instruction.AccessibleName = resources.GetString("tbInst_Instruction.AccessibleName");
			this.tbInst_Instruction.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbInst_Instruction.Anchor")));
			this.tbInst_Instruction.AutoSize = ((bool)(resources.GetObject("tbInst_Instruction.AutoSize")));
			this.tbInst_Instruction.BackColor = System.Drawing.SystemColors.Control;
			this.tbInst_Instruction.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbInst_Instruction.BackgroundImage")));
			this.tbInst_Instruction.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.tbInst_Instruction.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbInst_Instruction.Dock")));
			this.tbInst_Instruction.Enabled = ((bool)(resources.GetObject("tbInst_Instruction.Enabled")));
			this.tbInst_Instruction.Font = ((System.Drawing.Font)(resources.GetObject("tbInst_Instruction.Font")));
			this.tbInst_Instruction.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbInst_Instruction.ImeMode")));
			this.tbInst_Instruction.Location = ((System.Drawing.Point)(resources.GetObject("tbInst_Instruction.Location")));
			this.tbInst_Instruction.MaxLength = ((int)(resources.GetObject("tbInst_Instruction.MaxLength")));
			this.tbInst_Instruction.Multiline = ((bool)(resources.GetObject("tbInst_Instruction.Multiline")));
			this.tbInst_Instruction.Name = "tbInst_Instruction";
			this.tbInst_Instruction.PasswordChar = ((char)(resources.GetObject("tbInst_Instruction.PasswordChar")));
			this.tbInst_Instruction.ReadOnly = true;
			this.tbInst_Instruction.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbInst_Instruction.RightToLeft")));
			this.tbInst_Instruction.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbInst_Instruction.ScrollBars")));
			this.tbInst_Instruction.Size = ((System.Drawing.Size)(resources.GetObject("tbInst_Instruction.Size")));
			this.tbInst_Instruction.TabIndex = ((int)(resources.GetObject("tbInst_Instruction.TabIndex")));
			this.tbInst_Instruction.TabStop = false;
			this.tbInst_Instruction.Text = resources.GetString("tbInst_Instruction.Text");
			this.tbInst_Instruction.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbInst_Instruction.TextAlign")));
			this.tbInst_Instruction.Visible = ((bool)(resources.GetObject("tbInst_Instruction.Visible")));
			this.tbInst_Instruction.WordWrap = ((bool)(resources.GetObject("tbInst_Instruction.WordWrap")));
			// 
			// tbFilename
			// 
			this.tbFilename.AccessibleDescription = resources.GetString("tbFilename.AccessibleDescription");
			this.tbFilename.AccessibleName = resources.GetString("tbFilename.AccessibleName");
			this.tbFilename.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbFilename.Anchor")));
			this.tbFilename.AutoSize = ((bool)(resources.GetObject("tbFilename.AutoSize")));
			this.tbFilename.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbFilename.BackgroundImage")));
			this.tbFilename.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbFilename.Dock")));
			this.tbFilename.Enabled = ((bool)(resources.GetObject("tbFilename.Enabled")));
			this.tbFilename.Font = ((System.Drawing.Font)(resources.GetObject("tbFilename.Font")));
			this.tbFilename.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbFilename.ImeMode")));
			this.tbFilename.Location = ((System.Drawing.Point)(resources.GetObject("tbFilename.Location")));
			this.tbFilename.MaxLength = ((int)(resources.GetObject("tbFilename.MaxLength")));
			this.tbFilename.Multiline = ((bool)(resources.GetObject("tbFilename.Multiline")));
			this.tbFilename.Name = "tbFilename";
			this.tbFilename.PasswordChar = ((char)(resources.GetObject("tbFilename.PasswordChar")));
			this.tbFilename.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbFilename.RightToLeft")));
			this.tbFilename.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbFilename.ScrollBars")));
			this.tbFilename.Size = ((System.Drawing.Size)(resources.GetObject("tbFilename.Size")));
			this.tbFilename.TabIndex = ((int)(resources.GetObject("tbFilename.TabIndex")));
			this.tbFilename.Text = resources.GetString("tbFilename.Text");
			this.tbFilename.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbFilename.TextAlign")));
			this.tbFilename.Visible = ((bool)(resources.GetObject("tbFilename.Visible")));
			this.tbFilename.WordWrap = ((bool)(resources.GetObject("tbFilename.WordWrap")));
			this.tbFilename.TextChanged += new System.EventHandler(this.tbFilename_TextChanged);
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
			// tbReserved
			// 
			this.tbReserved.AccessibleDescription = resources.GetString("tbReserved.AccessibleDescription");
			this.tbReserved.AccessibleName = resources.GetString("tbReserved.AccessibleName");
			this.tbReserved.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbReserved.Anchor")));
			this.tbReserved.AutoSize = ((bool)(resources.GetObject("tbReserved.AutoSize")));
			this.tbReserved.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbReserved.BackgroundImage")));
			this.tbReserved.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbReserved.Dock")));
			this.tbReserved.Enabled = ((bool)(resources.GetObject("tbReserved.Enabled")));
			this.tbReserved.Font = ((System.Drawing.Font)(resources.GetObject("tbReserved.Font")));
			this.tbReserved.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbReserved.ImeMode")));
			this.tbReserved.Location = ((System.Drawing.Point)(resources.GetObject("tbReserved.Location")));
			this.tbReserved.MaxLength = ((int)(resources.GetObject("tbReserved.MaxLength")));
			this.tbReserved.Multiline = ((bool)(resources.GetObject("tbReserved.Multiline")));
			this.tbReserved.Name = "tbReserved";
			this.tbReserved.PasswordChar = ((char)(resources.GetObject("tbReserved.PasswordChar")));
			this.tbReserved.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbReserved.RightToLeft")));
			this.tbReserved.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbReserved.ScrollBars")));
			this.tbReserved.Size = ((System.Drawing.Size)(resources.GetObject("tbReserved.Size")));
			this.tbReserved.TabIndex = ((int)(resources.GetObject("tbReserved.TabIndex")));
			this.tbReserved.Text = resources.GetString("tbReserved.Text");
			this.tbReserved.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbReserved.TextAlign")));
			this.tbReserved.Visible = ((bool)(resources.GetObject("tbReserved.Visible")));
			this.tbReserved.WordWrap = ((bool)(resources.GetObject("tbReserved.WordWrap")));
			this.tbReserved.TextChanged += new System.EventHandler(this.BhavHeader_TextChanged);
			// 
			// tbLocalC
			// 
			this.tbLocalC.AccessibleDescription = resources.GetString("tbLocalC.AccessibleDescription");
			this.tbLocalC.AccessibleName = resources.GetString("tbLocalC.AccessibleName");
			this.tbLocalC.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbLocalC.Anchor")));
			this.tbLocalC.AutoSize = ((bool)(resources.GetObject("tbLocalC.AutoSize")));
			this.tbLocalC.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbLocalC.BackgroundImage")));
			this.tbLocalC.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbLocalC.Dock")));
			this.tbLocalC.Enabled = ((bool)(resources.GetObject("tbLocalC.Enabled")));
			this.tbLocalC.Font = ((System.Drawing.Font)(resources.GetObject("tbLocalC.Font")));
			this.tbLocalC.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbLocalC.ImeMode")));
			this.tbLocalC.Location = ((System.Drawing.Point)(resources.GetObject("tbLocalC.Location")));
			this.tbLocalC.MaxLength = ((int)(resources.GetObject("tbLocalC.MaxLength")));
			this.tbLocalC.Multiline = ((bool)(resources.GetObject("tbLocalC.Multiline")));
			this.tbLocalC.Name = "tbLocalC";
			this.tbLocalC.PasswordChar = ((char)(resources.GetObject("tbLocalC.PasswordChar")));
			this.tbLocalC.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbLocalC.RightToLeft")));
			this.tbLocalC.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbLocalC.ScrollBars")));
			this.tbLocalC.Size = ((System.Drawing.Size)(resources.GetObject("tbLocalC.Size")));
			this.tbLocalC.TabIndex = ((int)(resources.GetObject("tbLocalC.TabIndex")));
			this.tbLocalC.Text = resources.GetString("tbLocalC.Text");
			this.tbLocalC.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbLocalC.TextAlign")));
			this.tbLocalC.Visible = ((bool)(resources.GetObject("tbLocalC.Visible")));
			this.tbLocalC.WordWrap = ((bool)(resources.GetObject("tbLocalC.WordWrap")));
			this.tbLocalC.TextChanged += new System.EventHandler(this.BhavHeader_TextChanged);
			// 
			// tbFlags
			// 
			this.tbFlags.AccessibleDescription = resources.GetString("tbFlags.AccessibleDescription");
			this.tbFlags.AccessibleName = resources.GetString("tbFlags.AccessibleName");
			this.tbFlags.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbFlags.Anchor")));
			this.tbFlags.AutoSize = ((bool)(resources.GetObject("tbFlags.AutoSize")));
			this.tbFlags.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbFlags.BackgroundImage")));
			this.tbFlags.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbFlags.Dock")));
			this.tbFlags.Enabled = ((bool)(resources.GetObject("tbFlags.Enabled")));
			this.tbFlags.Font = ((System.Drawing.Font)(resources.GetObject("tbFlags.Font")));
			this.tbFlags.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbFlags.ImeMode")));
			this.tbFlags.Location = ((System.Drawing.Point)(resources.GetObject("tbFlags.Location")));
			this.tbFlags.MaxLength = ((int)(resources.GetObject("tbFlags.MaxLength")));
			this.tbFlags.Multiline = ((bool)(resources.GetObject("tbFlags.Multiline")));
			this.tbFlags.Name = "tbFlags";
			this.tbFlags.PasswordChar = ((char)(resources.GetObject("tbFlags.PasswordChar")));
			this.tbFlags.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbFlags.RightToLeft")));
			this.tbFlags.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbFlags.ScrollBars")));
			this.tbFlags.Size = ((System.Drawing.Size)(resources.GetObject("tbFlags.Size")));
			this.tbFlags.TabIndex = ((int)(resources.GetObject("tbFlags.TabIndex")));
			this.tbFlags.Text = resources.GetString("tbFlags.Text");
			this.tbFlags.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbFlags.TextAlign")));
			this.tbFlags.Visible = ((bool)(resources.GetObject("tbFlags.Visible")));
			this.tbFlags.WordWrap = ((bool)(resources.GetObject("tbFlags.WordWrap")));
			this.tbFlags.TextChanged += new System.EventHandler(this.BhavHeader_TextChanged);
			// 
			// tbArgC
			// 
			this.tbArgC.AccessibleDescription = resources.GetString("tbArgC.AccessibleDescription");
			this.tbArgC.AccessibleName = resources.GetString("tbArgC.AccessibleName");
			this.tbArgC.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbArgC.Anchor")));
			this.tbArgC.AutoSize = ((bool)(resources.GetObject("tbArgC.AutoSize")));
			this.tbArgC.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbArgC.BackgroundImage")));
			this.tbArgC.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbArgC.Dock")));
			this.tbArgC.Enabled = ((bool)(resources.GetObject("tbArgC.Enabled")));
			this.tbArgC.Font = ((System.Drawing.Font)(resources.GetObject("tbArgC.Font")));
			this.tbArgC.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbArgC.ImeMode")));
			this.tbArgC.Location = ((System.Drawing.Point)(resources.GetObject("tbArgC.Location")));
			this.tbArgC.MaxLength = ((int)(resources.GetObject("tbArgC.MaxLength")));
			this.tbArgC.Multiline = ((bool)(resources.GetObject("tbArgC.Multiline")));
			this.tbArgC.Name = "tbArgC";
			this.tbArgC.PasswordChar = ((char)(resources.GetObject("tbArgC.PasswordChar")));
			this.tbArgC.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbArgC.RightToLeft")));
			this.tbArgC.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbArgC.ScrollBars")));
			this.tbArgC.Size = ((System.Drawing.Size)(resources.GetObject("tbArgC.Size")));
			this.tbArgC.TabIndex = ((int)(resources.GetObject("tbArgC.TabIndex")));
			this.tbArgC.Text = resources.GetString("tbArgC.Text");
			this.tbArgC.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbArgC.TextAlign")));
			this.tbArgC.Visible = ((bool)(resources.GetObject("tbArgC.Visible")));
			this.tbArgC.WordWrap = ((bool)(resources.GetObject("tbArgC.WordWrap")));
			this.tbArgC.TextChanged += new System.EventHandler(this.BhavHeader_TextChanged);
			// 
			// tbType
			// 
			this.tbType.AccessibleDescription = resources.GetString("tbType.AccessibleDescription");
			this.tbType.AccessibleName = resources.GetString("tbType.AccessibleName");
			this.tbType.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbType.Anchor")));
			this.tbType.AutoSize = ((bool)(resources.GetObject("tbType.AutoSize")));
			this.tbType.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbType.BackgroundImage")));
			this.tbType.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbType.Dock")));
			this.tbType.Enabled = ((bool)(resources.GetObject("tbType.Enabled")));
			this.tbType.Font = ((System.Drawing.Font)(resources.GetObject("tbType.Font")));
			this.tbType.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbType.ImeMode")));
			this.tbType.Location = ((System.Drawing.Point)(resources.GetObject("tbType.Location")));
			this.tbType.MaxLength = ((int)(resources.GetObject("tbType.MaxLength")));
			this.tbType.Multiline = ((bool)(resources.GetObject("tbType.Multiline")));
			this.tbType.Name = "tbType";
			this.tbType.PasswordChar = ((char)(resources.GetObject("tbType.PasswordChar")));
			this.tbType.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbType.RightToLeft")));
			this.tbType.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbType.ScrollBars")));
			this.tbType.Size = ((System.Drawing.Size)(resources.GetObject("tbType.Size")));
			this.tbType.TabIndex = ((int)(resources.GetObject("tbType.TabIndex")));
			this.tbType.Text = resources.GetString("tbType.Text");
			this.tbType.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbType.TextAlign")));
			this.tbType.Visible = ((bool)(resources.GetObject("tbType.Visible")));
			this.tbType.WordWrap = ((bool)(resources.GetObject("tbType.WordWrap")));
			this.tbType.TextChanged += new System.EventHandler(this.BhavHeader_TextChanged);
			// 
			// tbFormat
			// 
			this.tbFormat.AccessibleDescription = resources.GetString("tbFormat.AccessibleDescription");
			this.tbFormat.AccessibleName = resources.GetString("tbFormat.AccessibleName");
			this.tbFormat.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbFormat.Anchor")));
			this.tbFormat.AutoSize = ((bool)(resources.GetObject("tbFormat.AutoSize")));
			this.tbFormat.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbFormat.BackgroundImage")));
			this.tbFormat.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbFormat.Dock")));
			this.tbFormat.Enabled = ((bool)(resources.GetObject("tbFormat.Enabled")));
			this.tbFormat.Font = ((System.Drawing.Font)(resources.GetObject("tbFormat.Font")));
			this.tbFormat.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbFormat.ImeMode")));
			this.tbFormat.Location = ((System.Drawing.Point)(resources.GetObject("tbFormat.Location")));
			this.tbFormat.MaxLength = ((int)(resources.GetObject("tbFormat.MaxLength")));
			this.tbFormat.Multiline = ((bool)(resources.GetObject("tbFormat.Multiline")));
			this.tbFormat.Name = "tbFormat";
			this.tbFormat.PasswordChar = ((char)(resources.GetObject("tbFormat.PasswordChar")));
			this.tbFormat.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbFormat.RightToLeft")));
			this.tbFormat.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbFormat.ScrollBars")));
			this.tbFormat.Size = ((System.Drawing.Size)(resources.GetObject("tbFormat.Size")));
			this.tbFormat.TabIndex = ((int)(resources.GetObject("tbFormat.TabIndex")));
			this.tbFormat.Text = resources.GetString("tbFormat.Text");
			this.tbFormat.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbFormat.TextAlign")));
			this.tbFormat.Visible = ((bool)(resources.GetObject("tbFormat.Visible")));
			this.tbFormat.WordWrap = ((bool)(resources.GetObject("tbFormat.WordWrap")));
			this.tbFormat.TextChanged += new System.EventHandler(this.BhavHeader_TextChanged);
			// 
			// label8
			// 
			this.label8.AccessibleDescription = resources.GetString("label8.AccessibleDescription");
			this.label8.AccessibleName = resources.GetString("label8.AccessibleName");
			this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label8.Anchor")));
			this.label8.AutoSize = ((bool)(resources.GetObject("label8.AutoSize")));
			this.label8.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label8.Dock")));
			this.label8.Enabled = ((bool)(resources.GetObject("label8.Enabled")));
			this.label8.Font = ((System.Drawing.Font)(resources.GetObject("label8.Font")));
			this.label8.Image = ((System.Drawing.Image)(resources.GetObject("label8.Image")));
			this.label8.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label8.ImageAlign")));
			this.label8.ImageIndex = ((int)(resources.GetObject("label8.ImageIndex")));
			this.label8.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label8.ImeMode")));
			this.label8.Location = ((System.Drawing.Point)(resources.GetObject("label8.Location")));
			this.label8.Name = "label8";
			this.label8.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label8.RightToLeft")));
			this.label8.Size = ((System.Drawing.Size)(resources.GetObject("label8.Size")));
			this.label8.TabIndex = ((int)(resources.GetObject("label8.TabIndex")));
			this.label8.Text = resources.GetString("label8.Text");
			this.label8.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label8.TextAlign")));
			this.label8.Visible = ((bool)(resources.GetObject("label8.Visible")));
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
			// pnHeading
			// 
			this.pnHeading.AccessibleDescription = resources.GetString("pnHeading.AccessibleDescription");
			this.pnHeading.AccessibleName = resources.GetString("pnHeading.AccessibleName");
			this.pnHeading.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("pnHeading.Anchor")));
			this.pnHeading.AutoScroll = ((bool)(resources.GetObject("pnHeading.AutoScroll")));
			this.pnHeading.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("pnHeading.AutoScrollMargin")));
			this.pnHeading.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("pnHeading.AutoScrollMinSize")));
			this.pnHeading.BackColor = System.Drawing.SystemColors.AppWorkspace;
			this.pnHeading.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnHeading.BackgroundImage")));
			this.pnHeading.Controls.Add(this.label1);
			this.pnHeading.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("pnHeading.Dock")));
			this.pnHeading.Enabled = ((bool)(resources.GetObject("pnHeading.Enabled")));
			this.pnHeading.Font = ((System.Drawing.Font)(resources.GetObject("pnHeading.Font")));
			this.pnHeading.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("pnHeading.ImeMode")));
			this.pnHeading.Location = ((System.Drawing.Point)(resources.GetObject("pnHeading.Location")));
			this.pnHeading.Name = "pnHeading";
			this.pnHeading.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("pnHeading.RightToLeft")));
			this.pnHeading.Size = ((System.Drawing.Size)(resources.GetObject("pnHeading.Size")));
			this.pnHeading.TabIndex = ((int)(resources.GetObject("pnHeading.TabIndex")));
			this.pnHeading.Text = resources.GetString("pnHeading.Text");
			this.pnHeading.Visible = ((bool)(resources.GetObject("pnHeading.Visible")));
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
			this.bhavPanel.Controls.Add(this.btnSort);
			this.bhavPanel.Controls.Add(this.btnCommit);
			this.bhavPanel.Controls.Add(this.label2);
			this.bhavPanel.Controls.Add(this.tbFilename);
			this.bhavPanel.Controls.Add(this.pnflowcontainer);
			this.bhavPanel.Controls.Add(this.gbInstruction);
			this.bhavPanel.Controls.Add(this.tbReserved);
			this.bhavPanel.Controls.Add(this.tbLocalC);
			this.bhavPanel.Controls.Add(this.tbFlags);
			this.bhavPanel.Controls.Add(this.tbArgC);
			this.bhavPanel.Controls.Add(this.tbType);
			this.bhavPanel.Controls.Add(this.tbFormat);
			this.bhavPanel.Controls.Add(this.label8);
			this.bhavPanel.Controls.Add(this.label7);
			this.bhavPanel.Controls.Add(this.label4);
			this.bhavPanel.Controls.Add(this.label6);
			this.bhavPanel.Controls.Add(this.label5);
			this.bhavPanel.Controls.Add(this.label3);
			this.bhavPanel.Controls.Add(this.pnHeading);
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
			// btnSort
			// 
			this.btnSort.AccessibleDescription = resources.GetString("btnSort.AccessibleDescription");
			this.btnSort.AccessibleName = resources.GetString("btnSort.AccessibleName");
			this.btnSort.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnSort.Anchor")));
			this.btnSort.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSort.BackgroundImage")));
			this.btnSort.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnSort.Dock")));
			this.btnSort.Enabled = ((bool)(resources.GetObject("btnSort.Enabled")));
			this.btnSort.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnSort.FlatStyle")));
			this.btnSort.Font = ((System.Drawing.Font)(resources.GetObject("btnSort.Font")));
			this.btnSort.Image = ((System.Drawing.Image)(resources.GetObject("btnSort.Image")));
			this.btnSort.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnSort.ImageAlign")));
			this.btnSort.ImageIndex = ((int)(resources.GetObject("btnSort.ImageIndex")));
			this.btnSort.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnSort.ImeMode")));
			this.btnSort.Location = ((System.Drawing.Point)(resources.GetObject("btnSort.Location")));
			this.btnSort.Name = "btnSort";
			this.btnSort.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnSort.RightToLeft")));
			this.btnSort.Size = ((System.Drawing.Size)(resources.GetObject("btnSort.Size")));
			this.btnSort.TabIndex = ((int)(resources.GetObject("btnSort.TabIndex")));
			this.btnSort.Text = resources.GetString("btnSort.Text");
			this.btnSort.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnSort.TextAlign")));
			this.btnSort.Visible = ((bool)(resources.GetObject("btnSort.Visible")));
			this.btnSort.Click += new System.EventHandler(this.btnSort_Clicked);
			// 
			// btnCommit
			// 
			this.btnCommit.AccessibleDescription = resources.GetString("btnCommit.AccessibleDescription");
			this.btnCommit.AccessibleName = resources.GetString("btnCommit.AccessibleName");
			this.btnCommit.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnCommit.Anchor")));
			this.btnCommit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCommit.BackgroundImage")));
			this.btnCommit.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnCommit.Dock")));
			this.btnCommit.Enabled = ((bool)(resources.GetObject("btnCommit.Enabled")));
			this.btnCommit.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnCommit.FlatStyle")));
			this.btnCommit.Font = ((System.Drawing.Font)(resources.GetObject("btnCommit.Font")));
			this.btnCommit.Image = ((System.Drawing.Image)(resources.GetObject("btnCommit.Image")));
			this.btnCommit.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnCommit.ImageAlign")));
			this.btnCommit.ImageIndex = ((int)(resources.GetObject("btnCommit.ImageIndex")));
			this.btnCommit.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnCommit.ImeMode")));
			this.btnCommit.Location = ((System.Drawing.Point)(resources.GetObject("btnCommit.Location")));
			this.btnCommit.Name = "btnCommit";
			this.btnCommit.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnCommit.RightToLeft")));
			this.btnCommit.Size = ((System.Drawing.Size)(resources.GetObject("btnCommit.Size")));
			this.btnCommit.TabIndex = ((int)(resources.GetObject("btnCommit.TabIndex")));
			this.btnCommit.Text = resources.GetString("btnCommit.Text");
			this.btnCommit.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnCommit.TextAlign")));
			this.btnCommit.Visible = ((bool)(resources.GetObject("btnCommit.Visible")));
			this.btnCommit.Click += new System.EventHandler(this.btnCommit_Clicked);
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
			this.gbInstruction.ResumeLayout(false);
			this.pnHeading.ResumeLayout(false);
			this.bhavPanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

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


		private void btnCommit_Clicked(object sender, System.EventArgs e)
		{
			try 
			{
				wrapper.SynchronizeUserData();
				btnCommit.Enabled = false;
			} 
			catch (Exception ex) 
			{
				Helper.ExceptionMessage(Localization.Manager.GetString("errwritingfile"), ex);
			}			
		}

		private void btnSort_Clicked(object sender, System.EventArgs e)
		{
			this.pnflowcontainer.Sort();
		}


		private void pnflowcontainer_SelectedInstChanged(object sender, System.EventArgs e)
		{
			if (internalchg) return;

			origInst = pnflowcontainer.SelectedInst;
			UpdateInstPanel(pnflowcontainer.SelectedInst);
		}


		private void llopenbhav_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			// We want to instantiate the current UI but with the Global BHAV linked from the current instruction
			Bhav b = Instruction.LoadGlobalBHAV(pnflowcontainer.SelectedInst.OpCode);
			BhavForm ui = (BhavForm)b.UIHandler;
			// but make it clear it's read only
			ui.btnCommit.Visible = ui.btnOpCode.Visible = ui.btnOperandWiz.Visible =
				ui.lladd.Visible = ui.llcancel.Visible = ui.lldel.Visible = ui.llmove.Visible =
				ui.llopenbhav.Visible = ui.btnSort.Visible = ui.tbLines.Enabled = false;
			ui.bhavPanel.Dock = DockStyle.Fill;
			ui.Text = "Global BHAV: " + pnflowcontainer.SelectedInst.ToString();
			b.UpdateUI();
			ui.Show();
		}

		private void btnOpCode_Clicked(object sender, System.EventArgs e)
		{
			Bhav bhav = new Bhav(wrapper.Opcodes);
			bhav.Package = wrapper.Package;
			bhav.FileDescriptor = wrapper.FileDescriptor;

			Instruction currentInst = this.pnflowcontainer.SelectedInst;
			int opcode = SimPe.Plugin.WrapperFactory.BhavWizardForm.Execute(bhav, this);

			if (opcode != -1 && opcode != currentInst.OpCode)
			{
				internalchg = true;
				tbInst_OpCode.Text = "0x"+Helper.HexString((ushort)opcode);
				internalchg = false;
				currentInst.OpCode = (ushort)opcode;
				SendInst(currentInst);
			}
		}

		private void btnOperandWiz_Clicked(object sender, System.EventArgs e)
		{
			BhavOperandWiz bwf = new BhavOperandWiz();
			Instruction ret = bwf.Execute(this.pnflowcontainer.SelectedInst);

			if (ret != null) 
			{
				UpdateInstPanel(ret);
				SendInst(ret);
			}
		}
		

		private void llcancel_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			this.pnflowcontainer.SelectedInst = origInst;
		}

		private void lladd_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			this.pnflowcontainer.Add();
		}

		private void lldel_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			this.pnflowcontainer.Delete();
		}

		private void tbmv_TextChanged(object sender, System.EventArgs e)
		{
			if (internalchg) return;

			try 
			{
				int mv = Convert.ToInt32(tbLines.Text);
				if (mv<0) lbUpDown.Text = "lines up";
				else lbUpDown.Text = "lines down";
			} 
			catch {}
		}

		private void llmove_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			int mv;
			try { mv = Convert.ToInt32(tbLines.Text); }
			catch (Exception) { return; }
			this.pnflowcontainer.MoveInst(mv);
		}


		private void tbFilename_TextChanged(object sender, System.EventArgs e)
		{
			if (internalchg) return;

			if (!this.tbFilename.Text.Equals(wrapper.FileName))
			{
				wrapper.FileName = tbFilename.Text;
			}
		}
		private void BhavHeader_TextChanged(object sender, System.EventArgs e)
		{
			if (this.internalchg) return;

			byte val;
			ushort uval;

			if (tbType.Equals(sender))
			{
				try { val = Convert.ToByte(tbType.Text, 16); }
				catch (Exception) { val = 0; }
				if (wrapper.Header.Type != val)
				{
					wrapper.Header.Type = val;
				}
			}

			TextBox[] decByte   = { tbArgC, tbLocalC };
			byte[] decVals      = { wrapper.Header.ArgumentCount, wrapper.Header.LocalVarCount };
			for (int i = 0; i < decByte.Length; i++)
			{
				if (decByte[i].Equals(sender))
				{
					try { val = Convert.ToByte(decByte[i].Text, 10); }
					catch (Exception) { val = 0; }
					if (decVals[i] != val)
					{
						switch(i)
						{
							case 0: wrapper.Header.ArgumentCount = val; break;
							case 1: wrapper.Header.LocalVarCount = val; break;
						}
					}
				}
			}

			TextBox[] hexUShort = { tbFormat, tbFlags, tbReserved };
			ushort[] hexVals    = { wrapper.Header.Format, wrapper.Header.Flags, wrapper.Header.Zero };
			for (int i = 0; i < hexUShort.Length; i++)
			{
				if (hexUShort[i].Equals(sender))
				{
					try { uval = Convert.ToUInt16(hexUShort[i].Text, 16); }
					catch (Exception) { uval = 0; }
					if (hexVals[i] != uval)
					{
						switch(i)
						{
							case 0: wrapper.Header.Format = uval; break;
							case 1: wrapper.Header.Flags = uval; break;
							case 2: wrapper.Header.Zero = uval; break;
						}
					}
				}
			}
		}

		private void Target_TextChanged(object sender, System.EventArgs e)
		{
			if (internalchg) return;

			bool somethingChanged = false;
			Instruction currentInst = this.pnflowcontainer.SelectedInst;

			ComboBox[] cb = { tba1, tba2 };
			ushort[] orig = { currentInst.Target1, currentInst.Target2 };
			for (int i = 0; i < cb.Length; i++)
			{
				if (cb[i].Equals(sender))
				{
					ushort val;
					try { val = Convert.ToUInt16(cb[i].Text, 16); }
					catch (Exception) { val = 0; }
					if (orig[i] != val)
					{
						if (i == 0) currentInst.Target1 = val;
						else        currentInst.Target2 = val;
						somethingChanged = true;
					}
				}
			}
			if (somethingChanged) SendInst(currentInst);
		}

		private void Target_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (internalchg) return;

			bool somethingChanged = false;
			Instruction currentInst = this.pnflowcontainer.SelectedInst;

			ComboBox[] cb = { tba1, tba2 };
			ushort[] orig = { currentInst.Target1, currentInst.Target2 };
			for (int i = 0; i < cb.Length; i++)
			{
				if ((cb[i].Equals(sender)) && (cb[i].SelectedIndex != -1))
				{
					ushort val = (ushort)(0x0FFFC + cb[i].SelectedIndex);
					if (orig[i] != val)
					{
						if (i == 0) currentInst.Target1 = val;
						else        currentInst.Target2 = val;
						somethingChanged = true;
					}
				}
			}
			if (somethingChanged) SendInst(currentInst);
		}

		private void UShort_TextChanged(object sender, System.EventArgs e)
		{
			if (internalchg) return;

			bool somethingChanged = false;
			Instruction currentInst = this.pnflowcontainer.SelectedInst;

			ushort val;
			if (tbInst_OpCode.Equals(sender))
			{
				try { val = Convert.ToUInt16(tbInst_OpCode.Text, 16); }
				catch (Exception) { val = 0; }
				if (currentInst.OpCode != val)
				{
					currentInst.OpCode = val;
					somethingChanged = true;
				}
			}
			if (somethingChanged) SendInst(currentInst);
		}

		private void Byte_TextChanged(object sender, System.EventArgs e)
		{
			if (internalchg) return;

			bool somethingChanged = false;
			Instruction currentInst = this.pnflowcontainer.SelectedInst;

			TextBox[] OpBytes =
				{
					tbInst_Op0, tbInst_Op1, tbInst_Op2, tbInst_Op3,
					tbInst_Op4, tbInst_Op5, tbInst_Op6, tbInst_Op7
				};
			TextBox[] UnkBytes =
				{
					tbInst_Unk0, tbInst_Unk1, tbInst_Unk2, tbInst_Unk3,
					tbInst_Unk4, tbInst_Unk5, tbInst_Unk6, tbInst_Unk7
				};

			byte val;
			for (int i = 0; i < 8; i++)
			{
				if (OpBytes[i].Equals(sender))
				{
					try { val = Convert.ToByte(OpBytes[i].Text, 16); }
					catch (Exception) { val = 0; }
					if (currentInst.Operands[i] != val)
					{
						currentInst.Operands[i] = val;
						somethingChanged = true;
					}
				}

				if (UnkBytes[i].Equals(sender))
				{
					try { val = Convert.ToByte(UnkBytes[i].Text, 16); }
					catch (Exception) { val = 0; }
					if (currentInst.Reserved1[i] != val)
					{
						currentInst.Reserved1[i] = val;
						somethingChanged = true;
					}
				}
			}
			if (tbInst_Reserved.Equals(sender))
			{
				try 
				{
					val = Convert.ToByte(this.tbInst_Reserved.Text, 16); 
				}
				catch (Exception) { val = 0; }
				if (currentInst.Reserved0 != val)
				{
					currentInst.Reserved0 = val;
					somethingChanged = true;
				}
			}
			if (somethingChanged) SendInst(currentInst);
		}
	}
}
