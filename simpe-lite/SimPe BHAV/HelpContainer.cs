using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace pjse
{
	/// <summary>
	/// Summary description for HelpContainer.
	/// </summary>
	public class HelpContainer : System.Windows.Forms.Form
	{
		#region Form variables
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage TTABHelp;
		private System.Windows.Forms.RichTextBox richTextBox1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion

		public HelpContainer()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		public HelpContainer(HelpPage p)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			tabControl1.SelectedIndex = (int)p;
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
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.TTABHelp = new System.Windows.Forms.TabPage();
			this.richTextBox1 = new System.Windows.Forms.RichTextBox();
			this.tabControl1.SuspendLayout();
			this.TTABHelp.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Left;
			this.tabControl1.Controls.Add(this.TTABHelp);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Multiline = true;
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(512, 533);
			this.tabControl1.TabIndex = 0;
			// 
			// TTABHelp
			// 
			this.TTABHelp.Controls.Add(this.richTextBox1);
			this.TTABHelp.Location = new System.Drawing.Point(23, 4);
			this.TTABHelp.Name = "TTABHelp";
			this.TTABHelp.Size = new System.Drawing.Size(485, 525);
			this.TTABHelp.TabIndex = 0;
			this.TTABHelp.Text = "TTAB Help";
			// 
			// richTextBox1
			// 
			this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.richTextBox1.Cursor = System.Windows.Forms.Cursors.Arrow;
			this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.richTextBox1.Font = new System.Drawing.Font("Arial", 10F);
			this.richTextBox1.Location = new System.Drawing.Point(0, 0);
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.ReadOnly = true;
			this.richTextBox1.Size = new System.Drawing.Size(485, 525);
			this.richTextBox1.TabIndex = 0;
			this.richTextBox1.Text = @"Minimum is the least level of that is advertised to all Sims for this motive.

Delta is the amount extra that would be advertised to Sims with a particular attribute, on a sliding scale according to how much of that attribute they have.

Type is the Sim’s attribute that matters when deciding whether to advertise the extra.

Values for the Type field:
00	None – all sims get the same advert.
01	Nice
02	Grouchy
03	Active
04	Lazy
05	Generous (probably unused)
06	Selfish (probably unused)
07	Playful
08	Serious
09	Outgoing
0A	Shy
0B	Neat
0C	Sloppy
0D	Cleaning Skill
0E	Cooking Skill
0F	Social Skill (Charisma)
10	Mechanical Skill
11	Gardening Skill (may be unused)
12	Music Skill (may be unused)
13	Creative Skill
14	Literacy Skill (probably unused)
15	Physical Skill (Body)
16	Logic Skill";
			// 
			// HelpContainer
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(512, 533);
			this.Controls.Add(this.tabControl1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Name = "HelpContainer";
			this.ShowInTaskbar = false;
			this.Text = "PJSE Help";
			this.tabControl1.ResumeLayout(false);
			this.TTABHelp.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		public enum HelpPage : int
		{
			TtabMotives = 0
		}
	}
}
