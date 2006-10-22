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

namespace SimPe.PackedFiles.Wrapper
{
	/// <summary>
	/// This is the actual FileWrapper
	/// </summary>
	/// <remarks>
	/// The wrapper is used to (un)serialize the Data of a file into it's Attributes. So Basically it reads 
	/// a BinaryStream and translates the data into some userdefine Attributes.
	/// </remarks>
	public class Ttab
		: pjse.ExtendedWrapper //AbstractWrapper				//Implements some of the default Behaviur of a Handler, you can Implement yourself if you want more flexibility!
		, IFileWrapper					//This Interface is used when loading a File
		, IFileWrapperSaveExtension		//This Interface (if available) will be used to store a File
		//,IPackedFileProperties		//This Interface can be used by thirdparties to retrive the FIleproperties, however you don't have to implement it!
	{
		#region Attributes
		private byte[] filename = new byte[64];
		private uint[] header = { 0xffffffff, 0x00000054, 0x00000000 };
		private TtabItemArrayList items = new TtabItemArrayList();
		private byte[] footer = new byte[0];
		#endregion

		#region Accessor methods
		public string FileName 
		{
			get { return Helper.ToString(filename); }
			set 
			{
				if (!Helper.ToString(filename).Equals(value))
				{
					filename = Helper.ToBytes(value, 0x40);
					OnWrapperChanged(this, new EventArgs());
				}
			}
		}
		public uint Format 
		{
			get { return header[1]; }
			set
			{
				if (header[1] != value )
				{
					header[1] = value;
					OnWrapperChanged(this, new EventArgs());
				}
			}
		}
        public uint Unknown
        {
            get { return header[2]; }
            set
            {
                if (header[2] != value)
                {
                    header[2] = value;
                    OnWrapperChanged(this, new EventArgs());
                }
            }
        }
        #endregion

		/// <summary>
		/// Constructor
		/// </summary>
		public Ttab() : base() { }


		#region AbstractWrapper Member
		public override bool CheckVersion(uint version) 
		{
			if ( (version==0012) //0.00
				|| (version==0013) //0.10
				) 
			{
				return true;
			}

			return false;
		}
		protected override IPackedFileUI CreateDefaultUIHandler()
		{
			return new UserInterface.TtabForm();
		}

		/// <summary>
		/// Returns a Human Readable Description of this Wrapper
		/// </summary>
		/// <returns>Human Readable Description</returns>
		protected override IWrapperInfo CreateWrapperInfo()
		{
			///
			/// TODO: Change the Description passed here
			/// 
			return new AbstractWrapperInfo(
				"PJSE TTAB Wrapper",
				"Peter L Jones",
				"Tree Table Editor",
				1
				); 
		}

		/// <summary>
		/// Serializes a the Attributes stored in this Instance to the BinaryStream
		/// </summary>
		/// <param name="writer">The Stream the Data should be stored to</param>
		/// <remarks>
		/// Be sure that the Position of the stream is Proper on 
		/// return (i.e. must point to the first Byte after your actual File)
		/// </remarks>
		protected override void Serialize(System.IO.BinaryWriter writer)
		{
			writer.Write(filename);
			writer.Write(header[0]);
			writer.Write(header[1]);
			writer.Write(header[2]);

			writer.Write((ushort)items.Count);

			for (int i = 0; i < items.Count; i++)
				if (items[i] != null) ((TtabItem)items[i]).Serialize(writer);

			writer.Write(footer);
		}
		/// <summary>
		/// Unserializes a BinaryStream into the Attributes of this Instance
		/// </summary>
		/// <param name="reader">The Stream that contains the FileData</param>
		protected override void Unserialize(System.IO.BinaryReader reader)
		{
			// in case we give up...
			items = null;
			footer = new byte[0];

			filename = reader.ReadBytes(0x40);

			header = new uint[3];
			header[0] = reader.ReadUInt32();
			if (header[0] != 0xffffffff)
				throw new Exception("Unexpected data in TTAB header."
                    + "  Read 0x" + SimPe.Helper.HexString(header[0]) + "."
                    + "  Expected 0xFFFFFFFF.");
			header[1] = reader.ReadUInt32();
			header[2] = reader.ReadUInt32();

			ushort itemCount = reader.ReadUInt16();

			TtabItem[] ti = new TtabItem[itemCount];
			items = new TtabItemArrayList(ti);
			for (int i = 0; i < itemCount; i++)
				items[i] = new TtabItem(this, reader);

			footer = reader.ReadBytes((int)(reader.BaseStream.Length - reader.BaseStream.Position));
		}

		#endregion

		#region IFileWrapper Member
		/// <summary>
		/// Returns a list of File Type this Plugin can process
		/// </summary>
		public uint[] AssignableTypes
		{
			get
			{
				uint[] types = {0x54544142}; //handles the TTAB File
				return types;
			}
		}

		/// <summary>
		/// Returns the Signature that can be used to identify Files processable with this Plugin
		/// </summary>
		public byte[] FileSignature
		{
			get
			{
				return new byte[0];
			}
		}

		#endregion		

		#region IFileWrapperSaveExtension Member		
		//all covered by AbstractWrapper
		#endregion

		#region ICollection Members
		public int Add(TtabItem item)
		{
			if (items.Count >= 0x8000) // only allow 32K lines
				return -1;

			item.Parent = this;
			int result = items.Add(item);
			if (result >= 0) OnWrapperChanged(items, new EventArgs());
			return result;
		}

		public void Clear()
		{
			items.Clear();
			OnWrapperChanged(items, new EventArgs());
		}

		public void Remove(TtabItem item) { this.RemoveAt(items.IndexOf(item)); }

		public void RemoveAt(int index)
		{
			if (index < 0 || index >= items.Count) return;

			items.RemoveAt(index);
			OnWrapperChanged(items, new EventArgs());
		}

		public TtabItem this[int index]
		{
			get
			{
				return items[index];
			}
			set
			{
				if (items[index] == null || !items[index].Equals(value))
				{
					value.Parent = this;
					items[index] = value;
					OnWrapperChanged(items, new EventArgs());
				}
			}
		}

		public bool Contains(TtabItem item) { return items.Contains(item); }

		public int IndexOf(object item) { return items.IndexOf(item); }

		public override void CopyTo(Array a, int i) { items.CopyTo(a, i); }

		public override int Count { get { return items.Count; } }

		public override bool IsSynchronized { get { return items.IsSynchronized; } }

		public override object SyncRoot { get { return items.SyncRoot; } }

		#region IEnumerable Members
		public override IEnumerator GetEnumerator() { return items.GetEnumerator(); }

		#endregion
		#endregion

		#region TtabItemArrayList
		private class TtabItemArrayList : ArrayList
		{
			public TtabItemArrayList() : base() { }

			public TtabItemArrayList(TtabItem[] c) : base(c) { }

			public TtabItemArrayList(int capacity) : base(capacity) { }

			public new TtabItem this[int index]
			{
				get { return (TtabItem)base[index]; }
				set { base[index] = value; }
			}

		}

		#endregion
    }


