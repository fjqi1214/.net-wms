using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Collections;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Domain.Material;
using ControlLibrary.Web.Language;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;


namespace BenQGuru.eMES.Web.WarehouseWeb.ImportData
{
    public class ImportDateEngine
    {
        private LanguageComponent _languageComponent1;
        private IDomainDataProvider _dataProvider = null;
        private BenQGuru.eMES.Material.MaterialFacade _Facade = null;
        private BenQGuru.eMES.MOModel.MOFacade _MoFacade = null;
        private BenQGuru.eMES.BaseSetting.BaseModelFacade _BaseModelFacade = null;
        private BenQGuru.eMES.MOModel.ItemFacade _ItemFacade = null;
        private BenQGuru.eMES.MOModel.ModelFacade _ModelFacade = null;
        private string ImportType = string.Empty;
        private DataTable ImportDatatable = null;
        private string UserCode = string.Empty;
        private ArrayList errorArray = null;
        private ArrayList ImportGridRow = null;

        public Page fromPage = null;
        public ArrayList ErrorArray
        {
            get
            {
                if (errorArray == null)
                {
                    errorArray = new ArrayList();
                    return errorArray;
                }
                else
                {
                    return errorArray;
                }
            }
            set
            {
                errorArray = value;
            }
        }
        ArrayList ResList = new ArrayList();
        ArrayList ItemList = new ArrayList();
        ArrayList MoList = new ArrayList();
        public ImportDateEngine(IDomainDataProvider dataProvider, LanguageComponent languageComponent1, string importType, DataTable table, string userCode, ArrayList importGridRow)
        {
            _dataProvider = dataProvider;
            _languageComponent1 = languageComponent1;
            ImportType = importType;
            ImportDatatable = table;
            UserCode = userCode;
            ImportGridRow = importGridRow;
        }

