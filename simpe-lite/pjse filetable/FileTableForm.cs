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
			this.OK = new System.Windows.Forms.Button();
			this.panel2 = new System.Windows.Forms.Panel();
			this.Cancel = new System.Windows.Forms.Button();
			this.cbLoadAtStartup = new System.Windows.Forms.CheckBox();
			this.btnRefresh = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// OK
			// 
			this.OK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.OK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.OK.Location = new System.Drawing.Point(144, 80);
			this.OK.Name = "OK";
			this.OK.TabIndex = 3;
			this.OK.Text = "Okay";
			// 
			// panel2
			// 
			this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel2.Location = new System.Drawing.Point(0, 72);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(232, 1);
			this.panel2.TabIndex = 0;
			// 
			// Cancel
			// 
			this.Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.Cancel.Location = new System.Drawing.Point(64, 80);
			this.Cancel.Name = "Cancel";
			this.Cancel.TabIndex = 4;
			this.Cancel.Text = "Cancel";
			// 
			// cbLoadAtStartup
			// 
			this.cbLoadAtStartup.Location = new System.Drawing.Point(8, 40);
			this.cbLoadAtStartup.Name = "cbLoadAtStartup";
			this.cbLoadAtStartup.TabIndex = 2;
			this.cbLoadAtStartup.Text = "Load at startup";
			// 
			// btnRefresh
			// 
			this.btnRefresh.Location = new System.Drawing.Point(8, 8);
			this.btnRefresh.Name = "btnRefresh";
			this.btnRefresh.Size = new System.Drawing.Size(80, 23);
			this.btnRefresh.TabIndex = 1;
			this.btnRefresh.Text = "Refresh now";
			this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
			// 
			// FileTableForm
			// 
			this.AcceptButton = this.OK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.Cancel;
			this.ClientSize = new System.Drawing.Size(224, 109);
			this.Controls.Add(this.cbLoadAtStartup);
			this.Controls.Add(this.btnRefresh);
			this.Controls.Add(this.OK);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.Cancel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FileTableForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "PJSE Filetable Settings";
			this.ResumeLayout(false);

		}
		#endregion

		private void btnRefresh_Click(object sender, System.EventArgs e)
		{
			pjse.FileTable.GFT.Refresh();
		}
	}
}
