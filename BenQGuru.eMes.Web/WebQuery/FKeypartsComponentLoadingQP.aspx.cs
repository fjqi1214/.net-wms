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
using BenQGuru.eMES.Domain.DataCollect;
using BenQGuru.eMES.DataCollect;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;

namespace BenQGuru.eMES.Web.WebQuery
{
    /// <summary>
    /// FKeypartsComponentLoadingQP 的摘要说明。
    /// </summary>
    public partial class FKeypartsComponentLoadingQP : BaseQPageNew
    {
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;

        protected ExcelExporter excelExporter = null;
        protected WebQueryHelperNew _helper = null;
        //protected GridHelper gridHelper = null;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            gridHelper = new GridHelperNew(this.gridWebGrid, this.DtSource);

            this._helper = new WebQueryHelperNew(this.cmdQuery, this.cmdGridExport, this.gridWebGrid, this.pagerSizeSelector, this.pagerToolBar, this.languageComponent1, DtSource);
            this._helper.LoadGridDataSource += new EventHandler(_helper_LoadGridDataSource);
            this._helper.DomainObjectToGridRow += new EventHandler(_helper_DomainObjectToGridRow);
            this._helper.DomainObjectToExportRow += new EventHandler(_helper_DomainObjectToExportRow);
            this._helper.GetExportHeadText += new EventHandler(_helper_GetExportHeadText);
            //this._helper.GridCellClick += new EventHandler(_helper_GridCellClick);
            FormatHelper.SetSNRangeValue(txtStartSnQuery, txtEndSnQuery);
            FormatHelper.SetSNRangeValue(txtStartKeypartsQuery, txtEndKeypartsQuery);

            if (!this.IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);

