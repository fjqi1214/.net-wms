#region System
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
#endregion

#region project
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Client.Service;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.DataCollect;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Domain.DataCollect;
using BenQGuru.eMES.Web.Helper;
using UserControl;
#endregion

namespace BenQGuru.eMES.Client
{
    public partial class FDown : BaseForm
    {

        private IDomainDataProvider _domainDataProvider = ApplicationService.Current().DataProvider;

        public IDomainDataProvider DataProvider
        {
            get
            {
                return _domainDataProvider;
            }
        }

        DataCollectFacade _dataCollectFacade = null;
        private DataTable _dataTable = new DataTable();
        private string _FunctionName = string.Empty;

        public FDown()
        {
            InitializeComponent();
            UserControl.UIStyleBuilder.FormUI(this);
            UserControl.UIStyleBuilder.GridUI(ultraGridMain);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.ucLabelEditDown.Value = string.Empty;
            this.ucLabelEditRcard.Value = string.Empty;
            this.ucLblEditNumber.Value = "0";
            this.ToCreateDownCode();
            this.QueryORUpdateGrid();
            this.ucLabelEditDown.TextFocus(false, true);
        }

        private void ucLabelEditRcard_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (string.IsNullOrEmpty(this.ucLabelEditDown.Value.Trim()))
                {
                    ApplicationRun.GetInfoForm().AddEx(this._FunctionName, this.ucLabelEditDown.Value + ";" + this.ucLabelEditDown.Value,
                                                    new UserControl.Message(MessageType.Error, "$CS_PleaseInputDownReason"), false);
                    this.ucLabelEditDown.TextFocus(false, true);
                    return;
                }

                if (string.IsNullOrEmpty(this.ucLabelEditRcard.Value.Trim()))
                {
                    ApplicationRun.GetInfoForm().AddEx(this._FunctionName, this.ucLabelEditRcard.Value + ";" + this.ucLabelEditRcard.Value,
                                                  new UserControl.Message(MessageType.Error, "$CS_PleaseInputID"), false);
                    this.ucLabelEditRcard.TextFocus(false, true);
                    return;
                }

                //检查事件号是否超出最大范围
                if (!this.CheckDownCodeLimits())
                {
                    ApplicationRun.GetInfoForm().AddEx(this._FunctionName, "" + ": " + this.ucLabelEditDownCode.Value,
                                                 new UserControl.Message(MessageType.Error, "$CS_DownCode_Out_Of_Limit"), true);
                    return;
                }


                if (_dataCollectFacade == null)
                {
                    _dataCollectFacade = new DataCollectFacade(this.DataProvider);
                }

                //清除序列号中的特殊字符
                string RunningCard = FormatHelper.CleanString(this.ucLabelEditRcard.Value.Trim().ToUpper());

                //根据当前序列号获取产品最原始的序列号
                DataCollectFacade dataCollectFacade = new DataCollectFacade(this.DataProvider);
                string sourceRCard = dataCollectFacade.GetSourceCard(RunningCard, string.Empty);

                //判断序列号在生产信息中是否存在，同时获取rcard
                object objectRcard = _dataCollectFacade.GetRcardFromSimulationReport(sourceRCard);
                if (objectRcard == null)
                {
                    ApplicationRun.GetInfoForm().AddEx(this._FunctionName, this.ucLabelEditRcard.Value + ";" + this.ucLabelEditRcard.Value,
                                                 new UserControl.Message(MessageType.Error, "$CS_RcardInput_Wrong $NoProductInfo"), false);
                    this.ucLabelEditRcard.TextFocus(false, true);
                    return;
                }

