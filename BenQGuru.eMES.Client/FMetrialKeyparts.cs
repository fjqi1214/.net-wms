using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Domain.Material;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Client.Service;

using UserControl;

namespace BenQGuru.eMES.Client
{
    public class FKeyParts : Form
    {
        //允许外部访问MO输入框
        private string moCode;
        public string MOCode
        {
            get
            {
                return moCode;
            }
            set
            {
                moCode = value;
            }
        }

        #region 自动生成

        private System.Windows.Forms.GroupBox grpQuery;
        private System.Windows.Forms.Panel panelButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private UserControl.UCButton ucBtnSave;
        private UserControl.UCButton ucBtnDelete;
        private UserControl.UCButton ucBtnExit;
        private UserControl.UCButton ucBtnUpdate;
        private UserControl.UCButton ucBtnAdd;
        private UserControl.UCLabelEdit ucLERCStart;
        private UserControl.UCLabelEdit ucLERCEnd;
        private UserControl.UCLabelEdit ucLEBIOS;
        private UserControl.UCLabelEdit ucLEVendorItem;
        private UserControl.UCLabelEdit ucLEDateCode;
        private UserControl.UCLabelEdit ucLEVersion;
        private UserControl.UCLabelEdit ucLEVendorCode;
        private UserControl.UCLabelEdit ucLELotNo;
        private UserControl.UCButton ucBtnQuery;
        private UserControl.UCLabelEdit ucLEItemCodeQuery;
        private System.Data.DataTable dtMKeyparts;
        private System.Data.DataColumn Sequence;
        private System.Data.DataColumn RunningCardPrefix;
        private System.Data.DataColumn RunningCardStart;
        private System.Data.DataColumn RunningCardEnd;
        private System.Data.DataColumn SNScale;
        private System.Data.DataColumn ItemCode;
        private System.Data.DataColumn LotNO;
        private System.Data.DataColumn VendorCode;
        private System.Data.DataColumn VendorItemCode;
        private System.Data.DataColumn DateCode;
        private System.Data.DataColumn Version;
        private System.Data.DataColumn BIOSVersion;
        private System.Data.DataColumn PCBAVersion;
        private System.Data.DataColumn MaintainUser;
        private System.Data.DataSet dsMKeyparts;
        private System.Data.DataColumn MaintainDate;
        private System.Data.DataColumn MaintainTime;
        private UserControl.UCLabelEdit ucLEPCBA;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGridMain;
        private UserControl.UCLabelEdit ucLEItemCode;
        private System.ComponentModel.IContainer components = null;

        private UserControl.UCDatetTime ucDate;
        private System.Windows.Forms.CheckBox chkNeedDate;
        private UserControl.UCLabelEdit ucMoCode;
        private UserControl.UCLabelEdit ucByMo;
        private System.Data.DataColumn MoCode;
        private UserControl.UCButton btnParseSN;
        private System.Windows.Forms.Label lblMitemName;
        private System.Data.DataColumn MITEMNAME;
        private UserControl.UCButton btnLockControl;
        private UserControl.UCLabelEdit txtSumNum;
        private UserControl.UCLabelEdit bRCardLenULE;
        private UCLabelEdit ucLabelEditRCardPrefix;
        public Infragistics.Win.UltraWinEditors.UltraOptionSet ultraOptionSetScale;
        private System.Windows.Forms.Label label1;
        private UCButton ucButtonCalcCount;
        private UCLabelEdit ucLabelEditSNCount;
        private UCLabelEdit ucLabelEditRCardQuery;
        private UCButton ucButtonCalcRCardEnd;
        private UCButton ucButtonCancel;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #endregion

