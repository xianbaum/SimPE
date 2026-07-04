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
using SimPe.Interfaces.Plugin;

namespace SimPe.Plugin
{
	/// <summary>
	/// Summary description for MyPackedFileForm.
	/// </summary>
	public class RefFileForm : System.Windows.Forms.Form
	{
        internal booby.gradientpanel wrapperPanel;
        private booby.panelheader panel3;
		internal System.Windows.Forms.ListBox lblist;
        private booby.TaskBox gbtypes;
		internal System.Windows.Forms.TextBox tbsubtype;
		internal System.Windows.Forms.TextBox tbinstance;
		private System.Windows.Forms.Label label11;
		internal System.Windows.Forms.TextBox tbtype;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
		internal System.Windows.Forms.TextBox tbgroup;
		internal System.Windows.Forms.ComboBox cbtypes;
		internal System.Windows.Forms.LinkLabel lldelete;
        internal System.Windows.Forms.LinkLabel lladd;
		internal System.Windows.Forms.Button btup;
		internal System.Windows.Forms.Button btdown;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.Button button2;
        internal System.Windows.Forms.PictureBox pb;
		private System.Windows.Forms.ContextMenu contextMenu1;
		private System.Windows.Forms.MenuItem miAdd;
		internal System.Windows.Forms.MenuItem miRem;
		private System.ComponentModel.IContainer components;
        internal System.Drawing.Image imge;

