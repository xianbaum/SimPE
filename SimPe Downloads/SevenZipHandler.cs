using System;
using System.Collections;
using SharpCompress.Archives;
using SharpCompress.Common;

namespace SimPe.Plugin.Downloads
{
	/// <summary>
	/// Summary description for SevenZipHandler.
	/// </summary>
	public class SevenZipHandler : ArchiveHandler
	{
		public SevenZipHandler(string filename) : base(filename)
		{

		}

		protected override StringArrayList ExtractArchive()
		{
			StringArrayList ret = new StringArrayList();
			using (IArchive archive = ArchiveFactory.Open(this.ArchiveName))
			{
				ExtractionOptions opts = new ExtractionOptions { ExtractFullPath = true, Overwrite = true };
				foreach (IArchiveEntry entry in archive.Entries)
				{
					if (entry.IsDirectory) continue;
					entry.WriteToDirectory(SimPe.Helper.SimPeTeleportPath, opts);

					string rname = System.IO.Path.Combine(Helper.SimPeTeleportPath, entry.Key);
					if (System.IO.File.Exists(rname))
						ret.Add(rname);
				}
			}
			return ret;
		}
	}
}
