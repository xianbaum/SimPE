using System;
using SimPe.Interfaces;

namespace SimPe.Plugin.CollWsp
{
    /// <summary>
    /// An instance of this class will hook into SimPEs Tools Menu, allowing the users to start your Plugin
    /// </summary>
  internal class CollWspTool : Interfaces.AbstractTool, Interfaces.ITool
  {
    static CollWspForm _collWspForm;
      internal CollWspTool()
      {
      }

      #region IToolExt Member

      public System.Drawing.Image Icon
      {
          get { 
              ///
              /// TODO: You can return an Image here, that will be displayed as the ICon for your Plugin in the SimPE Plugin Menu.
              /// 
              return null; 
          }
      }

      public System.Windows.Forms.Shortcut Shortcut
      {
          get {
              ///
              /// TODO: Return the Shurtcut key, that will start your Plugin
              /// 
              return System.Windows.Forms.Shortcut.None;
          }
      }

      public bool Visible
      {
          get { 
              ///
              /// TODO: Return false here, if you donÄt want your Plugin to show up in the Plugins Menus, but still want to catch the 
              /// ChangeEnabledStateEventHandler Event
              /// 
              return true; 
          }
      }

      #endregion

      public override string ToString()
      {
          ///
          /// TODO: Return the Name of your Plugin here
          /// 
          return "Collection Workshop\\Create Collection...";
      }

      #region ITool Members

      public bool IsEnabled( SimPe.Interfaces.Files.IPackedFileDescriptor pfd, SimPe.Interfaces.Files.IPackageFile package )
      {
        return true;
      }

      public SimPe.Interfaces.Plugin.IToolResult ShowDialog( ref SimPe.Interfaces.Files.IPackedFileDescriptor pfd, ref SimPe.Interfaces.Files.IPackageFile package )
      {
        if ( _collWspForm == null )
          _collWspForm = new CollWspForm(null);
        RemoteControl.ShowSubForm( _collWspForm );

        if ( _collWspForm.FileName == null )
          return new ToolResult( false, false );
        else
        {
          SimPe.Packages.GeneratableFile gf = SimPe.Packages.GeneratableFile.LoadFromFile( _collWspForm.FileName );
          package = gf;
          return new ToolResult( false, true );
        }
      }

      #endregion
    }

    /// <summary>
    /// An instance of this class will hook into SimPEs Tools Menu, allowing the users to start your Plugin
    /// </summary>
    internal class CollWspSortTool : Interfaces.AbstractTool, Interfaces.ITool
    {
       static CollWspSortForm _collWspSortForm;
       internal CollWspSortTool()
       {
       }

       #region IToolExt Member

       public System.Drawing.Image Icon
       {
          get
          {
             ///
             /// TODO: You can return an Image here, that will be displayed as the ICon for your Plugin in the SimPE Plugin Menu.
             /// 
             return null;
          }
       }

       public System.Windows.Forms.Shortcut Shortcut
       {
          get
          {
             ///
             /// TODO: Return the Shurtcut key, that will start your Plugin
             /// 
             return System.Windows.Forms.Shortcut.None;
          }
       }

       public bool Visible
       {
          get
          {
             ///
             /// TODO: Return false here, if you donÄt want your Plugin to show up in the Plugins Menus, but still want to catch the 
             /// ChangeEnabledStateEventHandler Event
             /// 
             return true;
          }
       }

       #endregion

       public override string ToString()
       {
          ///
          /// TODO: Return the Name of your Plugin here
          /// 
          return "Collection Workshop\\Sort Collections...";
       }

       #region ITool Members

       public bool IsEnabled(SimPe.Interfaces.Files.IPackedFileDescriptor pfd, SimPe.Interfaces.Files.IPackageFile package)
       {
          return true;
       }

       public SimPe.Interfaces.Plugin.IToolResult ShowDialog(ref SimPe.Interfaces.Files.IPackedFileDescriptor pfd, ref SimPe.Interfaces.Files.IPackageFile package)
       {
          if ( _collWspSortForm == null )
             _collWspSortForm = new CollWspSortForm();
          RemoteControl.ShowSubForm( _collWspSortForm );

          if ( _collWspSortForm.FileName == null )
             return new ToolResult( false, false );
          else
          {
             SimPe.Packages.GeneratableFile gf = SimPe.Packages.GeneratableFile.LoadFromFile( _collWspSortForm.FileName );
             package = gf;
             return new ToolResult( false, true );
          }
       }

       #endregion
    }
   internal class CollEditWspTool : Interfaces.AbstractTool, Interfaces.ITool
   {
      static CollWspForm _collWspForm;
      static SimPe.Interfaces.Files.IPackageFile pkg;
      internal CollEditWspTool()
      {
      }

      #region IToolExt Member

      public System.Drawing.Image Icon
      {
         get
         {
            ///
            /// TODO: You can return an Image here, that will be displayed as the ICon for your Plugin in the SimPE Plugin Menu.
            /// 
            return null;
         }
      }

      public System.Windows.Forms.Shortcut Shortcut
      {
         get
         {
            ///
            /// TODO: Return the Shurtcut key, that will start your Plugin
            /// 
            return System.Windows.Forms.Shortcut.None;
         }
      }

      public bool Visible
      {
         get
         {
            ///
            /// TODO: Return false here, if you donÄt want your Plugin to show up in the Plugins Menus, but still want to catch the 
            /// ChangeEnabledStateEventHandler Event
            /// 
            return true;
         }
      }

      #endregion

      public override string ToString()
      {
         ///
         /// TODO: Return the Name of your Plugin here
         /// 
         return "Collection Workshop\\Edit Collection...";
      }

      #region ITool Members

      public bool IsEnabled(SimPe.Interfaces.Files.IPackedFileDescriptor pfd, SimPe.Interfaces.Files.IPackageFile package)
      {
         return CollWspUtils.IsCollection(package);
      }

      public SimPe.Interfaces.Plugin.IToolResult ShowDialog(ref SimPe.Interfaces.Files.IPackedFileDescriptor pfd, ref SimPe.Interfaces.Files.IPackageFile package)
      {
         if ( _collWspForm == null || pkg.FileName != package.FileName )
         {
            pkg = package;
            _collWspForm = new CollWspForm( package );
         }
         RemoteControl.ShowSubForm( _collWspForm );

         if ( _collWspForm.FileName == null )
            return new ToolResult( false, false );
         else
         {
            SimPe.Packages.GeneratableFile gf = SimPe.Packages.GeneratableFile.LoadFromFile( _collWspForm.FileName );
            package = gf;
            return new ToolResult( false, true );
         }
      }

      #endregion
   }
}
