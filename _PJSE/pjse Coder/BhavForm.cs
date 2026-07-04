/***************************************************************************
 *   Copyright (C) 2008 by Peter L Jones                                   *
 *   pljones@users.sf.net                                                  *
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
using SimPe.Interfaces;
using SimPe.Interfaces.Files;
using SimPe.Interfaces.Plugin;
using SimPe.Interfaces.Scenegraph;
using SimPe.PackedFiles.Wrapper;
using pjse;

namespace SimPe.PackedFiles.UserInterface
{
	/// <summary>
	/// Summary description for BhavForm.
	/// </summary>
	public class BhavForm : System.Windows.Forms.Form, IPackedFileUI
	{
		#region Form variables

        private System.Windows.Forms.Label lbFilename;
		private System.Windows.Forms.Label lbFormat;
		private System.Windows.Forms.Label lbType;
		private System.Windows.Forms.Label lbLocalC;
		private System.Windows.Forms.TextBox tbFilename;
		private System.Windows.Forms.TextBox tbType;
		private System.Windows.Forms.TextBox tbArgC;
		private System.Windows.Forms.TextBox tbLocalC;
		private System.Windows.Forms.ComboBox tba1;
		private System.Windows.Forms.ComboBox tba2;
		private System.Windows.Forms.LinkLabel llopenbhav;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.TextBox tbInst_OpCode;
		private System.Windows.Forms.TextBox tbInst_Op7;
		private System.Windows.Forms.TextBox tbInst_Op6;
		private System.Windows.Forms.TextBox tbInst_Op5;
		private System.Windows.Forms.TextBox tbInst_Op4;
		private System.Windows.Forms.TextBox tbInst_Op3;
		private System.Windows.Forms.TextBox tbInst_Op2;
		private System.Windows.Forms.TextBox tbInst_Op1;
		private System.Windows.Forms.TextBox tbInst_Op0;
		private System.Windows.Forms.TextBox tbInst_Unk7;
		private System.Windows.Forms.TextBox tbInst_Unk6;
		private System.Windows.Forms.TextBox tbInst_Unk5;
		private System.Windows.Forms.TextBox tbInst_Unk4;
		private System.Windows.Forms.TextBox tbInst_Unk3;
		private System.Windows.Forms.TextBox tbInst_Unk2;
		private System.Windows.Forms.TextBox tbInst_Unk1;
		private System.Windows.Forms.TextBox tbInst_Unk0;
		private System.Windows.Forms.GroupBox gbInstruction;
		private System.Windows.Forms.Panel bhavPanel;
		private System.Windows.Forms.Button btnCommit;
		private System.Windows.Forms.Button btnOpCode;
		private System.Windows.Forms.Button btnOperandWiz;
		private System.Windows.Forms.Button btnSort;
		private System.Windows.Forms.Label lbUpDown;
		private System.Windows.Forms.TextBox tbLines;
		private System.Windows.Forms.Button btnUp;
		private System.Windows.Forms.Button btnDown;
		private System.Windows.Forms.Button btnDel;
		private System.Windows.Forms.Button btnAdd;
		private System.Windows.Forms.Button btnCancel;
        private SimPe.PackedFiles.UserInterface.BhavInstListControl pnflowcontainer;
		private System.Windows.Forms.GroupBox gbMove;
		private System.Windows.Forms.Label lbArgC;
		private System.Windows.Forms.GroupBox gbSpecial;
		private System.Windows.Forms.Button btnInsTrue;
		private System.Windows.Forms.Button btnInsFalse;
		private System.Windows.Forms.Button btnLinkInge;
		private System.Windows.Forms.Button btnDelPescado;
		private System.Windows.Forms.Button btnAppend;
		private System.Windows.Forms.ComboBox cbFormat;
		private System.Windows.Forms.Button btnDelMerola;
		private System.Windows.Forms.Label lbCacheFlags;
		private System.Windows.Forms.TextBox tbCacheFlags;
		private System.Windows.Forms.Label lbTreeVersion;
		private System.Windows.Forms.TextBox tbTreeVersion;
		private System.Windows.Forms.TextBox tbHeaderFlag;
		private System.Windows.Forms.Label lbHeaderFlag;
		private System.Windows.Forms.Button btnOperandRaw;
		private System.Windows.Forms.TextBox tbInst_NodeVersion;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.CheckBox cbSpecial;
		private System.Windows.Forms.TextBox tbInst_Longname;
        private System.Windows.Forms.Button btnCopyListing;
        private System.Windows.Forms.Button btnTPRPMaker;
        private Button btnGUIDIndex;
        private ContextMenuStrip cmenuGUIDIndex;
        private ToolStripMenuItem createAllPackagesToolStripMenuItem;
        private ToolStripMenuItem createCurrentPackageToolStripMenuItem;
        private ToolStripMenuItem loadIndexToolStripMenuItem;
        private ToolStripMenuItem defaultFileToolStripMenuItem;
        private ToolStripMenuItem fromFileToolStripMenuItem;
        private ToolStripMenuItem saveIndexToolStripMenuItem;
        private ToolStripMenuItem defaultFileToolStripMenuItem1;
        private ToolStripMenuItem toFileToolStripMenuItem;
        private Button btnCopyBHAV;
        private TextBox tbHidesOP;
        private LinkLabel llHidesOP;
        private Label lbHidesOP;
        private Button btnPasteListing;
        private Button btnZero;
        private ToolTip ttBhavForm;
        private pjse_banner pjse_banner1;
        private CompareButton cmpBHAV;
        private Button btnInsUnlinked;
        private Button btnImportBHAV;
        private Button button1;
        private IContainer components;
        #endregion
       
		public BhavForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
            hidesFmt = llHidesOP.Text;
			this.Tag = "Normal"; // Used by SetReadOnly
            if (booby.ThemeManager.ThemedForms)
            {
                this.bhavPanel.BackColor = booby.ThemeManager.Global.ThemeColorLight;
                this.tbHidesOP.BackColor = booby.ThemeManager.Global.ThemeColorLight;
                this.tbInst_Longname.BackColor = booby.ThemeManager.Global.ThemeColorLight;
                booby.ThemeManager.Global.AddControl(this.btnCommit);
            }
            else
                this.tbHidesOP.BackColor = System.Drawing.SystemColors.Control;

            TextBox[] iob = {
								 tbInst_Op0  ,tbInst_Op1  ,tbInst_Op2  ,tbInst_Op3
								,tbInst_Op4  ,tbInst_Op5  ,tbInst_Op6  ,tbInst_Op7
								,tbInst_Unk0 ,tbInst_Unk1 ,tbInst_Unk2 ,tbInst_Unk3
								,tbInst_Unk4 ,tbInst_Unk5 ,tbInst_Unk6 ,tbInst_Unk7
								,tbInst_NodeVersion
								,tbHeaderFlag
								,tbType
								,tbCacheFlags
								,tbArgC
								,tbLocalC
							};
			alHex8 = new ArrayList(iob);

            TextBox[] w = { tbInst_OpCode ,tbLines ,};
			alHex16 = new ArrayList(w);

			TextBox[] dw = { tbTreeVersion ,};
			alHex32 = new ArrayList(dw);

			ComboBox[] cb = { tba1 ,tba2 ,cbFormat ,};
			alHex16cb = new ArrayList(cb);

            this.button1.Visible = Helper.WindowsRegistry.CreatorMode;

			this.gbSpecial.Visible =
				this.cbSpecial.Checked = pjse.Settings.PJSE.ShowSpecialButtons;

			pjse.FileTable.GFT.FiletableRefresh += new System.EventHandler(this.FiletableRefresh);
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
			if (setHandler && wrapper != null)
			{
                wrapper.FileDescriptor.DescriptionChanged -= new EventHandler(FileDescriptor_DescriptionChanged);
                wrapper.WrapperChanged -= new System.EventHandler(this.WrapperChanged);
				setHandler = false;
			}
			wrapper = null;
			currentInst = null;
			origInst = null;
            alHex8 = alHex16 = alHex32 = alDec8 = alHex16cb = null;
		}

		
		#region BhavForm
		private Bhav wrapper;
		private bool setHandler = false;
		private BhavWiz currentInst;
		private Instruction origInst;
		private bool internalchg;
        private ArrayList alHex8;
		private ArrayList alHex16;
		private ArrayList alHex32;
		private ArrayList alDec8;
		private ArrayList alHex16cb;
        private String hidesFmt = "{0}";

        // These should be on the ExtendedWrapper class or BhavWiz or, indeed, PackedFileDescriptor
        private static IPackedFileDescriptor newPFD(IPackedFileDescriptor oldPFD) { return newPFD(oldPFD.Type, oldPFD.Group, oldPFD.SubType, oldPFD.Instance); }
        private static IPackedFileDescriptor newPFD(uint type, uint group, uint instance) { return newPFD(type, group, 0x00000000, instance); }
        private static IPackedFileDescriptor newPFD(uint type, uint group, uint subtype, uint instance)
        {
            IPackedFileDescriptor npfd = new SimPe.Packages.PackedFileDescriptor();
            npfd.Type = type;
            npfd.Group = group;
            npfd.SubType = subtype;
            npfd.Instance = instance;
            return npfd;
        }

        private IPackageFile currentPackage = null;
        private void TakeACopy()
        {
            IPackedFileDescriptor npfd = newPFD(wrapper.FileDescriptor);
            npfd.UserData = wrapper.Package.Read(wrapper.FileDescriptor).UncompressedData;
            currentPackage.Add(npfd, true);
        }

        private delegate bool ignoreEntry(pjse.FileTable.Entry i, IPackedFileDescriptor npfd);
        private delegate bool matchItem(object o, uint inst);
        private delegate void setter(object o, ushort inst);

        private void doUpdate(string typeName
            , uint oldInst
            , IPackedFileDescriptor npfd
            , pjse.FileTable.Entry[] entries
            , ignoreEntry ieDelegate
            , matchItem[] matchDelegates
            , setter[] setDelegates
            )
        {
            if (npfd == null) return;
            if (entries == null || entries.Length == 0) return;
            if (matchDelegates == null || matchDelegates.Length == 0) return;
            if (setDelegates == null || setDelegates.Length != matchDelegates.Length) return;

            WaitingScreen.Message = "Updating current package - " + typeName + "s...";
            foreach (pjse.FileTable.Entry i in entries)
            {
                ResourceLoader.Refresh(i); // make sure it's been saved before we search it
                Application.DoEvents();

                AbstractWrapper wrapper = i.Wrapper;
                if (wrapper as IEnumerable == null) break;

                if (ieDelegate != null && ieDelegate(i, npfd)) continue;

                foreach (object o in (IEnumerable)wrapper)
                {
                    for (int j = 0; j < matchDelegates.Length; j++)
                    {
                        matchItem md = matchDelegates[j];
                        setter sd = setDelegates[j];
                        if (md != null && sd != null && md(o, oldInst))
                        {
                            sd(o, (ushort)npfd.Instance);
                        }
                    }
                }
                if (wrapper.Changed)
                {
                    wrapper.SynchronizeUserData();
                    ResourceLoader.Refresh(i);
                }
            }
        }
        private void ImportBHAV()
        {
            WaitingScreen.Wait();

            #region Finding available BHAV number
            WaitingScreen.Message = "Finding available BHAV number...";
            pjse.FileTable.Entry[] ai = pjse.FileTable.GFT[Bhav.Bhavtype, pjse.FileTable.Source.Local];
            ushort newInst = 0x0fff;
            foreach (pjse.FileTable.Entry i in ai) if (i.Instance >= 0x1000 && i.Instance < 0x2000 && i.Instance > newInst) newInst = (ushort)i.Instance;
            newInst++;
            #endregion

            currentPackage.BeginUpdate();

            #region Cloning BHAV
            WaitingScreen.Message = "Cloning BHAV...";
            IPackedFileDescriptor npfd = newPFD(Bhav.Bhavtype, 0xffffffff, newInst);
            npfd.UserData = wrapper.Package.Read(wrapper.FileDescriptor).UncompressedData;
            currentPackage.Add(npfd, true);
            #endregion

            #region Updating current package - BHAVs
            doUpdate("BHAV"
                , wrapper.FileDescriptor.Instance
                , npfd
                , ai
                , delegate(pjse.FileTable.Entry i, IPackedFileDescriptor pfd) { return (i.Group != pfd.Group || i.Instance < 0x1000 || i.Instance >= 0x2000); }
                , new matchItem[] { delegate(object o, uint value) {
                    return ((Instruction)o).OpCode == value; } }
                , new setter[] { delegate(object o, ushort value) { ((Instruction)o).OpCode = value; } }
                );
            #endregion

            #region Updating current package - OBJFs
            doUpdate("OBJF"
                , wrapper.FileDescriptor.Instance
                , npfd
                , pjse.FileTable.GFT[Objf.Objftype, pjse.FileTable.Source.Local]
                , null
                , new matchItem[] {
                    delegate(object o, uint value) { return ((ObjfItem)o).Action == value; },
                    delegate(object o, uint value) { return ((ObjfItem)o).Guardian == value; },
                }
                , new setter[] {
                    delegate(object o, ushort value) { ((ObjfItem)o).Action = value; },
                    delegate(object o, ushort value) { ((ObjfItem)o).Guardian = value; },
                }
                );
            #endregion

            #region Updating current package - TTABs
            doUpdate("TTAB"
                , wrapper.FileDescriptor.Instance
                , npfd
                , pjse.FileTable.GFT[Ttab.Ttabtype, pjse.FileTable.Source.Local]
                , null
                , new matchItem[] {
                    delegate(object o, uint value) { return ((TtabItem)o).Action == value; },
                    delegate(object o, uint value) { return ((TtabItem)o).Guardian == value; },
                }
                , new setter[] {
                    delegate(object o, ushort value) { ((TtabItem)o).Action = value; },
                    delegate(object o, ushort value) { ((TtabItem)o).Guardian = value; },
                }
                );
            #endregion

            currentPackage.EndUpdate();

            WaitingScreen.Message = "";
            WaitingScreen.Stop();
            MessageBox.Show(
                pjse.Localization.GetString("ml_done")
                , btnImportBHAV.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private void cmpBHAV_CompareWith(object sender, CompareButton.CompareWithEventArgs e) { common_LinkClicked(e.Item, e.ExpansionItem, true); }
        private void common_LinkClicked(pjse.FileTable.Entry item) { common_LinkClicked(item, null, false); }
        private void common_LinkClicked(pjse.FileTable.Entry item, SimPe.ExpansionItem exp, bool noOverride)
        {
            if (item == null) return; // this should never happen
            Bhav bhav = new Bhav();
            bhav.ProcessData(item.PFD, item.Package);

            BhavForm ui = (BhavForm)bhav.UIHandler;
            string tag = "Popup"; // tells the SetReadOnly function it's in a popup - so everything locked down
            if (noOverride) tag += ";noOverride"; // prevents handleOverride displaying anything
            tag += ";callerID=+" + wrapper.FileDescriptor.ExportFileName +"+";
            if (exp != null) tag += ";expName=+" + exp.NameShort + "+";
            ui.Tag = tag;

            bhav.RefreshUI();
            ui.Show();
        }

        private string getValueFromTag(string key)
        {
            string s = this.Tag as string;
            if (s == null) return null;

            key = ";" + key + "=+";
            int i = s.IndexOf(key);
            if (i < 0) return null;

            s = s.Substring(i + key.Length);
            i = s.IndexOf("+");
            return (i >= 0) ? s.Substring(0, i) : null;
        }
        private bool isPopup { get { return (this.Tag == null || this.Tag as string == null) ? false : ((string)(this.Tag)).StartsWith("Popup"); } }
        private bool isNoOverride { get { return (this.Tag == null || this.Tag as string == null) ? false : ((string)(this.Tag)).Contains(";noOverride"); } }
        private string callerID { get { return getValueFromTag("callerID"); } }
        private string expName
        {
            get
            {
                string s = getValueFromTag("expName");
                if (s != null) return s;

                foreach(pjse.FileTable.Entry item in pjse.FileTable.GFT[wrapper.Package, wrapper.FileDescriptor])
                    if (item.PFD == wrapper.FileDescriptor)
                    {
                        if (item.IsMaxis) return pjse.Localization.GetString("expCurrent");
                        else break;
                    }
                return pjse.Localization.GetString("expCustom");
            }
        }

        private String formTitle
        {
            get
            {
                return pjse.Localization.GetString("pjseWindowTitle"
                    , expName // EP Name or Custom
                    , System.IO.Path.GetFileName(wrapper.Package.SaveFileName) // package Filename without path
                    , wrapper.FileDescriptor.TypeName.shortname // Type (short name)
                    , "0x" + SimPe.Helper.HexString(wrapper.FileDescriptor.Group) // Group Number
                    , "0x" + SimPe.Helper.HexString((ushort)wrapper.FileDescriptor.Instance) // Instance Number
                    , wrapper.FileName
                    ,  pjse.Localization.GetString(isPopup ? "pjseWindowTitleView" : "pjseWindowTitleEdit") // View or Edit
                    );
            }
        }

        private void handleOverride()
        {
            lbHidesOP.Visible = tbHidesOP.Visible = llHidesOP.Visible = false;
            llHidesOP.Tag = null;
            if (this.isNoOverride) return;

            pjse.FileTable.Entry[] items = pjse.FileTable.GFT[wrapper.Package, wrapper.FileDescriptor];
            
            if (items.Length > 1) // currentpkg, other, fixed, maxis
            {
                pjse.FileTable.Entry item = items[items.Length - 1];
                if (item.PFD == wrapper.FileDescriptor) return;
                if (!item.IsMaxis && !item.IsFixed) return;

                this.lbHidesOP.Visible = this.tbHidesOP.Visible = this.llHidesOP.Visible = true;
                llHidesOP.Links[0].Start -= llHidesOP.Text.Length;
                llHidesOP.Text = hidesFmt.Replace("{0}", System.IO.Path.GetFileName(item.Package.SaveFileName));
                llHidesOP.Links[0].Start += llHidesOP.Text.Length;
                this.tbHidesOP.Text = wrapper.Package.FileName;
                llHidesOP.Tag = item.IsMaxis ? pjse.FileTable.Source.Maxis : pjse.FileTable.Source.Fixed;
            }
        }

        private void SetReadOnly(bool state) 
		{
            //if (this.isPopup) state = true;

            this.tbInst_OpCode.ReadOnly = state;
			this.btnOpCode.Enabled = !state;
			this.tbInst_NodeVersion.ReadOnly = state || wrapper.Header.Format < 0x8005;
			this.tba1.Enabled = !state;
			this.tba2.Enabled = !state;

			/*this.tbInst_Op01_dec.ReadOnly = state;
			this.tbInst_Op23_dec.ReadOnly = state;*/

			this.tbInst_Op0.ReadOnly = state;
			this.tbInst_Op1.ReadOnly = state;
			this.tbInst_Op2.ReadOnly = state;
			this.tbInst_Op3.ReadOnly = state;
			this.tbInst_Op4.ReadOnly = state;
			this.tbInst_Op5.ReadOnly = state;
			this.tbInst_Op6.ReadOnly = state;
			this.tbInst_Op7.ReadOnly = state;

			this.btnOperandWiz.Enabled = !state;
			/*this.btnOperandRaw.Enabled = !state;*/
            this.btnZero.Enabled = !state;
			
			this.tbInst_Unk0.ReadOnly = state || wrapper.Header.Format < 0x8003;
			this.tbInst_Unk1.ReadOnly = state || wrapper.Header.Format < 0x8003;
			this.tbInst_Unk2.ReadOnly = state || wrapper.Header.Format < 0x8003;
			this.tbInst_Unk3.ReadOnly = state || wrapper.Header.Format < 0x8003;
			this.tbInst_Unk4.ReadOnly = state || wrapper.Header.Format < 0x8003;
			this.tbInst_Unk5.ReadOnly = state || wrapper.Header.Format < 0x8003;
			this.tbInst_Unk6.ReadOnly = state || wrapper.Header.Format < 0x8003;
			this.tbInst_Unk7.ReadOnly = state || wrapper.Header.Format < 0x8003;

			this.btnUp.Enabled = !state;
			this.btnDown.Enabled = !state;
			this.tbLines.ReadOnly = state;
			this.btnDelPescado.Enabled = this.btnDel.Enabled = !state;
			this.btnInsTrue.Enabled = this.btnInsFalse.Enabled = this.btnAdd.Enabled = !state;
		}

        private bool instIsBhav()
        {
            return wrapper.ResourceByInstance(SimPe.Data.MetaData.BHAV_FILE, currentInst.Instruction.OpCode) != null;
        }

        private void OperandWiz(int type)
        {
            internalchg = true;
            bool changed = false;
            Instruction inst = currentInst.Instruction;
            currentInst = null;
            try
            {
                changed = ((new BhavOperandWiz()).Execute(btnCommit.Visible ? inst : inst.Clone(), type) != null);
            }
            finally
            {
                currentInst = inst;
                if (btnCommit.Visible)
                {
                    if (changed) UpdateInstPanel();
                    this.btnCancel.Enabled = true;
                }
                internalchg = false;
            }
        }

        private void UpdateInstPanel()
		{
			internalchg = true;
            Application.UseWaitCursor = true;
			if (currentInst == null || wrapper.IndexOf(currentInst.Instruction) < 0)
			{
				SetReadOnly(true);
				this.llopenbhav.Enabled = false;
				this.btnInsTrue.Enabled = this.btnInsFalse.Enabled = this.btnAdd.Enabled = true;

				this.tbInst_OpCode.Text = "";
				this.tbInst_NodeVersion.Text = "";
				this.tba1.SelectedIndex = 0;
				this.tba2.SelectedIndex = 0;
				this.tbInst_Op0.Text = "";
				this.tbInst_Op1.Text = "";
				this.tbInst_Op2.Text = "";
				this.tbInst_Op3.Text = "";
				this.tbInst_Op4.Text = "";
				this.tbInst_Op5.Text = "";
				this.tbInst_Op6.Text = "";
				this.tbInst_Op7.Text = "";
				this.tbInst_Unk0.Text = "";
				this.tbInst_Unk1.Text = "";
				this.tbInst_Unk2.Text = "";
				this.tbInst_Unk3.Text = "";
				this.tbInst_Unk4.Text = "";
				this.tbInst_Unk5.Text = "";
				this.tbInst_Unk6.Text = "";
				this.tbInst_Unk7.Text = "";
			}
			else
			{
				Instruction inst = currentInst.Instruction; // saves typing

				SetReadOnly(false);

				this.tbInst_OpCode.Text = "0x"+Helper.HexString(inst.OpCode);
				this.tbInst_NodeVersion.Text = "0x"+Helper.HexString(inst.NodeVersion);

				if (inst.Target1 >= 0xFFFC && inst.Target1 < 0xFFFF)
				{
					this.tba1.SelectedIndex = inst.Target1 - 0xFFFC;
				}
				else
				{
					this.tba1.SelectedIndex = -1;
					this.tba1.Text = "0x"+Helper.HexString(inst.Target1);
				}
				if (inst.Target2 >= 0xFFFC && inst.Target2 < 0xFFFF)
				{
					this.tba2.SelectedIndex = inst.Target2 - 0xFFFC;
				}
				else
				{
					this.tba2.SelectedIndex = -1;
					this.tba2.Text = "0x"+Helper.HexString(inst.Target2);
				}

				this.tbInst_Op0.Text = Helper.HexString(inst.Operands[0]);
				this.tbInst_Op1.Text = Helper.HexString(inst.Operands[1]);
				this.tbInst_Op2.Text = Helper.HexString(inst.Operands[2]);
				this.tbInst_Op3.Text = Helper.HexString(inst.Operands[3]);
				this.tbInst_Op4.Text = Helper.HexString(inst.Operands[4]);
				this.tbInst_Op5.Text = Helper.HexString(inst.Operands[5]);
				this.tbInst_Op6.Text = Helper.HexString(inst.Operands[6]);
				this.tbInst_Op7.Text = Helper.HexString(inst.Operands[7]);

				this.tbInst_Unk0.Text = Helper.HexString(inst.Reserved1[0]);
				this.tbInst_Unk1.Text = Helper.HexString(inst.Reserved1[1]);
				this.tbInst_Unk2.Text = Helper.HexString(inst.Reserved1[2]);
				this.tbInst_Unk3.Text = Helper.HexString(inst.Reserved1[3]);
				this.tbInst_Unk4.Text = Helper.HexString(inst.Reserved1[4]);
				this.tbInst_Unk5.Text = Helper.HexString(inst.Reserved1[5]);
				this.tbInst_Unk6.Text = Helper.HexString(inst.Reserved1[6]);
				this.tbInst_Unk7.Text = Helper.HexString(inst.Reserved1[7]);

				this.btnUp.Enabled = pnflowcontainer.SelectedIndex > 0;
				this.btnDown.Enabled = pnflowcontainer.SelectedIndex < wrapper.Count - 1;

				this.btnDelPescado.Enabled = this.btnDel.Enabled = wrapper.Count > 1;

                this.llopenbhav.Enabled = instIsBhav();
				this.btnOperandWiz.Enabled = currentInst.Wizard() != null;
			}
            setLongname();
            Application.UseWaitCursor = false;
            internalchg = false;
		}

        private void OpcodeChanged(ushort value)
        {
            currentInst.Instruction.OpCode = value; 
            this.currentInst = currentInst.Instruction;
            this.llopenbhav.Enabled = instIsBhav();
            this.btnOperandWiz.Enabled = currentInst.Wizard() != null;
            setLongname();
        }

        private void ChangeLongname(byte oldval, byte newval) { if (oldval != newval) setLongname(); }

        private static string onearg = pjse.Localization.GetString("oneArg");
        private static string manyargs = pjse.Localization.GetString("manyArgs");
        private void setLongname()
        {
            if (currentInst == null || wrapper.IndexOf(currentInst.Instruction) < 0)
                this.tbInst_Longname.Text = "";
            else
            {
                bool state = Application.UseWaitCursor;
                Application.UseWaitCursor = true;
                try
                {
                    this.tbInst_Longname.Text = currentInst.LongName.Replace(", ", ",\r\n  ")
                    .Replace(onearg + ": ", onearg + ":\r\n  ")
                    .Replace(manyargs + ": ", manyargs + ":\r\n  ")
                    ;
                }
                finally { Application.UseWaitCursor = state; }
            }
        }


		private void CopyListing()
		{
			string listing = "";

			int lines = wrapper.Count;
			for (short i = 0; i < lines; i++)
			{
				Instruction inst = wrapper[i];
				BhavWiz w = inst;

				string operands = "";
				for(int j = 0; j < 8; j++) operands += SimPe.Helper.HexString(inst.Operands[j]);
				for(int j = 0; j < 8; j++) operands += SimPe.Helper.HexString(inst.Reserved1[j]);

				listing += ("     "
					+ SimPe.Helper.HexString(i)
					+ " : " + SimPe.Helper.HexString(inst.OpCode)
                    + " : " + SimPe.Helper.HexString(inst.NodeVersion)
                    + " : " + SimPe.Helper.HexString(inst.Target1)
                    + " : " + SimPe.Helper.HexString(inst.Target2)
                    + " : " + operands
					+ "\r\n" + w.LongName + "\r\n\r\n");
			}

			Clipboard.SetDataObject(listing, true);
		}

        private void PasteListing()
        {
            int i = 0;
            int origlen = wrapper.Count;

            string listing = Clipboard.GetText(TextDataFormat.Text);
            foreach (string line in listing.Split('\r', '\n'))
            {
                if (line.Length == 0) continue;
                string[] args = line.Split(':');
                if (args.Length != 6) continue;

                try
                {
                    if (Convert.ToUInt32(args[0].Trim(), 16) != i)
                        throw new Exception("Foo");

                    Instruction inst = new Instruction(wrapper);

                    inst.OpCode = Convert.ToUInt16(args[1].Trim(), 16);
                    inst.NodeVersion = Convert.ToByte(args[2].Trim(), 16);
                    inst.Target1 = Convert.ToUInt16(args[3].Trim(), 16);
                    inst.Target2 = Convert.ToUInt16(args[4].Trim(), 16);
                    for (int j = 0; j < 8; j++)
                        inst.Operands[j] = Convert.ToByte(args[5].Trim().Substring(j * 2, 2), 16);
                    for (int j = 0; j < 8; j++)
                        inst.Reserved1[j] = Convert.ToByte(args[5].Trim().Substring(16 + j * 2, 2), 16);

                    if (inst.Target1 < 0xfffc) inst.Target1 = (ushort)(inst.Target1 + origlen);
                    if (inst.Target2 < 0xfffc) inst.Target2 = (ushort)(inst.Target2 + origlen);

                    wrapper.Add(inst);
                }
                finally
                {
                    i++;
                }
            }
        }

        private void TPRPMaker()
        {
            bhavPanel.Cursor = Cursors.WaitCursor;
            Application.UseWaitCursor = true;
            try
            {
                int minArgc = 0;
                int minLocalC = 0;
                TPRP tprp = (TPRP)wrapper.SiblingResource(TPRP.TPRPtype); // find TPRP for this BHAV

                wrapper.Package.BeginUpdate();

                if (tprp != null && tprp.TextOnly)
                {
                    // if it exists but is unreadable, as if user wants to overwrite
                    DialogResult dr = MessageBox.Show(
                        pjse.Localization.GetString("ml_overwriteduff")
                        , btnTPRPMaker.Text
                        , MessageBoxButtons.OKCancel
                        , MessageBoxIcon.Warning);
                    if (dr != DialogResult.OK)
                        return;
                    wrapper.Package.Remove(tprp.FileDescriptor);
                    tprp = null;
                }
                if (tprp != null)
                {
                    // if it exists ask if user wants to preserve content
                    DialogResult dr = MessageBox.Show(
                        pjse.Localization.GetString("ml_keeplabels")
                        , btnTPRPMaker.Text
                        , MessageBoxButtons.YesNoCancel
                        , MessageBoxIcon.Warning);
                    if (dr == DialogResult.Cancel)
                        return;

                    if (!tprp.Package.Equals(wrapper.Package))
                    {
                        // Clone the original into this package
                        if (dr == DialogResult.Yes) Wait.MaxProgress = tprp.Count;
                        SimPe.Interfaces.Files.IPackedFileDescriptor npfd = newPFD(tprp.FileDescriptor);
                        TPRP ntprp = new TPRP();
                        ntprp.FileDescriptor = npfd;
                        wrapper.Package.Add(npfd, true);
                        if (dr == DialogResult.Yes) foreach (TPRPItem item in tprp) { ntprp.Add(item.Clone()); Wait.Progress++; }
                        tprp = ntprp;
                        tprp.SynchronizeUserData();
                        Wait.MaxProgress = 0;
                    }

                    if (dr == DialogResult.Yes)
                    {
                        minArgc = tprp.ParamCount;
                        minLocalC = tprp.LocalCount;
                    }
                    else
                        tprp.Clear();
                }
                else
                {
                    // create a new TPRP file
                    tprp = new TPRP();
                    tprp.FileDescriptor =
                        newPFD(TPRP.TPRPtype, wrapper.FileDescriptor.Group, wrapper.FileDescriptor.SubType, wrapper.FileDescriptor.Instance);
                    wrapper.Package.Add(tprp.FileDescriptor, true);
                    tprp.SynchronizeUserData();
                }

                Wait.MaxProgress = wrapper.Header.ArgumentCount - minArgc + wrapper.Header.LocalVarCount - minLocalC;
                tprp.FileName = wrapper.FileName;

                for (int arg = minArgc; arg < wrapper.Header.ArgumentCount; arg++)
                {
                    tprp.Add(new TPRPParamLabel(tprp));
                    tprp[false, tprp.ParamCount - 1].Label = BhavWiz.dnParam() + " " + arg.ToString();
                    Wait.Progress++;
                }
                for (int local = minLocalC; local < wrapper.Header.LocalVarCount; local++)
                {
                    tprp.Add(new TPRPLocalLabel(tprp));
                    tprp[true, tprp.LocalCount - 1].Label = BhavWiz.dnLocal() + " " + local.ToString();
                    Wait.Progress++;
                }
                tprp.SynchronizeUserData();
                wrapper.Package.EndUpdate();
            }
            finally
            {
                Wait.SubStop();
                bhavPanel.Cursor = Cursors.Default;
                Application.UseWaitCursor = false;
            }
            MessageBox.Show( pjse.Localization.GetString("ml_done"), btnTPRPMaker.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void TPFWMaker() // Fuck
        {
            try
            {
                SimPe.Plugin.TreesPackedFileWrapper tpfw = (SimPe.Plugin.TreesPackedFileWrapper)wrapper.SiblingResource(0x54524545); // find TPFW for this BHAV
                if (tpfw != null) return;
                tpfw = new SimPe.Plugin.TreesPackedFileWrapper();
                tpfw.FileDescriptor = newPFD(0x54524545, wrapper.FileDescriptor.Group, wrapper.FileDescriptor.SubType, wrapper.FileDescriptor.Instance);
                tpfw.Count = 0;
                tpfw.FileNam = wrapper.FileName;
                for (int i = 0; i < wrapper.Count; i++)
                {
                    tpfw.AddBlock();
                }
                wrapper.Package.Add(tpfw.FileDescriptor, true);
                tpfw.SynchronizeUserData();
                pjse_banner1.TreeVisible = true;
                button1.Enabled = false;
                MessageBox.Show(pjse.Localization.GetString("ml_done"), "comments", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch { }
        }

        private void SetComments()
        {
            if (!Helper.WindowsRegistry.CreatorMode) return;
            SimPe.Plugin.TreesPackedFileWrapper tpfw = (SimPe.Plugin.TreesPackedFileWrapper)wrapper.SiblingResource(0x54524545);
            if (tpfw == null) return;
            int indx = 0;
            BhavInstListItemUI cc;
            foreach (Control LI in this.pnflowcontainer.Controls)
            {
                if (LI.GetType() == typeof(BhavInstListItemUI))
                {
                    cc = LI as BhavInstListItemUI;
                    cc.SetComment(tpfw.ReadComment(indx));
                    indx++;
                }
            }
        }


		private short OpsToShort(byte lo, byte hi)
		{
			ushort uval = (ushort)(lo + (hi << 8));
			if (uval > 32767) return (short)(uval - 65536);
			else return (short)uval;
		}

		private byte[] ShortToOps(short val)
		{
			byte[] ops = new byte[2];
			ushort uval;
			if (val < 0)
				uval = (ushort)(65536 + val);
			else
				uval = (ushort)val;
			ops[0] = (byte)(uval & 0xFF);
			ops[1] = (byte)((uval >> 8) & 0xFF);
			return ops;
		}

		private bool cbHex16_IsValid(object sender)
		{
			if (alHex16cb.IndexOf(sender) < 0)
				throw new Exception("cbHex16_IsValid not applicable to control " + sender.ToString());
			if (((ComboBox)sender).Items.IndexOf(((ComboBox)sender).Text) != -1) return true;

			try { Convert.ToUInt16(((ComboBox)sender).Text, 16); }
			catch (Exception) { return false; }
			return true;
		}

		private bool dec8_IsValid(object sender)
		{
			if (alDec8.IndexOf(sender) < 0)
				throw new Exception("dec8_IsValid not applicable to control " + sender.ToString());
			try { Convert.ToByte(((TextBox)sender).Text); }
			catch (Exception) { return false; }
			return true;
		}

		private bool hex8_IsValid(object sender)
		{
			if (alHex8.IndexOf(sender) < 0)
				throw new Exception("hex8_IsValid not applicable to control " + sender.ToString());
			try { Convert.ToByte(((TextBox)sender).Text, 16); }
			catch (Exception) { return false; }
			return true;
		}

		private bool hex16_IsValid(object sender)
		{
			if (alHex16.IndexOf(sender) < 0)
				throw new Exception("hex16_IsValid not applicable to control " + sender.ToString());
			try { Convert.ToUInt16(((TextBox)sender).Text, 16); }
			catch (Exception) { return false; }
			return true;
		}

		private bool hex32_IsValid(object sender)
		{
			if (alHex32.IndexOf(sender) < 0)
				throw new Exception("hex32_IsValid not applicable to control " + sender.ToString());
			try { Convert.ToUInt32(((TextBox)sender).Text, 16); }
			catch (Exception) { return false; }
			return true;
		}


		private void FiletableRefresh(object sender, System.EventArgs e)
		{
            pjse_banner1.SiblingEnabled = wrapper != null && wrapper.SiblingResource(TPRP.TPRPtype) != null;
            UpdateInstPanel();
		}

		#endregion

		#region IPackedFileUI Member
		/// <summary>
		/// Returns the Control that will be displayed within SimPe
		/// </summary>
		public Control GUIHandle
		{
			get
			{
				return bhavPanel;
			}
		}

		/// <summary>
		/// Called by the AbstractWrapper when the file should be displayed to the user.
		/// </summary>
		/// <param name="wrp">Reference to the wrapper to be displayed.</param>
		public void UpdateGUI(IFileWrapper wrp) // Fuck
		{
			wrapper = (Bhav) wrp;

			internalchg = true;
            this.tbLines.Text = "0x0001";
			internalchg = false;

			this.WrapperChanged(wrapper, null);
            pjse_banner1.SiblingEnabled = wrapper.SiblingResource(TPRP.TPRPtype) != null;

			currentInst = null;
			origInst = null;
			UpdateInstPanel();
			this.pnflowcontainer.UpdateGUI(wrapper);
			// pnflowcontainer to install its handler before us.
			if (!setHandler)
			{
				wrapper.WrapperChanged += new System.EventHandler(this.WrapperChanged);
                wrapper.FileDescriptor.DescriptionChanged += new EventHandler(FileDescriptor_DescriptionChanged);
				setHandler = true;
			}

            if (this.isPopup)
            {
                currentPackage = pjse.FileTable.GFT.CurrentPackage;
                pjse_banner1.TreeVisible = pjse_banner1.ViewVisible = pjse_banner1.FloatVisible = false;
                btnClose.Visible = gbSpecial.Visible = true;
                button1.Enabled = cbSpecial.Enabled = false;
                btnCopyBHAV.Visible = (currentPackage != wrapper.Package);
                btnImportBHAV.Visible = (currentPackage != wrapper.Package)
                    && (callerID != null && callerID.IndexOf("-FFFFFFFF-") == 17); //42484156-00000000-FFFFFFFF-00001003
                btnCopyBHAV.Enabled = currentPackage != null;
                btnImportBHAV.Enabled = (currentPackage != null) &&
                    ((wrapper.FileDescriptor.Instance >= 0x100 && wrapper.FileDescriptor.Instance < 0x1000)
                    || (wrapper.FileDescriptor.Instance >= 0x2000 && wrapper.FileDescriptor.Instance < 0x3000));

                handleOverride();

                this.Text = formTitle;
                ttBhavForm.SetToolTip(tbFilename, null);
            }
            else
            {
                this.lbHidesOP.Visible = this.tbHidesOP.Visible = this.llHidesOP.Visible = false;
                this.llHidesOP.Tag = null;
                if (wrapper.SiblingResource(0x54524545) != null && Helper.WindowsRegistry.CreatorMode)
                {
                    pjse_banner1.TreeVisible = true;
                    pjse_banner1.TreeEnabled = wrapper.SiblingResource(0x54524545).Package == wrapper.Package;
                    button1.Enabled = false;
                }
                else
                {
                    pjse_banner1.TreeVisible = false;
                    button1.Enabled = true;
                }
                currentPackage = wrapper.Package;
                ttBhavForm.SetToolTip(tbFilename, expName + ": 0x" + SimPe.Helper.HexString((ushort)wrapper.FileDescriptor.Instance));
            }
            SetComments();
        }

        void FileDescriptor_DescriptionChanged(object sender, EventArgs e)
        {
            pjse_banner1.SiblingEnabled = wrapper.SiblingResource(TPRP.TPRPtype) != null;
            if (isPopup)
                this.Text = formTitle;
            else
            {
                ttBhavForm.SetToolTip(tbFilename, expName + ": 0x" + SimPe.Helper.HexString((ushort)wrapper.FileDescriptor.Instance));
                pjse_banner1.TreeVisible = (wrapper.SiblingResource(0x54524545) != null && Helper.WindowsRegistry.CreatorMode);
            }
            SetComments();
        }

        private void WrapperChanged(object sender, System.EventArgs e)
        {
            if (isPopup) wrapper.Changed = false;

            this.btnCommit.Enabled = wrapper.Changed;

            // Handler for header
            if (sender == wrapper && !internalchg)
            {
                internalchg = true;
                /*this.Text = */
                tbFilename.Text = wrapper.FileName;
                cbFormat.Text = "0x" + Helper.HexString(wrapper.Header.Format);
                tbType.Text = "0x" + Helper.HexString(wrapper.Header.Type);
                tbArgC.Text = "0x" + Helper.HexString(wrapper.Header.ArgumentCount);
                tbLocalC.Text = "0x" + Helper.HexString(wrapper.Header.LocalVarCount);
                tbHeaderFlag.Text = "0x" + Helper.HexString(wrapper.Header.HeaderFlag);
                tbTreeVersion.Text = "0x" + Helper.HexString(wrapper.Header.TreeVersion);
                tbCacheFlags.Text = "0x" + Helper.HexString(wrapper.Header.CacheFlags);
                tbCacheFlags.Enabled = (wrapper.Header.Format > 0x8008);
                cmpBHAV.Wrapper = wrapper;
                cmpBHAV.WrapperName = wrapper.FileName;
                internalchg = false;
            }

            // Handler for current instruction
            if (currentInst != null && sender == currentInst.Instruction)
            {
                if (internalchg)
                    this.btnCancel.Enabled = true;
                else
                    pnflowcontainer_SelectedInstChanged(null, null);
            }
            SetComments();
        }

		#endregion

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.gbInstruction = new System.Windows.Forms.GroupBox();
            this.btnZero = new System.Windows.Forms.Button();
            this.tbInst_Longname = new System.Windows.Forms.TextBox();
            this.btnOperandRaw = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOperandWiz = new System.Windows.Forms.Button();
            this.llopenbhav = new System.Windows.Forms.LinkLabel();
            this.tba2 = new System.Windows.Forms.ComboBox();
            this.tba1 = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.tbInst_Unk7 = new System.Windows.Forms.TextBox();
            this.tbInst_Unk6 = new System.Windows.Forms.TextBox();
            this.tbInst_Unk5 = new System.Windows.Forms.TextBox();
            this.tbInst_Unk4 = new System.Windows.Forms.TextBox();
            this.tbInst_Unk3 = new System.Windows.Forms.TextBox();
            this.tbInst_Unk2 = new System.Windows.Forms.TextBox();
            this.tbInst_Unk1 = new System.Windows.Forms.TextBox();
            this.tbInst_Unk0 = new System.Windows.Forms.TextBox();
            this.tbInst_Op7 = new System.Windows.Forms.TextBox();
            this.tbInst_Op6 = new System.Windows.Forms.TextBox();
            this.tbInst_Op5 = new System.Windows.Forms.TextBox();
            this.tbInst_Op4 = new System.Windows.Forms.TextBox();
            this.tbInst_Op3 = new System.Windows.Forms.TextBox();
            this.tbInst_Op2 = new System.Windows.Forms.TextBox();
            this.tbInst_Op1 = new System.Windows.Forms.TextBox();
            this.tbInst_Op0 = new System.Windows.Forms.TextBox();
            this.tbInst_NodeVersion = new System.Windows.Forms.TextBox();
            this.tbInst_OpCode = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.btnOpCode = new System.Windows.Forms.Button();
            this.tbFilename = new System.Windows.Forms.TextBox();
            this.lbFilename = new System.Windows.Forms.Label();
            this.tbLocalC = new System.Windows.Forms.TextBox();
            this.tbArgC = new System.Windows.Forms.TextBox();
            this.tbType = new System.Windows.Forms.TextBox();
            this.lbTreeVersion = new System.Windows.Forms.Label();
            this.lbType = new System.Windows.Forms.Label();
            this.lbLocalC = new System.Windows.Forms.Label();
            this.lbArgC = new System.Windows.Forms.Label();
            this.lbFormat = new System.Windows.Forms.Label();
            this.bhavPanel = new System.Windows.Forms.Panel();
            this.pjse_banner1 = new pjse.pjse_banner();
            this.lbHidesOP = new System.Windows.Forms.Label();
            this.gbSpecial = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.cmpBHAV = new pjse.CompareButton();
            this.btnPasteListing = new System.Windows.Forms.Button();
            this.btnAppend = new System.Windows.Forms.Button();
            this.btnInsTrue = new System.Windows.Forms.Button();
            this.btnInsFalse = new System.Windows.Forms.Button();
            this.btnDelPescado = new System.Windows.Forms.Button();
            this.btnLinkInge = new System.Windows.Forms.Button();
            this.btnGUIDIndex = new System.Windows.Forms.Button();
            this.btnInsUnlinked = new System.Windows.Forms.Button();
            this.btnDelMerola = new System.Windows.Forms.Button();
            this.btnCopyListing = new System.Windows.Forms.Button();
            this.btnTPRPMaker = new System.Windows.Forms.Button();
            this.llHidesOP = new System.Windows.Forms.LinkLabel();
            this.tbHidesOP = new System.Windows.Forms.TextBox();
            this.cbSpecial = new System.Windows.Forms.CheckBox();
            this.btnImportBHAV = new System.Windows.Forms.Button();
            this.btnCopyBHAV = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.tbHeaderFlag = new System.Windows.Forms.TextBox();
            this.lbHeaderFlag = new System.Windows.Forms.Label();
            this.tbCacheFlags = new System.Windows.Forms.TextBox();
            this.cbFormat = new System.Windows.Forms.ComboBox();
            this.pnflowcontainer = new SimPe.PackedFiles.UserInterface.BhavInstListControl();
            this.btnDel = new System.Windows.Forms.Button();
            this.gbMove = new System.Windows.Forms.GroupBox();
            this.btnUp = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.lbUpDown = new System.Windows.Forms.Label();
            this.tbLines = new System.Windows.Forms.TextBox();
            this.btnSort = new System.Windows.Forms.Button();
            this.btnCommit = new System.Windows.Forms.Button();
            this.tbTreeVersion = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.lbCacheFlags = new System.Windows.Forms.Label();
            this.cmenuGUIDIndex = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.createAllPackagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createCurrentPackageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadIndexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.defaultFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fromFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveIndexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.defaultFileToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ttBhavForm = new System.Windows.Forms.ToolTip(this.components);
            this.gbInstruction.SuspendLayout();
            this.bhavPanel.SuspendLayout();
            this.gbSpecial.SuspendLayout();
            this.gbMove.SuspendLayout();
            this.cmenuGUIDIndex.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbInstruction
            // 
            this.gbInstruction.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gbInstruction.BackColor = System.Drawing.Color.Transparent;
            this.gbInstruction.Controls.Add(this.btnZero);
            this.gbInstruction.Controls.Add(this.tbInst_Longname);
            this.gbInstruction.Controls.Add(this.btnOperandRaw);
            this.gbInstruction.Controls.Add(this.btnCancel);
            this.gbInstruction.Controls.Add(this.btnOperandWiz);
            this.gbInstruction.Controls.Add(this.llopenbhav);
            this.gbInstruction.Controls.Add(this.tba2);
            this.gbInstruction.Controls.Add(this.tba1);
            this.gbInstruction.Controls.Add(this.label13);
            this.gbInstruction.Controls.Add(this.tbInst_Unk7);
            this.gbInstruction.Controls.Add(this.tbInst_Unk6);
            this.gbInstruction.Controls.Add(this.tbInst_Unk5);
            this.gbInstruction.Controls.Add(this.tbInst_Unk4);
            this.gbInstruction.Controls.Add(this.tbInst_Unk3);
            this.gbInstruction.Controls.Add(this.tbInst_Unk2);
            this.gbInstruction.Controls.Add(this.tbInst_Unk1);
            this.gbInstruction.Controls.Add(this.tbInst_Unk0);
            this.gbInstruction.Controls.Add(this.tbInst_Op7);
            this.gbInstruction.Controls.Add(this.tbInst_Op6);
            this.gbInstruction.Controls.Add(this.tbInst_Op5);
            this.gbInstruction.Controls.Add(this.tbInst_Op4);
            this.gbInstruction.Controls.Add(this.tbInst_Op3);
            this.gbInstruction.Controls.Add(this.tbInst_Op2);
            this.gbInstruction.Controls.Add(this.tbInst_Op1);
            this.gbInstruction.Controls.Add(this.tbInst_Op0);
            this.gbInstruction.Controls.Add(this.tbInst_NodeVersion);
            this.gbInstruction.Controls.Add(this.tbInst_OpCode);
            this.gbInstruction.Controls.Add(this.label10);
            this.gbInstruction.Controls.Add(this.label9);
            this.gbInstruction.Controls.Add(this.label12);
            this.gbInstruction.Controls.Add(this.label11);
            this.gbInstruction.Controls.Add(this.btnOpCode);
            this.gbInstruction.Location = new System.Drawing.Point(395, 86);
            this.gbInstruction.Margin = new System.Windows.Forms.Padding(2);
            this.gbInstruction.Name = "gbInstruction";
            this.gbInstruction.Padding = new System.Windows.Forms.Padding(2);
            this.gbInstruction.Size = new System.Drawing.Size(362, 190);
            this.gbInstruction.TabIndex = 2;
            this.gbInstruction.TabStop = false;
            this.gbInstruction.Text = "&Instruction Settings";
            // 
            // btnZero
            // 
            this.btnZero.BackColor = System.Drawing.Color.Transparent;
            this.btnZero.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnZero.Location = new System.Drawing.Point(306, 87);
            this.btnZero.Margin = new System.Windows.Forms.Padding(0);
            this.btnZero.Name = "btnZero";
            this.btnZero.Size = new System.Drawing.Size(24, 22);
            this.btnZero.TabIndex = 26;
            this.btnZero.Text = "x";
            this.ttBhavForm.SetToolTip(this.btnZero, "Set all operands to zero");
            this.btnZero.UseVisualStyleBackColor = false;
            this.btnZero.Click += new System.EventHandler(this.btnZero_Click);
            // 
            // tbInst_Longname
            // 
            this.tbInst_Longname.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbInst_Longname.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbInst_Longname.Location = new System.Drawing.Point(8, 110);
            this.tbInst_Longname.Margin = new System.Windows.Forms.Padding(2);
            this.tbInst_Longname.Multiline = true;
            this.tbInst_Longname.Name = "tbInst_Longname";
            this.tbInst_Longname.ReadOnly = true;
            this.tbInst_Longname.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbInst_Longname.Size = new System.Drawing.Size(348, 76);
            this.tbInst_Longname.TabIndex = 27;
            this.tbInst_Longname.Text = "Instruction text here";
            this.ttBhavForm.SetToolTip(this.tbInst_Longname, "Click and drag to select\r\ntext for copying");
            // 
            // btnOperandRaw
            // 
            this.btnOperandRaw.BackColor = System.Drawing.Color.Transparent;
            this.btnOperandRaw.Font = new System.Drawing.Font("Wingdings", 14.25F);
            this.btnOperandRaw.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnOperandRaw.Location = new System.Drawing.Point(278, 87);
            this.btnOperandRaw.Margin = new System.Windows.Forms.Padding(0);
            this.btnOperandRaw.Name = "btnOperandRaw";
            this.btnOperandRaw.Size = new System.Drawing.Size(24, 22);
            this.btnOperandRaw.TabIndex = 25;
            this.btnOperandRaw.Text = "È";
            this.ttBhavForm.SetToolTip(this.btnOperandRaw, "Pop-up raw entry box");
            this.btnOperandRaw.UseVisualStyleBackColor = false;
            this.btnOperandRaw.Click += new System.EventHandler(this.btnOperandRaw_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCancel.Location = new System.Drawing.Point(252, 68);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(1);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(78, 19);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Clicked);
            // 
            // btnOperandWiz
            // 
            this.btnOperandWiz.BackColor = System.Drawing.Color.Transparent;
            this.btnOperandWiz.Font = new System.Drawing.Font("Webdings", 10.8F);
            this.btnOperandWiz.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnOperandWiz.Location = new System.Drawing.Point(252, 87);
            this.btnOperandWiz.Margin = new System.Windows.Forms.Padding(0);
            this.btnOperandWiz.Name = "btnOperandWiz";
            this.btnOperandWiz.Size = new System.Drawing.Size(24, 22);
            this.btnOperandWiz.TabIndex = 24;
            this.btnOperandWiz.Text = "@";
            this.ttBhavForm.SetToolTip(this.btnOperandWiz, "Pop-up Wizard");
            this.btnOperandWiz.UseVisualStyleBackColor = false;
            this.btnOperandWiz.Click += new System.EventHandler(this.btnOperandWiz_Clicked);
            // 
            // llopenbhav
            // 
            this.llopenbhav.AutoSize = true;
            this.llopenbhav.BackColor = System.Drawing.Color.Transparent;
            this.llopenbhav.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.llopenbhav.LinkArea = new System.Windows.Forms.LinkArea(0, 11);
            this.llopenbhav.Location = new System.Drawing.Point(149, 25);
            this.llopenbhav.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.llopenbhav.Name = "llopenbhav";
            this.llopenbhav.Size = new System.Drawing.Size(61, 17);
            this.llopenbhav.TabIndex = 3;
            this.llopenbhav.TabStop = true;
            this.llopenbhav.Text = "view BHAV";
            this.llopenbhav.UseCompatibleTextRendering = true;
            this.llopenbhav.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llopenbhav_LinkClicked);
            // 
            // tba2
            // 
            this.tba2.ItemHeight = 13;
            this.tba2.Items.AddRange(new object[] {
            "Error",
            "Return True",
            "Return False"});
            this.tba2.Location = new System.Drawing.Point(252, 45);
            this.tba2.Margin = new System.Windows.Forms.Padding(2);
            this.tba2.Name = "tba2";
            this.tba2.Size = new System.Drawing.Size(84, 21);
            this.tba2.TabIndex = 5;
            this.tba2.Text = "Return False";
            this.tba2.QueryContinueDrag += new System.Windows.Forms.QueryContinueDragEventHandler(this.ItemQueryContinueDragTarget);
            this.tba2.Validating += new System.ComponentModel.CancelEventHandler(this.cbHex16_Validating);
            this.tba2.DragOver += new System.Windows.Forms.DragEventHandler(this.ItemDragEnter);
            this.tba2.SelectedIndexChanged += new System.EventHandler(this.cbHex16_SelectedIndexChanged);
            this.tba2.Enter += new System.EventHandler(this.cbHex16_Enter);
            this.tba2.DragDrop += new System.Windows.Forms.DragEventHandler(this.ItemDrop);
            this.tba2.DragEnter += new System.Windows.Forms.DragEventHandler(this.ItemDragEnter);
            this.tba2.Validated += new System.EventHandler(this.cbHex16_Validated);
            this.tba2.TextChanged += new System.EventHandler(this.cbHex16_TextChanged);
            // 
            // tba1
            // 
            this.tba1.ItemHeight = 13;
            this.tba1.Items.AddRange(new object[] {
            "Error",
            "Return True",
            "Return False"});
            this.tba1.Location = new System.Drawing.Point(83, 45);
            this.tba1.Margin = new System.Windows.Forms.Padding(2);
            this.tba1.Name = "tba1";
            this.tba1.Size = new System.Drawing.Size(84, 21);
            this.tba1.TabIndex = 4;
            this.tba1.Text = "Return True";
            this.tba1.QueryContinueDrag += new System.Windows.Forms.QueryContinueDragEventHandler(this.ItemQueryContinueDragTarget);
            this.tba1.Validating += new System.ComponentModel.CancelEventHandler(this.cbHex16_Validating);
            this.tba1.DragOver += new System.Windows.Forms.DragEventHandler(this.ItemDragEnter);
            this.tba1.SelectedIndexChanged += new System.EventHandler(this.cbHex16_SelectedIndexChanged);
            this.tba1.Enter += new System.EventHandler(this.cbHex16_Enter);
            this.tba1.DragDrop += new System.Windows.Forms.DragEventHandler(this.ItemDrop);
            this.tba1.DragEnter += new System.Windows.Forms.DragEventHandler(this.ItemDragEnter);
            this.tba1.Validated += new System.EventHandler(this.cbHex16_Validated);
            this.tba1.TextChanged += new System.EventHandler(this.cbHex16_TextChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label13.Location = new System.Drawing.Point(19, 72);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(56, 13);
            this.label13.TabIndex = 5;
            this.label13.Text = "&Operands:";
            // 
            // tbInst_Unk7
            // 
            this.tbInst_Unk7.Location = new System.Drawing.Point(229, 88);
            this.tbInst_Unk7.Margin = new System.Windows.Forms.Padding(2);
            this.tbInst_Unk7.MaxLength = 2;
            this.tbInst_Unk7.Name = "tbInst_Unk7";
            this.tbInst_Unk7.Size = new System.Drawing.Size(22, 20);
            this.tbInst_Unk7.TabIndex = 23;
            this.tbInst_Unk7.Text = "0";
            this.tbInst_Unk7.TextChanged += new System.EventHandler(this.hex8_TextChanged);
            this.tbInst_Unk7.Validated += new System.EventHandler(this.hex8_Validated);
            this.tbInst_Unk7.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
            // 
            // tbInst_Unk6
            // 
            this.tbInst_Unk6.Location = new System.Drawing.Point(208, 88);
            this.tbInst_Unk6.Margin = new System.Windows.Forms.Padding(2);
            this.tbInst_Unk6.MaxLength = 2;
            this.tbInst_Unk6.Name = "tbInst_Unk6";
            this.tbInst_Unk6.Size = new System.Drawing.Size(22, 20);
            this.tbInst_Unk6.TabIndex = 22;
            this.tbInst_Unk6.Text = "0";
            this.tbInst_Unk6.TextChanged += new System.EventHandler(this.hex8_TextChanged);
            this.tbInst_Unk6.Validated += new System.EventHandler(this.hex8_Validated);
            this.tbInst_Unk6.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
            // 
            // tbInst_Unk5
            // 
            this.tbInst_Unk5.Location = new System.Drawing.Point(187, 88);
            this.tbInst_Unk5.Margin = new System.Windows.Forms.Padding(2);
            this.tbInst_Unk5.MaxLength = 2;
            this.tbInst_Unk5.Name = "tbInst_Unk5";
            this.tbInst_Unk5.Size = new System.Drawing.Size(22, 20);
            this.tbInst_Unk5.TabIndex = 21;
            this.tbInst_Unk5.Text = "0";
            this.tbInst_Unk5.TextChanged += new System.EventHandler(this.hex8_TextChanged);
            this.tbInst_Unk5.Validated += new System.EventHandler(this.hex8_Validated);
            this.tbInst_Unk5.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
            // 
            // tbInst_Unk4
            // 
            this.tbInst_Unk4.Location = new System.Drawing.Point(166, 88);
            this.tbInst_Unk4.Margin = new System.Windows.Forms.Padding(2);
            this.tbInst_Unk4.MaxLength = 2;
            this.tbInst_Unk4.Name = "tbInst_Unk4";
            this.tbInst_Unk4.Size = new System.Drawing.Size(22, 20);
            this.tbInst_Unk4.TabIndex = 20;
            this.tbInst_Unk4.Text = "0";
            this.tbInst_Unk4.TextChanged += new System.EventHandler(this.hex8_TextChanged);
            this.tbInst_Unk4.Validated += new System.EventHandler(this.hex8_Validated);
            this.tbInst_Unk4.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
            // 
            // tbInst_Unk3
            // 
            this.tbInst_Unk3.Location = new System.Drawing.Point(146, 88);
            this.tbInst_Unk3.Margin = new System.Windows.Forms.Padding(2);
            this.tbInst_Unk3.MaxLength = 2;
            this.tbInst_Unk3.Name = "tbInst_Unk3";
            this.tbInst_Unk3.Size = new System.Drawing.Size(22, 20);
            this.tbInst_Unk3.TabIndex = 19;
            this.tbInst_Unk3.Text = "0";
            this.tbInst_Unk3.TextChanged += new System.EventHandler(this.hex8_TextChanged);
            this.tbInst_Unk3.Validated += new System.EventHandler(this.hex8_Validated);
            this.tbInst_Unk3.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
            // 
            // tbInst_Unk2
            // 
            this.tbInst_Unk2.Location = new System.Drawing.Point(125, 88);
            this.tbInst_Unk2.Margin = new System.Windows.Forms.Padding(2);
            this.tbInst_Unk2.MaxLength = 2;
            this.tbInst_Unk2.Name = "tbInst_Unk2";
            this.tbInst_Unk2.Size = new System.Drawing.Size(22, 20);
            this.tbInst_Unk2.TabIndex = 18;
            this.tbInst_Unk2.Text = "0";
            this.tbInst_Unk2.TextChanged += new System.EventHandler(this.hex8_TextChanged);
            this.tbInst_Unk2.Validated += new System.EventHandler(this.hex8_Validated);
            this.tbInst_Unk2.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
            // 
            // tbInst_Unk1
            // 
            this.tbInst_Unk1.Location = new System.Drawing.Point(104, 88);
            this.tbInst_Unk1.Margin = new System.Windows.Forms.Padding(2);
            this.tbInst_Unk1.MaxLength = 2;
            this.tbInst_Unk1.Name = "tbInst_Unk1";
            this.tbInst_Unk1.Size = new System.Drawing.Size(22, 20);
            this.tbInst_Unk1.TabIndex = 17;
            this.tbInst_Unk1.Text = "0";
            this.tbInst_Unk1.TextChanged += new System.EventHandler(this.hex8_TextChanged);
            this.tbInst_Unk1.Validated += new System.EventHandler(this.hex8_Validated);
            this.tbInst_Unk1.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
            // 
            // tbInst_Unk0
            // 
            this.tbInst_Unk0.Location = new System.Drawing.Point(83, 88);
            this.tbInst_Unk0.Margin = new System.Windows.Forms.Padding(2);
            this.tbInst_Unk0.MaxLength = 2;
            this.tbInst_Unk0.Name = "tbInst_Unk0";
            this.tbInst_Unk0.Size = new System.Drawing.Size(22, 20);
            this.tbInst_Unk0.TabIndex = 16;
            this.tbInst_Unk0.Text = "0";
            this.tbInst_Unk0.TextChanged += new System.EventHandler(this.hex8_TextChanged);
            this.tbInst_Unk0.Validated += new System.EventHandler(this.hex8_Validated);
            this.tbInst_Unk0.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
            // 
            // tbInst_Op7
            // 
            this.tbInst_Op7.Location = new System.Drawing.Point(229, 69);
            this.tbInst_Op7.Margin = new System.Windows.Forms.Padding(2);
            this.tbInst_Op7.MaxLength = 2;
            this.tbInst_Op7.Name = "tbInst_Op7";
            this.tbInst_Op7.Size = new System.Drawing.Size(22, 20);
            this.tbInst_Op7.TabIndex = 15;
            this.tbInst_Op7.Text = "0";
            this.tbInst_Op7.TextChanged += new System.EventHandler(this.hex8_TextChanged);
            this.tbInst_Op7.Validated += new System.EventHandler(this.hex8_Validated);
            this.tbInst_Op7.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
            // 
            // tbInst_Op6
            // 
            this.tbInst_Op6.Location = new System.Drawing.Point(208, 69);
            this.tbInst_Op6.Margin = new System.Windows.Forms.Padding(2);
            this.tbInst_Op6.MaxLength = 2;
            this.tbInst_Op6.Name = "tbInst_Op6";
            this.tbInst_Op6.Size = new System.Drawing.Size(22, 20);
            this.tbInst_Op6.TabIndex = 14;
            this.tbInst_Op6.Text = "0";
            this.tbInst_Op6.TextChanged += new System.EventHandler(this.hex8_TextChanged);
            this.tbInst_Op6.Validated += new System.EventHandler(this.hex8_Validated);
            this.tbInst_Op6.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
            // 
            // tbInst_Op5
            // 
            this.tbInst_Op5.Location = new System.Drawing.Point(187, 69);
            this.tbInst_Op5.Margin = new System.Windows.Forms.Padding(2);
            this.tbInst_Op5.MaxLength = 2;
            this.tbInst_Op5.Name = "tbInst_Op5";
            this.tbInst_Op5.Size = new System.Drawing.Size(22, 20);
            this.tbInst_Op5.TabIndex = 13;
            this.tbInst_Op5.Text = "0";
            this.tbInst_Op5.TextChanged += new System.EventHandler(this.hex8_TextChanged);
            this.tbInst_Op5.Validated += new System.EventHandler(this.hex8_Validated);
            this.tbInst_Op5.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
            // 
            // tbInst_Op4
            // 
            this.tbInst_Op4.Location = new System.Drawing.Point(166, 69);
            this.tbInst_Op4.Margin = new System.Windows.Forms.Padding(2);
            this.tbInst_Op4.MaxLength = 2;
            this.tbInst_Op4.Name = "tbInst_Op4";
            this.tbInst_Op4.Size = new System.Drawing.Size(22, 20);
            this.tbInst_Op4.TabIndex = 12;
            this.tbInst_Op4.Text = "0";
            this.tbInst_Op4.TextChanged += new System.EventHandler(this.hex8_TextChanged);
            this.tbInst_Op4.Validated += new System.EventHandler(this.hex8_Validated);
            this.tbInst_Op4.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
            // 
            // tbInst_Op3
            // 
            this.tbInst_Op3.Location = new System.Drawing.Point(146, 69);
            this.tbInst_Op3.Margin = new System.Windows.Forms.Padding(2);
            this.tbInst_Op3.MaxLength = 2;
            this.tbInst_Op3.Name = "tbInst_Op3";
            this.tbInst_Op3.Size = new System.Drawing.Size(22, 20);
            this.tbInst_Op3.TabIndex = 11;
            this.tbInst_Op3.Text = "0";
            this.tbInst_Op3.TextChanged += new System.EventHandler(this.hex8_TextChanged);
            this.tbInst_Op3.Validated += new System.EventHandler(this.hex8_Validated);
            this.tbInst_Op3.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
            // 
            // tbInst_Op2
            // 
            this.tbInst_Op2.Location = new System.Drawing.Point(125, 69);
            this.tbInst_Op2.Margin = new System.Windows.Forms.Padding(2);
            this.tbInst_Op2.MaxLength = 2;
            this.tbInst_Op2.Name = "tbInst_Op2";
            this.tbInst_Op2.Size = new System.Drawing.Size(22, 20);
            this.tbInst_Op2.TabIndex = 10;
            this.tbInst_Op2.Text = "0";
            this.tbInst_Op2.TextChanged += new System.EventHandler(this.hex8_TextChanged);
            this.tbInst_Op2.Validated += new System.EventHandler(this.hex8_Validated);
            this.tbInst_Op2.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
            // 
            // tbInst_Op1
            // 
            this.tbInst_Op1.Location = new System.Drawing.Point(104, 69);
            this.tbInst_Op1.Margin = new System.Windows.Forms.Padding(2);
            this.tbInst_Op1.MaxLength = 2;
            this.tbInst_Op1.Name = "tbInst_Op1";
            this.tbInst_Op1.Size = new System.Drawing.Size(22, 20);
            this.tbInst_Op1.TabIndex = 9;
            this.tbInst_Op1.Text = "0";
            this.tbInst_Op1.TextChanged += new System.EventHandler(this.hex8_TextChanged);
            this.tbInst_Op1.Validated += new System.EventHandler(this.hex8_Validated);
            this.tbInst_Op1.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
            // 
            // tbInst_Op0
            // 
            this.tbInst_Op0.Location = new System.Drawing.Point(83, 69);
            this.tbInst_Op0.Margin = new System.Windows.Forms.Padding(2);
            this.tbInst_Op0.MaxLength = 2;
            this.tbInst_Op0.Name = "tbInst_Op0";
            this.tbInst_Op0.Size = new System.Drawing.Size(22, 20);
            this.tbInst_Op0.TabIndex = 8;
            this.tbInst_Op0.Text = "DD";
            this.tbInst_Op0.TextChanged += new System.EventHandler(this.hex8_TextChanged);
            this.tbInst_Op0.Validated += new System.EventHandler(this.hex8_Validated);
            this.tbInst_Op0.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
            // 
            // tbInst_NodeVersion
            // 
            this.tbInst_NodeVersion.Location = new System.Drawing.Point(304, 23);
            this.tbInst_NodeVersion.Margin = new System.Windows.Forms.Padding(2);
            this.tbInst_NodeVersion.MaxLength = 4;
            this.tbInst_NodeVersion.Name = "tbInst_NodeVersion";
            this.tbInst_NodeVersion.Size = new System.Drawing.Size(32, 20);
            this.tbInst_NodeVersion.TabIndex = 6;
            this.tbInst_NodeVersion.Text = "0xDD";
            this.tbInst_NodeVersion.TextChanged += new System.EventHandler(this.hex8_TextChanged);
            this.tbInst_NodeVersion.Validated += new System.EventHandler(this.hex8_Validated);
            this.tbInst_NodeVersion.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
            // 
            // tbInst_OpCode
            // 
            this.tbInst_OpCode.Location = new System.Drawing.Point(83, 23);
            this.tbInst_OpCode.Margin = new System.Windows.Forms.Padding(2);
            this.tbInst_OpCode.MaxLength = 6;
            this.tbInst_OpCode.Name = "tbInst_OpCode";
            this.tbInst_OpCode.Size = new System.Drawing.Size(48, 20);
            this.tbInst_OpCode.TabIndex = 1;
            this.tbInst_OpCode.Text = "0xDDDD";
            this.tbInst_OpCode.TextChanged += new System.EventHandler(this.hex16_TextChanged);
            this.tbInst_OpCode.Validated += new System.EventHandler(this.hex16_Validated);
            this.tbInst_OpCode.Validating += new System.ComponentModel.CancelEventHandler(this.hex16_Validating);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label10.Location = new System.Drawing.Point(221, 25);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(74, 13);
            this.label10.TabIndex = 0;
            this.label10.Text = "Node Version:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label9.Location = new System.Drawing.Point(27, 25);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(49, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "OpCode:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label12.Location = new System.Drawing.Point(174, 48);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(69, 13);
            this.label12.TabIndex = 0;
            this.label12.Text = "False Target:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label11.Location = new System.Drawing.Point(9, 48);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(66, 13);
            this.label11.TabIndex = 0;
            this.label11.Text = "True Target:";
            // 
            // btnOpCode
            // 
            this.btnOpCode.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnOpCode.Font = new System.Drawing.Font("Webdings", 10F);
            this.btnOpCode.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnOpCode.Location = new System.Drawing.Point(130, 24);
            this.btnOpCode.Margin = new System.Windows.Forms.Padding(2);
            this.btnOpCode.Name = "btnOpCode";
            this.btnOpCode.Size = new System.Drawing.Size(17, 16);
            this.btnOpCode.TabIndex = 2;
            this.btnOpCode.Text = "4";
            this.btnOpCode.Click += new System.EventHandler(this.btnOpCode_Clicked);
            // 
            // tbFilename
            // 
            this.tbFilename.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFilename.Location = new System.Drawing.Point(57, 32);
            this.tbFilename.Margin = new System.Windows.Forms.Padding(2);
            this.tbFilename.MaxLength = 64;
            this.tbFilename.Name = "tbFilename";
            this.tbFilename.Size = new System.Drawing.Size(97, 20);
            this.tbFilename.TabIndex = 3;
            this.tbFilename.Text = "ffffff";
            this.tbFilename.TextChanged += new System.EventHandler(this.tbFilename_TextChanged);
            this.tbFilename.Validated += new System.EventHandler(this.tbFilename_Validated);
            // 
            // lbFilename
            // 
            this.lbFilename.AutoSize = true;
            this.lbFilename.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbFilename.Location = new System.Drawing.Point(0, 34);
            this.lbFilename.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbFilename.Name = "lbFilename";
            this.lbFilename.Size = new System.Drawing.Size(49, 13);
            this.lbFilename.TabIndex = 0;
            this.lbFilename.Text = "Filename";
            this.lbFilename.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // tbLocalC
            // 
            this.tbLocalC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbLocalC.Location = new System.Drawing.Point(725, 60);
            this.tbLocalC.Margin = new System.Windows.Forms.Padding(2);
            this.tbLocalC.MaxLength = 4;
            this.tbLocalC.Name = "tbLocalC";
            this.tbLocalC.Size = new System.Drawing.Size(32, 20);
            this.tbLocalC.TabIndex = 10;
            this.tbLocalC.Text = "0xDD";
            this.ttBhavForm.SetToolTip(this.tbLocalC, "Number of Local\r\nvariables used\r\nin BHAV");
            this.tbLocalC.TextChanged += new System.EventHandler(this.hex8_TextChanged);
            this.tbLocalC.Validated += new System.EventHandler(this.hex8_Validated);
            this.tbLocalC.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
            // 
            // tbArgC
            // 
            this.tbArgC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbArgC.Location = new System.Drawing.Point(596, 60);
            this.tbArgC.Margin = new System.Windows.Forms.Padding(2);
            this.tbArgC.MaxLength = 4;
            this.tbArgC.Name = "tbArgC";
            this.tbArgC.Size = new System.Drawing.Size(32, 20);
            this.tbArgC.TabIndex = 9;
            this.tbArgC.Text = "0xDD";
            this.ttBhavForm.SetToolTip(this.tbArgC, "Number of\r\nparameters passed\r\nto BHAV");
            this.tbArgC.TextChanged += new System.EventHandler(this.hex8_TextChanged);
            this.tbArgC.Validated += new System.EventHandler(this.hex8_Validated);
            this.tbArgC.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
            // 
            // tbType
            // 
            this.tbType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbType.Location = new System.Drawing.Point(343, 32);
            this.tbType.Margin = new System.Windows.Forms.Padding(2);
            this.tbType.MaxLength = 4;
            this.tbType.Name = "tbType";
            this.tbType.Size = new System.Drawing.Size(32, 20);
            this.tbType.TabIndex = 5;
            this.tbType.Text = "0xDD";
            this.tbType.TextChanged += new System.EventHandler(this.hex8_TextChanged);
            this.tbType.Validated += new System.EventHandler(this.hex8_Validated);
            this.tbType.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
            // 
            // lbTreeVersion
            // 
            this.lbTreeVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbTreeVersion.AutoSize = true;
            this.lbTreeVersion.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbTreeVersion.Location = new System.Drawing.Point(493, 34);
            this.lbTreeVersion.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbTreeVersion.Name = "lbTreeVersion";
            this.lbTreeVersion.Size = new System.Drawing.Size(67, 13);
            this.lbTreeVersion.TabIndex = 0;
            this.lbTreeVersion.Text = "Tree Version";
            this.lbTreeVersion.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lbType
            // 
            this.lbType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbType.AutoSize = true;
            this.lbType.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbType.Location = new System.Drawing.Point(279, 34);
            this.lbType.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbType.Name = "lbType";
            this.lbType.Size = new System.Drawing.Size(56, 13);
            this.lbType.TabIndex = 0;
            this.lbType.Text = "Tree Type";
            this.lbType.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lbLocalC
            // 
            this.lbLocalC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbLocalC.AutoSize = true;
            this.lbLocalC.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbLocalC.Location = new System.Drawing.Point(635, 63);
            this.lbLocalC.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbLocalC.Name = "lbLocalC";
            this.lbLocalC.Size = new System.Drawing.Size(83, 13);
            this.lbLocalC.TabIndex = 0;
            this.lbLocalC.Text = "Local Var Count";
            this.lbLocalC.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lbArgC
            // 
            this.lbArgC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbArgC.AutoSize = true;
            this.lbArgC.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbArgC.Location = new System.Drawing.Point(535, 63);
            this.lbArgC.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbArgC.Name = "lbArgC";
            this.lbArgC.Size = new System.Drawing.Size(54, 13);
            this.lbArgC.TabIndex = 0;
            this.lbArgC.Text = "Arg Count";
            this.lbArgC.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lbFormat
            // 
            this.lbFormat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbFormat.AutoSize = true;
            this.lbFormat.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbFormat.Location = new System.Drawing.Point(160, 34);
            this.lbFormat.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbFormat.Name = "lbFormat";
            this.lbFormat.Size = new System.Drawing.Size(39, 13);
            this.lbFormat.TabIndex = 0;
            this.lbFormat.Text = "Format";
            this.lbFormat.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // bhavPanel
            // 
            this.bhavPanel.AutoScroll = true;
            this.bhavPanel.Controls.Add(this.pjse_banner1);
            this.bhavPanel.Controls.Add(this.lbHidesOP);
            this.bhavPanel.Controls.Add(this.gbSpecial);
            this.bhavPanel.Controls.Add(this.llHidesOP);
            this.bhavPanel.Controls.Add(this.tbHidesOP);
            this.bhavPanel.Controls.Add(this.cbSpecial);
            this.bhavPanel.Controls.Add(this.btnImportBHAV);
            this.bhavPanel.Controls.Add(this.btnCopyBHAV);
            this.bhavPanel.Controls.Add(this.btnClose);
            this.bhavPanel.Controls.Add(this.tbHeaderFlag);
            this.bhavPanel.Controls.Add(this.lbHeaderFlag);
            this.bhavPanel.Controls.Add(this.tbCacheFlags);
            this.bhavPanel.Controls.Add(this.cbFormat);
            this.bhavPanel.Controls.Add(this.pnflowcontainer);
            this.bhavPanel.Controls.Add(this.btnDel);
            this.bhavPanel.Controls.Add(this.gbMove);
            this.bhavPanel.Controls.Add(this.btnSort);
            this.bhavPanel.Controls.Add(this.btnCommit);
            this.bhavPanel.Controls.Add(this.lbFilename);
            this.bhavPanel.Controls.Add(this.tbFilename);
            this.bhavPanel.Controls.Add(this.gbInstruction);
            this.bhavPanel.Controls.Add(this.tbLocalC);
            this.bhavPanel.Controls.Add(this.tbTreeVersion);
            this.bhavPanel.Controls.Add(this.tbArgC);
            this.bhavPanel.Controls.Add(this.tbType);
            this.bhavPanel.Controls.Add(this.lbTreeVersion);
            this.bhavPanel.Controls.Add(this.lbType);
            this.bhavPanel.Controls.Add(this.lbLocalC);
            this.bhavPanel.Controls.Add(this.lbArgC);
            this.bhavPanel.Controls.Add(this.lbFormat);
            this.bhavPanel.Controls.Add(this.btnAdd);
            this.bhavPanel.Controls.Add(this.lbCacheFlags);
            this.bhavPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bhavPanel.Location = new System.Drawing.Point(0, 0);
            this.bhavPanel.Margin = new System.Windows.Forms.Padding(2);
            this.bhavPanel.Name = "bhavPanel";
            this.bhavPanel.Size = new System.Drawing.Size(760, 564);
            this.bhavPanel.TabIndex = 1;
            // 
            // pjse_banner1
            // 
            this.pjse_banner1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pjse_banner1.ExtractVisible = true;
            this.pjse_banner1.FloatVisible = true;
            this.pjse_banner1.Location = new System.Drawing.Point(0, 0);
            this.pjse_banner1.Margin = new System.Windows.Forms.Padding(2);
            this.pjse_banner1.Name = "pjse_banner1";
            this.pjse_banner1.Padding = new System.Windows.Forms.Padding(0, 0, 0, 1);
            this.pjse_banner1.SiblingText = "TPRP";
            this.pjse_banner1.SiblingVisible = true;
            this.pjse_banner1.Size = new System.Drawing.Size(758, 28);
            this.pjse_banner1.TabIndex = 22;
            this.pjse_banner1.TitleText = "Behaviour Function";
            this.pjse_banner1.TreeText = "Comments";
            this.pjse_banner1.ViewVisible = true;
            this.pjse_banner1.ExtractClick += new System.EventHandler(this.pjse_banner1_ExtractClick);
            this.pjse_banner1.SiblingClick += new System.EventHandler(this.pjse_banner1_SiblingClick);
            this.pjse_banner1.TreeClick += new System.EventHandler(this.pjse_banner1_TreeClick);
            this.pjse_banner1.ViewClick += new System.EventHandler(this.pjse_banner1_ViewClick);
            this.pjse_banner1.FloatClick += new System.EventHandler(this.btnFloat_Click);
            // 
            // lbHidesOP
            // 
            this.lbHidesOP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbHidesOP.AutoSize = true;
            this.lbHidesOP.BackColor = System.Drawing.Color.Transparent;
            this.lbHidesOP.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbHidesOP.Location = new System.Drawing.Point(395, 444);
            this.lbHidesOP.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbHidesOP.Name = "lbHidesOP";
            this.lbHidesOP.Size = new System.Drawing.Size(121, 13);
            this.lbHidesOP.TabIndex = 0;
            this.lbHidesOP.Text = "Displayed BHAV is from:";
            // 
            // gbSpecial
            // 
            this.gbSpecial.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gbSpecial.BackColor = System.Drawing.Color.Transparent;
            this.gbSpecial.Controls.Add(this.button1);
            this.gbSpecial.Controls.Add(this.cmpBHAV);
            this.gbSpecial.Controls.Add(this.btnPasteListing);
            this.gbSpecial.Controls.Add(this.btnAppend);
            this.gbSpecial.Controls.Add(this.btnInsTrue);
            this.gbSpecial.Controls.Add(this.btnInsFalse);
            this.gbSpecial.Controls.Add(this.btnDelPescado);
            this.gbSpecial.Controls.Add(this.btnLinkInge);
            this.gbSpecial.Controls.Add(this.btnGUIDIndex);
            this.gbSpecial.Controls.Add(this.btnInsUnlinked);
            this.gbSpecial.Controls.Add(this.btnDelMerola);
            this.gbSpecial.Controls.Add(this.btnCopyListing);
            this.gbSpecial.Controls.Add(this.btnTPRPMaker);
            this.gbSpecial.Location = new System.Drawing.Point(395, 336);
            this.gbSpecial.Margin = new System.Windows.Forms.Padding(2);
            this.gbSpecial.Name = "gbSpecial";
            this.gbSpecial.Padding = new System.Windows.Forms.Padding(2);
            this.gbSpecial.Size = new System.Drawing.Size(341, 86);
            this.gbSpecial.TabIndex = 17;
            this.gbSpecial.TabStop = false;
            this.gbSpecial.Text = "Special buttons";
            this.gbSpecial.Visible = false;
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.button1.Location = new System.Drawing.Point(275, 61);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(60, 18);
            this.button1.TabIndex = 12;
            this.button1.Text = "Comments";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cmpBHAV
            // 
            this.cmpBHAV.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmpBHAV.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cmpBHAV.Location = new System.Drawing.Point(199, 61);
            this.cmpBHAV.Margin = new System.Windows.Forms.Padding(2);
            this.cmpBHAV.Name = "cmpBHAV";
            this.cmpBHAV.Size = new System.Drawing.Size(73, 18);
            this.cmpBHAV.TabIndex = 11;
            this.cmpBHAV.Text = "Compare";
            this.cmpBHAV.UseVisualStyleBackColor = true;
            this.cmpBHAV.Wrapper = null;
            this.cmpBHAV.WrapperName = null;
            this.cmpBHAV.CompareWith += new pjse.CompareButton.CompareWithEventHandler(this.cmpBHAV_CompareWith);
            // 
            // btnPasteListing
            // 
            this.btnPasteListing.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnPasteListing.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnPasteListing.Location = new System.Drawing.Point(56, 17);
            this.btnPasteListing.Margin = new System.Windows.Forms.Padding(2);
            this.btnPasteListing.Name = "btnPasteListing";
            this.btnPasteListing.Size = new System.Drawing.Size(48, 18);
            this.btnPasteListing.TabIndex = 10;
            this.btnPasteListing.Text = "Paste";
            this.btnPasteListing.Click += new System.EventHandler(this.btnPasteListing_Click);
            // 
            // btnAppend
            // 
            this.btnAppend.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnAppend.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnAppend.Location = new System.Drawing.Point(236, 39);
            this.btnAppend.Margin = new System.Windows.Forms.Padding(2);
            this.btnAppend.Name = "btnAppend";
            this.btnAppend.Size = new System.Drawing.Size(100, 18);
            this.btnAppend.TabIndex = 7;
            this.btnAppend.Text = "Append BHAV";
            this.btnAppend.Click += new System.EventHandler(this.btnAppend_Click);
            // 
            // btnInsTrue
            // 
            this.btnInsTrue.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnInsTrue.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnInsTrue.Location = new System.Drawing.Point(108, 17);
            this.btnInsTrue.Margin = new System.Windows.Forms.Padding(2);
            this.btnInsTrue.Name = "btnInsTrue";
            this.btnInsTrue.Size = new System.Drawing.Size(60, 18);
            this.btnInsTrue.TabIndex = 1;
            this.btnInsTrue.Text = "Ins/true";
            this.btnInsTrue.Click += new System.EventHandler(this.btnInsVia_Click);
            // 
            // btnInsFalse
            // 
            this.btnInsFalse.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnInsFalse.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnInsFalse.Location = new System.Drawing.Point(172, 17);
            this.btnInsFalse.Margin = new System.Windows.Forms.Padding(2);
            this.btnInsFalse.Name = "btnInsFalse";
            this.btnInsFalse.Size = new System.Drawing.Size(60, 18);
            this.btnInsFalse.TabIndex = 2;
            this.btnInsFalse.Text = "Ins/false";
            this.btnInsFalse.Click += new System.EventHandler(this.btnInsVia_Click);
            // 
            // btnDelPescado
            // 
            this.btnDelPescado.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnDelPescado.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnDelPescado.Location = new System.Drawing.Point(4, 61);
            this.btnDelPescado.Margin = new System.Windows.Forms.Padding(2);
            this.btnDelPescado.Name = "btnDelPescado";
            this.btnDelPescado.Size = new System.Drawing.Size(100, 18);
            this.btnDelPescado.TabIndex = 4;
            this.btnDelPescado.Text = "Pescado\'s Delete";
            this.btnDelPescado.Click += new System.EventHandler(this.btnDelPescado_Click);
            // 
            // btnLinkInge
            // 
            this.btnLinkInge.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnLinkInge.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnLinkInge.Location = new System.Drawing.Point(4, 39);
            this.btnLinkInge.Margin = new System.Windows.Forms.Padding(2);
            this.btnLinkInge.Name = "btnLinkInge";
            this.btnLinkInge.Size = new System.Drawing.Size(100, 18);
            this.btnLinkInge.TabIndex = 3;
            this.btnLinkInge.Text = "Inge\'s InitLinker";
            this.btnLinkInge.Click += new System.EventHandler(this.btnLinkInge_Click);
            // 
            // btnGUIDIndex
            // 
            this.btnGUIDIndex.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnGUIDIndex.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnGUIDIndex.Location = new System.Drawing.Point(288, 17);
            this.btnGUIDIndex.Margin = new System.Windows.Forms.Padding(2);
            this.btnGUIDIndex.Name = "btnGUIDIndex";
            this.btnGUIDIndex.Size = new System.Drawing.Size(48, 18);
            this.btnGUIDIndex.TabIndex = 6;
            this.btnGUIDIndex.Text = "GUIDs";
            this.btnGUIDIndex.Click += new System.EventHandler(this.btnGUIDIndex_Click);
            // 
            // btnInsUnlinked
            // 
            this.btnInsUnlinked.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnInsUnlinked.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnInsUnlinked.Location = new System.Drawing.Point(108, 39);
            this.btnInsUnlinked.Margin = new System.Windows.Forms.Padding(2);
            this.btnInsUnlinked.Name = "btnInsUnlinked";
            this.btnInsUnlinked.Size = new System.Drawing.Size(124, 18);
            this.btnInsUnlinked.TabIndex = 5;
            this.btnInsUnlinked.Text = "Insert unlinked";
            this.btnInsUnlinked.Click += new System.EventHandler(this.btnInsUnlinked_Click);
            // 
            // btnDelMerola
            // 
            this.btnDelMerola.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnDelMerola.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnDelMerola.Location = new System.Drawing.Point(107, 61);
            this.btnDelMerola.Margin = new System.Windows.Forms.Padding(2);
            this.btnDelMerola.Name = "btnDelMerola";
            this.btnDelMerola.Size = new System.Drawing.Size(89, 18);
            this.btnDelMerola.TabIndex = 5;
            this.btnDelMerola.Text = "Delete to end";
            this.btnDelMerola.Click += new System.EventHandler(this.btnDelMerola_Click);
            // 
            // btnCopyListing
            // 
            this.btnCopyListing.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnCopyListing.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCopyListing.Location = new System.Drawing.Point(4, 17);
            this.btnCopyListing.Margin = new System.Windows.Forms.Padding(2);
            this.btnCopyListing.Name = "btnCopyListing";
            this.btnCopyListing.Size = new System.Drawing.Size(48, 18);
            this.btnCopyListing.TabIndex = 8;
            this.btnCopyListing.Text = "Copy";
            this.btnCopyListing.Click += new System.EventHandler(this.btnCopyListing_Click);
            // 
            // btnTPRPMaker
            // 
            this.btnTPRPMaker.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnTPRPMaker.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnTPRPMaker.Location = new System.Drawing.Point(236, 17);
            this.btnTPRPMaker.Margin = new System.Windows.Forms.Padding(2);
            this.btnTPRPMaker.Name = "btnTPRPMaker";
            this.btnTPRPMaker.Size = new System.Drawing.Size(48, 18);
            this.btnTPRPMaker.TabIndex = 9;
            this.btnTPRPMaker.Text = "Labels";
            this.btnTPRPMaker.Click += new System.EventHandler(this.btnTPRPMaker_Click);
            // 
            // llHidesOP
            // 
            this.llHidesOP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.llHidesOP.AutoSize = true;
            this.llHidesOP.BackColor = System.Drawing.Color.Transparent;
            this.llHidesOP.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.llHidesOP.LinkArea = new System.Windows.Forms.LinkArea(27, 13);
            this.llHidesOP.Location = new System.Drawing.Point(395, 428);
            this.llHidesOP.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.llHidesOP.Name = "llHidesOP";
            this.llHidesOP.Size = new System.Drawing.Size(211, 17);
            this.llHidesOP.TabIndex = 19;
            this.llHidesOP.TabStop = true;
            this.llHidesOP.Text = "BHAV from {0} overridden.  View original.";
            this.llHidesOP.UseCompatibleTextRendering = true;
            this.llHidesOP.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llHidesOP_LinkClicked);
            // 
            // tbHidesOP
            // 
            this.tbHidesOP.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbHidesOP.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbHidesOP.Location = new System.Drawing.Point(395, 465);
            this.tbHidesOP.Margin = new System.Windows.Forms.Padding(2);
            this.tbHidesOP.Multiline = true;
            this.tbHidesOP.Name = "tbHidesOP";
            this.tbHidesOP.ReadOnly = true;
            this.tbHidesOP.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbHidesOP.Size = new System.Drawing.Size(358, 60);
            this.tbHidesOP.TabIndex = 18;
            // 
            // cbSpecial
            // 
            this.cbSpecial.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbSpecial.AutoSize = true;
            this.cbSpecial.BackColor = System.Drawing.Color.Transparent;
            this.cbSpecial.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cbSpecial.Location = new System.Drawing.Point(652, 312);
            this.cbSpecial.Margin = new System.Windows.Forms.Padding(2);
            this.cbSpecial.Name = "cbSpecial";
            this.cbSpecial.Size = new System.Drawing.Size(99, 17);
            this.cbSpecial.TabIndex = 16;
            this.cbSpecial.Text = "Special buttons";
            this.cbSpecial.UseVisualStyleBackColor = false;
            this.cbSpecial.CheckStateChanged += new System.EventHandler(this.cbSpecial_CheckStateChanged);
            // 
            // btnImportBHAV
            // 
            this.btnImportBHAV.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImportBHAV.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnImportBHAV.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnImportBHAV.Location = new System.Drawing.Point(436, 533);
            this.btnImportBHAV.Margin = new System.Windows.Forms.Padding(2);
            this.btnImportBHAV.Name = "btnImportBHAV";
            this.btnImportBHAV.Size = new System.Drawing.Size(112, 21);
            this.btnImportBHAV.TabIndex = 20;
            this.btnImportBHAV.Text = "Import as Private";
            this.btnImportBHAV.Visible = false;
            this.btnImportBHAV.Click += new System.EventHandler(this.btnImportBHAV_Click);
            // 
            // btnCopyBHAV
            // 
            this.btnCopyBHAV.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCopyBHAV.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnCopyBHAV.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCopyBHAV.Location = new System.Drawing.Point(552, 533);
            this.btnCopyBHAV.Margin = new System.Windows.Forms.Padding(2);
            this.btnCopyBHAV.Name = "btnCopyBHAV";
            this.btnCopyBHAV.Size = new System.Drawing.Size(112, 21);
            this.btnCopyBHAV.TabIndex = 20;
            this.btnCopyBHAV.Text = "Import unchanged";
            this.btnCopyBHAV.Visible = false;
            this.btnCopyBHAV.Click += new System.EventHandler(this.btnCopyBHAV_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnClose.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnClose.Location = new System.Drawing.Point(669, 533);
            this.btnClose.Margin = new System.Windows.Forms.Padding(2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(71, 21);
            this.btnClose.TabIndex = 21;
            this.btnClose.Text = "Close";
            this.btnClose.Visible = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // tbHeaderFlag
            // 
            this.tbHeaderFlag.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbHeaderFlag.Location = new System.Drawing.Point(455, 32);
            this.tbHeaderFlag.Margin = new System.Windows.Forms.Padding(2);
            this.tbHeaderFlag.MaxLength = 4;
            this.tbHeaderFlag.Name = "tbHeaderFlag";
            this.tbHeaderFlag.Size = new System.Drawing.Size(32, 20);
            this.tbHeaderFlag.TabIndex = 6;
            this.tbHeaderFlag.Text = "0xDD";
            this.tbHeaderFlag.TextChanged += new System.EventHandler(this.hex8_TextChanged);
            this.tbHeaderFlag.Validated += new System.EventHandler(this.hex8_Validated);
            this.tbHeaderFlag.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
            // 
            // lbHeaderFlag
            // 
            this.lbHeaderFlag.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbHeaderFlag.AutoSize = true;
            this.lbHeaderFlag.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbHeaderFlag.Location = new System.Drawing.Point(381, 34);
            this.lbHeaderFlag.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbHeaderFlag.Name = "lbHeaderFlag";
            this.lbHeaderFlag.Size = new System.Drawing.Size(65, 13);
            this.lbHeaderFlag.TabIndex = 18;
            this.lbHeaderFlag.Text = "Header Flag";
            this.lbHeaderFlag.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // tbCacheFlags
            // 
            this.tbCacheFlags.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbCacheFlags.Location = new System.Drawing.Point(725, 32);
            this.tbCacheFlags.Margin = new System.Windows.Forms.Padding(2);
            this.tbCacheFlags.MaxLength = 4;
            this.tbCacheFlags.Name = "tbCacheFlags";
            this.tbCacheFlags.Size = new System.Drawing.Size(32, 20);
            this.tbCacheFlags.TabIndex = 8;
            this.tbCacheFlags.Text = "0xDD";
            this.tbCacheFlags.TextChanged += new System.EventHandler(this.hex8_TextChanged);
            this.tbCacheFlags.Validated += new System.EventHandler(this.hex8_Validated);
            this.tbCacheFlags.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
            // 
            // cbFormat
            // 
            this.cbFormat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbFormat.ItemHeight = 13;
            this.cbFormat.Items.AddRange(new object[] {
            "0x8000",
            "0x8001",
            "0x8002",
            "0x8003",
            "0x8004",
            "0x8005",
            "0x8006",
            "0x8007",
            "0x8008",
            "0x8009"});
            this.cbFormat.Location = new System.Drawing.Point(207, 32);
            this.cbFormat.Margin = new System.Windows.Forms.Padding(2);
            this.cbFormat.Name = "cbFormat";
            this.cbFormat.Size = new System.Drawing.Size(66, 21);
            this.cbFormat.TabIndex = 4;
            this.cbFormat.Text = "0xDDDD";
            this.cbFormat.Validating += new System.ComponentModel.CancelEventHandler(this.cbHex16_Validating);
            this.cbFormat.SelectedIndexChanged += new System.EventHandler(this.cbHex16_SelectedIndexChanged);
            this.cbFormat.Enter += new System.EventHandler(this.cbHex16_Enter);
            this.cbFormat.Validated += new System.EventHandler(this.cbHex16_Validated);
            this.cbFormat.TextChanged += new System.EventHandler(this.cbHex16_TextChanged);
            // 
            // pnflowcontainer
            // 
            this.pnflowcontainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnflowcontainer.AutoScroll = true;
            this.pnflowcontainer.Location = new System.Drawing.Point(0, 60);
            this.pnflowcontainer.Margin = new System.Windows.Forms.Padding(2);
            this.pnflowcontainer.Name = "pnflowcontainer";
            this.pnflowcontainer.SelectedIndex = -1;
            this.pnflowcontainer.Size = new System.Drawing.Size(391, 503);
            this.pnflowcontainer.TabIndex = 1;
            this.pnflowcontainer.SelectedInstChanged += new System.EventHandler(this.pnflowcontainer_SelectedInstChanged);
            // 
            // btnDel
            // 
            this.btnDel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnDel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnDel.Location = new System.Drawing.Point(570, 310);
            this.btnDel.Margin = new System.Windows.Forms.Padding(2);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(50, 18);
            this.btnDel.TabIndex = 15;
            this.btnDel.Text = "De&lete";
            this.btnDel.Click += new System.EventHandler(this.btnDel_Clicked);
            // 
            // gbMove
            // 
            this.gbMove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gbMove.BackColor = System.Drawing.Color.Transparent;
            this.gbMove.Controls.Add(this.btnUp);
            this.gbMove.Controls.Add(this.btnDown);
            this.gbMove.Controls.Add(this.lbUpDown);
            this.gbMove.Controls.Add(this.tbLines);
            this.gbMove.Location = new System.Drawing.Point(448, 281);
            this.gbMove.Margin = new System.Windows.Forms.Padding(2);
            this.gbMove.Name = "gbMove";
            this.gbMove.Padding = new System.Windows.Forms.Padding(2);
            this.gbMove.Size = new System.Drawing.Size(114, 51);
            this.gbMove.TabIndex = 13;
            this.gbMove.TabStop = false;
            this.gbMove.Text = "&Move";
            // 
            // btnUp
            // 
            this.btnUp.BackColor = System.Drawing.Color.Transparent;
            this.btnUp.Font = new System.Drawing.Font("Wingdings", 8.25F);
            this.btnUp.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnUp.Location = new System.Drawing.Point(8, 14);
            this.btnUp.Margin = new System.Windows.Forms.Padding(2);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(17, 17);
            this.btnUp.TabIndex = 2;
            this.btnUp.Text = "á         &Up";
            this.btnUp.UseVisualStyleBackColor = false;
            this.btnUp.Click += new System.EventHandler(this.btnMove_Clicked);
            // 
            // btnDown
            // 
            this.btnDown.BackColor = System.Drawing.Color.Transparent;
            this.btnDown.Font = new System.Drawing.Font("Wingdings", 8.25F);
            this.btnDown.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnDown.Location = new System.Drawing.Point(8, 31);
            this.btnDown.Margin = new System.Windows.Forms.Padding(2);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(17, 17);
            this.btnDown.TabIndex = 3;
            this.btnDown.Text = "â         &Down";
            this.btnDown.UseVisualStyleBackColor = false;
            this.btnDown.Click += new System.EventHandler(this.btnMove_Clicked);
            // 
            // lbUpDown
            // 
            this.lbUpDown.AutoSize = true;
            this.lbUpDown.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbUpDown.Location = new System.Drawing.Point(80, 23);
            this.lbUpDown.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbUpDown.Name = "lbUpDown";
            this.lbUpDown.Size = new System.Drawing.Size(28, 13);
            this.lbUpDown.TabIndex = 30;
            this.lbUpDown.Text = "lines";
            this.lbUpDown.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // tbLines
            // 
            this.tbLines.Location = new System.Drawing.Point(26, 21);
            this.tbLines.Margin = new System.Windows.Forms.Padding(2);
            this.tbLines.MaxLength = 6;
            this.tbLines.Name = "tbLines";
            this.tbLines.Size = new System.Drawing.Size(54, 20);
            this.tbLines.TabIndex = 1;
            this.tbLines.Text = "0xDDDD";
            this.tbLines.TextChanged += new System.EventHandler(this.hex16_TextChanged);
            this.tbLines.Validated += new System.EventHandler(this.hex16_Validated);
            this.tbLines.Validating += new System.ComponentModel.CancelEventHandler(this.hex16_Validating);
            // 
            // btnSort
            // 
            this.btnSort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSort.BackColor = System.Drawing.Color.Transparent;
            this.btnSort.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnSort.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnSort.Location = new System.Drawing.Point(406, 300);
            this.btnSort.Margin = new System.Windows.Forms.Padding(2);
            this.btnSort.Name = "btnSort";
            this.btnSort.Size = new System.Drawing.Size(38, 22);
            this.btnSort.TabIndex = 12;
            this.btnSort.Text = "&Sort";
            this.btnSort.UseVisualStyleBackColor = false;
            this.btnSort.Click += new System.EventHandler(this.btnSort_Clicked);
            // 
            // btnCommit
            // 
            this.btnCommit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCommit.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnCommit.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCommit.Location = new System.Drawing.Point(395, 60);
            this.btnCommit.Margin = new System.Windows.Forms.Padding(2);
            this.btnCommit.Name = "btnCommit";
            this.btnCommit.Size = new System.Drawing.Size(99, 22);
            this.btnCommit.TabIndex = 11;
            this.btnCommit.Text = "Commit &File";
            this.btnCommit.Click += new System.EventHandler(this.btnCommit_Clicked);
            // 
            // tbTreeVersion
            // 
            this.tbTreeVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbTreeVersion.Location = new System.Drawing.Point(570, 32);
            this.tbTreeVersion.Margin = new System.Windows.Forms.Padding(2);
            this.tbTreeVersion.MaxLength = 10;
            this.tbTreeVersion.Name = "tbTreeVersion";
            this.tbTreeVersion.Size = new System.Drawing.Size(80, 20);
            this.tbTreeVersion.TabIndex = 7;
            this.tbTreeVersion.Text = "0xDDDDDDDD";
            this.tbTreeVersion.TextChanged += new System.EventHandler(this.hex32_TextChanged);
            this.tbTreeVersion.Validated += new System.EventHandler(this.hex32_Validated);
            this.tbTreeVersion.Validating += new System.ComponentModel.CancelEventHandler(this.hex32_Validating);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnAdd.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnAdd.Location = new System.Drawing.Point(570, 288);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(2);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(50, 18);
            this.btnAdd.TabIndex = 14;
            this.btnAdd.Text = "&Add";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Clicked);
            // 
            // lbCacheFlags
            // 
            this.lbCacheFlags.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbCacheFlags.AutoSize = true;
            this.lbCacheFlags.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbCacheFlags.Location = new System.Drawing.Point(656, 34);
            this.lbCacheFlags.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbCacheFlags.Name = "lbCacheFlags";
            this.lbCacheFlags.Size = new System.Drawing.Size(63, 13);
            this.lbCacheFlags.TabIndex = 16;
            this.lbCacheFlags.Text = "Cache flags";
            this.lbCacheFlags.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // cmenuGUIDIndex
            // 
            this.cmenuGUIDIndex.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createAllPackagesToolStripMenuItem,
            this.createCurrentPackageToolStripMenuItem,
            this.loadIndexToolStripMenuItem,
            this.saveIndexToolStripMenuItem});
            this.cmenuGUIDIndex.Name = "cmenuGUIDIndex";
            this.cmenuGUIDIndex.Size = new System.Drawing.Size(205, 92);
            this.cmenuGUIDIndex.Opening += new System.ComponentModel.CancelEventHandler(this.cmenuGUIDIndex_Opening);
            // 
            // createAllPackagesToolStripMenuItem
            // 
            this.createAllPackagesToolStripMenuItem.Name = "createAllPackagesToolStripMenuItem";
            this.createAllPackagesToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.createAllPackagesToolStripMenuItem.Text = "Create - all packages";
            this.createAllPackagesToolStripMenuItem.Click += new System.EventHandler(this.createToolStripMenuItem_Click);
            // 
            // createCurrentPackageToolStripMenuItem
            // 
            this.createCurrentPackageToolStripMenuItem.Name = "createCurrentPackageToolStripMenuItem";
            this.createCurrentPackageToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.createCurrentPackageToolStripMenuItem.Text = "Create - current package";
            this.createCurrentPackageToolStripMenuItem.Click += new System.EventHandler(this.createToolStripMenuItem_Click);
            // 
            // loadIndexToolStripMenuItem
            // 
            this.loadIndexToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.defaultFileToolStripMenuItem,
            this.fromFileToolStripMenuItem});
            this.loadIndexToolStripMenuItem.Name = "loadIndexToolStripMenuItem";
            this.loadIndexToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.loadIndexToolStripMenuItem.Text = "Load index...";
            // 
            // defaultFileToolStripMenuItem
            // 
            this.defaultFileToolStripMenuItem.Name = "defaultFileToolStripMenuItem";
            this.defaultFileToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.defaultFileToolStripMenuItem.Text = "Default file";
            this.defaultFileToolStripMenuItem.Click += new System.EventHandler(this.defaultFileToolStripMenuItem_Click);
            // 
            // fromFileToolStripMenuItem
            // 
            this.fromFileToolStripMenuItem.Name = "fromFileToolStripMenuItem";
            this.fromFileToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.fromFileToolStripMenuItem.Text = "From file...";
            this.fromFileToolStripMenuItem.Click += new System.EventHandler(this.fileToolStripMenuItem_Click);
            // 
            // saveIndexToolStripMenuItem
            // 
            this.saveIndexToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.defaultFileToolStripMenuItem1,
            this.toFileToolStripMenuItem});
            this.saveIndexToolStripMenuItem.Name = "saveIndexToolStripMenuItem";
            this.saveIndexToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.saveIndexToolStripMenuItem.Text = "Save index...";
            // 
            // defaultFileToolStripMenuItem1
            // 
            this.defaultFileToolStripMenuItem1.Name = "defaultFileToolStripMenuItem1";
            this.defaultFileToolStripMenuItem1.Size = new System.Drawing.Size(131, 22);
            this.defaultFileToolStripMenuItem1.Text = "Default file";
            this.defaultFileToolStripMenuItem1.Click += new System.EventHandler(this.defaultFileToolStripMenuItem_Click);
            // 
            // toFileToolStripMenuItem
            // 
            this.toFileToolStripMenuItem.Name = "toFileToolStripMenuItem";
            this.toFileToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.toFileToolStripMenuItem.Text = "To file...";
            this.toFileToolStripMenuItem.Click += new System.EventHandler(this.fileToolStripMenuItem_Click);
            // 
            // ttBhavForm
            // 
            this.ttBhavForm.ShowAlways = true;
            // 
            // BhavForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(760, 564);
            this.Controls.Add(this.bhavPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "BhavForm";
            this.Text = "BhavForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.gbInstruction.ResumeLayout(false);
            this.gbInstruction.PerformLayout();
            this.bhavPanel.ResumeLayout(false);
            this.bhavPanel.PerformLayout();
            this.gbSpecial.ResumeLayout(false);
            this.gbMove.ResumeLayout(false);
            this.gbMove.PerformLayout();
            this.cmenuGUIDIndex.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private void pnflowcontainer_SelectedInstChanged(object sender, System.EventArgs e)
		{
			int index = pnflowcontainer.SelectedIndex;
			if (index < 0 || index >= wrapper.Count)
			{
				currentInst = null;
				origInst = null;
			}
			else
			{
				currentInst = wrapper[index];
				origInst = wrapper[index].Clone();
			}
			UpdateInstPanel();
			this.btnCancel.Enabled = false;
		}


		private void ItemQueryContinueDragTarget(object sender, QueryContinueDragEventArgs e)
		{
			if (e.KeyState==0) e.Action = DragAction.Drop;
			else e.Action = DragAction.Continue;
		}

		private void ItemDragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(typeof(int))) 
			{
				e.Effect = DragDropEffects.Link;		
			}
			else 
			{
				e.Effect = DragDropEffects.None;
			}					
		}

		private void ItemDrop(object sender, System.Windows.Forms.DragEventArgs e)
		{
			int sel = 0;
			sel = (int)e.Data.GetData(sel.GetType());
			ComboBox cb = ((ComboBox)sender);
			cb.SelectedIndex = -1;
			cb.Text = "0x"+Helper.HexString((ushort)sel);
		}


		private void btnCommit_Clicked(object sender, System.EventArgs e)
		{
			try 
			{
				wrapper.SynchronizeUserData();
				btnCommit.Enabled = wrapper.Changed;
				pnflowcontainer_SelectedInstChanged(null, null);
			} 
			catch (Exception ex) 
			{
				Helper.ExceptionMessage(pjse.Localization.GetString("errwritingfile"), ex);
			}			
		}


		private void btnCancel_Clicked(object sender, System.EventArgs e)
		{
			wrapper[pnflowcontainer.SelectedIndex] = origInst.Clone();
			pnflowcontainer_SelectedInstChanged(null, null);
		}


        private void pjse_banner1_SiblingClick(object sender, EventArgs e)
        {
            TPRP tprp = (TPRP)wrapper.SiblingResource(TPRP.TPRPtype);
            if (tprp == null) return;
            if (tprp.Package != wrapper.Package)
            {
                DialogResult dr = MessageBox.Show(Localization.GetString("OpenOtherPkg"), pjse_banner1.TitleText, MessageBoxButtons.YesNo);
                if (dr != DialogResult.Yes) return;
            }
            SimPe.RemoteControl.OpenPackedFile(tprp.FileDescriptor, tprp.Package);
        }


        private void pjse_banner1_TreeClick(object sender, EventArgs e) // Fuck
        {
            SimPe.Plugin.TreesPackedFileWrapper tpfw = (SimPe.Plugin.TreesPackedFileWrapper)wrapper.SiblingResource(0x54524545);
            if (tpfw == null) return;

            if (tpfw.Package != wrapper.Package)
            {
                DialogResult dr = MessageBox.Show(Localization.GetString("OpenOtherPkg"), pjse_banner1.TitleText, MessageBoxButtons.YesNo);
                if (dr != DialogResult.Yes) return;
            }
            SimPe.RemoteControl.OpenPackedFile(tpfw.FileDescriptor, tpfw.Package);
        }

        private void btnFloat_Click(object sender, EventArgs e)
        {
            Control old = this.bhavPanel.Parent;
            string oldFloatText = this.pjse_banner1.FloatText;

            Form f = new Form();
            f.Text = formTitle;
            f.WindowState = FormWindowState.Maximized;

            f.Controls.Add(this.bhavPanel);
            this.pjse_banner1.FloatText = pjse.Localization.GetString("bhavForm.Unfloat");
            this.pjse_banner1.FloatClick -= new System.EventHandler(this.btnFloat_Click);
            this.pjse_banner1.SetFormCancelButton(f);

            this.gbSpecial.Visible = true;
            this.cbSpecial.Enabled = false;
            this.btnCopyBHAV.Visible = false;

            handleOverride();

            f.ShowDialog();

            old.Controls.Add(this.bhavPanel);
            this.pjse_banner1.FloatText = oldFloatText;
            this.pjse_banner1.FloatClick += new System.EventHandler(this.btnFloat_Click);

            this.gbSpecial.Visible = this.cbSpecial.Checked;
            this.cbSpecial.Enabled = true;

            this.lbHidesOP.Visible = this.tbHidesOP.Visible = this.llHidesOP.Visible = false;
            this.llHidesOP.Tag = null;

            f.Dispose();

            wrapper.RefreshUI();
        }

        private void pjse_banner1_ViewClick(object sender, EventArgs e)
        {
            common_LinkClicked(pjse.FileTable.GFT[wrapper.Package, wrapper.FileDescriptor][0]);
        }

		private void llopenbhav_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
            common_LinkClicked(wrapper.ResourceByInstance(SimPe.Data.MetaData.BHAV_FILE, currentInst.Instruction.OpCode));
		}

        private void llHidesOP_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            common_LinkClicked(wrapper.ResourceByInstance(SimPe.Data.MetaData.BHAV_FILE, wrapper.FileDescriptor.Instance, (pjse.FileTable.Source)llHidesOP.Tag));
        }

		private void btnClose_Click(object sender, System.EventArgs e)
		{
            if (this.isPopup)
                Close();
		}

        private void btnCopyBHAV_Click(object sender, EventArgs e)
        {
            btnCopyBHAV.Enabled = false;
            TakeACopy();
            btnCopyBHAV.Text = pjse.Localization.GetString("ml_done");
        }

        private void btnImportBHAV_Click(object sender, EventArgs e)
        {
            btnImportBHAV.Enabled = false;
            ImportBHAV();
            btnImportBHAV.Text = pjse.Localization.GetString("ml_done");
        }


        private void pjse_banner1_ExtractClick(object sender, EventArgs e) { pjse.ExtractCurrent.Execute(wrapper, pjse_banner1.TitleText); }


		private void btnOpCode_Clicked(object sender, System.EventArgs e)
		{
            pjse.FileTable.Entry item = new ResourceChooser().Execute(SimPe.Data.MetaData.BHAV_FILE, wrapper.FileDescriptor.Group, bhavPanel.Parent, false);

			if (item != null && item.Instance != currentInst.Instruction.OpCode)
				this.tbInst_OpCode.Text = "0x" + SimPe.Helper.HexString((ushort)item.Instance);
		}

        private void btnOperandWiz_Clicked(object sender, System.EventArgs e) { OperandWiz(1); }
		
		private void btnOperandRaw_Click(object sender, System.EventArgs e) { OperandWiz(0); }

        private void btnZero_Click(object sender, EventArgs e)
        {
            internalchg = true;
            Instruction inst = currentInst.Instruction;
            currentInst = null;
            try
            {
                for (int i = 0; i < 8; i++) inst.Operands[i] = 0;
                for (int i = 0; i < 8; i++) inst.Reserved1[i] = 0;
            }
            finally
            {
                currentInst = inst;
                UpdateInstPanel();
                this.btnCancel.Enabled = true;
                internalchg = false;
            }
        }


        private void tbFilename_TextChanged(object sender, System.EventArgs e)
		{
			wrapper.FileName = tbFilename.Text;
		}

		private void tbFilename_Validated(object sender, System.EventArgs e)
		{
			tbFilename.SelectAll();
		}


		private void cbHex16_Enter(object sender, System.EventArgs e)
		{
			((ComboBox)sender).SelectAll();
		}

		private void cbHex16_TextChanged(object sender, System.EventArgs ev)
		{
			if (internalchg) return;
			if (!cbHex16_IsValid(sender)) return;
			if (((ComboBox)sender).Items.IndexOf(((ComboBox)sender).Text) != -1) return;

			ushort val = Convert.ToUInt16(((ComboBox)sender).Text, 16);
			internalchg = true;
			switch (alHex16cb.IndexOf(sender))
			{
				case 0: currentInst.Instruction.Target1 = val; break;
				case 1: currentInst.Instruction.Target2 = val; break;
				case 2: wrapper.Header.Format = val; break;
			}
			internalchg = false;
		}

		private void cbHex16_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (cbHex16_IsValid(sender)) return;

			int i = alHex16cb.IndexOf(sender);
			if (i < 0)
				throw new Exception("cbHex16_Validating not applicable to control " + sender.ToString());

			e.Cancel = true;

			ushort val = 0;
			switch (i)
			{
				case 0: val = origInst.Target1; break;
				case 1: val = origInst.Target2; break;
				case 2: val = wrapper.Header.Format; break;
			}

			if (i < 2 && val >= 0xfffc && val <= 0xfffe)
			{
				((ComboBox)sender).SelectedIndex = val - 0xfffc;
			}
			else if (i == 2 && val >= 0x8000 && val <= 0x8007)
			{
				((ComboBox)sender).SelectedIndex = val - 0x8000;
			}
			else
			{
				((ComboBox)sender).SelectedIndex = -1;
				((ComboBox)sender).Text = "0x" + Helper.HexString(val);
			}
			((ComboBox)sender).SelectAll();
		}

		private void cbHex16_Validated(object sender, System.EventArgs e)
		{
			int i = alHex16cb.IndexOf(sender);
			if (i < 0)
				throw new Exception("cbHex16_Validated not applicable to control " + sender.ToString());
			if (((ComboBox)sender).Items.IndexOf(((ComboBox)sender).Text) != -1) return;

			ushort val = Convert.ToUInt16(((ComboBox)sender).Text, 16);

			bool origstate = internalchg;
			internalchg = true;
			if (i < 2 && val >= 0xfffc && val <= 0xfffe)
			{
				((ComboBox)sender).SelectedIndex = val - 0xfffc;
			}
			else if (i == 2 && val >= 0x8000 && val <= 0x8007)
			{
				((ComboBox)sender).SelectedIndex = val - 0x8000;
			}
			else
			{
				((ComboBox)sender).SelectedIndex = -1;
				((ComboBox)sender).Text = "0x" + Helper.HexString(val);
			}
			internalchg = origstate;
			((ComboBox)sender).Select(0, 0);
		}

		private void cbHex16_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (internalchg) return;

			int i = alHex16cb.IndexOf(sender);
			if (i < 0)
				throw new Exception("cbHex16_SelectedIndexChanged not applicable to control " + sender.ToString());
			if (((ComboBox)sender).SelectedIndex == -1) return;

			ushort val = (ushort)((ComboBox)alHex16cb[i]).SelectedIndex;
			((ComboBox)sender).SelectAll();

			internalchg = true;
			if (i < 2)
			{
				val += 0xFFFC;
				if (i == 0) currentInst.Instruction.Target1 = val;
				else        currentInst.Instruction.Target2 = val;
			}
			else
			{
				val += 0x8000;
				wrapper.Header.Format = val;
			}
			internalchg = false;
		}

		private void dec8_TextChanged(object sender, System.EventArgs ev)
		{
			if (internalchg) return;
			if (!dec8_IsValid(sender)) return;

			byte val = Convert.ToByte(((TextBox)sender).Text);
			internalchg = true;
			switch (alDec8.IndexOf(sender))
			{
				default: break;
			}
			internalchg = false;
		}

		private void dec8_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (dec8_IsValid(sender)) return;

			e.Cancel = true;

			byte val = 0;
			switch (alDec8.IndexOf(sender))
			{
				default: break;
			}

			((TextBox)sender).Text = val.ToString();
			((TextBox)sender).SelectAll();
        }

        private void hex8_TextChanged(object sender, System.EventArgs ev)
		{
			if (internalchg) return;
			if (!hex8_IsValid(sender)) return;

			byte val = Convert.ToByte(((TextBox)sender).Text, 16);
			int i = alHex8.IndexOf(sender);

			internalchg = true;

            byte oldval = val;
            if (i < 8) { oldval = currentInst.Instruction.Operands[i]; currentInst.Instruction.Operands[i] = val; ChangeLongname(oldval, val); }
            else if (i < 16) { oldval = currentInst.Instruction.Reserved1[i - 8]; currentInst.Instruction.Reserved1[i - 8] = val; ChangeLongname(oldval, val); }
            else
                switch (i)
                {
                    case 16: oldval = currentInst.Instruction.NodeVersion; currentInst.Instruction.NodeVersion = val; ChangeLongname(oldval, val); break;
                    case 17: wrapper.Header.HeaderFlag = val; break;
                    case 18: wrapper.Header.Type = val; break;
                    case 19: wrapper.Header.CacheFlags = val; break;
                    case 20: oldval = wrapper.Header.ArgumentCount; wrapper.Header.ArgumentCount = val; ChangeLongname(oldval, val); break;
                    case 21: wrapper.Header.LocalVarCount = val; break;
                }

			internalchg = false;
		}

		private void hex8_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (hex8_IsValid(sender)) return;

			e.Cancel = true;

			byte val = 0;
			int i = alHex8.IndexOf(sender);

			if (i < 8) val = origInst.Operands[i];
			else if (i < 16) val = origInst.Reserved1[i-8];
			else switch(i)
				 {
					 case 16: val = origInst.NodeVersion; break;
					 case 17: val = wrapper.Header.HeaderFlag; break;
					 case 18: val = wrapper.Header.Type; break;
					 case 19: val = wrapper.Header.CacheFlags; break;
					 case 20: val = wrapper.Header.ArgumentCount; break;
					 case 21: val = wrapper.Header.LocalVarCount; break;
				 }

			((TextBox)sender).Text = ((i >= 16) ? "0x" : "") + Helper.HexString(val);
			((TextBox)sender).SelectAll();
		}

		private void hex8_Validated(object sender, System.EventArgs e)
		{
			bool origstate = internalchg;
			internalchg = true;
			((TextBox)sender).Text = ((alHex8.IndexOf(sender) >= 16) ? "0x" : "") + Helper.HexString(Convert.ToByte(((TextBox)sender).Text, 16));
			((TextBox)sender).SelectAll();
			internalchg = origstate;
		}


		private void hex16_TextChanged(object sender, System.EventArgs ev)
		{
			if (internalchg) return;
			if (!hex16_IsValid(sender)) return;

			ushort val = Convert.ToUInt16(((TextBox)sender).Text, 16);
			internalchg = true;
			switch (alHex16.IndexOf(sender))
			{
                case 0: OpcodeChanged(val); break;
			}
			internalchg = false;
		}

		private void hex16_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (hex16_IsValid(sender)) return;

			e.Cancel = true;

			ushort val = 0;
			switch (alHex16.IndexOf(sender))
			{
				case 0: val = origInst.OpCode; break;
                case 1: val = 1; break;
			}

			((TextBox)sender).Text = "0x" + Helper.HexString(val);
			((TextBox)sender).SelectAll();
		}

		private void hex16_Validated(object sender, System.EventArgs e)
		{
			bool origstate = internalchg;
			internalchg = true;
			((TextBox)sender).Text = "0x" + Helper.HexString(Convert.ToUInt16(((TextBox)sender).Text, 16));
			((TextBox)sender).SelectAll();
			internalchg = origstate;
		}


		private void hex32_TextChanged(object sender, System.EventArgs ev)
		{
			if (internalchg) return;
			if (!hex32_IsValid(sender)) return;

			uint val = Convert.ToUInt32(((TextBox)sender).Text, 16);
			internalchg = true;
			switch (alHex32.IndexOf(sender))
			{
				case 0: wrapper.Header.TreeVersion = val; break;
			}
			internalchg = false;
		}

		private void hex32_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (hex32_IsValid(sender)) return;

			e.Cancel = true;

			uint val = 0;
			switch (alHex32.IndexOf(sender))
			{
				case 0: val = wrapper.Header.TreeVersion; break;
			}

			((TextBox)sender).Text = "0x" + Helper.HexString(val);
			((TextBox)sender).SelectAll();
		}

		private void hex32_Validated(object sender, System.EventArgs e)
		{
			bool origstate = internalchg;
			internalchg = true;
			((TextBox)sender).Text = "0x" + Helper.HexString(Convert.ToUInt32(((TextBox)sender).Text, 16));
			((TextBox)sender).SelectAll();
			internalchg = origstate;
		}


		private void btnSort_Clicked(object sender, System.EventArgs e)
		{
            this.pnflowcontainer.Sort();
            SetComments();
		}

		private void btnMove_Clicked(object sender, System.EventArgs e)
		{
			int mv;
			try { mv = Convert.ToInt32(tbLines.Text, 16); }
			catch (Exception) { return; }
            try
            {
                this.gbMove.Enabled = false;
                if (sender == this.btnUp)
                    this.pnflowcontainer.MoveInst(mv * -1);
                else
                    this.pnflowcontainer.MoveInst(mv);
                SetComments();
            }
            finally
            {
                this.gbMove.Enabled = true;
            }
        }

		private void btnAdd_Clicked(object sender, EventArgs e)
		{
            this.pnflowcontainer.Add(BhavUIAddType.Default);
            SetComments();
		}

		private void btnDel_Clicked(object sender, EventArgs e)
		{
            this.pnflowcontainer.Delete(BhavUIDeleteType.Default);
            SetComments();
		}


		private void cbSpecial_CheckStateChanged(object sender, EventArgs e)
		{
			gbSpecial.Visible =
                pjse.Settings.PJSE.ShowSpecialButtons = ((CheckBox)sender).Checked;
		}


		private void btnInsVia_Click(object sender, EventArgs e)
		{
			this.pnflowcontainer.Add( (sender == this.btnInsTrue) ? BhavUIAddType.ViaTrue : BhavUIAddType.ViaFalse );
		}

		private void btnDelPescado_Click(object sender, EventArgs e)
		{
			this.pnflowcontainer.Delete(BhavUIDeleteType.Pescado);
		}

		private void btnLinkInge_Click(object sender, EventArgs e)
		{
			this.pnflowcontainer.Relink();
		}

		private void btnAppend_Click(object sender, EventArgs e)
		{
            this.pnflowcontainer.Append(new ResourceChooser().Execute(SimPe.Data.MetaData.BHAV_FILE, wrapper.FileDescriptor.Group, bhavPanel.Parent, true, 0x10));
		}

		private void btnDelMerola_Click(object sender, EventArgs e)
		{
			this.pnflowcontainer.DeleteUnlinked();
		}

		private void btnCopyListing_Click(object sender, EventArgs e)
		{
			this.CopyListing();
		}

        private void btnPasteListing_Click(object sender, EventArgs e)
        {
            this.PasteListing();
        }

		private void btnTPRPMaker_Click(object sender, EventArgs e)
		{
			this.TPRPMaker();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.TPFWMaker();
        }

        private void btnInsUnlinked_Click(object sender, EventArgs e)
        {
            this.pnflowcontainer.Add(BhavUIAddType.Unlinked);
        }


        private void btnGUIDIndex_Click(object sender, EventArgs e)
        {
            this.cmenuGUIDIndex.Show((Control)sender, new Point(3 ,3));
        }

        private void cmenuGUIDIndex_Opening(object sender, CancelEventArgs e)
        {
            createCurrentPackageToolStripMenuItem.Enabled =
                (pjse.FileTable.GFT.CurrentPackage != null
                && pjse.FileTable.GFT.CurrentPackage.FileName != null
                && !pjse.FileTable.GFT.CurrentPackage.FileName.ToLower().EndsWith("objects.package"));
        }

        private void createToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.UseWaitCursor = true;
            Application.DoEvents();
            pjse.GUIDIndex.TheGUIDIndex.Create(sender.Equals(this.createCurrentPackageToolStripMenuItem));
            Application.UseWaitCursor = false;
            Application.DoEvents();
            
            // DialogResult dr = pjseMsgBox.Show(RemoteControl.ApplicationForm, pjse.Localization.GetString("guidAskMessage"), pjse.Localization.GetString("guidAskTitle"),
            DialogResult dr = pjseMsgBox.Show(RemoteControl.ApplicationForm, "Do you want to save the GUID Index now? \r\n [Default] - save in the default location \r\n [Specify...] - let me specify where to save \r\n [No] - don't save, just let me get back to SimPe", pjse.Localization.GetString("guidAskTitle"),
                new Boolset("111"), new Boolset("111"), new string[] {
                    pjse.Localization.GetString("guidAskDefault"),
                    pjse.Localization.GetString("guidAskSpecify"),
                    pjse.Localization.GetString("guidAskNoSave"),
                },
                new DialogResult[] { DialogResult.OK, DialogResult.Retry, DialogResult.Cancel, });
            //DialogResult dr = MessageBox.Show(pjse.Localization.GetString("guidAskMessage"), pjse.Localization.GetString("guidAskTitle"), "\r\n" + 
            //    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (dr == DialogResult.OK) defaultFileToolStripMenuItem_Click(this.defaultFileToolStripMenuItem1, null);
            else if (dr == DialogResult.Retry) fileToolStripMenuItem_Click(this.toFileToolStripMenuItem, null);
        }

        private void defaultFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sender.Equals(this.defaultFileToolStripMenuItem))
                pjse.GUIDIndex.TheGUIDIndex.Load();
            else
                pjse.GUIDIndex.TheGUIDIndex.Save();
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool load = sender.Equals(this.fromFileToolStripMenuItem);
            FileDialog fd;
            if (load)
                fd = new OpenFileDialog();
            else
                fd = new SaveFileDialog();
            fd.AddExtension = true;
            fd.CheckFileExists = load;
            fd.CheckPathExists = true;
            fd.DefaultExt = "txt";
            fd.DereferenceLinks = true;
            //fd.FileName = pjse.GUIDIndex.DefaultGUIDFile;
            fd.FileName = "guidindex.txt";
            fd.Filter = pjse.Localization.GetString("guidFilter");
            fd.FilterIndex = 1;
            fd.RestoreDirectory = false;
            fd.ShowHelp = false;
            // fd.SupportMultiDottedExtensions = false; // Methods missing from Mono
            fd.Title = load
                ? pjse.Localization.GetString("guidLoadIndex")
                : pjse.Localization.GetString("guidSaveIndex");
            fd.ValidateNames = true;
            DialogResult dr = fd.ShowDialog();
            if (dr == DialogResult.OK)
            {
                if (load)
                    pjse.GUIDIndex.TheGUIDIndex.Load(fd.FileName);
                else
                    pjse.GUIDIndex.TheGUIDIndex.Save(fd.FileName);
            }
        }
	}
}
