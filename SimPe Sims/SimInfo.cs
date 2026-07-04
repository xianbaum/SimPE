using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SimPe.PackedFiles.Wrapper
{
    public partial class SimInfo : Form
    {
        public SimInfo(Wrapper.ExtSDesc Sim, Image img)
        {
            InitializeComponent();
            if (booby.ThemeManager.ThemedForms) booby.ThemeManager.Global.AddControl(this.pngradient);
            this.pbImage.Image = img;
            if (Sim.CharacterDescription.IsWoman) this.pngradient.BackgroundImage = booby.PrettyGirls.BathTime;
            this.pnheader.HeaderText = Sim.SimName + "\'s Profile";
            this.lbshead.Text = Sim.SimName + " " + Sim.SimFamilyName + ",";
            if (Sim.FamilyInstance == 0)
                this.lbshead.Text += "\r\n  Not in any family";
            else
            {
                if (Sim.HouseholdName.EndsWith("Family", StringComparison.CurrentCultureIgnoreCase))
                    this.lbshead.Text += "\r\n  From a " + Sim.HouseholdName;
                else
                    this.lbshead.Text += "\r\n  From the " + Sim.HouseholdName + " family";
            }
            if (Sim.CharacterDescription.ServiceTypes != SimPe.Data.MetaData.ServiceTypes.Normal)
            {
                if (Sim.CharacterDescription.ServiceTypes == SimPe.Data.MetaData.ServiceTypes.Streaker && Sim.Apartment.SoldNooky > 0)
                    this.lbshead.Text += ",\r\n  A Prostitute";
                else if (Sim.CharacterDescription.ServiceTypes == SimPe.Data.MetaData.ServiceTypes.God && Sim.CharacterDescription.Gender == Data.MetaData.Gender.Female)
                    this.lbshead.Text += ",\r\n  A Godess";
                else if (Localization.GetString(Sim.CharacterDescription.ServiceTypes.ToString()).EndsWith("Delivery"))
                    this.lbshead.Text += ",\r\n  A " + Localization.GetString(Sim.CharacterDescription.ServiceTypes.ToString()) + " Person";
                else
                    this.lbshead.Text += ",\r\n  A " + Localization.GetString(Sim.CharacterDescription.ServiceTypes.ToString());
            }
            else if (Sim.CharacterDescription.Career != SimPe.Data.MetaData.Careers.Unemployed && Sim.CharacterDescription.Career != SimPe.Data.MetaData.Careers.Unknown)
            {
                string carear = "";
                carear += (Data.LocalizedCareers)Sim.CharacterDescription.Career;
                if (carear.StartsWith("Teen/Elder - ")) carear = carear.Substring(13);
                this.lbshead.Text += ",\r\n  Works in the " + carear + " career";
            }
            else if (Sim.CharacterDescription.Retired != SimPe.Data.MetaData.Careers.Unemployed && Sim.CharacterDescription.Retired != SimPe.Data.MetaData.Careers.Unknown && Sim.CharacterDescription.Realage > 19)
                this.lbshead.Text += ",\r\n  Retired from the " + (Data.LocalizedCareers)Sim.CharacterDescription.Retired + " career";
            else if (Sim.University.OnCampus == 0x1 && Sim.University.Major != SimPe.Data.Majors.Unset && Sim.University.Major != SimPe.Data.Majors.Unknown)
                this.lbshead.Text += ",\r\n  Studying " + (Data.Majors)Sim.University.Major;
            else if (Sim.CharacterDescription.Realage < 17 && Sim.CharacterDescription.SchoolType != SimPe.Data.MetaData.SchoolTypes.NoSchool && Sim.CharacterDescription.SchoolType != SimPe.Data.MetaData.SchoolTypes.Unknown)
            {
                if (Sim.CharacterDescription.Realage < 3)
                    this.lbshead.Text += ",\r\n  Will attend " + (Data.LocalizedSchoolType)Sim.CharacterDescription.SchoolType;
                else
                    this.lbshead.Text += ",\r\n  Attends " + (Data.LocalizedSchoolType)Sim.CharacterDescription.SchoolType;
                if (!this.lbshead.Text.EndsWith("School")) this.lbshead.Text += " School";
            }
            this.lbInform.Text = SimPe.PackedFiles.UserInterface.SimOriGuid.AboutSim(Sim);
        }
    }
}