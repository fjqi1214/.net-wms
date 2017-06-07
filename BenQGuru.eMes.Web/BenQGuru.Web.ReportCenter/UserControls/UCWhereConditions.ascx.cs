using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.WebQuery;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Web.SelectQuery;
using BenQGuru.eMES.Web.UserControl;

using ControlLibrary.Web.Language;

namespace BenQGuru.Web.ReportCenter.UserControls
{
    public partial class UCWhereConditions : System.Web.UI.UserControl
    {
        private List<ControlWhereInfo> _DBInfo = null;

        #region 页面初始化

        protected LanguageComponent _LanguageComponent = null;
        protected IDomainDataProvider _DataProvider = null;

        override protected void OnInit(EventArgs e)
        {
            InitControlDBInfo();
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.txtSegCodeWhere.TextBox.TextChanged += new EventHandler(txtSegCodeWhere_TextChanged);
            this.ddlFirstClassWhere.SelectedIndexChanged += new EventHandler(ddlFirstClassWhere_SelectedIndexChanged);
            this.ddlSecondClassWhere.SelectedIndexChanged += new EventHandler(ddlSecondClassWhere_SelectedIndexChanged);
        }

        private void InitControlDBInfo()
        {
            _DBInfo = new List<ControlWhereInfo>();

            //**表示产生报表的三个核心Table
            List<string> tableFieldList = null;

            tableFieldList = new List<string>();
            tableFieldList.Add("tblmaterial.mtype");
            tableFieldList.Add("**.mtype");
            _DBInfo.Add(new ControlWhereInfo(ddlGoodSemiGoodWhere, tableFieldList, true, true, false, "IN"));

            tableFieldList = new List<string>();
            tableFieldList.Add("**.itemcode");
            _DBInfo.Add(new ControlWhereInfo(txtItemCodeWhere, tableFieldList, true, true, true, "IN/LIKE"));

            tableFieldList = new List<string>();
            tableFieldList.Add("tblmaterial.mmodelcode");
            tableFieldList.Add("**.mmodelcode");
            _DBInfo.Add(new ControlWhereInfo(txtMaterialModelCodeWhere, tableFieldList, true, true, true, "IN/LIKE"));

            tableFieldList = new List<string>();
            tableFieldList.Add("**.mocode");
            _DBInfo.Add(new ControlWhereInfo(txtMOCodeWhere, tableFieldList, true, true, true, "IN/LIKE"));

            tableFieldList = new List<string>();
            tableFieldList.Add("tblmo.motype");
            tableFieldList.Add("**.motype");
            _DBInfo.Add(new ControlWhereInfo(ddlMOTypeWhere, tableFieldList, true, true, true, "IN"));

            tableFieldList = new List<string>();
            tableFieldList.Add("tblmo.orderno");
            tableFieldList.Add("**.orderno");
            _DBInfo.Add(new ControlWhereInfo(txtOrderNoWhere, tableFieldList, true, true, true, "IN/LIKE"));

            tableFieldList = new List<string>();
            tableFieldList.Add("tblmo.mobom");
            tableFieldList.Add("**.mobom");
            _DBInfo.Add(new ControlWhereInfo(ddlMOBOMVersionWhere, tableFieldList, true, true, true, "IN"));

            tableFieldList = new List<string>();
            tableFieldList.Add("tblmaterial.mmachinetype");
            tableFieldList.Add("**.mmachinetype");
            _DBInfo.Add(new ControlWhereInfo(txtMaterialMachineTypeWhere, tableFieldList, true, true, true, "IN/LIKE"));

            tableFieldList = new List<string>();
            tableFieldList.Add("**.lotno");
            _DBInfo.Add(new ControlWhereInfo(txtLotNoWhere, tableFieldList, true, true, true, "IN/LIKE"));

            tableFieldList = new List<string>();
            tableFieldList.Add("tblmesentitylist.bigsscode");
            tableFieldList.Add("**.bigsscode");
            _DBInfo.Add(new ControlWhereInfo(txtBigSSCodeWhere, tableFieldList, true, true, true, "IN/LIKE"));

            tableFieldList = new List<string>();
            tableFieldList.Add("tblmesentitylist.segcode");
            tableFieldList.Add("**.segcode");
            _DBInfo.Add(new ControlWhereInfo(txtSegCodeWhere, tableFieldList, true, true, true, "IN/LIKE"));

            tableFieldList = new List<string>();
            tableFieldList.Add("tblmesentitylist.sscode");
            tableFieldList.Add("**.sscode");
            _DBInfo.Add(new ControlWhereInfo(txtSSCodeWhere, tableFieldList, true, true, true, "IN/LIKE"));

            tableFieldList = new List<string>();
            tableFieldList.Add("tblmesentitylist.opcode");
            tableFieldList.Add("**.opcode");
            _DBInfo.Add(new ControlWhereInfo(txtOPCodeWhere, tableFieldList, true, true, true, "IN/LIKE"));

            tableFieldList = new List<string>();
            tableFieldList.Add("tblmesentitylist.rescode");
            tableFieldList.Add("**.rescode");
            _DBInfo.Add(new ControlWhereInfo(txtResCodeWhere, tableFieldList, true, true, true, "IN/LIKE"));

            tableFieldList = new List<string>();
            tableFieldList.Add("tblmesentitylist.shiftcode");
            tableFieldList.Add("**.shiftcode");
            _DBInfo.Add(new ControlWhereInfo(ddlShiftCodeWhere, tableFieldList, true, true, true, "IN"));

            tableFieldList = new List<string>();
            tableFieldList.Add("tblline2crew.crewcode");
            tableFieldList.Add("**.crewcode");
            _DBInfo.Add(new ControlWhereInfo(ddlCrewCodeWhere, tableFieldList, true, true, true, "IN"));

            tableFieldList = new List<string>();
            tableFieldList.Add("tblitemclass.firstclass");
            tableFieldList.Add("**.firstclass");
            _DBInfo.Add(new ControlWhereInfo(ddlFirstClassWhere, tableFieldList, true, true, true, "IN"));

            tableFieldList = new List<string>();
            tableFieldList.Add("tblitemclass.secondclass");
            tableFieldList.Add("**.secondclass");
            _DBInfo.Add(new ControlWhereInfo(ddlSecondClassWhere, tableFieldList, true, true, true, "IN"));

            tableFieldList = new List<string>();
            tableFieldList.Add("tblitemclass.thirdclass");
            tableFieldList.Add("**.thirdclass");
            _DBInfo.Add(new ControlWhereInfo(ddlThirdClassWhere, tableFieldList, true, true, true, "IN"));

            tableFieldList = new List<string>();
            tableFieldList.Add("tblmo.momemo");
            _DBInfo.Add(new ControlWhereInfo(txtMOMemoWhere, tableFieldList, true, true, true, "IN/LIKE"));

            tableFieldList = new List<string>();
            tableFieldList.Add("tblsysparam_momemo.paramvalue");
            _DBInfo.Add(new ControlWhereInfo(ddlNewMassWhere, tableFieldList, true, true, false, "IN/LIKE"));

            tableFieldList = new List<string>();
            tableFieldList.Add("tblmaterial.mexportimport");
            tableFieldList.Add("**.mexportimport");
            _DBInfo.Add(new ControlWhereInfo(ddlMaterialExportImportWhere, tableFieldList, true, true, true, "IN"));

            tableFieldList = new List<string>();
            tableFieldList.Add("tbllot.productiontype");
            _DBInfo.Add(new ControlWhereInfo(txtProductionTypeWhere, tableFieldList, true, true, false, "IN/LIKE"));

            tableFieldList = new List<string>();
            tableFieldList.Add("tbllot.oqclottype");
            _DBInfo.Add(new ControlWhereInfo(txtOQCLotTypeWhere, tableFieldList, true, true, false, "IN/LIKE"));

            tableFieldList = new List<string>();
            tableFieldList.Add("tblecsg.ecsgcode");
            _DBInfo.Add(new ControlWhereInfo(txtErrorCauseGroupCodeWhere, tableFieldList, true, true, false, "IN/LIKE"));

            tableFieldList = new List<string>();
            tableFieldList.Add("**.dutycode");
            _DBInfo.Add(new ControlWhereInfo(txtDutyCodeWhere, tableFieldList, true, true, true, "IN/LIKE"));

            tableFieldList = new List<string>();
            tableFieldList.Add("**.exceptioncode");
            _DBInfo.Add(new ControlWhereInfo(txtExceptionCodeWhere, tableFieldList, true, true, true, "IN/LIKE"));

            tableFieldList = new List<string>();
            tableFieldList.Add("**.inspector");
            _DBInfo.Add(new ControlWhereInfo(txtInspectorWhere, tableFieldList, true, true, true, "IN/LIKE"));

            tableFieldList = new List<string>();
            tableFieldList.Add("**.exceptionflag");
            _DBInfo.Add(new ControlWhereInfo(chbExceptionFlagWhere, tableFieldList, true, true, true, "'Y'"));

            tableFieldList = new List<string>();
            tableFieldList.Add("**.actiontype");
            _DBInfo.Add(new ControlWhereInfo(chbIncludeDroppedMaterialWhere, tableFieldList, false, true, true, "BOOL::('0','1')::('0')"));

            tableFieldList = new List<string>();
            tableFieldList.Add("**.vendorcode");
            _DBInfo.Add(new ControlWhereInfo(txtVendorCodeWhere, tableFieldList, true, true, true, "IN/LIKE"));

            tableFieldList = new List<string>();
            tableFieldList.Add("**.itemcode");
            _DBInfo.Add(new ControlWhereInfo(txtMaterialCodeWhere, tableFieldList, true, true, true, "IN/LIKE"));

            tableFieldList = new List<string>();
            tableFieldList.Add("**.iqcitemtype");
            _DBInfo.Add(new ControlWhereInfo(ddlIQCItemTypeWhere, tableFieldList, true, true, false, "IN"));

            tableFieldList = new List<string>();
            tableFieldList.Add("**.iqclineitemtype");
            _DBInfo.Add(new ControlWhereInfo(ddlIQCLineItemTypeWhere, tableFieldList, true, true, false, "IN"));

            tableFieldList = new List<string>();
            tableFieldList.Add("tblmaterial.rohs");
            tableFieldList.Add("**.rohs");
            _DBInfo.Add(new ControlWhereInfo(ddlRoHSWhere, tableFieldList, true, true, true, "IN"));

            tableFieldList = new List<string>();
            tableFieldList.Add("**.concessionStatus");
            _DBInfo.Add(new ControlWhereInfo(ddlConcessionWhere, tableFieldList, true, true, true, "IN"));
        }

