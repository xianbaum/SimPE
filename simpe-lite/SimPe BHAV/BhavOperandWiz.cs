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
using pjse.BhavNameWizards;
using pjse.BhavOperandWizards;

namespace pjse
{
	/// <summary>
	/// Container for bhavPrimWizPanel from BhavOperandWizProvider
	/// </summary>
	public class BhavOperandWiz : System.Windows.Forms.Form
	{
		#region Form variables

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button OK;
		private System.Windows.Forms.Button Cancel;
		/// <summary>
		/// Erforderliche Designervariable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion

		public BhavOperandWiz()
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


		public Instruction Execute(Instruction i, int wizType)
		{
			pjse.ABhavOperandWiz wiz = null;
			switch(wizType)
			{
				case 0: wiz = pjse.BhavOperandWizProvider.Raw(i); break;
				case 1: wiz = pjse.BhavOperandWizProvider.For(i); break;
				default: wiz = pjse.BhavOperandWizProvider.Default(i); break;
			}
			if (wiz == null) return null;

			Panel pn = wiz.bhavPrimWizPanel;
			pn.Parent = this;
			pn.Top = 0;
			pn.Left = 0;
			int footHeight = this.Height - this.panel1.Bottom + 8;
			this.Width = pn.Width + 8;
			this.Height = pn.Height + footHeight;
			wiz.Execute();

			DialogResult dr = ShowDialog();
			Close();

			switch (dr)
			{
				case DialogResult.OK:
					return wiz.Write();
				default:
					return null;
			}
		}


