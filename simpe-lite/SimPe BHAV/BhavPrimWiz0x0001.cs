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
using SimPe.PackedFiles.Wrapper;

namespace SimPe.PackedFiles.UserInterface
{
	#region internal form
	/// <summary>
	/// Zusammenfassung für BhavInstruction.
	/// </summary>
	internal class MyForm0x0001 : System.Windows.Forms.Form
	{
		#region Form variables

		internal System.Windows.Forms.Panel pnWiz0x0001;
		private System.Windows.Forms.ComboBox cbGenericSimsCall;
		private System.Windows.Forms.Label lbGenericSimsCallparms;
		/// <summary>
		/// Erforderliche Designervariable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion

		public MyForm0x0001()
		{
			//
			// Erforderlich für die Windows Form-Designerunterstützung
			//
			InitializeComponent();
		}

		/// <summary>
		/// Die verwendeten Ressourcen bereinigen.
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

		
		#region MyForm0x0001
		private string[] parms;
		private byte operand0;

		public Instruction Write(Instruction inst)
		{
			if (this.cbGenericSimsCall.SelectedIndex >= 0)
				inst.Operands[0] = (byte)this.cbGenericSimsCall.SelectedIndex;
			return inst;
		}

		public void Execute(Instruction inst, string[] genericSimsCall, string[] parms)
		{
			this.operand0 = inst.Operands[0];
			this.parms = parms;

			this.cbGenericSimsCall.Items.Clear();
			for (byte i = 0; i < genericSimsCall.Length; i++)
				this.cbGenericSimsCall.Items.Add("0x" + Helper.HexString(i) + ": " + genericSimsCall[i]);
			this.lbGenericSimsCallparms.Text = "Unknown operand 0x" + Helper.HexString(operand0);

			if (operand0 < genericSimsCall.Length)
				this.cbGenericSimsCall.SelectedIndex = operand0;
			else
				this.cbGenericSimsCall.SelectedIndex = -1;
		}

		#endregion

		#region Vom Windows Form-Designer generierter Code
		/// <summary>
		/// Erforderliche Methode für die Designerunterstützung. 
		/// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
		/// </summary>
		private void InitializeComponent()
		{
			this.pnWiz0x0001 = new System.Windows.Forms.Panel();
			this.cbGenericSimsCall = new System.Windows.Forms.ComboBox();
			this.lbGenericSimsCallparms = new System.Windows.Forms.Label();
			this.pnWiz0x0001.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnWiz0x0001
			// 
			this.pnWiz0x0001.Controls.Add(this.lbGenericSimsCallparms);
			this.pnWiz0x0001.Controls.Add(this.cbGenericSimsCall);
			this.pnWiz0x0001.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.pnWiz0x0001.Location = new System.Drawing.Point(8, 8);
			this.pnWiz0x0001.Name = "pnWiz0x0001";
			this.pnWiz0x0001.Size = new System.Drawing.Size(264, 72);
			this.pnWiz0x0001.TabIndex = 0;
			// 
			// cbGenericSimsCall
			// 
			this.cbGenericSimsCall.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbGenericSimsCall.Location = new System.Drawing.Point(0, 0);
			this.cbGenericSimsCall.Name = "cbGenericSimsCall";
			this.cbGenericSimsCall.Size = new System.Drawing.Size(264, 21);
			this.cbGenericSimsCall.TabIndex = 0;
			this.cbGenericSimsCall.SelectedIndexChanged += new System.EventHandler(this.cbGenericSimsCall_Changed);
			// 
			// lbGenericSimsCallparms
			// 
			this.lbGenericSimsCallparms.Location = new System.Drawing.Point(0, 24);
			this.lbGenericSimsCallparms.Name = "lbGenericSimsCallparms";
			this.lbGenericSimsCallparms.Size = new System.Drawing.Size(264, 48);
			this.lbGenericSimsCallparms.TabIndex = 1;
			this.lbGenericSimsCallparms.Text = "label1";
			// 
			// MyForm0x0001
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(640, 366);
			this.Controls.Add(this.pnWiz0x0001);
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Name = "MyForm0x0001";
			this.Text = "Instruction Container";
			this.pnWiz0x0001.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void cbGenericSimsCall_Changed(object sender, System.EventArgs e)
		{
			if (cbGenericSimsCall.SelectedIndex < 0)
				lbGenericSimsCallparms.Text = "Unknown operand 0x" + Helper.HexString(operand0);
			else
				lbGenericSimsCallparms.Text = parms[cbGenericSimsCall.SelectedIndex];
		}

	}

	#endregion

	public class BhavPrimWiz0x0001 : pjse.ABhavPrimWiz
	{
		public BhavPrimWiz0x0001() : base() { }

		public BhavPrimWiz0x0001(Instruction i) : base() { instruction = i; }


		#region pjse.ABhavPrimWiz
		private MyForm0x0001 myForm = null;

		public override Panel bhavPrimWizPanel
		{
			get
			{
				myForm = new MyForm0x0001();
				return myForm.pnWiz0x0001;
			}
		}

		public override void Execute()
		{
			if (instruction != null) myForm.Execute(instruction, genericSimsCall, parms);
		}

		public override Instruction Write()
		{
			return (instruction == null) ? null : myForm.Write(instruction);
		}

