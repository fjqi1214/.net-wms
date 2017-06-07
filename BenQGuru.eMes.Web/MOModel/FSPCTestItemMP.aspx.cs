#region system
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
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;
#endregion

#region project
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Domain.SPC;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Web.Helper;
#endregion

namespace BenQGuru.eMES.Web.MOModel
{
    /// <summary>
    /// FModelMP 的摘要说明。
    /// </summary>
    public partial class FSPCTestItemMP : BaseMPageMinus
    {
        //protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;


        //private GridHelper gridHelper = null;
        private ButtonHelper buttonHelper = null;
        private SPCFacade _spcFacade;
        protected BenQGuru.eMES.Web.Helper.ExcelExporter excelExporter;




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
            //this.languageComponent1 = new ControlLibrary.Web.Language.LanguageComponent(this.components);
            this.excelExporter = new BenQGuru.eMES.Web.Helper.ExcelExporter(this.components);
            //this.gridWebGrid.ClickCellButton += new Infragistics.WebUI.UltraWebGrid.ClickCellButtonEventHandler(this.gridWebGrid_ClickCellButton);
            // 
            // languageComponent1
            // 
            //this.languageComponent1.Language = "CHS";
            //this.languageComponent1.LanguagePackageDir = "\\\\grd2-build\\language pack\\";
            //this.languageComponent1.RuntimePage = null;
            //this.languageComponent1.RuntimeUserControl = null;
            //this.languageComponent1.UserControlName = "";
            // 
            // excelExporter
            // 
            this.excelExporter.FileExtension = "xls";
            this.excelExporter.LanguageComponent = this.languageComponent1;
            this.excelExporter.Page = this;
            this.excelExporter.RowSplit = "\r\n";

        }
        #endregion