                //判断序列号在生产信息中是否存在,同时获取simulation
                object objectSimulationReport = _dataCollectFacade.GetLastSimulationReport(((SimulationReport)objectRcard).RunningCard.Trim().ToUpper());
                if (objectSimulationReport == null)
                {
                    ApplicationRun.GetInfoForm().AddEx(this._FunctionName, this.ucLabelEditRcard.Value + ";" + this.ucLabelEditRcard.Value,
                                                 new UserControl.Message(MessageType.Error, "$CS_RcardInput_Wrong $NoProductInfo"), false);
                    this.ucLabelEditRcard.TextFocus(false, true);
                    return;
                }

                //判断序列号在此事件号中是否重负
                object objectDown = _dataCollectFacade.GetDown(FormatHelper.CleanString(this.ucLabelEditDownCode.Value.ToUpper()), ((SimulationReport)objectRcard).RunningCard.Trim().ToUpper());
                if (objectDown != null)
                {
                    ApplicationRun.GetInfoForm().AddEx(this._FunctionName, this.ucLabelEditRcard.Value + ";" + this.ucLabelEditRcard.Value,
                                                 new UserControl.Message(MessageType.Error, "$Error_CS_ID_Already_Exist_INHERE"), false);
                    this.ucLabelEditRcard.TextFocus(false, true);
                    return;
                }

                //判断序列号在其他事件号中是否存在
                object[] objectDownList = _dataCollectFacade.QueryDownByRcard(((SimulationReport)objectRcard).RunningCard.Trim().ToUpper());
                if (objectDownList != null)
                {
                    for (int i = 0; i < objectDownList.Length; i++)
                    {
                        if (((Down)objectDownList[i]).DownCode != this.ucLabelEditDownCode.Value && ((Down)objectDownList[i]).DownStatus==DownStatus.DownStatus_Down)
                        {
                            ApplicationRun.GetInfoForm().AddEx(this._FunctionName, RunningCard + ";" + RunningCard,
                                                  new UserControl.Message(MessageType.Error, "$Error_CS_ID_Already_Exist_INOther :" + ((Down)objectDownList[0]).DownCode), false);
                            this.ucLabelEditRcard.TextFocus(false, true);
                            return; 
                        }
                    }                    
                }

                //下地号与下地原因没有关系，新建下地号
                object[] objectDowns = _dataCollectFacade.QueryDown(FormatHelper.CleanString(this.ucLabelEditDownCode.Value.ToUpper()), FormatHelper.CleanString(this.ucLabelEditDown.Value.Trim().ToUpper()));
                if (objectDowns == null)
                {
                    this.ToCreateDownCode();
                }

                //检查事件号是否超出最大范围
                if (!this.CheckDownCodeLimits())
                {
                    ApplicationRun.GetInfoForm().AddEx(this._FunctionName, "" + ": " + this.ucLabelEditDownCode.Value,
                                                 new UserControl.Message(MessageType.Error, "$CS_DownCode_Out_Of_Limit"), true);
                    return;
                }

                BaseSetting.BaseModelFacade baseModelFacade = new BaseModelFacade(this.DataProvider);
                object objectRes = baseModelFacade.GetResource(ApplicationService.Current().ResourceCode.ToUpper());
                string SSCode = ((Domain.BaseSetting.Resource)objectRes).StepSequenceCode;
                DBDateTime NowDBDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);

