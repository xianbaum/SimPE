/***************************************************************************
 *   Copyright (C) 2006 by Peter L Jones                                   *
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
 *   59 Temple Place - Suite 330, Boston, MA  32111-1307, USA.             *
 ***************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Collections;
using System.IO;
using System.Xml.XPath;

namespace pjse
{
    class AtomReader
    {
        static AtomReader()
        {
            String url = pjse.Settings.PJSE.AutoUpdateURL;
            String lastUpdate = pjse.Settings.PJSE.LastUpdateTS;

            String linkURL = "http://www.simlogical.com/PJSE/PJSElatest.htm";
            try
            {
                XPathDocument xpdoc = new XPathDocument(url);
                XPathNavigator xp = xpdoc.CreateNavigator();
                xp.MoveToRoot();
                xp.MoveToFirstChild();
                do
                {
                    //Find the first element.
                    if (xp.NodeType == XPathNodeType.Element)
                    {
                        //if children exist
                        if (xp.HasChildren == true)
                        {

                            //Move to the first child.
                            xp.MoveToFirstChild();

                            //Loop through all the children.
                            do
                            {
                                //Display the data.
                                System.Windows.Forms.MessageBox.Show("The XML string for this child is: " + xp.Value, xp.Name);

                                //Check for attributes.
                                if (xp.HasAttributes == true)
                                {
                                    System.Windows.Forms.MessageBox.Show("This node has attributes", xp.Name);
                                }
                            } while (xp.MoveToNext());
                        }
                    }
                } while (xp.MoveToNext());


                XPathNavigator xn = xp.SelectSingleNode("entry[0]/updated/text()");
                
                bool updated = false;
                String feedUpdate = lastUpdate;
                if (lastUpdate.CompareTo(xn.Value) < 0)
                {
                    updated = true;
                    feedUpdate = xn.Value;
                }

                if (updated)
                {
                    xn = xp.SelectSingleNode("/atom/entry[0]/link");
                    String updateLink = xn.GetAttribute("href", "href");
                    System.Windows.Forms.MessageBox.Show("Feed updated: " + feedUpdate
                        + "\r\n" + "Update link: " + updateLink);
                }
            }
            catch (System.Net.WebException e)
            {
                System.Windows.Forms.MessageBox.Show(url + "\r\n" + e.Message);
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message, e.GetType().FullName);
            }
        }

        public static bool AutoUpdate { get { return false; } }
    }

}
