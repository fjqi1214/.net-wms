using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Web.Helper;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;

namespace BenQGuru.eMES.Web.MOModel
{
    /// <summary>
    /// FMOMP 的摘要说明。
    /// </summary>
    public partial class FMOMP : BenQGuru.eMES.Web.Helper.BaseMPageMinus
    {
        //protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        //private BenQGuru.eMES.Web.Helper.GridHelper gridHelper = null;
        private System.ComponentModel.IContainer components = null;
        protected BenQGuru.eMES.Web.Helper.ExcelExporter excelExporter;
        protected global::System.Web.UI.WebControls.TextBox dateInDateFromQuery;
        protected global::System.Web.UI.WebControls.TextBox dateInDateToQuery;
        protected global::System.Web.UI.WebControls.TextBox ImportDateFrom;
        protected global::System.Web.UI.WebControls.TextBox ImportDateTo;

        private BenQGuru.eMES.MOModel.MOFacade _facade;//= new MOFacade();

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
            //this.languageComponent1.LanguagePackageDir = "D:\\SQC2.0\\eMES\\Source\\bin";
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

        #region Init
        protected void Page_Load(object sender, System.EventArgs e)
        {
            this.InitOnPostBack();

            if (!IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);

                this.InitUI();

                this.InitWebGrid();
                this.drpMoTypeQuery_Load(null, null);
                this.drpMoStatusQuery_Load(null, null);
                if (Session[this.GetType().GUID.ToString()] != null)
                {
                    GetSearchSession();
                    this.cmdQuery_ServerClick(null, null);
                }

                ImportDateFrom.Text = System.DateTime.Now.AddDays(-10).ToString("yyyy-MM-dd");
                ImportDateTo.Text = System.DateTime.Now.ToString("yyyy-MM-dd");

                dateInDateFromQuery.Text = System.DateTime.Now.AddDays(-10).ToString("yyyy-MM-dd");
                dateInDateToQuery.Text = System.DateTime.Now.ToString("yyyy-MM-dd");

            }

            //隐藏导入，导出
            this.cmdEnter.Visible = false;
            this.cmdExport.Visible = false;
        }

        private void SetSearchSession()
        {
            Hashtable controlHT = new Hashtable();
            controlHT[this.txtFactoryQuery.ID] = this.txtFactoryQuery.Text;
            controlHT[this.txtMoCodeQuery.ID] = this.txtMoCodeQuery.Text;
            controlHT[this.txtItemCodeQuery.ID] = this.txtItemCodeQuery.Text;
            controlHT[this.txtItemDescription.ID] = this.txtItemDescription.Text;

            controlHT[this.drpMoTypeQuery.ID] = this.drpMoTypeQuery.SelectedValue;
            controlHT[this.drpMoStatusQuery.ID] = this.drpMoStatusQuery.SelectedValue;
            controlHT[this.chbUseDate.ID] = this.chbUseDate.Checked;
            controlHT[this.dateInDateFromQuery.ID] = this.dateInDateFromQuery.Text;
            controlHT[this.dateInDateToQuery.ID] = this.dateInDateToQuery.Text;

            controlHT[this.chbImportDate.ID] = this.chbImportDate.Checked;
            controlHT[this.ImportDateFrom.ID] = this.ImportDateFrom.Text;
            controlHT[this.ImportDateTo.ID] = this.ImportDateTo.Text;


            Session[this.GetType().GUID.ToString()] = controlHT;

        }
        private void GetSearchSession()
        {
            if (Session[this.GetType().GUID.ToString()] != null)
            {
                Hashtable controlHT = (Hashtable)Session[this.GetType().GUID.ToString()];
                if (controlHT[this.drpMoStatusQuery.ID] != null)
                {
                    this.txtFactoryQuery.Text = controlHT[this.txtFactoryQuery.ID].ToString();
                    this.txtMoCodeQuery.Text = controlHT[this.txtMoCodeQuery.ID].ToString();
                    this.txtItemCodeQuery.Text = controlHT[this.txtItemCodeQuery.ID].ToString();
                    this.txtItemDescription.Text = controlHT[this.txtItemDescription.ID].ToString();
                    this.drpMoTypeQuery.SelectedValue = controlHT[this.drpMoTypeQuery.ID].ToString();
                    this.drpMoStatusQuery.SelectedValue = controlHT[this.drpMoStatusQuery.ID].ToString();

                    this.chbUseDate.Checked = bool.Parse(controlHT[this.chbUseDate.ID].ToString());
                    this.dateInDateFromQuery.Text = controlHT[this.dateInDateFromQuery.ID].ToString();
                    this.dateInDateToQuery.Text = controlHT[this.dateInDateToQuery.ID].ToString();

                    this.chbImportDate.Checked = bool.Parse(controlHT[this.chbImportDate.ID].ToString());
                    this.ImportDateFrom.Text = controlHT[this.ImportDateFrom.ID].ToString();
                    this.ImportDateTo.Text = controlHT[this.ImportDateTo.ID].ToString();
                }
            }
        }

