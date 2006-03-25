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
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using SimPe.PackedFiles.Wrapper;

namespace pjse.BhavOperandWizards.Wiz0x0024
{
	/// <summary>
	/// Zusammenfassung für BhavInstruction.
	/// </summary>
	internal class UI : System.Windows.Forms.Form
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
		private System.Windows.Forms.Button btnStrMessage;
		private System.Windows.Forms.Button btnStrButton1;
		private System.Windows.Forms.Button btnStrButton2;
		private System.Windows.Forms.Button btnStrButton3;
		private System.Windows.Forms.Button btnStrTitle;
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
		private System.Windows.Forms.Button btnDefMessage;
		private System.Windows.Forms.Button btnDefButton1;
		private System.Windows.Forms.Button btnDefButton2;
		private System.Windows.Forms.Button btnDefButton3;
		private System.Windows.Forms.Button btnDefTitle;
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
		/// <summary>
		/// Erforderliche Designervariable.
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
			cbType.Items.AddRange(GS.gStr(GS.BhavStr.Dialog).ToArray());

			if (typeDescriptions == null)
				typeDescriptions = (string[])(GS.gStr(GS.BhavStr.DialogDesc).ToArray(typeof(string)));

			cbTnsStyle.Items.Clear();
			cbTnsStyle.Items.AddRange(GS.gStr(GS.BhavStr.TnsStyle).ToArray());

			cbIconType.Items.Clear();
			cbIconType.Items.AddRange(GS.gStr(GS.BhavStr.DialogIcon).ToArray());

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


		#region UI
		private static string[] typeDescriptions = null;

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

			this.lbType.Text = typeDescriptions.Length > dialog ? typeDescriptions[dialog] : "";

