#region system
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using System.Data;
#endregion

#region Project
using BenQGuru.eMES.OQC;
using BenQGuru.eMES.Domain.OQC;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Client.Service;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.DataCollect.Action;
using BenQGuru.eMES.DataCollect;
using UserControl;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.TSModel;
using BenQGuru.eMES.Domain.TSModel;
using System.IO;
using System.Collections.Generic;

#endregion

namespace BenQGuru.eMES.Client
{
    public class FCollectionOQC : Form
    {
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox4;
        private UserControl.UCButton ucButton4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Splitter splitter2;
        public UserControl.UCLabelEdit ucLabOQCLot;
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel pnlChildErrorCode;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.ListBox listBox3;
        private UserControl.UCErrorCodeSelect errorCodeSelect;

        //region
        //private Infragistics.Win.UltraWinGrid.UltraDropDown ultraDropDownOQCGrade;
        DataTable dtCheckItem = new DataTable();
        public UserControl.UCLabelEdit ucLabRunningID;
        private System.Windows.Forms.ListBox lbRunningIDList;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGridMain;
        private UserControl.UCLabelEdit ucLabelEditBGrade;
        private UserControl.UCLabelEdit ucLabelEditCGrade;
        private UserControl.UCLabelEdit ucLabelEditAGrade;
        private System.Windows.Forms.ListBox listBoxErrorInformation;
        private UserControl.UCButton ucButtonOQCSample;
        private UserControl.UCButton ucButtonOQC;
        private UserControl.UCButton ucButtonOK;

        private string formStats = FormStatus.Noready;
        private Infragistics.Win.UltraWinEditors.UltraOptionSet ultraOptionSetPassReject;


        private IDomainDataProvider _domainDataProvider = ApplicationService.Current().DataProvider;
        private CheckMNIDManager _checkMNIDManager = new CheckMNIDManager();

        UltraWinGridHelper ultraWinGridHelper;
        ValueList valueList;
        private UserControl.UCLabelEdit leCheckedCount;
        ValueList valuResult;
        //Laws Lu,2005/11/01,新增	改善性能
        private Hashtable listActionCheckStatus = new Hashtable();
        private UserControl.UCButton btnFacilityInfo;
        private UserControl.UCButton btnGetLot;
        private UserControl.UCLabelEdit txtRcard;
        private UserControl.UCLabelEdit txtMem;
        private UltraGrid ultraGridCheckGroup;
        private Domain.BaseSetting.Resource Resource;
        private UCButton ucButtonForcePass;
        private UCLabelEdit ucLabelEditZGrade;
        private UCButton ucButtonCancel;
        DataTable dtCheckGroup = new DataTable();


        public IDomainDataProvider DataProvider
        {
            get
            {
                return _domainDataProvider;
            }
        }


