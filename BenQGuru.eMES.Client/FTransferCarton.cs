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
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Domain.Package;
using BenQGuru.eMES.Package;
using BenQGuru.eMES.Material;
#endregion


namespace BenQGuru.eMES.Client
{
    public class FTransferCarton : BaseForm
    {
        private UserControl.UCLabelEdit ucLabEdit2;
        private System.ComponentModel.IContainer components = null;
        private UserControl.UCLabelEdit ucLabelEdit1;
        private System.Windows.Forms.Panel panel1;
        private UserControl.UCLabelEdit txtCartonNO;
        private System.Windows.Forms.Panel panel2;
        private UserControl.UCButton uBtnExit;
        private UserControl.UCLabelEdit txtRCard;
        private UserControl.UCButton btnConfirm;
        private System.Windows.Forms.Panel panel4;
        private UserControl.UCMessage ucMessage;
        private UserControl.UCLabelEdit txtItemCode;
        private UserControl.UCLabelEdit txtMoCode;
        private UserControl.UCLabelEdit txtOPCode;
        private UserControl.UCLabelEdit txtCurrentCarton;
        private UserControl.UCLabelEdit txtResCode;
        private bool m_NeedAddNewCarton = false;

        private IDomainDataProvider _domainDataProvider = ApplicationService.Current().DataProvider;
        public IDomainDataProvider DataProvider
        {
            get
            {
                return _domainDataProvider;
            }
        }

