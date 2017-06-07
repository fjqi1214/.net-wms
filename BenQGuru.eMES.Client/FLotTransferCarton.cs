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
using BenQGuru.eMES.LotDataCollect;
using UserControl;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Domain.LotPackage;
using BenQGuru.eMES.Domain.Package;
using BenQGuru.eMES.Package;
using BenQGuru.eMES.Material;
#endregion


namespace BenQGuru.eMES.Client
{
    public class FLotTransferCarton :  BaseForm
    {
        private UserControl.UCLabelEdit ucLabEdit2;
        private System.ComponentModel.IContainer components = null;
        private UserControl.UCLabelEdit ucLabelEdit1;
        private System.Windows.Forms.Panel panel1;
        private UserControl.UCLabelEdit txtCartonNO;
        private System.Windows.Forms.Panel panel2;
        private UserControl.UCButton BtnExit;
        private UserControl.UCLabelEdit txtLotCode;
        private UserControl.UCButton btnConfirm;
        private System.Windows.Forms.Panel panel4;
        private UserControl.UCMessage ucMessage;
        private UserControl.UCLabelEdit txtItemCode;
        private UserControl.UCLabelEdit txtMoCode;
        private bool m_NeedAddNewCarton = false;
        public UCLabelCombox ucLabelComboxCurrentCarton;

        private IDomainDataProvider _domainDataProvider = ApplicationService.Current().DataProvider;
        public IDomainDataProvider DataProvider
        {
            get
            {
                return _domainDataProvider;
            }
        }

