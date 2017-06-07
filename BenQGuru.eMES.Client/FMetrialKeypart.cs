using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using BenQGuru.eMES.CodeSoftPrint;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Domain.Material;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Domain.Warehouse;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.IQC;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Client.Service;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Domain.BaseSetting;
using UserControl;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using System.Data;

namespace BenQGuru.eMES.Client
{
    public class FKeyPart : BaseForm
    {
        private bool _IsBatchPrint = true;
        private string _DataDescFileName = "Label.dsc";
        public PrintTemplate[] _PrintTemplateList = null;
        private DataSet m_CheckList = null;
        private DataTable m_LotGroup = null;
        private DataTable m_LotDetail = null;
        private int orgid = -1;
        public static decimal lotCount = 0;

        Hashtable lotno = new Hashtable();

        #region 自动生成

        private System.Windows.Forms.GroupBox grpQuery;
        private System.Windows.Forms.Panel panelButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private UserControl.UCButton ucBtnSave;
        private UserControl.UCButton ucBtnDelete;
        private UserControl.UCButton ucBtnExit;
        private System.ComponentModel.IContainer components = null;
        private UCButton ucButtonCalcRCardEnd;
        private UCLabelEdit ucLUint;
        private UCLabelEdit ucLEVenderQuery;
        private UCLabelEdit ucLabelEditRCardQuery;
        private UCLabelEdit ucLabelEditRcardCount;
        private UCLabelEdit ucLabelEditPrintCount;
        private RadioButton radioButton_StartRcard;
        private RadioButton radioButton_Rcard;
        public UCLabelCombox ucLabelComboxPrintTemplete;
        public UCLabelCombox ucLabelComboxPrintList;
        public UCButton ucButtonPrint;
        private UCLabelEdit ucLabelEditRecNo;
        private UCLabelEdit ucLabelEditSHCount;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGridMain;
        private Button cmdRecSelect;
        private UCLabelEdit ucLabelEditItemCode;
        private UCButton ucButtonNewLot;
        private UCLabelEdit ucLERCStartRcard;
        private UCLabelEdit ucLabelEditMControlType;
        private UCLabelCombox ucLabelComboxLineNo;
        private UCLabelEdit ucLabelEditBZCount;
        private UCLabelEdit ucLabelEditMemo;
        private UCButton ucButtonRcarInput;
        private RadioButton radioButtonRCardInput;
        private UCLabelEdit ucLabelEditLotNo;
        private CheckBox checkBox_selectedALL;
        private UCButton ucButtonRcardByMo;
        private RadioButton radioButtonRCardByMo;
        private UCButton ucButtonRcardByOQC;
        private RadioButton radioButtonRCardByOQC;
        private CheckBox checkBoxRcardCheck;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FKeyPart));
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
            this.grpQuery = new System.Windows.Forms.GroupBox();
            this.ucLabelEditMemo = new UserControl.UCLabelEdit();
            this.ucLabelComboxLineNo = new UserControl.UCLabelCombox();
            this.ucLabelEditMControlType = new UserControl.UCLabelEdit();
            this.ucLabelEditItemCode = new UserControl.UCLabelEdit();
            this.cmdRecSelect = new System.Windows.Forms.Button();
            this.ucLabelEditSHCount = new UserControl.UCLabelEdit();
            this.ucLabelEditRecNo = new UserControl.UCLabelEdit();
            this.ucLUint = new UserControl.UCLabelEdit();
            this.ucLEVenderQuery = new UserControl.UCLabelEdit();
            this.panelButton = new System.Windows.Forms.Panel();
            this.ucLabelEditLotNo = new UserControl.UCLabelEdit();
            this.ucLabelEditBZCount = new UserControl.UCLabelEdit();
            this.ucButtonNewLot = new UserControl.UCButton();
            this.ucBtnSave = new UserControl.UCButton();
            this.ucBtnDelete = new UserControl.UCButton();
            this.ucBtnExit = new UserControl.UCButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ucButtonRcardByOQC = new UserControl.UCButton();
            this.radioButtonRCardByOQC = new System.Windows.Forms.RadioButton();
            this.ucButtonRcardByMo = new UserControl.UCButton();
            this.radioButtonRCardByMo = new System.Windows.Forms.RadioButton();
            this.checkBox_selectedALL = new System.Windows.Forms.CheckBox();
            this.ucButtonRcarInput = new UserControl.UCButton();
            this.radioButtonRCardInput = new System.Windows.Forms.RadioButton();
            this.ucButtonPrint = new UserControl.UCButton();
            this.radioButton_StartRcard = new System.Windows.Forms.RadioButton();
            this.radioButton_Rcard = new System.Windows.Forms.RadioButton();
            this.ucLabelComboxPrintTemplete = new UserControl.UCLabelCombox();
            this.ucLabelComboxPrintList = new UserControl.UCLabelCombox();
            this.ucLabelEditRCardQuery = new UserControl.UCLabelEdit();
            this.ucLabelEditPrintCount = new UserControl.UCLabelEdit();
            this.ucLabelEditRcardCount = new UserControl.UCLabelEdit();
            this.checkBoxRcardCheck = new System.Windows.Forms.CheckBox();
            this.ucButtonCalcRCardEnd = new UserControl.UCButton();
            this.ucLERCStartRcard = new UserControl.UCLabelEdit();
            this.ultraGridMain = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.grpQuery.SuspendLayout();
            this.panelButton.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridMain)).BeginInit();
            this.SuspendLayout();
            // 
            // grpQuery
            // 
            this.grpQuery.BackColor = System.Drawing.SystemColors.Control;
            this.grpQuery.Controls.Add(this.ucLabelEditMemo);
            this.grpQuery.Controls.Add(this.ucLabelComboxLineNo);
            this.grpQuery.Controls.Add(this.ucLabelEditMControlType);
            this.grpQuery.Controls.Add(this.ucLabelEditItemCode);
            this.grpQuery.Controls.Add(this.cmdRecSelect);
            this.grpQuery.Controls.Add(this.ucLabelEditSHCount);
            this.grpQuery.Controls.Add(this.ucLabelEditRecNo);
            this.grpQuery.Controls.Add(this.ucLUint);
            this.grpQuery.Controls.Add(this.ucLEVenderQuery);
            this.grpQuery.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpQuery.Location = new System.Drawing.Point(0, 0);
            this.grpQuery.Name = "grpQuery";
            this.grpQuery.Size = new System.Drawing.Size(918, 107);
            this.grpQuery.TabIndex = 0;
            this.grpQuery.TabStop = false;
            // 
            // ucLabelEditMemo
            // 
            this.ucLabelEditMemo.AllowEditOnlyChecked = true;
            this.ucLabelEditMemo.AutoSelectAll = false;
            this.ucLabelEditMemo.AutoUpper = true;
            this.ucLabelEditMemo.Caption = "备注";
            this.ucLabelEditMemo.Checked = false;
            this.ucLabelEditMemo.EditType = UserControl.EditTypes.String;
            this.ucLabelEditMemo.Location = new System.Drawing.Point(48, 75);
            this.ucLabelEditMemo.MaxLength = 100;
            this.ucLabelEditMemo.Multiline = false;
            this.ucLabelEditMemo.Name = "ucLabelEditMemo";
            this.ucLabelEditMemo.PasswordChar = '\0';
            this.ucLabelEditMemo.ReadOnly = true;
            this.ucLabelEditMemo.ShowCheckBox = false;
            this.ucLabelEditMemo.Size = new System.Drawing.Size(437, 24);
            this.ucLabelEditMemo.TabIndex = 57;
            this.ucLabelEditMemo.TabNext = true;
            this.ucLabelEditMemo.Value = "";
            this.ucLabelEditMemo.WidthType = UserControl.WidthTypes.TooLong;
            this.ucLabelEditMemo.XAlign = 85;
            // 
            // ucLabelComboxLineNo
            // 
            this.ucLabelComboxLineNo.AllowEditOnlyChecked = true;
            this.ucLabelComboxLineNo.Caption = "行号";
            this.ucLabelComboxLineNo.Checked = false;
            this.ucLabelComboxLineNo.Location = new System.Drawing.Point(282, 20);
            this.ucLabelComboxLineNo.Name = "ucLabelComboxLineNo";
            this.ucLabelComboxLineNo.SelectedIndex = -1;
            this.ucLabelComboxLineNo.ShowCheckBox = false;
            this.ucLabelComboxLineNo.Size = new System.Drawing.Size(137, 20);
            this.ucLabelComboxLineNo.TabIndex = 56;
            this.ucLabelComboxLineNo.WidthType = UserControl.WidthTypes.Small;
            this.ucLabelComboxLineNo.XAlign = 319;
            this.ucLabelComboxLineNo.SelectedIndexChanged += new System.EventHandler(this.ucLabelComboxLineNo_SelectedIndexChanged_1);
            // 
            // ucLabelEditMControlType
            // 
            this.ucLabelEditMControlType.AllowEditOnlyChecked = true;
            this.ucLabelEditMControlType.AutoSelectAll = false;
            this.ucLabelEditMControlType.AutoUpper = true;
            this.ucLabelEditMControlType.Caption = "物料管控类型";
            this.ucLabelEditMControlType.Checked = false;
            this.ucLabelEditMControlType.EditType = UserControl.EditTypes.Integer;
            this.ucLabelEditMControlType.Enabled = false;
            this.ucLabelEditMControlType.Location = new System.Drawing.Point(427, 48);
            this.ucLabelEditMControlType.MaxLength = 8;
            this.ucLabelEditMControlType.Multiline = false;
            this.ucLabelEditMControlType.Name = "ucLabelEditMControlType";
            this.ucLabelEditMControlType.PasswordChar = '\0';
            this.ucLabelEditMControlType.ReadOnly = false;
            this.ucLabelEditMControlType.ShowCheckBox = false;
            this.ucLabelEditMControlType.Size = new System.Drawing.Size(185, 24);
            this.ucLabelEditMControlType.TabIndex = 55;
            this.ucLabelEditMControlType.TabNext = false;
            this.ucLabelEditMControlType.Tag = "";
            this.ucLabelEditMControlType.Value = "";
            this.ucLabelEditMControlType.WidthType = UserControl.WidthTypes.Small;
            this.ucLabelEditMControlType.XAlign = 512;
            // 
            // ucLabelEditItemCode
            // 
            this.ucLabelEditItemCode.AllowEditOnlyChecked = true;
            this.ucLabelEditItemCode.AutoSelectAll = false;
            this.ucLabelEditItemCode.AutoUpper = true;
            this.ucLabelEditItemCode.Caption = "物料代码";
            this.ucLabelEditItemCode.Checked = false;
            this.ucLabelEditItemCode.EditType = UserControl.EditTypes.String;
            this.ucLabelEditItemCode.Enabled = false;
            this.ucLabelEditItemCode.Location = new System.Drawing.Point(451, 20);
            this.ucLabelEditItemCode.MaxLength = 100;
            this.ucLabelEditItemCode.Multiline = false;
            this.ucLabelEditItemCode.Name = "ucLabelEditItemCode";
            this.ucLabelEditItemCode.PasswordChar = '\0';
            this.ucLabelEditItemCode.ReadOnly = false;
            this.ucLabelEditItemCode.ShowCheckBox = false;
            this.ucLabelEditItemCode.Size = new System.Drawing.Size(461, 24);
            this.ucLabelEditItemCode.TabIndex = 54;
            this.ucLabelEditItemCode.TabNext = true;
            this.ucLabelEditItemCode.Value = "";
            this.ucLabelEditItemCode.WidthType = UserControl.WidthTypes.TooLong;
            this.ucLabelEditItemCode.XAlign = 512;
            // 
            // cmdRecSelect
            // 
            this.cmdRecSelect.Location = new System.Drawing.Point(224, 19);
            this.cmdRecSelect.Name = "cmdRecSelect";
            this.cmdRecSelect.Size = new System.Drawing.Size(34, 25);
            this.cmdRecSelect.TabIndex = 53;
            this.cmdRecSelect.Text = "...";
            this.cmdRecSelect.UseVisualStyleBackColor = true;
            this.cmdRecSelect.Click += new System.EventHandler(this.cmdRecSelect_Click);
            // 
            // ucLabelEditSHCount
            // 
            this.ucLabelEditSHCount.AllowEditOnlyChecked = true;
            this.ucLabelEditSHCount.AutoSelectAll = false;
            this.ucLabelEditSHCount.AutoUpper = true;
            this.ucLabelEditSHCount.Caption = "合格数量";
            this.ucLabelEditSHCount.Checked = false;
            this.ucLabelEditSHCount.EditType = UserControl.EditTypes.Integer;
            this.ucLabelEditSHCount.Enabled = false;
            this.ucLabelEditSHCount.Location = new System.Drawing.Point(624, 48);
            this.ucLabelEditSHCount.MaxLength = 8;
            this.ucLabelEditSHCount.Multiline = false;
            this.ucLabelEditSHCount.Name = "ucLabelEditSHCount";
            this.ucLabelEditSHCount.PasswordChar = '\0';
            this.ucLabelEditSHCount.ReadOnly = false;
            this.ucLabelEditSHCount.ShowCheckBox = false;
            this.ucLabelEditSHCount.Size = new System.Drawing.Size(161, 24);
            this.ucLabelEditSHCount.TabIndex = 52;
            this.ucLabelEditSHCount.TabNext = false;
            this.ucLabelEditSHCount.Tag = "";
            this.ucLabelEditSHCount.Value = "";
            this.ucLabelEditSHCount.WidthType = UserControl.WidthTypes.Small;
            this.ucLabelEditSHCount.XAlign = 685;
            // 
            // ucLabelEditRecNo
            // 
            this.ucLabelEditRecNo.AllowEditOnlyChecked = true;
            this.ucLabelEditRecNo.AutoSelectAll = false;
            this.ucLabelEditRecNo.AutoUpper = true;
            this.ucLabelEditRecNo.Caption = "入库单号";
            this.ucLabelEditRecNo.Checked = false;
            this.ucLabelEditRecNo.EditType = UserControl.EditTypes.String;
            this.ucLabelEditRecNo.Location = new System.Drawing.Point(24, 22);
            this.ucLabelEditRecNo.MaxLength = 40;
            this.ucLabelEditRecNo.Multiline = false;
            this.ucLabelEditRecNo.Name = "ucLabelEditRecNo";
            this.ucLabelEditRecNo.PasswordChar = '\0';
            this.ucLabelEditRecNo.ReadOnly = false;
            this.ucLabelEditRecNo.ShowCheckBox = false;
            this.ucLabelEditRecNo.Size = new System.Drawing.Size(194, 24);
            this.ucLabelEditRecNo.TabIndex = 45;
            this.ucLabelEditRecNo.TabNext = true;
            this.ucLabelEditRecNo.Value = "";
            this.ucLabelEditRecNo.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabelEditRecNo.XAlign = 85;
            this.ucLabelEditRecNo.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ucLabelEditRecNo_TxtboxKeyPress);
            this.ucLabelEditRecNo.InnerTextChanged += new System.EventHandler(this.ucLabelEditRecNo_InnerTextChanged);
            // 
            // ucLUint
            // 
            this.ucLUint.AllowEditOnlyChecked = true;
            this.ucLUint.AutoSelectAll = false;
            this.ucLUint.AutoUpper = true;
            this.ucLUint.Caption = "单位";
            this.ucLUint.Checked = false;
            this.ucLUint.EditType = UserControl.EditTypes.String;
            this.ucLUint.Enabled = false;
            this.ucLUint.Location = new System.Drawing.Point(282, 48);
            this.ucLUint.MaxLength = 40;
            this.ucLUint.Multiline = false;
            this.ucLUint.Name = "ucLUint";
            this.ucLUint.PasswordChar = '\0';
            this.ucLUint.ReadOnly = false;
            this.ucLUint.ShowCheckBox = false;
            this.ucLUint.Size = new System.Drawing.Size(137, 24);
            this.ucLUint.TabIndex = 1;
            this.ucLUint.TabNext = true;
            this.ucLUint.Value = "";
            this.ucLUint.WidthType = UserControl.WidthTypes.Small;
            this.ucLUint.XAlign = 319;
            // 
            // ucLEVenderQuery
            // 
            this.ucLEVenderQuery.AllowEditOnlyChecked = true;
            this.ucLEVenderQuery.AutoSelectAll = false;
            this.ucLEVenderQuery.AutoUpper = true;
            this.ucLEVenderQuery.Caption = "供应商代码";
            this.ucLEVenderQuery.Checked = false;
            this.ucLEVenderQuery.EditType = UserControl.EditTypes.String;
            this.ucLEVenderQuery.Enabled = false;
            this.ucLEVenderQuery.Location = new System.Drawing.Point(12, 48);
            this.ucLEVenderQuery.MaxLength = 40;
            this.ucLEVenderQuery.Multiline = false;
            this.ucLEVenderQuery.Name = "ucLEVenderQuery";
            this.ucLEVenderQuery.PasswordChar = '\0';
            this.ucLEVenderQuery.ReadOnly = false;
            this.ucLEVenderQuery.ShowCheckBox = false;
            this.ucLEVenderQuery.Size = new System.Drawing.Size(206, 24);
            this.ucLEVenderQuery.TabIndex = 0;
            this.ucLEVenderQuery.TabNext = true;
            this.ucLEVenderQuery.Value = "";
            this.ucLEVenderQuery.WidthType = UserControl.WidthTypes.Normal;
            this.ucLEVenderQuery.XAlign = 85;
            // 
            // panelButton
            // 
            this.panelButton.BackColor = System.Drawing.SystemColors.Control;
            this.panelButton.Controls.Add(this.ucLabelEditLotNo);
            this.panelButton.Controls.Add(this.ucLabelEditBZCount);
            this.panelButton.Controls.Add(this.ucButtonNewLot);
            this.panelButton.Controls.Add(this.ucBtnSave);
            this.panelButton.Controls.Add(this.ucBtnDelete);
            this.panelButton.Controls.Add(this.ucBtnExit);
            this.panelButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButton.Location = new System.Drawing.Point(0, 644);
            this.panelButton.Name = "panelButton";
            this.panelButton.Size = new System.Drawing.Size(918, 48);
            this.panelButton.TabIndex = 4;
            // 
            // ucLabelEditLotNo
            // 
            this.ucLabelEditLotNo.AllowEditOnlyChecked = true;
            this.ucLabelEditLotNo.AutoSelectAll = false;
            this.ucLabelEditLotNo.AutoUpper = true;
            this.ucLabelEditLotNo.Caption = "批号";
            this.ucLabelEditLotNo.Checked = false;
            this.ucLabelEditLotNo.EditType = UserControl.EditTypes.String;
            this.ucLabelEditLotNo.Location = new System.Drawing.Point(224, 14);
            this.ucLabelEditLotNo.MaxLength = 40;
            this.ucLabelEditLotNo.Multiline = false;
            this.ucLabelEditLotNo.Name = "ucLabelEditLotNo";
            this.ucLabelEditLotNo.PasswordChar = '\0';
            this.ucLabelEditLotNo.ReadOnly = false;
            this.ucLabelEditLotNo.ShowCheckBox = false;
            this.ucLabelEditLotNo.Size = new System.Drawing.Size(170, 24);
            this.ucLabelEditLotNo.TabIndex = 54;
            this.ucLabelEditLotNo.TabNext = false;
            this.ucLabelEditLotNo.Tag = "";
            this.ucLabelEditLotNo.Value = "";
            this.ucLabelEditLotNo.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabelEditLotNo.XAlign = 261;
            this.ucLabelEditLotNo.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ucLabelEditLotNo_TxtboxKeyPress);
            // 
            // ucLabelEditBZCount
            // 
            this.ucLabelEditBZCount.AllowEditOnlyChecked = true;
            this.ucLabelEditBZCount.AutoSelectAll = false;
            this.ucLabelEditBZCount.AutoUpper = true;
            this.ucLabelEditBZCount.Caption = "最小包装数量";
            this.ucLabelEditBZCount.Checked = false;
            this.ucLabelEditBZCount.EditType = UserControl.EditTypes.Number;
            this.ucLabelEditBZCount.Location = new System.Drawing.Point(12, 14);
            this.ucLabelEditBZCount.MaxLength = 8;
            this.ucLabelEditBZCount.Multiline = false;
            this.ucLabelEditBZCount.Name = "ucLabelEditBZCount";
            this.ucLabelEditBZCount.PasswordChar = '\0';
            this.ucLabelEditBZCount.ReadOnly = false;
            this.ucLabelEditBZCount.ShowCheckBox = false;
            this.ucLabelEditBZCount.Size = new System.Drawing.Size(185, 24);
            this.ucLabelEditBZCount.TabIndex = 53;
            this.ucLabelEditBZCount.TabNext = false;
            this.ucLabelEditBZCount.Tag = "";
            this.ucLabelEditBZCount.Value = "";
            this.ucLabelEditBZCount.WidthType = UserControl.WidthTypes.Small;
            this.ucLabelEditBZCount.XAlign = 97;
            // 
            // ucButtonNewLot
            // 
            this.ucButtonNewLot.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonNewLot.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonNewLot.BackgroundImage")));
            this.ucButtonNewLot.ButtonType = UserControl.ButtonTypes.None;
            this.ucButtonNewLot.Caption = "生成新批";
            this.ucButtonNewLot.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonNewLot.Location = new System.Drawing.Point(433, 12);
            this.ucButtonNewLot.Name = "ucButtonNewLot";
            this.ucButtonNewLot.Size = new System.Drawing.Size(88, 22);
            this.ucButtonNewLot.TabIndex = 26;
            this.ucButtonNewLot.Click += new System.EventHandler(this.ucButtonNewLot_Click);
            // 
            // ucBtnSave
            // 
            this.ucBtnSave.BackColor = System.Drawing.SystemColors.Control;
            this.ucBtnSave.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucBtnSave.BackgroundImage")));
            this.ucBtnSave.ButtonType = UserControl.ButtonTypes.Save;
            this.ucBtnSave.Caption = "保存";
            this.ucBtnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnSave.Location = new System.Drawing.Point(621, 12);
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
            this.ucBtnDelete.Location = new System.Drawing.Point(527, 12);
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
            this.ucBtnExit.Location = new System.Drawing.Point(715, 12);
            this.ucBtnExit.Name = "ucBtnExit";
            this.ucBtnExit.Size = new System.Drawing.Size(88, 22);
            this.ucBtnExit.TabIndex = 25;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox1.Controls.Add(this.ucButtonRcardByOQC);
            this.groupBox1.Controls.Add(this.radioButtonRCardByOQC);
            this.groupBox1.Controls.Add(this.ucButtonRcardByMo);
            this.groupBox1.Controls.Add(this.radioButtonRCardByMo);
            this.groupBox1.Controls.Add(this.checkBox_selectedALL);
            this.groupBox1.Controls.Add(this.ucButtonRcarInput);
            this.groupBox1.Controls.Add(this.radioButtonRCardInput);
            this.groupBox1.Controls.Add(this.ucButtonPrint);
            this.groupBox1.Controls.Add(this.radioButton_StartRcard);
            this.groupBox1.Controls.Add(this.radioButton_Rcard);
            this.groupBox1.Controls.Add(this.ucLabelComboxPrintTemplete);
            this.groupBox1.Controls.Add(this.ucLabelComboxPrintList);
            this.groupBox1.Controls.Add(this.ucLabelEditRCardQuery);
            this.groupBox1.Controls.Add(this.ucLabelEditPrintCount);
            this.groupBox1.Controls.Add(this.ucLabelEditRcardCount);
            this.groupBox1.Controls.Add(this.checkBoxRcardCheck);
            this.groupBox1.Controls.Add(this.ucButtonCalcRCardEnd);
            this.groupBox1.Controls.Add(this.ucLERCStartRcard);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 446);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(918, 198);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // ucButtonRcardByOQC
            // 
            this.ucButtonRcardByOQC.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonRcardByOQC.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonRcardByOQC.BackgroundImage")));
            this.ucButtonRcardByOQC.ButtonType = UserControl.ButtonTypes.None;
            this.ucButtonRcardByOQC.Caption = "查询序列号";
            this.ucButtonRcardByOQC.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonRcardByOQC.Location = new System.Drawing.Point(697, 135);
            this.ucButtonRcardByOQC.Name = "ucButtonRcardByOQC";
            this.ucButtonRcardByOQC.Size = new System.Drawing.Size(88, 22);
            this.ucButtonRcardByOQC.TabIndex = 61;
            this.ucButtonRcardByOQC.Click += new System.EventHandler(this.ucButtonRcardByOQC_Click);
            // 
            // radioButtonRCardByOQC
            // 
            this.radioButtonRCardByOQC.AutoSize = true;
            this.radioButtonRCardByOQC.Location = new System.Drawing.Point(562, 138);
            this.radioButtonRCardByOQC.Name = "radioButtonRCardByOQC";
            this.radioButtonRCardByOQC.Size = new System.Drawing.Size(101, 16);
            this.radioButtonRCardByOQC.TabIndex = 60;
            this.radioButtonRCardByOQC.TabStop = true;
            this.radioButtonRCardByOQC.Text = "半成品OQC导入";
            this.radioButtonRCardByOQC.UseVisualStyleBackColor = true;
            this.radioButtonRCardByOQC.CheckedChanged += new System.EventHandler(this.radioButtonRCardByOQC_CheckedChanged);
            // 
            // ucButtonRcardByMo
            // 
            this.ucButtonRcardByMo.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonRcardByMo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonRcardByMo.BackgroundImage")));
            this.ucButtonRcardByMo.ButtonType = UserControl.ButtonTypes.None;
            this.ucButtonRcardByMo.Caption = "查询序列号";
            this.ucButtonRcardByMo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonRcardByMo.Location = new System.Drawing.Point(423, 135);
            this.ucButtonRcardByMo.Name = "ucButtonRcardByMo";
            this.ucButtonRcardByMo.Size = new System.Drawing.Size(88, 22);
            this.ucButtonRcardByMo.TabIndex = 59;
            this.ucButtonRcardByMo.Click += new System.EventHandler(this.ucButtonRcardByMo_Click);
            // 
            // radioButtonRCardByMo
            // 
            this.radioButtonRCardByMo.AutoSize = true;
            this.radioButtonRCardByMo.Location = new System.Drawing.Point(288, 138);
            this.radioButtonRCardByMo.Name = "radioButtonRCardByMo";
            this.radioButtonRCardByMo.Size = new System.Drawing.Size(119, 16);
            this.radioButtonRCardByMo.TabIndex = 58;
            this.radioButtonRCardByMo.TabStop = true;
            this.radioButtonRCardByMo.Text = "内部工单序号导入";
            this.radioButtonRCardByMo.UseVisualStyleBackColor = true;
            this.radioButtonRCardByMo.CheckedChanged += new System.EventHandler(this.radioButtonRCardByMo_CheckedChanged);
            // 
            // checkBox_selectedALL
            // 
            this.checkBox_selectedALL.AutoSize = true;
            this.checkBox_selectedALL.Location = new System.Drawing.Point(12, 14);
            this.checkBox_selectedALL.Name = "checkBox_selectedALL";
            this.checkBox_selectedALL.Size = new System.Drawing.Size(48, 16);
            this.checkBox_selectedALL.TabIndex = 57;
            this.checkBox_selectedALL.Text = "全选";
            this.checkBox_selectedALL.UseVisualStyleBackColor = true;
            this.checkBox_selectedALL.CheckedChanged += new System.EventHandler(this.checkBox_selectedALL_CheckedChanged);
            // 
            // ucButtonRcarInput
            // 
            this.ucButtonRcarInput.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonRcarInput.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonRcarInput.BackgroundImage")));
            this.ucButtonRcarInput.ButtonType = UserControl.ButtonTypes.None;
            this.ucButtonRcarInput.Caption = "查询序列号";
            this.ucButtonRcarInput.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonRcarInput.Location = new System.Drawing.Point(147, 135);
            this.ucButtonRcarInput.Name = "ucButtonRcarInput";
            this.ucButtonRcarInput.Size = new System.Drawing.Size(88, 22);
            this.ucButtonRcarInput.TabIndex = 56;
            this.ucButtonRcarInput.Click += new System.EventHandler(this.ucButtonRcarInput_Click);
            // 
            // radioButtonRCardInput
            // 
            this.radioButtonRCardInput.AutoSize = true;
            this.radioButtonRCardInput.Location = new System.Drawing.Point(12, 138);
            this.radioButtonRCardInput.Name = "radioButtonRCardInput";
            this.radioButtonRCardInput.Size = new System.Drawing.Size(119, 16);
            this.radioButtonRCardInput.TabIndex = 55;
            this.radioButtonRCardInput.TabStop = true;
            this.radioButtonRCardInput.Text = "外协工单序号导入";
            this.radioButtonRCardInput.UseVisualStyleBackColor = true;
            this.radioButtonRCardInput.CheckedChanged += new System.EventHandler(this.radioButtonRCardInput_CheckedChanged);
            // 
            // ucButtonPrint
            // 
            this.ucButtonPrint.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonPrint.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonPrint.BackgroundImage")));
            this.ucButtonPrint.ButtonType = UserControl.ButtonTypes.None;
            this.ucButtonPrint.Caption = "打印";
            this.ucButtonPrint.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonPrint.Location = new System.Drawing.Point(667, 165);
            this.ucButtonPrint.Name = "ucButtonPrint";
            this.ucButtonPrint.Size = new System.Drawing.Size(88, 22);
            this.ucButtonPrint.TabIndex = 53;
            this.ucButtonPrint.Click += new System.EventHandler(this.ucButtonPrint_Click);
            // 
            // radioButton_StartRcard
            // 
            this.radioButton_StartRcard.AutoSize = true;
            this.radioButton_StartRcard.Location = new System.Drawing.Point(12, 109);
            this.radioButton_StartRcard.Name = "radioButton_StartRcard";
            this.radioButton_StartRcard.Size = new System.Drawing.Size(14, 13);
            this.radioButton_StartRcard.TabIndex = 52;
            this.radioButton_StartRcard.TabStop = true;
            this.radioButton_StartRcard.UseVisualStyleBackColor = true;
            this.radioButton_StartRcard.CheckedChanged += new System.EventHandler(this.radioButton_StartRcard_CheckedChanged);
            // 
            // radioButton_Rcard
            // 
            this.radioButton_Rcard.AutoSize = true;
            this.radioButton_Rcard.Location = new System.Drawing.Point(12, 79);
            this.radioButton_Rcard.Name = "radioButton_Rcard";
            this.radioButton_Rcard.Size = new System.Drawing.Size(14, 13);
            this.radioButton_Rcard.TabIndex = 51;
            this.radioButton_Rcard.TabStop = true;
            this.radioButton_Rcard.UseVisualStyleBackColor = true;
            this.radioButton_Rcard.CheckedChanged += new System.EventHandler(this.radioButtonRcard_CheckedChanged);
            // 
            // ucLabelComboxPrintTemplete
            // 
            this.ucLabelComboxPrintTemplete.AllowEditOnlyChecked = true;
            this.ucLabelComboxPrintTemplete.Caption = "打印模板";
            this.ucLabelComboxPrintTemplete.Checked = false;
            this.ucLabelComboxPrintTemplete.Location = new System.Drawing.Point(423, 168);
            this.ucLabelComboxPrintTemplete.Name = "ucLabelComboxPrintTemplete";
            this.ucLabelComboxPrintTemplete.SelectedIndex = -1;
            this.ucLabelComboxPrintTemplete.ShowCheckBox = false;
            this.ucLabelComboxPrintTemplete.Size = new System.Drawing.Size(194, 20);
            this.ucLabelComboxPrintTemplete.TabIndex = 49;
            this.ucLabelComboxPrintTemplete.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabelComboxPrintTemplete.XAlign = 484;
            // 
            // ucLabelComboxPrintList
            // 
            this.ucLabelComboxPrintList.AllowEditOnlyChecked = true;
            this.ucLabelComboxPrintList.Caption = "打印机列表";
            this.ucLabelComboxPrintList.Checked = false;
            this.ucLabelComboxPrintList.Location = new System.Drawing.Point(188, 168);
            this.ucLabelComboxPrintList.Name = "ucLabelComboxPrintList";
            this.ucLabelComboxPrintList.SelectedIndex = -1;
            this.ucLabelComboxPrintList.ShowCheckBox = false;
            this.ucLabelComboxPrintList.Size = new System.Drawing.Size(206, 20);
            this.ucLabelComboxPrintList.TabIndex = 50;
            this.ucLabelComboxPrintList.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabelComboxPrintList.XAlign = 261;
            // 
            // ucLabelEditRCardQuery
            // 
            this.ucLabelEditRCardQuery.AllowEditOnlyChecked = true;
            this.ucLabelEditRCardQuery.AutoSelectAll = false;
            this.ucLabelEditRCardQuery.AutoUpper = true;
            this.ucLabelEditRCardQuery.Caption = "序列号";
            this.ucLabelEditRCardQuery.Checked = false;
            this.ucLabelEditRCardQuery.EditType = UserControl.EditTypes.String;
            this.ucLabelEditRCardQuery.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ucLabelEditRCardQuery.Location = new System.Drawing.Point(55, 75);
            this.ucLabelEditRCardQuery.MaxLength = 40;
            this.ucLabelEditRCardQuery.Multiline = false;
            this.ucLabelEditRCardQuery.Name = "ucLabelEditRCardQuery";
            this.ucLabelEditRCardQuery.PasswordChar = '\0';
            this.ucLabelEditRCardQuery.ReadOnly = false;
            this.ucLabelEditRCardQuery.ShowCheckBox = false;
            this.ucLabelEditRCardQuery.Size = new System.Drawing.Size(249, 24);
            this.ucLabelEditRCardQuery.TabIndex = 2;
            this.ucLabelEditRCardQuery.TabNext = true;
            this.ucLabelEditRCardQuery.Value = "";
            this.ucLabelEditRCardQuery.WidthType = UserControl.WidthTypes.Long;
            this.ucLabelEditRCardQuery.XAlign = 104;
            this.ucLabelEditRCardQuery.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ucLabelEditRCardQuery_KeyPress);
            // 
            // ucLabelEditPrintCount
            // 
            this.ucLabelEditPrintCount.AllowEditOnlyChecked = true;
            this.ucLabelEditPrintCount.AutoSelectAll = false;
            this.ucLabelEditPrintCount.AutoUpper = true;
            this.ucLabelEditPrintCount.Caption = "打印数量";
            this.ucLabelEditPrintCount.Checked = false;
            this.ucLabelEditPrintCount.EditType = UserControl.EditTypes.Integer;
            this.ucLabelEditPrintCount.Location = new System.Drawing.Point(12, 168);
            this.ucLabelEditPrintCount.MaxLength = 8;
            this.ucLabelEditPrintCount.Multiline = false;
            this.ucLabelEditPrintCount.Name = "ucLabelEditPrintCount";
            this.ucLabelEditPrintCount.PasswordChar = '\0';
            this.ucLabelEditPrintCount.ReadOnly = false;
            this.ucLabelEditPrintCount.ShowCheckBox = false;
            this.ucLabelEditPrintCount.Size = new System.Drawing.Size(161, 24);
            this.ucLabelEditPrintCount.TabIndex = 48;
            this.ucLabelEditPrintCount.TabNext = false;
            this.ucLabelEditPrintCount.Tag = "";
            this.ucLabelEditPrintCount.Value = "1";
            this.ucLabelEditPrintCount.WidthType = UserControl.WidthTypes.Small;
            this.ucLabelEditPrintCount.XAlign = 73;
            // 
            // ucLabelEditRcardCount
            // 
            this.ucLabelEditRcardCount.AllowEditOnlyChecked = true;
            this.ucLabelEditRcardCount.AutoSelectAll = false;
            this.ucLabelEditRcardCount.AutoUpper = true;
            this.ucLabelEditRcardCount.Caption = "数量";
            this.ucLabelEditRcardCount.Checked = false;
            this.ucLabelEditRcardCount.EditType = UserControl.EditTypes.Integer;
            this.ucLabelEditRcardCount.Location = new System.Drawing.Point(333, 103);
            this.ucLabelEditRcardCount.MaxLength = 4;
            this.ucLabelEditRcardCount.Multiline = false;
            this.ucLabelEditRcardCount.Name = "ucLabelEditRcardCount";
            this.ucLabelEditRcardCount.PasswordChar = '\0';
            this.ucLabelEditRcardCount.ReadOnly = false;
            this.ucLabelEditRcardCount.ShowCheckBox = false;
            this.ucLabelEditRcardCount.Size = new System.Drawing.Size(137, 24);
            this.ucLabelEditRcardCount.TabIndex = 47;
            this.ucLabelEditRcardCount.TabNext = false;
            this.ucLabelEditRcardCount.Tag = "";
            this.ucLabelEditRcardCount.Value = "";
            this.ucLabelEditRcardCount.WidthType = UserControl.WidthTypes.Small;
            this.ucLabelEditRcardCount.XAlign = 370;
            // 
            // checkBoxRcardCheck
            // 
            this.checkBoxRcardCheck.AutoSize = true;
            this.checkBoxRcardCheck.Location = new System.Drawing.Point(12, 46);
            this.checkBoxRcardCheck.Name = "checkBoxRcardCheck";
            this.checkBoxRcardCheck.Size = new System.Drawing.Size(84, 16);
            this.checkBoxRcardCheck.TabIndex = 46;
            this.checkBoxRcardCheck.Text = "序列号检查";
            this.checkBoxRcardCheck.UseVisualStyleBackColor = true;
            // 
            // ucButtonCalcRCardEnd
            // 
            this.ucButtonCalcRCardEnd.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonCalcRCardEnd.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonCalcRCardEnd.BackgroundImage")));
            this.ucButtonCalcRCardEnd.ButtonType = UserControl.ButtonTypes.None;
            this.ucButtonCalcRCardEnd.Caption = "生成序列号";
            this.ucButtonCalcRCardEnd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonCalcRCardEnd.Location = new System.Drawing.Point(499, 105);
            this.ucButtonCalcRCardEnd.Name = "ucButtonCalcRCardEnd";
            this.ucButtonCalcRCardEnd.Size = new System.Drawing.Size(88, 22);
            this.ucButtonCalcRCardEnd.TabIndex = 45;
            this.ucButtonCalcRCardEnd.Click += new System.EventHandler(this.ucButtonCalcRCardEnd_Click);
            // 
            // ucLERCStartRcard
            // 
            this.ucLERCStartRcard.AllowEditOnlyChecked = true;
            this.ucLERCStartRcard.AutoSelectAll = false;
            this.ucLERCStartRcard.AutoUpper = true;
            this.ucLERCStartRcard.Caption = "起始序列号";
            this.ucLERCStartRcard.Checked = false;
            this.ucLERCStartRcard.EditType = UserControl.EditTypes.Integer;
            this.ucLERCStartRcard.Location = new System.Drawing.Point(31, 105);
            this.ucLERCStartRcard.MaxLength = 4;
            this.ucLERCStartRcard.Multiline = false;
            this.ucLERCStartRcard.Name = "ucLERCStartRcard";
            this.ucLERCStartRcard.PasswordChar = '\0';
            this.ucLERCStartRcard.ReadOnly = false;
            this.ucLERCStartRcard.ShowCheckBox = false;
            this.ucLERCStartRcard.Size = new System.Drawing.Size(273, 24);
            this.ucLERCStartRcard.TabIndex = 8;
            this.ucLERCStartRcard.TabNext = true;
            this.ucLERCStartRcard.Value = "";
            this.ucLERCStartRcard.WidthType = UserControl.WidthTypes.Long;
            this.ucLERCStartRcard.XAlign = 104;
            // 
            // ultraGridMain
            // 
            appearance4.BackColor = System.Drawing.SystemColors.Window;
            appearance4.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.ultraGridMain.DisplayLayout.Appearance = appearance4;
            this.ultraGridMain.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.ultraGridMain.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance1.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance1.BorderColor = System.Drawing.SystemColors.Window;
            this.ultraGridMain.DisplayLayout.GroupByBox.Appearance = appearance1;
            appearance2.ForeColor = System.Drawing.SystemColors.GrayText;
            this.ultraGridMain.DisplayLayout.GroupByBox.BandLabelAppearance = appearance2;
            this.ultraGridMain.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance3.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance3.BackColor2 = System.Drawing.SystemColors.Control;
            appearance3.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
            this.ultraGridMain.DisplayLayout.GroupByBox.PromptAppearance = appearance3;
            this.ultraGridMain.DisplayLayout.MaxColScrollRegions = 1;
            this.ultraGridMain.DisplayLayout.MaxRowScrollRegions = 1;
            appearance12.BackColor = System.Drawing.SystemColors.Window;
            appearance12.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ultraGridMain.DisplayLayout.Override.ActiveCellAppearance = appearance12;
            appearance7.BackColor = System.Drawing.SystemColors.Highlight;
            appearance7.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.ultraGridMain.DisplayLayout.Override.ActiveRowAppearance = appearance7;
            this.ultraGridMain.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.ultraGridMain.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance6.BackColor = System.Drawing.SystemColors.Window;
            this.ultraGridMain.DisplayLayout.Override.CardAreaAppearance = appearance6;
            appearance5.BorderColor = System.Drawing.Color.Silver;
            appearance5.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.ultraGridMain.DisplayLayout.Override.CellAppearance = appearance5;
            this.ultraGridMain.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.ultraGridMain.DisplayLayout.Override.CellPadding = 0;
            appearance9.BackColor = System.Drawing.SystemColors.Control;
            appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance9.BorderColor = System.Drawing.SystemColors.Window;
            this.ultraGridMain.DisplayLayout.Override.GroupByRowAppearance = appearance9;
            appearance11.TextHAlignAsString = "Left";
            this.ultraGridMain.DisplayLayout.Override.HeaderAppearance = appearance11;
            this.ultraGridMain.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.ultraGridMain.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance10.BackColor = System.Drawing.SystemColors.Window;
            appearance10.BorderColor = System.Drawing.Color.Silver;
            this.ultraGridMain.DisplayLayout.Override.RowAppearance = appearance10;
            this.ultraGridMain.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance8.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ultraGridMain.DisplayLayout.Override.TemplateAddRowAppearance = appearance8;
            this.ultraGridMain.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.ultraGridMain.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.ultraGridMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGridMain.Location = new System.Drawing.Point(0, 107);
            this.ultraGridMain.Name = "ultraGridMain";
            this.ultraGridMain.Size = new System.Drawing.Size(918, 339);
            this.ultraGridMain.TabIndex = 5;
            this.ultraGridMain.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.ultraGridMain_InitializeLayout);
            this.ultraGridMain.InitializeRow += new Infragistics.Win.UltraWinGrid.InitializeRowEventHandler(this.ultraGridMain_InitializeRow);
            this.ultraGridMain.CellChange += new Infragistics.Win.UltraWinGrid.CellEventHandler(this.ultraGridMain_CellChange);
            // 
            // FKeyPart
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(918, 692);
            this.Controls.Add(this.ultraGridMain);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panelButton);
            this.Controls.Add(this.grpQuery);
            this.Name = "FKeyPart";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "物料追溯信息维护";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FKeyPart_FormClosed);
            this.Load += new System.EventHandler(this.FKeyPart_Load);
            this.grpQuery.ResumeLayout(false);
            this.panelButton.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridMain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        #region Form Base
        private WarehouseFacade warehouseFacade = null;
        private ItemFacade _ItemFacade = null;
        private IQCFacade _IQCFacade = null;
        private InventoryFacade _InventoryFacade = null;
        private MaterialFacade _MaterialFacade = null;
        private OPBOMFacade _OPBOMFacade = null;
        private MOFacade _MOFacade = null;
        private SBOMFacade _SBOMFacade = null;
        private IDomainDataProvider _domainDataProvider = ApplicationService.Current().DataProvider;
        private string LineNO = string.Empty;
        private string repNO = string.Empty;

        public IDomainDataProvider DataProvider
        {
            get
            {
                return _domainDataProvider;
            }
        }

        public FKeyPart()
        {
            // 该调用是 Windows 窗体设计器所必需的。
            InitializeComponent();

            // TODO: 在 InitializeComponent 调用后添加任何初始化

            //UserControl.UIStyleBuilder.GridUI(ultraGridMain);
            UserControl.UIStyleBuilder.FormUI(this);

            ultraGridMain.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Default;
            //UserControl.UIStyleBuilder.FormUI(this);
            this.ultraGridMain.UpdateMode = UpdateMode.OnCellChange;
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

            this._ItemFacade = new ItemFacade(this.DataProvider);
            this._MaterialFacade = new MaterialFacade(this.DataProvider);
            this._OPBOMFacade = new OPBOMFacade(this.DataProvider);
            this._MOFacade = new MOFacade(this.DataProvider);
            this._SBOMFacade = new SBOMFacade(this.DataProvider);
            this._InventoryFacade = new InventoryFacade(this.DataProvider);
            this.warehouseFacade = new WarehouseFacade(this.DataProvider);
        }

        public FKeyPart(String repNO, string LineNO)
        {

            // 该调用是 Windows 窗体设计器所必需的。
            InitializeComponent();

            // TODO: 在 InitializeComponent 调用后添加任何初始化

            //UserControl.UIStyleBuilder.GridUI(ultraGridMain);
            UserControl.UIStyleBuilder.FormUI(this);

            ultraGridMain.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Default;
            //UserControl.UIStyleBuilder.FormUI(this);
            this.ultraGridMain.UpdateMode = UpdateMode.OnCellChange;
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

            this._ItemFacade = new ItemFacade(this.DataProvider);
            this._MaterialFacade = new MaterialFacade(this.DataProvider);
            this._OPBOMFacade = new OPBOMFacade(this.DataProvider);
            this._MOFacade = new MOFacade(this.DataProvider);
            this._SBOMFacade = new SBOMFacade(this.DataProvider);
            this._InventoryFacade = new InventoryFacade(this.DataProvider);

            this.repNO = repNO;
            this.LineNO = LineNO;
            //this.ucLabelComboxLineNo.SetSelectItemText(LineNO);
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

        #region User Events
        private void FKeyPart_Load(object sender, System.EventArgs e)
        {
            this.ucLabelEditSHCount.InnerTextBox.ForeColor = Color.Black;
            this.ucLabelEditSHCount.InnerTextBox.TextAlign = HorizontalAlignment.Right;

            this.ucLabelEditPrintCount.InnerTextBox.ForeColor = Color.Black;
            this.ucLabelEditPrintCount.InnerTextBox.TextAlign = HorizontalAlignment.Right;

            LoadPrinter();
            LoadTemplateList();

            this.ucLabelEditRecNo.Value = repNO;

            //this.InitGridLanguage(ultraGridMain);
            //this.InitPageLanguage();
        }
        #endregion

        #region Object <--> Form

        protected ItemLot GetEditItemLotObject(Infragistics.Win.UltraWinGrid.UltraGridRow row)
        {
            if (row == null)
            {
                return null;
            }

            object obj = _InventoryFacade.GetItemLot(row.Cells["LotNO"].Text, row.Cells["MCODE"].Text);

            if (obj != null)
            {

                (obj as ItemLot).Datecode = FormatHelper.TODateInt((DateTime)row.Cells["DATECODE"].Value);
                (obj as ItemLot).Venderlotno = row.Cells["VenderLotNO"].Text.Trim();
                (obj as ItemLot).Vendoritemcode = row.Cells["VenderITEMCODE"].Text.Trim();
                (obj as ItemLot).Lotqty = decimal.Parse(row.Cells["LOTQTY"].Text.Trim());
                return (ItemLot)obj;
            }
            else
            {
                DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);

                ItemLot itemlot = new ItemLot();
                itemlot.Lotno = row.Cells["LotNO"].Text.Trim();
                itemlot.Mcode = row.Cells["MCODE"].Text.Trim();
                itemlot.Active = " ";
                itemlot.Lotqty = decimal.Parse(row.Cells["LOTQTY"].Text.Trim());
                itemlot.Datecode = FormatHelper.TODateInt((DateTime)row.Cells["DATECODE"].Value);
                itemlot.Venderlotno = row.Cells["VenderLotNO"].Text.Trim();
                itemlot.Vendoritemcode = row.Cells["VenderITEMCODE"].Text.Trim();
                itemlot.Printtimes = int.Parse(row.Cells["PrintTimes"].Text.Trim());
                if (row.Cells["lastPrintUSER"].Text.Trim() == "")
                {
                    itemlot.Lastprintuser = " ";
                }
                else
                {
                    itemlot.Lastprintuser = row.Cells["lastPrintUSER"].Text.Trim();
                }
                if (row.Cells["lastPrintDate"].Text.Trim() == "")
                {
                    itemlot.Lastprintdate = 0;
                }
                else
                {
                    itemlot.Lastprintdate = int.Parse(row.Cells["lastPrintDate"].Text.Trim());
                }
                if (row.Cells["lastPrintTime"].Text.Trim() == "")
                {
                    itemlot.Lastprinttime = 0;
                }
                else
                {
                    itemlot.Lastprinttime = int.Parse(row.Cells["lastPrintTime"].Text.Trim());
                }
                itemlot.Orgid = orgid;
                itemlot.Transline = int.Parse(this.ucLabelComboxLineNo.SelectedItemText);
                itemlot.Transno = this.ucLabelEditRecNo.Value.Trim();
                itemlot.MaintainUser = ApplicationService.Current().LoginInfo.UserCode;
                itemlot.MaintainDate = dbDateTime.DBDate;
                itemlot.MaintainTime = dbDateTime.DBTime;
                itemlot.Vendorcode = this.ucLEVenderQuery.Value.Trim();
                itemlot.Active = "Y";
                return itemlot;
            }
        }

        protected ItemLotDetail GetEditItemLotDetailObject(Infragistics.Win.UltraWinGrid.UltraGridRow row)
        {
            if (row == null)
            {
                return null;
            }

            object obj = _InventoryFacade.GetItemLotDetail(row.Cells["SERIALNO"].Text, row.Cells["MCODE"].Text);

            if (obj != null)
            {
                return (ItemLotDetail)obj;
            }
            else
            {
                DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);

                ItemLotDetail itemlotdetail = new ItemLotDetail();
                itemlotdetail.Serialno = row.Cells["SERIALNO"].Text.Trim();
                itemlotdetail.Lotno = row.Cells["LotNO"].Text.Trim();
                itemlotdetail.Mcode = row.Cells["MCODE"].Text.Trim();
                itemlotdetail.Printtimes = int.Parse(row.Cells["PrintTimes"].Text.Trim());
                if (row.Cells["lastPrintUSER"].Text.Trim() == "")
                {
                    itemlotdetail.Lastprintuser = " ";
                }
                else
                {
                    itemlotdetail.Lastprintuser = row.Cells["lastPrintUSER"].Text.Trim();
                }
                if (row.Cells["lastPrintDate"].Text.Trim() == "")
                {
                    itemlotdetail.Lastprintdate = 0;
                }
                else
                {
                    itemlotdetail.Lastprintdate = int.Parse(row.Cells["lastPrintDate"].Text.Trim());
                }
                if (row.Cells["lastPrintTime"].Text.Trim() == "")
                {
                    itemlotdetail.Lastprinttime = 0;
                }
                else
                {
                    itemlotdetail.Lastprinttime = int.Parse(row.Cells["lastPrintTime"].Text.Trim());
                }
                itemlotdetail.MaintainUser = ApplicationService.Current().LoginInfo.UserCode;
                itemlotdetail.MaintainDate = dbDateTime.DBDate;
                itemlotdetail.MaintainTime = dbDateTime.DBTime;

                return itemlotdetail;
            }
        }
        #endregion

        #region Button Events

        //更新TblInvRecepitdetail中的数量
        private void UpdateInvRecepitDetailCount()
        {
            if (_IQCFacade == null)
            {
                _IQCFacade = new IQCFacade(this.DataProvider);
            }

            object obj = _IQCFacade.GetInvReceiptDetail(this.ucLabelEditRecNo.Value.Trim(), int.Parse(this.ucLabelComboxLineNo.SelectedItemText.Trim()));

            if (obj != null)
            {
                if (_ItemFacade == null)
                {
                    _ItemFacade = new ItemFacade(this.DataProvider);
                }

                //object objInvReceipt = warehouseFacade.GetInvReceipt(this.ucLabelEditRecNo.Value.Trim());

                object[] objItemLots = _InventoryFacade.QueryItemLot((obj as Domain.IQC.InvReceiptDetail).Itemcode, this.ucLabelEditRecNo.Value.Trim(), int.Parse(this.ucLabelComboxLineNo.SelectedItemText.Trim()), orgid);
                decimal sum = 0;
                if (objItemLots != null)
                {

                    foreach (ItemLot itemlot in objItemLots)
                    {
                        sum += itemlot.Lotqty;
                    }
                }
                (obj as Domain.IQC.InvReceiptDetail).Actqty = sum;

                _IQCFacade.UpdateINVReceiptDetail((obj as Domain.IQC.InvReceiptDetail));
            }

        }

        //删除
        private void ucBtnDelete_Click(object sender, System.EventArgs e)
        {
            if (!CheckRecStatus())
            {
                return;
            }
            if (!CheckISSelectRow())
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_NO_ROW_SELECTED"));
                return;
            }

            if (MessageBox.Show(MutiLanguages.ParserMessage("$ConformDelete"), this.Text, MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                return;
            }

            List<ItemLot> itemLotList = new List<ItemLot>();
            List<ItemLotDetail> itemLotDetailList = new List<ItemLotDetail>();


            for (int i = 0; i < ultraGridMain.Rows.Count; i++)
            {
                Infragistics.Win.UltraWinGrid.UltraGridRow row = ultraGridMain.Rows[i];

                if (row.Band.Index == 0)
                {
                    if (Convert.ToBoolean(row.Cells["Checked"].Value) == true)
                    {
                        object obj = this.GetEditItemLotObject(row);
                        itemLotList.Add(obj as ItemLot);

                        //this.m_LotGroup.Rows.RemoveAt();
                    }

                    if (row.HasChild())
                    {
                        for (int j = 0; j < row.ChildBands[0].Rows.Count; j++)
                        {
                            if (Convert.ToBoolean(row.ChildBands[0].Rows[j].Cells["Checked"].Value) == true)
                            {
                                object obj = this.GetEditItemLotDetailObject(row.ChildBands[0].Rows[j]);
                                itemLotDetailList.Add(obj as ItemLotDetail);
                            }
                        }
                    }

                }
            }
            try
            {
                this.DataProvider.BeginTransaction();


                //先将选中的ItemLotDetail删除
                string lotNo = string.Empty;
                string MCode = string.Empty;
                foreach (ItemLotDetail itemLotDetail in itemLotDetailList)
                {
                    if (itemLotDetail == null)
                    {
                        continue;
                    }
                    _InventoryFacade.DeleteItemLotDetail(itemLotDetail);
                    //删除TBLMKEYPARTDETAIL
                    _MaterialFacade.DeleteMKeyPartDetail(itemLotDetail.Mcode, itemLotDetail.Serialno);
                    lotNo = itemLotDetail.Lotno;
                    MCode = itemLotDetail.Mcode;
                }

                // new fun
                if (lotNo != "" && MCode != "")
                {
                    object itemLot = _InventoryFacade.GetItemLot(lotNo, MCode);
                    if (itemLot != null)
                    {

                        (itemLot as ItemLot).Lotqty = _InventoryFacade.QueryItemLotDetailCount(lotNo, MCode, orgid);
                        _InventoryFacade.UpdateItemLot((itemLot as ItemLot));

                    }

                }

                foreach (ItemLot itemLot in itemLotList)
                {
                    if (itemLot == null)
                    {
                        continue;
                    }
                    object[] objs = _InventoryFacade.QueryItemLotDetail(itemLot.Lotno, itemLot.Mcode, orgid);
                    //根据ItemLot获取ItemLotDetail，删除
                    if (objs != null)
                    {
                        foreach (ItemLotDetail itemLotDetail in objs)
                        {
                            if (itemLotDetail == null)
                            {
                                continue;
                            }
                            this._InventoryFacade.DeleteItemLotDetail(itemLotDetail);
                            //删除TBLMKEYPARTDETAIL
                            _MaterialFacade.DeleteMKeyPartDetail(itemLotDetail.Mcode, itemLotDetail.Serialno);
                        }
                    }
                    // 再删除ItemLot 
                    this._InventoryFacade.DeleteItemLot(itemLot);
                    //删除TBLMKEYPART
                    _MaterialFacade.DeleteMKeyPartBySql(itemLot.Mcode, itemLot.Lotno);
                }


                //在更新TblInvReceipt表中的数据

                UpdateInvRecepitDetailCount();

                this.ultraGridMain.DeleteSelectedRows(false);
                this.DataProvider.CommitTransaction();

                //刷新页面数据
                LoadCheckList(this.ucLabelEditRecNo.Value.Trim(), this.ucLabelComboxLineNo.SelectedItemText.Trim());
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                this.ShowMessage(ex);
                return;
            }
        }

        //保存方法
        private void Save(bool flag)
        {
            if (!CheckISSelectRow())
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_NO_ROW_SELECTED"));
                return;
            }

            if (flag)
            {
                if (!CheckVendorLotNo())
                {
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_VendorLotNo_Error"));
                    return;
                }
            }

            //mark by alex.hu 将这个限制放开，即关闭的单据也允许修改相关资料
            //if (!CheckRecStatus())
            //{
            //    return;
            //}
            try
            {
                this.DataProvider.BeginTransaction();
                Domain.MOModel.Material material = this.GetMaterial() as Domain.MOModel.Material;
                DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
                for (int i = 0; i < ultraGridMain.Rows.Count; i++)
                {
                    Infragistics.Win.UltraWinGrid.UltraGridRow row = ultraGridMain.Rows[i];

                    //TBLMKEYPARTDETAIL获取TBLMKEYPART用到
                    int seq = 0;

                    if (row.Band.Index == 0)
                    {
                        if (Convert.ToBoolean(row.Cells["Checked"].Value) == true)
                        {
                            ItemLot objlot = this.GetEditItemLotObject(row);
                            //itemLotList.Add(obj);

                            if (!lotno.ContainsKey(objlot.Lotno))
                            {
                                lotno.Add(objlot.Lotno, true);
                            }

                            object obj = _InventoryFacade.GetItemLot(objlot.Lotno, objlot.Mcode);
                            if (obj == null)
                            {
                                DateTime dateTime = FormatHelper.ToDateTime(objlot.Datecode, 0).AddDays(material.MShelfLife);
                                objlot.Exdate = FormatHelper.TODateInt(dateTime);
                                if (objlot.Exdate == 0)
                                {
                                    objlot.Exdate = 99991231;
                                }

                                _InventoryFacade.AddItemLot(objlot as ItemLot);

                                //新增时需要插入数据到TBLMKEYPART                                
                                MKeyPart newMKeyPart = new MKeyPart();
                                newMKeyPart.MItemCode = material.MaterialCode;
                                newMKeyPart.Sequence = Convert.ToInt32(_MaterialFacade.GetUniqueMKeyPartSequence());
                                seq = newMKeyPart.Sequence;
                                if (row.HasChild())
                                {
                                    newMKeyPart.RunningCardStart = row.ChildBands[0].Rows[0].Cells["SERIALNO"].Value.ToString();
                                    newMKeyPart.RunningCardEnd = row.ChildBands[0].Rows[row.ChildBands[0].Rows.Count - 1].Cells["SERIALNO"].Value.ToString();
                                }
                                else
                                {
                                    newMKeyPart.RunningCardStart = " ";
                                    newMKeyPart.RunningCardEnd = " ";
                                }

                                newMKeyPart.LotNO = objlot.Lotno;
                                newMKeyPart.PCBA = string.Empty;
                                newMKeyPart.BIOS = string.Empty;
                                newMKeyPart.Version = string.Empty;
                                newMKeyPart.VendorItemCode = objlot.Vendoritemcode;
                                newMKeyPart.VendorCode = objlot.Vendorcode;
                                newMKeyPart.DateCode = objlot.Datecode.ToString();
                                newMKeyPart.MaintainUser = ApplicationService.Current().UserCode;
                                newMKeyPart.MaintainDate = dbDateTime.DBDate; ;
                                newMKeyPart.MaintainTime = dbDateTime.DBTime;
                                newMKeyPart.EAttribute1 = string.Empty;
                                newMKeyPart.MoCode = string.Empty;
                                newMKeyPart.MITEMNAME = material.MaterialName;
                                newMKeyPart.RCardPrefix = objlot.Lotno;
                                newMKeyPart.SNScale = "10";
                                newMKeyPart.TemplateName = string.Empty;
                                _MaterialFacade.AddNewMKeyPart(newMKeyPart);
                                //End 
                            }
                            else
                            {
                                if (this.ucLabelEditSHCount.Value != "")
                                {
                                    if (objlot.Lotqty >= 0)
                                    {
                                        decimal sum = this.GetSumLotQty(material.MaterialCode, this.ucLabelEditRecNo.Value.Trim(), this.ucLabelComboxLineNo.SelectedItemText);

                                        if (decimal.Parse(this.ucLabelEditSHCount.Value.Trim()) >= sum)
                                        {
                                            (obj as ItemLot).Datecode = objlot.Datecode;
                                            (obj as ItemLot).Venderlotno = objlot.Venderlotno;
                                            (obj as ItemLot).Vendoritemcode = objlot.Vendoritemcode;
                                            (obj as ItemLot).Lotqty = objlot.Lotqty;
                                            _InventoryFacade.UpdateItemLot(obj as ItemLot);

                                            //更新TBLMKEYPART
                                            MKeyPart oldMKeyPart = (MKeyPart)_MaterialFacade.GetMKeyPart(material.MaterialCode, objlot.Lotno);
                                            seq = oldMKeyPart.Sequence;
                                            if (oldMKeyPart != null)
                                            {
                                                oldMKeyPart.DateCode = objlot.Datecode.ToString();
                                                oldMKeyPart.VendorCode = objlot.Vendorcode;
                                                oldMKeyPart.VendorItemCode = objlot.Vendoritemcode;
                                                oldMKeyPart.EAttribute1 = objlot.Venderlotno;
                                                if (row.HasChild())
                                                {
                                                    oldMKeyPart.RunningCardStart = row.ChildBands[0].Rows[0].Cells["SERIALNO"].Value.ToString();
                                                    oldMKeyPart.RunningCardEnd = row.ChildBands[0].Rows[row.ChildBands[0].Rows.Count - 1].Cells["SERIALNO"].Value.ToString();
                                                }

                                                _MaterialFacade.UpdateMKeyPart(oldMKeyPart);
                                            }
                                            //End
                                        }
                                        else
                                        {
                                            ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_LOTQTY_ERROR"));
                                            this.DataProvider.RollbackTransaction();
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_LOTQTY_LESS_THAN_ZEOR"));
                                        this.DataProvider.RollbackTransaction();
                                        return;
                                    }
                                }
                            }

                            if (row.HasChild())
                            {
                                string lotNo = string.Empty;
                                string MCode = string.Empty;

                                for (int j = 0; j < row.ChildBands[0].Rows.Count; j++)
                                {
                                    if (Convert.ToBoolean(row.ChildBands[0].Rows[j].Cells["Checked"].Value) == true)
                                    {
                                        ItemLotDetail objlotdetail = this.GetEditItemLotDetailObject(row.ChildBands[0].Rows[j]);
                                        //itemLotDetailList.Add(obj);
                                        //判断序列号是否唯一

                                        object objtemp = _InventoryFacade.GetItemLotDetail(objlotdetail.Serialno, objlotdetail.Mcode);

                                        if (objtemp == null)
                                        {
                                            objlotdetail.Storageid = " ";
                                            objlotdetail.Stackcode = " ";
                                            objlotdetail.Serialstatus = "UNSTORAGE";
                                            _InventoryFacade.AddItemLotDetail(objlotdetail as ItemLotDetail);

                                            //新增TBLMKEYPARTDETAIL
                                            MKeyPartDetail newMKeyPartDetail = new MKeyPartDetail();
                                            newMKeyPartDetail.MItemCode = objlotdetail.Mcode;
                                            newMKeyPartDetail.Sequence = seq;
                                            newMKeyPartDetail.SerialNo = objlotdetail.Serialno;
                                            newMKeyPartDetail.PrintTimes = 0;
                                            newMKeyPartDetail.MaintainUser = ApplicationService.Current().UserCode;
                                            newMKeyPartDetail.MaintainDate = dbDateTime.DBDate; ;
                                            newMKeyPartDetail.MaintainTime = dbDateTime.DBTime;
                                            newMKeyPartDetail.EAttribute1 = " ";
                                            _MaterialFacade.AddMKeyPartDetail(newMKeyPartDetail);
                                            //End

                                            lotNo = objlotdetail.Lotno;
                                            MCode = objlotdetail.Mcode;
                                        }
                                    }
                                }

                                if (lotNo != "" && MCode != "")
                                {
                                    object itemLot = _InventoryFacade.GetItemLot(lotNo, MCode);
                                    if (itemLot != null)
                                    {
                                        if ((itemLot as ItemLot).Lotqty >= 0)
                                        {

                                            (itemLot as ItemLot).Lotqty = _InventoryFacade.QueryItemLotDetailCount(lotNo, MCode, orgid);
                                            _InventoryFacade.UpdateItemLot((itemLot as ItemLot));

                                        }
                                        else
                                        {
                                            ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_LOTQTY_LESS_THAN_ZEOR"));
                                            this.DataProvider.RollbackTransaction();
                                            return;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                //跟新tblinvreceiptdetail表中的数据
                UpdateInvRecepitDetailCount();

                this.DataProvider.CommitTransaction();
                LoadCheckList(this.ucLabelEditRecNo.Value.Trim(), this.ucLabelComboxLineNo.SelectedItemText.Trim());

                lotno.Clear();
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Success, "$CS_Save_Success"));
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
            }
        }

        //保存
        private void ucBtnSave_Click(object sender, System.EventArgs e)
        {

            Save(true);

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

            string machineType = string.Empty;

            object obj = this.GetMaterial();
            if (obj != null)
            {
                machineType = (obj as Domain.MOModel.Material).MaterialMachineType;
            }
            printRcardList(machineType);
        }

        //生成批号
        private void ucButtonNewLot_Click(object sender, EventArgs e)
        {

            if (_IQCFacade == null)
            {
                _IQCFacade = new IQCFacade(this.DataProvider);
            }
            if (this.ucLabelEditRecNo.Value == "" || this.ucLabelEditRecNo.Value.Trim() == "")
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$ERROR_REGNO_EMPTY"));
                return;
            }
            if (this.ucLabelComboxLineNo.SelectedItemText == "" || this.ucLabelComboxLineNo.SelectedItemText.Trim() == "")
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$ERROR_LINENO_EMPTY"));
                return;
            }
            object obj = _IQCFacade.GetInvReceiptDetail(this.ucLabelEditRecNo.Value.Trim(), int.Parse(this.ucLabelComboxLineNo.SelectedItemText.Trim()));

            if (obj != null)
            {
                if ((obj as Domain.IQC.InvReceiptDetail).Recstatus.ToUpper() == RecStatus.CLOSE)
                {
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_RECSTATUS_CLOSE"));
                    return;
                }
                int qty = 0;

                if (true)
                {
                    lotno.Clear();

                    Domain.IQC.InvReceiptDetail invreceiptdetail = obj as Domain.IQC.InvReceiptDetail;
                    string controlType = this.GetMControlType().ToUpper();
                    if (controlType == MControlType.ITEM_CONTROL_LOT)
                    {
                        decimal total = invreceiptdetail.Qualifyqty;

                        object[] objItemLots = _InventoryFacade.QueryItemLot(invreceiptdetail.Itemcode, this.ucLabelEditRecNo.Value.Trim(), int.Parse(this.ucLabelComboxLineNo.SelectedItemText.Trim()), orgid);

                        if (objItemLots != null)
                        {
                            decimal sum = 0;
                            foreach (ItemLot itemlot in objItemLots)
                            {
                                sum += itemlot.Lotqty;
                            }
                            total = total - sum;

                            if (total == 0)
                            {
                                CreateNewLot(0, true, invreceiptdetail);
                            }

                            else if (this.ucLabelEditBZCount.Value != "" && this.ucLabelEditBZCount.Value.Trim() != "")
                            {
                                if (decimal.Parse(this.ucLabelEditBZCount.Value.Trim()) <= 0)
                                {
                                    this.ucLabelEditBZCount.Value = "";
                                    this.ucLabelEditBZCount.TextFocus(false, true);
                                    return;
                                }

                                decimal tempCount = decimal.Parse(this.ucLabelEditBZCount.Value.Trim());

                                while (total > 0)
                                {
                                    if (total > tempCount)
                                    {
                                        CreateNewLot(tempCount, true, invreceiptdetail);
                                    }
                                    else
                                    {
                                        CreateNewLot(total, true, invreceiptdetail);
                                    }
                                    total = total - tempCount;
                                }
                            }
                        }
                        else
                        {
                            //if (this.ucLabelEditSHCount.Value != "" && this.ucLabelEditSHCount.Value.Trim() != "")
                            //{
                            //    qty = (int)decimal.Parse(this.ucLabelEditSHCount.Value.Trim());
                            //}
                            //else
                            //{
                            //    qty = 0;
                            //}
                            //CreateNewLot(qty, invreceiptdetail);

                            if (this.ucLabelEditBZCount.Value != "" && this.ucLabelEditBZCount.Value.Trim() != "")
                            {
                                if (decimal.Parse(this.ucLabelEditBZCount.Value.Trim()) <= 0)
                                {
                                    this.ucLabelEditBZCount.Value = "";
                                    this.ucLabelEditBZCount.TextFocus(false, true);
                                    return;
                                }

                                decimal tempCount = decimal.Parse(this.ucLabelEditBZCount.Value.Trim());

                                while (total > 0)
                                {
                                    if (total > tempCount)
                                    {
                                        CreateNewLot(tempCount, true, invreceiptdetail);
                                    }
                                    else
                                    {
                                        CreateNewLot(total, true, invreceiptdetail);
                                    }
                                    total = total - tempCount;
                                }
                            }
                        }
                    }
                    else if (controlType == MControlType.ITEM_CONTROL_KEYPARTS)
                    {
                        CreateNewLot(0, true, invreceiptdetail);
                    }
                    else if (controlType == MControlType.ITEM_CONTROL_NOCONTROL)
                    {
                        return;
                    }
                    else
                    {
                        return;
                    }

                    this.ultraGridMain.DataSource = this.m_CheckList;
                    this.ultraGridMain.UpdateData();
                }

                for (int i = 0; i < this.ultraGridMain.Rows.Count; i++)
                {
                    Infragistics.Win.UltraWinGrid.UltraGridRow row = ultraGridMain.Rows[i];
                    if (row.Band.Index == 0)
                    {
                        row.Cells["Checked"].Value = false;
                        if (lotno.ContainsKey(row.Cells["LotNO"].Value.ToString()))
                        {
                            row.Cells["Checked"].Value = true;
                        }
                    }
                }

                //生成后调用保存方法
                Save(false);
                lotno.Clear();
            }
        }


        private void CreateNewLot(decimal qty, bool flag, Domain.IQC.InvReceiptDetail invreceiptdetail)
        {

            string lotNo = string.Empty;
            if (flag)
            {
                lotNo = cmdCalCard(invreceiptdetail.Itemcode);
            }
            else
            {
                lotNo = this.ucLabelEditLotNo.Value.Trim();
            }
            if (lotNo == "")
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$Error_Create_LotNo"));
                return;
            }

            DBDateTime dbDatetime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            DataRow rowGroup = this.m_CheckList.Tables["LotGroup"].NewRow();
            rowGroup["Checked"] = "true";
            rowGroup["LotNO"] = lotNo;
            rowGroup["MCODE"] = invreceiptdetail.Itemcode;//(this.GetMaterial() as Domain.MOModel.Material).MaterialCode;
            rowGroup["LOTQTY"] = qty;
            rowGroup["DATECODE"] = FormatHelper.ToDateTime(dbDatetime.DBDate, 0);
            rowGroup["VenderLotNO"] = invreceiptdetail.VenderLotNO;
            rowGroup["VenderITEMCODE"] = " ";
            rowGroup["PrintTimes"] = 0;
            rowGroup["lastPrintUSER"] = " ";
            rowGroup["lastPrintDate"] = "";
            rowGroup["lastPrintTime"] = "";
            this.m_CheckList.Tables["LotGroup"].Rows.Add(rowGroup);
            this.m_CheckList.Tables["LotGroup"].AcceptChanges();

            if (!lotno.ContainsKey(lotNo))
            {
                lotno.Add(lotNo, true);
            }
        }


        //计算生成序列号
        private void ucButtonCalcRCardEnd_Click(object sender, EventArgs e)
        {


            if (!this.radioButton_StartRcard.Checked)
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_STARTRCARD_NO_SELECTED"));
                return;
            }


            if (this.ucLabelEditRecNo.Value == "" || this.ucLabelEditRecNo.Value.Trim() == "")
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$ERROR_REGNO_EMPTY"));
                this.ucLabelEditRecNo.TextFocus(false, true);
                return;
            }

            if (this.ucLabelComboxLineNo.SelectedIndex == -1)
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$ERROR_REGNO_LINENO_EMPTY"));
                return;
            }

            if (this.ucLERCStartRcard.Value.Trim() == "")
            {

                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_STARTRCARD_EMPTY"));
                this.ucLERCStartRcard.TextFocus(false, true);
                return;
            }

            if (this.ucLabelEditRcardCount.Value.Trim() == "")
            {

                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_STARTRCARD_COUNT_EMPTY"));
                this.ucLabelEditRcardCount.TextFocus(false, true);
                return;
            }

            decimal sum = this.GetSumLotQty() + decimal.Parse(this.ucLabelEditRcardCount.Value);

            if (decimal.Parse(this.ucLabelEditSHCount.Value.Trim()) < sum)
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_LOTQTY_ERROR"));
                return;
            }


            int numStart = int.Parse(this.ucLERCStartRcard.Value.Trim());
            int numCount = int.Parse(this.ucLabelEditRcardCount.Value.Trim());

            if (numStart < 0)
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_STARTRCARD_LESS_THAN_ZEOR"));
                this.ucLERCStartRcard.TextFocus(false, true);
                return;
            }

            if (numCount < 0)
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_STARTRCARD_COUNT_LESS_THAN_ZEOR"));
                this.ucLabelEditRcardCount.TextFocus(false, true);
                return;
            }

            if ((numStart + numCount) > 9999)
            {
                return;
            }

            if (!CheckRecStatus())
            {
                return;
            }

            if (!CheckSelectOnlyOneLot())
            {
                return;
            }


            UltraGridRow row = this.GetLotNoRow();
            for (int i = numStart; i < (numStart + numCount); i++)
            {
                string rCard = row.Cells["LotNO"].Value.ToString() + string.Format("{0:0000}", i);

                if (!CheckRCardIsSame(rCard))
                {
                    return;
                }
            }

            for (int i = numStart; i < (numStart + numCount); i++)
            {
                string rCard = row.Cells["LotNO"].Value.ToString() + string.Format("{0:0000}", i);
                BandingRCard(rCard, row);
            }

            Save(false);

        }
        #endregion

        #region 物料是单件管控料，需要用户维护序列号。


        //扫描生成序列号
        private void ucLabelEditRCardQuery_KeyPress(object sender, KeyPressEventArgs e)
        {


            if (e.KeyChar == '\r')
            {

                if (this.ucLabelEditRecNo.Value == "" || this.ucLabelEditRecNo.Value.Trim() == "")
                {
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$ERROR_REGNO_EMPTY"));
                    this.ucLabelEditRecNo.TextFocus(false, true);
                    return;
                }
                if (this.ucLabelComboxLineNo.SelectedIndex == -1)
                {
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$ERROR_REGNO_LINENO_EMPTY"));
                    return;
                }

                if (!this.radioButton_Rcard.Checked)
                {

                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_RCARD_NO_SELECTED"));
                    this.ucLabelEditRCardQuery.TextFocus(false, true);
                    return;
                }

                if (this.ucLabelEditRCardQuery.Value == "" || this.ucLabelEditRCardQuery.Value.Trim() == "")
                {
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_RCARD_EMPTY"));
                    this.ucLabelEditRCardQuery.TextFocus(false, true);
                    return;
                }

                if (!CheckRecStatus())
                {
                    this.ucLabelEditRCardQuery.TextFocus(false, true);
                    return;
                }

                if (!CheckSelectOnlyOneLot())
                {
                    this.ucLabelEditRCardQuery.TextFocus(false, true);
                    return;
                }

                if (!CheckRCardIsSame(this.ucLabelEditRCardQuery.Value.Trim()))
                {
                    this.ucLabelEditRCardQuery.TextFocus(false, true);
                    return;
                }

                if (!CheckRCardChecked(this.ucLabelEditRCardQuery.Value.Trim()))
                {
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_SERIALNO_LOTNO_ERROR"));
                    this.ucLabelEditRCardQuery.TextFocus(false, true);
                    return;
                }

                decimal sum = this.GetSumLotQty();

                if (decimal.Parse(this.ucLabelEditSHCount.Value.Trim()) <= sum)
                {
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_LOTQTY_ERROR"));
                    this.ucLabelEditRCardQuery.TextFocus(false, true);
                    return;
                }


                BandingRCard(this.ucLabelEditRCardQuery.Value.Trim(), this.GetLotNoRow());
                lotno.Clear();
                lotno.Add(this.GetLotNoRow().Cells["LotNO"].Value.ToString(), true);
                Save(true);
                lotno.Clear();
                this.ucLabelEditRCardQuery.TextFocus(false, true);
            }
        }

        //将生成的序列号绑定到Grid上
        private void BandingRCard(string rCard, UltraGridRow row)
        {
            //DBDateTime dbDatetime = FormatHelper.GetNowDBDateTime(this.DataProvider);

            DataRow rowLotDetail = this.m_CheckList.Tables["LotDetail"].NewRow();
            rowLotDetail["Checked"] = "true";
            rowLotDetail["SERIALNO"] = rCard;
            rowLotDetail["LotNO"] = row.Cells["LotNO"].Value.ToString();
            rowLotDetail["MCODE"] = row.Cells["MCODE"].Value.ToString();
            rowLotDetail["PrintTimes"] = 0;
            rowLotDetail["lastPrintUSER"] = " ";
            rowLotDetail["lastPrintDate"] = "";
            rowLotDetail["lastPrintTime"] = "";
            this.m_CheckList.Tables["LotDetail"].Rows.Add(rowLotDetail);
            this.m_CheckList.Tables["LotDetail"].AcceptChanges();
            this.ultraGridMain.DataSource = this.m_CheckList;
            this.ultraGridMain.UpdateData();
        }

        //获取Grid上选中的某个批号
        private UltraGridRow GetLotNoRow()
        {
            for (int i = 0; i < this.ultraGridMain.Rows.Count; i++)
            {
                Infragistics.Win.UltraWinGrid.UltraGridRow row = ultraGridMain.Rows[i];
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
            for (int i = 0; i < this.ultraGridMain.Rows.Count; i++)
            {
                Infragistics.Win.UltraWinGrid.UltraGridRow row = ultraGridMain.Rows[i];
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
                return false;
            }
            if (tempCount < 1)
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_LOT_NOT_SELECTED"));
                return false;
            }

            return true;
        }

        //检查序列号是否重复
        private bool CheckRCardIsSame(string rCard)
        {
            bool flag = true;

            //先判断Grid中的数据是否重复
            for (int i = 0; i < this.ultraGridMain.Rows.Count; i++)
            {
                Infragistics.Win.UltraWinGrid.UltraGridRow row = ultraGridMain.Rows[i];

                if (row.HasChild())
                {
                    for (int j = 0; j < row.ChildBands[0].Rows.Count; j++)
                    {

                        if (row.ChildBands[0].Rows[j].Cells["SERIALNO"].Value.ToString() == rCard)
                        {
                            flag = false;
                            ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_SERIALNO_IS_EXIST_IN_GRID"));
                            return flag;
                        }
                    }
                }
            }

            //在判断数据库中的数据是否重复，在Lot是否已经存在
            object objM = this.GetMaterial();
            if (objM != null)
            {
                object obj = _InventoryFacade.GetItemLotDetail(rCard, (objM as Domain.MOModel.Material).MaterialCode);
                if (obj != null)
                {
                    flag = false;
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_SERIALNO_IS_EXIST_IN_LOT $LOTNO_EXIST:" + (obj as ItemLotDetail).Lotno));
                    return flag;
                }
            }

            return flag;
        }

        //检查勾选了序号检查,检查序号的批号是否和当前勾选批一致
        private bool CheckRCardChecked(string rCard)
        {
            if (!this.checkBoxRcardCheck.Checked)
            {
                return true;
            }

            bool flag = false;

            UltraGridRow row = this.GetLotNoRow();

            if (rCard.Length <= 5)
            {
                return false;
            }
            string lotNO = rCard.Substring(0, rCard.Length - 5);//rCard.Split('-')[0];

            if (lotNO == row.Cells["LotNO"].Value.ToString())
            {
                flag = true;
            }
            return flag;
        }

        private bool CheckISSelectRow()
        {

            bool flag = false;

            //先判断Grid中的数据是否重复
            for (int i = 0; i < this.ultraGridMain.Rows.Count; i++)
            {
                Infragistics.Win.UltraWinGrid.UltraGridRow row = ultraGridMain.Rows[i];
                if (Convert.ToBoolean(row.Cells["Checked"].Value) == true)
                {
                    flag = true;
                }

                if (row.HasChild())
                {
                    for (int j = 0; j < row.ChildBands[0].Rows.Count; j++)
                    {

                        if (Convert.ToBoolean(row.ChildBands[0].Rows[j].Cells["Checked"].Value) == true)
                        {
                            flag = true;
                        }
                    }
                }
            }

            return flag;
        }



        //获取gird中所有批的数量总和
        private decimal GetSumLotQty(string mcode, string receiptno, string receiptline)
        {
            decimal sum = 0;

            Hashtable ht = new Hashtable();
            for (int i = 0; i < this.ultraGridMain.Rows.Count; i++)
            {
                Infragistics.Win.UltraWinGrid.UltraGridRow row = ultraGridMain.Rows[i];
                if (row.Band.Index == 0)
                {
                    if (Convert.ToBoolean(row.Cells["Checked"].Value) == true)
                    {
                        sum += decimal.Parse(row.Cells["LOTQTY"].Text.Trim());

                        ht.Add(row.Cells["LotNO"].Text.Trim(), "");
                    }
                }
            }

            object[] lotGroups = _InventoryFacade.QueryItemLot(mcode, receiptno, int.Parse(receiptline), orgid);
            if (lotGroups != null)
            {
                foreach (ItemLot item in lotGroups)
                {
                    if (!ht.ContainsKey(item.Lotno))
                    {
                        sum += item.Lotqty;
                    }
                }
            }
            return sum;
        }

        //检查供应商批号是否为空
        private bool CheckVendorLotNo()
        {
            bool flag = true;

            //先判断Grid中的数据是否重复
            for (int i = 0; i < this.ultraGridMain.Rows.Count; i++)
            {
                Infragistics.Win.UltraWinGrid.UltraGridRow row = ultraGridMain.Rows[i];
                if (Convert.ToBoolean(row.Cells["Checked"].Value) == true)
                {
                    if (row.Cells["VenderLotNO"].Value.ToString().Trim() == string.Empty)
                    {
                        flag = false;
                    }
                }
            }

            return flag;
        }

        //获取gird中所有批的数量总和
        private decimal GetSumLotQty()
        {
            decimal sum = 0;
            for (int i = 0; i < this.ultraGridMain.Rows.Count; i++)
            {
                Infragistics.Win.UltraWinGrid.UltraGridRow row = ultraGridMain.Rows[i];
                if (row.Band.Index == 0)
                {
                    sum += decimal.Parse(row.Cells["LOTQTY"].Text.Trim());
                }
            }
            return sum;
        }


        #endregion

        #region 入库批号生成Grid()
        private void ultraGridMain_InitializeRow(object sender, InitializeRowEventArgs e)
        {
            //e.Row.Activation = Activation.NoEdit;
            if (e.Row.Band.Index == 0)
            {
                e.Row.Cells["DATECODE"].Column.CellAppearance.BackColor = Color.LawnGreen;
                e.Row.Cells["VenderLotNO"].Column.CellAppearance.BackColor = Color.LawnGreen;
                e.Row.Cells["VenderITEMCODE"].Column.CellAppearance.BackColor = Color.LawnGreen;

                Domain.MOModel.Material material = this.GetMaterial() as Domain.MOModel.Material;
                if (material != null)
                {
                    if (material.MaterialControlType.ToUpper() == MControlType.ITEM_CONTROL_LOT)
                    {
                        e.Row.Cells["LOTQTY"].Column.CellActivation = Activation.AllowEdit;
                        e.Row.Cells["LOTQTY"].Column.CellAppearance.BackColor = Color.LawnGreen;
                    }
                }
            }
        }

        private void InitializeMainGrid()
        {
            this.m_CheckList = new DataSet();

            this.m_LotGroup = new DataTable("LotGroup");
            this.m_LotDetail = new DataTable("LotDetail");
            this.m_LotGroup.Columns.Add("Checked", typeof(string));
            this.m_LotGroup.Columns.Add("LotNO", typeof(string));
            this.m_LotGroup.Columns.Add("MCODE", typeof(string));
            this.m_LotGroup.Columns.Add("LOTQTY", typeof(string));
            this.m_LotGroup.Columns.Add("DATECODE", typeof(DateTime));
            this.m_LotGroup.Columns.Add("VenderLotNO", typeof(string));
            this.m_LotGroup.Columns.Add("VenderITEMCODE", typeof(string));
            this.m_LotGroup.Columns.Add("PrintTimes", typeof(string));
            this.m_LotGroup.Columns.Add("lastPrintUSER", typeof(string));
            this.m_LotGroup.Columns.Add("lastPrintDate", typeof(string));
            this.m_LotGroup.Columns.Add("lastPrintTime", typeof(string));

            this.m_LotDetail.Columns.Add("Checked", typeof(string));
            this.m_LotDetail.Columns.Add("LotNO", typeof(string));
            this.m_LotDetail.Columns.Add("MCODE", typeof(string));
            this.m_LotDetail.Columns.Add("SERIALNO", typeof(string));
            this.m_LotDetail.Columns.Add("PrintTimes", typeof(string));
            this.m_LotDetail.Columns.Add("lastPrintUSER", typeof(string));
            this.m_LotDetail.Columns.Add("lastPrintDate", typeof(string));
            this.m_LotDetail.Columns.Add("lastPrintTime", typeof(string));

            this.m_CheckList.Tables.Add(this.m_LotGroup);
            this.m_CheckList.Tables.Add(this.m_LotDetail);
            this.m_CheckList.Relations.Add(new DataRelation("LotGroupAndLotDetail",
                                                this.m_CheckList.Tables["LotGroup"].Columns["LotNO"],
                                                this.m_CheckList.Tables["LotDetail"].Columns["LotNO"]));
            this.m_CheckList.AcceptChanges();
            this.ultraGridMain.DataSource = this.m_CheckList;
        }

        private void ultraGridMain_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
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
            e.Layout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;

            // 不允许删除
            e.Layout.Override.AllowDelete = DefaultableBoolean.False;

            // 滚动提示
            e.Layout.Bands[0].ScrollTipField = "LotNO";
            e.Layout.Bands[1].ScrollTipField = "SERIALNO";

            // 设置列宽和列名称
            e.Layout.Bands[0].Columns["Checked"].Header.Caption = "";
            e.Layout.Bands[0].Columns["LotNO"].Header.Caption = "批号";
            e.Layout.Bands[0].Columns["MCODE"].Header.Caption = "";
            e.Layout.Bands[0].Columns["LOTQTY"].Header.Caption = "批内数量";
            e.Layout.Bands[0].Columns["DATECODE"].Header.Caption = "生产日期";
            e.Layout.Bands[0].Columns["VenderLotNO"].Header.Caption = "供应商批号";
            e.Layout.Bands[0].Columns["VenderITEMCODE"].Header.Caption = "供应商物料代码";
            e.Layout.Bands[0].Columns["PrintTimes"].Header.Caption = "打印数量";
            e.Layout.Bands[0].Columns["lastPrintUSER"].Header.Caption = "最后打印人";
            e.Layout.Bands[0].Columns["lastPrintDate"].Header.Caption = "最后日期";
            e.Layout.Bands[0].Columns["lastPrintTime"].Header.Caption = "最后时间";



            e.Layout.Bands[0].Columns["MCODE"].Hidden = true;
            e.Layout.Bands[0].Columns["Checked"].Width = 40;
            e.Layout.Bands[0].Columns["LotNO"].Width = 100;
            e.Layout.Bands[0].Columns["LOTQTY"].Width = 60;
            e.Layout.Bands[0].Columns["DATECODE"].Width = 100;
            e.Layout.Bands[0].Columns["VenderLotNO"].Width = 100;
            e.Layout.Bands[0].Columns["VenderITEMCODE"].Width = 100;
            e.Layout.Bands[0].Columns["PrintTimes"].Width = 60;
            e.Layout.Bands[0].Columns["lastPrintUSER"].Width = 100;
            e.Layout.Bands[0].Columns["lastPrintDate"].Width = 100;
            e.Layout.Bands[0].Columns["lastPrintTime"].Width = 100;

            // 设置栏位是否允许编辑，及栏位的显示形式
            e.Layout.Bands[0].Columns["Checked"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;
            e.Layout.Bands[0].Columns["LotNO"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
            e.Layout.Bands[0].Columns["LOTQTY"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[0].Columns["DATECODE"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.AllowEdit;
            e.Layout.Bands[0].Columns["VenderLotNO"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.AllowEdit;
            e.Layout.Bands[0].Columns["VenderITEMCODE"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.AllowEdit;
            e.Layout.Bands[0].Columns["PrintTimes"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[0].Columns["lastPrintUSER"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[0].Columns["lastPrintDate"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[0].Columns["lastPrintTime"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;

            //e.Layout.Bands[0].Columns["lastPrintDate"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Date;
            //e.Layout.Bands[0].Columns["lastPrintTime"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Date;
            e.Layout.Bands[0].Columns["DATECODE"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Date;
            //设置可编辑列的颜色
            //e.Layout.Bands[0].Columns["DATECODE"].CellAppearance.BackColor = Color.LawnGreen;
            //e.Layout.Bands[0].Columns["VenderLotNO"].CellAppearance.BackColor = Color.LawnGreen ;
            //e.Layout.Bands[0].Columns["VenderITEMCODE"].CellAppearance.BackColor = Color.LawnGreen;



            // 允许筛选
            e.Layout.Bands[0].Columns["LotNO"].AllowRowFiltering = DefaultableBoolean.True;
            e.Layout.Bands[0].Columns["LotNO"].SortIndicator = SortIndicator.Ascending;
            // 允许冻结，且Checked栏位始终处于冻结状态，不可更改
            //e.Layout.Bands[0].Columns["Checked"].Header.Fixed = true;
            //e.Layout.Bands[0].Columns["LotNO"].Header.Fixed = true;
            //e.Layout.Bands[0].Columns["LotNO"].Header.FixedHeaderIndicator = FixedHeaderIndicator.Button;
            //e.Layout.Bands[0].Columns["LotNO"].SortIndicator = SortIndicator.Ascending;

            // CheckItem
            e.Layout.Bands[1].Columns["LotNO"].Hidden = true;
            e.Layout.Bands[1].Columns["MCODE"].Hidden = true;
            e.Layout.Bands[1].Columns["Checked"].Header.Caption = "";
            e.Layout.Bands[1].Columns["LotNO"].Header.Caption = "批号";
            e.Layout.Bands[1].Columns["SERIALNO"].Header.Caption = "产品序列号";
            e.Layout.Bands[1].Columns["PrintTimes"].Header.Caption = "打印数量";
            e.Layout.Bands[1].Columns["lastPrintUSER"].Header.Caption = "最后打印人";
            e.Layout.Bands[1].Columns["lastPrintDate"].Header.Caption = "最后日期";
            e.Layout.Bands[1].Columns["lastPrintTime"].Header.Caption = "最后时间";

            e.Layout.Bands[1].Columns["Checked"].Width = 40;
            e.Layout.Bands[1].Columns["SERIALNO"].Width = 160;
            e.Layout.Bands[1].Columns["PrintTimes"].Width = 60;
            e.Layout.Bands[1].Columns["lastPrintUSER"].Width = 100;
            e.Layout.Bands[1].Columns["lastPrintDate"].Width = 100;
            e.Layout.Bands[1].Columns["lastPrintTime"].Width = 100;

            e.Layout.Bands[1].Columns["Checked"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;
            e.Layout.Bands[1].Columns["SERIALNO"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[1].Columns["PrintTimes"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[1].Columns["lastPrintUSER"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[1].Columns["lastPrintDate"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[1].Columns["lastPrintTime"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;

            e.Layout.Bands[1].Columns["SERIALNO"].SortIndicator = SortIndicator.Ascending;
        }

        private void ultraGridMain_CellChange(object sender, CellEventArgs e)
        {
            this.ultraGridMain.UpdateData();
            if (e.Cell.Column.Key == "Checked")
            {
                if (e.Cell.Row.Band.Index == 0) //Parent
                {
                    for (int i = 0; i < e.Cell.Row.ChildBands[0].Rows.Count; i++)
                    {
                        e.Cell.Row.ChildBands[0].Rows[i].Cells["Checked"].Value = e.Cell.Value;
                    }
                }

                //if (e.Cell.Row.Band.Index == 1) // Child
                //{
                //    if (Convert.ToBoolean(e.Cell.Value) == true)
                //    {
                //        e.Cell.Row.ParentRow.Cells["Checked"].Value = e.Cell.Value;
                //    }
                //    else
                //    {
                //        bool needUnCheckHeader = true;
                //        for (int i = 0; i < e.Cell.Row.ParentRow.ChildBands[0].Rows.Count; i++)
                //        {
                //            if (Convert.ToBoolean(e.Cell.Row.ParentRow.ChildBands[0].Rows[i].Cells["Checked"].Value) == true)
                //            {
                //                needUnCheckHeader = false;
                //                break;
                //            }
                //        }
                //        if (needUnCheckHeader)
                //        {
                //            e.Cell.Row.ParentRow.Cells["Checked"].Value = e.Cell.Value;
                //        }
                //    }
                //}
            }
            this.ultraGridMain.UpdateData();
        }

        private void LoadCheckList(string receiptno, string receiptline)
        {
            if (_ItemFacade == null)
            {
                _ItemFacade = new ItemFacade(this.DataProvider);
            }
            try
            {
                this.ClearCheckList();

                object objm = this.GetMaterial();
                if (objm == null)
                {
                    this.ultraGridMain.DataSource = this.m_CheckList;
                    this.ultraGridMain.UpdateData();
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$WarehouseItem_Not_Exist"));
                    return;
                }
                object[] lotGroups = _InventoryFacade.QueryItemLot((objm as Domain.MOModel.Material).MaterialCode, receiptno, int.Parse(receiptline), orgid);
                DataRow rowGroup;
                if (lotGroups != null)
                {
                    foreach (ItemLot item in lotGroups)
                    {
                        if (lotno.ContainsKey(item.Lotno))
                        {
                            rowGroup = this.m_CheckList.Tables["LotGroup"].NewRow();
                            rowGroup["Checked"] = "true";
                            rowGroup["MCODE"] = item.Mcode;
                            rowGroup["LotNO"] = item.Lotno;
                            rowGroup["LOTQTY"] = item.Lotqty;
                            rowGroup["DATECODE"] = FormatHelper.ToDateTime(item.Datecode);
                            rowGroup["VenderLotNO"] = item.Venderlotno;
                            rowGroup["VenderITEMCODE"] = item.Vendoritemcode;
                            rowGroup["PrintTimes"] = item.Printtimes;
                            rowGroup["lastPrintUSER"] = item.Lastprintuser;
                            rowGroup["lastPrintDate"] = item.Lastprintdate;
                            rowGroup["lastPrintTime"] = item.Lastprinttime;

                            this.m_CheckList.Tables["LotGroup"].Rows.Add(rowGroup);
                        }
                        else
                        {
                            rowGroup = this.m_CheckList.Tables["LotGroup"].NewRow();
                            rowGroup["Checked"] = "false";
                            rowGroup["MCODE"] = item.Mcode;
                            rowGroup["LotNO"] = item.Lotno;
                            rowGroup["LOTQTY"] = item.Lotqty;
                            rowGroup["DATECODE"] = FormatHelper.ToDateTime(item.Datecode);
                            rowGroup["VenderLotNO"] = item.Venderlotno;
                            rowGroup["VenderITEMCODE"] = item.Vendoritemcode;
                            rowGroup["PrintTimes"] = item.Printtimes;
                            rowGroup["lastPrintUSER"] = item.Lastprintuser;
                            rowGroup["lastPrintDate"] = item.Lastprintdate;
                            rowGroup["lastPrintTime"] = item.Lastprinttime;

                            this.m_CheckList.Tables["LotGroup"].Rows.Add(rowGroup);
                        }

                        object[] checkItems = _InventoryFacade.QueryItemLotDetail(item.Lotno, item.Mcode, orgid);
                        if (checkItems != null)
                        {
                            DataRow rowItem;
                            foreach (ItemLotDetail checkList in checkItems)
                            {
                                rowItem = this.m_CheckList.Tables["LotDetail"].NewRow();
                                rowItem["Checked"] = "false";
                                rowItem["MCODE"] = checkList.Mcode;
                                rowItem["LotNO"] = checkList.Lotno;
                                rowItem["SERIALNO"] = checkList.Serialno;
                                rowItem["PrintTimes"] = checkList.Printtimes;
                                rowItem["lastPrintUSER"] = checkList.Lastprintuser;
                                rowItem["lastPrintDate"] = checkList.Lastprintdate;
                                rowItem["lastPrintTime"] = checkList.Lastprinttime;

                                this.m_CheckList.Tables["LotDetail"].Rows.Add(rowItem);
                            }
                        }
                    }

                }

                this.m_CheckList.Tables["LotGroup"].AcceptChanges();
                this.m_CheckList.Tables["LotDetail"].AcceptChanges();
                this.m_CheckList.AcceptChanges();
                this.ultraGridMain.DataSource = this.m_CheckList;


                this.ultraGridMain.UpdateData();
            }
            catch (Exception ex)
            {
            }
        }

        private void ClearCheckList()
        {
            if (this.m_CheckList == null)
            {
                return;
            }
            this.m_CheckList.Tables["LotDetail"].Rows.Clear();
            this.m_CheckList.Tables["LotDetail"].AcceptChanges();
            this.m_CheckList.Tables["LotGroup"].Rows.Clear();
            this.m_CheckList.Tables["LotGroup"].AcceptChanges();

            this.m_CheckList.AcceptChanges();
        }
        #endregion

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
        private void printRcardList(string machineType)
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



                List<ItemLot> itemLotList = new List<ItemLot>();
                List<ItemLotDetail> itemLotDetailList = new List<ItemLotDetail>();


                for (int i = 0; i < ultraGridMain.Rows.Count; i++)
                {
                    Infragistics.Win.UltraWinGrid.UltraGridRow row = ultraGridMain.Rows[i];

                    if (row.Band.Index == 0)
                    {
                        if (Convert.ToBoolean(row.Cells["Checked"].Value) == true)
                        {
                            object obj = this.GetEditItemLotObject(row);
                            itemLotList.Add(obj as ItemLot);
                        }

                        if (row.HasChild())
                        {
                            for (int j = 0; j < row.ChildBands[0].Rows.Count; j++)
                            {
                                if (Convert.ToBoolean(row.ChildBands[0].Rows[j].Cells["Checked"].Value) == true)
                                {
                                    object obj = this.GetEditItemLotDetailObject(row.ChildBands[0].Rows[j]);
                                    itemLotDetailList.Add(obj as ItemLotDetail);
                                }
                            }
                        }

                    }
                }

                if (itemLotList.Count == 0 && itemLotDetailList.Count == 0)
                {
                    return;
                }

                if (!ValidateInput(printer, printTemplate))
                {
                    return;
                }


                for (int i = 0; i < int.Parse(this.ucLabelEditPrintCount.Value.Trim()); i++)
                {
                    Messages msg = this.Print(printer, printTemplate.TemplatePath, itemLotList, itemLotDetailList, machineType, (this.GetMaterial() as Domain.MOModel.Material).MaterialName);

                    if (msg.IsSuccess())
                    {
                        //打印后的数据处理
                        try
                        {
                            string userCode = ApplicationService.Current().UserCode;
                            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);

                            this.DataProvider.BeginTransaction();

                            foreach (ItemLot itemlot in itemLotList)
                            {
                                itemlot.Printtimes++;
                                itemlot.Lastprintuser = userCode;
                                itemlot.Lastprintdate = dbDateTime.DBDate;
                                itemlot.Lastprinttime = dbDateTime.DBTime;

                                _InventoryFacade.UpdateItemLot(itemlot);
                            }

                            foreach (ItemLotDetail itemlotdetail in itemLotDetailList)
                            {
                                itemlotdetail.Printtimes++;
                                itemlotdetail.Lastprintuser = userCode;
                                itemlotdetail.Lastprintdate = dbDateTime.DBDate;
                                itemlotdetail.Lastprinttime = dbDateTime.DBTime;
                                _InventoryFacade.UpdateItemLotDetail(itemlotdetail);
                            }

                            this.DataProvider.CommitTransaction();

                        }
                        catch (System.Exception ex)
                        {
                            this.DataProvider.RollbackTransaction();

                            this.ShowMessage(new UserControl.Message(UserControl.MessageType.Error, ex.Message));
                            return;
                        }
                    }

                    this.ShowMessage(msg);
                }

                LoadCheckList(this.ucLabelEditRecNo.Value.Trim(), this.ucLabelComboxLineNo.SelectedItemText.Trim());

            }
            finally
            {
                SetPrintButtonStatus(true);
            }
        }

        //打印
        public UserControl.Messages Print(string printer, string templatePath, List<ItemLot> itemLotList, List<ItemLotDetail> itemLotDetailList, string machineType, string mName)
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

                for (int i = 0; i < itemLotList.Count; i++)
                {
                    ItemLot itemlot = (ItemLot)itemLotList[i];

                    LabelPrintVars labelPrintVars = new LabelPrintVars();

                    string[] vars = new string[0];

                    if (messages.IsSuccess())
                    {


                        try
                        {
                            //要传给Codesoft的数组，字段顺序不能修改
                            vars = _CodeSoftPrintFacade.GetPrintVars(itemlot.Lotno, itemlot.Mcode, mName, machineType, itemlot.Lotqty.ToString(), itemlot.Venderlotno);

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


                for (int i = 0; i < itemLotDetailList.Count; i++)
                {
                    ItemLotDetail itemlotdetail = (ItemLotDetail)itemLotDetailList[i];

                    LabelPrintVars labelPrintVars = new LabelPrintVars();

                    string[] vars = new string[0];

                    if (messages.IsSuccess())
                    {
                        try
                        {
                            string datacode = string.Empty;
                            ItemLot lot = _InventoryFacade.GetItemLot(itemlotdetail.Lotno, itemlotdetail.Mcode) as ItemLot;
                            if (lot != null)
                            {
                                datacode = lot.Datecode.ToString();
                            }



                            //要传给Codesoft的数组，字段顺序不能修改
                            vars = _CodeSoftPrintFacade.GetPrintVars(itemlotdetail.Serialno, itemlotdetail.Mcode, mName, machineType, "1", datacode);
                            // vars = _CodeSoftPrintFacade.GetPrintVars(itemlotdetail.Serialno, itemlotdetail.Mcode, mName, machineType, "1",);

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

        #endregion

        private void ucLabelComboxLineNo_Load()
        {
            this.ucLabelComboxLineNo.Clear();
            if (_IQCFacade == null)
            {
                _IQCFacade = new IQCFacade(this.DataProvider);
            }
            object[] objs = _IQCFacade.QueryInvReceiptDetail(this.ucLabelEditRecNo.Value.Trim());

            if (objs != null && objs.Length > 0)
            {
                foreach (Domain.IQC.InvReceiptDetail detail in objs)
                {
                    this.ucLabelComboxLineNo.AddItem(detail.Receiptline.ToString(), detail.Receiptline.ToString());
                }

                this.ucLabelComboxLineNo.SelectedIndex = 0;
            }
        }

        private void cmdRecSelect_Click(object sender, EventArgs e)
        {
            FRecNoSelect objForm = new FRecNoSelect();
            objForm.ProductInfoEvent += new ParentChildRelateEventHandler<ParentChildRelateEventArgs<System.Collections.Hashtable>>(objForm_ProductInfoEvent);
            objForm.ShowDialog();
        }

        void objForm_ProductInfoEvent(object sender, ParentChildRelateEventArgs<System.Collections.Hashtable> e)
        {
            this.ucLabelEditRecNo.Value = e.CustomObject["ReceiptNO"].ToString();
        }


        //批号扫描
        private void ucLabelEditLotNo_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                //判断入库单号是否为空
                if (this.ucLabelEditRecNo.Value == "" || this.ucLabelEditRecNo.Value.Trim() == "")
                {
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_RECNO_EMPTY"));
                    this.ucLabelEditRecNo.TextFocus(false, true);
                    return;
                }

                //判断批号是否为空
                if (this.ucLabelEditLotNo.Value == "" || this.ucLabelEditLotNo.Value.Trim() == "")
                {
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_LOTNO_EMPTY"));
                    this.ucLabelEditLotNo.TextFocus(false, true);
                    return;
                }

                if (this.ucLabelEditLotNo.Value.Trim().Length < 10)
                {
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_LOTNO_IS_LESS_THAN_TEM"));
                    this.ucLabelEditLotNo.TextFocus(false, true);
                    return;
                }


                if (warehouseFacade == null)
                {
                    warehouseFacade = new WarehouseFacade(this.DataProvider);
                }

                if (this.ucLabelComboxLineNo.SelectedItemText == "" || this.ucLabelComboxLineNo.SelectedItemText.Trim() == "")
                {
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$ERROR_LINENO_EMPTY"));
                    return;
                }
                object obj = _IQCFacade.GetInvReceiptDetail(this.ucLabelEditRecNo.Value.Trim(), int.Parse(this.ucLabelComboxLineNo.SelectedItemText.Trim()));

                if (obj != null)
                {
                    if ((obj as Domain.IQC.InvReceiptDetail).Recstatus.ToUpper() == RecStatus.CLOSE)
                    {
                        ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_RECSTATUS_CLOSE"));
                        return;
                    }

                    object objMaterial = this.GetMaterial();
                    if (objMaterial != null)
                    {
                        //判断批号在ItemLot中是否存在
                        object objItemLot = _InventoryFacade.GetItemLot(this.ucLabelEditLotNo.Value.Trim(), (objMaterial as Domain.MOModel.Material).MaterialCode);
                        if (objItemLot != null)
                        {
                            ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$ERROR_LOTNO_EXIST"));
                            this.ucLabelEditLotNo.TextFocus(false, true);
                            return;
                        }
                    }

                    int qty = 0;

                    if (true)
                    {
                        lotno.Clear();

                        Domain.IQC.InvReceiptDetail invreceiptdetail = obj as Domain.IQC.InvReceiptDetail;
                        string controlType = this.GetMControlType().ToUpper();
                        if (controlType == MControlType.ITEM_CONTROL_LOT)
                        {
                            decimal total = invreceiptdetail.Qualifyqty;

                            object[] objItemLots = _InventoryFacade.QueryItemLot(invreceiptdetail.Itemcode, this.ucLabelEditRecNo.Value.Trim(), int.Parse(this.ucLabelComboxLineNo.SelectedItemText.Trim()), orgid);

                            if (objItemLots != null)
                            {
                                decimal sum = 0;
                                foreach (ItemLot itemlot in objItemLots)
                                {
                                    sum += itemlot.Lotqty;
                                }
                                total = total - sum;

                                if (total == 0)
                                {
                                    CreateNewLot(0, false, invreceiptdetail);
                                }

                                else if (this.ucLabelEditBZCount.Value != "" && this.ucLabelEditBZCount.Value.Trim() != "")
                                {
                                    if (decimal.Parse(this.ucLabelEditBZCount.Value.Trim()) <= 0)
                                    {
                                        this.ucLabelEditBZCount.Value = "";
                                        this.ucLabelEditBZCount.TextFocus(false, true);
                                        return;
                                    }

                                    decimal tempCount = decimal.Parse(this.ucLabelEditBZCount.Value.Trim());

                                    if (total > tempCount)
                                    {
                                        CreateNewLot(tempCount, false, invreceiptdetail);
                                    }
                                    else
                                    {
                                        CreateNewLot(total, false, invreceiptdetail);
                                    }
                                }
                            }
                            else
                            {
                                if (this.ucLabelEditBZCount.Value != "" && this.ucLabelEditBZCount.Value.Trim() != "")
                                {
                                    if (decimal.Parse(this.ucLabelEditBZCount.Value.Trim()) <= 0)
                                    {
                                        this.ucLabelEditBZCount.Value = "";
                                        this.ucLabelEditBZCount.TextFocus(false, true);
                                        return;
                                    }

                                    decimal tempCount = decimal.Parse(this.ucLabelEditBZCount.Value.Trim());

                                    if (total > tempCount)
                                    {
                                        CreateNewLot(tempCount, false, invreceiptdetail);
                                    }
                                    else
                                    {
                                        CreateNewLot(total, false, invreceiptdetail);
                                    }
                                }
                            }
                        }
                        else if (controlType == MControlType.ITEM_CONTROL_KEYPARTS)
                        {
                            CreateNewLot(0, false, invreceiptdetail);
                        }
                        else if (controlType == MControlType.ITEM_CONTROL_NOCONTROL)
                        {
                            return;
                        }
                        else
                        {
                            return;
                        }

                        this.ultraGridMain.DataSource = this.m_CheckList;
                        this.ultraGridMain.UpdateData();
                    }

                    for (int i = 0; i < this.ultraGridMain.Rows.Count; i++)
                    {
                        Infragistics.Win.UltraWinGrid.UltraGridRow row = ultraGridMain.Rows[i];
                        if (row.Band.Index == 0)
                        {
                            row.Cells["Checked"].Value = false;
                            if (lotno.ContainsKey(row.Cells["LotNO"].Value.ToString()))
                            {
                                row.Cells["Checked"].Value = true;
                            }
                        }
                    }

                    //生成后调用保存方法
                    Save(false);//ucBtnSave_Click(null, null);
                    this.ucLabelEditLotNo.Value = "";
                    this.ucLabelEditLotNo.Focus();
                    lotno.Clear();
                }


            }
        }

        //入库单号扫描
        private void ucLabelEditRecNo_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (this.ucLabelEditRecNo.Value == "" || this.ucLabelEditRecNo.Value.Trim() == "")
                {
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_RECNO_EMPTY"));
                    this.ucLabelEditRecNo.TextFocus(false, true);
                    return;
                }
                if (_IQCFacade == null)
                {
                    _IQCFacade = new IQCFacade(this.DataProvider);
                }

                object objInvReceipt = _IQCFacade.GetInvReceipt(this.ucLabelEditRecNo.Value);
                if (objInvReceipt != null)
                {
                    orgid = (objInvReceipt as Domain.IQC.InvReceipt).Orgid;
                    if ((objInvReceipt as Domain.IQC.InvReceipt).Rectype == "WX")
                    {
                        this.radioButtonRCardInput.Checked = true;
                    }
                }
                else
                {
                    return;
                }

                ucLabelComboxLineNo_Load();

                if (this.ucLabelComboxLineNo.SelectedItemText.Trim() != "")
                {

                }
                else
                {
                    this.ucLEVenderQuery.Value = "";
                    this.ucLabelEditItemCode.Value = "";
                    this.itemcode = string.Empty;
                    this.ucLabelEditSHCount.Value = "";
                    this.ucLUint.Value = "";
                    this.ucLabelEditMControlType.Value = "";
                    LoadCheckList(this.ucLabelEditRecNo.Value.Trim(), this.ucLabelComboxLineNo.SelectedItemText.Trim());
                }
            }
        }

        private object GetMaterial()
        {
            if (_IQCFacade == null)
            {
                _IQCFacade = new IQCFacade(this.DataProvider);
            }
            object[] objs = _IQCFacade.QueryInvReceiptDetail(this.ucLabelEditRecNo.Value.Trim(), int.Parse(this.ucLabelComboxLineNo.SelectedItemText.Trim()));

            if (objs != null && objs.Length > 0)
            {
                Domain.IQC.InvReceiptDetail detail = objs[0] as Domain.IQC.InvReceiptDetail;
                //if (warehouseFacade == null)
                //{
                //    warehouseFacade = new WarehouseFacade(this.DataProvider);
                //}

                object obj = _IQCFacade.GetInvReceipt(this.ucLabelEditRecNo.Value.Trim());

                if (obj != null)
                {
                    if (_ItemFacade == null)
                    {
                        _ItemFacade = new ItemFacade(this.DataProvider);
                    }
                    object objmaterial = _ItemFacade.GetMaterial((detail).Itemcode, (obj as Domain.IQC.InvReceipt).Orgid);

                    return objmaterial;
                }
            }

            return null;
        }

        //获取物料管控类型
        private string GetMControlType()
        {
            object obj = this.GetMaterial();
            if (obj != null)
            {
                return (obj as Domain.MOModel.Material).MaterialControlType;
            }
            return "";
        }
        private string itemcode=string.Empty;
        private void ucLabelComboxLineNo_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (_IQCFacade == null)
            {
                _IQCFacade = new IQCFacade(this.DataProvider);
            }

            //ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Normal,""));
            this.ucLabelEditRecNo.Focus();

            object obj = _IQCFacade.GetInvReceipt(this.ucLabelEditRecNo.Value.Trim());

            if (obj != null)
            {
                this.ucLEVenderQuery.Value = (obj as Domain.IQC.InvReceipt).Vendorcode;
            }

            object[] objs = _IQCFacade.QueryInvReceiptDetail(this.ucLabelEditRecNo.Value.Trim(), int.Parse(this.ucLabelComboxLineNo.SelectedItemText.Trim()));

            if (objs != null && objs.Length > 0)
            {
                Domain.IQC.InvReceiptDetail detail = objs[0] as Domain.IQC.InvReceiptDetail;

                if (_ItemFacade == null)
                {
                    _ItemFacade = new ItemFacade(this.DataProvider);
                }

                object objmaterial = _ItemFacade.GetMaterial(detail.Itemcode, orgid);

                if (objmaterial != null)
                {
                    this.ucLabelEditItemCode.Value = detail.Itemcode + "-" + (objmaterial as Domain.MOModel.Material).MaterialDescription;
                    this.itemcode = detail.Itemcode;
                    if ((objmaterial as Domain.MOModel.Material).MaterialControlType.ToUpper() == MControlType.ITEM_CONTROL_NOCONTROL)
                    {

                        this.checkBoxRcardCheck.Enabled = false;
                        this.radioButton_Rcard.Enabled = false;
                        this.radioButton_StartRcard.Enabled = false;
                        this.ucLabelEditRCardQuery.Enabled = false;
                        this.ucLERCStartRcard.Enabled = false;
                        this.ucLabelEditRcardCount.Enabled = false;
                        this.ucLabelEditPrintCount.Enabled = false;
                        this.ucLabelComboxPrintList.Enabled = false;
                        this.ucLabelComboxPrintTemplete.Enabled = false;
                        this.ucButtonCalcRCardEnd.Enabled = false;
                        this.ucButtonPrint.Enabled = false;
                        this.ucBtnDelete.Enabled = false;
                        this.ucBtnSave.Enabled = false;
                        this.ucButtonNewLot.Enabled = false;
                        this.ucButtonRcarInput.Enabled = false;
                        this.radioButtonRCardInput.Enabled = false;
                        this.ucButtonRcardByMo.Enabled = false;
                        this.radioButtonRCardByMo.Enabled = false;
                        this.ucButtonRcardByOQC.Enabled = false;
                        this.radioButtonRCardByOQC.Enabled = false;
                        this.ucLabelEditBZCount.Enabled = false;
                        this.ucLabelEditLotNo.Enabled = false;
                        //this.ucLabelEditMControlType.Enabled = false;
                    }
                    else if ((objmaterial as Domain.MOModel.Material).MaterialControlType.ToUpper() == MControlType.ITEM_CONTROL_LOT)
                    {
                        this.radioButton_Rcard.Enabled = false;
                        this.radioButton_StartRcard.Enabled = false;
                        this.ucLabelEditRCardQuery.Enabled = false;
                        this.ucLERCStartRcard.Enabled = false;
                        this.ucLabelEditRcardCount.Enabled = false;
                        this.ucButtonCalcRCardEnd.Enabled = false;
                        this.ucButtonRcarInput.Enabled = false;
                        this.radioButtonRCardInput.Enabled = false;
                        this.ucButtonRcardByMo.Enabled = false;
                        this.radioButtonRCardByMo.Enabled = false;
                        this.ucButtonRcardByOQC.Enabled = false;
                        this.radioButtonRCardByOQC.Enabled = false;

                        this.ucLabelEditBZCount.Enabled = true;
                        this.ucLabelEditLotNo.Enabled = true;
                        this.ucButtonPrint.Enabled = true;
                        this.ucBtnDelete.Enabled = true;
                        this.ucBtnSave.Enabled = true;
                        this.ucButtonNewLot.Enabled = true;
                        this.ucLabelComboxPrintTemplete.Enabled = true;
                        ucLabelEditPrintCount.Enabled = true;
                        ucLabelComboxPrintTemplete.Enabled = true;
                        ucLabelComboxPrintList.Enabled = true;
                    }
                    else if ((objmaterial as Domain.MOModel.Material).MaterialControlType.ToUpper() == MControlType.ITEM_CONTROL_KEYPARTS)
                    {
                        this.checkBoxRcardCheck.Enabled = true;
                        this.radioButton_Rcard.Enabled = true;
                        this.radioButton_StartRcard.Enabled = true;
                        this.ucLabelEditRCardQuery.Enabled = true;
                        this.ucLERCStartRcard.Enabled = true;
                        this.ucLabelEditRcardCount.Enabled = true;
                        this.ucLabelEditPrintCount.Enabled = true;
                        this.ucLabelComboxPrintList.Enabled = true;
                        this.ucLabelComboxPrintTemplete.Enabled = true;
                        this.ucButtonCalcRCardEnd.Enabled = true;
                        this.ucButtonPrint.Enabled = true;
                        this.ucBtnDelete.Enabled = true;
                        this.ucBtnSave.Enabled = true;
                        this.ucButtonNewLot.Enabled = true;
                        this.ucButtonRcarInput.Enabled = true;
                        this.radioButtonRCardInput.Enabled = true;
                        this.ucButtonRcardByMo.Enabled = true;
                        this.radioButtonRCardByMo.Enabled = true;
                        this.ucButtonRcardByOQC.Enabled = true;
                        this.radioButtonRCardByOQC.Enabled = true;

                        this.ucLabelEditBZCount.Enabled = true;
                        this.ucLabelEditLotNo.Enabled = true;
                        //this.ucLabelEditMControlType.Enabled = true;
                    }

                    object objReceipt = _IQCFacade.GetInvReceiptDetail(this.ucLabelEditRecNo.Value.Trim(), int.Parse(this.ucLabelComboxLineNo.SelectedItemText.Trim()));
                    if (objReceipt != null)
                    {
                        this.ucLabelEditSHCount.Value = (objReceipt as Domain.IQC.InvReceiptDetail).Qualifyqty.ToString();
                        this.ucLabelEditBZCount.Value = this.ucLabelEditSHCount.Value;
                        this.ucLabelEditMemo.Value = (objReceipt as Domain.IQC.InvReceiptDetail).Memo;

                        Domain.IQC.InvReceiptDetail invReceiptDetail = objReceipt as Domain.IQC.InvReceiptDetail;
                        if (invReceiptDetail.Recstatus.Equals("Close") || (invReceiptDetail.Recstatus.Equals("WaitCheck") && (invReceiptDetail.Iqcstatus.Equals("WaitCheck") || invReceiptDetail.Iqcstatus.Equals("UNQualified"))))
                        {
                            this.ucLabelEditBZCount.Enabled = false;
                            this.ucLabelEditLotNo.Enabled = false;
                            this.ucButtonNewLot.Enabled = false;
                            this.ucBtnDelete.Enabled = false;
                            this.ucBtnSave.Enabled = false;

                            this.checkBoxRcardCheck.Enabled = false;
                            this.radioButton_StartRcard.Enabled = false;
                            this.ucLabelEditRCardQuery.Enabled = false;
                            this.ucLERCStartRcard.Enabled = false;
                            this.ucLabelEditRcardCount.Enabled = false;
                            this.ucButtonCalcRCardEnd.Enabled = false;
                        }
                    }

                    //this.ucLabelEditSHCount.Value = detail.ReceiveQty.ToString();
                    this.ucLUint.Value = (objmaterial as Domain.MOModel.Material).MaterialUOM;
                    //if ((objmaterial as Domain.MOModel.Material).MaterialControlType.ToUpper() == MControlType.ITEM_CONTROL_NOCONTROL)
                    //{
                    //    this.ucLabelEditMControlType.Value = "非管控";
                    //}
                    //else if ((objmaterial as Domain.MOModel.Material).MaterialControlType.ToUpper() == MControlType.ITEM_CONTROL_LOT)
                    //{
                    //    this.ucLabelEditMControlType.Value = "批管控";
                    //}
                    //else if ((objmaterial as Domain.MOModel.Material).MaterialControlType.ToUpper() == MControlType.ITEM_CONTROL_KEYPARTS)
                    //{
                    //    this.ucLabelEditMControlType.Value = "单件管控";
                    //}
                    this.ucLabelEditMControlType.Value = MutiLanguages.ParserString((objmaterial as Domain.MOModel.Material).MaterialControlType);

                    InitializeMainGrid();
                    LoadCheckList(this.ucLabelEditRecNo.Value.Trim(), this.ucLabelComboxLineNo.SelectedItemText.Trim());
                }
                else
                {
                }

            }
            else
            {
                this.ucLabelEditItemCode.Value = "";
                this.itemcode = string.Empty;
                this.ucLUint.Value = "";
                this.ucLabelEditSHCount.Value = "";
                this.ucLEVenderQuery.Value = "";
                this.ucLabelEditMControlType.Value = "";
                this.ucLabelEditBZCount.Value = "";
                LoadCheckList(this.ucLabelEditRecNo.Value.Trim(), this.ucLabelComboxLineNo.SelectedItemText.Trim());
            }
        }

        #region 序列号转化
        protected string cmdCalCard(string Itemcode)
        {
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);
            string suffix = Itemcode;
            if (suffix.Length >= 10)
            {
                suffix = Itemcode.Substring(0, 10);
            }

            //处理日期
            int date = dbDateTime.DBDate;

            string year = date.ToString().Substring(0, 4);
            string month = date.ToString().Substring(4, 2);
            string day = date.ToString().Substring(6, 2);

            string formatDate = year.Substring(2) + DtoX(int.Parse(month)) + day;

            if (warehouseFacade == null)
            {
                warehouseFacade = new WarehouseFacade(this.DataProvider);
            }
            string maxserial = warehouseFacade.GetMaxSerial(suffix + formatDate);

            //如果已是最大值就返回为空
            if (maxserial == "999")
            {
                return "";
            }
            string orgId = string.Format("{0:00}", orgid);
            SERIALBOOK serialbook = new SERIALBOOK();

            if (maxserial == "")
            {
                serialbook.SNPrefix = suffix + formatDate;
                serialbook.MaxSerial = "1";
                serialbook.MDate = dbDateTime.DBDate;
                serialbook.MTime = dbDateTime.DBTime;
                serialbook.MUser = ApplicationService.Current().UserCode;
                warehouseFacade.AddSerialBook(serialbook);
                return orgId + suffix + formatDate + string.Format("{0:000}", int.Parse(serialbook.MaxSerial));
            }
            else
            {
                serialbook.SNPrefix = suffix + formatDate;
                serialbook.MaxSerial = (int.Parse(maxserial) + 1).ToString();
                serialbook.MDate = dbDateTime.DBDate;
                serialbook.MTime = dbDateTime.DBTime;
                serialbook.MUser = ApplicationService.Current().UserCode;
                warehouseFacade.UpdateSerialBook(serialbook);
                return orgId + suffix + formatDate + string.Format("{0:000}", int.Parse(serialbook.MaxSerial));
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

        #region 判断当前入库单状态

        private bool CheckRecStatus()
        {
            if (_IQCFacade == null)
            {
                _IQCFacade = new IQCFacade(this.DataProvider);
            }

            object obj = _IQCFacade.GetInvReceiptDetail(this.ucLabelEditRecNo.Value.Trim(), int.Parse(this.ucLabelComboxLineNo.SelectedItemText.Trim()));

            if (obj != null)
            {
                if ((obj as Domain.IQC.InvReceiptDetail).Recstatus.ToUpper() == RecStatus.CLOSE)
                {
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_RECSTATUS_CLOSE"));
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        #endregion

        private void ucLabelEditRecNo_InnerTextChanged(object sender, EventArgs e)
        {
            if ((repNO != "" || repNO.Trim() != "") && (LineNO != "" || LineNO.Trim() != ""))
            {
                if (_IQCFacade == null)
                {
                    _IQCFacade = new IQCFacade(this.DataProvider);
                }
                object objInvReceipt = _IQCFacade.GetInvReceipt(this.ucLabelEditRecNo.Value);
                if (objInvReceipt != null)
                {
                    orgid = (objInvReceipt as Domain.IQC.InvReceipt).Orgid;
                    if ((objInvReceipt as Domain.IQC.InvReceipt).Rectype == "WX")
                    {
                        this.radioButtonRCardInput.Checked = true;
                    }
                }
                else
                {
                    return;
                }
                ucLabelComboxLineNo_Load();
                this.ucLabelComboxLineNo.SetSelectItemText(LineNO);
                if (this.ucLabelComboxLineNo.SelectedItemText.Trim() != "")
                {
                }
                else
                {
                    this.ucLEVenderQuery.Value = "";
                    this.ucLabelEditItemCode.Value = "";
                    this.itemcode = string.Empty;
                    this.ucLabelEditSHCount.Value = "";
                    this.ucLUint.Value = "";
                    this.ucLabelEditMControlType.Value = "";
                    LoadCheckList(this.ucLabelEditRecNo.Value.Trim(), this.ucLabelComboxLineNo.SelectedItemText.Trim());
                }
            }
        }


        //导入外协工单序列号
        private void ucButtonRcarInput_Click(object sender, EventArgs e)
        {
            if (!this.radioButtonRCardInput.Checked)
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_INPUTRCARD_NO_SELECTED"));
                return;
            }
            if (this.ucLabelEditRecNo.Value == "" || this.ucLabelEditRecNo.Value.Trim() == "")
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$ERROR_REGNO_EMPTY"));
                return;
            }

            if (this.ucLabelComboxLineNo.SelectedIndex == -1)
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$ERROR_REGNO_LINENO_EMPTY"));
                return;
            }

            if (!CheckRecStatus())
            {
                return;
            }

            if (!CheckSelectOnlyOneLot())
            {
                return;
            }

            FRCardQuery objForm = new FRCardQuery();
            objForm.RCardSelectedEvent += new ParentChildRelateEventHandler<ParentChildRelateEventArgs<string>>(RCardSelector_RCardSelectedEvent);
            objForm.ShowDialog();
        }


        private void RCardSelector_RCardSelectedEvent(object sender, ParentChildRelateEventArgs<string> e)
        {
            if (e.CustomObject.Length > 1)
            {
                string[] rcards = e.CustomObject.Split(',');

                UltraGridRow row = this.GetLotNoRow();

                if (!CheckRecStatus())
                {
                    return;
                }

                if (!CheckSelectOnlyOneLot())
                {
                    return;
                }

                foreach (string rcard in rcards)
                {
                    if (!CheckRCardIsSame(rcard))
                    {
                        return;
                    }

                    if (!CheckRCardChecked(rcard))
                    {
                        ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_SERIALNO_LOTNO_ERROR"));
                        return;
                    }
                }

                foreach (string rcard in rcards)
                {
                    BandingRCard(rcard, row);
                }

                Save(true);
            }
        }

        private void FKeyPart_FormClosed(object sender, FormClosedEventArgs e)
        {
            //使用批内数量更新在收料入库页面中入库数量
            lotCount = 0;
            for (int j = 0; j < ultraGridMain.Rows.Count; j++)
            {
                lotCount += decimal.Parse(this.ultraGridMain.Rows[j].Cells["LOTQTY"].Value.ToString());
            }
        }

        private void checkBox_selectedALL_CheckedChanged(object sender, EventArgs e)
        {

            for (int i = 0; i < ultraGridMain.Rows.Count; i++)
            {
                Infragistics.Win.UltraWinGrid.UltraGridRow row = ultraGridMain.Rows[i];

                if (row.Band.Index == 0)
                {
                    if (this.checkBox_selectedALL.Checked)
                    {
                        row.Cells["Checked"].Value = true;
                    }
                    else
                    {
                        row.Cells["Checked"].Value = false;
                    }

                    if (row.HasChild())
                    {
                        for (int j = 0; j < row.ChildBands[0].Rows.Count; j++)
                        {

                            if (this.checkBox_selectedALL.Checked)
                            {
                                row.ChildBands[0].Rows[j].Cells["Checked"].Value = true;
                            }
                            else
                            {
                                row.ChildBands[0].Rows[j].Cells["Checked"].Value = false;
                            }

                        }
                    }

                }
            }

            this.ultraGridMain.Update();
        }

        private void radioButtonRcard_CheckedChanged(object sender, EventArgs e)
        {
            this.ucLERCStartRcard.Enabled = false;
            this.ucLabelEditRcardCount.Enabled = false;
            this.ucButtonCalcRCardEnd.Enabled = false;
            this.ucButtonRcarInput.Enabled = false;
            this.ucLabelEditRCardQuery.Enabled = true;

            this.ucButtonRcardByMo.Enabled = false;
            this.ucButtonRcardByOQC.Enabled = false;
        }

        private void radioButton_StartRcard_CheckedChanged(object sender, EventArgs e)
        {
            this.ucButtonRcarInput.Enabled = false;
            this.ucLabelEditRCardQuery.Enabled = false;
            this.ucLabelEditRcardCount.Enabled = true;
            this.ucButtonCalcRCardEnd.Enabled = true;
            this.ucLERCStartRcard.Enabled = true;

            this.ucButtonRcardByMo.Enabled = false;
            this.ucButtonRcardByOQC.Enabled = false;
        }

        private void radioButtonRCardInput_CheckedChanged(object sender, EventArgs e)
        {
            this.ucLERCStartRcard.Enabled = false;
            this.ucLabelEditRcardCount.Enabled = false;
            this.ucButtonCalcRCardEnd.Enabled = false;

            this.ucLabelEditRCardQuery.Enabled = false;

            this.ucButtonRcarInput.Enabled = true;

            this.ucButtonRcardByMo.Enabled = false;
            this.ucButtonRcardByOQC.Enabled = false;
        }

        private void radioButtonRCardByMo_CheckedChanged(object sender, EventArgs e)
        {
            this.ucLERCStartRcard.Enabled = false;
            this.ucLabelEditRcardCount.Enabled = false;
            this.ucButtonCalcRCardEnd.Enabled = false;

            this.ucLabelEditRCardQuery.Enabled = false;

            this.ucButtonRcarInput.Enabled = false;

            this.ucButtonRcardByMo.Enabled = true;
            this.ucButtonRcardByOQC.Enabled = false;
        }

        private void radioButtonRCardByOQC_CheckedChanged(object sender, EventArgs e)
        {
            this.ucLERCStartRcard.Enabled = false;
            this.ucLabelEditRcardCount.Enabled = false;
            this.ucButtonCalcRCardEnd.Enabled = false;

            this.ucLabelEditRCardQuery.Enabled = false;

            this.ucButtonRcarInput.Enabled = false;

            this.ucButtonRcardByMo.Enabled = false;
            this.ucButtonRcardByOQC.Enabled = true;
        }

        private void ucButtonRcardByMo_Click(object sender, EventArgs e)
        {
            if (!this.radioButtonRCardByMo.Checked)
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_INPUTRCARDByMo_NO_SELECTED"));
                return;
            }
            if (this.ucLabelEditRecNo.Value == "" || this.ucLabelEditRecNo.Value.Trim() == "")
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$ERROR_REGNO_EMPTY"));
                return;
            }

            if (this.ucLabelComboxLineNo.SelectedIndex == -1)
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$ERROR_REGNO_LINENO_EMPTY"));
                return;
            }

            if (!CheckRecStatus())
            {
                return;
            }

            if (!CheckSelectOnlyOneLot())
            {
                return;
            }

            FRcardImprotByMo objForm = new FRcardImprotByMo();
            objForm.ItemCode = this.itemcode;
            objForm.RCardSelectedEvent += new ParentChildRelateEventHandler<ParentChildRelateEventArgs<string>>(RCardSelector_RCardSelectedEvent);
            objForm.ShowDialog();
        }

        private void ucButtonRcardByOQC_Click(object sender, EventArgs e)
        {
            if (!this.radioButtonRCardByOQC.Checked)
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_INPUTRCARDByOQC_NO_SELECTED"));
                return;
            }
            if (this.ucLabelEditRecNo.Value == "" || this.ucLabelEditRecNo.Value.Trim() == "")
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$ERROR_REGNO_EMPTY"));
                return;
            }

            if (this.ucLabelComboxLineNo.SelectedIndex == -1)
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$ERROR_REGNO_LINENO_EMPTY"));
                return;
            }

            if (!CheckRecStatus())
            {
                return;
            }

            if (!CheckSelectOnlyOneLot())
            {
                return;
            }

            FRcardImprotByOQC objForm = new FRcardImprotByOQC();
            objForm.ItemCode = this.itemcode;
            objForm.RCardSelectedEvent += new ParentChildRelateEventHandler<ParentChildRelateEventArgs<string>>(RCardSelector_RCardSelectedEvent);
            objForm.ShowDialog();
        }



    }

    public class MControlType
    {
        public static string ITEM_CONTROL_KEYPARTS = "ITEM_CONTROL_KEYPARTS";
        public static string ITEM_CONTROL_LOT = "ITEM_CONTROL_LOT";
        public static string ITEM_CONTROL_NOCONTROL = "ITEM_CONTROL_NOCONTROL";

    }

    public class RecStatus
    {
        public static string NEW = "NEW";
        public static string WAITCHECK = "WAITCHECK";
        public static string CLOSE = "CLOSE";

    }

}

