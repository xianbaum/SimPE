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
                w.WriteLine("hood;SimId;SimName;FamilyInstance;SimFamilyName;HouseholdName;AvailableCharacterData;Unlinked" +
                    ";Ghost(Objects,Walls,People,Freely)" +
                    ";BodyType"+
                    ";AutonomyLevel;NPCType;MotivesStatic;VoiceType;SchoolType;Grade;CareerPerformance;Career;CareerLevel;ZodiacSign;Aspiration;Gender" +
                    ";LifeSection;Age;PrevAgeDays;AgeDuration;BlizLifelinePoints;LifelinePoints;LifelineScore" +
                    ";University(Effort,Grade,Time,Semester,Influence,Major)" +
                    ";Species" +
                    ";Salary" +
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

        string previousSimFamilyName = "";
        void AddHood(string outPath, string dir, StreamWriter w)
        {
            string hood = Path.GetFileName(dir);
            string hoodFile = Path.Combine(dir, hood + "_Neighborhood.package");
            if (!File.Exists(hoodFile)) return;

            SimPe.Packages.File pkg = SimPe.Packages.File.LoadFromFile(hoodFile);
            if (pkg == null) return;

            splash("Loading Neighborhood " + hood);
            SetProvider(pkg);

            previousSimFamilyName = "";
            IPackedFileDescriptor[] pfds = pkg.FindFiles(SimPe.Data.MetaData.SIM_DESCRIPTION_FILE);
            foreach (IPackedFileDescriptor spfd in pfds)
            {
                ExtSDesc sdsc = new ExtSDesc();
                sdsc.ProcessData(spfd, pkg);

                AddSim(outPath, hood, w, sdsc);
            }
        }

        enum bodyType : ushort { Unknown = 0, Fat = 1, PregnantFull = 2, PregnantHalf = 4, PregnantHidden = 8, };
        void AddSim(string outPath, string hood, StreamWriter w, ExtSDesc sdsc)
        {
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

            if (sdsc.SimFamilyName != previousSimFamilyName)
            {
                splash("Saving " + sdsc.SimName + " " + sdsc.SimFamilyName);
                previousSimFamilyName = sdsc.SimFamilyName;
            }
            string csv = hood +
                ";" + sdsc.Instance +
                ";" + sdsc.SimName +
                ";" + sdsc.FamilyInstance +
                ";" + sdsc.SimFamilyName +
                ";" + sdsc.HouseholdName +
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
                ""
            ;
            w.WriteLine(csv);

            AddImage(sdsc, Path.Combine(outPath, hood + "_" + sdsc.Instance + ".png"));
        }

        void AddImage(ExtSDesc sdsc, string f)
        {
            if (sdsc.Image != null)
            {
                if ((sdsc.Unlinked != 0x00) || (!sdsc.AvailableCharacterData) || sdsc.IsNPC)
                {
                    Image img = (Image)sdsc.Image.Clone();
                    System.Drawing.Graphics g = Graphics.FromImage(img);
                    g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                    g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;

                    Pen pen = new Pen(SimPe.Data.MetaData.SpecialSimColor, 3);

                    g.FillRectangle(pen.Brush, 0, 0, img.Width, img.Height);

                    int pos = 2;
                    if (sdsc.Unlinked != 0x00)
                    {
                        g.FillRectangle(new SolidBrush(SimPe.Data.MetaData.UnlinkedSim), pos, 2, 25, 25);
                        pos += 28;
                    }
                    if (!sdsc.AvailableCharacterData)
                    {
                        g.FillRectangle(new SolidBrush(SimPe.Data.MetaData.InactiveSim), pos, 2, 25, 25);
                        pos += 28;
                    }
                    if (sdsc.IsNPC)
                    {
                        g.FillRectangle(new SolidBrush(SimPe.Data.MetaData.NPCSim), pos, 2, 25, 25);
                        pos += 28;
                    }
                    img.Save(f);
                }
                else
                {
                    sdsc.Image.Save(f);
                }
            }
            else
            {
                (new Bitmap((new SimPe.Plugin.PlasticSurgery(null, null, null, null, null)).GetType().Assembly.GetManifestResourceStream("SimPe.Plugin.Network.png"))).Save(f);
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
