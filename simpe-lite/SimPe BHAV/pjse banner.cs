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
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace pjse
{
    public partial class pjse_banner : UserControl
    {
        private string title = "file type";
        private string format = "PJSE: {label} Editor";
        private string helpTarget = "Contents";
        public pjse_banner()
        {
            InitializeComponent();
        }

        [Category("Appearance")]
        [DefaultValue("PJSE: {label} Editor")]
        [Description("The format of the PJSE banner title.")]
        [Localizable(true)]
        public string FormatText
        {
            get { return format; }
            set
            {
                format = value;
                lbLabel.Text = format.Replace("{label}", title);
            }
        }

        [Category("Appearance")]
        [DefaultValue("file type")]
        [Description("The file type edited by this PJSE plugin.")]
        [Localizable(true)]
        public string TitleText
        {
            get { return title; }
            set
            {
                title = value;
                lbLabel.Text = format.Replace("{label}", title);
            }
        }


        [Category("Appearance")]
        [DefaultValue("Float")]
        [Description("The label on the Float button.")]
        [Localizable(true)]
        public string FloatText { get { return btnFloat.Text; } set { btnFloat.Text = value; } }

        [Category("Behavior")]
        [DefaultValue(false)]
        [Description("True if the Float button should be visible.")]
        public bool FloatVisible { get { return btnFloat.Visible; } set { btnFloat.Visible = value; } }

        [Category("Action")]
        [Description("Raised when the Float button is clicked.")]
        public event EventHandler FloatClick;
        public virtual void OnFloatClick(object sender, EventArgs e) { if (FloatClick != null) { FloatClick(sender, e); } }

        /// <summary>
        /// Sets the CancelButton attribute of Form <paramref name="f"/> to the Float Button
        /// </summary>
        /// <param name="f">Form on which to set CancelButton attribute</param>
        public void SetFormCancelButton(Form form) { form.CancelButton = btnFloat; }


        [Category("Appearance")]
        [DefaultValue("RFT")]
        [Description("The label on the Refresh Filetable button.")]
        [Localizable(true)]
        public string RFTText { get { return btnRefreshFT.Text; } set { btnRefreshFT.Text = value; } }


        [Category("Appearance")]
        [DefaultValue("Help")]
        [Description("The label on the Help button.")]
        [Localizable(true)]
        public string HelpText { get { return btnHelp.Text; } set { btnHelp.Text = value; } }

        [Category("Behavior")]
        [DefaultValue("Contents")]
        [Description("The help file to display when the Help button is clicked.")]
        [Localizable(true)]
        public string HelpTarget { get { return helpTarget; } set { this.helpTarget = value; } }



        private void btnFloat_Click(object sender, EventArgs e) { OnFloatClick(sender, e); }

        private void btnRefreshFT_Click(object sender, EventArgs e) { SimPe.FileTable.Reload(); }

        private void btnHelp_Click(object sender, System.EventArgs e) { pjse.HelpHelper.Help(helpTarget); }
    }
}
