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

namespace pjse
{
	public class HelpHelper
	{
		private static string help(string s)
		{
			return System.IO.Path.Combine(SimPe.Helper.SimPePluginPath, "pjse.coder.plugin\\PJSE_Help\\" + s + ".htm");
		}

		public static void Help(string s)
		{
			try
			{
				System.Diagnostics.Process.Start(help(s));
			}
			catch
			{
				System.Windows.Forms.MessageBox.Show("Please install the PJSE_Help archive.");
			}
		}

		public static void Help(string s1, string s2)
		{
			string s = help(s1);
			if (!System.IO.File.Exists(s))
				s = help(s2);
			try
			{
				System.Diagnostics.Process.Start(s);
			}
			catch
			{
				System.Windows.Forms.MessageBox.Show("Please install the PJSE_Help archive.");
			}
		}

		public static void PluginHelp(string s)
		{
			Help(s, "Contents");
		}

	}
}
