using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Domain.Material;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Domain.Performance;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Performance;
using BenQGuru.eMES.Web.Helper;
using ControlLibrary.Web.Language;
using Infragistics.Web.UI.GridControls;

namespace BenQGuru.eMES.Web.ImportData
{
    public class ImportDateEngine
    {
        #region 字段和属性

        private LanguageComponent _LanguageComponent1;
        private IDomainDataProvider _DataProvider = null;
        private MaterialFacade _MaterialFacade = null;
        private MOFacade _MOFacade = null;

        private string _ImportType = string.Empty;
        private DataTable _ImportDataTable = null;
        private string _UserCode = string.Empty;
        private ArrayList _ImportGridRow = null;
        private GridHelperNew gridHelper;
        private ArrayList _ErrorArray = new ArrayList();
        public ArrayList ErrorArray
        {
            get
            {
                return _ErrorArray;
            }
            set
            {
                _ErrorArray = value;
            }
        }

        #endregion

        public ImportDateEngine(IDomainDataProvider dataProvider, LanguageComponent languageComponent1, string importType, DataTable table, string userCode, ArrayList importGridRow, GridHelperNew _gridHelper)
        {
            _DataProvider = dataProvider;
            _LanguageComponent1 = languageComponent1;
            _ImportType = importType;
            _ImportDataTable = table;
            _UserCode = userCode;
            _ImportGridRow = importGridRow;
            this.gridHelper = _gridHelper;
        }

        #region 检查数据有效性

        public void CheckDataValid()
        {
            switch (this._ImportType.ToUpper())
            {
                case "WORKPLAN":
                    this.CheckWorkPlan();
                    break;
                case "MATERIALNEED":
                    this.CheckMaterialNeed();
                    break;
                case "PLANWORKTIME":
                    this.CheckPlanWorkTime();
                    break;
                default:
                    break;
            }
        }

