/***************************************************************************
 *   Copyright (C) 2007 by Peter L Jones                                   *
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
using System.Windows.Forms;
using System.Diagnostics;
using TrapKATEditor;

namespace TrapKATEditor.Updates
{
    public class Checker
    {
        static Checker()
        {
            // Should only be set to AskMe the first time through (although it might have been reset by the user)
            if (Settings.AutoUpdateChoice == 0)
            {
                DialogResult dr = MessageBox.Show(
                    L.G("UCAskMe")
                    , L.G("TrapKATEditor_UpdateSettings")
                    , MessageBoxButtons.YesNo, MessageBoxIcon.Question
                );
                if (dr.Equals(DialogResult.Yes))
                    Settings.UpdateAutomatically = true; // Daily
                else
                {
                    Settings.UpdateAutomatically = false; // Manual
                    MessageBox.Show(L.G("UCSaidNo")
                        , L.G("TrapKATEditor_UpdateSettings")
                    );
                }
            }
        }

        public static void Daily()
        {
            if ((Settings.AutoUpdateChoice == 1)
                && (DateTime.UtcNow.Date != Settings.LastUpdateTS.Date))
            {
                try { GetUpdate(true); }
                catch (ArgumentException) { }
                Settings.LastUpdateTS = DateTime.UtcNow; // Only the automated check updates this setting
            }
        }

        public static bool GetUpdate(bool autoCheck)
        {
            UpdateInfo ui = null;
            try { ui = new UpdateInfo(); }
            catch (System.Net.WebException we)
            {
                MessageBox.Show("URL: " + we.Response.ResponseUri
                    + "\r\n\r\n" + we.Message
                    + "\r\n\r\n" + L.G("UIWebException")
                    , L.G("TrapKATEditor_UpdateSettings")
                    , MessageBoxButtons.OK
                    , MessageBoxIcon.Exclamation);
                throw new ArgumentException();
            }

            if (UpdateApplicable(ui, autoCheck))
            {
                SkipPrompt sp = new SkipPrompt(autoCheck, ui.AvailableVersion, ui.UpdateURL);
                switch (sp.ShowDialog())
                {
                    case DialogResult.Yes: Process.Start(new ProcessStartInfo(ui.UpdateURL)); break;
                    case DialogResult.Cancel: Settings.LastIgnoredTS = DateTime.Parse(ui.AvailableVersion); break;
                }
                return true;
            }
            return false;
        }

        private static bool UpdateApplicable(UpdateInfo ui, bool autoCheck)
        {
            String ts = TrapKATEditor.Version.BuildTS;
#if DEBUG
            MessageBox.Show(
                "Update URL: " + Settings.AutoUpdateURL
                + "\r\n" + L.G("helpAboutVersion") + ": " + ts
                + "\r\n" + "offered version: " + ui.AvailableVersion
                + "\r\n" + "last ignored version: " + Settings.LastIgnoredTS
                , L.G("TrapKATEditor_UpdateSettings")
                );
#endif

            if (autoCheck && ui.AvailableVersion.CompareTo(Settings.LastIgnoredTS) <= 0)
                return false;

            if (ui.AvailableVersion.CompareTo(ts) <= 0)
                return false;

            return true;
        }
    }
}
