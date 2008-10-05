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
    class cHoodTool : ITool, ICommandLine
    {
        static string rufio = "-rufio";

        delegate void Splash(string message);
        Splash splash;

        void Rufio(List<string> largs)
        {
            string output = Path.Combine(Path.Combine(SimPe.PathProvider.SimSavegameFolder, "Rufio"), "ExportedSims.txt");
            if (largs != null && largs.Count != 0 && largs[0].Length != 0 && !Directory.Exists(largs[0]))
            {
                output = largs[0];
                largs.RemoveAt(0);
            }
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
                    ";SimId;SimName;FamilyInstance;SimFamilyName;HouseholdName" +
                    ";HouseNumber;HouseName" +
                    ";AvailableCharacterData;Unlinked" +
                    ";Ghost(Objects,Walls,People,Freely)" +
                    ";BodyType" +
                    ";AutonomyLevel;NPCType;MotivesStatic;VoiceType;SchoolType;Grade;CareerPerformance;Career;CareerLevel;ZodiacSign;Aspiration;Gender" +
                    ";LifeSection;AgeDaysLeft;PrevAgeDays;AgeDuration;BlizLifelinePoints;LifelinePoints;LifelineScore" +
                    ";University(Effort,Grade,Time,Semester,Influence,Major)" +
                    ";Species" +
                    ";Salary" +
                    //";Reputation" +
                    ""
                    );

                ExpansionItem.NeighborhoodPaths paths = PathProvider.Global.GetNeighborhoodsForGroup();
                foreach (ExpansionItem.NeighborhoodPath path in paths)
                {
                    string sourcepath = path.Path;
                    string[] dirs = System.IO.Directory.GetDirectories(sourcepath, "????");
                    foreach (string dir in dirs)
                        AddHood(outPath, dir, w);
                }
            }
            finally
            {
                w.Close();
            }
        }


        void SetProvider(SimPe.Interfaces.Files.IPackageFile pkg)
        {
            FileTable.ProviderRegistry.SimFamilynameProvider.BasePackage = pkg;
            FileTable.ProviderRegistry.SimDescriptionProvider.BasePackage = pkg;
            FileTable.ProviderRegistry.SimNameProvider.BaseFolder = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(pkg.FileName), "Characters");
            FileTable.ProviderRegistry.LotProvider.BaseFolder = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(pkg.FileName), "Lots");
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

            string ghost = "N(,,,)";
            if (sdsc.CharacterDescription.GhostFlag.IsGhost)
            {
                ghost = "Y(" + (sdsc.CharacterDescription.GhostFlag.CanPassThroughObjects ? "Y" : "N") +
                    (sdsc.CharacterDescription.GhostFlag.CanPassThroughWalls ? "Y" : "N") +
                    (sdsc.CharacterDescription.GhostFlag.CanPassThroughPeople ? "Y" : "N") +
                    (sdsc.CharacterDescription.GhostFlag.IgnoreTraversalCosts ? "Y" : "N") +
                    ")";
            }

            string university = "N(,,,,,)";
            if (sdsc.University != null && sdsc.University.OnCampus == 0x1)
            {
                university = "Y(" + sdsc.University.Effort +
                "," + sdsc.University.Grade +
                "," + sdsc.University.Time +
                "," + sdsc.University.Semester +
                "," + sdsc.University.Influence +
                "," + sdsc.University.Major +
                ")";
            }

            if (dt.Equals(new DateTime(0)) || wasUnk || dt.AddMilliseconds(200).CompareTo(DateTime.UtcNow) < 0)
            {
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
                ";" + ghost +
                ";" + (bodyType)(ushort)sdsc.CharacterDescription.BodyFlag +
                ";" + sdsc.CharacterDescription.AutonomyLevel +
                ";" + sdsc.CharacterDescription.NPCType +
                ";" + sdsc.CharacterDescription.MotivesStatic +
                ";" + sdsc.CharacterDescription.VoiceType +
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
                ";" + sdsc.CharacterDescription.PrevAgeDays +
                ";" + sdsc.CharacterDescription.AgeDuration +
                ";" + sdsc.CharacterDescription.BlizLifelinePoints +
                ";" + sdsc.CharacterDescription.LifelinePoints +
                ";" + sdsc.CharacterDescription.LifelineScore +
                ";" + university +
                ";" + (sdsc.Nightlife == null ? "Human" : sdsc.Nightlife.Species.ToString()) +
                ";" + (sdsc.Business == null ? "0" : sdsc.Business.Salary.ToString()) +
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
            try
            {
                SimPe.WaitingScreen.Wait();
                splash = delegate(string message) { SimPe.WaitingScreen.UpdateMessage(message); };
                Rufio(null);
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
            if (!argv.Remove(rufio)) return false;

            splash = delegate(string message) { SimPe.Splash.Screen.SetMessage(message); };
            Rufio(argv);
            splash("");
            return true;
        }

        public string[] Help()
        {
            return new string[] { rufio, L.Get("pjCHoodHelp") };
        }

        #endregion
    }
}
