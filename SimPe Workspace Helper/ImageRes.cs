using System;
using System.Drawing;
using System.IO;


namespace SimPe
{

    /// <summary>
    /// Various Images
    /// This is where the Larger Icons are.
    /// </summary>
    public class GetImage
    {
        /// <summary>
        /// Sim Image (missing)
        /// </summary>
        public static Image NoOne
        {
            get
            {
                return global::SimPe.Properties.Resources.noone;
            }
        }
        /// <summary>
        /// Sim Image (missing)
        /// </summary>
        public static Image SheOne
        {
            get
            {
                return global::SimPe.Properties.Resources.sheone;
            }
        }
        /// <summary>
        /// Sim Image (pretty)
        /// </summary>
        public static Image BabyDoll
        {
            get
            {
                if (Helper.WindowsRegistry.Layout.SelectedTheme == 4 || Helper.WindowsRegistry.Layout.SelectedTheme == 7)
                    return global::SimPe.Properties.Resources.beachbabe;
                else
                    return global::SimPe.Properties.Resources.valbabydoll;
            }
        }
        /// <summary>
        /// Sim Image (pretty)
        /// </summary>
        public static Image VallyDoll
        {
            get
            {
                return global::SimPe.Properties.Resources.valbabydoll;
            }
        }
        /// <summary>
        /// Sim Image (pretty)
        /// </summary>
        public static Image BeachBabe
        {
            get
            {
                return global::SimPe.Properties.Resources.beachbabe;
            }
        }
        /// <summary>
        /// Tool Box Image
        /// </summary>
        public static Image Network
        {
            get
            {
                return global::SimPe.Properties.Resources.network;
            }
        }
        /// <summary>
        /// Tool Box Image
        /// </summary>
        public static Image Demo
        {
            get
            {
                return global::SimPe.Properties.Resources.demo;
            }
        }
        /// <summary>
        /// Tool Box Image
        /// </summary>
        public static Image Cassie
        {
            get
            {
                if (booby.PrettyGirls.IsTitsInstalled())
                    return global::SimPe.Properties.Resources.aCAF;
                else
                    return global::SimPe.Properties.Resources.CAF;
            }
        }
        private static System.Collections.ArrayList simm;
        private static Random smm = new Random();
        public static Image GetrandomSim()
        {
            if (simm == null)
            {
                simm = new System.Collections.ArrayList();
                if (booby.PrettyGirls.IsTitsInstalled())
                {
                    simm.Add("19,0xABBAA100");
                    simm.Add("19,0xABBAA230");
                    simm.Add("19,0xACDC0032");
                    simm.Add("19,0xACDC0033");
                    simm.Add("19,0xACDC0034");
                    simm.Add("19,0xCCA02700");
                    simm.Add("19,0xCCA02900");
                    simm.Add("19,0xB00B69A2");
                    simm.Add("19,0xB00B69A3");
                    simm.Add("19,0xB00B69A6");
                    simm.Add("19,0xB00B69A7");
                    simm.Add("19,0xB00B69B1");
                    simm.Add("19,0xB00B69B2");
                    simm.Add("19,0xB00B69B3");
                    simm.Add("19,0xB00B69B4");
                    simm.Add("19,0xB00B69B5");
                    simm.Add("19,0xB00B69B7");
                    simm.Add("19,0xB00B69B8");
                    simm.Add("19,0xE9F50069");
                    simm.Add("19,0xFF17AAA1");
                    simm.Add("19,0xFF17AAA2");
                    simm.Add("19,0xFF17AAA4");
                    simm.Add("19,0xFF17BBB1");
                    simm.Add("19,0xFF17BBB2");
                    simm.Add("19,0xFF17BBB3");
                    simm.Add("19,0xFF17BBB4");
                    simm.Add("19,0xFF17BBB5");
                }
                else
                {
                    if (PathProvider.Global.GetExpansion(SimPe.Expansions.BaseGame).Exists)
                    {
                        simm.Add("0,0xCCC30170");
                        simm.Add("0,0xCCC30171");
                        simm.Add("0,0xCCC30172");
                    }
                    else if (PathProvider.Global.GetExpansion(SimPe.Expansions.IslandStories).Exists)
                    {
                        simm.Add("28,0xCCC30170");
                        simm.Add("28,0xCCC30171");
                        simm.Add("28,0xCCC30172");
                        simm.Add("28,0xBEE00380");
                        simm.Add("28,0xDAFEE200");
                        simm.Add("28,0xDAFEE201");
                        simm.Add("28,0xDAFEE202");
                        simm.Add("28,0xDAFEE203");
                    }
                    else if (PathProvider.Global.GetExpansion(SimPe.Expansions.PetStories).Exists)
                    {
                        simm.Add("29,0xCCC30170");
                        simm.Add("29,0xCCC30171");
                        simm.Add("29,0xCCC30172");
                    }
                    else if (PathProvider.Global.GetExpansion(SimPe.Expansions.LifeStories).Exists)
                    {
                        simm.Add("30,0xCCC30170");
                        simm.Add("30,0xCCC30171");
                        simm.Add("30,0xCCC30172");
                    }
                    if (PathProvider.Global.GetExpansion(SimPe.Expansions.University).Exists)
                    {
                        simm.Add("1,0xCCC30220");
                        simm.Add("1,0xCCC30221");
                        simm.Add("1,0xCCC30222");
                    }
                    if (PathProvider.Global.GetExpansion(SimPe.Expansions.Nightlife).Exists)
                    {
                        simm.Add("2,0xCCA02100");
                        simm.Add("2,0xCCA02200");
                        simm.Add("2,0xCCA02300");
                        simm.Add("2,0xCCA02400");
                        simm.Add("2,0xCCA02500");
                        simm.Add("2,0xCCA02600");
                        simm.Add("2,0xCCA02700");
                        simm.Add("2,0xCCA02800");
                        simm.Add("2,0xCCA02900");
                        simm.Add("2,0xCCA03000");
                        simm.Add("2,0xCCA03100");
                    }
                    if (PathProvider.Global.GetExpansion(SimPe.Expansions.Business).Exists)
                    {
                        simm.Add("3,0xABBAA200");
                        simm.Add("3,0xABBAA215");
                        simm.Add("3,0xABBAA220");
                        simm.Add("3,0xABBAA230");
                        simm.Add("3,0xABBAA235");
                    }
                    if (PathProvider.Global.GetExpansion(SimPe.Expansions.Seasons).Exists)
                    {
                        simm.Add("7,0xDAFEE200");
                        simm.Add("7,0xDAFEE201");
                        simm.Add("7,0xDAFEE202");
                        simm.Add("7,0xDAFEE203");
                    }
                    if (PathProvider.Global.GetExpansion(SimPe.Expansions.Voyage).Exists)
                    {
                        simm.Add("10,0xACDC0032");
                        simm.Add("10,0xACDC0033");
                        simm.Add("10,0xACDC0034");
                    }
                    if (PathProvider.Global.GetExpansion(SimPe.Expansions.FreeTime).Exists)
                    {
                        simm.Add("13,0x0BB4BBB1");
                        simm.Add("13,0x0BB4BBB2");
                        simm.Add("13,0x0BB4BBB3");
                        simm.Add("13,0x0BB4BBB4");
                        simm.Add("13,0x0BB4BBB5");
                    }
                    if (PathProvider.Global.GetExpansion(SimPe.Expansions.Apartments).Exists)
                    {
                        simm.Add("16,0xC11FBBB1");
                        simm.Add("16,0xC11FBBB2");
                        simm.Add("16,0xC11FBBB3");
                        simm.Add("16,0xC11FBBB4");
                    }
                    if (PathProvider.Global.GetExpansion(SimPe.Expansions.Mansions).Exists)
                    {
                        simm.Add("17,0xFF17BBB1");
                        simm.Add("17,0xFF17BBB2");
                        simm.Add("17,0xFF17BBB3");
                        simm.Add("17,0xFF17BBB4");
                    }
                }
                if (booby.PrettyGirls.IsAngelsInstalled())
                {
                    simm.Add("18,0xB00B6909");
                    simm.Add("18,0xE9F50069");
                    simm.Add("18,0xE9F5006A");
                }
            }
            int pc = smm.Next(0, simm.Count);
            string[] go = ((string)simm[pc]).Split(new char[] { ',' });
            ExpansionItem ei = PathProvider.Global.GetExpansion(Convert.ToInt32(go[0]));
            SimPe.Packages.File pkg = SimPe.Packages.File.LoadFromFile(ei.InstalledPath(Convert.ToInt32(go[0])) + "\\TSData\\Res\\UI\\ui.package");
            if (pkg != null)
            {
                SimPe.Interfaces.Files.IPackedFileDescriptor pfd = pkg.FindFile(0x856DDBAC, 0, 0x499DB772, Helper.HexStringToUInt(go[1]));
                if (pfd != null)
                {
                    SimPe.PackedFiles.Wrapper.Picture pic = new SimPe.PackedFiles.Wrapper.Picture();
                    pic.ProcessData(pfd, pkg);
                    return pic.Image;
                }
            }
            return null;
        }