        private void CheckWorkPlan()
        {
            int count = this._ImportDataTable.Rows.Count;
            Hashtable checkedRow = new Hashtable();
            ShiftModelFacade shiftModelFacade = new ShiftModelFacade(this._DataProvider);
            MaterialFacade materialFacade = new MaterialFacade(this._DataProvider);
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this._DataProvider);
            for (int i = count - 1; i >= 0; i--)
            {

                DataRow row = this._ImportDataTable.Rows[i];
                GridRecord gridRow = this._ImportGridRow[i] as GridRecord;
                string moCode = row["MOCODE"].ToString();
                string bigSSCode = row["BIGSSCODE"].ToString();
                string planDate = row["PLANDATE"].ToString();
                string planSeq = row["PLANSEQ"].ToString();
                string moSeq = row["MOSEQ"].ToString();
                string planStartTime = (Convert.ToDateTime(row["PLANSTARTTIME"])).ToString("HH:mm:ss");
                string planQty = row["PLANQTY"].ToString();


                if (string.IsNullOrEmpty(moCode))
                {
                    if (!checkedRow.ContainsKey(moCode))
                    {
                        checkedRow.Add(moCode, false);
                    }

                    gridRow.Items.FindItemByKey(gridHelper.CheckColumnKey).Value = false;
                    gridRow.Items.FindItemByKey("ImportResult").Text = _LanguageComponent1.GetString("$CS_MO_NotExit");
                    gridRow.Items.FindItemByKey("ImportResult").CssClass = "MsgYellow";
                    this._ImportDataTable.Rows.Remove(row);
                    this._ImportGridRow.Remove(gridRow);
                    continue;
                }

                if (string.IsNullOrEmpty(bigSSCode))
                {
                    if (!checkedRow.ContainsKey(bigSSCode))
                    {
                        checkedRow.Add(bigSSCode, false);
                    }

                    gridRow.Items.FindItemByKey(gridHelper.CheckColumnKey).Value = false;
                    gridRow.Items.FindItemByKey("ImportResult").Text = _LanguageComponent1.GetString("$BIGSSCODE_IS_NOT_EXIT");
                    gridRow.Items.FindItemByKey("ImportResult").CssClass = "MsgYellow";
                    this._ImportDataTable.Rows.Remove(row);
                    this._ImportGridRow.Remove(gridRow);

                    continue;
                }

                if (string.IsNullOrEmpty(planDate))
                {
                    if (!checkedRow.ContainsKey(planDate))
                    {
                        checkedRow.Add(planDate, false);
                    }

                    gridRow.Items.FindItemByKey(gridHelper.CheckColumnKey).Value = false;
                    gridRow.Items.FindItemByKey("ImportResult").Text = "计划日期不能为空";
                    gridRow.Items.FindItemByKey("ImportResult").CssClass = "MsgYellow";
                    this._ImportDataTable.Rows.Remove(row);
                    this._ImportGridRow.Remove(gridRow);

                    continue;
                }

                try
                {
                    DateTime planDateTime = Convert.ToDateTime(FormatHelper.ToDateString(int.Parse(planDate), "-"));
                }
                catch
                {
                    if (!checkedRow.ContainsKey(planDate))
                    {
                        checkedRow.Add(planDate, false);
                    }

                    gridRow.Items.FindItemByKey(gridHelper.CheckColumnKey).Value = false;
                    gridRow.Items.FindItemByKey("ImportResult").Text = "计划日期错误";
                    gridRow.Items.FindItemByKey("ImportResult").CssClass = "MsgYellow";
                    this._ImportDataTable.Rows.Remove(row);
                    this._ImportGridRow.Remove(gridRow);

                    continue;
                }

                if (string.IsNullOrEmpty(planSeq))
                {
                    if (!checkedRow.ContainsKey(planSeq))
                    {
                        checkedRow.Add(planSeq, false);
                    }

                    gridRow.Items.FindItemByKey(gridHelper.CheckColumnKey).Value = false;
                    gridRow.Items.FindItemByKey("ImportResult").Text = "生产顺序不能为空";
                    gridRow.Items.FindItemByKey("ImportResult").CssClass = "MsgYellow";
                    this._ImportDataTable.Rows.Remove(row);
                    this._ImportGridRow.Remove(gridRow);

                    continue;

                }

                if (string.IsNullOrEmpty(moSeq))
                {
                    if (!checkedRow.ContainsKey(moSeq))
                    {
                        checkedRow.Add(moSeq, false);
                    }

                    gridRow.Items.FindItemByKey(gridHelper.CheckColumnKey).Value = false;
                    gridRow.Items.FindItemByKey("ImportResult").Text = "工单项次不能为空";
                    gridRow.Items.FindItemByKey("ImportResult").CssClass = "MsgYellow";
                    this._ImportDataTable.Rows.Remove(row);
                    this._ImportGridRow.Remove(gridRow);

                    continue;
                }

                if (string.IsNullOrEmpty(planStartTime))
                {
                    if (!checkedRow.ContainsKey(planStartTime))
                    {
                        checkedRow.Add(planStartTime, false);
                    }

                    gridRow.Items.FindItemByKey(gridHelper.CheckColumnKey).Value = false;
                    gridRow.Items.FindItemByKey("ImportResult").Text = "计划开始时间不能为空";
                    gridRow.Items.FindItemByKey("ImportResult").CssClass = "MsgYellow";
                    this._ImportDataTable.Rows.Remove(row);
                    this._ImportGridRow.Remove(gridRow);

                    continue;
                }


                try
                {
                    int numplanSeq = int.Parse(planSeq);
                    if (numplanSeq < 0)
                    {
                        if (!checkedRow.ContainsKey(planSeq))
                        {
                            checkedRow.Add(planSeq, false);
                        }

                        gridRow.Items.FindItemByKey(gridHelper.CheckColumnKey).Value = false;
                        gridRow.Items.FindItemByKey("ImportResult").Text = "生产顺序不应小于0";
                        gridRow.Items.FindItemByKey("ImportResult").CssClass = "MsgYellow";
                        this._ImportDataTable.Rows.Remove(row);
                        this._ImportGridRow.Remove(gridRow);

                        continue;
                    }
                }
                catch
                {
                    if (!checkedRow.ContainsKey(planSeq))
                    {
                        checkedRow.Add(planSeq, false);
                    }

                    gridRow.Items.FindItemByKey(gridHelper.CheckColumnKey).Value = false;
                    gridRow.Items.FindItemByKey("ImportResult").Text = "生产顺序格式错误";
                    gridRow.Items.FindItemByKey("ImportResult").CssClass = "MsgYellow";
                    this._ImportDataTable.Rows.Remove(row);
                    this._ImportGridRow.Remove(gridRow);

                    continue;
                }

                try
                {
                    int nummoSeq = int.Parse(moSeq);
                    if (nummoSeq < 0)
                    {
                        if (!checkedRow.ContainsKey(moSeq))
                        {
                            checkedRow.Add(moSeq, false);
                        }

                        gridRow.Items.FindItemByKey(gridHelper.CheckColumnKey).Value = false;
                        gridRow.Items.FindItemByKey("ImportResult").Text = "工单项次不应小于0";
                        gridRow.Items.FindItemByKey("ImportResult").CssClass = "MsgYellow";
                        this._ImportDataTable.Rows.Remove(row);
                        this._ImportGridRow.Remove(gridRow);

                        continue;
                    }
                }
                catch
                {
                    if (!checkedRow.ContainsKey(moSeq))
                    {
                        checkedRow.Add(moSeq, false);
                    }

                    gridRow.Items.FindItemByKey(gridHelper.CheckColumnKey).Value = false;
                    gridRow.Items.FindItemByKey("ImportResult").Text = "工单项次格式错误";
                    gridRow.Items.FindItemByKey("ImportResult").CssClass = "MsgYellow";
                    this._ImportDataTable.Rows.Remove(row);
                    this._ImportGridRow.Remove(gridRow);

                    continue;
                }

                try
                {
                    decimal numplanQty = decimal.Parse(planQty);
                    if (numplanQty < 0)
                    {
                        if (!checkedRow.ContainsKey(planQty))
                        {
                            checkedRow.Add(planQty, false);
                        }

                        gridRow.Items.FindItemByKey(gridHelper.CheckColumnKey).Value = false;
                        gridRow.Items.FindItemByKey("ImportResult").Text = "计划数量不应小于0";
                        gridRow.Items.FindItemByKey("ImportResult").CssClass = "MsgYellow";
                        this._ImportDataTable.Rows.Remove(row);
                        this._ImportGridRow.Remove(gridRow);

                        continue;
                    }
                }
                catch
                {
                    if (!checkedRow.ContainsKey(planQty))
                    {
                        checkedRow.Add(planQty, false);
                    }

                    gridRow.Items.FindItemByKey(gridHelper.CheckColumnKey).Value = false;
                    gridRow.Items.FindItemByKey("ImportResult").Text = "计划数量格式错误";
                    gridRow.Items.FindItemByKey("ImportResult").CssClass = "MsgYellow";
                    this._ImportDataTable.Rows.Remove(row);
                    this._ImportGridRow.Remove(gridRow);
                    continue;
                }

                //planDate
                try
                {
                    int numplanDate = FormatHelper.TODateInt(planDate.Insert(4, "-").Insert(7, "-"));

                }
                catch
                {
                    if (!checkedRow.ContainsKey(planDate))
                    {
                        checkedRow.Add(planDate, false);
                    }

                    gridRow.Items.FindItemByKey(gridHelper.CheckColumnKey).Value = false;
                    gridRow.Items.FindItemByKey("ImportResult").Text = "计划日期格式错误";
                    gridRow.Items.FindItemByKey("ImportResult").CssClass = "MsgYellow";
                    this._ImportDataTable.Rows.Remove(row);
                    this._ImportGridRow.Remove(gridRow);
                    continue;
                }

                //planStartTime
                try
                {
                    int numplanStartTime = FormatHelper.TOTimeInt(planStartTime.ToString());

                }
                catch
                {
                    if (!checkedRow.ContainsKey(planStartTime))
                    {
                        checkedRow.Add(planStartTime, false);
                    }

                    gridRow.Items.FindItemByKey(gridHelper.CheckColumnKey).Value = false;
                    gridRow.Items.FindItemByKey("ImportResult").Text = "计划开始时间格式错误";
                    gridRow.Items.FindItemByKey("ImportResult").CssClass = "MsgYellow";
                    this._ImportDataTable.Rows.Remove(row);
                    this._ImportGridRow.Remove(gridRow);
                    continue;
                }

                int shiftDay = shiftModelFacade.GetShiftDayByBigSSCode(bigSSCode, dbDateTime.DateTime);

                object[] objs = _DataProvider.CustomQuery(typeof(BenQGuru.eMES.Domain.MOModel.Model),
                    new SQLParamCondition(@"select mocode from tblmo where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + @" and mocode=$MOCODE", new SQLParameter[] { new SQLParameter("$MOCODE", typeof(string), moCode.ToUpper()) }));

                if (objs == null)
                {
                    if (!checkedRow.ContainsKey(moCode))
                    {
                        checkedRow.Add(moCode, false);
                    }

                    gridRow.Items.FindItemByKey(gridHelper.CheckColumnKey).Value = false;
                    gridRow.Items.FindItemByKey("ImportResult").Text = _LanguageComponent1.GetString("$CS_MO_NotExit");
                    gridRow.Items.FindItemByKey("ImportResult").CssClass = "MsgYellow";
                    this._ImportDataTable.Rows.Remove(row);
                    this._ImportGridRow.Remove(gridRow);

                    continue;
                }

                object[] objss = _DataProvider.CustomQuery(typeof(BenQGuru.eMES.Domain.BaseSetting.StepSequence),
              new SQLParamCondition(@"select distinct bigsscode from tblss where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + @" and bigsscode=$BIGSSCODE",
              new SQLParameter[] { new SQLParameter("$BIGSSCODE", typeof(string), bigSSCode.ToUpper()) }));

                if (objss == null)
                {
                    if (!checkedRow.ContainsKey(bigSSCode))
                    {
                        checkedRow.Add(bigSSCode, false);
                    }

                    gridRow.Items.FindItemByKey(gridHelper.CheckColumnKey).Value = false;
                    gridRow.Items.FindItemByKey("ImportResult").Text = _LanguageComponent1.GetString("$BIGSSCODE_IS_NOT_EXIT");
                    gridRow.Items.FindItemByKey("ImportResult").CssClass = "MsgYellow";
                    this._ImportDataTable.Rows.Remove(row);
                    this._ImportGridRow.Remove(gridRow);

                    continue;
                }

                if (_MaterialFacade == null) { _MaterialFacade = new MaterialFacade(_DataProvider); }

                object objWorkPlanFromDB = _MaterialFacade.GetWorkPlan(bigSSCode, int.Parse(planDate), moCode, decimal.Parse(moSeq));

                if (objWorkPlanFromDB != null)
                {
                    //修改的投入数量必须大于等于实际数量
                    if (decimal.Parse(planQty) < ((WorkPlan)objWorkPlanFromDB).ActQty)
                    {
                        if (!checkedRow.ContainsKey(planQty))
                        {
                            checkedRow.Add(planQty, false);
                        }

                        gridRow.Items.FindItemByKey(gridHelper.CheckColumnKey).Value = false;
                        gridRow.Items.FindItemByKey("ImportResult").Text = _LanguageComponent1.GetString("$planqty_isnotequ_actqty");
                        gridRow.Items.FindItemByKey("ImportResult").CssClass = "MsgYellow";
                        this._ImportDataTable.Rows.Remove(row);
                        this._ImportGridRow.Remove(gridRow);

                        continue;
                    }

                    if ((((WorkPlan)objWorkPlanFromDB).ActionStatus == WorkPlanActionStatus.WorkPlanActionStatus_Close))
                    //执行状态为待投产或生产中才update                                  
                    {
                        if (!checkedRow.ContainsKey(planDate))
                        {
                            checkedRow.Add(planDate, false);
                        }

                        gridRow.Items.FindItemByKey(gridHelper.CheckColumnKey).Value = false;
                        gridRow.Items.FindItemByKey("ImportResult").Text = _LanguageComponent1.GetString("$status_error");
                        gridRow.Items.FindItemByKey("ImportResult").CssClass = "MsgYellow";
                        this._ImportDataTable.Rows.Remove(row);
                        this._ImportGridRow.Remove(gridRow);

                        continue;
                    }
                }

                if (int.Parse(planDate) < shiftDay)
                {

                    if (!checkedRow.ContainsKey(planDate))
                    {
                        checkedRow.Add(planDate, false);
                    }

                    gridRow.Items.FindItemByKey(gridHelper.CheckColumnKey).Value = false;
                    gridRow.Items.FindItemByKey("ImportResult").Text = "计划日期不能早于当前日期";
                    gridRow.Items.FindItemByKey("ImportResult").CssClass = "MsgYellow";
                    this._ImportDataTable.Rows.Remove(row);
                    this._ImportGridRow.Remove(gridRow);

                    continue;
                }

                WorkPlan GetworkPlanByKeys = (WorkPlan)materialFacade.GetWorkPlan(bigSSCode, int.Parse(planDate), moCode, decimal.Parse(moSeq));
                WorkPlan GetworkPlanByUniques = (WorkPlan)materialFacade.GetWorkPlan(bigSSCode, int.Parse(planDate), int.Parse(planSeq));

                if (GetworkPlanByKeys == null && GetworkPlanByUniques != null)
                {
                    if (!checkedRow.ContainsKey(moCode))
                    {
                        checkedRow.Add(moCode, false);
                    }

                    gridRow.Items.FindItemByKey(gridHelper.CheckColumnKey).Value = false;
                    gridRow.Items.FindItemByKey("ImportResult").Text = "请检查日期+线别+投产顺序的唯一性";
                    gridRow.Items.FindItemByKey("ImportResult").CssClass = "MsgYellow";
                    this._ImportDataTable.Rows.Remove(row);
                    this._ImportGridRow.Remove(gridRow);

                    continue;
                }

                if (GetworkPlanByKeys != null && GetworkPlanByUniques != null)
                {
                    if (GetworkPlanByKeys.BigSSCode != GetworkPlanByUniques.BigSSCode ||
                        GetworkPlanByKeys.PlanDate != GetworkPlanByUniques.PlanDate ||
                        GetworkPlanByKeys.MoCode != GetworkPlanByUniques.MoCode ||
                        GetworkPlanByKeys.MoSeq != GetworkPlanByUniques.MoSeq)
                    {
                        if (!checkedRow.ContainsKey(moCode))
                        {
                            checkedRow.Add(moCode, false);
                        }

                        gridRow.Items.FindItemByKey(gridHelper.CheckColumnKey).Value = false;
                        gridRow.Items.FindItemByKey("ImportResult").Text = "请检查日期+线别+投产顺序的唯一性";
                        gridRow.Items.FindItemByKey("ImportResult").CssClass = "MsgYellow";
                        this._ImportDataTable.Rows.Remove(row);
                        this._ImportGridRow.Remove(gridRow);

                        continue;
                    }
                }

                if (!checkedRow.ContainsKey(moCode))
                {
                    checkedRow.Add(moCode, false);
                }

                gridRow.Items.FindItemByKey(gridHelper.CheckColumnKey).Value = true;

            }
        }

        private void CheckMaterialNeed()
        {
            int count = _ImportDataTable.Rows.Count;
            Hashtable checkedRow = new Hashtable();
            for (int i = count - 1; i >= 0; i--)
            {
                DataRow row = _ImportDataTable.Rows[i];
                GridRecord gridRow = _ImportGridRow[i] as GridRecord;
                string itemCode = row["ITEMCODE"].ToString();
                string requestQty = row["REQUESTQTY"].ToString();
                string orgID = row["ORGID"].ToString();

                if (string.IsNullOrEmpty(itemCode))
                {
                    if (!checkedRow.ContainsKey(itemCode))
                    {
                        checkedRow.Add(itemCode, false);
                    }

                    gridRow.Items.FindItemByKey(gridHelper.CheckColumnKey).Value = false;
                    gridRow.Items.FindItemByKey("ITEMCODE").Text= itemCode;
                    gridRow.Items.FindItemByKey("ImportResult").Text = "物料不存在"; ;
                    gridRow.Items.FindItemByKey("ImportResult").CssClass = "MsgYellow";
                    this._ImportDataTable.Rows.Remove(row);
                    this._ImportGridRow.Remove(gridRow);
                    continue;

                }

                if (string.IsNullOrEmpty(orgID))
                {
                    if (!checkedRow.ContainsKey(orgID))
                    {
                        checkedRow.Add(orgID, false);
                    }

                    gridRow.Items.FindItemByKey(gridHelper.CheckColumnKey).Value = false;
                    gridRow.Items.FindItemByKey("ImportResult").Text = "组织代码不存在"; ;
                    gridRow.Items.FindItemByKey("ImportResult").CssClass = "MsgYellow";
                    this._ImportDataTable.Rows.Remove(row);
                    this._ImportGridRow.Remove(gridRow);
                    continue;

                }

                if (string.IsNullOrEmpty(requestQty))
                {
                    if (!checkedRow.ContainsKey(requestQty))
                    {
                        checkedRow.Add(requestQty, false);
                    }

                    gridRow.Items.FindItemByKey(gridHelper.CheckColumnKey).Value = false;
                    gridRow.Items.FindItemByKey("ImportResult").Text = "需求标准数量不存在";
                    gridRow.Items.FindItemByKey("ImportResult").CssClass = "MsgYellow";
                    this._ImportDataTable.Rows.Remove(row);
                    this._ImportGridRow.Remove(gridRow);
                    continue;

                }



                object[] objs = _DataProvider.CustomQuery(typeof(BenQGuru.eMES.Domain.MOModel.Material),
                    new SQLParamCondition(@"select mcode from tblmaterial where 1=1 and orgid=$ORGID and mcode=$ITEMCODE",
                    new SQLParameter[] {  new SQLParameter("$ORGID", typeof(int), int.Parse(orgID.ToUpper())),
                            new SQLParameter("$ITEMCODE", typeof(string), itemCode.ToUpper()) 
                            }));


                if (objs != null)
                {
                    if (!checkedRow.ContainsKey(itemCode))
                    {
                        checkedRow.Add(itemCode, true);
                    }

                    gridRow.Items.FindItemByKey(gridHelper.CheckColumnKey).Value = true;
                }
                else
                {
                    if (!checkedRow.ContainsKey(itemCode))
                    {
                        checkedRow.Add(itemCode, false);
                    }

                    gridRow.Items.FindItemByKey(gridHelper.CheckColumnKey).Value = false;
                    gridRow.Items.FindItemByKey("ImportResult").Text = _LanguageComponent1.GetString("$Error_Material_NotFound");
                    gridRow.Items.FindItemByKey("ImportResult").CssClass = "MsgYellow";
                    this._ImportDataTable.Rows.Remove(row);
                    this._ImportGridRow.Remove(gridRow);

                    continue;
                }



                try
                {
                    int numRequestQty = int.Parse(requestQty);
                    if (numRequestQty < 0)
                    {
                        if (!checkedRow.ContainsKey(requestQty))
                        {
                            checkedRow.Add(requestQty, false);
                        }

                        gridRow.Items.FindItemByKey(gridHelper.CheckColumnKey).Value = false;
                        gridRow.Items.FindItemByKey("ImportResult").Text = "需求标准数量不应小于0";
                        gridRow.Items.FindItemByKey("ImportResult").CssClass = "MsgYellow";
                        this._ImportDataTable.Rows.Remove(row);
                        this._ImportGridRow.Remove(gridRow);

                        continue;
                    }
                }
                catch
                {

                    if (!checkedRow.ContainsKey(requestQty))
                    {
                        checkedRow.Add(requestQty, false);
                    }

                    gridRow.Items.FindItemByKey(gridHelper.CheckColumnKey).Value = false;
                    gridRow.Items.FindItemByKey("ImportResult").Text = _LanguageComponent1.GetString("$Error_Number_Format_Error");
                    gridRow.Items.FindItemByKey("ImportResult").CssClass = "MsgYellow";
                    this._ImportDataTable.Rows.Remove(row);
                    this._ImportGridRow.Remove(gridRow);
                    continue;
                }
            }
        }

        private void CheckPlanWorkTime()
        {
            ItemFacade itemFacade = new ItemFacade(this._DataProvider);
            BaseModelFacade baseModelFacade = new BaseModelFacade(this._DataProvider);

            for (int i = _ImportDataTable.Rows.Count - 1; i >= 0 ; i--)
            {
                GridRecord gridRow = _ImportGridRow[i] as GridRecord;

                DataRow row = _ImportDataTable.Rows[i];
                string itemCode = row["ITEMCODE"].ToString();
                string ssCode = row["SSCODE"].ToString();
                string cycleTime = row["CYCLETIME"].ToString();
                string workingTime = row["WORKINGTIME"].ToString();

                //检查产品代码
                if (itemCode.Trim().Length <= 0 || itemFacade.GetItem(itemCode.Trim().ToUpper(), GlobalVariables.CurrentOrganizations.First().OrganizationID) == null)
                {
                    gridRow.Items.FindItemByKey(gridHelper.CheckColumnKey).Value = false;
                    gridRow.Items.FindItemByKey("ImportResult").Text = _LanguageComponent1.GetString("$CS_PRODUCT_CODE_NOT_EXIST");
                    gridRow.Items.FindItemByKey("ImportResult").CssClass = "MsgYellow";

                    _ImportDataTable.Rows.Remove(row);
                    _ImportGridRow.Remove(gridRow);
                    continue;
                }

                //检查产线代码
                if (ssCode.Trim().Length <= 0 || baseModelFacade.GetStepSequence(ssCode.Trim().ToUpper()) == null)
                {
                    gridRow.Items.FindItemByKey(gridHelper.CheckColumnKey).Value = false;
                    gridRow.Items.FindItemByKey("ImportResult").Text = _LanguageComponent1.GetString("$Error_SSCode_NotExist");
                    gridRow.Items.FindItemByKey("ImportResult").CssClass = "MsgYellow";

                    _ImportDataTable.Rows.Remove(row);
                    _ImportGridRow.Remove(gridRow);
                    continue;
                }

                //检查CycleTime
                if (cycleTime.Trim().Length <= 0)
                {
                    gridRow.Items.FindItemByKey(gridHelper.CheckColumnKey).Value = false;
                    gridRow.Items.FindItemByKey("ImportResult").Text = _LanguageComponent1.GetString("$Error_CycleTimeCannotEmpty");
                    gridRow.Items.FindItemByKey("ImportResult").CssClass = "MsgYellow";

                    _ImportDataTable.Rows.Remove(row);
                    _ImportGridRow.Remove(gridRow);
                    continue;
                }
                else
                {
                    int time = 0;
                    int.TryParse(cycleTime, out time);
                    if (time <= 0)
                    {

                        gridRow.Items.FindItemByKey(gridHelper.CheckColumnKey).Value = false;
                        gridRow.Items.FindItemByKey("ImportResult").Text = _LanguageComponent1.GetString("$Cycletime_Must_Over_Zero");
                        gridRow.Items.FindItemByKey("ImportResult").CssClass = "MsgYellow";

                        _ImportDataTable.Rows.Remove(row);
                        _ImportGridRow.Remove(gridRow);
                        continue;
                    }
                }

                //检查排产工时
                if (workingTime.Trim().Length <= 0)
                {
                    gridRow.Items.FindItemByKey(gridHelper.CheckColumnKey).Value = false;
                    gridRow.Items.FindItemByKey("ImportResult").Text = _LanguageComponent1.GetString("$Error_WorkingTimeCannotEmpty");
                    gridRow.Items.FindItemByKey("ImportResult").CssClass = "MsgYellow";

                    _ImportDataTable.Rows.Remove(row);
                    _ImportGridRow.Remove(gridRow);
                    continue;
                }
                else
                {
                    int time = 0;
                    int.TryParse(workingTime, out time);
                    if (time <= 0)
                    {

                        gridRow.Items.FindItemByKey(gridHelper.CheckColumnKey).Value = false;
                        gridRow.Items.FindItemByKey("ImportResult").Text = _LanguageComponent1.GetString("$WorkingTime_Must_Over_Zero");
                        gridRow.Items.FindItemByKey("ImportResult").CssClass = "MsgYellow";

                        _ImportDataTable.Rows.Remove(row);
                        _ImportGridRow.Remove(gridRow);
                        continue;
                    }
                }
            }
        }

        #endregion

        #region 导入

        public void Import(bool isRollBack)
        {
            switch (this._ImportType.ToUpper())
            {
                case "WORKPLAN":
                    this.ImportWorkPlan(isRollBack);
                    break;
                case "MATERIALNEED":
                    this.ImportMaterialNeed(isRollBack);
                    break;
                case "PLANWORKTIME":
                    this.ImportPlanWorkTime(isRollBack);
                    break;
                default:
                    break;
            }
        }

        public void ImportWorkPlan(bool isRollBack)
        {
            ShiftModelFacade shiftModelFacade = new ShiftModelFacade(this._DataProvider);
            DBDateTime now = FormatHelper.GetNowDBDateTime(this._DataProvider);

            object[] objs = ConvertArrayListToObjectArray(_ImportDataTable);
            object[] objDt = ConvertArrayListToObjectArray(_ImportDataTable);

            try
            {
                _DataProvider.BeginTransaction();

                if (objs != null && objDt != null)
                {
                    for (int i = 0; i < objs.Length; i++)
                    {
                        try
                        {
                            GridRecord row = _ImportGridRow[i] as GridRecord;
                            WorkPlan workPlanFromExcel = objs[i] as WorkPlan;
                            int shiftDay = shiftModelFacade.GetShiftDayByBigSSCode(workPlanFromExcel.BigSSCode, now.DateTime);
                            if (_MaterialFacade == null) { _MaterialFacade = new MaterialFacade(_DataProvider); }

                            WorkPlan objWorkPlanFromDB = (WorkPlan)_MaterialFacade.GetWorkPlan(workPlanFromExcel.BigSSCode, workPlanFromExcel.PlanDate,
                                                                            workPlanFromExcel.MoCode, workPlanFromExcel.MoSeq);

                            if (objWorkPlanFromDB != null)
                            {
                                ((WorkPlan)objs[i]).MaterialQty = objWorkPlanFromDB.MaterialQty;
                                ((WorkPlan)objs[i]).ActQty = objWorkPlanFromDB.ActQty;
                                ((WorkPlan)objs[i]).MaterialStatus = objWorkPlanFromDB.MaterialStatus;
                                ((WorkPlan)objs[i]).ActionStatus = objWorkPlanFromDB.ActionStatus;
                                ((WorkPlan)objs[i]).ItemCode = objWorkPlanFromDB.ItemCode;
                                ((WorkPlan)objs[i]).PromiseTime = objWorkPlanFromDB.PromiseTime;
                                ((WorkPlan)objs[i]).LastReqTime = objWorkPlanFromDB.LastReqTime;
                                ((WorkPlan)objs[i]).LastReceiveTime = objWorkPlanFromDB.LastReceiveTime;
                                ((WorkPlan)objs[i]).PlanEndTime = objWorkPlanFromDB.PlanEndTime;
                                ((WorkPlan)objDt[i]).MaterialQty = objWorkPlanFromDB.MaterialQty;
                                ((WorkPlan)objDt[i]).ActQty = objWorkPlanFromDB.ActQty;
                                ((WorkPlan)objDt[i]).MaterialStatus = objWorkPlanFromDB.MaterialStatus;
                                ((WorkPlan)objDt[i]).ActionStatus = objWorkPlanFromDB.ActionStatus;
                                ((WorkPlan)objDt[i]).ItemCode = objWorkPlanFromDB.ItemCode;
                                ((WorkPlan)objDt[i]).PromiseTime = objWorkPlanFromDB.PromiseTime;
                                ((WorkPlan)objDt[i]).LastReqTime = objWorkPlanFromDB.LastReqTime;
                                ((WorkPlan)objDt[i]).LastReceiveTime = objWorkPlanFromDB.LastReceiveTime;
                                ((WorkPlan)objDt[i]).PlanEndTime = objWorkPlanFromDB.PlanEndTime;

                                _DataProvider.Update(objs[i]);
                                _DataProvider.Update(objDt[i]);
                                row.Items.FindItemByKey("ImportResult").Text = _LanguageComponent1.GetString("$CycleImport_Success");
                                row.Items.FindItemByKey("ImportResult").CssClass = "LinkColorBlue";
                            }
                            else
                            {
                                if ((workPlanFromExcel.PlanDate) >= shiftDay)
                                {
                                    _DataProvider.Insert(objDt[i]);
                                    row.Items.FindItemByKey("ImportResult").Text = _LanguageComponent1.GetString("$CycleImport_Success");
                                    row.Items.FindItemByKey("ImportResult").CssClass = "LinkColorBlue";
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            GridRecord row = _ImportGridRow[i] as GridRecord;
                            row.Items.FindItemByKey("ImportResult").Text = _LanguageComponent1.GetString("$CycleImport_Error");
                            row.Items.FindItemByKey("ImportResult").CssClass = "ForeColorRed";
                            this.ErrorArray.Add(ex);
                        }
                    }
                }

                _DataProvider.CommitTransaction();

            }
            catch (Exception ex)
            {
                _DataProvider.RollbackTransaction();
                this.ErrorArray.Add(ex);
            }
        }

        public void ImportMaterialNeed(bool isRollBack)
        {
            ShiftModelFacade shiftModelFacade = new ShiftModelFacade(this._DataProvider);
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this._DataProvider);

            object[] objs = ConvertArrayListToObjectArray(_ImportDataTable);
            object[] objDt = ConvertArrayListToObjectArray(_ImportDataTable);

            try
            {
                _DataProvider.BeginTransaction();

                if (objs != null && objDt != null)
                {
                    for (int i = 0; i < objs.Length; i++)
                    {
                        try
                        {
                            MaterialReqStd materialReqStd = objs[i] as MaterialReqStd;
                            if (_MaterialFacade == null) { _MaterialFacade = new MaterialFacade(_DataProvider); }

                            object objWorkPlan = _MaterialFacade.GetMaterialReqStd(materialReqStd.ItemCode, materialReqStd.OrganizationID);

                            if (objWorkPlan != null)
                            {
                                _DataProvider.Update(objs[i]);
                                _DataProvider.Update(objDt[i]);

                            }
                            else
                            {
                                _DataProvider.Insert(objDt[i]);
                            }

                            GridRecord row = _ImportGridRow[i] as GridRecord;
                            row.Items.FindItemByKey("ImportResult").Text = _LanguageComponent1.GetString("$CycleImport_Success");
                            row.Items.FindItemByKey("ImportResult").CssClass = "LinkColorBlue";

                        }
                        catch (Exception ex)
                        {
                            GridRecord row = _ImportGridRow[i] as GridRecord;
                            row.Items.FindItemByKey("ImportResult").Text = "导入失败";
                            row.Items.FindItemByKey("ImportResult").CssClass = "ForeColorRed";
                            this.ErrorArray.Add(ex);
                        }
                    }
                }

                _DataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {
                _DataProvider.RollbackTransaction();
                this.ErrorArray.Add(ex);
            }
        }

        public void ImportPlanWorkTime(bool isRollBack)
        {
            PerformanceFacade performanceFacade = new PerformanceFacade(this._DataProvider);

            object[] objectArray = ConvertArrayListToObjectArray(_ImportDataTable);

            try
            {
                _DataProvider.BeginTransaction();

                if (objectArray != null)
                {
                    for (int i = 0; i < objectArray.Length; i++)
                    {
                        try
                        {
                            PlanWorkTime planWorkTime = (PlanWorkTime)objectArray[i];

                            object oldPlanWorkTime = performanceFacade.GetPlanWorkTime(planWorkTime.ItemCode, planWorkTime.SSCode);

                            if (oldPlanWorkTime == null)
                            {
                                _DataProvider.Insert(planWorkTime);
                            }
                            else
                            {
                                _DataProvider.Update(planWorkTime);
                            }

                            GridRecord row = _ImportGridRow[i] as GridRecord;
                            row.Items.FindItemByKey("ImportResult").Text = _LanguageComponent1.GetString("$CycleImport_Success");
                            row.Items.FindItemByKey("ImportResult").CssClass = "LinkColorBlue";

                        }
                        catch (Exception ex)
                        {
                            GridRecord row = _ImportGridRow[i] as GridRecord;
                            row.Items.FindItemByKey("ImportResult").Text = "导入失败";
                            row.Items.FindItemByKey("ImportResult").CssClass = "ForeColorRed";
                            this.ErrorArray.Add(ex);
                        }
                    }
                }

                _DataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {
                _DataProvider.RollbackTransaction();
                this.ErrorArray.Add(ex);
            }
        }

        #endregion

        #region Mapping

        private object[] ConvertArrayListToObjectArray(DataTable table)
        {
            TableMapAttribute tableAttribute = DomainObjectUtility.GetTableMapAttribute(GetImportType());

            string[] PKs = tableAttribute.GetKeyFields();
            if (PKs != null)
            {
                for (int i = 0; i < PKs.Length; i++)
                {
                    for (int j = 0; j < table.Rows.Count; j++)
                    {
                        if (string.Compare(PKs[i], "PLANDATE", true) == 0)
                        {
                            string planDate = table.Rows[j][PKs[i]].ToString().ToUpper();
                            if (planDate.Length > 8)
                                table.Rows[j][PKs[i]] = FormatHelper.TODateInt(table.Rows[j][PKs[i]].ToString().ToUpper());
                        }
                        else
                        {
                            table.Rows[j][PKs[i]] = table.Rows[j][PKs[i]].ToString().ToUpper();

                        }
                    }
                }

                for (int i = 0; i < table.Columns.Count; i++)
                {
                    for (int j = 0; j < table.Rows.Count; j++)
                    {
                        if (table.Columns[i].ColumnName == "PLANSTARTTIME")
                        {
                            string planStartTime = table.Rows[j][i].ToString().ToUpper();
                            if (planStartTime.Length > 6)
                                table.Rows[j][i] = FormatHelper.TOTimeInt(Convert.ToDateTime(table.Rows[j][i].ToString()).ToString("HH:mm:ss"));
                        }
                        else
                        {
                            table.Rows[j][i] = table.Rows[j][i].ToString().ToUpper();
                        }
                    }
                }
            }

            object[] objs = new object[table.Rows.Count];
            for (int i = 0; i < table.Rows.Count; i++)
            {
                object obj = GetImportType();

                obj = DomainObjectUtility.FillDomainObject(obj, table.Rows[i]);

                this.GetImportObjectType(ref obj, _ImportType);
                objs.SetValue(obj, i);
            }

            return objs;
        }

        private object GetImportType()
        {
            Type returnValue = typeof(object);

            switch (_ImportType.ToUpper())
            {
                case "WORKPLAN":
                    returnValue = typeof(WorkPlan);
                    break;
                case "MATERIALNEED":
                    returnValue = typeof(MaterialReqStd);
                    break;
                case "PLANWORKTIME":
                    returnValue = typeof(PlanWorkTime);
                    break;
                default:
                    break;
            }

            return DomainObjectUtility.CreateTypeInstance(returnValue);
        }

        private void GetImportObjectType(ref object obj, string importType)
        {
            if (obj == null)
            {
                return;
            }

            DBDateTime now = FormatHelper.GetNowDBDateTime(this._DataProvider);

            switch (importType.ToUpper())
            {
                case "WORKPLAN":
                    if (_MOFacade == null)
                    {
                        _MOFacade = new MOFacade(_DataProvider);
                    }

                    WorkPlan workPlan = obj as WorkPlan;
                    workPlan.MaintainUser = _UserCode;
                    workPlan.MaintainDate = now.DBDate;
                    workPlan.MaintainTime = now.DBTime;
                    workPlan.ActionStatus = WorkPlanActionStatus.WorkPlanActionStatus_Init;
                    workPlan.MaterialStatus = MaterialWarningStatus.MaterialWarningStatus_No;
                    object mo = _MOFacade.GetMO(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(workPlan.MoCode)));
                    workPlan.ItemCode = ((MO)mo).ItemCode;

                    break;

                case "MATERIALNEED":
                    MaterialReqStd materialReqStd = obj as MaterialReqStd;
                    materialReqStd.MaintainUser = _UserCode;
                    materialReqStd.MaintainDate = now.DBDate;
                    materialReqStd.MaintainTime = now.DBTime;
                    break;

                case "PLANWORKTIME":
                    PlanWorkTime planWorkTime = obj as PlanWorkTime;
                    planWorkTime.MaintainUser = _UserCode;
                    planWorkTime.MaintainDate = now.DBDate;
                    planWorkTime.MaintainTime = now.DBTime;
                    break;

                default:
                    break;
            }
        }

        #endregion

        #region Help Method for OPBOM


        #endregion
    }
}
