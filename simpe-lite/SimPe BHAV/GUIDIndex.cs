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
using System.Resources;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace pjse
{
    class GUIDIndex : ICollection
    {
        private Hashtable guidIndex = null;

        public static GUIDIndex TheGUIDIndex = new GUIDIndex();
        public static String DefaultGUIDFile = Path.Combine(SimPe.Helper.SimPePluginDataPath, "pjse.coder.plugin\\guidindex.txt");
        static GUIDIndex()
		{
            if (Settings.PJSE.LoadGUIDIndexAtStartup) TheGUIDIndex.Load();
		}
        
        public void Create() { Create(false); }
        public void Create(bool fromCurrent)
        {
            guidIndex = new Hashtable();
            pjse.FileTable.Entry[] items = fromCurrent
                ? pjse.FileTable.GFT[pjse.FileTable.GFT.CurrentPackage, SimPe.Data.MetaData.OBJD_FILE]
                : pjse.FileTable.GFT[SimPe.Data.MetaData.OBJD_FILE];

            foreach (pjse.FileTable.Entry item in items)
            {
                if (item.Wrapper != null)
                {
                    System.IO.BinaryReader reader = item.Wrapper.StoredData;
                    if (reader.BaseStream.Length >= 0x40) // filename length
                    {
                        String objdName = SimPe.Helper.ToString(reader.ReadBytes(0x40)).Trim();
                        if (reader.BaseStream.Length > 0x5c + 4) // sizeof(uint)
                        {
                            reader.BaseStream.Seek(0x5c, System.IO.SeekOrigin.Begin);
                            UInt32 objdGUID = reader.ReadUInt32();
                            guidIndex[objdGUID] = objdName;
                        }
                    }
                }
            }
            pjse.FileTable.GFT.OnFiletableRefresh(this, new EventArgs());
        }

        public void Load() { Load(DefaultGUIDFile); }
        public void Load(String fromFile)
        {
            if (File.Exists(fromFile))
            {
                guidIndex = new Hashtable();
                System.IO.StreamReader sr = new StreamReader(fromFile);
                for (string line = sr.ReadLine(); line != null; line = sr.ReadLine())
                {
                    if (line.StartsWith("#")) continue;
                    String[] s = line.Split(new char[] { '=' }, 2, StringSplitOptions.None);
                    if (s.Length != 2) continue;
                    try
                    {
                        UInt32 guid = Convert.ToUInt32(s[0], 16);
                        guidIndex[guid] = s[1].Trim();
                    }
                    catch(System.FormatException) { continue; }
                }
                sr.Close();
                pjse.FileTable.GFT.OnFiletableRefresh(this, new EventArgs());
            }
        }

        public void Save() { Save(DefaultGUIDFile); }
        public void Save(String toFile)
        {
            if (!System.IO.Directory.Exists(Path.Combine(SimPe.Helper.SimPePluginDataPath, "pjse.coder.plugin")))
                System.IO.Directory.CreateDirectory(Path.Combine(SimPe.Helper.SimPePluginDataPath, "pjse.coder.plugin"));
            System.IO.StreamWriter sw = new StreamWriter(toFile, false);
            sw.WriteLine("# PJSE GUID Index");
            foreach(UInt32 guid in guidIndex.Keys)
                sw.WriteLine("0x" + SimPe.Helper.HexString(guid) + "=" + guidIndex[guid]);
            sw.Close();
        }

        public String this[UInt32 guid]
        {
            get
            {
                String s;
                return (guidIndex == null || (s = (String)guidIndex[guid]) == null || s.Length == 0) ? null : s;
            }
        }

        #region ICollection Members
        public void CopyTo(Array a, int i) { guidIndex.CopyTo(a, i); }
        public int Count { get { return guidIndex.Count; } }
        public bool IsSynchronized { get { return guidIndex.IsSynchronized; } }
        public object SyncRoot { get { return guidIndex.SyncRoot; } }
        #region IEnumerable Members
        public IEnumerator GetEnumerator() { return guidIndex.GetEnumerator(); }
        #endregion
        #endregion
    }
}