        /// <summary>
        ///  Used in Last EP used and EP Selecter
        /// </summary>
        public static Image GetExpansionLogo(int ep)
        {
            // This is used by the EP selecter which may display the logo for an EP that SimPe sees as not installed.
            if ((ep > 19 && ep < 28) || ep > 30) ep = 0; // Store doesn't have a logo, there is no EPs between Store (20) and Castaway Story (28) - Return base game Logo
            ExpansionItem ei = PathProvider.Global.GetExpansion(ep);
            if (ei.InstalledPath(ep) == null) return null;
            SimPe.Packages.File pkg = SimPe.Packages.File.LoadFromFile(ei.InstalledPath(ep) + "\\TSData\\Res\\UI\\ui.package");            
            if (pkg != null)
            {
                SimPe.Interfaces.Files.IPackedFileDescriptor pfd = pkg.FindFile(0x856DDBAC, 0, 0x499DB772, 0x8CBB9323);
                if (pfd != null)
                {
                    SimPe.PackedFiles.Wrapper.Picture pic = new SimPe.PackedFiles.Wrapper.Picture();
                    pic.ProcessData(pfd, pkg);
                    return pic.Image;
                }
            }
            return null;
        }

        /// <summary>
        ///  Used in LotCompressor
        /// </summary>
        public static Image GetExpansionIcon(byte ep)
        {
            uint inst = 0xABBB0000 + ep; // 0xABBB0000  0xABBAFFFF
            SimPe.Packages.File pkg = SimPe.Packages.File.LoadFromFile(System.IO.Path.Combine(PathProvider.Global.Latest.InstallFolder, "TSData\\Res\\UI\\ui.package"));
            if (pkg != null)
            {
                SimPe.Interfaces.Files.IPackedFileDescriptor pfd = pkg.FindFile(0x856DDBAC, 0, 0x499DB772, inst);
                if (pfd != null)
                {
                    SimPe.PackedFiles.Wrapper.Picture pic = new SimPe.PackedFiles.Wrapper.Picture();
                    pic.ProcessData(pfd, pkg);
                    return pic.Image;
                }
            }
            if (System.IO.Directory.Exists(System.IO.Path.Combine(PathProvider.SimSavegameFolder, "Downloads")))
            {
                string[] files = System.IO.Directory.GetFiles(System.IO.Path.Combine(PathProvider.SimSavegameFolder, "Downloads"), "Bodyshape Icons.package", SearchOption.AllDirectories);
                if (files.Length > 0)
                {
                    pkg = SimPe.Packages.File.LoadFromFile(files[0]);
                    SimPe.Interfaces.Files.IPackedFileDescriptor pfd = pkg.FindFile(0x856DDBAC, 0, 0x499DB772, inst);
                    if (pfd != null)
                    {
                        SimPe.PackedFiles.Wrapper.Picture pic = new SimPe.PackedFiles.Wrapper.Picture();
                        pic.ProcessData(pfd, pkg);
                        return pic.Image;
                    }
                }
            }
            return null;
        }