        #region form events
        protected void Page_Load(object sender, System.EventArgs e)
        {
            InitHander();
            if (!IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);
                // 初始化界面UI
                this.InitUI();
                this.InitButton();
                this.InitWebGrid();
                this.InitDDL();

                this.cmdAdd.Visible = true;
            }
        }

        protected void cmdQuery_ServerClick(object sender, System.EventArgs e)
        {
            this.RequestData();
            this.buttonHelper.PageActionStatusHandle(PageActionType.Query);
        }

        protected void cmdAdd_ServerClick(object sender, System.EventArgs e)
        {
            if (_spcFacade == null) { _spcFacade = new FacadeFactory(base.DataProvider).CreateSPCFacade(); }
            object obj = this.GetEditObject();
            if (obj != null)
            {
                if (!CheckSPCObject(((SPCObject)obj).ObjectCode))
                {
                    return;
                }

                this._spcFacade.AddSPCObject((SPCObject)obj);
                this.RequestData();
                this.buttonHelper.PageActionStatusHandle(PageActionType.Add);
            }
        }

        protected void cmdDelete_ServerClick(object sender, System.EventArgs e)
        {
            if (_spcFacade == null) { _spcFacade = new FacadeFactory(base.DataProvider).CreateSPCFacade(); }
            ArrayList array = this.gridHelper.GetCheckedRows();
            if (array.Count > 0)
            {
                ArrayList objs = new ArrayList(array.Count);

                foreach (GridRecord row in array)
                {
                    object obj = this.GetEditObject(row);
                    if (obj != null)
                    {
                        objs.Add((SPCObject)obj);
                    }
                }

                this._spcFacade.DeleteSPCObject((SPCObject[])objs.ToArray(typeof(SPCObject)));
                this.RequestData();
                this.buttonHelper.PageActionStatusHandle(PageActionType.Delete);
            }
        }

        protected void cmdSave_ServerClick(object sender, System.EventArgs e)
        {
            if (_spcFacade == null) { _spcFacade = new FacadeFactory(base.DataProvider).CreateSPCFacade(); }
            object obj = this.GetEditObject();
            if (obj != null)
            {
                this._spcFacade.UpdateSPCObject((SPCObject)obj);
                this.RequestData();

                this.buttonHelper.PageActionStatusHandle(PageActionType.Save);
            }
        }

        protected void cmdCancel_ServerClick(object sender, System.EventArgs e)
        {
            this.buttonHelper.PageActionStatusHandle(PageActionType.Cancel);
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
            this.excelExporter.Export();
        }

        protected override void Grid_ClickCell(GridRecord row, string commandName)
        {
            if (commandName.ToUpper() == "CHECKITEMLIST".ToUpper())
            {
                string strUrl = this.MakeRedirectUrl("FSPCTestItemStoreSP.aspx", new string[] { "objectcode" }, new string[] { row.Items.FindItemByKey("SPCControlItem").Text });
                Response.Redirect(strUrl);
                return;
            }
            object obj = this.GetEditObject(row);

            if (obj != null)
            {
                this.SetEditObject(obj);

                this.buttonHelper.PageActionStatusHandle(PageActionType.Update);
            }
        }

        #endregion

        #region private method

        private void RequestData()
        {
            this.pagerToolBar.PageSize = this.pagerSizeSelector.PageSize;
            this.gridHelper.GridBind(PageGridBunding.Page, this.pagerSizeSelector.PageSize);
            this.pagerToolBar.RowCount = GetRowCount();
            this.pagerToolBar.InitPager();
        }

        private int GetRowCount()
        {
            if (_spcFacade == null) { _spcFacade = new FacadeFactory(base.DataProvider).CreateSPCFacade(); }
            return this._spcFacade.QuerySPCObjectCount(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtObjectCodeQuery.Text)),
                FormatHelper.CleanString(this.txtObjectNameQuery.Text));
        }


        private void InitHander()
        {
            this.gridHelper = new GridHelperNew(this.gridWebGrid,this.DtSource);
            this.gridHelper.LoadDataSourceHandle = new LoadDataSourceDelegateNew(this.LoadDataSource);
            this.gridHelper.BuildGridRowhandle = new BuildGridRowDelegateNew(this.GetGridRow);

            this.buttonHelper = new ButtonHelper(this);
            this.buttonHelper.SetEditObjectHandle = new SetEditObjectDelegate(this.SetEditObject);
            this.buttonHelper.AfterPageStatusChangeHandle = new PageStatusChangeDelegate(this.buttonHelper_AfterPageStatusChangeHandle);

            this.pagerToolBar.OnPagerToolBarClick += new EventHandler(this.PagerToolBar_OnPagerToolBarClick);

            this.excelExporter.LoadExportDataHandle = new LoadExportDataDelegate(LoadDataSource);
            this.excelExporter.FormatExportRecordHandle = new FormatExportRecordDelegate(FormatExportRecord);
            this.excelExporter.GetColumnHeaderTextHandle = new GetColumnHeaderTextDelegate(GetColumnHeaderText);
        }

        public bool CheckSPCObject(string objectCode)
        {
            if (_spcFacade == null) { _spcFacade = new FacadeFactory(base.DataProvider).CreateSPCFacade(); }
            object obj = _spcFacade.GetSPCObject(objectCode);
            if (obj != null)
            {
                WebInfoPublish.Publish(this, "$Error_PK_is_Repeat", this.languageComponent1);
                return false;
            }
            return true;
        }

        private void buttonHelper_AfterPageStatusChangeHandle(string pageAction)
        {
            if (pageAction == PageActionType.Save)
            {
                this.txtObjectCodeEdit.ReadOnly = false;
            }
            if (pageAction == PageActionType.Update)
            {
                this.txtObjectCodeEdit.ReadOnly = true;
            }
            if (pageAction == PageActionType.Cancel)
            {
                this.txtObjectCodeEdit.ReadOnly = false;
            }

        }


        private void PagerToolBar_OnPagerToolBarClick(object sender, System.EventArgs e)
        {
            this.gridHelper.GridBind(this.pagerToolBar.PageIndex, this.pagerToolBar.PageSize);
        }

        public void InitButton()
        {
            this.buttonHelper.AddDeleteConfirm();
            this.buttonHelper.PageActionStatusHandle(PageActionType.Add);
        }

        private void InitWebGrid()
        {
            this.gridHelper.AddColumn("SPCControlItem", "管控项目", null);
            this.gridHelper.AddColumn("ObjectName", "项目名称", null);
            this.gridHelper.AddColumn("GraphType", "图表类型", null);
            this.gridHelper.AddColumn("DateFromTo", "日期范围", null);

            this.gridHelper.AddColumn("MUSER", "维护用户", null);
            this.gridHelper.AddColumn("MDATE", "维护日期", null);
            this.gridHelper.AddColumn("MTIME", "维护时间", null);
            this.gridHelper.AddLinkColumn("CHECKITEMLIST", "检验项目", null);

           // this.gridHelper.CheckAllBox.Visible = false;

            this.gridHelper.AddDefaultColumn(false, true);
            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }

        private void InitDDL()
        {
            this.drpGraphTypeEdit.Items.Clear();
            this.drpGraphTypeEdit.Items.Add(new ListItem("", ""));
            new DropDownListBuilder2(new SPCChartType(), this.drpGraphTypeEdit, this.languageComponent1).Build();

            this.drpDateEdit.Items.Clear();
            this.drpDateEdit.Items.Add(new ListItem("", ""));
            new DropDownListBuilder2(new DateRange(), this.drpDateEdit, this.languageComponent1).Build();

        }

        private object GetEditObject()
        {
            if (this.ValidateInput())
            {
                if (_spcFacade == null) { _spcFacade = new FacadeFactory(base.DataProvider).CreateSPCFacade(); }
                SPCObject obj = this._spcFacade.CreateNewSPCObject();

                obj.ObjectCode = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtObjectCodeEdit.Text));
                obj.ObjectName = FormatHelper.CleanString(this.txtObjectNameEdit.Text);
                obj.GraphType = FormatHelper.CleanString(this.drpGraphTypeEdit.SelectedValue);
                obj.DateRange = FormatHelper.CleanString(this.drpDateEdit.SelectedValue);

                obj.MaintainUser = this.GetUserCode();

                return obj;
            }
            else
            {
                return null;
            }
        }

        private object GetEditObject(GridRecord row)
        {
            if (_spcFacade == null) { _spcFacade = new FacadeFactory(base.DataProvider).CreateSPCFacade(); }
            object obj = this._spcFacade.GetSPCObject(row.Items.FindItemByKey("SPCControlItem").Text.ToString());

            if (obj != null)
            {
                return (SPCObject)obj;
            }

            return null;
        }

        private bool ValidateInput()
        {
            PageCheckManager manager = new PageCheckManager();
            manager.Add(new LengthCheck(this.lblObjectCodeEdit, this.txtObjectCodeEdit, 40, true));
            manager.Add(new LengthCheck(this.lblObjectNameEdit, this.txtObjectNameEdit, 40, false));
            manager.Add(new LengthCheck(this.lblGraphTypeEdit, this.drpGraphTypeEdit, 40, true));
            manager.Add(new LengthCheck(this.lblDateEdit, this.drpDateEdit, 40, true));

            if (!manager.Check())
            {
                WebInfoPublish.Publish(this, manager.CheckMessage, languageComponent1);
                return false;
            }
            return true;
        }

        private void SetEditObject(object obj)
        {
            if (obj == null)
            {
                this.txtObjectCodeEdit.Text = string.Empty;
                this.txtObjectNameEdit.Text = string.Empty;
                this.drpGraphTypeEdit.SelectedIndex = 0;
                this.drpDateEdit.SelectedIndex = 0;
                return;
            }

            SPCObject spcObj = obj as SPCObject;

            this.txtObjectCodeEdit.Text = spcObj.ObjectCode.ToString();
            this.txtObjectNameEdit.Text = spcObj.ObjectName.ToString();
            this.drpGraphTypeEdit.SelectedValue = spcObj.GraphType.ToString();
            this.drpDateEdit.SelectedValue = spcObj.DateRange.ToString();

        }

        protected DataRow GetGridRow(object obj)
        {
            SPCObject spcObj = obj as SPCObject;
            //return new Infragistics.WebUI.UltraWebGrid.UltraGridRow(
            //    new object[]{
            //                    spcObj.ObjectCode.ToString(),
            //                    spcObj.ObjectName.ToString(),
            //                    this.languageComponent1.GetString( spcObj.GraphType.ToString() ),
            //                    this.languageComponent1.GetString( spcObj.DateRange.ToString() ),
            //                    spcObj.MaintainUser.ToString(),
            //                    FormatHelper.ToDateString(spcObj.MaintainDate),
            //                    FormatHelper.ToTimeString(spcObj.MaintainTime),
            //                    ""
            //                });
            DataRow row = this.DtSource.NewRow();
            row["SPCControlItem"] = spcObj.ObjectCode.ToString();
            row["ObjectName"] = spcObj.ObjectName.ToString();
            row["GraphType"] = this.languageComponent1.GetString(spcObj.GraphType.ToString());
            row["DateFromTo"] = this.languageComponent1.GetString(spcObj.DateRange.ToString());
            row["MUSER"] = spcObj.MaintainUser.ToString();
            row["MDATE"] = FormatHelper.ToDateString(spcObj.MaintainDate);
            row["MTIME"] = FormatHelper.ToTimeString(spcObj.MaintainTime);
            return row;

        }

        private object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (_spcFacade == null) { _spcFacade = new FacadeFactory(base.DataProvider).CreateSPCFacade(); }
            return this._spcFacade.QuerySPCObject(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtObjectCodeQuery.Text)),
                FormatHelper.CleanString(this.txtObjectNameQuery.Text),
                inclusive, exclusive);
        }

        private string[] FormatExportRecord(object obj)
        {
            SPCObject spcObj = obj as SPCObject;
            return new string[]{
								   spcObj.ObjectCode.ToString(),
								   spcObj.ObjectName.ToString(),
								   this.languageComponent1.GetString( spcObj.GraphType.ToString() ),
								   this.languageComponent1.GetString( spcObj.DateRange.ToString() ),
								   spcObj.MaintainUser.ToString(),
								   FormatHelper.ToDateString(spcObj.MaintainDate),
								   FormatHelper.ToTimeString(spcObj.MaintainTime)	
							   };
        }

        private string[] GetColumnHeaderText()
        {
            return new string[] {	"SPCControlItem",
									"ObjectName",
									"GraphType",
									"DateFromTo",
									"MUSER",
									"MDATE",
									"MTIME"
								};
        }

        #endregion


    }
}
