/***************************************************************************
 *   Copyright (C) 2006 by Peter L Jones                                   *
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
using SimPe.Interfaces;
using SimPe.Interfaces.Plugin;
using SimPe.Interfaces.Scenegraph;
using System.Diagnostics;
using System.Windows.Forms;

namespace pjse.packagetools
{
    class CpfImportExport
    {
        public static bool IsEnabled(SimPe.Interfaces.Files.IPackedFileDescriptor pfd, SimPe.Interfaces.Files.IPackageFile package)
        {
            if (pfd == null) return false;

            if (SimPe.FileTable.WrapperRegistry == null) return false;

            AbstractWrapper wrapper = (AbstractWrapper)SimPe.FileTable.WrapperRegistry.FindHandler(pfd.Type);
            if (wrapper == null) return false;

            Debug.WriteLine("[TEST] Wrapper type: "
                + "[" + SimPe.Data.MetaData.FindTypeAlias(pfd.Type).shortname + "] "
                + wrapper.GetType().FullName);

            return (wrapper.GetType().FullName.Equals("SimPe.PackedFiles.Wrapper.Cpf"));
        }
    }


    public class CpfExport : ITool
    {
        #region ITool Members

        public bool IsEnabled(SimPe.Interfaces.Files.IPackedFileDescriptor pfd, SimPe.Interfaces.Files.IPackageFile package)
        {
            return CpfImportExport.IsEnabled(pfd, package);
        }

        public SimPe.Interfaces.Plugin.IToolResult ShowDialog(ref SimPe.Interfaces.Files.IPackedFileDescriptor pfd, ref SimPe.Interfaces.Files.IPackageFile package)
        {
            if (pfd == null)
                throw new ArgumentException();

            if (SimPe.FileTable.WrapperRegistry == null)
                throw new ArgumentException();

            AbstractWrapper wrapper = (AbstractWrapper)SimPe.FileTable.WrapperRegistry.FindHandler(pfd.Type);
            if (wrapper == null)
                throw new ArgumentException();

            if (!(wrapper is SimPe.PackedFiles.Wrapper.Cpf))
                throw new ArgumentException();

            Debug.WriteLine("[FIRE] Wrapper type: "
                + "[" + SimPe.Data.MetaData.FindTypeAlias(pfd.Type).shortname + "] "
                + wrapper.GetType().FullName);

            SimPe.PackedFiles.Wrapper.Cpf cpf = (SimPe.PackedFiles.Wrapper.Cpf)wrapper;
            cpf.ProcessData(pfd, package);

            Clipboard.Clear();
            String s = "";
            foreach (SimPe.PackedFiles.Wrapper.CpfItem item in cpf.Items)
                s += item.ToString() + "\r\n";
            Clipboard.SetText(s);

            MessageBox.Show(
                pfd.TypeName + ": " + wrapper.GetType().FullName
                + "\r\nCopied to clipboard"
                );

            return new SimPe.Plugin.ToolResult(false, false);
        }

        #endregion

        #region IToolPlugin Members

        public override string ToString()
        {
            return "PJSE\\" + pjse.Localization.GetString("cei_CpfExport");
        }

        #endregion

    }


    public class CpfImport : ITool
    {
        #region ITool Members

        public bool IsEnabled(SimPe.Interfaces.Files.IPackedFileDescriptor pfd, SimPe.Interfaces.Files.IPackageFile package)
        {
            return CpfImportExport.IsEnabled(pfd, package);
        }

        public SimPe.Interfaces.Plugin.IToolResult ShowDialog(ref SimPe.Interfaces.Files.IPackedFileDescriptor pfd, ref SimPe.Interfaces.Files.IPackageFile package)
        {
            if (pfd == null)
                throw new ArgumentException();

            if (SimPe.FileTable.WrapperRegistry == null)
                throw new ArgumentException();

            AbstractWrapper wrapper = (AbstractWrapper)SimPe.FileTable.WrapperRegistry.FindHandler(pfd.Type);
            if (wrapper == null)
                throw new ArgumentException();

            if (!(wrapper is SimPe.PackedFiles.Wrapper.Cpf))
                throw new ArgumentException();

            Debug.WriteLine("[FIRE] Wrapper type: "
                + "[" + SimPe.Data.MetaData.FindTypeAlias(pfd.Type).shortname + "] "
                + wrapper.GetType().FullName);

            SimPe.PackedFiles.Wrapper.Cpf cpf = (SimPe.PackedFiles.Wrapper.Cpf)wrapper;
            cpf.ProcessData(pfd, package);

            System.IO.StringReader sr = new System.IO.StringReader(Clipboard.GetText());

            cpf.Items = new SimPe.PackedFiles.Wrapper.CpfItem[0];
            String s = sr.ReadLine();
            int i = 0;
            while (s != null)
            {
                try
                {
                    String[] parse = s.Split('=');
                    if (parse.Length > 2) continue;
                    parse[0] = parse[0].Trim();
                    parse[1] = parse[1].Trim();
                    if (!parse[0].EndsWith(")")) continue;
                    String[] parse2 = parse[0].Split('(');
                    if (parse2.Length > 2) continue;
                    parse2[1] = parse2[1].Trim();
                    if (!parse2[1].EndsWith(")")) continue;

                    SimPe.PackedFiles.Wrapper.CpfItem cpfItem = new SimPe.PackedFiles.Wrapper.CpfItem();
                    cpfItem.Name = parse2[0].Trim();
                    switch (parse2[1])
                    {
                        case "dtBoolean)": cpfItem.BooleanValue = Convert.ToBoolean(parse[1]); break;
                        case "dtInteger)": cpfItem.IntegerValue = Convert.ToInt32(parse[1].Remove(0, 2), 16); break;
                        case "dtSingle)": cpfItem.SingleValue = Convert.ToSingle(parse[1]); break;
                        case "dtString)": cpfItem.StringValue = parse[1]; break;
                        case "dtUInteger)": cpfItem.UIntegerValue = Convert.ToUInt32(parse[1].Remove(0, 2), 16); break;
                        default: continue;
                    }
                    cpf.AddItem(cpfItem);
                }
                finally
                {
                    i++;
                    s = sr.ReadLine();
                }
            }
            cpf.SynchronizeUserData();
            MessageBox.Show(
                pfd.TypeName + ": " + wrapper.GetType().FullName
                + "\r\nPasted " + i + " rows from clipboard"
                );

            return new SimPe.Plugin.ToolResult(false, false);
        }

        #endregion

        #region IToolPlugin Members

        public override string ToString()
        {
            return "PJSE\\" + pjse.Localization.GetString("cei_CpfImport");
        }

        #endregion

    }
}
namespace SimPe.Plugin
{
    public class WrapperFactory : AbstractWrapperFactory, IToolFactory
    {
        #region IToolFactory Members

        public SimPe.Interfaces.IToolPlugin[] KnownTools
        {
            get
            {
                IToolPlugin[] tools = {
										  new pjse.packagetools.CpfExport()
										  , new pjse.packagetools.CpfImport()
									  };
                return tools;
            }
        }

        #endregion
    }

}
