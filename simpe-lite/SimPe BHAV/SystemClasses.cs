using System;

namespace System
{
	/// <summary>
	/// Summary description for SystemClasses.
	/// </summary>
	public class boolset
	{
		private bool[] bitset = null;

		private boolset(int size, uint val)
		{
			bitset = new bool[size];
			for(int i = 0; i < size; i++)
				bitset[i] = (val & (1 << i)) != 0;
		}

		public boolset(uint val) : this(32, val) {}

		public boolset(ushort val) : this(16, val) {}

		public boolset(byte val) : this(8, val) {}


		public static implicit operator boolset(uint o) { return new boolset(o); }

		public static implicit operator boolset(ushort o) { return new boolset(o); }

		public static implicit operator boolset(byte o) { return new boolset(o); }


		private static int doOperator(boolset t, int l)
		{
			int val = 0;
			for(int i = 0; i < l && i < t.bitset.Length; i++)
				val += (t[i] ? 1 : 0) << i;
			return val;
		}

		public static implicit operator byte(boolset t) { return (byte)doOperator(t, 8); }

		public static implicit operator ushort(boolset t) { return (ushort)doOperator(t, 16); }

		public static implicit operator uint(boolset t) { return (uint)doOperator(t, 32); }


		public bool this[int i]
		{
			get
			{
				if (i > bitset.Length)
					throw new ArgumentOutOfRangeException();
				return bitset[i];
			}

			set
			{
				if (i > bitset.Length)
					throw new ArgumentOutOfRangeException();
				bitset[i] = value;
				/*
				 *   set: val |= 1 << bit;
				 * clear: val -= (val & (1 << bit))
				 */
			}

		}

		public bool Matches(string mask)
		{
			// right-hand end of mask is low end of bitset
			int mcnt = mask.Length - 1;
			bool matched = true;
			int i = 0;
			while(matched && mcnt > 0 && i < bitset.Length)
			{
				if (mask[mcnt].Equals('0'))
					matched = !bitset[i];
				else if (mask[mcnt].Equals('1'))
					matched = bitset[i];
				mcnt--;
				i++;
			}
			return matched;
		}
	}

}
