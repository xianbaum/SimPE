/***************************************************************************
 *   Copyright (C) 2007 by Peter L Jones                                   *
 *   peter@drealm.info                                                     *
 *                                                                         *
 *   This program is free software; you can redistribute it and/or modify  *
 *   it under the terms of the GNU General Public License as published by  *
 *   the Free Software Foundation; either version 2 of the License, or     *
 *   (at your option) any later version.                                   *
 *                                                                         *
 *   This program is distributed in the hope that it will be useful,       *
 *   but WITHOUT ANY WARRANTY; without even the implied warranty of        *
 *   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the         *
 *   GNU General Public License for more details.                          *
 *                                                                         *
 *   You should have received a copy of the GNU General Public License     *
 *   along with this program; if not, write to the                         *
 *   Free Software Foundation, Inc.,                                       *
 *   59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.             *
 ***************************************************************************/
namespace TrapKATEditor.UI
{
    partial class MainForm
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
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiFileSaveAllMemory = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiFileSaveAllMemoryAs = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiFileSaveGlobalMemory = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiFileSaveGlobalMemoryAs = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiFileSaveCurrentKit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiFileSaveCurrentKitAs = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiFileClose = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEditUndo = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEditRedo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiEditCopyKit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEditSwapKits = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiEditCopyPad = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEditSwapPads = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTools = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiToolsOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.sfdSaveSysEx = new System.Windows.Forms.SaveFileDialog();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.flowLayoutPanel9 = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.label36 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbKitName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbFCCurve = new System.Windows.Forms.ComboBox();
            this.label24 = new System.Windows.Forms.Label();
            this.flowLayoutPanel5 = new System.Windows.Forms.FlowLayoutPanel();
            this.nudFCChannel = new System.Windows.Forms.NumericUpDown();
            this.ckbAsChick = new System.Windows.Forms.CheckBox();
            this.label28 = new System.Windows.Forms.Label();
            this.cbFCFunction = new System.Windows.Forms.ComboBox();
            this.label27 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.flowLayoutPanel8 = new System.Windows.Forms.FlowLayoutPanel();
            this.nudKitChannel = new System.Windows.Forms.NumericUpDown();
            this.ckbVarChannel = new System.Windows.Forms.CheckBox();
            this.flowLayoutPanel7 = new System.Windows.Forms.FlowLayoutPanel();
            this.cbKitGate = new System.Windows.Forms.ComboBox();
            this.ckbVarGate = new System.Windows.Forms.CheckBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.flowLayoutPanel6 = new System.Windows.Forms.FlowLayoutPanel();
            this.cbKitCurve = new System.Windows.Forms.ComboBox();
            this.ckbVarCurve = new System.Windows.Forms.CheckBox();
            this.flowLayoutPanel12 = new System.Windows.Forms.FlowLayoutPanel();
            this.cbCurrentKit = new System.Windows.Forms.ComboBox();
            this.lbKitDataChanged = new System.Windows.Forms.Label();
            this.flowLayoutPanel15 = new System.Windows.Forms.FlowLayoutPanel();
            this.cbHHPad1 = new System.Windows.Forms.ComboBox();
            this.cbHHPad2 = new System.Windows.Forms.ComboBox();
            this.cbHHPad3 = new System.Windows.Forms.ComboBox();
            this.cbHHPad4 = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel13 = new System.Windows.Forms.FlowLayoutPanel();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.nudPrgChgTxmChn = new System.Windows.Forms.NumericUpDown();
            this.label35 = new System.Windows.Forms.Label();
            this.ckbNoPrgChg = new System.Windows.Forms.CheckBox();
            this.nudPrgChg = new System.Windows.Forms.NumericUpDown();
            this.label29 = new System.Windows.Forms.Label();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.nudBankLSB = new System.Windows.Forms.NumericUpDown();
            this.label32 = new System.Windows.Forms.Label();
            this.nudBankMSB = new System.Windows.Forms.NumericUpDown();
            this.label31 = new System.Windows.Forms.Label();
            this.nudBank = new System.Windows.Forms.NumericUpDown();
            this.label30 = new System.Windows.Forms.Label();
            this.flowLayoutPanel11 = new System.Windows.Forms.FlowLayoutPanel();
            this.tlpKitVelocity = new System.Windows.Forms.TableLayoutPanel();
            this.ckbVarMinVel = new System.Windows.Forms.CheckBox();
            this.ckbVarMaxVel = new System.Windows.Forms.CheckBox();
            this.nudKitMinVel = new System.Windows.Forms.NumericUpDown();
            this.nudKitMaxVel = new System.Windows.Forms.NumericUpDown();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.flowLayoutPanel14 = new System.Windows.Forms.FlowLayoutPanel();
            this.label33 = new System.Windows.Forms.Label();
            this.nudVolume = new System.Windows.Forms.NumericUpDown();
            this.ckbNoVolume = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.cbPad = new System.Windows.Forms.ComboBox();
            this.lbPadDataChanged = new System.Windows.Forms.Label();
            this.lbBreak2 = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label10 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.cbNote5 = new System.Windows.Forms.ComboBox();
            this.cbNote4 = new System.Windows.Forms.ComboBox();
            this.cbNote3 = new System.Windows.Forms.ComboBox();
            this.cbNote2 = new System.Windows.Forms.ComboBox();
            this.cbNote1 = new System.Windows.Forms.ComboBox();
            this.cbNote6 = new System.Windows.Forms.ComboBox();
            this.flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.label13 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.cbPadGate = new System.Windows.Forms.ComboBox();
            this.cbPadCurve = new System.Windows.Forms.ComboBox();
            this.nudPadChannel = new System.Windows.Forms.NumericUpDown();
            this.tlpPadVelocity = new System.Windows.Forms.TableLayoutPanel();
            this.nudPadMinVel = new System.Windows.Forms.NumericUpDown();
            this.nudPadMaxVel = new System.Windows.Forms.NumericUpDown();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.label18 = new System.Windows.Forms.Label();
            this.ckbF7 = new System.Windows.Forms.CheckBox();
            this.ckbF6 = new System.Windows.Forms.CheckBox();
            this.ckbF5 = new System.Windows.Forms.CheckBox();
            this.ckbF4 = new System.Windows.Forms.CheckBox();
            this.ckbF3 = new System.Windows.Forms.CheckBox();
            this.ckbF2 = new System.Windows.Forms.CheckBox();
            this.ckbF1 = new System.Windows.Forms.CheckBox();
            this.ckbF0 = new System.Windows.Forms.CheckBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lbGlobalDataChanged = new System.Windows.Forms.Label();
            this.flowLayoutPanel26 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel38 = new System.Windows.Forms.FlowLayoutPanel();
            this.label56 = new System.Windows.Forms.Label();
            this.nudFcOpenRegion = new System.Windows.Forms.NumericUpDown();
            this.flowLayoutPanel21 = new System.Windows.Forms.FlowLayoutPanel();
            this.label43 = new System.Windows.Forms.Label();
            this.nudFcClosedRegion = new System.Windows.Forms.NumericUpDown();
            this.flowLayoutPanel22 = new System.Windows.Forms.FlowLayoutPanel();
            this.label41 = new System.Windows.Forms.Label();
            this.nudFcPolarity = new System.Windows.Forms.NumericUpDown();
            this.flowLayoutPanel28 = new System.Windows.Forms.FlowLayoutPanel();
            this.label39 = new System.Windows.Forms.Label();
            this.nudFcVelocityLevel = new System.Windows.Forms.NumericUpDown();
            this.flowLayoutPanel61 = new System.Windows.Forms.FlowLayoutPanel();
            this.label66 = new System.Windows.Forms.Label();
            this.nudFcLowLevel = new System.Windows.Forms.NumericUpDown();
            this.flowLayoutPanel62 = new System.Windows.Forms.FlowLayoutPanel();
            this.label75 = new System.Windows.Forms.Label();
            this.nudFcHighLevel = new System.Windows.Forms.NumericUpDown();
            this.flowLayoutPanel47 = new System.Windows.Forms.FlowLayoutPanel();
            this.label65 = new System.Windows.Forms.Label();
            this.nudFcSplashEase = new System.Windows.Forms.NumericUpDown();
            this.flowLayoutPanel29 = new System.Windows.Forms.FlowLayoutPanel();
            this.label44 = new System.Windows.Forms.Label();
            this.nudFcWaitModeLevel = new System.Windows.Forms.NumericUpDown();
            this.flowLayoutPanel49 = new System.Windows.Forms.FlowLayoutPanel();
            this.label69 = new System.Windows.Forms.Label();
            this.nudHatNoteGate = new System.Windows.Forms.NumericUpDown();
            this.flowLayoutPanel25 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel24 = new System.Windows.Forms.FlowLayoutPanel();
            this.label34 = new System.Windows.Forms.Label();
            this.cbPadDynamics = new System.Windows.Forms.ComboBox();
            this.flowLayoutPanel56 = new System.Windows.Forms.FlowLayoutPanel();
            this.label73 = new System.Windows.Forms.Label();
            this.nudLowLevel = new System.Windows.Forms.NumericUpDown();
            this.flowLayoutPanel57 = new System.Windows.Forms.FlowLayoutPanel();
            this.label74 = new System.Windows.Forms.Label();
            this.nudHighLevel = new System.Windows.Forms.NumericUpDown();
            this.flowLayoutPanel27 = new System.Windows.Forms.FlowLayoutPanel();
            this.label46 = new System.Windows.Forms.Label();
            this.nudThresholdManual = new System.Windows.Forms.NumericUpDown();
            this.flowLayoutPanel39 = new System.Windows.Forms.FlowLayoutPanel();
            this.label47 = new System.Windows.Forms.Label();
            this.nudThresholdActual = new System.Windows.Forms.NumericUpDown();
            this.flowLayoutPanel40 = new System.Windows.Forms.FlowLayoutPanel();
            this.label48 = new System.Windows.Forms.Label();
            this.nudUserMargin = new System.Windows.Forms.NumericUpDown();
            this.flowLayoutPanel58 = new System.Windows.Forms.FlowLayoutPanel();
            this.label50 = new System.Windows.Forms.Label();
            this.nudInternalMargin = new System.Windows.Forms.NumericUpDown();
            this.flowLayoutPanel41 = new System.Windows.Forms.FlowLayoutPanel();
            this.label61 = new System.Windows.Forms.Label();
            this.nudTrigGain = new System.Windows.Forms.NumericUpDown();
            this.flowLayoutPanel10 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel18 = new System.Windows.Forms.FlowLayoutPanel();
            this.label38 = new System.Windows.Forms.Label();
            this.nudBcFunction = new System.Windows.Forms.NumericUpDown();
            this.flowLayoutPanel23 = new System.Windows.Forms.FlowLayoutPanel();
            this.label42 = new System.Windows.Forms.Label();
            this.nudBcPolarity = new System.Windows.Forms.NumericUpDown();
            this.flowLayoutPanel59 = new System.Windows.Forms.FlowLayoutPanel();
            this.label57 = new System.Windows.Forms.Label();
            this.nudBcLowLevel = new System.Windows.Forms.NumericUpDown();
            this.flowLayoutPanel60 = new System.Windows.Forms.FlowLayoutPanel();
            this.label63 = new System.Windows.Forms.Label();
            this.nudBcHighLevel = new System.Windows.Forms.NumericUpDown();
            this.flowLayoutPanel55 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel34 = new System.Windows.Forms.FlowLayoutPanel();
            this.label49 = new System.Windows.Forms.Label();
            this.nudMotifNumber = new System.Windows.Forms.NumericUpDown();
            this.flowLayoutPanel35 = new System.Windows.Forms.FlowLayoutPanel();
            this.label54 = new System.Windows.Forms.Label();
            this.nudMotifNumberPerc = new System.Windows.Forms.NumericUpDown();
            this.flowLayoutPanel36 = new System.Windows.Forms.FlowLayoutPanel();
            this.label59 = new System.Windows.Forms.Label();
            this.nudMotifNumberMel = new System.Windows.Forms.NumericUpDown();
            this.flowLayoutPanel54 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel31 = new System.Windows.Forms.FlowLayoutPanel();
            this.label51 = new System.Windows.Forms.Label();
            this.nudKitNumber = new System.Windows.Forms.NumericUpDown();
            this.flowLayoutPanel33 = new System.Windows.Forms.FlowLayoutPanel();
            this.label53 = new System.Windows.Forms.Label();
            this.nudKitNumberDemo = new System.Windows.Forms.NumericUpDown();
            this.flowLayoutPanel32 = new System.Windows.Forms.FlowLayoutPanel();
            this.label52 = new System.Windows.Forms.Label();
            this.nudKitNumberUser = new System.Windows.Forms.NumericUpDown();
            this.flowLayoutPanel51 = new System.Windows.Forms.FlowLayoutPanel();
            this.label72 = new System.Windows.Forms.Label();
            this.nudKitNumberKAT = new System.Windows.Forms.NumericUpDown();
            this.flowLayoutPanel53 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel46 = new System.Windows.Forms.FlowLayoutPanel();
            this.label64 = new System.Windows.Forms.Label();
            this.nudGrooveStatus = new System.Windows.Forms.NumericUpDown();
            this.flowLayoutPanel45 = new System.Windows.Forms.FlowLayoutPanel();
            this.label68 = new System.Windows.Forms.Label();
            this.nudGrooveVol = new System.Windows.Forms.NumericUpDown();
            this.flowLayoutPanel50 = new System.Windows.Forms.FlowLayoutPanel();
            this.label70 = new System.Windows.Forms.Label();
            this.grooveAutoOff = new System.Windows.Forms.NumericUpDown();
            this.flowLayoutPanel52 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel17 = new System.Windows.Forms.FlowLayoutPanel();
            this.label37 = new System.Windows.Forms.Label();
            this.nudBeeperStatus = new System.Windows.Forms.NumericUpDown();
            this.flowLayoutPanel20 = new System.Windows.Forms.FlowLayoutPanel();
            this.label40 = new System.Windows.Forms.Label();
            this.nudChokeFunction = new System.Windows.Forms.NumericUpDown();
            this.flowLayoutPanel30 = new System.Windows.Forms.FlowLayoutPanel();
            this.label45 = new System.Windows.Forms.Label();
            this.nudInstrumentID = new System.Windows.Forms.NumericUpDown();
            this.flowLayoutPanel37 = new System.Windows.Forms.FlowLayoutPanel();
            this.label55 = new System.Windows.Forms.Label();
            this.nudMidiMergeStatus = new System.Windows.Forms.NumericUpDown();
            this.flowLayoutPanel42 = new System.Windows.Forms.FlowLayoutPanel();
            this.label62 = new System.Windows.Forms.Label();
            this.cbPrgChgRcvChn = new System.Windows.Forms.ComboBox();
            this.flowLayoutPanel43 = new System.Windows.Forms.FlowLayoutPanel();
            this.label58 = new System.Windows.Forms.Label();
            this.nudDisplayAngle = new System.Windows.Forms.NumericUpDown();
            this.flowLayoutPanel44 = new System.Windows.Forms.FlowLayoutPanel();
            this.label60 = new System.Windows.Forms.Label();
            this.nudPlayMode = new System.Windows.Forms.NumericUpDown();
            this.flowLayoutPanel48 = new System.Windows.Forms.FlowLayoutPanel();
            this.label141 = new System.Windows.Forms.Label();
            this.nudNoteNamesStatus = new System.Windows.Forms.NumericUpDown();
            this.flowLayoutPanel16 = new System.Windows.Forms.FlowLayoutPanel();
            this.label67 = new System.Windows.Forms.Label();
            this.nudTTMeter = new System.Windows.Forms.NumericUpDown();
            this.flowLayoutPanel19 = new System.Windows.Forms.FlowLayoutPanel();
            this.label71 = new System.Windows.Forms.Label();
            this.nudHearSoundStatus = new System.Windows.Forms.NumericUpDown();
            this.label25 = new System.Windows.Forms.Label();
            this.msMain.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.flowLayoutPanel9.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.flowLayoutPanel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFCChannel)).BeginInit();
            this.flowLayoutPanel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudKitChannel)).BeginInit();
            this.flowLayoutPanel7.SuspendLayout();
            this.flowLayoutPanel6.SuspendLayout();
            this.flowLayoutPanel12.SuspendLayout();
            this.flowLayoutPanel15.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            this.flowLayoutPanel13.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrgChgTxmChn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrgChg)).BeginInit();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudBankLSB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBankMSB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBank)).BeginInit();
            this.flowLayoutPanel11.SuspendLayout();
            this.tlpKitVelocity.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudKitMinVel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudKitMaxVel)).BeginInit();
            this.flowLayoutPanel14.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudVolume)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPadChannel)).BeginInit();
            this.tlpPadVelocity.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPadMinVel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPadMaxVel)).BeginInit();
            this.tableLayoutPanel5.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.flowLayoutPanel26.SuspendLayout();
            this.flowLayoutPanel38.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFcOpenRegion)).BeginInit();
            this.flowLayoutPanel21.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFcClosedRegion)).BeginInit();
            this.flowLayoutPanel22.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFcPolarity)).BeginInit();
            this.flowLayoutPanel28.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFcVelocityLevel)).BeginInit();
            this.flowLayoutPanel61.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFcLowLevel)).BeginInit();
            this.flowLayoutPanel62.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFcHighLevel)).BeginInit();
            this.flowLayoutPanel47.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFcSplashEase)).BeginInit();
            this.flowLayoutPanel29.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFcWaitModeLevel)).BeginInit();
            this.flowLayoutPanel49.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudHatNoteGate)).BeginInit();
            this.flowLayoutPanel25.SuspendLayout();
            this.flowLayoutPanel24.SuspendLayout();
            this.flowLayoutPanel56.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudLowLevel)).BeginInit();
            this.flowLayoutPanel57.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudHighLevel)).BeginInit();
            this.flowLayoutPanel27.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudThresholdManual)).BeginInit();
            this.flowLayoutPanel39.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudThresholdActual)).BeginInit();
            this.flowLayoutPanel40.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudUserMargin)).BeginInit();
            this.flowLayoutPanel58.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudInternalMargin)).BeginInit();
            this.flowLayoutPanel41.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTrigGain)).BeginInit();
            this.flowLayoutPanel10.SuspendLayout();
            this.flowLayoutPanel18.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudBcFunction)).BeginInit();
            this.flowLayoutPanel23.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudBcPolarity)).BeginInit();
            this.flowLayoutPanel59.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudBcLowLevel)).BeginInit();
            this.flowLayoutPanel60.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudBcHighLevel)).BeginInit();
            this.flowLayoutPanel55.SuspendLayout();
            this.flowLayoutPanel34.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMotifNumber)).BeginInit();
            this.flowLayoutPanel35.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMotifNumberPerc)).BeginInit();
            this.flowLayoutPanel36.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMotifNumberMel)).BeginInit();
            this.flowLayoutPanel54.SuspendLayout();
            this.flowLayoutPanel31.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudKitNumber)).BeginInit();
            this.flowLayoutPanel33.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudKitNumberDemo)).BeginInit();
            this.flowLayoutPanel32.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudKitNumberUser)).BeginInit();
            this.flowLayoutPanel51.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudKitNumberKAT)).BeginInit();
            this.flowLayoutPanel53.SuspendLayout();
            this.flowLayoutPanel46.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudGrooveStatus)).BeginInit();
            this.flowLayoutPanel45.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudGrooveVol)).BeginInit();
            this.flowLayoutPanel50.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grooveAutoOff)).BeginInit();
            this.flowLayoutPanel52.SuspendLayout();
            this.flowLayoutPanel17.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudBeeperStatus)).BeginInit();
            this.flowLayoutPanel20.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudChokeFunction)).BeginInit();
            this.flowLayoutPanel30.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudInstrumentID)).BeginInit();
            this.flowLayoutPanel37.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMidiMergeStatus)).BeginInit();
            this.flowLayoutPanel42.SuspendLayout();
            this.flowLayoutPanel43.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDisplayAngle)).BeginInit();
            this.flowLayoutPanel44.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPlayMode)).BeginInit();
            this.flowLayoutPanel48.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudNoteNamesStatus)).BeginInit();
            this.flowLayoutPanel16.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTTMeter)).BeginInit();
            this.flowLayoutPanel19.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudHearSoundStatus)).BeginInit();
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
            this.msMain.Size = new System.Drawing.Size(590, 26);
            this.msMain.TabIndex = 0;
            this.msMain.Text = "menuStrip1";
            // 
            // tsmiFile
            // 
            this.tsmiFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiFileNew,
            this.tsmiFileOpen,
            this.toolStripSeparator3,
            this.tsmiFileSaveAllMemory,
            this.tsmiFileSaveAllMemoryAs,
            this.tsmiFileSaveGlobalMemory,
            this.tsmiFileSaveGlobalMemoryAs,
            this.tsmiFileSaveCurrentKit,
            this.tsmiFileSaveCurrentKitAs,
            this.toolStripSeparator4,
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
            this.tsmiFileNew.Size = new System.Drawing.Size(261, 22);
            this.tsmiFileNew.Text = "&New";
            this.tsmiFileNew.Click += new System.EventHandler(this.tsmiFileNew_Click);
            // 
            // tsmiFileOpen
            // 
            this.tsmiFileOpen.Name = "tsmiFileOpen";
            this.tsmiFileOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.tsmiFileOpen.Size = new System.Drawing.Size(261, 22);
            this.tsmiFileOpen.Text = "&Open...";
            this.tsmiFileOpen.Click += new System.EventHandler(this.tsmiFileOpen_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(258, 6);
            // 
            // tsmiFileSaveAllMemory
            // 
            this.tsmiFileSaveAllMemory.Name = "tsmiFileSaveAllMemory";
            this.tsmiFileSaveAllMemory.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.tsmiFileSaveAllMemory.Size = new System.Drawing.Size(261, 22);
            this.tsmiFileSaveAllMemory.Text = "&Save All Memory";
            this.tsmiFileSaveAllMemory.Click += new System.EventHandler(this.tsmiFileSave_Click);
            // 
            // tsmiFileSaveAllMemoryAs
            // 
            this.tsmiFileSaveAllMemoryAs.Name = "tsmiFileSaveAllMemoryAs";
            this.tsmiFileSaveAllMemoryAs.Size = new System.Drawing.Size(261, 22);
            this.tsmiFileSaveAllMemoryAs.Text = "Save All Memory &As...";
            this.tsmiFileSaveAllMemoryAs.Click += new System.EventHandler(this.tsmiFileSaveAs_Click);
            // 
            // tsmiFileSaveGlobalMemory
            // 
            this.tsmiFileSaveGlobalMemory.Name = "tsmiFileSaveGlobalMemory";
            this.tsmiFileSaveGlobalMemory.Size = new System.Drawing.Size(261, 22);
            this.tsmiFileSaveGlobalMemory.Text = "Save &Global Memory";
            this.tsmiFileSaveGlobalMemory.Click += new System.EventHandler(this.tsmiFileSave_Click);
            // 
            // tsmiFileSaveGlobalMemoryAs
            // 
            this.tsmiFileSaveGlobalMemoryAs.Name = "tsmiFileSaveGlobalMemoryAs";
            this.tsmiFileSaveGlobalMemoryAs.Size = new System.Drawing.Size(261, 22);
            this.tsmiFileSaveGlobalMemoryAs.Text = "Save Global Memory As...";
            this.tsmiFileSaveGlobalMemoryAs.Click += new System.EventHandler(this.tsmiFileSaveAs_Click);
            // 
            // tsmiFileSaveCurrentKit
            // 
            this.tsmiFileSaveCurrentKit.Name = "tsmiFileSaveCurrentKit";
            this.tsmiFileSaveCurrentKit.Size = new System.Drawing.Size(261, 22);
            this.tsmiFileSaveCurrentKit.Text = "Save Current &Kit";
            this.tsmiFileSaveCurrentKit.Click += new System.EventHandler(this.tsmiFileSave_Click);
            // 
            // tsmiFileSaveCurrentKitAs
            // 
            this.tsmiFileSaveCurrentKitAs.Name = "tsmiFileSaveCurrentKitAs";
            this.tsmiFileSaveCurrentKitAs.Size = new System.Drawing.Size(261, 22);
            this.tsmiFileSaveCurrentKitAs.Text = "Save Current Kit As";
            this.tsmiFileSaveCurrentKitAs.Click += new System.EventHandler(this.tsmiFileSaveAs_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(258, 6);
            // 
            // tsmiFileClose
            // 
            this.tsmiFileClose.Name = "tsmiFileClose";
            this.tsmiFileClose.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
            this.tsmiFileClose.Size = new System.Drawing.Size(261, 22);
            this.tsmiFileClose.Text = "&Close";
            this.tsmiFileClose.Click += new System.EventHandler(this.tsmiFileExitQuit_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(258, 6);
            // 
            // tsmiFileExit
            // 
            this.tsmiFileExit.Name = "tsmiFileExit";
            this.tsmiFileExit.Size = new System.Drawing.Size(261, 22);
            this.tsmiFileExit.Text = "E&xit";
            this.tsmiFileExit.Click += new System.EventHandler(this.tsmiFileExitQuit_Click);
            // 
            // tsmiEdit
            // 
            this.tsmiEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiEditUndo,
            this.tsmiEditRedo,
            this.toolStripSeparator2,
            this.tsmiEditCopyKit,
            this.tsmiEditSwapKits,
            this.toolStripSeparator5,
            this.tsmiEditCopyPad,
            this.tsmiEditSwapPads});
            this.tsmiEdit.Name = "tsmiEdit";
            this.tsmiEdit.Size = new System.Drawing.Size(43, 22);
            this.tsmiEdit.Text = "&Edit";
            // 
            // tsmiEditUndo
            // 
            this.tsmiEditUndo.Name = "tsmiEditUndo";
            this.tsmiEditUndo.Size = new System.Drawing.Size(176, 22);
            this.tsmiEditUndo.Text = "Undo";
            // 
            // tsmiEditRedo
            // 
            this.tsmiEditRedo.Name = "tsmiEditRedo";
            this.tsmiEditRedo.Size = new System.Drawing.Size(176, 22);
            this.tsmiEditRedo.Text = "Redo";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(173, 6);
            // 
            // tsmiEditCopyKit
            // 
            this.tsmiEditCopyKit.Name = "tsmiEditCopyKit";
            this.tsmiEditCopyKit.Size = new System.Drawing.Size(176, 22);
            this.tsmiEditCopyKit.Text = "Copy Kit...";
            // 
            // tsmiEditSwapKits
            // 
            this.tsmiEditSwapKits.Name = "tsmiEditSwapKits";
            this.tsmiEditSwapKits.Size = new System.Drawing.Size(176, 22);
            this.tsmiEditSwapKits.Text = "Swap Kits...";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(173, 6);
            // 
            // tsmiEditCopyPad
            // 
            this.tsmiEditCopyPad.Name = "tsmiEditCopyPad";
            this.tsmiEditCopyPad.Size = new System.Drawing.Size(176, 22);
            this.tsmiEditCopyPad.Text = "Copy Pad...";
            // 
            // tsmiEditSwapPads
            // 
            this.tsmiEditSwapPads.Name = "tsmiEditSwapPads";
            this.tsmiEditSwapPads.Size = new System.Drawing.Size(176, 22);
            this.tsmiEditSwapPads.Text = "Swap Pads...";
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
            this.tsmiHelpAbout.Size = new System.Drawing.Size(152, 22);
            this.tsmiHelpAbout.Text = "&About...";
            this.tsmiHelpAbout.Click += new System.EventHandler(this.tsmiHelpAbout_Click);
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
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl1.Location = new System.Drawing.Point(0, 26);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(590, 677);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.flowLayoutPanel9);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(582, 648);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Kits & Pads";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel9
            // 
            this.flowLayoutPanel9.AutoSize = true;
            this.flowLayoutPanel9.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel9.Controls.Add(this.groupBox1);
            this.flowLayoutPanel9.Controls.Add(this.groupBox2);
            this.flowLayoutPanel9.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel9.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel9.Name = "flowLayoutPanel9";
            this.flowLayoutPanel9.Size = new System.Drawing.Size(579, 638);
            this.flowLayoutPanel9.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSize = true;
            this.groupBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox1.Controls.Add(this.flowLayoutPanel1);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(573, 358);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "&Kit Editor";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.Controls.Add(this.tableLayoutPanel6);
            this.flowLayoutPanel1.Controls.Add(this.tableLayoutPanel8);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 18);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(564, 319);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.AutoSize = true;
            this.tableLayoutPanel6.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel6.ColumnCount = 2;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel6.Controls.Add(this.label36, 0, 11);
            this.tableLayoutPanel6.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.tbKitName, 1, 1);
            this.tableLayoutPanel6.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel6.Controls.Add(this.cbFCCurve, 1, 10);
            this.tableLayoutPanel6.Controls.Add(this.label24, 0, 10);
            this.tableLayoutPanel6.Controls.Add(this.flowLayoutPanel5, 1, 9);
            this.tableLayoutPanel6.Controls.Add(this.label28, 0, 9);
            this.tableLayoutPanel6.Controls.Add(this.cbFCFunction, 1, 8);
            this.tableLayoutPanel6.Controls.Add(this.label27, 0, 8);
            this.tableLayoutPanel6.Controls.Add(this.label26, 1, 7);
            this.tableLayoutPanel6.Controls.Add(this.flowLayoutPanel8, 1, 5);
            this.tableLayoutPanel6.Controls.Add(this.flowLayoutPanel7, 1, 4);
            this.tableLayoutPanel6.Controls.Add(this.label16, 0, 5);
            this.tableLayoutPanel6.Controls.Add(this.label19, 0, 3);
            this.tableLayoutPanel6.Controls.Add(this.label20, 0, 4);
            this.tableLayoutPanel6.Controls.Add(this.flowLayoutPanel6, 1, 3);
            this.tableLayoutPanel6.Controls.Add(this.flowLayoutPanel12, 1, 0);
            this.tableLayoutPanel6.Controls.Add(this.flowLayoutPanel15, 1, 11);
            this.tableLayoutPanel6.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 12;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel6.Size = new System.Drawing.Size(337, 313);
            this.tableLayoutPanel6.TabIndex = 1;
            // 
            // label36
            // 
            this.label36.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(0, 289);
            this.label36.Margin = new System.Windows.Forms.Padding(0);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(81, 17);
            this.label36.TabIndex = 0;
            this.label36.Text = "Hihat Pads:";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 6);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select Kit:";
            // 
            // tbKitName
            // 
            this.tbKitName.Location = new System.Drawing.Point(84, 33);
            this.tbKitName.MaxLength = 12;
            this.tbKitName.Name = "tbKitName";
            this.tbKitName.Size = new System.Drawing.Size(166, 22);
            this.tbKitName.TabIndex = 2;
            this.tbKitName.Text = "WWWWWWWWWWWW";
            this.tbKitName.TextChanged += new System.EventHandler(this.tbKitName_TextChanged);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 35);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "Name:";
            // 
            // cbFCCurve
            // 
            this.cbFCCurve.FormattingEnabled = true;
            this.cbFCCurve.Items.AddRange(new object[] {
            "see Curve.cs"});
            this.cbFCCurve.Location = new System.Drawing.Point(84, 256);
            this.cbFCCurve.MaxDropDownItems = 3;
            this.cbFCCurve.Name = "cbFCCurve";
            this.cbFCCurve.Size = new System.Drawing.Size(175, 24);
            this.cbFCCurve.TabIndex = 8;
            // 
            // label24
            // 
            this.label24.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(32, 259);
            this.label24.Margin = new System.Windows.Forms.Padding(0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(49, 17);
            this.label24.TabIndex = 0;
            this.label24.Text = "Curve:";
            // 
            // flowLayoutPanel5
            // 
            this.flowLayoutPanel5.AutoSize = true;
            this.flowLayoutPanel5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel5.Controls.Add(this.nudFCChannel);
            this.flowLayoutPanel5.Controls.Add(this.ckbAsChick);
            this.flowLayoutPanel5.Location = new System.Drawing.Point(81, 225);
            this.flowLayoutPanel5.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel5.Name = "flowLayoutPanel5";
            this.flowLayoutPanel5.Size = new System.Drawing.Size(179, 28);
            this.flowLayoutPanel5.TabIndex = 7;
            // 
            // nudFCChannel
            // 
            this.nudFCChannel.Location = new System.Drawing.Point(3, 3);
            this.nudFCChannel.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.nudFCChannel.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudFCChannel.Name = "nudFCChannel";
            this.nudFCChannel.Size = new System.Drawing.Size(42, 22);
            this.nudFCChannel.TabIndex = 1;
            this.nudFCChannel.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudFCChannel.ValueChanged += new System.EventHandler(this.nud_ValueChanged);
            // 
            // ckbAsChick
            // 
            this.ckbAsChick.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ckbAsChick.AutoSize = true;
            this.ckbAsChick.Location = new System.Drawing.Point(48, 3);
            this.ckbAsChick.Margin = new System.Windows.Forms.Padding(0);
            this.ckbAsChick.Name = "ckbAsChick";
            this.ckbAsChick.Size = new System.Drawing.Size(131, 21);
            this.ckbAsChick.TabIndex = 2;
            this.ckbAsChick.Text = "Same as Pad 26";
            this.ckbAsChick.UseVisualStyleBackColor = true;
            this.ckbAsChick.CheckedChanged += new System.EventHandler(this.ckbAsChick_CheckedChanged);
            // 
            // label28
            // 
            this.label28.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(17, 230);
            this.label28.Margin = new System.Windows.Forms.Padding(0);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(64, 17);
            this.label28.TabIndex = 0;
            this.label28.Text = "Channel:";
            // 
            // cbFCFunction
            // 
            this.cbFCFunction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFCFunction.FormattingEnabled = true;
            this.cbFCFunction.Items.AddRange(new object[] {
            "Off",
            "CC#01 (Mod Wheel)",
            "CC#04 (F/C 0..64)",
            "CC#04 (F/C 0..127)",
            "Hat Note"});
            this.cbFCFunction.Location = new System.Drawing.Point(84, 198);
            this.cbFCFunction.MaxDropDownItems = 5;
            this.cbFCFunction.Name = "cbFCFunction";
            this.cbFCFunction.Size = new System.Drawing.Size(153, 24);
            this.cbFCFunction.TabIndex = 6;
            // 
            // label27
            // 
            this.label27.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(15, 201);
            this.label27.Margin = new System.Windows.Forms.Padding(0);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(66, 17);
            this.label27.TabIndex = 0;
            this.label27.Text = "Function:";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(81, 178);
            this.label26.Margin = new System.Windows.Forms.Padding(0, 12, 0, 0);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(101, 17);
            this.label26.TabIndex = 0;
            this.label26.Text = "Foot Controller";
            this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // flowLayoutPanel8
            // 
            this.flowLayoutPanel8.AutoSize = true;
            this.flowLayoutPanel8.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel8.Controls.Add(this.nudKitChannel);
            this.flowLayoutPanel8.Controls.Add(this.ckbVarChannel);
            this.flowLayoutPanel8.Location = new System.Drawing.Point(81, 138);
            this.flowLayoutPanel8.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel8.Name = "flowLayoutPanel8";
            this.flowLayoutPanel8.Size = new System.Drawing.Size(123, 28);
            this.flowLayoutPanel8.TabIndex = 5;
            // 
            // nudKitChannel
            // 
            this.nudKitChannel.Location = new System.Drawing.Point(3, 3);
            this.nudKitChannel.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.nudKitChannel.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudKitChannel.Name = "nudKitChannel";
            this.nudKitChannel.Size = new System.Drawing.Size(42, 22);
            this.nudKitChannel.TabIndex = 1;
            this.nudKitChannel.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudKitChannel.ValueChanged += new System.EventHandler(this.nud_ValueChanged);
            // 
            // ckbVarChannel
            // 
            this.ckbVarChannel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ckbVarChannel.AutoSize = true;
            this.ckbVarChannel.Location = new System.Drawing.Point(48, 3);
            this.ckbVarChannel.Margin = new System.Windows.Forms.Padding(0);
            this.ckbVarChannel.Name = "ckbVarChannel";
            this.ckbVarChannel.Size = new System.Drawing.Size(75, 21);
            this.ckbVarChannel.TabIndex = 2;
            this.ckbVarChannel.Text = "Various";
            this.ckbVarChannel.UseVisualStyleBackColor = true;
            this.ckbVarChannel.CheckedChanged += new System.EventHandler(this.ckbVarChannel_CheckedChanged);
            // 
            // flowLayoutPanel7
            // 
            this.flowLayoutPanel7.AutoSize = true;
            this.flowLayoutPanel7.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel7.Controls.Add(this.cbKitGate);
            this.flowLayoutPanel7.Controls.Add(this.ckbVarGate);
            this.flowLayoutPanel7.Location = new System.Drawing.Point(81, 108);
            this.flowLayoutPanel7.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel7.Name = "flowLayoutPanel7";
            this.flowLayoutPanel7.Size = new System.Drawing.Size(188, 30);
            this.flowLayoutPanel7.TabIndex = 4;
            // 
            // cbKitGate
            // 
            this.cbKitGate.FormattingEnabled = true;
            this.cbKitGate.Items.AddRange(new object[] {
            "see Gate.cs"});
            this.cbKitGate.Location = new System.Drawing.Point(3, 3);
            this.cbKitGate.MaxDropDownItems = 3;
            this.cbKitGate.Name = "cbKitGate";
            this.cbKitGate.Size = new System.Drawing.Size(107, 24);
            this.cbKitGate.TabIndex = 1;
            this.cbKitGate.Validating += new System.ComponentModel.CancelEventHandler(this.cbGate_Validating);
            this.cbKitGate.Validated += new System.EventHandler(this.cbGate_Validated);
            this.cbKitGate.Enter += new System.EventHandler(this.cbGate_Enter);
            this.cbKitGate.SelectedIndexChanged += new System.EventHandler(this.cbGate_SelectedIndexChanged);
            this.cbKitGate.TextChanged += new System.EventHandler(this.cbGate_TextChanged);
            // 
            // ckbVarGate
            // 
            this.ckbVarGate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ckbVarGate.AutoSize = true;
            this.ckbVarGate.Location = new System.Drawing.Point(113, 4);
            this.ckbVarGate.Margin = new System.Windows.Forms.Padding(0);
            this.ckbVarGate.Name = "ckbVarGate";
            this.ckbVarGate.Size = new System.Drawing.Size(75, 21);
            this.ckbVarGate.TabIndex = 2;
            this.ckbVarGate.Text = "Various";
            this.ckbVarGate.UseVisualStyleBackColor = true;
            this.ckbVarGate.CheckedChanged += new System.EventHandler(this.ckbVarGate_CheckedChanged);
            // 
            // label16
            // 
            this.label16.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(17, 143);
            this.label16.Margin = new System.Windows.Forms.Padding(0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(64, 17);
            this.label16.TabIndex = 0;
            this.label16.Text = "Channel:";
            // 
            // label19
            // 
            this.label19.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(32, 84);
            this.label19.Margin = new System.Windows.Forms.Padding(0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(49, 17);
            this.label19.TabIndex = 0;
            this.label19.Text = "Curve:";
            // 
            // label20
            // 
            this.label20.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(38, 114);
            this.label20.Margin = new System.Windows.Forms.Padding(0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(43, 17);
            this.label20.TabIndex = 0;
            this.label20.Text = "Gate:";
            // 
            // flowLayoutPanel6
            // 
            this.flowLayoutPanel6.AutoSize = true;
            this.flowLayoutPanel6.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel6.Controls.Add(this.cbKitCurve);
            this.flowLayoutPanel6.Controls.Add(this.ckbVarCurve);
            this.flowLayoutPanel6.Location = new System.Drawing.Point(81, 78);
            this.flowLayoutPanel6.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel6.Name = "flowLayoutPanel6";
            this.flowLayoutPanel6.Size = new System.Drawing.Size(256, 30);
            this.flowLayoutPanel6.TabIndex = 3;
            // 
            // cbKitCurve
            // 
            this.cbKitCurve.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbKitCurve.FormattingEnabled = true;
            this.cbKitCurve.Items.AddRange(new object[] {
            "see Curve.cs"});
            this.cbKitCurve.Location = new System.Drawing.Point(3, 3);
            this.cbKitCurve.MaxDropDownItems = 25;
            this.cbKitCurve.Name = "cbKitCurve";
            this.cbKitCurve.Size = new System.Drawing.Size(175, 24);
            this.cbKitCurve.TabIndex = 1;
            this.cbKitCurve.SelectedIndexChanged += new System.EventHandler(this.cbCurve_SelectedIndexChanged);
            // 
            // ckbVarCurve
            // 
            this.ckbVarCurve.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ckbVarCurve.AutoSize = true;
            this.ckbVarCurve.Location = new System.Drawing.Point(181, 4);
            this.ckbVarCurve.Margin = new System.Windows.Forms.Padding(0);
            this.ckbVarCurve.Name = "ckbVarCurve";
            this.ckbVarCurve.Size = new System.Drawing.Size(75, 21);
            this.ckbVarCurve.TabIndex = 2;
            this.ckbVarCurve.Text = "Various";
            this.ckbVarCurve.UseVisualStyleBackColor = true;
            this.ckbVarCurve.CheckedChanged += new System.EventHandler(this.ckbVarCurve_CheckedChanged);
            // 
            // flowLayoutPanel12
            // 
            this.flowLayoutPanel12.AutoSize = true;
            this.flowLayoutPanel12.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel12.Controls.Add(this.cbCurrentKit);
            this.flowLayoutPanel12.Controls.Add(this.lbKitDataChanged);
            this.flowLayoutPanel12.Location = new System.Drawing.Point(81, 0);
            this.flowLayoutPanel12.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel12.Name = "flowLayoutPanel12";
            this.flowLayoutPanel12.Size = new System.Drawing.Size(226, 30);
            this.flowLayoutPanel12.TabIndex = 1;
            // 
            // cbCurrentKit
            // 
            this.cbCurrentKit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCurrentKit.DropDownWidth = 200;
            this.cbCurrentKit.FormattingEnabled = true;
            this.cbCurrentKit.Items.AddRange(new object[] {
            "1",
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
            "WW: WWWWWWWWWWWW"});
            this.cbCurrentKit.Location = new System.Drawing.Point(3, 3);
            this.cbCurrentKit.MaxDropDownItems = 25;
            this.cbCurrentKit.Name = "cbCurrentKit";
            this.cbCurrentKit.Size = new System.Drawing.Size(172, 24);
            this.cbCurrentKit.TabIndex = 1;
            this.cbCurrentKit.SelectedIndexChanged += new System.EventHandler(this.cbCurrentKit_SelectedIndexChanged);
            // 
            // lbKitDataChanged
            // 
            this.lbKitDataChanged.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbKitDataChanged.AutoSize = true;
            this.lbKitDataChanged.Location = new System.Drawing.Point(178, 6);
            this.lbKitDataChanged.Margin = new System.Windows.Forms.Padding(0);
            this.lbKitDataChanged.Name = "lbKitDataChanged";
            this.lbKitDataChanged.Size = new System.Drawing.Size(48, 17);
            this.lbKitDataChanged.TabIndex = 0;
            this.lbKitDataChanged.Text = "Edited";
            this.lbKitDataChanged.Visible = false;
            // 
            // flowLayoutPanel15
            // 
            this.flowLayoutPanel15.AutoSize = true;
            this.flowLayoutPanel15.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel15.Controls.Add(this.cbHHPad1);
            this.flowLayoutPanel15.Controls.Add(this.cbHHPad2);
            this.flowLayoutPanel15.Controls.Add(this.cbHHPad3);
            this.flowLayoutPanel15.Controls.Add(this.cbHHPad4);
            this.flowLayoutPanel15.Location = new System.Drawing.Point(84, 286);
            this.flowLayoutPanel15.Name = "flowLayoutPanel15";
            this.flowLayoutPanel15.Size = new System.Drawing.Size(220, 24);
            this.flowLayoutPanel15.TabIndex = 9;
            // 
            // cbHHPad1
            // 
            this.cbHHPad1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbHHPad1.FormattingEnabled = true;
            this.cbHHPad1.Items.AddRange(new object[] {
            "Off",
            "1",
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
            "24"});
            this.cbHHPad1.Location = new System.Drawing.Point(0, 0);
            this.cbHHPad1.Margin = new System.Windows.Forms.Padding(0);
            this.cbHHPad1.MaxDropDownItems = 25;
            this.cbHHPad1.Name = "cbHHPad1";
            this.cbHHPad1.Size = new System.Drawing.Size(55, 24);
            this.cbHHPad1.TabIndex = 1;
            this.cbHHPad1.SelectedIndexChanged += new System.EventHandler(this.cbHHPad_SelectedIndexChanged);
            // 
            // cbHHPad2
            // 
            this.cbHHPad2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbHHPad2.FormattingEnabled = true;
            this.cbHHPad2.Items.AddRange(new object[] {
            "Off",
            "1",
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
            "24"});
            this.cbHHPad2.Location = new System.Drawing.Point(55, 0);
            this.cbHHPad2.Margin = new System.Windows.Forms.Padding(0);
            this.cbHHPad2.MaxDropDownItems = 28;
            this.cbHHPad2.Name = "cbHHPad2";
            this.cbHHPad2.Size = new System.Drawing.Size(55, 24);
            this.cbHHPad2.TabIndex = 2;
            this.cbHHPad2.SelectedIndexChanged += new System.EventHandler(this.cbHHPad_SelectedIndexChanged);
            // 
            // cbHHPad3
            // 
            this.cbHHPad3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbHHPad3.FormattingEnabled = true;
            this.cbHHPad3.Items.AddRange(new object[] {
            "Off",
            "1",
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
            "24"});
            this.cbHHPad3.Location = new System.Drawing.Point(110, 0);
            this.cbHHPad3.Margin = new System.Windows.Forms.Padding(0);
            this.cbHHPad3.MaxDropDownItems = 28;
            this.cbHHPad3.Name = "cbHHPad3";
            this.cbHHPad3.Size = new System.Drawing.Size(55, 24);
            this.cbHHPad3.TabIndex = 3;
            this.cbHHPad3.SelectedIndexChanged += new System.EventHandler(this.cbHHPad_SelectedIndexChanged);
            // 
            // cbHHPad4
            // 
            this.cbHHPad4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbHHPad4.FormattingEnabled = true;
            this.cbHHPad4.Items.AddRange(new object[] {
            "Off",
            "1",
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
            "24"});
            this.cbHHPad4.Location = new System.Drawing.Point(165, 0);
            this.cbHHPad4.Margin = new System.Windows.Forms.Padding(0);
            this.cbHHPad4.MaxDropDownItems = 28;
            this.cbHHPad4.Name = "cbHHPad4";
            this.cbHHPad4.Size = new System.Drawing.Size(55, 24);
            this.cbHHPad4.TabIndex = 4;
            this.cbHHPad4.SelectedIndexChanged += new System.EventHandler(this.cbHHPad_SelectedIndexChanged);
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.AutoSize = true;
            this.tableLayoutPanel8.ColumnCount = 1;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel8.Controls.Add(this.flowLayoutPanel13, 0, 1);
            this.tableLayoutPanel8.Controls.Add(this.flowLayoutPanel11, 0, 0);
            this.flowLayoutPanel1.SetFlowBreak(this.tableLayoutPanel8, true);
            this.tableLayoutPanel8.Location = new System.Drawing.Point(370, 3);
            this.tableLayoutPanel8.Margin = new System.Windows.Forms.Padding(27, 3, 3, 3);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 3;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel8.Size = new System.Drawing.Size(191, 224);
            this.tableLayoutPanel8.TabIndex = 2;
            // 
            // flowLayoutPanel13
            // 
            this.flowLayoutPanel13.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.flowLayoutPanel13.AutoSize = true;
            this.flowLayoutPanel13.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel13.Controls.Add(this.tableLayoutPanel7);
            this.flowLayoutPanel13.Controls.Add(this.tableLayoutPanel4);
            this.flowLayoutPanel13.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel13.Location = new System.Drawing.Point(7, 119);
            this.flowLayoutPanel13.Margin = new System.Windows.Forms.Padding(3, 24, 3, 3);
            this.flowLayoutPanel13.Name = "flowLayoutPanel13";
            this.flowLayoutPanel13.Size = new System.Drawing.Size(177, 102);
            this.flowLayoutPanel13.TabIndex = 2;
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tableLayoutPanel7.AutoSize = true;
            this.tableLayoutPanel7.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel7.ColumnCount = 3;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel7.Controls.Add(this.nudPrgChgTxmChn, 1, 1);
            this.tableLayoutPanel7.Controls.Add(this.label35, 1, 0);
            this.tableLayoutPanel7.Controls.Add(this.ckbNoPrgChg, 2, 0);
            this.tableLayoutPanel7.Controls.Add(this.nudPrgChg, 0, 1);
            this.tableLayoutPanel7.Controls.Add(this.label29, 0, 0);
            this.tableLayoutPanel7.Location = new System.Drawing.Point(4, 3);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 2;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel7.Size = new System.Drawing.Size(168, 45);
            this.tableLayoutPanel7.TabIndex = 1;
            // 
            // nudPrgChgTxmChn
            // 
            this.nudPrgChgTxmChn.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.nudPrgChgTxmChn.Location = new System.Drawing.Point(69, 20);
            this.nudPrgChgTxmChn.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.nudPrgChgTxmChn.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudPrgChgTxmChn.Name = "nudPrgChgTxmChn";
            this.nudPrgChgTxmChn.Size = new System.Drawing.Size(42, 22);
            this.nudPrgChgTxmChn.TabIndex = 2;
            this.nudPrgChgTxmChn.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudPrgChgTxmChn.ValueChanged += new System.EventHandler(this.nud_ValueChanged);
            // 
            // label35
            // 
            this.label35.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(59, 0);
            this.label35.Margin = new System.Windows.Forms.Padding(0);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(63, 17);
            this.label35.TabIndex = 0;
            this.label35.Text = "TxmChn:";
            // 
            // ckbNoPrgChg
            // 
            this.ckbNoPrgChg.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ckbNoPrgChg.AutoSize = true;
            this.ckbNoPrgChg.Checked = true;
            this.ckbNoPrgChg.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbNoPrgChg.Location = new System.Drawing.Point(122, 12);
            this.ckbNoPrgChg.Margin = new System.Windows.Forms.Padding(0);
            this.ckbNoPrgChg.Name = "ckbNoPrgChg";
            this.tableLayoutPanel7.SetRowSpan(this.ckbNoPrgChg, 2);
            this.ckbNoPrgChg.Size = new System.Drawing.Size(46, 21);
            this.ckbNoPrgChg.TabIndex = 3;
            this.ckbNoPrgChg.Text = "Off";
            this.ckbNoPrgChg.UseVisualStyleBackColor = true;
            // 
            // nudPrgChg
            // 
            this.nudPrgChg.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.nudPrgChg.Location = new System.Drawing.Point(4, 20);
            this.nudPrgChg.Maximum = new decimal(new int[] {
            128,
            0,
            0,
            0});
            this.nudPrgChg.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudPrgChg.Name = "nudPrgChg";
            this.nudPrgChg.Size = new System.Drawing.Size(51, 22);
            this.nudPrgChg.TabIndex = 1;
            this.nudPrgChg.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudPrgChg.ValueChanged += new System.EventHandler(this.nud_ValueChanged);
            // 
            // label29
            // 
            this.label29.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(0, 0);
            this.label29.Margin = new System.Windows.Forms.Padding(0);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(59, 17);
            this.label29.TabIndex = 0;
            this.label29.Text = "PrgChg:";
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tableLayoutPanel4.AutoSize = true;
            this.tableLayoutPanel4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel4.ColumnCount = 3;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.Controls.Add(this.nudBankLSB, 1, 1);
            this.tableLayoutPanel4.Controls.Add(this.label32, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.nudBankMSB, 2, 1);
            this.tableLayoutPanel4.Controls.Add(this.label31, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.nudBank, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.label30, 0, 0);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 54);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(171, 45);
            this.tableLayoutPanel4.TabIndex = 2;
            // 
            // nudBankLSB
            // 
            this.nudBankLSB.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.nudBankLSB.Location = new System.Drawing.Point(60, 20);
            this.nudBankLSB.Maximum = new decimal(new int[] {
            127,
            0,
            0,
            0});
            this.nudBankLSB.Name = "nudBankLSB";
            this.nudBankLSB.Size = new System.Drawing.Size(51, 22);
            this.nudBankLSB.TabIndex = 2;
            this.nudBankLSB.ValueChanged += new System.EventHandler(this.nud_ValueChanged);
            // 
            // label32
            // 
            this.label32.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(66, 0);
            this.label32.Margin = new System.Windows.Forms.Padding(0);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(38, 17);
            this.label32.TabIndex = 0;
            this.label32.Text = "LSB:";
            // 
            // nudBankMSB
            // 
            this.nudBankMSB.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.nudBankMSB.Location = new System.Drawing.Point(117, 20);
            this.nudBankMSB.Maximum = new decimal(new int[] {
            127,
            0,
            0,
            0});
            this.nudBankMSB.Name = "nudBankMSB";
            this.nudBankMSB.Size = new System.Drawing.Size(51, 22);
            this.nudBankMSB.TabIndex = 3;
            this.nudBankMSB.ValueChanged += new System.EventHandler(this.nud_ValueChanged);
            // 
            // label31
            // 
            this.label31.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(122, 0);
            this.label31.Margin = new System.Windows.Forms.Padding(0);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(41, 17);
            this.label31.TabIndex = 0;
            this.label31.Text = "MSB:";
            // 
            // nudBank
            // 
            this.nudBank.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.nudBank.Location = new System.Drawing.Point(3, 20);
            this.nudBank.Maximum = new decimal(new int[] {
            127,
            0,
            0,
            0});
            this.nudBank.Name = "nudBank";
            this.nudBank.Size = new System.Drawing.Size(51, 22);
            this.nudBank.TabIndex = 1;
            this.nudBank.ValueChanged += new System.EventHandler(this.nud_ValueChanged);
            // 
            // label30
            // 
            this.label30.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(6, 0);
            this.label30.Margin = new System.Windows.Forms.Padding(0);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(44, 17);
            this.label30.TabIndex = 0;
            this.label30.Text = "Bank:";
            // 
            // flowLayoutPanel11
            // 
            this.flowLayoutPanel11.AutoSize = true;
            this.flowLayoutPanel11.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel11.Controls.Add(this.tlpKitVelocity);
            this.flowLayoutPanel11.Controls.Add(this.flowLayoutPanel14);
            this.flowLayoutPanel11.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel11.Name = "flowLayoutPanel11";
            this.flowLayoutPanel11.Size = new System.Drawing.Size(185, 89);
            this.flowLayoutPanel11.TabIndex = 1;
            // 
            // tlpKitVelocity
            // 
            this.tlpKitVelocity.AutoSize = true;
            this.tlpKitVelocity.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tlpKitVelocity.BackColor = System.Drawing.SystemColors.Control;
            this.tlpKitVelocity.ColumnCount = 2;
            this.tlpKitVelocity.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpKitVelocity.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpKitVelocity.Controls.Add(this.ckbVarMinVel, 0, 3);
            this.tlpKitVelocity.Controls.Add(this.ckbVarMaxVel, 0, 3);
            this.tlpKitVelocity.Controls.Add(this.nudKitMinVel, 0, 2);
            this.tlpKitVelocity.Controls.Add(this.nudKitMaxVel, 1, 2);
            this.tlpKitVelocity.Controls.Add(this.label21, 0, 1);
            this.tlpKitVelocity.Controls.Add(this.label22, 1, 1);
            this.tlpKitVelocity.Controls.Add(this.label23, 0, 0);
            this.tlpKitVelocity.Location = new System.Drawing.Point(3, 3);
            this.tlpKitVelocity.Name = "tlpKitVelocity";
            this.tlpKitVelocity.RowCount = 4;
            this.tlpKitVelocity.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpKitVelocity.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpKitVelocity.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpKitVelocity.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpKitVelocity.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpKitVelocity.Size = new System.Drawing.Size(114, 83);
            this.tlpKitVelocity.TabIndex = 1;
            // 
            // ckbVarMinVel
            // 
            this.ckbVarMinVel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.ckbVarMinVel.AutoSize = true;
            this.ckbVarMinVel.Location = new System.Drawing.Point(2, 62);
            this.ckbVarMinVel.Margin = new System.Windows.Forms.Padding(0);
            this.ckbVarMinVel.Name = "ckbVarMinVel";
            this.ckbVarMinVel.Size = new System.Drawing.Size(53, 21);
            this.ckbVarMinVel.TabIndex = 2;
            this.ckbVarMinVel.Text = "Var.";
            this.ckbVarMinVel.UseVisualStyleBackColor = true;
            // 
            // ckbVarMaxVel
            // 
            this.ckbVarMaxVel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.ckbVarMaxVel.AutoSize = true;
            this.ckbVarMaxVel.Location = new System.Drawing.Point(59, 62);
            this.ckbVarMaxVel.Margin = new System.Windows.Forms.Padding(0);
            this.ckbVarMaxVel.Name = "ckbVarMaxVel";
            this.ckbVarMaxVel.Size = new System.Drawing.Size(53, 21);
            this.ckbVarMaxVel.TabIndex = 4;
            this.ckbVarMaxVel.Text = "Var.";
            this.ckbVarMaxVel.UseVisualStyleBackColor = true;
            // 
            // nudKitMinVel
            // 
            this.nudKitMinVel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.nudKitMinVel.Location = new System.Drawing.Point(3, 37);
            this.nudKitMinVel.Maximum = new decimal(new int[] {
            127,
            0,
            0,
            0});
            this.nudKitMinVel.Name = "nudKitMinVel";
            this.nudKitMinVel.Size = new System.Drawing.Size(51, 22);
            this.nudKitMinVel.TabIndex = 1;
            this.nudKitMinVel.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudKitMinVel.ValueChanged += new System.EventHandler(this.nud_ValueChanged);
            // 
            // nudKitMaxVel
            // 
            this.nudKitMaxVel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.nudKitMaxVel.Location = new System.Drawing.Point(60, 37);
            this.nudKitMaxVel.Maximum = new decimal(new int[] {
            127,
            0,
            0,
            0});
            this.nudKitMaxVel.Name = "nudKitMaxVel";
            this.nudKitMaxVel.Size = new System.Drawing.Size(51, 22);
            this.nudKitMaxVel.TabIndex = 3;
            this.nudKitMaxVel.Value = new decimal(new int[] {
            127,
            0,
            0,
            0});
            this.nudKitMaxVel.ValueChanged += new System.EventHandler(this.nud_ValueChanged);
            // 
            // label21
            // 
            this.label21.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(13, 17);
            this.label21.Margin = new System.Windows.Forms.Padding(0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(30, 17);
            this.label21.TabIndex = 0;
            this.label21.Text = "Min";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label22
            // 
            this.label22.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(69, 17);
            this.label22.Margin = new System.Windows.Forms.Padding(0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(33, 17);
            this.label22.TabIndex = 0;
            this.label22.Text = "Max";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label23
            // 
            this.label23.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label23.AutoSize = true;
            this.tlpKitVelocity.SetColumnSpan(this.label23, 2);
            this.label23.Location = new System.Drawing.Point(28, 0);
            this.label23.Margin = new System.Windows.Forms.Padding(0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(57, 17);
            this.label23.TabIndex = 0;
            this.label23.Text = "Velocity";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // flowLayoutPanel14
            // 
            this.flowLayoutPanel14.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.flowLayoutPanel14.AutoSize = true;
            this.flowLayoutPanel14.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel14.Controls.Add(this.label33);
            this.flowLayoutPanel14.Controls.Add(this.nudVolume);
            this.flowLayoutPanel14.Controls.Add(this.ckbNoVolume);
            this.flowLayoutPanel14.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel14.Location = new System.Drawing.Point(123, 11);
            this.flowLayoutPanel14.Name = "flowLayoutPanel14";
            this.flowLayoutPanel14.Size = new System.Drawing.Size(59, 66);
            this.flowLayoutPanel14.TabIndex = 2;
            // 
            // label33
            // 
            this.label33.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(0, 0);
            this.label33.Margin = new System.Windows.Forms.Padding(0);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(59, 17);
            this.label33.TabIndex = 0;
            this.label33.Text = "Volume:";
            // 
            // nudVolume
            // 
            this.nudVolume.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.nudVolume.Location = new System.Drawing.Point(4, 20);
            this.nudVolume.Maximum = new decimal(new int[] {
            127,
            0,
            0,
            0});
            this.nudVolume.Name = "nudVolume";
            this.nudVolume.Size = new System.Drawing.Size(51, 22);
            this.nudVolume.TabIndex = 1;
            this.nudVolume.Value = new decimal(new int[] {
            127,
            0,
            0,
            0});
            this.nudVolume.ValueChanged += new System.EventHandler(this.nud_ValueChanged);
            // 
            // ckbNoVolume
            // 
            this.ckbNoVolume.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.ckbNoVolume.AutoSize = true;
            this.ckbNoVolume.Checked = true;
            this.ckbNoVolume.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbNoVolume.Location = new System.Drawing.Point(6, 45);
            this.ckbNoVolume.Margin = new System.Windows.Forms.Padding(0);
            this.ckbNoVolume.Name = "ckbNoVolume";
            this.ckbNoVolume.Size = new System.Drawing.Size(46, 21);
            this.ckbNoVolume.TabIndex = 5;
            this.ckbNoVolume.Text = "Off";
            this.ckbNoVolume.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.AutoSize = true;
            this.groupBox2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox2.Controls.Add(this.flowLayoutPanel2);
            this.groupBox2.Location = new System.Drawing.Point(3, 367);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(573, 268);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "&Pad Editor";
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.AutoSize = true;
            this.flowLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel2.Controls.Add(this.label3);
            this.flowLayoutPanel2.Controls.Add(this.cbPad);
            this.flowLayoutPanel2.Controls.Add(this.lbPadDataChanged);
            this.flowLayoutPanel2.Controls.Add(this.lbBreak2);
            this.flowLayoutPanel2.Controls.Add(this.tableLayoutPanel2);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 18);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(564, 229);
            this.flowLayoutPanel2.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 6);
            this.label3.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 17);
            this.label3.TabIndex = 0;
            this.label3.Text = "Select Pad:";
            // 
            // cbPad
            // 
            this.cbPad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPad.FormattingEnabled = true;
            this.cbPad.Items.AddRange(new object[] {
            "1",
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
            "25 bass",
            "26 chick",
            "27",
            "28"});
            this.cbPad.Location = new System.Drawing.Point(89, 3);
            this.cbPad.MaxDropDownItems = 28;
            this.cbPad.Name = "cbPad";
            this.cbPad.Size = new System.Drawing.Size(55, 24);
            this.cbPad.TabIndex = 1;
            this.cbPad.SelectedIndexChanged += new System.EventHandler(this.cbPad_SelectedIndexChanged);
            // 
            // lbPadDataChanged
            // 
            this.lbPadDataChanged.AutoSize = true;
            this.lbPadDataChanged.Location = new System.Drawing.Point(150, 6);
            this.lbPadDataChanged.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.lbPadDataChanged.Name = "lbPadDataChanged";
            this.lbPadDataChanged.Size = new System.Drawing.Size(48, 17);
            this.lbPadDataChanged.TabIndex = 0;
            this.lbPadDataChanged.Text = "Edited";
            this.lbPadDataChanged.Visible = false;
            // 
            // lbBreak2
            // 
            this.lbBreak2.AutoSize = true;
            this.flowLayoutPanel2.SetFlowBreak(this.lbBreak2, true);
            this.lbBreak2.Location = new System.Drawing.Point(204, 0);
            this.lbBreak2.Name = "lbBreak2";
            this.lbBreak2.Size = new System.Drawing.Size(12, 17);
            this.lbBreak2.TabIndex = 0;
            this.lbBreak2.Text = " ";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.flowLayoutPanel3, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.flowLayoutPanel4, 1, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 33);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(558, 193);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.AutoSize = true;
            this.flowLayoutPanel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel3.Controls.Add(this.label4);
            this.flowLayoutPanel3.Controls.Add(this.tableLayoutPanel1);
            this.flowLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(252, 186);
            this.flowLayoutPanel3.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 6);
            this.label4.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 17);
            this.label4.TabIndex = 0;
            this.label4.Text = "Notes:";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.label10, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label8, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label9, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.cbNote5, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.cbNote4, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.cbNote3, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.cbNote2, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.cbNote1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.cbNote6, 1, 5);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(58, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(191, 180);
            this.tableLayoutPanel1.TabIndex = 7;
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(3, 156);
            this.label10.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(16, 17);
            this.label10.TabIndex = 0;
            this.label10.Text = "6";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 6);
            this.label5.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(16, 17);
            this.label5.TabIndex = 0;
            this.label5.Text = "1";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 126);
            this.label6.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(16, 17);
            this.label6.TabIndex = 0;
            this.label6.Text = "5";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 96);
            this.label7.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(16, 17);
            this.label7.TabIndex = 0;
            this.label7.Text = "4";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 66);
            this.label8.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(16, 17);
            this.label8.TabIndex = 0;
            this.label8.Text = "3";
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 36);
            this.label9.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(16, 17);
            this.label9.TabIndex = 0;
            this.label9.Text = "2";
            // 
            // cbNote5
            // 
            this.cbNote5.FormattingEnabled = true;
            this.cbNote5.Items.AddRange(new object[] {
            "Off",
            "Sequencer Start",
            "Sequencer Stop",
            "Sequencer Continue",
            "Alternate Reset",
            "Next Kit",
            "Previous Kit"});
            this.cbNote5.Location = new System.Drawing.Point(25, 123);
            this.cbNote5.MaxDropDownItems = 7;
            this.cbNote5.MaxLength = 3;
            this.cbNote5.Name = "cbNote5";
            this.cbNote5.Size = new System.Drawing.Size(163, 24);
            this.cbNote5.TabIndex = 5;
            this.cbNote5.Validating += new System.ComponentModel.CancelEventHandler(this.cbNote_Validating);
            this.cbNote5.Validated += new System.EventHandler(this.cbNote_Validated);
            this.cbNote5.Enter += new System.EventHandler(this.cbNote_Enter);
            this.cbNote5.SelectedIndexChanged += new System.EventHandler(this.cbNote_SelectedIndexChanged);
            this.cbNote5.TextChanged += new System.EventHandler(this.cbNote_TextChanged);
            // 
            // cbNote4
            // 
            this.cbNote4.FormattingEnabled = true;
            this.cbNote4.Items.AddRange(new object[] {
            "Off",
            "Sequencer Start",
            "Sequencer Stop",
            "Sequencer Continue",
            "Alternate Reset",
            "Next Kit",
            "Previous Kit"});
            this.cbNote4.Location = new System.Drawing.Point(25, 93);
            this.cbNote4.MaxDropDownItems = 7;
            this.cbNote4.MaxLength = 3;
            this.cbNote4.Name = "cbNote4";
            this.cbNote4.Size = new System.Drawing.Size(163, 24);
            this.cbNote4.TabIndex = 4;
            this.cbNote4.Validating += new System.ComponentModel.CancelEventHandler(this.cbNote_Validating);
            this.cbNote4.Validated += new System.EventHandler(this.cbNote_Validated);
            this.cbNote4.Enter += new System.EventHandler(this.cbNote_Enter);
            this.cbNote4.SelectedIndexChanged += new System.EventHandler(this.cbNote_SelectedIndexChanged);
            this.cbNote4.TextChanged += new System.EventHandler(this.cbNote_TextChanged);
            // 
            // cbNote3
            // 
            this.cbNote3.FormattingEnabled = true;
            this.cbNote3.Items.AddRange(new object[] {
            "Off",
            "Sequencer Start",
            "Sequencer Stop",
            "Sequencer Continue",
            "Alternate Reset",
            "Next Kit",
            "Previous Kit"});
            this.cbNote3.Location = new System.Drawing.Point(25, 63);
            this.cbNote3.MaxDropDownItems = 7;
            this.cbNote3.MaxLength = 3;
            this.cbNote3.Name = "cbNote3";
            this.cbNote3.Size = new System.Drawing.Size(163, 24);
            this.cbNote3.TabIndex = 3;
            this.cbNote3.Validating += new System.ComponentModel.CancelEventHandler(this.cbNote_Validating);
            this.cbNote3.Validated += new System.EventHandler(this.cbNote_Validated);
            this.cbNote3.Enter += new System.EventHandler(this.cbNote_Enter);
            this.cbNote3.SelectedIndexChanged += new System.EventHandler(this.cbNote_SelectedIndexChanged);
            this.cbNote3.TextChanged += new System.EventHandler(this.cbNote_TextChanged);
            // 
            // cbNote2
            // 
            this.cbNote2.FormattingEnabled = true;
            this.cbNote2.Items.AddRange(new object[] {
            "Off",
            "Sequencer Start",
            "Sequencer Stop",
            "Sequencer Continue",
            "Alternate Reset",
            "Next Kit",
            "Previous Kit"});
            this.cbNote2.Location = new System.Drawing.Point(25, 33);
            this.cbNote2.MaxDropDownItems = 7;
            this.cbNote2.MaxLength = 3;
            this.cbNote2.Name = "cbNote2";
            this.cbNote2.Size = new System.Drawing.Size(163, 24);
            this.cbNote2.TabIndex = 2;
            this.cbNote2.Validating += new System.ComponentModel.CancelEventHandler(this.cbNote_Validating);
            this.cbNote2.Validated += new System.EventHandler(this.cbNote_Validated);
            this.cbNote2.Enter += new System.EventHandler(this.cbNote_Enter);
            this.cbNote2.SelectedIndexChanged += new System.EventHandler(this.cbNote_SelectedIndexChanged);
            this.cbNote2.TextChanged += new System.EventHandler(this.cbNote_TextChanged);
            // 
            // cbNote1
            // 
            this.cbNote1.FormattingEnabled = true;
            this.cbNote1.Items.AddRange(new object[] {
            "Off",
            "Sequencer Start",
            "Sequencer Stop",
            "Sequencer Continue",
            "Alternate Reset",
            "Next Kit",
            "Previous Kit"});
            this.cbNote1.Location = new System.Drawing.Point(25, 3);
            this.cbNote1.MaxDropDownItems = 7;
            this.cbNote1.MaxLength = 3;
            this.cbNote1.Name = "cbNote1";
            this.cbNote1.Size = new System.Drawing.Size(163, 24);
            this.cbNote1.TabIndex = 1;
            this.cbNote1.Validating += new System.ComponentModel.CancelEventHandler(this.cbNote_Validating);
            this.cbNote1.Validated += new System.EventHandler(this.cbNote_Validated);
            this.cbNote1.Enter += new System.EventHandler(this.cbNote_Enter);
            this.cbNote1.SelectedIndexChanged += new System.EventHandler(this.cbNote_SelectedIndexChanged);
            this.cbNote1.TextChanged += new System.EventHandler(this.cbNote_TextChanged);
            // 
            // cbNote6
            // 
            this.cbNote6.FormattingEnabled = true;
            this.cbNote6.Items.AddRange(new object[] {
            "Off",
            "Sequencer Start",
            "Sequencer Stop",
            "Sequencer Continue",
            "Alternate Reset",
            "Next Kit",
            "Previous Kit"});
            this.cbNote6.Location = new System.Drawing.Point(25, 153);
            this.cbNote6.MaxDropDownItems = 7;
            this.cbNote6.MaxLength = 3;
            this.cbNote6.Name = "cbNote6";
            this.cbNote6.Size = new System.Drawing.Size(163, 24);
            this.cbNote6.TabIndex = 6;
            this.cbNote6.Validating += new System.ComponentModel.CancelEventHandler(this.cbNote_Validating);
            this.cbNote6.Validated += new System.EventHandler(this.cbNote_Validated);
            this.cbNote6.Enter += new System.EventHandler(this.cbNote_Enter);
            this.cbNote6.SelectedIndexChanged += new System.EventHandler(this.cbNote_SelectedIndexChanged);
            this.cbNote6.TextChanged += new System.EventHandler(this.cbNote_TextChanged);
            // 
            // flowLayoutPanel4
            // 
            this.flowLayoutPanel4.AutoSize = true;
            this.flowLayoutPanel4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel4.Controls.Add(this.tableLayoutPanel3);
            this.flowLayoutPanel4.Controls.Add(this.tlpPadVelocity);
            this.flowLayoutPanel4.Controls.Add(this.tableLayoutPanel5);
            this.flowLayoutPanel4.Location = new System.Drawing.Point(261, 3);
            this.flowLayoutPanel4.Name = "flowLayoutPanel4";
            this.flowLayoutPanel4.Size = new System.Drawing.Size(286, 164);
            this.flowLayoutPanel4.TabIndex = 2;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.AutoSize = true;
            this.tableLayoutPanel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.Controls.Add(this.label13, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.label11, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.label12, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.cbPadGate, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.cbPadCurve, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.nudPadChannel, 1, 2);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 3;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(251, 88);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(3, 66);
            this.label13.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(64, 17);
            this.label13.TabIndex = 0;
            this.label13.Text = "Channel:";
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(18, 6);
            this.label11.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(49, 17);
            this.label11.TabIndex = 0;
            this.label11.Text = "Curve:";
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(24, 36);
            this.label12.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(43, 17);
            this.label12.TabIndex = 0;
            this.label12.Text = "Gate:";
            // 
            // cbPadGate
            // 
            this.cbPadGate.FormattingEnabled = true;
            this.cbPadGate.Items.AddRange(new object[] {
            "see Gate.cs"});
            this.cbPadGate.Location = new System.Drawing.Point(73, 33);
            this.cbPadGate.MaxDropDownItems = 3;
            this.cbPadGate.Name = "cbPadGate";
            this.cbPadGate.Size = new System.Drawing.Size(107, 24);
            this.cbPadGate.TabIndex = 2;
            this.cbPadGate.Validating += new System.ComponentModel.CancelEventHandler(this.cbGate_Validating);
            this.cbPadGate.Validated += new System.EventHandler(this.cbGate_Validated);
            this.cbPadGate.Enter += new System.EventHandler(this.cbGate_Enter);
            this.cbPadGate.TextUpdate += new System.EventHandler(this.cbGate_TextChanged);
            // 
            // cbPadCurve
            // 
            this.cbPadCurve.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPadCurve.FormattingEnabled = true;
            this.cbPadCurve.Items.AddRange(new object[] {
            "see Curve.cs"});
            this.cbPadCurve.Location = new System.Drawing.Point(73, 3);
            this.cbPadCurve.MaxDropDownItems = 25;
            this.cbPadCurve.Name = "cbPadCurve";
            this.cbPadCurve.Size = new System.Drawing.Size(175, 24);
            this.cbPadCurve.TabIndex = 1;
            this.cbPadCurve.SelectedIndexChanged += new System.EventHandler(this.cbCurve_SelectedIndexChanged);
            // 
            // nudPadChannel
            // 
            this.nudPadChannel.Location = new System.Drawing.Point(73, 63);
            this.nudPadChannel.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.nudPadChannel.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudPadChannel.Name = "nudPadChannel";
            this.nudPadChannel.Size = new System.Drawing.Size(42, 22);
            this.nudPadChannel.TabIndex = 3;
            this.nudPadChannel.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudPadChannel.ValueChanged += new System.EventHandler(this.nud_ValueChanged);
            // 
            // tlpPadVelocity
            // 
            this.tlpPadVelocity.AutoSize = true;
            this.tlpPadVelocity.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tlpPadVelocity.BackColor = System.Drawing.SystemColors.Control;
            this.tlpPadVelocity.ColumnCount = 2;
            this.tlpPadVelocity.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpPadVelocity.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpPadVelocity.Controls.Add(this.nudPadMinVel, 0, 2);
            this.tlpPadVelocity.Controls.Add(this.nudPadMaxVel, 0, 2);
            this.tlpPadVelocity.Controls.Add(this.label14, 0, 1);
            this.tlpPadVelocity.Controls.Add(this.label15, 1, 1);
            this.tlpPadVelocity.Controls.Add(this.label17, 0, 0);
            this.tlpPadVelocity.Location = new System.Drawing.Point(3, 97);
            this.tlpPadVelocity.Name = "tlpPadVelocity";
            this.tlpPadVelocity.RowCount = 3;
            this.tlpPadVelocity.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpPadVelocity.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpPadVelocity.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpPadVelocity.Size = new System.Drawing.Size(114, 62);
            this.tlpPadVelocity.TabIndex = 2;
            // 
            // nudPadMinVel
            // 
            this.nudPadMinVel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.nudPadMinVel.Location = new System.Drawing.Point(3, 37);
            this.nudPadMinVel.Maximum = new decimal(new int[] {
            127,
            0,
            0,
            0});
            this.nudPadMinVel.Name = "nudPadMinVel";
            this.nudPadMinVel.Size = new System.Drawing.Size(51, 22);
            this.nudPadMinVel.TabIndex = 1;
            this.nudPadMinVel.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudPadMinVel.ValueChanged += new System.EventHandler(this.nud_ValueChanged);
            // 
            // nudPadMaxVel
            // 
            this.nudPadMaxVel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.nudPadMaxVel.Location = new System.Drawing.Point(60, 37);
            this.nudPadMaxVel.Maximum = new decimal(new int[] {
            127,
            0,
            0,
            0});
            this.nudPadMaxVel.Name = "nudPadMaxVel";
            this.nudPadMaxVel.Size = new System.Drawing.Size(51, 22);
            this.nudPadMaxVel.TabIndex = 2;
            this.nudPadMaxVel.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudPadMaxVel.ValueChanged += new System.EventHandler(this.nud_ValueChanged);
            // 
            // label14
            // 
            this.label14.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(13, 17);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(30, 17);
            this.label14.TabIndex = 0;
            this.label14.Text = "Min";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label15
            // 
            this.label15.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(69, 17);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(33, 17);
            this.label15.TabIndex = 0;
            this.label15.Text = "Max";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label17
            // 
            this.label17.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label17.AutoSize = true;
            this.tlpPadVelocity.SetColumnSpan(this.label17, 2);
            this.label17.Location = new System.Drawing.Point(28, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(57, 17);
            this.label17.TabIndex = 0;
            this.label17.Text = "Velocity";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.AutoSize = true;
            this.tableLayoutPanel5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel5.ColumnCount = 8;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel5.Controls.Add(this.label18, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.ckbF7, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.ckbF6, 1, 1);
            this.tableLayoutPanel5.Controls.Add(this.ckbF5, 2, 1);
            this.tableLayoutPanel5.Controls.Add(this.ckbF4, 3, 1);
            this.tableLayoutPanel5.Controls.Add(this.ckbF3, 4, 1);
            this.tableLayoutPanel5.Controls.Add(this.ckbF2, 5, 1);
            this.tableLayoutPanel5.Controls.Add(this.ckbF1, 6, 1);
            this.tableLayoutPanel5.Controls.Add(this.ckbF0, 7, 1);
            this.tableLayoutPanel5.Location = new System.Drawing.Point(123, 97);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 2;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.Size = new System.Drawing.Size(160, 64);
            this.tableLayoutPanel5.TabIndex = 4;
            // 
            // label18
            // 
            this.label18.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label18.AutoSize = true;
            this.tableLayoutPanel5.SetColumnSpan(this.label18, 8);
            this.label18.Location = new System.Drawing.Point(59, 6);
            this.label18.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(42, 17);
            this.label18.TabIndex = 1;
            this.label18.Text = "Flags";
            this.label18.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // ckbF7
            // 
            this.ckbF7.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.ckbF7.AutoSize = true;
            this.ckbF7.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ckbF7.Location = new System.Drawing.Point(0, 26);
            this.ckbF7.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.ckbF7.Name = "ckbF7";
            this.ckbF7.Size = new System.Drawing.Size(20, 35);
            this.ckbF7.TabIndex = 2;
            this.ckbF7.Text = "7";
            this.ckbF7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ckbF7.UseVisualStyleBackColor = true;
            this.ckbF7.CheckedChanged += new System.EventHandler(this.ckbFlag_CheckedChanged);
            // 
            // ckbF6
            // 
            this.ckbF6.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.ckbF6.AutoSize = true;
            this.ckbF6.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ckbF6.Location = new System.Drawing.Point(20, 26);
            this.ckbF6.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.ckbF6.Name = "ckbF6";
            this.ckbF6.Size = new System.Drawing.Size(20, 35);
            this.ckbF6.TabIndex = 2;
            this.ckbF6.Text = "6";
            this.ckbF6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ckbF6.UseVisualStyleBackColor = true;
            this.ckbF6.CheckedChanged += new System.EventHandler(this.ckbFlag_CheckedChanged);
            // 
            // ckbF5
            // 
            this.ckbF5.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.ckbF5.AutoSize = true;
            this.ckbF5.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ckbF5.Location = new System.Drawing.Point(40, 26);
            this.ckbF5.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.ckbF5.Name = "ckbF5";
            this.ckbF5.Size = new System.Drawing.Size(20, 35);
            this.ckbF5.TabIndex = 2;
            this.ckbF5.Text = "5";
            this.ckbF5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ckbF5.UseVisualStyleBackColor = true;
            this.ckbF5.CheckedChanged += new System.EventHandler(this.ckbFlag_CheckedChanged);
            // 
            // ckbF4
            // 
            this.ckbF4.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.ckbF4.AutoSize = true;
            this.ckbF4.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ckbF4.Location = new System.Drawing.Point(60, 26);
            this.ckbF4.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.ckbF4.Name = "ckbF4";
            this.ckbF4.Size = new System.Drawing.Size(20, 35);
            this.ckbF4.TabIndex = 2;
            this.ckbF4.Text = "4";
            this.ckbF4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ckbF4.UseVisualStyleBackColor = true;
            this.ckbF4.CheckedChanged += new System.EventHandler(this.ckbFlag_CheckedChanged);
            // 
            // ckbF3
            // 
            this.ckbF3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.ckbF3.AutoSize = true;
            this.ckbF3.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ckbF3.Location = new System.Drawing.Point(80, 26);
            this.ckbF3.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.ckbF3.Name = "ckbF3";
            this.ckbF3.Size = new System.Drawing.Size(20, 35);
            this.ckbF3.TabIndex = 2;
            this.ckbF3.Text = "3";
            this.ckbF3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ckbF3.UseVisualStyleBackColor = true;
            this.ckbF3.CheckedChanged += new System.EventHandler(this.ckbFlag_CheckedChanged);
            // 
            // ckbF2
            // 
            this.ckbF2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.ckbF2.AutoSize = true;
            this.ckbF2.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ckbF2.Location = new System.Drawing.Point(100, 26);
            this.ckbF2.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.ckbF2.Name = "ckbF2";
            this.ckbF2.Size = new System.Drawing.Size(20, 35);
            this.ckbF2.TabIndex = 2;
            this.ckbF2.Text = "2";
            this.ckbF2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ckbF2.UseVisualStyleBackColor = true;
            this.ckbF2.CheckedChanged += new System.EventHandler(this.ckbFlag_CheckedChanged);
            // 
            // ckbF1
            // 
            this.ckbF1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.ckbF1.AutoSize = true;
            this.ckbF1.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ckbF1.Location = new System.Drawing.Point(120, 26);
            this.ckbF1.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.ckbF1.Name = "ckbF1";
            this.ckbF1.Size = new System.Drawing.Size(20, 35);
            this.ckbF1.TabIndex = 2;
            this.ckbF1.Text = "1";
            this.ckbF1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ckbF1.UseVisualStyleBackColor = true;
            this.ckbF1.CheckedChanged += new System.EventHandler(this.ckbFlag_CheckedChanged);
            // 
            // ckbF0
            // 
            this.ckbF0.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.ckbF0.AutoSize = true;
            this.ckbF0.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ckbF0.Location = new System.Drawing.Point(140, 26);
            this.ckbF0.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.ckbF0.Name = "ckbF0";
            this.ckbF0.Size = new System.Drawing.Size(20, 35);
            this.ckbF0.TabIndex = 2;
            this.ckbF0.Text = "0";
            this.ckbF0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ckbF0.UseVisualStyleBackColor = true;
            this.ckbF0.CheckedChanged += new System.EventHandler(this.ckbFlag_CheckedChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.lbGlobalDataChanged);
            this.tabPage2.Controls.Add(this.flowLayoutPanel26);
            this.tabPage2.Controls.Add(this.flowLayoutPanel25);
            this.tabPage2.Controls.Add(this.flowLayoutPanel10);
            this.tabPage2.Controls.Add(this.flowLayoutPanel55);
            this.tabPage2.Controls.Add(this.flowLayoutPanel54);
            this.tabPage2.Controls.Add(this.flowLayoutPanel53);
            this.tabPage2.Controls.Add(this.flowLayoutPanel52);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(582, 648);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Global";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // lbGlobalDataChanged
            // 
            this.lbGlobalDataChanged.AutoSize = true;
            this.lbGlobalDataChanged.Location = new System.Drawing.Point(6, 6);
            this.lbGlobalDataChanged.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.lbGlobalDataChanged.Name = "lbGlobalDataChanged";
            this.lbGlobalDataChanged.Size = new System.Drawing.Size(48, 17);
            this.lbGlobalDataChanged.TabIndex = 8;
            this.lbGlobalDataChanged.Text = "Edited";
            this.lbGlobalDataChanged.Visible = false;
            // 
            // flowLayoutPanel26
            // 
            this.flowLayoutPanel26.Controls.Add(this.flowLayoutPanel38);
            this.flowLayoutPanel26.Controls.Add(this.flowLayoutPanel21);
            this.flowLayoutPanel26.Controls.Add(this.flowLayoutPanel22);
            this.flowLayoutPanel26.Controls.Add(this.flowLayoutPanel28);
            this.flowLayoutPanel26.Controls.Add(this.flowLayoutPanel61);
            this.flowLayoutPanel26.Controls.Add(this.flowLayoutPanel62);
            this.flowLayoutPanel26.Controls.Add(this.flowLayoutPanel47);
            this.flowLayoutPanel26.Controls.Add(this.flowLayoutPanel29);
            this.flowLayoutPanel26.Controls.Add(this.flowLayoutPanel49);
            this.flowLayoutPanel26.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel26.Location = new System.Drawing.Point(6, 358);
            this.flowLayoutPanel26.Name = "flowLayoutPanel26";
            this.flowLayoutPanel26.Size = new System.Drawing.Size(567, 109);
            this.flowLayoutPanel26.TabIndex = 6;
            // 
            // flowLayoutPanel38
            // 
            this.flowLayoutPanel38.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel38.AutoSize = true;
            this.flowLayoutPanel38.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel38.Controls.Add(this.label56);
            this.flowLayoutPanel38.Controls.Add(this.nudFcOpenRegion);
            this.flowLayoutPanel38.Location = new System.Drawing.Point(11, 3);
            this.flowLayoutPanel38.Name = "flowLayoutPanel38";
            this.flowLayoutPanel38.Size = new System.Drawing.Size(156, 28);
            this.flowLayoutPanel38.TabIndex = 1;
            // 
            // label56
            // 
            this.label56.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label56.AutoSize = true;
            this.label56.Location = new System.Drawing.Point(0, 6);
            this.label56.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(99, 17);
            this.label56.TabIndex = 0;
            this.label56.Text = "fcOpenRegion";
            // 
            // nudFcOpenRegion
            // 
            this.nudFcOpenRegion.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.nudFcOpenRegion.Location = new System.Drawing.Point(102, 3);
            this.nudFcOpenRegion.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudFcOpenRegion.Name = "nudFcOpenRegion";
            this.nudFcOpenRegion.Size = new System.Drawing.Size(51, 22);
            this.nudFcOpenRegion.TabIndex = 1;
            this.nudFcOpenRegion.ValueChanged += new System.EventHandler(this.nud_ValueChanged);
            // 
            // flowLayoutPanel21
            // 
            this.flowLayoutPanel21.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel21.AutoSize = true;
            this.flowLayoutPanel21.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel21.Controls.Add(this.label43);
            this.flowLayoutPanel21.Controls.Add(this.nudFcClosedRegion);
            this.flowLayoutPanel21.Location = new System.Drawing.Point(3, 37);
            this.flowLayoutPanel21.Name = "flowLayoutPanel21";
            this.flowLayoutPanel21.Size = new System.Drawing.Size(164, 28);
            this.flowLayoutPanel21.TabIndex = 2;
            // 
            // label43
            // 
            this.label43.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label43.AutoSize = true;
            this.label43.Location = new System.Drawing.Point(0, 6);
            this.label43.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(107, 17);
            this.label43.TabIndex = 0;
            this.label43.Text = "fcClosedRegion";
            // 
            // nudFcClosedRegion
            // 
            this.nudFcClosedRegion.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.nudFcClosedRegion.Location = new System.Drawing.Point(110, 3);
            this.nudFcClosedRegion.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudFcClosedRegion.Name = "nudFcClosedRegion";
            this.nudFcClosedRegion.Size = new System.Drawing.Size(51, 22);
            this.nudFcClosedRegion.TabIndex = 1;
            this.nudFcClosedRegion.ValueChanged += new System.EventHandler(this.nud_ValueChanged);
            // 
            // flowLayoutPanel22
            // 
            this.flowLayoutPanel22.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel22.AutoSize = true;
            this.flowLayoutPanel22.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel22.Controls.Add(this.label41);
            this.flowLayoutPanel22.Controls.Add(this.nudFcPolarity);
            this.flowLayoutPanel22.Location = new System.Drawing.Point(44, 71);
            this.flowLayoutPanel22.Name = "flowLayoutPanel22";
            this.flowLayoutPanel22.Size = new System.Drawing.Size(123, 28);
            this.flowLayoutPanel22.TabIndex = 3;
            // 
            // label41
            // 
            this.label41.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label41.AutoSize = true;
            this.label41.Location = new System.Drawing.Point(0, 6);
            this.label41.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(66, 17);
            this.label41.TabIndex = 0;
            this.label41.Text = "fcPolarity";
            // 
            // nudFcPolarity
            // 
            this.nudFcPolarity.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.nudFcPolarity.Location = new System.Drawing.Point(69, 3);
            this.nudFcPolarity.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudFcPolarity.Name = "nudFcPolarity";
            this.nudFcPolarity.Size = new System.Drawing.Size(51, 22);
            this.nudFcPolarity.TabIndex = 1;
            this.nudFcPolarity.ValueChanged += new System.EventHandler(this.nud_ValueChanged);
            // 
            // flowLayoutPanel28
            // 
            this.flowLayoutPanel28.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel28.AutoSize = true;
            this.flowLayoutPanel28.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel28.Controls.Add(this.label39);
            this.flowLayoutPanel28.Controls.Add(this.nudFcVelocityLevel);
            this.flowLayoutPanel28.Location = new System.Drawing.Point(173, 3);
            this.flowLayoutPanel28.Name = "flowLayoutPanel28";
            this.flowLayoutPanel28.Size = new System.Drawing.Size(159, 28);
            this.flowLayoutPanel28.TabIndex = 4;
            // 
            // label39
            // 
            this.label39.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label39.AutoSize = true;
            this.label39.Location = new System.Drawing.Point(0, 6);
            this.label39.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(102, 17);
            this.label39.TabIndex = 0;
            this.label39.Text = "fcVelocityLevel";
            // 
            // nudFcVelocityLevel
            // 
            this.nudFcVelocityLevel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.nudFcVelocityLevel.Location = new System.Drawing.Point(105, 3);
            this.nudFcVelocityLevel.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudFcVelocityLevel.Name = "nudFcVelocityLevel";
            this.nudFcVelocityLevel.Size = new System.Drawing.Size(51, 22);
            this.nudFcVelocityLevel.TabIndex = 1;
            this.nudFcVelocityLevel.ValueChanged += new System.EventHandler(this.nud_ValueChanged);
            // 
            // flowLayoutPanel61
            // 
            this.flowLayoutPanel61.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel61.AutoSize = true;
            this.flowLayoutPanel61.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel61.Controls.Add(this.label66);
            this.flowLayoutPanel61.Controls.Add(this.nudFcLowLevel);
            this.flowLayoutPanel61.Location = new System.Drawing.Point(197, 37);
            this.flowLayoutPanel61.Name = "flowLayoutPanel61";
            this.flowLayoutPanel61.Size = new System.Drawing.Size(135, 28);
            this.flowLayoutPanel61.TabIndex = 5;
            // 
            // label66
            // 
            this.label66.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label66.AutoSize = true;
            this.label66.Location = new System.Drawing.Point(0, 6);
            this.label66.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label66.Name = "label66";
            this.label66.Size = new System.Drawing.Size(78, 17);
            this.label66.TabIndex = 0;
            this.label66.Text = "fcLowLevel";
            // 
            // nudFcLowLevel
            // 
            this.nudFcLowLevel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.nudFcLowLevel.Location = new System.Drawing.Point(81, 3);
            this.nudFcLowLevel.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudFcLowLevel.Name = "nudFcLowLevel";
            this.nudFcLowLevel.Size = new System.Drawing.Size(51, 22);
            this.nudFcLowLevel.TabIndex = 1;
            this.nudFcLowLevel.ValueChanged += new System.EventHandler(this.nud_ValueChanged);
            // 
            // flowLayoutPanel62
            // 
            this.flowLayoutPanel62.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel62.AutoSize = true;
            this.flowLayoutPanel62.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel62.Controls.Add(this.label75);
            this.flowLayoutPanel62.Controls.Add(this.nudFcHighLevel);
            this.flowLayoutPanel26.SetFlowBreak(this.flowLayoutPanel62, true);
            this.flowLayoutPanel62.Location = new System.Drawing.Point(193, 71);
            this.flowLayoutPanel62.Name = "flowLayoutPanel62";
            this.flowLayoutPanel62.Size = new System.Drawing.Size(139, 28);
            this.flowLayoutPanel62.TabIndex = 6;
            // 
            // label75
            // 
            this.label75.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label75.AutoSize = true;
            this.label75.Location = new System.Drawing.Point(0, 6);
            this.label75.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label75.Name = "label75";
            this.label75.Size = new System.Drawing.Size(82, 17);
            this.label75.TabIndex = 0;
            this.label75.Text = "fcHighLevel";
            // 
            // nudFcHighLevel
            // 
            this.nudFcHighLevel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.nudFcHighLevel.Location = new System.Drawing.Point(85, 3);
            this.nudFcHighLevel.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudFcHighLevel.Name = "nudFcHighLevel";
            this.nudFcHighLevel.Size = new System.Drawing.Size(51, 22);
            this.nudFcHighLevel.TabIndex = 1;
            this.nudFcHighLevel.ValueChanged += new System.EventHandler(this.nud_ValueChanged);
            // 
            // flowLayoutPanel47
            // 
            this.flowLayoutPanel47.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel47.AutoSize = true;
            this.flowLayoutPanel47.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel47.Controls.Add(this.label65);
            this.flowLayoutPanel47.Controls.Add(this.nudFcSplashEase);
            this.flowLayoutPanel47.Location = new System.Drawing.Point(360, 3);
            this.flowLayoutPanel47.Name = "flowLayoutPanel47";
            this.flowLayoutPanel47.Size = new System.Drawing.Size(151, 28);
            this.flowLayoutPanel47.TabIndex = 7;
            // 
            // label65
            // 
            this.label65.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label65.AutoSize = true;
            this.label65.Location = new System.Drawing.Point(0, 6);
            this.label65.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label65.Name = "label65";
            this.label65.Size = new System.Drawing.Size(94, 17);
            this.label65.TabIndex = 0;
            this.label65.Text = "fcSplashEase";
            // 
            // nudFcSplashEase
            // 
            this.nudFcSplashEase.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.nudFcSplashEase.Location = new System.Drawing.Point(97, 3);
            this.nudFcSplashEase.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudFcSplashEase.Name = "nudFcSplashEase";
            this.nudFcSplashEase.Size = new System.Drawing.Size(51, 22);
            this.nudFcSplashEase.TabIndex = 1;
            this.nudFcSplashEase.ValueChanged += new System.EventHandler(this.nud_ValueChanged);
            // 
            // flowLayoutPanel29
            // 
            this.flowLayoutPanel29.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel29.AutoSize = true;
            this.flowLayoutPanel29.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel29.Controls.Add(this.label44);
            this.flowLayoutPanel29.Controls.Add(this.nudFcWaitModeLevel);
            this.flowLayoutPanel29.Location = new System.Drawing.Point(338, 37);
            this.flowLayoutPanel29.Name = "flowLayoutPanel29";
            this.flowLayoutPanel29.Size = new System.Drawing.Size(173, 28);
            this.flowLayoutPanel29.TabIndex = 8;
            // 
            // label44
            // 
            this.label44.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label44.AutoSize = true;
            this.label44.Location = new System.Drawing.Point(0, 6);
            this.label44.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(116, 17);
            this.label44.TabIndex = 0;
            this.label44.Text = "fcWaitModeLevel";
            // 
            // nudFcWaitModeLevel
            // 
            this.nudFcWaitModeLevel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.nudFcWaitModeLevel.Location = new System.Drawing.Point(119, 3);
            this.nudFcWaitModeLevel.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudFcWaitModeLevel.Name = "nudFcWaitModeLevel";
            this.nudFcWaitModeLevel.Size = new System.Drawing.Size(51, 22);
            this.nudFcWaitModeLevel.TabIndex = 1;
            this.nudFcWaitModeLevel.ValueChanged += new System.EventHandler(this.nud_ValueChanged);
            // 
            // flowLayoutPanel49
            // 
            this.flowLayoutPanel49.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel49.AutoSize = true;
            this.flowLayoutPanel49.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel49.Controls.Add(this.label69);
            this.flowLayoutPanel49.Controls.Add(this.nudHatNoteGate);
            this.flowLayoutPanel49.Location = new System.Drawing.Point(365, 71);
            this.flowLayoutPanel49.Name = "flowLayoutPanel49";
            this.flowLayoutPanel49.Size = new System.Drawing.Size(146, 28);
            this.flowLayoutPanel49.TabIndex = 9;
            // 
            // label69
            // 
            this.label69.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label69.AutoSize = true;
            this.label69.Location = new System.Drawing.Point(0, 6);
            this.label69.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label69.Name = "label69";
            this.label69.Size = new System.Drawing.Size(89, 17);
            this.label69.TabIndex = 76;
            this.label69.Text = "hatNoteGate";
            // 
            // nudHatNoteGate
            // 
            this.nudHatNoteGate.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.nudHatNoteGate.Location = new System.Drawing.Point(92, 3);
            this.nudHatNoteGate.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudHatNoteGate.Name = "nudHatNoteGate";
            this.nudHatNoteGate.Size = new System.Drawing.Size(51, 22);
            this.nudHatNoteGate.TabIndex = 1;
            this.nudHatNoteGate.ValueChanged += new System.EventHandler(this.nud_ValueChanged);
            // 
            // flowLayoutPanel25
            // 
            this.flowLayoutPanel25.Controls.Add(this.flowLayoutPanel24);
            this.flowLayoutPanel25.Controls.Add(this.flowLayoutPanel56);
            this.flowLayoutPanel25.Controls.Add(this.flowLayoutPanel57);
            this.flowLayoutPanel25.Controls.Add(this.flowLayoutPanel27);
            this.flowLayoutPanel25.Controls.Add(this.flowLayoutPanel39);
            this.flowLayoutPanel25.Controls.Add(this.flowLayoutPanel40);
            this.flowLayoutPanel25.Controls.Add(this.flowLayoutPanel58);
            this.flowLayoutPanel25.Controls.Add(this.flowLayoutPanel41);
            this.flowLayoutPanel25.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel25.Location = new System.Drawing.Point(6, 473);
            this.flowLayoutPanel25.Name = "flowLayoutPanel25";
            this.flowLayoutPanel25.Size = new System.Drawing.Size(567, 112);
            this.flowLayoutPanel25.TabIndex = 7;
            // 
            // flowLayoutPanel24
            // 
            this.flowLayoutPanel24.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel24.AutoSize = true;
            this.flowLayoutPanel24.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel24.Controls.Add(this.label34);
            this.flowLayoutPanel24.Controls.Add(this.cbPadDynamics);
            this.flowLayoutPanel24.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel24.Name = "flowLayoutPanel24";
            this.flowLayoutPanel24.Size = new System.Drawing.Size(159, 30);
            this.flowLayoutPanel24.TabIndex = 1;
            // 
            // label34
            // 
            this.label34.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(0, 6);
            this.label34.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(98, 17);
            this.label34.TabIndex = 0;
            this.label34.Text = "Pad Dynamics";
            // 
            // cbPadDynamics
            // 
            this.cbPadDynamics.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPadDynamics.FormattingEnabled = true;
            this.cbPadDynamics.Items.AddRange(new object[] {
            "1",
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
            this.cbPadDynamics.Location = new System.Drawing.Point(101, 3);
            this.cbPadDynamics.MaxDropDownItems = 28;
            this.cbPadDynamics.Name = "cbPadDynamics";
            this.cbPadDynamics.Size = new System.Drawing.Size(55, 24);
            this.cbPadDynamics.TabIndex = 1;
            this.cbPadDynamics.SelectedIndexChanged += new System.EventHandler(this.cbPadDynamics_SelectedIndexChanged);
            // 
            // flowLayoutPanel56
            // 
            this.flowLayoutPanel56.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel56.AutoSize = true;
            this.flowLayoutPanel56.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel56.Controls.Add(this.label73);
            this.flowLayoutPanel56.Controls.Add(this.nudLowLevel);
            this.flowLayoutPanel56.Location = new System.Drawing.Point(43, 39);
            this.flowLayoutPanel56.Name = "flowLayoutPanel56";
            this.flowLayoutPanel56.Size = new System.Drawing.Size(119, 28);
            this.flowLayoutPanel56.TabIndex = 2;
            // 
            // label73
            // 
            this.label73.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label73.AutoSize = true;
            this.label73.Location = new System.Drawing.Point(0, 6);
            this.label73.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label73.Name = "label73";
            this.label73.Size = new System.Drawing.Size(62, 17);
            this.label73.TabIndex = 0;
            this.label73.Text = "lowLevel";
            // 
            // nudLowLevel
            // 
            this.nudLowLevel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.nudLowLevel.Location = new System.Drawing.Point(65, 3);
            this.nudLowLevel.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudLowLevel.Name = "nudLowLevel";
            this.nudLowLevel.Size = new System.Drawing.Size(51, 22);
            this.nudLowLevel.TabIndex = 1;
            this.nudLowLevel.ValueChanged += new System.EventHandler(this.nud_ValueChanged);
            // 
            // flowLayoutPanel57
            // 
            this.flowLayoutPanel57.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel57.AutoSize = true;
            this.flowLayoutPanel57.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel57.Controls.Add(this.label74);
            this.flowLayoutPanel57.Controls.Add(this.nudHighLevel);
            this.flowLayoutPanel25.SetFlowBreak(this.flowLayoutPanel57, true);
            this.flowLayoutPanel57.Location = new System.Drawing.Point(36, 73);
            this.flowLayoutPanel57.Name = "flowLayoutPanel57";
            this.flowLayoutPanel57.Size = new System.Drawing.Size(126, 28);
            this.flowLayoutPanel57.TabIndex = 3;
            // 
            // label74
            // 
            this.label74.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label74.AutoSize = true;
            this.label74.Location = new System.Drawing.Point(0, 6);
            this.label74.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label74.Name = "label74";
            this.label74.Size = new System.Drawing.Size(69, 17);
            this.label74.TabIndex = 0;
            this.label74.Text = "highLevel";
            // 
            // nudHighLevel
            // 
            this.nudHighLevel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.nudHighLevel.Location = new System.Drawing.Point(72, 3);
            this.nudHighLevel.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudHighLevel.Name = "nudHighLevel";
            this.nudHighLevel.Size = new System.Drawing.Size(51, 22);
            this.nudHighLevel.TabIndex = 1;
            this.nudHighLevel.ValueChanged += new System.EventHandler(this.nud_ValueChanged);
            // 
            // flowLayoutPanel27
            // 
            this.flowLayoutPanel27.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel27.AutoSize = true;
            this.flowLayoutPanel27.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel27.Controls.Add(this.label46);
            this.flowLayoutPanel27.Controls.Add(this.nudThresholdManual);
            this.flowLayoutPanel27.Location = new System.Drawing.Point(168, 3);
            this.flowLayoutPanel27.Name = "flowLayoutPanel27";
            this.flowLayoutPanel27.Size = new System.Drawing.Size(170, 28);
            this.flowLayoutPanel27.TabIndex = 4;
            // 
            // label46
            // 
            this.label46.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label46.AutoSize = true;
            this.label46.Location = new System.Drawing.Point(0, 6);
            this.label46.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(113, 17);
            this.label46.TabIndex = 0;
            this.label46.Text = "thresholdManual";
            // 
            // nudThresholdManual
            // 
            this.nudThresholdManual.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.nudThresholdManual.Location = new System.Drawing.Point(116, 3);
            this.nudThresholdManual.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudThresholdManual.Name = "nudThresholdManual";
            this.nudThresholdManual.Size = new System.Drawing.Size(51, 22);
            this.nudThresholdManual.TabIndex = 1;
            this.nudThresholdManual.ValueChanged += new System.EventHandler(this.nud_ValueChanged);
            // 
            // flowLayoutPanel39
            // 
            this.flowLayoutPanel39.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel39.AutoSize = true;
            this.flowLayoutPanel39.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel39.Controls.Add(this.label47);
            this.flowLayoutPanel39.Controls.Add(this.nudThresholdActual);
            this.flowLayoutPanel25.SetFlowBreak(this.flowLayoutPanel39, true);
            this.flowLayoutPanel39.Location = new System.Drawing.Point(175, 37);
            this.flowLayoutPanel39.Name = "flowLayoutPanel39";
            this.flowLayoutPanel39.Size = new System.Drawing.Size(163, 28);
            this.flowLayoutPanel39.TabIndex = 5;
            // 
            // label47
            // 
            this.label47.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label47.AutoSize = true;
            this.label47.Location = new System.Drawing.Point(0, 6);
            this.label47.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(106, 17);
            this.label47.TabIndex = 0;
            this.label47.Text = "thresholdActual";
            // 
            // nudThresholdActual
            // 
            this.nudThresholdActual.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.nudThresholdActual.Location = new System.Drawing.Point(109, 3);
            this.nudThresholdActual.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudThresholdActual.Name = "nudThresholdActual";
            this.nudThresholdActual.Size = new System.Drawing.Size(51, 22);
            this.nudThresholdActual.TabIndex = 1;
            this.nudThresholdActual.ValueChanged += new System.EventHandler(this.nud_ValueChanged);
            // 
            // flowLayoutPanel40
            // 
            this.flowLayoutPanel40.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel40.AutoSize = true;
            this.flowLayoutPanel40.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel40.Controls.Add(this.label48);
            this.flowLayoutPanel40.Controls.Add(this.nudUserMargin);
            this.flowLayoutPanel40.Location = new System.Drawing.Point(363, 3);
            this.flowLayoutPanel40.Name = "flowLayoutPanel40";
            this.flowLayoutPanel40.Size = new System.Drawing.Size(136, 28);
            this.flowLayoutPanel40.TabIndex = 6;
            // 
            // label48
            // 
            this.label48.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label48.AutoSize = true;
            this.label48.Location = new System.Drawing.Point(0, 6);
            this.label48.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(79, 17);
            this.label48.TabIndex = 0;
            this.label48.Text = "userMargin";
            // 
            // nudUserMargin
            // 
            this.nudUserMargin.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.nudUserMargin.Location = new System.Drawing.Point(82, 3);
            this.nudUserMargin.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudUserMargin.Name = "nudUserMargin";
            this.nudUserMargin.Size = new System.Drawing.Size(51, 22);
            this.nudUserMargin.TabIndex = 1;
            this.nudUserMargin.ValueChanged += new System.EventHandler(this.nud_ValueChanged);
            // 
            // flowLayoutPanel58
            // 
            this.flowLayoutPanel58.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel58.AutoSize = true;
            this.flowLayoutPanel58.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel58.Controls.Add(this.label50);
            this.flowLayoutPanel58.Controls.Add(this.nudInternalMargin);
            this.flowLayoutPanel58.Location = new System.Drawing.Point(344, 37);
            this.flowLayoutPanel58.Name = "flowLayoutPanel58";
            this.flowLayoutPanel58.Size = new System.Drawing.Size(155, 28);
            this.flowLayoutPanel58.TabIndex = 7;
            // 
            // label50
            // 
            this.label50.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label50.AutoSize = true;
            this.label50.Location = new System.Drawing.Point(0, 6);
            this.label50.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(98, 17);
            this.label50.TabIndex = 0;
            this.label50.Text = "internalMargin";
            // 
            // nudInternalMargin
            // 
            this.nudInternalMargin.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.nudInternalMargin.Location = new System.Drawing.Point(101, 3);
            this.nudInternalMargin.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudInternalMargin.Name = "nudInternalMargin";
            this.nudInternalMargin.Size = new System.Drawing.Size(51, 22);
            this.nudInternalMargin.TabIndex = 1;
            this.nudInternalMargin.ValueChanged += new System.EventHandler(this.nud_ValueChanged);
            // 
            // flowLayoutPanel41
            // 
            this.flowLayoutPanel41.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel41.AutoSize = true;
            this.flowLayoutPanel41.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel41.Controls.Add(this.label61);
            this.flowLayoutPanel41.Controls.Add(this.nudTrigGain);
            this.flowLayoutPanel41.Location = new System.Drawing.Point(384, 71);
            this.flowLayoutPanel41.Name = "flowLayoutPanel41";
            this.flowLayoutPanel41.Size = new System.Drawing.Size(115, 28);
            this.flowLayoutPanel41.TabIndex = 8;
            // 
            // label61
            // 
            this.label61.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label61.AutoSize = true;
            this.label61.Location = new System.Drawing.Point(0, 6);
            this.label61.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label61.Name = "label61";
            this.label61.Size = new System.Drawing.Size(58, 17);
            this.label61.TabIndex = 0;
            this.label61.Text = "trigGain";
            // 
            // nudTrigGain
            // 
            this.nudTrigGain.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.nudTrigGain.Location = new System.Drawing.Point(61, 3);
            this.nudTrigGain.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudTrigGain.Name = "nudTrigGain";
            this.nudTrigGain.Size = new System.Drawing.Size(51, 22);
            this.nudTrigGain.TabIndex = 1;
            // 
            // flowLayoutPanel10
            // 
            this.flowLayoutPanel10.Controls.Add(this.flowLayoutPanel18);
            this.flowLayoutPanel10.Controls.Add(this.flowLayoutPanel23);
            this.flowLayoutPanel10.Controls.Add(this.flowLayoutPanel59);
            this.flowLayoutPanel10.Controls.Add(this.flowLayoutPanel60);
            this.flowLayoutPanel10.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel10.Location = new System.Drawing.Point(394, 212);
            this.flowLayoutPanel10.Name = "flowLayoutPanel10";
            this.flowLayoutPanel10.Size = new System.Drawing.Size(179, 140);
            this.flowLayoutPanel10.TabIndex = 5;
            // 
            // flowLayoutPanel18
            // 
            this.flowLayoutPanel18.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel18.AutoSize = true;
            this.flowLayoutPanel18.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel18.Controls.Add(this.label38);
            this.flowLayoutPanel18.Controls.Add(this.nudBcFunction);
            this.flowLayoutPanel18.Location = new System.Drawing.Point(12, 3);
            this.flowLayoutPanel18.Name = "flowLayoutPanel18";
            this.flowLayoutPanel18.Size = new System.Drawing.Size(134, 28);
            this.flowLayoutPanel18.TabIndex = 1;
            // 
            // label38
            // 
            this.label38.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(0, 6);
            this.label38.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(77, 17);
            this.label38.TabIndex = 0;
            this.label38.Text = "bcFunction";
            // 
            // nudBcFunction
            // 
            this.nudBcFunction.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.nudBcFunction.Location = new System.Drawing.Point(80, 3);
            this.nudBcFunction.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudBcFunction.Name = "nudBcFunction";
            this.nudBcFunction.Size = new System.Drawing.Size(51, 22);
            this.nudBcFunction.TabIndex = 1;
            this.nudBcFunction.ValueChanged += new System.EventHandler(this.nud_ValueChanged);
            // 
            // flowLayoutPanel23
            // 
            this.flowLayoutPanel23.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel23.AutoSize = true;
            this.flowLayoutPanel23.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel23.Controls.Add(this.label42);
            this.flowLayoutPanel23.Controls.Add(this.nudBcPolarity);
            this.flowLayoutPanel23.Location = new System.Drawing.Point(19, 37);
            this.flowLayoutPanel23.Name = "flowLayoutPanel23";
            this.flowLayoutPanel23.Size = new System.Drawing.Size(127, 28);
            this.flowLayoutPanel23.TabIndex = 2;
            // 
            // label42
            // 
            this.label42.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label42.AutoSize = true;
            this.label42.Location = new System.Drawing.Point(0, 6);
            this.label42.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(70, 17);
            this.label42.TabIndex = 0;
            this.label42.Text = "bcPolarity";
            // 
            // nudBcPolarity
            // 
            this.nudBcPolarity.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.nudBcPolarity.Location = new System.Drawing.Point(73, 3);
            this.nudBcPolarity.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudBcPolarity.Name = "nudBcPolarity";
            this.nudBcPolarity.Size = new System.Drawing.Size(51, 22);
            this.nudBcPolarity.TabIndex = 1;
            this.nudBcPolarity.ValueChanged += new System.EventHandler(this.nud_ValueChanged);
            // 
            // flowLayoutPanel59
            // 
            this.flowLayoutPanel59.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel59.AutoSize = true;
            this.flowLayoutPanel59.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel59.Controls.Add(this.label57);
            this.flowLayoutPanel59.Controls.Add(this.nudBcLowLevel);
            this.flowLayoutPanel59.Location = new System.Drawing.Point(7, 71);
            this.flowLayoutPanel59.Name = "flowLayoutPanel59";
            this.flowLayoutPanel59.Size = new System.Drawing.Size(139, 28);
            this.flowLayoutPanel59.TabIndex = 3;
            // 
            // label57
            // 
            this.label57.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label57.AutoSize = true;
            this.label57.Location = new System.Drawing.Point(0, 6);
            this.label57.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(82, 17);
            this.label57.TabIndex = 0;
            this.label57.Text = "bcLowLevel";
            // 
            // nudBcLowLevel
            // 
            this.nudBcLowLevel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.nudBcLowLevel.Location = new System.Drawing.Point(85, 3);
            this.nudBcLowLevel.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudBcLowLevel.Name = "nudBcLowLevel";
            this.nudBcLowLevel.Size = new System.Drawing.Size(51, 22);
            this.nudBcLowLevel.TabIndex = 1;
            this.nudBcLowLevel.ValueChanged += new System.EventHandler(this.nud_ValueChanged);
            // 
            // flowLayoutPanel60
            // 
            this.flowLayoutPanel60.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel60.AutoSize = true;
            this.flowLayoutPanel60.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel60.Controls.Add(this.label63);
            this.flowLayoutPanel60.Controls.Add(this.nudBcHighLevel);
            this.flowLayoutPanel10.SetFlowBreak(this.flowLayoutPanel60, true);
            this.flowLayoutPanel60.Location = new System.Drawing.Point(3, 105);
            this.flowLayoutPanel60.Name = "flowLayoutPanel60";
            this.flowLayoutPanel60.Size = new System.Drawing.Size(143, 28);
            this.flowLayoutPanel60.TabIndex = 4;
            // 
            // label63
            // 
            this.label63.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label63.AutoSize = true;
            this.label63.Location = new System.Drawing.Point(0, 6);
            this.label63.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label63.Name = "label63";
            this.label63.Size = new System.Drawing.Size(86, 17);
            this.label63.TabIndex = 0;
            this.label63.Text = "bcHighLevel";
            // 
            // nudBcHighLevel
            // 
            this.nudBcHighLevel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.nudBcHighLevel.Location = new System.Drawing.Point(89, 3);
            this.nudBcHighLevel.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudBcHighLevel.Name = "nudBcHighLevel";
            this.nudBcHighLevel.Size = new System.Drawing.Size(51, 22);
            this.nudBcHighLevel.TabIndex = 1;
            this.nudBcHighLevel.ValueChanged += new System.EventHandler(this.nud_ValueChanged);
            // 
            // flowLayoutPanel55
            // 
            this.flowLayoutPanel55.Controls.Add(this.flowLayoutPanel34);
            this.flowLayoutPanel55.Controls.Add(this.flowLayoutPanel35);
            this.flowLayoutPanel55.Controls.Add(this.flowLayoutPanel36);
            this.flowLayoutPanel55.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel55.Location = new System.Drawing.Point(193, 211);
            this.flowLayoutPanel55.Name = "flowLayoutPanel55";
            this.flowLayoutPanel55.Size = new System.Drawing.Size(193, 142);
            this.flowLayoutPanel55.TabIndex = 4;
            // 
            // flowLayoutPanel34
            // 
            this.flowLayoutPanel34.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel34.AutoSize = true;
            this.flowLayoutPanel34.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel34.Controls.Add(this.label49);
            this.flowLayoutPanel34.Controls.Add(this.nudMotifNumber);
            this.flowLayoutPanel34.Location = new System.Drawing.Point(32, 3);
            this.flowLayoutPanel34.Name = "flowLayoutPanel34";
            this.flowLayoutPanel34.Size = new System.Drawing.Size(145, 28);
            this.flowLayoutPanel34.TabIndex = 1;
            // 
            // label49
            // 
            this.label49.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label49.AutoSize = true;
            this.label49.Location = new System.Drawing.Point(0, 6);
            this.label49.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(88, 17);
            this.label49.TabIndex = 0;
            this.label49.Text = "motifNumber";
            // 
            // nudMotifNumber
            // 
            this.nudMotifNumber.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.nudMotifNumber.Location = new System.Drawing.Point(91, 3);
            this.nudMotifNumber.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudMotifNumber.Name = "nudMotifNumber";
            this.nudMotifNumber.Size = new System.Drawing.Size(51, 22);
            this.nudMotifNumber.TabIndex = 1;
            this.nudMotifNumber.ValueChanged += new System.EventHandler(this.nud_ValueChanged);
            // 
            // flowLayoutPanel35
            // 
            this.flowLayoutPanel35.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel35.AutoSize = true;
            this.flowLayoutPanel35.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel35.Controls.Add(this.label54);
            this.flowLayoutPanel35.Controls.Add(this.nudMotifNumberPerc);
            this.flowLayoutPanel35.Location = new System.Drawing.Point(3, 37);
            this.flowLayoutPanel35.Name = "flowLayoutPanel35";
            this.flowLayoutPanel35.Size = new System.Drawing.Size(174, 28);
            this.flowLayoutPanel35.TabIndex = 2;
            // 
            // label54
            // 
            this.label54.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label54.AutoSize = true;
            this.label54.Location = new System.Drawing.Point(0, 6);
            this.label54.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(117, 17);
            this.label54.TabIndex = 0;
            this.label54.Text = "motifNumberPerc";
            // 
            // nudMotifNumberPerc
            // 
            this.nudMotifNumberPerc.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.nudMotifNumberPerc.Location = new System.Drawing.Point(120, 3);
            this.nudMotifNumberPerc.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudMotifNumberPerc.Name = "nudMotifNumberPerc";
            this.nudMotifNumberPerc.Size = new System.Drawing.Size(51, 22);
            this.nudMotifNumberPerc.TabIndex = 1;
            this.nudMotifNumberPerc.ValueChanged += new System.EventHandler(this.nud_ValueChanged);
            // 
            // flowLayoutPanel36
            // 
            this.flowLayoutPanel36.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel36.AutoSize = true;
            this.flowLayoutPanel36.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel36.Controls.Add(this.label59);
            this.flowLayoutPanel36.Controls.Add(this.nudMotifNumberMel);
            this.flowLayoutPanel36.Location = new System.Drawing.Point(10, 71);
            this.flowLayoutPanel36.Name = "flowLayoutPanel36";
            this.flowLayoutPanel36.Size = new System.Drawing.Size(167, 28);
            this.flowLayoutPanel36.TabIndex = 3;
            // 
            // label59
            // 
            this.label59.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label59.AutoSize = true;
            this.label59.Location = new System.Drawing.Point(0, 6);
            this.label59.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(110, 17);
            this.label59.TabIndex = 0;
            this.label59.Text = "motifNumberMel";
            // 
            // nudMotifNumberMel
            // 
            this.nudMotifNumberMel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.nudMotifNumberMel.Location = new System.Drawing.Point(113, 3);
            this.nudMotifNumberMel.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudMotifNumberMel.Name = "nudMotifNumberMel";
            this.nudMotifNumberMel.Size = new System.Drawing.Size(51, 22);
            this.nudMotifNumberMel.TabIndex = 1;
            this.nudMotifNumberMel.ValueChanged += new System.EventHandler(this.nud_ValueChanged);
            // 
            // flowLayoutPanel54
            // 
            this.flowLayoutPanel54.Controls.Add(this.flowLayoutPanel31);
            this.flowLayoutPanel54.Controls.Add(this.flowLayoutPanel33);
            this.flowLayoutPanel54.Controls.Add(this.flowLayoutPanel32);
            this.flowLayoutPanel54.Controls.Add(this.flowLayoutPanel51);
            this.flowLayoutPanel54.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel54.Location = new System.Drawing.Point(6, 211);
            this.flowLayoutPanel54.Name = "flowLayoutPanel54";
            this.flowLayoutPanel54.Size = new System.Drawing.Size(181, 142);
            this.flowLayoutPanel54.TabIndex = 3;
            // 
            // flowLayoutPanel31
            // 
            this.flowLayoutPanel31.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel31.AutoSize = true;
            this.flowLayoutPanel31.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel31.Controls.Add(this.label51);
            this.flowLayoutPanel31.Controls.Add(this.nudKitNumber);
            this.flowLayoutPanel31.Location = new System.Drawing.Point(40, 3);
            this.flowLayoutPanel31.Name = "flowLayoutPanel31";
            this.flowLayoutPanel31.Size = new System.Drawing.Size(129, 28);
            this.flowLayoutPanel31.TabIndex = 1;
            // 
            // label51
            // 
            this.label51.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label51.AutoSize = true;
            this.label51.Location = new System.Drawing.Point(0, 6);
            this.label51.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(72, 17);
            this.label51.TabIndex = 0;
            this.label51.Text = "kitNumber";
            // 
            // nudKitNumber
            // 
            this.nudKitNumber.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.nudKitNumber.Location = new System.Drawing.Point(75, 3);
            this.nudKitNumber.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudKitNumber.Name = "nudKitNumber";
            this.nudKitNumber.Size = new System.Drawing.Size(51, 22);
            this.nudKitNumber.TabIndex = 1;
            this.nudKitNumber.ValueChanged += new System.EventHandler(this.nud_ValueChanged);
            // 
            // flowLayoutPanel33
            // 
            this.flowLayoutPanel33.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel33.AutoSize = true;
            this.flowLayoutPanel33.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel33.Controls.Add(this.label53);
            this.flowLayoutPanel33.Controls.Add(this.nudKitNumberDemo);
            this.flowLayoutPanel33.Location = new System.Drawing.Point(3, 37);
            this.flowLayoutPanel33.Name = "flowLayoutPanel33";
            this.flowLayoutPanel33.Size = new System.Drawing.Size(166, 28);
            this.flowLayoutPanel33.TabIndex = 2;
            // 
            // label53
            // 
            this.label53.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label53.AutoSize = true;
            this.label53.Location = new System.Drawing.Point(0, 6);
            this.label53.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(109, 17);
            this.label53.TabIndex = 0;
            this.label53.Text = "kitNumberDemo";
            // 
            // nudKitNumberDemo
            // 
            this.nudKitNumberDemo.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.nudKitNumberDemo.Location = new System.Drawing.Point(112, 3);
            this.nudKitNumberDemo.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudKitNumberDemo.Name = "nudKitNumberDemo";
            this.nudKitNumberDemo.Size = new System.Drawing.Size(51, 22);
            this.nudKitNumberDemo.TabIndex = 1;
            this.nudKitNumberDemo.ValueChanged += new System.EventHandler(this.nud_ValueChanged);
            // 
            // flowLayoutPanel32
            // 
            this.flowLayoutPanel32.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel32.AutoSize = true;
            this.flowLayoutPanel32.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel32.Controls.Add(this.label52);
            this.flowLayoutPanel32.Controls.Add(this.nudKitNumberUser);
            this.flowLayoutPanel32.Location = new System.Drawing.Point(10, 71);
            this.flowLayoutPanel32.Name = "flowLayoutPanel32";
            this.flowLayoutPanel32.Size = new System.Drawing.Size(159, 28);
            this.flowLayoutPanel32.TabIndex = 3;
            // 
            // label52
            // 
            this.label52.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label52.AutoSize = true;
            this.label52.Location = new System.Drawing.Point(0, 6);
            this.label52.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(102, 17);
            this.label52.TabIndex = 0;
            this.label52.Text = "kitNumberUser";
            // 
            // nudKitNumberUser
            // 
            this.nudKitNumberUser.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.nudKitNumberUser.Location = new System.Drawing.Point(105, 3);
            this.nudKitNumberUser.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudKitNumberUser.Name = "nudKitNumberUser";
            this.nudKitNumberUser.Size = new System.Drawing.Size(51, 22);
            this.nudKitNumberUser.TabIndex = 1;
            this.nudKitNumberUser.ValueChanged += new System.EventHandler(this.nud_ValueChanged);
            // 
            // flowLayoutPanel51
            // 
            this.flowLayoutPanel51.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel51.AutoSize = true;
            this.flowLayoutPanel51.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel51.Controls.Add(this.label72);
            this.flowLayoutPanel51.Controls.Add(this.nudKitNumberKAT);
            this.flowLayoutPanel51.Location = new System.Drawing.Point(13, 105);
            this.flowLayoutPanel51.Name = "flowLayoutPanel51";
            this.flowLayoutPanel51.Size = new System.Drawing.Size(156, 28);
            this.flowLayoutPanel51.TabIndex = 4;
            // 
            // label72
            // 
            this.label72.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label72.AutoSize = true;
            this.label72.Location = new System.Drawing.Point(0, 6);
            this.label72.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label72.Name = "label72";
            this.label72.Size = new System.Drawing.Size(99, 17);
            this.label72.TabIndex = 0;
            this.label72.Text = "kitNumberKAT";
            // 
            // nudKitNumberKAT
            // 
            this.nudKitNumberKAT.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.nudKitNumberKAT.Location = new System.Drawing.Point(102, 3);
            this.nudKitNumberKAT.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudKitNumberKAT.Name = "nudKitNumberKAT";
            this.nudKitNumberKAT.Size = new System.Drawing.Size(51, 22);
            this.nudKitNumberKAT.TabIndex = 1;
            this.nudKitNumberKAT.ValueChanged += new System.EventHandler(this.nud_ValueChanged);
            // 
            // flowLayoutPanel53
            // 
            this.flowLayoutPanel53.Controls.Add(this.flowLayoutPanel46);
            this.flowLayoutPanel53.Controls.Add(this.flowLayoutPanel45);
            this.flowLayoutPanel53.Controls.Add(this.flowLayoutPanel50);
            this.flowLayoutPanel53.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel53.Location = new System.Drawing.Point(395, 26);
            this.flowLayoutPanel53.Name = "flowLayoutPanel53";
            this.flowLayoutPanel53.Size = new System.Drawing.Size(179, 179);
            this.flowLayoutPanel53.TabIndex = 2;
            // 
            // flowLayoutPanel46
            // 
            this.flowLayoutPanel46.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel46.AutoSize = true;
            this.flowLayoutPanel46.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel46.Controls.Add(this.label64);
            this.flowLayoutPanel46.Controls.Add(this.nudGrooveStatus);
            this.flowLayoutPanel46.Location = new System.Drawing.Point(11, 3);
            this.flowLayoutPanel46.Name = "flowLayoutPanel46";
            this.flowLayoutPanel46.Size = new System.Drawing.Size(149, 28);
            this.flowLayoutPanel46.TabIndex = 1;
            // 
            // label64
            // 
            this.label64.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label64.AutoSize = true;
            this.label64.Location = new System.Drawing.Point(0, 6);
            this.label64.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label64.Name = "label64";
            this.label64.Size = new System.Drawing.Size(92, 17);
            this.label64.TabIndex = 0;
            this.label64.Text = "grooveStatus";
            // 
            // nudGrooveStatus
            // 
            this.nudGrooveStatus.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.nudGrooveStatus.Location = new System.Drawing.Point(95, 3);
            this.nudGrooveStatus.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudGrooveStatus.Name = "nudGrooveStatus";
            this.nudGrooveStatus.Size = new System.Drawing.Size(51, 22);
            this.nudGrooveStatus.TabIndex = 1;
            this.nudGrooveStatus.ValueChanged += new System.EventHandler(this.nud_ValueChanged);
            // 
            // flowLayoutPanel45
            // 
            this.flowLayoutPanel45.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel45.AutoSize = true;
            this.flowLayoutPanel45.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel45.Controls.Add(this.label68);
            this.flowLayoutPanel45.Controls.Add(this.nudGrooveVol);
            this.flowLayoutPanel45.Location = new System.Drawing.Point(31, 37);
            this.flowLayoutPanel45.Name = "flowLayoutPanel45";
            this.flowLayoutPanel45.Size = new System.Drawing.Size(129, 28);
            this.flowLayoutPanel45.TabIndex = 2;
            // 
            // label68
            // 
            this.label68.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label68.AutoSize = true;
            this.label68.Location = new System.Drawing.Point(0, 6);
            this.label68.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label68.Name = "label68";
            this.label68.Size = new System.Drawing.Size(72, 17);
            this.label68.TabIndex = 0;
            this.label68.Text = "grooveVol";
            // 
            // nudGrooveVol
            // 
            this.nudGrooveVol.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.nudGrooveVol.Location = new System.Drawing.Point(75, 3);
            this.nudGrooveVol.Maximum = new decimal(new int[] {
            127,
            0,
            0,
            0});
            this.nudGrooveVol.Name = "nudGrooveVol";
            this.nudGrooveVol.Size = new System.Drawing.Size(51, 22);
            this.nudGrooveVol.TabIndex = 1;
            this.nudGrooveVol.ValueChanged += new System.EventHandler(this.nud_ValueChanged);
            // 
            // flowLayoutPanel50
            // 
            this.flowLayoutPanel50.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel50.AutoSize = true;
            this.flowLayoutPanel50.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel50.Controls.Add(this.label70);
            this.flowLayoutPanel50.Controls.Add(this.grooveAutoOff);
            this.flowLayoutPanel50.Location = new System.Drawing.Point(3, 71);
            this.flowLayoutPanel50.Name = "flowLayoutPanel50";
            this.flowLayoutPanel50.Size = new System.Drawing.Size(157, 28);
            this.flowLayoutPanel50.TabIndex = 3;
            // 
            // label70
            // 
            this.label70.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label70.AutoSize = true;
            this.label70.Location = new System.Drawing.Point(0, 6);
            this.label70.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label70.Name = "label70";
            this.label70.Size = new System.Drawing.Size(100, 17);
            this.label70.TabIndex = 0;
            this.label70.Text = "grooveAutoOff";
            // 
            // grooveAutoOff
            // 
            this.grooveAutoOff.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.grooveAutoOff.Location = new System.Drawing.Point(103, 3);
            this.grooveAutoOff.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.grooveAutoOff.Name = "grooveAutoOff";
            this.grooveAutoOff.Size = new System.Drawing.Size(51, 22);
            this.grooveAutoOff.TabIndex = 1;
            this.grooveAutoOff.ValueChanged += new System.EventHandler(this.nud_ValueChanged);
            // 
            // flowLayoutPanel52
            // 
            this.flowLayoutPanel52.Controls.Add(this.flowLayoutPanel17);
            this.flowLayoutPanel52.Controls.Add(this.flowLayoutPanel20);
            this.flowLayoutPanel52.Controls.Add(this.flowLayoutPanel30);
            this.flowLayoutPanel52.Controls.Add(this.flowLayoutPanel37);
            this.flowLayoutPanel52.Controls.Add(this.flowLayoutPanel42);
            this.flowLayoutPanel52.Controls.Add(this.flowLayoutPanel43);
            this.flowLayoutPanel52.Controls.Add(this.flowLayoutPanel44);
            this.flowLayoutPanel52.Controls.Add(this.flowLayoutPanel48);
            this.flowLayoutPanel52.Controls.Add(this.flowLayoutPanel16);
            this.flowLayoutPanel52.Controls.Add(this.flowLayoutPanel19);
            this.flowLayoutPanel52.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel52.Location = new System.Drawing.Point(6, 26);
            this.flowLayoutPanel52.Name = "flowLayoutPanel52";
            this.flowLayoutPanel52.Size = new System.Drawing.Size(380, 179);
            this.flowLayoutPanel52.TabIndex = 1;
            // 
            // flowLayoutPanel17
            // 
            this.flowLayoutPanel17.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel17.AutoSize = true;
            this.flowLayoutPanel17.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel17.Controls.Add(this.label37);
            this.flowLayoutPanel17.Controls.Add(this.nudBeeperStatus);
            this.flowLayoutPanel17.Location = new System.Drawing.Point(23, 3);
            this.flowLayoutPanel17.Name = "flowLayoutPanel17";
            this.flowLayoutPanel17.Size = new System.Drawing.Size(150, 28);
            this.flowLayoutPanel17.TabIndex = 1;
            // 
            // label37
            // 
            this.label37.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(0, 6);
            this.label37.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(93, 17);
            this.label37.TabIndex = 0;
            this.label37.Text = "beeperStatus";
            // 
            // nudBeeperStatus
            // 
            this.nudBeeperStatus.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.nudBeeperStatus.Location = new System.Drawing.Point(96, 3);
            this.nudBeeperStatus.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudBeeperStatus.Name = "nudBeeperStatus";
            this.nudBeeperStatus.Size = new System.Drawing.Size(51, 22);
            this.nudBeeperStatus.TabIndex = 1;
            this.nudBeeperStatus.ValueChanged += new System.EventHandler(this.nud_ValueChanged);
            // 
            // flowLayoutPanel20
            // 
            this.flowLayoutPanel20.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel20.AutoSize = true;
            this.flowLayoutPanel20.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel20.Controls.Add(this.label40);
            this.flowLayoutPanel20.Controls.Add(this.nudChokeFunction);
            this.flowLayoutPanel20.Location = new System.Drawing.Point(16, 37);
            this.flowLayoutPanel20.Name = "flowLayoutPanel20";
            this.flowLayoutPanel20.Size = new System.Drawing.Size(157, 28);
            this.flowLayoutPanel20.TabIndex = 2;
            // 
            // label40
            // 
            this.label40.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label40.AutoSize = true;
            this.label40.Location = new System.Drawing.Point(0, 6);
            this.label40.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(100, 17);
            this.label40.TabIndex = 0;
            this.label40.Text = "chokeFunction";
            // 
            // nudChokeFunction
            // 
            this.nudChokeFunction.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.nudChokeFunction.Location = new System.Drawing.Point(103, 3);
            this.nudChokeFunction.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudChokeFunction.Name = "nudChokeFunction";
            this.nudChokeFunction.Size = new System.Drawing.Size(51, 22);
            this.nudChokeFunction.TabIndex = 1;
            this.nudChokeFunction.ValueChanged += new System.EventHandler(this.nud_ValueChanged);
            // 
            // flowLayoutPanel30
            // 
            this.flowLayoutPanel30.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel30.AutoSize = true;
            this.flowLayoutPanel30.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel30.Controls.Add(this.label45);
            this.flowLayoutPanel30.Controls.Add(this.nudInstrumentID);
            this.flowLayoutPanel30.Location = new System.Drawing.Point(29, 71);
            this.flowLayoutPanel30.Name = "flowLayoutPanel30";
            this.flowLayoutPanel30.Size = new System.Drawing.Size(144, 28);
            this.flowLayoutPanel30.TabIndex = 3;
            // 
            // label45
            // 
            this.label45.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label45.AutoSize = true;
            this.label45.Location = new System.Drawing.Point(0, 6);
            this.label45.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(87, 17);
            this.label45.TabIndex = 0;
            this.label45.Text = "instrumentID";
            // 
            // nudInstrumentID
            // 
            this.nudInstrumentID.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.nudInstrumentID.Location = new System.Drawing.Point(90, 3);
            this.nudInstrumentID.Maximum = new decimal(new int[] {
            127,
            0,
            0,
            0});
            this.nudInstrumentID.Name = "nudInstrumentID";
            this.nudInstrumentID.Size = new System.Drawing.Size(51, 22);
            this.nudInstrumentID.TabIndex = 1;
            this.nudInstrumentID.ValueChanged += new System.EventHandler(this.nud_ValueChanged);
            // 
            // flowLayoutPanel37
            // 
            this.flowLayoutPanel37.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel37.AutoSize = true;
            this.flowLayoutPanel37.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel37.Controls.Add(this.label55);
            this.flowLayoutPanel37.Controls.Add(this.nudMidiMergeStatus);
            this.flowLayoutPanel37.Location = new System.Drawing.Point(3, 105);
            this.flowLayoutPanel37.Name = "flowLayoutPanel37";
            this.flowLayoutPanel37.Size = new System.Drawing.Size(170, 28);
            this.flowLayoutPanel37.TabIndex = 4;
            // 
            // label55
            // 
            this.label55.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label55.AutoSize = true;
            this.label55.Location = new System.Drawing.Point(0, 6);
            this.label55.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(113, 17);
            this.label55.TabIndex = 0;
            this.label55.Text = "midiMergeStatus";
            // 
            // nudMidiMergeStatus
            // 
            this.nudMidiMergeStatus.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.nudMidiMergeStatus.Location = new System.Drawing.Point(116, 3);
            this.nudMidiMergeStatus.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudMidiMergeStatus.Name = "nudMidiMergeStatus";
            this.nudMidiMergeStatus.Size = new System.Drawing.Size(51, 22);
            this.nudMidiMergeStatus.TabIndex = 1;
            this.nudMidiMergeStatus.ValueChanged += new System.EventHandler(this.nud_ValueChanged);
            // 
            // flowLayoutPanel42
            // 
            this.flowLayoutPanel42.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel42.AutoSize = true;
            this.flowLayoutPanel42.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel42.Controls.Add(this.label62);
            this.flowLayoutPanel42.Controls.Add(this.cbPrgChgRcvChn);
            this.flowLayoutPanel42.Location = new System.Drawing.Point(13, 139);
            this.flowLayoutPanel42.Name = "flowLayoutPanel42";
            this.flowLayoutPanel42.Size = new System.Drawing.Size(160, 30);
            this.flowLayoutPanel42.TabIndex = 5;
            // 
            // label62
            // 
            this.label62.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label62.AutoSize = true;
            this.label62.Location = new System.Drawing.Point(0, 6);
            this.label62.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label62.Name = "label62";
            this.label62.Size = new System.Drawing.Size(103, 17);
            this.label62.TabIndex = 0;
            this.label62.Text = "prgChgRcvChn";
            // 
            // cbPrgChgRcvChn
            // 
            this.cbPrgChgRcvChn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPrgChgRcvChn.FormattingEnabled = true;
            this.cbPrgChgRcvChn.Items.AddRange(new object[] {
            "1",
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
            "All",
            "Off"});
            this.cbPrgChgRcvChn.Location = new System.Drawing.Point(106, 3);
            this.cbPrgChgRcvChn.MaxDropDownItems = 18;
            this.cbPrgChgRcvChn.Name = "cbPrgChgRcvChn";
            this.cbPrgChgRcvChn.Size = new System.Drawing.Size(51, 24);
            this.cbPrgChgRcvChn.TabIndex = 1;
            this.cbPrgChgRcvChn.SelectedIndexChanged += new System.EventHandler(this.cbPrgChgRcvChn_SelectedIndexChanged);
            // 
            // flowLayoutPanel43
            // 
            this.flowLayoutPanel43.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel43.AutoSize = true;
            this.flowLayoutPanel43.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel43.Controls.Add(this.label58);
            this.flowLayoutPanel43.Controls.Add(this.nudDisplayAngle);
            this.flowLayoutPanel43.Location = new System.Drawing.Point(211, 3);
            this.flowLayoutPanel43.Name = "flowLayoutPanel43";
            this.flowLayoutPanel43.Size = new System.Drawing.Size(145, 28);
            this.flowLayoutPanel43.TabIndex = 6;
            // 
            // label58
            // 
            this.label58.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label58.AutoSize = true;
            this.label58.Location = new System.Drawing.Point(0, 6);
            this.label58.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(88, 17);
            this.label58.TabIndex = 0;
            this.label58.Text = "displayAngle";
            // 
            // nudDisplayAngle
            // 
            this.nudDisplayAngle.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.nudDisplayAngle.Location = new System.Drawing.Point(91, 3);
            this.nudDisplayAngle.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudDisplayAngle.Name = "nudDisplayAngle";
            this.nudDisplayAngle.Size = new System.Drawing.Size(51, 22);
            this.nudDisplayAngle.TabIndex = 1;
            this.nudDisplayAngle.ValueChanged += new System.EventHandler(this.nud_ValueChanged);
            // 
            // flowLayoutPanel44
            // 
            this.flowLayoutPanel44.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel44.AutoSize = true;
            this.flowLayoutPanel44.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel44.Controls.Add(this.label60);
            this.flowLayoutPanel44.Controls.Add(this.nudPlayMode);
            this.flowLayoutPanel44.Location = new System.Drawing.Point(230, 37);
            this.flowLayoutPanel44.Name = "flowLayoutPanel44";
            this.flowLayoutPanel44.Size = new System.Drawing.Size(126, 28);
            this.flowLayoutPanel44.TabIndex = 7;
            // 
            // label60
            // 
            this.label60.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label60.AutoSize = true;
            this.label60.Location = new System.Drawing.Point(0, 6);
            this.label60.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label60.Name = "label60";
            this.label60.Size = new System.Drawing.Size(69, 17);
            this.label60.TabIndex = 0;
            this.label60.Text = "playMode";
            // 
            // nudPlayMode
            // 
            this.nudPlayMode.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.nudPlayMode.Location = new System.Drawing.Point(72, 3);
            this.nudPlayMode.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudPlayMode.Name = "nudPlayMode";
            this.nudPlayMode.Size = new System.Drawing.Size(51, 22);
            this.nudPlayMode.TabIndex = 1;
            this.nudPlayMode.ValueChanged += new System.EventHandler(this.nud_ValueChanged);
            // 
            // flowLayoutPanel48
            // 
            this.flowLayoutPanel48.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel48.AutoSize = true;
            this.flowLayoutPanel48.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel48.Controls.Add(this.label141);
            this.flowLayoutPanel48.Controls.Add(this.nudNoteNamesStatus);
            this.flowLayoutPanel48.Location = new System.Drawing.Point(179, 71);
            this.flowLayoutPanel48.Name = "flowLayoutPanel48";
            this.flowLayoutPanel48.Size = new System.Drawing.Size(177, 28);
            this.flowLayoutPanel48.TabIndex = 8;
            // 
            // label141
            // 
            this.label141.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label141.AutoSize = true;
            this.label141.Location = new System.Drawing.Point(0, 6);
            this.label141.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label141.Name = "label141";
            this.label141.Size = new System.Drawing.Size(120, 17);
            this.label141.TabIndex = 0;
            this.label141.Text = "noteNamesStatus";
            // 
            // nudNoteNamesStatus
            // 
            this.nudNoteNamesStatus.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.nudNoteNamesStatus.Location = new System.Drawing.Point(123, 3);
            this.nudNoteNamesStatus.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudNoteNamesStatus.Name = "nudNoteNamesStatus";
            this.nudNoteNamesStatus.Size = new System.Drawing.Size(51, 22);
            this.nudNoteNamesStatus.TabIndex = 1;
            this.nudNoteNamesStatus.ValueChanged += new System.EventHandler(this.nud_ValueChanged);
            // 
            // flowLayoutPanel16
            // 
            this.flowLayoutPanel16.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel16.AutoSize = true;
            this.flowLayoutPanel16.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel16.Controls.Add(this.label67);
            this.flowLayoutPanel16.Controls.Add(this.nudTTMeter);
            this.flowLayoutPanel16.Location = new System.Drawing.Point(247, 105);
            this.flowLayoutPanel16.Name = "flowLayoutPanel16";
            this.flowLayoutPanel16.Size = new System.Drawing.Size(109, 28);
            this.flowLayoutPanel16.TabIndex = 9;
            // 
            // label67
            // 
            this.label67.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label67.AutoSize = true;
            this.label67.Location = new System.Drawing.Point(0, 6);
            this.label67.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label67.Name = "label67";
            this.label67.Size = new System.Drawing.Size(52, 17);
            this.label67.TabIndex = 0;
            this.label67.Text = "ttMeter";
            // 
            // nudTTMeter
            // 
            this.nudTTMeter.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.nudTTMeter.Location = new System.Drawing.Point(55, 3);
            this.nudTTMeter.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudTTMeter.Name = "nudTTMeter";
            this.nudTTMeter.Size = new System.Drawing.Size(51, 22);
            this.nudTTMeter.TabIndex = 1;
            this.nudTTMeter.ValueChanged += new System.EventHandler(this.nud_ValueChanged);
            // 
            // flowLayoutPanel19
            // 
            this.flowLayoutPanel19.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel19.AutoSize = true;
            this.flowLayoutPanel19.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel19.Controls.Add(this.label71);
            this.flowLayoutPanel19.Controls.Add(this.nudHearSoundStatus);
            this.flowLayoutPanel19.Location = new System.Drawing.Point(181, 139);
            this.flowLayoutPanel19.Name = "flowLayoutPanel19";
            this.flowLayoutPanel19.Size = new System.Drawing.Size(175, 28);
            this.flowLayoutPanel19.TabIndex = 10;
            // 
            // label71
            // 
            this.label71.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label71.AutoSize = true;
            this.label71.Location = new System.Drawing.Point(0, 6);
            this.label71.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label71.Name = "label71";
            this.label71.Size = new System.Drawing.Size(118, 17);
            this.label71.TabIndex = 0;
            this.label71.Text = "hearSoundStatus";
            // 
            // nudHearSoundStatus
            // 
            this.nudHearSoundStatus.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.nudHearSoundStatus.Location = new System.Drawing.Point(121, 3);
            this.nudHearSoundStatus.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudHearSoundStatus.Name = "nudHearSoundStatus";
            this.nudHearSoundStatus.Size = new System.Drawing.Size(51, 22);
            this.nudHearSoundStatus.TabIndex = 1;
            this.nudHearSoundStatus.ValueChanged += new System.EventHandler(this.nud_ValueChanged);
            // 
            // label25
            // 
            this.label25.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(80, 706);
            this.label25.Margin = new System.Windows.Forms.Padding(24, 0, 3, 0);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(449, 17);
            this.label25.TabIndex = 1;
            this.label25.Text = "MIDI OX SysEx Transmit: 32 bytes, 512 buffers, 16ms between buffers";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(590, 721);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.msMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.msMain;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TrapKAT SysEx Editor";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.msMain.ResumeLayout(false);
            this.msMain.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.flowLayoutPanel9.ResumeLayout(false);
            this.flowLayoutPanel9.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this.flowLayoutPanel5.ResumeLayout(false);
            this.flowLayoutPanel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFCChannel)).EndInit();
            this.flowLayoutPanel8.ResumeLayout(false);
            this.flowLayoutPanel8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudKitChannel)).EndInit();
            this.flowLayoutPanel7.ResumeLayout(false);
            this.flowLayoutPanel7.PerformLayout();
            this.flowLayoutPanel6.ResumeLayout(false);
            this.flowLayoutPanel6.PerformLayout();
            this.flowLayoutPanel12.ResumeLayout(false);
            this.flowLayoutPanel12.PerformLayout();
            this.flowLayoutPanel15.ResumeLayout(false);
            this.tableLayoutPanel8.ResumeLayout(false);
            this.tableLayoutPanel8.PerformLayout();
            this.flowLayoutPanel13.ResumeLayout(false);
            this.flowLayoutPanel13.PerformLayout();
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrgChgTxmChn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrgChg)).EndInit();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudBankLSB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBankMSB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBank)).EndInit();
            this.flowLayoutPanel11.ResumeLayout(false);
            this.flowLayoutPanel11.PerformLayout();
            this.tlpKitVelocity.ResumeLayout(false);
            this.tlpKitVelocity.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudKitMinVel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudKitMaxVel)).EndInit();
            this.flowLayoutPanel14.ResumeLayout(false);
            this.flowLayoutPanel14.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudVolume)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.flowLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel3.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.flowLayoutPanel4.ResumeLayout(false);
            this.flowLayoutPanel4.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPadChannel)).EndInit();
            this.tlpPadVelocity.ResumeLayout(false);
            this.tlpPadVelocity.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPadMinVel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPadMaxVel)).EndInit();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.flowLayoutPanel26.ResumeLayout(false);
            this.flowLayoutPanel26.PerformLayout();
            this.flowLayoutPanel38.ResumeLayout(false);
            this.flowLayoutPanel38.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFcOpenRegion)).EndInit();
            this.flowLayoutPanel21.ResumeLayout(false);
            this.flowLayoutPanel21.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFcClosedRegion)).EndInit();
            this.flowLayoutPanel22.ResumeLayout(false);
            this.flowLayoutPanel22.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFcPolarity)).EndInit();
            this.flowLayoutPanel28.ResumeLayout(false);
            this.flowLayoutPanel28.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFcVelocityLevel)).EndInit();
            this.flowLayoutPanel61.ResumeLayout(false);
            this.flowLayoutPanel61.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFcLowLevel)).EndInit();
            this.flowLayoutPanel62.ResumeLayout(false);
            this.flowLayoutPanel62.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFcHighLevel)).EndInit();
            this.flowLayoutPanel47.ResumeLayout(false);
            this.flowLayoutPanel47.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFcSplashEase)).EndInit();
            this.flowLayoutPanel29.ResumeLayout(false);
            this.flowLayoutPanel29.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFcWaitModeLevel)).EndInit();
            this.flowLayoutPanel49.ResumeLayout(false);
            this.flowLayoutPanel49.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudHatNoteGate)).EndInit();
            this.flowLayoutPanel25.ResumeLayout(false);
            this.flowLayoutPanel25.PerformLayout();
            this.flowLayoutPanel24.ResumeLayout(false);
            this.flowLayoutPanel24.PerformLayout();
            this.flowLayoutPanel56.ResumeLayout(false);
            this.flowLayoutPanel56.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudLowLevel)).EndInit();
            this.flowLayoutPanel57.ResumeLayout(false);
            this.flowLayoutPanel57.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudHighLevel)).EndInit();
            this.flowLayoutPanel27.ResumeLayout(false);
            this.flowLayoutPanel27.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudThresholdManual)).EndInit();
            this.flowLayoutPanel39.ResumeLayout(false);
            this.flowLayoutPanel39.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudThresholdActual)).EndInit();
            this.flowLayoutPanel40.ResumeLayout(false);
            this.flowLayoutPanel40.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudUserMargin)).EndInit();
            this.flowLayoutPanel58.ResumeLayout(false);
            this.flowLayoutPanel58.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudInternalMargin)).EndInit();
            this.flowLayoutPanel41.ResumeLayout(false);
            this.flowLayoutPanel41.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTrigGain)).EndInit();
            this.flowLayoutPanel10.ResumeLayout(false);
            this.flowLayoutPanel10.PerformLayout();
            this.flowLayoutPanel18.ResumeLayout(false);
            this.flowLayoutPanel18.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudBcFunction)).EndInit();
            this.flowLayoutPanel23.ResumeLayout(false);
            this.flowLayoutPanel23.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudBcPolarity)).EndInit();
            this.flowLayoutPanel59.ResumeLayout(false);
            this.flowLayoutPanel59.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudBcLowLevel)).EndInit();
            this.flowLayoutPanel60.ResumeLayout(false);
            this.flowLayoutPanel60.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudBcHighLevel)).EndInit();
            this.flowLayoutPanel55.ResumeLayout(false);
            this.flowLayoutPanel55.PerformLayout();
            this.flowLayoutPanel34.ResumeLayout(false);
            this.flowLayoutPanel34.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMotifNumber)).EndInit();
            this.flowLayoutPanel35.ResumeLayout(false);
            this.flowLayoutPanel35.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMotifNumberPerc)).EndInit();
            this.flowLayoutPanel36.ResumeLayout(false);
            this.flowLayoutPanel36.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMotifNumberMel)).EndInit();
            this.flowLayoutPanel54.ResumeLayout(false);
            this.flowLayoutPanel54.PerformLayout();
            this.flowLayoutPanel31.ResumeLayout(false);
            this.flowLayoutPanel31.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudKitNumber)).EndInit();
            this.flowLayoutPanel33.ResumeLayout(false);
            this.flowLayoutPanel33.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudKitNumberDemo)).EndInit();
            this.flowLayoutPanel32.ResumeLayout(false);
            this.flowLayoutPanel32.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudKitNumberUser)).EndInit();
            this.flowLayoutPanel51.ResumeLayout(false);
            this.flowLayoutPanel51.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudKitNumberKAT)).EndInit();
            this.flowLayoutPanel53.ResumeLayout(false);
            this.flowLayoutPanel53.PerformLayout();
            this.flowLayoutPanel46.ResumeLayout(false);
            this.flowLayoutPanel46.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudGrooveStatus)).EndInit();
            this.flowLayoutPanel45.ResumeLayout(false);
            this.flowLayoutPanel45.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudGrooveVol)).EndInit();
            this.flowLayoutPanel50.ResumeLayout(false);
            this.flowLayoutPanel50.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grooveAutoOff)).EndInit();
            this.flowLayoutPanel52.ResumeLayout(false);
            this.flowLayoutPanel52.PerformLayout();
            this.flowLayoutPanel17.ResumeLayout(false);
            this.flowLayoutPanel17.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudBeeperStatus)).EndInit();
            this.flowLayoutPanel20.ResumeLayout(false);
            this.flowLayoutPanel20.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudChokeFunction)).EndInit();
            this.flowLayoutPanel30.ResumeLayout(false);
            this.flowLayoutPanel30.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudInstrumentID)).EndInit();
            this.flowLayoutPanel37.ResumeLayout(false);
            this.flowLayoutPanel37.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMidiMergeStatus)).EndInit();
            this.flowLayoutPanel42.ResumeLayout(false);
            this.flowLayoutPanel42.PerformLayout();
            this.flowLayoutPanel43.ResumeLayout(false);
            this.flowLayoutPanel43.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDisplayAngle)).EndInit();
            this.flowLayoutPanel44.ResumeLayout(false);
            this.flowLayoutPanel44.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPlayMode)).EndInit();
            this.flowLayoutPanel48.ResumeLayout(false);
            this.flowLayoutPanel48.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudNoteNamesStatus)).EndInit();
            this.flowLayoutPanel16.ResumeLayout(false);
            this.flowLayoutPanel16.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTTMeter)).EndInit();
            this.flowLayoutPanel19.ResumeLayout(false);
            this.flowLayoutPanel19.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudHearSoundStatus)).EndInit();
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
        private System.Windows.Forms.ToolStripMenuItem tsmiFileSaveAllMemory;
        private System.Windows.Forms.ToolStripMenuItem tsmiFileSaveAllMemoryAs;
        private System.Windows.Forms.ToolStripMenuItem tsmiHelpAbout;
        private System.Windows.Forms.ToolStripMenuItem tsmiFileClose;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsmiFileExit;
        private System.Windows.Forms.ToolStripMenuItem tsmiEditUndo;
        private System.Windows.Forms.ToolStripMenuItem tsmiEditRedo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem tsmiEditCopyKit;
        private System.Windows.Forms.ToolStripMenuItem tsmiEditSwapKits;
        private System.Windows.Forms.ToolStripMenuItem tsmiTools;
        private System.Windows.Forms.ToolStripMenuItem tsmiToolsOptions;
        private System.Windows.Forms.SaveFileDialog sfdSaveSysEx;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem tsmiFileSaveGlobalMemory;
        private System.Windows.Forms.ToolStripMenuItem tsmiFileSaveGlobalMemoryAs;
        private System.Windows.Forms.ToolStripMenuItem tsmiFileSaveCurrentKit;
        private System.Windows.Forms.ToolStripMenuItem tsmiFileSaveCurrentKitAs;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem tsmiEditCopyPad;
        private System.Windows.Forms.ToolStripMenuItem tsmiEditSwapPads;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel9;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.ComboBox cbFCCurve;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel5;
        private System.Windows.Forms.NumericUpDown nudFCChannel;
        private System.Windows.Forms.CheckBox ckbAsChick;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.ComboBox cbFCFunction;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel8;
        private System.Windows.Forms.NumericUpDown nudKitChannel;
        private System.Windows.Forms.CheckBox ckbVarChannel;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel7;
        private System.Windows.Forms.ComboBox cbKitGate;
        private System.Windows.Forms.CheckBox ckbVarGate;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel6;
        private System.Windows.Forms.ComboBox cbKitCurve;
        private System.Windows.Forms.CheckBox ckbVarCurve;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbPad;
        private System.Windows.Forms.Label lbPadDataChanged;
        private System.Windows.Forms.Label lbBreak2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cbNote5;
        private System.Windows.Forms.ComboBox cbNote4;
        private System.Windows.Forms.ComboBox cbNote3;
        private System.Windows.Forms.ComboBox cbNote2;
        private System.Windows.Forms.ComboBox cbNote1;
        private System.Windows.Forms.ComboBox cbNote6;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cbPadGate;
        private System.Windows.Forms.ComboBox cbPadCurve;
        private System.Windows.Forms.NumericUpDown nudPadChannel;
        private System.Windows.Forms.TableLayoutPanel tlpPadVelocity;
        private System.Windows.Forms.NumericUpDown nudPadMinVel;
        private System.Windows.Forms.NumericUpDown nudPadMaxVel;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.CheckBox ckbF7;
        private System.Windows.Forms.CheckBox ckbF6;
        private System.Windows.Forms.CheckBox ckbF5;
        private System.Windows.Forms.CheckBox ckbF4;
        private System.Windows.Forms.CheckBox ckbF3;
        private System.Windows.Forms.CheckBox ckbF2;
        private System.Windows.Forms.CheckBox ckbF1;
        private System.Windows.Forms.CheckBox ckbF0;
        private System.Windows.Forms.TextBox tbKitName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel11;
        private System.Windows.Forms.TableLayoutPanel tlpKitVelocity;
        private System.Windows.Forms.CheckBox ckbVarMinVel;
        private System.Windows.Forms.CheckBox ckbVarMaxVel;
        private System.Windows.Forms.NumericUpDown nudKitMinVel;
        private System.Windows.Forms.NumericUpDown nudKitMaxVel;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel14;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.NumericUpDown nudVolume;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel13;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.NumericUpDown nudPrgChgTxmChn;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.CheckBox ckbNoPrgChg;
        private System.Windows.Forms.NumericUpDown nudPrgChg;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.NumericUpDown nudBankLSB;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.NumericUpDown nudBankMSB;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.NumericUpDown nudBank;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel12;
        private System.Windows.Forms.ComboBox cbCurrentKit;
        private System.Windows.Forms.Label lbKitDataChanged;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel15;
        private System.Windows.Forms.ComboBox cbHHPad1;
        private System.Windows.Forms.ComboBox cbHHPad2;
        private System.Windows.Forms.ComboBox cbHHPad3;
        private System.Windows.Forms.ComboBox cbHHPad4;
        private System.Windows.Forms.CheckBox ckbNoVolume;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel52;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel55;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel34;
        private System.Windows.Forms.Label label49;
        private System.Windows.Forms.NumericUpDown nudMotifNumber;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel36;
        private System.Windows.Forms.Label label59;
        private System.Windows.Forms.NumericUpDown nudMotifNumberMel;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel35;
        private System.Windows.Forms.Label label54;
        private System.Windows.Forms.NumericUpDown nudMotifNumberPerc;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel54;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel31;
        private System.Windows.Forms.Label label51;
        private System.Windows.Forms.NumericUpDown nudKitNumber;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel33;
        private System.Windows.Forms.Label label53;
        private System.Windows.Forms.NumericUpDown nudKitNumberDemo;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel32;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.NumericUpDown nudKitNumberUser;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel51;
        private System.Windows.Forms.Label label72;
        private System.Windows.Forms.NumericUpDown nudKitNumberKAT;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel53;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel46;
        private System.Windows.Forms.Label label64;
        private System.Windows.Forms.NumericUpDown nudGrooveStatus;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel45;
        private System.Windows.Forms.Label label68;
        private System.Windows.Forms.NumericUpDown nudGrooveVol;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel50;
        private System.Windows.Forms.Label label70;
        private System.Windows.Forms.NumericUpDown grooveAutoOff;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel17;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.NumericUpDown nudBeeperStatus;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel20;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.NumericUpDown nudChokeFunction;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel30;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.NumericUpDown nudInstrumentID;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel37;
        private System.Windows.Forms.Label label55;
        private System.Windows.Forms.NumericUpDown nudMidiMergeStatus;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel42;
        private System.Windows.Forms.Label label62;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel43;
        private System.Windows.Forms.Label label58;
        private System.Windows.Forms.NumericUpDown nudDisplayAngle;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel44;
        private System.Windows.Forms.Label label60;
        private System.Windows.Forms.NumericUpDown nudPlayMode;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel48;
        private System.Windows.Forms.Label label141;
        private System.Windows.Forms.NumericUpDown nudNoteNamesStatus;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel16;
        private System.Windows.Forms.Label label67;
        private System.Windows.Forms.NumericUpDown nudTTMeter;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel19;
        private System.Windows.Forms.Label label71;
        private System.Windows.Forms.NumericUpDown nudHearSoundStatus;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel10;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel18;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.NumericUpDown nudBcFunction;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel23;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.NumericUpDown nudBcPolarity;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel26;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel38;
        private System.Windows.Forms.Label label56;
        private System.Windows.Forms.NumericUpDown nudFcOpenRegion;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel21;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.NumericUpDown nudFcClosedRegion;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel22;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.NumericUpDown nudFcPolarity;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel25;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel24;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.ComboBox cbPadDynamics;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel56;
        private System.Windows.Forms.Label label73;
        private System.Windows.Forms.NumericUpDown nudLowLevel;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel57;
        private System.Windows.Forms.Label label74;
        private System.Windows.Forms.NumericUpDown nudHighLevel;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel27;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.NumericUpDown nudThresholdManual;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel39;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.NumericUpDown nudThresholdActual;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel40;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.NumericUpDown nudUserMargin;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel58;
        private System.Windows.Forms.Label label50;
        private System.Windows.Forms.NumericUpDown nudInternalMargin;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel28;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.NumericUpDown nudFcVelocityLevel;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel41;
        private System.Windows.Forms.Label label61;
        private System.Windows.Forms.NumericUpDown nudTrigGain;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel59;
        private System.Windows.Forms.Label label57;
        private System.Windows.Forms.NumericUpDown nudBcLowLevel;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel60;
        private System.Windows.Forms.Label label63;
        private System.Windows.Forms.NumericUpDown nudBcHighLevel;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel61;
        private System.Windows.Forms.Label label66;
        private System.Windows.Forms.NumericUpDown nudFcLowLevel;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel62;
        private System.Windows.Forms.Label label75;
        private System.Windows.Forms.NumericUpDown nudFcHighLevel;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel47;
        private System.Windows.Forms.Label label65;
        private System.Windows.Forms.NumericUpDown nudFcSplashEase;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel29;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.NumericUpDown nudFcWaitModeLevel;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel49;
        private System.Windows.Forms.Label label69;
        private System.Windows.Forms.NumericUpDown nudHatNoteGate;
        private System.Windows.Forms.Label lbGlobalDataChanged;
        private System.Windows.Forms.ComboBox cbPrgChgRcvChn;
        private System.Windows.Forms.Label label25;
    }
}

