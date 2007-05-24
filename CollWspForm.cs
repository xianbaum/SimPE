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
    SimPe.Cache.PackageCacheFile cachefile;
    string errorlog;
    bool cachechg;

    //stores only needed scanners
    CollWspScannerCollection fscanners; 
    string flname;
    public string FileName
    {
      get
      {
        return flname;
      }
    }
    public CollWspForm()
    {
      InitializeComponent();

      this.cbfolder.SelectedIndex = 0;
      
      //load the Group Cache
      SimPe.Plugin.ScenegraphWrapperFactory.LoadGroupCache();

      //load cache if any
      cachefile = new SimPe.Cache.PackageCacheFile();
      try
      {
        cachefile.Load( SimPe.Cache.PackageCacheFile.CacheFileName );
      }
      catch ( Exception ex )
      {
        Helper.ExceptionMessage( "Unable to reload the Cache File.", ex );
      }

      sorter = new ColumnSorter();
      resListView.ListViewItemSorter = sorter;

      fscanners = new CollWspScannerCollection();
      //display the list of identifiers
			foreach (IIdentifier id in CollWspScannerRegistry.Global.Identifiers)
			{
				lbid.Items.Add(id.GetType().Name+" (version="+id.Version.ToString()+", index="+id.Index.ToString()+")");
			}

			//add the scanners to the Selection and show the Scanner Controls (if available)
			//SimPe.Plugin.Scanner.AbstractScanner.UpdateList finishcallback = new SimPe.Plugin.Scanner.AbstractScanner.UpdateList(this.UpdateList);
			//ArrayList uids = new ArrayList();
      foreach ( IScanner i in CollWspScannerRegistry.Global.Scanners )
      {
        string name = i.GetType().Name + " (version=" + i.Version.ToString() + ", uid=0x" + Helper.HexString( i.Uid ) + ", index=" + i.Index.ToString() + ")";
        if (name.ToLower().Contains("clothingscanner"))
        {
          this.lbscandebug.Items.Add( name );
          fscanners.Add(i);
        }
      }
    }

    ColumnSorter sorter;
    private void SortList( object sender, System.Windows.Forms.ColumnClickEventArgs e )
    {
      if ( sorter.CurrentColumn == e.Column )
      {
        if ( resListView.Sorting == SortOrder.Ascending )
          resListView.Sorting = SortOrder.Descending;
        else
          resListView.Sorting = SortOrder.Ascending;
      }
      else
      {
        sorter.CurrentColumn = e.Column;
        resListView.ListViewItemSorter = sorter;
      }
      sorter.Sorting = resListView.Sorting;
      resListView.Sort();
    }

    
    byte[] iconData = null;
    private void SelectIcon(object sender, EventArgs e)
    {
      OpenFileDialog openFileDialog1 = new OpenFileDialog();

      openFileDialog1.InitialDirectory = System.IO.Path.Combine( PathProvider.SimSavegameFolder, "Collections\\Icons" );
      openFileDialog1.Filter = "Image Files(*.BMP;*.JPG)|*.BMP;*.JPG";
      openFileDialog1.RestoreDirectory = true;

      if ( openFileDialog1.ShowDialog() == DialogResult.OK )
      {
        try
        {
          string iconFile = openFileDialog1.FileName;
          //TODO:check the image size - no more 32x32 px
          
          iconData = System.IO.File.ReadAllBytes( iconFile );
          collIconPictureBox.Image = Image.FromFile( iconFile );
        }
        catch ( Exception ex )
        {
          MessageBox.Show( "Error: Could not read icon image file from disk. Original error: " + ex.Message );
        }
      }

    }

    string folder;
    private void SelectFolder( object sender, System.EventArgs e )
    {
      if ( cbfolder.SelectedIndex == 0 )
      {
        folder = System.IO.Path.Combine( PathProvider.SimSavegameFolder, "Downloads" );
      }
      else
      {
        if ( fbd.SelectedPath == "" )
          fbd.SelectedPath = PathProvider.SimSavegameFolder;
        if ( fbd.ShowDialog() == DialogResult.OK )
          folder = fbd.SelectedPath;
      }
    }

    /// <summary>
    /// Scan folders routine called on Scanbutton press
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Scan( object sender, EventArgs e )
    {
      errorlog = "";
      cachechg = false;
      resListView.Items.Clear();
      resListView.Columns.Clear();
      //ilist.Images.Clear();

      resListView.BeginUpdate();
      WaitingScreen.Wait();
      try
      {
        if ( Helper.WindowsRegistry.UseCache )
          cachefile.LoadFiles();

        //Setup ListView
        resListView.SmallImageList = null;
        resListView.Refresh();
        SimPe.Plugin.Scanner.AbstractScanner.AddColumn( resListView, "Filename", 180 );
        SimPe.Plugin.Scanner.AbstractScanner.AddColumn( resListView, "Enabled", 60 );
        SimPe.Plugin.Scanner.AbstractScanner.AddColumn( resListView, "Type", 80 );

        SimPe.Plugin.Scanner.AbstractScanner.AssignFileTable();
        //setup Scanners
        foreach ( IScanner s in fscanners ) s.InitScan( this.resListView );

        //scan all Files
        Scan( folder, cbrec.Checked, fscanners );

        //finish Scanners
        foreach ( IScanner s in fscanners ) s.FinishScan();
        SimPe.Plugin.Scanner.AbstractScanner.DeAssignFileTable();

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

      if ( errorlog.Trim() != "" )
        Helper.ExceptionMessage( new Warning( "Unreadable Files were found", errorlog ) );
      
      // Setup collListView to have the same column as resListView
      collListView.Columns.Clear();
      foreach ( ColumnHeader ch in resListView.Columns )
      {
        ColumnHeader cl = new ColumnHeader();
        cl.Text = ch.Text;
        collListView.Columns.Add(cl);
      }
    }

    private void Scan( string folder, bool rec, CollWspScannerCollection usedscanners )
    {

      //scan all Files
      //pb.Value = 0;
      string[] files = System.IO.Directory.GetFiles( folder, "*.package" );
      string[] dfiles = System.IO.Directory.GetFiles( folder, "*.simpedis" );
      string[] dofiles = System.IO.Directory.GetFiles( folder, "*.packagedisabled" );
      string[] tfiles = System.IO.Directory.GetFiles( folder, "*.Sims2Tmp" );

      int ct = files.Length + dfiles.Length + dofiles.Length + tfiles.Length;
      Scan( files, true, 0, ct, usedscanners );
      Scan( dfiles, false, files.Length, ct, usedscanners );
      Scan( dofiles, false, files.Length + dfiles.Length, ct, usedscanners );
      Scan( tfiles, false, files.Length + dfiles.Length + dofiles.Length, ct, usedscanners );
      //pb.Value = 0;

      //issue a recursive Scan
      if ( rec )
      {
        string[] dirs = System.IO.Directory.GetDirectories( folder, "*" );
        foreach ( string dir in dirs )
          Scan( dir, true, usedscanners );
      }

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
        //pb.Value = Math.Max( Math.Min( ( ( ct++ ) * pb.Maximum ) / count, pb.Maximum ), pb.Minimum );
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
          foreach ( IIdentifier id in CollWspScannerRegistry.Global.Identifiers )
          {
            if ( ( si.PackageCacheItem.Type != SimPe.Cache.PackageType.Unknown ) && ( si.PackageCacheItem.Type != SimPe.Cache.PackageType.Undefined ) )
              break;


            if ( ( si.PackageCacheItem.Type == SimPe.Cache.PackageType.Unknown ) || ( si.PackageCacheItem.Type == SimPe.Cache.PackageType.Undefined ) )
              si.PackageCacheItem.Type = id.GetType( si.Package );
          }

          if ( pt != si.PackageCacheItem.Type )
            cachechg = true;
          
          // TODO: Change to depend on collection type
          if ( si.PackageCacheItem.Type == SimPe.Cache.PackageType.Cloth )
          {
            //setup the ListView Item
            ListViewItem lvi = new ListViewItem();
            si.ListViewItem = lvi;
            lvi.Text = System.IO.Path.GetFileNameWithoutExtension( si.FileName );
            lvi.SubItems.Add( si.PackageCacheItem.Enabled.ToString() );
            lvi.SubItems.Add( si.PackageCacheItem.Type.ToString() );
            lvi.Tag = si;
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

            resListView.Items.Add( lvi );
          }


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

    private void MoveToCollection( object sender, EventArgs e )
    {
      if ( resListView.SelectedItems.Count == 0 ) return;

      foreach ( ListViewItem lvi in resListView.SelectedItems )
      {
        MoveToCollection( lvi );
      }
    }

    private void MoveToCollection( ListViewItem lvi )
    {
      if ( collListView.FindItemWithText( lvi.Text ) == null )
      {
        ScannerItem si = (ScannerItem)lvi.Tag;
        ListViewItem ni = (ListViewItem)lvi.Clone();
        collListView.Items.Add( ni );
        resListView.Items.Remove( lvi );
        si.ListViewItem = ni;
      }
    }

    private void MoveAllToCollection( object sender, EventArgs e )
    {
      foreach ( ListViewItem lvi in resListView.Items )
      {
        MoveToCollection( lvi );
      }
    }

    private void RemoveFromCollection( ListViewItem lvi )
    {
      if ( resListView.FindItemWithText( lvi.Text ) == null )
      {
        ScannerItem si = (ScannerItem)lvi.Tag;
        ListViewItem ni = (ListViewItem)lvi.Clone();
        resListView.Items.Add( ni );
        si.ListViewItem = ni;
      }
      collListView.Items.Remove( lvi );
    }

    private void RemoveFromCollection( object sender, EventArgs e )
    {
      if ( collListView.SelectedItems.Count == 0 )
        return;

      foreach ( ListViewItem lvi in collListView.SelectedItems )
      {
        RemoveFromCollection( lvi );
      }
    }

    private void RemoveAllFromCollection( object sender, EventArgs e )
    {
      foreach ( ListViewItem lvi in collListView.Items )
      {
        RemoveFromCollection( lvi );
      }
    }

    /// <summary>
    /// Saves collection to a file
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void SaveCollection( object sender, EventArgs e )
    {
      //TODO: replace with dialog
      string filename = System.IO.Path.Combine( PathProvider.SimSavegameFolder, "Collections\\testCollection.package" );
      IPackageFile package = (IPackageFile)SimPe.Packages.GeneratableFile.CreateNew();
      //TODO: validate all data first
      //create collection resource first
      uint instance = Hashes.InstanceHash( filename );
      uint group = Data.MetaData.LOCAL_GROUP;

      IPackedFileDescriptor collpfd = new SimPe.Packages.PackedFileDescriptor();
      IPackedFileDescriptor strpfd = new SimPe.Packages.PackedFileDescriptor();
      IPackedFileDescriptor iconpfd = new SimPe.Packages.PackedFileDescriptor();
      
      iconpfd = SaveImageResource( group, 0x00000001, package );
      if ( iconpfd == null ) return;
      strpfd = SaveStrResource( group, 0x00000001, package );
      if ( strpfd == null ) return;
      SaveRefFileHeaderResource( group, instance, package, strpfd, iconpfd );
      collpfd = SaveCollectionResource( group, instance, package );
      if ( collpfd == null )
        return;

      // SaveListViewItems( group, instance, package, collpfd );
      
      package.Save( filename );
    }

    private void SaveListViewItems( uint group, uint instance, IPackageFile package, IPackedFileDescriptor collpfd )
    {
      int j = 0;
      foreach ( ListViewItem lvi in collListView.Items )
      {
        IPackedFileDescriptor pfd = package.NewDescriptor( Data.MetaData.REF_FILE, 0, group, instance+(uint)j+1 );
        
        SimPe.Plugin.RefFile reffile = new SimPe.Plugin.RefFile();
        reffile.ProcessData( pfd, package );

        IPackedFileDescriptor[] pfds = new IPackedFileDescriptor[3];
        pfds[0] = package.NewDescriptor( 0, 0, 0, 0 );
        pfds[1] = collpfd;
        ScannerItem si;
        si = (ScannerItem)lvi.Tag;
        pfds[2] = si.Package.FindFiles( Data.MetaData.GZPS )[0];

        reffile.Items = pfds;
        reffile.SynchronizeUserData();

        package.Add( pfd );

        IPackedFileDescriptor binpfd = package.NewDescriptor( 0x0C560F39, 0, group, instance + (uint)j + 1 );

        SimPe.PackedFiles.Wrapper.Cpf cpf = new SimPe.PackedFiles.Wrapper.Cpf();
        cpf.ProcessData( binpfd, package );

        for ( int i = 0; i < 7; i++ )
        {
          SimPe.PackedFiles.Wrapper.CpfItem item = new SimPe.PackedFiles.Wrapper.CpfItem();
          switch ( i )
          {
            case 0:
              item.Name = "iconidx";
              item.UIntegerValue = 0x00000000;
              break;
            case 1:
              item.Name = "stringsetidx";
              item.UIntegerValue = 0x00000000;
              break;
            case 2:
              item.Name = "binidx";
              item.UIntegerValue = 0x00000001;
              break;
            case 3:
              item.Name = "objectidx";
              item.UIntegerValue = 0x00000002;
              break;
            case 4:
              item.Name = "creatorid";
              item.StringValue = "00000000-0000-0000-0000-000000000000";
              break;
            case 5:
              item.Name = "sortindex";
              item.IntegerValue = (int)j;
              break;
            case 6:
              item.Name = "stringindex";
              item.UIntegerValue = 0x00000000;
              break;
          }
          cpf.AddItem( item );
        }
        cpf.SynchronizeUserData();
        package.Add( binpfd );

        j++;
      } //foreach
    }

    private void SaveRefFileHeaderResource( uint group, uint instance, IPackageFile package, IPackedFileDescriptor strpfd, IPackedFileDescriptor iconpfd )
    {
      IPackedFileDescriptor pfd = package.NewDescriptor( Data.MetaData.REF_FILE, 0, group, instance );

      SimPe.Plugin.RefFile reffile = new SimPe.Plugin.RefFile();
      reffile.ProcessData( pfd, package );
      
      IPackedFileDescriptor[] pfds = new IPackedFileDescriptor[2];
      pfds[0] = iconpfd;
      pfds[1] = strpfd;

      reffile.Items = pfds;
      reffile.SynchronizeUserData();

      package.Add( pfd );
    }

    /// <summary>
    /// Saves str header to collection resource
    /// </summary>
    /// <param name="group"></param>
    /// <param name="instance"></param>
    /// <param name="package"></param>
    private IPackedFileDescriptor SaveStrResource( uint group, uint instance, IPackageFile package )
    {
      try
      {
        IPackedFileDescriptor pfd = package.NewDescriptor( Data.MetaData.STRING_FILE, 0, group, instance );
       
        SimPe.PackedFiles.Wrapper.Str str = new SimPe.PackedFiles.Wrapper.Str();
        str.ProcessData( pfd, package );

        SimPe.PackedFiles.Wrapper.StrToken token = new SimPe.PackedFiles.Wrapper.StrToken( 0, (byte)Data.MetaData.Languages.English, collToolTipTextBox.Text, "" );
        str.Add( token );
        str.SynchronizeUserData();

        package.Add( pfd );
        return pfd;
      }
      catch ( Exception ex )
      {
        Helper.ExceptionMessage( "Error processing text resource", ex );
        return null;
      }
    }

    /// <summary>
    /// Saves icon to collection header
    /// </summary>
    /// <param name="group"></param>
    /// <param name="instance"></param>
    /// <param name="package"></param>
    private IPackedFileDescriptor SaveImageResource( uint group, uint instance, IPackageFile package )
    {
      try
      {
        IPackedFileDescriptor pfd = package.NewDescriptor( 0x856DDBAC, 0, group, instance );

        pfd.SetUserData( iconData, false );
        package.Add( pfd );
        return pfd;

      }
      catch ( Exception ex )
      {
        Helper.ExceptionMessage( "Error processing icon resource", ex );
        return null;
      }
    }
    /// <summary>
    /// Creates COLL resource in package with instance and group
    /// </summary>
    /// <param name="group"></param>
    /// <param name="instance"></param>
    /// <param name="package"></param>
    private IPackedFileDescriptor SaveCollectionResource( uint group, uint instance, IPackageFile package )
    {
      try
      {
        IPackedFileDescriptor pfd = package.NewDescriptor( 0x6C4F359D, 0, group, instance );
        package.Add( pfd );
        SimPe.PackedFiles.Wrapper.Cpf cpf = new SimPe.PackedFiles.Wrapper.Cpf();
        cpf.ProcessData( pfd, package );
        uint stringsetidx = 0x00000001; //TODO: assign txt list instance    

        for ( int i = 0; i < 7; i++ )
        {
          SimPe.PackedFiles.Wrapper.CpfItem item = new SimPe.PackedFiles.Wrapper.CpfItem();
          switch ( i )
          {
            case 0:
              item.Name = "type";
              item.StringValue = "clothing";
              break;
            case 3:
              item.Name = "creatorid";
              item.StringValue = "00000000-0000-0000-0000-000000000000";
              break;
            case 1:
              item.Name = "iconidx";
              item.UIntegerValue = 0x00000000;
              break;
            case 2:
              item.Name = "stringsetidx";
              item.UIntegerValue = stringsetidx;
              break;
            case 5:
              item.Name = "flags";
              item.UIntegerValue = 0x00000012;
              break;
            case 6:
              item.Name = "stringindex";
              item.UIntegerValue = 0x00000000;
              break;
            case 4:
              item.Name = "sortindex";
              item.IntegerValue = 0x000000FA;
              break;
          }
          cpf.AddItem( item );
        }

        cpf.SynchronizeUserData();
        return pfd;
      }
      catch ( Exception ex )
      {
        Helper.ExceptionMessage( "Error processing collection resource", ex );
        return null;
      }
    }
  }
}