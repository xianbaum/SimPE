/***************************************************************************
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

namespace SimPe
{
	/// <summary>
	/// Zusammenfassung für InstalledPlugins.
	/// </summary>
	public class InstalledPlugins : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button btup;
		private System.Windows.Forms.Button btdown;
		private System.Windows.Forms.Button btOK;
		private System.Windows.Forms.CheckedListBox pluglist;
		/// <summary>
		/// Erforderliche Designervariable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public InstalledPlugins()
		{
			//
			// Erforderlich für die Windows Form-Designerunterstützung
			//
			InitializeComponent();

			//
			// TODO: Fügen Sie den Konstruktorcode nach dem Aufruf von InitializeComponent hinzu
			//
		}

		/// <summary>
		/// Die verwendeten Ressourcen bereinigen.
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

		#region Vom Windows Form-Designer generierter Code
		/// <summary>
		/// Erforderliche Methode für die Designerunterstützung. 
		/// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(InstalledPlugins));
			this.btup = new System.Windows.Forms.Button();
			this.btdown = new System.Windows.Forms.Button();
			this.btOK = new System.Windows.Forms.Button();
			this.pluglist = new System.Windows.Forms.CheckedListBox();
			this.SuspendLayout();
			// 
			// btup
			// 
			this.btup.AccessibleDescription = resources.GetString("btup.AccessibleDescription");
			this.btup.AccessibleName = resources.GetString("btup.AccessibleName");
			this.btup.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btup.Anchor")));
			this.btup.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btup.BackgroundImage")));
			this.btup.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btup.Dock")));
			this.btup.Enabled = ((bool)(resources.GetObject("btup.Enabled")));
			this.btup.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btup.FlatStyle")));
			this.btup.Font = ((System.Drawing.Font)(resources.GetObject("btup.Font")));
			this.btup.Image = ((System.Drawing.Image)(resources.GetObject("btup.Image")));
			this.btup.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btup.ImageAlign")));
			this.btup.ImageIndex = ((int)(resources.GetObject("btup.ImageIndex")));
			this.btup.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btup.ImeMode")));
			this.btup.Location = ((System.Drawing.Point)(resources.GetObject("btup.Location")));
			this.btup.Name = "btup";
			this.btup.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btup.RightToLeft")));
			this.btup.Size = ((System.Drawing.Size)(resources.GetObject("btup.Size")));
			this.btup.TabIndex = ((int)(resources.GetObject("btup.TabIndex")));
			this.btup.Text = resources.GetString("btup.Text");
			this.btup.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btup.TextAlign")));
			this.btup.Visible = ((bool)(resources.GetObject("btup.Visible")));
			this.btup.Click += new System.EventHandler(this.UpClicked);
			// 
			// btdown
			// 
			this.btdown.AccessibleDescription = resources.GetString("btdown.AccessibleDescription");
			this.btdown.AccessibleName = resources.GetString("btdown.AccessibleName");
			this.btdown.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btdown.Anchor")));
			this.btdown.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btdown.BackgroundImage")));
			this.btdown.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btdown.Dock")));
			this.btdown.Enabled = ((bool)(resources.GetObject("btdown.Enabled")));
			this.btdown.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btdown.FlatStyle")));
			this.btdown.Font = ((System.Drawing.Font)(resources.GetObject("btdown.Font")));
			this.btdown.Image = ((System.Drawing.Image)(resources.GetObject("btdown.Image")));
			this.btdown.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btdown.ImageAlign")));
			this.btdown.ImageIndex = ((int)(resources.GetObject("btdown.ImageIndex")));
			this.btdown.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btdown.ImeMode")));
			this.btdown.Location = ((System.Drawing.Point)(resources.GetObject("btdown.Location")));
			this.btdown.Name = "btdown";
			this.btdown.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btdown.RightToLeft")));
			this.btdown.Size = ((System.Drawing.Size)(resources.GetObject("btdown.Size")));
			this.btdown.TabIndex = ((int)(resources.GetObject("btdown.TabIndex")));
			this.btdown.Text = resources.GetString("btdown.Text");
			this.btdown.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btdown.TextAlign")));
			this.btdown.Visible = ((bool)(resources.GetObject("btdown.Visible")));
			this.btdown.Click += new System.EventHandler(this.DownClicked);
			// 
			// btOK
			// 
			this.btOK.AccessibleDescription = resources.GetString("btOK.AccessibleDescription");
			this.btOK.AccessibleName = resources.GetString("btOK.AccessibleName");
			this.btOK.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btOK.Anchor")));
			this.btOK.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btOK.BackgroundImage")));
			this.btOK.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btOK.Dock")));
			this.btOK.Enabled = ((bool)(resources.GetObject("btOK.Enabled")));
			this.btOK.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btOK.FlatStyle")));
			this.btOK.Font = ((System.Drawing.Font)(resources.GetObject("btOK.Font")));
			this.btOK.Image = ((System.Drawing.Image)(resources.GetObject("btOK.Image")));
			this.btOK.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btOK.ImageAlign")));
			this.btOK.ImageIndex = ((int)(resources.GetObject("btOK.ImageIndex")));
			this.btOK.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btOK.ImeMode")));
			this.btOK.Location = ((System.Drawing.Point)(resources.GetObject("btOK.Location")));
			this.btOK.Name = "btOK";
			this.btOK.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btOK.RightToLeft")));
			this.btOK.Size = ((System.Drawing.Size)(resources.GetObject("btOK.Size")));
			this.btOK.TabIndex = ((int)(resources.GetObject("btOK.TabIndex")));
			this.btOK.Text = resources.GetString("btOK.Text");
			this.btOK.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btOK.TextAlign")));
			this.btOK.Visible = ((bool)(resources.GetObject("btOK.Visible")));
			this.btOK.Click += new System.EventHandler(this.OKClicked);
			// 
			// pluglist
			// 
			this.pluglist.AccessibleDescription = resources.GetString("pluglist.AccessibleDescription");
			this.pluglist.AccessibleName = resources.GetString("pluglist.AccessibleName");
			this.pluglist.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("pluglist.Anchor")));
			this.pluglist.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pluglist.BackgroundImage")));
			this.pluglist.ColumnWidth = ((int)(resources.GetObject("pluglist.ColumnWidth")));
			this.pluglist.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("pluglist.Dock")));
			this.pluglist.Enabled = ((bool)(resources.GetObject("pluglist.Enabled")));
			this.pluglist.Font = ((System.Drawing.Font)(resources.GetObject("pluglist.Font")));
			this.pluglist.HorizontalExtent = ((int)(resources.GetObject("pluglist.HorizontalExtent")));
			this.pluglist.HorizontalScrollbar = ((bool)(resources.GetObject("pluglist.HorizontalScrollbar")));
			this.pluglist.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("pluglist.ImeMode")));
			this.pluglist.IntegralHeight = ((bool)(resources.GetObject("pluglist.IntegralHeight")));
			this.pluglist.Location = ((System.Drawing.Point)(resources.GetObject("pluglist.Location")));
			this.pluglist.Name = "pluglist";
			this.pluglist.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("pluglist.RightToLeft")));
			this.pluglist.ScrollAlwaysVisible = ((bool)(resources.GetObject("pluglist.ScrollAlwaysVisible")));
			this.pluglist.Size = ((System.Drawing.Size)(resources.GetObject("pluglist.Size")));
			this.pluglist.TabIndex = ((int)(resources.GetObject("pluglist.TabIndex")));
			this.pluglist.Visible = ((bool)(resources.GetObject("pluglist.Visible")));
			// 
			// InstalledPlugins
			// 
			this.AccessibleDescription = resources.GetString("$this.AccessibleDescription");
			this.AccessibleName = resources.GetString("$this.AccessibleName");
			this.AutoScaleBaseSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScaleBaseSize")));
			this.AutoScroll = ((bool)(resources.GetObject("$this.AutoScroll")));
			this.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMargin")));
			this.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMinSize")));
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.ClientSize = ((System.Drawing.Size)(resources.GetObject("$this.ClientSize")));
			this.Controls.Add(this.pluglist);
			this.Controls.Add(this.btOK);
			this.Controls.Add(this.btdown);
			this.Controls.Add(this.btup);
			this.Enabled = ((bool)(resources.GetObject("$this.Enabled")));
			this.Font = ((System.Drawing.Font)(resources.GetObject("$this.Font")));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("$this.ImeMode")));
			this.Location = ((System.Drawing.Point)(resources.GetObject("$this.Location")));
			this.MaximumSize = ((System.Drawing.Size)(resources.GetObject("$this.MaximumSize")));
			this.MinimumSize = ((System.Drawing.Size)(resources.GetObject("$this.MinimumSize")));
			this.Name = "InstalledPlugins";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.StartPosition = ((System.Windows.Forms.FormStartPosition)(resources.GetObject("$this.StartPosition")));
			this.Text = resources.GetString("$this.Text");
			this.Load += new System.EventHandler(this.InstalledPlugins_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void InstalledPlugins_Load(object sender, System.EventArgs e)
		{
		
		}

		public void Execute(SimPe.Interfaces.IWrapperRegistry reg)
		{
			pluglist.Items.Clear();
			foreach (SimPe.Interfaces.IWrapper w in reg.AllWrappers) 
			{				
				pluglist.Items.Add(w, (w.Priority>=0));
			}
			this.ShowDialog();
		}

		private void OKClicked(object sender, System.EventArgs e)
		{
			SimPe.Registry reg = new Registry();
			//change priority
			for (int i=0; i< pluglist.Items.Count; i++)
			{
				SimPe.Interfaces.IWrapper w = (SimPe.Interfaces.IWrapper)pluglist.Items[i];
				w.Priority = i+1;	
				if (!pluglist.GetItemChecked(i)) w.Priority = (-1 * i);
				reg.SetWrapperPriority(w.WrapperDescription.UID, w.Priority);
			}
			Close();
		}

		private void UpClicked(object sender, System.EventArgs e)
		{
			if (pluglist.SelectedIndex < 1) return;
			SimPe.Interfaces.IWrapper dum = (SimPe.Interfaces.IWrapper)pluglist.Items[pluglist.SelectedIndex];
			pluglist.Items[pluglist.SelectedIndex] = pluglist.Items[pluglist.SelectedIndex-1];
			pluglist.Items[pluglist.SelectedIndex-1] = dum;

			bool dumb = pluglist.GetItemChecked(pluglist.SelectedIndex);
			pluglist.SetItemChecked(pluglist.SelectedIndex, pluglist.GetItemChecked(pluglist.SelectedIndex-1));
			pluglist.SetItemChecked(pluglist.SelectedIndex-1, dumb);

			pluglist.SelectedIndex--;
		}

		private void DownClicked(object sender, System.EventArgs e)
		{
			if (pluglist.SelectedIndex < 0) return;
			if (pluglist.SelectedIndex >= pluglist.Items.Count-1) return;

			SimPe.Interfaces.IWrapper dum = (SimPe.Interfaces.IWrapper)pluglist.Items[pluglist.SelectedIndex];
			pluglist.Items[pluglist.SelectedIndex] = pluglist.Items[pluglist.SelectedIndex+1];
			pluglist.Items[pluglist.SelectedIndex+1] = dum;

			bool dumb = pluglist.GetItemChecked(pluglist.SelectedIndex);
			pluglist.SetItemChecked(pluglist.SelectedIndex, pluglist.GetItemChecked(pluglist.SelectedIndex+1));
			pluglist.SetItemChecked(pluglist.SelectedIndex+1, dumb);

			pluglist.SelectedIndex++;
		}
	}
}
