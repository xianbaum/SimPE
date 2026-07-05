using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace pj
{
    public partial class CloneWhat : Form
    {
        public CloneWhat()
        {
            InitializeComponent();
            lHex32 = new List<TextBox>(new TextBox[] { tbValue, });
        }

        List<TextBox> lHex32 = null;
        private bool hex32_IsValid(object sender)
        {
            if (!(sender is TextBox) || lHex32.IndexOf((TextBox)sender) < 0)
                throw new Exception("hex32_IsValid not applicable to control " + sender.ToString());
            try { Convert.ToUInt32(((TextBox)sender).Text, 16); }
            catch (Exception) { return false; }
            return true;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool useGUID
        {
            get { return rg1_ByGUID.Checked; }
            set { rg1_ByGUID.Checked = true; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public uint Value
        {
            get { return Convert.ToUInt32(tbValue.Text, 16); }
            set { tbValue.Text = "0x" + SimPe.Helper.HexString(value); }
        }

        private void hex32_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (hex32_IsValid(sender)) return;

            e.Cancel = true;

            uint val = 0;
            switch (lHex32.IndexOf((TextBox)sender))
            {
                case 0: val = 0; break;
            }

            ((TextBox)sender).Text = "0x" + SimPe.Helper.HexString(val);
            ((TextBox)sender).SelectAll();
        }
    }
}