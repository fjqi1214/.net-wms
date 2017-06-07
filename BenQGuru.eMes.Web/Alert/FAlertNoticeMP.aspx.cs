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

using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Web.Helper;
using ControlLibrary.Web.Language;

using BenQGuru.eMES.Domain.Alert;
using BenQGuru.eMES.AlertModel;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;

namespace BenQGuru.eMES.Web.Alert
{
    public partial class FAlertNoticeMP : BaseMPageNew
    {
        private System.ComponentModel.IContainer components;

        //private LanguageComponent languageComponent1;
        //private GridHelper gridHelper;
        private ExcelExporter _ExcelExporter;

        private AlertFacade _AlertFacade;

        #region Form Init

        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this._ExcelExporter = new BenQGuru.eMES.Web.Helper.ExcelExporter(this.components);
            //this.languageComponent1 = new ControlLibrary.Web.Language.LanguageComponent(this.components);
            //this.languageComponent1.Language = "CHS";
            //this.languageComponent1.LanguagePackageDir = "";
            //this.languageComponent1.RuntimePage = null;
            //this.languageComponent1.RuntimeUserControl = null;
            //this.languageComponent1.UserControlName = "";

            this._ExcelExporter = new BenQGuru.eMES.Web.Helper.ExcelExporter(this.components);
            this._ExcelExporter.FileExtension = "xls";
            this._ExcelExporter.LanguageComponent = this.languageComponent1;
            this._ExcelExporter.Page = this;
            this._ExcelExporter.RowSplit = "\r\n";

            this._AlertFacade = new AlertFacade(this.DataProvider);

           // this.gridWebGrid.ClickCellButton += new Infragistics.WebUI.UltraWebGrid.ClickCellButtonEventHandler(this.gridWebGrid_ClickCellButton);

            this.pagerToolBar.OnPagerToolBarClick += new EventHandler(this.PagerToolBar_OnPagerToolBarClick);
        }

        #endregion

        #region Events

        protected void Page_Load(object sender, System.EventArgs e)
        {
            InitHander();

            if (!IsPostBack)
            {
                InitPageLanguage(this.languageComponent1, false);

                InitUI();
                InitWebGrid();
                InitQueryList();
                this.datStartDateWhere.Text = System.DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                this.datEndDateWhere.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
                
            }
        }

        protected void cmdQuery_ServerClick(object sender, System.EventArgs e)
        {
            if (this._checkDateFields())
            {
                this.RequestData();
            }
        }

        protected override void Grid_ClickCell(GridRecord row, string commandName)
        {
            if (commandName == "DealAlertNotice")
            {
                object obj = this.GetEditObject(row);

                if (obj != null)
                {
                    this.Response.Redirect("FalertNoticeDeal.aspx?SERIAL=" + ((AlertNotice)obj).Serial);
                }
            }
        }

        //protected void chbSelectAll_CheckedChanged(object sender, System.EventArgs e)
        //{
        //    if (this.chbSelectAll.Checked)
        //    {
        //        this.gridHelper.CheckAllRows(CheckStatus.Checked);
        //    }
        //    else
        //    {
        //        this.gridHelper.CheckAllRows(CheckStatus.Unchecked);
        //    }
        //}

        protected void cmdGridExport_ServerClick(object sender, System.EventArgs e)
        {
            this._ExcelExporter.Export();
        }

        private void PagerToolBar_OnPagerToolBarClick(object sender, System.EventArgs e)
        {
            this.gridHelper.GridBind(this.pagerToolBar.PageIndex, this.pagerToolBar.PageSize);
        }