		public override string OpcodeName(Bhav parent, ushort opcode, byte[] operands)
		{
			byte operand0 = operands[0];
			string s = "0x" + Helper.HexString(operand0) + ": ";
			if (operand0 < genericSimsCall.Length)
			{
				s += genericSimsCall[operand0];
				if (!parms[operand0].Equals(""))
					s += " (" + parms[operand0] + ")";
				return s;
			}
			else
				return s + "Unknown";
			//return Localization.Manager.GetString("Unknown");
		}

		#endregion

		#region genericSimsCall strings
		private string[] genericSimsCall =
			{
				 "Exit Lot" // 0x00
				,"center view on stack object"
				,"set action icon to stack object"
				,"uncenter view"
				,"add to family" // 0x04
				,"combine assets"
				,"remove from family"
				,"depracated - make new neighbor"
				,"family tutorial complete" // 0x08
				,"architecture tutorial complete"
				,"disable build and buy"
				,"enable build and buy"
				,"get distance to camera" // 0x0c
				,"abort interactions"
				,"get house radio station"
				,"get my routing footprint"
				,"change normal outfit" // 0x10
				,"Swap Lot"
				,"Set Simulator Speed"
				,"Swap to neighbor's lot"
				,"Swap to family's lot" // 0x14
				,"Add Child to Family Relationship Array"
				,"Add Spouse to Family Relationship Array"
				,"Remove From Family Relationship Array"
				,"Preload New Sim Age" //0x18
				,"Set Selected Sim"
				,"Start Lot Transition"
				,"Get Number of community lots"
				,"Perform Money Effect" // 0x1c
				,"Preload Visitor"
				,"Preload Clothing"
				,"Preload Object"
				,"Get Outfits Information" // 0x20
				,"Update Footprint"
				,"Get Distance Between Lots"
				,"Get Zoning Type"
				,"GetID of Family In Lot" // 0x24
				,"Lot Transition Done"
				,"Unlink Character"
				,"Force Recalc of Wants"
				,"Game State Transition Control" // 0x28
				,"Facial Overlay Zits Toggle"
				,"Sim Fitness Update"
				,"Is It Ok to Idle Here"
				,"Hide / Unhide Puck" // 0x2c
				,"Copy Last Name"
				,"Set Sim Hair Override"
				,"Clear Sim Hair Override"
				,"Get Remapped Neighbor Id" // 0x30
				,"Can Change Footprint?"
				,"Extract Money From other Lot"
				,"Show Info On Lot Loading Screen"
				,"Wall in Front?" // 0x34
				,"Set Facial Overlay State"
			};
		#endregion

		#region genericSimsCall param descriptions
		private string[] parms =
			{
				"Temp 0:neighborhood, Temp 1:evict, Temp 2:save lot, Temp 3:reset tutorial" // 0x00
				,"" // 0x01
				,"" // 0x02
				,"" // 0x03
				,"Stack Obj:nID, Temp 0:familyID" // 0x04
				,"Temp 0:familyID" // 0x05
				,"Stack Obj:nID" // 0x06
				,"" // 0x07
				,"" // 0x08
				,"" // 0x09
				,"" // 0x0a
				,"" // 0x0b
				,"Temp 0:result value" // 0x0c
				,"Stack Obj" // 0x0d
				,"Temp 0:result value" // 0x0e
				,"Temp 0:result value" // 0x0f
				,"" // 0x10
				,"Temp 0:lotID" // 0x11
				,"Temp 0:speed" // 0x12
				,"Temp 0:neighbor ID" // 0x13
				,"Temp 0:family" // 0x14
				,"Temp 0:child nID, Temp 1:parent nID" // 0x15
				,"Temp 0:new spouse nID, Temp 1:initial spouse nID" // 0x16
				,"Temp 0:remove nID, Temp 1:relative nID" // 0x17
				,"Temp 0:age" // 0x18
				,"Temp 0" // 0x19
				,"" // 0x1a
				,"Temp 0:result value" // 0x1b
				,"Temp 0:amount, Temp 2:multiplier" // 0x1c
				,"Temp 0:nID" // 0x1d
				,"Temp 0:outfit" // 0x1e
				,"Temp 0,1:GUID" // 0x1f
				,"Temp 0:outfit, Temp 1:result value" // 0x20
				,"" // 0x21
				,"Temp 0:source, Temp 1:destination, Temp 2:result value" // 0x22
				,"Temp 0:lotID, Temp 1:result value" // 0x23
				,"Temp 0:lotID, Temp 1:result value" // 0x24
				,"" // 0x25
				,"Temp 0:nID" // 0x26
				,"" // 0x27
				,"Temp 0" // 0x28
				,"Temp 0" // 0x29
				,"" // 0x2a
				,"" // 0x2b
				,"Temp 0" // 0x2c
				,"Temp 0:source, Stack Obj:destination" // 0x2d
				,"Temp 0:tableID, Temp 1:index, Temp 3:fallback" // 0x2e
				,"" // 0x2f
				,"Temp 0:nID, Temp 1:result value" // 0x30
				,"Temp 0" // 0x31
				,"Temp 0:take, Temp 1:target nID, Temp 2:percent, Temp 3,4:amount, Temp 5:from assets" // 0x32
				,"Temp 0" // 0x33
				,"Temp 0:direction, Temp 1:result wall obj" // 0x34
				,"Temp 0:tableID, Temp 1:index, Temp 2:state, Temp 3:fallback" // 0x35
			};
		#endregion
	}

}
