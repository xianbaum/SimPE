using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace SimPe
{
	/// <summary>
	/// Summary description for CheckItem.
	/// </summary>
	[System.ComponentModel.DefaultEvent("ClickedFix")]
	public class CheckItem : System.Windows.Forms.UserControl
	{
		public delegate CheckItemState FixEventHandler(object sender, CheckItemState isok);
		private System.Windows.Forms.LinkLabel llfix;
		private System.Windows.Forms.Label lb;
		private System.Windows.Forms.PictureBox pb;
		private System.Windows.Forms.LinkLabel lldet;
		private System.Windows.Forms.Panel pnDetails;
		private System.Windows.Forms.LinkLabel linkLabel1;
		private System.Windows.Forms.RichTextBox rtb;
		//private System.ComponentModel.IContainer components;

		public CheckItem()
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

			cs = CheckItemState.Unknown;
			txt = "--";
			cf = false;		
			det = "";
			this.pnDetails.Visible = false;
			SetContent();
		}

		

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/*protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}*/

		#region Windows Form Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CheckItem));
            this.lb = new System.Windows.Forms.Label();
            this.llfix = new System.Windows.Forms.LinkLabel();
            this.pb = new System.Windows.Forms.PictureBox();
            this.lldet = new System.Windows.Forms.LinkLabel();
            this.pnDetails = new System.Windows.Forms.Panel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.rtb = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pb)).BeginInit();
            this.pnDetails.SuspendLayout();
            this.SuspendLayout();
            // 
            // lb
            // 
            resources.ApplyResources(this.lb, "lb");
            this.lb.Name = "lb";
            // 
            // llfix
            // 
            resources.ApplyResources(this.llfix, "llfix");
            this.llfix.Name = "llfix";
            this.llfix.TabStop = true;
            this.llfix.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llfix_LinkClicked);
            // 
            // pb
            // 
            resources.ApplyResources(this.pb, "pb");
            this.pb.Name = "pb";
            this.pb.TabStop = false;
            // 
            // lldet
            // 
            resources.ApplyResources(this.lldet, "lldet");
            this.lldet.Name = "lldet";
            this.lldet.TabStop = true;
            this.lldet.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lldet_LinkClicked);
            // 
            // pnDetails
            // 
            resources.ApplyResources(this.pnDetails, "pnDetails");
            this.pnDetails.Controls.Add(this.linkLabel1);
            this.pnDetails.Controls.Add(this.rtb);
            this.pnDetails.Name = "pnDetails";
            // 
            // linkLabel1
            // 
            resources.ApplyResources(this.linkLabel1, "linkLabel1");
            this.linkLabel1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.TabStop = true;
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // rtb
            // 
            this.rtb.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.rtb.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtb.Cursor = System.Windows.Forms.Cursors.Default;
            resources.ApplyResources(this.rtb, "rtb");
            this.rtb.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.rtb.Name = "rtb";
            this.rtb.ReadOnly = true;
            // 
            // CheckItem
            // 
            this.Controls.Add(this.pnDetails);
            this.Controls.Add(this.lb);
            this.Controls.Add(this.pb);
            this.Controls.Add(this.llfix);
            this.Controls.Add(this.lldet);
            resources.ApplyResources(this, "$this");
            this.Name = "CheckItem";
            ((System.ComponentModel.ISupportInitialize)(this.pb)).EndInit();
            this.pnDetails.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		bool cf;
		public bool CanFix
		{
			get {return cf;}
			set 
			{
				if (cf!=value)
				{
					cf = value;
					SetContent();
				}
			}
		}

		string txt;
		[System.ComponentModel.Localizable(true)]
		public string Caption
		{
			get {return  txt;}
			set 
			{
				txt = value;
				SetContent();
			}
		}

		CheckItemState cs;
		public CheckItemState CheckState
		{
			get {return cs;}
			set {
				cs = value;
				if (cs==CheckItemState.Fail) pb.Image = CheckControl.FailImage;
				else if (cs==CheckItemState.Ok) pb.Image = CheckControl.OKImage;
				else if (cs==CheckItemState.Warning) pb.Image = CheckControl.WarnImage;
				else pb.Image = CheckControl.UnknownImage;

				SetContent();
			}
		}

		string det;
		public string Details
		{
			get {return det;}
			set 
			{
				det = value;
				SetContent();
			}
		}

		protected virtual void SetContent()
		{
			this.rtb.Text = det;
			lldet.Visible = det.Trim()!="";
			lb.Text = txt;
			this.llfix.Visible = cs==CheckItemState.Fail && cf;
			this.Refresh();
		}

		protected virtual void OnFix()
		{			
		}

		public void Reset()
		{
			this.pnDetails.Visible = false;
			this.CheckState = CheckItemState.Unknown;
			det = "";
			SetContent();
		}

		public event FixEventHandler CalledCheck;
		protected virtual CheckItemState OnCheck()
		{
			return CheckItemState.Ok;
		}

		public void Check()
		{
			CheckItemState res = OnCheck();
			if (CalledCheck!=null) res = CalledCheck(this, res);
			this.CheckState = res;
		}

		public event FixEventHandler ClickedFix;
		private void llfix_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			CheckItemState res = this.CheckState;
			OnFix();
			if (ClickedFix!=null) 			
				res = ClickedFix(this, res);
			
			this.CheckState = res;
		}

		private void lldet_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			this.pnDetails.Visible = true;			
		}

		private void linkLabel1_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			this.pnDetails.Visible = false;
		}
	}
}
