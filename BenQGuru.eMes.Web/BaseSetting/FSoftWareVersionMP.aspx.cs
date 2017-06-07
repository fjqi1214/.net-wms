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
using BenQGuru.eMES.Domain.BaseSetting;
using System.Reflection;
using System.Xml;
using BenQGuru.eMES.Common;
using System.Globalization;
using System.Text;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;

namespace BenQGuru.eMES.Web.BaseSetting
{
    public partial class FSoftWareVersionMP : BaseMPageNew
    {

        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;
        protected System.Web.UI.WebControls.Label lblResourceTitle;

        private BaseModelFacade facade = null;

        //Import CSV file
        private object[] items;


        #region Web 窗体设计器生成的代码
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
        #endregion

        #region Init
        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!this.IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);

                RadioButtonListBuilder builder = new RadioButtonListBuilder(
                    new SoftWareVersionStatus(), this.rblSoftWareStatusEdit, this.languageComponent1);

                builder.Build();

                this.trImport.Visible = false;
            }
            RadioButtonListBuilder.FormatListControlStyle(this.rblSoftWareStatusEdit, 80);
        }

        protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
        {
            return this.languageComponent1;
        }

        protected void drpSoftWareStatus_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {
                this.drpSoftWareStatusQuery.Items.Insert(0, new ListItem("", ""));

                this.drpSoftWareStatusQuery.Items.Insert(1, new ListItem(languageComponent1.GetString("type_valid"), SoftWareVersionStatus.Valid));

                this.drpSoftWareStatusQuery.Items.Insert(2, new ListItem(languageComponent1.GetString("type_invalid"), SoftWareVersionStatus.InValid));
            }
        }
        #endregion

        #region WebGrid
        protected override void InitWebGrid()
        {
            base.InitWebGrid();
            this.gridHelper.AddColumn("VersionCode", "软件版本", null);
            this.gridHelper.AddColumn("Status", "状态", null);
            this.gridHelper.AddColumn("EffectiveDate", "生效日期", null);
            this.gridHelper.AddColumn("InvalidDate", "失效日期", null);
            this.gridHelper.AddColumn("MaintainUser", "维护人员", null);

            this.gridHelper.AddDefaultColumn(true, true);

            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);

            #region Import CSV File and DownLoad File

            this.aFileDownLoad.HRef = string.Format(@"{0}download\{1}.csv", this.VirtualHostRoot, "SoftWareVersion");
            
            /*
            string xmlPath = Server.MapPath("SoftWareVersionImport.xml");
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlPath);
            XmlNode nodeFormat = xmlDoc.SelectSingleNode("//Format[@Name='OutDataSoftWareVersion_Piece']");
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
            */
            #endregion
            
        }

        protected override DataRow GetGridRow(object obj)
        {             
            DataRow row = this.DtSource.NewRow();
            row["VersionCode"] = ((SoftWareVersion)obj).VersionCode.ToString();
            row["Status"] = languageComponent1.GetString(((SoftWareVersion)obj).Status);
            row["EffectiveDate"] =  FormatHelper.ToDateString(((SoftWareVersion)obj).EffectiveDate);
            row["InvalidDate"] =  FormatHelper.ToDateString(((SoftWareVersion)obj).InvalidDate);
            row["MaintainUser"] =((SoftWareVersion)obj).GetDisplayText("MaintainUser");
            return row;
        }


        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (facade == null)
            {
                facade = new BaseModelFacade(this.DataProvider);
            }
            return this.facade.QuerySoftWareVersion(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtSoftWareVersionQuery.Text)), FormatHelper.CleanString(this.drpSoftWareStatusQuery.SelectedValue.ToString()),
                inclusive, exclusive);
        }


        protected override int GetRowCount()
        {
            if (facade == null)
            {
                facade = new BaseModelFacade(this.DataProvider);
            }
            return this.facade.QuerySoftWareVersionCount(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtSoftWareVersionQuery.Text)), FormatHelper.CleanString(this.drpSoftWareStatusQuery.SelectedValue.ToString())
            );
        }

        #endregion

        #region Button

        protected override void AddDomainObject(object domainObject)
        {
            if (facade == null)
            {
                facade = new BaseModelFacade(this.DataProvider);
            }
            this.facade.AddSoftWareVersion((SoftWareVersion)domainObject);
        }

        protected override void DeleteDomainObjects(ArrayList domainObjects)
        {
            if (facade == null)
            {
                facade = new BaseModelFacade(this.DataProvider);
            }
            this.facade.DeleteSoftWareVersion((SoftWareVersion[])domainObjects.ToArray(typeof(SoftWareVersion)));
        }

        protected override void UpdateDomainObject(object domainObject)
        {
            if (facade == null)
            {
                facade = new BaseModelFacade(this.DataProvider);
            }
            this.facade.UpdateSoftWareVersion((SoftWareVersion)domainObject);
        }


        protected override void buttonHelper_AfterPageStatusChangeHandle(string pageAction)
        {
            if (pageAction == PageActionType.Add)
            {
                this.dateEffectiveDateEdit.Text = FormatHelper.ToDateString(FormatHelper.TODateInt(System.DateTime.Now));
                this.dateInvalidDateEdit.Text = "2099-12-31";
                this.txtSoftWareVersionEdit.ReadOnly = false;
            }

            if (pageAction == PageActionType.Update)
            {
                this.txtSoftWareVersionEdit.ReadOnly = true;
            }
        }

        #endregion

        #region Object <--> Page

        protected override object GetEditObject()
        {
            if (facade == null)
            {
                facade = new BaseModelFacade(this.DataProvider);
            }
            SoftWareVersion softWareVersion = this.facade.CreateNewSoftWareVersion();

            softWareVersion.VersionCode = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtSoftWareVersionEdit.Text, 40));
            softWareVersion.Status = FormatHelper.CleanString(this.rblSoftWareStatusEdit.SelectedValue);
            softWareVersion.EffectiveDate = FormatHelper.TODateInt(this.dateEffectiveDateEdit.Text);
            softWareVersion.InvalidDate = FormatHelper.TODateInt(this.dateInvalidDateEdit.Text);
            softWareVersion.MaintainUser = base.GetUserCode();
            softWareVersion.OrganizationID = GlobalVariables.CurrentOrganizations.First().OrganizationID;

            return softWareVersion;
        }


        protected override object GetEditObject(GridRecord row)
        {
            if (facade == null)
            {
                facade = new BaseModelFacade(base.DataProvider);
            }
            string strCode = string.Empty;
            object objCode = row.Items.FindItemByKey("VersionCode").Value;
            if (objCode != null)
            {
                strCode = objCode.ToString();
            }
            object obj = facade.GetSoftWareVersion(strCode);
            if (obj != null)
            {
                return (SoftWareVersion)obj;
            }
            return null;
        }

        protected override void SetEditObject(object obj)
        {
            if (obj == null)
            {
                this.txtSoftWareVersionEdit.Text = "";

                this.rblSoftWareStatusEdit.SelectedIndex = 0;

                return;
            }

            this.txtSoftWareVersionEdit.Text = ((SoftWareVersion)obj).VersionCode.ToString();
            if (((SoftWareVersion)obj).Status == SoftWareVersionStatus.Valid)
            {
                this.rblSoftWareStatusEdit.SelectedIndex = 0;
            }
            else
            {
                this.rblSoftWareStatusEdit.SelectedIndex = 1;
            }

            this.dateEffectiveDateEdit.Text = FormatHelper.ToDateString(((SoftWareVersion)obj).EffectiveDate);
            this.dateInvalidDateEdit.Text = FormatHelper.ToDateString(((SoftWareVersion)obj).InvalidDate);

        }

        protected override bool ValidateInput()
        {
            PageCheckManager manager = new PageCheckManager();

            manager.Add(new LengthCheck(this.lblSoftWareVersionEdit, this.txtSoftWareVersionEdit, 40, true));
            manager.Add(new DateCheck(this.lblEffectiveDateEdit, this.dateEffectiveDateEdit.Text, true));
            manager.Add(new DateCheck(this.lblInvalidDateEdit, this.dateInvalidDateEdit.Text, true));

            if (!manager.Check())
            {
                WebInfoPublish.Publish(this, manager.CheckMessage, this.languageComponent1);
                //throw new Exception(manager.CheckMessage);
                return false;
            }

            return true;
        }

        #endregion

        #region Export

        protected override string[] FormatExportRecord(object obj)
        {
            return new string[]{  
                                ((SoftWareVersion)obj).VersionCode.ToString(),
                                languageComponent1.GetString(((SoftWareVersion)obj).Status),
                                FormatHelper.ToDateString(((SoftWareVersion)obj).EffectiveDate),
                                FormatHelper.ToDateString(((SoftWareVersion)obj).InvalidDate),
                               ((SoftWareVersion)obj).GetDisplayText("MaintainUser")
            
            };
        }

        protected override string[] GetColumnHeaderText()
        {
            return new string[] {	"VersionCode",
                                    "Status",
                                    "EffectiveDate",	
                                    "InvalidDate",
                                    "MaintainUser"};
        }

        #endregion

        #region Import CSV File
        protected void cmdImport_ServerClick(object sender, EventArgs e)
        {
            string fileName = FileLoadProcess.UploadFile2ServerUploadFolder(this.Page, this.fileInit, null);
            if (fileName == null)
            {
                BenQGuru.eMES.Common.ExceptionManager.Raise(this.GetType().BaseType, "$Error_UploadFileIsEmpty");
            }
            if (!fileName.ToLower().EndsWith(".csv"))
            {
                BenQGuru.eMES.Common.ExceptionManager.Raise(this.GetType().BaseType, "$Error_UploadFileTypeError");
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
                        throw new Exception("软件版本不能为空");
                    }
                    else
                    {
                        object obj = facade.GetSoftWareVersion(versionCode);

                        if (obj != null)
                        {
                            throw new Exception(string.Format("软件版本:{0} 已经存在", versionCode));
                        }
                    }

                    if (Encoding.Default.GetByteCount(versionCode) > 40)
                    {
                        throw new Exception("软件版本长度超出范围，最大长度40字节");
                    }

                    string effectiveDate = GetFieldValue(items[i], "EffectiveDate");
                    if (effectiveDate == string.Empty)
                    {
                        throw new Exception("生效日期不能为空");
                    }
                    else
                    {
                        try
                        {
                            DateTime endDate = DateTime.ParseExact(effectiveDate, "yyyyMMdd", culture);
                        }
                        catch
                        {
                            throw new Exception("生效日期格式不正确");
                        }
                    }

                    string invalidDate = GetFieldValue(items[i], "InvalidDate");
                    if (invalidDate == string.Empty)
                    {
                        throw new Exception("失效日期不能为空");
                    }
                    else
                    {
                        try
                        {
                            DateTime endDate = DateTime.ParseExact(invalidDate, "yyyyMMdd", culture);
                        }
                        catch
                        {
                            throw new Exception("失效日期格式不正确");
                        }
                    }

                    #endregion
                    
                    softWareVersion.VersionCode = versionCode;
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
                string alertInfo =
                    string.Format("<script language=javascript>alert('{0}');</script>", strMessage);
                if (!this.IsClientScriptBlockRegistered("ImportAlert"))
                {
                    this.RegisterClientScriptBlock("ImportAlert", alertInfo);
                }

                items = null;

                this.gridHelper.RequestData();
                this.buttonHelper.PageActionStatusHandle(PageActionType.Query);
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                ExceptionManager.Raise(this.GetType().BaseType, ex.Message);
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
