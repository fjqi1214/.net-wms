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

using BenQGuru.eMES.Domain.Material;
using BenQGuru.eMES.Domain.Warehouse;
using BenQGuru.eMES.Material;
using System.Collections.Generic;

using Infragistics.WebUI.UltraWebGrid;

namespace BenQGuru.eMES.Web.WarehouseWeb
{
    public partial class FDNTempOutMP : BaseMPage
    {
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;

        private InventoryFacade _facade = null;

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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);
                this.InitUI();
                DrpStorageTypeQuery_Init();

            }
        }

        protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
        {
            return this.languageComponent1;
        }

        private void DrpStorageTypeQuery_Init()
        {
            if (_facade == null)
            {
                _facade = new InventoryFacade(this.DataProvider);
            }

            object[] StorageList = _facade.GetAllStorageCode();

            this.drpStorageTypeQuery.Items.Clear();

            this.drpStorageTypeQuery.Items.Add(new ListItem("", ""));
            for (int i = 0; i < StorageList.Length; i++)
            {
                //this.drpStorageTypeQuery.Items.Add(new ListItem(((Storage)StorageList[i]).StorageName, ((Storage)StorageList[i]).StorageCode));
            }
            this.drpStorageTypeQuery.SelectedIndex = 0;
        }
        #region WebGrid

        protected override void InitWebGrid()
        {
            this.gridHelper.AddColumn("Storage", "库别", null);
            this.gridHelper.AddColumn("StorageDesc", "库别描述", null);
            this.gridHelper.AddColumn("Company", "公司别", null);
            this.gridHelper.AddColumn("StackCode", "垛位代码", null);
            this.gridHelper.AddColumn("ItemCode", "产品代码", null);
            this.gridHelper.AddColumn("ItemDescription", "产品描述", null);
            this.gridHelper.AddColumn("MModelCode", "机型", null);
            this.gridHelper.AddColumn("lotqty", "在库数量", null);
            this.gridHelper.AddColumn("Mo_MOActualQty", "已完工数量", null);
            this.gridHelper.AddColumn("SAP_REPORTQTY", "SAP报工数量", null);
            this.gridHelper.AddColumn("TempOutQty", "绑定数量", null);
            this.gridHelper.AddColumn("CanDoQty", "可用数量", null);
            this.gridHelper.AddLinkColumn("TempDN", "绑定交货单", null);

            this.gridHelper.AddDefaultColumn(false, false);
            this.gridWebGrid.Columns.FromKey("TempDN").Width = 75;
            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }

        protected override void cmdQuery_Click(object sender, EventArgs e)
        {
            base.cmdQuery_Click(sender, e);
        }

        protected override int GetRowCount()
        {
            if (_facade == null)
            {
                _facade = new InventoryFacade(base.DataProvider);
            }
            return this._facade.GetDNTempOutCount(this.drpStorageTypeQuery.SelectedValue,
                                                  FormatHelper.CleanString(this.txtItemCodeEdit.Text.ToUpper()),
                                                  FormatHelper.CleanString(this.txtMmodelcode.Text.ToUpper()),
                                                  FormatHelper.CleanString(this.txtStackCodeQuery.Text.ToUpper())
                                                        );

        }

        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (_facade == null)
            {
                _facade = new InventoryFacade(base.DataProvider);
            }

            if (this.drpStorageTypeQuery.SelectedValue.ToString().Trim() == string.Empty)
            {
                WebInfoPublish.Publish(this, "$ERROR_Storage_Must_Selected", this.languageComponent1);
                return null;
            }

            return this._facade.QueryDNTempOut(this.drpStorageTypeQuery.SelectedValue,
                                              FormatHelper.CleanString(this.txtItemCodeEdit.Text.ToUpper()),
                                              FormatHelper.CleanString(this.txtMmodelcode.Text.ToUpper()),
                                              FormatHelper.CleanString(this.txtStackCodeQuery.Text.ToUpper()),
                                              inclusive, exclusive);

        }

        protected override Infragistics.WebUI.UltraWebGrid.UltraGridRow GetGridRow(object obj)
        {
            return new Infragistics.WebUI.UltraWebGrid.UltraGridRow(
                new object[]{
								((DNTempOutMessage)obj).StorageCode.ToString(),
								((DNTempOutMessage)obj).StorageName.ToString(),
								((DNTempOutMessage)obj).Company.ToString(),
                                ((DNTempOutMessage)obj).StackCode.ToString(),
                                ((DNTempOutMessage)obj).ItemCode.ToString(),
								((DNTempOutMessage)obj).ItemDescription.ToString(),
								((DNTempOutMessage)obj).MModelCode.ToString(),
                                ((DNTempOutMessage)obj).INVQTY.ToString(),
                                ((DNTempOutMessage)obj).COMQTY.ToString(),
                                ((DNTempOutMessage)obj).SAPQTY.ToString(),
                                ((DNTempOutMessage)obj).TEMPQTY.ToString(),
                                (((DNTempOutMessage)obj).SAPQTY-((DNTempOutMessage)obj).TEMPQTY).ToString(),
                                ""          
								});
        }

        protected override void Grid_ClickCell(UltraGridCell cell)
        {
            string userCode = this.GetUserCode();
            base.Grid_ClickCell(cell);
            if (this.gridHelper.IsClickColumn("TempDN", cell))
            {
                Response.Redirect(this.MakeRedirectUrl("FDNTempOutSP.aspx", new string[] { "Storage","Company", "StackCode", "ItemCode", "MModelCode" },
                    new string[] { 
                                cell.Row.Cells[0].ToString(), 
                                cell.Row.Cells[2].ToString(),
                                cell.Row.Cells[3].ToString(), 
                                cell.Row.Cells[4].ToString(),
                                cell.Row.Cells[6].ToString()
                    }));
            }
        }

        #endregion

        protected override string[] GetColumnHeaderText()
        {
            return new string[] {	"Storage",
									"StorageDesc",	
									"Company",
				                    "StackCode",
									"ItemCode",	
									"ItemDescription",
				                    "MModelCode",
									"lotqty",	
									"Mo_MOActualQty",
                                    "SAP_REPORTQTY" ,
                                     "TempOutQty",
                                    "CanDoQty"
            };
        }

        protected override string[] FormatExportRecord(object obj)
        {
            return new string[]{ ((DNTempOutMessage)obj).StorageCode.ToString(),
								((DNTempOutMessage)obj).StorageName.ToString(),
								((DNTempOutMessage)obj).Company.ToString(),
                                ((DNTempOutMessage)obj).StackCode.ToString(),
                                ((DNTempOutMessage)obj).ItemCode.ToString(),
								((DNTempOutMessage)obj).ItemDescription.ToString(),
								((DNTempOutMessage)obj).MModelCode.ToString(),
                                ((DNTempOutMessage)obj).INVQTY.ToString(),
                                ((DNTempOutMessage)obj).COMQTY.ToString(),
                                ((DNTempOutMessage)obj).SAPQTY.ToString(),
                                ((DNTempOutMessage)obj).TEMPQTY.ToString(),
                                (((DNTempOutMessage)obj).SAPQTY-((DNTempOutMessage)obj).TEMPQTY).ToString()
                                            
            };
        }
    }
}
