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
	/// Zusammenfassung für ExtObjdForm.
	/// </summary>
	public class ExtObjdForm : System.Windows.Forms.Form, IPackedFileUI
	{
		#region Form variables
		private System.Windows.Forms.Label label63;
		private System.Windows.Forms.TextBox tbproxguid;
		private System.Windows.Forms.Label label97;
		private System.Windows.Forms.TextBox tborgguid;
		private System.Windows.Forms.LinkLabel llgetGUID;
		private System.Windows.Forms.LinkLabel llcommitobjd;
		private System.Windows.Forms.Label label65;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Panel panel6;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.TextBox tbflname;
		private System.Windows.Forms.TextBox tbguid;
		private System.Windows.Forms.ComboBox cbtype;
		private System.Windows.Forms.TextBox tbtype;
		private System.Windows.Forms.CheckBox cbbathroom;
		private System.Windows.Forms.CheckBox cbbedroom;
		private System.Windows.Forms.CheckBox cbdinigroom;
		private System.Windows.Forms.CheckBox cbkitchen;
		private System.Windows.Forms.CheckBox cbstudy;
		private System.Windows.Forms.CheckBox cblivingroom;
		private System.Windows.Forms.CheckBox cboutside;
		private System.Windows.Forms.CheckBox cbmisc;
		private System.Windows.Forms.CheckBox cbgeneral;
		private System.Windows.Forms.CheckBox cbelectronics;
		private System.Windows.Forms.CheckBox cbdecorative;
		private System.Windows.Forms.CheckBox cbappliances;
		private System.Windows.Forms.CheckBox cbsurfaces;
		private System.Windows.Forms.CheckBox cbseating;
		private System.Windows.Forms.CheckBox cbplumbing;
		private System.Windows.Forms.CheckBox cblightning;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.LinkLabel linkLabel1;
		private System.Windows.Forms.PropertyGrid pg;
		private System.Windows.Forms.TabControl tc;
		private System.Windows.Forms.TabPage tpcatalogsort;
		private System.Windows.Forms.TabPage tpraw;
		private System.Windows.Forms.CheckBox cbhobby;
		private System.Windows.Forms.CheckBox cbaspiration;
		private System.Windows.Forms.CheckBox cbcareer;
		private System.Windows.Forms.CheckBox cbkids;
		private System.Windows.Forms.RadioButton rbbin;
		private System.Windows.Forms.RadioButton rbdec;
		private System.Windows.Forms.RadioButton rbhex;
		private System.Windows.Forms.CheckBox cball;
		private System.Windows.Forms.Panel extObjdPanel;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion

		public ExtObjdForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			cbtype.Items.Add(Data.ObjectTypes.Unknown);
			cbtype.Items.Add(Data.ObjectTypes.ArchitecturalSupport);
			cbtype.Items.Add(Data.ObjectTypes.Door);
			cbtype.Items.Add(Data.ObjectTypes.Memory);
			cbtype.Items.Add(Data.ObjectTypes.ModularStairs);
			cbtype.Items.Add(Data.ObjectTypes.ModularStairsPortal);
			cbtype.Items.Add(Data.ObjectTypes.Normal);
			cbtype.Items.Add(Data.ObjectTypes.Outfit);
			cbtype.Items.Add(Data.ObjectTypes.Person);
			cbtype.Items.Add(Data.ObjectTypes.SimType);
			cbtype.Items.Add(Data.ObjectTypes.Stairs);
			cbtype.Items.Add(Data.ObjectTypes.Template);
			cbtype.Items.Add(Data.ObjectTypes.Vehicle);
			cbtype.Items.Add(Data.ObjectTypes.Window);
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


		#region ExtObjdForm
		private ExtObjd wrapper;
		private uint initialguid;
		private Ambertation.PropertyObjectBuilder pob;
		private ArrayList names;
		private bool propchanged;
		private string GetName(int i)
		{
			string name = "0x"+Helper.HexString((ushort)i) + ": ";
			name += (string)names[i];

			return name;
		}

		private void ShowData()
		{
			propchanged = false;
			this.pg.SelectedObject = null;
			
			names = new ArrayList();
			names = wrapper.Opcodes.OBJDDescription((ushort)wrapper.Type);

			Hashtable ht = new Hashtable();
			for (int i=0; i<Math.Min(names.Count, wrapper.Data.Length); i++)
			{
				string name = GetName(i);				
				if (!ht.Contains(name)) ht.Add(name, wrapper.Data[i]);
			}

			pob = new Ambertation.PropertyObjectBuilder(ht);
			this.pg.SelectedObject = pob.Instance;
		}

		private void UpdateData()
		{
			if (!propchanged) return;
			propchanged = false;

			try 
			{
				Hashtable ht = pob.Properties;

				for (int i=0; i<Math.Min(names.Count, wrapper.Data.Length); i++)
				{
					string name = GetName(i);	
					if (ht.Contains(name)) wrapper.Data[i] = (short)ht[name];
				}

				wrapper.Changed = true;
			} 
			catch (Exception ex) 
			{
				Helper.ExceptionMessage("", ex);
			}

		}

		#endregion

		#region IPackedFileUI Member
		/// <summary>
		/// Returns the Panel that will be displayed within SimPe
		/// </summary>
		public System.Windows.Forms.Panel GUIHandle
		{
			get 
			{
				return extObjdPanel;
			}
		}

		/// <summary>
		/// Is called by SimPe (through the Wrapper) when the Panel is going to be displayed, so
		/// you should updatet the Data displayed by the Panel with the Attributes stored in the
		/// passed Wrapper.
		/// </summary>
		/// <remarks>attr.Tag is used to let TextChanged event handlers know the change is being
		/// made internally rather than by the users.</remarks>
		/// <param name="wrapper">The Attributes of this Wrapper have to be displayed</param>
		public void UpdateGUI(IFileWrapper wrp)
		{
			wrapper = (ExtObjd)wrp;
			initialguid = wrapper.Guid;
			this.Tag = true;

			try 
			{
				pg.SelectedObject = null;
				tc.SelectedTab = tpcatalogsort;				

				cbtype.SelectedIndex = 0;
				for (int i=0; i<cbtype.Items.Count; i++)
				{
					Data.ObjectTypes ot = (Data.ObjectTypes)cbtype.Items[i];
					if (ot==wrapper.Type) 
					{
						cbtype.SelectedIndex = i;
						break;
					}
				}

				tbtype.Text = "0x"+Helper.HexString((ushort)(wrapper.Type));

				tbguid.Text = "0x"+Helper.HexString(wrapper.Guid);
				tbproxguid.Text = "0x"+Helper.HexString(wrapper.ProxyGuid);
				tborgguid.Text = "0x"+Helper.HexString(wrapper.OriginalGuid);

				tbflname.Text = wrapper.FileName;

				cbbathroom.Checked = (wrapper.RoomSort.InBathroom);
				cbbedroom.Checked = (wrapper.RoomSort.InBedroom);
				cbdinigroom.Checked = (wrapper.RoomSort.InDiningRoom);
				cbkitchen.Checked = (wrapper.RoomSort.InKitchen);
				cblivingroom.Checked = (wrapper.RoomSort.InLivingRoom);
				cbmisc.Checked = (wrapper.RoomSort.InMisc);
				cboutside.Checked = (wrapper.RoomSort.InOutside);
				cbstudy.Checked = (wrapper.RoomSort.InStudy);
				cbkids.Checked = (wrapper.RoomSort.InKids);

				cbappliances.Checked = wrapper.FunctionSort.InAppliances;
				cbdecorative.Checked = wrapper.FunctionSort.InDecorative;
				cbelectronics.Checked = wrapper.FunctionSort.InElectronics;
				cbgeneral.Checked = wrapper.FunctionSort.InGeneral;
				cblightning.Checked = wrapper.FunctionSort.InLighting;
				cbplumbing.Checked = wrapper.FunctionSort.InPlumbing;
				cbseating.Checked = wrapper.FunctionSort.InSeating;
				cbsurfaces.Checked = wrapper.FunctionSort.InSurfaces;
				cbhobby.Checked = wrapper.FunctionSort.InHobbies;
				cbaspiration.Checked = wrapper.FunctionSort.InAspirationRewards;
				cbcareer.Checked = wrapper.FunctionSort.InCareerRewards;
			} 
			finally 
			{
				Tag = null;
			}
		}

		#endregion

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ExtObjdForm));
			this.extObjdPanel = new System.Windows.Forms.Panel();
			this.cball = new System.Windows.Forms.CheckBox();
			this.tc = new System.Windows.Forms.TabControl();
			this.tpcatalogsort = new System.Windows.Forms.TabPage();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.cbaspiration = new System.Windows.Forms.CheckBox();
			this.cbhobby = new System.Windows.Forms.CheckBox();
			this.cbappliances = new System.Windows.Forms.CheckBox();
			this.cbdecorative = new System.Windows.Forms.CheckBox();
			this.cbelectronics = new System.Windows.Forms.CheckBox();
			this.cbgeneral = new System.Windows.Forms.CheckBox();
			this.cblightning = new System.Windows.Forms.CheckBox();
			this.cbplumbing = new System.Windows.Forms.CheckBox();
			this.cbseating = new System.Windows.Forms.CheckBox();
			this.cbsurfaces = new System.Windows.Forms.CheckBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.cbkids = new System.Windows.Forms.CheckBox();
			this.cbbathroom = new System.Windows.Forms.CheckBox();
			this.cbbedroom = new System.Windows.Forms.CheckBox();
			this.cbdinigroom = new System.Windows.Forms.CheckBox();
			this.cbkitchen = new System.Windows.Forms.CheckBox();
			this.cbmisc = new System.Windows.Forms.CheckBox();
			this.cboutside = new System.Windows.Forms.CheckBox();
			this.cblivingroom = new System.Windows.Forms.CheckBox();
			this.cbstudy = new System.Windows.Forms.CheckBox();
			this.tpraw = new System.Windows.Forms.TabPage();
			this.rbhex = new System.Windows.Forms.RadioButton();
			this.rbdec = new System.Windows.Forms.RadioButton();
			this.pg = new System.Windows.Forms.PropertyGrid();
			this.rbbin = new System.Windows.Forms.RadioButton();
			this.tbtype = new System.Windows.Forms.TextBox();
			this.cbtype = new System.Windows.Forms.ComboBox();
			this.label63 = new System.Windows.Forms.Label();
			this.tbproxguid = new System.Windows.Forms.TextBox();
			this.label97 = new System.Windows.Forms.Label();
			this.tborgguid = new System.Windows.Forms.TextBox();
			this.llgetGUID = new System.Windows.Forms.LinkLabel();
			this.llcommitobjd = new System.Windows.Forms.LinkLabel();
			this.label65 = new System.Windows.Forms.Label();
			this.tbflname = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.tbguid = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.panel6 = new System.Windows.Forms.Panel();
			this.label12 = new System.Windows.Forms.Label();
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
			this.cbcareer = new System.Windows.Forms.CheckBox();
			this.extObjdPanel.SuspendLayout();
			this.tc.SuspendLayout();
			this.tpcatalogsort.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.tpraw.SuspendLayout();
			this.panel6.SuspendLayout();
			this.SuspendLayout();
			// 
			// extObjdPanel
			// 
			this.extObjdPanel.AccessibleDescription = resources.GetString("extObjdPanel.AccessibleDescription");
			this.extObjdPanel.AccessibleName = resources.GetString("extObjdPanel.AccessibleName");
			this.extObjdPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("extObjdPanel.Anchor")));
			this.extObjdPanel.AutoScroll = ((bool)(resources.GetObject("extObjdPanel.AutoScroll")));
			this.extObjdPanel.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("extObjdPanel.AutoScrollMargin")));
			this.extObjdPanel.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("extObjdPanel.AutoScrollMinSize")));
			this.extObjdPanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("extObjdPanel.BackgroundImage")));
			this.extObjdPanel.Controls.Add(this.cball);
			this.extObjdPanel.Controls.Add(this.tc);
			this.extObjdPanel.Controls.Add(this.tbtype);
			this.extObjdPanel.Controls.Add(this.cbtype);
			this.extObjdPanel.Controls.Add(this.label63);
			this.extObjdPanel.Controls.Add(this.tbproxguid);
			this.extObjdPanel.Controls.Add(this.label97);
			this.extObjdPanel.Controls.Add(this.tborgguid);
			this.extObjdPanel.Controls.Add(this.llgetGUID);
			this.extObjdPanel.Controls.Add(this.llcommitobjd);
			this.extObjdPanel.Controls.Add(this.label65);
			this.extObjdPanel.Controls.Add(this.tbflname);
			this.extObjdPanel.Controls.Add(this.label9);
			this.extObjdPanel.Controls.Add(this.tbguid);
			this.extObjdPanel.Controls.Add(this.label8);
			this.extObjdPanel.Controls.Add(this.panel6);
			this.extObjdPanel.Controls.Add(this.linkLabel1);
			this.extObjdPanel.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("extObjdPanel.Dock")));
			this.extObjdPanel.Enabled = ((bool)(resources.GetObject("extObjdPanel.Enabled")));
			this.extObjdPanel.Font = ((System.Drawing.Font)(resources.GetObject("extObjdPanel.Font")));
			this.extObjdPanel.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("extObjdPanel.ImeMode")));
			this.extObjdPanel.Location = ((System.Drawing.Point)(resources.GetObject("extObjdPanel.Location")));
			this.extObjdPanel.Name = "extObjdPanel";
			this.extObjdPanel.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("extObjdPanel.RightToLeft")));
			this.extObjdPanel.Size = ((System.Drawing.Size)(resources.GetObject("extObjdPanel.Size")));
			this.extObjdPanel.TabIndex = ((int)(resources.GetObject("extObjdPanel.TabIndex")));
			this.extObjdPanel.Text = resources.GetString("extObjdPanel.Text");
			this.extObjdPanel.Visible = ((bool)(resources.GetObject("extObjdPanel.Visible")));
			// 
			// cball
			// 
			this.cball.AccessibleDescription = resources.GetString("cball.AccessibleDescription");
			this.cball.AccessibleName = resources.GetString("cball.AccessibleName");
			this.cball.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cball.Anchor")));
			this.cball.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("cball.Appearance")));
			this.cball.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cball.BackgroundImage")));
			this.cball.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cball.CheckAlign")));
			this.cball.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cball.Dock")));
			this.cball.Enabled = ((bool)(resources.GetObject("cball.Enabled")));
			this.cball.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("cball.FlatStyle")));
			this.cball.Font = ((System.Drawing.Font)(resources.GetObject("cball.Font")));
			this.cball.Image = ((System.Drawing.Image)(resources.GetObject("cball.Image")));
			this.cball.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cball.ImageAlign")));
			this.cball.ImageIndex = ((int)(resources.GetObject("cball.ImageIndex")));
			this.cball.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cball.ImeMode")));
			this.cball.Location = ((System.Drawing.Point)(resources.GetObject("cball.Location")));
			this.cball.Name = "cball";
			this.cball.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cball.RightToLeft")));
			this.cball.Size = ((System.Drawing.Size)(resources.GetObject("cball.Size")));
			this.cball.TabIndex = ((int)(resources.GetObject("cball.TabIndex")));
			this.cball.Text = resources.GetString("cball.Text");
			this.cball.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cball.TextAlign")));
			this.cball.Visible = ((bool)(resources.GetObject("cball.Visible")));
			// 
			// tc
			// 
			this.tc.AccessibleDescription = resources.GetString("tc.AccessibleDescription");
			this.tc.AccessibleName = resources.GetString("tc.AccessibleName");
			this.tc.Alignment = ((System.Windows.Forms.TabAlignment)(resources.GetObject("tc.Alignment")));
			this.tc.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tc.Anchor")));
			this.tc.Appearance = ((System.Windows.Forms.TabAppearance)(resources.GetObject("tc.Appearance")));
			this.tc.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tc.BackgroundImage")));
			this.tc.Controls.Add(this.tpcatalogsort);
			this.tc.Controls.Add(this.tpraw);
			this.tc.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tc.Dock")));
			this.tc.Enabled = ((bool)(resources.GetObject("tc.Enabled")));
			this.tc.Font = ((System.Drawing.Font)(resources.GetObject("tc.Font")));
			this.tc.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tc.ImeMode")));
			this.tc.ItemSize = ((System.Drawing.Size)(resources.GetObject("tc.ItemSize")));
			this.tc.Location = ((System.Drawing.Point)(resources.GetObject("tc.Location")));
			this.tc.Name = "tc";
			this.tc.Padding = ((System.Drawing.Point)(resources.GetObject("tc.Padding")));
			this.tc.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tc.RightToLeft")));
			this.tc.SelectedIndex = 0;
			this.tc.ShowToolTips = ((bool)(resources.GetObject("tc.ShowToolTips")));
			this.tc.Size = ((System.Drawing.Size)(resources.GetObject("tc.Size")));
			this.tc.TabIndex = ((int)(resources.GetObject("tc.TabIndex")));
			this.tc.Text = resources.GetString("tc.Text");
			this.tc.Visible = ((bool)(resources.GetObject("tc.Visible")));
			this.tc.SelectedIndexChanged += new System.EventHandler(this.CangedTab);
			// 
			// tpcatalogsort
			// 
			this.tpcatalogsort.AccessibleDescription = resources.GetString("tpcatalogsort.AccessibleDescription");
			this.tpcatalogsort.AccessibleName = resources.GetString("tpcatalogsort.AccessibleName");
			this.tpcatalogsort.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tpcatalogsort.Anchor")));
			this.tpcatalogsort.AutoScroll = ((bool)(resources.GetObject("tpcatalogsort.AutoScroll")));
			this.tpcatalogsort.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("tpcatalogsort.AutoScrollMargin")));
			this.tpcatalogsort.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("tpcatalogsort.AutoScrollMinSize")));
			this.tpcatalogsort.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tpcatalogsort.BackgroundImage")));
			this.tpcatalogsort.Controls.Add(this.groupBox2);
			this.tpcatalogsort.Controls.Add(this.groupBox1);
			this.tpcatalogsort.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tpcatalogsort.Dock")));
			this.tpcatalogsort.Enabled = ((bool)(resources.GetObject("tpcatalogsort.Enabled")));
			this.tpcatalogsort.Font = ((System.Drawing.Font)(resources.GetObject("tpcatalogsort.Font")));
			this.tpcatalogsort.ImageIndex = ((int)(resources.GetObject("tpcatalogsort.ImageIndex")));
			this.tpcatalogsort.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tpcatalogsort.ImeMode")));
			this.tpcatalogsort.Location = ((System.Drawing.Point)(resources.GetObject("tpcatalogsort.Location")));
			this.tpcatalogsort.Name = "tpcatalogsort";
			this.tpcatalogsort.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tpcatalogsort.RightToLeft")));
			this.tpcatalogsort.Size = ((System.Drawing.Size)(resources.GetObject("tpcatalogsort.Size")));
			this.tpcatalogsort.TabIndex = ((int)(resources.GetObject("tpcatalogsort.TabIndex")));
			this.tpcatalogsort.Text = resources.GetString("tpcatalogsort.Text");
			this.tpcatalogsort.ToolTipText = resources.GetString("tpcatalogsort.ToolTipText");
			this.tpcatalogsort.Visible = ((bool)(resources.GetObject("tpcatalogsort.Visible")));
			// 
			// groupBox2
			// 
			this.groupBox2.AccessibleDescription = resources.GetString("groupBox2.AccessibleDescription");
			this.groupBox2.AccessibleName = resources.GetString("groupBox2.AccessibleName");
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("groupBox2.Anchor")));
			this.groupBox2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("groupBox2.BackgroundImage")));
			this.groupBox2.Controls.Add(this.cbaspiration);
			this.groupBox2.Controls.Add(this.cbhobby);
			this.groupBox2.Controls.Add(this.cbappliances);
			this.groupBox2.Controls.Add(this.cbdecorative);
			this.groupBox2.Controls.Add(this.cbelectronics);
			this.groupBox2.Controls.Add(this.cbgeneral);
			this.groupBox2.Controls.Add(this.cblightning);
			this.groupBox2.Controls.Add(this.cbplumbing);
			this.groupBox2.Controls.Add(this.cbseating);
			this.groupBox2.Controls.Add(this.cbsurfaces);
			this.groupBox2.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("groupBox2.Dock")));
			this.groupBox2.Enabled = ((bool)(resources.GetObject("groupBox2.Enabled")));
			this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox2.Font = ((System.Drawing.Font)(resources.GetObject("groupBox2.Font")));
			this.groupBox2.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("groupBox2.ImeMode")));
			this.groupBox2.Location = ((System.Drawing.Point)(resources.GetObject("groupBox2.Location")));
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("groupBox2.RightToLeft")));
			this.groupBox2.Size = ((System.Drawing.Size)(resources.GetObject("groupBox2.Size")));
			this.groupBox2.TabIndex = ((int)(resources.GetObject("groupBox2.TabIndex")));
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = resources.GetString("groupBox2.Text");
			this.groupBox2.Visible = ((bool)(resources.GetObject("groupBox2.Visible")));
			// 
			// cbaspiration
			// 
			this.cbaspiration.AccessibleDescription = resources.GetString("cbaspiration.AccessibleDescription");
			this.cbaspiration.AccessibleName = resources.GetString("cbaspiration.AccessibleName");
			this.cbaspiration.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cbaspiration.Anchor")));
			this.cbaspiration.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("cbaspiration.Appearance")));
			this.cbaspiration.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cbaspiration.BackgroundImage")));
			this.cbaspiration.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbaspiration.CheckAlign")));
			this.cbaspiration.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cbaspiration.Dock")));
			this.cbaspiration.Enabled = ((bool)(resources.GetObject("cbaspiration.Enabled")));
			this.cbaspiration.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("cbaspiration.FlatStyle")));
			this.cbaspiration.Font = ((System.Drawing.Font)(resources.GetObject("cbaspiration.Font")));
			this.cbaspiration.Image = ((System.Drawing.Image)(resources.GetObject("cbaspiration.Image")));
			this.cbaspiration.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbaspiration.ImageAlign")));
			this.cbaspiration.ImageIndex = ((int)(resources.GetObject("cbaspiration.ImageIndex")));
			this.cbaspiration.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cbaspiration.ImeMode")));
			this.cbaspiration.Location = ((System.Drawing.Point)(resources.GetObject("cbaspiration.Location")));
			this.cbaspiration.Name = "cbaspiration";
			this.cbaspiration.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cbaspiration.RightToLeft")));
			this.cbaspiration.Size = ((System.Drawing.Size)(resources.GetObject("cbaspiration.Size")));
			this.cbaspiration.TabIndex = ((int)(resources.GetObject("cbaspiration.TabIndex")));
			this.cbaspiration.Text = resources.GetString("cbaspiration.Text");
			this.cbaspiration.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbaspiration.TextAlign")));
			this.cbaspiration.Visible = ((bool)(resources.GetObject("cbaspiration.Visible")));
			this.cbaspiration.CheckedChanged += new System.EventHandler(this.SetFunctionFlags);
			// 
			// cbhobby
			// 
			this.cbhobby.AccessibleDescription = resources.GetString("cbhobby.AccessibleDescription");
			this.cbhobby.AccessibleName = resources.GetString("cbhobby.AccessibleName");
			this.cbhobby.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cbhobby.Anchor")));
			this.cbhobby.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("cbhobby.Appearance")));
			this.cbhobby.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cbhobby.BackgroundImage")));
			this.cbhobby.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbhobby.CheckAlign")));
			this.cbhobby.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cbhobby.Dock")));
			this.cbhobby.Enabled = ((bool)(resources.GetObject("cbhobby.Enabled")));
			this.cbhobby.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("cbhobby.FlatStyle")));
			this.cbhobby.Font = ((System.Drawing.Font)(resources.GetObject("cbhobby.Font")));
			this.cbhobby.Image = ((System.Drawing.Image)(resources.GetObject("cbhobby.Image")));
			this.cbhobby.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbhobby.ImageAlign")));
			this.cbhobby.ImageIndex = ((int)(resources.GetObject("cbhobby.ImageIndex")));
			this.cbhobby.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cbhobby.ImeMode")));
			this.cbhobby.Location = ((System.Drawing.Point)(resources.GetObject("cbhobby.Location")));
			this.cbhobby.Name = "cbhobby";
			this.cbhobby.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cbhobby.RightToLeft")));
			this.cbhobby.Size = ((System.Drawing.Size)(resources.GetObject("cbhobby.Size")));
			this.cbhobby.TabIndex = ((int)(resources.GetObject("cbhobby.TabIndex")));
			this.cbhobby.Text = resources.GetString("cbhobby.Text");
			this.cbhobby.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbhobby.TextAlign")));
			this.cbhobby.Visible = ((bool)(resources.GetObject("cbhobby.Visible")));
			this.cbhobby.CheckedChanged += new System.EventHandler(this.SetFunctionFlags);
			// 
			// cbappliances
			// 
			this.cbappliances.AccessibleDescription = resources.GetString("cbappliances.AccessibleDescription");
			this.cbappliances.AccessibleName = resources.GetString("cbappliances.AccessibleName");
			this.cbappliances.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cbappliances.Anchor")));
			this.cbappliances.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("cbappliances.Appearance")));
			this.cbappliances.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cbappliances.BackgroundImage")));
			this.cbappliances.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbappliances.CheckAlign")));
			this.cbappliances.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cbappliances.Dock")));
			this.cbappliances.Enabled = ((bool)(resources.GetObject("cbappliances.Enabled")));
			this.cbappliances.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("cbappliances.FlatStyle")));
			this.cbappliances.Font = ((System.Drawing.Font)(resources.GetObject("cbappliances.Font")));
			this.cbappliances.Image = ((System.Drawing.Image)(resources.GetObject("cbappliances.Image")));
			this.cbappliances.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbappliances.ImageAlign")));
			this.cbappliances.ImageIndex = ((int)(resources.GetObject("cbappliances.ImageIndex")));
			this.cbappliances.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cbappliances.ImeMode")));
			this.cbappliances.Location = ((System.Drawing.Point)(resources.GetObject("cbappliances.Location")));
			this.cbappliances.Name = "cbappliances";
			this.cbappliances.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cbappliances.RightToLeft")));
			this.cbappliances.Size = ((System.Drawing.Size)(resources.GetObject("cbappliances.Size")));
			this.cbappliances.TabIndex = ((int)(resources.GetObject("cbappliances.TabIndex")));
			this.cbappliances.Text = resources.GetString("cbappliances.Text");
			this.cbappliances.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbappliances.TextAlign")));
			this.cbappliances.Visible = ((bool)(resources.GetObject("cbappliances.Visible")));
			this.cbappliances.CheckedChanged += new System.EventHandler(this.SetFunctionFlags);
			// 
			// cbdecorative
			// 
			this.cbdecorative.AccessibleDescription = resources.GetString("cbdecorative.AccessibleDescription");
			this.cbdecorative.AccessibleName = resources.GetString("cbdecorative.AccessibleName");
			this.cbdecorative.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cbdecorative.Anchor")));
			this.cbdecorative.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("cbdecorative.Appearance")));
			this.cbdecorative.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cbdecorative.BackgroundImage")));
			this.cbdecorative.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbdecorative.CheckAlign")));
			this.cbdecorative.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cbdecorative.Dock")));
			this.cbdecorative.Enabled = ((bool)(resources.GetObject("cbdecorative.Enabled")));
			this.cbdecorative.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("cbdecorative.FlatStyle")));
			this.cbdecorative.Font = ((System.Drawing.Font)(resources.GetObject("cbdecorative.Font")));
			this.cbdecorative.Image = ((System.Drawing.Image)(resources.GetObject("cbdecorative.Image")));
			this.cbdecorative.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbdecorative.ImageAlign")));
			this.cbdecorative.ImageIndex = ((int)(resources.GetObject("cbdecorative.ImageIndex")));
			this.cbdecorative.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cbdecorative.ImeMode")));
			this.cbdecorative.Location = ((System.Drawing.Point)(resources.GetObject("cbdecorative.Location")));
			this.cbdecorative.Name = "cbdecorative";
			this.cbdecorative.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cbdecorative.RightToLeft")));
			this.cbdecorative.Size = ((System.Drawing.Size)(resources.GetObject("cbdecorative.Size")));
			this.cbdecorative.TabIndex = ((int)(resources.GetObject("cbdecorative.TabIndex")));
			this.cbdecorative.Text = resources.GetString("cbdecorative.Text");
			this.cbdecorative.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbdecorative.TextAlign")));
			this.cbdecorative.Visible = ((bool)(resources.GetObject("cbdecorative.Visible")));
			this.cbdecorative.CheckedChanged += new System.EventHandler(this.SetFunctionFlags);
			// 
			// cbelectronics
			// 
			this.cbelectronics.AccessibleDescription = resources.GetString("cbelectronics.AccessibleDescription");
			this.cbelectronics.AccessibleName = resources.GetString("cbelectronics.AccessibleName");
			this.cbelectronics.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cbelectronics.Anchor")));
			this.cbelectronics.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("cbelectronics.Appearance")));
			this.cbelectronics.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cbelectronics.BackgroundImage")));
			this.cbelectronics.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbelectronics.CheckAlign")));
			this.cbelectronics.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cbelectronics.Dock")));
			this.cbelectronics.Enabled = ((bool)(resources.GetObject("cbelectronics.Enabled")));
			this.cbelectronics.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("cbelectronics.FlatStyle")));
			this.cbelectronics.Font = ((System.Drawing.Font)(resources.GetObject("cbelectronics.Font")));
			this.cbelectronics.Image = ((System.Drawing.Image)(resources.GetObject("cbelectronics.Image")));
			this.cbelectronics.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbelectronics.ImageAlign")));
			this.cbelectronics.ImageIndex = ((int)(resources.GetObject("cbelectronics.ImageIndex")));
			this.cbelectronics.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cbelectronics.ImeMode")));
			this.cbelectronics.Location = ((System.Drawing.Point)(resources.GetObject("cbelectronics.Location")));
			this.cbelectronics.Name = "cbelectronics";
			this.cbelectronics.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cbelectronics.RightToLeft")));
			this.cbelectronics.Size = ((System.Drawing.Size)(resources.GetObject("cbelectronics.Size")));
			this.cbelectronics.TabIndex = ((int)(resources.GetObject("cbelectronics.TabIndex")));
			this.cbelectronics.Text = resources.GetString("cbelectronics.Text");
			this.cbelectronics.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbelectronics.TextAlign")));
			this.cbelectronics.Visible = ((bool)(resources.GetObject("cbelectronics.Visible")));
			this.cbelectronics.CheckedChanged += new System.EventHandler(this.SetFunctionFlags);
			// 
			// cbgeneral
			// 
			this.cbgeneral.AccessibleDescription = resources.GetString("cbgeneral.AccessibleDescription");
			this.cbgeneral.AccessibleName = resources.GetString("cbgeneral.AccessibleName");
			this.cbgeneral.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cbgeneral.Anchor")));
			this.cbgeneral.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("cbgeneral.Appearance")));
			this.cbgeneral.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cbgeneral.BackgroundImage")));
			this.cbgeneral.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbgeneral.CheckAlign")));
			this.cbgeneral.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cbgeneral.Dock")));
			this.cbgeneral.Enabled = ((bool)(resources.GetObject("cbgeneral.Enabled")));
			this.cbgeneral.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("cbgeneral.FlatStyle")));
			this.cbgeneral.Font = ((System.Drawing.Font)(resources.GetObject("cbgeneral.Font")));
			this.cbgeneral.Image = ((System.Drawing.Image)(resources.GetObject("cbgeneral.Image")));
			this.cbgeneral.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbgeneral.ImageAlign")));
			this.cbgeneral.ImageIndex = ((int)(resources.GetObject("cbgeneral.ImageIndex")));
			this.cbgeneral.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cbgeneral.ImeMode")));
			this.cbgeneral.Location = ((System.Drawing.Point)(resources.GetObject("cbgeneral.Location")));
			this.cbgeneral.Name = "cbgeneral";
			this.cbgeneral.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cbgeneral.RightToLeft")));
			this.cbgeneral.Size = ((System.Drawing.Size)(resources.GetObject("cbgeneral.Size")));
			this.cbgeneral.TabIndex = ((int)(resources.GetObject("cbgeneral.TabIndex")));
			this.cbgeneral.Text = resources.GetString("cbgeneral.Text");
			this.cbgeneral.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbgeneral.TextAlign")));
			this.cbgeneral.Visible = ((bool)(resources.GetObject("cbgeneral.Visible")));
			this.cbgeneral.CheckedChanged += new System.EventHandler(this.SetFunctionFlags);
			// 
			// cblightning
			// 
			this.cblightning.AccessibleDescription = resources.GetString("cblightning.AccessibleDescription");
			this.cblightning.AccessibleName = resources.GetString("cblightning.AccessibleName");
			this.cblightning.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cblightning.Anchor")));
			this.cblightning.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("cblightning.Appearance")));
			this.cblightning.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cblightning.BackgroundImage")));
			this.cblightning.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cblightning.CheckAlign")));
			this.cblightning.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cblightning.Dock")));
			this.cblightning.Enabled = ((bool)(resources.GetObject("cblightning.Enabled")));
			this.cblightning.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("cblightning.FlatStyle")));
			this.cblightning.Font = ((System.Drawing.Font)(resources.GetObject("cblightning.Font")));
			this.cblightning.Image = ((System.Drawing.Image)(resources.GetObject("cblightning.Image")));
			this.cblightning.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cblightning.ImageAlign")));
			this.cblightning.ImageIndex = ((int)(resources.GetObject("cblightning.ImageIndex")));
			this.cblightning.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cblightning.ImeMode")));
			this.cblightning.Location = ((System.Drawing.Point)(resources.GetObject("cblightning.Location")));
			this.cblightning.Name = "cblightning";
			this.cblightning.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cblightning.RightToLeft")));
			this.cblightning.Size = ((System.Drawing.Size)(resources.GetObject("cblightning.Size")));
			this.cblightning.TabIndex = ((int)(resources.GetObject("cblightning.TabIndex")));
			this.cblightning.Text = resources.GetString("cblightning.Text");
			this.cblightning.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cblightning.TextAlign")));
			this.cblightning.Visible = ((bool)(resources.GetObject("cblightning.Visible")));
			this.cblightning.CheckedChanged += new System.EventHandler(this.SetFunctionFlags);
			// 
			// cbplumbing
			// 
			this.cbplumbing.AccessibleDescription = resources.GetString("cbplumbing.AccessibleDescription");
			this.cbplumbing.AccessibleName = resources.GetString("cbplumbing.AccessibleName");
			this.cbplumbing.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cbplumbing.Anchor")));
			this.cbplumbing.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("cbplumbing.Appearance")));
			this.cbplumbing.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cbplumbing.BackgroundImage")));
			this.cbplumbing.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbplumbing.CheckAlign")));
			this.cbplumbing.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cbplumbing.Dock")));
			this.cbplumbing.Enabled = ((bool)(resources.GetObject("cbplumbing.Enabled")));
			this.cbplumbing.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("cbplumbing.FlatStyle")));
			this.cbplumbing.Font = ((System.Drawing.Font)(resources.GetObject("cbplumbing.Font")));
			this.cbplumbing.Image = ((System.Drawing.Image)(resources.GetObject("cbplumbing.Image")));
			this.cbplumbing.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbplumbing.ImageAlign")));
			this.cbplumbing.ImageIndex = ((int)(resources.GetObject("cbplumbing.ImageIndex")));
			this.cbplumbing.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cbplumbing.ImeMode")));
			this.cbplumbing.Location = ((System.Drawing.Point)(resources.GetObject("cbplumbing.Location")));
			this.cbplumbing.Name = "cbplumbing";
			this.cbplumbing.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cbplumbing.RightToLeft")));
			this.cbplumbing.Size = ((System.Drawing.Size)(resources.GetObject("cbplumbing.Size")));
			this.cbplumbing.TabIndex = ((int)(resources.GetObject("cbplumbing.TabIndex")));
			this.cbplumbing.Text = resources.GetString("cbplumbing.Text");
			this.cbplumbing.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbplumbing.TextAlign")));
			this.cbplumbing.Visible = ((bool)(resources.GetObject("cbplumbing.Visible")));
			this.cbplumbing.CheckedChanged += new System.EventHandler(this.SetFunctionFlags);
			// 
			// cbseating
			// 
			this.cbseating.AccessibleDescription = resources.GetString("cbseating.AccessibleDescription");
			this.cbseating.AccessibleName = resources.GetString("cbseating.AccessibleName");
			this.cbseating.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cbseating.Anchor")));
			this.cbseating.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("cbseating.Appearance")));
			this.cbseating.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cbseating.BackgroundImage")));
			this.cbseating.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbseating.CheckAlign")));
			this.cbseating.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cbseating.Dock")));
			this.cbseating.Enabled = ((bool)(resources.GetObject("cbseating.Enabled")));
			this.cbseating.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("cbseating.FlatStyle")));
			this.cbseating.Font = ((System.Drawing.Font)(resources.GetObject("cbseating.Font")));
			this.cbseating.Image = ((System.Drawing.Image)(resources.GetObject("cbseating.Image")));
			this.cbseating.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbseating.ImageAlign")));
			this.cbseating.ImageIndex = ((int)(resources.GetObject("cbseating.ImageIndex")));
			this.cbseating.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cbseating.ImeMode")));
			this.cbseating.Location = ((System.Drawing.Point)(resources.GetObject("cbseating.Location")));
			this.cbseating.Name = "cbseating";
			this.cbseating.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cbseating.RightToLeft")));
			this.cbseating.Size = ((System.Drawing.Size)(resources.GetObject("cbseating.Size")));
			this.cbseating.TabIndex = ((int)(resources.GetObject("cbseating.TabIndex")));
			this.cbseating.Text = resources.GetString("cbseating.Text");
			this.cbseating.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbseating.TextAlign")));
			this.cbseating.Visible = ((bool)(resources.GetObject("cbseating.Visible")));
			this.cbseating.CheckedChanged += new System.EventHandler(this.SetFunctionFlags);
			// 
			// cbsurfaces
			// 
			this.cbsurfaces.AccessibleDescription = resources.GetString("cbsurfaces.AccessibleDescription");
			this.cbsurfaces.AccessibleName = resources.GetString("cbsurfaces.AccessibleName");
			this.cbsurfaces.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cbsurfaces.Anchor")));
			this.cbsurfaces.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("cbsurfaces.Appearance")));
			this.cbsurfaces.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cbsurfaces.BackgroundImage")));
			this.cbsurfaces.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbsurfaces.CheckAlign")));
			this.cbsurfaces.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cbsurfaces.Dock")));
			this.cbsurfaces.Enabled = ((bool)(resources.GetObject("cbsurfaces.Enabled")));
			this.cbsurfaces.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("cbsurfaces.FlatStyle")));
			this.cbsurfaces.Font = ((System.Drawing.Font)(resources.GetObject("cbsurfaces.Font")));
			this.cbsurfaces.Image = ((System.Drawing.Image)(resources.GetObject("cbsurfaces.Image")));
			this.cbsurfaces.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbsurfaces.ImageAlign")));
			this.cbsurfaces.ImageIndex = ((int)(resources.GetObject("cbsurfaces.ImageIndex")));
			this.cbsurfaces.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cbsurfaces.ImeMode")));
			this.cbsurfaces.Location = ((System.Drawing.Point)(resources.GetObject("cbsurfaces.Location")));
			this.cbsurfaces.Name = "cbsurfaces";
			this.cbsurfaces.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cbsurfaces.RightToLeft")));
			this.cbsurfaces.Size = ((System.Drawing.Size)(resources.GetObject("cbsurfaces.Size")));
			this.cbsurfaces.TabIndex = ((int)(resources.GetObject("cbsurfaces.TabIndex")));
			this.cbsurfaces.Text = resources.GetString("cbsurfaces.Text");
			this.cbsurfaces.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbsurfaces.TextAlign")));
			this.cbsurfaces.Visible = ((bool)(resources.GetObject("cbsurfaces.Visible")));
			this.cbsurfaces.CheckedChanged += new System.EventHandler(this.SetFunctionFlags);
			// 
			// groupBox1
			// 
			this.groupBox1.AccessibleDescription = resources.GetString("groupBox1.AccessibleDescription");
			this.groupBox1.AccessibleName = resources.GetString("groupBox1.AccessibleName");
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("groupBox1.Anchor")));
			this.groupBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("groupBox1.BackgroundImage")));
			this.groupBox1.Controls.Add(this.cbkids);
			this.groupBox1.Controls.Add(this.cbbathroom);
			this.groupBox1.Controls.Add(this.cbbedroom);
			this.groupBox1.Controls.Add(this.cbdinigroom);
			this.groupBox1.Controls.Add(this.cbkitchen);
			this.groupBox1.Controls.Add(this.cbmisc);
			this.groupBox1.Controls.Add(this.cboutside);
			this.groupBox1.Controls.Add(this.cblivingroom);
			this.groupBox1.Controls.Add(this.cbstudy);
			this.groupBox1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("groupBox1.Dock")));
			this.groupBox1.Enabled = ((bool)(resources.GetObject("groupBox1.Enabled")));
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Font = ((System.Drawing.Font)(resources.GetObject("groupBox1.Font")));
			this.groupBox1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("groupBox1.ImeMode")));
			this.groupBox1.Location = ((System.Drawing.Point)(resources.GetObject("groupBox1.Location")));
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("groupBox1.RightToLeft")));
			this.groupBox1.Size = ((System.Drawing.Size)(resources.GetObject("groupBox1.Size")));
			this.groupBox1.TabIndex = ((int)(resources.GetObject("groupBox1.TabIndex")));
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = resources.GetString("groupBox1.Text");
			this.groupBox1.Visible = ((bool)(resources.GetObject("groupBox1.Visible")));
			// 
			// cbkids
			// 
			this.cbkids.AccessibleDescription = resources.GetString("cbkids.AccessibleDescription");
			this.cbkids.AccessibleName = resources.GetString("cbkids.AccessibleName");
			this.cbkids.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cbkids.Anchor")));
			this.cbkids.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("cbkids.Appearance")));
			this.cbkids.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cbkids.BackgroundImage")));
			this.cbkids.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbkids.CheckAlign")));
			this.cbkids.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cbkids.Dock")));
			this.cbkids.Enabled = ((bool)(resources.GetObject("cbkids.Enabled")));
			this.cbkids.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("cbkids.FlatStyle")));
			this.cbkids.Font = ((System.Drawing.Font)(resources.GetObject("cbkids.Font")));
			this.cbkids.Image = ((System.Drawing.Image)(resources.GetObject("cbkids.Image")));
			this.cbkids.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbkids.ImageAlign")));
			this.cbkids.ImageIndex = ((int)(resources.GetObject("cbkids.ImageIndex")));
			this.cbkids.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cbkids.ImeMode")));
			this.cbkids.Location = ((System.Drawing.Point)(resources.GetObject("cbkids.Location")));
			this.cbkids.Name = "cbkids";
			this.cbkids.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cbkids.RightToLeft")));
			this.cbkids.Size = ((System.Drawing.Size)(resources.GetObject("cbkids.Size")));
			this.cbkids.TabIndex = ((int)(resources.GetObject("cbkids.TabIndex")));
			this.cbkids.Text = resources.GetString("cbkids.Text");
			this.cbkids.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbkids.TextAlign")));
			this.cbkids.Visible = ((bool)(resources.GetObject("cbkids.Visible")));
			this.cbkids.CheckedChanged += new System.EventHandler(this.SetRoomFlags);
			// 
			// cbbathroom
			// 
			this.cbbathroom.AccessibleDescription = resources.GetString("cbbathroom.AccessibleDescription");
			this.cbbathroom.AccessibleName = resources.GetString("cbbathroom.AccessibleName");
			this.cbbathroom.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cbbathroom.Anchor")));
			this.cbbathroom.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("cbbathroom.Appearance")));
			this.cbbathroom.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cbbathroom.BackgroundImage")));
			this.cbbathroom.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbbathroom.CheckAlign")));
			this.cbbathroom.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cbbathroom.Dock")));
			this.cbbathroom.Enabled = ((bool)(resources.GetObject("cbbathroom.Enabled")));
			this.cbbathroom.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("cbbathroom.FlatStyle")));
			this.cbbathroom.Font = ((System.Drawing.Font)(resources.GetObject("cbbathroom.Font")));
			this.cbbathroom.Image = ((System.Drawing.Image)(resources.GetObject("cbbathroom.Image")));
			this.cbbathroom.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbbathroom.ImageAlign")));
			this.cbbathroom.ImageIndex = ((int)(resources.GetObject("cbbathroom.ImageIndex")));
			this.cbbathroom.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cbbathroom.ImeMode")));
			this.cbbathroom.Location = ((System.Drawing.Point)(resources.GetObject("cbbathroom.Location")));
			this.cbbathroom.Name = "cbbathroom";
			this.cbbathroom.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cbbathroom.RightToLeft")));
			this.cbbathroom.Size = ((System.Drawing.Size)(resources.GetObject("cbbathroom.Size")));
			this.cbbathroom.TabIndex = ((int)(resources.GetObject("cbbathroom.TabIndex")));
			this.cbbathroom.Text = resources.GetString("cbbathroom.Text");
			this.cbbathroom.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbbathroom.TextAlign")));
			this.cbbathroom.Visible = ((bool)(resources.GetObject("cbbathroom.Visible")));
			this.cbbathroom.CheckedChanged += new System.EventHandler(this.SetRoomFlags);
			// 
			// cbbedroom
			// 
			this.cbbedroom.AccessibleDescription = resources.GetString("cbbedroom.AccessibleDescription");
			this.cbbedroom.AccessibleName = resources.GetString("cbbedroom.AccessibleName");
			this.cbbedroom.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cbbedroom.Anchor")));
			this.cbbedroom.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("cbbedroom.Appearance")));
			this.cbbedroom.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cbbedroom.BackgroundImage")));
			this.cbbedroom.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbbedroom.CheckAlign")));
			this.cbbedroom.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cbbedroom.Dock")));
			this.cbbedroom.Enabled = ((bool)(resources.GetObject("cbbedroom.Enabled")));
			this.cbbedroom.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("cbbedroom.FlatStyle")));
			this.cbbedroom.Font = ((System.Drawing.Font)(resources.GetObject("cbbedroom.Font")));
			this.cbbedroom.Image = ((System.Drawing.Image)(resources.GetObject("cbbedroom.Image")));
			this.cbbedroom.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbbedroom.ImageAlign")));
			this.cbbedroom.ImageIndex = ((int)(resources.GetObject("cbbedroom.ImageIndex")));
			this.cbbedroom.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cbbedroom.ImeMode")));
			this.cbbedroom.Location = ((System.Drawing.Point)(resources.GetObject("cbbedroom.Location")));
			this.cbbedroom.Name = "cbbedroom";
			this.cbbedroom.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cbbedroom.RightToLeft")));
			this.cbbedroom.Size = ((System.Drawing.Size)(resources.GetObject("cbbedroom.Size")));
			this.cbbedroom.TabIndex = ((int)(resources.GetObject("cbbedroom.TabIndex")));
			this.cbbedroom.Text = resources.GetString("cbbedroom.Text");
			this.cbbedroom.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbbedroom.TextAlign")));
			this.cbbedroom.Visible = ((bool)(resources.GetObject("cbbedroom.Visible")));
			this.cbbedroom.CheckedChanged += new System.EventHandler(this.SetRoomFlags);
			// 
			// cbdinigroom
			// 
			this.cbdinigroom.AccessibleDescription = resources.GetString("cbdinigroom.AccessibleDescription");
			this.cbdinigroom.AccessibleName = resources.GetString("cbdinigroom.AccessibleName");
			this.cbdinigroom.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cbdinigroom.Anchor")));
			this.cbdinigroom.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("cbdinigroom.Appearance")));
			this.cbdinigroom.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cbdinigroom.BackgroundImage")));
			this.cbdinigroom.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbdinigroom.CheckAlign")));
			this.cbdinigroom.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cbdinigroom.Dock")));
			this.cbdinigroom.Enabled = ((bool)(resources.GetObject("cbdinigroom.Enabled")));
			this.cbdinigroom.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("cbdinigroom.FlatStyle")));
			this.cbdinigroom.Font = ((System.Drawing.Font)(resources.GetObject("cbdinigroom.Font")));
			this.cbdinigroom.Image = ((System.Drawing.Image)(resources.GetObject("cbdinigroom.Image")));
			this.cbdinigroom.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbdinigroom.ImageAlign")));
			this.cbdinigroom.ImageIndex = ((int)(resources.GetObject("cbdinigroom.ImageIndex")));
			this.cbdinigroom.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cbdinigroom.ImeMode")));
			this.cbdinigroom.Location = ((System.Drawing.Point)(resources.GetObject("cbdinigroom.Location")));
			this.cbdinigroom.Name = "cbdinigroom";
			this.cbdinigroom.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cbdinigroom.RightToLeft")));
			this.cbdinigroom.Size = ((System.Drawing.Size)(resources.GetObject("cbdinigroom.Size")));
			this.cbdinigroom.TabIndex = ((int)(resources.GetObject("cbdinigroom.TabIndex")));
			this.cbdinigroom.Text = resources.GetString("cbdinigroom.Text");
			this.cbdinigroom.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbdinigroom.TextAlign")));
			this.cbdinigroom.Visible = ((bool)(resources.GetObject("cbdinigroom.Visible")));
			this.cbdinigroom.CheckedChanged += new System.EventHandler(this.SetRoomFlags);
			// 
			// cbkitchen
			// 
			this.cbkitchen.AccessibleDescription = resources.GetString("cbkitchen.AccessibleDescription");
			this.cbkitchen.AccessibleName = resources.GetString("cbkitchen.AccessibleName");
			this.cbkitchen.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cbkitchen.Anchor")));
			this.cbkitchen.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("cbkitchen.Appearance")));
			this.cbkitchen.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cbkitchen.BackgroundImage")));
			this.cbkitchen.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbkitchen.CheckAlign")));
			this.cbkitchen.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cbkitchen.Dock")));
			this.cbkitchen.Enabled = ((bool)(resources.GetObject("cbkitchen.Enabled")));
			this.cbkitchen.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("cbkitchen.FlatStyle")));
			this.cbkitchen.Font = ((System.Drawing.Font)(resources.GetObject("cbkitchen.Font")));
			this.cbkitchen.Image = ((System.Drawing.Image)(resources.GetObject("cbkitchen.Image")));
			this.cbkitchen.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbkitchen.ImageAlign")));
			this.cbkitchen.ImageIndex = ((int)(resources.GetObject("cbkitchen.ImageIndex")));
			this.cbkitchen.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cbkitchen.ImeMode")));
			this.cbkitchen.Location = ((System.Drawing.Point)(resources.GetObject("cbkitchen.Location")));
			this.cbkitchen.Name = "cbkitchen";
			this.cbkitchen.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cbkitchen.RightToLeft")));
			this.cbkitchen.Size = ((System.Drawing.Size)(resources.GetObject("cbkitchen.Size")));
			this.cbkitchen.TabIndex = ((int)(resources.GetObject("cbkitchen.TabIndex")));
			this.cbkitchen.Text = resources.GetString("cbkitchen.Text");
			this.cbkitchen.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbkitchen.TextAlign")));
			this.cbkitchen.Visible = ((bool)(resources.GetObject("cbkitchen.Visible")));
			this.cbkitchen.CheckedChanged += new System.EventHandler(this.SetRoomFlags);
			// 
			// cbmisc
			// 
			this.cbmisc.AccessibleDescription = resources.GetString("cbmisc.AccessibleDescription");
			this.cbmisc.AccessibleName = resources.GetString("cbmisc.AccessibleName");
			this.cbmisc.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cbmisc.Anchor")));
			this.cbmisc.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("cbmisc.Appearance")));
			this.cbmisc.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cbmisc.BackgroundImage")));
			this.cbmisc.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbmisc.CheckAlign")));
			this.cbmisc.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cbmisc.Dock")));
			this.cbmisc.Enabled = ((bool)(resources.GetObject("cbmisc.Enabled")));
			this.cbmisc.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("cbmisc.FlatStyle")));
			this.cbmisc.Font = ((System.Drawing.Font)(resources.GetObject("cbmisc.Font")));
			this.cbmisc.Image = ((System.Drawing.Image)(resources.GetObject("cbmisc.Image")));
			this.cbmisc.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbmisc.ImageAlign")));
			this.cbmisc.ImageIndex = ((int)(resources.GetObject("cbmisc.ImageIndex")));
			this.cbmisc.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cbmisc.ImeMode")));
			this.cbmisc.Location = ((System.Drawing.Point)(resources.GetObject("cbmisc.Location")));
			this.cbmisc.Name = "cbmisc";
			this.cbmisc.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cbmisc.RightToLeft")));
			this.cbmisc.Size = ((System.Drawing.Size)(resources.GetObject("cbmisc.Size")));
			this.cbmisc.TabIndex = ((int)(resources.GetObject("cbmisc.TabIndex")));
			this.cbmisc.Text = resources.GetString("cbmisc.Text");
			this.cbmisc.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbmisc.TextAlign")));
			this.cbmisc.Visible = ((bool)(resources.GetObject("cbmisc.Visible")));
			this.cbmisc.CheckedChanged += new System.EventHandler(this.SetRoomFlags);
			// 
			// cboutside
			// 
			this.cboutside.AccessibleDescription = resources.GetString("cboutside.AccessibleDescription");
			this.cboutside.AccessibleName = resources.GetString("cboutside.AccessibleName");
			this.cboutside.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cboutside.Anchor")));
			this.cboutside.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("cboutside.Appearance")));
			this.cboutside.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cboutside.BackgroundImage")));
			this.cboutside.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cboutside.CheckAlign")));
			this.cboutside.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cboutside.Dock")));
			this.cboutside.Enabled = ((bool)(resources.GetObject("cboutside.Enabled")));
			this.cboutside.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("cboutside.FlatStyle")));
			this.cboutside.Font = ((System.Drawing.Font)(resources.GetObject("cboutside.Font")));
			this.cboutside.Image = ((System.Drawing.Image)(resources.GetObject("cboutside.Image")));
			this.cboutside.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cboutside.ImageAlign")));
			this.cboutside.ImageIndex = ((int)(resources.GetObject("cboutside.ImageIndex")));
			this.cboutside.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cboutside.ImeMode")));
			this.cboutside.Location = ((System.Drawing.Point)(resources.GetObject("cboutside.Location")));
			this.cboutside.Name = "cboutside";
			this.cboutside.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cboutside.RightToLeft")));
			this.cboutside.Size = ((System.Drawing.Size)(resources.GetObject("cboutside.Size")));
			this.cboutside.TabIndex = ((int)(resources.GetObject("cboutside.TabIndex")));
			this.cboutside.Text = resources.GetString("cboutside.Text");
			this.cboutside.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cboutside.TextAlign")));
			this.cboutside.Visible = ((bool)(resources.GetObject("cboutside.Visible")));
			this.cboutside.CheckedChanged += new System.EventHandler(this.SetRoomFlags);
			// 
			// cblivingroom
			// 
			this.cblivingroom.AccessibleDescription = resources.GetString("cblivingroom.AccessibleDescription");
			this.cblivingroom.AccessibleName = resources.GetString("cblivingroom.AccessibleName");
			this.cblivingroom.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cblivingroom.Anchor")));
			this.cblivingroom.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("cblivingroom.Appearance")));
			this.cblivingroom.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cblivingroom.BackgroundImage")));
			this.cblivingroom.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cblivingroom.CheckAlign")));
			this.cblivingroom.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cblivingroom.Dock")));
			this.cblivingroom.Enabled = ((bool)(resources.GetObject("cblivingroom.Enabled")));
			this.cblivingroom.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("cblivingroom.FlatStyle")));
			this.cblivingroom.Font = ((System.Drawing.Font)(resources.GetObject("cblivingroom.Font")));
			this.cblivingroom.Image = ((System.Drawing.Image)(resources.GetObject("cblivingroom.Image")));
			this.cblivingroom.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cblivingroom.ImageAlign")));
			this.cblivingroom.ImageIndex = ((int)(resources.GetObject("cblivingroom.ImageIndex")));
			this.cblivingroom.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cblivingroom.ImeMode")));
			this.cblivingroom.Location = ((System.Drawing.Point)(resources.GetObject("cblivingroom.Location")));
			this.cblivingroom.Name = "cblivingroom";
			this.cblivingroom.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cblivingroom.RightToLeft")));
			this.cblivingroom.Size = ((System.Drawing.Size)(resources.GetObject("cblivingroom.Size")));
			this.cblivingroom.TabIndex = ((int)(resources.GetObject("cblivingroom.TabIndex")));
			this.cblivingroom.Text = resources.GetString("cblivingroom.Text");
			this.cblivingroom.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cblivingroom.TextAlign")));
			this.cblivingroom.Visible = ((bool)(resources.GetObject("cblivingroom.Visible")));
			this.cblivingroom.CheckedChanged += new System.EventHandler(this.SetRoomFlags);
			// 
			// cbstudy
			// 
			this.cbstudy.AccessibleDescription = resources.GetString("cbstudy.AccessibleDescription");
			this.cbstudy.AccessibleName = resources.GetString("cbstudy.AccessibleName");
			this.cbstudy.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cbstudy.Anchor")));
			this.cbstudy.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("cbstudy.Appearance")));
			this.cbstudy.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cbstudy.BackgroundImage")));
			this.cbstudy.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbstudy.CheckAlign")));
			this.cbstudy.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cbstudy.Dock")));
			this.cbstudy.Enabled = ((bool)(resources.GetObject("cbstudy.Enabled")));
			this.cbstudy.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("cbstudy.FlatStyle")));
			this.cbstudy.Font = ((System.Drawing.Font)(resources.GetObject("cbstudy.Font")));
			this.cbstudy.Image = ((System.Drawing.Image)(resources.GetObject("cbstudy.Image")));
			this.cbstudy.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbstudy.ImageAlign")));
			this.cbstudy.ImageIndex = ((int)(resources.GetObject("cbstudy.ImageIndex")));
			this.cbstudy.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cbstudy.ImeMode")));
			this.cbstudy.Location = ((System.Drawing.Point)(resources.GetObject("cbstudy.Location")));
			this.cbstudy.Name = "cbstudy";
			this.cbstudy.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cbstudy.RightToLeft")));
			this.cbstudy.Size = ((System.Drawing.Size)(resources.GetObject("cbstudy.Size")));
			this.cbstudy.TabIndex = ((int)(resources.GetObject("cbstudy.TabIndex")));
			this.cbstudy.Text = resources.GetString("cbstudy.Text");
			this.cbstudy.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbstudy.TextAlign")));
			this.cbstudy.Visible = ((bool)(resources.GetObject("cbstudy.Visible")));
			this.cbstudy.CheckedChanged += new System.EventHandler(this.SetRoomFlags);
			// 
			// tpraw
			// 
			this.tpraw.AccessibleDescription = resources.GetString("tpraw.AccessibleDescription");
			this.tpraw.AccessibleName = resources.GetString("tpraw.AccessibleName");
			this.tpraw.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tpraw.Anchor")));
			this.tpraw.AutoScroll = ((bool)(resources.GetObject("tpraw.AutoScroll")));
			this.tpraw.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("tpraw.AutoScrollMargin")));
			this.tpraw.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("tpraw.AutoScrollMinSize")));
			this.tpraw.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tpraw.BackgroundImage")));
			this.tpraw.Controls.Add(this.rbhex);
			this.tpraw.Controls.Add(this.rbdec);
			this.tpraw.Controls.Add(this.pg);
			this.tpraw.Controls.Add(this.rbbin);
			this.tpraw.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tpraw.Dock")));
			this.tpraw.Enabled = ((bool)(resources.GetObject("tpraw.Enabled")));
			this.tpraw.Font = ((System.Drawing.Font)(resources.GetObject("tpraw.Font")));
			this.tpraw.ImageIndex = ((int)(resources.GetObject("tpraw.ImageIndex")));
			this.tpraw.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tpraw.ImeMode")));
			this.tpraw.Location = ((System.Drawing.Point)(resources.GetObject("tpraw.Location")));
			this.tpraw.Name = "tpraw";
			this.tpraw.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tpraw.RightToLeft")));
			this.tpraw.Size = ((System.Drawing.Size)(resources.GetObject("tpraw.Size")));
			this.tpraw.TabIndex = ((int)(resources.GetObject("tpraw.TabIndex")));
			this.tpraw.Text = resources.GetString("tpraw.Text");
			this.tpraw.ToolTipText = resources.GetString("tpraw.ToolTipText");
			this.tpraw.Visible = ((bool)(resources.GetObject("tpraw.Visible")));
			// 
			// rbhex
			// 
			this.rbhex.AccessibleDescription = resources.GetString("rbhex.AccessibleDescription");
			this.rbhex.AccessibleName = resources.GetString("rbhex.AccessibleName");
			this.rbhex.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("rbhex.Anchor")));
			this.rbhex.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("rbhex.Appearance")));
			this.rbhex.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("rbhex.BackgroundImage")));
			this.rbhex.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("rbhex.CheckAlign")));
			this.rbhex.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("rbhex.Dock")));
			this.rbhex.Enabled = ((bool)(resources.GetObject("rbhex.Enabled")));
			this.rbhex.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("rbhex.FlatStyle")));
			this.rbhex.Font = ((System.Drawing.Font)(resources.GetObject("rbhex.Font")));
			this.rbhex.Image = ((System.Drawing.Image)(resources.GetObject("rbhex.Image")));
			this.rbhex.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("rbhex.ImageAlign")));
			this.rbhex.ImageIndex = ((int)(resources.GetObject("rbhex.ImageIndex")));
			this.rbhex.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("rbhex.ImeMode")));
			this.rbhex.Location = ((System.Drawing.Point)(resources.GetObject("rbhex.Location")));
			this.rbhex.Name = "rbhex";
			this.rbhex.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("rbhex.RightToLeft")));
			this.rbhex.Size = ((System.Drawing.Size)(resources.GetObject("rbhex.Size")));
			this.rbhex.TabIndex = ((int)(resources.GetObject("rbhex.TabIndex")));
			this.rbhex.Text = resources.GetString("rbhex.Text");
			this.rbhex.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("rbhex.TextAlign")));
			this.rbhex.Visible = ((bool)(resources.GetObject("rbhex.Visible")));
			this.rbhex.CheckedChanged += new System.EventHandler(this.DigitChanged);
			// 
			// rbdec
			// 
			this.rbdec.AccessibleDescription = resources.GetString("rbdec.AccessibleDescription");
			this.rbdec.AccessibleName = resources.GetString("rbdec.AccessibleName");
			this.rbdec.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("rbdec.Anchor")));
			this.rbdec.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("rbdec.Appearance")));
			this.rbdec.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("rbdec.BackgroundImage")));
			this.rbdec.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("rbdec.CheckAlign")));
			this.rbdec.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("rbdec.Dock")));
			this.rbdec.Enabled = ((bool)(resources.GetObject("rbdec.Enabled")));
			this.rbdec.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("rbdec.FlatStyle")));
			this.rbdec.Font = ((System.Drawing.Font)(resources.GetObject("rbdec.Font")));
			this.rbdec.Image = ((System.Drawing.Image)(resources.GetObject("rbdec.Image")));
			this.rbdec.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("rbdec.ImageAlign")));
			this.rbdec.ImageIndex = ((int)(resources.GetObject("rbdec.ImageIndex")));
			this.rbdec.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("rbdec.ImeMode")));
			this.rbdec.Location = ((System.Drawing.Point)(resources.GetObject("rbdec.Location")));
			this.rbdec.Name = "rbdec";
			this.rbdec.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("rbdec.RightToLeft")));
			this.rbdec.Size = ((System.Drawing.Size)(resources.GetObject("rbdec.Size")));
			this.rbdec.TabIndex = ((int)(resources.GetObject("rbdec.TabIndex")));
			this.rbdec.Text = resources.GetString("rbdec.Text");
			this.rbdec.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("rbdec.TextAlign")));
			this.rbdec.Visible = ((bool)(resources.GetObject("rbdec.Visible")));
			this.rbdec.CheckedChanged += new System.EventHandler(this.DigitChanged);
			// 
			// pg
			// 
			this.pg.AccessibleDescription = resources.GetString("pg.AccessibleDescription");
			this.pg.AccessibleName = resources.GetString("pg.AccessibleName");
			this.pg.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("pg.Anchor")));
			this.pg.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pg.BackgroundImage")));
			this.pg.CommandsVisibleIfAvailable = true;
			this.pg.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("pg.Dock")));
			this.pg.Enabled = ((bool)(resources.GetObject("pg.Enabled")));
			this.pg.Font = ((System.Drawing.Font)(resources.GetObject("pg.Font")));
			this.pg.HelpVisible = ((bool)(resources.GetObject("pg.HelpVisible")));
			this.pg.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("pg.ImeMode")));
			this.pg.LargeButtons = false;
			this.pg.LineColor = System.Drawing.SystemColors.ScrollBar;
			this.pg.Location = ((System.Drawing.Point)(resources.GetObject("pg.Location")));
			this.pg.Name = "pg";
			this.pg.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
			this.pg.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("pg.RightToLeft")));
			this.pg.Size = ((System.Drawing.Size)(resources.GetObject("pg.Size")));
			this.pg.TabIndex = ((int)(resources.GetObject("pg.TabIndex")));
			this.pg.Text = resources.GetString("pg.Text");
			this.pg.ToolbarVisible = false;
			this.pg.ViewBackColor = System.Drawing.SystemColors.Window;
			this.pg.ViewForeColor = System.Drawing.SystemColors.WindowText;
			this.pg.Visible = ((bool)(resources.GetObject("pg.Visible")));
			this.pg.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.PropChanged);
			// 
			// rbbin
			// 
			this.rbbin.AccessibleDescription = resources.GetString("rbbin.AccessibleDescription");
			this.rbbin.AccessibleName = resources.GetString("rbbin.AccessibleName");
			this.rbbin.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("rbbin.Anchor")));
			this.rbbin.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("rbbin.Appearance")));
			this.rbbin.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("rbbin.BackgroundImage")));
			this.rbbin.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("rbbin.CheckAlign")));
			this.rbbin.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("rbbin.Dock")));
			this.rbbin.Enabled = ((bool)(resources.GetObject("rbbin.Enabled")));
			this.rbbin.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("rbbin.FlatStyle")));
			this.rbbin.Font = ((System.Drawing.Font)(resources.GetObject("rbbin.Font")));
			this.rbbin.Image = ((System.Drawing.Image)(resources.GetObject("rbbin.Image")));
			this.rbbin.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("rbbin.ImageAlign")));
			this.rbbin.ImageIndex = ((int)(resources.GetObject("rbbin.ImageIndex")));
			this.rbbin.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("rbbin.ImeMode")));
			this.rbbin.Location = ((System.Drawing.Point)(resources.GetObject("rbbin.Location")));
			this.rbbin.Name = "rbbin";
			this.rbbin.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("rbbin.RightToLeft")));
			this.rbbin.Size = ((System.Drawing.Size)(resources.GetObject("rbbin.Size")));
			this.rbbin.TabIndex = ((int)(resources.GetObject("rbbin.TabIndex")));
			this.rbbin.Text = resources.GetString("rbbin.Text");
			this.rbbin.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("rbbin.TextAlign")));
			this.rbbin.Visible = ((bool)(resources.GetObject("rbbin.Visible")));
			this.rbbin.CheckedChanged += new System.EventHandler(this.DigitChanged);
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
			this.tbtype.ReadOnly = true;
			this.tbtype.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbtype.RightToLeft")));
			this.tbtype.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbtype.ScrollBars")));
			this.tbtype.Size = ((System.Drawing.Size)(resources.GetObject("tbtype.Size")));
			this.tbtype.TabIndex = ((int)(resources.GetObject("tbtype.TabIndex")));
			this.tbtype.Text = resources.GetString("tbtype.Text");
			this.tbtype.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbtype.TextAlign")));
			this.tbtype.Visible = ((bool)(resources.GetObject("tbtype.Visible")));
			this.tbtype.WordWrap = ((bool)(resources.GetObject("tbtype.WordWrap")));
			// 
			// cbtype
			// 
			this.cbtype.AccessibleDescription = resources.GetString("cbtype.AccessibleDescription");
			this.cbtype.AccessibleName = resources.GetString("cbtype.AccessibleName");
			this.cbtype.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cbtype.Anchor")));
			this.cbtype.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cbtype.BackgroundImage")));
			this.cbtype.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cbtype.Dock")));
			this.cbtype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbtype.Enabled = ((bool)(resources.GetObject("cbtype.Enabled")));
			this.cbtype.Font = ((System.Drawing.Font)(resources.GetObject("cbtype.Font")));
			this.cbtype.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cbtype.ImeMode")));
			this.cbtype.IntegralHeight = ((bool)(resources.GetObject("cbtype.IntegralHeight")));
			this.cbtype.ItemHeight = ((int)(resources.GetObject("cbtype.ItemHeight")));
			this.cbtype.Location = ((System.Drawing.Point)(resources.GetObject("cbtype.Location")));
			this.cbtype.MaxDropDownItems = ((int)(resources.GetObject("cbtype.MaxDropDownItems")));
			this.cbtype.MaxLength = ((int)(resources.GetObject("cbtype.MaxLength")));
			this.cbtype.Name = "cbtype";
			this.cbtype.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cbtype.RightToLeft")));
			this.cbtype.Size = ((System.Drawing.Size)(resources.GetObject("cbtype.Size")));
			this.cbtype.TabIndex = ((int)(resources.GetObject("cbtype.TabIndex")));
			this.cbtype.Text = resources.GetString("cbtype.Text");
			this.cbtype.Visible = ((bool)(resources.GetObject("cbtype.Visible")));
			this.cbtype.SelectedIndexChanged += new System.EventHandler(this.ChangeType);
			// 
			// label63
			// 
			this.label63.AccessibleDescription = resources.GetString("label63.AccessibleDescription");
			this.label63.AccessibleName = resources.GetString("label63.AccessibleName");
			this.label63.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label63.Anchor")));
			this.label63.AutoSize = ((bool)(resources.GetObject("label63.AutoSize")));
			this.label63.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label63.Dock")));
			this.label63.Enabled = ((bool)(resources.GetObject("label63.Enabled")));
			this.label63.Font = ((System.Drawing.Font)(resources.GetObject("label63.Font")));
			this.label63.Image = ((System.Drawing.Image)(resources.GetObject("label63.Image")));
			this.label63.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label63.ImageAlign")));
			this.label63.ImageIndex = ((int)(resources.GetObject("label63.ImageIndex")));
			this.label63.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label63.ImeMode")));
			this.label63.Location = ((System.Drawing.Point)(resources.GetObject("label63.Location")));
			this.label63.Name = "label63";
			this.label63.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label63.RightToLeft")));
			this.label63.Size = ((System.Drawing.Size)(resources.GetObject("label63.Size")));
			this.label63.TabIndex = ((int)(resources.GetObject("label63.TabIndex")));
			this.label63.Text = resources.GetString("label63.Text");
			this.label63.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label63.TextAlign")));
			this.label63.Visible = ((bool)(resources.GetObject("label63.Visible")));
			// 
			// tbproxguid
			// 
			this.tbproxguid.AccessibleDescription = resources.GetString("tbproxguid.AccessibleDescription");
			this.tbproxguid.AccessibleName = resources.GetString("tbproxguid.AccessibleName");
			this.tbproxguid.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbproxguid.Anchor")));
			this.tbproxguid.AutoSize = ((bool)(resources.GetObject("tbproxguid.AutoSize")));
			this.tbproxguid.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbproxguid.BackgroundImage")));
			this.tbproxguid.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbproxguid.Dock")));
			this.tbproxguid.Enabled = ((bool)(resources.GetObject("tbproxguid.Enabled")));
			this.tbproxguid.Font = ((System.Drawing.Font)(resources.GetObject("tbproxguid.Font")));
			this.tbproxguid.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbproxguid.ImeMode")));
			this.tbproxguid.Location = ((System.Drawing.Point)(resources.GetObject("tbproxguid.Location")));
			this.tbproxguid.MaxLength = ((int)(resources.GetObject("tbproxguid.MaxLength")));
			this.tbproxguid.Multiline = ((bool)(resources.GetObject("tbproxguid.Multiline")));
			this.tbproxguid.Name = "tbproxguid";
			this.tbproxguid.PasswordChar = ((char)(resources.GetObject("tbproxguid.PasswordChar")));
			this.tbproxguid.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbproxguid.RightToLeft")));
			this.tbproxguid.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbproxguid.ScrollBars")));
			this.tbproxguid.Size = ((System.Drawing.Size)(resources.GetObject("tbproxguid.Size")));
			this.tbproxguid.TabIndex = ((int)(resources.GetObject("tbproxguid.TabIndex")));
			this.tbproxguid.Text = resources.GetString("tbproxguid.Text");
			this.tbproxguid.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbproxguid.TextAlign")));
			this.tbproxguid.Visible = ((bool)(resources.GetObject("tbproxguid.Visible")));
			this.tbproxguid.WordWrap = ((bool)(resources.GetObject("tbproxguid.WordWrap")));
			this.tbproxguid.TextChanged += new System.EventHandler(this.SetGuid);
			// 
			// label97
			// 
			this.label97.AccessibleDescription = resources.GetString("label97.AccessibleDescription");
			this.label97.AccessibleName = resources.GetString("label97.AccessibleName");
			this.label97.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label97.Anchor")));
			this.label97.AutoSize = ((bool)(resources.GetObject("label97.AutoSize")));
			this.label97.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label97.Dock")));
			this.label97.Enabled = ((bool)(resources.GetObject("label97.Enabled")));
			this.label97.Font = ((System.Drawing.Font)(resources.GetObject("label97.Font")));
			this.label97.Image = ((System.Drawing.Image)(resources.GetObject("label97.Image")));
			this.label97.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label97.ImageAlign")));
			this.label97.ImageIndex = ((int)(resources.GetObject("label97.ImageIndex")));
			this.label97.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label97.ImeMode")));
			this.label97.Location = ((System.Drawing.Point)(resources.GetObject("label97.Location")));
			this.label97.Name = "label97";
			this.label97.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label97.RightToLeft")));
			this.label97.Size = ((System.Drawing.Size)(resources.GetObject("label97.Size")));
			this.label97.TabIndex = ((int)(resources.GetObject("label97.TabIndex")));
			this.label97.Text = resources.GetString("label97.Text");
			this.label97.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label97.TextAlign")));
			this.label97.Visible = ((bool)(resources.GetObject("label97.Visible")));
			// 
			// tborgguid
			// 
			this.tborgguid.AccessibleDescription = resources.GetString("tborgguid.AccessibleDescription");
			this.tborgguid.AccessibleName = resources.GetString("tborgguid.AccessibleName");
			this.tborgguid.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tborgguid.Anchor")));
			this.tborgguid.AutoSize = ((bool)(resources.GetObject("tborgguid.AutoSize")));
			this.tborgguid.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tborgguid.BackgroundImage")));
			this.tborgguid.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tborgguid.Dock")));
			this.tborgguid.Enabled = ((bool)(resources.GetObject("tborgguid.Enabled")));
			this.tborgguid.Font = ((System.Drawing.Font)(resources.GetObject("tborgguid.Font")));
			this.tborgguid.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tborgguid.ImeMode")));
			this.tborgguid.Location = ((System.Drawing.Point)(resources.GetObject("tborgguid.Location")));
			this.tborgguid.MaxLength = ((int)(resources.GetObject("tborgguid.MaxLength")));
			this.tborgguid.Multiline = ((bool)(resources.GetObject("tborgguid.Multiline")));
			this.tborgguid.Name = "tborgguid";
			this.tborgguid.PasswordChar = ((char)(resources.GetObject("tborgguid.PasswordChar")));
			this.tborgguid.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tborgguid.RightToLeft")));
			this.tborgguid.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tborgguid.ScrollBars")));
			this.tborgguid.Size = ((System.Drawing.Size)(resources.GetObject("tborgguid.Size")));
			this.tborgguid.TabIndex = ((int)(resources.GetObject("tborgguid.TabIndex")));
			this.tborgguid.Text = resources.GetString("tborgguid.Text");
			this.tborgguid.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tborgguid.TextAlign")));
			this.tborgguid.Visible = ((bool)(resources.GetObject("tborgguid.Visible")));
			this.tborgguid.WordWrap = ((bool)(resources.GetObject("tborgguid.WordWrap")));
			this.tborgguid.TextChanged += new System.EventHandler(this.SetGuid);
			// 
			// llgetGUID
			// 
			this.llgetGUID.AccessibleDescription = resources.GetString("llgetGUID.AccessibleDescription");
			this.llgetGUID.AccessibleName = resources.GetString("llgetGUID.AccessibleName");
			this.llgetGUID.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("llgetGUID.Anchor")));
			this.llgetGUID.AutoSize = ((bool)(resources.GetObject("llgetGUID.AutoSize")));
			this.llgetGUID.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("llgetGUID.Dock")));
			this.llgetGUID.Enabled = ((bool)(resources.GetObject("llgetGUID.Enabled")));
			this.llgetGUID.Font = ((System.Drawing.Font)(resources.GetObject("llgetGUID.Font")));
			this.llgetGUID.Image = ((System.Drawing.Image)(resources.GetObject("llgetGUID.Image")));
			this.llgetGUID.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("llgetGUID.ImageAlign")));
			this.llgetGUID.ImageIndex = ((int)(resources.GetObject("llgetGUID.ImageIndex")));
			this.llgetGUID.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("llgetGUID.ImeMode")));
			this.llgetGUID.LinkArea = ((System.Windows.Forms.LinkArea)(resources.GetObject("llgetGUID.LinkArea")));
			this.llgetGUID.Location = ((System.Drawing.Point)(resources.GetObject("llgetGUID.Location")));
			this.llgetGUID.Name = "llgetGUID";
			this.llgetGUID.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("llgetGUID.RightToLeft")));
			this.llgetGUID.Size = ((System.Drawing.Size)(resources.GetObject("llgetGUID.Size")));
			this.llgetGUID.TabIndex = ((int)(resources.GetObject("llgetGUID.TabIndex")));
			this.llgetGUID.TabStop = true;
			this.llgetGUID.Text = resources.GetString("llgetGUID.Text");
			this.llgetGUID.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("llgetGUID.TextAlign")));
			this.llgetGUID.Visible = ((bool)(resources.GetObject("llgetGUID.Visible")));
			this.llgetGUID.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.GetGuid);
			// 
			// llcommitobjd
			// 
			this.llcommitobjd.AccessibleDescription = resources.GetString("llcommitobjd.AccessibleDescription");
			this.llcommitobjd.AccessibleName = resources.GetString("llcommitobjd.AccessibleName");
			this.llcommitobjd.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("llcommitobjd.Anchor")));
			this.llcommitobjd.AutoSize = ((bool)(resources.GetObject("llcommitobjd.AutoSize")));
			this.llcommitobjd.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("llcommitobjd.Dock")));
			this.llcommitobjd.Enabled = ((bool)(resources.GetObject("llcommitobjd.Enabled")));
			this.llcommitobjd.Font = ((System.Drawing.Font)(resources.GetObject("llcommitobjd.Font")));
			this.llcommitobjd.Image = ((System.Drawing.Image)(resources.GetObject("llcommitobjd.Image")));
			this.llcommitobjd.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("llcommitobjd.ImageAlign")));
			this.llcommitobjd.ImageIndex = ((int)(resources.GetObject("llcommitobjd.ImageIndex")));
			this.llcommitobjd.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("llcommitobjd.ImeMode")));
			this.llcommitobjd.LinkArea = ((System.Windows.Forms.LinkArea)(resources.GetObject("llcommitobjd.LinkArea")));
			this.llcommitobjd.Location = ((System.Drawing.Point)(resources.GetObject("llcommitobjd.Location")));
			this.llcommitobjd.Name = "llcommitobjd";
			this.llcommitobjd.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("llcommitobjd.RightToLeft")));
			this.llcommitobjd.Size = ((System.Drawing.Size)(resources.GetObject("llcommitobjd.Size")));
			this.llcommitobjd.TabIndex = ((int)(resources.GetObject("llcommitobjd.TabIndex")));
			this.llcommitobjd.TabStop = true;
			this.llcommitobjd.Text = resources.GetString("llcommitobjd.Text");
			this.llcommitobjd.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("llcommitobjd.TextAlign")));
			this.llcommitobjd.Visible = ((bool)(resources.GetObject("llcommitobjd.Visible")));
			this.llcommitobjd.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Commit);
			// 
			// label65
			// 
			this.label65.AccessibleDescription = resources.GetString("label65.AccessibleDescription");
			this.label65.AccessibleName = resources.GetString("label65.AccessibleName");
			this.label65.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label65.Anchor")));
			this.label65.AutoSize = ((bool)(resources.GetObject("label65.AutoSize")));
			this.label65.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label65.Dock")));
			this.label65.Enabled = ((bool)(resources.GetObject("label65.Enabled")));
			this.label65.Font = ((System.Drawing.Font)(resources.GetObject("label65.Font")));
			this.label65.Image = ((System.Drawing.Image)(resources.GetObject("label65.Image")));
			this.label65.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label65.ImageAlign")));
			this.label65.ImageIndex = ((int)(resources.GetObject("label65.ImageIndex")));
			this.label65.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label65.ImeMode")));
			this.label65.Location = ((System.Drawing.Point)(resources.GetObject("label65.Location")));
			this.label65.Name = "label65";
			this.label65.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label65.RightToLeft")));
			this.label65.Size = ((System.Drawing.Size)(resources.GetObject("label65.Size")));
			this.label65.TabIndex = ((int)(resources.GetObject("label65.TabIndex")));
			this.label65.Text = resources.GetString("label65.Text");
			this.label65.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label65.TextAlign")));
			this.label65.Visible = ((bool)(resources.GetObject("label65.Visible")));
			// 
			// tbflname
			// 
			this.tbflname.AccessibleDescription = resources.GetString("tbflname.AccessibleDescription");
			this.tbflname.AccessibleName = resources.GetString("tbflname.AccessibleName");
			this.tbflname.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbflname.Anchor")));
			this.tbflname.AutoSize = ((bool)(resources.GetObject("tbflname.AutoSize")));
			this.tbflname.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbflname.BackgroundImage")));
			this.tbflname.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbflname.Dock")));
			this.tbflname.Enabled = ((bool)(resources.GetObject("tbflname.Enabled")));
			this.tbflname.Font = ((System.Drawing.Font)(resources.GetObject("tbflname.Font")));
			this.tbflname.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbflname.ImeMode")));
			this.tbflname.Location = ((System.Drawing.Point)(resources.GetObject("tbflname.Location")));
			this.tbflname.MaxLength = ((int)(resources.GetObject("tbflname.MaxLength")));
			this.tbflname.Multiline = ((bool)(resources.GetObject("tbflname.Multiline")));
			this.tbflname.Name = "tbflname";
			this.tbflname.PasswordChar = ((char)(resources.GetObject("tbflname.PasswordChar")));
			this.tbflname.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbflname.RightToLeft")));
			this.tbflname.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbflname.ScrollBars")));
			this.tbflname.Size = ((System.Drawing.Size)(resources.GetObject("tbflname.Size")));
			this.tbflname.TabIndex = ((int)(resources.GetObject("tbflname.TabIndex")));
			this.tbflname.Text = resources.GetString("tbflname.Text");
			this.tbflname.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbflname.TextAlign")));
			this.tbflname.Visible = ((bool)(resources.GetObject("tbflname.Visible")));
			this.tbflname.WordWrap = ((bool)(resources.GetObject("tbflname.WordWrap")));
			this.tbflname.TextChanged += new System.EventHandler(this.SetFlName);
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
			// 
			// tbguid
			// 
			this.tbguid.AccessibleDescription = resources.GetString("tbguid.AccessibleDescription");
			this.tbguid.AccessibleName = resources.GetString("tbguid.AccessibleName");
			this.tbguid.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbguid.Anchor")));
			this.tbguid.AutoSize = ((bool)(resources.GetObject("tbguid.AutoSize")));
			this.tbguid.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbguid.BackgroundImage")));
			this.tbguid.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbguid.Dock")));
			this.tbguid.Enabled = ((bool)(resources.GetObject("tbguid.Enabled")));
			this.tbguid.Font = ((System.Drawing.Font)(resources.GetObject("tbguid.Font")));
			this.tbguid.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbguid.ImeMode")));
			this.tbguid.Location = ((System.Drawing.Point)(resources.GetObject("tbguid.Location")));
			this.tbguid.MaxLength = ((int)(resources.GetObject("tbguid.MaxLength")));
			this.tbguid.Multiline = ((bool)(resources.GetObject("tbguid.Multiline")));
			this.tbguid.Name = "tbguid";
			this.tbguid.PasswordChar = ((char)(resources.GetObject("tbguid.PasswordChar")));
			this.tbguid.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbguid.RightToLeft")));
			this.tbguid.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbguid.ScrollBars")));
			this.tbguid.Size = ((System.Drawing.Size)(resources.GetObject("tbguid.Size")));
			this.tbguid.TabIndex = ((int)(resources.GetObject("tbguid.TabIndex")));
			this.tbguid.Text = resources.GetString("tbguid.Text");
			this.tbguid.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbguid.TextAlign")));
			this.tbguid.Visible = ((bool)(resources.GetObject("tbguid.Visible")));
			this.tbguid.WordWrap = ((bool)(resources.GetObject("tbguid.WordWrap")));
			this.tbguid.TextChanged += new System.EventHandler(this.SetGuid);
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
			this.panel6.Controls.Add(this.label12);
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
			// 
			// linkLabel1
			// 
			this.linkLabel1.AccessibleDescription = resources.GetString("linkLabel1.AccessibleDescription");
			this.linkLabel1.AccessibleName = resources.GetString("linkLabel1.AccessibleName");
			this.linkLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("linkLabel1.Anchor")));
			this.linkLabel1.AutoSize = ((bool)(resources.GetObject("linkLabel1.AutoSize")));
			this.linkLabel1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("linkLabel1.Dock")));
			this.linkLabel1.Enabled = ((bool)(resources.GetObject("linkLabel1.Enabled")));
			this.linkLabel1.Font = ((System.Drawing.Font)(resources.GetObject("linkLabel1.Font")));
			this.linkLabel1.Image = ((System.Drawing.Image)(resources.GetObject("linkLabel1.Image")));
			this.linkLabel1.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("linkLabel1.ImageAlign")));
			this.linkLabel1.ImageIndex = ((int)(resources.GetObject("linkLabel1.ImageIndex")));
			this.linkLabel1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("linkLabel1.ImeMode")));
			this.linkLabel1.LinkArea = ((System.Windows.Forms.LinkArea)(resources.GetObject("linkLabel1.LinkArea")));
			this.linkLabel1.Location = ((System.Drawing.Point)(resources.GetObject("linkLabel1.Location")));
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("linkLabel1.RightToLeft")));
			this.linkLabel1.Size = ((System.Drawing.Size)(resources.GetObject("linkLabel1.Size")));
			this.linkLabel1.TabIndex = ((int)(resources.GetObject("linkLabel1.TabIndex")));
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = resources.GetString("linkLabel1.Text");
			this.linkLabel1.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("linkLabel1.TextAlign")));
			this.linkLabel1.Visible = ((bool)(resources.GetObject("linkLabel1.Visible")));
			this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.UpdateMMAT);
			// 
			// cbcareer
			// 
			this.cbcareer.AccessibleDescription = resources.GetString("cbcareer.AccessibleDescription");
			this.cbcareer.AccessibleName = resources.GetString("cbcareer.AccessibleName");
			this.cbcareer.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cbcareer.Anchor")));
			this.cbcareer.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("cbcareer.Appearance")));
			this.cbcareer.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cbcareer.BackgroundImage")));
			this.cbcareer.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbcareer.CheckAlign")));
			this.cbcareer.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cbcareer.Dock")));
			this.cbcareer.Enabled = ((bool)(resources.GetObject("cbcareer.Enabled")));
			this.cbcareer.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("cbcareer.FlatStyle")));
			this.cbcareer.Font = ((System.Drawing.Font)(resources.GetObject("cbcareer.Font")));
			this.cbcareer.Image = ((System.Drawing.Image)(resources.GetObject("cbcareer.Image")));
			this.cbcareer.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbcareer.ImageAlign")));
			this.cbcareer.ImageIndex = ((int)(resources.GetObject("cbcareer.ImageIndex")));
			this.cbcareer.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cbcareer.ImeMode")));
			this.cbcareer.Location = ((System.Drawing.Point)(resources.GetObject("cbcareer.Location")));
			this.cbcareer.Name = "cbcareer";
			this.cbcareer.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cbcareer.RightToLeft")));
			this.cbcareer.Size = ((System.Drawing.Size)(resources.GetObject("cbcareer.Size")));
			this.cbcareer.TabIndex = ((int)(resources.GetObject("cbcareer.TabIndex")));
			this.cbcareer.Text = resources.GetString("cbcareer.Text");
			this.cbcareer.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbcareer.TextAlign")));
			this.cbcareer.Visible = ((bool)(resources.GetObject("cbcareer.Visible")));
			this.cbcareer.CheckedChanged += new System.EventHandler(this.SetFunctionFlags);
			// 
			// ExtObjdForm
			// 
			this.AccessibleDescription = resources.GetString("$this.AccessibleDescription");
			this.AccessibleName = resources.GetString("$this.AccessibleName");
			this.AutoScaleBaseSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScaleBaseSize")));
			this.AutoScroll = ((bool)(resources.GetObject("$this.AutoScroll")));
			this.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMargin")));
			this.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMinSize")));
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.ClientSize = ((System.Drawing.Size)(resources.GetObject("$this.ClientSize")));
			this.Controls.Add(this.extObjdPanel);
			this.Enabled = ((bool)(resources.GetObject("$this.Enabled")));
			this.Font = ((System.Drawing.Font)(resources.GetObject("$this.Font")));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("$this.ImeMode")));
			this.Location = ((System.Drawing.Point)(resources.GetObject("$this.Location")));
			this.MaximumSize = ((System.Drawing.Size)(resources.GetObject("$this.MaximumSize")));
			this.MinimumSize = ((System.Drawing.Size)(resources.GetObject("$this.MinimumSize")));
			this.Name = "ExtObjdForm";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.StartPosition = ((System.Windows.Forms.FormStartPosition)(resources.GetObject("$this.StartPosition")));
			this.Text = resources.GetString("$this.Text");
			this.extObjdPanel.ResumeLayout(false);
			this.tc.ResumeLayout(false);
			this.tpcatalogsort.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.tpraw.ResumeLayout(false);
			this.panel6.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void GetGuid(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			Sims.GUID.GUIDGetterForm form = new Sims.GUID.GUIDGetterForm();
			Registry reg = new Registry();

			try 
			{
				uint guid = form.GetNewGUID(reg.Username, reg.Password, this.wrapper.Guid);

				reg.Username = form.tbusername.Text;
				reg.Password = form.tbpassword.Text;
				this.tbguid.Text = "0x"+Helper.HexString(guid);				
			} 
			catch (Exception ex) {
				if (Helper.DebugMode) Helper.ExceptionMessage("", ex);
			}
		}

		private void ChangeType(object sender, System.EventArgs e)
		{
			if (this.Tag!=null) return;
			this.Tag = true;

			try 
			{
				if (cbtype.SelectedIndex<0) return;
				Data.ObjectTypes ot = (Data.ObjectTypes)cbtype.Items[cbtype.SelectedIndex];
				tbtype.Text = "0x"+Helper.HexString((ushort)ot);

				wrapper.Type = ot;
				wrapper.Changed = true;

				if (this.pg.SelectedObject!=null) 
				{
					UpdateData();
					ShowData();
				}
			} 
			finally 
			{
				this.Tag = null;
			}
		}

		private void SetRoomFlags(object sender, System.EventArgs e)
		{
			if (this.Tag!=null) return;
			this.Tag = true;

			try 
			{
				wrapper.RoomSort.InBathroom = cbbathroom.Checked;
				wrapper.RoomSort.InBedroom = cbbedroom.Checked;
				wrapper.RoomSort.InDiningRoom = cbdinigroom.Checked;
				wrapper.RoomSort.InKitchen = cbkitchen.Checked;
				wrapper.RoomSort.InLivingRoom = cblivingroom.Checked;
				wrapper.RoomSort.InMisc = cbmisc.Checked;
				wrapper.RoomSort.InOutside = cboutside.Checked;
				wrapper.RoomSort.InStudy = cbstudy.Checked;
				wrapper.RoomSort.InKids = cbkids.Checked;

				wrapper.Changed = true;				
			}
			finally 
			{
				this.Tag = null;
			}
		}

		private void SetFunctionFlags(object sender, System.EventArgs e)
		{
			if (this.Tag!=null) return;
			this.Tag = true;

			try 
			{
				wrapper.FunctionSort.InAppliances = this.cbappliances.Checked;
				wrapper.FunctionSort.InDecorative = this.cbdecorative.Checked;
				wrapper.FunctionSort.InElectronics = this.cbelectronics.Checked;
				wrapper.FunctionSort.InGeneral = this.cbgeneral.Checked;
				wrapper.FunctionSort.InLighting = this.cblightning.Checked;
				wrapper.FunctionSort.InPlumbing = this.cbplumbing.Checked;
				wrapper.FunctionSort.InSeating = this.cbseating.Checked;
				wrapper.FunctionSort.InSurfaces = this.cbsurfaces.Checked;
				wrapper.FunctionSort.InHobbies = this.cbhobby.Checked;
				wrapper.FunctionSort.InAspirationRewards = this.cbaspiration.Checked;
				wrapper.FunctionSort.InCareerRewards = this.cbcareer.Checked;

				wrapper.Changed = true;
			} 
			finally 
			{
				this.Tag = null;
			}
		}

		private void SetGuid(object sender, System.EventArgs e)
		{
			if (this.Tag!=null) return;
			this.Tag = true;

			try 
			{
				wrapper.Guid = Convert.ToUInt32(tbguid.Text, 16);
				wrapper.ProxyGuid = Convert.ToUInt32(this.tbproxguid.Text, 16);
				wrapper.OriginalGuid = Convert.ToUInt32(this.tborgguid.Text, 16);
				wrapper.Changed = true;
			} 
			catch (Exception){}
			finally 
			{
				this.Tag = null;
			}
		}

		private void Commit(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{	
			if (this.pg.SelectedObject!=null) UpdateData();
			wrapper.SynchronizeUserData();
		}

		private void UpdateMMAT(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			if ((wrapper.Guid!=initialguid) || (cball.Checked))
			{
				SimPe.Plugin.FixGuid fg = new SimPe.Plugin.FixGuid(wrapper.Package);
				if (cball.Checked) 
				{
					fg.FixGuids(wrapper.Guid);
				} 
				else 
				{
					ArrayList al = new ArrayList();
					SimPe.Plugin.GuidSet gs = new SimPe.Plugin.GuidSet();
					gs.oldguid = initialguid;
					gs.guid = wrapper.Guid;
					al.Add(gs);

					fg.FixGuids(al);
				}
				initialguid = wrapper.Guid;
			}

			wrapper.SynchronizeUserData();
		}

		private void CangedTab(object sender, System.EventArgs e)
		{
			if (tc.SelectedTab == tpraw)
			{
				rbhex.Checked = (Ambertation.BaseChangeShort.DigitBase==16);
				rbbin.Checked = (Ambertation.BaseChangeShort.DigitBase==2);
				rbdec.Checked = (!rbhex.Checked && !rbbin.Checked);

				if (this.pg.SelectedObject==null) ShowData();
			} 
			else 
			{
				if (this.pg.SelectedObject!=null) UpdateData();
				this.pg.SelectedObject = null;
			}
		}

		private void PropChanged(object s, System.Windows.Forms.PropertyValueChangedEventArgs e)
		{
			propchanged = true;
		}

		private void SetFlName(object sender, System.EventArgs e)
		{
			if (this.Tag!=null) return;
			wrapper.FileName = tbflname.Text;
			wrapper.Changed = true;
		}

		private void DigitChanged(object sender, System.EventArgs e)
		{
			if (rbhex.Checked) Ambertation.BaseChangeShort.DigitBase = 16;
			else if (rbbin.Checked) Ambertation.BaseChangeShort.DigitBase = 2;			
			else Ambertation.BaseChangeShort.DigitBase = 10;

			this.pg.Refresh();		
		}
	}
}