	/// <summary>
	/// An Item stored in an TTAB
	/// </summary>
	public class TtabItem
	{
		#region Attributes
		private ushort action = 0;
		private ushort guard = 0;
		private TtabFlags flags = null;
		private ushort flags2 = 0;
		private uint strindex = 0;
		private uint attenuationcode = 0;
		private float attenuationvalue = 0f;
		private uint autonomy = 0;
		private uint joinindex = 0;
		private ushort uidisplaytype = 0;
		private uint facialanimation = 0;
		private float memoryitermult = 0f;
		private uint objecttype = 0;
		private uint modeltableid = 0;
        private TtabItemMotiveTable humanGroups = null;
        private TtabItemMotiveTable animalGroups = null;

        private Ttab parent = null;
		#endregion

		#region Accessor Methods
		public ushort Action
		{
			get { return action; }
			set {
				if (action != value)
				{
					action = value;
					parent.OnWrapperChanged(this, new EventArgs());
				}
			}
		}

		public ushort Guardian
		{
			get { return guard; }
			set
			{
				if (guard != value)
				{
					guard = value;
					parent.OnWrapperChanged(this, new EventArgs());
				}
			}
		}		

		public TtabFlags Flags
		{
			get { return flags; }
			set
			{
				if (flags != value)
				{
					flags = value;
					parent.OnWrapperChanged(this, new EventArgs());
				}
			}
		}

		public ushort Flags2
		{
			get { return flags2; }
			set
			{
				if (flags2 != value)
				{
					flags2 = value;
					parent.OnWrapperChanged(this, new EventArgs());
				}
			}
		}

