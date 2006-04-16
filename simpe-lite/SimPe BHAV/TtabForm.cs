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
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using SimPe.Interfaces.Plugin;
using SimPe.PackedFiles.Wrapper;

namespace SimPe.PackedFiles.UserInterface
{
	/// <summary>
	/// Summary description for BconForm.
	/// </summary>
	public class TtabForm : System.Windows.Forms.Form, IPackedFileUI
	{
		#region Form variables

		private System.Windows.Forms.Panel panel5;
		private System.Windows.Forms.Label label25;
		private System.Windows.Forms.Panel ttabPanel;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tpSettings;
		private System.Windows.Forms.Label lbaction;
		private System.Windows.Forms.Label lbguard;
		private System.Windows.Forms.Label label40;
		private System.Windows.Forms.Label label33;
		private System.Windows.Forms.Label label34;
		private System.Windows.Forms.Label label35;
		private System.Windows.Forms.Label label29;
		private System.Windows.Forms.Label label30;
		private System.Windows.Forms.Label label31;
		private System.Windows.Forms.Label label32;
		private System.Windows.Forms.Label label20;
		private System.Windows.Forms.TextBox tbGuardian;
		private System.Windows.Forms.CheckBox cbunk3;
		private System.Windows.Forms.CheckBox cbunk4;
		private System.Windows.Forms.CheckBox cbunk1;
		private System.Windows.Forms.CheckBox cbunk2;
		private System.Windows.Forms.CheckBox cbteens;
		private System.Windows.Forms.CheckBox cbelders;
		private System.Windows.Forms.CheckBox cbtodlers;
		private System.Windows.Forms.CheckBox cbautofirst;
		private System.Windows.Forms.CheckBox cbdebugmenu;
		private System.Windows.Forms.CheckBox cbadults;
		private System.Windows.Forms.CheckBox cbdemochild;
		private System.Windows.Forms.CheckBox cbchildren;
		private System.Windows.Forms.CheckBox cbconsecutive;
		private System.Windows.Forms.CheckBox cbimmediately;
		private System.Windows.Forms.CheckBox cbjoinable;
		private System.Windows.Forms.TabPage tpMotives;
		private System.Windows.Forms.CheckBox cbvisitor;
		private System.Windows.Forms.Label label24;
		private System.Windows.Forms.TextBox tbAction;
		private System.Windows.Forms.TextBox tbFlags2;
		private System.Windows.Forms.TextBox tbStringIndex;
		private System.Windows.Forms.GroupBox gbFlags;
		private System.Windows.Forms.TextBox tbFlags;
		private System.Windows.Forms.TextBox tbAttenuationValue;
		private System.Windows.Forms.TextBox tbAutonomy;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox tbJoinIndex;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btnGuardian;
		private System.Windows.Forms.Button btnAction;
		private SimPe.PackedFiles.UserInterface.TtabItemMotiveTableUI ttabItemMotiveTableUI1;
		private System.Windows.Forms.ComboBox cbAttenuationCode;
		private System.Windows.Forms.ListBox lbttab;
		private System.Windows.Forms.Button btnAdd;
		private System.Windows.Forms.Label label26;
		private System.Windows.Forms.Button btnDelete;
		private System.Windows.Forms.Label lbFilename;
		private System.Windows.Forms.TextBox tbFilename;
		private System.Windows.Forms.TextBox tbFormat;
		private System.Windows.Forms.Label label41;
		private System.Windows.Forms.Button btnCommit;
		private System.Windows.Forms.Button btnAppend;
		private System.Windows.Forms.TextBox tbUIDispType;
		private System.Windows.Forms.TextBox tbFaceAnimID;
		private System.Windows.Forms.TextBox tbMemIterMult;
		private System.Windows.Forms.TextBox tbObjType;
		private System.Windows.Forms.TextBox tbModelTabID;
		private System.Windows.Forms.ComboBox cbStringIndex;
		private System.Windows.Forms.LinkLabel llAction;
		private System.Windows.Forms.LinkLabel llGuardian;
		private System.Windows.Forms.Button btnNoFlags;
		private System.Windows.Forms.Button btnHelp;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion
       
		public TtabForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			TextBox[] tbua = { tbAction, tbGuardian, tbFlags, tbFlags2, tbUIDispType };
			alHex16 = new ArrayList(tbua);

			TextBox[] tbia = { tbFormat, tbStringIndex, tbAutonomy, tbFaceAnimID, tbObjType, tbModelTabID, tbJoinIndex };
			alHex32 = new ArrayList(tbia);

			TextBox[] tbfa = { tbAttenuationValue, tbMemIterMult };
			alFloats = new ArrayList(tbfa);

			CheckBox[] cba = {
							    cbvisitor   ,cbjoinable  ,cbimmediately ,cbconsecutive
							   ,cbchildren  ,cbdemochild ,cbadults      ,cbdebugmenu
							   ,cbautofirst ,cbtodlers   ,cbelders      ,cbteens
							   ,cbunk1      ,cbunk2      ,cbunk3        ,cbunk4
						   };
			alFlags = new ArrayList(cba);

			ComboBox[] cbb = { cbStringIndex ,cbAttenuationCode };
			alHex32cb = new ArrayList(cbb);

#if !(INPROGRESS || DEBUG)
			this.btnAppend.Visible = false;
#endif
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

		
		#region TtabForm
		/// <summary>
		/// Stores the currently active Wrapper
		/// </summary>
		private Ttab wrapper = null;
		private bool internalchg;
		private bool setHandler = false;
		private ArrayList alHex16;
		private ArrayList alHex32;
		private ArrayList alFloats;
		private ArrayList alFlags;
		private ArrayList alHex32cb;
		private TtabItem origItem;
		private TtabItem currentItem;

		private void doFlags()
		{
			internalchg = true;
			bool val;
			for (int i = 0; i < alFlags.Count; i++)
			{
				switch(i)
				{
					case  0: val = currentItem.Flags.ByVisitors; break;
					case  1: val = currentItem.Flags.Joinable; break;
					case  2: val = currentItem.Flags.RunImmediately; break;
					case  3: val = currentItem.Flags.AvailConsecutive; break;
					case  4: val = currentItem.Flags.ByChildren; break;
					case  5: val = currentItem.Flags.ByDemoChild; break;
					case  6: val = currentItem.Flags.ByAdults; break;
					case  7: val = currentItem.Flags.DebugMenu; break;
					case  8: val = currentItem.Flags.AutoFirstSelect; break;
					case  9: val = currentItem.Flags.ByToddlers; break;
					case 10: val = currentItem.Flags.ByElders; break;
					case 11: val = currentItem.Flags.ByTeens; break;
					case 12: val = currentItem.Flags.Unknown1; break;
					case 13: val = currentItem.Flags.Unknown2; break;
					case 14: val = currentItem.Flags.Unknown3; break;
					case 15: val = currentItem.Flags.Unknown4; break;
					default: val = false; break;
				}
				((CheckBox)alFlags[i]).Checked = val;
			}
			internalchg = false;
		}
		private bool cbHex32_IsValid(object sender)
		{
			if (alHex32cb.IndexOf(sender) < 0)
				throw new Exception("cbHex32_IsValid not applicable to control " + sender.ToString());
			if (((ComboBox)sender).FindStringExact(((ComboBox)sender).Text) >= 0) return true;

			try { Convert.ToUInt32(((ComboBox)sender).Text, 16); }
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

		private bool float_IsValid(object sender)
		{
			if (alFloats.IndexOf(sender) < 0)
				throw new Exception("float_IsValid not applicable to control " + sender.ToString());
			try { Convert.ToSingle(((TextBox)sender).Text); }
			catch (Exception) { return false; }
			return true;
		}


		public void Append(pjse.FileTable.Entry e)
		{
			if (e == null || !(e.Wrapper is Ttab)) return;

			bool savedstate = internalchg;
			internalchg = true;

			ttabPanel.Parent.Cursor = Cursors.WaitCursor;

			Ttab b = (Ttab)e.Wrapper;
			uint offset = getTTAsCount();
			for (int bi = 0; bi < b.Count; bi++)
			{
				int i = wrapper.Add(b[bi]);
				if (i < 0) break;
				wrapper[i].StringIndex += offset;
				lbttab.Items.Add(wrapper[i]);
			}
			ttabPanel.Parent.Cursor = Cursors.Default;

			internalchg = savedstate;
		}

		private uint getTTAsCount()
		{
			if (wrapper.StringResource == null) return 0;

			int max = 0;
			for (byte lid = 1; lid < 44; lid++) max = Math.Max(max, wrapper.StringResource[lid].Length);
			return (uint)max;
		}


		/// <summary>
		/// Gets the BHAV name and existence
		/// </summary>
		/// <param name="target">BHAV to find</param>
		/// <param name="found">whether it was found</param>
		/// <returns>the filename</returns>
		private string getBHAV(ushort target, ref bool found)
		{
			if (target == 0) return "---";
			string s = "0x" + SimPe.Helper.HexString(target);
			pjse.FileTable.Entry ftEntry = wrapper.ResourceByInstance(SimPe.Data.MetaData.BHAV_FILE, target);
			found = (ftEntry != null);
			return s + ": " + (ftEntry != null ? ftEntry : pjse.BhavWiz.readStr(pjse.GS.BhavStr.Primitives, target));
		}

		private void setBHAV(int which, ushort target, bool notxt)
		{
			TextBox[] tbaGA = { tbAction, tbGuardian };
			if (!notxt) tbaGA[which].Text = "0x"+Helper.HexString(target);

			Label[] lbaGA = { lbaction, lbguard };
			LinkLabel[] llaGA = { llAction, llGuardian };
			bool found = false;
			lbaGA[which].Text = getBHAV(target, ref found);
			llaGA[which].Enabled = found;
		}

		private void setStringIndex(uint si, bool doText, bool doCB)
		{
			if (doText) tbStringIndex.Text = "0x"+Helper.HexString(si);
			if (doCB)
			{
				if (wrapper.StringResource[1, (int)si] != null)
					this.cbStringIndex.SelectedIndex = (int)si;
					//this.cbStringIndex.SelectedValue = tbStringIndex.Text;
				else
				{
					this.cbStringIndex.SelectedIndex = -1;
					this.cbStringIndex.Text = tbStringIndex.Text;
				}
			}
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
				return ttabPanel;
			}
		}

		/// <summary>
		/// Called by the AbstractWrapper when the file should be displayed to the user.
		/// </summary>
		/// <param name="wrp">Reference to the wrapper to be displayed.</param>
		public void UpdateGUI(IFileWrapper wrp)
		{
			wrapper = (Ttab) wrp;
			WrapperChanged(wrapper, null);

			internalchg = true;

			lbttab.Items.Clear();
			for(int i = 0; i < wrapper.Count; i++)
				lbttab.Items.Add(wrapper[i]);

			this.cbStringIndex.Items.Clear();
			int c = (int)getTTAsCount();
			for (int i = 0; i < c; i++)
			{
				StrItem si = wrapper.StringResource[(byte)1, i];
				this.cbStringIndex.Items.Add("0x" + i.ToString("X") + ": " + ((si == null) ? "*!no default string!*" : si.Title));
			}
			this.cbStringIndex.SelectedIndex = -1;

			internalchg = false;

			if (lbttab.Items.Count>0) lbttab.SelectedIndex = 0;
			else TtabSelect(null, null);

			if (!setHandler)
			{
				wrapper.WrapperChanged += new System.EventHandler(this.WrapperChanged);
				setHandler = true;
			}
		}		

		private void WrapperChanged(object sender, System.EventArgs e)
		{
			this.btnCommit.Enabled = wrapper.Changed;

			if (internalchg) return;
			internalchg = true;
			this.Text = tbFilename.Text = wrapper.FileName;
			tbFormat.Text = "0x"+Helper.HexString(wrapper.Format);
			internalchg = false;
		}

