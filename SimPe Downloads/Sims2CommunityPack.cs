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
using System.IO;
using System.Collections;
using SimPe.Interfaces.Plugin;
using SimPe.Interfaces.Plugin.Internal;
using SimPe.Interfaces;
using SimPe.Interfaces.Files;
using System.Xml;
using System.Media;

namespace SimPe.Packages
{
	/// <summary>
	/// This class contains static Methods you can use to Handle S2CP Files
	/// </summary>
	public class Sims2CommunityPack
	{
		/// <summary>
		/// Defines the Default Compression that is used
		/// </summary>
        public const CompressionStrength DEFAULT_COMPRESSION_STRENGTH = CompressionStrength.None;

		/// <summary>
		/// Enumerates the Strength of the Used Compression
		/// </summary>
		public enum CompressionStrength:int 
		{
			/// <summary>
			/// Don't use any kind of compression
			/// </summary>
			None = 0x00,
			/// <summary>
			/// Fastest Compression (biggest Size)
			/// </summary>
			Fastest = 0x01,
			/// <summary>
			/// 
			/// </summary>
			Faster = 0x02,
			/// <summary>
			/// 
			/// </summary>
			Fast = 0x03,
			/// <summary>
			/// Default Compression Strength
			/// </summary>
			Default = 0x04,
			/// <summary>
			/// 
			/// </summary>
			Slow = 0x05,
			/// <summary>
			/// 
			/// </summary>
			Slower = 0x07,
			/// <summary>
			/// Slowest compression (smallest File)
			/// </summary>
			Slowest = 0x09
		}

		/// <summary>
		/// Decompress <paramref name="instream">input</paramref> writing 
		/// decompressed data to <paramref name="outstream">output stream</paramref>
		/// </summary>
		/// <param name="instream">Compressed Data</param>
		/// <param name="outstream">Uncompressed Data</param>
		/// <remarks>Overwritten, because i needed the <paramref name="outstream">output stream</paramref> 
		/// to Seek to the Beginning</remarks>
		protected static void Decompress(Stream instream, Stream outstream) 
		{
            ICSharpCode.SharpZipLib.BZip2.BZip2.Decompress(instream, outstream, false); // newer ICSharpCode.SharpZipLib requires three parameters, param three means is the stream already in the app
            // ICSharpCode.SharpZipLib.BZip2.BZip2.Decompress(instream, outstream); // older ICSharpCode.SharpZipLib requires two parameters
			outstream.Seek(0, System.IO.SeekOrigin.Begin);
		}
		
		/// <summary>
		/// Compress <paramref name="instream">input stream</paramref> sending 
		/// result to <paramref name="outputstream">output stream</paramref>
		/// </summary>
		/// <param name="outstream"></param>
		/// <param name="instream"></param>
		/// <param name="strength">Strngth of the Compression</param>
		/// <remarks>I had to change the Origial Compression Methode in a way that it woun't close 
		/// the <paramref name="instream">input stream</paramref> and that it seeks to the Beginning 
		/// of it on startup</remarks>
		protected static void Compress(Stream instream, Stream outstream, CompressionStrength strength) 
		{			
			System.IO.Stream bos = outstream;
			System.IO.Stream bis = instream;
			bis.Seek(0, System.IO.SeekOrigin.Begin);
			int ch = bis.ReadByte();
			ICSharpCode.SharpZipLib.BZip2.BZip2OutputStream bzos = new ICSharpCode.SharpZipLib.BZip2.BZip2OutputStream(bos, (int)strength);
			while (ch != -1) 
			{
				bzos.WriteByte((byte)ch);
				ch = bis.ReadByte();
			}
			bzos.Close();
		}	

		/// <summary>
		/// Create a Sims2CommunityPack File
		/// </summary>
		/// <param name="file">The Descriptors of the Package to inlcude</param>
		/// <param name="extension">true if you want to add the Community Extension, otherwise 
		/// a normal Sims2Pack File will be generated</param>
		/// <returns>The MemoryStream containing the S2CP File</returns>
		public static MemoryStream Create(S2CPDescriptor file, bool extension) 
		{
			S2CPDescriptor[] ms = new S2CPDescriptor[1];
			ms[0] = file;
			return Create(ms, extension);
		}