        #endregion

        #region 各个子控件的初始化函数

        private void txtSegCodeWhere_TextChanged(object sender, EventArgs e)
        {
            this.txtSSCodeWhere.Segment = this.txtSegCodeWhere.Text.Trim().ToUpper();
        }

        private void ddlGoodSemiGoodWhere_Load(object sender, System.EventArgs e)
        {
            this.ddlGoodSemiGoodWhere.Items.Clear();
            this.ddlGoodSemiGoodWhere.Items.Add(new ListItem("", ""));
            this.ddlGoodSemiGoodWhere.Items.Add(new ListItem(_LanguageComponent.GetString(ItemType.ITEMTYPE_FINISHEDPRODUCT), ItemType.ITEMTYPE_FINISHEDPRODUCT));
            this.ddlGoodSemiGoodWhere.Items.Add(new ListItem(_LanguageComponent.GetString(ItemType.ITEMTYPE_SEMIMANUFACTURE), ItemType.ITEMTYPE_SEMIMANUFACTURE));
        }

        private void ddlMOTypeWhere_Load(object sender, System.EventArgs e)
        {
            SystemSettingFacade sysFacade = new SystemSettingFacade(this._DataProvider);
            object[] moTypeList = sysFacade.GetParametersByParameterGroup("MOTYPE");

            ddlMOTypeWhere.Items.Clear();
            ddlMOTypeWhere.Items.Add(new ListItem("", ""));

            if (moTypeList != null)
            {
                foreach (BenQGuru.eMES.Domain.BaseSetting.Parameter param in moTypeList)
                {
                    string showText = _LanguageComponent.GetString(param.ParameterValue.Trim().ToUpper());
                    bool found = false;
                    foreach (ListItem item in ddlMOTypeWhere.Items)
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
                        ddlMOTypeWhere.Items.Add(new ListItem(showText, param.ParameterCode));
                    }
                }
            }
        }