		public RefFileForm()
		{
			components = null;
			//
			// Required designer variable.
			//
            InitializeComponent();
            if (Helper.WindowsRegistry.UseBigIcons) this.lblist.Font = new System.Drawing.Font(this.lblist.Font.FontFamily, 11F);

            if (booby.ThemeManager.ThemedForms)
            {
                booby.ThemeManager tm = booby.ThemeManager.Global.CreateChild();
                tm.AddControl(this.wrapperPanel);
                tm.AddControl(this.gbtypes);
                tm.AddControl(this.lblist);
                tm.AddControl(this.btup);
                tm.AddControl(this.btdown);
                tm.AddControl(this.cbtypes);
            }
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RefFileForm));
            this.wrapperPanel = new booby.gradientpanel();
            this.pb = new System.Windows.Forms.PictureBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.btdown = new System.Windows.Forms.Button();
            this.btup = new System.Windows.Forms.Button();
            this.gbtypes = new booby.TaskBox();
            this.lladd = new System.Windows.Forms.LinkLabel();
            this.lldelete = new System.Windows.Forms.LinkLabel();
            this.tbsubtype = new System.Windows.Forms.TextBox();
            this.tbinstance = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.tbtype = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.tbgroup = new System.Windows.Forms.TextBox();
            this.cbtypes = new System.Windows.Forms.ComboBox();
            this.lblist = new System.Windows.Forms.ListBox();
            this.contextMenu1 = new System.Windows.Forms.ContextMenu();
            this.miAdd = new System.Windows.Forms.MenuItem();
            this.miRem = new System.Windows.Forms.MenuItem();
            this.panel3 = new booby.panelheader();
            this.wrapperPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb)).BeginInit();
            this.gbtypes.SuspendLayout();
            this.SuspendLayout();
            // 
            // wrapperPanel
            // 
            resources.ApplyResources(this.wrapperPanel, "wrapperPanel");
            this.wrapperPanel.Controls.Add(this.pb);
            this.wrapperPanel.Controls.Add(this.btdown);
            this.wrapperPanel.Controls.Add(this.btup);
            this.wrapperPanel.Controls.Add(this.gbtypes);
            this.wrapperPanel.Controls.Add(this.lblist);
            this.wrapperPanel.Controls.Add(this.panel3);
            this.wrapperPanel.Name = "wrapperPanel";
            // 
            // pb
            // 
            resources.ApplyResources(this.pb, "pb");
            this.pb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            //this.pb.MaximumSize = new System.Drawing.Size(436, 436);
            this.pb.Name = "pb";
            this.pb.TabStop = false;
            this.pb.SizeChanged += new System.EventHandler(this.pb_SizeChanged);
            // 
            // button2
            // 
            resources.ApplyResources(this.button2, "button2");
            this.button2.Name = "button2";
            this.button2.Click += new System.EventHandler(this.ShowPackageSelector);
            // 
            // button4
            // 
            resources.ApplyResources(this.button4, "button4");
            this.button4.Name = "button4";
            this.button4.Click += new System.EventHandler(this.ChooseFile);
            // 
            // btdown
            // 
            resources.ApplyResources(this.btdown, "btdown");
            this.btdown.Name = "btdown";
            this.btdown.Click += new System.EventHandler(this.MoveDown);
            // 
            // btup
            // 
            resources.ApplyResources(this.btup, "btup");
            this.btup.Name = "btup";
            this.btup.Click += new System.EventHandler(this.MoveUp);
            // 
            // gbtypes
            // 
            resources.ApplyResources(this.gbtypes, "gbtypes");
            this.gbtypes.Controls.Add(this.lladd);
            this.gbtypes.Controls.Add(this.button2);
            this.gbtypes.Controls.Add(this.button4);
            this.gbtypes.Controls.Add(this.cbtypes);
            this.gbtypes.Controls.Add(this.lldelete);
            this.gbtypes.Controls.Add(this.tbsubtype);
            this.gbtypes.Controls.Add(this.tbgroup);
            this.gbtypes.Controls.Add(this.tbinstance);
            this.gbtypes.Controls.Add(this.label10);
            this.gbtypes.Controls.Add(this.label11);
            this.gbtypes.Controls.Add(this.label9);
            this.gbtypes.Controls.Add(this.tbtype);
            this.gbtypes.Controls.Add(this.label8);
            this.gbtypes.IconLocation = new System.Drawing.Point(4, 12);
            this.gbtypes.IconSize = new System.Drawing.Size(32, 32);
            this.gbtypes.Name = "gbtypes";
            this.gbtypes.TopGap = 2;
            // 
            // lladd
            // 
            resources.ApplyResources(this.lladd, "lladd");
            this.lladd.Name = "lladd";
            this.lladd.TabStop = true;
            this.lladd.UseCompatibleTextRendering = true;
            this.lladd.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.AddFile);
            // 
            // lldelete
            // 
            resources.ApplyResources(this.lldelete, "lldelete");
            this.lldelete.Name = "lldelete";
            this.lldelete.TabStop = true;
            this.lldelete.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.DeleteFile);
            // 
            // tbsubtype
            // 
            resources.ApplyResources(this.tbsubtype, "tbsubtype");
            this.tbsubtype.Name = "tbsubtype";
            this.tbsubtype.TextChanged += new System.EventHandler(this.AutoChange);
            // 
            // tbinstance
            // 
            resources.ApplyResources(this.tbinstance, "tbinstance");
            this.tbinstance.Name = "tbinstance";
            this.tbinstance.TextChanged += new System.EventHandler(this.AutoChange);
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            // 
            // tbtype
            // 
            resources.ApplyResources(this.tbtype, "tbtype");
            this.tbtype.Name = "tbtype";
            this.tbtype.TextChanged += new System.EventHandler(this.tbtype_TextChanged);
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // tbgroup
            // 
            resources.ApplyResources(this.tbgroup, "tbgroup");
            this.tbgroup.Name = "tbgroup";
            this.tbgroup.TextChanged += new System.EventHandler(this.AutoChange);
            // 
            // cbtypes
            // 
            this.cbtypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cbtypes, "cbtypes");
            this.cbtypes.Name = "cbtypes";
            this.cbtypes.Sorted = true;
            this.cbtypes.SelectedIndexChanged += new System.EventHandler(this.SelectType);
            // 
            // lblist
            // 
            this.lblist.AllowDrop = true;
            resources.ApplyResources(this.lblist, "lblist");
            this.lblist.ContextMenu = this.contextMenu1;
            this.lblist.Name = "lblist";
            this.lblist.SelectedIndexChanged += new System.EventHandler(this.SelectFile);
            this.lblist.DragDrop += new System.Windows.Forms.DragEventHandler(this.PackageItemDrop);
            this.lblist.DragEnter += new System.Windows.Forms.DragEventHandler(this.PackageItemDragEnter);
            // 
            // contextMenu1
            // 
            this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.miAdd,
            this.miRem});
            // 
            // miAdd
            // 
            this.miAdd.Index = 0;
            resources.ApplyResources(this.miAdd, "miAdd");
            this.miAdd.Click += new System.EventHandler(this.miAdd_Click);
            // 
            // miRem
            // 
            this.miRem.Index = 1;
            resources.ApplyResources(this.miRem, "miRem");
            this.miRem.Click += new System.EventHandler(this.menuItem1_Click);
            // 
            // panel3
            // 
            resources.ApplyResources(this.panel3, "panel3");
            this.panel3.CanCommit = true;
            this.panel3.Name = "panel3";
            this.panel3.OnCommit += new booby.panelheader.EventHandler(this.CommitAll);
            // 
            // RefFileForm
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.wrapperPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "RefFileForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.wrapperPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pb)).EndInit();
            this.gbtypes.ResumeLayout(false);
            this.gbtypes.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

		
		/// <summary>
		/// Stores the currently active Wrapper
		/// </summary>
		internal IFileWrapperSaveExtension wrapper = null;

		private void SelectType(object sender, System.EventArgs e)
		{
			if (cbtypes.Tag != null) return;
			tbtype.Text = "0x"+Helper.HexString(((SimPe.Data.TypeAlias)cbtypes.Items[cbtypes.SelectedIndex]).Id);
		}

		private void tbtype_TextChanged(object sender, System.EventArgs e)
		{
			cbtypes.Tag = true;
			Data.TypeAlias a = Data.MetaData.FindTypeAlias(Helper.HexStringToUInt(tbtype.Text));
			this.AutoChange(sender, e);
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

		private void SelectFile(object sender, System.EventArgs e)
		{
            if (lblist.SelectedIndex < 0) { lldelete.Enabled = btup.Enabled = btdown.Enabled = miAdd.Enabled = miRem.Enabled = false; return; }
            lldelete.Enabled = btup.Enabled = btdown.Enabled = miAdd.Enabled = miRem.Enabled = true;

			if (tbtype.Tag!=null) return;
			try 
			{
				tbtype.Tag = true;
				Interfaces.Files.IPackedFileDescriptor pfd = (Interfaces.Files.IPackedFileDescriptor)lblist.Items[lblist.SelectedIndex];
				this.tbgroup.Text = "0x"+Helper.HexString(pfd.Group);
				this.tbinstance.Text = "0x"+Helper.HexString(pfd.Instance);
				this.tbsubtype.Text = "0x"+Helper.HexString(pfd.SubType);
				this.tbtype.Text = "0x"+Helper.HexString(pfd.Type);

				//get Texture
				if (pfd.GetType()==typeof(RefFileItem)) 
				{
					RefFile wrp = (RefFile)wrapper;
					SkinChain sc = ((RefFileItem)pfd).Skin;
					SimPe.Plugin.GenericRcol txtr = null;
					if (sc!=null) txtr = sc.TXTR;
					
					//show the Image
					if (txtr==null) 
					{
                        pb.Image = imge;
					} 
					else 
					{
						MipMap mm = ((ImageData)txtr.Blocks[0]).GetLargestTexture(pb.Size);
						if (mm!=null) pb.Image = mm.Texture;
                        else pb.Image = imge;
					}
				} 
				else 
				{
                    pb.Image = imge;
				}
			} 
			catch (Exception ex) 
			{
				Helper.ExceptionMessage(Localization.Manager.GetString("errconvert"), ex);
			} 
			finally 
			{
				tbtype.Tag = null;
			}
		}

		private void ChangeFile(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			try 
			{
				Packages.PackedFileDescriptor pfd = null;
				if (lblist.SelectedIndex>=0) pfd = (Packages.PackedFileDescriptor)lblist.Items[lblist.SelectedIndex];
				else pfd = new Packages.PackedFileDescriptor();

				pfd.Group = Convert.ToUInt32(this.tbgroup.Text, 16);
				pfd.Instance = Convert.ToUInt32(this.tbinstance.Text, 16);
				pfd.SubType = Convert.ToUInt32(this.tbsubtype.Text, 16);
				pfd.Type = Convert.ToUInt32(this.tbtype.Text, 16);

				if (lblist.SelectedIndex>=0) 
				{
					lblist.Items[lblist.SelectedIndex] = pfd;
					try 
					{
						RefFileItem rfi = (RefFileItem)pfd;
						rfi.Skin = null;
					} 
					catch {}
				}
				else lblist.Items.Add(pfd);
			} 
			catch (Exception ex) 
			{
				Helper.ExceptionMessage(Localization.Manager.GetString("errconvert"), ex);
            }
		}

		private void DeleteFile(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			lldelete.Enabled = false;
			btup.Enabled = false;
			btdown.Enabled = false;
			miRem.Enabled = lldelete.Enabled;
			if (lblist.SelectedIndex<0) return;
			lldelete.Enabled = true;
			btup.Enabled = true;
			btdown.Enabled = true;
			miRem.Enabled = lldelete.Enabled;
			//lblist.Items.Remove(lblist.Items[lblist.SelectedIndex]);
            lblist.Items.RemoveAt(lblist.SelectedIndex);
            SetChange();
		}

		private void AddFile(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			lblist.SelectedIndex = -1;
			ChangeFile(null, null);
            lblist.SelectedIndex = lblist.Items.Count - 1;
            SetChange();
		}

		private void CommitAll(object sender, System.EventArgs e)
		{
			try 
			{
				RefFile wrp = (RefFile)wrapper;				

				Interfaces.Files.IPackedFileDescriptor[] pfds = new Interfaces.Files.IPackedFileDescriptor[lblist.Items.Count];
				for (int i=0; i<pfds.Length; i++) 
				{
					pfds[i] = (Interfaces.Files.IPackedFileDescriptor)lblist.Items[i];
				}

				wrp.Items = pfds;
				wrapper.SynchronizeUserData();
				MessageBox.Show(Localization.Manager.GetString("commited"));
			} 
			catch (Exception ex) 
			{
				Helper.ExceptionMessage(Localization.Manager.GetString("errwritingfile"), ex);
			}	
		}

		private void MoveUp(object sender, System.EventArgs e)
		{
			if (lblist.SelectedIndex<1) return;
			
			Interfaces.Files.IPackedFileDescriptor pfd = (Interfaces.Files.IPackedFileDescriptor)lblist.Items[lblist.SelectedIndex];
			lblist.Items[lblist.SelectedIndex] = lblist.Items[lblist.SelectedIndex-1];
			lblist.Items[lblist.SelectedIndex-1] = pfd;
            lblist.SelectedIndex--;
            SetChange();
		}

		private void MoveDown(object sender, System.EventArgs e)
		{
			if (lblist.SelectedIndex<0) return;
			if (lblist.SelectedIndex>lblist.Items.Count-2) return;
			
			Interfaces.Files.IPackedFileDescriptor pfd = (Interfaces.Files.IPackedFileDescriptor)lblist.Items[lblist.SelectedIndex];
			lblist.Items[lblist.SelectedIndex] = lblist.Items[lblist.SelectedIndex+1];
			lblist.Items[lblist.SelectedIndex+1] = pfd;
            lblist.SelectedIndex++;
            SetChange();
		}

		private void AutoChange(object sender, System.EventArgs e)
		{
			if (tbtype.Tag != null) return;

			tbtype.Tag = true;
			if (lblist.SelectedIndex>=0) ChangeFile(null, null);
			tbtype.Tag = null;
		}

		private void ChooseFile(object sender, System.EventArgs e)
		{
			try 
			{
				RefFile wrp = (RefFile)wrapper;
                Interfaces.Files.IPackedFileDescriptor pfd = FileSelect.Execute();
				if (pfd!=null) 
				{
					tbtype.Tag = true;
					this.tbgroup.Text = "0x"+Helper.HexString(pfd.Group);
					this.tbinstance.Text = "0x"+Helper.HexString(pfd.Instance);
					this.tbsubtype.Text = "0x"+Helper.HexString(pfd.SubType);
					this.tbtype.Text = "0x"+Helper.HexString(pfd.Type);
					tbtype.Tag = null;
                    this.AutoChange(sender, e);
                    SetChange();
				}
			} 
			catch (Exception) {} 
			finally 
			{
				tbtype.Tag = null;
			}
		}

		#region Package Selector
		private void ShowPackageSelector(object sender, System.EventArgs e)
		{
			SimPe.PackageSelectorForm form = new SimPe.PackageSelectorForm();
			form.Execute(((RefFile)wrapper).Package);
		}

		private void PackageItemDragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(typeof(SimPe.Packages.PackedFileDescriptor))) 
			{
                e.Effect = DragDropEffects.Copy;
                SetChange();
			}
			else 
			{
				e.Effect = DragDropEffects.None;
			}					
		}

		private void PackageItemDrop(object sender, System.Windows.Forms.DragEventArgs e)
		{
			try 
			{
				Interfaces.Files.IPackedFileDescriptor pfd = null;
				pfd = (Interfaces.Files.IPackedFileDescriptor)e.Data.GetData(typeof(SimPe.Packages.PackedFileDescriptor));
                lblist.Items.Add(pfd);
                SetChange();
			}
			catch (Exception ex) 
			{
				Helper.ExceptionMessage("", ex);
			}
		}
		#endregion

		private void pb_SizeChanged(object sender, System.EventArgs e)
		{
            if (pb.Height < 437)
                pb.Width = pb.Height;
            else pb.Width = 436;
        }

		private void miAdd_Click(object sender, System.EventArgs e)
		{
			AddFile(null, null);
		}

		private void menuItem1_Click(object sender, System.EventArgs e)
		{
			DeleteFile(null, null);
		}
        private void SetChange()
        {
            RefFile wrp = (RefFile)wrapper;            
            wrp.Changed = true;
        }
	}
}
