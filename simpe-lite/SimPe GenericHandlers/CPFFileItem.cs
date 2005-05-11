/***************************************************************************
 *   Copyright (C) 2005 by Ambertation                                     *
 *   quaxi@ambertation.de                                                  *
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
using SimPe.Data;

namespace SimPe.PackedFiles.Wrapper
{
	/// <summary>
	/// An Item stored in a CPF File
	/// </summary>
	public class CpfItemG : GenericExtendedItem
	{
		protected string[] GetNames()
		{
			string[] names = {
								 "Name",
								 "Value",
								 "Datatype"
							 };
			return  names;
		}


		/// <summary>
		/// Returns the
		/// </summary>
		public MetaData.DataTypes Datatype
		{
			get
			{
				string n = Base.Properties["Datatype"].GetType().Name;
				if ( n =="String") 
				{
					string s = Base.Properties["Datatype"].ToString();
					if (s.ToLower().Trim()=="dtstring") return MetaData.DataTypes.dtString;
					else if (s.ToLower().Trim()=="dtsingle") return MetaData.DataTypes.dtSingle;
					else return MetaData.DataTypes.dtInteger;
				} 
				else 
				{
					uint datatype = Convert.ToUInt32(Base.Properties["Datatype"]);
					return (MetaData.DataTypes)datatype;
				}
			}
		}
		
		/// <summary>
		/// Returns the Name
		/// </summary>
		public string Name
		{
			get
			{
				byte[] name = GenericCommon.ToByteArray(Base.Properties["Name"]);

				string s = "";
				for (int i=0; i<name.Length; i++) s+= ((char)name[i]).ToString();
				return s;
			}
		}

		/// <summary>
		/// Returns the Name as a Byte Array
		/// </summary>
		public byte[] PlainName
		{
			get
			{
				byte[] name = GenericCommon.ToByteArray(Base.Properties["Name"]);

				
				return name;
			}
		}


		#region value Handling
		/// <summary>
		/// Returns the
		/// </summary>
		public Byte[] Value
		{
			get
			{
				return GenericCommon.ToByteArray(Base.Properties["Value"]);
			}
		}
		
		/// <summary>
		/// Returns the value as a String
		/// </summary>
		public string StringValue
		{
			get
			{
				switch (Datatype)
				{
					case MetaData.DataTypes.dtSingle:
					{
						return AsSingle().ToString();
					}
					case MetaData.DataTypes.dtInteger:
					{
						return AsInteger().ToString();
					}
					case MetaData.DataTypes.dtString:
					{
						return AsString();
					}
					default: 
					{
						return "";
					}
				} //switch;
				
			}
		}

		/// <summary>
		/// Returns the value as a String
		/// </summary>
		public int IntegerValue
		{
			get
			{
				switch (Datatype)
				{
					case MetaData.DataTypes.dtSingle:
					{
						return Convert.ToInt32(AsSingle());
					}
					case MetaData.DataTypes.dtInteger:
					{
						return AsInteger();
					}
					case MetaData.DataTypes.dtString:
					{
						int ret = 0;
						try 
						{
							ret = int.Parse(AsString());
						} 
						catch (Exception){}
						return ret;
					}
					default: 
					{
						return 0;
					}
				} //switch;
				
			}
		}

		/// <summary>
		/// Returns the value as a Single Floatingpoint (4Bytes)
		/// </summary>
		public Single SingleValue
		{
			get
			{
				switch (Datatype)
				{
					case MetaData.DataTypes.dtSingle:
					{
						return AsSingle();
					}
					case MetaData.DataTypes.dtInteger:
					{
						return AsInteger();
					}
					case MetaData.DataTypes.dtString:
					{
						Single ret = 0;
						try 
						{
							ret = Single.Parse(AsString());
						} 
						catch (Exception){}
						return ret;
					}
					default: 
					{
						return 0;
					}
				} //switch;
				
			}
		}
		#endregion

		#region internal value Handling
		/// <summary>
		/// Interpretes the data as an Integer Value
		/// </summary>
		/// <returns>The Value interpreted as Integer</returns>
		protected int AsInteger()
		{
			int ret = 0;
			for (int i=Value.Length-1; i>=0; i--) ret = (ret*0xff) + Value[i];

			return ret;
		}

		/// <summary>
		/// Interpretes the data as an String Value
		/// </summary>
		/// <returns>The Value interpreted as String</returns>
		protected string AsString()
		{
			string ret = "";
			for (int i=0; i<Value.Length; i++) ret += ((char)Value[i]).ToString();

			return ret;
		}

		/// <summary>
		/// Interpretes the data as a SingleFloat Value
		/// </summary>
		/// <returns>The Value interpreted as Singel</returns>
		protected Single AsSingle()
		{
			System.IO.BinaryReader br = new System.IO.BinaryReader(new System.IO.MemoryStream(Value));
			return br.ReadSingle();
		}
		#endregion

		#region Generic.ExtendedFileItem Extensions
		/// <summary>
		/// Constructor for the class
		/// </summary>
		/// <param name="item">The Generic.FileItem Object</param>
		CpfItemG(GenericCommon item) : base(item) 
		{
			Base.NameDelegate = new GenericCommon.GetAlternativeNames(GetNames);
		}

		/// <summary>
		/// This is used so you can easily create a new Object by assigning  a Generic.FileItem Object
		/// </summary>
		/// <param name="item">The FileItem you want to convert from</param>
		/// <returns>The new FileItem Object</returns>
		public static implicit operator CpfItemG(GenericItem item)
		{
			return new CpfItemG(item);
		}

		/// <summary>
		/// This is used so you can easily create a new Object by assigning  a Generic.FileItem Object
		/// </summary>
		/// <param name="item">The Common Object you want to convert from</param>
		/// <returns>The new ExtendedFileItem Object</returns>
		/// <remarks>Every derived class should Implement this for it's implementation!</remarks>
		public static implicit operator CpfItemG(GenericCommon item)
		{
			return new CpfItemG(item);
		}	
		#endregion

	}
}
