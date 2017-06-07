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
    public partial class FNewReportModelFirstPassYieldQP : BaseQPageNew
    {
        #region 页面初始化

        private System.ComponentModel.IContainer components;
        //protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;

        private const string preferredTable = "tblmaterial,tblitemclass,tblmo,tbltimedimension,tblsysparam,tblres,tblline2crew,**";

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
            this.UCWhereConditions1.SetControlPosition(0, 2, UCWhereConditions1.PanelMOCodeWhere.ID);

            this.UCWhereConditions1.SetControlPosition(1, 0, UCWhereConditions1.PanelMOBOMVersionWhere.ID);
            this.UCWhereConditions1.SetControlPosition(1, 1, UCWhereConditions1.PanelBigSSCodeWhere.ID);
            this.UCWhereConditions1.SetControlPosition(1, 2, UCWhereConditions1.PanelSegCodeWhere.ID);

            this.UCWhereConditions1.SetControlPosition(2, 0, UCWhereConditions1.PanelFirstClassWhere.ID);
            this.UCWhereConditions1.SetControlPosition(2, 1, UCWhereConditions1.PanelSecondClassWhere.ID);
            this.UCWhereConditions1.SetControlPosition(2, 2, UCWhereConditions1.PanelThirdClassWhere.ID);

            this.UCWhereConditions1.SetControlPosition(3, 0, UCWhereConditions1.PanelStartDateWhere.ID);
            this.UCWhereConditions1.SetControlPosition(3, 1, UCWhereConditions1.PanelEndDateWhere.ID);

            this.UCWhereConditions1.SetControlPosition(4, 0, UCWhereConditions1.PanelShiftCodeWhere.ID);
            this.UCWhereConditions1.SetControlPosition(4, 1, UCWhereConditions1.PanelCrewCodeWhere.ID);
            this.UCWhereConditions1.SetControlPosition(4, 2, UCWhereConditions1.PanelMOTypeWhere.ID);
        }

        private void InitGroupControls()
        {
            this.UCGroupConditions1.ShowShiftDay = true;
            this.UCGroupConditions1.ShowWeek = true;
            this.UCGroupConditions1.ShowMonth = true;
            this.UCGroupConditions1.ShowYear = true;

            this.UCGroupConditions1.ShowByTimePanel = true;

            this.UCGroupConditions1.ShowBigSSCode = true;
            this.UCGroupConditions1.ShowSegCode = true;

            this.UCGroupConditions1.ShowItemCode = true;
            this.UCGroupConditions1.ShowMOCode = true;
            this.UCGroupConditions1.ShowCrewCode = true;

            this.UCGroupConditions1.ShowFirstClass = true;
            this.UCGroupConditions1.ShowSecondClass = true;
            this.UCGroupConditions1.ShowThirdClass = true;

            this.UCGroupConditions1.ShowSp1 = true;
            this.UCGroupConditions1.ShowSp2 = true;
        }

        private void InitResultControls()
        {
            //this.gridWebGrid.Visible = false;
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
            this.DoQuery();
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

            groupFieldsY = OrderFieldsY(groupFieldsY);

            if (string.IsNullOrEmpty(groupFieldsX))
            {
                groupFieldsX += "**.OPCODE || '-' || tblop.opdesc as opcode";
            }
            else
            {
                groupFieldsX += ",**.OPCODE || '-' || tblop.opdesc as opcode";
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

            ReportSQLEngine engine = new ReportSQLEngine(this.DataProvider, this.languageComponent1);
            engine.DetailedCoreTable = GetCoreTableDetail();
            engine.Formular = GetFormular(inputOutput, compareType, completeType, bigSSChecked, opResChecked, segSSChecked);
            engine.WhereCondition = this.UCWhereConditions1.GetWhereSQLStatement(preferredTable, byTimeType, roundDate, dateAdjust);

            //if (engine.WhereCondition.IndexOf("itemtype_finishedproduct") >= 0)
            //{
            //    engine.WhereCondition = engine.WhereCondition.Replace("itemtype_finishedproduct", "FINISHEDSTR");
            //}

            //if (engine.WhereCondition.IndexOf("itemtype_semimanufacture") >= 0)
            //{
            //    engine.WhereCondition = engine.WhereCondition.Replace("itemtype_semimanufacture", "SEMIFINISHEDSTR");
            //}
            engine.GroupFieldsX = groupFieldsX;
            engine.GroupFieldsY = groupFieldsY;

            return engine.GetReportDataSource(byTimeType, dateAdjust); ;
        }

        private string GetCoreTableDetail()
        {
            string returnValue = string.Empty;


            returnValue += "SELECT {0}, {1},TBLSYSPARAM.paramgroupcode AS mtype " + "\r\n";
            returnValue += "FROM tblrptsoqty " + "\r\n";
            returnValue += "INNER JOIN tblmesentitylist " + "\r\n";
            returnValue += "ON tblrptsoqty.tblmesentitylist_serial = tblmesentitylist.serial " + "\r\n";
            returnValue += " INNER JOIN TBLSYSPARAM " + "\r\n";
            returnValue += "on TBLSYSPARAM.Paramalias=TBLMESENTITYLIST.Opcode " + "\r\n";

            if (UCWhereConditions1.UserSelectGoodSemiGood.Trim() == ItemType.ITEMTYPE_FINISHEDPRODUCT)
            {
                returnValue += " AND TBLSYSPARAM.PARAMGROUPCODE='FINISHEDSTR' " + "\r\n";
            }

            if (UCWhereConditions1.UserSelectGoodSemiGood.Trim() == ItemType.ITEMTYPE_SEMIMANUFACTURE)
            {
                returnValue += " AND TBLSYSPARAM.PARAMGROUPCODE='SEMIFINISHEDSTR' " + "\r\n";
            }

            returnValue = string.Format(returnValue,
                    DomainObjectUtility.GetDomainObjectFieldsStringWithTableName(typeof(BenQGuru.eMES.Domain.Report.ReportSOQty)).ToLower(),
                    DomainObjectUtility.GetDomainObjectFieldsStringWithTableName(typeof(BenQGuru.eMES.Domain.Report.MESEntityList)).ToLower().Replace(", tblmesentitylist.eattribute1", ""));

            return returnValue;
        }

        private string GetFormular(string inputOutput, string compareType, string completeType, bool bigSSChecked, bool opResChecked, bool segSSChecked)
        {
            string returnValue = string.Empty;
            string YRForm = string.Empty;

            YRForm = " decode(SUM (CT.opcount),0,0,SUM (CT.opwhitecardcount)/SUM (CT.opcount)) AS PassRcardRate ";


            returnValue = YRForm;
            return returnValue;
        }

        #endregion

        #region 相关的函数



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
            if (UCWhereConditions1.UserSelectGoodSemiGood.Trim().Length <= 0)
            {
                WebInfoPublish.Publish(this, "$Report_Must_Select_GoodSemiGood", this.languageComponent1);
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

                //获得查询的工序
                string parameterGroupCode = string.Empty;
                if (UCWhereConditions1.UserSelectGoodSemiGood.Trim() == ItemType.ITEMTYPE_FINISHEDPRODUCT)
                {
                    parameterGroupCode = "FINISHEDSTR";
                }

                if (UCWhereConditions1.UserSelectGoodSemiGood.Trim() == ItemType.ITEMTYPE_SEMIMANUFACTURE)
                {
                    parameterGroupCode = "SEMIFINISHEDSTR";
                }

                SystemSettingFacade systemSettingFacade = new SystemSettingFacade(this.DataProvider);
                Object[] parameterList = systemSettingFacade.GetAllParametersOrderByEattribute1(parameterGroupCode);

                if (parameterList == null)
                {
                    WebInfoPublish.Publish(this.Page, "$BS_Pelease_Maintenance_QueryOP", this.languageComponent1);
                    return;
                }

                List<string> fixedHeadOPList = new List<string>();
                List<string> fixedHeadOPDescList = new List<string>();

                foreach (eMES.Domain.BaseSetting.Parameter parameter in parameterList)
                {
                    fixedHeadOPList.Add(parameter.ParameterAlias);
                    fixedHeadOPDescList.Add(parameter.ParameterDescription);
                }

                fixedHeadOPDescList.Add("直通率");



                //数据加载到Grid
                List<string> fixedColumnList = GetRows();
                fixedColumnList = OrderfixedColumnList(fixedColumnList);
                if (byTimeType==NewReportByTimeType.ShiftDay)
                {
                    fixedColumnList.Add("ShiftDay");
                }

                if (byTimeType==NewReportByTimeType.Week)
                {
                    fixedColumnList.Add("Week");
                }

                if (byTimeType == NewReportByTimeType.Month)
                {
                    fixedColumnList.Add("Month");
                }

                if (byTimeType == NewReportByTimeType.Year)
                {
                    fixedColumnList.Add("Year");
                }

                List<ReportGridDim3Property> dim3PropertyList = new List<ReportGridDim3Property>();

                ReportGridHelperNew reportGridHelper = new ReportGridHelperNew(this.DataProvider, this.languageComponent1, this.gridWebGrid,this.DtSource);
                reportGridHelper.DataSource = dateSource;
                reportGridHelper.DataSourceForCompare = dateSourceCompare;
                reportGridHelper.Dim1PropertyList = fixedColumnList;
                reportGridHelper.Dim3PropertyList = dim3PropertyList;
                reportGridHelper.FixedHeadDefaultValueList = fixedHeadOPList;
                reportGridHelper.FixedHeadDescDefaultValueList = fixedHeadOPDescList;
                reportGridHelper.HasDim3PropertyNameRowColumn = true;
                reportGridHelper.CompareType = compareType;
                reportGridHelper.ByTimeType = byTimeType;

                reportGridHelper.ShowGridWithFixedOPHead();
                base.InitWebGrid();

                this.gridWebGrid.Behaviors.RowSelectors.RowNumbering = false;
                this.gridWebGrid.Behaviors.Sorting.Enabled = false;

                //获取表格和图示
                if (UCDisplayConditions1.GetDisplayType().Trim().ToLower() == NewReportDisplayType.Grid)
                {
                    this.gridWebGrid.Visible = true;
                    this.cmdGridExport.Visible = true;
                }

                ReportPageHelper.SetPageScrollToBottom(this);
            }
            else
            {
                this.chbRefreshAuto.Checked = false;
                this.AutoRefresh = false;
            }
        }


        private string OrderFieldsY(string groupFieldsY)
        {
            string returnString = string.Empty;

            if (string.IsNullOrEmpty(groupFieldsY.Trim()))
            {
                return returnString;
            }

            if (groupFieldsY.IndexOf("**.segcode || ' - ' || tblseg.segdesc AS segcode") >= 0)
            {
                returnString += "**.segcode || ' - ' || tblseg.segdesc AS segcode,";
            }

            if (groupFieldsY.IndexOf("**.bigsscode AS bigsscode") >= 0)
            {
                returnString += "**.bigsscode AS bigsscode,";
            }

            if (groupFieldsY.IndexOf("tblline2crew.crewcode || ' - ' || tblcrew.crewdesc AS crewcode") >= 0)
            {
                returnString += "tblline2crew.crewcode || ' - ' || tblcrew.crewdesc AS crewcode,";
            }

            if (groupFieldsY.IndexOf("tblitemclass.firstclass AS firstclass") >= 0)
            {
                returnString += "tblitemclass.firstclass AS firstclass,";
            }

            if (groupFieldsY.IndexOf("tblitemclass.secondclass AS secondclass") >= 0)
            {
                returnString += "tblitemclass.secondclass AS secondclass,";
            }

            if (groupFieldsY.IndexOf("tblitemclass.thirdclass AS thirdclass") >= 0)
            {
                returnString += "tblitemclass.thirdclass AS thirdclass,";
            }

            if (groupFieldsY.IndexOf("**.itemcode || ' - ' || tblmaterial.mdesc AS itemcode") >= 0)
            {
                returnString += "**.itemcode || ' - ' || tblmaterial.mdesc AS itemcode,";
            }

            if (groupFieldsY.IndexOf("**.mocode AS mocode") >= 0)
            {
                returnString += "**.mocode AS mocode,";
                returnString += "tblmo.orderno AS orderno,";
                returnString += "tblmo.mobom AS mobom,";
            }

            returnString = returnString.Substring(0, returnString.Length - 1);

            return returnString;
        }

        private List<string> OrderfixedColumnList(List<string> fixedColumnList)
        {
            List<string> returnStringList = new List<string>();

            List<string> orderList = new List<string>();
            orderList.Add("SegCode");
            orderList.Add("BigSSCode");
            orderList.Add("CrewCode");
            orderList.Add("FirstClass");
            orderList.Add("SecondClass");
            orderList.Add("ThirdClass");
            orderList.Add("ItemCode");
            orderList.Add("MOCode");

            for (int i = 0; i < orderList.Count; i++)
			{
                if (fixedColumnList.Contains(orderList[i]))
                {
                    returnStringList.Add(orderList[i]);
                    fixedColumnList.Remove(orderList[i]);
                }
            }

            returnStringList.AddRange(fixedColumnList);

            return returnStringList;
        }

    }
}
