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
using BenQGuru.eMES.Domain.OQC;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;

namespace BenQGuru.eMES.Web.WebQuery
{
    /// <summary>
    /// FOQCCardCheckList 的摘要说明。
    /// </summary>
    public partial class FOQCCardCheckList : BaseQPageNew
    {
        protected System.Web.UI.WebControls.Label lblModelQuery;
        protected System.Web.UI.WebControls.Label lblItemQuery;
        protected System.Web.UI.WebControls.Label lblMoQuery;
        protected System.Web.UI.WebControls.Label lblTSStateQuery;
        protected System.Web.UI.WebControls.Label lblRepaireOperationQuery;
        //protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        //private System.ComponentModel.IContainer components;

        protected ExcelExporter excelExporter = null;
        protected WebQueryHelperNew _helper = null;
        protected System.Web.UI.WebControls.TextBox txtModelQuery;
        protected System.Web.UI.WebControls.TextBox txtItemQuery;
        protected System.Web.UI.WebControls.TextBox txtMoQuery;
        protected System.Web.UI.WebControls.TextBox txtTsStateQuery;
        protected System.Web.UI.WebControls.TextBox txtRepaireOperationQuery;
        protected System.Web.UI.WebControls.TextBox txtRepaireResourceQuery;
        protected GridHelperNew _gridHelper = null;

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

