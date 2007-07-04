using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SimPe.Interfaces.Plugin.Scanner;
using SimPe.Interfaces;
using SimPe.Interfaces.Files;
using SimPe.Interfaces.Scenegraph;
using SimPe.Plugin.Scanner;

namespace SimPe.Plugin
{
   public partial class CollWspSortForm : Form
   {
      string collType = "clothing";
      bool changed = false;
      string collDirectory = System.IO.Path.Combine( PathProvider.SimSavegameFolder, "Collections" );
      string custCollDirectory = System.IO.Path.Combine( PathProvider.SimSavegameFolder, "Downloads\\Collections" );
      string flname;
      public string FileName
      {
         get
         {
            return flname;
         }
      }
      public CollWspSortForm()
      {
         InitializeComponent();
      }

      private void collTypeComboBox_SelectionChangeCommitted(object sender, EventArgs e)
      {
         bool changed = false;
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
         sortListView.Items.Clear();
         iconImageList.Images.Clear();
      }

      private void Scan(object sender, EventArgs e)
      {
         sortListView.Items.Clear();
         Scan(System.IO.Directory.GetFiles( collDirectory, "*.package" ));
         Scan( System.IO.Directory.GetFiles( custCollDirectory, "*.package" ) );
      }

      private void Scan(string[] files)
      {
         SimPe.Packages.GeneratableFile pkg = null;
         
         foreach ( string file in files )
         {
            pkg = SimPe.Packages.GeneratableFile.LoadFromFile( file );
            //find collections
            SimPe.Interfaces.Files.IPackedFileDescriptor[] pfds = pkg.FindFiles( 0x6C4F359D );
            if ( pfds.Length > 0 )
            {
               foreach ( SimPe.Interfaces.Files.IPackedFileDescriptor pfd in pfds )
               {
                  SimPe.PackedFiles.Wrapper.Cpf cpf = new SimPe.PackedFiles.Wrapper.Cpf();
                  cpf.ProcessData( pfd, pkg );
                  string type = cpf.GetSaveItem( "type" ).StringValue;

                  string title = "";
                  if ( type.ToString() == collType )
                  {
                     SimPe.Interfaces.Files.IPackedFileDescriptor[] pfdsstr = pkg.FindFiles( Data.MetaData.STRING_FILE );
                     if ( pfdsstr.Length > 0 )
                     { 
                        SimPe.PackedFiles.Wrapper.Str str = new SimPe.PackedFiles.Wrapper.Str();
                        SimPe.Data.MetaData.Languages deflang = Helper.WindowsRegistry.LanguageCode;
                        str.ProcessData( pfdsstr[0], pkg );
                        SimPe.PackedFiles.Wrapper.StrItemList items = str.LanguageItems( deflang );
                        if ( items.Length > 0 )
                           title = items[0].Title;
                        else
                        {
                           items = str.LanguageItems( 1 );
                           if ( items.Length > 0 )
                              title = items[0].Title;
                           else
                              title = "";
                        }
                     }

                     Image img = null;
                     SimPe.Interfaces.Files.IPackedFileDescriptor[] pfdsimg = pkg.FindFiles( 0x856DDBAC );
                     if (pfdsimg.Length >0)
                     {
                        SimPe.PackedFiles.Wrapper.Picture pct = new SimPe.PackedFiles.Wrapper.Picture();
                        pct.ProcessData( pfdsimg[0], pkg );
                        img = pct.Image;
                     }

                     if ( title != "" )
                     {
                        ListViewItem lvi = new ListViewItem( title );
                        lvi.Tag = cpf;
                        if ( img != null )
                        {
                           lvi.ImageIndex = iconImageList.Images.Count;
                           iconImageList.Images.Add( img );
                           //lvi.ImageList.Images.Add( img );
                        }
                        sortListView.Items.Add( lvi ); 
                     }
                  }
               }
            }
            else
               continue;
         }
         //sort according to sortindex
         int[] aKeys = new int[sortListView.Items.Count];
         ListViewItem[] aLvi = new ListViewItem[sortListView.Items.Count];
         int i = 0;
         foreach ( ListViewItem lvi in sortListView.Items )
         {
            aKeys[i] = ( (SimPe.PackedFiles.Wrapper.Cpf)lvi.Tag ).GetSaveItem( "sortindex" ).IntegerValue;
            aLvi[i] = lvi;
            i++;
         }
         Array.Sort( aKeys, aLvi );
         sortListView.Items.Clear();
         sortListView.Items.AddRange( aLvi );
      }
 
