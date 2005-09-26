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
		private System.Windows.Forms.TextBox tbval2;
		internal System.Windows.Forms.Panel pnWiz0x0002;
		private System.Windows.Forms.ComboBox cbPicker1;
		private System.Windows.Forms.ComboBox cbPicker2;
		private System.Windows.Forms.ComboBox cbOperator;
		private System.Windows.Forms.ComboBox cbDataOwner1;
		private System.Windows.Forms.ComboBox cbDataOwner2;
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

			FormLoad();
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
			wrappedByteArray ops = inst.Operands;
			int val1 = (ops[0x01] << 8) | ops[0x00];
			int val2 = (ops[0x03] << 8) | ops[0x02];

			tbval1.Text = "0x"+SimPe.Helper.HexString((ushort)val1);
			tbval2.Text = "0x"+SimPe.Helper.HexString((ushort)val2);

			byte op = ops[0x05];
			if (cbOperator.Items.Count>op) cbOperator.SelectedIndex = op;

			byte n1 = ops[0x06];
			byte n2 = ops[0x07];
			if (cbDataOwner1.Items.Count>n1) cbDataOwner1.SelectedIndex = n1;
			if (cbDataOwner2.Items.Count>n2) cbDataOwner2.SelectedIndex = n2;
			//doFlagThing();
		}

		public Instruction Write(Instruction inst)
		{
			try 
			{
				wrappedByteArray ops = inst.Operands;
				ops[0x06] = (byte)cbDataOwner1.SelectedIndex;
				ops[0x07] = (byte)cbDataOwner2.SelectedIndex;
				ops[0x05] = (byte)cbOperator.SelectedIndex;

				ushort val1 = textToUShort(tbval1.Text);
				ushort val2 = textToUShort(tbval2.Text);

				ops[0x00] = (byte)(val1 & 0xff);
				ops[0x01] = (byte)((val1 >> 8) & 0xff);

				ops[0x02] = (byte)(val2 & 0xff);
				ops[0x03] = (byte)((val2 >> 8) & 0xff);

				return inst;
			} 
			catch (Exception ex) 
			{
				SimPe.Helper.ExceptionMessage(SimPe.Localization.Manager.GetString("errconvert"), ex);
				return null;
			}
		}

		private void FormLoad()
		{
			this.cbDataOwner1.Items.Clear();
			this.cbDataOwner1.Items.AddRange(GS.gStr(GS.SF.DataOwners).ToArray());
			this.cbDataOwner2.Items.Clear();
			this.cbDataOwner2.Items.AddRange(GS.gStr(GS.SF.DataOwners).ToArray());
			this.cbOperator.Items.Clear();
			this.cbOperator.Items.AddRange(GS.gStr(GS.SF.Operators).ToArray());
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
			this.cbPicker2 = new System.Windows.Forms.ComboBox();
			this.cbPicker1 = new System.Windows.Forms.ComboBox();
			this.cbOperator = new System.Windows.Forms.ComboBox();
			this.tbval2 = new System.Windows.Forms.TextBox();
			this.cbDataOwner2 = new System.Windows.Forms.ComboBox();
			this.tbval1 = new System.Windows.Forms.TextBox();
			this.cbDataOwner1 = new System.Windows.Forms.ComboBox();
			this.pnWiz0x0002.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnWiz0x0002
			// 
			this.pnWiz0x0002.Controls.Add(this.cbPicker2);
			this.pnWiz0x0002.Controls.Add(this.cbPicker1);
			this.pnWiz0x0002.Controls.Add(this.cbOperator);
			this.pnWiz0x0002.Controls.Add(this.tbval2);
			this.pnWiz0x0002.Controls.Add(this.cbDataOwner2);
			this.pnWiz0x0002.Controls.Add(this.tbval1);
			this.pnWiz0x0002.Controls.Add(this.cbDataOwner1);
			this.pnWiz0x0002.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.pnWiz0x0002.Location = new System.Drawing.Point(8, 8);
			this.pnWiz0x0002.Name = "pnWiz0x0002";
			this.pnWiz0x0002.Size = new System.Drawing.Size(464, 72);
			this.pnWiz0x0002.TabIndex = 0;
			// 
			// cbPicker2
			// 
			this.cbPicker2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cbPicker2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbPicker2.DropDownWidth = 352;
			this.cbPicker2.Location = new System.Drawing.Point(352, 50);
			this.cbPicker2.Name = "cbPicker2";
			this.cbPicker2.Size = new System.Drawing.Size(112, 21);
			this.cbPicker2.TabIndex = 5;
			this.cbPicker2.Visible = false;
			this.cbPicker2.SelectedIndexChanged += new System.EventHandler(this.cbPicker2_SelectedIndexChanged);
			// 
			// cbPicker1
			// 
			this.cbPicker1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cbPicker1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbPicker1.DropDownWidth = 352;
			this.cbPicker1.Location = new System.Drawing.Point(352, 0);
			this.cbPicker1.Name = "cbPicker1";
			this.cbPicker1.Size = new System.Drawing.Size(112, 21);
			this.cbPicker1.TabIndex = 2;
			this.cbPicker1.Visible = false;
			this.cbPicker1.SelectedIndexChanged += new System.EventHandler(this.cbPicker1_SelectedIndexChanged);
			// 
			// cbOperator
			// 
			this.cbOperator.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.cbOperator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbOperator.Location = new System.Drawing.Point(0, 26);
			this.cbOperator.Name = "cbOperator";
			this.cbOperator.Size = new System.Drawing.Size(464, 21);
			this.cbOperator.TabIndex = 3;
			// 
			// tbval2
			// 
			this.tbval2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.tbval2.Location = new System.Drawing.Point(352, 50);
			this.tbval2.Name = "tbval2";
			this.tbval2.Size = new System.Drawing.Size(112, 21);
			this.tbval2.TabIndex = 5;
			this.tbval2.Text = "";
			// 
			// cbDataOwner2
			// 
			this.cbDataOwner2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.cbDataOwner2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbDataOwner2.Location = new System.Drawing.Point(0, 50);
			this.cbDataOwner2.Name = "cbDataOwner2";
			this.cbDataOwner2.Size = new System.Drawing.Size(352, 21);
			this.cbDataOwner2.TabIndex = 4;
			this.cbDataOwner2.SelectedIndexChanged += new System.EventHandler(this.cbDataOwner2_SelectedIndexChanged);
			// 
			// tbval1
			// 
			this.tbval1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.tbval1.Location = new System.Drawing.Point(352, 0);
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
			this.cbDataOwner1.Size = new System.Drawing.Size(352, 21);
			this.cbDataOwner1.TabIndex = 1;
			this.cbDataOwner1.SelectedIndexChanged += new System.EventHandler(this.cbDataOwner1_SelectedIndexChanged);
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

		private void cbDataOwner1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.cbPicker1.Visible = false;
			if (BhavWiz.doidGStr[(byte)cbDataOwner1.SelectedIndex] != null)
			{
				this.cbPicker1.Visible = true;
				this.cbPicker1.Items.Clear();
				this.cbPicker1.Items.AddRange(GS.gStr((uint)BhavWiz.doidGStr[(byte)cbDataOwner1.SelectedIndex]).ToArray());
				try 
				{
					ushort val = textToUShort(tbval1.Text);
					this.cbPicker1.SelectedIndex = val;
				} 
				catch (Exception) { }
			}
			else if (cbDataOwner1.SelectedIndex == 0x1a || cbDataOwner1.SelectedIndex == 0x2f)
			{
				//constant
				ushort[] vals = ConstantValueParser(textToUShort(tbval1.Text));
				tbval1.Text = "0x"+SimPe.Helper.HexString(vals[0])+":0x"+SimPe.Helper.HexString((byte)vals[1]);
			} 
			else
			{
				tbval1.Text = "0x"+SimPe.Helper.HexString(textToUShort(tbval1.Text));
			}
			this.tbval2.Visible = !cbPicker2.Visible;
		}

		private void cbDataOwner2_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.cbPicker2.Visible = false;
			if (BhavWiz.doidGStr[(byte)cbDataOwner2.SelectedIndex] != null)
			{
				this.cbPicker2.Visible = true;
				this.cbPicker2.Items.Clear();
				this.cbPicker2.Items.AddRange(GS.gStr((uint)BhavWiz.doidGStr[(byte)cbDataOwner2.SelectedIndex]).ToArray());
				try 
				{
					ushort val = textToUShort(tbval2.Text);
					this.cbPicker2.SelectedIndex = val;
				} 
				catch (Exception) { }
			}
			else if (cbDataOwner2.SelectedIndex == 0x1a || cbDataOwner2.SelectedIndex == 0x2f)
			{
				//constant
				ushort[] vals = ConstantValueParser(textToUShort(tbval2.Text));
				tbval2.Text = "0x"+SimPe.Helper.HexString(vals[0])+":0x"+SimPe.Helper.HexString((byte)vals[1]);
			} 
			else
			{
				tbval2.Text = "0x"+SimPe.Helper.HexString(textToUShort(tbval2.Text));
			}
			this.tbval2.Visible = !cbPicker2.Visible;
		}

		private void cbPicker1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			tbval1.Text = "0x"+SimPe.Helper.HexString((ushort)this.cbPicker1.SelectedIndex);
		}

		private void cbPicker2_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			tbval2.Text = "0x"+SimPe.Helper.HexString((ushort)this.cbPicker2.SelectedIndex);
		}


		private static ushort textToUShort(string text)
		{
			ushort val = 0;
			try 
			{
				if (text.IndexOf(":")==-1)
				{
					val = Convert.ToUInt16(text, 16);
				}
				else 
				{
					string[] s = text.Split(":".ToCharArray(), 2);
					ushort[] b = new ushort[2];
					b[0] = Convert.ToUInt16(s[0], 16);
					b[1] = Convert.ToUInt16(s[1], 16);
					val = ConstantValueParser(b);
				}
			}
			catch (Exception) { }
			return val;
		}

		private static ushort[] ConstantValueParser(ushort val) 
		{
			return pjse.BhavWiz.ExpandBCON(val);
		}

		private static ushort ConstantValueParser(ushort[] values) 
		{
			int t = 2;
			if ((values[0]>=0x1000) && (values[0]<0x2000)) 
			{
				values[0] = (ushort)(values[0]-0x1000);
				t = 0;
			} 
			else if (values[0]>0x2000) 
			{
				values[0] = (ushort)(values[0]-0x2000);
				t = 1;
			} 
			else if ((values[0] & 0x0140) == 0x0140)
			{
				values[0] = (ushort)(values[0]-0x0140);
				t = 4;
			}
			else
			{
				values[0] = (ushort)(values[0]-0x0100);
			}

			ushort ret = 0;
			ret = (ushort)(t << 13);
			ret += (ushort)((values[0] & 0x3f)  << 7);
			ret += (ushort)(values[1] & 0x7F);
			
			return ret;
		}

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
		protected BhavWizBhav(Instruction i) : base (i) { }

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

		protected override string OpcodeName
		{
			get 
			{
				IScenegraphFileIndexItem[] items = findBHAV();
				if (items != null)
				{
					// Always refresh bhavs for current package
					if (items[0].Package == instruction.Parent.Package || bhavFilenames[items[0].FileDescriptor.Filename] == null)
						loadBHAV(items[0]);

					return (string)bhavFilenames[items[0].FileDescriptor.Filename];
				}
				else return "[" + SimPe.Localization.Manager.GetString("unk") + " BHAV]";
			}
		}

		public override string ShortName
		{
			get
			{
				Bhav b = LoadBHAV();
				if (b == null) return base.ShortName;

				string s = "";

				bool noParms = true;
				for (int i = 0; i < 8 && noParms; i++)
					noParms = instruction.Operands[i] == 0xFF;
				if (!noParms)
				{
					byte[] parms = new byte[16];
					((byte[])instruction.Operands).CopyTo(parms, 0);
					((byte[])instruction.Reserved1).CopyTo(parms, 8);
					if ((parms[12] & 0x01) != 0)
					{
						for (int i = 0; i < b.Header.ArgumentCount && i < 4; i++)
							s += (i>0 ? ", " : "") + dataOwner(parms[i*3], ToShort(parms[(i*3) + 1], parms[(i*3) + 2]));
						if (b.Header.ArgumentCount > 4)
							s += "...";
					}
					else if ((parms[12] & 0x02) != 0)
					{
						s = "Pass on " + b.Header.ArgumentCount.ToString() + " params";
					}
					else
					{
						for (int i = 0; i < b.Header.ArgumentCount && i < 4; i++)
							s += (i>0 ? ", " : "") + "0x" + SimPe.Helper.HexString(ToShort(parms[(i*2)], parms[(i*2) + 1]));
						if (b.Header.ArgumentCount > 4)
							s += "...";
					}
				}

				return base.ShortName + " (" + s + ")";
			}
		}

		public override Bhav LoadBHAV()
		{
			IScenegraphFileIndexItem[] items = findBHAV();
			return (items == null) ? null : loadBHAV(items[0]);
		}

		#endregion

		private Bhav loadBHAV(IScenegraphFileIndexItem item)
		{
			Bhav b = new Bhav(null);
			b.ProcessData(item);
			bhavFilenames[item.FileDescriptor.Filename] = b.FileName;
			return b;
		}


		private IScenegraphFileIndexItem[] findBHAV()
		{
			if (instruction == null) return null;

			SimPe.FileTable.FileIndex.Load();
			IScenegraphFileIndexItem[] items =
				SimPe.FileTable.FileIndex.FindFile(SimPe.Data.MetaData.BHAV_FILE, Group, (ulong)instruction.OpCode, null);
			if (items == null || items.Length == 0) return null;
#if false
#if DEBUG
			if (items.Length > 1)
			{
				string s = "Multiple BHAVs found:";
				for (int i = 0; i < items.Length; i++)
				{
					Bhav bh = new Bhav(null);
					bh.ProcessData(items[i]);
					s += "(" + i.ToString() + ") " + bh.FileName + " from ";
					s += items[i].Package.SaveFileName;
					s += ", ";
				}
				System.Windows.Forms.MessageBox.Show(s);
			}
#endif
#endif
			return items;
		}


		private static Hashtable bhavFilenames = new Hashtable();

		public ArrayList Aliases
		{
			get
			{
				ArrayList aliases = new ArrayList();
				SimPe.FileTable.FileIndex.Load();
				foreach (IScenegraphFileIndexItem item in SimPe.FileTable.FileIndex.FindFile(SimPe.Data.MetaData.BHAV_FILE, Group))
				{
					if (this is LocalWiz || bhavFilenames[item.FileDescriptor.Filename] == null)
					{
						Bhav b = new Bhav(null);
						b.ProcessData(item);
						bhavFilenames[item.FileDescriptor.Filename] = b.FileName;
					}
					aliases.Add(new SimPe.Data.Alias(item.FileDescriptor.Instance, (string)bhavFilenames[item.FileDescriptor.Filename]));
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
