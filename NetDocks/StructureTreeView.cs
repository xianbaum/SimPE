using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Ambertation.Windows.Forms.Debug
{
    public partial class StructureTreeView : UserControl
    {
        MyLayerdForm lf;

        class MyLayerdForm : LayeredForm
        {
            public MyLayerdForm(System.Drawing.Color cl)
                :base(cl, new System.Drawing.Size(2048, 2048))
            {
            }

            protected override void OnCreateBitmap(System.Drawing.Graphics g, System.Drawing.Bitmap bmp)
            {
                int sz = Math.Max(bmp.Width, bmp.Height);
                int step = 20;
                Pen p = new Pen(Color.White, 2);
                for (int i = step; i < 2* sz; i += step)
                {
                    g.DrawLine(p, new Point(i, -5), new Point(-5, i));
                    //g.DrawLine(p, new Point(i, bmp.Height+5), new Point(bmp.Width+5, i));
                }
                p = new Pen(Color.White, 5);
                g.DrawRectangle(p, 0, 0, bmp.Width-1, bmp.Height-1);
                p = new Pen(Color.Black, 1);
                g.DrawRectangle(p, 0, 0, bmp.Width-1, bmp.Height-1);
            }
        }

        class ContainerNode : TreeNode
        {
            DockContainer dc;
            public ContainerNode(DockContainer c)
            {
                dc = c;
                this.Text = c.Name + " (" + c.GetType().Name + ") - " + c.Dock + " " + c.Visible + " " + c.Location + " " + c.Size;
            }

            public DockContainer DockContainer
            {
                get { return dc; }
            }
        }

        class PanelNode : TreeNode
        {
            DockPanel dp;
            public PanelNode(DockPanel c)
            {
                dp = c;
                this.Text = c.TabText + ", " + c.CaptionText + ", " + c.Name + " (" + c.GetType().Name + ") - " + c.Dock + " " + c.Visible + " " + c.Location + " " + c.Size;
            }

            public DockPanel DockPanel
            {
                get { return dp; }
            }
        }

        class BarNode : TreeNode
        {
            DockButtonBar dbb;
            public BarNode(DockButtonBar c)
            {
                dbb = c;
                this.Text = c.Name + " (" + c.GetType().Name + ") - " + c.Dock + " " + c.Visible + " " + c.Location + " " + c.Size;
            }

            public DockButtonBar DockButtonBar
            {
                get { return dbb; }
            }
        }

        public StructureTreeView(DockManager manager)
        {
            InitializeComponent();
            lf = new MyLayerdForm(System.Drawing.Color.FromArgb(0x90, System.Drawing.Color.Red));
            lf.Visible = false;
            TreeNode mainnode = new TreeNode(manager.Name);
            AddNodes(mainnode, manager);

            this.tv.Nodes.Add(mainnode);
            this.tv.ExpandAll();
        }

        ~StructureTreeView(){
            HideOverlay();
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            if (!Visible) HideOverlay();
        }

        public void HideOverlay()
        {
            lf.Hide();
        }
        

              

        void AddNodes(TreeNode parent, DockContainer main)
        {
            foreach (Control c in main.Controls)
            {
                DockPanel dp = c as DockPanel;
                DockContainer dc = c as DockContainer;
                DockButtonBar dbb = c as DockButtonBar;

                if (dbb != null)
                {
                    parent.Nodes.Add(new BarNode(dbb));
                }
                else if (dp != null)
                {
                    parent.Nodes.Add(new PanelNode(dp));
                }
                else if (dc != null)
                {
                    TreeNode node = new ContainerNode(dc);
                    parent.Nodes.Add(node);
                    AddNodes(node, dc);
                }

            }
        }

        Form f;
        void RegForm(Form f)
        {
            this.f = f;
            f.FormClosed += new FormClosedEventHandler(f_FormClosed);
        }

        void UnRegForm()
        {
            f.FormClosed -= new FormClosedEventHandler(f_FormClosed);
        }
        public static void Execute(DockManager manager)
        {
            Form f = new Form();
            f.Text = manager.Name + " Structure";
            f.TopMost = true;
            Ambertation.Windows.Forms.Debug.StructureTreeView tv = new Ambertation.Windows.Forms.Debug.StructureTreeView(manager);
            f.Controls.Add(tv);
            tv.Dock = DockStyle.Fill;
            tv.RegForm(f);
            
            f.Show();            
        }

        void f_FormClosed(object sender, FormClosedEventArgs e)
        {
            HideOverlay();
            UnRegForm();
        }

        private void tv_AfterSelect(object sender, TreeViewEventArgs e)
        {
            ContainerNode cn = e.Node as ContainerNode;
            PanelNode pn = e.Node as PanelNode;
            BarNode bn = e.Node as BarNode;
            if (cn != null)
            {
                lf.Location = cn.DockContainer.ScreenLocation;
                lf.Size = cn.DockContainer.Size;
                lf.Show();
            }
            else if (pn != null)
            {
                lf.Location = pn.DockPanel.Parent.PointToScreen(pn.DockPanel.Location);
                lf.Size = pn.DockPanel.Size;
                lf.Show();
            }
            else if (bn != null)
            {
                lf.Location = bn.DockButtonBar.Parent.PointToScreen(bn.DockButtonBar.Location);
                lf.Size = bn.DockButtonBar.Size;
                lf.Show();
            }
            else
            {
                HideOverlay();
            }
        }

        
    }
}
