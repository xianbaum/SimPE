/***************************************************************************
 *   Copyright (C) 2008 by Peter L Jones                                   *
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
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using SimPe.Interfaces;
using SimPe.Interfaces.Plugin;
using SimPe.Interfaces.Scenegraph;
using SimPe.Interfaces.Files;
using SimPe.PackedFiles.Wrapper;

namespace pj
{
    public class cQuickCloneTool
    {
        public void Execute()
        {
            CloneWhat frm = new CloneWhat();
            frm.useGUID = true;
            frm.Value = 0;
            DialogResult dr = frm.ShowDialog();
            if (dr != DialogResult.OK) return;
            if (frm.Value == 0) return;

            uint group = frm.useGUID ? getGroup(frm.Value) : frm.Value;


            List<pjse.FileTable.Entry> opItems = new List<pjse.FileTable.Entry>(pjse.FileTable.GFT.FindGroup(group, pjse.FileTable.Source.Maxis));
            if (opItems.Count == 0)
            {
                string m = "";
                if (frm.useGUID)
                    m = L.Get("noItemsForGUID", "0x" + SimPe.Helper.HexString(frm.Value), "0x" + SimPe.Helper.HexString(group));
                else
                    m = L.Get("noItemsForGroup", "0x" + SimPe.Helper.HexString(group));

                MessageBox.Show(m, frm.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);

                return;
            }

            foreach (pjse.FileTable.Entry item in opItems)
            {
                if (!(item.Wrapper is StrWrapper && item.Instance == 0x85)) continue;
                StrWrapper sw = new StrWrapper();
                sw.ProcessData(item.PFD, item.Package);
                List<string> acnames = new List<string>();
                for (int i = 1; i < sw.CountOf(1); i++)
                    opItems.AddRange(getScenegraph(((StrItem)sw[1, i]).Title));
                break;
            }

            MessageBox.Show("Group 0x" + SimPe.Helper.HexString(group) + "\r\nfiles: " + opItems.Count.ToString());
        }

        private uint getGroup(uint guid)
        {
            SimPe.Wait.SubStart();
            pjse.FileTable.Entry[] items = pjse.FileTable.GFT[SimPe.Data.MetaData.OBJD_FILE];
            SimPe.Wait.SubStop();

            try
            {
                SimPe.Wait.SubStart(items.Length);
                foreach (pjse.FileTable.Entry item in items)
                {
                    System.IO.BinaryReader reader = item.Wrapper.StoredData;

                    if (reader.BaseStream.Length > 0x5c + 4) // sizeof(uint)
                    {
                        reader.BaseStream.Seek(0x5c, System.IO.SeekOrigin.Begin);
                        if (reader.ReadUInt32() == guid)
                            return item.Group;
                    }

                    SimPe.Wait.Progress++;
                }
            }
            finally
            {
                SimPe.Wait.SubStop();
            }
            return 0;
        }

        private pjse.FileTable.Entry[] getScenegraph(string cresName)
        {
            if (!cresName.ToLower().EndsWith("_cres")) cresName += "_cres";

            List<pjse.FileTable.Entry> result = new List<pjse.FileTable.Entry>();

            SimPe.Wait.SubStart();
            pjse.FileTable.Entry[] items = pjse.FileTable.GFT[SimPe.Data.MetaData.CRES, pjse.FileTable.Source.Maxis];
            SimPe.Wait.SubStop();

            try
            {
                SimPe.Wait.SubStart(items.Length);
                foreach (pjse.FileTable.Entry item in items)
                {
                    SimPe.Wait.Progress++;

                    SimPe.Plugin.GenericRcol cres = new SimPe.Plugin.GenericRcol();
                    cres.ProcessData(item.PFD, item.Package);
                    if (cres.FileName != cresName) continue;

                    result.Add(item);
                    result.AddRange(getScenegraph(item));

                    break;
                }
            }
            finally
            {
                SimPe.Wait.SubStop();
            }
            return result.ToArray();
        }

        private pjse.FileTable.Entry[] getScenegraph(pjse.FileTable.Entry item)
        {
            return new pjse.FileTable.Entry[0];
        }
    }
}
