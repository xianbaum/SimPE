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

        public static event EventHandler KitChanged;
        private static void OnKitChanged(object sender, EventArgs e)
        {
            if (KitChanged != null)
                KitChanged(sender, e);
        }
        private static int currentKit = -1;
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

        public static event EventHandler AllMemoryChanged;
        private static void OnAllMemoryChanged(object sender, EventArgs e)
        {
            if (AllMemoryChanged != null)
                AllMemoryChanged(sender, e);
        }
        private static Data.AllMemory allMemory = new Data.AllMemory();
        public static Data.AllMemory CurrentAllMemory
        {
            get { return allMemory; }
            set
            {
                if (allMemory == value) return;
                allMemory = value;
                currentKit = -1;
                OnAllMemoryChanged(allMemory, new EventArgs());
            }
        }

        public static event EventHandler GlobalChanged;
        private static void OnGlobalChanged(object sender, EventArgs e)
        {
            if (GlobalChanged != null)
                GlobalChanged(sender, e);
        }
        public static Data.Global CurrentGlobal
        {
            get { return allMemory == null ? null : allMemory.Global; }
            set
            {
                if (allMemory == null || allMemory.Global == value) return;
                allMemory.Global = value;
                OnGlobalChanged(allMemory.Global, new EventArgs());
            }
        }

        public static Data.Kit[] CurrentKits
        {
            get { return allMemory == null ? null : allMemory.Kits; }
        }

        public static void Reinit()
        {
            CurrentAllMemory = new Data.AllMemory();
        }

        private static String currentFilename = "";
        private static Dump.DumpType currentType = Dump.DumpType.AllMemory;
        public static void OpenFile(String filename)
        {
            Dump.SysexDump dump = (new Dump.SysexDump()).Open(filename);
            if (dump is Dump.AllMemoryDump)
                CurrentAllMemory = ((Dump.AllMemoryDump)dump).AllMemory;
            else if (dump is Dump.GlobalDump)
                allMemory.Global = ((Dump.GlobalDump)dump).Global;
            else if (dump is Dump.KitDump)
            {
                if (currentKit < 0 || currentKit >= allMemory.Kits.Length)
                    throw new ArgumentOutOfRangeException("Can't load a kit with no kit selected");
                allMemory.Kits[currentKit] = ((Dump.KitDump)dump).Kit;
            }
            else
                throw new ArgumentException("Invalid dump loaded");
            currentFilename = filename;
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
                    if (currentKit < 0 || currentKit >= allMemory.Kits.Length)
                        throw new ArgumentOutOfRangeException("Can't save a kit with no kit selected");
                    ((Dump.SysexDump)allMemory.Kits[currentKit]).Save(filename);
                    break;
            }
        }

    }
}