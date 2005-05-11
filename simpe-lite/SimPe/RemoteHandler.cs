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


namespace SimPe
{
	/// <summary>
	/// This is the entry point for the RemoteHandlers of the main SimPE Form
	/// </summary>
	internal class RemoteHandler
	{
		Form1 form;
		internal RemoteHandler(Form1 form) 
		{
			this.form = form;

			RemoteControl.OpenPackageFkt = new SimPe.RemoteControl.OpenPackageDelegate(OpenPackage);
			RemoteControl.OpenPackedFileFkt = new SimPe.RemoteControl.OpenPackedFileDelegate(OpenPackedFile);
		}

		public bool OpenPackage(string filename)
		{
			if (!System.IO.File.Exists(filename)) return false;
			if (!form.WarnOverwriteChanges()) return false;

			form.OpenPackage(filename);
			Helper.WindowsRegistry.AddRecentFile(filename);
			return true;
		}

		public bool OpenPackedFile(Interfaces.Files.IPackedFileDescriptor pfd) 
		{
			if (pfd==null) return false;			
			if (!form.UpdateFileListSelection(pfd)) form.SelectPackedFile(pfd);
			return true;
		}
	}

	
}
