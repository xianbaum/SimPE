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

namespace pjse
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class FileTableForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button OK;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Button Cancel;
		private System.Windows.Forms.CheckBox cbLoadAtStartup;
		private System.Windows.Forms.Button btnRefresh;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FileTableForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			this.cbLoadAtStartup.Checked = this.LoadAtStartup;
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



		private bool LoadAtStartup
		{
			get
			{
				SimPe.XmlRegistryKey  rkf = SimPe.Helper.WindowsRegistry.PluginRegistryKey.CreateSubKey("PJSE\\Bhav");
				object o = rkf.GetValue("loadAtStartup", false);
				return Convert.ToBoolean(o);
			}

			set
			{
				SimPe.XmlRegistryKey rkf = SimPe.Helper.WindowsRegistry.PluginRegistryKey.CreateSubKey("PJSE\\Bhav");
				rkf.SetValue("loadAtStartup", value);
			}

		}


		public void Settings()
		{
			DialogResult dr = ShowDialog();
			Close();

			if (dr == DialogResult.OK)
			{
				this.LoadAtStartup = this.cbLoadAtStartup.Checked;
			}
			return;
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FileTableForm));
            this.OK = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.Cancel = new System.Windows.Forms.Button();
            this.cbLoadAtStartup = new System.Windows.Forms.CheckBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // OK
            // 
            resources.ApplyResources(this.OK, "OK");
            this.OK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OK.Name = "OK";
            // 
            // panel2
            // 
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Name = "panel2";
            // 
            // Cancel
            // 
            resources.ApplyResources(this.Cancel, "Cancel");
            this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel.Name = "Cancel";
            // 
            // cbLoadAtStartup
            // 
            resources.ApplyResources(this.cbLoadAtStartup, "cbLoadAtStartup");
            this.cbLoadAtStartup.Name = "cbLoadAtStartup";
            // 
            // btnRefresh
            // 
            resources.ApplyResources(this.btnRefresh, "btnRefresh");
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // FileTableForm
            // 
            this.AcceptButton = this.OK;
            resources.ApplyResources(this, "$this");
            this.CancelButton = this.Cancel;
            this.Controls.Add(this.cbLoadAtStartup);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.OK);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.Cancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FileTableForm";
            this.ResumeLayout(false);

		}
		#endregion

		private void btnRefresh_Click(object sender, System.EventArgs e)
		{
			pjse.FileTable.GFT.Refresh();
		}
	}
}
