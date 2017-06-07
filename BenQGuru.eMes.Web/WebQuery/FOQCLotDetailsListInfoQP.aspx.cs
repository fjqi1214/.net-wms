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
using BenQGuru.eMES.Domain.OQC;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;

namespace BenQGuru.eMES.Web.WebQuery
{
    /// <summary>
    /// FOQCLotDetailsListInfoQP 的摘要说明。
    /// </summary>
    public partial class FOQCLotDetailsListInfoQP : BaseQPageNew
    {
        //protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        //private System.ComponentModel.IContainer components;

        protected ExcelExporter excelExporter = null;
        protected WebQueryHelperNew _helper = null;
        protected GridHelperNew _gridHelper = null;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            //this.pagerSizeSelector.Readonly = true;
            this.gridHelper = new GridHelperNew(this.gridWebGrid, this.DtSource);
            this.txtOQCLotQuery.Text = this.GetRequestParam("oqclot");

            this._gridHelper = new GridHelperNew(this.gridWebGrid, this.DtSource);

            this._helper = new WebQueryHelperNew(null, this.cmdGridExport, this.gridWebGrid, this.pagerSizeSelector, this.pagerToolBar, this.languageComponent1, this.DtSource);
            this._helper.LoadGridDataSource += new EventHandler(_helper_LoadGridDataSource);
            this._helper.DomainObjectToGridRow += new EventHandler(_helper_DomainObjectToGridRow);
            this._helper.DomainObjectToExportRow += new EventHandler(_helper_DomainObjectToExportRow);
            this._helper.GetExportHeadText += new EventHandler(_helper_GetExportHeadText);

            //this.gridWebGrid.DisplayLayout.CellClickActionDefault = CellClickAction.NotSet;

            if (!this.IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);

                this._initialWebGrid();