		public uint StringIndex
		{
			get { return strindex; }
			set
			{
				if (strindex != value)
				{
					strindex = value;
					parent.OnWrapperChanged(this, new EventArgs());
				}
			}
		}

		public uint AttenuationCode
		{
			get { return attenuationcode; }
			set
			{
				if (attenuationcode != value)
				{
					attenuationcode = value;
					parent.OnWrapperChanged(this, new EventArgs());
				}
			}
		}

		public float AttenuationValue
		{
			get { return attenuationvalue; }
			set
			{
				if (attenuationvalue != value)
				{
					attenuationvalue = value;
					parent.OnWrapperChanged(this, new EventArgs());
				}
			}
		}		

		public uint Autonomy
		{
			get { return autonomy; }
			set
			{
				if (autonomy != value)
				{
					autonomy = value;
					parent.OnWrapperChanged(this, new EventArgs());
				}
			}
		}

		public uint JoinIndex
		{
			get { return joinindex; }
			set
			{
				if (joinindex != value)
				{
					joinindex = value;
					parent.OnWrapperChanged(this, new EventArgs());
				}
			}
		}

		public ushort UIDisplayType
		{
			get { return uidisplaytype; }
			set
			{
				if (uidisplaytype != value)
				{
					uidisplaytype = value;
					parent.OnWrapperChanged(this, new EventArgs());
				}
			}
		}

		public uint FacialAnimationID
		{
			get { return facialanimation; }
			set
			{
				if (facialanimation != value)
				{
					facialanimation = value;
					parent.OnWrapperChanged(this, new EventArgs());
				}
			}
		}

		public float MemoryIterativeMultiplier
		{
			get { return memoryitermult; }
			set
			{
				if (!memoryitermult.Equals(value))
				{
					memoryitermult = value;
					parent.OnWrapperChanged(this, new EventArgs());
				}
			}
		}

		public uint ObjectType
		{
			get { return objecttype; }
			set
			{
				if (objecttype != value)
				{
					objecttype = value;
					parent.OnWrapperChanged(this, new EventArgs());
				}
			}
		}

		public uint ModelTableID
		{
			get { return modeltableid; }
			set
			{
				if (modeltableid != value)
				{
					modeltableid = value;
					parent.OnWrapperChanged(this, new EventArgs());
				}
			}
		}

        public TtabItemMotiveTable HumanMotives
        {
            get { return humanGroups; }
            set
            {
                if (humanGroups != value)
                {
                    humanGroups = value;
                    parent.OnWrapperChanged(this, new EventArgs());
                }
            }
        }

        public TtabItemMotiveTable AnimalMotives
        {
            get { return animalGroups; }
            set
            {
                if (animalGroups != value)
                {
                    animalGroups = value;
                    parent.OnWrapperChanged(this, new EventArgs());
                }
            }
        }

		public Ttab Parent
		{
			get { return parent; }
			set { parent = value; } // parent not part of wrapper
		}
		#endregion

		public TtabItem(Ttab parent)
		{
			this.parent = parent;
			flags = new TtabFlags(parent, 0);

            humanGroups = new TtabItemMotiveTable(this, null, TtabItemMotiveTableType.Human);
            if (parent.Format >= 0x54)
                animalGroups = new TtabItemMotiveTable(this, null, TtabItemMotiveTableType.Animal);
		}

		public TtabItem(Ttab parent, System.IO.BinaryReader reader)
		{
			this.parent = parent;
			Unserialize(reader);
		}


		public TtabItem Clone()
		{
			TtabItem clone = new TtabItem(this.parent);
			clone.action = this.action;
			clone.guard = this.guard;
			clone.flags = this.flags.Clone();
			clone.flags2 = this.flags2;
			clone.strindex = this.strindex;
			clone.attenuationcode = this.attenuationcode;
			clone.attenuationvalue = this.attenuationvalue;
			clone.autonomy = this.autonomy;
			clone.joinindex = this.joinindex;
			clone.uidisplaytype = this.uidisplaytype;
			clone.facialanimation = this.facialanimation;
			clone.memoryitermult = this.memoryitermult;
			clone.objecttype = this.objecttype;
			clone.modeltableid = this.modeltableid;
            clone.humanGroups = this.humanGroups == null ? null : this.humanGroups.Clone();
            clone.animalGroups = this.animalGroups == null ? null : this.animalGroups.Clone();

			return clone;
		}

