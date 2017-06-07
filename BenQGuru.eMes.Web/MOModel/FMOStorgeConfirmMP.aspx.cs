using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Domain.Warehouse;
using BenQGuru.eMES.SAPDataTransfer;
using BenQGuru.eMES.SAPDataTransferInterface;

namespace BenQGuru.eMES.Web.MOModel
{
    public partial class FMOStorgeConfirmMP : BenQGuru.eMES.Web.Helper.BasePage
    {

        private System.ComponentModel.IContainer components = null;
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private BenQGuru.eMES.Web.Helper.GridHelper gridHelper = null;
        private MOFacade m_MOFacade = null;
        private MOFacade _MOFacade
        {
            get
            {
                if (this.m_MOFacade == null)
                {
                    this.m_MOFacade = new FacadeFactory(base.DataProvider).CreateMOFacade();
                }
                return this.m_MOFacade;
            }
        }

        protected override void OnInit(EventArgs e)
        {
            this.InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.languageComponent1 = new ControlLibrary.Web.Language.LanguageComponent(this.components);
            this.gridWebGrid.ClickCellButton += new Infragistics.WebUI.UltraWebGrid.ClickCellButtonEventHandler(this.gridWebGrid_ClickCellButton);
            // 
            // languageComponent1
            // 
            this.languageComponent1.Language = "CHS";
            this.languageComponent1.LanguagePackageDir = "D:\\SQC2.0\\eMES\\Source\\bin";
            this.languageComponent1.RuntimePage = null;
            this.languageComponent1.RuntimeUserControl = null;
            this.languageComponent1.UserControlName = "";
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            this.InitOnPostBack();

            if (!Page.IsPostBack)
            {
                this.InitPageLanguage(this.languageComponent1, false);

                this.InitWebGrid();
                this.InitData();

                this.gridHelper.RequestData();
            }
        }

        private void InitWebGrid()
        {
            this.gridHelper.AddColumn("PostSequence", "报工批次", null);
            this.gridHelper.AddColumn("ConfirmQty", "报工数", null);
            this.gridHelper.AddColumn("ScrapQty", "报废数", null);
            this.gridHelper.AddColumn("ConfirmStatus", "确认状态", null);
            this.gridHelper.AddColumn("ManHour", "工时", null);
            this.gridHelper.AddColumn("MachineHour", "机时", null);
            this.gridHelper.AddColumn("MOLocation", "库存地点", null);
            this.gridHelper.AddColumn("Grade", "等级", null);
            this.gridHelper.AddColumn("MOCloseDate", "记账日期", null);
            this.gridHelper.AddColumn("MOOP", "工序", null);
            this.gridHelper.AddDefaultColumn(false, true);

            this.gridHelper.AddColumn("ConfirmFlag", "报工状态", null);

            this.gridHelper.AddLinkColumn("ErrorMessage", "错误信息", null);
            this.gridHelper.AddLinkColumn("SyncStatus", "状态同步", null);
            this.gridHelper.AddLinkColumn("ConfirmAgain", "再次报工", null);

            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }

        private void InitData()
        {
            string moCode = Request.QueryString["MOCode"];
            int orgID = int.Parse(Request.QueryString["OrgID"]);

            object mo = this._MOFacade.GetMO(moCode.ToUpper());
            if (mo == null)
            {
                WebInfoPublish.Publish(this, "$CS_MO_Not_Exist $CS_Param_MOCode" + moCode, this.languageComponent1);
                return;
            }

            this.txtMoCodeQuery.Text = moCode;
            this.txtMOEAttribute2Query.Text = ((MO)mo).EAttribute2;
            this.txtCompleteQtyQuery.Text = Convert.ToInt32(((MO)mo).MOActualQty).ToString();
            this.txtScrapQtyQuery.Text = Convert.ToInt32(((MO)mo).MOScrapQty).ToString();
            MO2SAP mo2sap = this.GetMO2SAPSumQtyObject(moCode);
            this.txtCompleteQtyConfirmedQuery.Text = mo2sap.MOProduced.ToString();
            this.txtScrapQtyConfirmedQuery.Text = mo2sap.MOScrap.ToString();

            //this.txtConfirmQtyEdit.Text = Convert.ToInt32(((MO)mo).MOActualQty - mo2sap.MOProduced).ToString();
            //this.txtConfirmScrapQtyEdit.Text = Convert.ToInt32(((MO)mo).MOScrapQty - mo2sap.MOScrap).ToString();

            this.drpConfirmStatusEdit.Items.Clear();
            this.drpConfirmStatusEdit.Items.Add("");
            this.drpConfirmStatusEdit.Items.Add("X");
            this.drpConfirmStatusEdit.SelectedIndex = -1;

            this.txtManHourEdit.Text = "";
            this.txtMachineHourEdit.Text = "";
            this.txtMOLocationEdit.Text = "";
            //this.txtGradeEdit.Text = "H";
            //this.txtOPCodeEdit.Text = ((MO)mo).MOOP;
            this.dateConfirmDateEdit.Date_DateTime = DateTime.Now.Date;

        }