        public void CheckDataValid()
        {
            this.Check();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isRollBack">true:出错就回滚；false:出错skip到下一个</param>
        public void Import(bool isRollBack)
        {
            ShiftModelFacade shiftModelFacade = new ShiftModelFacade(this._dataProvider);
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this._dataProvider);

            object[] objs = ConvertArrayListToObjectArray(ImportDatatable, string.Empty);
            object[] objDt = null;

            if (string.Compare(ImportType, "WORKPLAN", true) == 0)
            {
                objDt = new object[objs.Length];
                objDt = ConvertArrayListToObjectArray(ImportDatatable, "WORKPLAN");
            }

            if (string.Compare(ImportType, "MATERIALNEED", true) == 0)
            {
                objDt = new object[objs.Length];
                objDt = ConvertArrayListToObjectArray(ImportDatatable, "MATERIALNEED");
            }
            if (string.Compare(ImportType, "TESTDATA", true) == 0)
            {
                objDt = new object[objs.Length];
                objDt = ConvertArrayListToObjectArray(ImportDatatable, "TESTDATA");
            }


            try
            {
                _dataProvider.BeginTransaction();

                if (string.Compare(ImportType, "WORKPLAN", true) == 0)
                {
                    if (objs != null && objDt != null)
                    {
                        for (int i = 0; i < objs.Length; i++)
                        {
                            try
                            {

                                GridRecord  row = ImportGridRow[i] as GridRecord ;
                                WorkPlan workPlanFromExcel = objs[i] as WorkPlan;
                                int shiftDay = shiftModelFacade.GetShiftDayByBigSSCode(workPlanFromExcel.BigSSCode, dbDateTime.DateTime);
                                if (_Facade == null) { _Facade = new MaterialFacade(_dataProvider); }

                                WorkPlan objWorkPlanFromDB = (WorkPlan)_Facade.GetWorkPlan(workPlanFromExcel.BigSSCode, workPlanFromExcel.PlanDate,
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

                                    _dataProvider.Update(objs[i]);
                                    _dataProvider.Update(objDt[i]);
                                    row.Items.FindItemByKey("ImportResult").Value = _languageComponent1.GetString("$CycleImport_Success");
                                    //row.Items.FindItemByKey("ImportResult").Style.ForeColor = TextColor.SuccessedColor;
                                }
                                else
                                {
                                    if ((workPlanFromExcel.PlanDate) >= shiftDay)
                                    {
                                        _dataProvider.Insert(objDt[i]);
                                        row.Items.FindItemByKey("ImportResult").Value = _languageComponent1.GetString("$CycleImport_Success");
                                        //row.Items.FindItemByKey("ImportResult").Style.ForeColor = TextColor.SuccessedColor;
                                    
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                GridRecord row = ImportGridRow[i] as GridRecord;
                                row.Items.FindItemByKey("ImportResult").Value = _languageComponent1.GetString("$CycleImport_Error");
                                //row.Items.FindItemByKey("ImportResult").Style.ForeColor = TextColor.FailedColor;
                                this.ErrorArray.Add(ex);
                            }
                        }
                    }
                }
                else if (string.Compare(ImportType, "MATERIALNEED", true) == 0)
                {
                    if (objs != null && objDt != null)
                    {
                        for (int i = 0; i < objs.Length; i++)
                        {
                            try
                            {
                                MaterialReqStd materialReqStd = objs[i] as MaterialReqStd;
                                if (_Facade == null) { _Facade = new MaterialFacade(_dataProvider); }

                                object objWorkPlan = _Facade.GetMaterialReqStd(materialReqStd.ItemCode, materialReqStd.OrganizationID);

                                if (objWorkPlan != null)
                                {
                                    _dataProvider.Update(objs[i]);
                                    _dataProvider.Update(objDt[i]);

                                }
                                else
                                {
                                    //_dataProvider.Insert(objs[i]);
                                    _dataProvider.Insert(objDt[i]);
                                }


                                GridRecord row = ImportGridRow[i] as GridRecord;
                                row.Items.FindItemByKey("ImportResult").Value = _languageComponent1.GetString("$CycleImport_Success");
                                //row.Items.FindItemByKey("ImportResult").Style.ForeColor = TextColor.SuccessedColor;

                            }
                            catch (Exception ex)
                            {
                                GridRecord row = ImportGridRow[i] as GridRecord;
                                row.Items.FindItemByKey("ImportResult").Value = "导入失败";
                                row.Items.FindItemByKey("ImportResult").CssClass = "ForeColorRed";
                                //row.Items.FindItemByKey("ImportResult").Style.ForeColor = TextColor.FailedColor;
                                this.ErrorArray.Add(ex);
                            }
                        }
                    }
                }
                else if (string.Compare(ImportType, "TESTDATA", true) == 0)
                {
                    if (objs != null && objDt != null)
                    {
                        for (int i = 0; i < objs.Length; i++)
                        {
                            try
                            {
                                TestDataIM testDataIM = objDt[i] as TestDataIM;
                                if (_BaseModelFacade == null)
                                {
                                    _BaseModelFacade = new BaseModelFacade(this._dataProvider);
                                }
                                if (_ModelFacade == null)
                                {
                                    _ModelFacade = new BenQGuru.eMES.MOModel.ModelFacade(this._dataProvider);
                                }
                                if (_ItemFacade == null)
                                {
                                    _ItemFacade = new BenQGuru.eMES.MOModel.ItemFacade(this._dataProvider);
                                }

                                object objRes = _BaseModelFacade.GetResource(testDataIM.ResCode);
                                object objSS = _BaseModelFacade.GetStepSequence((objRes as Resource).StepSequenceCode);
                                int shiftDay = shiftModelFacade.GetShiftDayByBigSSCode((objSS as StepSequence).BigStepSequenceCode, dbDateTime.DateTime);

                                Operation2Resource op2Res = _BaseModelFacade.GetOperationByResource(testDataIM.ResCode);
                                TimePeriod currTimePeriod = (TimePeriod)shiftModelFacade.GetTimePeriod((objRes as Resource).ShiftTypeCode, dbDateTime.DBTime);


                                string shiftCode = currTimePeriod.ShiftCode;

                                #region  获取Serial

                                MESEntityList mesEntityList = new MESEntityList();
                                Segment segment = (Segment)_BaseModelFacade.GetSegment((objRes as Resource).SegmentCode);
                                Item item = _ItemFacade.GetItem(testDataIM.ItemCode, (objRes as Resource).OrganizationID) as Item;
                                Model2Item model2Item = _ModelFacade.GetModel2ItemByItemCode(item.ItemCode, segment.OrganizationID) as Model2Item;
                                if (segment != null)
                                {
                                    mesEntityList.FactoryCode = segment.FactoryCode;
                                }
                                mesEntityList.BigSSCode = (objSS as StepSequence).BigStepSequenceCode;
                                mesEntityList.SegmentCode = segment.SegmentCode;
                                mesEntityList.StepSequenceCode = (objRes as Resource).StepSequenceCode;
                                mesEntityList.ResourceCode = testDataIM.ResCode;
                                mesEntityList.OPCode = op2Res.OPCode;
                                mesEntityList.ModelCode = model2Item.ModelCode;
                                mesEntityList.ShiftTypeCode = (objRes as Resource).ShiftTypeCode;
                                mesEntityList.ShiftCode = shiftCode;
                                mesEntityList.TPCode = currTimePeriod.TimePeriodCode;
                                mesEntityList.OrganizationID = (objRes as Resource).OrganizationID;

                                //现查询看是否存在，不存在的话，新增一笔
                                int returnSerialValue = _BaseModelFacade.GetMESEntityListSerial(mesEntityList);
                                if (returnSerialValue <= 0)
                                {
                                    _BaseModelFacade.AddMESEntityList(mesEntityList);
                                    returnSerialValue = _BaseModelFacade.GetMESEntityListSerial(mesEntityList);
                                }
                                #endregion

                                TestData testData = new TestData();
                                testData.RCard = testDataIM.RCard;
                                testData.MOCode = testDataIM.MOCode;
                                testData.ItemCode = testDataIM.ItemCode;
                                testData.CheckGroup = testDataIM.CheckGroup;
                                testData.CheckItemCode = testDataIM.CheckItemCode;
                                testData.DeviceNO = testDataIM.DeviceNO;
                                testData.LSL = testDataIM.LSL;
                                testData.Param = testDataIM.Param;
                                testData.TestingDate = testDataIM.TestingDate;
                                testData.TestingTime = testDataIM.TestingTime;
                                testData.TestingResult = testDataIM.TestingResult;
                                testData.TestingValue = testDataIM.TestingValue;
                                testData.USL = testDataIM.USL;
                                testData.MaintainDate = dbDateTime.DBDate;
                                testData.MaintainTime = dbDateTime.DBTime;
                                testData.MaintainUser = UserCode;
                                testData.ShiftDay = shiftDay;
                                testData.Tblmesentitylist_Serial = returnSerialValue;

                                _dataProvider.Insert(testData);


                                GridRecord row = ImportGridRow[i] as GridRecord;
                                row.Items.FindItemByKey("ImportResult").Text = _languageComponent1.GetString("$CycleImport_Success");
                                //row.Items.FindItemByKey("ImportResult").Style.ForeColor = TextColor.SuccessedColor;
                                //string strSript = string.Format("$($find('gridWebGrid').get_rows().get_row({0}).get_cellByColumnKey('ImportResult').get_element()).css('color','blue');", row.Index);
                                //fromPage.ClientScript.RegisterStartupScript(fromPage.GetType(), Guid.NewGuid().ToString(), strSript, true);
                                //ScriptManager.RegisterStartupScript(fromPage, fromPage.GetType(), Guid.NewGuid().ToString(), strSript, true);
                                row.Items.FindItemByKey("ImportResult").CssClass = "LinkColorBlue";
                            }
                            catch (Exception ex)
                            {
                                GridRecord row = ImportGridRow[i] as GridRecord;
                                row.Items.FindItemByKey("ImportResult").Text = "导入失败";
                                //row.Items.FindItemByKey("ImportResult").Style.ForeColor = TextColor.FailedColor;
                                row.Items.FindItemByKey("ImportResult").CssClass = "ForeColorRed";
                                this.ErrorArray.Add(ex);
                            }
                        }
                    }
                }

                _dataProvider.CommitTransaction();

            }
            catch (Exception ex)
            {
                _dataProvider.RollbackTransaction();
                this.ErrorArray.Add(ex);
            }



        }

        #region Mapping
        /// <summary>
        /// 默认类型
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        private object[] ConvertArrayListToObjectArray(DataTable table, string importtypedt)
        {
            importtypedt = importtypedt.Length == 0 ? ImportType : importtypedt;
            TableMapAttribute tableAttribute =
                DomainObjectUtility.GetTableMapAttribute(GetImportType(importtypedt));
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
                object obj = GetImportType(importtypedt);

                obj = DomainObjectUtility.FillDomainObject(obj, table.Rows[i]);

                this.GetImportObjectType(ref obj, importtypedt);
                objs.SetValue(obj, i);
            }

            return objs;
        }


        /// <summary>
        /// 返回导入的一个空对象
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private object GetImportType(string input)
        {
            System.Type type;
            switch (input)
            {
                case "WORKPLAN":
                    type = typeof(WorkPlan);
                    break;
                case "MATERIALNEED":
                    type = typeof(MaterialReqStd);
                    break;
                case "TESTDATA":
                    type = typeof(TestDataIM);
                    break;
                default:
                    type = typeof(System.Object);
                    break;
            }

            return DomainObjectUtility.CreateTypeInstance(type);
        }

        /// <summary>
        /// 补充空缺的不允许为空的栏位
        /// </summary>
        /// <param name="obj">导入对象的引用</param>
        private void GetImportObjectType(ref object obj, string importtype)
        {
            if (obj == null)
            {
                return;
            }

            switch (importtype)
            {
                case "WORKPLAN":
                    WorkPlan workPlan = obj as WorkPlan;
                    workPlan.MaintainUser = UserCode;
                    workPlan.MaintainDate = FormatHelper.TODateInt(DateTime.Now);
                    workPlan.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);
                    workPlan.ActionStatus = WorkPlanActionStatus.WorkPlanActionStatus_Init;
                    workPlan.MaterialStatus = MaterialWarningStatus.MaterialWarningStatus_No;

                    if (_MoFacade == null)
                    {
                        _MoFacade = new BenQGuru.eMES.MOModel.MOFacade(_dataProvider);
                    }

                    object objMo = _MoFacade.GetMO(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(workPlan.MoCode)));
                    workPlan.ItemCode = ((BenQGuru.eMES.Domain.MOModel.MO)objMo).ItemCode;

                    break;

                case "MATERIALNEED":
                    MaterialReqStd materialReqStd = obj as MaterialReqStd;
                    materialReqStd.MaintainUser = UserCode;
                    materialReqStd.MaintainDate = FormatHelper.TODateInt(DateTime.Now);
                    materialReqStd.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);
                    break;


