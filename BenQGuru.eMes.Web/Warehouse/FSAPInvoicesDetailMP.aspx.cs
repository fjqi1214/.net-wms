using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Web.Helper;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.Domain.Warehouse;

namespace BenQGuru.eMES.Web.Warehouse
{
    /// <summary>
    /// FCreatePickHeadMP 的摘要说明。
    /// </summary>
    public partial class FSAPInvoicesDetailMP : BaseMPageNew
    {
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;

        protected System.Web.UI.HtmlControls.HtmlInputButton Submit1;

        private InventoryFacade _InventoryFacade = null;
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
            InitHander();
            if (!this.IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);
                this.txtInvNoQuery.Text = this.GetRequestParam("INVNO");
                lblMOSelectMOViewField.Visible = false;
                this.InitWebGrid();
                this.cmdQuery_Click(null, null);
                this.RequestData();
            }
        }

        protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
        {
            return this.languageComponent1;
        }

        #region
  
        #endregion

        #region 默认查询
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
        private void PagerToolBar_OnPagerToolBarClick(object sender, System.EventArgs e)
        {
            this.gridHelper.GridBind(this.pagerToolBar.PageIndex, this.pagerToolBar.PageSize);
        }

        #endregion

        #endregion

        #region WebGrid

        protected override void InitWebGrid()
        {
            base.InitWebGrid();
            for (int i = 0; i < this.PickHeadViewFieldList.Length; i++)
            {
                this.gridHelper.AddColumn(this.PickHeadViewFieldList[i].FieldName, /*this.languageComponent1.GetString(*/this.PickHeadViewFieldList[i].Description/*)*/, null);
            }
            this.gridHelper.AddDefaultColumn(false, false);
            //this.gridHelper.AddLinkColumn("LinkDetailMaterial", "已拣明细");
            this.gridWebGrid.Columns.FromKey("EATTRIBUTE1").Hidden = true;
            this.gridWebGrid.Columns.FromKey("EATTRIBUTE2").Hidden = true;
            this.gridWebGrid.Columns.FromKey("EATTRIBUTE3").Hidden = true;

            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }

        protected override DataRow GetGridRow(object obj)
        {
            DataRow row = this.DtSource.NewRow();

            SAPInvoicesDetail pickLine = obj as SAPInvoicesDetail;
            Type type = pickLine.GetType();
            for (int i = 0; i < this.PickHeadViewFieldList.Length; i++)
            {
                ViewField field = this.PickHeadViewFieldList[i];
                string strValue = string.Empty;
                System.Reflection.FieldInfo fieldInfo = type.GetField(field.FieldName.ToUpper());
                if (fieldInfo != null)
                {
                    strValue = fieldInfo.GetValue(pickLine).ToString();
                }
                row[i + 1] = strValue;
            }

            return row;

        }

        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (_InventoryFacade == null)
            {
                _InventoryFacade = new InventoryFacade(base.DataProvider);
            }

            return this._InventoryFacade.QuerySAPInvoicesDetail(
             FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtInvNoQuery.Text)) , inclusive, exclusive);
        }

        protected override int GetRowCount()
        {
            if (_InventoryFacade == null)
            {
                _InventoryFacade = new InventoryFacade(base.DataProvider);
            }
            return this._InventoryFacade.QueryInvoicesDetailCount(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtInvNoQuery.Text))
               );
        }

        #endregion

        #region Button
        //protected override void Grid_ClickCell(GridRecord row, string commandName)
        //{
        //    if (_InventoryFacade == null)
        //    {
        //        _InventoryFacade = new InventoryFacade(base.DataProvider);
        //    }
        //    //if (commandName == "LinkDetailMaterial")
        //    //{
        //    //    string pickNo = row.Items.FindItemByKey("PickNo").Text.Trim();
        //    //    Response.Redirect(this.MakeRedirectUrl("FCreateCheckedPickLineMP.aspx",
        //    //             new string[] { "PickNo" },
        //    //             new string[] { pickNo }));
        //    //}
        //}
        #endregion

        #region ToolButton

        #region 返回
        protected void cmdReturn_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect(this.MakeRedirectUrl("FSAPInvoicesMP.aspx"));
        }
        #endregion

        #endregion

        #region Export

        protected override string[] FormatExportRecord(object obj)
        {
            string[] objs = new string[this.PickHeadViewFieldList.Length];

            SAPInvoicesDetail pickLine = obj as SAPInvoicesDetail;
            Type type = pickLine.GetType();
            for (int i = 0; i < this.PickHeadViewFieldList.Length; i++)
            {
                ViewField field = this.PickHeadViewFieldList[i];
                string strValue = string.Empty;
                System.Reflection.FieldInfo fieldInfo = type.GetField(field.FieldName.ToUpper());
                if (fieldInfo != null)
                {
                    strValue = fieldInfo.GetValue(pickLine).ToString();
                }
              
                objs[i] = strValue;
            }
            return objs;
        }

        protected override string[] GetColumnHeaderText()
        {
            string[] strHeader = new string[this.PickHeadViewFieldList.Length];
            for (int i = 0; i < strHeader.Length; i++)
            {
                strHeader[i] = this.PickHeadViewFieldList[i].Description;
            }
            return strHeader;
        }

        #endregion

     



        private ViewField[] viewFieldList = null;
        private ViewField[] PickHeadViewFieldList
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
                    if (viewFieldList != null)
                    {
                        bool bExistPickNo = false;
                        for (int i = 0; i < viewFieldList.Length; i++)
                        {
                            if (viewFieldList[i].FieldName == "INVLINE")
                            {
                                bExistPickNo = true;
                                break;
                            }
                        }
                        if (bExistPickNo == false)
                        {
                            ViewField field = new ViewField();
                            field.FieldName = "INVLINE";
                            field.Description = "行号";
                            ArrayList list = new ArrayList();
                            list.Add(field);
                            list.AddRange(viewFieldList);
                            viewFieldList = new ViewField[list.Count];
                            list.CopyTo(viewFieldList, 0);
                        }
                    }
                }
                return viewFieldList;
            }
        }

    }

}
