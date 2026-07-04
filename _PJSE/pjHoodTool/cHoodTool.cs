/***************************************************************************
 *   Copyright (C) 2008 by Peter L Jones                                   *
 *   pljones@users.sf.net                                                  *
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
    class cHoodTool : SimPe.Interfaces.AbstractTool, ITool, ICommandLine
    {
        string q(string u)
        {
            if (u == null) return u;
            return "\"" + u.Replace("\"", "\"\"") + "\"";
        }
        Settims getim = new Settims();
        internal static bool incbas = true;
        internal static bool incint = true;
        internal static bool inccha = true;
        internal static bool incski = true;
        internal static bool incuni = PathProvider.Global.GetExpansion(SimPe.Expansions.University).Exists;
        internal static bool incbus = PathProvider.Global.GetExpansion(SimPe.Expansions.Business).Exists;
        internal static bool incfre = PathProvider.Global.GetExpansion(SimPe.Expansions.FreeTime).Exists;
        internal static bool incapa = PathProvider.Global.GetExpansion(SimPe.Expansions.Apartments).Exists;
        internal static bool incnpc = true;
        internal static bool incdes = true;
        internal static bool incpet = (PathProvider.Global.GetExpansion(SimPe.Expansions.Pets).Exists || PathProvider.Global.CurrentGroup > 2);
        internal static bool inclot = true;
        internal static bool incfam = true;
        internal static bool overwrite = false;
        internal static string outptype = ".txt";
        string[] lotfams = new string[5000];

        delegate void Splash(string message);
        Splash splash;

        void Rufio(string output, string hood, int group)
        {
            int ct = 1;
            string foldim = "Rufio";
            if (output.Length == 0)
            {
                if (!overwrite)
                {
                    while (Directory.Exists(Path.Combine(SimPe.PathProvider.SimSavegameFolder, foldim)))
                    {
                        ct++;
                        foldim = "Rufio" + Convert.ToString(ct);
                    }
                }
                output = Path.Combine(Path.Combine(SimPe.PathProvider.SimSavegameFolder, foldim), "ExportedSims" + outptype);
            }
            else
            {
                if (Path.GetFileName(output).Contains("Rufio"))//user has selected an existing Rufio folder, don't put a new Rufio inside existing
                {
                    if (overwrite) { output = Path.Combine(output, "ExportedSims" + outptype); ct = -1; }
                    else
                    {
                        DirectoryInfo di = new DirectoryInfo(output);
                        output = di.Parent.FullName;
                    }
                }
                if (!overwrite)
                {
                    while (Directory.Exists(Path.Combine(output, foldim)))
                    {
                        ct++;
                        foldim = "Rufio" + Convert.ToString(ct);
                    }
                }
                if (ct > 0) output = Path.Combine(Path.Combine(output, foldim), "ExportedSims" + outptype);
            }

            string outPath = Path.GetDirectoryName(output);
            if (!Directory.Exists(outPath))
                Directory.CreateDirectory(outPath);

            if (group < 1) group = PathProvider.Global.CurrentGroup;

            //for (int i = 0; i < lotfams.Length; i++) lotfams[i] = "";

            StreamWriter w1 = new StreamWriter(output, false, Encoding.Default);
            w1.AutoFlush = true;
            StreamWriter w2 = null;
            StreamWriter w3 = null;
            if (inclot)
            {
                w2 = new StreamWriter(Path.Combine(outPath, "ExportedLots" + outptype), false, Encoding.Default);
                w2.AutoFlush = true;
            }
            if (incfam) // Boobies
            {
                w3 = new StreamWriter(Path.Combine(outPath, "ExportedFamily" + outptype), false, Encoding.Default);
                w3.AutoFlush = true;
            }

            splash("Export Neighbourhoods...");
            try
            {
                #region ExportedSims header
                string heady = "";
                if (incbas) heady = "hood,Hood Name,";
                heady += "NID,First Name,Last Name";
                if (incdes) heady += ",Sim Description";
                heady += ",Family Instance,Household Name,House Number";
                if (incbas) heady += ",AvailableCharacterData,Unlinked,ParentA,ParentB,Spouse,Children,Body Condition";
                if (booby.PrettyGirls.IsTitsInstalled() || booby.PrettyGirls.IsAngelsInstalled()) heady += ",Body Shape"; // Tits
                heady += ",NPC Type";
                if (incbas) heady += ",School Type,Grade,Career Performance,Career,Career Level,Zodiac Sign";
                heady += ",Gender,Life Section";
                if (incbas) heady += ",AgeDaysLeft,PrevAgeDays,AgeDuration,BlizLifelinePoints,LifelinePoints,LifelineScore";
                if (inccha) heady += ",GenActive,GenNeat,GenNice,GenOutgoing,GenPlayful,Active,Neat,Nice,Outgoing,Playful";// Character
                if (inccha && booby.PrettyGirls.PervyMode) heady += ",Amorousness"; // Tits
                if (incint) heady += ",Animals,Crime,Culture,Entertainment,Environment,Fashion,Food,Health,Money,Paranormal,Politics,School,Scifi,Sports,Toys,Travel,Weather,Work"; //Interests
                heady += ",FemalePreference,MalePreference";
                if (incski)
                {
                    heady += ",Body,Charisma,Cleaning,Cooking,Creativity,Fatness,Logic,Mechanical";//Skills
                    if (Helper.WindowsRegistry.ShowMoreSkills)
                        heady += ",Art,Music";
                }
                if (incuni) heady += ",IsAtUniversity,UniEffort,UniGrade,UniTime,UniSemester,UniInfluence,UniMajor"; // University
                if (incpet && incbas) heady += ",Species";
                if (incbus) heady += ",Job Assignment,Lot ID,Salary"; // Business
                if (incfre) heady += ",PrimaryAspiration,SecondaryAspiration,Natural Talent,LongtermAspiration"; // FreeTime
                if (incapa) heady += ",Reputation,Title"; // Aparments
                if (incapa && booby.PrettyGirls.PervyMode) heady += ",Penis";
                heady += "";

                w1.WriteLine(heady);
                #endregion

                #region ExportedLots header
                if (inclot)
                {
                    w2.WriteLine("hood" +
                        ",Hood Name" +
                        ",Hood Type" +
                        ",House Number,House Name" +
                        ",Lot Type" +
                        ",Family" +
                        ",Lot Flags" +
                        ""
                        );
                }
                #endregion

                #region ExportedFamilies header
                if (incfam) // Boobies
                {
                    w3.WriteLine("hood" +
                        ",Family Name" +
                        ",Family Number" +
                        ",Family Funds" +
                        ",Family Freinds" +
                        ",Currenty on Lot" +
                        ",Family Home Lot" +
                        ",Family Members" +
                        ""
                        );
                }
                #endregion

                string storypath = "";
                if (hood.Contains("\\"))
                {
                    storypath = hood.Substring(0, hood.IndexOf("\\"));
                    hood = hood.Substring(hood.LastIndexOf("\\") + 1, hood.Length - (hood.LastIndexOf("\\") + 1));
                }
                ExpansionItem.NeighborhoodPaths paths = PathProvider.Global.GetNeighborhoodsForGroup(group);
                foreach (ExpansionItem.NeighborhoodPath path in paths) // Big Boobies
                {
                    string sourcepath = path.Path;
                    if (storypath == "" || Path.GetFileName(sourcepath) == storypath)
                    {
                        string[] dirs = System.IO.Directory.GetDirectories(sourcepath, hood.Length > 0 ? hood : "*");
                        foreach (string dir in dirs)
                            if (!dir.Contains("Tutorial") || booby.PrettyGirls.IsTitsInstalled() || booby.PrettyGirls.IsAngelsInstalled())
                                AddHood(outPath, dir, w1, w2, w3);
                    }
                }
            }
            finally
            {
                w1.Close();
                if (inclot) w2.Close();
                if (incfam) w3.Close();
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
        void AddHood(string outPath, string dir, StreamWriter w1, StreamWriter w2, StreamWriter w3)
        {
            string hood = Path.GetFileName(dir);
            string hoodFile = Path.Combine(dir, hood + "_Neighborhood.package");
            if (!File.Exists(hoodFile)) return;

            for (int i = 0; i < lotfams.Length; i++) lotfams[i] = "";

            SimPe.Packages.File pkg = SimPe.Packages.File.LoadFromFile(hoodFile);
            if (pkg == null) return;
            string hoodtipe = "Primary";
            string hoodName = Localization.GetString("Unknown");
            IPackedFileDescriptor[] pfds = pkg.FindFiles(SimPe.Data.MetaData.CTSS_FILE);
            StrWrapper ctss = null;
            Idno ntype = null;
            if (pfds.Length == 1)
            {
                ctss = new StrWrapper();
                ctss.ProcessData(pfds[0], pkg);
                if (ctss[(byte)Helper.WindowsRegistry.LanguageCode, 0] == null)
                    hoodName = q(ctss[1, 0]);
                else
                    hoodName = q(ctss[(byte)Helper.WindowsRegistry.LanguageCode, 0]);
            }
            else hoodName = hood; // Tutorial Hood has no CTSS

            if (!Directory.Exists(Path.Combine(outPath, "SimImage")))
                Directory.CreateDirectory(Path.Combine(outPath, "SimImage"));

            System.Windows.Forms.Application.DoEvents();
            splash("Loading Neighbourhood " + hood + ": " + hoodName);
            SetProvider(pkg);

            dt = new DateTime(0);
            wasUnk = true;
            pfds = pkg.FindFiles(SimPe.Data.MetaData.SIM_DESCRIPTION_FILE);
            foreach (IPackedFileDescriptor spfd in pfds)
            {
                ExtSDesc sdsc = new ExtSDesc();
                sdsc.ProcessData(spfd, pkg);
                if ((incnpc || (sdsc.FamilyInstance != 0 && sdsc.FamilyInstance < 0x7f00)) && (incpet || sdsc.Nightlife.Species == SdscNightlife.SpeciesType.Human))
                    AddSim(outPath, hood, hoodName, w1, sdsc);
            }
            if (booby.PrettyGirls.PervyMode) WaitingScreen.UpdateImage(SimPe.GetImage.BabyDoll);
            else WaitingScreen.UpdateImage(null);

            if (inclot)
            {
                if (!Directory.Exists(Path.Combine(outPath, "LotImage")))
                    Directory.CreateDirectory(Path.Combine(outPath, "LotImage"));

                splash("Loading Lots...");
                pfds = pkg.FindFiles(SimPe.Plugin.Ltxt.Ltxttype);
                foreach (IPackedFileDescriptor spfd in pfds)
                {
                    Ltxt ltxt = new Ltxt();
                    ltxt.ProcessData(spfd, pkg);
                    AddLot(outPath, hood, hoodName, w2, ltxt, hoodtipe);
                }
                // try to add all lots
                try
                {
                    string[] overs = Directory.GetFiles(Path.GetDirectoryName(hoodFile), hood + "*.package", SearchOption.TopDirectoryOnly);
                    foreach (string file in overs)
                    {
                        if (file != hoodFile)
                        {
                            pkg = SimPe.Packages.File.LoadFromFile(file);
                            hoodName = Localization.GetString("Unknown");
                            pfds = pkg.FindFiles(SimPe.Data.MetaData.CTSS_FILE);
                            if (pfds.Length == 1)
                            {
                                ctss = new StrWrapper();
                                ctss.ProcessData(pfds[0], pkg);
                                if (ctss[(byte)Helper.WindowsRegistry.LanguageCode, 0] == null)
                                    hoodName = q(ctss[1, 0]);
                                else
                                    hoodName = q(ctss[(byte)Helper.WindowsRegistry.LanguageCode, 0]);

                                System.Windows.Forms.Application.DoEvents();
                            }
                            pfds = pkg.FindFiles(SimPe.Data.MetaData.IDNO);
                            if (pfds.Length == 1)
                            {
                                ntype = new Idno();
                                ntype.ProcessData(pfds[0], pkg);
                                hoodtipe = System.Enum.GetName(typeof(NeighborhoodType), ntype.Type);
                                if (booby.PrettyGirls.IsTitsInstalled() || booby.PrettyGirls.IsAngelsInstalled())
                                {
                                    if (ntype.Subep == SimPe.Data.MetaData.NeighbourhoodEP.MansionGarden && hoodtipe == "Suburb")
                                    {
                                        if (ntype.SubName == "X002") hoodtipe = "Space Holiday";
                                        else if (ntype.SubName == "X003") hoodtipe = "Lost Island";
                                        else hoodtipe = "Pervy Suburb";
                                    }
                                    else if (ntype.SubName == "T100" && hoodtipe == "Island")
                                        hoodtipe = "Western Holiday";
                                    else if (ntype.Subep != SimPe.Data.MetaData.NeighbourhoodEP.Business && hoodtipe == "Suburb")
                                        hoodtipe = "Hidden Suburb";
                                }
                                else if (ntype.Subep != SimPe.Data.MetaData.NeighbourhoodEP.Business && hoodtipe == "Suburb")
                                    hoodtipe = "Hidden Suburb";
                            }

                            SetProvider(pkg);
                            pfds = pkg.FindFiles(SimPe.Plugin.Ltxt.Ltxttype);
                            foreach (IPackedFileDescriptor spfd in pfds)
                            {
                                Ltxt ltxt = new Ltxt();
                                ltxt.ProcessData(spfd, pkg);
                                AddLot(outPath, hood, hoodName, w2, ltxt, hoodtipe);
                            }
                        }
                    }
                }
                catch { }
                //Array.Clear(lotfams, 0, 5000);
            }

            if (incfam) // Boobies
            {
                if (!Directory.Exists(Path.Combine(outPath, "FamilyImage")))
                    Directory.CreateDirectory(Path.Combine(outPath, "FamilyImage"));

                dt = new DateTime(0);
                pkg = SimPe.Packages.File.LoadFromFile(hoodFile);
                SetProvider(pkg);
                pfds = pkg.FindFiles(0x46414D49); // FAMI
                foreach (IPackedFileDescriptor spfd in pfds)
                {
                    Fami fmil = new Fami(FileTable.ProviderRegistry.SimNameProvider);
                    fmil.ProcessData(spfd, pkg);                    
                    AddFami(outPath, hood, w3, fmil);
                }
            }
        }

        void AddSim(string outPath, string hood, string hoodName, StreamWriter w, ExtSDesc sdsc)
        {
            #region desc
            if (sdsc.CharacterFileName == null) return; //sim is probably in Downloads
            string desc = ",";
            try { desc = sdsc.SimName + "," + sdsc.SimFamilyName; }
            catch { return; }
            if (incdes)
            {
                desc += ",";
                if (Path.GetFileName(sdsc.CharacterFileName.ToLower()) != "objects.package") // won't find the correct Catalogue Description in objects.package
                {
                    SimPe.Interfaces.Files.IPackageFile pkg = SimPe.Packages.File.LoadFromFile(sdsc.CharacterFileName);
                    if (pkg != null)
                    {
                        IPackedFileDescriptor[] pfds = pkg.FindFiles(StrWrapper.CTSStype);
                        if (pfds != null && pfds.Length > 0)
                        {
                            try
                            {
                                StrWrapper ctss = new StrWrapper();
                                ctss.ProcessData(pfds[0], pkg);
                                if (ctss[(byte)Helper.WindowsRegistry.LanguageCode, 1] == null)
                                    desc += q(ctss[1, 1]).Replace(",", " ").Replace("\r", "").Replace("\n", " ") + "";
                                else if (ctss[(byte)Helper.WindowsRegistry.LanguageCode, 1] == "")
                                    desc += q(ctss[1, 1]).Replace(",", " ").Replace("\r", "").Replace("\n", " ") + "";
                                else
                                    desc += q(ctss[(byte)Helper.WindowsRegistry.LanguageCode, 1]).Replace(",", " ").Replace("\r", "").Replace("\n", " ") + ""; // description
                            }
                            catch { }
                        }
                    }
                }
                else desc += "Unique NPC (built into The Sims 2)";
            }
            #endregion

            #region family
            string family = ",,";
            string hsn;
            IPackedFileDescriptor pfd = sdsc.Package.FindFile(0x46414D49, 0x00000000, 0xffffffff, sdsc.FamilyInstance); // FAMI
            if (pfd != null)
            {
                Fami fami = null;
                fami = new Fami(FileTable.ProviderRegistry.SimNameProvider);
                fami.ProcessData(pfd, sdsc.Package);
                if (sdsc.FamilyInstance == 0) hsn = "No Family"; else hsn = q(sdsc.HouseholdName);

                family = sdsc.FamilyInstance +
                    "," + hsn +
                    "," + fami.LotInstance +
                    ""
                    ;
                if (fami.LotInstance != 0)
                    lotfams[fami.LotInstance] = sdsc.HouseholdName;
            }
            #endregion

            #region ties
            string ties = ",,,";
            if (eft != null)
            {
                SDesc[] p = eft.ParentSims(sdsc);
                SDesc[] s = eft.SpouseSims(sdsc);
                if (p == null || p.Length == 0) ties = ",,";
                else
                {
                    ties = p[0].Instance + " (" + p[0].SimName + ")" + ",";
                    if (p.Length > 1) ties += p[1].Instance + " (" + p[1].SimName + ")";
                    ties += ",";
                }
                ties += (s == null || s.Length < 1 ? "" : s[0].Instance + " (" + s[0].SimName + ")" + "") + ",";
                /*
                ties = (p == null || p.Length < 2 ? "," : p[0].Instance + " (" + p[0].SimName + ")" + "," + p[1].Instance + " (" + p[1].SimName + ")") +
                    "," + (s == null || s.Length < 1 ? "" : s[0].Instance + " (" + s[0].SimName + ")" + "") + ",";
                */
                SDesc[] c = eft.ChildSims(sdsc);
                if (c.Length > 0)
                {
                    foreach (SDesc k in c)
                    {
                        ties += k.Instance + " (" + k.SimName + ")*";
                    }// Boobies
                    if (ties.EndsWith("*")) ties = ties.Remove(ties.LastIndexOf("*"));
                }
                ties += "";
            }
            #endregion

            #region genetics
            string genetics = sdsc.GeneticCharacter.Active +
                "," + sdsc.GeneticCharacter.Neat +
                "," + sdsc.GeneticCharacter.Nice +
                "," + sdsc.GeneticCharacter.Outgoing +
                "," + sdsc.GeneticCharacter.Playful +
                ""
            ;
            #endregion

            #region character
            string character = sdsc.Character.Active +
                "," + sdsc.Character.Neat +
                "," + sdsc.Character.Nice +
                "," + sdsc.Character.Outgoing +
                "," + sdsc.Character.Playful +
                ""
            ;
            #endregion

            #region interests
            string interests = sdsc.Interests.Animals +
                "," + sdsc.Interests.Crime +
                "," + sdsc.Interests.Culture +
                "," + sdsc.Interests.Entertainment +
                "," + sdsc.Interests.Environment +
                "," + sdsc.Interests.Fashion +
                "," + sdsc.Interests.Food +
                "," + sdsc.Interests.Health +
                "," + sdsc.Interests.Money +
                "," + sdsc.Interests.Paranormal +
                "," + sdsc.Interests.Politics +
                "," + sdsc.Interests.School +
                "," + sdsc.Interests.Scifi +
                "," + sdsc.Interests.Sports +
                "," + sdsc.Interests.Toys +
                "," + sdsc.Interests.Travel +
                "," + sdsc.Interests.Weather +
                "," + sdsc.Interests.Work +
                ""
            ;
            #endregion

            #region skills
            string skills = sdsc.Skills.Body +
                "," + sdsc.Skills.Charisma +
                "," + sdsc.Skills.Cleaning +
                "," + sdsc.Skills.Cooking +
                "," + sdsc.Skills.Creativity +
                "," + sdsc.Skills.Fatness +
                "," + sdsc.Skills.Logic +
                "," + sdsc.Skills.Mechanical;
            if (Helper.WindowsRegistry.ShowMoreSkills)
                skills += "," + sdsc.Skills.Art + "," + sdsc.Skills.Music;
            skills += "";
            #endregion

            #region university
            string university = "N,,,,,,";
            if (sdsc.University != null)
            {
                if (sdsc.University.OnCampus == 0x1)
                {
                    university = "Y" +
                    "," + sdsc.University.Effort +
                    "," + sdsc.University.Grade +
                    "," + sdsc.University.Time +
                    "," + sdsc.University.Semester +
                    "," + sdsc.University.Influence +
                    "," + sdsc.University.Major
                    ;
                }
                else if (sdsc.University.Major != SimPe.Data.Majors.Unknown && sdsc.University.Major != SimPe.Data.Majors.Undeclared && sdsc.University.Major != SimPe.Data.Majors.Unset) university = "N,,,,,," + sdsc.University.Major;
            }
            #endregion

            #region business
            string business = ",,";
            if (sdsc.Business != null)
            {
                if (sdsc.Business.Salary > 0)
                {
                    business = Localization.GetString("SimPe.PackedFiles.Wrapper.JobAssignf." + Enum.GetName(typeof(JobAssignf), (ushort)sdsc.Business.Assignf));
                    business += "," + sdsc.Business.LotID;
                    business += "," + sdsc.Business.Salary;
                }
            }
            #endregion

            #region freetime
            string freetime = ",,,";
            if (sdsc.Freetime != null)
            {
                freetime = new SimPe.Data.LocalizedAspirationTypes(sdsc.Freetime.PrimaryAspiration).ToString() +
                    "," + new SimPe.Data.LocalizedAspirationTypes(sdsc.Freetime.SecondaryAspiration).ToString() +
                    "," + sdsc.Freetime.HobbyPredistined +
                    "," + sdsc.Freetime.LongtermAspiration // LifetimeWant ? - NO
                ;
                //sdsc.Freetime.BugCollection -- no...
            }
            #endregion

            #region apartments
            string apartments = ",";
            if (booby.PrettyGirls.PervyMode) apartments = ",,";
            if (sdsc.Apartment != null)
            {
                apartments = sdsc.Apartment.Reputation + ",";
                if (sdsc.Apartment.TitlePostName > 0)
                    apartments += SimPe.Data.MetaData.GetTitleName(sdsc.Apartment.TitlePostName);
                if (booby.PrettyGirls.PervyMode)
                {
                    apartments += ",";
                    if (sdsc.CharacterDescription.Gender == SimPe.Data.MetaData.Gender.Male && sdsc.Nightlife.Species == SdscNightlife.SpeciesType.Human)
                        apartments += new SimPe.Data.LocalizedPenisLength((SimPe.Data.MetaData.PenisLength)sdsc.Apartment.PenisLength).ToString();
                }
            }
            #endregion

            #region Species
            string species = "Human";
            if (sdsc.Nightlife != null)
            {
                if ((int)sdsc.Version == (int)SimPe.PackedFiles.Wrapper.SDescVersions.Castaway && sdsc.Castaway.Subspecies > 0)
                {
                    if (sdsc.Castaway.Subspecies == 2) species = "Orang-utan";
                    if (sdsc.Castaway.Subspecies == 1 && (int)sdsc.Nightlife.Species == 3) species = "Leopard";
                    if (sdsc.Castaway.Subspecies == 1 && (int)sdsc.Nightlife.Species < 3) species = "Wild Dog";
                }
                else
                    species = sdsc.Nightlife.Species.ToString();
            }
            #endregion

            string csv = "";
            if (incbas) csv = hood + "," + hoodName + ",";
            csv += sdsc.Instance + "," + desc + "," + family;
            if (incbas) csv += "," + (sdsc.AvailableCharacterData ? "Y" : "N") + "," + (sdsc.Unlinked != 0x00 ? "Y" : "N") + "," + ties + "," + bodycondiion(sdsc);
            if (booby.PrettyGirls.IsTitsInstalled() || booby.PrettyGirls.IsAngelsInstalled())
                csv += "," + new SimPe.Data.LocalizedBodyshape(sdsc.CharacterDescription.Bodyshape).ToString(); // Tits
            csv += "," + new SimPe.Data.LocalizedServiceTypes(sdsc.CharacterDescription.ServiceTypes).ToString();
            if (incbas) csv += "," + new SimPe.Data.LocalizedSchoolType(sdsc.CharacterDescription.SchoolType).ToString() +
                "," + new SimPe.Data.LocalizedGrades(sdsc.CharacterDescription.Grade).ToString() +
                "," + sdsc.CharacterDescription.CareerPerformance +
                "," + new SimPe.Data.LocalizedCareers(sdsc.CharacterDescription.Career).ToString() +
                "," + sdsc.CharacterDescription.CareerLevel +
                "," + sdsc.CharacterDescription.ZodiacSign;
            csv += "," + sdsc.CharacterDescription.Gender +
                "," + sdsc.CharacterDescription.LifeSection;
            if (incbas) csv += "," + sdsc.CharacterDescription.Age +
                "," + sdsc.CharacterDescription.PrevAgeDays +
                "," + sdsc.CharacterDescription.AgeDuration +
                "," + sdsc.CharacterDescription.BlizLifelinePoints +
                "," + sdsc.CharacterDescription.LifelinePoints +
                "," + sdsc.CharacterDescription.LifelineScore;
            if (inccha) csv += "," + genetics + "," + character;
            if (inccha && booby.PrettyGirls.PervyMode) csv += "," + sdsc.Skills.Romance; // Tits
            if (incint) csv += "," + interests;
            csv += "," + sdsc.Interests.FemalePreference + "," + sdsc.Interests.MalePreference;
            if (incski) csv += "," + skills;
            if (incuni) csv += "," + university;
            if (incpet && incbas) csv += "," + species;
            if (incbus) csv += "," + business;
            if (incfre) csv += "," + freetime;
            if (incapa) csv += "," + apartments;
            csv += "";
            w.WriteLine(csv);

            Image img = null;

            if (sdsc.Image != null)
                if (sdsc.Image.Width > 5)
                    img = Ambertation.Drawing.GraphicRoutines.KnockoutImage(sdsc.Image, new Point(0, 0), Color.Magenta);

            if (img == null)
            {
                if (sdsc.CharacterDescription.IsWoman && sdsc.Nightlife.Species == 0)
                    img = SimPe.GetImage.BabyDoll;
                else if (sdsc.CharacterDescription.Gender == SimPe.Data.MetaData.Gender.Female)
                    img = SimPe.GetImage.SheOne;
                else
                    img = SimPe.GetImage.NoOne;
            }

            img = Ambertation.Windows.Forms.Graph.ImagePanel.CreateThumbnail(img, new System.Drawing.Size(256, 256), 12, Color.FromArgb(90, Color.Black), SimPe.PackedFiles.Wrapper.SimPoolControl.GetImagePanelColor(sdsc), Color.White, Color.FromArgb(80, Color.White), true, 4, 0);
            AddImage(img, Path.Combine(Path.Combine(outPath, "SimImage"), hood + "_" + sdsc.Instance + ".png"));

            if (dt.Equals(new DateTime(0)) || wasUnk || dt.AddMilliseconds(300).CompareTo(DateTime.UtcNow) < 0)
            {
                System.Windows.Forms.Application.DoEvents();
                if (!((string)(sdsc.SimName + " " + sdsc.SimFamilyName)).Trim().ToLower().Equals("unknown"))
                {
                    dt = new DateTime(DateTime.UtcNow.Ticks);
                    wasUnk = false;
                    splash("Saving " + sdsc.SimName + " " + sdsc.SimFamilyName);
                    WaitingScreen.UpdateImage(img);
                }
                else
                    wasUnk = true;
            }
        }

        void AddLot(string outPath, string hood, string hoodName, StreamWriter w, Ltxt ltxt, string hoodtype)
        {
            string perv = "";
            Boolset bby = ltxt.Unknown0;
            if (bby[7]) perv = "Has Beach";
            if (bby[4]) perv += " - Hidden";
            Boolset tty = ltxt.Unknown4;
            if (ltxt.Type == Ltxt.LotType.Hobby)
            {
                if (tty[9]) perv += " (Music)";
                if (tty[8]) perv += " (Science)";
                if (tty[7]) perv += " (Fitness)";
                if (tty[6]) perv += " (Tinkering)";
                if (tty[5]) perv += " (Nature)";
                if (tty[4]) perv += " (Games)";
                if (tty[3]) perv += " (Sport)";
                if (tty[2]) perv += " (Films)";
                if (tty[1]) perv += " (Art)";
                if (tty[0]) perv += " (Cooking)";
            }
            if (booby.PrettyGirls.PervyMode)
            {
                if (tty[10]) perv += " - Woohoo Club";
                if (tty[20]) perv += " - Ladies Only";
                if (tty[21]) perv += " - Dudes Only";
                if (tty[11]) perv += " - Adults Only";
                if (tty[23]) perv += " - Community Lot";
                if (tty[24]) perv += " - Jungle Lot";
            }
            if (hoodtype != "Village" && hoodtype != "Lakes" && hoodtype != "Island")
            {
                if (lotfams[ltxt.LotDescription.Instance] == "" && ltxt.Type == Ltxt.LotType.Residential)
                {
                    if (hoodtype == "University") lotfams[ltxt.LotDescription.Instance] = "*For Rent*";
                    else lotfams[ltxt.LotDescription.Instance] = "*For Sale*";
                }
            }
            w.WriteLine(hood +
                "," + hoodName +
                "," + hoodtype +
                "," + (ltxt.LotDescription == null ? "," : ltxt.LotDescription.Instance + "," + q(ltxt.LotName.Replace("\r", "").Replace("\n", ""))) +
                "," + q(Enum.GetName(typeof(Ltxt.LotType), ltxt.Type)) +
                "," + lotfams[ltxt.LotDescription.Instance] +
                "," + perv +
                ""
                ); //ltxt.LotDescription.LotName
            if (ltxt.LotDescription != null)
            {
                if (ltxt.LotDescription.Image != null)
                    AddImage(ltxt.LotDescription.Image, Path.Combine(Path.Combine(outPath, "LotImage"), hood + "_Lot" + ltxt.FileDescriptor.Instance + ".jpg"));
                else
                    AddImage(SimPe.GetImage.Network, Path.Combine(Path.Combine(outPath, "LotImage"), hood + "_Lot" + ltxt.FileDescriptor.Instance + ".png"));
            }
            else
                AddImage(SimPe.GetImage.Network, Path.Combine(Path.Combine(outPath, "LotImage"), hood + "_Lot" + ltxt.FileDescriptor.Instance + ".png"));
            /*
            if (dt.Equals(new DateTime(0)) || dt.AddMilliseconds(200).CompareTo(DateTime.UtcNow) < 0)
            {
                dt = new DateTime(DateTime.UtcNow.Ticks);
                splash("Saving " + ltxt.LotName);
            } */
        }

        void AddFami(string outPath, string hood, StreamWriter w, Fami fml) // Boobies
        {
            string grls = ""; // adding all the sims is pointless as they are stored by GUID only and that won't be helpfull later
            foreach (string tit in fml.SimNames)
                grls += tit + "*";
            if (grls.EndsWith("*")) grls = grls.Remove(grls.LastIndexOf("*"));
            string fnme = fml.Name;
            if (fnme == null) fnme = "**" + SimPe.Data.MetaData.NPCFamily(fml.FileDescriptor.Instance);
            w.WriteLine(hood +
                "," + fnme +
                "," + Convert.ToString(fml.FileDescriptor.Instance) +
                "," + Convert.ToString(fml.Money) +
                "," + Convert.ToString(fml.Friends) +
                "," + Convert.ToString(fml.CurrentlyOnLotInstance) +
                "," + Convert.ToString(fml.LotInstance) +
                "," + grls +
                ""
                );
            if (fml.FileDescriptor.Instance > 0 && fml.FileDescriptor.Instance < 32512)
            {
                if (fml.FamiThumb != null)
                    AddImage(Ambertation.Windows.Forms.Graph.ImagePanel.CreateThumbnail(fml.FamiThumb, new System.Drawing.Size(256, 256), 12, Color.FromArgb(90, Color.Black), SystemColors.ControlDarkDark, Color.White, Color.FromArgb(80, Color.White), true, 4, 0), Path.Combine(Path.Combine(outPath, "FamilyImage"), hood + "_Family" + Convert.ToString(fml.FileDescriptor.Instance) + ".png"));
            }
            if (dt.Equals(new DateTime(0)) || dt.AddMilliseconds(300).CompareTo(DateTime.UtcNow) < 0)
            {
                dt = new DateTime(DateTime.UtcNow.Ticks);
                splash("Saving " + fml.Name + " family");
            }
        }

        string bodycondiion(ExtSDesc simdsc)
        {
            string bodyflugs = "";
            if (simdsc.CharacterDescription.GhostFlag.IsGhost) bodyflugs = "Deceased";
            else
            {
                if (simdsc.CharacterDescription.BodyFlag.Value == 0) bodyflugs = "Normal";
                if (simdsc.CharacterDescription.BodyFlag.BirthControl) bodyflugs = "BirthControl";
                if (simdsc.CharacterDescription.BodyFlag.Hospital) bodyflugs += " Hospital";
                if (simdsc.CharacterDescription.BodyFlag.Fit) bodyflugs += " Fit";
                if (simdsc.CharacterDescription.BodyFlag.Fat) bodyflugs += " Fat";
                if (simdsc.CharacterDescription.BodyFlag.PregnantHidden) bodyflugs += " Pregnant";
                if (simdsc.CharacterDescription.BodyFlag.PregnantHalf) bodyflugs += " PregnantHalf";
                if (simdsc.CharacterDescription.BodyFlag.PregnantFull) bodyflugs += " PregnantFull";
            }
            return bodyflugs;
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
                    "Please specify the correct SaveGame Folder in the Options Dialogue.");
                return new ToolResult(false, false);
            }

            System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();
            fbd.Description = "Choose the folder for extracted Sim data";
            fbd.SelectedPath = SimPe.PathProvider.SimSavegameFolder;
            fbd.ShowNewFolderButton = true;
            System.Windows.Forms.DialogResult dr = fbd.ShowDialog();
            if (dr != System.Windows.Forms.DialogResult.OK) return new ToolResult(false, false);
            NeighborhoodForm nfm = new NeighborhoodForm();
            nfm.LoadNgbh = false;
            nfm.ShowBackupManager = false;
            nfm.ShowSubHoods = false;
            if (PathProvider.Global.CurrentGroup == 8) nfm.Text = "Please Make A Selection - Castaway Story Edition makes a mess if you extract all";
            else nfm.Text = "Close window without selection to extract all";
            SimPe.Interfaces.Plugin.IToolResult ret = nfm.Execute(ref package, null);

            string hood = "";
            if (nfm.DialogResult == System.Windows.Forms.DialogResult.OK && nfm.SelectedNgbh != null) // Big Boobies
            {
                DirectoryInfo di = new DirectoryInfo(nfm.SelectedNgbh);
                string sthood = "";
                if (PathProvider.Global.CurrentGroup > 1) sthood = di.Parent.Parent.Name + "\\";
                hood = sthood + Path.GetFileName(Path.GetDirectoryName(nfm.SelectedNgbh));
            }
            bool dun;
            if (!SimPe.Helper.WindowsRegistry.HiddenMode)
            {
                Settims sf = new Settims();
                sf.Text = hood;
                sf.ShowDialog();
            }
            else
                dun = getim.Settings;

            try
            {
                SimPe.WaitingScreen.Wait();
                if (booby.PrettyGirls.PervyMode) WaitingScreen.UpdateImage(SimPe.GetImage.BabyDoll);
                splash = delegate(string message) { SimPe.WaitingScreen.UpdateMessage(message); };
                Rufio(fbd.SelectedPath, hood, 0);
                return new SimPe.Plugin.ToolResult(false, false);
            }
            finally
            {
                WaitingScreen.UpdateImage(null);
                WaitingScreen.Stop();
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
            if ((byte)Helper.WindowsRegistry.LanguageCode == 1)
                return "Neighbourhood\\Export Neighborhoods...";
            else
                return "Neighbourhood\\Export Neighbourhoods...";
        }

        #endregion

        #region IToolExt Member
        public override System.Drawing.Image Icon
        {
            get
            {
                return SimPe.GetIcon.HoodTool;
            }
        }
        #endregion

        #region ICommandLine Members

        public bool Parse(List<string> argv)
        {
            int i = ArgParser.Parse(argv, "-rufio");
            if (i < 0) return false;
            string outpath = "";
            string hood = "";
            string group = "";
            int groupno = 0;
            bool dun = getim.Settings;
            bool previ = Helper.WindowsRegistry.LoadTableAtStartup;
            Helper.WindowsRegistry.LoadTableAtStartup = false;
            while (argv.Count > i)
            {
                if (ArgParser.Parse(argv, i, "-out", ref outpath)) continue;
                if (ArgParser.Parse(argv, i, "-hood", ref hood)) continue;
                if (ArgParser.Parse(argv, i, "-group", ref group)) continue;
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
                SimPe.Message.Show("The Folder " + PathProvider.Global.NeighborhoodFolder + " was not found.\r\n" +
                    "Please specify the correct SaveGame Folder in the Options Dialogue.");
                return false;
            }

            if (group.Length > 0)
            {
                try { groupno = Convert.ToInt32(group); if (groupno != 1 && groupno != 2 && groupno != 4 && groupno != 8) throw new FormatException(); }
                catch (FormatException)
                {
                    SimPe.Message.Show("Invalid group.  Please specify a group from expansions.xreg.");
                    return false;
                }
            }

            splash = delegate(string message) { SimPe.Splash.Screen.SetMessage(message); };
            Rufio(outpath, hood, groupno);
            splash("");
            Helper.WindowsRegistry.LoadTableAtStartup = previ;
            return true;
        }

        public string[] Help()
        {
            return new string[] { "-rufio\r\n-out {output path}\r\n-hood {input neighbourhood folder}\r\n-group {group number (1,2,4 or 8)}", "Export Neighbourhood data" };
        }

        #endregion
    }
}
