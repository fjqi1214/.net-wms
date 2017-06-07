using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using BenQGuru.eMES.Web.Helper;
using System.Xml;
using BenQGuru.eMES.Common.Domain;
using System.Reflection;
using Infragistics.WebUI.UltraWebGrid;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Domain.MOModel;
using System.Globalization;

namespace BenQGuru.eMES.Web.MOModel
{
    public partial class FMOOutDataImport : BaseMPage
    {
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;

        protected System.Web.UI.HtmlControls.HtmlInputCheckBox Checkbox1;


        #region Web 窗体设计器生成的代码
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
            //
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.languageComponent1 = new ControlLibrary.Web.Language.LanguageComponent(this.components);
            // 
            // languageComponent1
            // 
            this.languageComponent1.Language = "CHS";
            this.languageComponent1.LanguagePackageDir = "D:\\SQC2.0\\eMES\\Source\\bin";
            this.languageComponent1.RuntimePage = null;
            this.languageComponent1.RuntimeUserControl = null;
            this.languageComponent1.UserControlName = "";

        }
        #endregion

        #region Init
        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);
            }
        }

        protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
        {
            return this.languageComponent1;
        }
        #endregion

        #region WebGrid
        protected override void InitWebGrid()
        {
            this.fileInit.Disabled = true;
            this.cmdView.Disabled = true;
            this.cmdEnter.Disabled = true;
            this.gridHelper.Grid.Columns.Clear();
            this.gridHelper.Grid.Rows.Clear();
            this.ViewState["GridColumn"] = null;

            string xmlPath = Server.MapPath("ExportMO.xml");
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlPath);
            XmlNode nodeFormat = xmlDoc.SelectSingleNode("//Format[@Name='OutDataMO_Piece']");
            if (nodeFormat == null)
                return;
            XmlNode nodeObj = nodeFormat.SelectSingleNode("ObjectMap");
            ArrayList listColumn = new ArrayList();
            for (int i = 0; i < nodeObj.ChildNodes.Count; i++)
            {
                string strKey = string.Empty;
                string strCaption = string.Empty;
                if (nodeObj.ChildNodes[i].Attributes["AttributeName"] != null)
                    strKey = nodeObj.ChildNodes[i].Attributes["AttributeName"].Value;
                if (nodeObj.ChildNodes[i].Attributes["Caption"] != null)
                    strCaption = nodeObj.ChildNodes[i].Attributes["Caption"].Value;
                if (strKey != string.Empty)
                {
                    this.gridHelper.AddColumn(strKey, strCaption);
                    listColumn.Add(strKey);
                }
            }
            this.gridHelper.AddColumn("IsValid", "导入状态");
            this.ViewState["GridColumn"] = listColumn;
            this.gridHelper.AddDefaultColumn(false, false);
            this.gridHelper.ApplyLanguage(this.languageComponent1);

            this.fileInit.Disabled = false;
            this.cmdView.Disabled = false;
        }

        #endregion

        #region Import
        private object[] items;
        protected void cmdView_ServerClick(object sender, System.EventArgs e)
        {
            string fileName = FileLoadProcess.UploadFile2ServerUploadFolder(this.Page, this.fileInit, null);
            if (fileName == null)
            {
                WebInfoPublish.Publish(this, "$Error_UploadFileIsEmpty", this.languageComponent1);
                return;
                //BenQGuru.eMES.Common.ExceptionManager.Raise(this.GetType().BaseType, "$Error_UploadFileIsEmpty");
            }
            if (!fileName.ToLower().EndsWith(".csv"))
            {
                WebInfoPublish.Publish(this, "$Error_UploadFileTypeError", this.languageComponent1);
                return;
                //BenQGuru.eMES.Common.ExceptionManager.Raise(this.GetType().BaseType, "$Error_UploadFileTypeError");
            }

            this.ViewState.Add("UploadedFileName", fileName);
            this.cmdQuery_Click(null, null);
            if (this.gridWebGrid.Rows.Count > 0)
            {
                this.cmdEnter.Disabled = false;
            }
        }

        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            ArrayList objs = new ArrayList();
            if (items == null)
            {
                this.GetAllItem();
            }
            for (int i = 1; i <= items.Length; i++)
            {
                if (i >= inclusive && i <= exclusive)
                {
                    objs.Add(items[i - 1]);
                }
            }

            return objs.ToArray();

        }
        protected override int GetRowCount()
        {
            if (items == null)
            {
                this.GetAllItem();
            }
            return items.Length;
        }

        protected override Infragistics.WebUI.UltraWebGrid.UltraGridRow GetGridRow(object obj)
        {
            if (this.ViewState["GridColumn"] != null)
            {
                System.Type type = obj.GetType();
                ArrayList list = (ArrayList)this.ViewState["GridColumn"];
                object[] objRow = new object[list.Count + 1];
                for (int i = 0; i < list.Count; i++)
                {
                    objRow[i] = GetFieldValue(obj, list[i].ToString());
                }
                objRow[objRow.Length - 1] = GetFieldValue(obj, "ValidateResult");
                return new UltraGridRow(objRow);
            }
            return null;
        }

        private object[] GetAllItem()
        {
            try
            {
                string fileName = string.Empty;

                fileName = this.ViewState["UploadedFileName"].ToString();

                string configFile = this.getParseConfigFileName();

                BenQGuru.eMES.Web.Helper.DataFileParser parser = new BenQGuru.eMES.Web.Helper.DataFileParser();
                parser.FormatName = "OutDataMO_Piece";
                parser.ConfigFile = configFile;
                items = parser.Parse(fileName);
                ValidateItems();
            }
            catch
            {

            }

            return items;

        }

        private string getParseConfigFileName()
        {
            string configFile = this.Server.MapPath(this.TemplateSourceDirectory);
            if (configFile[configFile.Length - 1] != '\\')
            {
                configFile += "\\";
            }
            configFile += "ExportMO.xml";
            return configFile;
        }

        private void ValidateItems()
        {
            if (items == null || items.Length == 0)
                return;

            CultureInfo culture = new System.Globalization.CultureInfo("en-US");

            for (int i = 0; i < items.Length; i++)
            {
                string result = string.Empty;
                MOFacade moFacade = new MOFacade(this.DataProvider);
                object item = items[i];

                /*-----MOCode  Check -----*/
                string moCode = GetFieldValue(items[i], "MOCode");
                object obj = moFacade.GetMO(moCode);
                if (obj == null)
                {
                    result = "工单不存在";
                }

                if (obj != null)
                {
                    MO objMo = obj as MO;
                    if (string.Compare(objMo.MOStatus, MOManufactureStatus.MOSTATUS_CLOSE, true) == 0)
                    {
                        result = string.Format("{0};工单状态已经关闭", result);
                    }

                    /*-----------MOPLANQTY Check -----------*/
                    string moPlanQty = GetFieldValue(items[i], "MOPlanQty1");
                    if (moPlanQty != string.Empty)
                    {
                        try
                        {
                            decimal numQty = decimal.Parse(moPlanQty);
                            if (numQty < objMo.MOInputQty)
                            {
                                result = string.Format("{0};计划数量不能小于投入数量", result);
                            }
                        }
                        catch
                        {
                            result = string.Format("{0};计划数量必须为数字", result);
                        }
                    }
                }

                /*-----------MOBOM Check  -------*/
                string moBOM = GetFieldValue(items[i], "BOMVersion1");
                if (moBOM != string.Empty)
                {
                    try
                    {
                        int numBOM = int.Parse(moBOM);
                    }
                    catch
                    {
                        result = string.Format("{0};工单BOM必须是数字", result);
                    }
                }

                /*----------- MOPLANSTARTDATE Check ------------*/
                string moPlanStartDate = GetFieldValue(items[i], "MOPlanStartDate1");
                if (moPlanStartDate != string.Empty)
                {
                    try
                    {
                        DateTime startDate = DateTime.ParseExact(moPlanStartDate, "yyyyMMdd", culture);
                    }
                    catch
                    {
                        result = string.Format("{0};计划开工日期格式不正确(YYYYMMDD)", result);
                    }
                }


                /*----------- MOPLANSTARTTIME Check ------------*/
                string moPlanStartTime = GetFieldValue(items[i], "MOPlanStartTime");
                if (moPlanStartTime != string.Empty)
                {
                    try
                    {
                        DateTime startTime = DateTime.ParseExact(moPlanStartTime.PadLeft(6, '0'), "HHmmss", culture);
                    }
                    catch
                    {
                        result = string.Format("{0};计划开工时间格式不正确(HHMMSS)", result);
                    }
                }

                /*----------- MOPLANENDDATE Check ------------*/
                string moPlanEndDate = GetFieldValue(items[i], "MOPlanEndDate1");
                if (moPlanEndDate != string.Empty)
                {
                    try
                    {
                        DateTime endDate = DateTime.ParseExact(moPlanEndDate, "yyyyMMdd", culture);
                    }
                    catch
                    {
                        result = string.Format("{0};计划完工日期格式不正确(YYYYMMDD)", result);
                    }
                }


                /*----------- MOPLANENDTIME Check ------------*/
                string moPlanEndTime = GetFieldValue(items[i], "MOPlanEndTime");
                if (moPlanEndTime != string.Empty)
                {
                    try
                    {
                        DateTime endTime = DateTime.ParseExact(moPlanEndTime.PadLeft(6, '0'), "HHmmss", culture);
                    }
                    catch
                    {
                        result = string.Format("{0};计划完工时间格式不正确(HHMMSS)", result);
                    }
                }

                /*-----------ORDERSEQ Check  -------*/
                string orderSeq = GetFieldValue(items[i], "OrderSequence1");
                if (orderSeq != string.Empty)
                {
                    try
                    {
                        decimal numOrderSeq = decimal.Parse(orderSeq);
                    }
                    catch
                    {
                        result = string.Format("{0};订单序号必须是数字", result);
                    }
                }


                if (result != string.Empty && result != null)
                {
                    int j = result.IndexOf(";");
                    if (j == 0)
                    {
                        result = result.Substring(1, result.Length - 1);
                    }
                }

                if (result == string.Empty)
                {
                    result = "OK";
                }

                SetFieldValue(item, "ValidateResult", result);
            }

        }

        protected void cmdImport_ServerClick(object sender, System.EventArgs e)
        {
            if (items == null)
            {
                items = GetAllItem();
                if (items == null)
                    return;
            }
            if (items == null || items.Length == 0)
                return;

            int successNum = 0;
            int failNum = 0;
            string objValue = string.Empty;

            DBDateTime now = FormatHelper.GetNowDBDateTime(this.DataProvider);
            string maintainUser = this.GetUserCode();

            for (int i = 0; i < items.Length; i++)
            {
                if (string.Compare(GetFieldValue(items[i], "ValidateResult"), "OK", true) == 0)
                {
                    string moCode = GetFieldValue(items[i], "MOCode");
                    MOFacade moFacade = new MOFacade(this.DataProvider);
                    object obj = moFacade.GetMO(moCode);
                    MO objMo = obj as MO;

                    /*---------------------------  CSV中数据(如果是空,则不更新对应的栏位) ---------------------------*/
                    objValue = GetFieldValue(items[i], "MOPlanQty1");
                    if (objValue != string.Empty)
                    {
                        objMo.MOPlanQty = decimal.Parse(objValue);  //MOPlanQty
                    }

                    objValue = GetFieldValue(items[i], "MOMemo1");
                    if (objValue != string.Empty)
                    {
                        objMo.MOMemo = objValue;  //MOMemo
                    }

                    objValue = GetFieldValue(items[i], "BOMVersion1");
                    if (objValue != string.Empty)
                    {
                        objMo.BOMVersion = objValue;  //BOMVersion
                    }

                    objValue = GetFieldValue(items[i], "MOPlanStartDate1");
                    if (objValue != string.Empty)
                    {
                        objMo.MOPlanStartDate = int.Parse(objValue); //MOPlanStartDate
                    }

                    objValue = GetFieldValue(items[i], "MOPlanStartTime");
                    if (objValue != string.Empty)
                    {
                        objMo.MOPlanStartTime = int.Parse(objValue);  //MOPlanStartTime
                    }

                    objValue = GetFieldValue(items[i], "MOPlanEndDate1");
                    if (objValue != string.Empty)
                    {
                        objMo.MOPlanEndDate = int.Parse(objValue);  //MOPlanEndDate
                    }

                    objValue = GetFieldValue(items[i], "MOPlanEndTime");
                    if (objValue != string.Empty)
                    {
                        objMo.MOPlanEndTime = int.Parse(objValue);  //MOPlanEndTime
                    }

                    objValue = GetFieldValue(items[i], "MOPlanLine");
                    if (objValue != string.Empty)
                    {
                        objMo.MOPlanLine = objValue;  //MOPlanLine 
                    }

                    objValue = GetFieldValue(items[i], "CustomerCode");
                    if (objValue != string.Empty)
                    {
                        objMo.CustomerCode = objValue;   //CustomerCode
                    }

                    objValue = GetFieldValue(items[i], "CustomerName");
                    if (objValue != string.Empty)
                    {
                        objMo.CustomerName = objValue;  //CustomerName
                    }

                    objValue = GetFieldValue(items[i], "CustomerOrderNO1");
                    if (objValue != string.Empty)
                    {
                        objMo.CustomerOrderNO = objValue;  //CustomerOrderNO
                    }

                    objValue = GetFieldValue(items[i], "CustomerItemCode");
                    if (objValue != string.Empty)
                    {
                        objMo.CustomerItemCode = objValue;  //CustomerItemCode
                    }

                    objValue = GetFieldValue(items[i], "OrderNO");
                    if (objValue != string.Empty)
                    {
                        objMo.OrderNO = objValue;  //OrderNO
                    }

                    objValue = GetFieldValue(items[i], "OrderSequence1");
                    if (objValue != string.Empty)
                    {
                        objMo.OrderSequence = decimal.Parse(objValue);  //OrderSequence
                    }

                    objValue = GetFieldValue(items[i], "EAttribute1");
                    if (objValue != string.Empty)
                    {
                        objMo.EAttribute1 = objValue;  //EAttribute1
                    }

                    objValue = GetFieldValue(items[i], "EAttribute4");
                    if (objValue != string.Empty)
                    {
                        objMo.EAttribute4 = objValue;  //EAttribute4
                    }

                    objValue = GetFieldValue(items[i], "EAttribute5");
                    if (objValue != string.Empty)
                    {
                        objMo.EAttribute5 = objValue;  //EAttribute5
                    }

                    objValue = GetFieldValue(items[i], "EAttribute6");
                    if (objValue != string.Empty)
                    {
                        objMo.EAttribute6 = objValue;  //EAttribute6
                    }

                    objMo.MaintainDate = now.DBDate;
                    objMo.MaintainTime = now.DBTime;
                    objMo.MaintainUser = maintainUser;

                    moFacade.UpdateMO(objMo, false);

                    successNum++;
                }
                else
                {
                    failNum++;
                }
            }

            //string strMessage = languageComponent1.GetString("导入完成: 成功" + successNum + "笔, 失败" + failNum + "笔");
            string strMessage = "导入完成: 成功" + successNum + "笔, 失败" + failNum + "笔";
            string alertInfo =
                string.Format("<script language=javascript>alert('{0}');</script>", strMessage);
            if (!this.IsClientScriptBlockRegistered("ImportAlert"))
            {
                this.RegisterClientScriptBlock("ImportAlert", alertInfo);
            }
            items = null;
            this.cmdEnter.Disabled = true;
        }



        protected void cmdReturn_ServerClick(object sender, EventArgs e)
        {
            this.Response.Redirect(this.MakeRedirectUrl("./FMOMP.aspx"));
        }

        private string GetFieldValue(object obj, string fieldName)
        {
            FieldInfo fi = obj.GetType().GetField(fieldName);
            if (fi == null)
                return string.Empty;
            return fi.GetValue(obj).ToString();
        }
        private void SetFieldValue(object obj, string fieldName, string value)
        {
            FieldInfo fi = obj.GetType().GetField(fieldName);
            if (fi != null)
                fi.SetValue(obj, value);
        }

        #endregion

    }
}
