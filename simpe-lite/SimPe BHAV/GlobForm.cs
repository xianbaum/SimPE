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
using SimPe.Interfaces.Plugin;
using SimPe.PackedFiles.Wrapper;

namespace SimPe.PackedFiles.UserInterface
{
	/// <summary>
	/// Zusammenfassung für MyPackedFileForm.
	/// </summary>
	public class GlobForm : System.Windows.Forms.Form, IPackedFileUI
	{
		#region Form variables
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Panel panel6;
		private System.Windows.Forms.Label lbglobfile;
		private System.Windows.Forms.Label label46;
		private System.Windows.Forms.Label label42;
		private System.Windows.Forms.ComboBox cbseminame;
		private System.Windows.Forms.Label label43;
		private System.Windows.Forms.TextBox tbgroup;
		private System.Windows.Forms.Panel globPanel;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion
       
		public GlobForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			cbseminame.Items.Clear();
			BuildGlobList();
			foreach (Data.SemiGlobalAlias a in globs) if (a.Known) cbseminame.Items.Add(a);
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

		
		#region Glob
		/// <summary>
		/// Stores the currently active Wrapper
		/// </summary>
		internal Glob wrapper = null;
		static System.Collections.ArrayList globs = null;
		static void BuildGlobList() 
		{
			if (globs!=null) return;
			globs = new System.Collections.ArrayList();

			FileTable.FileIndex.Load();
			Interfaces.Scenegraph.IScenegraphFileIndexItem[] iglobs = FileTable.FileIndex.FindFile(Data.MetaData.GLOB_FILE, true);
			System.Collections.ArrayList names = new System.Collections.ArrayList();
			string max = " / "+iglobs.Length.ToString();
			int ct = 0;
			foreach (Interfaces.Scenegraph.IScenegraphFileIndexItem item in iglobs) 
			{
				WaitingScreen.UpdateMessage(ct.ToString()+max);
				ct++;

				NamedGlob glob = new NamedGlob();
				glob.ProcessData(item.FileDescriptor, item.Package);

				if (!names.Contains(glob.SemiGlobalName.Trim().ToLower())) 
				{
					Data.SemiGlobalAlias g = new SimPe.Data.SemiGlobalAlias(true, glob.SemiGlobalGroup, glob.SemiGlobalName);
					globs.Add(g);
					names.Add(glob.SemiGlobalName.Trim().ToLower());
				}
			}
		}
		#endregion

		#region IPackedFileUI Member
		/// <summary>
		/// Returns the Panel that will be displayed within SimPe
		/// </summary>
		public System.Windows.Forms.Control GUIHandle
		{
			get
			{
				return globPanel;
			}
		}

		/// <summary>
		/// Is called by SimPe (through the Wrapper) when the Panel is going to be displayed, so
		/// you should updatet the Data displayed by the Panel with the Attributes stored in the
		/// passed Wrapper.
		/// </summary>
		/// <remarks>attr.Tag is used to let TextChanged event handlers know the change is being
		/// made internally rather than by the users.</remarks>
		/// <param name="wrp">The Attributes of this Wrapper have to be displayed</param>
		public void UpdateGUI(IFileWrapper wrp)
		{
			//wrapper;
			wrapper = (Glob)wrp;
			cbseminame.Tag = true;
			lbglobfile.Text = wrapper.FileName;
			cbseminame.Text = wrapper.SemiGlobalName;
			cbseminame.Tag = null;
			SemiGlobalChanged(null, null);
		}
		#endregion

