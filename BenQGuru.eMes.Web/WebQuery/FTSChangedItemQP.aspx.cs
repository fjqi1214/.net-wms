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
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.WebQuery;
using BenQGuru.eMES.Domain.TS;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;

namespace BenQGuru.eMES.Web.WebQuery
{
    /// <summary>
    /// FTSChangedItemQP 的摘要说明。
    /// </summary>
    public partial class FTSChangedItemQP : BaseQPageNew
    {
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;

        protected ExcelExporter excelExporter = null;
        protected WebQueryHelperNew _helper = null;

        #region ViewState
        private int SourceResourceDate
        {
            get
            {
                if (this.ViewState["SourceResourceDate"] != null)
                {
                    try
                    {
                        return System.Int32.Parse(this.ViewState["SourceResourceDate"].ToString());
                    }
                    catch
                    {
                        return 0;
                    }
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                this.ViewState["SourceResourceDate"] = value;
            }
        }

        private int SourceResourceTime
        {
            get
            {
                if (this.ViewState["SourceResourceTime"] != null)
                {
                    try
                    {
                        return System.Int32.Parse(this.ViewState["SourceResourceTime"].ToString());
                    }
                    catch
                    {
                        return 0;
                    }
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                this.ViewState["SourceResourceTime"] = value;
            }
        }
        #endregion

        protected void Page_Load(object sender, System.EventArgs e)
        {
            this._initialParamter();

            this.gridHelper = new GridHelperNew(this.gridWebGrid, DtSource);

            this._helper = new WebQueryHelperNew(null, this.cmdGridExport, this.gridWebGrid, this.pagerSizeSelector, this.pagerToolBar, this.languageComponent1, DtSource);
            this._helper.LoadGridDataSource += new EventHandler(_helper_LoadGridDataSource);
            this._helper.DomainObjectToGridRow += new EventHandler(_helper_DomainObjectToGridRow);
            this._helper.DomainObjectToExportRow += new EventHandler(_helper_DomainObjectToExportRow);
            this._helper.GetExportHeadText += new EventHandler(_helper_GetExportHeadText);

            if (!this.IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);

                this._initialWebGrid();
                this._helper.Query(sender);
            }
        }

        private void _initialParamter()
        {
            this.txtModelQuery.Text = this.GetRequestParam("ModelCode");
            this.txtItemQuery.Text = this.GetRequestParam("ItemCode");
            this.txtMoQuery.Text = this.GetRequestParam("MoCode");
            this.txtSnQuery.Text = this.GetRequestParam("RunningCard");
            this.txtTsStateQuery.Text = this.GetRequestParam("TSState");
            this.txtRepaireResourceQuery.Text = this.GetRequestParam("TSResourceCode");

            if (this.GetRequestParam("TSDate") != null)
            {
                string tsDate = this.GetRequestParam("TSDate");

                try
                {
                    this.SourceResourceDate = FormatHelper.TODateInt(tsDate);
                }
                catch
                {
                    this.SourceResourceDate = 0;
                }

                if (this.GetRequestParam("TSTime") != null)
                {
                    string tsTime = this.GetRequestParam("TSTime");

                    try
                    {
                        this.SourceResourceTime = FormatHelper.TOTimeInt(tsTime);
                    }
                    catch
                    {
                        this.SourceResourceTime = 0;
                    }
                }
            }
        }

        private void _initialWebGrid()
        {
            base.InitWebGrid();
            this.gridHelper.AddColumn("MItemCode1", "新物料料号", null);
            this.gridHelper.AddColumn("MCard1", "新物料序列号", null);
            this.gridHelper.AddColumn("SItemCode1", "原物料料号", null);
            this.gridHelper.AddColumn("MSCard1", "原物料序列号", null);
            this.gridHelper.AddColumn("Location1", "零件位置", null);
            this.gridHelper.AddColumn("LotNO2", "批号", null);
            this.gridHelper.AddColumn("VendorCode", "厂商", null);
            this.gridHelper.AddColumn("VendorItemCode", "厂商料号", null);
            this.gridHelper.AddColumn("DateCode", "生产日期", null);
            this.gridHelper.AddColumn("Reversion", "料品版本", null);
            this.gridHelper.AddColumn("PCBA", "PCBA版本", null);
            this.gridHelper.AddColumn("BIOS", "BIOS版本", null);
            this.gridHelper.AddColumn("MEMO", "补充说明", null);

            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }

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
            this.languageComponent1.LanguagePackageDir = "\\\\grd2-build\\language pack\\";
            this.languageComponent1.RuntimePage = null;
            this.languageComponent1.RuntimeUserControl = null;
            this.languageComponent1.UserControlName = "";

        }
        #endregion


