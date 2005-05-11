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
	/// Zusammenfassung für CompareForm.
	/// </summary>
	public class CompareForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListBox lb;
		/// <summary>
		/// Erforderliche Designervariable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public CompareForm()
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
			this.lb = new System.Windows.Forms.ListBox();
			this.SuspendLayout();
			// 
			// lb
			// 
			this.lb.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lb.Location = new System.Drawing.Point(8, 8);
			this.lb.Name = "lb";
			this.lb.Size = new System.Drawing.Size(536, 251);
			this.lb.TabIndex = 0;
			// 
			// CompareForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(552, 266);
			this.Controls.Add(this.lb);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Name = "CompareForm";
			this.Text = "CompareForm";
			this.ResumeLayout(false);

		}
		#endregion

		protected void ScanDirectory(string folder1, string folder2)
		{
			folder1 = folder1.ToLower();
			folder2 = folder2.ToLower();

			string[] files = System.IO.Directory.GetFiles(folder1);
			foreach (string file in files) 
			{
				string file1 = file.ToLower();
				string file2 = file1.Replace(folder1, folder2);

				if (System.IO.File.Exists(file2))
				{
					System.IO.BinaryReader br1 = new System.IO.BinaryReader(new System.IO.FileStream(file1, System.IO.FileMode.Open));
					try 
					{
						System.IO.BinaryReader br2 = new System.IO.BinaryReader(new System.IO.FileStream(file2, System.IO.FileMode.Open));
						try 
						{
							if (br1.BaseStream.Length != br2.BaseStream.Length) 
							{
								lb.Items.Add(file1 + " and "+ file2 + " have diffrent sizes.");
							} 
							else 
							{
								for (int i=0; i<Math.Min(br1.BaseStream.Length, br2.BaseStream.Length); i++) 
								{
									if (br1.ReadByte()!=br2.ReadByte()) 
									{
										lb.Items.Add(file1 + " and "+ file2 + " have diffrent content.");
										break;
									}
								}
							}
						} 
						finally 
						{
							br2.Close();
						}
					} 
					finally 
					{
						br1.Close();
					}
				} 
				else 
				{
					lb.Items.Add(file2+" not found");
				}
			}

			//work subdirectories
			string[] dirs = System.IO.Directory.GetDirectories(folder1);	
			foreach(string dir in dirs) 
			{
				string dir1 = dir.ToLower();
				string dir2 = dir1.Replace(folder1, folder2);

				if (System.IO.Directory.Exists(dir2))
				{
					ScanDirectory(dir1, dir2);
				} 
				else 
				{
					lb.Items.Add("Folder "+dir2 + " does not exits.");
				}
			}
		}

		public void Execute(string folder1, string folder2) 
		{			
			lb.Items.Clear();

			lb.Items.Add("Folder 1 is "+folder1);
			lb.Items.Add("Folder 2 is "+folder2);
			
			Cursor = Cursors.WaitCursor;
			ScanDirectory(folder1, folder2);	
			Cursor = Cursors.Default;

			this.Show();
		}
	}
}
