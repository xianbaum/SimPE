using System;
using System.Collections.Generic;
using System.Text;

namespace SimPe.PackedFiles.Wrapper.SCOR
{
    class ScorItemTokenBffl : IScorItemToken
    {
        ScoreItemBffl gui;
        public ScorItemTokenBffl()
        {
            gui = null;
        }
        public byte[] UnserializeToken(ScorItem si, System.IO.BinaryReader reader)
        {
            gui = new ScoreItemBffl(si);
            try
            {
                int cnt = reader.ReadInt32();
                gui.AddStart(cnt);
                for (int i = 0; i < cnt; i++)
                {
                    byte pendi = reader.ReadByte();
                    uint sim = reader.ReadUInt32();
                    ExtSDesc sdsc = FileTable.ProviderRegistry.SimDescriptionProvider.FindSim((ushort)sim) as ExtSDesc;
                    if (sdsc == null) gui.AddSim(SimPe.Localization.GetString("Unknown") + " (0x" + Helper.HexString(sim) + ")", sim);
                    else gui.AddSim(sdsc.SimName + " " + sdsc.SimFamilyName, sim);

                    byte[] stuf = reader.ReadBytes(23);
                    gui.Addtyme(reader.ReadUInt16());
                    // need 25 bytes after SimName
                }
            }
            catch { gui.AddError(); }
            return new byte[0];
        }

        public SCOR.AScorItem ActivatedGUI
        {
            get { return gui; }
        }
    }
}
