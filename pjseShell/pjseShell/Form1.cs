using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace pjseShell
{
    public partial class pjseShell : Form
    {
        public pjseShell()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            CancelEventArgs ex = new CancelEventArgs(false);
            Application.Exit(ex);
            if (ex.Cancel)
                MessageBox.Show("Cancelled");
        }
    }
}