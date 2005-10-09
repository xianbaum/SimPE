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
			return "&" + this.Text;
		}

		#endregion

		private uint guid = 0;
		private ArrayList alHex32 = null;
		private bool internalchg = false;

		private void GUIDSearch()
		{
			string s = "";
			uint itemguid = 0;

			AbstractWrapper wrapper = (AbstractWrapper)SimPe.FileTable.WrapperRegistry.FindHandler(SimPe.Data.MetaData.OBJD_FILE);
			if (wrapper == null)
			{
				this.rtbReport.Text = "[Error: can't process OBJD files]";
				return;
			}

			this.Cursor = System.Windows.Forms.Cursors.WaitCursor;

			this.progressBar1.Value = 0;
			this.rtbReport.Text = "Searching...";

			pjse.FileTable.Entry[] results = pjse.FileTable.GFT[SimPe.Data.MetaData.OBJD_FILE];
			this.progressBar1.Maximum = results.Length;

			foreach (pjse.FileTable.Entry item in results)
			{
				wrapper.ProcessData(item.PFD, item.Package);
				System.IO.BinaryReader reader = wrapper.StoredData;
				if (reader.BaseStream.Length > 0x5c + 4) // sizeof(uint)
				{
					reader.BaseStream.Seek(0x5c, System.IO.SeekOrigin.Begin);
					itemguid = reader.ReadUInt32();

					if (itemguid == guid)
					{
						s += "0x" + SimPe.Helper.HexString(guid) + ": "
							+ "Group 0x" + SimPe.Helper.HexString(item.PFD.Group) + " - "
							+ wrapper.ResourceName.Remove(0, 13) + " (" + item.Package.FileName + ")\n";
					}
				}
				this.progressBar1.Value++;
			}
			this.Cursor = System.Windows.Forms.Cursors.Default;
			this.rtbReport.Text = s.Length == 0 ? "No matches found" : s;
			this.progressBar1.Value = 0;
		}

		private bool hex32_IsValid(object sender)
		{
			if (alHex32.IndexOf(sender) < 0)
				throw new Exception("hex32_IsValid not applicable to control " + sender.ToString());
			try { Convert.ToUInt32(((TextBox)sender).Text, 16); }
			catch (Exception) { return false; }
			return true;
		}


		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.tbGUID = new System.Windows.Forms.TextBox();
			this.lbGUID = new System.Windows.Forms.Label();
			this.rtbReport = new System.Windows.Forms.RichTextBox();
			this.btnSearch = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.progressBar1 = new System.Windows.Forms.ProgressBar();
			this.SuspendLayout();
			// 
			// tbGUID
			// 
			this.tbGUID.Location = new System.Drawing.Point(52, 8);
			this.tbGUID.MaxLength = 10;
			this.tbGUID.Name = "tbGUID";
			this.tbGUID.Size = new System.Drawing.Size(88, 20);
			this.tbGUID.TabIndex = 2;
			this.tbGUID.Text = "0xDDDDDDDD";
			this.tbGUID.Validating += new System.ComponentModel.CancelEventHandler(this.hex32_Validating);
			this.tbGUID.Validated += new System.EventHandler(this.hex32_Validated);
			this.tbGUID.TextChanged += new System.EventHandler(this.hex32_TextChanged);
			this.tbGUID.Enter += new System.EventHandler(this.tbGUID_Enter);
			// 
			// lbGUID
			// 
			this.lbGUID.AutoSize = true;
			this.lbGUID.Location = new System.Drawing.Point(15, 11);
			this.lbGUID.Name = "lbGUID";
			this.lbGUID.Size = new System.Drawing.Size(32, 16);
			this.lbGUID.TabIndex = 1;
			this.lbGUID.Text = "GUID";
			// 
			// rtbReport
			// 
			this.rtbReport.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.rtbReport.DetectUrls = false;
			this.rtbReport.Location = new System.Drawing.Point(16, 40);
			this.rtbReport.Name = "rtbReport";
			this.rtbReport.ReadOnly = true;
			this.rtbReport.ShowSelectionMargin = true;
			this.rtbReport.Size = new System.Drawing.Size(416, 120);
			this.rtbReport.TabIndex = 0;
			this.rtbReport.TabStop = false;
			this.rtbReport.Text = "Type in the GUID and click Search";
			this.rtbReport.WordWrap = false;
			// 
			// btnSearch
			// 
			this.btnSearch.Location = new System.Drawing.Point(152, 8);
			this.btnSearch.Name = "btnSearch";
			this.btnSearch.TabIndex = 3;
			this.btnSearch.Text = "Search";
			this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
			// 
			// btnClose
			// 
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnClose.Location = new System.Drawing.Point(358, 8);
			this.btnClose.Name = "btnClose";
			this.btnClose.TabIndex = 4;
			this.btnClose.Text = "Close";
			// 
			// progressBar1
			// 
			this.progressBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.progressBar1.Location = new System.Drawing.Point(0, 166);
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new System.Drawing.Size(448, 23);
			this.progressBar1.TabIndex = 5;
			// 
			// GUIDTool
			// 
			this.AcceptButton = this.btnSearch;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnClose;
			this.ClientSize = new System.Drawing.Size(448, 189);
			this.Controls.Add(this.progressBar1);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnSearch);
			this.Controls.Add(this.rtbReport);
			this.Controls.Add(this.lbGUID);
			this.Controls.Add(this.tbGUID);
			this.Name = "GUIDTool";
			this.Text = "GUID Tool";
			this.ResumeLayout(false);

		}
		#endregion

		private void hex32_TextChanged(object sender, System.EventArgs ev)
		{
			if (internalchg) return;
			if (!hex32_IsValid(sender)) return;

			uint val = Convert.ToUInt32(((TextBox)sender).Text, 16);
			internalchg = true;
			switch (alHex32.IndexOf(sender))
			{
				case 0: guid = val; break;
			}
			internalchg = false;
		}

		private void hex32_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (hex32_IsValid(sender)) return;

			e.Cancel = true;

			internalchg = true;
			uint val = 0;
			switch (alHex32.IndexOf(sender))
			{
				case 0: val = 0; break;
			}

			((TextBox)sender).Text = "0x" + SimPe.Helper.HexString(val);
			((TextBox)sender).SelectAll();
			internalchg = false;
		}

		private void hex32_Validated(object sender, System.EventArgs e)
		{
			bool origstate = internalchg;
			internalchg = true;
			((TextBox)sender).Text = "0x" + SimPe.Helper.HexString(Convert.ToUInt32(((TextBox)sender).Text, 16));
			((TextBox)sender).SelectAll();
			internalchg = origstate;
		}


		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			GUIDSearch();
		}

		private void tbGUID_Enter(object sender, System.EventArgs e)
		{
			this.tbGUID.SelectAll();
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
