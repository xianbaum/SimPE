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
    class BodyMeshLinker : ITool
    {
        private IPackageFile currentPackage = null;
        private IPackedFileDescriptor refFilePFD = null;

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
            ofd.Title = L.Get("selectPkgMesh");
            ofd.ValidateNames = true;
            DialogResult dr = ofd.ShowDialog();
            if (DialogResult.OK.Equals(dr))
                return ofd.FileName;
            return null;
        }

        private void Main()
        {
            SimPe.Plugin.RefFile refFile = new SimPe.Plugin.RefFile();
            refFile.ProcessData(refFilePFD, currentPackage);
            if (refFile.Items[0].Type != SimPe.Data.MetaData.CRES
                || refFile.Items[1].Type != SimPe.Data.MetaData.SHPE)
            {
                MessageBox.Show(L.Get("noCRESSHPE"),
                    L.Get("pjSML"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            String meshPackage = getFilename();
            if (meshPackage == null || meshPackage.Length == 0)
                return;
            IPackageFile p = SimPe.Packages.File.LoadFromFile(meshPackage);
            if (p == null)
            {
                MessageBox.Show(L.Get("didNotOpen") + meshPackage,
                    L.Get("pjSML"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            IPackedFileDescriptor[] pfa = p.FindFiles(SimPe.Data.MetaData.CRES);
            IPackedFileDescriptor[] pfb = p.FindFiles(SimPe.Data.MetaData.SHPE);
            if (pfa == null || pfa.Length != 1 || pfb == null || pfb.Length != 1)
            {
                MessageBox.Show(L.Get("badMeshPackage") + meshPackage,
                    L.Get("pjSML"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            refFile.Items[0].Group = pfa[0].Group;
            refFile.Items[0].SubType = pfa[0].SubType;
            refFile.Items[0].Instance = pfa[0].Instance;
            refFile.Items[1].Group = pfb[0].Group;
            refFile.Items[1].SubType = pfb[0].SubType;
            refFile.Items[1].Instance = pfb[0].Instance;

            refFile.SynchronizeUserData();
            MessageBox.Show(L.Get("done") + meshPackage,
                L.Get("pjSML"), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #region ITool Members

        public bool IsEnabled(IPackedFileDescriptor pfd, IPackageFile package)
        {
            currentPackage = package;
            if (pfd != null && pfd.Type == SimPe.Data.MetaData.REF_FILE)
                refFilePFD = pfd;
            else
                refFilePFD = null;
            return package != null && refFilePFD != null;
        }

        public SimPe.Interfaces.Plugin.IToolResult ShowDialog(ref SimPe.Interfaces.Files.IPackedFileDescriptor pfd, ref SimPe.Interfaces.Files.IPackageFile package)
        {
            Main();
            return new SimPe.Plugin.ToolResult(false, false);
        }


        #region IToolPlugin Members

        public override string ToString()
        {
            return L.Get("pjBMTLink");
        }

        #endregion
        #endregion
    }
}
