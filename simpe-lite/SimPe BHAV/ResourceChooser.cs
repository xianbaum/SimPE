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
using SimPe.Plugin;
using SimPe.Interfaces;
using SimPe.Interfaces.Files;
using SimPe.Interfaces.Plugin;
using SimPe.PackedFiles.Wrapper;

namespace pjse
{
	/// <summary>
	/// Summary description for ResourceChooser.
	/// </summary>
	public class ResourceChooser : System.Windows.Forms.Form
	{
		#region Form variables

		private System.Windows.Forms.Button OK;
		private System.Windows.Forms.Button Cancel;
		private System.Windows.Forms.TabControl tcResources;
		private System.Windows.Forms.ListBox lbBuiltIn;
		private System.Windows.Forms.ListBox lbGlobalGroup;
		private System.Windows.Forms.ListBox lbSemiGroup;
		private System.Windows.Forms.ListBox lbGroup;
		private System.Windows.Forms.TabPage tpBuiltIn;
		private System.Windows.Forms.TabPage tpGlobalGroup;
		private System.Windows.Forms.TabPage tpSemiGroup;
		private System.Windows.Forms.TabPage tpGroup;
		private System.Windows.Forms.TabPage tpPackage;
		private System.Windows.Forms.ListBox lbPackage;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion

		public ResourceChooser()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
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


		#region ResourceChooser
		public pjse.FileTable.Entry Execute(uint resourceType, uint group, Control form)
		{
			return Execute(resourceType, group, form, 0);
		}

		public pjse.FileTable.Entry Execute(uint resourceType, uint group, Control form, byte skip_pages)
		{
			form.Cursor = Cursors.WaitCursor;
			this.Cursor = Cursors.WaitCursor;

			this.tcResources.TabPages.Clear();

			// There doesn't appear to be a way to compare two paths and have the OS decide if they refer to the same object
			if (((skip_pages & 0x01) == 0) 
				&& pjse.FileTable.GFT.CurrentPackage != null 
				&& pjse.FileTable.GFT.CurrentPackage.FileName != null 
				&& !pjse.FileTable.GFT.CurrentPackage.FileName.EndsWith("objects.package"))
				FillPackage(resourceType, this.lbPackage, this.tpPackage);

			if ((skip_pages & 0x02) == 0)
				FillGroup(resourceType, group, this.lbGroup, this.tpGroup);

			if ((skip_pages & 0x04) == 0)
			{
				Glob g = pjse.BhavWiz.GlobByGroup(group);
                if (g != null)
                {
                    FillGroup(resourceType, g.SemiGlobalGroup, this.lbSemiGroup, this.tpSemiGroup);
                    this.tpSemiGroup.Text = g.SemiGlobalName;
                }
			}

			if ((skip_pages & 0x08) == 0 && group != (uint)Group.Global)
				FillGroup(resourceType, (uint)Group.Global, this.lbGlobalGroup, this.tpGlobalGroup);

			if ((skip_pages & 0x10) == 0)
				FillBuiltIn(resourceType, this.lbBuiltIn, this.tpBuiltIn);

			form.Cursor = Cursors.Default;
			this.Cursor = Cursors.Default;

			DialogResult dr = ShowDialog();
			Close();

			if (dr == System.Windows.Forms.DialogResult.OK)
			{
				if (this.tcResources.SelectedTab == this.tpPackage && lbPackage.SelectedIndex >= 0)
					return (pjse.FileTable.Entry)lbPackage.Items[lbPackage.SelectedIndex];

				if (this.tcResources.SelectedTab == this.tpGroup && lbGroup.SelectedIndex >= 0)
					return (pjse.FileTable.Entry)lbGroup.Items[lbGroup.SelectedIndex];

				if (this.tcResources.SelectedTab == this.tpSemiGroup && lbSemiGroup.SelectedIndex >= 0)
					return (pjse.FileTable.Entry)lbSemiGroup.Items[lbSemiGroup.SelectedIndex];

				if (this.tcResources.SelectedTab == this.tpGlobalGroup && lbGlobalGroup.SelectedIndex >= 0)
					return (pjse.FileTable.Entry)lbGlobalGroup.Items[lbGlobalGroup.SelectedIndex];

				if (this.tcResources.SelectedTab == this.tpBuiltIn && lbBuiltIn.SelectedIndex >= 0)
					return returnBuiltIn((SimPe.Data.Alias)lbBuiltIn.Items[lbBuiltIn.SelectedIndex]);
			}
			return null;
		}


