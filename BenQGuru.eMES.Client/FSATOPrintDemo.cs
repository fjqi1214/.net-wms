using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Client.Service;
using BenQGuru.eMES.CodeSoftPrint;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Report;
using BenQGuru.eMES.Web.Helper;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using UserControl;
using BenQGuru.eMES.SATOPrint;

//**********此页面用于SATO打印机，用模板传参打印的DEMO,参照TDK项目***************
//*        SATO M84 PRO2   这个是打印CHIT LABEL的                               *
//*        SATO CT412iTT   这个是打印SN的                                       *
//*        模板路径在\bin\PrintLabel\                                           *
//*        Add By Terry 2013-4-15                                               *
//*******************************************************************************
namespace BenQGuru.eMES.Client
{
    public class FSATOPrintDemo : BaseForm
    {
        private bool _IsBatchPrint = true;
        private string _DataDescFileName = "Label.dsc";
        public PrintTemplate[] _PrintTemplateList = null;
        private DataSet m_CheckList = null;
        private DataTable m_mo2RcardLink = null;
        private string currentItemCode = string.Empty;
        public static decimal lotCount = 0;
        public SystemSettingFacade _systemFacade = null;
        public ReportFacade _reprotFacade = null;
        public string printRcard = string.Empty; //SerialNumber打印时用来存第一行值的变量
        public string printRcardLineTwo = string.Empty; //SerialNumber打印时用来存第二行值的变量
        bool IsNeedPassword = false; //打印时判断是否需要密码
        Hashtable lotno = new Hashtable();

        private struct RcardInfoForPrint
        {
            public string Rcard;
            public string Itemdesc;
            public string Item;
        }

        #region 自动生成

