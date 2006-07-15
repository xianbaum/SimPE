/***************************************************************************
 *   Copyright (C) 2005 by Peter L Jones                                   *
 *   peter@drealm.info                                                     *
 *   Copyright (C) 2005 by Ambertation                                     *
 *   quaxi@ambertation.de                                                  *
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
using SimPe.PackedFiles.UserInterface;
using SimPe.PackedFiles.Wrapper;

namespace SimPe.Plugin
{
	/// <summary>
	/// Lists all Plugins (=FileType Wrappers) available in this Package
	/// </summary>
	/// <remarks>
	/// GetWrappers() has to return a list of all Plugins provided by this Library. 
	/// If a Plugin isn't returned, SimPe won't recognize it!
	/// </remarks>
    public class WrapperFactory : AbstractWrapperFactory, IToolFactory, IHelpFactory, ISettingsFactory
	{
		/// <summary>
		/// Returns a List of all available Plugins in this Package
		/// </summary>
		/// <returns>A List of all provided Plugins (=FileType Wrappers)</returns>
		public override SimPe.Interfaces.IWrapper[] KnownWrappers
		{
			get 
			{
				IWrapper[] wrappers = {
										   new Bcon()
										  ,new Bhav()
										  ,new Objf()
										  ,new StrWrapper()
										  ,new TPRP()
										  ,new Trcn()
										  ,new Ttab()
									  };
				return wrappers;
			}
		}


		#region IToolFactory Members

		class tool : ITool
		{
			#region ITool Members

			public bool IsEnabled(SimPe.Interfaces.Files.IPackedFileDescriptor pfd, SimPe.Interfaces.Files.IPackageFile package)
			{
				return true;
			}

			public IToolResult ShowDialog(ref SimPe.Interfaces.Files.IPackedFileDescriptor pfd, ref SimPe.Interfaces.Files.IPackageFile package)
			{
				pjse.HelpHelper.Help("Contents");
				return new SimPe.Plugin.ToolResult(false, false);
			}

			#region IToolPlugin Members

			public override string ToString()
			{
				return "PJSE\\" + pjse.Localization.GetString("menuhelp");
			}

			#endregion

			#endregion

		}


		public IToolPlugin[] KnownTools
		{
			get
			{
				ITool[] tools = {
									new tool()
								};
				return tools;
			}
		}


		#endregion

		#region IHelpFactory Members

		class helpContents : IHelp
		{
			#region IHelp Members

			public void ShowHelp(ShowHelpEventArgs e)
			{
				pjse.HelpHelper.Help("Contents");
			}

			public override string ToString()
			{
				return "PJSE";
			}

			public System.Drawing.Image Icon
			{
				get
				{
					return null;
				}
			}

			#endregion

		}


		public IHelp[] KnownHelpTopics
		{
			get
			{
				IHelp[] helpTopics = {
									new helpContents()
								};
				return helpTopics;
			}
		}


		#endregion

        #region ISettingsFactory Members

        public ISettings[] KnownSettings { get { return new ISettings[] {
            pjse.Settings.PJSE,
        }; } }

        #endregion
    }

}
