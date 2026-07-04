/***************************************************************************
 *   Copyright (C) 2005 by Peter L Jones                                   *
 *   pljones@users.sf.net                                                  *
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
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using SimPe.PackedFiles.Wrapper;

namespace pjse.BhavOperandWizards.Wiz0x0024
{
	/// <summary>
	/// Summary description for BhavInstruction.
	/// </summary>
    internal class UI : System.Windows.Forms.Form, iBhavOperandWizForm
	{
		#region Form variables

		internal System.Windows.Forms.Panel pnWiz0x0024;
		private System.Windows.Forms.ComboBox cbType;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label lbType;
		private System.Windows.Forms.Label lbMessage;
		private System.Windows.Forms.Label lbTitle;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox cbScope;
		private System.Windows.Forms.Label lbIconType;
		private System.Windows.Forms.CheckBox cbBlockBHAV;
        private System.Windows.Forms.CheckBox cbBlockSim;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.CheckBox cbUTMessage;
		private System.Windows.Forms.CheckBox cbUTButton1;
		private System.Windows.Forms.CheckBox cbUTButton2;
		private System.Windows.Forms.CheckBox cbUTButton3;
		private System.Windows.Forms.CheckBox cbUTTitle;
		private System.Windows.Forms.ComboBox cbIconType;
		private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbIconID;
		private System.Windows.Forms.Button btnStrIcon;
		private System.Windows.Forms.Panel pnTNS;
		private System.Windows.Forms.TextBox tbPriority;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox tbTimeout;
		private System.Windows.Forms.Label lbTnsStyle;
		private System.Windows.Forms.ComboBox cbTnsStyle;
		private System.Windows.Forms.Panel pnTempVar;
		private System.Windows.Forms.Label lbTempVar;
		private System.Windows.Forms.Panel pnLocalVar;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.ComboBox cbTempVar;
		private System.Windows.Forms.ComboBox cbTVMessage;
		private System.Windows.Forms.ComboBox cbTVButton1;
		private System.Windows.Forms.ComboBox cbTVButton2;
		private System.Windows.Forms.ComboBox cbTVButton3;
		private System.Windows.Forms.ComboBox cbTVTitle;
		private System.Windows.Forms.TextBox tbLocalVar;
		private System.Windows.Forms.TextBox tbMessage;
		private System.Windows.Forms.TextBox tbButton1;
		private System.Windows.Forms.TextBox tbButton2;
		private System.Windows.Forms.TextBox tbButton3;
		private System.Windows.Forms.TextBox tbTitle;
		private System.Windows.Forms.TextBox tbStrMessage;
		private System.Windows.Forms.TextBox tbStrButton1;
		private System.Windows.Forms.TextBox tbStrButton2;
		private System.Windows.Forms.TextBox tbStrButton3;
		private System.Windows.Forms.TextBox tbStrTitle;
		private System.Windows.Forms.Label lbButton3;
		private System.Windows.Forms.Label lbButton2;
		private System.Windows.Forms.Label lbButton1;
        private Button btnDefTitle;
        private Button btnDefButton3;
        private Button btnDefButton2;
        private Button btnDefButton1;
        private Button btnDefMessage;
        private Button btnStrTitle;
        private Button btnStrButton3;
        private Button btnStrButton2;
        private Button btnStrButton1;
        private Button btnStrMessage;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion

		public UI()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//

			cbType.Items.Clear();
			cbType.Items.AddRange(BhavWiz.readStr(GS.BhavStr.Dialog).ToArray());

			if (typeDescriptions == null)
				typeDescriptions = BhavWiz.readStr(GS.BhavStr.DialogDesc);

			cbTnsStyle.Items.Clear();
			cbTnsStyle.Items.AddRange(BhavWiz.readStr(GS.BhavStr.TnsStyle).ToArray());

			cbIconType.Items.Clear();
			cbIconType.Items.AddRange(BhavWiz.readStr(GS.BhavStr.DialogIcon).ToArray());

			Button[] b = { btnStrMessage ,btnStrButton1 ,btnStrButton2 ,btnStrButton3 ,btnStrTitle ,btnStrIcon ,};
			alStrBtn = new ArrayList(b);

			Button[] bd = { btnDefMessage ,btnDefButton1 ,btnDefButton2 ,btnDefButton3 ,btnDefTitle ,};
			alDefBtn = new ArrayList(bd);

			TextBox[] t = { tbStrMessage ,tbStrButton1 ,tbStrButton2 ,tbStrButton3 ,tbStrTitle ,};
			alTextBox = new ArrayList(t);

			CheckBox[] c = { cbUTMessage ,cbUTButton1 ,cbUTButton2 ,cbUTButton3 ,cbUTTitle ,};
			alCBUseTemp = new ArrayList(c);

			ComboBox[] ct = { cbTVMessage ,cbTVButton1 ,cbTVButton2 ,cbTVButton3 ,cbTVTitle ,};
			alCBTempVar = new ArrayList(ct);

			TextBox[] tb8 = { tbPriority ,tbTimeout ,tbLocalVar ,tbIconID ,};
			alHex8 = new ArrayList(tb8);

			TextBox[] tb16 = { tbMessage ,tbButton1 ,tbButton2 ,tbButton3 ,tbTitle ,};
			alHex16 = new ArrayList(tb16);
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

			inst = null;
		}


		private static List<String> typeDescriptions = null;

		private Instruction inst = null;
		private ArrayList alStrBtn = null;
		private ArrayList alDefBtn = null;
		private ArrayList alTextBox = null;
		private ArrayList alCBUseTemp = null;
		private ArrayList alCBTempVar = null;
		private ArrayList alHex8 = null;
		private ArrayList alHex16 = null;

		byte dialog   = 0;
		bool nowait   = false;
		byte iconType = 0;
		byte iconID   = 0;
		byte tempVar  = 0;
		bool noblock  = false;
		byte tnsStyle = 0;
		byte priority = 0;
		byte timeout  = 0;
		byte localVar = 0;
		Scope scope   = Scope.Private;
		ushort[] messages = { 0, 0, 0, 0, 0 }; // Message, Yes, No, Cancel, Title
		bool[] useTemp = { false, false, false, false, false }; // Message, Yes, No, Cancel, Title
		bool[] states = { false, false, false, false, false }; // message, yes, no, cancel, title

		bool internalchg = false;

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


		private void setType(int newType)
		{
			internalchg = true;

			dialog = (byte)newType;

			if (dialog != cbType.SelectedIndex)
				cbType.SelectedIndex = (cbType.Items.Count > dialog) ? dialog : -1;

            this.lbType.Text = typeDescriptions.Count > dialog ? typeDescriptions[dialog] : "";

			bool tvState = false;
			bool tnsState = false;
            bool lvState = false;
            bool gtState = false;

			states[0] = states[1] = states[2] = states[3] = states[4] = false; // forget everything...
			switch(dialog)
			{
				case 0x00: case 0x03: case 0x04:
					states[0] = states[1] = states[4] = true; // message, button 1, title
					break;
				case 0x02:
					states[0] = states[1] = states[2] = states[4] = true; // message, button 1, button 2, title
					tvState = states[3] = true; // button 3
					break;
				case 0x08: case 0x0a: // TNS, TNS modify
					tnsState = tvState = states[0] = true; // message
					break;
				case 0x09: // TNS stop
					tvState = true;
					break;
				case 0x0e:
					states[0] = states[1] = states[2] = states[4] = true; // message, button 1, button 2, title
					lvState = true;
					break;
				case 0x0f:
					states[1] = states[2] = true; // button 1, button 2
					states[0] = states[3] = states[4] = false; // msg, btn3, title
					break;
				case 0x13:
					states[1] = states[2] = states[4] = true; // button 1, button 2, title
					break;
				case 0x0b: case 0x0c: case 0x0d:
				case 0x10: case 0x11: case 0x12:
				case 0x14: case 0x15:
                    break;
                    case 0x39: // Game Tip - CJH
                    gtState = states[0] = true;
                    break;
				case 0x16: case 0x19:
					states[0] = states[4] = true; // message, title
					break;
                case 0x1c: // TNS Append
                    tvState = states[0] = true; // message
                    break;
				default:
					states[0] = states[1] = states[2] = states[4] = true; // message, button 1, button 2, title
					break;
			}

			this.pnTempVar.Visible  = tvState;
			this.pnTNS.Visible      = tnsState;
            this.pnLocalVar.Visible = lvState;
            this.tbStrMessage.Visible = !gtState;
            this.label3.Visible = !gtState;
            this.label4.Visible = !gtState;
            this.lbIconType.Visible = !gtState;
            this.cbUTMessage.Visible = !gtState;
            this.cbScope.Visible = !gtState;
            this.cbBlockSim.Visible = !gtState;
            this.cbBlockBHAV.Visible = !gtState;
            this.cbIconType.Visible = !gtState;
            this.btnStrMessage.Visible = !gtState;
            
			internalchg = false;

			// Make the display match the help text
            for (int i = 0; i < states.Length; i++)
                setString(i, messages[i]);

            if (!gtState) 
                this.lbMessage.Text = "Message";
            else
                this.lbMessage.Text = "GameTip";
		}

		private void setTnsStyle(int newStyle)
		{
			internalchg = true;

			tnsStyle = (byte)newStyle;

			if (cbTnsStyle.Items.Count != tnsStyle)
				cbTnsStyle.SelectedIndex = (tnsStyle >= 0 && tnsStyle < cbTnsStyle.Items.Count) ? tnsStyle : -1;

			internalchg = false;
		}

		private void setScope(int newScope)
		{
			internalchg = true;

			scope = (Scope)newScope;

			if (cbScope.SelectedIndex != newScope)
				cbScope.SelectedIndex = (newScope >= 0 && newScope < cbScope.Items.Count) ? newScope : -1;

			for(int i = 0; i < messages.Length; i++)
				setString(i, messages[i]);

			internalchg = false;
		}

		private void setIconType(int newType)
		{
			internalchg = true;

			iconType = (byte)newType;

			if (cbIconType.SelectedIndex != iconType)
                cbIconType.SelectedIndex = (iconType >= 0  && iconType < cbIconType.Items.Count) ? iconType : -1;
			tbIconID.Enabled = (iconType == 3);
			btnStrIcon.Enabled = (iconType == 4);

			internalchg = false;
		}

		private void setTempVar(int newTempVar)
		{
			internalchg = true;

			tempVar = (byte)newTempVar;
            if (cbTempVar.SelectedIndex != tempVar)
    			cbTempVar.SelectedIndex = (tempVar >= 0 && tempVar < cbTempVar.Items.Count) ? tempVar : -1;

			internalchg = false;
		}

		private void setBlockBHAV(bool newFlag)
		{
			internalchg = true;

			nowait = !newFlag;
			this.cbBlockBHAV.Checked = newFlag;

			internalchg = false;
		}

		private void setBlockSim(bool newFlag)
		{
			internalchg = true;

			noblock = !newFlag;
			this.cbBlockSim.Checked = newFlag;

			internalchg = false;
		}

		private void setIconID(int newIconID)
		{
			iconID = (byte)newIconID;

			if (internalchg) return;
			internalchg = true;

			this.tbIconID.Text = "0x" + SimPe.Helper.HexString((byte)newIconID);

			internalchg = false;
		}

		private void setString(int which, int strnum)
		{
			messages[which] = (ushort)strnum;

			if (!states[which])
			{
				internalchg = true;
				((ComboBox)alCBTempVar[which]).SelectedIndex = -1;
				((TextBox)alHex16[which]).Text = "";
				internalchg = false;

				((TextBox)alTextBox[which]).Text = "";

				((ComboBox)this.alCBTempVar[which]).Enabled =
					((CheckBox)this.alCBUseTemp[which]).Enabled =
					((TextBox)alHex16[which]).Enabled =
					((Button)alStrBtn[which]).Enabled =
					((Button)alDefBtn[which]).Enabled =
					((TextBox)alTextBox[which]).Enabled =
					false;

				return;
			}

			((CheckBox)this.alCBUseTemp[which]).Enabled = true;

			if (useTemp[which])
			{
				ComboBox c = (ComboBox)alCBTempVar[which];
				internalchg = true;
				c.SelectedIndex = c.Items.Count > strnum ? strnum : -1;
				((TextBox)alHex16[which]).Text = "";
				internalchg = false;

				((TextBox)alTextBox[which]).Text = "";

				((CheckBox)this.alCBUseTemp[which]).Checked =
					((ComboBox)this.alCBTempVar[which]).Enabled = true;
				((TextBox)alHex16[which]).Enabled =
					((Button)alStrBtn[which]).Enabled =
					((Button)alDefBtn[which]).Enabled =
					((TextBox)alTextBox[which]).Enabled =
					false;
			}
			else
			{
				if (!internalchg)
				{
					internalchg = true;
					((ComboBox)this.alCBTempVar[which]).SelectedIndex = -1;
					((TextBox)alHex16[which]).Text = "0x" + SimPe.Helper.HexString((ushort)strnum);
					internalchg = false;
				}

				((TextBox)alTextBox[which]).Text = (strnum <= 0)
                    ? "[" + pjse.Localization.GetString("none") + "]"
					: ((BhavWiz)inst).readStr(scope, GS.GlobalStr.DialogString, (ushort)(strnum - 1), -1, pjse.Detail.ErrorNames)
					;

				((CheckBox)this.alCBUseTemp[which]).Checked =
					((ComboBox)this.alCBTempVar[which]).Enabled = false;
				((TextBox)alHex16[which]).Enabled =
					((Button)alStrBtn[which]).Enabled =
					((TextBox)alTextBox[which]).Enabled =
					true;
				((Button)alDefBtn[which]).Enabled = (strnum != 0);
			}
		}

		private void setUseTemp(int which, bool newFlag)
		{
			useTemp[which] = newFlag;
			setString(which, messages[which]);
		}

		private void setPriority(int newPriority)
		{
			priority = (byte)newPriority;

			if (internalchg) return;
			internalchg = true;

			this.tbPriority.Text = "0x" + SimPe.Helper.HexString((byte)newPriority);

			internalchg = false;
		}

		private void setTimeout(int newTimeout)
		{
			timeout = (byte)newTimeout;

			if (internalchg) return;
			internalchg = true;

			this.tbTimeout.Text = "0x" + SimPe.Helper.HexString((byte)newTimeout);

			internalchg = false;
		}

		private void setLocalVar(int newLocalVar)
		{
			localVar = (byte)newLocalVar;

			if (internalchg) return;
			internalchg = true;

			this.tbLocalVar.Text = "0x" + SimPe.Helper.HexString((byte)newLocalVar);

			internalchg = false;
		}


		private void doStrChooser(int which)
		{
			pjse.FileTable.Entry[] items = pjse.FileTable.GFT[(uint)SimPe.Data.MetaData.STRING_FILE, inst.Parent.GroupForScope(scope), (uint)GS.GlobalStr.DialogString];

            if (items == null || items.Length == 0)
            {
                MessageBox.Show(pjse.Localization.GetString("bow_noStrings")
                    + " (" + pjse.Localization.GetString(scope.ToString())  + ")");
                return; // eek!
            }

			SimPe.PackedFiles.Wrapper.StrWrapper str = new StrWrapper();
			str.ProcessData(items[0].PFD, items[0].Package);

			int i = (new StrChooser()).Strnum(str);
			if (i >= 0)
			{
				if (messages.Length > which)
				{
					setString(which, i + 1);
				}
				else
				{
					switch(which)
					{
						case 5: setIconID(i + 1); break;
					}
				}
			}
		}


        #region iBhavOperandWizForm
        public Panel WizPanel { get { return this.pnWiz0x0024; } }

        public void Execute(Instruction inst)
		{
			this.inst = inst;

			wrappedByteArray ops1 = inst.Operands;
			wrappedByteArray ops2 = inst.Reserved1;

			setType(ops1[5]);

			setTnsStyle(ops2[4]);

			if      ((ops2[0] & 0x01) != 0) setScope((int)Scope.SemiGlobal);
			else if ((ops2[0] & 0x40) != 0) setScope((int)Scope.Global);
			else                            setScope((int)Scope.Private);

			setIconID(ops1[0x01]);

			if (inst.NodeVersion == 0)
			{
				setString(0, ops1[2]);	// message
				setString(3, ops1[0]);	// cancel
			}
			else
			{
				setString(0, BhavWiz.ToShort(ops2[5], ops2[6]));	// message
				setString(3, BhavWiz.ToShort(ops1[0], ops1[2]));	// cancel
			}
			setString(1, ops1[3]); // Yes
			setString(2, ops1[4]); // No
			setString(4, ops1[6]); // Title

			setBlockBHAV((ops1[7] & 0x01) == 0);
			setIconType((ops1[7] >> 1) & 0x07);
			setTempVar((ops1[7] >> 4) & 0x07);
			setBlockSim((ops1[7] & 0x80) == 0);

			setUseTemp(0, (ops2[0] & 0x02) != 0); // Message
			setUseTemp(1, (ops2[0] & 0x04) != 0); // Yes
			setUseTemp(2, (ops2[0] & 0x08) != 0); // No
			setUseTemp(3, (ops2[0] & 0x20) != 0); // Cancel
			setUseTemp(4, (ops2[0] & 0x10) != 0); // Title

			setPriority(ops2[1] + 1);
			setTimeout(ops2[2]);
			setLocalVar(ops2[3]);
		}

		public Instruction Write(Instruction inst)
		{
			if (inst != null)
			{
				wrappedByteArray ops1 = inst.Operands;
				wrappedByteArray ops2 = inst.Reserved1;

				ops1[0x01] = iconID;

				if (inst.NodeVersion == 0)
				{
					ops1[2] = (byte)messages[0];	// message
					ops1[0] = (byte)messages[3];	// cancel
				}
				else
				{
                    BhavWiz.FromShort(ref ops2, 5, messages[0]);	// message
                    byte[] lohi = { 0, 0 };
                    BhavWiz.FromShort(ref lohi, 0, messages[3]);	// cancel
                    ops1[0] = lohi[0];
                    ops1[2] = lohi[1];
				}
				ops1[3] = (byte)messages[1]; // Yes
				ops1[4] = (byte)messages[2]; // No
				ops1[6] = (byte)messages[4]; // Title

				ops1[5] = dialog;

				ops1[7] &= 0xfe; ops1[7] |= (byte)(nowait  ? 0x01 : 0);
				ops1[7] &= 0xf1; ops1[7] |= (byte)((iconType & 0x07) << 1);
				ops1[7] &= 0x8f; ops1[7] |= (byte)((tempVar  & 0x07) << 4);
				ops1[7] &= 0x7f; ops1[7] |= (byte)(noblock ? 0x80 : 0);

				ops2[0] &= 0xfd; ops2[0] |= (byte)(useTemp[0] ? 0x02 : 0); // Message
				ops2[0] &= 0xfb; ops2[0] |= (byte)(useTemp[1] ? 0x04 : 0); // Yes
				ops2[0] &= 0xf7; ops2[0] |= (byte)(useTemp[2] ? 0x08 : 0); // No
				ops2[0] &= 0xdf; ops2[0] |= (byte)(useTemp[3] ? 0x20 : 0); // Cancel
				ops2[0] &= 0xef; ops2[0] |= (byte)(useTemp[4] ? 0x10 : 0); // Title

				ops2[0] &= 0xbe;
				if      (scope == Scope.SemiGlobal) ops2[0] |= 0x01;
				else if (scope == Scope.Global)     ops2[0] |= 0x40;

				ops2[1] = (byte)(priority - 1);
				ops2[2] = timeout;
				ops2[3] = localVar;
				ops2[4] = tnsStyle;

			}
			return inst;
		}

		#endregion

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.pnWiz0x0024 = new System.Windows.Forms.Panel();
            this.btnDefTitle = new System.Windows.Forms.Button();
            this.btnDefButton3 = new System.Windows.Forms.Button();
            this.btnDefButton2 = new System.Windows.Forms.Button();
            this.btnDefButton1 = new System.Windows.Forms.Button();
            this.btnDefMessage = new System.Windows.Forms.Button();
            this.btnStrTitle = new System.Windows.Forms.Button();
            this.btnStrButton3 = new System.Windows.Forms.Button();
            this.btnStrButton2 = new System.Windows.Forms.Button();
            this.btnStrButton1 = new System.Windows.Forms.Button();
            this.btnStrMessage = new System.Windows.Forms.Button();
            this.tbStrTitle = new System.Windows.Forms.TextBox();
            this.tbStrButton3 = new System.Windows.Forms.TextBox();
            this.tbStrButton2 = new System.Windows.Forms.TextBox();
            this.tbTitle = new System.Windows.Forms.TextBox();
            this.tbMessage = new System.Windows.Forms.TextBox();
            this.tbButton3 = new System.Windows.Forms.TextBox();
            this.cbTVMessage = new System.Windows.Forms.ComboBox();
            this.tbButton2 = new System.Windows.Forms.TextBox();
            this.lbMessage = new System.Windows.Forms.Label();
            this.tbButton1 = new System.Windows.Forms.TextBox();
            this.cbBlockBHAV = new System.Windows.Forms.CheckBox();
            this.cbBlockSim = new System.Windows.Forms.CheckBox();
            this.cbUTTitle = new System.Windows.Forms.CheckBox();
            this.cbUTButton3 = new System.Windows.Forms.CheckBox();
            this.lbIconType = new System.Windows.Forms.Label();
            this.cbUTButton2 = new System.Windows.Forms.CheckBox();
            this.cbIconType = new System.Windows.Forms.ComboBox();
            this.cbUTButton1 = new System.Windows.Forms.CheckBox();
            this.tbStrButton1 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbStrMessage = new System.Windows.Forms.TextBox();
            this.tbIconID = new System.Windows.Forms.TextBox();
            this.btnStrIcon = new System.Windows.Forms.Button();
            this.cbTVTitle = new System.Windows.Forms.ComboBox();
            this.cbTVButton3 = new System.Windows.Forms.ComboBox();
            this.cbTVButton2 = new System.Windows.Forms.ComboBox();
            this.cbTVButton1 = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbUTMessage = new System.Windows.Forms.CheckBox();
            this.cbScope = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lbTitle = new System.Windows.Forms.Label();
            this.lbButton3 = new System.Windows.Forms.Label();
            this.lbButton2 = new System.Windows.Forms.Label();
            this.lbButton1 = new System.Windows.Forms.Label();
            this.lbType = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbType = new System.Windows.Forms.ComboBox();
            this.pnLocalVar = new System.Windows.Forms.Panel();
            this.tbLocalVar = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.pnTempVar = new System.Windows.Forms.Panel();
            this.cbTempVar = new System.Windows.Forms.ComboBox();
            this.lbTempVar = new System.Windows.Forms.Label();
            this.pnTNS = new System.Windows.Forms.Panel();
            this.tbPriority = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tbTimeout = new System.Windows.Forms.TextBox();
            this.lbTnsStyle = new System.Windows.Forms.Label();
            this.cbTnsStyle = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.pnWiz0x0024.SuspendLayout();
            this.pnLocalVar.SuspendLayout();
            this.pnTempVar.SuspendLayout();
            this.pnTNS.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnWiz0x0024
            // 
            this.pnWiz0x0024.Controls.Add(this.btnDefTitle);
            this.pnWiz0x0024.Controls.Add(this.btnDefButton3);
            this.pnWiz0x0024.Controls.Add(this.btnDefButton2);
            this.pnWiz0x0024.Controls.Add(this.btnDefButton1);
            this.pnWiz0x0024.Controls.Add(this.btnDefMessage);
            this.pnWiz0x0024.Controls.Add(this.btnStrTitle);
            this.pnWiz0x0024.Controls.Add(this.btnStrButton3);
            this.pnWiz0x0024.Controls.Add(this.btnStrButton2);
            this.pnWiz0x0024.Controls.Add(this.btnStrButton1);
            this.pnWiz0x0024.Controls.Add(this.btnStrMessage);
            this.pnWiz0x0024.Controls.Add(this.tbStrTitle);
            this.pnWiz0x0024.Controls.Add(this.tbStrButton3);
            this.pnWiz0x0024.Controls.Add(this.tbStrButton2);
            this.pnWiz0x0024.Controls.Add(this.tbTitle);
            this.pnWiz0x0024.Controls.Add(this.tbMessage);
            this.pnWiz0x0024.Controls.Add(this.tbButton3);
            this.pnWiz0x0024.Controls.Add(this.cbTVMessage);
            this.pnWiz0x0024.Controls.Add(this.tbButton2);
            this.pnWiz0x0024.Controls.Add(this.lbMessage);
            this.pnWiz0x0024.Controls.Add(this.tbButton1);
            this.pnWiz0x0024.Controls.Add(this.cbBlockBHAV);
            this.pnWiz0x0024.Controls.Add(this.cbBlockSim);
            this.pnWiz0x0024.Controls.Add(this.cbUTTitle);
            this.pnWiz0x0024.Controls.Add(this.cbUTButton3);
            this.pnWiz0x0024.Controls.Add(this.lbIconType);
            this.pnWiz0x0024.Controls.Add(this.cbUTButton2);
            this.pnWiz0x0024.Controls.Add(this.cbIconType);
            this.pnWiz0x0024.Controls.Add(this.cbUTButton1);
            this.pnWiz0x0024.Controls.Add(this.tbStrButton1);
            this.pnWiz0x0024.Controls.Add(this.label5);
            this.pnWiz0x0024.Controls.Add(this.tbStrMessage);
            this.pnWiz0x0024.Controls.Add(this.tbIconID);
            this.pnWiz0x0024.Controls.Add(this.btnStrIcon);
            this.pnWiz0x0024.Controls.Add(this.cbTVTitle);
            this.pnWiz0x0024.Controls.Add(this.cbTVButton3);
            this.pnWiz0x0024.Controls.Add(this.cbTVButton2);
            this.pnWiz0x0024.Controls.Add(this.cbTVButton1);
            this.pnWiz0x0024.Controls.Add(this.label4);
            this.pnWiz0x0024.Controls.Add(this.cbUTMessage);
            this.pnWiz0x0024.Controls.Add(this.cbScope);
            this.pnWiz0x0024.Controls.Add(this.label3);
            this.pnWiz0x0024.Controls.Add(this.lbTitle);
            this.pnWiz0x0024.Controls.Add(this.lbButton3);
            this.pnWiz0x0024.Controls.Add(this.lbButton2);
            this.pnWiz0x0024.Controls.Add(this.lbButton1);
            this.pnWiz0x0024.Controls.Add(this.lbType);
            this.pnWiz0x0024.Controls.Add(this.label1);
            this.pnWiz0x0024.Controls.Add(this.cbType);
            this.pnWiz0x0024.Controls.Add(this.pnLocalVar);
            this.pnWiz0x0024.Controls.Add(this.pnTempVar);
            this.pnWiz0x0024.Controls.Add(this.pnTNS);
            this.pnWiz0x0024.Location = new System.Drawing.Point(0, 0);
            this.pnWiz0x0024.Margin = new System.Windows.Forms.Padding(2);
            this.pnWiz0x0024.Name = "pnWiz0x0024";
            this.pnWiz0x0024.Size = new System.Drawing.Size(578, 238);
            this.pnWiz0x0024.TabIndex = 0;
            // 
            // btnDefTitle
            // 
            this.btnDefTitle.Font = new System.Drawing.Font("Comic Sans MS", 7.8F, System.Drawing.FontStyle.Bold);
            this.btnDefTitle.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnDefTitle.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnDefTitle.Location = new System.Drawing.Point(200, 216);
            this.btnDefTitle.Margin = new System.Windows.Forms.Padding(2);
            this.btnDefTitle.Name = "btnDefTitle";
            this.btnDefTitle.Size = new System.Drawing.Size(19, 18);
            this.btnDefTitle.TabIndex = 49;
            this.btnDefTitle.Text = "X";
            this.btnDefTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDefTitle.Click += new System.EventHandler(this.btnDef_Click);
            // 
            // btnDefButton3
            // 
            this.btnDefButton3.Font = new System.Drawing.Font("Comic Sans MS", 7.8F, System.Drawing.FontStyle.Bold);
            this.btnDefButton3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnDefButton3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnDefButton3.Location = new System.Drawing.Point(200, 197);
            this.btnDefButton3.Margin = new System.Windows.Forms.Padding(2);
            this.btnDefButton3.Name = "btnDefButton3";
            this.btnDefButton3.Size = new System.Drawing.Size(19, 18);
            this.btnDefButton3.TabIndex = 48;
            this.btnDefButton3.Text = "X";
            this.btnDefButton3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDefButton3.Click += new System.EventHandler(this.btnDef_Click);
            // 
            // btnDefButton2
            // 
            this.btnDefButton2.Font = new System.Drawing.Font("Comic Sans MS", 7.8F, System.Drawing.FontStyle.Bold);
            this.btnDefButton2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnDefButton2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnDefButton2.Location = new System.Drawing.Point(200, 178);
            this.btnDefButton2.Margin = new System.Windows.Forms.Padding(2);
            this.btnDefButton2.Name = "btnDefButton2";
            this.btnDefButton2.Size = new System.Drawing.Size(19, 18);
            this.btnDefButton2.TabIndex = 47;
            this.btnDefButton2.Text = "X";
            this.btnDefButton2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDefButton2.Click += new System.EventHandler(this.btnDef_Click);
            // 
            // btnDefButton1
            // 
            this.btnDefButton1.Font = new System.Drawing.Font("Comic Sans MS", 7.8F, System.Drawing.FontStyle.Bold);
            this.btnDefButton1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnDefButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnDefButton1.Location = new System.Drawing.Point(200, 158);
            this.btnDefButton1.Margin = new System.Windows.Forms.Padding(2);
            this.btnDefButton1.Name = "btnDefButton1";
            this.btnDefButton1.Size = new System.Drawing.Size(19, 18);
            this.btnDefButton1.TabIndex = 46;
            this.btnDefButton1.Text = "X";
            this.btnDefButton1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDefButton1.Click += new System.EventHandler(this.btnDef_Click);
            // 
            // btnDefMessage
            // 
            this.btnDefMessage.Font = new System.Drawing.Font("Comic Sans MS", 7.8F, System.Drawing.FontStyle.Bold);
            this.btnDefMessage.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnDefMessage.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnDefMessage.Location = new System.Drawing.Point(200, 139);
            this.btnDefMessage.Margin = new System.Windows.Forms.Padding(2);
            this.btnDefMessage.Name = "btnDefMessage";
            this.btnDefMessage.Size = new System.Drawing.Size(19, 18);
            this.btnDefMessage.TabIndex = 45;
            this.btnDefMessage.Text = "X";
            this.btnDefMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDefMessage.Click += new System.EventHandler(this.btnDef_Click);
            // 
            // btnStrTitle
            // 
            this.btnStrTitle.Font = new System.Drawing.Font("Webdings", 7.8F, System.Drawing.FontStyle.Bold);
            this.btnStrTitle.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnStrTitle.Location = new System.Drawing.Point(180, 216);
            this.btnStrTitle.Margin = new System.Windows.Forms.Padding(2);
            this.btnStrTitle.Name = "btnStrTitle";
            this.btnStrTitle.Size = new System.Drawing.Size(19, 18);
            this.btnStrTitle.TabIndex = 44;
            this.btnStrTitle.Text = "8";
            this.btnStrTitle.Click += new System.EventHandler(this.btnStr_Click);
            // 
            // btnStrButton3
            // 
            this.btnStrButton3.Font = new System.Drawing.Font("Webdings", 7.8F, System.Drawing.FontStyle.Bold);
            this.btnStrButton3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnStrButton3.Location = new System.Drawing.Point(180, 197);
            this.btnStrButton3.Margin = new System.Windows.Forms.Padding(2);
            this.btnStrButton3.Name = "btnStrButton3";
            this.btnStrButton3.Size = new System.Drawing.Size(19, 18);
            this.btnStrButton3.TabIndex = 43;
            this.btnStrButton3.Text = "8";
            this.btnStrButton3.Click += new System.EventHandler(this.btnStr_Click);
            // 
            // btnStrButton2
            // 
            this.btnStrButton2.Font = new System.Drawing.Font("Webdings", 7.8F, System.Drawing.FontStyle.Bold);
            this.btnStrButton2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnStrButton2.Location = new System.Drawing.Point(180, 178);
            this.btnStrButton2.Margin = new System.Windows.Forms.Padding(2);
            this.btnStrButton2.Name = "btnStrButton2";
            this.btnStrButton2.Size = new System.Drawing.Size(19, 18);
            this.btnStrButton2.TabIndex = 42;
            this.btnStrButton2.Text = "8";
            this.btnStrButton2.Click += new System.EventHandler(this.btnStr_Click);
            // 
            // btnStrButton1
            // 
            this.btnStrButton1.Font = new System.Drawing.Font("Webdings", 7.8F, System.Drawing.FontStyle.Bold);
            this.btnStrButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnStrButton1.Location = new System.Drawing.Point(180, 158);
            this.btnStrButton1.Margin = new System.Windows.Forms.Padding(2);
            this.btnStrButton1.Name = "btnStrButton1";
            this.btnStrButton1.Size = new System.Drawing.Size(19, 18);
            this.btnStrButton1.TabIndex = 41;
            this.btnStrButton1.Text = "8";
            this.btnStrButton1.Click += new System.EventHandler(this.btnStr_Click);
            // 
            // btnStrMessage
            // 
            this.btnStrMessage.Font = new System.Drawing.Font("Webdings", 7.8F, System.Drawing.FontStyle.Bold);
            this.btnStrMessage.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnStrMessage.Location = new System.Drawing.Point(180, 139);
            this.btnStrMessage.Margin = new System.Windows.Forms.Padding(2);
            this.btnStrMessage.Name = "btnStrMessage";
            this.btnStrMessage.Size = new System.Drawing.Size(19, 18);
            this.btnStrMessage.TabIndex = 40;
            this.btnStrMessage.Text = "8";
            this.btnStrMessage.Click += new System.EventHandler(this.btnStr_Click);
            // 
            // tbStrTitle
            // 
            this.tbStrTitle.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbStrTitle.Location = new System.Drawing.Point(225, 218);
            this.tbStrTitle.Margin = new System.Windows.Forms.Padding(2);
            this.tbStrTitle.Name = "tbStrTitle";
            this.tbStrTitle.ReadOnly = true;
            this.tbStrTitle.Size = new System.Drawing.Size(242, 13);
            this.tbStrTitle.TabIndex = 39;
            this.tbStrTitle.TabStop = false;
            this.tbStrTitle.Text = "Title string";
            // 
            // tbStrButton3
            // 
            this.tbStrButton3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbStrButton3.Location = new System.Drawing.Point(225, 199);
            this.tbStrButton3.Margin = new System.Windows.Forms.Padding(2);
            this.tbStrButton3.Name = "tbStrButton3";
            this.tbStrButton3.ReadOnly = true;
            this.tbStrButton3.Size = new System.Drawing.Size(242, 13);
            this.tbStrButton3.TabIndex = 38;
            this.tbStrButton3.TabStop = false;
            this.tbStrButton3.Text = "Button3 string";
            // 
            // tbStrButton2
            // 
            this.tbStrButton2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbStrButton2.Location = new System.Drawing.Point(225, 180);
            this.tbStrButton2.Margin = new System.Windows.Forms.Padding(2);
            this.tbStrButton2.Name = "tbStrButton2";
            this.tbStrButton2.ReadOnly = true;
            this.tbStrButton2.Size = new System.Drawing.Size(242, 13);
            this.tbStrButton2.TabIndex = 37;
            this.tbStrButton2.TabStop = false;
            this.tbStrButton2.Text = "Button2 string";
            // 
            // tbTitle
            // 
            this.tbTitle.Location = new System.Drawing.Point(131, 216);
            this.tbTitle.Margin = new System.Windows.Forms.Padding(2);
            this.tbTitle.Name = "tbTitle";
            this.tbTitle.Size = new System.Drawing.Size(50, 20);
            this.tbTitle.TabIndex = 28;
            this.tbTitle.Text = "0xDDDD";
            this.tbTitle.TextChanged += new System.EventHandler(this.hex16_TextChanged);
            this.tbTitle.Validated += new System.EventHandler(this.hex16_Validated);
            this.tbTitle.Validating += new System.ComponentModel.CancelEventHandler(this.hex16_Validating);
            // 
            // tbMessage
            // 
            this.tbMessage.Location = new System.Drawing.Point(131, 139);
            this.tbMessage.Margin = new System.Windows.Forms.Padding(2);
            this.tbMessage.Name = "tbMessage";
            this.tbMessage.Size = new System.Drawing.Size(50, 20);
            this.tbMessage.TabIndex = 8;
            this.tbMessage.Text = "0xDDDD";
            this.tbMessage.TextChanged += new System.EventHandler(this.hex16_TextChanged);
            this.tbMessage.Validated += new System.EventHandler(this.hex16_Validated);
            this.tbMessage.Validating += new System.ComponentModel.CancelEventHandler(this.hex16_Validating);
            // 
            // tbButton3
            // 
            this.tbButton3.Location = new System.Drawing.Point(131, 197);
            this.tbButton3.Margin = new System.Windows.Forms.Padding(2);
            this.tbButton3.Name = "tbButton3";
            this.tbButton3.Size = new System.Drawing.Size(50, 20);
            this.tbButton3.TabIndex = 23;
            this.tbButton3.Text = "0xDDDD";
            this.tbButton3.TextChanged += new System.EventHandler(this.hex16_TextChanged);
            this.tbButton3.Validated += new System.EventHandler(this.hex16_Validated);
            this.tbButton3.Validating += new System.ComponentModel.CancelEventHandler(this.hex16_Validating);
            // 
            // cbTVMessage
            // 
            this.cbTVMessage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTVMessage.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7"});
            this.cbTVMessage.Location = new System.Drawing.Point(62, 139);
            this.cbTVMessage.Margin = new System.Windows.Forms.Padding(2);
            this.cbTVMessage.Name = "cbTVMessage";
            this.cbTVMessage.Size = new System.Drawing.Size(39, 21);
            this.cbTVMessage.Sorted = true;
            this.cbTVMessage.TabIndex = 6;
            this.cbTVMessage.SelectedIndexChanged += new System.EventHandler(this.cbTempVar_SelectedIndexChanged);
            // 
            // tbButton2
            // 
            this.tbButton2.Location = new System.Drawing.Point(131, 178);
            this.tbButton2.Margin = new System.Windows.Forms.Padding(2);
            this.tbButton2.Name = "tbButton2";
            this.tbButton2.Size = new System.Drawing.Size(50, 20);
            this.tbButton2.TabIndex = 18;
            this.tbButton2.Text = "0xDDDD";
            this.tbButton2.TextChanged += new System.EventHandler(this.hex16_TextChanged);
            this.tbButton2.Validated += new System.EventHandler(this.hex16_Validated);
            this.tbButton2.Validating += new System.ComponentModel.CancelEventHandler(this.hex16_Validating);
            // 
            // lbMessage
            // 
            this.lbMessage.AutoSize = true;
            this.lbMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold);
            this.lbMessage.Location = new System.Drawing.Point(0, 142);
            this.lbMessage.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbMessage.Name = "lbMessage";
            this.lbMessage.Size = new System.Drawing.Size(57, 13);
            this.lbMessage.TabIndex = 0;
            this.lbMessage.Text = "Message";
            // 
            // tbButton1
            // 
            this.tbButton1.Location = new System.Drawing.Point(131, 158);
            this.tbButton1.Margin = new System.Windows.Forms.Padding(2);
            this.tbButton1.Name = "tbButton1";
            this.tbButton1.Size = new System.Drawing.Size(50, 20);
            this.tbButton1.TabIndex = 13;
            this.tbButton1.Text = "0xDDDD";
            this.tbButton1.TextChanged += new System.EventHandler(this.hex16_TextChanged);
            this.tbButton1.Validated += new System.EventHandler(this.hex16_Validated);
            this.tbButton1.Validating += new System.ComponentModel.CancelEventHandler(this.hex16_Validating);
            // 
            // cbBlockBHAV
            // 
            this.cbBlockBHAV.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbBlockBHAV.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbBlockBHAV.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold);
            this.cbBlockBHAV.Location = new System.Drawing.Point(472, 154);
            this.cbBlockBHAV.Margin = new System.Windows.Forms.Padding(2);
            this.cbBlockBHAV.Name = "cbBlockBHAV";
            this.cbBlockBHAV.Size = new System.Drawing.Size(106, 22);
            this.cbBlockBHAV.TabIndex = 33;
            this.cbBlockBHAV.Text = "Wait for user";
            this.cbBlockBHAV.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbBlockBHAV.CheckedChanged += new System.EventHandler(this.cbBlockBHAV_CheckedChanged);
            // 
            // cbBlockSim
            // 
            this.cbBlockSim.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbBlockSim.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbBlockSim.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold);
            this.cbBlockSim.Location = new System.Drawing.Point(472, 180);
            this.cbBlockSim.Margin = new System.Windows.Forms.Padding(2);
            this.cbBlockSim.Name = "cbBlockSim";
            this.cbBlockSim.Size = new System.Drawing.Size(106, 22);
            this.cbBlockSim.TabIndex = 34;
            this.cbBlockSim.Text = "Block Sim";
            this.cbBlockSim.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbBlockSim.CheckedChanged += new System.EventHandler(this.cbBlockSim_CheckedChanged);
            // 
            // cbUTTitle
            // 
            this.cbUTTitle.Location = new System.Drawing.Point(106, 218);
            this.cbUTTitle.Margin = new System.Windows.Forms.Padding(2);
            this.cbUTTitle.Name = "cbUTTitle";
            this.cbUTTitle.Size = new System.Drawing.Size(21, 14);
            this.cbUTTitle.TabIndex = 27;
            this.cbUTTitle.Text = "-";
            this.cbUTTitle.CheckedChanged += new System.EventHandler(this.cbUT_CheckedChanged);
            // 
            // cbUTButton3
            // 
            this.cbUTButton3.Location = new System.Drawing.Point(106, 199);
            this.cbUTButton3.Margin = new System.Windows.Forms.Padding(2);
            this.cbUTButton3.Name = "cbUTButton3";
            this.cbUTButton3.Size = new System.Drawing.Size(21, 14);
            this.cbUTButton3.TabIndex = 22;
            this.cbUTButton3.Text = "-";
            this.cbUTButton3.CheckedChanged += new System.EventHandler(this.cbUT_CheckedChanged);
            // 
            // lbIconType
            // 
            this.lbIconType.AutoSize = true;
            this.lbIconType.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold);
            this.lbIconType.Location = new System.Drawing.Point(336, 3);
            this.lbIconType.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbIconType.Name = "lbIconType";
            this.lbIconType.Size = new System.Drawing.Size(32, 13);
            this.lbIconType.TabIndex = 0;
            this.lbIconType.Text = "Icon";
            // 
            // cbUTButton2
            // 
            this.cbUTButton2.Location = new System.Drawing.Point(106, 180);
            this.cbUTButton2.Margin = new System.Windows.Forms.Padding(2);
            this.cbUTButton2.Name = "cbUTButton2";
            this.cbUTButton2.Size = new System.Drawing.Size(21, 14);
            this.cbUTButton2.TabIndex = 17;
            this.cbUTButton2.Text = "-";
            this.cbUTButton2.CheckedChanged += new System.EventHandler(this.cbUT_CheckedChanged);
            // 
            // cbIconType
            // 
            this.cbIconType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbIconType.DropDownWidth = 120;
            this.cbIconType.Location = new System.Drawing.Point(371, 0);
            this.cbIconType.Margin = new System.Windows.Forms.Padding(2);
            this.cbIconType.Name = "cbIconType";
            this.cbIconType.Size = new System.Drawing.Size(77, 21);
            this.cbIconType.TabIndex = 2;
            this.cbIconType.SelectedIndexChanged += new System.EventHandler(this.cbIconType_SelectedIndexChanged);
            // 
            // cbUTButton1
            // 
            this.cbUTButton1.Location = new System.Drawing.Point(106, 161);
            this.cbUTButton1.Margin = new System.Windows.Forms.Padding(2);
            this.cbUTButton1.Name = "cbUTButton1";
            this.cbUTButton1.Size = new System.Drawing.Size(21, 15);
            this.cbUTButton1.TabIndex = 12;
            this.cbUTButton1.Text = "-";
            this.cbUTButton1.CheckedChanged += new System.EventHandler(this.cbUT_CheckedChanged);
            // 
            // tbStrButton1
            // 
            this.tbStrButton1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbStrButton1.Location = new System.Drawing.Point(225, 161);
            this.tbStrButton1.Margin = new System.Windows.Forms.Padding(2);
            this.tbStrButton1.Name = "tbStrButton1";
            this.tbStrButton1.ReadOnly = true;
            this.tbStrButton1.Size = new System.Drawing.Size(242, 13);
            this.tbStrButton1.TabIndex = 36;
            this.tbStrButton1.TabStop = false;
            this.tbStrButton1.Text = "Button1 string";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(457, 3);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(15, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "#";
            // 
            // tbStrMessage
            // 
            this.tbStrMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbStrMessage.Location = new System.Drawing.Point(225, 142);
            this.tbStrMessage.Margin = new System.Windows.Forms.Padding(2);
            this.tbStrMessage.Name = "tbStrMessage";
            this.tbStrMessage.ReadOnly = true;
            this.tbStrMessage.Size = new System.Drawing.Size(242, 13);
            this.tbStrMessage.TabIndex = 35;
            this.tbStrMessage.TabStop = false;
            this.tbStrMessage.Text = "Message string";
            // 
            // tbIconID
            // 
            this.tbIconID.Location = new System.Drawing.Point(475, 1);
            this.tbIconID.Margin = new System.Windows.Forms.Padding(2);
            this.tbIconID.MaxLength = 4;
            this.tbIconID.Name = "tbIconID";
            this.tbIconID.Size = new System.Drawing.Size(39, 20);
            this.tbIconID.TabIndex = 3;
            this.tbIconID.Text = "0xDD";
            this.tbIconID.TextChanged += new System.EventHandler(this.hex8_TextChanged);
            this.tbIconID.Validated += new System.EventHandler(this.hex8_TextChanged);
            this.tbIconID.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
            // 
            // btnStrIcon
            // 
            this.btnStrIcon.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnStrIcon.Font = new System.Drawing.Font("Webdings", 12F, System.Drawing.FontStyle.Bold);
            this.btnStrIcon.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnStrIcon.Location = new System.Drawing.Point(514, 1);
            this.btnStrIcon.Margin = new System.Windows.Forms.Padding(2);
            this.btnStrIcon.Name = "btnStrIcon";
            this.btnStrIcon.Size = new System.Drawing.Size(20, 18);
            this.btnStrIcon.TabIndex = 4;
            this.btnStrIcon.Text = "8";
            this.btnStrIcon.Click += new System.EventHandler(this.btnStr_Click);
            // 
            // cbTVTitle
            // 
            this.cbTVTitle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTVTitle.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7"});
            this.cbTVTitle.Location = new System.Drawing.Point(62, 216);
            this.cbTVTitle.Margin = new System.Windows.Forms.Padding(2);
            this.cbTVTitle.Name = "cbTVTitle";
            this.cbTVTitle.Size = new System.Drawing.Size(39, 21);
            this.cbTVTitle.Sorted = true;
            this.cbTVTitle.TabIndex = 26;
            this.cbTVTitle.SelectedIndexChanged += new System.EventHandler(this.cbTempVar_SelectedIndexChanged);
            // 
            // cbTVButton3
            // 
            this.cbTVButton3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTVButton3.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7"});
            this.cbTVButton3.Location = new System.Drawing.Point(62, 197);
            this.cbTVButton3.Margin = new System.Windows.Forms.Padding(2);
            this.cbTVButton3.Name = "cbTVButton3";
            this.cbTVButton3.Size = new System.Drawing.Size(39, 21);
            this.cbTVButton3.Sorted = true;
            this.cbTVButton3.TabIndex = 21;
            this.cbTVButton3.SelectedIndexChanged += new System.EventHandler(this.cbTempVar_SelectedIndexChanged);
            // 
            // cbTVButton2
            // 
            this.cbTVButton2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTVButton2.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7"});
            this.cbTVButton2.Location = new System.Drawing.Point(62, 178);
            this.cbTVButton2.Margin = new System.Windows.Forms.Padding(2);
            this.cbTVButton2.Name = "cbTVButton2";
            this.cbTVButton2.Size = new System.Drawing.Size(39, 21);
            this.cbTVButton2.Sorted = true;
            this.cbTVButton2.TabIndex = 16;
            this.cbTVButton2.SelectedIndexChanged += new System.EventHandler(this.cbTempVar_SelectedIndexChanged);
            // 
            // cbTVButton1
            // 
            this.cbTVButton1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTVButton1.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7"});
            this.cbTVButton1.Location = new System.Drawing.Point(62, 158);
            this.cbTVButton1.Margin = new System.Windows.Forms.Padding(2);
            this.cbTVButton1.Name = "cbTVButton1";
            this.cbTVButton1.Size = new System.Drawing.Size(39, 21);
            this.cbTVButton1.Sorted = true;
            this.cbTVButton1.TabIndex = 11;
            this.cbTVButton1.SelectedIndexChanged += new System.EventHandler(this.cbTempVar_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(83, 120);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 15);
            this.label4.TabIndex = 0;
            this.label4.Text = "Use temp";
            this.label4.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // cbUTMessage
            // 
            this.cbUTMessage.Location = new System.Drawing.Point(106, 142);
            this.cbUTMessage.Margin = new System.Windows.Forms.Padding(2);
            this.cbUTMessage.Name = "cbUTMessage";
            this.cbUTMessage.Size = new System.Drawing.Size(21, 15);
            this.cbUTMessage.TabIndex = 7;
            this.cbUTMessage.Text = "-";
            this.cbUTMessage.CheckedChanged += new System.EventHandler(this.cbUT_CheckedChanged);
            // 
            // cbScope
            // 
            this.cbScope.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbScope.Items.AddRange(new object[] {
            "Global",
            "Semi-global",
            "Private"});
            this.cbScope.Location = new System.Drawing.Point(266, 106);
            this.cbScope.Margin = new System.Windows.Forms.Padding(2);
            this.cbScope.Name = "cbScope";
            this.cbScope.Size = new System.Drawing.Size(138, 21);
            this.cbScope.TabIndex = 5;
            this.cbScope.SelectedIndexChanged += new System.EventHandler(this.cbScope_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(180, 108);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "String Scope";
            // 
            // lbTitle
            // 
            this.lbTitle.AutoSize = true;
            this.lbTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold);
            this.lbTitle.Location = new System.Drawing.Point(26, 218);
            this.lbTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(32, 13);
            this.lbTitle.TabIndex = 0;
            this.lbTitle.Text = "Title";
            // 
            // lbButton3
            // 
            this.lbButton3.AutoSize = true;
            this.lbButton3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold);
            this.lbButton3.Location = new System.Drawing.Point(2, 199);
            this.lbButton3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbButton3.Name = "lbButton3";
            this.lbButton3.Size = new System.Drawing.Size(55, 13);
            this.lbButton3.TabIndex = 0;
            this.lbButton3.Text = "Button 3";
            // 
            // lbButton2
            // 
            this.lbButton2.AutoSize = true;
            this.lbButton2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold);
            this.lbButton2.Location = new System.Drawing.Point(2, 180);
            this.lbButton2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbButton2.Name = "lbButton2";
            this.lbButton2.Size = new System.Drawing.Size(55, 13);
            this.lbButton2.TabIndex = 0;
            this.lbButton2.Text = "Button 2";
            // 
            // lbButton1
            // 
            this.lbButton1.AutoSize = true;
            this.lbButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold);
            this.lbButton1.Location = new System.Drawing.Point(2, 161);
            this.lbButton1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbButton1.Name = "lbButton1";
            this.lbButton1.Size = new System.Drawing.Size(55, 13);
            this.lbButton1.TabIndex = 0;
            this.lbButton1.Text = "Button 1";
            // 
            // lbType
            // 
            this.lbType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbType.Location = new System.Drawing.Point(0, 19);
            this.lbType.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbType.Name = "lbType";
            this.lbType.Size = new System.Drawing.Size(578, 58);
            this.lbType.TabIndex = 0;
            this.lbType.Text = "Description of dialog type";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(0, 3);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Dialogue Type";
            // 
            // cbType
            // 
            this.cbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbType.DropDownWidth = 160;
            this.cbType.Location = new System.Drawing.Point(93, 0);
            this.cbType.Margin = new System.Windows.Forms.Padding(2);
            this.cbType.Name = "cbType";
            this.cbType.Size = new System.Drawing.Size(239, 21);
            this.cbType.TabIndex = 1;
            this.cbType.SelectedIndexChanged += new System.EventHandler(this.cbType_SelectedIndexChanged);
            // 
            // pnLocalVar
            // 
            this.pnLocalVar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pnLocalVar.Controls.Add(this.tbLocalVar);
            this.pnLocalVar.Controls.Add(this.label8);
            this.pnLocalVar.Location = new System.Drawing.Point(472, 130);
            this.pnLocalVar.Margin = new System.Windows.Forms.Padding(2);
            this.pnLocalVar.Name = "pnLocalVar";
            this.pnLocalVar.Size = new System.Drawing.Size(106, 19);
            this.pnLocalVar.TabIndex = 32;
            // 
            // tbLocalVar
            // 
            this.tbLocalVar.Location = new System.Drawing.Point(67, 0);
            this.tbLocalVar.Margin = new System.Windows.Forms.Padding(2);
            this.tbLocalVar.MaxLength = 4;
            this.tbLocalVar.Name = "tbLocalVar";
            this.tbLocalVar.Size = new System.Drawing.Size(39, 20);
            this.tbLocalVar.TabIndex = 1;
            this.tbLocalVar.Text = "0xDD";
            this.tbLocalVar.TextChanged += new System.EventHandler(this.hex8_TextChanged);
            this.tbLocalVar.Validated += new System.EventHandler(this.hex8_Validated);
            this.tbLocalVar.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold);
            this.label8.Location = new System.Drawing.Point(0, 2);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(61, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "Local Var";
            // 
            // pnTempVar
            // 
            this.pnTempVar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pnTempVar.Controls.Add(this.cbTempVar);
            this.pnTempVar.Controls.Add(this.lbTempVar);
            this.pnTempVar.Location = new System.Drawing.Point(472, 106);
            this.pnTempVar.Margin = new System.Windows.Forms.Padding(2);
            this.pnTempVar.Name = "pnTempVar";
            this.pnTempVar.Size = new System.Drawing.Size(106, 19);
            this.pnTempVar.TabIndex = 31;
            // 
            // cbTempVar
            // 
            this.cbTempVar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTempVar.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7"});
            this.cbTempVar.Location = new System.Drawing.Point(67, 0);
            this.cbTempVar.Margin = new System.Windows.Forms.Padding(2);
            this.cbTempVar.Name = "cbTempVar";
            this.cbTempVar.Size = new System.Drawing.Size(39, 21);
            this.cbTempVar.Sorted = true;
            this.cbTempVar.TabIndex = 1;
            this.cbTempVar.SelectedIndexChanged += new System.EventHandler(this.cbTempVar_SelectedIndexChanged);
            // 
            // lbTempVar
            // 
            this.lbTempVar.AutoSize = true;
            this.lbTempVar.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold);
            this.lbTempVar.Location = new System.Drawing.Point(0, 2);
            this.lbTempVar.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbTempVar.Name = "lbTempVar";
            this.lbTempVar.Size = new System.Drawing.Size(61, 13);
            this.lbTempVar.TabIndex = 0;
            this.lbTempVar.Text = "Temp Var";
            // 
            // pnTNS
            // 
            this.pnTNS.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnTNS.Controls.Add(this.tbPriority);
            this.pnTNS.Controls.Add(this.label6);
            this.pnTNS.Controls.Add(this.label7);
            this.pnTNS.Controls.Add(this.tbTimeout);
            this.pnTNS.Controls.Add(this.lbTnsStyle);
            this.pnTNS.Controls.Add(this.cbTnsStyle);
            this.pnTNS.Location = new System.Drawing.Point(0, 77);
            this.pnTNS.Margin = new System.Windows.Forms.Padding(2);
            this.pnTNS.Name = "pnTNS";
            this.pnTNS.Size = new System.Drawing.Size(578, 19);
            this.pnTNS.TabIndex = 5;
            // 
            // tbPriority
            // 
            this.tbPriority.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbPriority.Location = new System.Drawing.Point(435, 1);
            this.tbPriority.Margin = new System.Windows.Forms.Padding(2);
            this.tbPriority.MaxLength = 4;
            this.tbPriority.Name = "tbPriority";
            this.tbPriority.Size = new System.Drawing.Size(39, 20);
            this.tbPriority.TabIndex = 2;
            this.tbPriority.Text = "0xDD";
            this.tbPriority.TextChanged += new System.EventHandler(this.hex8_TextChanged);
            this.tbPriority.Validated += new System.EventHandler(this.hex8_Validated);
            this.tbPriority.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(382, 3);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Priority";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold);
            this.label7.Location = new System.Drawing.Point(483, 3);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Timeout";
            // 
            // tbTimeout
            // 
            this.tbTimeout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbTimeout.Location = new System.Drawing.Point(539, 1);
            this.tbTimeout.Margin = new System.Windows.Forms.Padding(2);
            this.tbTimeout.MaxLength = 4;
            this.tbTimeout.Name = "tbTimeout";
            this.tbTimeout.Size = new System.Drawing.Size(39, 20);
            this.tbTimeout.TabIndex = 3;
            this.tbTimeout.Text = "0xDD";
            this.tbTimeout.TextChanged += new System.EventHandler(this.hex8_TextChanged);
            this.tbTimeout.Validated += new System.EventHandler(this.hex8_Validated);
            this.tbTimeout.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
            // 
            // lbTnsStyle
            // 
            this.lbTnsStyle.AutoSize = true;
            this.lbTnsStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold);
            this.lbTnsStyle.Location = new System.Drawing.Point(0, 3);
            this.lbTnsStyle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbTnsStyle.Name = "lbTnsStyle";
            this.lbTnsStyle.Size = new System.Drawing.Size(64, 13);
            this.lbTnsStyle.TabIndex = 0;
            this.lbTnsStyle.Text = "TNS Style";
            // 
            // cbTnsStyle
            // 
            this.cbTnsStyle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbTnsStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTnsStyle.Location = new System.Drawing.Point(69, 0);
            this.cbTnsStyle.Margin = new System.Windows.Forms.Padding(2);
            this.cbTnsStyle.Name = "cbTnsStyle";
            this.cbTnsStyle.Size = new System.Drawing.Size(305, 21);
            this.cbTnsStyle.TabIndex = 1;
            this.cbTnsStyle.SelectedIndexChanged += new System.EventHandler(this.cbTnsStyle_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Location = new System.Drawing.Point(582, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(151, 58);
            this.label2.TabIndex = 1;
            this.label2.Text = "see edithWiki AkeaPostMortem for a nice Edith DialogEditor screenshot";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(582, 242);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(71, 21);
            this.button1.TabIndex = 2;
            this.button1.Text = "button1";
            // 
            // UI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(753, 507);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pnWiz0x0024);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "UI";
            this.Text = "UI";
            this.pnWiz0x0024.ResumeLayout(false);
            this.pnWiz0x0024.PerformLayout();
            this.pnLocalVar.ResumeLayout(false);
            this.pnLocalVar.PerformLayout();
            this.pnTempVar.ResumeLayout(false);
            this.pnTempVar.PerformLayout();
            this.pnTNS.ResumeLayout(false);
            this.pnTNS.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

		private void cbType_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (internalchg) return;

			setType(((ComboBox)sender).SelectedIndex);
		}

		private void cbTnsStyle_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (internalchg) return;

			setTnsStyle(((ComboBox)sender).SelectedIndex);
		}

		private void cbScope_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (internalchg) return;

			setScope(((ComboBox)sender).SelectedIndex);
		}

		private void cbIconType_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (internalchg) return;

			setIconType(((ComboBox)sender).SelectedIndex);
		}


		private void cbBlockBHAV_CheckedChanged(object sender, System.EventArgs e)
		{
			if (internalchg) return;

			setBlockBHAV(((CheckBox)sender).Checked);
		}

		private void cbBlockSim_CheckedChanged(object sender, System.EventArgs e)
		{
			if (internalchg) return;

			setBlockSim(((CheckBox)sender).Checked);
		}

		private void cbUT_CheckedChanged(object sender, System.EventArgs e)
		{
			if (internalchg) return;

			setUseTemp(alCBUseTemp.IndexOf(sender), ((CheckBox)sender).Checked);
		}

		private void cbTempVar_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (internalchg) return;

			int i = this.alCBTempVar.IndexOf(sender);
			if (i >= 0)
				setString(i, ((ComboBox)sender).SelectedIndex);
			else
				setTempVar(((ComboBox)sender).SelectedIndex);
		}


		private void btnStr_Click(object sender, System.EventArgs e)
		{
			doStrChooser(alStrBtn.IndexOf(sender));
		}

		private void btnDef_Click(object sender, System.EventArgs e)
		{
			this.setString(alDefBtn.IndexOf(sender), 0);
		}


		private void hex8_TextChanged(object sender, System.EventArgs ev)
		{
			if (internalchg) return;
			if (!hex8_IsValid(sender)) return;

			byte val = Convert.ToByte(((TextBox)sender).Text, 16);
			int i = alHex8.IndexOf(sender);

			internalchg = true;

			switch(i)
			{
				case 0: setPriority(val); break;
				case 1: setTimeout(val); break;
				case 2: setLocalVar(val); break;
				case 3: setIconID(val); break;
			}

			internalchg = false;
		}

		private void hex8_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (hex8_IsValid(sender)) return;

			e.Cancel = true;

			byte val = 0;
			int i = alHex8.IndexOf(sender);

			switch(i)
			{
				case 0: val = priority; break;
				case 1: val = timeout; break;
				case 2: val = localVar; break;
				case 3: val = iconID; break;
			}

			bool origstate = internalchg;
			internalchg = true;
			((TextBox)sender).Text = "0x" + SimPe.Helper.HexString(val);
			((TextBox)sender).SelectAll();
			internalchg = origstate;
		}

		private void hex8_Validated(object sender, System.EventArgs e)
		{
			bool origstate = internalchg;
			internalchg = true;
			((TextBox)sender).Text = "0x" + SimPe.Helper.HexString(Convert.ToByte(((TextBox)sender).Text, 16));
			((TextBox)sender).SelectAll();
			internalchg = origstate;
		}


		private void hex16_TextChanged(object sender, System.EventArgs ev)
		{
			if (internalchg) return;
			if (!hex16_IsValid(sender)) return;

			internalchg = true;
			setString(alHex16.IndexOf(sender), Convert.ToUInt16(((TextBox)sender).Text, 16));
			internalchg = false;
		}

		private void hex16_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (hex16_IsValid(sender)) return;

			e.Cancel = true;

			bool origstate = internalchg;
			internalchg = true;
			((TextBox)sender).Text = "0x" + SimPe.Helper.HexString(messages[alHex16.IndexOf(sender)]);
			((TextBox)sender).SelectAll();
			internalchg = origstate;
		}

		private void hex16_Validated(object sender, System.EventArgs e)
		{
			bool origstate = internalchg;
			internalchg = true;
			((TextBox)sender).Text = "0x" + SimPe.Helper.HexString(Convert.ToUInt16(((TextBox)sender).Text, 16));
			((TextBox)sender).SelectAll();
			internalchg = origstate;
		}


	}


}

namespace pjse.BhavOperandWizards
{
	public class BhavOperandWiz0x0024 : pjse.ABhavOperandWiz
	{
		public BhavOperandWiz0x0024(Instruction i) : base(i) { myForm = new Wiz0x0024.UI(); }

		#region IDisposable Members
		public override void Dispose()
		{
			if (myForm != null) myForm = null;
		}
		#endregion

	}

}
