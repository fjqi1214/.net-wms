#region  system
using System;
using System.IO;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
#endregion

#region project
using BenQGuru.eMES.Domain.OQC;
using BenQGuru.eMES.OQC;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Client.Service;
using BenQGuru.eMES.Common.Domain;
using UserControl;
using BenQGuru.eMES.Common.DomainDataProvider;
using System.Data;
using System.Collections.Generic;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Domain.MOModel;
#endregion

namespace BenQGuru.eMES.Client
{
    /// <summary>
    /// FOQCDetail 的摘要说明。
    /// </summary>
    public class FOQCSamplePlan : BaseForm
    {
        private System.Windows.Forms.GroupBox groupBox4;
        private UserControl.UCButton ucButtonExit;
        private UserControl.UCButton ucButtonOK;
        public UserControl.UCLabelEdit ucLabEditSampleQty;
        private UserControl.UCLabelEdit ucLabEditLotQty;
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.Container components = null;
        private UserControl.UCButton btnShowCollectOQC;
        public System.Windows.Forms.ComboBox cbbLotNO;
        private System.Windows.Forms.Label lblLotNo;
        private UserControl.UCLabelEdit txtMemo;
        private UserControl.UCLabelEdit txtRcard;
        private UserControl.UCButton btnGetLot;
        private System.Windows.Forms.GroupBox productInfo;
        private System.Windows.Forms.RadioButton rbMass;
        private System.Windows.Forms.RadioButton rbTry;
        UltraWinGridHelper ultraWinGridHelper;
        DataTable dtCheckItem = new DataTable();
        private GroupBox lotType;
        private RadioButton rbRelapse;
        private RadioButton rbNormal;
        private RadioButton rbNew;
        private UCLabelEdit ucLabelEditSizeAndCapacity;
        private Label labelLotType;
        private Label labelProductionType;
        private UCLabelEdit txtCartonCode;
        private GroupBox templete;
        private UCLabelEdit ucLabEditAC3;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGridOQCCheckType;
        private UCLabelEdit ucLabEditAC4;
        private UCLabelEdit ucLabEditRE1;
        private UCLabelEdit ucLabEditAQL4;
        private UCLabelEdit ucLabEditAQL1;
        private UCLabelEdit ucLabEditRE4;
        private UCLabelEdit ucLabEditAC1;
        private Label labelZ;
        private UCLabelEdit ucLabEditRE2;
        private UCButton btnSaveConfig;
        private UCButton btnLoadConfig;
        private UCLabelEdit ucLabEditAQL2;
        private UCLabelEdit ucLabEditAC2;
        private UCLabelEdit ucLabEditRE3;
        private Label labelA;
        private Label labelB;
        private Label labelC;
        private UCLabelEdit ucLabEditAQL3;
        private IDomainDataProvider _domainDataProvider = ApplicationService.Current().DataProvider;
        public IDomainDataProvider DataProvider
        {
            get
            {
                return _domainDataProvider;
            }
        }


        public string oqcLotNo //= string.Empty;
        {
            get
            {
                return this.cbbLotNO.Text;//this.ucLabEditLotNO.Value;
            }
            set
            {
                this.cbbLotNO.Text = value; //this.ucLabEditLotNO.Value = value;
            }
        }

        public FOQCSamplePlan()
        {
            //
            // Windows 窗体设计器支持所必需的
            //
            InitializeComponent();

            //
            // TODO: 在 InitializeComponent 调用后添加任何构造函数代码
            //
            UserControl.UIStyleBuilder.FormUI(this);
            
            this.ultraGridOQCCheckType.DisplayLayout.Appearance.BackColor = System.Drawing.Color.Gainsboro;
            this.ultraGridOQCCheckType.DisplayLayout.CaptionAppearance.BackColor = Color.FromName("WhiteSmoke");
            //this.ultraGridMain.DisplayLayout.Appearance.BackColor = Color.FromArgb(255, 255, 255);
            this.ultraGridOQCCheckType.DisplayLayout.Override.HeaderAppearance.BackColor = Color.FromName("WhiteSmoke");
            this.ultraGridOQCCheckType.DisplayLayout.Override.RowAppearance.BackColor = Color.FromArgb(230, 234, 245);
            this.ultraGridOQCCheckType.DisplayLayout.Override.RowAlternateAppearance.BackColor = Color.FromArgb(255, 255, 255);
            this.ultraGridOQCCheckType.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.ultraGridOQCCheckType.DisplayLayout.Override.ActiveRowAppearance.BackColor = System.Drawing.Color.Gainsboro;
            this.ultraGridOQCCheckType.DisplayLayout.Override.ActiveRowAppearance.ForeColor = System.Drawing.Color.Black;
            this.ultraGridOQCCheckType.DisplayLayout.ScrollBarLook.Appearance.BackColor = Color.FromName("LightGray");

        }

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

