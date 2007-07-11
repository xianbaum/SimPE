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
using System.IO;
using System.Windows.Forms;
using SimPe.Interfaces;
using SimPe.Interfaces.Scenegraph;
using SimPe.Interfaces.Files;

namespace pj
{
    public class BodyMeshExtractor : ITool
    {
        private IPackageFile currentPackage;
        private String getFilename()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.AddExtension = true;
            ofd.CheckFileExists = true;
            ofd.CheckPathExists = true;
            ofd.DefaultExt = ".package";
            ofd.DereferenceLinks = true;
            ofd.FileName = "";
            ofd.Filter = L.Get("pkgFilter");
            ofd.FilterIndex = 0;
            ofd.InitialDirectory = "%MyDocuments%/EA Games/The Sims 2/Saved Sims";//.../My Documents/EA Games/The Sims 2/Saved Sims
            ofd.Multiselect = false;
            ofd.ReadOnlyChecked = true;
            ofd.ShowHelp = ofd.ShowReadOnly = false;
            ofd.Title = L.Get("selectPkgTexture");
            ofd.ValidateNames = true;
            DialogResult dr = ofd.ShowDialog();
            if (DialogResult.OK.Equals(dr))
                return ofd.FileName;
            return null;
        }

        private static String SimsPath = SimPe.PathProvider.Global[0].InstallFolder;
        private static ArrayList paths = new ArrayList();
        private static ArrayList packs = new ArrayList();

        static BodyMeshExtractor()
        {
            for (int i = SimPe.PathProvider.Global.Expansions.Count; --i >= 0;)
            {
                String path = SimPe.PathProvider.Global[i].InstallFolder;
                if (path.Length == 0 || (i != 0 && path.Equals(SimsPath)))
                    continue;
                paths.Add(path);

                if (!Directory.Exists(Path.Combine(path, "TSData\\Res\\Catalog\\Bins"))) continue;
                string[] va = Directory.GetFiles(Path.Combine(path, "TSData\\Res\\Catalog\\Bins"), "*.package");
                foreach (String pkg in va)
                {
                    if (!pkg.ToLower().EndsWith("\\globalcatbin.bundle.package"))
                        packs.Insert(0, pkg);
                }
            }
        }

        private bool findAndAdd(String name, uint type, String source)
        {
            foreach (String path in paths)
            {
                if (path.Length == 0)
                    continue;

                String sims2loc = Path.Combine(path, "TSData\\Res\\3D");
                if (!Directory.Exists(sims2loc))
                    sims2loc = Path.Combine(path, "TSData\\Res\\Sims3D");
                if (!Directory.Exists(sims2loc))
                    continue;

                if (!addFromPkg(name, type, Path.Combine(sims2loc, source)))
                    continue;

                return true;
            }

            foreach (String pack in packs)
            {
                if (!addFromPkg(name, type, pack))
                    continue;

                return true;
            }

            return false;
        }

        private bool addFromPkg(String name, uint type, String pkg)
        {
            IPackageFile p = SimPe.Packages.File.LoadFromFile(pkg);
            if (p == null)
                return false;

            IPackedFileDescriptor[] pfa = p.FindFiles(SimPe.Data.MetaData.NAME_MAP);
            if (pfa == null || pfa.Length != 1)
                return false;

            SimPe.Plugin.Nmap nmap = new SimPe.Plugin.Nmap(null);
            nmap.ProcessData(pfa[0], p);
            pfa = nmap.FindFiles(name + "_");
            if (pfa == null || pfa.Length != 1)
                return false;

            IPackedFileDescriptor pfd = null;
            for (int j = 0; j < p.Index.Length && pfd == null; j++)
                if (p.Index[j].Type == type
                    && p.Index[j].Group == pfa[0].Group
                    && p.Index[j].Instance == pfa[0].Instance)
                    pfd = p.Index[j];
            if (pfd == null)
                return false;
            if (isInPFDList(currentPackage.Index, pfd))
                return true;

            IPackedFileDescriptor npfd = pfd.Clone();
            npfd.UserData = p.Read(pfd).UncompressedData;
            currentPackage.Add(npfd, true);

            return true;
        }

        private bool isInPFDList(IPackedFileDescriptor[] pfdList, IPackedFileDescriptor pfd)
        {
            foreach (IPackedFileDescriptor i in pfdList)
                if (i.Filename.Equals(pfd.Filename))
                    return true;
            return false;
        }

