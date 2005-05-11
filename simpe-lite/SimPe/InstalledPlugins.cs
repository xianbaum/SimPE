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
			this.btup = new System.Windows.Forms.Button();
			this.btdown = new System.Windows.Forms.Button();
			this.btOK = new System.Windows.Forms.Button();
			this.pluglist = new System.Windows.Forms.CheckedListBox();
			this.SuspendLayout();
			// 
			// btup
			// 
			this.btup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btup.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btup.Location = new System.Drawing.Point(600, 8);
			this.btup.Name = "btup";
			this.btup.TabIndex = 1;
			this.btup.Text = "move up";
			this.btup.Click += new System.EventHandler(this.UpClicked);
			// 
			// btdown
			// 
			this.btdown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btdown.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btdown.Location = new System.Drawing.Point(600, 32);
			this.btdown.Name = "btdown";
			this.btdown.TabIndex = 2;
			this.btdown.Text = "move down";
			this.btdown.Click += new System.EventHandler(this.DownClicked);
			// 
			// btOK
			// 
			this.btOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btOK.Location = new System.Drawing.Point(600, 236);
			this.btOK.Name = "btOK";
			this.btOK.TabIndex = 3;
			this.btOK.Text = "OK";
			this.btOK.Click += new System.EventHandler(this.OKClicked);
			// 
			// pluglist
			// 
			this.pluglist.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.pluglist.IntegralHeight = false;
			this.pluglist.Location = new System.Drawing.Point(8, 8);
			this.pluglist.Name = "pluglist";
			this.pluglist.Size = new System.Drawing.Size(584, 248);
			this.pluglist.TabIndex = 4;
			// 
			// InstalledPlugins
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(680, 266);
			this.Controls.Add(this.pluglist);
			this.Controls.Add(this.btOK);
			this.Controls.Add(this.btdown);
			this.Controls.Add(this.btup);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Name = "InstalledPlugins";
			this.Text = "Installed Plugins";
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