        private void ddlShiftCodeWhere_Load(object sender, System.EventArgs e)
        {
            ShiftModel shiftModel = new ShiftModel(_DataProvider);

            DropDownListBuilder builder = new DropDownListBuilder(this.ddlShiftCodeWhere);
            builder.HandleGetObjectList += new BenQGuru.eMES.Web.Helper.GetObjectListDelegate(shiftModel.GetAllShift);
            builder.Build("ShiftCode", "ShiftCode");
            this.ddlShiftCodeWhere.Items.Insert(0, new ListItem("", ""));
        }

        private void ddlCrewCodeWhere_Load(object sender, System.EventArgs e)
        {
            ShiftModel shiftModel = new ShiftModel(_DataProvider);

            DropDownListBuilder builder = new DropDownListBuilder(this.ddlCrewCodeWhere);
            builder.HandleGetObjectList += new BenQGuru.eMES.Web.Helper.GetObjectListDelegate(shiftModel.GetAllShiftCrew);
            builder.Build("CrewCode", "CrewCode");
            this.ddlCrewCodeWhere.Items.Insert(0, new ListItem("", ""));
        }

        private void ddlFirstClassWhere_Load(object sender, System.EventArgs e)
        {
            ItemFacade itemFacade = new ItemFacade(_DataProvider);

            DropDownListBuilder builder = new DropDownListBuilder(this.ddlFirstClassWhere);
            builder.HandleGetObjectList += new BenQGuru.eMES.Web.Helper.GetObjectListDelegate(itemFacade.GetItemFirstClass);
            builder.Build("FirstClass", "FirstClass");
            this.ddlFirstClassWhere.Items.Insert(0, new ListItem("", ""));

            this.ddlSecondClassWhere.Items.Insert(0, new ListItem("", ""));

            this.ddlThirdClassWhere.Items.Insert(0, new ListItem("", ""));
        }

