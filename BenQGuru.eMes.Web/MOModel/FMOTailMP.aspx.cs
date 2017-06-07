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
using BenQGuru.eMES.Common;
using BenQGuru.eMES.DataCollect;
using BenQGuru.eMES.Domain.DataCollect;
using BenQGuru.eMES.Domain.Material;
using BenQGuru.eMES.Domain.OQC;
using BenQGuru.eMES.Domain.Warehouse;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.OQC;
using BenQGuru.eMES.Web.Helper;
using UserControl;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;

namespace BenQGuru.eMES.Web.MOModel
{
    public partial class FMOTailMP : BaseMPageNew
    {
        private System.ComponentModel.IContainer components = null;
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;

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
            // 
            // languageComponent1
            // 
            this.languageComponent1.Language = "CHS";
            this.languageComponent1.LanguagePackageDir = "D:\\SQC2.0\\eMES\\Source\\bin";
            this.languageComponent1.RuntimePage = null;
            this.languageComponent1.RuntimeUserControl = null;
            this.languageComponent1.UserControlName = "";
        }

        protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
        {
            return this.languageComponent1;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.InitPageLanguage(this.languageComponent1, false);

                this.lblSplitlbl.Text = this.languageComponent1.GetString("$PageControl_Splitlbl");
                this.lblcraplbl.Text = this.languageComponent1.GetString("$PageControl_craplbl");
                this.InitData();
            }
        }

        protected override void InitWebGrid()
        {
            base.InitWebGrid();
            this.gridHelper.AddColumn("RunningCard", "产品序列号", null);
            this.gridHelper.AddColumn("OPCode", "工序", null);
            this.gridHelper.AddColumn("Status", "状态", null);
            this.gridHelper.AddColumn("Line", "产线", null);
            this.gridHelper.AddColumn("ResCode", "资源", null);
            this.gridHelper.AddColumn("TestDate", "测试日期", null);
            this.gridHelper.AddColumn("TestTime", "测试时间", null);

            this.gridHelper.AddDefaultColumn(true, false);
            this.gridHelper.ApplyLanguage(this.languageComponent1);

            this.gridHelper.RequestData();
        }

        private void InitData()
        {
            string moCode = Request.QueryString["MOCode"];

            object mo = this._MOFacade.GetMO(moCode.ToUpper());
            if (mo == null)
            {
                WebInfoPublish.Publish(this, "$CS_MO_Not_Exist $CS_Param_MOCode" + moCode, this.languageComponent1);
                return;
            }

            this.txtMoCodeQuery.Text = moCode;
        }

        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            return this._MOFacade.GetMOTailList(this.txtMoCodeQuery.Text.Trim(), inclusive, exclusive);
        }

        protected override int GetRowCount()
        {
            return this._MOFacade.GetMOTailListCount(this.txtMoCodeQuery.Text.Trim());
        }

        protected override DataRow GetGridRow(object obj)
        {
            SimulationReport sr = obj as SimulationReport;
            DataRow row = DtSource.NewRow();
            row["RunningCard"] = sr.RunningCard;
            row["OPCode"] = sr.OPCode;
            row["Status"] = this.languageComponent1.GetString(sr.Status);
            row["Line"] = sr.StepSequenceCode;
            row["ResCode"] = sr.ResourceCode;
            row["TestDate"] = FormatHelper.ToDateString(sr.MaintainDate);
            row["TestTime"] = FormatHelper.ToTimeString(sr.MaintainTime);
            return row;
        }

        #region Export
        protected override string[] FormatExportRecord(object obj)
        {
            SimulationReport sr = obj as SimulationReport;
            return new string[]{
                                sr.RunningCard,
                                sr.OPCode,
                                this.languageComponent1.GetString(sr.Status),
                                sr.StepSequenceCode,
                                sr.ResourceCode,
                                FormatHelper.ToDateString(sr.MaintainDate),
                                FormatHelper.ToTimeString(sr.MaintainTime)
            };
        }

        protected override string[] GetColumnHeaderText()
        {
            return new string[] {	"RunningCard",
									"OPCode",	
									"Status",
                                    "Line",
                                    "ResCode",
                                    "TestDate",
									"TestTime"
                                };
        }

        #endregion

        protected void cmdSplit_ServerClick(object sender, System.EventArgs e)
        {
            ArrayList selected = this.gridHelper.GetCheckedRows();
            string rcard = string.Empty;
            string mocode = this.txtMoCodeQuery.Text.Trim();

            this.DataProvider.BeginTransaction();
            try
            {
                SimulationReport simulationReport;
                DataCollectFacade dcf = new DataCollectFacade(this.DataProvider);
                InventoryFacade inventoryFacade = new InventoryFacade(this.DataProvider);
                OQCFacade oqcFacade = new OQCFacade(this.DataProvider);
                DBDateTime currentDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);

                foreach (GridRecord row in selected)
                {
                    rcard = row.Items.FindItemByKey("RunningCard").Value.ToString();
                    simulationReport = dcf.GetLastSimulationReport(rcard);

                    if (simulationReport == null)
                    {
                        throw new Exception("$NoProductInfo $CS_Param_ID=" + rcard);
                    }

                    if (simulationReport.IsComplete == "1")
                    {
                        throw new Exception("$CS_ERROR_PRODUCT_ALREADY_COMPLETE $CS_Param_ID=" + rcard);
                    }

                    //在库不能拆解
                    object[] stackToRcards = inventoryFacade.QueryStacktoRcardByRcardAndCarton(simulationReport.RunningCard, simulationReport.CartonCode);

                    if (stackToRcards != null)
                    {
                        throw new Exception("$CS_ERROR_PRODUCT_ALREADY_IN_Storge $CS_Param_ID=" + rcard);
                    }
                    //end

                    //批样本检查，不允许拆批
                    object[] lot2CardCheckList = oqcFacade.ExtraQueryOQCLot2CardCheck(simulationReport.RunningCard, string.Empty, simulationReport.MOCode, simulationReport.LOTNO, string.Empty);

                    if (lot2CardCheckList != null && lot2CardCheckList.Length > 0)
                    {
                        throw new Exception("$CS_ERROR_PRODUCT_ALREADY_IN_OQCLOTEXAMING $CS_Param_ID=" + rcard);
                    }
                    //end

                    this._MOFacade.DoSplit(simulationReport, currentDateTime, this.GetUserCode());

                    //脱离批
                    dcf.TryToDeleteRCardFromLot(simulationReport.RunningCard, false);

                    //拆Carton和栈板
                    Messages msg = new Messages();
                    msg.AddMessages(dcf.RemoveFromCarton(simulationReport.RunningCard, this.GetUserCode()));
                    msg.AddMessages(dcf.RemoveFromPallet(simulationReport.RunningCard, this.GetUserCode(), true));
                }

                this.DataProvider.CommitTransaction();

                this.gridHelper.RequestData();
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                ExceptionManager.Raise(this.GetType(), "$Error_RCardSplitError", ex);
            }
        }

        protected void cmdScrap_ServerClick(object sender, System.EventArgs e)
        {
            ArrayList selected = this.gridHelper.GetCheckedRows();

            string rcard = string.Empty;
            string mocode = this.txtMoCodeQuery.Text.Trim();

            this.DataProvider.BeginTransaction();
            try
            {
                SimulationReport simulationReport;
                DataCollectFacade dcf = new DataCollectFacade(this.DataProvider);
                InventoryFacade inventoryFacade = new InventoryFacade(this.DataProvider);
                DBDateTime currentDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);

                foreach (GridRecord row in selected)
                {
                    rcard = row.Items.FindItemByKey("RunningCard").Value.ToString();
                    simulationReport = dcf.GetLastSimulationReport(rcard);

                    if (simulationReport == null)
                    {
                        throw new Exception("$NoProductInfo $CS_Param_ID=" + rcard);
                    }

                    if (simulationReport.IsComplete == "1")
                    {
                        throw new Exception("$CS_ERROR_PRODUCT_ALREADY_COMPLETE $CS_Param_ID=" + rcard);
                    }

                    //在库不能拆解
                    object[] stackToRcards = inventoryFacade.QueryStacktoRcardByRcardAndCarton(simulationReport.RunningCard, simulationReport.CartonCode);

                    if (stackToRcards != null)
                    {
                        throw new Exception("$CS_ERROR_PRODUCT_ALREADY_IN_Storge $CS_Param_ID=" + rcard);
                    }
                    //end

                    this._MOFacade.DoScrap(simulationReport, currentDateTime, this.GetUserCode());

                    //拆Carton和栈板
                    Messages msg = new Messages();
                    msg.AddMessages(dcf.RemoveFromCarton(simulationReport.RunningCard, this.GetUserCode()));
                    msg.AddMessages(dcf.RemoveFromPallet(simulationReport.RunningCard, this.GetUserCode(), true));
                }

                this.DataProvider.CommitTransaction();

                this.gridHelper.RequestData();
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                ExceptionManager.Raise(this.GetType(), "$Error_RCardScrapError", ex);
            }
        }

        protected void cmdCancel_ServerClick(object sender, System.EventArgs e)
        {
            Response.Redirect(this.MakeRedirectUrl("FMOMP.aspx"));
        }
    }
}
