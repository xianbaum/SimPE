using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace SimPe
{
	/// <summary>
	/// Zusammenfassung für Help.
	/// </summary>
	public class Help : System.Windows.Forms.Form
	{
		private System.Windows.Forms.LinkLabel linkLabel1;
		private System.Windows.Forms.LinkLabel linkLabel2;
		private System.Windows.Forms.LinkLabel linkLabel3;
		private System.Windows.Forms.LinkLabel linkLabel4;
		private System.Windows.Forms.LinkLabel linkLabel5;
		private System.Windows.Forms.LinkLabel linkLabel6;
		private System.Windows.Forms.LinkLabel linkLabel7;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.ListBox links;
		private System.Windows.Forms.LinkLabel linkLabel8;
		private System.Windows.Forms.TextBox tbsup;
		/// <summary>
		/// Erforderliche Designervariable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Help()
		{
			//
			// Erforderlich für die Windows Form-Designerunterstützung
			//
			InitializeComponent();
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Help));
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
			this.linkLabel2 = new System.Windows.Forms.LinkLabel();
			this.linkLabel3 = new System.Windows.Forms.LinkLabel();
			this.linkLabel4 = new System.Windows.Forms.LinkLabel();
			this.linkLabel5 = new System.Windows.Forms.LinkLabel();
			this.linkLabel6 = new System.Windows.Forms.LinkLabel();
			this.linkLabel7 = new System.Windows.Forms.LinkLabel();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.button1 = new System.Windows.Forms.Button();
			this.links = new System.Windows.Forms.ListBox();
			this.linkLabel8 = new System.Windows.Forms.LinkLabel();
			this.tbsup = new System.Windows.Forms.TextBox();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// linkLabel1
			// 
			this.linkLabel1.AccessibleDescription = resources.GetString("linkLabel1.AccessibleDescription");
			this.linkLabel1.AccessibleName = resources.GetString("linkLabel1.AccessibleName");
			this.linkLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("linkLabel1.Anchor")));
			this.linkLabel1.AutoSize = ((bool)(resources.GetObject("linkLabel1.AutoSize")));
			this.linkLabel1.Cursor = System.Windows.Forms.Cursors.Default;
			this.linkLabel1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("linkLabel1.Dock")));
			this.linkLabel1.Enabled = ((bool)(resources.GetObject("linkLabel1.Enabled")));
			this.linkLabel1.Font = ((System.Drawing.Font)(resources.GetObject("linkLabel1.Font")));
			this.linkLabel1.ForeColor = System.Drawing.Color.Red;
			this.linkLabel1.Image = ((System.Drawing.Image)(resources.GetObject("linkLabel1.Image")));
			this.linkLabel1.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("linkLabel1.ImageAlign")));
			this.linkLabel1.ImageIndex = ((int)(resources.GetObject("linkLabel1.ImageIndex")));
			this.linkLabel1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("linkLabel1.ImeMode")));
			this.linkLabel1.LinkArea = ((System.Windows.Forms.LinkArea)(resources.GetObject("linkLabel1.LinkArea")));
			this.linkLabel1.LinkColor = System.Drawing.Color.Firebrick;
			this.linkLabel1.Location = ((System.Drawing.Point)(resources.GetObject("linkLabel1.Location")));
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("linkLabel1.RightToLeft")));
			this.linkLabel1.Size = ((System.Drawing.Size)(resources.GetObject("linkLabel1.Size")));
			this.linkLabel1.TabIndex = ((int)(resources.GetObject("linkLabel1.TabIndex")));
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = resources.GetString("linkLabel1.Text");
			this.linkLabel1.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("linkLabel1.TextAlign")));
			this.linkLabel1.Visible = ((bool)(resources.GetObject("linkLabel1.Visible")));
			this.linkLabel1.VisitedLinkColor = System.Drawing.Color.DarkRed;
			this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.GoLink);
			// 
			// linkLabel2
			// 
			this.linkLabel2.AccessibleDescription = resources.GetString("linkLabel2.AccessibleDescription");
			this.linkLabel2.AccessibleName = resources.GetString("linkLabel2.AccessibleName");
			this.linkLabel2.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("linkLabel2.Anchor")));
			this.linkLabel2.AutoSize = ((bool)(resources.GetObject("linkLabel2.AutoSize")));
			this.linkLabel2.Cursor = System.Windows.Forms.Cursors.Default;
			this.linkLabel2.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("linkLabel2.Dock")));
			this.linkLabel2.Enabled = ((bool)(resources.GetObject("linkLabel2.Enabled")));
			this.linkLabel2.Font = ((System.Drawing.Font)(resources.GetObject("linkLabel2.Font")));
			this.linkLabel2.ForeColor = System.Drawing.Color.Red;
			this.linkLabel2.Image = ((System.Drawing.Image)(resources.GetObject("linkLabel2.Image")));
			this.linkLabel2.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("linkLabel2.ImageAlign")));
			this.linkLabel2.ImageIndex = ((int)(resources.GetObject("linkLabel2.ImageIndex")));
			this.linkLabel2.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("linkLabel2.ImeMode")));
			this.linkLabel2.LinkArea = ((System.Windows.Forms.LinkArea)(resources.GetObject("linkLabel2.LinkArea")));
			this.linkLabel2.LinkColor = System.Drawing.Color.Firebrick;
			this.linkLabel2.Location = ((System.Drawing.Point)(resources.GetObject("linkLabel2.Location")));
			this.linkLabel2.Name = "linkLabel2";
			this.linkLabel2.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("linkLabel2.RightToLeft")));
			this.linkLabel2.Size = ((System.Drawing.Size)(resources.GetObject("linkLabel2.Size")));
			this.linkLabel2.TabIndex = ((int)(resources.GetObject("linkLabel2.TabIndex")));
			this.linkLabel2.TabStop = true;
			this.linkLabel2.Text = resources.GetString("linkLabel2.Text");
			this.linkLabel2.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("linkLabel2.TextAlign")));
			this.linkLabel2.Visible = ((bool)(resources.GetObject("linkLabel2.Visible")));
			this.linkLabel2.VisitedLinkColor = System.Drawing.Color.DarkRed;
			this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.GoLink);
			// 
			// linkLabel3
			// 
			this.linkLabel3.AccessibleDescription = resources.GetString("linkLabel3.AccessibleDescription");
			this.linkLabel3.AccessibleName = resources.GetString("linkLabel3.AccessibleName");
			this.linkLabel3.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("linkLabel3.Anchor")));
			this.linkLabel3.AutoSize = ((bool)(resources.GetObject("linkLabel3.AutoSize")));
			this.linkLabel3.Cursor = System.Windows.Forms.Cursors.Default;
			this.linkLabel3.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("linkLabel3.Dock")));
			this.linkLabel3.Enabled = ((bool)(resources.GetObject("linkLabel3.Enabled")));
			this.linkLabel3.Font = ((System.Drawing.Font)(resources.GetObject("linkLabel3.Font")));
			this.linkLabel3.ForeColor = System.Drawing.Color.Red;
			this.linkLabel3.Image = ((System.Drawing.Image)(resources.GetObject("linkLabel3.Image")));
			this.linkLabel3.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("linkLabel3.ImageAlign")));
			this.linkLabel3.ImageIndex = ((int)(resources.GetObject("linkLabel3.ImageIndex")));
			this.linkLabel3.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("linkLabel3.ImeMode")));
			this.linkLabel3.LinkArea = ((System.Windows.Forms.LinkArea)(resources.GetObject("linkLabel3.LinkArea")));
			this.linkLabel3.LinkColor = System.Drawing.Color.Firebrick;
			this.linkLabel3.Location = ((System.Drawing.Point)(resources.GetObject("linkLabel3.Location")));
			this.linkLabel3.Name = "linkLabel3";
			this.linkLabel3.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("linkLabel3.RightToLeft")));
			this.linkLabel3.Size = ((System.Drawing.Size)(resources.GetObject("linkLabel3.Size")));
			this.linkLabel3.TabIndex = ((int)(resources.GetObject("linkLabel3.TabIndex")));
			this.linkLabel3.TabStop = true;
			this.linkLabel3.Text = resources.GetString("linkLabel3.Text");
			this.linkLabel3.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("linkLabel3.TextAlign")));
			this.linkLabel3.Visible = ((bool)(resources.GetObject("linkLabel3.Visible")));
			this.linkLabel3.VisitedLinkColor = System.Drawing.Color.Maroon;
			this.linkLabel3.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.GoLink);
			// 
			// linkLabel4
			// 
			this.linkLabel4.AccessibleDescription = resources.GetString("linkLabel4.AccessibleDescription");
			this.linkLabel4.AccessibleName = resources.GetString("linkLabel4.AccessibleName");
			this.linkLabel4.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("linkLabel4.Anchor")));
			this.linkLabel4.AutoSize = ((bool)(resources.GetObject("linkLabel4.AutoSize")));
			this.linkLabel4.Cursor = System.Windows.Forms.Cursors.Default;
			this.linkLabel4.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("linkLabel4.Dock")));
			this.linkLabel4.Enabled = ((bool)(resources.GetObject("linkLabel4.Enabled")));
			this.linkLabel4.Font = ((System.Drawing.Font)(resources.GetObject("linkLabel4.Font")));
			this.linkLabel4.ForeColor = System.Drawing.Color.Red;
			this.linkLabel4.Image = ((System.Drawing.Image)(resources.GetObject("linkLabel4.Image")));
			this.linkLabel4.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("linkLabel4.ImageAlign")));
			this.linkLabel4.ImageIndex = ((int)(resources.GetObject("linkLabel4.ImageIndex")));
			this.linkLabel4.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("linkLabel4.ImeMode")));
			this.linkLabel4.LinkArea = ((System.Windows.Forms.LinkArea)(resources.GetObject("linkLabel4.LinkArea")));
			this.linkLabel4.LinkColor = System.Drawing.Color.Firebrick;
			this.linkLabel4.Location = ((System.Drawing.Point)(resources.GetObject("linkLabel4.Location")));
			this.linkLabel4.Name = "linkLabel4";
			this.linkLabel4.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("linkLabel4.RightToLeft")));
			this.linkLabel4.Size = ((System.Drawing.Size)(resources.GetObject("linkLabel4.Size")));
			this.linkLabel4.TabIndex = ((int)(resources.GetObject("linkLabel4.TabIndex")));
			this.linkLabel4.TabStop = true;
			this.linkLabel4.Text = resources.GetString("linkLabel4.Text");
			this.linkLabel4.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("linkLabel4.TextAlign")));
			this.linkLabel4.Visible = ((bool)(resources.GetObject("linkLabel4.Visible")));
			this.linkLabel4.VisitedLinkColor = System.Drawing.Color.Maroon;
			this.linkLabel4.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.GoLink);
			// 
			// linkLabel5
			// 
			this.linkLabel5.AccessibleDescription = resources.GetString("linkLabel5.AccessibleDescription");
			this.linkLabel5.AccessibleName = resources.GetString("linkLabel5.AccessibleName");
			this.linkLabel5.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("linkLabel5.Anchor")));
			this.linkLabel5.AutoSize = ((bool)(resources.GetObject("linkLabel5.AutoSize")));
			this.linkLabel5.Cursor = System.Windows.Forms.Cursors.Default;
			this.linkLabel5.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("linkLabel5.Dock")));
			this.linkLabel5.Enabled = ((bool)(resources.GetObject("linkLabel5.Enabled")));
			this.linkLabel5.Font = ((System.Drawing.Font)(resources.GetObject("linkLabel5.Font")));
			this.linkLabel5.ForeColor = System.Drawing.Color.Red;
			this.linkLabel5.Image = ((System.Drawing.Image)(resources.GetObject("linkLabel5.Image")));
			this.linkLabel5.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("linkLabel5.ImageAlign")));
			this.linkLabel5.ImageIndex = ((int)(resources.GetObject("linkLabel5.ImageIndex")));
			this.linkLabel5.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("linkLabel5.ImeMode")));
			this.linkLabel5.LinkArea = ((System.Windows.Forms.LinkArea)(resources.GetObject("linkLabel5.LinkArea")));
			this.linkLabel5.LinkColor = System.Drawing.Color.Firebrick;
			this.linkLabel5.Location = ((System.Drawing.Point)(resources.GetObject("linkLabel5.Location")));
			this.linkLabel5.Name = "linkLabel5";
			this.linkLabel5.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("linkLabel5.RightToLeft")));
			this.linkLabel5.Size = ((System.Drawing.Size)(resources.GetObject("linkLabel5.Size")));
			this.linkLabel5.TabIndex = ((int)(resources.GetObject("linkLabel5.TabIndex")));
			this.linkLabel5.TabStop = true;
			this.linkLabel5.Text = resources.GetString("linkLabel5.Text");
			this.linkLabel5.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("linkLabel5.TextAlign")));
			this.linkLabel5.Visible = ((bool)(resources.GetObject("linkLabel5.Visible")));
			this.linkLabel5.VisitedLinkColor = System.Drawing.Color.Maroon;
			this.linkLabel5.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.GoLink);
			// 
			// linkLabel6
			// 
			this.linkLabel6.AccessibleDescription = resources.GetString("linkLabel6.AccessibleDescription");
			this.linkLabel6.AccessibleName = resources.GetString("linkLabel6.AccessibleName");
			this.linkLabel6.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("linkLabel6.Anchor")));
			this.linkLabel6.AutoSize = ((bool)(resources.GetObject("linkLabel6.AutoSize")));
			this.linkLabel6.Cursor = System.Windows.Forms.Cursors.Default;
			this.linkLabel6.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("linkLabel6.Dock")));
			this.linkLabel6.Enabled = ((bool)(resources.GetObject("linkLabel6.Enabled")));
			this.linkLabel6.Font = ((System.Drawing.Font)(resources.GetObject("linkLabel6.Font")));
			this.linkLabel6.ForeColor = System.Drawing.Color.Red;
			this.linkLabel6.Image = ((System.Drawing.Image)(resources.GetObject("linkLabel6.Image")));
			this.linkLabel6.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("linkLabel6.ImageAlign")));
			this.linkLabel6.ImageIndex = ((int)(resources.GetObject("linkLabel6.ImageIndex")));
			this.linkLabel6.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("linkLabel6.ImeMode")));
			this.linkLabel6.LinkArea = ((System.Windows.Forms.LinkArea)(resources.GetObject("linkLabel6.LinkArea")));
			this.linkLabel6.LinkColor = System.Drawing.Color.Firebrick;
			this.linkLabel6.Location = ((System.Drawing.Point)(resources.GetObject("linkLabel6.Location")));
			this.linkLabel6.Name = "linkLabel6";
			this.linkLabel6.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("linkLabel6.RightToLeft")));
			this.linkLabel6.Size = ((System.Drawing.Size)(resources.GetObject("linkLabel6.Size")));
			this.linkLabel6.TabIndex = ((int)(resources.GetObject("linkLabel6.TabIndex")));
			this.linkLabel6.TabStop = true;
			this.linkLabel6.Text = resources.GetString("linkLabel6.Text");
			this.linkLabel6.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("linkLabel6.TextAlign")));
			this.linkLabel6.Visible = ((bool)(resources.GetObject("linkLabel6.Visible")));
			this.linkLabel6.VisitedLinkColor = System.Drawing.Color.Maroon;
			this.linkLabel6.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.GoLink);
			// 
			// linkLabel7
			// 
			this.linkLabel7.AccessibleDescription = resources.GetString("linkLabel7.AccessibleDescription");
			this.linkLabel7.AccessibleName = resources.GetString("linkLabel7.AccessibleName");
			this.linkLabel7.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("linkLabel7.Anchor")));
			this.linkLabel7.AutoSize = ((bool)(resources.GetObject("linkLabel7.AutoSize")));
			this.linkLabel7.Cursor = System.Windows.Forms.Cursors.Default;
			this.linkLabel7.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("linkLabel7.Dock")));
			this.linkLabel7.Enabled = ((bool)(resources.GetObject("linkLabel7.Enabled")));
			this.linkLabel7.Font = ((System.Drawing.Font)(resources.GetObject("linkLabel7.Font")));
			this.linkLabel7.ForeColor = System.Drawing.Color.Red;
			this.linkLabel7.Image = ((System.Drawing.Image)(resources.GetObject("linkLabel7.Image")));
			this.linkLabel7.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("linkLabel7.ImageAlign")));
			this.linkLabel7.ImageIndex = ((int)(resources.GetObject("linkLabel7.ImageIndex")));
			this.linkLabel7.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("linkLabel7.ImeMode")));
			this.linkLabel7.LinkArea = ((System.Windows.Forms.LinkArea)(resources.GetObject("linkLabel7.LinkArea")));
			this.linkLabel7.LinkColor = System.Drawing.Color.Firebrick;
			this.linkLabel7.Location = ((System.Drawing.Point)(resources.GetObject("linkLabel7.Location")));
			this.linkLabel7.Name = "linkLabel7";
			this.linkLabel7.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("linkLabel7.RightToLeft")));
			this.linkLabel7.Size = ((System.Drawing.Size)(resources.GetObject("linkLabel7.Size")));
			this.linkLabel7.TabIndex = ((int)(resources.GetObject("linkLabel7.TabIndex")));
			this.linkLabel7.TabStop = true;
			this.linkLabel7.Text = resources.GetString("linkLabel7.Text");
			this.linkLabel7.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("linkLabel7.TextAlign")));
			this.linkLabel7.Visible = ((bool)(resources.GetObject("linkLabel7.Visible")));
			this.linkLabel7.VisitedLinkColor = System.Drawing.Color.Maroon;
			this.linkLabel7.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.GoLink);
			// 
			// groupBox1
			// 
			this.groupBox1.AccessibleDescription = resources.GetString("groupBox1.AccessibleDescription");
			this.groupBox1.AccessibleName = resources.GetString("groupBox1.AccessibleName");
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("groupBox1.Anchor")));
			this.groupBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("groupBox1.BackgroundImage")));
			this.groupBox1.Controls.Add(this.linkLabel6);
			this.groupBox1.Controls.Add(this.linkLabel5);
			this.groupBox1.Controls.Add(this.linkLabel4);
			this.groupBox1.Controls.Add(this.linkLabel3);
			this.groupBox1.Controls.Add(this.linkLabel2);
			this.groupBox1.Controls.Add(this.linkLabel7);
			this.groupBox1.Controls.Add(this.linkLabel1);
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
			// button1
			// 
			this.button1.AccessibleDescription = resources.GetString("button1.AccessibleDescription");
			this.button1.AccessibleName = resources.GetString("button1.AccessibleName");
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("button1.Anchor")));
			this.button1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
			this.button1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("button1.Dock")));
			this.button1.Enabled = ((bool)(resources.GetObject("button1.Enabled")));
			this.button1.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("button1.FlatStyle")));
			this.button1.Font = ((System.Drawing.Font)(resources.GetObject("button1.Font")));
			this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
			this.button1.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("button1.ImageAlign")));
			this.button1.ImageIndex = ((int)(resources.GetObject("button1.ImageIndex")));
			this.button1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("button1.ImeMode")));
			this.button1.Location = ((System.Drawing.Point)(resources.GetObject("button1.Location")));
			this.button1.Name = "button1";
			this.button1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("button1.RightToLeft")));
			this.button1.Size = ((System.Drawing.Size)(resources.GetObject("button1.Size")));
			this.button1.TabIndex = ((int)(resources.GetObject("button1.TabIndex")));
			this.button1.Text = resources.GetString("button1.Text");
			this.button1.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("button1.TextAlign")));
			this.button1.Visible = ((bool)(resources.GetObject("button1.Visible")));
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// links
			// 
			this.links.AccessibleDescription = resources.GetString("links.AccessibleDescription");
			this.links.AccessibleName = resources.GetString("links.AccessibleName");
			this.links.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("links.Anchor")));
			this.links.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("links.BackgroundImage")));
			this.links.ColumnWidth = ((int)(resources.GetObject("links.ColumnWidth")));
			this.links.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("links.Dock")));
			this.links.Enabled = ((bool)(resources.GetObject("links.Enabled")));
			this.links.Font = ((System.Drawing.Font)(resources.GetObject("links.Font")));
			this.links.HorizontalExtent = ((int)(resources.GetObject("links.HorizontalExtent")));
			this.links.HorizontalScrollbar = ((bool)(resources.GetObject("links.HorizontalScrollbar")));
			this.links.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("links.ImeMode")));
			this.links.IntegralHeight = ((bool)(resources.GetObject("links.IntegralHeight")));
			this.links.ItemHeight = ((int)(resources.GetObject("links.ItemHeight")));
			this.links.Items.AddRange(new object[] {
													   resources.GetString("links.Items"),
													   resources.GetString("links.Items1"),
													   resources.GetString("links.Items2"),
													   resources.GetString("links.Items3"),
													   resources.GetString("links.Items4"),
													   resources.GetString("links.Items5"),
													   resources.GetString("links.Items6")});
			this.links.Location = ((System.Drawing.Point)(resources.GetObject("links.Location")));
			this.links.Name = "links";
			this.links.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("links.RightToLeft")));
			this.links.ScrollAlwaysVisible = ((bool)(resources.GetObject("links.ScrollAlwaysVisible")));
			this.links.Size = ((System.Drawing.Size)(resources.GetObject("links.Size")));
			this.links.TabIndex = ((int)(resources.GetObject("links.TabIndex")));
			this.links.Visible = ((bool)(resources.GetObject("links.Visible")));
			// 
			// linkLabel8
			// 
			this.linkLabel8.AccessibleDescription = resources.GetString("linkLabel8.AccessibleDescription");
			this.linkLabel8.AccessibleName = resources.GetString("linkLabel8.AccessibleName");
			this.linkLabel8.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("linkLabel8.Anchor")));
			this.linkLabel8.AutoSize = ((bool)(resources.GetObject("linkLabel8.AutoSize")));
			this.linkLabel8.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("linkLabel8.Dock")));
			this.linkLabel8.Enabled = ((bool)(resources.GetObject("linkLabel8.Enabled")));
			this.linkLabel8.Font = ((System.Drawing.Font)(resources.GetObject("linkLabel8.Font")));
			this.linkLabel8.ForeColor = System.Drawing.Color.Gray;
			this.linkLabel8.Image = ((System.Drawing.Image)(resources.GetObject("linkLabel8.Image")));
			this.linkLabel8.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("linkLabel8.ImageAlign")));
			this.linkLabel8.ImageIndex = ((int)(resources.GetObject("linkLabel8.ImageIndex")));
			this.linkLabel8.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("linkLabel8.ImeMode")));
			this.linkLabel8.LinkArea = ((System.Windows.Forms.LinkArea)(resources.GetObject("linkLabel8.LinkArea")));
			this.linkLabel8.LinkColor = System.Drawing.Color.Firebrick;
			this.linkLabel8.Location = ((System.Drawing.Point)(resources.GetObject("linkLabel8.Location")));
			this.linkLabel8.Name = "linkLabel8";
			this.linkLabel8.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("linkLabel8.RightToLeft")));
			this.linkLabel8.Size = ((System.Drawing.Size)(resources.GetObject("linkLabel8.Size")));
			this.linkLabel8.TabIndex = ((int)(resources.GetObject("linkLabel8.TabIndex")));
			this.linkLabel8.TabStop = true;
			this.linkLabel8.Text = resources.GetString("linkLabel8.Text");
			this.linkLabel8.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("linkLabel8.TextAlign")));
			this.linkLabel8.Visible = ((bool)(resources.GetObject("linkLabel8.Visible")));
			this.linkLabel8.VisitedLinkColor = System.Drawing.Color.DarkRed;
			this.linkLabel8.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel8_LinkClicked);
			// 
			// tbsup
			// 
			this.tbsup.AccessibleDescription = resources.GetString("tbsup.AccessibleDescription");
			this.tbsup.AccessibleName = resources.GetString("tbsup.AccessibleName");
			this.tbsup.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbsup.Anchor")));
			this.tbsup.AutoSize = ((bool)(resources.GetObject("tbsup.AutoSize")));
			this.tbsup.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbsup.BackgroundImage")));
			this.tbsup.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbsup.Dock")));
			this.tbsup.Enabled = ((bool)(resources.GetObject("tbsup.Enabled")));
			this.tbsup.Font = ((System.Drawing.Font)(resources.GetObject("tbsup.Font")));
			this.tbsup.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbsup.ImeMode")));
			this.tbsup.Location = ((System.Drawing.Point)(resources.GetObject("tbsup.Location")));
			this.tbsup.MaxLength = ((int)(resources.GetObject("tbsup.MaxLength")));
			this.tbsup.Multiline = ((bool)(resources.GetObject("tbsup.Multiline")));
			this.tbsup.Name = "tbsup";
			this.tbsup.PasswordChar = ((char)(resources.GetObject("tbsup.PasswordChar")));
			this.tbsup.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbsup.RightToLeft")));
			this.tbsup.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbsup.ScrollBars")));
			this.tbsup.Size = ((System.Drawing.Size)(resources.GetObject("tbsup.Size")));
			this.tbsup.TabIndex = ((int)(resources.GetObject("tbsup.TabIndex")));
			this.tbsup.Text = resources.GetString("tbsup.Text");
			this.tbsup.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbsup.TextAlign")));
			this.tbsup.Visible = ((bool)(resources.GetObject("tbsup.Visible")));
			this.tbsup.WordWrap = ((bool)(resources.GetObject("tbsup.WordWrap")));
			// 
			// Help
			// 
			this.AccessibleDescription = resources.GetString("$this.AccessibleDescription");
			this.AccessibleName = resources.GetString("$this.AccessibleName");
			this.AutoScaleBaseSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScaleBaseSize")));
			this.AutoScroll = ((bool)(resources.GetObject("$this.AutoScroll")));
			this.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMargin")));
			this.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMinSize")));
			this.BackColor = System.Drawing.Color.White;
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.ClientSize = ((System.Drawing.Size)(resources.GetObject("$this.ClientSize")));
			this.Controls.Add(this.tbsup);
			this.Controls.Add(this.linkLabel8);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.links);
			this.Enabled = ((bool)(resources.GetObject("$this.Enabled")));
			this.Font = ((System.Drawing.Font)(resources.GetObject("$this.Font")));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("$this.ImeMode")));
			this.Location = ((System.Drawing.Point)(resources.GetObject("$this.Location")));
			this.MaximizeBox = false;
			this.MaximumSize = ((System.Drawing.Size)(resources.GetObject("$this.MaximumSize")));
			this.MinimizeBox = false;
			this.MinimumSize = ((System.Drawing.Size)(resources.GetObject("$this.MinimumSize")));
			this.Name = "Help";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.StartPosition = ((System.Windows.Forms.FormStartPosition)(resources.GetObject("$this.StartPosition")));
			this.Text = resources.GetString("$this.Text");
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void richTextBox1_TextChanged(object sender, System.EventArgs e)
		{
		
		}

		private void GoLink(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			LinkLabel ll = (LinkLabel)sender;
			int index = ll.TabIndex-1;

			if (index<links.Items.Count) 
			{
				string url = (string)links.Items[index];
				if (url.IndexOf("|")>0) 
				{
					string[] alts = url.Split("|".ToCharArray(), 2);
					alts[1] = alts[1].Replace("{SimPE}", Helper.SimPePath);
					if (System.IO.File.Exists(alts[1])) url = alts[1];
					else url = alts[0];
				}

				try 
				{
					System.Windows.Forms.Help.ShowHelp(this, url);
				} 
				catch (Exception ex) 
				{
					Helper.ExceptionMessage("", ex);
				}
			}
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			Close();
		}

		private void linkLabel8_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			try 
			{
				System.Windows.Forms.Help.ShowHelp(this, this.tbsup.Text);
			} 
			catch (Exception ex) 
			{
				Helper.ExceptionMessage("", ex);
			}
		}
	}
}