        public FCollectionOQC()
        {
            // 该调用是 Windows 窗体设计器所必需的。
            InitializeComponent();

            UserControl.UIStyleBuilder.FormUI(this);
            //UserControl.UIStyleBuilder.UltraOptionSetUI(this.ultraOptionSet2);
            //UserControl.UIStyleBuilder.UltraOptionSetUI(this.ultraOptionSet6);
            //traCombo1.
            UserControl.UIStyleBuilder.UltraOptionSetUI(this.ultraOptionSetPassReject);
            this.ultraGridMain.DisplayLayout.Appearance.BackColor = System.Drawing.Color.White; ;
            this.ultraGridMain.DisplayLayout.CaptionAppearance.BackColor = Color.FromName("WhiteSmoke");
            this.ultraGridMain.DisplayLayout.Appearance.BackColor = Color.FromArgb(255, 255, 255);
            this.ultraGridMain.DisplayLayout.Override.HeaderAppearance.BackColor = Color.FromName("WhiteSmoke");
            this.ultraGridMain.DisplayLayout.Override.RowAppearance.BackColor = Color.FromArgb(230, 234, 245);
            this.ultraGridMain.DisplayLayout.Override.RowAlternateAppearance.BackColor = Color.FromArgb(255, 255, 255);
            this.ultraGridMain.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.ultraGridMain.DisplayLayout.Override.ActiveRowAppearance.BackColor = System.Drawing.Color.Gainsboro;
            this.ultraGridMain.DisplayLayout.Override.ActiveRowAppearance.ForeColor = System.Drawing.Color.Black;
            this.ultraGridMain.DisplayLayout.ScrollBarLook.Appearance.BackColor = Color.FromName("LightGray");

            this.ultraGridCheckGroup.DisplayLayout.Appearance.BackColor = System.Drawing.Color.White; ;
            this.ultraGridCheckGroup.DisplayLayout.CaptionAppearance.BackColor = Color.FromName("WhiteSmoke");
            this.ultraGridCheckGroup.DisplayLayout.Appearance.BackColor = Color.FromArgb(255, 255, 255);
            this.ultraGridCheckGroup.DisplayLayout.Override.HeaderAppearance.BackColor = Color.FromName("WhiteSmoke");
            this.ultraGridCheckGroup.DisplayLayout.Override.RowAppearance.BackColor = Color.FromArgb(230, 234, 245);
            this.ultraGridCheckGroup.DisplayLayout.Override.RowAlternateAppearance.BackColor = Color.FromArgb(255, 255, 255);
            this.ultraGridCheckGroup.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.ultraGridCheckGroup.DisplayLayout.Override.ActiveRowAppearance.BackColor = System.Drawing.Color.Gainsboro;
            this.ultraGridCheckGroup.DisplayLayout.Override.ActiveRowAppearance.ForeColor = System.Drawing.Color.Black;
            this.ultraGridCheckGroup.DisplayLayout.ScrollBarLook.Appearance.BackColor = Color.FromName("LightGray");

            //UserControl.UIStyleBuilder.FormUI(this);	
            //UserControl.UIStyleBuilder.GridUI(this.g

            //MessageBox.Show(this,this.CheckedCard.Count.ToString());
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

        #region 设计器生成的代码
        /// <summary>
        /// 设计器支持所需的方法 - 不要使用代码编辑器修改
        /// 此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FCollectionOQC));
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.ValueListItem valueListItem1 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.ValueListItem valueListItem2 = new Infragistics.Win.ValueListItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ucLabelEditZGrade = new UserControl.UCLabelEdit();
            this.ucLabelEditCGrade = new UserControl.UCLabelEdit();
            this.ucButtonForcePass = new UserControl.UCButton();
            this.btnGetLot = new UserControl.UCButton();
            this.txtRcard = new UserControl.UCLabelEdit();
            this.btnFacilityInfo = new UserControl.UCButton();
            this.ucLabelEditBGrade = new UserControl.UCLabelEdit();
            this.ucLabelEditAGrade = new UserControl.UCLabelEdit();
            this.ucButtonOQCSample = new UserControl.UCButton();
            this.ucButtonOQC = new UserControl.UCButton();
            this.listBoxErrorInformation = new System.Windows.Forms.ListBox();
            this.ultraOptionSetPassReject = new Infragistics.Win.UltraWinEditors.UltraOptionSet();
            this.label1 = new System.Windows.Forms.Label();
            this.ucLabOQCLot = new UserControl.UCLabelEdit();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.ucButtonCancel = new UserControl.UCButton();
            this.txtMem = new UserControl.UCLabelEdit();
            this.ucLabRunningID = new UserControl.UCLabelEdit();
            this.ucButton4 = new UserControl.UCButton();
            this.ucButtonOK = new UserControl.UCButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.panel4 = new System.Windows.Forms.Panel();
            this.pnlChildErrorCode = new System.Windows.Forms.Panel();
            this.errorCodeSelect = new UserControl.UCErrorCodeSelect();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ultraGridMain = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ultraGridCheckGroup = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.leCheckedCount = new UserControl.UCLabelEdit();
            this.lbRunningIDList = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.listBox3 = new System.Windows.Forms.ListBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraOptionSetPassReject)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.pnlChildErrorCode.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridMain)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridCheckGroup)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ucLabelEditCGrade);
            this.groupBox1.Controls.Add(this.ucButtonForcePass);
            this.groupBox1.Controls.Add(this.btnGetLot);
            this.groupBox1.Controls.Add(this.txtRcard);
            this.groupBox1.Controls.Add(this.btnFacilityInfo);
            this.groupBox1.Controls.Add(this.ucLabelEditBGrade);
            this.groupBox1.Controls.Add(this.ucLabelEditAGrade);
            this.groupBox1.Controls.Add(this.ucButtonOQCSample);
            this.groupBox1.Controls.Add(this.ucButtonOQC);
            this.groupBox1.Controls.Add(this.listBoxErrorInformation);
            this.groupBox1.Controls.Add(this.ultraOptionSetPassReject);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.ucLabOQCLot);
            this.groupBox1.Controls.Add(this.ucLabelEditZGrade);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(768, 162);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "抽检批信息";
            // 
            // ucLabelEditZGrade
            // 
            this.ucLabelEditZGrade.AllowEditOnlyChecked = true;
            this.ucLabelEditZGrade.Caption = "Z   ";
            this.ucLabelEditZGrade.CausesValidation = false;
            this.ucLabelEditZGrade.Checked = false;
            this.ucLabelEditZGrade.EditType = UserControl.EditTypes.String;
            this.ucLabelEditZGrade.Location = new System.Drawing.Point(45, 67);
            this.ucLabelEditZGrade.MaxLength = 40;
            this.ucLabelEditZGrade.Multiline = false;
            this.ucLabelEditZGrade.Name = "ucLabelEditZGrade";
            this.ucLabelEditZGrade.PasswordChar = '\0';
            this.ucLabelEditZGrade.ReadOnly = true;
            this.ucLabelEditZGrade.ShowCheckBox = false;
            this.ucLabelEditZGrade.Size = new System.Drawing.Size(170, 24);
            this.ucLabelEditZGrade.TabIndex = 185;
            this.ucLabelEditZGrade.TabNext = true;
            this.ucLabelEditZGrade.Value = "4";
            this.ucLabelEditZGrade.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabelEditZGrade.XAlign = 82;
            // 
            // ucLabelEditCGrade
            // 
            this.ucLabelEditCGrade.AllowEditOnlyChecked = true;
            this.ucLabelEditCGrade.Caption = "C   ";
            this.ucLabelEditCGrade.CausesValidation = false;
            this.ucLabelEditCGrade.Checked = false;
            this.ucLabelEditCGrade.EditType = UserControl.EditTypes.String;
            this.ucLabelEditCGrade.Location = new System.Drawing.Point(45, 134);
            this.ucLabelEditCGrade.MaxLength = 40;
            this.ucLabelEditCGrade.Multiline = false;
            this.ucLabelEditCGrade.Name = "ucLabelEditCGrade";
            this.ucLabelEditCGrade.PasswordChar = '\0';
            this.ucLabelEditCGrade.ReadOnly = true;
            this.ucLabelEditCGrade.ShowCheckBox = false;
            this.ucLabelEditCGrade.Size = new System.Drawing.Size(170, 24);
            this.ucLabelEditCGrade.TabIndex = 23;
            this.ucLabelEditCGrade.TabNext = true;
            this.ucLabelEditCGrade.Value = "3";
            this.ucLabelEditCGrade.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabelEditCGrade.XAlign = 82;
            // 
            // ucButtonForcePass
            // 
            this.ucButtonForcePass.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonForcePass.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonForcePass.BackgroundImage")));
            this.ucButtonForcePass.ButtonType = UserControl.ButtonTypes.None;
            this.ucButtonForcePass.Caption = "批强制通过";
            this.ucButtonForcePass.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonForcePass.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ucButtonForcePass.Location = new System.Drawing.Point(549, 39);
            this.ucButtonForcePass.Name = "ucButtonForcePass";
            this.ucButtonForcePass.Size = new System.Drawing.Size(88, 22);
            this.ucButtonForcePass.TabIndex = 184;
            this.ucButtonForcePass.Click += new System.EventHandler(this.ucButtonForcePass_Click);
            // 
            // btnGetLot
            // 
            this.btnGetLot.BackColor = System.Drawing.SystemColors.Control;
            this.btnGetLot.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnGetLot.BackgroundImage")));
            this.btnGetLot.ButtonType = UserControl.ButtonTypes.None;
            this.btnGetLot.Caption = "获取批";
            this.btnGetLot.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGetLot.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnGetLot.Location = new System.Drawing.Point(284, 40);
            this.btnGetLot.Name = "btnGetLot";
            this.btnGetLot.Size = new System.Drawing.Size(88, 22);
            this.btnGetLot.TabIndex = 183;
            this.btnGetLot.Click += new System.EventHandler(this.btnGetLot_Click);
            // 
            // txtRcard
            // 
            this.txtRcard.AllowEditOnlyChecked = true;
            this.txtRcard.Caption = "产品序列号";
            this.txtRcard.Checked = false;
            this.txtRcard.EditType = UserControl.EditTypes.String;
            this.txtRcard.Location = new System.Drawing.Point(9, 40);
            this.txtRcard.MaxLength = 40;
            this.txtRcard.Multiline = false;
            this.txtRcard.Name = "txtRcard";
            this.txtRcard.PasswordChar = '\0';
            this.txtRcard.ReadOnly = false;
            this.txtRcard.ShowCheckBox = false;
            this.txtRcard.Size = new System.Drawing.Size(273, 24);
            this.txtRcard.TabIndex = 182;
            this.txtRcard.TabNext = true;
            this.txtRcard.Value = "";
            this.txtRcard.WidthType = UserControl.WidthTypes.Long;
            this.txtRcard.XAlign = 82;
            this.txtRcard.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRcard_TxtboxKeyPress);
            // 
            // btnFacilityInfo
            // 
            this.btnFacilityInfo.BackColor = System.Drawing.SystemColors.Control;
            this.btnFacilityInfo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnFacilityInfo.BackgroundImage")));
            this.btnFacilityInfo.ButtonType = UserControl.ButtonTypes.None;
            this.btnFacilityInfo.Caption = "功能测试信息";
            this.btnFacilityInfo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFacilityInfo.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnFacilityInfo.Location = new System.Drawing.Point(643, 12);
            this.btnFacilityInfo.Name = "btnFacilityInfo";
            this.btnFacilityInfo.Size = new System.Drawing.Size(88, 22);
            this.btnFacilityInfo.TabIndex = 25;
            this.btnFacilityInfo.Visible = false;
            this.btnFacilityInfo.Click += new System.EventHandler(this.btnFacilityInfo_Click);
            // 
            // ucLabelEditBGrade
            // 
            this.ucLabelEditBGrade.AllowEditOnlyChecked = true;
            this.ucLabelEditBGrade.Caption = "B   ";
            this.ucLabelEditBGrade.CausesValidation = false;
            this.ucLabelEditBGrade.Checked = false;
            this.ucLabelEditBGrade.EditType = UserControl.EditTypes.String;
            this.ucLabelEditBGrade.Location = new System.Drawing.Point(45, 111);
            this.ucLabelEditBGrade.MaxLength = 40;
            this.ucLabelEditBGrade.Multiline = false;
            this.ucLabelEditBGrade.Name = "ucLabelEditBGrade";
            this.ucLabelEditBGrade.PasswordChar = '\0';
            this.ucLabelEditBGrade.ReadOnly = true;
            this.ucLabelEditBGrade.ShowCheckBox = false;
            this.ucLabelEditBGrade.Size = new System.Drawing.Size(170, 25);
            this.ucLabelEditBGrade.TabIndex = 24;
            this.ucLabelEditBGrade.TabNext = true;
            this.ucLabelEditBGrade.Value = "2";
            this.ucLabelEditBGrade.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabelEditBGrade.XAlign = 82;
            // 
            // ucLabelEditAGrade
            // 
            this.ucLabelEditAGrade.AllowEditOnlyChecked = true;
            this.ucLabelEditAGrade.Caption = "A   ";
            this.ucLabelEditAGrade.Checked = false;
            this.ucLabelEditAGrade.EditType = UserControl.EditTypes.String;
            this.ucLabelEditAGrade.Location = new System.Drawing.Point(45, 89);
            this.ucLabelEditAGrade.MaxLength = 40;
            this.ucLabelEditAGrade.Multiline = false;
            this.ucLabelEditAGrade.Name = "ucLabelEditAGrade";
            this.ucLabelEditAGrade.PasswordChar = '\0';
            this.ucLabelEditAGrade.ReadOnly = true;
            this.ucLabelEditAGrade.ShowCheckBox = false;
            this.ucLabelEditAGrade.Size = new System.Drawing.Size(170, 24);
            this.ucLabelEditAGrade.TabIndex = 22;
            this.ucLabelEditAGrade.TabNext = true;
            this.ucLabelEditAGrade.Value = "1";
            this.ucLabelEditAGrade.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabelEditAGrade.XAlign = 82;
            // 
            // ucButtonOQCSample
            // 
            this.ucButtonOQCSample.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonOQCSample.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonOQCSample.BackgroundImage")));
            this.ucButtonOQCSample.ButtonType = UserControl.ButtonTypes.None;
            this.ucButtonOQCSample.Caption = "抽检方案";
            this.ucButtonOQCSample.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonOQCSample.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ucButtonOQCSample.Location = new System.Drawing.Point(643, 39);
            this.ucButtonOQCSample.Name = "ucButtonOQCSample";
            this.ucButtonOQCSample.Size = new System.Drawing.Size(88, 22);
            this.ucButtonOQCSample.TabIndex = 21;
            this.ucButtonOQCSample.Click += new System.EventHandler(this.ucButton6_Click);
            // 
            // ucButtonOQC
            // 
            this.ucButtonOQC.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonOQC.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonOQC.BackgroundImage")));
            this.ucButtonOQC.ButtonType = UserControl.ButtonTypes.None;
            this.ucButtonOQC.Caption = "批确认";
            this.ucButtonOQC.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonOQC.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ucButtonOQC.Location = new System.Drawing.Point(455, 40);
            this.ucButtonOQC.Name = "ucButtonOQC";
            this.ucButtonOQC.Size = new System.Drawing.Size(88, 22);
            this.ucButtonOQC.TabIndex = 20;
            this.ucButtonOQC.Click += new System.EventHandler(this.ucButtonOQC_Click);
            // 
            // listBoxErrorInformation
            // 
            this.listBoxErrorInformation.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxErrorInformation.ColumnWidth = 180;
            this.listBoxErrorInformation.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listBoxErrorInformation.ItemHeight = 12;
            this.listBoxErrorInformation.Location = new System.Drawing.Point(224, 72);
            this.listBoxErrorInformation.MultiColumn = true;
            this.listBoxErrorInformation.Name = "listBoxErrorInformation";
            this.listBoxErrorInformation.Size = new System.Drawing.Size(536, 76);
            this.listBoxErrorInformation.TabIndex = 19;
            // 
            // ultraOptionSetPassReject
            // 
            this.ultraOptionSetPassReject.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ultraOptionSetPassReject.ItemAppearance = appearance1;
            valueListItem1.DisplayText = "通过";
            valueListItem2.DisplayText = "判退";
            this.ultraOptionSetPassReject.Items.Add(valueListItem1);
            this.ultraOptionSetPassReject.Items.Add(valueListItem2);
            this.ultraOptionSetPassReject.Location = new System.Drawing.Point(400, 12);
            this.ultraOptionSetPassReject.Name = "ultraOptionSetPassReject";
            this.ultraOptionSetPassReject.Size = new System.Drawing.Size(143, 24);
            this.ultraOptionSetPassReject.TabIndex = 18;
            this.ultraOptionSetPassReject.Text = "通过";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(329, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "判定结果";
            // 
            // ucLabOQCLot
            // 
            this.ucLabOQCLot.AllowEditOnlyChecked = true;
            this.ucLabOQCLot.Caption = "批号";
            this.ucLabOQCLot.Checked = false;
            this.ucLabOQCLot.EditType = UserControl.EditTypes.String;
            this.ucLabOQCLot.Location = new System.Drawing.Point(45, 16);
            this.ucLabOQCLot.MaxLength = 40;
            this.ucLabOQCLot.Multiline = false;
            this.ucLabOQCLot.Name = "ucLabOQCLot";
            this.ucLabOQCLot.PasswordChar = '\0';
            this.ucLabOQCLot.ReadOnly = false;
            this.ucLabOQCLot.ShowCheckBox = false;
            this.ucLabOQCLot.Size = new System.Drawing.Size(237, 24);
            this.ucLabOQCLot.TabIndex = 0;
            this.ucLabOQCLot.TabNext = true;
            this.ucLabOQCLot.Value = "";
            this.ucLabOQCLot.WidthType = UserControl.WidthTypes.Long;
            this.ucLabOQCLot.XAlign = 82;
            this.ucLabOQCLot.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ucLabEdit1_TxtboxKeyPress);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.ucButtonCancel);
            this.groupBox4.Controls.Add(this.txtMem);
            this.groupBox4.Controls.Add(this.ucLabRunningID);
            this.groupBox4.Controls.Add(this.ucButton4);
            this.groupBox4.Controls.Add(this.ucButtonOK);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox4.Location = new System.Drawing.Point(0, 549);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(768, 56);
            this.groupBox4.TabIndex = 155;
            this.groupBox4.TabStop = false;
            // 
            // ucButtonCancel
            // 
            this.ucButtonCancel.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonCancel.BackgroundImage")));
            this.ucButtonCancel.ButtonType = UserControl.ButtonTypes.None;
            this.ucButtonCancel.Caption = "取消";
            this.ucButtonCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonCancel.Location = new System.Drawing.Point(586, 16);
            this.ucButtonCancel.Name = "ucButtonCancel";
            this.ucButtonCancel.Size = new System.Drawing.Size(88, 22);
            this.ucButtonCancel.TabIndex = 13;
            this.ucButtonCancel.Click += new System.EventHandler(this.ucButtonCancel_Click);
            // 
            // txtMem
            // 
            this.txtMem.AllowEditOnlyChecked = true;
            this.txtMem.Caption = "备注";
            this.txtMem.Checked = false;
            this.txtMem.EditType = UserControl.EditTypes.String;
            this.txtMem.Location = new System.Drawing.Point(256, 16);
            this.txtMem.MaxLength = 80;
            this.txtMem.Multiline = false;
            this.txtMem.Name = "txtMem";
            this.txtMem.PasswordChar = '\0';
            this.txtMem.ReadOnly = false;
            this.txtMem.ShowCheckBox = false;
            this.txtMem.Size = new System.Drawing.Size(237, 24);
            this.txtMem.TabIndex = 12;
            this.txtMem.TabNext = true;
            this.txtMem.Value = "";
            this.txtMem.WidthType = UserControl.WidthTypes.Long;
            this.txtMem.XAlign = 293;
            // 
            // ucLabRunningID
            // 
            this.ucLabRunningID.AllowEditOnlyChecked = true;
            this.ucLabRunningID.Caption = "输入框";
            this.ucLabRunningID.Checked = false;
            this.ucLabRunningID.EditType = UserControl.EditTypes.String;
            this.ucLabRunningID.Location = new System.Drawing.Point(5, 16);
            this.ucLabRunningID.MaxLength = 40;
            this.ucLabRunningID.Multiline = false;
            this.ucLabRunningID.Name = "ucLabRunningID";
            this.ucLabRunningID.PasswordChar = '\0';
            this.ucLabRunningID.ReadOnly = false;
            this.ucLabRunningID.ShowCheckBox = false;
            this.ucLabRunningID.Size = new System.Drawing.Size(249, 24);
            this.ucLabRunningID.TabIndex = 11;
            this.ucLabRunningID.TabNext = false;
            this.ucLabRunningID.Value = "";
            this.ucLabRunningID.WidthType = UserControl.WidthTypes.Long;
            this.ucLabRunningID.XAlign = 54;
            this.ucLabRunningID.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ucLabRunningID_TxtboxKeyPress);
            // 
            // ucButton4
            // 
            this.ucButton4.BackColor = System.Drawing.SystemColors.Control;
            this.ucButton4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButton4.BackgroundImage")));
            this.ucButton4.ButtonType = UserControl.ButtonTypes.Exit;
            this.ucButton4.Caption = "退出";
            this.ucButton4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButton4.Location = new System.Drawing.Point(677, 16);
            this.ucButton4.Name = "ucButton4";
            this.ucButton4.Size = new System.Drawing.Size(88, 22);
            this.ucButton4.TabIndex = 9;
            // 
            // ucButtonOK
            // 
            this.ucButtonOK.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonOK.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonOK.BackgroundImage")));
            this.ucButtonOK.ButtonType = UserControl.ButtonTypes.None;
            this.ucButtonOK.Caption = "样本确认";
            this.ucButtonOK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonOK.Location = new System.Drawing.Point(495, 16);
            this.ucButtonOK.Name = "ucButtonOK";
            this.ucButtonOK.Size = new System.Drawing.Size(88, 22);
            this.ucButtonOK.TabIndex = 8;
            this.ucButtonOK.Click += new System.EventHandler(this.ucButtonOK_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.splitter1);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 162);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(768, 387);
            this.panel1.TabIndex = 157;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.splitter2);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Controls.Add(this.groupBox2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(275, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(493, 387);
            this.panel3.TabIndex = 3;
            // 
            // splitter2
            // 
            this.splitter2.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter2.Location = new System.Drawing.Point(0, 152);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(493, 3);
            this.splitter2.TabIndex = 6;
            this.splitter2.TabStop = false;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.pnlChildErrorCode);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 152);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(493, 235);
            this.panel4.TabIndex = 5;
            // 
            // pnlChildErrorCode
            // 
            this.pnlChildErrorCode.Controls.Add(this.errorCodeSelect);
            this.pnlChildErrorCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlChildErrorCode.Location = new System.Drawing.Point(0, 0);
            this.pnlChildErrorCode.Name = "pnlChildErrorCode";
            this.pnlChildErrorCode.Size = new System.Drawing.Size(493, 235);
            this.pnlChildErrorCode.TabIndex = 12;
            // 
            // errorCodeSelect
            // 
            this.errorCodeSelect.AddButtonTop = 50;
            this.errorCodeSelect.BackColor = System.Drawing.Color.Gainsboro;
            this.errorCodeSelect.CanInput = true;
            this.errorCodeSelect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.errorCodeSelect.Location = new System.Drawing.Point(0, 0);
            this.errorCodeSelect.Name = "errorCodeSelect";
            this.errorCodeSelect.RemoveButtonTop = 80;
            this.errorCodeSelect.SelectedErrorCodeGroup = null;
            this.errorCodeSelect.Size = new System.Drawing.Size(493, 235);
            this.errorCodeSelect.TabIndex = 1;
            this.errorCodeSelect.Resize += new System.EventHandler(this.errorCodeSelect_Resize);
            this.errorCodeSelect.SelectedIndexChanged += new System.EventHandler(this.errorCodeSelect_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ultraGridMain);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(493, 152);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "检验项目";
            // 
            // ultraGridMain
            // 
            this.ultraGridMain.Cursor = System.Windows.Forms.Cursors.Default;
            this.ultraGridMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGridMain.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ultraGridMain.Location = new System.Drawing.Point(3, 17);
            this.ultraGridMain.Name = "ultraGridMain";
            this.ultraGridMain.Size = new System.Drawing.Size(487, 132);
            this.ultraGridMain.TabIndex = 8;
            this.ultraGridMain.Text = "检验项目";
            this.ultraGridMain.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.ultraGridMain_InitializeLayout);
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(271, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(4, 387);
            this.splitter1.TabIndex = 2;
            this.splitter1.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.ultraGridCheckGroup);
            this.panel2.Controls.Add(this.leCheckedCount);
            this.panel2.Controls.Add(this.lbRunningIDList);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(271, 387);
            this.panel2.TabIndex = 0;
            // 
            // ultraGridCheckGroup
            // 
            this.ultraGridCheckGroup.Cursor = System.Windows.Forms.Cursors.Default;
            this.ultraGridCheckGroup.Dock = System.Windows.Forms.DockStyle.Top;
            this.ultraGridCheckGroup.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ultraGridCheckGroup.Location = new System.Drawing.Point(0, 0);
            this.ultraGridCheckGroup.Name = "ultraGridCheckGroup";
            this.ultraGridCheckGroup.Size = new System.Drawing.Size(271, 173);
            this.ultraGridCheckGroup.TabIndex = 184;
            this.ultraGridCheckGroup.Text = "检验类型";
            this.ultraGridCheckGroup.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.ultraGridCheckGroup_InitializeLayout);
            this.ultraGridCheckGroup.CellChange += new Infragistics.Win.UltraWinGrid.CellEventHandler(this.ultraGridCheckGroup_CellChange);
            // 
            // leCheckedCount
            // 
            this.leCheckedCount.AllowEditOnlyChecked = true;
            this.leCheckedCount.Caption = "已抽检产品";
            this.leCheckedCount.Checked = false;
            this.leCheckedCount.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.leCheckedCount.EditType = UserControl.EditTypes.String;
            this.leCheckedCount.Location = new System.Drawing.Point(0, 363);
            this.leCheckedCount.MaxLength = 40;
            this.leCheckedCount.Multiline = false;
            this.leCheckedCount.Name = "leCheckedCount";
            this.leCheckedCount.PasswordChar = '\0';
            this.leCheckedCount.ReadOnly = true;
            this.leCheckedCount.ShowCheckBox = false;
            this.leCheckedCount.Size = new System.Drawing.Size(271, 24);
            this.leCheckedCount.TabIndex = 159;
            this.leCheckedCount.TabNext = false;
            this.leCheckedCount.Value = "";
            this.leCheckedCount.WidthType = UserControl.WidthTypes.Small;
            this.leCheckedCount.XAlign = 171;
            // 
            // lbRunningIDList
            // 
            this.lbRunningIDList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbRunningIDList.ItemHeight = 12;
            this.lbRunningIDList.Location = new System.Drawing.Point(0, 194);
            this.lbRunningIDList.Name = "lbRunningIDList";
            this.lbRunningIDList.Size = new System.Drawing.Size(268, 100);
            this.lbRunningIDList.TabIndex = 158;
            this.lbRunningIDList.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lbRunningIDList_MouseUp);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 177);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "已检验序列号";
            // 
            // listBox2
            // 
            this.listBox2.ItemHeight = 12;
            this.listBox2.Location = new System.Drawing.Point(392, 16);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(160, 124);
            this.listBox2.TabIndex = 0;
            // 
            // listBox3
            // 
            this.listBox3.ItemHeight = 12;
            this.listBox3.Location = new System.Drawing.Point(32, 48);
            this.listBox3.Name = "listBox3";
            this.listBox3.Size = new System.Drawing.Size(200, 100);
            this.listBox3.TabIndex = 0;
            // 
            // FCollectionOQC
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(768, 605);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox1);
            this.Name = "FCollectionOQC";
            this.Text = "FQC 检验";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FCollectionOQC_Load);
            this.Closed += new System.EventHandler(this.FCollectionOQC_Closed);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraOptionSetPassReject)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.pnlChildErrorCode.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridMain)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridCheckGroup)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion
        //Laws Lu,2005/08/18,注释
        //		protected void ShowMessage(string message)
        //		{
        //			///lablastMsg.Text =message;
        //			MessageBox.Show(message);
        //		}
        private void btnExit_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void ucButton5_Click(object sender, System.EventArgs e)
        {
            FOQCDetail form = new FOQCDetail();
            form.ShowDialog();
        }


        public UCLabelEdit OQCLotTextBox
        {
            get
            {
                return this.ucLabOQCLot;
            }
        }


        #region Init Form
        private Messages InitOQCItemGrid(Messages msg, string itemCode)
        {
            Messages newMessages = new Messages();
            OQCFacade oqcFacade = new OQCFacade(this.DataProvider);
            //object[] objs = oqcFacade.QueryItem2OQCCheckListNoPager(itemCode, string.Empty);
            object[] objs = oqcFacade.GetAllOQCCheckList();
            if (objs == null)
            {
                newMessages.Add(new UserControl.Message(MessageType.Error, "$Error_OQCLOTCheckListNotExisted"));
            }
            else
            {
                // Added By Hi1/Venus.Feng on 20080720 for Hisense Version 
                this.ultraGridCheckGroup.UpdateData();
                if (this.ultraGridCheckGroup.Rows.Count == 0)
                {
                    newMessages.Add(new UserControl.Message(MessageType.Error, "$Error_NoCheckGroupConfig"));
                }
                else
                {
                    string checkGroupCodeList = "";
                    for (int i = 0; i < this.ultraGridCheckGroup.Rows.Count; i++)
                    {
                        if (Convert.ToBoolean(this.ultraGridCheckGroup.Rows[i].Cells["Checked"].Value) == true)
                        {
                            checkGroupCodeList += this.ultraGridCheckGroup.Rows[i].Cells["CheckGroup"].Value.ToString() + ",";
                        }
                    }
                    if (checkGroupCodeList.Length > 0)
                    {
                        checkGroupCodeList = checkGroupCodeList.Substring(0, checkGroupCodeList.Length - 1);
                        checkGroupCodeList = checkGroupCodeList.Replace(",", "','");
                    }
                    else
                    {
                        return newMessages;
                    }
                    object[] checkList = oqcFacade.GetOQCCheckListByCheckGroup(checkGroupCodeList);
                    if (checkList == null || checkList.Length == 0)
                    {
                        newMessages.Add(new UserControl.Message(MessageType.Error, "$Error_OQCLOTCheckListNotExisted"));
                    }
                    else
                    {
                        object tmpItem2CheckList;
                        List<OQCCheckList> finalCheckList = new List<OQCCheckList>();

                        for (int i = objs.Length - 1; i >= 0; i--)
                        {
                            tmpItem2CheckList = objs[i];
                            foreach (OQCCheckList cl in checkList)
                            {
                                if (string.Compare(cl.CheckItemCode, ((OQCCheckList)tmpItem2CheckList).CheckItemCode, true) == 0)
                                {
                                    finalCheckList.Add(tmpItem2CheckList as OQCCheckList);
                                    break;
                                }
                            }
                        }
                        FillUltraWinGrid(this.ultraGridMain, finalCheckList);
                    }
                }
                // End Added                
            }

            return newMessages;
        }

        private Messages InitDefectStatis(Messages msg, string oqcLotNo)
        {
            Messages newMessages = new Messages();
            OQCFacade oqcFacade = new OQCFacade(this.DataProvider);
            object obj = oqcFacade.GetOQCLOTCheckList(FormatHelper.PKCapitalFormat(oqcLotNo), OQCFacade.Lot_Sequence_Default);
            if (obj != null)
            {
                this.ucLabelEditBGrade.Value = ((OQCLOTCheckList)obj).BGradeTimes.ToString();
                this.ucLabelEditCGrade.Value = ((OQCLOTCheckList)obj).CGradeTimes.ToString();
                this.ucLabelEditAGrade.Value = ((OQCLOTCheckList)obj).AGradeTimes.ToString();
                this.ucLabelEditZGrade.Value = ((OQCLOTCheckList)obj).ZGradeTimes.ToString();
            }
            return newMessages;
        }

        private Messages InitErrorCodeInformation(Messages msg, string itemCode)
        {
            Messages newMessaage = new Messages();
            newMessaage.AddMessages(SetErrorCodeList(itemCode));
            return newMessaage;
        }

        private Messages InitErrorCodeStaticInformation(Messages msg, string lotNo)
        {
            Messages newMessaage = new Messages();
            OQCFacade oqcFacade = new OQCFacade(this.DataProvider);
            object[] objs = oqcFacade.QueryOQCLot2ErrorCode(string.Empty, string.Empty, lotNo, OQCFacade.Lot_Sequence_Default, int.MinValue, int.MaxValue);
            if (objs != null)
            {
                listBoxErrorInformation.Items.Clear();
                for (int i = 0; i < objs.Length; i++)
                {
                    //Laws Lu,2005/08/14,修改	在不良信息后加TAB，以显示完整的信息
                    //listBoxErrorInformation.Items.Add( ((OQCLot2ErrorCode)objs[i]).ErrorCodeGroup+":"+((OQCLot2ErrorCode)objs[i]).ErrorCode+"("+ ((OQCLot2ErrorCode)objs[i]).Times.ToString()+")");
                    listBoxErrorInformation.Items.Add(((OQCLot2ErrorCode)objs[i]).ErrorCodeGroup + ":" + ((OQCLot2ErrorCode)objs[i]).ErrorCode + "(" + ((OQCLot2ErrorCode)objs[i]).Times.ToString() + ")\t");
                }
            }
            return newMessaage;
        }

        private Messages InitCheckIDs(Messages msg, string moCode, string oqcLotNo)
        {
            Messages newMessaage = new Messages();
            OQCFacade oqcFacade = new OQCFacade(this.DataProvider);
            //Laws Lu,2005/08/22,修改	允许显示不同工单的产品序列号
            object[] objs = oqcFacade.ExtraQueryOQCLot2CardCheck(string.Empty, string.Empty, string.Empty, oqcLotNo, OQCFacade.Lot_Sequence_Default.ToString());
            //			object[] objs = oqcFacade.QueryOQCLot2CardCheckLastRecord(string.Empty,string.Empty,string.Empty,oqcLotNo,OQCFacade.Lot_Sequence_Default.ToString(),string.Empty);
            if (objs != null)
            {
                lbRunningIDList.Items.Clear();

                this.CheckedCard.Clear();
                
                for (int i = 0; i < objs.Length; i++)
                {
                    //Laws Lu,2005/08/16,将样本检验对象直接写入lbRunningIDList
                    OQCLot2CardCheck card = (OQCLot2CardCheck)objs[i];
                    lbRunningIDList.Items.Add(card);

                    //Karron Qiu,2005-10-13,将runningID存入已抽检产品数量中
                    if (!this.CheckedCard.Contains(card.RunningCard))
                    {
                        this.CheckedCard.Add(card.RunningCard);
                    }
                    this.leCheckedCount.Value = CheckedCard.Count.ToString();
                }
            }
            return newMessaage;
        }

        #endregion


        #region private method
        private Messages SetErrorCodeList(string itemCode)
        {
            TSModelFacade tsFacade = new TSModelFacade(this.DataProvider);
            Messages msg = new Messages();
            try
            {
                object[] errorCodeGroups = tsFacade.GetErrorCodeGroupByItemCode(itemCode);
                if (errorCodeGroups != null)
                {
                    errorCodeSelect.ClearErrorGroup();
                    errorCodeSelect.ClearSelectedErrorCode();
                    errorCodeSelect.ClearSelectErrorCode();
                    errorCodeSelect.AddErrorGroups(errorCodeGroups);
                }
            }
            catch (Exception ex)
            {
                msg.Add(new UserControl.Message(ex));
            }
            return msg;
        }

        private void ClearDefectStatis()
        {
            this.ucLabelEditBGrade.Value = string.Empty;
            this.ucLabelEditCGrade.Value = string.Empty;
            this.ucLabelEditAGrade.Value = string.Empty;
            this.ucLabelEditZGrade.Value = string.Empty;
        }
        private void ClearErrorCodeInformation()
        {
            this.listBoxErrorInformation.Items.Clear();
        }
        private void ClearIDList()
        {
            this.lbRunningIDList.Items.Clear();
            this.CheckedCard.Clear();
            this.leCheckedCount.Value = CheckedCard.Count.ToString();
        }
        private void ClearCheckItems()
        {
            this.dtCheckItem.Rows.Clear();
        }
        private void ClearCheckGroups()
        {
            this.dtCheckGroup.Rows.Clear();
        }
        private void ClearErrorCodeControl()
        {
            this.errorCodeSelect.ClearErrorGroup();
            this.errorCodeSelect.ClearSelectedErrorCode();
            this.errorCodeSelect.ClearSelectErrorCode();
        }
        private ValueList GetOQCCheckItems()
        {
            valueList = new ValueList();
            valueList.ValueListItems.Add("", "");
            valueList.ValueListItems.Add(OQCFacade.OQC_ZGrade, "Z");
            valueList.ValueListItems.Add(OQCFacade.OQC_AGrade, "A");
            valueList.ValueListItems.Add(OQCFacade.OQC_BGrade, "B");
            valueList.ValueListItems.Add(OQCFacade.OQC_CGrade, "C");            

            return valueList;
        }

        private string GetOQCCheckedItemText(string strValue)
        {
            string strReturn = String.Empty;
            switch (strValue)
            {
                case "AGRADE":
                    {
                        strReturn = OQCFacade.OQC_AGrade;
                        break;
                    }
                case "BGRADE":
                    {
                        strReturn = OQCFacade.OQC_BGrade;
                        break;
                    }
                case "CGRADE":
                    {
                        strReturn = OQCFacade.OQC_CGrade;
                        break;
                    }
                case "ZGRADE":
                    {
                        strReturn = OQCFacade.OQC_ZGrade;
                        break;
                    }
            }

            return strReturn;
        }

        private ValueList GetResultItems()
        {
            valuResult = new ValueList();
            valuResult.ValueListItems.Add(true, "Good");
            valuResult.ValueListItems.Add(false, "NG");

            return valuResult;
        }

        private void InitializeUltraGrid()
        {
            dtCheckItem.Columns.Clear();
            dtCheckItem.Columns.Add("CheckItem", typeof(string)).ReadOnly = true;
            dtCheckItem.Columns.Add("AGrade", typeof(string));
            dtCheckItem.Columns.Add("Result", typeof(bool));
            dtCheckItem.Columns.Add("Memo", typeof(string));

            this.ultraGridMain.DataSource = dtCheckItem;
        }

        #endregion


        #region form events
        private void ucButton6_Click(object sender, System.EventArgs e)
        {
            FOQCSamplePlan form = new FOQCSamplePlan();
            form.oqcLotNo = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.ucLabOQCLot.Value));
            form.ShowDialog();
        }

        public void ucLabEdit1_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                LabOQCLotKeyPress();
            }
            //e.Handled=true;
        }

        public void LabOQCLotKeyPress()
        {
            Messages msg = RequesData();
            ApplicationRun.GetInfoForm().Add(msg);

            if (msg.IsSuccess())
            {
                ucLabRunningID.TextFocus(false, true);
                //SendKeys.Send("+{TAB}");
                //Remove UCLabel.SelectAll;
            }
            else
            {
                ucLabOQCLot.TextFocus(false, true);
                //SendKeys.Send("+{TAB}");
                //Remove UCLabel.SelectAll;
            }
        }

        public void LabOQCLotKeyPressForPlan()
        {
            Messages msg = RequesData();
            ApplicationRun.GetInfoForm().Add(msg);

            if (msg.IsSuccess())
            {
                ucLabRunningID.TextFocus(false, true);
                //Remove UCLabel.SelectAll;
                //SendKeys.Send("+{TAB}");
            }
            else
            {
                ucLabOQCLot.TextFocus(false, true);
                //Remove UCLabel.SelectAll;
                //SendKeys.Send("+{TAB}");
            }
        }


        private void ultraGridMain_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            ultraWinGridHelper = new UltraWinGridHelper(this.ultraGridMain);

            ultraWinGridHelper.AddCommonColumn("CheckItem", "检验项目");
            ultraWinGridHelper.AddDrpDownListColumn("AGrade", "缺陷等级", GetOQCCheckItems());
            ultraWinGridHelper.AddRadioButtonColumn("Result", "结果", GetResultItems());
            ultraWinGridHelper.AddCommonColumn("Memo", "备注");

        }

        private void FCollectionOQC_Load(object sender, System.EventArgs e)
        {
            InitultraOptionSetPassReject();
            InitializeUltraGrid();
            ClearDefectStatis();
            ClearErrorCodeInformation();
            ClearIDList();
            ClearCheckGroups();
            ClearCheckItems();
            ClearErrorCodeControl();
            formStats = FormStatus.Noready;
            InitializeCheckGroupGrid();
            //			//Added By Karron Qiu,2006-6-19,处理从FOQCSamplePlan传递过来的批号
            //			if(this.ucLabOQCLot.Value != "")
            //			{
            //				LabOQCLotKeyPress();
            //			}
            //			//End
        }

        //		private void RefreshCheckedCount()
        //		{
        //
        //		}

        private ArrayList CheckedCard = new ArrayList();

        private void InitultraOptionSetPassReject()
        {
            this.ultraOptionSetPassReject.Items.Clear();

            this.ultraOptionSetPassReject.CheckedItem = this.ultraOptionSetPassReject.Items.Add(OQCLotStatus.OQCLotStatus_Pass, "通过");
            this.ultraOptionSetPassReject.Items.Add(OQCLotStatus.OQCLotStatus_Reject, "判退");
        }

        public void ucLabRunningID_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                Messages msg = new Messages();
                string runningID = FormatHelper.CleanString(this.ucLabRunningID.Value);
                if (runningID.Length == 0)
                {
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_Please_Input_RunningCard"));

                    ucLabRunningID.TextFocus(false, true);
                    //Remove UCLabel.SelectAll;
                    return;
                }

                string oqcLotNo = FormatHelper.CleanString(this.ucLabOQCLot.Value);
                if (oqcLotNo.Length == 0)
                {
                    msg.Add(new UserControl.Message(new Exception("$CS_FQCLOT_NOT_NULL")));
                    ApplicationRun.GetInfoForm().Add(msg);

                    ucLabOQCLot.TextFocus(false, true);
                    //Remove UCLabel.SelectAll;
                    return;
                }
                OQCFacade oqcFacade = new OQCFacade(this.DataProvider);

                //Laws Lu,2006/12/25 只需要Open/Close一次
                ((SQLDomainDataProvider)DataProvider).PersistBroker.AutoCloseConnection = false;
                ((SQLDomainDataProvider)DataProvider).PersistBroker.OpenConnection();
                //判断批是否存在
                object obj = oqcFacade.GetOQCLot(FormatHelper.PKCapitalFormat(oqcLotNo), OQCFacade.Lot_Sequence_Default);
                if (obj == null)
                {
                    msg.Add(new UserControl.Message(MessageType.Error, "$CS_LOT_NOT_EXIST"));
                    ApplicationRun.GetInfoForm().Add(msg);

                    ucLabOQCLot.TextFocus(false, true);
                    //Remove UCLabel.SelectAll;
                    return;
                }
                //判断该批是否已经结束
                if ((((OQCLot)obj).LOTStatus == OQCLotStatus.OQCLotStatus_Pass) || (((OQCLot)obj).LOTStatus == OQCLotStatus.OQCLotStatus_Reject))
                {
                    msg.Add(new UserControl.Message(MessageType.Error, "$CS_FQC_LOT_USED"));
                    ApplicationRun.GetInfoForm().Add(msg);

                    ucLabOQCLot.TextFocus(false, true);
                    //Remove UCLabel.SelectAll;
                    return;
                }

                //判断ID是否有生产信息
                ActionOnLineHelper actinOnlineHelper = new ActionOnLineHelper(this.DataProvider);
                ProductInfo productInfo = (ProductInfo)actinOnlineHelper.GetIDInfo(FormatHelper.PKCapitalFormat(runningID)).GetData().Values[0];
                if (productInfo.LastSimulation == null)
                {
                    msg.Add(new UserControl.Message(MessageType.Error, "$NoSimulationInfo $CS_Param_ID =" + ucLabRunningID.Value));
                    ApplicationRun.GetInfoForm().Add(msg);

                    ucLabRunningID.Value = "";
                    ucLabRunningID.TextFocus(false, true);
                    //Remove UCLabel.SelectAll;
                    return;
                }
                //判断ID是否在批中
                obj = oqcFacade.GetOQCLot2Card(FormatHelper.PKCapitalFormat(runningID), productInfo.LastSimulation.MOCode, FormatHelper.PKCapitalFormat(oqcLotNo), OQCFacade.Lot_Sequence_Default);
                if (obj == null)
                {
                    msg.Add(new UserControl.Message(MessageType.Error, "$CS_SN_NOT_EXIST_LOT $CS_Param_ID =" + ucLabRunningID.Value));
                    ApplicationRun.GetInfoForm().Add(msg);

                    ucLabRunningID.Value = "";
                    ucLabRunningID.TextFocus(false, true);
                    //Remove UCLabel.SelectAll;
                    return;
                }
                //add ID in IDlist
                //Laws Lu,2005/08/30,修改	只允许正常的采集
                /*Joanne	AM0190	FQC的做法采取和良品/不良品采集相同的方式，即良品可以采集为不良
                 * ，不良不可以采集为良品，连续采集只保存最后一笔记录
                 * （所谓连续采集是产品没有离开该工位所做的采集）*/
                bool bIsExist = false;
                for (int i = 0; i < lbRunningIDList.Items.Count; i++)
                {
                    if ((lbRunningIDList.Items[i]) is OQCLot2CardCheck)
                    {
                        if (((OQCLot2CardCheck)lbRunningIDList.Items[i]).RunningCard == FormatHelper.PKCapitalFormat(runningID)
                            && productInfo.LastSimulation.LastAction != ActionType.DataCollectAction_OQCNG
                            && productInfo.LastSimulation.LastAction != ActionType.DataCollectAction_NG
                            && productInfo.LastSimulation.LastAction != ActionType.DataCollectAction_NG)
                        {
                            bIsExist = true;

                            lbRunningIDList.SelectedItem = lbRunningIDList.Items[i];
                            this.lbRunningIDList_MouseUp(sender, null);
                            break;
                        }
                    }
                    else
                    {
                        if (lbRunningIDList.Items[i].ToString().ToUpper().Trim() == FormatHelper.PKCapitalFormat(runningID)
                            && productInfo.LastSimulation.LastAction != ActionType.DataCollectAction_OQCNG
                            && productInfo.LastSimulation.LastAction != ActionType.DataCollectAction_NG
                            && productInfo.LastSimulation.LastAction != ActionType.DataCollectAction_NG)
                        {
                            bIsExist = true;

                            lbRunningIDList.SelectedItem = lbRunningIDList.Items[i];
                            this.lbRunningIDList_MouseUp(sender, null);
                            break;
                        }
                    }
                }
                if (!bIsExist
                    || productInfo.LastSimulation.LastAction == ActionType.DataCollectAction_OQCNG
                    || productInfo.LastSimulation.LastAction == ActionType.DataCollectAction_NG
                    || productInfo.LastSimulation.LastAction == ActionType.DataCollectAction_OutLineNG)
                {
                    lbRunningIDList.Items.Insert(0, _checkMNIDManager.AddMNID(new CheckMNID(FormatHelper.PKCapitalFormat(runningID), productInfo.LastSimulation.RunningCardSequence.ToString())));

                    //Karron Qiu,2005-10-13,将runningID存入已抽检产品数量中
                    if (!this.CheckedCard.Contains(runningID))
                    {
                        this.CheckedCard.Add(runningID);
                    }
                    this.leCheckedCount.Value = CheckedCard.Count.ToString();


                }

                //Laws Lu,2005/08/14,新增	在输入新的产品序列号后重新设置检验项目内容和已经选择的不良代码
                //Laws Lu,2006/05/25,修改	

                //Laws Lu,2006/06/02	新增		更新检验项目和不良代码
                //Added By Hi1/Venus.Feng on 20080720 for Hisense Version
                // Load CheckGroup List
                msg.AddMessages(InitCheckGroupData(productInfo.LastSimulation.ItemCode));

                msg.AddMessages(InitOQCItemGrid(null, productInfo.LastSimulation.ItemCode));
                msg.AddMessages(InitErrorCodeInformation(null, productInfo.LastSimulation.ItemCode));
                //Laws Lu,2006/12/25 只需要Open/Close一次
                ((SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
                ((SQLDomainDataProvider)DataProvider).PersistBroker.AutoCloseConnection = true;

                errorCodeSelect.ClearSelectedErrorCode();
                this.lbRunningIDList.ClearSelected();

                if (!msg.IsSuccess())
                {
                    ucLabRunningID.Value = "";
                    ucLabRunningID.TextFocus(false, true);
                    //Remove UCLabel.SelectAll;
                }
                else
                {
                    //控制不让再输入ID
                    this.ucLabOQCLot.Enabled = false;
                    this.ucLabRunningID.Enabled = false;
                    formStats = FormStatus.Ready;
                }
                ApplicationRun.GetInfoForm().Add(msg);
            }
            //			e.Handled=true;
        }

        private void errorCodeSelect_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            TSModelFacade tsFacade = new TSModelFacade(this._domainDataProvider);
            object[] errorCodes = tsFacade.GetSelectedErrorCodeByErrorCodeGroupCode(errorCodeSelect.SelectedErrorCodeGroup);
            if (errorCodes != null)
            {
                errorCodeSelect.ClearSelectErrorCode();
                errorCodeSelect.AddErrorCodes(errorCodes);
            }
        }


        public void ucButtonOK_Click(object sender, System.EventArgs e)
        {
            //数据检查
            if (ValidData())
            {
                Messages msg = new Messages();

                if (formStats == FormStatus.Ready)
                {

                    //					try
                    //					{
                    //Laws Lu,2005/11/01,新增	改善性能
                    if (Resource == null)
                    {
                        BenQGuru.eMES.BaseSetting.BaseModelFacade dataModel = new BenQGuru.eMES.BaseSetting.BaseModelFacade(this.DataProvider);
                        Resource = (Domain.BaseSetting.Resource)dataModel.GetResource(ApplicationService.Current().ResourceCode);
                    }

                    ActionCheckStatus actionCheckStatus = new ActionCheckStatus();


                    string oqcLotNo = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(ucLabOQCLot.Value));
                    string runningCarDID = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.ucLabRunningID.Value));
                    ActionOnLineHelper actionOnLineHelper = new ActionOnLineHelper(this.DataProvider);
                    ActionFactory actionFactory = new ActionFactory(this.DataProvider);
                    OQCFacade oqcFacade = new OQCFacade(DataProvider);

                    #region ProductionInfo
                    ProductInfo product = null;
                    msg = actionOnLineHelper.GetIDInfo(runningCarDID);
                    if (msg.IsSuccess())
                    {
                        product = (ProductInfo)msg.GetData().Values[0];
                        if (product.LastSimulation == null)
                        {
                            msg.Add(new UserControl.Message(new Exception("$Error_LastSimulation_IsNull")));
                            ApplicationRun.GetInfoForm().Add(msg);

                            this.ucLabRunningID.TextFocus(false, true);
                            return;
                        }

                    }
                    #endregion

                    if (listActionCheckStatus.ContainsKey(product.LastSimulation.MOCode))
                    {
                        actionCheckStatus = (ActionCheckStatus)listActionCheckStatus[product.LastSimulation.MOCode];

                        actionCheckStatus.ProductInfo = product;

                        actionCheckStatus.ProductInfo.Resource = Resource;

                        //lastSimulation = actionCheckStatus.ProductInfo.LastSimulation;
                        actionCheckStatus.ActionList = new ArrayList();
                    }
                    else
                    {
                        //actionCheckStatus.NeedUpdateSimulation = false;
                        //actionCheckStatus.NeedFillReport = true;
                        listActionCheckStatus.Add(product.LastSimulation.MOCode, actionCheckStatus);
                    }

                    //Laws Lu,2005/08/14,新增	判断产品序列号是否已经存在于ID列表中
                    //
                    //						object objOQCLot2Card = oqcFacade.GetOQCLot2Card(runningCarDID
                    //							,product.LastSimulation.MOCode
                    //							,oqcLotNo
                    //							,OQCFacade.Lot_Sequence_Default);
                    //
                    //						if(objOQCLot2Card != null)
                    //						{
                    //							msg.Add(new UserControl.Message( new Exception("$CS_SPECIMEN_ALREADY_CHECK"))); 
                    //							ApplicationRun.GetInfoForm().Add(msg);
                    //
                    //							return;
                    //						}
                    //End Laws Lu

                    //判断是OQCGood/OQCNG
                    object[] objsErrorCodes = errorCodeSelect.GetSelectedErrorCodes();

                    // Added by Hi1/venus.feng on 20080720 for Hisense Version : Add Check Group
                    ArrayList alCheckGroups = new ArrayList();
                    if (this.ultraGridCheckGroup.Rows.Count > 0)
                    {
                        for (int i = 0; i < this.ultraGridCheckGroup.Rows.Count; i++)
                        {
                            // Selected items
                            if (Convert.ToBoolean(this.ultraGridCheckGroup.Rows[i].Cells["Checked"].Value) == true)
                            {
                                OQCLot2CheckGroup oqcLot2CheckGroup = oqcFacade.CreateNewOQCLot2CheckGroup();
                                oqcLot2CheckGroup.LOTNO = oqcLotNo;
                                oqcLot2CheckGroup.LotSequence = OQCFacade.Lot_Sequence_Default;
                                oqcLot2CheckGroup.CheckedCount = int.Parse(this.ultraGridCheckGroup.Rows[i].Cells["TestedQty"].Value.ToString()) + 1;
                                oqcLot2CheckGroup.CheckGroup = this.ultraGridCheckGroup.Rows[i].Cells["CheckGroup"].Value.ToString();
                                oqcLot2CheckGroup.EAttribute1 = "";
                                oqcLot2CheckGroup.MaintainUser = ApplicationService.Current().UserCode;
                                alCheckGroups.Add(oqcLot2CheckGroup);
                            }
                        }
                    }
                    // End Added

                    #region OQCLOTCardCheckList
                    // 只有NG才会保存OQCLotCardCheckList ?????
                    ArrayList alCheckItems = new ArrayList();
                    this.ultraGridMain.UpdateData();
                    if (ultraGridMain.Rows.Count > 0)
                    {
                        for (int i = 0; i < this.ultraGridMain.Rows.Count; i++)
                        {
                            OQCLOTCardCheckList oqcLotCardCheckList = oqcFacade.CreateNewOQCLOTCardCheckList();
                            oqcLotCardCheckList.CheckItemCode = this.ultraGridMain.Rows[i].Cells[0].Value.ToString();
                            oqcLotCardCheckList.Grade = this.ultraGridMain.Rows[i].Cells[1].Value.ToString();
                            oqcLotCardCheckList.ItemCode = product.LastSimulation.ItemCode;
                            oqcLotCardCheckList.LOTNO = oqcLotNo;
                            oqcLotCardCheckList.LotSequence = OQCFacade.Lot_Sequence_Default;
                            oqcLotCardCheckList.MaintainUser = ApplicationService.Current().UserCode;
                            oqcLotCardCheckList.MEMO = FormatHelper.CleanString(this.ultraGridMain.Rows[i].Cells[3].Text);
                            oqcLotCardCheckList.Result = this.ultraGridMain.Rows[i].Cells[2].Text;
                            oqcLotCardCheckList.RunningCard = runningCarDID;
                            oqcLotCardCheckList.RunningCardSequence = product.LastSimulation.RunningCardSequence;
                            oqcLotCardCheckList.MOCode = product.LastSimulation.MOCode;
                            oqcLotCardCheckList.ModelCode = product.LastSimulation.ModelCode;
                            oqcLotCardCheckList.ItemCode = product.LastSimulation.ItemCode;

                            if ((oqcLotCardCheckList.Result.ToUpper() == "NG") || (oqcLotCardCheckList.Result.ToUpper() == "FALSE"))
                            {
                                if (oqcLotCardCheckList.Grade == string.Empty)
                                {
                                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$Error_MustChooseGrade"));
                                    this.ultraGridMain.Rows[i].Selected = true;
                                    return;
                                }
                                oqcLotCardCheckList.Result = "NG";
                                alCheckItems.Add(oqcLotCardCheckList);
                            }

                        }
                    }
                    else
                    {
                        ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_PLEASE_MAINTEN_CHECKLIST"));
                        ucLabRunningID.TextFocus(false, true);
                        return;
                    }
                    #endregion

                    #region 有检验项目GD,则不能选择Error Code
                    if ((alCheckItems.Count == 0) && (objsErrorCodes.Length > 0))
                    {
                        msg.Add(new UserControl.Message(MessageType.Error, "$CS_NOT_EXIST_NG_CHECKLIST"));
                        ApplicationRun.GetInfoForm().Add(msg);

                        this.ucLabRunningID.TextFocus(false, true);
                        return;
                    }

                    #endregion

                    #region 有检验项目为NG,则必须选择Error Code
                    if ((alCheckItems.Count > 0) && (objsErrorCodes.Length == 0))
                    {
                        msg.Add(new UserControl.Message(MessageType.Error, "$CS_Please_Select_ErrorCode"));
                        ApplicationRun.GetInfoForm().Add(msg);

                        this.ucLabRunningID.TextFocus(false, true);
                        return;
                    }

                    #endregion

                    ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.OpenConnection();
                    DataProvider.BeginTransaction();
                    try
                    {

                        //没有NG,Good 采集
                        if ((objsErrorCodes == null) || (objsErrorCodes.Length == 0))
                        {
                            #region GooD DataCollect

                            IAction actionOQCGood = actionFactory.CreateAction(ActionType.DataCollectAction_OQCGood);

                            OQCGoodEventArgs actionEventArgs = new OQCGoodEventArgs(ActionType.DataCollectAction_OQCGood, runningCarDID, ApplicationService.Current().
                                UserCode, ApplicationService.Current().ResourceCode, oqcLotNo, alCheckItems.ToArray(),alCheckGroups.ToArray(), product);

                            actionEventArgs.IsDataLink = BenQGuru.eMES.Web.Helper.FormatHelper.FALSE_STRING; //增加标识是不是来自数据连线
                            actionEventArgs.Memo = txtMem.Value;//Laws Lu,2006/07/12 add memo field

                            msg.AddMessages(((IActionWithStatus)actionOQCGood).Execute(actionEventArgs, actionCheckStatus));

                            #endregion
                        }
                        else
                        {
                            #region NG DataCollect

                            IAction actionOQCNG = actionFactory.CreateAction(ActionType.DataCollectAction_OQCNG);
                            OQCNGEventArgs actionEventArgs = new OQCNGEventArgs(ActionType.DataCollectAction_OQCNG, runningCarDID, ApplicationService.Current().
                                UserCode, ApplicationService.Current().ResourceCode, oqcLotNo, alCheckItems.ToArray(), alCheckGroups.ToArray(), objsErrorCodes, product);

                            actionEventArgs.IsDataLink = BenQGuru.eMES.Web.Helper.FormatHelper.FALSE_STRING; //增加标识是不是来自数据连线
                            actionEventArgs.Memo = txtMem.Value;//Laws Lu,2006/07/12 add memo field

                            msg.AddMessages(((IActionWithStatus)actionOQCNG).Execute(actionEventArgs, actionCheckStatus));

                            #endregion
                        }
                    }
                    catch (Exception ex)
                    {
                        DataProvider.RollbackTransaction();
                        msg.Add(new UserControl.Message(ex));
                    }
                    finally
                    {
                        if (!msg.IsSuccess())
                        {
                            DataProvider.RollbackTransaction();
                        }
                        else
                        {
                            DataProvider.CommitTransaction();
                        }
                        //Laws Lu,2005/10/19,新增	缓解性能问题
                        ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
                    }
                }
                //					catch(Exception ex)
                //					{
                //						msg.Add(new UserControl.Message(ex));
                //					}


                #region 状态控制
                //控制不让再输入ID
                this.ucLabOQCLot.Enabled = true;
                this.ucLabRunningID.Enabled = true;
                this.ucLabRunningID.Value = string.Empty;
                this.ucLabRunningID.TextFocus(false, true);
                formStats = FormStatus.Noready;
                #endregion

                ApplicationRun.GetInfoForm().Add(msg);

                this.RequesData();

                ucLabRunningID.TextFocus(false, true);
            }
            //			else
            //			{
            //			}
        }

        #endregion

        #region object<->UI
        private bool ValidData()
        {
            if (FormatHelper.CleanString(this.ucLabRunningID.Value.Trim()).Length == 0)
            {
                ApplicationRun.GetInfoForm().Add("$CS_Please_Input_RunningCard");

                ucLabRunningID.TextFocus(false, true);
                //Remove UCLabel.SelectAll;
                return false;
            }
            bool checkedAnyItem = false;
            for (int i = 0; i < this.ultraGridCheckGroup.Rows.Count; i++)
            {
                if (Convert.ToBoolean(this.ultraGridCheckGroup.Rows[i].Cells["Checked"].Value) == true)
                {
                    checkedAnyItem = true;
                    break;
                }
            }
            if (!checkedAnyItem)
            {
                ApplicationRun.GetInfoForm().Add("$CS_PleaseChooseOneCheckGroup");
                return false;
            }
            return true;
        }

        private void FillUltraWinGrid(Infragistics.Win.UltraWinGrid.UltraGrid ultraGrid, IList<OQCCheckList> objs)
        {
            dtCheckItem.Rows.Clear();
            for (int i = 0; i < objs.Count; i++)
            {
                dtCheckItem.Rows.Add(new object[] { objs[i].CheckItemCode, "", true, "" });
            }
            dtCheckItem.AcceptChanges();
        }

        private Messages RequesData()
        {
            Messages msg = new Messages();

            ClearCheckGroups();
            ClearCheckItems();
            ClearDefectStatis();
            ClearErrorCodeControl();
            ClearErrorCodeInformation();
            ClearIDList();

            string oqcLotNo = FormatHelper.CleanString(this.ucLabOQCLot.Value);
            if (oqcLotNo.Length == 0)
            {
                msg.Add(new UserControl.Message(MessageType.Error, "$CS_FQCLOT_NOT_NULL"));

                ucLabOQCLot.TextFocus(false, true);
                //Remove UCLabel.SelectAll;
                return msg;
            }
            //Laws Lu,2006/12/25 不允许自动关闭连接
            ((SQLDomainDataProvider)DataProvider).PersistBroker.AutoCloseConnection = false;
            ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.OpenConnection();
            try
            {
                OQCFacade oqcFacade = new OQCFacade(DataProvider);
                ActionOnLineHelper actionOnLineHelper = new ActionOnLineHelper(this.DataProvider);
                object obj = oqcFacade.GetOQCLot(FormatHelper.PKCapitalFormat(oqcLotNo), OQCFacade.Lot_Sequence_Default);
                if (obj == null)
                {
                    msg.Add(new UserControl.Message(MessageType.Error, "$CS_LOT_NOT_EXIST"));

                    ucLabOQCLot.TextFocus(false, true);
                    //Remove UCLabel.SelectAll;
                    return msg;
                }
                object[] objs = oqcFacade.QueryOQCLot2Card(string.Empty, string.Empty, FormatHelper.PKCapitalFormat(oqcLotNo), OQCFacade.Lot_Sequence_Default, int.MinValue, int.MaxValue);
                if (objs == null)
                {
                    msg.Add(new UserControl.Message(MessageType.Error, "$CS_LOT_NOT_EXIST"));

                    ucLabOQCLot.TextFocus(false, true);
                    //Remove UCLabel.SelectAll;
                    return msg;
                }
                ProductInfo productionInfo = (ProductInfo)actionOnLineHelper.GetIDInfo(((OQCLot2Card)objs[0]).RunningCard).GetData().Values[0];
                if (productionInfo.LastSimulation == null)
                {
                    msg.Add(new UserControl.Message(MessageType.Error, "$NoSimulationInfo"));

                    ucLabRunningID.TextFocus(false, true);
                    //Remove UCLabel.SelectAll;
                    return msg;
                }
                //Laws Lu,2005/08/16,新增	查询Lot时,设置状态
                if (((OQCLot)obj).LOTStatus == BenQGuru.eMES.Web.Helper.OQCLotStatus.OQCLotStatus_Reject)
                {
                    ultraOptionSetPassReject.Value = BenQGuru.eMES.Web.Helper.OQCLotStatus.OQCLotStatus_Reject;
                }
                if (((OQCLot)obj).LOTStatus == BenQGuru.eMES.Web.Helper.OQCLotStatus.OQCLotStatus_Pass)
                {
                    ultraOptionSetPassReject.Value = BenQGuru.eMES.Web.Helper.OQCLotStatus.OQCLotStatus_Pass;
                }

                msg.AddMessages(InitDefectStatis(null, oqcLotNo));
                //msg.AddMessages(InitErrorCodeInformation(null,productionInfo.LastSimulation.ItemCode));
                msg.AddMessages(InitErrorCodeStaticInformation(null, oqcLotNo));
                msg.AddMessages(InitCheckIDs(null, productionInfo.LastSimulation.MOCode, oqcLotNo));
                //msg.AddMessages(InitOQCItemGrid(null,productionInfo.LastSimulation.ItemCode));
            }
            catch (Exception ex)
            {
                msg.Add(new UserControl.Message(ex));
            }
            finally
            {
                //Laws Lu,2005/10/19,新增	缓解性能问题
                ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
                ((SQLDomainDataProvider)DataProvider).PersistBroker.AutoCloseConnection = true;
            }

            return msg;

            //Laws Lu,2005/08/12
            //			ApplicationRun.GetInfoForm().Add(msg);
        }

        //Laws Lu,2005/08/12,选择已检测产品序列号,带出不良信息
        //Laws Lu,2005/08/16,修改	样本每次检验的结果分别带出来
        private Messages SetErrCodeByRunningCard(OQCLot2CardCheck card)
        {
            string strCard = card.RunningCard.Trim();
            Messages msg = new Messages();

            string oqcLotNo = FormatHelper.CleanString(this.ucLabOQCLot.Value);
            if (oqcLotNo.Length == 0)
            {
                msg.Add(new UserControl.Message(MessageType.Error, "$CS_FQCLOT_NOT_NULL"));

                return msg;
            }

            OQCFacade oqcFacade = new OQCFacade(DataProvider);
            ActionOnLineHelper actionOnLineHelper = new ActionOnLineHelper(this.DataProvider);

            object objOQCLot = oqcFacade.GetOQCLot(
                FormatHelper.PKCapitalFormat(oqcLotNo)
                , OQCFacade.Lot_Sequence_Default);

            //判断批是否存在
            if (objOQCLot == null)
            {
                msg.Add(new UserControl.Message(MessageType.Error, "$CS_LOT_NOT_EXIST"));

                ucLabOQCLot.TextFocus(false, true);
                //Remove UCLabel.SelectAll;
                return msg;
            }

            //判断该批是否已经结束
            //			if( (((OQCLot)objOQCLot).LOTStatus == OQCLotStatus.OQCLotStatus_Pass )||(((OQCLot)objOQCLot).LOTStatus == OQCLotStatus.OQCLotStatus_Reject))
            //			{
            //				msg.Add(new UserControl.Message("$CS_FQC_LOT_USED")); 
            //				ucLabOQCLot.TextFocus(false, true);
            //
            //				return msg;
            //			}

            ProductInfo productionInfo = (ProductInfo)actionOnLineHelper.GetIDInfo(strCard).GetData().Values[0];
            if (productionInfo.LastSimulation == null)
            {
                msg.Add(new UserControl.Message(MessageType.Error, "$NoSimulationInfo"));

                return msg;
            }


            object objOQCLot2Card = oqcFacade.GetOQCLot2Card(
                strCard
                , productionInfo.LastSimulation.MOCode
                , FormatHelper.PKCapitalFormat(oqcLotNo)
                , OQCFacade.Lot_Sequence_Default);

            if (objOQCLot2Card == null)
            {
                msg.Add(new UserControl.Message(MessageType.Error, "$Error_LastSimulation_IsNull"));

                ucLabOQCLot.TextFocus(false, true);
                //Remove UCLabel.SelectAll;
                return msg;
            }

            //Laws Lu,2005/08/14,新增	SPEC上标明是根据产品来获取检验项目列表
            ClearCheckGroups();
            ClearCheckItems();
            // Added By Hi1/Venus.Feng on 20080720 for Hisense Version
            msg.AddMessages(InitCheckGroupData(card.ItemCode));
            InitOQCItemGrid(msg, card.ItemCode);
            errorCodeSelect.ClearSelectedErrorCode();

            //Laws Lu,2005/08/13,设置检验项目状态
            object[] objCardChecks = oqcFacade.QueryOQCLOTCardCheckList(
                card.ItemCode
                , card.RunningCard
                , card.RunningCardSequence - 1
                , oqcLotNo
                , card.MOCode
                , ((OQCLot)objOQCLot).LotSequence);

            //Laws Lu,2005/08/14,新增 根据批号获取检验项目列表

            SetCheckStatus(objCardChecks);

            //Laws Lu,2005/08/13,带出不良代码信息
            /*(OQCLotCard2ErrorCode[])*/
            object[] objCards = oqcFacade.QueryOQCLotCard2ErrorCode(
                card.RunningCard
                , card.RunningCardSequence
                , oqcLotNo
                , card.MOCode
                , ((OQCLot)objOQCLot).LotSequence);

            SetErrors(objCards);

            return msg;
        }
        //Laws Lu,2005/08/13,添加ErrorCode列表
        private void SetErrors(object[] cards)
        {
            if (cards != null)
            {
                ArrayList objs = new ArrayList();
                foreach (object obj in cards)
                {
                    OQCLotCard2ErrorCode err = (OQCLotCard2ErrorCode)obj;
                    string errCombine = err.ErrorCodeGroup.Trim() + ":" + err.ErrorCode.Trim();
                    if (!objs.Contains(errCombine))
                        objs.Add(errCombine);
                }

                errorCodeSelect.AddSelectedErrorCodes(objs.ToArray());
            }
        }

        //Laws Lu,2005/08/13,添加Check列表
        private void SetCheckStatus(object[] checks)
        {
            if (checks != null)
            {
                foreach (object obj in checks)
                {
                    OQCLOTCardCheckList chk = (OQCLOTCardCheckList)obj;
                    for (int iGridRowLoopIndex = 0; iGridRowLoopIndex < ultraGridMain.Rows.Count; iGridRowLoopIndex++)
                    {
                        UltraGridRow dr = ultraGridMain.Rows[iGridRowLoopIndex];
                        if (dr.Cells[0].Text.Trim() == chk.CheckItemCode)
                        {
                            if (chk.Result.ToUpper().Trim() != "TRUE")
                            {
                                dr.Cells[2].Value = false;

                                dr.Cells[1].Value = GetOQCCheckedItemText(chk.Grade.ToUpper().Trim());

                                dr.Cells[3].Value = chk.MEMO;
                            }

                        }
                    }
                }
            }
            this.ultraGridMain.UpdateData();
        }
        #endregion

        public void ucButtonOQC_Click(object sender, System.EventArgs e)
        {
            Messages msg = new Messages();

            //Laws Lu,2005/10/19,新增	缓解性能问题
            ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.OpenConnection();
            DataProvider.BeginTransaction();
            try
            {

                #region 逻辑检查
                string oqcLotNo = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.ucLabOQCLot.Value));
                if (oqcLotNo.Length == 0)
                {
                    msg.Add(new UserControl.Message(new Exception("$CS_FQCLOT_NOT_NULL")));
                    ApplicationRun.GetInfoForm().Add(msg);
                    return;
                }
                ActionFactory actionFactory = new ActionFactory(this.DataProvider);
                OQCFacade oqcFacade = new OQCFacade(DataProvider);


                //判断批是否存在
                //Laws Lu,2005/11/01,修改	只需要查询一次Lot
                //			object obj = oqcFacade.GetOQCLot(FormatHelper.PKCapitalFormat(oqcLotNo),OQCFacade.Lot_Sequence_Default);
                object obj = oqcFacade.GetExamingOQCLot(FormatHelper.PKCapitalFormat(oqcLotNo), OQCFacade.Lot_Sequence_Default);
                if (obj == null)
                {
                    msg.Add(new UserControl.Message(MessageType.Error, "$CS_LOT_NOT_EXIST  $CS_FQC_LOT_NOT_PASS_OR_REJECT"));
                    ApplicationRun.GetInfoForm().Add(msg);
                    return;
                }
                //判断该批是否已经结束
                if ((((OQCLot)obj).LOTStatus == OQCLotStatus.OQCLotStatus_Pass) || (((OQCLot)obj).LOTStatus == OQCLotStatus.OQCLotStatus_Reject))
                {
                    msg.Add(new UserControl.Message(MessageType.Error, "$CS_FQC_LOT_USED"));
                    ApplicationRun.GetInfoForm().Add(msg);
                    return;
                }

                string msgInfo = String.Empty;
                if (ultraOptionSetPassReject.CheckedItem.DataValue.ToString() == OQCLotStatus.OQCLotStatus_Pass)
                {
                    msgInfo = UserControl.MutiLanguages.ParserMessage("$CS_CONFIRM_LOT_PASS " + "$CS_CURRENT_LOT_SIZE :" + ((OQCLot)obj).LotSize);
                }
                else
                {
                    msgInfo = UserControl.MutiLanguages.ParserMessage("$CS_CONFIRM_LOT_RJECT " + "$CS_CURRENT_LOT_SIZE :" + ((OQCLot)obj).LotSize); ;
                }
                //Laws Lu,2005/09/26,修改	信息提示框不默认焦点
                frmDialog dialog = new frmDialog();
                dialog.Text = this.Text;
                dialog.DialogMessage = msgInfo;

                if (System.Configuration.ConfigurationSettings.AppSettings["EnabledAutoTest"] == "1")	// 如果自动测试，则不用Confirm
                { }
                else
                {
                    if (DialogResult.OK != dialog.ShowDialog(this)/*MessageBox.Show(msgInfo,this.Text,MessageBoxButtons.OKCancel,MessageBoxIcon.None,MessageBoxDefaultButton.Button3)*/)
                    {
                        return;
                    }
                }
                #endregion

                //业务逻辑			
                ActionOnLineHelper actinOnlineHelper = new ActionOnLineHelper(this.DataProvider);
                //Laws Lu,2005/11/01,修改	只需要查询一次Cards of Lot
                //			object[] objs = oqcFacade.QueryOQCLot2Card(string.Empty,string.Empty, FormatHelper.PKCapitalFormat(oqcLotNo),OQCFacade.Lot_Sequence_Default,int.MinValue,int.MaxValue);
                object[] objs = (new ActionOQCHelper(this.DataProvider)).QueryCardOfLot(FormatHelper.PKCapitalFormat(oqcLotNo), OQCFacade.Lot_Sequence_Default);
                if (objs == null)
                {
                    msg.Add(new UserControl.Message(MessageType.Error, "$CS_LOT_NOT_EXIST"));
                    ApplicationRun.GetInfoForm().Add(msg);
                    return;
                }
                #region Laws Lu,2005/11/05,注释
                //			ProductInfo productionInfo = (ProductInfo)actinOnlineHelper.GetIDInfo( ((OQCLot2Card)objs[0]).RunningCard).GetData().Values[0];
                //			if(productionInfo.LastSimulation == null)
                //			{
                //				msg.Add( new UserControl.Message(MessageType.Error, "$NoSimulationInfo"));
                //				ApplicationRun.GetInfoForm().Add(msg);
                //				return;
                //			}

                //Laws Lu,2005/11/01,新增	改善性能
                //			if (Resource == null)
                //			{
                //				BenQGuru.eMES.BaseSetting.BaseModelFacade dataModel=new BenQGuru.eMES.BaseSetting.BaseModelFacade(this.DataProvider);
                //				Resource = (Domain.BaseSetting.Resource)dataModel.GetResource(ApplicationService.Current().ResourceCode);
                //			}
                //						
                //			ActionCheckStatus actionCheckStatus = new ActionCheckStatus();
                //			if (listActionCheckStatus.ContainsKey(product.LastSimulation.MOCode))
                //			{
                //				actionCheckStatus = (ActionCheckStatus)listActionCheckStatus[product.LastSimulation.MOCode];
                //
                //				actionCheckStatus.ProductInfo = productionInfo;
                //
                //				actionCheckStatus.ProductInfo.Resource = Resource;
                //
                //				//lastSimulation = actionCheckStatus.ProductInfo.LastSimulation;
                //				actionCheckStatus.ActionList = new ArrayList();
                //			}
                //			else
                //			{
                //				actionCheckStatus.NeedUpdateSimulation = false;
                //				actionCheckStatus.NeedFillReport = true;
                //				listActionCheckStatus.Add(product.LastSimulation.MOCode, actionCheckStatus);
                //			}


                #endregion

                //判断是批退还是Pass
                //AMOI  MARK  START  20050725
                //if(ultraOptionSetPassReject.CheckedItem.DataValue == OQCLotStatus.OQCLotStatus_Pass)
                if (ultraOptionSetPassReject.CheckedItem.DataValue.ToString() == OQCLotStatus.OQCLotStatus_Pass)
                //AMOI  MARK  END
                {

                    IAction actionPass = actionFactory.CreateAction(ActionType.DataCollectAction_OQCPass);

                    //					OQCPASSEventArgs actionEventArgs = new OQCPASSEventArgs(ActionType.DataCollectAction_OQCPass,((OQCLot2Card)objs[0]).RunningCard,ApplicationService.Current().
                    //						UserCode,ApplicationService.Current().ResourceCode, oqcLotNo ,productionInfo);

                    OQCPASSEventArgs actionEventArgs = new OQCPASSEventArgs(ActionType.DataCollectAction_OQCPass, ((Domain.DataCollect.Simulation)objs[0]).RunningCard, ApplicationService.Current().
                        UserCode, ApplicationService.Current().ResourceCode, oqcLotNo, null);

                    actionEventArgs.Lot = obj;
                    actionEventArgs.CardOfLot = objs;

                    msg.AddMessages(actionPass.Execute(actionEventArgs));
                    if (msg.IsSuccess())
                    {
                        msg.Add(new UserControl.Message(MessageType.Success, "$CS_OQCPASSSUCCESS"));
                    }
                }
                //reject
                //AMOI  MARK  START  20050725
                //if(ultraOptionSetPassReject.CheckedItem.DataValue == OQCLotStatus.OQCLotStatus_Reject)
                if (ultraOptionSetPassReject.CheckedItem.DataValue.ToString() == OQCLotStatus.OQCLotStatus_Reject)
                //AMOI  MARK  END
                {
                    IAction actionReject = actionFactory.CreateAction(ActionType.DataCollectAction_OQCReject);

                    //					OQCRejectEventArgs actionEventArgs = new OQCRejectEventArgs(ActionType.DataCollectAction_OQCReject,((OQCLot2Card)objs[0]).RunningCard,ApplicationService.Current().
                    //						UserCode,ApplicationService.Current().ResourceCode,oqcLotNo,productionInfo);

                    OQCRejectEventArgs actionEventArgs = new OQCRejectEventArgs(ActionType.DataCollectAction_OQCReject, ((Domain.DataCollect.Simulation)objs[0]).RunningCard, ApplicationService.Current().
                        UserCode, ApplicationService.Current().ResourceCode, oqcLotNo, null);

                    actionEventArgs.Lot = obj;
                    //actionEventArgs.MO
                    actionEventArgs.CardOfLot = objs;
                    //this.DataProvider.BeginTransaction();
                    msg.AddMessages(actionReject.Execute(actionEventArgs));

                    if (msg.IsSuccess())
                    {
                        msg.Add(new UserControl.Message(MessageType.Success, "$CS_OQCREJECTSUCCESS"));
                    }
                }
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                msg.Add(new UserControl.Message(ex));
            }
            finally
            {
                if (!msg.IsSuccess())
                {
                    this.DataProvider.RollbackTransaction();
                }
                else
                {
                    this.DataProvider.CommitTransaction();
                }
                //Laws Lu,2005/10/19,新增	缓解性能问题
                ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
            }

            ApplicationRun.GetInfoForm().Add(msg);

        }

        private void FCollectionOQC_Closed(object sender, System.EventArgs e)
        {
            if (this.DataProvider != null)
                ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)this.DataProvider).PersistBroker.CloseConnection();
        }

        //Amoi,2005/07/26,Laws Lu	调整不良代码选择框中按钮的位置
        private void errorCodeSelect_Resize(object sender, System.EventArgs e)
        {
            if (errorCodeSelect.Height < 120)
            {
                errorCodeSelect.AddButtonTop = errorCodeSelect.Height;
                errorCodeSelect.RemoveButtonTop = errorCodeSelect.Height;
            }
            else
            {
                errorCodeSelect.AutoAdjustButtonLocation();
            }
        }
        //Laws Lu,2005/08/13,新增	单击带出不良信息
        //Laws Lu,2005/08/16,修改	样本对象存入lbRunningIDList
        private void lbRunningIDList_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (lbRunningIDList.SelectedIndex >= 0)
            {
                string cardNO = lbRunningIDList.SelectedItem.ToString();

                int i = cardNO.IndexOf("(");
                if (i >= 0)
                {
                    cardNO = cardNO.Substring(0, i);
                }

                if (lbRunningIDList.SelectedItem is OQCLot2CardCheck)
                {
                    OQCLot2CardCheck card = (OQCLot2CardCheck)lbRunningIDList.SelectedItem;

                    ApplicationRun.GetInfoForm().Add(SetErrCodeByRunningCard(card));
                }
            }
        }

        private void btnFacilityInfo_Click(object sender, System.EventArgs e)
        {
            if (this.ucLabOQCLot.Value.Trim() != string.Empty)
            {
                FfunTestInfo frmTestInfo = new FfunTestInfo(ucLabOQCLot.Value.Trim());
                frmTestInfo.ShowDialog();
            }
        }

        private void btnGetLot_Click(object sender, System.EventArgs e)
        {
            DataCollect.DataCollectFacade dcf = new BenQGuru.eMES.DataCollect.DataCollectFacade(DataProvider);
            string rcard = txtRcard.Value.ToUpper().Trim();
            if (rcard != String.Empty)
            {
                object obj = dcf.GetSimulation(rcard);
                if (obj != null)
                {
                    Domain.DataCollect.Simulation sim = obj as Domain.DataCollect.Simulation;

                    string lotno = sim.LOTNO;

                    ucLabOQCLot.Value = lotno;
                    ucLabOQCLot.TextFocus(false, true);

                    SendKeys.Send("\r");
                }
                else
                {
                    txtRcard.TextFocus(false, true);
                    //Remove UCLabel.SelectAll;
                }
            }
        }

        private void txtRcard_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                DataCollect.DataCollectFacade dcf = new BenQGuru.eMES.DataCollect.DataCollectFacade(DataProvider);
                string rcard = txtRcard.Value.ToUpper().Trim();
                if (rcard != String.Empty)
                {
                    object obj = dcf.GetSimulation(rcard);
                    if (obj != null)
                    {
                        Domain.DataCollect.Simulation sim = obj as Domain.DataCollect.Simulation;

                        string lotno = sim.LOTNO;

                        ucLabOQCLot.Value = lotno;
                        ucLabOQCLot.TextFocus(false, true);

                        LabOQCLotKeyPress();
                    }
                    else
                    {
                        txtRcard.TextFocus(false, true);
                        //Remove UCLabel.SelectAll;
                    }
                }
            }
        }

        private void InitializeCheckGroupGrid()
        {
            dtCheckGroup.Columns.Clear();

            dtCheckGroup.Columns.Add("Checked", typeof(bool));
            dtCheckGroup.Columns.Add("CheckGroup", typeof(string)).ReadOnly = true;
            dtCheckGroup.Columns.Add("TestedQty", typeof(int)).ReadOnly = true;
            dtCheckGroup.Columns.Add("SampleQty", typeof(int)).ReadOnly = true;

            this.ultraGridCheckGroup.DataSource = dtCheckGroup;
        }

        private Messages InitCheckGroupData(string itemCode)
        {
            Messages newMessages = new Messages();
            OQCFacade oqcFacade = new OQCFacade(DataProvider);

            // Get All CheckGroup
            object[] objs = oqcFacade.GetAllOQCCheckGroup();
            if (objs == null)
            {
                newMessages.Add(new UserControl.Message(MessageType.Error, "$Error_NoCheckGroupMaintain"));
                return newMessages;
            }

            // Get Local Config CheckGroup
            OQCConfiger oqcConfiger = OQCConfiger.Load();
            if (oqcConfiger.CheckGroupList.Count == 0)
            {
                newMessages.Add(new UserControl.Message(MessageType.Error, "$Error_NoCheckGroupConfig"));
                return newMessages;
            }

            dtCheckGroup.Rows.Clear();
            foreach (OQCCheckGroup oqcCKGroup in objs)
            {
                if (oqcConfiger.CheckGroupList.ContainsKey(oqcCKGroup.CheckGroupCode))
                {
                    dtCheckGroup.Rows.Add(new object[] { false, oqcCKGroup.CheckGroupCode, 0, int.Parse(oqcConfiger.CheckGroupList[oqcCKGroup.CheckGroupCode].ToString()) });
                }
            }

            object[] oqclot2ckgroup = oqcFacade.GetOQCLot2CheckGroupOfLot(FormatHelper.PKCapitalFormat(this.ucLabOQCLot.Value.Trim()), OQCFacade.Lot_Sequence_Default);
            if (oqclot2ckgroup != null && oqclot2ckgroup.Length > 0)
            {
                bool hasChecked = false;
                foreach (DataRow row in dtCheckGroup.Rows)
                {
                    foreach (OQCLot2CheckGroup oqcLot2CheckGroup in oqclot2ckgroup)
                    {
                        if (string.Compare(oqcLot2CheckGroup.CheckGroup, Convert.ToString(row["CheckGroup"]), true) == 0)
                        {
                            dtCheckGroup.Columns["TestedQty"].ReadOnly = false;
                            row["TestedQty"] = oqcLot2CheckGroup.CheckedCount;
                            if (hasChecked == false)
                            {
                                row["Checked"] = true;
                                hasChecked = true;
                            }

                            dtCheckGroup.Columns["TestedQty"].ReadOnly = true;
                            break;
                        }
                    }
                }
            }
            else
            {
                if (dtCheckGroup.Rows.Count > 0)
                {
                    dtCheckGroup.Rows[0]["Checked"] = true;
                }
            }

            this.dtCheckGroup.AcceptChanges();
            this.ultraGridCheckGroup.UpdateData();

            return newMessages;
        }

        private void ultraGridCheckGroup_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            ultraWinGridHelper = new UltraWinGridHelper(this.ultraGridCheckGroup);

            ultraWinGridHelper.AddCheckColumn("Checked", "");
            ultraWinGridHelper.AddReadOnlyColumn("CheckGroup", "检验类型");
            ultraWinGridHelper.AddReadOnlyColumn("TestedQty", "已测数量");
            ultraWinGridHelper.AddReadOnlyColumn("SampleQty", "样本数量");

            this.ultraGridCheckGroup.DisplayLayout.Bands[0].Columns["Checked"].Width = 24;
            this.ultraGridCheckGroup.DisplayLayout.Bands[0].Columns["CheckGroup"].Width = 70;
            this.ultraGridCheckGroup.DisplayLayout.Bands[0].Columns["TestedQty"].Width = 70;
            this.ultraGridCheckGroup.DisplayLayout.Bands[0].Columns["SampleQty"].Width = 70;
        }

        private void ultraGridCheckGroup_CellChange(object sender, CellEventArgs e)
        {
            Messages msg = new Messages();
            string oqcLotNo = FormatHelper.CleanString(this.ucLabOQCLot.Value);
            if (oqcLotNo.Length == 0)
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_FQCLOT_NOT_NULL"));
                return;
            }
            OQCFacade oqcFacade = new OQCFacade(DataProvider);
            object objOQCLot = oqcFacade.GetOQCLot(
                FormatHelper.PKCapitalFormat(oqcLotNo)
                , OQCFacade.Lot_Sequence_Default);

            //判断批是否存在
            if (objOQCLot == null)
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_LOT_NOT_EXIST"));
                ucLabOQCLot.TextFocus(false, true);
                return;
            }

            this.ultraGridCheckGroup.UpdateData();
            ClearCheckItems();
            if (e.Cell.Column.Key == "Checked")
            {
                if (Convert.ToBoolean(e.Cell.Value) == true)
                {
                    for (int i = 0; i < this.ultraGridCheckGroup.Rows.Count; i++)
                    {
                        if (this.ultraGridCheckGroup.Rows[i] != e.Cell.Row
                            && Convert.ToBoolean(this.ultraGridCheckGroup.Rows[i].Cells["Checked"].Value) == true)
                        {
                            this.ultraGridCheckGroup.Rows[i].Cells["Checked"].Value = false;
                            this.ultraGridCheckGroup.UpdateData();
                            break;
                        }
                    }
                }

                // Filter the CheckList
                if (lbRunningIDList.SelectedIndex >= 0)
                {
                    string cardNO = lbRunningIDList.SelectedItem.ToString();

                    int i = cardNO.IndexOf("(");
                    if (i >= 0)
                    {
                        cardNO = cardNO.Substring(0, i);
                    }

                    if (lbRunningIDList.SelectedItem is OQCLot2CardCheck)
                    {
                        OQCLot2CardCheck card = (OQCLot2CardCheck)lbRunningIDList.SelectedItem;
                        msg.AddMessages(InitOQCItemGrid(msg, card.ItemCode));
                        if (!msg.IsSuccess())
                        {

                        }
                        object[] objCardChecks = oqcFacade.QueryOQCLOTCardCheckList(
                                                            card.ItemCode
                                                            , card.RunningCard
                                                            , card.RunningCardSequence
                                                            , oqcLotNo
                                                            , card.MOCode
                                                            , ((OQCLot)objOQCLot).LotSequence);

                        SetCheckStatus(objCardChecks);
                    }
                    else
                    {
                        // 新输入的Item
                        string runningID = lbRunningIDList.Items[lbRunningIDList.SelectedIndex].ToString().ToUpper().Trim();
                        if (runningID.Length == 0)
                        {
                            ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_Please_Input_RunningCard"));

                            ucLabRunningID.TextFocus(false, true);
                            return;
                        }
                        //判断ID是否有生产信息
                        ActionOnLineHelper actinOnlineHelper = new ActionOnLineHelper(this.DataProvider);
                        ProductInfo productInfo = (ProductInfo)actinOnlineHelper.GetIDInfo(FormatHelper.PKCapitalFormat(runningID)).GetData().Values[0];
                        if (productInfo.LastSimulation == null)
                        {
                            msg.Add(new UserControl.Message(MessageType.Error, "$NoSimulationInfo $CS_Param_ID =" + ucLabRunningID.Value));
                            ApplicationRun.GetInfoForm().Add(msg);

                            ucLabRunningID.Enabled = true;
                            ucLabRunningID.TextFocus(false, true);
                            return;
                        }

                        msg.AddMessages(InitOQCItemGrid(msg, productInfo.LastSimulation.ItemCode));
                        if (!msg.IsSuccess())
                        {
                            ApplicationRun.GetInfoForm().Add(msg);
                            ucLabRunningID.Enabled = true;
                            ucLabRunningID.TextFocus(false, true);
                            return;
                        }
                    }
                }
                else
                { 
                    // 新输入的Item
                    string runningID = FormatHelper.CleanString(this.ucLabRunningID.Value);
                    if (runningID.Length == 0)
                    {
                        ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_Please_Input_RunningCard"));

                        ucLabRunningID.TextFocus(false, true);
                        return;
                    }
                    //判断ID是否有生产信息
                    ActionOnLineHelper actinOnlineHelper = new ActionOnLineHelper(this.DataProvider);
                    ProductInfo productInfo = (ProductInfo)actinOnlineHelper.GetIDInfo(FormatHelper.PKCapitalFormat(runningID)).GetData().Values[0];
                    if (productInfo.LastSimulation == null)
                    {
                        msg.Add(new UserControl.Message(MessageType.Error, "$NoSimulationInfo $CS_Param_ID =" + ucLabRunningID.Value));
                        ApplicationRun.GetInfoForm().Add(msg);

                        ucLabRunningID.Enabled = true;
                        ucLabRunningID.TextFocus(false, true);
                        return;
                    }

                    msg.AddMessages(InitOQCItemGrid(msg, productInfo.LastSimulation.ItemCode));
                    if (!msg.IsSuccess())
                    {
                        ApplicationRun.GetInfoForm().Add(msg);
                        ucLabRunningID.Enabled = true;
                        ucLabRunningID.TextFocus(false, true);
                        return;
                    }
                }
            }
        }

        private void ucButtonForcePass_Click(object sender, EventArgs e)
        {
            Messages msg = new Messages();

            //Laws Lu,2005/10/19,新增	缓解性能问题
            ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.OpenConnection();
            DataProvider.BeginTransaction();
            try
            {

                #region 逻辑检查
                string oqcLotNo = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.ucLabOQCLot.Value));
                if (oqcLotNo.Length == 0)
                {
                    msg.Add(new UserControl.Message(new Exception("$CS_FQCLOT_NOT_NULL")));
                    ApplicationRun.GetInfoForm().Add(msg);
                    return;
                }
                ActionFactory actionFactory = new ActionFactory(this.DataProvider);
                OQCFacade oqcFacade = new OQCFacade(DataProvider);


                //判断批是否存在
                //Laws Lu,2005/11/01,修改	只需要查询一次Lot
                object obj = oqcFacade.GetExamingOQCLot(FormatHelper.PKCapitalFormat(oqcLotNo), OQCFacade.Lot_Sequence_Default);
                if (obj == null)
                {
                    msg.Add(new UserControl.Message(MessageType.Error, "$CS_LOT_NOT_EXIST  $CS_FQC_LOT_NOT_PASS_OR_REJECT"));
                    ApplicationRun.GetInfoForm().Add(msg);
                    return;
                }
                //判断该批是否已经结束
                if ((((OQCLot)obj).LOTStatus == OQCLotStatus.OQCLotStatus_Pass) || (((OQCLot)obj).LOTStatus == OQCLotStatus.OQCLotStatus_Reject))
                {
                    msg.Add(new UserControl.Message(MessageType.Error, "$CS_FQC_LOT_USED"));
                    ApplicationRun.GetInfoForm().Add(msg);
                    return;
                }

                string msgInfo = String.Empty;
                if (ultraOptionSetPassReject.CheckedItem.DataValue.ToString() == OQCLotStatus.OQCLotStatus_Pass)
                {
                    msgInfo = UserControl.MutiLanguages.ParserMessage("$CS_CONFIRM_LOT_PASS " + "$CS_CURRENT_LOT_SIZE :" + ((OQCLot)obj).LotSize);
                }
                else
                {
                    msgInfo = UserControl.MutiLanguages.ParserMessage("$CS_CONFIRM_LOT_RJECT " + "$CS_CURRENT_LOT_SIZE :" + ((OQCLot)obj).LotSize); ;
                }

                frmDialog dialog = new frmDialog();
                dialog.Text = this.Text;
                dialog.DialogMessage = msgInfo;

                if (System.Configuration.ConfigurationSettings.AppSettings["EnabledAutoTest"] == "1")	// 如果自动测试，则不用Confirm
                { }
                else
                {
                    if (DialogResult.OK != dialog.ShowDialog(this)/*MessageBox.Show(msgInfo,this.Text,MessageBoxButtons.OKCancel,MessageBoxIcon.None,MessageBoxDefaultButton.Button3)*/)
                    {
                        return;
                    }
                }
                #endregion

                //业务逻辑			
                ActionOnLineHelper actinOnlineHelper = new ActionOnLineHelper(this.DataProvider);
                object[] objs = (new ActionOQCHelper(this.DataProvider)).QueryCardOfLot(FormatHelper.PKCapitalFormat(oqcLotNo), OQCFacade.Lot_Sequence_Default);
                if (objs == null)
                {
                    msg.Add(new UserControl.Message(MessageType.Error, "$CS_LOT_NOT_EXIST"));
                    ApplicationRun.GetInfoForm().Add(msg);
                    return;
                }
                
                IAction actionPass = actionFactory.CreateAction(ActionType.DataCollectAction_OQCPass);

                OQCPASSEventArgs actionEventArgs = new OQCPASSEventArgs(ActionType.DataCollectAction_OQCPass, ((Domain.DataCollect.Simulation)objs[0]).RunningCard, ApplicationService.Current().
                    UserCode, ApplicationService.Current().ResourceCode, oqcLotNo, null);

                actionEventArgs.Lot = obj;
                actionEventArgs.CardOfLot = objs;
                actionEventArgs.IsForcePass = true;

                msg.AddMessages(actionPass.Execute(actionEventArgs));
                if (msg.IsSuccess())
                {
                    msg.Add(new UserControl.Message(MessageType.Success, "$CS_OQCPASSSUCCESS"));
                }

            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                msg.Add(new UserControl.Message(ex));
            }
            finally
            {
                if (!msg.IsSuccess())
                {
                    this.DataProvider.RollbackTransaction();
                }
                else
                {
                    this.DataProvider.CommitTransaction();
                }
                //Laws Lu,2005/10/19,新增	缓解性能问题
                ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
            }

            ApplicationRun.GetInfoForm().Add(msg);
        }

        private void ucButtonCancel_Click(object sender, EventArgs e)
        {
            Messages msg = RequesData();
            ApplicationRun.GetInfoForm().Add(msg);

            if (msg.IsSuccess())
            {
                ucLabRunningID.Enabled = true;
                ucLabRunningID.TextFocus(false, true);
                //Remove UCLabel.SelectAll;
            }
            else
            {
                ucLabRunningID.Enabled = true;
                ucLabRunningID.Value = "";

                ucLabOQCLot.Enabled = true;
                ucLabOQCLot.TextFocus(false, true);
                //Remove UCLabel.SelectAll;
            }
        }
    }

    #region CheckMNID类
    internal class CheckMNID
    {
        private string _mnid;
        private string _mnidSequence;
        private int _occurCount;


        public CheckMNID(string mnid, string mnidSequence,int occurCount)
        {
            this._mnid = mnid;
            this._mnidSequence = mnidSequence;
            this._occurCount = occurCount;
        }

        public CheckMNID(string mnid, string mnidSequence)
        {
            this._mnid = mnid;
            this._mnidSequence = mnidSequence;
            this._occurCount = 0;
        }

        public string MNID
        {
            get
            {
                return this._mnid;
            }
        }

        public string MNIDSequence
        {
            get
            {
                return this._mnidSequence;
            }
        }

        public override string ToString()
        {

            if (this._occurCount <= 1)
            {
                return this._mnid;
            }
            else
            {
                return this._mnid;//+"("+ this._occurCount+")";
            }
        }

    }
    #endregion

    #region CheckMNIDManager类
    internal class CheckMNIDManager
    {
        Hashtable tmpHashTable = null;
        public CheckMNIDManager()
        {
            tmpHashTable = new Hashtable();
        }

        public string AddMNID(CheckMNID checkMNID)
        {
            if (checkMNID == null)
            {
                return string.Empty;
            }
            if (tmpHashTable.ContainsKey(checkMNID.MNID))
            {
                try
                {
                    tmpHashTable[checkMNID.MNID] = System.Int32.Parse(tmpHashTable[checkMNID.MNID].ToString()) + 1;

                }
                catch
                {
                    tmpHashTable[checkMNID.MNID] = 1;
                }
            }
            else
            {
                tmpHashTable[checkMNID.MNID] = 1;
            }
            return (new CheckMNID(checkMNID.MNID, checkMNID.MNIDSequence, (int)tmpHashTable[checkMNID.MNID])).ToString();
        }

    }
    #endregion
}

