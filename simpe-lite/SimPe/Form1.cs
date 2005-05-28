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
using System.Data;
using SimPe.Data;
using System.Xml;
using SimPe.Interfaces.Plugin;
using SimPe.Interfaces.Plugin.Internal;
using SimPe.Interfaces;
using SimPe.Interfaces.Files;
using System.Diagnostics;
using System.IO;

namespace SimPe
{
	/// <summary>
	/// Zusammenfassung für Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListView fileList;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		/// <summary>
		/// Erforderliche Designervariable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.TabControl tabControl1;
		public System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.Panel panel2;
		Ambertation.Editors.HexEditor hex1;
		private System.Windows.Forms.ListView attList;
		private System.Windows.Forms.ColumnHeader columnHeader10;
		private System.Windows.Forms.ColumnHeader columnHeader11;
		private System.Windows.Forms.ListView holeList;
		private System.Windows.Forms.ColumnHeader columnHeader8;
		private System.Windows.Forms.ColumnHeader columnHeader9;
		private System.Windows.Forms.TabPage tabPage4;
		private System.Windows.Forms.TabPage tabPage5;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.CheckBox cbComp;
		private System.Windows.Forms.TextBox tbSize;
		private System.Windows.Forms.TextBox tbUncsize;
		Ambertation.Editors.HexEditor hex2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label lbHandler;
		private System.Windows.Forms.OpenFileDialog ofd;
		private System.Windows.Forms.ListBox lbtype;
		//private Skybound.VisualStyles.VisualStyleProvider vsp;
		private System.Windows.Forms.SaveFileDialog sfd;
		private System.Windows.Forms.Panel panel4;
		private System.Windows.Forms.Button btgoto;
		private System.Windows.Forms.TextBox tboffset;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.FolderBrowserDialog fbd;
		private System.Windows.Forms.ProgressBar pb1;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.MenuItem menuItem6;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.MenuItem miopen;
		private System.Windows.Forms.MenuItem miclose;
		private System.Windows.Forms.MenuItem miextract;
		private System.Windows.Forms.MenuItem miexit;
		private System.Windows.Forms.ContextMenu PackedFileMenu;
		private System.Windows.Forms.MenuItem pfmiextract;
		private System.Windows.Forms.MenuItem pfmireplace;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem3;
		SimPe.PackedFiles.TypeRegistry registry;
		private System.Windows.Forms.MenuItem mirecent;
		private System.Windows.Forms.MenuItem mifoldercompare;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.TextBox tbtype;
		private System.Windows.Forms.TextBox tbsubtype;
		private System.Windows.Forms.TextBox tbgroup;
		private System.Windows.Forms.TextBox tbinstance;
		//private Skybound.VisualStyles.VisualStyleLinkLabel llcommit;
		private System.Windows.Forms.ComboBox cbtypes;
		private System.Windows.Forms.MenuItem pfmidelete;
		private System.Windows.Forms.MenuItem pfmiadd;
		private System.Windows.Forms.Label lbcount;
		private System.Windows.Forms.MenuItem miadd;
		private System.Windows.Forms.MenuItem minew;
		private System.Windows.Forms.Label lbbyte;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label lbword;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.Label lbdword;
		private System.Windows.Forms.MenuItem misaveas;
		private System.Windows.Forms.MenuItem misave;
		private System.Windows.Forms.MenuItem menuItem5;
		private System.Windows.Forms.MenuItem mihexview;
		private System.Windows.Forms.Label lbHexView;
		private System.Windows.Forms.MenuItem minometa;
		private Skybound.VisualStyles.VisualStyleLinkLabel llcommit;
		private System.Windows.Forms.GroupBox gbtypes;
		private System.Windows.Forms.Panel pntypes;
		private System.Windows.Forms.MenuItem mioptions;
		private System.Windows.Forms.MenuItem miintrigued;
		private System.Windows.Forms.MenuItem mirunsims;
		private System.Windows.Forms.MenuItem milistsims;
		private System.Windows.Forms.MenuItem miinstplug;
		private System.Windows.Forms.MenuItem menuItem8;
		private System.Windows.Forms.Button btbyteview;
		private System.Windows.Forms.MenuItem midecode;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.ComboBox tbgr;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.TextBox tbin;
		private System.Windows.Forms.MenuItem mimem;
		private System.Windows.Forms.MenuItem mihexdec;
		private System.Windows.Forms.MenuItem menuItem7;
		private System.Windows.Forms.MenuItem miclone;
		private System.Windows.Forms.MenuItem menuItem9;
		private System.Windows.Forms.MenuItem mifilelist;
		private System.Windows.Forms.MenuItem mibuildlist;
		private System.Windows.Forms.MenuItem miPlugins;
		private System.Windows.Forms.MenuItem menuItem11;

		Registry reg;
		private Skybound.VisualStyles.VisualStyleLinkLabel llchggroup;	
		LoadFileWrappers wloader;
		private System.Windows.Forms.MenuItem mis2cpid;
		private System.Windows.Forms.MenuItem miss2cp;
		private System.Windows.Forms.MenuItem mifix;
		private Skybound.VisualStyles.VisualStyleLinkLabel llchginst;
		private System.Windows.Forms.MenuItem micopyright;
		private Skybound.VisualStyles.VisualStyleLinkLabel llopenext;
		private System.Windows.Forms.ComboBox cbext;
		private System.Windows.Forms.MenuItem minamemap;
		private System.Windows.Forms.Panel pnLower;
		private System.Windows.Forms.Panel pnTop;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.MenuItem menuItem12;
		private System.Windows.Forms.MenuItem mireload;
		private System.Windows.Forms.MenuItem menuItem10;
		private Skybound.VisualStyles.VisualStyleProvider visualStyleProvider1;
		private Skybound.VisualStyles.VisualStyleLinkLabel llexportraw;
		private Skybound.VisualStyles.VisualStyleLinkLabel llimpraw;
		private System.Windows.Forms.MenuItem miAbout;
		SimPe.Interfaces.Plugin.IFileWrapper currentwrapper = null;

		public Form1()
		{
			//
			// Erforderlich für die Windows Form-Designerunterstützung
			//
			InitializeComponent();


			hex1 = new Ambertation.Editors.HexEditor(panel1, null, 16);
			hex2 = new Ambertation.Editors.HexEditor(panel2, null, 16);
			
			registry = new SimPe.PackedFiles.TypeRegistry();	
			//register builtin Wrappers
			registry.Register(new SimPe.PackedFiles.Wrapper.Factory.ExtendedWrapperFactory());
			registry.Register(new SimPe.PackedFiles.Wrapper.Factory.DefaultWrapperFactory());
			registry.Register(new SimPe.PackedFiles.Wrapper.Factory.GenericWrapperFactory());
//			registry.Register(new SimPe.Plugin.WrapperFactory()); // SimPe Scenegraph
//			registry.Register(new SimPe.Plugin.RefFileFactory()); // SimPe 3IDR
			registry.Register(new SimPe.PackedFiles.Wrapper.Factory.ClstWrapperFactory());
			
			wloader = new LoadFileWrappers(registry, registry);
			wloader.Scan(System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath)+"\\Plugins\\");
			wloader.AddMenuItems(this.miPlugins, new EventHandler(ToolChangePacakge));

			sorter = new ColumnSorter();
			sorter.CurrentColumn = 0;

			FileTable.ProviderRegistry = registry;
			FileTable.ToolRegistry = registry;
			FileTable.WrapperRegistry = registry;

			reg = new Registry();