        private void InitOnPostBack()
        {

            this.gridHelper = new GridHelperNew(this.gridWebGrid,this.DtSource);

            this.gridHelper.LoadDataSourceHandle = new LoadDataSourceDelegateNew(this.LoadDataSource);
            this.gridHelper.BuildGridRowhandle = new BuildGridRowDelegateNew(this.GetGridRow);

            this.pagerToolBar.OnPagerToolBarClick += new EventHandler(this.PagerToolBar_OnPagerToolBarClick);
            new ButtonHelper(this).AddDeleteConfirm();

            // 2005-04-06
            this.excelExporter.LoadExportDataHandle = new LoadExportDataDelegate(LoadDataSource);
            this.excelExporter.FormatExportRecordHandle = new FormatExportRecordDelegate(FormatExportRecord);
            this.excelExporter.GetColumnHeaderTextHandle = new GetColumnHeaderTextDelegate(GetColumnHeaderText);
        }


        private string[] FormatExportRecord(object obj)
        {
            MO mo = obj as MO;
            //工单的未完工数量＝工单的已投入数量－工单已完工数量－工单的拆解数量
            decimal moNotComQty = mo.MOInputQty - mo.MOActualQty - mo.MOScrapQty - mo.MOOffQty;

            int ActDate = 0;
            if (mo.MOActualStartDate > 0)
            {
                TimeSpan dateTime = DateTime.Now.Date - Convert.ToDateTime(FormatHelper.ToDateString(mo.MOActualStartDate, "/"));
                ActDate = dateTime.Days;
            }

            // Added by Icyer 2006/12/09
            string[] objs = new string[this.MOViewFieldList.Length];
            Type type = mo.GetType();
            for (int i = 0; i < this.MOViewFieldList.Length; i++)
            {
                MOViewField field = this.MOViewFieldList[i];
                string strValue = string.Empty;
                if (field.FieldName == "MONotActualQty")
                {
                    strValue = moNotComQty.ToString();
                }
                else if (field.FieldName == "MOActualDate")
                {
                    strValue = ActDate.ToString();
                }
                else
                {
                    System.Reflection.FieldInfo fieldInfo = type.GetField(field.FieldName);
                    if (fieldInfo != null)
                    {
                        strValue = fieldInfo.GetValue(mo).ToString();
                        if (field.FieldName.ToUpper().EndsWith("DATE") == true)
                        {
                            if (strValue == string.Empty)
                                strValue = "0";
                            strValue = FormatHelper.ToDateString(int.Parse(strValue));
                        }
                        else if (field.FieldName.ToUpper().EndsWith("TIME") == true)
                        {
                            if (strValue == string.Empty)
                                strValue = "0";
                            strValue = FormatHelper.ToTimeString(int.Parse(strValue));
                        }
                        else if (field.FieldName == "MOStatus")
                        {
                            strValue = this.languageComponent1.GetString(strValue);
                        }
                    }
                }
                objs[i] = strValue;
            }
            return objs;
            // Added end
        }

        private string[] GetColumnHeaderText()
        {
            // Added by Icyer 2006/12/09
            string[] strHeader = new string[this.MOViewFieldList.Length];
            for (int i = 0; i < strHeader.Length; i++)
            {
                strHeader[i] = this.MOViewFieldList[i].Description;
            }
            return strHeader;
            // Added end
        }

        private object[] LoadDataSource()
        {
            return this.LoadDataSource(1, int.MaxValue);
        }
        private void PagerToolBar_OnPagerToolBarClick(object sender, System.EventArgs e)
        {
            this.gridHelper.GridBind(this.pagerToolBar.PageIndex, this.pagerToolBar.PageSize);
        }
        #endregion

        #region WebGrid
        protected override void InitWebGrid()
        {
            base.InitWebGrid();
            // Added by Icyer 2006/12/09
            for (int i = 0; i < this.MOViewFieldList.Length; i++)
            {
                this.gridHelper.AddColumn(this.MOViewFieldList[i].Description, this.languageComponent1.GetString(this.MOViewFieldList[i].Description), null);
            }

            // Added By HI1/Venus.Feng on 20080804 for Hisense Version
            //this.gridHelper.AddLinkColumn("MOConfirm", "普通报工", null);
            //this.gridHelper.AddLinkColumn("MOStorgeConfirm", "库存报工", null);
            this.gridHelper.AddLinkColumn("MOTail", "尾数处理", null);
            // End Added

            this.gridHelper.AddDefaultColumn(true, true);
            // Added end

            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }

