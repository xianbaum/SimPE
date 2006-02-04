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
		private System.Windows.Forms.Label lbYes;
		private System.Windows.Forms.Label lbNo;
		private System.Windows.Forms.Label lbCancel;
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
		private System.Windows.Forms.TextBox tbMessage;
		private System.Windows.Forms.TextBox tbButton1;
		private System.Windows.Forms.TextBox tbButton2;
		private System.Windows.Forms.TextBox tbButton3;
		private System.Windows.Forms.TextBox tbTitle;
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
		private System.Windows.Forms.NumericUpDown udLocalVar;
		private System.Windows.Forms.ComboBox cbTempVar;
		private System.Windows.Forms.ComboBox cbTVMessage;
		private System.Windows.Forms.ComboBox cbTVButton1;
		private System.Windows.Forms.ComboBox cbTVButton2;
		private System.Windows.Forms.ComboBox cbTVButton3;
		private System.Windows.Forms.ComboBox cbTVTitle;
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

			TextBox[] t = { tbMessage ,tbButton1 ,tbButton2 ,tbButton3 ,tbTitle ,};
			alTextBox = new ArrayList(t);

			CheckBox[] c = { cbUTMessage ,cbUTButton1 ,cbUTButton2 ,cbUTButton3 ,cbUTTitle ,};
			alCBUseTemp = new ArrayList(c);

			ComboBox[] ct = { cbTVMessage ,cbTVButton1 ,cbTVButton2 ,cbTVButton3 ,cbTVTitle ,};
			alCBTempVar = new ArrayList(ct);
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

		bool internalchg = false;

		private void setType(int newType)
		{
			internalchg = true;

			dialog = (byte)newType;

			if (dialog != cbType.SelectedIndex)
				cbType.SelectedIndex = (cbType.Items.Count > dialog) ? dialog : -1;

			this.lbType.Text = typeDescriptions.Length > dialog ? typeDescriptions[dialog] : "";

			bool[] states = { false, false, false, false, false }; // message, yes, no, cancel, title
			bool tvState = false;
			bool tnsState = false;
			bool lvState = false;

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

			// Make the display match the help text
			for(int i = 0; i < states.Length; i++)
				((Button)this.alStrBtn[i]).Enabled =
					((Button)this.alDefBtn[i]).Enabled =
					((CheckBox)this.alCBUseTemp[i]).Enabled = states[i];
			this.pnTempVar.Visible  = tvState;
			this.pnTNS.Visible      = tnsState;
			this.pnLocalVar.Visible = lvState;

			internalchg = false;
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

		private void setIconID(int i)
		{
			iconID = (byte)i;
			tbIconID.Text = "0x" + SimPe.Helper.HexString(iconID);
		}

		private void setString(int which, int strnum)
		{
			messages[which] = (ushort)strnum;

			if (useTemp[which])
			{
				((TextBox)alTextBox[which]).Visible = false;
				((Button)alDefBtn[which]).Enabled = false;
				((Button)alStrBtn[which]).Enabled = false;

				ComboBox c = (ComboBox)alCBTempVar[which];

				internalchg = true;
				c.SelectedIndex = c.Items.Count > strnum ? strnum : -1;
				internalchg = false;

				c.Visible = true;
			}
			else
			{
				((ComboBox)this.alCBTempVar[which]).Visible = false;
				((Button)alDefBtn[which]).Enabled = (strnum != 0);
				((Button)alStrBtn[which]).Enabled = true;

				TextBox t = (TextBox)alTextBox[which];
				t.Text = (strnum <= 0)
					? "[none]"
					: ((BhavWiz)inst).readStr(scope, GS.GlobalStr.DialogString, strnum - 1, 60, pjse.Detail.Errors)
					;
				t.Visible = true;
			}
		}

		private void setUseTemp(int which, bool newFlag)
		{
			useTemp[which] = newFlag;
			setString(which, messages[which]);
		}

		private void setPriority(int newPriority)
		{
			internalchg = true;

			priority = (byte)newPriority;
			this.tbPriority.Text = "0x" + SimPe.Helper.HexString((byte)newPriority);

			internalchg = false;
		}

		private void setTimeout(int newTimeout)
		{
			internalchg = true;

			timeout = (byte)newTimeout;
			this.tbTimeout.Text = "0x" + SimPe.Helper.HexString((byte)newTimeout);

			internalchg = false;
		}

		private void setLocalVar(int newLocalVar)
		{
			internalchg = true;

			localVar = (byte)newLocalVar;
			this.udLocalVar.Value = localVar;

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
			if (i > 0)
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
				setString(0, BhavWiz.ToShort(ops2[5], ops1[6]));	// message
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
			this.pnLocalVar = new System.Windows.Forms.Panel();
			this.label8 = new System.Windows.Forms.Label();
			this.udLocalVar = new System.Windows.Forms.NumericUpDown();
			this.pnTempVar = new System.Windows.Forms.Panel();
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
			this.tbMessage = new System.Windows.Forms.TextBox();
			this.btnStrMessage = new System.Windows.Forms.Button();
			this.cbBlockBHAV = new System.Windows.Forms.CheckBox();
			this.cbScope = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.lbTitle = new System.Windows.Forms.Label();
			this.lbCancel = new System.Windows.Forms.Label();
			this.lbNo = new System.Windows.Forms.Label();
			this.lbYes = new System.Windows.Forms.Label();
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
			this.tbButton1 = new System.Windows.Forms.TextBox();
			this.tbButton2 = new System.Windows.Forms.TextBox();
			this.tbButton3 = new System.Windows.Forms.TextBox();
			this.tbTitle = new System.Windows.Forms.TextBox();
			this.btnDefButton1 = new System.Windows.Forms.Button();
			this.btnDefButton2 = new System.Windows.Forms.Button();
			this.btnDefButton3 = new System.Windows.Forms.Button();
			this.btnDefTitle = new System.Windows.Forms.Button();
			this.cbUTButton1 = new System.Windows.Forms.CheckBox();
			this.cbUTButton2 = new System.Windows.Forms.CheckBox();
			this.cbUTButton3 = new System.Windows.Forms.CheckBox();
			this.cbUTTitle = new System.Windows.Forms.CheckBox();
			this.label2 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.cbTempVar = new System.Windows.Forms.ComboBox();
			this.cbTVMessage = new System.Windows.Forms.ComboBox();
			this.cbTVButton1 = new System.Windows.Forms.ComboBox();
			this.cbTVButton2 = new System.Windows.Forms.ComboBox();
			this.cbTVButton3 = new System.Windows.Forms.ComboBox();
			this.cbTVTitle = new System.Windows.Forms.ComboBox();
			this.pnWiz0x0024.SuspendLayout();
			this.pnLocalVar.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.udLocalVar)).BeginInit();
			this.pnTempVar.SuspendLayout();
			this.pnTNS.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnWiz0x0024
			// 
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
			this.pnWiz0x0024.Controls.Add(this.tbMessage);
			this.pnWiz0x0024.Controls.Add(this.btnStrMessage);
			this.pnWiz0x0024.Controls.Add(this.cbBlockBHAV);
			this.pnWiz0x0024.Controls.Add(this.cbScope);
			this.pnWiz0x0024.Controls.Add(this.label3);
			this.pnWiz0x0024.Controls.Add(this.lbTitle);
			this.pnWiz0x0024.Controls.Add(this.lbCancel);
			this.pnWiz0x0024.Controls.Add(this.lbNo);
			this.pnWiz0x0024.Controls.Add(this.lbYes);
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
			this.pnWiz0x0024.Controls.Add(this.tbButton1);
			this.pnWiz0x0024.Controls.Add(this.tbButton2);
			this.pnWiz0x0024.Controls.Add(this.tbButton3);
			this.pnWiz0x0024.Controls.Add(this.tbTitle);
			this.pnWiz0x0024.Controls.Add(this.btnDefButton1);
			this.pnWiz0x0024.Controls.Add(this.btnDefButton2);
			this.pnWiz0x0024.Controls.Add(this.btnDefButton3);
			this.pnWiz0x0024.Controls.Add(this.btnDefTitle);
			this.pnWiz0x0024.Controls.Add(this.cbUTButton1);
			this.pnWiz0x0024.Controls.Add(this.cbUTButton2);
			this.pnWiz0x0024.Controls.Add(this.cbUTButton3);
			this.pnWiz0x0024.Controls.Add(this.cbUTTitle);
			this.pnWiz0x0024.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.pnWiz0x0024.Location = new System.Drawing.Point(0, 0);
			this.pnWiz0x0024.Name = "pnWiz0x0024";
			this.pnWiz0x0024.Size = new System.Drawing.Size(528, 280);
			this.pnWiz0x0024.TabIndex = 0;
			// 
			// pnLocalVar
			// 
			this.pnLocalVar.Controls.Add(this.label8);
			this.pnLocalVar.Controls.Add(this.udLocalVar);
			this.pnLocalVar.Location = new System.Drawing.Point(408, 184);
			this.pnLocalVar.Name = "pnLocalVar";
			this.pnLocalVar.Size = new System.Drawing.Size(112, 24);
			this.pnLocalVar.TabIndex = 24;
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
			// udLocalVar
			// 
			this.udLocalVar.Hexadecimal = true;
			this.udLocalVar.Location = new System.Drawing.Point(72, 0);
			this.udLocalVar.Maximum = new System.Decimal(new int[] {
																	   255,
																	   0,
																	   0,
																	   0});
			this.udLocalVar.Name = "udLocalVar";
			this.udLocalVar.Size = new System.Drawing.Size(40, 21);
			this.udLocalVar.TabIndex = 1;
			this.udLocalVar.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.udLocalVar.Value = new System.Decimal(new int[] {
																	 221,
																	 0,
																	 0,
																	 0});
			this.udLocalVar.ValueChanged += new System.EventHandler(this.udLocalVar_ValueChanged);
			// 
			// pnTempVar
			// 
			this.pnTempVar.Controls.Add(this.cbTempVar);
			this.pnTempVar.Controls.Add(this.lbTempVar);
			this.pnTempVar.Location = new System.Drawing.Point(408, 160);
			this.pnTempVar.Name = "pnTempVar";
			this.pnTempVar.Size = new System.Drawing.Size(112, 24);
			this.pnTempVar.TabIndex = 23;
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
			this.pnTNS.Controls.Add(this.tbPriority);
			this.pnTNS.Controls.Add(this.label6);
			this.pnTNS.Controls.Add(this.label7);
			this.pnTNS.Controls.Add(this.tbTimeout);
			this.pnTNS.Controls.Add(this.lbTnsStyle);
			this.pnTNS.Controls.Add(this.cbTnsStyle);
			this.pnTNS.Location = new System.Drawing.Point(0, 104);
			this.pnTNS.Name = "pnTNS";
			this.pnTNS.Size = new System.Drawing.Size(528, 24);
			this.pnTNS.TabIndex = 6;
			// 
			// tbPriority
			// 
			this.tbPriority.Enabled = false;
			this.tbPriority.Location = new System.Drawing.Point(360, 0);
			this.tbPriority.MaxLength = 4;
			this.tbPriority.Name = "tbPriority";
			this.tbPriority.Size = new System.Drawing.Size(40, 21);
			this.tbPriority.TabIndex = 2;
			this.tbPriority.Text = "0xDD";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label6.Location = new System.Drawing.Point(303, 3);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(52, 17);
			this.label6.TabIndex = 0;
			this.label6.Text = "Priority";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label7.Location = new System.Drawing.Point(411, 3);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(56, 17);
			this.label7.TabIndex = 0;
			this.label7.Text = "Timeout";
			// 
			// tbTimeout
			// 
			this.tbTimeout.Enabled = false;
			this.tbTimeout.Location = new System.Drawing.Point(472, 0);
			this.tbTimeout.MaxLength = 4;
			this.tbTimeout.Name = "tbTimeout";
			this.tbTimeout.Size = new System.Drawing.Size(40, 21);
			this.tbTimeout.TabIndex = 3;
			this.tbTimeout.Text = "0xDD";
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
			this.cbTnsStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbTnsStyle.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cbTnsStyle.Location = new System.Drawing.Point(72, 0);
			this.cbTnsStyle.Name = "cbTnsStyle";
			this.cbTnsStyle.Size = new System.Drawing.Size(216, 21);
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
			this.label4.Location = new System.Drawing.Point(334, 134);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(68, 16);
			this.label4.TabIndex = 0;
			this.label4.Text = "Use temp";
			this.label4.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// cbUTMessage
			// 
			this.cbUTMessage.Location = new System.Drawing.Point(360, 155);
			this.cbUTMessage.Name = "cbUTMessage";
			this.cbUTMessage.Size = new System.Drawing.Size(22, 16);
			this.cbUTMessage.TabIndex = 10;
			this.cbUTMessage.Text = "-";
			this.cbUTMessage.CheckedChanged += new System.EventHandler(this.cbUT_CheckedChanged);
			// 
			// btnDefMessage
			// 
			this.btnDefMessage.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnDefMessage.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Bold);
			this.btnDefMessage.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnDefMessage.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnDefMessage.Location = new System.Drawing.Point(336, 152);
			this.btnDefMessage.Name = "btnDefMessage";
			this.btnDefMessage.Size = new System.Drawing.Size(21, 21);
			this.btnDefMessage.TabIndex = 9;
			this.btnDefMessage.Text = "X";
			this.btnDefMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnDefMessage.Click += new System.EventHandler(this.btnDef_Click);
			// 
			// tbMessage
			// 
			this.tbMessage.Location = new System.Drawing.Point(80, 152);
			this.tbMessage.MaxLength = 6;
			this.tbMessage.Name = "tbMessage";
			this.tbMessage.ReadOnly = true;
			this.tbMessage.Size = new System.Drawing.Size(232, 21);
			this.tbMessage.TabIndex = 0;
			this.tbMessage.TabStop = false;
			this.tbMessage.Text = "tbMessage";
			// 
			// btnStrMessage
			// 
			this.btnStrMessage.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnStrMessage.Font = new System.Drawing.Font("Webdings", 12F, System.Drawing.FontStyle.Bold);
			this.btnStrMessage.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnStrMessage.Location = new System.Drawing.Point(312, 152);
			this.btnStrMessage.Name = "btnStrMessage";
			this.btnStrMessage.Size = new System.Drawing.Size(21, 21);
			this.btnStrMessage.TabIndex = 8;
			this.btnStrMessage.Text = "8";
			this.btnStrMessage.Click += new System.EventHandler(this.btnStr_Click);
			// 
			// cbBlockBHAV
			// 
			this.cbBlockBHAV.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.cbBlockBHAV.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
			this.cbBlockBHAV.Location = new System.Drawing.Point(384, 208);
			this.cbBlockBHAV.Name = "cbBlockBHAV";
			this.cbBlockBHAV.Size = new System.Drawing.Size(112, 24);
			this.cbBlockBHAV.TabIndex = 25;
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
			this.cbScope.Location = new System.Drawing.Point(104, 128);
			this.cbScope.Name = "cbScope";
			this.cbScope.Size = new System.Drawing.Size(144, 21);
			this.cbScope.TabIndex = 7;
			this.cbScope.SelectedIndexChanged += new System.EventHandler(this.cbScope_SelectedIndexChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label3.Location = new System.Drawing.Point(14, 131);
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
			this.lbTitle.TabIndex = 9;
			this.lbTitle.Text = "Title";
			// 
			// lbCancel
			// 
			this.lbCancel.AutoSize = true;
			this.lbCancel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lbCancel.Location = new System.Drawing.Point(15, 227);
			this.lbCancel.Name = "lbCancel";
			this.lbCancel.Size = new System.Drawing.Size(59, 17);
			this.lbCancel.TabIndex = 8;
			this.lbCancel.Text = "Button 3";
			// 
			// lbNo
			// 
			this.lbNo.AutoSize = true;
			this.lbNo.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lbNo.Location = new System.Drawing.Point(15, 203);
			this.lbNo.Name = "lbNo";
			this.lbNo.Size = new System.Drawing.Size(59, 17);
			this.lbNo.TabIndex = 7;
			this.lbNo.Text = "Button 2";
			// 
			// lbYes
			// 
			this.lbYes.AutoSize = true;
			this.lbYes.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lbYes.Location = new System.Drawing.Point(15, 179);
			this.lbYes.Name = "lbYes";
			this.lbYes.Size = new System.Drawing.Size(59, 17);
			this.lbYes.TabIndex = 6;
			this.lbYes.Text = "Button 1";
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
			this.lbType.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lbType.Location = new System.Drawing.Point(0, 32);
			this.lbType.Name = "lbType";
			this.lbType.Size = new System.Drawing.Size(528, 72);
			this.lbType.TabIndex = 5;
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
			this.cbBlockSim.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.cbBlockSim.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
			this.cbBlockSim.Location = new System.Drawing.Point(384, 232);
			this.cbBlockSim.Name = "cbBlockSim";
			this.cbBlockSim.Size = new System.Drawing.Size(112, 24);
			this.cbBlockSim.TabIndex = 26;
			this.cbBlockSim.Text = "Block Sim";
			this.cbBlockSim.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.cbBlockSim.CheckedChanged += new System.EventHandler(this.cbBlockSim_CheckedChanged);
			// 
			// btnStrButton1
			// 
			this.btnStrButton1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnStrButton1.Font = new System.Drawing.Font("Webdings", 12F, System.Drawing.FontStyle.Bold);
			this.btnStrButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnStrButton1.Location = new System.Drawing.Point(312, 176);
			this.btnStrButton1.Name = "btnStrButton1";
			this.btnStrButton1.Size = new System.Drawing.Size(21, 21);
			this.btnStrButton1.TabIndex = 11;
			this.btnStrButton1.Text = "8";
			this.btnStrButton1.Click += new System.EventHandler(this.btnStr_Click);
			// 
			// btnStrButton2
			// 
			this.btnStrButton2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnStrButton2.Font = new System.Drawing.Font("Webdings", 12F, System.Drawing.FontStyle.Bold);
			this.btnStrButton2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnStrButton2.Location = new System.Drawing.Point(312, 200);
			this.btnStrButton2.Name = "btnStrButton2";
			this.btnStrButton2.Size = new System.Drawing.Size(21, 21);
			this.btnStrButton2.TabIndex = 14;
			this.btnStrButton2.Text = "8";
			this.btnStrButton2.Click += new System.EventHandler(this.btnStr_Click);
			// 
			// btnStrButton3
			// 
			this.btnStrButton3.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnStrButton3.Font = new System.Drawing.Font("Webdings", 12F, System.Drawing.FontStyle.Bold);
			this.btnStrButton3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnStrButton3.Location = new System.Drawing.Point(312, 224);
			this.btnStrButton3.Name = "btnStrButton3";
			this.btnStrButton3.Size = new System.Drawing.Size(21, 21);
			this.btnStrButton3.TabIndex = 17;
			this.btnStrButton3.Text = "8";
			this.btnStrButton3.Click += new System.EventHandler(this.btnStr_Click);
			// 
			// btnStrTitle
			// 
			this.btnStrTitle.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnStrTitle.Font = new System.Drawing.Font("Webdings", 12F, System.Drawing.FontStyle.Bold);
			this.btnStrTitle.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnStrTitle.Location = new System.Drawing.Point(312, 248);
			this.btnStrTitle.Name = "btnStrTitle";
			this.btnStrTitle.Size = new System.Drawing.Size(21, 21);
			this.btnStrTitle.TabIndex = 20;
			this.btnStrTitle.Text = "8";
			this.btnStrTitle.Click += new System.EventHandler(this.btnStr_Click);
			// 
			// tbButton1
			// 
			this.tbButton1.Location = new System.Drawing.Point(80, 176);
			this.tbButton1.MaxLength = 6;
			this.tbButton1.Name = "tbButton1";
			this.tbButton1.ReadOnly = true;
			this.tbButton1.Size = new System.Drawing.Size(232, 21);
			this.tbButton1.TabIndex = 0;
			this.tbButton1.TabStop = false;
			this.tbButton1.Text = "tbButton1";
			// 
			// tbButton2
			// 
			this.tbButton2.Location = new System.Drawing.Point(80, 200);
			this.tbButton2.MaxLength = 6;
			this.tbButton2.Name = "tbButton2";
			this.tbButton2.ReadOnly = true;
			this.tbButton2.Size = new System.Drawing.Size(232, 21);
			this.tbButton2.TabIndex = 0;
			this.tbButton2.TabStop = false;
			this.tbButton2.Text = "tbButton2";
			// 
			// tbButton3
			// 
			this.tbButton3.Location = new System.Drawing.Point(80, 224);
			this.tbButton3.MaxLength = 6;
			this.tbButton3.Name = "tbButton3";
			this.tbButton3.ReadOnly = true;
			this.tbButton3.Size = new System.Drawing.Size(232, 21);
			this.tbButton3.TabIndex = 0;
			this.tbButton3.TabStop = false;
			this.tbButton3.Text = "tbButton3";
			// 
			// tbTitle
			// 
			this.tbTitle.Location = new System.Drawing.Point(80, 248);
			this.tbTitle.MaxLength = 6;
			this.tbTitle.Name = "tbTitle";
			this.tbTitle.ReadOnly = true;
			this.tbTitle.Size = new System.Drawing.Size(232, 21);
			this.tbTitle.TabIndex = 0;
			this.tbTitle.TabStop = false;
			this.tbTitle.Text = "tbTitle";
			// 
			// btnDefButton1
			// 
			this.btnDefButton1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnDefButton1.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Bold);
			this.btnDefButton1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnDefButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnDefButton1.Location = new System.Drawing.Point(336, 176);
			this.btnDefButton1.Name = "btnDefButton1";
			this.btnDefButton1.Size = new System.Drawing.Size(21, 21);
			this.btnDefButton1.TabIndex = 12;
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
			this.btnDefButton2.Location = new System.Drawing.Point(336, 200);
			this.btnDefButton2.Name = "btnDefButton2";
			this.btnDefButton2.Size = new System.Drawing.Size(21, 21);
			this.btnDefButton2.TabIndex = 15;
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
			this.btnDefButton3.Location = new System.Drawing.Point(336, 224);
			this.btnDefButton3.Name = "btnDefButton3";
			this.btnDefButton3.Size = new System.Drawing.Size(21, 21);
			this.btnDefButton3.TabIndex = 18;
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
			this.btnDefTitle.Location = new System.Drawing.Point(336, 248);
			this.btnDefTitle.Name = "btnDefTitle";
			this.btnDefTitle.Size = new System.Drawing.Size(21, 21);
			this.btnDefTitle.TabIndex = 21;
			this.btnDefTitle.Text = "X";
			this.btnDefTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnDefTitle.Click += new System.EventHandler(this.btnDef_Click);
			// 
			// cbUTButton1
			// 
			this.cbUTButton1.Location = new System.Drawing.Point(360, 179);
			this.cbUTButton1.Name = "cbUTButton1";
			this.cbUTButton1.Size = new System.Drawing.Size(22, 16);
			this.cbUTButton1.TabIndex = 13;
			this.cbUTButton1.Text = "-";
			this.cbUTButton1.CheckedChanged += new System.EventHandler(this.cbUT_CheckedChanged);
			// 
			// cbUTButton2
			// 
			this.cbUTButton2.Location = new System.Drawing.Point(360, 203);
			this.cbUTButton2.Name = "cbUTButton2";
			this.cbUTButton2.Size = new System.Drawing.Size(22, 16);
			this.cbUTButton2.TabIndex = 16;
			this.cbUTButton2.Text = "-";
			this.cbUTButton2.CheckedChanged += new System.EventHandler(this.cbUT_CheckedChanged);
			// 
			// cbUTButton3
			// 
			this.cbUTButton3.Location = new System.Drawing.Point(360, 227);
			this.cbUTButton3.Name = "cbUTButton3";
			this.cbUTButton3.Size = new System.Drawing.Size(22, 16);
			this.cbUTButton3.TabIndex = 19;
			this.cbUTButton3.Text = "-";
			this.cbUTButton3.CheckedChanged += new System.EventHandler(this.cbUT_CheckedChanged);
			// 
			// cbUTTitle
			// 
			this.cbUTTitle.Location = new System.Drawing.Point(360, 251);
			this.cbUTTitle.Name = "cbUTTitle";
			this.cbUTTitle.Size = new System.Drawing.Size(22, 16);
			this.cbUTTitle.TabIndex = 22;
			this.cbUTTitle.Text = "-";
			this.cbUTTitle.CheckedChanged += new System.EventHandler(this.cbUT_CheckedChanged);
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
			this.button1.Location = new System.Drawing.Point(528, 280);
			this.button1.Name = "button1";
			this.button1.TabIndex = 2;
			this.button1.Text = "button1";
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
			// cbTVMessage
			// 
			this.cbTVMessage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbTVMessage.Items.AddRange(new object[] {
															 "Temp 0",
															 "Temp 1",
															 "Temp 2",
															 "Temp 3",
															 "Temp 4",
															 "Temp 5",
															 "Temp 6",
															 "Temp 7"});
			this.cbTVMessage.Location = new System.Drawing.Point(80, 152);
			this.cbTVMessage.Name = "cbTVMessage";
			this.cbTVMessage.Size = new System.Drawing.Size(72, 21);
			this.cbTVMessage.Sorted = true;
			this.cbTVMessage.TabIndex = 27;
			this.cbTVMessage.SelectedIndexChanged += new System.EventHandler(this.cbTempVar_SelectedIndexChanged);
			// 
			// cbTVButton1
			// 
			this.cbTVButton1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbTVButton1.Items.AddRange(new object[] {
															 "Temp 0",
															 "Temp 1",
															 "Temp 2",
															 "Temp 3",
															 "Temp 4",
															 "Temp 5",
															 "Temp 6",
															 "Temp 7"});
			this.cbTVButton1.Location = new System.Drawing.Point(80, 176);
			this.cbTVButton1.Name = "cbTVButton1";
			this.cbTVButton1.Size = new System.Drawing.Size(72, 21);
			this.cbTVButton1.Sorted = true;
			this.cbTVButton1.TabIndex = 28;
			this.cbTVButton1.SelectedIndexChanged += new System.EventHandler(this.cbTempVar_SelectedIndexChanged);
			// 
			// cbTVButton2
			// 
			this.cbTVButton2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbTVButton2.Items.AddRange(new object[] {
															 "Temp 0",
															 "Temp 1",
															 "Temp 2",
															 "Temp 3",
															 "Temp 4",
															 "Temp 5",
															 "Temp 6",
															 "Temp 7"});
			this.cbTVButton2.Location = new System.Drawing.Point(80, 200);
			this.cbTVButton2.Name = "cbTVButton2";
			this.cbTVButton2.Size = new System.Drawing.Size(72, 21);
			this.cbTVButton2.Sorted = true;
			this.cbTVButton2.TabIndex = 29;
			this.cbTVButton2.SelectedIndexChanged += new System.EventHandler(this.cbTempVar_SelectedIndexChanged);
			// 
			// cbTVButton3
			// 
			this.cbTVButton3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbTVButton3.Items.AddRange(new object[] {
															 "Temp 0",
															 "Temp 1",
															 "Temp 2",
															 "Temp 3",
															 "Temp 4",
															 "Temp 5",
															 "Temp 6",
															 "Temp 7"});
			this.cbTVButton3.Location = new System.Drawing.Point(80, 224);
			this.cbTVButton3.Name = "cbTVButton3";
			this.cbTVButton3.Size = new System.Drawing.Size(72, 21);
			this.cbTVButton3.Sorted = true;
			this.cbTVButton3.TabIndex = 30;
			this.cbTVButton3.SelectedIndexChanged += new System.EventHandler(this.cbTempVar_SelectedIndexChanged);
			// 
			// cbTVTitle
			// 
			this.cbTVTitle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbTVTitle.Items.AddRange(new object[] {
														   "Temp 0",
														   "Temp 1",
														   "Temp 2",
														   "Temp 3",
														   "Temp 4",
														   "Temp 5",
														   "Temp 6",
														   "Temp 7"});
			this.cbTVTitle.Location = new System.Drawing.Point(80, 248);
			this.cbTVTitle.Name = "cbTVTitle";
			this.cbTVTitle.Size = new System.Drawing.Size(72, 21);
			this.cbTVTitle.Sorted = true;
			this.cbTVTitle.TabIndex = 31;
			this.cbTVTitle.SelectedIndexChanged += new System.EventHandler(this.cbTempVar_SelectedIndexChanged);
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
			((System.ComponentModel.ISupportInitialize)(this.udLocalVar)).EndInit();
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

		private void udLocalVar_ValueChanged(object sender, System.EventArgs e)
		{
			if (internalchg) return;

			setLocalVar(Convert.ToInt32(((NumericUpDown)sender).Value));
		}


		private void btnStr_Click(object sender, System.EventArgs e)
		{
			doStrChooser(alStrBtn.IndexOf(sender));
		}

		private void btnDef_Click(object sender, System.EventArgs e)
		{
			this.setString(alDefBtn.IndexOf(sender), 0);
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