        private void ddlFirstClassWhere_SelectedIndexChanged(object sender, EventArgs e)
        {
            string firstClass = ddlFirstClassWhere.SelectedValue;

            this.ddlSecondClassWhere.Items.Clear();

            if (firstClass.Trim().Length > 0)
            {
                ItemFacade itemFacade = new ItemFacade(_DataProvider);
                object[] itemClassList = itemFacade.GetItemSecondClass(firstClass);
                if (itemClassList != null)
                {
                    foreach (ItemClass itemClass in itemClassList)
                    {
                        this.ddlSecondClassWhere.Items.Add(new ListItem(itemClass.SecondClass, itemClass.SecondClass));
                    }
                }
            }

            this.ddlSecondClassWhere.Items.Insert(0, new ListItem("", ""));

            this.ddlThirdClassWhere.Items.Clear();
            this.ddlThirdClassWhere.Items.Insert(0, new ListItem("", ""));
        }

        private void ddlSecondClassWhere_SelectedIndexChanged(object sender, EventArgs e)
        {
            string firstClass = ddlFirstClassWhere.SelectedValue;
            string secondClass = ddlSecondClassWhere.SelectedValue;

            this.ddlThirdClassWhere.Items.Clear();

            if (firstClass.Trim().Length > 0 && secondClass.Trim().Length > 0)
            {
                ItemFacade itemFacade = new ItemFacade(_DataProvider);
                object[] itemClassList = itemFacade.GetItemThirdClass(firstClass, secondClass);
                if (itemClassList != null)
                {
                    foreach (ItemClass itemClass in itemClassList)
                    {
                        this.ddlThirdClassWhere.Items.Add(new ListItem(itemClass.ThirdClass, itemClass.ThirdClass));
                    }
                }
            }

            this.ddlThirdClassWhere.Items.Insert(0, new ListItem("", ""));
        }

        private void ddlInputOututWhere_Load(object sender, System.EventArgs e)
        {
            this.ddlInputOututWhere.Items.Clear();
            this.ddlInputOututWhere.Items.Add(new ListItem("", ""));
            this.ddlInputOututWhere.Items.Add(new ListItem(_LanguageComponent.GetString("input"), "input"));
            this.ddlInputOututWhere.Items.Add(new ListItem(_LanguageComponent.GetString("output"), "output"));
            this.ddlInputOututWhere.Items.Add(new ListItem(_LanguageComponent.GetString("CompeletePut"), "CompeletePut"));
        }

