/***************************************************************************
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
	public class WrapperFactory : SimPe.Interfaces.Plugin.AbstractWrapperFactory, SimPe.Interfaces.Plugin.IToolFactory
	{

		/// <summary>
		/// Bhav Wizard
		/// </summary>
		static BhavOpCodeWiz bw = null;

		/// <summary>
		/// Returns a handle to the Bhav Wizard Window
		/// </summary>
		internal static BhavOpCodeWiz BhavWizardForm
		{
			get 
			{
				if (bw==null) bw = new BhavOpCodeWiz();
				return bw;
			}
		}

		static IProviderRegistry provider;
		public static IProviderRegistry Provider
		{
			get { return provider; }
		}

		/// <summary>
		/// Returns or sets the Provider this Plugin can use
		/// </summary>
		public override IProviderRegistry LinkedProvider
		{
			get { return base.LinkedProvider; }
			set { 
				base.LinkedProvider = value; 
				provider = value;
			}
		}

		#region AbstractWrapperFactory Member
		/// <summary>
		/// Returns a List of all available Plugins in this Package
		/// </summary>
		/// <returns>A List of all provided Plugins (=FileType Wrappers)</returns>
		public override SimPe.Interfaces.IWrapper[] KnownWrappers
		{
			get 
			{
				IWrapper[] wrappers = {
										   new Bhav(LinkedProvider.OpcodeProvider)
										  ,new Ttab(LinkedProvider.OpcodeProvider)
#if INPROGRESS || DEBUG
										  ,new Bcon()
										  ,new Objf(LinkedProvider.OpcodeProvider)
										  ,new PjseObjd(LinkedProvider.OpcodeProvider)
										  ,new Glob()
										  ,new Str()
										  ,new Trcn()
#else
#endif
									  };
				return wrappers;
			}
		}

		#endregion

		#region IToolFactory Member

		public IToolPlugin[] KnownTools
		{
			get
			{
				IToolPlugin[] tools = {
									new ImportSemiTool(this.LinkedRegistry, this.LinkedProvider)
								};
				return tools;
			}
		}

		#endregion
	}
}