        private MO2SAP GetMO2SAPSumQtyObject(string moCode)
        {
            return this._MOFacade.GetMO2SAPSumQty(moCode) as MO2SAP;
        }

        private void InitOnPostBack()
        {
            this.gridHelper = new GridHelper(this.gridWebGrid);
            this.gridHelper.LoadDataSourceHandle = new LoadDataSourceDelegate(this.LoadDataSource);
            this.gridHelper.BuildGridRowhandle = new BuildGridRowDelegate(this.GetGridRow);
        }

        private void gridWebGrid_ClickCellButton(object sender, Infragistics.WebUI.UltraWebGrid.CellEventArgs e)
        {
            MO2SAP mo2sap = this._MOFacade.GetMO2SAP(this.txtMoCodeQuery.Text.ToUpper().Trim(), decimal.Parse(e.Cell.Row.Cells[0].Text)) as MO2SAP;
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);

            if (this.gridHelper.IsClickColumn("SyncStatus", e))
            {
                if (string.IsNullOrEmpty(e.Cell.Row.Cells.FromKey("ConfirmFlag").Text.ToString().Trim()))
                {
                    WebInfoPublish.Publish(this, "$Error_StatusNotAllowEmpty", this.languageComponent1);
                    return;
                }

                if (string.Compare(mo2sap.Flag, "MES", true) == 0)
                {
                    this.DataProvider.BeginTransaction();
                    try
                    {
                        // Update MO2SAP.Flag
                        mo2sap.Flag = "SAP";
                        mo2sap.ErrorMessage = "";
                        mo2sap.MaintainUser = this.GetUserCode();
                        mo2sap.MaintainDate = dbDateTime.DBDate;
                        mo2sap.MaintainTime = dbDateTime.DBTime;

                        this._MOFacade.UpdateMO2SAP(mo2sap);

                        // Update MO2SAPLog.active
                        this._MOFacade.UpdateMO2SAPLogStatus(mo2sap.MOCode, mo2sap.PostSequence);

                        e.Cell.Row.Cells[11].Text = "SAP";
                        e.Cell.Row.Cells[12].Text = "";
                        this.DataProvider.CommitTransaction();
                    }
                    catch (Exception ex)
                    {
                        this.DataProvider.RollbackTransaction();
                        WebInfoPublish.Publish(this, ex.Message, this.languageComponent1);
                    }
                }
                else
                {
                    WebInfoPublish.Publish(this, "$Error_OnlyMESStatusCanDo", this.languageComponent1);
                }
            }
            else if (this.gridHelper.IsClickColumn("ConfirmAgain", e))
            {
                if (string.IsNullOrEmpty(e.Cell.Row.Cells.FromKey("ConfirmFlag").Text.ToString().Trim()))
                {
                    WebInfoPublish.Publish(this, "$Error_StatusNotAllowEmpty", this.languageComponent1);
                    return;
                }

                if (string.Compare(mo2sap.Flag, "MES", true) == 0)
                {
                    // Update MO2SAP
                    mo2sap.MaintainUser = this.GetUserCode();
                    mo2sap.MaintainDate = dbDateTime.DBDate;
                    mo2sap.MaintainTime = dbDateTime.DBTime;

                    this._MOFacade.UpdateMO2SAP(mo2sap);

                    // Update MO2SAPLog.active 
                    this._MOFacade.UpdateMO2SAPLogStatus(mo2sap.MOCode, mo2sap.PostSequence);

                    this.CallMOConfirm(mo2sap);
                }
                else
                {
                    WebInfoPublish.Publish(this, "$Error_OnlyMESStatusCanDo", this.languageComponent1);
                }
            }
            else if (this.gridHelper.IsClickColumn("ErrorMessage", e))
            {
                if (string.IsNullOrEmpty(e.Cell.Row.Cells.FromKey("ConfirmFlag").Text.ToString().Trim()))
                {
                    WebInfoPublish.Publish(this, "$Error_StatusNotAllowEmpty", this.languageComponent1);
                    return;
                }

                if (string.Compare(mo2sap.Flag, "MES", true) == 0)
                {
                    Response.Redirect(this.MakeRedirectUrl("FMO2SAPLogQP.aspx", new string[] { "MOCode", "PostSeq", "PageName" }, new string[] { mo2sap.MOCode, mo2sap.PostSequence.ToString(), "FMOStorgeConfirmMP.aspx" }));
                }
                else
                {
                    WebInfoPublish.Publish(this, "$Error_OnlyMESStatusCanDo", this.languageComponent1);
                }
            }
            else if (this.gridHelper.IsClickColumn("Edit", e))
            {
                if (e.Cell.Row.Cells.FromKey("ConfirmFlag").Text.ToString().Trim() == string.Empty)
                {
                    this.SetEditObject(e.Cell.Row);
                    //this.buttonHelper.PageActionStatusHandle(PageActionType.Update);
                }
                else
                {
                    WebInfoPublish.Publish(this, "$Error_OnlyNotStatusCanDo", this.languageComponent1);
                }
            }
            else
            {
                return;
            }
        }

