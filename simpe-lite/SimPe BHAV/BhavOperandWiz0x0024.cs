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
		private System.Windows.Forms.ComboBox cbTnsStyle;
		private System.Windows.Forms.Label lbTnsStyle;
		private System.Windows.Forms.Label lbMessage;
		private System.Windows.Forms.Label lbYes;
		private System.Windows.Forms.Label lbNo;
		private System.Windows.Forms.Label lbCancel;
		private System.Windows.Forms.Label lbTitle;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox cbScope;
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
		private Instruction inst = null;
		private static string[] typeDescriptions = null;

		bool nowait   = false;
		byte iconType = 0;
		byte tempVar  = 0;
		bool noblock  = false;
		ushort msg    = 0;
		ushort cnc    = 0;
		byte tnsStyle = 0;
		Scope scope   = Scope.Private;

		private void doType(int newType)
		{
			this.lbType.Text = newType < typeDescriptions.Length ? typeDescriptions[newType] : "";

			if (newType == 0x08 || newType == 0x0a)
			{
				this.lbTnsStyle.Visible = this.cbTnsStyle.Visible = true;
				this.cbTnsStyle.SelectedIndex = this.cbTnsStyle.Items.Count > tnsStyle ? tnsStyle : -1;
			}
			else
			{
				this.lbTnsStyle.Visible = this.cbTnsStyle.Visible = false;
			}

			/*
			06 p/b - invite
			0c jewlery rack
			0d video game rack
			10 clothing purchase
			11 clothing selection
			12 tutorial tasks
			14 clothing try on
			 */
			/*
			08 start text notification
			0a modify t/n
			 */
			/*
			09 stop t/n
			 */
			/*
			0e food rack
			 */
			/*
			00 message
			01 yes-no
			02 yes-no-cancel
			03 text entry
			04 tutorial
			05 phone book - services
			07 p/b - party
			0b magazine rack
			0f special phone services
			13 list selection
			15 Vanity table
			16 Tutorial Next
			17 Baby Name
			18 Set Aspiration
			19 Tutorial Next Modal
			1a Set Major
			1b Resurectonomitron
			1c Fire Forget Append
			1d Move grave to lot
			1e Visit another lot
			1f Dating Services
			20 Manage groups
			21 Phone groups
			22 Set Turn On/Offs
			*/
		}

		private void doTnsStyle(int newStyle)
		{
			tnsStyle = (byte)newStyle;
		}

		private void doScope(int newScope)
		{
			scope = (Scope)newScope;
		}


		public void Execute(Instruction inst)
		{
			this.inst = inst;

			wrappedByteArray ops1 = inst.Operands;
			wrappedByteArray ops2 = inst.Reserved1;

			nowait   = (ops1[7] & 0x01) != 0;
			iconType = (byte)((ops1[7] >> 1) & 0x07);
			tempVar  = (byte)((ops1[7] >> 4) & 0x07);
			noblock  = (ops1[7] & 0x80) != 0;
			tnsStyle = ops2[4];

			if (inst.NodeVersion == 0)
			{
				msg = ops1[2];	// message
				cnc = ops1[0];	// cancel
			} 
			else 
			{
				msg = BhavWiz.ToShort(ops2[5], ops1[6]);	// message
				cnc = BhavWiz.ToShort(ops1[0], ops1[2]);	// cancel
			}

			if      ((ops2[0] & 0x01) != 0) scope = Scope.SemiGlobal;
			else if ((ops2[0] & 0x40) != 0) scope = Scope.Global;
			else                            scope = Scope.Private;

			cbType.SelectedIndex = (cbType.Items.Count > ops1[0x05]) ? ops1[0x05] : -1;
			doType(cbType.SelectedIndex);
			cbScope.SelectedIndex = cbScope.Items.Count > ((int)scope) ? ((int)scope) : -1;
		}


		public Instruction Write(Instruction inst)
		{
			if (inst != null)
			{
				wrappedByteArray ops1 = inst.Operands;
				wrappedByteArray ops2 = inst.Reserved1;

				ops1[0x05] = (byte)cbType.SelectedIndex;
				if (ops1[0x05] == 0x08 || ops1[0x05] == 0x0a)
				{
					ops2[4] = tnsStyle;
				}

				ops2[0] &= 0xbe;
				if      (scope == Scope.SemiGlobal) ops2[0] |= 0x01;
				else if (scope == Scope.Global)     ops2[0] |= 0x40;
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
			this.lbTnsStyle = new System.Windows.Forms.Label();
			this.cbTnsStyle = new System.Windows.Forms.ComboBox();
			this.lbType = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.cbType = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.lbMessage = new System.Windows.Forms.Label();
			this.lbYes = new System.Windows.Forms.Label();
			this.lbNo = new System.Windows.Forms.Label();
			this.lbCancel = new System.Windows.Forms.Label();
			this.lbTitle = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.cbScope = new System.Windows.Forms.ComboBox();
			this.pnWiz0x0024.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnWiz0x0024
			// 
			this.pnWiz0x0024.Controls.Add(this.cbScope);
			this.pnWiz0x0024.Controls.Add(this.label3);
			this.pnWiz0x0024.Controls.Add(this.lbTitle);
			this.pnWiz0x0024.Controls.Add(this.lbCancel);
			this.pnWiz0x0024.Controls.Add(this.lbNo);
			this.pnWiz0x0024.Controls.Add(this.lbYes);
			this.pnWiz0x0024.Controls.Add(this.lbMessage);
			this.pnWiz0x0024.Controls.Add(this.lbTnsStyle);
			this.pnWiz0x0024.Controls.Add(this.cbTnsStyle);
			this.pnWiz0x0024.Controls.Add(this.lbType);
			this.pnWiz0x0024.Controls.Add(this.label1);
			this.pnWiz0x0024.Controls.Add(this.cbType);
			this.pnWiz0x0024.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.pnWiz0x0024.Location = new System.Drawing.Point(0, 0);
			this.pnWiz0x0024.Name = "pnWiz0x0024";
			this.pnWiz0x0024.Size = new System.Drawing.Size(528, 272);
			this.pnWiz0x0024.TabIndex = 0;
			// 
			// lbTnsStyle
			// 
			this.lbTnsStyle.AutoSize = true;
			this.lbTnsStyle.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lbTnsStyle.Location = new System.Drawing.Point(258, 11);
			this.lbTnsStyle.Name = "lbTnsStyle";
			this.lbTnsStyle.Size = new System.Drawing.Size(65, 17);
			this.lbTnsStyle.TabIndex = 4;
			this.lbTnsStyle.Text = "TNS Style";
			// 
			// cbTnsStyle
			// 
			this.cbTnsStyle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.cbTnsStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbTnsStyle.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cbTnsStyle.Location = new System.Drawing.Point(328, 8);
			this.cbTnsStyle.Name = "cbTnsStyle";
			this.cbTnsStyle.Size = new System.Drawing.Size(200, 21);
			this.cbTnsStyle.TabIndex = 3;
			this.cbTnsStyle.SelectedIndexChanged += new System.EventHandler(this.cbTnsStyle_SelectedIndexChanged);
			// 
			// lbType
			// 
			this.lbType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lbType.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lbType.Location = new System.Drawing.Point(0, 32);
			this.lbType.Name = "lbType";
			this.lbType.Size = new System.Drawing.Size(528, 64);
			this.lbType.TabIndex = 2;
			this.lbType.Text = "Description of dialog type";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.Location = new System.Drawing.Point(4, 11);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(79, 17);
			this.label1.TabIndex = 1;
			this.label1.Text = "Dialog Type";
			// 
			// cbType
			// 
			this.cbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbType.DropDownWidth = 160;
			this.cbType.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cbType.Location = new System.Drawing.Point(88, 8);
			this.cbType.Name = "cbType";
			this.cbType.Size = new System.Drawing.Size(160, 21);
			this.cbType.TabIndex = 0;
			this.cbType.SelectedIndexChanged += new System.EventHandler(this.cbType_SelectedIndexChanged);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(672, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(176, 96);
			this.label2.TabIndex = 1;
			this.label2.Text = "see edithWiki AkeaPostMortem for a nice DialogEditor screenshot";
			// 
			// lbMessage
			// 
			this.lbMessage.AutoSize = true;
			this.lbMessage.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lbMessage.Location = new System.Drawing.Point(16, 144);
			this.lbMessage.Name = "lbMessage";
			this.lbMessage.Size = new System.Drawing.Size(59, 17);
			this.lbMessage.TabIndex = 5;
			this.lbMessage.Text = "Message";
			// 
			// lbYes
			// 
			this.lbYes.AutoSize = true;
			this.lbYes.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lbYes.Location = new System.Drawing.Point(16, 168);
			this.lbYes.Name = "lbYes";
			this.lbYes.Size = new System.Drawing.Size(27, 17);
			this.lbYes.TabIndex = 6;
			this.lbYes.Text = "Yes";
			// 
			// lbNo
			// 
			this.lbNo.AutoSize = true;
			this.lbNo.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lbNo.Location = new System.Drawing.Point(16, 192);
			this.lbNo.Name = "lbNo";
			this.lbNo.Size = new System.Drawing.Size(22, 17);
			this.lbNo.TabIndex = 7;
			this.lbNo.Text = "No";
			// 
			// lbCancel
			// 
			this.lbCancel.AutoSize = true;
			this.lbCancel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lbCancel.Location = new System.Drawing.Point(16, 216);
			this.lbCancel.Name = "lbCancel";
			this.lbCancel.Size = new System.Drawing.Size(46, 17);
			this.lbCancel.TabIndex = 8;
			this.lbCancel.Text = "Cancel";
			// 
			// lbTitle
			// 
			this.lbTitle.AutoSize = true;
			this.lbTitle.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lbTitle.Location = new System.Drawing.Point(16, 240);
			this.lbTitle.Name = "lbTitle";
			this.lbTitle.Size = new System.Drawing.Size(32, 17);
			this.lbTitle.TabIndex = 9;
			this.lbTitle.Text = "Title";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label3.Location = new System.Drawing.Point(14, 107);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(85, 17);
			this.label3.TabIndex = 10;
			this.label3.Text = "String Scope";
			// 
			// cbScope
			// 
			this.cbScope.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbScope.Items.AddRange(new object[] {
														 "Global",
														 "Semi-global",
														 "Private"});
			this.cbScope.Location = new System.Drawing.Point(104, 104);
			this.cbScope.Name = "cbScope";
			this.cbScope.Size = new System.Drawing.Size(144, 21);
			this.cbScope.TabIndex = 11;
			this.cbScope.SelectedIndexChanged += new System.EventHandler(this.cbScope_SelectedIndexChanged);
			// 
			// UI
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(856, 605);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.pnWiz0x0024);
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Name = "UI";
			this.Text = "UI";
			this.pnWiz0x0024.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void cbType_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			doType(((ComboBox)sender).SelectedIndex);
		}

		private void cbTnsStyle_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			doTnsStyle(((ComboBox)sender).SelectedIndex);
		}

		private void cbScope_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			doScope(((ComboBox)sender).SelectedIndex);
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
