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
using System.IO;

namespace TrapKATEditor
{
    public class Version
    {
        static String pluginName;
        static String timestamp;
        static String configuration;
        static Version()
        {
            //+ "Assembly location: " + this.GetType().Assembly.Location
            String version_txt = "version.txt";
            System.IO.StreamReader sr = new StreamReader(version_txt);
            String line1 = sr.ReadLine();
            String line2 = sr.ReadLine();
            sr.Close();

            String[] s = line1.Trim().Split('-');
            pluginName = s[0];
            configuration = s[1];

            timestamp = line2.Trim().Replace(' ', '0');
        }

        public static String PluginName { get { return pluginName; } }
        public static String Configuration { get { return configuration; } }
        public static String BuildTS { get { return timestamp; } }
    }
}
