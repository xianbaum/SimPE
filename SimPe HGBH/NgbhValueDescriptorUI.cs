using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace SimPe.Plugin
{
	/// <summary>
	/// Summary description for NgbhValueDescriptorUI.
	/// </summary>
	[System.ComponentModel.DefaultEvent("AddedNewItem")]
	public class NgbhValueDescriptorUI : System.Windows.Forms.UserControl
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public NgbhValueDescriptorUI()
		{
			SetStyle(
				ControlStyles.SupportsTransparentBackColor |
				ControlStyles.AllPaintingInWmPaint |
				ControlStyles.UserPaint |
				ControlStyles.ResizeRedraw 
				| ControlStyles.DoubleBuffer
				,true);
			// Required designer variable.
			InitializeComponent();

            if (booby.ThemeManager.ThemedForms) booby.ThemeManager.Global.AddControl(this.pb);

			SetContent();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NgbhValueDescriptorUI));
            this.panel1 = new System.Windows.Forms.Panel();
            this.pb = new booby.LabeledProgressBar();
            this.lbcolour = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cb = new System.Windows.Forms.CheckBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lb = new System.Windows.Forms.Label();
            this.ll = new System.Windows.Forms.LinkLabel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pb);
            this.panel1.Controls.Add(this.lbcolour);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // pb
            // 
            this.pb.DisplayOffset = 0;
            resources.ApplyResources(this.pb, "pb");
            this.pb.LabelAlignment = System.Windows.Forms.DockStyle.Bottom;
            this.pb.LabelFont = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pb.Maximum = 100;
            this.pb.Name = "pb";
            this.pb.NumberFormat = "N0";
            this.pb.NumberOffset = 0;
            this.pb.NumberScale = 1;
            this.pb.Style = booby.ProgresBarStyle.Increase;
            this.pb.TextboxWidth = 40;
            this.pb.TokenCount = 10;
            this.pb.Value = 0;
            this.pb.Changed += new System.EventHandler(this.pb_Changed);
            // 
            // lbcolour
            // 
            resources.ApplyResources(this.lbcolour, "lbcolour");
            this.lbcolour.BackColor = System.Drawing.Color.Transparent;
            this.lbcolour.Name = "lbcolour";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.cb);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // cb
            // 
            resources.ApplyResources(this.cb, "cb");
            this.cb.Name = "cb";
            this.cb.CheckedChanged += new System.EventHandler(this.cb_CheckedChanged);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.lb);
            this.panel3.Controls.Add(this.ll);
            resources.ApplyResources(this.panel3, "panel3");
            this.panel3.Name = "panel3";
            // 
            // lb
            // 
            resources.ApplyResources(this.lb, "lb");
            this.lb.Name = "lb";
            // 
            // ll
            // 
            resources.ApplyResources(this.ll, "ll");
            this.ll.Name = "ll";
            this.ll.TabStop = true;
            this.ll.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ll_LinkClicked);
            // 
            // NgbhValueDescriptorUI
            // 
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            resources.ApplyResources(this, "$this");
            this.Name = "NgbhValueDescriptorUI";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		NgbhSlot slot;
		[System.ComponentModel.Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public NgbhSlot Slot
		{
			get {return slot;}
			set 
			{
				slot = value;
				SetContent();				
			}
		}

		NgbhValueDescriptor des;
		private System.Windows.Forms.Panel panel1;
        private booby.LabeledProgressBar pb;
		private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox cb;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.LinkLabel ll;
        private System.Windows.Forms.Label lb;
        private Label lbcolour;
		
				
		[System.ComponentModel.Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public NgbhValueDescriptor NgbhValueDescriptor
		{
			get {return des;}
			set 
			{
				des = value;
				SetContent();				
			}
		}

		NgbhValueDescriptorSelection vds;
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public NgbhValueDescriptorSelection NgbhValueDescriptorSelection
		{
			get {return vds;}
			set 
			{
				if (vds!=null) vds.SelectedDescriptorChanged -= new EventHandler(vds_SelectedDescriptorChanged);
				vds = value;
				if (vds!=null) vds.SelectedDescriptorChanged += new EventHandler(vds_SelectedDescriptorChanged);
			}
		}

		void SetVisible()
		{
			panel1.Visible = item!=null;
			if (des!=null)
				panel2.Visible = des.HasComplededFlag && item!=null;
			else
				panel2.Visible = false;
			
			panel3.Visible = des!=null && item==null;
		}

		NgbhItem item;
		bool inter;
		void SetContent()
		{
			if (inter) return;
			inter = true;
			if (des!=null && slot!=null)
			{
				item = slot.FindItem(des.Guid);
				pb.NumberOffset = des.Minimum;
				pb.Maximum = des.Maximum;				
				
				if (item!=null) 			
				{	
					pb.Value = item.GetValue(des.DataNumber);
					if (des.HasComplededFlag)
						cb.Checked = item.GetValue(des.CompletedDataNumber)!=0;
				}
				else
					lb.Text = des.ToString();

				this.Enabled = true;
			} 	
			else 
			{
				this.Enabled = false;
			}

			SetVisible();
			inter = false;
		}

		private void vds_SelectedDescriptorChanged(object sender, EventArgs e)
		{
			this.NgbhValueDescriptor = vds.SelectedDescriptor;
		}

		protected override void OnResize(EventArgs e)
		{
			base.OnResize (e);
			pb.TokenCount = 10;			
		}

		public event EventHandler AddedNewItem;
		public event EventHandler ChangedItem;

		private void ll_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			if (item!=null) return;
			if (slot==null) return;
			if (des==null) return;
			
			if (des.Intern) item = slot.ItemsA.AddNew(SimMemoryType.Skill);
			else item = slot.ItemsB.AddNew(SimMemoryType.Skill);

			item.Guid = des.Guid;
			item.PutValue(des.DataNumber, 0);
			if (des.HasComplededFlag) item.PutValue(des.CompletedDataNumber, 0);
								
			SetContent();

			if (AddedNewItem!=null) AddedNewItem(this, new EventArgs());
		}

		private void cb_CheckedChanged(object sender, System.EventArgs e)
		{
			if (inter) return;
			if (item==null) return;
			if (des==null) return;
			if (!des.HasComplededFlag) return;

			if (cb.Checked) item.PutValue(des.CompletedDataNumber, 1);
			else item.PutValue(des.CompletedDataNumber, 0);

			if (ChangedItem!=null) ChangedItem(this, new EventArgs());
		}

		private void pb_Changed(object sender, System.EventArgs e)
		{
			if (item==null) return;
            if (des == null) return;
            if (pb.Maximum == 1000 && des.Type == NgbhValueDescriptorType.Badge)
            {
                this.lbcolour.Visible = true;
                if (this.pb.Value < 333) this.lbcolour.Text = "none";
                else if (this.pb.Value < 666) this.lbcolour.Text = "Bronze";
                else if (this.pb.Value < 999) this.lbcolour.Text = "Silver";
                else this.lbcolour.Text = "Gold";
            }
            else
                this.lbcolour.Visible = false;
            if (inter) return;
			item.PutValue(des.DataNumber, (ushort)pb.Value);			
			if (ChangedItem!=null) ChangedItem(this, new EventArgs());
		}
	}
}