        Package.PackageFacade pf = null;
        DataCollectFacade dataCollectFacade = null;
        public FLotTransferCarton()
        {
            // 该调用是 Windows 窗体设计器所必需的。
            InitializeComponent();

            UserControl.UIStyleBuilder.FormUI(this);
            ucMessage.Add(new UserControl.Message(">>$CS_Please_Input_LotCode"));

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FLotTransferCarton));
            this.ucLabEdit2 = new UserControl.UCLabelEdit();
            this.ucLabelEdit1 = new UserControl.UCLabelEdit();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ucLabelComboxCurrentCarton = new UserControl.UCLabelCombox();
            this.txtMoCode = new UserControl.UCLabelEdit();
            this.txtItemCode = new UserControl.UCLabelEdit();
            this.txtLotCode = new UserControl.UCLabelEdit();
            this.txtCartonNO = new UserControl.UCLabelEdit();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnConfirm = new UserControl.UCButton();
            this.BtnExit = new UserControl.UCButton();
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
            this.ucLabEdit2.AutoSelectAll = false;
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
            this.ucLabelEdit1.AutoSelectAll = false;
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
            this.panel1.Controls.Add(this.ucLabelComboxCurrentCarton);
            this.panel1.Controls.Add(this.txtMoCode);
            this.panel1.Controls.Add(this.txtItemCode);
            this.panel1.Controls.Add(this.txtLotCode);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(720, 77);
            this.panel1.TabIndex = 285;
            // 
            // ucLabelComboxCurrentCarton
            // 
            this.ucLabelComboxCurrentCarton.AllowEditOnlyChecked = true;
            this.ucLabelComboxCurrentCarton.Caption = "Carton号";
            this.ucLabelComboxCurrentCarton.Checked = false;
            this.ucLabelComboxCurrentCarton.Location = new System.Drawing.Point(362, 16);
            this.ucLabelComboxCurrentCarton.Name = "ucLabelComboxCurrentCarton";
            this.ucLabelComboxCurrentCarton.SelectedIndex = -1;
            this.ucLabelComboxCurrentCarton.ShowCheckBox = false;
            this.ucLabelComboxCurrentCarton.Size = new System.Drawing.Size(261, 24);
            this.ucLabelComboxCurrentCarton.TabIndex = 50;
            this.ucLabelComboxCurrentCarton.WidthType = UserControl.WidthTypes.Long;
            this.ucLabelComboxCurrentCarton.XAlign = 423;
            // 
            // txtMoCode
            // 
            this.txtMoCode.AllowEditOnlyChecked = true;
            this.txtMoCode.AutoSelectAll = false;
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
            this.txtItemCode.AutoSelectAll = false;
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
            // txtLotCode
            // 
            this.txtLotCode.AllowEditOnlyChecked = true;
            this.txtLotCode.AutoSelectAll = false;
            this.txtLotCode.AutoUpper = true;
            this.txtLotCode.BackColor = System.Drawing.Color.Gainsboro;
            this.txtLotCode.Caption = "产品批次条码";
            this.txtLotCode.Checked = false;
            this.txtLotCode.EditType = UserControl.EditTypes.String;
            this.txtLotCode.Location = new System.Drawing.Point(13, 16);
            this.txtLotCode.MaxLength = 4000;
            this.txtLotCode.Multiline = false;
            this.txtLotCode.Name = "txtLotCode";
            this.txtLotCode.PasswordChar = '\0';
            this.txtLotCode.ReadOnly = false;
            this.txtLotCode.ShowCheckBox = false;
            this.txtLotCode.Size = new System.Drawing.Size(285, 24);
            this.txtLotCode.TabIndex = 1;
            this.txtLotCode.TabNext = false;
            this.txtLotCode.Value = "";
            this.txtLotCode.WidthType = UserControl.WidthTypes.Long;
            this.txtLotCode.XAlign = 98;
            this.txtLotCode.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLotCode_TxtboxKeyPress);
            // 
            // txtCartonNO
            // 
            this.txtCartonNO.AllowEditOnlyChecked = true;
            this.txtCartonNO.AutoSelectAll = false;
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
            this.panel2.Controls.Add(this.BtnExit);
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
            // 
            // BtnExit
            // 
            this.BtnExit.BackColor = System.Drawing.SystemColors.Control;
            this.BtnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnExit.BackgroundImage")));
            this.BtnExit.ButtonType = UserControl.ButtonTypes.Exit;
            this.BtnExit.Caption = "退出";
            this.BtnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnExit.Location = new System.Drawing.Point(408, 16);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(88, 22);
            this.BtnExit.TabIndex = 100;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.ucMessage);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 77);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(720, 352);
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
            this.ucMessage.Size = new System.Drawing.Size(720, 352);
            this.ucMessage.TabIndex = 176;
            // 
            // FLotTransferCarton
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(720, 485);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "FLotTransferCarton";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Carton包装移转采集";
            this.Load += new System.EventHandler(this.FPack_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion


        #region 页面事件

        private void FPack_Load(object sender, System.EventArgs e)
        {
            dataCollectFacade = new DataCollectFacade(this.DataProvider);
            pf = new PackageFacade(this.DataProvider);
            //this.InitPageLanguage();
        }

        private void txtLotCode_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                string lotCode = txtLotCode.Value.Trim().ToUpper();
                ProcessLotCode(lotCode);
            }
            else
            {
                ClearLotCodeInfo();
                this.txtLotCode.TextFocus(false, false);

            }
        }

        private void txtCartonNO_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                string cartonno = txtCartonNO.Value.Trim().ToUpper();
                if (string.IsNullOrEmpty(cartonno))
                {
                    ucMessage.Add(new UserControl.Message(MessageType.Normal, ">>$CS_PleaseInputCartonNo"));
                    this.txtCartonNO.TextFocus(false, true);
                    return;
                }
                if (this.ucLabelComboxCurrentCarton.SelectedIndex == -1)
                {
                    ucMessage.Add(new UserControl.Message(MessageType.Error, "$CS_ERROR_OLDCartonNo_NO_EMPTY"));
                    ucMessage.Add(new UserControl.Message(MessageType.Normal, ">>$CS_PleaseInputCartonNo"));
                    this.txtCartonNO.TextFocus(true, true);
                    return;
                }
                ProcessCarton(cartonno);
            }

        }


        #endregion

        //Process LotCode
        private void ProcessLotCode(string lotCode)
        {
            DataCollectFacade dataCollectFacade = new DataCollectFacade(this.DataProvider);
            OQCFacade _OQCFacade = new OQCFacade(this.DataProvider);

            object objSimulationReport = dataCollectFacade.GetLastLotSimulationReport(lotCode);
            if (objSimulationReport != null)
            {
                // Added By hi1/venus.Feng on 20081127 for Hisense Version : 如果没有Carton信息，则不能进行移转
                object[] objCarton2Lot = pf.GetCarton2LotByLotCode(lotCode);
                if (objCarton2Lot == null || objCarton2Lot.Length == 0 )
                {
                    ClearLotCodeInfo();
                    ucMessage.Add(new UserControl.Message(MessageType.Error
                        , "$Error_LotNoCartonInfo $CS_Param_LotNO =" + lotCode));
                    ucMessage.Add(new UserControl.Message(">>$CS_Please_Input_LotCode"));
                    txtLotCode.TextFocus(true, true);
                }
                else
                {
                    //add by andy xin 判断包装转移判断Lot是否在TBLLOT2CARD中有
                    //object lot2Card = _OQCFacade.GetOQCLot2Card((objSimulationReport as Domain.LotDataCollect.LotSimulationReport).LotCode, (objSimulationReport as Domain.LotDataCollect.LotSimulationReport).MOCode, "", "");
                    //if (lot2Card != null)
                    //{
                    //    OQCLot oqcLot = _OQCFacade.GetOQCLot((lot2Card as OQCLot2Card).LOTNO, OQCFacade.Lot_Sequence_Default) as OQCLot;
                    //    if (!(oqcLot.LOTStatus == OQCLotStatus.OQCLotStatus_Reject || oqcLot.LOTStatus == OQCLotStatus.OQCLotStatus_Rejecting ||
                    //        oqcLot.LOTStatus == OQCLotStatus.OQCLotStatus_Pass || oqcLot.LOTStatus == OQCLotStatus.OQCLotStatus_Passing))
                    //    {

                    //        ucMessage.Add(new UserControl.Message(MessageType.Error, "$CS_RCARD_Can_not_Trans"));
                    //        this.txtLotCode.TextFocus(true, true);
                    //        return;
                    //    }
                    //}

                    DisplayLotCodeInfo(objCarton2Lot , objSimulationReport as Domain.LotDataCollect.LotSimulationReport);
                    ucMessage.Add(new UserControl.Message(">>$CS_PleaseInputCartonNo"));
                    txtCartonNO.TextFocus(true, true);
                }
                // End Added
            }
            else
            {
                ClearLotCodeInfo();
                ucMessage.Add(new UserControl.Message(MessageType.Error
                    , "$NoSimulation $CS_Param_LotNO =" + lotCode));
                ucMessage.Add(new UserControl.Message(">>$CS_Please_Input_LotCode"));
                txtLotCode.TextFocus(true, true);
            }
        }

        //Process Carton NO
        private void ProcessCarton(string cartonno)
        {
            DataCollectFacade dcf = new DataCollectFacade(DataProvider);
            string lotCode = txtLotCode.Value.Trim().ToUpper();

            #region lotCode 从当前Carton移转到另外一个Carton
            InventoryFacade materialDCF = new InventoryFacade(DataProvider);

            if (!CheckTargetCarton(cartonno))
            {
                if (!m_NeedAddNewCarton)
                {
                    ucMessage.Add(new UserControl.Message(MessageType.Normal, ">>$CS_PleaseInputCartonNo"));
                    txtCartonNO.TextFocus(true, true);
                    return;
                }
            }
            object objSimulationReport = dcf.GetLastLotSimulationReport(lotCode);

            if (objSimulationReport != null)
            {
                Domain.LotDataCollect.LotSimulationReport simulationReport = objSimulationReport as Domain.LotDataCollect.LotSimulationReport;

                //simulationReport.CartonCode = cartonno.Trim().ToUpper();


                #region 移转操作
                DataProvider.BeginTransaction();
                try
                {
                    //Add By Bernard @ 2010-11-01
                    string moCode = txtMoCode.Value.Trim().ToUpper();
                    string oldCartonno = ucLabelComboxCurrentCarton.SelectedItemText;
                    bool m_HasCartonAdded = false;
                    //end
                    decimal oldCartonQty = 0;
                    Carton2Lot oldCarton2Lot = (Carton2Lot)pf.GetCarton2Lot(oldCartonno.Trim().ToUpper(), lotCode.Trim().ToUpper());
                    if (oldCarton2Lot != null)
                    {
                        oldCartonQty = oldCarton2Lot.CartonQty;
                    }

                    int capaCity = ((new ItemFacade(DataProvider)).GetItem(simulationReport.ItemCode, GlobalVariables.CurrentOrganizations.First().OrganizationID) as Item).ItemCartonQty;



                    //包装箱不存在，为CartonInfo表增加一个包装箱信息
                    if (m_NeedAddNewCarton)
                    {
                        if (oldCartonQty > capaCity)
                        {
                            ucMessage.Add(new UserControl.Message(MessageType.Error
                               , "$CS_CartonQty_IS_Large $CS_Param_LotNO =" + txtLotCode.Value.Trim().ToUpper()));
                            ucMessage.Add(new UserControl.Message(MessageType.Normal, ">>$CS_PleaseInputCartonNo"));
                            this.DataProvider.RollbackTransaction();
                            txtCartonNO.TextFocus(true, true);
                            return;
                        }

                        DBDateTime currentDateTime = FormatHelper.GetNowDBDateTime(DataProvider);
                        BenQGuru.eMES.Domain.Package.CARTONINFO newCarton = new BenQGuru.eMES.Domain.Package.CARTONINFO();

                        newCarton.CARTONNO = cartonno.Trim().ToUpper();
                        newCarton.CAPACITY = capaCity;
                        newCarton.COLLECTED = 0;
                        newCarton.EATTRIBUTE1 = "";
                        newCarton.PKCARTONID = System.Guid.NewGuid().ToString().ToUpper();
                        newCarton.MUSER = ApplicationService.Current().UserCode;
                        newCarton.MDATE = currentDateTime.DBDate;
                        newCarton.MTIME = currentDateTime.DBTime;
                        pf.AddCARTONINFO(newCarton);

                        //0.向tblcarton2rcard表增加一个包装箱 Add By Bernard @ 2010-11-01
                        Carton2Lot newCarton2Lot = new Carton2Lot();
                        newCarton2Lot.CartonCode = cartonno.Trim().ToUpper();
                        newCarton2Lot.LotCode = lotCode;
                        newCarton2Lot.CartonQty = oldCartonQty;
                        newCarton2Lot.MUser = ApplicationService.Current().UserCode;
                        newCarton2Lot.MDate = currentDateTime.DBDate;
                        newCarton2Lot.MTime = currentDateTime.DBTime;
                        newCarton2Lot.Eattribute1 = "";
                        newCarton2Lot.MOCode = moCode;
                        
                        pf.AddCarton2Lot(newCarton2Lot);
                        m_HasCartonAdded = true;
                        //end
                    }


                    //判断箱中采集数量是否大与包装容量
                    object obj = pf.GetCARTONINFO(cartonno.Trim().ToUpper());
                    Domain.Package.CARTONINFO carton = obj as Domain.Package.CARTONINFO;
                    if ((carton.COLLECTED + oldCartonQty  )> carton.CAPACITY)
                    {
                        this.DataProvider.RollbackTransaction();
                        ucMessage.Add(new UserControl.Message(MessageType.Error, "$CS_CartonQty_IS_Large $CS_CARTON_NO =" + cartonno.Trim().ToUpper()));
                        ucMessage.Add(new UserControl.Message(MessageType.Normal, ">>$CS_PleaseInputCartonNo"));
                        txtCartonNO.TextFocus(true, true);
                        return;
                    }


                    //1.判断新箱与旧箱内产品工单代码是否一致 Add By Bernard @ 2010-11-01
                    object oldObj = pf.GetCarton2Lot(oldCartonno, lotCode);

                    string newMOCode = moCode;
                    string oldMOCode = ((Carton2Lot)oldObj).MOCode;

                    if (oldMOCode == newMOCode)//工单代码一致
                    {
                        if (m_HasCartonAdded == false)//箱号没有新增,则向Carton2RCard新增一条记录
                        {
                            DBDateTime currentDateTime = FormatHelper.GetNowDBDateTime(DataProvider);

                            object newObj = pf.GetCarton2Lot(cartonno, lotCode);
                            if (newObj == null)
                            {
                               
                                Carton2Lot carton2Lot = new Carton2Lot();
                                carton2Lot.LotCode = lotCode;
                                carton2Lot.CartonQty = oldCartonQty;
                                carton2Lot.CartonCode = cartonno;
                                carton2Lot.MOCode = moCode;
                                carton2Lot.MUser = ApplicationService.Current().UserCode;
                                carton2Lot.MDate = currentDateTime.DBDate;
                                carton2Lot.MTime = currentDateTime.DBTime;
                                carton2Lot.Eattribute1 = "";
                                pf.AddCarton2Lot(carton2Lot);
                            }
                            else
                            {
                                Carton2Lot carton2Lot = newObj as Carton2Lot;
                                carton2Lot.CartonQty = carton2Lot.CartonQty + oldCartonQty;
                                carton2Lot.MUser = ApplicationService.Current().UserCode;
                                carton2Lot.MDate = currentDateTime.DBDate;
                                carton2Lot.MTime = currentDateTime.DBTime;
                                pf.UpdateCarton2Lot(carton2Lot);
                            }
                        }
                        //end

                        dcf.RemoveFromCarton(simulationReport.LotCode, oldCartonno, ApplicationService.Current().UserCode);

                        pf.UpdateCollected(cartonno.Trim().ToUpper(), oldCartonQty);


                        //记log
                        pf.AddCarton2LotLog(cartonno, simulationReport.LotCode, oldCartonQty, ApplicationService.Current().UserCode);
                        //end

                    }
                    else
                    {
                        DataProvider.RollbackTransaction();
                        ucMessage.Add(new UserControl.Message(MessageType.Error, "$ERROR_CARTON_MOCODE $CS_CARTON_NO =" + cartonno.Trim().ToUpper()));
                        ucMessage.Add(new UserControl.Message(MessageType.Normal, ">>$CS_PleaseInputCartonNo"));
                        txtCartonNO.TextFocus(true, true);
                        return;
                    }

                    //判断相中的批次是否属于同一批次
                    int mocodeCount = pf.GetMoCountByCartonNo(cartonno);
                    if (mocodeCount > 1)
                    {
                        DataProvider.RollbackTransaction();
                        ucMessage.Add(new UserControl.Message(MessageType.Error,"$ERROR_CARTON_MOCODE $CS_CARTON_NO =" + cartonno.Trim().ToUpper()));
                        ucMessage.Add(new UserControl.Message(MessageType.Normal, ">>$CS_PleaseInputCartonNo"));
                        txtCartonNO.TextFocus(true, true);
                        return;
                    }
                    //end


                    DataProvider.CommitTransaction();

                    ucMessage.Add(new UserControl.Message(MessageType.Success
                        , "$CS_LOTCODE_TRANSFER_CARTON_SUCCESS $CS_Param_LotNO =" + txtLotCode.Value.Trim().ToUpper()));
                    ucMessage.Add(new UserControl.Message(">>$CS_Please_Input_LotCode"));
                    txtLotCode.TextFocus(true, true);
                    txtCartonNO.Value = String.Empty;
                    ClearLotCodeInfo();
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
                ucMessage.Add(new UserControl.Message(MessageType.Error, "$NoSimulation $CS_Param_LotNO =" + txtLotCode.Value.Trim().ToUpper()));
                ucMessage.Add(new UserControl.Message(">>$CS_Please_Input_LotCode"));
                ClearLotCodeInfo();
                txtLotCode.TextFocus(true, true);
            }

            #endregion


        }
        //check for carton capacity if overload
        private bool CheckTargetCarton(string tagCarton)
        {
            bool bResult = true;
            m_NeedAddNewCarton = false;
            object obj = (new Package.PackageFacade(DataProvider)).GetCARTONINFO(tagCarton);

            if (tagCarton == ucLabelComboxCurrentCarton.SelectedItemText)
            {
                ucMessage.Add(new UserControl.Message(MessageType.Error, "$CS_TAG_CARTON_EQUAL_CUR_CARTON $CS_CARTON_NO =" + tagCarton));
                bResult = false;
            }
            if (obj == null)
            {
                //ucMessage.Add(new  .Message(MessageType.Error,"$CS_CARTON_NOT_EXIST $CS_CARTON_NO =" + tagCarton));
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
        private void ClearLotCodeInfo()
        {
            ucLabelComboxCurrentCarton.Clear();
            txtItemCode.Value = String.Empty;
            txtMoCode.Value = String.Empty;
        }
        //display lotCode information
        private void DisplayLotCodeInfo(object[] objs, Domain.LotDataCollect.LotSimulationReport sim)
        {
            ucLabelComboxCurrentCarton.Clear();
            foreach(Carton2Lot item in objs)
            {
                ucLabelComboxCurrentCarton.AddItem(item.CartonCode, item.CartonCode);
            }
            ucLabelComboxCurrentCarton.SelectedIndex = 0;

            txtItemCode.Value = sim.ItemCode;
            txtMoCode.Value = sim.MOCode;
            txtCartonNO.TextFocus(true, true);
        }



    }
}

