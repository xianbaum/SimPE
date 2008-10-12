/***************************************************************************
 *   Copyright (C) 2008 by Peter L Jones                                   *
 *   peter@users.sf.net                                                    *
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
using System.Drawing;
using System.IO;
using System.Text;
using SimPe;
using SimPe.Interfaces;
using SimPe.Interfaces.Files;
using SimPe.PackedFiles.Wrapper;
using SimPe.Plugin;

namespace pjHoodTool
{
    /*
        University = 0x2,//1
        Nightlife = 0x4,//2
        Business = 0x8,//3
        (SP)FamilyFun = 0x10,//4
        (SP)Glamour = 0x20,//5
        Pets = 0x40,//6
        Seasons = 0x80,//7
        (SP)Celebrations = 0x100,//8
        (SP)Fashion = 0x200,//9
        Voyage = 0x400,//10
        (SP)Teen = 0x800,//11
        (SP)Store = 0x1000,//12
        FreeTime = 0x2000,//13
        (SP)Kitchens = 0x4000,//14
        (SP)IKEA = 0x8000,//15
        Apartments = 0x00010000,//16 --Flags2--
        (SP)Mansions = 0x00020000,//17
    */
    class cHoodTool : ITool, ICommandLine
    {
        delegate void Splash(string message);
        Splash splash;

        void Rufio(string output, string hood)
        {
            if (output.Length == 0)
                output = Path.Combine(Path.Combine(SimPe.PathProvider.SimSavegameFolder, "Rufio"), "ExportedSims.txt");
            else
                output = Path.Combine(Path.Combine(output, "Rufio"), "ExportedSims.txt");

            string outPath = Path.GetDirectoryName(output);
            if (!Directory.Exists(outPath))
                Directory.CreateDirectory(outPath);

            StreamWriter w = new StreamWriter(output);
            w.AutoFlush = true;

            splash(L.Get("pjCHoodTool"));
            try
            {
                w.WriteLine("hood" +
                    ";HoodName" +
                    ";NID;SimName;FamilyInstance;SimFamilyName;HouseholdName" +
                    ";HouseNumber;HouseName" +
                    ";AvailableCharacterData;Unlinked" +
                    ";ParentA;ParentB;Spouse" +
                    //";Ghost(Objects,Walls,People,Freely)" +
                    ";BodyType" +
                    //";AutonomyLevel"+
                    ";NPCType" +
                    //";MotivesStatic;VoiceType"+
                    ";SchoolType;Grade;CareerPerformance;Career;CareerLevel;ZodiacSign;Aspiration;Gender" +
                    ";LifeSection;AgeDaysLeft" +
                    //";PrevAgeDays;AgeDuration"+
                    ";BlizLifelinePoints;LifelinePoints;LifelineScore" +
                    ";GenActive;GenNeat;GenNice;GenOutgoing;GenPlayful" + // GeneticCharacter
                    ";Active;Neat;Nice;Outgoing;Playful" + // Character
                    ";Animals;Crime;Culture;Entertainment;Environment;Fashion;FemalePreference;Food;Health" + //Interests
                    ";MalePreference;Money;Paranormal;Politics;School;Scifi;Sports;Toys;Travel;Weather;Work" + //Interests
                    ";Body;Charisma;Cleaning;Cooking;Creativity;Fatness;Logic;Mechanical;Romance" + //Skills

                    ";IsAtUniversity;UniEffort;UniGrade;UniTime;UniSemester;UniInfluence;UniMajor" + // University
                    ";Species" + // Nightlife
                    ";Salary" + // Business
                    ";PrimaryAspiration;SecondaryAspiration;HobbyPredistined;LifetimeWant" + // FreeTime
                    //";Reputation" + // Aparments... not found it yet
                    ""
                    );

                ExpansionItem.NeighborhoodPaths paths = PathProvider.Global.GetNeighborhoodsForGroup();
                foreach (ExpansionItem.NeighborhoodPath path in paths)
                {
                    string sourcepath = path.Path;
                    string[] dirs = System.IO.Directory.GetDirectories(sourcepath, hood.Length > 0 ? hood : "????");
                    foreach (string dir in dirs)
                        AddHood(outPath, dir, w);
                }
            }
            finally
            {
                w.Close();
            }
        }

        ExtFamilyTies eft = null;
        void SetProvider(SimPe.Interfaces.Files.IPackageFile pkg)
        {
            FileTable.ProviderRegistry.SimFamilynameProvider.BasePackage = pkg;
            FileTable.ProviderRegistry.SimDescriptionProvider.BasePackage = pkg;
            FileTable.ProviderRegistry.SimNameProvider.BaseFolder = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(pkg.FileName), "Characters");
            FileTable.ProviderRegistry.LotProvider.BaseFolder = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(pkg.FileName), "Lots");
            eft = new ExtFamilyTies();
            IPackedFileDescriptor[] pfds = pkg.FindFiles(SimPe.Data.MetaData.FAMILY_TIES_FILE);
            if (pfds != null && pfds.Length > 0)
                eft.ProcessData(pfds[0], pkg);
        }

        DateTime dt = new DateTime(0);
        bool wasUnk = true;
        void AddHood(string outPath, string dir, StreamWriter w)
        {
            string hood = Path.GetFileName(dir);
            string hoodFile = Path.Combine(dir, hood + "_Neighborhood.package");
            if (!File.Exists(hoodFile)) return;

            SimPe.Packages.File pkg = SimPe.Packages.File.LoadFromFile(hoodFile);
            if (pkg == null) return;

            string hoodName = Localization.GetString("Unknown");
            IPackedFileDescriptor[] pfds = pkg.FindFiles(SimPe.Data.MetaData.CTSS_FILE);
            StrWrapper ctss = null;
            if (pfds.Length == 1)
            {
                ctss = new StrWrapper();
                ctss.ProcessData(pfds[0], pkg);
                hoodName = ctss[1, 0];
            }

            if (!Directory.Exists(Path.Combine(outPath, "SimImage")))
                Directory.CreateDirectory(Path.Combine(outPath, "SimImage"));

            if (!Directory.Exists(Path.Combine(outPath, "LotImage")))
                Directory.CreateDirectory(Path.Combine(outPath, "LotImage"));


            System.Windows.Forms.Application.DoEvents();
            splash("Loading Neighborhood " + hood + ": " + hoodName);
            SetProvider(pkg);

            dt = new DateTime(0);
            wasUnk = true;
            pfds = pkg.FindFiles(SimPe.Data.MetaData.SIM_DESCRIPTION_FILE);
            foreach (IPackedFileDescriptor spfd in pfds)
            {
                ExtSDesc sdsc = new ExtSDesc();
                sdsc.ProcessData(spfd, pkg);

                AddSim(outPath, hood, hoodName, w, sdsc);
            }
        }

        enum bodyType : ushort { Unknown = 0, Fat = 1, PregnantFull = 2, PregnantHalf = 4, PregnantHidden = 8, };
        void AddSim(string outPath, string hood, string hoodName, StreamWriter w, ExtSDesc sdsc)
        {
            IPackedFileDescriptor pfd = sdsc.Package.FindFile(0x46414D49, 0x00000000, 0xffffffff, sdsc.FamilyInstance); // FAMI
            Fami family = null;
            SimPe.Interfaces.Providers.ILotItem ilot = null;
            if (pfd != null)
            {
                family = new Fami(FileTable.ProviderRegistry.SimNameProvider);
                family.ProcessData(pfd, sdsc.Package);
                ilot = FileTable.ProviderRegistry.LotProvider.FindLot(family.LotInstance);
            }

            string ties = ";;";
            if (eft != null)
            {
                SDesc[] p = eft.ParentSims(sdsc);
                SDesc[] s = eft.SpouseSims(sdsc);
                ties = (p == null || p.Length < 2 ? ";" : p[0].Instance + ";" + p[1].Instance) +
                    ";" + (s == null || s.Length < 1 ? "" : s[0].Instance + "") +
                    ""
                    ;
            }

            /*string ghost = "N(,,,)";
            if (sdsc.CharacterDescription.GhostFlag.IsGhost)
            {
                ghost = "Y(" + (sdsc.CharacterDescription.GhostFlag.CanPassThroughObjects ? "Y" : "N") +
                    (sdsc.CharacterDescription.GhostFlag.CanPassThroughWalls ? "Y" : "N") +
                    (sdsc.CharacterDescription.GhostFlag.CanPassThroughPeople ? "Y" : "N") +
                    (sdsc.CharacterDescription.GhostFlag.IgnoreTraversalCosts ? "Y" : "N") +
                    ")";
            }*/

            string genetics = sdsc.GeneticCharacter.Active +
                ";" + sdsc.GeneticCharacter.Neat +
                ";" + sdsc.GeneticCharacter.Nice +
                ";" + sdsc.GeneticCharacter.Outgoing +
                ";" + sdsc.GeneticCharacter.Playful +
                ""
            ;

            string character = sdsc.Character.Active +
                ";" + sdsc.Character.Neat +
                ";" + sdsc.Character.Nice +
                ";" + sdsc.Character.Outgoing +
                ";" + sdsc.Character.Playful +
                ""
            ;

            string interests = sdsc.Interests.Animals +
                ";" + sdsc.Interests.Crime +
                ";" + sdsc.Interests.Culture +
                ";" + sdsc.Interests.Entertainment +
                ";" + sdsc.Interests.Environment +
                ";" + sdsc.Interests.Fashion +
                ";" + sdsc.Interests.FemalePreference +
                ";" + sdsc.Interests.Food +
                ";" + sdsc.Interests.Health +
                ";" + sdsc.Interests.MalePreference +
                ";" + sdsc.Interests.Money +
                ";" + sdsc.Interests.Paranormal +
                ";" + sdsc.Interests.Politics +
                ";" + sdsc.Interests.School +
                ";" + sdsc.Interests.Scifi +
                ";" + sdsc.Interests.Sports +
                ";" + sdsc.Interests.Toys +
                ";" + sdsc.Interests.Travel +
                ";" + sdsc.Interests.Weather +
                ";" + sdsc.Interests.Work +
                ""
            ;

            string skills = sdsc.Skills.Body +
                ";" + sdsc.Skills.Charisma +
                ";" + sdsc.Skills.Cleaning +
                ";" + sdsc.Skills.Cooking +
                ";" + sdsc.Skills.Creativity +
                ";" + sdsc.Skills.Fatness +
                ";" + sdsc.Skills.Logic +
                ";" + sdsc.Skills.Mechanical +
                ";" + sdsc.Skills.Romance +
                ""
            ;

            string university = "N;;;;;";
            if (sdsc.University != null && sdsc.University.OnCampus == 0x1)
            {
                university = "Y" +
                ";" + sdsc.University.Effort +
                ";" + sdsc.University.Grade +
                ";" + sdsc.University.Time +
                ";" + sdsc.University.Semester +
                ";" + sdsc.University.Influence +
                ";" + sdsc.University.Major
                ;
            }

            string freetime = ";;;";
            if (sdsc.Freetime != null)
            {
                freetime = sdsc.Freetime.PrimaryAspiration +
                    ";" + sdsc.Freetime.SecondaryAspiration +
                    ";" + sdsc.Freetime.HobbyPredistined +
                    ";" + sdsc.Freetime.LongtermAspiration // LifetimeWant ?
                ;
                //sdsc.Freetime.BugCollection -- no...
            }

            //sdsc.Business.LotID


            if (dt.Equals(new DateTime(0)) || wasUnk || dt.AddMilliseconds(200).CompareTo(DateTime.UtcNow) < 0)
            {
                System.Windows.Forms.Application.DoEvents();
                if (!((string)(sdsc.SimName + " " + sdsc.SimFamilyName)).Trim().ToLower().Equals("unknown"))
                {
                    dt = new DateTime(DateTime.UtcNow.Ticks);
                    wasUnk = false;
                    splash("Saving " + sdsc.SimName + " " + sdsc.SimFamilyName);
                }
                else
                    wasUnk = true;
            }
            string csv = hood +
                ";" + hoodName +
                ";" + sdsc.Instance +
                ";" + sdsc.SimName +
                ";" + sdsc.FamilyInstance +
                ";" + sdsc.SimFamilyName +
                ";" + sdsc.HouseholdName +
                ";" + (ilot != null ? ilot.Instance + ";" + ilot.LotName : ";") +
                ";" + (sdsc.AvailableCharacterData ? "Y" : "N") +
                ";" + (sdsc.Unlinked != 0x00 ? "Y" : "N").ToString() +
                ";" + ties +
                //";" + ghost +
                ";" + (bodyType)(ushort)sdsc.CharacterDescription.BodyFlag +
                //";" + sdsc.CharacterDescription.AutonomyLevel +
                ";" + sdsc.CharacterDescription.NPCType +
                //";" + sdsc.CharacterDescription.MotivesStatic +
                //";" + sdsc.CharacterDescription.VoiceType +
                ";" + sdsc.CharacterDescription.SchoolType +
                ";" + sdsc.CharacterDescription.Grade +
                ";" + sdsc.CharacterDescription.CareerPerformance +
                ";" + sdsc.CharacterDescription.Career +
                ";" + sdsc.CharacterDescription.CareerLevel +
                ";" + sdsc.CharacterDescription.ZodiacSign +
                ";" + sdsc.CharacterDescription.Aspiration +
                ";" + sdsc.CharacterDescription.Gender +
                ";" + sdsc.CharacterDescription.LifeSection +
                ";" + sdsc.CharacterDescription.Age +
                //";" + sdsc.CharacterDescription.PrevAgeDays +
                //";" + sdsc.CharacterDescription.AgeDuration +
                ";" + sdsc.CharacterDescription.BlizLifelinePoints +
                ";" + sdsc.CharacterDescription.LifelinePoints +
                ";" + sdsc.CharacterDescription.LifelineScore +
                ";" + genetics +
                ";" + character +
                ";" + interests +
                ";" + skills +
                ";" + university +
                ";" + (sdsc.Nightlife == null ? "Human" : sdsc.Nightlife.Species.ToString()) +
                ";" + (sdsc.Business == null ? "0" : sdsc.Business.Salary.ToString()) +
                ";" + freetime +
                //";Reputation" +
                ""
            ;
            w.WriteLine(csv);

            AddImage(sdsc.Image, Path.Combine(Path.Combine(outPath, "SimImage"), hood + "_" + sdsc.Instance + ".png"));
            if (ilot != null)
                AddImage(ilot.Image, Path.Combine(Path.Combine(outPath, "LotImage"), hood + "_" + ilot.Instance + ".png"));
        }

        void AddImage(Image img, string f)
        {
            if (img != null)
            {
                if (img.Size.Width > 16 && img.Size.Height > 16)
                    img.Save(f);
                else
                    System.Diagnostics.Trace.WriteLine("img too small: " + Path.GetFileNameWithoutExtension(f) + ";w=" + img.Width + ";h=" + img.Height);
            }
        }


        #region ITool Members

        SimPe.Interfaces.Plugin.IToolResult ITool.ShowDialog(ref SimPe.Interfaces.Files.IPackedFileDescriptor pfd, ref SimPe.Interfaces.Files.IPackageFile package)
        {
            if (!System.IO.Directory.Exists(PathProvider.Global.NeighborhoodFolder))
            {
                System.Windows.Forms.MessageBox.Show("The Folder " + PathProvider.Global.NeighborhoodFolder + " was not found.\n" +
                    "Please specify the correct SaveGame Folder in the Options Dialog.");
                return new ToolResult(false, false);
            }

            System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();
            fbd.Description = L.Get("ChooseFolder");
            fbd.SelectedPath = SimPe.PathProvider.SimSavegameFolder;
            fbd.ShowNewFolderButton = true;
            System.Windows.Forms.DialogResult dr = fbd.ShowDialog();
            if (dr != System.Windows.Forms.DialogResult.OK) return new ToolResult(false, false);


            NeighborhoodForm nfm = new NeighborhoodForm();
            nfm.LoadNgbh = false;
            nfm.ShowBackupManager = false;
            nfm.Text = L.Get("nfmTitle");
            SimPe.Interfaces.Plugin.IToolResult ret = nfm.Execute(ref package, null);

            string hood = "";
            if (nfm.DialogResult == System.Windows.Forms.DialogResult.OK && nfm.SelectedNgbh != null)
                hood = Path.GetFileName(Path.GetDirectoryName(nfm.SelectedNgbh));

            try
            {
                SimPe.WaitingScreen.Wait();
                splash = delegate(string message) { SimPe.WaitingScreen.UpdateMessage(message); };
                Rufio(fbd.SelectedPath, hood);
                return new SimPe.Plugin.ToolResult(false, false);
            }
            finally
            {
                SimPe.WaitingScreen.Stop();
            }
        }

        bool ITool.IsEnabled(SimPe.Interfaces.Files.IPackedFileDescriptor pfd, SimPe.Interfaces.Files.IPackageFile package)
        {
            return true;
        }

        #endregion

        #region IToolPlugin Members

        string IToolPlugin.ToString()
        {
            return "Neighborhood\\" + L.Get("pjCHoodTool");
        }

        #endregion

        #region ICommandLine Members

        public bool Parse(List<string> argv)
        {
            int i = ArgParser.Parse(argv, "-rufio");
            if (i < 0) return false;

            string outpath = "";
            string hood = "";
            while (argv.Count > i)
            {
                if (ArgParser.Parse(argv, i, "-out", ref outpath)) continue;
                if (ArgParser.Parse(argv, i, "-hood", ref hood)) continue;
                SimPe.Message.Show(Help()[0]);
                return true;
            }

            if (outpath.Length > 0 && !Directory.Exists(outpath))
            {
                SimPe.Message.Show("Use -out specify an existing folder");
                return true;
            }

            if (!Directory.Exists(PathProvider.Global.NeighborhoodFolder))
            {
                SimPe.Message.Show("The Folder " + PathProvider.Global.NeighborhoodFolder + " was not found.\n" +
                    "Please specify the correct SaveGame Folder in the Options Dialog.");
                return false;
            }

            splash = delegate(string message) { SimPe.Splash.Screen.SetMessage(message); };
            Rufio(outpath, hood);
            splash("");
            return true;
        }

        public string[] Help()
        {
            return new string[] { "-rufio -out {outpath} {-hood hood}", L.Get("pjCHoodHelp") };
        }

        #endregion
    }
}
