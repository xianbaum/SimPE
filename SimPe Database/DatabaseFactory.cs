/***************************************************************************
 *   Copyright (C) 2005 by Ambertation                                     *
 *   quaxi@ambertation.de                                                  *
 *                                                                         *
 *   This program is free software; you can redistribute it and/or modify  *
 *   it under the terms of the GNU General Public License as published by  *
 *   the Free Software Foundation; either version 2 of the License, or     *
 *   (at your option) any later version.                                   *
 ***************************************************************************/
using System;
using SimPe.Interfaces;

namespace SimPe.Database
{
    /// <summary>
    /// Advertises the Database plugin's tools to SimPe's plugin loader.
    /// </summary>
    public class DatabaseFactory : SimPe.Interfaces.Plugin.AbstractWrapperFactory, SimPe.Interfaces.Plugin.IToolFactory
    {
        internal static IToolPlugin[] Last;

        public DatabaseFactory()
            : base()
        {
        }

        #region AbstractWrapperFactory Member
        public override SimPe.Interfaces.IWrapper[] KnownWrappers
        {
            get
            {
                IWrapper[] wrappers = { };
                return wrappers;
            }
        }
        #endregion

        #region IToolFactory Member
        public IToolPlugin[] KnownTools
        {
            get
            {
                if (Last != null) return Last;

                Last = new IToolPlugin[]{
                        new SimPe.Database.DatabaseDock()
                    };

                return Last;
            }
        }
        #endregion
    }
}
