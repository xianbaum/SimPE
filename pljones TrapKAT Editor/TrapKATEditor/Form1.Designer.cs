namespace TrapKATEditor.UI
{
    partial class Form1
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
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ofdOpenSysEx = new System.Windows.Forms.OpenFileDialog();
            this.msMain = new System.Windows.Forms.MenuStrip();
            this.tsmiFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiFileNew = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiFileOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiFileSave = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiFileSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiFileClose = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEditUndo = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEditRedo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiEditCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEditPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEditSwap = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTools = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiToolsOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.sfdSaveSysEx = new System.Windows.Forms.SaveFileDialog();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.cbCurrentKit = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbKitName = new System.Windows.Forms.TextBox();
            this.msMain.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ofdOpenSysEx
            // 
            this.ofdOpenSysEx.DefaultExt = "syx";
            this.ofdOpenSysEx.FileName = "*.syx";
            this.ofdOpenSysEx.Filter = "SysEx files (*.syx)|*.syx|All files (*.*)|*.*";
            this.ofdOpenSysEx.RestoreDirectory = true;
            this.ofdOpenSysEx.Title = "Open TrapKAT SysEx dump";
            // 
            // msMain
            // 
            this.msMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiFile,
            this.tsmiEdit,
            this.tsmiTools,
            this.tsmiHelp});
            this.msMain.Location = new System.Drawing.Point(0, 0);
            this.msMain.Name = "msMain";
            this.msMain.Size = new System.Drawing.Size(619, 26);
            this.msMain.TabIndex = 0;
            this.msMain.Text = "menuStrip1";
            // 
            // tsmiFile
            // 
            this.tsmiFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiFileNew,
            this.tsmiFileOpen,
            this.tsmiFileSave,
            this.tsmiFileSaveAs,
            this.tsmiFileClose,
            this.toolStripSeparator1,
            this.tsmiFileExit});
            this.tsmiFile.Name = "tsmiFile";
            this.tsmiFile.Size = new System.Drawing.Size(40, 22);
            this.tsmiFile.Text = "&File";
            // 
            // tsmiFileNew
            // 
            this.tsmiFileNew.Name = "tsmiFileNew";
            this.tsmiFileNew.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.tsmiFileNew.Size = new System.Drawing.Size(248, 22);
            this.tsmiFileNew.Text = "&New";
            this.tsmiFileNew.Click += new System.EventHandler(this.tsmiFileNew_Click);
            // 
            // tsmiFileOpen
            // 
            this.tsmiFileOpen.Name = "tsmiFileOpen";
            this.tsmiFileOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.tsmiFileOpen.Size = new System.Drawing.Size(248, 22);
            this.tsmiFileOpen.Text = "&Open...";
            this.tsmiFileOpen.Click += new System.EventHandler(this.tsmiFileOpen_Click);
            // 
            // tsmiFileSave
            // 
            this.tsmiFileSave.Name = "tsmiFileSave";
            this.tsmiFileSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.tsmiFileSave.Size = new System.Drawing.Size(248, 22);
            this.tsmiFileSave.Text = "&Save All Memory";
            this.tsmiFileSave.Click += new System.EventHandler(this.tsmiFileSave_Click);
            // 
            // tsmiFileSaveAs
            // 
            this.tsmiFileSaveAs.Name = "tsmiFileSaveAs";
            this.tsmiFileSaveAs.Size = new System.Drawing.Size(248, 22);
            this.tsmiFileSaveAs.Text = "Save All Memory &As...";
            this.tsmiFileSaveAs.Click += new System.EventHandler(this.tsmiFileSaveAs_Click);
            // 
            // tsmiFileClose
            // 
            this.tsmiFileClose.Name = "tsmiFileClose";
            this.tsmiFileClose.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
            this.tsmiFileClose.Size = new System.Drawing.Size(248, 22);
            this.tsmiFileClose.Text = "&Close";
            this.tsmiFileClose.Click += new System.EventHandler(this.tsmiFileExitQuit_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(245, 6);
            // 
            // tsmiFileExit
            // 
            this.tsmiFileExit.Name = "tsmiFileExit";
            this.tsmiFileExit.Size = new System.Drawing.Size(248, 22);
            this.tsmiFileExit.Text = "E&xit";
            this.tsmiFileExit.Click += new System.EventHandler(this.tsmiFileExitQuit_Click);
            // 
            // tsmiEdit
            // 
            this.tsmiEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiEditUndo,
            this.tsmiEditRedo,
            this.toolStripSeparator2,
            this.tsmiEditCopy,
            this.tsmiEditPaste,
            this.tsmiEditSwap});
            this.tsmiEdit.Name = "tsmiEdit";
            this.tsmiEdit.Size = new System.Drawing.Size(43, 22);
            this.tsmiEdit.Text = "&Edit";
            // 
            // tsmiEditUndo
            // 
            this.tsmiEditUndo.Name = "tsmiEditUndo";
            this.tsmiEditUndo.Size = new System.Drawing.Size(152, 22);
            this.tsmiEditUndo.Text = "Undo";
            // 
            // tsmiEditRedo
            // 
            this.tsmiEditRedo.Name = "tsmiEditRedo";
            this.tsmiEditRedo.Size = new System.Drawing.Size(152, 22);
            this.tsmiEditRedo.Text = "Redo";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(149, 6);
            // 
            // tsmiEditCopy
            // 
            this.tsmiEditCopy.Name = "tsmiEditCopy";
            this.tsmiEditCopy.Size = new System.Drawing.Size(152, 22);
            this.tsmiEditCopy.Text = "Copy";
            // 
            // tsmiEditPaste
            // 
            this.tsmiEditPaste.Name = "tsmiEditPaste";
            this.tsmiEditPaste.Size = new System.Drawing.Size(152, 22);
            this.tsmiEditPaste.Text = "Paste";
            // 
            // tsmiEditSwap
            // 
            this.tsmiEditSwap.Name = "tsmiEditSwap";
            this.tsmiEditSwap.Size = new System.Drawing.Size(152, 22);
            this.tsmiEditSwap.Text = "Swap...";
            // 
            // tsmiTools
            // 
            this.tsmiTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiToolsOptions});
            this.tsmiTools.Name = "tsmiTools";
            this.tsmiTools.Size = new System.Drawing.Size(55, 22);
            this.tsmiTools.Text = "&Tools";
            // 
            // tsmiToolsOptions
            // 
            this.tsmiToolsOptions.Name = "tsmiToolsOptions";
            this.tsmiToolsOptions.Size = new System.Drawing.Size(155, 22);
            this.tsmiToolsOptions.Text = "&Options...";
            // 
            // tsmiHelp
            // 
            this.tsmiHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiHelpAbout});
            this.tsmiHelp.Name = "tsmiHelp";
            this.tsmiHelp.Size = new System.Drawing.Size(48, 22);
            this.tsmiHelp.Text = "&Help";
            // 
            // tsmiHelpAbout
            // 
            this.tsmiHelpAbout.Name = "tsmiHelpAbout";
            this.tsmiHelpAbout.Size = new System.Drawing.Size(144, 22);
            this.tsmiHelpAbout.Text = "&About...";
            // 
            // sfdSaveSysEx
            // 
            this.sfdSaveSysEx.DefaultExt = "syx";
            this.sfdSaveSysEx.FileName = "*.syx";
            this.sfdSaveSysEx.Filter = "SysEx files (*.syx)|*.syx|All files (*.*)|*.*";
            this.sfdSaveSysEx.RestoreDirectory = true;
            this.sfdSaveSysEx.Title = "Save TrapKAT SysEx dump As...";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 38);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(595, 221);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.flowLayoutPanel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(587, 192);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Kits";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(587, 192);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Global";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Controls.Add(this.cbCurrentKit);
            this.flowLayoutPanel1.Controls.Add(this.label2);
            this.flowLayoutPanel1.Controls.Add(this.tbKitName);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(581, 186);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select Kit:";
            // 
            // cbCurrentKit
            // 
            this.cbCurrentKit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCurrentKit.FormattingEnabled = true;
            this.cbCurrentKit.Items.AddRange(new object[] {
            "DD",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25"});
            this.cbCurrentKit.Location = new System.Drawing.Point(80, 3);
            this.cbCurrentKit.MaxDropDownItems = 25;
            this.cbCurrentKit.Name = "cbCurrentKit";
            this.cbCurrentKit.Size = new System.Drawing.Size(55, 24);
            this.cbCurrentKit.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(141, 6);
            this.label2.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "Name:";
            // 
            // tbKitName
            // 
            this.tbKitName.Location = new System.Drawing.Point(196, 4);
            this.tbKitName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 3);
            this.tbKitName.MaxLength = 12;
            this.tbKitName.Name = "tbKitName";
            this.tbKitName.Size = new System.Drawing.Size(166, 22);
            this.tbKitName.TabIndex = 2;
            this.tbKitName.Text = "WWWWWWWWWWWW";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(619, 271);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.msMain);
            this.MainMenuStrip = this.msMain;
            this.Name = "Form1";
            this.Text = "TrapKAT SysEx Editor";
            this.msMain.ResumeLayout(false);
            this.msMain.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog ofdOpenSysEx;
        private System.Windows.Forms.MenuStrip msMain;
        private System.Windows.Forms.ToolStripMenuItem tsmiFile;
        private System.Windows.Forms.ToolStripMenuItem tsmiFileNew;
        private System.Windows.Forms.ToolStripMenuItem tsmiFileOpen;
        private System.Windows.Forms.ToolStripMenuItem tsmiEdit;
        private System.Windows.Forms.ToolStripMenuItem tsmiHelp;
        private System.Windows.Forms.ToolStripMenuItem tsmiFileSave;
        private System.Windows.Forms.ToolStripMenuItem tsmiFileSaveAs;
        private System.Windows.Forms.ToolStripMenuItem tsmiHelpAbout;
        private System.Windows.Forms.ToolStripMenuItem tsmiFileClose;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsmiFileExit;
        private System.Windows.Forms.ToolStripMenuItem tsmiEditUndo;
        private System.Windows.Forms.ToolStripMenuItem tsmiEditRedo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem tsmiEditCopy;
        private System.Windows.Forms.ToolStripMenuItem tsmiEditPaste;
        private System.Windows.Forms.ToolStripMenuItem tsmiEditSwap;
        private System.Windows.Forms.ToolStripMenuItem tsmiTools;
        private System.Windows.Forms.ToolStripMenuItem tsmiToolsOptions;
        private System.Windows.Forms.SaveFileDialog sfdSaveSysEx;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbCurrentKit;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbKitName;
    }
}