                this._initialWebGrid();
            }
        }

        private void _initialWebGrid()
        {
            base.InitWebGrid();
            this.gridHelper.AddColumn("MOCode", "工单", null);
            this.gridHelper.AddColumn("ItemCode", "产品代码", null);
            this.gridHelper.AddColumn("SN", "产品序列号", null);
            this.gridHelper.AddColumn("ComponentLoadingOPCode", "上料工位", null);
            this.gridHelper.AddColumn("ComponentLoadingStepSequenceCode", "上料线别", null);
            this.gridHelper.AddColumn("ComponentLoadingResourceCode", "上料资源", null);
            this.gridHelper.AddColumn("OBItemCode", "子阶料号", null);
            this.gridHelper.AddColumn("Keyparts", "KeyPats序列号", null);
            this.gridHelper.AddColumn("EmployeeNo", "员工工号", null);
            this.gridHelper.AddColumn("ComponentLoadingDate", "上料日期", null);
            this.gridHelper.AddColumn("ComponentLoadingTime", "上料时间", null);
            this.gridHelper.AddLinkColumn("KeypartsDetails", "Keyparts明细", null);

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

        private bool _checkRequireFields()
        {
            PageCheckManager manager = new PageCheckManager();
            manager.Add(new LengthCheck(this.lblMOIDQuery, this.txtConditionMo.TextBox, System.Int32.MaxValue, true));

            if (!manager.Check())
            {
                WebInfoPublish.Publish(this, manager.CheckMessage, this.languageComponent1);
                return false;
            }
            return true;
        }

        private void _helper_LoadGridDataSource(object sender, EventArgs e)
        {
            //将序列号转换为SourceCode
            DataCollectFacade dataCollectfacade = new DataCollectFacade(this.DataProvider);
            //对于序列号的输入框，需要进行一下处理
            string startRCard = FormatHelper.CleanString(this.txtStartSnQuery.Text.Trim().ToUpper());
            string endRCard = FormatHelper.CleanString(this.txtEndSnQuery.Text.Trim().ToUpper());
            //转换成SourceCard
            string startSourceCard = dataCollectfacade.GetSourceCard(startRCard, string.Empty);
            string endSourceCard = dataCollectfacade.GetSourceCard(endRCard, string.Empty);
            //end

            FacadeFactory facadeFactory = new FacadeFactory(base.DataProvider);
            (e as WebQueryEventArgsNew).GridDataSource =
                facadeFactory.CreateQueryComponentLoadingFacade().QueryLoadingKeyparts(
                FormatHelper.CleanString(this.txtConditionItem.Text).ToUpper(),
                FormatHelper.CleanString(this.txtConditionMo.Text).ToUpper(),
                FormatHelper.CleanString(this.txtMItemCode.Text).ToUpper(),
                FormatHelper.CleanString(startSourceCard).ToUpper(),
                FormatHelper.CleanString(endSourceCard).ToUpper(),
                FormatHelper.CleanString(this.txtStartKeypartsQuery.Text.ToUpper()),
                FormatHelper.CleanString(this.txtEndKeypartsQuery.Text.ToUpper()),
                (e as WebQueryEventArgsNew).StartRow,
                (e as WebQueryEventArgsNew).EndRow);

            (e as WebQueryEventArgsNew).RowCount =
                facadeFactory.CreateQueryComponentLoadingFacade().QueryLoadingKeypartsCount(
                FormatHelper.CleanString(this.txtConditionItem.Text).ToUpper(),
                FormatHelper.CleanString(this.txtConditionMo.Text).ToUpper(),
                FormatHelper.CleanString(this.txtMItemCode.Text).ToUpper(),
                FormatHelper.CleanString(startSourceCard.ToUpper()),
                FormatHelper.CleanString(endSourceCard.ToUpper()),
                FormatHelper.CleanString(this.txtStartKeypartsQuery.Text.ToUpper()),
                FormatHelper.CleanString(this.txtEndKeypartsQuery.Text.ToUpper()));
        }

        private void _helper_DomainObjectToGridRow(object sender, EventArgs e)
        {
            if ((e as DomainObjectToGridRowEventArgsNew).DomainObject != null)
            {
                QDOKeyparts obj = (e as DomainObjectToGridRowEventArgsNew).DomainObject as QDOKeyparts;
                DataRow row = DtSource.NewRow();
                row["MOCode"] = obj.MoCode;
                row["ItemCode"] = obj.ItemCode;
                row["SN"] = obj.SN;
                row["ComponentLoadingOPCode"] = obj.OperationCode;
                row["ComponentLoadingStepSequenceCode"] = obj.StepSequenceCode;
                row["ComponentLoadingResourceCode"] = obj.ResourceCode;
                row["OBItemCode"] = obj.MItemCode;
                row["Keyparts"] = obj.INNO;
                row["EmployeeNo"] = obj.MaintainUser;
                row["ComponentLoadingDate"] = FormatHelper.ToDateString(obj.MaintainDate);
                row["ComponentLoadingTime"] = FormatHelper.ToTimeString(obj.MaintainTime);
                row["KeypartsDetails"] = "";
                (e as DomainObjectToGridRowEventArgsNew).GridRow = row;
            }
        }

        private void _helper_DomainObjectToExportRow(object sender, EventArgs e)
        {
            if ((e as DomainObjectToExportRowEventArgsNew).DomainObject != null)
            {
                QDOKeyparts obj = (e as DomainObjectToExportRowEventArgsNew).DomainObject as QDOKeyparts;
                (e as DomainObjectToExportRowEventArgsNew).ExportRow =
                    new string[]{
									obj.MoCode,
									obj.ItemCode,
									obj.SN,
									obj.OperationCode,													  
									obj.StepSequenceCode,
									obj.ResourceCode,
                                    obj.MItemCode,
									obj.INNO,
									obj.MaintainUser,
									FormatHelper.ToDateString(obj.MaintainDate),
									FormatHelper.ToTimeString(obj.MaintainTime)									
								};
            }
        }

        private void _helper_GetExportHeadText(object sender, EventArgs e)
        {
            (e as ExportHeadEventArgsNew).Heads =
                new string[]{
								"MOCode",
								"ItemCode",
								"SN",
								"ComponentLoadingOPCode",
								"ComponentLoadingStepSequenceCode",
								"ComponentLoadingResourceCode",
								"OBItemCode",
                                "Keyparts",
								"EmployeeNo",
								"ComponentLoadingDate",
								"ComponentLoadingTime"
							};
        }

        protected override void Grid_ClickCell(GridRecord row, string command)
        {

            if (command == "KeypartsDetails")
            {
                this.Response.Redirect(
                    this.MakeRedirectUrl(
                    "FKeypartsDetailsQP.aspx",
                    new string[]{
									"Keyparts",
									"RETURNPAGEURL"
								},
                    new string[]{
									row.Items.FindItemByKey("Keyparts").Text,
									"FKeypartsComponentLoadingQP.aspx"
								})
                    );
            }
        }
    }
}
