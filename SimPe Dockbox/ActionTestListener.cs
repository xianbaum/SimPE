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

namespace SimPe.Plugin.Tool.Action
{
	public class TestListener  : SimPe.Interfaces.IListener, SimPe.Interfaces.ITool
	{
		#region IListener Member

		public void SelectionChangedHandler(object sender, SimPe.Events.ResourceEventArgs e)
        {
            if (!e.Empty)
            Message.Show("Listeners Notified of a new Interface", "Debug Notice", System.Windows.Forms.MessageBoxButtons.OK);
		}

		#endregion

		#region IToolPlugin Member

		public override string ToString()
		{
			return "Test Listener";
		}

		#endregion

		#region ITool Member

		public bool IsEnabled(SimPe.Interfaces.Files.IPackedFileDescriptor pfd, SimPe.Interfaces.Files.IPackageFile package)
		{
			System.Windows.Forms.MessageBox.Show("Notified old Interface");
			return false;
		}

		public SimPe.Interfaces.Plugin.IToolResult ShowDialog(ref SimPe.Interfaces.Files.IPackedFileDescriptor pfd, ref SimPe.Interfaces.Files.IPackageFile package)
		{
			return null;
		}

		#endregion		

		#region IToolExt Member
		public System.Windows.Forms.Shortcut Shortcut
		{
			get
			{
				return System.Windows.Forms.Shortcut.None;
			}
		}

		public System.Drawing.Image Icon
		{
			get
            {
                return SimPe.GetIcon.Debug;
			}
		}

		public virtual bool Visible 
		{
			get {return true;}
		}

		#endregion
	}
}
