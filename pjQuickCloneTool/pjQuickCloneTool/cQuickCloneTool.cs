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
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using SimPe.Interfaces;
using SimPe.Interfaces.Plugin;
using SimPe.Interfaces.Scenegraph;
using SimPe.Interfaces.Files;

namespace pj
{
    public class cQuickCloneTool : ITool
    {
        #region ITool Members

        IToolResult ITool.ShowDialog(ref IPackedFileDescriptor pfd, ref IPackageFile package)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        bool ITool.IsEnabled(IPackedFileDescriptor pfd, IPackageFile package)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion

        #region IToolPlugin Members

        string IToolPlugin.ToString()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion
    }
}
