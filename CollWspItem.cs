using System;
using System.Collections;
using System.Drawing;

namespace SimPe.Plugin.CollWsp
{
  public interface ICollWspItem
  {
    Image Thumb
    {
      get;
    }
    string File
    {
      get;
    }
    SimPe.Packages.PackedFileDescriptor PackedFileDescriptor
    {
      get;
    }
    System.Windows.Forms.ListViewItem ListViewItem
    {
      get;
      set;
    }
    string Name
    {
      get;
    }

    bool Equals( Object obj );
  }

   public interface IObjCollWspItem : ICollWspItem
   {
      SimPe.PackedFiles.Wrapper.ObjRoomSort RoomSort
      { get; }
      SimPe.PackedFiles.Wrapper.ObjFunctionSort FunctionSort
      { get; }
      uint Guid
      { get; }
   }

  public interface IClothCollWspItem : ICollWspItem
  {
    SimPe.PackedFiles.Wrapper.CpfItem GetItem( string name );
    SimPe.PackedFiles.Wrapper.CpfItem GetSaveItem( string name );
    SimPe.PackedFiles.Wrapper.Cpf Cpf
    {
      get;
    }
    string GetSolvedString( string name );
    
    uint Age
    {
      get;
    }
    uint Category
    {
      get;
    }
    uint Gender
    {
      get;
    }

  }

   public class ObjCollWspItem : IObjCollWspItem, ICollWspItem
   {
      protected SimPe.PackedFiles.Wrapper.ExtObjd objd;
      protected ScannerItem si;
      protected System.Windows.Forms.ListViewItem lvi;
      protected uint guid;
      protected Image thumb;

      public ObjCollWspItem(SimPe.PackedFiles.Wrapper.ExtObjd objdarg, System.Windows.Forms.ListViewItem lvii)
      {
         objd = objdarg;
         guid = objdarg.Guid;
         lvi = lvii;
      }
      public ObjCollWspItem(ScannerItem siarg, System.Windows.Forms.ListViewItem lvii)
      {
         lvi = lvii;
         si = siarg;
         uint inst = 0xFFFFFFFF;
         uint subtype = 0;
         Interfaces.Files.IPackedFileDescriptor[] pfds = si.Package.FindFiles( Data.MetaData.OBJD_FILE );
         foreach ( Interfaces.Files.IPackedFileDescriptor pfd in pfds )
         {
            if ( pfd.Instance < inst ) 
            {
               inst = pfd.Instance; subtype = pfd.SubType;
            }
            if ( pfd.Instance == 0x41A7 || pfd.Instance == 0x41AF )
            {
               SimPe.PackedFiles.Wrapper.ExtObjd objdi = new SimPe.PackedFiles.Wrapper.ExtObjd();
               objdi.ProcessData( pfd, si.Package, false );
               
               guid = objdi.Guid;
               objd = objdi;
               break;
            }
         }
         if ( objd == null )
         { 
            //try to process pfd with lowest instance number
            Interfaces.Files.IPackedFileDescriptor[] pfdi = si.Package.FindFile( Data.MetaData.OBJD_FILE, subtype, inst );
            SimPe.PackedFiles.Wrapper.ExtObjd objdi = new SimPe.PackedFiles.Wrapper.ExtObjd();
            objdi.ProcessData( pfdi[0], si.Package, false );

            guid = objdi.Guid;
            objd = objdi;
         }
      }

      public uint Guid
      {
         get { return guid; }
         set { guid = value; }
      }

      public SimPe.PackedFiles.Wrapper.ExtObjd ExtObjd
      {
         get { return objd; }
         set { objd = value; }
      }
      
      public ScannerItem ScannerItem
      { 
         get { return si; } 
      }

      
      public System.Windows.Forms.ListViewItem ListViewItem
      {
         get { return lvi; }
         set { lvi = value; }
      }



      #region IObjCollWspItem Members

      public SimPe.PackedFiles.Wrapper.ObjRoomSort RoomSort
      {
         get { return objd.RoomSort; }
      }

      public SimPe.PackedFiles.Wrapper.ObjFunctionSort FunctionSort
      {
         get { return objd.FunctionSort; }
      }

      #endregion

      #region ICollWspItem Members

      public Image Thumb
      {
         get
         {
            if ( thumb == null )
               return si != null ? si.PackageCacheItem.Thumbnail : null;
            else
               return thumb;
         }
         set { thumb = value; }
      }

      public string File
      {
         get { return objd.FileName; }
      }

      public SimPe.Packages.PackedFileDescriptor PackedFileDescriptor
      {
         get
         {
            return (SimPe.Packages.PackedFileDescriptor)objd.FileDescriptor;
         }
      }

      public string Name
      {
         get 
         {
            return objd.ResourceName;
         }
      }
      // override object.Equals
      public override bool Equals(object obj)
      {
         if ( obj == null || GetType() != obj.GetType() )
         {
            return false;
         }

         IObjCollWspItem cwspi = (IObjCollWspItem)obj;
         return ( guid == cwspi.Guid);

      }