        protected DataRow GetGridRow(object obj)
        {
            DataRow row = DtSource.NewRow();
            MO mo = obj as MO;
            //工单的未完工数量＝工单的已投入数量－工单已完工数量－工单的拆解数量-脱离工单数量
            decimal moNotComQty = mo.MOInputQty - mo.MOActualQty - mo.MOScrapQty - mo.MOOffQty;

            int ActDate = 0;
            if (mo.MOActualStartDate > 0)
            {
                TimeSpan dateTime = DateTime.Now.Date - Convert.ToDateTime(FormatHelper.ToDateString(mo.MOActualStartDate, "/"));
                ActDate = dateTime.Days;
            }


            //object[] objs = new object[this.MOViewFieldList.Length + 3];
            //objs[0] = "false";
            //objs[objs.Length - 1] = "";
            //objs[objs.Length - 2] = "";
            //objs[objs.Length - 3] = "";
            Type type = mo.GetType();
            for (int i = 0; i < this.MOViewFieldList.Length; i++)
            {
                MOViewField field = this.MOViewFieldList[i];
                string strValue = string.Empty;
                if (field.FieldName == "MONotActualQty")
                {
                    strValue = moNotComQty.ToString();
                }
                else if (field.FieldName == "MOActualDate")
                {
                    strValue = ActDate.ToString();
                }
                else
                {
                    System.Reflection.FieldInfo fieldInfo = type.GetField(field.FieldName);
                    if (fieldInfo != null)
                    {
                        strValue = fieldInfo.GetValue(mo).ToString();
                        if (field.FieldName.ToUpper().EndsWith("DATE") == true)
                        {
                            if (strValue == string.Empty)
                                strValue = "0";
                            strValue = FormatHelper.ToDateString(int.Parse(strValue));
                        }
                        else if (field.FieldName.ToUpper().EndsWith("TIME") == true)
                        {
                            if (strValue == string.Empty)
                                strValue = "0";
                            strValue = FormatHelper.ToTimeString(int.Parse(strValue));
                        }
                        else if (field.FieldName == "MOStatus")
                        {
                            strValue = this.languageComponent1.GetString(strValue);
                        }
                    }
                }
                row[i + 1] = strValue;
               // objs[i + 1] = strValue;
            }
            return row;
        }

        private object[] LoadDataSource(int inclusive, int exclusive)
        {
            /* modified by jessie lee, 2005/12/8
             * CS187 工单的查询结果栏位增加“导入日期”栏位。
             * 工单的查询条件增加“导入日期过滤”开关项，打开后，导入起始日期默认为10天前，导入结束日期默认为当天。
             * 执行查询，依据导入日期的范围对工单记录进行过滤。 */
            if (this.chbImportDate.Checked)
            {
                PageCheckManager manager = new PageCheckManager();
                manager.Add(new DateRangeCheck(this.lblEnterDate, this.ImportDateFrom.Text, this.ImportDateTo.Text, false));

                if (!manager.Check())
                {
                    WebInfoPublish.Publish(this, manager.CheckMessage, this.languageComponent1);
                    return null;
                }
            }

            if (this.chbUseDate.Checked)
            {
                PageCheckManager manager = new PageCheckManager();
                manager.Add(new DateRangeCheck(this.lblInDateFromQuery, this.dateInDateFromQuery.Text, this.dateInDateToQuery.Text, false));

                if (!manager.Check())
                {
                    WebInfoPublish.Publish(this, manager.CheckMessage, this.languageComponent1);
                    return null;
                }
            }

            if (this.txtActstarDateFrom.Text.Trim() != string.Empty)
            {
                PageCheckManager manager = new PageCheckManager();
                manager.Add(new LengthCheck(this.lblActstarDateFrom, this.txtActstarDateFrom, 5, false));
                manager.Add(new NumberCheck(this.lblActstarDateFrom, this.txtActstarDateFrom, false));               

                if (!manager.Check())
                {
                    WebInfoPublish.Publish(this, manager.CheckMessage, this.languageComponent1);
                    return null;
                }
            }

            if (this.txtActstarDateTo.Text.Trim() != string.Empty)
            {
                PageCheckManager manager = new PageCheckManager();
                manager.Add(new LengthCheck(this.lblActstarDateTo, this.txtActstarDateTo, 5, false));
                manager.Add(new NumberCheck(this.lblActstarDateTo, this.txtActstarDateTo, false));               

                if (!manager.Check())
                {
                    WebInfoPublish.Publish(this, manager.CheckMessage, this.languageComponent1);
                    return null;
                }
            }

            if (this.txtActstarDateFrom.Text.Trim() != string.Empty && this.txtActstarDateTo.Text.Trim() != string.Empty)
            {
                if (Convert.ToInt32(this.txtActstarDateTo.Text.Trim()) - Convert.ToInt32(this.txtActstarDateFrom.Text.Trim()) < 0)
                {
                    WebInfoPublish.Publish(this, "$CS_ActstarDate_Must_Over_Zero", this.languageComponent1);
                    return null;
                }
            }

            int ActstarDateFrom = 0;
            if (this.txtActstarDateTo.Text.Trim() != string.Empty)
            {
                ActstarDateFrom = FormatHelper.TODateInt(DateTime.Now.Date.AddDays(-Convert.ToInt32(this.txtActstarDateTo.Text.Trim())));
            }

            int ActstarDateTo =0;
            if (this.txtActstarDateFrom.Text.Trim() != string.Empty && Convert.ToInt32(this.txtActstarDateFrom.Text.Trim()) != 0)
            {
                ActstarDateTo = FormatHelper.TODateInt(DateTime.Now.Date.AddDays(-Convert.ToInt32(this.txtActstarDateFrom.Text.Trim())));
            }


