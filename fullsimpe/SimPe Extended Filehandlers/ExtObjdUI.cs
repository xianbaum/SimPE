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
using System.Windows.Forms;
using System.Drawing;
using SimPe.Interfaces.Plugin;
using SimPe.Interfaces;

namespace SimPe.PackedFiles.UserInterface
{
	/// <summary>
	/// handles Packed SDSC Files
	/// </summary>
	public class ExtObjdUI : IPackedFileUI
	{
		ExtObjdForm form;

		internal ExtObjdUI ()
		{
			form = new ExtObjdForm();			

			form.cbtype.Items.Add(Data.ObjectTypes.Unknown);
			form.cbtype.Items.Add(Data.ObjectTypes.ArchitecturalSupport);
			form.cbtype.Items.Add(Data.ObjectTypes.Door);
			form.cbtype.Items.Add(Data.ObjectTypes.Memory);
			form.cbtype.Items.Add(Data.ObjectTypes.ModularStairs);
			form.cbtype.Items.Add(Data.ObjectTypes.ModularStairsPortal);
			form.cbtype.Items.Add(Data.ObjectTypes.Normal);
			form.cbtype.Items.Add(Data.ObjectTypes.Outfit);
			form.cbtype.Items.Add(Data.ObjectTypes.Person);
			form.cbtype.Items.Add(Data.ObjectTypes.SimType);
			form.cbtype.Items.Add(Data.ObjectTypes.Stairs);
			form.cbtype.Items.Add(Data.ObjectTypes.Template);
			form.cbtype.Items.Add(Data.ObjectTypes.Vehicle);
			form.cbtype.Items.Add(Data.ObjectTypes.Window);
		}

		#region IPackedFileHandler Member

		public Panel GUIHandle
		{
			get 
			{
				return form.pnobjd;
			}
		}

		public void UpdateGUI(SimPe.Interfaces.Plugin.IFileWrapper wrapper)
		{
			Wrapper.ExtObjd objd = (Wrapper.ExtObjd)wrapper;
			form.wrapper = objd;
			form.initialguid = objd.Guid;
			form.Tag = true;

			try 
			{
				form.pg.SelectedObject = null;
				form.tc.SelectedTab = form.tpcatalogsort;				

				form.cbtype.SelectedIndex = 0;
				for (int i=0; i<form.cbtype.Items.Count; i++)
				{
					Data.ObjectTypes ot = (Data.ObjectTypes)form.cbtype.Items[i];
					if (ot==objd.Type) 
					{
						form.cbtype.SelectedIndex = i;
						break;
					}
				}

				form.tbtype.Text = "0x"+Helper.HexString((ushort)(objd.Type));

				form.tbguid.Text = "0x"+Helper.HexString(objd.Guid);
				form.tbproxguid.Text = "0x"+Helper.HexString(objd.ProxyGuid);
				form.tborgguid.Text = "0x"+Helper.HexString(objd.OriginalGuid);

				form.tbflname.Text = objd.FileName;

				form.cbbathroom.Checked = (objd.RoomSort.InBathroom);
				form.cbbedroom.Checked = (objd.RoomSort.InBedroom);
				form.cbdinigroom.Checked = (objd.RoomSort.InDiningRoom);
				form.cbkitchen.Checked = (objd.RoomSort.InKitchen);
				form.cblivingroom.Checked = (objd.RoomSort.InLivingRoom);
				form.cbmisc.Checked = (objd.RoomSort.InMisc);
				form.cboutside.Checked = (objd.RoomSort.InOutside);
				form.cbstudy.Checked = (objd.RoomSort.InStudy);
				form.cbkids.Checked = (objd.RoomSort.InKids);

				form.cbappliances.Checked = objd.FunctionSort.InAppliances;
				form.cbdecorative.Checked = objd.FunctionSort.InDecorative;
				form.cbelectronics.Checked = objd.FunctionSort.InElectronics;
				form.cbgeneral.Checked = objd.FunctionSort.InGeneral;
				form.cblightning.Checked = objd.FunctionSort.InLighting;
				form.cbplumbing.Checked = objd.FunctionSort.InPlumbing;
				form.cbseating.Checked = objd.FunctionSort.InSeating;
				form.cbsurfaces.Checked = objd.FunctionSort.InSurfaces;
				form.cbhobby.Checked = objd.FunctionSort.InHobbies;
				form.cbaspiration.Checked = objd.FunctionSort.InAspirationRewards;
				form.cbcareer.Checked = objd.FunctionSort.InCareerRewards;
			} 
			finally 
			{
				form.Tag = null;
			}
		}

		
		#endregion
	}
}
