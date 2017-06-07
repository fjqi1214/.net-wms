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
    public partial class FMATERIALMSLMP : BaseMPageNew
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
            this.gridHelper.AddColumn("MCODE", "物料代码", null);
            this.gridHelper.AddColumn("MName", "物料名称", null);
            this.gridHelper.AddColumn("MDes", "物料描述", null);
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
            row["MCODE"] = ((MaterialMSLExc)obj).MCode.ToString();
            row["MName"] = ((MaterialMSLExc)obj).MaterialName.ToString();
            row["MDes"] = ((MaterialMSLExc)obj).MaterialDes.ToString();
            row["MHumidityLevel"] = ((MaterialMSLExc)obj).MHumidityLevel.ToString();
            row["MHumidityLevelDesc"] = ((MaterialMSLExc)obj).MHumidityLevelDesc.ToString();
            row["FloorLife"] = ((MaterialMSLExc)obj).FloorLife.ToString();
            row["DryingTime"] = ((MaterialMSLExc)obj).DryingTime.ToString();
            row["INDryingTime"] = ((MaterialMSLExc)obj).InDryingTime.ToString();
            row["MaintainUser"] = ((MaterialMSLExc)obj).GetDisplayText("MaintainUser");
            row["MaintainDate"] = FormatHelper.ToDateString(((MaterialMSLExc)obj).MaintainDate);
            row["MaintainTime"] = FormatHelper.ToTimeString(((MaterialMSLExc)obj).MaintainTime);
            return row;
        }

        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (_InventoryFacade == null)
            {
                _InventoryFacade = new InventoryFacade(base.DataProvider);
            }

            return this._InventoryFacade.QueryMaterialMSL(
                    FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtConditionItem.Text)),
                    FormatHelper.CleanString(this.txtItemNQuery.Text),
                    FormatHelper.CleanString(this.txtMaterialDescQuery.Text),
                    FormatHelper.CleanString(this.txtMSDLevelQuery.Text),
                    FormatHelper.CleanString(this.txtMSDLevelDescQuery.Text),
                    inclusive, exclusive);
        }

        protected override int GetRowCount()
        {
            if (_InventoryFacade == null)
            {
                _InventoryFacade = new InventoryFacade(base.DataProvider);
            }
            return this._InventoryFacade.QueryMaterialMSLCount(
                    FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtConditionItem.Text)),
                    FormatHelper.CleanString(this.txtItemNQuery.Text),
                    FormatHelper.CleanString(this.txtMaterialDescQuery.Text),
                    FormatHelper.CleanString(this.txtMSDLevelQuery.Text),
                    FormatHelper.CleanString(this.txtMSDLevelDescQuery.Text));

        }

        #endregion

        #region Button

        protected override void AddDomainObject(object domainObject)
        {
            if (_InventoryFacade == null)
            {
                _InventoryFacade = new InventoryFacade(base.DataProvider);
            }

            object obj = _InventoryFacade.GetMaterialMSL(((MaterialMSL)domainObject).MCode, ((MaterialMSL)domainObject).OrgID);

            if (obj != null)
            {
                WebInfoPublish.Publish(this, "$BS_MCODE_MHumidityLevel_REPEATE", languageComponent1);
                return;
            }
            else
            {
                this._InventoryFacade.AddMaterialMSL((MaterialMSL)domainObject);
            }
        }

        protected override void DeleteDomainObjects(ArrayList domainObjects)
        {
            if (_InventoryFacade == null)
            {
                _InventoryFacade = new InventoryFacade(base.DataProvider);
            }

            this._InventoryFacade.DeleteMaterialMSL((MaterialMSL[])domainObjects.ToArray(typeof(MaterialMSL)));
        }

        protected override void UpdateDomainObject(object domainObject)
        {
            if (_InventoryFacade == null)
            {
                _InventoryFacade = new InventoryFacade(base.DataProvider);
            }
            this._InventoryFacade.UpdateMaterialMSL((MaterialMSL)domainObject);
        }

        protected override void buttonHelper_AfterPageStatusChangeHandle(string pageAction)
        {
            if (pageAction == PageActionType.Add)
            {
                this.txtItemIDEdit.Readonly = false;
                this.txtMSDLevelEdit.Readonly = false;
            }

            if (pageAction == PageActionType.Update)
            {
                this.txtItemIDEdit.Readonly = true;
                this.txtMSDLevelEdit.Readonly = true;
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

            MaterialMSL materialMSL = this._InventoryFacade.CreateMaterialMSL();

            materialMSL.MCode = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtItemIDEdit.Text, 40));
            materialMSL.MHumidityLevel = FormatHelper.CleanString(this.txtMSDLevelEdit.Text, 40);
            //materialMSL.InDryingTime = int.Parse(this.txtInDdyingTime.Text);
            materialMSL.MaintainUser = this.GetUserCode();
            materialMSL.OrgID = GlobalVariables.CurrentOrganizations.First().OrganizationID;

            return materialMSL;
        }

        protected override object GetEditObject(GridRecord row)
        {
            if (_InventoryFacade == null)
            {
                _InventoryFacade = new InventoryFacade(base.DataProvider);
            }

            int orgId = GlobalVariables.CurrentOrganizations.First().OrganizationID;

            object obj = _InventoryFacade.GetMaterialMSL(row.Items.FindItemByKey("MCODE").Text.ToString(), orgId);

            if (obj != null)
            {
                return (MaterialMSL)obj;
            }

            return null;
        }

        protected override void SetEditObject(object obj)
        {
            if (obj == null)
            {
                this.txtItemIDEdit.Text = string.Empty;
                this.txtMSDLevelEdit.Text = string.Empty;
                //this.txtInDdyingTime.Text = string.Empty;
                return;
            }

            this.txtItemIDEdit.Text = ((MaterialMSL)obj).MCode.ToString();
            this.txtMSDLevelEdit.Text = ((MaterialMSL)obj).MHumidityLevel.ToString();
            //this.txtInDdyingTime.Text = ((MaterialMSL)obj).InDryingTime.ToString();
        }

        protected override bool ValidateInput()
        {
            PageCheckManager manager = new PageCheckManager();

            manager.Add(new LengthCheck(this.lblItemIDEdit, this.txtItemIDEdit, 40, true));
            manager.Add(new LengthCheck(this.lblMSDLevelEdit, this.txtMSDLevelEdit, 40, true));
            //manager.Add(new NumberCheck(this.lblInDdyingTime, this.txtInDdyingTime, int.MinValue, int.MaxValue, true));

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
            return new string[]{((MaterialMSLExc)obj).MCode.ToString(),
                                 ((MaterialMSLExc)obj).MaterialName.ToString(),
                                 ((MaterialMSLExc)obj).MaterialDes.ToString(),
                                 ((MaterialMSLExc)obj).MHumidityLevel.ToString(),
                                 ((MaterialMSLExc)obj).MHumidityLevelDesc.ToString(),
                                 ((MaterialMSLExc)obj).FloorLife.ToString(),
                                 ((MaterialMSLExc)obj).DryingTime.ToString(),
                                 ((MaterialMSLExc)obj).InDryingTime.ToString(),
                                 ((MaterialMSLExc)obj).GetDisplayText("MaintainUser"),
                                 FormatHelper.ToDateString(((MaterialMSLExc)obj).MaintainDate),
                                 FormatHelper.ToTimeString(((MaterialMSLExc)obj).MaintainTime)};
        }

        protected override string[] GetColumnHeaderText()
        {
            return new string[] {	"MCode",
                                    "MaterialName",
                                    "MaterialDes",
                                    "MHumidityLevel",
                                    "MHumidityLevelDesc",
                                    "FloorLife",
                                    "DryingTime",
                                    "InDryingTime",
                                     "MaintainUser",
                                    "MaintainDate",	
                                    "MaintainTime"};
        }

        #endregion
    }
}