            if (_facade == null)
            {
                _facade = new MOFacade(this.DataProvider);
            }

            if (this.chbImportDate.Checked && this.chbUseDate.Checked)
            {
                return this._facade.QueryMOIllegibility(
                    FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtMoCodeQuery.Text)),
                    FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtItemCodeQuery.Text)),
                    FormatHelper.CleanString(this.txtItemDescription.Text),
                    FormatHelper.CleanString(this.drpMoTypeQuery.SelectedValue),
                    FormatHelper.CleanString(this.drpMoStatusQuery.SelectedValue),
                    string.Empty,
                    FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtFactoryQuery.Text)),
                    FormatHelper.TODateInt(this.dateInDateFromQuery.Text),
                    FormatHelper.TODateInt(this.dateInDateToQuery.Text),
                    FormatHelper.TODateInt(this.ImportDateFrom.Text),
                    FormatHelper.TODateInt(this.ImportDateTo.Text),
                    ActstarDateFrom,
                    ActstarDateTo,
                    inclusive,
                    exclusive);
            }
            else if (this.chbImportDate.Checked && !this.chbUseDate.Checked)
            {
                return this._facade.QueryMOIllegibility(
                    FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtMoCodeQuery.Text)),
                    FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtItemCodeQuery.Text)),
                    FormatHelper.CleanString(this.txtItemDescription.Text),
                    FormatHelper.CleanString(this.drpMoTypeQuery.SelectedValue),
                    FormatHelper.CleanString(this.drpMoStatusQuery.SelectedValue),
                    string.Empty,
                    FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtFactoryQuery.Text)),
                    0,
                    0,
                    FormatHelper.TODateInt(this.ImportDateFrom.Text),
                    FormatHelper.TODateInt(this.ImportDateTo.Text),
                    ActstarDateFrom,
                    ActstarDateTo,
                    inclusive,
                    exclusive);
            }
            else if (!this.chbImportDate.Checked && this.chbUseDate.Checked)
            {
                return this._facade.QueryMOIllegibility(
                    FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtMoCodeQuery.Text)),
                    FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtItemCodeQuery.Text)),
                    FormatHelper.CleanString(this.txtItemDescription.Text),
                    FormatHelper.CleanString(this.drpMoTypeQuery.SelectedValue),
                    FormatHelper.CleanString(this.drpMoStatusQuery.SelectedValue),
                    string.Empty,
                    FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtFactoryQuery.Text)),
                    FormatHelper.TODateInt(this.dateInDateFromQuery.Text),
                    FormatHelper.TODateInt(this.dateInDateToQuery.Text),
                    0,
                    0,
                    ActstarDateFrom,
                    ActstarDateTo,
                    inclusive,
                    exclusive);
            }

            return this._facade.QueryMOIllegibility(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtMoCodeQuery.Text)),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtItemCodeQuery.Text)),
                FormatHelper.CleanString(this.txtItemDescription.Text),
                FormatHelper.CleanString(this.drpMoTypeQuery.SelectedValue),
                FormatHelper.CleanString(this.drpMoStatusQuery.SelectedValue),
                string.Empty,
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtFactoryQuery.Text)),
                0,
                0,
                0,
                0,
                ActstarDateFrom,
                ActstarDateTo,
                inclusive,
                exclusive);
        }


        private int GetRowCount()
        {
            if (this.txtActstarDateFrom.Text.Trim() != string.Empty)
            {
                PageCheckManager manager = new PageCheckManager();               
                manager.Add(new LengthCheck(this.lblActstarDateFrom, this.txtActstarDateFrom, 5, false));
                manager.Add(new NumberCheck(this.lblActstarDateFrom, this.txtActstarDateFrom, false));

                if (!manager.Check())
                {
                    WebInfoPublish.Publish(this, manager.CheckMessage, this.languageComponent1);
                    return 0;
                }
            }

            if (this.txtActstarDateTo.Text.Trim() != string.Empty)
            {
                PageCheckManager manager = new PageCheckManager();               
                manager.Add(new LengthCheck(this.lblActstarDateTo, this.txtActstarDateTo,5, false));
                manager.Add(new NumberCheck(this.lblActstarDateTo, this.txtActstarDateTo, false));

                if (!manager.Check())
                {
                    WebInfoPublish.Publish(this, manager.CheckMessage, this.languageComponent1);
                    return 0;
                }
            }

            if (this.txtActstarDateFrom.Text.Trim() != string.Empty && this.txtActstarDateTo.Text.Trim() != string.Empty)
            {
                if (Convert.ToInt32(this.txtActstarDateTo.Text.Trim()) - Convert.ToInt32(this.txtActstarDateFrom.Text.Trim()) < 0)
                {
                    WebInfoPublish.Publish(this, "$CS_ActstarDate_Must_Over_Zero", this.languageComponent1);
                    return 0;
                }
            }
            
            int ActstarDateFrom = 0;
            if (this.txtActstarDateTo.Text.Trim() != string.Empty)
            {
                ActstarDateFrom = FormatHelper.TODateInt(DateTime.Now.Date.AddDays(-Convert.ToInt32(this.txtActstarDateTo.Text.Trim())));
            }

            int ActstarDateTo = 0;
            if (this.txtActstarDateFrom.Text.Trim() != string.Empty && Convert.ToInt32(this.txtActstarDateFrom.Text.Trim()) != 0)
            {
                ActstarDateTo = FormatHelper.TODateInt(DateTime.Now.Date.AddDays(-Convert.ToInt32(this.txtActstarDateFrom.Text.Trim())));
            }


            if (_facade == null)
            {
                _facade = new MOFacade(base.DataProvider);
            }

            if (this.chbImportDate.Checked && this.chbUseDate.Checked)
            {
                return this._facade.QueryMOIllegibilityCount(
                    FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtMoCodeQuery.Text)),
                    FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtItemCodeQuery.Text)),
                    FormatHelper.CleanString(this.txtItemDescription.Text),
                    FormatHelper.CleanString(this.drpMoTypeQuery.SelectedValue),
                    FormatHelper.CleanString(this.drpMoStatusQuery.SelectedValue),
                    string.Empty,
                    FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtFactoryQuery.Text)),
                    FormatHelper.TODateInt(this.dateInDateFromQuery.Text),
                    FormatHelper.TODateInt(this.dateInDateToQuery.Text),
                    FormatHelper.TODateInt(this.ImportDateFrom.Text),
                    FormatHelper.TODateInt(this.ImportDateTo.Text),
                    ActstarDateFrom,
                    ActstarDateTo);
            }
            else if (this.chbImportDate.Checked && !this.chbUseDate.Checked)
            {
                return this._facade.QueryMOIllegibilityCount(
                    FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtMoCodeQuery.Text)),
                    FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtItemCodeQuery.Text)),
                    FormatHelper.CleanString(this.txtItemDescription.Text),
                    FormatHelper.CleanString(this.drpMoTypeQuery.SelectedValue),
                    FormatHelper.CleanString(this.drpMoStatusQuery.SelectedValue),
                    string.Empty,
                    FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtFactoryQuery.Text)),
                    0,
                    0,
                    FormatHelper.TODateInt(this.ImportDateFrom.Text),
                    FormatHelper.TODateInt(this.ImportDateTo.Text),
                    ActstarDateFrom,
                    ActstarDateTo);
            }
            else if (!this.chbImportDate.Checked && this.chbUseDate.Checked)
            {
                return this._facade.QueryMOIllegibilityCount(
                    FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtMoCodeQuery.Text)),
                    FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtItemCodeQuery.Text)),
                    FormatHelper.CleanString(this.txtItemDescription.Text),
                    FormatHelper.CleanString(this.drpMoTypeQuery.SelectedValue),
                    FormatHelper.CleanString(this.drpMoStatusQuery.SelectedValue),
                    string.Empty,
                    FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtFactoryQuery.Text)),
                    FormatHelper.TODateInt(this.dateInDateFromQuery.Text),
                    FormatHelper.TODateInt(this.dateInDateToQuery.Text),
                    0,
                    0,
                    ActstarDateFrom,
                    ActstarDateTo);
            }

            return this._facade.QueryMOIllegibilityCount(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtMoCodeQuery.Text)),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtItemCodeQuery.Text)),
                FormatHelper.CleanString(this.txtItemDescription.Text),
                FormatHelper.CleanString(this.drpMoTypeQuery.SelectedValue),
                FormatHelper.CleanString(this.drpMoStatusQuery.SelectedValue),
                string.Empty,
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtFactoryQuery.Text)),
                0,
                0,
                0,
                0,
                ActstarDateFrom,
                ActstarDateTo);
        }

        #endregion

        #region Button


        protected void cmdDelete_ServerClick(object sender, System.EventArgs e)
        {
            if (_facade == null) { _facade = new MOFacade(base.DataProvider); }
            ArrayList array = this.gridHelper.GetCheckedRows();
            if (array.Count > 0)
            {
                ArrayList objs = new ArrayList(array.Count);

                foreach (GridRecord row in array)
                {
                    object mo = this.GetEditObject(row);
                    if (mo != null)
                    {
                        objs.Add((MO)mo);
                    }
                }

                this._facade.DeleteMO((MO[])objs.ToArray(typeof(MO)));

                this.RequestData();
            }
        }


        protected void cmdQuery_ServerClick(object sender, System.EventArgs e)
        {
            this.SetSearchSession();
            this.RequestData();
        }

        private void RequestData()
        {
            // 2005-04-06
            this.pagerToolBar.PageSize = this.pagerSizeSelector.PageSize;
            this.gridHelper.GridBind(PageGridBunding.Page, this.pagerSizeSelector.PageSize);

            this.pagerToolBar.RowCount = GetRowCount();
            this.pagerToolBar.InitPager();
        }

        private void gridWebGrid_DblClick(object sender, Infragistics.WebUI.UltraWebGrid.ClickEventArgs e)
        {
            Response.Redirect(this.MakeRedirectUrl("FMOEP.aspx", new string[] { "ACT", "MOCode" }, new string[] { "EDIT", e.Row.Cells[2].Text.Trim() }));
        }

        #endregion

        #region Object <--> Page
        private object GetEditObject(GridRecord row)
        {
            if (_facade == null) { _facade = new MOFacade(base.DataProvider); }
            //object obj = _facade.GetMO( row.Cells[2].Text.ToString() );
            object obj = _facade.GetMO(row.Items.FindItemByKey("Mo_MOCode").Text);

            if (obj != null)
            {
                return obj;
            }

            return null;
        }

        #endregion

        #region form events
        protected void cmdDownload_ServerClick(object sender, System.EventArgs e)
        {
            Response.Redirect(this.MakeRedirectUrl("FMODownload.aspx"));
        }

        protected void cmdInitial_ServerClick(object sender, System.EventArgs e)
        {
            this.SetMOStatus(MOManufactureStatus.MOSTATUS_INITIAL);
        }

        private void SetMOStatus(string newStatus)
        {
            if (_facade == null) { _facade = new MOFacade(base.DataProvider); }
            ModelFacade modelFacade = new FacadeFactory(base.DataProvider).CreateModelFacade();
            ArrayList array = this.gridHelper.GetCheckedRows();
            if (array.Count > 0)
            {
                ArrayList objs = new ArrayList(array.Count);

                foreach (GridRecord row in array)
                {
                    object obj = this.GetEditObject(row);
                    MO mo = null;
                    if (obj != null)
                    {
                        mo = obj as MO;
                        mo.MOStatus = newStatus;
                        mo.MaintainUser = this.GetUserCode();

                        if (mo.EAttribute3 == AlertJob.alertJob)
                        {
                            ExceptionManager.Raise(this.GetType(), string.Format("$MOCode = {0}, $MO_STATUS_CHANGE", mo.MOCode));
                            return;
                        }

                        if (newStatus == MOManufactureStatus.MOSTATUS_RELEASE)
                        {
                            string itemCode = mo.ItemCode;
                            object model = modelFacade.GetModelByItemCode(itemCode);
                            if (model == null)
                            {
                                ExceptionManager.Raise(this.GetType(), string.Format("$Domain_ItemCode = {0}, $Error_Item_Not_Maintain_Model", itemCode));
                                return;
                            }
                            mo.MOReleaseDate = FormatHelper.TODateInt(DateTime.Now);
                            mo.MOReleaseTime = FormatHelper.TOTimeInt(DateTime.Now);


                            SystemSettingFacade systemSettingFacade = new SystemSettingFacade(this.DataProvider);
                            if (mo.MOMemo == null || mo.MOMemo.Trim().Length <= 0)
                            {
                                //如果下发时momemo字段为空，从系统参数中抓取
                                string moMemo = systemSettingFacade.GetParameterAlias("DEFAULT_MO_PRODUCT_TYPE", "MPR");
                                if (moMemo.Trim().Length > 0)
                                {
                                    mo.MOMemo = moMemo.Trim();
                                }
                            }
                            else
                            {
                                //检查是否该值和维护在参数中的一致，包括大小写也需要一致。
                                //如果不一致，则提示：工单类别维护错误，请检查。
                                object[] parameterList = systemSettingFacade.QueryParameter(string.Empty, "MO_PRODUCT_TYPE", int.MinValue, int.MaxValue);
                                bool found = false;
                                if (parameterList != null)
                                {
                                    foreach (Domain.BaseSetting.Parameter parameter in parameterList)
                                    {
                                        if (string.Compare(mo.MOMemo, parameter.ParameterAlias, false) == 0)
                                        {
                                            found = true;
                                            break;
                                        }
                                    }
                                }
                                if (!found)
                                {
                                    ExceptionManager.Raise(this.GetType(), "$CS_WrongMOMemo $MOCode=" + mo.MOCode);
                                }
                            }
                        }
                        if (newStatus == MOManufactureStatus.MOSTATUS_INITIAL)
                        {
                            mo.MOReleaseDate = 0;
                            mo.MOReleaseTime = 0;
                        }
                        if (newStatus == MOManufactureStatus.MOSTATUS_CLOSE)
                        {
                            mo.MOActualEndDate = FormatHelper.TODateInt(DateTime.Now);
                        }
                        objs.Add(mo);
                    }
                }

                if (objs.Count == 0)
                {
                    return;
                }


                this._facade.MOStatusChanged((MO[])objs.ToArray(typeof(MO)));

                this.RequestData();
            }

        }

        protected void cmdRelease_ServerClick(object sender, System.EventArgs e)
        {
            //OPBOM中需要数据 ()
            ArrayList array = this.gridHelper.GetCheckedRows();
            if (array.Count <= 0)
                return;

            this.SetMOStatus(MOManufactureStatus.MOSTATUS_RELEASE);
        }

        protected void cmdMOOpen_ServerClick(object sender, System.EventArgs e)
        {
            this.SetMOStatus(MOManufactureStatus.MOSTATUS_OPEN);
        }

        protected void cmdPending_ServerClick(object sender, System.EventArgs e)
        {
            this.SetMOStatus(MOManufactureStatus.MOSTATUS_PENDING);
        }

        protected void cmdMOClose_ServerClick(object sender, System.EventArgs e)
        {
            this.SetMOStatus(MOManufactureStatus.MOSTATUS_CLOSE);
        }

        protected void cmdMOExport_ServerClick(object sender, System.EventArgs e)
        {
            string downloadPhysicalPath = this.Request.PhysicalApplicationPath + @"upload\";
            if (!Directory.Exists(downloadPhysicalPath))
            {
                Directory.CreateDirectory(downloadPhysicalPath);
            }

            string filename = string.Format("{0}_{1}", FormatHelper.TODateInt(System.DateTime.Now).ToString(), FormatHelper.TOTimeInt(System.DateTime.Now).ToString());
            string filepath = string.Format(@"{0}{1}{2}", downloadPhysicalPath, filename, ".csv");

            while (File.Exists(filepath))
            {
                filename = string.Format("{0}_{1}", filename, "0");
                filepath = string.Format(@"{0}{1}{2}", downloadPhysicalPath, filename, ".csv");
            }

            StreamWriter writer = new StreamWriter(filepath, false, System.Text.Encoding.GetEncoding("GB2312"));
            writer.WriteLine("工单代码" + "," + "计划数量" + "," + "工单别" + "," + "工单BOM" + "," + "计划开工日期" + ","
              + "计划开工时间" + "," + "计划完工日期" + "," + "计划完工时间" + "," + "计划生产产线" + "," + "客户代码" + ","
              + "客户名称" + "," + "客户订单号" + "," + "客户料号" + "," + "订单号" + "," + "订单项次"
              + "," + "预留栏位1" + "," + "预留栏位4" + "," + "预留栏位5" + "," + "预留栏位6");

            if (_facade == null)
            {
                _facade = new MOFacade(base.DataProvider);
            }
            ArrayList array = this.gridHelper.GetCheckedRows();
            if (array.Count > 0)
            {
                ArrayList objs = new ArrayList(array.Count);

                foreach (GridRecord row in array)
                {
                    object obj = this.GetEditObject(row);
                    if (obj != null)
                    {
                        MO objMO = obj as MO;
                        writer.WriteLine(ReplaceStr(objMO.MOCode) + "," + ReplaceStr(objMO.MOPlanQty.ToString()) + ","
                            + ReplaceStr(objMO.MOMemo) + "," + ReplaceStr(objMO.BOMVersion) + "," + ReplaceStr(objMO.MOPlanStartDate.ToString()) + ","
                            + ReplaceStr(objMO.MOPlanStartTime.ToString()) + "," + ReplaceStr(objMO.MOPlanEndDate.ToString()) + "," + ReplaceStr(objMO.MOPlanEndTime.ToString()) + ","
                            + ReplaceStr(objMO.MOPlanLine) + "," + ReplaceStr(objMO.CustomerCode) + ","
                            + ReplaceStr(objMO.CustomerName) + "," + ReplaceStr(objMO.CustomerOrderNO) + "," + ReplaceStr(objMO.CustomerItemCode) + ","
                            + ReplaceStr(objMO.OrderNO) + "," + ReplaceStr(objMO.OrderSequence.ToString()) + ","
                            + ReplaceStr(objMO.EAttribute1) + ","
                            + ReplaceStr(objMO.EAttribute4) + "," + ReplaceStr(objMO.EAttribute5) + "," + ReplaceStr(objMO.EAttribute6));
                    }
                }

            }

            writer.Flush();
            writer.Close();

            this.DownloadFile(filename);

            //Response.Write(@"<iframe width='0' height='0' src="
            //        + string.Format(@"{0}FDownload.aspx", pageVirtualHostRoot)
            //        + "?fileName=" + string.Format(@"{0}{1}", pageVirtualHostRoot + "upload/", filename + ".csv")
            //        + "></iframe><script language=javascript>window.setTimeout('history.back()',2000);</script>"
            //        );
        }

        protected string ReplaceStr(string str)
        {
            string newStr = "\"" + str.Replace("\"", "\"\"") + "\"";

            return newStr;
        }

        protected void cmdImport_ServerClick(object sender, System.EventArgs e)
        {
            Response.Redirect(this.MakeRedirectUrl("FMOOutDataImport.aspx"));
        }

        protected override void Grid_ClickCell(GridRecord row, string commandName)
        {
            if (_facade == null) { _facade = new MOFacade(base.DataProvider); }
            #region 保留编辑工单信息
            MO mo = (MO)(this._facade.GetMO(row.Items.FindItemByKey("Mo_MOCode").Text.Trim()));
            //this.drpMoStatusQuery.SelectedValue = mo.MOStatus ;
            this.drpMoStatusQuery.SelectedValue = string.Empty;
            this.txtMoCodeQuery.Text = mo.MOCode;
            SetSearchSession();
            #endregion

            if (commandName == "Edit")
            {
                Response.Redirect(this.MakeRedirectUrl("FMOEP.aspx", new string[] { "ACT", "MOCode" }, new string[] { "EDIT", mo.MOCode }));
            }
            else if (commandName == "MO2RouteEdit")
            {
                Response.Redirect(this.MakeRedirectUrl("FMO2RouteSP.aspx", new string[] { "MOCode" }, new string[] { mo.MOCode }));
            }
            else if (commandName == "MOConfirm")
            {
                Response.Redirect(this.MakeRedirectUrl("FMOConfirmMP.aspx", new string[] { "MOCode", "OrgID" }, new string[] { mo.MOCode, mo.OrganizationID.ToString() }));
            }
            else if (commandName == "MOTail")
            {
                Response.Redirect(this.MakeRedirectUrl("FMOTailMP.aspx", new string[] { "MOCode" }, new string[] { mo.MOCode }));
            }
            else if (commandName == "MOStorgeConfirm")
            {
                Response.Redirect(this.MakeRedirectUrl("FMOStorgeConfirmMP.aspx", new string[] { "MOCode", "OrgID" }, new string[] { mo.MOCode, mo.OrganizationID.ToString() }));
            }
        }

        protected void cmdGridExport_ServerClick(object sender, System.EventArgs e)
        {
            this.excelExporter.Export();
        }

        #endregion

        #region Init DropDownLists

        private void drpMoTypeQuery_Load(object sender, System.EventArgs e)
        {
            if (!this.IsPostBack)
            {
                //update by crystal chu 2005/04/22
                SystemParameterListBuilder _builder = new SystemParameterListBuilder("MOTYPE", base.DataProvider);
                _builder.Build(this.drpMoTypeQuery);
                _builder.AddAllItem(languageComponent1);


                SystemSettingFacade sysFacade = new SystemSettingFacade(this.DataProvider);
                object[] moTypeList = sysFacade.GetParametersByParameterGroup("MOTYPE");

                drpMoTypeQuery.Items.Clear();
                drpMoTypeQuery.Items.Add(new ListItem("", ""));

                if (moTypeList != null)
                {
                    foreach (BenQGuru.eMES.Domain.BaseSetting.Parameter param in moTypeList)
                    {
                        string showText = this.languageComponent1.GetString(param.ParameterValue.Trim().ToUpper());
                        bool found = false;
                        foreach (ListItem item in drpMoTypeQuery.Items)
                        {
                            if (item.Text.Trim().ToUpper() == showText)
                            {
                                item.Value += "," + param.ParameterCode;
                                found = true;
                                break;
                            }
                        }
                        if (!found)
                        {
                            drpMoTypeQuery.Items.Add(new ListItem(showText, param.ParameterCode));
                        }
                    }
                }
            }
        }

        private void drpMoStatusQuery_Load(object sender, System.EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (_facade == null) { _facade = new MOFacade(base.DataProvider); }
                //update by crystal chu 2005/04/22
                DropDownListBuilder _builder = new DropDownListBuilder(this.drpMoStatusQuery);
                _builder.AddAllItem(this.languageComponent1);
                string[] moStatuses = this._facade.GetMOStatuses();
                foreach (string item in moStatuses)
                {
                    this.drpMoStatusQuery.Items.Add(new ListItem(this.languageComponent1.GetString(item), item));
                }
            }
        }


        #endregion

        private MOViewField[] viewFieldList = null;
        private MOViewField[] MOViewFieldList
        {
            get
            {
                if (viewFieldList == null)
                {
                    MOFacade moFacade = new MOFacade(this.DataProvider);
                    object[] objs = moFacade.QueryMOViewFieldByUserCode(this.GetUserCode());
                    if (objs != null)
                    {
                        viewFieldList = new MOViewField[objs.Length];
                        objs.CopyTo(viewFieldList, 0);
                    }
                    else
                    {
                        objs = moFacade.QueryMOViewFieldDefault();
                        if (objs != null)
                        {
                            ArrayList list = new ArrayList();
                            for (int i = 0; i < objs.Length; i++)
                            {
                                MOViewField field = (MOViewField)objs[i];
                                if (FormatHelper.StringToBoolean(field.IsDefault) == true)
                                {
                                    list.Add(field);
                                }
                            }
                            viewFieldList = new MOViewField[list.Count];
                            list.CopyTo(viewFieldList, 0);
                        }
                    }
                    if (viewFieldList != null)
                    {
                        bool bExistMOCode = false;
                        for (int i = 0; i < viewFieldList.Length; i++)
                        {
                            if (viewFieldList[i].FieldName == "MOCode")
                            {
                                bExistMOCode = true;
                                break;
                            }
                        }
                        if (bExistMOCode == false)
                        {
                            MOViewField field = new MOViewField();
                            field.FieldName = "MOCode";
                            field.Description = "Mo_MOCode";
                            ArrayList list = new ArrayList();
                            list.Add(field);
                            list.AddRange(viewFieldList);
                            viewFieldList = new MOViewField[list.Count];
                            list.CopyTo(viewFieldList, 0);
                        }
                    }
                }
                return viewFieldList;
            }
        }
    }

}
