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
using System.Windows.Forms;

namespace SimPe.Plugin.Tool.Action
{
	/// <summary>
	/// Rewrites a custom Fence's XFNC and String Set resources so the Fence works in the
	/// original (base) game.
	/// </summary>
	/// <remarks>
	/// Hidden-Mode developer tool ("Fix Fence"). A base-game Fence expects its XFNC (and the
	/// linked String Set) to live in a GUID-keyed group; this action performs that remap. It is
	/// deliberately gated behind Hidden Mode (see <see cref="WorkshopToolFactory"/>) because,
	/// as the warning explains, it is a nasty hack that can cause conflicts in the Game.
	/// </remarks>
	public class ActionEnableFenceInOriginalGame : SimPe.Interfaces.IToolAction
	{
		/// <summary>Group a base-game Fence's resources have to live in (0x4C8CC5C0).</summary>
		const uint OriginalGameFenceGroup = 0x4C8CC5C0;

		#region IToolAction Member

		public virtual bool ChangeEnabledStateEventHandler(object sender, SimPe.Events.ResourceEventArgs es)
		{
			return true;
		}

		/// <summary>
		/// The real enablement test: this tool is only meaningful when the loaded Package
		/// actually contains a Fence (XFNC) resource.
		/// </summary>
		bool RealChangeEnabledStateEventHandler(object sender, SimPe.Events.ResourceEventArgs es)
		{
			if (!es.Loaded) return false;
			return es.LoadedPackage.Package.FindFiles(Data.MetaData.XFNC).Length != 0;
		}

		public void ExecuteEventHandler(object sender, SimPe.Events.ResourceEventArgs e)
		{
			if (!RealChangeEnabledStateEventHandler(null, e))
			{
				MessageBox.Show(
					Localization.GetString("This is not an appropriate context in which to use this tool"),
					Localization.GetString(ToString()));
				return;
			}

			if (Message.Show(Localization.GetString("Fix_Fence_Warning"), Localization.GetString("Warning"), MessageBoxButtons.YesNo) == DialogResult.No)
				return;

			try
			{
				foreach (SimPe.Interfaces.Files.IPackedFileDescriptor pfd in e.LoadedPackage.Package.FindFiles(Data.MetaData.XFNC))
				{
					SimPe.PackedFiles.Wrapper.Cpf cpf = new SimPe.PackedFiles.Wrapper.Cpf();
					cpf.ProcessData(pfd, e.LoadedPackage.Package);

					uint guid = cpf.GetSaveItem("guid").UIntegerValue;

					//locate the connected String Set before we overwrite the CPF's pointers to it
					SimPe.Interfaces.Files.IPackedFileDescriptor strpfd = e.LoadedPackage.Package.FindFile(
						cpf.GetSaveItem("stringsetrestypeid").UIntegerValue,
						0,
						cpf.GetSaveItem("stringsetgroupid").UIntegerValue,
						cpf.GetSaveItem("stringsetid").UIntegerValue);

					//re-home the Fence (and its String Set) into the GUID-keyed base-game group
					cpf.GetSaveItem("resourcegroupid").UIntegerValue = OriginalGameFenceGroup;
					cpf.GetSaveItem("resourceid").UIntegerValue = guid;
					cpf.GetSaveItem("stringsetgroupid").UIntegerValue = guid;
					cpf.SynchronizeUserData(true, true);

					cpf.FileDescriptor.Instance = guid;
					cpf.FileDescriptor.Group = OriginalGameFenceGroup;

					if (strpfd != null) strpfd.Group = guid;
				}
			}
			catch (Exception ex)
			{
				Helper.ExceptionMessage(ex);
			}
		}

		#endregion

		#region IToolPlugin Member
		public override string ToString()
		{
			return "Fix Fence";
		}
		#endregion

		#region IToolExt Member
		public System.Windows.Forms.Shortcut Shortcut
		{
			get { return System.Windows.Forms.Shortcut.None; }
		}

		public System.Drawing.Image Icon
		{
			get { return null; }
		}

		public virtual bool Visible
		{
			get { return true; }
		}
		#endregion
	}
}
