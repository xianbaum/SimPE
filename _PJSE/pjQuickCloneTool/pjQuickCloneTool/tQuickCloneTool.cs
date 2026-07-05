/***************************************************************************
 *   Copyright (C) 2008 by Peter L Jones                                   *
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
using SimPe.Interfaces;
using SimPe.Interfaces.Plugin;
using SimPe.Interfaces.Files;

namespace pj
{
    class tQuickCloneTool : AbstractWrapperFactory, IToolFactory, IHelpFactory, ITool, IHelp
    {
        #region IHelp Members

        public void ShowHelp(SimPe.ShowHelpEventArgs e)
        {
            string relativePathToHelp = "pjQuickCloneTool.plugin/pjQuickCloneTool_Help";
            SimPe.RemoteControl.ShowHelp("file://" + SimPe.Helper.SimPePluginPath + "/" + relativePathToHelp + "/Contents.htm");
        }

        public override string ToString() { return L.Get("pjQuickCloneHelp"); }

        public System.Drawing.Image Icon { get { return null; } }

        #endregion

        #region ITool Members

        IToolResult ITool.ShowDialog(ref IPackedFileDescriptor pfd, ref IPackageFile package)
        {
            (new cQuickCloneTool()).Execute();
            return new SimPe.Plugin.ToolResult(false, false);
        }

        bool ITool.IsEnabled(IPackedFileDescriptor pfd, IPackageFile package)
        {
            return true;
        }

        #endregion

        #region IToolPlugin Members

        string IToolPlugin.ToString()
        {
            return L.Get("pjCQuickCloneTool");
        }

        #endregion

        #region IToolFactory Members

        public IToolPlugin[] KnownTools
        {
            get { return new IToolPlugin[] { new tQuickCloneTool() }; }
        }

        #endregion

        #region IHelpFactory Members

        public IHelp[] KnownHelpTopics
        {
            get { return new IHelp[] { new tQuickCloneTool() }; }
        }

        #endregion

    }
}