		#region Vom Windows Form-Designer generierter Code
		/// <summary>
		/// Erforderliche Methode für die Designerunterstützung. 
		/// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(GlobForm));
			this.globPanel = new System.Windows.Forms.Panel();
			this.tbgroup = new System.Windows.Forms.TextBox();
			this.label43 = new System.Windows.Forms.Label();
			this.cbseminame = new System.Windows.Forms.ComboBox();
			this.label42 = new System.Windows.Forms.Label();
			this.button3 = new System.Windows.Forms.Button();
			this.panel6 = new System.Windows.Forms.Panel();
			this.lbglobfile = new System.Windows.Forms.Label();
			this.label46 = new System.Windows.Forms.Label();
			this.globPanel.SuspendLayout();
			this.panel6.SuspendLayout();
			this.SuspendLayout();
			// 
			// globPanel
			// 
			this.globPanel.AccessibleDescription = resources.GetString("globPanel.AccessibleDescription");
			this.globPanel.AccessibleName = resources.GetString("globPanel.AccessibleName");
			this.globPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("globPanel.Anchor")));
			this.globPanel.AutoScroll = ((bool)(resources.GetObject("globPanel.AutoScroll")));
			this.globPanel.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("globPanel.AutoScrollMargin")));
			this.globPanel.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("globPanel.AutoScrollMinSize")));
			this.globPanel.BackColor = System.Drawing.SystemColors.Control;
			this.globPanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("globPanel.BackgroundImage")));
			this.globPanel.Controls.Add(this.tbgroup);
			this.globPanel.Controls.Add(this.label43);
			this.globPanel.Controls.Add(this.cbseminame);
			this.globPanel.Controls.Add(this.label42);
			this.globPanel.Controls.Add(this.button3);
			this.globPanel.Controls.Add(this.panel6);
			this.globPanel.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("globPanel.Dock")));
			this.globPanel.Enabled = ((bool)(resources.GetObject("globPanel.Enabled")));
			this.globPanel.Font = ((System.Drawing.Font)(resources.GetObject("globPanel.Font")));
			this.globPanel.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("globPanel.ImeMode")));
			this.globPanel.Location = ((System.Drawing.Point)(resources.GetObject("globPanel.Location")));
			this.globPanel.Name = "globPanel";
			this.globPanel.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("globPanel.RightToLeft")));
			this.globPanel.Size = ((System.Drawing.Size)(resources.GetObject("globPanel.Size")));
			this.globPanel.TabIndex = ((int)(resources.GetObject("globPanel.TabIndex")));
			this.globPanel.Text = resources.GetString("globPanel.Text");
			this.globPanel.Visible = ((bool)(resources.GetObject("globPanel.Visible")));
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
			this.tbgroup.ReadOnly = true;
			this.tbgroup.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbgroup.RightToLeft")));
			this.tbgroup.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbgroup.ScrollBars")));
			this.tbgroup.Size = ((System.Drawing.Size)(resources.GetObject("tbgroup.Size")));
			this.tbgroup.TabIndex = ((int)(resources.GetObject("tbgroup.TabIndex")));
			this.tbgroup.Text = resources.GetString("tbgroup.Text");
			this.tbgroup.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbgroup.TextAlign")));
			this.tbgroup.Visible = ((bool)(resources.GetObject("tbgroup.Visible")));
			this.tbgroup.WordWrap = ((bool)(resources.GetObject("tbgroup.WordWrap")));
			// 
			// label43
			// 
			this.label43.AccessibleDescription = resources.GetString("label43.AccessibleDescription");
			this.label43.AccessibleName = resources.GetString("label43.AccessibleName");
			this.label43.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label43.Anchor")));
			this.label43.AutoSize = ((bool)(resources.GetObject("label43.AutoSize")));
			this.label43.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label43.Dock")));
			this.label43.Enabled = ((bool)(resources.GetObject("label43.Enabled")));
			this.label43.Font = ((System.Drawing.Font)(resources.GetObject("label43.Font")));
			this.label43.Image = ((System.Drawing.Image)(resources.GetObject("label43.Image")));
			this.label43.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label43.ImageAlign")));
			this.label43.ImageIndex = ((int)(resources.GetObject("label43.ImageIndex")));
			this.label43.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label43.ImeMode")));
			this.label43.Location = ((System.Drawing.Point)(resources.GetObject("label43.Location")));
			this.label43.Name = "label43";
			this.label43.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label43.RightToLeft")));
			this.label43.Size = ((System.Drawing.Size)(resources.GetObject("label43.Size")));
			this.label43.TabIndex = ((int)(resources.GetObject("label43.TabIndex")));
			this.label43.Text = resources.GetString("label43.Text");
			this.label43.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label43.TextAlign")));
			this.label43.Visible = ((bool)(resources.GetObject("label43.Visible")));
			// 
			// cbseminame
			// 
			this.cbseminame.AccessibleDescription = resources.GetString("cbseminame.AccessibleDescription");
			this.cbseminame.AccessibleName = resources.GetString("cbseminame.AccessibleName");
			this.cbseminame.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cbseminame.Anchor")));
			this.cbseminame.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cbseminame.BackgroundImage")));
			this.cbseminame.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cbseminame.Dock")));
			this.cbseminame.Enabled = ((bool)(resources.GetObject("cbseminame.Enabled")));
			this.cbseminame.Font = ((System.Drawing.Font)(resources.GetObject("cbseminame.Font")));
			this.cbseminame.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cbseminame.ImeMode")));
			this.cbseminame.IntegralHeight = ((bool)(resources.GetObject("cbseminame.IntegralHeight")));
			this.cbseminame.ItemHeight = ((int)(resources.GetObject("cbseminame.ItemHeight")));
			this.cbseminame.Location = ((System.Drawing.Point)(resources.GetObject("cbseminame.Location")));
			this.cbseminame.MaxDropDownItems = ((int)(resources.GetObject("cbseminame.MaxDropDownItems")));
			this.cbseminame.MaxLength = ((int)(resources.GetObject("cbseminame.MaxLength")));
			this.cbseminame.Name = "cbseminame";
			this.cbseminame.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cbseminame.RightToLeft")));
			this.cbseminame.Size = ((System.Drawing.Size)(resources.GetObject("cbseminame.Size")));
			this.cbseminame.TabIndex = ((int)(resources.GetObject("cbseminame.TabIndex")));
			this.cbseminame.Text = resources.GetString("cbseminame.Text");
			this.cbseminame.Visible = ((bool)(resources.GetObject("cbseminame.Visible")));
			this.cbseminame.TextChanged += new System.EventHandler(this.SemiGlobalChanged);
			this.cbseminame.SelectedIndexChanged += new System.EventHandler(this.SemiGlobalChanged);
			// 
			// label42
			// 
			this.label42.AccessibleDescription = resources.GetString("label42.AccessibleDescription");
			this.label42.AccessibleName = resources.GetString("label42.AccessibleName");
			this.label42.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label42.Anchor")));
			this.label42.AutoSize = ((bool)(resources.GetObject("label42.AutoSize")));
			this.label42.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label42.Dock")));
			this.label42.Enabled = ((bool)(resources.GetObject("label42.Enabled")));
			this.label42.Font = ((System.Drawing.Font)(resources.GetObject("label42.Font")));
			this.label42.Image = ((System.Drawing.Image)(resources.GetObject("label42.Image")));
			this.label42.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label42.ImageAlign")));
			this.label42.ImageIndex = ((int)(resources.GetObject("label42.ImageIndex")));
			this.label42.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label42.ImeMode")));
			this.label42.Location = ((System.Drawing.Point)(resources.GetObject("label42.Location")));
			this.label42.Name = "label42";
			this.label42.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label42.RightToLeft")));
			this.label42.Size = ((System.Drawing.Size)(resources.GetObject("label42.Size")));
			this.label42.TabIndex = ((int)(resources.GetObject("label42.TabIndex")));
			this.label42.Text = resources.GetString("label42.Text");
			this.label42.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label42.TextAlign")));
			this.label42.Visible = ((bool)(resources.GetObject("label42.Visible")));
			// 
			// button3
			// 
			this.button3.AccessibleDescription = resources.GetString("button3.AccessibleDescription");
			this.button3.AccessibleName = resources.GetString("button3.AccessibleName");
			this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("button3.Anchor")));
			this.button3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button3.BackgroundImage")));
			this.button3.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("button3.Dock")));
			this.button3.Enabled = ((bool)(resources.GetObject("button3.Enabled")));
			this.button3.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("button3.FlatStyle")));
			this.button3.Font = ((System.Drawing.Font)(resources.GetObject("button3.Font")));
			this.button3.Image = ((System.Drawing.Image)(resources.GetObject("button3.Image")));
			this.button3.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("button3.ImageAlign")));
			this.button3.ImageIndex = ((int)(resources.GetObject("button3.ImageIndex")));
			this.button3.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("button3.ImeMode")));
			this.button3.Location = ((System.Drawing.Point)(resources.GetObject("button3.Location")));
			this.button3.Name = "button3";
			this.button3.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("button3.RightToLeft")));
			this.button3.Size = ((System.Drawing.Size)(resources.GetObject("button3.Size")));
			this.button3.TabIndex = ((int)(resources.GetObject("button3.TabIndex")));
			this.button3.Text = resources.GetString("button3.Text");
			this.button3.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("button3.TextAlign")));
			this.button3.Visible = ((bool)(resources.GetObject("button3.Visible")));
			this.button3.Click += new System.EventHandler(this.GlobCommit);
			// 
			// panel6
			// 
			this.panel6.AccessibleDescription = resources.GetString("panel6.AccessibleDescription");
			this.panel6.AccessibleName = resources.GetString("panel6.AccessibleName");
			this.panel6.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("panel6.Anchor")));
			this.panel6.AutoScroll = ((bool)(resources.GetObject("panel6.AutoScroll")));
			this.panel6.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("panel6.AutoScrollMargin")));
			this.panel6.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("panel6.AutoScrollMinSize")));
			this.panel6.BackColor = System.Drawing.SystemColors.AppWorkspace;
			this.panel6.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel6.BackgroundImage")));
			this.panel6.Controls.Add(this.lbglobfile);
			this.panel6.Controls.Add(this.label46);
			this.panel6.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("panel6.Dock")));
			this.panel6.Enabled = ((bool)(resources.GetObject("panel6.Enabled")));
			this.panel6.Font = ((System.Drawing.Font)(resources.GetObject("panel6.Font")));
			this.panel6.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.panel6.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("panel6.ImeMode")));
			this.panel6.Location = ((System.Drawing.Point)(resources.GetObject("panel6.Location")));
			this.panel6.Name = "panel6";
			this.panel6.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("panel6.RightToLeft")));
			this.panel6.Size = ((System.Drawing.Size)(resources.GetObject("panel6.Size")));
			this.panel6.TabIndex = ((int)(resources.GetObject("panel6.TabIndex")));
			this.panel6.Text = resources.GetString("panel6.Text");
			this.panel6.Visible = ((bool)(resources.GetObject("panel6.Visible")));
			// 
			// lbglobfile
			// 
			this.lbglobfile.AccessibleDescription = resources.GetString("lbglobfile.AccessibleDescription");
			this.lbglobfile.AccessibleName = resources.GetString("lbglobfile.AccessibleName");
			this.lbglobfile.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbglobfile.Anchor")));
			this.lbglobfile.AutoSize = ((bool)(resources.GetObject("lbglobfile.AutoSize")));
			this.lbglobfile.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbglobfile.Dock")));
			this.lbglobfile.Enabled = ((bool)(resources.GetObject("lbglobfile.Enabled")));
			this.lbglobfile.Font = ((System.Drawing.Font)(resources.GetObject("lbglobfile.Font")));
			this.lbglobfile.Image = ((System.Drawing.Image)(resources.GetObject("lbglobfile.Image")));
			this.lbglobfile.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbglobfile.ImageAlign")));
			this.lbglobfile.ImageIndex = ((int)(resources.GetObject("lbglobfile.ImageIndex")));
			this.lbglobfile.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbglobfile.ImeMode")));
			this.lbglobfile.Location = ((System.Drawing.Point)(resources.GetObject("lbglobfile.Location")));
			this.lbglobfile.Name = "lbglobfile";
			this.lbglobfile.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbglobfile.RightToLeft")));
			this.lbglobfile.Size = ((System.Drawing.Size)(resources.GetObject("lbglobfile.Size")));
			this.lbglobfile.TabIndex = ((int)(resources.GetObject("lbglobfile.TabIndex")));
			this.lbglobfile.Text = resources.GetString("lbglobfile.Text");
			this.lbglobfile.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbglobfile.TextAlign")));
			this.lbglobfile.Visible = ((bool)(resources.GetObject("lbglobfile.Visible")));
			// 
			// label46
			// 
			this.label46.AccessibleDescription = resources.GetString("label46.AccessibleDescription");
			this.label46.AccessibleName = resources.GetString("label46.AccessibleName");
			this.label46.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label46.Anchor")));
			this.label46.AutoSize = ((bool)(resources.GetObject("label46.AutoSize")));
			this.label46.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label46.Dock")));
			this.label46.Enabled = ((bool)(resources.GetObject("label46.Enabled")));
			this.label46.Font = ((System.Drawing.Font)(resources.GetObject("label46.Font")));
			this.label46.Image = ((System.Drawing.Image)(resources.GetObject("label46.Image")));
			this.label46.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label46.ImageAlign")));
			this.label46.ImageIndex = ((int)(resources.GetObject("label46.ImageIndex")));
			this.label46.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label46.ImeMode")));
			this.label46.Location = ((System.Drawing.Point)(resources.GetObject("label46.Location")));
			this.label46.Name = "label46";
			this.label46.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label46.RightToLeft")));
			this.label46.Size = ((System.Drawing.Size)(resources.GetObject("label46.Size")));
			this.label46.TabIndex = ((int)(resources.GetObject("label46.TabIndex")));
			this.label46.Text = resources.GetString("label46.Text");
			this.label46.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label46.TextAlign")));
			this.label46.Visible = ((bool)(resources.GetObject("label46.Visible")));
			// 
			// GlobForm
			// 
			this.AccessibleDescription = resources.GetString("$this.AccessibleDescription");
			this.AccessibleName = resources.GetString("$this.AccessibleName");
			this.AutoScaleBaseSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScaleBaseSize")));
			this.AutoScroll = ((bool)(resources.GetObject("$this.AutoScroll")));
			this.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMargin")));
			this.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMinSize")));
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.ClientSize = ((System.Drawing.Size)(resources.GetObject("$this.ClientSize")));
			this.Controls.Add(this.globPanel);
			this.Enabled = ((bool)(resources.GetObject("$this.Enabled")));
			this.Font = ((System.Drawing.Font)(resources.GetObject("$this.Font")));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("$this.ImeMode")));
			this.Location = ((System.Drawing.Point)(resources.GetObject("$this.Location")));
			this.MaximumSize = ((System.Drawing.Size)(resources.GetObject("$this.MaximumSize")));
			this.MinimumSize = ((System.Drawing.Size)(resources.GetObject("$this.MinimumSize")));
			this.Name = "GlobForm";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.StartPosition = ((System.Windows.Forms.FormStartPosition)(resources.GetObject("$this.StartPosition")));
			this.Text = resources.GetString("$this.Text");
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.globPanel.ResumeLayout(false);
			this.panel6.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		private void GlobCommit(object sender, System.EventArgs e)
		{
			try 
			{
				wrapper.SemiGlobalName = this.cbseminame.Text;
				wrapper.SynchronizeUserData();
				MessageBox.Show(Localization.Manager.GetString("commited"));
			} 
			catch (Exception ex) 
			{
				Helper.ExceptionMessage(Localization.Manager.GetString("errwritingfile"), ex);
			}
		}

		private void SemiGlobalChanged(object sender, System.EventArgs e)
		{
			if (cbseminame.SelectedIndex<0) 
			{
				this.tbgroup.Text = "0xffffffff";
				foreach (Data.SemiGlobalAlias a in cbseminame.Items) 
				{
					if (a.Name.ToLower()==cbseminame.Text.ToLower()) 
					{
						tbgroup.Text = "0x"+Helper.HexString(a.Id);
						break;
					}
				}
				
				return;
			}

			Data.SemiGlobalAlias al = (Data.SemiGlobalAlias)cbseminame.Items[cbseminame.SelectedIndex];
			tbgroup.Text = "0x"+Helper.HexString(al.Id);

			if (cbseminame.Tag == null) 
			{
				try 
				{
					Glob wrp = (Glob)wrapper;
					wrp.SemiGlobalName = this.cbseminame.Text;
					wrapper.Changed = true;
				} 
				catch (Exception ex) 
				{
					Helper.ExceptionMessage("", ex);
				}
			}
		}
		#endregion
	}
}
