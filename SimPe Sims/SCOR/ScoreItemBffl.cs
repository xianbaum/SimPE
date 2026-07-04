using System;
using System.Collections.Generic;
using System.Text;

namespace SimPe.PackedFiles.Wrapper.SCOR
{
    public partial class ScoreItemBffl : AScorItem
    {
        public ScoreItemBffl(ScorItem si) : base(si)
        {
            InitializeComponent();
            if (booby.ThemeManager.ThemedForms)
                booby.ThemeManager.Global.AddControl(this.rtbBffs);
            if (Helper.WindowsRegistry.UseBigIcons)
                this.rtbBffs.Font = new System.Drawing.Font(this.rtbBffs.Font.FontFamily, 12F);
        }

        internal void AddError()
        {
            this.rtbBffs.Text = "error reading file!";
        }
        internal void AddStart(int n)
        {
            this.rtbBffs.Text = Convert.ToString(n) + " Best Friends Forever pending\r\n";
            a = 0;
            cnt = n;
            vals = new ushort[n];
            frnds = new uint[n];
        }
        internal void AddSim(string s, uint sim)
        {
            this.rtbBffs.Text += "\r\n" + s;
            frnds[a] = sim;
        }
        internal void Addtyme(ushort t)
        {
            vals[a] = t;
            Boolset bby = t;
            int v = 0;
            //Boolset includes 0 so 15 is highest which is always 0
            if (bby[14])
            {
                v = 14;
                if (bby[9])
                {
                    v += 7;
                    for (int i = 0; i < 9; i++)
                    {
                        if (bby[i]) v += (i - 1);
                    }
                }
                else if (bby[8])
                {
                    v += 5;
                    for (int i = 0; i < 8; i++)
                    {
                        if (bby[i]) v += (i - 1);
                    }
                }
                else if (bby[7])
                {
                    v += 5;
                    for (int i = 0; i < 7; i++)
                    {
                        if (bby[i]) v += (i - 1);
                    }
                }
                else if (bby[6])
                {
                    v += 4;
                    for (int i = 0; i < 6; i++)
                    {
                        if (bby[i]) v += (i - 1);
                    }
                }
                else if (bby[5])
                {
                    v += 3;
                    for (int i = 0; i < 5; i++)
                    {
                        if (bby[i]) v += (i - 1);
                    }
                }
                else if (bby[4])
                {
                    v += 2;
                    for (int i = 0; i < 4; i++)
                    {
                        if (bby[i]) v += (i - 1);
                    }
                }
                else
                {
                    for (int i = 0; i < 14; i++)
                    {
                        if (bby[i]) v += (i - 1);
                    }
                }
                /*
                if (bby[0]) v += 1;// not normally used
                if (bby[1]) v += 1;// not normally used
                if (bby[2]) v += 1;
                if (bby[3]) v += 2;
                if (bby[4]) v += 4;
                if (bby[5]) v += 8;
                if (bby[6]) v += 16;
                if (bby[7]) v += 32;
                if (bby[8]) v += 64;
                */
                /*
                for (int i = 0; i < 14; i++)
                {
                    if (bby[i]) v += (i*2);
                }*/
            }
            else
            {
                for (int i = 0; i < 14; i++)
                {
                    if (bby[i]) v += 1;
                }
            }
            if (bby[14]) this.rtbBffs.Text += " : Amount Remaining " + Convert.ToString(v);// + " : (" + bby.ToString() + ")";
            else this.rtbBffs.Text += " : " + Convert.ToString(v) + " Hours Remaining";// +" : (" + bby.ToString() + ")";
            a++;
        }

        protected override void DoSetData(string name, System.IO.BinaryReader reader)
        {
            throw new Exception("SetData should not get called for Best Friends Forever!");
        }

        int a;
        byte[] stuf = new byte[] { 5, 2, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0x80, 0x3F, 1, 2, 0, 0, 0, 0, 0, 0 };
        byte pendi = 1;
        int cnt;
        ushort[] vals;
        uint[] frnds;
        internal override void Serialize(System.IO.BinaryWriter writer, bool last)
        {
            string s = "Best Friend Forever List";
            StreamHelper.WriteString(writer, s);
            writer.Write(cnt);
            for (int i = 0; i < cnt; i++)
            {
                writer.Write(pendi);
                writer.Write(frnds[i]);
                writer.Write(stuf);
                writer.Write(vals[i]);
            }
        }
    }
}
