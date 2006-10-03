using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Ambertation.Windows.Forms
{
    partial class DockContainer
    {

        protected virtual void GetPanels(Dictionary<string , DockPanel> list)
        {
            foreach (DockPanel p in panels)
            {
                if (p.Name == "") p.Name = "myDockPanel_" + list.Count;
                list[p.Name] = p; 
            }

            foreach (DockContainer dc in containers)
                dc.GetPanels(list);
        }

        protected virtual void DoSerialize(BinaryWriter writer, ref int counter)
        {
            counter = FixName(counter);

            writer.Write(Name);
            if (Parent != null)
            {
                writer.Write(Parent.Name);
                writer.Write(Parent.Controls.GetChildIndex(this));
            }
            else
            {
                writer.Write("");
                writer.Write((int)0);
            }

            writer.Write((int)Dock);
            writer.Write(Collapsed);
            writer.Write(Width);
            writer.Write(Height);

            if (Highlight != null)            
                writer.Write(Highlight.Name);            
            else
                writer.Write("");            



            writer.Write(containers.Count);
            foreach (DockContainer dc in containers)
            {
                dc.DoSerialize(writer, ref counter);
            }
        }

        private int FixName(int counter)
        {
            if (Name == "")
            {
                Name = "myDockContainer_" + counter;
                counter++;
            }
            return counter;
        }

        protected void PrepareDeserialize()
        {
            for (int i = panels.Count - 1; i >= 0; i--)
                Controls.Remove(panels[i]);

            for (int i = containers.Count - 1; i >= 0; i--)
                containers[i].PrepareDeserialize();
                

            for (int i = containers.Count - 1; i >= 0; i--)
                Controls.Remove(containers[i]);
        }

        protected void DoDeserialize(BinaryReader reader, Dictionary<string, DockManager.DockContainerDescriptor> docks, DockContainer parent)
        {
            string name = reader.ReadString();
            string pname = reader.ReadString();
            int index = reader.ReadInt32();
            System.Windows.Forms.DockStyle dock = (System.Windows.Forms.DockStyle)reader.ReadInt32();
            bool collapsed = reader.ReadBoolean();
            int wd = reader.ReadInt32();
            int hg = reader.ReadInt32();
            string hname = reader.ReadString();

            
            if (!(this is DockManager)) 
            {
                this.SetNoCleanUpIntern(true);
                this.Name = name;

                this.Dock = dock;
                this.Width = wd;
                this.Height = hg;

                this.Parent = parent;
            }
            docks[name] = new DockManager.DockContainerDescriptor(this, index, collapsed, hname);


            int ct = reader.ReadInt32();
            for (int i = 0; i < ct; i++)
            {
                DockContainer dc = this.CreateNewContainer();
                dc.SetForceUseAsTarget(true);
                dc.Visible = false;
                dc.DoDeserialize(reader, docks, this);
            }
        }

        
    }
}