			//this.fileList.ListViewItemSorter = sorter;
		}

		/// <summary>
		/// Die verwendeten Ressourcen bereinigen.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Vom Windows Form-Designer generierter Code
		/// <summary>
		/// Erforderliche Methode für die Designerunterstützung. 
		/// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Form1));
			this.fileList = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
			this.PackedFileMenu = new System.Windows.Forms.ContextMenu();
			this.pfmiadd = new System.Windows.Forms.MenuItem();
			this.pfmiextract = new System.Windows.Forms.MenuItem();
			this.pfmireplace = new System.Windows.Forms.MenuItem();
			this.pfmidelete = new System.Windows.Forms.MenuItem();
			this.miclone = new System.Windows.Forms.MenuItem();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.attList = new System.Windows.Forms.ListView();
			this.columnHeader10 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader11 = new System.Windows.Forms.ColumnHeader();
			this.holeList = new System.Windows.Forms.ListView();
			this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader9 = new System.Windows.Forms.ColumnHeader();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.tabPage4 = new System.Windows.Forms.TabPage();
			this.cbext = new System.Windows.Forms.ComboBox();
			this.llopenext = new Skybound.VisualStyles.VisualStyleLinkLabel();
			this.btbyteview = new System.Windows.Forms.Button();
			this.gbtypes = new System.Windows.Forms.GroupBox();
			this.pntypes = new System.Windows.Forms.Panel();
			this.llchginst = new Skybound.VisualStyles.VisualStyleLinkLabel();
			this.llchggroup = new Skybound.VisualStyles.VisualStyleLinkLabel();
			this.tbsubtype = new System.Windows.Forms.TextBox();
			this.tbinstance = new System.Windows.Forms.TextBox();
			this.label11 = new System.Windows.Forms.Label();
			this.tbtype = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.tbgroup = new System.Windows.Forms.TextBox();
			this.cbtypes = new System.Windows.Forms.ComboBox();
			this.llcommit = new Skybound.VisualStyles.VisualStyleLinkLabel();
			this.lbHandler = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.tbUncsize = new System.Windows.Forms.TextBox();
			this.tbSize = new System.Windows.Forms.TextBox();
			this.cbComp = new System.Windows.Forms.CheckBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.lbHexView = new System.Windows.Forms.Label();
			this.panel4 = new System.Windows.Forms.Panel();
			this.label15 = new System.Windows.Forms.Label();
			this.lbdword = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.lbword = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.lbbyte = new System.Windows.Forms.Label();
			this.btgoto = new System.Windows.Forms.Button();
			this.tboffset = new System.Windows.Forms.TextBox();
			this.panel2 = new System.Windows.Forms.Panel();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.llimpraw = new Skybound.VisualStyles.VisualStyleLinkLabel();
			this.llexportraw = new Skybound.VisualStyles.VisualStyleLinkLabel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.tabPage5 = new System.Windows.Forms.TabPage();
			this.label4 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.lbcount = new System.Windows.Forms.Label();
			this.ofd = new System.Windows.Forms.OpenFileDialog();
			this.lbtype = new System.Windows.Forms.ListBox();
			this.sfd = new System.Windows.Forms.SaveFileDialog();
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.minew = new System.Windows.Forms.MenuItem();
			this.miopen = new System.Windows.Forms.MenuItem();
			this.misave = new System.Windows.Forms.MenuItem();
			this.misaveas = new System.Windows.Forms.MenuItem();
			this.miclose = new System.Windows.Forms.MenuItem();
			this.mirecent = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.miadd = new System.Windows.Forms.MenuItem();
			this.miextract = new System.Windows.Forms.MenuItem();
			this.miss2cp = new System.Windows.Forms.MenuItem();
			this.menuItem6 = new System.Windows.Forms.MenuItem();
			this.miexit = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.mifoldercompare = new System.Windows.Forms.MenuItem();
			this.mirunsims = new System.Windows.Forms.MenuItem();
			this.mihexdec = new System.Windows.Forms.MenuItem();
			this.menuItem7 = new System.Windows.Forms.MenuItem();
			this.miintrigued = new System.Windows.Forms.MenuItem();
			this.mifix = new System.Windows.Forms.MenuItem();
			this.mireload = new System.Windows.Forms.MenuItem();
			this.milistsims = new System.Windows.Forms.MenuItem();
			this.mimem = new System.Windows.Forms.MenuItem();
			this.menuItem9 = new System.Windows.Forms.MenuItem();
			this.mifilelist = new System.Windows.Forms.MenuItem();
			this.mibuildlist = new System.Windows.Forms.MenuItem();
			this.micopyright = new System.Windows.Forms.MenuItem();
			this.mis2cpid = new System.Windows.Forms.MenuItem();
			this.minamemap = new System.Windows.Forms.MenuItem();
			this.menuItem5 = new System.Windows.Forms.MenuItem();
			this.mihexview = new System.Windows.Forms.MenuItem();
			this.minometa = new System.Windows.Forms.MenuItem();
			this.midecode = new System.Windows.Forms.MenuItem();
			this.menuItem8 = new System.Windows.Forms.MenuItem();
			this.mioptions = new System.Windows.Forms.MenuItem();
			this.miPlugins = new System.Windows.Forms.MenuItem();
			this.miinstplug = new System.Windows.Forms.MenuItem();
			this.menuItem11 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.menuItem12 = new System.Windows.Forms.MenuItem();
			this.menuItem10 = new System.Windows.Forms.MenuItem();
			this.miAbout = new System.Windows.Forms.MenuItem();
			this.fbd = new System.Windows.Forms.FolderBrowserDialog();
			this.pb1 = new System.Windows.Forms.ProgressBar();
			this.label14 = new System.Windows.Forms.Label();
			this.tbgr = new System.Windows.Forms.ComboBox();
			this.label16 = new System.Windows.Forms.Label();
			this.tbin = new System.Windows.Forms.TextBox();
			this.pnLower = new System.Windows.Forms.Panel();
			this.pnTop = new System.Windows.Forms.Panel();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.visualStyleProvider1 = new Skybound.VisualStyles.VisualStyleProvider();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage4.SuspendLayout();
			this.gbtypes.SuspendLayout();
			this.pntypes.SuspendLayout();
			this.tabPage3.SuspendLayout();
			this.panel4.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.pnLower.SuspendLayout();
			this.pnTop.SuspendLayout();
			this.SuspendLayout();
			// 
			// fileList
			// 
			this.fileList.AccessibleDescription = resources.GetString("fileList.AccessibleDescription");
			this.fileList.AccessibleName = resources.GetString("fileList.AccessibleName");
			this.fileList.Alignment = ((System.Windows.Forms.ListViewAlignment)(resources.GetObject("fileList.Alignment")));
			this.fileList.AllowDrop = true;
			this.fileList.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("fileList.Anchor")));
			this.fileList.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("fileList.BackgroundImage")));
			this.fileList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																					   this.columnHeader1,
																					   this.columnHeader2,
																					   this.columnHeader3,
																					   this.columnHeader4,
																					   this.columnHeader5,
																					   this.columnHeader6});
			this.fileList.ContextMenu = this.PackedFileMenu;
			this.fileList.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("fileList.Dock")));
			this.fileList.Enabled = ((bool)(resources.GetObject("fileList.Enabled")));
			this.fileList.Font = ((System.Drawing.Font)(resources.GetObject("fileList.Font")));
			this.fileList.FullRowSelect = true;
			this.fileList.GridLines = true;
			this.fileList.HideSelection = false;
			this.fileList.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("fileList.ImeMode")));
			this.fileList.LabelWrap = ((bool)(resources.GetObject("fileList.LabelWrap")));
			this.fileList.Location = ((System.Drawing.Point)(resources.GetObject("fileList.Location")));
			this.fileList.Name = "fileList";
			this.fileList.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("fileList.RightToLeft")));
			this.fileList.Size = ((System.Drawing.Size)(resources.GetObject("fileList.Size")));
			this.fileList.TabIndex = ((int)(resources.GetObject("fileList.TabIndex")));
			this.fileList.Text = resources.GetString("fileList.Text");
			this.fileList.View = System.Windows.Forms.View.Details;
			this.fileList.Visible = ((bool)(resources.GetObject("fileList.Visible")));
			this.fileList.DragDrop += new System.Windows.Forms.DragEventHandler(this.DragDropFile);
			this.fileList.DragEnter += new System.Windows.Forms.DragEventHandler(this.DragEnterFile);
			this.fileList.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.SortFileListClick);
			this.fileList.SelectedIndexChanged += new System.EventHandler(this.ProcessPackedFile);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = resources.GetString("columnHeader1.Text");
			this.columnHeader1.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("columnHeader1.TextAlign")));
			this.columnHeader1.Width = ((int)(resources.GetObject("columnHeader1.Width")));
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = resources.GetString("columnHeader2.Text");
			this.columnHeader2.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("columnHeader2.TextAlign")));
			this.columnHeader2.Width = ((int)(resources.GetObject("columnHeader2.Width")));
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = resources.GetString("columnHeader3.Text");
			this.columnHeader3.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("columnHeader3.TextAlign")));
			this.columnHeader3.Width = ((int)(resources.GetObject("columnHeader3.Width")));
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = resources.GetString("columnHeader4.Text");
			this.columnHeader4.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("columnHeader4.TextAlign")));
			this.columnHeader4.Width = ((int)(resources.GetObject("columnHeader4.Width")));
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = resources.GetString("columnHeader5.Text");
			this.columnHeader5.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("columnHeader5.TextAlign")));
			this.columnHeader5.Width = ((int)(resources.GetObject("columnHeader5.Width")));
			// 
			// columnHeader6
			// 
			this.columnHeader6.Text = resources.GetString("columnHeader6.Text");
			this.columnHeader6.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("columnHeader6.TextAlign")));
			this.columnHeader6.Width = ((int)(resources.GetObject("columnHeader6.Width")));
			// 
			// PackedFileMenu
			// 
			this.PackedFileMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						   this.pfmiadd,
																						   this.pfmiextract,
																						   this.pfmireplace,
																						   this.pfmidelete,
																						   this.miclone});
			this.PackedFileMenu.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("PackedFileMenu.RightToLeft")));
			this.PackedFileMenu.Popup += new System.EventHandler(this.PackedFileMenu_Popup);
			// 
			// pfmiadd
			// 
			this.pfmiadd.Enabled = ((bool)(resources.GetObject("pfmiadd.Enabled")));
			this.pfmiadd.Index = 0;
			this.pfmiadd.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("pfmiadd.Shortcut")));
			this.pfmiadd.ShowShortcut = ((bool)(resources.GetObject("pfmiadd.ShowShortcut")));
			this.pfmiadd.Text = resources.GetString("pfmiadd.Text");
			this.pfmiadd.Visible = ((bool)(resources.GetObject("pfmiadd.Visible")));
			this.pfmiadd.Click += new System.EventHandler(this.AddPackedFileClick);
			// 
			// pfmiextract
			// 
			this.pfmiextract.Enabled = ((bool)(resources.GetObject("pfmiextract.Enabled")));
			this.pfmiextract.Index = 1;
			this.pfmiextract.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("pfmiextract.Shortcut")));
			this.pfmiextract.ShowShortcut = ((bool)(resources.GetObject("pfmiextract.ShowShortcut")));
			this.pfmiextract.Text = resources.GetString("pfmiextract.Text");
			this.pfmiextract.Visible = ((bool)(resources.GetObject("pfmiextract.Visible")));
			this.pfmiextract.Click += new System.EventHandler(this.SavePackedFileClick);
			// 
			// pfmireplace
			// 
			this.pfmireplace.Enabled = ((bool)(resources.GetObject("pfmireplace.Enabled")));
			this.pfmireplace.Index = 2;
			this.pfmireplace.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("pfmireplace.Shortcut")));
			this.pfmireplace.ShowShortcut = ((bool)(resources.GetObject("pfmireplace.ShowShortcut")));
			this.pfmireplace.Text = resources.GetString("pfmireplace.Text");
			this.pfmireplace.Visible = ((bool)(resources.GetObject("pfmireplace.Visible")));
			this.pfmireplace.Click += new System.EventHandler(this.OpenPackedFileClick);
			// 
			// pfmidelete
			// 
			this.pfmidelete.Enabled = ((bool)(resources.GetObject("pfmidelete.Enabled")));
			this.pfmidelete.Index = 3;
			this.pfmidelete.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("pfmidelete.Shortcut")));
			this.pfmidelete.ShowShortcut = ((bool)(resources.GetObject("pfmidelete.ShowShortcut")));
			this.pfmidelete.Text = resources.GetString("pfmidelete.Text");
			this.pfmidelete.Visible = ((bool)(resources.GetObject("pfmidelete.Visible")));
			this.pfmidelete.Click += new System.EventHandler(this.DeletePackedFileClick);
			// 
			// miclone
			// 
			this.miclone.Enabled = ((bool)(resources.GetObject("miclone.Enabled")));
			this.miclone.Index = 4;
			this.miclone.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("miclone.Shortcut")));
			this.miclone.ShowShortcut = ((bool)(resources.GetObject("miclone.ShowShortcut")));
			this.miclone.Text = resources.GetString("miclone.Text");
			this.miclone.Visible = ((bool)(resources.GetObject("miclone.Visible")));
			this.miclone.Click += new System.EventHandler(this.ClonePackedFile);
			// 
			// tabControl1
			// 
			this.tabControl1.AccessibleDescription = resources.GetString("tabControl1.AccessibleDescription");
			this.tabControl1.AccessibleName = resources.GetString("tabControl1.AccessibleName");
			this.tabControl1.Alignment = ((System.Windows.Forms.TabAlignment)(resources.GetObject("tabControl1.Alignment")));
			this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tabControl1.Anchor")));
			this.tabControl1.Appearance = ((System.Windows.Forms.TabAppearance)(resources.GetObject("tabControl1.Appearance")));
			this.tabControl1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tabControl1.BackgroundImage")));
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage4);
			this.tabControl1.Controls.Add(this.tabPage3);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Controls.Add(this.tabPage5);
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
			this.visualStyleProvider1.SetVisualStyleSupport(this.tabControl1, true);
			// 
			// tabPage1
			// 
			this.tabPage1.AccessibleDescription = resources.GetString("tabPage1.AccessibleDescription");
			this.tabPage1.AccessibleName = resources.GetString("tabPage1.AccessibleName");
			this.tabPage1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tabPage1.Anchor")));
			this.tabPage1.AutoScroll = ((bool)(resources.GetObject("tabPage1.AutoScroll")));
			this.tabPage1.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("tabPage1.AutoScrollMargin")));
			this.tabPage1.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("tabPage1.AutoScrollMinSize")));
			this.tabPage1.BackColor = System.Drawing.Color.Transparent;
			this.tabPage1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tabPage1.BackgroundImage")));
			this.tabPage1.Controls.Add(this.attList);
			this.tabPage1.Controls.Add(this.holeList);
			this.tabPage1.Controls.Add(this.label5);
			this.tabPage1.Controls.Add(this.label6);
			this.tabPage1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tabPage1.Dock")));
			this.tabPage1.Enabled = ((bool)(resources.GetObject("tabPage1.Enabled")));
			this.tabPage1.Font = ((System.Drawing.Font)(resources.GetObject("tabPage1.Font")));
			this.tabPage1.ImageIndex = ((int)(resources.GetObject("tabPage1.ImageIndex")));
			this.tabPage1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tabPage1.ImeMode")));
			this.tabPage1.Location = ((System.Drawing.Point)(resources.GetObject("tabPage1.Location")));
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tabPage1.RightToLeft")));
			this.tabPage1.Size = ((System.Drawing.Size)(resources.GetObject("tabPage1.Size")));
			this.tabPage1.TabIndex = ((int)(resources.GetObject("tabPage1.TabIndex")));
			this.tabPage1.Text = resources.GetString("tabPage1.Text");
			this.tabPage1.ToolTipText = resources.GetString("tabPage1.ToolTipText");
			this.tabPage1.Visible = ((bool)(resources.GetObject("tabPage1.Visible")));
			this.visualStyleProvider1.SetVisualStyleSupport(this.tabPage1, true);
			// 
			// attList
			// 
			this.attList.AccessibleDescription = resources.GetString("attList.AccessibleDescription");
			this.attList.AccessibleName = resources.GetString("attList.AccessibleName");
			this.attList.Alignment = ((System.Windows.Forms.ListViewAlignment)(resources.GetObject("attList.Alignment")));
			this.attList.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("attList.Anchor")));
			this.attList.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("attList.BackgroundImage")));
			this.attList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																					  this.columnHeader10,
																					  this.columnHeader11});
			this.attList.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("attList.Dock")));
			this.attList.Enabled = ((bool)(resources.GetObject("attList.Enabled")));
			this.attList.Font = ((System.Drawing.Font)(resources.GetObject("attList.Font")));
			this.attList.FullRowSelect = true;
			this.attList.GridLines = true;
			this.attList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.attList.HideSelection = false;
			this.attList.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("attList.ImeMode")));
			this.attList.LabelWrap = ((bool)(resources.GetObject("attList.LabelWrap")));
			this.attList.Location = ((System.Drawing.Point)(resources.GetObject("attList.Location")));
			this.attList.MultiSelect = false;
			this.attList.Name = "attList";
			this.attList.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("attList.RightToLeft")));
			this.attList.Size = ((System.Drawing.Size)(resources.GetObject("attList.Size")));
			this.attList.TabIndex = ((int)(resources.GetObject("attList.TabIndex")));
			this.attList.Text = resources.GetString("attList.Text");
			this.attList.View = System.Windows.Forms.View.Details;
			this.attList.Visible = ((bool)(resources.GetObject("attList.Visible")));
			// 
			// columnHeader10
			// 
			this.columnHeader10.Text = resources.GetString("columnHeader10.Text");
			this.columnHeader10.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("columnHeader10.TextAlign")));
			this.columnHeader10.Width = ((int)(resources.GetObject("columnHeader10.Width")));
			// 
			// columnHeader11
			// 
			this.columnHeader11.Text = resources.GetString("columnHeader11.Text");
			this.columnHeader11.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("columnHeader11.TextAlign")));
			this.columnHeader11.Width = ((int)(resources.GetObject("columnHeader11.Width")));
			// 
			// holeList
			// 
			this.holeList.AccessibleDescription = resources.GetString("holeList.AccessibleDescription");
			this.holeList.AccessibleName = resources.GetString("holeList.AccessibleName");
			this.holeList.Alignment = ((System.Windows.Forms.ListViewAlignment)(resources.GetObject("holeList.Alignment")));
			this.holeList.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("holeList.Anchor")));
			this.holeList.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("holeList.BackgroundImage")));
			this.holeList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																					   this.columnHeader8,
																					   this.columnHeader9});
			this.holeList.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("holeList.Dock")));
			this.holeList.Enabled = ((bool)(resources.GetObject("holeList.Enabled")));
			this.holeList.Font = ((System.Drawing.Font)(resources.GetObject("holeList.Font")));
			this.holeList.FullRowSelect = true;
			this.holeList.GridLines = true;
			this.holeList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.holeList.HideSelection = false;
			this.holeList.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("holeList.ImeMode")));
			this.holeList.LabelWrap = ((bool)(resources.GetObject("holeList.LabelWrap")));
			this.holeList.Location = ((System.Drawing.Point)(resources.GetObject("holeList.Location")));
			this.holeList.MultiSelect = false;
			this.holeList.Name = "holeList";
			this.holeList.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("holeList.RightToLeft")));
			this.holeList.Size = ((System.Drawing.Size)(resources.GetObject("holeList.Size")));
			this.holeList.TabIndex = ((int)(resources.GetObject("holeList.TabIndex")));
			this.holeList.Text = resources.GetString("holeList.Text");
			this.holeList.View = System.Windows.Forms.View.Details;
			this.holeList.Visible = ((bool)(resources.GetObject("holeList.Visible")));
			// 
			// columnHeader8
			// 
			this.columnHeader8.Text = resources.GetString("columnHeader8.Text");
			this.columnHeader8.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("columnHeader8.TextAlign")));
			this.columnHeader8.Width = ((int)(resources.GetObject("columnHeader8.Width")));
			// 
			// columnHeader9
			// 
			this.columnHeader9.Text = resources.GetString("columnHeader9.Text");
			this.columnHeader9.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("columnHeader9.TextAlign")));
			this.columnHeader9.Width = ((int)(resources.GetObject("columnHeader9.Width")));
			// 
			// label5
			// 
			this.label5.AccessibleDescription = resources.GetString("label5.AccessibleDescription");
			this.label5.AccessibleName = resources.GetString("label5.AccessibleName");
			this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label5.Anchor")));
			this.label5.AutoSize = ((bool)(resources.GetObject("label5.AutoSize")));
			this.label5.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label5.Dock")));
			this.label5.Enabled = ((bool)(resources.GetObject("label5.Enabled")));
			this.label5.Font = ((System.Drawing.Font)(resources.GetObject("label5.Font")));
			this.label5.Image = ((System.Drawing.Image)(resources.GetObject("label5.Image")));
			this.label5.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label5.ImageAlign")));
			this.label5.ImageIndex = ((int)(resources.GetObject("label5.ImageIndex")));
			this.label5.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label5.ImeMode")));
			this.label5.Location = ((System.Drawing.Point)(resources.GetObject("label5.Location")));
			this.label5.Name = "label5";
			this.label5.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label5.RightToLeft")));
			this.label5.Size = ((System.Drawing.Size)(resources.GetObject("label5.Size")));
			this.label5.TabIndex = ((int)(resources.GetObject("label5.TabIndex")));
			this.label5.Text = resources.GetString("label5.Text");
			this.label5.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label5.TextAlign")));
			this.label5.Visible = ((bool)(resources.GetObject("label5.Visible")));
			this.visualStyleProvider1.SetVisualStyleSupport(this.label5, true);
			// 
			// label6
			// 
			this.label6.AccessibleDescription = resources.GetString("label6.AccessibleDescription");
			this.label6.AccessibleName = resources.GetString("label6.AccessibleName");
			this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label6.Anchor")));
			this.label6.AutoSize = ((bool)(resources.GetObject("label6.AutoSize")));
			this.label6.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label6.Dock")));
			this.label6.Enabled = ((bool)(resources.GetObject("label6.Enabled")));
			this.label6.Font = ((System.Drawing.Font)(resources.GetObject("label6.Font")));
			this.label6.Image = ((System.Drawing.Image)(resources.GetObject("label6.Image")));
			this.label6.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label6.ImageAlign")));
			this.label6.ImageIndex = ((int)(resources.GetObject("label6.ImageIndex")));
			this.label6.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label6.ImeMode")));
			this.label6.Location = ((System.Drawing.Point)(resources.GetObject("label6.Location")));
			this.label6.Name = "label6";
			this.label6.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label6.RightToLeft")));
			this.label6.Size = ((System.Drawing.Size)(resources.GetObject("label6.Size")));
			this.label6.TabIndex = ((int)(resources.GetObject("label6.TabIndex")));
			this.label6.Text = resources.GetString("label6.Text");
			this.label6.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label6.TextAlign")));
			this.label6.Visible = ((bool)(resources.GetObject("label6.Visible")));
			this.visualStyleProvider1.SetVisualStyleSupport(this.label6, true);
			// 
			// tabPage4
			// 
			this.tabPage4.AccessibleDescription = resources.GetString("tabPage4.AccessibleDescription");
			this.tabPage4.AccessibleName = resources.GetString("tabPage4.AccessibleName");
			this.tabPage4.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tabPage4.Anchor")));
			this.tabPage4.AutoScroll = ((bool)(resources.GetObject("tabPage4.AutoScroll")));
			this.tabPage4.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("tabPage4.AutoScrollMargin")));
			this.tabPage4.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("tabPage4.AutoScrollMinSize")));
			this.tabPage4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tabPage4.BackgroundImage")));
			this.tabPage4.Controls.Add(this.cbext);
			this.tabPage4.Controls.Add(this.llopenext);
			this.tabPage4.Controls.Add(this.btbyteview);
			this.tabPage4.Controls.Add(this.gbtypes);
			this.tabPage4.Controls.Add(this.lbHandler);
			this.tabPage4.Controls.Add(this.label3);
			this.tabPage4.Controls.Add(this.tbUncsize);
			this.tabPage4.Controls.Add(this.tbSize);
			this.tabPage4.Controls.Add(this.cbComp);
			this.tabPage4.Controls.Add(this.label2);
			this.tabPage4.Controls.Add(this.label1);
			this.tabPage4.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tabPage4.Dock")));
			this.tabPage4.Enabled = ((bool)(resources.GetObject("tabPage4.Enabled")));
			this.tabPage4.Font = ((System.Drawing.Font)(resources.GetObject("tabPage4.Font")));
			this.tabPage4.ImageIndex = ((int)(resources.GetObject("tabPage4.ImageIndex")));
			this.tabPage4.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tabPage4.ImeMode")));
			this.tabPage4.Location = ((System.Drawing.Point)(resources.GetObject("tabPage4.Location")));
			this.tabPage4.Name = "tabPage4";
			this.tabPage4.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tabPage4.RightToLeft")));
			this.tabPage4.Size = ((System.Drawing.Size)(resources.GetObject("tabPage4.Size")));
			this.tabPage4.TabIndex = ((int)(resources.GetObject("tabPage4.TabIndex")));
			this.tabPage4.Text = resources.GetString("tabPage4.Text");
			this.tabPage4.ToolTipText = resources.GetString("tabPage4.ToolTipText");
			this.tabPage4.Visible = ((bool)(resources.GetObject("tabPage4.Visible")));
			this.visualStyleProvider1.SetVisualStyleSupport(this.tabPage4, true);
			// 
			// cbext
			// 
			this.cbext.AccessibleDescription = resources.GetString("cbext.AccessibleDescription");
			this.cbext.AccessibleName = resources.GetString("cbext.AccessibleName");
			this.cbext.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cbext.Anchor")));
			this.cbext.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cbext.BackgroundImage")));
			this.cbext.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cbext.Dock")));
			this.cbext.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbext.Enabled = ((bool)(resources.GetObject("cbext.Enabled")));
			this.cbext.Font = ((System.Drawing.Font)(resources.GetObject("cbext.Font")));
			this.cbext.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cbext.ImeMode")));
			this.cbext.IntegralHeight = ((bool)(resources.GetObject("cbext.IntegralHeight")));
			this.cbext.ItemHeight = ((int)(resources.GetObject("cbext.ItemHeight")));
			this.cbext.Location = ((System.Drawing.Point)(resources.GetObject("cbext.Location")));
			this.cbext.MaxDropDownItems = ((int)(resources.GetObject("cbext.MaxDropDownItems")));
			this.cbext.MaxLength = ((int)(resources.GetObject("cbext.MaxLength")));
			this.cbext.Name = "cbext";
			this.cbext.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cbext.RightToLeft")));
			this.cbext.Size = ((System.Drawing.Size)(resources.GetObject("cbext.Size")));
			this.cbext.TabIndex = ((int)(resources.GetObject("cbext.TabIndex")));
			this.cbext.Text = resources.GetString("cbext.Text");
			this.cbext.Visible = ((bool)(resources.GetObject("cbext.Visible")));
			// 
			// llopenext
			// 
			this.llopenext.AccessibleDescription = resources.GetString("llopenext.AccessibleDescription");
			this.llopenext.AccessibleName = resources.GetString("llopenext.AccessibleName");
			this.llopenext.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("llopenext.Anchor")));
			this.llopenext.AutoSize = ((bool)(resources.GetObject("llopenext.AutoSize")));
			this.llopenext.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("llopenext.Dock")));
			this.llopenext.Enabled = ((bool)(resources.GetObject("llopenext.Enabled")));
			this.llopenext.Font = ((System.Drawing.Font)(resources.GetObject("llopenext.Font")));
			this.llopenext.Image = ((System.Drawing.Image)(resources.GetObject("llopenext.Image")));
			this.llopenext.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("llopenext.ImageAlign")));
			this.llopenext.ImageIndex = ((int)(resources.GetObject("llopenext.ImageIndex")));
			this.llopenext.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("llopenext.ImeMode")));
			this.llopenext.LinkArea = ((System.Windows.Forms.LinkArea)(resources.GetObject("llopenext.LinkArea")));
			this.llopenext.Location = ((System.Drawing.Point)(resources.GetObject("llopenext.Location")));
			this.llopenext.Name = "llopenext";
			this.llopenext.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("llopenext.RightToLeft")));
			this.llopenext.Size = ((System.Drawing.Size)(resources.GetObject("llopenext.Size")));
			this.llopenext.TabIndex = ((int)(resources.GetObject("llopenext.TabIndex")));
			this.llopenext.TabStop = true;
			this.llopenext.Text = resources.GetString("llopenext.Text");
			this.llopenext.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("llopenext.TextAlign")));
			this.llopenext.Visible = ((bool)(resources.GetObject("llopenext.Visible")));
			this.llopenext.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.HexOpenClick);
			// 
			// btbyteview
			// 
			this.btbyteview.AccessibleDescription = resources.GetString("btbyteview.AccessibleDescription");
			this.btbyteview.AccessibleName = resources.GetString("btbyteview.AccessibleName");
			this.btbyteview.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btbyteview.Anchor")));
			this.btbyteview.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btbyteview.BackgroundImage")));
			this.btbyteview.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btbyteview.Dock")));
			this.btbyteview.Enabled = ((bool)(resources.GetObject("btbyteview.Enabled")));
			this.btbyteview.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btbyteview.FlatStyle")));
			this.btbyteview.Font = ((System.Drawing.Font)(resources.GetObject("btbyteview.Font")));
			this.btbyteview.Image = ((System.Drawing.Image)(resources.GetObject("btbyteview.Image")));
			this.btbyteview.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btbyteview.ImageAlign")));
			this.btbyteview.ImageIndex = ((int)(resources.GetObject("btbyteview.ImageIndex")));
			this.btbyteview.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btbyteview.ImeMode")));
			this.btbyteview.Location = ((System.Drawing.Point)(resources.GetObject("btbyteview.Location")));
			this.btbyteview.Name = "btbyteview";
			this.btbyteview.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btbyteview.RightToLeft")));
			this.btbyteview.Size = ((System.Drawing.Size)(resources.GetObject("btbyteview.Size")));
			this.btbyteview.TabIndex = ((int)(resources.GetObject("btbyteview.TabIndex")));
			this.btbyteview.Text = resources.GetString("btbyteview.Text");
			this.btbyteview.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btbyteview.TextAlign")));
			this.btbyteview.Visible = ((bool)(resources.GetObject("btbyteview.Visible")));
			this.visualStyleProvider1.SetVisualStyleSupport(this.btbyteview, true);
			this.btbyteview.Click += new System.EventHandler(this.OpenByteView);
			// 
			// gbtypes
			// 
			this.gbtypes.AccessibleDescription = resources.GetString("gbtypes.AccessibleDescription");
			this.gbtypes.AccessibleName = resources.GetString("gbtypes.AccessibleName");
			this.gbtypes.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("gbtypes.Anchor")));
			this.gbtypes.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("gbtypes.BackgroundImage")));
			this.gbtypes.Controls.Add(this.pntypes);
			this.gbtypes.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("gbtypes.Dock")));
			this.gbtypes.Enabled = ((bool)(resources.GetObject("gbtypes.Enabled")));
			this.gbtypes.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.gbtypes.Font = ((System.Drawing.Font)(resources.GetObject("gbtypes.Font")));
			this.gbtypes.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("gbtypes.ImeMode")));
			this.gbtypes.Location = ((System.Drawing.Point)(resources.GetObject("gbtypes.Location")));
			this.gbtypes.Name = "gbtypes";
			this.gbtypes.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("gbtypes.RightToLeft")));
			this.gbtypes.Size = ((System.Drawing.Size)(resources.GetObject("gbtypes.Size")));
			this.gbtypes.TabIndex = ((int)(resources.GetObject("gbtypes.TabIndex")));
			this.gbtypes.TabStop = false;
			this.gbtypes.Text = resources.GetString("gbtypes.Text");
			this.gbtypes.Visible = ((bool)(resources.GetObject("gbtypes.Visible")));
			this.visualStyleProvider1.SetVisualStyleSupport(this.gbtypes, true);
			// 
			// pntypes
			// 
			this.pntypes.AccessibleDescription = resources.GetString("pntypes.AccessibleDescription");
			this.pntypes.AccessibleName = resources.GetString("pntypes.AccessibleName");
			this.pntypes.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("pntypes.Anchor")));
			this.pntypes.AutoScroll = ((bool)(resources.GetObject("pntypes.AutoScroll")));
			this.pntypes.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("pntypes.AutoScrollMargin")));
			this.pntypes.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("pntypes.AutoScrollMinSize")));
			this.pntypes.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pntypes.BackgroundImage")));
			this.pntypes.Controls.Add(this.llchginst);
			this.pntypes.Controls.Add(this.llchggroup);
			this.pntypes.Controls.Add(this.tbsubtype);
			this.pntypes.Controls.Add(this.tbinstance);
			this.pntypes.Controls.Add(this.label11);
			this.pntypes.Controls.Add(this.tbtype);
			this.pntypes.Controls.Add(this.label8);
			this.pntypes.Controls.Add(this.label9);
			this.pntypes.Controls.Add(this.label10);
			this.pntypes.Controls.Add(this.tbgroup);
			this.pntypes.Controls.Add(this.cbtypes);
			this.pntypes.Controls.Add(this.llcommit);
			this.pntypes.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("pntypes.Dock")));
			this.pntypes.Enabled = ((bool)(resources.GetObject("pntypes.Enabled")));
			this.pntypes.Font = ((System.Drawing.Font)(resources.GetObject("pntypes.Font")));
			this.pntypes.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("pntypes.ImeMode")));
			this.pntypes.Location = ((System.Drawing.Point)(resources.GetObject("pntypes.Location")));
			this.pntypes.Name = "pntypes";
			this.pntypes.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("pntypes.RightToLeft")));
			this.pntypes.Size = ((System.Drawing.Size)(resources.GetObject("pntypes.Size")));
			this.pntypes.TabIndex = ((int)(resources.GetObject("pntypes.TabIndex")));
			this.pntypes.Text = resources.GetString("pntypes.Text");
			this.pntypes.Visible = ((bool)(resources.GetObject("pntypes.Visible")));
			this.visualStyleProvider1.SetVisualStyleSupport(this.pntypes, true);
			// 
			// llchginst
			// 
			this.llchginst.AccessibleDescription = resources.GetString("llchginst.AccessibleDescription");
			this.llchginst.AccessibleName = resources.GetString("llchginst.AccessibleName");
			this.llchginst.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("llchginst.Anchor")));
			this.llchginst.AutoSize = ((bool)(resources.GetObject("llchginst.AutoSize")));
			this.llchginst.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("llchginst.Dock")));
			this.llchginst.Enabled = ((bool)(resources.GetObject("llchginst.Enabled")));
			this.llchginst.Font = ((System.Drawing.Font)(resources.GetObject("llchginst.Font")));
			this.llchginst.Image = ((System.Drawing.Image)(resources.GetObject("llchginst.Image")));
			this.llchginst.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("llchginst.ImageAlign")));
			this.llchginst.ImageIndex = ((int)(resources.GetObject("llchginst.ImageIndex")));
			this.llchginst.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("llchginst.ImeMode")));
			this.llchginst.LinkArea = ((System.Windows.Forms.LinkArea)(resources.GetObject("llchginst.LinkArea")));
			this.llchginst.Location = ((System.Drawing.Point)(resources.GetObject("llchginst.Location")));
			this.llchginst.Name = "llchginst";
			this.llchginst.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("llchginst.RightToLeft")));
			this.llchginst.Size = ((System.Drawing.Size)(resources.GetObject("llchginst.Size")));
			this.llchginst.TabIndex = ((int)(resources.GetObject("llchginst.TabIndex")));
			this.llchginst.TabStop = true;
			this.llchginst.Text = resources.GetString("llchginst.Text");
			this.llchginst.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("llchginst.TextAlign")));
			this.llchginst.Visible = ((bool)(resources.GetObject("llchginst.Visible")));
			this.llchginst.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.MakeUnique);
			// 
			// llchggroup
			// 
			this.llchggroup.AccessibleDescription = resources.GetString("llchggroup.AccessibleDescription");
			this.llchggroup.AccessibleName = resources.GetString("llchggroup.AccessibleName");
			this.llchggroup.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("llchggroup.Anchor")));
			this.llchggroup.AutoSize = ((bool)(resources.GetObject("llchggroup.AutoSize")));
			this.llchggroup.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("llchggroup.Dock")));
			this.llchggroup.Enabled = ((bool)(resources.GetObject("llchggroup.Enabled")));
			this.llchggroup.Font = ((System.Drawing.Font)(resources.GetObject("llchggroup.Font")));
			this.llchggroup.Image = ((System.Drawing.Image)(resources.GetObject("llchggroup.Image")));
			this.llchggroup.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("llchggroup.ImageAlign")));
			this.llchggroup.ImageIndex = ((int)(resources.GetObject("llchggroup.ImageIndex")));
			this.llchggroup.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("llchggroup.ImeMode")));
			this.llchggroup.LinkArea = ((System.Windows.Forms.LinkArea)(resources.GetObject("llchggroup.LinkArea")));
			this.llchggroup.Location = ((System.Drawing.Point)(resources.GetObject("llchggroup.Location")));
			this.llchggroup.Name = "llchggroup";
			this.llchggroup.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("llchggroup.RightToLeft")));
			this.llchggroup.Size = ((System.Drawing.Size)(resources.GetObject("llchggroup.Size")));
			this.llchggroup.TabIndex = ((int)(resources.GetObject("llchggroup.TabIndex")));
			this.llchggroup.TabStop = true;
			this.llchggroup.Text = resources.GetString("llchggroup.Text");
			this.llchggroup.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("llchggroup.TextAlign")));
			this.llchggroup.Visible = ((bool)(resources.GetObject("llchggroup.Visible")));
			this.llchggroup.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ChangeGroups);
			// 
			// tbsubtype
			// 
			this.tbsubtype.AccessibleDescription = resources.GetString("tbsubtype.AccessibleDescription");
			this.tbsubtype.AccessibleName = resources.GetString("tbsubtype.AccessibleName");
			this.tbsubtype.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbsubtype.Anchor")));
			this.tbsubtype.AutoSize = ((bool)(resources.GetObject("tbsubtype.AutoSize")));
			this.tbsubtype.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbsubtype.BackgroundImage")));
			this.tbsubtype.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbsubtype.Dock")));
			this.tbsubtype.Enabled = ((bool)(resources.GetObject("tbsubtype.Enabled")));
			this.tbsubtype.Font = ((System.Drawing.Font)(resources.GetObject("tbsubtype.Font")));
			this.tbsubtype.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbsubtype.ImeMode")));
			this.tbsubtype.Location = ((System.Drawing.Point)(resources.GetObject("tbsubtype.Location")));
			this.tbsubtype.MaxLength = ((int)(resources.GetObject("tbsubtype.MaxLength")));
			this.tbsubtype.Multiline = ((bool)(resources.GetObject("tbsubtype.Multiline")));
			this.tbsubtype.Name = "tbsubtype";
			this.tbsubtype.PasswordChar = ((char)(resources.GetObject("tbsubtype.PasswordChar")));
			this.tbsubtype.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbsubtype.RightToLeft")));
			this.tbsubtype.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbsubtype.ScrollBars")));
			this.tbsubtype.Size = ((System.Drawing.Size)(resources.GetObject("tbsubtype.Size")));
			this.tbsubtype.TabIndex = ((int)(resources.GetObject("tbsubtype.TabIndex")));
			this.tbsubtype.Text = resources.GetString("tbsubtype.Text");
			this.tbsubtype.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbsubtype.TextAlign")));
			this.tbsubtype.Visible = ((bool)(resources.GetObject("tbsubtype.Visible")));
			this.visualStyleProvider1.SetVisualStyleSupport(this.tbsubtype, true);
			this.tbsubtype.WordWrap = ((bool)(resources.GetObject("tbsubtype.WordWrap")));
			// 
			// tbinstance
			// 
			this.tbinstance.AccessibleDescription = resources.GetString("tbinstance.AccessibleDescription");
			this.tbinstance.AccessibleName = resources.GetString("tbinstance.AccessibleName");
			this.tbinstance.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbinstance.Anchor")));
			this.tbinstance.AutoSize = ((bool)(resources.GetObject("tbinstance.AutoSize")));
			this.tbinstance.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbinstance.BackgroundImage")));
			this.tbinstance.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbinstance.Dock")));
			this.tbinstance.Enabled = ((bool)(resources.GetObject("tbinstance.Enabled")));
			this.tbinstance.Font = ((System.Drawing.Font)(resources.GetObject("tbinstance.Font")));
			this.tbinstance.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbinstance.ImeMode")));
			this.tbinstance.Location = ((System.Drawing.Point)(resources.GetObject("tbinstance.Location")));
			this.tbinstance.MaxLength = ((int)(resources.GetObject("tbinstance.MaxLength")));
			this.tbinstance.Multiline = ((bool)(resources.GetObject("tbinstance.Multiline")));
			this.tbinstance.Name = "tbinstance";
			this.tbinstance.PasswordChar = ((char)(resources.GetObject("tbinstance.PasswordChar")));
			this.tbinstance.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbinstance.RightToLeft")));
			this.tbinstance.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbinstance.ScrollBars")));
			this.tbinstance.Size = ((System.Drawing.Size)(resources.GetObject("tbinstance.Size")));
			this.tbinstance.TabIndex = ((int)(resources.GetObject("tbinstance.TabIndex")));
			this.tbinstance.Text = resources.GetString("tbinstance.Text");
			this.tbinstance.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbinstance.TextAlign")));
			this.tbinstance.Visible = ((bool)(resources.GetObject("tbinstance.Visible")));
			this.visualStyleProvider1.SetVisualStyleSupport(this.tbinstance, true);
			this.tbinstance.WordWrap = ((bool)(resources.GetObject("tbinstance.WordWrap")));
			// 
			// label11
			// 
			this.label11.AccessibleDescription = resources.GetString("label11.AccessibleDescription");
			this.label11.AccessibleName = resources.GetString("label11.AccessibleName");
			this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label11.Anchor")));
			this.label11.AutoSize = ((bool)(resources.GetObject("label11.AutoSize")));
			this.label11.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label11.Dock")));
			this.label11.Enabled = ((bool)(resources.GetObject("label11.Enabled")));
			this.label11.Font = ((System.Drawing.Font)(resources.GetObject("label11.Font")));
			this.label11.Image = ((System.Drawing.Image)(resources.GetObject("label11.Image")));
			this.label11.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label11.ImageAlign")));
			this.label11.ImageIndex = ((int)(resources.GetObject("label11.ImageIndex")));
			this.label11.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label11.ImeMode")));
			this.label11.Location = ((System.Drawing.Point)(resources.GetObject("label11.Location")));
			this.label11.Name = "label11";
			this.label11.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label11.RightToLeft")));
			this.label11.Size = ((System.Drawing.Size)(resources.GetObject("label11.Size")));
			this.label11.TabIndex = ((int)(resources.GetObject("label11.TabIndex")));
			this.label11.Text = resources.GetString("label11.Text");
			this.label11.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label11.TextAlign")));
			this.label11.Visible = ((bool)(resources.GetObject("label11.Visible")));
			this.visualStyleProvider1.SetVisualStyleSupport(this.label11, true);
			// 
			// tbtype
			// 
			this.tbtype.AccessibleDescription = resources.GetString("tbtype.AccessibleDescription");
			this.tbtype.AccessibleName = resources.GetString("tbtype.AccessibleName");
			this.tbtype.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbtype.Anchor")));
			this.tbtype.AutoSize = ((bool)(resources.GetObject("tbtype.AutoSize")));
			this.tbtype.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbtype.BackgroundImage")));
			this.tbtype.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbtype.Dock")));
			this.tbtype.Enabled = ((bool)(resources.GetObject("tbtype.Enabled")));
			this.tbtype.Font = ((System.Drawing.Font)(resources.GetObject("tbtype.Font")));
			this.tbtype.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbtype.ImeMode")));
			this.tbtype.Location = ((System.Drawing.Point)(resources.GetObject("tbtype.Location")));
			this.tbtype.MaxLength = ((int)(resources.GetObject("tbtype.MaxLength")));
			this.tbtype.Multiline = ((bool)(resources.GetObject("tbtype.Multiline")));
			this.tbtype.Name = "tbtype";
			this.tbtype.PasswordChar = ((char)(resources.GetObject("tbtype.PasswordChar")));
			this.tbtype.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbtype.RightToLeft")));
			this.tbtype.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbtype.ScrollBars")));
			this.tbtype.Size = ((System.Drawing.Size)(resources.GetObject("tbtype.Size")));
			this.tbtype.TabIndex = ((int)(resources.GetObject("tbtype.TabIndex")));
			this.tbtype.Text = resources.GetString("tbtype.Text");
			this.tbtype.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbtype.TextAlign")));
			this.tbtype.Visible = ((bool)(resources.GetObject("tbtype.Visible")));
			this.visualStyleProvider1.SetVisualStyleSupport(this.tbtype, true);
			this.tbtype.WordWrap = ((bool)(resources.GetObject("tbtype.WordWrap")));
			this.tbtype.TextChanged += new System.EventHandler(this.SelectTypeByNameClick);
			// 
			// label8
			// 
			this.label8.AccessibleDescription = resources.GetString("label8.AccessibleDescription");
			this.label8.AccessibleName = resources.GetString("label8.AccessibleName");
			this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label8.Anchor")));
			this.label8.AutoSize = ((bool)(resources.GetObject("label8.AutoSize")));
			this.label8.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label8.Dock")));
			this.label8.Enabled = ((bool)(resources.GetObject("label8.Enabled")));
			this.label8.Font = ((System.Drawing.Font)(resources.GetObject("label8.Font")));
			this.label8.Image = ((System.Drawing.Image)(resources.GetObject("label8.Image")));
			this.label8.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label8.ImageAlign")));
			this.label8.ImageIndex = ((int)(resources.GetObject("label8.ImageIndex")));
			this.label8.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label8.ImeMode")));
			this.label8.Location = ((System.Drawing.Point)(resources.GetObject("label8.Location")));
			this.label8.Name = "label8";
			this.label8.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label8.RightToLeft")));
			this.label8.Size = ((System.Drawing.Size)(resources.GetObject("label8.Size")));
			this.label8.TabIndex = ((int)(resources.GetObject("label8.TabIndex")));
			this.label8.Text = resources.GetString("label8.Text");
			this.label8.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label8.TextAlign")));
			this.label8.Visible = ((bool)(resources.GetObject("label8.Visible")));
			this.visualStyleProvider1.SetVisualStyleSupport(this.label8, true);
			// 
			// label9
			// 
			this.label9.AccessibleDescription = resources.GetString("label9.AccessibleDescription");
			this.label9.AccessibleName = resources.GetString("label9.AccessibleName");
			this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label9.Anchor")));
			this.label9.AutoSize = ((bool)(resources.GetObject("label9.AutoSize")));
			this.label9.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label9.Dock")));
			this.label9.Enabled = ((bool)(resources.GetObject("label9.Enabled")));
			this.label9.Font = ((System.Drawing.Font)(resources.GetObject("label9.Font")));
			this.label9.Image = ((System.Drawing.Image)(resources.GetObject("label9.Image")));
			this.label9.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label9.ImageAlign")));
			this.label9.ImageIndex = ((int)(resources.GetObject("label9.ImageIndex")));
			this.label9.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label9.ImeMode")));
			this.label9.Location = ((System.Drawing.Point)(resources.GetObject("label9.Location")));
			this.label9.Name = "label9";
			this.label9.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label9.RightToLeft")));
			this.label9.Size = ((System.Drawing.Size)(resources.GetObject("label9.Size")));
			this.label9.TabIndex = ((int)(resources.GetObject("label9.TabIndex")));
			this.label9.Text = resources.GetString("label9.Text");
			this.label9.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label9.TextAlign")));
			this.label9.Visible = ((bool)(resources.GetObject("label9.Visible")));
			this.visualStyleProvider1.SetVisualStyleSupport(this.label9, true);
			// 
			// label10
			// 
			this.label10.AccessibleDescription = resources.GetString("label10.AccessibleDescription");
			this.label10.AccessibleName = resources.GetString("label10.AccessibleName");
			this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label10.Anchor")));
			this.label10.AutoSize = ((bool)(resources.GetObject("label10.AutoSize")));
			this.label10.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label10.Dock")));
			this.label10.Enabled = ((bool)(resources.GetObject("label10.Enabled")));
			this.label10.Font = ((System.Drawing.Font)(resources.GetObject("label10.Font")));
			this.label10.Image = ((System.Drawing.Image)(resources.GetObject("label10.Image")));
			this.label10.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label10.ImageAlign")));
			this.label10.ImageIndex = ((int)(resources.GetObject("label10.ImageIndex")));
			this.label10.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label10.ImeMode")));
			this.label10.Location = ((System.Drawing.Point)(resources.GetObject("label10.Location")));
			this.label10.Name = "label10";
			this.label10.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label10.RightToLeft")));
			this.label10.Size = ((System.Drawing.Size)(resources.GetObject("label10.Size")));
			this.label10.TabIndex = ((int)(resources.GetObject("label10.TabIndex")));
			this.label10.Text = resources.GetString("label10.Text");
			this.label10.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label10.TextAlign")));
			this.label10.Visible = ((bool)(resources.GetObject("label10.Visible")));
			this.visualStyleProvider1.SetVisualStyleSupport(this.label10, true);
			// 
			// tbgroup
			// 
			this.tbgroup.AccessibleDescription = resources.GetString("tbgroup.AccessibleDescription");
			this.tbgroup.AccessibleName = resources.GetString("tbgroup.AccessibleName");
			this.tbgroup.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbgroup.Anchor")));
			this.tbgroup.AutoSize = ((bool)(resources.GetObject("tbgroup.AutoSize")));
			this.tbgroup.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbgroup.BackgroundImage")));
			this.tbgroup.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbgroup.Dock")));
			this.tbgroup.Enabled = ((bool)(resources.GetObject("tbgroup.Enabled")));
			this.tbgroup.Font = ((System.Drawing.Font)(resources.GetObject("tbgroup.Font")));
			this.tbgroup.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbgroup.ImeMode")));
			this.tbgroup.Location = ((System.Drawing.Point)(resources.GetObject("tbgroup.Location")));
			this.tbgroup.MaxLength = ((int)(resources.GetObject("tbgroup.MaxLength")));
			this.tbgroup.Multiline = ((bool)(resources.GetObject("tbgroup.Multiline")));
			this.tbgroup.Name = "tbgroup";
			this.tbgroup.PasswordChar = ((char)(resources.GetObject("tbgroup.PasswordChar")));
			this.tbgroup.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbgroup.RightToLeft")));
			this.tbgroup.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbgroup.ScrollBars")));
			this.tbgroup.Size = ((System.Drawing.Size)(resources.GetObject("tbgroup.Size")));
			this.tbgroup.TabIndex = ((int)(resources.GetObject("tbgroup.TabIndex")));
			this.tbgroup.Text = resources.GetString("tbgroup.Text");
			this.tbgroup.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbgroup.TextAlign")));
			this.tbgroup.Visible = ((bool)(resources.GetObject("tbgroup.Visible")));
			this.visualStyleProvider1.SetVisualStyleSupport(this.tbgroup, true);
			this.tbgroup.WordWrap = ((bool)(resources.GetObject("tbgroup.WordWrap")));
			// 
			// cbtypes
			// 
			this.cbtypes.AccessibleDescription = resources.GetString("cbtypes.AccessibleDescription");
			this.cbtypes.AccessibleName = resources.GetString("cbtypes.AccessibleName");
			this.cbtypes.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cbtypes.Anchor")));
			this.cbtypes.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cbtypes.BackgroundImage")));
			this.cbtypes.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cbtypes.Dock")));
			this.cbtypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbtypes.Enabled = ((bool)(resources.GetObject("cbtypes.Enabled")));
			this.cbtypes.Font = ((System.Drawing.Font)(resources.GetObject("cbtypes.Font")));
			this.cbtypes.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cbtypes.ImeMode")));
			this.cbtypes.IntegralHeight = ((bool)(resources.GetObject("cbtypes.IntegralHeight")));
			this.cbtypes.ItemHeight = ((int)(resources.GetObject("cbtypes.ItemHeight")));
			this.cbtypes.Location = ((System.Drawing.Point)(resources.GetObject("cbtypes.Location")));
			this.cbtypes.MaxDropDownItems = ((int)(resources.GetObject("cbtypes.MaxDropDownItems")));
			this.cbtypes.MaxLength = ((int)(resources.GetObject("cbtypes.MaxLength")));
			this.cbtypes.Name = "cbtypes";
			this.cbtypes.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cbtypes.RightToLeft")));
			this.cbtypes.Size = ((System.Drawing.Size)(resources.GetObject("cbtypes.Size")));
			this.cbtypes.TabIndex = ((int)(resources.GetObject("cbtypes.TabIndex")));
			this.cbtypes.Text = resources.GetString("cbtypes.Text");
			this.cbtypes.Visible = ((bool)(resources.GetObject("cbtypes.Visible")));
			this.cbtypes.SelectedIndexChanged += new System.EventHandler(this.TypeSelectClick);
			// 
			// llcommit
			// 
			this.llcommit.AccessibleDescription = resources.GetString("llcommit.AccessibleDescription");
			this.llcommit.AccessibleName = resources.GetString("llcommit.AccessibleName");
			this.llcommit.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("llcommit.Anchor")));
			this.llcommit.AutoSize = ((bool)(resources.GetObject("llcommit.AutoSize")));
			this.llcommit.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("llcommit.Dock")));
			this.llcommit.Enabled = ((bool)(resources.GetObject("llcommit.Enabled")));
			this.llcommit.Font = ((System.Drawing.Font)(resources.GetObject("llcommit.Font")));
			this.llcommit.Image = ((System.Drawing.Image)(resources.GetObject("llcommit.Image")));
			this.llcommit.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("llcommit.ImageAlign")));
			this.llcommit.ImageIndex = ((int)(resources.GetObject("llcommit.ImageIndex")));
			this.llcommit.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("llcommit.ImeMode")));
			this.llcommit.LinkArea = ((System.Windows.Forms.LinkArea)(resources.GetObject("llcommit.LinkArea")));
			this.llcommit.Location = ((System.Drawing.Point)(resources.GetObject("llcommit.Location")));
			this.llcommit.Name = "llcommit";
			this.llcommit.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("llcommit.RightToLeft")));
			this.llcommit.Size = ((System.Drawing.Size)(resources.GetObject("llcommit.Size")));
			this.llcommit.TabIndex = ((int)(resources.GetObject("llcommit.TabIndex")));
			this.llcommit.TabStop = true;
			this.llcommit.Text = resources.GetString("llcommit.Text");
			this.llcommit.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("llcommit.TextAlign")));
			this.llcommit.Visible = ((bool)(resources.GetObject("llcommit.Visible")));
			this.llcommit.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ComitFileAttributesClick);
			// 
			// lbHandler
			// 
			this.lbHandler.AccessibleDescription = resources.GetString("lbHandler.AccessibleDescription");
			this.lbHandler.AccessibleName = resources.GetString("lbHandler.AccessibleName");
			this.lbHandler.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbHandler.Anchor")));
			this.lbHandler.AutoSize = ((bool)(resources.GetObject("lbHandler.AutoSize")));
			this.lbHandler.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbHandler.Dock")));
			this.lbHandler.Enabled = ((bool)(resources.GetObject("lbHandler.Enabled")));
			this.lbHandler.Font = ((System.Drawing.Font)(resources.GetObject("lbHandler.Font")));
			this.lbHandler.Image = ((System.Drawing.Image)(resources.GetObject("lbHandler.Image")));
			this.lbHandler.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbHandler.ImageAlign")));
			this.lbHandler.ImageIndex = ((int)(resources.GetObject("lbHandler.ImageIndex")));
			this.lbHandler.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbHandler.ImeMode")));
			this.lbHandler.Location = ((System.Drawing.Point)(resources.GetObject("lbHandler.Location")));
			this.lbHandler.Name = "lbHandler";
			this.lbHandler.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbHandler.RightToLeft")));
			this.lbHandler.Size = ((System.Drawing.Size)(resources.GetObject("lbHandler.Size")));
			this.lbHandler.TabIndex = ((int)(resources.GetObject("lbHandler.TabIndex")));
			this.lbHandler.Text = resources.GetString("lbHandler.Text");
			this.lbHandler.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbHandler.TextAlign")));
			this.lbHandler.Visible = ((bool)(resources.GetObject("lbHandler.Visible")));
			this.visualStyleProvider1.SetVisualStyleSupport(this.lbHandler, true);
			// 
			// label3
			// 
			this.label3.AccessibleDescription = resources.GetString("label3.AccessibleDescription");
			this.label3.AccessibleName = resources.GetString("label3.AccessibleName");
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label3.Anchor")));
			this.label3.AutoSize = ((bool)(resources.GetObject("label3.AutoSize")));
			this.label3.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label3.Dock")));
			this.label3.Enabled = ((bool)(resources.GetObject("label3.Enabled")));
			this.label3.Font = ((System.Drawing.Font)(resources.GetObject("label3.Font")));
			this.label3.Image = ((System.Drawing.Image)(resources.GetObject("label3.Image")));
			this.label3.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label3.ImageAlign")));
			this.label3.ImageIndex = ((int)(resources.GetObject("label3.ImageIndex")));
			this.label3.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label3.ImeMode")));
			this.label3.Location = ((System.Drawing.Point)(resources.GetObject("label3.Location")));
			this.label3.Name = "label3";
			this.label3.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label3.RightToLeft")));
			this.label3.Size = ((System.Drawing.Size)(resources.GetObject("label3.Size")));
			this.label3.TabIndex = ((int)(resources.GetObject("label3.TabIndex")));
			this.label3.Text = resources.GetString("label3.Text");
			this.label3.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label3.TextAlign")));
			this.label3.Visible = ((bool)(resources.GetObject("label3.Visible")));
			this.visualStyleProvider1.SetVisualStyleSupport(this.label3, true);
			// 
			// tbUncsize
			// 
			this.tbUncsize.AccessibleDescription = resources.GetString("tbUncsize.AccessibleDescription");
			this.tbUncsize.AccessibleName = resources.GetString("tbUncsize.AccessibleName");
			this.tbUncsize.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbUncsize.Anchor")));
			this.tbUncsize.AutoSize = ((bool)(resources.GetObject("tbUncsize.AutoSize")));
			this.tbUncsize.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbUncsize.BackgroundImage")));
			this.tbUncsize.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbUncsize.Dock")));
			this.tbUncsize.Enabled = ((bool)(resources.GetObject("tbUncsize.Enabled")));
			this.tbUncsize.Font = ((System.Drawing.Font)(resources.GetObject("tbUncsize.Font")));
			this.tbUncsize.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbUncsize.ImeMode")));
			this.tbUncsize.Location = ((System.Drawing.Point)(resources.GetObject("tbUncsize.Location")));
			this.tbUncsize.MaxLength = ((int)(resources.GetObject("tbUncsize.MaxLength")));
			this.tbUncsize.Multiline = ((bool)(resources.GetObject("tbUncsize.Multiline")));
			this.tbUncsize.Name = "tbUncsize";
			this.tbUncsize.PasswordChar = ((char)(resources.GetObject("tbUncsize.PasswordChar")));
			this.tbUncsize.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbUncsize.RightToLeft")));
			this.tbUncsize.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbUncsize.ScrollBars")));
			this.tbUncsize.Size = ((System.Drawing.Size)(resources.GetObject("tbUncsize.Size")));
			this.tbUncsize.TabIndex = ((int)(resources.GetObject("tbUncsize.TabIndex")));
			this.tbUncsize.Text = resources.GetString("tbUncsize.Text");
			this.tbUncsize.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbUncsize.TextAlign")));
			this.tbUncsize.Visible = ((bool)(resources.GetObject("tbUncsize.Visible")));
			this.visualStyleProvider1.SetVisualStyleSupport(this.tbUncsize, true);
			this.tbUncsize.WordWrap = ((bool)(resources.GetObject("tbUncsize.WordWrap")));
			// 
			// tbSize
			// 
			this.tbSize.AccessibleDescription = resources.GetString("tbSize.AccessibleDescription");
			this.tbSize.AccessibleName = resources.GetString("tbSize.AccessibleName");
			this.tbSize.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbSize.Anchor")));
			this.tbSize.AutoSize = ((bool)(resources.GetObject("tbSize.AutoSize")));
			this.tbSize.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbSize.BackgroundImage")));
			this.tbSize.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbSize.Dock")));
			this.tbSize.Enabled = ((bool)(resources.GetObject("tbSize.Enabled")));
			this.tbSize.Font = ((System.Drawing.Font)(resources.GetObject("tbSize.Font")));
			this.tbSize.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbSize.ImeMode")));
			this.tbSize.Location = ((System.Drawing.Point)(resources.GetObject("tbSize.Location")));
			this.tbSize.MaxLength = ((int)(resources.GetObject("tbSize.MaxLength")));
			this.tbSize.Multiline = ((bool)(resources.GetObject("tbSize.Multiline")));
			this.tbSize.Name = "tbSize";
			this.tbSize.PasswordChar = ((char)(resources.GetObject("tbSize.PasswordChar")));
			this.tbSize.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbSize.RightToLeft")));
			this.tbSize.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbSize.ScrollBars")));
			this.tbSize.Size = ((System.Drawing.Size)(resources.GetObject("tbSize.Size")));
			this.tbSize.TabIndex = ((int)(resources.GetObject("tbSize.TabIndex")));
			this.tbSize.Text = resources.GetString("tbSize.Text");
			this.tbSize.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbSize.TextAlign")));
			this.tbSize.Visible = ((bool)(resources.GetObject("tbSize.Visible")));
			this.visualStyleProvider1.SetVisualStyleSupport(this.tbSize, true);
			this.tbSize.WordWrap = ((bool)(resources.GetObject("tbSize.WordWrap")));
			// 
			// cbComp
			// 
			this.cbComp.AccessibleDescription = resources.GetString("cbComp.AccessibleDescription");
			this.cbComp.AccessibleName = resources.GetString("cbComp.AccessibleName");
			this.cbComp.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cbComp.Anchor")));
			this.cbComp.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("cbComp.Appearance")));
			this.cbComp.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cbComp.BackgroundImage")));
			this.cbComp.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbComp.CheckAlign")));
			this.cbComp.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cbComp.Dock")));
			this.cbComp.Enabled = ((bool)(resources.GetObject("cbComp.Enabled")));
			this.cbComp.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("cbComp.FlatStyle")));
			this.cbComp.Font = ((System.Drawing.Font)(resources.GetObject("cbComp.Font")));
			this.cbComp.Image = ((System.Drawing.Image)(resources.GetObject("cbComp.Image")));
			this.cbComp.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbComp.ImageAlign")));
			this.cbComp.ImageIndex = ((int)(resources.GetObject("cbComp.ImageIndex")));
			this.cbComp.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cbComp.ImeMode")));
			this.cbComp.Location = ((System.Drawing.Point)(resources.GetObject("cbComp.Location")));
			this.cbComp.Name = "cbComp";
			this.cbComp.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cbComp.RightToLeft")));
			this.cbComp.Size = ((System.Drawing.Size)(resources.GetObject("cbComp.Size")));
			this.cbComp.TabIndex = ((int)(resources.GetObject("cbComp.TabIndex")));
			this.cbComp.Text = resources.GetString("cbComp.Text");
			this.cbComp.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbComp.TextAlign")));
			this.cbComp.Visible = ((bool)(resources.GetObject("cbComp.Visible")));
			this.visualStyleProvider1.SetVisualStyleSupport(this.cbComp, true);
			this.cbComp.CheckedChanged += new System.EventHandler(this.ToogleCompression);
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
			this.visualStyleProvider1.SetVisualStyleSupport(this.label2, true);
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
			this.visualStyleProvider1.SetVisualStyleSupport(this.label1, true);
			// 
			// tabPage3
			// 
			this.tabPage3.AccessibleDescription = resources.GetString("tabPage3.AccessibleDescription");
			this.tabPage3.AccessibleName = resources.GetString("tabPage3.AccessibleName");
			this.tabPage3.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tabPage3.Anchor")));
			this.tabPage3.AutoScroll = ((bool)(resources.GetObject("tabPage3.AutoScroll")));
			this.tabPage3.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("tabPage3.AutoScrollMargin")));
			this.tabPage3.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("tabPage3.AutoScrollMinSize")));
			this.tabPage3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tabPage3.BackgroundImage")));
			this.tabPage3.Controls.Add(this.lbHexView);
			this.tabPage3.Controls.Add(this.panel4);
			this.tabPage3.Controls.Add(this.panel2);
			this.tabPage3.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tabPage3.Dock")));
			this.tabPage3.Enabled = ((bool)(resources.GetObject("tabPage3.Enabled")));
			this.tabPage3.Font = ((System.Drawing.Font)(resources.GetObject("tabPage3.Font")));
			this.tabPage3.ImageIndex = ((int)(resources.GetObject("tabPage3.ImageIndex")));
			this.tabPage3.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tabPage3.ImeMode")));
			this.tabPage3.Location = ((System.Drawing.Point)(resources.GetObject("tabPage3.Location")));
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tabPage3.RightToLeft")));
			this.tabPage3.Size = ((System.Drawing.Size)(resources.GetObject("tabPage3.Size")));
			this.tabPage3.TabIndex = ((int)(resources.GetObject("tabPage3.TabIndex")));
			this.tabPage3.Text = resources.GetString("tabPage3.Text");
			this.tabPage3.ToolTipText = resources.GetString("tabPage3.ToolTipText");
			this.tabPage3.Visible = ((bool)(resources.GetObject("tabPage3.Visible")));
			this.visualStyleProvider1.SetVisualStyleSupport(this.tabPage3, true);
			// 
			// lbHexView
			// 
			this.lbHexView.AccessibleDescription = resources.GetString("lbHexView.AccessibleDescription");
			this.lbHexView.AccessibleName = resources.GetString("lbHexView.AccessibleName");
			this.lbHexView.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbHexView.Anchor")));
			this.lbHexView.AutoSize = ((bool)(resources.GetObject("lbHexView.AutoSize")));
			this.lbHexView.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbHexView.Dock")));
			this.lbHexView.Enabled = ((bool)(resources.GetObject("lbHexView.Enabled")));
			this.lbHexView.Font = ((System.Drawing.Font)(resources.GetObject("lbHexView.Font")));
			this.lbHexView.ForeColor = System.Drawing.Color.Maroon;
			this.lbHexView.Image = ((System.Drawing.Image)(resources.GetObject("lbHexView.Image")));
			this.lbHexView.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbHexView.ImageAlign")));
			this.lbHexView.ImageIndex = ((int)(resources.GetObject("lbHexView.ImageIndex")));
			this.lbHexView.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbHexView.ImeMode")));
			this.lbHexView.Location = ((System.Drawing.Point)(resources.GetObject("lbHexView.Location")));
			this.lbHexView.Name = "lbHexView";
			this.lbHexView.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbHexView.RightToLeft")));
			this.lbHexView.Size = ((System.Drawing.Size)(resources.GetObject("lbHexView.Size")));
			this.lbHexView.TabIndex = ((int)(resources.GetObject("lbHexView.TabIndex")));
			this.lbHexView.Text = resources.GetString("lbHexView.Text");
			this.lbHexView.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbHexView.TextAlign")));
			this.lbHexView.Visible = ((bool)(resources.GetObject("lbHexView.Visible")));
			this.visualStyleProvider1.SetVisualStyleSupport(this.lbHexView, true);
			// 
			// panel4
			// 
			this.panel4.AccessibleDescription = resources.GetString("panel4.AccessibleDescription");
			this.panel4.AccessibleName = resources.GetString("panel4.AccessibleName");
			this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("panel4.Anchor")));
			this.panel4.AutoScroll = ((bool)(resources.GetObject("panel4.AutoScroll")));
			this.panel4.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("panel4.AutoScrollMargin")));
			this.panel4.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("panel4.AutoScrollMinSize")));
			this.panel4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel4.BackgroundImage")));
			this.panel4.Controls.Add(this.label15);
			this.panel4.Controls.Add(this.lbdword);
			this.panel4.Controls.Add(this.label13);
			this.panel4.Controls.Add(this.lbword);
			this.panel4.Controls.Add(this.label12);
			this.panel4.Controls.Add(this.lbbyte);
			this.panel4.Controls.Add(this.btgoto);
			this.panel4.Controls.Add(this.tboffset);
			this.panel4.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("panel4.Dock")));
			this.panel4.Enabled = ((bool)(resources.GetObject("panel4.Enabled")));
			this.panel4.Font = ((System.Drawing.Font)(resources.GetObject("panel4.Font")));
			this.panel4.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("panel4.ImeMode")));
			this.panel4.Location = ((System.Drawing.Point)(resources.GetObject("panel4.Location")));
			this.panel4.Name = "panel4";
			this.panel4.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("panel4.RightToLeft")));
			this.panel4.Size = ((System.Drawing.Size)(resources.GetObject("panel4.Size")));
			this.panel4.TabIndex = ((int)(resources.GetObject("panel4.TabIndex")));
			this.panel4.Text = resources.GetString("panel4.Text");
			this.panel4.Visible = ((bool)(resources.GetObject("panel4.Visible")));
			this.visualStyleProvider1.SetVisualStyleSupport(this.panel4, true);
			// 
			// label15
			// 
			this.label15.AccessibleDescription = resources.GetString("label15.AccessibleDescription");
			this.label15.AccessibleName = resources.GetString("label15.AccessibleName");
			this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label15.Anchor")));
			this.label15.AutoSize = ((bool)(resources.GetObject("label15.AutoSize")));
			this.label15.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label15.Dock")));
			this.label15.Enabled = ((bool)(resources.GetObject("label15.Enabled")));
			this.label15.Font = ((System.Drawing.Font)(resources.GetObject("label15.Font")));
			this.label15.Image = ((System.Drawing.Image)(resources.GetObject("label15.Image")));
			this.label15.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label15.ImageAlign")));
			this.label15.ImageIndex = ((int)(resources.GetObject("label15.ImageIndex")));
			this.label15.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label15.ImeMode")));
			this.label15.Location = ((System.Drawing.Point)(resources.GetObject("label15.Location")));
			this.label15.Name = "label15";
			this.label15.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label15.RightToLeft")));
			this.label15.Size = ((System.Drawing.Size)(resources.GetObject("label15.Size")));
			this.label15.TabIndex = ((int)(resources.GetObject("label15.TabIndex")));
			this.label15.Text = resources.GetString("label15.Text");
			this.label15.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label15.TextAlign")));
			this.label15.Visible = ((bool)(resources.GetObject("label15.Visible")));
			this.visualStyleProvider1.SetVisualStyleSupport(this.label15, true);
			// 
			// lbdword
			// 
			this.lbdword.AccessibleDescription = resources.GetString("lbdword.AccessibleDescription");
			this.lbdword.AccessibleName = resources.GetString("lbdword.AccessibleName");
			this.lbdword.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbdword.Anchor")));
			this.lbdword.AutoSize = ((bool)(resources.GetObject("lbdword.AutoSize")));
			this.lbdword.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbdword.Dock")));
			this.lbdword.Enabled = ((bool)(resources.GetObject("lbdword.Enabled")));
			this.lbdword.Font = ((System.Drawing.Font)(resources.GetObject("lbdword.Font")));
			this.lbdword.Image = ((System.Drawing.Image)(resources.GetObject("lbdword.Image")));
			this.lbdword.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbdword.ImageAlign")));
			this.lbdword.ImageIndex = ((int)(resources.GetObject("lbdword.ImageIndex")));
			this.lbdword.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbdword.ImeMode")));
			this.lbdword.Location = ((System.Drawing.Point)(resources.GetObject("lbdword.Location")));
			this.lbdword.Name = "lbdword";
			this.lbdword.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbdword.RightToLeft")));
			this.lbdword.Size = ((System.Drawing.Size)(resources.GetObject("lbdword.Size")));
			this.lbdword.TabIndex = ((int)(resources.GetObject("lbdword.TabIndex")));
			this.lbdword.Text = resources.GetString("lbdword.Text");
			this.lbdword.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbdword.TextAlign")));
			this.lbdword.Visible = ((bool)(resources.GetObject("lbdword.Visible")));
			this.visualStyleProvider1.SetVisualStyleSupport(this.lbdword, true);
			// 
			// label13
			// 
			this.label13.AccessibleDescription = resources.GetString("label13.AccessibleDescription");
			this.label13.AccessibleName = resources.GetString("label13.AccessibleName");
			this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label13.Anchor")));
			this.label13.AutoSize = ((bool)(resources.GetObject("label13.AutoSize")));
			this.label13.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label13.Dock")));
			this.label13.Enabled = ((bool)(resources.GetObject("label13.Enabled")));
			this.label13.Font = ((System.Drawing.Font)(resources.GetObject("label13.Font")));
			this.label13.Image = ((System.Drawing.Image)(resources.GetObject("label13.Image")));
			this.label13.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label13.ImageAlign")));
			this.label13.ImageIndex = ((int)(resources.GetObject("label13.ImageIndex")));
			this.label13.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label13.ImeMode")));
			this.label13.Location = ((System.Drawing.Point)(resources.GetObject("label13.Location")));
			this.label13.Name = "label13";
			this.label13.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label13.RightToLeft")));
			this.label13.Size = ((System.Drawing.Size)(resources.GetObject("label13.Size")));
			this.label13.TabIndex = ((int)(resources.GetObject("label13.TabIndex")));
			this.label13.Text = resources.GetString("label13.Text");
			this.label13.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label13.TextAlign")));
			this.label13.Visible = ((bool)(resources.GetObject("label13.Visible")));
			this.visualStyleProvider1.SetVisualStyleSupport(this.label13, true);
			// 
			// lbword
			// 
			this.lbword.AccessibleDescription = resources.GetString("lbword.AccessibleDescription");
			this.lbword.AccessibleName = resources.GetString("lbword.AccessibleName");
			this.lbword.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbword.Anchor")));
			this.lbword.AutoSize = ((bool)(resources.GetObject("lbword.AutoSize")));
			this.lbword.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbword.Dock")));
			this.lbword.Enabled = ((bool)(resources.GetObject("lbword.Enabled")));
			this.lbword.Font = ((System.Drawing.Font)(resources.GetObject("lbword.Font")));
			this.lbword.Image = ((System.Drawing.Image)(resources.GetObject("lbword.Image")));
			this.lbword.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbword.ImageAlign")));
			this.lbword.ImageIndex = ((int)(resources.GetObject("lbword.ImageIndex")));
			this.lbword.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbword.ImeMode")));
			this.lbword.Location = ((System.Drawing.Point)(resources.GetObject("lbword.Location")));
			this.lbword.Name = "lbword";
			this.lbword.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbword.RightToLeft")));
			this.lbword.Size = ((System.Drawing.Size)(resources.GetObject("lbword.Size")));
			this.lbword.TabIndex = ((int)(resources.GetObject("lbword.TabIndex")));
			this.lbword.Text = resources.GetString("lbword.Text");
			this.lbword.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbword.TextAlign")));
			this.lbword.Visible = ((bool)(resources.GetObject("lbword.Visible")));
			this.visualStyleProvider1.SetVisualStyleSupport(this.lbword, true);
			// 
			// label12
			// 
			this.label12.AccessibleDescription = resources.GetString("label12.AccessibleDescription");
			this.label12.AccessibleName = resources.GetString("label12.AccessibleName");
			this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label12.Anchor")));
			this.label12.AutoSize = ((bool)(resources.GetObject("label12.AutoSize")));
			this.label12.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label12.Dock")));
			this.label12.Enabled = ((bool)(resources.GetObject("label12.Enabled")));
			this.label12.Font = ((System.Drawing.Font)(resources.GetObject("label12.Font")));
			this.label12.Image = ((System.Drawing.Image)(resources.GetObject("label12.Image")));
			this.label12.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label12.ImageAlign")));
			this.label12.ImageIndex = ((int)(resources.GetObject("label12.ImageIndex")));
			this.label12.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label12.ImeMode")));
			this.label12.Location = ((System.Drawing.Point)(resources.GetObject("label12.Location")));
			this.label12.Name = "label12";
			this.label12.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label12.RightToLeft")));
			this.label12.Size = ((System.Drawing.Size)(resources.GetObject("label12.Size")));
			this.label12.TabIndex = ((int)(resources.GetObject("label12.TabIndex")));
			this.label12.Text = resources.GetString("label12.Text");
			this.label12.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label12.TextAlign")));
			this.label12.Visible = ((bool)(resources.GetObject("label12.Visible")));
			this.visualStyleProvider1.SetVisualStyleSupport(this.label12, true);
			// 
			// lbbyte
			// 
			this.lbbyte.AccessibleDescription = resources.GetString("lbbyte.AccessibleDescription");
			this.lbbyte.AccessibleName = resources.GetString("lbbyte.AccessibleName");
			this.lbbyte.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbbyte.Anchor")));
			this.lbbyte.AutoSize = ((bool)(resources.GetObject("lbbyte.AutoSize")));
			this.lbbyte.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbbyte.Dock")));
			this.lbbyte.Enabled = ((bool)(resources.GetObject("lbbyte.Enabled")));
			this.lbbyte.Font = ((System.Drawing.Font)(resources.GetObject("lbbyte.Font")));
			this.lbbyte.Image = ((System.Drawing.Image)(resources.GetObject("lbbyte.Image")));
			this.lbbyte.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbbyte.ImageAlign")));
			this.lbbyte.ImageIndex = ((int)(resources.GetObject("lbbyte.ImageIndex")));
			this.lbbyte.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbbyte.ImeMode")));
			this.lbbyte.Location = ((System.Drawing.Point)(resources.GetObject("lbbyte.Location")));
			this.lbbyte.Name = "lbbyte";
			this.lbbyte.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbbyte.RightToLeft")));
			this.lbbyte.Size = ((System.Drawing.Size)(resources.GetObject("lbbyte.Size")));
			this.lbbyte.TabIndex = ((int)(resources.GetObject("lbbyte.TabIndex")));
			this.lbbyte.Text = resources.GetString("lbbyte.Text");
			this.lbbyte.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbbyte.TextAlign")));
			this.lbbyte.Visible = ((bool)(resources.GetObject("lbbyte.Visible")));
			this.visualStyleProvider1.SetVisualStyleSupport(this.lbbyte, true);
			// 
			// btgoto
			// 
			this.btgoto.AccessibleDescription = resources.GetString("btgoto.AccessibleDescription");
			this.btgoto.AccessibleName = resources.GetString("btgoto.AccessibleName");
			this.btgoto.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btgoto.Anchor")));
			this.btgoto.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btgoto.BackgroundImage")));
			this.btgoto.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btgoto.Dock")));
			this.btgoto.Enabled = ((bool)(resources.GetObject("btgoto.Enabled")));
			this.btgoto.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btgoto.FlatStyle")));
			this.btgoto.Font = ((System.Drawing.Font)(resources.GetObject("btgoto.Font")));
			this.btgoto.Image = ((System.Drawing.Image)(resources.GetObject("btgoto.Image")));
			this.btgoto.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btgoto.ImageAlign")));
			this.btgoto.ImageIndex = ((int)(resources.GetObject("btgoto.ImageIndex")));
			this.btgoto.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btgoto.ImeMode")));
			this.btgoto.Location = ((System.Drawing.Point)(resources.GetObject("btgoto.Location")));
			this.btgoto.Name = "btgoto";
			this.btgoto.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btgoto.RightToLeft")));
			this.btgoto.Size = ((System.Drawing.Size)(resources.GetObject("btgoto.Size")));
			this.btgoto.TabIndex = ((int)(resources.GetObject("btgoto.TabIndex")));
			this.btgoto.Text = resources.GetString("btgoto.Text");
			this.btgoto.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btgoto.TextAlign")));
			this.btgoto.Visible = ((bool)(resources.GetObject("btgoto.Visible")));
			this.visualStyleProvider1.SetVisualStyleSupport(this.btgoto, true);
			this.btgoto.Click += new System.EventHandler(this.btgoto_Click);
			// 
			// tboffset
			// 
			this.tboffset.AccessibleDescription = resources.GetString("tboffset.AccessibleDescription");
			this.tboffset.AccessibleName = resources.GetString("tboffset.AccessibleName");
			this.tboffset.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tboffset.Anchor")));
			this.tboffset.AutoSize = ((bool)(resources.GetObject("tboffset.AutoSize")));
			this.tboffset.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tboffset.BackgroundImage")));
			this.tboffset.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tboffset.Dock")));
			this.tboffset.Enabled = ((bool)(resources.GetObject("tboffset.Enabled")));
			this.tboffset.Font = ((System.Drawing.Font)(resources.GetObject("tboffset.Font")));
			this.tboffset.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tboffset.ImeMode")));
			this.tboffset.Location = ((System.Drawing.Point)(resources.GetObject("tboffset.Location")));
			this.tboffset.MaxLength = ((int)(resources.GetObject("tboffset.MaxLength")));
			this.tboffset.Multiline = ((bool)(resources.GetObject("tboffset.Multiline")));
			this.tboffset.Name = "tboffset";
			this.tboffset.PasswordChar = ((char)(resources.GetObject("tboffset.PasswordChar")));
			this.tboffset.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tboffset.RightToLeft")));
			this.tboffset.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tboffset.ScrollBars")));
			this.tboffset.Size = ((System.Drawing.Size)(resources.GetObject("tboffset.Size")));
			this.tboffset.TabIndex = ((int)(resources.GetObject("tboffset.TabIndex")));
			this.tboffset.Text = resources.GetString("tboffset.Text");
			this.tboffset.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tboffset.TextAlign")));
			this.tboffset.Visible = ((bool)(resources.GetObject("tboffset.Visible")));
			this.visualStyleProvider1.SetVisualStyleSupport(this.tboffset, true);
			this.tboffset.WordWrap = ((bool)(resources.GetObject("tboffset.WordWrap")));
			// 
			// panel2
			// 
			this.panel2.AccessibleDescription = resources.GetString("panel2.AccessibleDescription");
			this.panel2.AccessibleName = resources.GetString("panel2.AccessibleName");
			this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("panel2.Anchor")));
			this.panel2.AutoScroll = ((bool)(resources.GetObject("panel2.AutoScroll")));
			this.panel2.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("panel2.AutoScrollMargin")));
			this.panel2.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("panel2.AutoScrollMinSize")));
			this.panel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel2.BackgroundImage")));
			this.panel2.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("panel2.Dock")));
			this.panel2.Enabled = ((bool)(resources.GetObject("panel2.Enabled")));
			this.panel2.Font = ((System.Drawing.Font)(resources.GetObject("panel2.Font")));
			this.panel2.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("panel2.ImeMode")));
			this.panel2.Location = ((System.Drawing.Point)(resources.GetObject("panel2.Location")));
			this.panel2.Name = "panel2";
			this.panel2.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("panel2.RightToLeft")));
			this.panel2.Size = ((System.Drawing.Size)(resources.GetObject("panel2.Size")));
			this.panel2.TabIndex = ((int)(resources.GetObject("panel2.TabIndex")));
			this.panel2.Text = resources.GetString("panel2.Text");
			this.panel2.Visible = ((bool)(resources.GetObject("panel2.Visible")));
			this.visualStyleProvider1.SetVisualStyleSupport(this.panel2, true);
			// 
			// tabPage2
			// 
			this.tabPage2.AccessibleDescription = resources.GetString("tabPage2.AccessibleDescription");
			this.tabPage2.AccessibleName = resources.GetString("tabPage2.AccessibleName");
			this.tabPage2.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tabPage2.Anchor")));
			this.tabPage2.AutoScroll = ((bool)(resources.GetObject("tabPage2.AutoScroll")));
			this.tabPage2.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("tabPage2.AutoScrollMargin")));
			this.tabPage2.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("tabPage2.AutoScrollMinSize")));
			this.tabPage2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tabPage2.BackgroundImage")));
			this.tabPage2.Controls.Add(this.llimpraw);
			this.tabPage2.Controls.Add(this.llexportraw);
			this.tabPage2.Controls.Add(this.panel1);
			this.tabPage2.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tabPage2.Dock")));
			this.tabPage2.Enabled = ((bool)(resources.GetObject("tabPage2.Enabled")));
			this.tabPage2.Font = ((System.Drawing.Font)(resources.GetObject("tabPage2.Font")));
			this.tabPage2.ImageIndex = ((int)(resources.GetObject("tabPage2.ImageIndex")));
			this.tabPage2.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tabPage2.ImeMode")));
			this.tabPage2.Location = ((System.Drawing.Point)(resources.GetObject("tabPage2.Location")));
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tabPage2.RightToLeft")));
			this.tabPage2.Size = ((System.Drawing.Size)(resources.GetObject("tabPage2.Size")));
			this.tabPage2.TabIndex = ((int)(resources.GetObject("tabPage2.TabIndex")));
			this.tabPage2.Text = resources.GetString("tabPage2.Text");
			this.tabPage2.ToolTipText = resources.GetString("tabPage2.ToolTipText");
			this.tabPage2.Visible = ((bool)(resources.GetObject("tabPage2.Visible")));
			this.visualStyleProvider1.SetVisualStyleSupport(this.tabPage2, true);
			// 
			// llimpraw
			// 
			this.llimpraw.AccessibleDescription = resources.GetString("llimpraw.AccessibleDescription");
			this.llimpraw.AccessibleName = resources.GetString("llimpraw.AccessibleName");
			this.llimpraw.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("llimpraw.Anchor")));
			this.llimpraw.AutoSize = ((bool)(resources.GetObject("llimpraw.AutoSize")));
			this.llimpraw.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("llimpraw.Dock")));
			this.llimpraw.Enabled = ((bool)(resources.GetObject("llimpraw.Enabled")));
			this.llimpraw.Font = ((System.Drawing.Font)(resources.GetObject("llimpraw.Font")));
			this.llimpraw.Image = ((System.Drawing.Image)(resources.GetObject("llimpraw.Image")));
			this.llimpraw.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("llimpraw.ImageAlign")));
			this.llimpraw.ImageIndex = ((int)(resources.GetObject("llimpraw.ImageIndex")));
			this.llimpraw.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("llimpraw.ImeMode")));
			this.llimpraw.LinkArea = ((System.Windows.Forms.LinkArea)(resources.GetObject("llimpraw.LinkArea")));
			this.llimpraw.Location = ((System.Drawing.Point)(resources.GetObject("llimpraw.Location")));
			this.llimpraw.Name = "llimpraw";
			this.llimpraw.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("llimpraw.RightToLeft")));
			this.llimpraw.Size = ((System.Drawing.Size)(resources.GetObject("llimpraw.Size")));
			this.llimpraw.TabIndex = ((int)(resources.GetObject("llimpraw.TabIndex")));
			this.llimpraw.TabStop = true;
			this.llimpraw.Text = resources.GetString("llimpraw.Text");
			this.llimpraw.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("llimpraw.TextAlign")));
			this.llimpraw.Visible = ((bool)(resources.GetObject("llimpraw.Visible")));
			this.llimpraw.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llimpraw_LinkClicked);
			// 
			// llexportraw
			// 
			this.llexportraw.AccessibleDescription = resources.GetString("llexportraw.AccessibleDescription");
			this.llexportraw.AccessibleName = resources.GetString("llexportraw.AccessibleName");
			this.llexportraw.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("llexportraw.Anchor")));
			this.llexportraw.AutoSize = ((bool)(resources.GetObject("llexportraw.AutoSize")));
			this.llexportraw.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("llexportraw.Dock")));
			this.llexportraw.Enabled = ((bool)(resources.GetObject("llexportraw.Enabled")));
			this.llexportraw.Font = ((System.Drawing.Font)(resources.GetObject("llexportraw.Font")));
			this.llexportraw.Image = ((System.Drawing.Image)(resources.GetObject("llexportraw.Image")));
			this.llexportraw.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("llexportraw.ImageAlign")));
			this.llexportraw.ImageIndex = ((int)(resources.GetObject("llexportraw.ImageIndex")));
			this.llexportraw.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("llexportraw.ImeMode")));
			this.llexportraw.LinkArea = ((System.Windows.Forms.LinkArea)(resources.GetObject("llexportraw.LinkArea")));
			this.llexportraw.Location = ((System.Drawing.Point)(resources.GetObject("llexportraw.Location")));
			this.llexportraw.Name = "llexportraw";
			this.llexportraw.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("llexportraw.RightToLeft")));
			this.llexportraw.Size = ((System.Drawing.Size)(resources.GetObject("llexportraw.Size")));
			this.llexportraw.TabIndex = ((int)(resources.GetObject("llexportraw.TabIndex")));
			this.llexportraw.TabStop = true;
			this.llexportraw.Text = resources.GetString("llexportraw.Text");
			this.llexportraw.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("llexportraw.TextAlign")));
			this.llexportraw.Visible = ((bool)(resources.GetObject("llexportraw.Visible")));
			this.llexportraw.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llexportraw_LinkClicked);
			// 
			// panel1
			// 
			this.panel1.AccessibleDescription = resources.GetString("panel1.AccessibleDescription");
			this.panel1.AccessibleName = resources.GetString("panel1.AccessibleName");
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("panel1.Anchor")));
			this.panel1.AutoScroll = ((bool)(resources.GetObject("panel1.AutoScroll")));
			this.panel1.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("panel1.AutoScrollMargin")));
			this.panel1.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("panel1.AutoScrollMinSize")));
			this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
			this.panel1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("panel1.Dock")));
			this.panel1.Enabled = ((bool)(resources.GetObject("panel1.Enabled")));
			this.panel1.Font = ((System.Drawing.Font)(resources.GetObject("panel1.Font")));
			this.panel1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("panel1.ImeMode")));
			this.panel1.Location = ((System.Drawing.Point)(resources.GetObject("panel1.Location")));
			this.panel1.Name = "panel1";
			this.panel1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("panel1.RightToLeft")));
			this.panel1.Size = ((System.Drawing.Size)(resources.GetObject("panel1.Size")));
			this.panel1.TabIndex = ((int)(resources.GetObject("panel1.TabIndex")));
			this.panel1.Text = resources.GetString("panel1.Text");
			this.panel1.Visible = ((bool)(resources.GetObject("panel1.Visible")));
			this.visualStyleProvider1.SetVisualStyleSupport(this.panel1, true);
			// 
			// tabPage5
			// 
			this.tabPage5.AccessibleDescription = resources.GetString("tabPage5.AccessibleDescription");
			this.tabPage5.AccessibleName = resources.GetString("tabPage5.AccessibleName");
			this.tabPage5.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tabPage5.Anchor")));
			this.tabPage5.AutoScroll = ((bool)(resources.GetObject("tabPage5.AutoScroll")));
			this.tabPage5.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("tabPage5.AutoScrollMargin")));
			this.tabPage5.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("tabPage5.AutoScrollMinSize")));
			this.tabPage5.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tabPage5.BackgroundImage")));
			this.tabPage5.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tabPage5.Dock")));
			this.tabPage5.Enabled = ((bool)(resources.GetObject("tabPage5.Enabled")));
			this.tabPage5.Font = ((System.Drawing.Font)(resources.GetObject("tabPage5.Font")));
			this.tabPage5.ImageIndex = ((int)(resources.GetObject("tabPage5.ImageIndex")));
			this.tabPage5.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tabPage5.ImeMode")));
			this.tabPage5.Location = ((System.Drawing.Point)(resources.GetObject("tabPage5.Location")));
			this.tabPage5.Name = "tabPage5";
			this.tabPage5.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tabPage5.RightToLeft")));
			this.tabPage5.Size = ((System.Drawing.Size)(resources.GetObject("tabPage5.Size")));
			this.tabPage5.TabIndex = ((int)(resources.GetObject("tabPage5.TabIndex")));
			this.tabPage5.Text = resources.GetString("tabPage5.Text");
			this.tabPage5.ToolTipText = resources.GetString("tabPage5.ToolTipText");
			this.tabPage5.Visible = ((bool)(resources.GetObject("tabPage5.Visible")));
			this.visualStyleProvider1.SetVisualStyleSupport(this.tabPage5, true);
			// 
			// label4
			// 
			this.label4.AccessibleDescription = resources.GetString("label4.AccessibleDescription");
			this.label4.AccessibleName = resources.GetString("label4.AccessibleName");
			this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label4.Anchor")));
			this.label4.AutoSize = ((bool)(resources.GetObject("label4.AutoSize")));
			this.label4.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label4.Dock")));
			this.label4.Enabled = ((bool)(resources.GetObject("label4.Enabled")));
			this.label4.Font = ((System.Drawing.Font)(resources.GetObject("label4.Font")));
			this.label4.Image = ((System.Drawing.Image)(resources.GetObject("label4.Image")));
			this.label4.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label4.ImageAlign")));
			this.label4.ImageIndex = ((int)(resources.GetObject("label4.ImageIndex")));
			this.label4.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label4.ImeMode")));
			this.label4.Location = ((System.Drawing.Point)(resources.GetObject("label4.Location")));
			this.label4.Name = "label4";
			this.label4.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label4.RightToLeft")));
			this.label4.Size = ((System.Drawing.Size)(resources.GetObject("label4.Size")));
			this.label4.TabIndex = ((int)(resources.GetObject("label4.TabIndex")));
			this.label4.Text = resources.GetString("label4.Text");
			this.label4.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label4.TextAlign")));
			this.label4.Visible = ((bool)(resources.GetObject("label4.Visible")));
			this.visualStyleProvider1.SetVisualStyleSupport(this.label4, true);
			// 
			// label7
			// 
			this.label7.AccessibleDescription = resources.GetString("label7.AccessibleDescription");
			this.label7.AccessibleName = resources.GetString("label7.AccessibleName");
			this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label7.Anchor")));
			this.label7.AutoSize = ((bool)(resources.GetObject("label7.AutoSize")));
			this.label7.ContextMenu = this.PackedFileMenu;
			this.label7.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label7.Dock")));
			this.label7.Enabled = ((bool)(resources.GetObject("label7.Enabled")));
			this.label7.Font = ((System.Drawing.Font)(resources.GetObject("label7.Font")));
			this.label7.Image = ((System.Drawing.Image)(resources.GetObject("label7.Image")));
			this.label7.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label7.ImageAlign")));
			this.label7.ImageIndex = ((int)(resources.GetObject("label7.ImageIndex")));
			this.label7.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label7.ImeMode")));
			this.label7.Location = ((System.Drawing.Point)(resources.GetObject("label7.Location")));
			this.label7.Name = "label7";
			this.label7.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label7.RightToLeft")));
			this.label7.Size = ((System.Drawing.Size)(resources.GetObject("label7.Size")));
			this.label7.TabIndex = ((int)(resources.GetObject("label7.TabIndex")));
			this.label7.Text = resources.GetString("label7.Text");
			this.label7.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label7.TextAlign")));
			this.label7.Visible = ((bool)(resources.GetObject("label7.Visible")));
			this.visualStyleProvider1.SetVisualStyleSupport(this.label7, true);
			// 
			// lbcount
			// 
			this.lbcount.AccessibleDescription = resources.GetString("lbcount.AccessibleDescription");
			this.lbcount.AccessibleName = resources.GetString("lbcount.AccessibleName");
			this.lbcount.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbcount.Anchor")));
			this.lbcount.AutoSize = ((bool)(resources.GetObject("lbcount.AutoSize")));
			this.lbcount.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbcount.Dock")));
			this.lbcount.Enabled = ((bool)(resources.GetObject("lbcount.Enabled")));
			this.lbcount.Font = ((System.Drawing.Font)(resources.GetObject("lbcount.Font")));
			this.lbcount.Image = ((System.Drawing.Image)(resources.GetObject("lbcount.Image")));
			this.lbcount.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbcount.ImageAlign")));
			this.lbcount.ImageIndex = ((int)(resources.GetObject("lbcount.ImageIndex")));
			this.lbcount.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbcount.ImeMode")));
			this.lbcount.Location = ((System.Drawing.Point)(resources.GetObject("lbcount.Location")));
			this.lbcount.Name = "lbcount";
			this.lbcount.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbcount.RightToLeft")));
			this.lbcount.Size = ((System.Drawing.Size)(resources.GetObject("lbcount.Size")));
			this.lbcount.TabIndex = ((int)(resources.GetObject("lbcount.TabIndex")));
			this.lbcount.Text = resources.GetString("lbcount.Text");
			this.lbcount.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbcount.TextAlign")));
			this.lbcount.Visible = ((bool)(resources.GetObject("lbcount.Visible")));
			this.visualStyleProvider1.SetVisualStyleSupport(this.lbcount, true);
			// 
			// ofd
			// 
			this.ofd.Filter = resources.GetString("ofd.Filter");
			this.ofd.Title = resources.GetString("ofd.Title");
			// 
			// lbtype
			// 
			this.lbtype.AccessibleDescription = resources.GetString("lbtype.AccessibleDescription");
			this.lbtype.AccessibleName = resources.GetString("lbtype.AccessibleName");
			this.lbtype.AllowDrop = true;
			this.lbtype.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbtype.Anchor")));
			this.lbtype.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("lbtype.BackgroundImage")));
			this.lbtype.ColumnWidth = ((int)(resources.GetObject("lbtype.ColumnWidth")));
			this.lbtype.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbtype.Dock")));
			this.lbtype.Enabled = ((bool)(resources.GetObject("lbtype.Enabled")));
			this.lbtype.Font = ((System.Drawing.Font)(resources.GetObject("lbtype.Font")));
			this.lbtype.HorizontalExtent = ((int)(resources.GetObject("lbtype.HorizontalExtent")));
			this.lbtype.HorizontalScrollbar = ((bool)(resources.GetObject("lbtype.HorizontalScrollbar")));
			this.lbtype.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbtype.ImeMode")));
			this.lbtype.IntegralHeight = ((bool)(resources.GetObject("lbtype.IntegralHeight")));
			this.lbtype.ItemHeight = ((int)(resources.GetObject("lbtype.ItemHeight")));
			this.lbtype.Location = ((System.Drawing.Point)(resources.GetObject("lbtype.Location")));
			this.lbtype.Name = "lbtype";
			this.lbtype.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbtype.RightToLeft")));
			this.lbtype.ScrollAlwaysVisible = ((bool)(resources.GetObject("lbtype.ScrollAlwaysVisible")));
			this.lbtype.Size = ((System.Drawing.Size)(resources.GetObject("lbtype.Size")));
			this.lbtype.TabIndex = ((int)(resources.GetObject("lbtype.TabIndex")));
			this.lbtype.Visible = ((bool)(resources.GetObject("lbtype.Visible")));
			this.lbtype.DragDrop += new System.Windows.Forms.DragEventHandler(this.DragDropFile);
			this.lbtype.DragEnter += new System.Windows.Forms.DragEventHandler(this.DragEnterFile);
			this.lbtype.SelectedIndexChanged += new System.EventHandler(this.UpdateFileGroupFilter);
			// 
			// sfd
			// 
			this.sfd.Filter = resources.GetString("sfd.Filter");
			this.sfd.Title = resources.GetString("sfd.Title");
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem1,
																					  this.menuItem2,
																					  this.menuItem5,
																					  this.miPlugins,
																					  this.menuItem3});
			this.mainMenu1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("mainMenu1.RightToLeft")));
			// 
			// menuItem1
			// 
			this.menuItem1.Enabled = ((bool)(resources.GetObject("menuItem1.Enabled")));
			this.menuItem1.Index = 0;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.minew,
																					  this.miopen,
																					  this.misave,
																					  this.misaveas,
																					  this.miclose,
																					  this.mirecent,
																					  this.menuItem4,
																					  this.miadd,
																					  this.miextract,
																					  this.miss2cp,
																					  this.menuItem6,
																					  this.miexit});
			this.menuItem1.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("menuItem1.Shortcut")));
			this.menuItem1.ShowShortcut = ((bool)(resources.GetObject("menuItem1.ShowShortcut")));
			this.menuItem1.Text = resources.GetString("menuItem1.Text");
			this.menuItem1.Visible = ((bool)(resources.GetObject("menuItem1.Visible")));
			// 
			// minew
			// 
			this.minew.Enabled = ((bool)(resources.GetObject("minew.Enabled")));
			this.minew.Index = 0;
			this.minew.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("minew.Shortcut")));
			this.minew.ShowShortcut = ((bool)(resources.GetObject("minew.ShowShortcut")));
			this.minew.Text = resources.GetString("minew.Text");
			this.minew.Visible = ((bool)(resources.GetObject("minew.Visible")));
			this.minew.Click += new System.EventHandler(this.CreateNewPackageClick);
			// 
			// miopen
			// 
			this.miopen.Enabled = ((bool)(resources.GetObject("miopen.Enabled")));
			this.miopen.Index = 1;
			this.miopen.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("miopen.Shortcut")));
			this.miopen.ShowShortcut = ((bool)(resources.GetObject("miopen.ShowShortcut")));
			this.miopen.Text = resources.GetString("miopen.Text");
			this.miopen.Visible = ((bool)(resources.GetObject("miopen.Visible")));
			this.miopen.Click += new System.EventHandler(this.OpenPackageClick);
			// 
			// misave
			// 
			this.misave.Enabled = ((bool)(resources.GetObject("misave.Enabled")));
			this.misave.Index = 2;
			this.misave.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("misave.Shortcut")));
			this.misave.ShowShortcut = ((bool)(resources.GetObject("misave.ShowShortcut")));
			this.misave.Text = resources.GetString("misave.Text");
			this.misave.Visible = ((bool)(resources.GetObject("misave.Visible")));
			this.misave.Click += new System.EventHandler(this.OnInstantSaveClick);
			// 
			// misaveas
			// 
			this.misaveas.Enabled = ((bool)(resources.GetObject("misaveas.Enabled")));
			this.misaveas.Index = 3;
			this.misaveas.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("misaveas.Shortcut")));
			this.misaveas.ShowShortcut = ((bool)(resources.GetObject("misaveas.ShowShortcut")));
			this.misaveas.Text = resources.GetString("misaveas.Text");
			this.misaveas.Visible = ((bool)(resources.GetObject("misaveas.Visible")));
			this.misaveas.Click += new System.EventHandler(this.SavePackageClick);
			// 
			// miclose
			// 
			this.miclose.Enabled = ((bool)(resources.GetObject("miclose.Enabled")));
			this.miclose.Index = 4;
			this.miclose.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("miclose.Shortcut")));
			this.miclose.ShowShortcut = ((bool)(resources.GetObject("miclose.ShowShortcut")));
			this.miclose.Text = resources.GetString("miclose.Text");
			this.miclose.Visible = ((bool)(resources.GetObject("miclose.Visible")));
			this.miclose.Click += new System.EventHandler(this.CloseFileHandleClick);
			// 
			// mirecent
			// 
			this.mirecent.Enabled = ((bool)(resources.GetObject("mirecent.Enabled")));
			this.mirecent.Index = 5;
			this.mirecent.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("mirecent.Shortcut")));
			this.mirecent.ShowShortcut = ((bool)(resources.GetObject("mirecent.ShowShortcut")));
			this.mirecent.Text = resources.GetString("mirecent.Text");
			this.mirecent.Visible = ((bool)(resources.GetObject("mirecent.Visible")));
			// 
			// menuItem4
			// 
			this.menuItem4.Enabled = ((bool)(resources.GetObject("menuItem4.Enabled")));
			this.menuItem4.Index = 6;
			this.menuItem4.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("menuItem4.Shortcut")));
			this.menuItem4.ShowShortcut = ((bool)(resources.GetObject("menuItem4.ShowShortcut")));
			this.menuItem4.Text = resources.GetString("menuItem4.Text");
			this.menuItem4.Visible = ((bool)(resources.GetObject("menuItem4.Visible")));
			// 
			// miadd
			// 
			this.miadd.Enabled = ((bool)(resources.GetObject("miadd.Enabled")));
			this.miadd.Index = 7;
			this.miadd.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("miadd.Shortcut")));
			this.miadd.ShowShortcut = ((bool)(resources.GetObject("miadd.ShowShortcut")));
			this.miadd.Text = resources.GetString("miadd.Text");
			this.miadd.Visible = ((bool)(resources.GetObject("miadd.Visible")));
			this.miadd.Click += new System.EventHandler(this.AddPackedFileClick);
			// 
			// miextract
			// 
			this.miextract.Enabled = ((bool)(resources.GetObject("miextract.Enabled")));
			this.miextract.Index = 8;
			this.miextract.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("miextract.Shortcut")));
			this.miextract.ShowShortcut = ((bool)(resources.GetObject("miextract.ShowShortcut")));
			this.miextract.Text = resources.GetString("miextract.Text");
			this.miextract.Visible = ((bool)(resources.GetObject("miextract.Visible")));
			this.miextract.Click += new System.EventHandler(this.ExtractAllClick);
			// 
			// miss2cp
			// 
			this.miss2cp.Enabled = ((bool)(resources.GetObject("miss2cp.Enabled")));
			this.miss2cp.Index = 9;
			this.miss2cp.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("miss2cp.Shortcut")));
			this.miss2cp.ShowShortcut = ((bool)(resources.GetObject("miss2cp.ShowShortcut")));
			this.miss2cp.Text = resources.GetString("miss2cp.Text");
			this.miss2cp.Visible = ((bool)(resources.GetObject("miss2cp.Visible")));
			this.miss2cp.Click += new System.EventHandler(this.SaveS2CP);
			// 
			// menuItem6
			// 
			this.menuItem6.Enabled = ((bool)(resources.GetObject("menuItem6.Enabled")));
			this.menuItem6.Index = 10;
			this.menuItem6.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("menuItem6.Shortcut")));
			this.menuItem6.ShowShortcut = ((bool)(resources.GetObject("menuItem6.ShowShortcut")));
			this.menuItem6.Text = resources.GetString("menuItem6.Text");
			this.menuItem6.Visible = ((bool)(resources.GetObject("menuItem6.Visible")));
			// 
			// miexit
			// 
			this.miexit.Enabled = ((bool)(resources.GetObject("miexit.Enabled")));
			this.miexit.Index = 11;
			this.miexit.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("miexit.Shortcut")));
			this.miexit.ShowShortcut = ((bool)(resources.GetObject("miexit.ShowShortcut")));
			this.miexit.Text = resources.GetString("miexit.Text");
			this.miexit.Visible = ((bool)(resources.GetObject("miexit.Visible")));
			this.miexit.Click += new System.EventHandler(this.ExitClick);
			// 
			// menuItem2
			// 
			this.menuItem2.Enabled = ((bool)(resources.GetObject("menuItem2.Enabled")));
			this.menuItem2.Index = 1;
			this.menuItem2.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.mifoldercompare,
																					  this.mirunsims,
																					  this.mihexdec,
																					  this.menuItem7,
																					  this.miintrigued,
																					  this.mifix,
																					  this.mireload,
																					  this.milistsims,
																					  this.mimem,
																					  this.menuItem9,
																					  this.mis2cpid,
																					  this.minamemap});
			this.menuItem2.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("menuItem2.Shortcut")));
			this.menuItem2.ShowShortcut = ((bool)(resources.GetObject("menuItem2.ShowShortcut")));
			this.menuItem2.Text = resources.GetString("menuItem2.Text");
			this.menuItem2.Visible = ((bool)(resources.GetObject("menuItem2.Visible")));
			// 
			// mifoldercompare
			// 
			this.mifoldercompare.Enabled = ((bool)(resources.GetObject("mifoldercompare.Enabled")));
			this.mifoldercompare.Index = 0;
			this.mifoldercompare.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("mifoldercompare.Shortcut")));
			this.mifoldercompare.ShowShortcut = ((bool)(resources.GetObject("mifoldercompare.ShowShortcut")));
			this.mifoldercompare.Text = resources.GetString("mifoldercompare.Text");
			this.mifoldercompare.Visible = ((bool)(resources.GetObject("mifoldercompare.Visible")));
			this.mifoldercompare.Click += new System.EventHandler(this.FolderCompareClick);
			// 
			// mirunsims
			// 
			this.mirunsims.Enabled = ((bool)(resources.GetObject("mirunsims.Enabled")));
			this.mirunsims.Index = 1;
			this.mirunsims.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("mirunsims.Shortcut")));
			this.mirunsims.ShowShortcut = ((bool)(resources.GetObject("mirunsims.ShowShortcut")));
			this.mirunsims.Text = resources.GetString("mirunsims.Text");
			this.mirunsims.Visible = ((bool)(resources.GetObject("mirunsims.Visible")));
			this.mirunsims.Click += new System.EventHandler(this.RunSims2Clicked);
			// 
			// mihexdec
			// 
			this.mihexdec.Enabled = ((bool)(resources.GetObject("mihexdec.Enabled")));
			this.mihexdec.Index = 2;
			this.mihexdec.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("mihexdec.Shortcut")));
			this.mihexdec.ShowShortcut = ((bool)(resources.GetObject("mihexdec.ShowShortcut")));
			this.mihexdec.Text = resources.GetString("mihexdec.Text");
			this.mihexdec.Visible = ((bool)(resources.GetObject("mihexdec.Visible")));
			this.mihexdec.Click += new System.EventHandler(this.HexDecConverterStart);
			// 
			// menuItem7
			// 
			this.menuItem7.Enabled = ((bool)(resources.GetObject("menuItem7.Enabled")));
			this.menuItem7.Index = 3;
			this.menuItem7.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("menuItem7.Shortcut")));
			this.menuItem7.ShowShortcut = ((bool)(resources.GetObject("menuItem7.ShowShortcut")));
			this.menuItem7.Text = resources.GetString("menuItem7.Text");
			this.menuItem7.Visible = ((bool)(resources.GetObject("menuItem7.Visible")));
			// 
			// miintrigued
			// 
			this.miintrigued.Enabled = ((bool)(resources.GetObject("miintrigued.Enabled")));
			this.miintrigued.Index = 4;
			this.miintrigued.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("miintrigued.Shortcut")));
			this.miintrigued.ShowShortcut = ((bool)(resources.GetObject("miintrigued.ShowShortcut")));
			this.miintrigued.Text = resources.GetString("miintrigued.Text");
			this.miintrigued.Visible = ((bool)(resources.GetObject("miintrigued.Visible")));
			this.miintrigued.Click += new System.EventHandler(this.MakeIntriguedNeighborhhodClick);
			// 
			// mifix
			// 
			this.mifix.Enabled = ((bool)(resources.GetObject("mifix.Enabled")));
			this.mifix.Index = 5;
			this.mifix.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("mifix.Shortcut")));
			this.mifix.ShowShortcut = ((bool)(resources.GetObject("mifix.ShowShortcut")));
			this.mifix.Text = resources.GetString("mifix.Text");
			this.mifix.Visible = ((bool)(resources.GetObject("mifix.Visible")));
			this.mifix.Click += new System.EventHandler(this.FixIntegrity);
			// 
			// mireload
			// 
			this.mireload.Enabled = ((bool)(resources.GetObject("mireload.Enabled")));
			this.mireload.Index = 6;
			this.mireload.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("mireload.Shortcut")));
			this.mireload.ShowShortcut = ((bool)(resources.GetObject("mireload.ShowShortcut")));
			this.mireload.Text = resources.GetString("mireload.Text");
			this.mireload.Visible = ((bool)(resources.GetObject("mireload.Visible")));
			this.mireload.Click += new System.EventHandler(this.ReloadFileTable);
			// 
			// milistsims
			// 
			this.milistsims.Enabled = ((bool)(resources.GetObject("milistsims.Enabled")));
			this.milistsims.Index = 7;
			this.milistsims.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("milistsims.Shortcut")));
			this.milistsims.ShowShortcut = ((bool)(resources.GetObject("milistsims.ShowShortcut")));
			this.milistsims.Text = resources.GetString("milistsims.Text");
			this.milistsims.Visible = ((bool)(resources.GetObject("milistsims.Visible")));
			this.milistsims.Click += new System.EventHandler(this.ListSimsClicked);
			// 
			// mimem
			// 
			this.mimem.Enabled = ((bool)(resources.GetObject("mimem.Enabled")));
			this.mimem.Index = 8;
			this.mimem.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("mimem.Shortcut")));
			this.mimem.ShowShortcut = ((bool)(resources.GetObject("mimem.ShowShortcut")));
			this.mimem.Text = resources.GetString("mimem.Text");
			this.mimem.Visible = ((bool)(resources.GetObject("mimem.Visible")));
			this.mimem.Click += new System.EventHandler(this.ListMemoriesClick);
			// 
			// menuItem9
			// 
			this.menuItem9.Enabled = ((bool)(resources.GetObject("menuItem9.Enabled")));
			this.menuItem9.Index = 9;
			this.menuItem9.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.mifilelist,
																					  this.mibuildlist,
																					  this.micopyright});
			this.menuItem9.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("menuItem9.Shortcut")));
			this.menuItem9.ShowShortcut = ((bool)(resources.GetObject("menuItem9.ShowShortcut")));
			this.menuItem9.Text = resources.GetString("menuItem9.Text");
			this.menuItem9.Visible = ((bool)(resources.GetObject("menuItem9.Visible")));
			// 
			// mifilelist
			// 
			this.mifilelist.Enabled = ((bool)(resources.GetObject("mifilelist.Enabled")));
			this.mifilelist.Index = 0;
			this.mifilelist.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("mifilelist.Shortcut")));
			this.mifilelist.ShowShortcut = ((bool)(resources.GetObject("mifilelist.ShowShortcut")));
			this.mifilelist.Text = resources.GetString("mifilelist.Text");
			this.mifilelist.Visible = ((bool)(resources.GetObject("mifilelist.Visible")));
			this.mifilelist.Click += new System.EventHandler(this.GenerateFileList);
			// 
			// mibuildlist
			// 
			this.mibuildlist.Enabled = ((bool)(resources.GetObject("mibuildlist.Enabled")));
			this.mibuildlist.Index = 1;
			this.mibuildlist.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("mibuildlist.Shortcut")));
			this.mibuildlist.ShowShortcut = ((bool)(resources.GetObject("mibuildlist.ShowShortcut")));
			this.mibuildlist.Text = resources.GetString("mibuildlist.Text");
			this.mibuildlist.Visible = ((bool)(resources.GetObject("mibuildlist.Visible")));
			this.mibuildlist.Click += new System.EventHandler(this.BuildFileList);
			// 
			// micopyright
			// 
			this.micopyright.Enabled = ((bool)(resources.GetObject("micopyright.Enabled")));
			this.micopyright.Index = 2;
			this.micopyright.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("micopyright.Shortcut")));
			this.micopyright.ShowShortcut = ((bool)(resources.GetObject("micopyright.ShowShortcut")));
			this.micopyright.Text = resources.GetString("micopyright.Text");
			this.micopyright.Visible = ((bool)(resources.GetObject("micopyright.Visible")));
			this.micopyright.Click += new System.EventHandler(this.AddCopyright);
			// 
			// mis2cpid
			// 
			this.mis2cpid.Enabled = ((bool)(resources.GetObject("mis2cpid.Enabled")));
			this.mis2cpid.Index = 10;
			this.mis2cpid.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("mis2cpid.Shortcut")));
			this.mis2cpid.ShowShortcut = ((bool)(resources.GetObject("mis2cpid.ShowShortcut")));
			this.mis2cpid.Text = resources.GetString("mis2cpid.Text");
			this.mis2cpid.Visible = ((bool)(resources.GetObject("mis2cpid.Visible")));
			this.mis2cpid.Click += new System.EventHandler(this.AddS2CPID);
			// 
			// minamemap
			// 
			this.minamemap.Enabled = ((bool)(resources.GetObject("minamemap.Enabled")));
			this.minamemap.Index = 11;
			this.minamemap.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("minamemap.Shortcut")));
			this.minamemap.ShowShortcut = ((bool)(resources.GetObject("minamemap.ShowShortcut")));
			this.minamemap.Text = resources.GetString("minamemap.Text");
			this.minamemap.Visible = ((bool)(resources.GetObject("minamemap.Visible")));
			this.minamemap.Click += new System.EventHandler(this.CreateNameMap);
			// 
			// menuItem5
			// 
			this.menuItem5.Enabled = ((bool)(resources.GetObject("menuItem5.Enabled")));
			this.menuItem5.Index = 2;
			this.menuItem5.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.mihexview,
																					  this.minometa,
																					  this.midecode,
																					  this.menuItem8,
																					  this.mioptions});
			this.menuItem5.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("menuItem5.Shortcut")));
			this.menuItem5.ShowShortcut = ((bool)(resources.GetObject("menuItem5.ShowShortcut")));
			this.menuItem5.Text = resources.GetString("menuItem5.Text");
			this.menuItem5.Visible = ((bool)(resources.GetObject("menuItem5.Visible")));
			// 
			// mihexview
			// 
			this.mihexview.Enabled = ((bool)(resources.GetObject("mihexview.Enabled")));
			this.mihexview.Index = 0;
			this.mihexview.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("mihexview.Shortcut")));
			this.mihexview.ShowShortcut = ((bool)(resources.GetObject("mihexview.ShowShortcut")));
			this.mihexview.Text = resources.GetString("mihexview.Text");
			this.mihexview.Visible = ((bool)(resources.GetObject("mihexview.Visible")));
			this.mihexview.Click += new System.EventHandler(this.UseHexViewClick);
			// 
			// minometa
			// 
			this.minometa.Enabled = ((bool)(resources.GetObject("minometa.Enabled")));
			this.minometa.Index = 1;
			this.minometa.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("minometa.Shortcut")));
			this.minometa.ShowShortcut = ((bool)(resources.GetObject("minometa.ShowShortcut")));
			this.minometa.Text = resources.GetString("minometa.Text");
			this.minometa.Visible = ((bool)(resources.GetObject("minometa.Visible")));
			this.minometa.Click += new System.EventHandler(this.NoMetaInformationClick);
			// 
			// midecode
			// 
			this.midecode.Enabled = ((bool)(resources.GetObject("midecode.Enabled")));
			this.midecode.Index = 2;
			this.midecode.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("midecode.Shortcut")));
			this.midecode.ShowShortcut = ((bool)(resources.GetObject("midecode.ShowShortcut")));
			this.midecode.Text = resources.GetString("midecode.Text");
			this.midecode.Visible = ((bool)(resources.GetObject("midecode.Visible")));
			this.midecode.Click += new System.EventHandler(this.DecodeFilenamesClicked);
			// 
			// menuItem8
			// 
			this.menuItem8.Enabled = ((bool)(resources.GetObject("menuItem8.Enabled")));
			this.menuItem8.Index = 3;
			this.menuItem8.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("menuItem8.Shortcut")));
			this.menuItem8.ShowShortcut = ((bool)(resources.GetObject("menuItem8.ShowShortcut")));
			this.menuItem8.Text = resources.GetString("menuItem8.Text");
			this.menuItem8.Visible = ((bool)(resources.GetObject("menuItem8.Visible")));
			// 
			// mioptions
			// 
			this.mioptions.Enabled = ((bool)(resources.GetObject("mioptions.Enabled")));
			this.mioptions.Index = 4;
			this.mioptions.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("mioptions.Shortcut")));
			this.mioptions.ShowShortcut = ((bool)(resources.GetObject("mioptions.ShowShortcut")));
			this.mioptions.Text = resources.GetString("mioptions.Text");
			this.mioptions.Visible = ((bool)(resources.GetObject("mioptions.Visible")));
			this.mioptions.Click += new System.EventHandler(this.OptionsOpenClick);
			// 
			// miPlugins
			// 
			this.miPlugins.Enabled = ((bool)(resources.GetObject("miPlugins.Enabled")));
			this.miPlugins.Index = 3;
			this.miPlugins.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.miinstplug,
																					  this.menuItem11});
			this.miPlugins.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("miPlugins.Shortcut")));
			this.miPlugins.ShowShortcut = ((bool)(resources.GetObject("miPlugins.ShowShortcut")));
			this.miPlugins.Text = resources.GetString("miPlugins.Text");
			this.miPlugins.Visible = ((bool)(resources.GetObject("miPlugins.Visible")));
			// 
			// miinstplug
			// 
			this.miinstplug.Enabled = ((bool)(resources.GetObject("miinstplug.Enabled")));
			this.miinstplug.Index = 0;
			this.miinstplug.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("miinstplug.Shortcut")));
			this.miinstplug.ShowShortcut = ((bool)(resources.GetObject("miinstplug.ShowShortcut")));
			this.miinstplug.Text = resources.GetString("miinstplug.Text");
			this.miinstplug.Visible = ((bool)(resources.GetObject("miinstplug.Visible")));
			this.miinstplug.Click += new System.EventHandler(this.InstalledPluginsClick);
			// 
			// menuItem11
			// 
			this.menuItem11.Enabled = ((bool)(resources.GetObject("menuItem11.Enabled")));
			this.menuItem11.Index = 1;
			this.menuItem11.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("menuItem11.Shortcut")));
			this.menuItem11.ShowShortcut = ((bool)(resources.GetObject("menuItem11.ShowShortcut")));
			this.menuItem11.Text = resources.GetString("menuItem11.Text");
			this.menuItem11.Visible = ((bool)(resources.GetObject("menuItem11.Visible")));
			// 
			// menuItem3
			// 
			this.menuItem3.Enabled = ((bool)(resources.GetObject("menuItem3.Enabled")));
			this.menuItem3.Index = 4;
			this.menuItem3.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem12,
																					  this.menuItem10,
																					  this.miAbout});
			this.menuItem3.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("menuItem3.Shortcut")));
			this.menuItem3.ShowShortcut = ((bool)(resources.GetObject("menuItem3.ShowShortcut")));
			this.menuItem3.Text = resources.GetString("menuItem3.Text");
			this.menuItem3.Visible = ((bool)(resources.GetObject("menuItem3.Visible")));
			// 
			// menuItem12
			// 
			this.menuItem12.Enabled = ((bool)(resources.GetObject("menuItem12.Enabled")));
			this.menuItem12.Index = 0;
			this.menuItem12.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("menuItem12.Shortcut")));
			this.menuItem12.ShowShortcut = ((bool)(resources.GetObject("menuItem12.ShowShortcut")));
			this.menuItem12.Text = resources.GetString("menuItem12.Text");
			this.menuItem12.Visible = ((bool)(resources.GetObject("menuItem12.Visible")));
			this.menuItem12.Click += new System.EventHandler(this.ShowTutorials);
			// 
			// menuItem10
			// 
			this.menuItem10.Enabled = ((bool)(resources.GetObject("menuItem10.Enabled")));
			this.menuItem10.Index = 1;
			this.menuItem10.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("menuItem10.Shortcut")));
			this.menuItem10.ShowShortcut = ((bool)(resources.GetObject("menuItem10.ShowShortcut")));
			this.menuItem10.Text = resources.GetString("menuItem10.Text");
			this.menuItem10.Visible = ((bool)(resources.GetObject("menuItem10.Visible")));
			this.menuItem10.Click += new System.EventHandler(this.CheckForOnlineUpdate);
			// 
			// miAbout
			// 
			this.miAbout.Enabled = ((bool)(resources.GetObject("miAbout.Enabled")));
			this.miAbout.Index = 2;
			this.miAbout.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("miAbout.Shortcut")));
			this.miAbout.ShowShortcut = ((bool)(resources.GetObject("miAbout.ShowShortcut")));
			this.miAbout.Text = resources.GetString("miAbout.Text");
			this.miAbout.Visible = ((bool)(resources.GetObject("miAbout.Visible")));
			this.miAbout.Click += new System.EventHandler(this.miAbout_Click);
			// 
			// fbd
			// 
			this.fbd.Description = resources.GetString("fbd.Description");
			this.fbd.SelectedPath = resources.GetString("fbd.SelectedPath");
			// 
			// pb1
			// 
			this.pb1.AccessibleDescription = resources.GetString("pb1.AccessibleDescription");
			this.pb1.AccessibleName = resources.GetString("pb1.AccessibleName");
			this.pb1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("pb1.Anchor")));
			this.pb1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pb1.BackgroundImage")));
			this.pb1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("pb1.Dock")));
			this.pb1.Enabled = ((bool)(resources.GetObject("pb1.Enabled")));
			this.pb1.Font = ((System.Drawing.Font)(resources.GetObject("pb1.Font")));
			this.pb1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("pb1.ImeMode")));
			this.pb1.Location = ((System.Drawing.Point)(resources.GetObject("pb1.Location")));
			this.pb1.Name = "pb1";
			this.pb1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("pb1.RightToLeft")));
			this.pb1.Size = ((System.Drawing.Size)(resources.GetObject("pb1.Size")));
			this.pb1.TabIndex = ((int)(resources.GetObject("pb1.TabIndex")));
			this.pb1.Text = resources.GetString("pb1.Text");
			this.pb1.Visible = ((bool)(resources.GetObject("pb1.Visible")));
			// 
			// label14
			// 
			this.label14.AccessibleDescription = resources.GetString("label14.AccessibleDescription");
			this.label14.AccessibleName = resources.GetString("label14.AccessibleName");
			this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label14.Anchor")));
			this.label14.AutoSize = ((bool)(resources.GetObject("label14.AutoSize")));
			this.label14.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label14.Dock")));
			this.label14.Enabled = ((bool)(resources.GetObject("label14.Enabled")));
			this.label14.Font = ((System.Drawing.Font)(resources.GetObject("label14.Font")));
			this.label14.Image = ((System.Drawing.Image)(resources.GetObject("label14.Image")));
			this.label14.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label14.ImageAlign")));
			this.label14.ImageIndex = ((int)(resources.GetObject("label14.ImageIndex")));
			this.label14.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label14.ImeMode")));
			this.label14.Location = ((System.Drawing.Point)(resources.GetObject("label14.Location")));
			this.label14.Name = "label14";
			this.label14.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label14.RightToLeft")));
			this.label14.Size = ((System.Drawing.Size)(resources.GetObject("label14.Size")));
			this.label14.TabIndex = ((int)(resources.GetObject("label14.TabIndex")));
			this.label14.Text = resources.GetString("label14.Text");
			this.label14.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label14.TextAlign")));
			this.label14.Visible = ((bool)(resources.GetObject("label14.Visible")));
			this.visualStyleProvider1.SetVisualStyleSupport(this.label14, true);
			// 
			// tbgr
			// 
			this.tbgr.AccessibleDescription = resources.GetString("tbgr.AccessibleDescription");
			this.tbgr.AccessibleName = resources.GetString("tbgr.AccessibleName");
			this.tbgr.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbgr.Anchor")));
			this.tbgr.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbgr.BackgroundImage")));
			this.tbgr.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbgr.Dock")));
			this.tbgr.Enabled = ((bool)(resources.GetObject("tbgr.Enabled")));
			this.tbgr.Font = ((System.Drawing.Font)(resources.GetObject("tbgr.Font")));
			this.tbgr.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbgr.ImeMode")));
			this.tbgr.IntegralHeight = ((bool)(resources.GetObject("tbgr.IntegralHeight")));
			this.tbgr.ItemHeight = ((int)(resources.GetObject("tbgr.ItemHeight")));
			this.tbgr.Items.AddRange(new object[] {
													  resources.GetString("tbgr.Items"),
													  resources.GetString("tbgr.Items1")});
			this.tbgr.Location = ((System.Drawing.Point)(resources.GetObject("tbgr.Location")));
			this.tbgr.MaxDropDownItems = ((int)(resources.GetObject("tbgr.MaxDropDownItems")));
			this.tbgr.MaxLength = ((int)(resources.GetObject("tbgr.MaxLength")));
			this.tbgr.Name = "tbgr";
			this.tbgr.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbgr.RightToLeft")));
			this.tbgr.Size = ((System.Drawing.Size)(resources.GetObject("tbgr.Size")));
			this.tbgr.TabIndex = ((int)(resources.GetObject("tbgr.TabIndex")));
			this.tbgr.Text = resources.GetString("tbgr.Text");
			this.tbgr.Visible = ((bool)(resources.GetObject("tbgr.Visible")));
			this.tbgr.TextChanged += new System.EventHandler(this.GroupFilterTextChanged);
			this.tbgr.SelectedIndexChanged += new System.EventHandler(this.GroupFilterChanged);
			// 
			// label16
			// 
			this.label16.AccessibleDescription = resources.GetString("label16.AccessibleDescription");
			this.label16.AccessibleName = resources.GetString("label16.AccessibleName");
			this.label16.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label16.Anchor")));
			this.label16.AutoSize = ((bool)(resources.GetObject("label16.AutoSize")));
			this.label16.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label16.Dock")));
			this.label16.Enabled = ((bool)(resources.GetObject("label16.Enabled")));
			this.label16.Font = ((System.Drawing.Font)(resources.GetObject("label16.Font")));
			this.label16.Image = ((System.Drawing.Image)(resources.GetObject("label16.Image")));
			this.label16.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label16.ImageAlign")));
			this.label16.ImageIndex = ((int)(resources.GetObject("label16.ImageIndex")));
			this.label16.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label16.ImeMode")));
			this.label16.Location = ((System.Drawing.Point)(resources.GetObject("label16.Location")));
			this.label16.Name = "label16";
			this.label16.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label16.RightToLeft")));
			this.label16.Size = ((System.Drawing.Size)(resources.GetObject("label16.Size")));
			this.label16.TabIndex = ((int)(resources.GetObject("label16.TabIndex")));
			this.label16.Text = resources.GetString("label16.Text");
			this.label16.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label16.TextAlign")));
			this.label16.Visible = ((bool)(resources.GetObject("label16.Visible")));
			this.visualStyleProvider1.SetVisualStyleSupport(this.label16, true);
			// 
			// tbin
			// 
			this.tbin.AccessibleDescription = resources.GetString("tbin.AccessibleDescription");
			this.tbin.AccessibleName = resources.GetString("tbin.AccessibleName");
			this.tbin.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbin.Anchor")));
			this.tbin.AutoSize = ((bool)(resources.GetObject("tbin.AutoSize")));
			this.tbin.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbin.BackgroundImage")));
			this.tbin.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbin.Dock")));
			this.tbin.Enabled = ((bool)(resources.GetObject("tbin.Enabled")));
			this.tbin.Font = ((System.Drawing.Font)(resources.GetObject("tbin.Font")));
			this.tbin.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbin.ImeMode")));
			this.tbin.Location = ((System.Drawing.Point)(resources.GetObject("tbin.Location")));
			this.tbin.MaxLength = ((int)(resources.GetObject("tbin.MaxLength")));
			this.tbin.Multiline = ((bool)(resources.GetObject("tbin.Multiline")));
			this.tbin.Name = "tbin";
			this.tbin.PasswordChar = ((char)(resources.GetObject("tbin.PasswordChar")));
			this.tbin.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbin.RightToLeft")));
			this.tbin.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbin.ScrollBars")));
			this.tbin.Size = ((System.Drawing.Size)(resources.GetObject("tbin.Size")));
			this.tbin.TabIndex = ((int)(resources.GetObject("tbin.TabIndex")));
			this.tbin.Text = resources.GetString("tbin.Text");
			this.tbin.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbin.TextAlign")));
			this.tbin.Visible = ((bool)(resources.GetObject("tbin.Visible")));
			this.visualStyleProvider1.SetVisualStyleSupport(this.tbin, true);
			this.tbin.WordWrap = ((bool)(resources.GetObject("tbin.WordWrap")));
			this.tbin.TextChanged += new System.EventHandler(this.GroupFilterTextChanged);
			// 
			// pnLower
			// 
			this.pnLower.AccessibleDescription = resources.GetString("pnLower.AccessibleDescription");
			this.pnLower.AccessibleName = resources.GetString("pnLower.AccessibleName");
			this.pnLower.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("pnLower.Anchor")));
			this.pnLower.AutoScroll = ((bool)(resources.GetObject("pnLower.AutoScroll")));
			this.pnLower.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("pnLower.AutoScrollMargin")));
			this.pnLower.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("pnLower.AutoScrollMinSize")));
			this.pnLower.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnLower.BackgroundImage")));
			this.pnLower.Controls.Add(this.tabControl1);
			this.pnLower.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("pnLower.Dock")));
			this.pnLower.DockPadding.Bottom = 8;
			this.pnLower.DockPadding.Left = 8;
			this.pnLower.DockPadding.Right = 8;
			this.pnLower.DockPadding.Top = 12;
			this.pnLower.Enabled = ((bool)(resources.GetObject("pnLower.Enabled")));
			this.pnLower.Font = ((System.Drawing.Font)(resources.GetObject("pnLower.Font")));
			this.pnLower.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("pnLower.ImeMode")));
			this.pnLower.Location = ((System.Drawing.Point)(resources.GetObject("pnLower.Location")));
			this.pnLower.Name = "pnLower";
			this.pnLower.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("pnLower.RightToLeft")));
			this.pnLower.Size = ((System.Drawing.Size)(resources.GetObject("pnLower.Size")));
			this.pnLower.TabIndex = ((int)(resources.GetObject("pnLower.TabIndex")));
			this.pnLower.Text = resources.GetString("pnLower.Text");
			this.pnLower.Visible = ((bool)(resources.GetObject("pnLower.Visible")));
			this.visualStyleProvider1.SetVisualStyleSupport(this.pnLower, true);
			// 
			// pnTop
			// 
			this.pnTop.AccessibleDescription = resources.GetString("pnTop.AccessibleDescription");
			this.pnTop.AccessibleName = resources.GetString("pnTop.AccessibleName");
			this.pnTop.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("pnTop.Anchor")));
			this.pnTop.AutoScroll = ((bool)(resources.GetObject("pnTop.AutoScroll")));
			this.pnTop.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("pnTop.AutoScrollMargin")));
			this.pnTop.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("pnTop.AutoScrollMinSize")));
			this.pnTop.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnTop.BackgroundImage")));
			this.pnTop.Controls.Add(this.fileList);
			this.pnTop.Controls.Add(this.lbcount);
			this.pnTop.Controls.Add(this.lbtype);
			this.pnTop.Controls.Add(this.label14);
			this.pnTop.Controls.Add(this.tbgr);
			this.pnTop.Controls.Add(this.label16);
			this.pnTop.Controls.Add(this.tbin);
			this.pnTop.Controls.Add(this.label4);
			this.pnTop.Controls.Add(this.label7);
			this.pnTop.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("pnTop.Dock")));
			this.pnTop.Enabled = ((bool)(resources.GetObject("pnTop.Enabled")));
			this.pnTop.Font = ((System.Drawing.Font)(resources.GetObject("pnTop.Font")));
			this.pnTop.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("pnTop.ImeMode")));
			this.pnTop.Location = ((System.Drawing.Point)(resources.GetObject("pnTop.Location")));
			this.pnTop.Name = "pnTop";
			this.pnTop.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("pnTop.RightToLeft")));
			this.pnTop.Size = ((System.Drawing.Size)(resources.GetObject("pnTop.Size")));
			this.pnTop.TabIndex = ((int)(resources.GetObject("pnTop.TabIndex")));
			this.pnTop.Text = resources.GetString("pnTop.Text");
			this.pnTop.Visible = ((bool)(resources.GetObject("pnTop.Visible")));
			this.visualStyleProvider1.SetVisualStyleSupport(this.pnTop, true);
			// 
			// splitter1
			// 
			this.splitter1.AccessibleDescription = resources.GetString("splitter1.AccessibleDescription");
			this.splitter1.AccessibleName = resources.GetString("splitter1.AccessibleName");
			this.splitter1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("splitter1.Anchor")));
			this.splitter1.BackColor = System.Drawing.SystemColors.ControlLight;
			this.splitter1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("splitter1.BackgroundImage")));
			this.splitter1.Cursor = System.Windows.Forms.Cursors.HSplit;
			this.splitter1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("splitter1.Dock")));
			this.splitter1.Enabled = ((bool)(resources.GetObject("splitter1.Enabled")));
			this.splitter1.Font = ((System.Drawing.Font)(resources.GetObject("splitter1.Font")));
			this.splitter1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("splitter1.ImeMode")));
			this.splitter1.Location = ((System.Drawing.Point)(resources.GetObject("splitter1.Location")));
			this.splitter1.MinExtra = ((int)(resources.GetObject("splitter1.MinExtra")));
			this.splitter1.MinSize = ((int)(resources.GetObject("splitter1.MinSize")));
			this.splitter1.Name = "splitter1";
			this.splitter1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("splitter1.RightToLeft")));
			this.splitter1.Size = ((System.Drawing.Size)(resources.GetObject("splitter1.Size")));
			this.splitter1.TabIndex = ((int)(resources.GetObject("splitter1.TabIndex")));
			this.splitter1.TabStop = false;
			this.splitter1.Visible = ((bool)(resources.GetObject("splitter1.Visible")));
			// 
			// Form1
			// 
			this.AccessibleDescription = resources.GetString("$this.AccessibleDescription");
			this.AccessibleName = resources.GetString("$this.AccessibleName");
			this.AutoScaleBaseSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScaleBaseSize")));
			this.AutoScroll = ((bool)(resources.GetObject("$this.AutoScroll")));
			this.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMargin")));
			this.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMinSize")));
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.ClientSize = ((System.Drawing.Size)(resources.GetObject("$this.ClientSize")));
			this.Controls.Add(this.splitter1);
			this.Controls.Add(this.pnLower);
			this.Controls.Add(this.pb1);
			this.Controls.Add(this.pnTop);
			this.Enabled = ((bool)(resources.GetObject("$this.Enabled")));
			this.Font = ((System.Drawing.Font)(resources.GetObject("$this.Font")));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("$this.ImeMode")));
			this.Location = ((System.Drawing.Point)(resources.GetObject("$this.Location")));
			this.MaximumSize = ((System.Drawing.Size)(resources.GetObject("$this.MaximumSize")));
			this.Menu = this.mainMenu1;
			this.MinimumSize = ((System.Drawing.Size)(resources.GetObject("$this.MinimumSize")));
			this.Name = "Form1";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.StartPosition = ((System.Windows.Forms.FormStartPosition)(resources.GetObject("$this.StartPosition")));
			this.Text = resources.GetString("$this.Text");
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Closing += new System.ComponentModel.CancelEventHandler(this.Form1_Closing);
			this.Load += new System.EventHandler(this.Form1_Load);
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage4.ResumeLayout(false);
			this.gbtypes.ResumeLayout(false);
			this.pntypes.ResumeLayout(false);
			this.tabPage3.ResumeLayout(false);
			this.panel4.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.pnLower.ResumeLayout(false);
			this.pnTop.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion


		static string GPL =
			"This program is derived from SimPe, originally created by Ambertation.\n\n" +
			"This program is free software; you can redistribute it and/or modify\n" +
			"it under the terms of the GNU General Public License as published by\n" +
			"the Free Software Foundation; either version 2 of the License, or\n" +
			"(at your option) any later version.\n" +
			"\n" +
			"This program is distributed in the hope that it will be useful,\n" +
			"but WITHOUT ANY WARRANTY; without even the implied warranty of\n" +
			"MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the\n" +
			"GNU General Public License for more details.\n" +
			"\n" +
			"You should have received a copy of the GNU General Public License\n" +
			"along with this program; if not, write to the\n" +
			"Free Software Foundation, Inc.,\n" +
			"59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.";


		/// <summary>
		/// Der Haupteinstiegspunkt für die Anwendung.
		/// </summary>
		[STAThread]
		static void Main(string[] args) 
		{
			//Application.EnableVisualStyles();
			


			//remove the HexApplication Setting if avail
			try 
			{				
				if (Helper.WindowsRegistry.XPStyle) Skybound.VisualStyles.VisualStyleProvider.EnableVisualStyles();

				Microsoft.Win32.RegistryKey rkf = Helper.WindowsRegistry.RegistryKey.CreateSubKey("Settings");
				object o = rkf.GetValue("HexApplication");
				if (o!=null)
				{
					rkf.DeleteValue("HexApplication");

					ToolLoaderItem tli = new ToolLoaderItem("HexEditor");
					tli.FileName = o.ToString();
					ToolLoader.Add(tli);
					ToolLoader.StoreTools();
				}
			}
			catch (Exception) {}
			try 
			{
				Parameters param = new Parameters(args);
				Helper.CommandlineParameters = param;

				#region check for Delphys Plugin
				string plugname = System.IO.Path.Combine(Helper.SimPePluginPath, "cGraphicNode.plugin.dll");
				if (System.IO.File.Exists(plugname))
				{
					if (MessageBox.Show("SimPE has detected that Delphy's GMDC Plugin is installed on your System. This Plugin is now a Part of the main SimPE Application, in order to use the new Features of the native GMDC Wrapper, you should remove the File cGraphicNode.plugin.dll from the SimPE Plugins Folder.\n\nDo you want SimPE to disable it?", "Information", MessageBoxButtons.YesNo)==DialogResult.Yes)
					{
						string newname = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(plugname),System.IO.Path.GetFileNameWithoutExtension(plugname)+".bak");
						System.IO.File.Move(plugname, newname);
					}
				}
				#endregion

				if (Helper.WindowsRegistry.Version < Helper.SimPeVersion.FileMinorPart)
				{
					System.Windows.Forms.MessageBox.Show(GPL,
						"About PLJones SimAntics Editor",
						System.Windows.Forms.MessageBoxButtons.OK,
						System.Windows.Forms.MessageBoxIcon.Information);
					//if (MessageBox.Show(Localization.Manager.GetString("disclaimer"), "Information", MessageBoxButtons.YesNo)==DialogResult.No) return; 
				}
				if (Helper.WindowsRegistry.Version<1) 
				{
					string xmlname = System.IO.Path.Combine(Helper.SimPeDataPath, "folders.xml");
					if (System.IO.File.Exists(xmlname))
					{
						if (MessageBox.Show(
								"The file " + xmlname + " belongs to an old version of this program.\n\n" + 
								"If you select 'Yes' it will be removed and recreated.\n" +
								"If you select 'No' you should edit the file yourself to update it.\n\n" +
								"You will not be prompted again.\n\n" +
								"Replace folders.xml?",
								"Replace folders.xml?",
								System.Windows.Forms.MessageBoxButtons.YesNo,
								System.Windows.Forms.MessageBoxIcon.Information,
								System.Windows.Forms.MessageBoxDefaultButton.Button1
							) == DialogResult.Yes)
						{
							//string newname = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(xmlname),System.IO.Path.GetFileNameWithoutExtension(xmlname)+".bak");
							System.IO.File.Delete(xmlname);
						}
					}
				}

				Helper.WindowsRegistry.Version = Helper.SimPeVersion.FileMinorPart;
				if (!Commandline.Start(args))  
				{
					Helper.WindowsRegistry.UpdateSimPEDirectory();
					Application.Run(new Form1());
				}
			} 
			catch (Exception ex)
			{
				Helper.ExceptionMessage("SimPE will shut down due to an unhadled Exception.", ex);//MessageBox.Show("SimPE will shut down due to an unhandled Exception!\n\nError: "+ex.Message+"\nSource: "+ex.Source+"\nStack:"+ex.StackTrace);
			} 
			finally 
			{
				WaitingScreen.Stop();
			}
		}

		private ListViewItem CreateAttributeItem(string name, string val)
		{
			ListViewItem item = new ListViewItem(name);
			item.SubItems.Add(val);

			return item;
		}

		private SimPe.Packages.GeneratableFile package = null;

		
		private void Form1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (!WarnOverwriteChanges()) 
			{
				e.Cancel = true;
				return;
			}
			if (package!=null) if (package.Reader!=null) package.Reader.Close();

