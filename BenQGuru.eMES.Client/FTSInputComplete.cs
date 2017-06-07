using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

#region Project
using BenQGuru.eMES.TS;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Client.Service;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.DataCollect.Action;
using BenQGuru.eMES.DataCollect;
using UserControl;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.TSModel;
using BenQGuru.eMES.Domain.TS;
using BenQGuru.eMES.Domain.Package;
using BenQGuru.eMES.Domain.DataCollect;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Domain.TSModel;
using BenQGuru.eMES.BaseSetting;
#endregion

namespace BenQGuru.eMES.Client
{
    /// <summary>
    /// FTSInputComplete 的摘要说明。
    /// </summary>
    public class FTSInputComplete : BaseForm
    {
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox;
        private UserControl.UCButton btnConfirm;
        private UserControl.UCButton ucButtonExit;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox2;
        /// <summary>
        /// 报废
        /// </summary>
        private Infragistics.Win.UltraWinEditors.UltraCheckEditor ultraCheckEditor2;
        private System.Windows.Forms.GroupBox groupBox1;
        /// <summary>
        /// 工序
        /// </summary>
        private UserControl.UCLabelCombox OPCode;
        /// <summary>
        /// 产品代码
        /// </summary>
        private UserControl.UCLabelEdit ucLabelEditItemCode;
        /// <summary>
        /// 工单
        /// </summary>
        private UserControl.UCLabelEdit ucLabelEditMOCode;
        /// <summary>
        /// 回流
        /// </summary>
        private Infragistics.Win.UltraWinEditors.UltraCheckEditor ultraCheckEditorReflow;

        private IDomainDataProvider _domainDataProvider = ApplicationService.Current().DataProvider;
        private System.Windows.Forms.Label txtRunningCard;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor rCardEditor;
        private UserControl.UCLabelEdit txtAgentUser;
        private System.Windows.Forms.Label scrapLabel;
        private System.Windows.Forms.TextBox txtScrapCause;
        private UCLabelCombox Route;
        private Infragistics.Win.UltraWinEditors.UltraCheckEditor ultraCheckEditorMisjudge;
        private Infragistics.Win.UltraWinEditors.UltraCheckEditor ultraCheckEditorRMA;

        public IDomainDataProvider DataProvider
        {
            get
            {
                return _domainDataProvider;
            }
        }

        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.Container components = null;

