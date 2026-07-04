/***************************************************************************
 *   Copyright (C) 2005-2008 by Peter L Jones                              *
 *   pljones@users.sf.net                                                  *
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
using SimPe.PackedFiles.Wrapper;

namespace pjse.guidtool
{
	/// <summary>
	/// Summary description for GUIDForm.
	/// </summary>
    public class GUIDForm : System.Windows.Forms.Form
    {
        #region Form variables

        private booby.ExtProgressBar progressBar1;
        private System.Windows.Forms.Label lbStatus;
        private RichTextBox rtbReport;
        private TextBox tbNumber;
        private Label lbName;
        private TextBox tbName;
        private Label lbNumber;
        private Button btnSearch;
        private Button btnClose;
        private GroupBox groupBox1;
        private CheckBox ckbObjdGUID;
        private CheckBox ckbObjdName;
        private CheckBox ckbNrefName;
        private CheckBox ckbBhavName;
        private CheckBox ckbBconName;
        private GroupBox groupBox2;
        private RadioButton rb1default;
        private RadioButton rb1CPOnly;
        private Button btnHelp;
        private CheckBox ckbCallsToBHAV;
        private SimPe.Plugin.GUIDChooser gcGroup;
        private Button btnClearFilter;
        private CheckBox ckbSGSearch;
        private Label label1;
        private CheckBox ckbFromBHAV;
        private CheckBox ckbFromObjf;
        private CheckBox ckbFromTtab;
        private CheckBox ckbGLOB;
        private Label label2;
        private CheckBox ckbSTR;
        private CheckBox ckbCTSS;
        private CheckBox ckbTTAs;
        private CheckBox ckbDefLang;
        private booby.gradientpanel panel1;
        private Panel pnFixer;
        private Button btclipb;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

        #endregion

        public GUIDForm(bool packageloaded)
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            rb1CPOnly.Enabled = packageloaded;
            if (!rb1CPOnly.Enabled && rb1CPOnly.Checked)
                rb1default.Checked = true;


            if (pjse.FileTable.gft == null)
                pjse.FileTable.GFT.Refresh();

            booby.ThemeManager tm = booby.ThemeManager.Global.CreateChild();
            if (booby.ThemeManager.ThemedForms)
            {
                tm.AddControl(this.panel1);
                tm.AddControl(this.rtbReport);
                tm.AddControl(this.btnSearch);
                tm.AddControl(this.btnClose);
                tm.AddControl(this.btnClearFilter);
                tm.AddControl(this.btnHelp);
                tm.AddControl(this.btclipb);
                this.progressBar1.GradientEndColor = booby.ThemeManager.Global.ThemeColorLighter;
                this.progressBar1.GradientStartColor = booby.ThemeManager.Global.ThemeColorLighter;
                this.progressBar1.SelectedColor = booby.ThemeManager.Global.ThemeColorDark;
            }

            lHex32 = new List<TextBox>(new TextBox[] { tbNumber, });
            rbGroup = new List<RadioButton>(new RadioButton[] { rb1default, rb1CPOnly });

            this.oldText = this.btnSearch.Text;

            SearchComplete += new EventHandler(Complete);

            #region Group filter
            sgNames = new List<string>();
            sgGroups = new List<uint>();
            sgNames.Add("Globals");
            sgGroups.Add(0x7FD46CD0);
            sgNames.Add("Behaviour");
            sgGroups.Add(0x7FE59FD0);
            foreach (SimPe.Data.SemiGlobalAlias sga in SimPe.Data.MetaData.SemiGlobals)
                if (sga.Known)
                {
                    sgNames.Add(sga.Name);
                    sgGroups.Add(sga.Id);
                }

            gcGroup.KnownObjects = new object[] { sgNames, sgGroups, };
            if (booby.PrettyGirls.PervyMode || booby.PrettyGirls.IsAngelsInstalled()) this.panel1.BackgroundImage = booby.PrettyGirls.BowPeep;
            gcGroup.ComboBoxWidth = 420;
            #endregion
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
        //private string prompt = null;
        private Thread searchThread = null;

        private List<String> sgNames = null;
        private List<UInt32> sgGroups = null;

        private List<RadioButton> rbGroup = null;
        private static bool Selected(RadioButton rb) { return rb.Checked; }

        private static int byPackageGroupTypeInstance(pjse.FileTable.Entry x, pjse.FileTable.Entry y)
        {
            int result = x.Package.FileName.CompareTo(y.Package.FileName);
            if (result == 0)
                result = x.Group.CompareTo(y.Group);
            if (result == 0)
                result = x.Type.CompareTo(y.Type);
            if (result == 0)
                result = x.Instance.CompareTo(y.Instance);
            return result;
        }

        private void Search(object o)
        {
            bool[] type = (bool[])((object[])o)[0];
            FileTable.Source where = (FileTable.Source)((object[])o)[1];
            uint searchNumber = (uint)((object[])o)[2];
            string searchText = (string)((object[])o)[3];
            uint group = (uint)((object[])o)[4];

            SetProgressCallback setProgress = new SetProgressCallback(SetProgress);
            AddResultCallback addResult = new AddResultCallback(AddResult);
            StopSearchCallback stopSearch = new StopSearchCallback(StopSearch);
            EventHandler onSearchComplete = new EventHandler(OnSearchComplete);

            try
            {
                List<pjse.FileTable.Entry> results = new List<FileTable.Entry>();
                if (group != 0)
                {
                    if (type[6])
                    #region Focus on SemiGlobal group
                    {
                        List<pjse.FileTable.Entry> globs = new List<FileTable.Entry>(pjse.FileTable.GFT[SimPe.Data.MetaData.GLOB_FILE, where]);
                        foreach (pjse.FileTable.Entry fte in globs)
                        {
                            SimPe.Plugin.Glob glob = ((SimPe.Plugin.Glob)fte.Wrapper);
                            if (glob == null) continue;
                            if (group != glob.SemiGlobalGroup) continue;

                            List<pjse.FileTable.Entry> temp = new List<FileTable.Entry>();
                            if (type[7]) temp.AddRange(pjse.FileTable.GFT[Bhav.Bhavtype, fte.Group, where]);
                            if (type[8]) temp.AddRange(pjse.FileTable.GFT[Objf.Objftype, fte.Group, where]);
                            if (type[9]) temp.AddRange(pjse.FileTable.GFT[Ttab.Ttabtype, fte.Group, where]);

                            if (fte.Group == 0xffffffff)
                            {
                                foreach (pjse.FileTable.Entry entry in temp)
                                    if (entry.Package == fte.Package) results.Add(entry);
                            }
                            else results.AddRange(temp);
                        }
                        if (type[7]) results.AddRange(pjse.FileTable.GFT[Bhav.Bhavtype, group, where]);
                        if (type[8]) results.AddRange(pjse.FileTable.GFT[Objf.Objftype, group, where]);
                        if (type[9]) results.AddRange(pjse.FileTable.GFT[Ttab.Ttabtype, group, where]);
                    }
                    #endregion
                    else if (type[10])
                    #region References to GLOB
                    {
                        List<pjse.FileTable.Entry> globs = new List<FileTable.Entry>(pjse.FileTable.GFT[SimPe.Data.MetaData.GLOB_FILE, where]);
                        foreach (pjse.FileTable.Entry fte in globs)
                        {
                            SimPe.Plugin.Glob glob = ((SimPe.Plugin.Glob)fte.Wrapper);
                            if (glob == null) continue;
                            if (group != glob.SemiGlobalGroup) continue;

                            pjse.FileTable.Entry[] objds = pjse.FileTable.GFT[SimPe.Data.MetaData.OBJD_FILE, fte.Group, where];

                            if (objds.Length == 0)
                                results.Add(fte);
                            else
                            {
                                if (fte.Group == 0xffffffff)
                                {
                                    foreach(pjse.FileTable.Entry entry in objds)
                                        if (entry.Package == fte.Package)
                                        {
                                            results.Add(entry);
                                            break;
                                        }
                                }
                                else
                                    results.Add(objds[0]);
                            }
                        }
                    }
                    #endregion
                    else
                    #region Search within group
                    {
                        if (type[0] || type[1])
                            results.AddRange(pjse.FileTable.GFT[SimPe.Data.MetaData.OBJD_FILE, group, where]);
                        if (type[2])
                            results.AddRange(pjse.FileTable.GFT[0x4E524546, group, where]); // NREF
                        if (type[3])
                            results.AddRange(pjse.FileTable.GFT[Bhav.Bhavtype, group, where]);
                        if (type[4])
                            results.AddRange(pjse.FileTable.GFT[Bcon.Bcontype, group, where]);
                        if (type[5])
                        {
                            if (type[7]) results.AddRange(pjse.FileTable.GFT[Bhav.Bhavtype, group, where]);
                            if (type[8]) results.AddRange(pjse.FileTable.GFT[Objf.Objftype, group, where]);
                            if (type[9]) results.AddRange(pjse.FileTable.GFT[Ttab.Ttabtype, group, where]);
                        }
                        if (type[11])
                            results.AddRange(pjse.FileTable.GFT[StrWrapper.Strtype, group, where]);
                        if (type[12])
                            results.AddRange(pjse.FileTable.GFT[StrWrapper.CTSStype, group, where]);
                        if (type[13])
                            results.AddRange(pjse.FileTable.GFT[StrWrapper.TTAstype, group, where]);
                    }
                    #endregion
                }
                else // group == 0
                {
                    if (type[6] || type[10]) { } // no results for group == 0
                    else
                    #region Search without group
                    {
                        if (type[0] || type[1])
                            results.AddRange(pjse.FileTable.GFT[SimPe.Data.MetaData.OBJD_FILE, where]);
                        if (type[2])
                            results.AddRange(pjse.FileTable.GFT[0x4E524546, where]); // NREF
                        if (type[3])
                            results.AddRange(pjse.FileTable.GFT[Bhav.Bhavtype, where]);
                        if (type[4])
                            results.AddRange(pjse.FileTable.GFT[Bcon.Bcontype, where]);
                        if (type[5])
                        {
                            if (type[7]) results.AddRange(pjse.FileTable.GFT[Bhav.Bhavtype, where]);
                            if (type[8]) results.AddRange(pjse.FileTable.GFT[Objf.Objftype, where]);
                            if (type[9]) results.AddRange(pjse.FileTable.GFT[Ttab.Ttabtype, where]);
                        }
                        if (type[11])
                            results.AddRange(pjse.FileTable.GFT[StrWrapper.Strtype, where]);
                        if (type[12])
                            results.AddRange(pjse.FileTable.GFT[StrWrapper.CTSStype, where]);
                        if (type[13])
                            results.AddRange(pjse.FileTable.GFT[StrWrapper.TTAstype, where]);
                    }
                    #endregion
                }

                results.Sort(byPackageGroupTypeInstance);

                Invoke(setProgress, new object[] { false, results.Count });

                int j = 0;
                pjse.FileTable.Entry previtem = null;
                foreach (pjse.FileTable.Entry item in results)
                {
                    if (item != previtem)
                    {
                        previtem = item;

                        uint itemguid = 0;

                        System.IO.BinaryReader reader = item.Wrapper.StoredData;
                        if (item.Type == SimPe.Data.MetaData.OBJD_FILE)
                            if (reader.BaseStream.Length > 0x5c + 4) // sizeof(uint)
                            {
                                reader.BaseStream.Seek(0x5c, System.IO.SeekOrigin.Begin);
                                itemguid = reader.ReadUInt32();
                            }

                        if ((type[0] && itemguid == searchNumber) ||
                            ((type[1] || type[2] || type[3]) && item.ToString().ToLower().Contains(searchText)) ||
                            type[10])
                            Invoke(addResult, new object[] { itemguid, item, });

                        else if (type[5]) switch (item.Type)
                            {
                                case Bhav.Bhavtype:
                                    foreach (Instruction i in (Bhav)item.Wrapper)
                                        if (i.OpCode == searchNumber)
                                            Invoke(addResult, new object[] { itemguid, item, });
                                    break;
                                case Objf.Objftype:
                                    foreach (ObjfItem i in (Objf)item.Wrapper)
                                        if (i.Action == searchNumber || i.Guardian == searchNumber)
                                            Invoke(addResult, new object[] { itemguid, item, });
                                    break;
                                case Ttab.Ttabtype:
                                    foreach (TtabItem i in (Ttab)item.Wrapper)
                                        if (i.Action == searchNumber || i.Guardian == searchNumber)
                                            Invoke(addResult, new object[] { itemguid, item, });
                                    break;
                            }

                        else if (((type[11] && item.Type == StrWrapper.Strtype) ||
                          (type[12] && item.Type == StrWrapper.CTSStype) ||
                          (type[13] && item.Type == StrWrapper.TTAstype)))
                        {
                            if (type[14])
                                foreach (StrItem si in ((StrWrapper)item.Wrapper)[(byte)1])
                                {
                                    if (si.Title.ToString().ToLower().Contains(searchText))
                                    {
                                        Invoke(addResult, new object[] { itemguid, item, });
                                        break;
                                    }
                                }
                            else
                                foreach (StrItem si in (StrWrapper)item.Wrapper)
                                {
                                    if (si.Title.ToString().ToLower().Contains(searchText))
                                    {
                                        Invoke(addResult, new object[] { itemguid, item, });
                                        break;
                                    }
                                }
                        }
                    }
                //DealtWith:
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
            {
                SimPe.WaitingScreen.Stop();
                this.progressBar1.Maximum = progress;
            }
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
                /*0*/ckbObjdGUID.Checked, ckbObjdName.Checked, ckbNrefName.Checked, ckbBhavName.Checked, ckbBconName.Checked,
                /*5*/ckbCallsToBHAV.Checked, ckbSGSearch.Checked, ckbFromBHAV.Checked, ckbFromObjf.Checked, ckbFromTtab.Checked,
                /*10*/ckbGLOB.Checked, ckbSTR.Checked, ckbCTSS.Checked, ckbTTAs.Checked, ckbDefLang.Checked,
            };
            uint number = 0;
            try { number = Convert.ToUInt32(this.tbNumber.Text.Trim(), 16); }
            catch(System.FormatException) { number = 0; }
            this.tbNumber.Text = "0x" + SimPe.Helper.HexString(number);
            if (number == 0) { type[0] = type[5] = false; } // don't search for 0 GUID...
            if (number < 0x2000 || number > 0x2fff) { type[6] = false; } // don't do SG search except for SG BHAVs...
            if (gcGroup.Value == 0) { type[6] = type[10] = false; } // don't search with no Group filter
            this.tbName.Text = this.tbName.Text.Trim().ToLower();
            if (this.tbName.Text.Length == 0) { type[1] = type[2] = type[3] = type[4] = type[11] = type[12] = type[13] = false; } // don't search for empty string
            SimPe.WaitingScreen.Wait();
            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.btnSearch.Cursor = System.Windows.Forms.Cursors.Default;
            groupBox1.Enabled = false;
            ckbObjdGUID.Enabled = ckbCallsToBHAV.Enabled = ckbFromBHAV.Enabled = ckbFromObjf.Enabled = ckbFromTtab.Enabled = false;
            gcGroup.Enabled = ckbSGSearch.Enabled = btnClearFilter.Enabled = tbNumber.Enabled = tbName.Enabled = this.btnClose.Enabled = false;
            this.btnSearch.Text = pjse.Localization.GetString("gt_Stop");
            this.lbStatus.Visible = this.btclipb.Visible = false;
            this.progressBar1.Value = 0;
            this.progressBar1.Visible = true;
            this.rtbReport.Text = "";

            searching = true;
            matches = 0;

            FileTable.Source[] aS = new FileTable.Source[] { FileTable.Source.Any, FileTable.Source.Local };
            FileTable.Source s;
            int rbS = rbGroup.FindIndex(Selected);

            s = (rbS >= 0 && rbS < aS.Length) ? aS[rbS] : FileTable.Source.Any;

            searchThread = new Thread(new ParameterizedThreadStart(Search));
            searchThread.Start(new object[] { type, s, number, this.tbName.Text, gcGroup.Value });
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

        internal void Complete(object sender, EventArgs e)
        {
            searching = false;
            while (searchThread != null && searchThread.IsAlive)
                searchThread.Join(10);
            searchThread = null;
            this.Cursor = this.btnSearch.Cursor = System.Windows.Forms.Cursors.Default;
            ckbObjdGUID.Enabled = ckbCallsToBHAV.Enabled = ckbFromBHAV.Enabled = ckbFromObjf.Enabled = ckbFromTtab.Enabled = gcGroup.Enabled = true;
            ckbSGSearch.Enabled = btnClearFilter.Enabled = tbNumber.Enabled = tbName.Enabled = this.btnClose.Enabled = this.btnSearch.Enabled = true;
            groupBox1.Enabled = true;
            this.btnSearch.Text = oldText;
            this.progressBar1.Value = 0;
            this.progressBar1.Visible = false;
            this.lbStatus.Visible = true;
            if (matches > 0)
            {
                this.lbStatus.Text = pjse.Localization.GetString("gt_MatchesFound") + ": " + matches.ToString();
                this.btclipb.Visible = true;
            }
            else
            {
                this.lbStatus.Text = pjse.Localization.GetString("gt_NoMatchesFound");
                this.btclipb.Visible = false;
            }
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

        #region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GUIDForm));
            this.progressBar1 = new booby.ExtProgressBar();
            this.lbStatus = new System.Windows.Forms.Label();
            this.rtbReport = new System.Windows.Forms.RichTextBox();
            this.lbNumber = new System.Windows.Forms.Label();
            this.tbNumber = new System.Windows.Forms.TextBox();
            this.lbName = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.gcGroup = new SimPe.Plugin.GUIDChooser();
            this.ckbSGSearch = new System.Windows.Forms.CheckBox();
            this.btnClearFilter = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ckbObjdName = new System.Windows.Forms.CheckBox();
            this.ckbGLOB = new System.Windows.Forms.CheckBox();
            this.ckbSTR = new System.Windows.Forms.CheckBox();
            this.ckbObjdGUID = new System.Windows.Forms.CheckBox();
            this.ckbCallsToBHAV = new System.Windows.Forms.CheckBox();
            this.ckbCTSS = new System.Windows.Forms.CheckBox();
            this.ckbNrefName = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ckbTTAs = new System.Windows.Forms.CheckBox();
            this.ckbFromTtab = new System.Windows.Forms.CheckBox();
            this.ckbBhavName = new System.Windows.Forms.CheckBox();
            this.ckbDefLang = new System.Windows.Forms.CheckBox();
            this.ckbFromObjf = new System.Windows.Forms.CheckBox();
            this.ckbFromBHAV = new System.Windows.Forms.CheckBox();
            this.ckbBconName = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rb1default = new System.Windows.Forms.RadioButton();
            this.rb1CPOnly = new System.Windows.Forms.RadioButton();
            this.btnHelp = new System.Windows.Forms.Button();
            this.panel1 = new booby.gradientpanel();
            this.btclipb = new System.Windows.Forms.Button();
            this.pnFixer = new System.Windows.Forms.Panel();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnFixer.SuspendLayout();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.BackColor = System.Drawing.Color.Transparent;
            this.progressBar1.BorderColor = System.Drawing.Color.Black;
            this.progressBar1.Gradient = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.progressBar1.GradientEndColor = System.Drawing.Color.White;
            this.progressBar1.GradientStartColor = System.Drawing.Color.White;
            this.progressBar1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.progressBar1.Location = new System.Drawing.Point(-2, 512);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(2);
            this.progressBar1.Maximum = 100;
            this.progressBar1.Minimum = 0;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.ProgressBackColor = System.Drawing.SystemColors.Window;
            this.progressBar1.Quality = true;
            this.progressBar1.SelectedColor = System.Drawing.Color.Orange;
            this.progressBar1.Size = new System.Drawing.Size(781, 22);
            this.progressBar1.Style = booby.ProgresBarStyle.Simple;
            this.progressBar1.TabIndex = 0;
            this.progressBar1.TokenCount = 34;
            this.progressBar1.UnselectedColor = System.Drawing.Color.Black;
            this.progressBar1.UseTokenBuffer = false;
            this.progressBar1.Value = 0;
            this.progressBar1.Visible = false;
            // 
            // lbStatus
            // 
            this.lbStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbStatus.AutoSize = true;
            this.lbStatus.BackColor = System.Drawing.Color.Transparent;
            this.lbStatus.Font = new System.Drawing.Font("Comic Sans MS", 9.75F);
            this.lbStatus.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbStatus.Location = new System.Drawing.Point(0, 492);
            this.lbStatus.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Size = new System.Drawing.Size(765, 18);
            this.lbStatus.TabIndex = 0;
            this.lbStatus.Text = "Set the search type, key in the GUID and/or (part of) the resource name/string te" +
                "xt, or the BHAV number, then click Search";
            // 
            // rtbReport
            // 
            this.rtbReport.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbReport.BackColor = System.Drawing.SystemColors.Window;
            this.rtbReport.DetectUrls = false;
            this.rtbReport.Font = new System.Drawing.Font("Lucida Console", 8.25F);
            this.rtbReport.Location = new System.Drawing.Point(2, 287);
            this.rtbReport.Margin = new System.Windows.Forms.Padding(2);
            this.rtbReport.Name = "rtbReport";
            this.rtbReport.ReadOnly = true;
            this.rtbReport.ShowSelectionMargin = true;
            this.rtbReport.Size = new System.Drawing.Size(774, 201);
            this.rtbReport.TabIndex = 5;
            this.rtbReport.Text = "";
            this.rtbReport.WordWrap = false;
            // 
            // lbNumber
            // 
            this.lbNumber.AutoSize = true;
            this.lbNumber.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbNumber.Location = new System.Drawing.Point(14, 206);
            this.lbNumber.Margin = new System.Windows.Forms.Padding(0);
            this.lbNumber.Name = "lbNumber";
            this.lbNumber.Size = new System.Drawing.Size(38, 13);
            this.lbNumber.TabIndex = 1;
            this.lbNumber.Text = "GUID";
            // 
            // tbNumber
            // 
            this.tbNumber.Enabled = false;
            this.tbNumber.Location = new System.Drawing.Point(56, 202);
            this.tbNumber.Margin = new System.Windows.Forms.Padding(0);
            this.tbNumber.MaxLength = 10;
            this.tbNumber.Name = "tbNumber";
            this.tbNumber.Size = new System.Drawing.Size(80, 21);
            this.tbNumber.TabIndex = 2;
            this.tbNumber.Text = "0x00000000";
            // 
            // lbName
            // 
            this.lbName.AutoSize = true;
            this.lbName.Enabled = false;
            this.lbName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbName.Location = new System.Drawing.Point(20, 232);
            this.lbName.Margin = new System.Windows.Forms.Padding(18, 0, 0, 0);
            this.lbName.Name = "lbName";
            this.lbName.Size = new System.Drawing.Size(31, 13);
            this.lbName.TabIndex = 3;
            this.lbName.Text = "Text";
            // 
            // tbName
            // 
            this.tbName.Enabled = false;
            this.tbName.Location = new System.Drawing.Point(56, 228);
            this.tbName.Margin = new System.Windows.Forms.Padding(0);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(481, 21);
            this.tbName.TabIndex = 4;
            // 
            // gcGroup
            // 
            this.gcGroup.AutoSize = true;
            this.gcGroup.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gcGroup.BackColor = System.Drawing.Color.Transparent;
            this.gcGroup.ComboBoxWidth = 570;
            this.gcGroup.DropDownHeight = 250;
            this.gcGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.gcGroup.DropDownWidth = 420;
            this.gcGroup.Label = "Group Filter:";
            this.gcGroup.Location = new System.Drawing.Point(0, 0);
            this.gcGroup.Margin = new System.Windows.Forms.Padding(0);
            this.gcGroup.MaximumSize = new System.Drawing.Size(587, 23);
            this.gcGroup.Name = "gcGroup";
            this.gcGroup.Size = new System.Drawing.Size(587, 21);
            this.gcGroup.TabIndex = 1;
            this.gcGroup.Value = ((uint)(0u));
            // 
            // ckbSGSearch
            // 
            this.ckbSGSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ckbSGSearch.AutoSize = true;
            this.ckbSGSearch.BackColor = System.Drawing.Color.Transparent;
            this.ckbSGSearch.Enabled = false;
            this.ckbSGSearch.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbSGSearch.Location = new System.Drawing.Point(594, 262);
            this.ckbSGSearch.Margin = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.ckbSGSearch.Name = "ckbSGSearch";
            this.ckbSGSearch.Size = new System.Drawing.Size(76, 17);
            this.ckbSGSearch.TabIndex = 2;
            this.ckbSGSearch.Text = "As Focus";
            this.ckbSGSearch.UseVisualStyleBackColor = false;
            // 
            // btnClearFilter
            // 
            this.btnClearFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearFilter.AutoSize = true;
            this.btnClearFilter.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnClearFilter.BackColor = System.Drawing.Color.Transparent;
            this.btnClearFilter.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnClearFilter.Location = new System.Drawing.Point(684, 256);
            this.btnClearFilter.Margin = new System.Windows.Forms.Padding(0);
            this.btnClearFilter.Name = "btnClearFilter";
            this.btnClearFilter.Size = new System.Drawing.Size(96, 23);
            this.btnClearFilter.TabIndex = 3;
            this.btnClearFilter.Text = "Remove Filter";
            this.btnClearFilter.UseVisualStyleBackColor = false;
            this.btnClearFilter.Click += new System.EventHandler(this.btnClearFilter_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.BackColor = System.Drawing.Color.Transparent;
            this.btnSearch.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnSearch.Location = new System.Drawing.Point(705, 10);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(2, 10, 9, 2);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(74, 22);
            this.btnSearch.TabIndex = 7;
            this.btnSearch.Text = "&Search";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnClose.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnClose.Location = new System.Drawing.Point(705, 56);
            this.btnClose.Margin = new System.Windows.Forms.Padding(2, 10, 0, 2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(74, 22);
            this.btnClose.TabIndex = 8;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.ckbObjdName);
            this.groupBox1.Controls.Add(this.ckbGLOB);
            this.groupBox1.Controls.Add(this.ckbSTR);
            this.groupBox1.Controls.Add(this.ckbObjdGUID);
            this.groupBox1.Controls.Add(this.ckbCallsToBHAV);
            this.groupBox1.Controls.Add(this.ckbCTSS);
            this.groupBox1.Controls.Add(this.ckbNrefName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.ckbTTAs);
            this.groupBox1.Controls.Add(this.ckbFromTtab);
            this.groupBox1.Controls.Add(this.ckbBhavName);
            this.groupBox1.Controls.Add(this.ckbDefLang);
            this.groupBox1.Controls.Add(this.ckbFromObjf);
            this.groupBox1.Controls.Add(this.ckbFromBHAV);
            this.groupBox1.Controls.Add(this.ckbBconName);
            this.groupBox1.Location = new System.Drawing.Point(19, 58);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.groupBox1.Size = new System.Drawing.Size(435, 135);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Search resources &for:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 76);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Strings in:";
            // 
            // ckbObjdName
            // 
            this.ckbObjdName.AutoSize = true;
            this.ckbObjdName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbObjdName.Location = new System.Drawing.Point(5, 44);
            this.ckbObjdName.Margin = new System.Windows.Forms.Padding(0);
            this.ckbObjdName.Name = "ckbObjdName";
            this.ckbObjdName.Size = new System.Drawing.Size(94, 17);
            this.ckbObjdName.TabIndex = 1;
            this.ckbObjdName.Text = "OBJD Name";
            this.ckbObjdName.CheckedChanged += new System.EventHandler(this.ckbSomeName_CheckedChanged);
            // 
            // ckbGLOB
            // 
            this.ckbGLOB.AutoSize = true;
            this.ckbGLOB.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbGLOB.Location = new System.Drawing.Point(6, 103);
            this.ckbGLOB.Margin = new System.Windows.Forms.Padding(0);
            this.ckbGLOB.Name = "ckbGLOB";
            this.ckbGLOB.Size = new System.Drawing.Size(280, 17);
            this.ckbGLOB.TabIndex = 4;
            this.ckbGLOB.Text = "Objects for GLOBs with group in Group Filter";
            this.ckbGLOB.CheckedChanged += new System.EventHandler(this.ckbGLOB_CheckedChanged);
            // 
            // ckbSTR
            // 
            this.ckbSTR.AutoSize = true;
            this.ckbSTR.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbSTR.Location = new System.Drawing.Point(80, 74);
            this.ckbSTR.Margin = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.ckbSTR.Name = "ckbSTR";
            this.ckbSTR.Size = new System.Drawing.Size(58, 17);
            this.ckbSTR.TabIndex = 2;
            this.ckbSTR.Text = "STR#";
            this.ckbSTR.UseVisualStyleBackColor = true;
            this.ckbSTR.CheckedChanged += new System.EventHandler(this.ckbSomeName_CheckedChanged);
            // 
            // ckbObjdGUID
            // 
            this.ckbObjdGUID.AutoSize = true;
            this.ckbObjdGUID.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbObjdGUID.Location = new System.Drawing.Point(6, 16);
            this.ckbObjdGUID.Margin = new System.Windows.Forms.Padding(0);
            this.ckbObjdGUID.Name = "ckbObjdGUID";
            this.ckbObjdGUID.Size = new System.Drawing.Size(57, 17);
            this.ckbObjdGUID.TabIndex = 1;
            this.ckbObjdGUID.Text = "GUID";
            this.ckbObjdGUID.CheckedChanged += new System.EventHandler(this.ckbObjdGUID_CheckedChanged);
            // 
            // ckbCallsToBHAV
            // 
            this.ckbCallsToBHAV.AutoSize = true;
            this.ckbCallsToBHAV.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbCallsToBHAV.Location = new System.Drawing.Point(73, 16);
            this.ckbCallsToBHAV.Margin = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.ckbCallsToBHAV.Name = "ckbCallsToBHAV";
            this.ckbCallsToBHAV.Size = new System.Drawing.Size(119, 17);
            this.ckbCallsToBHAV.TabIndex = 2;
            this.ckbCallsToBHAV.Text = "Calls to OpCode";
            this.ckbCallsToBHAV.UseVisualStyleBackColor = true;
            this.ckbCallsToBHAV.CheckedChanged += new System.EventHandler(this.ckbCallsToBHAV_CheckedChanged);
            // 
            // ckbCTSS
            // 
            this.ckbCTSS.AutoSize = true;
            this.ckbCTSS.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbCTSS.Location = new System.Drawing.Point(149, 74);
            this.ckbCTSS.Margin = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.ckbCTSS.Name = "ckbCTSS";
            this.ckbCTSS.Size = new System.Drawing.Size(58, 17);
            this.ckbCTSS.TabIndex = 3;
            this.ckbCTSS.Text = "CTSS";
            this.ckbCTSS.UseVisualStyleBackColor = true;
            this.ckbCTSS.CheckedChanged += new System.EventHandler(this.ckbSomeName_CheckedChanged);
            // 
            // ckbNrefName
            // 
            this.ckbNrefName.AutoSize = true;
            this.ckbNrefName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbNrefName.Location = new System.Drawing.Point(106, 44);
            this.ckbNrefName.Margin = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.ckbNrefName.Name = "ckbNrefName";
            this.ckbNrefName.Size = new System.Drawing.Size(92, 17);
            this.ckbNrefName.TabIndex = 2;
            this.ckbNrefName.Text = "NREF Name";
            this.ckbNrefName.CheckedChanged += new System.EventHandler(this.ckbSomeName_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(189, 17);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "From:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ckbTTAs
            // 
            this.ckbTTAs.AutoSize = true;
            this.ckbTTAs.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbTTAs.Location = new System.Drawing.Point(217, 74);
            this.ckbTTAs.Margin = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.ckbTTAs.Name = "ckbTTAs";
            this.ckbTTAs.Size = new System.Drawing.Size(53, 17);
            this.ckbTTAs.TabIndex = 4;
            this.ckbTTAs.Text = "TTAs";
            this.ckbTTAs.UseVisualStyleBackColor = true;
            this.ckbTTAs.CheckedChanged += new System.EventHandler(this.ckbSomeName_CheckedChanged);
            // 
            // ckbFromTtab
            // 
            this.ckbFromTtab.AutoSize = true;
            this.ckbFromTtab.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbFromTtab.Location = new System.Drawing.Point(369, 16);
            this.ckbFromTtab.Margin = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.ckbFromTtab.Name = "ckbFromTtab";
            this.ckbFromTtab.Size = new System.Drawing.Size(55, 17);
            this.ckbFromTtab.TabIndex = 4;
            this.ckbFromTtab.Text = "TTAB";
            // 
            // ckbBhavName
            // 
            this.ckbBhavName.AutoSize = true;
            this.ckbBhavName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbBhavName.Location = new System.Drawing.Point(205, 44);
            this.ckbBhavName.Margin = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.ckbBhavName.Name = "ckbBhavName";
            this.ckbBhavName.Size = new System.Drawing.Size(95, 17);
            this.ckbBhavName.TabIndex = 3;
            this.ckbBhavName.Text = "BHAV Name";
            this.ckbBhavName.CheckedChanged += new System.EventHandler(this.ckbSomeName_CheckedChanged);
            // 
            // ckbDefLang
            // 
            this.ckbDefLang.AutoSize = true;
            this.ckbDefLang.Enabled = false;
            this.ckbDefLang.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbDefLang.Location = new System.Drawing.Point(282, 74);
            this.ckbDefLang.Margin = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.ckbDefLang.Name = "ckbDefLang";
            this.ckbDefLang.Size = new System.Drawing.Size(128, 17);
            this.ckbDefLang.TabIndex = 5;
            this.ckbDefLang.Text = "Default Lang Only";
            this.ckbDefLang.UseVisualStyleBackColor = true;
            // 
            // ckbFromObjf
            // 
            this.ckbFromObjf.AutoSize = true;
            this.ckbFromObjf.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbFromObjf.Location = new System.Drawing.Point(303, 16);
            this.ckbFromObjf.Margin = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.ckbFromObjf.Name = "ckbFromObjf";
            this.ckbFromObjf.Size = new System.Drawing.Size(54, 17);
            this.ckbFromObjf.TabIndex = 3;
            this.ckbFromObjf.Text = "OBJF";
            // 
            // ckbFromBHAV
            // 
            this.ckbFromBHAV.AutoSize = true;
            this.ckbFromBHAV.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbFromBHAV.Location = new System.Drawing.Point(236, 16);
            this.ckbFromBHAV.Margin = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.ckbFromBHAV.Name = "ckbFromBHAV";
            this.ckbFromBHAV.Size = new System.Drawing.Size(58, 17);
            this.ckbFromBHAV.TabIndex = 2;
            this.ckbFromBHAV.Text = "BHAV";
            // 
            // ckbBconName
            // 
            this.ckbBconName.AutoSize = true;
            this.ckbBconName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbBconName.Location = new System.Drawing.Point(305, 44);
            this.ckbBconName.Margin = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.ckbBconName.Name = "ckbBconName";
            this.ckbBconName.Size = new System.Drawing.Size(97, 17);
            this.ckbBconName.TabIndex = 4;
            this.ckbBconName.Text = "BCON Name";
            this.ckbBconName.CheckedChanged += new System.EventHandler(this.ckbSomeName_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.rb1default);
            this.groupBox2.Controls.Add(this.rb1CPOnly);
            this.groupBox2.Location = new System.Drawing.Point(19, 9);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.groupBox2.Size = new System.Drawing.Size(324, 49);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Search for resources &in:";
            // 
            // rb1default
            // 
            this.rb1default.AutoSize = true;
            this.rb1default.Checked = true;
            this.rb1default.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.rb1default.Location = new System.Drawing.Point(4, 16);
            this.rb1default.Margin = new System.Windows.Forms.Padding(0);
            this.rb1default.Name = "rb1default";
            this.rb1default.Size = new System.Drawing.Size(142, 17);
            this.rb1default.TabIndex = 1;
            this.rb1default.TabStop = true;
            this.rb1default.Text = "entire PJSE file table";
            // 
            // rb1CPOnly
            // 
            this.rb1CPOnly.AutoSize = true;
            this.rb1CPOnly.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.rb1CPOnly.Location = new System.Drawing.Point(162, 16);
            this.rb1CPOnly.Margin = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.rb1CPOnly.Name = "rb1CPOnly";
            this.rb1CPOnly.Size = new System.Drawing.Size(146, 17);
            this.rb1CPOnly.TabIndex = 2;
            this.rb1CPOnly.Text = "current package only";
            // 
            // btnHelp
            // 
            this.btnHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHelp.BackColor = System.Drawing.Color.Transparent;
            this.btnHelp.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnHelp.Location = new System.Drawing.Point(666, 9);
            this.btnHelp.Margin = new System.Windows.Forms.Padding(2, 10, 9, 2);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(20, 25);
            this.btnHelp.TabIndex = 6;
            this.btnHelp.Text = "&?";
            this.btnHelp.UseVisualStyleBackColor = false;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // panel1
            // 
            this.panel1.BackgroundImageLocation = new System.Drawing.Point(500, 0);
            this.panel1.BackgroundImageScale = 0.8F;
            this.panel1.Controls.Add(this.btclipb);
            this.panel1.Controls.Add(this.pnFixer);
            this.panel1.Controls.Add(this.tbName);
            this.panel1.Controls.Add(this.lbNumber);
            this.panel1.Controls.Add(this.lbName);
            this.panel1.Controls.Add(this.btnClearFilter);
            this.panel1.Controls.Add(this.ckbSGSearch);
            this.panel1.Controls.Add(this.tbNumber);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.btnHelp);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Controls.Add(this.lbStatus);
            this.panel1.Controls.Add(this.progressBar1);
            this.panel1.Controls.Add(this.rtbReport);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(784, 536);
            this.panel1.TabIndex = 9;
            // 
            // btclipb
            // 
            this.btclipb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btclipb.AutoSize = true;
            this.btclipb.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btclipb.Location = new System.Drawing.Point(687, 220);
            this.btclipb.Margin = new System.Windows.Forms.Padding(0);
            this.btclipb.Name = "btclipb";
            this.btclipb.Size = new System.Drawing.Size(89, 23);
            this.btclipb.TabIndex = 10;
            this.btclipb.Text = "To Clipboard";
            this.btclipb.UseVisualStyleBackColor = true;
            this.btclipb.Visible = false;
            this.btclipb.Click += new System.EventHandler(this.btclipb_Click);
            // 
            // pnFixer
            // 
            this.pnFixer.Controls.Add(this.gcGroup);
            this.pnFixer.Location = new System.Drawing.Point(0, 259);
            this.pnFixer.Name = "pnFixer";
            this.pnFixer.Size = new System.Drawing.Size(590, 25);
            this.pnFixer.TabIndex = 9;
            // 
            // GUIDForm
            // 
            this.AcceptButton = this.btnSearch;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(784, 536);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximumSize = new System.Drawing.Size(800, 575);
            this.MinimumSize = new System.Drawing.Size(800, 575);
            this.Name = "GUIDForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Simantics Resource Finder";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnFixer.ResumeLayout(false);
            this.pnFixer.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

        protected override void OnClosing(CancelEventArgs e)
        {
            // PersistantHeight = this.Height;
            // PersistantWidth = this.Width;
            searching = false;
            if (searchThread != null && searchThread.IsAlive)
            {
                searchThread.Interrupt();
                searchThread.Join();
                searchThread = null;
            }
            e.Cancel = true;
            Hide();
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
            string protocol = "file://";
            string relativePathToHelp = "pjse.coder.plugin/PJSE_Help";

            SimPe.RemoteControl.ShowHelp(protocol + SimPe.Helper.SimPePluginPath + "/" + relativePathToHelp + "/Finder.htm");
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
            //Hide();
        }

        private bool isCkbObjdGUIDEnabled { get { return !ckbCallsToBHAV.Checked && !ckbGLOB.Checked; } }
        private bool isCkbCallsToBHAVEnabled { get { return !ckbObjdGUID.Checked && !ckbGLOB.Checked && !isCkbSomeTextChecked; } }
        private bool isCkbGLOBEnabled { get { return !ckbObjdGUID.Checked && !ckbCallsToBHAV.Checked && !isCkbSomeTextChecked; } }
        private bool isFlpNamesEnabled { get { return !ckbCallsToBHAV.Checked && !ckbGLOB.Checked; } }
        private static bool isChecked(CheckBox cb) { return cb.Checked; }
        private bool isCkbSomeTextChecked { get { return isCkbSomeNameChecked || isCkbSomeStringChecked; } }
        private bool isCkbSomeNameChecked
        {
            get
            {
                List<CheckBox> lcb = new List<CheckBox>(new CheckBox[] { ckbObjdName, ckbNrefName, ckbBhavName, ckbBconName, });
                return (lcb.Find(isChecked) != null);
            }
        }
        private bool isCkbSomeStringChecked
        {
            get
            {
                List<CheckBox> lcb = new List<CheckBox>(new CheckBox[] { ckbSTR, ckbCTSS, ckbTTAs, });
                return (lcb.Find(isChecked) != null);
            }
        }

        private void ckbObjdGUID_CheckedChanged(object sender, EventArgs e)
        {
            ckbCallsToBHAV.Enabled = isCkbCallsToBHAVEnabled;
            ckbGLOB.Enabled = isCkbGLOBEnabled;
            ckbSTR.Enabled = ckbCTSS.Enabled = ckbTTAs.Enabled = ckbDefLang.Enabled = ckbObjdName.Enabled = ckbNrefName.Enabled = ckbBhavName.Enabled = ckbBconName.Enabled = isFlpNamesEnabled;

            if (ckbObjdGUID.Checked) ckbCallsToBHAV.Checked = ckbGLOB.Checked = false;

            tbNumber.Enabled = ckbObjdGUID.Checked;
            lbNumber.Text = ckbObjdGUID.Checked ? pjse.Localization.GetString("GUID") : "";
        }

        private void ckbCallsToBHAV_CheckedChanged(object sender, EventArgs e)
        {
            ckbGLOB.Enabled = isCkbGLOBEnabled;
            ckbObjdGUID.Enabled = isCkbObjdGUIDEnabled;
            ckbSTR.Enabled = ckbCTSS.Enabled = ckbTTAs.Enabled = ckbDefLang.Enabled = ckbObjdName.Enabled = ckbNrefName.Enabled = ckbBhavName.Enabled = ckbBconName.Enabled = isFlpNamesEnabled;

            if (ckbCallsToBHAV.Checked) ckbObjdGUID.Checked = ckbGLOB.Checked = false;

            tbNumber.Enabled = ckbSGSearch.Enabled = ckbFromBHAV.Enabled = ckbFromObjf.Enabled = ckbFromTtab.Enabled = ckbCallsToBHAV.Checked;
            lbNumber.Text = ckbCallsToBHAV.Checked ? pjse.Localization.GetString("OpCode") : "";
        }

        private void ckbGLOB_CheckedChanged(object sender, EventArgs e)
        {
            ckbCallsToBHAV.Enabled = isCkbObjdGUIDEnabled;
            ckbObjdGUID.Enabled = isCkbObjdGUIDEnabled;
            ckbSTR.Enabled = ckbCTSS.Enabled = ckbTTAs.Enabled = ckbDefLang.Enabled = ckbObjdName.Enabled = ckbNrefName.Enabled = ckbBhavName.Enabled = ckbBconName.Enabled = isFlpNamesEnabled;

            if (ckbGLOB.Checked) ckbObjdGUID.Checked = ckbCallsToBHAV.Checked = false;
        }

        private void ckbSomeName_CheckedChanged(object sender, EventArgs e)
        {
            ckbCallsToBHAV.Enabled = isCkbCallsToBHAVEnabled;
            ckbGLOB.Enabled = isCkbGLOBEnabled;
            ckbObjdGUID.Enabled = isCkbObjdGUIDEnabled;

            lbName.Enabled = tbName.Enabled = isCkbSomeTextChecked;
            ckbDefLang.Enabled = isCkbSomeStringChecked;
        }

        private void btnClearFilter_Click(object sender, EventArgs e)
        {
            gcGroup.Value = 0;
        }

        private void btclipb_Click(object sender, EventArgs e)
        {
            string text = "";
            foreach (string clit in this.rtbReport.Lines) text += clit + "\r\n";
            Clipboard.SetDataObject(text, true);
        }
	}
}