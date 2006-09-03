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
using System.Windows.Forms;
using SimPe.Interfaces;
using SimPe.Interfaces.Plugin;
using SimPe.Interfaces.Scenegraph;
using SimPe.Interfaces.Files;

namespace pj
{
    public class BodyMeshTool : ITool
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
            ofd.Filter = "Package file|*.package|All files|*.*";
            ofd.FilterIndex = 0;
            ofd.InitialDirectory = "%MyDocuments%/EA Games/The Sims 2/Saved Sims";//.../My Documents/EA Games/The Sims 2/Saved Sims
            ofd.Multiselect = false;
            ofd.ReadOnlyChecked = true;
            ofd.ShowHelp = ofd.ShowReadOnly = false;
            ofd.Title = "Select Exported Texture Package";
            ofd.ValidateNames = true;
            DialogResult dr = ofd.ShowDialog();
            if (DialogResult.OK.Equals(dr))
                return ofd.FileName;
            return null;
        }

        private bool findAndAdd(String name, uint type, String source)
        {
            string[] paths = {
								 SimPe.Helper.WindowsRegistry.SimsSP1Path,
								 SimPe.Helper.WindowsRegistry.SimsEP3Path,
								 SimPe.Helper.WindowsRegistry.SimsEP2Path,
								 SimPe.Helper.WindowsRegistry.SimsEP1Path,
								 SimPe.Helper.WindowsRegistry.SimsPath
							 };

            for (int i = 0; i < paths.Length; i++)
            {
                if (paths[i].Length == 0)
                    continue;

                String sims2loc = System.IO.Path.Combine(paths[i], "TSData\\Res\\3D");
                if (!System.IO.Directory.Exists(sims2loc))
                    sims2loc = System.IO.Path.Combine(paths[i], "TSData\\Res\\Sims3D");
                if (!System.IO.Directory.Exists(sims2loc))
                    continue;

                IPackageFile p = SimPe.Packages.File.LoadFromFile(System.IO.Path.Combine(sims2loc, source));
                if (p == null)
                    continue;

                IPackedFileDescriptor[] pfa = p.FindFiles(SimPe.Data.MetaData.NAME_MAP);
                if (pfa == null || pfa.Length != 1)
                    continue;

                SimPe.Plugin.Nmap nmap = new SimPe.Plugin.Nmap(null);
                nmap.ProcessData(pfa[0], p);
                pfa = nmap.FindFiles(name + "_");
                if (pfa == null || pfa.Length != 1)
                    continue;

                IPackedFileDescriptor pfd = null;
                for (int j = 0; j < p.Index.Length && pfd == null; j++)
                    if (p.Index[j].Type == type
                        && p.Index[j].Group == pfa[0].Group
                        && p.Index[j].Instance == pfa[0].Instance)
                        pfd = p.Index[j];
                if (pfd == null)
                    continue;

                IPackedFileDescriptor npfd = pfd.Clone();
                npfd.UserData = p.Read(pfd).UncompressedData;
                currentPackage.Add(npfd, true);

                return true;
            }
            return false;
        }

        private void Main()
        {
            #region Get body mesh package file name and open the package
            String bodyMeshPackage = getFilename();
            if (bodyMeshPackage == null) return;

            IPackageFile p = SimPe.Packages.File.LoadFromFile(bodyMeshPackage);
            if (p == null) return;
            #endregion

            #region Find the Property Set and get the name(s)
            IPackedFileDescriptor[] pfa = p.FindFiles(SimPe.Data.MetaData.GZPS);
            if (pfa == null || pfa.Length == 0)
            {
                MessageBox.Show("No Property Sets in package.", "Body Mesh Tool", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ArrayList al = new ArrayList();
            bool prompted = false;
            SimPe.PackedFiles.Wrapper.Cpf cpf = new SimPe.PackedFiles.Wrapper.Cpf();
            for (int i = 0; i < pfa.Length; i++)
            {
                cpf.ProcessData(pfa[i], p);
                for (int j = 0; j < cpf.Items.Length; j++)
                {
                    if (cpf.Items[j].Name.ToLower().Equals("name"))
                        al.Add(cpf.Items[j].StringValue);
                    if (al.Count > 10 && !prompted)
                    {
                        if (MessageBox.Show("More than 10 meshes found (did you pick the right package?)."
                            + "\r\nImport resources for them all?", "Body Mesh Tool", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                            != DialogResult.Yes)
                            return;
                        prompted = true;
                    }
                }
            }

            if (al.Count == 0)
            {
                MessageBox.Show("No meshes in package.", "Body Mesh Tool", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            #endregion


            #region For each mesh, find the GMDC, GMND, SHPE and CRES and add them to the current package

            foreach (String m in al)
            {
                SimPe.RemoteControl.ApplicationForm.Cursor = Cursors.WaitCursor;
                String mesh = m.Split('_')[0];

                bool success = true
                    && findAndAdd(mesh, SimPe.Data.MetaData.GMDC, "Sims03.package")
                    && findAndAdd(mesh, SimPe.Data.MetaData.GMND, "Sims04.package")
                    && findAndAdd(mesh, SimPe.Data.MetaData.SHPE, "Sims05.package")
                    && findAndAdd(mesh, SimPe.Data.MetaData.CRES, "Sims06.package")
                    ;
                SimPe.RemoteControl.ApplicationForm.Cursor = Cursors.Default;
                if (!success)
                    MessageBox.Show("Be aware that not all resources were added to the current package.", "Mesh " + m);
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
            return "PJSE\\Body Mesh Tool";
        }

        #endregion
        #endregion
    }

    public class WrapperFactory : AbstractWrapperFactory, IToolFactory
    {
        #region IToolFactory Members

        public IToolPlugin[] KnownTools
        {
            get
            {
                IToolPlugin[] tools = { new BodyMeshTool() };
                return tools;
            }
        }

        #endregion
    }

}
