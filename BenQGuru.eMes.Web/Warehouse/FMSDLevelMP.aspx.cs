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

using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Domain.Warehouse;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.Web.Helper;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;

namespace BenQGuru.eMES.Web.WarehouseWeb
{
    public partial class FMSDLevelMP : BaseMPageNew
    {
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;


        protected System.Web.UI.HtmlControls.HtmlInputButton Submit1;

        private InventoryFacade _InventoryFacade = null;
        private SystemSettingFacade _SystemSettingFacade = null;


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
            _SystemSettingFacade = new SystemSettingFacade(this.DataProvider);

            if (!this.IsPostBack)
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
            base.InitWebGrid();
            this.gridHelper.AddColumn("MHumidityLevel", "湿敏等级", null);
            this.gridHelper.AddColumn("MHumidityLevelDesc", "湿敏等级描述", null);
            this.gridHelper.AddColumn("FloorLife", "有效车间寿命", null);
            this.gridHelper.AddColumn("DryingTime", "干燥箱最小干燥时间", null);
            this.gridHelper.AddColumn("INDryingTime", "暴露时间", null);
            this.gridHelper.AddColumn("MaintainUser", "维护用户", null);
            this.gridHelper.AddColumn("MaintainDate", "维护日期", null);
            this.gridHelper.AddColumn("MaintainTime", "维护时间", null);
            this.gridHelper.AddDefaultColumn(true, true);

            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }

        protected override DataRow GetGridRow(object obj)
        {
            DataRow row = DtSource.NewRow();
            row["MHumidityLevel"] = ((MSDLevel)obj).MHumidityLevel.ToString();
            row["MHumidityLevelDesc"] = ((MSDLevel)obj).MHumidityLevelDesc.ToString();
            row["FloorLife"] = ((MSDLevel)obj).FloorLife.ToString();
            row["DryingTime"] = ((MSDLevel)obj).DryingTime.ToString();
            row["INDryingTime"] = ((MSDLevel)obj).INDryingTime.ToString();
            row["MaintainUser"] = ((MSDLevel)obj).GetDisplayText("MaintainUser");
            row["MaintainDate"] = FormatHelper.ToDateString(((MSDLevel)obj).MaintainDate);
            row["MaintainTime"] = FormatHelper.ToTimeString(((MSDLevel)obj).MaintainTime);
            return row;
        }

        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (_InventoryFacade == null)
            {
                _InventoryFacade = new InventoryFacade(base.DataProvider);
            }