		private void FillPackage(uint type, ListBox list, TabPage tab)
		{
			Fill(pjse.FileTable.GFT[pjse.FileTable.GFT.CurrentPackage, type], list, tab);
		}

		private void FillGroup(uint type, uint group, ListBox list, TabPage tab)
		{
			Fill(pjse.FileTable.GFT[type, group], list, tab);
		}

		private void Fill(pjse.FileTable.Entry[] items, ListBox list, TabPage tab)
		{
			list.Items.Clear();

			list.Items.AddRange(items);
			this.tcResources.TabPages.Add(tab);
			if (list.Items.Count > 0)
				list.SelectedIndex = 0;
		}


		private void FillBuiltIn(uint type, ListBox list, TabPage tab)
		{
			list.Items.Clear();

			if (type == SimPe.Data.MetaData.BHAV_FILE)
			{
				uint i = 0;
				foreach (string s in BhavWiz.readStr(pjse.GS.BhavStr.Primitives))
				{
					if (!s.StartsWith("~"))
						list.Items.Add(new SimPe.Data.Alias(i, s));
					i++;
				}
			}
			this.tcResources.TabPages.Add(tab);
			if (list.Items.Count > 0)
				list.SelectedIndex = 0;
		}

		private pjse.FileTable.Entry returnBuiltIn(SimPe.Data.Alias a)
		{
			IPackedFileDescriptor pfd = new SimPe.Packages.PackedFileDescriptor();
			pfd.Instance = a.Id;
			return new pjse.FileTable.Entry(null, pfd);
		}

		#endregion

