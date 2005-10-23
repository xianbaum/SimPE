using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace pjse.widgets
{
	/// <summary>
	/// Summary description for DataOwner.
	/// </summary>
	public class DataOwner : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox cbOwner;
		private HexBoxU8 tbOwner;
		private System.Windows.Forms.ComboBox cbInstance;
		private HexBoxU16 tbInstance;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public DataOwner()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call

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

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.cbOwner = new System.Windows.Forms.ComboBox();
			this.tbOwner = new pjse.widgets.HexBoxU8();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.cbInstance = new System.Windows.Forms.ComboBox();
			this.tbInstance = new pjse.widgets.HexBoxU16();
			this.SuspendLayout();
			// 
			// cbOwner
			// 
			this.cbOwner.Location = new System.Drawing.Point(56, 0);
			this.cbOwner.Name = "cbOwner";
			this.cbOwner.Size = new System.Drawing.Size(121, 21);
			this.cbOwner.TabIndex = 0;
			this.cbOwner.Text = "cbOwner";
			// 
			// tbOwner
			// 
			this.tbOwner.Location = new System.Drawing.Point(176, 0);
			this.tbOwner.Name = "tbOwner";
			this.tbOwner.Size = new System.Drawing.Size(56, 20);
			this.tbOwner.TabIndex = 1;
			this.tbOwner.Text = "0xDD";
			this.tbOwner.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// TODO: Code generation for 'this.tbOwner.Value' failed because of Exception 'Invalid Primitive Type: System.UInt32. Only CLS compliant primitive types can be used. Consider using CodeObjectCreateExpression.'.
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 3);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(38, 16);
			this.label1.TabIndex = 2;
			this.label1.Text = "Owner";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(4, 27);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(47, 16);
			this.label2.TabIndex = 3;
			this.label2.Text = "Instance";
			// 
			// cbInstance
			// 
			this.cbInstance.Location = new System.Drawing.Point(56, 24);
			this.cbInstance.Name = "cbInstance";
			this.cbInstance.Size = new System.Drawing.Size(121, 21);
			this.cbInstance.TabIndex = 4;
			this.cbInstance.Text = "cbInstance";
			// 
			// tbInstance
			// 
			this.tbInstance.Location = new System.Drawing.Point(176, 24);
			this.tbInstance.Name = "tbInstance";
			this.tbInstance.Size = new System.Drawing.Size(56, 20);
			this.tbInstance.TabIndex = 5;
			this.tbInstance.Text = "0xDDDD";
			this.tbInstance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// TODO: Code generation for 'this.tbInstance.Value' failed because of Exception 'Invalid Primitive Type: System.UInt32. Only CLS compliant primitive types can be used. Consider using CodeObjectCreateExpression.'.
			// 
			// DataOwner
			// 
			this.Controls.Add(this.tbInstance);
			this.Controls.Add(this.cbInstance);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.tbOwner);
			this.Controls.Add(this.cbOwner);
			this.Name = "DataOwner";
			this.Size = new System.Drawing.Size(232, 48);
			this.ResumeLayout(false);

		}
		#endregion
	}
}