                this._helper.Query(sender);
            }
        }

        private void _initialWebGrid()
        {
            base.InitWebGrid();
            this._gridHelper.AddColumn("RunningCard", "产品序列号", null);
            this._gridHelper.AddColumn("MOCode", "工单", null);
            this._gridHelper.AddColumn("ItemCode", "产品代码", null);
            this._gridHelper.AddColumn("CollectionDate", "采集日期", null);
            this._gridHelper.AddColumn("CollectionTime", "采集时间", null);

            ((BoundDataField)this.gridHelper.Grid.Columns.FromKey("RunningCard")).HtmlEncode = false; 

            //this.gridWebGrid.Columns[0].CellStyle.Font.Underline = true;
            //this.gridWebGrid.Columns[0].CellStyle.ForeColor = Color.Blue;
            //this.gridWebGrid.Columns[0].CellStyle.Cursor = Infragistics.WebUI.Shared.Cursors.Hand;
           // this.gridWebGrid.Columns[0].CssClass ="LinkColorBlue";
            //多语言
            this._gridHelper.ApplyLanguage(this.languageComponent1);
            
        }

        //protected void cmdQuery_Click(object sender, EventArgs e)
        //{
        //    this.gridHelper.RequestData();
        //    //this.buttonHelper.PageActionStatusHandle(PageActionType.Query);
        //    // base.cmdQuery_Click(sender, e);
        //    foreach (GridRecord row in this.gridWebGrid.Rows)
        //    {
        //        row.Items[0].CssClass = "LinkColorBlue";
        //        //this.gridWebGrid.Columns[0].CssClass = "LinkColorBlue";

        //    }
        //}

        //添加跳转url
        private string GetRCardLink(string no)
        {
            string url = string.Format("../WEBQUERY/FItemTracingQP.aspx?RCARDFROM={0}&RCARDTO={0}", this.Server.UrlEncode(no));
            return string.Format("<a style= 'color:blue' href=" + url + ">{0}</a>", no);
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
           // this.gridWebGrid.Click += new Infragistics.WebUI.UltraWebGrid.ClickEventHandler(this.gridWebGrid_Click);
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
            manager.Add(new LengthCheck(this.lblOQCLotQuery, this.txtOQCLotQuery, System.Int32.MaxValue, true));

            if (!manager.Check())
            {
                WebInfoPublish.Publish(this, manager.CheckMessage, this.languageComponent1);
                return false;
            }
            return true;
        }

        private void _helper_LoadGridDataSource(object sender, EventArgs e)
        {
            if (this._checkRequireFields())
            {
                FacadeFactory facadeFactory = new FacadeFactory(base.DataProvider);
                (e as WebQueryEventArgsNew).GridDataSource =
                    facadeFactory.CreateQueryOQCLotDetailsFacade().QueryOQCLotDetailsListInfo(
                    FormatHelper.CleanString(this.txtOQCLotQuery.Text).ToUpper(),
                    (e as WebQueryEventArgsNew).StartRow,
                    (e as WebQueryEventArgsNew).EndRow);

                (e as WebQueryEventArgsNew).RowCount =
                    facadeFactory.CreateQueryOQCLotDetailsFacade().QueryOQCLotDetailsListInfoCount(
                    FormatHelper.CleanString(this.txtOQCLotQuery.Text).ToUpper());
            }
        }

        private void _helper_DomainObjectToGridRow(object sender, EventArgs e)
        {
            if ((e as DomainObjectToGridRowEventArgsNew).DomainObject != null)
            {
                OQCLot2Card obj = (e as DomainObjectToGridRowEventArgsNew).DomainObject as OQCLot2Card;
                DataRow row = this.DtSource.NewRow();
                row["RunningCard"] = GetRCardLink(obj.RunningCard.ToString());
                row["MOCode"] = obj.MOCode;
                row["ItemCode"] = obj.ItemCode;
                row["CollectionDate"] = FormatHelper.ToDateString(obj.MaintainDate);
                row["CollectionTime"] = FormatHelper.ToTimeString(obj.MaintainTime);
                (e as DomainObjectToGridRowEventArgsNew).GridRow = row;
                //    new UltraGridRow( new object[]{
                //                                      obj.RunningCard,
                //                                      obj.MOCode,
                //                                      obj.ItemCode,
                //                                      FormatHelper.ToDateString(obj.MaintainDate),
                //                                      FormatHelper.ToTimeString(obj.MaintainTime)
                //                                  }
                //    );
                //this.cmdQuery_Click(null, null);
            }
        }

        private void _helper_DomainObjectToExportRow(object sender, EventArgs e)
        {
            if ((e as DomainObjectToExportRowEventArgsNew).DomainObject != null)
            {
                OQCLot2Card obj = (e as DomainObjectToExportRowEventArgsNew).DomainObject as OQCLot2Card;
                (e as DomainObjectToExportRowEventArgsNew).ExportRow =
                    new string[]{
									obj.RunningCard,
									obj.MOCode,
									obj.ItemCode,
									FormatHelper.ToDateString(obj.MaintainDate),
									FormatHelper.ToTimeString(obj.MaintainTime)
								};
            }
        }

        private void _helper_GetExportHeadText(object sender, EventArgs e)
        {
            (e as ExportHeadEventArgsNew).Heads =
                new string[]{
								"RunningCard",
								"MOCode",
								"ItemCode",
								"CollectionDate",
								"CollectionTime"
							};
        }

        protected void cmdReturn_ServerClick(object sender, System.EventArgs e)
        {
            this.Response.Redirect("FOQCLotDetailsQP.aspx");
        }

        protected override void Grid_ClickCell(GridRecord row, string command)
        {
            if (command.ToUpper() == "RunningCard".ToUpper())
            {
                this.Response.Redirect(
                    this.MakeRedirectUrl(
                    "FItemTracingQP.aspx",
                    new string[] { "RCARDFROM", "RCARDTO" },
                    new string[] { row.Items.FindItemByKey("RCARDFROM").Text, row.Items.FindItemByKey("RCARDTO").Text })
                    );
            }
        }
    }
}
