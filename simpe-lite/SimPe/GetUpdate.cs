using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace SimPe
{
	/// <summary>
	/// Zusammenfassung für GetUpdate.
	/// </summary>
	public class GetUpdate : System.Windows.Forms.Form
	{
		internal System.Windows.Forms.LinkLabel llyes;
		internal System.Windows.Forms.LinkLabel llno;
		/// <summary>
		/// Erforderliche Designervariable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public GetUpdate()
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
			this.llyes = new System.Windows.Forms.LinkLabel();
			this.llno = new System.Windows.Forms.LinkLabel();
			this.SuspendLayout();
			// 
			// llyes
			// 
			this.llyes.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
			this.llyes.ForeColor = System.Drawing.Color.Gray;
			this.llyes.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.llyes.LinkArea = new System.Windows.Forms.LinkArea(40, 11);
			this.llyes.LinkColor = System.Drawing.Color.Firebrick;
			this.llyes.Location = new System.Drawing.Point(8, 8);
			this.llyes.Name = "llyes";
			this.llyes.Size = new System.Drawing.Size(280, 72);
			this.llyes.TabIndex = 102;
			this.llyes.TabStop = true;
			this.llyes.Text = "Automated update disabled.\nPlease check SourceForge.";
			this.llyes.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.llyes.VisitedLinkColor = System.Drawing.Color.DarkRed;
			this.llyes.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel8_LinkClicked);
			// 
			// llno
			// 
			this.llno.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
			this.llno.ForeColor = System.Drawing.Color.Gray;
			this.llno.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.llno.LinkArea = new System.Windows.Forms.LinkArea(67, 26);
			this.llno.LinkColor = System.Drawing.Color.Firebrick;
			this.llno.Location = new System.Drawing.Point(8, 8);
			this.llno.Name = "llno";
			this.llno.Size = new System.Drawing.Size(280, 72);
			this.llno.TabIndex = 103;
			this.llno.Text = "There is no new Version of SimPE.";
			this.llno.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.llno.VisitedLinkColor = System.Drawing.Color.DarkRed;
			// 
			// GetUpdate
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(290, 86);
			this.Controls.Add(this.llno);
			this.Controls.Add(this.llyes);
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "GetUpdate";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "PLJones SimAntics Editor";
			this.ResumeLayout(false);

		}
		#endregion

		private void linkLabel8_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			try 
			{
				System.Windows.Forms.Help.ShowHelp(this, "http://sf.net/projects/plj-simpe");
			} 
			catch (Exception ex) 
			{
				Helper.ExceptionMessage("", ex);
			}
		}
	}
}