        private decimal RcardSequence
        {
            get
            {
                if (this.ViewState["RcardSequence"] != null)
                {
                    try
                    {
                        return decimal.Parse(this.ViewState["RcardSequence"].ToString());
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
                this.ViewState["RcardSequence"] = value;
            }
        }

        private decimal LotSequence
        {
            get
            {
                if (this.ViewState["LotSequence"] != null)
                {
                    try
                    {
                        return decimal.Parse(this.ViewState["LotSequence"].ToString());
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
                this.ViewState["LotSequence"] = value;
            }
        }

        private decimal CheckSequence
        {
            get
            {
                if (this.ViewState["CHECKSEQ"] != null)
                {
                    try
                    {
                        return decimal.Parse(this.ViewState["CHECKSEQ"].ToString());
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
                this.ViewState["CHECKSEQ"] = value;
            }
        }
        #endregion

        protected void Page_Load(object sender, System.EventArgs e)
        {
            this._initialParamter();
            this.gridHelper = new GridHelperNew(this.gridWebGrid, this.DtSource);
            this._gridHelper = new GridHelperNew(this.gridWebGrid, this.DtSource);

            this._helper = new WebQueryHelperNew(null, this.cmdGridExport, this.gridWebGrid, this.pagerSizeSelector, this.pagerToolBar, this.languageComponent1, this.DtSource);
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
            this.txtLotno.Text = this.GetRequestParam("LotNo");
            this.txtItemCode.Text = this.GetRequestParam("ItemCode");
            this.txtMOCode.Text = this.GetRequestParam("MoCode");
            this.txtSnQuery.Text = this.GetRequestParam("RunningCard");

            this.ViewState["RcardSequence"] = this.GetRequestParam("RunningCardSeq");
            this.ViewState["LotSequence"] = this.GetRequestParam("LotSeq");
            this.ViewState["CHECKSEQ"] = this.GetRequestParam("CHECKSEQ");
        }

        private void _initialWebGrid()
        {
            base.InitWebGrid();
            this._gridHelper.AddColumn("RunningCard", "序列号", null);
            this._gridHelper.AddColumn("LotNO", "批号", null);
            this._gridHelper.AddColumn("ItemCode", "产品", null);
            this._gridHelper.AddColumn("MOCode", "工单", null);
            this._gridHelper.AddColumn("ModelCode", "产品别", null);
            this._gridHelper.AddColumn("CheckItemCOde", "检验项目代码", null);
            this._gridHelper.AddColumn("Grade", "严重等级", null);
            this._gridHelper.AddColumn("OQCResult", "抽检结果", null);
            this._gridHelper.AddColumn("OQCMan", "检验人员", null);
            this._gridHelper.AddColumn("OQCDate", "检验日期", null);
            this._gridHelper.AddColumn("OQCTime", "检验时间", null);
            this._gridHelper.AddColumn("Memo", "补充说明", null);

            //多语言
            this._gridHelper.ApplyLanguage(this.languageComponent1);
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
            return true;
        }

        private void _helper_LoadGridDataSource(object sender, EventArgs e)
        {
            if (this._checkRequireFields())
            {
                BenQGuru.eMES.OQC.OQCFacade oqcfcade = new BenQGuru.eMES.OQC.OQCFacade(base.DataProvider);
                (e as WebQueryEventArgsNew).GridDataSource =
                    oqcfcade.QueryOQCLOTCardCheckList2(
                    FormatHelper.CleanString(this.txtItemCode.Text).ToUpper(),
                    FormatHelper.CleanString(this.txtMOCode.Text).ToUpper(),
                    FormatHelper.CleanString(this.txtSnQuery.Text).ToUpper(),
                    this.RcardSequence,
                    FormatHelper.CleanString(this.txtLotno.Text).ToUpper(),
                    this.LotSequence,
                    this.CheckSequence,
                    (e as WebQueryEventArgsNew).StartRow,
                    (e as WebQueryEventArgsNew).EndRow);

                (e as WebQueryEventArgsNew).RowCount =
                    oqcfcade.QueryOQCLOTCardCheckListCount2(
                    FormatHelper.CleanString(this.txtItemCode.Text).ToUpper(),
                    FormatHelper.CleanString(this.txtMOCode.Text).ToUpper(),
                    FormatHelper.CleanString(this.txtSnQuery.Text).ToUpper(),
                    this.RcardSequence,

                    FormatHelper.CleanString(this.txtLotno.Text).ToUpper(),
                    this.LotSequence,
                     this.CheckSequence);

            }
        }

        private void _helper_DomainObjectToGridRow(object sender, EventArgs e)
        {




            if ((e as DomainObjectToGridRowEventArgsNew).DomainObject != null)
            {
                OQCLOTCardCheckList obj = (e as DomainObjectToGridRowEventArgsNew).DomainObject as OQCLOTCardCheckList;

                DataRow row = this.DtSource.NewRow();
                row["RunningCard"] = obj.RunningCard;
                row["LotNO"] = obj.LOTNO;
                row["ItemCode"] = obj.ItemCode;
                row["MOCode"] = obj.MOCode;
                row["ModelCode"] = obj.ModelCode;
                row["CheckItemCOde"] = obj.CheckItemCode;
                row["Grade"] = this.languageComponent1.GetString(obj.Grade);
                row["OQCResult"] = obj.Result;
                row["OQCMan"] = obj.MaintainUser;
                row["OQCDate"] = FormatHelper.ToDateString(obj.MaintainDate);
                row["OQCTime"] = FormatHelper.ToTimeString(obj.MaintainTime);
                row["Memo"] = obj.MEMO;
                (e as DomainObjectToGridRowEventArgsNew).GridRow = row;
                    //new UltraGridRow(new object[]{
                    //                                  obj.RunningCard ,
                    //                                  obj.LOTNO ,
                    //                                  obj.ItemCode ,
                    //                                  obj.MOCode ,
                    //                                  obj.ModelCode ,
                    //                                  obj.CheckItemCode ,
                    //                                  this.languageComponent1.GetString(obj.Grade) ,
                    //                                  obj.Result ,
                    //                                  obj.MaintainUser,
                    //                                  FormatHelper.ToDateString(obj.MaintainDate),
                    //                                  FormatHelper.ToTimeString(obj.MaintainTime),
                    //                                  obj.MEMO
                    //                              }
                    //);
            }
        }

        private void _helper_DomainObjectToExportRow(object sender, EventArgs e)
        {
            if ((e as DomainObjectToExportRowEventArgsNew).DomainObject != null)
            {
                OQCLOTCardCheckList obj = (e as DomainObjectToExportRowEventArgsNew).DomainObject as OQCLOTCardCheckList;
                (e as DomainObjectToExportRowEventArgsNew).ExportRow =
                    new string[]{
									obj.RunningCard ,
									obj.LOTNO ,
									obj.MOCode ,
									obj.ItemCode ,
									obj.ModelCode ,
									obj.CheckItemCode ,
									obj.Grade ,
									obj.Result ,
									obj.MaintainUser,
									FormatHelper.ToDateString(obj.MaintainDate),
									FormatHelper.ToTimeString(obj.MaintainTime),
									obj.MEMO
								};
            }
        }

        private void _helper_GetExportHeadText(object sender, EventArgs e)
        {
            (e as ExportHeadEventArgsNew).Heads =
                new string[]{
								"RunningCard",
								"LotNO",
								"MOCode",
								"ItemCode",
								"ModelCode",
								"CheckItemCOde",
								"Grade",
								"OQCResult",
								"MaintainUser",
								"MaintainDate",
								"MaintainTime",
								"Memo",
							};
        }

        protected void cmdReturn_ServerClick(object sender, System.EventArgs e)
        {
            ArrayList keys = new ArrayList();
            ArrayList values = new ArrayList();

            for (int i = 0; i < this.Request.QueryString.AllKeys.Length; i++)
            {
                if (this.Request.QueryString.AllKeys.GetValue(i).ToString().StartsWith("12_"))
                {
                    keys.Add(this.Request.QueryString.AllKeys.GetValue(i).ToString());
                    values.Add(this.Request.QueryString[this.Request.QueryString.AllKeys.GetValue(i).ToString()]);
                }
            }

            this.Response.Redirect(
                this.MakeRedirectUrl(
                this.GetRequestParam("BackUrl"), (string[])keys.ToArray(typeof(string)), (string[])values.ToArray(typeof(string))));
        }
    }
}