		/// <summary>
		/// Create a Sims2CommunityPack File
		/// </summary>
		/// <param name="files">List of Descriptors for all Packages to include</param>
		/// <param name="extension">true if you want to add the Community Extension, otherwise 
		/// a normal Sims2Pack File will be generated</param>
		/// <returns>The MemoryStream containing the S2CP File</returns>
		public static MemoryStream Create(S2CPDescriptor[] files, bool extension) 
		{
			string xml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n";
            xml += "<Sims2Package type=\"" + getfiletype(files) + "\">\n";
            xml += "  <ArchiveVersion>1</ArchiveVersion>\n";
            xml += "  <GameVersion>2141707388.153.1</GameVersion>\n";
            xml += "  <CodeVersion>17</CodeVersion>\n";
			int offset = 0;
			MemoryStream[] streams = new MemoryStream[files.Length];
			for (int i=0; i<files.Length; i++)
            {
                if (files[i].Crc == "0") files[i].Crc = System.Guid.NewGuid().ToString().Replace("-", "");
                if (files[i].Type == "Object") files[i].Type = gettype(files[i].Package);
				if (files[i].Compressed != CompressionStrength.None) 
				{
					MemoryStream cms = new MemoryStream();
					Compress(files[i].Package.Build(), cms, files[i].Compressed);
					streams[i] = new MemoryStream(cms.ToArray());
				} 
				else 
				{
					streams[i] = files[i].Package.Build();
				}
				MemoryStream m = streams[i];
                xml += "  <PackagedFile>\n";
                if (files[i].Name == "")
                    xml += "    <Name><![CDATA[]]></Name>\n";
                else
                    xml += "    <Name>" + files[i].Name.Replace("&", ";amp;") + "</Name>\n";
                xml += "    <Length>" + m.Length.ToString() + "</Length>\n";
                if (files[i].Type == "NPC Mod")
                    xml += "    <Type>Person</Type>\n";
                else
                    xml += "    <Type>" + files[i].Type + "</Type>\n";
                xml += "    <Identifiers>\n";
                xml += "      <Crc>" + files[i].Crc + "</Crc>\n";
                if (files[i].Guid == "" || files[i].Guid == null)
                    xml += "      <Guid>" + files[i].Crc + "</Guid>\n";
                else
                    xml += "      <Guid>" + files[i].Guid + "</Guid>\n";
                xml += "      <Version>0</Version>\n";
                xml += "    </Identifiers>\n";
                xml += "    <Offset>" + offset.ToString() + "</Offset>\n";
                if (files[i].Title == "")
                    xml += "    <DisplayName><![CDATA[]]></DisplayName>\n";
                else
                    xml += "    <DisplayName>" + files[i].Title.Replace("&", ";amp;") + "</DisplayName>\n";
                if (files[i].Description == "")
                    xml += "    <Description><![CDATA[]]></Description>\n";
                else
                    xml += "    <Description>" + files[i].Description.Replace("&", ";amp;") + "</Description>\n";
				if (extension)
				{
                    xml += "    <Sims2CommuniytExt>\n";
                    xml += "      <Compressed>" + ((int)files[i].Compressed).ToString() + "</Compressed>\n";
                    xml += "      <Author>\n";
                    if (files[i].Author == "")
                        xml += "        <PersonalName><![CDATA[]]></PersonalName>\n";
                    else
                        xml += "        <PersonalName>" + files[i].Author.Replace("&", ";amp;") + "</PersonalName>\n";
                    if (files[i].Contact == "")
                        xml += "        <Contact><![CDATA[]]></Contact>\n";
                    else
                        xml += "        <Contact>" + files[i].Contact.Replace("&", ";amp;") + "</Contact>\n";
                    xml += "      </Author>\n";
                    if (files[i].Title == "")
                        xml += "      <Title><![CDATA[]]></Title>\n";
                    else
                        xml += "      <Title>" + files[i].Title.Replace("&", ";amp;") + "</Title>\n";
                    if (files[i].ObjectVersion != "0") xml += "      <ObjectVersion>" + files[i].ObjectVersion + "</ObjectVersion>\n";
                    if (files[i].Guid == "")
                        xml += "      <GlobalGUID><![CDATA[]]></GlobalGUID>\n";
                    else
                        xml += "      <GlobalGUID>" + files[i].Guid + "</GlobalGUID>\n";
                    xml += "      <Dependency>\n";
					foreach (S2CPDescriptorBase s2cpd in files[i].Dependency) 
					{
                        xml += "        <DependantFile>\n";// swap PackagedFile for DependantFile so default packager don't trip up
                        if (s2cpd.Name == "")
                            xml += "          <Depend><![CDATA[]]></Depend>\n";//swap Name for Depend so default packager don't trip up
                        else
                            xml += "          <Depend>" + s2cpd.Name.Replace("&", ";amp;") + "</Depend>\n";
                        if (s2cpd.ObjectVersion != "0") xml += "          <ObjectVersion>" + s2cpd.ObjectVersion + "</ObjectVersion>\n";
                        if (s2cpd.Guid == "")
                            xml += "          <GlobalGUID><![CDATA[]]></GlobalGUID>\n";
                        else
                            xml += "          <GlobalGUID>" + s2cpd.Guid + "</GlobalGUID>\n";
                        if (s2cpd.Optional) xml += "          <Optional />\n";
                        xml += "        </DependantFile>\n";
					}
                    xml += "      </Dependency>\n";
                    xml += "    </Sims2CommuniytExt>\n";
				}
                xml += "  </PackagedFile>\n";
                offset += (int)m.Length;
			}
			xml += "</Sims2Package>";
			MemoryStream ms = new MemoryStream();
			BinaryWriter bw = new BinaryWriter(ms);
			bw.Write(Helper.ToBytes("Sims2 Packager 1.0"));
            bw.Write((int)(22 + xml.Length));
			bw.Write(Helper.ToBytes(xml));
            if (Helper.WindowsRegistry.HiddenMode)
            {
                FileStream fs = new FileStream(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "OutputXML.txt"), FileMode.Create);
                try { fs.Write(ms.ToArray(), 0, (int)ms.Length); }
                finally { fs.Close(); fs.Dispose(); fs = null; }
            }
			for (int i=0; i<streams.Length; i++)
			{
				MemoryStream m = streams[i];
				bw.Write(m.ToArray());
			}
            return ms;
		}

