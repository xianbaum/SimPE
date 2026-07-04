/***************************************************************************
*   Copyright (C) 2005 by Ambertation                                     *
*   quaxi@ambertation.de                                                  *
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
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Windows.Forms.Design;

namespace Ambertation.Windows.Forms
{
    public class DockManagerDesigner : System.Windows.Forms.Design.ParentControlDesigner 
    {
        private DesignerVerbCollection actions;
        DockManager manager;


        public override void Initialize(IComponent component)
        {
            base.Initialize(component);
            manager = component as DockManager;

            // Hook up events
            ISelectionService s = (ISelectionService)GetService(
                typeof(ISelectionService));
            IComponentChangeService c = (IComponentChangeService)
                GetService(typeof(IComponentChangeService));
            s.SelectionChanged += new EventHandler(s_SelectionChanged);
            c.ComponentRemoving += new ComponentEventHandler(c_ComponentRemoving);
        }

        ~DockManagerDesigner()
        {
           
			ISelectionService s = (ISelectionService) GetService(
				typeof(ISelectionService));
			IComponentChangeService c = (IComponentChangeService)
				GetService(typeof(IComponentChangeService));

			// Unhook events
            s.SelectionChanged -= new EventHandler(s_SelectionChanged);
			c.ComponentRemoving -= new ComponentEventHandler(
                c_ComponentRemoving);					
        }
        
        void s_SelectionChanged(object sender, EventArgs e)
        {
            
        }

        void c_ComponentRemoving(object sender, ComponentEventArgs e)
        {
            
        }

        public override System.ComponentModel.Design.DesignerVerbCollection Verbs
        {
            get
            {
                if (actions == null)
                {
                    actions = new DesignerVerbCollection();
                    if (manager != null)
                    {
                        actions.Add(new DesignerVerb("&Add Container", new EventHandler(AddContainer)));                                                
                    }
                }

                return actions;
            }
        }        

        private void AddPanel(object sender, System.EventArgs e)
        {
            DockPanel pn;
            IDesignerHost h = (IDesignerHost)GetService(typeof(IDesignerHost));

            DesignerTransaction dt;
            IComponentChangeService c = (IComponentChangeService)
                GetService(typeof(IComponentChangeService));

            // Add a new panel to the collection            
            dt = h.CreateTransaction("Add Panel");
            pn = (DockPanel)h.CreateComponent(typeof(DockPanel));
            pn.SetManager(manager);

            InitializeNewComponent(null);
            pn.Parent = manager;            
        }

        private void AddContainer(object sender, System.EventArgs e)
        {
            DockContainer pn;
            IDesignerHost h = (IDesignerHost)GetService(typeof(IDesignerHost));

            DesignerTransaction dt;

            // Add a new panel to the collection            
            dt = h.CreateTransaction("Add Container");
            pn = (DockContainer)h.CreateComponent(typeof(DockContainer));
            pn.SetManager(manager);
            pn.Dock = DockStyle.Left;
            IDictionary np = new Dictionary<string, object>();            
            np.Add("Dock", DockStyle.Right);
            InitializeNewComponent(np);
            
            manager.Controls.Add(pn);
            dt.Commit();
        }	
    }
}