        private void ddlNewMassWhere_Load(object sender, System.EventArgs e)
        {
            this.ddlNewMassWhere.Items.Clear();
            this.ddlNewMassWhere.Items.Add(new ListItem("", ""));
            this.ddlNewMassWhere.Items.Add(new ListItem(_LanguageComponent.GetString(MOProductType.MO_Product_Type_New), MOProductType.MO_Product_Type_New));
            this.ddlNewMassWhere.Items.Add(new ListItem(_LanguageComponent.GetString(MOProductType.MO_Product_Type_Mass), MOProductType.MO_Product_Type_Mass));
        }

        private void ddlMaterialExportImportWhere_Load(object sender, System.EventArgs e)
        {
            this.ddlMaterialExportImportWhere.Items.Clear();
            this.ddlMaterialExportImportWhere.Items.Add(new ListItem("", ""));
            this.ddlMaterialExportImportWhere.Items.Add(new ListItem(_LanguageComponent.GetString("materialexportimport_import"), "IMPORT"));
            this.ddlMaterialExportImportWhere.Items.Add(new ListItem(_LanguageComponent.GetString("materialexportimport_export"), "EXPORT"));
        }

        private void ddlBOMVersionWhere_Load(object sender, System.EventArgs e)
        {
            MOFacade moFacade = new MOFacade(_DataProvider);

            DropDownListBuilder builder = new DropDownListBuilder(this.ddlMOBOMVersionWhere);
            builder.HandleGetObjectList += new BenQGuru.eMES.Web.Helper.GetObjectListDelegate(moFacade.GetAllMOBOMVersion);
            builder.Build("BOMVersion", "BOMVersion");
            this.ddlMOBOMVersionWhere.Items.Insert(0, new ListItem("", ""));
        }

        private void ddlIQCItemTypeWhere_Load(object sender, System.EventArgs e)
        {
            this.ddlIQCItemTypeWhere.Items.Clear();
            this.ddlIQCItemTypeWhere.Items.Add(new ListItem("", ""));
            this.ddlIQCItemTypeWhere.Items.Add(new ListItem(_LanguageComponent.GetString("iqcheadattribute_normal"), "iqcheadattribute_normal"));
            this.ddlIQCItemTypeWhere.Items.Add(new ListItem(_LanguageComponent.GetString("iqcheadattribute_repo"), "iqcheadattribute_repo"));
            this.ddlIQCItemTypeWhere.Items.Add(new ListItem(_LanguageComponent.GetString("iqcheadattribute_present"), "iqcheadattribute_present"));
            this.ddlIQCItemTypeWhere.Items.Add(new ListItem(_LanguageComponent.GetString("iqcheadattribute_branchreturn"), "iqcheadattribute_branchreturn"));
        }

        private void ddlIQCLineItemTypeWhere_Load(object sender, System.EventArgs e)
        {
            this.ddlIQCLineItemTypeWhere.Items.Clear();
            this.ddlIQCLineItemTypeWhere.Items.Add(new ListItem("", ""));
            this.ddlIQCLineItemTypeWhere.Items.Add(new ListItem(_LanguageComponent.GetString("iqcdetailattribute_normal"), "iqcdetailattribute_normal"));
            this.ddlIQCLineItemTypeWhere.Items.Add(new ListItem(_LanguageComponent.GetString("iqcdetailattribute_claim"), "iqcdetailattribute_claim"));
            this.ddlIQCLineItemTypeWhere.Items.Add(new ListItem(_LanguageComponent.GetString("iqcdetailattribute_try"), "iqcdetailattribute_try"));
            this.ddlIQCLineItemTypeWhere.Items.Add(new ListItem(_LanguageComponent.GetString("iqcdetailattribute_ts_market"), "iqcdetailattribute_ts_market"));
        }

        private void ddlRoHSWhere_Load(object sender, System.EventArgs e)
        {
            this.ddlRoHSWhere.Items.Clear();
            this.ddlRoHSWhere.Items.Add(new ListItem("", ""));
            this.ddlRoHSWhere.Items.Add(new ListItem(_LanguageComponent.GetString("Y"), "Y"));
            this.ddlRoHSWhere.Items.Add(new ListItem(_LanguageComponent.GetString("N"), "N"));
        }

