using System;
using System.Collections;
using System.Drawing;

namespace SimPe.Plugin.CollWsp
{
  /// <summary>
  /// Class to store basic collection workshop scan item
  /// </summary>
  public class CollWspItem
  {
    //uint age;
    public uint Age
    {
      get
      {
        foreach ( SimPe.PackedFiles.Wrapper.CpfItem item in items )
        {
          if ( item.Name == "age" )
            return item.UIntegerValue;
        }
        return 0;
      }
    }
    
    //uint cat;
    public uint Category
    {
      get
      {
        foreach ( SimPe.PackedFiles.Wrapper.CpfItem item in items )
        {
          if ( item.Name == "category" ) return item.UIntegerValue;
        }
        return 0;
      }
    }

    Image thumb;
    public Image Thumb
    {
      get      {        return thumb;      }
      set      {        thumb = value;      }
    }

    SimPe.Packages.PackedFileDescriptor pfd;
    public SimPe.Packages.PackedFileDescriptor PackedFileDescriptor
    {
      get      {        return pfd;      }
      set      {        pfd = value;      }
    }

    SimPe.PackedFiles.Wrapper.CpfItem[] items;
    public SimPe.PackedFiles.Wrapper.CpfItem[] Items
    {
      get      {        return items;      }
      set      {        items = value;      }
    }

    public void SetPackedFile(SimPe.Interfaces.Scenegraph.IScenegraphFileIndexItem si)
    {
      SimPe.PackedFiles.Wrapper.Cpf cpf = new SimPe.PackedFiles.Wrapper.Cpf();
      cpf.ProcessData( si );
    }
    
    System.Windows.Forms.ListViewItem lvi;
    public System.Windows.Forms.ListViewItem ListViewItem
    {
      get      {        return lvi;      }
      set      {        lvi = value;      }
    }
  }

  /// <summary>
  /// Typesave ArrayList for CollWspItem
  /// </summary>
  public class CollWspItems : ArrayList
  {
    public new CollWspItem this[int index]
    {
      get
      {
        return ( (CollWspItem)base[index] );
      }
      set
      {
        base[index] = value;
      }
    }

    public CollWspItem this[uint index]
    {
      get
      {
        return ( (CollWspItem)base[(int)index] );
      }
      set
      {
        base[(int)index] = value;
      }
    }

    public int Add( CollWspItem item )
    {
      return base.Add( item );
    }

    public void Insert( int index, CollWspItem item )
    {
      base.Insert( index, item );
    }

    public void Remove( CollWspItem item )
    {
      base.Remove( item );
    }

    public bool Contains( CollWspItem item )
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
      foreach ( CollWspItem item in this )
        list.Add( item );

      return list;
    }

  }
}