        #region Windows 窗体设计器生成的代码
        /// <summary>
        /// 设计器支持所需的方法 - 不要使用代码编辑器修改
        /// 此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.GroupBox groupBox5;
            System.Windows.Forms.Label lblLotType;
            System.Windows.Forms.Label lblProductionType;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FOQCSamplePlan));
            this.labelLotType = new System.Windows.Forms.Label();
            this.labelProductionType = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnShowCollectOQC = new UserControl.UCButton();
            this.ucButtonExit = new UserControl.UCButton();
            this.ucButtonOK = new UserControl.UCButton();
            this.cbbLotNO = new System.Windows.Forms.ComboBox();
            this.lblLotNo = new System.Windows.Forms.Label();
            this.productInfo = new System.Windows.Forms.GroupBox();
            this.rbNew = new System.Windows.Forms.RadioButton();
            this.rbTry = new System.Windows.Forms.RadioButton();
            this.rbMass = new System.Windows.Forms.RadioButton();
            this.lotType = new System.Windows.Forms.GroupBox();
            this.rbRelapse = new System.Windows.Forms.RadioButton();
            this.rbNormal = new System.Windows.Forms.RadioButton();
            this.ucLabelEditSizeAndCapacity = new UserControl.UCLabelEdit();
            this.btnGetLot = new UserControl.UCButton();
            this.txtRcard = new UserControl.UCLabelEdit();
            this.txtMemo = new UserControl.UCLabelEdit();
            this.ucLabEditLotQty = new UserControl.UCLabelEdit();
            this.ucLabEditSampleQty = new UserControl.UCLabelEdit();
            this.txtCartonCode = new UserControl.UCLabelEdit();
            this.templete = new System.Windows.Forms.GroupBox();
            this.ucLabEditAC3 = new UserControl.UCLabelEdit();
            this.ultraGridOQCCheckType = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.ucLabEditAC4 = new UserControl.UCLabelEdit();
            this.ucLabEditRE1 = new UserControl.UCLabelEdit();
            this.ucLabEditAQL4 = new UserControl.UCLabelEdit();
            this.ucLabEditAQL1 = new UserControl.UCLabelEdit();
            this.ucLabEditRE4 = new UserControl.UCLabelEdit();
            this.ucLabEditAC1 = new UserControl.UCLabelEdit();
            this.labelZ = new System.Windows.Forms.Label();
            this.ucLabEditRE2 = new UserControl.UCLabelEdit();
            this.btnSaveConfig = new UserControl.UCButton();
            this.btnLoadConfig = new UserControl.UCButton();
            this.ucLabEditAQL2 = new UserControl.UCLabelEdit();
            this.ucLabEditAC2 = new UserControl.UCLabelEdit();
            this.ucLabEditRE3 = new UserControl.UCLabelEdit();
            this.labelA = new System.Windows.Forms.Label();
            this.labelB = new System.Windows.Forms.Label();
            this.labelC = new System.Windows.Forms.Label();
            this.ucLabEditAQL3 = new UserControl.UCLabelEdit();
            groupBox5 = new System.Windows.Forms.GroupBox();
            lblLotType = new System.Windows.Forms.Label();
            lblProductionType = new System.Windows.Forms.Label();
            groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.productInfo.SuspendLayout();
            this.lotType.SuspendLayout();
            this.templete.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridOQCCheckType)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(this.labelLotType);
            groupBox5.Controls.Add(this.labelProductionType);
            groupBox5.Controls.Add(lblLotType);
            groupBox5.Controls.Add(lblProductionType);
            groupBox5.Location = new System.Drawing.Point(31, 441);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new System.Drawing.Size(634, 56);
            groupBox5.TabIndex = 191;
            groupBox5.TabStop = false;
            // 
            // labelLotType
            // 
            this.labelLotType.AutoSize = true;
            this.labelLotType.Location = new System.Drawing.Point(377, 30);
            this.labelLotType.Name = "labelLotType";
            this.labelLotType.Size = new System.Drawing.Size(0, 12);
            this.labelLotType.TabIndex = 3;
            // 
            // labelProductionType
            // 
            this.labelProductionType.AutoSize = true;
            this.labelProductionType.Location = new System.Drawing.Point(91, 30);
            this.labelProductionType.Name = "labelProductionType";
            this.labelProductionType.Size = new System.Drawing.Size(0, 12);
            this.labelProductionType.TabIndex = 2;
            // 
            // lblLotType
            // 
            lblLotType.AutoSize = true;
            lblLotType.Location = new System.Drawing.Point(318, 30);
            lblLotType.Name = "lblLotType";
            lblLotType.Size = new System.Drawing.Size(53, 12);
            lblLotType.TabIndex = 1;
            lblLotType.Text = "批类型：";
            // 
            // lblProductionType
            // 
            lblProductionType.AutoSize = true;
            lblProductionType.Location = new System.Drawing.Point(20, 30);
            lblProductionType.Name = "lblProductionType";
            lblProductionType.Size = new System.Drawing.Size(65, 12);
            lblProductionType.TabIndex = 0;
            lblProductionType.Text = "生产类型：";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnShowCollectOQC);
            this.groupBox4.Controls.Add(this.ucButtonExit);
            this.groupBox4.Controls.Add(this.ucButtonOK);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox4.Location = new System.Drawing.Point(0, 627);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(734, 56);
            this.groupBox4.TabIndex = 20;
            this.groupBox4.TabStop = false;
            // 
            // btnShowCollectOQC
            // 
            this.btnShowCollectOQC.BackColor = System.Drawing.SystemColors.Control;
            this.btnShowCollectOQC.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnShowCollectOQC.BackgroundImage")));
            this.btnShowCollectOQC.ButtonType = UserControl.ButtonTypes.None;
            this.btnShowCollectOQC.Caption = "OQC数据采集";
            this.btnShowCollectOQC.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnShowCollectOQC.Location = new System.Drawing.Point(510, 20);
            this.btnShowCollectOQC.Name = "btnShowCollectOQC";
            this.btnShowCollectOQC.Size = new System.Drawing.Size(88, 22);
            this.btnShowCollectOQC.TabIndex = 2;
            this.btnShowCollectOQC.Visible = false;
            this.btnShowCollectOQC.Click += new System.EventHandler(this.btnShowCollectOQC_Click);
            // 
            // ucButtonExit
            // 
            this.ucButtonExit.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonExit.BackgroundImage")));
            this.ucButtonExit.ButtonType = UserControl.ButtonTypes.Exit;
            this.ucButtonExit.Caption = "退出";
            this.ucButtonExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonExit.Location = new System.Drawing.Point(371, 20);
            this.ucButtonExit.Name = "ucButtonExit";
            this.ucButtonExit.Size = new System.Drawing.Size(88, 22);
            this.ucButtonExit.TabIndex = 1;
            // 
            // ucButtonOK
            // 
            this.ucButtonOK.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonOK.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonOK.BackgroundImage")));
            this.ucButtonOK.ButtonType = UserControl.ButtonTypes.Confirm;
            this.ucButtonOK.Caption = "确认";
            this.ucButtonOK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonOK.Location = new System.Drawing.Point(226, 20);
            this.ucButtonOK.Name = "ucButtonOK";
            this.ucButtonOK.Size = new System.Drawing.Size(88, 22);
            this.ucButtonOK.TabIndex = 0;
            this.ucButtonOK.Click += new System.EventHandler(this.ucButtonOK_Click);
            // 
            // cbbLotNO
            // 
            this.cbbLotNO.Location = new System.Drawing.Point(104, 13);
            this.cbbLotNO.Name = "cbbLotNO";
            this.cbbLotNO.Size = new System.Drawing.Size(400, 20);
            this.cbbLotNO.TabIndex = 0;
            this.cbbLotNO.Leave += new System.EventHandler(this.cbbLotNO_Leave);
            this.cbbLotNO.Enter += new System.EventHandler(this.cbbLotNO_Enter);
            this.cbbLotNO.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cbbLotNO_KeyUp);
            this.cbbLotNO.SelectedValueChanged += new System.EventHandler(this.cbbLotNO_SelectedValueChanged);
            this.cbbLotNO.Click += new System.EventHandler(this.cbbLotNO_Click);
            // 
            // lblLotNo
            // 
            this.lblLotNo.Location = new System.Drawing.Point(32, 17);
            this.lblLotNo.Name = "lblLotNo";
            this.lblLotNo.Size = new System.Drawing.Size(66, 20);
            this.lblLotNo.TabIndex = 178;
            this.lblLotNo.Text = "抽检批号";
            this.lblLotNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // productInfo
            // 
            this.productInfo.Controls.Add(this.rbNew);
            this.productInfo.Controls.Add(this.rbTry);
            this.productInfo.Controls.Add(this.rbMass);
            this.productInfo.Location = new System.Drawing.Point(31, 441);
            this.productInfo.Name = "productInfo";
            this.productInfo.Size = new System.Drawing.Size(314, 56);
            this.productInfo.TabIndex = 16;
            this.productInfo.TabStop = false;
            this.productInfo.Text = "生产类型";
            this.productInfo.Visible = false;
            // 
            // rbNew
            // 
            this.rbNew.Checked = true;
            this.rbNew.Location = new System.Drawing.Point(22, 24);
            this.rbNew.Name = "rbNew";
            this.rbNew.Size = new System.Drawing.Size(90, 24);
            this.rbNew.TabIndex = 0;
            this.rbNew.TabStop = true;
            this.rbNew.Text = "新品批";
            this.rbNew.Visible = false;
            // 
            // rbTry
            // 
            this.rbTry.Location = new System.Drawing.Point(112, 24);
            this.rbTry.Name = "rbTry";
            this.rbTry.Size = new System.Drawing.Size(90, 24);
            this.rbTry.TabIndex = 1;
            this.rbTry.Text = "试流批";
            this.rbTry.Visible = false;
            // 
            // rbMass
            // 
            this.rbMass.Location = new System.Drawing.Point(202, 24);
            this.rbMass.Name = "rbMass";
            this.rbMass.Size = new System.Drawing.Size(90, 24);
            this.rbMass.TabIndex = 2;
            this.rbMass.Text = "量产批";
            this.rbMass.Visible = false;
            // 
            // lotType
            // 
            this.lotType.Controls.Add(this.rbRelapse);
            this.lotType.Controls.Add(this.rbNormal);
            this.lotType.Location = new System.Drawing.Point(351, 441);
            this.lotType.Name = "lotType";
            this.lotType.Size = new System.Drawing.Size(314, 56);
            this.lotType.TabIndex = 17;
            this.lotType.TabStop = false;
            this.lotType.Text = "批类型";
            this.lotType.Visible = false;
            // 
            // rbRelapse
            // 
            this.rbRelapse.Location = new System.Drawing.Point(131, 24);
            this.rbRelapse.Name = "rbRelapse";
            this.rbRelapse.Size = new System.Drawing.Size(104, 24);
            this.rbRelapse.TabIndex = 1;
            this.rbRelapse.Text = "复检批";
            this.rbRelapse.Visible = false;
            // 
            // rbNormal
            // 
            this.rbNormal.Checked = true;
            this.rbNormal.Location = new System.Drawing.Point(27, 24);
            this.rbNormal.Name = "rbNormal";
            this.rbNormal.Size = new System.Drawing.Size(104, 24);
            this.rbNormal.TabIndex = 0;
            this.rbNormal.TabStop = true;
            this.rbNormal.Text = "正常批";
            this.rbNormal.Visible = false;
            // 
            // ucLabelEditSizeAndCapacity
            // 
            this.ucLabelEditSizeAndCapacity.AllowEditOnlyChecked = true;
            this.ucLabelEditSizeAndCapacity.AutoSelectAll = false;
            this.ucLabelEditSizeAndCapacity.AutoUpper = true;
            this.ucLabelEditSizeAndCapacity.Caption = "实际批量";
            this.ucLabelEditSizeAndCapacity.Checked = false;
            this.ucLabelEditSizeAndCapacity.EditType = UserControl.EditTypes.String;
            this.ucLabelEditSizeAndCapacity.Enabled = false;
            this.ucLabelEditSizeAndCapacity.Location = new System.Drawing.Point(43, 103);
            this.ucLabelEditSizeAndCapacity.MaxLength = 40;
            this.ucLabelEditSizeAndCapacity.Multiline = false;
            this.ucLabelEditSizeAndCapacity.Name = "ucLabelEditSizeAndCapacity";
            this.ucLabelEditSizeAndCapacity.PasswordChar = '\0';
            this.ucLabelEditSizeAndCapacity.ReadOnly = false;
            this.ucLabelEditSizeAndCapacity.ShowCheckBox = false;
            this.ucLabelEditSizeAndCapacity.Size = new System.Drawing.Size(161, 26);
            this.ucLabelEditSizeAndCapacity.TabIndex = 190;
            this.ucLabelEditSizeAndCapacity.TabNext = true;
            this.ucLabelEditSizeAndCapacity.Value = "";
            this.ucLabelEditSizeAndCapacity.WidthType = UserControl.WidthTypes.Small;
            this.ucLabelEditSizeAndCapacity.XAlign = 104;
            // 
            // btnGetLot
            // 
            this.btnGetLot.BackColor = System.Drawing.SystemColors.Control;
            this.btnGetLot.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnGetLot.BackgroundImage")));
            this.btnGetLot.ButtonType = UserControl.ButtonTypes.None;
            this.btnGetLot.Caption = "获取批";
            this.btnGetLot.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGetLot.Location = new System.Drawing.Point(528, 75);
            this.btnGetLot.Name = "btnGetLot";
            this.btnGetLot.Size = new System.Drawing.Size(88, 22);
            this.btnGetLot.TabIndex = 181;
            this.btnGetLot.Click += new System.EventHandler(this.btnGetLot_Click);
            // 
            // txtRcard
            // 
            this.txtRcard.AllowEditOnlyChecked = true;
            this.txtRcard.AutoSelectAll = false;
            this.txtRcard.AutoUpper = true;
            this.txtRcard.Caption = "产品序列号";
            this.txtRcard.Checked = false;
            this.txtRcard.EditType = UserControl.EditTypes.String;
            this.txtRcard.Location = new System.Drawing.Point(31, 75);
            this.txtRcard.MaxLength = 40;
            this.txtRcard.Multiline = false;
            this.txtRcard.Name = "txtRcard";
            this.txtRcard.PasswordChar = '\0';
            this.txtRcard.ReadOnly = false;
            this.txtRcard.ShowCheckBox = false;
            this.txtRcard.Size = new System.Drawing.Size(473, 25);
            this.txtRcard.TabIndex = 1;
            this.txtRcard.TabNext = true;
            this.txtRcard.Value = "";
            this.txtRcard.WidthType = UserControl.WidthTypes.TooLong;
            this.txtRcard.XAlign = 104;
            this.txtRcard.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRcard_TxtboxKeyPress);
            // 
            // txtMemo
            // 
            this.txtMemo.AllowEditOnlyChecked = true;
            this.txtMemo.AutoSelectAll = false;
            this.txtMemo.AutoUpper = true;
            this.txtMemo.Caption = "备注";
            this.txtMemo.Checked = false;
            this.txtMemo.EditType = UserControl.EditTypes.String;
            this.txtMemo.Location = new System.Drawing.Point(31, 502);
            this.txtMemo.MaxLength = 100;
            this.txtMemo.Multiline = true;
            this.txtMemo.Name = "txtMemo";
            this.txtMemo.PasswordChar = '\0';
            this.txtMemo.ReadOnly = false;
            this.txtMemo.ShowCheckBox = true;
            this.txtMemo.Size = new System.Drawing.Size(453, 39);
            this.txtMemo.TabIndex = 15;
            this.txtMemo.TabNext = true;
            this.txtMemo.Value = "";
            this.txtMemo.WidthType = UserControl.WidthTypes.TooLong;
            this.txtMemo.XAlign = 84;
            // 
            // ucLabEditLotQty
            // 
            this.ucLabEditLotQty.AllowEditOnlyChecked = true;
            this.ucLabEditLotQty.AutoSelectAll = false;
            this.ucLabEditLotQty.AutoUpper = true;
            this.ucLabEditLotQty.Caption = "批量";
            this.ucLabEditLotQty.Checked = false;
            this.ucLabEditLotQty.EditType = UserControl.EditTypes.String;
            this.ucLabEditLotQty.Location = new System.Drawing.Point(544, 107);
            this.ucLabEditLotQty.MaxLength = 40;
            this.ucLabEditLotQty.Multiline = false;
            this.ucLabEditLotQty.Name = "ucLabEditLotQty";
            this.ucLabEditLotQty.PasswordChar = '\0';
            this.ucLabEditLotQty.ReadOnly = true;
            this.ucLabEditLotQty.ShowCheckBox = false;
            this.ucLabEditLotQty.Size = new System.Drawing.Size(170, 24);
            this.ucLabEditLotQty.TabIndex = 2;
            this.ucLabEditLotQty.TabNext = true;
            this.ucLabEditLotQty.Value = "";
            this.ucLabEditLotQty.Visible = false;
            this.ucLabEditLotQty.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabEditLotQty.XAlign = 581;
            // 
            // ucLabEditSampleQty
            // 
            this.ucLabEditSampleQty.AllowEditOnlyChecked = true;
            this.ucLabEditSampleQty.AutoSelectAll = false;
            this.ucLabEditSampleQty.AutoUpper = true;
            this.ucLabEditSampleQty.Caption = "样本（套）数";
            this.ucLabEditSampleQty.Checked = false;
            this.ucLabEditSampleQty.EditType = UserControl.EditTypes.Integer;
            this.ucLabEditSampleQty.Location = new System.Drawing.Point(313, 107);
            this.ucLabEditSampleQty.MaxLength = 40;
            this.ucLabEditSampleQty.Multiline = false;
            this.ucLabEditSampleQty.Name = "ucLabEditSampleQty";
            this.ucLabEditSampleQty.PasswordChar = '\0';
            this.ucLabEditSampleQty.ReadOnly = false;
            this.ucLabEditSampleQty.ShowCheckBox = false;
            this.ucLabEditSampleQty.Size = new System.Drawing.Size(218, 24);
            this.ucLabEditSampleQty.TabIndex = 158;
            this.ucLabEditSampleQty.TabNext = true;
            this.ucLabEditSampleQty.Value = "";
            this.ucLabEditSampleQty.Visible = false;
            this.ucLabEditSampleQty.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabEditSampleQty.XAlign = 398;
            // 
            // txtCartonCode
            // 
            this.txtCartonCode.AllowEditOnlyChecked = true;
            this.txtCartonCode.AutoSelectAll = false;
            this.txtCartonCode.AutoUpper = true;
            this.txtCartonCode.Caption = "箱号";
            this.txtCartonCode.Checked = false;
            this.txtCartonCode.EditType = UserControl.EditTypes.String;
            this.txtCartonCode.Location = new System.Drawing.Point(67, 44);
            this.txtCartonCode.MaxLength = 40;
            this.txtCartonCode.Multiline = false;
            this.txtCartonCode.Name = "txtCartonCode";
            this.txtCartonCode.PasswordChar = '\0';
            this.txtCartonCode.ReadOnly = false;
            this.txtCartonCode.ShowCheckBox = false;
            this.txtCartonCode.Size = new System.Drawing.Size(437, 25);
            this.txtCartonCode.TabIndex = 192;
            this.txtCartonCode.TabNext = true;
            this.txtCartonCode.Value = "";
            this.txtCartonCode.WidthType = UserControl.WidthTypes.TooLong;
            this.txtCartonCode.XAlign = 104;
            this.txtCartonCode.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCartonCode_TxtboxKeyPress);
            // 
            // templete
            // 
            this.templete.Controls.Add(this.ucLabEditAC3);
            this.templete.Controls.Add(this.ultraGridOQCCheckType);
            this.templete.Controls.Add(this.ucLabEditAC4);
            this.templete.Controls.Add(this.ucLabEditRE1);
            this.templete.Controls.Add(this.ucLabEditAQL4);
            this.templete.Controls.Add(this.ucLabEditAQL1);
            this.templete.Controls.Add(this.ucLabEditRE4);
            this.templete.Controls.Add(this.ucLabEditAC1);
            this.templete.Controls.Add(this.labelZ);
            this.templete.Controls.Add(this.ucLabEditRE2);
            this.templete.Controls.Add(this.btnSaveConfig);
            this.templete.Controls.Add(this.btnLoadConfig);
            this.templete.Controls.Add(this.ucLabEditAQL2);
            this.templete.Controls.Add(this.ucLabEditAC2);
            this.templete.Controls.Add(this.ucLabEditRE3);
            this.templete.Controls.Add(this.labelA);
            this.templete.Controls.Add(this.labelB);
            this.templete.Controls.Add(this.labelC);
            this.templete.Controls.Add(this.ucLabEditAQL3);
            this.templete.Location = new System.Drawing.Point(31, 129);
            this.templete.Name = "templete";
            this.templete.Size = new System.Drawing.Size(634, 316);
            this.templete.TabIndex = 193;
            this.templete.TabStop = false;
            this.templete.Text = "模板";
            // 
            // ucLabEditAC3
            // 
            this.ucLabEditAC3.AllowEditOnlyChecked = true;
            this.ucLabEditAC3.AutoSelectAll = false;
            this.ucLabEditAC3.AutoUpper = true;
            this.ucLabEditAC3.Caption = "AC";
            this.ucLabEditAC3.Checked = false;
            this.ucLabEditAC3.EditType = UserControl.EditTypes.Integer;
            this.ucLabEditAC3.Location = new System.Drawing.Point(466, 54);
            this.ucLabEditAC3.MaxLength = 8;
            this.ucLabEditAC3.Multiline = false;
            this.ucLabEditAC3.Name = "ucLabEditAC3";
            this.ucLabEditAC3.PasswordChar = '\0';
            this.ucLabEditAC3.ReadOnly = false;
            this.ucLabEditAC3.ShowCheckBox = false;
            this.ucLabEditAC3.Size = new System.Drawing.Size(125, 24);
            this.ucLabEditAC3.TabIndex = 195;
            this.ucLabEditAC3.TabNext = true;
            this.ucLabEditAC3.Value = "";
            this.ucLabEditAC3.WidthType = UserControl.WidthTypes.Small;
            this.ucLabEditAC3.XAlign = 491;
            // 
            // ultraGridOQCCheckType
            // 
            this.ultraGridOQCCheckType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.ultraGridOQCCheckType.Cursor = System.Windows.Forms.Cursors.Default;
            this.ultraGridOQCCheckType.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ultraGridOQCCheckType.Location = new System.Drawing.Point(50, 117);
            this.ultraGridOQCCheckType.Name = "ultraGridOQCCheckType";
            this.ultraGridOQCCheckType.Size = new System.Drawing.Size(409, 172);
            this.ultraGridOQCCheckType.TabIndex = 197;
            this.ultraGridOQCCheckType.Text = "检验类型";
            this.ultraGridOQCCheckType.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.ultraGridMain_InitializeLayout);
            // 
            // ucLabEditAC4
            // 
            this.ucLabEditAC4.AllowEditOnlyChecked = true;
            this.ucLabEditAC4.AutoSelectAll = false;
            this.ucLabEditAC4.AutoUpper = true;
            this.ucLabEditAC4.Caption = "AC";
            this.ucLabEditAC4.Checked = false;
            this.ucLabEditAC4.EditType = UserControl.EditTypes.Integer;
            this.ucLabEditAC4.Location = new System.Drawing.Point(50, 54);
            this.ucLabEditAC4.MaxLength = 8;
            this.ucLabEditAC4.Multiline = false;
            this.ucLabEditAC4.Name = "ucLabEditAC4";
            this.ucLabEditAC4.PasswordChar = '\0';
            this.ucLabEditAC4.ReadOnly = false;
            this.ucLabEditAC4.ShowCheckBox = false;
            this.ucLabEditAC4.Size = new System.Drawing.Size(125, 24);
            this.ucLabEditAC4.TabIndex = 186;
            this.ucLabEditAC4.TabNext = true;
            this.ucLabEditAC4.Value = "";
            this.ucLabEditAC4.WidthType = UserControl.WidthTypes.Small;
            this.ucLabEditAC4.XAlign = 75;
            // 
            // ucLabEditRE1
            // 
            this.ucLabEditRE1.AllowEditOnlyChecked = true;
            this.ucLabEditRE1.AutoSelectAll = false;
            this.ucLabEditRE1.AutoUpper = true;
            this.ucLabEditRE1.Caption = "RE";
            this.ucLabEditRE1.Checked = false;
            this.ucLabEditRE1.EditType = UserControl.EditTypes.Integer;
            this.ucLabEditRE1.Location = new System.Drawing.Point(189, 82);
            this.ucLabEditRE1.MaxLength = 8;
            this.ucLabEditRE1.Multiline = false;
            this.ucLabEditRE1.Name = "ucLabEditRE1";
            this.ucLabEditRE1.PasswordChar = '\0';
            this.ucLabEditRE1.ReadOnly = false;
            this.ucLabEditRE1.ShowCheckBox = false;
            this.ucLabEditRE1.Size = new System.Drawing.Size(125, 24);
            this.ucLabEditRE1.TabIndex = 190;
            this.ucLabEditRE1.TabNext = true;
            this.ucLabEditRE1.Value = "";
            this.ucLabEditRE1.WidthType = UserControl.WidthTypes.Small;
            this.ucLabEditRE1.XAlign = 214;
            // 
            // ucLabEditAQL4
            // 
            this.ucLabEditAQL4.AllowEditOnlyChecked = true;
            this.ucLabEditAQL4.AutoSelectAll = false;
            this.ucLabEditAQL4.AutoUpper = true;
            this.ucLabEditAQL4.Caption = "AQL";
            this.ucLabEditAQL4.Checked = false;
            this.ucLabEditAQL4.EditType = UserControl.EditTypes.Number;
            this.ucLabEditAQL4.Location = new System.Drawing.Point(44, 26);
            this.ucLabEditAQL4.MaxLength = 14;
            this.ucLabEditAQL4.Multiline = false;
            this.ucLabEditAQL4.Name = "ucLabEditAQL4";
            this.ucLabEditAQL4.PasswordChar = '\0';
            this.ucLabEditAQL4.ReadOnly = false;
            this.ucLabEditAQL4.ShowCheckBox = false;
            this.ucLabEditAQL4.Size = new System.Drawing.Size(131, 24);
            this.ucLabEditAQL4.TabIndex = 185;
            this.ucLabEditAQL4.TabNext = true;
            this.ucLabEditAQL4.Value = "";
            this.ucLabEditAQL4.WidthType = UserControl.WidthTypes.Small;
            this.ucLabEditAQL4.XAlign = 75;
            // 
            // ucLabEditAQL1
            // 
            this.ucLabEditAQL1.AllowEditOnlyChecked = true;
            this.ucLabEditAQL1.AutoSelectAll = false;
            this.ucLabEditAQL1.AutoUpper = true;
            this.ucLabEditAQL1.Caption = "AQL";
            this.ucLabEditAQL1.Checked = false;
            this.ucLabEditAQL1.EditType = UserControl.EditTypes.Number;
            this.ucLabEditAQL1.Location = new System.Drawing.Point(183, 26);
            this.ucLabEditAQL1.MaxLength = 14;
            this.ucLabEditAQL1.Multiline = false;
            this.ucLabEditAQL1.Name = "ucLabEditAQL1";
            this.ucLabEditAQL1.PasswordChar = '\0';
            this.ucLabEditAQL1.ReadOnly = false;
            this.ucLabEditAQL1.ShowCheckBox = false;
            this.ucLabEditAQL1.Size = new System.Drawing.Size(131, 24);
            this.ucLabEditAQL1.TabIndex = 188;
            this.ucLabEditAQL1.TabNext = true;
            this.ucLabEditAQL1.Value = "";
            this.ucLabEditAQL1.WidthType = UserControl.WidthTypes.Small;
            this.ucLabEditAQL1.XAlign = 214;
            // 
            // ucLabEditRE4
            // 
            this.ucLabEditRE4.AllowEditOnlyChecked = true;
            this.ucLabEditRE4.AutoSelectAll = false;
            this.ucLabEditRE4.AutoUpper = true;
            this.ucLabEditRE4.Caption = "RE";
            this.ucLabEditRE4.Checked = false;
            this.ucLabEditRE4.EditType = UserControl.EditTypes.Integer;
            this.ucLabEditRE4.Location = new System.Drawing.Point(50, 82);
            this.ucLabEditRE4.MaxLength = 8;
            this.ucLabEditRE4.Multiline = false;
            this.ucLabEditRE4.Name = "ucLabEditRE4";
            this.ucLabEditRE4.PasswordChar = '\0';
            this.ucLabEditRE4.ReadOnly = false;
            this.ucLabEditRE4.ShowCheckBox = false;
            this.ucLabEditRE4.Size = new System.Drawing.Size(125, 24);
            this.ucLabEditRE4.TabIndex = 187;
            this.ucLabEditRE4.TabNext = true;
            this.ucLabEditRE4.Value = "";
            this.ucLabEditRE4.WidthType = UserControl.WidthTypes.Small;
            this.ucLabEditRE4.XAlign = 75;
            // 
            // ucLabEditAC1
            // 
            this.ucLabEditAC1.AllowEditOnlyChecked = true;
            this.ucLabEditAC1.AutoSelectAll = false;
            this.ucLabEditAC1.AutoUpper = true;
            this.ucLabEditAC1.Caption = "AC";
            this.ucLabEditAC1.Checked = false;
            this.ucLabEditAC1.EditType = UserControl.EditTypes.Integer;
            this.ucLabEditAC1.Location = new System.Drawing.Point(189, 54);
            this.ucLabEditAC1.MaxLength = 8;
            this.ucLabEditAC1.Multiline = false;
            this.ucLabEditAC1.Name = "ucLabEditAC1";
            this.ucLabEditAC1.PasswordChar = '\0';
            this.ucLabEditAC1.ReadOnly = false;
            this.ucLabEditAC1.ShowCheckBox = false;
            this.ucLabEditAC1.Size = new System.Drawing.Size(125, 24);
            this.ucLabEditAC1.TabIndex = 189;
            this.ucLabEditAC1.TabNext = true;
            this.ucLabEditAC1.Value = "";
            this.ucLabEditAC1.WidthType = UserControl.WidthTypes.Small;
            this.ucLabEditAC1.XAlign = 214;
            // 
            // labelZ
            // 
            this.labelZ.AutoSize = true;
            this.labelZ.Location = new System.Drawing.Point(76, 12);
            this.labelZ.Name = "labelZ";
            this.labelZ.Size = new System.Drawing.Size(11, 12);
            this.labelZ.TabIndex = 203;
            this.labelZ.Text = "Z";
            // 
            // ucLabEditRE2
            // 
            this.ucLabEditRE2.AllowEditOnlyChecked = true;
            this.ucLabEditRE2.AutoSelectAll = false;
            this.ucLabEditRE2.AutoUpper = true;
            this.ucLabEditRE2.Caption = "RE";
            this.ucLabEditRE2.Checked = false;
            this.ucLabEditRE2.EditType = UserControl.EditTypes.Integer;
            this.ucLabEditRE2.Location = new System.Drawing.Point(327, 82);
            this.ucLabEditRE2.MaxLength = 8;
            this.ucLabEditRE2.Multiline = false;
            this.ucLabEditRE2.Name = "ucLabEditRE2";
            this.ucLabEditRE2.PasswordChar = '\0';
            this.ucLabEditRE2.ReadOnly = false;
            this.ucLabEditRE2.ShowCheckBox = false;
            this.ucLabEditRE2.Size = new System.Drawing.Size(125, 24);
            this.ucLabEditRE2.TabIndex = 193;
            this.ucLabEditRE2.TabNext = true;
            this.ucLabEditRE2.Value = "";
            this.ucLabEditRE2.WidthType = UserControl.WidthTypes.Small;
            this.ucLabEditRE2.XAlign = 352;
            // 
            // btnSaveConfig
            // 
            this.btnSaveConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSaveConfig.BackColor = System.Drawing.SystemColors.Control;
            this.btnSaveConfig.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSaveConfig.BackgroundImage")));
            this.btnSaveConfig.ButtonType = UserControl.ButtonTypes.None;
            this.btnSaveConfig.Caption = "保存配置值";
            this.btnSaveConfig.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSaveConfig.Location = new System.Drawing.Point(52, 292);
            this.btnSaveConfig.Name = "btnSaveConfig";
            this.btnSaveConfig.Size = new System.Drawing.Size(88, 22);
            this.btnSaveConfig.TabIndex = 198;
            this.btnSaveConfig.Click += new System.EventHandler(this.btnSaveConfig_Click);
            // 
            // btnLoadConfig
            // 
            this.btnLoadConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLoadConfig.BackColor = System.Drawing.SystemColors.Control;
            this.btnLoadConfig.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnLoadConfig.BackgroundImage")));
            this.btnLoadConfig.ButtonType = UserControl.ButtonTypes.None;
            this.btnLoadConfig.Caption = "读取配置值";
            this.btnLoadConfig.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLoadConfig.Location = new System.Drawing.Point(146, 292);
            this.btnLoadConfig.Name = "btnLoadConfig";
            this.btnLoadConfig.Size = new System.Drawing.Size(88, 22);
            this.btnLoadConfig.TabIndex = 199;
            this.btnLoadConfig.Click += new System.EventHandler(this.btnLoadConfig_Click);
            // 
            // ucLabEditAQL2
            // 
            this.ucLabEditAQL2.AllowEditOnlyChecked = true;
            this.ucLabEditAQL2.AutoSelectAll = false;
            this.ucLabEditAQL2.AutoUpper = true;
            this.ucLabEditAQL2.Caption = "AQL";
            this.ucLabEditAQL2.Checked = false;
            this.ucLabEditAQL2.EditType = UserControl.EditTypes.Number;
            this.ucLabEditAQL2.Location = new System.Drawing.Point(321, 26);
            this.ucLabEditAQL2.MaxLength = 14;
            this.ucLabEditAQL2.Multiline = false;
            this.ucLabEditAQL2.Name = "ucLabEditAQL2";
            this.ucLabEditAQL2.PasswordChar = '\0';
            this.ucLabEditAQL2.ReadOnly = false;
            this.ucLabEditAQL2.ShowCheckBox = false;
            this.ucLabEditAQL2.Size = new System.Drawing.Size(131, 24);
            this.ucLabEditAQL2.TabIndex = 191;
            this.ucLabEditAQL2.TabNext = true;
            this.ucLabEditAQL2.Value = "";
            this.ucLabEditAQL2.WidthType = UserControl.WidthTypes.Small;
            this.ucLabEditAQL2.XAlign = 352;
            // 
            // ucLabEditAC2
            // 
            this.ucLabEditAC2.AllowEditOnlyChecked = true;
            this.ucLabEditAC2.AutoSelectAll = false;
            this.ucLabEditAC2.AutoUpper = true;
            this.ucLabEditAC2.Caption = "AC";
            this.ucLabEditAC2.Checked = false;
            this.ucLabEditAC2.EditType = UserControl.EditTypes.Integer;
            this.ucLabEditAC2.Location = new System.Drawing.Point(327, 54);
            this.ucLabEditAC2.MaxLength = 8;
            this.ucLabEditAC2.Multiline = false;
            this.ucLabEditAC2.Name = "ucLabEditAC2";
            this.ucLabEditAC2.PasswordChar = '\0';
            this.ucLabEditAC2.ReadOnly = false;
            this.ucLabEditAC2.ShowCheckBox = false;
            this.ucLabEditAC2.Size = new System.Drawing.Size(125, 24);
            this.ucLabEditAC2.TabIndex = 192;
            this.ucLabEditAC2.TabNext = true;
            this.ucLabEditAC2.Value = "";
            this.ucLabEditAC2.WidthType = UserControl.WidthTypes.Small;
            this.ucLabEditAC2.XAlign = 352;
            // 
            // ucLabEditRE3
            // 
            this.ucLabEditRE3.AllowEditOnlyChecked = true;
            this.ucLabEditRE3.AutoSelectAll = false;
            this.ucLabEditRE3.AutoUpper = true;
            this.ucLabEditRE3.Caption = "RE";
            this.ucLabEditRE3.Checked = false;
            this.ucLabEditRE3.EditType = UserControl.EditTypes.Integer;
            this.ucLabEditRE3.Location = new System.Drawing.Point(466, 82);
            this.ucLabEditRE3.MaxLength = 8;
            this.ucLabEditRE3.Multiline = false;
            this.ucLabEditRE3.Name = "ucLabEditRE3";
            this.ucLabEditRE3.PasswordChar = '\0';
            this.ucLabEditRE3.ReadOnly = false;
            this.ucLabEditRE3.ShowCheckBox = false;
            this.ucLabEditRE3.Size = new System.Drawing.Size(125, 24);
            this.ucLabEditRE3.TabIndex = 196;
            this.ucLabEditRE3.TabNext = true;
            this.ucLabEditRE3.Value = "";
            this.ucLabEditRE3.WidthType = UserControl.WidthTypes.Small;
            this.ucLabEditRE3.XAlign = 491;
            // 
            // labelA
            // 
            this.labelA.AutoSize = true;
            this.labelA.Location = new System.Drawing.Point(217, 11);
            this.labelA.Name = "labelA";
            this.labelA.Size = new System.Drawing.Size(11, 12);
            this.labelA.TabIndex = 200;
            this.labelA.Text = "A";
            // 
            // labelB
            // 
            this.labelB.AutoSize = true;
            this.labelB.Location = new System.Drawing.Point(354, 11);
            this.labelB.Name = "labelB";
            this.labelB.Size = new System.Drawing.Size(11, 12);
            this.labelB.TabIndex = 201;
            this.labelB.Text = "B";
            // 
            // labelC
            // 
            this.labelC.AutoSize = true;
            this.labelC.Location = new System.Drawing.Point(491, 11);
            this.labelC.Name = "labelC";
            this.labelC.Size = new System.Drawing.Size(11, 12);
            this.labelC.TabIndex = 202;
            this.labelC.Text = "C";
            // 
            // ucLabEditAQL3
            // 
            this.ucLabEditAQL3.AllowEditOnlyChecked = true;
            this.ucLabEditAQL3.AutoSelectAll = false;
            this.ucLabEditAQL3.AutoUpper = true;
            this.ucLabEditAQL3.Caption = "AQL";
            this.ucLabEditAQL3.Checked = false;
            this.ucLabEditAQL3.EditType = UserControl.EditTypes.Number;
            this.ucLabEditAQL3.Location = new System.Drawing.Point(460, 26);
            this.ucLabEditAQL3.MaxLength = 14;
            this.ucLabEditAQL3.Multiline = false;
            this.ucLabEditAQL3.Name = "ucLabEditAQL3";
            this.ucLabEditAQL3.PasswordChar = '\0';
            this.ucLabEditAQL3.ReadOnly = false;
            this.ucLabEditAQL3.ShowCheckBox = false;
            this.ucLabEditAQL3.Size = new System.Drawing.Size(131, 24);
            this.ucLabEditAQL3.TabIndex = 194;
            this.ucLabEditAQL3.TabNext = true;
            this.ucLabEditAQL3.Value = "";
            this.ucLabEditAQL3.WidthType = UserControl.WidthTypes.Small;
            this.ucLabEditAQL3.XAlign = 491;
            // 
            // FOQCSamplePlan
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(734, 683);
            this.Controls.Add(this.templete);
            this.Controls.Add(this.txtCartonCode);
            this.Controls.Add(groupBox5);
            this.Controls.Add(this.ucLabelEditSizeAndCapacity);
            this.Controls.Add(this.lotType);
            this.Controls.Add(this.cbbLotNO);
            this.Controls.Add(this.productInfo);
            this.Controls.Add(this.btnGetLot);
            this.Controls.Add(this.txtRcard);
            this.Controls.Add(this.txtMemo);
            this.Controls.Add(this.lblLotNo);
            this.Controls.Add(this.ucLabEditLotQty);
            this.Controls.Add(this.ucLabEditSampleQty);
            this.Controls.Add(this.groupBox4);
            this.Name = "FOQCSamplePlan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "OQC抽样计划";
            this.Load += new System.EventHandler(this.FOQCSamplePlan_Load);
            this.Closed += new System.EventHandler(this.FOQCSamplePlan_Closed);
            this.Activated += new System.EventHandler(this.FOQCSamplePlan_Activated);
            groupBox5.ResumeLayout(false);
            groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.productInfo.ResumeLayout(false);
            this.lotType.ResumeLayout(false);
            this.templete.ResumeLayout(false);
            this.templete.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridOQCCheckType)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        #region Init Form
        private void InitForm()
        {
            if (oqcLotNo.Trim().Length == 0)
            {
                SetEditObject(null);
            }
            else
            {
                this.btnLoadConfig_Click(this.btnLoadConfig, null);

                OQCFacade _oqcFacade = new OQCFacade(this.DataProvider);
                object obj = _oqcFacade.GetOQCLot(oqcLotNo, OQCHelper.Lot_Sequence_Default);
                SetEditObject(obj);
                //this.ucLabEditLotNO.ReadOnly = true;
                this.cbbLotNO.Enabled = false;
                this.ucLabEditSampleQty.ReadOnly = true;

                this.ucLabEditAQL1.ReadOnly = true;
                this.ucLabEditAC1.ReadOnly = true;
                this.ucLabEditRE1.ReadOnly = true;
                this.ucLabEditAQL2.ReadOnly = true;
                this.ucLabEditAC2.ReadOnly = true;
                this.ucLabEditRE2.ReadOnly = true;
                this.ucLabEditAQL3.ReadOnly = true;
                this.ucLabEditAQL4.ReadOnly = true;
                this.ucLabEditAC4.ReadOnly = true;
                this.ucLabEditAC3.ReadOnly = true;
                this.ucLabEditRE3.ReadOnly = true;
                this.ucLabEditRE4.ReadOnly = true;

                this.ucButtonOK.Enabled = false;

                this.ultraGridOQCCheckType.Enabled = false;
            }
        }
        #endregion

        #region object <-> UI
        private void SetEditObject(object obj)
        {           
            if (obj == null)
            {
                //this.ucLabEditLotNO.Value = string.Empty;
                this.cbbLotNO.Text = "";
                this.ucLabEditSampleQty.Value = string.Empty;
                this.ucLabEditLotQty.Value = string.Empty;
                this.ucLabEditAQL1.Value = string.Empty;
                this.ucLabEditAC1.Value = string.Empty;
                this.ucLabEditRE1.Value = string.Empty;
                this.ucLabEditAQL2.Value = string.Empty;
                this.ucLabEditAC2.Value = string.Empty;
                this.ucLabEditRE2.Value = string.Empty;
                this.ucLabEditAQL3.Value = string.Empty;
                this.ucLabEditAC3.Value = string.Empty;
                this.ucLabEditRE3.Value = string.Empty;
                this.ucLabEditAQL4.Value = string.Empty;
                this.ucLabEditAC4.Value = string.Empty;
                this.ucLabEditRE4.Value = string.Empty;
                this.txtMemo.Value = string.Empty;
                this.ucLabelEditSizeAndCapacity.Value = string.Empty;
            }
            else
            {
                OQCLot oqcLot = obj as OQCLot;              
               
                //this.ucLabEditLotNO.Value = oqcLot.LOTNO;
                this.cbbLotNO.Text = oqcLot.LOTNO;
                this.ucLabEditLotQty.Value = oqcLot.LotSize.ToString();
                this.ucLabEditSampleQty.Value = oqcLot.LotCapacity.ToString();
                this.ucLabelEditSizeAndCapacity.Value = oqcLot.LotSize.ToString();// + "/" + oqcLot.LotCapacity.ToString();
                //add by alex 2010.11.8
                if (oqcLot.AQL.Equals(0) && oqcLot.AcceptSize.Equals(0) && oqcLot.RejectSize.Equals(0) && 
                    oqcLot.AQL1.Equals(0) && oqcLot.AcceptSize1.Equals(0) && oqcLot.RejectSize1.Equals(0) && 
                    oqcLot.AQL2.Equals(0) && oqcLot.AcceptSize2.Equals(0) && oqcLot.RejectSize2.Equals(0) && 
                    oqcLot.AQL3.Equals(0) && oqcLot.AcceptSize3.Equals(0) && oqcLot.RejectSize3.Equals(0))
                {
                    this.btnLoadConfig_Click(this.btnLoadConfig, null);
                }
                else
                {
                    if (oqcLot.AQL.ToString() != String.Empty)
                    {
                        string strAQL = this.FormatString(oqcLot.AQL.ToString());
                        this.ucLabEditAQL1.Value = strAQL;
                    }
                    if (oqcLot.AcceptSize.ToString() != String.Empty)
                    {
                        this.ucLabEditAC1.Value = oqcLot.AcceptSize.ToString();
                    }
                    if (oqcLot.RejectSize.ToString() != String.Empty)
                    {
                        this.ucLabEditRE1.Value = oqcLot.RejectSize.ToString();
                    }
                    if (oqcLot.AQL1.ToString() != String.Empty)
                    {
                        string strAQL = this.FormatString(oqcLot.AQL1.ToString());
                        this.ucLabEditAQL2.Value = strAQL;
                    }
                    if (oqcLot.AcceptSize1.ToString() != String.Empty)
                    {
                        this.ucLabEditAC2.Value = oqcLot.AcceptSize1.ToString();
                    }
                    if (oqcLot.RejectSize1.ToString() != String.Empty)
                    {
                        this.ucLabEditRE2.Value = oqcLot.RejectSize1.ToString();
                    }
                    if (oqcLot.AQL2.ToString() != String.Empty)
                    {
                        string strAQL = this.FormatString(oqcLot.AQL2.ToString());
                        this.ucLabEditAQL3.Value = strAQL;
                    }
                    if (oqcLot.AQL3.ToString() != string.Empty)
                    {
                        string strAQL = this.FormatString(oqcLot.AQL3.ToString());
                        this.ucLabEditAQL4.Value = strAQL;
                    }
                    if (oqcLot.AcceptSize2.ToString() != String.Empty)
                    {
                        this.ucLabEditAC3.Value = oqcLot.AcceptSize2.ToString();
                    }
                    if (oqcLot.AcceptSize3.ToString() != String.Empty)
                    {
                        this.ucLabEditAC4.Value = oqcLot.AcceptSize3.ToString();
                    }
                    if (oqcLot.RejectSize2.ToString() != String.Empty)
                    {
                        this.ucLabEditRE3.Value = oqcLot.RejectSize2.ToString();
                    }
                    if (oqcLot.RejectSize3.ToString() != String.Empty)
                    {
                        this.ucLabEditRE4.Value = oqcLot.RejectSize3.ToString();
                    }
                }
                //清除Grid中旧批次检测类型信息
                InitCheckGroup();                
                DataRow[] dataRow = dtCheckItem.Select("Checked = true");
                if (dataRow.Length.Equals(0))
                {
                     object[] objOQCParaAll = this.LoadOQCPara();
                     if (objOQCParaAll != null)
                     {
                         Infragistics.Win.UltraWinGrid.UltraGridRow row;
                         for (int i = 0; i < this.ultraGridOQCCheckType.Rows.Count; i++)
                         {
                             row = this.ultraGridOQCCheckType.Rows[i];
                             foreach (object OQCPara in objOQCParaAll)
                             {
                                 if (((OQCPara)OQCPara).NodeName == Convert.ToString(row.Cells["CheckGroup"].Text))
                                 {
                                     row.Cells["Checked"].Value = true;
                                     row.Cells["SampleQty"].Value = ((OQCPara)OQCPara).NodeValue;
                                     break;
                                 }
                                 else
                                 {
                                     row.Cells["Checked"].Value = false;
                                     row.Cells["SampleQty"].Value = 0;
                                 }
                             }
                         }
                     }                     
                     this.ultraGridOQCCheckType.UpdateData();
                }
                OQCFacade oqcFacade = new OQCFacade(this.DataProvider);
                object[] objList = oqcFacade.GetOQCLot2CheckGroupOfLot(oqcLot.LOTNO, oqcLot.LotSequence);
                if (objList != null)
                {                    
                    Infragistics.Win.UltraWinGrid.UltraGridRow row;

                    for (int i = 0; i < this.ultraGridOQCCheckType.Rows.Count; i++)
                    {
                        row = this.ultraGridOQCCheckType.Rows[i];
                        foreach (OQCLot2CheckGroup oqcLot2CheckGroup in objList)
                        {
                            if (oqcLot2CheckGroup.CheckGroup == Convert.ToString(row.Cells["CheckGroup"].Text))
                            {
                                row.Cells["Checked"].Value = true;
                                row.Cells["SampleQty"].Value = oqcLot2CheckGroup.NeedCheckCount;
                                break;
                            }
                            else
                            {
                                row.Cells["Checked"].Value = false;
                                row.Cells["SampleQty"].Value = 0;
                            }
                        }
                    }
                    this.ultraGridOQCCheckType.UpdateData();
                }

                if (oqcLot.EAttribute1 != String.Empty)
                {
                    txtMemo.Checked = true;
                    txtMemo.Value = oqcLot.EAttribute1;
                }
                else
                {
                    txtMemo.Checked = false;
                    this.txtMemo.Value = oqcLot.EAttribute1;
                }
                /*
                this.rbMass.Checked = (oqcLot.ProductionType == BenQGuru.eMES.Web.Helper.ProductionType.ProductionType_Mass);
                this.rbTry.Checked =  (oqcLot.ProductionType == BenQGuru.eMES.Web.Helper.ProductionType.ProductionType_Try);
                this.rbNew.Checked = (oqcLot.ProductionType == BenQGuru.eMES.Web.Helper.ProductionType.ProductionType_New);

                this.rbNormal.Checked = (oqcLot.OQCLotType == BenQGuru.eMES.Web.Helper.OQCLotType.OQCLotType_Normal);
                this.rbRelapse.Checked = (oqcLot.OQCLotType == BenQGuru.eMES.Web.Helper.OQCLotType.OQCLotType_ReDO);
                */

                this.labelProductionType.Text = UserControl.MutiLanguages.ParserString(oqcLot.ProductionType);
                this.labelLotType.Text = UserControl.MutiLanguages.ParserString(oqcLot.OQCLotType);

                this.cbbLotNO.Focus();
            }
        }

        private OQCLot GetEditObject()
        {
            if (this.cbbLotNO.Text.Trim() == string.Empty) //(this.ucLabEditLotNO.Value.Trim() == string.Empty)
            {
                return null;
            }
            OQCFacade _oqcFacade = new OQCFacade(this.DataProvider);
            //object obj = _oqcFacade.GetOQCLot(FormatHelper.CleanString(this.ucLabEditLotNO.Value),OQCHelper.Lot_Sequence_Default);
            object obj = _oqcFacade.GetOQCLot(FormatHelper.CleanString(this.cbbLotNO.Text), OQCHelper.Lot_Sequence_Default);
            if (obj == null)
            {
                return null;
            }
            OQCLot oqcLot = obj as OQCLot;
            oqcLot.AcceptSize = System.Int32.Parse(FormatHelper.CleanString(this.ucLabEditAC1.Value.Trim()));
            oqcLot.AcceptSize1 = System.Int32.Parse(FormatHelper.CleanString(this.ucLabEditAC2.Value.Trim()));
            oqcLot.AcceptSize2 = System.Int32.Parse(FormatHelper.CleanString(this.ucLabEditAC3.Value.Trim()));
            oqcLot.AcceptSize3 = System.Int32.Parse(FormatHelper.CleanString(this.ucLabEditAC4.Value.Trim()));
            oqcLot.AQL = System.Decimal.Parse(FormatHelper.CleanString(this.ucLabEditAQL1.Value.Trim()));
            oqcLot.AQL1 = System.Decimal.Parse(FormatHelper.CleanString(this.ucLabEditAQL2.Value.Trim()));
            oqcLot.AQL2 = System.Decimal.Parse(FormatHelper.CleanString(this.ucLabEditAQL3.Value.Trim()));
            oqcLot.AQL3 = System.Decimal.Parse(FormatHelper.CleanString(this.ucLabEditAQL4.Value.Trim()));
            //			oqcLot.LOTStatus = OQCLotStatus.OQCLotStatus_NoExame;
            oqcLot.MaintainUser = ApplicationService.Current().UserCode;
            oqcLot.RejectSize = System.Int32.Parse(FormatHelper.CleanString(this.ucLabEditRE1.Value.Trim()));
            oqcLot.RejectSize1 = System.Int32.Parse(FormatHelper.CleanString(this.ucLabEditRE2.Value.Trim()));
            oqcLot.RejectSize2 = System.Int32.Parse(FormatHelper.CleanString(this.ucLabEditRE3.Value.Trim()));
            oqcLot.RejectSize3 = System.Int32.Parse(FormatHelper.CleanString(this.ucLabEditRE4.Value.Trim()));
            // Edited By Hi1/Venus.Feng on 20080718 for Hisense Version
            oqcLot.SampleSize = 0;
            //oqcLot.SampleSize = System.Int32.Parse(FormatHelper.CleanString(this.ucLabEditSampleQty.Value.Trim()));
            // End edited
            /*
            if(this.rbMass.Checked)
                oqcLot.ProductionType = BenQGuru.eMES.Web.Helper.ProductionType.ProductionType_Mass;
            if(this.rbTry.Checked)
                oqcLot.ProductionType = BenQGuru.eMES.Web.Helper.ProductionType.ProductionType_Try;
            if(this.rbNew.Checked)
                oqcLot.ProductionType = BenQGuru.eMES.Web.Helper.ProductionType.ProductionType_New;
            if(this.rbNormal.Checked)
                oqcLot.OQCLotType =BenQGuru.eMES.Web.Helper.OQCLotType.OQCLotType_Normal;
            if(this.rbRelapse.Checked)
                oqcLot.OQCLotType = BenQGuru.eMES.Web.Helper.OQCLotType.OQCLotType_ReDO;
            */
            if (txtMemo.Checked = true && txtMemo.Value.Trim() != String.Empty)
            {
                oqcLot.EAttribute1 = txtMemo.Value.ToUpper().Trim();
            }

            return oqcLot;
        }


        private OQCLot2CheckGroup[] GetOQCLot2CheckGroupObject()
        {
            int objNUm = 0;
            int OQCNum = 0;
            foreach (DataRow row in this.dtCheckItem.Rows)
            {
                if (Convert.ToBoolean(row["Checked"]) == true)
                {
                    objNUm += 1;
                }
            }
            OQCLot2CheckGroup[] getOQCLot2CheckGroup = new OQCLot2CheckGroup[objNUm];
            foreach (DataRow row in this.dtCheckItem.Rows)
            {
                if (Convert.ToBoolean(row["Checked"]) == true)
                {
                    OQCLot2CheckGroup newone = new OQCLot2CheckGroup();
                    getOQCLot2CheckGroup[OQCNum] = newone;
                    getOQCLot2CheckGroup[OQCNum].LOTNO = this.cbbLotNO.Text.ToString();
                    getOQCLot2CheckGroup[OQCNum].LotSequence = OQCHelper.Lot_Sequence_Default;
                    getOQCLot2CheckGroup[OQCNum].CheckedCount = 0;
                    getOQCLot2CheckGroup[OQCNum].CheckGroup = Convert.ToString(row["CheckGroup"]);
                    getOQCLot2CheckGroup[OQCNum].NeedCheckCount = Convert.ToInt32(row["SampleQty"]);
                    getOQCLot2CheckGroup[OQCNum].MaintainUser = ApplicationService.Current().UserCode;
                    OQCNum++;
                }
            }
            return getOQCLot2CheckGroup;
        }

        //Laws Lu,2005/08/22,修改	界面校验调整
        private Messages ValidateInput()
        {
            Messages messages = new Messages();
            //			if(ucLabEditLotNO.Value.Trim() == String.Empty)
            //			{
            //				messages.Add( new UserControl.Message(MessageType.Error, "抽检批号不存在"));
            //
            //				return messages;
            //			}

            // Marked By Hi1/Venus.Feng on 20080718 for Hisense Version
            /*
            try
			{
				System.Int32.Parse( FormatHelper.CleanString( this.ucLabEditSampleQty.Value.Trim()));

				if( System.Int32.Parse( FormatHelper.CleanString( this.ucLabEditSampleQty.Value.Trim())) <=0)
				{
					messages.Add( new UserControl.Message(MessageType.Error, "样本数必须大于0"));

					return messages;
				}
			}
			catch
			{
				messages.Add( new UserControl.Message(MessageType.Error, "样本数必须为整型"));

				return messages;
			}
            */
            // End Marked By Hi1/Venus.Feng on 20080718 for Hisense Version

            try
            {
                System.Decimal.Parse(FormatHelper.CleanString(this.ucLabEditAQL1.Value.Trim()));

                if (System.Decimal.Parse(FormatHelper.CleanString(this.ucLabEditAQL1.Value.Trim())) < 0)
                {
                    messages.Add(new UserControl.Message(MessageType.Error, "$CS_A_Grade_AQL_Can not_be_minus"));

                    return messages;
                }
            }
            catch
            {
                messages.Add(new UserControl.Message(MessageType.Error, "$CS_A_Grade_AQL_Must_be_numberType"));

                return messages;
            }

            try
            {
                System.Int32.Parse(FormatHelper.CleanString(this.ucLabEditAC1.Value.Trim()));

                if (System.Int32.Parse(FormatHelper.CleanString(this.ucLabEditAC1.Value.Trim())) < 0)
                {
                    messages.Add(new UserControl.Message(MessageType.Error, "$CS_A_Grade_AC_Can_not_be_minus"));

                    return messages;
                }
            }
            catch
            {
                messages.Add(new UserControl.Message(MessageType.Error, "$CS_A_Grade_AC_Must_be_IntType"));

                return messages;
            }

            try
            {
                System.Int32.Parse(FormatHelper.CleanString(this.ucLabEditRE1.Value.Trim()));

                if (System.Int32.Parse(FormatHelper.CleanString(this.ucLabEditRE1.Value.Trim())) < 0)
                {
                    messages.Add(new UserControl.Message(MessageType.Error, "$CS_A_Grade_RE_Can_not_be_minus"));

                    return messages;
                }
            }
            catch
            {
                messages.Add(new UserControl.Message(MessageType.Error, "$CS_A_Grade_RE_Must_be_IntType"));

                return messages;
            }

            try
            {
                System.Decimal.Parse(FormatHelper.CleanString(this.ucLabEditAQL2.Value.Trim()));

                if (System.Decimal.Parse(FormatHelper.CleanString(this.ucLabEditAQL2.Value.Trim())) < 0)
                {
                    messages.Add(new UserControl.Message(MessageType.Error, "$CS_B_Grade_AQL_Can_not_be_minus"));

                    return messages;
                }
            }
            catch
            {
                messages.Add(new UserControl.Message(MessageType.Error, "$CS_B_Grade_AQL_Must_be_NumberType"));

                return messages;
            }

            try
            {
                System.Int32.Parse(FormatHelper.CleanString(this.ucLabEditAC2.Value.Trim()));


                if (System.Int32.Parse(FormatHelper.CleanString(this.ucLabEditAC2.Value.Trim())) < 0)
                {
                    messages.Add(new UserControl.Message(MessageType.Error, "$CS_B_Grade_AC_Can_not_be_minus"));

                    return messages;
                }
            }
            catch
            {
                messages.Add(new UserControl.Message(MessageType.Error, "$CS_B_Grade_AC_Must_be_IntType"));

                return messages;
            }

            try
            {
                System.Int32.Parse(FormatHelper.CleanString(this.ucLabEditRE2.Value.Trim()));

                if (System.Int32.Parse(FormatHelper.CleanString(this.ucLabEditRE2.Value.Trim())) < 0)
                {
                    messages.Add(new UserControl.Message(MessageType.Error, "$CS_B_Grade_RE_Can_not_be_minus"));

                    return messages;
                }
            }
            catch
            {
                messages.Add(new UserControl.Message(MessageType.Error, "$CS_B_Grade_RE_Must_be_IntType"));

                return messages;
            }

            try
            {
                System.Decimal.Parse(FormatHelper.CleanString(this.ucLabEditAQL3.Value.Trim()));

                if (System.Decimal.Parse(FormatHelper.CleanString(this.ucLabEditAQL3.Value.Trim())) < 0)
                {
                    messages.Add(new UserControl.Message(MessageType.Error, "$CS_C_Grade_AQL_Can_not_be_minus"));

                    return messages;
                }
            }
            catch
            {
                messages.Add(new UserControl.Message(MessageType.Error, "$CS_C_Grade_AQL_Must_be_NumberType"));

                return messages;
            }

            try
            {
                System.Int32.Parse(FormatHelper.CleanString(this.ucLabEditAC3.Value.Trim()));

                if (System.Int32.Parse(FormatHelper.CleanString(this.ucLabEditAC3.Value.Trim())) < 0)
                {
                    messages.Add(new UserControl.Message(MessageType.Error, "$CS_C_Grade_AC_Can_not_be_minus"));

                    return messages;
                }
            }
            catch
            {
                messages.Add(new UserControl.Message(MessageType.Error, "$CS_C_Grade_AC_Must_be_IntType"));

                return messages;
            }

            try
            {
                System.Int32.Parse(FormatHelper.CleanString(this.ucLabEditRE3.Value.Trim()));

                if (System.Int32.Parse(FormatHelper.CleanString(this.ucLabEditRE3.Value.Trim())) < 0)
                {
                    messages.Add(new UserControl.Message(MessageType.Error, "$CS_C_Grade_RE_Can_not_be_minus"));

                    return messages;
                }
            }
            catch
            {
                messages.Add(new UserControl.Message(MessageType.Error, "$CS_C_Grade_RE_Must_be_IntType"));

                return messages;
            }

            try
            {
                System.Decimal.Parse(FormatHelper.CleanString(this.ucLabEditAQL4.Value.Trim()));

                if (System.Decimal.Parse(FormatHelper.CleanString(this.ucLabEditAQL4.Value.Trim())) < 0)
                {
                    messages.Add(new UserControl.Message(MessageType.Error, "$CS_Z_Grade_AQL_Can_not_be_minus"));

                    return messages;
                }
            }
            catch
            {
                messages.Add(new UserControl.Message(MessageType.Error, "$CS_Z_Grade_AQL_Must_be_NumberType"));

                return messages;
            }

            try
            {
                System.Int32.Parse(FormatHelper.CleanString(this.ucLabEditAC4.Value.Trim()));

                if (System.Int32.Parse(FormatHelper.CleanString(this.ucLabEditAC4.Value.Trim())) < 0)
                {
                    messages.Add(new UserControl.Message(MessageType.Error, "$CS_Z_Grade_AC_Can_not_be_minus"));

                    return messages;
                }
            }
            catch
            {
                messages.Add(new UserControl.Message(MessageType.Error, "$CS_Z_Grade_AC_Must_be_IntType"));

                return messages;
            }

            try
            {
                System.Int32.Parse(FormatHelper.CleanString(this.ucLabEditRE4.Value.Trim()));

                if (System.Int32.Parse(FormatHelper.CleanString(this.ucLabEditRE4.Value.Trim())) < 0)
                {
                    messages.Add(new UserControl.Message(MessageType.Error, "$CS_Z_Grade_RE_Can_not_be_minus"));

                    return messages;
                }
            }
            catch
            {
                messages.Add(new UserControl.Message(MessageType.Error, "$CS_Z_Grade_RE_Must_be_IntType"));

                return messages;
            }
            if (this.ucLabEditAQL1.Value == "0" &&
                this.ucLabEditAC1.Value == "0" &&
                this.ucLabEditRE1.Value == "0" &&
                this.ucLabEditAQL2.Value == "0" &&
                this.ucLabEditAC2.Value == "0" &&
                this.ucLabEditRE2.Value == "0" &&
                this.ucLabEditAQL3.Value == "0" &&
                this.ucLabEditAC3.Value == "0" &&
                this.ucLabEditRE3.Value == "0" &&
                this.ucLabEditAQL4.Value == "0" &&
                this.ucLabEditAC4.Value == "0" &&
                this.ucLabEditRE4.Value == "0")
            {
                messages.Add(new UserControl.Message(MessageType.Error, "$CS_Please_Maintain_Parameters"));

                return messages;
            }


            try
            {
                if (this.cbbLotNO.Text.Trim().Length == 0)
                {
                    messages.Add(new UserControl.Message(MessageType.Error, "$Please_Input_Lot_No"));
                    return messages;
                }
                OQCFacade oqcFacade = new OQCFacade(this.DataProvider);
                string lotno = FormatHelper.PKCapitalFormat(this.cbbLotNO.Text);

                object objLot = oqcFacade.GetOQCLot(FormatHelper.PKCapitalFormat(this.cbbLotNO.Text.ToString()), OQCFacade.Lot_Sequence_Default);

                
                object[] lot2cardList = oqcFacade.ExactQueryOQCLot2Card(lotno, OQCFacade.Lot_Sequence_Default);
                if (lot2cardList == null || lot2cardList.Length == 0)
                {
                    messages.Add(new UserControl.Message(MessageType.Error, "$CS_NO_Pallet_in_This_Lot"));

                    return messages;
                }
                string itemCode = (lot2cardList[0] as OQCLot2Card).ItemCode;
                ItemFacade itemFacade = new ItemFacade(this.DataProvider);
                object item = itemFacade.GetItem(itemCode, GlobalVariables.CurrentOrganizations.First().OrganizationID);
                Decimal lotSize = 0;

                if (objLot != null)
                {
                    lotSize =((OQCLot)objLot).LotSize;
                }


                this.ultraGridOQCCheckType.UpdateData();
                for (int i = 0; i < this.ultraGridOQCCheckType.Rows.Count; i++)
                {
                    Infragistics.Win.UltraWinGrid.UltraGridRow row = this.ultraGridOQCCheckType.Rows[i];
                    if (Convert.ToBoolean(row.Cells["Checked"].Value) == true)
                    {
                        if (Convert.ToString(row.Cells["SampleQty"].Text).Trim().Length == 0
                            || Convert.ToInt32(row.Cells["SampleQty"].Text) <= 0
                            || Convert.ToInt32(row.Cells["SampleQty"].Text) > lotSize)
                        {
                            messages.Add(new UserControl.Message(MessageType.Error, "$CS_SampleQty_Should_bigger_than_0 $CS_And_not_bigger_than_LotQty:" + lotSize.ToString()));

                            return messages;
                        }
                        if (Convert.ToString(row.Cells["SampleQty"].Text).Trim().Length >8                            
                            || Convert.ToInt32(row.Cells["SampleQty"].Text) > 999999999)
                        {
                            messages.Add(new UserControl.Message(MessageType.Error, "$CS_SampleQty_Beyond_MaxQty"));

                            return messages;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                messages.Add(new UserControl.Message(MessageType.Error, ex.Message));
                return messages;
            }

            return messages;
        }

        #endregion

        #region 事件的处理
        private void FOQCSamplePlan_Load(object sender, System.EventArgs e)
        {
            //UserControl.UIStyleBuilder.FormUI(this);
            InitForm();

            InitLotNo();

            // Added By Hi1/Venus.Feng on 20080717 for Hisense Version
            InitializeUltraGrid();
            this.InitCheckGroup();
            // End Added
            //this.InitPageLanguage();
        }



        public void ucButtonOK_Click(object sender, System.EventArgs e)
        {
            Messages messages = new Messages();
            messages.AddMessages(ValidateInput());
            if (!messages.IsSuccess())
            {
                ApplicationRun.GetInfoForm().Add(messages);
                this.cbbLotNO.Focus();
                return;
            }
            if (messages.IsSuccess())
            {
                OQCLot oqcLot = GetEditObject();
                OQCLot2CheckGroup[] GetOQCLot2CheckGroup = this.GetOQCLot2CheckGroupObject();

                if (oqcLot == null)
                {
                    return;
                }
                else
                {
                    OQCFacade _oqcFacade = new OQCFacade(DataProvider);
                    if ((oqcLot.LOTStatus == OQCLotStatus.OQCLotStatus_Initial)
                        || (oqcLot.LOTStatus == OQCLotStatus.OQCLotStatus_NoExame)
                        || (oqcLot.LOTStatus == OQCLotStatus.OQCLotStatus_Examing)
                        || (oqcLot.LOTStatus == OQCLotStatus.OQCLotStatus_SendExame))//Laws Lu,2006/07/12 support send for exam
                    {
                        if ((oqcLot.LOTStatus == OQCLotStatus.OQCLotStatus_Initial)
                            || (oqcLot.LOTStatus == OQCLotStatus.OQCLotStatus_NoExame)
                            || (oqcLot.LOTStatus == OQCLotStatus.OQCLotStatus_SendExame))
                        //Laws Lu,2006/07/12 support send for exam
                        {
                            oqcLot.LOTStatus = OQCLotStatus.OQCLotStatus_NoExame;
                        }

                        //if (rbMass.Checked)
                        //    oqcLot.ProductionType = BenQGuru.eMES.Web.Helper.ProductionType.ProductionType_Mass;
                        //else
                        //    oqcLot.ProductionType = BenQGuru.eMES.Web.Helper.ProductionType.ProductionType_Try;

                        int objNUm = 0;
                        foreach (DataRow row in this.dtCheckItem.Rows)
                        {
                            if (Convert.ToBoolean(row["Checked"]) == true)
                            {
                                objNUm += 1;
                            }
                        }
                        if (objNUm == 0)
                        {
                            ApplicationRun.GetInfoForm().Add(new UserControl.Message(UserControl.MessageType.Error, "$Error_NoCheckGroupChecked"));
                            return;
                        }

                        //Laws Lu,2005/10/19,新增	缓解性能问题
                        DataProvider.BeginTransaction();
                        try
                        {
                            //lock该lot，防止同时对该lot操作产生死锁
                            _oqcFacade.LockOQCLotByLotNO(oqcLot.LOTNO);
                            //end

                            //Laws Lu,2005/10/21,修改	更新样本计划，仅仅应该只更新与样本计划有关的信息                          
                            _oqcFacade.UpdateOQCLotSample(oqcLot);

                            this.OQCPara_save();

                            if (GetOQCLot2CheckGroup != null)
                            {
                                //当检验类型减少时，先删除旧的检验类型 add by alex hu
                                object[] objs = _oqcFacade.GetOQCLot2CheckGroupOfLot(FormatHelper.PKCapitalFormat(this.cbbLotNO.Text.ToString()), OQCFacade.Lot_Sequence_Default);
                                if (objs != null)
                                {
                                    if (objs.Length > GetOQCLot2CheckGroup.Length)
                                    {
                                        foreach (OQCLot2CheckGroup obj in objs)
                                        {
                                            _oqcFacade.DeleteOQCLot2CheckGroup(obj);
                                        }
                                    }
                                }

                                for(int i=0;i<GetOQCLot2CheckGroup.Length;i++)
                                {
                                    if (_oqcFacade.GetOQCLot2CheckGroup(GetOQCLot2CheckGroup[i].LOTNO, GetOQCLot2CheckGroup[i].LotSequence, GetOQCLot2CheckGroup[i].CheckGroup) == null)
                                    {
                                        _oqcFacade.AddOQCLot2CheckGroup(GetOQCLot2CheckGroup[i]);
                                    }
                                    else
                                    {
                                        object oldObj = _oqcFacade.GetOQCLot2CheckGroup(GetOQCLot2CheckGroup[i].LOTNO, GetOQCLot2CheckGroup[i].LotSequence, GetOQCLot2CheckGroup[i].CheckGroup);
                                        GetOQCLot2CheckGroup[i].CheckedCount=((OQCLot2CheckGroup)oldObj).CheckedCount;
                                        _oqcFacade.DeleteOQCLot2CheckGroup(GetOQCLot2CheckGroup[i]);
                                        _oqcFacade.AddOQCLot2CheckGroup(GetOQCLot2CheckGroup[i]);
                                    }
                                }
                            }

                            this.DataProvider.CommitTransaction();
                            messages.Add(new UserControl.Message(MessageType.Success, "$CS_OQC_Sample_Plan_Success"));
                        }
                        catch (Exception ex)
                        {
                            this.DataProvider.RollbackTransaction();
                            messages.Add(new UserControl.Message(ex));
                        }                       

                        //Added By Karron Qiu,
                        if (this.LotNOs.ContainsKey(oqcLot.LOTNO))
                        {
                            this.LotNOs[oqcLot.LOTNO] = oqcLot;
                        }
                        else
                        {
                            this.LotNOs.Add(oqcLot.LOTNO, oqcLot);
                        }
                        //end
                    }
                    else if (oqcLot.LOTStatus == OQCLotStatus.OQCLotStatus_Examing)
                    {
                        messages.Add(new UserControl.Message("$CS_LOT_ALREADY_PLANED"));
                    }
                    else if (oqcLot.LOTStatus == OQCLotStatus.OQCLotStatus_Pass || oqcLot.LOTStatus == OQCLotStatus.OQCLotStatus_Reject)
                    {
                        messages.Add(new UserControl.Message("$CS_FQC_LOT_USED"));
                    }
                }
            }
            ApplicationRun.GetInfoForm().Add(messages);
        }
        #endregion

        private void LabEditLotNOTxtboxKeyPress()
        {
            if (this.cbbLotNO.Text.Trim() == string.Empty) //(this.ucLabEditLotNO.Value.Trim() == string.Empty)
            {
                return;
            }
            Messages messages = new Messages();

            OQCFacade _oqcFacade = new OQCFacade(DataProvider);

            object objLot = _oqcFacade.GetOQCLot(FormatHelper.PKCapitalFormat(oqcLotNo), OQCFacade.Lot_Sequence_Default);
            if (objLot == null)
            {
                messages.Add(new UserControl.Message(MessageType.Error, "$CS_LOT_NOT_EXIST"));

                ApplicationRun.GetInfoForm().Add(messages);

                this.cbbLotNO.Focus();
                //ucLabEditLotNO.TextFocus(false, true);
                //SendKeys.Send("+{TAB}");

                return;
            }
            //				else if(((OQCLot)objLot).LOTStatus == OQCLotStatus.OQCLotStatus_Examing)
            //				{
            //					
            //					messages.Add(new UserControl.Message("$CS_LOT_ALREADY_PLANED"));
            //
            //					ApplicationRun.GetInfoForm().Add(messages);
            //
            //					ucLabEditLotNO.TextFocus(false, true);
            //					SendKeys.Send("+{TAB}");
            //
            //					return;
            //				}
            else if (((OQCLot)objLot).LOTStatus == OQCLotStatus.OQCLotStatus_Pass || ((OQCLot)objLot).LOTStatus == OQCLotStatus.OQCLotStatus_Reject)
            {
                messages.Add(new UserControl.Message("$CS_FQC_LOT_USED"));

                ApplicationRun.GetInfoForm().Add(messages);

                this.cbbLotNO.Focus();
                //ucLabEditLotNO.TextFocus(false, true);
                //SendKeys.Send("+{TAB}");

                return;
            }

            //				object obj = _oqcFacade.GetUnExameOQCLot(FormatHelper.CleanString(this.ucLabEditLotNO.Value),OQCHelper.Lot_Sequence_Default);
            //				if(obj == null)
            //				{
            //					messages.Add(new UserControl.Message("$CS_LOT_NOT_EXIST_OR_INVALIDE"));
            //
            //
            //					ApplicationRun.GetInfoForm().Add(messages);
            //
            //					ucLabEditLotNO.TextFocus(false, true);
            //					SendKeys.Send("+{TAB}");
            //				}
            SetEditObject(objLot);
        }

        public void ucLabEditLotNO_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                LabEditLotNOTxtboxKeyPress();
            }
        }

        private void FOQCSamplePlan_Closed(object sender, System.EventArgs e)
        {
            if (this.DataProvider != null)
                ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)this.DataProvider).PersistBroker.CloseConnection();
        }
        //Laws Lu,2005/08/12,新增设置焦点
        private void FOQCSamplePlan_Activated(object sender, System.EventArgs e)
        {
            //ucLabEditLotNO.TextFocus(false, true);
            this.cbbLotNO.Focus();
        }

        private void btnShowCollectOQC_Click(object sender, System.EventArgs e)
        {
            // Added By Karron Qiu,2006-6-19

            //if(ucLabEditLotNO.Value == "")
            //	return;

            if (this.cbbLotNO.Text.Trim() == string.Empty)
                return;

            if (!this.LotNOs.ContainsKey(this.cbbLotNO.Text.Trim()))
                return;

            OQCLot lot = (OQCLot)LotNOs[this.cbbLotNO.Text.Trim()];

            if (lot.LOTStatus != Web.Helper.OQCLotStatus.OQCLotStatus_Examing &&
                lot.LOTStatus != Web.Helper.OQCLotStatus.OQCLotStatus_NoExame)
            {
                return;
            }

            bool find = false;

            foreach (System.Windows.Forms.Form child in BenQGuru.eMES.Client.Service.ApplicationService.Current().MainWindows.MdiChildren)
            {
                if (child is FCollectionOQC)
                {
                    ((FCollectionOQC)child).OQCLotTextBox.Value = this.cbbLotNO.Text;//ucLabEditLotNO.Value;
                    child.BringToFront();

                    child.Show();
                    ((FCollectionOQC)child).LabOQCLotKeyPressForPlan();//LabOQCLotKeyPress();
                    find = true;
                    break;
                }
            }

            if (!find)
            {
                FCollectionOQC form = new FCollectionOQC();

                form.MdiParent = BenQGuru.eMES.Client.Service.ApplicationService.Current().MainWindows;
                form.OQCLotTextBox.Value = this.cbbLotNO.Text; // ucLabEditLotNO.Value;
                form.WindowState = FormWindowState.Maximized;

                form.Show();
                form.LabOQCLotKeyPressForPlan();//LabOQCLotKeyPress();
            }

            // end
        }

        private void InitLotNo()
        {
            OQCFacade _oqcFacade = new OQCFacade(DataProvider);
            object[] objs = _oqcFacade.GetExamingNoExameInitialOQCLot();

            cbbLotNO.Items.Clear();
            LotNOs.Clear();

            if (objs == null)
            {
                return;
            }

            //			cbbLotNO.DataSource = objs;
            //
            //			cbbLotNO.DisplayMember = "LOTNO";
            foreach (OQCLot lot in objs)
            {
                cbbLotNO.Items.Add(lot.LOTNO);
                LotNOs.Add(lot.LOTNO, lot);
            }
        }

        private Hashtable LotNOs = new Hashtable();

        public void cbbLotNO_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;

            this.LabEditLotNOTxtboxKeyPress();
        }

        private void cbbLotNO_SelectedValueChanged(object sender, System.EventArgs e)
        {
            this.LabEditLotNOTxtboxKeyPress();
        }

        #region Added By Karron Qiu,2006-6-26,保存到配置文件
        private void btnSaveConfig_Click(object sender, System.EventArgs e)
        {
            Messages msg = ValidateInputForConfiger();

            if (!msg.IsSuccess())
            {
                ApplicationRun.GetInfoForm().Add(msg);
                this.cbbLotNO.Focus();
                return;
            }
            int objNUm = 0;
            foreach (DataRow row in this.dtCheckItem.Rows)
            {
                if (Convert.ToBoolean(row["Checked"]) == true)
                {
                    objNUm += 1;
                }
            }
            if (objNUm == 0)
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(UserControl.MessageType.Error, "$Error_NoCheckGroupChecked"));
                return;
            }
            
            DataProvider.BeginTransaction();
            try
            {
                this.OQCPara_save();
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(UserControl.MessageType.Success, "$CS_OQCConfiguration_Save_Success"));

                DataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {
                DataProvider.RollbackTransaction();
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(ex));
            }
        }

        private void OQCPara_save()
        {
            OQCFacade oqcf = new OQCFacade(DataProvider);
            //modify by alex 2010.11.8
            //object[] objOqcPara = oqcf.GetOQCPara(ApplicationService.Current().ResourceCode);           
            OQCLot oqcLot = oqcf.GetOQCLot(FormatHelper.PKCapitalFormat(oqcLotNo), OQCFacade.Lot_Sequence_Default) as OQCLot;
            object[] objOqcPara = oqcf.GetOQCPara(oqcLot.ItemCode);
            //OQCConfiger configer = new OQCConfiger();
            string ANodeVaule = string.Empty;
            string BNodeVaule = string.Empty;
            string CNodeVaule = string.Empty;
            string ZNodeVaule = string.Empty;

            ANodeVaule = System.Decimal.Parse(FormatHelper.CleanString(this.ucLabEditAQL1.Value.Trim())) + ";" + System.Int32.Parse(FormatHelper.CleanString(this.ucLabEditAC1.Value.Trim())) + ";" + System.Int32.Parse(FormatHelper.CleanString(this.ucLabEditRE1.Value.Trim()));
            BNodeVaule = System.Decimal.Parse(FormatHelper.CleanString(this.ucLabEditAQL2.Value.Trim())) + ";" + System.Int32.Parse(FormatHelper.CleanString(this.ucLabEditAC2.Value.Trim())) + ";" + System.Int32.Parse(FormatHelper.CleanString(this.ucLabEditRE2.Value.Trim()));
            CNodeVaule = System.Decimal.Parse(FormatHelper.CleanString(this.ucLabEditAQL3.Value.Trim())) + ";" + System.Int32.Parse(FormatHelper.CleanString(this.ucLabEditAC3.Value.Trim())) + ";" + System.Int32.Parse(FormatHelper.CleanString(this.ucLabEditRE3.Value.Trim()));
            ZNodeVaule = System.Decimal.Parse(FormatHelper.CleanString(this.ucLabEditAQL4.Value.Trim())) + ";" + System.Int32.Parse(FormatHelper.CleanString(this.ucLabEditAC4.Value.Trim())) + ";" + System.Int32.Parse(FormatHelper.CleanString(this.ucLabEditRE4.Value.Trim()));

            int objNUm = 0;
            int OQCNum = 0;
            foreach (DataRow row in this.dtCheckItem.Rows)
            {
                if (Convert.ToBoolean(row["Checked"]) == true)
                {
                    objNUm += 1;
                }
            }
            //GET NEWOQCPARA
            OQCPara[] newOQCPara = new OQCPara[objNUm + 4];
            for (OQCNum = 0; OQCNum < 4; OQCNum++)
            {
                OQCPara newone = new OQCPara();
                newOQCPara[OQCNum] = newone;
                //modify by alex 2010.11.8
                //newOQCPara[OQCNum].TemplateName = ApplicationService.Current().ResourceCode;
                newOQCPara[OQCNum].TemplateName = oqcLot.ItemCode;
                newOQCPara[OQCNum].ISTemplate = "N";
                if (OQCNum == 0)
                {
                    newOQCPara[OQCNum].NodeName = OQCFacade.OQC_AGrade;
                    newOQCPara[OQCNum].NodeValue = ANodeVaule;
                }
                if (OQCNum == 1)
                {
                    newOQCPara[OQCNum].NodeName = OQCFacade.OQC_BGrade;
                    newOQCPara[OQCNum].NodeValue = BNodeVaule;
                }
                if (OQCNum == 2)
                {
                    newOQCPara[OQCNum].NodeName = OQCFacade.OQC_CGrade;
                    newOQCPara[OQCNum].NodeValue = CNodeVaule;
                }
                if (OQCNum == 3)
                {
                    newOQCPara[OQCNum].NodeName = OQCFacade.OQC_ZGrade;
                    newOQCPara[OQCNum].NodeValue = ZNodeVaule;
                }
                newOQCPara[OQCNum].MaintainUser = ApplicationService.Current().UserCode;
            }

            foreach (DataRow row in this.dtCheckItem.Rows)
            {
                if (Convert.ToBoolean(row["Checked"]) == true)
                {

                    OQCPara newone = new OQCPara();
                    newOQCPara[OQCNum] = newone;
                    //modify by alex 2010.11.8
                    //newOQCPara[OQCNum].TemplateName = ApplicationService.Current().ResourceCode;
                    newOQCPara[OQCNum].TemplateName = oqcLot.ItemCode;
                    newOQCPara[OQCNum].ISTemplate = "N";
                    newOQCPara[OQCNum].NodeName = Convert.ToString(row["CheckGroup"]);
                    newOQCPara[OQCNum].NodeValue = Convert.ToString(row["SampleQty"]);
                    newOQCPara[OQCNum].MaintainUser = ApplicationService.Current().UserCode;
                    OQCNum++;
                }
            }
            if (objOqcPara == null)
            {
                oqcf.AddOQCPara(newOQCPara);
            }
            else
            {
                OQCPara[] oldOQCPara = new OQCPara[objOqcPara.Length];
                for (int i = 0; i < objOqcPara.Length; i++)
                {
                    oldOQCPara[i] = ((OQCPara)objOqcPara[i]);
                }
                oqcf.DeleteOQCPara(oldOQCPara);
                oqcf.AddOQCPara(newOQCPara);
            }               
        }

        public void btnLoadConfig_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (oqcLotNo == string.Empty)
                {
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(UserControl.MessageType.Error, "$Error_Please_Select_lotNo"));
                    this.cbbLotNO.Focus();
                    return;
                }
                //OQCConfiger configer = OQCConfiger.Load();
                string strA = string.Empty;
                string strB = string.Empty;
                string strC = string.Empty;
                string strZ = string.Empty;
                string strAAQL = string.Empty;
                string strAAC = string.Empty;
                string strARE = string.Empty;
                string strBAQL = string.Empty;
                string strBAC = string.Empty;
                string strBRE = string.Empty;
                string strCAQL = string.Empty;
                string strCAC = string.Empty;
                string strCRE = string.Empty;
                string strZAQL = string.Empty;
                string strZAC = string.Empty;
                string strZRE = string.Empty;
                int intStringLength = 0;
                int numOne = 0;
                int numTwo = 0;

                object[] objOQCParaAll = this.LoadOQCPara();
                if (objOQCParaAll == null)
                {
                    this.ucLabEditSampleQty.Value = string.Empty;
                    this.ucLabEditLotQty.Value = (0).ToString();
                    this.ucLabEditAQL1.Value = (0.000000m).ToString();
                    this.ucLabEditAC1.Value = (0).ToString();
                    this.ucLabEditRE1.Value = (0).ToString();
                    this.ucLabEditAQL2.Value = (0.000000m).ToString();
                    this.ucLabEditAC2.Value=(0).ToString();
                    this.ucLabEditRE2.Value = (0).ToString();
                    this.ucLabEditAQL3.Value = (0.000000m).ToString();
                    this.ucLabEditAC3.Value = (0).ToString();
                    this.ucLabEditRE3.Value = (0).ToString();
                    this.ucLabEditAQL4.Value = (0.000000m).ToString();
                    this.ucLabEditAC4.Value = (0).ToString();
                    this.ucLabEditRE4.Value = (0).ToString();
                }
                else
                {
                    foreach (object obj in objOQCParaAll)
                    {
                        if (((OQCPara)obj).NodeName == OQCFacade.OQC_AGrade)
                        {
                            strA = ((OQCPara)obj).NodeValue;
                        }
                        if (((OQCPara)obj).NodeName == OQCFacade.OQC_BGrade)
                        {
                            strB = ((OQCPara)obj).NodeValue;
                        }
                        if (((OQCPara)obj).NodeName == OQCFacade.OQC_CGrade)
                        {
                            strC = ((OQCPara)obj).NodeValue;
                        }
                        if (((OQCPara)obj).NodeName == OQCFacade.OQC_ZGrade)
                        {
                            strZ = ((OQCPara)obj).NodeValue;
                        }
                    }

                    intStringLength = strA.Length;
                    numOne = strA.IndexOf(";");
                    numTwo = strA.IndexOf(";", numOne+1);
                    strAAQL = strA.Substring(0, numOne);
                    strAAC = strA.Substring(numOne+1, (numTwo - numOne-1));
                    strARE = strA.Substring(numTwo+1, (intStringLength - numTwo-1));

                    intStringLength = strB.Length;
                    numOne = strB.IndexOf(";");
                    numTwo = strB.IndexOf(";", numOne+1);
                    strBAQL = strB.Substring(0, numOne);
                    strBAC = strB.Substring(numOne+1, (numTwo - numOne-1));
                    strBRE = strB.Substring(numTwo+1, (intStringLength - numTwo-1));

                    intStringLength = strC.Length;
                    numOne = strC.IndexOf(";");
                    numTwo = strC.IndexOf(";", numOne+1);
                    strCAQL = strC.Substring(0, numOne);
                    strCAC = strC.Substring(numOne+1, (numTwo - numOne-1));
                    strCRE = strC.Substring(numTwo+1, (intStringLength - numTwo-1));

                    intStringLength = strZ.Length;
                    numOne = strZ.IndexOf(";");
                    numTwo = strZ.IndexOf(";", numOne+1);
                    strZAQL = strZ.Substring(0, numOne);
                    strZAC = strZ.Substring(numOne+1, (numTwo - numOne-1));
                    strZRE = strZ.Substring(numTwo+1, (intStringLength - numTwo-1));


                    strAAQL = this.FormatString(strAAQL);
                    strBAQL = this.FormatString(strBAQL);
                    strCAQL = this.FormatString(strCAQL);
                    strZAQL = this.FormatString(strZAQL);                   

                    this.ucLabEditAC1.Value = strAAC;
                    this.ucLabEditAC2.Value = strBAC;
                    this.ucLabEditAC3.Value = strCAC;
                    this.ucLabEditAC4.Value = strZAC;
                    this.ucLabEditAQL1.Value = strAAQL;
                    this.ucLabEditAQL2.Value = strBAQL;
                    this.ucLabEditAQL3.Value = strCAQL;
                    this.ucLabEditAQL4.Value = strZAQL;
                    this.ucLabEditRE1.Value = strARE;
                    this.ucLabEditRE2.Value = strBRE;
                    this.ucLabEditRE3.Value = strCRE;
                    this.ucLabEditRE4.Value = strZRE;

                    Infragistics.Win.UltraWinGrid.UltraGridRow row;                    
                    for (int i = 0; i < this.ultraGridOQCCheckType.Rows.Count; i++)
                    {
                        row = this.ultraGridOQCCheckType.Rows[i];
                        foreach (object obj in objOQCParaAll)
                        {
                            if (((OQCPara)obj).NodeName == Convert.ToString(row.Cells["CheckGroup"].Text))
                            {
                                row.Cells["Checked"].Value = true;
                                row.Cells["SampleQty"].Value = ((OQCPara)obj).NodeValue;
                                break;
                            }
                            else
                            {
                                row.Cells["Checked"].Value = false;
                                row.Cells["SampleQty"].Value = 0;
                            }
                        }
                    }
                }
                this.ultraGridOQCCheckType.UpdateData();
            }
            catch (Exception ex)
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(ex));
            }
        }

        private Messages ValidateInputForConfiger()
        {
            Messages messages = new Messages();

            #region check
            try
            {
                System.Decimal.Parse(FormatHelper.CleanString(this.ucLabEditAQL1.Value.Trim()));

                if (System.Decimal.Parse(FormatHelper.CleanString(this.ucLabEditAQL1.Value.Trim())) < 0)
                {
                    messages.Add(new UserControl.Message(MessageType.Error, "$CS_A_Grade_AQL_Can not_be_minus"));

                    return messages;
                }
            }
            catch
            {
                messages.Add(new UserControl.Message(MessageType.Error, "$CS_A_Grade_AQL_Must_be_numberType"));

                return messages;
            }

            try
            {
                System.Int32.Parse(FormatHelper.CleanString(this.ucLabEditAC1.Value.Trim()));

                if (System.Int32.Parse(FormatHelper.CleanString(this.ucLabEditAC1.Value.Trim())) < 0)
                {
                    messages.Add(new UserControl.Message(MessageType.Error, "$CS_A_Grade_AC_Can_not_be_minus"));

                    return messages;
                }
            }
            catch
            {
                messages.Add(new UserControl.Message(MessageType.Error, "$CS_A_Grade_AC_Must_be_IntType"));

                return messages;
            }

            try
            {
                System.Int32.Parse(FormatHelper.CleanString(this.ucLabEditRE1.Value.Trim()));

                if (System.Int32.Parse(FormatHelper.CleanString(this.ucLabEditRE1.Value.Trim())) < 0)
                {
                    messages.Add(new UserControl.Message(MessageType.Error, "$CS_A_Grade_RE_Can_not_be_minus"));

                    return messages;
                }
            }
            catch
            {
                messages.Add(new UserControl.Message(MessageType.Error, "$CS_A_Grade_RE_Must_be_IntType"));

                return messages;
            }

            try
            {
                System.Decimal.Parse(FormatHelper.CleanString(this.ucLabEditAQL2.Value.Trim()));

                if (System.Decimal.Parse(FormatHelper.CleanString(this.ucLabEditAQL2.Value.Trim())) < 0)
                {
                    messages.Add(new UserControl.Message(MessageType.Error, "$CS_B_Grade_AQL_Can_not_be_minus"));

                    return messages;
                }
            }
            catch
            {
                messages.Add(new UserControl.Message(MessageType.Error, "$CS_B_Grade_AQL_Must_be_NumberType"));

                return messages;
            }

            try
            {
                System.Int32.Parse(FormatHelper.CleanString(this.ucLabEditAC2.Value.Trim()));


                if (System.Int32.Parse(FormatHelper.CleanString(this.ucLabEditAC2.Value.Trim())) < 0)
                {
                    messages.Add(new UserControl.Message(MessageType.Error, "$CS_B_Grade_AC_Can_not_be_minus"));

                    return messages;
                }
            }
            catch
            {
                messages.Add(new UserControl.Message(MessageType.Error, "$CS_B_Grade_AC_Must_be_IntType"));

                return messages;
            }

            try
            {
                System.Int32.Parse(FormatHelper.CleanString(this.ucLabEditRE2.Value.Trim()));

                if (System.Int32.Parse(FormatHelper.CleanString(this.ucLabEditRE2.Value.Trim())) < 0)
                {
                    messages.Add(new UserControl.Message(MessageType.Error, "$CS_B_Grade_RE_Can_not_be_minus"));

                    return messages;
                }
            }
            catch
            {
                messages.Add(new UserControl.Message(MessageType.Error, "$CS_B_Grade_RE_Must_be_IntType"));

                return messages;
            }

            try
            {
                System.Decimal.Parse(FormatHelper.CleanString(this.ucLabEditAQL3.Value.Trim()));

                if (System.Decimal.Parse(FormatHelper.CleanString(this.ucLabEditAQL3.Value.Trim())) < 0)
                {
                    messages.Add(new UserControl.Message(MessageType.Error, "$CS_C_Grade_AQL_Can_not_be_minus"));

                    return messages;
                }
            }
            catch
            {
                messages.Add(new UserControl.Message(MessageType.Error, "$CS_C_Grade_AQL_Must_be_NumberType"));

                return messages;
            }

            try
            {
                System.Int32.Parse(FormatHelper.CleanString(this.ucLabEditAC3.Value.Trim()));

                if (System.Int32.Parse(FormatHelper.CleanString(this.ucLabEditAC3.Value.Trim())) < 0)
                {
                    messages.Add(new UserControl.Message(MessageType.Error, "$CS_C_Grade_AC_Can_not_be_minus"));

                    return messages;
                }
            }
            catch
            {
                messages.Add(new UserControl.Message(MessageType.Error, "$CS_C_Grade_AC_Must_be_IntType"));

                return messages;
            }

            try
            {
                System.Int32.Parse(FormatHelper.CleanString(this.ucLabEditRE3.Value.Trim()));

                if (System.Int32.Parse(FormatHelper.CleanString(this.ucLabEditRE3.Value.Trim())) < 0)
                {
                    messages.Add(new UserControl.Message(MessageType.Error, "$CS_C_Grade_RE_Can_not_be_minus"));

                    return messages;
                }
            }
            catch
            {
                messages.Add(new UserControl.Message(MessageType.Error, "$CS_C_Grade_RE_Must_be_IntType"));

                return messages;
            }

            try
            {
                System.Decimal.Parse(FormatHelper.CleanString(this.ucLabEditAQL4.Value.Trim()));

                if (System.Decimal.Parse(FormatHelper.CleanString(this.ucLabEditAQL4.Value.Trim())) < 0)
                {
                    messages.Add(new UserControl.Message(MessageType.Error, "$CS_Z_Grade_AQL_Can_not_be_minus"));

                    return messages;
                }
            }
            catch
            {
                messages.Add(new UserControl.Message(MessageType.Error, "$CS_Z_Grade_AQL_Must_be_NumberType"));

                return messages;
            }

            try
            {
                System.Int32.Parse(FormatHelper.CleanString(this.ucLabEditAC4.Value.Trim()));

                if (System.Int32.Parse(FormatHelper.CleanString(this.ucLabEditAC4.Value.Trim())) < 0)
                {
                    messages.Add(new UserControl.Message(MessageType.Error, "$CS_Z_Grade_AC_Can_not_be_minus"));

                    return messages;
                }
            }
            catch
            {
                messages.Add(new UserControl.Message(MessageType.Error, "$CS_Z_Grade_AC_Must_be_IntType"));

                return messages;
            }

            try
            {
                System.Int32.Parse(FormatHelper.CleanString(this.ucLabEditRE4.Value.Trim()));

                if (System.Int32.Parse(FormatHelper.CleanString(this.ucLabEditRE4.Value.Trim())) < 0)
                {
                    messages.Add(new UserControl.Message(MessageType.Error, "$CS_Z_Grade_RE_Can_not_be_minus"));

                    return messages;
                }
            }
            catch
            {
                messages.Add(new UserControl.Message(MessageType.Error, "$CS_Z_Grade_RE_Must_be_IntType"));

                return messages;
            }

            if (this.ucLabEditAQL1.Value == "0" &&
                this.ucLabEditAC1.Value == "0" &&
                this.ucLabEditRE1.Value == "0" &&
                this.ucLabEditAQL2.Value == "0" &&
                this.ucLabEditAC2.Value == "0" &&
                this.ucLabEditRE2.Value == "0" &&
                this.ucLabEditAQL3.Value == "0" &&
                this.ucLabEditAC3.Value == "0" &&
                this.ucLabEditRE3.Value == "0" &&
                this.ucLabEditAQL4.Value == "0" &&
                this.ucLabEditAC4.Value == "0" &&
                this.ucLabEditRE4.Value == "0")
            {
                messages.Add(new UserControl.Message(MessageType.Error, "$CS_Please_Maintain_Parameters"));

                return messages;
            }

            try
            {
                if (this.cbbLotNO.Text.Trim().Length == 0)
                {
                    messages.Add(new UserControl.Message(MessageType.Error, "$Please_Input_Lot_No"));
                    return messages;
                }
                string lotno = FormatHelper.PKCapitalFormat(this.cbbLotNO.Text);
                OQCFacade oqcFacade = new OQCFacade(this.DataProvider);
                object[] lot2cardList = oqcFacade.ExactQueryOQCLot2Card(lotno, OQCFacade.Lot_Sequence_Default);

                object objLot = oqcFacade.GetOQCLot(FormatHelper.PKCapitalFormat(this.cbbLotNO.Text.ToString()), OQCFacade.Lot_Sequence_Default);

                if (lot2cardList == null || lot2cardList.Length == 0)
                {
                    messages.Add(new UserControl.Message(MessageType.Error, "$CS_NO_Pallet_in_This_Lot"));

                    return messages;
                }
                //string itemCode = (lot2cardList[0] as OQCLot2Card).ItemCode;
                //ItemFacade itemFacade = new ItemFacade(this.DataProvider);
                //object item = itemFacade.GetItem(itemCode, GlobalVariables.CurrentOrganizations.First().OrganizationID);
                Decimal lotSize = 0;
                if (objLot != null)
                {
                    lotSize = ((OQCLot)objLot).LotSize;
                }

                this.ultraGridOQCCheckType.UpdateData();
                for (int i = 0; i < this.ultraGridOQCCheckType.Rows.Count; i++)
                {
                    Infragistics.Win.UltraWinGrid.UltraGridRow row = this.ultraGridOQCCheckType.Rows[i];
                    if (Convert.ToBoolean(row.Cells["Checked"].Value) == true)
                    {
                        if (Convert.ToString(row.Cells["SampleQty"].Text).Trim().Length == 0
                            || Convert.ToInt32(row.Cells["SampleQty"].Text) <= 0
                            || Convert.ToInt32(row.Cells["SampleQty"].Text) > lotSize)
                        {
                            //messages.Add(new UserControl.Message(MessageType.Error, "$CS_SampleQty_Should_bigger than 0 and not bigger than LotQty" + lotSize.ToString()));
                            messages.Add(new UserControl.Message(MessageType.Error, "$CS_SampleQty_Should_bigger_than_0 $CS_And_not_bigger_than_LotQty:" + lotSize.ToString()));
                            return messages;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                messages.Add(new UserControl.Message(MessageType.Error, ex.Message));
                return messages;
            }

            #endregion

            return messages;
        }

        #endregion

        private void btnGetLot_Click(object sender, System.EventArgs e)
        {
            this.InitLotNo();

            DataCollect.DataCollectFacade dcf = new BenQGuru.eMES.DataCollect.DataCollectFacade(DataProvider);
            string rcard = txtRcard.Value.ToUpper().Trim();
            string cartonCode = txtCartonCode.Value.ToUpper().Trim();

            InitCheckGroup();
            SetEditObject(null);

            //根据当前序列号获取产品原始的序列号
            string sourceRCard = dcf.GetSourceCard(rcard.Trim().ToUpper(), string.Empty);

            if (rcard != String.Empty)
            {
                object obj = dcf.GetSimulation(sourceRCard);
                if (obj != null)
                {
                    Domain.DataCollect.Simulation sim = obj as Domain.DataCollect.Simulation;

                    string lotno = sim.LOTNO;
                    //					for(int i = 0;i < cbbLotNO.Items;i ++)
                    //					{
                    //						if(lotno == cbbLotNO.Items[i])
                    //						{
                    if (lotno == string.Empty)
                    {
                        Messages messages = new Messages();
                        messages.Add(new UserControl.Message(MessageType.Error, "$RCard_No_Lot"));
                        ApplicationRun.GetInfoForm().Add(messages);

                        this.txtRcard.TextFocus(false, true);
                        return;
                    }
                    if (!CheckLotStatus(lotno))
                    {
                        this.txtRcard.TextFocus(false, true);
                        return;
                    }
                    for (int i = 0; i < cbbLotNO.Items.Count; i++)
                    {
                        if (cbbLotNO.Items[i].ToString().ToUpper().Trim() == lotno)
                        {
                            cbbLotNO.SelectedIndex = i;
                            break;
                        }
                    }
                }
                else
                {
                    Messages messages = new Messages();
                    messages.Add(new UserControl.Message(MessageType.Error, "$NoProductInfo"));
                    ApplicationRun.GetInfoForm().Add(messages);
                    this.txtRcard.TextFocus(false, true);
                    //Remove UCLabel.SelectAll;
                }
            }
            //add by alex 2010.11.8
            else if (cartonCode != String.Empty)
            {
                OQCFacade oqcFacade = new OQCFacade(this.DataProvider);
                object obj = oqcFacade.GetLot2CartonByCartonNo(cartonCode);
                if (obj != null)
                {
                    Lot2Carton lot2Carton = obj as Lot2Carton;

                    string lotno = lot2Carton.OQCLot;
                    if (lotno == string.Empty)
                    {
                        Messages messages = new Messages();
                        messages.Add(new UserControl.Message(MessageType.Error, "$CS_CARTON_NOT_EXIST_LOT"));
                        ApplicationRun.GetInfoForm().Add(messages);

                        this.txtCartonCode.TextFocus(false, true);
                        return;
                    }

                    if (!CheckLotStatus(lot2Carton.OQCLot.Trim()))
                    {
                        this.txtCartonCode.TextFocus(false, true);
                        return;
                    }
                    for (int i = 0; i < cbbLotNO.Items.Count; i++)
                    {
                        if (cbbLotNO.Items[i].ToString().ToUpper().Trim() == lotno)
                        {
                            cbbLotNO.SelectedIndex = i;
                            break;
                        }
                    }
                }
                else
                {
                    Messages messages = new Messages();
                    messages.Add(new UserControl.Message(MessageType.Error, "$NoLol2CartonInfo"));
                    ApplicationRun.GetInfoForm().Add(messages);
                    this.txtCartonCode.TextFocus(false, true);                    
                }
            }
            else
            {
                this.txtRcard.TextFocus(false, true);
                //Remove UCLabel.SelectAll;
            }
        }

        private bool CheckLotStatus(string lotno)
        {
            OQCFacade oqcFacade = new OQCFacade(DataProvider);
            OQCLot oqcLot = oqcFacade.GetOQCLot(lotno, OQCFacade.Lot_Sequence_Default) as OQCLot;
            //判断批状态，为以下4种状态的继续
            if (oqcLot.LOTStatus == OQCLotStatus.OQCLotStatus_Initial || oqcLot.LOTStatus == OQCLotStatus.OQCLotStatus_Examing ||
                 oqcLot.LOTStatus == OQCLotStatus.OQCLotStatus_NoExame || oqcLot.LOTStatus == OQCLotStatus.OQCLotStatus_SendExame)
            {

            }
            else if (oqcLot.LOTStatus == OQCLotStatus.OQCLotStatus_Reject || oqcLot.LOTStatus == OQCLotStatus.OQCLotStatus_Rejecting)
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_Lot_Has_Reject"));
                return false;
            }
            else if (oqcLot.LOTStatus == OQCLotStatus.OQCLotStatus_Pass || oqcLot.LOTStatus == OQCLotStatus.OQCLotStatus_Passing)
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_Lot_Has_Pass"));
                return false;
            }
            return true;
        }

        private void cbbLotNO_Enter(object sender, System.EventArgs e)
        {
            cbbLotNO.BackColor = Color.GreenYellow;
        }

        private void cbbLotNO_Leave(object sender, System.EventArgs e)
        {
            cbbLotNO.BackColor = Color.White;
        }

        private void txtRcard_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                this.btnGetLot_Click(this.btnGetLot, null);
            }
        }        

        private void ultraGridMain_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            ultraWinGridHelper = new UltraWinGridHelper(this.ultraGridOQCCheckType);

            ultraWinGridHelper.AddCheckColumn("Checked", "");
            ultraWinGridHelper.AddReadOnlyColumn("CheckGroup", "检验类型");
            ultraWinGridHelper.AddCommonColumn("SampleQty", "样本数量");
            //this.InitGridLanguage(ultraGridOQCCheckType);
        }

        private void InitCheckGroup()
        {
            dtCheckItem.Rows.Clear();

            OQCFacade oqcFacade = new OQCFacade(DataProvider);
            object[] checkGroupList = oqcFacade.GetAllOQCCheckGroup();
            if (checkGroupList != null)
            {
                foreach (OQCCheckGroup oqcCKGroup in checkGroupList)
                {
                    dtCheckItem.Rows.Add(new object[] { false, oqcCKGroup.CheckGroupCode, 0 });
                }
                this.dtCheckItem.AcceptChanges();
            }

        }

        private void InitializeUltraGrid()
        {
            dtCheckItem.Columns.Clear();

            dtCheckItem.Columns.Add("Checked", typeof(bool));
            dtCheckItem.Columns.Add("CheckGroup", typeof(string)).ReadOnly = true;
            dtCheckItem.Columns.Add("SampleQty", typeof(int));

            this.ultraGridOQCCheckType.DataSource = dtCheckItem;
        }

        private void cbbLotNO_Click(object sender, EventArgs e)
        {
            this.InitLotNo();
        }

        private object[] LoadOQCPara()
        {
            OQCFacade oqcf = new OQCFacade(DataProvider);
            //modify by alex 2010.11.8
            //object[] objOqcPara = oqcf.GetOQCPara(ApplicationService.Current().ResourceCode);      
            OQCLot oqcLot = oqcf.GetOQCLot(FormatHelper.PKCapitalFormat(oqcLotNo), OQCFacade.Lot_Sequence_Default) as OQCLot;
            object[] objOqcPara = oqcf.GetOQCPara(oqcLot.ItemCode);
            return objOqcPara;
        }

        private void ultraGridMain_CellDataError(object sender, Infragistics.Win.UltraWinGrid.CellDataErrorEventArgs e)
        {
            MessageBox.Show(MutiLanguages.ParserString("$CS_SampleQty_Error"), MutiLanguages.ParserString("$ShowMessage"), MessageBoxButtons.OK);
            e.RaiseErrorEvent=false;
        }

        private string FormatString(string strFormat)
        {
            string returnString = string.Empty;            
            if (strFormat.IndexOf(".") == -1)
            {
                returnString = strFormat + ".00000";
                return returnString;
            }
            else
            {
                int findNumber = 0;
                int stringLength = strFormat.Length;
                findNumber = strFormat.IndexOf(".");
                if (stringLength - findNumber == 1)
                {
                    returnString = strFormat + "00000";
                    return returnString;
                }
                if (stringLength - findNumber == 2)
                {
                    returnString = strFormat + "0000";
                    return returnString;
                }
                if (stringLength - findNumber == 3)
                {
                    returnString = strFormat + "000";
                    return returnString;
                }
                if (stringLength - findNumber == 4)
                {
                    returnString = strFormat + "00";
                    return returnString;
                }
                if (stringLength - findNumber == 5)
                {
                    returnString = strFormat + "0";
                    return returnString;
                }
                return strFormat;

            }

        }

        //add by alex 2010.11.8
        private void txtCartonCode_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                this.btnGetLot_Click(this.btnGetLot, null);
            }
        }

     
          
       
    }
}
        
    //#region Added By Karron Qiu,2006-6-26,保存到配置文件
