using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Ambertation.Windows.Forms
{
    partial class DockManager
    {
        const uint MAGIC = 0xFB001A07;
        const uint VERSION = 6;

        public List<DockPanel> GetPanels()
        {
            Dictionary<string, DockPanel> list = new Dictionary<string, DockPanel>();
            GetPanels(list);

            List<DockPanel> ret = new List<DockPanel>();
            foreach (DockPanel dp in list.Values)
                ret.Add(dp);

            return ret;
        }

        protected override void GetPanels(Dictionary<string, DockPanel> list)
        {
            foreach (DockPanel dp in floatingpanels)
            {
                if (dp.Name == "") dp.Name = "dp_" + list.Count+"_"+dp.Guid.ToString();
                list[dp.Name] = dp;
            }
            base.GetPanels(list);
        }

        public void Serialize(BinaryWriter writer)
        {
            writer.Write(MAGIC);
            writer.Write(VERSION);
            Dictionary<string, DockPanel> list = new Dictionary<string, DockPanel>();
            GetPanels(list);

            int counter = 0;
            DoSerialize(writer, ref counter);

            writer.Write(list.Count);
            foreach (DockPanel p in list.Values)
            {
                writer.Write(p.Name);
                p.Serialize(writer);
            }
        }

        public class DockContainerDescriptor
        {
            DockContainer dc;
            public DockContainer Container
            {
                get { return dc; }
            }

            int index;
            public int Index
            {
                get { return index; }
            }

            bool collapsed;
            public bool Collapsed
            {
                get { return collapsed; }
            }

            string hname;
            public string HighlightName
            {
                get { return hname;}
            }

            internal DockContainerDescriptor(DockContainer dc, int index, bool collapsed, string highlightname)
            {
                this.dc = dc;
                this.index = index;
                this.collapsed = collapsed;
                this.hname = highlightname;
            }
        }

        void ReadException(BinaryReader reader, string msg)
        {
            throw new System.IO.FileLoadException(msg);
        }

        public void Deserialize(BinaryReader reader)
        {
            uint mg = reader.ReadUInt32();
            uint ver = reader.ReadUInt32();

            if (mg != MAGIC) ReadException(reader, "Not a DockLayout Resource (invalid MAGIC Code)");
            if (ver > VERSION) ReadException(reader, "Not a DockLayout Resource (unknown Version)");

            bool vis = Visible;            
            SuspendLayout();
            Visible = false;

            Dictionary<string , DockPanel> list = new Dictionary<string , DockPanel>();
            Dictionary<string, DockPanel> vlist = new Dictionary<string, DockPanel>();
            GetPanels(list);

            foreach (DockButtonBar bb in colconts.Values)
                bb.Clear();

            Dictionary<string, DockContainerDescriptor> docks = new Dictionary<string, DockContainerDescriptor>();
            DeserializeContainers(reader, docks);

            DeserializePanels(reader, list, vlist, docks, ver);            
            DeserializePass2(docks, vlist);

            Visible = vis;
            ResumeLayout();
        }

        protected void DeserializeContainers(BinaryReader reader, Dictionary<string, DockManager.DockContainerDescriptor> docks)
        {
            PrepareDeserialize();
            DoDeserialize(reader, docks, null);
        }

        private void DeserializePanels(BinaryReader reader, Dictionary<string, DockPanel> list, Dictionary<string, DockPanel> vlist, Dictionary<string, DockContainerDescriptor> docks, uint ver)
        {
            int ct = reader.ReadInt32();
            for (int i = 0; i < ct; i++)
            {
                string name = reader.ReadString();
                DockPanel p;
                if (!list.ContainsKey(name))
                {
                    p = ManagerSingelton.Global.GetPanelWithName(name);
                    if (p == null) p= new DockPanel(this);
                }
                else
                {
                    p = list[name];
                    list.Remove(name);
                    vlist[name] = p;
                }

                p.Deserialize(reader, docks, ver);
            }
            
            CloseRemainingPanels(list);
        }

        private static void CloseRemainingPanels(Dictionary<string, DockPanel> list)
        {
            foreach (DockPanel p in list.Values)
                p.Close();
        }

        protected void DeserializePass2(Dictionary<string, DockManager.DockContainerDescriptor> docks, Dictionary<string, DockPanel> list)
        {
            foreach (DockContainerDescriptor dc in docks.Values)
            {
                if (dc.Container is DockManager) continue;

                dc.Container.Parent.Controls.SetChildIndex(dc.Container, dc.Index);
                if (dc.Collapsed) dc.Container.Collapse(false);
                else dc.Container.Visible = true;
                if (list.ContainsKey(dc.HighlightName)) list[dc.HighlightName].EnsureVisible();

                dc.Container.SetNoCleanUpIntern(false);
                dc.Container.SetForceUseAsTarget(false);
                
            }
        }
    }
}