        public FTransferCarton()
        {
            // 该调用是 Windows 窗体设计器所必需的。
            InitializeComponent();

            UserControl.UIStyleBuilder.FormUI(this);
            //UserControl.UIStyleBuilder.GridUI(ultraGridMain);

            ucMessage.Add(new UserControl.Message(">>$CS_Please_Input_RunningCard"));

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FTransferCarton));
            this.ucLabEdit2 = new UserControl.UCLabelEdit();
            this.ucLabelEdit1 = new UserControl.UCLabelEdit();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtCurrentCarton = new UserControl.UCLabelEdit();
            this.txtResCode = new UserControl.UCLabelEdit();
            this.txtOPCode = new UserControl.UCLabelEdit();
            this.txtMoCode = new UserControl.UCLabelEdit();
            this.txtItemCode = new UserControl.UCLabelEdit();
            this.txtRCard = new UserControl.UCLabelEdit();
            this.txtCartonNO = new UserControl.UCLabelEdit();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnConfirm = new UserControl.UCButton();
            this.uBtnExit = new UserControl.UCButton();
            this.panel4 = new System.Windows.Forms.Panel();
            this.ucMessage = new UserControl.UCMessage();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // ucLabEdit2
            // 
            this.ucLabEdit2.AllowEditOnlyChecked = true;
            this.ucLabEdit2.AutoUpper = true;
            this.ucLabEdit2.Caption = "包装数量";
            this.ucLabEdit2.Checked = false;
            this.ucLabEdit2.EditType = UserControl.EditTypes.String;
            this.ucLabEdit2.Location = new System.Drawing.Point(161, -16);
            this.ucLabEdit2.MaxLength = 40;
            this.ucLabEdit2.Multiline = false;
            this.ucLabEdit2.Name = "ucLabEdit2";
            this.ucLabEdit2.PasswordChar = '\0';
            this.ucLabEdit2.ReadOnly = false;
            this.ucLabEdit2.ShowCheckBox = false;
            this.ucLabEdit2.Size = new System.Drawing.Size(194, 56);
            this.ucLabEdit2.TabIndex = 16;
            this.ucLabEdit2.TabNext = true;
            this.ucLabEdit2.Value = "";
            this.ucLabEdit2.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabEdit2.XAlign = 222;
            // 
            // ucLabelEdit1
            // 
            this.ucLabelEdit1.AllowEditOnlyChecked = true;
            this.ucLabelEdit1.AutoUpper = true;
            this.ucLabelEdit1.Caption = "输入标示";
            this.ucLabelEdit1.Checked = false;
            this.ucLabelEdit1.EditType = UserControl.EditTypes.String;
            this.ucLabelEdit1.Location = new System.Drawing.Point(358, 16);
            this.ucLabelEdit1.MaxLength = 40;
            this.ucLabelEdit1.Multiline = false;
            this.ucLabelEdit1.Name = "ucLabelEdit1";
            this.ucLabelEdit1.PasswordChar = '\0';
            this.ucLabelEdit1.ReadOnly = false;
            this.ucLabelEdit1.ShowCheckBox = false;
            this.ucLabelEdit1.Size = new System.Drawing.Size(194, 24);
            this.ucLabelEdit1.TabIndex = 1;
            this.ucLabelEdit1.TabNext = true;
            this.ucLabelEdit1.Value = "";
            this.ucLabelEdit1.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabelEdit1.XAlign = 419;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtCurrentCarton);
            this.panel1.Controls.Add(this.txtResCode);
            this.panel1.Controls.Add(this.txtOPCode);
            this.panel1.Controls.Add(this.txtMoCode);
            this.panel1.Controls.Add(this.txtItemCode);
            this.panel1.Controls.Add(this.txtRCard);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(720, 112);
            this.panel1.TabIndex = 285;
            // 
            // txtCurrentCarton
            // 
            this.txtCurrentCarton.AllowEditOnlyChecked = true;
            this.txtCurrentCarton.AutoUpper = true;
            this.txtCurrentCarton.BackColor = System.Drawing.Color.Gainsboro;
            this.txtCurrentCarton.Caption = "Carton号";
            this.txtCurrentCarton.Checked = false;
            this.txtCurrentCarton.EditType = UserControl.EditTypes.String;
            this.txtCurrentCarton.Location = new System.Drawing.Point(362, 16);
            this.txtCurrentCarton.MaxLength = 4000;
            this.txtCurrentCarton.Multiline = false;
            this.txtCurrentCarton.Name = "txtCurrentCarton";
            this.txtCurrentCarton.PasswordChar = '\0';
            this.txtCurrentCarton.ReadOnly = true;
            this.txtCurrentCarton.ShowCheckBox = false;
            this.txtCurrentCarton.Size = new System.Drawing.Size(261, 24);
            this.txtCurrentCarton.TabIndex = 6;
            this.txtCurrentCarton.TabNext = false;
            this.txtCurrentCarton.TabStop = false;
            this.txtCurrentCarton.Value = "";
            this.txtCurrentCarton.WidthType = UserControl.WidthTypes.Long;
            this.txtCurrentCarton.XAlign = 423;
            // 
            // txtResCode
            // 
            this.txtResCode.AllowEditOnlyChecked = true;
            this.txtResCode.AutoUpper = true;
            this.txtResCode.BackColor = System.Drawing.Color.Gainsboro;
            this.txtResCode.Caption = "最后所在资源";
            this.txtResCode.Checked = false;
            this.txtResCode.EditType = UserControl.EditTypes.String;
            this.txtResCode.Location = new System.Drawing.Point(338, 77);
            this.txtResCode.MaxLength = 4000;
            this.txtResCode.Multiline = false;
            this.txtResCode.Name = "txtResCode";
            this.txtResCode.PasswordChar = '\0';
            this.txtResCode.ReadOnly = true;
            this.txtResCode.ShowCheckBox = false;
            this.txtResCode.Size = new System.Drawing.Size(285, 24);
            this.txtResCode.TabIndex = 5;
            this.txtResCode.TabNext = false;
            this.txtResCode.TabStop = false;
            this.txtResCode.Value = "";
            this.txtResCode.WidthType = UserControl.WidthTypes.Long;
            this.txtResCode.XAlign = 423;
            // 
            // txtOPCode
            // 
            this.txtOPCode.AllowEditOnlyChecked = true;
            this.txtOPCode.AutoUpper = true;
            this.txtOPCode.BackColor = System.Drawing.Color.Gainsboro;
            this.txtOPCode.Caption = "最后所在工序";
            this.txtOPCode.Checked = false;
            this.txtOPCode.EditType = UserControl.EditTypes.String;
            this.txtOPCode.Location = new System.Drawing.Point(13, 77);
            this.txtOPCode.MaxLength = 4000;
            this.txtOPCode.Multiline = false;
            this.txtOPCode.Name = "txtOPCode";
            this.txtOPCode.PasswordChar = '\0';
            this.txtOPCode.ReadOnly = true;
            this.txtOPCode.ShowCheckBox = false;
            this.txtOPCode.Size = new System.Drawing.Size(285, 24);
            this.txtOPCode.TabIndex = 4;
            this.txtOPCode.TabNext = false;
            this.txtOPCode.TabStop = false;
            this.txtOPCode.Value = "";
            this.txtOPCode.WidthType = UserControl.WidthTypes.Long;
            this.txtOPCode.XAlign = 98;
            // 
            // txtMoCode
            // 
            this.txtMoCode.AllowEditOnlyChecked = true;
            this.txtMoCode.AutoUpper = true;
            this.txtMoCode.BackColor = System.Drawing.Color.Gainsboro;
            this.txtMoCode.Caption = "工单代码";
            this.txtMoCode.Checked = false;
            this.txtMoCode.EditType = UserControl.EditTypes.String;
            this.txtMoCode.Location = new System.Drawing.Point(361, 46);
            this.txtMoCode.MaxLength = 4000;
            this.txtMoCode.Multiline = false;
            this.txtMoCode.Name = "txtMoCode";
            this.txtMoCode.PasswordChar = '\0';
            this.txtMoCode.ReadOnly = true;
            this.txtMoCode.ShowCheckBox = false;
            this.txtMoCode.Size = new System.Drawing.Size(261, 24);
            this.txtMoCode.TabIndex = 3;
            this.txtMoCode.TabNext = false;
            this.txtMoCode.TabStop = false;
            this.txtMoCode.Value = "";
            this.txtMoCode.WidthType = UserControl.WidthTypes.Long;
            this.txtMoCode.XAlign = 422;
            // 
            // txtItemCode
            // 
            this.txtItemCode.AllowEditOnlyChecked = true;
            this.txtItemCode.AutoUpper = true;
            this.txtItemCode.BackColor = System.Drawing.Color.Gainsboro;
            this.txtItemCode.Caption = "产品代码";
            this.txtItemCode.Checked = false;
            this.txtItemCode.EditType = UserControl.EditTypes.String;
            this.txtItemCode.Location = new System.Drawing.Point(37, 46);
            this.txtItemCode.MaxLength = 4000;
            this.txtItemCode.Multiline = false;
            this.txtItemCode.Name = "txtItemCode";
            this.txtItemCode.PasswordChar = '\0';
            this.txtItemCode.ReadOnly = true;
            this.txtItemCode.ShowCheckBox = false;
            this.txtItemCode.Size = new System.Drawing.Size(261, 24);
            this.txtItemCode.TabIndex = 2;
            this.txtItemCode.TabNext = false;
            this.txtItemCode.TabStop = false;
            this.txtItemCode.Value = "";
            this.txtItemCode.WidthType = UserControl.WidthTypes.Long;
            this.txtItemCode.XAlign = 98;
            // 
            // txtRCard
            // 
            this.txtRCard.AllowEditOnlyChecked = true;
            this.txtRCard.AutoUpper = true;
            this.txtRCard.BackColor = System.Drawing.Color.Gainsboro;
            this.txtRCard.Caption = "产品序列号";
            this.txtRCard.Checked = false;
            this.txtRCard.EditType = UserControl.EditTypes.String;
            this.txtRCard.Location = new System.Drawing.Point(25, 16);
            this.txtRCard.MaxLength = 4000;
            this.txtRCard.Multiline = false;
            this.txtRCard.Name = "txtRCard";
            this.txtRCard.PasswordChar = '\0';
            this.txtRCard.ReadOnly = false;
            this.txtRCard.ShowCheckBox = false;
            this.txtRCard.Size = new System.Drawing.Size(273, 24);
            this.txtRCard.TabIndex = 1;
            this.txtRCard.TabNext = false;
            this.txtRCard.Value = "";
            this.txtRCard.WidthType = UserControl.WidthTypes.Long;
            this.txtRCard.XAlign = 98;
            this.txtRCard.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRCard_TxtboxKeyPress);
            // 
            // txtCartonNO
            // 
            this.txtCartonNO.AllowEditOnlyChecked = true;
            this.txtCartonNO.AutoUpper = true;
            this.txtCartonNO.BackColor = System.Drawing.Color.Gainsboro;
            this.txtCartonNO.Caption = "Carton号";
            this.txtCartonNO.Checked = false;
            this.txtCartonNO.EditType = UserControl.EditTypes.String;
            this.txtCartonNO.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCartonNO.Location = new System.Drawing.Point(25, 16);
            this.txtCartonNO.MaxLength = 40;
            this.txtCartonNO.Multiline = false;
            this.txtCartonNO.Name = "txtCartonNO";
            this.txtCartonNO.PasswordChar = '\0';
            this.txtCartonNO.ReadOnly = false;
            this.txtCartonNO.ShowCheckBox = false;
            this.txtCartonNO.Size = new System.Drawing.Size(261, 24);
            this.txtCartonNO.TabIndex = 3;
            this.txtCartonNO.TabNext = true;
            this.txtCartonNO.Value = "";
            this.txtCartonNO.WidthType = UserControl.WidthTypes.Long;
            this.txtCartonNO.XAlign = 86;
            this.txtCartonNO.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCartonNO_TxtboxKeyPress);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnConfirm);
            this.panel2.Controls.Add(this.uBtnExit);
            this.panel2.Controls.Add(this.txtCartonNO);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 429);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(720, 56);
            this.panel2.TabIndex = 286;
            // 
            // btnConfirm
            // 
            this.btnConfirm.BackColor = System.Drawing.SystemColors.Control;
            this.btnConfirm.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnConfirm.BackgroundImage")));
            this.btnConfirm.ButtonType = UserControl.ButtonTypes.Confirm;
            this.btnConfirm.Caption = "确认";
            this.btnConfirm.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConfirm.Location = new System.Drawing.Point(304, 17);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(88, 22);
            this.btnConfirm.TabIndex = 101;
            this.btnConfirm.Visible = false;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // uBtnExit
            // 
            this.uBtnExit.BackColor = System.Drawing.SystemColors.Control;
            this.uBtnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("uBtnExit.BackgroundImage")));
            this.uBtnExit.ButtonType = UserControl.ButtonTypes.Exit;
            this.uBtnExit.Caption = "退出";
            this.uBtnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uBtnExit.Location = new System.Drawing.Point(408, 16);
            this.uBtnExit.Name = "uBtnExit";
            this.uBtnExit.Size = new System.Drawing.Size(88, 22);
            this.uBtnExit.TabIndex = 100;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.ucMessage);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 112);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(720, 317);
            this.panel4.TabIndex = 288;
            // 
            // ucMessage
            // 
            this.ucMessage.AutoScroll = true;
            this.ucMessage.BackColor = System.Drawing.Color.Gainsboro;
            this.ucMessage.ButtonVisible = false;
            this.ucMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucMessage.Location = new System.Drawing.Point(0, 0);
            this.ucMessage.Name = "ucMessage";
            this.ucMessage.Size = new System.Drawing.Size(720, 317);
            this.ucMessage.TabIndex = 176;
            // 
            // FTransferCarton
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(720, 485);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "FTransferCarton";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Carton包装移转采集";
            this.Load += new System.EventHandler(this.FPack_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion


        #region form的初始化
        private void InitForm()
        {

        }

        #endregion


        #region 页面事件

        private void FPack_Load(object sender, System.EventArgs e)
        {
            //			InitForm();
            //			InitializeGrid();
            //this.InitPageLanguage();
        }

        private void txtRCard_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                string rcard = txtRCard.Value.Trim().ToUpper();
                //转换成起始序列号
                DataCollectFacade dataCollectFacade = new DataCollectFacade(this.DataProvider);
                string sourceCard = dataCollectFacade.GetSourceCard(rcard, string.Empty);
                //end
                ProcessRCard(sourceCard);
            }
        }

        private void txtCartonNO_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                string cartonno = txtCartonNO.Value.Trim().ToUpper();
                if (string.IsNullOrEmpty(cartonno))
                {
                    ucMessage.Add(new UserControl.Message(MessageType.Normal, "$CS_PleaseInputCartonNo"));
                    this.txtCartonNO.TextFocus(false, true);
                    return;
                }
                ProcessCarton(cartonno);
            }
        }

        private void btnConfirm_Click(object sender, System.EventArgs e)
        {

        }

        #endregion

        //Process Running Card
        private void ProcessRCard(string rcard)
        {
            //根据当前序列号获取最新序列号
            DataCollectFacade dataCollectFacade = new DataCollectFacade(this.DataProvider);
            OQCFacade _OQCFacade = new OQCFacade(this.DataProvider);
            string sourceRCard = dataCollectFacade.GetSourceCard(rcard.Trim().ToUpper(), string.Empty);

            object objSimulationReport = dataCollectFacade.GetLastSimulationReport(sourceRCard);
            if (objSimulationReport != null)
            {
                // Added By hi1/venus.Feng on 20081127 for Hisense Version : 如果没有Carton信息，则不能进行移转

                if (string.IsNullOrEmpty((objSimulationReport as Domain.DataCollect.SimulationReport).CartonCode))
                {
                    ClearRCardInfo();
                    ucMessage.Add(new UserControl.Message(MessageType.Error
                        , "$Error_RCardNoCartonInfo $CS_Param_ID =" + rcard));
                    txtRCard.TextFocus(true, true);
                }
                else
                {
                    //add by andy xin 判断包装转移判断RCARD是否在TBLLOT2CARD中有
                    object lot2Card = _OQCFacade.GetOQCLot2Card((objSimulationReport as Domain.DataCollect.SimulationReport).RunningCard, (objSimulationReport as Domain.DataCollect.SimulationReport).MOCode, "", "");
                    if (lot2Card != null)
                    {
                        OQCLot oqcLot = _OQCFacade.GetOQCLot((lot2Card as OQCLot2Card).LOTNO, OQCFacade.Lot_Sequence_Default) as OQCLot;
                        if (!(oqcLot.LOTStatus == OQCLotStatus.OQCLotStatus_Reject || oqcLot.LOTStatus == OQCLotStatus.OQCLotStatus_Rejecting ||
                            oqcLot.LOTStatus == OQCLotStatus.OQCLotStatus_Pass || oqcLot.LOTStatus == OQCLotStatus.OQCLotStatus_Passing))
                        {

                            ucMessage.Add(new UserControl.Message(MessageType.Error, "$CS_RCARD_In_OQC"));
                            this.txtRCard.TextFocus(true, true);
                            return;
                        }
                    }


                    DisplayRCardInfo(objSimulationReport as Domain.DataCollect.SimulationReport);
                }
                // End Added
            }
            else
            {
                ClearRCardInfo();
                ucMessage.Add(new UserControl.Message(MessageType.Error
                    , "$NoSimulation $CS_Param_ID =" + rcard));
                txtRCard.TextFocus(true, true);
            }
        }

        //Process Carton NO
        private void ProcessCarton(string cartonno)
        {
            DataCollectFacade dcf = new DataCollectFacade(DataProvider);
            string rCard = txtRCard.Value.Trim().ToUpper();
            rCard = dcf.GetSourceCard(rCard, string.Empty);

            //箱号为空时，表示序列号从旧箱中移除
            if (cartonno == String.Empty)
            {
                #region RCARD 从 Carton中移出
                if (DialogResult.OK == MessageBox.Show(this, MutiLanguages.ParserString("$CS_Confirm_Transfer_1") + " " + txtRCard.Value.Trim().ToUpper() + " "
                    + MutiLanguages.ParserString("$CS_Confirm_Transfer_2") + txtCurrentCarton.Value.Trim().ToUpper() + " " + MutiLanguages.ParserString("$CS_Confirm_Transfer_3"), MutiLanguages.ParserString("$ShowConfirm"), MessageBoxButtons.OKCancel))
                {
                    InventoryFacade materialDCF = new InventoryFacade(DataProvider);

                    object objSimulationReport = dcf.GetLastSimulationReport(rCard);

                    //在制品存在
                    if (objSimulationReport != null)
                    {
                        Domain.DataCollect.SimulationReport sim = objSimulationReport as Domain.DataCollect.SimulationReport;

                        sim.CartonCode = String.Empty;
                        PackageFacade pf = new PackageFacade(DataProvider);

                        #region 移出操作
                        DataProvider.BeginTransaction();
                        try
                        {
                            dcf.RemoveFromCarton(sim.RunningCard, ApplicationService.Current().UserCode);
                            //dcf.RemoveFromPallet2RCard(sim.RunningCard);
                            DataProvider.CommitTransaction();

                            ucMessage.Add(new UserControl.Message(MessageType.Success
                                , "$CS_RCARD_BREAK_FROM_CARTON_SUCCESS $CS_Param_ID =" + txtRCard.Value.Trim().ToUpper()));

                            txtRCard.TextFocus(true, true);
                            txtCartonNO.Value = String.Empty;
                            ClearRCardInfo();
                        }
                        catch (Exception ex)
                        {
                            DataProvider.RollbackTransaction();
                            ucMessage.Add(new UserControl.Message(ex));

                            txtCartonNO.TextFocus(true, true);
                        }
                        finally
                        {
                            (DataProvider as SQLDomainDataProvider).PersistBroker.CloseConnection();
                        }
                        #endregion
                    }
                    else//在制品不存在
                    {
                        ucMessage.Add(new UserControl.Message(MessageType.Error
                            , "$NoSimulation $CS_Param_ID =" + txtRCard.Value.Trim().ToUpper()));

                        ClearRCardInfo();
                        txtRCard.TextFocus(true, true);
                    }
                }
                #endregion
            }
            else
            {
                #region RCARD 从当前Carton移转到另外一个Carton
                InventoryFacade materialDCF = new InventoryFacade(DataProvider);
                //ActionOnLineHelper onlineHelper = new ActionOnLineHelper(DataProvider);

                if (!CheckTargetCarton(cartonno))
                {
                    if (!m_NeedAddNewCarton)
                    {
                        txtCartonNO.TextFocus(true, true);
                        return;
                    }
                }
                string sourceCard = dcf.GetSourceCard(txtRCard.Value.Trim().ToUpper(), string.Empty);
                object objSimulationReport = dcf.GetLastSimulationReport(sourceCard);

                if (objSimulationReport != null)
                {
                    Domain.DataCollect.SimulationReport simulationReport = objSimulationReport as Domain.DataCollect.SimulationReport;

                    simulationReport.CartonCode = cartonno.Trim().ToUpper();
                    Package.PackageFacade pf = new BenQGuru.eMES.Package.PackageFacade(DataProvider);

                    #region 移转操作
                    DataProvider.BeginTransaction();
                    try
                    {
                        //Add By Bernard @ 2010-11-01
                        string moCode = txtMoCode.Value.Trim().ToUpper();
                        string oldCartonno = txtCurrentCarton.Value.Trim().ToUpper();
                        bool m_HasCartonAdded = false;
                        //end

                        //包装箱不存在，为CartonInfo表增加一个包装箱信息
                        if (m_NeedAddNewCarton)
                        {
                            DBDateTime currentDateTime = FormatHelper.GetNowDBDateTime(DataProvider);
                            BenQGuru.eMES.Domain.Package.CARTONINFO newCarton = new BenQGuru.eMES.Domain.Package.CARTONINFO();

                            newCarton.CARTONNO = cartonno.Trim().ToUpper();
                            newCarton.CAPACITY = ((new ItemFacade(DataProvider)).GetItem(simulationReport.ItemCode, GlobalVariables.CurrentOrganizations.First().OrganizationID) as Item).ItemCartonQty;
                            newCarton.COLLECTED = 0;
                            newCarton.EATTRIBUTE1 = "";
                            newCarton.PKCARTONID = System.Guid.NewGuid().ToString().ToUpper();
                            newCarton.MUSER = ApplicationService.Current().UserCode;
                            newCarton.MDATE = currentDateTime.DBDate;
                            newCarton.MTIME = currentDateTime.DBTime;
                            pf.AddCARTONINFO(newCarton);

                            //0.向tblcarton2rcard表增加一个包装箱 Add By Bernard @ 2010-11-01
                            Carton2RCARD newCarton2RCard = new Carton2RCARD();
                            newCarton2RCard.CartonCode = cartonno.Trim().ToUpper();
                            newCarton2RCard.Rcard = rCard;
                            newCarton2RCard.MUser = ApplicationService.Current().UserCode;
                            newCarton2RCard.MDate = currentDateTime.DBDate;
                            newCarton2RCard.MTime = currentDateTime.DBTime;
                            newCarton2RCard.Eattribute1 = "";
                            newCarton2RCard.MOCode = moCode;
                            pf.AddCarton2RCARD(newCarton2RCard);
                            m_HasCartonAdded = true;
                            //end
                        }

                        //1.判断新箱与旧箱内产品工单代码是否一致 Add By Bernard @ 2010-11-01
                        object oldObj = pf.GetCarton2RCARD(oldCartonno, rCard);
                        //modified by lisa @20120829
                        //string newMOCode = moCode;
                        object[] newObj = pf.GetCarton2RCARDByCartonNO(FormatHelper.CleanString(this.txtCartonNO.Value.Trim()));
                        string newMOCode = ((Carton2RCARD)newObj[0]).MOCode;
                        //end add
                        string oldMOCode = ((Carton2RCARD)oldObj).MOCode;

                        if (oldMOCode == newMOCode)//工单代码一致
                        {
                            if (m_HasCartonAdded == false)//箱号没有新增,则向Carton2RCard新增一条记录
                            {
                                DBDateTime currentDateTime = FormatHelper.GetNowDBDateTime(DataProvider);
                                Carton2RCARD carton2rcard = new Carton2RCARD();
                                carton2rcard.Rcard = rCard;
                                carton2rcard.CartonCode = cartonno;
                                carton2rcard.MOCode = moCode;
                                carton2rcard.MUser = ApplicationService.Current().UserCode;
                                carton2rcard.MDate = currentDateTime.DBDate;
                                carton2rcard.MTime = currentDateTime.DBTime;
                                carton2rcard.Eattribute1 = "";
                                pf.AddCarton2RCARD(carton2rcard);
                            }
                            //end

                            dcf.RemoveFromCarton(simulationReport.RunningCard, ApplicationService.Current().UserCode);

                            pf.UpdateCollected(cartonno.Trim().ToUpper());

                            Domain.DataCollect.Simulation simulation = (Domain.DataCollect.Simulation)dcf.GetSimulation(simulationReport.RunningCard);

                            if (simulation != null)
                            {
                                simulation.CartonCode = cartonno.Trim().ToUpper();
                                dcf.UpdateSimulation(simulation);
                            }

                            dcf.UpdateSimulationReport(simulationReport);

                            //记log
                            pf.addCarton2RCARDLog(cartonno, simulationReport.RunningCard, ApplicationService.Current().UserCode);
                            //end
                        }
                        else//新旧包装箱工单代码不一致 Add By Bernard @ 2010-11-01
                        {
                            if (m_HasCartonAdded == true)
                            {
                                //删除CartonInfo表新增加的包装箱
                                CARTONINFO cartoninfo = new CARTONINFO();
                                cartoninfo.CARTONNO = cartonno;
                                pf.DeleteCARTONINFO(cartoninfo);

                                //删除旧Carton2RCARD表新增加的箱号
                                Carton2RCARD carton2rcard = new Carton2RCARD();
                                carton2rcard.Rcard = rCard;
                                carton2rcard.CartonCode = cartonno;
                                pf.DeleteCarton2RCARD(carton2rcard);
                            }
                            else
                            {
                                DataProvider.RollbackTransaction();
                                ucMessage.Add(new UserControl.Message(MessageType.Error,
                                    "$CS_CARTON_NO :" + cartonno + "$CS_Which_MOCode_With_RCard :" + rCard + "$CS_For_Carton_No's_MOCode_Is_Different"));
                                txtCartonNO.TextFocus(true, true);
                                return;
                            }
                        }
                        //end

                        DataProvider.CommitTransaction();

                        ucMessage.Add(new UserControl.Message(MessageType.Success
                            , "$CS_RCARD_TRANSFER_CARTON_SUCCESS $CS_Param_ID =" + txtRCard.Value.Trim().ToUpper()));

                        txtRCard.TextFocus(true, true);
                        txtCartonNO.Value = String.Empty;
                        ClearRCardInfo();
                    }
                    catch (Exception ex)
                    {
                        DataProvider.RollbackTransaction();
                        ucMessage.Add(new UserControl.Message(ex));

                        txtCartonNO.TextFocus(true, true);
                    }
                    finally
                    {
                        (DataProvider as SQLDomainDataProvider).PersistBroker.CloseConnection();
                    }
                    #endregion

                }
                else
                {
                    ucMessage.Add(new UserControl.Message(MessageType.Error, "$NoSimulation $CS_Param_ID =" + txtRCard.Value.Trim().ToUpper()));

                    ClearRCardInfo();
                    txtRCard.TextFocus(true, true);
                }

                #endregion
            }

        }
        //check for carton capacity if overload
        private bool CheckTargetCarton(string tagCarton)
        {
            bool bResult = true;
            m_NeedAddNewCarton = false;
            object obj = (new Package.PackageFacade(DataProvider)).GetCARTONINFO(tagCarton);

            if (tagCarton == txtCurrentCarton.Value.ToUpper().Trim())
            {
                ucMessage.Add(new UserControl.Message(MessageType.Error, "$CS_TAG_CARTON_EQUAL_CUR_CARTON $CS_CARTON_NO =" + tagCarton));
                bResult = false;
            }
            
            #region OQC检验
            //Added by lisa@2012-8-29 
            //检查目标小箱是否已经在OQC处理过程中
            OQCFacade _OQCFacade = new OQCFacade(this.DataProvider);
            Lot2Carton lot2carton = _OQCFacade.GetLot2CartonByCartonNo(tagCarton) as Lot2Carton;
            OQCLot oqcCartonLot = null;
            if (lot2carton != null)
            {
                oqcCartonLot = _OQCFacade.GetOQCLot(lot2carton.OQCLot, OQCFacade.Lot_Sequence_Default) as OQCLot;
                if (oqcCartonLot != null && !(oqcCartonLot.LOTStatus == OQCLotStatus.OQCLotStatus_Reject || oqcCartonLot.LOTStatus == OQCLotStatus.OQCLotStatus_Rejecting ||
                                oqcCartonLot.LOTStatus == OQCLotStatus.OQCLotStatus_Pass || oqcCartonLot.LOTStatus == OQCLotStatus.OQCLotStatus_Passing))
                {
                    ucMessage.Add(new UserControl.Message(MessageType.Error, "$CS_RCARD_In_OQC"));
                    this.txtRCard.TextFocus(false, true);
                    return false;
                }
            }

            //检查当前Rcard所在箱和目标箱的OQC状态是否一致，或者都不在OQC中，或者都是OQC判过，或者都是OQC判退
            DataCollectFacade dataCollectFacade = new DataCollectFacade(this.DataProvider);
            string sourceRCard = dataCollectFacade.GetSourceCard(txtRCard.Value.Trim().ToUpper(), string.Empty);
            object objSimulationReport = dataCollectFacade.GetLastSimulationReport(sourceRCard);
            string currentlotno = (objSimulationReport as Domain.DataCollect.SimulationReport).LOTNO;

            OQCLot oqcRcardLot = null;

            if (!string.IsNullOrEmpty(currentlotno))
            {
                oqcRcardLot = _OQCFacade.GetOQCLot(currentlotno.Trim(), OQCFacade.Lot_Sequence_Default) as OQCLot;
            }

            if (!(oqcCartonLot == null && oqcRcardLot == null))
            {
                if (oqcCartonLot == null)
                {
                    ucMessage.Add(new UserControl.Message(MessageType.Error, "$CS_RCARD_CARTON_OQC_NOT_SAME"));
                    this.txtRCard.TextFocus(false, true);
                    return false;
                }
                if (oqcRcardLot == null)
                {
                    ucMessage.Add(new UserControl.Message(MessageType.Error, "$CS_RCARD_CARTON_OQC_NOT_SAME"));
                    this.txtRCard.TextFocus(false, true);
                    return false;
                }
                if (oqcCartonLot.LOTStatus != oqcRcardLot.LOTStatus)
                {
                    ucMessage.Add(new UserControl.Message(MessageType.Error, "$CS_RCARD_CARTON_OQC_NOT_SAME"));
                    this.txtRCard.TextFocus(false, true);
                    return false;
                }
            }

            #endregion

            if (obj == null)
            {
                //ucMessage.Add(new UserControl.Message(MessageType.Error,"$CS_CARTON_NOT_EXIST $CS_CARTON_NO =" + tagCarton));
                bResult = false;
                m_NeedAddNewCarton = true;
            }
            else
            {
                Domain.Package.CARTONINFO carton = obj as Domain.Package.CARTONINFO;
                if (carton.COLLECTED >= carton.CAPACITY)
                {
                    ucMessage.Add(new UserControl.Message(MessageType.Error, "$CARTON_ALREADY_FILL_OUT $CS_CARTON_NO =" + tagCarton));
                    bResult = false;
                }
            }

            return bResult;
        }
        //clear UI display information
        private void ClearRCardInfo()
        {
            txtCurrentCarton.Value = String.Empty;
            txtItemCode.Value = String.Empty;
            txtMoCode.Value = String.Empty;
            txtOPCode.Value = String.Empty;
            txtResCode.Value = String.Empty;
        }
        //display rcard information
        private void DisplayRCardInfo(Domain.DataCollect.SimulationReport sim)
        {
            txtCurrentCarton.Value = sim.CartonCode;
            txtItemCode.Value = sim.ItemCode;
            txtMoCode.Value = sim.MOCode;
            txtOPCode.Value = sim.OPCode;
            txtResCode.Value = sim.ResourceCode;

            txtCartonNO.TextFocus(true, true);
        }



    }
}

