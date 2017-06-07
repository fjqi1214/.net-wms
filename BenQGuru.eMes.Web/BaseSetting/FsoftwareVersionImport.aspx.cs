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
using BenQGuru.eMES.BaseSetting;
using System.Globalization;
using BenQGuru.eMES.Domain.BaseSetting;
using System.Text;
using BenQGuru.eMES.Common;
using System.Reflection;

namespace BenQGuru.eMES.Web.BaseSetting
{
    public partial class FsoftwareVersionImport : BaseMPageMinus
    {
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;

        private BaseModelFacade facade = null;

        //Import CSV file
        private object[] items;
        protected UpdatePanel UpdatePanel1;

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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);

                this.aFileDownLoad.HRef = string.Format(@"{0}download\{1}.csv", this.VirtualHostRoot, "SoftWareVersion");
            }
        }

        #region Import CSV File
        protected void cmdImport_ServerClick(object sender, EventArgs e)
        {
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

            if (items == null)
            {
                items = GetAllItem(fileName);
                if (items == null)
                    return;
            }
            if (items == null || items.Length == 0)
                return;

            int successNum = 0;
            CultureInfo culture = new System.Globalization.CultureInfo("en-US");
            string userCode = this.GetUserCode();

            if (facade == null)
            {
                facade = new BaseModelFacade(this.DataProvider);
            }

            SoftWareVersion softWareVersion = this.facade.CreateNewSoftWareVersion();

            ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.AutoCloseConnection = false;
            ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.OpenConnection();
            DataProvider.BeginTransaction();
            try
            {
                for (int i = 0; i < items.Length; i++)
                {
                    #region Check

                    string versionCode = GetFieldValue(items[i], "VersionCode");
                    if (versionCode == string.Empty)
                    {
                        WebInfoPublish.Publish(this, "软件版本不能为空", this.languageComponent1);
                        return;
                    }
                    else
                    {
                        object obj = facade.GetSoftWareVersion(versionCode);

                        if (obj != null)
                        {
                            WebInfoPublish.Publish(this, string.Format("软件版本:{0} 已经存在", versionCode), this.languageComponent1);
                            return;
                        }
                    }

                    if (Encoding.Default.GetByteCount(versionCode) > 40)
                    {
                        WebInfoPublish.Publish(this, "软件版本长度超出范围，最大长度40字节", this.languageComponent1);
                        return;
                    }

                    string effectiveDate = GetFieldValue(items[i], "EffectiveDate");
                    if (effectiveDate == string.Empty)
                    {
                        WebInfoPublish.Publish(this, "生效日期不能为空", this.languageComponent1);
                        return;
                    }
                    else
                    {
                        try
                        {
                            DateTime endDate = DateTime.ParseExact(effectiveDate, "yyyyMMdd", culture);
                        }
                        catch
                        {
                            WebInfoPublish.Publish(this, "生效日期格式不正确", this.languageComponent1);
                            return;
                        }
                    }

                    string invalidDate = GetFieldValue(items[i], "InvalidDate");
                    if (invalidDate == string.Empty)
                    {
                        WebInfoPublish.Publish(this, "失效日期不能为空", this.languageComponent1);
                        return;
                    }
                    else
                    {
                        try
                        {
                            DateTime endDate = DateTime.ParseExact(invalidDate, "yyyyMMdd", culture);
                        }
                        catch
                        {
                            WebInfoPublish.Publish(this, "失效日期格式不正确", this.languageComponent1);
                            return;
                        }
                    }

                    #endregion

                    softWareVersion.VersionCode = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(versionCode));
                    softWareVersion.Status = SoftWareVersionStatus.Valid;
                    softWareVersion.EffectiveDate = int.Parse(effectiveDate);
                    softWareVersion.InvalidDate = int.Parse(invalidDate);
                    softWareVersion.MaintainUser = userCode;
                    softWareVersion.OrganizationID = GlobalVariables.CurrentOrganizations.First().OrganizationID;

                    this.facade.AddSoftWareVersion(softWareVersion);

                    successNum++;
                }

                this.DataProvider.CommitTransaction();

                string strMessage = "导入:" + successNum + "笔";
                WebInfoPublish.Publish(this, strMessage, this.languageComponent1);
                items = null;
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                WebInfoPublish.Publish(this, ex.Message, this.languageComponent1);
                return;
            }
            finally
            {
                ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
                ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.AutoCloseConnection = true;
            }
        }

        private string getParseConfigFileName()
        {
            string configFile = this.Server.MapPath(this.TemplateSourceDirectory);
            if (configFile[configFile.Length - 1] != '\\')
            {
                configFile += "\\";
            }
            configFile += "SoftWareVersionImport.xml";
            return configFile;
        }

        private object[] GetAllItem(string fileName)
        {
            try
            {
                string configFile = this.getParseConfigFileName();

                BenQGuru.eMES.Web.Helper.DataFileParser parser = new BenQGuru.eMES.Web.Helper.DataFileParser();
                parser.FormatName = "OutDataSoftWareVersion_Piece";
                parser.ConfigFile = configFile;
                items = parser.Parse(fileName);
            }
            catch
            {

            }

            return items;
        }

        private string GetFieldValue(object obj, string fieldName)
        {
            FieldInfo fi = obj.GetType().GetField(fieldName);
            if (fi == null)
                return string.Empty;
            return fi.GetValue(obj).ToString();
        }
        #endregion
    }
}