                case "TESTDATA":
                    //Domain.OQC.TestDataIM testData = obj as Domain.OQC.TestData;
                    //testData.MaintainUser = UserCode;
                    //testData.MaintainDate = FormatHelper.TODateInt(DateTime.Now);
                    //testData.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);

                    //if (_BaseModelFacade == null)
                    //{
                    //    _BaseModelFacade = new BenQGuru.eMES.BaseSetting.BaseModelFacade(_dataProvider);
                    //}
                    //Operation2Resource op2Res = _BaseModelFacade.GetOperationByResource(testData.r);

                    //string opCode = op2Res.OPCode;


                    break;


                default:
                    break;
            }
        }
        #endregion


        #region Check Data Valid

        private void Check()
        {
            switch (ImportType)
            {

                case "WORKPLAN":
                    this.CheckWorkPlan();
                    break;
                case "MATERIALNEED":
                    this.CheckMaterialNeed();
                    break;
                case "TESTDATA":
                    this.CheckTestData();
                    break;
                default:
                    break;
            }
        }

        private void CheckWorkPlan()
        {
            int count = this.ImportDatatable.Rows.Count;
            Hashtable checkedRow = new Hashtable();
            ShiftModelFacade shiftModelFacade = new ShiftModelFacade(this._dataProvider);
            MaterialFacade materialFacade = new MaterialFacade(this._dataProvider);
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this._dataProvider);
            for (int i = count - 1; i >= 0; i--)
            {

                DataRow row = this.ImportDatatable.Rows[i];
                GridRecord gridRow = this.ImportGridRow[i] as GridRecord;
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

                    gridRow.Items[0].Value = false;
                    gridRow.Items.FindItemByKey("ImportResult").Value = _languageComponent1.GetString("$CS_MO_NotExit");
                    ////gridRow.Items.FindItemByKey("ImportResult").Style.ForeColor = TextColor.NoticeColor;
                    this.ImportDatatable.Rows.Remove(row);
                    this.ImportGridRow.Remove(gridRow);
                    continue;
                }

                if (string.IsNullOrEmpty(bigSSCode))
                {
                    if (!checkedRow.ContainsKey(bigSSCode))
                    {
                        checkedRow.Add(bigSSCode, false);
                    }

                    gridRow.Items[0].Value = false;
                    gridRow.Items.FindItemByKey("ImportResult").Value = _languageComponent1.GetString("$BIGSSCODE_IS_NOT_EXIT");
                    //gridRow.Items.FindItemByKey("ImportResult").Style.ForeColor = TextColor.NoticeColor;
                    this.ImportDatatable.Rows.Remove(row);
                    this.ImportGridRow.Remove(gridRow);

                    continue;
                }

                if (string.IsNullOrEmpty(planDate))
                {
                    if (!checkedRow.ContainsKey(planDate))
                    {
                        checkedRow.Add(planDate, false);
                    }

                    gridRow.Items[0].Value = false;
                    gridRow.Items.FindItemByKey("ImportResult").Value = "计划日期不能为空";
                    //gridRow.Items.FindItemByKey("ImportResult").Style.ForeColor = TextColor.NoticeColor;
                    this.ImportDatatable.Rows.Remove(row);
                    this.ImportGridRow.Remove(gridRow);

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

                    gridRow.Items[0].Value = false;
                    gridRow.Items.FindItemByKey("ImportResult").Value = "计划日期错误";
                    //gridRow.Items.FindItemByKey("ImportResult").Style.ForeColor = TextColor.NoticeColor;
                    this.ImportDatatable.Rows.Remove(row);
                    this.ImportGridRow.Remove(gridRow);

                    continue;
                }

                if (string.IsNullOrEmpty(planSeq))
                {
                    if (!checkedRow.ContainsKey(planSeq))
                    {
                        checkedRow.Add(planSeq, false);
                    }

                    gridRow.Items[0].Value = false;
                    gridRow.Items.FindItemByKey("ImportResult").Value = "生产顺序不能为空";
                    //gridRow.Items.FindItemByKey("ImportResult").Style.ForeColor = TextColor.NoticeColor;
                    this.ImportDatatable.Rows.Remove(row);
                    this.ImportGridRow.Remove(gridRow);

                    continue;

                }

                if (string.IsNullOrEmpty(moSeq))
                {
                    if (!checkedRow.ContainsKey(moSeq))
                    {
                        checkedRow.Add(moSeq, false);
                    }

                    gridRow.Items[0].Value = false;
                    gridRow.Items.FindItemByKey("ImportResult").Value = "工单项次不能为空";
                    //gridRow.Items.FindItemByKey("ImportResult").Style.ForeColor = TextColor.NoticeColor;
                    this.ImportDatatable.Rows.Remove(row);
                    this.ImportGridRow.Remove(gridRow);

                    continue;
                }

                if (string.IsNullOrEmpty(planStartTime))
                {
                    if (!checkedRow.ContainsKey(planStartTime))
                    {
                        checkedRow.Add(planStartTime, false);
                    }

                    gridRow.Items[0].Value = false;
                    gridRow.Items.FindItemByKey("ImportResult").Value = "计划开始时间不能为空";
                    //gridRow.Items.FindItemByKey("ImportResult").Style.ForeColor = TextColor.NoticeColor;
                    this.ImportDatatable.Rows.Remove(row);
                    this.ImportGridRow.Remove(gridRow);

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

                        gridRow.Items[0].Value = false;
                        gridRow.Items.FindItemByKey("ImportResult").Value = "生产顺序不应小于0";
                        //gridRow.Items.FindItemByKey("ImportResult").Style.ForeColor = TextColor.NoticeColor;
                        this.ImportDatatable.Rows.Remove(row);
                        this.ImportGridRow.Remove(gridRow);

                        continue;
                    }
                }
                catch
                {
                    if (!checkedRow.ContainsKey(planSeq))
                    {
                        checkedRow.Add(planSeq, false);
                    }

                    gridRow.Items[0].Value = false;
                    gridRow.Items.FindItemByKey("ImportResult").Value = "生产顺序格式错误";
                    //gridRow.Items.FindItemByKey("ImportResult").Style.ForeColor = TextColor.NoticeColor;
                    this.ImportDatatable.Rows.Remove(row);
                    this.ImportGridRow.Remove(gridRow);

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

                        gridRow.Items[0].Value = false;
                        gridRow.Items.FindItemByKey("ImportResult").Value = "工单项次不应小于0";
                        //gridRow.Items.FindItemByKey("ImportResult").Style.ForeColor = TextColor.NoticeColor;
                        this.ImportDatatable.Rows.Remove(row);
                        this.ImportGridRow.Remove(gridRow);

                        continue;
                    }
                }
                catch
                {
                    if (!checkedRow.ContainsKey(moSeq))
                    {
                        checkedRow.Add(moSeq, false);
                    }

                    gridRow.Items[0].Value = false;
                    gridRow.Items.FindItemByKey("ImportResult").Value = "工单项次格式错误";
                    //gridRow.Items.FindItemByKey("ImportResult").Style.ForeColor = TextColor.NoticeColor;
                    this.ImportDatatable.Rows.Remove(row);
                    this.ImportGridRow.Remove(gridRow);

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

                        gridRow.Items[0].Value = false;
                        gridRow.Items.FindItemByKey("ImportResult").Value = "计划数量不应小于0";
                        //gridRow.Items.FindItemByKey("ImportResult").Style.ForeColor = TextColor.NoticeColor;
                        this.ImportDatatable.Rows.Remove(row);
                        this.ImportGridRow.Remove(gridRow);

                        continue;
                    }
                }
                catch
                {
                    if (!checkedRow.ContainsKey(planQty))
                    {
                        checkedRow.Add(planQty, false);
                    }

                    gridRow.Items[0].Value = false;
                    gridRow.Items.FindItemByKey("ImportResult").Value = "计划数量格式错误";
                    //gridRow.Items.FindItemByKey("ImportResult").Style.ForeColor = TextColor.NoticeColor;
                    this.ImportDatatable.Rows.Remove(row);
                    this.ImportGridRow.Remove(gridRow);
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

                    gridRow.Items[0].Value = false;
                    gridRow.Items.FindItemByKey("ImportResult").Value = "计划日期格式错误";
                    //gridRow.Items.FindItemByKey("ImportResult").Style.ForeColor = TextColor.NoticeColor;
                    this.ImportDatatable.Rows.Remove(row);
                    this.ImportGridRow.Remove(gridRow);
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

                    gridRow.Items[0].Value = false;
                    gridRow.Items.FindItemByKey("ImportResult").Value = "计划开始时间格式错误";
                    //gridRow.Items.FindItemByKey("ImportResult").Style.ForeColor = TextColor.NoticeColor;
                    this.ImportDatatable.Rows.Remove(row);
                    this.ImportGridRow.Remove(gridRow);
                    continue;
                }

                int shiftDay = shiftModelFacade.GetShiftDayByBigSSCode(bigSSCode, dbDateTime.DateTime);

                object[] objs = _dataProvider.CustomQuery(typeof(BenQGuru.eMES.Domain.MOModel.Model),
                    new SQLParamCondition(@"select mocode from tblmo where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + @" and mocode=$MOCODE", new SQLParameter[] { new SQLParameter("$MOCODE", typeof(string), moCode.ToUpper()) }));

                if (objs == null)
                {
                    if (!checkedRow.ContainsKey(moCode))
                    {
                        checkedRow.Add(moCode, false);
                    }

                    gridRow.Items[0].Value = false;
                    gridRow.Items.FindItemByKey("ImportResult").Value = _languageComponent1.GetString("$CS_MO_NotExit");
                    //gridRow.Items.FindItemByKey("ImportResult").Style.ForeColor = TextColor.NoticeColor;
                    this.ImportDatatable.Rows.Remove(row);
                    this.ImportGridRow.Remove(gridRow);

                    continue;
                }

                object[] objss = _dataProvider.CustomQuery(typeof(BenQGuru.eMES.Domain.BaseSetting.StepSequence),
              new SQLParamCondition(@"select distinct bigsscode from tblss where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + @" and bigsscode=$BIGSSCODE",
              new SQLParameter[] { new SQLParameter("$BIGSSCODE", typeof(string), bigSSCode.ToUpper()) }));

                if (objss == null)
                {
                    if (!checkedRow.ContainsKey(bigSSCode))
                    {
                        checkedRow.Add(bigSSCode, false);
                    }

                    gridRow.Items[0].Value = false;
                    gridRow.Items.FindItemByKey("ImportResult").Value = _languageComponent1.GetString("$BIGSSCODE_IS_NOT_EXIT");
                    //gridRow.Items.FindItemByKey("ImportResult").Style.ForeColor = TextColor.NoticeColor;
                    this.ImportDatatable.Rows.Remove(row);
                    this.ImportGridRow.Remove(gridRow);

                    continue;
                }

                if (_Facade == null) { _Facade = new MaterialFacade(_dataProvider); }

                object objWorkPlanFromDB = _Facade.GetWorkPlan(bigSSCode, int.Parse(planDate), moCode, decimal.Parse(moSeq));

                if (objWorkPlanFromDB != null)
                {
                    //修改的投入数量必须大于等于实际数量
                    if (decimal.Parse(planQty) < ((WorkPlan)objWorkPlanFromDB).ActQty)
                    {
                        if (!checkedRow.ContainsKey(planQty))
                        {
                            checkedRow.Add(planQty, false);
                        }

                        gridRow.Items[0].Value = false;
                        gridRow.Items.FindItemByKey("ImportResult").Value = _languageComponent1.GetString("$planqty_isnotequ_actqty");
                        //gridRow.Items.FindItemByKey("ImportResult").Style.ForeColor = TextColor.NoticeColor;
                        this.ImportDatatable.Rows.Remove(row);
                        this.ImportGridRow.Remove(gridRow);

                        continue;
                    }

                    if ((((WorkPlan)objWorkPlanFromDB).ActionStatus == WorkPlanActionStatus.WorkPlanActionStatus_Close))
                    //执行状态为待投产或生产中才update                                  
                    {
                        if (!checkedRow.ContainsKey(planDate))
                        {
                            checkedRow.Add(planDate, false);
                        }

                        gridRow.Items[0].Value = false;
                        gridRow.Items.FindItemByKey("ImportResult").Value = _languageComponent1.GetString("$status_error");
                        //gridRow.Items.FindItemByKey("ImportResult").Style.ForeColor = TextColor.NoticeColor;
                        this.ImportDatatable.Rows.Remove(row);
                        this.ImportGridRow.Remove(gridRow);

                        continue;
                    }
                }

                if (int.Parse(planDate) < shiftDay)
                {

                    if (!checkedRow.ContainsKey(planDate))
                    {
                        checkedRow.Add(planDate, false);
                    }

                    gridRow.Items[0].Value = false;
                    gridRow.Items.FindItemByKey("ImportResult").Value = "计划日期不能早于当前日期";
                    //gridRow.Items.FindItemByKey("ImportResult").Style.ForeColor = TextColor.NoticeColor;
                    this.ImportDatatable.Rows.Remove(row);
                    this.ImportGridRow.Remove(gridRow);

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

                    gridRow.Items[0].Value = false;
                    gridRow.Items.FindItemByKey("ImportResult").Value = "请检查日期+线别+投产顺序的唯一性";
                    //gridRow.Items.FindItemByKey("ImportResult").Style.ForeColor = TextColor.NoticeColor;
                   
                    this.ImportDatatable.Rows.Remove(row);
                    this.ImportGridRow.Remove(gridRow);

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

                        gridRow.Items[0].Value = false;
                        gridRow.Items.FindItemByKey("ImportResult").Value = "请检查日期+线别+投产顺序的唯一性";
                        //gridRow.Items.FindItemByKey("ImportResult").Style.ForeColor = TextColor.NoticeColor;
                        this.ImportDatatable.Rows.Remove(row);
                        this.ImportGridRow.Remove(gridRow);

                        continue;
                    }
                }

                if (!checkedRow.ContainsKey(moCode))
                {
                    checkedRow.Add(moCode, false);
                }

                gridRow.Items[0].Value = true;

            }
        }

        private void CheckMaterialNeed()
        {
            int count = this.ImportDatatable.Rows.Count;
            Hashtable checkedRow = new Hashtable();
            for (int i = count - 1; i >= 0; i--)
            {
                DataRow row = this.ImportDatatable.Rows[i];
                GridRecord gridRow = this.ImportGridRow[i] as GridRecord;
                string itemCode = row["ITEMCODE"].ToString();
                string requestQty = row["REQUESTQTY"].ToString();
                string orgID = row["ORGID"].ToString();

                if (string.IsNullOrEmpty(itemCode))
                {
                    if (!checkedRow.ContainsKey(itemCode))
                    {
                        checkedRow.Add(itemCode, false);
                    }

                    gridRow.Items[0].Value = false;
                    gridRow.Items[1].Value = itemCode;
                    gridRow.Items.FindItemByKey("ImportResult").Value = "物料不存在"; ;
                    //gridRow.Items.FindItemByKey("ImportResult").Style.ForeColor = TextColor.NoticeColor;
                    this.ImportDatatable.Rows.Remove(row);
                    this.ImportGridRow.Remove(gridRow);
                    continue;

                }

                if (string.IsNullOrEmpty(orgID))
                {
                    if (!checkedRow.ContainsKey(orgID))
                    {
                        checkedRow.Add(orgID, false);
                    }

                    gridRow.Items[0].Value = false;
                    gridRow.Items.FindItemByKey("ImportResult").Value = "组织代码不存在"; ;
                    //gridRow.Items.FindItemByKey("ImportResult").Style.ForeColor = TextColor.NoticeColor;
                    this.ImportDatatable.Rows.Remove(row);
                    this.ImportGridRow.Remove(gridRow);
                    continue;

                }

                if (string.IsNullOrEmpty(requestQty))
                {
                    if (!checkedRow.ContainsKey(requestQty))
                    {
                        checkedRow.Add(requestQty, false);
                    }

                    gridRow.Items[0].Value = false;
                    gridRow.Items.FindItemByKey("ImportResult").Value = "需求标准数量不存在";
                    //gridRow.Items.FindItemByKey("ImportResult").Style.ForeColor = TextColor.NoticeColor;
                    this.ImportDatatable.Rows.Remove(row);
                    this.ImportGridRow.Remove(gridRow);
                    continue;

                }



                object[] objs = _dataProvider.CustomQuery(typeof(BenQGuru.eMES.Domain.MOModel.Material),
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

                    gridRow.Items[0].Value = true;
                }
                else
                {
                    if (!checkedRow.ContainsKey(itemCode))
                    {
                        checkedRow.Add(itemCode, false);
                    }

                    gridRow.Items[0].Value = false;
                    gridRow.Items.FindItemByKey("ImportResult").Value = _languageComponent1.GetString("$Error_Material_NotFound");
                    //gridRow.Items.FindItemByKey("ImportResult").Style.ForeColor = TextColor.NoticeColor;
                    this.ImportDatatable.Rows.Remove(row);
                    this.ImportGridRow.Remove(gridRow);

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

                        gridRow.Items[0].Value = false;
                        gridRow.Items.FindItemByKey("ImportResult").Value = "需求标准数量不应小于0";
                        //gridRow.Items.FindItemByKey("ImportResult").Style.ForeColor = TextColor.NoticeColor;
                        this.ImportDatatable.Rows.Remove(row);
                        this.ImportGridRow.Remove(gridRow);

                        continue;
                    }
                }
                catch
                {

                    if (!checkedRow.ContainsKey(requestQty))
                    {
                        checkedRow.Add(requestQty, false);
                    }

                    gridRow.Items[0].Value = false;
                    gridRow.Items.FindItemByKey("ImportResult").Value = _languageComponent1.GetString("$Error_Number_Format_Error");
                    //gridRow.Items.FindItemByKey("ImportResult").Style.ForeColor = TextColor.NoticeColor;
                    this.ImportDatatable.Rows.Remove(row);
                    this.ImportGridRow.Remove(gridRow);
                    continue;
                }
            }
        }

        private void CheckTestData()
        {
            int count = this.ImportDatatable.Rows.Count;
            Hashtable checkedRow = new Hashtable();
            for (int i = count - 1; i >= 0; i--)
            {
                DataRow row = this.ImportDatatable.Rows[i];
                GridRecord gridRow = this.ImportGridRow[i] as GridRecord;
                string resCode = row["RESCODE"].ToString();
                string itemCode = row["ITEMCODE"].ToString();
                string moCode = row["MOCODE"].ToString();
        
                string mDate = row["TESTINGDATE"].ToString();
                string mTime = row["TESTINGTIME"].ToString();

                if (string.IsNullOrEmpty(itemCode))
                {
                    if (!checkedRow.ContainsKey(itemCode))
                    {
                        checkedRow.Add(itemCode, false);
                    }

                    gridRow.Items[0].Value = false;
                    gridRow.Items[1].Value = itemCode;
                    gridRow.Items.FindItemByKey("ImportResult").Value = "产品不存在"; ;
                    //gridRow.Items.FindItemByKey("ImportResult").Style.ForeColor = TextColor.NoticeColor;
                    gridRow.Items.FindItemByKey("ImportResult").CssClass = "MsgYellow";
                    this.ImportDatatable.Rows.Remove(row);
                    this.ImportGridRow.Remove(gridRow);
                    continue;

                }


                if (string.IsNullOrEmpty(resCode))
                {
                    if (!checkedRow.ContainsKey(resCode))
                    {
                        checkedRow.Add(resCode, false);
                    }

                    gridRow.Items[0].Value = false;
                    gridRow.Items[1].Value = resCode;
                    gridRow.Items.FindItemByKey("ImportResult").Text = "资源不存在";
                    //gridRow.Items.FindItemByKey("ImportResult").Style.ForeColor = TextColor.NoticeColor;
                    gridRow.Items.FindItemByKey("ImportResult").CssClass = "MsgYellow";
                    this.ImportDatatable.Rows.Remove(row);
                    this.ImportGridRow.Remove(gridRow);
                    continue;

                }

                if (string.IsNullOrEmpty(moCode))
                {
                    if (!checkedRow.ContainsKey(moCode))
                    {
                        checkedRow.Add(itemCode, false);
                    }

                    gridRow.Items[0].Value = false;
                    gridRow.Items[1].Value = moCode;
                    gridRow.Items.FindItemByKey("ImportResult").Text = "工单不存在"; 
                    //gridRow.Items.FindItemByKey("ImportResult").Style.ForeColor = TextColor.NoticeColor;
                    gridRow.Items.FindItemByKey("ImportResult").CssClass = "ForeColorRed";
                    this.ImportDatatable.Rows.Remove(row);
                    this.ImportGridRow.Remove(gridRow);
                    continue;

                }

                if (string.IsNullOrEmpty(mDate))
                {
                    if (!checkedRow.ContainsKey(mDate))
                    {
                        checkedRow.Add(mDate, false);
                    }

                    gridRow.Items[0].Value = false;
                    gridRow.Items.FindItemByKey("ImportResult").Text = "日期不存在"; ;
                    //gridRow.Items.FindItemByKey("ImportResult").Style.ForeColor = TextColor.NoticeColor;
                    gridRow.Items.FindItemByKey("ImportResult").CssClass = "MsgYellow";
                    this.ImportDatatable.Rows.Remove(row);
                    this.ImportGridRow.Remove(gridRow);
                    continue;

                }

                if (string.IsNullOrEmpty(mTime))
                {
                    if (!checkedRow.ContainsKey(mTime))
                    {
                        checkedRow.Add(mTime, false);
                    }

                    gridRow.Items[0].Value = false;
                    gridRow.Items.FindItemByKey("ImportResult").Text = "时间不存在"; ;
                    //gridRow.Items.FindItemByKey("ImportResult").Style.ForeColor = TextColor.NoticeColor;
                    gridRow.Items.FindItemByKey("ImportResult").CssClass = "MsgYellow";
                    this.ImportDatatable.Rows.Remove(row);
                    this.ImportGridRow.Remove(gridRow);
                    continue;

                }

                object[] objs = null;

                if (!ResList.Contains(resCode))
                {
                    
                    objs = _dataProvider.CustomQuery(typeof(BenQGuru.eMES.Domain.BaseSetting.Resource),
                        new SQLParamCondition(@"select rescode from tblRES where 1=1 and rescode=$RESCODE",
                        new SQLParameter[] { new SQLParameter("$RESCODE", typeof(string), resCode.ToUpper()) }));


                    if (objs != null)
                    {
                        if (!checkedRow.ContainsKey(resCode))
                        {
                            checkedRow.Add(resCode, true);
                        }

                        gridRow.Items[0].Value = true;
                        //验证通过才加入到List中
                        ResList.Add(resCode);
                    }
                    else
                    {
                        if (!checkedRow.ContainsKey(resCode))
                        {
                            checkedRow.Add(resCode, false);
                        }

                        gridRow.Items[0].Value = false;
                        gridRow.Items.FindItemByKey("ImportResult").Text = _languageComponent1.GetString("$Error_ResCode_NotFound");
                        //gridRow.Items.FindItemByKey("ImportResult").Style.ForeColor = TextColor.NoticeColor;
                        gridRow.Items.FindItemByKey("ImportResult").CssClass = "MsgYellow";
                        this.ImportDatatable.Rows.Remove(row);
                        this.ImportGridRow.Remove(gridRow);

                        continue;
                    }
                }

                if (!MoList.Contains(moCode))
                {
                    
                    objs = _dataProvider.CustomQuery(typeof(BenQGuru.eMES.Domain.MOModel.MO),
                        new SQLParamCondition(@"select mocode from tblmo where 1=1 and mocode=$MOCODE",
                        new SQLParameter[] { new SQLParameter("$MOCODE", typeof(string), moCode.ToUpper()) }));

                    if (objs != null)
                    {
                        if (!checkedRow.ContainsKey(moCode))
                        {
                            checkedRow.Add(moCode, true);
                        }

                        gridRow.Items[0].Value = true;
                        //验证通过才加入到List中
                        MoList.Add(moCode);
                    }
                    else
                    {
                        if (!checkedRow.ContainsKey(moCode))
                        {
                            checkedRow.Add(moCode, false);
                        }

                        gridRow.Items[0].Value = false;
                        gridRow.Items.FindItemByKey("ImportResult").Text = _languageComponent1.GetString("$Error_MoCode_NotFound");
                        //gridRow.Items.FindItemByKey("ImportResult").Style.ForeColor = TextColor.NoticeColor;
                        gridRow.Items.FindItemByKey("ImportResult").CssClass = "MsgYellow";
                        this.ImportDatatable.Rows.Remove(row);
                        this.ImportGridRow.Remove(gridRow);

                        continue;
                    }
                }

                if (!ItemList.Contains(itemCode))
                {
                   
                    objs = _dataProvider.CustomQuery(typeof(BenQGuru.eMES.Domain.MOModel.Item),
                        new SQLParamCondition(@"select itemcode from tblitem where 1=1 and itemcode=$ITEMCODE",
                        new SQLParameter[] { new SQLParameter("$ITEMCODE", typeof(string), itemCode.ToUpper()) }));

                    if (objs != null)
                    {
                        if (!checkedRow.ContainsKey(itemCode))
                        {
                            checkedRow.Add(itemCode, true);
                        }

                        gridRow.Items[0].Value = true;

                        //验证通过才加入到List中
                        ItemList.Add(itemCode);
                    }
                    else
                    {
                        if (!checkedRow.ContainsKey(itemCode))
                        {
                            checkedRow.Add(itemCode, false);
                        }

                        gridRow.Items[0].Value = false;
                        gridRow.Items.FindItemByKey("ImportResult").Text = _languageComponent1.GetString("$Error_ItemCode_NotFound");
                        //gridRow.Items.FindItemByKey("ImportResult").Style.ForeColor = TextColor.NoticeColor;
                        gridRow.Items.FindItemByKey("ImportResult").CssClass = "MsgYellow";
                        this.ImportDatatable.Rows.Remove(row);
                        this.ImportGridRow.Remove(gridRow);

                        continue;
                    }
                }
            }
        }


        #endregion

        #region Help Method for OPBOM


        #endregion
    }
}
