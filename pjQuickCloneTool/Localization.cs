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
using System.Resources;

namespace pj
{
    public class L
    {
        private static ResourceManager resource = null;

        static L() { resource = new ResourceManager(typeof(L)); }

        public static string Get(string name, params string[] args)
        {
            string res = resource.GetString(name);
#if DEBUG
            if (res == null) res = "<<" + name + ">>";
#else
            if (res == null) res = name;
#endif
            for (int i = 1; (i - 1) < args.Length; i++)
            {
                if (res.Contains("{" + i + "}"))
                    res = res.Replace("{" + i + "}", args[(i - 1)]);
#if DEBUG
                else
                    res += ", {" + i + ": " + args[(i - 1)] + "}";
#endif
            }

            return res;
        }
    }
}