			bool tvState = false;
			bool tnsState = false;
			bool lvState = false;

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
				case 0x16: case 0x19:
					states[0] = states[4] = true; // message, title
					break;
				default:
					states[0] = states[1] = states[2] = states[4] = true; // message, button 1, button 2, title
					break;
			}

			this.pnTempVar.Visible  = tvState;
			this.pnTNS.Visible      = tnsState;
			this.pnLocalVar.Visible = lvState;

			internalchg = false;

			// Make the display match the help text
			for(int i = 0; i < states.Length; i++)
				setString(i, messages[i]);
		}

		private void setTnsStyle(int newStyle)
		{
			internalchg = true;

			tnsStyle = (byte)newStyle;

			if (cbTnsStyle.Items.Count > tnsStyle)
				cbTnsStyle.SelectedIndex = tnsStyle;

			internalchg = false;
		}

		private void setScope(int newScope)
		{
			internalchg = true;

			scope = (Scope)newScope;

			if (cbScope.SelectedIndex != newScope)
				cbScope.SelectedIndex = newScope;

			for(int i = 0; i < messages.Length; i++)
				setString(i, messages[i]);

			internalchg = false;
		}

		private void setIconType(int newType)
		{
			internalchg = true;

			iconType = (byte)newType;

			if (cbIconType.SelectedIndex != iconType)
				cbIconType.SelectedIndex = iconType;
			tbIconID.Enabled = (iconType == 3);
			btnStrIcon.Enabled = (iconType == 4);

			internalchg = false;
		}

		private void setTempVar(int newTempVar)
		{
			internalchg = true;

			tempVar = (byte)newTempVar;
			this.cbTempVar.SelectedIndex = tempVar;

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
					? "[none]"
					: ((BhavWiz)inst).readStr(scope, GS.GlobalStr.DialogString, strnum - 1, -1, pjse.Detail.Errors)
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
				return; // eek!

			SimPe.PackedFiles.Wrapper.Str str = new Str();
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
					ops2[5] = (byte)(messages[0] & 0xff);	// message
					ops1[6] = (byte)(messages[0] >> 8);		// message
					ops1[0] = (byte)(messages[3] & 0xff);	// cancel
					ops1[2] = (byte)(messages[3] >> 8);		// cancel
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
		/// Erforderliche Methode für die Designerunterstützung. 
		/// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
		/// </summary>
		private void InitializeComponent()
		{
			this.pnWiz0x0024 = new System.Windows.Forms.Panel();
			this.tbStrTitle = new System.Windows.Forms.TextBox();
			this.tbStrButton3 = new System.Windows.Forms.TextBox();
			this.tbStrButton2 = new System.Windows.Forms.TextBox();
			this.tbStrButton1 = new System.Windows.Forms.TextBox();
			this.tbStrMessage = new System.Windows.Forms.TextBox();
			this.tbMessage = new System.Windows.Forms.TextBox();
			this.cbTVTitle = new System.Windows.Forms.ComboBox();
			this.cbTVButton3 = new System.Windows.Forms.ComboBox();
			this.cbTVButton2 = new System.Windows.Forms.ComboBox();
			this.cbTVButton1 = new System.Windows.Forms.ComboBox();
			this.cbTVMessage = new System.Windows.Forms.ComboBox();
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
			this.btnStrIcon = new System.Windows.Forms.Button();
			this.tbIconID = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.cbIconType = new System.Windows.Forms.ComboBox();
			this.label4 = new System.Windows.Forms.Label();
			this.cbUTMessage = new System.Windows.Forms.CheckBox();
			this.btnDefMessage = new System.Windows.Forms.Button();
			this.btnStrMessage = new System.Windows.Forms.Button();
			this.cbBlockBHAV = new System.Windows.Forms.CheckBox();
			this.cbScope = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.lbTitle = new System.Windows.Forms.Label();
			this.lbButton3 = new System.Windows.Forms.Label();
			this.lbButton2 = new System.Windows.Forms.Label();
			this.lbButton1 = new System.Windows.Forms.Label();
			this.lbMessage = new System.Windows.Forms.Label();
			this.lbType = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.cbType = new System.Windows.Forms.ComboBox();
			this.lbIconType = new System.Windows.Forms.Label();
			this.cbBlockSim = new System.Windows.Forms.CheckBox();
			this.btnStrButton1 = new System.Windows.Forms.Button();
			this.btnStrButton2 = new System.Windows.Forms.Button();
			this.btnStrButton3 = new System.Windows.Forms.Button();
			this.btnStrTitle = new System.Windows.Forms.Button();
			this.btnDefButton1 = new System.Windows.Forms.Button();
			this.btnDefButton2 = new System.Windows.Forms.Button();
			this.btnDefButton3 = new System.Windows.Forms.Button();
			this.btnDefTitle = new System.Windows.Forms.Button();
			this.cbUTButton1 = new System.Windows.Forms.CheckBox();
			this.cbUTButton2 = new System.Windows.Forms.CheckBox();
			this.cbUTButton3 = new System.Windows.Forms.CheckBox();
			this.cbUTTitle = new System.Windows.Forms.CheckBox();
			this.tbButton1 = new System.Windows.Forms.TextBox();
			this.tbButton2 = new System.Windows.Forms.TextBox();
			this.tbButton3 = new System.Windows.Forms.TextBox();
			this.tbTitle = new System.Windows.Forms.TextBox();
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
			this.pnWiz0x0024.Controls.Add(this.tbStrTitle);
			this.pnWiz0x0024.Controls.Add(this.tbStrButton3);
			this.pnWiz0x0024.Controls.Add(this.tbStrButton2);
			this.pnWiz0x0024.Controls.Add(this.tbStrButton1);
			this.pnWiz0x0024.Controls.Add(this.tbStrMessage);
			this.pnWiz0x0024.Controls.Add(this.tbMessage);
			this.pnWiz0x0024.Controls.Add(this.cbTVTitle);
			this.pnWiz0x0024.Controls.Add(this.cbTVButton3);
			this.pnWiz0x0024.Controls.Add(this.cbTVButton2);
			this.pnWiz0x0024.Controls.Add(this.cbTVButton1);
			this.pnWiz0x0024.Controls.Add(this.cbTVMessage);
			this.pnWiz0x0024.Controls.Add(this.pnLocalVar);
			this.pnWiz0x0024.Controls.Add(this.pnTempVar);
			this.pnWiz0x0024.Controls.Add(this.pnTNS);
			this.pnWiz0x0024.Controls.Add(this.btnStrIcon);
			this.pnWiz0x0024.Controls.Add(this.tbIconID);
			this.pnWiz0x0024.Controls.Add(this.label5);
			this.pnWiz0x0024.Controls.Add(this.cbIconType);
			this.pnWiz0x0024.Controls.Add(this.label4);
			this.pnWiz0x0024.Controls.Add(this.cbUTMessage);
			this.pnWiz0x0024.Controls.Add(this.btnDefMessage);
			this.pnWiz0x0024.Controls.Add(this.btnStrMessage);
			this.pnWiz0x0024.Controls.Add(this.cbBlockBHAV);
			this.pnWiz0x0024.Controls.Add(this.cbScope);
			this.pnWiz0x0024.Controls.Add(this.label3);
			this.pnWiz0x0024.Controls.Add(this.lbTitle);
			this.pnWiz0x0024.Controls.Add(this.lbButton3);
			this.pnWiz0x0024.Controls.Add(this.lbButton2);
			this.pnWiz0x0024.Controls.Add(this.lbButton1);
			this.pnWiz0x0024.Controls.Add(this.lbMessage);
			this.pnWiz0x0024.Controls.Add(this.lbType);
			this.pnWiz0x0024.Controls.Add(this.label1);
			this.pnWiz0x0024.Controls.Add(this.cbType);
			this.pnWiz0x0024.Controls.Add(this.lbIconType);
			this.pnWiz0x0024.Controls.Add(this.cbBlockSim);
			this.pnWiz0x0024.Controls.Add(this.btnStrButton1);
			this.pnWiz0x0024.Controls.Add(this.btnStrButton2);
			this.pnWiz0x0024.Controls.Add(this.btnStrButton3);
			this.pnWiz0x0024.Controls.Add(this.btnStrTitle);
			this.pnWiz0x0024.Controls.Add(this.btnDefButton1);
			this.pnWiz0x0024.Controls.Add(this.btnDefButton2);
			this.pnWiz0x0024.Controls.Add(this.btnDefButton3);
			this.pnWiz0x0024.Controls.Add(this.btnDefTitle);
			this.pnWiz0x0024.Controls.Add(this.cbUTButton1);
			this.pnWiz0x0024.Controls.Add(this.cbUTButton2);
			this.pnWiz0x0024.Controls.Add(this.cbUTButton3);
			this.pnWiz0x0024.Controls.Add(this.cbUTTitle);
			this.pnWiz0x0024.Controls.Add(this.tbButton1);
			this.pnWiz0x0024.Controls.Add(this.tbButton2);
			this.pnWiz0x0024.Controls.Add(this.tbButton3);
			this.pnWiz0x0024.Controls.Add(this.tbTitle);
			this.pnWiz0x0024.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.pnWiz0x0024.Location = new System.Drawing.Point(0, 0);
			this.pnWiz0x0024.Name = "pnWiz0x0024";
			this.pnWiz0x0024.Size = new System.Drawing.Size(632, 280);
			this.pnWiz0x0024.TabIndex = 0;
			// 
			// tbStrTitle
			// 
			this.tbStrTitle.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.tbStrTitle.Location = new System.Drawing.Point(248, 252);
			this.tbStrTitle.Name = "tbStrTitle";
			this.tbStrTitle.ReadOnly = true;
			this.tbStrTitle.Size = new System.Drawing.Size(232, 14);
			this.tbStrTitle.TabIndex = 39;
			this.tbStrTitle.TabStop = false;
			this.tbStrTitle.Text = "Title string";
			// 
			// tbStrButton3
			// 
			this.tbStrButton3.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.tbStrButton3.Location = new System.Drawing.Point(248, 228);
			this.tbStrButton3.Name = "tbStrButton3";
			this.tbStrButton3.ReadOnly = true;
			this.tbStrButton3.Size = new System.Drawing.Size(232, 14);
			this.tbStrButton3.TabIndex = 38;
			this.tbStrButton3.TabStop = false;
			this.tbStrButton3.Text = "Button3 string";
			// 
			// tbStrButton2
			// 
			this.tbStrButton2.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.tbStrButton2.Location = new System.Drawing.Point(248, 204);
			this.tbStrButton2.Name = "tbStrButton2";
			this.tbStrButton2.ReadOnly = true;
			this.tbStrButton2.Size = new System.Drawing.Size(232, 14);
			this.tbStrButton2.TabIndex = 37;
			this.tbStrButton2.TabStop = false;
			this.tbStrButton2.Text = "Button2 string";
			// 
			// tbStrButton1
			// 
			this.tbStrButton1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.tbStrButton1.Location = new System.Drawing.Point(248, 180);
			this.tbStrButton1.Name = "tbStrButton1";
			this.tbStrButton1.ReadOnly = true;
			this.tbStrButton1.Size = new System.Drawing.Size(232, 14);
			this.tbStrButton1.TabIndex = 36;
			this.tbStrButton1.TabStop = false;
			this.tbStrButton1.Text = "Button1 string";
			// 
			// tbStrMessage
			// 
			this.tbStrMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.tbStrMessage.Location = new System.Drawing.Point(248, 156);
			this.tbStrMessage.Name = "tbStrMessage";
			this.tbStrMessage.ReadOnly = true;
			this.tbStrMessage.Size = new System.Drawing.Size(232, 14);
			this.tbStrMessage.TabIndex = 35;
			this.tbStrMessage.TabStop = false;
			this.tbStrMessage.Text = "Message string";
			// 
			// tbMessage
			// 
			this.tbMessage.Location = new System.Drawing.Point(144, 152);
			this.tbMessage.Name = "tbMessage";
			this.tbMessage.Size = new System.Drawing.Size(56, 21);
			this.tbMessage.TabIndex = 8;
			this.tbMessage.Text = "0xDDDD";
			this.tbMessage.Validating += new System.ComponentModel.CancelEventHandler(this.hex16_Validating);
			this.tbMessage.Validated += new System.EventHandler(this.hex16_Validated);
			this.tbMessage.TextChanged += new System.EventHandler(this.hex16_TextChanged);
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
			this.cbTVTitle.Location = new System.Drawing.Point(80, 248);
			this.cbTVTitle.Name = "cbTVTitle";
			this.cbTVTitle.Size = new System.Drawing.Size(40, 21);
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
			this.cbTVButton3.Location = new System.Drawing.Point(80, 224);
			this.cbTVButton3.Name = "cbTVButton3";
			this.cbTVButton3.Size = new System.Drawing.Size(40, 21);
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
			this.cbTVButton2.Location = new System.Drawing.Point(80, 200);
			this.cbTVButton2.Name = "cbTVButton2";
			this.cbTVButton2.Size = new System.Drawing.Size(40, 21);
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
			this.cbTVButton1.Location = new System.Drawing.Point(80, 176);
			this.cbTVButton1.Name = "cbTVButton1";
			this.cbTVButton1.Size = new System.Drawing.Size(40, 21);
			this.cbTVButton1.Sorted = true;
			this.cbTVButton1.TabIndex = 11;
			this.cbTVButton1.SelectedIndexChanged += new System.EventHandler(this.cbTempVar_SelectedIndexChanged);
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
			this.cbTVMessage.Location = new System.Drawing.Point(80, 152);
			this.cbTVMessage.Name = "cbTVMessage";
			this.cbTVMessage.Size = new System.Drawing.Size(40, 21);
			this.cbTVMessage.Sorted = true;
			this.cbTVMessage.TabIndex = 6;
			this.cbTVMessage.SelectedIndexChanged += new System.EventHandler(this.cbTempVar_SelectedIndexChanged);
			// 
			// pnLocalVar
			// 
			this.pnLocalVar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.pnLocalVar.Controls.Add(this.tbLocalVar);
			this.pnLocalVar.Controls.Add(this.label8);
			this.pnLocalVar.Location = new System.Drawing.Point(512, 184);
			this.pnLocalVar.Name = "pnLocalVar";
			this.pnLocalVar.Size = new System.Drawing.Size(112, 24);
			this.pnLocalVar.TabIndex = 32;
			// 
			// tbLocalVar
			// 
			this.tbLocalVar.Location = new System.Drawing.Point(72, 0);
			this.tbLocalVar.MaxLength = 4;
			this.tbLocalVar.Name = "tbLocalVar";
			this.tbLocalVar.Size = new System.Drawing.Size(40, 21);
			this.tbLocalVar.TabIndex = 1;
			this.tbLocalVar.Text = "0xDD";
			this.tbLocalVar.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
			this.tbLocalVar.Validated += new System.EventHandler(this.hex8_Validated);
			this.tbLocalVar.TextChanged += new System.EventHandler(this.hex8_TextChanged);
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label8.Location = new System.Drawing.Point(2, 3);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(63, 17);
			this.label8.TabIndex = 0;
			this.label8.Text = "Local Var";
			// 
			// pnTempVar
			// 
			this.pnTempVar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.pnTempVar.Controls.Add(this.cbTempVar);
			this.pnTempVar.Controls.Add(this.lbTempVar);
			this.pnTempVar.Location = new System.Drawing.Point(512, 160);
			this.pnTempVar.Name = "pnTempVar";
			this.pnTempVar.Size = new System.Drawing.Size(112, 24);
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
			this.cbTempVar.Location = new System.Drawing.Point(72, 0);
			this.cbTempVar.Name = "cbTempVar";
			this.cbTempVar.Size = new System.Drawing.Size(40, 21);
			this.cbTempVar.Sorted = true;
			this.cbTempVar.TabIndex = 1;
			this.cbTempVar.SelectedIndexChanged += new System.EventHandler(this.cbTempVar_SelectedIndexChanged);
			// 
			// lbTempVar
			// 
			this.lbTempVar.AutoSize = true;
			this.lbTempVar.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lbTempVar.Location = new System.Drawing.Point(2, 3);
			this.lbTempVar.Name = "lbTempVar";
			this.lbTempVar.Size = new System.Drawing.Size(65, 17);
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
			this.pnTNS.Location = new System.Drawing.Point(0, 104);
			this.pnTNS.Name = "pnTNS";
			this.pnTNS.Size = new System.Drawing.Size(632, 24);
			this.pnTNS.TabIndex = 5;
			// 
			// tbPriority
			// 
			this.tbPriority.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.tbPriority.Location = new System.Drawing.Point(464, 0);
			this.tbPriority.MaxLength = 4;
			this.tbPriority.Name = "tbPriority";
			this.tbPriority.Size = new System.Drawing.Size(40, 21);
			this.tbPriority.TabIndex = 2;
			this.tbPriority.Text = "0xDD";
			this.tbPriority.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
			this.tbPriority.Validated += new System.EventHandler(this.hex8_Validated);
			this.tbPriority.TextChanged += new System.EventHandler(this.hex8_TextChanged);
			// 
			// label6
			// 
			this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label6.Location = new System.Drawing.Point(407, 3);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(52, 17);
			this.label6.TabIndex = 0;
			this.label6.Text = "Priority";
			// 
			// label7
			// 
			this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label7.AutoSize = true;
			this.label7.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label7.Location = new System.Drawing.Point(515, 3);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(56, 17);
			this.label7.TabIndex = 0;
			this.label7.Text = "Timeout";
			// 
			// tbTimeout
			// 
			this.tbTimeout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.tbTimeout.Location = new System.Drawing.Point(576, 0);
			this.tbTimeout.MaxLength = 4;
			this.tbTimeout.Name = "tbTimeout";
			this.tbTimeout.Size = new System.Drawing.Size(40, 21);
			this.tbTimeout.TabIndex = 3;
			this.tbTimeout.Text = "0xDD";
			this.tbTimeout.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
			this.tbTimeout.Validated += new System.EventHandler(this.hex8_Validated);
			this.tbTimeout.TextChanged += new System.EventHandler(this.hex8_TextChanged);
			// 
			// lbTnsStyle
			// 
			this.lbTnsStyle.AutoSize = true;
			this.lbTnsStyle.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lbTnsStyle.Location = new System.Drawing.Point(2, 3);
			this.lbTnsStyle.Name = "lbTnsStyle";
			this.lbTnsStyle.Size = new System.Drawing.Size(65, 17);
			this.lbTnsStyle.TabIndex = 0;
			this.lbTnsStyle.Text = "TNS Style";
			// 
			// cbTnsStyle
			// 
			this.cbTnsStyle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.cbTnsStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbTnsStyle.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cbTnsStyle.Location = new System.Drawing.Point(72, 0);
			this.cbTnsStyle.Name = "cbTnsStyle";
			this.cbTnsStyle.Size = new System.Drawing.Size(320, 21);
			this.cbTnsStyle.TabIndex = 1;
			// 
			// btnStrIcon
			// 
			this.btnStrIcon.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnStrIcon.Font = new System.Drawing.Font("Webdings", 12F, System.Drawing.FontStyle.Bold);
			this.btnStrIcon.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnStrIcon.Location = new System.Drawing.Point(440, 0);
			this.btnStrIcon.Name = "btnStrIcon";
			this.btnStrIcon.Size = new System.Drawing.Size(21, 21);
			this.btnStrIcon.TabIndex = 4;
			this.btnStrIcon.Text = "8";
			this.btnStrIcon.Click += new System.EventHandler(this.btnStr_Click);
			// 
			// tbIconID
			// 
			this.tbIconID.Location = new System.Drawing.Point(400, 0);
			this.tbIconID.MaxLength = 4;
			this.tbIconID.Name = "tbIconID";
			this.tbIconID.Size = new System.Drawing.Size(40, 21);
			this.tbIconID.TabIndex = 3;
			this.tbIconID.Text = "0xDD";
			this.tbIconID.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
			this.tbIconID.Validated += new System.EventHandler(this.hex8_TextChanged);
			this.tbIconID.TextChanged += new System.EventHandler(this.hex8_TextChanged);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label5.Location = new System.Drawing.Point(381, 3);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(14, 17);
			this.label5.TabIndex = 0;
			this.label5.Text = "#";
			// 
			// cbIconType
			// 
			this.cbIconType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbIconType.DropDownWidth = 120;
			this.cbIconType.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cbIconType.Location = new System.Drawing.Point(296, 0);
			this.cbIconType.Name = "cbIconType";
			this.cbIconType.Size = new System.Drawing.Size(80, 21);
			this.cbIconType.TabIndex = 2;
			this.cbIconType.SelectedIndexChanged += new System.EventHandler(this.cbIconType_SelectedIndexChanged);
			// 
			// label4
			// 
			this.label4.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label4.Location = new System.Drawing.Point(88, 131);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(68, 16);
			this.label4.TabIndex = 0;
			this.label4.Text = "Use temp";
			this.label4.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// cbUTMessage
			// 
			this.cbUTMessage.Location = new System.Drawing.Point(120, 155);
			this.cbUTMessage.Name = "cbUTMessage";
			this.cbUTMessage.Size = new System.Drawing.Size(22, 16);
			this.cbUTMessage.TabIndex = 7;
			this.cbUTMessage.Text = "-";
			this.cbUTMessage.CheckedChanged += new System.EventHandler(this.cbUT_CheckedChanged);
			// 
			// btnDefMessage
			// 
			this.btnDefMessage.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnDefMessage.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Bold);
			this.btnDefMessage.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnDefMessage.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnDefMessage.Location = new System.Drawing.Point(224, 152);
			this.btnDefMessage.Name = "btnDefMessage";
			this.btnDefMessage.Size = new System.Drawing.Size(21, 21);
			this.btnDefMessage.TabIndex = 10;
			this.btnDefMessage.Text = "X";
			this.btnDefMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnDefMessage.Click += new System.EventHandler(this.btnDef_Click);
			// 
			// btnStrMessage
			// 
			this.btnStrMessage.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnStrMessage.Font = new System.Drawing.Font("Webdings", 12F, System.Drawing.FontStyle.Bold);
			this.btnStrMessage.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnStrMessage.Location = new System.Drawing.Point(200, 152);
			this.btnStrMessage.Name = "btnStrMessage";
			this.btnStrMessage.Size = new System.Drawing.Size(21, 21);
			this.btnStrMessage.TabIndex = 9;
			this.btnStrMessage.Text = "8";
			this.btnStrMessage.Click += new System.EventHandler(this.btnStr_Click);
			// 
			// cbBlockBHAV
			// 
			this.cbBlockBHAV.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cbBlockBHAV.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.cbBlockBHAV.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
			this.cbBlockBHAV.Location = new System.Drawing.Point(488, 208);
			this.cbBlockBHAV.Name = "cbBlockBHAV";
			this.cbBlockBHAV.Size = new System.Drawing.Size(112, 24);
			this.cbBlockBHAV.TabIndex = 33;
			this.cbBlockBHAV.Text = "Wait for user";
			this.cbBlockBHAV.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.cbBlockBHAV.CheckedChanged += new System.EventHandler(this.cbBlockBHAV_CheckedChanged);
			// 
			// cbScope
			// 
			this.cbScope.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbScope.Items.AddRange(new object[] {
														 "Global",
														 "Semi-global",
														 "Private"});
			this.cbScope.Location = new System.Drawing.Point(280, 128);
			this.cbScope.Name = "cbScope";
			this.cbScope.Size = new System.Drawing.Size(144, 21);
			this.cbScope.TabIndex = 5;
			this.cbScope.SelectedIndexChanged += new System.EventHandler(this.cbScope_SelectedIndexChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label3.Location = new System.Drawing.Point(190, 132);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(85, 17);
			this.label3.TabIndex = 0;
			this.label3.Text = "String Scope";
			// 
			// lbTitle
			// 
			this.lbTitle.AutoSize = true;
			this.lbTitle.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lbTitle.Location = new System.Drawing.Point(43, 251);
			this.lbTitle.Name = "lbTitle";
			this.lbTitle.Size = new System.Drawing.Size(32, 17);
			this.lbTitle.TabIndex = 0;
			this.lbTitle.Text = "Title";
			// 
			// lbButton3
			// 
			this.lbButton3.AutoSize = true;
			this.lbButton3.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lbButton3.Location = new System.Drawing.Point(15, 227);
			this.lbButton3.Name = "lbButton3";
			this.lbButton3.Size = new System.Drawing.Size(59, 17);
			this.lbButton3.TabIndex = 0;
			this.lbButton3.Text = "Button 3";
			// 
			// lbButton2
			// 
			this.lbButton2.AutoSize = true;
			this.lbButton2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lbButton2.Location = new System.Drawing.Point(15, 203);
			this.lbButton2.Name = "lbButton2";
			this.lbButton2.Size = new System.Drawing.Size(59, 17);
			this.lbButton2.TabIndex = 0;
			this.lbButton2.Text = "Button 2";
			// 
			// lbButton1
			// 
			this.lbButton1.AutoSize = true;
			this.lbButton1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lbButton1.Location = new System.Drawing.Point(15, 179);
			this.lbButton1.Name = "lbButton1";
			this.lbButton1.Size = new System.Drawing.Size(59, 17);
			this.lbButton1.TabIndex = 0;
			this.lbButton1.Text = "Button 1";
			// 
			// lbMessage
			// 
			this.lbMessage.AutoSize = true;
			this.lbMessage.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lbMessage.Location = new System.Drawing.Point(16, 155);
			this.lbMessage.Name = "lbMessage";
			this.lbMessage.Size = new System.Drawing.Size(59, 17);
			this.lbMessage.TabIndex = 0;
			this.lbMessage.Text = "Message";
			// 
			// lbType
			// 
			this.lbType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lbType.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lbType.Location = new System.Drawing.Point(0, 32);
			this.lbType.Name = "lbType";
			this.lbType.Size = new System.Drawing.Size(632, 72);
			this.lbType.TabIndex = 0;
			this.lbType.Text = "Description of dialog type";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.Location = new System.Drawing.Point(4, 3);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(79, 17);
			this.label1.TabIndex = 0;
			this.label1.Text = "Dialog Type";
			// 
			// cbType
			// 
			this.cbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbType.DropDownWidth = 160;
			this.cbType.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cbType.Location = new System.Drawing.Point(88, 0);
			this.cbType.Name = "cbType";
			this.cbType.Size = new System.Drawing.Size(160, 21);
			this.cbType.TabIndex = 1;
			this.cbType.SelectedIndexChanged += new System.EventHandler(this.cbType_SelectedIndexChanged);
			// 
			// lbIconType
			// 
			this.lbIconType.AutoSize = true;
			this.lbIconType.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lbIconType.Location = new System.Drawing.Point(258, 3);
			this.lbIconType.Name = "lbIconType";
			this.lbIconType.Size = new System.Drawing.Size(33, 17);
			this.lbIconType.TabIndex = 0;
			this.lbIconType.Text = "Icon";
			// 
			// cbBlockSim
			// 
			this.cbBlockSim.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cbBlockSim.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.cbBlockSim.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
			this.cbBlockSim.Location = new System.Drawing.Point(488, 232);
			this.cbBlockSim.Name = "cbBlockSim";
			this.cbBlockSim.Size = new System.Drawing.Size(112, 24);
			this.cbBlockSim.TabIndex = 34;
			this.cbBlockSim.Text = "Block Sim";
			this.cbBlockSim.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.cbBlockSim.CheckedChanged += new System.EventHandler(this.cbBlockSim_CheckedChanged);
			// 
			// btnStrButton1
			// 
			this.btnStrButton1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnStrButton1.Font = new System.Drawing.Font("Webdings", 12F, System.Drawing.FontStyle.Bold);
			this.btnStrButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnStrButton1.Location = new System.Drawing.Point(200, 176);
			this.btnStrButton1.Name = "btnStrButton1";
			this.btnStrButton1.Size = new System.Drawing.Size(21, 21);
			this.btnStrButton1.TabIndex = 14;
			this.btnStrButton1.Text = "8";
			this.btnStrButton1.Click += new System.EventHandler(this.btnStr_Click);
			// 
			// btnStrButton2
			// 
			this.btnStrButton2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnStrButton2.Font = new System.Drawing.Font("Webdings", 12F, System.Drawing.FontStyle.Bold);
			this.btnStrButton2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnStrButton2.Location = new System.Drawing.Point(200, 200);
			this.btnStrButton2.Name = "btnStrButton2";
			this.btnStrButton2.Size = new System.Drawing.Size(21, 21);
			this.btnStrButton2.TabIndex = 19;
			this.btnStrButton2.Text = "8";
			this.btnStrButton2.Click += new System.EventHandler(this.btnStr_Click);
			// 
			// btnStrButton3
			// 
			this.btnStrButton3.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnStrButton3.Font = new System.Drawing.Font("Webdings", 12F, System.Drawing.FontStyle.Bold);
			this.btnStrButton3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnStrButton3.Location = new System.Drawing.Point(200, 224);
			this.btnStrButton3.Name = "btnStrButton3";
			this.btnStrButton3.Size = new System.Drawing.Size(21, 21);
			this.btnStrButton3.TabIndex = 24;
			this.btnStrButton3.Text = "8";
			this.btnStrButton3.Click += new System.EventHandler(this.btnStr_Click);
			// 
			// btnStrTitle
			// 
			this.btnStrTitle.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnStrTitle.Font = new System.Drawing.Font("Webdings", 12F, System.Drawing.FontStyle.Bold);
			this.btnStrTitle.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnStrTitle.Location = new System.Drawing.Point(200, 248);
			this.btnStrTitle.Name = "btnStrTitle";
			this.btnStrTitle.Size = new System.Drawing.Size(21, 21);
			this.btnStrTitle.TabIndex = 29;
			this.btnStrTitle.Text = "8";
			this.btnStrTitle.Click += new System.EventHandler(this.btnStr_Click);
			// 
			// btnDefButton1
			// 
			this.btnDefButton1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnDefButton1.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Bold);
			this.btnDefButton1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnDefButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnDefButton1.Location = new System.Drawing.Point(224, 176);
			this.btnDefButton1.Name = "btnDefButton1";
			this.btnDefButton1.Size = new System.Drawing.Size(21, 21);
			this.btnDefButton1.TabIndex = 15;
			this.btnDefButton1.Text = "X";
			this.btnDefButton1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnDefButton1.Click += new System.EventHandler(this.btnDef_Click);
			// 
			// btnDefButton2
			// 
			this.btnDefButton2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnDefButton2.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Bold);
			this.btnDefButton2.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnDefButton2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnDefButton2.Location = new System.Drawing.Point(224, 200);
			this.btnDefButton2.Name = "btnDefButton2";
			this.btnDefButton2.Size = new System.Drawing.Size(21, 21);
			this.btnDefButton2.TabIndex = 20;
			this.btnDefButton2.Text = "X";
			this.btnDefButton2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnDefButton2.Click += new System.EventHandler(this.btnDef_Click);
			// 
			// btnDefButton3
			// 
			this.btnDefButton3.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnDefButton3.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Bold);
			this.btnDefButton3.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnDefButton3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnDefButton3.Location = new System.Drawing.Point(224, 224);
			this.btnDefButton3.Name = "btnDefButton3";
			this.btnDefButton3.Size = new System.Drawing.Size(21, 21);
			this.btnDefButton3.TabIndex = 25;
			this.btnDefButton3.Text = "X";
			this.btnDefButton3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnDefButton3.Click += new System.EventHandler(this.btnDef_Click);
			// 
			// btnDefTitle
			// 
			this.btnDefTitle.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnDefTitle.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Bold);
			this.btnDefTitle.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnDefTitle.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnDefTitle.Location = new System.Drawing.Point(224, 248);
			this.btnDefTitle.Name = "btnDefTitle";
			this.btnDefTitle.Size = new System.Drawing.Size(21, 21);
			this.btnDefTitle.TabIndex = 30;
			this.btnDefTitle.Text = "X";
			this.btnDefTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnDefTitle.Click += new System.EventHandler(this.btnDef_Click);
			// 
			// cbUTButton1
			// 
			this.cbUTButton1.Location = new System.Drawing.Point(120, 179);
			this.cbUTButton1.Name = "cbUTButton1";
			this.cbUTButton1.Size = new System.Drawing.Size(22, 16);
			this.cbUTButton1.TabIndex = 12;
			this.cbUTButton1.Text = "-";
			this.cbUTButton1.CheckedChanged += new System.EventHandler(this.cbUT_CheckedChanged);
			// 
			// cbUTButton2
			// 
			this.cbUTButton2.Location = new System.Drawing.Point(120, 203);
			this.cbUTButton2.Name = "cbUTButton2";
			this.cbUTButton2.Size = new System.Drawing.Size(22, 16);
			this.cbUTButton2.TabIndex = 17;
			this.cbUTButton2.Text = "-";
			this.cbUTButton2.CheckedChanged += new System.EventHandler(this.cbUT_CheckedChanged);
			// 
			// cbUTButton3
			// 
			this.cbUTButton3.Location = new System.Drawing.Point(120, 227);
			this.cbUTButton3.Name = "cbUTButton3";
			this.cbUTButton3.Size = new System.Drawing.Size(22, 16);
			this.cbUTButton3.TabIndex = 22;
			this.cbUTButton3.Text = "-";
			this.cbUTButton3.CheckedChanged += new System.EventHandler(this.cbUT_CheckedChanged);
			// 
			// cbUTTitle
			// 
			this.cbUTTitle.Location = new System.Drawing.Point(120, 251);
			this.cbUTTitle.Name = "cbUTTitle";
			this.cbUTTitle.Size = new System.Drawing.Size(22, 16);
			this.cbUTTitle.TabIndex = 27;
			this.cbUTTitle.Text = "-";
			this.cbUTTitle.CheckedChanged += new System.EventHandler(this.cbUT_CheckedChanged);
			// 
			// tbButton1
			// 
			this.tbButton1.Location = new System.Drawing.Point(144, 176);
			this.tbButton1.Name = "tbButton1";
			this.tbButton1.Size = new System.Drawing.Size(56, 21);
			this.tbButton1.TabIndex = 13;
			this.tbButton1.Text = "0xDDDD";
			this.tbButton1.Validating += new System.ComponentModel.CancelEventHandler(this.hex16_Validating);
			this.tbButton1.Validated += new System.EventHandler(this.hex16_Validated);
			this.tbButton1.TextChanged += new System.EventHandler(this.hex16_TextChanged);
			// 
			// tbButton2
			// 
			this.tbButton2.Location = new System.Drawing.Point(144, 200);
			this.tbButton2.Name = "tbButton2";
			this.tbButton2.Size = new System.Drawing.Size(56, 21);
			this.tbButton2.TabIndex = 18;
			this.tbButton2.Text = "0xDDDD";
			this.tbButton2.Validating += new System.ComponentModel.CancelEventHandler(this.hex16_Validating);
			this.tbButton2.Validated += new System.EventHandler(this.hex16_Validated);
			this.tbButton2.TextChanged += new System.EventHandler(this.hex16_TextChanged);
			// 
			// tbButton3
			// 
			this.tbButton3.Location = new System.Drawing.Point(144, 224);
			this.tbButton3.Name = "tbButton3";
			this.tbButton3.Size = new System.Drawing.Size(56, 21);
			this.tbButton3.TabIndex = 23;
			this.tbButton3.Text = "0xDDDD";
			this.tbButton3.Validating += new System.ComponentModel.CancelEventHandler(this.hex16_Validating);
			this.tbButton3.Validated += new System.EventHandler(this.hex16_Validated);
			this.tbButton3.TextChanged += new System.EventHandler(this.hex16_TextChanged);
			// 
			// tbTitle
			// 
			this.tbTitle.Location = new System.Drawing.Point(144, 248);
			this.tbTitle.Name = "tbTitle";
			this.tbTitle.Size = new System.Drawing.Size(56, 21);
			this.tbTitle.TabIndex = 28;
			this.tbTitle.Text = "0xDDDD";
			this.tbTitle.Validating += new System.ComponentModel.CancelEventHandler(this.hex16_Validating);
			this.tbTitle.Validated += new System.EventHandler(this.hex16_Validated);
			this.tbTitle.TextChanged += new System.EventHandler(this.hex16_TextChanged);
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label2.Location = new System.Drawing.Point(696, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(160, 64);
			this.label2.TabIndex = 1;
			this.label2.Text = "see edithWiki AkeaPostMortem for a nice Edith DialogEditor screenshot";
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(632, 280);
			this.button1.Name = "button1";
			this.button1.TabIndex = 2;
			this.button1.Text = "button1";
			// 
			// UI
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(856, 805);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.pnWiz0x0024);
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Name = "UI";
			this.Text = "UI";
			this.pnWiz0x0024.ResumeLayout(false);
			this.pnLocalVar.ResumeLayout(false);
			this.pnTempVar.ResumeLayout(false);
			this.pnTNS.ResumeLayout(false);
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
		public BhavOperandWiz0x0024() : base() { }

		public BhavOperandWiz0x0024(Instruction i) : base(i) { }


		private Wiz0x0024.UI myForm = null;
		public override Panel bhavPrimWizPanel
		{
			get
			{
				if (myForm == null) myForm = new Wiz0x0024.UI();
				return myForm.pnWiz0x0024;
			}
		}

		public override void Execute()
		{
			if (instruction != null) myForm.Execute(instruction);
		}

		public override Instruction Write()
		{
			return (instruction == null) ? null : myForm.Write(instruction);
		}


		#region IDisposable Members
		public override void Dispose()
		{
			if (myForm != null) myForm = null;
		}
		#endregion

	}

}
