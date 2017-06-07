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
    public partial class FNewReportFirstPassYieldByECSGQP : BaseQPageNew
    {
        #region 页面初始化

        //private System.ComponentModel.IContainer components;
        //protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;

        private const string preferredTableForError = "tblmaterial,tblmo,tblres,tblline2crew,tblitemclass,tbltimedimension,**";
        private const string preferredTableForOutput = "tblmesentitylist,tblmaterial,tblmo,tblres,tblline2crew,tblitemclass,tbltimedimension";
        private const string preferredTable = "tblmaterial,tblmo,tblres,tblline2crew,tblitemclass,tbltimedimension,**";

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
            this.UCWhereConditions1.SetControlPosition(0, 0, UCWhereConditions1.PanelGoodSemiGoodWhere.ID);
            this.UCWhereConditions1.SetControlPosition(0, 1, UCWhereConditions1.PanelItemCodeWhere.ID);
            this.UCWhereConditions1.SetControlPosition(0, 2, UCWhereConditions1.PanelMaterialModelCodeWhere.ID);

            this.UCWhereConditions1.SetControlPosition(1, 0, UCWhereConditions1.PanelMOCodeWhere.ID);
            this.UCWhereConditions1.SetControlPosition(1, 1, UCWhereConditions1.PanelMaterialMachineTypeWhere.ID);
            this.UCWhereConditions1.SetControlPosition(1, 2, UCWhereConditions1.PanelMOBOMVersionWhere.ID);

            this.UCWhereConditions1.SetControlPosition(2, 0, UCWhereConditions1.PanelBigSSCodeWhere.ID); 
            this.UCWhereConditions1.SetControlPosition(2, 1, UCWhereConditions1.PanelShiftCodeWhere.ID);
            this.UCWhereConditions1.SetControlPosition(2, 2, UCWhereConditions1.PanelCrewCodeWhere.ID);

            this.UCWhereConditions1.SetControlPosition(3, 0, UCWhereConditions1.PanelStartDateWhere.ID);
            this.UCWhereConditions1.SetControlPosition(3, 1, UCWhereConditions1.PanelEndDateWhere.ID);

            this.UCWhereConditions1.SetControlPosition(4, 0, UCWhereConditions1.PanelFirstClassWhere.ID);
            this.UCWhereConditions1.SetControlPosition(4, 1, UCWhereConditions1.PanelSecondClassWhere.ID);
            this.UCWhereConditions1.SetControlPosition(4, 2, UCWhereConditions1.PanelThirdClassWhere.ID);

            this.UCWhereConditions1.SetControlPosition(5, 0, UCWhereConditions1.PanelMOMemoWhere.ID);
            this.UCWhereConditions1.SetControlPosition(5, 1, UCWhereConditions1.PanelNewMassWhere.ID);
            this.UCWhereConditions1.SetControlPosition(5, 2, UCWhereConditions1.PanelMOTypeWhere.ID);

            this.UCWhereConditions1.SetControlPosition(6, 0, UCWhereConditions1.PanelErrorCauseGroupCodeWhere.ID);

            this.UCWhereConditions1.ManualTable = "tblecsg";
        }

        private void InitGroupControls()
        {
            this.UCGroupConditions1.ShowShiftDay = true;
            this.UCGroupConditions1.ShowWeek = true;
            this.UCGroupConditions1.ShowMonth = true;
            this.UCGroupConditions1.ShowYear = true;

            this.UCGroupConditions1.ShowByTimePanel = true;
            this.UCGroupConditions1.ShowCompareTypePanel = true;
            this.UCGroupConditions1.ShowCompleteTypePanel = true;

            this.UCGroupConditions1.ShowBigSSCode = true;

            this.UCGroupConditions1.ShowGoodSemiGood = true;
            this.UCGroupConditions1.ShowItemCode = true;
            this.UCGroupConditions1.ShowMaterialModelCode = true;
            this.UCGroupConditions1.ShowMaterialMachineType = true;
            this.UCGroupConditions1.ShowMaterialExportImport = true;

            this.UCGroupConditions1.ShowMOCode = true;
            this.UCGroupConditions1.ShowMOMemo = true;
            this.UCGroupConditions1.ShowNewMass = true;
            this.UCGroupConditions1.ShowCrewCode = true;

            this.UCGroupConditions1.ShowFirstClass = true;
            this.UCGroupConditions1.ShowSecondClass = true;
            this.UCGroupConditions1.ShowThirdClass = true;

            this.UCGroupConditions1.ShowSp1 = true;
        }

        private void InitResultControls()
        {
            //this.gridWebGrid.Visible = false;
            this.cmdGridExport.Visible = false;
            this.columnChart.Visible = false;
            this.lineChart.Visible = false;
        }

        #endregion

        #region 公用属性和事件处理

        private void LoadDisplayControls()
        {
            if (!this.IsPostBack)
            {
                List<ListItem> displayList = new List<ListItem>();
                displayList.Add(new ListItem(this.languageComponent1.GetString(NewReportDisplayType.Grid), NewReportDisplayType.Grid));
                displayList.Add(new ListItem(this.languageComponent1.GetString(NewReportDisplayType.LineChart), NewReportDisplayType.LineChart));
                displayList.Add(new ListItem(this.languageComponent1.GetString(NewReportDisplayType.HistogramChart), NewReportDisplayType.HistogramChart));
                this.UCDisplayConditions1.DisplayList = displayList;

                if (this.Request.Params["Width"] != null)
                {
                    ViewState["Width"] = this.Request.Params["Width"];
                }

                if (this.Request.Params["Height"] != null)
                {
                    ViewState["Height"] = this.Request.Params["Height"];
                }
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
            DoQuery();
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

            string groupFieldsX = this.UCGroupConditions1.GetGroupFieldAliasList(preferredTable, "X");
            string groupFieldsY = this.UCGroupConditions1.GetGroupFieldAliasList(preferredTable, "Y");

            //用于环比同期比时的修改时间过滤条件
            int dateAdjust = 0;
            if (string.Compare(compareType, NewReportCompareType.LastYear, true) == 0)
            {
                dateAdjust = -12;
            }
            else if (string.Compare(compareType, NewReportCompareType.Previous, true) == 0)
            {
                dateAdjust = -1;
            }

            ReportSQLEngine engine = new ReportSQLEngine(this.DataProvider, this.languageComponent1);
            engine.DetailedCoreTable = GetCoreTableDetail(isForCompare, roundDate);
            engine.Formular = GetFormular(inputOutput, compareType, completeType, bigSSChecked, opResChecked, segSSChecked);
            engine.WhereCondition = string.Empty;
            engine.HavingCondition = GetHavingCondition(inputOutput, compareType, completeType, bigSSChecked, opResChecked, segSSChecked);
            engine.GroupFieldsX = groupFieldsX;
            engine.GroupFieldsY = groupFieldsY;

            return engine.GetReportDataSource(byTimeType, dateAdjust); ;
        }

        private string GetCoreTableDetailForError()
        {
            string returnValue = string.Empty;
            string errorCauseGroupCode = this.UCWhereConditions1.UserSelectErrorCauseGroupCode.ToUpper();

            returnValue += "SELECT tblts.mocode, tblts.shiftday, tblts.itemcode, " + "\r\n"; 
            returnValue += "tblts.modelcode, tblts.shifttypecode, tblts.shiftcode, tblts.tpcode, " + "\r\n";
            returnValue += "tblts.frmsegcode AS segcode, tblts.frmsscode AS sscode, tblts.frmrescode AS rescode,tblts.frmopcode AS opcode, " + "\r\n";
            returnValue += "tblss.bigsscode, tblts.rcard " + "\r\n"; 
            returnValue += "FROM tblts " + "\r\n";
            returnValue += "LEFT OUTER JOIN tblss " + "\r\n";
            returnValue += "ON tblts.frmsscode = tblss.sscode " + "\r\n";
            returnValue += "LEFT OUTER JOIN tbltserrorcause " + "\r\n";
            returnValue += "ON tblts.tsid = tbltserrorcause.tsid " + "\r\n";
            returnValue += "WHERE tblts.cardtype = 'cardtype_product' " + "\r\n";
            if (errorCauseGroupCode.Trim().Length > 0)
            {
                if (errorCauseGroupCode.IndexOf(",") >= 0)
                {
                    errorCauseGroupCode = errorCauseGroupCode.Replace("'", "''");
                    errorCauseGroupCode = FormatHelper.ProcessQueryValues(errorCauseGroupCode);
                    returnValue += "AND tbltserrorcause.ecsgcode IN (" + errorCauseGroupCode + ") " + "\r\n";
                }
                else
                {
                    returnValue += "AND tbltserrorcause.ecsgcode LIKE '%" + errorCauseGroupCode + "%' " + "\r\n";
                }
            }           

            return returnValue;
        }

        private string GetCoreTableDetail(bool isForCompare, bool roundDate)
        {
            string returnValue = string.Empty;

            string inputOutput = UCWhereConditions1.UserSelectInputOutput;
            string byTimeType = UCGroupConditions1.UserSelectByTimeType;
            string compareType = UCGroupConditions1.UserSelectCompareType;
            string completeType = UCGroupConditions1.UserSelectCompleteType;

            if (!isForCompare)
            {
                compareType = string.Empty;
            }

            //用于环比同期比时的修改时间过滤条件
            int dateAdjust = 0;
            if (string.Compare(compareType, NewReportCompareType.LastYear, true) == 0)
            {
                dateAdjust = -12;
            }
            else if (string.Compare(compareType, NewReportCompareType.Previous, true) == 0)
            {
                dateAdjust = -1;
            }

            bool bigSSChecked = UCGroupConditions1.BigSSChecked;
            bool opResChecked = UCGroupConditions1.OPChecked || UCGroupConditions1.ResChecked;
            bool segSSChecked = UCGroupConditions1.SegChecked || UCGroupConditions1.SSChecked;

            string groupFieldsXForError = this.UCGroupConditions1.GetGroupFieldList(preferredTableForError, "X");
            string groupFieldsYForError = this.UCGroupConditions1.GetGroupFieldList(preferredTableForError, "Y");

            ReportSQLEngine engineForError = new ReportSQLEngine(this.DataProvider, this.languageComponent1);
            engineForError.CoreTableAlias = GetCoreTableAliasForError();
            engineForError.DetailedCoreTable = GetCoreTableDetailForError();
            engineForError.Formular = GetFormularForError(inputOutput, compareType, completeType, bigSSChecked, opResChecked, segSSChecked);
            engineForError.WhereCondition = this.UCWhereConditions1.GetWhereSQLStatement(preferredTableForError, byTimeType, roundDate, dateAdjust);
            engineForError.GroupFieldsX = groupFieldsXForError;
            engineForError.GroupFieldsY = groupFieldsYForError;

            string errorSQL = engineForError.GetReportSQL();

            string groupFieldsXForOutput = this.UCGroupConditions1.GetGroupFieldList(preferredTableForOutput, "X");
            string groupFieldsYForOutput = this.UCGroupConditions1.GetGroupFieldList(preferredTableForOutput, "Y");

            ReportSQLEngine engineForOutput = new ReportSQLEngine(this.DataProvider, this.languageComponent1);
            engineForOutput.CoreTableAlias = GetCoreTableAliasForOutput();
            engineForOutput.Formular = GetFormularForOutput(inputOutput, compareType, completeType, bigSSChecked, opResChecked, segSSChecked);
            engineForOutput.WhereCondition = this.UCWhereConditions1.GetWhereSQLStatement(preferredTableForOutput, byTimeType, roundDate, dateAdjust);
            engineForOutput.GroupFieldsX = groupFieldsXForOutput;
            engineForOutput.GroupFieldsY = groupFieldsYForOutput;

            string outputSQL = engineForOutput.GetReportSQL();

            //抓到SQL后,把错误的地方替换掉
            string replaceWhere = "AND tblline2crew.sscode = tblrptsoqty.sscode AND tblline2crew.shiftcode = tblrptsoqty.shiftcode";
            string replaceAt = "";
            if (outputSQL.Contains(replaceWhere))
            {
                replaceAt = " left outer join tblmesentitylist tblmesentitylist on tblmesentitylist.serial = tblrptsoqty.tblmesentitylist_serial "
                            + "AND tblline2crew.shiftcode = tblmesentitylist.shiftcode AND tblline2crew.sscode = tblmesentitylist.sscode ";
                outputSQL = outputSQL.Replace(replaceWhere, replaceAt);
            }
            if (outputSQL.Contains("tblitemclass tblitemclass ON tblitemclass.firstclass = tblrptsoqty.firstclass"))
            {
                replaceWhere = "tblitemclass tblitemclass ON tblitemclass.firstclass = tblrptsoqty.firstclass";
                replaceAt = " tblmaterial tblmaterial ON tblmaterial.mcode = tblrptsoqty.itemcode "
                            + "LEFT OUTER JOIN tblitemclass tblitemclass ON tblitemclass.itemgroup = tblmaterial.mgroup ";
                outputSQL = outputSQL.Replace(replaceWhere, replaceAt);
            }


            string groupFieldAliasX = this.UCGroupConditions1.GetGroupFieldAliasList(preferredTable, "X");
            string groupFieldAliasY = this.UCGroupConditions1.GetGroupFieldAliasList(preferredTable, "Y");

            returnValue += "SELECT " + GetFieldWithTableName("a", groupFieldAliasX, groupFieldAliasY) + ",output,errorcount " + "\r\n";
            returnValue += "FROM " + "\r\n";
            returnValue += "(" + outputSQL + ") a " + "\r\n";
            returnValue += "LEFT OUTER JOIN (" + errorSQL + ") b " + "\r\n";
            returnValue += GetJoinOnSQL("a", "b", groupFieldAliasX, groupFieldAliasY);

            return returnValue;
        }

        private string GetCoreTableAliasForError()
        {
            string returnValue = string.Empty;

            returnValue = "ct_error";

            return returnValue;
        }

        private string GetCoreTableAliasForOutput()
        {
            string returnValue = string.Empty;

            returnValue = "tblrptsoqty";

            return returnValue;
        }

        private string GetFormularForError(string inputOutput, string compareType, string completeType, bool bigSSChecked, bool opResChecked, bool segSSChecked)
        {
            string returnValue = string.Empty;

            returnValue = "COUNT(DISTINCT **.mocode || '///' || **.rcard) AS errorcount";

            return returnValue;
        }

        private string GetFormularForOutput(string inputOutput, string compareType, string completeType, bool bigSSChecked, bool opResChecked, bool segSSChecked)
        {
            //目前页面上只有大线

            string returnValue = string.Empty;

            if (completeType.Trim().ToLower() == NewReportCompleteType.Offline)
            {
                returnValue = "SUM(**.molineoutputcount) AS output";
            }
            else if (completeType.Trim().ToLower() == NewReportCompleteType.Complete)
            {
                returnValue = "SUM(**.mooutputcount) AS output";
            }

            //EATTRIBUTE1用于标记投入/产出+比较类型
            if (compareType.Trim().Length > 0)
            {
                if (returnValue.Trim().Length > 0)
                {
                    returnValue += ",";
                }
                returnValue += "'" + compareType.Trim() + "'" + " AS EATTRIBUTE1 ";
            }

            return returnValue;
        }

        private string GetFormular(string inputOutput, string compareType, string completeType, bool bigSSChecked, bool opResChecked, bool segSSChecked)
        {
            string returnValue = string.Empty;
            string errorForm = string.Empty;
            string outputForm = string.Empty;
            string YRForm = string.Empty;

            outputForm = "NVL(SUM(**.output), 0) AS output";
            errorForm = "NVL(SUM(**.errorcount), 0) AS errorcount";
            YRForm = "(CASE ";
            YRForm += "WHEN NVL(SUM(**.errorcount), 0) > NVL(SUM(**.output), 0) THEN 0 ";
            YRForm += "WHEN NVL(SUM(**.output), 0) = 0 THEN 1 ";            
            YRForm += "ELSE 1 - NVL(SUM(**.errorcount), 0) / NVL(SUM(**.output), 0) END) as FirstPassYieldByECSG";
            
            returnValue = errorForm + "," + outputForm + "," + YRForm;

            //EATTRIBUTE1用于标记投入/产出+比较类型
            if (compareType.Trim().Length > 0)
            {
                if (returnValue.Trim().Length > 0)
                {
                    returnValue += ",";
                }
                returnValue += "'" + compareType.Trim() + "'" + " AS EATTRIBUTE1 ";
            }
            return returnValue;
        }

        private string GetHavingCondition(string inputOutput, string compareType, string completeType, bool bigSSChecked, bool opResChecked, bool segSSChecked)
        {
            string returnValue = string.Empty;
            string errorForm = string.Empty;
            string outputForm = string.Empty;
            string YRForm = string.Empty;

            outputForm = "NVL(SUM(**.output), 0) > 0";
            errorForm = "NVL(SUM(**.errorcount), 0) > 0";

            returnValue = errorForm + " OR " + outputForm;

            return returnValue;
        }

        private string GetJoinOnSQL(string tableA, string tableB, string groupFieldAliasX, string groupFieldAliasY)
        {
            string returnValue = string.Empty;
            string groupField = string.Empty;
            groupFieldAliasX = groupFieldAliasX.Trim();
            groupFieldAliasY = groupFieldAliasY.Trim();

            groupField += groupFieldAliasX;
            if (groupField.Length > 0 && groupFieldAliasY.Length > 0)
            {
                groupField += "," + groupFieldAliasY;
            }

            if (groupField.Length > 0)
            {
                string[] valueList = groupField.Split(',');
                for (int i = 0; i < valueList.Length; i++)
                {
                    if (returnValue.Trim().Length > 0)
                    {
                        returnValue += " AND ";
                    }
                    returnValue += tableA + "." + valueList[i].Trim() + " = " + tableB + "." + valueList[i].Trim() + "\r\n";
                }
                returnValue = "ON " + returnValue;
            }

            return returnValue;
        }

        private string GetFieldWithTableName(string table, string groupFieldAliasX, string groupFieldAliasY)
        {
            string returnValue = string.Empty;
            string groupField = string.Empty;
            groupFieldAliasX = groupFieldAliasX.Trim();
            groupFieldAliasY = groupFieldAliasY.Trim();

            groupField += groupFieldAliasX;
            if (groupField.Length > 0 && groupFieldAliasY.Length > 0)
            {
                groupField += "," + groupFieldAliasY;
            }

            if ((groupField.IndexOf("dweek") >= 0 || groupField.IndexOf("dmonth") >= 0) 
                && groupField.IndexOf(" year") < 0)
            {
                groupField += ",year";
            }

            if (groupField.Length > 0)
            {
                string[] valueList = groupField.Split(',');
                for (int i = 0; i < valueList.Length; i++)
                {
                    if (returnValue.Trim().Length > 0)
                    {
                        returnValue += ",";
                    }
                    returnValue += table + "." + valueList[i].Trim();
                }
            }

            return returnValue;
        }

        #endregion

        #region 相关的函数

        private string[] GetOWCSchema()
        {
            string[] rows = GetRows().ToArray();
            string[] columns = GetColumns().ToArray();

            ArrayList schemaList = new ArrayList();
            foreach (string row in rows)
            {
                schemaList.Add(row);
            }
            foreach (string column in columns)
            {
                schemaList.Add(column);
            }

            schemaList.Add("Input");
            schemaList.Add("Output");
            schemaList.Add("EAttribute1");


            return (string[])schemaList.ToArray(typeof(string));
        }

        private List<string> GetColumns()
        {
            List<string> returnValue = new List<string>();

            string rowString = UCGroupConditions1.GetGroupFieldAliasList(preferredTable, "X");

            if (rowString.Trim().Length > 0)
            {

                returnValue.AddRange(rowString.Split(','));

                for (int i = 0; i < returnValue.Count; i++)
                {
                    returnValue[i] = DomainObjectUtility.GetPropertyNameByFieldName(typeof(NewReportDomainObject), returnValue[i]);
                }
            }

            return returnValue;
        }

        private List<string> GetRows()
        {
            List<string> returnValue = new List<string>();

            string columnString = UCGroupConditions1.GetGroupFieldAliasList(preferredTable, "Y");

            if (columnString.Trim().Length > 0)
            {

                returnValue.AddRange(columnString.Split(','));

                for (int i = 0; i < returnValue.Count; i++)
                {
                    returnValue[i] = DomainObjectUtility.GetPropertyNameByFieldName(typeof(NewReportDomainObject), returnValue[i]);
                }
            }

            return returnValue;
        }

        #endregion

        private bool CheckBeforeQuery()
        {
            ////过滤条件中选择OP时，汇总条件中必须选择Res或者OP
            //if (UCWhereConditions1.UserSelectOP.Trim().Length > 0
            //    && !UCGroupConditions1.OPChecked
            //    && !UCGroupConditions1.ResChecked)
            //{
            //    WebInfoPublish.Publish(this, "$Report_GroupByOPOrResMustBeChecked", this.languageComponent1);
            //    return false;
            //}

            //同期比只能用于月
            if (UCGroupConditions1.UserSelectCompareType == NewReportCompareType.LastYear
                && UCGroupConditions1.UserSelectByTimeType != NewReportByTimeType.Month)
            {
                WebInfoPublish.Publish(this, "$Report_LastYearOnlyForMonth", this.languageComponent1);
                return false;
            }

            //环比只能用于周、月、年
            if (UCGroupConditions1.UserSelectCompareType == NewReportCompareType.Previous
                && UCGroupConditions1.UserSelectByTimeType != NewReportByTimeType.Week
                && UCGroupConditions1.UserSelectByTimeType != NewReportByTimeType.Month
                && UCGroupConditions1.UserSelectByTimeType != NewReportByTimeType.Year)
            {
                WebInfoPublish.Publish(this, "$Report_PreviousOnlyForWeekMonthYear", this.languageComponent1);
                return false;
            }

            return true;
        }

        protected override void DoQuery()
        {
            base.DoQuery();

            if (this.CheckBeforeQuery())
            {
                this.AutoRefresh = this.chbRefreshAuto.Checked;

                string compareType = this.UCGroupConditions1.UserSelectCompareType.Trim().ToLower();
                string byTimeType = this.UCGroupConditions1.UserSelectByTimeType.Trim().ToLower();
                string inputOutput = this.UCWhereConditions1.UserSelectInputOutput.Trim().ToLower();
                object[] dateSource = null;
                object[] dateSourceCompare = null;



                //一般数据
                dateSource = this.LoadDataSource(false, compareType.Trim().Length > 0);

                //((NewReportDomainObject)dateSource[0]).YR=((NewReportDomainObject)dateSource[0]).YR  *100%;

                if (dateSource == null || dateSource.Length <= 0)
                {
                    this.gridWebGrid.Visible = false;
                    this.cmdGridExport.Visible = false;
                    this.columnChart.Visible = false;
                    this.lineChart.Visible = false;

                    ReportPageHelper.SetPageScrollToBottom(this);
                    return;
                }

                //环比/同期比数据
                if (compareType.Trim().Length > 0)
                {
                    dateSourceCompare = this.LoadDataSource(true, true);
                }
                if (dateSourceCompare == null)
                {
                    dateSourceCompare = new NewReportDomainObject[0] { };
                }

                //数据加载到Grid
                List<string> fixedColumnList = GetRows();
                List<ReportGridDim3Property> dim3PropertyList = new List<ReportGridDim3Property>();

                dim3PropertyList.Add(new ReportGridDim3Property("Output", "0", "SUM", "SUM", false));
                dim3PropertyList.Add(new ReportGridDim3Property("ErrorCount", "0", "SUM", "SUM", false));
                dim3PropertyList.Add(new ReportGridDim3Property("FirstPassYieldByECSG", "0.00%", "DIVS({-1},{-2})", "DIVS({-1},{-2})", false));

                List<string> dim3DefaultValueList = new List<string>();
                dim3DefaultValueList.Add("0");
                dim3DefaultValueList.Add("0");
                dim3DefaultValueList.Add("1");

                ReportGridHelperNew reportGridHelper = new ReportGridHelperNew(this.DataProvider, this.languageComponent1, this.gridWebGrid,this.DtSource);
                reportGridHelper.DataSource = dateSource;
                reportGridHelper.DataSourceForCompare = dateSourceCompare;
                reportGridHelper.Dim1PropertyList = fixedColumnList;
                reportGridHelper.Dim3PropertyList = dim3PropertyList;
                reportGridHelper.Dim3DefaultValueList = dim3DefaultValueList;
                reportGridHelper.HasDim3PropertyNameRowColumn = true;
                reportGridHelper.CompareType = compareType;
                reportGridHelper.ByTimeType = byTimeType;

                reportGridHelper.ShowGrid();
                base.InitWebGrid();

                this.gridWebGrid.Behaviors.RowSelectors.RowNumbering = false;
                this.gridWebGrid.Behaviors.Sorting.Enabled = false;

                //获取表格和图示
                if (UCDisplayConditions1.GetDisplayType().Trim().ToLower() == NewReportDisplayType.Grid)
                {
                    this.gridWebGrid.Visible = true;
                    this.cmdGridExport.Visible = true;
                    this.columnChart.Visible = false;
                    this.lineChart.Visible = false;
                }
                else if (UCDisplayConditions1.GetDisplayType().Trim().ToLower() == NewReportDisplayType.LineChart
                    || UCDisplayConditions1.GetDisplayType().Trim().ToLower() == NewReportDisplayType.PieChart
                    || UCDisplayConditions1.GetDisplayType().Trim().ToLower() == NewReportDisplayType.HistogramChart)
                {
                    List<string> rowPropertyList = GetColumns();
                    List<string> columnPropertyList = GetRows();
                    columnPropertyList.Add("EAttribute1");
                    List<string> dataPropertyList = new List<string>();
                    dataPropertyList.Add("FirstPassYieldByECSG");

                    NewReportDomainObject[] dateSourceForOWC = new NewReportDomainObject[dateSource.Length + dateSourceCompare.Length];
                    dateSource.CopyTo(dateSourceForOWC, 0);
                    for (int i = 0; i < dateSourceCompare.Length; i++)
                    {
                        dateSourceForOWC[dateSource.Length + i] = (NewReportDomainObject)dateSourceCompare[i];
                    }
                    string propertyName = this.languageComponent1.GetString(dataPropertyList[0]);
                    foreach (NewReportDomainObject domainObject in dateSourceForOWC)
                    {
                        domainObject.EAttribute1 = propertyName + domainObject.EAttribute1;
                    }

                    //add by seven 20110110
                    foreach (NewReportDomainObject obj in dateSourceForOWC)
                    {
                        obj.TempValue = obj.FirstPassYieldByECSG.ToString();

                        //天、周、月、年
                        if (UCGroupConditions1.UserSelectByTimeType == NewReportByTimeType.ShiftDay)
                        {
                            obj.PeriodCode = obj.ShiftDay.ToString();
                        }

                        if (UCGroupConditions1.UserSelectByTimeType == NewReportByTimeType.Week)
                        {
                            obj.PeriodCode = obj.Week.ToString();
                        }

                        if (UCGroupConditions1.UserSelectByTimeType == NewReportByTimeType.Month)
                        {
                            obj.PeriodCode = obj.Month.ToString();
                        }

                        if (UCGroupConditions1.UserSelectByTimeType == NewReportByTimeType.Year)
                        {
                            obj.PeriodCode = obj.Year.ToString();
                        }
                    }

                    if (UCDisplayConditions1.GetDisplayType().Trim().ToLower() == NewReportDisplayType.LineChart)
                    {
                        this.columnChart.Visible = false;
                        this.lineChart.Visible = true;

                        lineChart.ChartGroupByString = UCGroupConditions1.GetCheckedColumnsString();

                        //设置首页报表的大小
                        if (ViewState["Width"] != null)
                        {
                            lineChart.Width = int.Parse(ViewState["Width"].ToString());
                        }

                        if (ViewState["Height"] != null)
                        {
                            lineChart.Height = int.Parse(ViewState["Height"].ToString());
                        }
                        //end

                        this.lineChart.ChartTextFormatString = "<DATA_VALUE:00.00%>";
                        this.lineChart.YLabelFormatString = "<DATA_VALUE:00.00%>";
                        this.lineChart.DataType = true;
                        this.lineChart.DataSource = dateSourceForOWC;
                        this.lineChart.DataBind();
                    }
                    else if (UCDisplayConditions1.GetDisplayType().Trim().ToLower() == NewReportDisplayType.HistogramChart)
                    {
                        this.columnChart.Visible = true;
                        this.lineChart.Visible = false;

                        columnChart.ChartGroupByString = UCGroupConditions1.GetCheckedColumnsString();

                        //设置首页报表的大小
                        if (ViewState["Width"] != null)
                        {
                            columnChart.Width = int.Parse(ViewState["Width"].ToString());
                        }

                        if (ViewState["Height"] != null)
                        {
                            columnChart.Height = int.Parse(ViewState["Height"].ToString());
                        }
                        //end

                        this.columnChart.ChartTextFormatString = "<DATA_VALUE:#0.##%>";
                        this.columnChart.YLabelFormatString = "<DATA_VALUE:#0.##%>";
                        this.columnChart.DataType = true;
                        this.columnChart.DataSource = dateSourceForOWC;
                        this.columnChart.DataBind();
                    }
                    else
                    {
                        this.columnChart.Visible = false;
                        this.lineChart.Visible = false;
                    }
                    //end

                    this.gridWebGrid.Visible = false;
                    this.cmdGridExport.Visible = false;
                }

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
