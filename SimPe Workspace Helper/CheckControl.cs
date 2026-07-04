using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Media;

namespace SimPe
{
	public enum CheckItemState
	{
		Unknown, Ok, Fail, Warning
	}
	/// <summary>
	/// Summary description for CheckControl.
	/// </summary>
	public class CheckControl : System.Windows.Forms.UserControl
	{
		
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		
		public CheckControl()
		{
			SetStyle(
				ControlStyles.SupportsTransparentBackColor |
				ControlStyles.AllPaintingInWmPaint |
				ControlStyles.UserPaint |
				ControlStyles.ResizeRedraw 
				| ControlStyles.DoubleBuffer
				,true);
			// Required designer variable.
			InitializeComponent();
            if (booby.ThemeManager.ThemedForms && Helper.StartedGui != Executable.WizardsOfSimpe) booby.ThemeManager.Global.AddControl(this.button1);
            /*
			try 
			{
				
			} 
			catch {}*/
		}

		static Image LoadFromResource(string name)
		{
            if (name == "fail")
                return GetIcon.Fail;
            else if (name == "ok")
                return GetIcon.OK;
            else if (name == "warn")
                return GetIcon.Warn;
            else return GetIcon.Unk;
		}

		static Image iok, ifail, iunk, iwarn;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button button1;
		private SimPe.CheckItem chkCache;
        private SimPe.CheckItem chkFileTable;
        private SimPe.CheckItem chkSimFolder;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Panel panel4;
        static bool cacleared = false;
	
