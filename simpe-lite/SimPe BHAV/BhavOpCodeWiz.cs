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
	/// <summary>
	/// Summary description for BhavOpCodeWiz.
	/// </summary>
	public class BhavOpCodeWiz : System.Windows.Forms.Form
	{
		#region Form variables
		internal System.Windows.Forms.Panel pnOpCode;
		private System.Windows.Forms.TabControl tcopcodes;
		private System.Windows.Forms.TabPage tbprimitive;
		private System.Windows.Forms.ListBox lbprimitives;
		private System.Windows.Forms.TabPage tbglobal;
		private System.Windows.Forms.ListBox lbglobal;
		private System.Windows.Forms.TabPage tbsemi;
		private System.Windows.Forms.ListBox lbsemi;
		private System.Windows.Forms.TabPage tbprivate;
		private System.Windows.Forms.ListBox lbprivate;
		private System.Windows.Forms.Button OK;
		private System.Windows.Forms.Button Cancel;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion

		public BhavOpCodeWiz()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
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


		#region BhavOpCodeWizUI
		//the current SemiGroup

		public int Execute(Bhav bhav, Control form)
		{
			if (bhav == null) return -1;
			if (bhav.Package == null) return -1;
			if (bhav.Opcodes == null) return -1;

			form.Cursor = Cursors.WaitCursor;
			this.Cursor = Cursors.WaitCursor;

			Primitives();
			Globals(bhav);
			Locals(bhav);
			SemiGlobals(bhav);
			
			form.Cursor = Cursors.Default;
			this.Cursor = Cursors.Default;

			if (lbprimitives.Items.Count>0) lbprimitives.SelectedIndex = 0;
			if (lbglobal.Items.Count>0) lbglobal.SelectedIndex = 0;
			if (lbprivate.Items.Count>0) lbprivate.SelectedIndex = 0;
			if (lbsemi.Items.Count>0) lbsemi.SelectedIndex = 0;

			int opcode = -1;
			this.DialogResult = DialogResult.Cancel;
			switch(ShowDialog())
			{
				case System.Windows.Forms.DialogResult.OK:
				case System.Windows.Forms.DialogResult.Yes:
					switch (this.tcopcodes.SelectedIndex)
					{
						case 0:
							if (lbprimitives.SelectedIndex >= 0) opcode = (ushort)((SimPe.Data.Alias)lbprimitives.Items[lbprimitives.SelectedIndex]).Id;
							break;
						case 1:
							if (lbglobal.SelectedIndex >= 0) opcode = (ushort)((SimPe.Data.Alias)lbglobal.Items[lbglobal.SelectedIndex]).Id;
							break;
						case 2:
							if (lbsemi.SelectedIndex >= 0) opcode = (ushort)((SimPe.Data.Alias)lbsemi.Items[lbsemi.SelectedIndex]).Id;
							break;
						case 3:
							if (lbprivate.SelectedIndex >= 0) opcode = (ushort)((SimPe.Data.Alias)lbprivate.Items[lbprivate.SelectedIndex]).Id;
							break;
						default:
							break;
					}
					return opcode;
				default:
					return -1;
			}
		}

		private void Primitives()
		{
			if (this.lbprimitives.Items.Count != 0) return;

			for (int i=0; i<pjse.BhavNameWizards.ANamePrimitiveWiz.Length; i++)
			{
				string name = pjse.BhavNameWizards.ANamePrimitiveWiz.Name(i);
				if (!name.StartsWith("~"))
				{
					SimPe.Data.Alias a = new SimPe.Data.Alias((uint)i, name);
					this.lbprimitives.Items.Add(a);
				}
			}
		}

		private void Globals(Bhav wrapper)
		{
			if (this.lbglobal.Items.Count != 0) return;

			SimPe.Data.Alias a = pjse.BhavNameWizards.GlobalWiz.First(wrapper);
			while (a != null)
			{
				lbglobal.Items.Add(a);
				a = pjse.BhavNameWizards.GlobalWiz.Next();
			}
		}

		private void Locals(Bhav wrapper)
		{
			this.lbprivate.Items.Clear();

			SimPe.Data.Alias a = pjse.BhavNameWizards.LocalWiz.First(wrapper);
			while (a != null)
			{
				lbprivate.Items.Add(a);
				a = pjse.BhavNameWizards.LocalWiz.Next();
			}
		}

		private void SemiGlobals(Bhav wrapper)
		{
			if (this.lbsemi.Items.Count != 0 && pjse.BhavNameWizards.SemiGlobalWiz.SameGroup(wrapper)) return;
			this.lbsemi.Items.Clear();

			SimPe.Data.Alias a = pjse.BhavNameWizards.SemiGlobalWiz.First(wrapper);
			while (a != null)
			{
				lbsemi.Items.Add(a);
				a = pjse.BhavNameWizards.SemiGlobalWiz.Next();
			}
			this.tbsemi.Text = pjse.BhavNameWizards.SemiGlobalWiz.SemiGroupName;
		}


		#endregion

		#region Vom Windows Form-Designer generierter Code
		/// <summary>
		/// Erforderliche Methode für die Designerunterstützung. 
		/// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
		/// </summary>
		private void InitializeComponent()
		{
			this.pnOpCode = new System.Windows.Forms.Panel();
			this.tcopcodes = new System.Windows.Forms.TabControl();
			this.tbprimitive = new System.Windows.Forms.TabPage();
			this.lbprimitives = new System.Windows.Forms.ListBox();
			this.tbglobal = new System.Windows.Forms.TabPage();
			this.lbglobal = new System.Windows.Forms.ListBox();
			this.tbsemi = new System.Windows.Forms.TabPage();
			this.lbsemi = new System.Windows.Forms.ListBox();
			this.tbprivate = new System.Windows.Forms.TabPage();
			this.lbprivate = new System.Windows.Forms.ListBox();
			this.OK = new System.Windows.Forms.Button();
			this.Cancel = new System.Windows.Forms.Button();
			this.pnOpCode.SuspendLayout();
			this.tcopcodes.SuspendLayout();
			this.tbprimitive.SuspendLayout();
			this.tbglobal.SuspendLayout();
			this.tbsemi.SuspendLayout();
			this.tbprivate.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnOpCode
			// 
			this.pnOpCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.pnOpCode.Controls.Add(this.tcopcodes);
			this.pnOpCode.Location = new System.Drawing.Point(8, 8);
			this.pnOpCode.Name = "pnOpCode";
			this.pnOpCode.Size = new System.Drawing.Size(392, 424);
			this.pnOpCode.TabIndex = 2;
			// 
			// tcopcodes
			// 
			this.tcopcodes.Controls.Add(this.tbprimitive);
			this.tcopcodes.Controls.Add(this.tbglobal);
			this.tcopcodes.Controls.Add(this.tbsemi);
			this.tcopcodes.Controls.Add(this.tbprivate);
			this.tcopcodes.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tcopcodes.Location = new System.Drawing.Point(0, 0);
			this.tcopcodes.Name = "tcopcodes";
			this.tcopcodes.SelectedIndex = 0;
			this.tcopcodes.Size = new System.Drawing.Size(392, 424);
			this.tcopcodes.TabIndex = 2;
			// 
			// tbprimitive
			// 
			this.tbprimitive.Controls.Add(this.lbprimitives);
			this.tbprimitive.Location = new System.Drawing.Point(4, 22);
			this.tbprimitive.Name = "tbprimitive";
			this.tbprimitive.Size = new System.Drawing.Size(384, 398);
			this.tbprimitive.TabIndex = 0;
			this.tbprimitive.Text = "Primitives";
			// 
			// lbprimitives
			// 
			this.lbprimitives.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lbprimitives.IntegralHeight = false;
			this.lbprimitives.Location = new System.Drawing.Point(8, 8);
			this.lbprimitives.Name = "lbprimitives";
			this.lbprimitives.Size = new System.Drawing.Size(368, 384);
			this.lbprimitives.Sorted = true;
			this.lbprimitives.TabIndex = 0;
			this.lbprimitives.DoubleClick += new System.EventHandler(this.OK_Click);
			// 
			// tbglobal
			// 
			this.tbglobal.Controls.Add(this.lbglobal);
			this.tbglobal.Location = new System.Drawing.Point(4, 22);
			this.tbglobal.Name = "tbglobal";
			this.tbglobal.Size = new System.Drawing.Size(384, 398);
			this.tbglobal.TabIndex = 3;
			this.tbglobal.Text = "Global";
			this.tbglobal.Visible = false;
			// 
			// lbglobal
			// 
			this.lbglobal.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lbglobal.IntegralHeight = false;
			this.lbglobal.Location = new System.Drawing.Point(8, 7);
			this.lbglobal.Name = "lbglobal";
			this.lbglobal.Size = new System.Drawing.Size(368, 384);
			this.lbglobal.Sorted = true;
			this.lbglobal.TabIndex = 1;
			this.lbglobal.DoubleClick += new System.EventHandler(this.OK_Click);
			// 
			// tbsemi
			// 
			this.tbsemi.Controls.Add(this.lbsemi);
			this.tbsemi.Location = new System.Drawing.Point(4, 22);
			this.tbsemi.Name = "tbsemi";
			this.tbsemi.Size = new System.Drawing.Size(384, 398);
			this.tbsemi.TabIndex = 2;
			this.tbsemi.Text = "SemiGlobal";
			this.tbsemi.Visible = false;
			// 
			// lbsemi
			// 
			this.lbsemi.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lbsemi.IntegralHeight = false;
			this.lbsemi.Location = new System.Drawing.Point(8, 7);
			this.lbsemi.Name = "lbsemi";
			this.lbsemi.Size = new System.Drawing.Size(368, 384);
			this.lbsemi.Sorted = true;
			this.lbsemi.TabIndex = 1;
			this.lbsemi.DoubleClick += new System.EventHandler(this.OK_Click);
			// 
			// tbprivate
			// 
			this.tbprivate.Controls.Add(this.lbprivate);
			this.tbprivate.Location = new System.Drawing.Point(4, 22);
			this.tbprivate.Name = "tbprivate";
			this.tbprivate.Size = new System.Drawing.Size(384, 398);
			this.tbprivate.TabIndex = 1;
			this.tbprivate.Text = "Private";
			this.tbprivate.Visible = false;
			// 
			// lbprivate
			// 
			this.lbprivate.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lbprivate.IntegralHeight = false;
			this.lbprivate.Location = new System.Drawing.Point(8, 7);
			this.lbprivate.Name = "lbprivate";
			this.lbprivate.Size = new System.Drawing.Size(368, 384);
			this.lbprivate.Sorted = true;
			this.lbprivate.TabIndex = 1;
			this.lbprivate.DoubleClick += new System.EventHandler(this.OK_Click);
			// 
			// OK
			// 
			this.OK.Location = new System.Drawing.Point(320, 440);
			this.OK.Name = "OK";
			this.OK.Size = new System.Drawing.Size(80, 24);
			this.OK.TabIndex = 3;
			this.OK.Text = "Okay";
			this.OK.Click += new System.EventHandler(this.OK_Click);
			// 
			// Cancel
			// 
			this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.Cancel.Location = new System.Drawing.Point(240, 440);
			this.Cancel.Name = "Cancel";
			this.Cancel.TabIndex = 4;
			this.Cancel.Text = "Cancel";
			this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
			// 
			// BhavOpCodeWizUI
			// 
			this.AcceptButton = this.OK;
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.CancelButton = this.Cancel;
			this.ClientSize = new System.Drawing.Size(410, 471);
			this.Controls.Add(this.Cancel);
			this.Controls.Add(this.OK);
			this.Controls.Add(this.pnOpCode);
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "BhavOpCodeWizUI";
			this.ShowInTaskbar = false;
			this.Text = "BhavOpCodeWizUI";
			this.pnOpCode.ResumeLayout(false);
			this.tcopcodes.ResumeLayout(false);
			this.tbprimitive.ResumeLayout(false);
			this.tbglobal.ResumeLayout(false);
			this.tbsemi.ResumeLayout(false);
			this.tbprivate.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void OK_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.Close();
		}

		private void Cancel_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.Close();
		}

	}

}