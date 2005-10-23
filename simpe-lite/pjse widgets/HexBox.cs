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
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace pjse.widgets
{
	/// <summary>
	/// Base for format-specific Hex input boxes
	/// </summary>
	public abstract class HexBox : System.Windows.Forms.TextBox
	{
		protected HexBox() : base() { this.TextAlign = HorizontalAlignment.Right; }


		protected HexBox(int digits) : base() { this.digits = digits; }


		private int digits = 8;
		private uint lastValue = 0;

		public override string Text
		{
			get { return base.Text; }

			set
			{
				UInt32 i = Convert.ToUInt32(value, 16);
				string s = i.ToString("X");
				if (s.Length > digits)
					throw new OverflowException();
				base.Text = "0x" + s.PadLeft(digits, '0');
			}
		}

		public override int MaxLength { get { return digits + 2; } }

		public override bool Multiline { get { return false; } }

		protected override void OnValidating(CancelEventArgs e)
		{
			if (!e.Cancel)
			{
				if (!IsValid || Value.ToString("X").Length > digits)
				{
					e.Cancel = true;
					Value = lastValue;
					SelectAll();
				}
			}
			base.OnValidating (e);
		}


		protected override void OnValidated(EventArgs e)
		{
			Value = Value;
			SelectAll();
			base.OnValidated (e);
		}


		public UInt32 Value
		{
			get { return Convert.ToUInt32(Text, 16); }

			set
			{
				string s = value.ToString("X");
				if (s.Length > digits)
					throw new OverflowException();
				base.Text = "0x" + s.PadLeft(digits, '0');
				lastValue = value;
			}
		}

		public bool IsValid
		{
			get
			{
				try 
				{
					Convert.ToUInt32(this.Text, 16);
					return true;
				}
				catch { }
				return false;
			}
		}
	}

	public class HexBoxU32 : HexBox
	{
		public HexBoxU32() : base(8) { }

	}

	public class HexBoxU16 : HexBox
	{
		public HexBoxU16() : base(4) { }
	}

	public class HexBoxU8 : HexBox
	{
		public HexBoxU8() : base(2) { }
	}

}