        private void Main()
        {
            ArrayList al = new ArrayList();

            #region Prompt for mesh name or browse for package and extract names
            GetMeshName gmn = new GetMeshName();
            DialogResult dr = gmn.ShowDialog();
            if (dr.Equals(DialogResult.OK))
            {
                if (gmn.MeshName.Length > 0)
                    al.Add(gmn.MeshName);
                else
                {
                    MessageBox.Show(L.Get("noMeshName"), L.Get("pjSME"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else if (dr.Equals(DialogResult.Retry)) // nasty...
            {
                #region Get body mesh package file name and open the package
                String bodyMeshPackage = getFilename();
                if (bodyMeshPackage == null) return;

                IPackageFile p = SimPe.Packages.File.LoadFromFile(bodyMeshPackage);
                if (p == null) return;
                #endregion

                #region Find the Property Set or XML Mesh Overlay
                IPackedFileDescriptor[] pfa = p.FindFiles(SimPe.Data.MetaData.GZPS);
                IPackedFileDescriptor[] pfb = p.FindFiles(0x0C1FE246); // XMOL?
                if ((pfa == null || pfa.Length == 0) && (pfb == null || pfb.Length == 0))
                {
                    MessageBox.Show(L.Get("noGZPSXMOL"),
                        L.Get("pjSME"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                #endregion

                #region Get the mesh name(s)
                bool prompted = false;
                SimPe.PackedFiles.Wrapper.Cpf cpf = new SimPe.PackedFiles.Wrapper.Cpf();
                for (int i = 0; i < pfa.Length + pfb.Length; i++)
                {
                    if (i < pfa.Length)
                        cpf.ProcessData(pfa[i], p);
                    else
                        cpf.ProcessData(pfb[i - pfa.Length], p);

                    for (int j = 0; j < cpf.Items.Length; j++)
                    {
                        if (cpf.Items[j].Name.ToLower().Equals("name"))
                            al.Add(cpf.Items[j].StringValue);
                        if (al.Count > 1 && !prompted)
                        {
                            if (MessageBox.Show(L.Get("multipleMeshes"),
                                L.Get("pjSME"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                                != DialogResult.Yes)
                                return;
                            prompted = true;
                        }
                    }
                }
                if (al.Count == 0)
                {
                    MessageBox.Show(L.Get("noMeshPkg"),
                        L.Get("pjSME"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                #endregion
            }
            else
                return;

            #endregion

            #region For each mesh, find the GMDC, GMND, SHPE and CRES and add them to the current package

            foreach (String m in al)
            {
                String[] ma = m.Split('_');
                String mesh = ma[ma[0].Equals("CASIE") ? 1 : 0];
                if (mesh.ToLower().StartsWith("ym")) mesh = "am" + mesh.Substring(2);
                if (mesh.ToLower().StartsWith("yf")) mesh = "af" + mesh.Substring(2);

                bool success = true;
                SimPe.RemoteControl.ApplicationForm.Cursor = Cursors.WaitCursor;
                success = success && findAndAdd(mesh, SimPe.Data.MetaData.GMDC, "Sims03.package");
                success = success && findAndAdd(mesh, SimPe.Data.MetaData.GMND, "Sims04.package");
                success = success && findAndAdd(mesh, SimPe.Data.MetaData.SHPE, "Sims05.package");
                success = success && findAndAdd(mesh, SimPe.Data.MetaData.CRES, "Sims06.package");
                SimPe.RemoteControl.ApplicationForm.Cursor = Cursors.Default;
                if (!success)
                    MessageBox.Show(L.Get("notAllPartsFound") + m,
                        L.Get("pjSME"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            #endregion
        }

        #region ITool Members

        public bool IsEnabled(IPackedFileDescriptor pfd, IPackageFile package)
        {
            currentPackage = package;
            return package != null;
        }

        public SimPe.Interfaces.Plugin.IToolResult ShowDialog(ref SimPe.Interfaces.Files.IPackedFileDescriptor pfd, ref SimPe.Interfaces.Files.IPackageFile package)
        {
            Main();
            return new SimPe.Plugin.ToolResult(false, false);
        }


        #region IToolPlugin Members

        public override string ToString()
        {
            return L.Get("pjBMTExtract");
        }

        #endregion
        #endregion
    }
}