        public static string getfiletype(S2CPDescriptor[] files)
        {
            foreach (S2CPDescriptor fil in files)
            {
                if (fil.Package.FindFiles(0x484F5553).Length > 0) return "Lot";
            }
            foreach (S2CPDescriptor fil in files)
            {
                if (fil.Package.FindFiles(0xAC598EAC).Length > 0 && fil.Package.FindFiles(0xEBCF3E27).Length > 0 && fil.Package.FindFiles(0x4F424A44).Length == 0) return "Sim";
            }
            return "Object";
        }

        public static string gettype(GeneratableFile fl)
        {
            if (fl.FindFiles(0xAC598EAC).Length > 0 && fl.FindFiles(0xEBCF3E27).Length > 0 && fl.FindFiles(0x4F424A44).Length > 0) return "Person";//Age Data + Property Set + Object Data- Person
            if (fl.FindFiles(0xAC598EAC).Length > 0 && fl.FindFiles(0xEBCF3E27).Length > 0) return "Sim";//Age Data + Property Set - Sim
            if (fl.FindFiles(0x484F5553).Length > 0) return "Lot";//HOUS
            if (fl.FindFiles(0xCDB8BDC4).Length > 0) return "Family";//Single Sim Memory
            return "Object";
        }


		/// <summary>
		/// Parse the Dependency Node in an XML
		/// </summary>
		/// <param name="dep">The Dependency Node</param>
		/// <returns>The Descriptor of the DependencyPackage (the package value is NULL)</returns>
		/// <remarks>The Descriptor won't contain the package Data. Yo you have to find the Matching Package 
		/// by the returned GUID and Name</remarks>
        protected static S2CPDescriptorBase ParesDependingPackedFile(XmlNode dep)
        {
            S2CPDescriptorBase s2cp = new S2CPDescriptorBase(null);
            s2cp.Optional = false;
            foreach (XmlNode subnode in dep)
            {
                switch (subnode.LocalName)
                {
                    case "Name"://keep for backward compat
                        {
                            s2cp.Name = subnode.InnerText.Replace(";amp;", "&");
                            break;
                        }
                    case "Depend"://new Name
                        {
                            s2cp.Name = subnode.InnerText.Replace(";amp;", "&");
                            break;
                        }
                    case "ObjectVersion":
                        {
                            s2cp.ObjectVersion = subnode.InnerText;
                            break;
                        }
                    case "GlobalGUID":
                        {
                            s2cp.Guid = subnode.InnerText;
                            break;
                        }
                    case "Optional":
                        {
                            s2cp.Optional = true;
                            break;
                        }
                } //switch
            } //foreach
            return s2cp;
        }

		/// <summary>
		/// Reads a package from the Stream
		/// </summary>
		/// <param name="reader">The S2CP Stream</param>
		/// <param name="offset">The starting offset</param>
		/// <param name="length">The Length of the Package</param>
		/// <param name="desc">Package Description</param>
		/// <returns>The Package</returns>
		protected static SimPe.Packages.GeneratableFile LoadPackage(BinaryReader reader, int offset, int length, SimPe.Packages.S2CPDescriptor desc)
		{
			reader.BaseStream.Seek(offset, System.IO.SeekOrigin.Begin);
			MemoryStream ms = new MemoryStream(reader.ReadBytes(length));

			if (desc.Compressed == CompressionStrength.None) 
			{
				SimPe.Packages.GeneratableFile pkg = GeneratableFile.LoadFromStream(new BinaryReader(ms));
				return pkg;
			} 
			else 
			{
				MemoryStream unc = new MemoryStream();
				Decompress(ms, unc);
				SimPe.Packages.GeneratableFile pkg = GeneratableFile.LoadFromStream(new BinaryReader(unc));
				pkg.FileName = desc.Name;
				return pkg;
			}
		}