		/// <summary>
		/// Reads Data from the Stream
		/// </summary>
		/// <param name="reader"></param>
		private void Unserialize(System.IO.BinaryReader reader)
		{
			action = reader.ReadUInt16();
			guard = reader.ReadUInt16();

            uint nGroups = 0;
            if (parent.Format < 0x44) nGroups = 1;
            else if (parent.Format < 0x54) nGroups = 7;

            int[] counts = nGroups > 0 ? new int[nGroups] : null;
            for (int i = 0; i < nGroups; i++)
				counts[i] = reader.ReadInt32();

			flags = new TtabFlags(parent, reader.ReadUInt16());
			flags2 = reader.ReadUInt16();

			strindex = reader.ReadUInt32();
			attenuationcode = reader.ReadUInt32();
			attenuationvalue = reader.ReadSingle(); //float
			autonomy = reader.ReadUInt32();
			joinindex = reader.ReadUInt32();

			uidisplaytype = 0;
			facialanimation = 0;
			memoryitermult = 0f;
			objecttype = 0;
			modeltableid = 0;
            if (parent.Format > 0x44)
            {
                uidisplaytype = reader.ReadUInt16();
                if (parent.Format >= 0x46)
                {
                    if (parent.Format >= 0x4a)
                    {
                        facialanimation = reader.ReadUInt32();
                        if (parent.Format >= 0x4c)
                        {
                            memoryitermult = reader.ReadSingle(); //float
                            objecttype = reader.ReadUInt32();
                        }
                    }
                    modeltableid = reader.ReadUInt32();
                }
            }

            humanGroups = new TtabItemMotiveTable(this, counts, TtabItemMotiveTableType.Human, reader);
            if (parent.Format >= 0x54)
                animalGroups = new TtabItemMotiveTable(this, null, TtabItemMotiveTableType.Animal, reader);
		}

		/// <summary>
		/// Writes Data to the Stream
		/// </summary>
		/// <param name="reader"></param>
		internal void Serialize(System.IO.BinaryWriter writer)
		{
			writer.Write(action);
			writer.Write(guard);

            uint nGroups = 0;
            if (parent.Format < 0x44) nGroups = 1;
            else if (parent.Format < 0x54) nGroups = 7;
            for (int i = 0; i < nGroups; i++) writer.Write(i < humanGroups.Count ? humanGroups[i].Count : 0);

			writer.Write(flags.Value);
			writer.Write(flags2);
			writer.Write(strindex);
			writer.Write(attenuationcode);
			writer.Write(attenuationvalue);
			writer.Write(autonomy);
			writer.Write(joinindex);

			if (parent.Format >0x44) 
			{
				writer.Write(uidisplaytype);
				if (parent.Format >= 0x46)
				{
					if (parent.Format >= 0x4a) 
					{
						writer.Write(facialanimation);
						if (parent.Format >= 0x4c)
						{
							writer.Write(memoryitermult);
							writer.Write(objecttype);
						}
					}
					writer.Write(modeltableid);
				}
			}
            humanGroups.Serialize(writer);
            if (parent.Format >= 0x54)
                animalGroups.Serialize(writer);
		}
    }

    public class TtabItemMotiveTable : ICollection
    {
        #region Attributes
        private TtabItem parent;
        private int[] counts = null;
        private TtabItemMotiveTableType type;
        private TtabItemMotiveGroupArrayList items = null;
        #endregion

        #region Accessor Methods
        public TtabItem Parent
        {
            get { return parent; }
            set { this.parent = value; }
        }
        public Ttab Wrapper { get { return parent.Parent; } }
        public TtabItemMotiveTableType Type
        {
            get { return type; }
            set
            {
                if (type != value)
                {
                    type = value;
                    Wrapper.OnWrapperChanged(this, new EventArgs());
                }
            }
        }
        #endregion


        public TtabItemMotiveTable(TtabItem parent, int[] counts, TtabItemMotiveTableType type)
		{
            this.parent = parent;
            this.counts = counts;
            this.type = type;

            if (Wrapper.Format >= 0x54 && counts != null)
                throw new Exception("'counts' must be null for Format >= 0x54");
		}

        public TtabItemMotiveTable(TtabItem parent, int[] counts, TtabItemMotiveTableType type, System.IO.BinaryReader reader)
            : this(parent, counts, type) { Unserialize(reader); }