        protected void cmdQuery_ServerClick(object sender, System.EventArgs e)
        {
            this.gridHelper.RequestData();
            this.InitData();

            this.txtConfirmQtyEdit.Text = string.Empty;
            this.txtConfirmScrapQtyEdit.Text = string.Empty;
            this.txtGradeEdit.Text = string.Empty;
        }

        private object[] LoadDataSource(int inclusive, int exclusive)
        {
            return this._MOFacade.QueryMO2SAPDetailList(this.txtMoCodeQuery.Text.Trim().ToUpper());
        }

        protected Infragistics.WebUI.UltraWebGrid.UltraGridRow GetGridRow(object obj)
        {
            MO2SAP mo2SAP = obj as MO2SAP;

            return new Infragistics.WebUI.UltraWebGrid.UltraGridRow(
                new object[] {
                    mo2SAP.PostSequence.ToString(),
                    mo2SAP.MOProduced.ToString(),
                    mo2SAP.MOScrap.ToString(),
                    mo2SAP.MOConfirm.ToString(),
                    mo2SAP.MOManHour.ToString(),
                    mo2SAP.MOMachineHour.ToString(),
                    mo2SAP.MOLocation.ToString(),
                    mo2SAP.MOGrade.ToString(),
                    FormatHelper.ToDateString(mo2SAP.MOCloseDate),
                    mo2SAP.MOOP.ToString(),
                    "",
                    mo2SAP.Flag.ToString(),
                    "","",""
                });
        }

        protected void cmdMOConfirm_ServerClick(object sender, EventArgs e)
        {
            if (this.ValidInput())
            {
                object[] stackToRcardList = this._MOFacade.QueryRcardFromStack2Rcard(FormatHelper.CleanString(this.txtGradeEdit.Text.Trim().ToString()),
                                                                                      FormatHelper.CleanString(this.txtMOLocationEdit.Text.Trim().ToString()),
                                                                                      FormatHelper.CleanString(this.txtMoCodeQuery.Text.Trim().ToUpper()));
                if (stackToRcardList == null)
                {
                    WebInfoPublish.Publish(this, "$STORGE_HAVE_NORCARD", this.languageComponent1);
                    return;
                }

                int getCount = stackToRcardList.Length;
                MO2SAP mo2sap = this.GetMO2SAP(getCount);

                this.DataProvider.BeginTransaction();
                try
                {

                    DBDateTime dBDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
                    for (int i = 0; i < stackToRcardList.Length; i++)
                    {
                        MO2SAPDetail mo2sapdetail = new MO2SAPDetail();

                        mo2sapdetail.MOCode = FormatHelper.CleanString(this.txtMoCodeQuery.Text.Trim().ToString());
                        mo2sapdetail.PostSequence = mo2sap.PostSequence;
                        mo2sapdetail.RuningCrad = ((StackToRcard)stackToRcardList[i]).SerialNo;
                        mo2sapdetail.MaintainUser = this.GetUserCode();
                        mo2sapdetail.MaintainDate = dBDateTime.DBDate;
                        mo2sapdetail.MaintainTime = dBDateTime.DBTime;

                        this._MOFacade.AddMO2SAPDetail(mo2sapdetail);
                    }
                   
                    this._MOFacade.AddMO2SAP(mo2sap);

                }
                catch (Exception ex)
                {
                    this.DataProvider.RollbackTransaction();
                    WebInfoPublish.Publish(this, ex.Message, this.languageComponent1);
                }
                finally
                {
                    this.DataProvider.CommitTransaction();
                    this.CallMOConfirm(mo2sap);
                }                

                this.gridHelper.RequestData();
                this.ClearValue();
            }
        }

