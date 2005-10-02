/***************************************************************************
 *   Copyright (C) 2005 by Peter L Jones                                   *
 *   peter@drealm.info                                                     *
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
using SimPe.Interfaces.Files;
using SimPe.Interfaces.Plugin;
using SimPe.PackedFiles.Wrapper;
using SimPe.Interfaces.Scenegraph;

namespace pjse.BhavOperandWizards.WizBhav
{
	#region internal form
	/// <summary>
	/// Zusammenfassung für BhavInstruction.
	/// </summary>
	internal class UI : System.Windows.Forms.Form
	{
		#region Form variables

		private System.Windows.Forms.TextBox tbval1;
		internal System.Windows.Forms.Panel pnWiz0x0002;
		private System.Windows.Forms.ComboBox cbPicker1;
		private System.Windows.Forms.ComboBox cbDataOwner1;
		/// <summary>
		/// Erforderliche Designervariable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion

		public UI()
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

		
		#region UI
		public void Execute(Instruction inst)
		{
			return;
		}

		public Instruction Write(Instruction inst)
		{
			return null;
		}

		#endregion

		#region Vom Windows Form-Designer generierter Code
		/// <summary>
		/// Erforderliche Methode für die Designerunterstützung. 
		/// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
		/// </summary>
		private void InitializeComponent()
		{
			this.pnWiz0x0002 = new System.Windows.Forms.Panel();
			this.cbPicker1 = new System.Windows.Forms.ComboBox();
			this.tbval1 = new System.Windows.Forms.TextBox();
			this.cbDataOwner1 = new System.Windows.Forms.ComboBox();
			this.pnWiz0x0002.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnWiz0x0002
			// 
			this.pnWiz0x0002.Controls.Add(this.cbPicker1);
			this.pnWiz0x0002.Controls.Add(this.tbval1);
			this.pnWiz0x0002.Controls.Add(this.cbDataOwner1);
			this.pnWiz0x0002.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.pnWiz0x0002.Location = new System.Drawing.Point(8, 8);
			this.pnWiz0x0002.Name = "pnWiz0x0002";
			this.pnWiz0x0002.Size = new System.Drawing.Size(224, 24);
			this.pnWiz0x0002.TabIndex = 0;
			// 
			// cbPicker1
			// 
			this.cbPicker1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cbPicker1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbPicker1.DropDownWidth = 352;
			this.cbPicker1.Location = new System.Drawing.Point(112, 0);
			this.cbPicker1.Name = "cbPicker1";
			this.cbPicker1.Size = new System.Drawing.Size(112, 21);
			this.cbPicker1.TabIndex = 2;
			this.cbPicker1.Visible = false;
			// 
			// tbval1
			// 
			this.tbval1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.tbval1.Location = new System.Drawing.Point(112, 0);
			this.tbval1.Name = "tbval1";
			this.tbval1.Size = new System.Drawing.Size(112, 21);
			this.tbval1.TabIndex = 2;
			this.tbval1.Text = "";
			// 
			// cbDataOwner1
			// 
			this.cbDataOwner1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.cbDataOwner1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbDataOwner1.Location = new System.Drawing.Point(0, 0);
			this.cbDataOwner1.Name = "cbDataOwner1";
			this.cbDataOwner1.Size = new System.Drawing.Size(112, 21);
			this.cbDataOwner1.TabIndex = 1;
			// 
			// UI
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(640, 366);
			this.Controls.Add(this.pnWiz0x0002);
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Name = "UI";
			this.Text = "UI";
			this.pnWiz0x0002.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
	}

	#endregion
}

namespace pjse.BhavNameWizards
{

	/// <summary>
	/// Abstract class for BHAV name providers (global, local, semiglobal)
	/// </summary>
	public abstract class BhavWizBhav : BhavWiz
	{
		private Bhav bhav = null;

		protected BhavWizBhav(Instruction i) : base (i)
		{
			if (i != null)
			{
				SimPe.FileTable.FileIndex.Load();
				IScenegraphFileIndexItem[] items =
					SimPe.FileTable.FileIndex.FindFile(SimPe.Data.MetaData.BHAV_FILE, Group, (ulong)i.OpCode, null);
				if (items != null && items.Length > 0)
				{
					bhav = new Bhav(null);
					bhav.ProcessData(items[0]);
					bhavFilenames[items[0].FileDescriptor.Filename] = bhav.FileName;
				}
			}
		}

		public static implicit operator BhavWizBhav(Instruction i)
		{
			if (i.OpCode < 0x0100)
				throw new InvalidCastException("OpCode not a BHAV");
			if (i.OpCode < 0x1000) return new GlobalWiz(i);
			if (i.OpCode < 0x2000) return new LocalWiz(i);
			return new SemiGlobalWiz(i);
		}

		protected abstract uint Group { get; }


		#region BhavWiz
		protected override string Prefix { get { return "BHAV"; } }

		protected override string OpcodeName { get  { return (bhav != null) ? bhav.FileName : "[" + SimPe.Localization.Manager.GetString("unk") + " BHAV]"; } }

		public override string ShortName { get { return base.ShortName + " (" + Operands(false) + ")"; } }

		public override string LongName { get { return base.ShortName + " (" + Operands(true) + ")"; } }

		public override Bhav Wrapper { get { return bhav; } }

		#endregion

		private string Operands(bool lng)
		{
			if (bhav == null) 
				return "[" + SimPe.Localization.Manager.GetString("unk") + "]";

			string s = "";
			int myArgc = (instruction.Parent is Bhav) ? (int)((Bhav)instruction.Parent).Header.ArgumentCount : 0;
			int thisArgc = bhav.Header.ArgumentCount;

			if (lng)
			{
				s += (thisArgc == 0) ? "no" : thisArgc.ToString();
				s += " arg" + (thisArgc == 1 ? "" : "s");
			}

			if (thisArgc > 0)
			{
				byte[] o = new byte[16];
				((byte[])instruction.Operands).CopyTo(o, 0);
				((byte[])instruction.Reserved1).CopyTo(o, 8);

				s += (lng ? ": " : "");


				bool noOperands = o[12] == 0; // original format; check for "operand killer"
				for (int i = 0; i < 8 && noOperands; i++)
					noOperands = o[i] == 0xFF;

				int p = 0;
				if ((o[12] & 0x02) != 0)	// Params first
				{
					int i = Math.Min(thisArgc, myArgc);
					if (i > 0)
						s += (lng ? "Caller's params " + p.ToString() + (i > 1 ? " to " + i.ToString() : "") : "Caller's params");
					thisArgc -= i;
					myArgc -= i;
					p += i;
					if (i > 0 && thisArgc > 0) s += ", ";
				}


				if ((o[12] & 0x01) == 0) // original format; I reckon nodeversion should be >0 to get the new format
				{
					for (int i = 0; !noOperands && thisArgc > 0 && i < 4; i++, thisArgc--)
						s += (i>0 ? ", " : "") + "0x" + SimPe.Helper.HexString(ToShort(o[(i*2)], o[(i*2) + 1]));
				}
				else	// 16 byte format
				{
					for (int i = 0; thisArgc > 0 && i < 4; i++, thisArgc--)
						s += (i>0 ? ", " : "") +
							(lng
							? dataOwner(o[i*3], ToShort(o[(i*3) + 1], o[(i*3) + 2]))
							: GS.GStr(GS.SF.DataOwners, o[i*3]) + " 0x" + SimPe.Helper.HexString(ToShort(o[(i*3) + 1], o[(i*3) + 2]))
							);
				}

				if (thisArgc > 0)
				{
					s += (noOperands ? "" : ", ")
						+ (lng ? "Caller's params " + p.ToString() + (thisArgc > 1 ? " to " + (p + thisArgc - 1).ToString() : "") : "Caller's params");
				}
			}
			return s;

		}


		private static Hashtable bhavFilenames = new Hashtable();

		public ArrayList Aliases
		{
			get
			{
				ArrayList aliases = new ArrayList();
				ArrayList instances = new ArrayList();

				SimPe.FileTable.FileIndex.Load();
				foreach (IScenegraphFileIndexItem item in SimPe.FileTable.FileIndex.FindFile(SimPe.Data.MetaData.BHAV_FILE, Group))
				{
					if (instruction == null || instruction.Parent == null || instruction.Parent.Package == item.Package
						|| bhavFilenames[item.FileDescriptor.Filename] == null)
					{
						Bhav b = new Bhav(null);
						b.ProcessData(item);
						bhavFilenames[item.FileDescriptor.Filename] = b.FileName;
					}
					if (!instances.Contains(item.FileDescriptor.Instance))
					{
						instances.Add(item.FileDescriptor.Instance);
						aliases.Add(new SimPe.Data.Alias(item.FileDescriptor.Instance, (string)bhavFilenames[item.FileDescriptor.Filename]));
					}
				}

				return aliases;
			}
		}
	}


	public class GlobalWiz : BhavWizBhav
	{
		public GlobalWiz(Instruction i) : base(i) { }

		public static implicit operator GlobalWiz(AbstractWrapper parent)
		{
			return new GlobalWiz(new Instruction(parent, 0x0100));
		}

		#region BhavWizBhav
		protected override uint Group { get { return 0x7FD46CD0; } }

		#endregion
		#region BhavWiz
		protected override string Prefix { get { return "global"; } }

		#endregion
	}

	public class LocalWiz : BhavWizBhav
	{
		public LocalWiz(Instruction i) : base(i) { }

		public static implicit operator LocalWiz(AbstractWrapper parent)
		{
			return new LocalWiz(new Instruction(parent, 0x1000));
		}

		#region BhavWizBhav
		protected override uint Group
		{
			get
			{
				return (instruction != null && instruction.Parent != null && instruction.Parent.FileDescriptor != null)
					? instruction.Parent.FileDescriptor.Group
					: 0xffffffff;
			}
		}

		#endregion
		#region BhavWiz
		protected override string Prefix { get { return "private"; } }

		#endregion
	}

	public class SemiGlobalWiz : BhavWizBhav
	{
		public SemiGlobalWiz(Instruction i) : base(i) { }

		public static implicit operator SemiGlobalWiz(AbstractWrapper parent)
		{
			return new SemiGlobalWiz(new Instruction(parent, 0x2000));
		}

		#region BhavWizBhav
		protected override uint Group
		{
			get
			{
				return (instruction != null && instruction.Parent != null && instruction.Parent.FileDescriptor != null)
					? SemiGlobalGroup
					: 0;
			}
		}

		#endregion
		#region BhavWiz
		protected override string Prefix { get { return "semi"; } }

		#endregion
	}

}
