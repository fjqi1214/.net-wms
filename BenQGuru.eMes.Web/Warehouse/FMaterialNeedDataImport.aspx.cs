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
using BenQGuru.eMES.Domain.Material;
using BenQGuru.eMES.Material;

namespace BenQGuru.eMES.Web.WarehouseWeb
{
    public partial class FMaterialNeedDataImport : BaseMPage
    {
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;

        protected System.Web.UI.HtmlControls.HtmlInputCheckBox Checkbox1;

        private BenQGuru.eMES.Material.MaterialFacade _Facade = null;//new BaseModelFacadeFactory().Create();
        private BenQGuru.eMES.MOModel.ItemFacade _ItemFacade = null;

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

      

        #region Import
        private object[] items;

        private object[] GetAllItem()
        {
            string fileName = string.Empty;
            fileName = this.ViewState["UploadedFileName"].ToString();

            string configFile = this.getParseConfigFileName();
            BenQGuru.eMES.Web.Helper.DataFileParser parser = new BenQGuru.eMES.Web.Helper.DataFileParser();
            parser.FormatName = "OutDataMO_Piece";
            parser.ConfigFile = configFile;
            items = parser.Parse(fileName);
            ValidateItems();
           
            return items;

        }

        private string getParseConfigFileName()
        {
            string configFile = this.Server.MapPath(this.TemplateSourceDirectory);
            if (configFile[configFile.Length - 1] != '\\')
            {
                configFile += "\\";
            }
            configFile += "ExportMaterialNeed.xml";
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
                object item = items[i];

                this._ItemFacade = new ItemFacade(this.DataProvider);           

                string itemCode = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(GetFieldValue(items[i], "ITEMCODE")));
                Int32 orgID = GlobalVariables.CurrentOrganizations.First().OrganizationID;
                object obj = _ItemFacade.GetMaterial(itemCode, orgID);
                if (obj == null)
                {
                    result = "物料代码不存在";
                }
                else
                {
                    this._Facade = new MaterialFacade(this.DataProvider);

                    object objExit = _Facade.GetMaterialReqStd(itemCode, orgID);

                    if (objExit == null)
                    {
                        result = "Insert";
                    }
                }

                string requestQty = GetFieldValue(items[i], "RequestQTY");
                if (requestQty != string.Empty)
                {
                    try
                    {
                        int numRequestQty = int.Parse(requestQty);
                    }
                    catch
                    {
                        result = string.Format("{0};数量必须是数字", result);
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

                SetFieldValue(item, "EAttribute1", result);
            }

        }

        protected void cmdImport_ServerClick(object sender, System.EventArgs e)
        {
            this._Facade = new MaterialFacade(this.DataProvider);
            string errorItemCode = string.Empty;
            string fileName = FileLoadProcess.UploadFile2ServerUploadFolder(this.Page, this.fileInit, null);
            if (fileName == null)
            {
                WebInfoPublish.Publish(this, "$Error_UploadFileIsEmpty", this.languageComponent1);
                return;

            }
            if (!fileName.ToLower().EndsWith(".csv"))
            {
                WebInfoPublish.Publish(this, "$Error_UploadFileTypeError", this.languageComponent1);
                return;
            }

            this.ViewState.Add("UploadedFileName", fileName);

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

          
            string orgID = GlobalVariables.CurrentOrganizations.First().OrganizationID.ToString();
          
            string mUser = GetUserCode();

            for (int i = 0; i < items.Length; i++)
            {
                string itemCode = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(GetFieldValue(items[i], "ITEMCODE")));
                string requestQty = FormatHelper.CleanString(GetFieldValue(items[i], "RequestQTY"));

                if (string.Compare(GetFieldValue(items[i], "EAttribute1"), "OK", true) == 0
                    || string.Compare(GetFieldValue(items[i], "EAttribute1"), "Insert", true) == 0)
                {                             
                    object obj = _Facade.GetMaterialReqStd(itemCode, int.Parse(orgID));                  
                    MaterialReqStd newMaterialReqStd = new MaterialReqStd();
                   if (string.Compare(GetFieldValue(items[i], "EAttribute1"), "Insert", true) == 0)
                    {
                        newMaterialReqStd.ItemCode = itemCode;
                        newMaterialReqStd.OrganizationID = int.Parse(orgID);
                        newMaterialReqStd.RequestQTY = int.Parse(requestQty);
                        newMaterialReqStd.MaintainUser = mUser;
                        _Facade.AddMaterialReqStd(newMaterialReqStd);
                    }
                    else
                    {
                        MaterialReqStd objMaterialReqStd = obj as MaterialReqStd;
                        objValue = requestQty.ToString();
                        if (objValue != string.Empty)
                        {
                            objMaterialReqStd.RequestQTY = int.Parse(objValue);
                        }
                        objMaterialReqStd.MaintainUser = mUser;
                        if (string.Compare(GetFieldValue(items[i], "EAttribute1"), "OK", true) == 0)
                        {
                            _Facade.UpdateMaterialReqStd(objMaterialReqStd);
                        }
                    }
                    successNum++;
                }
                else
                {    
                               
                    failNum++;
                    errorItemCode +=GetFieldValue(items[i], "ITEMCODE").ToString()+"\\n";;                   
                }
            }

            errorItemCode = "\\n导入失败的料号：\\n" + errorItemCode;

            string strMessage = "导入完成: 成功" + successNum + "笔, 失败" + failNum + "笔\\n" + errorItemCode;
            string alertInfo =
                string.Format("<script language=javascript>alert('{0}');</script>", strMessage);
            if (!this.IsClientScriptBlockRegistered("ImportAlert"))
            {
                this.RegisterClientScriptBlock("ImportAlert", alertInfo);
            }
            items = null;
        }



        protected void cmdReturn_ServerClick(object sender, EventArgs e)
        {
            this.Response.Redirect(this.MakeRedirectUrl("./FMaterialNeed.aspx"));
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
