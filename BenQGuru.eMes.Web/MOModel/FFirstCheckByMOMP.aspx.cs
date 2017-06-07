using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Domain.MOModel;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;

namespace BenQGuru.eMES.Web.MOModel
{
    public partial class FFirstCheckByMOMP : BaseMPageMinus
    {
        protected global::System.Web.UI.WebControls.TextBox firstCheckDateQuery;
        protected global::System.Web.UI.WebControls.TextBox firstCheckDateEdit;

        private System.ComponentModel.IContainer components;
        //private ControlLibrary.Web.Language.LanguageComponent languageComponent1;

        //private GridHelper gridHelper = null;
        private ButtonHelper buttonHelper = null;
        private ExcelExporter excelExporter;

        private MOFacade _MOFacade;

        #region Init

        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            //this.languageComponent1 = new ControlLibrary.Web.Language.LanguageComponent(this.components);
            this.excelExporter = new BenQGuru.eMES.Web.Helper.ExcelExporter(this.components);
            //this.gridWebGrid.ClickCellButton += new Infragistics.WebUI.UltraWebGrid.ClickCellButtonEventHandler(this.gridWebGrid_ClickCellButton);

            // languageComponent1
            //this.languageComponent1.Language = "CHS";
            //this.languageComponent1.LanguagePackageDir = "D:\\SQC2.0\\eMES\\Source\\bin";
            //this.languageComponent1.RuntimePage = null;
            //this.languageComponent1.RuntimeUserControl = null;
            //this.languageComponent1.UserControlName = "";

            // excelExporter
            this.excelExporter.FileExtension = "xls";
            this.excelExporter.LanguageComponent = this.languageComponent1;
            this.excelExporter.Page = this;
            this.excelExporter.RowSplit = "\r\n";
        }

        #endregion

        #region Events

        protected void Page_Load(object sender, System.EventArgs e)
        {
            this.InitOnPostBack();

            if (!IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);

                // 初始化界面UI
                this.InitUI();
                this.InitButtonHelp();
                this.SetEditObject(null);
                this.InitWebGrid();

                // 初始化控件
                IInternalSystemVariable IInternalSystemVariableNew = new IQCCheckStatus();
                IInternalSystemVariableNew.Items.Remove(IQCCheckStatus.IQCCheckStatus_WaitCheck);
                RadioButtonListBuilder builder1 = new RadioButtonListBuilder(IInternalSystemVariableNew, this.rblCheckResult, this.languageComponent1);
                builder1.Build();
                this.rblCheckResult.SelectedIndex = 0;

            }

