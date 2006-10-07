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

        List<Pass2Descriptor> pass2;
        Dictionary<Type, SerilaizeDescriptor> map;
        Dictionary<int, SerilaizeDescriptor> revmap;

        Dictionary<string, Control> items;
        Dictionary<string, ToolStripItem> buts;
        Serializer()
        {
            namect = 0;
            pass2 = new List<Pass2Descriptor>();
            items = new Dictionary<string, Control>();
            buts = new Dictionary<string, ToolStripItem>();

            map = new Dictionary<Type, SerilaizeDescriptor>();
            map[typeof(object)] = new SerilaizeDescriptor(0, new SerializeControl(SerializeGeneric), new DeSerializeControl(DeserializeGeneric));
            map[typeof(ToolStripItem)] = new SerilaizeDescriptor(1, new SerializeControl(SerializeToolStripItem), new DeSerializeControl(DeserializeToolStripItem));
            map[typeof(ToolStrip)] = new SerilaizeDescriptor(2, new SerializeControl(SerializeToolStrip), new DeSerializeControl(DeserializeToolStrip));
            map[typeof(DockManager)] = new SerilaizeDescriptor(3, new SerializeControl(SerializeDockManager), new DeSerializeControl(DeserializeDockManager));


            reorderstrips = new List<Pass2ToolStripDescriptor>();
            revmap = new Dictionary<int, SerilaizeDescriptor>();
            foreach (SerilaizeDescriptor d in map.Values)
                revmap[d.Id] = d;
        }

        #region Register
        void RegisterControl(Control obj)
        {
            if (obj == null) return;
            SetName(obj);
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
            SetName(item);
            buts[item.Name] = item;
        }
        #endregion

        int namect;
        void SetName(Control c)
        {
            if (c.Name == "")
            {
                c.Name = "my"+c.GetType().Name+"_" + namect;
                namect++;
            }
        }

        void SetName(ToolStripItem c)
        {
            if (c.Name == "")
            {
                c.Name = "my" + c.GetType().Name + "_" + namect;
                namect++;
            }
        }

        const uint MAGIC = 0xFB001A07;
        const uint VERSION = 5;
        #region Serialize
        public void ToFile(string flname)
        {
            BinaryWriter writer = new BinaryWriter(File.Create(flname));
            Serialize(writer);
            writer.Close();
        }

        public Stream ToStream()
        {
            MemoryStream s = new MemoryStream();
            ToStream(s);
            return s;
        }

        public void ToStream(Stream s)
        {
            BinaryWriter bw = new BinaryWriter(s);
            Serialize(bw);
            s.Flush();
        }


        private void Serialize(BinaryWriter writer)
        {
            writer.Write(MAGIC);
            writer.Write(VERSION);
            SerializeButtons(writer);
            SerializeControls(writer);
        }

        private void SerializeControls(BinaryWriter writer)
        {            
            
            writer.Write(items.Count);
            foreach (Control c in items.Values)
            {
                Type t = c.GetType();
                if (!map.ContainsKey(t)) t = typeof(object);

                SerilaizeDescriptor d = map[t];
                Serialize(writer, d, c.Name, c);

            }
        }

        private void SerializeButtons(BinaryWriter writer)
        {
            writer.Write(buts.Values.Count);
            foreach (ToolStripItem ts in buts.Values)
            {
                SerilaizeDescriptor d = map[typeof(ToolStripItem)];
                Serialize(writer, d, ts.Name, ts);
            }
        }

        void Serialize(BinaryWriter writer, SerilaizeDescriptor d, string name, object o)
        {
            if (o == null) o = new Object();
            if (d == null) d = map[typeof(object)];
            //Console.WriteLine(writer.BaseStream.Position + ": " + d.Id + " " + name);
            writer.Write(d.Id);
            writer.Write(name);
            d.Serilaizer(writer, o);
        }
        #endregion

        #region Deserialize
        public void FromFile(string flname)
        {
            if (File.Exists(flname))
            {
                Stream s = File.Open(flname, FileMode.Open);
                try
                {
                    FromStream(s);
                }
                finally
                {
                    s.Close();
                }
            }
        }

        public void FromStream(Stream s)
        {           
            FromStream(s, true);
        }

        void ReadException(BinaryReader reader, string msg)
        {            
            throw new System.IO.FileLoadException(msg);
        }

        public void FromStream(Stream s, bool seekbeg)
        {
            if (seekbeg) s.Seek(0, SeekOrigin.Begin);
            pass2.Clear();
            BinaryReader reader = new BinaryReader(s);

            if (s.Length - s.Position < 8) return; // ReadException(reader, "Not a Layout Resource (invalid length)");
            uint mg = reader.ReadUInt32();
            uint ver = reader.ReadUInt32();

            if (mg != MAGIC) ReadException(reader, "Not a Layout Resource (invalid MAGIC Code)");
            if (ver>VERSION) ReadException(reader, "Not a Layout Resource (unknown Version)");
            DeserializeButtons(reader);
            DeserializeControls(reader);
            //reader.Close();

            foreach (Pass2Descriptor pass in pass2)
                pass.Pass2(pass);

            RorderButtons();
        }

        private void RorderButtons()
        {
            reorderstrips.Sort();
            foreach (Pass2ToolStripDescriptor ts in reorderstrips)
            {
                ToolStrip strip = ts.Object as ToolStrip;
                strip.Parent = null;
            }

            foreach (Pass2ToolStripDescriptor ts in reorderstrips)
            {
                ToolStripPanel parent = ts.Parent as ToolStripPanel;
                ToolStrip strip = ts.Object as ToolStrip;
                if (parent != null)
                {
                    strip.Location = ts.Location;
                    parent.Controls.Add(strip);
                    strip.Location = ts.Location;
                }
            }
        }

        private void DeserializeControls(BinaryReader reader)
        {
            reorderstrips.Clear();
            int ct = reader.ReadInt32();
            for (int i = 0; i < ct; i++)
            {
                Console.WriteLine(reader.BaseStream.Position);
                int id = reader.ReadInt32();
                string name = reader.ReadString();
                if (revmap.ContainsKey(id))
                {
                    SerilaizeDescriptor d = revmap[id];
                    Control c;
                    if (items.ContainsKey(name)) c = items[name];
                    else c = new Control();
                    d.DeSerializer(reader, c);
                } 
            }
        }

        private void DeserializeButtons(BinaryReader reader)
        {
            int ct = reader.ReadInt32();
            for (int i = 0; i < ct; i++)
            {
                Console.WriteLine(reader.BaseStream.Position);
                int id = reader.ReadInt32();
                string name = reader.ReadString();
                if (revmap.ContainsKey(id))
                {
                    SerilaizeDescriptor d = revmap[id];
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
        
        protected class Pass2Descriptor 
        {
            object o, par;
            public object Object
            {
                get { return o; }
            }

            public Control Parent
            {
                get { return par as Control; }
            }


            Pass2Control ser;
            public Pass2Control Pass2
            {
                get { return ser; }
            }



            public Pass2Descriptor(object o, Pass2Control pass)
            {
                this.o = o;
                this.ser = pass;
                if (o is Control)
                {
                    par = ((Control)o).Parent;
                }
                else
                {
                    par = null;
                }
            }

            
        }

        protected class Pass2ToolStripDescriptor : Pass2Descriptor, IComparable
        {
            System.Drawing.Point loc;
            public System.Drawing.Point Location
            {
                get { return loc; }
            }

            int index;
            public int Index
            {
                get { return index; }
            }

            public Pass2ToolStripDescriptor(object o, Pass2Control pass, int index, System.Drawing.Point loc)
                : base(o, pass)
            {
                this.loc = loc;
                this.index = index;
            }

            #region IComparable Member

            public int CompareTo(object obj)
            {
                Pass2ToolStripDescriptor b = obj as Pass2ToolStripDescriptor;
                if (b != null)
                {
                    if (this.Location.X > b.Location.X) return 1;
                    else if (this.Location.X < b.Location.X) return -1;
                }

                return 0;
            }

            #endregion
        }
        protected delegate void Pass2Control(Pass2Descriptor o);

        protected class SerilaizeDescriptor
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

            public SerilaizeDescriptor(int id, SerializeControl ser, DeSerializeControl deser)
            {
                this.id = id;
                this.ser = ser;
                this.deser = deser;
            }
        }
        protected delegate void SerializeControl(BinaryWriter writer, object o);
        protected delegate void DeSerializeControl(BinaryReader reader, object o);
        #endregion
        #region Custom (De)Serializing
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
            writer.Write(ts.Available);
            if (ts is ToolStripButton)
            {
                writer.Write(((ToolStripButton)ts).Checked);
                //Console.WriteLine(((ToolStripButton)ts).Checked + " " + ((ToolStripButton)ts).Visible + " " + ((ToolStripButton)ts).Available);
            }
        }

        void DeserializeToolStripItem(BinaryReader reader, object o)
        {
            ToolStripItem ts = o as ToolStripItem;
            ts.Overflow = (ToolStripItemOverflow)reader.ReadInt32();
            bool vis = reader.ReadBoolean(); 
            ts.Visible = vis;
            ts.Available = reader.ReadBoolean();
            
            if (ts is ToolStripButtonExt || ts is MenuStripButtonExt) ts.Visible = true;

            if (ts is ToolStripButton)
            {
                bool chk = reader.ReadBoolean();
                ((ToolStripButton)ts).Checked = chk;
                //Console.WriteLine(((ToolStripButton)ts).Checked + " " + ((ToolStripButton)ts).Visible + " " + ((ToolStripButton)ts).Available);
            }
            
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
            
            string pname = reader.ReadString();
            int index = reader.ReadInt32();
            if (items.ContainsKey(pname))
            {
                ts.Parent = items[pname];
                ts.Parent.Controls.SetChildIndex(ts, index);
            }
            ts.Visible = reader.ReadBoolean();
            ts.Location = new System.Drawing.Point(x, y);

            pass2.Add(new Pass2ToolStripDescriptor(o, new Pass2Control(Pass2ToolStrip), index, new System.Drawing.Point(x, y)));
        }

        List<Pass2ToolStripDescriptor> reorderstrips;
        void Pass2ToolStrip(Pass2Descriptor pass)
        {
            Pass2ToolStripDescriptor pass2 = pass as Pass2ToolStripDescriptor;
            ToolStrip ts = pass2.Object as ToolStrip;
            if (ts.Parent!=null)
                ts.Parent.Controls.SetChildIndex(ts, pass2.Index);
            ts.Location = pass2.Location;

            if (ts.Parent is ToolStripPanel)
                reorderstrips.Add(pass2);
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
        #endregion
    }
}
