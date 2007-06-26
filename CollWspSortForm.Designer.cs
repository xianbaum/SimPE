namespace SimPe.Plugin
{
   partial class CollWspSortForm
   {
      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary>
      /// Clean up any resources being used.
      /// </summary>
      /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
      protected override void Dispose(bool disposing)
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
         this.label2 = new System.Windows.Forms.Label();
         this.collTypeComboBox = new System.Windows.Forms.ComboBox();
         this.scanButton = new System.Windows.Forms.Button();
         this.sortListView = new System.Windows.Forms.ListView();
         this.iconImageList = new System.Windows.Forms.ImageList( this.components );
         this.moveTopButton = new System.Windows.Forms.Button();
         this.moveUpButton = new System.Windows.Forms.Button();
         this.moveBottomButton = new System.Windows.Forms.Button();
         this.moveDownButton = new System.Windows.Forms.Button();
         this.saveButton = new System.Windows.Forms.Button();
         this.SuspendLayout();
         // 
         // label2
         // 
         this.label2.AutoSize = true;
         this.label2.Location = new System.Drawing.Point( 131, 20 );
         this.label2.Name = "label2";
         this.label2.Size = new System.Drawing.Size( 79, 13 );
         this.label2.TabIndex = 31;
         this.label2.Text = "Collection type:";
         // 
         // collTypeComboBox
         // 
         this.collTypeComboBox.FormattingEnabled = true;
         this.collTypeComboBox.Items.AddRange( new object[] {
            "Cloth collection",
            "Object residential collection",
            "Object community collection",
            "Object any lot collection"} );
         this.collTypeComboBox.Location = new System.Drawing.Point( 219, 12 );
         this.collTypeComboBox.Name = "collTypeComboBox";
         this.collTypeComboBox.Size = new System.Drawing.Size( 347, 21 );
         this.collTypeComboBox.TabIndex = 30;
         this.collTypeComboBox.Text = "Cloth collection";
         this.collTypeComboBox.SelectionChangeCommitted += new System.EventHandler( this.collTypeComboBox_SelectionChangeCommitted );
         // 
         // scanButton
         // 
         this.scanButton.Font = new System.Drawing.Font( "Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte)( 238 ) ) );
         this.scanButton.Location = new System.Drawing.Point( 592, 12 );
         this.scanButton.Name = "scanButton";
         this.scanButton.Size = new System.Drawing.Size( 75, 21 );
         this.scanButton.TabIndex = 32;
         this.scanButton.Text = "Scan";
         this.scanButton.UseVisualStyleBackColor = true;
         this.scanButton.Click += new System.EventHandler( this.Scan );
         // 
         // sortListView
         // 
         this.sortListView.AutoArrange = false;
         this.sortListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
         this.sortListView.LargeImageList = this.iconImageList;
         this.sortListView.Location = new System.Drawing.Point( 12, 72 );
         this.sortListView.Name = "sortListView";
         this.sortListView.Size = new System.Drawing.Size( 870, 339 );
         this.sortListView.TabIndex = 33;
         this.sortListView.UseCompatibleStateImageBehavior = false;
         // 
         // iconImageList
         // 
         this.iconImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth16Bit;
         this.iconImageList.ImageSize = new System.Drawing.Size( 32, 32 );
         this.iconImageList.TransparentColor = System.Drawing.Color.Transparent;
         // 
         // moveTopButton
         // 
         this.moveTopButton.Font = new System.Drawing.Font( "Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( (byte)( 238 ) ) );
         this.moveTopButton.Location = new System.Drawing.Point( 347, 433 );
         this.moveTopButton.Name = "moveTopButton";
         this.moveTopButton.Size = new System.Drawing.Size( 39, 31 );
         this.moveTopButton.TabIndex = 37;
         this.moveTopButton.Text = "<<";
         this.moveTopButton.UseVisualStyleBackColor = true;
         this.moveTopButton.Click += new System.EventHandler( this.MoveTop );
         // 
         // moveUpButton
         // 
         this.moveUpButton.Font = new System.Drawing.Font( "Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( (byte)( 238 ) ) );
         this.moveUpButton.Location = new System.Drawing.Point( 410, 433 );
         this.moveUpButton.Name = "moveUpButton";
         this.moveUpButton.Size = new System.Drawing.Size( 39, 31 );
         this.moveUpButton.TabIndex = 36;
         this.moveUpButton.Text = "<";
         this.moveUpButton.UseVisualStyleBackColor = true;
         this.moveUpButton.Click += new System.EventHandler( this.MoveUp );
         // 
         // moveBottomButton
         // 
         this.moveBottomButton.Font = new System.Drawing.Font( "Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( (byte)( 238 ) ) );
         this.moveBottomButton.Location = new System.Drawing.Point( 536, 433 );
         this.moveBottomButton.Name = "moveBottomButton";
         this.moveBottomButton.Size = new System.Drawing.Size( 39, 31 );
         this.moveBottomButton.TabIndex = 35;
         this.moveBottomButton.Text = ">>";
         this.moveBottomButton.UseVisualStyleBackColor = true;
         this.moveBottomButton.Click += new System.EventHandler( this.MoveBottom );
         // 
         // moveDownButton
         // 
         this.moveDownButton.Font = new System.Drawing.Font( "Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( (byte)( 238 ) ) );
         this.moveDownButton.Location = new System.Drawing.Point( 473, 433 );
         this.moveDownButton.Name = "moveDownButton";
         this.moveDownButton.Size = new System.Drawing.Size( 39, 31 );
         this.moveDownButton.TabIndex = 34;
         this.moveDownButton.Text = ">";
         this.moveDownButton.UseVisualStyleBackColor = true;
         this.moveDownButton.Click += new System.EventHandler( this.MoveDown );
         // 
         // saveButton
         // 
         this.saveButton.Location = new System.Drawing.Point( 775, 435 );
         this.saveButton.Name = "saveButton";
         this.saveButton.Size = new System.Drawing.Size( 106, 28 );
         this.saveButton.TabIndex = 38;
         this.saveButton.Text = "Save";
         this.saveButton.UseVisualStyleBackColor = true;
         this.saveButton.Click += new System.EventHandler( this.SaveSorting );
         // 
         // CollWspSortForm
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size( 894, 476 );
         this.Controls.Add( this.saveButton );
         this.Controls.Add( this.moveTopButton );
         this.Controls.Add( this.moveUpButton );
         this.Controls.Add( this.moveBottomButton );
         this.Controls.Add( this.moveDownButton );
         this.Controls.Add( this.sortListView );
         this.Controls.Add( this.scanButton );
         this.Controls.Add( this.label2 );
         this.Controls.Add( this.collTypeComboBox );
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
         this.Name = "CollWspSortForm";
         this.Text = "Collection Workshop Plugin - Sort Your Collections";
         this.ResumeLayout( false );
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.Label label2;
      private System.Windows.Forms.ComboBox collTypeComboBox;
      private System.Windows.Forms.Button scanButton;
      private System.Windows.Forms.ListView sortListView;
      private System.Windows.Forms.ImageList iconImageList;
      private System.Windows.Forms.Button moveTopButton;
      private System.Windows.Forms.Button moveUpButton;
      private System.Windows.Forms.Button moveBottomButton;
      private System.Windows.Forms.Button moveDownButton;
      private System.Windows.Forms.Button saveButton;
   }
}