        private bool _checkRequireFields()
        {
            //			PageCheckManager manager = new PageCheckManager();
            //			manager.Add( new LengthCheck(this.lblModelQuery,this.txtModelQuery,System.Int32.MaxValue,true) );
            //			manager.Add( new LengthCheck(this.lblItemQuery,this.txtItemQuery,System.Int32.MaxValue,true) );
            //			manager.Add( new LengthCheck(this.lblMoQuery,this.txtMoQuery,System.Int32.MaxValue,true) );
            //			manager.Add( new LengthCheck(this.lblSnQuery,this.txtSnQuery,System.Int32.MaxValue,true) );
            //			manager.Add( new LengthCheck(this.lblTSStateQuery,this.txtTsStateQuery,System.Int32.MaxValue,true) );
            //			manager.Add( new LengthCheck(this.lblRepaireOperationQuery,this.txtRepaireResourceQuery,System.Int32.MaxValue,true) );
            //
            //			if( !manager.Check() )
            //			{
            //				WebInfoPublish.Publish(this,manager.CheckMessage,this.languageComponent1);
            //				return true;
            //			}	
            return true;
        }

        private void _helper_LoadGridDataSource(object sender, EventArgs e)
        {
            if (this._checkRequireFields())
            {
                FacadeFactory facadeFactory = new FacadeFactory(base.DataProvider);
                (e as WebQueryEventArgsNew).GridDataSource =
                    facadeFactory.CreateQueryTSChangedPartsFacade().QueryTSChangedParts(
                    "",
                    "",
                    FormatHelper.CleanString(this.txtMoQuery.Text).ToUpper(),
                    FormatHelper.CleanString(this.txtSnQuery.Text).ToUpper(),
                    "",
                    FormatHelper.CleanString(this.txtRepaireResourceQuery.Text).ToUpper(),
                    "",
                    (e as WebQueryEventArgsNew).StartRow,
                    (e as WebQueryEventArgsNew).EndRow);

                (e as WebQueryEventArgsNew).RowCount =
                    facadeFactory.CreateQueryTSChangedPartsFacade().QueryTSChangedPartsCount(
                    "",
                    "",
                    FormatHelper.CleanString(this.txtMoQuery.Text).ToUpper(),
                    FormatHelper.CleanString(this.txtSnQuery.Text).ToUpper(),
                    "",
                    FormatHelper.CleanString(this.txtRepaireResourceQuery.Text).ToUpper(),
                    "");
            }
        }

        private void _helper_DomainObjectToGridRow(object sender, EventArgs e)
        {
            if ((e as DomainObjectToGridRowEventArgsNew).DomainObject != null)
            {
                TSItem obj = (e as DomainObjectToGridRowEventArgsNew).DomainObject as TSItem;
                DataRow row = DtSource.NewRow();
                row["MItemCode1"] = obj.MItemCode;
                row["MCard1"] = obj.MCard;
                row["SItemCode1"] = obj.SourceItemCode;
                row["MSCard1"] = obj.MSourceCard;
                row["Location1"] = obj.Location;
                row["LotNO2"] = obj.LotNO;
                row["VendorCode"] = obj.VendorCode;
                row["VendorItemCode"] = obj.VendorItemCode;
                row["DateCode"] = obj.DateCode;
                row["Reversion"] = obj.Reversion;
                row["PCBA"] = obj.PCBA;
                row["BIOS"] = obj.BIOS;
                row["MEMO"] = obj.MEMO;
                (e as DomainObjectToGridRowEventArgsNew).GridRow = row;

            }
        }

        private void _helper_DomainObjectToExportRow(object sender, EventArgs e)
        {
            if ((e as DomainObjectToExportRowEventArgsNew).DomainObject != null)
            {
                TSItem obj = (e as DomainObjectToExportRowEventArgsNew).DomainObject as TSItem;
                (e as DomainObjectToExportRowEventArgsNew).ExportRow =
                    new string[]{
									obj.SourceItemCode,
									obj.MSourceCard,
									obj.SourceItemCode ,
									obj.MSourceCard ,
									obj.Location,
									obj.LotNO,
									obj.VendorCode,
									obj.VendorItemCode,
									obj.DateCode,													  
									obj.Reversion,
									obj.PCBA,
									obj.BIOS,
									obj.MEMO
								};
            }
        }

        private void _helper_GetExportHeadText(object sender, EventArgs e)
        {
            (e as ExportHeadEventArgsNew).Heads =
                new string[]{
								"MItemCode1",
								"MCard1",
								"SItemCode1",
								"MSCard1",
								"Location1",
								"LotNO2",
								"VendorCode",
								"VendorItemCode",
								"DateCode",
								"Reversion",
								"PCBA",
								"BIOS",
								"MEMO"
							};
        }

        protected void cmdReturn_ServerClick(object sender, System.EventArgs e)
        {
            ArrayList keys = new ArrayList();
            ArrayList values = new ArrayList();

            for (int i = 0; i < this.Request.QueryString.AllKeys.Length; i++)
            {
                //if( this.Request.QueryString.AllKeys.GetValue(i).ToString().StartsWith("12_") )
                //{
                keys.Add("12_" + this.Request.QueryString.AllKeys.GetValue(i).ToString());
                values.Add(this.Request.QueryString[this.Request.QueryString.AllKeys.GetValue(i).ToString()]);
                //}
            }

            this.Response.Redirect(
                this.MakeRedirectUrl(
                this.GetRequestParam("BackUrl"), (string[])keys.ToArray(typeof(string)), (string[])values.ToArray(typeof(string))));
        }
    }
}

