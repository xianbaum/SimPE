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
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Threading;
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
        #region Form variables

        private System.Windows.Forms.RichTextBox rtbReport;
		private System.Windows.Forms.Button btnSearch;
		private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ProgressBar progressBar1;
		private System.Windows.Forms.Label lbStatus;
		private System.Windows.Forms.Button btnHelp;
        private GroupBox gbOptions;
        private FlowLayoutPanel flowLayoutPanel1;
        private CheckBox ckbObjdGUID;
        private CheckBox ckbObjdName;
        private CheckBox ckbFixedOnly;
        private TextBox tbName;
        private Label lbName;
        private TextBox tbGUID;
        private Label lbGUID;
        private CheckBox ckbNrefName;
        private CheckBox ckbBhavName;
        private CheckBox ckbBconName;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

        #endregion

        public GUIDTool()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

            this.Height = PersistantHeight;
            this.Width = PersistantWidth;

            lHex32 = new List<TextBox>(new TextBox[] { tbGUID, });

            this.oldText = this.btnSearch.Text;
            this.prompt = this.lbStatus.Text;

            SearchComplete += new EventHandler(Complete);
        }

        private static string BASENAME = "PJSE\\Bhav\\GUIDTool";
        private static SimPe.XmlRegistryKey xrk = SimPe.Helper.WindowsRegistry.PluginRegistryKey;
        private int PersistantHeight
        {
            get
            {
                SimPe.XmlRegistryKey rkf = SimPe.Helper.WindowsRegistry.PluginRegistryKey.CreateSubKey(BASENAME);
                object o = rkf.GetValue("PersistentHeight", this.Height);
                return Convert.ToInt32(o);
            }

            set
            {
                SimPe.XmlRegistryKey rkf = SimPe.Helper.WindowsRegistry.PluginRegistryKey.CreateSubKey(BASENAME);
                rkf.SetValue("PersistentHeight", value);
            }
        }

        private int PersistantWidth
        {
            get
            {
                SimPe.XmlRegistryKey rkf = SimPe.Helper.WindowsRegistry.PluginRegistryKey.CreateSubKey(BASENAME);
                object o = rkf.GetValue("PersistentWidth", this.Width);
                return Convert.ToInt32(o);
            }

            set
            {
                SimPe.XmlRegistryKey rkf = SimPe.Helper.WindowsRegistry.PluginRegistryKey.CreateSubKey(BASENAME);
                rkf.SetValue("PersistentWidth", value);
            }
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


        private bool searching = false;
        private int matches = 0;
        private string oldText = null;
        private string prompt = null;
        private Thread searchThread = null;

        private static int byGroupTypeInstance(pjse.FileTable.Entry x, pjse.FileTable.Entry y)
        {
            int result = x.Group.CompareTo(y.Group);
            if (result == 0)
                result = x.Type.CompareTo(y.Type);
            if (result == 0)
                result = x.Instance.CompareTo(y.Instance);
            return result;
        }

        private void Search(object o)
        {
            bool[] type = (bool[])((object[])o)[0];
            bool isFixed = (bool)((object[])o)[1];
            uint searchGUID = (uint)((object[])o)[2];
            string searchText = (string)((object[])o)[3];

            SetProgressCallback setProgress = new SetProgressCallback(SetProgress);
            AddResultCallback addResult = new AddResultCallback(AddResult);
            StopSearchCallback stopSearch = new StopSearchCallback(StopSearch);
            EventHandler onSearchComplete = new EventHandler(OnSearchComplete);

            try
            {
                List<pjse.FileTable.Entry> results = new List<FileTable.Entry>();
                if (type[0] || type[1])
                    results.AddRange(pjse.FileTable.GFT[SimPe.Data.MetaData.OBJD_FILE, isFixed]);
                if (type[2])
                    results.AddRange(pjse.FileTable.GFT[0x4E524546, isFixed]); // NREF
                if (type[3])
                    results.AddRange(pjse.FileTable.GFT[SimPe.Data.MetaData.BHAV_FILE, isFixed]);
                if (type[4])
                    results.AddRange(pjse.FileTable.GFT[0x42434F4E, isFixed]); // BCON

                results.Sort(byGroupTypeInstance);

                Invoke(setProgress, new object[] { false, results.Count });

                int j = 0;
                foreach (pjse.FileTable.Entry item in results)
                {
                    uint itemguid = 0;

                    System.IO.BinaryReader reader = item.Wrapper.StoredData;

                    if (item.Type == SimPe.Data.MetaData.OBJD_FILE)
                        if (reader.BaseStream.Length >= 0x40) // filename length
                            if (reader.BaseStream.Length > 0x5c + 4) // sizeof(uint)
                            {
                                reader.BaseStream.Seek(0x5c, System.IO.SeekOrigin.Begin);
                                itemguid = reader.ReadUInt32();
                            }

                    if ((type[0] && itemguid == searchGUID) ||
                        ((type[1] || type[2] || type[3]) && item.ToString().ToLower().IndexOf(searchText) >= 0))
                        Invoke(addResult, new object[] { itemguid, item, });

                    Invoke(setProgress, new object[] { true, ++j });
                    Thread.Sleep(0);
                    if ((bool)Invoke(stopSearch))
                        break;
                }
            }
            catch (ThreadInterruptedException) { }
            finally
            {
                Thread.Sleep(0);
                BeginInvoke(onSearchComplete, new object[] { this, EventArgs.Empty });
            }
        }

        private delegate void SetProgressCallback(bool maxOrValue, int progress);
        private void SetProgress(bool maxOrValue, int progress)
        {
            if (maxOrValue == false)
                this.progressBar1.Maximum = progress;
            else
                this.progressBar1.Value = progress;
        }

        private delegate void AddResultCallback(uint itemguid, pjse.FileTable.Entry item);
        private void AddResult(uint itemguid, pjse.FileTable.Entry item)
        {
            //string report_line = "Group {0}: [{1} guid: {2}] {3} ({4})";
            if (item.Type == SimPe.Data.MetaData.OBJD_FILE)
            {
                this.rtbReport.AppendText(Localization.GetString("gt_reportOBJD",
                    SimPe.Helper.HexString(item.PFD.Group),
                    item.PFD.TypeName.Name,
                    "0x" + SimPe.Helper.HexString(itemguid),
                    item.ToString(),
                    item.Package.FileName) + "\r\n");
            }
            else
            //string report_line = "Group {0}: [{1} {2}] {3} ({4})";
            {
                this.rtbReport.AppendText(Localization.GetString("gt_report",
                    SimPe.Helper.HexString(item.PFD.Group),
                    item.PFD.TypeName.Name,
                    item.ToString(),
                    item.Package.FileName)+"\r\n");
            }

            this.rtbReport.ScrollToCaret();
            matches++;
        }

        private delegate bool StopSearchCallback();
        private bool StopSearch()
        {
            return !searching;
        }

        private event EventHandler SearchComplete;
        private void OnSearchComplete(object sender, EventArgs e)
        {
            if (SearchComplete != null) { SearchComplete(sender, e); }
        }



        private void Start()
        {
            bool[] type = new bool[] {
                ckbObjdGUID.Checked, ckbObjdName.Checked, ckbNrefName.Checked, ckbBhavName.Checked, ckbBconName.Checked,
            };
            uint guid = 0;
            guid = Convert.ToUInt32(this.tbGUID.Text, 16);
            this.tbGUID.Text = "0x" + SimPe.Helper.HexString(guid);
            if (guid == 0) type[0] = false; // don't search for 0 GUID...

            this.tbName.Text = this.tbName.Text.Trim().ToLower();
            if (this.tbName.Text.Length == 0) { type[1] = type[2] = type[3] = false; } // don't search for empty string

            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            // this.rtbReport.UseWaitCursor = true; // Methods missing from Mono
            this.btnSearch.Cursor = System.Windows.Forms.Cursors.Default;
            this.tbGUID.Enabled = this.tbName.Enabled = this.btnHelp.Enabled = this.btnClose.Enabled = false;
            this.btnSearch.Text = pjse.Localization.GetString("gt_Stop");
            this.lbStatus.Visible = false;
            this.progressBar1.Value = 0;
            this.progressBar1.Visible = true;
            this.rtbReport.Text = "";

            searching = true;
            matches = 0;

            searchThread = new Thread(new ParameterizedThreadStart(Search));
            searchThread.Start(new object[] { type, ckbFixedOnly.Checked, guid, this.tbName.Text });
        }

        private void Stop()
        {
            if (!searching) Complete(null, null);
            else
            {
                this.btnSearch.Enabled = false;
                this.btnSearch.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                searching = false;
            }
        }

        private void Complete(object sender, EventArgs e)
        {
            searching = false;
            while (searchThread != null && searchThread.IsAlive)
                searchThread.Join(10);
            searchThread = null;
            this.Cursor = this.btnSearch.Cursor = System.Windows.Forms.Cursors.Default;
            //this.rtbReport.UseWaitCursor = false; // Methods missing from Mono
            this.tbGUID.Enabled = this.tbName.Enabled = this.btnHelp.Enabled = this.btnClose.Enabled = this.btnSearch.Enabled = true;
            this.btnSearch.Text = oldText;
            this.progressBar1.Value = 0;
            this.progressBar1.Visible = false;
            this.lbStatus.Visible = true;
            if (matches > 0)
                this.lbStatus.Text = pjse.Localization.GetString("gt_MatchesFound") + ": " + matches.ToString();
            else
                this.lbStatus.Text = pjse.Localization.GetString("gt_NoMatchesFound");
        }


        List<TextBox> lHex32 = null;
        private bool hex32_IsValid(object sender)
		{
			if (!(sender is TextBox) || lHex32.IndexOf((TextBox)sender) < 0)
				throw new Exception("hex32_IsValid not applicable to control " + sender.ToString());
			try { Convert.ToUInt32(((TextBox)sender).Text, 16); }
			catch (Exception) { return false; }
			return true;
		}


        #region ITool Members

        public bool IsEnabled(SimPe.Interfaces.Files.IPackedFileDescriptor pfd, SimPe.Interfaces.Files.IPackageFile package)
        {
            return true;
        }

        public SimPe.Interfaces.Plugin.IToolResult ShowDialog(ref SimPe.Interfaces.Files.IPackedFileDescriptor pfd, ref SimPe.Interfaces.Files.IPackageFile package)
        {
            Complete(null, null);
            this.tbGUID.Text = "0x" + SimPe.Helper.HexString((uint)0);
            this.rtbReport.Text = this.tbName.Text = "";
            this.progressBar1.Visible = false;
            this.lbStatus.Text = this.prompt;
            this.lbStatus.Visible = true;

            this.ShowDialog(this.Parent);
            return new SimPe.Plugin.ToolResult(false, false);
        }

        #endregion

        #region IToolPlugin Members

        public override string ToString()
        {
            return "PJSE\\" + pjse.Localization.GetString("gt_ResourceFinder");
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
            this.rtbReport = new System.Windows.Forms.RichTextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lbStatus = new System.Windows.Forms.Label();
            this.btnHelp = new System.Windows.Forms.Button();
            this.gbOptions = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.ckbObjdGUID = new System.Windows.Forms.CheckBox();
            this.ckbObjdName = new System.Windows.Forms.CheckBox();
            this.ckbNrefName = new System.Windows.Forms.CheckBox();
            this.ckbBhavName = new System.Windows.Forms.CheckBox();
            this.ckbBconName = new System.Windows.Forms.CheckBox();
            this.ckbFixedOnly = new System.Windows.Forms.CheckBox();
            this.tbName = new System.Windows.Forms.TextBox();
            this.lbName = new System.Windows.Forms.Label();
            this.tbGUID = new System.Windows.Forms.TextBox();
            this.lbGUID = new System.Windows.Forms.Label();
            this.gbOptions.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // rtbReport
            // 
            resources.ApplyResources(this.rtbReport, "rtbReport");
            this.rtbReport.DetectUrls = false;
            this.rtbReport.Name = "rtbReport";
            this.rtbReport.ReadOnly = true;
            this.rtbReport.ShowSelectionMargin = true;
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
            // lbStatus
            // 
            resources.ApplyResources(this.lbStatus, "lbStatus");
            this.lbStatus.Name = "lbStatus";
            // 
            // btnHelp
            // 
            resources.ApplyResources(this.btnHelp, "btnHelp");
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // gbOptions
            // 
            this.gbOptions.Controls.Add(this.flowLayoutPanel1);
            resources.ApplyResources(this.gbOptions, "gbOptions");
            this.gbOptions.Name = "gbOptions";
            this.gbOptions.TabStop = false;
            // 
            // flowLayoutPanel1
            // 
            resources.ApplyResources(this.flowLayoutPanel1, "flowLayoutPanel1");
            this.flowLayoutPanel1.Controls.Add(this.ckbObjdGUID);
            this.flowLayoutPanel1.Controls.Add(this.ckbObjdName);
            this.flowLayoutPanel1.Controls.Add(this.ckbNrefName);
            this.flowLayoutPanel1.Controls.Add(this.ckbBhavName);
            this.flowLayoutPanel1.Controls.Add(this.ckbBconName);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            // 
            // ckbObjdGUID
            // 
            resources.ApplyResources(this.ckbObjdGUID, "ckbObjdGUID");
            this.ckbObjdGUID.Name = "ckbObjdGUID";
            this.ckbObjdGUID.UseVisualStyleBackColor = true;
            // 
            // ckbObjdName
            // 
            resources.ApplyResources(this.ckbObjdName, "ckbObjdName");
            this.ckbObjdName.Name = "ckbObjdName";
            this.ckbObjdName.UseVisualStyleBackColor = true;
            // 
            // ckbNrefName
            // 
            resources.ApplyResources(this.ckbNrefName, "ckbNrefName");
            this.ckbNrefName.Name = "ckbNrefName";
            this.ckbNrefName.UseVisualStyleBackColor = true;
            // 
            // ckbBhavName
            // 
            resources.ApplyResources(this.ckbBhavName, "ckbBhavName");
            this.ckbBhavName.Name = "ckbBhavName";
            this.ckbBhavName.UseVisualStyleBackColor = true;
            // 
            // ckbBconName
            // 
            resources.ApplyResources(this.ckbBconName, "ckbBconName");
            this.ckbBconName.Name = "ckbBconName";
            this.ckbBconName.UseVisualStyleBackColor = true;
            // 
            // ckbFixedOnly
            // 
            resources.ApplyResources(this.ckbFixedOnly, "ckbFixedOnly");
            this.ckbFixedOnly.Name = "ckbFixedOnly";
            this.ckbFixedOnly.UseVisualStyleBackColor = true;
            // 
            // tbName
            // 
            resources.ApplyResources(this.tbName, "tbName");
            this.tbName.Name = "tbName";
            // 
            // lbName
            // 
            resources.ApplyResources(this.lbName, "lbName");
            this.lbName.Name = "lbName";
            // 
            // tbGUID
            // 
            resources.ApplyResources(this.tbGUID, "tbGUID");
            this.tbGUID.Name = "tbGUID";
            // 
            // lbGUID
            // 
            resources.ApplyResources(this.lbGUID, "lbGUID");
            this.lbGUID.Name = "lbGUID";
            // 
            // GUIDTool
            // 
            this.AcceptButton = this.btnSearch;
            resources.ApplyResources(this, "$this");
            this.CancelButton = this.btnClose;
            this.Controls.Add(this.lbGUID);
            this.Controls.Add(this.tbGUID);
            this.Controls.Add(this.lbName);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.ckbFixedOnly);
            this.Controls.Add(this.gbOptions);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.lbStatus);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.rtbReport);
            this.Name = "GUIDTool";
            this.gbOptions.ResumeLayout(false);
            this.gbOptions.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

        protected override void OnClosing(CancelEventArgs e)
        {
            PersistantHeight = this.Height;
            PersistantWidth = this.Width;
            searching = false;
            if (searchThread != null && searchThread.IsAlive)
            {
                searchThread.Interrupt();
                searchThread.Join();
                searchThread = null;
            }
            base.OnClosing(e);
        }

		private void hex32_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (hex32_IsValid(sender)) return;

			e.Cancel = true;

			uint val = 0;
			switch (lHex32.IndexOf((TextBox)sender))
			{
				case 0: val = 0; break;
			}

			((TextBox)sender).Text = "0x" + SimPe.Helper.HexString(val);
			((TextBox)sender).SelectAll();
		}

        private void btnSearch_Click(object sender, System.EventArgs e)
        {
            if (searching)
                Stop();
            else
                Start();
        }

		private void btnHelp_Click(object sender, System.EventArgs e)
		{
			System.Windows.Forms.MessageBox.Show(
                pjse.Localization.GetString("gt_ResourceFinderHelp"),
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