      // override object.GetHashCode
      public override int GetHashCode()
      {
         // TODO: write your implementation of GetHashCode() here.
         return base.GetHashCode();
      }
      #endregion
   }

  /// <summary>
  /// Class to store basic collection workshop scan item
  /// </summary>
  public class CustClothCollWspItem : ClothCollWspItem, IClothCollWspItem, ICollWspItem
  {
    protected ScannerItem si;
    /// <summary>
    /// constructor to speed up
    /// </summary>
    public CustClothCollWspItem( ScannerItem siarg, SimPe.PackedFiles.Wrapper.Cpf cpfarg, System.Windows.Forms.ListViewItem lvii):base( cpfarg, lvii)
    {
      si = siarg;
    }

    public ScannerItem ScannerItem
    {
      get
      {
        return si;
      }
    }

    public new Image Thumb
    {
      get
      {
        if ( si.PackageCacheItem.Thumbnail != null)
          return si.PackageCacheItem.Thumbnail;
        try
        {
          WaitingScreen.Wait();
          WaitingScreen.UpdateMessage( "Trying to load image" );
          FileTable.FileIndex.StoreCurrentState();
          FileTable.FileIndex.AddIndexFromPackage( si.Package );

          SkinChain sc = new SkinChain( cpf );
          GenericRcol rcol = sc.TXTR;
          Size sz = new Size(96,96);

          if ( rcol != null )
          {
            ImageData id = (ImageData)rcol.Blocks[0];
            MipMap mm = id.GetLargestTexture( sz );
            if ( mm != null )
            {
              Thumb = ImageLoader.Preview( mm.Texture, sz );
              //imagePictureBox.Image = ( (CustClothCollWspItem)lvi.Tag ).Thumb;
              
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
        return si.PackageCacheItem.Thumbnail;
      }
      set
      {
        si.PackageCacheItem.Thumbnail = value;
        base.Thumb = value;
      }
    }
    // override object.Equals
    public override bool Equals( object obj )
    {
      if ( obj == null || GetType() != obj.GetType() )
      {
        return false;
      }

      IClothCollWspItem cwspi = (IClothCollWspItem)obj;
      return ( cpf.FileDescriptor.Type == cwspi.PackedFileDescriptor.Type ) &&
             ( cpf.FileDescriptor.Group == cwspi.PackedFileDescriptor.Group ) &&
             ( cpf.FileDescriptor.Instance == cwspi.PackedFileDescriptor.Instance );

    }

    // override object.GetHashCode
    public override int GetHashCode()
    {
      // TODO: write your implementation of GetHashCode() here.
      return base.GetHashCode();
    }

  }
  
  
  public class ClothCollWspItem : IClothCollWspItem
  {
    protected SimPe.PackedFiles.Wrapper.Cpf cpf;
    public ClothCollWspItem( SimPe.PackedFiles.Wrapper.Cpf cpfarg, System.Windows.Forms.ListViewItem lvii )
    {
      lvi = lvii;
      cpf = cpfarg;
    }
    /// <summary>
    /// returns the Item with the given Name or null if not found
    /// </summary>
    /// <param name="name"></param>
    /// <returns>null or the Item</returns>
    public SimPe.PackedFiles.Wrapper.CpfItem GetItem( string name )
    {
      return cpf.GetSaveItem( name );
    }


    public SimPe.PackedFiles.Wrapper.Cpf Cpf
    {
      get
      {
        return cpf;
      }
    }
    /// <summary>
		/// returns the Item with the given Name or null if not found
		/// </summary>
		/// <param name="name"></param>
		/// <returns>the CpfItem</returns>
    public SimPe.PackedFiles.Wrapper.CpfItem GetSaveItem( string name ) 
		{
      return cpf.GetSaveItem( name );
		}
    /// <summary>
    /// Gets Age or Category String list separated by comma
    /// </summary>
    /// <param name="name">can be age or category</param>
    /// <returns>string solved Age or Category</returns>

    public string GetSolvedString( string name )
    {
      string ret = "";
      switch ( name )
      {
        case ( "age" ):
          {
            Data.Ages[] ages = (Data.Ages[])System.Enum.GetValues( typeof( Data.Ages ) );
            uint a = cpf.GetSaveItem( "age" ).UIntegerValue;
            foreach ( Data.Ages ag in ages )
            {
              if ( ( a & (uint)ag ) != 0 )
              {
                if ( ret != "" )
                  ret += ", ";
                ret += ag.ToString();
              }
            }
            break;
          }
        case ( "category" ):
          {
            Data.SkinCategories[] cats = (Data.SkinCategories[])System.Enum.GetValues( typeof( Data.SkinCategories ) );
            uint c = cpf.GetSaveItem( "category" ).UIntegerValue;
            foreach ( Data.SkinCategories cat in cats )
            {
              if ( ( c & (uint)cat ) != 0 )
              {
                if ( ret != "" )
                  ret += ", ";
                ret += cat.ToString();
              }
            }
            break;
          }

      }
      return ret;
    }

      Image thumb;
      public Image Thumb
      {
        get
        {
          if ( thumb != null )
            return thumb;

          Size sz = new Size( 128, 128 );

          SkinChain sc = new SkinChain( cpf );
          GenericRcol rcol = sc.TXTR;

          if ( rcol != null )
          {
            ImageData id = (ImageData)rcol.Blocks[0];
            MipMap mm = id.GetLargestTexture( sz );
            if ( mm != null )
              return ImageLoader.Preview( mm.Texture, sz );
          }
          return null;
        }
        set      {        thumb = value;      }
      }

    public SimPe.Packages.PackedFileDescriptor PackedFileDescriptor
    {
      get
      {
        return (SimPe.Packages.PackedFileDescriptor)cpf.FileDescriptor;
      }
    }

    //SimPe.PackedFiles.Wrapper.CpfItem[] items;
    public SimPe.PackedFiles.Wrapper.CpfItem[] Items
    {
      get      {        return cpf.Items;      }
    }

    public uint Category
    {
      get
      {
        try
        {
          if ( cpf != null )
          {
            SimPe.PackedFiles.Wrapper.CpfItem citem = cpf.GetItem( "category" );
            if ( citem != null )
              return citem.UIntegerValue;

          }
        }
        catch ( Exception )
        {
        }
        return 0;
      }
    }

    public uint Age
    {
      get
      {
        try
        {
          if ( cpf != null )
          {
            SimPe.PackedFiles.Wrapper.CpfItem citem = cpf.GetItem( "age" );
            if ( citem != null )
              return citem.UIntegerValue;

          }
        }
        catch ( Exception )
        {
        }
        return 0;
      }
    }
    public uint Gender
    {
      get
      {
        try
        {
          if ( cpf != null )
          {
            SimPe.PackedFiles.Wrapper.CpfItem citem = cpf.GetItem( "gender" );
            if ( citem != null )
              return citem.UIntegerValue;

          }
        }
        catch ( Exception )
        {
        }
        return 0;
      }
    }

    public string Name
    {
      get
      {
        try
        {
          if ( cpf != null )
          {
            SimPe.PackedFiles.Wrapper.CpfItem citem = cpf.GetItem( "name" );
            if ( citem != null )
              return citem.StringValue;

          }
        }
        catch ( Exception )
        {
        }
        return "";
      }
    }

    public override string ToString()
    {
      return "Category=" + GetSolvedString("category") + "; Age=" + GetSolvedString("age") + "; Name=" + Name;
    }

    //get file name of original package
    public string File
    {
      get      {        return cpf.Package.FileName;      }
    }

    System.Windows.Forms.ListViewItem lvi;
    public System.Windows.Forms.ListViewItem ListViewItem
    {
      get      {        return lvi;      }
      set      {        lvi = value;      }
    }

    // override object.Equals
    public override bool Equals (object obj)
    {
      if (obj == null || GetType() != obj.GetType()) 
      {
          return false;
      }

      IClothCollWspItem cwspi = (IClothCollWspItem)obj;
      return (cpf.FileDescriptor.Type==cwspi.PackedFileDescriptor.Type) &&
             ( cpf.FileDescriptor.Group == cwspi.PackedFileDescriptor.Group ) &&
             ( cpf.FileDescriptor.Instance == cwspi.PackedFileDescriptor.Instance );
 
    }
    
    // override object.GetHashCode
    public override int GetHashCode()
    {
        // TODO: write your implementation of GetHashCode() here.
        return base.GetHashCode();
    }
  }

  /// <summary>
  /// Typesave ArrayList for CollWspItem
  /// </summary>
  public class CollWspItems : ArrayList
  {
    public new ICollWspItem this[int index]
    {
      get
      {
        return ( (ICollWspItem)base[index] );
      }
      set
      {
        base[index] = value;
      }
    }

    public ICollWspItem this[uint index]
    {
      get
      {
        return ( (ICollWspItem)base[(int)index] );
      }
      set
      {
        base[(int)index] = value;
      }
    }

    public int Add( ICollWspItem item )
    {
      return base.Add( item );
    }

    public void Insert( int index, ICollWspItem item )
    {
      base.Insert( index, item );
    }

    public void Remove( ICollWspItem item )
    {
      base.Remove( item );
    }

    public bool Contains( ICollWspItem item )
    {
      return base.Contains( item );
    }

    public int Length
    {
      get
      {
        return this.Count;
      }
    }

    public override object Clone()
    {
      CollWspItems list = new CollWspItems();
      foreach ( ICollWspItem item in this )
        list.Add( item );

      return list;
    }

  }
}
