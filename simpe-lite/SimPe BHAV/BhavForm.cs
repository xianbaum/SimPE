/***************************************************************************
 *   Copyright (C) 2005 by Peter L Jones                                   *
 *   peter@drealm.info                                                     *
 *   Copyright (C) 2005 by Ambertation                                     *
 *   quaxi@ambertation.de                                                  *
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
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using SimPe.Interfaces.Plugin;
using SimPe.PackedFiles.Wrapper;
using pjse;

namespace SimPe.PackedFiles.UserInterface
{
	/// <summary>
	/// Summary description for BhavForm.
	/// </summary>
	public class BhavForm : System.Windows.Forms.Form, IPackedFileUI
	{
		#region Form variables
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Panel pnHeading;
		private System.Windows.Forms.Label lbFilename;
		private System.Windows.Forms.Label lbFormat;
		private System.Windows.Forms.Label lbType;
		private System.Windows.Forms.Label lbLocalC;
		private System.Windows.Forms.TextBox tbFilename;
		private System.Windows.Forms.TextBox tbType;
		private System.Windows.Forms.TextBox tbArgC;
		private System.Windows.Forms.TextBox tbLocalC;
		private System.Windows.Forms.ComboBox tba1;
		private System.Windows.Forms.ComboBox tba2;
		private System.Windows.Forms.LinkLabel llopenbhav;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.TextBox tbInst_OpCode;
		private System.Windows.Forms.TextBox tbInst_Op7;
		private System.Windows.Forms.TextBox tbInst_Op6;
		private System.Windows.Forms.TextBox tbInst_Op5;
		private System.Windows.Forms.TextBox tbInst_Op4;
		private System.Windows.Forms.TextBox tbInst_Op3;
		private System.Windows.Forms.TextBox tbInst_Op2;
		private System.Windows.Forms.TextBox tbInst_Op1;
		private System.Windows.Forms.TextBox tbInst_Op0;
		private System.Windows.Forms.TextBox tbInst_Unk7;
		private System.Windows.Forms.TextBox tbInst_Unk6;
		private System.Windows.Forms.TextBox tbInst_Unk5;
		private System.Windows.Forms.TextBox tbInst_Unk4;
		private System.Windows.Forms.TextBox tbInst_Unk3;
		private System.Windows.Forms.TextBox tbInst_Unk2;
		private System.Windows.Forms.TextBox tbInst_Unk1;
		private System.Windows.Forms.TextBox tbInst_Unk0;
		private System.Windows.Forms.GroupBox gbInstruction;
		private System.Windows.Forms.Panel bhavPanel;
		private System.Windows.Forms.Button btnCommit;
		private System.Windows.Forms.Button btnOpCode;
		private System.Windows.Forms.Button btnOperandWiz;
		private System.Windows.Forms.Button btnSort;
		private System.Windows.Forms.Label lbUpDown;
		private System.Windows.Forms.TextBox tbLines;
		private System.Windows.Forms.Button btnUp;
		private System.Windows.Forms.Button btnDown;
		private System.Windows.Forms.Button btnDel;
		private System.Windows.Forms.Button btnAdd;
		private System.Windows.Forms.Button btnCancel;
		private SimPe.PackedFiles.UserInterface.BhavInstListControl pnflowcontainer;
		private System.Windows.Forms.GroupBox gbMove;
		private System.Windows.Forms.Label lbArgC;
		private System.Windows.Forms.GroupBox gbSpecial;
		private System.Windows.Forms.Button btnInsTrue;
		private System.Windows.Forms.Button btnInsFalse;
		private System.Windows.Forms.Button btnLinkInge;
		private System.Windows.Forms.Button btnDelPescado;
		private System.Windows.Forms.Button btnAppend;
		private System.Windows.Forms.ComboBox cbFormat;
		private System.Windows.Forms.Button btnDelMerola;
		private System.Windows.Forms.Label lbCacheFlags;
		private System.Windows.Forms.TextBox tbCacheFlags;
		private System.Windows.Forms.Label lbTreeVersion;
		private System.Windows.Forms.TextBox tbTreeVersion;
		private System.Windows.Forms.TextBox tbHeaderFlag;
		private System.Windows.Forms.Label lbHeaderFlag;
		private System.Windows.Forms.Button btnOperandRaw;
		private System.Windows.Forms.TextBox tbInst_NodeVersion;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.CheckBox cbSpecial;
		private System.Windows.Forms.TextBox tbInst_Longname;
		private System.Windows.Forms.Button btnListing;
		private System.Windows.Forms.Button btnHelp;
		private System.Windows.Forms.Button btnTPRPMaker;
        private Button btnRefreshFT;
        private Button btnGUIDIndex;
        private ContextMenuStrip cmenuGUIDIndex;
        private ToolStripMenuItem createAllPackagesToolStripMenuItem;
        private ToolStripMenuItem createCurrentPackageToolStripMenuItem;
        private ToolStripMenuItem loadIndexToolStripMenuItem;
        private ToolStripMenuItem defaultFileToolStripMenuItem;
        private ToolStripMenuItem fromFileToolStripMenuItem;
        private ToolStripMenuItem saveIndexToolStripMenuItem;
        private ToolStripMenuItem defaultFileToolStripMenuItem1;
        private ToolStripMenuItem toFileToolStripMenuItem;
        private IContainer components;
        #endregion
       
		public BhavForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			Control[] cs = {
							   tbCacheFlags, lbCacheFlags,
							   tbTreeVersion, lbTreeVersion,
							   tbHeaderFlag, lbHeaderFlag,
							   tbType, lbType,
							   cbFormat, lbFormat
						   };
			int left = this.bhavPanel.Width - 4;
			for (int i = 0; i < cs.Length; i++)
				left = cs[i].Left = left - (cs[i].Width + 4);
			this.lbFilename.Left = 4;
			this.tbFilename.Left = this.lbFilename.Right + 4;
			this.tbFilename.Width = this.lbFormat.Left - (this.tbFilename.Left + 4);
			this.Tag = "Normal"; // Used by SetReadOnly

#if DEC16
			TextBox[] iow = { null, null, null };
			alDec16 = new ArrayList(iow);
#endif
            TextBox[] iob = {
								 tbInst_Op0  ,tbInst_Op1  ,tbInst_Op2  ,tbInst_Op3
								,tbInst_Op4  ,tbInst_Op5  ,tbInst_Op6  ,tbInst_Op7
								,tbInst_Unk0 ,tbInst_Unk1 ,tbInst_Unk2 ,tbInst_Unk3
								,tbInst_Unk4 ,tbInst_Unk5 ,tbInst_Unk6 ,tbInst_Unk7
								,tbInst_NodeVersion
								,tbHeaderFlag
								,tbType
								,tbCacheFlags
								,tbArgC
								,tbLocalC
							};
			alHex8 = new ArrayList(iob);

            TextBox[] w = { tbInst_OpCode ,tbLines ,};
			alHex16 = new ArrayList(w);

			TextBox[] dw = { tbTreeVersion ,};
			alHex32 = new ArrayList(dw);

			ComboBox[] cb = { tba1 ,tba2 ,cbFormat ,};
			alHex16cb = new ArrayList(cb);

			this.gbSpecial.Visible =
				this.cbSpecial.Checked = this.ShowSpecialButtons;

			pjse.FileTable.GFT.FiletableRefresh += new System.EventHandler(this.FiletableRefresh);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
			if (setHandler && wrapper != null)
			{
				wrapper.WrapperChanged -= new System.EventHandler(this.WrapperChanged);
				setHandler = false;
			}
			wrapper = null;
			currentInst = null;
			origInst = null;
#if DEC16
			alDec16 = 
#endif
            alHex8 = alHex16 = alHex32 = alDec8 = alHex16cb = null;
		}

		
		#region BhavForm
		private Bhav wrapper;
		private bool setHandler = false;
		private BhavWiz currentInst;
		private Instruction origInst;
		private bool internalchg;
#if DEC16
		private ArrayList alDec16;
#endif
        private ArrayList alHex8;
		private ArrayList alHex16;
		private ArrayList alHex32;
		private ArrayList alDec8;
		private ArrayList alHex16cb;

		private void SetReadOnly(bool state) 
		{
			if (((string)this.Tag).Equals("Popup"))
			{
				// make it very clear it's read only
				tbFilename.Enabled = cbFormat.Enabled = tbType.Enabled =
					tbHeaderFlag.Enabled = tbTreeVersion.Enabled = tbCacheFlags.Enabled =
					tbArgC.Enabled = tbLocalC.Enabled =
					/*btnSort.Visible =*/ btnCommit.Visible = gbMove.Visible = 
					btnDel.Visible = btnAdd.Visible = 
					btnOpCode.Visible = btnOperandWiz.Visible = /*btnOperandRaw.Visible =*/
					gbSpecial.Visible = cbSpecial.Visible =
					btnCancel.Visible = false;
				btnClose.Visible = state = true;
			}

			this.tbInst_OpCode.ReadOnly = state;
			this.btnOpCode.Enabled = !state;
			this.tbInst_NodeVersion.ReadOnly = state || wrapper.Header.Format < 0x8005;
			this.tba1.Enabled = !state;
			this.tba2.Enabled = !state;

			/*this.tbInst_Op01_dec.ReadOnly = state;
			this.tbInst_Op23_dec.ReadOnly = state;*/

			this.tbInst_Op0.ReadOnly = state;
			this.tbInst_Op1.ReadOnly = state;
			this.tbInst_Op2.ReadOnly = state;
			this.tbInst_Op3.ReadOnly = state;
			this.tbInst_Op4.ReadOnly = state;
			this.tbInst_Op5.ReadOnly = state;
			this.tbInst_Op6.ReadOnly = state;
			this.tbInst_Op7.ReadOnly = state;

			this.btnOperandWiz.Enabled = !state;
			/*this.btnOperandRaw.Enabled = !state;*/
			
			this.tbInst_Unk0.ReadOnly = state || wrapper.Header.Format < 0x8003;
			this.tbInst_Unk1.ReadOnly = state || wrapper.Header.Format < 0x8003;
			this.tbInst_Unk2.ReadOnly = state || wrapper.Header.Format < 0x8003;
			this.tbInst_Unk3.ReadOnly = state || wrapper.Header.Format < 0x8003;
			this.tbInst_Unk4.ReadOnly = state || wrapper.Header.Format < 0x8003;
			this.tbInst_Unk5.ReadOnly = state || wrapper.Header.Format < 0x8003;
			this.tbInst_Unk6.ReadOnly = state || wrapper.Header.Format < 0x8003;
			this.tbInst_Unk7.ReadOnly = state || wrapper.Header.Format < 0x8003;

			this.btnUp.Enabled = !state;
			this.btnDown.Enabled = !state;
			this.tbLines.ReadOnly = state;
			this.btnDelPescado.Enabled = this.btnDel.Enabled = !state;
			this.btnInsTrue.Enabled = this.btnInsFalse.Enabled = this.btnAdd.Enabled = !state;
		}

        private bool instIsBhav()
        {
            return wrapper.ResourceByInstance(SimPe.Data.MetaData.BHAV_FILE, currentInst.Instruction.OpCode) != null;
        }

		private void UpdateInstPanel()
		{
			internalchg = true;
			if (currentInst == null || wrapper.IndexOf(currentInst.Instruction) < 0)
			{
				SetReadOnly(true);
				this.llopenbhav.Enabled = false;
				this.btnInsTrue.Enabled = this.btnInsFalse.Enabled = this.btnAdd.Enabled = true;

				this.tbInst_OpCode.Text = "";
				this.tbInst_NodeVersion.Text = "";
				this.tba1.SelectedIndex = 0;
				this.tba2.SelectedIndex = 0;
				this.tbInst_Op0.Text = "";
				this.tbInst_Op1.Text = "";
				this.tbInst_Op2.Text = "";
				this.tbInst_Op3.Text = "";
				this.tbInst_Op4.Text = "";
				this.tbInst_Op5.Text = "";
				this.tbInst_Op6.Text = "";
				this.tbInst_Op7.Text = "";
				this.tbInst_Unk0.Text = "";
				this.tbInst_Unk1.Text = "";
				this.tbInst_Unk2.Text = "";
				this.tbInst_Unk3.Text = "";
				this.tbInst_Unk4.Text = "";
				this.tbInst_Unk5.Text = "";
				this.tbInst_Unk6.Text = "";
				this.tbInst_Unk7.Text = "";

				Longname = "";
			}
			else
			{
				Instruction inst = currentInst.Instruction; // saves typing

				SetReadOnly(false);

				this.tbInst_OpCode.Text = "0x"+Helper.HexString(inst.OpCode);
				this.tbInst_NodeVersion.Text = "0x"+Helper.HexString(inst.NodeVersion);

				if (inst.Target1 >= 0xFFFC && inst.Target1 < 0xFFFF)
				{
					this.tba1.SelectedIndex = inst.Target1 - 0xFFFC;
				}
				else
				{
					this.tba1.SelectedIndex = -1;
					this.tba1.Text = "0x"+Helper.HexString(inst.Target1);
				}
				if (inst.Target2 >= 0xFFFC && inst.Target2 < 0xFFFF)
				{
					this.tba2.SelectedIndex = inst.Target2 - 0xFFFC;
				}
				else
				{
					this.tba2.SelectedIndex = -1;
					this.tba2.Text = "0x"+Helper.HexString(inst.Target2);
				}

				this.tbInst_Op0.Text = Helper.HexString(inst.Operands[0]);
				this.tbInst_Op1.Text = Helper.HexString(inst.Operands[1]);
				this.tbInst_Op2.Text = Helper.HexString(inst.Operands[2]);
				this.tbInst_Op3.Text = Helper.HexString(inst.Operands[3]);
				this.tbInst_Op4.Text = Helper.HexString(inst.Operands[4]);
				this.tbInst_Op5.Text = Helper.HexString(inst.Operands[5]);
				this.tbInst_Op6.Text = Helper.HexString(inst.Operands[6]);
				this.tbInst_Op7.Text = Helper.HexString(inst.Operands[7]);

				this.tbInst_Unk0.Text = Helper.HexString(inst.Reserved1[0]);
				this.tbInst_Unk1.Text = Helper.HexString(inst.Reserved1[1]);
				this.tbInst_Unk2.Text = Helper.HexString(inst.Reserved1[2]);
				this.tbInst_Unk3.Text = Helper.HexString(inst.Reserved1[3]);
				this.tbInst_Unk4.Text = Helper.HexString(inst.Reserved1[4]);
				this.tbInst_Unk5.Text = Helper.HexString(inst.Reserved1[5]);
				this.tbInst_Unk6.Text = Helper.HexString(inst.Reserved1[6]);
				this.tbInst_Unk7.Text = Helper.HexString(inst.Reserved1[7]);

				this.btnUp.Enabled = pnflowcontainer.SelectedIndex > 0;
				this.btnDown.Enabled = pnflowcontainer.SelectedIndex < wrapper.Count - 1;

				this.btnDelPescado.Enabled = this.btnDel.Enabled = wrapper.Count > 1;

                this.llopenbhav.Enabled = instIsBhav();
				this.btnOperandWiz.Enabled = currentInst.Wizard() != null;
				Longname = currentInst.LongName;
			}
			internalchg = false;
		}


        private static string onearg = pjse.Localization.GetString("oneArg");
        private static string manyargs = pjse.Localization.GetString("manyArgs");
        private string Longname
		{
			set
			{
				this.tbInst_Longname.Text = value.Replace(", ", ",\r\n  ")
                    .Replace(onearg + ": ", onearg  +":\r\n  ")
                    .Replace(manyargs + ": ", manyargs + ":\r\n  ")
                    ;
			}
		}


		private void CopyListing()
		{
			string listing = "";

			int lines = wrapper.Count;
			for (short i = 0; i < lines; i++)
			{
				Instruction inst = wrapper[i];
				BhavWiz w = inst;

				string operands = "";
				for(int j = 0; j < 7; j++) operands += SimPe.Helper.HexString(inst.Operands[j]);
				for(int j = 0; j < 7; j++) operands += SimPe.Helper.HexString(inst.Reserved1[j]);

				listing += ("     "
					+ SimPe.Helper.HexString(i)
					+ " : " + SimPe.Helper.HexString(inst.OpCode)
					+ " : " + operands
					+ " : " + SimPe.Helper.HexString(inst.NodeVersion)
					+ " : " + SimPe.Helper.HexString(inst.Target1)
					+ " : " + SimPe.Helper.HexString(inst.Target2)
					+ "\r\n" + w.ShortName + "\r\n\r\n");
			}

			Clipboard.SetDataObject(listing, true);
		}

		private void TPRPMaker()
		{
			int minArgc = 0;
			int minLocalC = 0;
			TPRP tprp = wrapper.TPRPResource;

			wrapper.Package.BeginUpdate();

			// find TPRP for this BHAV
			if (tprp != null)
			{
				// if it exists ask if user wants to preserve content
				DialogResult dr = MessageBox.Show(
                    pjse.Localization.GetString("ml_keeplabels")
					, btnTPRPMaker.Text
					, MessageBoxButtons.YesNoCancel
					, MessageBoxIcon.Warning);
				if (dr == DialogResult.Cancel)
					return;
				if (dr == DialogResult.Yes)
				{
					minArgc = tprp.ParamCount;
					minLocalC = tprp.LocalCount;
				}
				else
					tprp.Clear();
			}
			else
			{
				// create a new TPRP file
				SimPe.Interfaces.Files.IPackedFileDescriptor npfd
					= wrapper.Package.Add(0x54505250, 0, wrapper.FileDescriptor.Group, wrapper.FileDescriptor.Instance);
				tprp = new TPRP();
				tprp.ProcessData(npfd, wrapper.Package);
				tprp.FileName = wrapper.FileName;
			}

			for(int arg = minArgc; arg < wrapper.Header.ArgumentCount; arg++)
			{
				int p = tprp.Add(new TPRPParamLabel(tprp));
				tprp[false, p].Label = BhavWiz.dnParam() + " " + arg.ToString();
			}
			for(int local = minLocalC; local < wrapper.Header.LocalVarCount; local++)
			{
				int l = tprp.Add(new TPRPLocalLabel(tprp));
                tprp[true, l].Label = BhavWiz.dnLocal() + " " + local.ToString();
			}
			tprp.SynchronizeUserData();
			wrapper.Package.EndUpdate();
			MessageBox.Show(
                pjse.Localization.GetString("ml_done")
                , btnTPRPMaker.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
		}


		private short OpsToShort(byte lo, byte hi)
		{
			ushort uval = (ushort)(lo + (hi << 8));
			if (uval > 32767) return (short)(uval - 65536);
			else return (short)uval;
		}

		private byte[] ShortToOps(short val)
		{
			byte[] ops = new byte[2];
			ushort uval;
			if (val < 0)
				uval = (ushort)(65536 + val);
			else
				uval = (ushort)val;
			ops[0] = (byte)(uval & 0xFF);
			ops[1] = (byte)((uval >> 8) & 0xFF);
			return ops;
		}

		private bool cbHex16_IsValid(object sender)
		{
			if (alHex16cb.IndexOf(sender) < 0)
				throw new Exception("cbHex16_IsValid not applicable to control " + sender.ToString());
			if (((ComboBox)sender).Items.IndexOf(((ComboBox)sender).Text) != -1) return true;

			try { Convert.ToUInt16(((ComboBox)sender).Text, 16); }
			catch (Exception) { return false; }
			return true;
		}

		private bool dec8_IsValid(object sender)
		{
			if (alDec8.IndexOf(sender) < 0)
				throw new Exception("dec8_IsValid not applicable to control " + sender.ToString());
			try { Convert.ToByte(((TextBox)sender).Text); }
			catch (Exception) { return false; }
			return true;
		}

#if DEC16
		private bool dec16_IsValid(object sender)
		{
			if (alDec16.IndexOf(sender) < 0)
				throw new Exception("dec16_IsValid not applicable to control " + sender.ToString());
			try { Convert.ToInt16(((TextBox)sender).Text); }
			catch (Exception) { return false; }
			return true;
		}
#endif

		private bool hex8_IsValid(object sender)
		{
			if (alHex8.IndexOf(sender) < 0)
				throw new Exception("hex8_IsValid not applicable to control " + sender.ToString());
			try { Convert.ToByte(((TextBox)sender).Text, 16); }
			catch (Exception) { return false; }
			return true;
		}

		private bool hex16_IsValid(object sender)
		{
			if (alHex16.IndexOf(sender) < 0)
				throw new Exception("hex16_IsValid not applicable to control " + sender.ToString());
			try { Convert.ToUInt16(((TextBox)sender).Text, 16); }
			catch (Exception) { return false; }
			return true;
		}

		private bool hex32_IsValid(object sender)
		{
			if (alHex32.IndexOf(sender) < 0)
				throw new Exception("hex32_IsValid not applicable to control " + sender.ToString());
			try { Convert.ToUInt32(((TextBox)sender).Text, 16); }
			catch (Exception) { return false; }
			return true;
		}


		private bool ShowSpecialButtons
		{
			get
			{
				XmlRegistryKey  rkf = Helper.WindowsRegistry.PluginRegistryKey.CreateSubKey("PJSE\\Bhav");
				object o = rkf.GetValue("showSpecialButtons", false);
				return Convert.ToBoolean(o);
			}

			set
			{
				XmlRegistryKey rkf = Helper.WindowsRegistry.PluginRegistryKey.CreateSubKey("PJSE\\Bhav");
				rkf.SetValue("showSpecialButtons", value);
			}

		}

		private void FiletableRefresh(object sender, System.EventArgs e)
		{
			UpdateInstPanel();
		}

		#endregion

		#region IPackedFileUI Member
		/// <summary>
		/// Returns the Control that will be displayed within SimPe
		/// </summary>
		public Control GUIHandle
		{
			get
			{
				return bhavPanel;
			}
		}

		/// <summary>
		/// Called by the AbstractWrapper when the file should be displayed to the user.
		/// </summary>
		/// <param name="wrp">Reference to the wrapper to be displayed.</param>
		public void UpdateGUI(IFileWrapper wrp)
		{
			wrapper = (Bhav) wrp;

			internalchg = true;
            this.tbLines.Text = "0x0001";
			internalchg = false;

			this.WrapperChanged(wrapper, null);

			currentInst = null;
			origInst = null;
			UpdateInstPanel();
			this.pnflowcontainer.UpdateGUI(wrapper);
			// pnflowcontainer to install its handler before us.
			if (!setHandler)
			{
				wrapper.WrapperChanged += new System.EventHandler(this.WrapperChanged);
				setHandler = true;
			}
		}

		private void WrapperChanged(object sender, System.EventArgs e)
		{
			if (((string)this.Tag).Equals("Popup"))
			{
				wrapper.Changed = false;
			}

			this.btnCommit.Enabled = wrapper.Changed;

			// Handler for header
			if (sender == wrapper)
			{
				if (internalchg) return;
				internalchg = true;
				/*this.Text = */tbFilename.Text = wrapper.FileName;
				cbFormat.Text = "0x"+Helper.HexString(wrapper.Header.Format);
				tbType.Text = "0x"+Helper.HexString(wrapper.Header.Type);
				tbArgC.Text = "0x"+Helper.HexString(wrapper.Header.ArgumentCount);
				tbLocalC.Text = "0x"+Helper.HexString(wrapper.Header.LocalVarCount);
				tbHeaderFlag.Text = "0x"+Helper.HexString(wrapper.Header.HeaderFlag);
				tbTreeVersion.Text = "0x"+Helper.HexString(wrapper.Header.TreeVersion);
				tbCacheFlags.Text = "0x"+Helper.HexString(wrapper.Header.CacheFlags);
				tbCacheFlags.Enabled = (wrapper.Header.Format > 0x8008);
				internalchg = false;
			}

				// Handler for current instruction
			if (currentInst != null && sender == currentInst.Instruction)
			{
				if (internalchg)
				{
					this.btnCancel.Enabled = true;

					this.currentInst = currentInst.Instruction;
                    this.llopenbhav.Enabled = instIsBhav();
					this.btnOperandWiz.Enabled = currentInst.Wizard() != null;
					Longname = currentInst.LongName;
				}
				else
					pnflowcontainer_SelectedInstChanged(null, null);
			}
		}

		#endregion

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BhavForm));
            this.label1 = new System.Windows.Forms.Label();
            this.gbInstruction = new System.Windows.Forms.GroupBox();
            this.tbInst_Longname = new System.Windows.Forms.TextBox();
            this.btnOperandRaw = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOperandWiz = new System.Windows.Forms.Button();
            this.llopenbhav = new System.Windows.Forms.LinkLabel();
            this.tba2 = new System.Windows.Forms.ComboBox();
            this.tba1 = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.tbInst_Unk7 = new System.Windows.Forms.TextBox();
            this.tbInst_Unk6 = new System.Windows.Forms.TextBox();
            this.tbInst_Unk5 = new System.Windows.Forms.TextBox();
            this.tbInst_Unk4 = new System.Windows.Forms.TextBox();
            this.tbInst_Unk3 = new System.Windows.Forms.TextBox();
            this.tbInst_Unk2 = new System.Windows.Forms.TextBox();
            this.tbInst_Unk1 = new System.Windows.Forms.TextBox();
            this.tbInst_Unk0 = new System.Windows.Forms.TextBox();
            this.tbInst_Op7 = new System.Windows.Forms.TextBox();
            this.tbInst_Op6 = new System.Windows.Forms.TextBox();
            this.tbInst_Op5 = new System.Windows.Forms.TextBox();
            this.tbInst_Op4 = new System.Windows.Forms.TextBox();
            this.tbInst_Op3 = new System.Windows.Forms.TextBox();
            this.tbInst_Op2 = new System.Windows.Forms.TextBox();
            this.tbInst_Op1 = new System.Windows.Forms.TextBox();
            this.tbInst_Op0 = new System.Windows.Forms.TextBox();
            this.tbInst_NodeVersion = new System.Windows.Forms.TextBox();
            this.tbInst_OpCode = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.btnOpCode = new System.Windows.Forms.Button();
            this.tbFilename = new System.Windows.Forms.TextBox();
            this.lbFilename = new System.Windows.Forms.Label();
            this.tbLocalC = new System.Windows.Forms.TextBox();
            this.tbArgC = new System.Windows.Forms.TextBox();
            this.tbType = new System.Windows.Forms.TextBox();
            this.lbTreeVersion = new System.Windows.Forms.Label();
            this.lbType = new System.Windows.Forms.Label();
            this.lbLocalC = new System.Windows.Forms.Label();
            this.lbArgC = new System.Windows.Forms.Label();
            this.lbFormat = new System.Windows.Forms.Label();
            this.pnHeading = new System.Windows.Forms.Panel();
            this.btnRefreshFT = new System.Windows.Forms.Button();
            this.btnHelp = new System.Windows.Forms.Button();
            this.bhavPanel = new System.Windows.Forms.Panel();
            this.cbSpecial = new System.Windows.Forms.CheckBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.tbHeaderFlag = new System.Windows.Forms.TextBox();
            this.lbHeaderFlag = new System.Windows.Forms.Label();
            this.tbCacheFlags = new System.Windows.Forms.TextBox();
            this.cbFormat = new System.Windows.Forms.ComboBox();
            this.gbSpecial = new System.Windows.Forms.GroupBox();
            this.btnAppend = new System.Windows.Forms.Button();
            this.btnInsTrue = new System.Windows.Forms.Button();
            this.btnInsFalse = new System.Windows.Forms.Button();
            this.btnDelPescado = new System.Windows.Forms.Button();
            this.btnLinkInge = new System.Windows.Forms.Button();
            this.btnGUIDIndex = new System.Windows.Forms.Button();
            this.btnDelMerola = new System.Windows.Forms.Button();
            this.btnListing = new System.Windows.Forms.Button();
            this.btnTPRPMaker = new System.Windows.Forms.Button();
            this.pnflowcontainer = new SimPe.PackedFiles.UserInterface.BhavInstListControl();
            this.btnDel = new System.Windows.Forms.Button();
            this.gbMove = new System.Windows.Forms.GroupBox();
            this.btnUp = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.lbUpDown = new System.Windows.Forms.Label();
            this.tbLines = new System.Windows.Forms.TextBox();
            this.btnSort = new System.Windows.Forms.Button();
            this.btnCommit = new System.Windows.Forms.Button();
            this.tbTreeVersion = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.lbCacheFlags = new System.Windows.Forms.Label();
            this.cmenuGUIDIndex = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.createAllPackagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createCurrentPackageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadIndexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.defaultFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fromFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveIndexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.defaultFileToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gbInstruction.SuspendLayout();
            this.pnHeading.SuspendLayout();
            this.bhavPanel.SuspendLayout();
            this.gbSpecial.SuspendLayout();
            this.gbMove.SuspendLayout();
            this.cmenuGUIDIndex.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Name = "label1";
            // 
            // gbInstruction
            // 
            resources.ApplyResources(this.gbInstruction, "gbInstruction");
            this.gbInstruction.Controls.Add(this.tbInst_Longname);
            this.gbInstruction.Controls.Add(this.btnOperandRaw);
            this.gbInstruction.Controls.Add(this.btnCancel);
            this.gbInstruction.Controls.Add(this.btnOperandWiz);
            this.gbInstruction.Controls.Add(this.llopenbhav);
            this.gbInstruction.Controls.Add(this.tba2);
            this.gbInstruction.Controls.Add(this.tba1);
            this.gbInstruction.Controls.Add(this.label13);
            this.gbInstruction.Controls.Add(this.tbInst_Unk7);
            this.gbInstruction.Controls.Add(this.tbInst_Unk6);
            this.gbInstruction.Controls.Add(this.tbInst_Unk5);
            this.gbInstruction.Controls.Add(this.tbInst_Unk4);
            this.gbInstruction.Controls.Add(this.tbInst_Unk3);
            this.gbInstruction.Controls.Add(this.tbInst_Unk2);
            this.gbInstruction.Controls.Add(this.tbInst_Unk1);
            this.gbInstruction.Controls.Add(this.tbInst_Unk0);
            this.gbInstruction.Controls.Add(this.tbInst_Op7);
            this.gbInstruction.Controls.Add(this.tbInst_Op6);
            this.gbInstruction.Controls.Add(this.tbInst_Op5);
            this.gbInstruction.Controls.Add(this.tbInst_Op4);
            this.gbInstruction.Controls.Add(this.tbInst_Op3);
            this.gbInstruction.Controls.Add(this.tbInst_Op2);
            this.gbInstruction.Controls.Add(this.tbInst_Op1);
            this.gbInstruction.Controls.Add(this.tbInst_Op0);
            this.gbInstruction.Controls.Add(this.tbInst_NodeVersion);
            this.gbInstruction.Controls.Add(this.tbInst_OpCode);
            this.gbInstruction.Controls.Add(this.label10);
            this.gbInstruction.Controls.Add(this.label9);
            this.gbInstruction.Controls.Add(this.label12);
            this.gbInstruction.Controls.Add(this.label11);
            this.gbInstruction.Controls.Add(this.btnOpCode);
            this.gbInstruction.Name = "gbInstruction";
            this.gbInstruction.TabStop = false;
            // 
            // tbInst_Longname
            // 
            resources.ApplyResources(this.tbInst_Longname, "tbInst_Longname");
            this.tbInst_Longname.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbInst_Longname.Name = "tbInst_Longname";
            this.tbInst_Longname.ReadOnly = true;
            // 
            // btnOperandRaw
            // 
            resources.ApplyResources(this.btnOperandRaw, "btnOperandRaw");
            this.btnOperandRaw.Name = "btnOperandRaw";
            this.btnOperandRaw.Click += new System.EventHandler(this.btnOperandRaw_Click);
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Clicked);
            // 
            // btnOperandWiz
            // 
            resources.ApplyResources(this.btnOperandWiz, "btnOperandWiz");
            this.btnOperandWiz.Name = "btnOperandWiz";
            this.btnOperandWiz.Click += new System.EventHandler(this.btnOperandWiz_Clicked);
            // 
            // llopenbhav
            // 
            resources.ApplyResources(this.llopenbhav, "llopenbhav");
            this.llopenbhav.Name = "llopenbhav";
            this.llopenbhav.TabStop = true;
            this.llopenbhav.UseCompatibleTextRendering = true;
            this.llopenbhav.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llopenbhav_LinkClicked);
            // 
            // tba2
            // 
            resources.ApplyResources(this.tba2, "tba2");
            this.tba2.Items.AddRange(new object[] {
            resources.GetString("tba2.Items"),
            resources.GetString("tba2.Items1"),
            resources.GetString("tba2.Items2")});
            this.tba2.Name = "tba2";
            this.tba2.QueryContinueDrag += new System.Windows.Forms.QueryContinueDragEventHandler(this.ItemQueryContinueDragTarget);
            this.tba2.Validating += new System.ComponentModel.CancelEventHandler(this.cbHex16_Validating);
            this.tba2.DragEnter += new System.Windows.Forms.DragEventHandler(this.ItemDragEnter);
            this.tba2.DragDrop += new System.Windows.Forms.DragEventHandler(this.ItemDrop);
            this.tba2.Validated += new System.EventHandler(this.cbHex16_Validated);
            this.tba2.Enter += new System.EventHandler(this.cbHex16_Enter);
            this.tba2.SelectedIndexChanged += new System.EventHandler(this.cbHex16_SelectedIndexChanged);
            this.tba2.TextChanged += new System.EventHandler(this.cbHex16_TextChanged);
            this.tba2.DragOver += new System.Windows.Forms.DragEventHandler(this.ItemDragEnter);
            // 
            // tba1
            // 
            resources.ApplyResources(this.tba1, "tba1");
            this.tba1.Items.AddRange(new object[] {
            resources.GetString("tba1.Items"),
            resources.GetString("tba1.Items1"),
            resources.GetString("tba1.Items2")});
            this.tba1.Name = "tba1";
            this.tba1.QueryContinueDrag += new System.Windows.Forms.QueryContinueDragEventHandler(this.ItemQueryContinueDragTarget);
            this.tba1.Validating += new System.ComponentModel.CancelEventHandler(this.cbHex16_Validating);
            this.tba1.DragEnter += new System.Windows.Forms.DragEventHandler(this.ItemDragEnter);
            this.tba1.DragDrop += new System.Windows.Forms.DragEventHandler(this.ItemDrop);
            this.tba1.Validated += new System.EventHandler(this.cbHex16_Validated);
            this.tba1.Enter += new System.EventHandler(this.cbHex16_Enter);
            this.tba1.SelectedIndexChanged += new System.EventHandler(this.cbHex16_SelectedIndexChanged);
            this.tba1.TextChanged += new System.EventHandler(this.cbHex16_TextChanged);
            this.tba1.DragOver += new System.Windows.Forms.DragEventHandler(this.ItemDragEnter);
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.Name = "label13";
            // 
            // tbInst_Unk7
            // 
            resources.ApplyResources(this.tbInst_Unk7, "tbInst_Unk7");
            this.tbInst_Unk7.Name = "tbInst_Unk7";
            this.tbInst_Unk7.Validated += new System.EventHandler(this.hex8_Validated);
            this.tbInst_Unk7.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
            this.tbInst_Unk7.TextChanged += new System.EventHandler(this.hex8_TextChanged);
            // 
            // tbInst_Unk6
            // 
            resources.ApplyResources(this.tbInst_Unk6, "tbInst_Unk6");
            this.tbInst_Unk6.Name = "tbInst_Unk6";
            this.tbInst_Unk6.Validated += new System.EventHandler(this.hex8_Validated);
            this.tbInst_Unk6.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
            this.tbInst_Unk6.TextChanged += new System.EventHandler(this.hex8_TextChanged);
            // 
            // tbInst_Unk5
            // 
            resources.ApplyResources(this.tbInst_Unk5, "tbInst_Unk5");
            this.tbInst_Unk5.Name = "tbInst_Unk5";
            this.tbInst_Unk5.Validated += new System.EventHandler(this.hex8_Validated);
            this.tbInst_Unk5.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
            this.tbInst_Unk5.TextChanged += new System.EventHandler(this.hex8_TextChanged);
            // 
            // tbInst_Unk4
            // 
            resources.ApplyResources(this.tbInst_Unk4, "tbInst_Unk4");
            this.tbInst_Unk4.Name = "tbInst_Unk4";
            this.tbInst_Unk4.Validated += new System.EventHandler(this.hex8_Validated);
            this.tbInst_Unk4.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
            this.tbInst_Unk4.TextChanged += new System.EventHandler(this.hex8_TextChanged);
            // 
            // tbInst_Unk3
            // 
            resources.ApplyResources(this.tbInst_Unk3, "tbInst_Unk3");
            this.tbInst_Unk3.Name = "tbInst_Unk3";
            this.tbInst_Unk3.Validated += new System.EventHandler(this.hex8_Validated);
            this.tbInst_Unk3.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
            this.tbInst_Unk3.TextChanged += new System.EventHandler(this.hex8_TextChanged);
            // 
            // tbInst_Unk2
            // 
            resources.ApplyResources(this.tbInst_Unk2, "tbInst_Unk2");
            this.tbInst_Unk2.Name = "tbInst_Unk2";
            this.tbInst_Unk2.Validated += new System.EventHandler(this.hex8_Validated);
            this.tbInst_Unk2.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
            this.tbInst_Unk2.TextChanged += new System.EventHandler(this.hex8_TextChanged);
            // 
            // tbInst_Unk1
            // 
            resources.ApplyResources(this.tbInst_Unk1, "tbInst_Unk1");
            this.tbInst_Unk1.Name = "tbInst_Unk1";
            this.tbInst_Unk1.Validated += new System.EventHandler(this.hex8_Validated);
            this.tbInst_Unk1.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
            this.tbInst_Unk1.TextChanged += new System.EventHandler(this.hex8_TextChanged);
            // 
            // tbInst_Unk0
            // 
            resources.ApplyResources(this.tbInst_Unk0, "tbInst_Unk0");
            this.tbInst_Unk0.Name = "tbInst_Unk0";
            this.tbInst_Unk0.Validated += new System.EventHandler(this.hex8_Validated);
            this.tbInst_Unk0.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
            this.tbInst_Unk0.TextChanged += new System.EventHandler(this.hex8_TextChanged);
            // 
            // tbInst_Op7
            // 
            resources.ApplyResources(this.tbInst_Op7, "tbInst_Op7");
            this.tbInst_Op7.Name = "tbInst_Op7";
            this.tbInst_Op7.Validated += new System.EventHandler(this.hex8_Validated);
            this.tbInst_Op7.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
            this.tbInst_Op7.TextChanged += new System.EventHandler(this.hex8_TextChanged);
            // 
            // tbInst_Op6
            // 
            resources.ApplyResources(this.tbInst_Op6, "tbInst_Op6");
            this.tbInst_Op6.Name = "tbInst_Op6";
            this.tbInst_Op6.Validated += new System.EventHandler(this.hex8_Validated);
            this.tbInst_Op6.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
            this.tbInst_Op6.TextChanged += new System.EventHandler(this.hex8_TextChanged);
            // 
            // tbInst_Op5
            // 
            resources.ApplyResources(this.tbInst_Op5, "tbInst_Op5");
            this.tbInst_Op5.Name = "tbInst_Op5";
            this.tbInst_Op5.Validated += new System.EventHandler(this.hex8_Validated);
            this.tbInst_Op5.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
            this.tbInst_Op5.TextChanged += new System.EventHandler(this.hex8_TextChanged);
            // 
            // tbInst_Op4
            // 
            resources.ApplyResources(this.tbInst_Op4, "tbInst_Op4");
            this.tbInst_Op4.Name = "tbInst_Op4";
            this.tbInst_Op4.Validated += new System.EventHandler(this.hex8_Validated);
            this.tbInst_Op4.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
            this.tbInst_Op4.TextChanged += new System.EventHandler(this.hex8_TextChanged);
            // 
            // tbInst_Op3
            // 
            resources.ApplyResources(this.tbInst_Op3, "tbInst_Op3");
            this.tbInst_Op3.Name = "tbInst_Op3";
            this.tbInst_Op3.Validated += new System.EventHandler(this.hex8_Validated);
            this.tbInst_Op3.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
            this.tbInst_Op3.TextChanged += new System.EventHandler(this.hex8_TextChanged);
            // 
            // tbInst_Op2
            // 
            resources.ApplyResources(this.tbInst_Op2, "tbInst_Op2");
            this.tbInst_Op2.Name = "tbInst_Op2";
            this.tbInst_Op2.Validated += new System.EventHandler(this.hex8_Validated);
            this.tbInst_Op2.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
            this.tbInst_Op2.TextChanged += new System.EventHandler(this.hex8_TextChanged);
            // 
            // tbInst_Op1
            // 
            resources.ApplyResources(this.tbInst_Op1, "tbInst_Op1");
            this.tbInst_Op1.Name = "tbInst_Op1";
            this.tbInst_Op1.Validated += new System.EventHandler(this.hex8_Validated);
            this.tbInst_Op1.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
            this.tbInst_Op1.TextChanged += new System.EventHandler(this.hex8_TextChanged);
            // 
            // tbInst_Op0
            // 
            resources.ApplyResources(this.tbInst_Op0, "tbInst_Op0");
            this.tbInst_Op0.Name = "tbInst_Op0";
            this.tbInst_Op0.Validated += new System.EventHandler(this.hex8_Validated);
            this.tbInst_Op0.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
            this.tbInst_Op0.TextChanged += new System.EventHandler(this.hex8_TextChanged);
            // 
            // tbInst_NodeVersion
            // 
            resources.ApplyResources(this.tbInst_NodeVersion, "tbInst_NodeVersion");
            this.tbInst_NodeVersion.Name = "tbInst_NodeVersion";
            this.tbInst_NodeVersion.Validated += new System.EventHandler(this.hex8_Validated);
            this.tbInst_NodeVersion.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
            this.tbInst_NodeVersion.TextChanged += new System.EventHandler(this.hex8_TextChanged);
            // 
            // tbInst_OpCode
            // 
            resources.ApplyResources(this.tbInst_OpCode, "tbInst_OpCode");
            this.tbInst_OpCode.Name = "tbInst_OpCode";
            this.tbInst_OpCode.Validated += new System.EventHandler(this.hex16_Validated);
            this.tbInst_OpCode.Validating += new System.ComponentModel.CancelEventHandler(this.hex16_Validating);
            this.tbInst_OpCode.TextChanged += new System.EventHandler(this.hex16_TextChanged);
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            // 
            // btnOpCode
            // 
            resources.ApplyResources(this.btnOpCode, "btnOpCode");
            this.btnOpCode.Name = "btnOpCode";
            this.btnOpCode.Click += new System.EventHandler(this.btnOpCode_Clicked);
            // 
            // tbFilename
            // 
            resources.ApplyResources(this.tbFilename, "tbFilename");
            this.tbFilename.Name = "tbFilename";
            this.tbFilename.Validated += new System.EventHandler(this.tbFilename_Validated);
            this.tbFilename.TextChanged += new System.EventHandler(this.tbFilename_TextChanged);
            // 
            // lbFilename
            // 
            resources.ApplyResources(this.lbFilename, "lbFilename");
            this.lbFilename.Name = "lbFilename";
            // 
            // tbLocalC
            // 
            resources.ApplyResources(this.tbLocalC, "tbLocalC");
            this.tbLocalC.Name = "tbLocalC";
            this.tbLocalC.Validated += new System.EventHandler(this.hex8_Validated);
            this.tbLocalC.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
            this.tbLocalC.TextChanged += new System.EventHandler(this.hex8_TextChanged);
            // 
            // tbArgC
            // 
            resources.ApplyResources(this.tbArgC, "tbArgC");
            this.tbArgC.Name = "tbArgC";
            this.tbArgC.Validated += new System.EventHandler(this.hex8_Validated);
            this.tbArgC.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
            this.tbArgC.TextChanged += new System.EventHandler(this.hex8_TextChanged);
            // 
            // tbType
            // 
            resources.ApplyResources(this.tbType, "tbType");
            this.tbType.Name = "tbType";
            this.tbType.Validated += new System.EventHandler(this.hex8_Validated);
            this.tbType.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
            this.tbType.TextChanged += new System.EventHandler(this.hex8_TextChanged);
            // 
            // lbTreeVersion
            // 
            resources.ApplyResources(this.lbTreeVersion, "lbTreeVersion");
            this.lbTreeVersion.Name = "lbTreeVersion";
            // 
            // lbType
            // 
            resources.ApplyResources(this.lbType, "lbType");
            this.lbType.Name = "lbType";
            // 
            // lbLocalC
            // 
            resources.ApplyResources(this.lbLocalC, "lbLocalC");
            this.lbLocalC.Name = "lbLocalC";
            // 
            // lbArgC
            // 
            resources.ApplyResources(this.lbArgC, "lbArgC");
            this.lbArgC.Name = "lbArgC";
            // 
            // lbFormat
            // 
            resources.ApplyResources(this.lbFormat, "lbFormat");
            this.lbFormat.Name = "lbFormat";
            // 
            // pnHeading
            // 
            resources.ApplyResources(this.pnHeading, "pnHeading");
            this.pnHeading.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.pnHeading.Controls.Add(this.btnRefreshFT);
            this.pnHeading.Controls.Add(this.btnHelp);
            this.pnHeading.Controls.Add(this.label1);
            this.pnHeading.Name = "pnHeading";
            // 
            // btnRefreshFT
            // 
            resources.ApplyResources(this.btnRefreshFT, "btnRefreshFT");
            this.btnRefreshFT.Name = "btnRefreshFT";
            this.btnRefreshFT.Click += new System.EventHandler(this.btnRefreshFT_Click);
            // 
            // btnHelp
            // 
            resources.ApplyResources(this.btnHelp, "btnHelp");
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // bhavPanel
            // 
            resources.ApplyResources(this.bhavPanel, "bhavPanel");
            this.bhavPanel.BackColor = System.Drawing.SystemColors.Control;
            this.bhavPanel.Controls.Add(this.cbSpecial);
            this.bhavPanel.Controls.Add(this.btnClose);
            this.bhavPanel.Controls.Add(this.tbHeaderFlag);
            this.bhavPanel.Controls.Add(this.lbHeaderFlag);
            this.bhavPanel.Controls.Add(this.tbCacheFlags);
            this.bhavPanel.Controls.Add(this.cbFormat);
            this.bhavPanel.Controls.Add(this.gbSpecial);
            this.bhavPanel.Controls.Add(this.pnflowcontainer);
            this.bhavPanel.Controls.Add(this.btnDel);
            this.bhavPanel.Controls.Add(this.gbMove);
            this.bhavPanel.Controls.Add(this.btnSort);
            this.bhavPanel.Controls.Add(this.btnCommit);
            this.bhavPanel.Controls.Add(this.lbFilename);
            this.bhavPanel.Controls.Add(this.tbFilename);
            this.bhavPanel.Controls.Add(this.gbInstruction);
            this.bhavPanel.Controls.Add(this.tbLocalC);
            this.bhavPanel.Controls.Add(this.tbTreeVersion);
            this.bhavPanel.Controls.Add(this.tbArgC);
            this.bhavPanel.Controls.Add(this.tbType);
            this.bhavPanel.Controls.Add(this.lbTreeVersion);
            this.bhavPanel.Controls.Add(this.lbType);
            this.bhavPanel.Controls.Add(this.lbLocalC);
            this.bhavPanel.Controls.Add(this.lbArgC);
            this.bhavPanel.Controls.Add(this.lbFormat);
            this.bhavPanel.Controls.Add(this.pnHeading);
            this.bhavPanel.Controls.Add(this.btnAdd);
            this.bhavPanel.Controls.Add(this.lbCacheFlags);
            this.bhavPanel.Name = "bhavPanel";
            // 
            // cbSpecial
            // 
            resources.ApplyResources(this.cbSpecial, "cbSpecial");
            this.cbSpecial.Name = "cbSpecial";
            this.cbSpecial.CheckStateChanged += new System.EventHandler(this.cbSpecial_CheckStateChanged);
            // 
            // btnClose
            // 
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Name = "btnClose";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // tbHeaderFlag
            // 
            resources.ApplyResources(this.tbHeaderFlag, "tbHeaderFlag");
            this.tbHeaderFlag.Name = "tbHeaderFlag";
            this.tbHeaderFlag.Validated += new System.EventHandler(this.hex8_Validated);
            this.tbHeaderFlag.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
            this.tbHeaderFlag.TextChanged += new System.EventHandler(this.hex8_TextChanged);
            // 
            // lbHeaderFlag
            // 
            resources.ApplyResources(this.lbHeaderFlag, "lbHeaderFlag");
            this.lbHeaderFlag.Name = "lbHeaderFlag";
            // 
            // tbCacheFlags
            // 
            resources.ApplyResources(this.tbCacheFlags, "tbCacheFlags");
            this.tbCacheFlags.Name = "tbCacheFlags";
            this.tbCacheFlags.Validated += new System.EventHandler(this.hex8_Validated);
            this.tbCacheFlags.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
            this.tbCacheFlags.TextChanged += new System.EventHandler(this.hex8_TextChanged);
            // 
            // cbFormat
            // 
            resources.ApplyResources(this.cbFormat, "cbFormat");
            this.cbFormat.Items.AddRange(new object[] {
            resources.GetString("cbFormat.Items"),
            resources.GetString("cbFormat.Items1"),
            resources.GetString("cbFormat.Items2"),
            resources.GetString("cbFormat.Items3"),
            resources.GetString("cbFormat.Items4"),
            resources.GetString("cbFormat.Items5"),
            resources.GetString("cbFormat.Items6"),
            resources.GetString("cbFormat.Items7"),
            resources.GetString("cbFormat.Items8"),
            resources.GetString("cbFormat.Items9")});
            this.cbFormat.Name = "cbFormat";
            this.cbFormat.Validating += new System.ComponentModel.CancelEventHandler(this.cbHex16_Validating);
            this.cbFormat.Validated += new System.EventHandler(this.cbHex16_Validated);
            this.cbFormat.Enter += new System.EventHandler(this.cbHex16_Enter);
            this.cbFormat.SelectedIndexChanged += new System.EventHandler(this.cbHex16_SelectedIndexChanged);
            this.cbFormat.TextChanged += new System.EventHandler(this.cbHex16_TextChanged);
            // 
            // gbSpecial
            // 
            resources.ApplyResources(this.gbSpecial, "gbSpecial");
            this.gbSpecial.Controls.Add(this.btnAppend);
            this.gbSpecial.Controls.Add(this.btnInsTrue);
            this.gbSpecial.Controls.Add(this.btnInsFalse);
            this.gbSpecial.Controls.Add(this.btnDelPescado);
            this.gbSpecial.Controls.Add(this.btnLinkInge);
            this.gbSpecial.Controls.Add(this.btnGUIDIndex);
            this.gbSpecial.Controls.Add(this.btnDelMerola);
            this.gbSpecial.Controls.Add(this.btnListing);
            this.gbSpecial.Controls.Add(this.btnTPRPMaker);
            this.gbSpecial.Name = "gbSpecial";
            this.gbSpecial.TabStop = false;
            // 
            // btnAppend
            // 
            resources.ApplyResources(this.btnAppend, "btnAppend");
            this.btnAppend.Name = "btnAppend";
            this.btnAppend.Click += new System.EventHandler(this.btnAppend_Click);
            // 
            // btnInsTrue
            // 
            resources.ApplyResources(this.btnInsTrue, "btnInsTrue");
            this.btnInsTrue.Name = "btnInsTrue";
            this.btnInsTrue.Click += new System.EventHandler(this.btnInsVia_Click);
            // 
            // btnInsFalse
            // 
            resources.ApplyResources(this.btnInsFalse, "btnInsFalse");
            this.btnInsFalse.Name = "btnInsFalse";
            this.btnInsFalse.Click += new System.EventHandler(this.btnInsVia_Click);
            // 
            // btnDelPescado
            // 
            resources.ApplyResources(this.btnDelPescado, "btnDelPescado");
            this.btnDelPescado.Name = "btnDelPescado";
            this.btnDelPescado.Click += new System.EventHandler(this.btnDelPescado_Click);
            // 
            // btnLinkInge
            // 
            resources.ApplyResources(this.btnLinkInge, "btnLinkInge");
            this.btnLinkInge.Name = "btnLinkInge";
            this.btnLinkInge.Click += new System.EventHandler(this.btnLinkInge_Click);
            // 
            // btnGUIDIndex
            // 
            resources.ApplyResources(this.btnGUIDIndex, "btnGUIDIndex");
            this.btnGUIDIndex.Name = "btnGUIDIndex";
            this.btnGUIDIndex.Click += new System.EventHandler(this.btnGUIDIndex_Click);
            // 
            // btnDelMerola
            // 
            resources.ApplyResources(this.btnDelMerola, "btnDelMerola");
            this.btnDelMerola.Name = "btnDelMerola";
            this.btnDelMerola.Click += new System.EventHandler(this.btnDelMerola_Click);
            // 
            // btnListing
            // 
            resources.ApplyResources(this.btnListing, "btnListing");
            this.btnListing.Name = "btnListing";
            this.btnListing.Click += new System.EventHandler(this.btnListing_Click);
            // 
            // btnTPRPMaker
            // 
            resources.ApplyResources(this.btnTPRPMaker, "btnTPRPMaker");
            this.btnTPRPMaker.Name = "btnTPRPMaker";
            this.btnTPRPMaker.Click += new System.EventHandler(this.btnTPRPMaker_Click);
            // 
            // pnflowcontainer
            // 
            resources.ApplyResources(this.pnflowcontainer, "pnflowcontainer");
            this.pnflowcontainer.Name = "pnflowcontainer";
            this.pnflowcontainer.SelectedIndex = -1;
            this.pnflowcontainer.SelectedInstChanged += new System.EventHandler(this.pnflowcontainer_SelectedInstChanged);
            // 
            // btnDel
            // 
            resources.ApplyResources(this.btnDel, "btnDel");
            this.btnDel.Name = "btnDel";
            this.btnDel.Click += new System.EventHandler(this.btnDel_Clicked);
            // 
            // gbMove
            // 
            resources.ApplyResources(this.gbMove, "gbMove");
            this.gbMove.Controls.Add(this.btnUp);
            this.gbMove.Controls.Add(this.btnDown);
            this.gbMove.Controls.Add(this.lbUpDown);
            this.gbMove.Controls.Add(this.tbLines);
            this.gbMove.Name = "gbMove";
            this.gbMove.TabStop = false;
            // 
            // btnUp
            // 
            resources.ApplyResources(this.btnUp, "btnUp");
            this.btnUp.Name = "btnUp";
            this.btnUp.Click += new System.EventHandler(this.btnMove_Clicked);
            // 
            // btnDown
            // 
            resources.ApplyResources(this.btnDown, "btnDown");
            this.btnDown.Name = "btnDown";
            this.btnDown.Click += new System.EventHandler(this.btnMove_Clicked);
            // 
            // lbUpDown
            // 
            resources.ApplyResources(this.lbUpDown, "lbUpDown");
            this.lbUpDown.Name = "lbUpDown";
            // 
            // tbLines
            // 
            resources.ApplyResources(this.tbLines, "tbLines");
            this.tbLines.Name = "tbLines";
            this.tbLines.Validated += new System.EventHandler(this.hex16_Validated);
            this.tbLines.Validating += new System.ComponentModel.CancelEventHandler(this.hex16_Validating);
            this.tbLines.TextChanged += new System.EventHandler(this.hex16_TextChanged);
            // 
            // btnSort
            // 
            resources.ApplyResources(this.btnSort, "btnSort");
            this.btnSort.Name = "btnSort";
            this.btnSort.Click += new System.EventHandler(this.btnSort_Clicked);
            // 
            // btnCommit
            // 
            resources.ApplyResources(this.btnCommit, "btnCommit");
            this.btnCommit.Name = "btnCommit";
            this.btnCommit.Click += new System.EventHandler(this.btnCommit_Clicked);
            // 
            // tbTreeVersion
            // 
            resources.ApplyResources(this.tbTreeVersion, "tbTreeVersion");
            this.tbTreeVersion.Name = "tbTreeVersion";
            this.tbTreeVersion.Validated += new System.EventHandler(this.hex32_Validated);
            this.tbTreeVersion.Validating += new System.ComponentModel.CancelEventHandler(this.hex32_Validating);
            this.tbTreeVersion.TextChanged += new System.EventHandler(this.hex32_TextChanged);
            // 
            // btnAdd
            // 
            resources.ApplyResources(this.btnAdd, "btnAdd");
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Clicked);
            // 
            // lbCacheFlags
            // 
            resources.ApplyResources(this.lbCacheFlags, "lbCacheFlags");
            this.lbCacheFlags.Name = "lbCacheFlags";
            // 
            // cmenuGUIDIndex
            // 
            this.cmenuGUIDIndex.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createAllPackagesToolStripMenuItem,
            this.createCurrentPackageToolStripMenuItem,
            this.loadIndexToolStripMenuItem,
            this.saveIndexToolStripMenuItem});
            this.cmenuGUIDIndex.Name = "cmenuGUIDIndex";
            resources.ApplyResources(this.cmenuGUIDIndex, "cmenuGUIDIndex");
            // 
            // createAllPackagesToolStripMenuItem
            // 
            this.createAllPackagesToolStripMenuItem.Name = "createAllPackagesToolStripMenuItem";
            resources.ApplyResources(this.createAllPackagesToolStripMenuItem, "createAllPackagesToolStripMenuItem");
            this.createAllPackagesToolStripMenuItem.Click += new System.EventHandler(this.createToolStripMenuItem_Click);
            // 
            // createCurrentPackageToolStripMenuItem
            // 
            this.createCurrentPackageToolStripMenuItem.Name = "createCurrentPackageToolStripMenuItem";
            resources.ApplyResources(this.createCurrentPackageToolStripMenuItem, "createCurrentPackageToolStripMenuItem");
            this.createCurrentPackageToolStripMenuItem.Click += new System.EventHandler(this.createToolStripMenuItem_Click);
            // 
            // loadIndexToolStripMenuItem
            // 
            this.loadIndexToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.defaultFileToolStripMenuItem,
            this.fromFileToolStripMenuItem});
            this.loadIndexToolStripMenuItem.Name = "loadIndexToolStripMenuItem";
            resources.ApplyResources(this.loadIndexToolStripMenuItem, "loadIndexToolStripMenuItem");
            // 
            // defaultFileToolStripMenuItem
            // 
            this.defaultFileToolStripMenuItem.Name = "defaultFileToolStripMenuItem";
            resources.ApplyResources(this.defaultFileToolStripMenuItem, "defaultFileToolStripMenuItem");
            this.defaultFileToolStripMenuItem.Click += new System.EventHandler(this.defaultFileToolStripMenuItem_Click);
            // 
            // fromFileToolStripMenuItem
            // 
            this.fromFileToolStripMenuItem.Name = "fromFileToolStripMenuItem";
            resources.ApplyResources(this.fromFileToolStripMenuItem, "fromFileToolStripMenuItem");
            this.fromFileToolStripMenuItem.Click += new System.EventHandler(this.fileToolStripMenuItem_Click);
            // 
            // saveIndexToolStripMenuItem
            // 
            this.saveIndexToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.defaultFileToolStripMenuItem1,
            this.toFileToolStripMenuItem});
            this.saveIndexToolStripMenuItem.Name = "saveIndexToolStripMenuItem";
            resources.ApplyResources(this.saveIndexToolStripMenuItem, "saveIndexToolStripMenuItem");
            // 
            // defaultFileToolStripMenuItem1
            // 
            this.defaultFileToolStripMenuItem1.Name = "defaultFileToolStripMenuItem1";
            resources.ApplyResources(this.defaultFileToolStripMenuItem1, "defaultFileToolStripMenuItem1");
            this.defaultFileToolStripMenuItem1.Click += new System.EventHandler(this.defaultFileToolStripMenuItem_Click);
            // 
            // toFileToolStripMenuItem
            // 
            this.toFileToolStripMenuItem.Name = "toFileToolStripMenuItem";
            resources.ApplyResources(this.toFileToolStripMenuItem, "toFileToolStripMenuItem");
            this.toFileToolStripMenuItem.Click += new System.EventHandler(this.fileToolStripMenuItem_Click);
            // 
            // BhavForm
            // 
            resources.ApplyResources(this, "$this");
            this.CancelButton = this.btnClose;
            this.Controls.Add(this.bhavPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "BhavForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.gbInstruction.ResumeLayout(false);
            this.gbInstruction.PerformLayout();
            this.pnHeading.ResumeLayout(false);
            this.pnHeading.PerformLayout();
            this.bhavPanel.ResumeLayout(false);
            this.bhavPanel.PerformLayout();
            this.gbSpecial.ResumeLayout(false);
            this.gbMove.ResumeLayout(false);
            this.gbMove.PerformLayout();
            this.cmenuGUIDIndex.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private void pnflowcontainer_SelectedInstChanged(object sender, System.EventArgs e)
		{
			int index = pnflowcontainer.SelectedIndex;
			if (index < 0 || index >= wrapper.Count)
			{
				currentInst = null;
				origInst = null;
			}
			else
			{
				currentInst = wrapper[index];
				origInst = wrapper[index].Clone();
			}
			UpdateInstPanel();
			this.btnCancel.Enabled = false;
		}


		private void ItemQueryContinueDragTarget(object sender, QueryContinueDragEventArgs e)
		{
			if (e.KeyState==0) e.Action = DragAction.Drop;
			else e.Action = DragAction.Continue;
		}

		private void ItemDragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(typeof(int))) 
			{
				e.Effect = DragDropEffects.Link;		
			}
			else 
			{
				e.Effect = DragDropEffects.None;
			}					
		}

		private void ItemDrop(object sender, System.Windows.Forms.DragEventArgs e)
		{
			int sel = 0;
			sel = (int)e.Data.GetData(sel.GetType());
			ComboBox cb = ((ComboBox)sender);
			cb.SelectedIndex = -1;
			cb.Text = "0x"+Helper.HexString((ushort)sel);
		}


		private void btnCommit_Clicked(object sender, System.EventArgs e)
		{
			try 
			{
				wrapper.SynchronizeUserData();
				btnCommit.Enabled = wrapper.Changed;
				pnflowcontainer_SelectedInstChanged(null, null);
			} 
			catch (Exception ex) 
			{
				Helper.ExceptionMessage(pjse.Localization.GetString("errwritingfile"), ex);
			}			
		}


		private void btnCancel_Clicked(object sender, System.EventArgs e)
		{
			wrapper[pnflowcontainer.SelectedIndex] = origInst.Clone();
			pnflowcontainer_SelectedInstChanged(null, null);
		}


		private void btnHelp_Click(object sender, System.EventArgs e)
		{
            pjse.HelpHelper.Help("Contents");
		}



		private void llopenbhav_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
            pjse.FileTable.Entry item = wrapper.ResourceByInstance(SimPe.Data.MetaData.BHAV_FILE, currentInst.Instruction.OpCode);
            if (item == null) return; // this should never happen
            Bhav bhav = new Bhav();
            bhav.ProcessData(item.PFD, item.Package);

            BhavForm ui = (BhavForm)bhav.UIHandler;
			ui.Tag = "Popup"; // tells the SetReadOnly function it's in a popup - so everything locked down
			ui.Text = 
                pjse.Localization.GetString("viewbhav") + ": " + currentInst.ShortName + " [" + bhav.Package.SaveFileName + "]";
			bhav.RefreshUI();
			ui.Show();
		}

		private void btnClose_Click(object sender, System.EventArgs e)
		{
			Close();
		}


		private void btnOpCode_Clicked(object sender, System.EventArgs e)
		{
			pjse.FileTable.Entry item = new ResourceChooser().Execute(SimPe.Data.MetaData.BHAV_FILE, wrapper.FileDescriptor.Group, bhavPanel.Parent);

			if (item != null && item.Instance != currentInst.Instruction.OpCode)
				this.tbInst_OpCode.Text = "0x" + SimPe.Helper.HexString((ushort)item.Instance);
		}

		private void btnOperandWiz_Clicked(object sender, System.EventArgs e)
		{
			internalchg = true;
			if ((new BhavOperandWiz()).Execute(currentInst, 1) != null)
				UpdateInstPanel();
			internalchg = false;
		}
		
		private void btnOperandRaw_Click(object sender, System.EventArgs e)
		{
			internalchg = true;
			if ((new BhavOperandWiz()).Execute(btnCommit.Visible ? currentInst : (BhavWiz)(currentInst.Instruction.Clone()), 0) != null)
				UpdateInstPanel();
			internalchg = false;
		}


		private void tbFilename_TextChanged(object sender, System.EventArgs e)
		{
			wrapper.FileName = tbFilename.Text;
		}

		private void tbFilename_Validated(object sender, System.EventArgs e)
		{
			tbFilename.SelectAll();
		}


		private void cbHex16_Enter(object sender, System.EventArgs e)
		{
			((ComboBox)sender).SelectAll();
		}

		private void cbHex16_TextChanged(object sender, System.EventArgs ev)
		{
			if (internalchg) return;
			if (!cbHex16_IsValid(sender)) return;
			if (((ComboBox)sender).Items.IndexOf(((ComboBox)sender).Text) != -1) return;

			ushort val = Convert.ToUInt16(((ComboBox)sender).Text, 16);
			internalchg = true;
			switch (alHex16cb.IndexOf(sender))
			{
				case 0: currentInst.Instruction.Target1 = val; break;
				case 1: currentInst.Instruction.Target2 = val; break;
				case 2: wrapper.Header.Format = val; break;
			}
			internalchg = false;
		}

		private void cbHex16_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (cbHex16_IsValid(sender)) return;

			int i = alHex16cb.IndexOf(sender);
			if (i < 0)
				throw new Exception("cbHex16_Validating not applicable to control " + sender.ToString());

			e.Cancel = true;

			ushort val = 0;
			switch (i)
			{
				case 0: val = origInst.Target1; break;
				case 1: val = origInst.Target2; break;
				case 2: val = wrapper.Header.Format; break;
			}

			if (i < 2 && val >= 0xfffc && val <= 0xfffe)
			{
				((ComboBox)sender).SelectedIndex = val - 0xfffc;
			}
			else if (i == 2 && val >= 0x8000 && val <= 0x8007)
			{
				((ComboBox)sender).SelectedIndex = val - 0x8000;
			}
			else
			{
				((ComboBox)sender).SelectedIndex = -1;
				((ComboBox)sender).Text = "0x" + Helper.HexString(val);
			}
			((ComboBox)sender).SelectAll();
		}

		private void cbHex16_Validated(object sender, System.EventArgs e)
		{
			int i = alHex16cb.IndexOf(sender);
			if (i < 0)
				throw new Exception("cbHex16_Validated not applicable to control " + sender.ToString());
			if (((ComboBox)sender).Items.IndexOf(((ComboBox)sender).Text) != -1) return;

			ushort val = Convert.ToUInt16(((ComboBox)sender).Text, 16);

			bool origstate = internalchg;
			internalchg = true;
			if (i < 2 && val >= 0xfffc && val <= 0xfffe)
			{
				((ComboBox)sender).SelectedIndex = val - 0xfffc;
			}
			else if (i == 2 && val >= 0x8000 && val <= 0x8007)
			{
				((ComboBox)sender).SelectedIndex = val - 0x8000;
			}
			else
			{
				((ComboBox)sender).SelectedIndex = -1;
				((ComboBox)sender).Text = "0x" + Helper.HexString(val);
			}
			internalchg = origstate;
			((ComboBox)sender).Select(0, 0);
		}

		private void cbHex16_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (internalchg) return;

			int i = alHex16cb.IndexOf(sender);
			if (i < 0)
				throw new Exception("cbHex16_SelectedIndexChanged not applicable to control " + sender.ToString());
			if (((ComboBox)sender).SelectedIndex == -1) return;

			ushort val = (ushort)((ComboBox)alHex16cb[i]).SelectedIndex;
			((ComboBox)sender).SelectAll();

			internalchg = true;
			if (i < 2)
			{
				val += 0xFFFC;
				if (i == 0) currentInst.Instruction.Target1 = val;
				else        currentInst.Instruction.Target2 = val;
			}
			else
			{
				val += 0x8000;
				wrapper.Header.Format = val;
			}
			internalchg = false;
		}


		private void dec8_TextChanged(object sender, System.EventArgs ev)
		{
			if (internalchg) return;
			if (!dec8_IsValid(sender)) return;

			byte val = Convert.ToByte(((TextBox)sender).Text);
			internalchg = true;
			switch (alDec8.IndexOf(sender))
			{
				default: break;
			}
			internalchg = false;
		}

		private void dec8_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (dec8_IsValid(sender)) return;

			e.Cancel = true;

			byte val = 0;
			switch (alDec8.IndexOf(sender))
			{
				default: break;
			}

			((TextBox)sender).Text = val.ToString();
			((TextBox)sender).SelectAll();
        }


