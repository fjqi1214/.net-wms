using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Domain.DataCollect;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.DataCollect;
using BenQGuru.eMES.DataCollect.Action;
using BenQGuru.eMES.Client.Service;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.MOModel;
using UserControl;
using BenQGuru.eMES.SMT;
using BenQGuru.eMES.Domain.SMT;

namespace BenQGuru.eMES.Client
{
    /// <summary>
    /// FCollectionIDMerge 的摘要说明。
    /// </summary>
    public class FCollectionIDMerge_OutLine : BaseForm
    {
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel1;
        private UserControl.UCMessage ucMessage;
        private UserControl.UCButton ucBtnCancel;
        private UserControl.UCButton ucBtnExit;
        private UserControl.UCButton ucBtnOK;
        public UserControl.UCLabelEdit ucLERunningCard;
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.Container components = null;
        private UserControl.UCButton ucButton1;
        private UserControl.UCButton ucButton2;
        private UserControl.UCButton ucBtnRecede;
        private int _currSequence = 0;
        private int _mergeRule = 0;
        private ArrayList _runningCardList = null;
        private ProductInfo _product = null;
        private ActionOnLineHelper _helper = null;
        private string _idMergeType = string.Empty;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.GroupBox groupBox2;
        private UserControl.UCLabelEdit bCardTransLenULE;
        private UserControl.UCLabelEdit aCardTransLenULE;
        private UserControl.UCLabelEdit aCardTransLetterULE;
        private UserControl.UCLabelEdit bCardTransLetterULE;
        private SimulationReport simReport = null;
        private IDomainDataProvider _domainDataProvider = ApplicationService.Current().DataProvider;
        private TextBox txtIDMergeValue;
        private Label lblSplit;

        private DataCollectFacade _DataCollectFacade;
        private CheckBox chkUndo;
        private UCLabelEdit txtMoCode;

        public IDomainDataProvider DataProvider
        {
            get
            {
                return _domainDataProvider;
            }
        }