		#endregion

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(TtabForm));
			this.ttabPanel = new System.Windows.Forms.Panel();
			this.btnAppend = new System.Windows.Forms.Button();
			this.lbFilename = new System.Windows.Forms.Label();
			this.tbFilename = new System.Windows.Forms.TextBox();
			this.tbFormat = new System.Windows.Forms.TextBox();
			this.label41 = new System.Windows.Forms.Label();
			this.btnCommit = new System.Windows.Forms.Button();
			this.btnAdd = new System.Windows.Forms.Button();
			this.label26 = new System.Windows.Forms.Label();
			this.btnDelete = new System.Windows.Forms.Button();
			this.lbttab = new System.Windows.Forms.ListBox();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tpSettings = new System.Windows.Forms.TabPage();
			this.llGuardian = new System.Windows.Forms.LinkLabel();
			this.llAction = new System.Windows.Forms.LinkLabel();
			this.cbStringIndex = new System.Windows.Forms.ComboBox();
			this.cbAttenuationCode = new System.Windows.Forms.ComboBox();
			this.btnAction = new System.Windows.Forms.Button();
			this.btnGuardian = new System.Windows.Forms.Button();
			this.lbaction = new System.Windows.Forms.Label();
			this.lbguard = new System.Windows.Forms.Label();
			this.tbStringIndex = new System.Windows.Forms.TextBox();
			this.label40 = new System.Windows.Forms.Label();
			this.tbModelTabID = new System.Windows.Forms.TextBox();
			this.label33 = new System.Windows.Forms.Label();
			this.tbObjType = new System.Windows.Forms.TextBox();
			this.label34 = new System.Windows.Forms.Label();
			this.tbUIDispType = new System.Windows.Forms.TextBox();
			this.label35 = new System.Windows.Forms.Label();
			this.tbAutonomy = new System.Windows.Forms.TextBox();
			this.tbMemIterMult = new System.Windows.Forms.TextBox();
			this.label29 = new System.Windows.Forms.Label();
			this.tbFaceAnimID = new System.Windows.Forms.TextBox();
			this.label30 = new System.Windows.Forms.Label();
			this.tbAttenuationValue = new System.Windows.Forms.TextBox();
			this.label31 = new System.Windows.Forms.Label();
			this.label32 = new System.Windows.Forms.Label();
			this.tbFlags2 = new System.Windows.Forms.TextBox();
			this.label20 = new System.Windows.Forms.Label();
			this.tbGuardian = new System.Windows.Forms.TextBox();
			this.gbFlags = new System.Windows.Forms.GroupBox();
			this.btnNoFlags = new System.Windows.Forms.Button();
			this.tbFlags = new System.Windows.Forms.TextBox();
			this.label24 = new System.Windows.Forms.Label();
			this.cbvisitor = new System.Windows.Forms.CheckBox();
			this.cbunk3 = new System.Windows.Forms.CheckBox();
			this.cbunk4 = new System.Windows.Forms.CheckBox();
			this.cbunk1 = new System.Windows.Forms.CheckBox();
			this.cbunk2 = new System.Windows.Forms.CheckBox();
			this.cbteens = new System.Windows.Forms.CheckBox();
			this.cbelders = new System.Windows.Forms.CheckBox();
			this.cbtodlers = new System.Windows.Forms.CheckBox();
			this.cbautofirst = new System.Windows.Forms.CheckBox();
			this.cbdebugmenu = new System.Windows.Forms.CheckBox();
			this.cbadults = new System.Windows.Forms.CheckBox();
			this.cbdemochild = new System.Windows.Forms.CheckBox();
			this.cbchildren = new System.Windows.Forms.CheckBox();
			this.cbconsecutive = new System.Windows.Forms.CheckBox();
			this.cbimmediately = new System.Windows.Forms.CheckBox();
			this.cbjoinable = new System.Windows.Forms.CheckBox();
			this.tbAction = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.tbJoinIndex = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.tpMotives = new System.Windows.Forms.TabPage();
			this.ttabItemMotiveTableUI1 = new SimPe.PackedFiles.UserInterface.TtabItemMotiveTableUI();
			this.panel5 = new System.Windows.Forms.Panel();
			this.btnHelp = new System.Windows.Forms.Button();
			this.label25 = new System.Windows.Forms.Label();
			this.ttabPanel.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tpSettings.SuspendLayout();
			this.gbFlags.SuspendLayout();
			this.tpMotives.SuspendLayout();
			this.panel5.SuspendLayout();
			this.SuspendLayout();
			// 
			// ttabPanel
			// 
			this.ttabPanel.AccessibleDescription = resources.GetString("ttabPanel.AccessibleDescription");
			this.ttabPanel.AccessibleName = resources.GetString("ttabPanel.AccessibleName");
			this.ttabPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("ttabPanel.Anchor")));
			this.ttabPanel.AutoScroll = ((bool)(resources.GetObject("ttabPanel.AutoScroll")));
			this.ttabPanel.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("ttabPanel.AutoScrollMargin")));
			this.ttabPanel.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("ttabPanel.AutoScrollMinSize")));
			this.ttabPanel.BackColor = System.Drawing.SystemColors.Control;
			this.ttabPanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ttabPanel.BackgroundImage")));
			this.ttabPanel.Controls.Add(this.btnAppend);
			this.ttabPanel.Controls.Add(this.lbFilename);
			this.ttabPanel.Controls.Add(this.tbFilename);
			this.ttabPanel.Controls.Add(this.tbFormat);
			this.ttabPanel.Controls.Add(this.label41);
			this.ttabPanel.Controls.Add(this.btnCommit);
			this.ttabPanel.Controls.Add(this.btnAdd);
			this.ttabPanel.Controls.Add(this.label26);
			this.ttabPanel.Controls.Add(this.btnDelete);
			this.ttabPanel.Controls.Add(this.lbttab);
			this.ttabPanel.Controls.Add(this.tabControl1);
			this.ttabPanel.Controls.Add(this.panel5);
			this.ttabPanel.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("ttabPanel.Dock")));
			this.ttabPanel.Enabled = ((bool)(resources.GetObject("ttabPanel.Enabled")));
			this.ttabPanel.Font = ((System.Drawing.Font)(resources.GetObject("ttabPanel.Font")));
			this.ttabPanel.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("ttabPanel.ImeMode")));
			this.ttabPanel.Location = ((System.Drawing.Point)(resources.GetObject("ttabPanel.Location")));
			this.ttabPanel.Name = "ttabPanel";
			this.ttabPanel.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("ttabPanel.RightToLeft")));
			this.ttabPanel.Size = ((System.Drawing.Size)(resources.GetObject("ttabPanel.Size")));
			this.ttabPanel.TabIndex = ((int)(resources.GetObject("ttabPanel.TabIndex")));
			this.ttabPanel.Text = resources.GetString("ttabPanel.Text");
			this.ttabPanel.Visible = ((bool)(resources.GetObject("ttabPanel.Visible")));
			// 
			// btnAppend
			// 
			this.btnAppend.AccessibleDescription = resources.GetString("btnAppend.AccessibleDescription");
			this.btnAppend.AccessibleName = resources.GetString("btnAppend.AccessibleName");
			this.btnAppend.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnAppend.Anchor")));
			this.btnAppend.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAppend.BackgroundImage")));
			this.btnAppend.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnAppend.Dock")));
			this.btnAppend.Enabled = ((bool)(resources.GetObject("btnAppend.Enabled")));
			this.btnAppend.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnAppend.FlatStyle")));
			this.btnAppend.Font = ((System.Drawing.Font)(resources.GetObject("btnAppend.Font")));
			this.btnAppend.Image = ((System.Drawing.Image)(resources.GetObject("btnAppend.Image")));
			this.btnAppend.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnAppend.ImageAlign")));
			this.btnAppend.ImageIndex = ((int)(resources.GetObject("btnAppend.ImageIndex")));
			this.btnAppend.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnAppend.ImeMode")));
			this.btnAppend.Location = ((System.Drawing.Point)(resources.GetObject("btnAppend.Location")));
			this.btnAppend.Name = "btnAppend";
			this.btnAppend.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnAppend.RightToLeft")));
			this.btnAppend.Size = ((System.Drawing.Size)(resources.GetObject("btnAppend.Size")));
			this.btnAppend.TabIndex = ((int)(resources.GetObject("btnAppend.TabIndex")));
			this.btnAppend.Text = resources.GetString("btnAppend.Text");
			this.btnAppend.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnAppend.TextAlign")));
			this.btnAppend.Visible = ((bool)(resources.GetObject("btnAppend.Visible")));
			this.btnAppend.Click += new System.EventHandler(this.btnAppend_Click);
			// 
			// lbFilename
			// 
			this.lbFilename.AccessibleDescription = resources.GetString("lbFilename.AccessibleDescription");
			this.lbFilename.AccessibleName = resources.GetString("lbFilename.AccessibleName");
			this.lbFilename.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbFilename.Anchor")));
			this.lbFilename.AutoSize = ((bool)(resources.GetObject("lbFilename.AutoSize")));
			this.lbFilename.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbFilename.Dock")));
			this.lbFilename.Enabled = ((bool)(resources.GetObject("lbFilename.Enabled")));
			this.lbFilename.Font = ((System.Drawing.Font)(resources.GetObject("lbFilename.Font")));
			this.lbFilename.Image = ((System.Drawing.Image)(resources.GetObject("lbFilename.Image")));
			this.lbFilename.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbFilename.ImageAlign")));
			this.lbFilename.ImageIndex = ((int)(resources.GetObject("lbFilename.ImageIndex")));
			this.lbFilename.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbFilename.ImeMode")));
			this.lbFilename.Location = ((System.Drawing.Point)(resources.GetObject("lbFilename.Location")));
			this.lbFilename.Name = "lbFilename";
			this.lbFilename.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbFilename.RightToLeft")));
			this.lbFilename.Size = ((System.Drawing.Size)(resources.GetObject("lbFilename.Size")));
			this.lbFilename.TabIndex = ((int)(resources.GetObject("lbFilename.TabIndex")));
			this.lbFilename.Text = resources.GetString("lbFilename.Text");
			this.lbFilename.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbFilename.TextAlign")));
			this.lbFilename.Visible = ((bool)(resources.GetObject("lbFilename.Visible")));
			// 
			// tbFilename
			// 
			this.tbFilename.AccessibleDescription = resources.GetString("tbFilename.AccessibleDescription");
			this.tbFilename.AccessibleName = resources.GetString("tbFilename.AccessibleName");
			this.tbFilename.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbFilename.Anchor")));
			this.tbFilename.AutoSize = ((bool)(resources.GetObject("tbFilename.AutoSize")));
			this.tbFilename.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbFilename.BackgroundImage")));
			this.tbFilename.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbFilename.Dock")));
			this.tbFilename.Enabled = ((bool)(resources.GetObject("tbFilename.Enabled")));
			this.tbFilename.Font = ((System.Drawing.Font)(resources.GetObject("tbFilename.Font")));
			this.tbFilename.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbFilename.ImeMode")));
			this.tbFilename.Location = ((System.Drawing.Point)(resources.GetObject("tbFilename.Location")));
			this.tbFilename.MaxLength = ((int)(resources.GetObject("tbFilename.MaxLength")));
			this.tbFilename.Multiline = ((bool)(resources.GetObject("tbFilename.Multiline")));
			this.tbFilename.Name = "tbFilename";
			this.tbFilename.PasswordChar = ((char)(resources.GetObject("tbFilename.PasswordChar")));
			this.tbFilename.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbFilename.RightToLeft")));
			this.tbFilename.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbFilename.ScrollBars")));
			this.tbFilename.Size = ((System.Drawing.Size)(resources.GetObject("tbFilename.Size")));
			this.tbFilename.TabIndex = ((int)(resources.GetObject("tbFilename.TabIndex")));
			this.tbFilename.Text = resources.GetString("tbFilename.Text");
			this.tbFilename.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbFilename.TextAlign")));
			this.tbFilename.Visible = ((bool)(resources.GetObject("tbFilename.Visible")));
			this.tbFilename.WordWrap = ((bool)(resources.GetObject("tbFilename.WordWrap")));
			this.tbFilename.Validated += new System.EventHandler(this.tbFilename_Validated);
			this.tbFilename.TextChanged += new System.EventHandler(this.tbFilename_TextChanged);
			// 
			// tbFormat
			// 
			this.tbFormat.AccessibleDescription = resources.GetString("tbFormat.AccessibleDescription");
			this.tbFormat.AccessibleName = resources.GetString("tbFormat.AccessibleName");
			this.tbFormat.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbFormat.Anchor")));
			this.tbFormat.AutoSize = ((bool)(resources.GetObject("tbFormat.AutoSize")));
			this.tbFormat.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbFormat.BackgroundImage")));
			this.tbFormat.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbFormat.Dock")));
			this.tbFormat.Enabled = ((bool)(resources.GetObject("tbFormat.Enabled")));
			this.tbFormat.Font = ((System.Drawing.Font)(resources.GetObject("tbFormat.Font")));
			this.tbFormat.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbFormat.ImeMode")));
			this.tbFormat.Location = ((System.Drawing.Point)(resources.GetObject("tbFormat.Location")));
			this.tbFormat.MaxLength = ((int)(resources.GetObject("tbFormat.MaxLength")));
			this.tbFormat.Multiline = ((bool)(resources.GetObject("tbFormat.Multiline")));
			this.tbFormat.Name = "tbFormat";
			this.tbFormat.PasswordChar = ((char)(resources.GetObject("tbFormat.PasswordChar")));
			this.tbFormat.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbFormat.RightToLeft")));
			this.tbFormat.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbFormat.ScrollBars")));
			this.tbFormat.Size = ((System.Drawing.Size)(resources.GetObject("tbFormat.Size")));
			this.tbFormat.TabIndex = ((int)(resources.GetObject("tbFormat.TabIndex")));
			this.tbFormat.Text = resources.GetString("tbFormat.Text");
			this.tbFormat.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbFormat.TextAlign")));
			this.tbFormat.Visible = ((bool)(resources.GetObject("tbFormat.Visible")));
			this.tbFormat.WordWrap = ((bool)(resources.GetObject("tbFormat.WordWrap")));
			this.tbFormat.Validating += new System.ComponentModel.CancelEventHandler(this.hex32_Validating);
			this.tbFormat.Validated += new System.EventHandler(this.hex32_Validated);
			this.tbFormat.TextChanged += new System.EventHandler(this.hex32_TextChanged);
			// 
			// label41
			// 
			this.label41.AccessibleDescription = resources.GetString("label41.AccessibleDescription");
			this.label41.AccessibleName = resources.GetString("label41.AccessibleName");
			this.label41.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label41.Anchor")));
			this.label41.AutoSize = ((bool)(resources.GetObject("label41.AutoSize")));
			this.label41.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label41.Dock")));
			this.label41.Enabled = ((bool)(resources.GetObject("label41.Enabled")));
			this.label41.Font = ((System.Drawing.Font)(resources.GetObject("label41.Font")));
			this.label41.Image = ((System.Drawing.Image)(resources.GetObject("label41.Image")));
			this.label41.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label41.ImageAlign")));
			this.label41.ImageIndex = ((int)(resources.GetObject("label41.ImageIndex")));
			this.label41.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label41.ImeMode")));
			this.label41.Location = ((System.Drawing.Point)(resources.GetObject("label41.Location")));
			this.label41.Name = "label41";
			this.label41.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label41.RightToLeft")));
			this.label41.Size = ((System.Drawing.Size)(resources.GetObject("label41.Size")));
			this.label41.TabIndex = ((int)(resources.GetObject("label41.TabIndex")));
			this.label41.Text = resources.GetString("label41.Text");
			this.label41.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label41.TextAlign")));
			this.label41.Visible = ((bool)(resources.GetObject("label41.Visible")));
			// 
			// btnCommit
			// 
			this.btnCommit.AccessibleDescription = resources.GetString("btnCommit.AccessibleDescription");
			this.btnCommit.AccessibleName = resources.GetString("btnCommit.AccessibleName");
			this.btnCommit.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnCommit.Anchor")));
			this.btnCommit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCommit.BackgroundImage")));
			this.btnCommit.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnCommit.Dock")));
			this.btnCommit.Enabled = ((bool)(resources.GetObject("btnCommit.Enabled")));
			this.btnCommit.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnCommit.FlatStyle")));
			this.btnCommit.Font = ((System.Drawing.Font)(resources.GetObject("btnCommit.Font")));
			this.btnCommit.Image = ((System.Drawing.Image)(resources.GetObject("btnCommit.Image")));
			this.btnCommit.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnCommit.ImageAlign")));
			this.btnCommit.ImageIndex = ((int)(resources.GetObject("btnCommit.ImageIndex")));
			this.btnCommit.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnCommit.ImeMode")));
			this.btnCommit.Location = ((System.Drawing.Point)(resources.GetObject("btnCommit.Location")));
			this.btnCommit.Name = "btnCommit";
			this.btnCommit.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnCommit.RightToLeft")));
			this.btnCommit.Size = ((System.Drawing.Size)(resources.GetObject("btnCommit.Size")));
			this.btnCommit.TabIndex = ((int)(resources.GetObject("btnCommit.TabIndex")));
			this.btnCommit.Text = resources.GetString("btnCommit.Text");
			this.btnCommit.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnCommit.TextAlign")));
			this.btnCommit.Visible = ((bool)(resources.GetObject("btnCommit.Visible")));
			this.btnCommit.Click += new System.EventHandler(this.btnCommit_Click);
			// 
			// btnAdd
			// 
			this.btnAdd.AccessibleDescription = resources.GetString("btnAdd.AccessibleDescription");
			this.btnAdd.AccessibleName = resources.GetString("btnAdd.AccessibleName");
			this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnAdd.Anchor")));
			this.btnAdd.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAdd.BackgroundImage")));
			this.btnAdd.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnAdd.Dock")));
			this.btnAdd.Enabled = ((bool)(resources.GetObject("btnAdd.Enabled")));
			this.btnAdd.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnAdd.FlatStyle")));
			this.btnAdd.Font = ((System.Drawing.Font)(resources.GetObject("btnAdd.Font")));
			this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
			this.btnAdd.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnAdd.ImageAlign")));
			this.btnAdd.ImageIndex = ((int)(resources.GetObject("btnAdd.ImageIndex")));
			this.btnAdd.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnAdd.ImeMode")));
			this.btnAdd.Location = ((System.Drawing.Point)(resources.GetObject("btnAdd.Location")));
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnAdd.RightToLeft")));
			this.btnAdd.Size = ((System.Drawing.Size)(resources.GetObject("btnAdd.Size")));
			this.btnAdd.TabIndex = ((int)(resources.GetObject("btnAdd.TabIndex")));
			this.btnAdd.Text = resources.GetString("btnAdd.Text");
			this.btnAdd.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnAdd.TextAlign")));
			this.btnAdd.Visible = ((bool)(resources.GetObject("btnAdd.Visible")));
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			// 
			// label26
			// 
			this.label26.AccessibleDescription = resources.GetString("label26.AccessibleDescription");
			this.label26.AccessibleName = resources.GetString("label26.AccessibleName");
			this.label26.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label26.Anchor")));
			this.label26.AutoSize = ((bool)(resources.GetObject("label26.AutoSize")));
			this.label26.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label26.Dock")));
			this.label26.Enabled = ((bool)(resources.GetObject("label26.Enabled")));
			this.label26.Font = ((System.Drawing.Font)(resources.GetObject("label26.Font")));
			this.label26.Image = ((System.Drawing.Image)(resources.GetObject("label26.Image")));
			this.label26.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label26.ImageAlign")));
			this.label26.ImageIndex = ((int)(resources.GetObject("label26.ImageIndex")));
			this.label26.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label26.ImeMode")));
			this.label26.Location = ((System.Drawing.Point)(resources.GetObject("label26.Location")));
			this.label26.Name = "label26";
			this.label26.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label26.RightToLeft")));
			this.label26.Size = ((System.Drawing.Size)(resources.GetObject("label26.Size")));
			this.label26.TabIndex = ((int)(resources.GetObject("label26.TabIndex")));
			this.label26.Text = resources.GetString("label26.Text");
			this.label26.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label26.TextAlign")));
			this.label26.Visible = ((bool)(resources.GetObject("label26.Visible")));
			// 
			// btnDelete
			// 
			this.btnDelete.AccessibleDescription = resources.GetString("btnDelete.AccessibleDescription");
			this.btnDelete.AccessibleName = resources.GetString("btnDelete.AccessibleName");
			this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnDelete.Anchor")));
			this.btnDelete.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDelete.BackgroundImage")));
			this.btnDelete.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnDelete.Dock")));
			this.btnDelete.Enabled = ((bool)(resources.GetObject("btnDelete.Enabled")));
			this.btnDelete.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnDelete.FlatStyle")));
			this.btnDelete.Font = ((System.Drawing.Font)(resources.GetObject("btnDelete.Font")));
			this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
			this.btnDelete.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnDelete.ImageAlign")));
			this.btnDelete.ImageIndex = ((int)(resources.GetObject("btnDelete.ImageIndex")));
			this.btnDelete.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnDelete.ImeMode")));
			this.btnDelete.Location = ((System.Drawing.Point)(resources.GetObject("btnDelete.Location")));
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnDelete.RightToLeft")));
			this.btnDelete.Size = ((System.Drawing.Size)(resources.GetObject("btnDelete.Size")));
			this.btnDelete.TabIndex = ((int)(resources.GetObject("btnDelete.TabIndex")));
			this.btnDelete.Text = resources.GetString("btnDelete.Text");
			this.btnDelete.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnDelete.TextAlign")));
			this.btnDelete.Visible = ((bool)(resources.GetObject("btnDelete.Visible")));
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			// 
			// lbttab
			// 
			this.lbttab.AccessibleDescription = resources.GetString("lbttab.AccessibleDescription");
			this.lbttab.AccessibleName = resources.GetString("lbttab.AccessibleName");
			this.lbttab.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbttab.Anchor")));
			this.lbttab.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("lbttab.BackgroundImage")));
			this.lbttab.ColumnWidth = ((int)(resources.GetObject("lbttab.ColumnWidth")));
			this.lbttab.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbttab.Dock")));
			this.lbttab.Enabled = ((bool)(resources.GetObject("lbttab.Enabled")));
			this.lbttab.Font = ((System.Drawing.Font)(resources.GetObject("lbttab.Font")));
			this.lbttab.HorizontalExtent = ((int)(resources.GetObject("lbttab.HorizontalExtent")));
			this.lbttab.HorizontalScrollbar = ((bool)(resources.GetObject("lbttab.HorizontalScrollbar")));
			this.lbttab.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbttab.ImeMode")));
			this.lbttab.IntegralHeight = ((bool)(resources.GetObject("lbttab.IntegralHeight")));
			this.lbttab.ItemHeight = ((int)(resources.GetObject("lbttab.ItemHeight")));
			this.lbttab.Location = ((System.Drawing.Point)(resources.GetObject("lbttab.Location")));
			this.lbttab.Name = "lbttab";
			this.lbttab.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbttab.RightToLeft")));
			this.lbttab.ScrollAlwaysVisible = ((bool)(resources.GetObject("lbttab.ScrollAlwaysVisible")));
			this.lbttab.Size = ((System.Drawing.Size)(resources.GetObject("lbttab.Size")));
			this.lbttab.TabIndex = ((int)(resources.GetObject("lbttab.TabIndex")));
			this.lbttab.Visible = ((bool)(resources.GetObject("lbttab.Visible")));
			this.lbttab.SelectedIndexChanged += new System.EventHandler(this.TtabSelect);
			// 
			// tabControl1
			// 
			this.tabControl1.AccessibleDescription = resources.GetString("tabControl1.AccessibleDescription");
			this.tabControl1.AccessibleName = resources.GetString("tabControl1.AccessibleName");
			this.tabControl1.Alignment = ((System.Windows.Forms.TabAlignment)(resources.GetObject("tabControl1.Alignment")));
			this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tabControl1.Anchor")));
			this.tabControl1.Appearance = ((System.Windows.Forms.TabAppearance)(resources.GetObject("tabControl1.Appearance")));
			this.tabControl1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tabControl1.BackgroundImage")));
			this.tabControl1.Controls.Add(this.tpSettings);
			this.tabControl1.Controls.Add(this.tpMotives);
			this.tabControl1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tabControl1.Dock")));
			this.tabControl1.Enabled = ((bool)(resources.GetObject("tabControl1.Enabled")));
			this.tabControl1.Font = ((System.Drawing.Font)(resources.GetObject("tabControl1.Font")));
			this.tabControl1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tabControl1.ImeMode")));
			this.tabControl1.ItemSize = ((System.Drawing.Size)(resources.GetObject("tabControl1.ItemSize")));
			this.tabControl1.Location = ((System.Drawing.Point)(resources.GetObject("tabControl1.Location")));
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.Padding = ((System.Drawing.Point)(resources.GetObject("tabControl1.Padding")));
			this.tabControl1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tabControl1.RightToLeft")));
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.ShowToolTips = ((bool)(resources.GetObject("tabControl1.ShowToolTips")));
			this.tabControl1.Size = ((System.Drawing.Size)(resources.GetObject("tabControl1.Size")));
			this.tabControl1.TabIndex = ((int)(resources.GetObject("tabControl1.TabIndex")));
			this.tabControl1.Text = resources.GetString("tabControl1.Text");
			this.tabControl1.Visible = ((bool)(resources.GetObject("tabControl1.Visible")));
			// 
			// tpSettings
			// 
			this.tpSettings.AccessibleDescription = resources.GetString("tpSettings.AccessibleDescription");
			this.tpSettings.AccessibleName = resources.GetString("tpSettings.AccessibleName");
			this.tpSettings.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tpSettings.Anchor")));
			this.tpSettings.AutoScroll = ((bool)(resources.GetObject("tpSettings.AutoScroll")));
			this.tpSettings.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("tpSettings.AutoScrollMargin")));
			this.tpSettings.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("tpSettings.AutoScrollMinSize")));
			this.tpSettings.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tpSettings.BackgroundImage")));
			this.tpSettings.Controls.Add(this.llGuardian);
			this.tpSettings.Controls.Add(this.llAction);
			this.tpSettings.Controls.Add(this.cbStringIndex);
			this.tpSettings.Controls.Add(this.cbAttenuationCode);
			this.tpSettings.Controls.Add(this.btnAction);
			this.tpSettings.Controls.Add(this.btnGuardian);
			this.tpSettings.Controls.Add(this.lbaction);
			this.tpSettings.Controls.Add(this.lbguard);
			this.tpSettings.Controls.Add(this.tbStringIndex);
			this.tpSettings.Controls.Add(this.label40);
			this.tpSettings.Controls.Add(this.tbModelTabID);
			this.tpSettings.Controls.Add(this.label33);
			this.tpSettings.Controls.Add(this.tbObjType);
			this.tpSettings.Controls.Add(this.label34);
			this.tpSettings.Controls.Add(this.tbUIDispType);
			this.tpSettings.Controls.Add(this.label35);
			this.tpSettings.Controls.Add(this.tbAutonomy);
			this.tpSettings.Controls.Add(this.tbMemIterMult);
			this.tpSettings.Controls.Add(this.label29);
			this.tpSettings.Controls.Add(this.tbFaceAnimID);
			this.tpSettings.Controls.Add(this.label30);
			this.tpSettings.Controls.Add(this.tbAttenuationValue);
			this.tpSettings.Controls.Add(this.label31);
			this.tpSettings.Controls.Add(this.label32);
			this.tpSettings.Controls.Add(this.tbFlags2);
			this.tpSettings.Controls.Add(this.label20);
			this.tpSettings.Controls.Add(this.tbGuardian);
			this.tpSettings.Controls.Add(this.gbFlags);
			this.tpSettings.Controls.Add(this.tbAction);
			this.tpSettings.Controls.Add(this.label1);
			this.tpSettings.Controls.Add(this.tbJoinIndex);
			this.tpSettings.Controls.Add(this.label2);
			this.tpSettings.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tpSettings.Dock")));
			this.tpSettings.Enabled = ((bool)(resources.GetObject("tpSettings.Enabled")));
			this.tpSettings.Font = ((System.Drawing.Font)(resources.GetObject("tpSettings.Font")));
			this.tpSettings.ImageIndex = ((int)(resources.GetObject("tpSettings.ImageIndex")));
			this.tpSettings.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tpSettings.ImeMode")));
			this.tpSettings.Location = ((System.Drawing.Point)(resources.GetObject("tpSettings.Location")));
			this.tpSettings.Name = "tpSettings";
			this.tpSettings.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tpSettings.RightToLeft")));
			this.tpSettings.Size = ((System.Drawing.Size)(resources.GetObject("tpSettings.Size")));
			this.tpSettings.TabIndex = ((int)(resources.GetObject("tpSettings.TabIndex")));
			this.tpSettings.Text = resources.GetString("tpSettings.Text");
			this.tpSettings.ToolTipText = resources.GetString("tpSettings.ToolTipText");
			this.tpSettings.Visible = ((bool)(resources.GetObject("tpSettings.Visible")));
			// 
			// llGuardian
			// 
			this.llGuardian.AccessibleDescription = resources.GetString("llGuardian.AccessibleDescription");
			this.llGuardian.AccessibleName = resources.GetString("llGuardian.AccessibleName");
			this.llGuardian.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("llGuardian.Anchor")));
			this.llGuardian.AutoSize = ((bool)(resources.GetObject("llGuardian.AutoSize")));
			this.llGuardian.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("llGuardian.Dock")));
			this.llGuardian.Enabled = ((bool)(resources.GetObject("llGuardian.Enabled")));
			this.llGuardian.Font = ((System.Drawing.Font)(resources.GetObject("llGuardian.Font")));
			this.llGuardian.Image = ((System.Drawing.Image)(resources.GetObject("llGuardian.Image")));
			this.llGuardian.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("llGuardian.ImageAlign")));
			this.llGuardian.ImageIndex = ((int)(resources.GetObject("llGuardian.ImageIndex")));
			this.llGuardian.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("llGuardian.ImeMode")));
			this.llGuardian.LinkArea = ((System.Windows.Forms.LinkArea)(resources.GetObject("llGuardian.LinkArea")));
			this.llGuardian.Location = ((System.Drawing.Point)(resources.GetObject("llGuardian.Location")));
			this.llGuardian.Name = "llGuardian";
			this.llGuardian.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("llGuardian.RightToLeft")));
			this.llGuardian.Size = ((System.Drawing.Size)(resources.GetObject("llGuardian.Size")));
			this.llGuardian.TabIndex = ((int)(resources.GetObject("llGuardian.TabIndex")));
			this.llGuardian.TabStop = true;
			this.llGuardian.Text = resources.GetString("llGuardian.Text");
			this.llGuardian.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("llGuardian.TextAlign")));
			this.llGuardian.Visible = ((bool)(resources.GetObject("llGuardian.Visible")));
			this.llGuardian.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llBhav_LinkClicked);
			// 
			// llAction
			// 
			this.llAction.AccessibleDescription = resources.GetString("llAction.AccessibleDescription");
			this.llAction.AccessibleName = resources.GetString("llAction.AccessibleName");
			this.llAction.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("llAction.Anchor")));
			this.llAction.AutoSize = ((bool)(resources.GetObject("llAction.AutoSize")));
			this.llAction.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("llAction.Dock")));
			this.llAction.Enabled = ((bool)(resources.GetObject("llAction.Enabled")));
			this.llAction.Font = ((System.Drawing.Font)(resources.GetObject("llAction.Font")));
			this.llAction.Image = ((System.Drawing.Image)(resources.GetObject("llAction.Image")));
			this.llAction.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("llAction.ImageAlign")));
			this.llAction.ImageIndex = ((int)(resources.GetObject("llAction.ImageIndex")));
			this.llAction.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("llAction.ImeMode")));
			this.llAction.LinkArea = ((System.Windows.Forms.LinkArea)(resources.GetObject("llAction.LinkArea")));
			this.llAction.Location = ((System.Drawing.Point)(resources.GetObject("llAction.Location")));
			this.llAction.Name = "llAction";
			this.llAction.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("llAction.RightToLeft")));
			this.llAction.Size = ((System.Drawing.Size)(resources.GetObject("llAction.Size")));
			this.llAction.TabIndex = ((int)(resources.GetObject("llAction.TabIndex")));
			this.llAction.TabStop = true;
			this.llAction.Text = resources.GetString("llAction.Text");
			this.llAction.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("llAction.TextAlign")));
			this.llAction.Visible = ((bool)(resources.GetObject("llAction.Visible")));
			this.llAction.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llBhav_LinkClicked);
			// 
			// cbStringIndex
			// 
			this.cbStringIndex.AccessibleDescription = resources.GetString("cbStringIndex.AccessibleDescription");
			this.cbStringIndex.AccessibleName = resources.GetString("cbStringIndex.AccessibleName");
			this.cbStringIndex.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cbStringIndex.Anchor")));
			this.cbStringIndex.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cbStringIndex.BackgroundImage")));
			this.cbStringIndex.DisplayMember = "Display";
			this.cbStringIndex.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cbStringIndex.Dock")));
			this.cbStringIndex.DropDownWidth = 240;
			this.cbStringIndex.Enabled = ((bool)(resources.GetObject("cbStringIndex.Enabled")));
			this.cbStringIndex.Font = ((System.Drawing.Font)(resources.GetObject("cbStringIndex.Font")));
			this.cbStringIndex.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cbStringIndex.ImeMode")));
			this.cbStringIndex.IntegralHeight = ((bool)(resources.GetObject("cbStringIndex.IntegralHeight")));
			this.cbStringIndex.ItemHeight = ((int)(resources.GetObject("cbStringIndex.ItemHeight")));
			this.cbStringIndex.Items.AddRange(new object[] {
															   resources.GetString("cbStringIndex.Items"),
															   resources.GetString("cbStringIndex.Items1"),
															   resources.GetString("cbStringIndex.Items2")});
			this.cbStringIndex.Location = ((System.Drawing.Point)(resources.GetObject("cbStringIndex.Location")));
			this.cbStringIndex.MaxDropDownItems = ((int)(resources.GetObject("cbStringIndex.MaxDropDownItems")));
			this.cbStringIndex.MaxLength = ((int)(resources.GetObject("cbStringIndex.MaxLength")));
			this.cbStringIndex.Name = "cbStringIndex";
			this.cbStringIndex.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cbStringIndex.RightToLeft")));
			this.cbStringIndex.Size = ((System.Drawing.Size)(resources.GetObject("cbStringIndex.Size")));
			this.cbStringIndex.TabIndex = ((int)(resources.GetObject("cbStringIndex.TabIndex")));
			this.cbStringIndex.TabStop = false;
			this.cbStringIndex.Text = resources.GetString("cbStringIndex.Text");
			this.cbStringIndex.ValueMember = "Value";
			this.cbStringIndex.Visible = ((bool)(resources.GetObject("cbStringIndex.Visible")));
			this.cbStringIndex.Validating += new System.ComponentModel.CancelEventHandler(this.cbHex32_Validating);
			this.cbStringIndex.Validated += new System.EventHandler(this.cbHex32_Validated);
			this.cbStringIndex.TextChanged += new System.EventHandler(this.cbHex32_TextChanged);
			this.cbStringIndex.SelectedIndexChanged += new System.EventHandler(this.cbHex32_SelectedIndexChanged);
			this.cbStringIndex.Enter += new System.EventHandler(this.cbHex32_Enter);
			// 
			// cbAttenuationCode
			// 
			this.cbAttenuationCode.AccessibleDescription = resources.GetString("cbAttenuationCode.AccessibleDescription");
			this.cbAttenuationCode.AccessibleName = resources.GetString("cbAttenuationCode.AccessibleName");
			this.cbAttenuationCode.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cbAttenuationCode.Anchor")));
			this.cbAttenuationCode.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cbAttenuationCode.BackgroundImage")));
			this.cbAttenuationCode.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cbAttenuationCode.Dock")));
			this.cbAttenuationCode.Enabled = ((bool)(resources.GetObject("cbAttenuationCode.Enabled")));
			this.cbAttenuationCode.Font = ((System.Drawing.Font)(resources.GetObject("cbAttenuationCode.Font")));
			this.cbAttenuationCode.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cbAttenuationCode.ImeMode")));
			this.cbAttenuationCode.IntegralHeight = ((bool)(resources.GetObject("cbAttenuationCode.IntegralHeight")));
			this.cbAttenuationCode.ItemHeight = ((int)(resources.GetObject("cbAttenuationCode.ItemHeight")));
			this.cbAttenuationCode.Items.AddRange(new object[] {
																   resources.GetString("cbAttenuationCode.Items"),
																   resources.GetString("cbAttenuationCode.Items1"),
																   resources.GetString("cbAttenuationCode.Items2"),
																   resources.GetString("cbAttenuationCode.Items3"),
																   resources.GetString("cbAttenuationCode.Items4")});
			this.cbAttenuationCode.Location = ((System.Drawing.Point)(resources.GetObject("cbAttenuationCode.Location")));
			this.cbAttenuationCode.MaxDropDownItems = ((int)(resources.GetObject("cbAttenuationCode.MaxDropDownItems")));
			this.cbAttenuationCode.MaxLength = ((int)(resources.GetObject("cbAttenuationCode.MaxLength")));
			this.cbAttenuationCode.Name = "cbAttenuationCode";
			this.cbAttenuationCode.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cbAttenuationCode.RightToLeft")));
			this.cbAttenuationCode.Size = ((System.Drawing.Size)(resources.GetObject("cbAttenuationCode.Size")));
			this.cbAttenuationCode.TabIndex = ((int)(resources.GetObject("cbAttenuationCode.TabIndex")));
			this.cbAttenuationCode.Text = resources.GetString("cbAttenuationCode.Text");
			this.cbAttenuationCode.Visible = ((bool)(resources.GetObject("cbAttenuationCode.Visible")));
			this.cbAttenuationCode.Validating += new System.ComponentModel.CancelEventHandler(this.cbHex32_Validating);
			this.cbAttenuationCode.Validated += new System.EventHandler(this.cbHex32_Validated);
			this.cbAttenuationCode.TextChanged += new System.EventHandler(this.cbHex32_TextChanged);
			this.cbAttenuationCode.SelectedIndexChanged += new System.EventHandler(this.cbHex32_SelectedIndexChanged);
			this.cbAttenuationCode.Enter += new System.EventHandler(this.cbHex32_Enter);
			// 
			// btnAction
			// 
			this.btnAction.AccessibleDescription = resources.GetString("btnAction.AccessibleDescription");
			this.btnAction.AccessibleName = resources.GetString("btnAction.AccessibleName");
			this.btnAction.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnAction.Anchor")));
			this.btnAction.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAction.BackgroundImage")));
			this.btnAction.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnAction.Dock")));
			this.btnAction.Enabled = ((bool)(resources.GetObject("btnAction.Enabled")));
			this.btnAction.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnAction.FlatStyle")));
			this.btnAction.Font = ((System.Drawing.Font)(resources.GetObject("btnAction.Font")));
			this.btnAction.Image = ((System.Drawing.Image)(resources.GetObject("btnAction.Image")));
			this.btnAction.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnAction.ImageAlign")));
			this.btnAction.ImageIndex = ((int)(resources.GetObject("btnAction.ImageIndex")));
			this.btnAction.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnAction.ImeMode")));
			this.btnAction.Location = ((System.Drawing.Point)(resources.GetObject("btnAction.Location")));
			this.btnAction.Name = "btnAction";
			this.btnAction.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnAction.RightToLeft")));
			this.btnAction.Size = ((System.Drawing.Size)(resources.GetObject("btnAction.Size")));
			this.btnAction.TabIndex = ((int)(resources.GetObject("btnAction.TabIndex")));
			this.btnAction.Text = resources.GetString("btnAction.Text");
			this.btnAction.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnAction.TextAlign")));
			this.btnAction.Visible = ((bool)(resources.GetObject("btnAction.Visible")));
			this.btnAction.Click += new System.EventHandler(this.GetTTABAction);
			// 
			// btnGuardian
			// 
			this.btnGuardian.AccessibleDescription = resources.GetString("btnGuardian.AccessibleDescription");
			this.btnGuardian.AccessibleName = resources.GetString("btnGuardian.AccessibleName");
			this.btnGuardian.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnGuardian.Anchor")));
			this.btnGuardian.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnGuardian.BackgroundImage")));
			this.btnGuardian.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnGuardian.Dock")));
			this.btnGuardian.Enabled = ((bool)(resources.GetObject("btnGuardian.Enabled")));
			this.btnGuardian.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnGuardian.FlatStyle")));
			this.btnGuardian.Font = ((System.Drawing.Font)(resources.GetObject("btnGuardian.Font")));
			this.btnGuardian.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardian.Image")));
			this.btnGuardian.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnGuardian.ImageAlign")));
			this.btnGuardian.ImageIndex = ((int)(resources.GetObject("btnGuardian.ImageIndex")));
			this.btnGuardian.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnGuardian.ImeMode")));
			this.btnGuardian.Location = ((System.Drawing.Point)(resources.GetObject("btnGuardian.Location")));
			this.btnGuardian.Name = "btnGuardian";
			this.btnGuardian.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnGuardian.RightToLeft")));
			this.btnGuardian.Size = ((System.Drawing.Size)(resources.GetObject("btnGuardian.Size")));
			this.btnGuardian.TabIndex = ((int)(resources.GetObject("btnGuardian.TabIndex")));
			this.btnGuardian.Text = resources.GetString("btnGuardian.Text");
			this.btnGuardian.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnGuardian.TextAlign")));
			this.btnGuardian.Visible = ((bool)(resources.GetObject("btnGuardian.Visible")));
			this.btnGuardian.Click += new System.EventHandler(this.GetTTABGuard);
			// 
			// lbaction
			// 
			this.lbaction.AccessibleDescription = resources.GetString("lbaction.AccessibleDescription");
			this.lbaction.AccessibleName = resources.GetString("lbaction.AccessibleName");
			this.lbaction.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbaction.Anchor")));
			this.lbaction.AutoSize = ((bool)(resources.GetObject("lbaction.AutoSize")));
			this.lbaction.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbaction.Dock")));
			this.lbaction.Enabled = ((bool)(resources.GetObject("lbaction.Enabled")));
			this.lbaction.Font = ((System.Drawing.Font)(resources.GetObject("lbaction.Font")));
			this.lbaction.Image = ((System.Drawing.Image)(resources.GetObject("lbaction.Image")));
			this.lbaction.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbaction.ImageAlign")));
			this.lbaction.ImageIndex = ((int)(resources.GetObject("lbaction.ImageIndex")));
			this.lbaction.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbaction.ImeMode")));
			this.lbaction.Location = ((System.Drawing.Point)(resources.GetObject("lbaction.Location")));
			this.lbaction.Name = "lbaction";
			this.lbaction.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbaction.RightToLeft")));
			this.lbaction.Size = ((System.Drawing.Size)(resources.GetObject("lbaction.Size")));
			this.lbaction.TabIndex = ((int)(resources.GetObject("lbaction.TabIndex")));
			this.lbaction.Text = resources.GetString("lbaction.Text");
			this.lbaction.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbaction.TextAlign")));
			this.lbaction.UseMnemonic = false;
			this.lbaction.Visible = ((bool)(resources.GetObject("lbaction.Visible")));
			// 
			// lbguard
			// 
			this.lbguard.AccessibleDescription = resources.GetString("lbguard.AccessibleDescription");
			this.lbguard.AccessibleName = resources.GetString("lbguard.AccessibleName");
			this.lbguard.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbguard.Anchor")));
			this.lbguard.AutoSize = ((bool)(resources.GetObject("lbguard.AutoSize")));
			this.lbguard.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbguard.Dock")));
			this.lbguard.Enabled = ((bool)(resources.GetObject("lbguard.Enabled")));
			this.lbguard.Font = ((System.Drawing.Font)(resources.GetObject("lbguard.Font")));
			this.lbguard.Image = ((System.Drawing.Image)(resources.GetObject("lbguard.Image")));
			this.lbguard.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbguard.ImageAlign")));
			this.lbguard.ImageIndex = ((int)(resources.GetObject("lbguard.ImageIndex")));
			this.lbguard.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbguard.ImeMode")));
			this.lbguard.Location = ((System.Drawing.Point)(resources.GetObject("lbguard.Location")));
			this.lbguard.Name = "lbguard";
			this.lbguard.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbguard.RightToLeft")));
			this.lbguard.Size = ((System.Drawing.Size)(resources.GetObject("lbguard.Size")));
			this.lbguard.TabIndex = ((int)(resources.GetObject("lbguard.TabIndex")));
			this.lbguard.Text = resources.GetString("lbguard.Text");
			this.lbguard.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbguard.TextAlign")));
			this.lbguard.UseMnemonic = false;
			this.lbguard.Visible = ((bool)(resources.GetObject("lbguard.Visible")));
			// 
			// tbStringIndex
			// 
			this.tbStringIndex.AccessibleDescription = resources.GetString("tbStringIndex.AccessibleDescription");
			this.tbStringIndex.AccessibleName = resources.GetString("tbStringIndex.AccessibleName");
			this.tbStringIndex.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbStringIndex.Anchor")));
			this.tbStringIndex.AutoSize = ((bool)(resources.GetObject("tbStringIndex.AutoSize")));
			this.tbStringIndex.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbStringIndex.BackgroundImage")));
			this.tbStringIndex.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbStringIndex.Dock")));
			this.tbStringIndex.Enabled = ((bool)(resources.GetObject("tbStringIndex.Enabled")));
			this.tbStringIndex.Font = ((System.Drawing.Font)(resources.GetObject("tbStringIndex.Font")));
			this.tbStringIndex.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbStringIndex.ImeMode")));
			this.tbStringIndex.Location = ((System.Drawing.Point)(resources.GetObject("tbStringIndex.Location")));
			this.tbStringIndex.MaxLength = ((int)(resources.GetObject("tbStringIndex.MaxLength")));
			this.tbStringIndex.Multiline = ((bool)(resources.GetObject("tbStringIndex.Multiline")));
			this.tbStringIndex.Name = "tbStringIndex";
			this.tbStringIndex.PasswordChar = ((char)(resources.GetObject("tbStringIndex.PasswordChar")));
			this.tbStringIndex.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbStringIndex.RightToLeft")));
			this.tbStringIndex.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbStringIndex.ScrollBars")));
			this.tbStringIndex.Size = ((System.Drawing.Size)(resources.GetObject("tbStringIndex.Size")));
			this.tbStringIndex.TabIndex = ((int)(resources.GetObject("tbStringIndex.TabIndex")));
			this.tbStringIndex.Text = resources.GetString("tbStringIndex.Text");
			this.tbStringIndex.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbStringIndex.TextAlign")));
			this.tbStringIndex.Visible = ((bool)(resources.GetObject("tbStringIndex.Visible")));
			this.tbStringIndex.WordWrap = ((bool)(resources.GetObject("tbStringIndex.WordWrap")));
			this.tbStringIndex.Validating += new System.ComponentModel.CancelEventHandler(this.hex32_Validating);
			this.tbStringIndex.Validated += new System.EventHandler(this.hex32_Validated);
			this.tbStringIndex.TextChanged += new System.EventHandler(this.hex32_TextChanged);
			// 
			// label40
			// 
			this.label40.AccessibleDescription = resources.GetString("label40.AccessibleDescription");
			this.label40.AccessibleName = resources.GetString("label40.AccessibleName");
			this.label40.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label40.Anchor")));
			this.label40.AutoSize = ((bool)(resources.GetObject("label40.AutoSize")));
			this.label40.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label40.Dock")));
			this.label40.Enabled = ((bool)(resources.GetObject("label40.Enabled")));
			this.label40.Font = ((System.Drawing.Font)(resources.GetObject("label40.Font")));
			this.label40.Image = ((System.Drawing.Image)(resources.GetObject("label40.Image")));
			this.label40.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label40.ImageAlign")));
			this.label40.ImageIndex = ((int)(resources.GetObject("label40.ImageIndex")));
			this.label40.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label40.ImeMode")));
			this.label40.Location = ((System.Drawing.Point)(resources.GetObject("label40.Location")));
			this.label40.Name = "label40";
			this.label40.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label40.RightToLeft")));
			this.label40.Size = ((System.Drawing.Size)(resources.GetObject("label40.Size")));
			this.label40.TabIndex = ((int)(resources.GetObject("label40.TabIndex")));
			this.label40.Text = resources.GetString("label40.Text");
			this.label40.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label40.TextAlign")));
			this.label40.Visible = ((bool)(resources.GetObject("label40.Visible")));
			// 
			// tbModelTabID
			// 
			this.tbModelTabID.AccessibleDescription = resources.GetString("tbModelTabID.AccessibleDescription");
			this.tbModelTabID.AccessibleName = resources.GetString("tbModelTabID.AccessibleName");
			this.tbModelTabID.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbModelTabID.Anchor")));
			this.tbModelTabID.AutoSize = ((bool)(resources.GetObject("tbModelTabID.AutoSize")));
			this.tbModelTabID.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbModelTabID.BackgroundImage")));
			this.tbModelTabID.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbModelTabID.Dock")));
			this.tbModelTabID.Enabled = ((bool)(resources.GetObject("tbModelTabID.Enabled")));
			this.tbModelTabID.Font = ((System.Drawing.Font)(resources.GetObject("tbModelTabID.Font")));
			this.tbModelTabID.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbModelTabID.ImeMode")));
			this.tbModelTabID.Location = ((System.Drawing.Point)(resources.GetObject("tbModelTabID.Location")));
			this.tbModelTabID.MaxLength = ((int)(resources.GetObject("tbModelTabID.MaxLength")));
			this.tbModelTabID.Multiline = ((bool)(resources.GetObject("tbModelTabID.Multiline")));
			this.tbModelTabID.Name = "tbModelTabID";
			this.tbModelTabID.PasswordChar = ((char)(resources.GetObject("tbModelTabID.PasswordChar")));
			this.tbModelTabID.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbModelTabID.RightToLeft")));
			this.tbModelTabID.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbModelTabID.ScrollBars")));
			this.tbModelTabID.Size = ((System.Drawing.Size)(resources.GetObject("tbModelTabID.Size")));
			this.tbModelTabID.TabIndex = ((int)(resources.GetObject("tbModelTabID.TabIndex")));
			this.tbModelTabID.Text = resources.GetString("tbModelTabID.Text");
			this.tbModelTabID.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbModelTabID.TextAlign")));
			this.tbModelTabID.Visible = ((bool)(resources.GetObject("tbModelTabID.Visible")));
			this.tbModelTabID.WordWrap = ((bool)(resources.GetObject("tbModelTabID.WordWrap")));
			this.tbModelTabID.Validating += new System.ComponentModel.CancelEventHandler(this.hex32_Validating);
			this.tbModelTabID.Validated += new System.EventHandler(this.hex32_Validated);
			this.tbModelTabID.TextChanged += new System.EventHandler(this.hex32_TextChanged);
			// 
			// label33
			// 
			this.label33.AccessibleDescription = resources.GetString("label33.AccessibleDescription");
			this.label33.AccessibleName = resources.GetString("label33.AccessibleName");
			this.label33.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label33.Anchor")));
			this.label33.AutoSize = ((bool)(resources.GetObject("label33.AutoSize")));
			this.label33.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label33.Dock")));
			this.label33.Enabled = ((bool)(resources.GetObject("label33.Enabled")));
			this.label33.Font = ((System.Drawing.Font)(resources.GetObject("label33.Font")));
			this.label33.Image = ((System.Drawing.Image)(resources.GetObject("label33.Image")));
			this.label33.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label33.ImageAlign")));
			this.label33.ImageIndex = ((int)(resources.GetObject("label33.ImageIndex")));
			this.label33.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label33.ImeMode")));
			this.label33.Location = ((System.Drawing.Point)(resources.GetObject("label33.Location")));
			this.label33.Name = "label33";
			this.label33.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label33.RightToLeft")));
			this.label33.Size = ((System.Drawing.Size)(resources.GetObject("label33.Size")));
			this.label33.TabIndex = ((int)(resources.GetObject("label33.TabIndex")));
			this.label33.Text = resources.GetString("label33.Text");
			this.label33.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label33.TextAlign")));
			this.label33.Visible = ((bool)(resources.GetObject("label33.Visible")));
			// 
			// tbObjType
			// 
			this.tbObjType.AccessibleDescription = resources.GetString("tbObjType.AccessibleDescription");
			this.tbObjType.AccessibleName = resources.GetString("tbObjType.AccessibleName");
			this.tbObjType.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbObjType.Anchor")));
			this.tbObjType.AutoSize = ((bool)(resources.GetObject("tbObjType.AutoSize")));
			this.tbObjType.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbObjType.BackgroundImage")));
			this.tbObjType.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbObjType.Dock")));
			this.tbObjType.Enabled = ((bool)(resources.GetObject("tbObjType.Enabled")));
			this.tbObjType.Font = ((System.Drawing.Font)(resources.GetObject("tbObjType.Font")));
			this.tbObjType.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbObjType.ImeMode")));
			this.tbObjType.Location = ((System.Drawing.Point)(resources.GetObject("tbObjType.Location")));
			this.tbObjType.MaxLength = ((int)(resources.GetObject("tbObjType.MaxLength")));
			this.tbObjType.Multiline = ((bool)(resources.GetObject("tbObjType.Multiline")));
			this.tbObjType.Name = "tbObjType";
			this.tbObjType.PasswordChar = ((char)(resources.GetObject("tbObjType.PasswordChar")));
			this.tbObjType.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbObjType.RightToLeft")));
			this.tbObjType.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbObjType.ScrollBars")));
			this.tbObjType.Size = ((System.Drawing.Size)(resources.GetObject("tbObjType.Size")));
			this.tbObjType.TabIndex = ((int)(resources.GetObject("tbObjType.TabIndex")));
			this.tbObjType.Text = resources.GetString("tbObjType.Text");
			this.tbObjType.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbObjType.TextAlign")));
			this.tbObjType.Visible = ((bool)(resources.GetObject("tbObjType.Visible")));
			this.tbObjType.WordWrap = ((bool)(resources.GetObject("tbObjType.WordWrap")));
			this.tbObjType.Validating += new System.ComponentModel.CancelEventHandler(this.hex32_Validating);
			this.tbObjType.Validated += new System.EventHandler(this.hex32_Validated);
			this.tbObjType.TextChanged += new System.EventHandler(this.hex32_TextChanged);
			// 
			// label34
			// 
			this.label34.AccessibleDescription = resources.GetString("label34.AccessibleDescription");
			this.label34.AccessibleName = resources.GetString("label34.AccessibleName");
			this.label34.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label34.Anchor")));
			this.label34.AutoSize = ((bool)(resources.GetObject("label34.AutoSize")));
			this.label34.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label34.Dock")));
			this.label34.Enabled = ((bool)(resources.GetObject("label34.Enabled")));
			this.label34.Font = ((System.Drawing.Font)(resources.GetObject("label34.Font")));
			this.label34.Image = ((System.Drawing.Image)(resources.GetObject("label34.Image")));
			this.label34.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label34.ImageAlign")));
			this.label34.ImageIndex = ((int)(resources.GetObject("label34.ImageIndex")));
			this.label34.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label34.ImeMode")));
			this.label34.Location = ((System.Drawing.Point)(resources.GetObject("label34.Location")));
			this.label34.Name = "label34";
			this.label34.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label34.RightToLeft")));
			this.label34.Size = ((System.Drawing.Size)(resources.GetObject("label34.Size")));
			this.label34.TabIndex = ((int)(resources.GetObject("label34.TabIndex")));
			this.label34.Text = resources.GetString("label34.Text");
			this.label34.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label34.TextAlign")));
			this.label34.Visible = ((bool)(resources.GetObject("label34.Visible")));
			// 
			// tbUIDispType
			// 
			this.tbUIDispType.AccessibleDescription = resources.GetString("tbUIDispType.AccessibleDescription");
			this.tbUIDispType.AccessibleName = resources.GetString("tbUIDispType.AccessibleName");
			this.tbUIDispType.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbUIDispType.Anchor")));
			this.tbUIDispType.AutoSize = ((bool)(resources.GetObject("tbUIDispType.AutoSize")));
			this.tbUIDispType.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbUIDispType.BackgroundImage")));
			this.tbUIDispType.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbUIDispType.Dock")));
			this.tbUIDispType.Enabled = ((bool)(resources.GetObject("tbUIDispType.Enabled")));
			this.tbUIDispType.Font = ((System.Drawing.Font)(resources.GetObject("tbUIDispType.Font")));
			this.tbUIDispType.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbUIDispType.ImeMode")));
			this.tbUIDispType.Location = ((System.Drawing.Point)(resources.GetObject("tbUIDispType.Location")));
			this.tbUIDispType.MaxLength = ((int)(resources.GetObject("tbUIDispType.MaxLength")));
			this.tbUIDispType.Multiline = ((bool)(resources.GetObject("tbUIDispType.Multiline")));
			this.tbUIDispType.Name = "tbUIDispType";
			this.tbUIDispType.PasswordChar = ((char)(resources.GetObject("tbUIDispType.PasswordChar")));
			this.tbUIDispType.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbUIDispType.RightToLeft")));
			this.tbUIDispType.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbUIDispType.ScrollBars")));
			this.tbUIDispType.Size = ((System.Drawing.Size)(resources.GetObject("tbUIDispType.Size")));
			this.tbUIDispType.TabIndex = ((int)(resources.GetObject("tbUIDispType.TabIndex")));
			this.tbUIDispType.Text = resources.GetString("tbUIDispType.Text");
			this.tbUIDispType.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbUIDispType.TextAlign")));
			this.tbUIDispType.Visible = ((bool)(resources.GetObject("tbUIDispType.Visible")));
			this.tbUIDispType.WordWrap = ((bool)(resources.GetObject("tbUIDispType.WordWrap")));
			this.tbUIDispType.Validating += new System.ComponentModel.CancelEventHandler(this.hex16_Validating);
			this.tbUIDispType.Validated += new System.EventHandler(this.hex16_Validated);
			this.tbUIDispType.TextChanged += new System.EventHandler(this.hex16_TextChanged);
			// 
			// label35
			// 
			this.label35.AccessibleDescription = resources.GetString("label35.AccessibleDescription");
			this.label35.AccessibleName = resources.GetString("label35.AccessibleName");
			this.label35.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label35.Anchor")));
			this.label35.AutoSize = ((bool)(resources.GetObject("label35.AutoSize")));
			this.label35.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label35.Dock")));
			this.label35.Enabled = ((bool)(resources.GetObject("label35.Enabled")));
			this.label35.Font = ((System.Drawing.Font)(resources.GetObject("label35.Font")));
			this.label35.Image = ((System.Drawing.Image)(resources.GetObject("label35.Image")));
			this.label35.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label35.ImageAlign")));
			this.label35.ImageIndex = ((int)(resources.GetObject("label35.ImageIndex")));
			this.label35.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label35.ImeMode")));
			this.label35.Location = ((System.Drawing.Point)(resources.GetObject("label35.Location")));
			this.label35.Name = "label35";
			this.label35.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label35.RightToLeft")));
			this.label35.Size = ((System.Drawing.Size)(resources.GetObject("label35.Size")));
			this.label35.TabIndex = ((int)(resources.GetObject("label35.TabIndex")));
			this.label35.Text = resources.GetString("label35.Text");
			this.label35.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label35.TextAlign")));
			this.label35.Visible = ((bool)(resources.GetObject("label35.Visible")));
			// 
			// tbAutonomy
			// 
			this.tbAutonomy.AccessibleDescription = resources.GetString("tbAutonomy.AccessibleDescription");
			this.tbAutonomy.AccessibleName = resources.GetString("tbAutonomy.AccessibleName");
			this.tbAutonomy.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbAutonomy.Anchor")));
			this.tbAutonomy.AutoSize = ((bool)(resources.GetObject("tbAutonomy.AutoSize")));
			this.tbAutonomy.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbAutonomy.BackgroundImage")));
			this.tbAutonomy.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbAutonomy.Dock")));
			this.tbAutonomy.Enabled = ((bool)(resources.GetObject("tbAutonomy.Enabled")));
			this.tbAutonomy.Font = ((System.Drawing.Font)(resources.GetObject("tbAutonomy.Font")));
			this.tbAutonomy.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbAutonomy.ImeMode")));
			this.tbAutonomy.Location = ((System.Drawing.Point)(resources.GetObject("tbAutonomy.Location")));
			this.tbAutonomy.MaxLength = ((int)(resources.GetObject("tbAutonomy.MaxLength")));
			this.tbAutonomy.Multiline = ((bool)(resources.GetObject("tbAutonomy.Multiline")));
			this.tbAutonomy.Name = "tbAutonomy";
			this.tbAutonomy.PasswordChar = ((char)(resources.GetObject("tbAutonomy.PasswordChar")));
			this.tbAutonomy.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbAutonomy.RightToLeft")));
			this.tbAutonomy.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbAutonomy.ScrollBars")));
			this.tbAutonomy.Size = ((System.Drawing.Size)(resources.GetObject("tbAutonomy.Size")));
			this.tbAutonomy.TabIndex = ((int)(resources.GetObject("tbAutonomy.TabIndex")));
			this.tbAutonomy.Text = resources.GetString("tbAutonomy.Text");
			this.tbAutonomy.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbAutonomy.TextAlign")));
			this.tbAutonomy.Visible = ((bool)(resources.GetObject("tbAutonomy.Visible")));
			this.tbAutonomy.WordWrap = ((bool)(resources.GetObject("tbAutonomy.WordWrap")));
			this.tbAutonomy.Validating += new System.ComponentModel.CancelEventHandler(this.hex32_Validating);
			this.tbAutonomy.Validated += new System.EventHandler(this.hex32_Validated);
			this.tbAutonomy.TextChanged += new System.EventHandler(this.hex32_TextChanged);
			// 
			// tbMemIterMult
			// 
			this.tbMemIterMult.AccessibleDescription = resources.GetString("tbMemIterMult.AccessibleDescription");
			this.tbMemIterMult.AccessibleName = resources.GetString("tbMemIterMult.AccessibleName");
			this.tbMemIterMult.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbMemIterMult.Anchor")));
			this.tbMemIterMult.AutoSize = ((bool)(resources.GetObject("tbMemIterMult.AutoSize")));
			this.tbMemIterMult.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbMemIterMult.BackgroundImage")));
			this.tbMemIterMult.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbMemIterMult.Dock")));
			this.tbMemIterMult.Enabled = ((bool)(resources.GetObject("tbMemIterMult.Enabled")));
			this.tbMemIterMult.Font = ((System.Drawing.Font)(resources.GetObject("tbMemIterMult.Font")));
			this.tbMemIterMult.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbMemIterMult.ImeMode")));
			this.tbMemIterMult.Location = ((System.Drawing.Point)(resources.GetObject("tbMemIterMult.Location")));
			this.tbMemIterMult.MaxLength = ((int)(resources.GetObject("tbMemIterMult.MaxLength")));
			this.tbMemIterMult.Multiline = ((bool)(resources.GetObject("tbMemIterMult.Multiline")));
			this.tbMemIterMult.Name = "tbMemIterMult";
			this.tbMemIterMult.PasswordChar = ((char)(resources.GetObject("tbMemIterMult.PasswordChar")));
			this.tbMemIterMult.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbMemIterMult.RightToLeft")));
			this.tbMemIterMult.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbMemIterMult.ScrollBars")));
			this.tbMemIterMult.Size = ((System.Drawing.Size)(resources.GetObject("tbMemIterMult.Size")));
			this.tbMemIterMult.TabIndex = ((int)(resources.GetObject("tbMemIterMult.TabIndex")));
			this.tbMemIterMult.Text = resources.GetString("tbMemIterMult.Text");
			this.tbMemIterMult.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbMemIterMult.TextAlign")));
			this.tbMemIterMult.Visible = ((bool)(resources.GetObject("tbMemIterMult.Visible")));
			this.tbMemIterMult.WordWrap = ((bool)(resources.GetObject("tbMemIterMult.WordWrap")));
			this.tbMemIterMult.Validating += new System.ComponentModel.CancelEventHandler(this.float_Validating);
			this.tbMemIterMult.Validated += new System.EventHandler(this.float_Validated);
			this.tbMemIterMult.TextChanged += new System.EventHandler(this.float_TextChanged);
			// 
			// label29
			// 
			this.label29.AccessibleDescription = resources.GetString("label29.AccessibleDescription");
			this.label29.AccessibleName = resources.GetString("label29.AccessibleName");
			this.label29.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label29.Anchor")));
			this.label29.AutoSize = ((bool)(resources.GetObject("label29.AutoSize")));
			this.label29.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label29.Dock")));
			this.label29.Enabled = ((bool)(resources.GetObject("label29.Enabled")));
			this.label29.Font = ((System.Drawing.Font)(resources.GetObject("label29.Font")));
			this.label29.Image = ((System.Drawing.Image)(resources.GetObject("label29.Image")));
			this.label29.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label29.ImageAlign")));
			this.label29.ImageIndex = ((int)(resources.GetObject("label29.ImageIndex")));
			this.label29.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label29.ImeMode")));
			this.label29.Location = ((System.Drawing.Point)(resources.GetObject("label29.Location")));
			this.label29.Name = "label29";
			this.label29.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label29.RightToLeft")));
			this.label29.Size = ((System.Drawing.Size)(resources.GetObject("label29.Size")));
			this.label29.TabIndex = ((int)(resources.GetObject("label29.TabIndex")));
			this.label29.Text = resources.GetString("label29.Text");
			this.label29.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label29.TextAlign")));
			this.label29.Visible = ((bool)(resources.GetObject("label29.Visible")));
			// 
			// tbFaceAnimID
			// 
			this.tbFaceAnimID.AccessibleDescription = resources.GetString("tbFaceAnimID.AccessibleDescription");
			this.tbFaceAnimID.AccessibleName = resources.GetString("tbFaceAnimID.AccessibleName");
			this.tbFaceAnimID.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbFaceAnimID.Anchor")));
			this.tbFaceAnimID.AutoSize = ((bool)(resources.GetObject("tbFaceAnimID.AutoSize")));
			this.tbFaceAnimID.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbFaceAnimID.BackgroundImage")));
			this.tbFaceAnimID.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbFaceAnimID.Dock")));
			this.tbFaceAnimID.Enabled = ((bool)(resources.GetObject("tbFaceAnimID.Enabled")));
			this.tbFaceAnimID.Font = ((System.Drawing.Font)(resources.GetObject("tbFaceAnimID.Font")));
			this.tbFaceAnimID.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbFaceAnimID.ImeMode")));
			this.tbFaceAnimID.Location = ((System.Drawing.Point)(resources.GetObject("tbFaceAnimID.Location")));
			this.tbFaceAnimID.MaxLength = ((int)(resources.GetObject("tbFaceAnimID.MaxLength")));
			this.tbFaceAnimID.Multiline = ((bool)(resources.GetObject("tbFaceAnimID.Multiline")));
			this.tbFaceAnimID.Name = "tbFaceAnimID";
			this.tbFaceAnimID.PasswordChar = ((char)(resources.GetObject("tbFaceAnimID.PasswordChar")));
			this.tbFaceAnimID.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbFaceAnimID.RightToLeft")));
			this.tbFaceAnimID.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbFaceAnimID.ScrollBars")));
			this.tbFaceAnimID.Size = ((System.Drawing.Size)(resources.GetObject("tbFaceAnimID.Size")));
			this.tbFaceAnimID.TabIndex = ((int)(resources.GetObject("tbFaceAnimID.TabIndex")));
			this.tbFaceAnimID.Text = resources.GetString("tbFaceAnimID.Text");
			this.tbFaceAnimID.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbFaceAnimID.TextAlign")));
			this.tbFaceAnimID.Visible = ((bool)(resources.GetObject("tbFaceAnimID.Visible")));
			this.tbFaceAnimID.WordWrap = ((bool)(resources.GetObject("tbFaceAnimID.WordWrap")));
			this.tbFaceAnimID.Validating += new System.ComponentModel.CancelEventHandler(this.hex32_Validating);
			this.tbFaceAnimID.Validated += new System.EventHandler(this.hex32_Validated);
			this.tbFaceAnimID.TextChanged += new System.EventHandler(this.hex32_TextChanged);
			// 
			// label30
			// 
			this.label30.AccessibleDescription = resources.GetString("label30.AccessibleDescription");
			this.label30.AccessibleName = resources.GetString("label30.AccessibleName");
			this.label30.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label30.Anchor")));
			this.label30.AutoSize = ((bool)(resources.GetObject("label30.AutoSize")));
			this.label30.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label30.Dock")));
			this.label30.Enabled = ((bool)(resources.GetObject("label30.Enabled")));
			this.label30.Font = ((System.Drawing.Font)(resources.GetObject("label30.Font")));
			this.label30.Image = ((System.Drawing.Image)(resources.GetObject("label30.Image")));
			this.label30.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label30.ImageAlign")));
			this.label30.ImageIndex = ((int)(resources.GetObject("label30.ImageIndex")));
			this.label30.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label30.ImeMode")));
			this.label30.Location = ((System.Drawing.Point)(resources.GetObject("label30.Location")));
			this.label30.Name = "label30";
			this.label30.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label30.RightToLeft")));
			this.label30.Size = ((System.Drawing.Size)(resources.GetObject("label30.Size")));
			this.label30.TabIndex = ((int)(resources.GetObject("label30.TabIndex")));
			this.label30.Text = resources.GetString("label30.Text");
			this.label30.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label30.TextAlign")));
			this.label30.Visible = ((bool)(resources.GetObject("label30.Visible")));
			// 
			// tbAttenuationValue
			// 
			this.tbAttenuationValue.AccessibleDescription = resources.GetString("tbAttenuationValue.AccessibleDescription");
			this.tbAttenuationValue.AccessibleName = resources.GetString("tbAttenuationValue.AccessibleName");
			this.tbAttenuationValue.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbAttenuationValue.Anchor")));
			this.tbAttenuationValue.AutoSize = ((bool)(resources.GetObject("tbAttenuationValue.AutoSize")));
			this.tbAttenuationValue.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbAttenuationValue.BackgroundImage")));
			this.tbAttenuationValue.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbAttenuationValue.Dock")));
			this.tbAttenuationValue.Enabled = ((bool)(resources.GetObject("tbAttenuationValue.Enabled")));
			this.tbAttenuationValue.Font = ((System.Drawing.Font)(resources.GetObject("tbAttenuationValue.Font")));
			this.tbAttenuationValue.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbAttenuationValue.ImeMode")));
			this.tbAttenuationValue.Location = ((System.Drawing.Point)(resources.GetObject("tbAttenuationValue.Location")));
			this.tbAttenuationValue.MaxLength = ((int)(resources.GetObject("tbAttenuationValue.MaxLength")));
			this.tbAttenuationValue.Multiline = ((bool)(resources.GetObject("tbAttenuationValue.Multiline")));
			this.tbAttenuationValue.Name = "tbAttenuationValue";
			this.tbAttenuationValue.PasswordChar = ((char)(resources.GetObject("tbAttenuationValue.PasswordChar")));
			this.tbAttenuationValue.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbAttenuationValue.RightToLeft")));
			this.tbAttenuationValue.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbAttenuationValue.ScrollBars")));
			this.tbAttenuationValue.Size = ((System.Drawing.Size)(resources.GetObject("tbAttenuationValue.Size")));
			this.tbAttenuationValue.TabIndex = ((int)(resources.GetObject("tbAttenuationValue.TabIndex")));
			this.tbAttenuationValue.Text = resources.GetString("tbAttenuationValue.Text");
			this.tbAttenuationValue.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbAttenuationValue.TextAlign")));
			this.tbAttenuationValue.Visible = ((bool)(resources.GetObject("tbAttenuationValue.Visible")));
			this.tbAttenuationValue.WordWrap = ((bool)(resources.GetObject("tbAttenuationValue.WordWrap")));
			this.tbAttenuationValue.Validating += new System.ComponentModel.CancelEventHandler(this.float_Validating);
			this.tbAttenuationValue.Validated += new System.EventHandler(this.float_Validated);
			this.tbAttenuationValue.TextChanged += new System.EventHandler(this.float_TextChanged);
			// 
			// label31
			// 
			this.label31.AccessibleDescription = resources.GetString("label31.AccessibleDescription");
			this.label31.AccessibleName = resources.GetString("label31.AccessibleName");
			this.label31.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label31.Anchor")));
			this.label31.AutoSize = ((bool)(resources.GetObject("label31.AutoSize")));
			this.label31.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label31.Dock")));
			this.label31.Enabled = ((bool)(resources.GetObject("label31.Enabled")));
			this.label31.Font = ((System.Drawing.Font)(resources.GetObject("label31.Font")));
			this.label31.Image = ((System.Drawing.Image)(resources.GetObject("label31.Image")));
			this.label31.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label31.ImageAlign")));
			this.label31.ImageIndex = ((int)(resources.GetObject("label31.ImageIndex")));
			this.label31.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label31.ImeMode")));
			this.label31.Location = ((System.Drawing.Point)(resources.GetObject("label31.Location")));
			this.label31.Name = "label31";
			this.label31.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label31.RightToLeft")));
			this.label31.Size = ((System.Drawing.Size)(resources.GetObject("label31.Size")));
			this.label31.TabIndex = ((int)(resources.GetObject("label31.TabIndex")));
			this.label31.Text = resources.GetString("label31.Text");
			this.label31.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label31.TextAlign")));
			this.label31.Visible = ((bool)(resources.GetObject("label31.Visible")));
			// 
			// label32
			// 
			this.label32.AccessibleDescription = resources.GetString("label32.AccessibleDescription");
			this.label32.AccessibleName = resources.GetString("label32.AccessibleName");
			this.label32.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label32.Anchor")));
			this.label32.AutoSize = ((bool)(resources.GetObject("label32.AutoSize")));
			this.label32.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label32.Dock")));
			this.label32.Enabled = ((bool)(resources.GetObject("label32.Enabled")));
			this.label32.Font = ((System.Drawing.Font)(resources.GetObject("label32.Font")));
			this.label32.Image = ((System.Drawing.Image)(resources.GetObject("label32.Image")));
			this.label32.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label32.ImageAlign")));
			this.label32.ImageIndex = ((int)(resources.GetObject("label32.ImageIndex")));
			this.label32.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label32.ImeMode")));
			this.label32.Location = ((System.Drawing.Point)(resources.GetObject("label32.Location")));
			this.label32.Name = "label32";
			this.label32.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label32.RightToLeft")));
			this.label32.Size = ((System.Drawing.Size)(resources.GetObject("label32.Size")));
			this.label32.TabIndex = ((int)(resources.GetObject("label32.TabIndex")));
			this.label32.Text = resources.GetString("label32.Text");
			this.label32.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label32.TextAlign")));
			this.label32.Visible = ((bool)(resources.GetObject("label32.Visible")));
			// 
			// tbFlags2
			// 
			this.tbFlags2.AccessibleDescription = resources.GetString("tbFlags2.AccessibleDescription");
			this.tbFlags2.AccessibleName = resources.GetString("tbFlags2.AccessibleName");
			this.tbFlags2.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbFlags2.Anchor")));
			this.tbFlags2.AutoSize = ((bool)(resources.GetObject("tbFlags2.AutoSize")));
			this.tbFlags2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbFlags2.BackgroundImage")));
			this.tbFlags2.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbFlags2.Dock")));
			this.tbFlags2.Enabled = ((bool)(resources.GetObject("tbFlags2.Enabled")));
			this.tbFlags2.Font = ((System.Drawing.Font)(resources.GetObject("tbFlags2.Font")));
			this.tbFlags2.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbFlags2.ImeMode")));
			this.tbFlags2.Location = ((System.Drawing.Point)(resources.GetObject("tbFlags2.Location")));
			this.tbFlags2.MaxLength = ((int)(resources.GetObject("tbFlags2.MaxLength")));
			this.tbFlags2.Multiline = ((bool)(resources.GetObject("tbFlags2.Multiline")));
			this.tbFlags2.Name = "tbFlags2";
			this.tbFlags2.PasswordChar = ((char)(resources.GetObject("tbFlags2.PasswordChar")));
			this.tbFlags2.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbFlags2.RightToLeft")));
			this.tbFlags2.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbFlags2.ScrollBars")));
			this.tbFlags2.Size = ((System.Drawing.Size)(resources.GetObject("tbFlags2.Size")));
			this.tbFlags2.TabIndex = ((int)(resources.GetObject("tbFlags2.TabIndex")));
			this.tbFlags2.Text = resources.GetString("tbFlags2.Text");
			this.tbFlags2.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbFlags2.TextAlign")));
			this.tbFlags2.Visible = ((bool)(resources.GetObject("tbFlags2.Visible")));
			this.tbFlags2.WordWrap = ((bool)(resources.GetObject("tbFlags2.WordWrap")));
			this.tbFlags2.Validating += new System.ComponentModel.CancelEventHandler(this.hex16_Validating);
			this.tbFlags2.Validated += new System.EventHandler(this.hex16_Validated);
			this.tbFlags2.TextChanged += new System.EventHandler(this.hex16_TextChanged);
			// 
			// label20
			// 
			this.label20.AccessibleDescription = resources.GetString("label20.AccessibleDescription");
			this.label20.AccessibleName = resources.GetString("label20.AccessibleName");
			this.label20.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label20.Anchor")));
			this.label20.AutoSize = ((bool)(resources.GetObject("label20.AutoSize")));
			this.label20.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label20.Dock")));
			this.label20.Enabled = ((bool)(resources.GetObject("label20.Enabled")));
			this.label20.Font = ((System.Drawing.Font)(resources.GetObject("label20.Font")));
			this.label20.Image = ((System.Drawing.Image)(resources.GetObject("label20.Image")));
			this.label20.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label20.ImageAlign")));
			this.label20.ImageIndex = ((int)(resources.GetObject("label20.ImageIndex")));
			this.label20.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label20.ImeMode")));
			this.label20.Location = ((System.Drawing.Point)(resources.GetObject("label20.Location")));
			this.label20.Name = "label20";
			this.label20.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label20.RightToLeft")));
			this.label20.Size = ((System.Drawing.Size)(resources.GetObject("label20.Size")));
			this.label20.TabIndex = ((int)(resources.GetObject("label20.TabIndex")));
			this.label20.Text = resources.GetString("label20.Text");
			this.label20.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label20.TextAlign")));
			this.label20.Visible = ((bool)(resources.GetObject("label20.Visible")));
			// 
			// tbGuardian
			// 
			this.tbGuardian.AccessibleDescription = resources.GetString("tbGuardian.AccessibleDescription");
			this.tbGuardian.AccessibleName = resources.GetString("tbGuardian.AccessibleName");
			this.tbGuardian.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbGuardian.Anchor")));
			this.tbGuardian.AutoSize = ((bool)(resources.GetObject("tbGuardian.AutoSize")));
			this.tbGuardian.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbGuardian.BackgroundImage")));
			this.tbGuardian.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbGuardian.Dock")));
			this.tbGuardian.Enabled = ((bool)(resources.GetObject("tbGuardian.Enabled")));
			this.tbGuardian.Font = ((System.Drawing.Font)(resources.GetObject("tbGuardian.Font")));
			this.tbGuardian.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbGuardian.ImeMode")));
			this.tbGuardian.Location = ((System.Drawing.Point)(resources.GetObject("tbGuardian.Location")));
			this.tbGuardian.MaxLength = ((int)(resources.GetObject("tbGuardian.MaxLength")));
			this.tbGuardian.Multiline = ((bool)(resources.GetObject("tbGuardian.Multiline")));
			this.tbGuardian.Name = "tbGuardian";
			this.tbGuardian.PasswordChar = ((char)(resources.GetObject("tbGuardian.PasswordChar")));
			this.tbGuardian.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbGuardian.RightToLeft")));
			this.tbGuardian.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbGuardian.ScrollBars")));
			this.tbGuardian.Size = ((System.Drawing.Size)(resources.GetObject("tbGuardian.Size")));
			this.tbGuardian.TabIndex = ((int)(resources.GetObject("tbGuardian.TabIndex")));
			this.tbGuardian.Text = resources.GetString("tbGuardian.Text");
			this.tbGuardian.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbGuardian.TextAlign")));
			this.tbGuardian.Visible = ((bool)(resources.GetObject("tbGuardian.Visible")));
			this.tbGuardian.WordWrap = ((bool)(resources.GetObject("tbGuardian.WordWrap")));
			this.tbGuardian.Validating += new System.ComponentModel.CancelEventHandler(this.hex16_Validating);
			this.tbGuardian.Validated += new System.EventHandler(this.hex16_Validated);
			this.tbGuardian.TextChanged += new System.EventHandler(this.hex16_TextChanged);
			// 
			// gbFlags
			// 
			this.gbFlags.AccessibleDescription = resources.GetString("gbFlags.AccessibleDescription");
			this.gbFlags.AccessibleName = resources.GetString("gbFlags.AccessibleName");
			this.gbFlags.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("gbFlags.Anchor")));
			this.gbFlags.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("gbFlags.BackgroundImage")));
			this.gbFlags.Controls.Add(this.btnNoFlags);
			this.gbFlags.Controls.Add(this.tbFlags);
			this.gbFlags.Controls.Add(this.label24);
			this.gbFlags.Controls.Add(this.cbvisitor);
			this.gbFlags.Controls.Add(this.cbunk3);
			this.gbFlags.Controls.Add(this.cbunk4);
			this.gbFlags.Controls.Add(this.cbunk1);
			this.gbFlags.Controls.Add(this.cbunk2);
			this.gbFlags.Controls.Add(this.cbteens);
			this.gbFlags.Controls.Add(this.cbelders);
			this.gbFlags.Controls.Add(this.cbtodlers);
			this.gbFlags.Controls.Add(this.cbautofirst);
			this.gbFlags.Controls.Add(this.cbdebugmenu);
			this.gbFlags.Controls.Add(this.cbadults);
			this.gbFlags.Controls.Add(this.cbdemochild);
			this.gbFlags.Controls.Add(this.cbchildren);
			this.gbFlags.Controls.Add(this.cbconsecutive);
			this.gbFlags.Controls.Add(this.cbimmediately);
			this.gbFlags.Controls.Add(this.cbjoinable);
			this.gbFlags.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("gbFlags.Dock")));
			this.gbFlags.Enabled = ((bool)(resources.GetObject("gbFlags.Enabled")));
			this.gbFlags.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.gbFlags.Font = ((System.Drawing.Font)(resources.GetObject("gbFlags.Font")));
			this.gbFlags.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("gbFlags.ImeMode")));
			this.gbFlags.Location = ((System.Drawing.Point)(resources.GetObject("gbFlags.Location")));
			this.gbFlags.Name = "gbFlags";
			this.gbFlags.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("gbFlags.RightToLeft")));
			this.gbFlags.Size = ((System.Drawing.Size)(resources.GetObject("gbFlags.Size")));
			this.gbFlags.TabIndex = ((int)(resources.GetObject("gbFlags.TabIndex")));
			this.gbFlags.TabStop = false;
			this.gbFlags.Text = resources.GetString("gbFlags.Text");
			this.gbFlags.Visible = ((bool)(resources.GetObject("gbFlags.Visible")));
			// 
			// btnNoFlags
			// 
			this.btnNoFlags.AccessibleDescription = resources.GetString("btnNoFlags.AccessibleDescription");
			this.btnNoFlags.AccessibleName = resources.GetString("btnNoFlags.AccessibleName");
			this.btnNoFlags.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnNoFlags.Anchor")));
			this.btnNoFlags.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnNoFlags.BackgroundImage")));
			this.btnNoFlags.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnNoFlags.Dock")));
			this.btnNoFlags.Enabled = ((bool)(resources.GetObject("btnNoFlags.Enabled")));
			this.btnNoFlags.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnNoFlags.FlatStyle")));
			this.btnNoFlags.Font = ((System.Drawing.Font)(resources.GetObject("btnNoFlags.Font")));
			this.btnNoFlags.Image = ((System.Drawing.Image)(resources.GetObject("btnNoFlags.Image")));
			this.btnNoFlags.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnNoFlags.ImageAlign")));
			this.btnNoFlags.ImageIndex = ((int)(resources.GetObject("btnNoFlags.ImageIndex")));
			this.btnNoFlags.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnNoFlags.ImeMode")));
			this.btnNoFlags.Location = ((System.Drawing.Point)(resources.GetObject("btnNoFlags.Location")));
			this.btnNoFlags.Name = "btnNoFlags";
			this.btnNoFlags.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnNoFlags.RightToLeft")));
			this.btnNoFlags.Size = ((System.Drawing.Size)(resources.GetObject("btnNoFlags.Size")));
			this.btnNoFlags.TabIndex = ((int)(resources.GetObject("btnNoFlags.TabIndex")));
			this.btnNoFlags.Text = resources.GetString("btnNoFlags.Text");
			this.btnNoFlags.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnNoFlags.TextAlign")));
			this.btnNoFlags.Visible = ((bool)(resources.GetObject("btnNoFlags.Visible")));
			this.btnNoFlags.Click += new System.EventHandler(this.btnNoFlags_Click);
			// 
			// tbFlags
			// 
			this.tbFlags.AccessibleDescription = resources.GetString("tbFlags.AccessibleDescription");
			this.tbFlags.AccessibleName = resources.GetString("tbFlags.AccessibleName");
			this.tbFlags.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbFlags.Anchor")));
			this.tbFlags.AutoSize = ((bool)(resources.GetObject("tbFlags.AutoSize")));
			this.tbFlags.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbFlags.BackgroundImage")));
			this.tbFlags.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbFlags.Dock")));
			this.tbFlags.Enabled = ((bool)(resources.GetObject("tbFlags.Enabled")));
			this.tbFlags.Font = ((System.Drawing.Font)(resources.GetObject("tbFlags.Font")));
			this.tbFlags.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbFlags.ImeMode")));
			this.tbFlags.Location = ((System.Drawing.Point)(resources.GetObject("tbFlags.Location")));
			this.tbFlags.MaxLength = ((int)(resources.GetObject("tbFlags.MaxLength")));
			this.tbFlags.Multiline = ((bool)(resources.GetObject("tbFlags.Multiline")));
			this.tbFlags.Name = "tbFlags";
			this.tbFlags.PasswordChar = ((char)(resources.GetObject("tbFlags.PasswordChar")));
			this.tbFlags.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbFlags.RightToLeft")));
			this.tbFlags.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbFlags.ScrollBars")));
			this.tbFlags.Size = ((System.Drawing.Size)(resources.GetObject("tbFlags.Size")));
			this.tbFlags.TabIndex = ((int)(resources.GetObject("tbFlags.TabIndex")));
			this.tbFlags.Text = resources.GetString("tbFlags.Text");
			this.tbFlags.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbFlags.TextAlign")));
			this.tbFlags.Visible = ((bool)(resources.GetObject("tbFlags.Visible")));
			this.tbFlags.WordWrap = ((bool)(resources.GetObject("tbFlags.WordWrap")));
			this.tbFlags.Validating += new System.ComponentModel.CancelEventHandler(this.hex16_Validating);
			this.tbFlags.Validated += new System.EventHandler(this.hex16_Validated);
			this.tbFlags.TextChanged += new System.EventHandler(this.hex16_TextChanged);
			// 
			// label24
			// 
			this.label24.AccessibleDescription = resources.GetString("label24.AccessibleDescription");
			this.label24.AccessibleName = resources.GetString("label24.AccessibleName");
			this.label24.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label24.Anchor")));
			this.label24.AutoSize = ((bool)(resources.GetObject("label24.AutoSize")));
			this.label24.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label24.Dock")));
			this.label24.Enabled = ((bool)(resources.GetObject("label24.Enabled")));
			this.label24.Font = ((System.Drawing.Font)(resources.GetObject("label24.Font")));
			this.label24.Image = ((System.Drawing.Image)(resources.GetObject("label24.Image")));
			this.label24.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label24.ImageAlign")));
			this.label24.ImageIndex = ((int)(resources.GetObject("label24.ImageIndex")));
			this.label24.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label24.ImeMode")));
			this.label24.Location = ((System.Drawing.Point)(resources.GetObject("label24.Location")));
			this.label24.Name = "label24";
			this.label24.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label24.RightToLeft")));
			this.label24.Size = ((System.Drawing.Size)(resources.GetObject("label24.Size")));
			this.label24.TabIndex = ((int)(resources.GetObject("label24.TabIndex")));
			this.label24.Text = resources.GetString("label24.Text");
			this.label24.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label24.TextAlign")));
			this.label24.Visible = ((bool)(resources.GetObject("label24.Visible")));
			// 
			// cbvisitor
			// 
			this.cbvisitor.AccessibleDescription = resources.GetString("cbvisitor.AccessibleDescription");
			this.cbvisitor.AccessibleName = resources.GetString("cbvisitor.AccessibleName");
			this.cbvisitor.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cbvisitor.Anchor")));
			this.cbvisitor.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("cbvisitor.Appearance")));
			this.cbvisitor.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cbvisitor.BackgroundImage")));
			this.cbvisitor.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbvisitor.CheckAlign")));
			this.cbvisitor.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cbvisitor.Dock")));
			this.cbvisitor.Enabled = ((bool)(resources.GetObject("cbvisitor.Enabled")));
			this.cbvisitor.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("cbvisitor.FlatStyle")));
			this.cbvisitor.Font = ((System.Drawing.Font)(resources.GetObject("cbvisitor.Font")));
			this.cbvisitor.Image = ((System.Drawing.Image)(resources.GetObject("cbvisitor.Image")));
			this.cbvisitor.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbvisitor.ImageAlign")));
			this.cbvisitor.ImageIndex = ((int)(resources.GetObject("cbvisitor.ImageIndex")));
			this.cbvisitor.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cbvisitor.ImeMode")));
			this.cbvisitor.Location = ((System.Drawing.Point)(resources.GetObject("cbvisitor.Location")));
			this.cbvisitor.Name = "cbvisitor";
			this.cbvisitor.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cbvisitor.RightToLeft")));
			this.cbvisitor.Size = ((System.Drawing.Size)(resources.GetObject("cbvisitor.Size")));
			this.cbvisitor.TabIndex = ((int)(resources.GetObject("cbvisitor.TabIndex")));
			this.cbvisitor.Text = resources.GetString("cbvisitor.Text");
			this.cbvisitor.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbvisitor.TextAlign")));
			this.cbvisitor.Visible = ((bool)(resources.GetObject("cbvisitor.Visible")));
			this.cbvisitor.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
			// 
			// cbunk3
			// 
			this.cbunk3.AccessibleDescription = resources.GetString("cbunk3.AccessibleDescription");
			this.cbunk3.AccessibleName = resources.GetString("cbunk3.AccessibleName");
			this.cbunk3.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cbunk3.Anchor")));
			this.cbunk3.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("cbunk3.Appearance")));
			this.cbunk3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cbunk3.BackgroundImage")));
			this.cbunk3.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbunk3.CheckAlign")));
			this.cbunk3.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cbunk3.Dock")));
			this.cbunk3.Enabled = ((bool)(resources.GetObject("cbunk3.Enabled")));
			this.cbunk3.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("cbunk3.FlatStyle")));
			this.cbunk3.Font = ((System.Drawing.Font)(resources.GetObject("cbunk3.Font")));
			this.cbunk3.Image = ((System.Drawing.Image)(resources.GetObject("cbunk3.Image")));
			this.cbunk3.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbunk3.ImageAlign")));
			this.cbunk3.ImageIndex = ((int)(resources.GetObject("cbunk3.ImageIndex")));
			this.cbunk3.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cbunk3.ImeMode")));
			this.cbunk3.Location = ((System.Drawing.Point)(resources.GetObject("cbunk3.Location")));
			this.cbunk3.Name = "cbunk3";
			this.cbunk3.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cbunk3.RightToLeft")));
			this.cbunk3.Size = ((System.Drawing.Size)(resources.GetObject("cbunk3.Size")));
			this.cbunk3.TabIndex = ((int)(resources.GetObject("cbunk3.TabIndex")));
			this.cbunk3.Text = resources.GetString("cbunk3.Text");
			this.cbunk3.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbunk3.TextAlign")));
			this.cbunk3.Visible = ((bool)(resources.GetObject("cbunk3.Visible")));
			this.cbunk3.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
			// 
			// cbunk4
			// 
			this.cbunk4.AccessibleDescription = resources.GetString("cbunk4.AccessibleDescription");
			this.cbunk4.AccessibleName = resources.GetString("cbunk4.AccessibleName");
			this.cbunk4.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cbunk4.Anchor")));
			this.cbunk4.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("cbunk4.Appearance")));
			this.cbunk4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cbunk4.BackgroundImage")));
			this.cbunk4.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbunk4.CheckAlign")));
			this.cbunk4.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cbunk4.Dock")));
			this.cbunk4.Enabled = ((bool)(resources.GetObject("cbunk4.Enabled")));
			this.cbunk4.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("cbunk4.FlatStyle")));
			this.cbunk4.Font = ((System.Drawing.Font)(resources.GetObject("cbunk4.Font")));
			this.cbunk4.Image = ((System.Drawing.Image)(resources.GetObject("cbunk4.Image")));
			this.cbunk4.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbunk4.ImageAlign")));
			this.cbunk4.ImageIndex = ((int)(resources.GetObject("cbunk4.ImageIndex")));
			this.cbunk4.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cbunk4.ImeMode")));
			this.cbunk4.Location = ((System.Drawing.Point)(resources.GetObject("cbunk4.Location")));
			this.cbunk4.Name = "cbunk4";
			this.cbunk4.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cbunk4.RightToLeft")));
			this.cbunk4.Size = ((System.Drawing.Size)(resources.GetObject("cbunk4.Size")));
			this.cbunk4.TabIndex = ((int)(resources.GetObject("cbunk4.TabIndex")));
			this.cbunk4.Text = resources.GetString("cbunk4.Text");
			this.cbunk4.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbunk4.TextAlign")));
			this.cbunk4.Visible = ((bool)(resources.GetObject("cbunk4.Visible")));
			this.cbunk4.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
			// 
			// cbunk1
			// 
			this.cbunk1.AccessibleDescription = resources.GetString("cbunk1.AccessibleDescription");
			this.cbunk1.AccessibleName = resources.GetString("cbunk1.AccessibleName");
			this.cbunk1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cbunk1.Anchor")));
			this.cbunk1.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("cbunk1.Appearance")));
			this.cbunk1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cbunk1.BackgroundImage")));
			this.cbunk1.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbunk1.CheckAlign")));
			this.cbunk1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cbunk1.Dock")));
			this.cbunk1.Enabled = ((bool)(resources.GetObject("cbunk1.Enabled")));
			this.cbunk1.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("cbunk1.FlatStyle")));
			this.cbunk1.Font = ((System.Drawing.Font)(resources.GetObject("cbunk1.Font")));
			this.cbunk1.Image = ((System.Drawing.Image)(resources.GetObject("cbunk1.Image")));
			this.cbunk1.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbunk1.ImageAlign")));
			this.cbunk1.ImageIndex = ((int)(resources.GetObject("cbunk1.ImageIndex")));
			this.cbunk1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cbunk1.ImeMode")));
			this.cbunk1.Location = ((System.Drawing.Point)(resources.GetObject("cbunk1.Location")));
			this.cbunk1.Name = "cbunk1";
			this.cbunk1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cbunk1.RightToLeft")));
			this.cbunk1.Size = ((System.Drawing.Size)(resources.GetObject("cbunk1.Size")));
			this.cbunk1.TabIndex = ((int)(resources.GetObject("cbunk1.TabIndex")));
			this.cbunk1.Text = resources.GetString("cbunk1.Text");
			this.cbunk1.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbunk1.TextAlign")));
			this.cbunk1.Visible = ((bool)(resources.GetObject("cbunk1.Visible")));
			this.cbunk1.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
			// 
			// cbunk2
			// 
			this.cbunk2.AccessibleDescription = resources.GetString("cbunk2.AccessibleDescription");
			this.cbunk2.AccessibleName = resources.GetString("cbunk2.AccessibleName");
			this.cbunk2.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cbunk2.Anchor")));
			this.cbunk2.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("cbunk2.Appearance")));
			this.cbunk2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cbunk2.BackgroundImage")));
			this.cbunk2.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbunk2.CheckAlign")));
			this.cbunk2.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cbunk2.Dock")));
			this.cbunk2.Enabled = ((bool)(resources.GetObject("cbunk2.Enabled")));
			this.cbunk2.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("cbunk2.FlatStyle")));
			this.cbunk2.Font = ((System.Drawing.Font)(resources.GetObject("cbunk2.Font")));
			this.cbunk2.Image = ((System.Drawing.Image)(resources.GetObject("cbunk2.Image")));
			this.cbunk2.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbunk2.ImageAlign")));
			this.cbunk2.ImageIndex = ((int)(resources.GetObject("cbunk2.ImageIndex")));
			this.cbunk2.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cbunk2.ImeMode")));
			this.cbunk2.Location = ((System.Drawing.Point)(resources.GetObject("cbunk2.Location")));
			this.cbunk2.Name = "cbunk2";
			this.cbunk2.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cbunk2.RightToLeft")));
			this.cbunk2.Size = ((System.Drawing.Size)(resources.GetObject("cbunk2.Size")));
			this.cbunk2.TabIndex = ((int)(resources.GetObject("cbunk2.TabIndex")));
			this.cbunk2.Text = resources.GetString("cbunk2.Text");
			this.cbunk2.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbunk2.TextAlign")));
			this.cbunk2.Visible = ((bool)(resources.GetObject("cbunk2.Visible")));
			this.cbunk2.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
			// 
			// cbteens
			// 
			this.cbteens.AccessibleDescription = resources.GetString("cbteens.AccessibleDescription");
			this.cbteens.AccessibleName = resources.GetString("cbteens.AccessibleName");
			this.cbteens.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cbteens.Anchor")));
			this.cbteens.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("cbteens.Appearance")));
			this.cbteens.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cbteens.BackgroundImage")));
			this.cbteens.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbteens.CheckAlign")));
			this.cbteens.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cbteens.Dock")));
			this.cbteens.Enabled = ((bool)(resources.GetObject("cbteens.Enabled")));
			this.cbteens.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("cbteens.FlatStyle")));
			this.cbteens.Font = ((System.Drawing.Font)(resources.GetObject("cbteens.Font")));
			this.cbteens.Image = ((System.Drawing.Image)(resources.GetObject("cbteens.Image")));
			this.cbteens.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbteens.ImageAlign")));
			this.cbteens.ImageIndex = ((int)(resources.GetObject("cbteens.ImageIndex")));
			this.cbteens.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cbteens.ImeMode")));
			this.cbteens.Location = ((System.Drawing.Point)(resources.GetObject("cbteens.Location")));
			this.cbteens.Name = "cbteens";
			this.cbteens.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cbteens.RightToLeft")));
			this.cbteens.Size = ((System.Drawing.Size)(resources.GetObject("cbteens.Size")));
			this.cbteens.TabIndex = ((int)(resources.GetObject("cbteens.TabIndex")));
			this.cbteens.Text = resources.GetString("cbteens.Text");
			this.cbteens.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbteens.TextAlign")));
			this.cbteens.Visible = ((bool)(resources.GetObject("cbteens.Visible")));
			this.cbteens.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
			// 
			// cbelders
			// 
			this.cbelders.AccessibleDescription = resources.GetString("cbelders.AccessibleDescription");
			this.cbelders.AccessibleName = resources.GetString("cbelders.AccessibleName");
			this.cbelders.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cbelders.Anchor")));
			this.cbelders.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("cbelders.Appearance")));
			this.cbelders.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cbelders.BackgroundImage")));
			this.cbelders.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbelders.CheckAlign")));
			this.cbelders.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cbelders.Dock")));
			this.cbelders.Enabled = ((bool)(resources.GetObject("cbelders.Enabled")));
			this.cbelders.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("cbelders.FlatStyle")));
			this.cbelders.Font = ((System.Drawing.Font)(resources.GetObject("cbelders.Font")));
			this.cbelders.Image = ((System.Drawing.Image)(resources.GetObject("cbelders.Image")));
			this.cbelders.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbelders.ImageAlign")));
			this.cbelders.ImageIndex = ((int)(resources.GetObject("cbelders.ImageIndex")));
			this.cbelders.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cbelders.ImeMode")));
			this.cbelders.Location = ((System.Drawing.Point)(resources.GetObject("cbelders.Location")));
			this.cbelders.Name = "cbelders";
			this.cbelders.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cbelders.RightToLeft")));
			this.cbelders.Size = ((System.Drawing.Size)(resources.GetObject("cbelders.Size")));
			this.cbelders.TabIndex = ((int)(resources.GetObject("cbelders.TabIndex")));
			this.cbelders.Text = resources.GetString("cbelders.Text");
			this.cbelders.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbelders.TextAlign")));
			this.cbelders.Visible = ((bool)(resources.GetObject("cbelders.Visible")));
			this.cbelders.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
			// 
			// cbtodlers
			// 
			this.cbtodlers.AccessibleDescription = resources.GetString("cbtodlers.AccessibleDescription");
			this.cbtodlers.AccessibleName = resources.GetString("cbtodlers.AccessibleName");
			this.cbtodlers.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cbtodlers.Anchor")));
			this.cbtodlers.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("cbtodlers.Appearance")));
			this.cbtodlers.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cbtodlers.BackgroundImage")));
			this.cbtodlers.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbtodlers.CheckAlign")));
			this.cbtodlers.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cbtodlers.Dock")));
			this.cbtodlers.Enabled = ((bool)(resources.GetObject("cbtodlers.Enabled")));
			this.cbtodlers.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("cbtodlers.FlatStyle")));
			this.cbtodlers.Font = ((System.Drawing.Font)(resources.GetObject("cbtodlers.Font")));
			this.cbtodlers.Image = ((System.Drawing.Image)(resources.GetObject("cbtodlers.Image")));
			this.cbtodlers.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbtodlers.ImageAlign")));
			this.cbtodlers.ImageIndex = ((int)(resources.GetObject("cbtodlers.ImageIndex")));
			this.cbtodlers.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cbtodlers.ImeMode")));
			this.cbtodlers.Location = ((System.Drawing.Point)(resources.GetObject("cbtodlers.Location")));
			this.cbtodlers.Name = "cbtodlers";
			this.cbtodlers.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cbtodlers.RightToLeft")));
			this.cbtodlers.Size = ((System.Drawing.Size)(resources.GetObject("cbtodlers.Size")));
			this.cbtodlers.TabIndex = ((int)(resources.GetObject("cbtodlers.TabIndex")));
			this.cbtodlers.Text = resources.GetString("cbtodlers.Text");
			this.cbtodlers.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbtodlers.TextAlign")));
			this.cbtodlers.Visible = ((bool)(resources.GetObject("cbtodlers.Visible")));
			this.cbtodlers.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
			// 
			// cbautofirst
			// 
			this.cbautofirst.AccessibleDescription = resources.GetString("cbautofirst.AccessibleDescription");
			this.cbautofirst.AccessibleName = resources.GetString("cbautofirst.AccessibleName");
			this.cbautofirst.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cbautofirst.Anchor")));
			this.cbautofirst.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("cbautofirst.Appearance")));
			this.cbautofirst.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cbautofirst.BackgroundImage")));
			this.cbautofirst.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbautofirst.CheckAlign")));
			this.cbautofirst.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cbautofirst.Dock")));
			this.cbautofirst.Enabled = ((bool)(resources.GetObject("cbautofirst.Enabled")));
			this.cbautofirst.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("cbautofirst.FlatStyle")));
			this.cbautofirst.Font = ((System.Drawing.Font)(resources.GetObject("cbautofirst.Font")));
			this.cbautofirst.Image = ((System.Drawing.Image)(resources.GetObject("cbautofirst.Image")));
			this.cbautofirst.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbautofirst.ImageAlign")));
			this.cbautofirst.ImageIndex = ((int)(resources.GetObject("cbautofirst.ImageIndex")));
			this.cbautofirst.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cbautofirst.ImeMode")));
			this.cbautofirst.Location = ((System.Drawing.Point)(resources.GetObject("cbautofirst.Location")));
			this.cbautofirst.Name = "cbautofirst";
			this.cbautofirst.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cbautofirst.RightToLeft")));
			this.cbautofirst.Size = ((System.Drawing.Size)(resources.GetObject("cbautofirst.Size")));
			this.cbautofirst.TabIndex = ((int)(resources.GetObject("cbautofirst.TabIndex")));
			this.cbautofirst.Text = resources.GetString("cbautofirst.Text");
			this.cbautofirst.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbautofirst.TextAlign")));
			this.cbautofirst.Visible = ((bool)(resources.GetObject("cbautofirst.Visible")));
			this.cbautofirst.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
			// 
			// cbdebugmenu
			// 
			this.cbdebugmenu.AccessibleDescription = resources.GetString("cbdebugmenu.AccessibleDescription");
			this.cbdebugmenu.AccessibleName = resources.GetString("cbdebugmenu.AccessibleName");
			this.cbdebugmenu.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cbdebugmenu.Anchor")));
			this.cbdebugmenu.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("cbdebugmenu.Appearance")));
			this.cbdebugmenu.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cbdebugmenu.BackgroundImage")));
			this.cbdebugmenu.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbdebugmenu.CheckAlign")));
			this.cbdebugmenu.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cbdebugmenu.Dock")));
			this.cbdebugmenu.Enabled = ((bool)(resources.GetObject("cbdebugmenu.Enabled")));
			this.cbdebugmenu.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("cbdebugmenu.FlatStyle")));
			this.cbdebugmenu.Font = ((System.Drawing.Font)(resources.GetObject("cbdebugmenu.Font")));
			this.cbdebugmenu.Image = ((System.Drawing.Image)(resources.GetObject("cbdebugmenu.Image")));
			this.cbdebugmenu.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbdebugmenu.ImageAlign")));
			this.cbdebugmenu.ImageIndex = ((int)(resources.GetObject("cbdebugmenu.ImageIndex")));
			this.cbdebugmenu.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cbdebugmenu.ImeMode")));
			this.cbdebugmenu.Location = ((System.Drawing.Point)(resources.GetObject("cbdebugmenu.Location")));
			this.cbdebugmenu.Name = "cbdebugmenu";
			this.cbdebugmenu.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cbdebugmenu.RightToLeft")));
			this.cbdebugmenu.Size = ((System.Drawing.Size)(resources.GetObject("cbdebugmenu.Size")));
			this.cbdebugmenu.TabIndex = ((int)(resources.GetObject("cbdebugmenu.TabIndex")));
			this.cbdebugmenu.Text = resources.GetString("cbdebugmenu.Text");
			this.cbdebugmenu.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbdebugmenu.TextAlign")));
			this.cbdebugmenu.Visible = ((bool)(resources.GetObject("cbdebugmenu.Visible")));
			this.cbdebugmenu.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
			// 
			// cbadults
			// 
			this.cbadults.AccessibleDescription = resources.GetString("cbadults.AccessibleDescription");
			this.cbadults.AccessibleName = resources.GetString("cbadults.AccessibleName");
			this.cbadults.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cbadults.Anchor")));
			this.cbadults.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("cbadults.Appearance")));
			this.cbadults.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cbadults.BackgroundImage")));
			this.cbadults.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbadults.CheckAlign")));
			this.cbadults.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cbadults.Dock")));
			this.cbadults.Enabled = ((bool)(resources.GetObject("cbadults.Enabled")));
			this.cbadults.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("cbadults.FlatStyle")));
			this.cbadults.Font = ((System.Drawing.Font)(resources.GetObject("cbadults.Font")));
			this.cbadults.Image = ((System.Drawing.Image)(resources.GetObject("cbadults.Image")));
			this.cbadults.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbadults.ImageAlign")));
			this.cbadults.ImageIndex = ((int)(resources.GetObject("cbadults.ImageIndex")));
			this.cbadults.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cbadults.ImeMode")));
			this.cbadults.Location = ((System.Drawing.Point)(resources.GetObject("cbadults.Location")));
			this.cbadults.Name = "cbadults";
			this.cbadults.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cbadults.RightToLeft")));
			this.cbadults.Size = ((System.Drawing.Size)(resources.GetObject("cbadults.Size")));
			this.cbadults.TabIndex = ((int)(resources.GetObject("cbadults.TabIndex")));
			this.cbadults.Text = resources.GetString("cbadults.Text");
			this.cbadults.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbadults.TextAlign")));
			this.cbadults.Visible = ((bool)(resources.GetObject("cbadults.Visible")));
			this.cbadults.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
			// 
			// cbdemochild
			// 
			this.cbdemochild.AccessibleDescription = resources.GetString("cbdemochild.AccessibleDescription");
			this.cbdemochild.AccessibleName = resources.GetString("cbdemochild.AccessibleName");
			this.cbdemochild.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cbdemochild.Anchor")));
			this.cbdemochild.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("cbdemochild.Appearance")));
			this.cbdemochild.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cbdemochild.BackgroundImage")));
			this.cbdemochild.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbdemochild.CheckAlign")));
			this.cbdemochild.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cbdemochild.Dock")));
			this.cbdemochild.Enabled = ((bool)(resources.GetObject("cbdemochild.Enabled")));
			this.cbdemochild.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("cbdemochild.FlatStyle")));
			this.cbdemochild.Font = ((System.Drawing.Font)(resources.GetObject("cbdemochild.Font")));
			this.cbdemochild.Image = ((System.Drawing.Image)(resources.GetObject("cbdemochild.Image")));
			this.cbdemochild.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbdemochild.ImageAlign")));
			this.cbdemochild.ImageIndex = ((int)(resources.GetObject("cbdemochild.ImageIndex")));
			this.cbdemochild.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cbdemochild.ImeMode")));
			this.cbdemochild.Location = ((System.Drawing.Point)(resources.GetObject("cbdemochild.Location")));
			this.cbdemochild.Name = "cbdemochild";
			this.cbdemochild.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cbdemochild.RightToLeft")));
			this.cbdemochild.Size = ((System.Drawing.Size)(resources.GetObject("cbdemochild.Size")));
			this.cbdemochild.TabIndex = ((int)(resources.GetObject("cbdemochild.TabIndex")));
			this.cbdemochild.Text = resources.GetString("cbdemochild.Text");
			this.cbdemochild.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbdemochild.TextAlign")));
			this.cbdemochild.Visible = ((bool)(resources.GetObject("cbdemochild.Visible")));
			this.cbdemochild.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
			// 
			// cbchildren
			// 
			this.cbchildren.AccessibleDescription = resources.GetString("cbchildren.AccessibleDescription");
			this.cbchildren.AccessibleName = resources.GetString("cbchildren.AccessibleName");
			this.cbchildren.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cbchildren.Anchor")));
			this.cbchildren.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("cbchildren.Appearance")));
			this.cbchildren.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cbchildren.BackgroundImage")));
			this.cbchildren.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbchildren.CheckAlign")));
			this.cbchildren.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cbchildren.Dock")));
			this.cbchildren.Enabled = ((bool)(resources.GetObject("cbchildren.Enabled")));
			this.cbchildren.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("cbchildren.FlatStyle")));
			this.cbchildren.Font = ((System.Drawing.Font)(resources.GetObject("cbchildren.Font")));
			this.cbchildren.Image = ((System.Drawing.Image)(resources.GetObject("cbchildren.Image")));
			this.cbchildren.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbchildren.ImageAlign")));
			this.cbchildren.ImageIndex = ((int)(resources.GetObject("cbchildren.ImageIndex")));
			this.cbchildren.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cbchildren.ImeMode")));
			this.cbchildren.Location = ((System.Drawing.Point)(resources.GetObject("cbchildren.Location")));
			this.cbchildren.Name = "cbchildren";
			this.cbchildren.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cbchildren.RightToLeft")));
			this.cbchildren.Size = ((System.Drawing.Size)(resources.GetObject("cbchildren.Size")));
			this.cbchildren.TabIndex = ((int)(resources.GetObject("cbchildren.TabIndex")));
			this.cbchildren.Text = resources.GetString("cbchildren.Text");
			this.cbchildren.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbchildren.TextAlign")));
			this.cbchildren.Visible = ((bool)(resources.GetObject("cbchildren.Visible")));
			this.cbchildren.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
			// 
			// cbconsecutive
			// 
			this.cbconsecutive.AccessibleDescription = resources.GetString("cbconsecutive.AccessibleDescription");
			this.cbconsecutive.AccessibleName = resources.GetString("cbconsecutive.AccessibleName");
			this.cbconsecutive.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cbconsecutive.Anchor")));
			this.cbconsecutive.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("cbconsecutive.Appearance")));
			this.cbconsecutive.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cbconsecutive.BackgroundImage")));
			this.cbconsecutive.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbconsecutive.CheckAlign")));
			this.cbconsecutive.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cbconsecutive.Dock")));
			this.cbconsecutive.Enabled = ((bool)(resources.GetObject("cbconsecutive.Enabled")));
			this.cbconsecutive.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("cbconsecutive.FlatStyle")));
			this.cbconsecutive.Font = ((System.Drawing.Font)(resources.GetObject("cbconsecutive.Font")));
			this.cbconsecutive.Image = ((System.Drawing.Image)(resources.GetObject("cbconsecutive.Image")));
			this.cbconsecutive.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbconsecutive.ImageAlign")));
			this.cbconsecutive.ImageIndex = ((int)(resources.GetObject("cbconsecutive.ImageIndex")));
			this.cbconsecutive.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cbconsecutive.ImeMode")));
			this.cbconsecutive.Location = ((System.Drawing.Point)(resources.GetObject("cbconsecutive.Location")));
			this.cbconsecutive.Name = "cbconsecutive";
			this.cbconsecutive.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cbconsecutive.RightToLeft")));
			this.cbconsecutive.Size = ((System.Drawing.Size)(resources.GetObject("cbconsecutive.Size")));
			this.cbconsecutive.TabIndex = ((int)(resources.GetObject("cbconsecutive.TabIndex")));
			this.cbconsecutive.Text = resources.GetString("cbconsecutive.Text");
			this.cbconsecutive.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbconsecutive.TextAlign")));
			this.cbconsecutive.Visible = ((bool)(resources.GetObject("cbconsecutive.Visible")));
			this.cbconsecutive.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
			// 
			// cbimmediately
			// 
			this.cbimmediately.AccessibleDescription = resources.GetString("cbimmediately.AccessibleDescription");
			this.cbimmediately.AccessibleName = resources.GetString("cbimmediately.AccessibleName");
			this.cbimmediately.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cbimmediately.Anchor")));
			this.cbimmediately.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("cbimmediately.Appearance")));
			this.cbimmediately.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cbimmediately.BackgroundImage")));
			this.cbimmediately.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbimmediately.CheckAlign")));
			this.cbimmediately.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cbimmediately.Dock")));
			this.cbimmediately.Enabled = ((bool)(resources.GetObject("cbimmediately.Enabled")));
			this.cbimmediately.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("cbimmediately.FlatStyle")));
			this.cbimmediately.Font = ((System.Drawing.Font)(resources.GetObject("cbimmediately.Font")));
			this.cbimmediately.Image = ((System.Drawing.Image)(resources.GetObject("cbimmediately.Image")));
			this.cbimmediately.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbimmediately.ImageAlign")));
			this.cbimmediately.ImageIndex = ((int)(resources.GetObject("cbimmediately.ImageIndex")));
			this.cbimmediately.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cbimmediately.ImeMode")));
			this.cbimmediately.Location = ((System.Drawing.Point)(resources.GetObject("cbimmediately.Location")));
			this.cbimmediately.Name = "cbimmediately";
			this.cbimmediately.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cbimmediately.RightToLeft")));
			this.cbimmediately.Size = ((System.Drawing.Size)(resources.GetObject("cbimmediately.Size")));
			this.cbimmediately.TabIndex = ((int)(resources.GetObject("cbimmediately.TabIndex")));
			this.cbimmediately.Text = resources.GetString("cbimmediately.Text");
			this.cbimmediately.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbimmediately.TextAlign")));
			this.cbimmediately.Visible = ((bool)(resources.GetObject("cbimmediately.Visible")));
			this.cbimmediately.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
			// 
			// cbjoinable
			// 
			this.cbjoinable.AccessibleDescription = resources.GetString("cbjoinable.AccessibleDescription");
			this.cbjoinable.AccessibleName = resources.GetString("cbjoinable.AccessibleName");
			this.cbjoinable.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cbjoinable.Anchor")));
			this.cbjoinable.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("cbjoinable.Appearance")));
			this.cbjoinable.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cbjoinable.BackgroundImage")));
			this.cbjoinable.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbjoinable.CheckAlign")));
			this.cbjoinable.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cbjoinable.Dock")));
			this.cbjoinable.Enabled = ((bool)(resources.GetObject("cbjoinable.Enabled")));
			this.cbjoinable.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("cbjoinable.FlatStyle")));
			this.cbjoinable.Font = ((System.Drawing.Font)(resources.GetObject("cbjoinable.Font")));
			this.cbjoinable.Image = ((System.Drawing.Image)(resources.GetObject("cbjoinable.Image")));
			this.cbjoinable.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbjoinable.ImageAlign")));
			this.cbjoinable.ImageIndex = ((int)(resources.GetObject("cbjoinable.ImageIndex")));
			this.cbjoinable.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cbjoinable.ImeMode")));
			this.cbjoinable.Location = ((System.Drawing.Point)(resources.GetObject("cbjoinable.Location")));
			this.cbjoinable.Name = "cbjoinable";
			this.cbjoinable.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cbjoinable.RightToLeft")));
			this.cbjoinable.Size = ((System.Drawing.Size)(resources.GetObject("cbjoinable.Size")));
			this.cbjoinable.TabIndex = ((int)(resources.GetObject("cbjoinable.TabIndex")));
			this.cbjoinable.Text = resources.GetString("cbjoinable.Text");
			this.cbjoinable.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbjoinable.TextAlign")));
			this.cbjoinable.Visible = ((bool)(resources.GetObject("cbjoinable.Visible")));
			this.cbjoinable.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
			// 
			// tbAction
			// 
			this.tbAction.AccessibleDescription = resources.GetString("tbAction.AccessibleDescription");
			this.tbAction.AccessibleName = resources.GetString("tbAction.AccessibleName");
			this.tbAction.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbAction.Anchor")));
			this.tbAction.AutoSize = ((bool)(resources.GetObject("tbAction.AutoSize")));
			this.tbAction.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbAction.BackgroundImage")));
			this.tbAction.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbAction.Dock")));
			this.tbAction.Enabled = ((bool)(resources.GetObject("tbAction.Enabled")));
			this.tbAction.Font = ((System.Drawing.Font)(resources.GetObject("tbAction.Font")));
			this.tbAction.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbAction.ImeMode")));
			this.tbAction.Location = ((System.Drawing.Point)(resources.GetObject("tbAction.Location")));
			this.tbAction.MaxLength = ((int)(resources.GetObject("tbAction.MaxLength")));
			this.tbAction.Multiline = ((bool)(resources.GetObject("tbAction.Multiline")));
			this.tbAction.Name = "tbAction";
			this.tbAction.PasswordChar = ((char)(resources.GetObject("tbAction.PasswordChar")));
			this.tbAction.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbAction.RightToLeft")));
			this.tbAction.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbAction.ScrollBars")));
			this.tbAction.Size = ((System.Drawing.Size)(resources.GetObject("tbAction.Size")));
			this.tbAction.TabIndex = ((int)(resources.GetObject("tbAction.TabIndex")));
			this.tbAction.Text = resources.GetString("tbAction.Text");
			this.tbAction.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbAction.TextAlign")));
			this.tbAction.Visible = ((bool)(resources.GetObject("tbAction.Visible")));
			this.tbAction.WordWrap = ((bool)(resources.GetObject("tbAction.WordWrap")));
			this.tbAction.Validating += new System.ComponentModel.CancelEventHandler(this.hex16_Validating);
			this.tbAction.Validated += new System.EventHandler(this.hex16_Validated);
			this.tbAction.TextChanged += new System.EventHandler(this.hex16_TextChanged);
			// 
			// label1
			// 
			this.label1.AccessibleDescription = resources.GetString("label1.AccessibleDescription");
			this.label1.AccessibleName = resources.GetString("label1.AccessibleName");
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label1.Anchor")));
			this.label1.AutoSize = ((bool)(resources.GetObject("label1.AutoSize")));
			this.label1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label1.Dock")));
			this.label1.Enabled = ((bool)(resources.GetObject("label1.Enabled")));
			this.label1.Font = ((System.Drawing.Font)(resources.GetObject("label1.Font")));
			this.label1.Image = ((System.Drawing.Image)(resources.GetObject("label1.Image")));
			this.label1.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label1.ImageAlign")));
			this.label1.ImageIndex = ((int)(resources.GetObject("label1.ImageIndex")));
			this.label1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label1.ImeMode")));
			this.label1.Location = ((System.Drawing.Point)(resources.GetObject("label1.Location")));
			this.label1.Name = "label1";
			this.label1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label1.RightToLeft")));
			this.label1.Size = ((System.Drawing.Size)(resources.GetObject("label1.Size")));
			this.label1.TabIndex = ((int)(resources.GetObject("label1.TabIndex")));
			this.label1.Text = resources.GetString("label1.Text");
			this.label1.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label1.TextAlign")));
			this.label1.Visible = ((bool)(resources.GetObject("label1.Visible")));
			// 
			// tbJoinIndex
			// 
			this.tbJoinIndex.AccessibleDescription = resources.GetString("tbJoinIndex.AccessibleDescription");
			this.tbJoinIndex.AccessibleName = resources.GetString("tbJoinIndex.AccessibleName");
			this.tbJoinIndex.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbJoinIndex.Anchor")));
			this.tbJoinIndex.AutoSize = ((bool)(resources.GetObject("tbJoinIndex.AutoSize")));
			this.tbJoinIndex.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbJoinIndex.BackgroundImage")));
			this.tbJoinIndex.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbJoinIndex.Dock")));
			this.tbJoinIndex.Enabled = ((bool)(resources.GetObject("tbJoinIndex.Enabled")));
			this.tbJoinIndex.Font = ((System.Drawing.Font)(resources.GetObject("tbJoinIndex.Font")));
			this.tbJoinIndex.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbJoinIndex.ImeMode")));
			this.tbJoinIndex.Location = ((System.Drawing.Point)(resources.GetObject("tbJoinIndex.Location")));
			this.tbJoinIndex.MaxLength = ((int)(resources.GetObject("tbJoinIndex.MaxLength")));
			this.tbJoinIndex.Multiline = ((bool)(resources.GetObject("tbJoinIndex.Multiline")));
			this.tbJoinIndex.Name = "tbJoinIndex";
			this.tbJoinIndex.PasswordChar = ((char)(resources.GetObject("tbJoinIndex.PasswordChar")));
			this.tbJoinIndex.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbJoinIndex.RightToLeft")));
			this.tbJoinIndex.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbJoinIndex.ScrollBars")));
			this.tbJoinIndex.Size = ((System.Drawing.Size)(resources.GetObject("tbJoinIndex.Size")));
			this.tbJoinIndex.TabIndex = ((int)(resources.GetObject("tbJoinIndex.TabIndex")));
			this.tbJoinIndex.Text = resources.GetString("tbJoinIndex.Text");
			this.tbJoinIndex.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbJoinIndex.TextAlign")));
			this.tbJoinIndex.Visible = ((bool)(resources.GetObject("tbJoinIndex.Visible")));
			this.tbJoinIndex.WordWrap = ((bool)(resources.GetObject("tbJoinIndex.WordWrap")));
			this.tbJoinIndex.Validating += new System.ComponentModel.CancelEventHandler(this.hex32_Validating);
			this.tbJoinIndex.Validated += new System.EventHandler(this.hex32_Validated);
			this.tbJoinIndex.TextChanged += new System.EventHandler(this.hex32_TextChanged);
			// 
			// label2
			// 
			this.label2.AccessibleDescription = resources.GetString("label2.AccessibleDescription");
			this.label2.AccessibleName = resources.GetString("label2.AccessibleName");
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label2.Anchor")));
			this.label2.AutoSize = ((bool)(resources.GetObject("label2.AutoSize")));
			this.label2.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label2.Dock")));
			this.label2.Enabled = ((bool)(resources.GetObject("label2.Enabled")));
			this.label2.Font = ((System.Drawing.Font)(resources.GetObject("label2.Font")));
			this.label2.Image = ((System.Drawing.Image)(resources.GetObject("label2.Image")));
			this.label2.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label2.ImageAlign")));
			this.label2.ImageIndex = ((int)(resources.GetObject("label2.ImageIndex")));
			this.label2.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label2.ImeMode")));
			this.label2.Location = ((System.Drawing.Point)(resources.GetObject("label2.Location")));
			this.label2.Name = "label2";
			this.label2.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label2.RightToLeft")));
			this.label2.Size = ((System.Drawing.Size)(resources.GetObject("label2.Size")));
			this.label2.TabIndex = ((int)(resources.GetObject("label2.TabIndex")));
			this.label2.Text = resources.GetString("label2.Text");
			this.label2.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label2.TextAlign")));
			this.label2.Visible = ((bool)(resources.GetObject("label2.Visible")));
			// 
			// tpMotives
			// 
			this.tpMotives.AccessibleDescription = resources.GetString("tpMotives.AccessibleDescription");
			this.tpMotives.AccessibleName = resources.GetString("tpMotives.AccessibleName");
			this.tpMotives.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tpMotives.Anchor")));
			this.tpMotives.AutoScroll = ((bool)(resources.GetObject("tpMotives.AutoScroll")));
			this.tpMotives.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("tpMotives.AutoScrollMargin")));
			this.tpMotives.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("tpMotives.AutoScrollMinSize")));
			this.tpMotives.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tpMotives.BackgroundImage")));
			this.tpMotives.Controls.Add(this.ttabItemMotiveTableUI1);
			this.tpMotives.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tpMotives.Dock")));
			this.tpMotives.Enabled = ((bool)(resources.GetObject("tpMotives.Enabled")));
			this.tpMotives.Font = ((System.Drawing.Font)(resources.GetObject("tpMotives.Font")));
			this.tpMotives.ImageIndex = ((int)(resources.GetObject("tpMotives.ImageIndex")));
			this.tpMotives.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tpMotives.ImeMode")));
			this.tpMotives.Location = ((System.Drawing.Point)(resources.GetObject("tpMotives.Location")));
			this.tpMotives.Name = "tpMotives";
			this.tpMotives.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tpMotives.RightToLeft")));
			this.tpMotives.Size = ((System.Drawing.Size)(resources.GetObject("tpMotives.Size")));
			this.tpMotives.TabIndex = ((int)(resources.GetObject("tpMotives.TabIndex")));
			this.tpMotives.Text = resources.GetString("tpMotives.Text");
			this.tpMotives.ToolTipText = resources.GetString("tpMotives.ToolTipText");
			this.tpMotives.Visible = ((bool)(resources.GetObject("tpMotives.Visible")));
			// 
			// ttabItemMotiveTableUI1
			// 
			this.ttabItemMotiveTableUI1.AccessibleDescription = resources.GetString("ttabItemMotiveTableUI1.AccessibleDescription");
			this.ttabItemMotiveTableUI1.AccessibleName = resources.GetString("ttabItemMotiveTableUI1.AccessibleName");
			this.ttabItemMotiveTableUI1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("ttabItemMotiveTableUI1.Anchor")));
			this.ttabItemMotiveTableUI1.AutoScroll = ((bool)(resources.GetObject("ttabItemMotiveTableUI1.AutoScroll")));
			this.ttabItemMotiveTableUI1.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("ttabItemMotiveTableUI1.AutoScrollMargin")));
			this.ttabItemMotiveTableUI1.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("ttabItemMotiveTableUI1.AutoScrollMinSize")));
			this.ttabItemMotiveTableUI1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ttabItemMotiveTableUI1.BackgroundImage")));
			this.ttabItemMotiveTableUI1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("ttabItemMotiveTableUI1.Dock")));
			this.ttabItemMotiveTableUI1.Enabled = ((bool)(resources.GetObject("ttabItemMotiveTableUI1.Enabled")));
			this.ttabItemMotiveTableUI1.Font = ((System.Drawing.Font)(resources.GetObject("ttabItemMotiveTableUI1.Font")));
			this.ttabItemMotiveTableUI1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("ttabItemMotiveTableUI1.ImeMode")));
			this.ttabItemMotiveTableUI1.Location = ((System.Drawing.Point)(resources.GetObject("ttabItemMotiveTableUI1.Location")));
			this.ttabItemMotiveTableUI1.Name = "ttabItemMotiveTableUI1";
			this.ttabItemMotiveTableUI1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("ttabItemMotiveTableUI1.RightToLeft")));
			this.ttabItemMotiveTableUI1.Size = ((System.Drawing.Size)(resources.GetObject("ttabItemMotiveTableUI1.Size")));
			this.ttabItemMotiveTableUI1.TabIndex = ((int)(resources.GetObject("ttabItemMotiveTableUI1.TabIndex")));
			this.ttabItemMotiveTableUI1.Visible = ((bool)(resources.GetObject("ttabItemMotiveTableUI1.Visible")));
			// 
			// panel5
			// 
			this.panel5.AccessibleDescription = resources.GetString("panel5.AccessibleDescription");
			this.panel5.AccessibleName = resources.GetString("panel5.AccessibleName");
			this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("panel5.Anchor")));
			this.panel5.AutoScroll = ((bool)(resources.GetObject("panel5.AutoScroll")));
			this.panel5.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("panel5.AutoScrollMargin")));
			this.panel5.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("panel5.AutoScrollMinSize")));
			this.panel5.BackColor = System.Drawing.SystemColors.AppWorkspace;
			this.panel5.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel5.BackgroundImage")));
			this.panel5.Controls.Add(this.btnHelp);
			this.panel5.Controls.Add(this.label25);
			this.panel5.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("panel5.Dock")));
			this.panel5.Enabled = ((bool)(resources.GetObject("panel5.Enabled")));
			this.panel5.Font = ((System.Drawing.Font)(resources.GetObject("panel5.Font")));
			this.panel5.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.panel5.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("panel5.ImeMode")));
			this.panel5.Location = ((System.Drawing.Point)(resources.GetObject("panel5.Location")));
			this.panel5.Name = "panel5";
			this.panel5.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("panel5.RightToLeft")));
			this.panel5.Size = ((System.Drawing.Size)(resources.GetObject("panel5.Size")));
			this.panel5.TabIndex = ((int)(resources.GetObject("panel5.TabIndex")));
			this.panel5.Text = resources.GetString("panel5.Text");
			this.panel5.Visible = ((bool)(resources.GetObject("panel5.Visible")));
			// 
			// btnHelp
			// 
			this.btnHelp.AccessibleDescription = resources.GetString("btnHelp.AccessibleDescription");
			this.btnHelp.AccessibleName = resources.GetString("btnHelp.AccessibleName");
			this.btnHelp.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnHelp.Anchor")));
			this.btnHelp.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnHelp.BackgroundImage")));
			this.btnHelp.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnHelp.Dock")));
			this.btnHelp.Enabled = ((bool)(resources.GetObject("btnHelp.Enabled")));
			this.btnHelp.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnHelp.FlatStyle")));
			this.btnHelp.Font = ((System.Drawing.Font)(resources.GetObject("btnHelp.Font")));
			this.btnHelp.Image = ((System.Drawing.Image)(resources.GetObject("btnHelp.Image")));
			this.btnHelp.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnHelp.ImageAlign")));
			this.btnHelp.ImageIndex = ((int)(resources.GetObject("btnHelp.ImageIndex")));
			this.btnHelp.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnHelp.ImeMode")));
			this.btnHelp.Location = ((System.Drawing.Point)(resources.GetObject("btnHelp.Location")));
			this.btnHelp.Name = "btnHelp";
			this.btnHelp.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnHelp.RightToLeft")));
			this.btnHelp.Size = ((System.Drawing.Size)(resources.GetObject("btnHelp.Size")));
			this.btnHelp.TabIndex = ((int)(resources.GetObject("btnHelp.TabIndex")));
			this.btnHelp.Text = resources.GetString("btnHelp.Text");
			this.btnHelp.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnHelp.TextAlign")));
			this.btnHelp.Visible = ((bool)(resources.GetObject("btnHelp.Visible")));
			this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
			// 
			// label25
			// 
			this.label25.AccessibleDescription = resources.GetString("label25.AccessibleDescription");
			this.label25.AccessibleName = resources.GetString("label25.AccessibleName");
			this.label25.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label25.Anchor")));
			this.label25.AutoSize = ((bool)(resources.GetObject("label25.AutoSize")));
			this.label25.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label25.Dock")));
			this.label25.Enabled = ((bool)(resources.GetObject("label25.Enabled")));
			this.label25.Font = ((System.Drawing.Font)(resources.GetObject("label25.Font")));
			this.label25.Image = ((System.Drawing.Image)(resources.GetObject("label25.Image")));
			this.label25.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label25.ImageAlign")));
			this.label25.ImageIndex = ((int)(resources.GetObject("label25.ImageIndex")));
			this.label25.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label25.ImeMode")));
			this.label25.Location = ((System.Drawing.Point)(resources.GetObject("label25.Location")));
			this.label25.Name = "label25";
			this.label25.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label25.RightToLeft")));
			this.label25.Size = ((System.Drawing.Size)(resources.GetObject("label25.Size")));
			this.label25.TabIndex = ((int)(resources.GetObject("label25.TabIndex")));
			this.label25.Text = resources.GetString("label25.Text");
			this.label25.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label25.TextAlign")));
			this.label25.Visible = ((bool)(resources.GetObject("label25.Visible")));
			// 
			// TtabForm
			// 
			this.AccessibleDescription = resources.GetString("$this.AccessibleDescription");
			this.AccessibleName = resources.GetString("$this.AccessibleName");
			this.AutoScaleBaseSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScaleBaseSize")));
			this.AutoScroll = ((bool)(resources.GetObject("$this.AutoScroll")));
			this.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMargin")));
			this.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMinSize")));
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.ClientSize = ((System.Drawing.Size)(resources.GetObject("$this.ClientSize")));
			this.Controls.Add(this.ttabPanel);
			this.Enabled = ((bool)(resources.GetObject("$this.Enabled")));
			this.Font = ((System.Drawing.Font)(resources.GetObject("$this.Font")));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("$this.ImeMode")));
			this.Location = ((System.Drawing.Point)(resources.GetObject("$this.Location")));
			this.MaximumSize = ((System.Drawing.Size)(resources.GetObject("$this.MaximumSize")));
			this.MinimumSize = ((System.Drawing.Size)(resources.GetObject("$this.MinimumSize")));
			this.Name = "TtabForm";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.StartPosition = ((System.Windows.Forms.FormStartPosition)(resources.GetObject("$this.StartPosition")));
			this.Text = resources.GetString("$this.Text");
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.ttabPanel.ResumeLayout(false);
			this.tabControl1.ResumeLayout(false);
			this.tpSettings.ResumeLayout(false);
			this.gbFlags.ResumeLayout(false);
			this.tpMotives.ResumeLayout(false);
			this.panel5.ResumeLayout(false);
			this.ResumeLayout(false);

		}
	
		#endregion

		private void TtabSelect(object sender, System.EventArgs e)
		{
			if (this.internalchg) return;

			this.btnDelete.Enabled = false;
			if (lbttab.SelectedIndex >= 0)
			{
				currentItem = wrapper[lbttab.SelectedIndex];
				origItem = currentItem.Clone();

				internalchg = true;

				btnDelete.Enabled = true;

				setStringIndex(currentItem.StringIndex, true, true);

				setBHAV(0, currentItem.Action, false);
				setBHAV(1, currentItem.Guardian, false);

				this.tbFlags.Text = "0x"+Helper.HexString(currentItem.Flags.Value);
				this.tbFlags2.Text = "0x"+Helper.HexString(currentItem.Flags2);
				if (currentItem.AttenuationCode < this.cbAttenuationCode.Items.Count)
				{
					cbAttenuationCode.SelectedIndex = (int)currentItem.AttenuationCode;
				}
				else
				{
					cbAttenuationCode.SelectedIndex = -1;
					cbAttenuationCode.Text = "0x"+Helper.HexString(currentItem.AttenuationCode);
				}
				tbAttenuationValue.Text = currentItem.AttenuationValue.ToString("N8");
				tbAutonomy.Text = "0x"+Helper.HexString(currentItem.Autonomy);
				tbJoinIndex.Text = "0x"+Helper.HexString(currentItem.JoinIndex);
				tbUIDispType.Text = "0x"+Helper.HexString(currentItem.UIDisplayType);
				tbFaceAnimID.Text = "0x"+Helper.HexString(currentItem.FacialAnimationID);
				tbMemIterMult.Text = currentItem.MemoryIterativeMultiplier.ToString("N8");
				tbObjType.Text = "0x"+Helper.HexString(currentItem.ObjectType);
				tbModelTabID.Text = "0x"+Helper.HexString(currentItem.ModelTableID);

				doFlags();

				this.ttabItemMotiveTableUI1.SetData(wrapper[lbttab.SelectedIndex]);
				this.tabControl1.Enabled = true;
				internalchg = false;
			}
			else
			{
				internalchg = true;
				cbAttenuationCode.SelectedIndex = -1;
				tbGuardian.Text = tbAction.Text = lbguard.Text = lbaction.Text = tbFlags.Text = tbFlags2.Text =
					tbStringIndex.Text = tbAttenuationValue.Text = tbAutonomy.Text = tbJoinIndex.Text =
					tbUIDispType.Text = tbFaceAnimID.Text = tbMemIterMult.Text = tbObjType.Text = tbModelTabID.Text = 
					"";
				for (int i = 0; i < alFlags.Count; i++) ((CheckBox)alFlags[i]).Checked = false;
				this.tabControl1.Enabled = false;
				internalchg = false;
			}
		}		


		private void llBhav_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			pjse.FileTable.Entry item = wrapper.ResourceByInstance(SimPe.Data.MetaData.BHAV_FILE, (sender == llAction) ? currentItem.Action : currentItem.Guardian);
			Bhav b = new Bhav();
			b.ProcessData(item.PFD, item.Package);

			BhavForm ui = (BhavForm)b.UIHandler;
			ui.Tag = "Popup"; // tells the SetReadOnly function it's in a popup - so everything locked down
			ui.Text = "View BHAV: " + b.FileName + " [" + b.Package.SaveFileName + "]";
			b.RefreshUI();
			ui.Show();
		}

		private void btnCommit_Click(object sender, System.EventArgs e)
		{
			try 
			{
				wrapper.SynchronizeUserData();
				btnCommit.Enabled = wrapper.Changed;
				TtabSelect(null, null);
			} 
			catch (Exception ex) 
			{
				Helper.ExceptionMessage(Localization.Manager.GetString("errwritingfile"), ex);
			}			
		}

		private void btnHelp_Click(object sender, System.EventArgs e)
		{
			pjse.HelpHelper.Help("PieMenus");
		}


		private void btnAdd_Click(object sender, System.EventArgs e)
		{
			int i = wrapper.Add((lbttab.SelectedIndex == -1) ? new TtabItem(wrapper) : wrapper[lbttab.SelectedIndex].Clone());
			if (i < 0) return;

			lbttab.Items.Add(wrapper[i]);
			lbttab.SelectedIndex = i;
		}

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			if (lbttab.SelectedIndex < 0) return;

			int i = lbttab.SelectedIndex;

			lbttab.Items.RemoveAt(i);
			wrapper.RemoveAt(i);

			if (i >= lbttab.Items.Count)
				i = lbttab.Items.Count - 1;
			lbttab.SelectedIndex = -1;
			lbttab.SelectedIndex = i;
		}

		private void btnAppend_Click(object sender, System.EventArgs e)
		{
			this.Append((new pjse.ResourceChooser()).Execute(wrapper.FileDescriptor.Type, wrapper.FileDescriptor.Group, ttabPanel));
		}

		private void btnNoFlags_Click(object sender, System.EventArgs e)
		{
			internalchg = true;
			currentItem.Flags.Value = (ushort)0x0070;
			this.tbFlags.Text = "0x"+Helper.HexString(currentItem.Flags.Value);
			doFlags();
			internalchg = false;
		}


		private void GetTTABGuard(object sender, System.EventArgs e)
		{
			pjse.FileTable.Entry item = new pjse.ResourceChooser().Execute(SimPe.Data.MetaData.BHAV_FILE, wrapper.FileDescriptor.Group, ttabPanel.Parent);
			if (item != null)
				setBHAV(1, (ushort)item.Instance, false);
		}

		private void GetTTABAction(object sender, System.EventArgs e)
		{
			pjse.FileTable.Entry item = new pjse.ResourceChooser().Execute(SimPe.Data.MetaData.BHAV_FILE, wrapper.FileDescriptor.Group, ttabPanel.Parent);
			if (item != null)
				setBHAV(0, (ushort)item.Instance, false);
		}


		private void tbFilename_TextChanged(object sender, System.EventArgs e)
		{
			wrapper.FileName = tbFilename.Text;
		}

		private void tbFilename_Validated(object sender, System.EventArgs e)
		{
			tbFilename.SelectAll();
		}


		private void cbHex32_Enter(object sender, System.EventArgs e)
		{
			((ComboBox)sender).SelectAll();
		}

		private void cbHex32_TextChanged(object sender, System.EventArgs ev)
		{
			if (internalchg) return;
			if (!cbHex32_IsValid(sender)) return;
			if (((ComboBox)sender).FindStringExact(((ComboBox)sender).Text) >= 0) return;

			uint val = Convert.ToUInt32(((ComboBox)sender).Text, 16);
			internalchg = true;
			switch (alHex32cb.IndexOf(sender))
			{
				case 0:
					currentItem.StringIndex = val;
					setStringIndex(val, true, false);
					lbttab.Items[lbttab.SelectedIndex] = currentItem;
					break;
				case 1: currentItem.AttenuationCode = val; break;
			}
			internalchg = false;
		}

		private void cbHex32_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (cbHex32_IsValid(sender)) return;

			e.Cancel = true;

			int i = alHex32cb.IndexOf(sender);
			if (i < 0)
				throw new Exception("cbHex32_Validating not applicable to control " + sender.ToString());

			uint val = 0;
			switch (i)
			{
				case 0: val = origItem.StringIndex; currentItem.StringIndex = val; break;
				case 1: val = origItem.AttenuationCode; currentItem.AttenuationCode = val; break;
			}

			bool origstate = internalchg;
			internalchg = true;
			if (i == 0)
			{
				setStringIndex(val, true, true);
				lbttab.Items[lbttab.SelectedIndex] = currentItem;
			}
			else if (i == 1)
			{
				if (val < ((ComboBox)sender).Items.Count)
				{
					((ComboBox)sender).SelectedIndex = (int)val;
				}
				else
				{
					((ComboBox)sender).SelectedIndex = -1;
					((ComboBox)sender).Text = "0x"+Helper.HexString(val);
				}
			}
			internalchg = origstate;
			((ComboBox)sender).SelectAll();
		}

		private void cbHex32_Validated(object sender, System.EventArgs e)
		{
			int i = alHex32cb.IndexOf(sender);
			if (i < 0)
				throw new Exception("cbHex32_Validated not applicable to control " + sender.ToString());
			if (((ComboBox)sender).FindStringExact(((ComboBox)sender).Text) >= 0) return;

			uint val = Convert.ToUInt32(((ComboBox)sender).Text, 16);

			bool origstate = internalchg;
			internalchg = true;
			if (i == 0)
			{
				setStringIndex(val, true, true);
			}
			else if (i == 1)
			{
				if (val < ((ComboBox)sender).Items.Count)
				{
					((ComboBox)sender).SelectedIndex = (int)val;
				}
				else
				{
					((ComboBox)sender).SelectedIndex = -1;
					((ComboBox)sender).Text = "0x"+Helper.HexString(val);
				}
			}
			internalchg = origstate;
			((ComboBox)sender).Select(0, 0);
		}

		private void cbHex32_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (internalchg) return;

			int i = alHex32cb.IndexOf(sender);
			if (i < 0)
				throw new Exception("cbHex32_SelectedIndexChanged not applicable to control " + sender.ToString());
			if (((ComboBox)sender).SelectedIndex == -1) return;

			internalchg = true;
			if (i == 0)
			{
				currentItem.StringIndex = (uint)((ComboBox)sender).SelectedIndex;
				setStringIndex(currentItem.StringIndex, true, false);
				lbttab.Items[lbttab.SelectedIndex] = currentItem;
				tbStringIndex.Focus();
			}
			else if (i == 1)
			{
				currentItem.AttenuationCode = (uint)((ComboBox)sender).SelectedIndex;
			}
			internalchg = false;

			((ComboBox)sender).SelectAll();
		}


		/*
		 * By way of reminder:
		 * action           - ushort - 4 hex digits (BHAV number)
		 * guard            - ushort - 4 hex digits (BHAV number)
		 * flags            - ushort - 4 hex digits
		 * flags2           - ushort - 4 hex digits
		 * strindex         - uint   - 8 hex digits
		 * attenuationcode  - uint   - 8 hex digits
		 * attenuationvalue - uint   - 8 hex digits
		 * autonomy         - uint   - 8 hex digits
		 * joinindex        - uint   - 8 hex digits
		 * uidisplaytype    - ushort - 4 hex digits
		 * facialanimation  - uint   - 8 hex digits
		 * memoryitermult   - float  - decimal digits and "."
		 * objecttype       - uint   - 8 hex digits
		 * modeltableid     - uint   - 8 hex digits
		 */



		private void hex16_TextChanged(object sender, System.EventArgs ev)
		{
			if (internalchg) return;
			if (!hex16_IsValid(sender)) return;

			ushort val = Convert.ToUInt16(((TextBox)sender).Text, 16);
			internalchg = true;
			switch (alHex16.IndexOf(sender))
			{
				case 0:
					currentItem.Action = val;
					setBHAV(0, val, true);
					break;
				case 1:
					currentItem.Guardian = val;
					setBHAV(1, val, true);
					break;
				case 2:
					currentItem.Flags.Value = val;
					doFlags();
					break;
				case 3: currentItem.Flags2 = val; break;
				case 4: currentItem.UIDisplayType = val; break;
			}
			internalchg = false;
		}

		private void hex16_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (hex16_IsValid(sender)) return;

			e.Cancel = true;

			internalchg = true;
			ushort val = 0;
			switch (alHex16.IndexOf(sender))
			{
				case 0:
					currentItem.Action = val = origItem.Action;
					setBHAV(0, val, true);
					break;
				case 1:
					currentItem.Guardian = val = origItem.Guardian;
					setBHAV(1, val, true);
					break;
				case 2:
					currentItem.Flags.Value = val = origItem.Flags.Value;
					doFlags();
					break;
				case 3: currentItem.Flags2 = val = origItem.Flags2; break;
				case 4: currentItem.UIDisplayType = val = origItem.UIDisplayType; break;
			}
			((TextBox)sender).Text = "0x" + Helper.HexString(val);
			((TextBox)sender).SelectAll();
			internalchg = false;
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
				case 0: wrapper.Format = val; break;
				case 1:
					currentItem.StringIndex = val;
					setStringIndex(val, false, true);
					lbttab.Items[lbttab.SelectedIndex] = currentItem;
					break;
				case 2: currentItem.Autonomy = val; break;
				case 3: currentItem.FacialAnimationID = val; break;
				case 4: currentItem.ObjectType = val; break;
				case 5: currentItem.ModelTableID = val; break;
				case 6: currentItem.JoinIndex = val; break;
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
				case 0: val = wrapper.Format; break;
				case 1:
					currentItem.StringIndex = val = origItem.StringIndex;
					lbttab.Items[lbttab.SelectedIndex] = currentItem;
					break;
				case 2: currentItem.Autonomy = val = origItem.Autonomy; break;
				case 3: currentItem.FacialAnimationID = val = origItem.FacialAnimationID; break;
				case 4: currentItem.ObjectType = val = origItem.ObjectType; break;
				case 5: currentItem.ModelTableID = val = origItem.ModelTableID; break;
				case 6: currentItem.JoinIndex = val = origItem.JoinIndex; break;
			}

			((TextBox)sender).Text = "0x" + Helper.HexString(val);
			((TextBox)sender).SelectAll();
			internalchg = false;
		}

		private void hex32_Validated(object sender, System.EventArgs e)
		{
			bool origstate = internalchg;
			internalchg = true;
			((TextBox)sender).Text = "0x" + Helper.HexString(Convert.ToUInt32(((TextBox)sender).Text, 16));
			((TextBox)sender).SelectAll();
			internalchg = origstate;
		}


		private void float_TextChanged(object sender, System.EventArgs ev)
		{
			if (internalchg) return;
			if (!float_IsValid(sender)) return;

			float val = Convert.ToSingle(((TextBox)sender).Text);
			internalchg = true;
			switch (alFloats.IndexOf(sender))
			{
				case 0: currentItem.AttenuationValue = val; break;
				case 1: currentItem.MemoryIterativeMultiplier = val; break;
			}
			internalchg = false;
		}

		private void float_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (float_IsValid(sender)) return;

			e.Cancel = true;

			internalchg = true;
			float val = 0.0f;
			switch (alFloats.IndexOf(sender))
			{
				case 0: currentItem.AttenuationValue = val = origItem.AttenuationValue; break;
				case 1: currentItem.MemoryIterativeMultiplier = val = origItem.MemoryIterativeMultiplier; break;
			}

			((TextBox)sender).Text = val.ToString("N8");
			((TextBox)sender).SelectAll();
			internalchg = false;
		}

		private void float_Validated(object sender, System.EventArgs e)
		{
			bool origstate = internalchg;
			internalchg = true;
			((TextBox)sender).Text = Convert.ToSingle(((TextBox)sender).Text).ToString("N8");
			((TextBox)sender).SelectAll();
			internalchg = origstate;
		}


		private void checkbox_CheckedChanged(object sender, System.EventArgs e)
		{
			if (internalchg) return;

			if (!(sender is CheckBox)) return;
			bool val = ((CheckBox)sender).Checked;

			int i = alFlags.IndexOf(sender);
			switch(i)
			{
				case  0: currentItem.Flags.ByVisitors = val; break;
				case  1: currentItem.Flags.Joinable = val; break;
				case  2: currentItem.Flags.RunImmediately = val; break;
				case  3: currentItem.Flags.AvailConsecutive = val; break;
				case  4: currentItem.Flags.ByChildren = val; break;
				case  5: currentItem.Flags.ByDemoChild = val; break;
				case  6: currentItem.Flags.ByAdults = val; break;
				case  7: currentItem.Flags.DebugMenu = val; break;
				case  8: currentItem.Flags.AutoFirstSelect = val; break;
				case  9: currentItem.Flags.ByToddlers = val; break;
				case 10: currentItem.Flags.ByElders = val; break;
				case 11: currentItem.Flags.ByTeens = val; break;
				case 12: currentItem.Flags.Unknown1 = val; break;
				case 13: currentItem.Flags.Unknown2 = val; break;
				case 14: currentItem.Flags.Unknown3 = val; break;
				case 15: currentItem.Flags.Unknown4 = val; break;
				default:
					throw new Exception("checkbox_CheckedChanged not applicable to control " + sender.ToString());
			}
			internalchg = true;
			this.tbFlags.Text = "0x"+Helper.HexString(currentItem.Flags.Value);
			internalchg = false;
		}

	}
}
