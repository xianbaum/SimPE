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
	public class Objd
		: pjse.ExtendedWrapper //AbstractWrapper				//Implements some of the default Behaviur of a Handler, you can Implement yourself if you want more flexibility!
		, IFileWrapper					//This Interface is used when loading a File
		, IFileWrapperSaveExtension		//This Interface (if available) will be used to store a File
		//,IPackedFileProperties		//This Interface can be used by thirdparties to retrive the FIleproperties, however you don't have to implement it!
	{
		#region Attributes
		/// <summary>
		/// The number of short integers (byte pairs) contained in an OBJD file
		/// </summary>
		private static int bytePairs = 106;
		/// <summary>
		/// Contains the Filename
		/// </summary>
		private byte[] filename = new byte[64];
		/// <summary>
		/// Version / format of the File
		/// </summary>
		private uint version = 0x8B;
		/// <summary>
		/// The object definition data
		/// </summary>
		private ObjdItemArrayList items = new ObjdItemArrayList(bytePairs);
		#endregion

		#region Accessor methods
		/// <summary>
		/// Returns / Sets the Filename
		/// </summary>
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

		/// <summary>
		/// Returns / Sets the Version
		/// </summary>
		public uint Version
		{
			get { return version; }			
			set 
			{
				if (version != value)
				{
					version = value;
					OnWrapperChanged(this, new EventArgs());
				}
			}
		}

		#endregion

		/// <summary>
		/// Constructor
		/// </summary>
		public Objd() : base() { }


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
			return new UserInterface.ObjdForm();
		}

		/// <summary>
		/// Returns a Human Readable Description of this Wrapper
		/// </summary>
		/// <returns>Human Readable Description</returns>
		protected override IWrapperInfo CreateWrapperInfo()
		{
			return new AbstractWrapperInfo(
				"PJSE OBJD Wrapper",
				"Peter L Jones",
				"Object Description Editor",
				1
				); 
		}
		/// <summary>
		/// Serializes the Attributes stored in this Instance to the BinaryStream
		/// </summary>
		/// <param name="writer">The Stream the Data should be stored to</param>
		/// <remarks>
		/// Be sure that the Position of the stream is Proper on 
		/// return (i.e. must point to the first Byte after your actual File)
		/// </remarks>
		protected override void Serialize(System.IO.BinaryWriter writer) 
		{		
			writer.Write(filename);
			writer.Write(version);
			foreach(ObjdItem i in items) i.Serialize(writer);
			Serialize4String(writer, SimPe.Helper.ToString(filename));
		}

		protected override void Unserialize(System.IO.BinaryReader reader)
		{
			filename = reader.ReadBytes(64);

			for (int i = 0; i < items.Capacity; i++)
				items[i] = new ObjdItem(this, i, reader);

			string s = Unserialize4String(reader);
			if (!s.Equals(filename))
				throw new Exception("Main filename '" + filename + "' does not match"
					+ " tail-end filename '" + s + "'.");
		}


		private void Serialize4String(System.IO.BinaryWriter w, string s)
		{
			w.Write((uint)s.Length);
			w.Write(SimPe.Helper.ToBytes(s));
		}

		private string Unserialize4String(System.IO.BinaryReader r)
		{
			return SimPe.Helper.ToString(r.ReadBytes(r.ReadInt32()));
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
				uint[] types = { SimPe.Data.MetaData.OBJD_FILE }; // handles the OBJD File
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
#if NOT_FIXED_ARRAYLIST
		public int Add(ObjdItem item)
		{
			if (items.Count >= items.Capacity) // only allow 32K lines
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

		public void Remove(ObjdItem item) { this.RemoveAt(items.IndexOf(item)); }

		public void RemoveAt(int index)
		{
			if (index < 0 || index >= items.Count) return;

			items.RemoveAt(index);
			OnWrapperChanged(items, new EventArgs());
		}

#endif

		public ObjdItem this[int index]
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

		public bool Contains(ObjdItem item) { return items.Contains(item); }

		public int IndexOf(object item) { return items.IndexOf(item); }

		public override void CopyTo(Array a, int i) { items.CopyTo(a, i); }

		public override int Count { get { return items.Count; } }

		public override bool IsSynchronized { get { return items.IsSynchronized; } }

		public override object SyncRoot { get { return items.SyncRoot; } }

		#region IEnumerable Members
		public override IEnumerator GetEnumerator() { return items.GetEnumerator(); }

		#endregion
		#endregion

		#region ObjdItemArrayList
		private class ObjdItemArrayList : ArrayList
		{
			public ObjdItemArrayList() : base() { }

			public ObjdItemArrayList(ObjdItem[] c) : base(c) { }

			public ObjdItemArrayList(int capacity) : base(capacity) { }

			public new ObjdItem this[int index]
			{
				get { return (ObjdItem)base[index]; }
				set { base[index] = value; }
			}

		}

		#endregion
	}


	/// <summary>
	/// An Item stored in an OBJD File
	/// </summary>
	public class ObjdItem
	{
		#region Attributes
		private short item = 0;
		private Objd parent = null;
		private int index = -1;

		#region shortFields
		private static int[] shortFields = { // identifiees two-byte fields containing unsigned shorts
											   //0x00: Initial Stack Size
											   //0x0C: Interaction Group (null)
											   //0x0E: Type of object (a buyable object has value 4)
											   //0x10: Multi-Tile master id (null)
											   //0x12: Multi-Tile sub index (null)
											   //0x16: Look At Score
											   //0x20: Price The price of the object
											   //0x3A: Slot group (null)
											   //0x40: Sale Price Different for each type of removal? (Some have a value, trees have null)
											   //0x42: Initial depreciation Sale price loss after first day of purchase
											   //0x44: Daily depreciation Loss of sale price after each successive day
											   //0x48: Depreciation limit The lowest price the object can be sold for
											   //0x52: ToolTip Name Type - 0 default (null)
											   //0x54: Template Version (null)
											   //0x56: Niceness Multiplier (variable)
											   //0x5A: Want Category (variable)
											   //0x5E: Object version (null)
											   //0x60: Default Thumbnail ID (null)
											   //0x62: Motive effects ID (null)
											   //0x68: Catalog popup ID (null)
											   //0x6C: Level offset (null)
											   //0x70: Num attributes (variable)
											   //0x72: Number of object arrays (variable)
											   //0x76: Front direction (null)
											   //0x82: Tile width (null)
											   //0x96: Footprint mask (null)
											   //0xAC: Room rating (null)
											   //0xB0: Num type attributes (Some have 3, trees have null)
											   //0xC0: Reset Lot Action (null)
											   //0xC2: 3D Object Type
											   0x00	// initial stack size
											   ,0x0C	// TTAB group??????????????? move?
											   ,0x0E	// object type ????
											   ,0x10	// (multi-tile) Master ID (instance number?? move??)
											   ,0x12	// (multi-tile) sub-index (?)
											   ,0x16	// Look at score
											   ,0x20	// Price
											   ,0x3A	// Slot group??????????????
											   ,0x40	// Sale price initial
											   ,0x42	// Sale price reduction after first day
											   ,0x44	// Sale price reduction after each subsequent day
											   ,0x48	// Sale price minimum
											   ,0x52	// Tooltip name type??????????
											   ,0x54	// Template version
											   ,0x56	// Niceness multiplier?? (float, surely?)
											   ,0x5A	// Want category??????
											   ,0x5E	// Object version
											   ,0x60	// Thumbname ID (default) ??????
											   ,0x62	// Motive effects ID ??????
											   ,0x68	// Catalogue popup ID??????
											   ,0x6C	// Level offset ??
											   ,0x70	// Number of attributes
											   ,0x72	// Number of object arrays
											   ,0x76	// Front direction (pick from a list?)
											   ,0x82	// Tile width
											   ,0x96	// Footprint mask (sounds like flags?)
											   ,0xAC	// Room rating
											   ,0xB0	// Number of Type attributes
											   ,0xC0	// Reset Lot action (is a BHAV instance?)
											   ,0xC2	// 3D Object Type (instance??)
										   };
		#endregion
		#region flagFields
		private static int[] flagFields = { // identifies two-byte flag fields // and (later) the flag name (Behaviour) String instance
									   //0x02: Default Wall Adjacent Flags
									   //0x04: Default Placement Flags
									   //0x06: Default Wall Placement Flags (null)
									   //0x08: Default Allowed Height Flags (null)
									   //0x1E: Catalog Use Flags (null)
									   //0x3C: Aspiration Flags (null)
									   //0x4A: Room sort flags The room category (bit0=Kitchen, bit1=Bedroom, bit2=Bathroom, bit3=Family Room, bit4=Outside, bit5=Dining Room, bit6=Misc, bit7=Study)
									   //0x4C: Function sort flags The object type (bit0=Seating, bit1=Surfaces, bit2=Appliances, bit3=Electronics, bit4=Plumbing, bit5=Decorative, bit6=General, bit7=Lighting)
									   //0x80: Chair entry flags So you enter the chair from the correct side (null)
									   //0x86: Build mode type The build mode type (1=door,2=window,3=stair,4=plant,5=fireplace,6=column,7=pool)
									   //0x90: Build Mode Subsort (null)
									   //0xAE: Skill Flags The skills this object gives (bit0=Cleaning, bit1=Cooking, bit2=Mechanical, bit3=Logic, bit4=Body, bit5=Creativity, bit6=Charisma, bit7=School Study?)
									   //0xB2: Misc flags (Some have 16, trees have null)
									   //0xB8: Function Sub-Sort Sub-type of the object eg. Electronic sub-sort are entertainment, video, audio, phone (bit1=first, bit2=second,...bit8=last)
									   //0xBA: Downtown Sort (null)
									   //0xBE: Vacation Sort (null)
									   //0xC4: Community Sort Community categories (bit0=, bit1=, bit2=, bit3=, bit4=, bit5=, bit6=, bit7=)
									   //0xC6: Dream Flags (null)
									   0x02  // ,?? // Wall Adjacent Flags (default)
									   ,0x04 // ,?? // Placement Flags (default)
									   ,0x06 // ,?? // Wall Placement Flags (default)
									   ,0x08 // ,?? // Allowed Height Flags (default)
									   ,0x1E // ,?? // Catalogue Flags ??
									   ,0x3C // ,?? // Aspiration Flags
									   ,0x4A // ,?? // Room Sort Flags
									   ,0x4C // ,?? // Function Sort Flags
									   ,0x80 // ,?? // Chair entry flags
									   ,0x86 // ,?? // Build mode sort
									   ,0x90 // ,?? // Build mode sub-sort
									   ,0xAE // ,?? // Skill flags
									   ,0xB2 // ,?? // Misc flags
									   ,0xB8 // ,?? // Function sub-sort flags
									   ,0xBA // ,?? // Downtown sort
									   ,0xBE // ,?? // Vacation sort
									   ,0xC4 // ,?? // Community sort
									   ,0xC6 // ,?? // Dream flags
								   };
		#endregion
		#region boolFields
		private static int[] boolFields = { // identifies two-byte fields used as boolean (0 false; 1 true)
									   //0x14: Use Default Placement flags (null)
									   //0x1C: Item Is Unlockable
									   //0x2E: Object ownership flags (null or 1)
									   //0x30: Ignore GlobalSim field in CAS Lot (null)
									   //0x32: Cannot Move Out With (null)
									   //0x34: Hauntable (null)
									   //0x3E: Memory Nice/Bad (null)
									   //0x46: Self depreciating Has a value if the object calculates its own depreciation, null if the game calculates it
									   //0x50: Is global sim object (null)
									   //0x58: No Duplicate On Placement (null)
									   //0x5C: No New Name from Template (null)
									   //0x6A: Ignore Current Model Index In Icons (null)
									   //0x6E: Shadow type Object has a shadow? (null if no shadow, FFFF if has a shadow)
									   //0x7A: Multi-tile lead object (null)
									   //0x84: Inhibit suit copying (null)
									   //0xBC: Keep Buying (null)
									   0x14	// use value in 0x04 when initialising object
									   ,0x1C	// can be unlocked??
									   ,0x2E	// can be owned??
									   ,0x30	// ??
									   ,0x32	// leave when moving out
									   ,0x34	// hauntable
									   ,0x3E	// memory is nice
									   ,0x46	// self-depreciating (hopefully not a BHAV reference!)
									   ,0x50	// global object
									   ,0x58	// No duplicate on placement
									   ,0x5C	// No new name from template
									   ,0x6A	// Ignore current model index in icons
									   ,0x6E	// Has shadow
									   ,0x7A	// (multi-tile) Lead object
									   ,0x84	// inhibit suit copying
									   ,0xBC	// Keep buying
								   };
		#endregion
		#region GUIDs
		private static int[] GUIDs = { // identifies four-byte field low-bytes
								  //0x18: GUID A unique ID number for each object. May also be a Sim ID (you can find this value in the Neighborhood FAMI and 0xAACE2EFB files)
								  //0x26: Diagonal selector GUID (GUID of Diagonal placement object - If object has a diagonal placement option - 0x00000000 (or 0x00000001 ?) if not)
								  //0x2A: Grid-aligned selector GUID (GUID of 'normal' placement object - Only used if this object is a diagonal placement option - 0x00000000 (or 0x00000001 ?) if not)
								  //0x36: Proxy GUID
								  //0x64: Job Object GUID
								  //0x88: Original GUID Unique ID of the source object, ie the object this one was based off
								  //0x8C: Object model GUID
								  0x18 // Object GUID
								  ,0x26 // Object GUID for diagonal placement (if available)
								  ,0x2A // Object GUID for grid-aligned placement (if available)
								  ,0x36 // Proxy GUID
								  ,0x64 // Job GUID
								  ,0x88 // Original GUID
								  ,0x8C // Model GUID
								  ,0xB4 // Unused (type attr GUID)
							  };
		#endregion
		#region motiveFields
		private static int[] motiveFields = { // identifiees two-byte fields used as motive ratings
										 //0xA0: Hunger rating (null)
										 //0xA2: Comfort rating (null)
										 //0xA4: Hygiene rating (null)
										 //0xA6: Bladder rating (null)
										 //0xA8: Energy rating (null)
										 //0xAA: Fun rating (null)
										 0xA0
										 ,0xA2
										 ,0xA4
										 ,0xA6
										 ,0xA8
										 ,0xAA
									 };
		#endregion
		#region resourceRefs
		private static int[] ttabResourceRefs = { // identifies two-byte instance numbers for this packed file type
													//0x0A: Interaction Table ID Pointer to the TTAB interaction information (a value of 1 for objects with no interactions)
													0x0A // "1" for no interactions else TTAB instance number
												};
		private static int[] strResourceRefs = { // identifies two-byte instance numbers for this packed file type
												   //0x22: Body strings ID Pointer to STR# resource for sims (null for objects)
												   0x22 // null for non-Sim objects else STR# file containing Body Strings
											   };
		private static int[] slotResourceRefs = { // identifies two-byte instance numbers for this packed file type
													//0x24: Slot ID Pointer to a SLOT resource
													0x24 // Slot resource
												};
		private static int[] ctssResourceRefs = { // identifies two-byte instance numbers for this packed file type
													//0x4E: Catalog strings ID Pointer to the objects CTSS catalog details
													0x4E // Catalogue strings
												};
		private static int[][] resourceRefs = {
												   ttabResourceRefs
												   ,strResourceRefs
												   ,slotResourceRefs
												   ,ctssResourceRefs
											   };

		#endregion
		#endregion

		#region Accessor methods
		public short Item
		{
			get { return item; }
			set
			{
				if (value != item)
				{
					item = value;
					parent.OnWrapperChanged(this, new EventArgs());
				}
			}
		}

		public Objd Parent
		{
			get { return parent; }
			set { parent = value; } // parent not part of wrapper
		}


		public bool IsShort
		{
			get
			{
				foreach(int i in ObjdItem.shortFields) if (i.Equals(this.index)) return true;
				return false;
			}
		}

		public bool IsFlag
		{
			get
			{
				foreach(int i in ObjdItem.flagFields) if (i.Equals(this.index)) return true;
				return false;
			}
		}

		public bool IsBool
		{
			get
			{
				foreach(int i in ObjdItem.boolFields) if (i.Equals(this.index)) return true;
				return false;
			}
		}

		public bool IsGUID
		{
			get
			{
				foreach(int i in ObjdItem.GUIDs) if (i.Equals(this.index)) return true;
				return false;
			}
		}

		public bool IsMotive
		{
			get
			{
				foreach(int i in ObjdItem.motiveFields) if (i.Equals(this.index)) return true;
				return false;
			}
		}

		public bool IsResource
		{
			get
			{
				foreach(int[] j in ObjdItem.resourceRefs) foreach(int i in j) if (i.Equals(this.index)) return true;
				return false;
			}
		}

#if UNUSED
		private int[] unusedFields = { // identifies two-byte fields that are not used
										 //0x74: Unused
										 //0x78: Unused
										 //0x7C: Unused - dynamic sprites base id (Sims1)
										 //0x7E: Unused - num dynamic sprites (Sims1)
										 //0x92: Unused - thumbnail graphic (Sims1)
										 //0x94: Unused - shadow flags (Sims1)
										 //0x98: Unused
										 //0x9A: Unused - shadow brightness (Sims1)
										 //0x9C: Unused
										 //0x9E: Unused - wall style sprite id (Sims1)
										 //0xB4: Unused - type attr guid (Sims1)
										 //0xC8: unused, reserved (12 bytes)
										 0x74 ,0x78 ,0x7C ,0x7E
										 ,0x92 ,0x94 ,0x98 ,0x9A
										 ,0x9C ,0x9E, 0xB4, 0xB6
										 ,0xC8 ,0xCA ,0xCC ,0xCE
										 ,0xD0 ,0xD2
									 };
#endif
		#endregion

		public ObjdItem(Objd parent, int index)
		{
			this.parent = parent;
			this.index = index;
		}

		public ObjdItem(Objd parent, int index, System.IO.BinaryReader reader)
		{
			this.parent = parent;
			this.index = index;
			this.Unserialize(reader);
		}


		public void Serialize(System.IO.BinaryWriter writer)
		{
			writer.Write(item);
		}

		public void Unserialize(System.IO.BinaryReader reader)
		{
			item = reader.ReadInt16();
		}

	}

}
