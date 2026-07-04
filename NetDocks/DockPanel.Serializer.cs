using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Ambertation.Windows.Forms
{
    partial class DockPanel
    {
        internal void Serialize(BinaryWriter writer)
        {
            DoSerialize(writer);
        }

        protected virtual void DoSerialize(BinaryWriter writer)
        {
            writer.Write(last.Pos.X);
            writer.Write(last.Pos.Y);
            writer.Write(last.Floating);
            if (last.Container != null) writer.Write(last.Container.Name);
            else writer.Write("");

            writer.Write(Visible);
            writer.Write(Collapsed);
            writer.Write(IsOpen);
            writer.Write(Width);
            writer.Write(Height);
            if (Floating)
            {
                writer.Write(ParentForm.Left);
                writer.Write(ParentForm.Top);
            }
            else
            {
                writer.Write(Left);
                writer.Write(Top);
            }
            if (this.Parent != null)
            {
                writer.Write(Parent.Name);
                writer.Write(Parent.Controls.GetChildIndex(this));
            }
            else
            {
                writer.Write("");
                writer.Write((int)0);
            }
        }

        internal void Deserialize(BinaryReader reader, Dictionary<string, DockManager.DockContainerDescriptor> docks, uint ver)
        {
            DoDeserialize(reader, docks, ver);
        }

        protected virtual void DoDeserialize(BinaryReader reader, Dictionary<string, DockManager.DockContainerDescriptor> docks, uint ver)
        {
            LastOpenState l = new LastOpenState();
            if (ver >= 6)
            {
                int px = reader.ReadInt32();
                int py = reader.ReadInt32();
                l.Pos = new System.Drawing.Point(px, py);
                l.Floating = reader.ReadBoolean();
                string pname = reader.ReadString();
                if (docks.ContainsKey(pname))                
                    l.Container = docks[pname].Container;                
            }
            Visible = reader.ReadBoolean();
            bool collaps = reader.ReadBoolean();
            bool open = reader.ReadBoolean();
            

            Width = reader.ReadInt32();
            Height = reader.ReadInt32();
            int left = reader.ReadInt32();
            int top = reader.ReadInt32();

            string name = reader.ReadString();
            int index = reader.ReadInt32();

            if (docks.ContainsKey(name))
            {
                this.Close();
                last = l;
                last.Container = docks[name].Container;
                if (open) this.Open();
                //Visible = !collaps;
            }
            else
            {
                last = l;
                Visible = true;
                if (open)  Float(new System.Drawing.Point(left, top));
            }
            
        }
    }
}
