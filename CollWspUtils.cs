using System;
using System.Collections;
using System.Text;

namespace DimPe.Data
{
  
}

namespace SimPe.Plugin.CollWsp
{
  public enum Genders : ushort
  {
    Female = 0x01,
    Male = 0x02,
    Both = 0x03
  }

  public static class CollWspUtils
  {
    public static string[] GetAllDirectories( string folder)
    {
      // Get the root of the search string.
      string[] directories;
      directories = System.IO.Directory.GetDirectories( folder );

      ArrayList directoryList = new ArrayList( directories );

      // Retrieve subdirectories of matches.
      for ( int i = 0, max = directories.Length; i < max; i++ )
      {
        string directory = (string)directoryList[i];
        string[] more = GetAllDirectories( directory );

        // For each subdirectory found, add in the base path.
        //for ( int j = 0; j < more.Length; j++ )
        //  more[j] = directory + more[j];

        // Insert the subdirectories into the list and 
        // update the counter and upper bound.

        directoryList.InsertRange( i + 1, more );
        i += more.Length;
        max += more.Length;
      }
      return (string[])directoryList.ToArray( Type.GetType( "System.String" ) );
    }

    /// <summary>
    /// Get string equivelent of gender ushort
    /// </summary>
    /// <param name="g"></param>
    /// <returns>Gender string</returns>
    public static string GetGenderString( uint g )
    {
      //TODO: change to Data.MetaData.Gender when it will be fixed by Quaxi
      SimPe.Plugin.CollWsp.Genders[] genders = (SimPe.Plugin.CollWsp.Genders[])System.Enum.GetValues( typeof( SimPe.Plugin.CollWsp.Genders ) );
      foreach ( SimPe.Plugin.CollWsp.Genders gnd in genders )
      {
        if ( ( g & (uint)gnd ) == g )
        {
          return gnd.ToString();
        }
      }
      return "unknown";
    }

     public static string GetRoomString(SimPe.PackedFiles.Wrapper.ObjRoomSort r)
     {
        string room = "";
        Type MyTyper = r.GetType();

        Data.ObjRoomSortBits[] rooms = (Data.ObjRoomSortBits[])System.Enum.GetValues( typeof( Data.ObjRoomSortBits ) );
        foreach ( Data.ObjRoomSortBits rb in rooms )
        {
           string meth = "In" + rb.ToString();
           System.Reflection.PropertyInfo mypropinfo = MyTyper.GetProperty( meth );
           if ( (bool)mypropinfo.GetValue( r, null ) )
           {
              if ( room != "" )
                 room += ", ";
              room += rb.ToString();
           }
        }
        return room;
     }

     public static string GetFunctionString(SimPe.PackedFiles.Wrapper.ObjFunctionSort f)
     {
        string function = "";
        Type MyTypef = f.GetType();

        Data.ObjFunctionSortBits[] funcs = (Data.ObjFunctionSortBits[])System.Enum.GetValues( typeof( Data.ObjFunctionSortBits ) );
        foreach ( Data.ObjFunctionSortBits fb in funcs )
        {
           string meth = "In" + fb.ToString();
           System.Reflection.PropertyInfo mypropinfo = MyTypef.GetProperty( meth );
           if ( (bool)mypropinfo.GetValue( f, null ) )
           {
              if ( function != "" )
                 function += ", ";
              function += fb.ToString();
           }
        }
        return function;
     }
  }
}
