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
            writer.Write(Visible);
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

        internal void Deserialize(BinaryReader reader, Dictionary<string, DockManager.DockContainerDescriptor> docks)
        {
            DoDeserialize(reader, docks);
        }

        protected virtual void DoDeserialize(BinaryReader reader, Dictionary<string, DockManager.DockContainerDescriptor> docks)
        {
            Visible = reader.ReadBoolean();
            bool open = reader.ReadBoolean();
            Visible = true;

            Width = reader.ReadInt32();
            Height = reader.ReadInt32();
            int left = reader.ReadInt32();
            int top = reader.ReadInt32();

            string name = reader.ReadString();
            int index = reader.ReadInt32();

            if (docks.ContainsKey(name))
            {
                this.Close();
                lastdock = docks[name].Container;
                if (open) this.Open();
            }
            else
            {
                if (open)  Float(new System.Drawing.Point(left, top));
            }
            
        }
    }
}
