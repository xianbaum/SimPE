using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SimPe.PackedFiles.Wrapper;
using SimPe.PackedFiles.UserInterface;

namespace pjse
{
    public partial class TtabAnimalMotiveWiz : Form
    {
        public TtabAnimalMotiveWiz()
        {
            InitializeComponent();

            this.Controls.Remove(this.label1);
            this.Controls.Remove(this.ttabSingleMotiveUI1);
            lastTop = ttabSingleMotiveUI1.Top;
        }

        private TtabItemAnimalMotiveItem item = null;

        public TtabItemAnimalMotiveItem MotiveSet
        {
            get { return item; }
            set
            {
                if (item != value)
                {
                    item = value;
                    setItem();
                }
            }
        }

        private int lastTop;
        private void setItem()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TtabAnimalMotiveWiz));
            this.SuspendLayout();

            foreach (Control c in this.Controls)
                if (c is TtabSingleMotiveUI )
                    this.Controls.Remove(c);

            for (int i = 0; i < item.Count; i++)
            {
                TtabSingleMotiveUI s = new TtabSingleMotiveUI();
                resources.ApplyResources(s, "ttabSingleMotiveUI1");
                s.Motive = item[i];
                s.Top = lastTop;
                this.Controls.Add(s);

                Label l = new Label();
                resources.ApplyResources(l, "label1");
                l.Text = "0x" + SimPe.Helper.HexString((short)i);
                l.Top = s.Top + 2;
                this.Controls.Add(l);

                lastTop += s.Height + 2;
            }
            this.Height = lastTop + 4 + 78;

            this.ResumeLayout();
        }

    }
}