                this.DataProvider.BeginTransaction();
                try
                {
                    Down NewDown = _dataCollectFacade.CreateNewDown();

                    NewDown.DownCode = FormatHelper.CleanString(this.ucLabelEditDownCode.Value.Trim());
                    NewDown.RCard = ((SimulationReport)objectRcard).RunningCard.Trim().ToUpper();
                    NewDown.MOCode = ((SimulationReport)objectSimulationReport).MOCode;
                    NewDown.ModelCode = ((SimulationReport)objectSimulationReport).ModelCode;
                    NewDown.ItemCode = ((SimulationReport)objectSimulationReport).ItemCode;
                    NewDown.SSCode = SSCode.Trim().ToUpper();
                    NewDown.ResCode = ApplicationService.Current().ResourceCode;
                    NewDown.DownStatus = DownStatus.DownStatus_Down;
                    NewDown.DownReason = FormatHelper.CleanString(this.ucLabelEditDown.Value.Trim());
                    NewDown.DownDate = NowDBDateTime.DBDate;
                    NewDown.DownShiftDate = NowDBDateTime.DBDate;
                    NewDown.DownTime = NowDBDateTime.DBTime;
                    NewDown.DownBy = ApplicationService.Current().UserCode;
                    NewDown.UPReason = string.Empty;
                    NewDown.ORGID = GlobalVariables.CurrentOrganizations.First().OrganizationID;
                    NewDown.UPBY = string.Empty;
                    NewDown.MaintainUser = ApplicationService.Current().UserCode;
                    NewDown.MaintainDate = NowDBDateTime.DBDate;
                    NewDown.MaintainTime = NowDBDateTime.DBTime;
                    NewDown.EAttribute1 = string.Empty;

                    _dataCollectFacade.AddDown(NewDown);
                }
                catch (Exception ex)
                {
                    this.DataProvider.RollbackTransaction();
                    ApplicationRun.GetInfoForm().AddEx(this._FunctionName, this.ucLabelEditRcard.Value + ";" + this.ucLabelEditRcard.Value,
                                                 new UserControl.Message(ex), false);
                }
                finally
                {
                    this.DataProvider.CommitTransaction();
                    this.ucLblEditNumber.Value = Convert.ToString(System.Int32.Parse(this.ucLblEditNumber.Value.Trim()) + 1);
                    ApplicationRun.GetInfoForm().AddEx(this._FunctionName, this.ucLabelEditRcard.Value + ";" + this.ucLabelEditRcard.Value,
                             new UserControl.Message(MessageType.Success, "$CS_DownIsSuccess $CS_Param_RunSeq:" + ((SimulationReport)objectRcard).RunningCard.Trim().ToUpper()), false);
                }
                //同步Grid
                this.QueryORUpdateGrid();
                this.ucLabelEditRcard.TextFocus(false, true);
            }
        }

        private void ucLabelEditMemo_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (string.IsNullOrEmpty(FormatHelper.CleanString(this.ucLabelEditDown.Value.Trim())))
                {
                    ApplicationRun.GetInfoForm().AddEx(this._FunctionName, this.ucLabelEditDown.Value + ";" + this.ucLabelEditDown.Value,
                                                   new UserControl.Message(MessageType.Error, "$CS_PleaseInputDownReason"), false);
                    this.ucLabelEditDown.TextFocus(false, true);
                }
                else
                {
                    this.ucLabelEditRcard.TextFocus(false, true);
                }
            }
        }

        private void ultraGridMain_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            UltraWinGridHelper ultraWinGridHelper = new UltraWinGridHelper(this.ultraGridMain);

            ultraWinGridHelper.AddCommonColumn("mocode", "工单");
            ultraWinGridHelper.AddCommonColumn("runningcard", "序列号");
            ultraWinGridHelper.AddCommonColumn("itemcode", "产品");
            ultraWinGridHelper.AddCommonColumn("itemcodedesc", "产品描述");

            e.Layout.Bands[0].Columns["mocode"].Width = 150;
            e.Layout.Bands[0].Columns["runningcard"].Width = 150;
            e.Layout.Bands[0].Columns["itemcode"].Width = 150;
            e.Layout.Bands[0].Columns["itemcodedesc"].Width = 250;

            e.Layout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
        }


        private void InitializeGrid()
        {
            _dataTable.Columns.Clear();
            _dataTable.Columns.Add("mocode", typeof(string)).ReadOnly = true;
            _dataTable.Columns.Add("runningcard", typeof(string)).ReadOnly = true;
            _dataTable.Columns.Add("itemcode", typeof(string)).ReadOnly = true;
            _dataTable.Columns.Add("itemcodedesc", typeof(string)).ReadOnly = true;
            this.ultraGridMain.DataSource = _dataTable;
        }

        //绑定数据
        private void BindGrid(object[] objs)
        {
            _dataTable.Clear();
            foreach (DownWithRCardInfo sim in objs)
            {
                _dataTable.Rows.Add(new object[]{
                                                   sim.MOCode,
                                                   sim.RCard
                                                  ,sim.ItemCode	
                                                  ,sim.ItemDescription
                                              });
            }
            _dataTable.AcceptChanges();
        }


        //更新查询数据
        private void QueryORUpdateGrid()
        {
            object[] objectDownsAndItemDesc = _dataCollectFacade.QueryDownANDItemdesc(FormatHelper.CleanString(this.ucLabelEditDownCode.Value.Trim()));
            if (objectDownsAndItemDesc != null)
            {
                BindGrid(objectDownsAndItemDesc);
            }
            else
            {
                _dataTable.Clear();
            }
        }

        //生成新事件号
        private void ToCreateDownCode()
        {
            if (_dataCollectFacade == null)
            {
                _dataCollectFacade = new DataCollectFacade(this.DataProvider);
            }
            BaseSetting.BaseModelFacade baseModelFacade = new BaseModelFacade(this.DataProvider);
            object objectRes = baseModelFacade.GetResource(ApplicationService.Current().ResourceCode.ToUpper());
            if (objectRes == null)
            {
                ApplicationRun.GetInfoForm().AddEx(this._FunctionName, "" + ": " + ApplicationService.Current().ResourceCode,
                                              new UserControl.Message(MessageType.Error, "$CS_Res_NotHave_SSCode"), true);
                return;
            }

            string SSCode = ((Domain.BaseSetting.Resource)objectRes).StepSequenceCode;

            DBDateTime DBDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            string DBDate = DBDateTime.DBDate.ToString();

            object objectMaxDownCode = _dataCollectFacade.GetMaxDownCode(SSCode, DBDate, GlobalVariables.CurrentOrganizations.First().OrganizationID);

            if (objectMaxDownCode == null)
            {
                this.ucLabelEditDownCode.Value = SSCode + DBDate + "_001";
            }
            else
            {
                if (string.IsNullOrEmpty(((Down)objectMaxDownCode).DownCode))
                {
                    this.ucLabelEditDownCode.Value = SSCode + DBDate + "_001";
                }
                else
                {
                    string MaxDownCode = ((Down)objectMaxDownCode).DownCode;
                    int DownCodelength = MaxDownCode.Length;
                    string DownCodeNum = Convert.ToString(System.Int32.Parse(MaxDownCode.Substring(DownCodelength - 3, 3)) + 1);
                    if (DownCodeNum.Length == 1)
                    {
                        DownCodeNum = "00" + DownCodeNum;
                    }
                    else if (DownCodeNum.Length == 2)
                    {
                        DownCodeNum = "0" + DownCodeNum;
                    }

                    this.ucLabelEditDownCode.Value = SSCode + DBDate + "_" + DownCodeNum;
                }
            }
        }

        //检查事件号是否超出最大范围
        private bool CheckDownCodeLimits()
        {
            string DownCode = FormatHelper.CleanString(this.ucLabelEditDownCode.Value);
            int DownCodeLength = DownCode.Length;
            int DownCodeNum = System.Int32.Parse(DownCode.Substring(DownCodeLength - 3, 3));
            if (DownCodeNum == 000)
            {
                return false;
            }
            return true;
        }

        private void ucLabelEditDownCode_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void FDown_Load(object sender, EventArgs e)
        {
            this.InitializeGrid();
            this.ToCreateDownCode();
            this.ucLabelEditDownCode.Enabled = false;
            this.ucLblEditNumber.Value = "0";
            //this.InitPageLanguage();
            //this.InitGridLanguage(this.ultraGridMain);
        }
    }
}