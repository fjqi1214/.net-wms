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

using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Domain.Performance;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Performance;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Material;
using ControlLibrary.Web.Language;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;

namespace BenQGuru.eMES.Web.BaseSetting.ImportIndirectManCountDate
{
    public class ImportManCountDateEngine
    {
        private LanguageComponent _languageComponent1;
        private IDomainDataProvider _dataProvider = null;
        private PerformanceFacade _Facade = null;
        private string ImportType = string.Empty;
        private DataTable ImportDatatable = null;
        private string UserCode = string.Empty;
        private ArrayList errorArray = null;
        private ArrayList ImportGridRow = null;
        private string checkColumn;
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
        public ImportManCountDateEngine(IDomainDataProvider dataProvider, LanguageComponent languageComponent1, string importType, DataTable table, string userCode, ArrayList importGridRow,string checkcolumn)
        {
            _dataProvider = dataProvider;
            _languageComponent1 = languageComponent1;
            ImportType = importType;
            ImportDatatable = table;
            UserCode = userCode;
            ImportGridRow = importGridRow;
            checkColumn = checkcolumn;
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
            object[] objDt=null;
            if (string.Compare(ImportType, "IndirectManCount", true) == 0)
            {
                objDt = ConvertArrayListToObjectArray(ImportDatatable, "IndirectManCount");
            }

            if (string.Compare(ImportType, "Line2Crew", true) == 0)
            {
                objDt = ConvertArrayListToObjectArray(ImportDatatable, "Line2Crew");
            }

            try
            {
                _dataProvider.BeginTransaction();

                if (string.Compare(ImportType, "IndirectManCount", true) == 0)
                {
                    if (objDt != null)
                    {
                        for (int i = 0; i < objDt.Length; i++)
                        {
                            try
                            {

                                GridRecord row = ImportGridRow[i] as GridRecord;
                                IndirectManCount IndirectManCountExcel = objDt[i] as IndirectManCount;

                                if (_Facade == null)
                                {
                                    _Facade = new PerformanceFacade(_dataProvider);
                                }

                                IndirectManCount getWorkPlanFromDB = (IndirectManCount)_Facade.GetIndirectManCount(IndirectManCountExcel.ShiftDate,
                                                                                                                    IndirectManCountExcel.ShiftCode,
                                                                                                                    IndirectManCountExcel.CrewCode,
                                                                                                                    IndirectManCountExcel.FactoryCode,
                                                                                                                    IndirectManCountExcel.FirstClass);

                                if (getWorkPlanFromDB != null)
                                {
                                    ((IndirectManCount)objDt[i]).FactoryCode = IndirectManCountExcel.FactoryCode;
                                    ((IndirectManCount)objDt[i]).FirstClass = IndirectManCountExcel.FirstClass;

                                    _dataProvider.Update(objDt[i]);
                                    row.Items.FindItemByKey("ImportResult").Text = _languageComponent1.GetString("$CycleImport_Success");
                                     row.Items.FindItemByKey("ImportResult").CssClass = "LinkColorBlue";
                                }
                                else
                                {
                                    _dataProvider.Insert(objDt[i]);
                                    row.Items.FindItemByKey("ImportResult").Text = _languageComponent1.GetString("$CycleImport_Success");
                                     row.Items.FindItemByKey("ImportResult").CssClass = "LinkColorBlue";
                                }
                            }
                            catch (Exception ex)
                            {
                                GridRecord row = ImportGridRow[i] as GridRecord;
                                row.Items.FindItemByKey("ImportResult").Text = _languageComponent1.GetString("$CycleImport_Error");
                                row.Items.FindItemByKey("ImportResult").CssClass = "ForeColorRed";
                                this.ErrorArray.Add(ex);
                            }
                        }
                    }
                }

                if (string.Compare(ImportType, "Line2Crew", true) == 0)
                {
                    if (objDt != null)
                    {
                        for (int i = 0; i < objDt.Length; i++)
                        {
                            try
                            {
                                GridRecord row = ImportGridRow[i] as GridRecord;
                                Line2Crew Line2CrewExcel = objDt[i] as Line2Crew;

                                if (_Facade == null)
                                {
                                    _Facade = new PerformanceFacade(_dataProvider);
                                }

                                Line2Crew getLine2CrewFromDB = (Line2Crew)_Facade.GetLine2Crew(Convert.ToInt32(Line2CrewExcel.ShiftDate), Line2CrewExcel.SSCode.ToString(), Line2CrewExcel.ShiftCode);

                                if (getLine2CrewFromDB != null)
                                {
                                    getLine2CrewFromDB.CrewCode = Line2CrewExcel.CrewCode;

                                    _dataProvider.Update(getLine2CrewFromDB);
                                    row.Items.FindItemByKey("ImportResult").Text = _languageComponent1.GetString("$CycleImport_Success");
                                     row.Items.FindItemByKey("ImportResult").CssClass = "LinkColorBlue";
                                }
                                else
                                {
                                    _dataProvider.Insert(objDt[i]);
                                    row.Items.FindItemByKey("ImportResult").Text = _languageComponent1.GetString("$CycleImport_Success");
                                     row.Items.FindItemByKey("ImportResult").CssClass = "LinkColorBlue";
                                }
                            }
                            catch (Exception ex)
                            {
                                GridRecord row = ImportGridRow[i] as GridRecord;
                                row.Items.FindItemByKey("ImportResult").Text = _languageComponent1.GetString("$CycleImport_Error");
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
            TableMapAttribute tableAttribute =
                DomainObjectUtility.GetTableMapAttribute(GetImportType(importtypedt));
            string[] PKs = tableAttribute.GetKeyFields();
            string[] Columns = new string[PKs.Length + 1];
            if (string.Compare(importtypedt, "IndirectManCount", true) == 0)
            {
                for (int i = 0; i < Columns.Length; i++)
                {
                    if (i == Columns.Length - 1)
                    {
                        Columns[i] = "Duration";
                    }
                    else
                    {
                        Columns[i] = PKs[i];
                    }
                }
                if (Columns != null)
                {
                    for (int i = 0; i < Columns.Length; i++)
                    {
                        for (int j = 0; j < table.Rows.Count; j++)
                        {
                            if (string.Compare(Columns[i], "ShiftDate", true) == 0)
                            {
                                string planDate = table.Rows[j][Columns[i]].ToString().ToUpper();
                                if (planDate.Length > 7)
                                    table.Rows[j][Columns[i]] = FormatHelper.TODateInt(Convert.ToDateTime(table.Rows[j][Columns[i]].ToString()).ToString("yyyy-MM-dd"));
                            }
                            else if (string.Compare(Columns[i], "Duration", true) == 0)
                            {
                                table.Rows[j][Columns[i]] = (Decimal.Parse(table.Rows[j][Columns[i]].ToString()) * 3600).ToString("0");
                            }
                            else
                            {
                                table.Rows[j][Columns[i]] = table.Rows[j][Columns[i]].ToString().ToUpper();
                            }
                        }
                    }
                }
            }

            if (string.Compare(importtypedt, "Line2Crew", true) == 0)
            {
                for (int i = 0; i < PKs.Length; i++)
                {
                    for (int j = 0; j < table.Rows.Count; j++)
                    {
                        if (string.Compare(PKs[i], "ShiftDate", true) == 0)
                        {
                            string planDate = table.Rows[j][PKs[i]].ToString().ToUpper();
                            if (planDate.Length > 7)
                                table.Rows[j][PKs[i]] = FormatHelper.TODateInt(Convert.ToDateTime(table.Rows[j][PKs[i]].ToString()).ToString("yyyy-MM-dd"));
                        }
                        else
                        {
                            table.Rows[j][PKs[i]] = table.Rows[j][PKs[i]].ToString().ToUpper();
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
                case "IndirectManCount":
                    type = typeof(IndirectManCount);
                    break;
                case "Line2Crew":
                    type = typeof(Line2Crew);
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
                case "IndirectManCount":
                    IndirectManCount indirectManCount = obj as IndirectManCount;
                    indirectManCount.MaintainUser = UserCode;
                    indirectManCount.MaintainDate = FormatHelper.TODateInt(DateTime.Now);
                    indirectManCount.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);
                    break;
                case "Line2Crew":
                    Line2Crew line2Crew = obj as Line2Crew;
                    line2Crew.MaintainUser = UserCode;
                    line2Crew.MaintainDate = FormatHelper.TODateInt(DateTime.Now);
                    line2Crew.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);
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
                case "IndirectManCount":
                    this.CheckIndirectManCount();
                    break;
                case "Line2Crew":
                    this.CheckLine2Crew();
                    break;
                default:
                    break;
            }
        }

        private void CheckIndirectManCount()
        {
            int count = this.ImportDatatable.Rows.Count;
            Hashtable checkedRow = new Hashtable();
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this._dataProvider);
            for (int i = count - 1; i >= 0; i--)
            {

                DataRow row = this.ImportDatatable.Rows[i];
                GridRecord gridRow = this.ImportGridRow[i] as GridRecord;
                string ShiftDate = row["ShiftDate"].ToString();
                string ShiftCode = row["ShiftCode"].ToString();
                string CrewCode = row["CrewCode"].ToString();
                string FacCode = row["FacCode"].ToString();
                string FirstClass = row["FirstClass"].ToString();
                string ManCount = row["ManCount"].ToString();
                string Duration = row["Duration"].ToString();
                Decimal DurationDecimal = Convert.ToDecimal(row["Duration"].ToString()) * 3600;

                if (string.IsNullOrEmpty(ShiftDate))
                {
                    if (!checkedRow.ContainsKey(ShiftDate))
                    {
                        checkedRow.Add(ShiftDate, false);
                    }

                    gridRow.Items.FindItemByKey(checkColumn).Value = false;
                    gridRow.Items.FindItemByKey("ImportResult").Text = _languageComponent1.GetString("$ShiftDate_NotExit");
                    gridRow.Items.FindItemByKey("ImportResult").CssClass = "ForeColorRed";
                   // gridRow.Items.FindItemByKey("ImportResult").Style.ForeColor = TextColor.NoticeColor;
                    this.ImportDatatable.Rows.Remove(row);
                    this.ImportGridRow.Remove(gridRow);
                    continue;
                }


                try
                {
                    DateTime planDateTime = Convert.ToDateTime(ShiftDate);
                }
                catch
                {
                    if (!checkedRow.ContainsKey(ShiftDate))
                    {
                        checkedRow.Add(ShiftDate, false);
                    }

                    gridRow.Items.FindItemByKey("").Value = false;
                    gridRow.Items.FindItemByKey("ImportResult").Text = _languageComponent1.GetString("$ShiftDate_Is_Wrong");
                    gridRow.Items.FindItemByKey("ImportResult").CssClass = "ForeColorRed";
                    //gridRow.Items.FindItemByKey("ImportResult").Style.ForeColor = TextColor.NoticeColor;
                    this.ImportDatatable.Rows.Remove(row);
                    this.ImportGridRow.Remove(gridRow);

                    continue;
                }

                if (string.IsNullOrEmpty(ShiftCode))
                {
                    if (!checkedRow.ContainsKey(ShiftCode))
                    {
                        checkedRow.Add(ShiftCode, false);
                    }

                    gridRow.Items.FindItemByKey(checkColumn).Value = false;
                    gridRow.Items.FindItemByKey("ImportResult").Text = _languageComponent1.GetString("$ShiftCode_NotExit");
                    gridRow.Items.FindItemByKey("ImportResult").CssClass = "ForeColorRed";
                    //gridRow.Items.FindItemByKey("ImportResult").Style.ForeColor = TextColor.NoticeColor;
                    this.ImportDatatable.Rows.Remove(row);
                    this.ImportGridRow.Remove(gridRow);

                    continue;
                }

                if (string.IsNullOrEmpty(CrewCode))
                {
                    if (!checkedRow.ContainsKey(CrewCode))
                    {
                        checkedRow.Add(CrewCode, false);
                    }

                    gridRow.Items.FindItemByKey(checkColumn).Value = false;
                    gridRow.Items.FindItemByKey("ImportResult").Text = _languageComponent1.GetString("$CrewCode_NotExit");
                    gridRow.Items.FindItemByKey("ImportResult").CssClass = "ForeColorRed";
                    //gridRow.Items.FindItemByKey("ImportResult").Style.ForeColor = TextColor.NoticeColor;
                    this.ImportDatatable.Rows.Remove(row);
                    this.ImportGridRow.Remove(gridRow);

                    continue;
                }


                if (string.IsNullOrEmpty(FacCode))
                {
                    if (!checkedRow.ContainsKey(FacCode))
                    {
                        checkedRow.Add(FacCode, false);
                    }

                    gridRow.Items.FindItemByKey(checkColumn).Value = false;
                    gridRow.Items.FindItemByKey("ImportResult").Text = _languageComponent1.GetString("$FacCode_NotExit");
                    gridRow.Items.FindItemByKey("ImportResult").CssClass = "ForeColorRed";
                    //gridRow.Items.FindItemByKey("ImportResult").Style.ForeColor = TextColor.NoticeColor;
                    this.ImportDatatable.Rows.Remove(row);
                    this.ImportGridRow.Remove(gridRow);

                    continue;

                }

                if (string.IsNullOrEmpty(FirstClass))
                {
                    if (!checkedRow.ContainsKey(FirstClass))
                    {
                        checkedRow.Add(FirstClass, false);
                    }

                    gridRow.Items.FindItemByKey(checkColumn).Value = false;
                    gridRow.Items.FindItemByKey("ImportResult").Text = _languageComponent1.GetString("$FirstClass_NotExit");
                    gridRow.Items.FindItemByKey("ImportResult").CssClass = "ForeColorRed";
                    //gridRow.Items.FindItemByKey("ImportResult").Style.ForeColor = TextColor.NoticeColor;
                    this.ImportDatatable.Rows.Remove(row);
                    this.ImportGridRow.Remove(gridRow);

                    continue;
                }

                if (string.IsNullOrEmpty(ManCount))
                {
                    if (!checkedRow.ContainsKey(ManCount))
                    {
                        checkedRow.Add(ManCount, false);
                    }

                    gridRow.Items.FindItemByKey(checkColumn).Value = false;
                    gridRow.Items.FindItemByKey("ImportResult").Text = _languageComponent1.GetString("$ManCount_NotExit");
                    gridRow.Items.FindItemByKey("ImportResult").CssClass = "ForeColorRed";
                    //gridRow.Items.FindItemByKey("ImportResult").Style.ForeColor = TextColor.NoticeColor;
                    this.ImportDatatable.Rows.Remove(row);
                    this.ImportGridRow.Remove(gridRow);

                    continue;
                }

                try
                {
                    int manCount = int.Parse(ManCount);
                }
                catch
                {
                    if (!checkedRow.ContainsKey(ManCount))
                    {
                        checkedRow.Add(ManCount, false);
                    }

                    gridRow.Items.FindItemByKey(checkColumn).Value = false;
                    gridRow.Items.FindItemByKey("ImportResult").Text = _languageComponent1.GetString("$ManCount_Is_Wrong");
                    gridRow.Items.FindItemByKey("ImportResult").CssClass = "ForeColorRed";
                    //gridRow.Items.FindItemByKey("ImportResult").Style.ForeColor = TextColor.NoticeColor;
                    this.ImportDatatable.Rows.Remove(row);
                    this.ImportGridRow.Remove(gridRow);

                    continue;
                }

                if (string.IsNullOrEmpty(Duration))
                {
                    if (!checkedRow.ContainsKey(Duration))
                    {
                        checkedRow.Add(Duration, false);
                    }

                    gridRow.Items.FindItemByKey(checkColumn).Value = false;
                    gridRow.Items.FindItemByKey("ImportResult").Text = _languageComponent1.GetString("$Duration_NotExit");
                    gridRow.Items.FindItemByKey("ImportResult").CssClass = "ForeColorRed";
                    //gridRow.Items.FindItemByKey("ImportResult").Style.ForeColor = TextColor.NoticeColor;
                    this.ImportDatatable.Rows.Remove(row);
                    this.ImportGridRow.Remove(gridRow);

                    continue;
                }

                try
                {
                    decimal duration = Decimal.Parse(Duration);
                }
                catch
                {
                    if (!checkedRow.ContainsKey(Duration))
                    {
                        checkedRow.Add(Duration, false);
                    }

                    gridRow.Items.FindItemByKey(checkColumn).Value = false;
                    gridRow.Items.FindItemByKey("ImportResult").Text = _languageComponent1.GetString("$Duration_Is_Wrong");
                    gridRow.Items.FindItemByKey("ImportResult").CssClass = "ForeColorRed";
                    //gridRow.Items.FindItemByKey("ImportResult").Style.ForeColor = TextColor.NoticeColor;
                    this.ImportDatatable.Rows.Remove(row);
                    this.ImportGridRow.Remove(gridRow);

                    continue;
                }


                ShiftModelFacade shiftModelFacade = new ShiftModelFacade(this._dataProvider);
                object shiftObject = shiftModelFacade.GetShift(FormatHelper.CleanString(ShiftCode.ToUpper()));

                if (shiftObject == null)
                {
                    if (!checkedRow.ContainsKey(ShiftCode))
                    {
                        checkedRow.Add(ShiftCode, false);
                    }

                    gridRow.Items.FindItemByKey(checkColumn).Value = false;
                    gridRow.Items.FindItemByKey("ImportResult").Text = _languageComponent1.GetString("$Error_Shift_Not_Exist");
                    gridRow.Items.FindItemByKey("ImportResult").CssClass = "ForeColorRed";
                    //gridRow.Items.FindItemByKey("ImportResult").Style.ForeColor = TextColor.NoticeColor;
                    this.ImportDatatable.Rows.Remove(row);
                    this.ImportGridRow.Remove(gridRow);

                    continue;
                }

                ShiftModel shiftModel = new ShiftModel(this._dataProvider);
                object crewObject = shiftModel.GetShiftCrew(FormatHelper.CleanString(CrewCode.ToUpper()));

                if (crewObject == null)
                {
                    if (!checkedRow.ContainsKey(CrewCode))
                    {
                        checkedRow.Add(CrewCode, false);
                    }

                    gridRow.Items.FindItemByKey(checkColumn).Value = false;
                    gridRow.Items.FindItemByKey("ImportResult").Text = _languageComponent1.GetString("$Error_ShiftCrew_Not_Exist");
                    gridRow.Items.FindItemByKey("ImportResult").CssClass = "ForeColorRed";
                    //gridRow.Items.FindItemByKey("ImportResult").Style.ForeColor = TextColor.NoticeColor;
                    this.ImportDatatable.Rows.Remove(row);
                    this.ImportGridRow.Remove(gridRow);

                    continue;
                }

                WarehouseFacade warehouseFacade = new WarehouseFacade(this._dataProvider);
                object facObject = warehouseFacade.GetFactory(FormatHelper.CleanString(FacCode.ToUpper()));

                if (facObject == null)
                {
                    if (!checkedRow.ContainsKey(FacCode))
                    {
                        checkedRow.Add(FacCode, false);
                    }

                    gridRow.Items.FindItemByKey(checkColumn).Value = false;
                    gridRow.Items.FindItemByKey("ImportResult").Text = _languageComponent1.GetString("$Error_FACCODE_Not_Exist");
                    gridRow.Items.FindItemByKey("ImportResult").CssClass = "ForeColorRed";
                    //gridRow.Items.FindItemByKey("ImportResult").Style.ForeColor = TextColor.NoticeColor;
                    this.ImportDatatable.Rows.Remove(row);
                    this.ImportGridRow.Remove(gridRow);

                    continue;
                }

                ItemFacade itemFacade = new ItemFacade(this._dataProvider);
                object itemClass = itemFacade.GetItemSecondClass(FormatHelper.CleanString(FirstClass.ToUpper()));

                if (itemClass == null)
                {
                    if (!checkedRow.ContainsKey(FirstClass))
                    {
                        checkedRow.Add(FirstClass, false);
                    }

                    gridRow.Items.FindItemByKey(checkColumn).Value = false;
                    gridRow.Items.FindItemByKey("ImportResult").Text = _languageComponent1.GetString("$Error_FirstClass_Not_Exist");
                    gridRow.Items.FindItemByKey("ImportResult").CssClass = "ForeColorRed";
                    //gridRow.Items.FindItemByKey("ImportResult").Style.ForeColor = TextColor.NoticeColor;
                    this.ImportDatatable.Rows.Remove(row);
                    this.ImportGridRow.Remove(gridRow);

                    continue;
                }

                gridRow.Items.FindItemByKey(checkColumn).Value = true;

            }
        }

        private void CheckLine2Crew()
        {
            int count = this.ImportDatatable.Rows.Count;
            Hashtable checkedRow = new Hashtable();
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this._dataProvider);
            for (int i = count - 1; i >= 0; i--)
            {

                DataRow row = this.ImportDatatable.Rows[i];
                GridRecord gridRow = this.ImportGridRow[i] as GridRecord;
                string ShiftDate = row["ShiftDate"].ToString();
                string ShiftCode = row["ShiftCode"].ToString();
                string CrewCode = row["CrewCode"].ToString();
                string ssCode = row["SSCode"].ToString();

                if (string.IsNullOrEmpty(ShiftDate))
                {
                    if (!checkedRow.ContainsKey(ShiftDate))
                    {
                        checkedRow.Add(ShiftDate, false);
                    }

                    gridRow.Items.FindItemByKey(checkColumn).Value = false;
                    gridRow.Items.FindItemByKey("ImportResult").Text = _languageComponent1.GetString("$ShiftDate_NotExit");
                    gridRow.Items.FindItemByKey("ImportResult").CssClass = "ForeColorRed";
                    //gridRow.Items.FindItemByKey("ImportResult").Style.ForeColor = TextColor.NoticeColor;
                    this.ImportDatatable.Rows.Remove(row);
                    this.ImportGridRow.Remove(gridRow);
                    continue;
                }

                try
                {
                    DateTime planDateTime = Convert.ToDateTime(ShiftDate);
                }
                catch
                {
                    if (!checkedRow.ContainsKey(ShiftDate))
                    {
                        checkedRow.Add(ShiftDate, false);
                    }

                    gridRow.Items.FindItemByKey(checkColumn).Value = false;
                    gridRow.Items.FindItemByKey("ImportResult").Text = _languageComponent1.GetString("$ShiftDate_Is_Wrong");
                    gridRow.Items.FindItemByKey("ImportResult").CssClass = "ForeColorRed";
                    //gridRow.Items.FindItemByKey("ImportResult").Style.ForeColor = TextColor.NoticeColor;
                    this.ImportDatatable.Rows.Remove(row);
                    this.ImportGridRow.Remove(gridRow);

                    continue;
                }

                if (string.IsNullOrEmpty(ShiftCode))
                {
                    if (!checkedRow.ContainsKey(ShiftCode))
                    {
                        checkedRow.Add(ShiftCode, false);
                    }

                    gridRow.Items.FindItemByKey(checkColumn).Value = false;
                    gridRow.Items.FindItemByKey("ImportResult").Text = _languageComponent1.GetString("$ShiftCode_NotExit");
                    gridRow.Items.FindItemByKey("ImportResult").CssClass = "ForeColorRed";
                    //gridRow.Items.FindItemByKey("ImportResult").Style.ForeColor = TextColor.NoticeColor;
                    this.ImportDatatable.Rows.Remove(row);
                    this.ImportGridRow.Remove(gridRow);

                    continue;
                }

                if (string.IsNullOrEmpty(CrewCode))
                {
                    if (!checkedRow.ContainsKey(CrewCode))
                    {
                        checkedRow.Add(CrewCode, false);
                    }

                    gridRow.Items.FindItemByKey(checkColumn).Value = false;
                    gridRow.Items.FindItemByKey("ImportResult").Text = _languageComponent1.GetString("$CrewCode_NotExit");
                    gridRow.Items.FindItemByKey("ImportResult").CssClass = "ForeColorRed";
                    //gridRow.Items.FindItemByKey("ImportResult").Style.ForeColor = TextColor.NoticeColor;
                    this.ImportDatatable.Rows.Remove(row);
                    this.ImportGridRow.Remove(gridRow);

                    continue;
                }


                if (string.IsNullOrEmpty(ssCode))
                {
                    if (!checkedRow.ContainsKey(ssCode))
                    {
                        checkedRow.Add(ssCode, false);
                    }

                    gridRow.Items.FindItemByKey(checkColumn).Value = false;
                    gridRow.Items.FindItemByKey("ImportResult").Text = _languageComponent1.GetString("$FacCode_NotExit");
                    gridRow.Items.FindItemByKey("ImportResult").CssClass = "ForeColorRed";
                    //gridRow.Items.FindItemByKey("ImportResult").Style.ForeColor = TextColor.NoticeColor;
                    this.ImportDatatable.Rows.Remove(row);
                    this.ImportGridRow.Remove(gridRow);

                    continue;

                }

                ShiftModelFacade shiftModelFacade = new ShiftModelFacade(this._dataProvider);
                object shiftObject = shiftModelFacade.GetShift(FormatHelper.CleanString(ShiftCode.ToUpper()));

                if (shiftObject == null)
                {
                    if (!checkedRow.ContainsKey(ShiftCode))
                    {
                        checkedRow.Add(ShiftCode, false);
                    }

                    gridRow.Items.FindItemByKey(checkColumn).Value = false;
                    gridRow.Items.FindItemByKey("ImportResult").Text = _languageComponent1.GetString("$Error_Shift_Not_Exist");
                    gridRow.Items.FindItemByKey("ImportResult").CssClass = "ForeColorRed";
                    //gridRow.Items.FindItemByKey("ImportResult").Style.ForeColor = TextColor.NoticeColor;
                    this.ImportDatatable.Rows.Remove(row);
                    this.ImportGridRow.Remove(gridRow);

                    continue;
                }

                ShiftModel shiftModel = new ShiftModel(this._dataProvider);
                object crewObject = shiftModel.GetShiftCrew(FormatHelper.CleanString(CrewCode.ToUpper()));

                if (crewObject == null)
                {
                    if (!checkedRow.ContainsKey(CrewCode))
                    {
                        checkedRow.Add(CrewCode, false);
                    }

                    gridRow.Items.FindItemByKey(checkColumn).Value = false;
                    gridRow.Items.FindItemByKey("ImportResult").Text = _languageComponent1.GetString("$Error_ShiftCrew_Not_Exist");
                    gridRow.Items.FindItemByKey("ImportResult").CssClass = "ForeColorRed";
                    //gridRow.Items.FindItemByKey("ImportResult").Style.ForeColor = TextColor.NoticeColor;
                    this.ImportDatatable.Rows.Remove(row);
                    this.ImportGridRow.Remove(gridRow);

                    continue;
                }

                BaseModelFacade baseModelFacade = new BaseModelFacade(this._dataProvider);
                object stepSequenceObject = baseModelFacade.GetStepSequence(FormatHelper.CleanString(ssCode.ToUpper()));

                if (stepSequenceObject == null)
                {
                    if (!checkedRow.ContainsKey(ssCode))
                    {
                        checkedRow.Add(ssCode, false);
                    }

                    gridRow.Items.FindItemByKey(checkColumn).Value = false;
                    gridRow.Items.FindItemByKey("ImportResult").Text = _languageComponent1.GetString("$CS_STEPSEQUENCE_NOT_EXIST");
                    gridRow.Items.FindItemByKey("ImportResult").CssClass = "ForeColorRed";
                    //gridRow.Items.FindItemByKey("ImportResult").Style.ForeColor = TextColor.NoticeColor;
                    this.ImportDatatable.Rows.Remove(row);
                    this.ImportGridRow.Remove(gridRow);

                    continue;
                }

                gridRow.Items.FindItemByKey(checkColumn).Value = true;

            }
        }
        #endregion
    }
}
