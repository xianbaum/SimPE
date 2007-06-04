using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using SimPe.Interfaces.Plugin.Scanner;
using SimPe.Interfaces;
using SimPe.Interfaces.Files;
using SimPe.Interfaces.Scenegraph;
using SimPe.Plugin.Scanner;

namespace SimPe.Plugin.CollWsp
{
  public partial class CollWspForm : Form
  {
    /// <summary>
    /// Start the scanning
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Scan( string folder, bool rec, CollWspScannerCollection usedscanners )
    {

      //scan all Files
      pb.Value = 0;
      string[] files = System.IO.Directory.GetFiles( folder, "*.package" );

      int ct = files.Length;
      Scan( files, true, 0, ct, usedscanners );
      pb.Value = 0;

      //issue a recursive Scan
      if ( rec )
      {
        string[] dirs = System.IO.Directory.GetDirectories( folder, "*" );
        foreach ( string dir in dirs )
          Scan( dir, true, usedscanners );
      }

    }

    /// <summary>
    /// Start the scanning
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Scan( object sender, EventArgs e )
    {

      errorlog = "";

      resListView.Items.Clear();
      resListView.Columns.Clear();
      resCollection.Clear();
      ResetFilters();

      resListView.BeginUpdate();
      WaitingScreen.Wait();

      try
      {
        if ( Helper.WindowsRegistry.UseCache )
          cachefile.LoadFiles();

        //Setup ListView
        //lv.SmallImageList = null;
        resListView.Refresh();
        SimPe.Plugin.Scanner.AbstractScanner.AddColumn( resListView, "Filename", 180 );
        SimPe.Plugin.Scanner.AbstractScanner.AddColumn( resListView, "Type", 80 );


        SimPe.Plugin.Scanner.AbstractScanner.AssignFileTable();
        //setup Scanners
        foreach ( IScanner s in fscanners )
          s.InitScan( this.resListView );

        //scan all Files
        Scan( folder, cbrec.Checked, fscanners );

        //finish Scanners
        foreach ( IScanner s in fscanners )
          s.FinishScan();
        SimPe.Plugin.Scanner.AbstractScanner.DeAssignFileTable();

        if ( includeMaxisCheckBox.Checked )
          ScanMaxis();
        try
        {
          if ( Helper.WindowsRegistry.UseCache && cachechg )
            cachefile.Save();
        }
        catch ( Exception ex )
        {
          Helper.ExceptionMessage( "", ex );
        }
      }
      catch ( Exception ex )
      {
        Helper.ExceptionMessage( ex );
      }
      finally
      {
        WaitingScreen.Stop();
        resListView.EndUpdate();
      }

      // Setup collListView to have the same column as resListView
      collListView.Columns.Clear();
      foreach ( ColumnHeader ch in resListView.Columns )
      {
        ColumnHeader cl = new ColumnHeader();
        cl.Text = ch.Text;
        collListView.Columns.Add( cl );
      }

      if ( errorlog.Trim() != "" )
        Helper.ExceptionMessage( new Warning( "Unreadable Files were found", errorlog ) );
    }