        private void Unserialize(System.IO.BinaryReader reader)
        {
            int nrGroups = 0;
            if (Wrapper.Format < 0x44) nrGroups = 1;
            else if (Wrapper.Format < 0x54) nrGroups = 7;
            else nrGroups = reader.ReadInt32();

            items = new TtabItemMotiveGroupArrayList(new TtabItemMotiveGroup[nrGroups]);
            for (int i = 0; i < nrGroups; i++)
                items[i] = new TtabItemMotiveGroup(this, counts != null ? counts[i] : 0, type, reader);
        }

        internal void Serialize(System.IO.BinaryWriter writer)
        {
            if (Wrapper.Format >= 0x54)
                writer.Write(items.Count);
            for (int i = 0; i < items.Count; i++)
                items[i].Serialize(writer);
        }

        public TtabItemMotiveTable Clone()
        {
            TtabItemMotiveTable clone = new TtabItemMotiveTable(parent, counts, type);
            clone.items = items == null ? null : items.Clone();
            return clone;
        }


        #region TtabItemMotiveGroupArrayList
        private class TtabItemMotiveGroupArrayList : ArrayList
        {
			public TtabItemMotiveGroupArrayList() : base() { }

            public TtabItemMotiveGroupArrayList(TtabItemMotiveGroup[] c) : base(c) { }

            public TtabItemMotiveGroupArrayList(int capacity) : base(capacity) { }

            public new TtabItemMotiveGroup this[int index]
			{
                get { return (TtabItemMotiveGroup)base[index]; }
				set { base[index] = value; }
			}

            public new TtabItemMotiveGroupArrayList Clone()
            {
                TtabItemMotiveGroupArrayList clone = new TtabItemMotiveGroupArrayList();
                foreach (TtabItemMotiveGroup item in this)
                    clone.Add(item);
                return clone;
            }

        }
        #endregion

        #region ICollection Members
        public int Add(TtabItemMotiveGroup item)
        {
            //if (items.Count >= 0x08) // we don't really know...
                //return -1;

            item.Parent = this;
            int result = items.Add(item);
            if (result >= 0) Wrapper.OnWrapperChanged(items, new EventArgs());
            return result;
        }

        public void Clear()
        {
            items.Clear();
            Wrapper.OnWrapperChanged(items, new EventArgs());
        }

        public void Remove(TtabItemMotiveGroup item) { this.RemoveAt(items.IndexOf(item)); }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= items.Count) return;

