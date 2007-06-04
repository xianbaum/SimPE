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
    /// Saves collection to a file
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void SaveCollection( object sender, EventArgs e )
    {

      if ( iconData == null )
      {
        MessageBox.Show( "Please select icon for your collection.", this.Text );
        return;
      }
      if ( collNameTextBox.Text.Length == 0 )
      {
        MessageBox.Show( "Please give a name for your collection.", this.Text );
        return;
      }

      SaveFileDialog sfd = new SaveFileDialog();
      sfd.InitialDirectory = System.IO.Path.Combine( PathProvider.SimSavegameFolder, "Collections" );
      sfd.DefaultExt = "package";

      //TODO: remove this from here
      sfd.FileName = "testCollection.package";
      if ( !( sfd.ShowDialog() == DialogResult.OK ) )
        return;
      else
        flname = sfd.FileName;
      //string filename = System.IO.Path.Combine( PathProvider.SimSavegameFolder, "Collections\\testCollection.package" );

      try
      {
        IPackageFile package = (IPackageFile)SimPe.Packages.GeneratableFile.CreateNew();

        //create collection resource first
        uint instance = Hashes.InstanceHash( flname );
        uint group = Data.MetaData.LOCAL_GROUP;

        IPackedFileDescriptor collpfd = new SimPe.Packages.PackedFileDescriptor();
        IPackedFileDescriptor strpfd = new SimPe.Packages.PackedFileDescriptor();
        IPackedFileDescriptor iconpfd = new SimPe.Packages.PackedFileDescriptor();

        iconpfd = SaveImageResource( group, 0x00000001, package );
        if ( iconpfd == null )
          return;
        strpfd = SaveStrResource( group, 0x00000001, package );
        if ( strpfd == null )
          return;

        SaveRefFileHeaderResource( group, instance, package, strpfd, iconpfd );
        collpfd = SaveCollectionResource( group, instance, package );
        if ( collpfd == null )
          return;

        SaveListViewItems( group, instance, package, collpfd );

        package.Save( flname );
        MessageBox.Show( "Collection created", this.Text );
      }
      catch ( Exception ex )
      {
        MessageBox.Show( "A problem occured while saving: " + ex.ToString(), "Collection Workshop" );
      }
    }

    /// <summary>
    /// Safe all selected items from collection list view
    /// </summary>
    /// <param name="group"></param>
    /// <param name="instance"></param>
    /// <param name="package"></param>
    /// <param name="collpfd"></param>
    private void SaveListViewItems( uint group, uint instance, IPackageFile package, IPackedFileDescriptor collpfd )
    {
      int j = 0;
      foreach ( ListViewItem lvi in collListView.Items )
      {
        IPackedFileDescriptor pfd = package.NewDescriptor( Data.MetaData.REF_FILE, 0, group, instance + (uint)j + 1 );

        SimPe.Plugin.RefFile reffile = new SimPe.Plugin.RefFile();
        reffile.ProcessData( pfd, package );

        IPackedFileDescriptor[] pfds = new IPackedFileDescriptor[3];
        //UI empty
        pfds[0] = package.NewDescriptor( 0, 0, 0, 0 );
        //collection
        pfds[1] = collpfd;
        //property set
        pfds[2] = ( (IClothCollWspItem)lvi.Tag ).PackedFileDescriptor;

        reffile.Items = pfds;
        reffile.SynchronizeUserData();

        package.Add( pfd );

        //BINX resource
        IPackedFileDescriptor binpfd = package.NewDescriptor( 0x0C560F39, 0, group, instance + (uint)j + 1 );

        SimPe.PackedFiles.Wrapper.Cpf cpf = new SimPe.PackedFiles.Wrapper.Cpf();
        cpf.ProcessData( binpfd, package );

        //TODO: refactor to more ellegant array processing
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
    
    /// <summary>
    /// Safes icon and text collection header resources
    /// </summary>
    /// <param name="group"></param>
    /// <param name="instance"></param>
    /// <param name="package"></param>
    /// <param name="strpfd"></param>
    /// <param name="iconpfd"></param>
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

        SimPe.PackedFiles.Wrapper.StrToken token = new SimPe.PackedFiles.Wrapper.StrToken( 0, (byte)Data.MetaData.Languages.English, collNameTextBox.Text, "" );
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
