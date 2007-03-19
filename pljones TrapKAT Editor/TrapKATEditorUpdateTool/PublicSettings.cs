/***************************************************************************
 *   Copyright (C) 2007 by Peter L Jones                                   *
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
using System.Collections.Generic;
using System.Text;
using TrapKATEditor;

namespace TrapKATEditorUpdateTool
{
    public class PublicSettings
    {
        private static TrapKATEditorUpdateTool.Properties.Settings settings = TrapKATEditorUpdateTool.Properties.Settings.Default;
        public static String AutoUpdateChoice
        { get {
            string[] s = { "UTAskMe", "UTDaily", "UTManual" };
            return L.G(s[settings.AutoUpdateChoice]);
        } }
        public static String AutoUpdateURL { get { return settings.AutoUpdateURL; } }
        public static String LastUpdateTS { get { return settings.LastUpdateTS.ToString(); } }
    }
}