        private object[] transedRunningCardByProduct = null;	// Added by Icyer 2006/11/08
        public FCollectionIDMerge_OutLine()
        {
            //
            // Windows 窗体设计器支持所必需的
            //
            InitializeComponent();

            //
            // TODO: 在 InitializeComponent 调用后添加任何构造函数代码
            //			
            UserControl.UIStyleBuilder.FormUI(this);

            this._helper = new ActionOnLineHelper(this.DataProvider);

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FCollectionIDMerge_OutLine));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkUndo = new System.Windows.Forms.CheckBox();
            this.txtIDMergeValue = new System.Windows.Forms.TextBox();
            this.lblSplit = new System.Windows.Forms.Label();
            this.ucBtnCancel = new UserControl.UCButton();
            this.ucLERunningCard = new UserControl.UCLabelEdit();
            this.ucBtnExit = new UserControl.UCButton();
            this.ucBtnOK = new UserControl.UCButton();
            this.ucButton1 = new UserControl.UCButton();
            this.ucButton2 = new UserControl.UCButton();
            this.ucBtnRecede = new UserControl.UCButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtMoCode = new UserControl.UCLabelEdit();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.bCardTransLenULE = new UserControl.UCLabelEdit();
            this.aCardTransLenULE = new UserControl.UCLabelEdit();
            this.aCardTransLetterULE = new UserControl.UCLabelEdit();
            this.bCardTransLetterULE = new UserControl.UCLabelEdit();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.ucMessage = new UserControl.UCMessage();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkUndo);
            this.groupBox1.Controls.Add(this.txtIDMergeValue);
            this.groupBox1.Controls.Add(this.lblSplit);
            this.groupBox1.Controls.Add(this.ucBtnCancel);
            this.groupBox1.Controls.Add(this.ucLERunningCard);
            this.groupBox1.Controls.Add(this.ucBtnExit);
            this.groupBox1.Controls.Add(this.ucBtnOK);
            this.groupBox1.Controls.Add(this.ucButton1);
            this.groupBox1.Controls.Add(this.ucButton2);
            this.groupBox1.Controls.Add(this.ucBtnRecede);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 526);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(814, 97);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // chkUndo
            // 
            this.chkUndo.Location = new System.Drawing.Point(668, 54);
            this.chkUndo.Name = "chkUndo";
            this.chkUndo.Size = new System.Drawing.Size(104, 24);
            this.chkUndo.TabIndex = 21;
            this.chkUndo.Text = "更改转换结果";
            this.chkUndo.Visible = false;
            // 
            // txtIDMergeValue
            // 
            this.txtIDMergeValue.Location = new System.Drawing.Point(464, 23);
            this.txtIDMergeValue.Name = "txtIDMergeValue";
            this.txtIDMergeValue.Size = new System.Drawing.Size(154, 21);
            this.txtIDMergeValue.TabIndex = 20;
            // 
            // lblSplit
            // 
            this.lblSplit.AutoSize = true;
            this.lblSplit.Location = new System.Drawing.Point(395, 29);
            this.lblSplit.Name = "lblSplit";
            this.lblSplit.Size = new System.Drawing.Size(53, 12);
            this.lblSplit.TabIndex = 19;
            this.lblSplit.Text = "分版比例";
            // 
            // ucBtnCancel
            // 
            this.ucBtnCancel.BackColor = System.Drawing.SystemColors.Control;
            this.ucBtnCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucBtnCancel.BackgroundImage")));
            this.ucBtnCancel.ButtonType = UserControl.ButtonTypes.Cancle;
            this.ucBtnCancel.Caption = "取消";
            this.ucBtnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnCancel.Location = new System.Drawing.Point(248, 56);
            this.ucBtnCancel.Name = "ucBtnCancel";
            this.ucBtnCancel.Size = new System.Drawing.Size(88, 22);
            this.ucBtnCancel.TabIndex = 2;
            this.ucBtnCancel.Click += new System.EventHandler(this.ucBtnCancel_Click);
            // 
            // ucLERunningCard
            // 
            this.ucLERunningCard.AllowEditOnlyChecked = true;
            this.ucLERunningCard.AutoSelectAll = false;
            this.ucLERunningCard.AutoUpper = true;
            this.ucLERunningCard.Caption = "输入框";
            this.ucLERunningCard.Checked = false;
            this.ucLERunningCard.EditType = UserControl.EditTypes.String;
            this.ucLERunningCard.Location = new System.Drawing.Point(24, 22);
            this.ucLERunningCard.MaxLength = 40;
            this.ucLERunningCard.Multiline = false;
            this.ucLERunningCard.Name = "ucLERunningCard";
            this.ucLERunningCard.PasswordChar = '\0';
            this.ucLERunningCard.ReadOnly = false;
            this.ucLERunningCard.ShowCheckBox = false;
            this.ucLERunningCard.Size = new System.Drawing.Size(249, 24);
            this.ucLERunningCard.TabIndex = 1;
            this.ucLERunningCard.TabNext = false;
            this.ucLERunningCard.Value = "";
            this.ucLERunningCard.WidthType = UserControl.WidthTypes.Long;
            this.ucLERunningCard.XAlign = 73;
            this.ucLERunningCard.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ucLERunningCard_TxtboxKeyPress);
            // 
            // ucBtnExit
            // 
            this.ucBtnExit.BackColor = System.Drawing.SystemColors.Control;
            this.ucBtnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucBtnExit.BackgroundImage")));
            this.ucBtnExit.ButtonType = UserControl.ButtonTypes.Exit;
            this.ucBtnExit.Caption = "退出";
            this.ucBtnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnExit.Location = new System.Drawing.Point(508, 56);
            this.ucBtnExit.Name = "ucBtnExit";
            this.ucBtnExit.Size = new System.Drawing.Size(88, 22);
            this.ucBtnExit.TabIndex = 4;
            // 
            // ucBtnOK
            // 
            this.ucBtnOK.BackColor = System.Drawing.SystemColors.Control;
            this.ucBtnOK.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucBtnOK.BackgroundImage")));
            this.ucBtnOK.ButtonType = UserControl.ButtonTypes.Confirm;
            this.ucBtnOK.Caption = "确认";
            this.ucBtnOK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnOK.Location = new System.Drawing.Point(120, 56);
            this.ucBtnOK.Name = "ucBtnOK";
            this.ucBtnOK.Size = new System.Drawing.Size(88, 22);
            this.ucBtnOK.TabIndex = 1;
            this.ucBtnOK.Click += new System.EventHandler(this.ucBtnOK_Click);
            // 
            // ucButton1
            // 
            this.ucButton1.BackColor = System.Drawing.SystemColors.Control;
            this.ucButton1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButton1.BackgroundImage")));
            this.ucButton1.ButtonType = UserControl.ButtonTypes.Confirm;
            this.ucButton1.Caption = "确认";
            this.ucButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButton1.Location = new System.Drawing.Point(120, 56);
            this.ucButton1.Name = "ucButton1";
            this.ucButton1.Size = new System.Drawing.Size(88, 22);
            this.ucButton1.TabIndex = 1;
            // 
            // ucButton2
            // 
            this.ucButton2.BackColor = System.Drawing.SystemColors.Control;
            this.ucButton2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButton2.BackgroundImage")));
            this.ucButton2.ButtonType = UserControl.ButtonTypes.Cancle;
            this.ucButton2.Caption = "取消";
            this.ucButton2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButton2.Location = new System.Drawing.Point(248, 56);
            this.ucButton2.Name = "ucButton2";
            this.ucButton2.Size = new System.Drawing.Size(88, 22);
            this.ucButton2.TabIndex = 2;
            // 
            // ucBtnRecede
            // 
            this.ucBtnRecede.BackColor = System.Drawing.SystemColors.Control;
            this.ucBtnRecede.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucBtnRecede.BackgroundImage")));
            this.ucBtnRecede.ButtonType = UserControl.ButtonTypes.Change;
            this.ucBtnRecede.Caption = "更正";
            this.ucBtnRecede.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnRecede.Location = new System.Drawing.Point(384, 56);
            this.ucBtnRecede.Name = "ucBtnRecede";
            this.ucBtnRecede.Size = new System.Drawing.Size(88, 22);
            this.ucBtnRecede.TabIndex = 3;
            this.ucBtnRecede.Click += new System.EventHandler(this.ucBtnRecede_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtMoCode);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.splitter1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(814, 112);
            this.panel1.TabIndex = 1;
            // 
            // txtMoCode
            // 
            this.txtMoCode.AllowEditOnlyChecked = true;
            this.txtMoCode.AutoSelectAll = false;
            this.txtMoCode.AutoUpper = true;
            this.txtMoCode.Caption = "工单代码";
            this.txtMoCode.Checked = false;
            this.txtMoCode.EditType = UserControl.EditTypes.String;
            this.txtMoCode.Location = new System.Drawing.Point(24, 12);
            this.txtMoCode.MaxLength = 40;
            this.txtMoCode.Multiline = false;
            this.txtMoCode.Name = "txtMoCode";
            this.txtMoCode.PasswordChar = '\0';
            this.txtMoCode.ReadOnly = false;
            this.txtMoCode.ShowCheckBox = true;
            this.txtMoCode.Size = new System.Drawing.Size(210, 24);
            this.txtMoCode.TabIndex = 0;
            this.txtMoCode.TabNext = false;
            this.txtMoCode.Value = "";
            this.txtMoCode.WidthType = UserControl.WidthTypes.Normal;
            this.txtMoCode.XAlign = 101;
            this.txtMoCode.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMoCode_KeyPress);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.bCardTransLenULE);
            this.groupBox2.Controls.Add(this.aCardTransLenULE);
            this.groupBox2.Controls.Add(this.aCardTransLetterULE);
            this.groupBox2.Controls.Add(this.bCardTransLetterULE);
            this.groupBox2.Location = new System.Drawing.Point(24, 32);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(713, 72);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            // 
            // bCardTransLenULE
            // 
            this.bCardTransLenULE.AllowEditOnlyChecked = true;
            this.bCardTransLenULE.AutoSelectAll = false;
            this.bCardTransLenULE.AutoUpper = true;
            this.bCardTransLenULE.Caption = "转换前序列号长度";
            this.bCardTransLenULE.Checked = false;
            this.bCardTransLenULE.EditType = UserControl.EditTypes.String;
            this.bCardTransLenULE.Location = new System.Drawing.Point(42, 16);
            this.bCardTransLenULE.MaxLength = 40;
            this.bCardTransLenULE.Multiline = false;
            this.bCardTransLenULE.Name = "bCardTransLenULE";
            this.bCardTransLenULE.PasswordChar = '\0';
            this.bCardTransLenULE.ReadOnly = false;
            this.bCardTransLenULE.ShowCheckBox = true;
            this.bCardTransLenULE.Size = new System.Drawing.Size(258, 24);
            this.bCardTransLenULE.TabIndex = 22;
            this.bCardTransLenULE.TabNext = false;
            this.bCardTransLenULE.Value = "";
            this.bCardTransLenULE.WidthType = UserControl.WidthTypes.Normal;
            this.bCardTransLenULE.XAlign = 167;
            // 
            // aCardTransLenULE
            // 
            this.aCardTransLenULE.AllowEditOnlyChecked = true;
            this.aCardTransLenULE.AutoSelectAll = false;
            this.aCardTransLenULE.AutoUpper = true;
            this.aCardTransLenULE.Caption = "转换后序列号长度";
            this.aCardTransLenULE.Checked = false;
            this.aCardTransLenULE.EditType = UserControl.EditTypes.String;
            this.aCardTransLenULE.Location = new System.Drawing.Point(42, 40);
            this.aCardTransLenULE.MaxLength = 40;
            this.aCardTransLenULE.Multiline = false;
            this.aCardTransLenULE.Name = "aCardTransLenULE";
            this.aCardTransLenULE.PasswordChar = '\0';
            this.aCardTransLenULE.ReadOnly = false;
            this.aCardTransLenULE.ShowCheckBox = true;
            this.aCardTransLenULE.Size = new System.Drawing.Size(258, 24);
            this.aCardTransLenULE.TabIndex = 21;
            this.aCardTransLenULE.TabNext = false;
            this.aCardTransLenULE.Value = "";
            this.aCardTransLenULE.WidthType = UserControl.WidthTypes.Normal;
            this.aCardTransLenULE.XAlign = 167;
            // 
            // aCardTransLetterULE
            // 
            this.aCardTransLetterULE.AllowEditOnlyChecked = true;
            this.aCardTransLetterULE.AutoSelectAll = false;
            this.aCardTransLetterULE.AutoUpper = true;
            this.aCardTransLetterULE.Caption = "转换后序列号首字符串";
            this.aCardTransLetterULE.Checked = false;
            this.aCardTransLetterULE.EditType = UserControl.EditTypes.String;
            this.aCardTransLetterULE.Location = new System.Drawing.Point(383, 40);
            this.aCardTransLetterULE.MaxLength = 40;
            this.aCardTransLetterULE.Multiline = false;
            this.aCardTransLetterULE.Name = "aCardTransLetterULE";
            this.aCardTransLetterULE.PasswordChar = '\0';
            this.aCardTransLetterULE.ReadOnly = false;
            this.aCardTransLetterULE.ShowCheckBox = true;
            this.aCardTransLetterULE.Size = new System.Drawing.Size(282, 24);
            this.aCardTransLetterULE.TabIndex = 20;
            this.aCardTransLetterULE.TabNext = false;
            this.aCardTransLetterULE.Value = "";
            this.aCardTransLetterULE.WidthType = UserControl.WidthTypes.Normal;
            this.aCardTransLetterULE.XAlign = 532;
            this.aCardTransLetterULE.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.aCardTransLetterULE_TxtboxKeyPress);
            // 
            // bCardTransLetterULE
            // 
            this.bCardTransLetterULE.AllowEditOnlyChecked = true;
            this.bCardTransLetterULE.AutoSelectAll = false;
            this.bCardTransLetterULE.AutoUpper = true;
            this.bCardTransLetterULE.Caption = "转换前序列号首字符串";
            this.bCardTransLetterULE.Checked = false;
            this.bCardTransLetterULE.EditType = UserControl.EditTypes.String;
            this.bCardTransLetterULE.Location = new System.Drawing.Point(383, 16);
            this.bCardTransLetterULE.MaxLength = 40;
            this.bCardTransLetterULE.Multiline = false;
            this.bCardTransLetterULE.Name = "bCardTransLetterULE";
            this.bCardTransLetterULE.PasswordChar = '\0';
            this.bCardTransLetterULE.ReadOnly = false;
            this.bCardTransLetterULE.ShowCheckBox = true;
            this.bCardTransLetterULE.Size = new System.Drawing.Size(282, 24);
            this.bCardTransLetterULE.TabIndex = 19;
            this.bCardTransLetterULE.TabNext = false;
            this.bCardTransLetterULE.Value = "";
            this.bCardTransLetterULE.WidthType = UserControl.WidthTypes.Normal;
            this.bCardTransLetterULE.XAlign = 532;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(0, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(2, 112);
            this.splitter1.TabIndex = 13;
            this.splitter1.TabStop = false;
            // 
            // ucMessage
            // 
            this.ucMessage.BackColor = System.Drawing.Color.Gainsboro;
            this.ucMessage.ButtonVisible = false;
            this.ucMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucMessage.Location = new System.Drawing.Point(0, 112);
            this.ucMessage.Name = "ucMessage";
            this.ucMessage.Size = new System.Drawing.Size(814, 414);
            this.ucMessage.TabIndex = 2;
            // 
            // FCollectionIDMerge_OutLine
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(814, 623);
            this.Controls.Add(this.ucMessage);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.Name = "FCollectionIDMerge_OutLine";
            this.Text = "SMT分板采集";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FCollectionIDMerge_Load);
            this.Closed += new System.EventHandler(this.FCollectionIDMerge_Closed);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        #region Button Events
        private void ucBtnOK_Click(object sender, System.EventArgs e)
        {
            if (this.ucLERunningCard.Value.Trim() == string.Empty)
            {
                //Laws Lu,2005/08/11,新增焦点设置
                ucLERunningCard.TextFocus(true, true);
                return;
            }

            this.ucMessage.Add(string.Format("<< {0}", this.ucLERunningCard.Value.Trim().ToUpper()));

            // 输入分板前产品序列号
            if (this._currSequence == 0)
            {
                #region 工单、序列号匹配
                _runningCardList = new ArrayList();

                if (this.txtMoCode.Checked)
                {
                    if (string.IsNullOrEmpty(txtMoCode.Value.ToUpper().Trim()))
                    {
                        this.ucMessage.Add(new UserControl.Message(MessageType.Error, "$CS_Please_Input_MOCode"));
                        this.ucLERunningCard.Value = "";

                        //Laws Lu,2005/08/11,新增焦点设置
                        txtMoCode.Focus();
                        return;
                    }
                }

                //Laws Lu,2005/10/19,新增	缓解性能问题
                //Laws Lu,2006/12/25 修改	减少Open/Close的次数
                ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
                ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.AutoCloseConnection = true;
                //added by jessie lee, 2005/11/29
                #region 判断转换前序列号是否符合条件
                //长度检查
                if (bCardTransLenULE.Checked)
                {
                    if (bCardTransLenULE.Value.Trim().Length == 0)
                    {
                        this.ucMessage.Add(new UserControl.Message(MessageType.Error, "$CS_Before_Card_Transfer_Empty"));
                        ucLERunningCard.TextFocus(true, true);
                        return;
                    }

                    int len = 0;
                    try
                    {
                        len = int.Parse(bCardTransLenULE.Value.Trim());
                    }
                    catch
                    {
                        this.ucMessage.Add(new UserControl.Message(MessageType.Error, "$Error_BeforeCardTransLen_Should_be_Integer"));
                        ucLERunningCard.TextFocus(true, true);
                        return;
                    }

                    if (len != this.ucLERunningCard.Value.Trim().Length)
                    {
                        this.ucMessage.Add(new UserControl.Message(MessageType.Error, "$Error_BeforeCardTransLen_Not_Correct"));
                        ucLERunningCard.TextFocus(true, true);
                        return;
                    }
                }

                //首字符串检查
                if (bCardTransLetterULE.Checked)
                {
                    if (bCardTransLetterULE.Value.Trim().Length == 0)
                    {
                        this.ucMessage.Add(new UserControl.Message(MessageType.Error, "$CS_Before_Card_Transfer_FLetter_Empty"));
                        ucLERunningCard.TextFocus(true, true);
                        return;
                    }

                    int index = ucLERunningCard.Value.Trim().IndexOf(bCardTransLetterULE.Value.Trim());
                    if (index != 0)
                    {
                        this.ucMessage.Add(new UserControl.Message(MessageType.Error, "$CS_Before_Card_Transfer_FLetter_NotCompare"));
                        ucLERunningCard.TextFocus(true, true);
                        return;
                    }
                }
                #endregion

                #region 取得分板比例,从工单和SMT是否是打叉板得到分板比例，若不是，带出工单的分版比例

                simReport = _DataCollectFacade.GetLastSimulationReport(this.ucLERunningCard.Value.Trim().ToUpper());
                if (simReport == null)
                {
                    this.ucMessage.Add(new UserControl.Message(MessageType.Error, "$Error_CS_ID_Not_Exist"));
                    this.ucLERunningCard.Value = "";
                    this.ucLERunningCard.TextFocus(true, true);
                    return;
                }
                else
                {
                    if (simReport.IsComplete != "1")
                    {
                        //完工后才可以分板，故注释。 
                        //主要应用于以下业务情况：
                        //序列号 s1 通过两张工单 mo1, mo2 完成。s1 经过第一张工单 mo1 完工后，需要进行分板 s1--> s1-1, s1-2，
                        //分板后再用 s1-1, s1-2 进行第二张工单 mo2 的归属生产。
                        this.ucMessage.Add(new UserControl.Message(MessageType.Error, "$CS_ERROR_PRODUCT_NO_COMPLETE"));
                        ucLERunningCard.TextFocus(true, true);
                        return;
                    }

                    if (txtMoCode.Checked)
                    {
                        if (string.Compare(txtMoCode.Value.ToUpper().Trim(), simReport.MOCode, true) != 0)
                        {
                            this.ucMessage.Add(new UserControl.Message(MessageType.Error, "$RCARD_NOT_IN_MO"));
                            ucLERunningCard.TextFocus(true, true);
                            return;
                        }

                    }

                    int rcardCount = _DataCollectFacade.GetSplitBoardCount(this.ucLERunningCard.Value.Trim().ToUpper());

                    if (rcardCount > 0)
                    {
                        //已经分板的序列号就不可以在分了
                        this.ucMessage.Add(new UserControl.Message(MessageType.Error, "$CS_ERROR_PRODUCT_ALREADY_SPLITE"));
                        ucLERunningCard.TextFocus(true, true);
                        return;
                    }


                }


                SMTFacade smtFacade = new SMTFacade(this.DataProvider);
                object objsmtrel = smtFacade.GetSMTRelationQty(this.ucLERunningCard.Value.Trim().ToUpper(), simReport.MOCode);
                if (objsmtrel != null)
                {
                    Smtrelationqty smtrel = (Smtrelationqty)objsmtrel;
                    this.txtIDMergeValue.Text = Convert.ToString(smtrel.Relationqtry);
                }
                else
                {
                    MOFacade mofacade = new MOFacade(this.DataProvider);
                    object objMO = mofacade.GetMO(simReport.MOCode);
                    if (objMO != null)
                    {
                        MO mo = (MO)objMO;
                        this.txtIDMergeValue.Text = Convert.ToString(mo.IDMergeRule);
                    }
                    else
                    {
                        this.ucMessage.Add(new UserControl.Message(MessageType.Error, "$CS_MO_Not_Exit"));
                        txtMoCode.Focus();
                        txtMoCode.SelectAll();
                        return;
                    }
                }
                #endregion

                #region 判断分板前序列号是否存在


                int mergeRule = 1;
                try
                {
                    mergeRule = System.Int32.Parse(this.txtIDMergeValue.Text.ToUpper().Trim());
                }
                catch
                {
                    this.ucMessage.Add(new UserControl.Message(MessageType.Error, "$Error_CS_IDMerge_Should_be_Integer"));//分板比例必须为整数
                    //Laws Lu,2005/08/11,新增焦点设置
                    ucLERunningCard.TextFocus(true, true);
                    return;
                }

                if (mergeRule <= 0)
                {
                    this.ucMessage.Add(new UserControl.Message(MessageType.Error, "$Error_CS_IDMerge_Should_Over_Zero"));//分板比例必须大于零
                    ucLERunningCard.TextFocus(true, true);
                    return;
                }
                this._mergeRule = mergeRule;
                #endregion

                #endregion

            }
            else
            {

                //added by jessie lee, 2005/11/29
                #region 判断转换后序列号是否符合条件
                //长度检查
                if (aCardTransLenULE.Checked)
                {
                    if (aCardTransLenULE.Value.Trim().Length == 0)
                    {
                        this.ucMessage.Add(new UserControl.Message(MessageType.Error, "$CS_After_Card_Transfer_Empty"));
                        ucLERunningCard.TextFocus(true, true);
                        return;
                    }

                    int len = 0;
                    try
                    {
                        len = int.Parse(aCardTransLenULE.Value.Trim());
                    }
                    catch
                    {
                        this.ucMessage.Add(new UserControl.Message(MessageType.Error, "$Error_AfterCardTransLen_Should_be_Integer"));
                        ucLERunningCard.TextFocus(true, true);
                        return;
                    }

                    if (len != this.ucLERunningCard.Value.Trim().Length)
                    {
                        this.ucMessage.Add(new UserControl.Message(MessageType.Error, "$Error_AfterCardTransLen_Not_Correct"));
                        ucLERunningCard.TextFocus(true, true);
                        return;
                    }
                }

                //首字符串检查
                if (aCardTransLetterULE.Checked)
                {
                    if (aCardTransLetterULE.Value.Trim().Length == 0)
                    {
                        this.ucMessage.Add(new UserControl.Message(MessageType.Error, "$CS_After_Card_Transfer_FLetter_Empty"));
                        ucLERunningCard.TextFocus(true, true);
                        return;
                    }

                    int index = ucLERunningCard.Value.Trim().IndexOf(aCardTransLetterULE.Value.Trim());
                    if (index != 0)
                    {
                        this.ucMessage.Add(new UserControl.Message(MessageType.Error, "$CS_After_Card_Transfer_FLetter_NotCompare"));
                        ucLERunningCard.TextFocus(true, true);
                        return;
                    }
                }
                #endregion

                #region 判断分板后序列号是否重复
                if (this._runningCardList.Contains(this.ucLERunningCard.Value.Trim().ToUpper()))
                {
                    this.ucMessage.Add(new UserControl.Message(MessageType.Error, "$Error_CS_Merge_ID_Exist"));//转换后产品序列号重复
                    this.ucLERunningCard.Value = "";

                    //Laws Lu,2005/08/11,新增焦点设置
                    ucLERunningCard.TextFocus(true, true);
                    return;
                }
                #endregion


                int rcardCount = _DataCollectFacade.GetSplitBoardCount(this.ucLERunningCard.Value.Trim().ToUpper());

                if (rcardCount > 0)
                {
                    //已经分板的序列号就不可以在分了
                    this.ucMessage.Add(new UserControl.Message(MessageType.Error, "$CS_ERROR_PRODUCT_ALREADY_SPLITE"));
                    this.ucMessage.AddBoldText(string.Format(">>$CS_Please_Input_Merge_ID {0}/{1}", this._currSequence.ToString(), this._mergeRule.ToString()));//请输入转换后产品序列号
                    ucLERunningCard.TextFocus(true, true);
                    return;
                }


                this._runningCardList.Add(this.ucLERunningCard.Value.Trim().ToUpper());
            }

            if (this._currSequence < this._mergeRule)
            {
                this._currSequence++;

                this.txtIDMergeValue.Enabled = false;
                this.ucMessage.AddBoldText(string.Format(">>$CS_Please_Input_Merge_ID {0}/{1}", this._currSequence.ToString(), this._mergeRule.ToString()));//请输入转换后产品序列号
            }
            else if (this._currSequence == this._mergeRule) // 达到分板比例,写入数据库
            {
                try
                {
                    this.DataProvider.BeginTransaction();
                    int tempCount = 1;
                    DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
                    foreach (string strRcard in _runningCardList)
                    {

                        #region 将ID添加到SplitBoard
                        SplitBoard splitBorad = new SplitBoard();
                        splitBorad.Seq = tempCount;
                        splitBorad.Mocode = simReport.MOCode;
                        splitBorad.Rcard = strRcard;
                        splitBorad.Modelcode = simReport.ModelCode;
                        splitBorad.Itemcode = simReport.ItemCode;
                        splitBorad.Opcode = simReport.OPCode;
                        splitBorad.Rescode = ApplicationService.Current().ResourceCode;
                        splitBorad.Routecode = simReport.RouteCode;
                        splitBorad.Scard = simReport.SourceCard;
                        splitBorad.Segcode = simReport.SegmentCode;
                        splitBorad.Shiftcode = simReport.ShiftCode;
                        splitBorad.Shifttypecode = simReport.ShiftTypeCode;
                        splitBorad.Sscode = simReport.StepSequenceCode;
                        splitBorad.Tpcode = simReport.TimePeriodCode;
                        splitBorad.Muser = ApplicationService.Current().UserCode;
                        splitBorad.Mdate = dbDateTime.DBDate;
                        splitBorad.Mtime = dbDateTime.DBTime;
                        _DataCollectFacade.AddSplitBoard(splitBorad);
                        tempCount++;
                        #endregion
                    }
                    this.DataProvider.CommitTransaction();

                    this.ucMessage.Add(new UserControl.Message(MessageType.Success, ">>$CS_SplitID_CollectSuccess"));//产品序列号转换采集成功
                    //added by jessie lee, 2005/11/29,
                }
                catch (Exception ex)
                {
                    this.DataProvider.RollbackTransaction();
                    this.ucMessage.Add(new UserControl.Message(MessageType.Error, ">>$CS_SplitID_CollectSuccess"));//产品序列号转换采集成功
                }


                this.initInput();
            }

            this.ucLERunningCard.Value = "";

            //Laws Lu,2005/08/11,新增焦点设置
            if (!aCardTransLetterULE.Checked)
            {
                ucLERunningCard.TextFocus(true, true);
            }
            else
            {
                ucLERunningCard.TextFocus(true, true);
            }
        }

        private void ucBtnCancel_Click(object sender, System.EventArgs e)
        {
            this.initInput();
            this.ucLERunningCard.TextFocus(true, true);
        }

        private void ucBtnRecede_Click(object sender, System.EventArgs e)
        {
            if (this._currSequence > 0)
            {
                
                if (this._runningCardList != null &&  this._runningCardList.Count > 0)
                {
                    this._runningCardList.RemoveAt(this._runningCardList.Count - 1);
                }
                this._currSequence--;

                if (this._currSequence > 0)
                {
                    this.ucMessage.Add(string.Format(">>$CS_Please_Input_Merge_ID {0}/{1}", this._currSequence.ToString(), this._mergeRule.ToString()));
                }
                else
                {
                    this.ucMessage.Add(">>$CS_Please_Input_ID_To_Merge");//请输入转换前产品序列号
                    this.txtIDMergeValue.Enabled = true;
                }
            }

            //Laws Lu,2005/08/11,新增焦点设置
            ucLERunningCard.TextFocus(true, true);
        }

        #endregion

        #region Events
        public void ucLERunningCard_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                this.ucBtnOK_Click(sender, null);
            }
        }

        private void FCollectionIDMerge_Load(object sender, System.EventArgs e)
        {
            this.initInput();
            _DataCollectFacade = new DataCollectFacade(this.DataProvider);
            //this.InitPageLanguage();
        }
        #endregion

        #region Function
        private void initInput()
        {
            this.chkUndo.Checked = false;	// Added by Icyer 2006/11/08
            this._currSequence = 0;
            this.txtIDMergeValue.Enabled = true;
            this.ucMessage.Add(">>$CS_Please_Input_ID_To_Merge");

            this.txtMoCode.TextFocus(false, true);
        }

        #endregion

        private void FCollectionIDMerge_Closed(object sender, System.EventArgs e)
        {
            if (this.DataProvider != null)
                ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)this.DataProvider).PersistBroker.CloseConnection();
        }

        private void panel2_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {

        }

        /// <summary>
        /// 检查当前rcard是否拆解或则报废
        /// </summary>
        /// <param name="rcard"></param>
        /// <param name="rcardseq"></param>
        /// <param name="mocode"></param>
        /// <returns></returns>
        private bool CheckIMEISpliteOrScrape(string rcard, decimal rcardseq, string mocode)
        {
            string sql = string.Format(" select count(*) from tblts where rcard='{0}' and rcardseq={1} and mocode='{2}' and tsstatus in ('{3}','{4}')",
                rcard, rcardseq, mocode, TSStatus.TSStatus_Scrap, TSStatus.TSStatus_Split);
            int count = this.DataProvider.GetCount(new SQLCondition(sql));
            if (count > 0)
            {
                return true;
            }
            return false;
        }

        private void aCardTransLetterULE_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                ucLERunningCard.TextFocus(true, true);
            }
        }

        private void txtMoCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (string.IsNullOrEmpty(this.txtMoCode.Value.ToUpper().Trim()))
                {
                    //this.txtMoCode.SelectAll();
                    //this.txtMoCode.Focus();
                    this.txtMoCode.TextFocus(false, true);
                }
                else
                {
                    this.ucLERunningCard.TextFocus(false, true);
                }
            }
        }

    }
}