    /// <summary>
    /// Scan for all Files and display the Result
    /// </summary>
    /// <param name="files"></param>
    /// <param name="enabled"></param>
    /// <param name="pboffset"></param>
    /// <param name="count"></param>
    void Scan( string[] files, bool enabled, int pboffset, int count, CollWspScannerCollection usedscanners )
    {
      int ct = pboffset;
      foreach ( string file in files )
      {
        pb.Value = Math.Max( Math.Min( ( ( ct++ ) * pb.Maximum ) / count, pb.Maximum ), pb.Minimum );
        Application.DoEvents();
        try
        {
          //Load the Item from the cache (if possible)
          ScannerItem si = cachefile.LoadItem( file );
          si.PackageCacheItem.Enabled = enabled;

          if ( si.PackageCacheItem.Thumbnail != null )
            WaitingScreen.Update( si.PackageCacheItem.Thumbnail, si.PackageCacheItem.Name );
          else
            WaitingScreen.UpdateMessage( si.PackageCacheItem.Name );

          //determine Type
          SimPe.Cache.PackageType pt = si.PackageCacheItem.Type;
          foreach ( IIdentifier id in ScannerRegistry.Global.Identifiers )
          {
            if ( ( si.PackageCacheItem.Type != SimPe.Cache.PackageType.Unknown ) && ( si.PackageCacheItem.Type != SimPe.Cache.PackageType.Undefined ) )
              break;


            if ( ( si.PackageCacheItem.Type == SimPe.Cache.PackageType.Unknown ) || ( si.PackageCacheItem.Type == SimPe.Cache.PackageType.Undefined ) )
              si.PackageCacheItem.Type = id.GetType( si.Package );
          }

          if ( si.PackageCacheItem.Type != SimPe.Cache.PackageType.Cloth )
            continue;
          if ( pt != si.PackageCacheItem.Type )
            cachechg = true;

          //setup the ListView Item
          ListViewItem lvi = new ListViewItem();
          si.ListViewItem = lvi;
          lvi.Text = System.IO.Path.GetFileNameWithoutExtension( si.FileName );
          lvi.SubItems.Add( si.PackageCacheItem.Type.ToString() );

          if ( !si.PackageCacheItem.Enabled )
            lvi.ForeColor = Color.Gray;

          //run file through available scanners
          foreach ( IScanner s in usedscanners )
          {
            SimPe.Cache.PackageState ps = si.PackageCacheItem.FindState( s.Uid, true );
            if ( ps.State == SimPe.Cache.TriState.Null )
            {
              s.ScanPackage( si, ps, lvi );
              if ( ps.State != SimPe.Cache.TriState.Null )
                cachechg = true;
            }
            else
              s.UpdateState( si, ps, lvi );
          }

          //setup CustClothCollWspItem
          SimPe.PackedFiles.Wrapper.Cpf cpf = new SimPe.PackedFiles.Wrapper.Cpf();
          cpf.ProcessData( si.Package.FindFiles( Data.MetaData.GZPS )[0], si.Package );
          CustClothCollWspItem cwspi = new CustClothCollWspItem( si, cpf, lvi );
          lvi.Tag = cwspi;
          resCollection.Add( cwspi );
          resListView.Items.Add( lvi );


          Application.DoEvents();
        }
        catch ( Exception ex )
        {
          /*if (Helper.DebugMode) 
          {
            Helper.ExceptionMessage("", ex);
          } 
          else 
          {*/
          errorlog += file + ": " + ex.Message + Helper.lbr + "----------------------------------------" + Helper.lbr;
          //}
        }
      } //foreach			
    }

    /// <summary>
    /// Scan maxis objects
    /// </summary>
    private void ScanMaxis()
    {
      WaitingScreen.UpdateMessage( "Loading Maxis items" );
      FileTable.FileIndex.Load();

      try
      {

        IScenegraphFileIndexItem[] items = FileTable.FileIndex.FindFile( Data.MetaData.GZPS, true );
        int count = items.Length;
        pb.Value = 0;
        int ct = 0;
        //make sure it's cloth not skin
        foreach ( IScenegraphFileIndexItem item in items )
        {
          pb.Value = Math.Max( Math.Min( ( ( ct++ ) * pb.Maximum ) / count, pb.Maximum ), pb.Minimum );
          SimPe.PackedFiles.Wrapper.Cpf cpf = new SimPe.PackedFiles.Wrapper.Cpf();
          cpf.ProcessData( item );

          string o = cpf.GetSaveItem( "override0subset" ).StringValue;
          uint cat = cpf.GetSaveItem( "category" ).UIntegerValue;
          string name = cpf.GetSaveItem( "name" ).StringValue;

          if ( ( cpf.GetSaveItem( "type" ).StringValue.Trim().ToLower() == "skin" )
            && ( ( cat & (uint)Data.SkinCategories.Skin ) != (uint)Data.SkinCategories.Skin )
            && ( cat > 0 ) && ( name.Length > 0 )
            && ( !o.Contains( "hair" ) ) && ( !o.Contains( "bang" ) ) )
          {
            WaitingScreen.UpdateMessage( name );

            ListViewItem lvi = new ListViewItem( System.IO.Path.GetFileNameWithoutExtension( cpf.Package.FileName ) );
            ClothCollWspItem cwsitem = new ClothCollWspItem( cpf, lvi );
            lvi.SubItems.Add( "Cloth" );
            lvi.SubItems.Add( name );

            string g = CollWsp.CollWspUtils.GetGenderString( cwsitem.GetSaveItem( "gender" ).UIntegerValue );
            lvi.SubItems.Add( g );

            lvi.SubItems.Add( cwsitem.GetSolvedString( "age" ) );
            lvi.SubItems.Add( cwsitem.GetSolvedString( "category" ) );
            lvi.SubItems.Add( o );

            lvi.Tag = cwsitem;
            resCollection.Add( cwsitem );
            resListView.Items.Add( lvi );

          }
        }

        if ( errorlog.Trim() != "" )
          Helper.ExceptionMessage( new Warning( "Unreadable Files were found", errorlog ) );

      }
      catch ( Exception ex )
      {
        Helper.ExceptionMessage( "Error scanning maxis resource", ex );
      }
      finally
      {
        pb.Value = 0;
      }
    }

  }
}
