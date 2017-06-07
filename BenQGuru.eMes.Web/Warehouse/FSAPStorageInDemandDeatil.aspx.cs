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
    public partial class FSAPStorageInDemandDeatil : BaseMPageNew
    {
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;


        protected System.Web.UI.HtmlControls.HtmlInputButton Submit1;

        private InventoryFacade _InventoryFacade = null;
        private SystemSettingFacade _SystemSettingFacade = null;
        private UserFacade _UserFacade = null;
        bool isVendor = false;//判断当前用户是否为供应商
        private static string StorageInType = string.Empty;
        private static string InvNo = string.Empty;
        private static string Complete = string.Empty;
        private static string VendorCode = string.Empty;
        private static string CreateUser = string.Empty;
        private static string CBDate = string.Empty;
        private static string EDate = string.Empty;

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
            if (_UserFacade == null)
            {
                _UserFacade = new UserFacade(this.DataProvider);
            }
            isVendor = _UserFacade.IsVendor(this.GetUserCode());
            if (!this.IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);
                this.InitStorageInTypeList();
                InitHander();


                StorageInType = Request.QueryString["StorageInType"].Trim(',');


                this.txtInvNoQuery.Text = Request.QueryString["InvNo"].Trim(',');

                InvNo = Request.QueryString["InvNo"].Trim(',');
                Complete = this.GetRequestParam("Complete");
                VendorCode = this.GetRequestParam("VendorCode");
                CreateUser = this.GetRequestParam("CreateUser");


                this.drpStorageInTypeQuery.SelectedValue = StorageInType;

                lblMOSelectMOViewField.Visible = false;
                this.InitWebGrid();
                this.cmdQuery_Click(null, null);
                this.RequestData();
            }
        }
        private void RequestData()
        {

            this.pagerToolBar.PageSize = this.pagerSizeSelector.PageSize;
            this.gridHelper.GridBind(PageGridBunding.Page, this.pagerSizeSelector.PageSize);

            this.pagerToolBar.RowCount = GetRowCount();
            this.pagerToolBar.InitPager();
        }
        private void InitHander()
        {
            this.gridHelper = new GridHelperNew(this.gridWebGrid, this.DtSource);
            this.gridHelper.LoadDataSourceHandle = new LoadDataSourceDelegateNew(this.LoadDataSource);
            this.gridHelper.GetRowCountHandle = new GetRowCountDelegateNew(this.GetRowCount);
            this.gridHelper.BuildGridRowhandle = new BuildGridRowDelegateNew(this.GetGridRow);

            this.buttonHelper = new ButtonHelper(this);

            this.pagerToolBar.OnPagerToolBarClick += new EventHandler(this.PagerToolBar_OnPagerToolBarClick);


            #region Exporter
            this.excelExporter = new BenQGuru.eMES.Web.Helper.ExcelExporter(this.components);
            this.excelExporter.Page = this;
            this.excelExporter.LanguageComponent = this.languageComponent1;
            this.excelExporter.LoadExportDataHandle = new LoadExportDataDelegate(LoadDataSource);
            this.excelExporter.FormatExportRecordHandle = new FormatExportRecordDelegate(FormatExportRecord);
            this.excelExporter.GetColumnHeaderTextHandle = new GetColumnHeaderTextDelegate(GetColumnHeaderText);
            #endregion

        }
        protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
        {
            return this.languageComponent1;
        }
        private void PagerToolBar_OnPagerToolBarClick(object sender, System.EventArgs e)
        {
            this.gridHelper.GridBind(this.pagerToolBar.PageIndex, this.pagerToolBar.PageSize);
        }
        //初始入库类型下拉框
        /// <summary>
        /// 初始化入库类型
        /// </summary>
        private void InitStorageInTypeList()
        {
            if (_SystemSettingFacade == null)
            {
                _SystemSettingFacade = new SystemSettingFacade(base.DataProvider);
            }
            if (isVendor)
            {
                this.drpStorageInTypeQuery.Items.Add(new ListItem("PO入库", "POR"));
                this.drpStorageInTypeQuery.SelectedIndex = 0;
            }
            else
            {
                object[] parameters = _SystemSettingFacade.GetParametersByParameterGroup("INVINTYPE");
                this.drpStorageInTypeQuery.Items.Add(new ListItem("", ""));
                foreach (Domain.BaseSetting.Parameter parameter in parameters)
                {
                    this.drpStorageInTypeQuery.Items.Add(new ListItem(parameter.ParameterDescription, parameter.ParameterAlias));
                }
                this.drpStorageInTypeQuery.SelectedIndex = 0;
            }


        }
        #endregion

        #region WebGrid

        protected override void InitWebGrid()
        {
            //base.InitWebGrid();
            //this.gridHelper.AddColumn("InvNo", "SAP单据号", null);
            //this.gridHelper.AddColumn("StorageInType", "入库类型", null);
            //this.gridHelper.AddColumn("FacCode", "工厂", null);
            //this.gridHelper.AddColumn("StorageCode1", "入库库位", null);
            //this.gridHelper.AddColumn("InvLine1", "行项目号", null);
            //this.gridHelper.AddColumn("Status1", "行项目状态", null);
            //this.gridHelper.AddColumn("SAPMCode", "SAP物料编码", null);
            //this.gridHelper.AddColumn("DQMCode", "鼎桥物料编码", null);
            //this.gridHelper.AddColumn("MENShortDesc", "鼎桥物料编码描述", null);
            //this.gridHelper.AddColumn("Unit1", "物料单位", null);
            //this.gridHelper.AddColumn("PlanQty1", "需求数量", null);
            //this.gridHelper.AddColumn("ActQty1", "已入库数量", null);
            //this.gridHelper.AddColumn("FROMSTORAGECODE", "出库库位", null);

            //this.gridHelper.AddDefaultColumn(false, false);

            ////多语言
            //this.gridHelper.ApplyLanguage(this.languageComponent1);
            //this.gridHelper.RequestData();





            base.InitWebGrid();

            for (int i = 0; i < this.SAPHeadViewFieldList.Length; i++)
            {
                this.gridHelper.AddColumn(this.SAPHeadViewFieldList[i].FieldName, /*this.languageComponent1.GetString(*/this.SAPHeadViewFieldList[i].Description/*)*/, null);
            }
            this.gridHelper.AddDefaultColumn(false, false);


            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);

            this.gridHelper.RequestData();



        }

        protected override DataRow GetGridRow(object obj)
        {
            DataRow row = this.DtSource.NewRow();

            //row["InvNo"] = ((InvoicesDetailExt)obj).InvNo;
            //row["StorageInType"] = this.GetInvInName(((InvoicesDetailExt)obj).InvType);
            //row["FacCode"] = ((InvoicesDetailExt)obj).FacCode;
            //row["StorageCode1"] = ((InvoicesDetailExt)obj).StorageCode;
            //row["InvLine1"] = ((InvoicesDetailExt)obj).InvLine;
            //row["Status1"] = ((InvoicesDetailExt)obj).InvLineStatus;
            //row["SAPMCode"] = ((InvoicesDetailExt)obj).MCode;
            //row["DQMCode"] = ((InvoicesDetailExt)obj).DQMCode;
            //row["MENShortDesc"] = ((InvoicesDetailExt)obj).MenshortDesc;
            //row["Unit1"] = ((InvoicesDetailExt)obj).Unit;
            //row["PlanQty1"] = ((InvoicesDetailExt)obj).PlanQty;
            //row["ActQty1"] = ((InvoicesDetailExt)obj).ActQty;
            //row["FROMSTORAGECODE"] = ((InvoicesDetailExt)obj).FromStorageCode;
            //return row;


            InvoicesDetailExt1 inv = obj as InvoicesDetailExt1;
            Type type = inv.GetType();
            for (int i = 1; i < this.DtSource.Columns.Count; i++)
            {
                ViewField field = this.SAPHeadViewFieldList[i - 1];
                string strValue = string.Empty;
                System.Reflection.FieldInfo fieldInfo = type.GetField(field.FieldName, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Instance);
                if (fieldInfo != null)
                {
                    strValue = fieldInfo.GetValue(inv).ToString();
                }
                if (field.FieldName == "InvType")
                {
                    strValue = this.GetInvInName(inv.InvType);

                }


                else if (field.FieldName == "InvStatus1")
                {
                    strValue = this.GetStatusName(inv.InvLineStatus);
                }

                else if (field.FieldName == "CDate")
                {
                    strValue = FormatHelper.ToDateString(inv.CDate);
                }
                else if (field.FieldName == "CTime")
                {
                    strValue = FormatHelper.ToTimeString(inv.CTime);
                }

                //PlanSendDate
                row[i] = strValue;
            }

            return row;


        }

        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (_InventoryFacade == null)
            {
                _InventoryFacade = new InventoryFacade(base.DataProvider);
            }

            return this._InventoryFacade.QueryInvoicesDetail1(
                FormatHelper.CleanString(this.drpStorageInTypeQuery.SelectedValue),
                FormatHelper.CleanString(this.txtInvNoQuery.Text),
                GetType(),
                inclusive, exclusive);
        }


        private ViewField[] viewFieldList = null;
        private ViewField[] SAPHeadViewFieldList
        {
            get
            {
                if (viewFieldList == null)
                {
                    if (_InventoryFacade == null)
                    {
                        _InventoryFacade = new InventoryFacade(base.DataProvider);
                    }
                    object[] objs = _InventoryFacade.QueryViewFieldByUserCode(this.GetUserCode(), "TBLINVOICESDETAIL");
                    if (objs != null)
                    {
                        viewFieldList = new ViewField[objs.Length];
                        objs.CopyTo(viewFieldList, 0);
                    }
                    else
                    {
                        objs = _InventoryFacade.QueryViewFieldDefault("INVOICESDETAIL_FIELD_LIST_SYSTEM_DEFAULT", "TBLINVOICESDETAIL");
                        if (objs != null)
                        {
                            ArrayList list = new ArrayList();
                            for (int i = 0; i < objs.Length; i++)
                            {
                                ViewField field = (ViewField)objs[i];
                                if (FormatHelper.StringToBoolean(field.IsDefault) == true)
                                {
                                    list.Add(field);
                                }
                            }
                            viewFieldList = new ViewField[list.Count];
                            list.CopyTo(viewFieldList, 0);
                        }
                    }

                }
                return viewFieldList;
            }
        }


        protected override int GetRowCount()
        {
            if (_InventoryFacade == null)
            {
                _InventoryFacade = new InventoryFacade(base.DataProvider);
            }
            return this._InventoryFacade.QueryInvoicesDetailCount(
                FormatHelper.CleanString(this.drpStorageInTypeQuery.SelectedValue),
                FormatHelper.CleanString(this.txtInvNoQuery.Text),
                GetType());
        }

        #endregion

        #region Button

        protected void cmdReturn_ServerClick(object sender, EventArgs e)
        {
            //this.Response.Redirect(this.MakeRedirectUrl("FSAPStorageInDemandQuery.aspx"));
            Response.Redirect(this.MakeRedirectUrl("FSAPStorageInDemandQuery.aspx",
                                  new string[] { "StorageInType", "InvNo", "Complete", "VendorCode", "CreateUser", "CBDate", "EDate" },
                                  new string[] { StorageInType, InvNo, Complete, VendorCode, CreateUser, CBDate, EDate
                                        
                                    }));
        }

        #endregion

        #region Export

        protected override string[] FormatExportRecord(object obj)
        {
            return new string[]{((InvoicesDetailExt)obj).InvNo,
                                this.GetInvInName(((InvoicesDetailExt)obj).InvType),
                                ((InvoicesDetailExt)obj).FacCode,
                                ((InvoicesDetailExt)obj).StorageCode,
                                ((InvoicesDetailExt)obj).InvLine.ToString(),
                                this.GetStatusName(((InvoicesDetailExt)obj).Status),
                                ((InvoicesDetailExt)obj).MCode,
                                ((InvoicesDetailExt)obj).DqSmCode,
                                ((InvoicesDetailExt)obj).MenshortDesc,
                                ((InvoicesDetailExt)obj).Unit.ToString(),
                                ((InvoicesDetailExt)obj).PlanQty.ToString(),
                                ((InvoicesDetailExt)obj).ActQty.ToString()};
        }

        protected override string[] GetColumnHeaderText()
        {
            return new string[] {	"InvNo",
                                    "StorageInType",
                                    "FacCode",
                                    "StorageCode",
                                    "InvLine",
                                    "Status",	
                                    "SAPMCode",
                                    "DQMCode",	
                                    "MENShortDesc",
                                    "Unit",	
                                    "PlanQty",
                                    "ActQty"};
        }

        #endregion

        #region FUNCTION

        //获取盘亏/盘盈标识
        /// <summary>
        /// 获取盘亏/盘盈标识
        /// </summary>
        /// <returns></returns>
        private string GetType()
        {
            if (this.GetRequestParam("StorageInType") == "PD")
            {
                return "701";
            }
            return "";
        }
        #endregion

    }
}