		#region Vom Windows Form-Designer generierter Code
		/// <summary>
		/// Erforderliche Methode für die Designerunterstützung. 
		/// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
		/// </summary>
		private void InitializeComponent()
		{
			this.panel1 = new System.Windows.Forms.Panel();
			this.OK = new System.Windows.Forms.Button();
			this.Cancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Location = new System.Drawing.Point(0, 112);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(320, 1);
			this.panel1.TabIndex = 1;
			// 
			// OK
			// 
			this.OK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.OK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.OK.Location = new System.Drawing.Point(232, 120);
			this.OK.Name = "OK";
			this.OK.TabIndex = 2;
			this.OK.Text = "Okay";
			// 
			// Cancel
			// 
			this.Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.Cancel.Location = new System.Drawing.Point(144, 120);
			this.Cancel.Name = "Cancel";
			this.Cancel.TabIndex = 2;
			this.Cancel.Text = "Cancel";
			// 
			// BhavOperandWiz
			// 
			this.AcceptButton = this.OK;
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.CancelButton = this.Cancel;
			this.ClientSize = new System.Drawing.Size(314, 151);
			this.Controls.Add(this.OK);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.Cancel);
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "BhavOperandWiz";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Instruction Wizard (EXPERIMENTAL)";
			this.ResumeLayout(false);

		}
		#endregion

	}


	class DataOwnerControl : IDisposable, IDataOwnerListener
	{
		#region Form variables
		private ComboBox cbDataOwner;
		private ComboBox cbPicker;
		private TextBox tbValue;
		#endregion

		#region Form event handlers
		private void cbDataOwner_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (internalchg) return;
			if (cbDataOwner.SelectedIndex != -1)
				UpdateDataOwner();
		}

		private void cbPicker_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (internalchg) return;
			if (cbPicker.SelectedIndex != -1)
				SetInstance((ushort)cbPicker.SelectedIndex);
		}


		private void tbValue_Enter(object sender, System.EventArgs e)
		{
			((TextBox)sender).SelectAll();
		}

		private void tbValue_TextChanged(object sender, System.EventArgs ev)
		{
			if (internalchg) return;
			if (!tbValue_IsValid((TextBox)sender)) return;
			SetInstance(tbValueConverter((TextBox)sender));
		}

		private void tbValue_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (tbValue_IsValid((TextBox)sender)) return;
			e.Cancel = true;
			tbValue_Validated(sender, null);
		}

		private void tbValue_Validated(object sender, System.EventArgs e)
		{
			bool origstate = internalchg;
			internalchg = true;
			((TextBox)sender).Text = tbValueConverter(instance);
			internalchg = origstate;
		}

		#endregion

		#region Form validation
		private bool tbValue_IsValid(TextBox tb)
		{
			try
			{
				ushort v = tbValueConverter(tb);
				return true;
			}
			catch
			{
				return false;
			}
		}

		private string tbValueConverter(ushort v)
		{
			if      (dataOwner == 0x1a) return pjse.BhavWiz.ExpandBCONtoString(v, false);
			else if (dataOwner == 0x2f) return pjse.BhavWiz.ExpandBCONtoString(v, true);
			else if (isDecimal) return ((int)v).ToString();
			else                return "0x" + SimPe.Helper.HexString(v);
		}

		private ushort tbValueConverter(TextBox sender)
		{
			if      (dataOwner == 0x1a) return pjse.BhavWiz.StringtoExpandBCON(((TextBox)sender).Text, false);
			else if (dataOwner == 0x2f) return pjse.BhavWiz.StringtoExpandBCON(((TextBox)sender).Text, true);
			else if (isDecimal) return Convert.ToUInt16(((TextBox)sender).Text, 10);
			else                return Convert.ToUInt16(((TextBox)sender).Text, 16);
		}

		#endregion

		public DataOwnerControl(BhavWiz inst, ComboBox cbDataOwner, ComboBox cbPicker, TextBox tbValue, byte dataOwner, ushort instance, IDataOwnerListener listener)
		{
			this.cbDataOwner = cbDataOwner;
			this.cbPicker = cbPicker;
			this.tbValue = tbValue;

			this.inst = inst;
			this.listener = listener;
			if (listener != null)
				listener.FlagsFor = this;

			this.dataOwner = dataOwner;
			this.instance = instance;

			this.flagsFor = null;

			this.tbValue.Text = this.tbValueConverter(instance);

			this.cbDataOwner.Items.Clear();
			this.cbDataOwner.Items.AddRange(GS.gStr(GS.BhavStr.DataOwners).ToArray());
			if (cbDataOwner.Items.Count > dataOwner)
				cbDataOwner.SelectedIndex = dataOwner;
			UpdateDataOwner();

			this.cbDataOwner.SelectedIndexChanged += new System.EventHandler(this.cbDataOwner_SelectedIndexChanged);
			this.cbPicker.SelectedIndexChanged += new System.EventHandler(this.cbPicker_SelectedIndexChanged);

			this.tbValue.Validating += new System.ComponentModel.CancelEventHandler(this.tbValue_Validating);
			this.tbValue.Validated += new System.EventHandler(this.tbValue_Validated);
			this.tbValue.TextChanged += new System.EventHandler(this.tbValue_TextChanged);
			this.tbValue.Enter += new System.EventHandler(this.tbValue_Enter);
		}

		public DataOwnerControl(BhavWiz inst, ComboBox cbDataOwner, ComboBox cbPicker, TextBox tbValue, byte dataOwner, ushort instance)
			: this(inst, cbDataOwner, cbPicker, tbValue, dataOwner, instance, null) {}


		#region IDisposable Members

		public void Dispose()
		{
			this.inst = null;
			this.cbDataOwner = null;
			this.cbPicker = null;
			this.tbValue = null;
			this.listener = null;
			this.flagsFor = null;
			this.cbDataOwner.SelectedIndexChanged -= new System.EventHandler(this.cbDataOwner_SelectedIndexChanged);
			this.cbPicker.SelectedIndexChanged -= new System.EventHandler(this.cbPicker_SelectedIndexChanged);
			this.tbValue.TextChanged -= new System.EventHandler(this.tbValue_TextChanged);
		}

		#endregion

		#region IDataOwner Members

		private byte dataOwner = 0;
		private ushort instance = 0;
		public byte DataOwner { get { return dataOwner; } }

		public ushort Value { get { return instance; } }

		#endregion

		#region IDataOwnerListener Members

		private IDataOwner flagsFor = null;
		public IDataOwner FlagsFor
		{
			set
			{
				flagsFor = value;
			}
		}

		public void Notify()
		{
			UpdateDataOwner();
		}

		#endregion

		private bool internalchg = false;

		private bool isDecimal = false;
		private bool useAttrPicker = true;
		private bool useFlagNames = false;

		public bool Decimal
		{
			get { return this.isDecimal; }

			set
			{
				if (isDecimal != value)
				{
					isDecimal = value;
					internalchg = true;
					tbValue.Text = tbValueConverter(instance);
					internalchg = false;
				}
			}

		}

		public bool UseAttrPicker
		{
			get { return this.useAttrPicker; }

			set
			{
				if (useAttrPicker != value)
				{
					useAttrPicker = value;
					UpdateDataOwner();
				}
			}

		}

		public bool UseFlagNames
		{
			get { return this.useFlagNames; }

			set
			{
				if (useFlagNames != value)
				{
					useFlagNames = value;
					UpdateDataOwner();
				}
			}

		}


		private BhavWiz inst;
		private IDataOwnerListener listener;

		private void UpdateDataOwner()
		{
			if (internalchg)
				return;

			internalchg = true;

			if (cbDataOwner.SelectedIndex != dataOwner)
			{
				dataOwner = (byte)cbDataOwner.SelectedIndex;
				tbValue.Text = tbValueConverter(instance);
				if (listener != null)
					listener.Notify();
			}

			#region pickerNames
			ArrayList pickerNames = null;
			if (useFlagNames && dataOwner == 0x07 && flagsFor != null)
			{
				pickerNames = (ArrayList)WizPrim0x0002.flagNames(flagsFor.DataOwner, flagsFor.Value);
				if (pickerNames != null)
				{
					pickerNames = (ArrayList)pickerNames.Clone();
					pickerNames.Insert(0, "[0: invalid]");
				}
			}
			else if (useAttrPicker && (dataOwner == 0x00 || dataOwner == 0x01))
			{
				pickerNames = inst.GetAttrNames(Scope.Private);
			}
			else if (useAttrPicker && (dataOwner == 0x02 || dataOwner == 0x05))
			{
				pickerNames = inst.GetAttrNames(Scope.SemiGlobal);
			}
			else if (dataOwner == 0x09 || dataOwner == 0x16 || dataOwner == 0x32) // Param
			{
				pickerNames = inst.GetTPRPnames(false);
			}
			else if (dataOwner == 0x19) // Local
			{
				pickerNames = inst.GetTPRPnames(true);
			}
			else if (BhavWiz.doidGStr[dataOwner] != null)
			{
				pickerNames = GS.gStr((GS.BhavStr)BhavWiz.doidGStr[dataOwner]);
			}
			#endregion

			cbPicker.Visible = false;
			if (pickerNames != null && pickerNames.Count > 0)
			{
				cbPicker.Visible = true;
				cbPicker.Items.Clear();
				cbPicker.Items.AddRange(pickerNames.ToArray());
				cbPicker.SelectedIndex = (cbPicker.Items.Count > instance) ? instance : -1;
			}
			tbValue.Visible = !cbPicker.Visible;

			internalchg = false;
		}

		private void SetInstance(ushort i)
		{
			if (instance != i)
			{
				instance = i;
				if (listener != null)
					listener.Notify();
			}
		}
	}

}