		/// <summary>
		/// Parse the packedFile Node in an XML
		/// </summary>
		/// <param name="reader">The source Stream of the S2CP File</param>
		/// <param name="pfile">The PackedFile Node</param>
		/// <param name="offset">The Ofset for the package Data</param>
		/// <returns>The Descriptor of the PackedFile</returns>
		/// <remarks>The GUIDs and the Names of the Descriptors are the ones stored in the
		/// XML Data not the ones from the Package. So you have to <seealso cref="SimPe.Packages.S2CPDescriptorBase.Valid"/><see cref="SimPe.Packages.S2CPDescriptorBase.Valid"/> the Content with the Package.</remarks>
        protected static S2CPDescriptor ParesPackedFile(BinaryReader reader, XmlNode pfile, int offset)
        {
            S2CPDescriptor s2cp = new S2CPDescriptor(null, "", "", "", "", Sims2CommunityPack.CompressionStrength.None, null, true);
            int pkglen = 0;
            int pkgoffset = 0;
            foreach (XmlNode mainnode in pfile)
            {
                switch (mainnode.LocalName)
                {
                    case "Name":
                        {
                            s2cp.Name = mainnode.InnerText.Replace(";amp;", "&");
                            break;
                        }
                    case "Type":
                        {
                            s2cp.Type = mainnode.InnerText;
                            break;
                        }
                    case "Identifiers":
                        {
                            foreach (XmlNode subnode in mainnode)
                            {
                                switch (subnode.LocalName)
                                {
                                    case "Crc":
                                        {
                                            s2cp.Crc = subnode.InnerText;
                                            break;
                                        }
                                    case "Guid":
                                        {
                                            s2cp.Guid = subnode.InnerText;
                                            break;
                                        }
                                    case "Version":
                                        {
                                            s2cp.ObjectVersion = subnode.InnerText;
                                            break;
                                        }
                                }
                            }
                            break;
                        }
                    case "DisplayName":
                        {
                            s2cp.Title = mainnode.InnerText.Replace(";amp;", "&");
                            break;
                        }
                    case "Description":
                        {
                            s2cp.Description = mainnode.InnerText.Replace(";amp;", "&");
                            break;
                        }
                    case "Length":
                        {
                            pkglen = Convert.ToInt32(mainnode.InnerText);
                            break;
                        }
                    case "Offset":
                        {
                            pkgoffset = Convert.ToInt32(mainnode.InnerText);
                            break;
                        }
                    case "Sims2CommuniytExt":
                        {
                            foreach (XmlNode subnode in mainnode)
                            {
                                switch (subnode.LocalName)
                                {
                                    case "Author":
                                        {
                                            foreach (XmlNode authnode in subnode)
                                            {
                                                switch (authnode.LocalName)
                                                {
                                                    case "PersonalName":
                                                        {
                                                            s2cp.Author = authnode.InnerText.Replace(";amp;", "&");
                                                            break;
                                                        }
                                                    case "Contact":
                                                        {
                                                            s2cp.Contact = authnode.InnerText.Replace(";amp;", "&");
                                                            break;
                                                        }
                                                }//switch
                                            }
                                            break;
                                        }
                                    case "Title":
                                        {
                                            s2cp.Title = subnode.InnerText.Replace(";amp;", "&");
                                            break;
                                        }
                                    case "ObjectVersion":
                                        {
                                            s2cp.ObjectVersion = subnode.InnerText;
                                            break;
                                        }
                                    case "GlobalGUID":
                                        {
                                            s2cp.Guid = subnode.InnerText;
                                            break;
                                        }
                                    case "Compressed":
                                        {
                                            s2cp.Compressed = (CompressionStrength)Convert.ToInt32(subnode.InnerText);
                                            break;
                                        }
                                    case "Dependency":
                                        {
                                            ArrayList al = new ArrayList();
                                            foreach (XmlNode dep in subnode)
                                            {
                                                if (dep.LocalName == "PackagedFile" || dep.LocalName == "DependantFile")
                                                {
                                                    al.Add(ParesDependingPackedFile(dep));
                                                }
                                            }
                                            S2CPDescriptorBase[] deps = new S2CPDescriptorBase[al.Count];
                                            al.CopyTo(deps);
                                            s2cp.Dependency = deps;
                                            break;
                                        }
                                }
                            }
                            break;
                        }
                } //switch
            } //foreach
            s2cp.Package = LoadPackage(reader, offset + pkgoffset, pkglen, s2cp);
            return s2cp;
        }

