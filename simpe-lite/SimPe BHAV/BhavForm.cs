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
		private System.Windows.Forms.Label lbFilename;
		private System.Windows.Forms.Label lbFormat;
		private System.Windows.Forms.Label lbType;
		private System.Windows.Forms.Label lbLocalC;
		private System.Windows.Forms.Label lbFlags;
		private System.Windows.Forms.Label lbReserved;
		private System.Windows.Forms.TextBox tbFilename;
		private System.Windows.Forms.TextBox tbType;
		private System.Windows.Forms.TextBox tbArgC;
		private System.Windows.Forms.TextBox tbLocalC;
		private System.Windows.Forms.TextBox tbReserved;
		private System.Windows.Forms.ComboBox tba1;
		private System.Windows.Forms.ComboBox tba2;
		private System.Windows.Forms.LinkLabel llopenbhav;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label13;
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
		private System.Windows.Forms.GroupBox gbInstruction;
		private System.Windows.Forms.Panel bhavPanel;
		private System.Windows.Forms.Button btnCommit;
		private System.Windows.Forms.Button btnOpCode;
		private System.Windows.Forms.Button btnOperandWiz;
		private System.Windows.Forms.Button btnSort;
		private System.Windows.Forms.Label lbUpDown;
		private System.Windows.Forms.TextBox tbLines;
		private System.Windows.Forms.Button btnUp;
		private System.Windows.Forms.Button btnDown;
		private System.Windows.Forms.Button btnDel;
		private System.Windows.Forms.Button btnAdd;
		private System.Windows.Forms.Button btnCancel;
		private SimPe.PackedFiles.UserInterface.BhavInstListControl pnflowcontainer;
		private System.Windows.Forms.GroupBox gbMove;
		private System.Windows.Forms.TextBox tbInst_Op01_dec;
		private System.Windows.Forms.TextBox tbInst_Op23_dec;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label lbArgC;
		private System.Windows.Forms.GroupBox gbSpecial;
		private System.Windows.Forms.Button btnInsTrue;
		private System.Windows.Forms.Button btnInsFalse;
		private System.Windows.Forms.Button btnLinkInge;
		private System.Windows.Forms.Button btnDelPescado;
		private System.Windows.Forms.Button btnAppend;
		private System.Windows.Forms.TextBox tbFlags;
		private System.Windows.Forms.ComboBox cbFormat;
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

			Control[] cs = {
				tbLocalC, lbLocalC, tbArgC, lbArgC, tbReserved, lbReserved,
				tbFlags, lbFlags, tbType, lbType, cbFormat, lbFormat };
			int left = this.bhavPanel.Width;
			for (int i = 0; i < cs.Length; i++)
				left = cs[i].Left = left - (cs[i].Width + 4);
			this.lbFilename.Left = 4;
			this.tbFilename.Left = this.lbFilename.Right + 4;
			this.Tag = "Normal"; // Used by SetReadOnly

			TextBox[] iow = { tbInst_Op01_dec, tbInst_Op23_dec, tbLines };
			alDec16 = new ArrayList(iow);
			TextBox[] iob = {
								 tbInst_Op0  ,tbInst_Op1  ,tbInst_Op2  ,tbInst_Op3
								,tbInst_Op4  ,tbInst_Op5  ,tbInst_Op6  ,tbInst_Op7
								,tbInst_Unk0 ,tbInst_Unk1 ,tbInst_Unk2 ,tbInst_Unk3
								,tbInst_Unk4 ,tbInst_Unk5 ,tbInst_Unk6 ,tbInst_Unk7
								,tbInst_Reserved
								,tbType
							};
			alHex8 = new ArrayList(iob);

			TextBox[] w = { tbFlags ,tbReserved ,tbInst_OpCode ,};
			alHex16 = new ArrayList(w);

			TextBox[] db = { tbArgC ,tbLocalC ,};
			alDec8 = new ArrayList(db);

			ComboBox[] cb = { tba1 ,tba2 ,cbFormat ,};
			alHex16cb = new ArrayList(cb);

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
		private bool setHandler = false;
		private Instruction currentInst;
		private Instruction origInst;
		private bool internalchg;
		private ArrayList alDec16;
		private ArrayList alHex8;
		private ArrayList alHex16;
		private ArrayList alDec8;
		private ArrayList alHex16cb;

		private void SetReadOnly(bool state) 
		{
			if (((string)this.Tag).Equals("Popup"))
			{
				// make it very clear it's read only
				tbFilename.Enabled = cbFormat.Enabled = tbType.Enabled = tbArgC.Enabled = 
					tbLocalC.Enabled = tbFlags.Enabled = tbReserved.Enabled =
					btnSort.Visible = btnCommit.Visible = gbMove.Visible = 
					btnDel.Visible = btnAdd.Visible = 
					btnOpCode.Visible = btnOperandWiz.Visible = 
					btnCancel.Visible = false;
				state = true;
			}

			this.tbInst_OpCode.ReadOnly = state;
			this.btnOpCode.Enabled = !state;
			this.tbInst_Reserved.ReadOnly = state;
			this.tba1.Enabled = !state;
			this.tba2.Enabled = !state;

			this.tbInst_Op01_dec.ReadOnly = state;
			this.tbInst_Op23_dec.ReadOnly = state;

			this.tbInst_Op0.ReadOnly = state;
			this.tbInst_Op1.ReadOnly = state;
			this.tbInst_Op2.ReadOnly = state;
			this.tbInst_Op3.ReadOnly = state;
			this.tbInst_Op4.ReadOnly = state;
			this.tbInst_Op5.ReadOnly = state;
			this.tbInst_Op6.ReadOnly = state;
			this.tbInst_Op7.ReadOnly = state;

			this.btnOperandWiz.Enabled = !state;
			
			this.tbInst_Unk0.ReadOnly = state;
			this.tbInst_Unk1.ReadOnly = state;
			this.tbInst_Unk2.ReadOnly = state;
			this.tbInst_Unk3.ReadOnly = state;
			this.tbInst_Unk4.ReadOnly = state;
			this.tbInst_Unk5.ReadOnly = state;
			this.tbInst_Unk6.ReadOnly = state;
			this.tbInst_Unk7.ReadOnly = state;

			this.btnUp.Enabled = !state;
			this.btnDown.Enabled = !state;
			this.tbLines.ReadOnly = state;
			this.btnDelPescado.Enabled = this.btnDel.Enabled = !state;
			this.btnInsTrue.Enabled = this.btnInsFalse.Enabled = this.btnAdd.Enabled = !state;
		}

		private void UpdateInstPanel()
		{
			internalchg = true;
			if (wrapper.Instructions.IndexOf(currentInst) < 0)
			{
				SetReadOnly(true);
				this.btnInsTrue.Enabled = this.btnInsFalse.Enabled = this.btnAdd.Enabled = true;

				this.tbInst_OpCode.Text = "";
				this.tbInst_Reserved.Text = "";
				this.tba1.SelectedIndex = 0;
				this.tba2.SelectedIndex = 0;
				this.tbInst_Op01_dec.Text = "";
				this.tbInst_Op23_dec.Text = "";
				this.tbInst_Op0.Text = "";
				this.tbInst_Op1.Text = "";
				this.tbInst_Op2.Text = "";
				this.tbInst_Op3.Text = "";
				this.tbInst_Op4.Text = "";
				this.tbInst_Op5.Text = "";
				this.tbInst_Op6.Text = "";
				this.tbInst_Op7.Text = "";
				this.tbInst_Unk0.Text = "";
				this.tbInst_Unk1.Text = "";
				this.tbInst_Unk2.Text = "";
				this.tbInst_Unk3.Text = "";
				this.tbInst_Unk4.Text = "";
				this.tbInst_Unk5.Text = "";
				this.tbInst_Unk6.Text = "";
				this.tbInst_Unk7.Text = "";

				this.tbInst_Instruction.Text = "";
			}
			else
			{
				SetReadOnly(false);

				//load referenced Bhav
				Instruction inst = currentInst;
				Bhav b = null;
				if (inst.GlobalBhav)
					b = Instruction.LoadGlobalBHAV(inst.OpCode);
				this.llopenbhav.Enabled = (b!=null);

				this.btnDelPescado.Enabled = this.btnDel.Enabled = wrapper.Instructions.Count > 1;

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

				this.tbInst_Op01_dec.Text = OpsToShort(inst.Operands[0], inst.Operands[1]).ToString();
				this.tbInst_Op23_dec.Text = OpsToShort(inst.Operands[2], inst.Operands[3]).ToString();

				this.tbInst_Op0.Text = Helper.HexString(inst.Operands[0]);
				this.tbInst_Op1.Text = Helper.HexString(inst.Operands[1]);
				this.tbInst_Op2.Text = Helper.HexString(inst.Operands[2]);
				this.tbInst_Op3.Text = Helper.HexString(inst.Operands[3]);
				this.tbInst_Op4.Text = Helper.HexString(inst.Operands[4]);
				this.tbInst_Op5.Text = Helper.HexString(inst.Operands[5]);
				this.tbInst_Op6.Text = Helper.HexString(inst.Operands[6]);
				this.tbInst_Op7.Text = Helper.HexString(inst.Operands[7]);

				this.tbInst_Unk0.Text = Helper.HexString(inst.Reserved1[0]);
				this.tbInst_Unk1.Text = Helper.HexString(inst.Reserved1[1]);
				this.tbInst_Unk2.Text = Helper.HexString(inst.Reserved1[2]);
				this.tbInst_Unk3.Text = Helper.HexString(inst.Reserved1[3]);
				this.tbInst_Unk4.Text = Helper.HexString(inst.Reserved1[4]);
				this.tbInst_Unk5.Text = Helper.HexString(inst.Reserved1[5]);
				this.tbInst_Unk6.Text = Helper.HexString(inst.Reserved1[6]);
				this.tbInst_Unk7.Text = Helper.HexString(inst.Reserved1[7]);

				this.tbInst_Instruction.Text = inst.ToString();

				this.btnOperandWiz.Enabled = BhavOperandWiz.Available(inst);
				this.btnUp.Enabled = pnflowcontainer.SelectedIndex > 0;
				this.btnDown.Enabled = pnflowcontainer.SelectedIndex < wrapper.Instructions.Count - 1;
			}
			internalchg = false;
		}


		private short OpsToShort(byte lo, byte hi)
		{
			ushort uval = (ushort)(lo + (hi << 8));
			if (uval > 32767) return (short)(uval - 65536);
			else return (short)uval;
		}

		private byte[] ShortToOps(short val)
		{
			byte[] ops = new byte[2];
			ushort uval;
			if (val < 0)
				uval = (ushort)(65536 + val);
			else
				uval = (ushort)val;
			ops[0] = (byte)(uval & 0xFF);
			ops[1] = (byte)((uval >> 8) & 0xFF);
			return ops;
		}

		private bool cbHex16_IsValid(object sender)
		{
			if (alHex16cb.IndexOf(sender) < 0)
				throw new Exception("cbHex16_IsValid not applicable to control " + sender.ToString());
			if (((ComboBox)sender).Items.IndexOf(((ComboBox)sender).Text) != -1) return true;

			try { Convert.ToUInt16(((ComboBox)sender).Text, 16); }
			catch (Exception) { return false; }
			return true;
		}

		private bool dec8_IsValid(object sender)
		{
			if (alDec8.IndexOf(sender) < 0)
				throw new Exception("dec8_IsValid not applicable to control " + sender.ToString());
			try { Convert.ToByte(((TextBox)sender).Text); }
			catch (Exception) { return false; }
			return true;
		}

		private bool dec16_IsValid(object sender)
		{
			if (alDec16.IndexOf(sender) < 0)
				throw new Exception("dec16_IsValid not applicable to control " + sender.ToString());
			try { Convert.ToInt16(((TextBox)sender).Text); }
			catch (Exception) { return false; }
			return true;
		}

		private bool hex8_IsValid(object sender)
		{
			if (alHex8.IndexOf(sender) < 0)
				throw new Exception("hex8_IsValid not applicable to control " + sender.ToString());
			try { Convert.ToByte(((TextBox)sender).Text, 16); }
			catch (Exception) { return false; }
			return true;
		}

		private bool hex16_IsValid(object sender)
		{
			if (alHex16.IndexOf(sender) < 0)
				throw new Exception("hex16_IsValid not applicable to control " + sender.ToString());
			try { Convert.ToUInt16(((TextBox)sender).Text, 16); }
			catch (Exception) { return false; }
			return true;
		}

		#endregion

		#region IPackedFileUI Member
		/// <summary>
		/// Returns the Panel that will be displayed within SimPe
		/// </summary>
		public System.Windows.Forms.Control GUIHandle
		{
			get
			{
				return bhavPanel;
			}
		}

		/// <summary>
		/// Is called by SimPe (through the Wrapper) when the Panel is going to be displayed, so
		/// you should update the Data displayed by the Panel with the Attributes stored in the
		/// passed Wrapper.
		/// </summary>
		/// <param name="wrp">The Attributes of this Wrapper have to be displayed</param>
		public void UpdateGUI(IFileWrapper wrp)
		{
			wrapper = (Bhav) wrp;
			this.WrapperChanged(wrapper, null);

			currentInst = null;
			origInst = null;
			this.pnflowcontainer.UpdateGUI(wrapper);
			// pnflowcontainer to install its handler before us.
			if (!setHandler)
			{
				wrapper.WrapperChanged += new System.EventHandler(this.WrapperChanged);
				setHandler = true;
			}
		}

		private void WrapperChanged(object sender, System.EventArgs e)
		{
			this.btnCommit.Enabled = wrapper.Changed;

			// Handler for header
			if (sender == wrapper)
			{
				if (internalchg) return;
				internalchg = true;
				tbFilename.Text = wrapper.FileName;
				tbArgC.Text = wrapper.Header.ArgumentCount.ToString();
				tbFlags.Text = "0x"+Helper.HexString(wrapper.Header.Flags);
				cbFormat.Text = "0x"+Helper.HexString(wrapper.Header.Format);
				tbLocalC.Text = wrapper.Header.LocalVarCount.ToString();
				tbType.Text = "0x"+Helper.HexString(wrapper.Header.Type);
				tbReserved.Text = "0x"+Helper.HexString(wrapper.Header.Zero);
				internalchg = false;
			}

				// Handler for current instruction
			else if (sender == currentInst)
			{
				if (internalchg)
				{
					this.tbInst_Instruction.Text = currentInst.ToString();
					this.btnCancel.Enabled = true;
				}
				else
					pnflowcontainer_SelectedInstChanged(null, null);
			}
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
			this.label1 = new System.Windows.Forms.Label();
			this.gbInstruction = new System.Windows.Forms.GroupBox();
			this.tbInst_Op01_dec = new System.Windows.Forms.TextBox();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOperandWiz = new System.Windows.Forms.Button();
			this.llopenbhav = new System.Windows.Forms.LinkLabel();
			this.tba2 = new System.Windows.Forms.ComboBox();
			this.tba1 = new System.Windows.Forms.ComboBox();
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
			this.tbInst_Op23_dec = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.tbFilename = new System.Windows.Forms.TextBox();
			this.lbFilename = new System.Windows.Forms.Label();
			this.tbReserved = new System.Windows.Forms.TextBox();
			this.tbLocalC = new System.Windows.Forms.TextBox();
			this.tbArgC = new System.Windows.Forms.TextBox();
			this.tbType = new System.Windows.Forms.TextBox();
			this.lbReserved = new System.Windows.Forms.Label();
			this.lbFlags = new System.Windows.Forms.Label();
			this.lbType = new System.Windows.Forms.Label();
			this.lbLocalC = new System.Windows.Forms.Label();
			this.lbArgC = new System.Windows.Forms.Label();
			this.lbFormat = new System.Windows.Forms.Label();
			this.pnHeading = new System.Windows.Forms.Panel();
			this.bhavPanel = new System.Windows.Forms.Panel();
			this.cbFormat = new System.Windows.Forms.ComboBox();
			this.gbSpecial = new System.Windows.Forms.GroupBox();
			this.btnAppend = new System.Windows.Forms.Button();
			this.btnInsTrue = new System.Windows.Forms.Button();
			this.btnInsFalse = new System.Windows.Forms.Button();
			this.btnDelPescado = new System.Windows.Forms.Button();
			this.btnLinkInge = new System.Windows.Forms.Button();
			this.pnflowcontainer = new SimPe.PackedFiles.UserInterface.BhavInstListControl();
			this.btnDel = new System.Windows.Forms.Button();
			this.gbMove = new System.Windows.Forms.GroupBox();
			this.btnUp = new System.Windows.Forms.Button();
			this.btnDown = new System.Windows.Forms.Button();
			this.lbUpDown = new System.Windows.Forms.Label();
			this.tbLines = new System.Windows.Forms.TextBox();
			this.btnSort = new System.Windows.Forms.Button();
			this.btnCommit = new System.Windows.Forms.Button();
			this.tbFlags = new System.Windows.Forms.TextBox();
			this.btnAdd = new System.Windows.Forms.Button();
			this.gbInstruction.SuspendLayout();
			this.pnHeading.SuspendLayout();
			this.bhavPanel.SuspendLayout();
			this.gbSpecial.SuspendLayout();
			this.gbMove.SuspendLayout();
			this.SuspendLayout();
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
			this.gbInstruction.Controls.Add(this.tbInst_Op01_dec);
			this.gbInstruction.Controls.Add(this.btnCancel);
			this.gbInstruction.Controls.Add(this.btnOperandWiz);
			this.gbInstruction.Controls.Add(this.llopenbhav);
			this.gbInstruction.Controls.Add(this.tba2);
			this.gbInstruction.Controls.Add(this.tba1);
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
			this.gbInstruction.Controls.Add(this.tbInst_Op23_dec);
			this.gbInstruction.Controls.Add(this.label2);
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
			// tbInst_Op01_dec
			// 
			this.tbInst_Op01_dec.AccessibleDescription = resources.GetString("tbInst_Op01_dec.AccessibleDescription");
			this.tbInst_Op01_dec.AccessibleName = resources.GetString("tbInst_Op01_dec.AccessibleName");
			this.tbInst_Op01_dec.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbInst_Op01_dec.Anchor")));
			this.tbInst_Op01_dec.AutoSize = ((bool)(resources.GetObject("tbInst_Op01_dec.AutoSize")));
			this.tbInst_Op01_dec.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbInst_Op01_dec.BackgroundImage")));
			this.tbInst_Op01_dec.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbInst_Op01_dec.Dock")));
			this.tbInst_Op01_dec.Enabled = ((bool)(resources.GetObject("tbInst_Op01_dec.Enabled")));
			this.tbInst_Op01_dec.Font = ((System.Drawing.Font)(resources.GetObject("tbInst_Op01_dec.Font")));
			this.tbInst_Op01_dec.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbInst_Op01_dec.ImeMode")));
			this.tbInst_Op01_dec.Location = ((System.Drawing.Point)(resources.GetObject("tbInst_Op01_dec.Location")));
			this.tbInst_Op01_dec.MaxLength = ((int)(resources.GetObject("tbInst_Op01_dec.MaxLength")));
			this.tbInst_Op01_dec.Multiline = ((bool)(resources.GetObject("tbInst_Op01_dec.Multiline")));
			this.tbInst_Op01_dec.Name = "tbInst_Op01_dec";
			this.tbInst_Op01_dec.PasswordChar = ((char)(resources.GetObject("tbInst_Op01_dec.PasswordChar")));
			this.tbInst_Op01_dec.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbInst_Op01_dec.RightToLeft")));
			this.tbInst_Op01_dec.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbInst_Op01_dec.ScrollBars")));
			this.tbInst_Op01_dec.Size = ((System.Drawing.Size)(resources.GetObject("tbInst_Op01_dec.Size")));
			this.tbInst_Op01_dec.TabIndex = ((int)(resources.GetObject("tbInst_Op01_dec.TabIndex")));
			this.tbInst_Op01_dec.Text = resources.GetString("tbInst_Op01_dec.Text");
			this.tbInst_Op01_dec.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbInst_Op01_dec.TextAlign")));
			this.tbInst_Op01_dec.Visible = ((bool)(resources.GetObject("tbInst_Op01_dec.Visible")));
			this.tbInst_Op01_dec.WordWrap = ((bool)(resources.GetObject("tbInst_Op01_dec.WordWrap")));
			this.tbInst_Op01_dec.Validating += new System.ComponentModel.CancelEventHandler(this.dec16_Validating);
			this.tbInst_Op01_dec.Validated += new System.EventHandler(this.dec16_Validated);
			this.tbInst_Op01_dec.TextChanged += new System.EventHandler(this.dec16_TextChanged);
			// 
			// btnCancel
			// 
			this.btnCancel.AccessibleDescription = resources.GetString("btnCancel.AccessibleDescription");
			this.btnCancel.AccessibleName = resources.GetString("btnCancel.AccessibleName");
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnCancel.Anchor")));
			this.btnCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.BackgroundImage")));
			this.btnCancel.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnCancel.Dock")));
			this.btnCancel.Enabled = ((bool)(resources.GetObject("btnCancel.Enabled")));
			this.btnCancel.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnCancel.FlatStyle")));
			this.btnCancel.Font = ((System.Drawing.Font)(resources.GetObject("btnCancel.Font")));
			this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
			this.btnCancel.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnCancel.ImageAlign")));
			this.btnCancel.ImageIndex = ((int)(resources.GetObject("btnCancel.ImageIndex")));
			this.btnCancel.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnCancel.ImeMode")));
			this.btnCancel.Location = ((System.Drawing.Point)(resources.GetObject("btnCancel.Location")));
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnCancel.RightToLeft")));
			this.btnCancel.Size = ((System.Drawing.Size)(resources.GetObject("btnCancel.Size")));
			this.btnCancel.TabIndex = ((int)(resources.GetObject("btnCancel.TabIndex")));
			this.btnCancel.Text = resources.GetString("btnCancel.Text");
			this.btnCancel.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnCancel.TextAlign")));
			this.btnCancel.Visible = ((bool)(resources.GetObject("btnCancel.Visible")));
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Clicked);
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
			this.tba2.Validating += new System.ComponentModel.CancelEventHandler(this.cbHex16_Validating);
			this.tba2.DragOver += new System.Windows.Forms.DragEventHandler(this.ItemDragEnter);
			this.tba2.Validated += new System.EventHandler(this.cbHex16_Validated);
			this.tba2.DragDrop += new System.Windows.Forms.DragEventHandler(this.ItemDrop);
			this.tba2.TextChanged += new System.EventHandler(this.cbHex16_TextChanged);
			this.tba2.SelectedIndexChanged += new System.EventHandler(this.cbHex16_SelectedIndexChanged);
			this.tba2.QueryContinueDrag += new System.Windows.Forms.QueryContinueDragEventHandler(this.ItemQueryContinueDragTarget);
			this.tba2.DragEnter += new System.Windows.Forms.DragEventHandler(this.ItemDragEnter);
			this.tba2.Enter += new System.EventHandler(this.cbHex16_Enter);
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
			this.tba1.Validating += new System.ComponentModel.CancelEventHandler(this.cbHex16_Validating);
			this.tba1.DragOver += new System.Windows.Forms.DragEventHandler(this.ItemDragEnter);
			this.tba1.Validated += new System.EventHandler(this.cbHex16_Validated);
			this.tba1.DragDrop += new System.Windows.Forms.DragEventHandler(this.ItemDrop);
			this.tba1.TextChanged += new System.EventHandler(this.cbHex16_TextChanged);
			this.tba1.SelectedIndexChanged += new System.EventHandler(this.cbHex16_SelectedIndexChanged);
			this.tba1.QueryContinueDrag += new System.Windows.Forms.QueryContinueDragEventHandler(this.ItemQueryContinueDragTarget);
			this.tba1.DragEnter += new System.Windows.Forms.DragEventHandler(this.ItemDragEnter);
			this.tba1.Enter += new System.EventHandler(this.cbHex16_Enter);
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
			this.tbInst_Unk7.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
			this.tbInst_Unk7.Validated += new System.EventHandler(this.hex8_Validated);
			this.tbInst_Unk7.TextChanged += new System.EventHandler(this.hex8_TextChanged);
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
			this.tbInst_Unk6.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
			this.tbInst_Unk6.Validated += new System.EventHandler(this.hex8_Validated);
			this.tbInst_Unk6.TextChanged += new System.EventHandler(this.hex8_TextChanged);
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
			this.tbInst_Unk5.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
			this.tbInst_Unk5.Validated += new System.EventHandler(this.hex8_Validated);
			this.tbInst_Unk5.TextChanged += new System.EventHandler(this.hex8_TextChanged);
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
			this.tbInst_Unk4.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
			this.tbInst_Unk4.Validated += new System.EventHandler(this.hex8_Validated);
			this.tbInst_Unk4.TextChanged += new System.EventHandler(this.hex8_TextChanged);
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
			this.tbInst_Unk3.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
			this.tbInst_Unk3.Validated += new System.EventHandler(this.hex8_Validated);
			this.tbInst_Unk3.TextChanged += new System.EventHandler(this.hex8_TextChanged);
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
			this.tbInst_Unk2.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
			this.tbInst_Unk2.Validated += new System.EventHandler(this.hex8_Validated);
			this.tbInst_Unk2.TextChanged += new System.EventHandler(this.hex8_TextChanged);
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
			this.tbInst_Unk1.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
			this.tbInst_Unk1.Validated += new System.EventHandler(this.hex8_Validated);
			this.tbInst_Unk1.TextChanged += new System.EventHandler(this.hex8_TextChanged);
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
			this.tbInst_Unk0.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
			this.tbInst_Unk0.Validated += new System.EventHandler(this.hex8_Validated);
			this.tbInst_Unk0.TextChanged += new System.EventHandler(this.hex8_TextChanged);
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
			this.tbInst_Op7.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
			this.tbInst_Op7.Validated += new System.EventHandler(this.hex8_Validated);
			this.tbInst_Op7.TextChanged += new System.EventHandler(this.hex8_TextChanged);
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
			this.tbInst_Op6.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
			this.tbInst_Op6.Validated += new System.EventHandler(this.hex8_Validated);
			this.tbInst_Op6.TextChanged += new System.EventHandler(this.hex8_TextChanged);
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
			this.tbInst_Op5.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
			this.tbInst_Op5.Validated += new System.EventHandler(this.hex8_Validated);
			this.tbInst_Op5.TextChanged += new System.EventHandler(this.hex8_TextChanged);
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
			this.tbInst_Op4.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
			this.tbInst_Op4.Validated += new System.EventHandler(this.hex8_Validated);
			this.tbInst_Op4.TextChanged += new System.EventHandler(this.hex8_TextChanged);
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
			this.tbInst_Op3.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
			this.tbInst_Op3.Validated += new System.EventHandler(this.hex8_Validated);
			this.tbInst_Op3.TextChanged += new System.EventHandler(this.hex8_TextChanged);
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
			this.tbInst_Op2.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
			this.tbInst_Op2.Validated += new System.EventHandler(this.hex8_Validated);
			this.tbInst_Op2.TextChanged += new System.EventHandler(this.hex8_TextChanged);
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
			this.tbInst_Op1.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
			this.tbInst_Op1.Validated += new System.EventHandler(this.hex8_Validated);
			this.tbInst_Op1.TextChanged += new System.EventHandler(this.hex8_TextChanged);
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
			this.tbInst_Op0.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
			this.tbInst_Op0.Validated += new System.EventHandler(this.hex8_Validated);
			this.tbInst_Op0.TextChanged += new System.EventHandler(this.hex8_TextChanged);
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
			this.tbInst_Reserved.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
			this.tbInst_Reserved.Validated += new System.EventHandler(this.hex8_Validated);
			this.tbInst_Reserved.TextChanged += new System.EventHandler(this.hex8_TextChanged);
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
			this.tbInst_OpCode.Validating += new System.ComponentModel.CancelEventHandler(this.hex16_Validating);
			this.tbInst_OpCode.Validated += new System.EventHandler(this.hex16_Validated);
			this.tbInst_OpCode.TextChanged += new System.EventHandler(this.hex16_TextChanged);
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
			// tbInst_Op23_dec
			// 
			this.tbInst_Op23_dec.AccessibleDescription = resources.GetString("tbInst_Op23_dec.AccessibleDescription");
			this.tbInst_Op23_dec.AccessibleName = resources.GetString("tbInst_Op23_dec.AccessibleName");
			this.tbInst_Op23_dec.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbInst_Op23_dec.Anchor")));
			this.tbInst_Op23_dec.AutoSize = ((bool)(resources.GetObject("tbInst_Op23_dec.AutoSize")));
			this.tbInst_Op23_dec.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbInst_Op23_dec.BackgroundImage")));
			this.tbInst_Op23_dec.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbInst_Op23_dec.Dock")));
			this.tbInst_Op23_dec.Enabled = ((bool)(resources.GetObject("tbInst_Op23_dec.Enabled")));
			this.tbInst_Op23_dec.Font = ((System.Drawing.Font)(resources.GetObject("tbInst_Op23_dec.Font")));
			this.tbInst_Op23_dec.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbInst_Op23_dec.ImeMode")));
			this.tbInst_Op23_dec.Location = ((System.Drawing.Point)(resources.GetObject("tbInst_Op23_dec.Location")));
			this.tbInst_Op23_dec.MaxLength = ((int)(resources.GetObject("tbInst_Op23_dec.MaxLength")));
			this.tbInst_Op23_dec.Multiline = ((bool)(resources.GetObject("tbInst_Op23_dec.Multiline")));
			this.tbInst_Op23_dec.Name = "tbInst_Op23_dec";
			this.tbInst_Op23_dec.PasswordChar = ((char)(resources.GetObject("tbInst_Op23_dec.PasswordChar")));
			this.tbInst_Op23_dec.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbInst_Op23_dec.RightToLeft")));
			this.tbInst_Op23_dec.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbInst_Op23_dec.ScrollBars")));
			this.tbInst_Op23_dec.Size = ((System.Drawing.Size)(resources.GetObject("tbInst_Op23_dec.Size")));
			this.tbInst_Op23_dec.TabIndex = ((int)(resources.GetObject("tbInst_Op23_dec.TabIndex")));
			this.tbInst_Op23_dec.Text = resources.GetString("tbInst_Op23_dec.Text");
			this.tbInst_Op23_dec.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbInst_Op23_dec.TextAlign")));
			this.tbInst_Op23_dec.Visible = ((bool)(resources.GetObject("tbInst_Op23_dec.Visible")));
			this.tbInst_Op23_dec.WordWrap = ((bool)(resources.GetObject("tbInst_Op23_dec.WordWrap")));
			this.tbInst_Op23_dec.Validating += new System.ComponentModel.CancelEventHandler(this.dec16_Validating);
			this.tbInst_Op23_dec.Validated += new System.EventHandler(this.dec16_Validated);
			this.tbInst_Op23_dec.TextChanged += new System.EventHandler(this.dec16_TextChanged);
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
			this.tbFilename.Validated += new System.EventHandler(this.tbFilename_Validated);
			this.tbFilename.TextChanged += new System.EventHandler(this.tbFilename_TextChanged);
			// 
			// lbFilename
			// 
			this.lbFilename.AccessibleDescription = resources.GetString("lbFilename.AccessibleDescription");
			this.lbFilename.AccessibleName = resources.GetString("lbFilename.AccessibleName");
			this.lbFilename.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbFilename.Anchor")));
			this.lbFilename.AutoSize = ((bool)(resources.GetObject("lbFilename.AutoSize")));
			this.lbFilename.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbFilename.Dock")));
			this.lbFilename.Enabled = ((bool)(resources.GetObject("lbFilename.Enabled")));
			this.lbFilename.Font = ((System.Drawing.Font)(resources.GetObject("lbFilename.Font")));
			this.lbFilename.Image = ((System.Drawing.Image)(resources.GetObject("lbFilename.Image")));
			this.lbFilename.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbFilename.ImageAlign")));
			this.lbFilename.ImageIndex = ((int)(resources.GetObject("lbFilename.ImageIndex")));
			this.lbFilename.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbFilename.ImeMode")));
			this.lbFilename.Location = ((System.Drawing.Point)(resources.GetObject("lbFilename.Location")));
			this.lbFilename.Name = "lbFilename";
			this.lbFilename.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbFilename.RightToLeft")));
			this.lbFilename.Size = ((System.Drawing.Size)(resources.GetObject("lbFilename.Size")));
			this.lbFilename.TabIndex = ((int)(resources.GetObject("lbFilename.TabIndex")));
			this.lbFilename.Text = resources.GetString("lbFilename.Text");
			this.lbFilename.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbFilename.TextAlign")));
			this.lbFilename.Visible = ((bool)(resources.GetObject("lbFilename.Visible")));
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
			this.tbReserved.Validating += new System.ComponentModel.CancelEventHandler(this.hex16_Validating);
			this.tbReserved.Validated += new System.EventHandler(this.hex16_Validated);
			this.tbReserved.TextChanged += new System.EventHandler(this.hex16_TextChanged);
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
			this.tbLocalC.Validating += new System.ComponentModel.CancelEventHandler(this.dec8_Validating);
			this.tbLocalC.Validated += new System.EventHandler(this.dec8_Validated);
			this.tbLocalC.TextChanged += new System.EventHandler(this.dec8_TextChanged);
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
			this.tbArgC.Validating += new System.ComponentModel.CancelEventHandler(this.dec8_Validating);
			this.tbArgC.Validated += new System.EventHandler(this.dec8_Validated);
			this.tbArgC.TextChanged += new System.EventHandler(this.dec8_TextChanged);
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
			this.tbType.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
			this.tbType.Validated += new System.EventHandler(this.hex8_Validated);
			this.tbType.TextChanged += new System.EventHandler(this.hex8_TextChanged);
			// 
			// lbReserved
			// 
			this.lbReserved.AccessibleDescription = resources.GetString("lbReserved.AccessibleDescription");
			this.lbReserved.AccessibleName = resources.GetString("lbReserved.AccessibleName");
			this.lbReserved.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbReserved.Anchor")));
			this.lbReserved.AutoSize = ((bool)(resources.GetObject("lbReserved.AutoSize")));
			this.lbReserved.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbReserved.Dock")));
			this.lbReserved.Enabled = ((bool)(resources.GetObject("lbReserved.Enabled")));
			this.lbReserved.Font = ((System.Drawing.Font)(resources.GetObject("lbReserved.Font")));
			this.lbReserved.Image = ((System.Drawing.Image)(resources.GetObject("lbReserved.Image")));
			this.lbReserved.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbReserved.ImageAlign")));
			this.lbReserved.ImageIndex = ((int)(resources.GetObject("lbReserved.ImageIndex")));
			this.lbReserved.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbReserved.ImeMode")));
			this.lbReserved.Location = ((System.Drawing.Point)(resources.GetObject("lbReserved.Location")));
			this.lbReserved.Name = "lbReserved";
			this.lbReserved.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbReserved.RightToLeft")));
			this.lbReserved.Size = ((System.Drawing.Size)(resources.GetObject("lbReserved.Size")));
			this.lbReserved.TabIndex = ((int)(resources.GetObject("lbReserved.TabIndex")));
			this.lbReserved.Text = resources.GetString("lbReserved.Text");
			this.lbReserved.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbReserved.TextAlign")));
			this.lbReserved.Visible = ((bool)(resources.GetObject("lbReserved.Visible")));
			// 
			// lbFlags
			// 
			this.lbFlags.AccessibleDescription = resources.GetString("lbFlags.AccessibleDescription");
			this.lbFlags.AccessibleName = resources.GetString("lbFlags.AccessibleName");
			this.lbFlags.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbFlags.Anchor")));
			this.lbFlags.AutoSize = ((bool)(resources.GetObject("lbFlags.AutoSize")));
			this.lbFlags.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbFlags.Dock")));
			this.lbFlags.Enabled = ((bool)(resources.GetObject("lbFlags.Enabled")));
			this.lbFlags.Font = ((System.Drawing.Font)(resources.GetObject("lbFlags.Font")));
			this.lbFlags.Image = ((System.Drawing.Image)(resources.GetObject("lbFlags.Image")));
			this.lbFlags.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbFlags.ImageAlign")));
			this.lbFlags.ImageIndex = ((int)(resources.GetObject("lbFlags.ImageIndex")));
			this.lbFlags.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbFlags.ImeMode")));
			this.lbFlags.Location = ((System.Drawing.Point)(resources.GetObject("lbFlags.Location")));
			this.lbFlags.Name = "lbFlags";
			this.lbFlags.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbFlags.RightToLeft")));
			this.lbFlags.Size = ((System.Drawing.Size)(resources.GetObject("lbFlags.Size")));
			this.lbFlags.TabIndex = ((int)(resources.GetObject("lbFlags.TabIndex")));
			this.lbFlags.Text = resources.GetString("lbFlags.Text");
			this.lbFlags.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbFlags.TextAlign")));
			this.lbFlags.Visible = ((bool)(resources.GetObject("lbFlags.Visible")));
			// 
			// lbType
			// 
			this.lbType.AccessibleDescription = resources.GetString("lbType.AccessibleDescription");
			this.lbType.AccessibleName = resources.GetString("lbType.AccessibleName");
			this.lbType.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbType.Anchor")));
			this.lbType.AutoSize = ((bool)(resources.GetObject("lbType.AutoSize")));
			this.lbType.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbType.Dock")));
			this.lbType.Enabled = ((bool)(resources.GetObject("lbType.Enabled")));
			this.lbType.Font = ((System.Drawing.Font)(resources.GetObject("lbType.Font")));
			this.lbType.Image = ((System.Drawing.Image)(resources.GetObject("lbType.Image")));
			this.lbType.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbType.ImageAlign")));
			this.lbType.ImageIndex = ((int)(resources.GetObject("lbType.ImageIndex")));
			this.lbType.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbType.ImeMode")));
			this.lbType.Location = ((System.Drawing.Point)(resources.GetObject("lbType.Location")));
			this.lbType.Name = "lbType";
			this.lbType.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbType.RightToLeft")));
			this.lbType.Size = ((System.Drawing.Size)(resources.GetObject("lbType.Size")));
			this.lbType.TabIndex = ((int)(resources.GetObject("lbType.TabIndex")));
			this.lbType.Text = resources.GetString("lbType.Text");
			this.lbType.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbType.TextAlign")));
			this.lbType.Visible = ((bool)(resources.GetObject("lbType.Visible")));
			// 
			// lbLocalC
			// 
			this.lbLocalC.AccessibleDescription = resources.GetString("lbLocalC.AccessibleDescription");
			this.lbLocalC.AccessibleName = resources.GetString("lbLocalC.AccessibleName");
			this.lbLocalC.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbLocalC.Anchor")));
			this.lbLocalC.AutoSize = ((bool)(resources.GetObject("lbLocalC.AutoSize")));
			this.lbLocalC.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbLocalC.Dock")));
			this.lbLocalC.Enabled = ((bool)(resources.GetObject("lbLocalC.Enabled")));
			this.lbLocalC.Font = ((System.Drawing.Font)(resources.GetObject("lbLocalC.Font")));
			this.lbLocalC.Image = ((System.Drawing.Image)(resources.GetObject("lbLocalC.Image")));
			this.lbLocalC.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbLocalC.ImageAlign")));
			this.lbLocalC.ImageIndex = ((int)(resources.GetObject("lbLocalC.ImageIndex")));
			this.lbLocalC.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbLocalC.ImeMode")));
			this.lbLocalC.Location = ((System.Drawing.Point)(resources.GetObject("lbLocalC.Location")));
			this.lbLocalC.Name = "lbLocalC";
			this.lbLocalC.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbLocalC.RightToLeft")));
			this.lbLocalC.Size = ((System.Drawing.Size)(resources.GetObject("lbLocalC.Size")));
			this.lbLocalC.TabIndex = ((int)(resources.GetObject("lbLocalC.TabIndex")));
			this.lbLocalC.Text = resources.GetString("lbLocalC.Text");
			this.lbLocalC.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbLocalC.TextAlign")));
			this.lbLocalC.Visible = ((bool)(resources.GetObject("lbLocalC.Visible")));
			// 
			// lbArgC
			// 
			this.lbArgC.AccessibleDescription = resources.GetString("lbArgC.AccessibleDescription");
			this.lbArgC.AccessibleName = resources.GetString("lbArgC.AccessibleName");
			this.lbArgC.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbArgC.Anchor")));
			this.lbArgC.AutoSize = ((bool)(resources.GetObject("lbArgC.AutoSize")));
			this.lbArgC.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbArgC.Dock")));
			this.lbArgC.Enabled = ((bool)(resources.GetObject("lbArgC.Enabled")));
			this.lbArgC.Font = ((System.Drawing.Font)(resources.GetObject("lbArgC.Font")));
			this.lbArgC.Image = ((System.Drawing.Image)(resources.GetObject("lbArgC.Image")));
			this.lbArgC.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbArgC.ImageAlign")));
			this.lbArgC.ImageIndex = ((int)(resources.GetObject("lbArgC.ImageIndex")));
			this.lbArgC.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbArgC.ImeMode")));
			this.lbArgC.Location = ((System.Drawing.Point)(resources.GetObject("lbArgC.Location")));
			this.lbArgC.Name = "lbArgC";
			this.lbArgC.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbArgC.RightToLeft")));
			this.lbArgC.Size = ((System.Drawing.Size)(resources.GetObject("lbArgC.Size")));
			this.lbArgC.TabIndex = ((int)(resources.GetObject("lbArgC.TabIndex")));
			this.lbArgC.Text = resources.GetString("lbArgC.Text");
			this.lbArgC.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbArgC.TextAlign")));
			this.lbArgC.Visible = ((bool)(resources.GetObject("lbArgC.Visible")));
			// 
			// lbFormat
			// 
			this.lbFormat.AccessibleDescription = resources.GetString("lbFormat.AccessibleDescription");
			this.lbFormat.AccessibleName = resources.GetString("lbFormat.AccessibleName");
			this.lbFormat.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbFormat.Anchor")));
			this.lbFormat.AutoSize = ((bool)(resources.GetObject("lbFormat.AutoSize")));
			this.lbFormat.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbFormat.Dock")));
			this.lbFormat.Enabled = ((bool)(resources.GetObject("lbFormat.Enabled")));
			this.lbFormat.Font = ((System.Drawing.Font)(resources.GetObject("lbFormat.Font")));
			this.lbFormat.Image = ((System.Drawing.Image)(resources.GetObject("lbFormat.Image")));
			this.lbFormat.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbFormat.ImageAlign")));
			this.lbFormat.ImageIndex = ((int)(resources.GetObject("lbFormat.ImageIndex")));
			this.lbFormat.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbFormat.ImeMode")));
			this.lbFormat.Location = ((System.Drawing.Point)(resources.GetObject("lbFormat.Location")));
			this.lbFormat.Name = "lbFormat";
			this.lbFormat.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbFormat.RightToLeft")));
			this.lbFormat.Size = ((System.Drawing.Size)(resources.GetObject("lbFormat.Size")));
			this.lbFormat.TabIndex = ((int)(resources.GetObject("lbFormat.TabIndex")));
			this.lbFormat.Text = resources.GetString("lbFormat.Text");
			this.lbFormat.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbFormat.TextAlign")));
			this.lbFormat.Visible = ((bool)(resources.GetObject("lbFormat.Visible")));
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
			this.bhavPanel.BackColor = System.Drawing.SystemColors.Control;
			this.bhavPanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("bhavPanel.BackgroundImage")));
			this.bhavPanel.Controls.Add(this.cbFormat);
			this.bhavPanel.Controls.Add(this.gbSpecial);
			this.bhavPanel.Controls.Add(this.pnflowcontainer);
			this.bhavPanel.Controls.Add(this.btnDel);
			this.bhavPanel.Controls.Add(this.gbMove);
			this.bhavPanel.Controls.Add(this.btnSort);
			this.bhavPanel.Controls.Add(this.btnCommit);
			this.bhavPanel.Controls.Add(this.lbFilename);
			this.bhavPanel.Controls.Add(this.tbFilename);
			this.bhavPanel.Controls.Add(this.gbInstruction);
			this.bhavPanel.Controls.Add(this.tbReserved);
			this.bhavPanel.Controls.Add(this.tbLocalC);
			this.bhavPanel.Controls.Add(this.tbFlags);
			this.bhavPanel.Controls.Add(this.tbArgC);
			this.bhavPanel.Controls.Add(this.tbType);
			this.bhavPanel.Controls.Add(this.lbReserved);
			this.bhavPanel.Controls.Add(this.lbFlags);
			this.bhavPanel.Controls.Add(this.lbType);
			this.bhavPanel.Controls.Add(this.lbLocalC);
			this.bhavPanel.Controls.Add(this.lbArgC);
			this.bhavPanel.Controls.Add(this.lbFormat);
			this.bhavPanel.Controls.Add(this.pnHeading);
			this.bhavPanel.Controls.Add(this.btnAdd);
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
			this.bhavPanel.Resize += new System.EventHandler(this.bhavPanel_Resize);
			// 
			// cbFormat
			// 
			this.cbFormat.AccessibleDescription = resources.GetString("cbFormat.AccessibleDescription");
			this.cbFormat.AccessibleName = resources.GetString("cbFormat.AccessibleName");
			this.cbFormat.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cbFormat.Anchor")));
			this.cbFormat.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cbFormat.BackgroundImage")));
			this.cbFormat.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cbFormat.Dock")));
			this.cbFormat.Enabled = ((bool)(resources.GetObject("cbFormat.Enabled")));
			this.cbFormat.Font = ((System.Drawing.Font)(resources.GetObject("cbFormat.Font")));
			this.cbFormat.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cbFormat.ImeMode")));
			this.cbFormat.IntegralHeight = ((bool)(resources.GetObject("cbFormat.IntegralHeight")));
			this.cbFormat.ItemHeight = ((int)(resources.GetObject("cbFormat.ItemHeight")));
			this.cbFormat.Items.AddRange(new object[] {
														  resources.GetString("cbFormat.Items"),
														  resources.GetString("cbFormat.Items1"),
														  resources.GetString("cbFormat.Items2"),
														  resources.GetString("cbFormat.Items3"),
														  resources.GetString("cbFormat.Items4"),
														  resources.GetString("cbFormat.Items5"),
														  resources.GetString("cbFormat.Items6"),
														  resources.GetString("cbFormat.Items7")});
			this.cbFormat.Location = ((System.Drawing.Point)(resources.GetObject("cbFormat.Location")));
			this.cbFormat.MaxDropDownItems = ((int)(resources.GetObject("cbFormat.MaxDropDownItems")));
			this.cbFormat.MaxLength = ((int)(resources.GetObject("cbFormat.MaxLength")));
			this.cbFormat.Name = "cbFormat";
			this.cbFormat.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cbFormat.RightToLeft")));
			this.cbFormat.Size = ((System.Drawing.Size)(resources.GetObject("cbFormat.Size")));
			this.cbFormat.TabIndex = ((int)(resources.GetObject("cbFormat.TabIndex")));
			this.cbFormat.Text = resources.GetString("cbFormat.Text");
			this.cbFormat.Visible = ((bool)(resources.GetObject("cbFormat.Visible")));
			this.cbFormat.Validating += new System.ComponentModel.CancelEventHandler(this.cbHex16_Validating);
			this.cbFormat.Validated += new System.EventHandler(this.cbHex16_Validated);
			this.cbFormat.TextChanged += new System.EventHandler(this.cbHex16_TextChanged);
			this.cbFormat.SelectedIndexChanged += new System.EventHandler(this.cbHex16_SelectedIndexChanged);
			this.cbFormat.Enter += new System.EventHandler(this.cbHex16_Enter);
			// 
			// gbSpecial
			// 
			this.gbSpecial.AccessibleDescription = resources.GetString("gbSpecial.AccessibleDescription");
			this.gbSpecial.AccessibleName = resources.GetString("gbSpecial.AccessibleName");
			this.gbSpecial.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("gbSpecial.Anchor")));
			this.gbSpecial.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("gbSpecial.BackgroundImage")));
			this.gbSpecial.Controls.Add(this.btnAppend);
			this.gbSpecial.Controls.Add(this.btnInsTrue);
			this.gbSpecial.Controls.Add(this.btnInsFalse);
			this.gbSpecial.Controls.Add(this.btnDelPescado);
			this.gbSpecial.Controls.Add(this.btnLinkInge);
			this.gbSpecial.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("gbSpecial.Dock")));
			this.gbSpecial.Enabled = ((bool)(resources.GetObject("gbSpecial.Enabled")));
			this.gbSpecial.Font = ((System.Drawing.Font)(resources.GetObject("gbSpecial.Font")));
			this.gbSpecial.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("gbSpecial.ImeMode")));
			this.gbSpecial.Location = ((System.Drawing.Point)(resources.GetObject("gbSpecial.Location")));
			this.gbSpecial.Name = "gbSpecial";
			this.gbSpecial.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("gbSpecial.RightToLeft")));
			this.gbSpecial.Size = ((System.Drawing.Size)(resources.GetObject("gbSpecial.Size")));
			this.gbSpecial.TabIndex = ((int)(resources.GetObject("gbSpecial.TabIndex")));
			this.gbSpecial.TabStop = false;
			this.gbSpecial.Text = resources.GetString("gbSpecial.Text");
			this.gbSpecial.Visible = ((bool)(resources.GetObject("gbSpecial.Visible")));
			// 
			// btnAppend
			// 
			this.btnAppend.AccessibleDescription = resources.GetString("btnAppend.AccessibleDescription");
			this.btnAppend.AccessibleName = resources.GetString("btnAppend.AccessibleName");
			this.btnAppend.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnAppend.Anchor")));
			this.btnAppend.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAppend.BackgroundImage")));
			this.btnAppend.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnAppend.Dock")));
			this.btnAppend.Enabled = ((bool)(resources.GetObject("btnAppend.Enabled")));
			this.btnAppend.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnAppend.FlatStyle")));
			this.btnAppend.Font = ((System.Drawing.Font)(resources.GetObject("btnAppend.Font")));
			this.btnAppend.Image = ((System.Drawing.Image)(resources.GetObject("btnAppend.Image")));
			this.btnAppend.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnAppend.ImageAlign")));
			this.btnAppend.ImageIndex = ((int)(resources.GetObject("btnAppend.ImageIndex")));
			this.btnAppend.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnAppend.ImeMode")));
			this.btnAppend.Location = ((System.Drawing.Point)(resources.GetObject("btnAppend.Location")));
			this.btnAppend.Name = "btnAppend";
			this.btnAppend.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnAppend.RightToLeft")));
			this.btnAppend.Size = ((System.Drawing.Size)(resources.GetObject("btnAppend.Size")));
			this.btnAppend.TabIndex = ((int)(resources.GetObject("btnAppend.TabIndex")));
			this.btnAppend.Text = resources.GetString("btnAppend.Text");
			this.btnAppend.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnAppend.TextAlign")));
			this.btnAppend.Visible = ((bool)(resources.GetObject("btnAppend.Visible")));
			this.btnAppend.Click += new System.EventHandler(this.btnAppend_Click);
			// 
			// btnInsTrue
			// 
			this.btnInsTrue.AccessibleDescription = resources.GetString("btnInsTrue.AccessibleDescription");
			this.btnInsTrue.AccessibleName = resources.GetString("btnInsTrue.AccessibleName");
			this.btnInsTrue.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnInsTrue.Anchor")));
			this.btnInsTrue.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnInsTrue.BackgroundImage")));
			this.btnInsTrue.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnInsTrue.Dock")));
			this.btnInsTrue.Enabled = ((bool)(resources.GetObject("btnInsTrue.Enabled")));
			this.btnInsTrue.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnInsTrue.FlatStyle")));
			this.btnInsTrue.Font = ((System.Drawing.Font)(resources.GetObject("btnInsTrue.Font")));
			this.btnInsTrue.Image = ((System.Drawing.Image)(resources.GetObject("btnInsTrue.Image")));
			this.btnInsTrue.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnInsTrue.ImageAlign")));
			this.btnInsTrue.ImageIndex = ((int)(resources.GetObject("btnInsTrue.ImageIndex")));
			this.btnInsTrue.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnInsTrue.ImeMode")));
			this.btnInsTrue.Location = ((System.Drawing.Point)(resources.GetObject("btnInsTrue.Location")));
			this.btnInsTrue.Name = "btnInsTrue";
			this.btnInsTrue.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnInsTrue.RightToLeft")));
			this.btnInsTrue.Size = ((System.Drawing.Size)(resources.GetObject("btnInsTrue.Size")));
			this.btnInsTrue.TabIndex = ((int)(resources.GetObject("btnInsTrue.TabIndex")));
			this.btnInsTrue.Text = resources.GetString("btnInsTrue.Text");
			this.btnInsTrue.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnInsTrue.TextAlign")));
			this.btnInsTrue.Visible = ((bool)(resources.GetObject("btnInsTrue.Visible")));
			this.btnInsTrue.Click += new System.EventHandler(this.btnInsVia_Click);
			// 
			// btnInsFalse
			// 
			this.btnInsFalse.AccessibleDescription = resources.GetString("btnInsFalse.AccessibleDescription");
			this.btnInsFalse.AccessibleName = resources.GetString("btnInsFalse.AccessibleName");
			this.btnInsFalse.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnInsFalse.Anchor")));
			this.btnInsFalse.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnInsFalse.BackgroundImage")));
			this.btnInsFalse.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnInsFalse.Dock")));
			this.btnInsFalse.Enabled = ((bool)(resources.GetObject("btnInsFalse.Enabled")));
			this.btnInsFalse.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnInsFalse.FlatStyle")));
			this.btnInsFalse.Font = ((System.Drawing.Font)(resources.GetObject("btnInsFalse.Font")));
			this.btnInsFalse.Image = ((System.Drawing.Image)(resources.GetObject("btnInsFalse.Image")));
			this.btnInsFalse.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnInsFalse.ImageAlign")));
			this.btnInsFalse.ImageIndex = ((int)(resources.GetObject("btnInsFalse.ImageIndex")));
			this.btnInsFalse.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnInsFalse.ImeMode")));
			this.btnInsFalse.Location = ((System.Drawing.Point)(resources.GetObject("btnInsFalse.Location")));
			this.btnInsFalse.Name = "btnInsFalse";
			this.btnInsFalse.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnInsFalse.RightToLeft")));
			this.btnInsFalse.Size = ((System.Drawing.Size)(resources.GetObject("btnInsFalse.Size")));
			this.btnInsFalse.TabIndex = ((int)(resources.GetObject("btnInsFalse.TabIndex")));
			this.btnInsFalse.Text = resources.GetString("btnInsFalse.Text");
			this.btnInsFalse.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnInsFalse.TextAlign")));
			this.btnInsFalse.Visible = ((bool)(resources.GetObject("btnInsFalse.Visible")));
			this.btnInsFalse.Click += new System.EventHandler(this.btnInsVia_Click);
			// 
			// btnDelPescado
			// 
			this.btnDelPescado.AccessibleDescription = resources.GetString("btnDelPescado.AccessibleDescription");
			this.btnDelPescado.AccessibleName = resources.GetString("btnDelPescado.AccessibleName");
			this.btnDelPescado.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnDelPescado.Anchor")));
			this.btnDelPescado.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDelPescado.BackgroundImage")));
			this.btnDelPescado.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnDelPescado.Dock")));
			this.btnDelPescado.Enabled = ((bool)(resources.GetObject("btnDelPescado.Enabled")));
			this.btnDelPescado.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnDelPescado.FlatStyle")));
			this.btnDelPescado.Font = ((System.Drawing.Font)(resources.GetObject("btnDelPescado.Font")));
			this.btnDelPescado.Image = ((System.Drawing.Image)(resources.GetObject("btnDelPescado.Image")));
			this.btnDelPescado.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnDelPescado.ImageAlign")));
			this.btnDelPescado.ImageIndex = ((int)(resources.GetObject("btnDelPescado.ImageIndex")));
			this.btnDelPescado.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnDelPescado.ImeMode")));
			this.btnDelPescado.Location = ((System.Drawing.Point)(resources.GetObject("btnDelPescado.Location")));
			this.btnDelPescado.Name = "btnDelPescado";
			this.btnDelPescado.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnDelPescado.RightToLeft")));
			this.btnDelPescado.Size = ((System.Drawing.Size)(resources.GetObject("btnDelPescado.Size")));
			this.btnDelPescado.TabIndex = ((int)(resources.GetObject("btnDelPescado.TabIndex")));
			this.btnDelPescado.Text = resources.GetString("btnDelPescado.Text");
			this.btnDelPescado.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnDelPescado.TextAlign")));
			this.btnDelPescado.Visible = ((bool)(resources.GetObject("btnDelPescado.Visible")));
			this.btnDelPescado.Click += new System.EventHandler(this.btnDelPescado_Click);
			// 
			// btnLinkInge
			// 
			this.btnLinkInge.AccessibleDescription = resources.GetString("btnLinkInge.AccessibleDescription");
			this.btnLinkInge.AccessibleName = resources.GetString("btnLinkInge.AccessibleName");
			this.btnLinkInge.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnLinkInge.Anchor")));
			this.btnLinkInge.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnLinkInge.BackgroundImage")));
			this.btnLinkInge.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnLinkInge.Dock")));
			this.btnLinkInge.Enabled = ((bool)(resources.GetObject("btnLinkInge.Enabled")));
			this.btnLinkInge.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnLinkInge.FlatStyle")));
			this.btnLinkInge.Font = ((System.Drawing.Font)(resources.GetObject("btnLinkInge.Font")));
			this.btnLinkInge.Image = ((System.Drawing.Image)(resources.GetObject("btnLinkInge.Image")));
			this.btnLinkInge.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnLinkInge.ImageAlign")));
			this.btnLinkInge.ImageIndex = ((int)(resources.GetObject("btnLinkInge.ImageIndex")));
			this.btnLinkInge.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnLinkInge.ImeMode")));
			this.btnLinkInge.Location = ((System.Drawing.Point)(resources.GetObject("btnLinkInge.Location")));
			this.btnLinkInge.Name = "btnLinkInge";
			this.btnLinkInge.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnLinkInge.RightToLeft")));
			this.btnLinkInge.Size = ((System.Drawing.Size)(resources.GetObject("btnLinkInge.Size")));
			this.btnLinkInge.TabIndex = ((int)(resources.GetObject("btnLinkInge.TabIndex")));
			this.btnLinkInge.Text = resources.GetString("btnLinkInge.Text");
			this.btnLinkInge.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnLinkInge.TextAlign")));
			this.btnLinkInge.Visible = ((bool)(resources.GetObject("btnLinkInge.Visible")));
			this.btnLinkInge.Click += new System.EventHandler(this.btnLinkInge_Click);
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
			// btnDel
			// 
			this.btnDel.AccessibleDescription = resources.GetString("btnDel.AccessibleDescription");
			this.btnDel.AccessibleName = resources.GetString("btnDel.AccessibleName");
			this.btnDel.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnDel.Anchor")));
			this.btnDel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDel.BackgroundImage")));
			this.btnDel.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnDel.Dock")));
			this.btnDel.Enabled = ((bool)(resources.GetObject("btnDel.Enabled")));
			this.btnDel.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnDel.FlatStyle")));
			this.btnDel.Font = ((System.Drawing.Font)(resources.GetObject("btnDel.Font")));
			this.btnDel.Image = ((System.Drawing.Image)(resources.GetObject("btnDel.Image")));
			this.btnDel.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnDel.ImageAlign")));
			this.btnDel.ImageIndex = ((int)(resources.GetObject("btnDel.ImageIndex")));
			this.btnDel.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnDel.ImeMode")));
			this.btnDel.Location = ((System.Drawing.Point)(resources.GetObject("btnDel.Location")));
			this.btnDel.Name = "btnDel";
			this.btnDel.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnDel.RightToLeft")));
			this.btnDel.Size = ((System.Drawing.Size)(resources.GetObject("btnDel.Size")));
			this.btnDel.TabIndex = ((int)(resources.GetObject("btnDel.TabIndex")));
			this.btnDel.Text = resources.GetString("btnDel.Text");
			this.btnDel.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnDel.TextAlign")));
			this.btnDel.Visible = ((bool)(resources.GetObject("btnDel.Visible")));
			this.btnDel.Click += new System.EventHandler(this.btnDel_Clicked);
			// 
			// gbMove
			// 
			this.gbMove.AccessibleDescription = resources.GetString("gbMove.AccessibleDescription");
			this.gbMove.AccessibleName = resources.GetString("gbMove.AccessibleName");
			this.gbMove.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("gbMove.Anchor")));
			this.gbMove.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("gbMove.BackgroundImage")));
			this.gbMove.Controls.Add(this.btnUp);
			this.gbMove.Controls.Add(this.btnDown);
			this.gbMove.Controls.Add(this.lbUpDown);
			this.gbMove.Controls.Add(this.tbLines);
			this.gbMove.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("gbMove.Dock")));
			this.gbMove.Enabled = ((bool)(resources.GetObject("gbMove.Enabled")));
			this.gbMove.Font = ((System.Drawing.Font)(resources.GetObject("gbMove.Font")));
			this.gbMove.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("gbMove.ImeMode")));
			this.gbMove.Location = ((System.Drawing.Point)(resources.GetObject("gbMove.Location")));
			this.gbMove.Name = "gbMove";
			this.gbMove.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("gbMove.RightToLeft")));
			this.gbMove.Size = ((System.Drawing.Size)(resources.GetObject("gbMove.Size")));
			this.gbMove.TabIndex = ((int)(resources.GetObject("gbMove.TabIndex")));
			this.gbMove.TabStop = false;
			this.gbMove.Text = resources.GetString("gbMove.Text");
			this.gbMove.Visible = ((bool)(resources.GetObject("gbMove.Visible")));
			// 
			// btnUp
			// 
			this.btnUp.AccessibleDescription = resources.GetString("btnUp.AccessibleDescription");
			this.btnUp.AccessibleName = resources.GetString("btnUp.AccessibleName");
			this.btnUp.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnUp.Anchor")));
			this.btnUp.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnUp.BackgroundImage")));
			this.btnUp.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnUp.Dock")));
			this.btnUp.Enabled = ((bool)(resources.GetObject("btnUp.Enabled")));
			this.btnUp.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnUp.FlatStyle")));
			this.btnUp.Font = ((System.Drawing.Font)(resources.GetObject("btnUp.Font")));
			this.btnUp.Image = ((System.Drawing.Image)(resources.GetObject("btnUp.Image")));
			this.btnUp.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnUp.ImageAlign")));
			this.btnUp.ImageIndex = ((int)(resources.GetObject("btnUp.ImageIndex")));
			this.btnUp.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnUp.ImeMode")));
			this.btnUp.Location = ((System.Drawing.Point)(resources.GetObject("btnUp.Location")));
			this.btnUp.Name = "btnUp";
			this.btnUp.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnUp.RightToLeft")));
			this.btnUp.Size = ((System.Drawing.Size)(resources.GetObject("btnUp.Size")));
			this.btnUp.TabIndex = ((int)(resources.GetObject("btnUp.TabIndex")));
			this.btnUp.Text = resources.GetString("btnUp.Text");
			this.btnUp.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnUp.TextAlign")));
			this.btnUp.Visible = ((bool)(resources.GetObject("btnUp.Visible")));
			this.btnUp.Click += new System.EventHandler(this.btnMove_Clicked);
			// 
			// btnDown
			// 
			this.btnDown.AccessibleDescription = resources.GetString("btnDown.AccessibleDescription");
			this.btnDown.AccessibleName = resources.GetString("btnDown.AccessibleName");
			this.btnDown.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnDown.Anchor")));
			this.btnDown.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDown.BackgroundImage")));
			this.btnDown.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnDown.Dock")));
			this.btnDown.Enabled = ((bool)(resources.GetObject("btnDown.Enabled")));
			this.btnDown.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnDown.FlatStyle")));
			this.btnDown.Font = ((System.Drawing.Font)(resources.GetObject("btnDown.Font")));
			this.btnDown.Image = ((System.Drawing.Image)(resources.GetObject("btnDown.Image")));
			this.btnDown.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnDown.ImageAlign")));
			this.btnDown.ImageIndex = ((int)(resources.GetObject("btnDown.ImageIndex")));
			this.btnDown.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnDown.ImeMode")));
			this.btnDown.Location = ((System.Drawing.Point)(resources.GetObject("btnDown.Location")));
			this.btnDown.Name = "btnDown";
			this.btnDown.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnDown.RightToLeft")));
			this.btnDown.Size = ((System.Drawing.Size)(resources.GetObject("btnDown.Size")));
			this.btnDown.TabIndex = ((int)(resources.GetObject("btnDown.TabIndex")));
			this.btnDown.Text = resources.GetString("btnDown.Text");
			this.btnDown.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnDown.TextAlign")));
			this.btnDown.Visible = ((bool)(resources.GetObject("btnDown.Visible")));
			this.btnDown.Click += new System.EventHandler(this.btnMove_Clicked);
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
			this.tbLines.Validating += new System.ComponentModel.CancelEventHandler(this.dec16_Validating);
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
			this.tbFlags.Validating += new System.ComponentModel.CancelEventHandler(this.hex16_Validating);
			this.tbFlags.Validated += new System.EventHandler(this.hex16_Validated);
			this.tbFlags.TextChanged += new System.EventHandler(this.hex16_TextChanged);
			// 
			// btnAdd
			// 
			this.btnAdd.AccessibleDescription = resources.GetString("btnAdd.AccessibleDescription");
			this.btnAdd.AccessibleName = resources.GetString("btnAdd.AccessibleName");
			this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnAdd.Anchor")));
			this.btnAdd.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAdd.BackgroundImage")));
			this.btnAdd.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnAdd.Dock")));
			this.btnAdd.Enabled = ((bool)(resources.GetObject("btnAdd.Enabled")));
			this.btnAdd.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnAdd.FlatStyle")));
			this.btnAdd.Font = ((System.Drawing.Font)(resources.GetObject("btnAdd.Font")));
			this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
			this.btnAdd.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnAdd.ImageAlign")));
			this.btnAdd.ImageIndex = ((int)(resources.GetObject("btnAdd.ImageIndex")));
			this.btnAdd.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnAdd.ImeMode")));
			this.btnAdd.Location = ((System.Drawing.Point)(resources.GetObject("btnAdd.Location")));
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnAdd.RightToLeft")));
			this.btnAdd.Size = ((System.Drawing.Size)(resources.GetObject("btnAdd.Size")));
			this.btnAdd.TabIndex = ((int)(resources.GetObject("btnAdd.TabIndex")));
			this.btnAdd.Text = resources.GetString("btnAdd.Text");
			this.btnAdd.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnAdd.TextAlign")));
			this.btnAdd.Visible = ((bool)(resources.GetObject("btnAdd.Visible")));
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Clicked);
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
			this.gbSpecial.ResumeLayout(false);
			this.gbMove.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private void bhavPanel_Resize(object sender, System.EventArgs e)
		{
			this.tbFilename.Width = this.lbFormat.Left - (this.tbFilename.Left + 4);
		}


		private void pnflowcontainer_SelectedInstChanged(object sender, System.EventArgs e)
		{
			int index = pnflowcontainer.SelectedIndex;
			if (index < 0 || index >= wrapper.Instructions.Count)
			{
				currentInst = null;
				origInst = null;
			}
			else
			{
				currentInst = wrapper.Instructions[pnflowcontainer.SelectedIndex];
				origInst = currentInst.Clone();
			}
			UpdateInstPanel();
			this.btnCancel.Enabled = false;
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


		private void btnCommit_Clicked(object sender, System.EventArgs e)
		{
			try 
			{
				wrapper.SynchronizeUserData();
				btnCommit.Enabled = wrapper.Changed;
				pnflowcontainer_SelectedInstChanged(null, null);
			} 
			catch (Exception ex) 
			{
				Helper.ExceptionMessage(Localization.Manager.GetString("errwritingfile"), ex);
			}			
		}


		private void btnCancel_Clicked(object sender, System.EventArgs e)
		{
			wrapper.Instructions[pnflowcontainer.SelectedIndex] = origInst.Clone();
			pnflowcontainer_SelectedInstChanged(null, null);
		}


		private void llopenbhav_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			// We want to instantiate the current UI but with the Global BHAV linked from the current instruction
			Bhav b = Instruction.LoadGlobalBHAV(currentInst.OpCode);
			BhavForm ui = (BhavForm)b.UIHandler;
			ui.tbInst_Instruction.Width = ui.gbInstruction.Width - (2 * ui.tbInst_Instruction.Location.X);
			ui.Tag = "Popup"; // tells the SetReadOnly function it's in a popup - so everything locked down
			ui.Text = "Global BHAV: " + currentInst.ToString();
			b.RefreshUI();
			ui.Show();
		}

		private void btnOpCode_Clicked(object sender, System.EventArgs e)
		{
			int opcode = SimPe.Plugin.WrapperFactory.BhavWizardForm.Execute(wrapper, bhavPanel.Parent, BhavOpCodeWiz.Flags.All);

			if (opcode != -1 && opcode != currentInst.OpCode)
			{
				internalchg = true;
				currentInst.OpCode = (ushort)opcode;
				tbInst_OpCode.Text = "0x"+Helper.HexString((ushort)opcode);
				internalchg = false;
			}
		}

		private void btnOperandWiz_Clicked(object sender, System.EventArgs e)
		{
			internalchg = true;
			if ((new BhavOperandWiz()).Execute(currentInst) != null)
				UpdateInstPanel();
			internalchg = false;
		}
		

		private void tbFilename_TextChanged(object sender, System.EventArgs e)
		{
			wrapper.FileName = tbFilename.Text;
		}

		private void tbFilename_Validated(object sender, System.EventArgs e)
		{
			tbFilename.SelectAll();
		}


		private void cbHex16_Enter(object sender, System.EventArgs e)
		{
			((ComboBox)sender).SelectAll();
		}

		private void cbHex16_TextChanged(object sender, System.EventArgs ev)
		{
			if (internalchg) return;
			if (!cbHex16_IsValid(sender)) return;
			if (((ComboBox)sender).Items.IndexOf(((ComboBox)sender).Text) != -1) return;

			ushort val = Convert.ToUInt16(((ComboBox)sender).Text, 16);
			internalchg = true;
			switch (alHex16cb.IndexOf(sender))
			{
				case 0: currentInst.Target1 = val; break;
				case 1: currentInst.Target2 = val; break;
				case 2: wrapper.Header.Format = val; break;
			}
			internalchg = false;
		}

		private void cbHex16_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (cbHex16_IsValid(sender)) return;

			e.Cancel = true;

			int i = alHex16cb.IndexOf(sender);
			if (i < 0)
				throw new Exception("cbHex16_Validating not applicable to control " + sender.ToString());

			ushort val = 0;
			switch (i)
			{
				case 0: val = origInst.Target1; currentInst.Target1 = val; break;
				case 1: val = origInst.Target2; currentInst.Target2 = val; break;
				case 2: val = wrapper.Header.Format; break;
			}

			bool origstate = internalchg;
			internalchg = true;
			if (i < 2 && val >= 0xfffc && val <= 0xfffe)
			{
				((ComboBox)sender).SelectedIndex = val - 0xfffc;
			}
			else if (i == 2 && val >= 0x8000 && val <= 0x8007)
			{
				((ComboBox)sender).SelectedIndex = val - 0x8000;
			}
			else
			{
				((ComboBox)sender).SelectedIndex = -1;
				((ComboBox)sender).Text = "0x" + Helper.HexString(val);
			}
			internalchg = origstate;
			((ComboBox)sender).SelectAll();
		}

		private void cbHex16_Validated(object sender, System.EventArgs e)
		{
			int i = alHex16cb.IndexOf(sender);
			if (i < 0)
				throw new Exception("cbHex16_Validated not applicable to control " + sender.ToString());
			if (((ComboBox)sender).Items.IndexOf(((ComboBox)sender).Text) != -1) return;

			ushort val = Convert.ToUInt16(((ComboBox)sender).Text, 16);

			bool origstate = internalchg;
			internalchg = true;
			if (i < 2 && val >= 0xfffc && val <= 0xfffe)
			{
				((ComboBox)sender).SelectedIndex = val - 0xfffc;
			}
			else if (i == 2 && val >= 0x8000 && val <= 0x8007)
			{
				((ComboBox)sender).SelectedIndex = val - 0x8000;
			}
			else
			{
				((ComboBox)sender).SelectedIndex = -1;
				((ComboBox)sender).Text = "0x" + Helper.HexString(val);
			}
			internalchg = origstate;
			((ComboBox)sender).Select(0, 0);
		}

		private void cbHex16_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (internalchg) return;

			int i = alHex16cb.IndexOf(sender);
			if (i < 0)
				throw new Exception("cbHex16_SelectedIndexChanged not applicable to control " + sender.ToString());
			if (((ComboBox)sender).SelectedIndex == -1) return;

			ushort val = (ushort)((ComboBox)alHex16cb[i]).SelectedIndex;
			((ComboBox)sender).SelectAll();

			internalchg = true;
			if (i < 2)
			{
				val += 0xFFFC;
				if (i == 0) currentInst.Target1 = val;
				else        currentInst.Target2 = val;
			}
			else
			{
				val += 0x8000;
				wrapper.Header.Format = val;
			}
			internalchg = false;
		}


		private void dec8_TextChanged(object sender, System.EventArgs ev)
		{
			if (internalchg) return;
			if (!dec8_IsValid(sender)) return;

			byte val = Convert.ToByte(((TextBox)sender).Text);
			internalchg = true;
			switch (alDec8.IndexOf(sender))
			{
				case 0: wrapper.Header.ArgumentCount = val; break;
				case 1: wrapper.Header.LocalVarCount = val; break;
			}
			internalchg = false;
		}

		private void dec8_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (dec8_IsValid(sender)) return;

			e.Cancel = true;

			internalchg = true;
			byte val = 0;
			switch (alDec8.IndexOf(sender))
			{
				case 0: val = wrapper.Header.ArgumentCount; break;
				case 1: val = wrapper.Header.LocalVarCount; break;
			}

			((TextBox)sender).Text = val.ToString();
			((TextBox)sender).SelectAll();
			internalchg = false;
		}

		private void dec8_Validated(object sender, System.EventArgs e)
		{
			((TextBox)sender).SelectAll();
		}


		private void dec16_TextChanged(object sender, System.EventArgs ev)
		{
			if (internalchg) return;
			if (!dec16_IsValid(sender)) return;

			byte[] ops = ShortToOps(Convert.ToInt16(((TextBox)sender).Text));
			internalchg = true;
			switch (alDec16.IndexOf(sender))
			{
				case 0:
					currentInst.Operands[0] = ops[0];
					currentInst.Operands[1] = ops[1];
					this.tbInst_Op0.Text = Helper.HexString(currentInst.Operands[0]);
					this.tbInst_Op1.Text = Helper.HexString(currentInst.Operands[1]);
					break;
				case 1:
					currentInst.Operands[2] = ops[0];
					currentInst.Operands[3] = ops[1];
					this.tbInst_Op2.Text = Helper.HexString(currentInst.Operands[2]);
					this.tbInst_Op3.Text = Helper.HexString(currentInst.Operands[3]);
					break;
			}
			internalchg = false;
		}

		private void dec16_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (dec16_IsValid(sender)) return;

			e.Cancel = true;

			internalchg = true;
			short val = 0;
			switch (alDec16.IndexOf(sender))
			{
				case 0:
					currentInst.Operands[0] = origInst.Operands[0];
					currentInst.Operands[1] = origInst.Operands[1];
					this.tbInst_Op0.Text = Helper.HexString(currentInst.Operands[0]);
					this.tbInst_Op1.Text = Helper.HexString(currentInst.Operands[1]);
					val = OpsToShort(origInst.Operands[0], origInst.Operands[1]);
					break;
				case 1:
					currentInst.Operands[2] = origInst.Operands[2];
					currentInst.Operands[3] = origInst.Operands[3];
					this.tbInst_Op2.Text = Helper.HexString(currentInst.Operands[2]);
					this.tbInst_Op3.Text = Helper.HexString(currentInst.Operands[3]);
					val = OpsToShort(origInst.Operands[2], origInst.Operands[3]);
					break;
				case 2: // Move
					val = 1;
					break;
			}

			((TextBox)sender).Text = val.ToString();
			((TextBox)sender).SelectAll();
			internalchg = false;
		}

		private void dec16_Validated(object sender, System.EventArgs e)
		{
			((TextBox)sender).SelectAll();
		}


		private void hex8_TextChanged(object sender, System.EventArgs ev)
		{
			if (internalchg) return;
			if (!hex8_IsValid(sender)) return;


			byte val = Convert.ToByte(((TextBox)sender).Text, 16);

			internalchg = true;
			int i = alHex8.IndexOf(sender);
			if (i < 8)
			{
				currentInst.Operands[i] = val;
				if (i < 2)
					this.tbInst_Op01_dec.Text = OpsToShort(currentInst.Operands[0], currentInst.Operands[1]).ToString();
				else if (i < 4)
					this.tbInst_Op23_dec.Text = OpsToShort(currentInst.Operands[2], currentInst.Operands[3]).ToString();
			}
			else
			{
				if (i < 16)
					currentInst.Reserved1[i-8] = val;
				else switch(i)
					 {
						 case 16: currentInst.Reserved0 = val; break;
						 case 17: wrapper.Header.Type = val; break;
					 }
			}
			internalchg = false;
		}

		private void hex8_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (hex8_IsValid(sender)) return;

			e.Cancel = true;

			byte val = 0;
			int i = alHex8.IndexOf(sender);
			if (i < 8)
			{
				currentInst.Operands[i] = val = origInst.Operands[i];
				if (i < 2)
					this.tbInst_Op01_dec.Text = OpsToShort(currentInst.Operands[0], currentInst.Operands[1]).ToString();
				else if (i < 4)
					this.tbInst_Op23_dec.Text = OpsToShort(currentInst.Operands[2], currentInst.Operands[3]).ToString();
			}
			else
			{
				if (i < 16)
				{
					currentInst.Reserved1[i-8] = val = origInst.Reserved1[i-8];
				}
				else switch(i)
					{
						case 16: val = origInst.Reserved0; currentInst.Reserved0 = val; break;
						case 17: val = wrapper.Header.Type; break;
					}
			}

			internalchg = true;
			((TextBox)sender).Text = ((i >= 16) ? "0x" : "") + Helper.HexString(val);
			((TextBox)sender).SelectAll();
			internalchg = false;
		}

		private void hex8_Validated(object sender, System.EventArgs e)
		{
			bool origstate = internalchg;
			internalchg = true;
			((TextBox)sender).Text = ((alHex8.IndexOf(sender) >= 16) ? "0x" : "") + Helper.HexString(Convert.ToByte(((TextBox)sender).Text, 16));
			((TextBox)sender).SelectAll();
			internalchg = origstate;
		}


		private void hex16_TextChanged(object sender, System.EventArgs ev)
		{
			if (internalchg) return;
			if (!hex16_IsValid(sender)) return;

			ushort val = Convert.ToUInt16(((TextBox)sender).Text, 16);
			internalchg = true;
			switch (alHex16.IndexOf(sender))
			{
				case 0: wrapper.Header.Flags = val; break;
				case 1: wrapper.Header.Zero = val; break;
				case 2:
					currentInst.OpCode = val;
					this.btnOperandWiz.Enabled = BhavOperandWiz.Available(currentInst);
					break;
			}
			internalchg = false;
		}

		private void hex16_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (hex16_IsValid(sender)) return;

			e.Cancel = true;

			internalchg = true;
			ushort val = 0;
			switch (alHex16.IndexOf(sender))
			{
				case 0: val = wrapper.Header.Flags; break;
				case 1: val = wrapper.Header.Zero; break;
				case 2: currentInst.OpCode = val = origInst.OpCode; break;
			}

			((TextBox)sender).Text = "0x" + Helper.HexString(val);
			((TextBox)sender).SelectAll();
			internalchg = false;
		}

		private void hex16_Validated(object sender, System.EventArgs e)
		{
			bool origstate = internalchg;
			internalchg = true;
			((TextBox)sender).Text = "0x" + Helper.HexString(Convert.ToUInt16(((TextBox)sender).Text, 16));
			((TextBox)sender).SelectAll();
			internalchg = origstate;
		}


		private void btnSort_Clicked(object sender, System.EventArgs e)
		{
			this.pnflowcontainer.Sort();
		}

		private void btnMove_Clicked(object sender, System.EventArgs e)
		{
			int mv;
			try { mv = Convert.ToInt32(tbLines.Text); }
			catch (Exception) { return; }
			if (sender == this.btnUp)
				this.pnflowcontainer.MoveInst(mv * -1);
			else
				this.pnflowcontainer.MoveInst(mv);
		}

		private void btnAdd_Clicked(object sender, System.EventArgs e)
		{
			this.pnflowcontainer.Add(BhavUIAddType.Default);
		}

		private void btnDel_Clicked(object sender, System.EventArgs e)
		{
			this.pnflowcontainer.Delete(BhavUIDeleteType.Default);
		}


		private void btnInsVia_Click(object sender, System.EventArgs e)
		{
			this.pnflowcontainer.Add( (sender == this.btnInsTrue) ? BhavUIAddType.ViaTrue : BhavUIAddType.ViaFalse );
		}

		private void btnDelPescado_Click(object sender, System.EventArgs e)
		{
			this.pnflowcontainer.Delete(BhavUIDeleteType.Pescado);
		}

		private void btnLinkInge_Click(object sender, System.EventArgs e)
		{
			this.pnflowcontainer.Relink();
		}

		private void btnAppend_Click(object sender, System.EventArgs e)
		{
			int opcode = SimPe.Plugin.WrapperFactory.BhavWizardForm.Execute(wrapper, bhavPanel.Parent, BhavOpCodeWiz.Flags.Locals);

			if (opcode == -1) return;
			this.pnflowcontainer.Append((uint)opcode);
		}

	}
}
