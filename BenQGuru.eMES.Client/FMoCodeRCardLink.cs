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
using System.Text.RegularExpressions;

namespace BenQGuru.eMES.Client
{
    public class FMoCodeRCardLink : BaseForm
    {
        private bool _IsBatchPrint = true;
        private string _DataDescFileName = "Label.dsc";
        public PrintTemplate[] _PrintTemplateList = null;
        private DataSet m_CheckList = null;
        private DataTable m_mo2RcardLink = null;
        private string currentItemCode = string.Empty;
        public static decimal lotCount = 0;

        Hashtable lotno = new Hashtable();

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
        private Label lblMaxNumber;
        private Label lblMaxRcardSeq;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FMoCodeRCardLink));
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.ValueListItem valueListItem1 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.ValueListItem valueListItem2 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.ValueListItem valueListItem3 = new Infragistics.Win.ValueListItem();
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblMaxNumber = new System.Windows.Forms.Label();
            this.lblMaxRcardSeq = new System.Windows.Forms.Label();
            this.ultraOptionSetScale = new Infragistics.Win.UltraWinEditors.UltraOptionSet();
            this.ucButtonExit = new UserControl.UCButton();
            this.ucButtonCalcRCardEnd = new UserControl.UCButton();
            this.txtMoCodeEdit = new UserControl.UCLabelEdit();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.ucLERCEndRcard = new UserControl.UCLabelEdit();
            this.checkBoxSNContentCheck = new System.Windows.Forms.CheckBox();
            this.ucLabelComboxCheckType = new UserControl.UCLabelCombox();
            this.ucBtnDelete = new UserControl.UCButton();
            this.ucLabelRCardLength = new UserControl.UCLabelEdit();
            this.ucLabelCreateQTY = new UserControl.UCLabelEdit();
            this.ucLabelEditRCardPrefix = new UserControl.UCLabelEdit();
            this.ucLERCStartRcard = new UserControl.UCLabelEdit();
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
            this.grpQuery.Size = new System.Drawing.Size(831, 39);
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
            this.ucLabelRCardQuery.Location = new System.Drawing.Point(342, 13);
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
            this.ucLabelRCardQuery.XAlign = 415;
            // 
            // ucButtonQuery
            // 
            this.ucButtonQuery.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonQuery.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonQuery.BackgroundImage")));
            this.ucButtonQuery.ButtonType = UserControl.ButtonTypes.Query;
            this.ucButtonQuery.Caption = "查询";
            this.ucButtonQuery.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonQuery.Location = new System.Drawing.Point(689, 13);
            this.ucButtonQuery.Name = "ucButtonQuery";
            this.ucButtonQuery.Size = new System.Drawing.Size(88, 22);
            this.ucButtonQuery.TabIndex = 57;
            this.ucButtonQuery.Click += new System.EventHandler(this.ucButtonQuery_Click);
            // 
            // panelButton
            // 
            this.panelButton.BackColor = System.Drawing.SystemColors.Control;
            this.panelButton.Controls.Add(this.ucBtnExit);
            this.panelButton.Controls.Add(this.ucButtonPrint);
            this.panelButton.Controls.Add(this.ucLabelComboxPrintTemplete);
            this.panelButton.Controls.Add(this.ucLabelEditPrintCount);
            this.panelButton.Controls.Add(this.ucLabelComboxPrintList);
            this.panelButton.Controls.Add(this.checkBoxNoPrint);
            this.panelButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButton.Location = new System.Drawing.Point(0, 612);
            this.panelButton.Name = "panelButton";
            this.panelButton.Size = new System.Drawing.Size(831, 80);
            this.panelButton.TabIndex = 4;
            // 
            // ucBtnExit
            // 
            this.ucBtnExit.BackColor = System.Drawing.SystemColors.Control;
            this.ucBtnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucBtnExit.BackgroundImage")));
            this.ucBtnExit.ButtonType = UserControl.ButtonTypes.Exit;
            this.ucBtnExit.Caption = "退出";
            this.ucBtnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnExit.Location = new System.Drawing.Point(438, 53);
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
            this.ucButtonPrint.Location = new System.Drawing.Point(304, 53);
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
            this.ucLabelComboxPrintTemplete.Location = new System.Drawing.Point(256, 27);
            this.ucLabelComboxPrintTemplete.Name = "ucLabelComboxPrintTemplete";
            this.ucLabelComboxPrintTemplete.SelectedIndex = -1;
            this.ucLabelComboxPrintTemplete.ShowCheckBox = false;
            this.ucLabelComboxPrintTemplete.Size = new System.Drawing.Size(194, 20);
            this.ucLabelComboxPrintTemplete.TabIndex = 49;
            this.ucLabelComboxPrintTemplete.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabelComboxPrintTemplete.XAlign = 317;
            // 
            // ucLabelEditPrintCount
            // 
            this.ucLabelEditPrintCount.AllowEditOnlyChecked = true;
            this.ucLabelEditPrintCount.AutoSelectAll = false;
            this.ucLabelEditPrintCount.AutoUpper = true;
            this.ucLabelEditPrintCount.Caption = "打印数量";
            this.ucLabelEditPrintCount.Checked = false;
            this.ucLabelEditPrintCount.EditType = UserControl.EditTypes.Integer;
            this.ucLabelEditPrintCount.Location = new System.Drawing.Point(491, 27);
            this.ucLabelEditPrintCount.MaxLength = 8;
            this.ucLabelEditPrintCount.Multiline = false;
            this.ucLabelEditPrintCount.Name = "ucLabelEditPrintCount";
            this.ucLabelEditPrintCount.PasswordChar = '\0';
            this.ucLabelEditPrintCount.ReadOnly = false;
            this.ucLabelEditPrintCount.ShowCheckBox = false;
            this.ucLabelEditPrintCount.Size = new System.Drawing.Size(194, 24);
            this.ucLabelEditPrintCount.TabIndex = 48;
            this.ucLabelEditPrintCount.TabNext = false;
            this.ucLabelEditPrintCount.Tag = "";
            this.ucLabelEditPrintCount.Value = "1";
            this.ucLabelEditPrintCount.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabelEditPrintCount.XAlign = 552;
            // 
            // ucLabelComboxPrintList
            // 
            this.ucLabelComboxPrintList.AllowEditOnlyChecked = true;
            this.ucLabelComboxPrintList.Caption = "打印机列表";
            this.ucLabelComboxPrintList.Checked = false;
            this.ucLabelComboxPrintList.Location = new System.Drawing.Point(33, 27);
            this.ucLabelComboxPrintList.Name = "ucLabelComboxPrintList";
            this.ucLabelComboxPrintList.SelectedIndex = -1;
            this.ucLabelComboxPrintList.ShowCheckBox = false;
            this.ucLabelComboxPrintList.Size = new System.Drawing.Size(206, 20);
            this.ucLabelComboxPrintList.TabIndex = 50;
            this.ucLabelComboxPrintList.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabelComboxPrintList.XAlign = 106;
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
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox1.Controls.Add(this.lblMaxNumber);
            this.groupBox1.Controls.Add(this.lblMaxRcardSeq);
            this.groupBox1.Controls.Add(this.ultraOptionSetScale);
            this.groupBox1.Controls.Add(this.ucButtonExit);
            this.groupBox1.Controls.Add(this.ucButtonCalcRCardEnd);
            this.groupBox1.Controls.Add(this.txtMoCodeEdit);
            this.groupBox1.Controls.Add(this.radioButton4);
            this.groupBox1.Controls.Add(this.ucLERCEndRcard);
            this.groupBox1.Controls.Add(this.checkBoxSNContentCheck);
            this.groupBox1.Controls.Add(this.ucLabelComboxCheckType);
            this.groupBox1.Controls.Add(this.ucBtnDelete);
            this.groupBox1.Controls.Add(this.ucLabelRCardLength);
            this.groupBox1.Controls.Add(this.ucLabelCreateQTY);
            this.groupBox1.Controls.Add(this.ucLabelEditRCardPrefix);
            this.groupBox1.Controls.Add(this.ucLERCStartRcard);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 473);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(831, 139);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // lblMaxNumber
            // 
            this.lblMaxNumber.AutoSize = true;
            this.lblMaxNumber.Location = new System.Drawing.Point(763, 50);
            this.lblMaxNumber.Name = "lblMaxNumber";
            this.lblMaxNumber.Size = new System.Drawing.Size(0, 12);
            this.lblMaxNumber.TabIndex = 69;
            this.lblMaxNumber.Visible = false;
            // 
            // lblMaxRcardSeq
            // 
            this.lblMaxRcardSeq.AutoSize = true;
            this.lblMaxRcardSeq.Location = new System.Drawing.Point(688, 50);
            this.lblMaxRcardSeq.Name = "lblMaxRcardSeq";
            this.lblMaxRcardSeq.Size = new System.Drawing.Size(83, 12);
            this.lblMaxRcardSeq.TabIndex = 68;
            this.lblMaxRcardSeq.Text = "最大的序列号:";
            this.lblMaxRcardSeq.Visible = false;
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
            this.ultraOptionSetScale.Location = new System.Drawing.Point(472, 82);
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
            this.ucButtonExit.Location = new System.Drawing.Point(438, 109);
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
            this.ucButtonCalcRCardEnd.Location = new System.Drawing.Point(224, 109);
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
            this.txtMoCodeEdit.Location = new System.Drawing.Point(39, 22);
            this.txtMoCodeEdit.MaxLength = 40;
            this.txtMoCodeEdit.Multiline = false;
            this.txtMoCodeEdit.Name = "txtMoCodeEdit";
            this.txtMoCodeEdit.PasswordChar = '\0';
            this.txtMoCodeEdit.ReadOnly = false;
            this.txtMoCodeEdit.ShowCheckBox = false;
            this.txtMoCodeEdit.Size = new System.Drawing.Size(194, 24);
            this.txtMoCodeEdit.TabIndex = 59;
            this.txtMoCodeEdit.TabNext = false;
            this.txtMoCodeEdit.Value = "";
            this.txtMoCodeEdit.WidthType = UserControl.WidthTypes.Normal;
            this.txtMoCodeEdit.XAlign = 100;
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
            this.ucLERCEndRcard.Location = new System.Drawing.Point(472, 50);
            this.ucLERCEndRcard.MaxLength = 8;
            this.ucLERCEndRcard.Multiline = false;
            this.ucLERCEndRcard.Name = "ucLERCEndRcard";
            this.ucLERCEndRcard.PasswordChar = '\0';
            this.ucLERCEndRcard.ReadOnly = false;
            this.ucLERCEndRcard.ShowCheckBox = false;
            this.ucLERCEndRcard.Size = new System.Drawing.Size(206, 24);
            this.ucLERCEndRcard.TabIndex = 62;
            this.ucLERCEndRcard.TabNext = true;
            this.ucLERCEndRcard.Value = "";
            this.ucLERCEndRcard.WidthType = UserControl.WidthTypes.Normal;
            this.ucLERCEndRcard.XAlign = 545;
            // 
            // checkBoxSNContentCheck
            // 
            this.checkBoxSNContentCheck.AutoSize = true;
            this.checkBoxSNContentCheck.Location = new System.Drawing.Point(472, 25);
            this.checkBoxSNContentCheck.Name = "checkBoxSNContentCheck";
            this.checkBoxSNContentCheck.Size = new System.Drawing.Size(210, 16);
            this.checkBoxSNContentCheck.TabIndex = 61;
            this.checkBoxSNContentCheck.Text = "限制序列号内容为字符,数字和空格";
            this.checkBoxSNContentCheck.UseVisualStyleBackColor = true;
            // 
            // ucLabelComboxCheckType
            // 
            this.ucLabelComboxCheckType.AllowEditOnlyChecked = true;
            this.ucLabelComboxCheckType.Caption = "检查类型";
            this.ucLabelComboxCheckType.Checked = false;
            this.ucLabelComboxCheckType.Location = new System.Drawing.Point(256, 22);
            this.ucLabelComboxCheckType.Name = "ucLabelComboxCheckType";
            this.ucLabelComboxCheckType.SelectedIndex = -1;
            this.ucLabelComboxCheckType.ShowCheckBox = false;
            this.ucLabelComboxCheckType.Size = new System.Drawing.Size(194, 24);
            this.ucLabelComboxCheckType.TabIndex = 60;
            this.ucLabelComboxCheckType.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabelComboxCheckType.XAlign = 317;
            this.ucLabelComboxCheckType.SelectedIndexChanged += new System.EventHandler(this.ucLabelComboxCheckType_SelectedIndexChanged);
            // 
            // ucBtnDelete
            // 
            this.ucBtnDelete.BackColor = System.Drawing.SystemColors.Control;
            this.ucBtnDelete.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucBtnDelete.BackgroundImage")));
            this.ucBtnDelete.ButtonType = UserControl.ButtonTypes.Delete;
            this.ucBtnDelete.Caption = "删除";
            this.ucBtnDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnDelete.Location = new System.Drawing.Point(331, 109);
            this.ucBtnDelete.Name = "ucBtnDelete";
            this.ucBtnDelete.Size = new System.Drawing.Size(88, 22);
            this.ucBtnDelete.TabIndex = 22;
            this.ucBtnDelete.Click += new System.EventHandler(this.ucBtnDelete_Click);
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
            this.ucLabelRCardLength.Location = new System.Drawing.Point(11, 78);
            this.ucLabelRCardLength.MaxLength = 4;
            this.ucLabelRCardLength.Multiline = false;
            this.ucLabelRCardLength.Name = "ucLabelRCardLength";
            this.ucLabelRCardLength.PasswordChar = '\0';
            this.ucLabelRCardLength.ReadOnly = false;
            this.ucLabelRCardLength.ShowCheckBox = true;
            this.ucLabelRCardLength.Size = new System.Drawing.Size(222, 24);
            this.ucLabelRCardLength.TabIndex = 2;
            this.ucLabelRCardLength.TabNext = true;
            this.ucLabelRCardLength.Value = "";
            this.ucLabelRCardLength.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabelRCardLength.XAlign = 100;
            // 
            // ucLabelCreateQTY
            // 
            this.ucLabelCreateQTY.AllowEditOnlyChecked = true;
            this.ucLabelCreateQTY.AutoSelectAll = false;
            this.ucLabelCreateQTY.AutoUpper = true;
            this.ucLabelCreateQTY.Caption = "生成数量";
            this.ucLabelCreateQTY.Checked = false;
            this.ucLabelCreateQTY.EditType = UserControl.EditTypes.Integer;
            this.ucLabelCreateQTY.Location = new System.Drawing.Point(256, 78);
            this.ucLabelCreateQTY.MaxLength = 8;
            this.ucLabelCreateQTY.Multiline = false;
            this.ucLabelCreateQTY.Name = "ucLabelCreateQTY";
            this.ucLabelCreateQTY.PasswordChar = '\0';
            this.ucLabelCreateQTY.ReadOnly = false;
            this.ucLabelCreateQTY.ShowCheckBox = false;
            this.ucLabelCreateQTY.Size = new System.Drawing.Size(194, 24);
            this.ucLabelCreateQTY.TabIndex = 8;
            this.ucLabelCreateQTY.TabNext = true;
            this.ucLabelCreateQTY.Value = "";
            this.ucLabelCreateQTY.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabelCreateQTY.XAlign = 317;
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
            this.ucLabelEditRCardPrefix.Location = new System.Drawing.Point(27, 50);
            this.ucLabelEditRCardPrefix.MaxLength = 40;
            this.ucLabelEditRCardPrefix.Multiline = false;
            this.ucLabelEditRCardPrefix.Name = "ucLabelEditRCardPrefix";
            this.ucLabelEditRCardPrefix.PasswordChar = '\0';
            this.ucLabelEditRCardPrefix.ReadOnly = false;
            this.ucLabelEditRCardPrefix.ShowCheckBox = false;
            this.ucLabelEditRCardPrefix.Size = new System.Drawing.Size(206, 24);
            this.ucLabelEditRCardPrefix.TabIndex = 2;
            this.ucLabelEditRCardPrefix.TabNext = true;
            this.ucLabelEditRCardPrefix.Value = "";
            this.ucLabelEditRCardPrefix.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabelEditRCardPrefix.XAlign = 100;
            // 
            // ucLERCStartRcard
            // 
            this.ucLERCStartRcard.AllowEditOnlyChecked = true;
            this.ucLERCStartRcard.AutoSelectAll = false;
            this.ucLERCStartRcard.AutoUpper = true;
            this.ucLERCStartRcard.Caption = "起始序列号";
            this.ucLERCStartRcard.Checked = false;
            this.ucLERCStartRcard.EditType = UserControl.EditTypes.Integer;
            this.ucLERCStartRcard.Location = new System.Drawing.Point(244, 50);
            this.ucLERCStartRcard.MaxLength = 8;
            this.ucLERCStartRcard.Multiline = false;
            this.ucLERCStartRcard.Name = "ucLERCStartRcard";
            this.ucLERCStartRcard.PasswordChar = '\0';
            this.ucLERCStartRcard.ReadOnly = false;
            this.ucLERCStartRcard.ShowCheckBox = false;
            this.ucLERCStartRcard.Size = new System.Drawing.Size(206, 24);
            this.ucLERCStartRcard.TabIndex = 8;
            this.ucLERCStartRcard.TabNext = true;
            this.ucLERCStartRcard.Value = "";
            this.ucLERCStartRcard.WidthType = UserControl.WidthTypes.Normal;
            this.ucLERCStartRcard.XAlign = 317;
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
            this.ultraGridMain.Size = new System.Drawing.Size(831, 434);
            this.ultraGridMain.TabIndex = 5;
            this.ultraGridMain.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.ultraGridMain_InitializeLayout);
            this.ultraGridMain.CellChange += new Infragistics.Win.UltraWinGrid.CellEventHandler(this.ultraGridMain_CellChange);
            // 
            // FMoCodeRCardLink
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(831, 692);
            this.Controls.Add(this.ultraGridMain);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panelButton);
            this.Controls.Add(this.grpQuery);
            this.Name = "FMoCodeRCardLink";
            this.Text = "工单关联产品序号";
            this.Load += new System.EventHandler(this.FMoCodeRCardLink_Load);
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

        public FMoCodeRCardLink()
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
        private void FMoCodeRCardLink_Load(object sender, System.EventArgs e)
        {


            this.ucLabelEditPrintCount.InnerTextBox.ForeColor = Color.Black;
            this.ucLabelEditPrintCount.InnerTextBox.TextAlign = HorizontalAlignment.Right;
            InitializeMainGrid();
            this.ultraGridMain.DisplayLayout.Bands[0].Columns["Checked"].Header.CheckBoxVisibility = Infragistics.Win.UltraWinGrid.HeaderCheckBoxVisibility.WhenUsingCheckEditor;
            LoadPrinter();
            LoadTemplateList();

            //this.InitGridLanguage(ultraGridMain);
            //this.InitPageLanguage();

        }
        #endregion


        #region Button Events

        //查询
        private void ucButtonQuery_Click(object sender, EventArgs e)
        {
            //RCARD需要使用GetSourceCard方法转换成初始序列号
            string rcard = string.Empty;
            if (this.ucLabelRCardQuery.Value.Trim() != "")
            {
                rcard = _DataCollectFacade.GetSourceCard(this.ucLabelRCardQuery.Value.Trim().ToUpper(), string.Empty);
            }

            LoadCheckList(rcard, this.txtMoCodeQuery.Value.Trim());
        }

        //删除
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

        //计算生成序列号
        private void ucButtonCalcRCardEnd_Click(object sender, EventArgs e)
        {

            //检查是否输入工单，工单是否存在TBLMO中。
            if (this.txtMoCodeEdit.Value.Trim() == "")
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_STARTRCARD_LESS_THAN_ZEOR"));
                return;
            }
            else
            {

                if (this.ucLabelComboxCheckType.SelectedIndex == -1)
                {
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_ERROR_ITEM_CHECK_TYPE"));
                    return;
                }

                string currentMo = this.txtMoCodeEdit.Value.Trim();
                object objMo = this._MOFacade.GetMO(currentMo);
                long createCount = 0;
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

                    //如果有勾”选限制序列号内容为字符,数字和空格”和”序列号长度”，需要检查生产的序列号是否符合规则
                    if (this.checkBoxSNContentCheck.Checked)
                    {
                        string pattern = @"^([A-Za-z0-9]+[ ]*)*[A-Za-z0-9]+$";
                        string rcard = this.ucLabelEditRCardPrefix.Value.Trim();
                        Regex rex = new Regex(pattern, RegexOptions.IgnoreCase);
                        Match match = rex.Match(rcard);

                        if (!match.Success)
                        {
                            ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_SNContent_CheckWrong $CS_Param_RunSeq:" + this.ucLabelEditRCardPrefix.Value.Trim().ToString()));
                            return;
                        }

                    }


                    //如果生成的数量大于等于5000则提醒用户”将会生成的序列号个数超过，很有可能影响系统效率，是否继续？”，让用户选择是否继续操作。
                    if (this.ucLabelCreateQTY.Value.Trim() != "")
                    {
                        if (int.Parse(this.ucLabelCreateQTY.Value.Trim()) > 5000)
                        {
                            DialogResult dr = MessageBox.Show( MutiLanguages.ParserMessage("$Generate_number_Is_Greate"), MutiLanguages.ParserMessage("$ShowMessage"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);

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
                            DialogResult dr = MessageBox.Show( MutiLanguages.ParserMessage("$Generate_number_Is_Greate"), MutiLanguages.ParserMessage("$ShowMessage"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                            if (dr == DialogResult.No)
                            {
                                return;
                            }
                        }

                        createCount = endRcardCount - startRcardCount + 1;
                    }

                    //10进制、16进制、34进制生成序列号
                    //modify by klaus 多产生一个，但是不会关联到mocode
                    List<string> RCardList = CalcRCardEnd(createCount+1);

                    //根据生成的起始序列号和结束序列号检查是否在该工单已关联的序列号中，存在则报错
                    //foreach (string rcard in RCardList)
                        for (int i = 0; i < RCardList.Count - 1;i++ )
                        {
                            object obj = _MOFacade.GetMO2RCardLink(RCardList[i]);
                            if (obj != null)
                            {
                                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_RCARD_EXIST_MO2RCARDLINK$RCARD:" + RCardList[i]));
                                return;
                            }
                        }
                    DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);

                    this.DataProvider.BeginTransaction();
                    try
                    {
                        //foreach (string rcard in RCardList)
                        for (int i = 0; i < RCardList.Count - 1; i++)
                        {
                            MO2RCARDLINK rcarLink = new MO2RCARDLINK();
                            rcarLink.MOCode = this.txtMoCodeEdit.Value.Trim();
                            rcarLink.PrintTimes = 0;
                            rcarLink.LastPrintDate = 0;
                            rcarLink.LastPrintTime = 0;
                            rcarLink.LastPrintUSER = "";
                            rcarLink.RCard = RCardList[i];
                            rcarLink.MDate = dbDateTime.DBDate;
                            rcarLink.MTime = dbDateTime.DBTime;
                            rcarLink.MUser = ApplicationService.Current().UserCode;
                            _MOFacade.AddMO2RCardLink(rcarLink);
                        }
                        //add by klaus 20130508 将最大的值保存到serialbook中,显示出来
                        if (warehouseFacade == null)
                        {
                            warehouseFacade = new WarehouseFacade(this.DataProvider);
                        }
                        string maxNumberExist=warehouseFacade.GetMaxSerial(this.ucLabelEditRCardPrefix.Value.Trim().ToUpper());
                        SERIALBOOK serialBook = new SERIALBOOK();
                        string maxNumber = RCardList[RCardList.Count - 1].Substring(this.ucLabelEditRCardPrefix.Value.Trim().Length);
                        serialBook.SNPrefix = this.ucLabelEditRCardPrefix.Value.Trim().ToUpper();
                        serialBook.MaxSerial = maxNumber;
                        serialBook.SerialType = this.ultraOptionSetScale.CheckedIndex.ToString();
                        serialBook.MDate = dbDateTime.DBDate;
                        serialBook.MTime = dbDateTime.DBTime;
                        serialBook.MUser = ApplicationService.Current().UserCode;
                        if(string.IsNullOrEmpty(maxNumberExist))
                        {
                            //为空的话就新增
                            
                            warehouseFacade.AddSerialBook(serialBook);
                        }
                        else
                        {
                            //string.CompareOrdinal(maxNumber, maxNumberExist)大于0，则maxNumber大
                            if (string.CompareOrdinal(maxNumber, maxNumberExist) > 0)
                            {
                                warehouseFacade.UpdateSerialBook(serialBook);
                            }
                            else
                            { 
                                
                            }
                        }

                        //end
                        this.DataProvider.CommitTransaction();
                        ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Success, "$CS_SUCCESS_CREATE_RCARD"));
                        //add by klaus 产生新序列号之后重新获取最大的序列号
                        this.lblMaxNumber.Text = warehouseFacade.GetMaxSerial(this.ucLabelEditRCardPrefix.Value.Trim().ToUpper());
                        this.ucLERCStartRcard.Value = warehouseFacade.GetMaxSerial(this.ucLabelEditRCardPrefix.Value.Trim().ToUpper());
                        this.ucLERCStartRcard.Enabled = false;
                        this.ultraOptionSetScale.Enabled = false;
                        this.ucLabelCreateQTY.TextFocus(true,true);
                        //end
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
                        return;
                    }
                }
                else
                {
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_MO_Not_Exist"));
                    return;
                }               
            }
        }

        //打印
        private void ucButtonPrint_Click(object sender, EventArgs e)
        {
            //获取打印模板的路径
            if (this.ucLabelComboxPrintTemplete.SelectedIndex < 0)
            {
                this.ShowMessage("$Error_NO_TempLeteSelect");
                return;
            }

            printRcardList();

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
            this.lblMaxNumber.Text = "";
            this.ultraOptionSetScale.Enabled = true;
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

            //设置可编辑列的颜色
            //e.Layout.Bands[0].Columns["DATECODE"].CellAppearance.BackColor = Color.LawnGreen;
            //e.Layout.Bands[0].Columns["VenderLotNO"].CellAppearance.BackColor = Color.LawnGreen ;
            //e.Layout.Bands[0].Columns["VenderITEMCODE"].CellAppearance.BackColor = Color.LawnGreen;

            // 允许筛选
            e.Layout.Bands[0].Columns["MoCode"].AllowRowFiltering = DefaultableBoolean.True;
            e.Layout.Bands[0].Columns["MoCode"].SortIndicator = SortIndicator.Ascending;

            //this.InitGridLanguage(ultraGridMain);


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

                object[] mo2RcardLinks = _MOFacade.QueryMO2RCardLink(rcard, mocode);
                DataRow rowMo2Rcard;

                foreach (MO2RCARDLINK item in mo2RcardLinks)
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

                object[] mo2RcardLinks = _MOFacade.GetMO2RCardLinkByMoCode(moCode, beginRcard, endRcard, "");
                DataRow rowMo2Rcard;

                foreach (MO2RCARDLINK item in mo2RcardLinks)
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

        private void txtMoCodeEdit_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                string currentMo = this.txtMoCodeEdit.Value.Trim();
                //检查工单是否存在，且状态不为关闭
                object objMo = this._MOFacade.GetMO(currentMo);

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
                }

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
                this.ultraOptionSetScale.Enabled = true;
                this.ucLabelRCardLength.Value = item2SNCheck.SNLength.ToString();
                this.ucLabelEditRCardPrefix.Value = item2SNCheck.SNPrefix.ToString();
                //add by klaus 20130508 显示最大的序号
                if (warehouseFacade == null)
                {
                    warehouseFacade = new WarehouseFacade(this.DataProvider);
                }
                if (!string.IsNullOrEmpty(warehouseFacade.GetMaxSerial(item2SNCheck.SNPrefix.ToString())))
                {
                    string maxSerialNext = warehouseFacade.GetMaxSerial(item2SNCheck.SNPrefix.ToString());
                    SERIALBOOK serialBook = (SERIALBOOK)warehouseFacade.GetSerialBook(item2SNCheck.SNPrefix.ToString());
                    this.lblMaxNumber.Text = maxSerialNext;
                    this.ucLERCStartRcard.Value = maxSerialNext;
                    this.ucLERCStartRcard.Enabled = false;
                    this.ultraOptionSetScale.CheckedIndex = int.Parse(serialBook.SerialType);
                    this.ultraOptionSetScale.Enabled = false;
                }
                else
                {
                    int lenOfZero = item2SNCheck.SNLength - item2SNCheck.SNPrefix.Length - 1;

                    this.ucLERCStartRcard.Value = Math.Pow(10, lenOfZero).ToString().Substring(1, lenOfZero) + "1";
                    this.ucLERCStartRcard.Enabled = true;
                    this.ultraOptionSetScale.Enabled = true;
                    this.ultraOptionSetScale.CheckedIndex = 0;
                }

                //end
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
            try
            {
                startSN = long.Parse(NumberScaleHelper.ChangeNumber(ucLERCStartRcard.Value.Trim(), scale, NumberScale.Scale10));
            }
            catch (Exception ex)
            {
                this.ShowMessage(ex);

            }

            long endSN = startSN + count - 1;

            try
            {
                for (long i = startSN; i <= endSN; i++)
                {

                    string RCardEnd = NumberScaleHelper.ChangeNumber(i.ToString(), NumberScale.Scale10, scale);

                    RCardEnd = RCardEnd.PadLeft(ucLERCStartRcard.Value.Trim().Length, '0');

                    RCardList.Add(this.ucLabelEditRCardPrefix.Value.Trim() + RCardEnd);
                }
            }
            catch (Exception ex)
            {
                this.ShowMessage(ex);

            }

            return RCardList;
        }


    }


}