        public static void Loadimges(booby.TS2Logo tsLogo, int ep)
        {
            // This is used by the EP selecter which may display the logo for an EP that SimPe sees as not installed.
            ExpansionItem ei = PathProvider.Global.GetExpansion(ep);
            if (ei.InstalledPath(ep) == null) { tsLogo.BackImage = null; return; }
            string useown = ei.InstalledPath(ep) + "\\TSData\\Res\\UI\\ui.package";
            string usebase = PathProvider.Global[Expansions.BaseGame].InstallFolder + "\\TSData\\Res\\UI\\ui.package";
            string bakimge = "";
            string forimge = usebase;
            uint foimg = 0xccc30200; // the plumbob image intance in ui.package     Both temp set
            uint maimg = 0xccc30080; // the logo image intance in ui.package        to Basegame
            SimPe.Interfaces.Files.IPackedFileDescriptor pfd;
            SimPe.Packages.File pkg;
            tsLogo.Speed = 32; // lower value is faster
            tsLogo.ImageLocationX = 70;
            tsLogo.ImageWidth = 85; // width of each foreground image
            tsLogo.OverHead = 8; // space above logo for raised plumbob
            tsLogo.ImageLocationY = 0;
            if (SimPe.Helper.GameName == "The Sims 2 Legacy") ep = 0; // Legacy is crap
            switch (ep)
            {
                case 1: // University
                    bakimge = useown;
                    break;
                case 2: // Nightlife
                    bakimge = useown;
                    break;
                case 3: // Business
                    bakimge = useown;
                    maimg = 0x0000900D;
                    break;
                case 4: // Glamour
                    if (Helper.WindowsRegistry.CachedUserId == 105)
                    {
                        bakimge = useown;
                        maimg = 0x0000900E;
                        tsLogo.ImageLocationX = 48;
                    }
                    else
                    {
                        bakimge = usebase;
                        tsLogo.OverHead = 0;
                        tsLogo.ImageLocationY = 4;
                    }
                    break;
                case 5: // Family Fun
                    if (Helper.WindowsRegistry.CachedUserId == 105)
                    {
                        bakimge = useown;
                        maimg = 0x0000900E;
                        tsLogo.ImageLocationX = 48;
                    }
                    else
                    {
                        bakimge = usebase;
                        tsLogo.OverHead = 0;
                        tsLogo.ImageLocationY = 4;
                    }
                    break;
                case 6: // Pets
                    bakimge = useown;
                    maimg = 0x0000900E;
                    tsLogo.ImageLocationX = 78;
                    break;
                case 7: // Seasons
                    bakimge = useown;
                    maimg = 0x00009010;
                    break;
                case 8: // Celebrations
                    bakimge = useown;
                    maimg = 0x00009011;
                    tsLogo.OverHead = 12;
                    tsLogo.ImageLocationX = 52;
                    break;
                case 9: // H&M Fashion
                    bakimge = useown;
                    maimg = 0x00009012;
                    tsLogo.OverHead = 12;
                    tsLogo.ImageLocationX = 52;
                    break;
                case 10: // BonVoyage
                    bakimge = useown;
                    maimg = 0x00009013;
                    break;
                case 11: // Teen Style
                    bakimge = useown;
                    maimg = 0x00009014;
                    tsLogo.OverHead = 12;
                    tsLogo.ImageLocationX = 52;
                    break;
                case 12: // Extra Stuff
                    bakimge = useown;
                    maimg = 0x00009015;
                    tsLogo.OverHead = 0;
                    tsLogo.ImageLocationX = 46;
                    break;
                case 13: // Freetime
                    bakimge = useown;
                    maimg = 0x00009016;
                    break;
                case 14: // Kitchens & Bathrooms
                    bakimge = useown;
                    maimg = 0x00009017;
                    tsLogo.OverHead = 12;
                    tsLogo.ImageLocationX = 52;
                    break;
                case 15: // Ikea
                    bakimge = useown;
                    maimg = 0x00009018;
                    tsLogo.OverHead = 12;
                    tsLogo.ImageLocationX = 52;
                    break;
                case 16: // Apartment Life
                    bakimge = useown;
                    maimg = 0x00009019;
                    break;
                case 17: // Mansion & Garden
                    bakimge = useown;
                    maimg = 0x00009020;
                    break;
                case 18: // Angel & Nurses
                    bakimge = useown;
                    forimge = useown;
                    foimg = 0xccc30269;
                    maimg = 0x00009020;
                    break;
                case 19: // Tits & Arse
                    bakimge = useown;
                    forimge = useown;
                    foimg = 0xccc30269;
                    maimg = 0x00009020;
                    tsLogo.ImageLocationY = 0;
                    tsLogo.OverHead = 15;
                    tsLogo.Speed = 48;
                    break;
                case 28: // Castaway
                    bakimge = useown;
                    forimge = useown;
                    maimg = 0xBEEF0022;
                    tsLogo.OverHead = 0;
                    tsLogo.ImageWidth = 42; // Sim Stoies have tiny plumbob
                    tsLogo.ImageLocationY = 20; // move plumbob down
                    tsLogo.ImageLocationX = 78;
                    break;
                case 29: // Pet Stories
                    bakimge = useown;
                    forimge = useown;
                    maimg = 0xBEEF0022;
                    tsLogo.OverHead = 0;
                    tsLogo.ImageWidth = 42;
                    tsLogo.ImageLocationY = 20;
                    break;
                case 30: // Life Stories
                    bakimge = useown;
                    forimge = useown;
                    maimg = 0xBEEF0022;
                    tsLogo.OverHead = 0;
                    tsLogo.ImageWidth = 42;
                    tsLogo.ImageLocationY = 20;
                    break;
                default: // Basegame
                    bakimge = usebase;
                    tsLogo.OverHead = 0;
                    tsLogo.ImageLocationY = 4;
                    break;
            }
            try
            {
                pkg = SimPe.Packages.File.LoadFromFile(bakimge);
                pfd = pkg.FindFile(0x856DDBAC, 0, 0x499DB772, maimg);
                if (pfd != null)
                {
                    SimPe.PackedFiles.Wrapper.Picture pic = new SimPe.PackedFiles.Wrapper.Picture();
                    pic.ProcessData(pfd, pkg);
                    tsLogo.BackImage = pic.Image;
                }
                else tsLogo.BackImage = null;
                if (forimge != bakimge)
                    pkg = SimPe.Packages.File.LoadFromFile(forimge);

                pfd = pkg.FindFile(0x856DDBAC, 0, 0x499DB772, foimg);
                if (pfd != null)
                {
                    SimPe.PackedFiles.Wrapper.Picture pic = new SimPe.PackedFiles.Wrapper.Picture();
                    pic.ProcessData(pfd, pkg);
                    tsLogo.ForeImage = pic.Image;
                }
                else tsLogo.ForeImage = null;
            }
            catch { tsLogo.BackImage = tsLogo.ForeImage = null; }
        }
    }
}