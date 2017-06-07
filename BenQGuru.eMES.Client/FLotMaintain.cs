using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using BenQGuru.eMES.TS;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Client.Service;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.DataCollect.Action;
using BenQGuru.eMES.DataCollect;
using UserControl;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Domain.Warehouse;
using Infragistics.Win.UltraWinGrid;
using BenQGuru.eMES.CodeSoftPrint;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Domain.BaseSetting;
using System.Collections.Generic;
using BenQGuru.eMES.Material;
using Infragistics.Win;


namespace BenQGuru.eMES.Client
{
    /// <summary>
    /// FBurnIn 的摘要说明。
    /// </summary>
    public class FLotMaintain : BaseForm
    {
        private string type = "";
        private decimal qty = 0;

        private bool _IsBatchPrint = true;
        private string _DataDescFileName = "Label.dsc";
        public PrintTemplate[] _PrintTemplateList = null;
        private Hashtable ht = new Hashtable();

        private DataSet m_CheckHeadList = null;
        private DataSet m_CheckFooderList = null;
        private DataTable m_LotHead = null;
        private DataTable m_LotFooder = null;
        private InventoryFacade _InventoryFacade = null;
        private MOFacade _MOFacade = null;
        private ItemFacade _ItemFacade = null;
        private WarehouseFacade _WarehouseFacade = null;


        private string itemcode = string.Empty;
        public string ItemCode
        {
            get
            {
                return itemcode;
            }
            set
            {
                itemcode = value;
            }
        }

        private string firstletter = string.Empty;
        public string FirstLetter
        {
            get
            {
                return firstletter;
            }
            set
            {
                firstletter = value;
            }
        }

        public UserControl.UCLabelEdit uclMaterialLotNo;
        public UserControl.UCLabelEdit uclMaterialCode;
        private UserControl.UCButton ucbCancel;
        private IDomainDataProvider _domainDataProvider = ApplicationService.Current().DataProvider;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private UserControl.UCLabelEdit uclVeenderCode;
        private Button button_VeenderCode;
        private Button button_MaterialCode;
        private Panel panel3;
        private UCButton ucButton_OpenLot;
        private UCButton ucButton_MerageLot;
        private UCButton btnSave;
        public UCLabelCombox ucLabelComboxPrintTemplete;
        public UCLabelCombox ucLabelComboxPrintList;
        private UCLabelEdit ucLabelEditPrintCount;
        public UCButton ucButtonPrint;
        private UCButton ucButtonQuery;
        private UltraGrid ultraGridFooder;
        private UltraGrid ultraGridHead;
        private UCLabelEdit ucLabelEditLotCount;
        private UCLabelEdit ucLabelEditBZCount;
        private RadioButton radioButtonBz;
        private RadioButton radioButtonLotNum;
        private IContainer components;


        public IDomainDataProvider DataProvider
        {
            get
            {
                return _domainDataProvider;
            }
        }

