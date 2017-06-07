using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.Config;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.WebQuery;
using BenQGuru.Web.ReportCenter.UserControls;


using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;


namespace BenQGuru.Web.ReportCenter
{
    public partial class FIndexIQCManPerformanceCountQP : BaseQPageNew
    {
        #region 页面初始化

        private System.ComponentModel.IContainer components;
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;

        private const string preferredTable = "tblmaterial,tblitemclass,tblmo,tbltimedimension,tbluser,tblasniqc,**";

        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);

            //初始化控件的位置和可见性
            InitWhereControls();
            InitGroupControls();
            InitResultControls();
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.languageComponent1 = new ControlLibrary.Web.Language.LanguageComponent(this.components);
            this.languageComponent1.Language = "CHS";
            this.languageComponent1.LanguagePackageDir = "\\\\grd2-build\\language pack\\";
            this.languageComponent1.RuntimePage = null;
            this.languageComponent1.RuntimeUserControl = null;
            this.languageComponent1.UserControlName = "";
        }

        private void InitWhereControls()
        {
            this.UCWhereConditions1.SetControlPosition(0, 0, UCWhereConditions1.PanelInspectorWhere.ID);
            this.UCWhereConditions1.SetControlPosition(0, 1, UCWhereConditions1.PanelFirstClassWhere.ID);


            this.UCWhereConditions1.SetControlPosition(1, 0, UCWhereConditions1.PanelStartDateWhere.ID);
            this.UCWhereConditions1.SetControlPosition(1, 1, UCWhereConditions1.PanelEndDateWhere.ID);
        }

        private void InitGroupControls()
        {
            this.UCGroupConditions1.ShowShiftDay = true;
            this.UCGroupConditions1.ShowWeek = true;
            this.UCGroupConditions1.ShowMonth = true;
            this.UCGroupConditions1.ShowYear = true;

            this.UCGroupConditions1.ShowByTimePanel = true;

            this.UCGroupConditions1.ShowInspector = true;

            this.UCGroupConditions1.ShowFirstClass = true;
            this.UCGroupConditions1.ShowSecondClass = true;
        }

        private void InitResultControls()
        {
            //this.gridWebGrid.Visible = false;
            this.cmdGridExport.Visible = false;
        }

        #endregion

        #region 公用属性和事件处理

        private void LoadDisplayControls()
        {
            if (!this.IsPostBack)
            {
                List<ListItem> displayList = new List<ListItem>();
                displayList.Add(new ListItem(this.languageComponent1.GetString(NewReportDisplayType.Grid), NewReportDisplayType.Grid));
                this.UCDisplayConditions1.DisplayList = displayList;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.gridHelper = new GridHelperNew(this.gridWebGrid,this.DtSource);
            if (!this.IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);

                this.UCWhereConditions1.InitUserControl(this.languageComponent1, this.DataProvider);
                this.UCGroupConditions1.InitUserControl(this.languageComponent1, this.DataProvider);
                this.UCDisplayConditions1.InitUserControl(this.languageComponent1, this.DataProvider);

                this.RefreshController1.Interval = ConfigSection.Current.DomainSetting.Interval;
            }

            LoadDisplayControls();
            this.gridWebGrid.Behaviors.RowSelectors.RowNumbering = false;
            this.gridWebGrid.Behaviors.Sorting.Enabled = false;
        }

        protected override void OnPreRender(EventArgs e)
        {
            ReportPageHelper.SetControlValue(this, this.Request.Params);
            ReportPageHelper.DoQueryForBSHome(this, this.Request.Params, this.DoQuery);

            if (this.AutoRefresh)
            {
                this.DoQuery();
            }

            base.OnPreRender(e);
        }

        public bool AutoRefresh
        {
            get
            {
                if (this.ViewState["AutoRefresh"] != null)
                {
                    try
                    {
                        return bool.Parse(this.ViewState["AutoRefresh"].ToString());
                    }
                    catch
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            set
            {
                this.ViewState["AutoRefresh"] = value.ToString();

                if (value)
                {
                    this.RefreshController1.Start();
                }
                else
                {
                    this.RefreshController1.Stop();
                }
            }
        }

        protected void cmdGridExport_ServerClick(object sender, System.EventArgs e)
        {
            this.GridExport(this.gridWebGrid);
        }

        protected void cmdQuery_ServerClick(object sender, System.EventArgs e)
        {
            this.DoQuery();
        }

        protected void chkRefreshAuto_CheckedChanged(object sender, System.EventArgs e)
        {
            this.DoQuery();
        }

        #endregion

        #region 使用ReportSQLEngine相关的函数

        private object[] LoadDataSource(bool isForCompare, bool roundDate)
        {
            string inputOutput = UCWhereConditions1.UserSelectInputOutput;
            string byTimeType = UCGroupConditions1.UserSelectByTimeType;
            string compareType = UCGroupConditions1.UserSelectCompareType;
            string completeType = UCGroupConditions1.UserSelectCompleteType;

            if (!isForCompare)
            {
                compareType = string.Empty;
            }

            bool bigSSChecked = UCGroupConditions1.BigSSChecked;
            bool opResChecked = UCGroupConditions1.OPChecked || UCGroupConditions1.ResChecked;
            bool segSSChecked = UCGroupConditions1.SegChecked || UCGroupConditions1.SSChecked;

            string groupFieldsX = this.UCGroupConditions1.GetGroupFieldList(preferredTable, "X");
            string groupFieldsY = this.UCGroupConditions1.GetGroupFieldList(preferredTable, "Y");


            ReportSQLEngine engine = new ReportSQLEngine(this.DataProvider, this.languageComponent1);
            engine.DetailedCoreTable = GetCoreTableDetail();
            engine.Formular = GetFormular();
            engine.WhereCondition = this.UCWhereConditions1.GetWhereSQLStatement(preferredTable, byTimeType, roundDate, 0);

            engine.GroupFieldsX = groupFieldsX;
            engine.GroupFieldsY = groupFieldsY;

            return engine.GetReportDataSource(byTimeType, 0);
        }

        private string GetCoreTableDetail()
        {
            string returnValue = string.Empty;

            //Modified By Nettie Chen 2009/09/22 get INSPECTOR,INSPDATE from TBLIQCDETAIL
            //returnValue += "SELECT A.INSPECTOR,A.INSPDATE AS SHIFTDAY,B.ITEMCODE,COUNT(1) AS QUALIFIEDQTY,0 AS UNQUALIFIEDQTY";
            //returnValue += " FROM TBLASNIQC A, TBLIQCDETAIL B   WHERE A.IQCNO = B.IQCNO  AND B.CHECKSTATUS = 'Qualified'";
            //returnValue += "  GROUP BY A.INSPECTOR, A.INSPDATE, B.ITEMCODE   UNION   SELECT A.INSPECTOR,";
            //returnValue += "  A.INSPDATE,B.ITEMCODE,0 ASQUALIFIEDQTY,COUNT(1) AS UNQUALIFIEDQTY  FROM TBLASNIQC A,";
            //returnValue += " TBLIQCDETAIL B  WHERE A.IQCNO = B.IQCNO   AND B.CHECKSTATUS = 'UnQualified'";
            //returnValue += " GROUP BY A.INSPECTOR, A.INSPDATE, B.ITEMCODE";
            returnValue += "SELECT B.DINSPECTOR AS INSPECTOR,B.DINSPDATE AS SHIFTDAY,B.ITEMCODE,COUNT(1) AS QUALIFIEDQTY,0 AS UNQUALIFIEDQTY";
            returnValue += " FROM TBLASNIQC A, TBLIQCDETAIL B   WHERE A.IQCNO = B.IQCNO  AND B.CHECKSTATUS = 'Qualified'";
            returnValue += "  GROUP BY B.DINSPECTOR, B.DINSPDATE, B.ITEMCODE   UNION   SELECT B.DINSPECTOR AS INSPECTOR,";
            returnValue += "  B.DINSPDATE,B.ITEMCODE,0 ASQUALIFIEDQTY,COUNT(1) AS UNQUALIFIEDQTY  FROM TBLASNIQC A,";
            returnValue += " TBLIQCDETAIL B  WHERE A.IQCNO = B.IQCNO   AND B.CHECKSTATUS = 'UnQualified'";
            returnValue += " GROUP BY B.DINSPECTOR, B.DINSPDATE, B.ITEMCODE";
            //End Modified
            return returnValue;
        }

        private string GetFormular()
        {
            string returnValue = string.Empty;
            string YRForm = string.Empty;

            YRForm = " SUM(QUALIFIEDQTY) AS QUALIFIEDQTY,SUM(UNQUALIFIEDQTY) UNQUALIFIEDQTY,SUM(QUALIFIEDQTY + UNQUALIFIEDQTY) AS ALLQTY ";

            returnValue = YRForm;
            return returnValue;
        }

        #endregion

        #region 相关的函数

        private List<string> GetRows(string byTimeType)
        {
            List<string> returnValue = new List<string>();

            if (byTimeType == NewReportByTimeType.ShiftDay)
            {
                returnValue.Add("ShiftDay");
            }

            if (byTimeType == NewReportByTimeType.Week)
            {
                returnValue.Add("Week");
            }

            if (byTimeType == NewReportByTimeType.Month)
            {
                returnValue.Add("Month");
            }

            if (byTimeType == NewReportByTimeType.Year)
            {
                returnValue.Add("Year");
            }

            string columnString = UCGroupConditions1.GetGroupFieldAliasList(preferredTable, "Y");

            if (columnString.Trim().Length > 0)
            {
                returnValue.AddRange(columnString.Split(','));

                for (int i = 1; i < returnValue.Count; i++)
                {
                    returnValue[i] = DomainObjectUtility.GetPropertyNameByFieldName(typeof(NewReportDomainObject), returnValue[i]);
                }
            }

            returnValue.Add("QUALIFIEDQTY");
            returnValue.Add("UNQUALIFIEDQTY");
            returnValue.Add("ALLQTY");

            return returnValue;
        }

        #endregion

        protected override void DoQuery()
        {
            base.DoQuery();
            if (true)
            {
                this.AutoRefresh = this.chbRefreshAuto.Checked;

                string compareType = this.UCGroupConditions1.UserSelectCompareType.Trim().ToLower();
                string byTimeType = this.UCGroupConditions1.UserSelectByTimeType.Trim().ToLower();
                string inputOutput = this.UCWhereConditions1.UserSelectInputOutput.Trim().ToLower();
                object[] dateSource = null;

                //一般数据
                dateSource = this.LoadDataSource(false, compareType.Trim().Length > 0);

                if (dateSource == null || dateSource.Length <= 0)
                {
                    this.gridWebGrid.Visible = false;
                    this.cmdGridExport.Visible = false;
                    return;
                }

                //数据加载到Grid
                List<string> fixedColumnList = GetRows(byTimeType);                

                List<ReportGridDim3Property> dim3PropertyList = new List<ReportGridDim3Property>();

                ReportGridHelperNew reportGridHelper = new ReportGridHelperNew(this.DataProvider, this.languageComponent1, this.gridWebGrid,this.DtSource);
                reportGridHelper.DataSource = dateSource;
                reportGridHelper.Dim1PropertyList = fixedColumnList;
                reportGridHelper.Dim3PropertyList = dim3PropertyList;
                reportGridHelper.HasDim3PropertyNameRowColumn = true;
                reportGridHelper.CompareType = compareType;
                reportGridHelper.ByTimeType = byTimeType;

                reportGridHelper.ShowNormalGrid();
                base.InitWebGrid();

                this.gridWebGrid.Behaviors.RowSelectors.RowNumbering = false;
                this.gridWebGrid.Behaviors.Sorting.Enabled = false;

                this.gridWebGrid.Visible = true;
                this.cmdGridExport.Visible = true;

                ReportPageHelper.SetPageScrollToBottom(this);
            }
            else
            {
                this.chbRefreshAuto.Checked = false;
                this.AutoRefresh = false;
            }
        }
    }
}