		public static Image OKImage
		{
			get 
			{
				if (iok==null) iok = LoadFromResource("ok");
				return iok;
			}
		}
		public static Image FailImage
		{
			get 
			{
				if (ifail==null) ifail = LoadFromResource("fail");
				return ifail;
			}
		}
		public static Image UnknownImage
		{
			get 
			{
				if (iunk==null) iunk = LoadFromResource("unk");
				return iunk;
			}
		}
		public static Image WarnImage
		{
			get 
			{
				if (iwarn==null) iwarn = LoadFromResource("warn");
				return iwarn;
			}
        }
        public static bool CaCleared
        {
            get
            {
                return cacleared;
            }
            set
            {
                cacleared = value;
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

		#region Windows Form Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CheckControl));
            this.chkSimFolder = new SimPe.CheckItem();
            this.chkCache = new SimPe.CheckItem();
            this.chkFileTable = new SimPe.CheckItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkSimFolder
            // 
            this.chkSimFolder.CanFix = true;
            resources.ApplyResources(this.chkSimFolder, "chkSimFolder");
            this.chkSimFolder.CheckState = SimPe.CheckItemState.Unknown;
            this.chkSimFolder.Details = "";
            this.chkSimFolder.Name = "chkSimFolder";
            this.chkSimFolder.ClickedFix += new SimPe.CheckItem.FixEventHandler(this.chkSimFolder_ClickedFix);
            this.chkSimFolder.CalledCheck += new SimPe.CheckItem.FixEventHandler(this.chkSimFolder_CalledCheck);
            // 
            // chkCache
            // 
            this.chkCache.CanFix = true;
            resources.ApplyResources(this.chkCache, "chkCache");
            this.chkCache.CheckState = SimPe.CheckItemState.Unknown;
            this.chkCache.Details = "";
            this.chkCache.Name = "chkCache";
            this.chkCache.ClickedFix += new SimPe.CheckItem.FixEventHandler(this.chkCache_ClickedFix);
            this.chkCache.CalledCheck += new SimPe.CheckItem.FixEventHandler(this.chkCache_CalledCheck);
            // 
            // chkFileTable
            // 
            this.chkFileTable.CanFix = true;
            resources.ApplyResources(this.chkFileTable, "chkFileTable");
            this.chkFileTable.CheckState = SimPe.CheckItemState.Unknown;
            this.chkFileTable.Details = "";
            this.chkFileTable.Name = "chkFileTable";
            this.chkFileTable.ClickedFix += new SimPe.CheckItem.FixEventHandler(this.chkFileTable_ClickedFix);
            this.chkFileTable.CalledCheck += new SimPe.CheckItem.FixEventHandler(this.chkFileTable_CalledCheck);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button1);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.AppWorkspace;
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.AppWorkspace;
            resources.ApplyResources(this.panel3, "panel3");
            this.panel3.Name = "panel3";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.AppWorkspace;
            resources.ApplyResources(this.panel4, "panel4");
            this.panel4.Name = "panel4";
            // 
            // CheckControl
            // 
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.chkFileTable);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.chkCache);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.chkSimFolder);
            resources.ApplyResources(this, "$this");
            this.Name = "CheckControl";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		public void Reset()
		{
			foreach (Control c in this.Controls)		
				if (c is CheckItem) 				
					((CheckItem)c).Reset();							
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
            this.button1.Visible = false;
			this.Cursor = Cursors.WaitCursor;
			Reset();
			foreach (Control c in this.Controls)
				if (c is CheckItem) 
					((CheckItem)c).Check();
			this.Cursor = Cursors.Default;

			foreach (Control d in this.Controls)
				if (d is CheckItem)
                    if (((CheckItem)d).CheckState != CheckItemState.Ok) this.button1.Visible = true;

            if (booby.PrettyGirls.PervyMode)
            {
                if (this.button1.Visible)
                {
                    SoundPlayer ahhooh = new SoundPlayer(booby.NoisyGirls.Aah);
                    ahhooh.Play();
                }
                else
                {
                    SoundPlayer yeehh = new SoundPlayer(booby.NoisyGirls.ooGood);
                    yeehh.Play();
                }
            }
		}

		public static void ClearCache()
		{
			string[] files = System.IO.Directory.GetFiles(Helper.SimPeDataPath, "*.simpepkg");
			foreach (string file in files) 
			{
				try 
				{
					System.IO.File.Delete(file);
                    cacleared = true;
				} 
				catch (Exception ex) 
				{
					Helper.ExceptionMessage("", ex);
				}
			}
            if (Helper.StartedGui == Executable.Other) Message.Show("The caches are cleared.", "Information", MessageBoxButtons.OK);
			else Message.Show(SimPe.Localization.GetString("cache_cleared"), "Information", MessageBoxButtons.OK);
		}

		public event System.EventHandler FixedFileTable;

		#region Sims Path Test
		private SimPe.CheckItemState chkSimFolder_CalledCheck(object sender, SimPe.CheckItemState isok)
		{
			isok = CheckItemState.Ok;
			CheckItem ci = sender as CheckItem;
            try
            {
                string test, path;
                foreach (ExpansionItem ei in PathProvider.Global.Expansions)
                {
                    if (!ei.Exists) continue;
                    path = ei.InstallFolder;//Helper.WindowsRegistry.GetExecutableFolder(ep);
                    string name = ei.ExeName;

                    if (ei == PathProvider.Global.Latest || ei.Flag.SimStory)
                    {
                        test = System.IO.Path.Combine(path, "TSBin" + Helper.PATH_SEP + name);
                        if (!System.IO.File.Exists(test))
                        {
                            isok = CheckItemState.Fail;
                            ci.Details += SimPe.Localization.GetString("Check: Folder not found").Replace("{name}", ei.Name) + Helper.lbr;
                            ci.Details += "    " + SimPe.Localization.GetString("Check: Unable to locate").Replace("{name}", test) + Helper.lbr + Helper.lbr;
                            continue;
                        }

                        test = System.IO.Path.Combine(path, "TSData" + Helper.PATH_SEP + "Res" + Helper.PATH_SEP + "Objects" + Helper.PATH_SEP + "objects.package");
                        if (!System.IO.File.Exists(test))
                        {
                            isok = CheckItemState.Fail;
                            ci.Details += SimPe.Localization.GetString("Check: Folder not found").Replace("{name}", ei.Name) + Helper.lbr;
                            ci.Details += "    " + SimPe.Localization.GetString("Check: Unable to locate").Replace("{name}", test) + Helper.lbr + Helper.lbr;
                            continue;
                        } // objects.package is Only used in the Highest EP
                    }
                    if (!ei.Flag.FullObjectsPackage)
                    {
                        test = System.IO.Path.Combine(path, ei.ObjectsSubFolder + Helper.PATH_SEP + "SPObjects.package");
                        if (!System.IO.File.Exists(test))
                        {
                            isok = CheckItemState.Fail;
                            ci.Details += SimPe.Localization.GetString("Check: Folder not found").Replace("{name}", ei.Name) + Helper.lbr;
                            ci.Details += "    " + SimPe.Localization.GetString("Check: Unable to locate").Replace("{name}", test) + Helper.lbr + Helper.lbr;
                            continue;
                        }
                    }
                    if (ei.Flag.Class == ExpansionItem.Classes.BaseGame)
                        test = System.IO.Path.Combine(path, "TSData" + Helper.PATH_SEP + "Res" + Helper.PATH_SEP + "Sims3D");
                    else
                        test = System.IO.Path.Combine(path, "TSData" + Helper.PATH_SEP + "Res" + Helper.PATH_SEP + "3D");
                    if (!System.IO.Directory.Exists(test))
                    {
                        isok = CheckItemState.Fail;
                        ci.Details += SimPe.Localization.GetString("Check: Folder not found").Replace("{name}", ei.Name) + Helper.lbr;
                        ci.Details += "    " + SimPe.Localization.GetString("Check: Unable to locate").Replace("{name}", test) + Helper.lbr + Helper.lbr;
                        continue;
                    }
                }

                if (SimPe.PathProvider.Global.GetSaveGamePathForGroup(SimPe.PathProvider.Global.CurrentGroup).Count > 0)
                    path = PathProvider.Global.GetSaveGamePathForGroup(SimPe.PathProvider.Global.CurrentGroup)[0];
                else
                    path = PathProvider.SimSavegameFolder;
                test = System.IO.Path.Combine(path, "Neighborhoods");
                if (!System.IO.Directory.Exists(test))
                {
                    isok = CheckItemState.Fail;
                    ci.Details += SimPe.Localization.GetString("Check: Folder not found").Replace("{name}", SimPe.Localization.GetString("Savegames")) + Helper.lbr;
                    ci.Details += "    " + SimPe.Localization.GetString("Check: Unable to locate").Replace("{name}", test) + Helper.lbr + Helper.lbr;
                }

                if (isok == CheckItemState.Ok)
                {
                    if (SimPe.PathProvider.Global.GameVersion < 16)
                    {
                        test = Data.MetaData.GMND_PACKAGE;
                        if (!System.IO.File.Exists(test))
                        {
                            isok = CheckItemState.Warning;
                            ci.Details += SimPe.Localization.GetString("Check: CEP not found") + Helper.lbr;
                            ci.Details += "    " + SimPe.Localization.GetString("Check: Unable to locate").Replace("{name}", test) + Helper.lbr + Helper.lbr;
                        }
                        test = Data.MetaData.MMAT_PACKAGE;
                        if (!System.IO.File.Exists(test))
                        {
                            isok = CheckItemState.Warning;
                            ci.Details += SimPe.Localization.GetString("Check: CEP not found") + Helper.lbr;
                            ci.Details += "    " + SimPe.Localization.GetString("Check: Unable to locate").Replace("{name}", test) + Helper.lbr + Helper.lbr;
                        }
                    }
                    else if (SimPe.PathProvider.Global.GameVersion > 17)
                    {
                        if (System.IO.File.Exists(Data.MetaData.GMND_PACKAGE) || System.IO.File.Exists(Data.MetaData.MMAT_PACKAGE) || System.IO.File.Exists(Data.MetaData.CTLG_FOLDER) || System.IO.File.Exists(Data.MetaData.ZCEP_FOLDER))
                        {
                            isok = CheckItemState.Warning;
                            ci.Details += SimPe.Localization.GetString("Check: CEP is installed") + Helper.lbr;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                isok = CheckItemState.Fail;
                ci.Details = ex.Message;
            }

			return isok;
		}

		private SimPe.CheckItemState chkSimFolder_ClickedFix(object sender, SimPe.CheckItemState isok)
		{
			isok = CheckItemState.Unknown;
            PathProvider.Global.SetDefaultPaths();
            if (Helper.Profile.Length > 0)
                System.Windows.Forms.MessageBox.Show("You will need to re-save profile " + Helper.Profile, "Fix",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
            return isok;
		}
		#endregion

		#region Cache Test
		private SimPe.CheckItemState chkCache_CalledCheck(object sender, SimPe.CheckItemState isok)
		{
			isok = CheckItemState.Ok;
			CheckItem ci = sender as CheckItem;
			try 
			{				
				SimPe.Cache.CacheFile cf = new SimPe.Cache.CacheFile();		
				string path = System.IO.Path.Combine(Helper.SimPeDataPath, "objcache.simpepkg");
				try
                {
                    if (System.IO.File.Exists(path)) System.IO.File.Delete(System.IO.Path.Combine(Helper.SimPeDataPath, "objcache.simpepkg"));
				} 
				catch
				{ }
				
				path = Helper.SimPeLanguageCache;
				try 
				{
					cf.Load(path);
				} 
				catch (Exception ex)
				{
					ci.Details += SimPe.Localization.GetString("Check: Unable to load cache")+Helper.lbr; 
					ci.Details += "    "+SimPe.Localization.GetString("Check: Error while load").Replace("{name}", path)+Helper.lbr;
                    ci.Details += "    " + ex.Message + Helper.lbr + Helper.lbr;
                    isok = CheckItemState.Fail;
				}
			} 
			catch (Exception ex)
			{
				isok = CheckItemState.Fail;
				ci.Details = ex.Message;
			}

			return isok;
		}

		private SimPe.CheckItemState chkCache_ClickedFix(object sender, SimPe.CheckItemState isok)
		{
			isok = CheckItemState.Unknown;
			ClearCache();
			return isok;
		}
		#endregion

		#region Filetable Test
		private SimPe.CheckItemState chkFileTable_CalledCheck(object sender, SimPe.CheckItemState isok)
		{
			isok = CheckItemState.Ok;
			FileTable.FileIndex.Load();
			CheckItem ci = sender as CheckItem;
			try 
			{				
				SimPe.Interfaces.Scenegraph.IScenegraphFileIndexItem[] items = FileTable.FileIndex.FindFile(Data.MetaData.OBJD_FILE, true);
				if (items.Length<3000) 
				{
					ci.Details += SimPe.Localization.GetString("Check: No Objects")+Helper.lbr;
					isok = CheckItemState.Fail;
				} 
				else 
				{
                    items = FileTable.FileIndex.FindFile(Data.MetaData.OBJD_FILE, 0x7F94AFE8, 0x000041AB, null);//Bed - Double - Loft - D
					if (items.Length==0) 
					{
						ci.Details += SimPe.Localization.GetString("Check: No Objects")+Helper.lbr;
						isok = CheckItemState.Fail;
					}
				}
                /*
                 * This is stoopid - the objects.package contains more than 100 TXMTs, if it is found then this error never occurs.
                 * If it is not found then that is problem enough. If we are working with op-codes or neighbourhoods then we don't
                 * even want TXMTs anyway.
				items = FileTable.FileIndex.FindFile(Data.MetaData.TXMT, true);
				if (items.Length<100) 
				{
					ci.Details += SimPe.Localization.GetString("Check: No Textures")+Helper.lbr;
					isok = CheckItemState.Fail;
				}
                */
			}
			catch (Exception ex)
			{
				isok = CheckItemState.Fail;
				ci.Details = ex.Message;
			}

			return isok;
		}

		private SimPe.CheckItemState chkFileTable_ClickedFix(object sender, SimPe.CheckItemState isok)
		{
			isok = CheckItemState.Unknown;
			try 
			{
                string msg = "your file table folder settings will be reset";
                if (Helper.Profile.Length > 0) msg += " and you will need to re-save profile " + Helper.Profile;
                if (System.Windows.Forms.MessageBox.Show("The File table settings file was not correct and you have asked to fix it.\n" +
                    Helper.DataFolder.FoldersXREG + "\n" +
                    "SimPe can generate a new one (" + msg + ").\n\n" +
                    "Should SimPe delete the File table settings File?"
                    , "Fix",
                    System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    System.IO.File.Delete(Helper.DataFolder.FoldersXREG);
                    FileTable.Reload();
                    if (FixedFileTable != null) FixedFileTable(this, new EventArgs());
                }
            } 
			catch 
			{
				isok = CheckItemState.Fail;
			}
			return isok;
		}
		#endregion
	}
}
