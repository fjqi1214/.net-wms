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
using BenQGuru.eMES.Material;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Domain.Warehouse;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;


namespace BenQGuru.eMES.Web.WarehouseWeb
{
    public partial class FLocationMP : BaseMPageNew
    {

        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;

        protected System.Web.UI.HtmlControls.HtmlInputButton Submit1;

        private InventoryFacade facade = null;

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
                this.InitStorageList();
            }
        }

        protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
        {
            return this.languageComponent1;
        }

        //初始库位下拉框
        /// <summary>
        /// 初始化库位
        /// </summary>
        private void InitStorageList()
        {
            if (facade == null)
            {
                facade = new InventoryFacade(base.DataProvider);
            }
            object[] objStorage = facade.GetAllStorage();
            if (objStorage != null && objStorage.Length  > 0)
            {
                foreach (Storage storage in objStorage)
                {
                    this.drpStorageEdit.Items.Add(new ListItem(
                        string.Format("{0}—{1}",storage.StorageCode,storage.StorageName), 
                        storage.StorageCode)
                        );
                }
            }
            this.drpStorageEdit.SelectedIndex = 0;
        }
        #endregion

        #region WebGrid
        protected override void InitWebGrid()
        {
            base.InitWebGrid();
            this.gridHelper.AddColumn("LocationCode", "货位代码", null);
            this.gridHelper.AddColumn("LocationName", "货位名称", null);
            this.gridHelper.AddColumn("StorageCode", "库位代码", null);
            this.gridHelper.AddColumn("StorageName", "库位名称", null);
            this.gridHelper.AddColumn("MaintainUser", "维护人员", null);
            this.gridHelper.AddColumn("MaintainDate", "维护时间", null);

            this.gridHelper.AddDefaultColumn(true, true);

            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }

        protected override DataRow GetGridRow(object obj)
        {
            DataRow row = this.DtSource.NewRow();
            row["LocationCode"] = ((LocationWithStorageName)obj).LocationCode;
            row["LocationName"] = ((LocationWithStorageName)obj).LocationName;
            row["StorageCode"] = ((LocationWithStorageName)obj).StorageCode;
            row["StorageName"] = ((LocationWithStorageName)obj).StorageName;
            row["MaintainUser"] = ((LocationWithStorageName)obj).GetDisplayText("MaintainUser");
            row["MaintainDate"] = FormatHelper.ToDateString(((LocationWithStorageName)obj).MaintainDate);
            return row;
        }


        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (facade == null)
            {
                facade = new InventoryFacade(base.DataProvider);
            }
            return this.facade.QueryLocation(
                FormatHelper.CleanString(this.txtStorageCodeQuery.Text), 
                FormatHelper.CleanString(this.txtStorageNameQuery.Text),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtLocationCodeQuery.Text)),
                FormatHelper.CleanString(this.txtLocationNameQuery.Text), 
                GlobalVariables.CurrentOrganizations.First().OrganizationID, 
                inclusive, exclusive);
        }
        protected override int GetRowCount()
        {
            if (facade == null)
            {
                facade = new InventoryFacade(base.DataProvider);
            }
            return this.facade.QueryLocationCount(
                FormatHelper.CleanString(this.txtStorageCodeQuery.Text),
                FormatHelper.CleanString(this.txtStorageNameQuery.Text),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtLocationCodeQuery.Text)),
                FormatHelper.CleanString(this.txtLocationNameQuery.Text), 
                GlobalVariables.CurrentOrganizations.First().OrganizationID
            );
        }

        #endregion

        #region Button

        protected override void AddDomainObject(object domainObject)
        {
            if (facade == null)
            {
                facade = new InventoryFacade(base.DataProvider);
            }
            int orgId = GlobalVariables.CurrentOrganizations.First().OrganizationID;
            object objLocation = facade.GetLocation(this.txtLocationCodeEdit.Text, orgId);
            if (objLocation != null)
            {
                WebInfoPublish.Publish(this, "货位代码已存在", this.languageComponent1);
                return;
            }
            this.facade.AddLocation((Location)domainObject);
        }

        protected override void DeleteDomainObjects(ArrayList domainObjects)
        {
            if (facade == null)
            {
                facade = new InventoryFacade(base.DataProvider);
            } 
            //检查库存
            foreach (Location location in domainObjects.ToArray())
            {
                bool isUsed = facade.CheckLocationIsUsed(location.LocationCode);
                if (isUsed)
                {
                    WebInfoPublish.Publish(this, "货位已使用，不能删除", this.languageComponent1);
                    return;
                }
            }
          
            this.facade.DeleteLocation((Location[])domainObjects.ToArray(typeof(Location)));

        }

        protected override void UpdateDomainObject(object domainObject)
        {
            if (facade == null)
            {
                facade = new InventoryFacade(base.DataProvider);
            }
            this.facade.UpdateLocation((Location)domainObject);
        }

        protected override void buttonHelper_AfterPageStatusChangeHandle(string pageAction)
        {
            if (pageAction == PageActionType.Add)
            {
                this.drpStorageEdit.Enabled = true;
                this.txtLocationCodeEdit.Enabled = true;
            }

            if (pageAction == PageActionType.Update)
            {
                this.drpStorageEdit.Enabled = false;
                this.txtLocationCodeEdit.Enabled = false;
            }
        }

        #endregion

        #region Object <--> Page

        protected override object GetEditObject()
        {
            if (facade == null)
            {
                facade = new InventoryFacade(base.DataProvider);
            }

            Location location = this.facade.CreateNewLocation();

            location.LocationCode = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtLocationCodeEdit.Text, 40));
            location.LocationName = FormatHelper.CleanString(this.txtLocationNameEdit.Text, 100);
            location.StorageCode = FormatHelper.CleanString(this.drpStorageEdit.SelectedValue, 40);
            location.OrgID = GlobalVariables.CurrentOrganizations.First().OrganizationID;
            location.MaintainUser = this.GetUserCode();

            return location;
        }


        protected override object GetEditObject(GridRecord row)
        {
            if (facade == null)
            {
                facade = new InventoryFacade(base.DataProvider);
            }
            int orgId = GlobalVariables.CurrentOrganizations.First().OrganizationID;
            object obj = facade.GetLocation(row.Items.FindItemByKey("LocationCode").Value.ToString(), orgId);

            if (obj != null)
            {
                return (Location)obj;
            }

            return null;
        }

        protected override void SetEditObject(object obj)
        {
            if (obj == null)
            {
                this.drpStorageEdit.SelectedIndex = 0;
                this.txtLocationCodeEdit.Text = "";
                this.txtLocationNameEdit.Text = "";

                return;
            }
            this.drpStorageEdit.SelectedValue = ((Location)obj).StorageCode;
            this.txtLocationCodeEdit.Text = ((Location)obj).LocationCode;
            this.txtLocationNameEdit.Text = ((Location)obj).LocationName;
        }

        protected override bool ValidateInput()
        {
            if (facade == null)
            {
                facade = new InventoryFacade(base.DataProvider);
            }
            
            PageCheckManager manager = new PageCheckManager();
            manager.Add(new LengthCheck(this.lblStorageEdit, this.drpStorageEdit,100, true));
            manager.Add(new LengthCheck(this.lblLocationCodeEdit, this.txtLocationCodeEdit, 40, true));
            manager.Add(new LengthCheck(this.lblLocationNameEdit, this.txtLocationNameEdit, 100, true));

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
            return new string[]{((LocationWithStorageName)obj).LocationCode,
                                ((LocationWithStorageName)obj).LocationName,
                                ((LocationWithStorageName)obj).StorageCode,
                                ((LocationWithStorageName)obj).StorageName,
                                ((LocationWithStorageName)obj).MaintainUser,
                                FormatHelper.ToDateString(((LocationWithStorageName)obj).MaintainDate)}
                ;
        }

        protected override string[] GetColumnHeaderText()
        {
            return new string[] {	"LocationCode",
                                    "LocationName",   
                                    "StorageCode",
                                    "StorageName",
                                    "MaintainUser",
                                    "MaintainDate"};
        }

        #endregion
    }
}
