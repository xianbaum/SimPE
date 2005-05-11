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
/*using System;
using SimPe.Data;

namespace SimPe.PackedFiles.Wrapper
{
	/// <summary>
	/// A Listing Item
	/// </summary>
	public class DirectoryListItem : GenericExtendedItem
	{				
		/// <summary>
		/// Returns the Type of the referenced File
		/// </summary>
		public uint Type
		{
			get 
			{
				return Convert.ToUInt32(Base.Properties["Type"]);
			}
		}

		/// <summary>
		/// Returns the Group the referenced file is assigned to
		/// </summary>
		public uint Group
		{
			get 
			{
				return Convert.ToUInt32(Base.Properties["Group"]);
			}
		}

		/// <summary>
		/// Returns the Instance Data
		/// </summary>
		public uint Instance
		{
			get 
			{
				return Convert.ToUInt32(Base.Properties["Instance"]);
			}
		}

		/// <summary>
		/// Returns an yet unknown Type
		/// </summary>		
		/// <remarks>Only in Version 1.1 of package Files</remarks>
		public uint SubType
		{
			get 
			{
				return Convert.ToUInt32(Base.Properties["Subtype"]);
			}
		}

		/// <summary>
		/// Returns the (real) uncompressed Size of the File
		/// </summary>
		public uint UncompressedSize 
		{
			get
			{
				return Convert.ToUInt32(Base.Properties["Uncsize"]);
			}
		}

		#region Generic.ExtendedFileItem Extensions
		/// <summary>
		/// Constructor for the class
		/// </summary>
		/// <param name="item">The Generic.FileItem Object</param>
		DirectoryListItem(GenericCommon item) : base(item) {}

		/// <summary>
		/// This is used so you can easily create a new Object by assigning  a Generic.FileItem Object
		/// </summary>
		/// <param name="item">The FileItem you want to convert from</param>
		/// <returns>The new FileItem Object</returns>
		public static implicit operator DirectoryListItem(GenericItem item)
		{
			return new DirectoryListItem(item);
		}

		/// <summary>
		/// This is used so you can easily create a new Object by assigning  a Generic.FileItem Object
		/// </summary>
		/// <param name="item">The Common Object you want to convert from</param>
		/// <returns>The new ExtendedFileItem Object</returns>
		/// <remarks>Every derived class should Implement this for it's implementation!</remarks>
		public static implicit operator DirectoryListItem(GenericCommon item)
		{
			return new DirectoryListItem(item);
		}
		#endregion
	}
}*/