		/// <summary>
		/// Opens a Sim2Pack File
		/// </summary>
		/// <param name="filename">Filename of the S2CP File</param>
		/// <returns>List of all Package descriptors</returns>
		/// <remarks>The GUIDs/Names included in the S2CPDescriptor are the ones found in the Xml Description, 
		/// you need to check them with the GUIDs stored in the packges itself to 
		/// <see cref="SimPe.Packages.S2CPDescriptorBase.Valid"/>
		/// <seealso cref="SimPe.Packages.S2CPDescriptorBase.Valid"/> the Content</remarks>
		public static S2CPDescriptor[] Open(string filename) 
		{
			System.IO.BinaryReader br = new System.IO.BinaryReader(System.IO.File.OpenRead(filename));
			return Open(br);
		}

		/// <summary>
		/// Opens a Sim2Pack File
		/// </summary>
		/// <param name="reader">The Stream containing the File</param>
		/// <returns>List of all Package descriptors</returns>
		/// <remarks>The GUIDs/Names included in the S2CPDescriptor are the ones found in the Xml Description, 
		/// you need to check them with the GUIDs stored in the packges itself to 
		/// <see cref="SimPe.Packages.S2CPDescriptorBase.Valid"/>
		/// <seealso cref="SimPe.Packages.S2CPDescriptorBase.Valid"/> the Content</remarks>
		public static S2CPDescriptor[] Open(BinaryReader reader)
		{
			
			//BinaryReader reader = new BinaryReader(s2cp);
			byte[] header = reader.ReadBytes(18);
			int offset = reader.ReadInt32();
			string xml = Helper.ToString(reader.ReadBytes((int)(offset - reader.BaseStream.Position)));

			System.Xml.XmlDocument xmlfile = new XmlDocument();
			xmlfile.LoadXml(xml);

			//Root Node suchen
			XmlNodeList XMLData = xmlfile.GetElementsByTagName("Sims2Package");

			ArrayList list = new ArrayList();
			//Alle Eintr&auml;ge im Root Node verarbeiten
			//Data.MetaData.IndexTypes type = Data.MetaData.IndexTypes.ptShortFileIndex;
			for (int i=0; i<XMLData.Count; i++)
			{
				XmlNode node = XMLData.Item(i);
				foreach (XmlNode pfile in node) 
				{
					//a New FileItem
					if (pfile.LocalName == "PackagedFile") 
					{
						list.Add(ParesPackedFile(reader, pfile, offset));
					}
				} //foreach pfile
			} // for i

			S2CPDescriptor[] res = new S2CPDescriptor[list.Count];
			list.CopyTo(res);
			return res;
		}

		/// <summary>
		/// Displays a Save Dialog for the S2CP Files
		/// </summary>
		/// <param name="extension">true if you want to add the Community Extension, otherwise 
		/// a normal Sims2Pack File will be generated</param>
		public static bool ShowSaveDialog(bool extension) 
		{
			return ShowSaveDialog((SimPe.Packages.GeneratableFile)null, extension);
		}

		/// <summary>
		/// Displays a Save Dialog for the S2CP Files
		/// </summary>
		/// <param name="package">The package that should be added to the S2CP Files (the user can add more!)</param>
		/// <param name="extension">true if you want to add the Community Extension, otherwise 
		/// a normal Sims2Pack File will be generated</param>
		public static bool ShowSaveDialog(SimPe.Packages.GeneratableFile package, bool extension) 
		{

			SimPe.Packages.GeneratableFile[] fls = null;

			if (package!=null) 
			{
				fls = new SimPe.Packages.GeneratableFile[1];
				fls[0] = package;
			} 
			else 
			{
				fls = new SimPe.Packages.GeneratableFile[0];
			}

			return ShowSaveDialog(fls, extension);
		}