        #region 设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要使用代码编辑器修改
        /// 此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FKeyParts));
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.ValueListItem valueListItem1 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.ValueListItem valueListItem2 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.ValueListItem valueListItem3 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("dtMKeyparts", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Sequence");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("RunningCardPrefix");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("RunningCardStart");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("RunningCardEnd");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SNScale");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ItemCode");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn7 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("LotNO");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn8 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("VendorCode");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn9 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("VendorItemCode");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn10 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("DateCode");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn11 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Version");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn12 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("BIOSVersion");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn13 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PCBAVersion");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn14 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MaintainUser");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn15 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MaintainDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn16 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MaintainTime");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn17 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MoCode");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn18 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MITEMNAME");
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            this.dtMKeyparts = new System.Data.DataTable();
            this.Sequence = new System.Data.DataColumn();
            this.RunningCardPrefix = new System.Data.DataColumn();
            this.RunningCardStart = new System.Data.DataColumn();
            this.RunningCardEnd = new System.Data.DataColumn();
            this.SNScale = new System.Data.DataColumn();
            this.ItemCode = new System.Data.DataColumn();
            this.LotNO = new System.Data.DataColumn();
            this.VendorCode = new System.Data.DataColumn();
            this.VendorItemCode = new System.Data.DataColumn();
            this.DateCode = new System.Data.DataColumn();
            this.Version = new System.Data.DataColumn();
            this.BIOSVersion = new System.Data.DataColumn();
            this.PCBAVersion = new System.Data.DataColumn();
            this.MaintainUser = new System.Data.DataColumn();
            this.MaintainDate = new System.Data.DataColumn();
            this.MaintainTime = new System.Data.DataColumn();
            this.MoCode = new System.Data.DataColumn();
            this.MITEMNAME = new System.Data.DataColumn();
            this.dsMKeyparts = new System.Data.DataSet();
            this.grpQuery = new System.Windows.Forms.GroupBox();
            this.ucLabelEditRCardQuery = new UserControl.UCLabelEdit();
            this.ucMoCode = new UserControl.UCLabelEdit();
            this.chkNeedDate = new System.Windows.Forms.CheckBox();
            this.ucDate = new UserControl.UCDatetTime();
            this.ucBtnQuery = new UserControl.UCButton();
            this.ucLEItemCodeQuery = new UserControl.UCLabelEdit();
            this.panelButton = new System.Windows.Forms.Panel();
            this.ucButtonCancel = new UserControl.UCButton();
            this.ucBtnSave = new UserControl.UCButton();
            this.ucBtnDelete = new UserControl.UCButton();
            this.ucBtnExit = new UserControl.UCButton();
            this.ucBtnUpdate = new UserControl.UCButton();
            this.ucBtnAdd = new UserControl.UCButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ucButtonCalcRCardEnd = new UserControl.UCButton();
            this.ucLabelEditSNCount = new UserControl.UCLabelEdit();
            this.ucButtonCalcCount = new UserControl.UCButton();
            this.label1 = new System.Windows.Forms.Label();
            this.ultraOptionSetScale = new Infragistics.Win.UltraWinEditors.UltraOptionSet();
            this.ucLabelEditRCardPrefix = new UserControl.UCLabelEdit();
            this.bRCardLenULE = new UserControl.UCLabelEdit();
            this.txtSumNum = new UserControl.UCLabelEdit();
            this.btnLockControl = new UserControl.UCButton();
            this.lblMitemName = new System.Windows.Forms.Label();
            this.ucByMo = new UserControl.UCLabelEdit();
            this.ucLEItemCode = new UserControl.UCLabelEdit();
            this.ucLERCStart = new UserControl.UCLabelEdit();
            this.ucLERCEnd = new UserControl.UCLabelEdit();
            this.ucLEBIOS = new UserControl.UCLabelEdit();
            this.ucLEPCBA = new UserControl.UCLabelEdit();
            this.ucLEVendorItem = new UserControl.UCLabelEdit();
            this.ucLEDateCode = new UserControl.UCLabelEdit();
            this.ucLEVersion = new UserControl.UCLabelEdit();
            this.ucLEVendorCode = new UserControl.UCLabelEdit();
            this.ucLELotNo = new UserControl.UCLabelEdit();
            this.btnParseSN = new UserControl.UCButton();
            this.ultraGridMain = new Infragistics.Win.UltraWinGrid.UltraGrid();
            ((System.ComponentModel.ISupportInitialize)(this.dtMKeyparts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsMKeyparts)).BeginInit();
            this.grpQuery.SuspendLayout();
            this.panelButton.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraOptionSetScale)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridMain)).BeginInit();
            this.SuspendLayout();
            // 
            // dtMKeyparts
            // 
            this.dtMKeyparts.Columns.AddRange(new System.Data.DataColumn[] {
            this.Sequence,
            this.RunningCardPrefix,
            this.RunningCardStart,
            this.RunningCardEnd,
            this.SNScale,
            this.ItemCode,
            this.LotNO,
            this.VendorCode,
            this.VendorItemCode,
            this.DateCode,
            this.Version,
            this.BIOSVersion,
            this.PCBAVersion,
            this.MaintainUser,
            this.MaintainDate,
            this.MaintainTime,
            this.MoCode,
            this.MITEMNAME});
            this.dtMKeyparts.TableName = "dtMKeyparts";
            // 
            // Sequence
            // 
            this.Sequence.ColumnName = "Sequence";
            // 
            // RunningCardPrefix
            // 
            this.RunningCardPrefix.ColumnName = "RunningCardPrefix";
            // 
            // RunningCardStart
            // 
            this.RunningCardStart.ColumnName = "RunningCardStart";
            // 
            // RunningCardEnd
            // 
            this.RunningCardEnd.ColumnName = "RunningCardEnd";
            // 
            // SNScale
            // 
            this.SNScale.ColumnName = "SNScale";
            // 
            // ItemCode
            // 
            this.ItemCode.ColumnName = "ItemCode";
            // 
            // LotNO
            // 
            this.LotNO.ColumnName = "LotNO";
            // 
            // VendorCode
            // 
            this.VendorCode.ColumnName = "VendorCode";
            // 
            // VendorItemCode
            // 
            this.VendorItemCode.ColumnName = "VendorItemCode";
            // 
            // DateCode
            // 
            this.DateCode.ColumnName = "DateCode";
            // 
            // Version
            // 
            this.Version.ColumnName = "Version";
            // 
            // BIOSVersion
            // 
            this.BIOSVersion.Caption = "BIOSVersion";
            this.BIOSVersion.ColumnName = "BIOSVersion";
            // 
            // PCBAVersion
            // 
            this.PCBAVersion.ColumnName = "PCBAVersion";
            // 
            // MaintainUser
            // 
            this.MaintainUser.ColumnName = "MaintainUser";
            // 
            // MaintainDate
            // 
            this.MaintainDate.ColumnName = "MaintainDate";
            // 
            // MaintainTime
            // 
            this.MaintainTime.ColumnName = "MaintainTime";
            // 
            // MoCode
            // 
            this.MoCode.Caption = "MoCode";
            this.MoCode.ColumnName = "MoCode";
            // 
            // MITEMNAME
            // 
            this.MITEMNAME.ColumnName = "MITEMNAME";
            // 
            // dsMKeyparts
            // 
            this.dsMKeyparts.DataSetName = "dsMKeyparts";
            this.dsMKeyparts.Locale = new System.Globalization.CultureInfo("zh-CN");
            this.dsMKeyparts.Tables.AddRange(new System.Data.DataTable[] {
            this.dtMKeyparts});
            // 
            // grpQuery
            // 
            this.grpQuery.Controls.Add(this.ucLabelEditRCardQuery);
            this.grpQuery.Controls.Add(this.ucMoCode);
            this.grpQuery.Controls.Add(this.chkNeedDate);
            this.grpQuery.Controls.Add(this.ucDate);
            this.grpQuery.Controls.Add(this.ucBtnQuery);
            this.grpQuery.Controls.Add(this.ucLEItemCodeQuery);
            this.grpQuery.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpQuery.Location = new System.Drawing.Point(0, 0);
            this.grpQuery.Name = "grpQuery";
            this.grpQuery.Size = new System.Drawing.Size(816, 48);
            this.grpQuery.TabIndex = 0;
            this.grpQuery.TabStop = false;
            // 
            // ucLabelEditRCardQuery
            // 
            this.ucLabelEditRCardQuery.AllowEditOnlyChecked = true;
            this.ucLabelEditRCardQuery.Caption = "序列号";
            this.ucLabelEditRCardQuery.Checked = false;
            this.ucLabelEditRCardQuery.EditType = UserControl.EditTypes.String;
            this.ucLabelEditRCardQuery.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ucLabelEditRCardQuery.Location = new System.Drawing.Point(374, 16);
            this.ucLabelEditRCardQuery.MaxLength = 40;
            this.ucLabelEditRCardQuery.Multiline = false;
            this.ucLabelEditRCardQuery.Name = "ucLabelEditRCardQuery";
            this.ucLabelEditRCardQuery.PasswordChar = '\0';
            this.ucLabelEditRCardQuery.ReadOnly = false;
            this.ucLabelEditRCardQuery.ShowCheckBox = false;
            this.ucLabelEditRCardQuery.Size = new System.Drawing.Size(182, 24);
            this.ucLabelEditRCardQuery.TabIndex = 2;
            this.ucLabelEditRCardQuery.TabNext = true;
            this.ucLabelEditRCardQuery.Value = "";
            this.ucLabelEditRCardQuery.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabelEditRCardQuery.XAlign = 423;
            // 
            // ucMoCode
            // 
            this.ucMoCode.AllowEditOnlyChecked = true;
            this.ucMoCode.Caption = "工单号";
            this.ucMoCode.Checked = false;
            this.ucMoCode.EditType = UserControl.EditTypes.String;
            this.ucMoCode.Location = new System.Drawing.Point(183, 16);
            this.ucMoCode.MaxLength = 40;
            this.ucMoCode.Multiline = false;
            this.ucMoCode.Name = "ucMoCode";
            this.ucMoCode.PasswordChar = '\0';
            this.ucMoCode.ReadOnly = false;
            this.ucMoCode.ShowCheckBox = false;
            this.ucMoCode.Size = new System.Drawing.Size(182, 24);
            this.ucMoCode.TabIndex = 1;
            this.ucMoCode.TabNext = true;
            this.ucMoCode.Value = "";
            this.ucMoCode.WidthType = UserControl.WidthTypes.Normal;
            this.ucMoCode.XAlign = 232;
            // 
            // chkNeedDate
            // 
            this.chkNeedDate.Checked = true;
            this.chkNeedDate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkNeedDate.Location = new System.Drawing.Point(570, 16);
            this.chkNeedDate.Name = "chkNeedDate";
            this.chkNeedDate.Size = new System.Drawing.Size(16, 24);
            this.chkNeedDate.TabIndex = 3;
            this.chkNeedDate.CheckedChanged += new System.EventHandler(this.chkNeedDate_CheckedChanged);
            // 
            // ucDate
            // 
            this.ucDate.Caption = "日期";
            this.ucDate.Enabled = false;
            this.ucDate.Location = new System.Drawing.Point(582, 16);
            this.ucDate.Name = "ucDate";
            this.ucDate.ShowType = UserControl.DateTimeTypes.Date;
            this.ucDate.Size = new System.Drawing.Size(125, 21);
            this.ucDate.TabIndex = 3;
            this.ucDate.Value = new System.DateTime(2005, 12, 14, 0, 0, 0, 0);
            this.ucDate.XAlign = 621;
            // 
            // ucBtnQuery
            // 
            this.ucBtnQuery.BackColor = System.Drawing.SystemColors.Control;
            this.ucBtnQuery.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucBtnQuery.BackgroundImage")));
            this.ucBtnQuery.ButtonType = UserControl.ButtonTypes.Query;
            this.ucBtnQuery.Caption = "查询";
            this.ucBtnQuery.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnQuery.Location = new System.Drawing.Point(721, 16);
            this.ucBtnQuery.Name = "ucBtnQuery";
            this.ucBtnQuery.Size = new System.Drawing.Size(88, 22);
            this.ucBtnQuery.TabIndex = 4;
            this.ucBtnQuery.Click += new System.EventHandler(this.ucBtnQuery_Click);
            // 
            // ucLEItemCodeQuery
            // 
            this.ucLEItemCodeQuery.AllowEditOnlyChecked = true;
            this.ucLEItemCodeQuery.Caption = "料号";
            this.ucLEItemCodeQuery.Checked = false;
            this.ucLEItemCodeQuery.EditType = UserControl.EditTypes.String;
            this.ucLEItemCodeQuery.Location = new System.Drawing.Point(4, 16);
            this.ucLEItemCodeQuery.MaxLength = 40;
            this.ucLEItemCodeQuery.Multiline = false;
            this.ucLEItemCodeQuery.Name = "ucLEItemCodeQuery";
            this.ucLEItemCodeQuery.PasswordChar = '\0';
            this.ucLEItemCodeQuery.ReadOnly = false;
            this.ucLEItemCodeQuery.ShowCheckBox = false;
            this.ucLEItemCodeQuery.Size = new System.Drawing.Size(170, 24);
            this.ucLEItemCodeQuery.TabIndex = 0;
            this.ucLEItemCodeQuery.TabNext = true;
            this.ucLEItemCodeQuery.Value = "";
            this.ucLEItemCodeQuery.WidthType = UserControl.WidthTypes.Normal;
            this.ucLEItemCodeQuery.XAlign = 41;
            // 
            // panelButton
            // 
            this.panelButton.Controls.Add(this.ucButtonCancel);
            this.panelButton.Controls.Add(this.ucBtnSave);
            this.panelButton.Controls.Add(this.ucBtnDelete);
            this.panelButton.Controls.Add(this.ucBtnExit);
            this.panelButton.Controls.Add(this.ucBtnUpdate);
            this.panelButton.Controls.Add(this.ucBtnAdd);
            this.panelButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButton.Location = new System.Drawing.Point(0, 517);
            this.panelButton.Name = "panelButton";
            this.panelButton.Size = new System.Drawing.Size(816, 48);
            this.panelButton.TabIndex = 4;
            // 
            // ucButtonCancel
            // 
            this.ucButtonCancel.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonCancel.BackgroundImage")));
            this.ucButtonCancel.ButtonType = UserControl.ButtonTypes.Cancle;
            this.ucButtonCancel.Caption = "取消";
            this.ucButtonCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonCancel.Location = new System.Drawing.Point(418, 16);
            this.ucButtonCancel.Name = "ucButtonCancel";
            this.ucButtonCancel.Size = new System.Drawing.Size(88, 22);
            this.ucButtonCancel.TabIndex = 23;
            this.ucButtonCancel.Click += new System.EventHandler(this.ucButtonCancel_Click);
            // 
            // ucBtnSave
            // 
            this.ucBtnSave.BackColor = System.Drawing.SystemColors.Control;
            this.ucBtnSave.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucBtnSave.BackgroundImage")));
            this.ucBtnSave.ButtonType = UserControl.ButtonTypes.Save;
            this.ucBtnSave.Caption = "保存";
            this.ucBtnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnSave.Location = new System.Drawing.Point(529, 16);
            this.ucBtnSave.Name = "ucBtnSave";
            this.ucBtnSave.Size = new System.Drawing.Size(88, 22);
            this.ucBtnSave.TabIndex = 24;
            this.ucBtnSave.Click += new System.EventHandler(this.ucBtnSave_Click);
            // 
            // ucBtnDelete
            // 
            this.ucBtnDelete.BackColor = System.Drawing.SystemColors.Control;
            this.ucBtnDelete.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucBtnDelete.BackgroundImage")));
            this.ucBtnDelete.ButtonType = UserControl.ButtonTypes.Delete;
            this.ucBtnDelete.Caption = "删除";
            this.ucBtnDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnDelete.Location = new System.Drawing.Point(306, 16);
            this.ucBtnDelete.Name = "ucBtnDelete";
            this.ucBtnDelete.Size = new System.Drawing.Size(88, 22);
            this.ucBtnDelete.TabIndex = 22;
            this.ucBtnDelete.Click += new System.EventHandler(this.ucBtnDelete_Click);
            // 
            // ucBtnExit
            // 
            this.ucBtnExit.BackColor = System.Drawing.SystemColors.Control;
            this.ucBtnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucBtnExit.BackgroundImage")));
            this.ucBtnExit.ButtonType = UserControl.ButtonTypes.Exit;
            this.ucBtnExit.Caption = "退出";
            this.ucBtnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnExit.Location = new System.Drawing.Point(641, 16);
            this.ucBtnExit.Name = "ucBtnExit";
            this.ucBtnExit.Size = new System.Drawing.Size(88, 22);
            this.ucBtnExit.TabIndex = 25;
            // 
            // ucBtnUpdate
            // 
            this.ucBtnUpdate.BackColor = System.Drawing.SystemColors.Control;
            this.ucBtnUpdate.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucBtnUpdate.BackgroundImage")));
            this.ucBtnUpdate.ButtonType = UserControl.ButtonTypes.Edit;
            this.ucBtnUpdate.Caption = "编辑";
            this.ucBtnUpdate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnUpdate.Location = new System.Drawing.Point(194, 16);
            this.ucBtnUpdate.Name = "ucBtnUpdate";
            this.ucBtnUpdate.Size = new System.Drawing.Size(88, 22);
            this.ucBtnUpdate.TabIndex = 21;
            this.ucBtnUpdate.Click += new System.EventHandler(this.ucBtnUpdate_Click);
            // 
            // ucBtnAdd
            // 
            this.ucBtnAdd.BackColor = System.Drawing.SystemColors.Control;
            this.ucBtnAdd.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucBtnAdd.BackgroundImage")));
            this.ucBtnAdd.ButtonType = UserControl.ButtonTypes.Add;
            this.ucBtnAdd.Caption = "添加";
            this.ucBtnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnAdd.Location = new System.Drawing.Point(82, 16);
            this.ucBtnAdd.Name = "ucBtnAdd";
            this.ucBtnAdd.Size = new System.Drawing.Size(88, 22);
            this.ucBtnAdd.TabIndex = 20;
            this.ucBtnAdd.Click += new System.EventHandler(this.ucBtnAdd_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ucButtonCalcRCardEnd);
            this.groupBox1.Controls.Add(this.ucLabelEditSNCount);
            this.groupBox1.Controls.Add(this.ucButtonCalcCount);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.ultraOptionSetScale);
            this.groupBox1.Controls.Add(this.ucLabelEditRCardPrefix);
            this.groupBox1.Controls.Add(this.bRCardLenULE);
            this.groupBox1.Controls.Add(this.txtSumNum);
            this.groupBox1.Controls.Add(this.btnLockControl);
            this.groupBox1.Controls.Add(this.lblMitemName);
            this.groupBox1.Controls.Add(this.ucByMo);
            this.groupBox1.Controls.Add(this.ucLEItemCode);
            this.groupBox1.Controls.Add(this.ucLERCStart);
            this.groupBox1.Controls.Add(this.ucLERCEnd);
            this.groupBox1.Controls.Add(this.ucLEBIOS);
            this.groupBox1.Controls.Add(this.ucLEPCBA);
            this.groupBox1.Controls.Add(this.ucLEVendorItem);
            this.groupBox1.Controls.Add(this.ucLEDateCode);
            this.groupBox1.Controls.Add(this.ucLEVersion);
            this.groupBox1.Controls.Add(this.ucLEVendorCode);
            this.groupBox1.Controls.Add(this.ucLELotNo);
            this.groupBox1.Controls.Add(this.btnParseSN);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 317);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(816, 200);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // ucButtonCalcRCardEnd
            // 
            this.ucButtonCalcRCardEnd.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonCalcRCardEnd.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonCalcRCardEnd.BackgroundImage")));
            this.ucButtonCalcRCardEnd.ButtonType = UserControl.ButtonTypes.None;
            this.ucButtonCalcRCardEnd.Caption = "试算结束序列号";
            this.ucButtonCalcRCardEnd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonCalcRCardEnd.Location = new System.Drawing.Point(667, 140);
            this.ucButtonCalcRCardEnd.Name = "ucButtonCalcRCardEnd";
            this.ucButtonCalcRCardEnd.Size = new System.Drawing.Size(88, 22);
            this.ucButtonCalcRCardEnd.TabIndex = 45;
            this.ucButtonCalcRCardEnd.Click += new System.EventHandler(this.ucButtonCalcRCardEnd_Click);
            // 
            // ucLabelEditSNCount
            // 
            this.ucLabelEditSNCount.AllowEditOnlyChecked = true;
            this.ucLabelEditSNCount.Caption = "数量";
            this.ucLabelEditSNCount.Checked = false;
            this.ucLabelEditSNCount.EditType = UserControl.EditTypes.Integer;
            this.ucLabelEditSNCount.Location = new System.Drawing.Point(518, 168);
            this.ucLabelEditSNCount.MaxLength = 8;
            this.ucLabelEditSNCount.Multiline = false;
            this.ucLabelEditSNCount.Name = "ucLabelEditSNCount";
            this.ucLabelEditSNCount.PasswordChar = '\0';
            this.ucLabelEditSNCount.ReadOnly = false;
            this.ucLabelEditSNCount.ShowCheckBox = false;
            this.ucLabelEditSNCount.Size = new System.Drawing.Size(137, 24);
            this.ucLabelEditSNCount.TabIndex = 44;
            this.ucLabelEditSNCount.TabNext = false;
            this.ucLabelEditSNCount.Tag = "";
            this.ucLabelEditSNCount.Value = "";
            this.ucLabelEditSNCount.WidthType = UserControl.WidthTypes.Small;
            this.ucLabelEditSNCount.XAlign = 555;
            this.ucLabelEditSNCount.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ucLabelEditSNCount_TxtboxKeyPress);
            // 
            // ucButtonCalcCount
            // 
            this.ucButtonCalcCount.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonCalcCount.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonCalcCount.BackgroundImage")));
            this.ucButtonCalcCount.ButtonType = UserControl.ButtonTypes.None;
            this.ucButtonCalcCount.Caption = "试算数量";
            this.ucButtonCalcCount.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonCalcCount.Location = new System.Drawing.Point(667, 110);
            this.ucButtonCalcCount.Name = "ucButtonCalcCount";
            this.ucButtonCalcCount.Size = new System.Drawing.Size(88, 22);
            this.ucButtonCalcCount.TabIndex = 43;
            this.ucButtonCalcCount.Click += new System.EventHandler(this.ucButtonCalcCount_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(237, 172);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 20);
            this.label1.TabIndex = 42;
            this.label1.Text = "序列号进制";
            // 
            // ultraOptionSetScale
            // 
            appearance1.FontData.BoldAsString = "False";
            this.ultraOptionSetScale.Appearance = appearance1;
            this.ultraOptionSetScale.BorderStyle = Infragistics.Win.UIElementBorderStyle.None;
            this.ultraOptionSetScale.CausesValidation = false;
            this.ultraOptionSetScale.CheckedIndex = 2;
            valueListItem1.DataValue = "0";
            valueListItem1.DisplayText = "10进制";
            valueListItem2.DataValue = "1";
            valueListItem2.DisplayText = "16进制";
            valueListItem3.DataValue = "2";
            valueListItem3.DisplayText = "34进制";
            this.ultraOptionSetScale.Items.AddRange(new Infragistics.Win.ValueListItem[] {
            valueListItem1,
            valueListItem2,
            valueListItem3});
            this.ultraOptionSetScale.Location = new System.Drawing.Point(309, 172);
            this.ultraOptionSetScale.Name = "ultraOptionSetScale";
            this.ultraOptionSetScale.Size = new System.Drawing.Size(298, 20);
            this.ultraOptionSetScale.TabIndex = 19;
            this.ultraOptionSetScale.Text = "34进制";
            this.ultraOptionSetScale.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            // 
            // ucLabelEditRCardPrefix
            // 
            this.ucLabelEditRCardPrefix.AllowEditOnlyChecked = true;
            this.ucLabelEditRCardPrefix.Caption = "首字符串";
            this.ucLabelEditRCardPrefix.Checked = false;
            this.ucLabelEditRCardPrefix.EditType = UserControl.EditTypes.String;
            this.ucLabelEditRCardPrefix.Location = new System.Drawing.Point(38, 44);
            this.ucLabelEditRCardPrefix.MaxLength = 40;
            this.ucLabelEditRCardPrefix.Multiline = false;
            this.ucLabelEditRCardPrefix.Name = "ucLabelEditRCardPrefix";
            this.ucLabelEditRCardPrefix.PasswordChar = '\0';
            this.ucLabelEditRCardPrefix.ReadOnly = false;
            this.ucLabelEditRCardPrefix.ShowCheckBox = false;
            this.ucLabelEditRCardPrefix.Size = new System.Drawing.Size(194, 24);
            this.ucLabelEditRCardPrefix.TabIndex = 7;
            this.ucLabelEditRCardPrefix.TabNext = true;
            this.ucLabelEditRCardPrefix.Value = "";
            this.ucLabelEditRCardPrefix.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabelEditRCardPrefix.XAlign = 99;
            this.ucLabelEditRCardPrefix.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ucLabelEditRCardPrefix_TxtboxKeyPress);
            // 
            // bRCardLenULE
            // 
            this.bRCardLenULE.AllowEditOnlyChecked = true;
            this.bRCardLenULE.Caption = "序列号长度";
            this.bRCardLenULE.Checked = false;
            this.bRCardLenULE.EditType = UserControl.EditTypes.Integer;
            this.bRCardLenULE.Location = new System.Drawing.Point(9, 168);
            this.bRCardLenULE.MaxLength = 40;
            this.bRCardLenULE.Multiline = false;
            this.bRCardLenULE.Name = "bRCardLenULE";
            this.bRCardLenULE.PasswordChar = '\0';
            this.bRCardLenULE.ReadOnly = false;
            this.bRCardLenULE.ShowCheckBox = true;
            this.bRCardLenULE.Size = new System.Drawing.Size(222, 24);
            this.bRCardLenULE.TabIndex = 18;
            this.bRCardLenULE.TabNext = false;
            this.bRCardLenULE.Value = "";
            this.bRCardLenULE.WidthType = UserControl.WidthTypes.Normal;
            this.bRCardLenULE.XAlign = 98;
            this.bRCardLenULE.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.bRCardLenULE_TxtboxKeyPress);
            // 
            // txtSumNum
            // 
            this.txtSumNum.AllowEditOnlyChecked = true;
            this.txtSumNum.Caption = "记录数";
            this.txtSumNum.Checked = false;
            this.txtSumNum.EditType = UserControl.EditTypes.String;
            this.txtSumNum.Location = new System.Drawing.Point(649, 13);
            this.txtSumNum.MaxLength = 40;
            this.txtSumNum.Multiline = false;
            this.txtSumNum.Name = "txtSumNum";
            this.txtSumNum.PasswordChar = '\0';
            this.txtSumNum.ReadOnly = true;
            this.txtSumNum.ShowCheckBox = false;
            this.txtSumNum.Size = new System.Drawing.Size(149, 24);
            this.txtSumNum.TabIndex = 28;
            this.txtSumNum.TabNext = false;
            this.txtSumNum.Value = "0";
            this.txtSumNum.WidthType = UserControl.WidthTypes.Small;
            this.txtSumNum.XAlign = 698;
            // 
            // btnLockControl
            // 
            this.btnLockControl.BackColor = System.Drawing.SystemColors.Control;
            this.btnLockControl.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnLockControl.BackgroundImage")));
            this.btnLockControl.ButtonType = UserControl.ButtonTypes.None;
            this.btnLockControl.Caption = "锁定";
            this.btnLockControl.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLockControl.Location = new System.Drawing.Point(667, 46);
            this.btnLockControl.Name = "btnLockControl";
            this.btnLockControl.Size = new System.Drawing.Size(88, 22);
            this.btnLockControl.TabIndex = 26;
            this.btnLockControl.Click += new System.EventHandler(this.btnLockControl_Click);
            // 
            // lblMitemName
            // 
            this.lblMitemName.Location = new System.Drawing.Point(243, 18);
            this.lblMitemName.Name = "lblMitemName";
            this.lblMitemName.Size = new System.Drawing.Size(386, 18);
            this.lblMitemName.TabIndex = 37;
            // 
            // ucByMo
            // 
            this.ucByMo.AllowEditOnlyChecked = true;
            this.ucByMo.Caption = "按工单备料";
            this.ucByMo.Checked = false;
            this.ucByMo.EditType = UserControl.EditTypes.String;
            this.ucByMo.Location = new System.Drawing.Point(9, 138);
            this.ucByMo.MaxLength = 40;
            this.ucByMo.Multiline = false;
            this.ucByMo.Name = "ucByMo";
            this.ucByMo.PasswordChar = '\0';
            this.ucByMo.ReadOnly = false;
            this.ucByMo.ShowCheckBox = true;
            this.ucByMo.Size = new System.Drawing.Size(222, 24);
            this.ucByMo.TabIndex = 17;
            this.ucByMo.TabNext = true;
            this.ucByMo.Value = "";
            this.ucByMo.WidthType = UserControl.WidthTypes.Normal;
            this.ucByMo.XAlign = 98;
            this.ucByMo.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ucByMo_TxtboxKeyPress);
            // 
            // ucLEItemCode
            // 
            this.ucLEItemCode.AllowEditOnlyChecked = true;
            this.ucLEItemCode.Caption = "料号";
            this.ucLEItemCode.Checked = false;
            this.ucLEItemCode.EditType = UserControl.EditTypes.String;
            this.ucLEItemCode.Location = new System.Drawing.Point(62, 13);
            this.ucLEItemCode.MaxLength = 40;
            this.ucLEItemCode.Multiline = false;
            this.ucLEItemCode.Name = "ucLEItemCode";
            this.ucLEItemCode.PasswordChar = '\0';
            this.ucLEItemCode.ReadOnly = false;
            this.ucLEItemCode.ShowCheckBox = false;
            this.ucLEItemCode.Size = new System.Drawing.Size(170, 24);
            this.ucLEItemCode.TabIndex = 6;
            this.ucLEItemCode.TabNext = true;
            this.ucLEItemCode.Value = "";
            this.ucLEItemCode.WidthType = UserControl.WidthTypes.Normal;
            this.ucLEItemCode.XAlign = 99;
            this.ucLEItemCode.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ucLEItemCode_TxtboxKeyPress);
            // 
            // ucLERCStart
            // 
            this.ucLERCStart.AllowEditOnlyChecked = true;
            this.ucLERCStart.Caption = "起始序列号";
            this.ucLERCStart.Checked = false;
            this.ucLERCStart.EditType = UserControl.EditTypes.String;
            this.ucLERCStart.Location = new System.Drawing.Point(237, 44);
            this.ucLERCStart.MaxLength = 40;
            this.ucLERCStart.Multiline = false;
            this.ucLERCStart.Name = "ucLERCStart";
            this.ucLERCStart.PasswordChar = '\0';
            this.ucLERCStart.ReadOnly = false;
            this.ucLERCStart.ShowCheckBox = false;
            this.ucLERCStart.Size = new System.Drawing.Size(206, 24);
            this.ucLERCStart.TabIndex = 8;
            this.ucLERCStart.TabNext = true;
            this.ucLERCStart.Value = "";
            this.ucLERCStart.WidthType = UserControl.WidthTypes.Normal;
            this.ucLERCStart.XAlign = 310;
            this.ucLERCStart.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ucLERCStart_TxtboxKeyPress);
            // 
            // ucLERCEnd
            // 
            this.ucLERCEnd.AllowEditOnlyChecked = true;
            this.ucLERCEnd.Caption = "结束序列号";
            this.ucLERCEnd.Checked = false;
            this.ucLERCEnd.EditType = UserControl.EditTypes.String;
            this.ucLERCEnd.Location = new System.Drawing.Point(449, 44);
            this.ucLERCEnd.MaxLength = 40;
            this.ucLERCEnd.Multiline = false;
            this.ucLERCEnd.Name = "ucLERCEnd";
            this.ucLERCEnd.PasswordChar = '\0';
            this.ucLERCEnd.ReadOnly = false;
            this.ucLERCEnd.ShowCheckBox = false;
            this.ucLERCEnd.Size = new System.Drawing.Size(206, 24);
            this.ucLERCEnd.TabIndex = 9;
            this.ucLERCEnd.TabNext = true;
            this.ucLERCEnd.Value = "";
            this.ucLERCEnd.WidthType = UserControl.WidthTypes.Normal;
            this.ucLERCEnd.XAlign = 522;
            this.ucLERCEnd.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ucLERCEnd_TxtboxKeyPress);
            // 
            // ucLEBIOS
            // 
            this.ucLEBIOS.AllowEditOnlyChecked = true;
            this.ucLEBIOS.Caption = "BIOS版本";
            this.ucLEBIOS.Checked = false;
            this.ucLEBIOS.EditType = UserControl.EditTypes.String;
            this.ucLEBIOS.Location = new System.Drawing.Point(461, 108);
            this.ucLEBIOS.MaxLength = 40;
            this.ucLEBIOS.Multiline = false;
            this.ucLEBIOS.Name = "ucLEBIOS";
            this.ucLEBIOS.PasswordChar = '\0';
            this.ucLEBIOS.ReadOnly = false;
            this.ucLEBIOS.ShowCheckBox = false;
            this.ucLEBIOS.Size = new System.Drawing.Size(194, 24);
            this.ucLEBIOS.TabIndex = 15;
            this.ucLEBIOS.TabNext = true;
            this.ucLEBIOS.Value = "";
            this.ucLEBIOS.WidthType = UserControl.WidthTypes.Normal;
            this.ucLEBIOS.XAlign = 522;
            this.ucLEBIOS.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ucLEBIOS_TxtboxKeyPress);
            // 
            // ucLEPCBA
            // 
            this.ucLEPCBA.AllowEditOnlyChecked = true;
            this.ucLEPCBA.Caption = "PCBA版本";
            this.ucLEPCBA.Checked = false;
            this.ucLEPCBA.EditType = UserControl.EditTypes.String;
            this.ucLEPCBA.Location = new System.Drawing.Point(249, 108);
            this.ucLEPCBA.MaxLength = 40;
            this.ucLEPCBA.Multiline = false;
            this.ucLEPCBA.Name = "ucLEPCBA";
            this.ucLEPCBA.PasswordChar = '\0';
            this.ucLEPCBA.ReadOnly = false;
            this.ucLEPCBA.ShowCheckBox = false;
            this.ucLEPCBA.Size = new System.Drawing.Size(194, 24);
            this.ucLEPCBA.TabIndex = 14;
            this.ucLEPCBA.TabNext = true;
            this.ucLEPCBA.Value = "";
            this.ucLEPCBA.WidthType = UserControl.WidthTypes.Normal;
            this.ucLEPCBA.XAlign = 310;
            this.ucLEPCBA.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ucLEPCBA_TxtboxKeyPress);
            // 
            // ucLEVendorItem
            // 
            this.ucLEVendorItem.AllowEditOnlyChecked = true;
            this.ucLEVendorItem.Caption = "厂商料号";
            this.ucLEVendorItem.Checked = false;
            this.ucLEVendorItem.EditType = UserControl.EditTypes.String;
            this.ucLEVendorItem.Location = new System.Drawing.Point(461, 76);
            this.ucLEVendorItem.MaxLength = 40;
            this.ucLEVendorItem.Multiline = false;
            this.ucLEVendorItem.Name = "ucLEVendorItem";
            this.ucLEVendorItem.PasswordChar = '\0';
            this.ucLEVendorItem.ReadOnly = false;
            this.ucLEVendorItem.ShowCheckBox = false;
            this.ucLEVendorItem.Size = new System.Drawing.Size(194, 24);
            this.ucLEVendorItem.TabIndex = 12;
            this.ucLEVendorItem.TabNext = true;
            this.ucLEVendorItem.Value = "";
            this.ucLEVendorItem.WidthType = UserControl.WidthTypes.Normal;
            this.ucLEVendorItem.XAlign = 522;
            this.ucLEVendorItem.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ucLEVendorItem_TxtboxKeyPress);
            // 
            // ucLEDateCode
            // 
            this.ucLEDateCode.AllowEditOnlyChecked = true;
            this.ucLEDateCode.Caption = "生产日期";
            this.ucLEDateCode.Checked = false;
            this.ucLEDateCode.EditType = UserControl.EditTypes.String;
            this.ucLEDateCode.Location = new System.Drawing.Point(37, 76);
            this.ucLEDateCode.MaxLength = 40;
            this.ucLEDateCode.Multiline = false;
            this.ucLEDateCode.Name = "ucLEDateCode";
            this.ucLEDateCode.PasswordChar = '\0';
            this.ucLEDateCode.ReadOnly = false;
            this.ucLEDateCode.ShowCheckBox = false;
            this.ucLEDateCode.Size = new System.Drawing.Size(194, 24);
            this.ucLEDateCode.TabIndex = 10;
            this.ucLEDateCode.TabNext = true;
            this.ucLEDateCode.Value = "";
            this.ucLEDateCode.WidthType = UserControl.WidthTypes.Normal;
            this.ucLEDateCode.XAlign = 98;
            this.ucLEDateCode.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ucLEDateCode_TxtboxKeyPress);
            // 
            // ucLEVersion
            // 
            this.ucLEVersion.AllowEditOnlyChecked = true;
            this.ucLEVersion.Caption = "物料版本";
            this.ucLEVersion.Checked = false;
            this.ucLEVersion.EditType = UserControl.EditTypes.String;
            this.ucLEVersion.Location = new System.Drawing.Point(37, 108);
            this.ucLEVersion.MaxLength = 40;
            this.ucLEVersion.Multiline = false;
            this.ucLEVersion.Name = "ucLEVersion";
            this.ucLEVersion.PasswordChar = '\0';
            this.ucLEVersion.ReadOnly = false;
            this.ucLEVersion.ShowCheckBox = false;
            this.ucLEVersion.Size = new System.Drawing.Size(194, 24);
            this.ucLEVersion.TabIndex = 13;
            this.ucLEVersion.TabNext = true;
            this.ucLEVersion.Value = "";
            this.ucLEVersion.WidthType = UserControl.WidthTypes.Normal;
            this.ucLEVersion.XAlign = 98;
            this.ucLEVersion.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ucLEVersion_TxtboxKeyPress);
            // 
            // ucLEVendorCode
            // 
            this.ucLEVendorCode.AllowEditOnlyChecked = true;
            this.ucLEVendorCode.Caption = "厂商";
            this.ucLEVendorCode.Checked = false;
            this.ucLEVendorCode.EditType = UserControl.EditTypes.String;
            this.ucLEVendorCode.Location = new System.Drawing.Point(272, 76);
            this.ucLEVendorCode.MaxLength = 40;
            this.ucLEVendorCode.Multiline = false;
            this.ucLEVendorCode.Name = "ucLEVendorCode";
            this.ucLEVendorCode.PasswordChar = '\0';
            this.ucLEVendorCode.ReadOnly = false;
            this.ucLEVendorCode.ShowCheckBox = false;
            this.ucLEVendorCode.Size = new System.Drawing.Size(170, 24);
            this.ucLEVendorCode.TabIndex = 11;
            this.ucLEVendorCode.TabNext = true;
            this.ucLEVendorCode.Value = "";
            this.ucLEVendorCode.WidthType = UserControl.WidthTypes.Normal;
            this.ucLEVendorCode.XAlign = 309;
            this.ucLEVendorCode.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ucLEVendorCode_TxtboxKeyPress);
            // 
            // ucLELotNo
            // 
            this.ucLELotNo.AllowEditOnlyChecked = true;
            this.ucLELotNo.Caption = "生产批号";
            this.ucLELotNo.Checked = false;
            this.ucLELotNo.EditType = UserControl.EditTypes.String;
            this.ucLELotNo.Location = new System.Drawing.Point(461, 138);
            this.ucLELotNo.MaxLength = 40;
            this.ucLELotNo.Multiline = false;
            this.ucLELotNo.Name = "ucLELotNo";
            this.ucLELotNo.PasswordChar = '\0';
            this.ucLELotNo.ReadOnly = false;
            this.ucLELotNo.ShowCheckBox = false;
            this.ucLELotNo.Size = new System.Drawing.Size(194, 24);
            this.ucLELotNo.TabIndex = 16;
            this.ucLELotNo.TabNext = true;
            this.ucLELotNo.Value = "";
            this.ucLELotNo.WidthType = UserControl.WidthTypes.Normal;
            this.ucLELotNo.XAlign = 522;
            this.ucLELotNo.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ucLELotNo_TxtboxKeyPress);
            // 
            // btnParseSN
            // 
            this.btnParseSN.BackColor = System.Drawing.SystemColors.Control;
            this.btnParseSN.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnParseSN.BackgroundImage")));
            this.btnParseSN.ButtonType = UserControl.ButtonTypes.None;
            this.btnParseSN.Caption = "解析序列号";
            this.btnParseSN.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnParseSN.Location = new System.Drawing.Point(667, 78);
            this.btnParseSN.Name = "btnParseSN";
            this.btnParseSN.Size = new System.Drawing.Size(88, 22);
            this.btnParseSN.TabIndex = 27;
            this.btnParseSN.Click += new System.EventHandler(this.btnParseSN_Click);
            // 
            // ultraGridMain
            // 
            this.ultraGridMain.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ultraGridMain.DataSource = this.dtMKeyparts;
            appearance3.BackColor = System.Drawing.Color.White;
            this.ultraGridMain.DisplayLayout.Appearance = appearance3;
            ultraGridBand1.AddButtonCaption = "Table1";
            ultraGridColumn1.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn1.Header.Caption = "序号";
            ultraGridColumn1.Header.VisiblePosition = 0;
            ultraGridColumn1.Hidden = true;
            ultraGridColumn2.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn2.Header.Caption = "首字符串";
            ultraGridColumn2.Header.VisiblePosition = 2;
            ultraGridColumn2.Width = 122;
            ultraGridColumn3.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn3.Header.Caption = "起始序列号";
            ultraGridColumn3.Header.VisiblePosition = 3;
            ultraGridColumn3.Width = 122;
            ultraGridColumn4.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn4.Header.Caption = "结束序列号";
            ultraGridColumn4.Header.VisiblePosition = 4;
            ultraGridColumn4.Width = 124;
            ultraGridColumn5.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn5.Header.Caption = "进制";
            ultraGridColumn5.Header.VisiblePosition = 5;
            ultraGridColumn5.Width = 50;
            ultraGridColumn6.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn6.Header.Caption = "料号";
            ultraGridColumn6.Header.VisiblePosition = 6;
            ultraGridColumn7.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn7.Header.Caption = "生产批号";
            ultraGridColumn7.Header.VisiblePosition = 9;
            ultraGridColumn8.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn8.Header.Caption = "厂商";
            ultraGridColumn8.Header.VisiblePosition = 10;
            ultraGridColumn9.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn9.Header.Caption = "厂商料号";
            ultraGridColumn9.Header.VisiblePosition = 11;
            ultraGridColumn10.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn10.Header.Caption = "生产日期";
            ultraGridColumn10.Header.VisiblePosition = 12;
            ultraGridColumn11.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn11.Header.Caption = "物料版本";
            ultraGridColumn11.Header.VisiblePosition = 13;
            ultraGridColumn12.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn12.Header.Caption = "BIOS版本";
            ultraGridColumn12.Header.VisiblePosition = 14;
            ultraGridColumn13.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn13.Header.Caption = "PCBA版本";
            ultraGridColumn13.Header.VisiblePosition = 15;
            ultraGridColumn14.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn14.Header.Caption = "维护用户";
            ultraGridColumn14.Header.VisiblePosition = 16;
            ultraGridColumn14.Hidden = true;
            ultraGridColumn15.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn15.Header.Caption = "维护日期";
            ultraGridColumn15.Header.VisiblePosition = 1;
            ultraGridColumn16.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn16.Header.Caption = "维护时间";
            ultraGridColumn16.Header.VisiblePosition = 17;
            ultraGridColumn16.Hidden = true;
            ultraGridColumn17.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn17.Header.Caption = "工单";
            ultraGridColumn17.Header.VisiblePosition = 8;
            ultraGridColumn18.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn18.Header.Caption = "物料描述";
            ultraGridColumn18.Header.VisiblePosition = 7;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn1,
            ultraGridColumn2,
            ultraGridColumn3,
            ultraGridColumn4,
            ultraGridColumn5,
            ultraGridColumn6,
            ultraGridColumn7,
            ultraGridColumn8,
            ultraGridColumn9,
            ultraGridColumn10,
            ultraGridColumn11,
            ultraGridColumn12,
            ultraGridColumn13,
            ultraGridColumn14,
            ultraGridColumn15,
            ultraGridColumn16,
            ultraGridColumn17,
            ultraGridColumn18});
            this.ultraGridMain.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            appearance4.FontData.BoldAsString = "True";
            appearance4.TextHAlignAsString = "Left";
            this.ultraGridMain.DisplayLayout.CaptionAppearance = appearance4;
            this.ultraGridMain.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
            this.ultraGridMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGridMain.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ultraGridMain.Location = new System.Drawing.Point(0, 48);
            this.ultraGridMain.Name = "ultraGridMain";
            this.ultraGridMain.Size = new System.Drawing.Size(816, 269);
            this.ultraGridMain.TabIndex = 5;
            // 
            // FKeyParts
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(816, 565);
            this.Controls.Add(this.ultraGridMain);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panelButton);
            this.Controls.Add(this.grpQuery);
            this.Name = "FKeyParts";
            this.Text = "物料追溯信息维护";
            this.Load += new System.EventHandler(this.FKeyParts_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtMKeyparts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsMKeyparts)).EndInit();
            this.grpQuery.ResumeLayout(false);
            this.panelButton.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraOptionSetScale)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridMain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        #region Form Base

        private ItemFacade _ItemFacade = null;
        private MaterialFacade _MaterialFacade = null;
        private OPBOMFacade _OPBOMFacade = null;
        private MOFacade _MOFacade = null;
        private SBOMFacade _SBOMFacade = null;
        private Infragistics.Win.UltraWinGrid.UltraGridRow _currRow = null;
        private IDomainDataProvider _domainDataProvider = ApplicationService.Current().DataProvider;

        public IDomainDataProvider DataProvider
        {
            get
            {
                return _domainDataProvider;
            }
        }

        public FKeyParts()
        {
            // 该调用是 Windows 窗体设计器所必需的。
            InitializeComponent();

            // TODO: 在 InitializeComponent 调用后添加任何初始化

            UserControl.UIStyleBuilder.GridUI(ultraGridMain);
            UserControl.UIStyleBuilder.FormUI(this);

            ultraGridMain.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Default;

            this._ItemFacade = new ItemFacade(this.DataProvider);
            this._MaterialFacade = new MaterialFacade(this.DataProvider);
            this._OPBOMFacade = new OPBOMFacade(this.DataProvider);
            this._MOFacade = new MOFacade(this.DataProvider);
            this._SBOMFacade = new SBOMFacade(this.DataProvider);
        }

        protected void ShowMessage(string message)
        {
            ApplicationRun.GetInfoForm().Add(message);
        }

        protected void ShowMessage(Exception e)
        {
            ApplicationRun.GetInfoForm().Add(new UserControl.Message(e));
        }

        protected void ShowMessage(Messages messages)
        {
            ApplicationRun.GetInfoForm().Add(messages);
        }

        protected void ShowMessage(UserControl.Message message)
        {
            ApplicationRun.GetInfoForm().Add(message);
        }

        #endregion

        #region FormStatus

        private string _status = FormStatus.Ready;
        private string status
        {
            get
            {
                return this._status;
            }
            set
            {
                this._status = value;

                try
                {
                    if (this._status == FormStatus.Ready)
                    {
                        this.ucBtnAdd.Enabled = true;
                        this.ucBtnDelete.Enabled = true;
                        this.ucBtnUpdate.Enabled = true;
                        this.ucButtonCancel.Enabled = false;
                        this.ucBtnSave.Enabled = false;

                        this._currRow = null;

                        this.ucLabelEditRCardPrefix.Enabled = false;
                        this.ucLERCStart.Enabled = false;
                        this.ucLERCEnd.Enabled = false;
                        this.ucLEItemCode.Enabled = false;
                        this.ucLELotNo.Enabled = false;
                        this.ucLEDateCode.Enabled = false;
                        this.ucLEVendorCode.Enabled = false;
                        this.ucLEVendorItem.Enabled = false;
                        this.ucLEVersion.Enabled = false;
                        this.ucLEBIOS.Enabled = false;
                        this.ucLEPCBA.Enabled = false;
                        this.ucByMo.Enabled = false;
                    }
                    if (this._status == FormStatus.Add)
                    {
                        this.ucBtnAdd.Enabled = true;
                        this.ucBtnDelete.Enabled = false;
                        this.ucBtnUpdate.Enabled = false;
                        this.ucButtonCancel.Enabled = true;
                        this.ucBtnSave.Enabled = true;

                        this.ucLabelEditRCardPrefix.TextFocus(false, true);

                        this.ucLabelEditRCardPrefix.Enabled = true;
                        this.ucLERCStart.Enabled = true;
                        this.ucLERCEnd.Enabled = true;
                        this.ucLEItemCode.Enabled = true;
                        this.ucLELotNo.Enabled = true;
                        this.ucLEDateCode.Enabled = true;
                        this.ucLEVendorCode.Enabled = true;
                        this.ucLEVendorItem.Enabled = true;
                        this.ucLEVersion.Enabled = true;
                        this.ucLEBIOS.Enabled = true;
                        this.ucLEPCBA.Enabled = true;
                        this.ucByMo.Enabled = true;
                    }
                    if (this._status == FormStatus.Update)
                    {
                        this.ucBtnAdd.Enabled = false;
                        this.ucBtnDelete.Enabled = false;
                        this.ucBtnUpdate.Enabled = false;
                        this.ucButtonCancel.Enabled = true;
                        this.ucBtnSave.Enabled = true;

                        this.ucLabelEditRCardPrefix.TextFocus(false, true);

                        this.ucLabelEditRCardPrefix.Enabled = true;
                        this.ucLERCStart.Enabled = true;
                        this.ucLERCEnd.Enabled = true;
                        this.ucLEItemCode.Enabled = true;
                        this.ucLELotNo.Enabled = true;
                        this.ucLEDateCode.Enabled = true;
                        this.ucLEVendorCode.Enabled = true;
                        this.ucLEVendorItem.Enabled = true;
                        this.ucLEVersion.Enabled = true;
                        this.ucLEBIOS.Enabled = true;
                        this.ucLEPCBA.Enabled = true;
                        this.ucByMo.Enabled = true;
                    }
                }
                catch { }
            }
        }

        #endregion

        #region User Events

        private void FKeyParts_Load(object sender, System.EventArgs e)
        {
            this.status = FormStatus.Ready;
            this.ucDate.Value = DateTime.Today;

            ChangeDateStatus();

            this.txtSumNum.InnerTextBox.TextAlign = HorizontalAlignment.Right;
            this.ucLabelEditSNCount.InnerTextBox.ForeColor = Color.Black;
            this.ucLabelEditSNCount.InnerTextBox.TextAlign = HorizontalAlignment.Right;
        }

        private void FKeyParts_Closed(object sender, System.EventArgs e)
        {
            if (this.DataProvider != null)
                ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)this.DataProvider).PersistBroker.CloseConnection();
        }

        private void chkNeedDate_CheckedChanged(object sender, System.EventArgs e)
        {
            ChangeDateStatus();
        }

        private void ucLEItemCode_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (this.ucLEItemCode.Value.Trim() == string.Empty)
                {
                    this.ShowMessage(new UserControl.Message(MessageType.Error, "料号$Error_Input_Empty"));
                    ucLEItemCode.TextFocus(false, true);
                    return;
                }

                if (!checkItemCode())
                {
                    this.ShowMessage(new UserControl.Message(MessageType.Error, "$Error_CS_OPBOMItem_Not_Exist"));
                    ucLEItemCode.TextFocus(false, true);
                    return;
                }

                if (this.ucLEItemCode.Value != String.Empty)
                {
                    //object obj = _OPBOMFacade.GetOPBOMDetailByBItemCode(
                    //    ucByMo.Value.Trim().ToUpper(),
                    //    ucLEItemCode.Value.Trim().ToUpper());

                    //if (obj != null)
                    //{
                    //    lblMitemName.Text = (obj as OPBOMDetail).OPBOMItemName;
                    //}

                    Domain.MOModel.Material material = (Domain.MOModel.Material)_ItemFacade.GetMaterial(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.ucLEItemCode.Value)), GlobalVariables.CurrentOrganizations.First().OrganizationID);

                    if (material != null)
                    {
                        lblMitemName.Text = material.MaterialDescription;

                    }
                }

                this.ucLabelEditRCardPrefix.TextFocus(false, true);
            }
        }

        private void ucLELotNo_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
                this.ucLabelEditRCardPrefix.TextFocus(false, true);
        }

        private void ucLEDateCode_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
                this.ucLEVendorCode.TextFocus(false, true);
        }

        private void ucLEVendorCode_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
                this.ucLEVendorItem.TextFocus(false, true);
        }

        private void ucLEVendorItem_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
                this.ucLEVersion.TextFocus(false, true);
        }

        private void ucLEVersion_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
                this.ucLEPCBA.TextFocus(false, true);
        }

        private void ucLEPCBA_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
                this.ucLEBIOS.TextFocus(false, true);
        }

        private void ucLEBIOS_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
                this.ucLELotNo.TextFocus(false, true);
        }

        private void ucLabelEditRCardPrefix_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (this.ucLabelEditRCardPrefix.Value.Trim() == string.Empty)
                {
                    this.ShowMessage(new UserControl.Message(MessageType.Error, "$CS_RCardPrefix$Error_Input_Empty"));
                    ucLabelEditRCardPrefix.TextFocus(false, true);
                    return;
                }

                this.ucLERCStart.TextFocus(false, true);
            }
        }

        private void ucLERCStart_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (this.ucLERCStart.Value.Trim() == string.Empty)
                {
                    this.ShowMessage(new UserControl.Message(MessageType.Error, "$CS_RunningCardStart$Error_Input_Empty"));
                    ucLERCStart.TextFocus(false, true);
                    return;
                }

                this.ucLERCEnd.TextFocus(false, true);
            }
        }

        private void ucLERCEnd_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {

                if (ParseSN() && CalcSNCount())
                    this.ucLEDateCode.TextFocus(false, true);
            }
        }

        private void ucByMo_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (!CheckMO())
                    return;

                this.ucLabelEditRCardPrefix.TextFocus(false, true);
            }
        }

        private void bRCardLenULE_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
                this.ucLabelEditRCardPrefix.TextFocus(false, true);
        }

        private void ucLabelEditSNCount_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '-')
            {
                e.Handled = true;
            };
        }

        #endregion

        #region Button Events

        private void btnLockControl_Click(object sender, System.EventArgs e)
        {
            LockControl();
        }

        private void LockControl()
        {
            if (btnLockControl.Caption == "锁定" && ucLEItemCode.Value.Trim() != String.Empty)
            {
                btnLockControl.Caption = "解除锁定";

                ucLEBIOS.Enabled = false;
                ucLEVendorItem.Enabled = false;
                ucLEDateCode.Enabled = false;
                ucLEVersion.Enabled = false;
                ucLEVendorCode.Enabled = false;
                ucLELotNo.Enabled = false;
                ucLEPCBA.Enabled = false;
                ucLEItemCode.Enabled = false;
            }
            else if (btnLockControl.Caption == "解除锁定")
            {
                btnLockControl.Caption = "锁定";

                ucLEBIOS.Enabled = true;
                ucLEVendorItem.Enabled = true;
                ucLEDateCode.Enabled = true;
                ucLEVersion.Enabled = true;
                ucLEVendorCode.Enabled = true;
                ucLELotNo.Enabled = true;
                ucLEPCBA.Enabled = true;
                ucLEItemCode.Enabled = true;
            }
        }

        private void btnParseSN_Click(object sender, EventArgs e)
        {
            ParseSN();
        }

        private bool ParseSN()
        {
            if (ucLabelEditRCardPrefix.Value.Trim().Length >= 6)
            {
                try
                {
                    string prefix = ucLabelEditRCardPrefix.Value.Trim();

                    this.ucLEVendorCode.Value = prefix.Substring(0, 2);

                    string dateCode = string.Empty;
                    dateCode += "20" + int.Parse(NumberScaleHelper.ChangeNumber(prefix.Substring(2, 2), NumberScale.Scale10, NumberScale.Scale10)).ToString("00");
                    dateCode += int.Parse(NumberScaleHelper.ChangeNumber(prefix.Substring(4, 1), NumberScale.Scale16, NumberScale.Scale10)).ToString("00");
                    dateCode += int.Parse(NumberScaleHelper.ChangeNumber(prefix.Substring(5, 1), NumberScale.Scale34, NumberScale.Scale10)).ToString("00");
                    this.ucLEDateCode.Value = dateCode;

                    return true;
                }
                catch (Exception ex)
                {
                    this.ShowMessage(ex);
                    ucLabelEditRCardPrefix.TextFocus(false, true);
                    return false;
                }
            }

            return false;
        }

        private void ucBtnAdd_Click(object sender, System.EventArgs e)
        {
            if (this.status == FormStatus.Ready)
            {
                InitEditControls(true);
                this.status = FormStatus.Add;

                if (btnLockControl.Caption == "锁定")
                {
                    ucLEItemCode.TextFocus(false, true);
                }
                else
                {
                    ucLabelEditRCardPrefix.TextFocus(false, true);
                }
            }
            else if (this._status == FormStatus.Add)
            {
                if (Save())
                {
                    InitEditControls(false);
                    ucLabelEditRCardPrefix.TextFocus(false, true);
                }
            }
        }

        private void ucBtnUpdate_Click(object sender, System.EventArgs e)
        {
            this._currRow = ultraGridMain.ActiveRow;

            object obj = this.GetEditObject(ultraGridMain.ActiveRow);

            if (obj == null)
            {
                return;
            }

            List<MKeyPart> mKeyPartList = new List<MKeyPart>();
            mKeyPartList.Add((MKeyPart)obj);
            if (!CheckBeforeDeleteAndUpdate(mKeyPartList))
            {
                this.ShowMessage("$Error_CannotUpdateMKeyPart");
                return;
            }

            this.SetEditObject(obj);

            this.status = FormStatus.Update;
        }

        private void ucButtonCancel_Click(object sender, EventArgs e)
        {
            this.status = FormStatus.Ready;
            this.btnLockControl.Caption = "锁定";
            InitEditControls(true);
        }

        private void ucBtnDelete_Click(object sender, System.EventArgs e)
        {
            if (MessageBox.Show(MutiLanguages.ParserMessage("$ConformDelete"), this.Text, MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                return;
            }

            List<MKeyPart> mKeyPartList = new List<MKeyPart>();

            for (int iGridRowLoopIndex = 0; iGridRowLoopIndex < ultraGridMain.Selected.Rows.Count; iGridRowLoopIndex++)
            {
                Infragistics.Win.UltraWinGrid.UltraGridRow row = ultraGridMain.Selected.Rows[iGridRowLoopIndex];
                object obj = this.GetEditObject(row);

                mKeyPartList.Add((MKeyPart)obj);
            }

            if (!CheckBeforeDeleteAndUpdate(mKeyPartList))
            {
                this.ShowMessage("$Error_CannotDeleteMKeyPart");
                return;
            }
            
            try
            {
                this.DataProvider.BeginTransaction();

                foreach (MKeyPart mKeyPart in mKeyPartList)
                {
                    if (mKeyPart == null)
                    {
                        continue;
                    }

                    this._MaterialFacade.DeleteMKeyPart(mKeyPart);
                }
                this.ultraGridMain.DeleteSelectedRows(false);

                this.DataProvider.CommitTransaction();

            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                this.ShowMessage(ex);
                return;
            }

            this.status = FormStatus.Ready;
        }

        private void ucBtnSave_Click(object sender, System.EventArgs e)
        {
            if (Save())
            {
                this.status = FormStatus.Ready;
                this.btnLockControl.Caption = "锁定";

                InitEditControls(true);
                ucLabelEditRCardPrefix.TextFocus(false, true);
            }
        }

        private bool Save()
        {

            object obj = this.GetEditObject();
            if (obj == null)
            {
                return false;
            }

           
            NumberScale scale = NumberScale.Scale34;
            if (ultraOptionSetScale.CheckedIndex == 0)
                scale = NumberScale.Scale10;
            else if (ultraOptionSetScale.CheckedIndex == 1)
                scale = NumberScale.Scale16;
            else if (ultraOptionSetScale.CheckedIndex == 2)
                scale = NumberScale.Scale34;

            try
            {
                if (!this.CheckID(this.ucLERCStart.Value.Trim().ToUpper(), this.ucLERCEnd.Value.Trim().ToUpper()))
                    return false;

                //判断是否需要涉及TBLMKEYPARTDETAIL（更新时，如果首字符串、起始序列号、结束序列号未修改，则不动TBLMKEYPARTDETAIL）
                bool needUpdateDetail = true;
                string[] snList = null;
                if (this._status == FormStatus.Update)
                {
                    MKeyPart newMKeyPart = (MKeyPart)obj;
                    //newMKeyPart中的Sequence字段目前还没有意义
                    MKeyPart oldMKeyPart = (MKeyPart)this._MaterialFacade.GetMKeyPart(Int32.Parse(this._currRow.Cells[0].Text), newMKeyPart.MItemCode);

                    if (oldMKeyPart != null
                        && oldMKeyPart.RCardPrefix.Trim().ToUpper() == newMKeyPart.RCardPrefix.Trim().ToUpper()
                        && oldMKeyPart.RunningCardStart.Trim().ToUpper() == newMKeyPart.RunningCardStart.Trim().ToUpper()
                        && oldMKeyPart.RunningCardEnd.Trim().ToUpper() == newMKeyPart.RunningCardEnd.Trim().ToUpper()
                        && oldMKeyPart.SNScale.Trim().ToUpper() == newMKeyPart.SNScale.Trim().ToUpper())
                    {
                        needUpdateDetail = false;
                    }
                }

                if (needUpdateDetail)
                {
                    int length = ucLERCStart.Value.Trim().Length;

                    long startSN = 0;
                    try
                    {
                        startSN = long.Parse(NumberScaleHelper.ChangeNumber(ucLERCStart.Value.Trim(), scale, NumberScale.Scale10));
                    }
                    catch (Exception ex)
                    {
                        this.ShowMessage(ex);
                        ucLERCStart.TextFocus(false, true);
                        return false;
                    }

                    long endSN = 0;
                    try
                    {
                        endSN = long.Parse(NumberScaleHelper.ChangeNumber(ucLERCEnd.Value.Trim(), scale, NumberScale.Scale10));
                    }
                    catch (Exception ex)
                    {
                        this.ShowMessage(ex);
                        ucLERCEnd.TextFocus(false, true);
                        return false;
                    }

                    if ((startSN.ToString().Length + ((MKeyPart)obj).RCardPrefix.Length) > 40)
                    {
                        this.ShowMessage("$Error_SNLength_Wrong");
                        ucLERCStart.TextFocus(false, true);
                        return false;

                    }

                    if (startSN > endSN)
                    {
                        this.ShowMessage("$Error_CS_RunningCardStart_Greater_Than_RunningCardEnd");
                        ucLERCEnd.TextFocus(false, true);
                        return false;
                    }

                    //检查需要插入的detail数据量是否太多
                    if (endSN - startSN > 4999)
                    {
                        if (MessageBox.Show( MutiLanguages.ParserMessage("$Generate_number_Is_Greate"), this.Text, MessageBoxButtons.OKCancel) != DialogResult.OK)
                        {
                            this.ucLERCEnd.TextFocus(false, true);
                            return false;
                        }
                    }

                    //获取所有的序列号到数组中
                    snList = new string[endSN - startSN + 1];
                    for (long i = 0; i < snList.Length; i++)
                    {
                        snList[i] = NumberScaleHelper.ChangeNumber(Convert.ToString(startSN + i), NumberScale.Scale10, scale);
                        if (snList[i].Length < length)
                            snList[i] = snList[i].PadLeft(length, '0');
                        snList[i] = ((MKeyPart)obj).RCardPrefix + snList[i];
                    }

                    //检查需要插入的detail数据量是否重复
                    string startSNString = ((MKeyPart)obj).RCardPrefix + ucLERCStart.Value.Trim().ToUpper();
                    string endSNString = ((MKeyPart)obj).RCardPrefix + ucLERCEnd.Value.Trim().ToUpper();

                    object[] existDetailList = _MaterialFacade.QueryMKeyPartDetailWithHead(((MKeyPart)obj).MItemCode.ToUpper(), -1, startSNString, endSNString);
                    bool exist = false;
                    if (existDetailList != null)
                    {
                        foreach (MKeyPartDetail detial in existDetailList)
                        {
                            for (long i = 0; i < snList.Length; i++)
                            {
                                if (this._status == FormStatus.Add
                                    && detial.SerialNo.ToUpper() == snList[i].ToUpper())
                                {
                                    exist = true;
                                    break;
                                }

                                if (this._status == FormStatus.Update
                                    && detial.Sequence != Int32.Parse(this._currRow.Cells[0].Text)
                                    && detial.SerialNo.ToUpper() == snList[i].ToUpper())
                                {
                                    exist = true;
                                    break;
                                }
                            }

                            if (exist)
                                break;
                        }

                        if (exist)
                        {
                            this.ShowMessage("$Error_CS_RunningCard_Range_Overlap");
                            ucLERCStart.TextFocus(false, true);
                            return false;
                        }
                    }

                    //同时要卡如果序列号重复,则不允许不存在BOM关系的料号对应的序列号重复(BOM关系用低阶向高阶展)
                    string existSN = string.Empty;
                    for (long i = 0; i < snList.Length; i++)
                    {
                        if (_MaterialFacade.QueryMKeyPartDetailCountNotInBOMRelation(((MKeyPart)obj).MItemCode.Trim().ToUpper(), snList[i].Trim().ToUpper()) > 0)
                        {
                            existSN = snList[i].Trim().ToUpper();
                            break;
                        }
                    }

                    if (existSN.Length > 0)
                    {
                        this.ShowMessage("$Error_CS_RunningCard_Range_Overlap2 $SERIAL_NO=" + existSN);
                        ucLERCStart.TextFocus(false, true);
                        return false;
                    }
                }

                //保存前的准备
                DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);
                int date = dbDateTime.DBDate;
                int time = dbDateTime.DBTime;
                string user = ApplicationService.Current().UserCode;

                ((MKeyPart)obj).MaintainDate = date;
                ((MKeyPart)obj).MaintainTime = time;
                ((MKeyPart)obj).MaintainUser = user;

                MKeyPartDetail detail = _MaterialFacade.CreateNewMKeyPartDetail();
                detail.PrintTimes = 0;
                detail.MaintainDate = date;
                detail.MaintainTime = time;
                detail.MaintainUser = user;
                detail.EAttribute1 = " ";

                this.DataProvider.BeginTransaction();

                if (this._status == FormStatus.Add)
                {
                    //this._MaterialFacade.AddMKeyPart((MKeyPart)obj);
                    this._MaterialFacade.AddMKeyPartTrace((MKeyPart)obj);
                }
                else if (this._status == FormStatus.Update)
                {
                    ((MKeyPart)obj).Sequence = Int32.Parse(this._currRow.Cells[0].Text);
                    object oldMkeyPart = _MaterialFacade.GetMKeyPart(((MKeyPart)obj).Sequence, ((MKeyPart)obj).MItemCode);
                    if (oldMkeyPart == null)
                    {
                        //this._MaterialFacade.AddMKeyPart((MKeyPart)obj);
                        this._MaterialFacade.AddMKeyPartTrace((MKeyPart)obj);
                    }
                    else
                    {
                        this._MaterialFacade.UpdateMKeyPart((MKeyPart)obj);                        
                    }
                }

                if (needUpdateDetail && snList != null)
                {
                    this._MaterialFacade.UpdateMKeyPartDetialEAttribute1(((MKeyPart)obj).MItemCode, ((MKeyPart)obj).Sequence, "N");

                    for (long i = 0; i < snList.Length; i++)
                    {
                        detail.MItemCode = ((MKeyPart)obj).MItemCode;
                        detail.Sequence = ((MKeyPart)obj).Sequence;
                        detail.SerialNo = snList[i];                        

                        object oldMkeyPartDetail = _MaterialFacade.GetMKeyPartDetail(detail.MItemCode, detail.SerialNo);
                        if (oldMkeyPartDetail == null)
                        {
                            ((MKeyPartDetail)detail).PrintTimes = 0;
                            this._MaterialFacade.AddMKeyPartDetail((MKeyPartDetail)detail);
                        }
                        else
                        {
                            ((MKeyPartDetail)detail).PrintTimes = ((MKeyPartDetail)oldMkeyPartDetail).PrintTimes;
                            this._MaterialFacade.UpdateMKeyPartDetail((MKeyPartDetail)detail);
                        }
                    }

                    this._MaterialFacade.DeleteMKeyPartDetail(((MKeyPart)obj).MItemCode, ((MKeyPart)obj).Sequence, "N");
                }                

                this.DataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                this.ShowMessage(ex);

                ucLERCStart.TextFocus(false, true);
                //Remove UCLabel.SelectAll;
                return false;
            }

            this.ShowMessage(new UserControl.Message(MessageType.Success, "$CS_Save_Success"));

            //查询
            this.ucLEItemCodeQuery.Value = ((MKeyPart)obj).MItemCode;
            this.ucDate.Value = DateTime.Today;
            this.requestData();

            return true;
        }

        private void ucBtnQuery_Click(object sender, System.EventArgs e)
        {
            if (this.checkQuery())
            {
                this.requestData();
                this.status = FormStatus.Ready;
            }
        }

        private bool checkQuery()
        {
            return true;
        }

        private void ucButtonCalcCount_Click(object sender, EventArgs e)
        {
            CalcSNCount();
        }

        private bool CalcSNCount()
        {
            //检查是否一致的长度，是否都输入了
            if (!CheckSNStartAndEnd())
            {
                ucLabelEditSNCount.Value = "";
                return false;
            }

            NumberScale scale = NumberScale.Scale34;
            if (ultraOptionSetScale.CheckedIndex == 0)
                scale = NumberScale.Scale10;
            else if (ultraOptionSetScale.CheckedIndex == 1)
                scale = NumberScale.Scale16;
            else if (ultraOptionSetScale.CheckedIndex == 2)
                scale = NumberScale.Scale34;

            long startSN = 0;
            try
            {
                startSN = long.Parse(NumberScaleHelper.ChangeNumber(ucLERCStart.Value.Trim(), scale, NumberScale.Scale10));
            }
            catch (Exception ex)
            {
                this.ShowMessage(ex);
                ucLERCStart.TextFocus(false, true);
                return false;
            }

            long endSN = 0;
            try
            {
                endSN = long.Parse(NumberScaleHelper.ChangeNumber(ucLERCEnd.Value.Trim(), scale, NumberScale.Scale10));
            }
            catch (Exception ex)
            {
                this.ShowMessage(ex);
                ucLERCEnd.TextFocus(false, true);
                return false;
            }

            if (startSN > endSN)
            {
                this.ShowMessage("$Error_CS_RunningCardStart_Greater_Than_RunningCardEnd");
                ucLERCEnd.TextFocus(false, true);
                return false;
            }

            long count = endSN - startSN + 1;
            ucLabelEditSNCount.Value = count.ToString();

            return true;
        }

        private void ucButtonCalcRCardEnd_Click(object sender, EventArgs e)
        {
            CalcRCardEnd();
        }

        private bool CalcRCardEnd()
        {
            //检查是否一致的长度，是否都输入了
            if (!CheckBeforeCalcRCardEnd())
            {
                return false;
            }

            NumberScale scale = NumberScale.Scale34;
            if (ultraOptionSetScale.CheckedIndex == 0)
                scale = NumberScale.Scale10;
            else if (ultraOptionSetScale.CheckedIndex == 1)
                scale = NumberScale.Scale16;
            else if (ultraOptionSetScale.CheckedIndex == 2)
                scale = NumberScale.Scale34;

            long startSN = 0;
            try
            {
                startSN = long.Parse(NumberScaleHelper.ChangeNumber(ucLERCStart.Value.Trim(), scale, NumberScale.Scale10));
            }
            catch (Exception ex)
            {
                this.ShowMessage(ex);
                ucLERCStart.TextFocus(false, true);
                return false;
            }

            long count = 0;
            try
            {
                count = long.Parse(ucLabelEditSNCount.Value.Trim());
            }
            catch (Exception ex)
            {
                this.ShowMessage(ex);
                ucLabelEditSNCount.TextFocus(false, true);
                return false;
            }

            long endSN = startSN + count - 1;

            try
            {
                string RCardEnd = NumberScaleHelper.ChangeNumber(endSN.ToString(), NumberScale.Scale10, scale);
                if (RCardEnd.Trim().Length < ucLERCStart.Value.Trim().Length)
                {
                    RCardEnd = RCardEnd.PadLeft(ucLERCStart.Value.Trim().Length, '0');                    
                }

                ucLERCEnd.Value = RCardEnd;

                if (RCardEnd.Trim().Length != ucLERCStart.Value.Trim().Length)
                {
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_RunningCard_Range_Should_be_Equal_Length"));
                    ucLabelEditSNCount.TextFocus(false, true);
                }                
            }
            catch (Exception ex)
            {
                this.ShowMessage(ex);
                ucLERCEnd.TextFocus(false, true);
                return false;
            }

            return true;
        }

        private bool CheckBeforeDeleteAndUpdate(List<MKeyPart> mKeyPartList)
        {
            bool returnValue = true;

            foreach (MKeyPart mKeyPart in mKeyPartList)
            {
                if (_MaterialFacade.CheckMKeyPartUsed(mKeyPart))
                {
                    returnValue = false;
                    break;
                }
            }

            return returnValue;
        }

        #endregion

        #region DataSource

        private void requestData()
        {
            this.GridBind();
        }

        private void GridBind()
        {
            object[] objs = this.LoadDataSource();

            dtMKeyparts.Rows.Clear();

            if (objs == null)
            {
                this.ShowMessage("$CS_No_Data_To_Display");//没有符合查询条件的记录
                return;
            }

            foreach (object obj in objs)
            {
                dtMKeyparts.Rows.Add(this.GetRow(obj));
            }
            dtMKeyparts.DefaultView.Sort = "MaintainDate DESC,MaintainTime DESC";

            this.txtSumNum.Value = objs.Length.ToString();
        }

        private object[] LoadDataSource()
        {
            try
            {
                DataCollect.DataCollectFacade dataCollectFacade = new BenQGuru.eMES.DataCollect.DataCollectFacade(this.DataProvider);
                string sourceRCard = dataCollectFacade.GetSourceCard(ucLabelEditRCardQuery.Value.Trim().ToUpper(), string.Empty);

                //Laws Lu,2005/12/16,修改	添加工单
                return this._MaterialFacade.QueryMKeyPartByMo(this.ucLEItemCodeQuery.Value.Trim().ToUpper(),
                    sourceRCard.Trim().ToUpper(),
                    ucMoCode.Value.Trim().ToUpper(),
                    GetDateToInt());
            }
            catch (Exception ex)
            {
                this.ShowMessage(ex);
            }

            return null;
        }

        private int GetCount()
        {
            try
            {
                return this._MaterialFacade.QueryMKeyPartCount(this.ucLEItemCodeQuery.Value.Trim().ToUpper(),
                    string.Empty, FormatHelper.TODateInt(this.ucDate.Value));
            }
            catch (Exception ex)
            {
                this.ShowMessage(ex);
            }

            return 0;
        }

        private object[] GetRow(object obj)
        {
            if (obj == null)
            {
                return new object[] { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" };
            }

            return new object[] {
			    ((MKeyPart)obj).Sequence,
                ((MKeyPart)obj).RCardPrefix,
			    ((MKeyPart)obj).RunningCardStart,
			    ((MKeyPart)obj).RunningCardEnd,
                ((MKeyPart)obj).SNScale,
			    ((MKeyPart)obj).MItemCode,
			    ((MKeyPart)obj).LotNO,
			    ((MKeyPart)obj).VendorCode,
			    ((MKeyPart)obj).VendorItemCode,
			    ((MKeyPart)obj).DateCode,
			    ((MKeyPart)obj).Version,
			    ((MKeyPart)obj).BIOS,
			    ((MKeyPart)obj).PCBA,
			    ((MKeyPart)obj).MaintainUser,
			    ((MKeyPart)obj).MaintainDate,
			    ((MKeyPart)obj).MaintainTime,
			    ((MKeyPart)obj).MoCode,
			    ((MKeyPart)obj).MITEMNAME,
								};
        }

        #endregion

        #region Object <--> Form

        protected object GetEditObject()
        {
            if (this.ValidateInput() == false)
            {
                return null;
            }

            MKeyPart mKeyPart = this._MaterialFacade.CreateNewMKeyPart();

            mKeyPart.RCardPrefix = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.ucLabelEditRCardPrefix.Value.Trim(),100));
            mKeyPart.RunningCardStart = this.ucLERCStart.Value.Trim();
            mKeyPart.RunningCardEnd = this.ucLERCEnd.Value.Trim();
            mKeyPart.MItemCode = this.ucLEItemCode.Value.ToUpper().Trim();
            mKeyPart.LotNO = this.ucLELotNo.Value.Trim();
            mKeyPart.DateCode = this.ucLEDateCode.Value.Trim();
            mKeyPart.VendorCode = this.ucLEVendorCode.Value.Trim();
            mKeyPart.VendorItemCode = this.ucLEVendorItem.Value.Trim();
            mKeyPart.Version = this.ucLEVersion.Value.Trim();
            mKeyPart.BIOS = this.ucLEBIOS.Value.Trim();
            mKeyPart.PCBA = this.ucLEPCBA.Value.Trim();
            mKeyPart.MoCode = this.ucByMo.Value.Trim().ToUpper();
            mKeyPart.MaintainUser = ApplicationService.Current().UserCode;

            switch (this.ultraOptionSetScale.CheckedIndex)
            {
                case 0:
                    mKeyPart.SNScale = "10";
                    break;
                case 1:
                    mKeyPart.SNScale = "16";
                    break;
                case 2:
                    mKeyPart.SNScale = "34";
                    break;
                default:
                    mKeyPart.SNScale = "34";
                    break;
            }

            if (this.ucLEItemCode.Value != String.Empty)
            {
                //object obj = (new OPBOMFacade(DataProvider)).GetOPBOMDetailByBItemCode(
                //    ucByMo.Value.Trim().ToUpper(),
                //    ucLEItemCode.Value.Trim().ToUpper());

                //if (obj != null)
                //{
                //    lblMitemName.Text = (obj as OPBOMDetail).OPBOMItemName;
                //    mKeyPart.MITEMNAME = lblMitemName.Text;
                //}

                Domain.MOModel.Material material = (Domain.MOModel.Material)_ItemFacade.GetMaterial(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.ucLEItemCode.Value)), GlobalVariables.CurrentOrganizations.First().OrganizationID);

                if (material != null)
                {
                    lblMitemName.Text = material.MaterialDescription;
                    mKeyPart.MITEMNAME = lblMitemName.Text;
 
                }
            }

            return mKeyPart;
        }

        protected object GetEditObject(Infragistics.Win.UltraWinGrid.UltraGridRow row)
        {
            if (row == null)
            {
                return null;
            }

            object obj = _MaterialFacade.GetMKeyPart(System.Int32.Parse(row.Cells[0].Text), row.Cells[5].Text);

            if (obj != null)
            {
                return (MKeyPart)obj;
            }

            return null;
        }

        protected void SetEditObject(object obj)
        {
            if (obj == null)
            {
                this.ucLERCStart.Value = "";
                this.ucLERCEnd.Value = "";

                if (btnLockControl.Caption == "解除锁定" && this.status == FormStatus.Add)
                {
                    return;
                }
                this.ucLEItemCode.Value = "";
                this.ucLELotNo.Value = "";
                this.ucLEDateCode.Value = "";
                this.ucLEVendorCode.Value = "";
                this.ucLEVendorItem.Value = "";
                this.ucLEVersion.Value = "";
                this.ucLEBIOS.Value = "";
                this.ucLEPCBA.Value = "";
                this.lblMitemName.Text = "";
                this.ucLabelEditRCardPrefix.Value = "";

                return;
            }

            this.ucLERCStart.Value = ((MKeyPart)obj).RunningCardStart;
            this.ucLERCEnd.Value = ((MKeyPart)obj).RunningCardEnd;
            this.ucLELotNo.Value = ((MKeyPart)obj).LotNO;
            this.ucLEDateCode.Value = ((MKeyPart)obj).DateCode;
            this.ucLEVendorCode.Value = ((MKeyPart)obj).VendorCode;
            this.ucLEVendorItem.Value = ((MKeyPart)obj).VendorItemCode;
            this.ucLEVersion.Value = ((MKeyPart)obj).Version;
            this.ucLEBIOS.Value = ((MKeyPart)obj).BIOS;
            this.ucLEPCBA.Value = ((MKeyPart)obj).PCBA;
            this.ucLEItemCode.Value = ((MKeyPart)obj).MItemCode;
            this.lblMitemName.Text = ((MKeyPart)obj).MITEMNAME;
            this.ucLabelEditRCardPrefix.Value = ((MKeyPart)obj).RCardPrefix;

            switch (((MKeyPart)obj).SNScale)
            {
                case "10":
                    this.ultraOptionSetScale.CheckedIndex = 0;
                    break;
                case "16":
                    this.ultraOptionSetScale.CheckedIndex = 1;
                    break;
                case "34":
                    this.ultraOptionSetScale.CheckedIndex = 2;
                    break;
                default:
                    this.ultraOptionSetScale.CheckedIndex = 2;
                    break;
            }

            if (((MKeyPart)obj).MoCode != String.Empty)
            {
                this.ucByMo.Checked = true;

                this.ucByMo.Value = ((MKeyPart)obj).MoCode;
            }
            else
            {
                this.ucByMo.Checked = false;

                this.ucByMo.Value = String.Empty;
            }
        }

        protected bool ValidateInput()
        {
            bool validate = true;

            if (this.ucLEItemCode.Value.Trim() == string.Empty)
            {
                this.ShowMessage(new UserControl.Message(MessageType.Error, "$CS_MItemCode$Error_Input_Empty"));
                ucLEItemCode.TextFocus(false, true);
                validate = false;
                return validate;
            }

            if (!checkItemCode())
            {
                this.ShowMessage(new UserControl.Message(MessageType.Error, "$Error_CS_OPBOMItem_Not_Exist"));
                ucLEItemCode.TextFocus(false, true);
                validate = false;
                return validate;
            }

            if (this.ucLabelEditRCardPrefix.Value.Trim() == string.Empty)
            {
                this.ShowMessage(new UserControl.Message(MessageType.Error, "$CS_RCardPrefix$Error_Input_Empty"));
                ucLabelEditRCardPrefix.TextFocus(false, true);
                validate = false;
                return validate;
            }

            validate = CheckSNStartAndEnd();
            if (!validate)
                return validate;

            if (this.ucByMo.Checked)
            {
                validate = CheckMO();
                if (!validate)
                    return validate;
            }

            //if (validate)
            //{
            //    if (!this.checkItemCode())
            //    {
            //        validate = false;
            //        return validate;
            //    }
            //}

            return validate;
        }

        private bool CheckMO()
        {
            //必须输入
            if (ucByMo.Value.Trim().Length <= 0)
            {
                this.ShowMessage(new UserControl.Message(MessageType.Error, "$CS_CMPleaseInputMO"));
                this.ucByMo.TextFocus(false, true);
                return false;
            }

            //检查工单是否存在
            MO mo = (MO)_MOFacade.GetMO(ucByMo.Value.Trim());
            if (mo == null)
            {
                this.ShowMessage(new UserControl.Message(MessageType.Error, "$CS_MO_Not_Exist"));
                this.ucByMo.TextFocus(false, true);
                return false;
            }

            //检查当前所备料是否在MO的SBOM中            
            object item = _SBOMFacade.GetSBOM(mo.ItemCode, this.ucLEItemCode.Value.Trim().ToUpper(),
                string.Empty, string.Empty, string.Empty,
                mo.OrganizationID, mo.BOMVersion);
            if (item == null)
            {
                this.ShowMessage(new UserControl.Message(MessageType.Error, "$CS_Error_ItemNotInMOSBOM"));
                this.ucByMo.TextFocus(false, true);
                return false;
            }

            return true;
        }

        private bool CheckSNStartAndEnd()
        {
            if (this.ucLERCStart.Value.Trim() == string.Empty)
            {
                this.ShowMessage(new UserControl.Message(MessageType.Error, "$CS_RunningCardStart$Error_Input_Empty"));
                ucLERCStart.TextFocus(false, true);
                return false;
            }

            if (this.ucLERCEnd.Value.Trim() == string.Empty)
            {
                this.ShowMessage(new UserControl.Message(MessageType.Error, "$CS_RunningCardEnd$Error_Input_Empty"));
                ucLERCEnd.TextFocus(false, true);
                return false;
            }

            if (this.ucLERCStart.Value.Trim().Length != this.ucLERCEnd.Value.Trim().Length)
            {
                this.ShowMessage(new UserControl.Message(MessageType.Error, "$CS_RunningCard_Range_Should_be_Equal_Length"));
                ucLERCStart.TextFocus(false, true);
                return false;
            }

            return true;
        }

        private bool CheckBeforeCalcRCardEnd()
        {
            if (this.ucLERCStart.Value.Trim() == string.Empty)
            {
                this.ShowMessage(new UserControl.Message(MessageType.Error, "$CS_RunningCardStart$Error_Input_Empty"));
                ucLERCStart.TextFocus(false, true);
                return false;
            }

            if (this.ucLabelEditSNCount.Value.Trim() == string.Empty)
            {
                this.ShowMessage(new UserControl.Message(MessageType.Error, "$CS_RunningCardCount$Error_Input_Empty"));
                ucLabelEditSNCount.TextFocus(false, true);
                return false;
            }

            return true;
        }

        #endregion

        #region Function

        private bool checkItemCode()
        {
            bool result = true;

            Domain.MOModel.Material material = (Domain.MOModel.Material)_ItemFacade.GetMaterial(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.ucLEItemCode.Value)), GlobalVariables.CurrentOrganizations.First().OrganizationID);

            if (material==null)
                result = false;
            return result;
        }

        private void ChangeDateStatus()
        {
            if (chkNeedDate.Checked == true)
            {
                ucDate.Enabled = true;
            }
            else
            {
                ucDate.Enabled = false;
            }
        }

        private bool CheckID(string startCard, string endCard)
        {
            bool passCheck = false;
            //长度检查
            if (bRCardLenULE.Checked && bRCardLenULE.Value.Trim() != string.Empty)
            {
                int len = 0;
                try
                {
                    len = int.Parse(bRCardLenULE.Value.Trim());
                    if (ucLabelEditRCardPrefix.Value.Length + startCard.Length != len)
                    {
                        passCheck = false;
                        ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_MCARD_Len_NotCompare"));
                        ucLERCStart.TextFocus(false, true);
                        //Remove UCLabel.SelectAll;
                        return passCheck;
                    }

                    if (ucLabelEditRCardPrefix.Value.Length + endCard.Length != len)
                    {
                        passCheck = false;
                        ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_endMCARD_Len_NotCompare"));
                        ucLERCStart.TextFocus(false, true);
                        //Remove UCLabel.SelectAll;
                        return passCheck;
                    }
                }
                catch
                {
                    return passCheck;
                }
            }

            passCheck = true;

            return passCheck;

        }

        private int GetDateToInt()
        {
            int iReturn = 0;
            if (chkNeedDate.Checked == true)
            {
                iReturn = FormatHelper.TODateInt(this.ucDate.Value);
            }

            return iReturn;
        }

        private void InitEditControls(bool clearPublic)
        {
            if (clearPublic)
            {
                ucLEItemCode.Value = string.Empty;
                lblMitemName.Text = String.Empty;
            }

            ucLELotNo.Value = string.Empty;
            ucLEDateCode.Value = string.Empty;
            ucLEVendorCode.Value = string.Empty;
            ucLEVendorItem.Value = string.Empty;
            ucLEVersion.Value = string.Empty;
            ucLEPCBA.Value = string.Empty;
            ucLEBIOS.Value = string.Empty;

            ucLabelEditSNCount.Value = string.Empty;

            ucLabelEditRCardPrefix.Value = string.Empty;
            ucLERCStart.Value = string.Empty;
            ucLERCEnd.Value = string.Empty;
            ucByMo.Value = String.Empty;
            ucByMo.Checked = false;
            //bRCardLenULE.Value = String.Empty;
            //bRCardLenULE.Checked = false;

            //ultraOptionSetScale.CheckedIndex = 0;
        }

        #endregion

        

    }

    public class FormStatus
    {
        public static string Add = "Add";
        public static string Update = "Update";
        public static string Ready = "Ready";
        public static string Noready = "Noready";
    }
}

