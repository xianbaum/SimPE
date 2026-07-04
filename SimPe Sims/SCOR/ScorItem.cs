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
using SimPe.Interfaces;
using SimPe.PackedFiles.Wrapper.Supporting;
using SimPe.Data;
using System.Collections;
using System.Collections.Generic;
using SimPe.PackedFiles.Wrapper.SCOR;

namespace SimPe.PackedFiles.Wrapper
{
	/// <summary>
	/// An Item as stored in a Scor Resource
	/// </summary>
    public class ScorItem
    {
        #region GuiWrappers
        static Dictionary<string, Type> guis;
        static Dictionary<string, IScorItemToken> readers;
        static IScorItemToken deftoken;
        internal static Dictionary<string, Type> GuiElements
        {
            get
            {
                if (guis == null) LoadGuielements();
                return guis;
            }
        }

        internal static Dictionary<string, IScorItemToken> Readers
        {
            get
            {
                if (guis == null) LoadGuielements();
                return readers;
            }
        }

        internal static IScorItemToken DefaultTokenParser
        {
            get {
                if (guis == null) LoadGuielements();
                return deftoken;
            }
        }

        static void LoadGuielements(){
            guis = new Dictionary<string, Type>();
            readers = new Dictionary<string, IScorItemToken>();
            deftoken = new SCOR.ScorItemTokenDefault();
            guis.Add("Learned Behaviors", typeof(SCOR.ScoreItemLearnedBehaviour));
            //guis.Add("Most Recent Learned Behavior", typeof(SCOR.ScoreItemLearnedBehaviour));
            guis.Add("Business Rewards", typeof(SCOR.ScoreItemBusinessRewards));
            readers.Add("Business Rewards", new SCOR.ScorItemTokenBusinessRewards());
            guis.Add("Best Friend Forever List", typeof(SCOR.ScorItemTokenBffl));
            readers.Add("Best Friend Forever List", new SCOR.ScorItemTokenBffl());
        }

        internal void LoadGuiElement(string name)            
        {
            gui = GetGuiElement(name, null);
        }

        protected  SCOR.AScorItem GetGuiElement(string name, byte[] data)
        {
            SCOR.AScorItem ret = null;
            if (GuiElements.ContainsKey(name))
            {
                ret = System.Activator.CreateInstance(GuiElements[name], new object[] { this }) as SCOR.AScorItem;                
            }
            if (ret==null) ret = new SCOR.ScoreItemDefault(this);
            if (data != null)
            {
                System.IO.BinaryReader br = new System.IO.BinaryReader(new System.IO.MemoryStream(data));
                ret.SetData(name, br);
                br.Close();
            }

            return ret;
        }

        internal SCOR.IScorItemToken GetTokenParser(string name)
        {
            if (Readers.ContainsKey(name)) return Readers[name];
            return DefaultTokenParser;
        }
        #endregion

        SCOR.AScorItem gui;
        public SCOR.AScorItem Gui
        {
            get {
                if (gui == null) SetGui("", new byte[0]);
                return gui; 
            }
        }

        Scor parent;
        public Scor Parent
        {
            get { return parent; }
        }		
		
		/// <summary>
		/// Constructor
		/// </summary>
		public ScorItem(string name, Scor parent) : this(parent)
		{
			SetGui(name, new byte[0]);
		}

		internal ScorItem(Scor parent) 
		{
			this.parent = parent;
            SetGui("", new byte[0]);
		}

        ~ScorItem()
        {
            //if (gui != null) gui.Dispose();
        }

        protected void SetGui(string name, byte[] data)
        {
            //if (gui != null) gui.Dispose();
            gui = GetGuiElement(name, data);
        }

        internal static string nayme;
        internal static uint tipe;
						
		/// <summary>
		/// Unserializes a BinaryStream into the Attributes of this Instance
		/// </summary>
		/// <param name="reader">The Stream that contains the FileData</param>
		internal void Unserialize(System.IO.BinaryReader reader)
        {
            tipe = reader.ReadUInt32();//type
            nayme = StreamHelper.ReadString(reader);
            SCOR.IScorItemToken tp = GetTokenParser(nayme);

            lock (tp)
            {
                byte[] data = tp.UnserializeToken(this, reader);

                if (tp.ActivatedGUI == null)
                    SetGui(nayme, data);
                else
                {
                    gui = tp.ActivatedGUI;
                    gui.SetData(nayme, null);
                }
            }
		}