		/// <summary>
		/// Displays a Save Dialog for the S2CP Files
		/// </summary>
		/// <param name="packages">The packages that should be added to the S2CP Files (the user can add more!)</param>
		/// <param name="extension">true if you want to add the Community Extension, otherwise 
		/// a normal Sims2Pack File will be generated</param>
		/// <returns>true if the File was saved</returns>
		public static bool ShowSaveDialog(SimPe.Packages.GeneratableFile[] packages, bool extension)
        {
			SaveSims2CommunityPack form = new SaveSims2CommunityPack();
			S2CPDescriptor[] desc = form.Execute(packages, ref extension);
			if (desc!=null)
            {
                SoundPlayer savy = new SoundPlayer(booby.NoisyGirls.Save);
				MemoryStream ms = Create(desc, extension);
				try
                {
                    System.IO.FileStream fs = new FileStream(form.tbflname.Text, FileMode.Create);
                    fs.Write(ms.ToArray(), 0, (int)ms.Length);
                    savy.Play();
                    fs.Close();
                    fs.Dispose();
                    fs = null;
                }
                catch (Exception ex)
                {
                    Helper.ExceptionMessage(Localization.Manager.GetString("errwritingfile"), ex);
                    return false;
                }
				return true;
			}
			return false;
		}

		/// <summary>
		/// Show the Package Selector Dialog for a S2CP File
		/// </summary>
		/// <param name="filename">The Filename of the S2CP File</param>
		/// <param name="selmode">Selection Mode for the Listview</param>
		/// <returns>All Packages that were selected in the Dialog by the User or null 
		/// if the User Cancled the Dialog</returns>
		public static S2CPDescriptor[] ShowOpenDialog(string filename, System.Windows.Forms.SelectionMode selmode) 
		{
			SaveSims2CommunityPack form = new SaveSims2CommunityPack();
            form.tbflname.Text = System.IO.Path.GetFileName(filename);
            return form.Execute(Open(filename), selmode, filename.EndsWith("s2cp"));
		}

