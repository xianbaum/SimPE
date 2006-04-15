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
using SimPe.PackedFiles.Wrapper;

namespace pjse
{
	/// <summary>
	/// Summary description for Str.
	/// </summary>
	public class Str : IDisposable
	{
		private ExtendedWrapper parent = null;
		private uint group = 0;
		private uint instance = 0;
		public Str(ExtendedWrapper parent, uint group, uint instance)
		{
			this.parent = parent;
			this.group = group;
			this.instance = instance;
		}



		private static myHT wrapperHashtable = new myHT();
		class myHT : Hashtable
		{
			private Hashtable groupHash = new Hashtable();
			public StrWrapper this[uint group, uint instance]
			{
				get
				{
					if (groupHash[group] == null)
						return null;
					Hashtable instanceHash = (Hashtable)groupHash[group];
					if (instanceHash[instance] == null)
						return null;
					return (StrWrapper)instanceHash[instance];
				}

				set
				{
					Hashtable instanceHash = (Hashtable)groupHash[group];
					if (instanceHash == null)
						groupHash[group] = instanceHash = new Hashtable();
					instanceHash[instance] = value;
				}

			}

			public void Invalidate(uint group, uint instance)
			{
				if (groupHash[group] == null) return;
				Hashtable instanceHash = (Hashtable)groupHash[group];
				if (instanceHash[instance] == null) return;
				instanceHash.Remove(instance);
				if (instanceHash.Count == 0)
					groupHash.Remove(group);
			}
		}

		private StrWrapper wrapper = null;
		private StrWrapper Wrapper
		{
			get
			{
				if (wrapper == null)
				{
					if (wrapperHashtable[this.group, this.instance] == null)
					{
						pjse.FileTable.Entry[] items = pjse.FileTable.GFT[(uint)SimPe.Data.MetaData.STRING_FILE, this.group, this.instance];

						if (items != null && items.Length != 0)
						{
							wrapper = new StrWrapper();
							wrapper.ProcessData(items[0].PFD, items[0].Package);
							wrapper.FileDescriptor.ChangedData += new SimPe.Events.PackedFileChanged(FileDescriptor_ChangedData);
							wrapperHashtable[this.group, this.instance] = wrapper;
						}
					}
					else
						wrapper = (StrWrapper)wrapperHashtable[this.group, this.instance];
				}
				return wrapper;
			}
		}


		public void FileDescriptor_ChangedData(SimPe.Interfaces.Files.IPackedFileDescriptor pfd)
		{
			if (pfd == null) return;
			if (pfd.Type != SimPe.Data.MetaData.STRING_FILE) return;
			if (pfd.Group != group) return;
			if (pfd.Instance != instance) return;
			wrapperHashtable.Invalidate(group, instance);
		}


		private Str semiGlobalStr = null;
		private Str SemiGlobalStr
		{
			get
			{
				if (semiGlobalStr == null)
					semiGlobalStr = new Str(parent, parent.SemiGroup, this.instance);
				return semiGlobalStr;
			}
		}


		private Str globalStr = null;
		private Str GlobalStr
		{
			get
			{
				if (globalStr == null)
					globalStr = new Str(parent, parent.GlobalGroup, this.instance);
				return globalStr;
			}
		}



		private bool rejectStrItem(FallbackStrItem fsi)
		{
			if (fsi == null) return true;
			if (fsi.strItem == null) return true;
			if (fsi.strItem.Title.Trim().Length.Equals(0)) return true;
			return false;
		}


		public FallbackStrItem this[int sid] { get { return this[1, sid]; } }

		public FallbackStrItem this[byte lid, int sid]
		{
			get
			{
				FallbackStrItem fsi = new FallbackStrItem();

				if (Wrapper != null)
				{
					fsi.strItem = Wrapper[lid, sid]; // try to find instance/lid/sid at scope
					if (!this.rejectStrItem(fsi))
						return fsi;

					if (lid != 1)
					{
						fsi.strItem = Wrapper[1, sid]; // try to find instance/1/sid at scope
						if (!this.rejectStrItem(fsi))
						{
							if (fsi.fallback.Count == 0)
								fsi.fallback.Add("Fallback: LID=1");
							return fsi;
						}
					}
				}

				if (parent != null)
				{
					if (group != parent.GlobalGroup)
					{
						if (group != parent.SemiGroup && SemiGlobalStr != null)
						{
							fsi = SemiGlobalStr[lid, sid];
							if (!this.rejectStrItem(fsi))
							{
								if (fsi.fallback.Count == 0)
									fsi.fallback.Add("Fallback: SemiGlobal");
								return fsi;
							}
						}

						if (GlobalStr != null)
						{
							fsi = GlobalStr[lid, sid];
							if (!this.rejectStrItem(fsi))
							{
								if (fsi.fallback.Count == 0)
									fsi.fallback.Add("Fallback: Global");
								return fsi;
							}
						}
					}
				}

				return null;
			}
		}


		public static FallbackStrItem getFallbackStrItem(ExtendedWrapper parent, uint group, uint instance, int sid)
		{
			Str str = new Str(parent, group, instance);
			return getFallbackStrItem(parent, group, instance, 1, sid);
		}

		public static FallbackStrItem getFallbackStrItem(ExtendedWrapper parent, uint group, uint instance, byte lid, int sid)
		{
			Str str = new Str(parent, group, instance);
			return str == null ? null : str[lid, sid];
		}


		#region IDisposable Members

		public void Dispose()
		{
			if (this.wrapper != null && this.wrapper.FileDescriptor != null)
				this.wrapper.FileDescriptor.ChangedData -= new SimPe.Events.PackedFileChanged(FileDescriptor_ChangedData);
			this.parent = null;
			this.wrapper = null;
			this.semiGlobalStr = null;
			this.globalStr = null;
		}

		#endregion
	}

	public class FallbackStrItem
	{
		public ArrayList fallback = new ArrayList();
		public StrItem strItem = null;
	}

}