            items.RemoveAt(index);
            Wrapper.OnWrapperChanged(items, new EventArgs());
        }

        public TtabItemMotiveGroup this[int index]
        {
            get
            {
                return items[index];
            }
            set
            {
                if (items[index] == null || !items[index].Equals(value))
                {
                    value.Parent = this;
                    items[index] = value;
                    Wrapper.OnWrapperChanged(items, new EventArgs());
                }
            }
        }

        public bool Contains(TtabItem item) { return items.Contains(item); }

        public int IndexOf(object item) { return items.IndexOf(item); }

        public void CopyTo(Array a, int i) { items.CopyTo(a, i); }

        public int Count { get { return items.Count; } }

        public bool IsSynchronized { get { return items.IsSynchronized; } }

        public object SyncRoot { get { return items.SyncRoot; } }

        #region IEnumerable Members
        public IEnumerator GetEnumerator() { return items.GetEnumerator(); }

        #endregion
        #endregion

    }

    public class TtabItemMotiveGroup : ICollection
    {
        #region Attributes
        private TtabItemMotiveTable parent = null;
        private int count = 0;
        private TtabItemMotiveTableType type = TtabItemMotiveTableType.Human;
        private int unknown = 0;
        private TtabItemMotiveItemArrayList items = null;
        #endregion

        #region Accessor Methods
        public Ttab Wrapper { get { return parent.Parent.Parent; } }
        public TtabItemMotiveTable Parent
        {
            get { return parent; }
            set { parent = value; }
        }
        public int Unknown
        {
            get { return unknown; }
            set
            {
                if (unknown != value)
                {
                    unknown = value;
                    Wrapper.OnWrapperChanged(this, new EventArgs());
                }
            }
        }
        #endregion

        public TtabItemMotiveGroup(TtabItemMotiveTable parent, int count, TtabItemMotiveTableType type)
        {
            this.parent = parent;
            this.count = count;
            this.type = type;
        }

        public TtabItemMotiveGroup(TtabItemMotiveTable parent, int count, TtabItemMotiveTableType type, System.IO.BinaryReader reader)
            : this(parent, count, type) { Unserialize(reader); }

        private void Unserialize(System.IO.BinaryReader reader)
        {
            if (Wrapper.Format >= 0x54)
            {
                count = reader.ReadInt32();
                if (type == TtabItemMotiveTableType.Animal)
                    unknown = reader.ReadInt32();
            }

            items = new TtabItemMotiveItemArrayList(new TtabItemMotiveItem[count < 16 ? 16 : count]);
            for (int i = 0; i < count; i++)
                items[i] = new TtabItemMotiveItem(this, reader);
            for (int i = count; i < 16; i++)
                items[i] = new TtabItemMotiveItem(this);
        }

        internal void Serialize(System.IO.BinaryWriter writer)
        {
            if (Wrapper.Format >= 0x54)
            {
                writer.Write(items.Count);
                if (type == TtabItemMotiveTableType.Animal)
                    writer.Write(unknown);
            }

            for (int i = 0; i < items.Count; i++)
                items[i].Serialize(writer);
        }


        private class TtabItemMotiveItemArrayList : ArrayList
        {
			public TtabItemMotiveItemArrayList() : base() { }

            public TtabItemMotiveItemArrayList(TtabItemMotiveItem[] c) : base(c) { }

            public TtabItemMotiveItemArrayList(int capacity) : base(capacity) { }

            public new TtabItemMotiveItem this[int index]
			{
                get { return (TtabItemMotiveItem)base[index]; }
				set { base[index] = value; }
			}

            /// <summary>
            /// Creates a deep copy of the TtabItemMotiveItemArrayList
            /// </summary>
            public new TtabItemMotiveItemArrayList Clone()
            {
                TtabItemMotiveItemArrayList clone = new TtabItemMotiveItemArrayList();
                foreach (TtabItemMotiveItem item in this)
                    clone.Add(item.Clone());
                return clone;
            }

        }

        public TtabItemMotiveGroup Clone()
        {
            TtabItemMotiveGroup clone = new TtabItemMotiveGroup(parent, count, type);
            clone.unknown = unknown;
            clone.items = items == null ? null : items.Clone();
            return clone;
        }

        #region ICollection Members
        public int Add(TtabItemMotiveItem item)
        {
            //if (items.Count >= 0x10) // we don't really know...
            //return -1;

            item.Parent = this;
            int result = items.Add(item);
            if (result >= 0) Wrapper.OnWrapperChanged(items, new EventArgs());
            return result;
        }

        public void Clear()
        {
            items.Clear();
            Wrapper.OnWrapperChanged(items, new EventArgs());
        }

        public void Remove(TtabItemMotiveItem item) { this.RemoveAt(items.IndexOf(item)); }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= items.Count) return;

            items.RemoveAt(index);
            Wrapper.OnWrapperChanged(items, new EventArgs());
        }

        public TtabItemMotiveItem this[int index]
        {
            get
            {
                return items[index];
            }
            set
            {
                if (items[index] == null || !items[index].Equals(value))
                {
                    value.Parent = this;
                    items[index] = value;
                    Wrapper.OnWrapperChanged(items, new EventArgs());
                }
            }
        }

        public bool Contains(TtabItem item) { return items.Contains(item); }

        public int IndexOf(object item) { return items.IndexOf(item); }

        public void CopyTo(Array a, int i) { items.CopyTo(a, i); }

        public int Count { get { return items.Count; } }

        public bool IsSynchronized { get { return items.IsSynchronized; } }

        public object SyncRoot { get { return items.SyncRoot; } }

        #region IEnumerable Members
        public IEnumerator GetEnumerator() { return items.GetEnumerator(); }

        #endregion
        #endregion
    }

    public class TtabItemMotiveItem
    {
        #region Attributes
        private TtabItemMotiveGroup parent = null;
        private short[] items = null;
        #endregion

        #region Accessor Methods
        public TtabItemMotiveGroup Parent
        {
            get { return parent; }
            set { parent = value; }
        }
        public short Min
        {
            get { return this[0]; }
            set { this[0] = value; }
        }
        public short Delta
        {
            get { return items[1]; }
            set { this[1] = value; }
        }
        public short Type
        {
            get { return items[2]; }
            set { this[2] = value; }
        }
        #endregion

        public Ttab Wrapper { get { return parent.Wrapper; } }

        public TtabItemMotiveItem(TtabItemMotiveGroup parent)
        {
            this.parent = parent;
            items = new short[3];
        }

        public TtabItemMotiveItem(TtabItemMotiveGroup parent, System.IO.BinaryReader reader)
            : this(parent) { Unserialize(reader); }

        private void Unserialize(System.IO.BinaryReader reader)
        {
            for (int i = 0; i < items.Length; i++)
                items[i] = reader.ReadInt16();
        }

        internal void Serialize(System.IO.BinaryWriter writer)
        {
            for (int i = 0; i < items.Length; i++)
                writer.Write(items[i]);
        }

        private short this[int index]
        {
            get { return items[index]; }
            set
            {
                if (items[index] != value)
                {
                    items[index] = value;
                    Wrapper.OnWrapperChanged(items, new EventArgs());
                }
            }
        }

        public TtabItemMotiveItem Clone()
        {
            TtabItemMotiveItem clone = new TtabItemMotiveItem(parent);
            clone.items = new short[3];
            items.CopyTo(clone.items, 0);
            return clone;
        }
    }

	#region Flags and Enums
	public class TtabFlags : FlagBase
	{
        #region Attributes
        private Ttab parent;
        #endregion

        #region Accessor Methods
        public bool ByVisitors
        {
            get { return GetBit(0); }
            set { SetBit(0, value); parent.OnWrapperChanged(this, new EventArgs()); }
        }

        public bool Joinable
        {
            get { return GetBit(1); }
            set { SetBit(1, value); parent.OnWrapperChanged(this, new EventArgs()); }
        }

        public bool RunImmediately
        {
            get { return GetBit(2); }
            set { SetBit(2, value); parent.OnWrapperChanged(this, new EventArgs()); }
        }

        public bool AvailConsecutive
        {
            get { return GetBit(3); }
            set { SetBit(3, value); parent.OnWrapperChanged(this, new EventArgs()); }
        }

        public bool ByChildren
        {
            get { return !GetBit(4); }
            set { SetBit(4, !value); parent.OnWrapperChanged(this, new EventArgs()); }
        }

        public bool ByDemoChild
        {
            get { return !GetBit(5); }
            set { SetBit(5, !value); parent.OnWrapperChanged(this, new EventArgs()); }
        }

        public bool ByAdults
        {
            get { return !GetBit(6); }
            set { SetBit(6, !value); parent.OnWrapperChanged(this, new EventArgs()); }
        }

        public bool DebugMenu
        {
            get { return GetBit(7); }
            set { SetBit(7, value); parent.OnWrapperChanged(this, new EventArgs()); }
        }

        public bool AutoFirstSelect
        {
            get { return GetBit(8); }
            set { SetBit(8, value); parent.OnWrapperChanged(this, new EventArgs()); }
        }

        public bool ByToddlers
        {
            get { return GetBit(9); }
            set { SetBit(9, value); parent.OnWrapperChanged(this, new EventArgs()); }
        }

        public bool ByElders
        {
            get { return GetBit(10); }
            set { SetBit(10, value); parent.OnWrapperChanged(this, new EventArgs()); }
        }

        public bool ByTeens
        {
            get { return GetBit(11); }
            set { SetBit(11, value); parent.OnWrapperChanged(this, new EventArgs()); }
        }

        public bool Unknown1
        {
            get { return GetBit(12); }
            set { SetBit(12, value); parent.OnWrapperChanged(this, new EventArgs()); }
        }

        public bool Unknown2
        {
            get { return GetBit(13); }
            set { SetBit(13, value); parent.OnWrapperChanged(this, new EventArgs()); }
        }

        public bool Unknown3
        {
            get { return GetBit(14); }
            set { SetBit(14, value); parent.OnWrapperChanged(this, new EventArgs()); }
        }

        public bool Unknown4
        {
            get { return GetBit(15); }
            set { SetBit(15, value); parent.OnWrapperChanged(this, new EventArgs()); }
        }
        #endregion

        public TtabFlags(Ttab parent, ushort flags) : base(flags) { this.parent = parent; }

		public new ushort Value
		{
			get { return base.Value; }
			set
			{
				if (base.Value != value)
				{
					base.Value = value;
					parent.OnWrapperChanged(this, new EventArgs());
				}
			}
		}

		public TtabFlags Clone()
		{
			return new TtabFlags(parent, Value);
		}
	}

    public enum TtabItemMotiveTableType : int
    {
        Human, Animal
    }

	#endregion
}