internal class OQCConfiger
{
    private static readonly string FileName = "OQCConfiguration.OQC";

    public int AC1 = 0;
    public int AC2 = 0;
    public int AC3 = 0;
    public int AC4 = 0;

    public decimal AQL1 = 0.0m;
    public decimal AQL2 = 0.0m;
    public decimal AQL3 = 0.0m;
    public decimal AQL4 = 0.0m;

    public int RE1 = 0;
    public int RE2 = 0;
    public int RE3 = 0;
    public int RE4 = 0;

    // For CheckGroup + SampleQty
    public Hashtable CheckGroupList = new Hashtable();

    public static OQCConfiger Load()
    {
        OQCConfiger configer = new OQCConfiger();

        if (!System.IO.File.Exists(FileName))
            return configer;

        using (StreamReader reader = System.IO.File.OpenText(FileName))
        {
            #region AC
            string str = reader.ReadLine();
            if (str != null)
            {
                try
                {
                    configer.AC1 = System.Int32.Parse(FormatHelper.CleanString(str));
                }
                catch
                {
                    configer.AC1 = 0;
                }
            }

            str = reader.ReadLine();
            if (str != null)
            {
                try
                {
                    configer.AC2 = System.Int32.Parse(FormatHelper.CleanString(str));
                }
                catch
                {
                    configer.AC2 = 0;
                }
            }

            str = reader.ReadLine();
            if (str != null)
            {
                try
                {
                    configer.AC3 = System.Int32.Parse(FormatHelper.CleanString(str));
                }
                catch
                {
                    configer.AC3 = 0;
                }
            }

            str = reader.ReadLine();
            if (str != null)
            {
                try
                {
                    configer.AC4 = System.Int32.Parse(FormatHelper.CleanString(str));
                }
                catch
                {
                    configer.AC4 = 0;
                }
            }
            #endregion

            #region AQL
            str = reader.ReadLine();
            if (str != null)
            {
                try
                {
                    configer.AQL1 = System.Decimal.Parse(FormatHelper.CleanString(str));
                }
                catch
                {
                    configer.AQL1 = 0.0m;
                }
            }

            str = reader.ReadLine();
            if (str != null)
            {
                try
                {
                    configer.AQL2 = System.Decimal.Parse(FormatHelper.CleanString(str));
                }
                catch
                {
                    configer.AQL2 = 0.0m;
                }
            }

            str = reader.ReadLine();
            if (str != null)
            {
                try
                {
                    configer.AQL3 = System.Decimal.Parse(FormatHelper.CleanString(str));
                }
                catch
                {
                    configer.AQL3 = 0.0m;
                }
            }

            str = reader.ReadLine();
            if (str != null)
            {
                try
                {
                    configer.AQL4 = System.Decimal.Parse(FormatHelper.CleanString(str));
                }
                catch
                {
                    configer.AQL4 = 0.0m;
                }
            }
            #endregion

            #region RE
            str = reader.ReadLine();
            if (str != null)
            {
                try
                {
                    configer.RE1 = System.Int32.Parse(FormatHelper.CleanString(str));
                }
                catch
                {
                    configer.RE1 = 0;
                }
            }

            str = reader.ReadLine();
            if (str != null)
            {
                try
                {
                    configer.RE2 = System.Int32.Parse(FormatHelper.CleanString(str));
                }
                catch
                {
                    configer.RE2 = 0;
                }
            }

            str = reader.ReadLine();
            if (str != null)
            {
                try
                {
                    configer.RE3 = System.Int32.Parse(FormatHelper.CleanString(str));
                }
                catch
                {
                    configer.RE3 = 0;
                }
            }

            str = reader.ReadLine();
            if (str != null)
            {
                try
                {
                    configer.RE4 = System.Int32.Parse(FormatHelper.CleanString(str));
                }
                catch
                {
                    configer.RE4 = 0;
                }
            }
            #endregion

            str = reader.ReadLine();
            string checkGroupCode = "";
            int checkGroupQty = 0;
            while (str != null)
            {
                str = FormatHelper.CleanString(str);
                checkGroupCode = str.Split('|')[0];
                checkGroupQty = System.Int32.Parse(str.Split('|')[1]);

                configer.CheckGroupList.Add(checkGroupCode, checkGroupQty);

                str = reader.ReadLine();
            }
        }

        return configer;
    }

    public static void Save(OQCConfiger configer)
    {
        if (System.IO.File.Exists(FileName))
            System.IO.File.Delete(FileName);

        using (StreamWriter sr = System.IO.File.CreateText(FileName))
        {
            sr.WriteLine(configer.AC1);
            sr.WriteLine(configer.AC2);
            sr.WriteLine(configer.AC3);
            sr.WriteLine(configer.AC4);

            sr.WriteLine(configer.AQL1);
            sr.WriteLine(configer.AQL2);
            sr.WriteLine(configer.AQL3);
            sr.WriteLine(configer.AQL4);

            sr.WriteLine(configer.RE1);
            sr.WriteLine(configer.RE2);
            sr.WriteLine(configer.RE3);
            sr.WriteLine(configer.RE4);

            foreach (string checkGroup in configer.CheckGroupList.Keys)
            {
                sr.WriteLine(checkGroup + "|" + configer.CheckGroupList[checkGroup].ToString());
            }
        }
    }
}