#if DEC16
		private void dec16_TextChanged(object sender, System.EventArgs ev)
		{
			if (internalchg) return;
			if (!dec16_IsValid(sender)) return;

			byte[] ops = ShortToOps(Convert.ToInt16(((TextBox)sender).Text));
			internalchg = true;
			switch (alDec16.IndexOf(sender))
			{
				case 0:
					currentInst.Instruction.Operands[0] = ops[0];
					currentInst.Instruction.Operands[1] = ops[1];
					this.tbInst_Op0.Text = Helper.HexString(currentInst.Instruction.Operands[0]);
					this.tbInst_Op1.Text = Helper.HexString(currentInst.Instruction.Operands[1]);
					break;
				case 1:
					currentInst.Instruction.Operands[2] = ops[0];
					currentInst.Instruction.Operands[3] = ops[1];
					this.tbInst_Op2.Text = Helper.HexString(currentInst.Instruction.Operands[2]);
					this.tbInst_Op3.Text = Helper.HexString(currentInst.Instruction.Operands[3]);
					break;
			}
			internalchg = false;
		}

		private void dec16_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (dec16_IsValid(sender)) return;

			e.Cancel = true;

			short val = 0;
			switch (alDec16.IndexOf(sender))
			{
				case 0: val = OpsToShort(origInst.Operands[0], origInst.Operands[1]); break;
				case 1: val = OpsToShort(origInst.Operands[2], origInst.Operands[3]); break;
				case 2: val = 1; break; // Move
			}

			((TextBox)sender).Text = val.ToString();
			((TextBox)sender).SelectAll();
		}

		private void dec16_Validated(object sender, System.EventArgs e)
		{
			((TextBox)sender).SelectAll();
		}
