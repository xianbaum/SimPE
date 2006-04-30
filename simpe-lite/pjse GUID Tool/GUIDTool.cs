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
using SimPe.Interfaces;
using SimPe.Interfaces.Plugin;
using SimPe.Interfaces.Scenegraph;

namespace pjse.guidtool
{
	/// <summary>
	/// Summary description for GUIDTool.
	/// </summary>
	public class GUIDTool : System.Windows.Forms.Form, ITool
	{
		private System.Windows.Forms.Label lbGUID;
		private System.Windows.Forms.TextBox tbGUID;
		private System.Windows.Forms.RichTextBox rtbReport;
		private System.Windows.Forms.Button btnSearch;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.ProgressBar progressBar1;
		private System.Windows.Forms.Label lbName;
		private System.Windows.Forms.TextBox tbName;
		private System.Windows.Forms.Label lbStatus;
		private System.Windows.Forms.Button btnHelp;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public GUIDTool()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			TextBox[] alHex32s = { tbGUID, };
			alHex32 = new ArrayList(alHex32s);
			this.progressBar1.Visible = false;
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


		#region ITool Members

		public bool IsEnabled(SimPe.Interfaces.Files.IPackedFileDescriptor pfd, SimPe.Interfaces.Files.IPackageFile package)
		{
			return true;
		}

		public SimPe.Interfaces.Plugin.IToolResult ShowDialog(ref SimPe.Interfaces.Files.IPackedFileDescriptor pfd, ref SimPe.Interfaces.Files.IPackageFile package)
		{
			this.tbGUID.Text = "0x" + SimPe.Helper.HexString(guid);
			this.progressBar1.Value = 0;

			this.ShowDialog(this.Parent);
			return new SimPe.Plugin.ToolResult(false, false);
		}

		#endregion

		#region IToolPlugin Members

		public override string ToString()
		{
			return "PJSE\\" + pjse.Localization.GetString("ObjectFinder");
		}

		#endregion

		private uint guid = 0;
		private ArrayList alHex32 = null;
		private Control last = null;

		private void Search(SearchType type)
		{
			uint itemguid = 0;

			this.Cursor = System.Windows.Forms.Cursors.WaitCursor;

			this.lbStatus.Visible = false;
			this.progressBar1.Value = 0;
			this.progressBar1.Visible = true;

			pjse.FileTable.Entry[] results = pjse.FileTable.GFT[SimPe.Data.MetaData.OBJD_FILE];
			this.progressBar1.Maximum = results.Length;

			this.rtbReport.Text = "";
			int i = 0;
			foreach (pjse.FileTable.Entry item in results)
			{
				System.IO.BinaryReader reader = item.Wrapper.StoredData;

				if (reader.BaseStream.Length >= 0x40) // filename length
				{
					if (reader.BaseStream.Length > 0x5c + 4) // sizeof(uint)
					{
						reader.BaseStream.Seek(0x5c, System.IO.SeekOrigin.Begin);
						itemguid = reader.ReadUInt32();
					}
					else
						itemguid = 0;

					if ((type == SearchType.GUID && itemguid == guid)
						||
						(type == SearchType.Name && ((string)item).ToLower().IndexOf(this.tbName.Text) >= 0))
					{
						this.rtbReport.Text += "0x" + SimPe.Helper.HexString(itemguid) + ": "
							+ pjse.Localization.GetString("Group") + " 0x" + SimPe.Helper.HexString(item.PFD.Group)
                            + " - " + item + " (" + item.Package.FileName + ")\n";
						i++;
					}
				}
				this.progressBar1.Value++;
				if (i >= 180)
					break;
			}
			this.Cursor = System.Windows.Forms.Cursors.Default;
			this.progressBar1.Value = 0;
			this.progressBar1.Visible = false;
			if (i < 180)
				this.lbStatus.Text = (this.rtbReport.Text.Length == 0
                    ? pjse.Localization.GetString("No")
                    : i.ToString()) + Localization.GetString("MatchesFound");
			else
				this.lbStatus.Text = pjse.Localization.GetString("TooManyMatches");
			this.lbStatus.Visible = true;
		}

		private bool hex32_IsValid(object sender)
		{
			if (alHex32.IndexOf(sender) < 0)
				throw new Exception("hex32_IsValid not applicable to control " + sender.ToString());
			try { Convert.ToUInt32(((TextBox)sender).Text, 16); }
			catch (Exception) { return false; }
			return true;
		}