		#region Vom Windows Form-Designer generierter Code
		/// <summary>
		/// Erforderliche Methode für die Designerunterstützung. 
		/// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ResourceChooser));
            this.tcResources = new System.Windows.Forms.TabControl();
            this.tpPackage = new System.Windows.Forms.TabPage();
            this.lbPackage = new System.Windows.Forms.ListBox();
            this.tpGlobalGroup = new System.Windows.Forms.TabPage();
            this.lbGlobalGroup = new System.Windows.Forms.ListBox();
            this.tpGroup = new System.Windows.Forms.TabPage();
            this.lbGroup = new System.Windows.Forms.ListBox();
            this.tpSemiGroup = new System.Windows.Forms.TabPage();
            this.lbSemiGroup = new System.Windows.Forms.ListBox();
            this.tpBuiltIn = new System.Windows.Forms.TabPage();
            this.lbBuiltIn = new System.Windows.Forms.ListBox();
            this.OK = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.tcResources.SuspendLayout();
            this.tpPackage.SuspendLayout();
            this.tpGlobalGroup.SuspendLayout();
            this.tpGroup.SuspendLayout();
            this.tpSemiGroup.SuspendLayout();
            this.tpBuiltIn.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcResources
            // 
            resources.ApplyResources(this.tcResources, "tcResources");
            this.tcResources.Controls.Add(this.tpPackage);
            this.tcResources.Controls.Add(this.tpGlobalGroup);
            this.tcResources.Controls.Add(this.tpGroup);
            this.tcResources.Controls.Add(this.tpSemiGroup);
            this.tcResources.Controls.Add(this.tpBuiltIn);
            this.tcResources.Name = "tcResources";
            this.tcResources.SelectedIndex = 0;
            // 
            // tpPackage
            // 
            this.tpPackage.Controls.Add(this.lbPackage);
            resources.ApplyResources(this.tpPackage, "tpPackage");
            this.tpPackage.Name = "tpPackage";
            // 
            // lbPackage
            // 
            resources.ApplyResources(this.lbPackage, "lbPackage");
            this.lbPackage.Name = "lbPackage";
            this.lbPackage.Sorted = true;
            this.lbPackage.DoubleClick += new System.EventHandler(this.listBox_DoubleClick);
            // 
            // tpGlobalGroup
            // 
            this.tpGlobalGroup.Controls.Add(this.lbGlobalGroup);
            resources.ApplyResources(this.tpGlobalGroup, "tpGlobalGroup");
            this.tpGlobalGroup.Name = "tpGlobalGroup";
            // 
            // lbGlobalGroup
            // 
            resources.ApplyResources(this.lbGlobalGroup, "lbGlobalGroup");
            this.lbGlobalGroup.Name = "lbGlobalGroup";
            this.lbGlobalGroup.Sorted = true;
            this.lbGlobalGroup.DoubleClick += new System.EventHandler(this.listBox_DoubleClick);
            // 
            // tpGroup
            // 
            this.tpGroup.Controls.Add(this.lbGroup);
            resources.ApplyResources(this.tpGroup, "tpGroup");
            this.tpGroup.Name = "tpGroup";
            // 
            // lbGroup
            // 
            resources.ApplyResources(this.lbGroup, "lbGroup");
            this.lbGroup.Name = "lbGroup";
            this.lbGroup.Sorted = true;
            this.lbGroup.DoubleClick += new System.EventHandler(this.listBox_DoubleClick);
            // 
            // tpSemiGroup
            // 
            this.tpSemiGroup.Controls.Add(this.lbSemiGroup);
            resources.ApplyResources(this.tpSemiGroup, "tpSemiGroup");
            this.tpSemiGroup.Name = "tpSemiGroup";
            // 
            // lbSemiGroup
            // 
            resources.ApplyResources(this.lbSemiGroup, "lbSemiGroup");
            this.lbSemiGroup.Name = "lbSemiGroup";
            this.lbSemiGroup.Sorted = true;
            this.lbSemiGroup.DoubleClick += new System.EventHandler(this.listBox_DoubleClick);
            // 
            // tpBuiltIn
            // 
            this.tpBuiltIn.Controls.Add(this.lbBuiltIn);
            resources.ApplyResources(this.tpBuiltIn, "tpBuiltIn");
            this.tpBuiltIn.Name = "tpBuiltIn";
            // 
            // lbBuiltIn
            // 
            resources.ApplyResources(this.lbBuiltIn, "lbBuiltIn");
            this.lbBuiltIn.Name = "lbBuiltIn";
            this.lbBuiltIn.Sorted = true;
            this.lbBuiltIn.DoubleClick += new System.EventHandler(this.listBox_DoubleClick);
            // 
            // OK
            // 
            resources.ApplyResources(this.OK, "OK");
            this.OK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OK.Name = "OK";
            // 
            // Cancel
            // 
            resources.ApplyResources(this.Cancel, "Cancel");
            this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel.Name = "Cancel";
            // 
            // ResourceChooser
            // 
            this.AcceptButton = this.OK;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.Cancel;
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.OK);
            this.Controls.Add(this.tcResources);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "ResourceChooser";
            this.ShowInTaskbar = false;
            this.tcResources.ResumeLayout(false);
            this.tpPackage.ResumeLayout(false);
            this.tpGlobalGroup.ResumeLayout(false);
            this.tpGroup.ResumeLayout(false);
            this.tpSemiGroup.ResumeLayout(false);
            this.tpBuiltIn.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		private void listBox_DoubleClick(object sender, System.EventArgs e)
		{
			this.DialogResult = System.Windows.Forms.DialogResult.OK;
			Close();
		}

	}

}