        public FLotMaintain()
        {
            //
            // Windows 窗体设计器支持所必需的
            //
            InitializeComponent();

            UserControl.UIStyleBuilder.FormUI(this);

            //this.ultraGridHead.DisplayLayout.Appearance.BackColor = System.Drawing.Color.White;;
            //this.ultraGridHead.DisplayLayout.CaptionAppearance.BackColor =Color.FromName("WhiteSmoke");
            //this.ultraGridHead.DisplayLayout.Appearance.BackColor=Color.FromArgb(255, 255, 255);
            //this.ultraGridHead.DisplayLayout.Override.HeaderAppearance.BackColor = Color.FromName("WhiteSmoke");
            //this.ultraGridHead.DisplayLayout.Override.RowAppearance.BackColor =Color.FromArgb(230, 234, 245);
            //this.ultraGridHead.DisplayLayout.Override.RowAlternateAppearance.BackColor=Color.FromArgb(255, 255, 255);
            //this.ultraGridHead.DisplayLayout.Override.RowSelectors =Infragistics.Win.DefaultableBoolean.False;
            //this.ultraGridHead.DisplayLayout.Override.ActiveRowAppearance.BackColor = System.Drawing.Color.Gainsboro;
            //this.ultraGridHead.DisplayLayout.Override.ActiveRowAppearance.ForeColor = System.Drawing.Color.Black;
            //this.ultraGridHead.DisplayLayout.ScrollBarLook.Appearance.BackColor =Color.FromName("LightGray");

            InitializeMainGrid();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FLotMaintain));
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance27 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance28 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance29 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance30 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance31 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance32 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance33 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance34 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance35 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance36 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance37 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance38 = new Infragistics.Win.Appearance();
            this.uclMaterialLotNo = new UserControl.UCLabelEdit();
            this.uclMaterialCode = new UserControl.UCLabelEdit();
            this.ucbCancel = new UserControl.UCButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ucButtonQuery = new UserControl.UCButton();
            this.button_VeenderCode = new System.Windows.Forms.Button();
            this.button_MaterialCode = new System.Windows.Forms.Button();
            this.uclVeenderCode = new UserControl.UCLabelEdit();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ucButtonPrint = new UserControl.UCButton();
            this.ucLabelComboxPrintTemplete = new UserControl.UCLabelCombox();
            this.ucLabelComboxPrintList = new UserControl.UCLabelCombox();
            this.ucLabelEditPrintCount = new UserControl.UCLabelEdit();
            this.btnSave = new UserControl.UCButton();
            this.panel3 = new System.Windows.Forms.Panel();
            this.radioButtonLotNum = new System.Windows.Forms.RadioButton();
            this.radioButtonBz = new System.Windows.Forms.RadioButton();
            this.ucLabelEditLotCount = new UserControl.UCLabelEdit();
            this.ucLabelEditBZCount = new UserControl.UCLabelEdit();
            this.ultraGridFooder = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.ultraGridHead = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.ucButton_OpenLot = new UserControl.UCButton();
            this.ucButton_MerageLot = new UserControl.UCButton();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridFooder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridHead)).BeginInit();
            this.SuspendLayout();
            // 
            // uclMaterialLotNo
            // 
            this.uclMaterialLotNo.AllowEditOnlyChecked = true;
            this.uclMaterialLotNo.AutoSelectAll = false;
            this.uclMaterialLotNo.AutoUpper = true;
            this.uclMaterialLotNo.Caption = "物料批号";
            this.uclMaterialLotNo.Checked = false;
            this.uclMaterialLotNo.EditType = UserControl.EditTypes.String;
            this.uclMaterialLotNo.Location = new System.Drawing.Point(14, 7);
            this.uclMaterialLotNo.MaxLength = 40;
            this.uclMaterialLotNo.Multiline = false;
            this.uclMaterialLotNo.Name = "uclMaterialLotNo";
            this.uclMaterialLotNo.PasswordChar = '\0';
            this.uclMaterialLotNo.ReadOnly = false;
            this.uclMaterialLotNo.ShowCheckBox = false;
            this.uclMaterialLotNo.Size = new System.Drawing.Size(194, 23);
            this.uclMaterialLotNo.TabIndex = 0;
            this.uclMaterialLotNo.TabNext = true;
            this.uclMaterialLotNo.Value = "";
            this.uclMaterialLotNo.WidthType = UserControl.WidthTypes.Normal;
            this.uclMaterialLotNo.XAlign = 75;
            // 
            // uclMaterialCode
            // 
            this.uclMaterialCode.AllowEditOnlyChecked = true;
            this.uclMaterialCode.AutoSelectAll = false;
            this.uclMaterialCode.AutoUpper = true;
            this.uclMaterialCode.Caption = "物料代码";
            this.uclMaterialCode.Checked = false;
            this.uclMaterialCode.EditType = UserControl.EditTypes.String;
            this.uclMaterialCode.Location = new System.Drawing.Point(224, 7);
            this.uclMaterialCode.MaxLength = 60;
            this.uclMaterialCode.Multiline = false;
            this.uclMaterialCode.Name = "uclMaterialCode";
            this.uclMaterialCode.PasswordChar = '\0';
            this.uclMaterialCode.ReadOnly = false;
            this.uclMaterialCode.ShowCheckBox = false;
            this.uclMaterialCode.Size = new System.Drawing.Size(194, 23);
            this.uclMaterialCode.TabIndex = 1;
            this.uclMaterialCode.TabNext = true;
            this.uclMaterialCode.Value = "";
            this.uclMaterialCode.WidthType = UserControl.WidthTypes.Normal;
            this.uclMaterialCode.XAlign = 285;
            // 
            // ucbCancel
            // 
            this.ucbCancel.BackColor = System.Drawing.SystemColors.Control;
            this.ucbCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucbCancel.BackgroundImage")));
            this.ucbCancel.ButtonType = UserControl.ButtonTypes.Cancle;
            this.ucbCancel.Caption = "取消";
            this.ucbCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucbCancel.Location = new System.Drawing.Point(102, 16);
            this.ucbCancel.Name = "ucbCancel";
            this.ucbCancel.Size = new System.Drawing.Size(88, 22);
            this.ucbCancel.TabIndex = 13;
            this.ucbCancel.Click += new System.EventHandler(this.ucbCancel_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ucButtonQuery);
            this.panel1.Controls.Add(this.button_VeenderCode);
            this.panel1.Controls.Add(this.button_MaterialCode);
            this.panel1.Controls.Add(this.uclVeenderCode);
            this.panel1.Controls.Add(this.uclMaterialLotNo);
            this.panel1.Controls.Add(this.uclMaterialCode);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1023, 39);
            this.panel1.TabIndex = 20;
            // 
            // ucButtonQuery
            // 
            this.ucButtonQuery.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonQuery.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonQuery.BackgroundImage")));
            this.ucButtonQuery.ButtonType = UserControl.ButtonTypes.Query;
            this.ucButtonQuery.Caption = "查询";
            this.ucButtonQuery.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonQuery.Location = new System.Drawing.Point(725, 7);
            this.ucButtonQuery.Name = "ucButtonQuery";
            this.ucButtonQuery.Size = new System.Drawing.Size(88, 22);
            this.ucButtonQuery.TabIndex = 8;
            this.ucButtonQuery.Click += new System.EventHandler(this.ucButtonQuery_Click);
            // 
            // button_VeenderCode
            // 
            this.button_VeenderCode.Location = new System.Drawing.Point(684, 7);
            this.button_VeenderCode.Name = "button_VeenderCode";
            this.button_VeenderCode.Size = new System.Drawing.Size(35, 23);
            this.button_VeenderCode.TabIndex = 5;
            this.button_VeenderCode.Text = "...";
            this.button_VeenderCode.UseVisualStyleBackColor = true;
            this.button_VeenderCode.Click += new System.EventHandler(this.button_VeenderCode_Click);
            // 
            // button_MaterialCode
            // 
            this.button_MaterialCode.Location = new System.Drawing.Point(424, 7);
            this.button_MaterialCode.Name = "button_MaterialCode";
            this.button_MaterialCode.Size = new System.Drawing.Size(35, 23);
            this.button_MaterialCode.TabIndex = 4;
            this.button_MaterialCode.Text = "...";
            this.button_MaterialCode.UseVisualStyleBackColor = true;
            this.button_MaterialCode.Click += new System.EventHandler(this.button_MaterialCode_Click);
            // 
            // uclVeenderCode
            // 
            this.uclVeenderCode.AllowEditOnlyChecked = true;
            this.uclVeenderCode.AutoSelectAll = false;
            this.uclVeenderCode.AutoUpper = true;
            this.uclVeenderCode.Caption = "供应商代码";
            this.uclVeenderCode.Checked = false;
            this.uclVeenderCode.EditType = UserControl.EditTypes.String;
            this.uclVeenderCode.Location = new System.Drawing.Point(472, 7);
            this.uclVeenderCode.MaxLength = 60;
            this.uclVeenderCode.Multiline = false;
            this.uclVeenderCode.Name = "uclVeenderCode";
            this.uclVeenderCode.PasswordChar = '\0';
            this.uclVeenderCode.ReadOnly = false;
            this.uclVeenderCode.ShowCheckBox = false;
            this.uclVeenderCode.Size = new System.Drawing.Size(206, 23);
            this.uclVeenderCode.TabIndex = 2;
            this.uclVeenderCode.TabNext = true;
            this.uclVeenderCode.Value = "";
            this.uclVeenderCode.WidthType = UserControl.WidthTypes.Normal;
            this.uclVeenderCode.XAlign = 545;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.ucButtonPrint);
            this.panel2.Controls.Add(this.ucLabelComboxPrintTemplete);
            this.panel2.Controls.Add(this.ucLabelComboxPrintList);
            this.panel2.Controls.Add(this.ucLabelEditPrintCount);
            this.panel2.Controls.Add(this.btnSave);
            this.panel2.Controls.Add(this.ucbCancel);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 515);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1023, 56);
            this.panel2.TabIndex = 21;
            // 
            // ucButtonPrint
            // 
            this.ucButtonPrint.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonPrint.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonPrint.BackgroundImage")));
            this.ucButtonPrint.ButtonType = UserControl.ButtonTypes.None;
            this.ucButtonPrint.Caption = "打印";
            this.ucButtonPrint.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonPrint.Location = new System.Drawing.Point(771, 14);
            this.ucButtonPrint.Name = "ucButtonPrint";
            this.ucButtonPrint.Size = new System.Drawing.Size(88, 22);
            this.ucButtonPrint.TabIndex = 54;
            this.ucButtonPrint.Click += new System.EventHandler(this.ucButtonPrint_Click);
            // 
            // ucLabelComboxPrintTemplete
            // 
            this.ucLabelComboxPrintTemplete.AllowEditOnlyChecked = true;
            this.ucLabelComboxPrintTemplete.Caption = "打印模板";
            this.ucLabelComboxPrintTemplete.Checked = false;
            this.ucLabelComboxPrintTemplete.Location = new System.Drawing.Point(571, 16);
            this.ucLabelComboxPrintTemplete.Name = "ucLabelComboxPrintTemplete";
            this.ucLabelComboxPrintTemplete.SelectedIndex = -1;
            this.ucLabelComboxPrintTemplete.ShowCheckBox = false;
            this.ucLabelComboxPrintTemplete.Size = new System.Drawing.Size(194, 20);
            this.ucLabelComboxPrintTemplete.TabIndex = 52;
            this.ucLabelComboxPrintTemplete.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabelComboxPrintTemplete.XAlign = 632;
            // 
            // ucLabelComboxPrintList
            // 
            this.ucLabelComboxPrintList.AllowEditOnlyChecked = true;
            this.ucLabelComboxPrintList.Caption = "打印机列表";
            this.ucLabelComboxPrintList.Checked = false;
            this.ucLabelComboxPrintList.Location = new System.Drawing.Point(361, 16);
            this.ucLabelComboxPrintList.Name = "ucLabelComboxPrintList";
            this.ucLabelComboxPrintList.SelectedIndex = -1;
            this.ucLabelComboxPrintList.ShowCheckBox = false;
            this.ucLabelComboxPrintList.Size = new System.Drawing.Size(206, 20);
            this.ucLabelComboxPrintList.TabIndex = 53;
            this.ucLabelComboxPrintList.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabelComboxPrintList.XAlign = 434;
            // 
            // ucLabelEditPrintCount
            // 
            this.ucLabelEditPrintCount.AllowEditOnlyChecked = true;
            this.ucLabelEditPrintCount.AutoSelectAll = false;
            this.ucLabelEditPrintCount.AutoUpper = true;
            this.ucLabelEditPrintCount.Caption = "打印数量";
            this.ucLabelEditPrintCount.Checked = false;
            this.ucLabelEditPrintCount.EditType = UserControl.EditTypes.Integer;
            this.ucLabelEditPrintCount.Location = new System.Drawing.Point(195, 16);
            this.ucLabelEditPrintCount.MaxLength = 8;
            this.ucLabelEditPrintCount.Multiline = false;
            this.ucLabelEditPrintCount.Name = "ucLabelEditPrintCount";
            this.ucLabelEditPrintCount.PasswordChar = '\0';
            this.ucLabelEditPrintCount.ReadOnly = false;
            this.ucLabelEditPrintCount.ShowCheckBox = false;
            this.ucLabelEditPrintCount.Size = new System.Drawing.Size(161, 24);
            this.ucLabelEditPrintCount.TabIndex = 51;
            this.ucLabelEditPrintCount.TabNext = false;
            this.ucLabelEditPrintCount.Tag = "";
            this.ucLabelEditPrintCount.Value = "1";
            this.ucLabelEditPrintCount.WidthType = UserControl.WidthTypes.Small;
            this.ucLabelEditPrintCount.XAlign = 256;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.SystemColors.Control;
            this.btnSave.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSave.BackgroundImage")));
            this.btnSave.ButtonType = UserControl.ButtonTypes.Save;
            this.btnSave.Caption = "保存";
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.Location = new System.Drawing.Point(13, 16);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(88, 22);
            this.btnSave.TabIndex = 21;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.Controls.Add(this.radioButtonLotNum);
            this.panel3.Controls.Add(this.radioButtonBz);
            this.panel3.Controls.Add(this.ucLabelEditLotCount);
            this.panel3.Controls.Add(this.ucLabelEditBZCount);
            this.panel3.Controls.Add(this.ultraGridFooder);
            this.panel3.Controls.Add(this.ultraGridHead);
            this.panel3.Controls.Add(this.ucButton_OpenLot);
            this.panel3.Controls.Add(this.ucButton_MerageLot);
            this.panel3.Location = new System.Drawing.Point(0, 37);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1023, 472);
            this.panel3.TabIndex = 22;
            // 
            // radioButtonLotNum
            // 
            this.radioButtonLotNum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.radioButtonLotNum.AutoSize = true;
            this.radioButtonLotNum.Location = new System.Drawing.Point(195, 238);
            this.radioButtonLotNum.Name = "radioButtonLotNum";
            this.radioButtonLotNum.Size = new System.Drawing.Size(14, 13);
            this.radioButtonLotNum.TabIndex = 55;
            this.radioButtonLotNum.TabStop = true;
            this.radioButtonLotNum.UseVisualStyleBackColor = true;
            this.radioButtonLotNum.CheckedChanged += new System.EventHandler(this.radioButtonLotNum_CheckedChanged);
            // 
            // radioButtonBz
            // 
            this.radioButtonBz.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.radioButtonBz.AutoSize = true;
            this.radioButtonBz.Checked = true;
            this.radioButtonBz.Location = new System.Drawing.Point(20, 238);
            this.radioButtonBz.Name = "radioButtonBz";
            this.radioButtonBz.Size = new System.Drawing.Size(14, 13);
            this.radioButtonBz.TabIndex = 54;
            this.radioButtonBz.TabStop = true;
            this.radioButtonBz.UseVisualStyleBackColor = true;
            this.radioButtonBz.CheckedChanged += new System.EventHandler(this.radioButtonBz_CheckedChanged);
            // 
            // ucLabelEditLotCount
            // 
            this.ucLabelEditLotCount.AllowEditOnlyChecked = true;
            this.ucLabelEditLotCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ucLabelEditLotCount.AutoSelectAll = false;
            this.ucLabelEditLotCount.AutoUpper = true;
            this.ucLabelEditLotCount.Caption = "批数";
            this.ucLabelEditLotCount.Checked = false;
            this.ucLabelEditLotCount.EditType = UserControl.EditTypes.Integer;
            this.ucLabelEditLotCount.Enabled = false;
            this.ucLabelEditLotCount.Location = new System.Drawing.Point(223, 234);
            this.ucLabelEditLotCount.MaxLength = 8;
            this.ucLabelEditLotCount.Multiline = false;
            this.ucLabelEditLotCount.Name = "ucLabelEditLotCount";
            this.ucLabelEditLotCount.PasswordChar = '\0';
            this.ucLabelEditLotCount.ReadOnly = false;
            this.ucLabelEditLotCount.ShowCheckBox = false;
            this.ucLabelEditLotCount.Size = new System.Drawing.Size(87, 24);
            this.ucLabelEditLotCount.TabIndex = 53;
            this.ucLabelEditLotCount.TabNext = false;
            this.ucLabelEditLotCount.Tag = "";
            this.ucLabelEditLotCount.Value = "";
            this.ucLabelEditLotCount.WidthType = UserControl.WidthTypes.Tiny;
            this.ucLabelEditLotCount.XAlign = 260;
            // 
            // ucLabelEditBZCount
            // 
            this.ucLabelEditBZCount.AllowEditOnlyChecked = true;
            this.ucLabelEditBZCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ucLabelEditBZCount.AutoSelectAll = false;
            this.ucLabelEditBZCount.AutoUpper = true;
            this.ucLabelEditBZCount.Caption = "最小包装数量";
            this.ucLabelEditBZCount.Checked = false;
            this.ucLabelEditBZCount.EditType = UserControl.EditTypes.Number;
            this.ucLabelEditBZCount.Location = new System.Drawing.Point(40, 234);
            this.ucLabelEditBZCount.MaxLength = 8;
            this.ucLabelEditBZCount.Multiline = false;
            this.ucLabelEditBZCount.Name = "ucLabelEditBZCount";
            this.ucLabelEditBZCount.PasswordChar = '\0';
            this.ucLabelEditBZCount.ReadOnly = false;
            this.ucLabelEditBZCount.ShowCheckBox = false;
            this.ucLabelEditBZCount.Size = new System.Drawing.Size(135, 24);
            this.ucLabelEditBZCount.TabIndex = 52;
            this.ucLabelEditBZCount.TabNext = false;
            this.ucLabelEditBZCount.Tag = "";
            this.ucLabelEditBZCount.Value = "";
            this.ucLabelEditBZCount.WidthType = UserControl.WidthTypes.Tiny;
            this.ucLabelEditBZCount.XAlign = 125;
            // 
            // ultraGridFooder
            // 
            appearance4.BackColor = System.Drawing.SystemColors.Window;
            appearance4.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.ultraGridFooder.DisplayLayout.Appearance = appearance4;
            this.ultraGridFooder.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.ultraGridFooder.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance1.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance1.BorderColor = System.Drawing.SystemColors.Window;
            this.ultraGridFooder.DisplayLayout.GroupByBox.Appearance = appearance1;
            appearance2.ForeColor = System.Drawing.SystemColors.GrayText;
            this.ultraGridFooder.DisplayLayout.GroupByBox.BandLabelAppearance = appearance2;
            this.ultraGridFooder.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance3.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance3.BackColor2 = System.Drawing.SystemColors.Control;
            appearance3.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
            this.ultraGridFooder.DisplayLayout.GroupByBox.PromptAppearance = appearance3;
            this.ultraGridFooder.DisplayLayout.MaxColScrollRegions = 1;
            this.ultraGridFooder.DisplayLayout.MaxRowScrollRegions = 1;
            appearance12.BackColor = System.Drawing.SystemColors.Window;
            appearance12.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ultraGridFooder.DisplayLayout.Override.ActiveCellAppearance = appearance12;
            appearance7.BackColor = System.Drawing.SystemColors.Highlight;
            appearance7.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.ultraGridFooder.DisplayLayout.Override.ActiveRowAppearance = appearance7;
            this.ultraGridFooder.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.ultraGridFooder.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance6.BackColor = System.Drawing.SystemColors.Window;
            this.ultraGridFooder.DisplayLayout.Override.CardAreaAppearance = appearance6;
            appearance5.BorderColor = System.Drawing.Color.Silver;
            appearance5.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.ultraGridFooder.DisplayLayout.Override.CellAppearance = appearance5;
            this.ultraGridFooder.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.ultraGridFooder.DisplayLayout.Override.CellPadding = 0;
            appearance9.BackColor = System.Drawing.SystemColors.Control;
            appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance9.BorderColor = System.Drawing.SystemColors.Window;
            this.ultraGridFooder.DisplayLayout.Override.GroupByRowAppearance = appearance9;
            appearance11.TextHAlignAsString = "Left";
            this.ultraGridFooder.DisplayLayout.Override.HeaderAppearance = appearance11;
            this.ultraGridFooder.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.ultraGridFooder.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance10.BackColor = System.Drawing.SystemColors.Window;
            appearance10.BorderColor = System.Drawing.Color.Silver;
            this.ultraGridFooder.DisplayLayout.Override.RowAppearance = appearance10;
            this.ultraGridFooder.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance8.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ultraGridFooder.DisplayLayout.Override.TemplateAddRowAppearance = appearance8;
            this.ultraGridFooder.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.ultraGridFooder.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.ultraGridFooder.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ultraGridFooder.Location = new System.Drawing.Point(0, 262);
            this.ultraGridFooder.Name = "ultraGridFooder";
            this.ultraGridFooder.Size = new System.Drawing.Size(1023, 210);
            this.ultraGridFooder.TabIndex = 26;
            this.ultraGridFooder.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.ultraGridFooder_InitializeLayout);
            this.ultraGridFooder.InitializeRow += new Infragistics.Win.UltraWinGrid.InitializeRowEventHandler(this.ultraGridFooder_InitializeRow);
            // 
            // ultraGridHead
            // 
            this.ultraGridHead.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            appearance27.BackColor = System.Drawing.SystemColors.Window;
            appearance27.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.ultraGridHead.DisplayLayout.Appearance = appearance27;
            this.ultraGridHead.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.ultraGridHead.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance28.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance28.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance28.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance28.BorderColor = System.Drawing.SystemColors.Window;
            this.ultraGridHead.DisplayLayout.GroupByBox.Appearance = appearance28;
            appearance29.ForeColor = System.Drawing.SystemColors.GrayText;
            this.ultraGridHead.DisplayLayout.GroupByBox.BandLabelAppearance = appearance29;
            this.ultraGridHead.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance30.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance30.BackColor2 = System.Drawing.SystemColors.Control;
            appearance30.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance30.ForeColor = System.Drawing.SystemColors.GrayText;
            this.ultraGridHead.DisplayLayout.GroupByBox.PromptAppearance = appearance30;
            this.ultraGridHead.DisplayLayout.MaxColScrollRegions = 1;
            this.ultraGridHead.DisplayLayout.MaxRowScrollRegions = 1;
            appearance31.BackColor = System.Drawing.SystemColors.Window;
            appearance31.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ultraGridHead.DisplayLayout.Override.ActiveCellAppearance = appearance31;
            appearance32.BackColor = System.Drawing.SystemColors.Highlight;
            appearance32.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.ultraGridHead.DisplayLayout.Override.ActiveRowAppearance = appearance32;
            this.ultraGridHead.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.ultraGridHead.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance33.BackColor = System.Drawing.SystemColors.Window;
            this.ultraGridHead.DisplayLayout.Override.CardAreaAppearance = appearance33;
            appearance34.BorderColor = System.Drawing.Color.Silver;
            appearance34.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.ultraGridHead.DisplayLayout.Override.CellAppearance = appearance34;
            this.ultraGridHead.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.ultraGridHead.DisplayLayout.Override.CellPadding = 0;
            appearance35.BackColor = System.Drawing.SystemColors.Control;
            appearance35.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance35.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance35.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance35.BorderColor = System.Drawing.SystemColors.Window;
            this.ultraGridHead.DisplayLayout.Override.GroupByRowAppearance = appearance35;
            appearance36.TextHAlignAsString = "Left";
            this.ultraGridHead.DisplayLayout.Override.HeaderAppearance = appearance36;
            this.ultraGridHead.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.ultraGridHead.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance37.BackColor = System.Drawing.SystemColors.Window;
            appearance37.BorderColor = System.Drawing.Color.Silver;
            this.ultraGridHead.DisplayLayout.Override.RowAppearance = appearance37;
            this.ultraGridHead.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance38.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ultraGridHead.DisplayLayout.Override.TemplateAddRowAppearance = appearance38;
            this.ultraGridHead.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.ultraGridHead.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.ultraGridHead.Location = new System.Drawing.Point(0, 0);
            this.ultraGridHead.Name = "ultraGridHead";
            this.ultraGridHead.Size = new System.Drawing.Size(1023, 228);
            this.ultraGridHead.TabIndex = 25;
            this.ultraGridHead.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.ultraGridHead_InitializeLayout);
            this.ultraGridHead.CellChange += new Infragistics.Win.UltraWinGrid.CellEventHandler(this.ultraGridHead_CellChange);
            // 
            // ucButton_OpenLot
            // 
            this.ucButton_OpenLot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ucButton_OpenLot.BackColor = System.Drawing.SystemColors.Control;
            this.ucButton_OpenLot.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButton_OpenLot.BackgroundImage")));
            this.ucButton_OpenLot.ButtonType = UserControl.ButtonTypes.None;
            this.ucButton_OpenLot.Caption = "分批";
            this.ucButton_OpenLot.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButton_OpenLot.Location = new System.Drawing.Point(330, 234);
            this.ucButton_OpenLot.Name = "ucButton_OpenLot";
            this.ucButton_OpenLot.Size = new System.Drawing.Size(88, 22);
            this.ucButton_OpenLot.TabIndex = 22;
            this.ucButton_OpenLot.Click += new System.EventHandler(this.ucButton_OpenLot_Click);
            // 
            // ucButton_MerageLot
            // 
            this.ucButton_MerageLot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ucButton_MerageLot.BackColor = System.Drawing.SystemColors.Control;
            this.ucButton_MerageLot.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButton_MerageLot.BackgroundImage")));
            this.ucButton_MerageLot.ButtonType = UserControl.ButtonTypes.None;
            this.ucButton_MerageLot.Caption = "合批";
            this.ucButton_MerageLot.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButton_MerageLot.Location = new System.Drawing.Point(514, 234);
            this.ucButton_MerageLot.Name = "ucButton_MerageLot";
            this.ucButton_MerageLot.Size = new System.Drawing.Size(88, 22);
            this.ucButton_MerageLot.TabIndex = 21;
            this.ucButton_MerageLot.Click += new System.EventHandler(this.ucButton_MerageLot_Click);
            // 
            // FLotMaintain
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1023, 571);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "FLotMaintain";
            this.Text = "分批合批";
            this.Load += new System.EventHandler(this.FLotMaintain_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridFooder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridHead)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        #region 初始化Grid样式
        private void ultraGridHead_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            // 自适应列宽
            e.Layout.AutoFitColumns = false;
            e.Layout.Override.AllowColSizing = AllowColSizing.Free;
            e.Layout.ScrollBounds = ScrollBounds.ScrollToFill;

            //e.Layout.MaxBandDepth = 1;
            // 自动判断是否显示前面的+、-号
            e.Layout.Override.ExpansionIndicator = ShowExpansionIndicator.CheckOnDisplay;

            // 设置Grid的Split窗口个数，建议设置为1--不允许Split
            e.Layout.MaxColScrollRegions = 1;
            e.Layout.MaxRowScrollRegions = 1;

            e.Layout.Override.MaxSelectedRows = 2;
            // 冻结列
            e.Layout.UseFixedHeaders = true;
            e.Layout.Override.FixedHeaderAppearance.BackColor = Color.LightYellow;
            e.Layout.Override.FixedHeaderAppearance.ForeColor = Color.Blue;
            e.Layout.Override.FixedCellAppearance.BackColor = Color.LightYellow;
            e.Layout.Override.FixedCellAppearance.ForeColor = Color.Blue;
            e.Layout.Override.FixedHeaderIndicator = FixedHeaderIndicator.None;

            // 排序
            //e.Layout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;

            // 不允许删除
            e.Layout.Override.AllowDelete = DefaultableBoolean.False;

            // 滚动提示
            e.Layout.Bands[0].ScrollTipField = "LotNO";

            // 设置列宽和列名称
            e.Layout.Bands[0].Columns["Checked"].Header.Caption = "";
            e.Layout.Bands[0].Columns["LotNo"].Header.Caption = "批号";
            e.Layout.Bands[0].Columns["MCode"].Header.Caption = "物料编码";
            e.Layout.Bands[0].Columns["MDesc"].Header.Caption = "物料编码+描述";
            e.Layout.Bands[0].Columns["Active"].Header.Caption = "状态";
            e.Layout.Bands[0].Columns["VenderCode"].Header.Caption = "供应商代码";
            e.Layout.Bands[0].Columns["VenderName"].Header.Caption = "供应商代码+描述";
            e.Layout.Bands[0].Columns["VenderLotNO"].Header.Caption = "供应商批号";
            e.Layout.Bands[0].Columns["DateCode"].Header.Caption = "生产日期";
            e.Layout.Bands[0].Columns["LotQty"].Header.Caption = "批内数量";
            e.Layout.Bands[0].Columns["StorageID"].Header.Caption = "库别";
            e.Layout.Bands[0].Columns["StackCode"].Header.Caption = "库位";
            e.Layout.Bands[0].Columns["OrgID"].Header.Caption = "组织代码";

            e.Layout.Bands[0].Columns["VenderCode"].Hidden = true;
            e.Layout.Bands[0].Columns["OrgID"].Hidden = true;
            e.Layout.Bands[0].Columns["MCODE"].Hidden = true;

            e.Layout.Bands[0].Columns["Checked"].Width = 40;
            e.Layout.Bands[0].Columns["LotNo"].Width = 200;
            e.Layout.Bands[0].Columns["MCode"].Width = 60;
            e.Layout.Bands[0].Columns["MDesc"].Width = 150;
            e.Layout.Bands[0].Columns["Active"].Width = 60;
            e.Layout.Bands[0].Columns["VenderCode"].Width = 60;
            e.Layout.Bands[0].Columns["VenderName"].Width = 160;
            e.Layout.Bands[0].Columns["VenderLotNO"].Width = 60;
            e.Layout.Bands[0].Columns["DateCode"].Width = 100;
            e.Layout.Bands[0].Columns["LotQty"].Width = 100;

            e.Layout.Bands[0].Columns["StorageID"].Width = 100;
            e.Layout.Bands[0].Columns["StackCode"].Width = 100;
            e.Layout.Bands[0].Columns["OrgID"].Width = 100;

            // 设置栏位是否允许编辑，及栏位的显示形式
            e.Layout.Bands[0].Columns["Checked"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;
            e.Layout.Bands[0].Columns["LotNo"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
            e.Layout.Bands[0].Columns["LotQty"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[0].Columns["Active"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[0].Columns["DateCode"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[0].Columns["VenderLotNO"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[0].Columns["VenderName"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[0].Columns["StorageID"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[0].Columns["StackCode"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[0].Columns["MDesc"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;


            //e.Layout.Bands[0].Columns["lastPrintDate"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Date;
            //e.Layout.Bands[0].Columns["lastPrintTime"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Date;
            //e.Layout.Bands[0].Columns["DateCode"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Date;
            //设置可编辑列的颜色
            //e.Layout.Bands[0].Columns["DATECODE"].CellAppearance.BackColor = Color.LawnGreen;
            //e.Layout.Bands[0].Columns["VenderLotNO"].CellAppearance.BackColor = Color.LawnGreen ;
            //e.Layout.Bands[0].Columns["VenderITEMCODE"].CellAppearance.BackColor = Color.LawnGreen;



            // 允许筛选
            //e.Layout.Bands[0].Columns["LotNo"].AllowRowFiltering = DefaultableBoolean.True;
            e.Layout.Bands[0].Columns["LotNo"].SortIndicator = SortIndicator.Ascending;
        }

        private void ultraGridFooder_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            // 自适应列宽
            e.Layout.AutoFitColumns = false;
            e.Layout.Override.AllowColSizing = AllowColSizing.Free;
            e.Layout.ScrollBounds = ScrollBounds.ScrollToFill;

            //e.Layout.MaxBandDepth = 1;
            // 自动判断是否显示前面的+、-号
            e.Layout.Override.ExpansionIndicator = ShowExpansionIndicator.CheckOnDisplay;

            // 设置Grid的Split窗口个数，建议设置为1--不允许Split
            e.Layout.MaxColScrollRegions = 1;
            e.Layout.MaxRowScrollRegions = 1;

            // 冻结列
            e.Layout.UseFixedHeaders = true;
            e.Layout.Override.FixedHeaderAppearance.BackColor = Color.LightYellow;
            e.Layout.Override.FixedHeaderAppearance.ForeColor = Color.Blue;
            e.Layout.Override.FixedCellAppearance.BackColor = Color.LightYellow;
            e.Layout.Override.FixedCellAppearance.ForeColor = Color.Blue;
            e.Layout.Override.FixedHeaderIndicator = FixedHeaderIndicator.None;

            // 排序
            //e.Layout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;

            // 不允许删除
            e.Layout.Override.AllowDelete = DefaultableBoolean.False;

            // 滚动提示
            e.Layout.Bands[0].ScrollTipField = "LotNO";

            // 设置列宽和列名称
            e.Layout.Bands[0].Columns["Checked"].Header.Caption = "";
            e.Layout.Bands[0].Columns["LotNo"].Header.Caption = "批号";
            e.Layout.Bands[0].Columns["MCode"].Header.Caption = "物料编码";
            e.Layout.Bands[0].Columns["MDesc"].Header.Caption = "物料编码+描述";
            e.Layout.Bands[0].Columns["Active"].Header.Caption = "状态";
            e.Layout.Bands[0].Columns["VenderCode"].Header.Caption = "供应商代码";
            e.Layout.Bands[0].Columns["VenderName"].Header.Caption = "供应商代码+描述";
            e.Layout.Bands[0].Columns["VenderLotNO"].Header.Caption = "供应商批号";
            e.Layout.Bands[0].Columns["DateCode"].Header.Caption = "生产日期";
            e.Layout.Bands[0].Columns["LotQty"].Header.Caption = "批内数量";
            e.Layout.Bands[0].Columns["StorageID"].Header.Caption = "库别";
            e.Layout.Bands[0].Columns["StackCode"].Header.Caption = "库位";
            e.Layout.Bands[0].Columns["OrgID"].Header.Caption = "组织代码";

            e.Layout.Bands[0].Columns["VenderCode"].Hidden = true;
            e.Layout.Bands[0].Columns["OrgID"].Hidden = true;
            e.Layout.Bands[0].Columns["MCODE"].Hidden = true;

            e.Layout.Bands[0].Columns["Checked"].Width = 40;
            e.Layout.Bands[0].Columns["LotNo"].Width = 200;
            e.Layout.Bands[0].Columns["MCode"].Width = 60;
            e.Layout.Bands[0].Columns["MDesc"].Width = 150;
            e.Layout.Bands[0].Columns["Active"].Width = 60;
            e.Layout.Bands[0].Columns["VenderCode"].Width = 60;
            e.Layout.Bands[0].Columns["VenderName"].Width = 160;
            e.Layout.Bands[0].Columns["VenderLotNO"].Width = 60;
            e.Layout.Bands[0].Columns["DateCode"].Width = 100;
            e.Layout.Bands[0].Columns["LotQty"].Width = 100;

            e.Layout.Bands[0].Columns["StorageID"].Width = 100;
            e.Layout.Bands[0].Columns["StackCode"].Width = 100;
            e.Layout.Bands[0].Columns["OrgID"].Width = 100;

            // 设置栏位是否允许编辑，及栏位的显示形式
            e.Layout.Bands[0].Columns["Checked"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;
            e.Layout.Bands[0].Columns["LotNo"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
            e.Layout.Bands[0].Columns["LotQty"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[0].Columns["DateCode"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[0].Columns["Active"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[0].Columns["VenderLotNO"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[0].Columns["VenderName"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[0].Columns["StorageID"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[0].Columns["StackCode"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[0].Columns["MDesc"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;


            //e.Layout.Bands[0].Columns["lastPrintDate"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Date;
            //e.Layout.Bands[0].Columns["lastPrintTime"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Date;
            //e.Layout.Bands[0].Columns["DateCode"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Date;
            //设置可编辑列的颜色
            //e.Layout.Bands[0].Columns["DATECODE"].CellAppearance.BackColor = Color.LawnGreen;
            //e.Layout.Bands[0].Columns["VenderLotNO"].CellAppearance.BackColor = Color.LawnGreen ;
            //e.Layout.Bands[0].Columns["VenderITEMCODE"].CellAppearance.BackColor = Color.LawnGreen;

            e.Layout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.Select;

            // 允许筛选
            e.Layout.Bands[0].Columns["LotNo"].AllowRowFiltering = DefaultableBoolean.True;
            //e.Layout.Bands[0].Columns["LotNo"].SortIndicator = SortIndicator.Ascending;
        }

        private void InitializeMainGrid()
        {
            this.m_CheckHeadList = new DataSet();
            this.m_CheckFooderList = new DataSet();

            this.m_LotHead = new DataTable("LotHead");
            this.m_LotFooder = new DataTable("LotFooder");

            this.m_LotHead.Columns.Add("Checked", typeof(bool));
            this.m_LotHead.Columns.Add("LotNO", typeof(string));
            this.m_LotHead.Columns.Add("MCode", typeof(string));
            this.m_LotHead.Columns.Add("MDesc", typeof(string));
            this.m_LotHead.Columns.Add("Active", typeof(string));
            this.m_LotHead.Columns.Add("VenderCode", typeof(string));
            this.m_LotHead.Columns.Add("VenderName", typeof(string));
            this.m_LotHead.Columns.Add("VenderLotNO", typeof(string));
            this.m_LotHead.Columns.Add("DateCode", typeof(string));
            this.m_LotHead.Columns.Add("LotQty", typeof(string));
            this.m_LotHead.Columns.Add("StorageID", typeof(string));
            this.m_LotHead.Columns.Add("StackCode", typeof(string));
            this.m_LotHead.Columns.Add("OrgID", typeof(string));


            this.m_LotFooder.Columns.Add("Checked", typeof(bool));
            this.m_LotFooder.Columns.Add("LotNO", typeof(string));
            this.m_LotFooder.Columns.Add("MCode", typeof(string));
            this.m_LotFooder.Columns.Add("MDesc", typeof(string));
            this.m_LotFooder.Columns.Add("Active", typeof(string));
            this.m_LotFooder.Columns.Add("VenderCode", typeof(string));
            this.m_LotFooder.Columns.Add("VenderName", typeof(string));
            this.m_LotFooder.Columns.Add("VenderLotNO", typeof(string));
            this.m_LotFooder.Columns.Add("DateCode", typeof(string));
            this.m_LotFooder.Columns.Add("LotQty", typeof(string));
            this.m_LotFooder.Columns.Add("StorageID", typeof(string));
            this.m_LotFooder.Columns.Add("StackCode", typeof(string));
            this.m_LotFooder.Columns.Add("OrgID", typeof(string));

            this.m_CheckHeadList.Tables.Add(this.m_LotHead);
            this.m_CheckFooderList.Tables.Add(this.m_LotFooder);

            this.m_CheckHeadList.AcceptChanges();
            this.m_CheckFooderList.AcceptChanges();

            this.ultraGridHead.DataSource = this.m_CheckHeadList;
            this.ultraGridFooder.DataSource = this.m_CheckFooderList;
        }

        private void ultraGridFooder_InitializeRow(object sender, InitializeRowEventArgs e)
        {
            if (type == "Open")
            {
                if (e.Row.Band.Index == 0)
                {
                    e.Row.Cells["LotQty"].Column.CellActivation = Activation.AllowEdit;
                    e.Row.Cells["LotQty"].Column.CellAppearance.BackColor = Color.LawnGreen;
                }
            }
        }


        private void ultraGridHead_CellChange(object sender, CellEventArgs e)
        {
            this.ultraGridHead.UpdateData();
            if (e.Cell.Column.Key == "Checked")
            {
                if (e.Cell.Row.Band.Index == 0) //Parent
                {
                    //for (int i = 0; i < e.Cell.Row.ChildBands[0].Rows.Count; i++)
                    //{
                    //    e.Cell.Row.ChildBands[0].Rows[i].Cells["Checked"].Value = e.Cell.Value;
                    //}
                }

                if (e.Cell.Row.Band.Index == 1) // Child
                {
                    if (Convert.ToBoolean(e.Cell.Value) == true)
                    {
                        //e.Cell.Row.ParentRow.Cells["Checked"].Value = e.Cell.Value;
                    }
                    //else
                    //{
                    //    bool needUnCheckHeader = true;
                    //    for (int i = 0; i < e.Cell.Row.ParentRow.ChildBands[0].Rows.Count; i++)
                    //    {
                    //        if (Convert.ToBoolean(e.Cell.Row.ParentRow.ChildBands[0].Rows[i].Cells["Checked"].Value) == true)
                    //        {
                    //            needUnCheckHeader = false;
                    //            break;
                    //        }
                    //    }
                    //    if (needUnCheckHeader)
                    //    {
                    //        e.Cell.Row.ParentRow.Cells["Checked"].Value = e.Cell.Value;
                    //    }
                    //}
                }
            }
            this.ultraGridHead.UpdateData();
        }
        #endregion

        private void FLotMaintain_Load(object sender, EventArgs e)
        {
            LoadPrinter();
            LoadTemplateList();

            //this.InitPageLanguage();
            //this.InitGridLanguage(this.ultraGridHead);
            //this.InitGridLanguage(this.ultraGridFooder);
        }

        #region Button Events
        //取消
        private void ucbCancel_Click(object sender, EventArgs e)
        {
            //取消:清空下面的Grid
            ClearGrid();

            //重新调用查询
            ucButtonQuery_Click(null, null);

            type = "";
            this.ucButton_OpenLot.Enabled = true;
            this.ucButton_MerageLot.Enabled = true;

        }

        //保存
        private void btnSave_Click(object sender, EventArgs e)
        {
            this.ultraGridFooder.UpdateData();

            //A：检查下面的Grid中必须有值。
            if (this.ultraGridFooder.Rows.Count < 1)
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_LOT_SELECTED_COUNT_THEN_ONE"));
                return;
            }

            //B：如果是拆批，检查数量之和必须等于原批数量。
            if (type == "")
            {
                return;
            }
            try
            {
                this.DataProvider.BeginTransaction();
                if (type == "Open")
                {
                    //检查数量是否与原来批中的数量相等
                    decimal countTemp = 0;
                    foreach (UltraGridRow row in this.ultraGridFooder.Rows)
                    {

                        if (row.Cells["LotQty"].Value.ToString().Trim() != "")
                        {
                            countTemp += decimal.Parse(row.Cells["LotQty"].Value.ToString().Trim());
                        }
                    }
                    if (qty != countTemp)
                    {
                        this.DataProvider.RollbackTransaction();
                        ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_ERROR_QTY_DIF"));
                        return;
                    }

                    //将原批数量修改（tblitemlot. LOTQTY，tblstoragelotinfo. LOTQTY）,新增数据到tblitemlot和tblstorageloginfo,并插入TBLLotChangeLOG.
                    DataRow dr = ht["old"] as DataRow;

                    DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);

                    object objOldItemLot = _InventoryFacade.GetItemLot(dr["LotNo"].ToString().Trim(), dr["MCode"].ToString().Trim());
                    //modified by klaus 20130411
                    int rowsCount = this.ultraGridFooder.Rows.Count;
                    if (objOldItemLot != null)
                    {
                        #region 更新旧的ItemLot
                        //原批处于最后一行
                        if (this.ultraGridFooder.Rows[rowsCount-1].Cells["LotQty"].Value.ToString().Trim() != "")
                        {
                            (objOldItemLot as ItemLot).Lotqty = decimal.Parse(this.ultraGridFooder.Rows[rowsCount - 1].Cells["LotQty"].Value.ToString().Trim());
                        }
                        else
                        {
                            (objOldItemLot as ItemLot).Lotqty = 0;
                        }
                        (objOldItemLot as ItemLot).MaintainUser = ApplicationService.Current().UserCode;
                        (objOldItemLot as ItemLot).MaintainDate = dbDateTime.DBDate;
                        (objOldItemLot as ItemLot).MaintainTime = dbDateTime.DBTime;

                        _InventoryFacade.UpdateItemLot((objOldItemLot as ItemLot));
                        #endregion

                        #region 插入新的ItemLot
                        //遍历ultraGridFooder（除最下面的一行外）都是新增的
                        for (int i = 0; i < rowsCount-1; i++)
                        {
                            if (decimal.Parse(this.ultraGridFooder.Rows[i].Cells["LotQty"].Text.Trim()) == 0)
                            {
                                break;
                            }
                            ItemLot itemlot = new ItemLot();
                            itemlot.Lotno = this.ultraGridFooder.Rows[i].Cells["LotNO"].Text.Trim();
                            itemlot.Mcode = this.ultraGridFooder.Rows[i].Cells["MCode"].Text.Trim();
                            itemlot.Active = this.ultraGridFooder.Rows[i].Cells["Active"].Text.Trim();
                            if (itemlot.Active == string.Empty)
                            {
                                itemlot.Active = " ";
                            }
                            itemlot.Lotqty = decimal.Parse(this.ultraGridFooder.Rows[i].Cells["LotQty"].Text.Trim());
                            itemlot.Datecode = int.Parse(this.ultraGridFooder.Rows[i].Cells["DateCode"].Value.ToString());
                            itemlot.Venderlotno = this.ultraGridFooder.Rows[i].Cells["VenderLotNO"].Text.Trim();
                            itemlot.Vendoritemcode = (objOldItemLot as ItemLot).Vendoritemcode;
                            itemlot.Printtimes = 0;
                            itemlot.Lastprintuser = " ";
                            itemlot.Lastprintdate = 0;
                            itemlot.Lastprinttime = 0;
                            itemlot.Exdate = (objOldItemLot as ItemLot).Exdate;

                            itemlot.Orgid = (objOldItemLot as ItemLot).Orgid;
                            itemlot.Transline = (objOldItemLot as ItemLot).Transline;
                            itemlot.Transno = (objOldItemLot as ItemLot).Transno;
                            itemlot.MaintainUser = ApplicationService.Current().LoginInfo.UserCode;
                            itemlot.MaintainDate = dbDateTime.DBDate;
                            itemlot.MaintainTime = dbDateTime.DBTime;
                            itemlot.Vendorcode = (objOldItemLot as ItemLot).Vendorcode;
                            _InventoryFacade.AddItemLot(itemlot);
                        }


                        #endregion

                        #region 更新tblstorageloginfo
                        string storageID = this.ultraGridFooder.Rows[rowsCount-1].Cells["StorageID"].Text.Trim();
                        string stackCode = this.ultraGridFooder.Rows[rowsCount-1].Cells["StackCode"].Text.Trim();
                        string lotNO = this.ultraGridFooder.Rows[rowsCount-1].Cells["LotNO"].Text.Trim();
                        string Mcode = this.ultraGridFooder.Rows[rowsCount-1].Cells["MCode"].Text.Trim();

                        object objStroageInfo = _InventoryFacade.GetStorageLotInfo(lotNO, storageID, stackCode, Mcode);
                        if (objStroageInfo != null)
                        {
                            (objStroageInfo as Domain.Warehouse.StorageLotInfo).Lotqty = decimal.Parse(this.ultraGridFooder.Rows[rowsCount-1].Cells["LotQty"].Text.Trim());
                            (objStroageInfo as Domain.Warehouse.StorageLotInfo).Mdate = dbDateTime.DBDate;
                            (objStroageInfo as Domain.Warehouse.StorageLotInfo).Mtime = dbDateTime.DBTime;
                            (objStroageInfo as Domain.Warehouse.StorageLotInfo).Muser = ApplicationService.Current().UserCode;

                            _InventoryFacade.UpdateStorageLotInfo(objStroageInfo as Domain.Warehouse.StorageLotInfo);
                        }

                        #endregion

                        #region 插入新tblstorageloginfo
                         //遍历ultraGridFooder（除最下面的一行外）都是新增的
                        for (int i = 0; i < rowsCount - 1; i++)
                        {
                            if (decimal.Parse(this.ultraGridFooder.Rows[i].Cells["LotQty"].Text.Trim()) == 0)
                            {
                                break;
                            }
                            Domain.Warehouse.StorageLotInfo strorageLotInfo = new BenQGuru.eMES.Domain.Warehouse.StorageLotInfo();
                            strorageLotInfo.Lotno = this.ultraGridFooder.Rows[i].Cells["LotNO"].Text.Trim();
                            strorageLotInfo.Lotqty = decimal.Parse(this.ultraGridFooder.Rows[i].Cells["LotQty"].Text.Trim());
                            strorageLotInfo.Stackcode = stackCode;
                            strorageLotInfo.Storageid = storageID;
                            strorageLotInfo.Receivedate = (objStroageInfo as Domain.Warehouse.StorageLotInfo).Receivedate;
                            strorageLotInfo.Mcode = Mcode;
                            strorageLotInfo.Mdate = dbDateTime.DBDate;
                            strorageLotInfo.Mtime = dbDateTime.DBTime;
                            strorageLotInfo.Muser = ApplicationService.Current().UserCode;
                            _InventoryFacade.AddStorageLotInfo(strorageLotInfo);
                        }

                        #endregion

                        #region 插入log日志
                        LotChangeLog lotChange = new LotChangeLog();
                        
                        lotChange.Oldlotno = this.ultraGridFooder.Rows[rowsCount - 1].Cells["LotNo"].Value.ToString().Trim();
                        for (int i = 0; i < rowsCount - 1; i++)
                        {
                            lotChange.Newlotno = this.ultraGridFooder.Rows[i].Cells["LotNo"].Value.ToString().Trim();
                            if (this.ultraGridFooder.Rows[i].Cells["LotQty"].Value.ToString().Trim() != "")
                            {
                                lotChange.Newlotqty = decimal.Parse(this.ultraGridFooder.Rows[i].Cells["LotQty"].Value.ToString().Trim());
                            }
                            else
                            {
                                lotChange.Newlotqty = 0;
                            }
                            if (this.ultraGridFooder.Rows[rowsCount - 1].Cells["LotQty"].Value.ToString().Trim() != "")
                            {
                                lotChange.Oldlotqty = decimal.Parse(this.ultraGridFooder.Rows[rowsCount - 1].Cells["LotQty"].Value.ToString().Trim());
                            }
                            else
                            {
                                lotChange.Oldlotqty = 0;
                            }
                            lotChange.Chgtype = "Split";
                            lotChange.Muser = ApplicationService.Current().UserCode;
                            lotChange.Mdate = dbDateTime.DBDate;
                            lotChange.Mtime = dbDateTime.DBTime;

                            _InventoryFacade.AddLotChangeLog(lotChange);
                        }



                        #endregion
                     //end
                    }

                }
                else if (type == "Merage")
                {
                    //将原来的数据删除
                    DataRow oldDr1 = ht["old1"] as DataRow;
                    object objItemLot1 = _InventoryFacade.GetItemLot(oldDr1["LotNo"].ToString(), oldDr1["MCode"].ToString());

                    DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);

                    #region 将旧的ItemLot删除,旧的StorageLotInfo删除

                    string Lotno = this.ultraGridFooder.Rows[0].Cells["LotNO"].Text.Trim();
                    string Mcode = this.ultraGridFooder.Rows[0].Cells["MCode"].Text.Trim();
                    string storageID = this.ultraGridFooder.Rows[0].Cells["StorageID"].Text.Trim();
                    string stackCode = this.ultraGridFooder.Rows[0].Cells["StackCode"].Text.Trim();

                    object objItemLotOld = _InventoryFacade.GetItemLot(Lotno, Mcode);
                    if (objItemLotOld != null)
                    {
                        _InventoryFacade.DeleteItemLot(objItemLotOld as ItemLot);
                    }

                    object objStorageOld = _InventoryFacade.GetStorageLotInfo(Lotno, storageID, stackCode, Mcode);
                    if (objStorageOld != null)
                    {
                        _InventoryFacade.DeleteStorageLotInfo(objStorageOld as Domain.Warehouse.StorageLotInfo);
                    }

                    string LotnoTwo = this.ultraGridFooder.Rows[1].Cells["LotNO"].Text.Trim();
                    string McodeTwo = this.ultraGridFooder.Rows[1].Cells["MCode"].Text.Trim();
                    string storageIDTwo = this.ultraGridFooder.Rows[1].Cells["StorageID"].Text.Trim();
                    string stackCodeTwo = this.ultraGridFooder.Rows[1].Cells["StackCode"].Text.Trim();


                    object objItemLotTwo = _InventoryFacade.GetItemLot(LotnoTwo, McodeTwo);
                    if (objItemLotTwo != null)
                    {
                        _InventoryFacade.DeleteItemLot(objItemLotTwo as ItemLot);
                    }

                    objStorageOld = _InventoryFacade.GetStorageLotInfo(LotnoTwo, storageIDTwo, stackCodeTwo, McodeTwo);
                    if (objStorageOld != null)
                    {
                        _InventoryFacade.DeleteStorageLotInfo(objStorageOld as Domain.Warehouse.StorageLotInfo);
                    }

                    #endregion

                    #region 新增的ItemLot

                    ItemLot itemlot = new ItemLot();
                    itemlot.Lotno = this.ultraGridFooder.Rows[2].Cells["LotNO"].Text.Trim();
                    itemlot.Mcode = this.ultraGridFooder.Rows[2].Cells["MCode"].Text.Trim();
                    itemlot.Active = this.ultraGridFooder.Rows[2].Cells["Active"].Text.Trim();
                    if (itemlot.Active == string.Empty)
                    {
                        itemlot.Active = " ";
                    }
                    itemlot.Lotqty = decimal.Parse(this.ultraGridFooder.Rows[2].Cells["LotQty"].Text.Trim());
                    itemlot.Datecode = int.Parse(this.ultraGridFooder.Rows[2].Cells["DateCode"].Value.ToString());
                    itemlot.Venderlotno = this.ultraGridFooder.Rows[2].Cells["VenderLotNO"].Text.Trim();
                    itemlot.Vendoritemcode = (objItemLotOld as ItemLot).Vendoritemcode;
                    itemlot.Printtimes = 0;
                    itemlot.Lastprintuser = " ";
                    itemlot.Lastprintdate = 0;
                    itemlot.Lastprinttime = 0;

                    if ((objItemLotOld as ItemLot).Exdate <= (objItemLotTwo as ItemLot).Exdate)
                    {
                        itemlot.Exdate = (objItemLotOld as ItemLot).Exdate;
                    }
                    else
                    {
                        itemlot.Exdate = (objItemLotTwo as ItemLot).Exdate;
                    }

                    itemlot.Orgid = (objItemLotOld as ItemLot).Orgid;
                    itemlot.Transline = (objItemLotOld as ItemLot).Transline;
                    itemlot.Transno = (objItemLotOld as ItemLot).Transno;
                    itemlot.MaintainUser = ApplicationService.Current().LoginInfo.UserCode;
                    itemlot.MaintainDate = dbDateTime.DBDate;
                    itemlot.MaintainTime = dbDateTime.DBTime;
                    itemlot.Vendorcode = (objItemLotOld as ItemLot).Vendorcode;
                    _InventoryFacade.AddItemLot(itemlot);

                    #endregion

                    #region 新增的StorageLotInfo

                    Domain.Warehouse.StorageLotInfo strorageLotInfo = new BenQGuru.eMES.Domain.Warehouse.StorageLotInfo();
                    strorageLotInfo.Lotno = this.ultraGridFooder.Rows[2].Cells["LotNO"].Text.Trim();
                    strorageLotInfo.Lotqty = decimal.Parse(this.ultraGridFooder.Rows[2].Cells["LotQty"].Text.Trim());
                    strorageLotInfo.Stackcode = stackCode;
                    strorageLotInfo.Storageid = storageID;
                    strorageLotInfo.Receivedate = (objStorageOld as Domain.Warehouse.StorageLotInfo).Receivedate;
                    strorageLotInfo.Mcode = Mcode;
                    strorageLotInfo.Mdate = dbDateTime.DBDate;
                    strorageLotInfo.Mtime = dbDateTime.DBTime;
                    strorageLotInfo.Muser = ApplicationService.Current().UserCode;
                    _InventoryFacade.AddStorageLotInfo(strorageLotInfo);
                    #endregion

                    #region 插入log日志
                    LotChangeLog lotChange = new LotChangeLog();
                    lotChange.Newlotno = this.ultraGridFooder.Rows[2].Cells["LotNo"].Value.ToString().Trim();
                    lotChange.Oldlotno = this.ultraGridFooder.Rows[0].Cells["LotNo"].Value.ToString().Trim();
                    if (this.ultraGridFooder.Rows[2].Cells["LotQty"].Value.ToString().Trim() != "")
                    {
                        lotChange.Newlotqty = decimal.Parse(this.ultraGridFooder.Rows[2].Cells["LotQty"].Value.ToString().Trim());
                    }
                    else
                    {
                        lotChange.Newlotqty = 0;
                    }

                    if (this.ultraGridFooder.Rows[0].Cells["LotQty"].Value.ToString().Trim() != "")
                    {
                        lotChange.Oldlotqty = decimal.Parse(this.ultraGridFooder.Rows[0].Cells["LotQty"].Value.ToString().Trim());
                    }
                    else
                    {
                        lotChange.Oldlotqty = 0;
                    }
                    lotChange.Chgtype = "Merage";
                    lotChange.Muser = ApplicationService.Current().UserCode;
                    lotChange.Mdate = dbDateTime.DBDate;
                    lotChange.Mtime = dbDateTime.DBTime;
                    _InventoryFacade.AddLotChangeLog(lotChange);

                    lotChange = new LotChangeLog();
                    lotChange.Newlotno = this.ultraGridFooder.Rows[2].Cells["LotNo"].Value.ToString().Trim();
                    lotChange.Oldlotno = this.ultraGridFooder.Rows[1].Cells["LotNo"].Value.ToString().Trim();
                    if (this.ultraGridFooder.Rows[2].Cells["LotQty"].Value.ToString().Trim() != "")
                    {
                        lotChange.Newlotqty = decimal.Parse(this.ultraGridFooder.Rows[2].Cells["LotQty"].Value.ToString().Trim());
                    }
                    else
                    {
                        lotChange.Newlotqty = 0;
                    }

                    if (this.ultraGridFooder.Rows[1].Cells["LotQty"].Value.ToString().Trim() != "")
                    {
                        lotChange.Oldlotqty = decimal.Parse(this.ultraGridFooder.Rows[1].Cells["LotQty"].Value.ToString().Trim());
                    }
                    else
                    {
                        lotChange.Oldlotqty = 0;
                    }
                    lotChange.Chgtype = "Merage";
                    lotChange.Muser = ApplicationService.Current().UserCode;
                    lotChange.Mdate = dbDateTime.DBDate;
                    lotChange.Mtime = dbDateTime.DBTime;
                    _InventoryFacade.AddLotChangeLog(lotChange);

                    #endregion
                }

                this.DataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_Save_Lot_Error"));
                return;

            }

            //E：提示保存成功。
            if (type == "Merage")
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Success, "$CS_Merage_Lot_SUCCESS"));
            }
            if (type == "Open")
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Success, "$CS_Split_Lot_SUCCESS"));
            }
            //F：清空下面的Grid，重新调用查询。
            this.ucbCancel_Click(null, null);

        }

        private void MCodeSelector_MCodeSelectedEvent(object sender, ParentChildRelateEventArgs<string> e)
        {
            this.uclMaterialCode.Value = e.CustomObject;
        }

        //选择物料代码
        private void button_MaterialCode_Click(object sender, EventArgs e)
        {
            FMCodeQuery fMCodeQuery = new FMCodeQuery();
            fMCodeQuery.Owner = this;
            fMCodeQuery.StartPosition = FormStartPosition.CenterScreen;
            fMCodeQuery.MCodeSelectedEvent += new ParentChildRelateEventHandler<ParentChildRelateEventArgs<string>>(MCodeSelector_MCodeSelectedEvent);
            fMCodeQuery.ShowDialog();
            fMCodeQuery = null;
        }

        private void VeenderCodeSelector_VeenderCodeSelectedEvent(object sender, ParentChildRelateEventArgs<string> e)
        {
            this.uclVeenderCode.Value = e.CustomObject;
        }

        //选择供应商代码
        private void button_VeenderCode_Click(object sender, EventArgs e)
        {
            FVendorCodeQuery fVendorCodeQuery = new FVendorCodeQuery();
            fVendorCodeQuery.Owner = this;
            fVendorCodeQuery.StartPosition = FormStartPosition.CenterScreen;
            fVendorCodeQuery.BigSSCodeSelectedEvent += new ParentChildRelateEventHandler<ParentChildRelateEventArgs<string>>(VeenderCodeSelector_VeenderCodeSelectedEvent);
            fVendorCodeQuery.ShowDialog();
            fVendorCodeQuery = null;
        }

        //查询
        private void ucButtonQuery_Click(object sender, EventArgs e)
        {
            //取消:清空下面的Grid
            ClearGrid();

            string materialLotNo = this.uclMaterialLotNo.Value.Trim();
            string materialCodes = this.uclMaterialCode.Value.Trim();
            string veendorCodes = this.uclVeenderCode.Value.Trim();

            if (materialLotNo == "" && materialCodes == "" && veendorCodes == "")
            {
                this.ShowMessage(new UserControl.Message(MessageType.Error, "$At_Least_One_Conditon_Not_NULL"));
                return;
            }

            if (_InventoryFacade == null)
            {
                _InventoryFacade = new InventoryFacade(this.DataProvider);
            }

            object[] objs = _InventoryFacade.QueryItemLotForQuery(materialLotNo, materialCodes, veendorCodes);

            if (objs != null)
            {
                foreach (ItemLotForQuery query in objs)
                {
                    DataRow dr = this.m_LotHead.NewRow();
                    dr["Checked"] = false;
                    dr["LotNO"] = query.Lotno;
                    dr["MCode"] = query.Mcode;
                    dr["MDesc"] = query.Mcode + "-" + query.MaterialDescription;
                    dr["Active"] = query.Active;
                    dr["VenderName"] = query.Vendorcode + "-" + query.VendorName;
                    dr["VenderCode"] = query.Vendorcode;
                    dr["VenderLotNO"] = query.Venderlotno;
                    dr["DateCode"] = query.Datecode;
                    dr["LotQty"] = Convert.ToInt32(query.Lotqty);
                    dr["StorageID"] = query.StorageID;
                    dr["StackCode"] = query.StackCode;
                    this.m_LotHead.Rows.Add(dr);
                }
                this.m_CheckHeadList.AcceptChanges();
                this.ultraGridHead.DataSource = this.m_CheckHeadList;   
            }
            this.ucLabelEditBZCount.Value = "1";
            this.ucLabelEditLotCount.Value = "1";
        }

        //合批
        private void ucButton_MerageLot_Click(object sender, EventArgs e)
        {
            //A：检查下面的Grid必须没有数据，否则提示：请将前一笔处理保存或取消。
            //先判断下面Grid中有没有数据
            if (ultraGridFooder.Rows.Count > 0)
            {
                this.ShowMessage(new UserControl.Message(MessageType.Error, "$CS_Please_Save_Pre_Recard"));
                return;
            }

            //判断是否选择了两笔数据
            if (!CheckSelectTwoLot())
            {
                return;
            }

            //B：检查上面的Grid必须选择两笔数据，两笔数据的物料料号和供应商必须一致,库别必须一致。
            if (!IsLotRowsSame())
            {
                return;
            }

            //设置Type =Merage
            type = "Merage";


            //获取勾选的行            
            List<UltraGridRow> list = this.GetLotNoRows();

            //先进行序列号转化
            string newLotNo = cmdCalCard(list[0].Cells["MCode"].Value.ToString(), GlobalVariables.CurrentOrganizations.First().OrganizationID, list[0].Cells["LotNO"].Value.ToString());


            #region C：合并逻辑：
            Copy(newLotNo, list);


            #endregion

            type = "Merage";
            this.ucButton_OpenLot.Enabled = false;
        }

        //拆批
        private void ucButton_OpenLot_Click(object sender, EventArgs e)
        {
            if (this.radioButtonBz.Checked)
            {
                ucLabelEditBZCount_TxtboxKeyPress(null, new KeyPressEventArgs((char)Keys.Enter));
            }
            else if (this.radioButtonLotNum.Checked)
            {
                ucLabelEditLotCount_TxtboxKeyPress(null, new KeyPressEventArgs((char)Keys.Enter));
            }
            return;
            //added by Gawain@20120708,for 分批回车根据批次数量或者批量来进行分批。代替原来的分为两批。

            //先判断下面Grid中有没有数据
            if (ultraGridFooder.Rows.Count > 0)
            {
                this.ShowMessage(new UserControl.Message(MessageType.Error, "$CS_Please_Save_Pre_Recard"));
                return;
            }

            //上面Grid中只能选择一笔数据
            if (!CheckSelectOnlyOneLot())
            {
                return;
            }

            #region 拆解逻辑

            //设置Type =Open
            type = "Open";
            //获取勾选的行            
            Infragistics.Win.UltraWinGrid.UltraGridRow row = this.GetLotNoRow();

            //先进行序列号转化
            string newLotNo = cmdCalCard(row.Cells["MCode"].Value.ToString(), GlobalVariables.CurrentOrganizations.First().OrganizationID, row.Cells["LotNO"].Value.ToString());

            //qty 设置数量
            if (row.Cells["LotQty"].ToString().Trim() != "")
            {
                qty = decimal.Parse(row.Cells["LotQty"].Value.ToString().Trim());
            }

            // 1：原批号原信息复制一笔在下面的Grid中。
            // 2：新增加一个批号：生成新的批号
            // 3：新批的信息等同于原批，数量为0。
            Copy(newLotNo, row);

            // 4：拆批时：下面Grid中的数量可以被修改。
            #endregion


            this.ucButton_MerageLot.Enabled = false;

        }

        //add by klaus 20130410 输入最小包装数量。回车进行拆批
        private void ucLabelEditBZCount_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                //total表示当前选中的行的lotqty的数量
                double total = 0;
                //设置Type =Open
                type = "Open";

                //获取勾选的行            
                Infragistics.Win.UltraWinGrid.UltraGridRow row = this.GetLotNoRow();

                //先进行序列号转化
                //string newLotNo = cmdCalCard(row.Cells["MCode"].Value.ToString(), GlobalVariables.CurrentOrganizations.First().OrganizationID, row.Cells["LotNO"].Value.ToString());
                //if (row == null)
                //{
                //    return;
                //}

                //上面Grid中只能选择一笔数据
                if (!CheckSelectOnlyOneLot())
                {
                    return;
                }

                //qty 设置数量
                if (row.Cells["LotQty"].ToString().Trim() != "")
                {
                    qty = decimal.Parse(row.Cells["LotQty"].Value.ToString().Trim());
                    total = Convert.ToDouble(row.Cells["LotQty"].Value.ToString().Trim());
                }
                //先判断下面Grid中有没有数据
                if (ultraGridFooder.Rows.Count > 0)
                {
                    this.ShowMessage(new UserControl.Message(MessageType.Error, "$CS_Please_Save_Pre_Recard"));
                    this.ucButtonQuery.Focus();
                    return;
                }

                string strCount=this.ucLabelEditBZCount.Value.Trim();
                if ( strCount== string.Empty)
                {
                    //请输入最小包装数量
                    this.ShowMessage(new UserControl.Message(MessageType.Error, "$CS_Please_Input_BZCount"));
                    this.ucLabelEditBZCount.TextFocus(false, true);
                    return;     
                }

                double bzCount=0;

                if (!double.TryParse(strCount, out bzCount))
                {
                    //输入的最小包装数量必须是整数，请重新输入
                    this.ShowMessage(new UserControl.Message(MessageType.Error, "$CS_Please_BZCount_Nunber"));
                    this.ucLabelEditBZCount.TextFocus(false, true);
                    return;   
                }

                //如果最小包装数量小于等于0或者大于等于LotQTY,返回
                if (bzCount <= 0 || bzCount > total)
                {
                    //输入的最小包装数量不能大于批内总数量，请重新输入
                    this.ShowMessage(new UserControl.Message(MessageType.Error, "$CS_BZCount_OverFlow"));
                    this.ucLabelEditBZCount.TextFocus(false, true);
                    return;   
                }

                #region 拆解逻辑
                //按照最小包装数量进行分批。
                //增加多个连续的批号
               
                string lotNo = row.Cells["LotNO"].Value.ToString();
                if (this.m_LotFooder != null)
                {
                    this.m_LotFooder.Rows.Clear();
                }
                while (total > 0)
                {
                    if (total > bzCount)
                    {
                        CopyByBZCount(bzCount, true,lotNo,row);
                    }
                    else
                    {
                        CopyByBZCount(Convert.ToDouble(total.ToString("0.00")),false,lotNo,row);
                    }
                    total = total - bzCount;
                }
                //拆批时下面的Grid的数量可以被修改
                #endregion

                this.ultraGridFooder.DataSource = this.m_CheckFooderList;
                this.ultraGridFooder.UpdateData();
                this.ucButton_MerageLot.Enabled = false;
            }


        }
        //end
        //add by klaus 20130411 输入批数量，回车就行拆批
        private void ucLabelEditLotCount_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                //total表示当前选中的行的lotqty的数量
                int total = 0;
                //设置Type =Open
                type = "Open";

                //获取勾选的行            
                Infragistics.Win.UltraWinGrid.UltraGridRow row = this.GetLotNoRow();

                //先进行序列号转化
                //string newLotNo = cmdCalCard(row.Cells["MCode"].Value.ToString(), GlobalVariables.CurrentOrganizations.First().OrganizationID, row.Cells["LotNO"].Value.ToString());
                //if (row == null)
                //{
                //    return;
                //}
                //上面Grid中只能选择一笔数据
                if (!CheckSelectOnlyOneLot())
                {
                    return;
                }


                //qty 设置数量
                if (row.Cells["LotQty"].ToString().Trim() != "")
                {
                    qty = decimal.Parse(row.Cells["LotQty"].Value.ToString().Trim());
                    total = Convert.ToInt32(row.Cells["LotQty"].Value.ToString().Trim());
                }
                //先判断下面Grid中有没有数据
                if (ultraGridFooder.Rows.Count > 0)
                {
                    this.ShowMessage(new UserControl.Message(MessageType.Error, "$CS_Please_Save_Pre_Recard"));
                    this.ucButtonQuery.Focus();
                    return;
                }

                string strCount = this.ucLabelEditLotCount.Value.Trim();
                if (strCount == string.Empty)
                {
                    //请输入分批的批数
                    this.ShowMessage(new UserControl.Message(MessageType.Error, "$CS_Please_Input_LotCount"));
                    this.ucLabelEditLotCount.TextFocus(false, true);
                    return;
                }

                int lotCount = 0;

                if (!int.TryParse(strCount, out lotCount))
                {
                    //输入的分批批数必须是整数，请重新输入
                    this.ShowMessage(new UserControl.Message(MessageType.Error, "$CS_Please_LotCount_Nunber"));
                    this.ucLabelEditLotCount.TextFocus(false, true);
                    return;
                }

                //如果分批批数量小于等于0或者大于等于LotQTY,返回
                if (lotCount <= 0 || lotCount > total)
                {
                    //输入的分批批数不能大于批内总数量，请重新输入
                    this.ShowMessage(new UserControl.Message(MessageType.Error, "$CS_Please_LotCount_OverFlow"));
                    this.ucLabelEditLotCount.TextFocus(false, true);
                    return;
                }


                #region 拆解逻辑
                //按照批的数量进行分批。
                //增加多个连续的批号
                //result表示每批的数量
                //remainder表示余数，即放入原批的数量
                int result=0;
                int remainder=0;
                
                if (total > 0)
                {
                    result = total / lotCount;
                    remainder = total % lotCount;
                }
                string lotNo = row.Cells["LotNO"].Value.ToString();
                if (this.m_LotFooder != null)
                {
                    this.m_LotFooder.Rows.Clear();
                }
                for (int i = 0; i < lotCount; i++)
                {
                    CopyByBZCount(result, true, lotNo, row); 
                }
                //最后的余数，放入原批，位于最下面
                CopyByBZCount(remainder, false, lotNo, row);  
  
                #endregion
                this.ultraGridFooder.DataSource = this.m_CheckFooderList;
                this.ultraGridFooder.UpdateData();
                this.ucButton_MerageLot.Enabled = false;
            }
        }
        //add by klaus 
        //按照最小包装数量进行分批到下面的Grid中
        private void CopyByBZCount(int bzCount,bool flag,string lotNo,Infragistics.Win.UltraWinGrid.UltraGridRow row)
        {
            string lotNoNew = string.Empty;
            if (ht.Count > 0)
            {
                ht.Clear();
            }
            //flag=true 表示为新增的
            if (flag == true)
            {
                lotNoNew = cmdCalCard(row.Cells["MCode"].Value.ToString(),
                        GlobalVariables.CurrentOrganizations.First().OrganizationID, row.Cells["LotNO"].Value.ToString());
            }
            else
            {
                lotNoNew = lotNo; 
            }

            DataRow dr = this.m_LotFooder.NewRow();

            dr = this.m_LotFooder.NewRow();
            dr["Checked"] = false;
            dr["LotNo"] = lotNoNew;
            dr["MCode"] = row.Cells["MCode"].Value.ToString();
            dr["MDesc"] = row.Cells["MDesc"].Value.ToString();
            dr["Active"] = row.Cells["Active"].Value.ToString();
            dr["VenderCode"] = row.Cells["VenderCode"].Value.ToString();
            dr["VenderName"] = row.Cells["VenderName"].Value.ToString();
            dr["VenderLotNO"] = row.Cells["VenderLotNO"].Value.ToString();
            dr["DateCode"] = row.Cells["DateCode"].Value.ToString();
            dr["LotQty"] = bzCount;
            dr["StorageID"] = row.Cells["StorageID"].Value.ToString();
            dr["StackCode"] = row.Cells["StackCode"].Value.ToString();
            this.m_LotFooder.Rows.Add(dr);
            this.m_LotFooder.AcceptChanges();
            if (flag == true)
            {
                ht.Add("new", dr);
            }
            else
            {
                ht.Add("old", dr);
            }
            

            this.m_CheckFooderList.AcceptChanges();

        }

        private void CopyByBZCount(double bzCount, bool flag, string lotNo, Infragistics.Win.UltraWinGrid.UltraGridRow row)
        {
            string lotNoNew = string.Empty;
            if (ht.Count > 0)
            {
                ht.Clear();
            }
            //flag=true 表示为新增的
            if (flag == true)
            {
                lotNoNew = cmdCalCard(row.Cells["MCode"].Value.ToString(),
                        GlobalVariables.CurrentOrganizations.First().OrganizationID, row.Cells["LotNO"].Value.ToString());
            }
            else
            {
                lotNoNew = lotNo;
            }

            DataRow dr = this.m_LotFooder.NewRow();

            dr = this.m_LotFooder.NewRow();
            dr["Checked"] = false;
            dr["LotNo"] = lotNoNew;
            dr["MCode"] = row.Cells["MCode"].Value.ToString();
            dr["MDesc"] = row.Cells["MDesc"].Value.ToString();
            dr["Active"] = row.Cells["Active"].Value.ToString();
            dr["VenderCode"] = row.Cells["VenderCode"].Value.ToString();
            dr["VenderName"] = row.Cells["VenderName"].Value.ToString();
            dr["VenderLotNO"] = row.Cells["VenderLotNO"].Value.ToString();
            dr["DateCode"] = row.Cells["DateCode"].Value.ToString();
            dr["LotQty"] = bzCount;
            dr["StorageID"] = row.Cells["StorageID"].Value.ToString();
            dr["StackCode"] = row.Cells["StackCode"].Value.ToString();
            this.m_LotFooder.Rows.Add(dr);
            this.m_LotFooder.AcceptChanges();
            if (flag == true)
            {
                ht.Add("new", dr);
            }
            else
            {
                ht.Add("old", dr);
            }


            this.m_CheckFooderList.AcceptChanges();

        }

        //end

        //原批号信息放在最下面，新产生的批号放上面
        private void Copy(string lotNo, Infragistics.Win.UltraWinGrid.UltraGridRow row)
        {
            if (this.m_LotFooder != null)
            {
                this.m_LotFooder.Rows.Clear();
            }

            if (ht.Count > 0)
            {
                ht.Clear();
            }
            //新的批号放上面，旧的批号放最下面
            DataRow dr = this.m_LotFooder.NewRow(); 
            dr["Checked"] = false;
            dr["LotNo"] = lotNo;
            dr["MCode"] = row.Cells["MCode"].Value.ToString();
            dr["MDesc"] = row.Cells["MDesc"].Value.ToString();
            dr["Active"] = row.Cells["Active"].Value.ToString();
            dr["VenderCode"] = row.Cells["VenderCode"].Value.ToString();
            dr["VenderName"] = row.Cells["VenderName"].Value.ToString();
            dr["VenderLotNO"] = row.Cells["VenderLotNO"].Value.ToString();
            dr["DateCode"] = row.Cells["DateCode"].Value.ToString();
            dr["LotQty"] = 0 + "";
            dr["StorageID"] = row.Cells["StorageID"].Value.ToString();
            dr["StackCode"] = row.Cells["StackCode"].Value.ToString();
            this.m_LotFooder.Rows.Add(dr);

            ht.Add("new", dr);

            dr = this.m_LotFooder.NewRow();
            dr["Checked"] = false;
            dr["LotNo"] = row.Cells["LotNo"].Value.ToString();
            dr["MCode"] = row.Cells["MCode"].Value.ToString();
            dr["MDesc"] = row.Cells["MDesc"].Value.ToString();
            dr["Active"] = row.Cells["Active"].Value.ToString();
            dr["VenderCode"] = row.Cells["VenderCode"].Value.ToString();
            dr["VenderName"] = row.Cells["VenderName"].Value.ToString();
            dr["VenderLotNO"] = row.Cells["VenderLotNO"].Value.ToString();
            dr["DateCode"] = row.Cells["DateCode"].Value.ToString();
            dr["LotQty"] = row.Cells["LotQty"].Value.ToString();
            dr["StorageID"] = row.Cells["StorageID"].Value.ToString();
            dr["StackCode"] = row.Cells["StackCode"].Value.ToString();
            this.m_LotFooder.Rows.Add(dr);

            ht.Add("old", dr);



            this.m_CheckFooderList.AcceptChanges();
            this.ultraGridFooder.DataSource = this.m_CheckFooderList;
        }

        //原批号原信息复制一笔在下面的Grid中。
        private void Copy(string lotNo, List<UltraGridRow> list)
        {
            if (this.m_LotFooder != null)
            {
                this.m_LotFooder.Rows.Clear();
            }

            if (ht.Count > 0)
            {
                ht.Clear();
            }

            DataRow dr;
            for (int i = 0; i < list.Count; i++)
            {
                dr = this.m_LotFooder.NewRow();
                dr["Checked"] = false;
                dr["LotNo"] = list[i].Cells["LotNo"].Value.ToString();
                dr["MCode"] = list[i].Cells["MCode"].Value.ToString();
                dr["MDesc"] = list[i].Cells["MDesc"].Value.ToString();
                dr["Active"] = list[i].Cells["Active"].Value.ToString();
                dr["VenderCode"] = list[i].Cells["VenderCode"].Value.ToString();
                dr["VenderName"] = list[i].Cells["VenderName"].Value.ToString();
                dr["VenderLotNO"] = list[i].Cells["VenderLotNO"].Value.ToString();
                dr["DateCode"] = list[i].Cells["DateCode"].Value.ToString();
                dr["LotQty"] = list[i].Cells["LotQty"].Value.ToString();
                dr["StorageID"] = list[i].Cells["StorageID"].Value.ToString();
                dr["StackCode"] = list[i].Cells["StackCode"].Value.ToString();
                this.m_LotFooder.Rows.Add(dr);

                ht.Add("old" + i, dr);
            }

            dr = this.m_LotFooder.NewRow();
            dr["Checked"] = false;
            if (list[0].Cells["LotQty"].ToString().Trim() != "" && list[1].Cells["LotQty"].ToString().Trim() != "")
            {
                dr["LotQty"] = decimal.Parse(list[0].Cells["LotQty"].Value.ToString().Trim()) + decimal.Parse(list[1].Cells["LotQty"].Value.ToString().Trim());
            }
            else
            {
                dr["LotQty"] = 0 + "";
            }
            dr["LotNo"] = lotNo;
            dr["MCode"] = list[0].Cells["MCode"].Value.ToString();
            dr["MDesc"] = list[0].Cells["MDesc"].Value.ToString();
            dr["Active"] = list[0].Cells["Active"].Value.ToString();
            dr["VenderCode"] = list[0].Cells["VenderCode"].Value.ToString();
            dr["VenderName"] = list[0].Cells["VenderName"].Value.ToString();
            dr["VenderLotNO"] = list[0].Cells["VenderLotNO"].Value.ToString();
            dr["DateCode"] = list[0].Cells["DateCode"].Value.ToString();

            dr["StorageID"] = list[0].Cells["StorageID"].Value.ToString();
            dr["StackCode"] = list[0].Cells["StackCode"].Value.ToString();
            this.m_LotFooder.Rows.Add(dr);

            ht.Add("new", dr);

            this.m_CheckFooderList.AcceptChanges();
            this.ultraGridFooder.DataSource = this.m_CheckFooderList;
        }

        //获取Grid上选中的某个批号
        private UltraGridRow GetLotNoRow()
        {
            for (int i = 0; i < this.ultraGridHead.Rows.Count; i++)
            {
                Infragistics.Win.UltraWinGrid.UltraGridRow row = ultraGridHead.Rows[i];
                if (row.Band.Index == 0)
                {
                    if (Convert.ToBoolean(row.Cells["Checked"].Value) == true)
                    {
                        return row;
                    }
                }
            }
            return null;
        }

        //检查Grid上是否唯一勾选了某个批次
        private bool CheckSelectOnlyOneLot()
        {
            int tempCount = 0;
            for (int i = 0; i < this.ultraGridHead.Rows.Count; i++)
            {
                Infragistics.Win.UltraWinGrid.UltraGridRow row = ultraGridHead.Rows[i];
                if (row.Band.Index == 0)
                {
                    if (Convert.ToBoolean(row.Cells["Checked"].Value) == true)
                    {
                        tempCount++;
                    }
                }
            }
            if (tempCount > 1)
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_LOT_SELECTED_COUNT_THEN_ONE"));
                this.ucButtonQuery.Focus();
                return false;
            }

            if (tempCount < 1)
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_LOT_NOT_SELECTED"));
                this.ucButtonQuery.Focus();
                return false;
            }
            return true;
        }

        //检查Grid上是否唯一勾选了两个批次
        private bool CheckSelectTwoLot()
        {
            int tempCount = 0;
            for (int i = 0; i < this.ultraGridHead.Rows.Count; i++)
            {
                Infragistics.Win.UltraWinGrid.UltraGridRow row = ultraGridHead.Rows[i];
                if (row.Band.Index == 0)
                {
                    if (Convert.ToBoolean(row.Cells["Checked"].Value) == true)
                    {
                        tempCount++;
                    }
                }
            }
            if (tempCount > 2)
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_LOT_SELECTED_COUNT_THEN_TWO"));
                return false;
            }

            if (tempCount < 2)
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_LOT_SELECTED_COUNT_LESS_TWO"));
                return false;
            }
            return true;
        }

        //获取Grid上选中的两个批号
        private List<UltraGridRow> GetLotNoRows()
        {
            List<UltraGridRow> list = new List<UltraGridRow>();
            for (int i = 0; i < this.ultraGridHead.Rows.Count; i++)
            {
                Infragistics.Win.UltraWinGrid.UltraGridRow row = ultraGridHead.Rows[i];
                if (row.Band.Index == 0)
                {
                    if (Convert.ToBoolean(row.Cells["Checked"].Value) == true)
                    {
                        list.Add(row);
                    }
                }
            }
            return list;
        }

        //两笔数据的物料料号和供应商必须一致,库别必须一致
        private bool IsLotRowsSame()
        {
            List<UltraGridRow> list = this.GetLotNoRows();

            Infragistics.Win.UltraWinGrid.UltraGridRow row1 = list[0];
            Infragistics.Win.UltraWinGrid.UltraGridRow row2 = list[1];

            if (row1.Cells["MCode"].Value.ToString() != row2.Cells["MCode"].Value.ToString())
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_MCode_MUST_BE_SAME"));
                return false;
            }
            if (row1.Cells["VenderCode"].Value.ToString() != row2.Cells["VenderCode"].Value.ToString())
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_VenderCode_MUST_BE_SAME"));
                return false;
            }
            if (row1.Cells["StorageID"].Value.ToString() != row2.Cells["StorageID"].Value.ToString())
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_StorageID_MUST_BE_SAME"));
                return false;
            }

            return true;

        }

        //判断是否选择了要打印的数据
        private bool CheckISSelectRow()
        {
            bool flag = false;
            //先判断Grid中的数据是否重复
            for (int i = 0; i < this.ultraGridFooder.Rows.Count; i++)
            {
                Infragistics.Win.UltraWinGrid.UltraGridRow row = ultraGridFooder.Rows[i];
                if (Convert.ToBoolean(row.Cells["Checked"].Value) == true)
                {
                    flag = true;
                }
            }
            //先判断Grid中的数据是否重复
            for (int i = 0; i < this.ultraGridHead.Rows.Count; i++)
            {
                Infragistics.Win.UltraWinGrid.UltraGridRow row = ultraGridHead.Rows[i];
                if (Convert.ToBoolean(row.Cells["Checked"].Value) == true)
                {
                    flag = true;
                }
            }
            return flag;
        }

        //打印
        private void ucButtonPrint_Click(object sender, EventArgs e)
        {
            if (!CheckISSelectRow())
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_NO_ROW_SELECTED"));
                return;
            }

            //获取打印模板的路径
            if (this.ucLabelComboxPrintTemplete.SelectedIndex < 0)
            {
                this.ShowMessage("$Error_NO_TempLeteSelect");
                return;
            }
            string filePath = ((PrintTemplate)this.ucLabelComboxPrintTemplete.SelectedItemValue).TemplatePath.ToString();

            if (!filePath.ToUpper().Contains(".LAB"))
            {
                this.ShowMessage("$Error_LAB_File_Select!");
                return;
            }

            printRcardList();
        }

        #endregion

        //清空Grid
        private void ClearGrid()
        {
            if (this.m_CheckHeadList == null)
            {
                return;
            }
            if (this.m_CheckFooderList == null)
            {
                return;
            }
            this.m_CheckHeadList.Tables["LotHead"].Rows.Clear();
            this.m_CheckHeadList.Tables["LotHead"].AcceptChanges();
            this.m_CheckFooderList.Tables["LotFooder"].Rows.Clear();
            this.m_CheckFooderList.Tables["LotFooder"].AcceptChanges();

            this.m_CheckHeadList.AcceptChanges();
            this.m_CheckFooderList.AcceptChanges();

            this.ultraGridHead.DataSource = this.m_CheckHeadList;
            this.ultraGridFooder.DataSource = this.m_CheckFooderList;
        }

        #region 打印

        //打印机列表
        private void LoadPrinter()
        {
            this.ucLabelComboxPrintList.Clear();

            // Added By hi1/Venus.Feng on 20081127 for Hisense Version : Check Printers
            if (System.Drawing.Printing.PrinterSettings.InstalledPrinters == null ||
                System.Drawing.Printing.PrinterSettings.InstalledPrinters.Count == 0)
            {
                this.ShowMessage(new UserControl.Message(MessageType.Error, "$CS_PleaseInstallPrinter"));

                return;
            }
            // End Added

            int defaultprinter = 0;
            for (int i = 0; i < System.Drawing.Printing.PrinterSettings.InstalledPrinters.Count; i++)
            {
                this.ucLabelComboxPrintList.AddItem(System.Drawing.Printing.PrinterSettings.InstalledPrinters[i], System.Drawing.Printing.PrinterSettings.InstalledPrinters[i]);
                System.Drawing.Printing.PrinterSettings pts = new System.Drawing.Printing.PrinterSettings();
                pts.PrinterName = System.Drawing.Printing.PrinterSettings.InstalledPrinters[i];
                if (pts.IsDefaultPrinter)
                {
                    defaultprinter = i;
                }
            }
            this.ucLabelComboxPrintList.SelectedIndex = defaultprinter;
        }

        //打印模板
        private void LoadTemplateList()
        {

            this.ucLabelComboxPrintTemplete.Clear();

            object[] objs = this.LoadTemplateListDataSource();
            if (objs == null)
            {
                this.ShowMessage("$CS_No_Data_To_Display");
                return;
            }

            _PrintTemplateList = new PrintTemplate[objs.Length];

            for (int i = 0; i < objs.Length; i++)
            {
                _PrintTemplateList[i] = (PrintTemplate)objs[i];

                ucLabelComboxPrintTemplete.AddItem(_PrintTemplateList[i].TemplateName, _PrintTemplateList[i]);

            }
        }

        //打印模板数据源
        private object[] LoadTemplateListDataSource()
        {
            try
            {
                PrintTemplateFacade printTemplateFacade = new PrintTemplateFacade(this.DataProvider);
                return printTemplateFacade.QueryPrintTemplate(string.Empty, string.Empty, int.MinValue, int.MaxValue);
            }
            catch (Exception ex)
            {
                this.ShowMessage(ex);
            }

            return null;
        }

        //打印前检查数据
        private bool ValidateInput(string printer, PrintTemplate printTemplate)
        {
            ////序列号

            if (this.ucLabelEditPrintCount.Value.Trim() == "")
            {
                this.ShowMessage(new UserControl.Message(MessageType.Error, "$Error_Print_Count_Empty"));
                return false;
            }

            //模板
            if (printTemplate == null)
            {
                this.ShowMessage(new UserControl.Message(MessageType.Error, "$Error_PrintTemplate_Empty"));
                return false;
            }

            //打印机

            if (printer == null || printer.Trim().Length <= 0)
            {
                this.ShowMessage(new UserControl.Message(MessageType.Error, "$Error_Printer_Empty"));
                return false;
            }

            return true;
        }

        //条码打印方法
        private void printRcardList()
        {
            try
            {
                //Added By Hi1/Venus.Feng on 20081127 for Hisense : Check Printers
                if (this.ucLabelComboxPrintList.ComboBoxData.Items.Count == 0)
                {
                    this.ShowMessage(new UserControl.Message(MessageType.Error, "$CS_PleaseInstallPrinter"));
                    return;
                }
                //ENd Added

                SetPrintButtonStatus(false);

                PrintTemplateFacade printTemplateFacade = new PrintTemplateFacade(this.DataProvider);
                MOFacade moFacade = new MOFacade(this.DataProvider);

                string printer = this.ucLabelComboxPrintList.SelectedItemText;

                PrintTemplate printTemplate = (PrintTemplate)this.ucLabelComboxPrintTemplete.SelectedItemValue;
                printTemplate = (PrintTemplate)printTemplateFacade.GetPrintTemplate(printTemplate.TemplateName);

                if (!System.IO.Path.IsPathRooted(printTemplate.TemplatePath))
                {
                    string ExePath = Application.StartupPath;
                    string SimplyPath = printTemplate.TemplatePath;
                    int PathIndex = SimplyPath.IndexOf("\\");
                    SimplyPath = SimplyPath.Substring(PathIndex);
                    printTemplate.TemplatePath = ExePath + SimplyPath;
                }

                List<Domain.Warehouse.ItemLot> itemLotList = new List<Domain.Warehouse.ItemLot>();

                for (int i = 0; i < ultraGridFooder.Rows.Count; i++)
                {
                    Infragistics.Win.UltraWinGrid.UltraGridRow row = ultraGridFooder.Rows[i];

                    if (row.Band.Index == 0)
                    {
                        if (Convert.ToBoolean(row.Cells["Checked"].Value) == true)
                        {
                            object obj = this.GetEditItemLotObject(row);
                            itemLotList.Add(obj as ItemLot);
                        }
                    }
                }
                //for (int i = 0; i < ultraGridHead.Rows.Count; i++)
                //{
                //    Infragistics.Win.UltraWinGrid.UltraGridRow row = ultraGridHead.Rows[i];

                //    if (row.Band.Index == 0)
                //    {
                //        if (Convert.ToBoolean(row.Cells["Checked"].Value) == true)
                //        {
                //            object obj = this.GetEditItemLotObject(row);
                //            itemLotList.Add(obj as ItemLot);
                //        }
                //    }
                //}

                if (itemLotList.Count == 0)
                {
                    return;
                }

                if (!ValidateInput(printer, printTemplate))
                {
                    return;
                }

                for (int i = 0; i < int.Parse(this.ucLabelEditPrintCount.Value.Trim()); i++)
                {
                    Messages msg = this.Print(printer, printTemplate.TemplatePath, itemLotList);

                    this.ShowMessage(msg);
                }
            }
            finally
            {
                SetPrintButtonStatus(true);
            }
        }

        //打印
        public UserControl.Messages Print(string printer, string templatePath, List<ItemLot> itemLotList)
        {
            UserControl.Messages messages = new UserControl.Messages();
            //CodeSoftPrintData _CodeSoftPrintData = new CodeSoftPrintData();
            CodeSoftFacade _CodeSoftFacade = new CodeSoftFacade();

            CodeSoftPrintFacade _CodeSoftPrintFacade = new CodeSoftPrintFacade(this.DataProvider);
            try
            {
                try
                {
                    _CodeSoftPrintFacade.PrePrint();
                    _CodeSoftFacade.OpenTemplate(printer, templatePath);
                }
                catch (System.Exception ex)
                {
                    messages.Add(new UserControl.Message(UserControl.MessageType.Error, ex.Message));
                    return messages;
                }

                //批量打印前生成文本文件

                string strBatchDataFile = string.Empty;
                if (_IsBatchPrint)
                {
                    strBatchDataFile = _CodeSoftPrintFacade.CreateFile();
                }

                if (_ItemFacade == null)
                {
                    _ItemFacade = new ItemFacade(this.DataProvider);
                }

                for (int i = 0; i < itemLotList.Count; i++)
                {
                    Domain.Warehouse.ItemLot itemlot = (Domain.Warehouse.ItemLot)itemLotList[i];

                    string machineType = string.Empty;
                    string materialName = string.Empty;

                    object objMaterial = _ItemFacade.GetMaterial(itemlot.Mcode, itemlot.Orgid);

                    if (objMaterial != null)
                    {
                        machineType = (objMaterial as Domain.MOModel.Material).MaterialMachineType;
                        materialName = (objMaterial as Domain.MOModel.Material).MaterialName;
                    }

                    LabelPrintVars labelPrintVars = new LabelPrintVars();

                    string[] vars = new string[0];


                    if (messages.IsSuccess())
                    {
                        try
                        {
                            //要传给Codesoft的数组，字段顺序不能修改
                            vars = _CodeSoftPrintFacade.GetPrintVars(itemlot.Lotno, itemlot.Mcode, materialName, machineType, itemlot.Lotqty.ToString(), itemlot.Venderlotno);

                            //批量打印前的写文件

                            if (_IsBatchPrint)
                            {
                                string[] printVars = _CodeSoftPrintFacade.ProcessVars(vars, labelPrintVars);
                                _CodeSoftPrintFacade.WriteFile(strBatchDataFile, printVars);
                            }
                            //直接打印
                            else
                            {
                                _CodeSoftFacade.LabelPrintVars = labelPrintVars;
                                _CodeSoftFacade.Print(vars);
                            }
                        }
                        catch (System.Exception ex)
                        {
                            messages.Add(new UserControl.Message(UserControl.MessageType.Error, ex.Message));
                            return messages;
                        }
                    }
                }
                //批量打印
                if (_IsBatchPrint)
                {
                    try
                    {
                        _CodeSoftFacade.Print(strBatchDataFile, _CodeSoftPrintFacade.GetDataDescPath(_DataDescFileName));
                    }
                    catch (System.Exception ex)
                    {
                        messages.Add(new UserControl.Message(UserControl.MessageType.Error, ex.Message));
                        return messages;
                    }
                }

                messages.Add(new UserControl.Message(UserControl.MessageType.Success, "$Success_Print_Label"));
            }
            finally
            {
                _CodeSoftFacade.ReleaseCom();

            }

            return messages;
        }

        private void SetPrintButtonStatus(bool enabled)
        {
            this.ucButtonPrint.Enabled = enabled;

            if (enabled)
            {
                this.Cursor = System.Windows.Forms.Cursors.Arrow;
            }
            else
            {
                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            }
        }

        protected ItemLot GetEditItemLotObject(Infragistics.Win.UltraWinGrid.UltraGridRow row)
        {
            if (row == null)
            {
                return null;
            }

            object obj = _InventoryFacade.GetItemLot(row.Cells["LotNO"].Text, row.Cells["MCode"].Text);

            if (obj != null)
            {
                (obj as ItemLot).Datecode = int.Parse(row.Cells["DateCode"].Value.ToString());
                (obj as ItemLot).Venderlotno = row.Cells["VenderLotNO"].Text.Trim();
                (obj as ItemLot).Lotqty = decimal.Parse(row.Cells["LotQty"].Text.Trim());
                return (ItemLot)obj;
            }
            else
            {
                DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
                ItemLot itemlot = new ItemLot();
                itemlot.Lotno = row.Cells["LotNO"].Text.Trim();
                itemlot.Mcode = row.Cells["MCode"].Text.Trim();
                itemlot.Active = row.Cells["Active"].Text.Trim();
                itemlot.Lotqty = decimal.Parse(row.Cells["LotQty"].Text.Trim());
                itemlot.Datecode = int.Parse(row.Cells["DateCode"].Value.ToString());
                itemlot.Venderlotno = row.Cells["VenderLotNO"].Text.Trim();
                itemlot.Lastprintuser = ApplicationService.Current().UserCode;
                itemlot.Lastprintdate = dbDateTime.DBDate;
                itemlot.Lastprinttime = dbDateTime.DBTime;
                itemlot.MaintainUser = ApplicationService.Current().LoginInfo.UserCode;

                return itemlot;
            }
        }

        #endregion

        #region Base Messages
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

        #region 序列号转化
        protected string cmdCalCard(string itemCode, int orgid, string lotno)
        {
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);
            //string suffix = itemCode;
            //if (suffix.Length >= 10)
            //{
            //    suffix = itemCode.Substring(0, 10);
            //}
            string lotNoSubstring = lotno.Substring(2, lotno.Length - 5);

            ////处理日期
            //int date = dbDateTime.DBDate;

            //string year = date.ToString().Substring(0, 4);
            //string month = date.ToString().Substring(4, 2);
            //string day = date.ToString().Substring(6, 2);

            //string formatDate = year.Substring(2) + DtoX(int.Parse(month)) + day;

            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(this.DataProvider);
            }
            string maxserial = _WarehouseFacade.GetMaxSerial(lotNoSubstring);

            //如果已是最大值就返回为空
            if (maxserial == "999")
            {
                return "";
            }


            string orgId = string.Format("{0:00}", orgid);
            Domain.Warehouse.SERIALBOOK serialbook = new Domain.Warehouse.SERIALBOOK();

            if (maxserial == "")
            {
                serialbook.SNPrefix = lotNoSubstring;
                serialbook.MaxSerial = "1";
                serialbook.MDate = dbDateTime.DBDate;
                serialbook.MTime = dbDateTime.DBTime;
                serialbook.MUser = ApplicationService.Current().UserCode;
                _WarehouseFacade.AddSerialBook(serialbook);
                return lotno.Substring(0, lotno.Length - 3) + string.Format("{0:000}", int.Parse(serialbook.MaxSerial));
            }
            else
            {
                serialbook.SNPrefix = lotNoSubstring;
                serialbook.MaxSerial = (int.Parse(maxserial) + 1).ToString();
                serialbook.MDate = dbDateTime.DBDate;
                serialbook.MTime = dbDateTime.DBTime;
                serialbook.MUser = ApplicationService.Current().UserCode;
                _WarehouseFacade.UpdateSerialBook(serialbook);
                return lotno.Substring(0, lotno.Length - 3) + string.Format("{0:000}", int.Parse(serialbook.MaxSerial));
            }
        }

        //十进制转十六进制
        public string DtoX(int d)
        {
            string x = "";
            if (d < 16)
            {
                x = chang(d);
            }
            else
            {
                int c;

                int s = 0;
                int n = d;
                int temp = d;
                while (n >= 16)
                {
                    s++;
                    n = n / 16;
                }
                string[] m = new string[s];
                int i = 0;
                do
                {
                    c = d / 16;
                    m[i++] = chang(d % 16);//判断是否大于10，如果大于10，则转换为A~F的格式
                    d = c;
                } while (c >= 16);
                x = chang(d);
                for (int j = m.Length - 1; j >= 0; j--)
                {
                    x += m[j];
                }
            }
            return x;
        }

        //判断是否为10~15之间的数，如果是则进行转换
        public string chang(int d)
        {
            string x = "";
            switch (d)
            {
                case 10:
                    x = "A";
                    break;
                case 11:
                    x = "B";
                    break;
                case 12:
                    x = "C";
                    break;
                case 13:
                    x = "D";
                    break;
                case 14:
                    x = "E";
                    break;
                case 15:
                    x = "F";
                    break;
                default:
                    x = d.ToString();
                    break;
            }
            return x;
        }
        #endregion


        private void radioButtonLotNum_CheckedChanged(object sender, EventArgs e)
        {
            this.ucLabelEditLotCount.Enabled = this.radioButtonLotNum.Checked;
            if (this.radioButtonLotNum.Checked)
            {
                this.ucLabelEditLotCount.Focus();
                this.ucLabelEditLotCount.SelectAll();
            }
        }

        private void radioButtonBz_CheckedChanged(object sender, EventArgs e)
        {
            this.ucLabelEditBZCount.Enabled = this.radioButtonBz.Checked;
            if (this.radioButtonBz.Checked)
            {
                this.ucLabelEditBZCount.Focus();
                this.ucLabelEditBZCount.SelectAll();
            }
        }

    }
}