        private void ddlConcessionWhere_Load(object sender, System.EventArgs e)
        {
            this.ddlConcessionWhere.Items.Clear();
            this.ddlConcessionWhere.Items.Add(new ListItem("", ""));
            this.ddlConcessionWhere.Items.Add(new ListItem(_LanguageComponent.GetString("Y"), "Y"));
            this.ddlConcessionWhere.Items.Add(new ListItem(_LanguageComponent.GetString("N"), "N"));
        }

        #endregion

        #region 直接通过属性获得各个子控件的值，可以根据需要适当补充

        public string UserSelectInputOutput
        {
            get { return this.ddlInputOututWhere.SelectedValue; }
        }

        public string UserSelectOP
        {
            get { return this.txtOPCodeWhere.Text; }
        }

        public string UserSelectErrorCauseGroupCode
        {
            get { return this.txtErrorCauseGroupCodeWhere.Text; }
        }

        public string UserSelectGoodSemiGood
        {
            get { return this.ddlGoodSemiGoodWhere.SelectedValue; }
            set 
            {
                try
                {
                    this.ddlGoodSemiGoodWhere.SelectedValue = value;
                }
                catch
                {
                    this.ddlGoodSemiGoodWhere.SelectedIndex = 0;
                }
            }
        }

        public string UserSelectMOType
        {
            get { return this.ddlMOTypeWhere.SelectedValue; }
        }

        public string UserSelectStartDate
        {
            get { return this.datStartDateWhere.Value; } 
        }

        public string UserSelectEndDate
        {
            get { return this.datEndDateWhere.Value; }
        }

        #endregion

        #region 获得Where条件的相关函数和属性

        private string _ByTimeType = string.Empty;
        private bool _RoundDate = false;
        private int _TimeAdjust = 0;
        private string _ManualTable = string.Empty;

        public string ManualTable
        {
            get
            {
                return _ManualTable;
            }
            set
            {
                _ManualTable = value;
            }
        }

        private string GetControlValue(Control control)
        {
            string returnValue = string.Empty;

            if (control.Visible)
            {
                if (control.GetType() == typeof(TextBox))
                {
                    returnValue = ((TextBox)control).Text;
                }
                else if (control.GetType() == typeof(SelectableTextBox))
                {
                    returnValue = ((SelectableTextBox)control).Text;
                }
                else if (control.GetType() == typeof(SelectableTextBox4SS))
                {
                    returnValue = ((SelectableTextBox4SS)control).Text;
                }
                else if (control.GetType() == typeof(DropDownList))
                {
                    returnValue = ((DropDownList)control).SelectedValue;
                }
                else if (control.GetType() == typeof(eMESDate) || control.GetType().BaseType == typeof(eMESDate))
                {
                    returnValue = ((eMESDate)control).Date_DateTime.ToString("yyyyMMdd");
                }
                else if (control.GetType() == typeof(CheckBox))
                {
                    returnValue = ((CheckBox)control).Checked ? "true" : "";
                }
            }

            return returnValue.Trim();
        }

        private string ModifyValue(string rawText, bool isString, bool isMultiValues)
        {
            rawText = rawText.Replace("'", "''");

            if (isString)
            {
                if (isMultiValues)
                {
                    string[] value = rawText.Split(',');
                    for (int i = 0; i < value.Length; i++)
                    {
                        value[i] = "'" + value[i].Trim() + "'";
                    }

                    rawText = string.Join(",", value);
                }
                else
                {
                    rawText = "'" + rawText + "'";
                }
            }

            if (isMultiValues)
            {
                rawText = "(" + rawText + ")";
            }

            return rawText;
        }

