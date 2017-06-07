using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.WebQuery;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;

namespace BenQGuru.eMES.Web.WebQuery
{
    /// <summary>
    /// FOnWipResourceQP 的摘要说明。
    /// </summary>
    public partial class FOnWipResourceQP : BaseQPageNew
    {

        protected WebQueryHelperNew _helper = null;
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;
        //protected GridHelper gridHelper = null;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            this.gridHelper = new GridHelperNew(this.gridWebGrid, DtSource);
            this._helper = new WebQueryHelperNew(null, this.cmdGridExport, this.gridWebGrid, this.pagerSizeSelector, this.pagerToolBar, this.languageComponent1, DtSource);
            this._helper.LoadGridDataSource += new EventHandler(_helper_LoadGridDataSource);
            this._helper.DomainObjectToGridRow += new EventHandler(_helper_DomainObjectToGridRow);
            this._helper.DomainObjectToExportRow += new EventHandler(_helper_DomainObjectToExportRow);
            this._helper.GetExportHeadText += new EventHandler(_helper_GetExportHeadText);
            //this._helper.GridCellClick +=new EventHandler(_helper_GridCellClick);

            if (!this.IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);

                /* added by jessie lee, 2005/12/30,
                 * 在在制分布页面判断工序是否为FQC工序 */
                bool isFQC = false;
                if (string.Compare(this.GetRequestParam("OperationCode"), "TS", true) != 0)
                {
                    FacadeFactory facadeFactory = new FacadeFactory(base.DataProvider);
                    isFQC = facadeFactory.CreateQueryFacade3().IsOpFQC(
                        this.GetRequestParam("OperationCode"),
                        this.GetRequestParam("ItemCode"),
                        this.GetRequestParam("MoCode"),
                        FormatHelper.TODateInt(this.GetRequestParam("STARTDATE")),
                        FormatHelper.TODateInt(this.GetRequestParam("ENDDATE")));
                }
                this.ViewState["IsFQC"] = isFQC;

                this._initialWebGrid();

                this.txtItemCodeQuery.Text = this.GetRequestParam("ItemCode");
                this.txtMoCodeQuery.Text = this.GetRequestParam("MoCode");
                this.txtOperationCodeQuery.Text = this.GetRequestParam("OperationCode");
                this.txtStartDate.Value = this.GetRequestParam("STARTDATE");
                this.txtEndDate.Value = this.GetRequestParam("ENDDATE");