        internal static string s2cptype(GeneratableFile fl)
        {
            Interfaces.Files.IPackedFileDescriptor[] pfds;
            if (fl.FindFiles(0x4F424A44).Length > 0)//Object Data
            {
                string ret = "";
                pfds = fl.FindFiles(0x4F424A44);
                foreach (Interfaces.Files.IPackedFileDescriptor pfd in pfds)
                {
                    SimPe.PackedFiles.Wrapper.ExtObjd objd = new SimPe.PackedFiles.Wrapper.ExtObjd();
                    objd.ProcessData(pfd, fl);
                    if (objd.Ok == SimPe.PackedFiles.Wrapper.ObjdHealth.Unreadable) return objd.Ok.ToString();
                    if (objd.Type.ToString() == "SimType" && objd.ProxyGuid == 0x6DB7E00F) return "Hug Bug Infection";
                    if (pjse.GUIDIndex.TheGUIDIndex.ContainsKey(objd.Guid))
                    {
                        if (!pjse.GUIDIndex.TheGUIDIndex[objd.Guid].Contains("**"))
                        {
                            if (objd.Type.ToString() == "Person") return "NPC Mod (" + pjse.GUIDIndex.TheGUIDIndex[objd.Guid] + ")";
                            return "Override (" + pjse.GUIDIndex.TheGUIDIndex[objd.Guid] + ")";
                        }
                    }
                    if (objd.Type.ToString() == "SimType") return objd.Type.ToString() + getglobby(fl);
                    ret = objd.Type.ToString();
                }
                return ret;
            }
            // Sims with Packaged lots do have objd - Sims alone Don't (unless is NPC)
            if (fl.FindFiles(0xAC598EAC).Length > 0 && fl.FindFiles(0xEBCF3E27).Length > 0) return "Packaged Sim";//Age Data + Property Set - Sim
            if (fl.FindFiles(0x484F5553).Length > 0) return "Lot";//HOUS
            if (fl.FindFiles(0xCDB8BDC4).Length > 0) return "Family";//Single Sim Memory
            if (fl.FindFilesByGroup(0x7FD46CD0).Length > 0) return "Global Override";
            pfds = fl.FindFiles(0x8C1580B5); //Hairtone XML - hair that do have GZPS returned skin when GZPS was first
            if (pfds.Length == 0) pfds = fl.FindFiles(Data.MetaData.GZPS);
            if (pfds.Length == 0) pfds = fl.FindFiles(Data.MetaData.XOBJ); //Object XML
            if (pfds.Length == 0) pfds = fl.FindFiles(Data.MetaData.XSTN); //Skin Tone XML
            if (pfds.Length == 0) pfds = fl.FindFiles(0x2C1FD8A1); //TextureOverlay XML
            if (pfds.Length == 0) pfds = fl.FindFiles(0x0C1FE246); //Mesh Overlay XML
            if (pfds.Length == 0) pfds = fl.FindFiles(Data.MetaData.XROF); //Object XML
            if (pfds.Length == 0) pfds = fl.FindFiles(Data.MetaData.XFLR); //Object XML
            if (pfds.Length == 0) pfds = fl.FindFiles(Data.MetaData.XFNC); //Object XML
            if (pfds.Length > 0)
            {
                SimPe.PackedFiles.Wrapper.Cpf cpf = new SimPe.PackedFiles.Wrapper.Cpf();
                cpf.ProcessData(pfds[0], fl, false);
                string type = cpf.GetSaveItem("type").StringValue.Trim().ToLower();
                switch (type)
                {
                    case "wall":
                        {
                            return SimPe.Cache.PackageType.Wallpaper.ToString();
                        }
                    case "terrainpaint":
                        {
                            return SimPe.Cache.PackageType.Terrain.ToString();
                        }
                    case "floor":
                        {
                            return SimPe.Cache.PackageType.Floor.ToString();
                        }
                    case "roof":
                        {
                            return SimPe.Cache.PackageType.Roof.ToString();
                        }
                    case "fence":
                        {
                            if (cpf.GetSaveItem("ishalfwall").UIntegerValue == 1) return "Half Wall";
                            return SimPe.Cache.PackageType.Fence.ToString();
                        }
                    case "skin":
                        {
                            uint cat = cpf.GetSaveItem("category").UIntegerValue;
                            if ((cat & (uint)Data.OutfitCats.Skin) != 0) return SimPe.Cache.PackageType.Skin.ToString();
                            else return SimPe.Cache.PackageType.Clothing.ToString();
                        }
                    case "meshoverlay":
                    case "textureoverlay":
                        {
                            if (cpf.GetSaveItem("subtype").UIntegerValue == (uint)Data.TextureOverlayTypes.Blush) return SimPe.Cache.PackageType.Blush.ToString();
                            if (cpf.GetSaveItem("subtype").UIntegerValue == (uint)Data.TextureOverlayTypes.Eye) return SimPe.Cache.PackageType.Eye.ToString();
                            if (cpf.GetSaveItem("subtype").UIntegerValue == (uint)Data.TextureOverlayTypes.EyeBrow) return SimPe.Cache.PackageType.EyeBrow.ToString();
                            if (cpf.GetSaveItem("subtype").UIntegerValue == (uint)Data.TextureOverlayTypes.EyeShadow) return SimPe.Cache.PackageType.EyeShadow.ToString();
                            if (cpf.GetSaveItem("subtype").UIntegerValue == (uint)Data.TextureOverlayTypes.Glasses) return SimPe.Cache.PackageType.Glasses.ToString();
                            if (cpf.GetSaveItem("subtype").UIntegerValue == (uint)Data.TextureOverlayTypes.Lipstick) return SimPe.Cache.PackageType.Lipstick.ToString();
                            if (cpf.GetSaveItem("subtype").UIntegerValue == (uint)Data.TextureOverlayTypes.Mask) return SimPe.Cache.PackageType.Mask.ToString();
                            if (cpf.GetSaveItem("subtype").UIntegerValue == (uint)Data.TextureOverlayTypes.Beard) return SimPe.Cache.PackageType.Beard.ToString();
                            if (type == "meshoverlay") return SimPe.Cache.PackageType.Accessory.ToString();
                            return SimPe.Cache.PackageType.Makeup.ToString();
                        }
                    case "hairtone":
                        {
                            return SimPe.Cache.PackageType.Hair.ToString();
                        }
                    case "skintone":
                        {
                            return SimPe.Cache.PackageType.Skin.ToString();
                        }
                }
            }
            if (fl.FindFile(0xAC4F8687, 0x64B33975, 0x1C0532FA, 0xFF84D154) != null) return "Face Template Override";//Specific GMDC
            if (fl.FindFiles(0xAC4F8687).Length > 0) return "Mesh";//GMDC
            if (fl.FindFiles(0xAC598EAC).Length > 0 && fl.FindFiles(0xAC506764).Length > 0 && fl.FindFiles(0x0C7E9A76).Length > 0) return "Pet Breed or Coat";//Age Data,3DID,JPEG
            if (fl.FindFiles(0xAC598EAC).Length > 0 && fl.FindFiles(0xAC506764).Length > 0) return "Outfit (extension)";//Age Data + 3DID
            if (fl.FindFiles(0x7BA3838C).Length > 0 && fl.FindFiles(0x4C697E5A).Length > 0) return "Colour Enabler";//GMND + MMAT
            if (fl.FindFiles(0x4C697E5A).Length > 0 && fl.FindFiles(0x49596978).Length > 0) return "Re-Colour";//MMAT + TXMT
            if (fl.FindFilesByGroup(0x7FE066DD).Length > 0) return "Game Tip Mod";
            if (fl.FindFilesByGroup(0x08000600).Length > 0 || fl.FindFilesByGroup(0xA99D8A11).Length > 0 || fl.FindFilesByGroup(0x499DB772).Length > 0) return "UI Mod";//The UI groups
            //no Object Data so now this can't be a custom object, must be a mod
            if (fl.FindFiles(0x42484156).Length > 0 || fl.FindFiles(0x7F00AADE).Length > 0 || fl.FindFiles(0x4F424A66).Length > 0) return "Behaviour Mod";//BHAV,BCON,OBJF
            if (fl.FindFiles(0x43545353).Length > 0 || fl.FindFiles(0x53545223).Length > 0 || fl.FindFiles(0x54544173).Length > 0) return "General Mod";//CTSS,STR#,TTAs
            if (fl.FindFiles(0x54544142).Length > 0 || fl.FindFiles(0x856DDBAC).Length > 0) return "General Mod";//TTAB,jpg/tga/png Image
            if (fl.FindFilesInGroup(0x1C4A276C, 0x1C0532FA).Length > 0) return "Texture Override";//TXMT In default group
            return SimPe.Cache.PackageType.Unknown.ToString();
        }

