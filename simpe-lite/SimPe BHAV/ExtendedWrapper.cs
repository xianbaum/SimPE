/***************************************************************************
 *   Copyright (C) 2005 by Peter L Jones                                   *
 *   peter@drealm.info                                                     *
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
using SimPe.Interfaces.Plugin;
using SimPe.PackedFiles.Wrapper;
using SimPe.Plugin;

namespace pjse
{
	public abstract class ExtendedWrapper : AbstractWrapper
		, IMultiplePackedFileWrapper	//Allow Multiple Instances
		, ICollection
	{
		/// <summary>
		/// Indicates the data content of the wrapper (packed file) has changed
		/// </summary>
		public event EventHandler WrapperChanged;
		/// <summary>
		/// Indicates a wrapper routine is updating the wrapper and will generate the WrapperChanged event
		/// </summary>
		protected bool internalchg = false;

		public ExtendedWrapper() : base() { }


		internal virtual void OnWrapperChanged(object sender, EventArgs e)
		{
			this.Changed = true;

			if (internalchg) return;
			if (WrapperChanged != null) 
			{
				WrapperChanged(sender, e);
			}
		}


		#region ICollection Members
		public abstract void CopyTo(Array a, int i);
		public abstract int Count { get ; }
		public abstract bool IsSynchronized { get ; }
		public abstract object SyncRoot { get ; }
		#region IEnumerable Members
		public abstract IEnumerator GetEnumerator();
		#endregion
		#endregion

		
		/// <summary>
		/// This object's group
		/// </summary>
		public uint PrivateGroup
        {
            get
            {
                if (Context == Scope.Global || Context == Scope.SemiGlobal)
                    return 0;

                return this.FileDescriptor.Group;
            }
        }

		/// <summary>
		/// The SemiGlobal group for this object
		/// </summary>
		public uint SemiGroup
		{
			get
			{
                if (Context == Scope.Global)
                    return 0;

                Glob glob = BhavWiz.GlobByGroup(this.FileDescriptor.Group);
                return (glob != null ? glob.SemiGlobalGroup : this.FileDescriptor.Group);
            }
		}

		/// <summary>
		/// The Global group
		/// </summary>
		public uint GlobalGroup { get { return (uint)pjse.Group.Global; } }


		public Scope Context
		{
			get
			{
				if (this is Bhav && this.FileDescriptor != null)
				{
					if (this.FileDescriptor.Instance < 0x1000)
						return Scope.Global;
					else if (this.FileDescriptor.Instance < 0x2000)
						return Scope.Private;
					else
						return Scope.SemiGlobal;
				}
				else
					return Scope.Private; // at least for now
			}
		}


        public uint GroupForScope(Scope s)
        {
            if (s == Scope.Global) return GlobalGroup;
            if (s == Scope.SemiGlobal) return SemiGroup;
            return PrivateGroup;
        }


		public uint GroupForContext { get { return GroupForScope(Context); } }


		public pjse.FileTable.Entry ResourceByInstance(uint type, uint instance)
		{
            uint group = PrivateGroup;
            if (type == SimPe.Data.MetaData.BHAV_FILE)
            {
                if (instance < 0x1000) group = GlobalGroup;
                else if (instance >= 0x2000) group = SemiGroup;
            }

			pjse.FileTable.Entry[] items = pjse.FileTable.GFT[type, group, instance];
			return (items == null || items.Length == 0) ? null : items[0];
		}

	}

}
