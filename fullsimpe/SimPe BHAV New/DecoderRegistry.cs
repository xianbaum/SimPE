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
using System.Collections;
using SimPe.Interfaces.Plugin;

namespace SimPe.Plugin
{
	/// <summary>
	/// Enumerates all availabel BHAV Decoders
	/// </summary>
	public class DecoderRegistry
	{
		/// <summary>
		/// This delegate is used to Updated the Instructions in the "Plugin View"
		/// </summary>
		public delegate void ForceUpdate(Instruction inst);

		/// <summary>
		/// Create a new Instance and load all available Decoder Plugins
		/// </summary>
		public DecoderRegistry()
		{
			decoders = new IBhavInstructionDecoder[0];
			LoadDecoders();
		}

		/// <summary>
		/// Returns the Folder where Decodeder Plugins are stored
		/// </summary>
		public string DecoderFolder
		{
			get 
			{
				return System.IO.Path.Combine(Helper.SimPePath, "Plugins\\Decoders\\");
			}
		}

		/// <summary>
		/// Reads all Decoder Plugins from a given Subdirectory
		/// </summary>
		protected void LoadDecoders()
		{
			if (!System.IO.Directory.Exists(DecoderFolder)) return;

			string[] plugins = System.IO.Directory.GetFiles(DecoderFolder, "*.decoder.dll");
			foreach (string plugin in plugins) 
			{
				object[] objs = SimPe.LoadFileWrappers.LoadPlugins(plugin, typeof(IBhavInstructionDecoder));
				foreach (object o in objs) 
				{
					IBhavInstructionDecoder bid = (IBhavInstructionDecoder)o;
					Register(bid);
				}
			}
		}

		IBhavInstructionDecoder[] decoders;

		/// <summary>
		/// Returns a List of all kown Decoders
		/// </summary>
		public IBhavInstructionDecoder[] Decoders
		{
			get {return decoders;}
			set {decoders = value;}
		}

		/// <summary>
		/// Returns a List of all Decoders that can handle the given Opcode
		/// </summary>
		/// <param name="opcode"></param>
		/// <returns>ArrayList of IBhavInstructionDecoder Items</returns>
		public ArrayList FindDecoders(IOpcodeDescriptor opcode) 
		{ 
			ArrayList ret = new ArrayList();
			ArrayList fallback = new ArrayList();
			foreach(IBhavInstructionDecoder bid in Decoders) 
			{
				//This Decoder is general purpose, so add it
				if (bid.Opcodes.Length==0) 
				{
					fallback.Add(bid);
				} 
				else 
				{
					foreach (IOpcodeDescriptor od in bid.Opcodes) 
					{
						if (opcode.Equals(od))
						{
							///Make sure, that Items with any Group are last!
							if ((ret.Count==0) || (opcode.SemiGlobalGroup==0)) ret.Add(bid);
							else ret.Insert(0, bid);
						}
					}
				}
			}

			//Add a Fallback Decoder if needed
			for (int i=0; i<fallback.Count; i++) ret.Add(fallback[i]);
			return ret;
		}

		/// <summary>
		/// Register a new Decoder
		/// </summary>
		/// <param name="bid">The new Decoder</param>
		public void Register(IBhavInstructionDecoder bid) 
		{
			decoders = (IBhavInstructionDecoder[])Helper.Add(decoders, bid, typeof(IBhavInstructionDecoder));
		}
	}
}
