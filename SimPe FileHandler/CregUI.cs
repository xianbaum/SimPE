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
 ***************************************************************************/
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using SimPe.Interfaces.Plugin;
using SimPe.Interfaces;
using SimPe.Windows.Forms;
using SimPe.PackedFiles.Wrapper;

namespace SimPe.PackedFiles.UserInterface
{
	/// <summary>
	/// UI handler for the Content Registry (Creg) wrapper.
	/// </summary>
	public class CregUI :
		SimPe.Windows.Forms.WrapperBaseControl, SimPe.Interfaces.Plugin.IPackedFileUI
	{
		private SimPe.PackedFiles.Wrapper.Creg3UI creg3;
		private System.ComponentModel.Container components = null;

		public CregUI()
		{
			SetStyle(
				ControlStyles.SupportsTransparentBackColor |
				ControlStyles.AllPaintingInWmPaint |
				ControlStyles.UserPaint |
				ControlStyles.ResizeRedraw
				| ControlStyles.DoubleBuffer
				,true);

			InitializeComponent();
		}

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

		#region Component Designer generated code
		// Hand-written layout: the original loaded every property from a .resx via
		// ResourceManager(typeof(CregUI)). That values were purely cosmetic and the
		// resx manifest name would not line up with this project's RootNamespace on
		// .NET (MissingManifestResourceException), so the host is configured in code.
		private void InitializeComponent()
		{
			this.creg3 = new SimPe.PackedFiles.Wrapper.Creg3UI();
			this.SuspendLayout();
			//
			// creg3
			//
			this.creg3.Creg = null;
			this.creg3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.creg3.Name = "creg3";
			//
			// CregUI
			//
			this.Controls.Add(this.creg3);
			this.Padding = new System.Windows.Forms.Padding(0, 24, 0, 0);
			this.HeaderText = "Content Registry";
			this.Name = "CregUI";
			this.Size = new System.Drawing.Size(408, 128);
			this.Controls.SetChildIndex(this.creg3, 0);
			this.ResumeLayout(false);
		}
		#endregion

		public Creg Creg
		{
			get { return (Creg)Wrapper; }
		}

		protected override void RefreshGUI()
		{
			this.creg3.Visible = false;
			if (Creg!=null)
			{
				this.creg3.Visible = Creg.Group3!=null;
				creg3.Creg = Creg.Group3;

				this.Enabled = (creg3.Creg != null);
			}
		}

		public override void OnCommit()
		{
			Creg.SynchronizeUserData(true, false);
		}
	}
}