        private void GetWhereSQL(ControlWhereInfo info, ref string text, ref string oper)
        {
            oper = info.SQLOperation;

            string rawText = text;
            if (info.ToUpper)
            {
                rawText = rawText.ToUpper();
            }
            else
            {
                rawText = rawText.ToLower();
            }

            switch (info.SQLOperation.Trim().ToUpper())
            {
                case "IN":
                    text = ModifyValue(rawText, info.IsString, true);
                    break;

                case "LIKE":
                    if (info.IsString)
                    {
                        text = "'%" + rawText + "%'";
                    }
                    break;

                case "IN/LIKE":
                    if (text.IndexOf(",") >= 0)
                    {
                        text = ModifyValue(rawText, info.IsString, true);
                        oper = "IN";
                    }
                    else
                    {
                        if (info.IsString)
                        {
                            text = "'%" + rawText + "%'";
                            oper = "LIKE";
                        }
                        else
                        {
                            text = ModifyValue(rawText, info.IsString, true);
                            oper = "IN";
                        }
                    }

                    break;

                case ">=":
                    text = ModifyValue(rawText, info.IsString, false);
                    break;

                case "<=":
                    text = ModifyValue(rawText, info.IsString, false);
                    break;

                case "'Y'":
                    text = "'Y'";
                    oper = "=";
                    break;

                default:
                    if (info.SQLOperation.Trim().ToUpper().IndexOf("BOOL") == 0)
                    {
                        string op = info.SQLOperation.Trim().ToUpper();
                        string[] list = op.Split(new string[] { "::" }, StringSplitOptions.None);
                        string trueValue = string.Empty;
                        string falseValue = string.Empty; 
                        if (list.Length >= 2)
                        {
                            trueValue = list[1];
                        }
                        if (list.Length >= 3)
                        {
                            falseValue = list[2];
                        }

                        bool boolValue = true;
                        bool.TryParse(rawText, out boolValue);

                        oper = "IN";
                        if (boolValue)
                        {
                            text = trueValue;
                        }
                        else
                        {
                            text = falseValue;
                        }
                    }

                    break;
            }
        }

        private string GetAdditionalSQL()
        {
            string returnValue = " ";

            #region 日期部分

            DateTime startDate =DateTime.ParseExact(datStartDateWhere.Value,"yyyy-MM-dd",System.Globalization.CultureInfo.CurrentCulture);

            DateTime endDate =DateTime.ParseExact(datEndDateWhere.Value,"yyyy-MM-dd",System.Globalization.CultureInfo.CurrentCulture);

            switch (_ByTimeType)
            {
                case NewReportByTimeType.Year:
                    startDate = startDate.AddYears(_TimeAdjust);
                    endDate = endDate.AddYears(_TimeAdjust);
                    break;

                case NewReportByTimeType.Month:
                    startDate = startDate.AddMonths(_TimeAdjust);
                    endDate = endDate.AddMonths(_TimeAdjust);
                    break;

                case NewReportByTimeType.Week:
                    startDate = startDate.AddDays(_TimeAdjust * 7);
                    endDate = endDate.AddDays(_TimeAdjust * 7);
                    break;

                default:
                    startDate = startDate.AddDays(_TimeAdjust);
                    endDate = endDate.AddDays(_TimeAdjust);
                    break;
            }

            string startDateString = startDate.ToString("yyyyMMdd");
            string endDateString = endDate.ToString("yyyyMMdd");
            if (_RoundDate)
            {
                ReportHelpher reportHelpher = new ReportHelpher(_DataProvider);
                startDateString = reportHelpher.RoundDateToBegin(_ByTimeType, startDate);
                endDateString = reportHelpher.RoundDateToEnd(_ByTimeType, endDate);
            }

            if (datStartDateWhere.Visible )
            {
                returnValue += " AND **.shiftday >= " + startDateString + " ";
            }
            if (datEndDateWhere.Visible )
            {
                returnValue += " AND **.shiftday <= " + endDateString + " ";
            }

            #endregion

            return returnValue;
        }