        public FTSInputComplete()
        {
            //
            // Windows 窗体设计器支持所必需的
            //
            InitializeComponent();

            //
            // TODO: 在 InitializeComponent 调用后添加任何构造函数代码
            //
            UserControl.UIStyleBuilder.FormUI(this);
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FTSInputComplete));
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtScrapCause = new System.Windows.Forms.TextBox();
            this.scrapLabel = new System.Windows.Forms.Label();
            this.ultraCheckEditor2 = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Route = new UserControl.UCLabelCombox();
            this.OPCode = new UserControl.UCLabelCombox();
            this.ucLabelEditItemCode = new UserControl.UCLabelEdit();
            this.ucLabelEditMOCode = new UserControl.UCLabelEdit();
            this.ultraCheckEditorReflow = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ultraCheckEditorRMA = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
            this.ultraCheckEditorMisjudge = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
            this.txtAgentUser = new UserControl.UCLabelEdit();
            this.rCardEditor = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.txtRunningCard = new System.Windows.Forms.Label();
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.btnConfirm = new UserControl.UCButton();
            this.ucButtonExit = new UserControl.UCButton();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rCardEditor)).BeginInit();
            this.groupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.groupBox2);
            this.groupBox3.Controls.Add(this.groupBox1);
            this.groupBox3.Controls.Add(this.panel1);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(565, 227);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtScrapCause);
            this.groupBox2.Controls.Add(this.scrapLabel);
            this.groupBox2.Controls.Add(this.ultraCheckEditor2);
            this.groupBox2.Location = new System.Drawing.Point(0, 184);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(498, 104);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Visible = false;
            // 
            // txtScrapCause
            // 
            this.txtScrapCause.Location = new System.Drawing.Point(80, 24);
            this.txtScrapCause.MaxLength = 100;
            this.txtScrapCause.Multiline = true;
            this.txtScrapCause.Name = "txtScrapCause";
            this.txtScrapCause.ReadOnly = true;
            this.txtScrapCause.Size = new System.Drawing.Size(328, 72);
            this.txtScrapCause.TabIndex = 10;
            // 
            // scrapLabel
            // 
            this.scrapLabel.Location = new System.Drawing.Point(24, 24);
            this.scrapLabel.Name = "scrapLabel";
            this.scrapLabel.Size = new System.Drawing.Size(56, 16);
            this.scrapLabel.TabIndex = 9;
            this.scrapLabel.Text = "报废原因";
            // 
            // ultraCheckEditor2
            // 
            this.ultraCheckEditor2.Location = new System.Drawing.Point(8, 0);
            this.ultraCheckEditor2.Name = "ultraCheckEditor2";
            this.ultraCheckEditor2.Size = new System.Drawing.Size(48, 16);
            this.ultraCheckEditor2.TabIndex = 8;
            this.ultraCheckEditor2.Text = "报废";
            this.ultraCheckEditor2.CheckedValueChanged += new System.EventHandler(this.ultraCheckEditor2_CheckedValueChanged);
            this.ultraCheckEditor2.CheckedChanged += new System.EventHandler(this.ultraCheckEditor2_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Route);
            this.groupBox1.Controls.Add(this.OPCode);
            this.groupBox1.Controls.Add(this.ucLabelEditItemCode);
            this.groupBox1.Controls.Add(this.ucLabelEditMOCode);
            this.groupBox1.Controls.Add(this.ultraCheckEditorReflow);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(3, 91);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(559, 79);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            // 
            // Route
            // 
            this.Route.AllowEditOnlyChecked = true;
            this.Route.Caption = "途程";
            this.Route.Checked = false;
            this.Route.Location = new System.Drawing.Point(33, 45);
            this.Route.Name = "Route";
            this.Route.SelectedIndex = -1;
            this.Route.ShowCheckBox = false;
            this.Route.Size = new System.Drawing.Size(170, 24);
            this.Route.TabIndex = 6;
            this.Route.WidthType = UserControl.WidthTypes.Normal;
            this.Route.XAlign = 70;
            this.Route.SelectedIndexChanged += new System.EventHandler(this.ucLabComboxRoute_SelectedIndexChanged);
            // 
            // OPCode
            // 
            this.OPCode.AllowEditOnlyChecked = true;
            this.OPCode.Caption = "工序";
            this.OPCode.Checked = false;
            this.OPCode.Location = new System.Drawing.Point(309, 45);
            this.OPCode.Name = "OPCode";
            this.OPCode.SelectedIndex = -1;
            this.OPCode.ShowCheckBox = false;
            this.OPCode.Size = new System.Drawing.Size(170, 24);
            this.OPCode.TabIndex = 7;
            this.OPCode.WidthType = UserControl.WidthTypes.Normal;
            this.OPCode.XAlign = 346;
            // 
            // ucLabelEditItemCode
            // 
            this.ucLabelEditItemCode.AllowEditOnlyChecked = true;
            this.ucLabelEditItemCode.AutoSelectAll = false;
            this.ucLabelEditItemCode.AutoUpper = true;
            this.ucLabelEditItemCode.Caption = "产品代码";
            this.ucLabelEditItemCode.Checked = false;
            this.ucLabelEditItemCode.EditType = UserControl.EditTypes.String;
            this.ucLabelEditItemCode.Location = new System.Drawing.Point(287, 15);
            this.ucLabelEditItemCode.MaxLength = 40;
            this.ucLabelEditItemCode.Multiline = false;
            this.ucLabelEditItemCode.Name = "ucLabelEditItemCode";
            this.ucLabelEditItemCode.PasswordChar = '\0';
            this.ucLabelEditItemCode.ReadOnly = true;
            this.ucLabelEditItemCode.ShowCheckBox = false;
            this.ucLabelEditItemCode.Size = new System.Drawing.Size(194, 24);
            this.ucLabelEditItemCode.TabIndex = 5;
            this.ucLabelEditItemCode.TabNext = true;
            this.ucLabelEditItemCode.Value = "";
            this.ucLabelEditItemCode.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabelEditItemCode.XAlign = 348;
            // 
            // ucLabelEditMOCode
            // 
            this.ucLabelEditMOCode.AllowEditOnlyChecked = true;
            this.ucLabelEditMOCode.AutoSelectAll = false;
            this.ucLabelEditMOCode.AutoUpper = true;
            this.ucLabelEditMOCode.Caption = "工单";
            this.ucLabelEditMOCode.Checked = false;
            this.ucLabelEditMOCode.EditType = UserControl.EditTypes.String;
            this.ucLabelEditMOCode.Location = new System.Drawing.Point(33, 16);
            this.ucLabelEditMOCode.MaxLength = 40;
            this.ucLabelEditMOCode.Multiline = false;
            this.ucLabelEditMOCode.Name = "ucLabelEditMOCode";
            this.ucLabelEditMOCode.PasswordChar = '\0';
            this.ucLabelEditMOCode.ReadOnly = true;
            this.ucLabelEditMOCode.ShowCheckBox = false;
            this.ucLabelEditMOCode.Size = new System.Drawing.Size(170, 24);
            this.ucLabelEditMOCode.TabIndex = 4;
            this.ucLabelEditMOCode.TabNext = true;
            this.ucLabelEditMOCode.Value = "";
            this.ucLabelEditMOCode.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabelEditMOCode.XAlign = 70;
            // 
            // ultraCheckEditorReflow
            // 
            this.ultraCheckEditorReflow.Location = new System.Drawing.Point(12, 0);
            this.ultraCheckEditorReflow.Name = "ultraCheckEditorReflow";
            this.ultraCheckEditorReflow.Size = new System.Drawing.Size(68, 16);
            this.ultraCheckEditorReflow.TabIndex = 3;
            this.ultraCheckEditorReflow.Text = "回流";
            this.ultraCheckEditorReflow.CheckedValueChanged += new System.EventHandler(this.ultraCheckEditor1_CheckedValueChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ultraCheckEditorRMA);
            this.panel1.Controls.Add(this.ultraCheckEditorMisjudge);
            this.panel1.Controls.Add(this.txtAgentUser);
            this.panel1.Controls.Add(this.rCardEditor);
            this.panel1.Controls.Add(this.txtRunningCard);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 17);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(559, 74);
            this.panel1.TabIndex = 0;
            // 
            // ultraCheckEditorRMA
            // 
            this.ultraCheckEditorRMA.Location = new System.Drawing.Point(12, 52);
            this.ultraCheckEditorRMA.Name = "ultraCheckEditorRMA";
            this.ultraCheckEditorRMA.Size = new System.Drawing.Size(79, 16);
            this.ultraCheckEditorRMA.TabIndex = 11;
            this.ultraCheckEditorRMA.Text = "RMA维修";
            // 
            // ultraCheckEditorMisjudge
            // 
            this.ultraCheckEditorMisjudge.Location = new System.Drawing.Point(135, 55);
            this.ultraCheckEditorMisjudge.Name = "ultraCheckEditorMisjudge";
            this.ultraCheckEditorMisjudge.Size = new System.Drawing.Size(68, 16);
            this.ultraCheckEditorMisjudge.TabIndex = 2;
            this.ultraCheckEditorMisjudge.Text = "误判";
            this.ultraCheckEditorMisjudge.Visible = false;
            this.ultraCheckEditorMisjudge.CheckedValueChanged += new System.EventHandler(this.ultraCheckEditorMisjudge_CheckedValueChanged);
            // 
            // txtAgentUser
            // 
            this.txtAgentUser.AllowEditOnlyChecked = true;
            this.txtAgentUser.AutoSelectAll = false;
            this.txtAgentUser.AutoUpper = true;
            this.txtAgentUser.Caption = "代录维修人员";
            this.txtAgentUser.Checked = false;
            this.txtAgentUser.EditType = UserControl.EditTypes.String;
            this.txtAgentUser.Location = new System.Drawing.Point(249, 13);
            this.txtAgentUser.MaxLength = 20;
            this.txtAgentUser.Multiline = false;
            this.txtAgentUser.Name = "txtAgentUser";
            this.txtAgentUser.PasswordChar = '\0';
            this.txtAgentUser.ReadOnly = false;
            this.txtAgentUser.ShowCheckBox = true;
            this.txtAgentUser.Size = new System.Drawing.Size(234, 24);
            this.txtAgentUser.TabIndex = 10;
            this.txtAgentUser.TabNext = false;
            this.txtAgentUser.Value = "";
            this.txtAgentUser.WidthType = UserControl.WidthTypes.Normal;
            this.txtAgentUser.XAlign = 350;
            // 
            // rCardEditor
            // 
            this.rCardEditor.ImageTransparentColor = System.Drawing.Color.Teal;
            this.rCardEditor.Location = new System.Drawing.Point(88, 16);
            this.rCardEditor.MaxLength = 100;
            this.rCardEditor.Name = "rCardEditor";
            this.rCardEditor.Size = new System.Drawing.Size(136, 21);
            this.rCardEditor.TabIndex = 1;
            this.rCardEditor.WordWrap = false;
            this.rCardEditor.TextChanged += new System.EventHandler(this.rCardEditor_TextChanged);
            this.rCardEditor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.rCardEditor_KeyPress);
            // 
            // txtRunningCard
            // 
            this.txtRunningCard.Location = new System.Drawing.Point(8, 16);
            this.txtRunningCard.Name = "txtRunningCard";
            this.txtRunningCard.Size = new System.Drawing.Size(100, 16);
            this.txtRunningCard.TabIndex = 0;
            this.txtRunningCard.Text = "产品序列号";
            this.txtRunningCard.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // groupBox
            // 
            this.groupBox.Controls.Add(this.btnConfirm);
            this.groupBox.Controls.Add(this.ucButtonExit);
            this.groupBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox.Location = new System.Drawing.Point(0, 171);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(565, 56);
            this.groupBox.TabIndex = 170;
            this.groupBox.TabStop = false;
            // 
            // btnConfirm
            // 
            this.btnConfirm.BackColor = System.Drawing.SystemColors.Control;
            this.btnConfirm.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnConfirm.BackgroundImage")));
            this.btnConfirm.ButtonType = UserControl.ButtonTypes.Confirm;
            this.btnConfirm.Caption = "确认";
            this.btnConfirm.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConfirm.Location = new System.Drawing.Point(120, 16);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(88, 22);
            this.btnConfirm.TabIndex = 8;
            this.btnConfirm.Click += new System.EventHandler(this.ucButton2_Click);
            // 
            // ucButtonExit
            // 
            this.ucButtonExit.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonExit.BackgroundImage")));
            this.ucButtonExit.ButtonType = UserControl.ButtonTypes.Exit;
            this.ucButtonExit.Caption = "退出";
            this.ucButtonExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonExit.Location = new System.Drawing.Point(253, 16);
            this.ucButtonExit.Name = "ucButtonExit";
            this.ucButtonExit.Size = new System.Drawing.Size(88, 22);
            this.ucButtonExit.TabIndex = 9;
            this.ucButtonExit.Visible = false;
            // 
            // FTSInputComplete
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(565, 227);
            this.Controls.Add(this.groupBox);
            this.Controls.Add(this.groupBox3);
            this.Name = "FTSInputComplete";
            this.Text = "维修完成";
            this.Load += new System.EventHandler(this.FTSInputComplete_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rCardEditor)).EndInit();
            this.groupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        #region event

        private void ultraCheckEditor1_CheckedValueChanged(object sender, EventArgs e)
        {
            if (ultraCheckEditorReflow.Checked == true)
            {
                if (ultraCheckEditor2.Checked == true)
                {
                    ultraCheckEditor2.Checked = false;
                }

                Messages messages = new Messages();
                if (rCardEditor.Text.Trim() == String.Empty)
                {
                    messages.Add(new UserControl.Message(MessageType.Error, "$CSError_RCard_CanNot_Empty"));
                    this.ultraCheckEditorReflow.Checked = false;
                    ApplicationRun.GetInfoForm().Add(messages);
                    return;
                }

                //Laws Lu,2005/09/16,修改	逻辑调整P4.8
                TSFacade tsFacade = new TSFacade(this.DataProvider);
                //转换成起始序列号
                DataCollectFacade dataCollectFacade = new DataCollectFacade(this.DataProvider);
                string sourceCard = dataCollectFacade.GetSourceCard(rCardEditor.Value.ToString().Trim().ToUpper(), string.Empty);
                //end
                object obj = tsFacade.GetCardLastTSRecord(sourceCard);

                if (obj == null)
                {
                    messages.Add(new UserControl.Message(MessageType.Error, "$CSError_Card_Not_In_TS"));

                    ApplicationRun.GetInfoForm().Add(messages);
                    return;
                }
                else
                {
                    Domain.TS.TS ts = (Domain.TS.TS)obj;

                    if (ts.TSStatus == TSStatus.TSStatus_Scrap
                        || ts.TSStatus == TSStatus.TSStatus_Split
                        || ts.TSStatus == TSStatus.TSStatus_Reflow)
                    {
                        messages.Add(new UserControl.Message(MessageType.Error, "$CSError_Card_TSStatus_IsNot_TS $Current_Status $" + ts.TSStatus));

                        ApplicationRun.GetInfoForm().Add(messages);
                        return;
                    }
                    if (ts.TSStatus != TSStatus.TSStatus_TS
                        && !(ts.TSStatus == TSStatus.TSStatus_Confirm || (ultraCheckEditorMisjudge.Checked && ts.TSStatus == TSStatus.TSStatus_New)))
                    {
                        messages.Add(new UserControl.Message(MessageType.Error, "$CSError_Card_TSStatus_IsNot_TS $Current_Status $" + ts.TSStatus));
                        this.ultraCheckEditorReflow.Checked = false;
                        ApplicationRun.GetInfoForm().Add(messages);
                        return;
                    }
                    else
                    {
                        SetRaflowPanel(obj);
                    }
                }
                //				object obj = tsFacade.GetCardLastTSRecordInTSStatus(rCardEditor.Text.ToUpper().Trim());
                //				if(obj == null)
                //				{
                //					messages.Add(new UserControl. Message(MessageType.Error,"$CSError_Card_TSStatus_IsNot_TS"));
                //					this.ultraCheckEditor1.Checked = false ;
                //					ApplicationRun.GetInfoForm().Add(messages);
                //					return;
                //				}
                //				else
                //				{
                //					SetRaflowPanel(obj);
                //				}

            }
            else
            {
                ClearReflowPanel();
            }
        }

        private void ultraCheckEditor2_CheckedValueChanged(object sender, EventArgs e)
        {
            if (ultraCheckEditor2.Checked == true)
            {
                if (ultraCheckEditorReflow.Checked == true)
                {
                    ultraCheckEditorReflow.Checked = false;
                }

                ClearReflowPanel();

                Messages messages = new Messages();
                if (rCardEditor.Text.Trim() == String.Empty)
                {
                    messages.Add(new UserControl.Message(MessageType.Error, "$CSError_RCard_CanNot_Empty"));
                    this.ultraCheckEditor2.Checked = false;
                    ApplicationRun.GetInfoForm().Add(messages);
                    return;
                }
            }
        }

        private void ucButton2_Click(object sender, EventArgs e)
        {
            Messages messages = new Messages();

            TSFacade tsFacade = new TSFacade(this.DataProvider);
            TSModelFacade tsModelFacade = new TSModelFacade(this.DataProvider);
            ShiftModelFacade shiftModelFacade = new ShiftModelFacade(this.DataProvider);
            BaseModelFacade baseModelFacade = new BaseModelFacade(this.DataProvider);

            DataCollectFacade dataCollectFacade = new DataCollectFacade(this.DataProvider);
            string sourceRCard = dataCollectFacade.GetSourceCard(rCardEditor.Text.Trim().ToUpper(), string.Empty);

            if (rCardEditor.Text.Trim() == String.Empty)
            {
                messages.Add(new UserControl.Message(MessageType.Error, "$CSError_RCard_CanNot_Empty"));
                ApplicationRun.GetInfoForm().Add(messages);
                return;
            }

            string tsStatus;
            //Laws Lu,2005/11/22,新增	添加代理录入人员
            //modified by jessie lee, 2005/11/24
            string userCode = ApplicationService.Current().UserCode;
            if (txtAgentUser.Checked == true && txtAgentUser.Value.Trim().Length == 0)
            {
                messages.Add(new UserControl.Message(MessageType.Error, "$CS_Error_Vicegerent_CanNot_Empty"));
                ApplicationRun.GetInfoForm().Add(messages);
                return;
            }
            else if (txtAgentUser.Checked == true && txtAgentUser.Value != String.Empty)
            {
                if ((new BenQGuru.eMES.Security.SecurityFacade(this.DataProvider)).CheckResourceRight(txtAgentUser.Value.Trim().ToUpper()
                    , ApplicationService.Current().ResourceCode))
                {
                    userCode = txtAgentUser.Value;
                }
                else
                {
                    messages.Add(new UserControl.Message(MessageType.Error, "$CS_Error_Vicegerent_Is_Wrong"));
                    ApplicationRun.GetInfoForm().Add(messages);
                    return;
                }
            }



            #region 误判时需要做的前期准备（前期判断、模拟一次TS、一次TSConfirm）

            //误判时需要做的前期准备（前期判断、模拟一次TS、一次TSConfirm）

            bool autoTSConfirm = false;
            ActionEventArgs autoTSConfirmActionEventArgs = null;

            Domain.TS.TS misjudgeTS = null;
            object[] oldTSErrorCodes = null;
            object[] oldTSErrorCauses = null;
            TSErrorCode newTSErrorCode = null;
            TSErrorCause newTSErrorCause = null;

            if (ultraCheckEditorMisjudge.Checked)
            {
                //Check ts status
                misjudgeTS = (Domain.TS.TS)tsFacade.GetCardLastTSRecord(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(sourceRCard)));
                if (misjudgeTS == null)
                {
                    messages.Add(new UserControl.Message(MessageType.Error, "$CSError_Card_Not_In_TS"));
                    ApplicationRun.GetInfoForm().Add(messages);
                    return;
                }
                if (misjudgeTS.TSStatus == TSStatus.TSStatus_New)
                {
                    //messages.Add(new UserControl.Message(MessageType.Error, "$CSError_Card_TSStatus_Is_New"));
                    //ApplicationRun.GetInfoForm().Add(messages);
                    //return;
                    autoTSConfirmActionEventArgs = new ActionEventArgs(
                        ActionType.DataCollectAction_TSConfirm,
                        FormatHelper.PKCapitalFormat(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(sourceRCard))),
                        userCode,
                        ApplicationService.Current().ResourceCode,
                        ApplicationService.Current().UserCode);

                    autoTSConfirm = true;

                }
                if (misjudgeTS.TSStatus == TSStatus.TSStatus_TS)
                {
                    messages.Add(new UserControl.Message(MessageType.Error, "$CSError_Card_TSStatus_Is_TS"));
                    ApplicationRun.GetInfoForm().Add(messages);
                    return;
                }
                if (misjudgeTS.TSStatus != TSStatus.TSStatus_Confirm && misjudgeTS.TSStatus != TSStatus.TSStatus_New)
                {
                    messages.Add(new UserControl.Message(MessageType.Error, "$CSError_Card_TSStatus_IsNot_Confirm"));
                    ApplicationRun.GetInfoForm().Add(messages);
                    return;
                }

                //Get parameters for misjudge
                SystemSettingFacade systemSettingFacade = new SystemSettingFacade(this.DataProvider);
                Parameter errCodeParameter = (Parameter)systemSettingFacade.GetParameter("ERRCODE", "TSMISJUDGE");
                Parameter errCauseParameter = (Parameter)systemSettingFacade.GetParameter("ERRCAUSE", "TSMISJUDGE");
                Parameter errCauseGroupParameter = (Parameter)systemSettingFacade.GetParameter("ERRCAUSEGROUP", "TSMISJUDGE");
                Parameter errDutyParameter = (Parameter)systemSettingFacade.GetParameter("ERRDUTY", "TSMISJUDGE");
                Parameter errSolutionParameter = (Parameter)systemSettingFacade.GetParameter("ERRSOLUTION", "TSMISJUDGE");
                if (errCodeParameter == null)
                {
                    messages.Add(new UserControl.Message(MessageType.Error,
                        "$CS_TSMisJudgeParameter_NotComplete $PageControl_ParameterCodeQuery : " + "ERRCODE"));
                    ApplicationRun.GetInfoForm().Add(messages);
                    return;
                }
                if (errCauseParameter == null)
                {
                    messages.Add(new UserControl.Message(MessageType.Error,
                        "$CS_TSMisJudgeParameter_NotComplete $PageControl_ParameterCodeQuery : " + "ERRCAUSE"));
                    ApplicationRun.GetInfoForm().Add(messages);
                    return;
                }
                if (errCauseGroupParameter == null)
                {
                    messages.Add(new UserControl.Message(MessageType.Error,
                        "$CS_TSMisJudgeParameter_NotComplete $PageControl_ParameterCodeQuery : " + "ERRCAUSEGROUP"));
                    ApplicationRun.GetInfoForm().Add(messages);
                    return;
                }
                if (errDutyParameter == null)
                {
                    messages.Add(new UserControl.Message(MessageType.Error,
                        "$CS_TSMisJudgeParameter_NotComplete $PageControl_ParameterCodeQuery : " + "ERRDUTY"));
                    ApplicationRun.GetInfoForm().Add(messages);
                    return;
                }
                if (errSolutionParameter == null)
                {
                    messages.Add(new UserControl.Message(MessageType.Error,
                        "$CS_TSMisJudgeParameter_NotComplete $PageControl_ParameterCodeQuery : " + "ERRSOLUTION"));
                    ApplicationRun.GetInfoForm().Add(messages);
                    return;
                }

                ErrorCodeGroup2ErrorCode errorCodeGroup2ErrorCode = (ErrorCodeGroup2ErrorCode)tsModelFacade.GetErrorCodeGroup2ErrorCodeByecCode(errCodeParameter.ParameterAlias);

                if (errorCodeGroup2ErrorCode == null)
                {
                    messages.Add(new UserControl.Message(MessageType.Error, "$CS_TSMisJudgeParameter_NotComplete : " + "ERRCODE"));
                    ApplicationRun.GetInfoForm().Add(messages);
                    return;
                }

                //Get old error code and error cause
                oldTSErrorCodes = tsFacade.QueryTSErrorCode(string.Empty, string.Empty, misjudgeTS.TSId, int.MinValue, int.MaxValue);
                oldTSErrorCauses = tsFacade.QueryTSErrorCause(misjudgeTS.TSId, int.MinValue, int.MaxValue);

                DBDateTime dbDateTime;
                dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);

                //Get new error code
                newTSErrorCode = tsFacade.CreateNewTSErrorCode();
                newTSErrorCode.TSId = misjudgeTS.TSId;
                newTSErrorCode.RunningCard = misjudgeTS.RunningCard;
                newTSErrorCode.RunningCardSequence = misjudgeTS.RunningCardSequence;
                newTSErrorCode.ModelCode = misjudgeTS.ModelCode;
                newTSErrorCode.ItemCode = misjudgeTS.ItemCode;
                newTSErrorCode.MOCode = misjudgeTS.MOCode;
                newTSErrorCode.MOSeq = misjudgeTS.MOSeq;

                newTSErrorCode.ErrorCode = errCodeParameter.ParameterAlias;
                newTSErrorCode.ErrorCodeGroup = errorCodeGroup2ErrorCode.ErrorCodeGroup;

                newTSErrorCode.MaintainUser = userCode;
                newTSErrorCode.MaintainDate = dbDateTime.DBDate;
                newTSErrorCode.MaintainTime = dbDateTime.DBTime;


                //Get new error cause
                newTSErrorCause = tsFacade.CreateNewTSErrorCause();
                newTSErrorCause.TSId = newTSErrorCode.TSId;
                newTSErrorCause.RunningCard = misjudgeTS.RunningCard;
                newTSErrorCause.RunningCardSequence = misjudgeTS.RunningCardSequence;
                newTSErrorCause.ModelCode = misjudgeTS.ModelCode;
                newTSErrorCause.ItemCode = misjudgeTS.ItemCode;
                newTSErrorCause.MOCode = misjudgeTS.MOCode;
                newTSErrorCause.MOSeq = misjudgeTS.MOSeq;

                newTSErrorCause.ErrorCode = newTSErrorCode.ErrorCode;
                newTSErrorCause.ErrorCodeGroup = newTSErrorCode.ErrorCodeGroup;
                newTSErrorCause.ErrorCauseGroupCode = errCauseGroupParameter.ParameterAlias;
                newTSErrorCause.ErrorCauseCode = errCauseParameter.ParameterAlias;
                newTSErrorCause.DutyCode = errDutyParameter.ParameterAlias;
                newTSErrorCause.SolutionCode = errSolutionParameter.ParameterAlias;

                newTSErrorCause.MaintainUser = userCode;
                newTSErrorCause.MaintainDate = dbDateTime.DBDate;
                newTSErrorCause.MaintainTime = dbDateTime.DBTime;

                newTSErrorCause.RepairResourceCode = ApplicationService.Current().ResourceCode;
                newTSErrorCause.RepairOPCode = OPType.TS;

                Resource res = (Resource)baseModelFacade.GetResource(ApplicationService.Current().ResourceCode);
                DateTime workDateTime = FormatHelper.ToDateTime(dbDateTime.DBDate, dbDateTime.DBTime);
                TimePeriod period = (TimePeriod)shiftModelFacade.GetTimePeriod(res.ShiftTypeCode, Web.Helper.FormatHelper.TOTimeInt(workDateTime));
                if (period == null)
                {
                    throw new Exception("$OutOfPerid");
                }

                if (period.IsOverDate == Web.Helper.FormatHelper.TRUE_STRING)
                {
                    if (period.TimePeriodBeginTime < period.TimePeriodEndTime)
                    {
                        newTSErrorCause.TSShiftDay = FormatHelper.TODateInt(workDateTime.AddDays(-1));
                    }
                    else if (Web.Helper.FormatHelper.TOTimeInt(workDateTime) < period.TimePeriodBeginTime)
                    {
                        newTSErrorCause.TSShiftDay = FormatHelper.TODateInt(workDateTime.AddDays(-1));
                    }
                    else
                    {
                        newTSErrorCause.TSShiftDay = FormatHelper.TODateInt(workDateTime);
                    }
                }
                else
                {
                    newTSErrorCause.TSShiftDay = FormatHelper.TODateInt(workDateTime);
                }

                //Set ts type
                misjudgeTS.TSType = TSType.TS_Misjudge;
                misjudgeTS.TSStatus = TSStatus.TSStatus_TS;
            }

            #endregion

            if (ultraCheckEditorReflow.Checked == true)
            {
                tsStatus = TSStatus.TSStatus_Reflow;

                //勾选回流，然后不工位栏位未选资料也可允许通过。在业务上回流是一定有确定的工位的。
                //系统要检查此时保存，工位是否为空。
                if (OPCode.SelectedItemText == string.Empty)
                {
                    messages.Add(new UserControl.Message(MessageType.Error, "$CS_Error_ReflowOPCode_CanNot_Empty"));
                    ApplicationRun.GetInfoForm().Add(messages);
                    OPCode.Focus();
                    return;
                }
            }
            else if (ultraCheckEditor2.Checked == true)
            {
                tsStatus = TSStatus.TSStatus_Scrap;
            }
            else
            {

                tsStatus = TSStatus.TSStatus_Complete;
                //				//修改,Karron Qiu 2005-9-23
                //				//线上采集的不良品，修复后则“回流”；修不好则“报废”。而不选“回流”或“报废”代表该产品要入了良品库，
                //				//线上的不良品修好后是直接回产线回流，不需要入良品库的
                //				messages.Add(new UserControl.Message(MessageType.Error,"$CSError_Please_Select_Reflow_OR_Scrap"));//请选择回流或者报废
                //				ApplicationRun.GetInfoForm().Add(messages);
                //				return;
            }

            ActionFactory actionFactory = new ActionFactory(this.DataProvider);
            IAction actionTSComplete = actionFactory.CreateAction(ActionType.DataCollectAction_TSComplete);
            TSActionEventArgs actionEventArgs = new TSActionEventArgs(
                ActionType.DataCollectAction_TSComplete,
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(sourceRCard)),
                userCode,
                ApplicationService.Current().ResourceCode,
                tsStatus,
                this.ucLabelEditMOCode.Value,
                this.ucLabelEditItemCode.Value,
                this.Route.SelectedItemText,
                this.OPCode.SelectedItemText,
                ApplicationService.Current().UserCode,
                FormatHelper.CleanString(this.txtScrapCause.Text, 100));


            //karron qiu ,2005/9/16 ,增加try catch,在catch中添加rollback操作
            DataProvider.BeginTransaction();
            try
            {

                #region 误判时需要做的DB动作
                //误判时需要做的DB动作
                if (ultraCheckEditorMisjudge.Checked)
                {
                    if (autoTSConfirm && autoTSConfirmActionEventArgs != null)
                    {
                        IAction actionTSCofirm = actionFactory.CreateAction(ActionType.DataCollectAction_TSConfirm);
                        messages.AddMessages(actionTSCofirm.Execute(autoTSConfirmActionEventArgs));
                    }

                    if (oldTSErrorCodes != null)
                    {
                        foreach (TSErrorCode old in oldTSErrorCodes)
                        {
                            tsFacade.DeleteTSErrorCode(old);
                        }
                    }
                    if (oldTSErrorCauses != null)
                    {
                        foreach (TSErrorCause old in oldTSErrorCauses)
                        {
                            tsFacade.DeleteTSErrorCauseWithNoTrans(old);
                        }
                    }
                    tsFacade.AddTSErrorCode(newTSErrorCode);
                    tsFacade.AddTSErrorCause(userCode, newTSErrorCause);
                    tsFacade.UpdateTS(misjudgeTS);

                    #region 拆箱、拆栈板
                    //DataCollectFacade dataCollectFacade = new DataCollectFacade(this.DataProvider);
                    if (messages.IsSuccess())
                    {
                        messages.AddMessages(dataCollectFacade.RemoveFromCarton(sourceRCard.Trim().ToUpper(), ApplicationService.Current().UserCode));
                    }

                    if (messages.IsSuccess())
                    {
                        messages.AddMessages(dataCollectFacade.RemoveFromPallet(sourceRCard.Trim().ToUpper(), ApplicationService.Current().UserCode,true));
                    }

                    #endregion
                }
                #endregion

                //修改 Karron Qiu 2005-9-26
                //在做维修完成处理时，依然按照之前的检查逻辑
                //（不良品是否“已选不良零件”或“已选不良位置”有信息），如果没有，则弹出提示信息，
                //比如：“该不良品无“不良零件”或“不良位置”信息，是否要维修完成”，
                //点击“确认”即维修完成，点击“取消”则维修完成失败
                //Laws Lu,2007/01/05 将OpenClose的次数降低
                object obj = tsFacade.GetCardLastTSRecord(actionEventArgs.RunningCard);

                if (obj == null)
                {
                    messages.Add(new UserControl.Message(MessageType.Error, "$CSError_Card_Not_In_TS"));
                    ApplicationRun.GetInfoForm().Add(messages);
                    this.DataProvider.RollbackTransaction();
                    return;
                }
                else
                {
                    Domain.TS.TS ts = (Domain.TS.TS)obj;
                    if (tsFacade.CheckErrorCodeCountAndErrorSolutionForTSComplete(actionEventArgs.RunningCard))
                    {
                        if (!tsFacade.CheckErrorPartAndErrorLocationForTSComplete(actionEventArgs.RunningCard))
                        {
                            if (System.Windows.Forms.MessageBox.Show(null, MutiLanguages.ParserString("$CS_No_ErrorPartAndLoc_Is_Com"), "", System.Windows.Forms.MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.Cancel)
                            {
                                this.DataProvider.RollbackTransaction();
                                return;
                            }
                        }
                    }
                    else
                    {
                        messages.Add(new UserControl.Message(MessageType.Error, "$CSError_Card_TS_CanNot_Complete $Current_Status $" + ts.TSStatus));
                        ApplicationRun.GetInfoForm().Add(messages);
                        this.DataProvider.RollbackTransaction();
                        return;
                    }
                }

                messages.AddMessages(actionTSComplete.Execute(actionEventArgs));
                if (!messages.IsSuccess())
                {
                    this.DataProvider.RollbackTransaction();
                    ApplicationRun.GetInfoForm().Add(messages);
                }
                else
                {
                    this.DataProvider.CommitTransaction();
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Success, "$CS_TSComplete_SUCCESS"));
                    this.ClearInfoWhenTSSuccess();
                }
            }
            catch
            {
                this.DataProvider.RollbackTransaction();
                throw;
            }
            finally
            {
                (DataProvider as SQLDomainDataProvider).PersistBroker.CloseConnection();
            }
        }

        //维修完成后界面信息清空
        private void ClearInfoWhenTSSuccess()
        {
            this.rCardEditor.Text = string.Empty;
            //this.txtAgentUser.Text = string.Empty;
            this.ucLabelEditMOCode.Text = string.Empty;
            this.ucLabelEditItemCode.Text = string.Empty;
            this.Route.Clear();

            //this.txtAgentUser.Checked = false;
            this.ultraCheckEditorReflow.Checked = false;
            this.ultraCheckEditor2.Checked = false;
            this.ultraCheckEditorMisjudge.Checked = false;

            this.OPCode.Clear();
            this.rCardEditor.Focus();
        }

        private void rCardEditor_TextChanged(object sender, EventArgs e)
        {
            ClearReflowPanel();
            this.ultraCheckEditorReflow.Checked = false;
            this.ultraCheckEditor2.Checked = false;
            this.ultraCheckEditorMisjudge.Checked = false;
        }
        private void rCardEditor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                Messages messages = new Messages();
                if (rCardEditor.Text.Trim() == String.Empty)
                {
                    messages.Add(new UserControl.Message(MessageType.Error, "$CSError_RCard_CanNot_Empty"));
                    ApplicationRun.GetInfoForm().Add(messages);
                    return;
                }

                DataCollectFacade dataCollectFacade = new DataCollectFacade(this.DataProvider);
                string sourceRCard = dataCollectFacade.GetSourceCard(rCardEditor.Text.Trim().ToUpper(), string.Empty);

                //Laws Lu,2005/08/25,新增	检查当前资源是否为TS站
                ActionOnLineHelper dataCollect = new ActionOnLineHelper(this._domainDataProvider);
                ActionEventArgs actionEventArgs = new ActionEventArgs(
                    ActionType.DataCollectAction_TSConfirm,
                    FormatHelper.PKCapitalFormat(FormatHelper.CleanString(sourceRCard)),
                    ApplicationService.Current().UserCode,
                    ApplicationService.Current().ResourceCode);

                messages.AddMessages(dataCollect.CheckResourceInOperationTS(actionEventArgs));
                //End Laws LU
                if (!messages.IsSuccess())
                {
                    ApplicationRun.GetInfoForm().Add(messages);
                    return;
                }

                //				TSFacade tsFacade = new TSFacade(this.DataProvider);
                //				if(!tsFacade.IsCardInTS(rCardEditor.Text.ToUpper().Trim()))
                //				{
                //					messages.Add(new UserControl.Message (MessageType.Error,"$CSError_Card_Not_In_TS"));
                //					ApplicationRun.GetInfoForm().Add(messages);
                //					return;
                //				}
                //
                //				object obj = tsFacade.GetCardLastTSRecordInTSStatus(rCardEditor.Text.ToUpper().Trim());
                //				if(obj == null)
                //				{
                //					messages.Add(new UserControl. Message(MessageType.Error,"$CSError_Card_TSStatus_IsNot_TS $Current_Status $"+ts.TSStatus));
                //					ApplicationRun.GetInfoForm().Add(messages);
                //					return;
                //				}
                //				BenQGuru.eMES.Domain.TS.TS ts = (BenQGuru.eMES.Domain.TS.TS)obj;
                TSFacade tsFacade = new TSFacade(this.DataProvider);

                //Laws Lu,2005/09/16,修改	逻辑调整P4.8
                object obj = tsFacade.GetCardLastTSRecord(actionEventArgs.RunningCard);

                Domain.TS.TS ts = null;
                if (obj == null)
                {
                    messages.Add(new UserControl.Message(MessageType.Error, "$CSError_Card_Not_In_TS"));
                    ApplicationRun.GetInfoForm().Add(messages);
                    return;
                }
                else
                {
                    ts = (Domain.TS.TS)obj;

                    if (ts.TSStatus == TSStatus.TSStatus_Scrap
                        || ts.TSStatus == TSStatus.TSStatus_Split
                        || ts.TSStatus == TSStatus.TSStatus_Reflow)
                    {
                        messages.Add(new UserControl.Message(MessageType.Error, "$CSError_Card_TSStatus_IsNot_TS $Current_Status $" + ts.TSStatus));

                        ApplicationRun.GetInfoForm().Add(messages);
                        return;
                    }
                    if (ts.TSStatus != TSStatus.TSStatus_TS
                        && !(ts.TSStatus == TSStatus.TSStatus_Confirm || (ultraCheckEditorMisjudge.Checked && ts.TSStatus == TSStatus.TSStatus_New)))
                    {
                        messages.Add(new UserControl.Message(MessageType.Error, "$CSError_Card_TSStatus_IsNot_TS $Current_Status $" + ts.TSStatus));
                        ApplicationRun.GetInfoForm().Add(messages);
                        return;
                    }
                }

                if (ts.FromInputType == TSFacade.TSSource_RMA)
                {
                    this.ultraCheckEditorRMA.Enabled = true;
                    this.ultraCheckEditorRMA.Checked = true;
                    ultraCheckEditorReflow.Enabled = false;
                    ultraCheckEditorReflow.Checked = false;
                }
                else
                {
                    ultraCheckEditorReflow.Enabled = true;
                    ultraCheckEditorReflow.Checked = true;
                    ultraCheckEditorRMA.Enabled = false;
                    ultraCheckEditorRMA.Checked = false;
                }
            }
        }
        #endregion

        private void ultraCheckEditor2_CheckedChanged(object sender, System.EventArgs e)
        {
            if (ultraCheckEditor2.Checked)
            {
                this.txtScrapCause.ReadOnly = false;
                this.txtScrapCause.Text = string.Empty;
            }
            else
            {
                this.txtScrapCause.ReadOnly = true;
                this.txtScrapCause.Text = string.Empty;
            }
        }

        #region Private function

        private void SetRaflowPanel(object obj)
        {
            ClearReflowPanel();
            this.ucLabelEditMOCode.Value = ((BenQGuru.eMES.Domain.TS.TS)obj).MOCode;
            this.ucLabelEditItemCode.Value = ((BenQGuru.eMES.Domain.TS.TS)obj).ItemCode;

            #region Marked By Hi1/venus.Feng on 20080716 for Hisense Version
            /*
            if (((BenQGuru.eMES.Domain.TS.TS)obj).FromRouteCode != string.Empty)
            {
                this.ucLabEdit3.Value = ((BenQGuru.eMES.Domain.TS.TS)obj).FromRouteCode;
            }
            else
            {
                TSFacade tsFacade = new TSFacade(this.DataProvider);
                BenQGuru.eMES.Domain.DataCollect.Simulation simulation = tsFacade.GetSimulation(((BenQGuru.eMES.Domain.TS.TS)obj).RunningCard, ((BenQGuru.eMES.Domain.TS.TS)obj).MOCode);
                if (simulation == null)
                {
                    Messages message = new Messages();
                    message.Add(new UserControl.Message(MessageType.Error, "$CSError_Card_HasNot_RouteCode"));
                    ApplicationRun.GetInfoForm().Add(message);
                    ClearReflowPanel();
                    this.ultraCheckEditor1.Checked = false;
                    return;
                }
                else
                {
                    this.ucLabEdit3.Value = simulation.FromRoute;
                }
            }


			BenQGuru.eMES.MOModel.ItemFacade  itemFacade = new BenQGuru.eMES.MOModel.ItemFacade(this.DataProvider);
			
			
			BenQGuru.eMES.Domain.MOModel.Model model= new BenQGuru.eMES.MOModel.ModelFacade(this.DataProvider).GetModelByItemCode(this.ucLabEdit2.Value);
			//如果产品别设定了使用回流，则根据不良原因组找到回流工序，如果没设，则把route下的所有的工序列出来
			if(model == null || model.IsReflow != BenQGuru.eMES.Web.Helper.FormatHelper.TRUE_STRING)
			{
				object[] item2Op = itemFacade.QueryItem2Operation(this.ucLabEdit2.Value, this.ucLabEdit3.Value);
				if(item2Op == null)
				{
					return;
				}
				else
				{
					for(int i=0 ; i <item2Op.Length ; i++)
					{
						this.ucLabCombox2.AddItem( ((BenQGuru.eMES.Domain.MOModel.ItemRoute2OP)item2Op[i]).OPCode,((BenQGuru.eMES.Domain.MOModel.ItemRoute2OP)item2Op[i]).OPCode);
					}
				}
			}
			else	
			{
				this.ucLabCombox2.Clear();
				//根据产品，途程，和不良代码组取得回流工序
				string item = this.ucLabEdit2.Value;
				string route = this.ucLabEdit3.Value;
				
				//找到维修中所有的不良原因组
				TSFacade facade = new TSFacade(this.DataProvider);
				object[] objs = facade.QueryTSErrorCause(((BenQGuru.eMES.Domain.TS.TS)obj).TSId,1,int.MaxValue);
				if(objs == null) 
				{
					Messages message = new Messages();
					message.Add(new UserControl.Message(MessageType.Error,"$CSError_CauseGroup_No"));
					ApplicationRun.GetInfoForm().Add(message);
					return;
				}

				//把重复的不良原因组去年
				TSModelFacade modelFacade = new TSModelFacade(this.DataProvider);
				ArrayList egList = new ArrayList();
				if(objs.Length > 0)
				{
					foreach(TSErrorCause errorCase in objs)
					{
						if(!egList.Contains(errorCase.ErrorCauseGroupCode))
							egList.Add(errorCase.ErrorCauseGroupCode);
					}
				}

				//如果有多个工序可用，则取序号最小的工序
				BenQGuru.eMES.MOModel.ItemFacade opfacade = new BenQGuru.eMES.MOModel.ItemFacade(this.DataProvider);
				string opstr = null;
				int minOpSeq = int.MaxValue;
				for(int i=0;i<egList.Count;i++)
				{
					string eg = egList[i] as String;

					object[] objs2= modelFacade.QueryItemRouteOp2ErrorCauseGroup(item,route,eg,1,int.MaxValue);
					if(objs2 != null && objs2.Length > 0)
					{
						BenQGuru.eMES.Domain.TSModel.ItemRouteOp2ErrorCauseGroup ig = objs2[0] as BenQGuru.eMES.Domain.TSModel.ItemRouteOp2ErrorCauseGroup;
						string opid = ig.OpID;

						BenQGuru.eMES.Domain.MOModel.ItemRoute2OP op = opfacade.GetItemRoute2Op(opid, GlobalVariables.CurrentOrganizations.First().OrganizationID) as BenQGuru.eMES.Domain.MOModel.ItemRoute2OP;
						//找到序号最小的ID
						if(op != null && op.OPSequence < minOpSeq)
						{
							minOpSeq = (int)op.OPSequence;
							opstr = op.OPCode;
						}
					}
				}
				
				if(opstr != null)
				{
					this.ucLabCombox2.AddItem(opstr,opstr);
					this.ucLabCombox2.SelectedIndex = 0;
				}

				if(this.ucLabCombox2.ComboBoxData.Items.Count ==0) //如果没有回流工序，则提示用户 1.可能是维修时不良原因组选择错误。2.没有设定产品途程的不良原因的回流工序
				{
					Messages message = new Messages();
					message.Add(new UserControl.Message(MessageType.Error,"$CSError_CauseGroup_No"));
					ApplicationRun.GetInfoForm().Add(message);
					return;
				}
			}
            */

            #endregion

            //产品的所有途程
            BenQGuru.eMES.MOModel.ItemFacade itemFacade = new BenQGuru.eMES.MOModel.ItemFacade(this.DataProvider);
            BenQGuru.eMES.Domain.MOModel.Item item_check = itemFacade.GetItem(((BenQGuru.eMES.Domain.TS.TS)obj).ItemCode, GlobalVariables.CurrentOrganizations.First().OrganizationID) as BenQGuru.eMES.Domain.MOModel.Item;
            object[] objs = null;
            this.Route.Clear();

            if (item_check != null)
            {
                objs = itemFacade.QueryItem2Route(item_check.ItemCode, string.Empty, GlobalVariables.CurrentOrganizations.First().OrganizationID.ToString());
            }
            if (objs != null && objs.Length > 0)
            {
                foreach (BenQGuru.eMES.Domain.MOModel.Item2Route ir in objs)
                {
                    Route.AddItem(ir.RouteCode, ir.RouteCode);
                }
            }

            try
            {
                Route.SetSelectItemText(((Domain.TS.TS)obj).FromRouteCode);
                OPCode.SetSelectItemText(((Domain.TS.TS)obj).FromOPCode);
            }
            catch
            {
                Route.SelectedIndex = 0;
            }
        }

        private void ClearReflowPanel()
        {
            this.ucLabelEditMOCode.Value = String.Empty;
            this.ucLabelEditItemCode.Value = String.Empty;
            this.Route.Clear();
            this.OPCode.Clear();
        }

        #endregion

        private void ucLabComboxRoute_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.OPCode.Clear();
            BenQGuru.eMES.MOModel.ItemFacade itemFacade = new BenQGuru.eMES.MOModel.ItemFacade(this.DataProvider);
            object[] objs = itemFacade.QueryItem2Operation(this.ucLabelEditItemCode.Value, this.Route.SelectedItemText);

            if (objs != null && objs.Length > 0)
            {
                foreach (BenQGuru.eMES.Domain.MOModel.ItemRoute2OP iro in objs)
                {
                    if (iro.OPControl.Substring(4, 1) == "1")  // remove oqc and the continued op
                    {
                        break;
                    }
                    this.OPCode.AddItem(iro.OPCode, iro.OPCode);
                }

                BenQGuru.eMES.DataCollect.DataCollectFacade dc = new DataCollectFacade(this.DataProvider);
                try
                {
                    BenQGuru.eMES.Domain.MOModel.ItemRoute2OP op = dc.GetMORouteFirstOP(this.ucLabelEditMOCode.Value, this.Route.SelectedItemText);
                    if (op != null)
                    {
                        this.OPCode.SetSelectItemText(op.OPCode);
                    }
                }
                catch (System.Exception ex)
                {
                    ApplicationRun.GetInfoForm().Add(ex.Message);
                }
            }
        }

        private void ultraCheckEditorMisjudge_CheckedValueChanged(object sender, EventArgs e)
        {
            if (ultraCheckEditorMisjudge.Checked)
            {
                Messages messages = new Messages();
                if (rCardEditor.Text.Trim() == String.Empty)
                {
                    messages.Add(new UserControl.Message(MessageType.Error, "$CSError_RCard_CanNot_Empty"));
                    this.ultraCheckEditorMisjudge.Checked = false;
                    ApplicationRun.GetInfoForm().Add(messages);
                    return;
                }
            }

            ultraCheckEditorReflow.Checked = ultraCheckEditorMisjudge.Checked;
        }

        private void FTSInputComplete_Load(object sender, EventArgs e)
        {
            rCardEditor.Focus();
            //this.InitPageLanguage();
        }
    }
}