//			Ambertation.Panel3D.StopAll();
		}

		protected void UpdateMenuItemState()
		{
			misaveas.Enabled = (package!=null);
			miclose.Enabled = (package!=null);
			miextract.Enabled = (package!=null);
			miadd.Enabled = (package!=null);
			fileList.Enabled = (package!=null);
			//miintrigued.Enabled = (package!=null);
			miintrigued.Visible = false;
			mifilelist.Enabled = (package != null);
			//milistsims.Enabled = ((package!=null) && (registry.SimFamilynameProvider.BasePackage!=null));
			milistsims.Visible = false;
			//mimem.Enabled = (registry.OpcodeProvider != null);
			mimem.Visible = false;
			//mifix.Enabled = (package != null );
			mifix.Visible = false;
			//micopyright.Enabled = (package != null);
			micopyright.Visible = false;
			mireload.Visible = false;

			//bthexopen.Enabled = ((File.Exists(reg.HexApplication)) && (package!=null) && (fileList.SelectedItems.Count>0) );
			
			btbyteview.Enabled = (package!=null) && (fileList.SelectedItems.Count>0); 
			btbyteview.Refresh();
			mirunsims.Enabled = (File.Exists(reg.SimsApplication));

/*			if ((lbtype.SelectedIndex>0) && (package!=null) )
			{
				TypeAlias a = (TypeAlias)lbtype.Items[lbtype.SelectedIndex];	
				minamemap.Enabled = Data.MetaData.RcolList.Contains(a.Id);
			} 
			else
				minamemap.Enabled = false;
*/			minamemap.Visible = false;
			

			this.mis2cpid.Enabled = false;
			if (package!=null) if (package.FileName != null) if (System.IO.File.Exists(package.FileName)) this.mis2cpid.Enabled = true;
			//miss2cp.Enabled = mis2cpid.Enabled;

			if (package != null) 
			{
				misave.Enabled = System.IO.File.Exists(package.FileName);
			} 
			else 
			{
				misave.Enabled = false;
			}

			if (currentwrapper!=null) wloader.EnableMenuItems(this.miPlugins, this.currentwrapper.FileDescriptor, package);
			else wloader.EnableMenuItems(this.miPlugins, null, package);
		}

		

		private void ProcessPackedFile(object sender, System.EventArgs e)
		{
			this.UpdateMenuItemState();
			if (!this.WarnWrapperChange()) return;
			currentwrapper = null;

			if (package==null) return;
			if (fileList.SelectedItems.Count>1)RemoveCurrentPlugin();
			if (fileList.SelectedItems.Count!=1) return;

			this.cbComp.Tag = true;
			try 
			{
				Interfaces.Files.IPackedFileDescriptor pfd = (Interfaces.Files.IPackedFileDescriptor)fileList.SelectedItems[0].Tag;
//				Ambertation.Panel3D.StopAll();
				this.SelectPackedFile(pfd);				
			} 
			catch (Exception ex) 
			{
				Helper.ExceptionMessage(Localization.Manager.GetString("err000") , ex);
			} 
			finally 
			{
				this.cbComp.Tag = null;
			}
				
		}

		void EnableCurrentPlugin(bool enabled) 
		{
			foreach (Control c in tabPage5.Controls) c.Enabled = enabled;
		}

		void RemoveCurrentPlugin()
		{
			//remove old Plugin
			if (this.Tag!=null) 
			{
				Panel pn = (Panel)this.Tag;
				pn.Dispose();
				this.Tag = null;
			}

			//delet alle controsl that are subassigned to tabPage5
			tabPage5.Controls.Clear();
			

			lbHandler.Text = Localization.Manager.GetString("none");
			WaitingScreen.Stop();
		}

		internal void SelectPackedFile(Interfaces.Files.IPackedFileDescriptor fii) {
			RemoveCurrentPlugin();
			if (package==null) return;
			if (fii==null) return;

			lbdword.Text = "---";
			lbword.Text = "---";
			lbbyte.Text = "---";
			this.Cursor = Cursors.WaitCursor;
			try 
			{				
				wloader.EnableMenuItems(this.miPlugins, fii, package);
				IPackedFile pf = package.Read(fii);
					
				if (mihexview.Checked) hex2.Data = pf.UncompressedData;						

				//set Informations of Package File
				cbComp.Checked = pf.IsCompressed;
				if (pf.IsCompressed) 
				{				
					if (mihexview.Checked) hex1.Data = pf.Data;
					tbSize.Text = "0x"+pf.Size.ToString("X");
					tbUncsize.Text = "0x"+pf.UncompressedSize.ToString("X")+" ("+Localization.Manager.GetString("realsize")+"=0x"+pf.UncompressedData.Length.ToString("X")+")";				
				} 
				else 
				{
					if (mihexview.Checked) hex1.Data = null;
					tbSize.Text = Localization.Manager.GetString("unknown");
					tbUncsize.Text = Localization.Manager.GetString("unknown");				
				}
				tbtype.Text = "0x"+Helper.HexString(fii.Type);
				tbsubtype.Text = "0x"+Helper.HexString(fii.SubType);
				tbgroup.Text = "0x"+Helper.HexString(fii.Group);
				tbinstance.Text = "0x"+Helper.HexString(fii.Instance);

				//Do we have a registred handler?
				SimPe.Interfaces.Plugin.IFileWrapper wrapper = (SimPe.Interfaces.Plugin.IFileWrapper)registry.FindHandler(fii.Type);
				currentwrapper = wrapper;
				if (wrapper==null) 
				{
					wrapper = registry.FindHandler(pf.UncompressedData);
				}

				RemoveCurrentPlugin();
				
				
				if (wrapper!=null) 
				{
					Panel pan = wrapper.UIHandler.GUIHandle; 
					if (pan!=null) 
					{
						pan.Visible = false;
				
						pan.Parent = this.tabPage5;
						pan.Location = new Point(0, 0);
				
						pan.Width = this.tabPage5.ClientRectangle.Width;
						pan.Height = this.tabPage5.ClientRectangle.Height;
						pan.Visible = true;

						//pan.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right);
						pan.Dock = DockStyle.Fill;
						pan.BackColor = System.Drawing.Color.Transparent;
						pan.BackColor = panel2.BackColor;
						pan.BackgroundImage = panel2.BackgroundImage;
						pan.AutoScroll = true;
						this.tabPage5.AutoScroll = true;

						lbHandler.Text = wrapper.ToString();
					} 
					else 
					{
						lbHandler.Text = Localization.Manager.GetString("none");
					}

					try 
					{
						wrapper.ProcessData(fii, package);
						wrapper.UpdateUI();
					} 
					catch(Exception ex) 
					{
						Helper.ExceptionMessage("", ex);
					}
				}

				pntypes.Enabled = true;
				UpdateMenuItemState();

				UpdateExtToolSelect(fii);
			} 
			catch (Exception ex) 
			{
				Helper.ExceptionMessage(Localization.Manager.GetString("err000") , ex);
			}
			this.Cursor = Cursors.Default;

		
		}

		protected void UpdateExtToolSelect(Interfaces.Files.IPackedFileDescriptor pfd) 
		{
			ToolLoaderItem[] items = ToolLoader.UsableItems(pfd.Type);
			cbext.Items.Clear();
			foreach (ToolLoaderItem tli in items) 
			{
				cbext.Items.Add(new ToolLoaderListBoxItem(tli));
			}

			if (cbext.Items.Count>0) 
			{
				cbext.SelectedIndex = 0;
				llopenext.Enabled = true;
			} 
			else 
			{
				llopenext.Enabled = false;
			}
		}

		private void Form1_Load(object sender, System.EventArgs e)
		{
			//init the RemoteHandler
			RemoteHandler rh = new RemoteHandler(this);

			this.Text += " (Version "+Helper.SimPeVersion.ProductVersion+")";
			UpdateRecentFileMenu();

			minometa.Checked = !reg.LoadMetaInfo;			

			//hew view Handling
			mihexview.Checked = reg.HexViewState;
			panel2.Visible = panel4.Visible = mihexview.Checked;
			midecode.Checked = reg.DecodeFilenamesState;
			lbHexView.Visible = !mihexview.Checked;			
#if DEBUG
			if (!mihexview.Checked) tabControl1.TabPages.Remove(tabPage2);
#else
			this.tabPage2.Controls.Clear();
			this.tabPage2.Dispose();
			mifoldercompare.Enabled = false;
			//miinstplug.Visible = false;
#endif
			micopyright.Visible = false; //Helper.WindowsRegistry.HiddenMode;
			foreach (SimPe.Data.TypeAlias a in SimPe.Data.MetaData.FileTypes) 
			{
				cbtypes.Items.Add(a);
			}
			cbtypes.Sorted = true;

			hex2.OnSelectionChange += new EventHandler(OnHexSelectionChanged);
			this.tabControl1.SelectedIndex = 0;
			UpdateMenuItemState();

			//here is for the Resizing
			if (fileList.Location.X+fileList.Width > this.ClientRectangle.Width) 
			{
				fileList.Width = ClientRectangle.Width - (fileList.Location.X + 8);
				this.tabControl1.Width = ClientRectangle.Width - (tabControl1.Location.X + 8);
				this.tabControl1.Height = ClientRectangle.Height - (tabControl1.Location.Y + 8);

				this.tbin.Left = (fileList.Left + fileList.Width) - tbin.Width;
				this.label16.Left = this.tbin.Left - (4 + this.label16.Width);

				tbgr.Width = label16.Left - tbgr.Left - 12;
			}

			foreach (Alias a in Data.MetaData.SemiGlobals) 
			{
				tbgr.Items.Add("0x"+Helper.HexString(a.Id)+" "+a.Name);
			}

			if (Helper.CommandlineParameters.Files.Length>0) 
			{
				this.OpenPackage(Helper.CommandlineParameters.Files[0]);
				Helper.WindowsRegistry.AddRecentFile(Helper.CommandlineParameters.Files[0]);
				this.UpdateRecentFileMenu();
			}

			this.llexportraw.Visible = Helper.WindowsRegistry.HiddenMode;
			this.llimpraw.Visible = Helper.WindowsRegistry.HiddenMode;
		}


		protected void OnHexSelectionChanged(object sender, System.EventArgs e) 
		{
			lbbyte.Text = "0x"+Helper.HexString(hex2.ByteValue) +"\n"+hex2.ByteValue.ToString();
			lbword.Text = "0x"+Helper.HexString(hex2.ShortValue) +"\n"+hex2.ShortValue.ToString()+"\n"+((short)hex2.ShortValue).ToString();
			lbdword.Text = "0x"+Helper.HexString(hex2.IntValue) +"\n"+hex2.IntValue.ToString()+"\n"+((int)hex2.IntValue).ToString();
			tboffset.Text = "0x"+Helper.HexString((uint)hex2.Offset);
		}

		/// <summary>
		/// Adds an Item to the FileList
		/// </summary>
		/// <param name="pfd">The description of the Item</param>
		/// <param name="filter">true, if you want to add only files matching the selected type in lbtype</param>
		/// <returns>true, if the file is of the same type as the Item selected in lbtype</returns>
		protected bool AddFileItem(IPackedFileDescriptor pfd, bool filter)
		{
			if ((pfd.MarkForDelete) && (filter)) return false;
			bool ret = true;
			string name = "0x"+pfd.Type.ToString("X");
			string flname = "";
			
			if (filter) 
			{
				//filter by Group
				if (tbgr.Tag != null) 
				{
					try 
					{
						uint gp = (uint)tbgr.Tag;
						if (pfd.Group != gp) return false;
					} 
					catch (Exception) {}
				}

				//filter by Instance
				if (tbin.Tag != null) 
				{
					try 
					{
						uint ins = (uint)tbin.Tag;
						if (pfd.Instance != ins) return false;
					} 
					catch (Exception) {}
				}
			}

			if (lbtype.Items[lbtype.SelectedIndex].ToString() == "-----------") 
			{
				foreach (TypeAlias a in MetaData.FileTypes)
				{
					if (a.Id == pfd.Type) 
					{
						name = a.Name ;//+ " ("+a.shortname+", "+name+")";						
						break;
					}
				} //for
			} 
			else 
			{
				TypeAlias a = (TypeAlias)lbtype.Items[lbtype.SelectedIndex];	
				if (pfd.Type!=a.Id) 
				{
					ret = false;
					if (filter) return ret;
				}
				name = a.Name + " ("+a.shortname+", "+name+")";

				if ((a.containsfilename) && (midecode.Checked))
				{
					byte[] data = new byte[0];
					if (pfd.HasUserdata) 
					{
						data = pfd.UserData;
					} 
					else 
					{
						IPackedFile fl = package.Read(pfd);
					
						if (fl.IsCompressed) 
						{
							data = fl.Decompress(64);
						} 
						else 
						{
							data = fl.Data;
						}
					}

					flname = Helper.ToString(data);						
					name = flname+": "+name;
				}
			}

			

			ListViewItem item = new ListViewItem(name);
			item.Tag = pfd;

			item.SubItems.Add("0x"+Helper.HexString(pfd.SubType));
			item.SubItems.Add("0x"+Helper.HexString(pfd.Group));
			item.SubItems.Add("0x"+Helper.HexString(pfd.Instance));				
			item.SubItems.Add("0x"+Helper.HexString((uint)pfd.Size));
			item.SubItems.Add("0x"+Helper.HexString(pfd.Offset));

			fileList.Items.Add(item);
			
			return ret;
		}


		private void UpdateFileGroupFilter(object sender, System.EventArgs e)
		{
			if (package==null) return;
			fileList.BeginUpdate();
			fileList.Items.Clear();		
			if (lbtype.SelectedIndex<0) 
			{
				fileList.EndUpdate();
				return;
			}

			fileList.ListViewItemSorter = null;	
			this.Cursor = Cursors.WaitCursor;
			try 
			{
				for (uint i=0; i<package.Header.Index.Count; i++)
				{
					IPackedFileDescriptor fii = package.GetFileIndex(i);
					
					if (!AddFileItem(fii, true)) continue;
				}//for

				//if (fileList.Items.Count>0) fileList.Items[0].Selected = true;	
				lbcount.Text = "("+fileList.Items.Count+")";
				
			} 
			catch (Exception ex) 
			{
				Helper.ExceptionMessage("Error while processing a Packed File Filter." , ex);
			}
			finally 
			{
				this.Cursor = Cursors.Default;				
			}
			//fileList.ListViewItemSorter = sorter;
			UpdateMenuItemState();			
			fileList.EndUpdate();
		}


		private void btgoto_Click(object sender, System.EventArgs e)
		{
			hex2.Offset = (int)Helper.HexStringToUInt(tboffset.Text.Replace("0x", "").ToUpper());
		}

		protected void ExtractAllFiles(string SelectedPath, IList items) 
		{
			int excount = 0;
			int filecount = 0;
			this.Cursor = Cursors.WaitCursor;				
			pb1.Maximum = items.Count;
			pb1.Value = 0;
			pb1.Visible = true;
			string xml = "";
			xml += "<?xml version=\"1.0\" encoding=\"UTF-8\" ?>" + Helper.lbr;		
			xml += "<package type=\""+((uint)package.Header.IndexType).ToString()+"\">" + Helper.lbr;
			for (int i=0; i<items.Count; i++) 
			{
				pb1.Value = i;
				Application.DoEvents();
				
				Packages.PackedFileDescriptor fii = null;
				if (items[i].GetType() == typeof(ListViewItem)) fii =(Packages.PackedFileDescriptor)((ListViewItem)items[i]).Tag;				
				//if (items[i].GetType() == typeof(SelectedListViewItem)) fii =(Packages.PackedFileDescriptor)((SelectedListViewItem)items[i]).Tag;				
				Data.TypeAlias a = fii.TypeName;

				fii.Path = null;
				string path = System.IO.Path.Combine(SelectedPath, fii.Path);
								
				fii.Filename = null;
				string name = System.IO.Path.Combine(path, fii.Filename);

				try 
				{
					if (!System.IO.Directory.Exists(path)) System.IO.Directory.CreateDirectory(path);
					
					//make sure the sub xmls don't have a Filename
					fii.Path = "";
					package.SavePackedFile(name, null, fii, true);
					fii.Path = null;
					
					xml += fii.GenerateXmlMetaInfo();
					
					if ((filecount%10)==0)
					{
						//System.Threading.Thread.Sleep(new TimeSpan(100000));
						Application.DoEvents();
					}
					filecount++;
				} 
				catch (Exception ex) 
				{
					excount++;
					Helper.ExceptionMessage(Localization.Manager.GetString("errwritingfile")+" "+name, ex);
					if (excount>=5) 
					{
						
						if (MessageBox.Show(Localization.Manager.GetString("ask000"), Localization.Manager.GetString("proceed"), System.Windows.Forms.MessageBoxButtons.YesNo)==System.Windows.Forms.DialogResult.Yes) 
						{
							i=items.Count;
						}
					}
				}
				
			}//for i
			xml += "</package>" + Helper.lbr;

			System.IO.TextWriter tw = System.IO.File.CreateText(System.IO.Path.Combine(SelectedPath, "package.xml"));
			try 
			{				
				tw.Write(xml);
				tw.Close();
			} 
			catch (Exception ex) 
			{
				Helper.ExceptionMessage(Localization.Manager.GetString("err001"), ex);
			}
			finally 
			{
				tw.Close();
			}

			/*try 
			{
				package.GeneratePackageXML(System.IO.Path.Combine(SelectedPath, "package.xml"));
			} 
			catch (Exception ex) 
			{
				Helper.ExceptionMessage(Localization.Manager.GetString("err001"), ex);
			}*/


			pb1.Value = pb1.Maximum;
			MessageBox.Show(Localization.Manager.GetString("nfo000").Replace("{0}", filecount.ToString()));
			pb1.Visible = false;
			this.Cursor = Cursors.Default;
		}

		private void ExtractAllClick(object sender, System.EventArgs e)
		{
			if (fileList.Items.Count<1) return;
#if DEBUG
#else
			fbd.SelectedPath = System.Environment.GetEnvironmentVariable("HOMEDRIVE")+"\\";
#endif
			
			if (fbd.ShowDialog()==DialogResult.OK) 
			{
				try 
				{
					object[] o = new object[2];
					o[0] = fbd.SelectedPath;
					o[1] = fileList.Items;
					ExtractAllFiles(fbd.SelectedPath, fileList.Items); 
				} 
				catch (Exception ex) 
				{
					Helper.ExceptionMessage(Localization.Manager.GetString("err002")+fbd.SelectedPath, ex);
				}
			}
		}

		

		private void SavePackedFileClick(object sender, System.EventArgs e)
		{
			if (fileList.SelectedItems.Count<1) return;

			if (fileList.SelectedItems.Count==1) //extract one File
			{
				Packages.PackedFileDescriptor fii = (Packages.PackedFileDescriptor)fileList.SelectedItems[0].Tag;
				//string path = System.Environment.GetEnvironmentVariable("HOMEDRIVE");
						
				//sfd.FileName = path + "\\" + System.IO.Path.GetFileName(filename)+".simpe";
				sfd.FileName = fii.Path + " - " + fii.Filename;
				sfd.Filter = "Extracted File (*.simpe)|*.simpe|All Files (*.*)|*.*";
				if (sfd.ShowDialog()==DialogResult.OK) 
				{
					ToolLoaderItem.SavePackedFile(sfd.FileName, true, fii, package);
					fii.Path = null;
				}

			} 
			else //extract multiple Files
			{
#if DEBUG
#else
			fbd.SelectedPath = System.Environment.GetEnvironmentVariable("HOMEDRIVE")+"\\";
#endif
			
				if (fbd.ShowDialog()==DialogResult.OK) 
				{
					try 
					{						
						ExtractAllFiles(fbd.SelectedPath, fileList.SelectedItems);
					} 
					catch (Exception ex) 
					{
						Helper.ExceptionMessage(Localization.Manager.GetString("err002")+fbd.SelectedPath, ex);
					}
				}

			}
		}

		private void SavePackageClick(object sender, System.EventArgs e)
		{
			if (fileList.Items.Count<1) return;
			string path = System.Environment.GetEnvironmentVariable("HOMEDRIVE");
						
			string filename = "";
			if (package!=null) filename = System.IO.Path.GetFileName(package.FileName);
			sfd.FileName = filename;
			sfd.Filter = "Sims 2 Package (*.package)|*.package|All Files (*.*)|*.*";

			if (sfd.ShowDialog()==DialogResult.OK) 
			{
				try 
				{
					this.Cursor = Cursors.WaitCursor;

					this.RemoveCurrentPlugin();
					package.Save(sfd.FileName);
					this.UpdateFileGroupFilter(null, null);

					this.Text = "PLJones SimAntics Editor ["+package.FileName+"]";
					if (package!=null) package.FileName = sfd.FileName;
					reg.AddRecentFile(sfd.FileName);
					UpdateRecentFileMenu();				
					
				}
				catch (Exception ex) 
				{
					Helper.ExceptionMessage(Localization.Manager.GetString("errwritingfile")+sfd.FileName, ex);
				} 
				finally 
				{
					this.Cursor = Cursors.Default;
				}
			}
		}

		private ColumnSorter sorter;
		private void SortFileListClick(object sender, System.Windows.Forms.ColumnClickEventArgs e)
		{
			if (((ListView)sender).ListViewItemSorter == null) ((ListView)sender).ListViewItemSorter = sorter;
			sorter.CurrentColumn = e.Column;
			((ListView)sender).Sort();
		}

		protected bool OpenPackedFile(string filename, ref Packages.PackedFileDescriptor pfd) 
		{			
			this.Cursor = Cursors.WaitCursor;
			ToolLoaderItem.OpenPackedFile(filename, ref pfd, package);
				
			this.Cursor = Cursors.Default;
			return true;
		}

		protected Packages.PackedFileDescriptor[] OpenPackedFile() 
		{
			if (ofd.ShowDialog()==DialogResult.OK) 
			{
				ArrayList list = new ArrayList();		
				foreach (string flname in ofd.FileNames) 
				{
					if (flname.ToLower().EndsWith("package.xml")) 
					{
						pb1.Visible = true;
						SimPe.Packages.File pkg = new Packages.GeneratableFile(XmlPackageReader.OpenExtractedPackage(pb1, flname));	
						pb1.Visible = false;

						foreach (Interfaces.Files.IPackedFileDescriptor pfd in pkg.Index) 
						{
							Interfaces.Files.IPackedFile file = pkg.Read(pfd);
							pfd.UserData = file.UncompressedData;
							list.Add(pfd);
						}
					} 
					else if (flname.ToLower().EndsWith(".package")) 
					{
						SimPe.Packages.File pkg = new SimPe.Packages.File(flname);
						foreach (Interfaces.Files.IPackedFileDescriptor pfd in pkg.Index) 
						{
							Interfaces.Files.IPackedFile file = pkg.Read(pfd);
							pfd.UserData = file.UncompressedData;
							list.Add(pfd);
						}
					}
					else 
					{
						Packages.PackedFileDescriptor pfd = new SimPe.Packages.PackedFileDescriptor();
						pfd.Type = 0xffffffff;
						if (OpenPackedFile(flname, ref pfd)) list.Add(pfd);
					}
				}
				Packages.PackedFileDescriptor[] pfds = new Packages.PackedFileDescriptor[list.Count];
				list.CopyTo(pfds);

				return pfds;
			}			
			return new Packages.PackedFileDescriptor[0];
		}

		private void OpenPackedFileClick(object sender, System.EventArgs e)
		{
			if (fileList.SelectedItems.Count<1) return;
			ofd.Filter = "Extracted File (*.simpe)|*.simpe|All Files (*.*)|*.*";
			Packages.PackedFileDescriptor pfd = (Packages.PackedFileDescriptor)fileList.SelectedItems[0].Tag;
			Packages.PackedFileDescriptor[] pfds = OpenPackedFile();
			if (pfds.Length>0) pfd.UserData = pfds[0].UserData;
			ProcessPackedFile(null, null);
		}

		private void ExitClick(object sender, System.EventArgs e)
		{
			Close();
		}


		protected void EnlistPackage() 
		{
			//this.filename = filename;
			fileList.Enabled = true;
			attList.Items.Clear();
			holeList.Items.Clear();
			lbtype.Items.Clear();
			fileList.Items.Clear();

			if (package==null) return;
			try 
			{				
				this.Cursor = Cursors.WaitCursor;				

				
				attList.Items.Add(CreateAttributeItem("ID", package.Header.Identifier));
				attList.Items.Add(CreateAttributeItem("Version", package.Header.MajorVersion + "." + package.Header.MinorVersion));
				attList.Items.Add(CreateAttributeItem("Index Version", "0x"+package.Header.Index.Type.ToString("X")));
				attList.Items.Add(CreateAttributeItem("Index Type", "0x"+((uint)package.Header.IndexType).ToString("X")+ " ("+package.Header.IndexType.ToString()+")"));
				attList.Items.Add(CreateAttributeItem("Index Count", package.Header.Index.Count.ToString()));
				attList.Items.Add(CreateAttributeItem("Index Offset", "0x"+package.Header.Index.Offset.ToString("X")));
				attList.Items.Add(CreateAttributeItem("Index Size", "0x"+package.Header.Index.Size.ToString("X")));
				attList.Items.Add(CreateAttributeItem("Hole Count", package.Header.HoleIndex.Count.ToString()));
				attList.Items.Add(CreateAttributeItem("Hole Offset", "0x"+package.Header.HoleIndex.Offset.ToString("X")));
				attList.Items.Add(CreateAttributeItem("Hole Size", "0x"+package.Header.HoleIndex.Size.ToString("X")));				


#if DEBUG
				/*attList.Items.Add(CreateAttributeItem("Created", "0x"+package.Header.created.ToString("X")));
				attList.Items.Add(CreateAttributeItem("Modified", "0x"+package.Header.modified.ToString("X")));
			
				for (uint i=0; i< package.Header.reserved_00.Length;i++) 
					attList.Items.Add(CreateAttributeItem("Reserved 0"+i.ToString(), "0x"+package.Header.reserved_00[i].ToString("X")));			

				

				for (uint i=0; i< package.Header.reserved_02.Length;i++) 
					attList.Items.Add(CreateAttributeItem("Reserved 2"+i.ToString(), "0x"+package.Header.reserved_02[i].ToString("X")));			*/
#endif

				
				for (uint i=0; i<package.Header.HoleIndex.Count; i++)
				{
					Packages.HoleIndexItem hii = package.GetHoleIndex(i);

					ListViewItem item = new ListViewItem("0x"+hii.Size.ToString("X"));
					item.Tag = hii;

					item.SubItems.Add("0x"+hii.Offset.ToString("X"));

					holeList.Items.Add(item);				
				}

				
				lbtype.Sorted = false;
				lbtype.Items.Add("-----------");

				for (uint i=0; i<package.Header.Index.Count; i++)
				{
					IPackedFileDescriptor fii = package.GetFileIndex(i);
					bool check = false;
					foreach (TypeAlias a in MetaData.FileTypes)
					{
						if (a.Id == fii.Type) 
						{
							if (!lbtype.Items.Contains(a)) lbtype.Items.Add(a);
							check = true;
							break;
						}
					} //for

					//no Alias Found
					if (!check) 
					{
						TypeAlias a = new TypeAlias(false, Localization.Manager.GetString("unk"), fii.Type,  "0x"+fii.Type.ToString("X"));
					
						if (!lbtype.Items.Contains(a)) lbtype.Items.Add(a);
					}
				}
				lbtype.SelectedIndex = 0;
				lbtype.Sorted = true;

				lbcount.Text = "("+package.Header.Index.Count.ToString()+")";
				UpdateMenuItemState();
			} 
			catch ( Exception ex) 
			{
				Helper.ExceptionMessage(Localization.Manager.GetString("erropenfile")+" "+ofd.FileName, ex);
			} 
			finally 
			{
				this.Cursor = Cursors.Default;
				pb1.Visible = false;
				//this.fileList.ListViewItemSorter = sorter;
			}
		}

		protected void UpdatePackageData(string filename) 
		{
			if (filename==null) filename="";

			//Optain Providers
/*			if (Helper.IsNeighborhoodFile(filename) && (!minometa.Checked))
			{
				registry.SimNameProvider.BaseFolder = System.IO.Path.GetDirectoryName(filename)+"\\Characters";
				registry.SimFamilynameProvider.BasePackage = package;
				registry.SimDescriptionProvider.BasePackage = package;
			} 
			else 
			{
				registry.SimNameProvider.BaseFolder = "";
				registry.SimFamilynameProvider.BasePackage = null;
				registry.SimDescriptionProvider.BasePackage = null;
			}
*/

			EnlistPackage();
			wloader.EnableMenuItems(this.miPlugins, null, package);
		}

		internal void OpenPackage(string filename) 
		{
			RemoveCurrentPlugin();
			this.Text = "PLJones SimAntics Editor ["+filename+"]";
			try 
			{		
				this.Cursor = Cursors.WaitCursor;
				this.fileList.ListViewItemSorter = null;				

				//open a previsouly extracted Package
				if (filename.ToLower().EndsWith("package.xml")) 
				{
					pb1.Visible = true;

					package = new Packages.GeneratableFile(XmlPackageReader.OpenExtractedPackage(pb1, filename));	
				} 
				else 
				{
					package = new Packages.GeneratableFile(filename);	
				}
				
				
				if (filename.ToLower().EndsWith("package.xml"))  package.FileName = "";
				else package.FileName = filename;


				UpdatePackageData(filename);
			} 
			catch ( Exception ex) 
			{
				Helper.ExceptionMessage(Localization.Manager.GetString("erropenfile")+" "+ofd.FileName, ex);
			} 
			finally 
			{
				this.Cursor = Cursors.Default;
				pb1.Visible = false;
			}
		}
		

		private void OpenPackageClick(object sender, System.EventArgs e)
		{
			if (!WarnOverwriteChanges()) return;
			ofd.Filter = "Sims 2 Package (*.package)|*.package|Package Description (*package.xml)|*package.xml|Sims 2 Community Package (*.s2cp)|*.s2cp|All Files (*.*)|*.*";
			ofd.FilterIndex = 1;
			if (ofd.ShowDialog() == DialogResult.OK) 
			{			
				try 
				{
					if (ofd.FileName.Trim().ToLower().EndsWith(".s2cp")) 
					{
						this.CloseFileHandleClick(sender, e);
						SimPe.Packages.S2CPDescriptor[] descs = SimPe.Packages.Sims2CommunityPack.ShowOpenDialog(ofd.FileName, SelectionMode.One);
						if (descs!=null) 
						{
							if (descs.Length>0) 
							{
								package = descs[0].Package;
								UpdatePackageData(package.FileName);
								Text = "SimPE ("+package.FileName+")";
								return;
							} 							
						}

						package = null;
						UpdatePackageData(null);
						
					} 
					else 
					{
						this.reg.AddRecentFile(ofd.FileName);							
						UpdateRecentFileMenu();
						this.CloseFileHandleClick(sender, e);
						OpenPackage(ofd.FileName);
					}
					this.lbtype.Focus();
				} 
				catch (Exception ex) 
				{
					Helper.ExceptionMessage(Localization.Manager.GetString("erropenfile")+" "+ofd.FileName, ex);
				}
			}
		}

		private void OpenRecentClick(object sender, System.EventArgs e)
		{
			if (!WarnOverwriteChanges()) return;
			try 
			{
				string[] names = reg.GetRecentFiles();
				MenuItem mi = (MenuItem)sender;	
				this.CloseFileHandleClick(null, null);
				OpenPackage(names[mi.Index]);
				this.reg.AddRecentFile(names[mi.Index]);
				UpdateRecentFileMenu();		
				this.lbtype.Focus();
			} 
			catch (Exception ex) 
			{
				Helper.ExceptionMessage(Localization.Manager.GetString("err004"), ex);
			}
		}

		private void CloseFileHandleClick(object sender, System.EventArgs e)
		{
			if (sender!=null) if (!WarnOverwriteChanges()) return;
			if (package!=null) if (package.Reader!=null) package.Reader.Close();
			package = null;

			UpdateMenuItemState();			
			wloader.EnableMenuItems(this.miPlugins, null, null);
			RemoveCurrentPlugin();
			SimPe.Packages.StreamFactory.CloseAll();
		}


		private void PackedFileMenu_Popup(object sender, System.EventArgs e)
		{
			//pfmiextract.Enabled = miextract.Enabled;
			//pfmireplace.Enabled = pfmiextract.Enabled;
			pfmiadd.Enabled =  pfmiextract.Enabled;
			pfmidelete.Enabled  = pfmiextract.Enabled;

			//this.pfmiextract.Enabled = (this.fileList.SelectedItems.Count==1);
			this.pfmireplace.Enabled = (this.fileList.SelectedItems.Count==1);
			this.miclone.Enabled = (this.fileList.SelectedItems.Count>=1);
		}


		protected void UpdateRecentFileMenu()
		{
			try 
			{
				string[] names = reg.GetRecentFiles();
				int accel = 1;

				mirecent.MenuItems.Clear();
				foreach(string file in names) 
				{
					System.Windows.Forms.MenuItem mi = new MenuItem("&" + accel.ToString() + ". " + file,
						new EventHandler(OpenRecentClick));
					switch (accel)
					{
						case 1: mi.Shortcut = System.Windows.Forms.Shortcut.Ctrl1; break;
						case 2: mi.Shortcut = System.Windows.Forms.Shortcut.Ctrl2; break;
						case 3: mi.Shortcut = System.Windows.Forms.Shortcut.Ctrl3; break;
						case 4: mi.Shortcut = System.Windows.Forms.Shortcut.Ctrl4; break;
					}
					accel++;
					mirecent.MenuItems.Add(mi);
				}
			}
			catch (Exception ex) 
			{
				Helper.ExceptionMessage(Localization.Manager.GetString("errregistry"), ex);
			}
		}

		private void FolderCompareClick(object sender, System.EventArgs e)
		{
			try
			{	
				if (fbd.ShowDialog()==DialogResult.OK) 
				{
					string path1 = fbd.SelectedPath;
					if (fbd.ShowDialog()==DialogResult.OK) 
					{
						CompareForm form = new CompareForm();
						form.Execute(path1, fbd.SelectedPath);
					}
				}
			} 
			catch (Exception ex) 
			{
				Helper.ExceptionMessage("Error while trying to Compare Folders", ex);
			}
		}

		private void ComitFileAttributesClick(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{			
			if (fileList.SelectedItems.Count<1) return;

			foreach(ListViewItem item in fileList.SelectedItems) 
			{
				SimPe.Packages.PackedFileDescriptor pfd = (SimPe.Packages.PackedFileDescriptor)item.Tag;
				pfd.Type = Helper.HexStringToUInt(tbtype.Text);
				pfd.SubType = Helper.HexStringToUInt(tbsubtype.Text);
				pfd.Group = Helper.HexStringToUInt(tbgroup.Text);
				pfd.Instance = Helper.HexStringToUInt(tbinstance.Text);

				item.SubItems[0].Text = pfd.TypeName.Name +" ("+pfd.TypeName.shortname+", 0x"+pfd.Type.ToString("X")+")";
				item.SubItems[1].Text = "0x"+pfd.SubType.ToString("X");
				item.SubItems[2].Text = "0x"+pfd.Group.ToString("X");
				item.SubItems[3].Text = "0x"+pfd.Instance.ToString("X");
			}
		}

		private void TypeSelectClick(object sender, System.EventArgs e)
		{
			if (cbtypes.Tag != null) return;
			tbtype.Text = "0x"+Helper.HexString(((SimPe.Data.TypeAlias)cbtypes.Items[cbtypes.SelectedIndex]).Id);
		}

		
		private void SelectTypeByNameClick(object sender, System.EventArgs e)
		{
			cbtypes.Tag = true;
			Data.TypeAlias a = Data.MetaData.FindTypeAlias(Helper.HexStringToUInt(tbtype.Text));

			int ct=0;
			foreach(Data.TypeAlias i in cbtypes.Items) 
			{								
				if (i==a) 
				{
					cbtypes.SelectedIndex = ct;
					cbtypes.Tag = null;
					return;
				}
				ct++;
			}

			cbtypes.SelectedIndex = -1;
			cbtypes.Tag = null;
		}

		private void DeletePackedFileClick(object sender, System.EventArgs e)
		{
			if (fileList.SelectedItems.Count<1) return;

			DialogResult dr = DialogResult.Yes;			
			if (!Helper.WindowsRegistry.Silent) dr = MessageBox.Show(Localization.Manager.GetString("askdelete"), Localization.Manager.GetString("delete?"), System.Windows.Forms.MessageBoxButtons.YesNo);
			if ( dr == DialogResult.Yes) 
			{
				try 
				{
					foreach(ListViewItem item in fileList.SelectedItems) 
					{
						SimPe.Packages.PackedFileDescriptor pfd = (SimPe.Packages.PackedFileDescriptor)item.Tag;
						item.Remove();
						pfd.MarkForDelete = true;
						//package.Remove(pfd);
						lbcount.Text = "("+package.Header.Index.Count.ToString()+")";
					} //foreach
				}
				catch (Exception ex) 
				{
					Helper.ExceptionMessage(Localization.Manager.GetString("err005"), ex);
				}
			}
		}

		private void AddPackedFileClick(object sender, System.EventArgs e)
		{
			ofd.Multiselect = true;
			ofd.Filter = "Extracted File Description (*.xml)|*.xml|Package Description (*package.xml)|*package.xml|Sims 2 Package (*.package)|*.package|Extracted File (*.simpe)|*.simpe|All Files (*.*)|*.*";
			SimPe.Packages.PackedFileDescriptor[] pfds = OpenPackedFile();
			ofd.DefaultExt = "xml";
			ofd.Multiselect = false;

			foreach (SimPe.Packages.PackedFileDescriptor pfd in pfds)
			{
				try 
				{
					package.Add(pfd);
					
					this.AddFileItem(pfd, true);					
					lbcount.Text = "("+package.Header.Index.Count.ToString()+")";
				} 
				catch (Exception ex) 
				{
					Helper.ExceptionMessage(Localization.Manager.GetString("err006"), ex);
				}
			} 
			ofd.DefaultExt = "";
		}

		private void CreateNewPackageClick(object sender, System.EventArgs e)
		{
			if (!WarnOverwriteChanges()) return;

			Data.MetaData.IndexTypes type = Data.MetaData.IndexTypes.ptLongFileIndex;
			if (NewDialog.Execute(ref type)) 
			{
				this.RemoveCurrentPlugin();
				if (package!=null) if (package.Reader!=null) package.Reader.Close();

				if (package != null) package.FileName = "";
				UpdateMenuItemState();

				package = new SimPe.Packages.GeneratableFile(new System.IO.BinaryReader(new System.IO.MemoryStream()));
				package.FileName = "";
				package.Header.IndexType = type;
				Text = "SimPE";
				EnlistPackage();
				UpdateMenuItemState();
				wloader.EnableMenuItems(this.miPlugins, null, package);
			}
		}

		private void OnInstantSaveClick(object sender, System.EventArgs e)
		{
			//remove Plugin
			//tabPage5.Controls.Clear();

			if (package==null) return;

			sfd.Filter = "Sims 2 Package (*.package)|*.package|All Files (*.*)|*.*";
			if (System.IO.File.Exists(package.FileName)) 
			{
				try 
				{
					this.Cursor = Cursors.WaitCursor;
					this.RemoveCurrentPlugin();
#if INCSAVE
					package.Save();

					package.Close();
					package = null;
#else
					/*System.IO.MemoryStream ms = package.Build();
					if (package!=null) if (package.Reader!=null)  
									   {
										   package.Reader.Close();
									   }
					//package=null;

					package.Save(ms, package.FileName);*/
					package.Save();
					this.Text = "PLJones SimAntics Editor ["+package.FileName+"]";
#endif					
					//this.OpenPackage(package.FileName);		
					this.UpdateFileGroupFilter(null, null);
							
				} 
				catch (Exception ex) 
				{
					Helper.ExceptionMessage(Localization.Manager.GetString("errwritingfile")+" "+sfd.FileName, ex);
				} 
				finally 
				{
					this.Cursor = Cursors.Default;
				}

			} 
			else 
			{
				this.SavePackageClick(sender, e);
			}
		}

		private void UseHexViewClick(object sender, System.EventArgs e)
		{
			
			mihexview.Checked = !mihexview.Checked;
			lbHexView.Visible = !mihexview.Checked;
			panel4.Visible = panel2.Visible = mihexview.Checked;
			reg.HexViewState = mihexview.Checked;
#if DEBUG
			if (mihexview.Checked) 
			{					
				if (!tabControl1.TabPages.Contains(tabPage2)) tabControl1.TabPages.Add(tabPage2);
			} 
			else 
			{				
				if (tabControl1.TabPages.Contains(tabPage2)) tabControl1.TabPages.Remove(tabPage2);
			}
#endif
		}

		private void NoMetaInformationClick(object sender, System.EventArgs e)
		{
			minometa.Checked = !minometa.Checked;
			reg.LoadMetaInfo = !minometa.Checked;
		}

		private void HexOpenClick(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			if (fileList.SelectedItems.Count<1) return;
			Packages.PackedFileDescriptor pfd = (Packages.PackedFileDescriptor)fileList.SelectedItems[0].Tag;

			if (cbext.SelectedIndex<0) return;
			ToolLoaderListBoxItem tli = (ToolLoaderListBoxItem)cbext.Items[cbext.SelectedIndex];
			tli.Execute(pfd, package);

			ProcessPackedFile(null, null);
		}

		private void OptionsOpenClick(object sender, System.EventArgs e)
		{
			OptionForm f = new OptionForm();
			f.Execute();
		}

		private void MakeIntriguedNeighborhhodClick(object sender, System.EventArgs e)
		{
			if (package==null) return;
			try 
			{
				this.Cursor = Cursors.WaitCursor;
				IPackedFileDescriptor[] pfds = package.FindFiles(Data.MetaData.SIM_DESCRIPTION_FILE);

				SimPe.PackedFiles.Wrapper.SDesc sdesc = new SimPe.PackedFiles.Wrapper.SDesc(null, null, null);
				foreach(IPackedFileDescriptor pfd in pfds) 
				{
					sdesc.ProcessData(pfd, package);

					sdesc.Interests.Animals = 1000;
					sdesc.Interests.Crime = 1000;
					sdesc.Interests.Culture = 1000;
					sdesc.Interests.Entertainment = 1000;
					sdesc.Interests.Environment = 1000;
					sdesc.Interests.Fashion = 1000;
					sdesc.Interests.Food = 1000;
					sdesc.Interests.Health = 1000;
					sdesc.Interests.Money = 1000;
					sdesc.Interests.Paranormal = 1000;
					sdesc.Interests.Politics = 1000;
					sdesc.Interests.School = 1000;
					sdesc.Interests.Scifi = 1000;
					sdesc.Interests.Sports = 1000;
					sdesc.Interests.Toys = 1000;
					sdesc.Interests.Travel = 1000;
					sdesc.Interests.Weather = 1000;
					sdesc.Interests.Work = 1000;

					sdesc.SynchronizeUserData();
					
				}
			} 
			catch (Exception ex) 
			{
				Helper.ExceptionMessage(Localization.Manager.GetString("err007"), ex);
			}
			finally 
			{
				this.Cursor = Cursors.Default;
			}

			ProcessPackedFile(null, null);
		}

		private void RunSims2Clicked(object sender, System.EventArgs e)
		{
			if (!File.Exists(reg.SimsApplication)) return;

			Process p = new Process();
			p.StartInfo.FileName = reg.SimsApplication;
			if (Helper.WindowsRegistry.EnableSound) 
			{
				p.StartInfo.Arguments = "-w -r800x600 -skipintro -skipverify";
			} 
			else 
			{
				p.StartInfo.Arguments = "-w -r800x600 -nosound -skipintro -skipverify";
			}
			p.Start();
		}

		private void ListSimsClicked(object sender, System.EventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;
			try 
			{
				SimListing sl = new SimListing();
				sl.Execute(registry.SimNameProvider);
			} 
			finally 
			{
				this.Cursor = Cursors.Default;
			}
		}

		private void InstalledPluginsClick(object sender, System.EventArgs e)
		{
			InstalledPlugins ip = new InstalledPlugins();
			ip.Execute(registry);
		}

		private void OpenByteView(object sender, System.EventArgs e)
		{
			ByteView bv = new ByteView();
			if (fileList.SelectedItems.Count<1) return;

			try 
			{
				Packages.PackedFileDescriptor pfd = (Packages.PackedFileDescriptor)fileList.SelectedItems[0].Tag;
				if ((pfd.Size > 0x2800) && (!Helper.WindowsRegistry.Silent))
				{
					if (MessageBox.Show(Localization.Manager.GetString("usebigfile?"), Localization.Manager.GetString("confirm"), MessageBoxButtons.YesNo) == DialogResult.No) return;
				}
				bv.Execute(pfd , package, this, registry);
			} 
			catch (Exception ex) 
			{
				Cursor = Cursors.Default;
				Helper.ExceptionMessage(Localization.Manager.GetString("byteviewerror"), ex);
			}
		}

		private void DecodeFilenamesClicked(object sender, System.EventArgs e)
		{
			this.midecode.Checked = !this.midecode.Checked;
			reg.DecodeFilenamesState = this.midecode.Checked;
		}

		private void GroupFilterChanged(object sender, System.EventArgs e)
		{
			ComboBox cb = (ComboBox)sender;
			if (cb.SelectedIndex >=0) 
			{

				string[] s = cb.Text.Split(" ".ToCharArray(), 2);
				try 
				{
					cb.Tag = Convert.ToUInt32(s[0], 16);		
				} 
				catch (Exception) {
					cb.Tag = null;
				}
			}

			this.UpdateFileGroupFilter(null, null);
		}

		private void GroupFilterTextChanged(object sender, System.EventArgs e)
		{
			if (sender.GetType()==typeof(ComboBox)) 
			{
				ComboBox cb = (ComboBox)sender;
				try 
				{
					cb.Tag = Convert.ToUInt32(cb.Text, 16);		
				} 
				catch (Exception) 
				{
					cb.Tag = null;
				}
			} 
			else 
			{
				TextBox tb = (TextBox)sender;
				try 
				{
					tb.Tag = Convert.ToUInt32(tb.Text, 16);		
				} 
				catch (Exception) 
				{
					tb.Tag = null;
				}
			}

			this.UpdateFileGroupFilter(null, null);
		}

		private void ListMemoriesClick(object sender, System.EventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;
			try 
			{
				SimListing sl = new SimListing();
				sl.ExecuteMem(registry.OpcodeProvider);
			} 
			finally 
			{
				this.Cursor = Cursors.Default;
			}
		}

		private void HexDecConverterStart(object sender, System.EventArgs e)
		{
			Converter cnv = new Converter();
			cnv.Show();
		}

		private void GenerateFileList(object sender, System.EventArgs e)
		{
			System.IO.StreamWriter sw = new StreamWriter(new MemoryStream());
			sw.WriteLine("TypeName; Type; Group; SubType; Instance; Description;");

			this.Cursor = Cursors.WaitCursor;
			pb1.Maximum = fileList.Items.Count;
			pb1.Value = 0;
			pb1.Visible = true;
			//SimPe.PackedFiles.Wrapper.Picture pic = new SimPe.PackedFiles.Wrapper.Picture();

			WaitingScreen.Wait();
			try 
			{
				int max = fileList.Items.Count;
				for (int i=0; i<max; i++)
				{
					pb1.Value = i;
					System.Windows.Forms.Application.DoEvents();
					ListViewItem item = fileList.Items[i];

					Interfaces.Files.IPackedFileDescriptor pfd = (Interfaces.Files.IPackedFileDescriptor)fileList.Items[i].Tag;
					sw.Write(item.Text.Replace(";", ",") + "; ");					

					sw.Write("0x" + Helper.HexString(pfd.Type) + "; ");
					sw.Write("0x" + Helper.HexString(pfd.Group) + "; ");
					sw.Write("0x" + Helper.HexString(pfd.SubType) + "; ");
					sw.Write("0x" + Helper.HexString(pfd.Instance) + "; ");
					
					SimPe.Interfaces.Plugin.IFileWrapper wrapper = (SimPe.Interfaces.Plugin.IFileWrapper)registry.FindHandler(pfd.Type);				
					if (wrapper!=null) 
					{
						wrapper.ProcessData(pfd, package);
						sw.Write(wrapper.Description);
					}
					sw.WriteLine(";");					
					Application.DoEvents();
					WaitingScreen.UpdateMessage(i.ToString()+" / "+max.ToString());
				}

				sw.Flush();
				sw.BaseStream.Seek(0, SeekOrigin.Begin);
				StreamReader sr = new StreamReader(sw.BaseStream);
				sr.BaseStream.Seek(0, SeekOrigin.Begin);
				string list = sr.ReadToEnd();
				FileList fl = new FileList();
				fl.rtb.Text = list;
				if (lbtype.SelectedIndex>=0) fl.Text += " "+this.lbtype.Items[lbtype.SelectedIndex].ToString();
				fl.Show();
			}
			finally 
			{
				this.pb1.Visible = false;
				this.Cursor = Cursors.Default;
				WaitingScreen.Stop();
			}
			
		}

		private void AddCopyright(object sender, System.EventArgs e)
		{
/*			this.Cursor = Cursors.WaitCursor;
			pb1.Maximum = fileList.Items.Count;
			pb1.Value = 0;
			pb1.Visible = true;
			
			try 
			{
				for (int i=0; i<fileList.Items.Count; i++)
				{
					pb1.Value = i;
					ListViewItem item = fileList.Items[i];

					Interfaces.Files.IPackedFileDescriptor pfd = (Interfaces.Files.IPackedFileDescriptor)fileList.Items[i].Tag;
					if (!Data.MetaData.RcolList.Contains(pfd.Type)) continue;

					try 
					{
						SimPe.Plugin.Rcol gmnd = new SimPe.Plugin.GenericRcol(registry, false);
						gmnd.ProcessData(pfd, package);
						bool add = true;
						foreach (SimPe.Plugin.IRcolBlock irb in gmnd.Blocks) 
						{
							if (irb.BlockName == "cDataListExtension") 
							{
								SimPe.Plugin.DataListExtension dle = (SimPe.Plugin.DataListExtension)irb;
								if (dle.Extension.VarName.Trim().ToLower()=="copyright") 
								{
									add = false;
									break;
								}
							}
						}

						if (add) 
						{
							SimPe.Plugin.DataListExtension ndle = new SimPe.Plugin.DataListExtension(registry, null);
							ndle.Extension.VarName = "copyright";
							
							SimPe.Plugin.ExtensionItem[] items = new SimPe.Plugin.ExtensionItem[2];
	
							items[0] = new SimPe.Plugin.ExtensionItem();
							items[0].Typecode = SimPe.Plugin.ExtensionItem.ItemTypes.String;
							items[0].Name = "created by";
							items[0].String = "Numenor, RGiles";

							items[1] = new SimPe.Plugin.ExtensionItem(); 
							items[1].Typecode = SimPe.Plugin.ExtensionItem.ItemTypes.String;
							items[1].Name = "license";
							items[1].String = "This File was created as Part of the EnablerPackages from ModTheSims2. If you payed for a package that contains this File please report it to quaxi@ambertation.de.";

							ndle.Extension.Items = items;

							gmnd.Blocks = (SimPe.Plugin.IRcolBlock[])Helper.Add(gmnd.Blocks, ndle, typeof(SimPe.Plugin.IRcolBlock));
							gmnd.SynchronizeUserData();
						}
					} catch (Exception) {}	
				}
			}
			finally 
			{
				this.pb1.Visible = false;
				this.Cursor = Cursors.Default;
			}
			
*/		}

		private void ClonePackedFile(object sender, System.EventArgs e)
		{
			if (fileList.SelectedItems.Count<1) return;

			for (int i=0; i<fileList.SelectedItems.Count; i++) 
			{
				Interfaces.Files.IPackedFileDescriptor ppfd = (Interfaces.Files.IPackedFileDescriptor)fileList.SelectedItems[i].Tag;
				SimPe.Packages.PackedFileDescriptor pfd = new SimPe.Packages.PackedFileDescriptor();
				pfd.Group = ppfd.Group;
				pfd.Type = ppfd.Type;
				pfd.Instance = ppfd.Instance;
				pfd.SubType = ppfd.SubType;
				pfd.UserData = package.Read(ppfd).UncompressedData;
				try 
				{
					package.Add(pfd);
					
					this.AddFileItem(pfd, true);					
					lbcount.Text = "("+package.Header.Index.Count.ToString()+")";
				} 
				catch (Exception ex) 
				{
					Helper.ExceptionMessage(Localization.Manager.GetString("err006"), ex);
				}
			}
			
		}

		#region FileList Builder
		protected string LoadFileListFromDir(string folder, string list) 
		{
			string priv = "";
			string[] files = System.IO.Directory.GetFiles(folder, "*.package");
			foreach (string file in files)
			{
				SimPe.Packages.File fl = new SimPe.Packages.File(file);
				priv += Helper.lbr+"------------------------ "+file+Helper.lbr;
				foreach (SimPe.Interfaces.Files.IPackedFileDescriptor pfd in fl.Index) 
				{
					priv += pfd.ToString() + Helper.lbr;
				}
				/*priv += fl.Header.IndexType.ToString();
				priv += " ";
				if (fl.FileListFile.LongFormat)
					priv += "clst long";
				else
					priv += "clst short";

				if (fl.Header.IndexType==Data.MetaData.IndexTypes.ptShortFileIndex)
				{
					if (fl.FileListFile.LongFormat) priv += " differ";
				} 
				else 
				{
					if (!fl.FileListFile.LongFormat) priv += " differ";
				}
				priv += Helper.lbr;*/
			}

			string[] dirs = System.IO.Directory.GetDirectories(folder, "*");
			foreach (string dir in dirs)
			{
				try 
				{
					priv = LoadFileListFromDir(dir, priv);
				} 
				catch (Exception ex)
				{
					priv += dir + " Exception: " + ex.Message + Helper.lbr;
				}
			}

			return list + priv;
		}

		private void BuildFileList(object sender, System.EventArgs e)
		{
			if (fbd.ShowDialog()==DialogResult.OK) 
			{
				this.Cursor = Cursors.WaitCursor;
				try 
				{
					string list = LoadFileListFromDir(fbd.SelectedPath, "");
					list = list.Trim();
					FileList fl = new FileList();
					fl.rtb.Text = list;
					if (lbtype.SelectedIndex>=0) fl.Text += " "+this.lbtype.Items[lbtype.SelectedIndex].ToString();
					fl.Show();
				} 
				finally 
				{
					this.Cursor = Cursors.Default;
				}
			}
		}
		#endregion

		protected bool WarnWrapperChange()
		{
			if (currentwrapper!=null) 
			{
				if (currentwrapper.GetType().GetInterface("SimPe.Interfaces.Plugin.IFileWrapperSaveExtension", true)!=null)
				{
					SimPe.Interfaces.Plugin.IFileWrapperSaveExtension save = (SimPe.Interfaces.Plugin.IFileWrapperSaveExtension)currentwrapper;
					if (save.Changed) 
					{
						DialogResult di = DialogResult.No;
						if (!Helper.WindowsRegistry.Silent) di = MessageBox.Show(Localization.Manager.GetString("savewrapperchanges"), Localization.Manager.GetString("savechanges?"), MessageBoxButtons.YesNoCancel);
						
						if (di == DialogResult.Cancel) return false;
						if (di == DialogResult.Yes) save.SynchronizeUserData();
						else save.Changed = false;
					}
				}
			}

			return true;
		}

		internal bool WarnOverwriteChanges()
		{
			if (!WarnWrapperChange()) return false;

			if (HasUserChanges(package)) 
			{
				DialogResult di = DialogResult.No;
				if (!Helper.WindowsRegistry.Silent) di = MessageBox.Show(Localization.Manager.GetString("savechanges"), Localization.Manager.GetString("savechanges?"), MessageBoxButtons.YesNoCancel);
				if (di == DialogResult.Cancel) return false;
				if (di == DialogResult.Yes) this.OnInstantSaveClick(null, null);
			}

			return true;
		}

		protected bool HasUserChanges(Interfaces.Files.IPackageFile pk)
		{
			if (pk==null) return false;
			return pk.HasUserChanges;
		}

		private void ToolChangePacakge(object sender, System.EventArgs e)
		{
			string filename = "";

			try 
			{
				PackageArg pk = (PackageArg)e;
				if (pk.Result.ChangedPackage) 
				{
					this.RemoveCurrentPlugin();
					package = null;

					package = (SimPe.Packages.GeneratableFile)pk.Package;
					if (package!=null) 
					{
						filename = package.FileName;
						if (filename==null) filename="";
					} 
					else 
					{
						package = null;
					}

					if (package==null) package=null;
				}

				if (pk.Result.ChangedFile) 
				{					
					this.RemoveCurrentPlugin();
					if (!UpdateFileListSelection(pk.FileDescriptor)) SelectPackedFile(pk.FileDescriptor);
					

					//show PluginView if available
					if (tabPage5.Controls.Count>0) 
					{
						//find PluginView Index and select it
						for(int i=0; i<tabControl1.TabPages.Count; i++) 
						{	
							if (tabControl1.TabPages[i]==tabPage5) 
							{
								tabControl1.SelectedIndex = i;
								break;
							}
						}//for i
					}
				}

				if (pk.Result.ChangedPackage) 
				{
					reg.AddRecentFile(filename);
					UpdatePackageData(filename);
					this.UpdateMenuItemState();
					this.UpdateRecentFileMenu();
				}
			} 
			catch (Exception ex) {
				Helper.ExceptionMessage("",ex);
			}

			if (package!=null) filename = package.FileName;
			Text = "SimPE ("+filename+")";
		}

		/// <summary>
		/// Selects the passsed File if available
		/// </summary>
		/// <param name="pfd"></param>
		internal bool UpdateFileListSelection(Interfaces.Files.IPackedFileDescriptor pfd) 
		{
			if (pfd==null) return false;

			bool select = false;
			for (int i=1; i<this.lbtype.Items.Count; i++)
			{
				SimPe.Data.TypeAlias ta = (SimPe.Data.TypeAlias)this.lbtype.Items[i];
				if (ta.Id == pfd.Type) 
				{
					this.lbtype.SelectedIndex = i;
					select = true;
					break;
				}
			}

			if (!select) this.lbtype.SelectedIndex=0;

			fileList.SelectedItems.Clear();
			//fileList.MultiSelect = false;
			foreach (ListViewItem lvi in this.fileList.Items) 
			{
				Interfaces.Files.IPackedFileDescriptor itempfd = (Interfaces.Files.IPackedFileDescriptor)lvi.Tag;						
				if (itempfd.Equals(pfd)) 
				{
					lvi.Selected = true;	
					lvi.EnsureVisible();	
					return true;
				}
			}
			//fileList.MultiSelect = true;

			return false;
		}

		#region Drag&Drop
		/**
		 * @brief Jemand will etwas Abwerfen
		 * */
		private void DragEnterFile(object sender, System.Windows.Forms.DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop)) 
			{
				try
				{
					Array a = (Array)e.Data.GetData(DataFormats.FileDrop);

					if ( a != null )
					{
						string s = a.GetValue(0).ToString().ToLower();
						e.Effect = DragDropEffects.Copy;
					}

					
				} 
				catch (Exception)
				{
				}
				
			}
			else 
			{
				e.Effect = DragDropEffects.None;
			}
		}

		/**
		 * @brief Jemand hat einen Datei abgeworfen
		 * */
		private void DragDropFile(object sender, System.Windows.Forms.DragEventArgs e)
		{
			try
			{
				Array a = (Array)e.Data.GetData(DataFormats.FileDrop);

				if ( a != null )
				{
					// Extract string from first array element
					// (ignore all files except first if number of files are dropped).
					string s = a.GetValue(0).ToString();

					

					this.Activate();        // in the case Explorer overlaps this form

					this.OpenPackage(s);
					this.reg.AddRecentFile(s);							
					UpdateRecentFileMenu();
					//this.LoadFile(s);
				}
			}
			catch (Exception)
			{
			}

		}
		#endregion

		private void ChangeGroups(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			foreach (ListViewItem lvi in fileList.Items)
			{
				try 
				{
					SimPe.Packages.PackedFileDescriptor pfd = (SimPe.Packages.PackedFileDescriptor)lvi.Tag;
					pfd.Group = Helper.HexStringToUInt(tbgroup.Text);

					lvi.SubItems[2].Text = "0x"+pfd.Group.ToString("X");
				} 
				catch (Exception) {}
			}
		}


		private void AddS2CPID(object sender, System.EventArgs e)
		{
			if (package==null) return;
			if (package.FileName == null) return;
			if (!System.IO.File.Exists(package.FileName)) return;

			string name = System.IO.Path.GetFileName(package.FileName);
			string author = "";
			string title = "";
			string description = "";
			string contact = "";
			string gameguid = "";
			SimPe.Packages.S2CPDescriptor.GetSetGlobalGuid(package, ref name, ref title, ref author, ref contact, ref description, ref gameguid);

			UpdatePackageData(package.FileName);
		}

		private void SaveS2CP(object sender, System.EventArgs e)
		{
			if ((package==null) || (package.FileName == null) || (!System.IO.File.Exists(package.FileName))) SimPe.Packages.Sims2CommunityPack.ShowSaveDialog(false);
			else 
			{
				if (SimPe.Packages.Sims2CommunityPack.ShowSaveDialog(package, false)) 
				{
					package.Save();
				} 
			}
						
		}

		private void FixIntegrity(object sender, System.EventArgs e)
		{
			if (package==null) return;

			this.Cursor = Cursors.WaitCursor;
			try 
			{
				foreach (Interfaces.Files.IPackedFileDescriptor pfd in package.Index)
				{
					//Do we have a registred handler?
					SimPe.Interfaces.Plugin.IFileWrapper wrapper = (SimPe.Interfaces.Plugin.IFileWrapper)registry.FindHandler(pfd.Type);
					SimPe.Interfaces.Files.IPackedFile file = package.Read(pfd);
					if (wrapper==null) wrapper = registry.FindHandler(file.UncompressedData);

					if (wrapper!=null) 
					{
						wrapper.ProcessData(pfd, package);
						wrapper.Fix(registry);
					}
				}

				this.UpdatePackageData(package.FileName);
			} 
			finally 
			{
				this.Cursor = Cursors.Default;
			}
		}

		private void MakeUnique(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			uint inst = Helper.HexStringToUInt(tbinstance.Text);
			foreach (ListViewItem lvi in fileList.Items)
			{
				try 
				{
					SimPe.Packages.PackedFileDescriptor pfd = (SimPe.Packages.PackedFileDescriptor)lvi.Tag;
					pfd.Instance = inst++;

					lvi.SubItems[3].Text = "0x"+pfd.Instance.ToString("X");
				} 
				catch (Exception) {}
			}
		}

		private void CreateNameMap(object sender, System.EventArgs e)
		{
/*			if (package==null) return;

			if (lbtype.SelectedIndex>0) 
			{
				TypeAlias a = (TypeAlias)lbtype.Items[lbtype.SelectedIndex];
				if (Data.MetaData.RcolList.Contains(a.Id)) 
				{
					SimPe.Packages.PackedFileDescriptor fd = new SimPe.Packages.PackedFileDescriptor();
					fd.Type = Data.MetaData.NAME_MAP;
					fd.Group = 0x52737256;
					fd.Instance = a.Id;
					fd.SubType = 0;
					
					SimPe.Plugin.Nmap nmap = new SimPe.Plugin.Nmap(registry);
					nmap.FileDescriptor = fd;
					bool add = false;
					if (package.FindFile(fd)==null) add = true;
					
					ArrayList list = new ArrayList();
					foreach (ListViewItem lvi in fileList.Items)
					{
						try 
						{
							SimPe.Packages.PackedFileDescriptor pfd = (SimPe.Packages.PackedFileDescriptor)lvi.Tag;

							SimPe.Plugin.Rcol rcol = new SimPe.Plugin.GenericRcol(null, false);
							rcol.ProcessData(pfd, package);

							pfd.Filename = rcol.FileName;
							list.Add(pfd);
						} 
						catch (Exception) {}
					} //foreach

					nmap.Items = new SimPe.Packages.PackedFileDescriptor[list.Count];
					list.CopyTo(nmap.Items);

					nmap.SynchronizeUserData();
					if (add) package.Add(nmap.FileDescriptor);
				}
			}
*/		}

		private void ShowTutorials(object sender, System.EventArgs e)
		{
			Help help = new Help();
			help.ShowDialog();
		}

		private void ReloadFileTable(object sender, System.EventArgs e)
		{
			SimPe.FileTable.FileIndex.ForceReload();
		}

		private void CheckForOnlineUpdate(object sender, System.EventArgs e)
		{
			GetUpdate gu = new GetUpdate();
			//gu.llno.Visible = !WebUpdate.CheckUpdate();
			//gu.llyes.Visible = !gu.llno.Visible;
			gu.llno.Visible = false;
			gu.llyes.Visible = true;
			gu.ShowDialog();
		}

		private void ToogleCompression(object sender, System.EventArgs e)
		{
			if (package==null) return;

			CheckBox cb = (CheckBox)sender;
			if (cb.Tag != null) return;
			foreach (ListViewItem lvi in this.fileList.SelectedItems) 
			{
				Interfaces.Files.IPackedFileDescriptor pfd = (Interfaces.Files.IPackedFileDescriptor)lvi.Tag;
				
				if (!pfd.HasUserdata) pfd.UserData = package.Read(pfd).UncompressedData;

				pfd.MarkForReCompress = cb.Checked;
			}
		}

		private void llexportraw_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			try 
			{
				if (fileList.SelectedItems.Count==0) return;
				if (sfd.ShowDialog()==DialogResult.OK) 
				{
					Interfaces.Files.IPackedFileDescriptor pfd = (Interfaces.Files.IPackedFileDescriptor)fileList.SelectedItems[0].Tag;
					byte[] data = package.Read(pfd).PlainData;

					BinaryWriter bw = new BinaryWriter(System.IO.File.Create(sfd.FileName));
					try 
					{
						bw.Write(data);
					} 
					finally 
					{
						bw.BaseStream.Close();
					}
				}
			}
			catch (Exception ex)
			{
				Helper.ExceptionMessage("", ex);
			}
		}

		private void llimpraw_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			try 
			{
				if (fileList.SelectedItems.Count==0) return;
				if (ofd.ShowDialog()==DialogResult.OK) 
				{
					Interfaces.Files.IPackedFileDescriptor pfd = (Interfaces.Files.IPackedFileDescriptor)fileList.SelectedItems[0].Tag;
					SimPe.Interfaces.Files.IPackedFile file = package.Read(pfd);

					BinaryReader br = new BinaryReader(System.IO.File.OpenRead(ofd.FileName));
					
					try 
					{
						byte[] data = br.ReadBytes((int)br.BaseStream.Length);
						pfd.UserData = SimPe.Packages.PackedFile.Uncompress(data, file.UncompressedSize, 9);
					} 
					finally 
					{
						br.BaseStream.Close();
					}
				}
			}
			catch (Exception ex)
			{
				Helper.ExceptionMessage("", ex);
			}
		}

		public void miAbout_Click(object sender, System.EventArgs e)
		{
			System.Windows.Forms.MessageBox.Show(GPL,
				"About PLJones SimAntics Editor",
				System.Windows.Forms.MessageBoxButtons.OK,
				System.Windows.Forms.MessageBoxIcon.Information);
		}
	}
}