        protected void cmdQuickDeal_ServerClick(object sender, System.EventArgs e)
        {
            ArrayList rowArray = this.gridHelper.GetCheckedRows();
            if (rowArray != null && rowArray.Count > 0)
            {
                ArrayList modelArray = new ArrayList();

                foreach (GridRecord row in rowArray)
                {
                    AlertNotice model = (AlertNotice)GetEditObject(row);

                    if (model != null)
                    {
                        if (model.Status == AlertNoticeStatus.AlertNoticeStatus_NotDeal)
                        {
                            model.Status = AlertNoticeStatus.AlertNoticeStatus_HasDeal;

                            model.DealDate = FormatHelper.TODateInt(DateTime.Now.ToShortDateString());
                            model.DealTime = FormatHelper.TOTimeInt(DateTime.Now);
                            model.DealUser = this.GetUserCode();
                            modelArray.Add(model);
                        }
                    }
                }

                if (modelArray.Count>0)
                {
                    this._AlertFacade.QuickDealAlertNotice((AlertNotice[])modelArray.ToArray(typeof(AlertNotice)));
                }
                this.RequestData();
            }else{
                WebInfoPublish.Publish(this, "$CS_GRID_SELECT_ONE_RECORD", this.languageComponent1);
            }

        }
        

        #endregion

        #region LoadData

        private void RequestData()
        {
            this.pagerToolBar.PageSize = this.pagerSizeSelector.PageSize;
            this.gridHelper.GridBind(PageGridBunding.Page, this.pagerSizeSelector.PageSize);
            this.pagerToolBar.RowCount = GetRowCount();
            this.pagerToolBar.InitPager();
        }

        private object[] LoadDataSource()
        {
            return this.LoadDataSource(1, int.MaxValue);
        }

        private object[] LoadDataSource(int inclusive, int exclusive)
        {
            return this._AlertFacade.QueryAlertNotice(this.ddlAlertTypeQuery.SelectedValue,
                                                      FormatHelper.TODateInt(this.datStartDateWhere.Text),
                                                      FormatHelper.TODateInt(this.datEndDateWhere.Text),
                                                      this.ddlNoticeStatus.SelectedValue,
                                                      inclusive, exclusive);            
        }

        private int GetRowCount()
        {
            return this._AlertFacade.QueryAlertNoticeCount(this.ddlAlertTypeQuery.SelectedValue,
                                                      FormatHelper.TODateInt(this.datStartDateWhere.Text),
                                                      FormatHelper.TODateInt(this.datEndDateWhere.Text),
                                                      this.ddlNoticeStatus.SelectedValue);
        }

        

        #endregion

        #region Init Functions

        private void InitHander()
        {
            this.gridHelper = new GridHelperNew(this.gridWebGrid,DtSource);
            this.gridHelper.LoadDataSourceHandle = new LoadDataSourceDelegateNew(this.LoadDataSource);
            this.gridHelper.BuildGridRowhandle = new BuildGridRowDelegateNew(this.GetGridRow);

            this._ExcelExporter.LoadExportDataHandle = new LoadExportDataDelegate(LoadDataSource);
            this._ExcelExporter.FormatExportRecordHandle = new FormatExportRecordDelegate(FormatExportRecord);
            this._ExcelExporter.GetColumnHeaderTextHandle = new GetColumnHeaderTextDelegate(GetColumnHeaderText);
        }

        private void InitWebGrid()
        {
            this.gridHelper.Grid.Columns.Clear();

            this.gridHelper.AddColumn("SERIAL", "序号", null);
            this.gridHelper.AddColumn("AlertItemType", "预警项目", 130);
            this.gridHelper.AddColumn("AlertItem", "预警项次", 150);
            //Modified By Nettie Chen 2009/09/22
            //this.gridHelper.AddColumn("AlertNotice", "通告信息", null);
            //this.gridHelper.AddColumn("Status", "状态", null, 60);
            this.gridHelper.AddColumn("AlertNotice", "通告信息", 200);
            this.gridHelper.AddColumn("Status", "状态",60);
            this.gridHelper.AddColumn("DealUser", "最后处理人", 60);
            this.gridHelper.AddColumn("DealDate", "处理日期", 60);
            this.gridHelper.AddColumn("DealTime", "处理时间", 60);
            //End Modified
            this.gridHelper.AddLinkColumn("DealAlertNotice", "处置信息", null);
            this.gridHelper.Grid.Columns.FromKey("SERIAL").Hidden = true;

            this.gridHelper.AddDefaultColumn(true, false);

            //this.gridHelper.ApplyLanguage(this.languageComponent1);
        }

