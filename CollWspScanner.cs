/***************************************************************************
 *   Copyright (C) 2007 by vonLeeb & Ambertation                           *
 *   quaxi@ambertation.de                     
 *   vonleebpl@gmail.com                                                   *
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
using SimPe.Cache;
using SimPe.PackedFiles.Wrapper;
using System.Collections;
using System.Windows.Forms;
using System.Drawing;
using SimPe.Interfaces.Plugin.Scanner;

namespace SimPe.Plugin.Scanner
{
  /// <summary>
  /// This class is retriving details of body cloth only (no accessories and hair)
  /// Use mainly by collection workshop plugin
  /// </summary>
  internal class BodyClothScanner : AbstractScanner, IScanner
  {
    public BodyClothScanner() : base()
    {
    }

    private string n; //name
    private string o; //override0subset

    #region IScannerBase Member
    public uint Version
    {
      get
      {
        return 1;
      }
    }

    public int Index
    {
      get
      {
        return 1200;
      }
    }
    #endregion

    #region IScanner Member

    protected override void DoInitScan()
    {
      AbstractScanner.AddColumn( ListView, "Name", 150 );
      AbstractScanner.AddColumn( ListView, "Gender", 80 );
      AbstractScanner.AddColumn( ListView, "Ages", 150 );
      AbstractScanner.AddColumn( ListView, "Categories", 150 );
      AbstractScanner.AddColumn( ListView, "Cloth type", 80 );
    }


    public void ScanPackage( ScannerItem si, SimPe.Cache.PackageState ps, System.Windows.Forms.ListViewItem lvi )
    {
      if ( si.PackageCacheItem.Type == SimPe.Cache.PackageType.Cloth )
      {
        ps.State = TriState.True;
      }
      else
      {
        ps.State = TriState.False;
      }

      UpdateState( si, ps, lvi );
    }

    public void UpdateState( ScannerItem si, SimPe.Cache.PackageState ps, System.Windows.Forms.ListViewItem lvi )
    {
      AbstractScanner.SetSubItem( lvi, this.StartColum + 1, "" );

      System.Drawing.Color cl = lvi.ForeColor;
      if ( ps.State == TriState.True )
      {
        uint g = 0;
        uint c = 0;
        uint a = 0;
        string n = "";
        string o = "";

        Interfaces.Files.IPackedFileDescriptor[] pfds = si.Package.FindFiles( Data.MetaData.GZPS );

        ArrayList data = new ArrayList();
        foreach ( Interfaces.Files.IPackedFileDescriptor pfd in pfds )
        {
          SimPe.PackedFiles.Wrapper.Cpf cpf = new Cpf();
          cpf.ProcessData( pfd, si.Package, false );

          a = cpf.GetSaveItem( "age" ).UIntegerValue;
          c = cpf.GetSaveItem( "category" ).UIntegerValue;
          g = cpf.GetSaveItem( "gender" ).UIntegerValue;
          n = cpf.GetSaveItem( "name" ).StringValue;
          o = cpf.GetSaveItem( "override0subset" ).StringValue;
        }
        if ( n.Length == 0 )
          cl = System.Drawing.Color.Red;
        AbstractScanner.SetSubItem( lvi, this.StartColum, n, cl );

        string gender = CollWsp.CollWspUtils.GetGenderString(g);
        AbstractScanner.SetSubItem( lvi, this.StartColum+1, gender, cl );

        string age = "";
        Data.Ages[] ages = (Data.Ages[])System.Enum.GetValues( typeof( Data.Ages ) );
        foreach ( Data.Ages ag in ages )
        {
          if ( ( a & (uint)ag ) != 0 )
          {
            if ( age != "" )
              age += ", ";
            age += ag.ToString();
          }
        }
        if ( a == 0 )
          cl = System.Drawing.Color.Red;
        AbstractScanner.SetSubItem( lvi, this.StartColum+2, age, cl );

        string category = "";
        Data.SkinCategories[] cats = (Data.SkinCategories[])System.Enum.GetValues( typeof( Data.SkinCategories ) );
        foreach ( Data.SkinCategories cat in cats )
        {

          if ( ( c & (uint)cat ) != 0 )
          {
            if ( category != "" )
              category += ", ";
            category += cat.ToString();
          }
        }
        if ( c == 0 )
          cl = System.Drawing.Color.Red;
        AbstractScanner.SetSubItem( lvi, this.StartColum + 3, category, cl );

        if (o.Length == 0)
          cl = System.Drawing.Color.Red;
        AbstractScanner.SetSubItem( lvi, this.StartColum + 4, o, cl );
      }
    }

    public void FinishScan()
    {
    }

    public override bool IsActiveByDefault
    {
      get
      {
        return false;
      }
    }

    #endregion

    public override string ToString()
    {
      return "Body Cloth Scanner";
    }
  }

   internal class ObjectExtendedScanner : AbstractScanner, IScanner
   {
      public ObjectExtendedScanner() : base()
      { }

      #region IScannerBase Member
      public uint Version
      {
         get
         {
            return 1;
         }
      }

      public int Index
      {
         get
         {
            return 1300;
         }
      }
      #endregion
      
      #region IScanner Member

      protected override void DoInitScan()
      {
         AbstractScanner.AddColumn( ListView, "Object Type", 80 );
         AbstractScanner.AddColumn( ListView, "Room", 80 );
         AbstractScanner.AddColumn( ListView, "Function", 80 );
         AbstractScanner.AddColumn( ListView, "Sub Function", 80 );
      }


      public void ScanPackage(ScannerItem si, SimPe.Cache.PackageState ps, System.Windows.Forms.ListViewItem lvi)
      {
         if ( si.PackageCacheItem.Type == SimPe.Cache.PackageType.Object )
         {
            ps.State = TriState.True;
         }
         else
         {
            ps.State = TriState.False;
         }

         UpdateState( si, ps, lvi );
      }

      public void UpdateState(ScannerItem si, SimPe.Cache.PackageState ps, System.Windows.Forms.ListViewItem lvi)
      {
         AbstractScanner.SetSubItem( lvi, this.StartColum + 1, "" );
         
         System.Drawing.Color cl = lvi.ForeColor;
         if ( ps.State == TriState.True )
         {
            Interfaces.Files.IPackedFileDescriptor[] pfds = si.Package.FindFiles( Data.MetaData.OBJD_FILE );

            string t = "";
            uint inst = 0xFFFFFFFF;
            uint subtype = 0;
            SimPe.PackedFiles.Wrapper.ObjRoomSort r = null;
            SimPe.PackedFiles.Wrapper.ObjFunctionSort f = null;
            foreach ( Interfaces.Files.IPackedFileDescriptor pfd in pfds )
            {
               if ( pfd.Instance < inst )
               {
                  inst = pfd.Instance;
                  subtype = pfd.SubType;
               }
               if ( pfd.Instance == 0x41A7 || pfd.Instance == 0x41AF )
               {
                  ExtObjd objd = new ExtObjd();
                  objd.ProcessData( pfd, si.Package, false );
                  r = objd.RoomSort;
                  f = objd.FunctionSort;
                  t = objd.Type.ToString();
                  break;
               }
            }
            if ( t == "" )
            {
               //try to process pfd with lowest instance number
               Interfaces.Files.IPackedFileDescriptor[] pfdi = si.Package.FindFile( Data.MetaData.OBJD_FILE, subtype, inst );
               ExtObjd objd = new ExtObjd();

               objd.ProcessData( pfdi[0], si.Package, false );
               r = objd.RoomSort;
               f = objd.FunctionSort;
               t = objd.Type.ToString();
            }
            AbstractScanner.SetSubItem( lvi, this.StartColum, t, cl );

            //if ( a == 0 )
            //   cl = System.Drawing.Color.Red;
            AbstractScanner.SetSubItem( lvi, this.StartColum + 1, CollWsp.CollWspUtils.GetRoomString(r), cl );
            
            AbstractScanner.SetSubItem( lvi, this.StartColum + 2, CollWsp.CollWspUtils.GetFunctionString(f), cl );
            AbstractScanner.SetSubItem( lvi, this.StartColum + 3, "", cl );
         }
      }

      public void FinishScan()
      {
      }

      public override bool IsActiveByDefault
      {
         get
         {
            return false;
         }
      }

      #endregion

      public override string ToString()
      {
         return "Extended Object Scanner";
      }
   }
}