        private System.Windows.Forms.GroupBox grpQuery;
        private System.Windows.Forms.Panel panelButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private UserControl.UCButton ucBtnDelete;
        private UserControl.UCButton ucBtnExit;
        private System.ComponentModel.IContainer components = null;
        private UCLabelEdit ucLabelEditRCardPrefix;
        private UCLabelEdit ucLabelEditPrintCount;
        public UCLabelCombox ucLabelComboxPrintTemplete;
        public UCLabelCombox ucLabelComboxPrintList;
        public UCButton ucButtonPrint;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGridMain;
        private UCLabelEdit ucLERCStartRcard;
        private CheckBox checkBoxNoPrint;
        private UCLabelEdit ucLabelRCardQuery;
        private UCButton ucButtonQuery;
        public UCLabelCombox ucLabelComboxCheckType;
        private CheckBox checkBoxSNContentCheck;
        private UCLabelEdit ucLERCEndRcard;
        private UCLabelEdit txtMoCodeEdit;
        private RadioButton radioButton4;
        private UCLabelEdit ucLabelRCardLength;
        private UCButton ucButtonCalcRCardEnd;
        private UCButton ucButtonExit;
        private UCLabelEdit ucLabelCreateQTY;
        public Infragistics.Win.UltraWinEditors.UltraOptionSet ultraOptionSetScale;
        public UCLabelCombox ucLabelPrintType;
        private Infragistics.Win.UltraWinProgressBar.UltraProgressBar processBar;
        private UCLabelEdit ucLabelEditlast;
        private UCLabelEdit txtMoCodeQuery;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FSATOPrintDemo));
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.ValueListItem valueListItem1 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.ValueListItem valueListItem2 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.ValueListItem valueListItem3 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
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
            this.txtMoCodeQuery = new UserControl.UCLabelEdit();
            this.ucLabelRCardQuery = new UserControl.UCLabelEdit();
            this.ucButtonQuery = new UserControl.UCButton();
            this.panelButton = new System.Windows.Forms.Panel();
            this.ucBtnExit = new UserControl.UCButton();
            this.ucButtonPrint = new UserControl.UCButton();
            this.ucLabelComboxPrintTemplete = new UserControl.UCLabelCombox();
            this.ucLabelEditPrintCount = new UserControl.UCLabelEdit();
            this.ucLabelComboxPrintList = new UserControl.UCLabelCombox();
            this.checkBoxNoPrint = new System.Windows.Forms.CheckBox();
            this.checkBoxSNContentCheck = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ucLabelEditlast = new UserControl.UCLabelEdit();
            this.ucLabelPrintType = new UserControl.UCLabelCombox();
            this.ultraOptionSetScale = new Infragistics.Win.UltraWinEditors.UltraOptionSet();
            this.ucButtonExit = new UserControl.UCButton();
            this.ucButtonCalcRCardEnd = new UserControl.UCButton();
            this.txtMoCodeEdit = new UserControl.UCLabelEdit();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.ucLERCEndRcard = new UserControl.UCLabelEdit();
            this.ucLabelComboxCheckType = new UserControl.UCLabelCombox();
            this.ucBtnDelete = new UserControl.UCButton();
            this.ucLabelRCardLength = new UserControl.UCLabelEdit();
            this.ucLabelCreateQTY = new UserControl.UCLabelEdit();
            this.ucLabelEditRCardPrefix = new UserControl.UCLabelEdit();
            this.ucLERCStartRcard = new UserControl.UCLabelEdit();
            this.processBar = new Infragistics.Win.UltraWinProgressBar.UltraProgressBar();
            this.ultraGridMain = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.grpQuery.SuspendLayout();
            this.panelButton.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraOptionSetScale)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridMain)).BeginInit();
            this.SuspendLayout();
            // 
            // grpQuery
            // 
            this.grpQuery.Controls.Add(this.txtMoCodeQuery);
            this.grpQuery.Controls.Add(this.ucLabelRCardQuery);
            this.grpQuery.Controls.Add(this.ucButtonQuery);
            this.grpQuery.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpQuery.Location = new System.Drawing.Point(0, 0);
            this.grpQuery.Name = "grpQuery";
            this.grpQuery.Size = new System.Drawing.Size(647, 39);
            this.grpQuery.TabIndex = 0;
            this.grpQuery.TabStop = false;
            // 
            // txtMoCodeQuery
            // 
            this.txtMoCodeQuery.AllowEditOnlyChecked = true;
            this.txtMoCodeQuery.AutoSelectAll = false;
            this.txtMoCodeQuery.AutoUpper = true;
            this.txtMoCodeQuery.Caption = "工单代码";
            this.txtMoCodeQuery.Checked = false;
            this.txtMoCodeQuery.EditType = UserControl.EditTypes.String;
            this.txtMoCodeQuery.Location = new System.Drawing.Point(24, 13);
            this.txtMoCodeQuery.MaxLength = 40;
            this.txtMoCodeQuery.Multiline = false;
            this.txtMoCodeQuery.Name = "txtMoCodeQuery";
            this.txtMoCodeQuery.PasswordChar = '\0';
            this.txtMoCodeQuery.ReadOnly = false;
            this.txtMoCodeQuery.ShowCheckBox = false;
            this.txtMoCodeQuery.Size = new System.Drawing.Size(194, 24);
            this.txtMoCodeQuery.TabIndex = 59;
            this.txtMoCodeQuery.TabNext = false;
            this.txtMoCodeQuery.Value = "";
            this.txtMoCodeQuery.WidthType = UserControl.WidthTypes.Normal;
            this.txtMoCodeQuery.XAlign = 85;
            // 
            // ucLabelRCardQuery
            // 
            this.ucLabelRCardQuery.AllowEditOnlyChecked = true;
            this.ucLabelRCardQuery.AutoSelectAll = false;
            this.ucLabelRCardQuery.AutoUpper = true;
            this.ucLabelRCardQuery.Caption = "产品序列号";
            this.ucLabelRCardQuery.Checked = false;
            this.ucLabelRCardQuery.EditType = UserControl.EditTypes.String;
            this.ucLabelRCardQuery.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ucLabelRCardQuery.Location = new System.Drawing.Point(244, 13);
            this.ucLabelRCardQuery.MaxLength = 40;
            this.ucLabelRCardQuery.Multiline = false;
            this.ucLabelRCardQuery.Name = "ucLabelRCardQuery";
            this.ucLabelRCardQuery.PasswordChar = '\0';
            this.ucLabelRCardQuery.ReadOnly = false;
            this.ucLabelRCardQuery.ShowCheckBox = false;
            this.ucLabelRCardQuery.Size = new System.Drawing.Size(206, 24);
            this.ucLabelRCardQuery.TabIndex = 58;
            this.ucLabelRCardQuery.TabNext = true;
            this.ucLabelRCardQuery.Value = "";
            this.ucLabelRCardQuery.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabelRCardQuery.XAlign = 317;
            // 
            // ucButtonQuery
            // 
            this.ucButtonQuery.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonQuery.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonQuery.BackgroundImage")));
            this.ucButtonQuery.ButtonType = UserControl.ButtonTypes.Query;
            this.ucButtonQuery.Caption = "查询";
            this.ucButtonQuery.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonQuery.Location = new System.Drawing.Point(491, 13);
            this.ucButtonQuery.Name = "ucButtonQuery";
            this.ucButtonQuery.Size = new System.Drawing.Size(88, 22);
            this.ucButtonQuery.TabIndex = 57;
            this.ucButtonQuery.Click += new System.EventHandler(this.ucButtonQuery_Click);
            // 
            // panelButton
            // 
            this.panelButton.Controls.Add(this.ucBtnExit);
            this.panelButton.Controls.Add(this.ucButtonPrint);
            this.panelButton.Controls.Add(this.ucLabelComboxPrintTemplete);
            this.panelButton.Controls.Add(this.ucLabelEditPrintCount);
            this.panelButton.Controls.Add(this.ucLabelComboxPrintList);
            this.panelButton.Controls.Add(this.checkBoxNoPrint);
            this.panelButton.Controls.Add(this.checkBoxSNContentCheck);
            this.panelButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButton.Location = new System.Drawing.Point(0, 349);
            this.panelButton.Name = "panelButton";
            this.panelButton.Size = new System.Drawing.Size(647, 80);
            this.panelButton.TabIndex = 4;
            // 
            // ucBtnExit
            // 
            this.ucBtnExit.BackColor = System.Drawing.SystemColors.Control;
            this.ucBtnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucBtnExit.BackgroundImage")));
            this.ucBtnExit.ButtonType = UserControl.ButtonTypes.Exit;
            this.ucBtnExit.Caption = "退出";
            this.ucBtnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnExit.Location = new System.Drawing.Point(328, 55);
            this.ucBtnExit.Name = "ucBtnExit";
            this.ucBtnExit.Size = new System.Drawing.Size(88, 22);
            this.ucBtnExit.TabIndex = 25;
            this.ucBtnExit.Click += new System.EventHandler(this.ucBtnExit_Click);
            // 
            // ucButtonPrint
            // 
            this.ucButtonPrint.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonPrint.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonPrint.BackgroundImage")));
            this.ucButtonPrint.ButtonType = UserControl.ButtonTypes.None;
            this.ucButtonPrint.Caption = "打印";
            this.ucButtonPrint.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonPrint.Location = new System.Drawing.Point(177, 55);
            this.ucButtonPrint.Name = "ucButtonPrint";
            this.ucButtonPrint.Size = new System.Drawing.Size(88, 22);
            this.ucButtonPrint.TabIndex = 53;
            this.ucButtonPrint.Click += new System.EventHandler(this.ucButtonPrint_Click);
            // 
            // ucLabelComboxPrintTemplete
            // 
            this.ucLabelComboxPrintTemplete.AllowEditOnlyChecked = true;
            this.ucLabelComboxPrintTemplete.Caption = "打印模板";
            this.ucLabelComboxPrintTemplete.Checked = false;
            this.ucLabelComboxPrintTemplete.Location = new System.Drawing.Point(427, 27);
            this.ucLabelComboxPrintTemplete.Name = "ucLabelComboxPrintTemplete";
            this.ucLabelComboxPrintTemplete.SelectedIndex = -1;
            this.ucLabelComboxPrintTemplete.ShowCheckBox = false;
            this.ucLabelComboxPrintTemplete.Size = new System.Drawing.Size(194, 20);
            this.ucLabelComboxPrintTemplete.TabIndex = 49;
            this.ucLabelComboxPrintTemplete.Visible = false;
            this.ucLabelComboxPrintTemplete.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabelComboxPrintTemplete.XAlign = 488;
            // 
            // ucLabelEditPrintCount
            // 
            this.ucLabelEditPrintCount.AllowEditOnlyChecked = true;
            this.ucLabelEditPrintCount.AutoSelectAll = false;
            this.ucLabelEditPrintCount.AutoUpper = true;
            this.ucLabelEditPrintCount.Caption = "打印数量";
            this.ucLabelEditPrintCount.Checked = false;
            this.ucLabelEditPrintCount.EditType = UserControl.EditTypes.Integer;
            this.ucLabelEditPrintCount.Location = new System.Drawing.Point(296, 27);
            this.ucLabelEditPrintCount.MaxLength = 8;
            this.ucLabelEditPrintCount.Multiline = false;
            this.ucLabelEditPrintCount.Name = "ucLabelEditPrintCount";
            this.ucLabelEditPrintCount.PasswordChar = '\0';
            this.ucLabelEditPrintCount.ReadOnly = false;
            this.ucLabelEditPrintCount.ShowCheckBox = false;
            this.ucLabelEditPrintCount.Size = new System.Drawing.Size(111, 24);
            this.ucLabelEditPrintCount.TabIndex = 48;
            this.ucLabelEditPrintCount.TabNext = false;
            this.ucLabelEditPrintCount.Tag = "";
            this.ucLabelEditPrintCount.Value = "1";
            this.ucLabelEditPrintCount.Visible = false;
            this.ucLabelEditPrintCount.WidthType = UserControl.WidthTypes.Tiny;
            this.ucLabelEditPrintCount.XAlign = 357;
            // 
            // ucLabelComboxPrintList
            // 
            this.ucLabelComboxPrintList.AllowEditOnlyChecked = true;
            this.ucLabelComboxPrintList.Caption = "打印机列表";
            this.ucLabelComboxPrintList.Checked = false;
            this.ucLabelComboxPrintList.Location = new System.Drawing.Point(5, 27);
            this.ucLabelComboxPrintList.Name = "ucLabelComboxPrintList";
            this.ucLabelComboxPrintList.SelectedIndex = -1;
            this.ucLabelComboxPrintList.ShowCheckBox = false;
            this.ucLabelComboxPrintList.Size = new System.Drawing.Size(273, 20);
            this.ucLabelComboxPrintList.TabIndex = 50;
            this.ucLabelComboxPrintList.WidthType = UserControl.WidthTypes.Long;
            this.ucLabelComboxPrintList.XAlign = 78;
            // 
            // checkBoxNoPrint
            // 
            this.checkBoxNoPrint.AutoSize = true;
            this.checkBoxNoPrint.Location = new System.Drawing.Point(11, 6);
            this.checkBoxNoPrint.Name = "checkBoxNoPrint";
            this.checkBoxNoPrint.Size = new System.Drawing.Size(60, 16);
            this.checkBoxNoPrint.TabIndex = 57;
            this.checkBoxNoPrint.Text = "未打印";
            this.checkBoxNoPrint.UseVisualStyleBackColor = true;
            this.checkBoxNoPrint.CheckedChanged += new System.EventHandler(this.checkBoxNoPrint_CheckedChanged);
            // 
            // checkBoxSNContentCheck
            // 
            this.checkBoxSNContentCheck.AutoSize = true;
            this.checkBoxSNContentCheck.Location = new System.Drawing.Point(424, 6);
            this.checkBoxSNContentCheck.Name = "checkBoxSNContentCheck";
            this.checkBoxSNContentCheck.Size = new System.Drawing.Size(210, 16);
            this.checkBoxSNContentCheck.TabIndex = 61;
            this.checkBoxSNContentCheck.Text = "限制序列号内容为字符,数字和空格";
            this.checkBoxSNContentCheck.UseVisualStyleBackColor = true;
            this.checkBoxSNContentCheck.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ucLabelEditlast);
            this.groupBox1.Controls.Add(this.ucLabelPrintType);
            this.groupBox1.Controls.Add(this.ultraOptionSetScale);
            this.groupBox1.Controls.Add(this.ucButtonExit);
            this.groupBox1.Controls.Add(this.ucButtonCalcRCardEnd);
            this.groupBox1.Controls.Add(this.txtMoCodeEdit);
            this.groupBox1.Controls.Add(this.radioButton4);
            this.groupBox1.Controls.Add(this.ucLERCEndRcard);
            this.groupBox1.Controls.Add(this.ucLabelComboxCheckType);
            this.groupBox1.Controls.Add(this.ucBtnDelete);
            this.groupBox1.Controls.Add(this.ucLabelRCardLength);
            this.groupBox1.Controls.Add(this.ucLabelCreateQTY);
            this.groupBox1.Controls.Add(this.ucLabelEditRCardPrefix);
            this.groupBox1.Controls.Add(this.ucLERCStartRcard);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 210);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(647, 139);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // ucLabelEditlast
            // 
            this.ucLabelEditlast.AllowEditOnlyChecked = true;
            this.ucLabelEditlast.AutoSelectAll = false;
            this.ucLabelEditlast.AutoUpper = true;
            this.ucLabelEditlast.Caption = "RCardPostfix";
            this.ucLabelEditlast.Checked = false;
            this.ucLabelEditlast.EditType = UserControl.EditTypes.String;
            this.ucLabelEditlast.Location = new System.Drawing.Point(499, 17);
            this.ucLabelEditlast.MaxLength = 4;
            this.ucLabelEditlast.Multiline = false;
            this.ucLabelEditlast.Name = "ucLabelEditlast";
            this.ucLabelEditlast.PasswordChar = '\0';
            this.ucLabelEditlast.ReadOnly = false;
            this.ucLabelEditlast.ShowCheckBox = false;
            this.ucLabelEditlast.Size = new System.Drawing.Size(135, 24);
            this.ucLabelEditlast.TabIndex = 69;
            this.ucLabelEditlast.TabNext = false;
            this.ucLabelEditlast.Value = "";
            this.ucLabelEditlast.WidthType = UserControl.WidthTypes.Tiny;
            this.ucLabelEditlast.XAlign = 584;
            // 
            // ucLabelPrintType
            // 
            this.ucLabelPrintType.AllowEditOnlyChecked = true;
            this.ucLabelPrintType.Caption = "打印类型";
            this.ucLabelPrintType.Checked = false;
            this.ucLabelPrintType.Location = new System.Drawing.Point(205, 17);
            this.ucLabelPrintType.Name = "ucLabelPrintType";
            this.ucLabelPrintType.SelectedIndex = -1;
            this.ucLabelPrintType.ShowCheckBox = false;
            this.ucLabelPrintType.Size = new System.Drawing.Size(261, 24);
            this.ucLabelPrintType.TabIndex = 68;
            this.ucLabelPrintType.WidthType = UserControl.WidthTypes.Long;
            this.ucLabelPrintType.XAlign = 266;
            this.ucLabelPrintType.SelectedIndexChanged += new System.EventHandler(this.ucLabelPrintType_SelectedIndexChanged);
            // 
            // ultraOptionSetScale
            // 
            appearance1.FontData.BoldAsString = "False";
            this.ultraOptionSetScale.Appearance = appearance1;
            this.ultraOptionSetScale.BorderStyle = Infragistics.Win.UIElementBorderStyle.None;
            this.ultraOptionSetScale.CausesValidation = false;
            this.ultraOptionSetScale.CheckedIndex = 0;
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
            this.ultraOptionSetScale.Location = new System.Drawing.Point(386, 78);
            this.ultraOptionSetScale.Name = "ultraOptionSetScale";
            this.ultraOptionSetScale.Size = new System.Drawing.Size(188, 20);
            this.ultraOptionSetScale.TabIndex = 67;
            this.ultraOptionSetScale.Text = "10进制";
            this.ultraOptionSetScale.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            // 
            // ucButtonExit
            // 
            this.ucButtonExit.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonExit.BackgroundImage")));
            this.ucButtonExit.ButtonType = UserControl.ButtonTypes.Cancle;
            this.ucButtonExit.Caption = "取消";
            this.ucButtonExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonExit.Location = new System.Drawing.Point(328, 109);
            this.ucButtonExit.Name = "ucButtonExit";
            this.ucButtonExit.Size = new System.Drawing.Size(88, 22);
            this.ucButtonExit.TabIndex = 66;
            this.ucButtonExit.Click += new System.EventHandler(this.ucButtonExit_Click);
            // 
            // ucButtonCalcRCardEnd
            // 
            this.ucButtonCalcRCardEnd.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonCalcRCardEnd.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonCalcRCardEnd.BackgroundImage")));
            this.ucButtonCalcRCardEnd.ButtonType = UserControl.ButtonTypes.None;
            this.ucButtonCalcRCardEnd.Caption = "生成序列号";
            this.ucButtonCalcRCardEnd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonCalcRCardEnd.Location = new System.Drawing.Point(177, 109);
            this.ucButtonCalcRCardEnd.Name = "ucButtonCalcRCardEnd";
            this.ucButtonCalcRCardEnd.Size = new System.Drawing.Size(88, 22);
            this.ucButtonCalcRCardEnd.TabIndex = 65;
            this.ucButtonCalcRCardEnd.Click += new System.EventHandler(this.ucButtonCalcRCardEnd_Click);
            // 
            // txtMoCodeEdit
            // 
            this.txtMoCodeEdit.AllowEditOnlyChecked = true;
            this.txtMoCodeEdit.AutoSelectAll = false;
            this.txtMoCodeEdit.AutoUpper = true;
            this.txtMoCodeEdit.Caption = "工单代码";
            this.txtMoCodeEdit.Checked = false;
            this.txtMoCodeEdit.EditType = UserControl.EditTypes.String;
            this.txtMoCodeEdit.Location = new System.Drawing.Point(29, 17);
            this.txtMoCodeEdit.MaxLength = 40;
            this.txtMoCodeEdit.Multiline = false;
            this.txtMoCodeEdit.Name = "txtMoCodeEdit";
            this.txtMoCodeEdit.PasswordChar = '\0';
            this.txtMoCodeEdit.ReadOnly = false;
            this.txtMoCodeEdit.ShowCheckBox = false;
            this.txtMoCodeEdit.Size = new System.Drawing.Size(161, 24);
            this.txtMoCodeEdit.TabIndex = 59;
            this.txtMoCodeEdit.TabNext = false;
            this.txtMoCodeEdit.Value = "";
            this.txtMoCodeEdit.WidthType = UserControl.WidthTypes.Small;
            this.txtMoCodeEdit.XAlign = 90;
            this.txtMoCodeEdit.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMoCodeEdit_TxtboxKeyPress);
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Location = new System.Drawing.Point(376, 512);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(59, 16);
            this.radioButton4.TabIndex = 63;
            this.radioButton4.TabStop = true;
            this.radioButton4.Text = "16进制";
            this.radioButton4.UseVisualStyleBackColor = true;
            // 
            // ucLERCEndRcard
            // 
            this.ucLERCEndRcard.AllowEditOnlyChecked = true;
            this.ucLERCEndRcard.AutoSelectAll = false;
            this.ucLERCEndRcard.AutoUpper = true;
            this.ucLERCEndRcard.Caption = "结束序列号";
            this.ucLERCEndRcard.Checked = false;
            this.ucLERCEndRcard.EditType = UserControl.EditTypes.Integer;
            this.ucLERCEndRcard.Location = new System.Drawing.Point(381, 47);
            this.ucLERCEndRcard.MaxLength = 8;
            this.ucLERCEndRcard.Multiline = false;
            this.ucLERCEndRcard.Name = "ucLERCEndRcard";
            this.ucLERCEndRcard.PasswordChar = '\0';
            this.ucLERCEndRcard.ReadOnly = false;
            this.ucLERCEndRcard.ShowCheckBox = false;
            this.ucLERCEndRcard.Size = new System.Drawing.Size(173, 24);
            this.ucLERCEndRcard.TabIndex = 62;
            this.ucLERCEndRcard.TabNext = true;
            this.ucLERCEndRcard.Value = "";
            this.ucLERCEndRcard.WidthType = UserControl.WidthTypes.Small;
            this.ucLERCEndRcard.XAlign = 454;
            // 
            // ucLabelComboxCheckType
            // 
            this.ucLabelComboxCheckType.AllowEditOnlyChecked = true;
            this.ucLabelComboxCheckType.Caption = "检查类型";
            this.ucLabelComboxCheckType.Checked = false;
            this.ucLabelComboxCheckType.Location = new System.Drawing.Point(472, 16);
            this.ucLabelComboxCheckType.Name = "ucLabelComboxCheckType";
            this.ucLabelComboxCheckType.SelectedIndex = -1;
            this.ucLabelComboxCheckType.ShowCheckBox = false;
            this.ucLabelComboxCheckType.Size = new System.Drawing.Size(161, 24);
            this.ucLabelComboxCheckType.TabIndex = 60;
            this.ucLabelComboxCheckType.Visible = false;
            this.ucLabelComboxCheckType.WidthType = UserControl.WidthTypes.Small;
            this.ucLabelComboxCheckType.XAlign = 533;
            this.ucLabelComboxCheckType.SelectedIndexChanged += new System.EventHandler(this.ucLabelComboxCheckType_SelectedIndexChanged);
            // 
            // ucBtnDelete
            // 
            this.ucBtnDelete.BackColor = System.Drawing.SystemColors.Control;
            this.ucBtnDelete.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucBtnDelete.BackgroundImage")));
            this.ucBtnDelete.ButtonType = UserControl.ButtonTypes.Delete;
            this.ucBtnDelete.Caption = "删除";
            this.ucBtnDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnDelete.Location = new System.Drawing.Point(466, 109);
            this.ucBtnDelete.Name = "ucBtnDelete";
            this.ucBtnDelete.Size = new System.Drawing.Size(88, 22);
            this.ucBtnDelete.TabIndex = 22;
            this.ucBtnDelete.Visible = false;
            // 
            // ucLabelRCardLength
            // 
            this.ucLabelRCardLength.AllowEditOnlyChecked = true;
            this.ucLabelRCardLength.AutoSelectAll = false;
            this.ucLabelRCardLength.AutoUpper = true;
            this.ucLabelRCardLength.Caption = "序列号长度";
            this.ucLabelRCardLength.Checked = false;
            this.ucLabelRCardLength.EditType = UserControl.EditTypes.Integer;
            this.ucLabelRCardLength.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ucLabelRCardLength.Location = new System.Drawing.Point(17, 78);
            this.ucLabelRCardLength.MaxLength = 4;
            this.ucLabelRCardLength.Multiline = false;
            this.ucLabelRCardLength.Name = "ucLabelRCardLength";
            this.ucLabelRCardLength.PasswordChar = '\0';
            this.ucLabelRCardLength.ReadOnly = false;
            this.ucLabelRCardLength.ShowCheckBox = false;
            this.ucLabelRCardLength.Size = new System.Drawing.Size(173, 24);
            this.ucLabelRCardLength.TabIndex = 2;
            this.ucLabelRCardLength.TabNext = true;
            this.ucLabelRCardLength.Value = "";
            this.ucLabelRCardLength.WidthType = UserControl.WidthTypes.Small;
            this.ucLabelRCardLength.XAlign = 90;
            // 
            // ucLabelCreateQTY
            // 
            this.ucLabelCreateQTY.AllowEditOnlyChecked = true;
            this.ucLabelCreateQTY.AutoSelectAll = false;
            this.ucLabelCreateQTY.AutoUpper = true;
            this.ucLabelCreateQTY.Caption = "生成数量";
            this.ucLabelCreateQTY.Checked = false;
            this.ucLabelCreateQTY.EditType = UserControl.EditTypes.Integer;
            this.ucLabelCreateQTY.Location = new System.Drawing.Point(205, 78);
            this.ucLabelCreateQTY.MaxLength = 8;
            this.ucLabelCreateQTY.Multiline = false;
            this.ucLabelCreateQTY.Name = "ucLabelCreateQTY";
            this.ucLabelCreateQTY.PasswordChar = '\0';
            this.ucLabelCreateQTY.ReadOnly = false;
            this.ucLabelCreateQTY.ShowCheckBox = false;
            this.ucLabelCreateQTY.Size = new System.Drawing.Size(161, 24);
            this.ucLabelCreateQTY.TabIndex = 8;
            this.ucLabelCreateQTY.TabNext = true;
            this.ucLabelCreateQTY.Value = "";
            this.ucLabelCreateQTY.WidthType = UserControl.WidthTypes.Small;
            this.ucLabelCreateQTY.XAlign = 266;
            // 
            // ucLabelEditRCardPrefix
            // 
            this.ucLabelEditRCardPrefix.AllowEditOnlyChecked = true;
            this.ucLabelEditRCardPrefix.AutoSelectAll = false;
            this.ucLabelEditRCardPrefix.AutoUpper = true;
            this.ucLabelEditRCardPrefix.Caption = "序列号前缀";
            this.ucLabelEditRCardPrefix.Checked = false;
            this.ucLabelEditRCardPrefix.EditType = UserControl.EditTypes.String;
            this.ucLabelEditRCardPrefix.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ucLabelEditRCardPrefix.Location = new System.Drawing.Point(17, 47);
            this.ucLabelEditRCardPrefix.MaxLength = 40;
            this.ucLabelEditRCardPrefix.Multiline = false;
            this.ucLabelEditRCardPrefix.Name = "ucLabelEditRCardPrefix";
            this.ucLabelEditRCardPrefix.PasswordChar = '\0';
            this.ucLabelEditRCardPrefix.ReadOnly = false;
            this.ucLabelEditRCardPrefix.ShowCheckBox = false;
            this.ucLabelEditRCardPrefix.Size = new System.Drawing.Size(173, 24);
            this.ucLabelEditRCardPrefix.TabIndex = 2;
            this.ucLabelEditRCardPrefix.TabNext = true;
            this.ucLabelEditRCardPrefix.Value = "";
            this.ucLabelEditRCardPrefix.WidthType = UserControl.WidthTypes.Small;
            this.ucLabelEditRCardPrefix.XAlign = 90;
            // 
            // ucLERCStartRcard
            // 
            this.ucLERCStartRcard.AllowEditOnlyChecked = true;
            this.ucLERCStartRcard.AutoSelectAll = false;
            this.ucLERCStartRcard.AutoUpper = true;
            this.ucLERCStartRcard.Caption = "起始序列号";
            this.ucLERCStartRcard.Checked = false;
            this.ucLERCStartRcard.EditType = UserControl.EditTypes.Integer;
            this.ucLERCStartRcard.Location = new System.Drawing.Point(193, 47);
            this.ucLERCStartRcard.MaxLength = 8;
            this.ucLERCStartRcard.Multiline = false;
            this.ucLERCStartRcard.Name = "ucLERCStartRcard";
            this.ucLERCStartRcard.PasswordChar = '\0';
            this.ucLERCStartRcard.ReadOnly = false;
            this.ucLERCStartRcard.ShowCheckBox = false;
            this.ucLERCStartRcard.Size = new System.Drawing.Size(173, 24);
            this.ucLERCStartRcard.TabIndex = 8;
            this.ucLERCStartRcard.TabNext = true;
            this.ucLERCStartRcard.Value = "";
            this.ucLERCStartRcard.WidthType = UserControl.WidthTypes.Small;
            this.ucLERCStartRcard.XAlign = 266;
            // 
            // processBar
            // 
            this.processBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            appearance14.ForeColor = System.Drawing.Color.Red;
            this.processBar.FillAppearance = appearance14;
            this.processBar.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.processBar.Location = new System.Drawing.Point(232, 101);
            this.processBar.Name = "processBar";
            this.processBar.Size = new System.Drawing.Size(163, 22);
            this.processBar.TabIndex = 62;
            this.processBar.Text = "[Formatted]";
            this.processBar.TextRenderingMode = Infragistics.Win.TextRenderingMode.GDIPlus;
            this.processBar.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            this.processBar.UseOsThemes = Infragistics.Win.DefaultableBoolean.True;
            this.processBar.UseWaitCursor = true;
            // 
            // ultraGridMain
            // 
            appearance4.BackColor = System.Drawing.SystemColors.Window;
            appearance4.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.ultraGridMain.DisplayLayout.Appearance = appearance4;
            this.ultraGridMain.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.ultraGridMain.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance13.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance13.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance13.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance13.BorderColor = System.Drawing.SystemColors.Window;
            this.ultraGridMain.DisplayLayout.GroupByBox.Appearance = appearance13;
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
            this.ultraGridMain.Location = new System.Drawing.Point(0, 39);
            this.ultraGridMain.Name = "ultraGridMain";
            this.ultraGridMain.Size = new System.Drawing.Size(647, 171);
            this.ultraGridMain.TabIndex = 5;
            this.ultraGridMain.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.ultraGridMain_InitializeLayout);
            this.ultraGridMain.CellChange += new Infragistics.Win.UltraWinGrid.CellEventHandler(this.ultraGridMain_CellChange);
            // 
            // FSATOPrintDemo
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(647, 429);
            this.Controls.Add(this.processBar);
            this.Controls.Add(this.ultraGridMain);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panelButton);
            this.Controls.Add(this.grpQuery);
            this.Name = "FSATOPrintDemo";
            this.Text = "工单关联产品序号(TDK)";
            this.Load += new System.EventHandler(this.FSATOPrintDemo_Load);
            this.grpQuery.ResumeLayout(false);
            this.panelButton.ResumeLayout(false);
            this.panelButton.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraOptionSetScale)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridMain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        #region Form Base
        private WarehouseFacade warehouseFacade = null;
        private ItemFacade _ItemFacade = null;
        private InventoryFacade _InventoryFacade = null;
        private MaterialFacade _MaterialFacade = null;
        private MOFacade _MOFacade = null;
        private DataCollect.DataCollectFacade _DataCollectFacade = null;
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

        public FSATOPrintDemo()
        {
            // 该调用是 Windows 窗体设计器所必需的。
            InitializeComponent();
            this.processBar.Visible = false;
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
            this._MOFacade = new MOFacade(this.DataProvider);
            this._InventoryFacade = new InventoryFacade(this.DataProvider);
            this.warehouseFacade = new WarehouseFacade(this.DataProvider);
            this._DataCollectFacade = new DataCollect.DataCollectFacade(this.DataProvider);
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
        private void FSATOPrintDemo_Load(object sender, System.EventArgs e)
        {


            this.ucLabelEditPrintCount.InnerTextBox.ForeColor = Color.Black;
            this.ucLabelEditPrintCount.InnerTextBox.TextAlign = HorizontalAlignment.Right;
            InitializeMainGrid();
            this.ultraGridMain.DisplayLayout.Bands[0].Columns["Checked"].Header.CheckBoxVisibility = Infragistics.Win.UltraWinGrid.HeaderCheckBoxVisibility.WhenUsingCheckEditor;
            LoadPrinter();
            ucLabelPrintType_Load(); //打印类型
            this.txtMoCodeQuery.TextFocus(false, true);
            //LoadTemplateList();
            this.InitGridLanguage(ultraGridMain);
            this.InitPageLanguage();

        }
        #endregion

        #region Button Events

        //查询
        private void ucButtonQuery_Click(object sender, EventArgs e)
        {
            //RCARD需要使用GetSourceCard方法转换成初始序列号
            string rcard = string.Empty;
            if (txtMoCodeQuery.Value.Trim().Length == 0)
            {
                this.ShowMessage(new UserControl.Message(UserControl.MessageType.Error, "$MO_Cannot_Null"));
                this.txtMoCodeQuery.TextFocus(false, true);
                return;
            }

            if (this.ucLabelRCardQuery.Value.Trim() != "")
            {
                rcard = _DataCollectFacade.GetSourceCard(this.ucLabelRCardQuery.Value.Trim().ToUpper(), string.Empty);
            }
            LoadCheckList(rcard, this.txtMoCodeQuery.Value.Trim());
        }

        //删除
        //调整逻辑： 去掉删除功能 

        #region 产生序列号得删除功能
        /*
        private void ucBtnDelete_Click(object sender, System.EventArgs e)
        {

            if (!CheckISSelectRow())
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_NO_ROW_SELECTED"));
                return;
            }

            for (int i = 0; i < this.ultraGridMain.Rows.Count; i++)
            {
                Infragistics.Win.UltraWinGrid.UltraGridRow row = ultraGridMain.Rows[i];
                //根据工单代码+产品序列号到表TBLONWIP获取数据
                if (bool.Parse(row.Cells["Checked"].Value.ToString()))
                {
                    int count = _DataCollectFacade.GetRCardInfoCount(row.Cells["RCard"].Value.ToString(), row.Cells["MoCode"].Value.ToString(), "", "");
                    if (count > 0)
                    {
                        ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_RCARD_EXIST_ONWIP$RCARD:" + row.Cells["RCard"].Value.ToString()));
                        return;
                    }
                }
            }
            try
            {
                this.DataProvider.BeginTransaction();
                for (int i = 0; i < this.ultraGridMain.Rows.Count; i++)
                {
                    Infragistics.Win.UltraWinGrid.UltraGridRow row = ultraGridMain.Rows[i];
                    //不存在，则从TBLMO2RCARDLink表中删除
                    if (bool.Parse(row.Cells["Checked"].Value.ToString()))
                    {
                        object obj = _MOFacade.GetMO2RCardLink(row.Cells["RCard"].Value.ToString());
                        if (obj != null)
                        {
                            _MOFacade.DeleteMO2RCardLink(obj as MO2RCARDLINK);
                        }
                    }
                }
                this.DataProvider.CommitTransaction();


                //RCARD需要使用GetSourceCard方法转换成初始序列号
                string rcard = string.Empty;
                if (this.ucLabelRCardQuery.Value.Trim() != "")
                {
                    rcard = _DataCollectFacade.GetSourceCard(this.ucLabelRCardQuery.Value.Trim().ToUpper(), string.Empty);
                }

                LoadCheckList(rcard, this.txtMoCodeQuery.Value.Trim());

            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
            }

        }
         * */
        #endregion

        //计算生成序列号  
        private void ucButtonCalcRCardEnd_Click(object sender, EventArgs e)
        {
            if (_MOFacade == null)
            {
                _MOFacade = new MOFacade(this.DataProvider);
            }

            //检查后缀
            if ((this.ucLabelEditlast.Value.Trim() == "")||(this.ucLabelEditlast.Value.Length!=4))
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_LASTRCARD_LENGTH_IS_ERROR"));
                this.ucLabelEditlast.TextFocus(false, true);
                return;
            }



            //检查是否输入工单，工单是否存在TBLMO中。
            if (this.txtMoCodeEdit.Value.Trim() == "")
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_STARTRCARD_LESS_THAN_ZEOR"));
                return;
            }
            else
            {
                //去掉检查类型的判断 
                //if (this.ucLabelComboxCheckType.SelectedIndex == -1)
                //{
                //    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_ERROR_ITEM_CHECK_TYPE"));
                //    return;
                //}

                string currentMo = this.txtMoCodeEdit.Value.Trim();
                object objMo = this._MOFacade.GetMO(currentMo);
                long createCount = 0;

                object[] mo2rcard = _MOFacade.GetMO2RCardLinkByMoCode(currentMo);
                if (mo2rcard != null)
                {
                    Clear();
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$MO_Link_Rcard_AlreadyExists"));
                    this.txtMoCodeEdit.TextFocus(false, true);
                    return;
                }

                if (objMo != null)
                {
                    //检查是否输入”首字符串”、”起始字符串”、”结束字符串”和”生成数量”两个只要有一个输入即可，两者都输入，则以生成数量为准。
                    if (this.ucLabelEditRCardPrefix.Value.Trim() == "")
                    {
                        ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_RCardPrefix$Error_Input_Empty"));
                        return;
                    }
                    if (this.ucLERCStartRcard.Value.Trim() == "")
                    {
                        ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_RunningCardStart$Error_Input_Empty"));
                        return;
                    }
                    if (this.ucLERCEndRcard.Value.Trim() == "" && this.ucLabelCreateQTY.Value.Trim() == "")
                    {
                        ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_RunningCardEnd$CS_RunningCardCount$Error_Input_Same_Empty"));
                        return;
                    }

                    //如果勾选序列号长度，则序列号长度输入框必须有>0的值
                    //调整逻辑：去掉序列号长度的相关检查。序列号长度按照序列号产生规则自动计算出来，显示 Modify by 20120912

                    #region 序列号长度检查
                    /*
                    if (this.ucLabelRCardLength.Checked)
                    {
                        if (this.ucLabelRCardLength.Value.Trim() == "")
                        {
                            ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_RunningCardLength$Error_Input_Empty"));
                            return;
                        }
                        int value = int.Parse(this.ucLabelRCardLength.Value.Trim());
                        if (value <= 0)
                        {
                            ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_RunningCardLength_LESS_THAN_ZEOR"));
                            return;
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
                            startSN = long.Parse(NumberScaleHelper.ChangeNumber(ucLERCStartRcard.Value.Trim(), scale, NumberScale.Scale10));
                        }
                        catch (Exception ex)
                        {
                            this.ShowMessage(ex);

                        }
                

                        int strLength = 0;
                        int numLenght = 0;

                        if (ultraOptionSetScale.CheckedIndex == 0)
                        {

                            if (this.ucLabelCreateQTY.Value.Trim() != "")
                            {
                                int length = (int.Parse(this.ucLERCStartRcard.Value.Trim()) + int.Parse(this.ucLabelCreateQTY.Value.Trim()) - 1).ToString().Length;
                                numLenght = this.ucLabelEditRCardPrefix.Value.Trim().Length + length;
                                strLength = this.ucLabelEditRCardPrefix.Value.Trim().Length + this.ucLERCStartRcard.Value.Trim().Length;


                                if (value != strLength || numLenght > value)
                                {
                                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_RunningCardLength_ERROR"));
                                    return;
                                }

                            }
                            else
                            {

                                if (int.Parse(ucLERCStartRcard.Value.Trim()) > int.Parse(ucLERCEndRcard.Value.Trim()))
                                {
                                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_Error_Start_Bigger_Than_End"));
                                    return;
                                }
                                if (this.ucLERCStartRcard.Value.Trim().Length != this.ucLERCEndRcard.Value.Trim().Length)
                                {
                                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_Error_Start_End_Lenght"));
                                    return;
                                }
                                int length = this.ucLERCEndRcard.Value.Trim().Length;
                                strLength = this.ucLabelEditRCardPrefix.Value.Trim().Length + length;

                                if (value != strLength)
                                {
                                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_RunningCardLength_ERROR"));
                                    return;
                                }
                            }
                        }
                        else if (ultraOptionSetScale.CheckedIndex == 1 || ultraOptionSetScale.CheckedIndex == 2)
                        {
                            if (this.ucLabelCreateQTY.Value.Trim() != "")
                            {
                                int length = NumberScaleHelper.ChangeNumber((startSN + int.Parse(this.ucLabelCreateQTY.Value.Trim()) - 1).ToString(), NumberScale.Scale10, scale).Length;

                                numLenght = this.ucLabelEditRCardPrefix.Value.Trim().Length + length;
                                strLength = this.ucLabelEditRCardPrefix.Value.Trim().Length + this.ucLERCStartRcard.Value.Trim().Length;


                                if (value != strLength || numLenght > value)
                                {
                                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_RunningCardLength_ERROR"));
                                    return;
                                }
                            }
                            else
                            {

                                if (startSN > long.Parse(NumberScaleHelper.ChangeNumber(this.ucLERCEndRcard.Value.Trim(), scale, NumberScale.Scale10)))
                                {
                                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_Error_Start_Bigger_Than_End"));
                                    return;
                                }
                                if (this.ucLERCStartRcard.Value.Trim().Length != this.ucLERCEndRcard.Value.Trim().Length)
                                {
                                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_Error_Start_End_Lenght"));
                                    return;
                                }
                                int length = this.ucLERCEndRcard.Value.Trim().Length;
                                strLength = this.ucLabelEditRCardPrefix.Value.Trim().Length + length;

                                if (value != strLength)
                                {
                                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_RunningCardLength_ERROR"));
                                    return;
                                }
                            }
                            
    
                        }

                    }
                     * */
                    #endregion

                    //如果有勾”选限制序列号内容为字符,数字和空格”和”序列号长度”，需要检查生产的序列号是否符合规则
                    //调整逻辑：去掉序列号的非法字符 
                    #region 检查序列号的非法字符
                    //if (this.checkBoxSNContentCheck.Checked)
                    //{
                    //    string pattern = @"^([A-Za-z0-9]+[ ]*)*[A-Za-z0-9]+$";
                    //    string rcard = this.ucLabelEditRCardPrefix.Value.Trim();
                    //    Regex rex = new Regex(pattern, RegexOptions.IgnoreCase);
                    //    Match match = rex.Match(rcard);

                    //    if (!match.Success)
                    //    {
                    //        ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_SNContent_CheckWrong $CS_Param_RunSeq:" + this.ucLabelEditRCardPrefix.Value.Trim().ToString()));
                    //        return;
                    //    }

                    //}
                    #endregion


                    //如果生成的数量大于等于5000则提醒用户”将会生成的序列号个数超过，很有可能影响系统效率，是否继续？”，让用户选择是否继续操作。
                    if (this.ucLabelCreateQTY.Value.Trim() != "")
                    {
                        if (int.Parse(this.ucLabelCreateQTY.Value.Trim()) > 5000)
                        {
                            DialogResult dr = MessageBox.Show(MutiLanguages.ParserMessage("$Generate_number_Is_Greate"), MutiLanguages.ParserMessage("$ShowMessage"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                            if (dr == DialogResult.No)
                            {
                                return;
                            }
                        }

                        createCount = long.Parse(this.ucLabelCreateQTY.Value.Trim());
                    }
                    else
                    {
                        long endRcardCount = long.Parse(this.ucLERCEndRcard.Value.Trim());
                        long startRcardCount = long.Parse(this.ucLERCStartRcard.Value.Trim());
                        if ((endRcardCount - startRcardCount) > 5000)
                        {
                            DialogResult dr = MessageBox.Show(MutiLanguages.ParserMessage("$Generate_number_Is_Greate"), MutiLanguages.ParserMessage("$ShowMessage"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                            if (dr == DialogResult.No)
                            {
                                return;
                            }
                        }

                        createCount = endRcardCount - startRcardCount + 1;
                    }

                    //10进制、16进制、34进制生成序列号
                    List<string> RCardList = CalcRCardEnd(createCount);

                    if (RCardList == null)
                    {
                        this.ShowMessage(new UserControl.Message(UserControl.MessageType.Error, "$Error_Has_IllegalRcard"));
                        this.txtMoCodeEdit.TextFocus(false, true);
                        return;
                    }


                    //根据生成的起始序列号和结束序列号检查是否在该工单已关联的序列号中，存在则报错
                    foreach (string rcard in RCardList)
                    {
                        object obj = _MOFacade.GetMO2RCardLink(rcard);
                        if (obj != null)
                        {
                            ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_RCARD_EXIST_MO2RCARDLINK$RCARD:" + rcard));
                            return;
                        }
                    }
                    DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);

                    this.DataProvider.BeginTransaction();
                    try
                    {
                        foreach (string rcard in RCardList)
                        {
                            MO2RCARDLINK rcarLink = new MO2RCARDLINK();
                            rcarLink.MOCode = this.txtMoCodeEdit.Value.Trim();
                            rcarLink.PrintTimes = 0;
                            rcarLink.LastPrintDate = 0;
                            rcarLink.LastPrintTime = 0;
                            rcarLink.LastPrintUSER = "";
                            rcarLink.RCard = rcard;
                            rcarLink.MDate = dbDateTime.DBDate;
                            rcarLink.MTime = dbDateTime.DBTime;
                            rcarLink.MUser = ApplicationService.Current().UserCode;
                            _MOFacade.AddMO2RCardLink(rcarLink);
                        }

                        this.DataProvider.CommitTransaction();
                        ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Success, "$CS_SUCCESS_CREATE_RCARD"));

                        if (RCardList.Count > 0)//add by Jarvis
                        {
                            LoadCheckListNewCreate(this.txtMoCodeEdit.Value.Trim(), RCardList[0], RCardList[RCardList.Count - 1]);
                        }

                        return;


                    }
                    catch (Exception ex)
                    {
                        this.DataProvider.RollbackTransaction();
                        ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_MO_Not_Exist"));
                        this.txtMoCodeEdit.TextFocus(false, true);
                        return;
                    }
                }
                else
                {
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_MO_Not_Exist"));
                    this.txtMoCodeEdit.TextFocus(false, true);
                    return;
                }
            }
        }

        //打印  Modify For TDK
        private void ucButtonPrint_Click(object sender, EventArgs e)
        {
            //Add 打印机为TravellingTag和Lot SerialNumber时且打印次数大于等于1 需要输入密码 by Leo20120913
            #region 打印是否需要密码
            IsNeedPassword = false;
            if (_systemFacade == null)
            {
                _systemFacade = new SystemSettingFacade(this.DataProvider);
            }
            //先检查打印模板是否存在
            if (this.ucLabelPrintType.SelectedItemValue == null || this.ucLabelPrintType.SelectedItemValue.ToString() == string.Empty)
            {
                this.ShowMessage(new UserControl.Message(UserControl.MessageType.Error, "$Error_PrintType_Empty"));
                return;
            }
            this.ucButtonPrint.Focus();

            Parameter paramterTravelling = (Parameter)_systemFacade.GetParameter("TRAVELLING TAG(600DPI)", "PRINTRELATE");
            //Parameter paramterLotSerial = (Parameter)_systemFacade.GetParameter("LOT SERIAL NUMBER(600DPI)", "PRINTRELATE");

            // Modify by 刘晓鹤（crane.liu）-DS22 2013-01-12 for 修改为所有类型的重打都需要输入密码   
            //if (this.ucLabelPrintType.SelectedItemValue.ToString().Equals(paramterTravelling.ParameterCode) ||
            //   this.ucLabelPrintType.SelectedItemValue.ToString().Equals(paramterLotSerial.ParameterCode))
            // End modify by 刘晓鹤（crane.liu）-DS22 2013-01-12
            {
                for (int i = 0; i < ultraGridMain.Rows.Count; i++)
                {
                    Infragistics.Win.UltraWinGrid.UltraGridRow row = ultraGridMain.Rows[i];

                    if (bool.Parse(row.Cells["Checked"].Value.ToString()))
                    {
                        object obj = _MOFacade.GetMO2RCardLink(row.Cells["RCard"].Value.ToString());
                        if (((MO2RCARDLINK)obj).PrintTimes >= 1)
                        {
                            IsNeedPassword = true;
                            break;
                        }
                    }
                }
            }


            //如果需要输入密码，弹出密码输入框
            if (IsNeedPassword == true)
            {
                string strMsg = string.Empty;
                string password = string.Empty;
                strMsg = UserControl.MutiLanguages.ParserString("$Please_Input_Password");
                FDialogInput finput = new FDialogInput(strMsg);
                strMsg = UserControl.MutiLanguages.ParserString("$Please_Input_Password");
                finput.Text = strMsg;
                finput.InputPasswordChar = '*';
                // DialogResult dialogResult = finput.ShowDialog();

                //密码弹出窗口
                if (PasswordPopupWindow(finput) == false)
                {
                    return;
                }

            }
            #endregion

            //判断打印类型是否为Serial Number1和Serial Number2
            //这两种不需要txt模板，直接打印

            if (_systemFacade == null)
            {
                _systemFacade = new SystemSettingFacade(this.DataProvider);
            }
            if (_MOFacade == null)
            {
                _MOFacade = new MOFacade(this.DataProvider);
            }
            if (_ItemFacade == null)
            {
                _ItemFacade = new ItemFacade(this.DataProvider);
            }


            Parameter SerialNumber1 = (Parameter)_systemFacade.GetParameter("SERIAL NUMBER1", "PRINTRELATE");
            Parameter SerialNumber2 = (Parameter)_systemFacade.GetParameter("SERIAL NUMBER2", "PRINTRELATE");
            Parameter LotSerialNumber2D = (Parameter)_systemFacade.GetParameter("2D LOT SERIAL NUMBER(600DPI)", "PRINTRELATE");
            Parameter TravellingTag = (Parameter)_systemFacade.GetParameter("TRAVELLING TAG(600DPI)", "PRINTRELATE");
            //更换模板 使用LOT SERIAL NUMBER(600DPI)-1 打印两组相同的条码，每次打两张，左右两张相同
            //Parameter LotSerialNumber = (Parameter)_systemFacade.GetParameter("LOT SERIAL NUMBER(600DPI)", "PRINTRELATE");
            Parameter LotSerialNumber = (Parameter)_systemFacade.GetParameter("LOT SERIAL NUMBER(600DPI)-1", "PRINTRELATE");
            //打印一组条码，每次打两张，左右两张不同
            Parameter LotSerialNumber600_2 = (Parameter)_systemFacade.GetParameter("LOT SERIAL NUMBER(600DPI)-2", "PRINTRELATE");
            //Parameter OffLotSerialNumber = (Parameter)_systemFacade.GetParameter("OFF LOT SERIAL NUMBER", "PRINTRELATE");
            Parameter LotSerial2D300DPI = (Parameter)_systemFacade.GetParameter("2D LOT SERIAL NUMBER(300DPI)", "PRINTRELATE");
            Parameter LotSerial300DPI = (Parameter)_systemFacade.GetParameter("LOT SERIAL NUMBER(300DPI)", "PRINTRELATE");

            // Add by 刘晓鹤（crane.liu）-DS22 2013-01-14 for 从包装页面挪过来的       
            Parameter print300dpi = ((Domain.BaseSetting.Parameter)_systemFacade.GetParameter("MINI TRAVELLING TAG (300 DPI)", "PRINTRELATE"));
            Parameter print200dpi = ((Domain.BaseSetting.Parameter)_systemFacade.GetParameter("MINI TRAVELLING TAG (200 DPI)", "PRINTRELATE"));
            Parameter printLotNO200dpi = ((Domain.BaseSetting.Parameter)_systemFacade.GetParameter("LOT SERIAL NUMBER (200 DPI)", "PRINTRELATE"));
            // End add by 刘晓鹤（crane.liu）-DS22 2013-01-14
            Parameter printSMTOQC = ((Domain.BaseSetting.Parameter)_systemFacade.GetParameter("SMT QC LABEL(50MM)", "PRINTRELATE"));

            string printerName = this.ucLabelComboxPrintList.SelectedItemValue.ToString();

            int NewlineCount = 0; //换行计数
            string rcardsLineOne = string.Empty; //第一行数据收集
            string rcardsLineTwo = string.Empty;  //第二行数据收集

            //打印类型为printLotNO200dpi new added 20130114 from carton pack by crane
            #region printLotNO200dpi
            if (printLotNO200dpi != null && this.ucLabelPrintType.SelectedItemValue.ToString().Equals(printLotNO200dpi.ParameterCode))
            {
                bool IsShowProcessBar = false;
                ArrayList printValue = new ArrayList(); //存放最终打印值
                string fileName = Application.StartupPath + "\\PrintLabel\\" + printLotNO200dpi.ParameterAlias + ".txt";
                int count = 0; //判断是否有Rcard要打印
                //判断模板文件是否能存在
                try
                {
                    FileStream textReader = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                }
                catch (Exception)
                {
                    this.ShowMessage(new UserControl.Message(UserControl.MessageType.Error, "$TemplateFile_doesnot_exist"));
                    return;
                }

                StringDictionary valueList = new StringDictionary();  //解析文件键值对
                valueList = SATOPrint.SATOPrintHelper.InitPrintDictionary();

                for (int i = 0; i < ultraGridMain.Rows.Count; i++) //选中的Rcard 赋值
                {
                    count++;
                    Infragistics.Win.UltraWinGrid.UltraGridRow row = ultraGridMain.Rows[i];
                    if (bool.Parse(row.Cells["Checked"].Value.ToString()))
                    {
                        count--;
                        IsShowProcessBar = true;
                        printRcard = row.Cells["RCard"].Value.ToString().Trim();
                        string itemDesc = row.Cells["ITEMDESC"].Value.ToString().Trim();
                        string itemCode = row.Cells["ITEM"].Value.ToString().Trim();
                        //新增逻辑 特殊ItemDesc转化
                        itemDesc = FormatItemDesc(itemDesc, itemCode);

                        valueList["itemdesc"] = itemDesc;

                        string rcardPrint = string.Empty;
                        if (printRcard.Length > 14)
                        {
                            rcardPrint = printRcard.Substring(0, 13) + FormatHelper.GetFormatRcards(printRcard.Substring(0, 13));
                        }
                        else
                        {
                            rcardPrint = printRcard;
                        }
                        valueList["rcard"] = rcardPrint;
                        valueList["rcard0to3"] = printRcard.Substring(0, 3); //rcard前3位
                        valueList["rcard4to10"] = printRcard.Substring(3, 6); //rcard 从第4开始取6位
                        valueList["rcard10to14"] = printRcard.Substring(9, 4); //rcard 从第10开始取4为
                        valueList["rcardoverage"] = printRcard.Substring(13, printRcard.Length - 3 - 6 - 4); //前面截取后的剩余
                        try
                        {
                            printValue = SATOPrint.SATOPrintHelper.ParsePrintFile(fileName, valueList);
                            SATOPrint.SATOPrinter.Print(printerName, printValue);
                        }
                        catch (Exception)
                        {
                            this.Enabled = true;
                            this.processBar.Visible = false;
                            throw;
                        }
                    }

                    if (IsShowProcessBar)
                    {
                        this.Enabled = false; //冻结页面数据
                        this.processBar.Visible = true;
                        this.processBar.Maximum = ultraGridMain.Rows.Count;
                        this.processBar.Value = i;
                        Application.DoEvents();
                    }
                }
                if (count == this.ultraGridMain.Rows.Count && count != 0)
                {
                    this.ShowMessage(new UserControl.Message(UserControl.MessageType.Error, "$CS_Please_Take_OneDate"));
                    this.Enabled = true;
                    this.processBar.Visible = false;
                    return;
                }

            }
            #endregion

            //打印类型为print200dpi new added 20130114 from carton pack by crane
            #region print200dpi
            if (print200dpi != null && this.ucLabelPrintType.SelectedItemValue.ToString().Equals(print200dpi.ParameterCode))
            {
                bool IsShowProcessBar = false;
                ArrayList printValue = new ArrayList(); //存放最终打印值
                string fileName = Application.StartupPath + "\\PrintLabel\\" + print200dpi.ParameterAlias + ".txt";
                int count = 0; //判断是否有Rcard要打印
                //判断模板文件是否能存在
                try
                {
                    FileStream textReader = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                }
                catch (Exception)
                {
                    this.ShowMessage(new UserControl.Message(UserControl.MessageType.Error, "$TemplateFile_doesnot_exist"));
                    return;
                }

                StringDictionary valueList = new StringDictionary();  //解析文件键值对
                valueList = SATOPrint.SATOPrintHelper.InitPrintDictionary();

                for (int i = 0; i < ultraGridMain.Rows.Count; i++) //选中的Rcard 赋值
                {
                    count++;
                    Infragistics.Win.UltraWinGrid.UltraGridRow row = ultraGridMain.Rows[i];
                    if (bool.Parse(row.Cells["Checked"].Value.ToString()))
                    {
                        count--;
                        IsShowProcessBar = true;

                        valueList["mocode"] = row.Cells["MoCode"].Value.ToString().Trim();
                        valueList["itemdesc"] = row.Cells["ITEMDESC"].Value.ToString().Trim();
                        valueList["rcard"] = row.Cells["RCard"].Value.ToString().Trim();
                        valueList["moplanqty"] = ((int)float.Parse(row.Cells["MOPLANQTY"].Value.ToString().Trim())).ToString().Trim();
                        valueList["lotno"] = row.Cells["LOTNO"].Value.ToString().Trim();


                        try
                        {
                            printValue = SATOPrint.SATOPrintHelper.ParsePrintFile(fileName, valueList);
                            SATOPrint.SATOPrinter.Print(printerName, printValue);
                        }
                        catch (Exception)
                        {
                            this.Enabled = true;
                            this.processBar.Visible = false;
                            throw;
                        }
                    }

                    if (IsShowProcessBar)
                    {
                        this.Enabled = false; //冻结页面数据
                        this.processBar.Visible = true;
                        this.processBar.Maximum = ultraGridMain.Rows.Count;
                        this.processBar.Value = i;
                        Application.DoEvents();
                    }
                }
                if (count == this.ultraGridMain.Rows.Count && count != 0)
                {
                    this.ShowMessage(new UserControl.Message(UserControl.MessageType.Error, "$CS_Please_Take_OneDate"));
                    this.Enabled = true;
                    this.processBar.Visible = false;
                    return;
                }

            }
            #endregion

            //打印类型为print300dpi new added 20130114 from carton pack by crane
            #region print300dpi
            if (print300dpi != null && this.ucLabelPrintType.SelectedItemValue.ToString().Equals(print300dpi.ParameterCode))
            {
                bool IsShowProcessBar = false;
                ArrayList printValue = new ArrayList(); //存放最终打印值
                string fileName = Application.StartupPath + "\\PrintLabel\\" + print300dpi.ParameterAlias + ".txt";
                int count = 0; //判断是否有Rcard要打印
                //判断模板文件是否能存在
                try
                {
                    FileStream textReader = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                }
                catch (Exception)
                {
                    this.ShowMessage(new UserControl.Message(UserControl.MessageType.Error, "$TemplateFile_doesnot_exist"));
                    return;
                }

                StringDictionary valueList = new StringDictionary();  //解析文件键值对
                valueList = SATOPrint.SATOPrintHelper.InitPrintDictionary();

                for (int i = 0; i < ultraGridMain.Rows.Count; i++) //选中的Rcard 赋值
                {
                    count++;
                    Infragistics.Win.UltraWinGrid.UltraGridRow row = ultraGridMain.Rows[i];
                    if (bool.Parse(row.Cells["Checked"].Value.ToString()))
                    {
                        count--;
                        IsShowProcessBar = true;
                        valueList["mocode"] = row.Cells["MoCode"].Value.ToString().Trim();
                        valueList["itemdesc"] = row.Cells["ITEMDESC"].Value.ToString().Trim();
                        valueList["rcard"] = row.Cells["RCard"].Value.ToString().Trim();
                        valueList["moplanqty"] = ((int)float.Parse(row.Cells["MOPLANQTY"].Value.ToString().Trim())).ToString().Trim();
                        valueList["lotno"] = row.Cells["LOTNO"].Value.ToString().Trim();
                        try
                        {
                            printValue = SATOPrint.SATOPrintHelper.ParsePrintFile(fileName, valueList);
                            SATOPrint.SATOPrinter.Print(printerName, printValue);
                        }
                        catch (Exception)
                        {
                            this.Enabled = true;
                            this.processBar.Visible = false;
                            throw;
                        }
                    }

                    if (IsShowProcessBar)
                    {
                        this.Enabled = false; //冻结页面数据
                        this.processBar.Visible = true;
                        this.processBar.Maximum = ultraGridMain.Rows.Count;
                        this.processBar.Value = i;
                        Application.DoEvents();
                    }
                }
                if (count == this.ultraGridMain.Rows.Count && count != 0)
                {
                    this.ShowMessage(new UserControl.Message(UserControl.MessageType.Error, "$CS_Please_Take_OneDate"));
                    this.Enabled = true;
                    this.processBar.Visible = false;
                    return;
                }

            }
            #endregion

            //打印类型为：SerialNumber1 和SerialNumber2
            #region  SerialNumber1AndSerialNumber2
            if ((SerialNumber1 != null && this.ucLabelPrintType.SelectedItemValue.ToString().Equals(SerialNumber1.ParameterCode)) ||
                (SerialNumber2 != null && this.ucLabelPrintType.SelectedItemValue.ToString().Equals(SerialNumber2.ParameterCode)))
            {
                System.Collections.ArrayList printValueList = new System.Collections.ArrayList(); //打印数据的第一行
                ArrayList Rcards = new ArrayList(); //存放需要打印的rcard
                ArrayList printValueListLineTwo = new ArrayList(); //打印数据的第二行 

                object obj = new object();
                object objMO = new object();
                object objItem = new object();

                //打印类型为SerialNumber1
                if (SerialNumber1 != null && this.ucLabelPrintType.SelectedItemValue.ToString().Equals(SerialNumber1.ParameterCode))
                {
                    printValueList.Clear();
                    printValueListLineTwo.Clear();
                    printRcard = string.Empty;
                    printRcardLineTwo = string.Empty;

                    for (int i = 0; i < ultraGridMain.Rows.Count; i++)
                    {
                        Infragistics.Win.UltraWinGrid.UltraGridRow row = ultraGridMain.Rows[i];
                        if (bool.Parse(row.Cells["Checked"].Value.ToString()))
                        {
                            obj = _MOFacade.GetMO2RCardLink(row.Cells["RCard"].Value.ToString());
                            objMO = _MOFacade.GetMO((obj as MO2RCARDLINK).MOCode);
                            objItem = _ItemFacade.GetItem(((objMO as MO)).ItemCode, GlobalVariables.CurrentOrganizations.First().OrganizationID);
                            Rcards.Add(row.Cells["RCard"].Value.ToString());
                        }
                    }
                    //至少有一条数据打印
                    if (Rcards.Count == 0)
                    {
                        this.ShowMessage(new UserControl.Message(UserControl.MessageType.Error, "$CS_Please_Take_OneDate"));
                        return;
                    }


                    for (int i = 0; i < Rcards.Count; i++)
                    {
                        //第一行
                        printRcard = " " + (objItem as Item).ItemDescription.Trim();
                        printRcard = FormatRcard(printRcard, objMO); //过滤Rcard

                        //第二行
                        printRcardLineTwo = Rcards[i].ToString();
                        printRcardLineTwo = printRcardLineTwo.Substring(printRcardLineTwo.Length - 8); //8代表 【四位流水号】+【四位编号】
                        printRcardLineTwo = printRcardLineTwo.Insert(4, "-");

                        printRcard = printRcard.PadRight(10, ' ');  //serialNumber打印位数10，不足空格补全
                        printRcardLineTwo = printRcardLineTwo.PadRight(10, ' ');

                        rcardsLineOne += printRcard + "#";  //两个printRcard中间加“#”分隔；
                        rcardsLineOne += printRcard + "#"; //两遍相同；
                        rcardsLineTwo += printRcardLineTwo + "#";
                        rcardsLineTwo += printRcardLineTwo + "#"; //第二行

                        NewlineCount += 2;

                        if (i == Rcards.Count - 1 || NewlineCount == 10)  //如果当前是要打印的最后一个或者10个数据 则要换行
                        {
                            rcardsLineOne += "\r\n";
                            rcardsLineTwo += "\r\n";
                            NewlineCount = 0;
                            printValueList.Add(rcardsLineOne); //第一行
                            printValueListLineTwo.Add(rcardsLineTwo); //第二行
                            rcardsLineOne = string.Empty;
                            rcardsLineTwo = string.Empty;
                        }
                        this.Enabled = false; //冻结页面数据
                        this.processBar.Visible = true;
                        this.processBar.Maximum = Rcards.Count;
                        this.processBar.Value = i;
                        Application.DoEvents();
                    }

                    //将两个ArrayList合并成一个
                    ArrayList printList = new ArrayList();
                    for (int i = 0; i < printValueList.Count; i++)
                    {
                        printList.Add(printValueList[i].ToString());
                        printList.Add(printValueListLineTwo[i].ToString());
                    }


                    //再调打印信息
                    SATOPrint.SATOPrinter.Print(printerName, printList, this.ucLabelPrintType.SelectedItemValue.ToString());
                }

                //打印类型为SerialNumber2
                if (SerialNumber2 != null && this.ucLabelPrintType.SelectedItemValue.ToString().Equals(SerialNumber2.ParameterCode))
                {
                    printValueList.Clear();
                    printValueListLineTwo.Clear();
                    printRcard = string.Empty;
                    printRcardLineTwo = string.Empty;

                    for (int i = 0; i < ultraGridMain.Rows.Count; i++)
                    {
                        Infragistics.Win.UltraWinGrid.UltraGridRow row = ultraGridMain.Rows[i];
                        if (bool.Parse(row.Cells["Checked"].Value.ToString()))
                        {
                            obj = _MOFacade.GetMO2RCardLink(row.Cells["RCard"].Value.ToString());
                            objMO = _MOFacade.GetMO((obj as MO2RCARDLINK).MOCode);
                            objItem = _ItemFacade.GetItem(((objMO as MO)).ItemCode, GlobalVariables.CurrentOrganizations.First().OrganizationID);
                            Rcards.Add(row.Cells["RCard"].Value.ToString());
                        }
                    }
                    //至少有一条数据打印
                    if (Rcards.Count == 0)
                    {
                        this.ShowMessage(new UserControl.Message(UserControl.MessageType.Error, "$CS_Please_Take_OneDate"));
                        return;
                    }

                    for (int i = 0; i < Rcards.Count; i++)
                    {
                        string serialNo = string.Empty; //四位流水号的后面三位
                        //第一行

                        if ((objItem as Item).ItemDescription.Length >= 9)
                        {
                            printRcard = (objItem as Item).ItemDescription.Trim().Substring(0, 9); //取产品描述的前9位
                        }
                        else
                        {
                            printRcard = (objItem as Item).ItemDescription.Trim();
                        }

                        // printRcard = (objItem as Item).ItemDescription.Trim().Substring(0, 9); //取产品描述的前9位
                        //第二行
                        serialNo = Rcards[i].ToString().Substring(Rcards[i].ToString().Length - 8).Substring(1, 3); //8代表 【四位流水号】+【四位编号】
                        printRcardLineTwo = " " + '-' + serialNo;
                        //截取四位流水号

                        printRcard = printRcard.PadRight(10, ' ');  //serialNumber打印位数10，不足空格补全
                        printRcardLineTwo = printRcardLineTwo.PadRight(10, ' ');
                        rcardsLineOne += printRcard + "#";  //两个printRcard中间加“#”分隔；
                        rcardsLineOne += printRcard + "#"; //两遍相同；
                        rcardsLineTwo += printRcardLineTwo + "#";
                        rcardsLineTwo += printRcardLineTwo + "#"; //第二行

                        NewlineCount += 2;

                        if (i == Rcards.Count - 1 || NewlineCount == 10)  //如果当前是要打印的最后一个或者10个数据 则要换行
                        {
                            rcardsLineOne += "\r\n";
                            rcardsLineTwo += "\r\n";
                            NewlineCount = 0;
                            printValueList.Add(rcardsLineOne); //第一行
                            printValueListLineTwo.Add(rcardsLineTwo); //第二行
                            rcardsLineOne = string.Empty;
                            rcardsLineTwo = string.Empty;
                        }
                        this.Enabled = false; //冻结页面数据
                        this.processBar.Visible = true;
                        this.processBar.Maximum = Rcards.Count;
                        this.processBar.Value = i;
                        Application.DoEvents();
                    }

                    //将两个ArrayList合并成一个
                    ArrayList printList = new ArrayList();
                    for (int i = 0; i < printValueList.Count; i++)
                    {
                        printList.Add(printValueList[i].ToString());
                        printList.Add(printValueListLineTwo[i].ToString());
                    }
                    //再调打印信息
                    SATOPrint.SATOPrinter.Print(printerName, printList, this.ucLabelPrintType.SelectedItemValue.ToString());

                }
            }

            #endregion

            //获取打印模板的路径
            //if (this.ucLabelComboxPrintTemplete.SelectedIndex < 0)
            //{
            //    this.ShowMessage("$Error_NO_TempLeteSelect");
            //    return;
            //}

            //打印类型为 Travelling Tag（600dpi）
            #region TravellingTag
            if (TravellingTag != null && this.ucLabelPrintType.SelectedItemValue.ToString().Equals(TravellingTag.ParameterCode))
            {
                ArrayList printValue = new ArrayList(); //存放最终打印值
                string fileName = Application.StartupPath + "\\PrintLabel\\" + TravellingTag.ParameterAlias + ".txt";
                bool IsShowProcessBar = false;
                int count = 0; //判断是否有Rcard要打印
                //判断模板文件是否能存在
                try
                {
                    FileStream textReader = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                }
                catch (Exception)
                {
                    this.ShowMessage(new UserControl.Message(UserControl.MessageType.Error, "$TemplateFile_doesnot_exist"));
                    return;
                }

                StringDictionary valueList = new StringDictionary();  //解析文件键值对
                valueList = SATOPrint.SATOPrintHelper.InitPrintDictionary();

                for (int i = 0; i < ultraGridMain.Rows.Count; i++)  //选中的Rcard 赋值
                {
                    count++;
                    Infragistics.Win.UltraWinGrid.UltraGridRow row = ultraGridMain.Rows[i];
                    if (bool.Parse(row.Cells["Checked"].Value.ToString()))
                    {

                        count--;
                        IsShowProcessBar = true;
                        printRcard = row.Cells["RCard"].Value.ToString().Trim();
                        //object obj = _MOFacade.GetMO2RCardLink(row.Cells["RCard"].Value.ToString());
                        //object objMO = _MOFacade.GetMO((obj as MO2RCARDLINK).MOCode);
                        //object objItem = _ItemFacade.GetItem(((objMO as MO)).ItemCode, GlobalVariables.CurrentOrganizations.First().OrganizationID);

                        //valueList["mocode"] = (objMO as MO).MOCode.Trim();
                        //valueList["itemdesc"] = (objItem as Item).ItemDescription.Trim();
                        //valueList["rcard"] = printRcard.Trim();
                        //valueList["moplanqty"] = ((int)(objMO as MO).MOPlanQty).ToString().Trim();
                        //valueList["lotno"] = " ";

                        //[Esc]V100[Esc]H620[Esc]OB[$serialno]

                        string moPlanQty = row.Cells["MOPLANQTY"].Value.ToString().Trim();
                        valueList["mocode"] = row.Cells["MoCode"].Value.ToString().Trim();
                        valueList["itemdesc"] = row.Cells["ITEMDESC"].Value.ToString().Trim();
                     

                        valueList["rcard"] = printRcard.Trim();
                        valueList["serialno"] = printRcard.Trim().Substring(9,4);

                        valueList["moplanqty"] = moPlanQty.Substring(0, moPlanQty.IndexOf('.'));
                        valueList["lotno"] = row.Cells["LOTNO"].Value.ToString().Trim();


                        try
                        {
                            printValue = SATOPrint.SATOPrintHelper.ParsePrintFile(fileName, valueList);
                            SATOPrint.SATOPrinter.Print(printerName, printValue);

                        }
                        catch (Exception)
                        {
                            this.Enabled = true;
                            this.processBar.Visible = false;
                            return;
                        }

                    }
                    if (IsShowProcessBar)
                    {
                        this.Enabled = false; //冻结页面数据
                        this.processBar.Visible = true;
                        this.processBar.Maximum = this.ultraGridMain.Rows.Count;
                        this.processBar.Value = i;
                        Application.DoEvents();
                    }
                }

                if (count == ultraGridMain.Rows.Count && count != 0)
                {
                    this.ShowMessage(new UserControl.Message(UserControl.MessageType.Error, "$CS_Please_Take_OneDate"));
                    this.Enabled = true;
                    this.processBar.Visible = false;
                    return;
                }
            }
            #endregion

            //打印类型为 Lot Serial Number(600dpi)-1  
            #region LotSerialNumber(600dpi)-1
            if (LotSerialNumber != null && this.ucLabelPrintType.SelectedItemValue.ToString().Equals(LotSerialNumber.ParameterCode))
            {
                bool IsShowProcessBar = false;
                ArrayList printValue = new ArrayList(); //存放最终打印值
                string fileName = Application.StartupPath + "\\PrintLabel\\OffLine CheckLabel.txt";
                int count = 0; //判断是否有Rcard要打印
                //判断模板文件是否能存在
                try
                {
                    //先检查打印rcard信息后需要打印的check标签模板是否存在
                    FileStream textReader = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    //后检查打印rcard模板是否存在 后面先打印rcard 使用rcard模板转化参数值
                    fileName = Application.StartupPath + "\\PrintLabel\\" + LotSerialNumber.ParameterAlias + ".txt";
                    textReader = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                }
                catch (Exception)
                {
                    this.ShowMessage(new UserControl.Message(UserControl.MessageType.Error, "$TemplateFile_doesnot_exist"));
                    return;
                }

                StringDictionary valueList = new StringDictionary();  //解析文件键值对
                valueList = SATOPrint.SATOPrintHelper.InitPrintDictionary();
                string itemDesc = string.Empty;//也供打印check标签使用
                string moLot = string.Empty;//供打印check标签使用
                bool everPrint = false;//是否打印过rcard信息，用于判断是否打印check标签
                for (int i = 0; i < ultraGridMain.Rows.Count; i++) //选中的Rcard 赋值
                {
                    count++;
                    Infragistics.Win.UltraWinGrid.UltraGridRow row = ultraGridMain.Rows[i];
                    if (bool.Parse(row.Cells["Checked"].Value.ToString()))
                    {
                        count--;
                        IsShowProcessBar = true;
                        printRcard = row.Cells["RCard"].Value.ToString().Trim();
                        //object obj = _MOFacade.GetMO2RCardLink(row.Cells["RCard"].Value.ToString());
                        //object objMO = _MOFacade.GetMO((obj as MO2RCARDLINK).MOCode);
                        //object objItem = _ItemFacade.GetItem(((objMO as MO)).ItemCode, GlobalVariables.CurrentOrganizations.First().OrganizationID);

                        //string itemDesc = (objItem as Item).ItemDescription;
                        itemDesc = row.Cells["ITEMDESC"].Value.ToString().Trim();
                        string itemCode = row.Cells["ITEM"].Value.ToString().Trim();
                        moLot = row.Cells["LOTNO"].Value.ToString().Trim();//供打印check标签使用
                        //新增逻辑 特殊ItemDesc转化
                        itemDesc = FormatItemDesc(itemDesc, itemCode);

                        valueList["itemdesc"] = itemDesc;
                        //valueList["rcard"] = printRcard;
                        valueList["rcard"] = printRcard.Substring(0, 13) + FormatHelper.GetFormatRcards(printRcard.Substring(0, 13)); //Rcard由17位转到14位
                        valueList["rcard0to3"] = printRcard.Substring(0, 3); //rcard前3位
                        valueList["rcard4to10"] = printRcard.Substring(3, 6); //rcard 从第4开始取6位
                        valueList["rcard10to14"] = printRcard.Substring(9, 4); //rcard 从第10开始取4为
                        valueList["rcardoverage"] = printRcard.Substring((3 + 6 + 4), printRcard.Length - 3 - 6 - 4); //前面截取后的剩余

                        try
                        {
                            printValue = SATOPrint.SATOPrintHelper.ParsePrintFile(fileName, valueList);
                            SATOPrint.SATOPrinter.Print(printerName, printValue);
                            everPrint = true;
                        }
                        catch (Exception)
                        {
                            this.Enabled = true;
                            this.processBar.Visible = false;
                            throw;
                        }
                    }

                    if (IsShowProcessBar)
                    {
                        this.Enabled = false; //冻结页面数据
                        this.processBar.Visible = true;
                        this.processBar.Maximum = ultraGridMain.Rows.Count;
                        this.processBar.Value = i;
                        Application.DoEvents();
                    }
                }
                //前面打印过rcard标签 这里要打印check标签
                if (everPrint)
                {
                    try
                    {
                        valueList.Clear();
                        valueList["itemdesc"] = itemDesc;
                        valueList["molot"] = moLot;
                        valueList["checkuser"] = MutiLanguages.ParserMessage("$CS_CHECK_USER_LABEL");//从多语言读取
                        fileName = Application.StartupPath + "\\PrintLabel\\OffLine CheckLabel.txt";
                        printValue = SATOPrint.SATOPrintHelper.ParsePrintFile(fileName, valueList);
                        SATOPrint.SATOPrinter.Print(printerName, printValue);

                       
                    }
                    catch (Exception)
                    {
                        this.Enabled = true;
                        this.processBar.Visible = false;
                        throw;
                    }
                }
                if (count == this.ultraGridMain.Rows.Count && count != 0)
                {
                    this.ShowMessage(new UserControl.Message(UserControl.MessageType.Error, "$CS_Please_Take_OneDate"));
                    this.Enabled = true;
                    this.processBar.Visible = false;
                    return;
                }

            }
            #endregion

            //打印类型为 Lot Serial Number(600dpi)-2 
            #region LotSerialNumber(600dpi)-2
            if (LotSerialNumber600_2 != null && this.ucLabelPrintType.SelectedItemValue.ToString().Equals(LotSerialNumber600_2.ParameterCode))
            {
                bool IsShowProcessBar = false;
                ArrayList printValue = new ArrayList(); //存放最终打印值
                string fileName = Application.StartupPath + "\\PrintLabel\\OffLine CheckLabel.txt";
                int count = 0; //判断是否有Rcard要打印
                bool everPrint = false;//是否打印过rcard信息，用于判断是否打印check标签
                //判断模板文件是否能存在
                try
                {
                    //先检查打印rcard信息后需要打印的check标签模板是否存在
                    FileStream textReader = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    //后检查打印rcard模板是否存在 后面先打印rcard 使用rcard模板转化参数值
                    fileName = Application.StartupPath + "\\PrintLabel\\" + LotSerialNumber600_2.ParameterAlias + ".txt";
                    textReader = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                }
                catch (Exception)
                {
                    this.ShowMessage(new UserControl.Message(UserControl.MessageType.Error, "$TemplateFile_doesnot_exist"));
                    return;
                }

                StringDictionary valueList = new StringDictionary();  //解析文件键值对
                valueList = SATOPrint.SATOPrintHelper.InitPrintDictionary();
                //保存选中的要打印的rcard信息
                List<RcardInfoForPrint> rcardsForPrint = new List<RcardInfoForPrint>();
                bool uncheckedContinue = false;//如果没选中是否重新执行循环遍历
                string itemDesc = string.Empty;//也供打印check标签使用
                string moLot = string.Empty;//供打印check标签使用
                for (int i = 0; i < ultraGridMain.Rows.Count; i++) //选中的Rcard 赋值
                {
                    count++;
                    Infragistics.Win.UltraWinGrid.UltraGridRow row = ultraGridMain.Rows[i];
                    if (bool.Parse(row.Cells["Checked"].Value.ToString()))
                    {
                        count--;
                        IsShowProcessBar = true;
                        printRcard = row.Cells["RCard"].Value.ToString().Trim();
                        itemDesc = row.Cells["ITEMDESC"].Value.ToString().Trim();
                        string itemCode = row.Cells["ITEM"].Value.ToString().Trim();
                        moLot = row.Cells["LOTNO"].Value.ToString().Trim();//供打印check标签使用
                        //新增逻辑 特殊ItemDesc转化
                        itemDesc = FormatItemDesc(itemDesc, itemCode);

                        RcardInfoForPrint rcardTemp = new RcardInfoForPrint();
                        rcardTemp.Item = itemCode;
                        rcardTemp.Itemdesc = itemDesc;
                        rcardTemp.Rcard = printRcard;
                        rcardsForPrint.Add(rcardTemp);

                        //两个rcard信息打印一次，每次打印两个条码
                        //如果rcardsForPrint中只有一个rcard信息，而且没有遍历到grid的末尾，继续查找第二个选中的rcard信息
                        if (rcardsForPrint.Count == 1 && i < ultraGridMain.Rows.Count - 1)
                        {
                            uncheckedContinue = true;
                            continue;
                        }
                    }
                    else
                    {
                        if (i < ultraGridMain.Rows.Count - 1 && uncheckedContinue)
                        {
                            continue;
                        }
                    }
                    if (rcardsForPrint != null && rcardsForPrint.Count > 0)
                    {
                        //取出rcardsForPrint中的两个或一个rcard信息
                        valueList["itemdesc1"] = rcardsForPrint[0].Itemdesc;
                        valueList["rcard1"] = rcardsForPrint[0].Rcard.Substring(0, 13) + FormatHelper.GetFormatRcards(rcardsForPrint[0].Rcard.Substring(0, 13)); //Rcard由17位转到14位
                        valueList["rcard10to3"] = rcardsForPrint[0].Rcard.Substring(0, 3); //rcard前3位
                        valueList["rcard14to10"] = rcardsForPrint[0].Rcard.Substring(3, 6); //rcard 从第4开始取6位
                        valueList["rcard110to14"] = rcardsForPrint[0].Rcard.Substring(9, 4); //rcard 从第10开始取4为
                        valueList["rcardoverage1"] = rcardsForPrint[0].Rcard.Substring((3 + 6 + 4), rcardsForPrint[0].Rcard.Length - 3 - 6 - 4); //前面截取后的剩余
                        //如果只有一个rcard信息，为第二个条码信息赋空值 这种情况是打印到最后一张，而且选择的rcard数量为奇数或者只选了一个rcard
                        if (rcardsForPrint.Count == 1)
                        {
                            valueList["itemdesc2"] = string.Empty;
                            valueList["rcard2"] = string.Empty; //Rcard由17位转到14位
                            valueList["rcard20to3"] = string.Empty;//rcard前3位
                            valueList["rcard24to10"] = string.Empty; //rcard 从第4开始取6位
                            valueList["rcard210to14"] = string.Empty; //rcard 从第10开始取4为
                            valueList["rcardoverage2"] = string.Empty; //前面截取后的剩余
                        }
                        //为第二个rcard赋值打印信息
                        else
                        {
                            valueList["itemdesc2"] = rcardsForPrint[1].Itemdesc;
                            valueList["rcard2"] = rcardsForPrint[1].Rcard.Substring(0, 13) + FormatHelper.GetFormatRcards(rcardsForPrint[1].Rcard.Substring(0, 13)); //Rcard由17位转到14位
                            valueList["rcard20to3"] = rcardsForPrint[1].Rcard.Substring(0, 3); //rcard前3位
                            valueList["rcard24to10"] = rcardsForPrint[1].Rcard.Substring(3, 6); //rcard 从第4开始取6位
                            valueList["rcard210to14"] = rcardsForPrint[1].Rcard.Substring(9, 4); //rcard 从第10开始取4为
                            valueList["rcardoverage2"] = rcardsForPrint[1].Rcard.Substring((3 + 6 + 4), rcardsForPrint[1].Rcard.Length - 3 - 6 - 4); //前面截取后的剩余
                        }
                        try
                        {
                            printValue = SATOPrint.SATOPrintHelper.ParsePrintFile(fileName, valueList);
                            //如果只有一个rcard信息 去掉第二个rcard的空白信息
                            if (rcardsForPrint.Count == 1)
                            {
                                printValue.RemoveRange(3, 3);
                            }
                            rcardsForPrint.Clear();//清空打印的rcard数据，为下一次打印做准备
                            uncheckedContinue = false;
                            SATOPrint.SATOPrinter.Print(printerName, printValue);
                            everPrint = true;//打印过rcard信息 后面要打印check标签
                        }
                        catch (Exception)
                        {
                            this.Enabled = true;
                            this.processBar.Visible = false;
                            throw;
                        }
                        if (IsShowProcessBar)
                        {
                            this.Enabled = false; //冻结页面数据
                            this.processBar.Visible = true;
                            this.processBar.Maximum = ultraGridMain.Rows.Count;
                            this.processBar.Value = i;
                            Application.DoEvents();
                        }
                    }
                }
                //前面打印过rcard标签 这里要打印check标签
                if (everPrint)
                {
                    try
                    {
                        valueList.Clear();
                        valueList["itemdesc"] = itemDesc;
                        valueList["molot"] = moLot;
                        valueList["checkuser"] = MutiLanguages.ParserMessage("$CS_CHECK_USER_LABEL");//从多语言读取
                        fileName = Application.StartupPath + "\\PrintLabel\\OffLine CheckLabel.txt";
                        printValue = SATOPrint.SATOPrintHelper.ParsePrintFile(fileName, valueList);
                        SATOPrint.SATOPrinter.Print(printerName, printValue);
                    }
                    catch (Exception)
                    {
                        this.Enabled = true;
                        this.processBar.Visible = false;
                        throw;
                    }
                }
                if (count == this.ultraGridMain.Rows.Count && count != 0)
                {
                    this.ShowMessage(new UserControl.Message(UserControl.MessageType.Error, "$CS_Please_Take_OneDate"));
                    this.Enabled = true;
                    this.processBar.Visible = false;
                    return;
                }

            }
            #endregion

            //打印类型为2D Lot SerialNumber
            #region 2DLotSerialNumber
            if (LotSerialNumber2D != null && this.ucLabelPrintType.SelectedItemValue.ToString().Equals(LotSerialNumber2D.ParameterCode))
            {
                ArrayList printValue = new ArrayList();
                ArrayList print = new ArrayList(); //存放最终打印值
                bool IsShowProcessBar = false;
                string printLineOne = string.Empty;
                string printLineTwo = string.Empty;
                string fileName = Application.StartupPath + "\\PrintLabel\\" + LotSerialNumber2D.ParameterAlias + ".txt";

                //判断模板文件是否能存在
                try
                {
                    FileStream textReader = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                }
                catch (Exception)
                {
                    this.ShowMessage(new UserControl.Message(UserControl.MessageType.Error, "$TemplateFile_doesnot_exist"));
                    return;
                }
                StringDictionary valueList = new StringDictionary();  //解析文件键值对
                valueList = SATOPrint.SATOPrintHelper.InitPrintDictionary();
                int count = 0;  //记录循环次数
                int printRcardCount = 0; //要打印Rcard的数量
                for (int i = 0; i < ultraGridMain.Rows.Count; i++) //选中的Rcard 赋值
                {
                    printRcardCount++;
                    Infragistics.Win.UltraWinGrid.UltraGridRow row = ultraGridMain.Rows[i];
                    if (bool.Parse(row.Cells["Checked"].Value.ToString()))
                    {
                        count++;
                        printRcardCount--;
                        IsShowProcessBar = true;
                        printRcard = row.Cells["RCard"].Value.ToString().Trim();
                        //object obj = _MOFacade.GetMO2RCardLink(row.Cells["RCard"].Value.ToString());
                        //object objMO = _MOFacade.GetMO((obj as MO2RCARDLINK).MOCode);

                        string serialNo = printRcard.Substring(printRcard.Length - 8, 4);
                        //string LotNo = (objMO as MO).LotNo.ToString().Trim();
                        string LotNo = row.Cells["LOTNO"].Value.ToString().Trim();

                        string rcardkey = "rcard" + count.ToString();
                        string serialfourKey = "fourserialno" + count.ToString();
                        string lotNokey = "lotno" + count.ToString();
                        valueList[rcardkey] = printRcard;
                        valueList[serialfourKey] = serialNo;
                        valueList[lotNokey] = LotNo;
                    }

                    if (count == 5)  //五个条码或者数据结束时 就换行
                    {
                        count = 0;
                        printValue = SATOPrint.SATOPrintHelper.ParsePrintFile(fileName, valueList);
                        print.AddRange(printValue);
                        printValue.Clear();
                        valueList.Clear();  //参数值对清空  否则没有覆盖会一直存在
                    }

                    if (count != 0 && i == ultraGridMain.Rows.Count - 1)
                    {
                        count = 0;
                        printValue = SATOPrint.SATOPrintHelper.ParsePrintFile(fileName, valueList);
                        print.AddRange(printValue);
                        printValue.Clear();
                        valueList.Clear();
                    }
                    if (IsShowProcessBar)
                    {
                        this.Enabled = false; //冻结页面数据
                        this.processBar.Visible = true;
                        this.processBar.Maximum = ultraGridMain.Rows.Count;
                        this.processBar.Value = i;
                        Application.DoEvents();
                    }
                }
                if (printRcardCount == this.ultraGridMain.Rows.Count && printRcardCount != 0)
                {
                    this.ShowMessage(new UserControl.Message(UserControl.MessageType.Error, "$CS_Please_Take_OneDate"));
                    this.Enabled = true;
                    this.processBar.Visible = false;
                    return;
                }

                if (print.Count != 0)
                {
                    int Loopcount = 0; //循环计数
                    for (int i = 0; i < print.Count; i++)
                    {
                        Loopcount++;
                        if (Loopcount <= 10)
                        {
                            printLineOne += print[i].ToString().Trim();
                            if (Loopcount == 10)
                            {
                                printLineOne += "\r\n";
                                printValue.Add(printLineOne);
                                printLineOne = string.Empty;
                            }
                        }
                        if (Loopcount > 10)
                        {
                            printLineTwo += print[i].ToString().Trim();
                            if (Loopcount == 15)
                            {
                                printLineOne += "\r\n";
                                printValue.Add(printLineTwo);
                                printLineTwo = string.Empty;
                            }
                        }
                        if (Loopcount == 15)  //最后 清空计数
                        {
                            Loopcount = 0;
                        }
                    }

                }

                try
                {
                    SATOPrint.SATOPrinter.Print2DLabel(printerName, printValue);
                }
                catch (Exception)
                {
                    //给出提示信息
                    this.Enabled = true;
                    this.processBar.Visible = false;
                    throw;
                }

            }
            #endregion

            //打印类型为Off Lot Serial Number
            #region Off Lot Serial Number
            //////打印类型为Off Lot Serial Number
            ////if (this.ucLabelPrintType.SelectedItemValue.ToString().Equals(OffLotSerialNumber.ParameterCode))
            ////{
            ////    ArrayList printValue = new ArrayList(); //存放最终打印值
            ////    ArrayList print = new ArrayList();
            ////    string fileName = Application.StartupPath + "\\PrintLabel\\" + OffLotSerialNumber.ParameterAlias + ".txt";

            ////    //判断模板文件是否能存在
            ////    try
            ////    {
            ////        FileStream textReader = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            ////    }
            ////    catch (Exception)
            ////    {
            ////        this.ShowMessage(new UserControl.Message(UserControl.MessageType.Error, "$TemplateFile_doesnot_exist"));
            ////        return;
            ////    }

            ////    StringDictionary valueList = new StringDictionary();  //解析文件键值对
            ////    valueList = SATOPrint.SATOPrintHelper.InitPrintDictionary();
            ////    int count = 0;  //记录循环次数
            ////    int printRcardCount = 0; //要打印Rcard的数量
            ////    bool IsShowProcessBar = false;
            ////    for (int i = 0; i < ultraGridMain.Rows.Count; i++)  //替换模板里面的变量值
            ////    {
            ////        printRcardCount++;
            ////        Infragistics.Win.UltraWinGrid.UltraGridRow row = ultraGridMain.Rows[i];
            ////        if (bool.Parse(row.Cells["Checked"].Value.ToString()))
            ////        {
            ////            count++;
            ////            printRcardCount--;
            ////            IsShowProcessBar = true;
            ////            printRcard = row.Cells["RCard"].Value.ToString().Trim();
            ////            printRcard = printRcard.Substring(0, 13) + FormatHelper.GetFormatRcards(printRcard.Substring(0, 13));
            ////            //object obj = _MOFacade.GetMO2RCardLink(row.Cells["RCard"].Value.ToString());
            ////            //object objMO = _MOFacade.GetMO((obj as MO2RCARDLINK).MOCode);
            ////            //object objItem = _ItemFacade.GetItem(((objMO as MO)).ItemCode, GlobalVariables.CurrentOrganizations.First().OrganizationID);
            ////            //string itemDesc = (objItem as Item).ItemDescription;

            ////            string itemDesc = row.Cells["ITEMDESC"].Value.ToString().Trim();
            ////            string itemCode = row.Cells["ITEM"].Value.ToString().Trim();
            ////            //特殊的ItemDesc 需要转化
            ////            itemDesc = FormatItemDesc(itemDesc,itemCode);

            ////            string itemdescKey = "itemdesc" + count.ToString();
            ////            string rcardKey = "rcard" + count.ToString();
            ////            string rcardFormatKey = "rcardFormat" + count.ToString();

            ////            valueList[itemdescKey] = itemDesc;
            ////            valueList[rcardKey] = printRcard;
            ////            string printRcard17 = row.Cells["RCard"].Value.ToString().Trim();
            ////            valueList[rcardFormatKey] = printRcard17.Substring(0, 3) + '-' + printRcard17.Substring(3, 6) + '-' + printRcard17.Substring(9, 4) + '-' + printRcard17.Substring((3 + 6 + 4), printRcard17.Length - 3 - 6 - 4);


            ////        }
            ////        if (IsShowProcessBar)
            ////        {
            ////            this.Enabled = false; //冻结页面数据
            ////            this.processBar.Visible = true;
            ////            this.processBar.Maximum = ultraGridMain.Rows.Count;
            ////            this.processBar.Value = i;
            ////            Application.DoEvents();
            ////        }

            ////        if (count == 2)  //五个条码或者数据结束时 就换行
            ////        {
            ////            count = 0;
            ////            try
            ////            {
            ////                printValue = SATOPrint.SATOPrintHelper.ParsePrintFile(fileName, valueList);
            ////                SATOPrint.SATOPrinter.Print(printerName, printValue);

            ////            }
            ////            catch (Exception)
            ////            {
            ////                this.Enabled = true;
            ////                return;
            ////            }
            ////            printValue.Clear();
            ////            valueList.Clear();  //参数值对清空  否则没有覆盖会一直存在
            ////        }
            ////        if (count != 0 && i == ultraGridMain.Rows.Count - 1)
            ////        {
            ////            count = 0;
            ////            try
            ////            {
            ////                printValue = SATOPrint.SATOPrintHelper.ParsePrintFile(fileName, valueList);
            ////                SATOPrint.SATOPrinter.Print(printerName, printValue);
            ////            }
            ////            catch (Exception)
            ////            {
            ////                this.Enabled = true;
            ////                this.processBar.Visible = false;
            ////                return;
            ////            }
            ////            printValue.Clear();
            ////            valueList.Clear();  //参数值对清空  否则没有覆盖会一直存在
            ////        }
            ////    }
            ////    if (printRcardCount == ultraGridMain.Rows.Count && printRcardCount != 0)
            ////    {
            ////        this.ShowMessage(new UserControl.Message(UserControl.MessageType.Error, "$CS_Please_Take_OneDate"));
            ////        this.Enabled = true;
            ////        this.processBar.Visible = false;
            ////        return;
            ////    }
            ////}

            #endregion

            //打印类型为Lot Serial Number 300DPI
            #region LotSerailNumber300DPI
            if (LotSerial300DPI != null && this.ucLabelPrintType.SelectedItemValue.ToString().Equals(LotSerial300DPI.ParameterCode))
            {
                bool IsShowProcessBar = false;
                ArrayList printValue = new ArrayList(); //存放最终打印值
                string fileName = Application.StartupPath + "\\PrintLabel\\" + LotSerial300DPI.ParameterAlias + ".txt";
                int count = 0; //判断是否有Rcard要打印
                //判断模板文件是否能存在
                try
                {
                    FileStream textReader = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                }
                catch (Exception)
                {
                    this.ShowMessage(new UserControl.Message(UserControl.MessageType.Error, "$TemplateFile_doesnot_exist"));
                    return;
                }

                StringDictionary valueList = new StringDictionary();  //解析文件键值对
                valueList = SATOPrint.SATOPrintHelper.InitPrintDictionary();

                for (int i = 0; i < ultraGridMain.Rows.Count; i++) //选中的Rcard 赋值
                {
                    count++;
                    Infragistics.Win.UltraWinGrid.UltraGridRow row = ultraGridMain.Rows[i];
                    if (bool.Parse(row.Cells["Checked"].Value.ToString()))
                    {
                        count--;
                        IsShowProcessBar = true;
                        printRcard = row.Cells["RCard"].Value.ToString().Trim();

                        //object obj = _MOFacade.GetMO2RCardLink(row.Cells["RCard"].Value.ToString());
                        //object objMO = _MOFacade.GetMO((obj as MO2RCARDLINK).MOCode);
                        //object objItem = _ItemFacade.GetItem(((objMO as MO)).ItemCode, GlobalVariables.CurrentOrganizations.First().OrganizationID);
                        // string itemDesc = (objItem as Item).ItemDescription;
                        //新增逻辑 特殊ItemDesc转化

                        string itemDesc = row.Cells["ITEMDESC"].Value.ToString().Trim();
                        string itemCode = row.Cells["ITEM"].Value.ToString().Trim();
                        itemDesc = FormatItemDesc(itemDesc, itemCode);
                        //printRcard = printRcard.Substring(0, 13) + FormatHelper.GetFormatRcards(printRcard.Substring(0, 13)); //Rcard由17位转到14位

                        valueList["itemdesc"] = itemDesc;
                        valueList["rcard"] = printRcard.Substring(0, 13) + FormatHelper.GetFormatRcards(printRcard.Substring(0, 13)); //Rcard由17位转到14位;
                        valueList["rcard0to3"] = printRcard.Substring(0, 3); //rcard前3位
                        valueList["rcard4to10"] = printRcard.Substring(3, 6); //rcard 从第4开始取6位
                        valueList["rcard10to14"] = printRcard.Substring(9, 4); //rcard 从第10开始取4为
                        valueList["rcardoverage"] = printRcard.Substring((3 + 6 + 4), printRcard.Length - 3 - 6 - 4); //前面截取后的剩余

                        try
                        {
                            printValue = SATOPrint.SATOPrintHelper.ParsePrintFile(fileName, valueList);
                            SATOPrint.SATOPrinter.Print(printerName, printValue);
                        }
                        catch (Exception)
                        {
                            this.Enabled = true;
                            this.processBar.Visible = false;
                            throw;
                        }


                    }
                    if (IsShowProcessBar)
                    {
                        this.Enabled = false; //锁定页面，防止数据被修改
                        this.processBar.Visible = true;
                        this.processBar.Maximum = ultraGridMain.Rows.Count;
                        this.processBar.Value = i;
                        Application.DoEvents();
                    }
                }
                if (count == this.ultraGridMain.Rows.Count && count != 0)
                {
                    this.ShowMessage(new UserControl.Message(UserControl.MessageType.Error, "$CS_Please_Take_OneDate"));
                    this.Enabled = true;
                    this.processBar.Visible = false;
                    return;
                }

            }
            #endregion

            //打印类型为2D Lot SerialNumber 300DPI
            #region  2D Lot SerialNumber 300DPI
            if (LotSerial2D300DPI != null && this.ucLabelPrintType.SelectedItemValue.ToString().Equals(LotSerial2D300DPI.ParameterCode))
            {
                ArrayList printValue = new ArrayList();
                ArrayList print = new ArrayList(); //存放最终打印值

                bool IsShowProcessBar = false;
                string printLineOne = string.Empty;
                string printLineTwo = string.Empty;
                string fileName = Application.StartupPath + "\\PrintLabel\\" + LotSerial2D300DPI.ParameterAlias + ".txt";

                //判断模板文件是否能存在
                try
                {
                    FileStream textReader = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                }
                catch (Exception)
                {
                    this.ShowMessage(new UserControl.Message(UserControl.MessageType.Error, "$TemplateFile_doesnot_exist"));
                    this.Enabled = true;
                    return;
                }
                StringDictionary valueList = new StringDictionary();  //解析文件键值对
                valueList = SATOPrint.SATOPrintHelper.InitPrintDictionary();
                int count = 0;  //记录循环次数
                int printRcardCount = 0; //要打印Rcard的数量
                for (int i = 0; i < ultraGridMain.Rows.Count; i++) //选中的Rcard 赋值
                {
                    printRcardCount++;
                    Infragistics.Win.UltraWinGrid.UltraGridRow row = ultraGridMain.Rows[i];
                    if (bool.Parse(row.Cells["Checked"].Value.ToString()))
                    {
                        count++;
                        printRcardCount--;
                        IsShowProcessBar = true;
                        printRcard = row.Cells["RCard"].Value.ToString().Trim();
                        //object obj = _MOFacade.GetMO2RCardLink(row.Cells["RCard"].Value.ToString());
                        //object objMO = _MOFacade.GetMO((obj as MO2RCARDLINK).MOCode);

                        string lotNO = row.Cells["LOTNO"].Value.ToString().Trim();
                        string serialNo = printRcard.Substring(printRcard.Length - 8, 4);
                        //string LotNo = (objMO as MO).LotNo.ToString().Trim();

                        string rcardkey = "rcard" + count.ToString();
                        string serialfourKey = "fourserialno" + count.ToString();
                        string lotNokey = "lotno" + count.ToString();
                        valueList[rcardkey] = printRcard;
                        valueList[serialfourKey] = serialNo;
                        valueList[lotNokey] = lotNO;

                    }

                    if (count == 5)  //五个条码或者数据结束时 就换行
                    {
                        count = 0;
                        printValue = SATOPrint.SATOPrintHelper.ParsePrintFile(fileName, valueList);
                        print.AddRange(printValue);
                        printValue.Clear();
                        valueList.Clear();  //参数值对清空  否则没有覆盖会一直存在
                    }

                    if (count != 0 && i == ultraGridMain.Rows.Count - 1)
                    {
                        count = 0;
                        printValue = SATOPrint.SATOPrintHelper.ParsePrintFile(fileName, valueList);
                        print.AddRange(printValue);
                        printValue.Clear();
                        valueList.Clear();
                    }

                    if (IsShowProcessBar)
                    {
                        this.Enabled = false; //锁定页面，防止数据被修改
                        this.processBar.Visible = true;
                        this.processBar.Maximum = ultraGridMain.Rows.Count;
                        this.processBar.Value = i;
                        Application.DoEvents();
                    }

                }
                if (printRcardCount == this.ultraGridMain.Rows.Count && printRcardCount != 0)
                {
                    this.ShowMessage(new UserControl.Message(UserControl.MessageType.Error, "$CS_Please_Take_OneDate"));
                    this.Enabled = true;
                    this.processBar.Visible = false;
                    return;
                }

                if (print.Count != 0)
                {
                    int Loopcount = 0; //循环计数
                    for (int i = 0; i < print.Count; i++)
                    {
                        Loopcount++;
                        if (Loopcount <= 10)
                        {
                            printLineOne += print[i].ToString().Trim();
                            if (Loopcount == 10)
                            {
                                printLineOne += "\r\n";
                                printValue.Add(printLineOne);
                                printLineOne = string.Empty;
                            }
                        }
                        if (Loopcount > 10)
                        {
                            printLineTwo += print[i].ToString().Trim();
                            if (Loopcount == 15)
                            {
                                printLineOne += "\r\n";
                                printValue.Add(printLineTwo);
                                printLineTwo = string.Empty;
                            }
                        }
                        if (Loopcount == 15)  //最后 清空计数
                        {
                            Loopcount = 0;
                        }
                    }

                }

                try
                {
                    SATOPrint.SATOPrinter.Print2DLabel(printerName, printValue);
                }
                catch (Exception)
                {
                    //给出提示信息
                    this.Enabled = true;
                    throw;
                }

            }
            #endregion

            //打印类型为 SMTOQCSCANNING 
            #region SMT OQC SCANNING
            if (printSMTOQC != null && this.ucLabelPrintType.SelectedItemValue.ToString().Equals(printSMTOQC.ParameterCode))
            {
                bool IsShowProcessBar = false;
                ArrayList printValue = new ArrayList(); //存放最终打印值
                int count = 0; //判断是否有Rcard要打印
                string fileName = string.Empty;
                //判断模板文件是否能存在
                try
                {
                    //检查打印rcard模板是否存在
                    fileName = Application.StartupPath + "\\PrintLabel\\" + printSMTOQC.ParameterAlias + ".txt";
                    FileStream textReader = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                }
                catch (Exception)
                {
                    this.ShowMessage(new UserControl.Message(UserControl.MessageType.Error, "$TemplateFile_doesnot_exist"));
                    return;
                }

                StringDictionary valueList = new StringDictionary();  //解析文件键值对
                valueList = SATOPrint.SATOPrintHelper.InitPrintDictionary();
                //保存选中的要打印的rcard信息
                List<RcardInfoForPrint> rcardsForPrint = new List<RcardInfoForPrint>();
                bool uncheckedContinue = false;//如果没选中是否重新执行循环遍历
                string itemDesc = string.Empty;
                for (int i = 0; i < ultraGridMain.Rows.Count; i++) //选中的Rcard 赋值
                {
                    count++;
                    Infragistics.Win.UltraWinGrid.UltraGridRow row = ultraGridMain.Rows[i];
                    if (bool.Parse(row.Cells["Checked"].Value.ToString()))
                    {
                        count--;
                        IsShowProcessBar = true;
                        printRcard = row.Cells["RCard"].Value.ToString().Trim();
                        itemDesc = row.Cells["ITEMDESC"].Value.ToString().Trim();
                        string itemCode = row.Cells["ITEM"].Value.ToString().Trim();
                        //新增逻辑 特殊ItemDesc转化
                        itemDesc = FormatItemDesc(itemDesc, itemCode);

                        RcardInfoForPrint rcardTemp = new RcardInfoForPrint();
                        rcardTemp.Item = itemCode;
                        rcardTemp.Itemdesc = itemDesc;
                        rcardTemp.Rcard = printRcard;
                        rcardsForPrint.Add(rcardTemp);

                        //两个rcard信息打印一次，每次打印两个条码
                        //如果rcardsForPrint中只有一个rcard信息，而且没有遍历到grid的末尾，继续查找第二个选中的rcard信息
                        if (rcardsForPrint.Count == 1 && i < ultraGridMain.Rows.Count - 1)
                        {
                            uncheckedContinue = true;
                            continue;
                        }
                    }
                    else
                    {
                        if (i < ultraGridMain.Rows.Count - 1 && uncheckedContinue)
                        {
                            continue;
                        }
                    }
                    if (rcardsForPrint != null && rcardsForPrint.Count > 0)
                    {
                        //取出rcardsForPrint中的两个或一个rcard信息
                        valueList["itemdesc1"] = rcardsForPrint[0].Itemdesc;
                        //valueList["rcard1"] = rcardsForPrint[0].Rcard.Substring(0, 13) + FormatHelper.GetFormatRcards(rcardsForPrint[0].Rcard.Substring(0, 13)); //Rcard由17位转到14位
                        valueList["rcard1"] = rcardsForPrint[0].Rcard;
                        valueList["rcard14to10"] = rcardsForPrint[0].Rcard.Substring(3, 6); //rcard 从第4开始取6位
                        valueList["rcard110to14"] = rcardsForPrint[0].Rcard.Substring(9, 4); //rcard 从第10开始取4为
                        //如果只有一个rcard信息，为第二个条码信息赋空值 这种情况是打印到最后一张，而且选择的rcard数量为奇数或者只选了一个rcard
                        if (rcardsForPrint.Count == 1)
                        {
                            valueList["itemdesc2"] = string.Empty;
                            valueList["rcard2"] = string.Empty; //Rcard由17位转到14位
                            valueList["rcard24to10"] = string.Empty; //rcard 从第4开始取6位
                            valueList["rcard210to14"] = string.Empty; //rcard 从第10开始取4为
                        }
                        //为第二个rcard赋值打印信息
                        else
                        {
                            valueList["itemdesc2"] = rcardsForPrint[1].Itemdesc;
                            //valueList["rcard2"] = rcardsForPrint[1].Rcard.Substring(0, 13) + FormatHelper.GetFormatRcards(rcardsForPrint[1].Rcard.Substring(0, 13)); //Rcard由17位转到14位
                            valueList["rcard2"] = rcardsForPrint[1].Rcard;
                            valueList["rcard24to10"] = rcardsForPrint[1].Rcard.Substring(3, 6); //rcard 从第4开始取6位
                            valueList["rcard210to14"] = rcardsForPrint[1].Rcard.Substring(9, 4); //rcard 从第10开始取4为
                        }
                        try
                        {
                            printValue = SATOPrint.SATOPrintHelper.ParsePrintFile(fileName, valueList);
                            //如果只有一个rcard信息 去掉第二个rcard的空白信息
                            if (rcardsForPrint.Count == 1)
                            {
                                printValue.RemoveRange(4, 4);
                            }
                            rcardsForPrint.Clear();//清空打印的rcard数据，为下一次打印做准备
                            uncheckedContinue = false;
                            SATOPrint.SATOPrinter.Print(printerName, printValue);
                        }
                        catch (Exception)
                        {
                            this.Enabled = true;
                            this.processBar.Visible = false;
                            throw;
                        }
                        if (IsShowProcessBar)
                        {
                            this.Enabled = false; //冻结页面数据
                            this.processBar.Visible = true;
                            this.processBar.Maximum = ultraGridMain.Rows.Count;
                            this.processBar.Value = i;
                            Application.DoEvents();
                        }
                    }
                }
               
                if (count == this.ultraGridMain.Rows.Count && count != 0)
                {
                    this.ShowMessage(new UserControl.Message(UserControl.MessageType.Error, "$CS_Please_Take_OneDate"));
                    this.Enabled = true;
                    this.processBar.Visible = false;
                    return;
                }

            }
            #endregion
            //逻辑调整 注释该功能
            //  printRcardList();

            // 打印完成后的数据处理
            #region AfterPrint
            AfterPrint();
            this.Enabled = true;
            IsNeedPassword = false; //是否打印标志初始化
            this.processBar.Visible = false;

            #endregion
        }

        private void ucButtonExit_Click(object sender, EventArgs e)
        {
            this.txtMoCodeEdit.Value = "";
            this.ucLabelComboxCheckType.Clear();
            this.checkBoxSNContentCheck.Enabled = true;
            this.checkBoxSNContentCheck.Checked = false;
            this.ucLabelEditRCardPrefix.Value = "";
            this.ucLERCStartRcard.Value = "";
            this.ucLERCEndRcard.Value = "";
            this.ucLabelRCardLength.Value = "";
            this.ucLabelRCardLength.Checked = false;
            this.ucLabelRCardLength.Enabled = true;
            this.ucLabelCreateQTY.Value = "";

            //编辑区的控件恢复可用
            this.ucLabelCreateQTY.Enabled = false;
            this.ucLabelRCardLength.Enabled = false;
            this.ucLERCEndRcard.Enabled = false;
            this.ucLERCStartRcard.Enabled = false;
            this.ucLabelEditRCardPrefix.Enabled = false;

            this.ultraGridMain.DataSource = null;
            this.ultraGridMain.UpdateData();
        }


        private void ucBtnExit_Click(object sender, EventArgs e)
        {
        }

        #endregion

        #region 入库批号生成Grid()

        private void InitializeMainGrid()
        {
            this.m_CheckList = new DataSet();

            this.m_mo2RcardLink = new DataTable("MO2RcardLink");
            this.m_mo2RcardLink.Columns.Add("Checked", typeof(string));
            this.m_mo2RcardLink.Columns.Add("MoCode", typeof(string));
            this.m_mo2RcardLink.Columns.Add("RCard", typeof(string));
            this.m_mo2RcardLink.Columns.Add("PrintTimes", typeof(string));
            this.m_mo2RcardLink.Columns.Add("lastPrintUSER", typeof(string));
            this.m_mo2RcardLink.Columns.Add("lastPrintDate", typeof(string));
            this.m_mo2RcardLink.Columns.Add("lastPrintTime", typeof(string));
            this.m_mo2RcardLink.Columns.Add("MUser", typeof(string));
            this.m_mo2RcardLink.Columns.Add("MDate", typeof(string));
            this.m_mo2RcardLink.Columns.Add("MTime", typeof(string));
            this.m_mo2RcardLink.Columns.Add("LOTNO", typeof(string));
            this.m_mo2RcardLink.Columns.Add("ITEM", typeof(string));
            this.m_mo2RcardLink.Columns.Add("ITEMDESC", typeof(string));
            this.m_mo2RcardLink.Columns.Add("MOPLANQTY", typeof(decimal));
            this.m_CheckList.Tables.Add(this.m_mo2RcardLink);
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
            e.Layout.Bands[0].ScrollTipField = "MoCode";

            // 设置列宽和列名称
            e.Layout.Bands[0].Columns["Checked"].Header.Caption = "";
            e.Layout.Bands[0].Columns["MoCode"].Header.Caption = "工单编码";
            e.Layout.Bands[0].Columns["RCard"].Header.Caption = "产品序列号";
            e.Layout.Bands[0].Columns["PrintTimes"].Header.Caption = "打印数量";
            e.Layout.Bands[0].Columns["lastPrintUSER"].Header.Caption = "最后打印人";
            e.Layout.Bands[0].Columns["lastPrintDate"].Header.Caption = "最后日期";
            e.Layout.Bands[0].Columns["lastPrintTime"].Header.Caption = "最后时间";
            e.Layout.Bands[0].Columns["MUser"].Header.Caption = "维护人";
            e.Layout.Bands[0].Columns["MDate"].Header.Caption = "维护日期";
            e.Layout.Bands[0].Columns["MTime"].Header.Caption = "维护时间";
            e.Layout.Bands[0].Columns["LOTNO"].Header.Caption = "LotNo";
            e.Layout.Bands[0].Columns["ITEM"].Header.Caption = "产品代码";
            e.Layout.Bands[0].Columns["ITEMDESC"].Header.Caption = "产品描述";
            e.Layout.Bands[0].Columns["MOPLANQTY"].Header.Caption = "工单计划数量";

            e.Layout.Bands[0].Columns["Checked"].Width = 40;
            e.Layout.Bands[0].Columns["MoCode"].Width = 100;
            e.Layout.Bands[0].Columns["RCard"].Width = 100;
            e.Layout.Bands[0].Columns["PrintTimes"].Width = 80;
            e.Layout.Bands[0].Columns["lastPrintUSER"].Width = 100;
            e.Layout.Bands[0].Columns["lastPrintDate"].Width = 100;
            e.Layout.Bands[0].Columns["lastPrintTime"].Width = 100;
            e.Layout.Bands[0].Columns["MUser"].Width = 100;
            e.Layout.Bands[0].Columns["MDate"].Width = 100;
            e.Layout.Bands[0].Columns["MTime"].Width = 100;
            e.Layout.Bands[0].Columns["LOTNO"].Width = 100;
            e.Layout.Bands[0].Columns["ITEM"].Width = 100;
            e.Layout.Bands[0].Columns["ITEMDESC"].Width = 100;
            e.Layout.Bands[0].Columns["MOPLANQTY"].Width = 100;

            // 设置栏位是否允许编辑，及栏位的显示形式
            e.Layout.Bands[0].Columns["Checked"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;
            e.Layout.Bands[0].Columns["MoCode"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
            e.Layout.Bands[0].Columns["RCard"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[0].Columns["MUser"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[0].Columns["MDate"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[0].Columns["MTime"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[0].Columns["PrintTimes"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[0].Columns["lastPrintUSER"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[0].Columns["lastPrintDate"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[0].Columns["lastPrintTime"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;

            e.Layout.Bands[0].Columns["LOTNO"].Hidden = true;
            e.Layout.Bands[0].Columns["ITEM"].Hidden = true;
            e.Layout.Bands[0].Columns["ITEMDESC"].Hidden = true;
            e.Layout.Bands[0].Columns["MOPLANQTY"].Hidden = true;

            //设置可编辑列的颜色
            //e.Layout.Bands[0].Columns["DATECODE"].CellAppearance.BackColor = Color.LawnGreen;
            //e.Layout.Bands[0].Columns["VenderLotNO"].CellAppearance.BackColor = Color.LawnGreen ;
            //e.Layout.Bands[0].Columns["VenderITEMCODE"].CellAppearance.BackColor = Color.LawnGreen;

            // 允许筛选
            e.Layout.Bands[0].Columns["MoCode"].AllowRowFiltering = DefaultableBoolean.True;
            e.Layout.Bands[0].Columns["MoCode"].SortIndicator = SortIndicator.Ascending;

            this.InitGridLanguage(ultraGridMain);


        }

        private void ultraGridMain_CellChange(object sender, CellEventArgs e)
        {
            this.ultraGridMain.UpdateData();
            if (e.Cell.Column.Key == "Checked")
            {
                if (e.Cell.Row.Band.Index == 0) //Parent
                {

                }

                if (e.Cell.Row.Band.Index == 1) // Child
                {
                    if (Convert.ToBoolean(e.Cell.Value) == true)
                    {
                        //e.Cell.Row.ParentRow.Cells["Checked"].Value = e.Cell.Value;
                    }

                }
            }
            this.ultraGridMain.UpdateData();
        }

        private void LoadCheckList(string rcard, string mocode)
        {
            try
            {
                this.ClearCheckList();

                // object[] mo2RcardLinks = _MOFacade.QueryMO2RCardLink(rcard, mocode);
                object[] mo2RcardLinks = _MOFacade.GetMO2RCardLinkMOItemForQuery(rcard, mocode);
                DataRow rowMo2Rcard;

                foreach (MO2RCARDLINKForQuery item in mo2RcardLinks)
                {
                    rowMo2Rcard = this.m_CheckList.Tables["MO2RcardLink"].NewRow();
                    rowMo2Rcard["Checked"] = "true";
                    rowMo2Rcard["MoCode"] = item.MOCode;
                    rowMo2Rcard["RCard"] = item.RCard;
                    rowMo2Rcard["PrintTimes"] = item.PrintTimes;
                    rowMo2Rcard["lastPrintUSER"] = item.LastPrintUSER;
                    rowMo2Rcard["lastPrintDate"] = FormatHelper.ToDateString(item.LastPrintDate);
                    rowMo2Rcard["lastPrintTime"] = FormatHelper.ToTimeString(item.LastPrintTime);
                    rowMo2Rcard["MUser"] = item.MUser;
                    rowMo2Rcard["MDate"] = FormatHelper.ToDateString(item.MDate);
                    rowMo2Rcard["MTime"] = FormatHelper.ToTimeString(item.MTime);
                    rowMo2Rcard["LOTNO"] = item.LotNo;
                    rowMo2Rcard["ITEM"] = item.Item;
                    rowMo2Rcard["ITEMDESC"] = item.ItemDesc;
                    rowMo2Rcard["MOPLANQTY"] = item.MoPlanQty.ToString();

                    this.m_CheckList.Tables["MO2RcardLink"].Rows.Add(rowMo2Rcard);
                }
                this.m_CheckList.Tables["MO2RcardLink"].AcceptChanges();
                this.m_CheckList.AcceptChanges();
                this.ultraGridMain.DataSource = this.m_CheckList;
                this.ultraGridMain.UpdateData();
            }
            catch (Exception ex)
            {
            }
        }

        //add by Jarvis
        private void LoadCheckListNewCreate(string moCode, string beginRcard, string endRcard)
        {
            try
            {
                this.ClearCheckList();

                //object[] mo2RcardLinks = _MOFacade.GetMO2RCardLinkByMoCode(moCode, beginRcard, endRcard, "");
                object[] mo2RcardLinks = _MOFacade.GetMO2RCardLinkMOItemForQuery(string.Empty, moCode);
                DataRow rowMo2Rcard;

                foreach (MO2RCARDLINKForQuery item in mo2RcardLinks)
                {
                    rowMo2Rcard = this.m_CheckList.Tables["MO2RcardLink"].NewRow();
                    rowMo2Rcard["Checked"] = "true";
                    rowMo2Rcard["MoCode"] = item.MOCode;
                    rowMo2Rcard["RCard"] = item.RCard;
                    rowMo2Rcard["PrintTimes"] = item.PrintTimes;
                    rowMo2Rcard["lastPrintUSER"] = item.LastPrintUSER;
                    rowMo2Rcard["lastPrintDate"] = FormatHelper.ToDateString(item.LastPrintDate);
                    rowMo2Rcard["lastPrintTime"] = FormatHelper.ToTimeString(item.LastPrintTime);
                    rowMo2Rcard["MUser"] = item.MUser;
                    rowMo2Rcard["MDate"] = FormatHelper.ToDateString(item.MDate);
                    rowMo2Rcard["MTime"] = FormatHelper.ToTimeString(item.MTime);
                    rowMo2Rcard["LOTNO"] = item.LotNo;
                    rowMo2Rcard["ITEM"] = item.Item;
                    rowMo2Rcard["ITEMDESC"] = item.ItemDesc;
                    rowMo2Rcard["MOPLANQTY"] = item.MoPlanQty.ToString();
                    this.m_CheckList.Tables["MO2RcardLink"].Rows.Add(rowMo2Rcard);
                    this.txtMoCodeQuery.Value = item.MOCode;
                }
                this.m_CheckList.Tables["MO2RcardLink"].AcceptChanges();
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
            this.m_CheckList.Tables["MO2RcardLink"].Rows.Clear();
            this.m_CheckList.Tables["MO2RcardLink"].AcceptChanges();

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
        //调整逻辑： 去掉打印模板 Modify by LeoZhang 20120912
        #region 打印模板
        /*
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
         * */
        #endregion

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
                if (this.ucLabelComboxPrintList.ComboBoxData.Items.Count == 0)
                {
                    this.ShowMessage(new UserControl.Message(MessageType.Error, "$CS_PleaseInstallPrinter"));
                    return;
                }
                //ENd Added

                SetPrintButtonStatus(false);

                PrintTemplateFacade printTemplateFacade = new PrintTemplateFacade(this.DataProvider);

                string printer = this.ucLabelComboxPrintList.SelectedItemText;

                PrintTemplate printTemplate = (PrintTemplate)this.ucLabelComboxPrintTemplete.SelectedItemValue;
                printTemplate = (PrintTemplate)printTemplateFacade.GetPrintTemplate(printTemplate.TemplateName);

                List<MO2RCARDLINK> mo2RcardLinkList = new List<MO2RCARDLINK>();

                for (int i = 0; i < ultraGridMain.Rows.Count; i++)
                {
                    Infragistics.Win.UltraWinGrid.UltraGridRow row = ultraGridMain.Rows[i];

                    if (bool.Parse(row.Cells["Checked"].Value.ToString()))
                    {
                        object obj = _MOFacade.GetMO2RCardLink(row.Cells["RCard"].Value.ToString());
                        mo2RcardLinkList.Add(obj as MO2RCARDLINK);
                    }

                }

                if (mo2RcardLinkList.Count == 0)
                {
                    return;
                }

                if (!ValidateInput(printer, printTemplate))
                {
                    return;
                }


                for (int i = 0; i < int.Parse(this.ucLabelEditPrintCount.Value.Trim()); i++)
                {
                    Messages msg = this.Print(printer, printTemplate.TemplatePath, mo2RcardLinkList);

                    if (msg.IsSuccess())
                    {
                        //打印后的数据处理
                        try
                        {
                            string userCode = ApplicationService.Current().UserCode;
                            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);

                            this.DataProvider.BeginTransaction();

                            foreach (MO2RCARDLINK rcarlink in mo2RcardLinkList)
                            {
                                rcarlink.PrintTimes++;
                                rcarlink.LastPrintUSER = userCode;
                                rcarlink.LastPrintDate = dbDateTime.DBDate;
                                rcarlink.LastPrintTime = dbDateTime.DBTime;

                                _MOFacade.UpdateMO2RCardLink(rcarlink);
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

                //RCARD需要使用GetSourceCard方法转换成初始序列号
                string rcard = string.Empty;
                if (this.ucLabelRCardQuery.Value.Trim() != "")
                {
                    rcard = _DataCollectFacade.GetSourceCard(this.ucLabelRCardQuery.Value.Trim().ToUpper(), string.Empty);
                }

                LoadCheckList(rcard, this.txtMoCodeQuery.Value.Trim());

            }
            finally
            {
                SetPrintButtonStatus(true);
            }
        }

        //打印
        public UserControl.Messages Print(string printer, string templatePath, List<MO2RCARDLINK> mo2RcardLink)
        {
            UserControl.Messages messages = new UserControl.Messages();
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

                Hashtable ht = new Hashtable();

                for (int i = 0; i < mo2RcardLink.Count; i++)
                {
                    MO2RCARDLINK rcardLink = (MO2RCARDLINK)mo2RcardLink[i];

                    if (!ht.ContainsKey(rcardLink.MOCode))
                    {
                        MO mo = _MOFacade.GetMO(rcardLink.MOCode) as MO;
                        Item item = _ItemFacade.GetItem(mo.ItemCode, mo.OrganizationID) as Item;
                        ht.Add(rcardLink.MOCode, item);
                    }

                    LabelPrintVars labelPrintVars = new LabelPrintVars();

                    string[] vars = new string[0];

                    if (messages.IsSuccess())
                    {


                        try
                        {
                            //要传给Codesoft的数组，字段顺序不能修改
                            vars = _CodeSoftPrintFacade.GetPrintVars(rcardLink.RCard, (ht[rcardLink.MOCode] as Item).ItemCode, (ht[rcardLink.MOCode] as Item).ItemName, rcardLink.MOCode, "", "");

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

        //编辑区功能调整 Add by LeoZhang 20120911
        private void txtMoCodeEdit_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {

                //先检查打印类型是否存在
                if (this.ucLabelPrintType.SelectedItemValue == null || this.ucLabelPrintType.SelectedItemValue.ToString() == string.Empty)
                {
                    this.ShowMessage(new UserControl.Message(UserControl.MessageType.Error, "$Error_PrintType_Empty"));
                    this.txtMoCodeEdit.TextFocus(false, true);
                    return;
                }

                string currentMo = this.txtMoCodeEdit.Value.Trim();
                //检查工单是否存在，且状态不为关闭
                //调整为检查工单是否存在 ss
                object objMo = this._MOFacade.GetMO(currentMo);
                if (objMo == null)
                {
                    Clear();
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_MO_Not_Exist"));
                    this.txtMoCodeEdit.TextFocus(false, true);
                    return;
                }

                //工单是否已经产生过产品序列号
                if (_MOFacade != null)
                {
                    _MOFacade = new MOFacade(this.DataProvider);
                }
                object[] mo2rcard = _MOFacade.GetMO2RCardLinkByMoCode(currentMo);
                if (mo2rcard != null)
                {
                    Clear();
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$MO_Link_Rcard_AlreadyExists"));
                    this.txtMoCodeEdit.TextFocus(false, true);
                    return;
                }

                //检查工单状态:init和Release 才可以编辑
                if (_systemFacade == null)
                {
                    _systemFacade = new SystemSettingFacade(this.DataProvider);
                }
                Parameter paramterTravelling = (Parameter)_systemFacade.GetParameter("TRAVELLING TAG(600DPI)", "PRINTRELATE");
                //Parameter paramterLotSerial = (Parameter)_systemFacade.GetParameter("LOT SERIAL NUMBER(600DPI)", "PRINTRELATE");

                if (this.ucLabelPrintType.SelectedItemValue.ToString().Equals(paramterTravelling.ParameterCode)) 
               // ||                   this.ucLabelPrintType.SelectedItemValue.ToString().Equals(paramterLotSerial.ParameterCode)
                {
                    if (objMo != null)
                    {
                        if (((objMo as MO).MOStatus != MOManufactureStatus.MOSTATUS_RELEASE) && (objMo as MO).MOStatus != MOManufactureStatus.MOSTATUS_INITIAL)
                        {
                            Clear();
                            ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_ERROR_MO_Release"));
                            this.txtMoCodeEdit.TextFocus(false, true);
                            return;
                        }
                    }
                }

                string itemCode = string.Empty;
                if (_ItemFacade == null)
                {
                    _ItemFacade = new ItemFacade(this.DataProvider);
                }
                if (objMo != null)
                {
                    itemCode = ((objMo as MO)).ItemCode.ToString();
                }
                Item item = (Item)_ItemFacade.GetItem(itemCode, GlobalVariables.CurrentOrganizations.First().OrganizationID);
                //以上检查都通过时 带出编辑区的栏位数据

                if (item == null)
                {
                    Clear();
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_No_ItemInformation"));
                    this.txtMoCodeEdit.TextFocus(false, true);
                    return;
                }


                if (objMo != null)
                {
                    //序列号前缀
                    //if (item.ItemAbbreViation == string.Empty)
                    //{
                    //    this.ucLabelEditRCardPrefix.Value = "DGH" + ((objMo as MO)).LotNo.Trim();
                    //}
                    //else
                    //{
                    //    this.ucLabelEditRCardPrefix.Value = item.ItemAbbreViation.Trim() + ((objMo as MO)).LotNo.Trim();
                    //}
                    this.ucLabelEditRCardPrefix.Value = string.Empty;
                    //起始序列号 和 结束序列号 进制的区别

                    NumberScale scale = NumberScale.Scale34;
                    if (ultraOptionSetScale.CheckedIndex == 0)
                        scale = NumberScale.Scale10;
                    else if (ultraOptionSetScale.CheckedIndex == 1)
                        scale = NumberScale.Scale16;
                    else if (ultraOptionSetScale.CheckedIndex == 2)
                        scale = NumberScale.Scale34;

                    //rcard的前缀有最大值取其流水号+1，开始 如果没有从‘0001’ 开始
                    object maxRcard = _MOFacade.GetMaxRcardSameLotNo(FormatHelper.CleanString(this.ucLabelEditRCardPrefix.Value));
                    if (maxRcard == null || (maxRcard as MO2RCARDLINK).RCard == string.Empty)
                    {
                        //如果序列号的前9位在表中已经存在
                        this.ucLERCStartRcard.Value = NumberScaleHelper.ChangeNumber("0001", NumberScale.Scale10, scale).PadLeft(4, '0');
                    }
                    else
                    {
                        try
                        {

                            string serialString = (maxRcard as MO2RCARDLINK).RCard.Substring(9, 4);//前缀一定是9位：3位简称+6为LotNo
                            serialString = NumberScaleHelper.ChangeNumber(serialString, scale, NumberScale.Scale10);
                            int serialNo = Convert.ToInt32(serialString) + 1;//从最大Rcard四位流水号+1开始

                            this.ucLERCStartRcard.Value = NumberScaleHelper.ChangeNumber(serialNo.ToString(), NumberScale.Scale10, scale).PadLeft(4, '0');
                        }
                        catch (Exception)
                        {
                            this.ShowMessage(new UserControl.Message(UserControl.MessageType.Error, "$Error_Has_IllegalRcard"));
                            this.txtMoCodeEdit.TextFocus(false, true);
                            return;
                        }
                    }

                    string endRcardNumber = NumberScaleHelper.ChangeNumber(this.ucLERCStartRcard.Value, scale, NumberScale.Scale10);
                    string endRcardValue = ((int)(((objMo as MO).MOPlanQty - 1) + Convert.ToInt32(endRcardNumber))).ToString();
                    endRcardValue = NumberScaleHelper.ChangeNumber(endRcardValue, NumberScale.Scale10, scale);
                    this.ucLERCEndRcard.Value = endRcardValue.PadLeft(4, '0');


                    //序列号长度
                    //规则：产品简称【tblitem. ITEMAbbreviation】+tblmo.LOTNO+四位流水码【0001开始】+四位编号。
                    //四位编码：P【代码关单，写死】+第几周【根据当前日期到TBLTimeDimension获取】+年的最后一位【例如2012，值为2】
                    string rcard = this.ucLabelEditRCardPrefix.Value + this.ucLERCStartRcard.Value;
                    this.ucLabelRCardLength.Value = (rcard.Length + 4).ToString();


                    if (_reprotFacade == null)
                    {
                        this._reprotFacade = new ReportFacade(this.DataProvider);
                    }

                    Domain.Report.TimeDimension timeDimension = (Domain.Report.TimeDimension)_reprotFacade.GetTimeDimension(FormatHelper.TODateInt(DateTime.Now));
                    if (timeDimension == null)
                    {
                        throw new Exception("Time Dimension Not Exist");
                    }

                    ucLabelEditlast.Value = "P" + timeDimension.Week.ToString().PadLeft(2, '0') + timeDimension.Year.ToString().Substring(timeDimension.Year.ToString().Length - 1);
                   
                    //生产数量
                    this.ucLabelCreateQTY.Value = ((int)(objMo as MO).MOPlanQty).ToString();

                    SetTextBoxEnable();
                }

                #region 检查工单状态是否为“关闭”
                /*
                if (objMo != null)
                {
                    if ((objMo as MO).MOStatus == MOStatus.Close)
                    {
                        Clear();
                        ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_ERROR_MO_COLSE"));
                        return;
                    }
                    else
                    {

                        currentItemCode = (objMo as MO).ItemCode;
                        Init_ComboxCheckType(currentItemCode);
                    }
                }
                else
                {
                    Clear();
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_MO_Not_Exist"));
                    return;
                } **/
                #endregion
            }
            else  //不是回车 就清空下面的数据
            {
                this.ucLabelCreateQTY.Value = string.Empty;
                this.ucLabelRCardLength.Value = string.Empty;
                this.ucLERCEndRcard.Value = string.Empty;
                this.ucLERCStartRcard.Value = string.Empty;
                this.ucLabelEditRCardPrefix.Value = string.Empty;
            }
        }

        private void Init_ComboxCheckType(string itemCode)
        {
            this.ucLabelComboxCheckType.Clear();

            object[] objs = _ItemFacade.QueryItem2SNCheck(itemCode);
            if (objs != null && objs.Length > 0)
            {
                foreach (Item2SNCheck item in objs)
                {
                    this.ucLabelComboxCheckType.AddItem(item.Type, item.Type);
                }
            }
        }

        private void Clear()
        {
            currentItemCode = string.Empty;
            this.ucLabelComboxCheckType.Clear();

        }

        private void SetControlIsEnable(bool isEnable)
        {
            this.ucLabelEditRCardPrefix.Enabled = isEnable;
            this.ucLabelRCardLength.Checked = (!isEnable);
            this.ucLabelRCardLength.Enabled = isEnable;

            this.checkBoxSNContentCheck.Enabled = isEnable;
        }

        private void ucLabelComboxCheckType_SelectedIndexChanged(object sender, EventArgs e)
        {
            object obj = _ItemFacade.GetItem2SNCheck(currentItemCode, this.ucLabelComboxCheckType.SelectedItemValue.ToString());

            if (obj != null)
            {
                Item2SNCheck item2SNCheck = obj as Item2SNCheck;
                if (item2SNCheck.SNContentCheck == "Y")
                {
                    this.checkBoxSNContentCheck.Checked = true;
                }
                else
                {
                    this.checkBoxSNContentCheck.Checked = false;
                }

                this.ucLabelRCardLength.Value = item2SNCheck.SNLength.ToString();
                this.ucLabelEditRCardPrefix.Value = item2SNCheck.SNPrefix.ToString();
                SetControlIsEnable(false);

            }
            else
            {

            }

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

            }

            return flag;
        }

        private void checkBoxNoPrint_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBoxNoPrint.Checked)
            {
                for (int i = 0; i < this.ultraGridMain.Rows.Count; i++)
                {
                    Infragistics.Win.UltraWinGrid.UltraGridRow row = ultraGridMain.Rows[i];
                    if (row.Cells["PrintTimes"].Value.ToString() == "0")
                    {
                        row.Cells["Checked"].Value = true;
                    }
                    else
                    {
                        row.Cells["Checked"].Value = false;
                    }
                }
            }
            else
            {
                for (int i = 0; i < this.ultraGridMain.Rows.Count; i++)
                {
                    Infragistics.Win.UltraWinGrid.UltraGridRow row = ultraGridMain.Rows[i];


                    row.Cells["Checked"].Value = false;

                }
            }

            this.ultraGridMain.UpdateData();
        }

        //Modify Rcard 生成规则 By LeoZhang 20120913
        //规则：产品简称【tblitem. ITEMAbbreviation】+tblmo.LOTNO+四位流水码【0001开始】+四位编号。
        //四位编码：P【代码关单，写死】+第几周【根据当前日期到TBLTimeDimension获取】+年的最后一位【例如2012，值为2】
        private List<string> CalcRCardEnd(long count)
        {

            List<string> RCardList = new List<string>();


            NumberScale scale = NumberScale.Scale34;
            if (ultraOptionSetScale.CheckedIndex == 0)
                scale = NumberScale.Scale10;
            else if (ultraOptionSetScale.CheckedIndex == 1)
                scale = NumberScale.Scale16;
            else if (ultraOptionSetScale.CheckedIndex == 2)
                scale = NumberScale.Scale34;

            long startSN = 0;
            string startRcard = string.Empty;

            object maxRcard = _MOFacade.GetMaxRcardSameLotNo(FormatHelper.CleanString(this.ucLabelEditRCardPrefix.Value));
            if (maxRcard == null || (maxRcard as MO2RCARDLINK).RCard == string.Empty)
            {
                startRcard = NumberScaleHelper.ChangeNumber("0001", NumberScale.Scale10, scale).PadLeft(4, '0');
            }
            else
            {
                try
                {
                    string serialString = (maxRcard as MO2RCARDLINK).RCard.Substring(9, 4);//前缀一定是9位：3位简称+6为LotNo
                    serialString = NumberScaleHelper.ChangeNumber(serialString, scale, NumberScale.Scale10);
                    int serialNo = Convert.ToInt32(serialString) + 1;//从最大Rcard四位流水号+1开始

                    startRcard = NumberScaleHelper.ChangeNumber(serialNo.ToString(), NumberScale.Scale10, scale).PadLeft(4, '0');
                }
                catch (Exception)
                {
                    this.ShowMessage(new UserControl.Message(UserControl.MessageType.Error, "$Error_Has_IllegalRcard"));
                    return null;
                }
            }


            try
            {
                startSN = long.Parse(NumberScaleHelper.ChangeNumber(startRcard, scale, NumberScale.Scale10));
                //startSN = long.Parse(this.ucLERCStartRcard.Value.Trim());

            }
            catch (Exception ex)
            {
                this.ShowMessage(ex);

            }

            long endSN = startSN + count - 1;

            try
            {
                //if (_reprotFacade == null)
                //{
                //    this._reprotFacade = new ReportFacade(this.DataProvider);
                //}
                //Domain.Report.TimeDimension timeDimension = (Domain.Report.TimeDimension)_reprotFacade.GetTimeDimension(FormatHelper.TODateInt(DateTime.Now));
                //if (timeDimension == null)
                //{
                //    throw new Exception("Time Dimension Not Exist");
                //}

                for (long i = startSN; i <= endSN; i++)
                {
                    string RCardEnd = NumberScaleHelper.ChangeNumber(i.ToString(), NumberScale.Scale10, scale);

                    RCardEnd = RCardEnd.PadLeft(ucLERCStartRcard.Value.Trim().Length, '0');
                    //string year = DateTime.Now.Year.ToString();
                    string rcard = this.ucLabelEditRCardPrefix.Value + RCardEnd + ucLabelEditlast.Value;
                    RCardList.Add(rcard);
                }
            }
            catch (Exception ex)
            {
                this.ShowMessage(ex);

            }

            return RCardList;
        }

        //初始化 “打印类型” add by LeoZhang 20120912
        public void ucLabelPrintType_Load()
        {
            this.ucLabelPrintType.Clear();
            SystemSettingFacade systemFacade = new SystemSettingFacade(this.DataProvider);
            object[] objs = systemFacade.GetParamtersByGroupAndparentParamter("PRINTRELATE", "OFFLINEPRINTLABLETYPE");

            if (objs == null)
            {
                this.ShowMessage(new UserControl.Message(UserControl.MessageType.Error, "$Error_PrintType_Empty"));
                this.txtMoCodeEdit.TextFocus(false, false);
                return;
            }

            if (objs != null)
            {
                foreach (Domain.BaseSetting.Parameter param in objs)
                {
                    this.ucLabelPrintType.AddItem(param.ParameterAlias, param.ParameterCode);
                }
            }
            this.ucLabelPrintType.SelectedIndex = 0;
        }

        //设置控件为不可用
        public void SetTextBoxEnable()
        {
            this.ucLabelCreateQTY.Enabled = false;
            this.ucLabelRCardLength.Enabled = false;
            this.ucLERCEndRcard.Enabled = false;
            this.ucLERCStartRcard.Enabled = false;
            this.ucLabelEditRCardPrefix.Enabled = false;
        }

        //当前Rcard打印前转化规则
        public string FormatRcard(string printRcard, object objMO)
        {
            string RcardNew = string.Empty;

            if (printRcard.Substring(printRcard.Length - 3).Equals("HFP"))
            {
                if (printRcard.Substring(printRcard.Length - 4).Equals("EHFP"))
                {
                    RcardNew = " " + "EHFP";
                    return RcardNew;
                }

                RcardNew = " " + "HFP";
                return RcardNew;
            }


            if (printRcard.Substring(printRcard.Length - 5).Equals("HFP +") ||
                printRcard.Substring(printRcard.Length - 6).Equals("HFP ++"))
            {
                RcardNew = " " + "HFP";
                return RcardNew;
            }
            if (printRcard.Substring(printRcard.Length - 2).Equals("LF") || printRcard.Substring(printRcard.Length - 2).Equals("RC"))
            {
                if (printRcard.Substring(printRcard.Length - 2).Equals("LF"))
                {
                    RcardNew = " " + "LF";
                    return RcardNew;
                }

                if (printRcard.Substring(printRcard.Length - 2).Equals("RC"))
                {
                    RcardNew = " " + "RC";
                    return RcardNew;
                }
            }
            return " ";
        }

        //输入密码的弹出窗口
        public Boolean PasswordPopupWindow(FDialogInput finput)
        {
            if (_systemFacade == null)
            {
                _systemFacade = new SystemSettingFacade(this.DataProvider);
            }
            Parameter pwd = (Parameter)_systemFacade.GetParameter("SNPRINTPASSWORD", "PRINTRELATE");
            if (pwd == null)
            {
                this.ShowMessage(new UserControl.Message(UserControl.MessageType.Error, "$Error_Psssword_Not_Exit!"));
                return false;
            }
            DialogResult dialogResult = finput.ShowDialog();
            string password = string.Empty;
            password = finput.InputText.Trim().ToUpper();
            if (dialogResult != DialogResult.OK)
            {
                finput.Close();
                return false;
            }
            if (!password.Equals(pwd.ParameterAlias.ToUpper()))
            {
                if (dialogResult != DialogResult.OK)
                {
                    finput.Close();
                }
                else
                {
                    this.ShowMessage(new UserControl.Message(UserControl.MessageType.Error, "$Error_Password_Not_Match!"));
                }
                return false;

            }

            IsNeedPassword = false;
            return true;
        }

        //打印完成后的数据处理
        public void AfterPrint()
        {
            if (_MOFacade == null)
            {
                _MOFacade = new MOFacade(this.DataProvider);
            }
            MO2RCARDLINK rcarlink = new MO2RCARDLINK();
            List<MO2RCARDLINK> mo2RcardLinkList = new List<MO2RCARDLINK>();  //存放要更新的Rcard
            string mocode = string.Empty;

            //更新Rcard的数据
            for (int i = 0; i < ultraGridMain.Rows.Count; i++)
            {
                Infragistics.Win.UltraWinGrid.UltraGridRow row = ultraGridMain.Rows[i];

                if (bool.Parse(row.Cells["Checked"].Value.ToString()))
                {
                    object obj = _MOFacade.GetMO2RCardLink(row.Cells["RCard"].Value.ToString());
                    rcarlink = (MO2RCARDLINK)obj;
                    //mo2RcardLinkList.Add(obj as MO2RCARDLINK);
                    //打印后的数据处理

                    string userCode = ApplicationService.Current().UserCode;
                    DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
                    rcarlink.PrintTimes++;
                    rcarlink.LastPrintUSER = userCode;
                    rcarlink.LastPrintDate = dbDateTime.DBDate;
                    rcarlink.LastPrintTime = dbDateTime.DBTime;
                    mocode = rcarlink.MOCode;
                    mo2RcardLinkList.Add(rcarlink);
                }
                Application.DoEvents();
            }

            //更新到数据库
            if (mo2RcardLinkList.Count != 0)
            {
                try
                {
                    this.DataProvider.BeginTransaction();
                    foreach (MO2RCARDLINK mo2rcardLink in mo2RcardLinkList)
                    {
                        _MOFacade.UpdateMO2RCardLink(mo2rcardLink);
                    }
                    this.DataProvider.CommitTransaction();
                    this.ShowMessage(new UserControl.Message(UserControl.MessageType.Success, "$Success_Print_Label"));
                }
                catch (System.Exception ex)
                {
                    this.DataProvider.RollbackTransaction();
                    this.ShowMessage(new UserControl.Message(UserControl.MessageType.Error, ex.Message));
                    return;
                }
            }
            //更新 grid里面的数据

            string rcard = string.Empty;
            if (this.ucLabelRCardQuery.Value.Trim() != "")
            {
                rcard = _DataCollectFacade.GetSourceCard(this.ucLabelRCardQuery.Value.Trim().ToUpper(), string.Empty);
            }

            LoadCheckList(rcard, mocode);

        }

        //打印模板选择改变后 清空相应的数据
        private void ucLabelPrintType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ucLabelCreateQTY.Value = string.Empty;
            this.ucLabelRCardLength.Value = string.Empty;
            this.ucLERCEndRcard.Value = string.Empty;
            this.ucLERCStartRcard.Value = string.Empty;
            this.ucLabelEditRCardPrefix.Value = string.Empty;
        }

        //转化特殊的ItemDesc，若不满足转化条件则原值返回
        public string FormatItemDesc(string itemDesc, string itemCode)
        {
            if (_systemFacade == null)
            {
                _systemFacade = new SystemSettingFacade(this.DataProvider);
            }

            object[] objs = _systemFacade.GetParametersByParameterGroup("SPECIALPRINTITEM");

            if (objs != null)
            {
                foreach (Parameter para in objs)
                {
                    if (itemCode.ToUpper().Equals(para.ParameterCode))
                    {
                        return para.ParameterDescription;
                    }
                }
            }
            return itemDesc;
        }

    }
}

