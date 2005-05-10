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
	/// Zusammenfassung für SimListing.
	/// </summary>
	public class SimListing : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListBox list;
		/// <summary>
		/// Erforderliche Designervariable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public SimListing()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(SimListing));
			this.list = new System.Windows.Forms.ListBox();
			this.SuspendLayout();
			// 
			// list
			// 
			this.list.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.list.Location = new System.Drawing.Point(8, 8);
			this.list.Name = "list";
			this.list.Size = new System.Drawing.Size(272, 251);
			this.list.TabIndex = 0;
			// 
			// SimListing
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(292, 266);
			this.Controls.Add(this.list);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "SimListing";
			this.Text = "Listing";
			this.Load += new System.EventHandler(this.SimListing_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void SimListing_Load(object sender, System.EventArgs e)
		{
		
		}

		public void Execute (SimPe.Interfaces.Providers.ISimNames names) 
		{
			list.Items.Clear();
			list.Sorted = false;
			foreach (SimPe.Interfaces.IAlias a in names.StoredData.Values) 
			{
				object[] o = (object[])a.Tag;
				string file = Localization.Manager.GetString("nofilefound");
				if (o.Length>=1) 
				{
					file = o[0].ToString();
				}
				list.Items.Add(a.ToString()+" - "+file);	
			}
			list.Sorted = true;

			this.Show();
		}

		public void ExecuteMem (SimPe.Interfaces.Providers.IOpcodeProvider names) 
		{
			list.Items.Clear();
			list.Sorted = false;

			foreach (SimPe.Interfaces.IAlias a in names.StoredMemories.Values) 
			{				
				object[] o = (object[])a.Tag;
				Interfaces.Files.IPackedFileDescriptor pfd = null;
				if (o.Length>=1) pfd = (Interfaces.Files.IPackedFileDescriptor)o[0];
				
				list.Items.Add(a.ToString());
			}

			list.Sorted = true;

			this.Show();
		}
	}
}