                this._helper.Query(sender);

            }
        }

        private void _initialWebGrid()
        {
            base.InitWebGrid();
            if (string.Compare(this.GetRequestParam("OperationCode").ToString(), "TS", true) != 0)
            {
                this.gridHelper.AddColumn("SegmentCode", "工段", null);
                this.gridHelper.AddColumn("ShiftDay", "日期", null);
                this.gridHelper.AddColumn("ShiftCode", "班次", null);
                this.gridHelper.AddColumn("StepSequenceCode", "生产线", null);
                this.gridHelper.AddColumn("ResourceCode", "资源", null);
                this.gridHelper.AddColumn("MoCode", "工单", null);
                this.gridHelper.AddColumn("OnWipGoodQuantityOnResource", "在制良品数量", null);
                this.gridHelper.AddLinkColumn("OnWipGoodDistributing", "在制良品明细", null);
                this.gridHelper.AddColumn("OnWipNGQuantityOnResource", "在制不良品数量", null);
                this.gridHelper.AddLinkColumn("OnWipNGDistributing", "在制不良品明细", null);

                if (bool.Parse(this.ViewState["IsFQC"].ToString()))
                {
                    this.gridHelper.AddColumn("OQCNGWaitForRework", "判退待返工数量", null);
                    this.gridHelper.AddLinkColumn("OQCNGWaitForReworkDistributing", "判退待返工数量明细", null);
                    //this.gridWebGrid.Bands[0].Columns.FromKey("OQCNGWaitForReworkDistributing").Width = new Unit(60);
                }

                //this.gridWebGrid.Bands[0].Columns.FromKey("OnWipGoodDistributing").Width = new Unit(60);
                //this.gridWebGrid.Bands[0].Columns.FromKey("OnWipNGDistributing").Width = new Unit(60);
            }
            else
            {
                /* 维修 */
                this.gridHelper.AddColumn("SegmentCode", "工段", null);
                this.gridHelper.AddColumn("ShiftDay", "日期", null);
                this.gridHelper.AddColumn("ShiftCode", "班次", null);
                this.gridHelper.AddColumn("StepSequenceCode", "生产线", null);
                this.gridHelper.AddColumn("ResourceCode", "资源", null);
                this.gridHelper.AddColumn("MoCode", "工单", null);

                this.gridHelper.AddColumn("TSConfirmQty", "待修数量", null);
                this.gridHelper.AddLinkColumn("TSConfirmQtyDistributing", "待修数量明细", null);
                //this.gridWebGrid.Bands[0].Columns.FromKey("TSConfirmQtyDistributing").Width = new Unit(60);

                this.gridHelper.AddColumn("TSQty", "维修中数量", null);
                this.gridHelper.AddLinkColumn("TSQtyDistributing", "维修中数量明细", null);
                //this.gridWebGrid.Bands[0].Columns.FromKey("TSQtyDistributing").Width = new Unit(60);

                this.gridHelper.AddColumn("TSReflowQty", "待回流数量", null);
                this.gridHelper.AddLinkColumn("TSReflowQtyDistributing", "待回流数量", null);
                //this.gridWebGrid.Bands[0].Columns.FromKey("TSReflowQtyDistributing").Width = new Unit(60);

            }

            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }

        #region Web 窗体设计器生成的代码
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// 设计器支持所需的方法 - 不要使用代码编辑器修改
        /// 此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.languageComponent1 = new ControlLibrary.Web.Language.LanguageComponent(this.components);
            // 
            // languageComponent1
            // 
            this.languageComponent1.Language = "CHS";
            this.languageComponent1.LanguagePackageDir = "\\\\grd2-build\\language pack\\";
            this.languageComponent1.RuntimePage = null;
            this.languageComponent1.RuntimeUserControl = null;
            this.languageComponent1.UserControlName = "";

        }
        #endregion

        private void _helper_LoadGridDataSource(object sender, EventArgs e)
        {
            FacadeFactory facadeFactory = new FacadeFactory(base.DataProvider);
            (e as WebQueryEventArgsNew).GridDataSource = facadeFactory.CreateQueryFacade3().QueryOnWipInfoOnResource(
                this.txtItemCodeQuery.Text,
                this.txtMoCodeQuery.Text,
                this.txtOperationCodeQuery.Text,
                FormatHelper.TODateInt(this.txtStartDate.Value),
                FormatHelper.TODateInt(this.txtEndDate.Value),
                (e as WebQueryEventArgsNew).StartRow,
                (e as WebQueryEventArgsNew).EndRow);

            (e as WebQueryEventArgsNew).RowCount =
                facadeFactory.CreateQueryFacade3().QueryOnWipInfoOnResourceCount(
                this.txtItemCodeQuery.Text,
                this.txtMoCodeQuery.Text,
                this.txtOperationCodeQuery.Text,
                FormatHelper.TODateInt(this.txtStartDate.Value),
                FormatHelper.TODateInt(this.txtEndDate.Value));
        }

        private void _helper_DomainObjectToGridRow(object sender, EventArgs e)
        {
            if ((e as DomainObjectToGridRowEventArgsNew).DomainObject != null)
            {
                if (string.Compare(this.GetRequestParam("OperationCode").ToString(), "TS", true) != 0)
                {
                    if (bool.Parse(this.ViewState["IsFQC"].ToString()))
                    {
                        /* FQC */
                        OnWipInfoOnResource obj = (e as DomainObjectToGridRowEventArgsNew).DomainObject as OnWipInfoOnResource;
                        DataRow row = DtSource.NewRow();
                        row["SegmentCode"] = obj.SegmentCode;
                        row["ShiftDay"] = FormatHelper.ToDateString(obj.ShiftDay);
                        row["ShiftCode"] = obj.ShiftCode;
                        row["StepSequenceCode"] = obj.StepSequenceCode;
                        row["ResourceCode"] = obj.ResourceCode;
                        row["MoCode"] = obj.MoCode;
                        row["OnWipGoodQuantityOnResource"] = obj.OnWipGoodQuantityOnResource;
                        row["OnWipGoodDistributing"] = "";
                        row["OnWipNGQuantityOnResource"] = obj.OnWipNGQuantityOnResource;
                        row["OnWipNGDistributing"] = "";
                        row["OQCNGWaitForRework"] = obj.NGForReworksQTY;
                        row["OQCNGWaitForReworkDistributing"] = "";
                        (e as DomainObjectToGridRowEventArgsNew).GridRow = row;
                    }
                    else
                    {
                        /* 普通 */
                        OnWipInfoOnResource obj = (e as DomainObjectToGridRowEventArgsNew).DomainObject as OnWipInfoOnResource;
                        DataRow row = DtSource.NewRow();
                        row["SegmentCode"] = obj.SegmentCode;
                        row["ShiftDay"] = FormatHelper.ToDateString(obj.ShiftDay);
                        row["ShiftCode"] = obj.ShiftCode;
                        row["StepSequenceCode"] = obj.StepSequenceCode;
                        row["ResourceCode"] = obj.ResourceCode;
                        row["MoCode"] = obj.MoCode;

                        row["OnWipGoodQuantityOnResource"] = obj.OnWipGoodQuantityOnResource;
                        row["OnWipGoodDistributing"] = "";
                        row["OnWipNGQuantityOnResource"] = obj.OnWipNGQuantityOnResource;
                        row["OnWipNGDistributing"] = "";

                        (e as DomainObjectToGridRowEventArgsNew).GridRow = row;
                          
                    }
                }
                else
                {
                    /* 维修 */
                    OnWipInfoOnResource obj = (e as DomainObjectToGridRowEventArgsNew).DomainObject as OnWipInfoOnResource;
                    DataRow row = DtSource.NewRow();
                    row["SegmentCode"] = obj.SegmentCode;
                    row["ShiftDay"] = FormatHelper.ToDateString(obj.ShiftDay);
                    row["ShiftCode"] = obj.ShiftCode;
                    row["StepSequenceCode"] = obj.StepSequenceCode;
                    row["ResourceCode"] = obj.ResourceCode;
                    row["MoCode"] = obj.MoCode;
                    row["TSConfirmQty"] = obj.TSConfirmQty;
                    row["TSConfirmQtyDistributing"] = "";
                    row["TSQty"] = obj.TSQty;
                    row["TSQtyDistributing"] = "";
                    row["TSReflowQty"] = obj.TSReflowQty;
                    row["TSReflowQtyDistributing"] = "";

                    (e as DomainObjectToGridRowEventArgsNew).GridRow = row;
                }
            }
        }

        private void _helper_DomainObjectToExportRow(object sender, EventArgs e)
        {
            if ((e as DomainObjectToExportRowEventArgsNew).DomainObject != null)
            {
                if (string.Compare(this.GetRequestParam("OperationCode").ToString(), "TS", true) != 0)
                {
                    if (bool.Parse(this.ViewState["IsFQC"].ToString()))
                    {
                        /* FQC */
                        OnWipInfoOnResource obj = (e as DomainObjectToExportRowEventArgsNew).DomainObject as OnWipInfoOnResource;
                        (e as DomainObjectToExportRowEventArgsNew).ExportRow =
                            new string[]{
											this.txtItemCodeQuery.Text,
											this.txtOperationCodeQuery.Text,
											obj.SegmentCode,
											FormatHelper.ToDateString( obj.ShiftDay ),
											obj.ShiftCode,
											obj.StepSequenceCode,
											obj.ResourceCode,
											obj.MoCode,
											obj.OnWipGoodQuantityOnResource.ToString(),
											obj.OnWipNGQuantityOnResource.ToString(),
											obj.NGForReworksQTY.ToString()
										};
                    }
                    else
                    {
                        /* 普通 */
                        OnWipInfoOnResource obj = (e as DomainObjectToExportRowEventArgsNew).DomainObject as OnWipInfoOnResource;
                        (e as DomainObjectToExportRowEventArgsNew).ExportRow =
                            new string[]{
											this.txtItemCodeQuery.Text,
											this.txtOperationCodeQuery.Text,
											obj.SegmentCode,
											FormatHelper.ToDateString( obj.ShiftDay ),
											obj.ShiftCode,
											obj.StepSequenceCode,
											obj.ResourceCode,
											obj.MoCode,
											obj.OnWipGoodQuantityOnResource.ToString(),
											obj.OnWipNGQuantityOnResource.ToString()
										};
                    }
                }
                else
                {
                    /* 维修 */
                    OnWipInfoOnResource obj = (e as DomainObjectToExportRowEventArgsNew).DomainObject as OnWipInfoOnResource;
                    (e as DomainObjectToExportRowEventArgsNew).ExportRow =
                        new string[]{
										this.txtItemCodeQuery.Text,
										this.txtOperationCodeQuery.Text,
										obj.SegmentCode,
										FormatHelper.ToDateString( obj.ShiftDay ),
										obj.ShiftCode,
										obj.StepSequenceCode,
										obj.ResourceCode,
										obj.MoCode,
										obj.TSConfirmQty.ToString(),
										obj.TSQty.ToString(),
										obj.TSReflowQty.ToString()
									};
                }
            }
        }

        private void _helper_GetExportHeadText(object sender, EventArgs e)
        {
            if (string.Compare(this.GetRequestParam("OperationCode").ToString(), "TS", true) != 0)
            {
                if (bool.Parse(this.ViewState["IsFQC"].ToString()))
                {
                    /* FQC */
                    (e as ExportHeadEventArgsNew).Heads =
                        new string[]{
										"ItemCode",
										"OperationCode",
										"SegmentCode",
										"ShiftDay",
										"ShiftCode",
										"StepSequenceCode",
										"ResourceCode",
										"MOCode",
										"OnWipGoodQuantityOnResource",
										"OnWipNGQuantityOnResource",
										"OQCNGWaitForRework"
									};
                }
                else
                {
                    /* Common */
                    (e as ExportHeadEventArgsNew).Heads =
                        new string[]{
										"ItemCode",
										"OperationCode",
										"SegmentCode",
										"ShiftDay",
										"ShiftCode",
										"StepSequenceCode",
										"ResourceCode",
										"MOCode",
										"OnWipGoodQuantityOnResource",
										"OnWipNGQuantityOnResource"
									};
                }
            }
            else
            {
                /* TS */
                (e as ExportHeadEventArgsNew).Heads =
                    new string[]{
									"ItemCode",
									"OperationCode",
									"SegmentCode",
									"ShiftDay",
									"ShiftCode",
									"StepSequenceCode",
									"ResourceCode",
									"MOCode",
									"TSConfirmQty",
									"TSQty",
									"TSReflowQty"
								};
            }
        }

        protected override void Grid_ClickCell(GridRecord row, string command)
        {

            if (command == "OnWipGoodDistributing")/* 在制良品明细 */
            {
                this.Response.Redirect(
                    this.MakeRedirectUrl(
                    "FOnWipDistributingQP.aspx",
                    new string[]{
									"Status",
									"ItemCode",
									"ShiftDay",
									"ShiftCode",
									"OperationCode",
									"ResourceCode",
									"MoCode",
									"StartDate",
									"EndDate" ,
									"MoCodes" ,
									"IsFQC"
								},
                    new string[]{
									"GOOD",
									this.txtItemCodeQuery.Text,
									row.Items.FindItemByKey("ShiftDay").Text,
									row.Items.FindItemByKey("ShiftCode").Text,
									this.txtOperationCodeQuery.Text,
									row.Items.FindItemByKey("ResourceCode").Text,
									row.Items.FindItemByKey("MoCode").Text,
									this.txtStartDate.Value,
									this.txtEndDate.Value,
									this.txtMoCodeQuery.Text,
									this.ViewState["IsFQC"].ToString()
									
				})
                    );
            }
            else if (command == "OnWipNGDistributing")/* 在制不良品明细 */
            {
                this.Response.Redirect(
                    this.MakeRedirectUrl(
                    "FOnWipDistributingQP.aspx",
                    new string[]{
									"Status",
									"ItemCode",
									"ShiftDay",
									"ShiftCode",
									"OperationCode",
									"ResourceCode",
									"MoCode",
									"StartDate",
									"EndDate" ,
									"MoCodes",
									"IsFQC"
								},
                    new string[]{
									"NG",
									this.txtItemCodeQuery.Text,
									row.Items.FindItemByKey("ShiftDay").Text,
									row.Items.FindItemByKey("ShiftCode").Text,
									this.txtOperationCodeQuery.Text,
									row.Items.FindItemByKey("ResourceCode").Text,
									row.Items.FindItemByKey("MoCode").Text,
									this.txtStartDate.Value,
									this.txtEndDate.Value,
									this.txtMoCodeQuery.Text,
									this.ViewState["IsFQC"].ToString()
								})
                    );
            }
            else if (command == "OQCNGWaitForReworkDistributing") /* Reject */
            {
                this.Response.Redirect(
                    this.MakeRedirectUrl(
                    "FOnWipDistributingQP.aspx",
                    new string[]{
									"Status",
									"ItemCode",
									"ShiftDay",
									"ShiftCode",
									"OperationCode",
									"ResourceCode",
									"MoCode",
									"StartDate",
									"EndDate" ,
									"MoCodes" ,
									"IsFQC"
								},
                    new string[]{
									"REJECT",
									this.txtItemCodeQuery.Text,
									row.Items.FindItemByKey("ShiftDay").Text,
									row.Items.FindItemByKey("ShiftCode").Text,
									this.txtOperationCodeQuery.Text,
									row.Items.FindItemByKey("ResourceCode").Text,
									row.Items.FindItemByKey("MoCode").Text,
									this.txtStartDate.Value,
									this.txtEndDate.Value,
									this.txtMoCodeQuery.Text,
									this.ViewState["IsFQC"].ToString()
								})
                    );
            }
            else if (command == "TSConfirmQtyDistributing") /* TS Confirm */
            {
                this.Response.Redirect(
                    this.MakeRedirectUrl(
                    "FOnWipDistributingQP.aspx",
                    new string[]{
									"Status",
									"ItemCode",
									"ShiftDay",
									"ShiftCode",
									"OperationCode",
									"ResourceCode",
									"MoCode",
									"StartDate",
									"EndDate" ,
									"MoCodes" ,
									"IsFQC"
								},
                    new string[]{
									TSStatus.TSStatus_Confirm,
									this.txtItemCodeQuery.Text,
									row.Items.FindItemByKey("ShiftDay").Text,
									row.Items.FindItemByKey("ShiftCode").Text,
									this.txtOperationCodeQuery.Text,
									row.Items.FindItemByKey("ResourceCode").Text,
									row.Items.FindItemByKey("MoCode").Text,
									this.txtStartDate.Value,
									this.txtEndDate.Value,
									this.txtMoCodeQuery.Text,
									this.ViewState["IsFQC"].ToString()
									
								})
                    );
            }
            else if (command == "TSQtyDistributing")/* TS */
            {
                this.Response.Redirect(
                    this.MakeRedirectUrl(
                    "FOnWipDistributingQP.aspx",
                    new string[]{
									"Status",
									"ItemCode",
									"ShiftDay",
									"ShiftCode",
									"OperationCode",
									"ResourceCode",
									"MoCode",
									"StartDate",
									"EndDate" ,
									"MoCodes",
									"IsFQC"
								},
                    new string[]{
									TSStatus.TSStatus_TS,
									this.txtItemCodeQuery.Text,
									row.Items.FindItemByKey("ShiftDay").Text,
									row.Items.FindItemByKey("ShiftCode").Text,
									this.txtOperationCodeQuery.Text,
								    row.Items.FindItemByKey("ResourceCode").Text,
									row.Items.FindItemByKey("MoCode").Text,
									this.txtStartDate.Value,
									this.txtEndDate.Value,
									this.txtMoCodeQuery.Text,
									this.ViewState["IsFQC"].ToString()
								})
                    );
            }
            else if (command == "TSReflowQtyDistributing")/* TS Reflow */
            {
                this.Response.Redirect(
                    this.MakeRedirectUrl(
                    "FOnWipDistributingQP.aspx",
                    new string[]{
									"Status",
									"ItemCode",
									"ShiftDay",
									"ShiftCode",
									"OperationCode",
									"ResourceCode",
									"MoCode",
									"StartDate",
									"EndDate" ,
									"MoCodes" ,
									"IsFQC"
								},
                    new string[]{
									TSStatus.TSStatus_Reflow,
									this.txtItemCodeQuery.Text,
									row.Items.FindItemByKey("ShiftDay").Text,
									row.Items.FindItemByKey("ShiftCode").Text,
									this.txtOperationCodeQuery.Text,
									row.Items.FindItemByKey("ResourceCode").Text,
									row.Items.FindItemByKey("MoCode").Text,
									this.txtStartDate.Value,
									this.txtEndDate.Value,
									this.txtMoCodeQuery.Text,
									this.ViewState["IsFQC"].ToString()
								})
                    );
            }
        }

        protected void cmdReturn_ServerClick(object sender, System.EventArgs e)
        {
            this.Response.Redirect(this.MakeRedirectUrl("FOnWipQP.aspx"));
        }
    }
}