            _MOFacade = new MOFacade(base.DataProvider);

        }

        protected void cmdQuery_ServerClick(object sender, System.EventArgs e)
        {
            this.RequestData();
            this.buttonHelper.PageActionStatusHandle(PageActionType.Query);
        }

        protected void cmdAdd_ServerClick(object sender, System.EventArgs e)
        {
            object obj = this.GetEditObject();

            if (obj != null)
            {
                if (_MOFacade.GetFirstCheckByMO(
                    FormatHelper.PKCapitalFormat(FormatHelper.CleanString(((FirstCheckByMO)obj).MOCode)),
                    ((FirstCheckByMO)obj).CheckDate) != null)
                {
                    WebInfoPublish.Publish(this, "$Error_Primary_Key_Overlap", languageComponent1);
                    return;
                };
                if (_MOFacade.GetMO(FormatHelper.CleanString(((FirstCheckByMO)obj).MOCode)) == null)
                {
                    WebInfoPublish.Publish(this, "$Error_Input_MoCode", languageComponent1);
                    return;
                }
                this._MOFacade.AddFirstCheckByMO((FirstCheckByMO)obj);

                this.RequestData();
                this.buttonHelper.PageActionStatusHandle(PageActionType.Add);
            }
        }

        protected void cmdDelete_ServerClick(object sender, System.EventArgs e)
        {
            ArrayList array = this.gridHelper.GetCheckedRows();
            if (array.Count > 0)
            {
                ArrayList items = new ArrayList(array.Count);

                foreach (GridRecord row in array)
                {
                    object obj = this.GetEditObject(row);
                    if (obj != null)
                    {
                        items.Add((FirstCheckByMO)obj);
                    }
                }

                this._MOFacade.DeleteFirstCheckByMO((FirstCheckByMO[])items.ToArray(typeof(FirstCheckByMO)));

                this.RequestData();
                this.buttonHelper.PageActionStatusHandle(PageActionType.Delete);
            }
        }

        protected void cmdSave_ServerClick(object sender, System.EventArgs e)
        {
            object obj = this.GetEditObject();

            if (obj != null)
            {
                this._MOFacade.UpdateFirstCheckByMO((FirstCheckByMO)obj);

                this.RequestData();
                this.buttonHelper.PageActionStatusHandle(PageActionType.Save);
                this.txtMOCodeEdit.Readonly = false;
                this.firstCheckDateEdit.ReadOnly = false;
                this.firstCheckDateEdit.Enabled = true;

            }
        }

        protected void cmdCancel_ServerClick(object sender, System.EventArgs e)
        {
            this.txtMOCodeEdit.Readonly = false;
            this.firstCheckDateEdit.ReadOnly = false;
            this.firstCheckDateEdit.Enabled = true;
            this.buttonHelper.PageActionStatusHandle(PageActionType.Cancel);
        }

        protected void cmdGridExport_ServerClick(object sender, System.EventArgs e)
        {
            this.excelExporter.Export();
        }

        protected override void Grid_ClickCell(GridRecord row, string commandName)
        {
            if (commandName == "Edit")
            {
                object obj = this.GetEditObject(row);

                if (obj != null)
                {
                    this.SetEditObject(obj);

                    this.buttonHelper.PageActionStatusHandle(PageActionType.Update);
                    this.txtMOCodeEdit.Readonly = true;
                    this.firstCheckDateEdit.ReadOnly = true;
                    this.firstCheckDateEdit.Enabled = false;
                }
            }
        }

        private bool ValidateInput()
        {
            PageCheckManager manager = new PageCheckManager();
            manager.Add(new LengthCheck(lblMOCodeEdit, txtMOCodeEdit, 40, true));
            manager.Add(new DateCheck(lblFirstCheckDateEdit, firstCheckDateEdit.Text, true));


            if (!manager.Check())
            {
                WebInfoPublish.Publish(this, manager.CheckMessage, languageComponent1);
                return false;
            }

            return true;
        }

        #endregion

        #region For Page_Load

        private void InitOnPostBack()
        {
            this.buttonHelper = new ButtonHelper(this);
            this.buttonHelper.SetEditObjectHandle = new SetEditObjectDelegate(this.SetEditObject);

            this.gridHelper = new GridHelperNew(this.gridWebGrid, this.DtSource);
            this.gridHelper.LoadDataSourceHandle = new LoadDataSourceDelegateNew(this.LoadDataSource);
            this.gridHelper.BuildGridRowhandle = new BuildGridRowDelegateNew(this.GetGridRow);

            this.excelExporter.LoadExportDataHandle = new LoadExportDataDelegate(LoadDataSource);
            this.excelExporter.FormatExportRecordHandle = new FormatExportRecordDelegate(FormatExportRecord);
            this.excelExporter.GetColumnHeaderTextHandle = new GetColumnHeaderTextDelegate(GetColumnHeaderText);
        }

        public void InitButtonHelp()
        {
            this.buttonHelper.PageActionStatusHandle(PageActionType.Add);
            this.buttonHelper.SetEditObjectHandle = new SetEditObjectDelegate(this.SetEditObject);
        }

        #endregion

        #region For Query Data

        private void RequestData()
        {
            this.pagerToolBar.PageSize = this.pagerSizeSelector.PageSize;
            this.gridHelper.GridBind(PageGridBunding.Page, this.pagerSizeSelector.PageSize);
            this.pagerToolBar.RowCount = GetRowCount();
            this.pagerToolBar.InitPager();
        }

        private object[] LoadDataSource()
        {
            return this.LoadDataSource(int.MinValue, int.MaxValue);
        }

        private object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (_MOFacade == null) { _MOFacade = new MOFacade(this.DataProvider); }
            return this._MOFacade.QueryFirstCheckByMO(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtMOCodeQuery.Text)),
                FormatHelper.TODateInt(this.firstCheckDateQuery.Text),
                inclusive,
                exclusive);
        }

        private int GetRowCount()
        {
            if (_MOFacade == null) { _MOFacade = new MOFacade(this.DataProvider); }
            return this._MOFacade.QueryFirstCheckByMOCount(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtMOCodeQuery.Text)),
                FormatHelper.TODateInt(this.firstCheckDateQuery.Text));
        }

        #endregion

        #region For Grid And Edit

        private void InitWebGrid()
        {
            this.gridHelper.AddColumn("MOCode", "工单代码", null);
            this.gridHelper.AddColumn("ItemCode", "产品代码", null);
            this.gridHelper.AddColumn("ItemName", "产品名称", null);
            this.gridHelper.AddColumn("FirstCheckDate", "首检日期", null);
            this.gridHelper.AddColumn("FirstCheckResult", "首检结果", null);
            this.gridHelper.AddColumn("Memo", "备注", null);
            this.gridHelper.AddColumn("MUser", "维护人员", null);
            this.gridHelper.AddColumn("MDate", "维护日期", null);
            this.gridHelper.AddColumn("MTime", "维护时间", null);

            this.gridHelper.AddDefaultColumn(true, true);

            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }

        protected DataRow GetGridRow(object obj)
        {
            DataRow row = this.DtSource.NewRow();
            row["MOCode"] = ((FirstCheckByMOForQuery)obj).MOCode;
            row["ItemCode"] = ((FirstCheckByMOForQuery)obj).ItemCode;
            row["ItemName"] = ((FirstCheckByMOForQuery)obj).ItemName;
            row["FirstCheckDate"] = FormatHelper.ToDateString(((FirstCheckByMOForQuery)obj).CheckDate);
            row["FirstCheckResult"] = this.languageComponent1.GetString(((FirstCheckByMOForQuery)obj).CheckResult);
            row["Memo"] = ((FirstCheckByMOForQuery)obj).Memo;
            row["MUser"] = ((FirstCheckByMOForQuery)obj).Muser;
            row["MDate"] = FormatHelper.ToDateString(((FirstCheckByMOForQuery)obj).Mdate);
            row["MTime"] = FormatHelper.ToTimeString(((FirstCheckByMOForQuery)obj).Mtime);
            return row;
        }

        private object GetEditObject()
        {
            if (this.ValidateInput())
            {
                FirstCheckByMO firstCheckByMO = this._MOFacade.CreateNewFirstCheckByMO();
                DBDateTime DBDateTimeNow = FormatHelper.GetNowDBDateTime(this.DataProvider);

                firstCheckByMO.MOCode = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtMOCodeEdit.Text, 40));
                firstCheckByMO.CheckDate = FormatHelper.TODateInt(this.firstCheckDateEdit.Text);
                firstCheckByMO.CheckResult = this.rblCheckResult.SelectedValue;
                firstCheckByMO.Memo = this.txtRemarkEdit.Text.ToString();
                firstCheckByMO.Muser = this.GetUserCode();
                firstCheckByMO.Mdate = DBDateTimeNow.DBDate;
                firstCheckByMO.Mtime = DBDateTimeNow.DBTime;

                return firstCheckByMO;
            }
            else
            {
                return null;
            }
        }

        private object GetEditObject(GridRecord row)
        {
            object obj = this._MOFacade.GetFirstCheckByMO(row.Items.FindItemByKey("MOCode").Text.Trim().ToUpper(), FormatHelper.TODateInt(row.Items.FindItemByKey("FirstCheckDate").Text.Trim()));

            if (obj != null)
            {
                return (FirstCheckByMO)obj;
            }

            return null;
        }

        private void SetEditObject(object obj)
        {
            if (obj == null)
            {
                this.txtMOCodeEdit.Text = string.Empty;
                this.firstCheckDateEdit.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
                this.rblCheckResult.SelectedIndex = 0;
                this.txtRemarkEdit.Text = string.Empty;

                return;
            }

            this.txtMOCodeEdit.Text = ((FirstCheckByMO)obj).MOCode.ToString();
            this.firstCheckDateEdit.Text = FormatHelper.ToDateString(((FirstCheckByMO)obj).CheckDate);
            this.rblCheckResult.SelectedValue = ((FirstCheckByMO)obj).CheckResult.ToString();
            this.txtRemarkEdit.Text = ((FirstCheckByMO)obj).Memo.ToString();

        }

        #endregion

        #region For Export To Excel

        private string[] FormatExportRecord(object obj)
        {
            return new string[]{
                ((FirstCheckByMOForQuery)obj).MOCode,
                    ((FirstCheckByMOForQuery)obj).ItemCode,
                    ((FirstCheckByMOForQuery)obj).ItemName,
                    FormatHelper.ToDateString(((FirstCheckByMOForQuery)obj).CheckDate),
                    this.languageComponent1.GetString(((FirstCheckByMOForQuery)obj).CheckResult),
                    ((FirstCheckByMOForQuery)obj).Memo,
                    ((FirstCheckByMOForQuery)obj).Muser,
                    FormatHelper.ToDateString(((FirstCheckByMOForQuery)obj).Mdate),
                    FormatHelper.ToTimeString(((FirstCheckByMOForQuery)obj).Mtime)
            };
        }

        private string[] GetColumnHeaderText()
        {
            return new string[] {	
                "MOCode",
                "ItemCode",
                "ItemName",
                "FirstCheckDate",	
                "FirstCheckResult",
                "Memo",
                "MUser",
                "MDate",
                "MTime"
            };
        }

        #endregion
    }
}
