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
using System.Collections;

namespace SimPe.Interfaces.Plugin
{
	/// <summary>
	/// This interface is needed if third_party Applications should be able to 
	/// use your wrapper.
	/// </summary>
	/// <remarks>Enables the users of your Wrapper to acces the FIle Attribute sin a genuin way</remarks>
	public interface IPackedFileProperties
	{
		/// <summary>
		/// Returns all Attributes tored in the File.
		/// </summary>
		/// <remarks>Each Attribute is unique!</remarks>
		Hashtable Attributes
		{
			get;
		}

		/// <summary>
		/// Returns all Items stored in the File (can be null)
		/// </summary>
		/// <remarks>
		/// All Items returned here have the same structure, 
		/// however each Item can have SubItmes of it's own.
		/// 
		/// If null is returned, no Items are provided by this File
		/// </remarks>
		IPackedFileProperties[] Items 
		{
			get;
		}
	}
}
