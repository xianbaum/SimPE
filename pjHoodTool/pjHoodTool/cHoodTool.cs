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

        void Rufio(List<string> largs)
        {
            string output = Path.Combine(Path.Combine(SimPe.PathProvider.SimSavegameFolder, "Rufio"), "ExportedSims.txt");
            if (largs != null && largs.Count != 0 && largs[0].Length != 0)
            {
                output = largs[0];
                largs.RemoveAt(0);
            }
            string outPath = Path.GetDirectoryName(output);
            if (!Directory.Exists(outPath))
                Directory.CreateDirectory(outPath);

            StreamWriter w = new StreamWriter(output);
            w.AutoFlush = true;

            SimPe.WaitingScreen.Wait();
            SimPe.WaitingScreen.UpdateMessage(L.Get("pjCHoodTool"));
            try
            {
                w.WriteLine("dir;Instance;SimId;SimName;FamilyInstance;SimFamilyName;LifeSection;AgeDuration");

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
                SimPe.WaitingScreen.Stop();
            }
        }

        void AddHood(string outPath, string dir, StreamWriter w)
        {
            string hood = Path.GetFileName(dir);
            string hoodFile = Path.Combine(dir, hood + "_Neighborhood.package");
            if (!File.Exists(hoodFile)) return;

            SimPe.Packages.File pkg = SimPe.Packages.File.LoadFromFile(hoodFile);
            if (pkg == null) return;

            IPackedFileDescriptor[] pfds = pkg.FindFiles(SimPe.Data.MetaData.SIM_DESCRIPTION_FILE);
            foreach (IPackedFileDescriptor spfd in pfds)
            {
                WaitingScreen.Wait();
                ExtSDesc sdsc = new ExtSDesc();
                sdsc.ProcessData(spfd, pkg);

                AddSim(outPath, hood, w, sdsc);
            }
        }

        void AddSim(string outPath, string hood, StreamWriter w, ExtSDesc sdsc)
        {
            string csv = hood +
                ";" + sdsc.Instance +
                ";" + sdsc.SimId +
                ";" + sdsc.SimName +
                ";" + sdsc.FamilyInstance +
                ";" + sdsc.SimFamilyName +
                ";" + sdsc.CharacterDescription.LifeSection +
                ";" + sdsc.CharacterDescription.AgeDuration
            ;
            w.WriteLine(csv);

            //AddImage(sdsc, Path.Combine(outPath, hood + "_" + sdsc.Instance + ".png"));
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

        private bool realIsNPC(ExtSDesc sdsc) { return sdsc.FamilyInstance == 0x7fff; }
        private bool realIsTownie(ExtSDesc sdsc) { return sdsc.FamilyInstance < 0x7fff && sdsc.FamilyInstance >= 0x7fe0; }
        private bool realIsPlayable(ExtSDesc sdsc) { return sdsc.FamilyInstance < 0x7fe0 && sdsc.FamilyInstance > 0; }
        private bool realIsUneditable(ExtSDesc sdsc) { return sdsc.FamilyInstance == 0; }

        #region ITool Members

        SimPe.Interfaces.Plugin.IToolResult ITool.ShowDialog(ref SimPe.Interfaces.Files.IPackedFileDescriptor pfd, ref SimPe.Interfaces.Files.IPackageFile package)
        {
            Rufio(null);
            return new SimPe.Plugin.ToolResult(false, false);
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

        public bool Parse(ref string[] args)
        {
            if (args.Length != 1 || !args[0].Trim().ToLower().Equals(rufio)) return false;
            List<string> largs = new List<string>(args);

            largs.RemoveAt(0);
            Rufio(largs);
            return true;
        }

        public string[] Help()
        {
            return new string[] { rufio, L.Get("pjCHoodHelp") };
        }

        #endregion
    }
}
