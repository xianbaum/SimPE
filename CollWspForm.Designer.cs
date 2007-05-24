namespace SimPe.Plugin.CollWsp
{
  partial class CollWspForm
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose( bool disposing )
    {
      if ( disposing && ( components != null ) )
      {
        components.Dispose();
      }
      base.Dispose( disposing );
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem( new string[] {
            "Atest ch1",
            "Ctest",
            "Atest type"}, -1 );
      System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem( new string[] {
            "Btest ch1",
            "Btest ch2",
            "Btest ch3"}, -1 );
      this.label1 = new System.Windows.Forms.Label();
      this.resListView = new System.Windows.Forms.ListView();
      this.fileColumnHeader = new System.Windows.Forms.ColumnHeader();
      this.enabledColumnHeader = new System.Windows.Forms.ColumnHeader();
      this.typeColumnHeader = new System.Windows.Forms.ColumnHeader();
      this.scanButton = new System.Windows.Forms.Button();
      this.lbscandebug = new System.Windows.Forms.ListBox();
      this.lbid = new System.Windows.Forms.ListBox();
      this.cbrec = new System.Windows.Forms.CheckBox();
      this.cbfolder = new System.Windows.Forms.ComboBox();
      this.fbd = new System.Windows.Forms.FolderBrowserDialog();
      this.collListView = new System.Windows.Forms.ListView();
      this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
      this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
      this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
      this.toCollButton = new System.Windows.Forms.Button();
      this.toolTip1 = new System.Windows.Forms.ToolTip( this.components );
      this.allToCollButton = new System.Windows.Forms.Button();
      this.removeAllFromCollButton = new System.Windows.Forms.Button();
      this.remFromCollButton = new System.Windows.Forms.Button();
      this.collToolTipTextBox = new System.Windows.Forms.TextBox();
      this.collIconPictureBox = new System.Windows.Forms.PictureBox();
      this.collIconButton = new System.Windows.Forms.Button();
      this.label2 = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.label5 = new System.Windows.Forms.Label();
      this.iconFileDialog = new System.Windows.Forms.OpenFileDialog();
      this.saveButton = new System.Windows.Forms.Button();
      ( (System.ComponentModel.ISupportInitialize)( this.collIconPictureBox ) ).BeginInit();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point( 22, 145 );
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size( 58, 13 );
      this.label1.TabIndex = 0;
      this.label1.Text = "Resources";
      // 
      // resListView
      // 
      this.resListView.Columns.AddRange( new System.Windows.Forms.ColumnHeader[] {
            this.fileColumnHeader,
            this.enabledColumnHeader,
            this.typeColumnHeader} );
      this.resListView.Items.AddRange( new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2} );
      this.resListView.Location = new System.Drawing.Point( 25, 161 );
      this.resListView.Name = "resListView";
      this.resListView.Size = new System.Drawing.Size( 367, 423 );
      this.resListView.TabIndex = 1;
      this.resListView.UseCompatibleStateImageBehavior = false;
      this.resListView.View = System.Windows.Forms.View.Details;
      this.resListView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler( this.SortList );
      // 
      // fileColumnHeader
      // 
      this.fileColumnHeader.Text = "Filename";
      // 
      // enabledColumnHeader
      // 
      this.enabledColumnHeader.Text = "Enabled";
      // 
      // typeColumnHeader
      // 
      this.typeColumnHeader.Text = "Type";
      // 
      // scanButton
      // 
      this.scanButton.Font = new System.Drawing.Font( "Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( (byte)( 238 ) ) );
      this.scanButton.Location = new System.Drawing.Point( 351, 23 );
      this.scanButton.Name = "scanButton";
      this.scanButton.Size = new System.Drawing.Size( 75, 23 );
      this.scanButton.TabIndex = 2;
      this.scanButton.Text = "Scan";
      this.toolTip1.SetToolTip( this.scanButton, "Scan selected directory for appropriate packages" );
      this.scanButton.UseVisualStyleBackColor = true;
      this.scanButton.Click += new System.EventHandler( this.Scan );
      // 
      // lbscandebug
      // 
      this.lbscandebug.HorizontalScrollbar = true;
      this.lbscandebug.Location = new System.Drawing.Point( 25, 594 );
      this.lbscandebug.Name = "lbscandebug";
      this.lbscandebug.Size = new System.Drawing.Size( 259, 17 );
      this.lbscandebug.TabIndex = 7;
      // 
      // lbid
      // 
      this.lbid.HorizontalScrollbar = true;
      this.lbid.Location = new System.Drawing.Point( 290, 594 );
      this.lbid.Name = "lbid";
      this.lbid.Size = new System.Drawing.Size( 259, 17 );
      this.lbid.TabIndex = 6;
      // 
      // cbrec
      // 
      this.cbrec.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.cbrec.Location = new System.Drawing.Point( 351, 52 );
      this.cbrec.Name = "cbrec";
      this.cbrec.Size = new System.Drawing.Size( 80, 24 );
      this.cbrec.TabIndex = 8;
      this.cbrec.Text = "Recursive";
      this.toolTip1.SetToolTip( this.cbrec, "Indicate scan to go into subdirectories" );
      // 
      // cbfolder
      // 
      this.cbfolder.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
                  | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.cbfolder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbfolder.Items.AddRange( new object[] {
            "Download Folder",
            "..."} );
      this.cbfolder.Location = new System.Drawing.Point( 25, 23 );
      this.cbfolder.Name = "cbfolder";
      this.cbfolder.Size = new System.Drawing.Size( 320, 21 );
      this.cbfolder.TabIndex = 9;
      this.cbfolder.SelectedIndexChanged += new System.EventHandler( this.SelectFolder );
      // 
      // fbd
      // 
      this.fbd.ShowNewFolderButton = false;
      // 
      // collListView
      // 
      this.collListView.Columns.AddRange( new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3} );
      this.collListView.Location = new System.Drawing.Point( 444, 161 );
      this.collListView.Name = "collListView";
      this.collListView.Size = new System.Drawing.Size( 367, 423 );
      this.collListView.TabIndex = 10;
      this.collListView.UseCompatibleStateImageBehavior = false;
      this.collListView.View = System.Windows.Forms.View.Details;
      // 
      // columnHeader1
      // 
      this.columnHeader1.Text = "Filename";
      // 
      // columnHeader2
      // 
      this.columnHeader2.Text = "Enabled";
      // 
      // columnHeader3
      // 
      this.columnHeader3.Text = "Type";
      // 
      // toCollButton
      // 
      this.toCollButton.Font = new System.Drawing.Font( "Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( (byte)( 238 ) ) );
      this.toCollButton.Location = new System.Drawing.Point( 398, 214 );
      this.toCollButton.Name = "toCollButton";
      this.toCollButton.Size = new System.Drawing.Size( 39, 23 );
      this.toCollButton.TabIndex = 11;
      this.toCollButton.Text = ">";
      this.toolTip1.SetToolTip( this.toCollButton, "Move selected items to collection" );
      this.toCollButton.UseVisualStyleBackColor = true;
      this.toCollButton.Click += new System.EventHandler( this.MoveToCollection );
      // 
      // allToCollButton
      // 
      this.allToCollButton.Font = new System.Drawing.Font( "Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( (byte)( 238 ) ) );
      this.allToCollButton.Location = new System.Drawing.Point( 399, 264 );
      this.allToCollButton.Name = "allToCollButton";
      this.allToCollButton.Size = new System.Drawing.Size( 39, 23 );
      this.allToCollButton.TabIndex = 12;
      this.allToCollButton.Text = ">>";
      this.toolTip1.SetToolTip( this.allToCollButton, "Move all resource items to collection" );
      this.allToCollButton.UseVisualStyleBackColor = true;
      this.allToCollButton.Click += new System.EventHandler( this.MoveAllToCollection );
      // 
      // removeAllFromCollButton
      // 
      this.removeAllFromCollButton.Font = new System.Drawing.Font( "Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( (byte)( 238 ) ) );
      this.removeAllFromCollButton.Location = new System.Drawing.Point( 399, 398 );
      this.removeAllFromCollButton.Name = "removeAllFromCollButton";
      this.removeAllFromCollButton.Size = new System.Drawing.Size( 39, 23 );
      this.removeAllFromCollButton.TabIndex = 14;
      this.removeAllFromCollButton.Text = "<<";
      this.toolTip1.SetToolTip( this.removeAllFromCollButton, "Remove all items from collection" );
      this.removeAllFromCollButton.UseVisualStyleBackColor = true;
      this.removeAllFromCollButton.Click += new System.EventHandler( this.RemoveAllFromCollection );
      // 
      // remFromCollButton
      // 
      this.remFromCollButton.Font = new System.Drawing.Font( "Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( (byte)( 238 ) ) );
      this.remFromCollButton.Location = new System.Drawing.Point( 398, 348 );
      this.remFromCollButton.Name = "remFromCollButton";
      this.remFromCollButton.Size = new System.Drawing.Size( 39, 23 );
      this.remFromCollButton.TabIndex = 13;
      this.remFromCollButton.Text = "<";
      this.toolTip1.SetToolTip( this.remFromCollButton, "Remove selected items from collection" );
      this.remFromCollButton.UseVisualStyleBackColor = true;
      this.remFromCollButton.Click += new System.EventHandler( this.RemoveFromCollection );
      // 
      // collToolTipTextBox
      // 
      this.collToolTipTextBox.Location = new System.Drawing.Point( 564, 30 );
      this.collToolTipTextBox.Name = "collToolTipTextBox";
      this.collToolTipTextBox.Size = new System.Drawing.Size( 247, 20 );
      this.collToolTipTextBox.TabIndex = 19;
      this.toolTip1.SetToolTip( this.collToolTipTextBox, "This will be shown in Sims2 as yor tooltip in collection browser" );
      // 
      // collIconPictureBox
      // 
      this.collIconPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.collIconPictureBox.Location = new System.Drawing.Point( 564, 68 );
      this.collIconPictureBox.Name = "collIconPictureBox";
      this.collIconPictureBox.Size = new System.Drawing.Size( 32, 32 );
      this.collIconPictureBox.TabIndex = 21;
      this.collIconPictureBox.TabStop = false;
      this.toolTip1.SetToolTip( this.collIconPictureBox, "This is your collection icon" );
      // 
      // collIconButton
      // 
      this.collIconButton.Font = new System.Drawing.Font( "Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( (byte)( 238 ) ) );
      this.collIconButton.Location = new System.Drawing.Point( 611, 77 );
      this.collIconButton.Name = "collIconButton";
      this.collIconButton.Size = new System.Drawing.Size( 32, 23 );
      this.collIconButton.TabIndex = 22;
      this.collIconButton.Text = "...";
      this.toolTip1.SetToolTip( this.collIconButton, "Select collection icon file" );
      this.collIconButton.UseVisualStyleBackColor = true;
      this.collIconButton.Click += new System.EventHandler( this.SelectIcon );
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point( 441, 145 );
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size( 53, 13 );
      this.label2.TabIndex = 15;
      this.label2.Text = "Collection";
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point( 470, 30 );
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size( 85, 13 );
      this.label4.TabIndex = 18;
      this.label4.Text = "Collection name:";
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point( 470, 80 );
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size( 79, 13 );
      this.label5.TabIndex = 20;
      this.label5.Text = "Collection icon:";
      // 
      // iconFileDialog
      // 
      this.iconFileDialog.FileName = "openFileDialog1";
      // 
      // saveButton
      // 
      this.saveButton.Location = new System.Drawing.Point( 736, 590 );
      this.saveButton.Name = "saveButton";
      this.saveButton.Size = new System.Drawing.Size( 75, 23 );
      this.saveButton.TabIndex = 23;
      this.saveButton.Text = "Save";
      this.saveButton.UseVisualStyleBackColor = true;
      this.saveButton.Click += new System.EventHandler( this.SaveCollection );
      // 
      // CollWspForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size( 901, 624 );
      this.Controls.Add( this.saveButton );
      this.Controls.Add( this.collIconButton );
      this.Controls.Add( this.collIconPictureBox );
      this.Controls.Add( this.label5 );
      this.Controls.Add( this.collToolTipTextBox );
      this.Controls.Add( this.label4 );
      this.Controls.Add( this.label2 );
      this.Controls.Add( this.removeAllFromCollButton );
      this.Controls.Add( this.remFromCollButton );
      this.Controls.Add( this.allToCollButton );
      this.Controls.Add( this.toCollButton );
      this.Controls.Add( this.collListView );
      this.Controls.Add( this.cbfolder );
      this.Controls.Add( this.cbrec );
      this.Controls.Add( this.lbscandebug );
      this.Controls.Add( this.lbid );
      this.Controls.Add( this.scanButton );
      this.Controls.Add( this.resListView );
      this.Controls.Add( this.label1 );
      this.Name = "CollWspForm";
      this.Text = "CollWspForm";
      ( (System.ComponentModel.ISupportInitialize)( this.collIconPictureBox ) ).EndInit();
      this.ResumeLayout( false );
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.ListView resListView;
    private System.Windows.Forms.Button scanButton;
    private System.Windows.Forms.ColumnHeader fileColumnHeader;
    private System.Windows.Forms.ColumnHeader enabledColumnHeader;
    private System.Windows.Forms.ColumnHeader typeColumnHeader;
    private System.Windows.Forms.ListBox lbscandebug;
    private System.Windows.Forms.ListBox lbid;
    private System.Windows.Forms.CheckBox cbrec;
    private System.Windows.Forms.ComboBox cbfolder;
    private System.Windows.Forms.FolderBrowserDialog fbd;
    private System.Windows.Forms.ListView collListView;
    private System.Windows.Forms.ColumnHeader columnHeader1;
    private System.Windows.Forms.ColumnHeader columnHeader2;
    private System.Windows.Forms.ColumnHeader columnHeader3;
    private System.Windows.Forms.Button toCollButton;
    private System.Windows.Forms.ToolTip toolTip1;
    private System.Windows.Forms.Button allToCollButton;
    private System.Windows.Forms.Button removeAllFromCollButton;
    private System.Windows.Forms.Button remFromCollButton;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.TextBox collToolTipTextBox;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.PictureBox collIconPictureBox;
    private System.Windows.Forms.Button collIconButton;
    private System.Windows.Forms.OpenFileDialog iconFileDialog;
    private System.Windows.Forms.Button saveButton;
  }
}