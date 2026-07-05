using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace SimPe.Plugin
{
	/// <summary>
	/// Summary description for NgbhSkillHelper.
	/// </summary>
	[System.ComponentModel.DefaultEvent("AddedNewItem")]
	public class NgbhSkillHelper : System.Windows.Forms.UserControl
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

        booby.ThemeManager tm;
		public NgbhSkillHelper()
		{
			SetStyle(
				ControlStyles.SupportsTransparentBackColor |
				ControlStyles.AllPaintingInWmPaint |
				//ControlStyles.Opaque |
				ControlStyles.UserPaint |
				ControlStyles.ResizeRedraw 
				| ControlStyles.DoubleBuffer
				,true);
			// Required designer variable.
			InitializeComponent();	
		
			try 
			{
				tm = booby.ThemeManager.Global.CreateChild();
				tm.AddControl(this.xpBadges);
				tm.AddControl(this.xpSkills);

                this.xpBadges.Visible = (SimPe.PathProvider.Global.EPInstalled >= 3 || SimPe.PathProvider.Global.STInstalled >= 28);
				SetContent();
			} 
			catch {}
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (tm!=null) 
				{
					tm.Clear();
					tm.Parent = null;
					tm = null;
				}
				
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NgbhSkillHelper));
            this.badges = new SimPe.Plugin.NgbhSkillHelperElement();
            this.xpBadges = new booby.TaskBox();
            this.xpSkills = new booby.TaskBox();
            this.skills = new SimPe.Plugin.NgbhSkillHelperElement();
            this.xpBadges.SuspendLayout();
            this.xpSkills.SuspendLayout();
            this.SuspendLayout();
            // 
            // badges
            // 
            resources.ApplyResources(this.badges, "badges");
            this.badges.Name = "badges";
            this.badges.NgbhResource = null;
            this.badges.ShowBadges = true;
            this.badges.ShowSkills = false;
            this.badges.ShowToddlerSkills = false;
            this.badges.Slot = null;
            this.badges.ChangedItem += new System.EventHandler(this.skills_ChangedItem);
            this.badges.AddedNewItem += new System.EventHandler(this.skills_AddedNewItem);
            // 
            // xpBadges
            // 
            this.xpBadges.Controls.Add(this.badges);
            resources.ApplyResources(this.xpBadges, "xpBadges");
            this.xpBadges.HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Bold);
            this.xpBadges.IconLocation = new System.Drawing.Point(4, 0);
            this.xpBadges.IconSize = new System.Drawing.Size(48, 48);
            this.xpBadges.Name = "xpBadges";
            // 
            // xpSkills
            // 
            this.xpSkills.Controls.Add(this.skills);
            resources.ApplyResources(this.xpSkills, "xpSkills");
            this.xpSkills.HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Bold);
            this.xpSkills.IconLocation = new System.Drawing.Point(4, 0);
            this.xpSkills.IconSize = new System.Drawing.Size(48, 48);
            this.xpSkills.Name = "xpSkills";
            // 
            // skills
            // 
            resources.ApplyResources(this.skills, "skills");
            this.skills.Name = "skills";
            this.skills.NgbhResource = null;
            this.skills.ShowBadges = false;
            this.skills.ShowSkills = true;
            this.skills.ShowToddlerSkills = true;
            this.skills.Slot = null;
            this.skills.ChangedItem += new System.EventHandler(this.skills_ChangedItem);
            this.skills.AddedNewItem += new System.EventHandler(this.skills_AddedNewItem);
            // 
            // NgbhSkillHelper
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.xpSkills);
            this.Controls.Add(this.xpBadges);
            this.Name = "NgbhSkillHelper";
            this.xpBadges.ResumeLayout(false);
            this.xpSkills.ResumeLayout(false);
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

		Ngbh ngbh;
		private SimPe.Plugin.NgbhSkillHelperElement badges;
		private SimPe.Plugin.NgbhSkillHelperElement skills;
        private booby.TaskBox xpBadges;
        private booby.TaskBox xpSkills;
				
		[System.ComponentModel.Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Ngbh NgbhResource
		{
			get {return ngbh;}
			set 
			{
				ngbh = value;
				pc_SelectedSimChanged(pc, null, null);
				SetContent();				
			}
		}

		SimPe.PackedFiles.Wrapper.SimPoolControl pc;
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public SimPe.PackedFiles.Wrapper.SimPoolControl SimPoolControl
		{
			get {return pc;}
			set 
			{
				if (pc!=null) pc.SelectedSimChanged -= new SimPe.PackedFiles.Wrapper.SimPoolControl.SelectedSimHandler(pc_SelectedSimChanged);
				pc = value;
				
				if (pc!=null) 
				{
					pc.SelectedSimChanged += new SimPe.PackedFiles.Wrapper.SimPoolControl.SelectedSimHandler(pc_SelectedSimChanged);
					pc_SelectedSimChanged(pc, null, null);
				}
			}
		}

		void SetContent()
		{
			badges.Slot = slot;
			skills.Slot = slot;

			if (pc!=null) 
			{
				if (pc.SelectedSim!=null) SetImage(pc.SelectedSim.Image);
				else SetImage(new Bitmap(1,1));
			}
		}

		void SetImage(Image img)
		{
			img = Ambertation.Drawing.GraphicRoutines.KnockoutImage(img, new Point(0), Color.Transparent, true);
			img = Ambertation.Drawing.GraphicRoutines.ScaleImage(img, 48, 48, true);

			this.xpBadges.Icon = img;
			this.xpSkills.Icon = img;			
		}

		private void pc_SelectedSimChanged(object sender, Image thumb, SimPe.PackedFiles.Wrapper.SDesc sdesc)
		{
			
			if (ngbh!=null && pc!=null) 
			{
				
				if (pc.SelectedSim!=null) {
					this.Slot = ngbh.GetSlots(Data.NeighborhoodSlots.SimsIntern).GetInstanceSlot(pc.SelectedSim.FileDescriptor.Instance);	
					SetImage(pc.SelectedSim.Image);
				}
				else 
				{
					this.Slot = null;
					
				}
			}
		}
		

		private void skills_AddedNewItem(object sender, System.EventArgs e)
		{
			if (AddedNewItem!=null) AddedNewItem(this, e);
		}	

		private void skills_ChangedItem(object sender, System.EventArgs e)
		{
			if (ChangedItem!=null) ChangedItem(this, e);
		}
	
		public event EventHandler AddedNewItem;
		public event EventHandler ChangedItem;
	}
}
