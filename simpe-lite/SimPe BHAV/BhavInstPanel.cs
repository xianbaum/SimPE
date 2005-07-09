/***************************************************************************
 *   Copyright (C) 2005 by Peter L Jones                                   *
 *   peter@drealm.info                                                     *
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
using SimPe.PackedFiles.Wrapper;
using pjse;

namespace SimPe.PackedFiles.UserInterface
{
	/// <summary>
	/// Summary description for BhavInstPanel.
	/// </summary>
	public class BhavInstPanel : System.Windows.Forms.UserControl
	{
		#region Form variables

		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOperandWiz;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Button btnOpCode;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.LinkLabel llViewBHAV;
		private System.Windows.Forms.TextBox tbOp01_dec;
		private System.Windows.Forms.ComboBox cbFalse;
		private System.Windows.Forms.ComboBox cbTrue;
		private System.Windows.Forms.TextBox tbOpF;
		private System.Windows.Forms.TextBox tbOpE;
		private System.Windows.Forms.TextBox tbOpD;
		private System.Windows.Forms.TextBox tbOpC;
		private System.Windows.Forms.TextBox tbOpB;
		private System.Windows.Forms.TextBox tbOpA;
		private System.Windows.Forms.TextBox tbOp9;
		private System.Windows.Forms.TextBox tbOp8;
		private System.Windows.Forms.TextBox tbOp7;
		private System.Windows.Forms.TextBox tbOp6;
		private System.Windows.Forms.TextBox tbOp5;
		private System.Windows.Forms.TextBox tbOp4;
		private System.Windows.Forms.TextBox tbOp3;
		private System.Windows.Forms.TextBox tbOp2;
		private System.Windows.Forms.TextBox tbOp1;
		private System.Windows.Forms.TextBox tbOp0;
		private System.Windows.Forms.TextBox tbReserved;
		private System.Windows.Forms.TextBox tbOpCode;
		private System.Windows.Forms.TextBox tbOp23_dec;
		private System.Windows.Forms.TextBox tbInstruction;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion

		public BhavInstPanel()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
			ComboBox[] cb = {
								cbTrue ,cbFalse
							};
			aTargets = cb;
			TextBox[] dec = {
								tbOp01_dec ,tbOp23_dec
							};
			aOpWords = dec;
			TextBox[] ops = {
								 tbOp0 ,tbOp1 ,tbOp2 ,tbOp3
								,tbOp4 ,tbOp5 ,tbOp6 ,tbOp7
								,tbOp8 ,tbOp9 ,tbOpA ,tbOpB
								,tbOpC ,tbOpD ,tbOpE ,tbOpF
							};
			aOpBytes = ops;

			internalchg = true;
			btnCancel.Enabled = 
				tbOpCode.Enabled = btnOpCode.Enabled = 
				llViewBHAV.Enabled =
				tbReserved.Enabled = 
				cbTrue.Enabled = cbFalse.Enabled = 
				btnOperandWiz.Enabled = 
				false;
			tbOpCode.Text = tbReserved.Text = "";
			cbTrue.SelectedIndex = cbFalse.SelectedIndex = 0;
			tbInstruction.Text = "(no instruction selected)";
			foreach (TextBox c in aOpWords) { c.Enabled = false; c.Text = ""; }
			foreach (TextBox c in aOpBytes) { c.Enabled = false; c.Text = ""; }
			internalchg = false;
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


		#region BhavInstPanel
		private Instruction currentInst = null;
		private bool readOnly = false;

		private ComboBox[] aTargets = null;
		private TextBox[] aOpWords = null;
		private TextBox[] aOpBytes = null;

		private bool internalchg = false;

		public event EventHandler Cancelled;
		public Instruction CurrentInst
		{
			/*get { return currentInst; }*/
			set
			{
				if (currentInst != value)
				{
					currentInst = value;
					// this method is used to inform the panel of the new instruction
					// to be working on.  As such, it's where all the controls on the
					// panel get updated.
					btnCancel.Enabled = false;
					tbOpCode.Enabled = btnOpCode.Enabled = 
						tbReserved.Enabled = 
						cbTrue.Enabled = cbFalse.Enabled = 
						(!readOnly && (currentInst != null));
					foreach (Control c in aOpWords) { c.Enabled = (!readOnly && (currentInst != null)); }
					foreach (Control c in aOpBytes) { c.Enabled = (!readOnly && (currentInst != null)); }

					internalchg = true;
					if (currentInst != null)
					{
						tbOpCode.Text = "0x"+Helper.HexString(currentInst.Opcode);
						llViewBHAV.Enabled = (BhavNameWizProvider.For(currentInst).LoadBHAV() != null);
						for (int i = 0; i < aTargets.Length; i++) setTarget(i);
						tbReserved.Text = "0x"+Helper.HexString(currentInst.Reserved0);
						for (int i = 0; i < aOpWords.Length; i++) aOpWords[i].Text = decOpWord(i);
						for (int i = 0; i < aOpBytes.Length; i++) aOpBytes[i].Text = hexOpByte(i);
						btnOperandWiz.Enabled = !readOnly && (BhavOperandWizProvider.For(currentInst) != null);
						tbInstruction.Text = BhavNameWizProvider.For(currentInst).LongName;
					}
					else
					{
						tbOpCode.Text = "";
						llViewBHAV.Enabled = false;
						cbTrue.SelectedIndex = cbFalse.SelectedIndex = 0;
						tbReserved.Text = "";
						foreach (TextBox c in aOpWords) { c.Text = ""; }
						foreach (TextBox c in aOpBytes) { c.Text = ""; }
						btnOperandWiz.Enabled = false;
						tbInstruction.Text = "(no instruction selected)";
					}
					internalchg = false;
				}
			}
		}

		public bool ReadOnly
		{
			get { return readOnly; }
			set
			{
				if (readOnly != value)
				{
					readOnly = value;
					// Read only makes a difference when there's a selected instruction
					if (currentInst != null)
					{
						tbOpCode.Enabled = btnOpCode.Enabled = 
							tbReserved.Enabled = 
							cbTrue.Enabled = cbFalse.Enabled = 
							!readOnly;
						foreach (Control c in aOpWords) { c.Enabled = !readOnly; }
						foreach (Control c in aOpBytes) { c.Enabled = !readOnly; }
						btnOperandWiz.Enabled = !readOnly && (BhavOperandWizProvider.For(currentInst) != null);
					}
				}
			}
		}

		public void EnableCancel() { this.btnCancel.Enabled = true; }


		protected virtual void OnCancelled(EventArgs e)
		{
			if (Cancelled != null) Cancelled(this, e);
		}


		private string decOpWord(int o)
		{
			return (currentInst.Operands[o*2 + 0] + (currentInst.Operands[o*2 + 1] << 8)).ToString();
		}

		private string hexOpByte(int o)
		{
			string s = Helper.HexString((o < 8) ? currentInst.Operands[o] : currentInst.Reserved1[o-8]);
			return s;
		}

		private void setTarget(int t)
		{
			ComboBox target = (ComboBox)aTargets[t];
			ushort addr = (t == 0) ? currentInst.Target1 : currentInst.Target2;
			if (addr >= 0xFFFC)
			{
				target.SelectedIndex = addr - 0xFFFC;
			}
			else
			{
				target.SelectedIndex = -1;
				target.Text = "0x"+Helper.HexString(addr);
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
			this.tbOp01_dec = new System.Windows.Forms.TextBox();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOperandWiz = new System.Windows.Forms.Button();
			this.llViewBHAV = new System.Windows.Forms.LinkLabel();
			this.cbFalse = new System.Windows.Forms.ComboBox();
			this.cbTrue = new System.Windows.Forms.ComboBox();
			this.label13 = new System.Windows.Forms.Label();
			this.tbOpF = new System.Windows.Forms.TextBox();
			this.tbOpE = new System.Windows.Forms.TextBox();
			this.tbOpD = new System.Windows.Forms.TextBox();
			this.tbOpC = new System.Windows.Forms.TextBox();
			this.tbOpB = new System.Windows.Forms.TextBox();
			this.tbOpA = new System.Windows.Forms.TextBox();
			this.tbOp9 = new System.Windows.Forms.TextBox();
			this.tbOp8 = new System.Windows.Forms.TextBox();
			this.tbOp7 = new System.Windows.Forms.TextBox();
			this.tbOp6 = new System.Windows.Forms.TextBox();
			this.tbOp5 = new System.Windows.Forms.TextBox();
			this.tbOp4 = new System.Windows.Forms.TextBox();
			this.tbOp3 = new System.Windows.Forms.TextBox();
			this.tbOp2 = new System.Windows.Forms.TextBox();
			this.tbOp1 = new System.Windows.Forms.TextBox();
			this.tbOp0 = new System.Windows.Forms.TextBox();
			this.tbReserved = new System.Windows.Forms.TextBox();
			this.tbOpCode = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.btnOpCode = new System.Windows.Forms.Button();
			this.tbInstruction = new System.Windows.Forms.TextBox();
			this.tbOp23_dec = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// tbOp01_dec
			// 
			this.tbOp01_dec.Font = new System.Drawing.Font("Verdana", 8.25F);
			this.tbOp01_dec.Location = new System.Drawing.Point(64, 82);
			this.tbOp01_dec.MaxLength = 5;
			this.tbOp01_dec.Name = "tbOp01_dec";
			this.tbOp01_dec.Size = new System.Drawing.Size(48, 21);
			this.tbOp01_dec.TabIndex = 10;
			this.tbOp01_dec.Text = "88888";
			this.tbOp01_dec.Validating += new System.ComponentModel.CancelEventHandler(this.dec16_Validating);
			this.tbOp01_dec.TextChanged += new System.EventHandler(this.OpWord_TextChanged);
			// 
			// btnCancel
			// 
			this.btnCancel.CausesValidation = false;
			this.btnCancel.Font = new System.Drawing.Font("Verdana", 8.25F);
			this.btnCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnCancel.Location = new System.Drawing.Point(200, 49);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(56, 20);
			this.btnCancel.TabIndex = 32;
			this.btnCancel.Text = "&Cancel";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnOperandWiz
			// 
			this.btnOperandWiz.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnOperandWiz.Font = new System.Drawing.Font("Verdana", 8.25F);
			this.btnOperandWiz.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnOperandWiz.Location = new System.Drawing.Point(6, 107);
			this.btnOperandWiz.Name = "btnOperandWiz";
			this.btnOperandWiz.Size = new System.Drawing.Size(56, 20);
			this.btnOperandWiz.TabIndex = 13;
			this.btnOperandWiz.Text = "&Wizard";
			this.btnOperandWiz.Click += new System.EventHandler(this.btnOperandWiz_Click);
			// 
			// llViewBHAV
			// 
			this.llViewBHAV.AutoSize = true;
			this.llViewBHAV.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
			this.llViewBHAV.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.llViewBHAV.LinkArea = new System.Windows.Forms.LinkArea(0, 11);
			this.llViewBHAV.Location = new System.Drawing.Point(153, 4);
			this.llViewBHAV.Name = "llViewBHAV";
			this.llViewBHAV.Size = new System.Drawing.Size(73, 17);
			this.llViewBHAV.TabIndex = 4;
			this.llViewBHAV.TabStop = true;
			this.llViewBHAV.Text = "view BHAV";
			this.llViewBHAV.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llViewBHAV_LinkClicked);
			// 
			// cbFalse
			// 
			this.cbFalse.Font = new System.Drawing.Font("Verdana", 8.25F);
			this.cbFalse.ItemHeight = 13;
			this.cbFalse.Items.AddRange(new object[] {
														 "Error",
														 "Return True",
														 "Return False"});
			this.cbFalse.Location = new System.Drawing.Point(56, 48);
			this.cbFalse.Name = "cbFalse";
			this.cbFalse.Size = new System.Drawing.Size(96, 21);
			this.cbFalse.TabIndex = 8;
			this.cbFalse.Text = "False";
			this.cbFalse.Validating += new System.ComponentModel.CancelEventHandler(this.Target_Validating);
			this.cbFalse.TextChanged += new System.EventHandler(this.Target_TextChanged);
			this.cbFalse.SelectedValueChanged += new System.EventHandler(this.Target_SelectedValueChanged);
			// 
			// cbTrue
			// 
			this.cbTrue.Font = new System.Drawing.Font("Verdana", 8.25F);
			this.cbTrue.ItemHeight = 13;
			this.cbTrue.Items.AddRange(new object[] {
														"Error",
														"Return True",
														"Return False"});
			this.cbTrue.Location = new System.Drawing.Point(56, 24);
			this.cbTrue.Name = "cbTrue";
			this.cbTrue.Size = new System.Drawing.Size(96, 21);
			this.cbTrue.TabIndex = 6;
			this.cbTrue.Text = "True";
			this.cbTrue.Validating += new System.ComponentModel.CancelEventHandler(this.Target_Validating);
			this.cbTrue.TextChanged += new System.EventHandler(this.Target_TextChanged);
			this.cbTrue.SelectedValueChanged += new System.EventHandler(this.Target_SelectedValueChanged);
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.Font = new System.Drawing.Font("Verdana", 8.25F);
			this.label13.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label13.Location = new System.Drawing.Point(5, 85);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(59, 17);
			this.label13.TabIndex = 9;
			this.label13.Text = "&Operands";
			// 
			// tbOpF
			// 
			this.tbOpF.Font = new System.Drawing.Font("Verdana", 8.25F);
			this.tbOpF.Location = new System.Drawing.Point(232, 130);
			this.tbOpF.MaxLength = 2;
			this.tbOpF.Name = "tbOpF";
			this.tbOpF.Size = new System.Drawing.Size(24, 21);
			this.tbOpF.TabIndex = 29;
			this.tbOpF.Text = "0";
			this.tbOpF.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
			this.tbOpF.TextChanged += new System.EventHandler(this.OpByte_TextChanged);
			// 
			// tbOpE
			// 
			this.tbOpE.Font = new System.Drawing.Font("Verdana", 8.25F);
			this.tbOpE.Location = new System.Drawing.Point(208, 130);
			this.tbOpE.MaxLength = 2;
			this.tbOpE.Name = "tbOpE";
			this.tbOpE.Size = new System.Drawing.Size(24, 21);
			this.tbOpE.TabIndex = 28;
			this.tbOpE.Text = "0";
			this.tbOpE.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
			this.tbOpE.TextChanged += new System.EventHandler(this.OpByte_TextChanged);
			// 
			// tbOpD
			// 
			this.tbOpD.Font = new System.Drawing.Font("Verdana", 8.25F);
			this.tbOpD.Location = new System.Drawing.Point(184, 130);
			this.tbOpD.MaxLength = 2;
			this.tbOpD.Name = "tbOpD";
			this.tbOpD.Size = new System.Drawing.Size(24, 21);
			this.tbOpD.TabIndex = 27;
			this.tbOpD.Text = "0";
			this.tbOpD.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
			this.tbOpD.TextChanged += new System.EventHandler(this.OpByte_TextChanged);
			// 
			// tbOpC
			// 
			this.tbOpC.Font = new System.Drawing.Font("Verdana", 8.25F);
			this.tbOpC.Location = new System.Drawing.Point(160, 130);
			this.tbOpC.MaxLength = 2;
			this.tbOpC.Name = "tbOpC";
			this.tbOpC.Size = new System.Drawing.Size(24, 21);
			this.tbOpC.TabIndex = 26;
			this.tbOpC.Text = "0";
			this.tbOpC.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
			this.tbOpC.TextChanged += new System.EventHandler(this.OpByte_TextChanged);
			// 
			// tbOpB
			// 
			this.tbOpB.Font = new System.Drawing.Font("Verdana", 8.25F);
			this.tbOpB.Location = new System.Drawing.Point(136, 130);
			this.tbOpB.MaxLength = 2;
			this.tbOpB.Name = "tbOpB";
			this.tbOpB.Size = new System.Drawing.Size(24, 21);
			this.tbOpB.TabIndex = 25;
			this.tbOpB.Text = "0";
			this.tbOpB.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
			this.tbOpB.TextChanged += new System.EventHandler(this.OpByte_TextChanged);
			// 
			// tbOpA
			// 
			this.tbOpA.Font = new System.Drawing.Font("Verdana", 8.25F);
			this.tbOpA.Location = new System.Drawing.Point(112, 130);
			this.tbOpA.MaxLength = 2;
			this.tbOpA.Name = "tbOpA";
			this.tbOpA.Size = new System.Drawing.Size(24, 21);
			this.tbOpA.TabIndex = 24;
			this.tbOpA.Text = "0";
			this.tbOpA.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
			this.tbOpA.TextChanged += new System.EventHandler(this.OpByte_TextChanged);
			// 
			// tbOp9
			// 
			this.tbOp9.Font = new System.Drawing.Font("Verdana", 8.25F);
			this.tbOp9.Location = new System.Drawing.Point(88, 130);
			this.tbOp9.MaxLength = 2;
			this.tbOp9.Name = "tbOp9";
			this.tbOp9.Size = new System.Drawing.Size(24, 21);
			this.tbOp9.TabIndex = 23;
			this.tbOp9.Text = "0";
			this.tbOp9.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
			this.tbOp9.TextChanged += new System.EventHandler(this.OpByte_TextChanged);
			// 
			// tbOp8
			// 
			this.tbOp8.Font = new System.Drawing.Font("Verdana", 8.25F);
			this.tbOp8.Location = new System.Drawing.Point(64, 130);
			this.tbOp8.MaxLength = 2;
			this.tbOp8.Name = "tbOp8";
			this.tbOp8.Size = new System.Drawing.Size(24, 21);
			this.tbOp8.TabIndex = 22;
			this.tbOp8.Text = "0";
			this.tbOp8.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
			this.tbOp8.TextChanged += new System.EventHandler(this.OpByte_TextChanged);
			// 
			// tbOp7
			// 
			this.tbOp7.Font = new System.Drawing.Font("Verdana", 8.25F);
			this.tbOp7.Location = new System.Drawing.Point(232, 106);
			this.tbOp7.MaxLength = 2;
			this.tbOp7.Name = "tbOp7";
			this.tbOp7.Size = new System.Drawing.Size(24, 21);
			this.tbOp7.TabIndex = 21;
			this.tbOp7.Text = "0";
			this.tbOp7.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
			this.tbOp7.TextChanged += new System.EventHandler(this.OpByte_TextChanged);
			// 
			// tbOp6
			// 
			this.tbOp6.Font = new System.Drawing.Font("Verdana", 8.25F);
			this.tbOp6.Location = new System.Drawing.Point(208, 106);
			this.tbOp6.MaxLength = 2;
			this.tbOp6.Name = "tbOp6";
			this.tbOp6.Size = new System.Drawing.Size(24, 21);
			this.tbOp6.TabIndex = 20;
			this.tbOp6.Text = "0";
			this.tbOp6.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
			this.tbOp6.TextChanged += new System.EventHandler(this.OpByte_TextChanged);
			// 
			// tbOp5
			// 
			this.tbOp5.Font = new System.Drawing.Font("Verdana", 8.25F);
			this.tbOp5.Location = new System.Drawing.Point(184, 106);
			this.tbOp5.MaxLength = 2;
			this.tbOp5.Name = "tbOp5";
			this.tbOp5.Size = new System.Drawing.Size(24, 21);
			this.tbOp5.TabIndex = 19;
			this.tbOp5.Text = "0";
			this.tbOp5.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
			this.tbOp5.TextChanged += new System.EventHandler(this.OpByte_TextChanged);
			// 
			// tbOp4
			// 
			this.tbOp4.Font = new System.Drawing.Font("Verdana", 8.25F);
			this.tbOp4.Location = new System.Drawing.Point(160, 106);
			this.tbOp4.MaxLength = 2;
			this.tbOp4.Name = "tbOp4";
			this.tbOp4.Size = new System.Drawing.Size(24, 21);
			this.tbOp4.TabIndex = 18;
			this.tbOp4.Text = "0";
			this.tbOp4.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
			this.tbOp4.TextChanged += new System.EventHandler(this.OpByte_TextChanged);
			// 
			// tbOp3
			// 
			this.tbOp3.Font = new System.Drawing.Font("Verdana", 8.25F);
			this.tbOp3.Location = new System.Drawing.Point(136, 106);
			this.tbOp3.MaxLength = 2;
			this.tbOp3.Name = "tbOp3";
			this.tbOp3.Size = new System.Drawing.Size(24, 21);
			this.tbOp3.TabIndex = 17;
			this.tbOp3.Text = "0";
			this.tbOp3.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
			this.tbOp3.TextChanged += new System.EventHandler(this.OpByte_TextChanged);
			// 
			// tbOp2
			// 
			this.tbOp2.Font = new System.Drawing.Font("Verdana", 8.25F);
			this.tbOp2.Location = new System.Drawing.Point(112, 106);
			this.tbOp2.MaxLength = 2;
			this.tbOp2.Name = "tbOp2";
			this.tbOp2.Size = new System.Drawing.Size(24, 21);
			this.tbOp2.TabIndex = 16;
			this.tbOp2.Text = "0";
			this.tbOp2.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
			this.tbOp2.TextChanged += new System.EventHandler(this.OpByte_TextChanged);
			// 
			// tbOp1
			// 
			this.tbOp1.Font = new System.Drawing.Font("Verdana", 8.25F);
			this.tbOp1.Location = new System.Drawing.Point(88, 106);
			this.tbOp1.MaxLength = 2;
			this.tbOp1.Name = "tbOp1";
			this.tbOp1.Size = new System.Drawing.Size(24, 21);
			this.tbOp1.TabIndex = 15;
			this.tbOp1.Text = "0";
			this.tbOp1.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
			this.tbOp1.TextChanged += new System.EventHandler(this.OpByte_TextChanged);
			// 
			// tbOp0
			// 
			this.tbOp0.Font = new System.Drawing.Font("Verdana", 8.25F);
			this.tbOp0.Location = new System.Drawing.Point(64, 106);
			this.tbOp0.MaxLength = 2;
			this.tbOp0.Name = "tbOp0";
			this.tbOp0.Size = new System.Drawing.Size(24, 21);
			this.tbOp0.TabIndex = 14;
			this.tbOp0.Text = "DD";
			this.tbOp0.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
			this.tbOp0.TextChanged += new System.EventHandler(this.OpByte_TextChanged);
			// 
			// tbReserved
			// 
			this.tbReserved.Font = new System.Drawing.Font("Verdana", 8.25F);
			this.tbReserved.Location = new System.Drawing.Point(216, 24);
			this.tbReserved.MaxLength = 4;
			this.tbReserved.Name = "tbReserved";
			this.tbReserved.Size = new System.Drawing.Size(40, 21);
			this.tbReserved.TabIndex = 31;
			this.tbReserved.Text = "0xDD";
			this.tbReserved.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
			this.tbReserved.TextChanged += new System.EventHandler(this.tbReserved_TextChanged);
			// 
			// tbOpCode
			// 
			this.tbOpCode.Font = new System.Drawing.Font("Verdana", 8.25F);
			this.tbOpCode.Location = new System.Drawing.Point(56, 0);
			this.tbOpCode.MaxLength = 6;
			this.tbOpCode.Name = "tbOpCode";
			this.tbOpCode.Size = new System.Drawing.Size(76, 21);
			this.tbOpCode.TabIndex = 2;
			this.tbOpCode.Text = "0xDDDD";
			this.tbOpCode.Validating += new System.ComponentModel.CancelEventHandler(this.hex16_Validating);
			this.tbOpCode.TextChanged += new System.EventHandler(this.tbOpCode_TextChanged);
			this.tbOpCode.DoubleClick += new System.EventHandler(this.tbOpCode_DoubleClick);
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Font = new System.Drawing.Font("Verdana", 8.25F);
			this.label10.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label10.Location = new System.Drawing.Point(159, 27);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(57, 17);
			this.label10.TabIndex = 6;
			this.label10.Text = "Reserved";
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Font = new System.Drawing.Font("Verdana", 8.25F);
			this.label9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label9.Location = new System.Drawing.Point(9, 3);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(47, 17);
			this.label9.TabIndex = 1;
			this.label9.Text = "Opcode";
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Font = new System.Drawing.Font("Verdana", 8.25F);
			this.label12.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label12.Location = new System.Drawing.Point(12, 51);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(43, 17);
			this.label12.TabIndex = 7;
			this.label12.Text = "If false";
			// 
			// btnOpCode
			// 
			this.btnOpCode.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnOpCode.Font = new System.Drawing.Font("Webdings", 10F);
			this.btnOpCode.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnOpCode.Location = new System.Drawing.Point(132, 2);
			this.btnOpCode.Name = "btnOpCode";
			this.btnOpCode.Size = new System.Drawing.Size(18, 18);
			this.btnOpCode.TabIndex = 3;
			this.btnOpCode.Text = "4";
			this.btnOpCode.Click += new System.EventHandler(this.btnOpCode_Click);
			// 
			// tbInstruction
			// 
			this.tbInstruction.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.tbInstruction.BackColor = System.Drawing.SystemColors.Control;
			this.tbInstruction.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.tbInstruction.Font = new System.Drawing.Font("Verdana", 8.25F);
			this.tbInstruction.Location = new System.Drawing.Point(0, 168);
			this.tbInstruction.Multiline = true;
			this.tbInstruction.Name = "tbInstruction";
			this.tbInstruction.ReadOnly = true;
			this.tbInstruction.Size = new System.Drawing.Size(256, 48);
			this.tbInstruction.TabIndex = 30;
			this.tbInstruction.TabStop = false;
			this.tbInstruction.Text = "Instruction";
			// 
			// tbOp23_dec
			// 
			this.tbOp23_dec.Font = new System.Drawing.Font("Verdana", 8.25F);
			this.tbOp23_dec.Location = new System.Drawing.Point(112, 82);
			this.tbOp23_dec.MaxLength = 5;
			this.tbOp23_dec.Name = "tbOp23_dec";
			this.tbOp23_dec.Size = new System.Drawing.Size(48, 21);
			this.tbOp23_dec.TabIndex = 11;
			this.tbOp23_dec.Text = "0";
			this.tbOp23_dec.Validating += new System.ComponentModel.CancelEventHandler(this.dec16_Validating);
			this.tbOp23_dec.TextChanged += new System.EventHandler(this.OpWord_TextChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Verdana", 8.25F);
			this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label2.Location = new System.Drawing.Point(160, 86);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(58, 17);
			this.label2.TabIndex = 12;
			this.label2.Text = "(decimal)";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Verdana", 8.25F);
			this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label1.Location = new System.Drawing.Point(16, 27);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(40, 17);
			this.label1.TabIndex = 5;
			this.label1.Text = "If true";
			// 
			// BhavInstPanel
			// 
			this.Controls.Add(this.tbOp01_dec);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOperandWiz);
			this.Controls.Add(this.llViewBHAV);
			this.Controls.Add(this.cbFalse);
			this.Controls.Add(this.cbTrue);
			this.Controls.Add(this.label13);
			this.Controls.Add(this.tbOpF);
			this.Controls.Add(this.tbOpE);
			this.Controls.Add(this.tbOpD);
			this.Controls.Add(this.tbOpC);
			this.Controls.Add(this.tbOpB);
			this.Controls.Add(this.tbOpA);
			this.Controls.Add(this.tbOp9);
			this.Controls.Add(this.tbOp8);
			this.Controls.Add(this.tbOp7);
			this.Controls.Add(this.tbOp6);
			this.Controls.Add(this.tbOp5);
			this.Controls.Add(this.tbOp4);
			this.Controls.Add(this.tbOp3);
			this.Controls.Add(this.tbOp2);
			this.Controls.Add(this.tbOp1);
			this.Controls.Add(this.tbOp0);
			this.Controls.Add(this.tbReserved);
			this.Controls.Add(this.tbOpCode);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.label12);
			this.Controls.Add(this.btnOpCode);
			this.Controls.Add(this.tbInstruction);
			this.Controls.Add(this.tbOp23_dec);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Name = "BhavInstPanel";
			this.Size = new System.Drawing.Size(256, 216);
			this.ResumeLayout(false);

		}
		#endregion

		private void tbOpCode_DoubleClick(object sender, System.EventArgs e)
		{
			if (!llViewBHAV.Enabled) return;
			llViewBHAV_LinkClicked(null, null);
		}

		private void btnOpCode_Click(object sender, System.EventArgs e)
		{
			int opcode = SimPe.Plugin.WrapperFactory.BhavWizardForm.Execute(currentInst.Parent, this.Parent);

			if (opcode != -1)
			{
				currentInst.Opcode = (ushort)opcode;
				tbOpCode.Text = "0x"+Helper.HexString((ushort)opcode);
				tbInstruction.Text = BhavNameWizProvider.For(currentInst).LongName;
			}
		}

		private void llViewBHAV_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			if (currentInst == null) return;

			ABhavNameWiz nameWiz = pjse.BhavNameWizProvider.For(currentInst);

			Bhav b = nameWiz.LoadBHAV();
			BhavForm ui = (BhavForm)b.UIHandler;
			//?? ui.tbInst_Instruction.Width = ui.gbInstruction.Width - (2 * ui.tbInst_Instruction.Location.X);
			ui.Tag = "Popup"; // tells the SetReadOnly function it's in a popup - so everything locked down
			ui.Text = "View BHAV: " + nameWiz.ShortName;
			b.RefreshUI();
			ui.Show();
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			OnCancelled(new System.EventArgs());
		}

		private void btnOperandWiz_Click(object sender, System.EventArgs e)
		{
			if (currentInst == null) return;

			if ((new BhavOperandWiz()).Execute(currentInst) != null) 
			{
				for (int i = 0; i < aOpWords.Length; i++) aOpWords[i].Text = decOpWord(i);
				for (int i = 0; i < aOpBytes.Length; i++) aOpBytes[i].Text = hexOpByte(i);
			}
		}


		private void tbOpCode_TextChanged(object sender, System.EventArgs e)
		{
			if (internalchg) return;
			if (currentInst == null) return;

			System.ComponentModel.CancelEventArgs cea = new CancelEventArgs(false);
			hex16_Validating(sender, cea);
			if (cea.Cancel)
			{
				llViewBHAV.Enabled = false;
			}
			else
			{
				currentInst.Opcode = Convert.ToUInt16(((TextBox)sender).Text, 16);
				tbInstruction.Text = BhavNameWizProvider.For(currentInst).LongName;
				llViewBHAV.Enabled = (BhavNameWizProvider.For(currentInst).LoadBHAV() != null);
			}
		}

		private void Target_TextChanged(object sender, System.EventArgs e)
		{
			if (internalchg) return;
			if (currentInst == null) return;

			int val = -1;
			if (((ComboBox)sender).Text.Equals("")) 
				val = 0;
			else
				try { val = Convert.ToUInt16(((ComboBox)sender).Text, 16); }
				catch (Exception) { return; }

			if ((new ArrayList(aTargets)).IndexOf(sender) == 0 && val >= 0)
				currentInst.Target1 = (ushort) val;
			else
				currentInst.Target2 = (ushort) val;
		}

		private void Target_SelectedValueChanged(object sender, System.EventArgs e)
		{
			if (internalchg) return;
			if (currentInst == null) return;

			if (((ComboBox)sender).SelectedIndex != -1)
			{
				if ((new ArrayList(aTargets)).IndexOf(sender) == 0)
					currentInst.Target1 = (ushort)(0x0FFFC + ((ComboBox)sender).SelectedIndex);
				else
					currentInst.Target2 = (ushort)(0x0FFFC + ((ComboBox)sender).SelectedIndex);
			}
		}

		private void OpWord_TextChanged(object sender, System.EventArgs e)
		{
			if (internalchg) return;
			if (currentInst == null) return;

			System.ComponentModel.CancelEventArgs cea = new CancelEventArgs(false);
			dec16_Validating(sender, cea);
			if (cea.Cancel) return;

			int i = (new ArrayList(aOpWords)).IndexOf(sender);
			if (i < 0)
				throw new Exception("OpWord_TextChanged not applicable to control " + sender.ToString());

			ushort val = 0;
			if (!((TextBox)sender).Text.Equals("")) val = Convert.ToUInt16(((TextBox)sender).Text);

			currentInst.Operands[i] = (byte)(val & 0xFF);
			currentInst.Operands[i+1] = (byte)((val >> 8) & 0xFF);

			internalchg = true;
			aOpBytes[i].Text = hexOpByte(i);
			aOpBytes[i+1].Text = hexOpByte(i+1);
			internalchg = false;
			tbInstruction.Text = BhavNameWizProvider.For(currentInst).LongName;
		}

		private void OpByte_TextChanged(object sender, System.EventArgs e)
		{
			if (internalchg) return;
			if (currentInst == null) return;

			System.ComponentModel.CancelEventArgs cea = new CancelEventArgs(false);
			hex8_Validating(sender, cea);
			if (cea.Cancel) return;

			int i = (new ArrayList(aOpBytes)).IndexOf(sender);
			if (i < 0)
				throw new Exception("OpByte_TextChanged not applicable to control " + sender.ToString());

			byte val = 0;
			if (!((TextBox)sender).Text.Equals("")) val = Convert.ToByte(((TextBox)sender).Text, 16);
			if (i < 8)
				currentInst.Operands[i] = val;
			else
				currentInst.Reserved1[i-8] = val;

			internalchg = true;
			if (i < 4)
				aOpWords[i / 2].Text = decOpWord(i / 2);
			internalchg = false;
			tbInstruction.Text = BhavNameWizProvider.For(currentInst).LongName;
		}

		private void tbReserved_TextChanged(object sender, System.EventArgs e)
		{
			if (internalchg) return;
			if (currentInst == null) return;

			System.ComponentModel.CancelEventArgs cea = new CancelEventArgs(false);
			hex8_Validating(sender, cea);
			if (cea.Cancel) return;

			currentInst.Reserved0 = Convert.ToByte(((TextBox)sender).Text, 16);
		}


		private void hex8_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			try { if (!((TextBox)sender).Text.Equals("")) Convert.ToByte(((TextBox)sender).Text, 16); }
			catch (Exception) { e.Cancel = true; }
		}

		private void hex16_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			try { if (!((TextBox)sender).Text.Equals("")) Convert.ToUInt16(((TextBox)sender).Text, 16); }
			catch (Exception) { e.Cancel = true; }
		}

		private void dec16_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			try { if (!((TextBox)sender).Text.Equals("")) Convert.ToUInt16(((TextBox)sender).Text); }
			catch (Exception) { e.Cancel = true; }
		}

		private void Target_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (((ComboBox)sender).SelectedIndex == -1)
				try { if (!((ComboBox)sender).Text.Equals("")) Convert.ToUInt16(((ComboBox)sender).Text, 16); }
				catch (Exception) { e.Cancel = true; }
		}

	}
}
