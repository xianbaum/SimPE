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

namespace SimPe.PackedFiles.UserInterface
{
	/// <summary>
	/// Handles CPF Files
	/// </summary>
	public class CpfG : Generic
	{
		protected override string PropertyToString(string name, SimPe.PackedFiles.Wrapper.GenericCommon item, object o)
		{
			if (name=="Value") 
			{
				Wrapper.CpfItemG fi = item;
				switch (fi.Datatype)
				{
					case Data.MetaData.DataTypes.dtInteger:
					{
						return fi.IntegerValue.ToString() + " (0x" + fi.IntegerValue.ToString("X") + ")";
					}
					case Data.MetaData.DataTypes.dtSingle:
					{
						return fi.SingleValue.ToString();
					}
					default:
					{
						return null;
					}
				}//switch
			}
			else 
			{
				return null;
			}
		}

	}
}