        private void ClearValue()
        {            
            this.txtManHourEdit.Text = string.Empty;
            this.drpConfirmStatusEdit.SelectedIndex =-1 ;
            this.txtMachineHourEdit.Text = string.Empty;
            this.txtMOLocationEdit.Text = string.Empty;
            this.txtOPCodeEdit.Text = string.Empty;

            this.InitData();

            this.txtConfirmQtyEdit.Text = string.Empty;
            this.txtConfirmScrapQtyEdit.Text = string.Empty;
            this.txtGradeEdit.Text = string.Empty;
        }

        public void CallMOConfirm(MO2SAP mo2sap)
        {
            MOConfirm moConfirm = new MOConfirm();

            MOConfirmArgument moargument = new MOConfirmArgument(this.DataProvider);
            moargument.OrgID = mo2sap.OrganizationID;
            moargument.MOList = new System.Collections.Generic.List<DT_MES_POCONFIRM_REQPOLIST>();
            moargument.MOList.Add(this.GenerateMOArgument(mo2sap));
            moConfirm.SetArguments(moargument);
            ServiceResult sr = moConfirm.Run(BenQGuru.eMES.SAPDataTransferInterface.RunMethod.Manually);

            string moCode = mo2sap.MOCode;
            object mo = this._MOFacade.GetMO(moCode.ToUpper());
            if (mo == null)
            {
                WebInfoPublish.Publish(this, "$CS_MO_Not_Exist $CS_Param_MOCode" + moCode, this.languageComponent1);
                return;
            }

            this.txtMoCodeQuery.Text = moCode;
            this.txtMOEAttribute2Query.Text = ((MO)mo).EAttribute2;
            this.txtCompleteQtyQuery.Text = Convert.ToInt32(((MO)mo).MOActualQty).ToString();
            this.txtScrapQtyQuery.Text = Convert.ToInt32(((MO)mo).MOScrapQty).ToString();
            MO2SAP mo2sapTemp = this.GetMO2SAPSumQtyObject(moCode);
            this.txtCompleteQtyConfirmedQuery.Text = mo2sapTemp.MOProduced.ToString();
            this.txtScrapQtyConfirmedQuery.Text = mo2sapTemp.MOScrap.ToString();

            this.txtConfirmQtyEdit.Text = Convert.ToInt32(((MO)mo).MOActualQty - mo2sapTemp.MOProduced).ToString();
            this.txtConfirmScrapQtyEdit.Text = Convert.ToInt32(((MO)mo).MOScrapQty - mo2sapTemp.MOScrap).ToString();

            this.gridHelper.RequestData();

            if (sr.Result == true)
            {

            }
            else
            {
                WebInfoPublish.Publish(this, sr.Message + " Transaction Code=" + sr.TransactionCode, this.languageComponent1);
            }
        }

        private bool ValidInput()
        {
            PageCheckManager manager = new PageCheckManager();
            manager.Add(new NumberCheck(this.lblConfirmCompleteQtyEdit, this.txtConfirmQtyEdit, 1,
                    Convert.ToInt32(decimal.Parse(this.txtCompleteQtyQuery.Text) - decimal.Parse(this.txtCompleteQtyConfirmedQuery.Text)), true));
            manager.Add(new NumberCheck(this.lblConfirmScrapQtyEdit, this.txtConfirmScrapQtyEdit, 0,
                    Convert.ToInt32(decimal.Parse(this.txtScrapQtyQuery.Text) - decimal.Parse(this.txtScrapQtyConfirmedQuery.Text)), true));

            if (this.txtManHourEdit.Text.Trim().Length > 0)
            {
                manager.Add(new NumberCheck(this.lblManHourEdit, this.txtManHourEdit, false));
            }
            if (this.txtMachineHourEdit.Text.Trim().Length > 0)
            {
                manager.Add(new NumberCheck(this.lblMachineHourEdit, this.txtMachineHourEdit, false));
            }

            manager.Add(new DateCheck(this.lblConfirmDateEdit,this.dateConfirmDateEdit.Text,false));

            manager.Add(new LengthCheck(this.lblMOLocationEdit, this.txtMOLocationEdit, 40, true));
            manager.Add(new LengthCheck(this.lblGradeEdit, this.txtGradeEdit, 10, false));


            if (!manager.Check())
            {
                WebInfoPublish.Publish(this, manager.CheckMessage, languageComponent1);
                return false;
            }
            return true;
        }

