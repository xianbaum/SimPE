using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Ambertation.Windows.Forms
{
    public class Serializer
    {
        static Serializer sz;        
        public static Serializer Global
        {
            get
            {
                if (sz == null) sz = new Serializer();
                return sz;
            }
        }

        Dictionary<Type, Descriptor> map;
        Dictionary<int, Descriptor> revmap;

        Dictionary<string, Control> items;
        Dictionary<string, ToolStripItem> buts;
        Serializer()
        {
            items = new Dictionary<string, Control>();
            buts = new Dictionary<string, ToolStripItem>();

            map = new Dictionary<Type, Descriptor>();
            map[typeof(object)] = new Descriptor(0, new SerializeControl(SerializeGeneric), new DeSerializeControl(DeserializeGeneric));
            map[typeof(ToolStripItem)] = new Descriptor(1, new SerializeControl(SerializeToolStripItem), new DeSerializeControl(DeserializeToolStripItem));
            map[typeof(ToolStrip)] = new Descriptor(2, new SerializeControl(SerializeToolStrip), new DeSerializeControl(DeserializeToolStrip));
            map[typeof(DockManager)] = new Descriptor(3, new SerializeControl(SerializeDockManager), new DeSerializeControl(DeserializeDockManager));


            revmap = new Dictionary<int, Descriptor>();
            foreach (Descriptor d in map.Values)
                revmap[d.Id] = d;
        }

        #region Register
        void RegisterControl(Control obj)
        {
            if (obj == null) return;
            items[obj.Name] = obj;
        }

        public void Register(DockManager dm)
        {
            RegisterControl(dm);
        }

        public void Register(ToolStripContainer tsc)
        {
            RegisterControl(tsc);
            Register(tsc.LeftToolStripPanel);
            Register(tsc.TopToolStripPanel);
            Register(tsc.RightToolStripPanel);
            Register(tsc.BottomToolStripPanel);
            Register(tsc.ContextMenuStrip);
        }

        public void Register(ToolStripPanel pn)
        {
            if (pn.Name == "") pn.Name = "myToolStripPanel_" + items.Count;
            RegisterControl(pn);

            foreach (Control c in pn.Controls)
            {
                ToolStrip ts = c as ToolStrip;
                if (ts == null) continue;

                Register(ts);
            }
        }

        public void Register(ContextMenuStrip men)
        {
            RegisterControl(men);

            foreach (ToolStripItem ts in men.Items)
                Register(ts);
        }

        public void Register(ToolStrip ts)
        {
            RegisterControl(ts);
            foreach (ToolStripItem i in ts.Items)
                Register(i);
        }

        public void Register(ToolStripItem item)
        {
            if (item == null) return;
            buts[item.Name] = item;
        }
        #endregion

        #region Serialize
        public void Serialize(string flname)
        {
            BinaryWriter writer = new BinaryWriter(File.Create(flname));
            SerializeButtons(writer);
            SerializeControls(writer);
            writer.Close();
        }

        private void SerializeControls(BinaryWriter writer)
        {
            writer.Write(items.Count);
            foreach (Control c in items.Values)
            {
                Type t = c.GetType();
                if (!map.ContainsKey(t)) t = typeof(object);

                Descriptor d = map[t];
                Serialize(writer, d, c.Name, c);

            }
        }

        private void SerializeButtons(BinaryWriter writer)
        {
            writer.Write(buts.Count);
            foreach (ToolStripItem ts in buts.Values)
            {
                Descriptor d = map[typeof(ToolStripItem)];
                Serialize(writer, d, ts.Name, ts);
            }
        }

        void Serialize(BinaryWriter writer, Descriptor d, string name, object o)
        {
            if (o == null || d == null) return;
            writer.Write(d.Id);
            writer.Write(name);
            d.Serilaizer(writer, o);
        }
        #endregion

        #region Deserialize
        public void Deserialize(string flname)
        {
            BinaryReader reader = new BinaryReader(File.Open(flname, FileMode.Open));
            DeserializeButtons(reader);

            int ct = reader.ReadInt32();
            for (int i = 0; i < ct; i++)
            {
                int id = reader.ReadInt32();
                string name = reader.ReadString();
                if (revmap.ContainsKey(id))
                {
                    Descriptor d = revmap[id];
                    Control c;
                    if (items.ContainsKey(name)) c  = items[name];
                    else c = new Control();
                    d.DeSerializer(reader, c);                    
                }
            }
            reader.Close();
        }

        private void DeserializeButtons(BinaryReader reader)
        {
            int ct = reader.ReadInt32();
            for (int i = 0; i < ct; i++)
            {
                int id = reader.ReadInt32();
                string name = reader.ReadString();
                if (revmap.ContainsKey(id))
                {
                    Descriptor d = revmap[id];
                    if (buts.ContainsKey(name))
                    {
                        ToolStripItem ts = buts[name];
                        d.DeSerializer(reader, ts);
                    }
                }
            }
        }
        #endregion


        #region Descriptor
        protected class Descriptor
        {
            int id;
            public int Id
            {
                get { return id; }
            }

            SerializeControl ser;
            public SerializeControl Serilaizer
            {
                get { return ser; }
            }

            DeSerializeControl deser;
            public DeSerializeControl DeSerializer
            {
                get { return deser; }
            }

            public Descriptor(int id, SerializeControl ser, DeSerializeControl deser)
            {
                this.id = id;
                this.ser = ser;
                this.deser = deser;
            }
        }
        protected delegate void SerializeControl(BinaryWriter writer, object o);
        protected delegate void DeSerializeControl(BinaryReader reader, object o);
        #endregion

        void SerializeGeneric(BinaryWriter writer, object o)
        {
        }


        void DeserializeGeneric(BinaryReader reader, object o)
        {
        }


        void SerializeToolStripItem(BinaryWriter writer, object o)
        {
            ToolStripItem ts = o as ToolStripItem;
            writer.Write((int)ts.Overflow);
            writer.Write(ts.Visible);
        }

        void DeserializeToolStripItem(BinaryReader reader, object o)
        {
            ToolStripItem ts = o as ToolStripItem;
            ts.Overflow = (ToolStripItemOverflow)reader.ReadInt32();
            ts.Visible = reader.ReadBoolean();
            if (ts is ToolStripButtonExt || ts is MenuStripButtonExt) ts.Visible = true;
        }

        void SerializeToolStrip(BinaryWriter writer, object o)
        {
            ToolStrip ts = o as ToolStrip;
            writer.Write(ts.Location.X);
            writer.Write(ts.Location.Y);
            if (ts.Parent != null)
            {
                writer.Write(ts.Parent.Name);
                writer.Write(ts.Parent.Controls.GetChildIndex(ts));
            }
            else
            {
                writer.Write("");
                writer.Write((int)0);
            }
            writer.Write(ts.Visible);
           
        }

        void DeserializeToolStrip(BinaryReader reader, object o)
        {
            ToolStrip ts = o as ToolStrip;
            int x = reader.ReadInt32();
            int y = reader.ReadInt32();
            ts.Location = new System.Drawing.Point(x, y);
            string pname = reader.ReadString();
            int index = reader.ReadInt32();
            if (items.ContainsKey(pname))
            {
                ts.Parent = items[pname];
                ts.Parent.Controls.SetChildIndex(ts, index);
            }
            ts.Visible = reader.ReadBoolean();
        }

        void SerializeDockManager(BinaryWriter writer, object o)
        {
            DockManager dm = o as DockManager;
            dm.Serialize(writer);
        }

        void DeserializeDockManager(BinaryReader reader, object o)
        {
            DockManager dm = o as DockManager;
            dm.Deserialize(reader);
        }
    }
}
