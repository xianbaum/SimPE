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
using SimPe.Interfaces.Plugin;
using System.Windows.Forms;
using System.Drawing;
using SimPe.Cache;

namespace SimPe.Plugin
{
	/// <summary>
	/// This class is used to fill the UI for this FileType with Data
	/// </summary>
	public class NgbhUI : IPackedFileUI
	{		
		/// <summary>
		/// Returns the MemoryObject Cache
		/// </summary>
		internal static MemoryCacheFile ObjectCache
		{
			get 
			{
				return SimPe.PackedFiles.Wrapper.ObjectComboBox.ObjectCache;
			}
		}
		#region Code to Startup the UI

		/// <summary>
		/// Holds a reference to the Form containing the UI Panel
		/// </summary>
		internal static NgbhForm form;

		/// <summary>
		/// Constructor for the Class
		/// </summary>
		public NgbhUI()
		{
            //form = WrapperFactory.form;
			if (form==null) 
			{
				form = new NgbhForm();
				form.cbguid.Items.Clear();
				form.cbguid.Sorted = false;

				form.cbguid.Items.Add(new Data.Alias(0, "-: "+Localization.Manager.GetString("Unknown"), "{name}"));							

				Wait.Message = ("Load Memories from Cache");
				foreach (MemoryCacheItem mci in ObjectCache.List) 
				{
					Data.Alias a = new SimPe.Data.Alias(mci.Guid, mci.Name);
					object[] o = new object[3];
					o[0] = mci.FileDescriptor;
					o[1] = mci.ObjectType;
					o[2] = mci.Icon;

					a.Tag = o;

					if (mci.ObjectType == Data.ObjectTypes.Memory) 
					{
						form.cbguid.Items.Add(a);
					}
					else if (mci.ObjectType == Data.ObjectTypes.Normal) 
					{
						if (mci.ObjdName.ToLower().IndexOf("token")!=-1) 
							form.cbguid.Items.Add(a);						
					} 					
				}
				form.cbguid.Sorted = true;
			}			
		}
		#endregion

		#region IPackedFileUI Member

		/// <summary>
		/// Returns the Panel that will be displayed within SimPe
		/// </summary>
		public System.Windows.Forms.Control GUIHandle
		{
			get
			{
				return form.ngbhPanel;
			}
		}

		/// <summary>
		/// Is called by SimPe (through the Wrapper) when the Panel is going to be displayed, so
		/// you should updatet the Data displayed by the Panel with the Attributes stored in the
		/// passed Wrapper.
		/// </summary>
		/// <param name="wrapper">The Attributes of this Wrapper have to be displayed</param>
		public void UpdateGUI(IFileWrapper wrapper)
		{
			form.wrapper = (IFileWrapperSaveExtension)wrapper;

			Ngbh wrp = (Ngbh) wrapper;

			form.lv.BeginUpdate();
			form.lv.Items.Clear();
			form.ilist.Images.Clear();
			form.cbsub.Items.Clear();
			form.cbown.Items.Clear();
			form.gbmem.Enabled = false;
			form.lbmem.Items.Clear();

			Interfaces.Files.IPackedFileDescriptor[] pfds = wrp.Package.FindFiles(Data.MetaData.SIM_DESCRIPTION_FILE);
			form.cbsub.Items.Add(new Data.Alias(0, "---", "{name}"));
			form.cbsub.Sorted = false;
			form.cbown.Items.Add(new Data.Alias(0, "---", "{name}"));
			form.cbown.Sorted = false;
			foreach(Interfaces.Files.IPackedFileDescriptor spfd in pfds) 
			{
				PackedFiles.Wrapper.SDesc sdesc = new SimPe.PackedFiles.Wrapper.SDesc(wrp.Provider.SimNameProvider, wrp.Provider.SimFamilynameProvider, null);
                Wait.SubStart();				
				sdesc.ProcessData(spfd, wrp.Package);				
				ListViewItem lvi = new ListViewItem();
                lvi.Text = sdesc.SimName + " " + sdesc.SimFamilyName;
                Data.Alias a = new Data.Alias(sdesc.SimId, lvi.Text, "{name}");
                if (Helper.WindowsRegistry.HiddenMode)
                    lvi.Text += " (0x" + Helper.HexString(sdesc.Instance) + ")";
				lvi.Tag = sdesc;
				a.Tag = new object[1];
				a.Tag[0] = sdesc.Instance;
				form.cbsub.Items.Add(a);
				form.cbown.Items.Add(a);

                Image img = null;
                if (sdesc.Image != null)
                    if (sdesc.Image.Width > 5)
                        img = Ambertation.Drawing.GraphicRoutines.KnockoutImage(sdesc.Image, new Point(0, 0), Color.Magenta);

                if (img == null)
                {
                    if (sdesc.CharacterDescription.IsWoman && sdesc.Nightlife.Species == 0)
                        img = SimPe.GetImage.BabyDoll;
                    else if (sdesc.CharacterDescription.Gender == SimPe.Data.MetaData.Gender.Female)
                        img = SimPe.GetImage.SheOne;
                    else
                        img = SimPe.GetImage.NoOne;
                }

                img = Ambertation.Windows.Forms.Graph.ImagePanel.CreateThumbnail(img, form.ilist.ImageSize, 12, Color.FromArgb(90, Color.Black), SimPe.PackedFiles.Wrapper.SimPoolControl.GetImagePanelColor(sdesc), Color.White, Color.FromArgb(80, Color.White), true, 4, 0);

                form.ilist.Images.Add(img);
                lvi.ImageIndex = form.ilist.Images.Count - 1;
				form.lv.Items.Add(lvi);
			}
			form.cbsub.Sorted = true;
			form.cbown.Sorted = true;
			form.lv.Sort();
			form.lv.EndUpdate();
            Wait.SubStop();
		}
		#endregion
		
		#region IDisposable Member
		public virtual void Dispose() { }
		#endregion
	}
}
