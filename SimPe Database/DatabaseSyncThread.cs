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
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace SimPe.Database
{
    /// <summary>
    /// Builds/refreshes the cache on a single background thread.
    /// </summary>
    class DatabaseSyncThread
    {
        readonly Database db;
        Thread worker;
        volatile bool cancel;
        int done;

        public int Total { get; private set; }
        public int Done { get { return done; } }
        public bool Running { get { return worker != null && worker.IsAlive; } }

        public DatabaseSyncThread(Database db)
        {
            this.db = db;
        }

        public void Start()
        {
            cancel = false;
            done = 0;
            Total = 0;
            worker = new Thread(Run);
            worker.Name = "SimPe DB Sync";
            worker.IsBackground = true;
            worker.Start();
        }

        void Run()
        {
            try
            {
                Database.FileList list = new Database.FileList();
                foreach (SimPe.FileTableItem s in SimPe.FileTable.DefaultFolders)
                {
                    if (cancel) return;
                    db.LoadUpdateableFiles(list, s);
                }
                Total = list.Count;

                foreach (string flname in list)
                {
                    if (cancel) break;
                    try { db.AddPackageFile(flname); }
                    catch (Exception ex) { Helper.ExceptionMessage(ex); }
                    Interlocked.Increment(ref done);
                }
            }
            catch (Exception ex)
            {
                Helper.ExceptionMessage(ex);
            }
        }

        public void Cancel()
        {
            cancel = true;
        }

        public void WaitForFinish()
        {
            Thread w = worker;
            if (w != null) w.Join();
        }
    }
}
