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
using System;
using SimPe.PackedFiles.UserInterface;
using SimPe.PackedFiles.Wrapper;

namespace pjse
{
	/// <summary>
	/// Summary description for BhavPrimWizProvider.
	/// </summary>
	public class BhavPrimWizProvider
	{
		public static ABhavPrimWiz ForInstruction(Instruction i)
		{
			switch(i.Opcode)
			{
				case 0x0001:
					return new BhavPrimWiz0x0001(i);
				case 0x0002:
					return new BhavPrimWiz0x0002(i);
				case 0x0008:
				case 0x000B:
				case 0x000C:
				case 0x0016:
				case 0x001E:
					break;
				default:
					break;
			}
			return new BhavPrimWizDefault(i);
		}

		public static ABhavPrimWiz Default() { return new BhavPrimWizDefault(); }

		public static string[] gPrims = {
											"Sleep","Generic Sims Call","","Find Best Interaction",
											"~(old)Grab","~(old)Drop","~(old)Change Suit","Refresh",
											"Random Number","~(old)Burn","Tutorial","Get Distance To",
											"Get Direction To","Push Interaction","Find Best Object for Function","Break Point",
											"Find Location For","Idle for Input","Remove Object Instance","Make New Character",
											"Run Functional Tree","~Show String ( UNUSED )","Turn Body Towards","Play / Stop Sound Event",
											"~UNUSED (was old relationship)","Alter Budget","Relationship","Go To Relative Position",
											"Run Tree by Name","Set Motive Change","Gosub Found Action","Set to Next",
											"Test Object Type","Find 5 Worst Motives","UI Effect","Camera Control",
											"Dialog","Test Sim Interacting With","~unused","~unused",
											"~unused","~(old)Set Balloon/Headline","Create New Object Instance","~(old)Drop Onto",
											"~(old)Animate Sim [old]","Go To Routing Slot","Snap","~(old)Reach",
											"Stop ALL Sounds","Notify the Stack Object out of Idle","Add/Change the Action String","Manage Inventory",
											"~unused (TSO)","~unused (TSO)","~unused (TSO)","~unused (TSO)",
											"~unused (TSO)","~unused","~unused","~unused","~unused","~unused","~unused","~unused",
											"~unused","~unused","~unused","~unused","~unused","~unused","~unused","~unused",
											"~unused","~unused","~unused","~unused","~unused","~unused","~unused","~unused",
											"~unused","~unused","~unused","~unused","~unused","~unused","~unused","~unused",
											"~unused","~unused","~unused","~unused","~unused","~unused","~unused","~unused",
											"~unused","~unused","~unused","~unused","~reserved","~unused","~unused","~unused",
											"~unused","Animate Object","Animate Sim","Animate Overlay",
											"Animate Stop","Change Material","Look At","Change Light",
											"Effect Stop/Start","Snap Into","Assign Locomotion Animations","Debug",
											"Reach/Put","Age","Array Operation","Message",
											"RayTrace","Change Outfit","On Timer","Cinematic","Want Satisfy","Influence"
										};
	}
}