        internal static byte[] UnserializeDefaultToken(System.IO.BinaryReader reader)
        {
            //1=Sales Info or Business Rewards, 3=Learned Behaviours, 5=Best Friends Forever List, 6=Pet Ownership, 
            //7=Most Recent Learned Behaviour or Witch Names, 8=Apartment Wall Adjacencies, 9=Apartment Object Adjacencies, 
            if (reader.BaseStream.Position > reader.BaseStream.Length - 1) return new byte[0];
            System.Collections.ArrayList bytes = new ArrayList();
            if (nayme == "Business Rewards")//23 bytes per this crap will come back here again and chuck a fruiy
            {
                long ct = reader.BaseStream.Position;
                int a = reader.ReadInt32();
                a *= 23; a += 4;
                reader.BaseStream.Position = ct;
                for (int i = 0; i < a; i++)
                {
                    bytes.Add(reader.ReadByte());
                }
            }
            else if (tipe == 1 || tipe == 3 || tipe == 7)// learned behaviours, Most Recent Learned Behaviour, sales info and WitchNames have 10 bytes per
            {
                long ct = reader.BaseStream.Position;
                int a = reader.ReadInt32();// new pos is ct + 4 (for a) + a x 10
                a *= 10; a += 4;
                reader.BaseStream.Position = ct;
                for (int i = 0; i < a; i++)
                {
                    bytes.Add(reader.ReadByte());
                }
            }
            else if (tipe == 5)// Best Friends Forever List 30bytes each
            {
                long ct = reader.BaseStream.Position;
                int a = reader.ReadInt32();
                a *= 30; a += 4;
                reader.BaseStream.Position = ct;
                for (int i = 0; i < a; i++)
                {
                    bytes.Add(reader.ReadByte());
                }
            }
            else
            {
                byte test = reader.ReadByte();
                byte last = test;
                while (last != 0x00 || test != 0x04)
                {
                    bytes.Add(test);
                    if (reader.BaseStream.Position > reader.BaseStream.Length - 1) break;
                    last = test;
                    test = reader.ReadByte();
                }

                if (reader.BaseStream.Position <= reader.BaseStream.Length - 1)
                    if (bytes.Count > 0)
                        bytes.RemoveAt(bytes.Count - 1);
            }

            byte[] data = new byte[bytes.Count];
            bytes.CopyTo(data);

            return data;
        }

		/// <summary>
		/// Serializes a the Attributes stored in this Instance to the BinaryStream
		/// </summary>
		/// <param name="writer">The Stream the Data should be stored to</param>
		/// <remarks>
		/// Be sure that the Position of the stream is Proper on 
		/// return (i.e. must point to the first Byte after your actual File)
		/// </remarks>
		internal  void Serialize(System.IO.BinaryWriter writer, bool last)
		{
            writer.Write(tipe);
            Gui.Serialize(writer, last);
            SerializeDefaultToken(writer, last);			
		}

        internal static void SerializeDefaultToken(System.IO.BinaryWriter writer, bool last)
        {            
            if (!last) writer.Write((ushort)0x0400);
        }		

		public override string ToString()
		{
            return Gui.TokenName;
		}

	}

	public class ScorItems : 
        System.Collections.Generic.IEnumerable<ScorItem> , System.IDisposable
	{
		System.Collections.Generic.List<ScorItem> list;
		public ScorItems()
		{
            list = new List<ScorItem>();
		}

		public void Add(ScorItem si)
		{
			list.Add(si);
		}

		public void Remove(ScorItem si)
		{
			list.Remove(si);
		}

		public void Clear()
		{
			list.Clear();
		}

		public int Count
		{
			get {return list.Count;}
		}

		public bool Contains(ScorItem si)
		{
			return list.Contains(si);
		}

		protected int FindIndex(string name)
		{
			for (int i=0; i< list.Count; i++)
				if (this[i].Gui.Name==name) return i;

			return -1;
		}

		public ScorItem this[string name]
		{
			get { return list[FindIndex(name)] as ScorItem;}
			set 
			{
				list[FindIndex(name)] = value;
			}
		}

		public ScorItem this[int index]
		{
			get { return list[index] as ScorItem;}
			set 
			{
				list[index] = value;
			}
		}

		#region IEnumerable Member

		public IEnumerator GetEnumerator()
		{
			return list.GetEnumerator();
		}

		#endregion

		#region IDisposable Member

		public void Dispose()
		{
			list.Clear();
			list = null;
		}

		#endregion

        #region IEnumerable<ScorItem> Members

        IEnumerator<ScorItem> IEnumerable<ScorItem>.GetEnumerator()
        {
            return list.GetEnumerator();
        }

        internal IEnumerator<ScorItem> GetScorItemEnumerator()
        {
            return list.GetEnumerator();
        }

        #endregion
    }

}