            return this._InventoryFacade.QueryMSDLevel(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtMSDLevelQuery.Text)), FormatHelper.CleanString(this.txtMSDLevelDescQuery.Text),
                inclusive, exclusive);

        }

        protected override int GetRowCount()
        {
            if (_InventoryFacade == null)
            {
                _InventoryFacade = new InventoryFacade(base.DataProvider);
            }
            return this._InventoryFacade.QueryMSDLevelCount(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtMSDLevelQuery.Text)), FormatHelper.CleanString(this.txtMSDLevelDescQuery.Text));

        }

        #endregion

        #region Button

        protected override void AddDomainObject(object domainObject)
        {
            if (_InventoryFacade == null)
            {
                _InventoryFacade = new InventoryFacade(base.DataProvider);
            }

            object obj = _InventoryFacade.GetMSDLevel(((MSDLevel)domainObject).MHumidityLevel);

            if (obj != null)
            {
                WebInfoPublish.Publish(this, "$BS_MHumidityLevel_REPEATE", languageComponent1);
                return;
            }


            this._InventoryFacade.AddMSDLevel((MSDLevel)domainObject);
        }

        protected override void DeleteDomainObjects(ArrayList domainObjects)
        {
            if (_InventoryFacade == null)
            {
                _InventoryFacade = new InventoryFacade(base.DataProvider);
            }

            this._InventoryFacade.DeleteMSDLevel((MSDLevel[])domainObjects.ToArray(typeof(MSDLevel)));
        }

        protected override void UpdateDomainObject(object domainObject)
        {
            if (_InventoryFacade == null)
            {
                _InventoryFacade = new InventoryFacade(base.DataProvider);
            }
            this._InventoryFacade.UpdateMSDLevel((MSDLevel)domainObject);
        }

        protected override void buttonHelper_AfterPageStatusChangeHandle(string pageAction)
        {
            if (pageAction == PageActionType.Add)
            {
                this.txtMSDLevelEdit.ReadOnly = false;
            }

            if (pageAction == PageActionType.Update)
            {
                this.txtMSDLevelEdit.ReadOnly = true;
            }
        }

        #endregion

        #region Object <--> Page

        protected override object GetEditObject()
        {
            if (_InventoryFacade == null)
            {
                _InventoryFacade = new InventoryFacade(base.DataProvider);
            }

            MSDLevel MSDLevel = this._InventoryFacade.CreateMSDLevel();

            MSDLevel.MHumidityLevel = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtMSDLevelEdit.Text, 40));
            MSDLevel.MHumidityLevelDesc = FormatHelper.CleanString(this.txtMSDLevelDescEdit.Text, 100);
            MSDLevel.FloorLife = int.Parse(this.txtFloorLifeEdit.Text);
            MSDLevel.DryingTime = int.Parse(this.txtDryingTimeEdit.Text);
            MSDLevel.INDryingTime = int.Parse(this.txtINDryingTimeEdit.Text);
            MSDLevel.MaintainUser = this.GetUserCode();

            return MSDLevel;
        }

        protected override object GetEditObject(GridRecord row)
        {
            if (_InventoryFacade == null)
            {
                _InventoryFacade = new InventoryFacade(base.DataProvider);
            }

            int orgId = GlobalVariables.CurrentOrganizations.First().OrganizationID;

            object obj = _InventoryFacade.GetMSDLevel(row.Items.FindItemByKey("MHumidityLevel").Value.ToString());

            if (obj != null)
            {
                return (MSDLevel)obj;
            }

            return null;
        }

        protected override void SetEditObject(object obj)
        {
            if (obj == null)
            {
                this.txtMSDLevelEdit.Text = string.Empty;
                this.txtMSDLevelDescEdit.Text = string.Empty;
                this.txtFloorLifeEdit.Text = string.Empty;
                this.txtDryingTimeEdit.Text = string.Empty;
                this.txtINDryingTimeEdit.Text = string.Empty;
                return;
            }

            this.txtMSDLevelEdit.Text = ((MSDLevel)obj).MHumidityLevel.ToString();
            this.txtMSDLevelDescEdit.Text = ((MSDLevel)obj).MHumidityLevelDesc.ToString();
            this.txtFloorLifeEdit.Text = ((MSDLevel)obj).FloorLife.ToString();
            this.txtDryingTimeEdit.Text = ((MSDLevel)obj).DryingTime.ToString();
            this.txtINDryingTimeEdit.Text = ((MSDLevel)obj).INDryingTime.ToString();
        }

        protected override bool ValidateInput()
        {
            PageCheckManager manager = new PageCheckManager();

            manager.Add(new LengthCheck(this.lblMSDLevelEdit, this.txtMSDLevelEdit, 40, true));
            manager.Add(new LengthCheck(this.lblMSDLevelDescEdit, this.txtMSDLevelDescEdit, 40, false));
            manager.Add(new NumberCheck(this.lblFloorLifeEdit, this.txtFloorLifeEdit, int.MinValue, int.MaxValue, true));
            manager.Add(new NumberCheck(this.lblDryingTimeEdit, this.txtDryingTimeEdit, int.MinValue, int.MaxValue, true));
            manager.Add(new NumberCheck(this.lblINDryingTimeEdit, this.txtINDryingTimeEdit, int.MinValue, int.MaxValue, true));

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
            return new string[]{((MSDLevel)obj).MHumidityLevel.ToString(),
                                ((MSDLevel)obj).MHumidityLevelDesc.ToString(),
                                ((MSDLevel)obj).FloorLife.ToString(),
                                ((MSDLevel)obj).DryingTime.ToString(),
                                ((MSDLevel)obj).INDryingTime.ToString(),
                                ((MSDLevel)obj).GetDisplayText("MaintainUser"),
                                FormatHelper.ToDateString(((MSDLevel)obj).MaintainDate),
                                FormatHelper.ToTimeString(((MSDLevel)obj).MaintainTime)};
        }

        protected override string[] GetColumnHeaderText()
        {
            return new string[] {	"MHumidityLevel",
                                    "MHumidityLevelDesc",
                                    "FloorLife",
                                    "DryingTime",
                                    "INDryingTime",
                                    "MaintainUser",
                                    "MaintainDate",	
                                    "MaintainTime"};
        }

        #endregion
    }
}
