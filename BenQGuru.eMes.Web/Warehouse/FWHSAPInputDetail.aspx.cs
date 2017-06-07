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
    public partial class FWHSAPInputDetail : BaseMPageNew
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
            this.gridHelper.AddColumn("sapNO", "SAP单据号", null);
            this.gridHelper.AddColumn("inputType", "入库类型", null);
            this.gridHelper.AddColumn("Factory", "工厂", null);
            this.gridHelper.AddColumn("StockOrientation ", "库位", null);
            this.gridHelper.AddColumn("ItemNo", "Item号", null);
            this.gridHelper.AddColumn("sapState", "状态", null);
            this.gridHelper.AddColumn("SAPMaterialNO", "SAP物料编码", null);
            this.gridHelper.AddColumn("DQMaterialNo", "鼎桥物料编码", null);
            this.gridHelper.AddColumn("DQMaterialNODesc", "鼎桥物料编码描述", null);
            this.gridHelper.AddColumn("MUOM", "单位", null);
            this.gridHelper.AddColumn("OrderQTY", "订单数量", null);
            this.gridHelper.AddColumn("StorageQTY", "已入库数量", null);
            this.gridHelper.AddColumn("MaintainUser", "最后维护用户", null);
            this.gridHelper.AddColumn("MaintainDate", "最后维护日期", null);

            this.gridWebGrid.Columns.FromKey("MaintainUser").Hidden = true;
            this.gridWebGrid.Columns.FromKey("MaintainDate").Hidden = true;

            this.gridHelper.AddDefaultColumn(true, true);

            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }

        protected override DataRow GetGridRow(object obj)
        {

            DataRow row = this.DtSource.NewRow();
            row["sapNO"] = ((AsndetailEX)obj).Orderno;
            row["inputType"] = ((AsndetailEX)obj).STTYPE;
            row["Factory"] = ((AsndetailEX)obj).FACCODE;
            row["StockOrientation"] = ((AsndetailEX)obj).StorageCode;
            row["ItemNo"] = ((AsndetailEX)obj).Orderline;
            row["sapState"] = this.languageComponent1.GetString(((AsndetailEX)obj).Status);
            row["SAPMaterialNO"] = ((AsndetailEX)obj).MCode;
            row["DQMaterialNo"] = ((AsndetailEX)obj).DqmCode;
            row["DQMaterialNODesc"] = ((AsndetailEX)obj).MCHLdesc;           
            row["MUOM"] = ((AsndetailEX)obj).Unit;
            row["OrderQTY"] = ((AsndetailEX)obj).Qty.ToString();
            row["StorageQTY"] = ((AsndetailEX)obj).ActQty.ToString();
            row["MaintainUser"] = ((AsndetailEX)obj).GetDisplayText("MaintainUser");
            row["MaintainDate"] = FormatHelper.ToDateString(((AsndetailEX)obj).MaintainDate);
            return row;
        }


        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            //if (facade == null)
            //{
            //    facade = new WarehouseFacade(base.DataProvider);
            //}
            //return this.facade.QueryStack(
            //    FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtStorageCodeQuery.Text)), 
            //    FormatHelper.CleanString(this.txtStorageNameQuery.Text),
            //    FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtLocationCodeQuery.Text)),
            //    FormatHelper.CleanString(this.txtLocationNameQuery.Text), 
            //    GlobalVariables.CurrentOrganizations.First().OrganizationID, 
            //    inclusive, exclusive);

            return null;
        }
        protected override int GetRowCount()
        {
            //if (facade == null)
            //{
            //    facade = new InventoryFacade(base.DataProvider);
            //}
            //return this.facade.QueryStackCount(
            //    FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtStorageCodeQuery.Text)),
            //    FormatHelper.CleanString(this.txtStorageNameQuery.Text),
            //    FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtLocationCodeQuery.Text)),
            //    FormatHelper.CleanString(this.txtLocationNameQuery.Text),
            //    GlobalVariables.CurrentOrganizations.First().OrganizationID
            //);

            return 0;
        }

        #endregion

       

        #region Object <--> Page

        protected override object GetEditObject()
        {
            if (facade == null)
            {
                facade = new InventoryFacade(base.DataProvider);
            }

            SStack stack = this.facade.CreateSStack();

            //stack.StackCode = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtLocationCodeEdit.Text, 40));
            //stack.StackDesc = FormatHelper.CleanString(this.txtLocationNameEdit.Text, 100);

            //stack.OrgID = GlobalVariables.CurrentOrganizations.First().OrganizationID;
            //object[] storageList =facade.QueryStorage(stack.StorageCode);
            //if (storageList != null)
            //{
            //    stack.OrgID = ((Storage)storageList[0]).OrgID; 
            //}
            
            //stack.MaintainUser = this.GetUserCode();

            return stack;
        }


        protected override object GetEditObject(GridRecord row)
        {
            //if (facade == null)
            //{
            //    facade = new InventoryFacade(base.DataProvider);
            //}
            //object obj = facade.GetSStack(row.Items.FindItemByKey("StackCode").Value.ToString());

            //if (obj != null)
            //{
            //    return (SStack)obj;
            //}

            return null;
        }

        protected override void SetEditObject(object obj)
        {
            if (obj == null)
            {
                //this.txtStackCodeEdit.Text = "";
                //this.txtStackDescriptionEdit.Text = "";
                //this.drpStorageCodeEdit.SelectedIndex = 0;
                //this.txtCapacityEdit.Text = "";
                //this.chbIsOneItem.Checked = true;

                return;
            }

            //this.txtStackCodeEdit.Text = ((SStack)obj).StackCode.ToString();
            //this.txtStackDescriptionEdit.Text = ((SStack)obj).StackDesc.ToString();
            //this.txtCapacityEdit.Text = ((SStack)obj).Capacity.ToString();
            //this.chbIsOneItem.Checked = ((SStack)obj).IsOneItem.ToString() == "Y" ? true : false;

            //this.drpStorageCodeEdit.SelectedValue = ((SStack)obj).StorageCode.ToString();
        }

        protected override bool ValidateInput()
        {
            PageCheckManager manager = new PageCheckManager();

            //manager.Add(new LengthCheck(this.lblStackCodeEdit, this.txtStackCodeEdit, 40, true));
            //manager.Add(new LengthCheck(this.lblStackDescriptionEdit, this.txtStackDescriptionEdit, 100, true));
            //manager.Add(new LengthCheck(this.lblStorageCodeEdit, this.drpStorageCodeEdit, 40, true));
            //manager.Add(new LengthCheck(this.lblCapacityEdit, this.txtCapacityEdit, 10, true));

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
            //return new string[]{((SStackWithStorageName)obj).StackCode.ToString(),
            //                    ((SStackWithStorageName)obj).Capacity.ToString(),
            //                    ((SStackWithStorageName)obj).StackDesc.ToString(),
            //                    ((SStackWithStorageName)obj).StorageCode.ToString(),
            //                    ((SStackWithStorageName)obj).StorageName.ToString(),
            //                    this.languageComponent1.GetString(((SStackWithStorageName)obj).IsOneItem.ToString()),
            //                    ((SStackWithStorageName)obj).MaintainUser.ToString(),
            //                    FormatHelper.ToDateString(((SStackWithStorageName)obj).MaintainDate)}
            //    ;
            return null;
        }

        protected override string[] GetColumnHeaderText()
        {
            //return new string[] {	"StackCode",
            //                        "Capatity",   
            //                        "StackDescription",
            //                        "StorageCode",
            //                        "StorageDescription",
            //                        "IsOneItem",
            //                        "MaintainUser",   
            //                        "MaintainDate"};
            return null;
        }

        #endregion
    }
}