#endif


        private void hex8_TextChanged(object sender, System.EventArgs ev)
		{
			if (internalchg) return;
			if (!hex8_IsValid(sender)) return;

			byte val = Convert.ToByte(((TextBox)sender).Text, 16);
			int i = alHex8.IndexOf(sender);

			internalchg = true;

			if (i < 8) currentInst.Instruction.Operands[i] = val;
			else if (i < 16) currentInst.Instruction.Reserved1[i-8] = val;
			else switch(i)
				 {
					 case 16: currentInst.Instruction.NodeVersion = val; break;
					 case 17: wrapper.Header.HeaderFlag = val; break;
					 case 18: wrapper.Header.Type = val; break;
					 case 19: wrapper.Header.CacheFlags = val; break;
					 case 20: wrapper.Header.ArgumentCount = val; break;
					 case 21: wrapper.Header.LocalVarCount = val; break;
				 }

			internalchg = false;
		}

		private void hex8_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (hex8_IsValid(sender)) return;

			e.Cancel = true;

			byte val = 0;
			int i = alHex8.IndexOf(sender);

			if (i < 8) val = origInst.Operands[i];
			else if (i < 16) val = origInst.Reserved1[i-8];
			else switch(i)
				 {
					 case 16: val = origInst.NodeVersion; break;
					 case 17: val = wrapper.Header.HeaderFlag; break;
					 case 18: val = wrapper.Header.Type; break;
					 case 19: val = wrapper.Header.CacheFlags; break;
					 case 20: val = wrapper.Header.ArgumentCount; break;
					 case 21: val = wrapper.Header.LocalVarCount; break;
				 }

			((TextBox)sender).Text = ((i >= 16) ? "0x" : "") + Helper.HexString(val);
			((TextBox)sender).SelectAll();
		}

		private void hex8_Validated(object sender, System.EventArgs e)
		{
			bool origstate = internalchg;
			internalchg = true;
			((TextBox)sender).Text = ((alHex8.IndexOf(sender) >= 16) ? "0x" : "") + Helper.HexString(Convert.ToByte(((TextBox)sender).Text, 16));
			((TextBox)sender).SelectAll();
			internalchg = origstate;
		}


		private void hex16_TextChanged(object sender, System.EventArgs ev)
		{
			if (internalchg) return;
			if (!hex16_IsValid(sender)) return;

			ushort val = Convert.ToUInt16(((TextBox)sender).Text, 16);
			internalchg = true;
			switch (alHex16.IndexOf(sender))
			{
				case 0: currentInst.Instruction.OpCode = val; break;
			}
			internalchg = false;
		}

		private void hex16_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (hex16_IsValid(sender)) return;

			e.Cancel = true;

			ushort val = 0;
			switch (alHex16.IndexOf(sender))
			{
				case 0: val = origInst.OpCode; break;
                case 1: val = 1; break;
			}

			((TextBox)sender).Text = "0x" + Helper.HexString(val);
			((TextBox)sender).SelectAll();
		}

		private void hex16_Validated(object sender, System.EventArgs e)
		{
			bool origstate = internalchg;
			internalchg = true;
			((TextBox)sender).Text = "0x" + Helper.HexString(Convert.ToUInt16(((TextBox)sender).Text, 16));
			((TextBox)sender).SelectAll();
			internalchg = origstate;
		}


		private void hex32_TextChanged(object sender, System.EventArgs ev)
		{
			if (internalchg) return;
			if (!hex32_IsValid(sender)) return;

			uint val = Convert.ToUInt32(((TextBox)sender).Text, 16);
			internalchg = true;
			switch (alHex32.IndexOf(sender))
			{
				case 0: wrapper.Header.TreeVersion = val; break;
			}
			internalchg = false;
		}

		private void hex32_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (hex32_IsValid(sender)) return;

			e.Cancel = true;

			uint val = 0;
			switch (alHex32.IndexOf(sender))
			{
				case 0: val = wrapper.Header.TreeVersion; break;
			}

			((TextBox)sender).Text = "0x" + Helper.HexString(val);
			((TextBox)sender).SelectAll();
		}

		private void hex32_Validated(object sender, System.EventArgs e)
		{
			bool origstate = internalchg;
			internalchg = true;
			((TextBox)sender).Text = "0x" + Helper.HexString(Convert.ToUInt32(((TextBox)sender).Text, 16));
			((TextBox)sender).SelectAll();
			internalchg = origstate;
		}


		private void btnSort_Clicked(object sender, System.EventArgs e)
		{
			this.pnflowcontainer.Sort();
		}

		private void btnMove_Clicked(object sender, System.EventArgs e)
		{
			int mv;
			try { mv = Convert.ToInt32(tbLines.Text, 16); }
			catch (Exception) { return; }
            try
            {
                this.gbMove.Enabled = false;
                if (sender == this.btnUp)
                    this.pnflowcontainer.MoveInst(mv * -1);
                else
                    this.pnflowcontainer.MoveInst(mv);
            }
            finally
            {
                this.gbMove.Enabled = true;
            }
        }

		private void btnAdd_Clicked(object sender, System.EventArgs e)
		{
			this.pnflowcontainer.Add(BhavUIAddType.Default);
		}

		private void btnDel_Clicked(object sender, System.EventArgs e)
		{
			this.pnflowcontainer.Delete(BhavUIDeleteType.Default);
		}


		private void cbSpecial_CheckStateChanged(object sender, System.EventArgs e)
		{
			gbSpecial.Visible =
				this.ShowSpecialButtons = ((CheckBox)sender).Checked;
		}


		private void btnInsVia_Click(object sender, System.EventArgs e)
		{
			this.pnflowcontainer.Add( (sender == this.btnInsTrue) ? BhavUIAddType.ViaTrue : BhavUIAddType.ViaFalse );
		}

		private void btnDelPescado_Click(object sender, System.EventArgs e)
		{
			this.pnflowcontainer.Delete(BhavUIDeleteType.Pescado);
		}

		private void btnLinkInge_Click(object sender, System.EventArgs e)
		{
			this.pnflowcontainer.Relink();
		}

		private void btnAppend_Click(object sender, System.EventArgs e)
		{
			this.pnflowcontainer.Append(new ResourceChooser().Execute(SimPe.Data.MetaData.BHAV_FILE, wrapper.FileDescriptor.Group, bhavPanel.Parent));
		}

		private void btnDelMerola_Click(object sender, System.EventArgs e)
		{
			this.pnflowcontainer.DeleteUnlinked();
		}

		private void btnListing_Click(object sender, System.EventArgs e)
		{
			this.CopyListing();
		}

		private void btnTPRPMaker_Click(object sender, System.EventArgs e)
		{
			this.TPRPMaker();
		}

        private void btnRefreshFT_Click(object sender, EventArgs e)
        {
            pjse.FileTable.GFT.UIRefresh();
        }

        private void btnGUIDIndex_Click(object sender, EventArgs e)
        {
            this.cmenuGUIDIndex.Show((Control)sender, new Point(3 ,3));
        }

        private void createToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SimPe.Wait.Start();
            SimPe.RemoteControl.ApplicationForm.Cursor = Cursors.WaitCursor;
            pjse.GUIDIndex.TheGUIDIndex.Create(sender.Equals(this.createCurrentPackageToolStripMenuItem));
            SimPe.RemoteControl.ApplicationForm.Cursor = Cursors.Default;
            SimPe.Wait.Stop();
        }

        private void defaultFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sender.Equals(this.defaultFileToolStripMenuItem))
                pjse.GUIDIndex.TheGUIDIndex.Load();
            else
                pjse.GUIDIndex.TheGUIDIndex.Save();
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileDialog fd;
            bool load;
            if (load = sender.Equals(this.fromFileToolStripMenuItem))
                fd = new OpenFileDialog();
            else
                fd = new SaveFileDialog();
            fd.AddExtension = true;
            fd.CheckFileExists = load;
            fd.CheckPathExists = true;
            fd.DefaultExt = "txt";
            fd.DereferenceLinks = true;
            fd.FileName = "guidindex.txt";
            fd.Filter = pjse.Localization.GetString("guidFilter");
            fd.FilterIndex = 1;
            fd.RestoreDirectory = false;
            fd.ShowHelp = false;
            fd.SupportMultiDottedExtensions = false;
            fd.Title = load
                ? pjse.Localization.GetString("guidLoadIndex")
                : pjse.Localization.GetString("guidSaveIndex");
            fd.ValidateNames = true;
            DialogResult dr = fd.ShowDialog();
            if (dr == DialogResult.OK)
            {
                if (load)
                    pjse.GUIDIndex.TheGUIDIndex.Load(fd.FileName);
                else
                    pjse.GUIDIndex.TheGUIDIndex.Save(fd.FileName);
            }
        }

	}
}