      #region sort collection items
      private void MoveUp(object sender, EventArgs e)
      {
         if ( sortListView.SelectedItems.Count == 0 )
            return;
         changed = true;
         sortListView.BeginUpdate();
         foreach ( ListViewItem lvi in sortListView.SelectedItems )
         {
            if ( lvi.Index > 0 && !sortListView.SelectedItems.Contains( sortListView.Items[lvi.Index - 1] ) )
            {
               int ind = lvi.Index;
               sortListView.Items.Remove( lvi );
               sortListView.Items.Insert( ind - 1, lvi );
               sortListView.SelectedIndices.Add( ind-1 );
            }
         }

         RefreshSortListView();
      }

      private void RefreshSortListView()
      {
         ListViewItem[] aLvi = new ListViewItem[sortListView.Items.Count];
         int i = 0;
         foreach ( ListViewItem lvi in sortListView.Items )
         {
            aLvi[i] = lvi;
            i++;
         }
         sortListView.Items.Clear();
         sortListView.Items.AddRange( aLvi );
         sortListView.EndUpdate();
      }

      private void MoveDown(object sender, EventArgs e)
      {
         if ( sortListView.SelectedItems.Count == 0 )
            return;
         changed = true;
         sortListView.BeginUpdate();
         ListViewItem[] lvarr = new ListViewItem[sortListView.SelectedItems.Count];
         int i = 0;
         foreach ( ListViewItem lvi in sortListView.SelectedItems )
         {
            lvarr[i] = lvi;
            i++;
         }
         Array.Reverse( lvarr );
         foreach ( ListViewItem lvi in lvarr )
         {
            if ( lvi.Index + 1 < sortListView.Items.Count && !sortListView.SelectedItems.Contains( sortListView.Items[lvi.Index + 1] ) )
            {
               int ind = lvi.Index;
               sortListView.Items.Remove( lvi );
               sortListView.Items.Insert( ind + 1, lvi );
               sortListView.SelectedIndices.Add( ind + 1 );
            }
         }
         RefreshSortListView();
      }
      private void MoveBottom(object sender, EventArgs e)
      {
         if ( sortListView.SelectedItems.Count == 0 )
            return;
         changed = true;
         sortListView.BeginUpdate();
         foreach ( ListViewItem lvi in sortListView.SelectedItems )
         {
            sortListView.Items.Remove( lvi );
            sortListView.Items.Add( lvi );
         }
         RefreshSortListView();
      }

      private void MoveTop(object sender, EventArgs e)
      {
         if ( sortListView.SelectedItems.Count == 0 )
            return;
         changed = true;
         sortListView.BeginUpdate();
         int i = 0;
         foreach ( ListViewItem lvi in sortListView.SelectedItems )
         {
            sortListView.Items.Remove( lvi );
            sortListView.Items.Insert( i, lvi );
            i++;
         }
         RefreshSortListView();
      }
      #endregion

      private void SaveSorting(object sender, EventArgs e)
      {
         if ( changed )
         {
            int i = 0;
            try
            {
               foreach ( ListViewItem lvi in sortListView.Items )
               {

                  SimPe.PackedFiles.Wrapper.Cpf cpf = (SimPe.PackedFiles.Wrapper.Cpf)lvi.Tag;
                  int ind = cpf.GetSaveItem( "sortindex" ).IntegerValue;
                  if ( ind != i )
                  {
                     cpf.GetSaveItem( "sortindex" ).IntegerValue = i;
                     cpf.SynchronizeUserData();
                     //cpf.Save( cpf.FileDescriptor );
                     cpf.Package.Save(); 
                  }
                  i++;
               }
               MessageBox.Show( "Sorting saved", this.Text );
               changed = false;
            }
            catch ( Exception ex)
            {
               MessageBox.Show( "A problem occured while saving sort index: " + ex.ToString(), "Collection Workshop" );
            }
         }
      }
   }
}