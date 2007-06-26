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
       System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager( typeof( CollWspForm ) );
       this.resListView = new System.Windows.Forms.ListView();
       this.fileColumnHeader = new System.Windows.Forms.ColumnHeader();
       this.ilist = new System.Windows.Forms.ImageList( this.components );
       this.scanButton = new System.Windows.Forms.Button();
       this.cbrec = new System.Windows.Forms.CheckBox();
       this.cbfolder = new System.Windows.Forms.ComboBox();
       this.fbd = new System.Windows.Forms.FolderBrowserDialog();
       this.collListView = new System.Windows.Forms.ListView();
       this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
       this.toCollButton = new System.Windows.Forms.Button();
       this.toolTip1 = new System.Windows.Forms.ToolTip( this.components );
       this.allToCollButton = new System.Windows.Forms.Button();
       this.removeAllFromCollButton = new System.Windows.Forms.Button();
       this.remFromCollButton = new System.Windows.Forms.Button();
       this.collNameTextBox = new System.Windows.Forms.TextBox();
       this.collIconPictureBox = new System.Windows.Forms.PictureBox();
       this.collIconButton = new System.Windows.Forms.Button();
       this.moveUpButton = new System.Windows.Forms.Button();
       this.moveTopButton = new System.Windows.Forms.Button();
       this.moveDownButton = new System.Windows.Forms.Button();
       this.moveBottomButton = new System.Windows.Forms.Button();
       this.label4 = new System.Windows.Forms.Label();
       this.label5 = new System.Windows.Forms.Label();
       this.iconFileDialog = new System.Windows.Forms.OpenFileDialog();
       this.saveButton = new System.Windows.Forms.Button();
       this.rightTabControl = new System.Windows.Forms.TabControl();
       this.collTabPage = new System.Windows.Forms.TabPage();
       this.resTabPage = new System.Windows.Forms.TabPage();
       this.propertyListView = new System.Windows.Forms.ListView();
       this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
       this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
       this.label1 = new System.Windows.Forms.Label();
       this.imagePictureBox = new System.Windows.Forms.PictureBox();
       this.resTabControl = new System.Windows.Forms.TabControl();
       this.tabPage1 = new System.Windows.Forms.TabPage();
       this.clothFiltersTabPage = new System.Windows.Forms.TabPage();
       this.groupBox2 = new System.Windows.Forms.GroupBox();
       this.maskCheckBox = new System.Windows.Forms.CheckBox();
       this.hatCheckBox = new System.Windows.Forms.CheckBox();
       this.otherCheckBox = new System.Windows.Forms.CheckBox();
       this.bodyCheckBox = new System.Windows.Forms.CheckBox();
       this.bottomCheckBox = new System.Windows.Forms.CheckBox();
       this.topCheckBox = new System.Windows.Forms.CheckBox();
       this.ageFilterListView = new System.Windows.Forms.ListView();
       this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
       this.categoryFilterListView = new System.Windows.Forms.ListView();
       this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
       this.resetClothFilterButton = new System.Windows.Forms.Button();
       this.applyFilterButton = new System.Windows.Forms.Button();
       this.groupBox1 = new System.Windows.Forms.GroupBox();
       this.bothCheckBox = new System.Windows.Forms.CheckBox();
       this.femaleOnlyCheckBox = new System.Windows.Forms.CheckBox();
       this.maleOnlyCheckBox = new System.Windows.Forms.CheckBox();
       this.objectFiltersTabPage = new System.Windows.Forms.TabPage();
       this.functionFilterListView = new System.Windows.Forms.ListView();
       this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
       this.roomFilterListView = new System.Windows.Forms.ListView();
       this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
       this.resetObjectFilterButton = new System.Windows.Forms.Button();
       this.button2 = new System.Windows.Forms.Button();
       this.pb = new System.Windows.Forms.ProgressBar();
       this.collTypeComboBox = new System.Windows.Forms.ComboBox();
       this.label2 = new System.Windows.Forms.Label();
       ( (System.ComponentModel.ISupportInitialize)( this.collIconPictureBox ) ).BeginInit();
       this.rightTabControl.SuspendLayout();
       this.collTabPage.SuspendLayout();
       this.resTabPage.SuspendLayout();
       ( (System.ComponentModel.ISupportInitialize)( this.imagePictureBox ) ).BeginInit();
       this.resTabControl.SuspendLayout();
       this.tabPage1.SuspendLayout();
       this.clothFiltersTabPage.SuspendLayout();
       this.groupBox2.SuspendLayout();
       this.groupBox1.SuspendLayout();
       this.objectFiltersTabPage.SuspendLayout();
       this.SuspendLayout();
       // 
       // resListView
       // 
       this.resListView.AllowColumnReorder = true;
       this.resListView.Columns.AddRange( new System.Windows.Forms.ColumnHeader[] {
            this.fileColumnHeader} );
       this.resListView.FullRowSelect = true;
       this.resListView.LargeImageList = this.ilist;
       this.resListView.Location = new System.Drawing.Point( 6, 6 );
       this.resListView.Name = "resListView";
       this.resListView.Size = new System.Drawing.Size( 422, 482 );
       this.resListView.TabIndex = 1;
       this.resListView.UseCompatibleStateImageBehavior = false;
       this.resListView.View = System.Windows.Forms.View.Details;
       this.resListView.SelectedIndexChanged += new System.EventHandler( this.SelectItem );
       this.resListView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler( this.SortList );
       // 
       // fileColumnHeader
       // 
       this.fileColumnHeader.Text = "Filename";
       this.fileColumnHeader.Width = 72;
       // 
       // ilist
       // 
       this.ilist.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
       this.ilist.ImageSize = new System.Drawing.Size( 48, 48 );
       this.ilist.TransparentColor = System.Drawing.Color.Transparent;
       // 
       // scanButton
       // 
       this.scanButton.Font = new System.Drawing.Font( "Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte)( 238 ) ) );
       this.scanButton.Location = new System.Drawing.Point( 378, 27 );
       this.scanButton.Name = "scanButton";
       this.scanButton.Size = new System.Drawing.Size( 75, 21 );
       this.scanButton.TabIndex = 2;
       this.scanButton.Text = "Scan";
       this.toolTip1.SetToolTip( this.scanButton, "Scan selected directory for appropriate packages" );
       this.scanButton.UseVisualStyleBackColor = true;
       this.scanButton.Click += new System.EventHandler( this.Scan );
       // 
       // cbrec
       // 
       this.cbrec.FlatStyle = System.Windows.Forms.FlatStyle.System;
       this.cbrec.Location = new System.Drawing.Point( 378, 56 );
       this.cbrec.Name = "cbrec";
       this.cbrec.Size = new System.Drawing.Size( 75, 24 );
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
            "Select Folder ...",
            "Scan Maxis Items"} );
       this.cbfolder.Location = new System.Drawing.Point( 11, 27 );
       this.cbfolder.MaximumSize = new System.Drawing.Size( 348, 0 );
       this.cbfolder.Name = "cbfolder";
       this.cbfolder.Size = new System.Drawing.Size( 348, 21 );
       this.cbfolder.TabIndex = 9;
       this.toolTip1.SetToolTip( this.cbfolder, "Select a folder to scan for resources" );
       this.cbfolder.SelectedIndexChanged += new System.EventHandler( this.SelectFolder );
       // 
       // fbd
       // 
       this.fbd.ShowNewFolderButton = false;
       // 
       // collListView
       // 
       this.collListView.Columns.AddRange( new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1} );
       this.collListView.Location = new System.Drawing.Point( 6, 6 );
       this.collListView.Name = "collListView";
       this.collListView.Size = new System.Drawing.Size( 373, 482 );
       this.collListView.TabIndex = 10;
       this.collListView.UseCompatibleStateImageBehavior = false;
       this.collListView.View = System.Windows.Forms.View.Details;
       // 
       // columnHeader1
       // 
       this.columnHeader1.Text = "Filename";
       this.columnHeader1.Width = 91;
       // 
       // toCollButton
       // 
       this.toCollButton.Font = new System.Drawing.Font( "Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( (byte)( 238 ) ) );
       this.toCollButton.Location = new System.Drawing.Point( 474, 214 );
       this.toCollButton.Name = "toCollButton";
       this.toCollButton.Size = new System.Drawing.Size( 39, 31 );
       this.toCollButton.TabIndex = 11;
       this.toCollButton.Text = ">";
       this.toolTip1.SetToolTip( this.toCollButton, "Move selected items to collection" );
       this.toCollButton.UseVisualStyleBackColor = true;
       this.toCollButton.Click += new System.EventHandler( this.MoveToCollection );
       // 
       // allToCollButton
       // 
       this.allToCollButton.Font = new System.Drawing.Font( "Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( (byte)( 238 ) ) );
       this.allToCollButton.Location = new System.Drawing.Point( 475, 264 );
       this.allToCollButton.Name = "allToCollButton";
       this.allToCollButton.Size = new System.Drawing.Size( 39, 31 );
       this.allToCollButton.TabIndex = 12;
       this.allToCollButton.Text = ">>";
       this.toolTip1.SetToolTip( this.allToCollButton, "Move all resource items to collection" );
       this.allToCollButton.UseVisualStyleBackColor = true;
       this.allToCollButton.Click += new System.EventHandler( this.MoveAllToCollection );
       // 
       // removeAllFromCollButton
       // 
       this.removeAllFromCollButton.Font = new System.Drawing.Font( "Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( (byte)( 238 ) ) );
       this.removeAllFromCollButton.Location = new System.Drawing.Point( 475, 398 );
       this.removeAllFromCollButton.Name = "removeAllFromCollButton";
       this.removeAllFromCollButton.Size = new System.Drawing.Size( 39, 31 );
       this.removeAllFromCollButton.TabIndex = 14;
       this.removeAllFromCollButton.Text = "<<";
       this.toolTip1.SetToolTip( this.removeAllFromCollButton, "Remove all items from collection" );
       this.removeAllFromCollButton.UseVisualStyleBackColor = true;
       this.removeAllFromCollButton.Click += new System.EventHandler( this.RemoveAllFromCollection );
       // 
       // remFromCollButton
       // 
       this.remFromCollButton.Font = new System.Drawing.Font( "Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( (byte)( 238 ) ) );
       this.remFromCollButton.Location = new System.Drawing.Point( 474, 348 );
       this.remFromCollButton.Name = "remFromCollButton";
       this.remFromCollButton.Size = new System.Drawing.Size( 39, 31 );
       this.remFromCollButton.TabIndex = 13;
       this.remFromCollButton.Text = "<";
       this.toolTip1.SetToolTip( this.remFromCollButton, "Remove selected items from collection" );
       this.remFromCollButton.UseVisualStyleBackColor = true;
       this.remFromCollButton.Click += new System.EventHandler( this.RemoveFromCollection );
       // 
       // collNameTextBox
       // 
       this.collNameTextBox.Location = new System.Drawing.Point( 712, 80 );
       this.collNameTextBox.Name = "collNameTextBox";
       this.collNameTextBox.Size = new System.Drawing.Size( 254, 20 );
       this.collNameTextBox.TabIndex = 19;
       this.toolTip1.SetToolTip( this.collNameTextBox, "This will be shown in Sims2 as yor tooltip in collection browser" );
       // 
       // collIconPictureBox
       // 
       this.collIconPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
       this.collIconPictureBox.Location = new System.Drawing.Point( 619, 68 );
       this.collIconPictureBox.Name = "collIconPictureBox";
       this.collIconPictureBox.Size = new System.Drawing.Size( 32, 32 );
       this.collIconPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
       this.collIconPictureBox.TabIndex = 21;
       this.collIconPictureBox.TabStop = false;
       this.toolTip1.SetToolTip( this.collIconPictureBox, "This is your collection icon, should be 28x21 px for cloth collection and 32x32px" +
               " for other object collections." );
       // 
       // collIconButton
       // 
       this.collIconButton.Font = new System.Drawing.Font( "Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( (byte)( 238 ) ) );
       this.collIconButton.Location = new System.Drawing.Point( 661, 80 );
       this.collIconButton.Name = "collIconButton";
       this.collIconButton.Size = new System.Drawing.Size( 32, 20 );
       this.collIconButton.TabIndex = 22;
       this.collIconButton.Text = "...";
       this.toolTip1.SetToolTip( this.collIconButton, "Select collection icon file. Should be 28x21 px for cloth collection and 32x32px " +
               "for other object collections." );
       this.collIconButton.UseVisualStyleBackColor = true;
       this.collIconButton.Click += new System.EventHandler( this.SelectIcon );
       // 
       // moveUpButton
       // 
       this.moveUpButton.Image = ( (System.Drawing.Image)( resources.GetObject( "moveUpButton.Image" ) ) );
       this.moveUpButton.Location = new System.Drawing.Point( 385, 130 );
       this.moveUpButton.Name = "moveUpButton";
       this.moveUpButton.Size = new System.Drawing.Size( 35, 31 );
       this.moveUpButton.TabIndex = 11;
       this.toolTip1.SetToolTip( this.moveUpButton, "Move selected items up." );
       this.moveUpButton.UseVisualStyleBackColor = true;
       this.moveUpButton.Click += new System.EventHandler( this.MoveUp );
       // 
       // moveTopButton
       // 
       this.moveTopButton.Image = global::SimPe.Plugin.Images.MoveTop;
       this.moveTopButton.Location = new System.Drawing.Point( 385, 80 );
       this.moveTopButton.Name = "moveTopButton";
       this.moveTopButton.Size = new System.Drawing.Size( 35, 31 );
       this.moveTopButton.TabIndex = 12;
       this.toolTip1.SetToolTip( this.moveTopButton, "Move selected items to top." );
       this.moveTopButton.UseVisualStyleBackColor = true;
       this.moveTopButton.Click += new System.EventHandler( this.MoveTop );
       // 
       // moveDownButton
       // 
       this.moveDownButton.Image = global::SimPe.Plugin.Images.MoveDown;
       this.moveDownButton.Location = new System.Drawing.Point( 385, 214 );
       this.moveDownButton.Name = "moveDownButton";
       this.moveDownButton.Size = new System.Drawing.Size( 35, 31 );
       this.moveDownButton.TabIndex = 14;
       this.toolTip1.SetToolTip( this.moveDownButton, "Move selected items down." );
       this.moveDownButton.UseVisualStyleBackColor = true;
       this.moveDownButton.Click += new System.EventHandler( this.MoveDown );
       // 
       // moveBottomButton
       // 
       this.moveBottomButton.Image = global::SimPe.Plugin.Images.MoveBottom;
       this.moveBottomButton.Location = new System.Drawing.Point( 385, 264 );
       this.moveBottomButton.Name = "moveBottomButton";
       this.moveBottomButton.Size = new System.Drawing.Size( 35, 31 );
       this.moveBottomButton.TabIndex = 13;
       this.toolTip1.SetToolTip( this.moveBottomButton, "Move selected items up." );
       this.moveBottomButton.UseVisualStyleBackColor = true;
       this.moveBottomButton.Click += new System.EventHandler( this.MoveBottom );
       // 
       // label4
       // 
       this.label4.AutoSize = true;
       this.label4.Location = new System.Drawing.Point( 709, 61 );
       this.label4.Name = "label4";
       this.label4.Size = new System.Drawing.Size( 85, 13 );
       this.label4.TabIndex = 18;
       this.label4.Text = "Collection name:";
       // 
       // label5
       // 
       this.label5.AutoSize = true;
       this.label5.Location = new System.Drawing.Point( 531, 87 );
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
       this.saveButton.Location = new System.Drawing.Point( 891, 639 );
       this.saveButton.Name = "saveButton";
       this.saveButton.Size = new System.Drawing.Size( 75, 23 );
       this.saveButton.TabIndex = 23;
       this.saveButton.Text = "Save";
       this.saveButton.UseVisualStyleBackColor = true;
       this.saveButton.Click += new System.EventHandler( this.SaveCollection );
       // 
       // rightTabControl
       // 
       this.rightTabControl.Controls.Add( this.collTabPage );
       this.rightTabControl.Controls.Add( this.resTabPage );
       this.rightTabControl.Location = new System.Drawing.Point( 531, 112 );
       this.rightTabControl.Name = "rightTabControl";
       this.rightTabControl.SelectedIndex = 0;
       this.rightTabControl.Size = new System.Drawing.Size( 435, 520 );
       this.rightTabControl.TabIndex = 25;
       // 
       // collTabPage
       // 
       this.collTabPage.Controls.Add( this.moveDownButton );
       this.collTabPage.Controls.Add( this.moveBottomButton );
       this.collTabPage.Controls.Add( this.moveTopButton );
       this.collTabPage.Controls.Add( this.moveUpButton );
       this.collTabPage.Controls.Add( this.collListView );
       this.collTabPage.Location = new System.Drawing.Point( 4, 22 );
       this.collTabPage.Name = "collTabPage";
       this.collTabPage.Padding = new System.Windows.Forms.Padding( 3 );
       this.collTabPage.Size = new System.Drawing.Size( 427, 494 );
       this.collTabPage.TabIndex = 0;
       this.collTabPage.Text = "Collection";
       this.collTabPage.ToolTipText = "Construct and sort yor collection items";
       this.collTabPage.UseVisualStyleBackColor = true;
       // 
       // resTabPage
       // 
       this.resTabPage.Controls.Add( this.propertyListView );
       this.resTabPage.Controls.Add( this.label1 );
       this.resTabPage.Controls.Add( this.imagePictureBox );
       this.resTabPage.Location = new System.Drawing.Point( 4, 22 );
       this.resTabPage.Name = "resTabPage";
       this.resTabPage.Padding = new System.Windows.Forms.Padding( 3 );
       this.resTabPage.Size = new System.Drawing.Size( 427, 494 );
       this.resTabPage.TabIndex = 1;
       this.resTabPage.Text = "Resource details";
       this.resTabPage.ToolTipText = "Show details of selected resource";
       this.resTabPage.UseVisualStyleBackColor = true;
       // 
       // propertyListView
       // 
       this.propertyListView.Columns.AddRange( new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader4,
            this.columnHeader5} );
       this.propertyListView.Location = new System.Drawing.Point( 6, 181 );
       this.propertyListView.MultiSelect = false;
       this.propertyListView.Name = "propertyListView";
       this.propertyListView.Size = new System.Drawing.Size( 415, 259 );
       this.propertyListView.TabIndex = 2;
       this.propertyListView.UseCompatibleStateImageBehavior = false;
       this.propertyListView.View = System.Windows.Forms.View.Details;
       // 
       // columnHeader4
       // 
       this.columnHeader4.Text = "Property";
       this.columnHeader4.Width = 92;
       // 
       // columnHeader5
       // 
       this.columnHeader5.Text = "Value";
       this.columnHeader5.Width = 308;
       // 
       // label1
       // 
       this.label1.AutoSize = true;
       this.label1.Location = new System.Drawing.Point( 19, 25 );
       this.label1.Name = "label1";
       this.label1.Size = new System.Drawing.Size( 43, 13 );
       this.label1.TabIndex = 1;
       this.label1.Text = "Picture:";
       // 
       // imagePictureBox
       // 
       this.imagePictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
       this.imagePictureBox.Location = new System.Drawing.Point( 84, 25 );
       this.imagePictureBox.Name = "imagePictureBox";
       this.imagePictureBox.Size = new System.Drawing.Size( 128, 128 );
       this.imagePictureBox.TabIndex = 0;
       this.imagePictureBox.TabStop = false;
       // 
       // resTabControl
       // 
       this.resTabControl.Controls.Add( this.tabPage1 );
       this.resTabControl.Controls.Add( this.clothFiltersTabPage );
       this.resTabControl.Controls.Add( this.objectFiltersTabPage );
       this.resTabControl.Font = new System.Drawing.Font( "Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte)( 238 ) ) );
       this.resTabControl.Location = new System.Drawing.Point( 11, 112 );
       this.resTabControl.Name = "resTabControl";
       this.resTabControl.SelectedIndex = 0;
       this.resTabControl.Size = new System.Drawing.Size( 442, 520 );
       this.resTabControl.TabIndex = 26;
       // 
       // tabPage1
       // 
       this.tabPage1.Controls.Add( this.resListView );
       this.tabPage1.Location = new System.Drawing.Point( 4, 22 );
       this.tabPage1.Name = "tabPage1";
       this.tabPage1.Padding = new System.Windows.Forms.Padding( 3 );
       this.tabPage1.Size = new System.Drawing.Size( 434, 494 );
       this.tabPage1.TabIndex = 0;
       this.tabPage1.Text = "Resources";
       this.tabPage1.UseVisualStyleBackColor = true;
       // 
       // clothFiltersTabPage
       // 
       this.clothFiltersTabPage.Controls.Add( this.groupBox2 );
       this.clothFiltersTabPage.Controls.Add( this.ageFilterListView );
       this.clothFiltersTabPage.Controls.Add( this.categoryFilterListView );
       this.clothFiltersTabPage.Controls.Add( this.resetClothFilterButton );
       this.clothFiltersTabPage.Controls.Add( this.applyFilterButton );
       this.clothFiltersTabPage.Controls.Add( this.groupBox1 );
       this.clothFiltersTabPage.Font = new System.Drawing.Font( "Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte)( 238 ) ) );
       this.clothFiltersTabPage.Location = new System.Drawing.Point( 4, 22 );
       this.clothFiltersTabPage.Name = "clothFiltersTabPage";
       this.clothFiltersTabPage.Padding = new System.Windows.Forms.Padding( 3 );
       this.clothFiltersTabPage.Size = new System.Drawing.Size( 434, 494 );
       this.clothFiltersTabPage.TabIndex = 1;
       this.clothFiltersTabPage.Text = "Clothing Filters";
       this.clothFiltersTabPage.UseVisualStyleBackColor = true;
       // 
       // groupBox2
       // 
       this.groupBox2.Controls.Add( this.maskCheckBox );
       this.groupBox2.Controls.Add( this.hatCheckBox );
       this.groupBox2.Controls.Add( this.otherCheckBox );
       this.groupBox2.Controls.Add( this.bodyCheckBox );
       this.groupBox2.Controls.Add( this.bottomCheckBox );
       this.groupBox2.Controls.Add( this.topCheckBox );
       this.groupBox2.Location = new System.Drawing.Point( 16, 320 );
       this.groupBox2.Name = "groupBox2";
       this.groupBox2.Size = new System.Drawing.Size( 403, 71 );
       this.groupBox2.TabIndex = 5;
       this.groupBox2.TabStop = false;
       this.groupBox2.Text = "Cloth type";
       // 
       // maskCheckBox
       // 
       this.maskCheckBox.AutoSize = true;
       this.maskCheckBox.Location = new System.Drawing.Point( 269, 30 );
       this.maskCheckBox.Name = "maskCheckBox";
       this.maskCheckBox.Size = new System.Drawing.Size( 52, 17 );
       this.maskCheckBox.TabIndex = 5;
       this.maskCheckBox.Text = "Mask";
       this.maskCheckBox.UseVisualStyleBackColor = true;
       // 
       // hatCheckBox
       // 
       this.hatCheckBox.AutoSize = true;
       this.hatCheckBox.Location = new System.Drawing.Point( 269, 13 );
       this.hatCheckBox.Name = "hatCheckBox";
       this.hatCheckBox.Size = new System.Drawing.Size( 43, 17 );
       this.hatCheckBox.TabIndex = 4;
       this.hatCheckBox.Text = "Hat";
       this.hatCheckBox.UseVisualStyleBackColor = true;
       // 
       // otherCheckBox
       // 
       this.otherCheckBox.AutoSize = true;
       this.otherCheckBox.Location = new System.Drawing.Point( 269, 47 );
       this.otherCheckBox.Name = "otherCheckBox";
       this.otherCheckBox.Size = new System.Drawing.Size( 52, 17 );
       this.otherCheckBox.TabIndex = 3;
       this.otherCheckBox.Text = "Other";
       this.otherCheckBox.UseVisualStyleBackColor = true;
       // 
       // bodyCheckBox
       // 
       this.bodyCheckBox.AutoSize = true;
       this.bodyCheckBox.Location = new System.Drawing.Point( 62, 47 );
       this.bodyCheckBox.Name = "bodyCheckBox";
       this.bodyCheckBox.Size = new System.Drawing.Size( 68, 17 );
       this.bodyCheckBox.TabIndex = 2;
       this.bodyCheckBox.Text = "Full body";
       this.bodyCheckBox.UseVisualStyleBackColor = true;
       // 
       // bottomCheckBox
       // 
       this.bottomCheckBox.AutoSize = true;
       this.bottomCheckBox.Location = new System.Drawing.Point( 62, 30 );
       this.bottomCheckBox.Name = "bottomCheckBox";
       this.bottomCheckBox.Size = new System.Drawing.Size( 59, 17 );
       this.bottomCheckBox.TabIndex = 1;
       this.bottomCheckBox.Text = "Bottom";
       this.bottomCheckBox.UseVisualStyleBackColor = true;
       // 
       // topCheckBox
       // 
       this.topCheckBox.AutoSize = true;
       this.topCheckBox.Location = new System.Drawing.Point( 62, 13 );
       this.topCheckBox.Name = "topCheckBox";
       this.topCheckBox.Size = new System.Drawing.Size( 45, 17 );
       this.topCheckBox.TabIndex = 0;
       this.topCheckBox.Text = "Top";
       this.topCheckBox.UseVisualStyleBackColor = true;
       // 
       // ageFilterListView
       // 
       this.ageFilterListView.CheckBoxes = true;
       this.ageFilterListView.Columns.AddRange( new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader7} );
       this.ageFilterListView.Location = new System.Drawing.Point( 232, 75 );
       this.ageFilterListView.Name = "ageFilterListView";
       this.ageFilterListView.Size = new System.Drawing.Size( 187, 231 );
       this.ageFilterListView.TabIndex = 4;
       this.ageFilterListView.UseCompatibleStateImageBehavior = false;
       this.ageFilterListView.View = System.Windows.Forms.View.Details;
       // 
       // columnHeader7
       // 
       this.columnHeader7.Text = "Age";
       this.columnHeader7.Width = 177;
       // 
       // categoryFilterListView
       // 
       this.categoryFilterListView.CheckBoxes = true;
       this.categoryFilterListView.Columns.AddRange( new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader6} );
       this.categoryFilterListView.Location = new System.Drawing.Point( 16, 75 );
       this.categoryFilterListView.Name = "categoryFilterListView";
       this.categoryFilterListView.Size = new System.Drawing.Size( 187, 231 );
       this.categoryFilterListView.TabIndex = 3;
       this.categoryFilterListView.UseCompatibleStateImageBehavior = false;
       this.categoryFilterListView.View = System.Windows.Forms.View.Details;
       // 
       // columnHeader6
       // 
       this.columnHeader6.Text = "Category";
       this.columnHeader6.Width = 178;
       // 
       // resetClothFilterButton
       // 
       this.resetClothFilterButton.Enabled = false;
       this.resetClothFilterButton.Location = new System.Drawing.Point( 256, 404 );
       this.resetClothFilterButton.Name = "resetClothFilterButton";
       this.resetClothFilterButton.Size = new System.Drawing.Size( 75, 23 );
       this.resetClothFilterButton.TabIndex = 2;
       this.resetClothFilterButton.Text = "Reset";
       this.resetClothFilterButton.UseVisualStyleBackColor = true;
       this.resetClothFilterButton.Click += new System.EventHandler( this.ResetFilters );
       // 
       // applyFilterButton
       // 
       this.applyFilterButton.Location = new System.Drawing.Point( 344, 404 );
       this.applyFilterButton.Name = "applyFilterButton";
       this.applyFilterButton.Size = new System.Drawing.Size( 75, 23 );
       this.applyFilterButton.TabIndex = 1;
       this.applyFilterButton.Text = "Apply";
       this.applyFilterButton.UseVisualStyleBackColor = true;
       this.applyFilterButton.Click += new System.EventHandler( this.ApplyFilters );
       // 
       // groupBox1
       // 
       this.groupBox1.Controls.Add( this.bothCheckBox );
       this.groupBox1.Controls.Add( this.femaleOnlyCheckBox );
       this.groupBox1.Controls.Add( this.maleOnlyCheckBox );
       this.groupBox1.Location = new System.Drawing.Point( 16, 15 );
       this.groupBox1.Name = "groupBox1";
       this.groupBox1.Size = new System.Drawing.Size( 403, 48 );
       this.groupBox1.TabIndex = 0;
       this.groupBox1.TabStop = false;
       this.groupBox1.Text = "Gender:";
       // 
       // bothCheckBox
       // 
       this.bothCheckBox.AutoSize = true;
       this.bothCheckBox.Location = new System.Drawing.Point( 269, 18 );
       this.bothCheckBox.Name = "bothCheckBox";
       this.bothCheckBox.Size = new System.Drawing.Size( 48, 17 );
       this.bothCheckBox.TabIndex = 2;
       this.bothCheckBox.Text = "Both";
       this.bothCheckBox.UseVisualStyleBackColor = true;
       this.bothCheckBox.Click += new System.EventHandler( this.SetGenderFilterArray );
       // 
       // femaleOnlyCheckBox
       // 
       this.femaleOnlyCheckBox.AutoSize = true;
       this.femaleOnlyCheckBox.Location = new System.Drawing.Point( 168, 18 );
       this.femaleOnlyCheckBox.Name = "femaleOnlyCheckBox";
       this.femaleOnlyCheckBox.Size = new System.Drawing.Size( 60, 17 );
       this.femaleOnlyCheckBox.TabIndex = 1;
       this.femaleOnlyCheckBox.Text = "Female";
       this.femaleOnlyCheckBox.UseVisualStyleBackColor = true;
       this.femaleOnlyCheckBox.Click += new System.EventHandler( this.SetGenderFilterArray );
       // 
       // maleOnlyCheckBox
       // 
       this.maleOnlyCheckBox.AutoSize = true;
       this.maleOnlyCheckBox.Location = new System.Drawing.Point( 62, 18 );
       this.maleOnlyCheckBox.Name = "maleOnlyCheckBox";
       this.maleOnlyCheckBox.Size = new System.Drawing.Size( 49, 17 );
       this.maleOnlyCheckBox.TabIndex = 0;
       this.maleOnlyCheckBox.Text = "Male";
       this.maleOnlyCheckBox.UseVisualStyleBackColor = true;
       this.maleOnlyCheckBox.Click += new System.EventHandler( this.SetGenderFilterArray );
       // 
       // objectFiltersTabPage
       // 
       this.objectFiltersTabPage.Controls.Add( this.functionFilterListView );
       this.objectFiltersTabPage.Controls.Add( this.roomFilterListView );
       this.objectFiltersTabPage.Controls.Add( this.resetObjectFilterButton );
       this.objectFiltersTabPage.Controls.Add( this.button2 );
       this.objectFiltersTabPage.Location = new System.Drawing.Point( 4, 22 );
       this.objectFiltersTabPage.Name = "objectFiltersTabPage";
       this.objectFiltersTabPage.Size = new System.Drawing.Size( 434, 494 );
       this.objectFiltersTabPage.TabIndex = 2;
       this.objectFiltersTabPage.Text = "Object Filters";
       this.objectFiltersTabPage.UseVisualStyleBackColor = true;
       // 
       // functionFilterListView
       // 
       this.functionFilterListView.CheckBoxes = true;
       this.functionFilterListView.Columns.AddRange( new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3} );
       this.functionFilterListView.Location = new System.Drawing.Point( 233, 14 );
       this.functionFilterListView.Name = "functionFilterListView";
       this.functionFilterListView.Size = new System.Drawing.Size( 187, 381 );
       this.functionFilterListView.TabIndex = 6;
       this.functionFilterListView.UseCompatibleStateImageBehavior = false;
       this.functionFilterListView.View = System.Windows.Forms.View.Details;
       // 
       // columnHeader3
       // 
       this.columnHeader3.Text = "Function";
       this.columnHeader3.Width = 178;
       // 
       // roomFilterListView
       // 
       this.roomFilterListView.CheckBoxes = true;
       this.roomFilterListView.Columns.AddRange( new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2} );
       this.roomFilterListView.Location = new System.Drawing.Point( 13, 14 );
       this.roomFilterListView.Name = "roomFilterListView";
       this.roomFilterListView.Size = new System.Drawing.Size( 187, 381 );
       this.roomFilterListView.TabIndex = 5;
       this.roomFilterListView.UseCompatibleStateImageBehavior = false;
       this.roomFilterListView.View = System.Windows.Forms.View.Details;
       // 
       // columnHeader2
       // 
       this.columnHeader2.Text = "Room";
       this.columnHeader2.Width = 178;
       // 
       // resetObjectFilterButton
       // 
       this.resetObjectFilterButton.Enabled = false;
       this.resetObjectFilterButton.Location = new System.Drawing.Point( 257, 406 );
       this.resetObjectFilterButton.Name = "resetObjectFilterButton";
       this.resetObjectFilterButton.Size = new System.Drawing.Size( 75, 23 );
       this.resetObjectFilterButton.TabIndex = 4;
       this.resetObjectFilterButton.Text = "Reset";
       this.resetObjectFilterButton.UseVisualStyleBackColor = true;
       this.resetObjectFilterButton.Click += new System.EventHandler( this.ResetFilters );
       // 
       // button2
       // 
       this.button2.Location = new System.Drawing.Point( 345, 406 );
       this.button2.Name = "button2";
       this.button2.Size = new System.Drawing.Size( 75, 23 );
       this.button2.TabIndex = 3;
       this.button2.Text = "Apply";
       this.button2.UseVisualStyleBackColor = true;
       this.button2.Click += new System.EventHandler( this.ApplyFilters );
       // 
       // pb
       // 
       this.pb.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left )
                   | System.Windows.Forms.AnchorStyles.Right ) ) );
       this.pb.Location = new System.Drawing.Point( 0, 668 );
       this.pb.Maximum = 1000;
       this.pb.Name = "pb";
       this.pb.Size = new System.Drawing.Size( 978, 16 );
       this.pb.TabIndex = 27;
       // 
       // collTypeComboBox
       // 
       this.collTypeComboBox.FormattingEnabled = true;
       this.collTypeComboBox.Items.AddRange( new object[] {
            "Cloth collection",
            "Object residential collection",
            "Object community collection",
            "Object any lot collection"} );
       this.collTypeComboBox.Location = new System.Drawing.Point( 619, 29 );
       this.collTypeComboBox.Name = "collTypeComboBox";
       this.collTypeComboBox.Size = new System.Drawing.Size( 347, 21 );
       this.collTypeComboBox.TabIndex = 28;
       this.collTypeComboBox.Text = "Cloth collection";
       this.collTypeComboBox.SelectionChangeCommitted += new System.EventHandler( this.collTypeComboBox_SelectionChangeCommitted );
       // 
       // label2
       // 
       this.label2.AutoSize = true;
       this.label2.Location = new System.Drawing.Point( 531, 37 );
       this.label2.Name = "label2";
       this.label2.Size = new System.Drawing.Size( 79, 13 );
       this.label2.TabIndex = 29;
       this.label2.Text = "Collection type:";
       // 
       // CollWspForm
       // 
       this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
       this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
       this.ClientSize = new System.Drawing.Size( 978, 685 );
       this.Controls.Add( this.label2 );
       this.Controls.Add( this.collTypeComboBox );
       this.Controls.Add( this.pb );
       this.Controls.Add( this.resTabControl );
       this.Controls.Add( this.rightTabControl );
       this.Controls.Add( this.saveButton );
       this.Controls.Add( this.collIconButton );
       this.Controls.Add( this.collIconPictureBox );
       this.Controls.Add( this.label5 );
       this.Controls.Add( this.collNameTextBox );
       this.Controls.Add( this.label4 );
       this.Controls.Add( this.removeAllFromCollButton );
       this.Controls.Add( this.remFromCollButton );
       this.Controls.Add( this.allToCollButton );
       this.Controls.Add( this.toCollButton );
       this.Controls.Add( this.cbfolder );
       this.Controls.Add( this.cbrec );
       this.Controls.Add( this.scanButton );
       this.Name = "CollWspForm";
       this.Text = "Collection Workshop Plugin";
       this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
       ( (System.ComponentModel.ISupportInitialize)( this.collIconPictureBox ) ).EndInit();
       this.rightTabControl.ResumeLayout( false );
       this.collTabPage.ResumeLayout( false );
       this.resTabPage.ResumeLayout( false );
       this.resTabPage.PerformLayout();
       ( (System.ComponentModel.ISupportInitialize)( this.imagePictureBox ) ).EndInit();
       this.resTabControl.ResumeLayout( false );
       this.tabPage1.ResumeLayout( false );
       this.clothFiltersTabPage.ResumeLayout( false );
       this.groupBox2.ResumeLayout( false );
       this.groupBox2.PerformLayout();
       this.groupBox1.ResumeLayout( false );
       this.groupBox1.PerformLayout();
       this.objectFiltersTabPage.ResumeLayout( false );
       this.ResumeLayout( false );
       this.PerformLayout();

    }



    #endregion

    private System.Windows.Forms.ListView resListView;
    private System.Windows.Forms.Button scanButton;
    private System.Windows.Forms.ColumnHeader fileColumnHeader;
    private System.Windows.Forms.CheckBox cbrec;
    private System.Windows.Forms.ComboBox cbfolder;
    private System.Windows.Forms.FolderBrowserDialog fbd;
    private System.Windows.Forms.ListView collListView;
    private System.Windows.Forms.ColumnHeader columnHeader1;
    private System.Windows.Forms.Button toCollButton;
    private System.Windows.Forms.ToolTip toolTip1;
    private System.Windows.Forms.Button allToCollButton;
    private System.Windows.Forms.Button removeAllFromCollButton;
    private System.Windows.Forms.Button remFromCollButton;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.TextBox collNameTextBox;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.PictureBox collIconPictureBox;
    private System.Windows.Forms.Button collIconButton;
    private System.Windows.Forms.OpenFileDialog iconFileDialog;
     private System.Windows.Forms.Button saveButton;
    private System.Windows.Forms.TabControl rightTabControl;
    private System.Windows.Forms.TabPage collTabPage;
    private System.Windows.Forms.TabPage resTabPage;
    private System.Windows.Forms.TabControl resTabControl;
    private System.Windows.Forms.TabPage tabPage1;
    private System.Windows.Forms.TabPage clothFiltersTabPage;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.PictureBox imagePictureBox;
    private System.Windows.Forms.ProgressBar pb;
    private System.Windows.Forms.ImageList ilist;
    private System.Windows.Forms.ListView propertyListView;
    private System.Windows.Forms.ColumnHeader columnHeader4;
    private System.Windows.Forms.ColumnHeader columnHeader5;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.CheckBox femaleOnlyCheckBox;
    private System.Windows.Forms.CheckBox maleOnlyCheckBox;
    private System.Windows.Forms.Button resetClothFilterButton;
    private System.Windows.Forms.Button applyFilterButton;
    private System.Windows.Forms.ListView ageFilterListView;
    private System.Windows.Forms.ColumnHeader columnHeader7;
    private System.Windows.Forms.ListView categoryFilterListView;
    private System.Windows.Forms.ColumnHeader columnHeader6;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.CheckBox otherCheckBox;
    private System.Windows.Forms.CheckBox bodyCheckBox;
    private System.Windows.Forms.CheckBox bottomCheckBox;
    private System.Windows.Forms.CheckBox topCheckBox;
    private System.Windows.Forms.CheckBox bothCheckBox;
    private System.Windows.Forms.CheckBox maskCheckBox;
    private System.Windows.Forms.CheckBox hatCheckBox;
    private System.Windows.Forms.ComboBox collTypeComboBox;
    private System.Windows.Forms.Label label2;
     private System.Windows.Forms.TabPage objectFiltersTabPage;
     private System.Windows.Forms.Button resetObjectFilterButton;
     private System.Windows.Forms.Button button2;
     private System.Windows.Forms.Button moveDownButton;
     private System.Windows.Forms.Button moveBottomButton;
     private System.Windows.Forms.Button moveTopButton;
     private System.Windows.Forms.Button moveUpButton;
     private System.Windows.Forms.ListView functionFilterListView;
     private System.Windows.Forms.ColumnHeader columnHeader3;
     private System.Windows.Forms.ListView roomFilterListView;
     private System.Windows.Forms.ColumnHeader columnHeader2;
  }

  
}