        private void InitQueryList()
        {
            AlertType alertType = new AlertType();

            this.ddlAlertTypeQuery.Items.Clear();

            foreach (string item in (alertType.Items))
            {
                if (item == AlertType.AlertType_OQCReject)
                {
                    continue;
                }
                this.ddlAlertTypeQuery.Items.Add(new ListItem(this.languageComponent1.GetString(item), item));
            }
            this.ddlAlertTypeQuery.Items.Insert(0, new ListItem("", ""));

            //初始化状态
            AlertNoticeStatus noticeStatus = new AlertNoticeStatus();

            this.ddlNoticeStatus.Items.Clear();

            foreach (string status in (noticeStatus.Items))
            {
                this.ddlNoticeStatus.Items.Add(new ListItem(this.languageComponent1.GetString(status), status));
            }
            this.ddlNoticeStatus.Items.Insert(0, new ListItem("", ""));
            this.ddlNoticeStatus.SelectedIndex = 2;
        }

        #endregion

        #region Get/Set Edit Object
        protected DataRow GetGridRow(object obj)
        {
            AlertNotice model = (AlertNotice)obj;

            //return new UltraGridRow(
            //    new object[]{
            //        "false",
            //        model.Serial,
            //        this.languageComponent1.GetString(model.AlertType),
            //        model.ItemSequence,
            //        model.NoticeContent,
            //        this.languageComponent1.GetString(model.Status)
            //        //Added By Nettie Chen 2009/09/22
            //        ,model.DealUser 
            //        ,FormatHelper.ToDateString(model.DealDate) 
            //        ,FormatHelper.ToTimeString(model.DealTime)
            //        //End Added
            //    });
            DataRow row = this.DtSource.NewRow();
            row["SERIAL"] = model.Serial;
            row["AlertItemType"] = this.languageComponent1.GetString(model.AlertType);
            row["AlertItem"] = model.ItemSequence;
            row["AlertNotice"] = model.NoticeContent;
            row["Status"] = this.languageComponent1.GetString(model.Status);
            row["DealUser"] = model.DealUser;
            row["DealDate"] = FormatHelper.ToDateString(model.DealDate);
            row["DealTime"] = FormatHelper.ToTimeString(model.DealTime);
            return row;

        }
        
        private object GetEditObject(GridRecord row)
        {

            return this._AlertFacade.GetAlertNotice(int.Parse(row.Items.FindItemByKey("SERIAL").Text));
        }

        private bool _checkDateFields()
        {
            PageCheckManager manager = new PageCheckManager();
            manager.Add(new DateCheck(this.lblAlertStartDate, datStartDateWhere.Text, false));
            manager.Add(new DateCheck(this.lblAlertEndDate, datEndDateWhere.Text, false));
            manager.Add(new DateRangeCheck(this.lblAlertStartDate, this.datStartDateWhere.Text, this.lblAlertEndDate, this.datEndDateWhere.Text, false));
            if (!manager.Check())
            {
                WebInfoPublish.Publish(this, manager.CheckMessage, this.languageComponent1);
                return false;
            }
            return true;
        }
        #endregion

        #region Export

        private string[] GetColumnHeaderText()
        {
            return new string[] {	
                "AlertItemType",
                "AlertItem",
                "AlertNotice",
                "Status"
                 //Added By Nettie Chen 2009/09/22
                ,"DealUser"
                ,"DealDate" 
                ,"DealTime"
                //End Added
            };

        }

        private string[] FormatExportRecord(object obj)
        {
            AlertNotice model = (AlertNotice)obj;

            return new string[]{
                this.languageComponent1.GetString(model.AlertType),
                model.ItemSequence,
                model.NoticeContent,
                this.languageComponent1.GetString(model.Status)
                //Added By Nettie Chen 2009/09/22
                ,model.DealUser 
                ,FormatHelper.ToDateString(model.DealDate) 
                ,FormatHelper.ToTimeString(model.DealTime)
                //End Added
            };
        }

        #endregion
    }
}
