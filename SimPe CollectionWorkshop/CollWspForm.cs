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
    
    string errorlog;
    bool cachechg;
    bool scanMaxis;
     bool identifying = false;
    //indicate if collection is edited or new
    bool newcoll;
    SimPe.Cache.PackageCacheFile cachefile;

    //stores only needed scanners
    CollWspScannerCollection fscanners = new CollWspScannerCollection();

    CollWspItems resCollection;
    CollWspItems collCollection;
    ListViewItem[] filteredCollection = { };
    Type categories = typeof( Data.SkinCategories );
    Type ages = typeof( Data.Ages );
    Type genders = typeof( CollWsp.Genders );
    Type rooms = typeof(Data.ObjRoomSortBits);
    Type functions = typeof(Data.ObjFunctionSortBits);
    ArrayList genderFilter;
    
    //collection, icon, string filled when collection is saved or edited from simpe
    SimPe.PackedFiles.Wrapper.Cpf collcpf = new SimPe.PackedFiles.Wrapper.Cpf();
    IPackedFileDescriptor collpfd;
    IPackedFileDescriptor strpfd;
    IPackedFileDescriptor iconpfd;
    bool iconChanged = false;

    string collType;

    private void InitFiltersLayout()
    {
      foreach ( string cat in System.Enum.GetNames( categories ) )
      {
        if ( !cat.Contains( "Casual" ) && !cat.Contains( "Skin" ) )
          categoryFilterListView.Items.Add( cat );
      }

      foreach ( string age in System.Enum.GetNames( ages ) )
        ageFilterListView.Items.Add( age );

     foreach ( string room in System.Enum.GetNames( rooms ) )
        roomFilterListView.Items.Add( room );

     foreach ( string func in System.Enum.GetNames( functions ) )
        functionFilterListView.Items.Add( func );

    }

    string flname;
    public string FileName
    {
      get
      {
        return flname;
      }
    }
     /// <summary>
     /// Initiate class data
     /// </summary>
     private void InitClass()
     {
        InitializeComponent();
        InitFiltersLayout();
        CheckForIllegalCrossThreadCalls = false;
        scanMaxis = false;

        //load the Group Cache
        SimPe.Plugin.ScenegraphWrapperFactory.LoadGroupCache();

        this.cbfolder.SelectedIndex = 0;
        
        this.resTabControl.Controls.Remove( this.objectFiltersTabPage );

        resCollection = new CollWspItems();
        collCollection = new CollWspItems();
        genderFilter = new ArrayList();
        //filteredCollection = new ArrayList();

        sorter = new ColumnSorter();
        resListView.ListViewItemSorter = sorter;

        cachefile = new SimPe.Cache.PackageCacheFile();
        try
        {
           cachefile.Load( SimPe.Cache.PackageCacheFile.CacheFileName );
        }
        catch ( Exception ex )
        {
           Helper.ExceptionMessage( "Unable to reload the Cache File.", ex );
        }

        
     }

     ColumnSorter sorter;
     private void SortList(object sender, System.Windows.Forms.ColumnClickEventArgs e)
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

     public CollWspForm(SimPe.Interfaces.Files.IPackageFile pkg)
    {
       InitClass();
       if ( pkg == null )
          InitNewColl();
       else
          InitEditColl(pkg);
       ResetScanners();
    }

     private void InitEditColl(SimPe.Interfaces.Files.IPackageFile pkg)
     {
        //not a new collection
        newcoll = false;
        flname = pkg.FileName;

        SimPe.Interfaces.Files.IPackedFileDescriptor[] pfds = pkg.FindFiles( 0x6C4F359D );
        if ( pfds.Length == 1 )
        {
           collpfd = pfds[0];
           
           SimPe.PackedFiles.Wrapper.Cpf cpf = new SimPe.PackedFiles.Wrapper.Cpf();
           cpf.ProcessData( collpfd, pkg );
           collType = cpf.GetSaveItem( "type" ).StringValue;
           AdjustCollTypeComboBox();
           PrepareCollColumns();

           SimPe.Interfaces.Files.IPackedFileDescriptor[] pfdsstr = pkg.FindFiles( Data.MetaData.STRING_FILE );
           if ( pfdsstr.Length > 0 )
           {
              strpfd = pfdsstr[0];
              SimPe.PackedFiles.Wrapper.Str str = new SimPe.PackedFiles.Wrapper.Str();
              SimPe.Data.MetaData.Languages deflang = Helper.WindowsRegistry.LanguageCode;
              str.ProcessData( strpfd, pkg );
              SimPe.PackedFiles.Wrapper.StrItemList items = str.LanguageItems( deflang );
              if ( items.Length > 0 )
                 collNameTextBox.Text = items[0].Title;
              else
              {
                 items = str.LanguageItems( 1 );
                 if ( items.Length > 0 )
                    collNameTextBox.Text = items[0].Title;
                 else
                    collNameTextBox.Text = "";
              }
           }

           SimPe.Interfaces.Files.IPackedFileDescriptor[] pfdsimg = pkg.FindFiles( 0x856DDBAC );
           if ( pfdsimg.Length > 0 )
           {
              iconpfd = pfdsimg[0];
              SimPe.PackedFiles.Wrapper.Picture pct = new SimPe.PackedFiles.Wrapper.Picture();
              pct.ProcessData( iconpfd, pkg );
              Image img = new Bitmap(pct.Image);
              
              collIconPictureBox.Image =img ;
              System.IO.MemoryStream ms = new System.IO.MemoryStream();
              collIconPictureBox.Image.Save( ms, System.Drawing.Imaging.ImageFormat.Bmp);
              iconData = ms.ToArray();
           }
        
           //now read items
           SimPe.Interfaces.Files.IPackedFileDescriptor[] refpfds = pkg.FindFiles( Data.MetaData.REF_FILE );
           foreach ( SimPe.Interfaces.Files.IPackedFileDescriptor refpfd in refpfds )
           {
              RefFile reffile = new RefFile();
              reffile.ProcessData( refpfd, pkg );
              if ( reffile.Items.Length>2 )
              {
                 foreach ( SimPe.Interfaces.Files.IPackedFileDescriptor item in reffile.Items )
                 {
                    if ( item.Type != 0 && item.Type != 0x6C4F359D )
                    {
                       int sortindex = 0;

                       SimPe.Interfaces.Files.IPackedFileDescriptor binxpfd = pkg.FindExactFile( 0x0C560F39, 0, reffile.FileDescriptor.Group, reffile.FileDescriptor.Instance, 0 );
                       if ( binxpfd != null )
                       {
                          SimPe.PackedFiles.Wrapper.Cpf cpfr = new SimPe.PackedFiles.Wrapper.Cpf();
                          cpfr.ProcessData( binxpfd, pkg );
                          sortindex = cpfr.GetSaveItem( "sortindex" ).IntegerValue;
                       }
                       if ( item.Type == 0x69DA3F9F || item.Type == 0xE9DA450E )
                       {
                          //object here
                          ListViewItem lvi = new ListViewItem();
                          if ( item.Type == 0x69DA3F9F )
                             lvi.Text = "Object";
                          else if ( item.Group == 1 )
                             lvi.Text = "Floor";
                          else
                             lvi.Text = "Wall";

                          ObjCollWspItem objitem = new ObjCollWspItem( reffile, sortindex, lvi );
                          lvi.SubItems.Add( objitem.Guid.ToString() );
                          lvi.Tag = objitem;
                          collListView.Items.Add( lvi );
                          collCollection.Add( objitem );
                       }
                       else
                       { 
                          //clothing here
                          ListViewItem lvi = new ListViewItem();
                          lvi.Text = "Cloth";
                          ClothCollWspItem clitem = new ClothCollWspItem( reffile, sortindex, lvi );
                          lvi.SubItems.Add( clitem.PackedFileDescriptor.Instance.ToString() );
                          lvi.Tag = clitem;
                          collListView.Items.Add( lvi );
                          collCollection.Add( clitem );
                       }
                    }
                 } 
              }
           }
           //sort according to sortindex
           int[] aKeys = new int[collListView.Items.Count];
           ListViewItem[] aLvi = new ListViewItem[collListView.Items.Count];
           int i = 0;
           foreach ( ListViewItem lvi in collListView.Items )
           {
              aKeys[i] = ( (ICollWspItem)lvi.Tag ).Sortindex;
              aLvi[i] = lvi;
              i++;
           }
           Array.Sort( aKeys, aLvi );
           collListView.Items.Clear();
           collListView.Items.AddRange( aLvi );
        }
     }

     private void invoke_Finished(object sender, EventArgs e)
     {
        UpdateItems();
        identifying = false;
     }

     /// <summary>
     /// Updates collection listview items when identifying items
     /// </summary>
     private void UpdateItems()
     {
        foreach ( ListViewItem lvi in collListView.Items )
        {
           if ( !( (ICollWspItem)lvi.Tag ).Identified )
           {
              foreach ( ListViewItem lvis in resListView.Items )
              {
                 if ( ( (ICollWspItem)lvi.Tag ).Equals( (ICollWspItem)lvis.Tag ) )
                 {
                    //identified
                    ( (ICollWspItem)lvis.Tag ).ExistingRefFile = ( (ICollWspItem)lvi.Tag ).ExistingRefFile;

                    lvi.Tag = lvis.Tag;
                    lvi.SubItems.Clear();
                    lvi.Text = lvis.Text;
                    int i = 0;
                    foreach ( ListViewItem.ListViewSubItem lvisb in lvis.SubItems )
                       if ( i == 0 )
                          i++;
                       else
                          lvi.SubItems.Add( lvisb );

                    break;
                 }
              }
           }
        }
     }

     private void IdentifyItems(object sender, EventArgs e)
     { 
        //check first if we need identifying at all
        if ( NeedsIdentify() )
        {
           identifying = true;
           //scan custom items first
           cbfolder.SelectedIndex = 0;
           cbrec.Checked = true;
           Scan( sender, e );

           UpdateItems();
        }
        if ( NeedsIdentify() )
        {
           //scan maxis items then
           cbfolder.SelectedIndex = 2;
           cbrec.Checked = false;
           scanMaxis = true;
           Scan( sender, e );

           UpdateItems();
        }
        else
           identifying = false;
     }

     private bool NeedsIdentify()
     {
        foreach ( ListViewItem lvi in collListView.Items )
        {
           if ( !( (ICollWspItem)lvi.Tag ).Identified )
              return true;
        }
        return false;
     }

     private void InitNewColl()
     {
        //init to clothing
        collType = "clothing";
        this.collTypeComboBox.SelectedIndex = 0;

        //new collection
        newcoll = true;
     }

     private void AdjustCollTypeComboBox()
     {
        string[] colls = { "clothing", "collection", "communitylotcollection", "lotcollection" };
        ArrayList arrcollections = new ArrayList( colls );
        collTypeComboBox.SelectedIndex = arrcollections.IndexOf( collType );
     }

     private void PrepareCollColumns()
     {
        collListView.Columns.Clear();
        collListView.Columns.Add( "Type" );
        if ( collType == "clothing" )
           collListView.Columns.Add( "Instance" );
        else
           collListView.Columns.Add( "Guid" );
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
          
          iconData = System.IO.File.ReadAllBytes( iconFile );
          Image tmpi = Image.FromFile( iconFile );
          collIconPictureBox.Image = tmpi;
          iconChanged = true;

          if ( (tmpi.Size.Height > 32) || (tmpi.Size.Width > 32) ) 
            MessageBox.Show( "This icon is too big, although visible in the game might not look as you expect." );
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
        scanMaxis = false;
      }
      else if ( cbfolder.SelectedIndex == 1 )
      {
         if ( fbd.SelectedPath == "" )
            fbd.SelectedPath = PathProvider.SimSavegameFolder;
         if ( fbd.ShowDialog() == DialogResult.OK )
            folder = fbd.SelectedPath;
         scanMaxis = false;
      }
      else
      { scanMaxis = true; }
   }

   #region items manipulation
   private void MoveToCollection( object sender, EventArgs e )
    {
      if ( resListView.SelectedItems.Count == 0 ) return;

      foreach ( ListViewItem lvi in resListView.SelectedItems )
        MoveToCollection( lvi );
    }

    private void MoveToCollection( ListViewItem lvi )
    {
      //Check if item exist in collection list already
      foreach ( ListViewItem lvii in collListView.Items )
      {
        if ( ( (ICollWspItem)lvi.Tag ).Equals( (ICollWspItem)lvii.Tag ) )
          return;
      }
      //not found, we can process request
      ICollWspItem cwspi = (ICollWspItem)lvi.Tag;
      ListViewItem ni = (ListViewItem)lvi.Clone();
      
      collListView.Items.Add( ni );
      resListView.Items.Remove( lvi );
      resCollection.Remove( cwspi );
      cwspi.ListViewItem = ni;
      collCollection.Add( cwspi );
    
    }

    private void MoveAllToCollection( object sender, EventArgs e )
    {
      foreach ( ListViewItem lvi in resListView.Items )
        MoveToCollection( lvi );
    }

    private void RemoveFromCollection( ListViewItem lvi )
    {
      //Check if item exist in collection list already
      bool found = false;
      foreach ( ListViewItem lvii in resListView.Items )
      {
        if ( ( (ICollWspItem)lvi.Tag ).Equals( (ICollWspItem)lvii.Tag ) )
        {
          found = true;
          break;
        }
      }
      //progress if not found
      if ( !found )
      {
        ICollWspItem cwspi = (ICollWspItem)lvi.Tag;
        ListViewItem ni = (ListViewItem)lvi.Clone();

        resListView.Items.Add( ni );
        cwspi.ListViewItem = ni;
        resCollection.Add( cwspi );
      }
      //remove anyhow but don't add to resources
      collListView.Items.Remove( lvi );
      collCollection.Remove( (ICollWspItem)lvi.Tag );
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
   #endregion

   private void ClearResDetails()
    {
      propertyListView.Items.Clear();
      imagePictureBox.Image = null;
    }

    private void SelectItem( object sender, System.EventArgs e )
    {
      if ( rightTabControl.SelectedTab.Name == "resTabPage" )
      {
        if ( resListView.SelectedItems.Count == 0 )
          return;

        ClearResDetails();
        ListViewItem lvi = resListView.SelectedItems[0];
        
        for ( int i = 0; i < resListView.Columns.Count; i++ )
        {
          ListViewItem lvii = new ListViewItem(resListView.Columns[i].Text);
          lvii.SubItems.Add( lvi.SubItems[i].Text );
          propertyListView.Items.Add( lvii );
        }

        if ( ( (ICollWspItem)lvi.Tag ).Thumb != null )
          imagePictureBox.Image = ( (ICollWspItem)lvi.Tag ).Thumb;
        cachefile.Save();
      }
    }

    private void SetGenderFilterArray( object sender, EventArgs e )
    {
      if ( ( (CheckBox)sender ).Checked )
        genderFilter.Add( ( (CheckBox)sender ).Text );
      else
        genderFilter.Remove( ( (CheckBox)sender ).Text );
    }

    private void ApplyFilters( object sender, EventArgs e )
    {
      if (filteredCollection == null)
        filteredCollection = new ListViewItem[0];
      pb.Value = 0;
      WaitingScreen.Wait();
      int count = resListView.Items.Count;
      int ct = 0;
      foreach ( ListViewItem lvi in resListView.Items )
      {
        pb.Value = Math.Max( Math.Min( ( ( ct++ ) * pb.Maximum ) / count, pb.Maximum ), pb.Minimum );
        if ( Filter(lvi) )
        {
          resListView.Items.Remove( lvi );
          filteredCollection = (ListViewItem[])(SimPe.Helper.Add(filteredCollection, lvi) );
        }
      }

      if ( filteredCollection.Length > 0 )
      {
        resetClothFilterButton.Enabled = true;
        resetObjectFilterButton.Enabled = true;
        if ( !resTabControl.SelectedTab.Text.Contains("*") )
          resTabControl.SelectedTab.Text = "*" + resTabControl.SelectedTab.Text;
      }

      resTabControl.SelectTab( 0 );
      pb.Value = 0;
      WaitingScreen.Stop();
    }

     private bool Filter(ListViewItem lvi)
     {
        if ( collType == "clothing" )
           return ( (!ProcessAgeFilter( lvi )) || (!ProcessCategoryFilter( lvi )) || (!ProcessClothTypeFilter( lvi )) || (!ProcessGenderFilter( lvi )) );
        else
           return ( (!ProcessRoomFilter(lvi)) || (!ProcessFunctionFilter(lvi)) );

     }

     private void ResetFilters()
     {
        ResetClothFilters();
        ResetObjectFilters();
     }
     private void ResetFilters( object sender, EventArgs e )
    {
       ResetFilters();
   }

   #region object filters

     private bool ProcessRoomFilter(ListViewItem lvi)
     {
        if ( roomFilterListView.CheckedItems.Count > 0 )
        {
           SimPe.PackedFiles.Wrapper.ObjRoomSort r = ( (IObjCollWspItem)lvi.Tag ).RoomSort;
           string room = CollWsp.CollWspUtils.GetRoomString( r );
           foreach ( ListViewItem item in roomFilterListView.CheckedItems )
           {
              if ( room.Contains( item.Text ) )
                 return true;
           }
           return false;
        }
        else
           //no rooms checked
           return true;
     }

     private bool ProcessFunctionFilter(ListViewItem lvi)
     {
        if ( functionFilterListView.CheckedItems.Count > 0 )
        {
           SimPe.PackedFiles.Wrapper.ObjFunctionSort f = ( (IObjCollWspItem)lvi.Tag ).FunctionSort;
           string function = CollWsp.CollWspUtils.GetFunctionString( f );
           foreach ( ListViewItem item in functionFilterListView.CheckedItems )
           {
              if ( function.Contains( item.Text ) )
                 return true;
           }
           return false;
        }
        else
           return true;
     }

     private void ResetObjectFilters()
     {
        resListView.BeginUpdate();
        if ( filteredCollection != null )
           resListView.Items.AddRange( filteredCollection );
        filteredCollection = null;

        if ( objectFiltersTabPage.Text.Contains( "*" ) )
           objectFiltersTabPage.Text = objectFiltersTabPage.Text.Substring( 1 );

        foreach ( ListViewItem lvi in roomFilterListView.CheckedItems )
           lvi.Checked = false;
        foreach ( ListViewItem lvi in functionFilterListView.CheckedItems )
           lvi.Checked = false;

        resetObjectFilterButton.Enabled = false;

        resListView.EndUpdate();
     }

   #endregion

   #region cloth filters
   private void ResetClothFilters()
    {
      resListView.BeginUpdate();
      if ( filteredCollection != null )
        resListView.Items.AddRange( filteredCollection );
      filteredCollection = null;

      if ( clothFiltersTabPage.Text.Contains( "*" ) )
         clothFiltersTabPage.Text = clothFiltersTabPage.Text.Substring( 1 );
      //resTabControl.SelectedTab.Text = resTabControl.SelectedTab.Text.Substring( 1 );

      genderFilter.Clear();
      maleOnlyCheckBox.Checked = false;
      femaleOnlyCheckBox.Checked = false;
      bothCheckBox.Checked = false;

      topCheckBox.Checked = false;
      bottomCheckBox.Checked = false;
      otherCheckBox.Checked = false;
      bodyCheckBox.Checked = false;
      maskCheckBox.Checked = false;
      hatCheckBox.Checked = false;

      foreach ( ListViewItem lvi in categoryFilterListView.CheckedItems )
        lvi.Checked = false;
      foreach ( ListViewItem lvi in ageFilterListView.CheckedItems )
        lvi.Checked = false;

      resetClothFilterButton.Enabled = false;

      resListView.EndUpdate();
    }

    /// <summary>
    /// Filters ListViewItem agains gender filter
    /// </summary>
    /// <param name="lvi">ListViewItem to be checked</param>
    /// <returns>true if item shoud stay visible, false otherwise</returns>
    private bool ProcessGenderFilter( ListViewItem lvi )
    {
      if ( femaleOnlyCheckBox.Checked ^ maleOnlyCheckBox.Checked ^ bothCheckBox.Checked )
      {
        uint g = ((IClothCollWspItem)lvi.Tag ).Gender;
        
        foreach ( string gnd in genderFilter )
        { 
          uint i = Convert.ToUInt32( (CollWsp.Genders)System.Enum.Parse( genders, gnd ) );
          if ( ( i & g ) == i )
            return true;
        }
        // if we're here then filter it out
        return false;
      }
      else
        return true;
    }

    private bool ProcessCategoryFilter( ListViewItem lvi )
    {
      if ( categoryFilterListView.CheckedItems.Count > 0 )
      {
        uint cat = ( (IClothCollWspItem)lvi.Tag ).Category;
        foreach ( ListViewItem item in categoryFilterListView.CheckedItems )
        {
          uint i = Convert.ToUInt16( (Data.SkinCategories)System.Enum.Parse( categories, item.Text ) );
          if ( (i&cat)==i )
            return true;
         
        }
        // if we're here then filter it out
        return false;
      }
      else
        // no categories checked
        return true;
    }

    private bool ProcessAgeFilter( ListViewItem lvi )
    {
      if ( ageFilterListView.CheckedItems.Count > 0 )
      {
        uint age = ( (IClothCollWspItem)lvi.Tag ).Age;
        foreach ( ListViewItem item in ageFilterListView.CheckedItems )
        {
          uint i = Convert.ToUInt16( (Data.Ages)System.Enum.Parse( ages, item.Text ) );
          if ( ( i & age ) == i )
            return true;

        }
        // if we're here then filter it out
        return false;
      }
      else
        //no ages checked
        return true;
    }

    private bool ProcessClothTypeFilter( ListViewItem lvi )
    {
      if ( topCheckBox.Checked ^ bottomCheckBox.Checked ^
        otherCheckBox.Checked ^ bodyCheckBox.Checked ^
        hatCheckBox.Checked ^ maskCheckBox.Checked)
      {
        string s = ( (IClothCollWspItem)lvi.Tag ).GetSaveItem( "override0subset" ).StringValue;
        switch ( s.ToLower().Trim() )
        {
          case "body":
            if ( bodyCheckBox.Checked ) return true;
            break;
          case "top":
            if ( topCheckBox.Checked ) return true;
            break;
          case "bottom":
            if ( bottomCheckBox.Checked ) return true;
            break;
          case "hat":
            if ( hatCheckBox.Checked ) return true;
            break;
          case "mask":
            if ( maskCheckBox.Checked ) return true;
            break;
          default:
            //we have some special case
            return true;
        }
        //if we are here then filter it out
        return false;
      }
      else
        //no cloth type checked
        return true;

  }
   #endregion

     private void collTypeComboBox_SelectionChangeCommitted( object sender, EventArgs e )
       {
          bool changed = false; //indicates type of items change
          string prevtype = collType;
          switch ( collTypeComboBox.SelectedIndex )
          {
             case 0:
             {
                if ( collType != "clothing" )
                { 
                   collType = "clothing";
                   changed = true;
                }
                break;
             }
             case 1:
             {
                if ( collType == "clothing" )
                   changed = true;
                collType = "collection";
                break;
             }
            case 2:
            {
               if ( collType == "clothing" )
                  changed = true;
               collType = "communitylotcollection";
               break;
            }
            case 3:
            {
               if ( collType == "clothing" )
                  changed = true;
               collType = "lotcollection";
               break;
            }
          }

       
          if (changed && ( resCollection.Count > 0 || collCollection.Count > 0))
          { 
             //check if user really wants to change collection type
             if ( MessageBox.Show( "This will clear all your scanned resource and collection items. Proceed?", "Collection Workshop Plugin", MessageBoxButtons.YesNo ) == DialogResult.Yes )
             {
                resCollection.Clear();
                resListView.Clear();
                collListView.Clear();
                collCollection.Clear();
                filteredCollection = null;
             }
             else
             {
                collType = prevtype;
                switch ( prevtype )
                {
                   case "clothing":
                   {
                      collTypeComboBox.SelectedIndex = 0;
                      break;
                   }
                   case "collection":
                   {
                      collTypeComboBox.SelectedIndex = 1;
                      break;
                   }
                   case "communitylotcollection":
                   {
                      collTypeComboBox.SelectedIndex = 2;
                      break;
                   }
                   default:
                   {
                      collTypeComboBox.SelectedIndex = 3;
                      break;
                   }
                }
             }
          }
          
          ResetFilters();
          ResetScanners();
       }

     private void NewCollection(Object sender, EventArgs e)
     {
        if ( MessageBox.Show( "This will clear all your scanned resource and collection items. Proceed?", "Collection Workshop Plugin", MessageBoxButtons.YesNo ) == DialogResult.Yes )
        {
           resCollection.Clear();
           resListView.Clear();
           collListView.Clear();
           collCollection.Clear();
           filteredCollection = null;
           newcoll = true;
           ResetFilters();
           ResetScanners();
           flname = null;
           iconData = null;
           collIconPictureBox.Image = null;
           collNameTextBox.Text = "";
        }
     }

     private void ResetScanners()
     {
        fscanners.Clear();

        if ( collType == "clothing" )
        {
           if ( !this.resTabControl.Controls.ContainsKey( this.clothFiltersTabPage.Name ) )
              this.resTabControl.Controls.Add( this.clothFiltersTabPage );
           this.resTabControl.Controls.Remove( this.objectFiltersTabPage );

           foreach ( IScanner i in CollWspScannerRegistry.Global.Scanners )
           {
              string name = i.GetType().Name + " (version=" + i.Version.ToString() + ", uid=0x" + Helper.HexString( i.Uid ) + ", index=" + i.Index.ToString() + ")";
              if ( name.ToLower().Contains( "bodyclothscanner" ) )
              {
                 fscanners.Add( i );
              }
           }
        }
        else
        {
           if ( !this.resTabControl.Controls.ContainsKey( this.objectFiltersTabPage.Name ) )
              this.resTabControl.Controls.Add( this.objectFiltersTabPage );
           this.resTabControl.Controls.Remove( this.clothFiltersTabPage );

           foreach ( IScanner i in CollWspScannerRegistry.Global.Scanners )
           {
              string name = i.GetType().Name + " (version=" + i.Version.ToString() + ", uid=0x" + Helper.HexString( i.Uid ) + ", index=" + i.Index.ToString() + ")";
              if ( name.ToLower().Contains( "objectextendedscanner" ) )
              {
                 fscanners.Add( i );
              }
           }
        }
     }

    #region sort collection items
    private void MoveUp(object sender, EventArgs e)
     {
        if ( collListView.SelectedItems.Count == 0 ) return;
        foreach ( ListViewItem lvi in collListView.SelectedItems )
        {
           if ( lvi.Index > 0 && !collListView.SelectedItems.Contains( collListView.Items[lvi.Index - 1] ) )
           {
              int ind = lvi.Index;
              collListView.Items.Remove(lvi);
              collListView.Items.Insert( ind - 1, lvi );
              collListView.SelectedIndices.Add( ind - 1 );
           }
        }
     }
     private void MoveDown(object sender, EventArgs e)
     {
        if ( collListView.SelectedItems.Count == 0 ) return;
        ListViewItem[] lvarr = new ListViewItem[collListView.SelectedItems.Count];
        int i =0;
        foreach ( ListViewItem lvi in collListView.SelectedItems )
        { 
           lvarr[i] = lvi;
           i++;
        }
        Array.Reverse( lvarr );
        foreach ( ListViewItem lvi in lvarr )
        {
           if ( lvi.Index+1 < collListView.Items.Count && !collListView.SelectedItems.Contains( collListView.Items[lvi.Index + 1] ) )
           {
              int ind = lvi.Index;
              collListView.Items.Remove( lvi );
              collListView.Items.Insert( ind + 1, lvi );
              collListView.SelectedIndices.Add( ind + 1 );
           }
        }
     }
     private void MoveBottom(object sender, EventArgs e)
     {
        if ( collListView.SelectedItems.Count == 0 )
           return;
        
        foreach ( ListViewItem lvi in collListView.SelectedItems )
        {
           collListView.Items.Remove( lvi );
           collListView.Items.Add( lvi );
        }
     }

     private void MoveTop(object sender, EventArgs e)
     {
        if ( collListView.SelectedItems.Count == 0 )
           return;
        int i = 0;
        foreach(ListViewItem lvi in collListView.SelectedItems)
        {
           collListView.Items.Remove( lvi );
           collListView.Items.Insert(i, lvi );
           i++;
        }
     }
    #endregion

  }
}