        internal static string getglobby(GeneratableFile phile)
        {
            Interfaces.Files.IPackedFileDescriptor[] pfds = phile.FindFiles(0x474C4F42);//Global Data
            if (pfds.Length > 0)
            {
                SimPe.Plugin.Glob glob = new SimPe.Plugin.Glob();
                glob.ProcessData(pfds[0], phile);
                return " (" + glob.SemiGlobalName + ")";
            }
            return "";
        }

        internal static void gethertitle(GeneratableFile pack, S2CPDescriptor fil)
        {
            SimPe.Interfaces.Files.IPackedFileDescriptor[] pfds;
            if (pack.FindFiles(0x484F5553).Length > 0)//HOUS
            {
                pfds = pack.FindFiles(0x6C589723);//LOTD
                if (pfds.Length == 0) return;
                SimPe.Plugin.Lot lote = new SimPe.Plugin.Lot();
                lote.ProcessData(pfds[0], pack);
                fil.Title = lote.LotName;
                fil.Description = lote.Description;
            }
            else if (pack.FindFiles(0xCDB8BDC4).Length > 0)//Single Sim Memory - Family
            {
                pfds = pack.FindFiles(0x53545223);//STR#
                if (pfds.Length == 0) return;
                SimPe.PackedFiles.Wrapper.Str str = new SimPe.PackedFiles.Wrapper.Str();
                str.ProcessData(pfds[0], pack);
                SimPe.PackedFiles.Wrapper.StrItemList items = str.FallbackedLanguageItems(Helper.WindowsRegistry.LanguageCode);
                if (items.Length > 0) fil.Title = items[0].Title + " Family";
                if (items.Length > 1) fil.Description = Helper.ToString(Helper.ToBytes(items[1].Title));
            }
            else
            {
                pfds = pack.FindFiles(0x43545353);//CTSS
                if (pfds.Length == 0) return;
                SimPe.PackedFiles.Wrapper.Str str = new SimPe.PackedFiles.Wrapper.Str();
                str.ProcessData(pfds[0], pack);
                SimPe.PackedFiles.Wrapper.StrItemList items = str.FallbackedLanguageItems(Helper.WindowsRegistry.LanguageCode);
                if (items.Length > 0) fil.Title = Helper.ToString(Helper.ToBytes(items[0].Title));//to use ASCII Encoding
                if (items.Length > 1) fil.Description = Helper.ToString(Helper.ToBytes(items[1].Title));
                if (items.Length > 2) fil.Title += " " + Helper.ToString(Helper.ToBytes(items[2].Title));
            }
        }

        internal static bool findhugbug(GeneratableFile phile)
        {
            Interfaces.Files.IPackedFileDescriptor OBJT = phile.FindFile(0x6F626A74, 0, 0xFFFFFFFF, 0);
            if (OBJT != null)
            {
                uint guide;
                string namer;
                Interfaces.Files.IPackedFile PF = phile.Read(OBJT);
                byte[] dati = PF.UncompressedData;
                BinaryReader brd = SimPe.Helper.GetBinaryReader(dati);
                brd.BaseStream.Seek(76, System.IO.SeekOrigin.Begin);
                while (brd.BaseStream.Position < brd.BaseStream.Length) // to help prevent trying to read past the end
                {
                    guide = brd.ReadUInt32();
                    if (guide == 0x6DB7E00F) return true;
                    if (guide == 0) return false;
                    brd.BaseStream.Seek(24, System.IO.SeekOrigin.Current);
                    namer = brd.ReadString();
                    brd.BaseStream.Seek(4, System.IO.SeekOrigin.Current);
                    if (brd.ReadUInt32() == 0x6DB7E00F) return true;
                }
            }
            return false;
        }
	}
}
