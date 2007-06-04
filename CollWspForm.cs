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
    SimPe.Cache.PackageCacheFile cachefile;

    //stores only needed scanners
    CollWspScannerCollection fscanners;

    ClothCollWspItems resCollection;
    ClothCollWspItems collCollection;
    ListViewItem[] filteredCollection = { };
    Type categories = typeof( Data.SkinCategories );
    Type ages = typeof( Data.Ages );
    Type genders = typeof( CollWsp.Genders );
    ArrayList genderFilter;
    string collType = "cloth";

    private void InitFiltersLayout()
    {
      foreach ( string cat in System.Enum.GetNames( categories ) )
      {
        if ( !cat.Contains( "Casual" ) && !cat.Contains( "Skin" ) )
          categoryFilterListView.Items.Add( cat );
      }

      foreach ( string age in System.Enum.GetNames( ages ) )
        ageFilterListView.Items.Add( age );

    }

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
      InitFiltersLayout();

      //load the Group Cache
      SimPe.Plugin.ScenegraphWrapperFactory.LoadGroupCache();

      this.cbfolder.SelectedIndex = 0;

      resCollection = new ClothCollWspItems();
      collCollection = new ClothCollWspItems();
      genderFilter = new ArrayList();
      //filteredCollection = new ArrayList();
            
      sorter = new ColumnSorter();
      resListView.ListViewItemSorter = sorter;

      //resListView.SmallImageList = resListView.LargeImageList;
      cachefile = new SimPe.Cache.PackageCacheFile();
      try
      {
        cachefile.Load( SimPe.Cache.PackageCacheFile.CacheFileName );
      }
      catch ( Exception ex )
      {
        Helper.ExceptionMessage( "Unable to reload the Cache File.", ex );
      }

      fscanners = new CollWspScannerCollection();
      foreach ( IScanner i in CollWspScannerRegistry.Global.Scanners )
      {
         string name = i.GetType().Name + " (version=" + i.Version.ToString() + ", uid=0x" + Helper.HexString( i.Uid ) + ", index=" + i.Index.ToString() + ")";
         if ( name.ToLower().Contains( "bodyclothscanner" ) )
         {
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
          
          iconData = System.IO.File.ReadAllBytes( iconFile );
          Image tmpi = Image.FromFile( iconFile );
          collIconPictureBox.Image = tmpi;

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
      }
      else
      {
        if ( fbd.SelectedPath == "" )
          fbd.SelectedPath = PathProvider.SimSavegameFolder;
        if ( fbd.ShowDialog() == DialogResult.OK )
          folder = fbd.SelectedPath;
      }
    }

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
        if ( ( (IClothCollWspItem)lvi.Tag ).Equals( (IClothCollWspItem)lvii.Tag ) )
          return;
      }
      //not found, we can process request
      IClothCollWspItem cwspi = (IClothCollWspItem)lvi.Tag;
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
        if ( ( (IClothCollWspItem)lvi.Tag ).Equals( (IClothCollWspItem)lvii.Tag ) )
        {
          found = true;
          break;
        }
      }
      //progress if not found
      if ( !found )
      {
        IClothCollWspItem cwspi = (IClothCollWspItem)lvi.Tag;
        ListViewItem ni = (ListViewItem)lvi.Clone();

        resListView.Items.Add( ni );
        cwspi.ListViewItem = ni;
        resCollection.Add( cwspi );
      }
      //remove anyhow but don't add to resources
      collListView.Items.Remove( lvi );
      collCollection.Remove( (IClothCollWspItem)lvi.Tag );
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
        
        //TODO: add more details here
        for ( int i = 0; i < resListView.Columns.Count; i++ )
        {
          ListViewItem lvii = new ListViewItem(resListView.Columns[i].Text);
          lvii.SubItems.Add( lvi.SubItems[i].Text );
          propertyListView.Items.Add( lvii );
        }

        if ( ( (IClothCollWspItem)lvi.Tag ).Thumb != null )
          imagePictureBox.Image = ( (IClothCollWspItem)lvi.Tag ).Thumb;
        else
        {
          try
          {
            WaitingScreen.Wait();
            WaitingScreen.UpdateMessage( "Trying to load image" );
            FileTable.FileIndex.StoreCurrentState();
            FileTable.FileIndex.AddIndexFromPackage( ( (CustClothCollWspItem)lvi.Tag ).ScannerItem.Package );

            SkinChain sc = new SkinChain( ( (IClothCollWspItem)lvi.Tag ).Cpf );
            GenericRcol rcol = sc.TXTR;

            if ( rcol != null )
            {
              ImageData id = (ImageData)rcol.Blocks[0];
              MipMap mm = id.GetLargestTexture( imagePictureBox.Size );
              if ( mm != null )
              {
                ( (CustClothCollWspItem)lvi.Tag ).Thumb = ImageLoader.Preview( mm.Texture, imagePictureBox.Size );
                imagePictureBox.Image = ( (CustClothCollWspItem)lvi.Tag ).Thumb;

                cachefile.Save();
              }
            }
          }
          catch ( Exception ex )
          {
          }
          finally
          {
            FileTable.FileIndex.RestoreLastState();
            WaitingScreen.Stop();
          }
        }
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
        if ( !ProcessAgeFilter(lvi) || !ProcessCategoryFilter(lvi) || !ProcessClothTypeFilter(lvi) || !ProcessGenderFilter( lvi ) )
        {
          resListView.Items.Remove( lvi );
          filteredCollection = (ListViewItem[])(SimPe.Helper.Add(filteredCollection, lvi) );
        }
      }

      if ( filteredCollection.Length > 0 )
      {
        resetFilterButton.Enabled = true;
        if ( !resTabControl.SelectedTab.Text.Contains("*") )
          resTabControl.SelectedTab.Text = "*" + resTabControl.SelectedTab.Text;
      }

      resTabControl.SelectTab( 0 );
      pb.Value = 0;
      WaitingScreen.Stop();
    }

    private void ResetFilters( object sender, EventArgs e )
    {
      ResetFilters(); 
    }

    private void ResetFilters()
    {
      resListView.BeginUpdate();
      if ( filteredCollection != null )
        resListView.Items.AddRange( filteredCollection );
      filteredCollection = null;

      if ( resTabPage.Text.Contains( "*" ) )
        resTabPage.Text = resTabPage.Text.Substring( 1 );
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

      resetFilterButton.Enabled = false;

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

    private void collTypeComboBox_SelectionChangeCommitted( object sender, EventArgs e )
    {
      if ( collTypeComboBox.SelectedIndex == 0 )
      {
        collType = "cloth";
      }
      else
      {
        collType = "lot";
      }
    }

  }
}