		#region SearchType enum
		private enum SearchType : int
		{
			GUID,
			Name,
		}
		#endregion

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GUIDTool));
            this.tbGUID = new System.Windows.Forms.TextBox();
            this.lbGUID = new System.Windows.Forms.Label();
            this.rtbReport = new System.Windows.Forms.RichTextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lbName = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.lbStatus = new System.Windows.Forms.Label();
            this.btnHelp = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbGUID
            // 
            resources.ApplyResources(this.tbGUID, "tbGUID");
            this.tbGUID.Name = "tbGUID";
            this.tbGUID.Enter += new System.EventHandler(this.textBox_Enter);
            this.tbGUID.Validated += new System.EventHandler(this.textBox_Validated);
            this.tbGUID.Validating += new System.ComponentModel.CancelEventHandler(this.hex32_Validating);
            // 
            // lbGUID
            // 
            resources.ApplyResources(this.lbGUID, "lbGUID");
            this.lbGUID.Name = "lbGUID";
            // 
            // rtbReport
            // 
            resources.ApplyResources(this.rtbReport, "rtbReport");
            this.rtbReport.DetectUrls = false;
            this.rtbReport.Name = "rtbReport";
            this.rtbReport.ReadOnly = true;
            this.rtbReport.ShowSelectionMargin = true;
            this.rtbReport.TabStop = false;
            // 
            // btnSearch
            // 
            resources.ApplyResources(this.btnSearch, "btnSearch");
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnClose
            // 
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnClose.Name = "btnClose";
            // 
            // progressBar1
            // 
            resources.ApplyResources(this.progressBar1, "progressBar1");
            this.progressBar1.Name = "progressBar1";
            // 
            // lbName
            // 
            resources.ApplyResources(this.lbName, "lbName");
            this.lbName.Name = "lbName";
            // 
            // tbName
            // 
            resources.ApplyResources(this.tbName, "tbName");
            this.tbName.Name = "tbName";
            this.tbName.Enter += new System.EventHandler(this.textBox_Enter);
            this.tbName.Validated += new System.EventHandler(this.textBox_Validated);
            // 
            // lbStatus
            // 
            resources.ApplyResources(this.lbStatus, "lbStatus");
            this.lbStatus.Name = "lbStatus";
            // 
            // btnHelp
            // 
            resources.ApplyResources(this.btnHelp, "btnHelp");
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.TabStop = false;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // GUIDTool
            // 
            this.AcceptButton = this.btnSearch;
            resources.ApplyResources(this, "$this");
            this.CancelButton = this.btnClose;
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.lbStatus);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.lbName);
            this.Controls.Add(this.lbGUID);
            this.Controls.Add(this.tbGUID);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.rtbReport);
            this.Name = "GUIDTool";
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private void hex32_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (hex32_IsValid(sender)) return;

			e.Cancel = true;

			uint val = 0;
			switch (alHex32.IndexOf(sender))
			{
				case 0: val = 0; break;
			}

			((TextBox)sender).Text = "0x" + SimPe.Helper.HexString(val);
			((TextBox)sender).SelectAll();
		}

		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			if (this.last == this.tbGUID)
			{
				guid = Convert.ToUInt32(this.tbGUID.Text, 16);
				this.tbGUID.Text = "0x" + SimPe.Helper.HexString(guid);
				this.tbName.Text = "";
				Search(SearchType.GUID);
			}
			else if (this.last == this.tbName)
			{
				guid = 0;
				this.tbGUID.Text = "0x" + SimPe.Helper.HexString(guid);
				this.tbName.Text = this.tbName.Text.Trim().ToLower();
				Search(SearchType.Name);
			}
		}

		private void textBox_Enter(object sender, System.EventArgs e)
		{
			((TextBox)sender).SelectAll();
		}

		private void textBox_Validated(object sender, System.EventArgs e)
		{
			this.last = (TextBox)sender;
		}

		private void btnHelp_Click(object sender, System.EventArgs e)
		{
			System.Windows.Forms.MessageBox.Show(
                pjse.Localization.GetString("ObjectFinderHelp"),
				this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

	}
}
namespace SimPe.Plugin
{
	public class WrapperFactory : AbstractWrapperFactory, IToolFactory
	{
		#region IToolFactory Members

		public SimPe.Interfaces.IToolPlugin[] KnownTools
		{
			get
			{
				IToolPlugin[] tools = {
										  new pjse.guidtool.GUIDTool()
									  };
				return tools;
			}
		}

		#endregion
	}

}
