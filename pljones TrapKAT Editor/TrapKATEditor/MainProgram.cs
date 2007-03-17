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
        private static int currentKit = 0;
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

        public static void Reinit()
        {
            CurrentAllMemory = new Data.AllMemory();
        }

        private static String currentFilename = "";
        public static String CurrentFilename { get { return currentFilename; } }
        private static Dump.DumpType currentType = Dump.DumpType.NotSet;
        public static Dump.DumpType CurrentType { get { return currentType; } }
        public static void OpenFile(String filename)
        {
            Dump.SysexDump dump = (new Dump.SysexDump()).Open(filename);
            currentFilename = filename;

            if (dump is Dump.AllMemoryDump)
            {
                currentType = Dump.DumpType.AllMemory;
                CurrentAllMemory = ((Dump.AllMemoryDump)dump).AllMemory;
            }
            else if (dump is Dump.GlobalDump)
            {
                currentType = Dump.DumpType.Global;
                CurrentGlobal = ((Dump.GlobalDump)dump).Global;
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