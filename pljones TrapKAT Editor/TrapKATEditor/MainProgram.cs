/***************************************************************************
 *   Copyright (C) 2007 by Peter L Jones                                   *
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
using System.Collections.Generic;
using System.Windows.Forms;

namespace TrapKATEditor
{
    static class MainProgram
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new UI.MainForm());
        }


        #region AllMemory
        private static Data.AllMemory allMemory = new Data.AllMemory();
        public static Data.AllMemory CurrentAllMemory { get { return allMemory; } }
        public static event EventHandler AllMemoryChanged;
        private static void OnAllMemoryChanged(object sender, EventArgs e)
        {
            if (AllMemoryChanged != null) AllMemoryChanged(sender, e);
        }
        #endregion

        public static event EventHandler GlobalChanged;
        private static void OnGlobalChanged(object sender, EventArgs e) { if (GlobalChanged != null) GlobalChanged(sender, e); }

        #region CurrentKit
        private static int currentKit = 0;
        public static event EventHandler KitChanged;
        private static void OnKitChanged(object sender, EventArgs e) { if (KitChanged != null) KitChanged(sender, e); }
        public static int CurrentKit
        {
            get { return currentKit; }
            set
            {
                if (currentKit == value) return;
                currentKit = value;
                OnKitChanged(currentKit, new EventArgs());
            }
        }
        #endregion

        public static void Reinit()
        {
            allMemory = new Data.AllMemory();
            OnAllMemoryChanged(allMemory, new EventArgs());
        }

        private static String currentFilename = "";
        private static Dump.DumpType currentType = Dump.DumpType.NotSet;

        public static String CurrentFilename { get { return currentFilename; } }
        public static Dump.DumpType CurrentType { get { return currentType; } }

        public static void OpenFile(String filename)
        {
            Dump.SysexDump dump = (new Dump.SysexDump()).Open(filename);
            currentFilename = filename;

            if (dump is Dump.AllMemoryDump)
            {
                currentType = Dump.DumpType.AllMemory;
                allMemory = ((Dump.AllMemoryDump)dump).AllMemory;
                OnAllMemoryChanged(allMemory, new EventArgs());
            }
            else if (dump is Dump.GlobalDump)
            {
                currentType = Dump.DumpType.Global;
                allMemory.Global = ((Dump.GlobalDump)dump).Global;
                OnGlobalChanged(allMemory.Global, new EventArgs());
            }
            else if (dump is Dump.KitDump)
            {
                if (currentKit < 0 || currentKit >= allMemory.Length)
                    throw new ArgumentOutOfRangeException("Can't load a kit with no kit selected");
                currentType = Dump.DumpType.Kit;
                allMemory[currentKit] = ((Dump.KitDump)dump).Kit;
                OnKitChanged(allMemory[currentKit], new EventArgs());
            }
            else
                throw new ArgumentException("Invalid dump loaded");
        }

        public static void SaveFile() { SaveFile(currentFilename, currentType); }
        public static void SaveFile(Dump.DumpType type) { SaveFile(currentFilename, type); }
        public static void SaveFile(String filename) { SaveFile(filename, currentType); }
        public static void SaveFile(String filename, Dump.DumpType type)
        {
            switch (type)
            {
                case Dump.DumpType.AllMemory: ((Dump.SysexDump)allMemory).Save(filename); break;
                case Dump.DumpType.Global: ((Dump.SysexDump)allMemory.Global).Save(filename); break;
                case Dump.DumpType.Kit:
                    if (currentKit < 0 || currentKit >= allMemory.Length)
                        throw new ArgumentOutOfRangeException("Can't save a kit with no kit selected");
                    ((Dump.SysexDump)allMemory[currentKit]).Save(filename);
                    break;
            }
        }

    }
}