        public string GetWhereSQLStatement(string preferredTableNameList, string byTimeType, bool roundDate, int timeAdjust)
        {
            _ByTimeType = byTimeType;
            _RoundDate = roundDate;
            _TimeAdjust = timeAdjust;

            string sql = "1 = 1 ";
            string tableList = ",";
            string tableName = string.Empty;
            string text = string.Empty;
            string oper = string.Empty;

            if (preferredTableNameList.Trim().Length > 0)
            {
                tableList += preferredTableNameList.Trim().ToLower() + ",";
            }

            //第一遍扫描，处理那些对应数据库字段固定的控件
            foreach (ControlWhereInfo info in _DBInfo)
            {
                if (info.Control.Visible && info.TableFieldList.Count == 1)
                {
                    text = GetControlValue(info.Control).Trim();

                    if (!info.EmptyMeansAll || text.Length > 0)
                    {
                        tableName = info.TableFieldList[0].Substring(0, info.TableFieldList[0].IndexOf(".")).Trim().ToLower();

                        if (_ManualTable.IndexOf(tableName) >= 0)
                        {
                            continue;
                        }

                        if (tableList.IndexOf("," + tableName + ",") < 0)
                        {
                            tableList += tableName + ",";
                        }

                        GetWhereSQL(info, ref text, ref oper);
                        sql += "AND " + info.TableFieldList[0].Trim().ToLower() + " " + oper + " " + text + " ";

                    }
                }
            }

            //第二遍扫描，处理那些对应数据库字段可多选的控件
            foreach (ControlWhereInfo info in _DBInfo)
            {
                if (info.Control.Visible && info.TableFieldList.Count > 1)
                {
                    int pos = -1;
                    int lastPos = tableList.Length;
                    int usedIndex = -1;

                    for (int i = 0; i < info.TableFieldList.Count; i++)
                    {
                        tableName = info.TableFieldList[i].Substring(0, info.TableFieldList[i].IndexOf(".")).Trim().ToLower();

                        if (_ManualTable.IndexOf(tableName) >= 0)
                        {
                            continue;
                        }

                        pos = tableList.IndexOf("," + tableName + ",");
                        if (pos >= 0 && pos < lastPos)
                        {
                            usedIndex = i;
                            lastPos = pos;
                        }
                    }

                    if (usedIndex < 0)
                    {
                        usedIndex = 0;
                    }

                    tableList += info.TableFieldList[usedIndex].Substring(0, info.TableFieldList[usedIndex].IndexOf(".")).Trim().ToLower() + ",";

                    text = GetControlValue(info.Control).Trim();

                    if (!info.EmptyMeansAll || text.Length > 0)
                    {

                        GetWhereSQL(info, ref text, ref oper);
                        sql += "AND " + info.TableFieldList[usedIndex].Trim().ToLower() + " " + oper + " " + text + " ";
                    }
                }
            }

            sql += GetAdditionalSQL();

            return sql;
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlGoodSemiGoodWhere_Load(null, null);
                ddlMOTypeWhere_Load(null, null);
                ddlShiftCodeWhere_Load(null, null);
                ddlCrewCodeWhere_Load(null, null);
                datStartDateWhere.Value = DateTime.Today.ToString("yyyy-MM-dd");
                datEndDateWhere.Value = DateTime.Today.ToString("yyyy-MM-dd");
                ddlFirstClassWhere_Load(null, null);
                ddlInputOututWhere_Load(null, null);
                ddlNewMassWhere_Load(null, null);
                ddlMaterialExportImportWhere_Load(null, null);
                ddlBOMVersionWhere_Load(null, null);
                ddlIQCItemTypeWhere_Load(null, null);
                ddlIQCLineItemTypeWhere_Load(null, null);
                ddlRoHSWhere_Load(null, null);
                ddlConcessionWhere_Load(null, null);

                ReportPageHelper.SetControlValue(this, this.Request.Params);
            }

            this.PanelWhereConditions.Visible = false;
        }

        public void InitUserControl(LanguageComponent languageComponent, IDomainDataProvider dataProvider)
        {
            _LanguageComponent = languageComponent;
            _DataProvider = dataProvider;
        }

        public void SetControlPosition(int rowIndex, int columnIndex, string controlID)
        {
            if (rowIndex < 0 || columnIndex < 0)
            {
                return;
            }

            Control control = this.FindControl(controlID);
            if (control != null)
            {
                TableRow row = null;
                while (rowIndex >= TableControls.Rows.Count)
                {
                    row = new TableRow();
                    TableControls.Rows.Add(row);
                }
                row = TableControls.Rows[rowIndex];

                TableCell cell = null;
                while (columnIndex >= row.Cells.Count)
                {
                    cell = new TableCell();
                    cell.Width = 240;
                    row.Cells.Add(cell);
                }
                cell = row.Cells[columnIndex];

                cell.Controls.Add(control);
            }
        }
    }
}