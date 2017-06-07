using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Domain.Warehouse;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Web.Helper;
using Infragistics.WebUI.UltraWebGrid;
using BenQGuru.eMES.BaseSetting;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;

namespace BenQGuru.eMES.Web.WarehouseWeb
{
    /// <summary>
    /// FFactory 的摘要说明。
    /// </summary>
    public partial class FFactoryMP : BaseMPageNew
    {
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;
        //protected BenQGuru.eMES.Web.Helper.ExcelExporter excelExporter;

        //private ButtonHelper buttonHelper = null;
        private WarehouseFacade _facade;//= new WarehouseFacade(base.DataProvider);


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
                BuildOrgList();
                
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
            base.InitWebGrid();
            this.gridHelper.AddColumn("FactoryCode", "工厂代码", null);
            this.gridHelper.AddColumn("FactoryDescription", "工厂说明", null);
            this.gridHelper.AddColumn("MaintainUser", "维护用户", null);
            this.gridHelper.AddColumn("MaintainDate", "维护日期", null);
            this.gridHelper.AddColumn("MaintainTime", "维护时间", null);
            this.gridHelper.AddDefaultColumn(true, true);
            this.gridHelper.ApplyLanguage(this.languageComponent1);

            if (!IsPostBack)
            {
                this.cmdQuery_Click(null, null);
            }
        }


        protected override DataRow GetGridRow(object obj)
        {
            DataRow row = this.DtSource.NewRow();
            row["FactoryCode"] = (obj as Factory).FactoryCode.ToString();
            row["FactoryDescription"] = (obj as Factory).FactoryDescription.ToString();
            row["MaintainUser"] = (obj as Factory).GetDisplayText("MaintainUser");
            row["MaintainDate"] = FormatHelper.ToDateString((obj as Factory).MaintainDate);
            row["MaintainTime"] = FormatHelper.ToTimeString((obj as Factory).MaintainTime);
            return row;
        }


        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (_facade == null) { _facade = new WarehouseFacade(base.DataProvider); }
            return this._facade.QueryFactory(
                string.Empty,
                inclusive, exclusive);
        }

        protected override int GetRowCount()
        {
            if (_facade == null) { _facade = new WarehouseFacade(base.DataProvider); }
            return this._facade.QueryFactoryCount(string.Empty);
        }

        #endregion

        #region Button
        protected override void AddDomainObject(object domainObject)
        {
            if (_facade == null) { _facade = new WarehouseFacade(base.DataProvider); }
            this._facade.AddFactory((Factory)domainObject);
        }

        protected override void DeleteDomainObjects(ArrayList domainObjects)
        {
            if (_facade == null) { _facade = new WarehouseFacade(base.DataProvider); }
            this._facade.DeleteFactory((Factory[])domainObjects.ToArray(typeof(Factory)));
        }

        protected override void UpdateDomainObject(object domainObject)
        {
            if (_facade == null) { _facade = new WarehouseFacade(base.DataProvider); }
            this._facade.UpdateFactory((Factory)domainObject);
        }

        protected override void buttonHelper_AfterPageStatusChangeHandle(string pageAction)
        {
            if (pageAction == PageActionType.Add)
            {
                this.txtFactoryCodeEdit.ReadOnly = false;
                this.DropDownListOrg.Enabled = true;
            }

            if (pageAction == PageActionType.Update)
            {
                this.txtFactoryCodeEdit.ReadOnly = true;
                this.DropDownListOrg.Enabled = false;
            }
        }
        #endregion

        #region Object <--> Page
        protected override object GetEditObject()
        {
            if (_facade == null) { _facade = new WarehouseFacade(base.DataProvider); }
            Factory factory = this._facade.CreateNewFactory();
            factory.FactoryDescription = FormatHelper.CleanString(this.txtFactoryDescriptionEdit.Text, 100);
            factory.FactoryCode = FormatHelper.CleanString(this.txtFactoryCodeEdit.Text, 40);
            factory.OrganizationID = int.Parse(this.DropDownListOrg.SelectedValue);
            factory.MaintainUser = this.GetUserCode();

            return factory;
        }

        protected override object GetEditObject(GridRecord row)
        {
            if (_facade == null) { _facade = new WarehouseFacade(base.DataProvider); }
            object objCode = row.Items.FindItemByKey("FactoryCode").Value;
            string routeCode = string.Empty;
            if (objCode != null)
            {
                routeCode = objCode.ToString();
            }
            object obj = _facade.GetFactory(routeCode);
            if (obj != null)
            {
                return (Factory)obj;
            }
            return null;
        }

        protected override void SetEditObject(object obj)
        {
            if (obj == null)
            {
                this.txtFactoryDescriptionEdit.Text = "";
                this.txtFactoryCodeEdit.Text = "";
                this.DropDownListOrg.SelectedIndex = 0;
                return;
            }

            Factory factory = (Factory)obj;
            this.txtFactoryDescriptionEdit.Text = factory.FactoryDescription.ToString();
            this.txtFactoryCodeEdit.Text = factory.FactoryCode.ToString();

            try
            {
                this.DropDownListOrg.SelectedValue = factory.OrganizationID.ToString();
            }
            catch
            {
                this.DropDownListOrg.SelectedIndex = 0;
            }

            factory = null;
        }


        protected override bool ValidateInput()
        {
            PageCheckManager manager = new PageCheckManager();

            manager.Add(new LengthCheck(lblFactoryCodeEdit, txtFactoryCodeEdit, 40, true));
            manager.Add(new LengthCheck(lblFactoryDescriptionEdit, txtFactoryDescriptionEdit, 100, true));
            manager.Add(new LengthCheck(lblOrgEdit, DropDownListOrg, 8, true));

            if (!manager.Check())
            {
                WebInfoPublish.Publish(this, manager.CheckMessage, this.languageComponent1);

                return false;
            }

            return true;
        }

        #endregion

        #region Export
        protected override string[] FormatExportRecord(object obj)
        {
            Factory factory = (Factory)obj;
            string[] strArr =
                new string[]{  factory.FactoryCode.ToString(),
								   factory.FactoryDescription.ToString(),
								   factory.GetDisplayText("MaintainUser"),
								   FormatHelper.ToDateString(factory.MaintainDate),
								   FormatHelper.ToTimeString(factory.MaintainTime)};
            factory = null;
            return strArr;
        }

        protected override string[] GetColumnHeaderText()
        {
            return new string[] {	
									"FactoryCode",
									"FactoryDescription",
									"MaintainUser",
									"MaintainDate",
									"MaintainTime"};
        }
        #endregion

        #region 数据初始化

        private void BuildOrgList()
        {
            DropDownListBuilder builder = new DropDownListBuilder(this.DropDownListOrg);
            builder.HandleGetObjectList = new GetObjectListDelegate(this.GetAllOrg);
            builder.Build("OrganizationDescription", "OrganizationID");
            this.DropDownListOrg.Items.Insert(0, new ListItem("", ""));

            this.DropDownListOrg.SelectedIndex = 0;
        }
        private object[] GetAllOrg()
        {
            BaseModelFacade facadeBaseModel = new BaseModelFacade(base.DataProvider);
            return facadeBaseModel.GetCurrentOrgList();
        }
        #endregion
    }
}