        private void SetEditObject(Infragistics.WebUI.UltraWebGrid.UltraGridRow row)
        {
            this.txtConfirmQtyEdit.Text = row.Cells.FromKey("ConfirmQty").Text.ToString();
            this.txtConfirmScrapQtyEdit.Text = row.Cells.FromKey("ScrapQty").Text.ToString();
            this.txtMOLocationEdit.Text = row.Cells.FromKey("MOLocation").Text.ToString();
            this.txtGradeEdit.Text = row.Cells.FromKey("Grade").Text.ToString();
            this.txtOPCodeEdit.Text = row.Cells.FromKey("MOOP").Text.ToString();

            this.txtConfirmQtyEdit.Enabled = false;
            this.txtMOLocationEdit.Enabled = false;
            this.txtGradeEdit.Enabled = false;
            this.txtOPCodeEdit.Enabled = false;

        }

        private DT_MES_POCONFIRM_REQPOLIST GenerateMOArgument(MO2SAP mo2sap)
        {
            DT_MES_POCONFIRM_REQPOLIST req = new DT_MES_POCONFIRM_REQPOLIST();
            req.MOCloseDate = mo2sap.MOCloseDate.ToString();
            req.MOCode = mo2sap.MOCode;
            req.MOconfirm = mo2sap.MOConfirm;
            req.MOGrade = mo2sap.MOGrade;
            req.MOLocation = mo2sap.MOLocation;
            req.MOMachineHour = mo2sap.MOMachineHour.ToString();
            req.MOManHour = mo2sap.MOManHour.ToString();
            req.MOOP = mo2sap.MOOP;
            req.MOProducet = mo2sap.MOProduced.ToString();
            req.MOScrap = mo2sap.MOScrap.ToString();
            req.PostSeq = mo2sap.PostSequence.ToString();
            return req;
        }

        private MO2SAP GetMO2SAP(int getCount)
        {
            DBDateTime currentDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            MO2SAP mo2sap = new MO2SAP();

            mo2sap.EAttribute1 = "";
            mo2sap.ErrorMessage = "";
            mo2sap.Flag = "MES";
            mo2sap.MaintainUser = base.GetUserCode();
            mo2sap.MOCloseDate = this.dateConfirmDateEdit.Text == "" ? 0 : FormatHelper.TODateInt(this.dateConfirmDateEdit.Date_DateTime);
            mo2sap.MOCode = this.txtMoCodeQuery.Text.ToUpper();
            mo2sap.MOConfirm = this.drpConfirmStatusEdit.SelectedValue;
            mo2sap.MOGrade = this.txtGradeEdit.Text.Trim().Length == 0 ? "" : this.txtGradeEdit.Text.ToUpper().Trim();
            mo2sap.MOLocation = this.txtMOLocationEdit.Text.Trim();
            mo2sap.MOMachineHour = this.txtMachineHourEdit.Text.Trim().Length == 0 ? "" : this.txtMachineHourEdit.Text.Trim();
            mo2sap.MOManHour = this.txtManHourEdit.Text.Trim().Length == 0 ? "" : this.txtManHourEdit.Text.Trim();
            mo2sap.MOOP = this.txtOPCodeEdit.Text;
            mo2sap.MOProduced = getCount;
            mo2sap.MOScrap = decimal.Parse(this.txtConfirmScrapQtyEdit.Text.Trim());
            mo2sap.PostSequence = (this._MOFacade.GetMO2SAPMaxPostSeq(mo2sap.MOCode) as MO2SAP).PostSequence + 1;
            mo2sap.OrganizationID = GlobalVariables.CurrentOrganizations.First().OrganizationID;
            mo2sap.MaintainDate = currentDateTime.DBDate;
            mo2sap.MaintainTime = currentDateTime.DBTime;
            return mo2sap;
        }
    }
}
