using System;
using SimPe.Interfaces.Plugin.Scanner;
//using SimPe.Plugin.Scanner;

namespace SimPe.Plugin.CollWsp
{
  public class CollWspScannerRegistry
  {
    static CollWspScannerRegistry glob;
		public static CollWspScannerRegistry Global
		{
			get 
			{
				if (glob==null) glob = new CollWspScannerRegistry();
				return glob;
			}
		}

		CollWspScannerCollection scanners, identifiers;
		CollWspScannerRegistry()
		{
			scanners = new CollWspScannerCollection();
      identifiers = new CollWspScannerCollection();
			LoadScanners();
		}
    void LoadScanners()
    {
      string[] files = System.IO.Directory.GetFiles( Helper.SimPePluginPath, "*.plugin.dll" );
      scanners.Clear();

      foreach ( string file in files )
      {
        object[] args = new object[0];
        object[] scnrs = SimPe.LoadFileWrappers.LoadPlugins( file, typeof( SimPe.Interfaces.Plugin.Scanner.IScannerPluginBase ), args );
        foreach ( IScannerPluginBase isb in scnrs )
        {
          if ( isb.Version == 1 )
          {
            if ( ( (byte)isb.PluginType & (byte)ScannerPluginType.Scanner ) != 0 )
            {
              try
              {
                IScanner sc = (IScanner)isb;
                scanners.Add( sc );
              }
              catch ( Exception ex )
              {
                Helper.ExceptionMessage( "Unable to load Scanner.", ex );
              }
            }
            else
            {
              try
              {
                IIdentifier i = (IIdentifier)isb;
                identifiers.Add( i );
              }
              catch ( Exception ex )
              {
                Helper.ExceptionMessage( "Unable to load Identifier.", ex );
              }
            }
          }
        }
      }
      identifiers.Sort( new PluginScannerBaseComparer() );
    }

    public CollWspScannerCollection Scanners
    {
      get
      {
        return scanners;
      }
    }

    public CollWspScannerCollection Identifiers
    {
      get
      {
        return identifiers;
      }
    }
  }

  /// <summary>
  /// Comapers two IIdentifierBase Instances
  /// </summary>
  internal class PluginScannerBaseComparer : System.Collections.IComparer
  {
    #region IComparer Member

    public int Compare( object x, object y )
    {
      if ( x == null )
      {
        if ( y == null )
          return 0;
        else
          return 1;
      }

      IScannerPluginBase ix = (IScannerPluginBase)x;
      IScannerPluginBase iy = (IScannerPluginBase)y;

      return ix.Index - iy.Index;
    }

    #endregion

  }
}
