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
using System.ComponentModel;
using System.Drawing.Design;

namespace SimPe
{
	/// <summary>
	/// This is used to display Paths in the Options Dialog
	/// </summary>
    public class PathSettings : SimPe.GlobalizedObject
	{
		Registry r;
        static PathSettings ps;
        public static PathSettings Global
        {
            get
            {
                if (ps == null) { ps = CreateInstance(); }
                return ps;
            }
        }
       
        static PathSettings CreateInstance()
        {
            return new PathSettings(Helper.WindowsRegistry);
        }

		protected PathSettings(Registry r)
		{
			this.r = r;
		}

        protected string GetPath(ExpansionItem ei)
        {
            if (ei.InstallFolder == null) return ei.RealInstallFolder;
            if (ei.InstallFolder.Trim() == "") return ei.RealInstallFolder;
            return ei.InstallFolder;
        }

		protected string GetPath(string userpath, string defpath)
		{
			if (userpath==null) userpath="";
			if (userpath.Trim()=="") return defpath;
			return userpath;
		}

		[Category("BaseGame"),System.ComponentModel.Editor(typeof(SimPe.SelectSimFolderUITypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
		public string SaveGamePath
		{
			get
			{
                return GetPath(PathProvider.SimSavegameFolder, PathProvider.RealSavegamePath);
			}
            set { PathProvider.SimSavegameFolder = value; }
		}

        protected override PropertyDescriptorCollection GetBaseProperties()
        {
            return AppendExpansionProperties(base.GetBaseProperties());
        }

        protected override PropertyDescriptorCollection GetBaseProperties(Attribute[] attributes)
        {
            return AppendExpansionProperties(base.GetBaseProperties(attributes));
        }

        static PropertyDescriptorCollection AppendExpansionProperties(PropertyDescriptorCollection baseProps)
        {
            List<PropertyDescriptor> props = new List<PropertyDescriptor>();
            foreach (PropertyDescriptor pd in baseProps)
                props.Add(pd);
            foreach (ExpansionItem ei in PathProvider.Global.Expansions)
                props.Add(new ExpansionPathPropertyDescriptor(ei));
            return new PropertyDescriptorCollection(props.ToArray());
        }

        sealed class ExpansionPathPropertyDescriptor : PropertyDescriptor
        {
            readonly ExpansionItem ei;

            internal ExpansionPathPropertyDescriptor(ExpansionItem ei)
                : base(ei.ShortId + "Path", BuildAttributes(ei))
            {
                this.ei = ei;
            }

            static Attribute[] BuildAttributes(ExpansionItem ei)
            {
                string description = SimPe.Localization.GetString("[Description:]")
                    .Replace("{LongName}", ei.Name).Trim();
                return new Attribute[]
                {
                    new CategoryAttribute(ei.Flag.Class.ToString()),
                    new DisplayNameAttribute(ei.NameSortNumber + ": " + ei.NameShorter),
                    new DescriptionAttribute(description),
                    new EditorAttribute(typeof(SelectSimFolderUITypeEditor), typeof(UITypeEditor)),
                };
            }

            public override Type ComponentType { get { return typeof(PathSettings); } }
            public override Type PropertyType { get { return typeof(string); } }
            public override bool IsReadOnly { get { return false; } }

            public override object GetValue(object component)
            {
                return ((PathSettings)component).GetPath(ei);
            }

            public override void SetValue(object component, object value)
            {
                ei.InstallFolder = value == null ? "" : value.ToString();
                OnValueChanged(component, EventArgs.Empty);
            }

            public override bool CanResetValue(object component) { return false; }
            public override void ResetValue(object component) { }
            public override bool ShouldSerializeValue(object component) { return false; }
        }
	}
}
