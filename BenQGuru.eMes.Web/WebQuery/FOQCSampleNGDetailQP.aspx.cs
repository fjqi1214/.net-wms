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
using BenQGuru.eMES.Domain.TSModel;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.WebQuery;
using BenQGuru.eMES.TS;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;

namespace BenQGuru.eMES.Web.WebQuery
{
    /// <summary>
    /// FOQCSampleNGDetailQP 的摘要说明。
    /// </summary>
    public partial class FOQCSampleNGDetailQP : BaseQPageNew
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

        private string lotno
        {
            get
            {
                if (this.ViewState["LotNo"] != null)
                {
                    try
                    {
                        return this.ViewState["LotNo"].ToString();
                    }
                    catch
                    {
                        return string.Empty;
                    }
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                this.ViewState["LotNo"] = value;
            }
        }

        private string rcard
        {
            get
            {
                if (this.ViewState["RunningCard"] != null)
                {
                    try
                    {
                        return this.ViewState["RunningCard"].ToString();
                    }
                    catch
                    {
                        return string.Empty;
                    }
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                this.ViewState["RunningCard"] = value;
            }
        }

        private string rcardseq
        {
            get
            {
                if (this.ViewState["RunningCardSeq"] != null)
                {
                    try
                    {
                        return this.ViewState["RunningCardSeq"].ToString();
                    }
                    catch
                    {
                        return string.Empty;
                    }
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                this.ViewState["RunningCardSeq"] = value;
            }
        }

        private string mocode
        {
            get
            {
                if (this.ViewState["MoCode"] != null)
                {
                    try
                    {
                        return this.ViewState["MoCode"].ToString();
                    }
                    catch
                    {
                        return string.Empty;
                    }
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                this.ViewState["MoCode"] = value;
            }
        }

        #endregion

        protected void Page_Load(object sender, System.EventArgs e)
        {
            this.gridHelper = new GridHelperNew(this.gridWebGrid, this.DtSource);
            this._initialParamter();

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


            this.ViewState["LotNo"] = this.GetRequestParam("LotNo");
            this.ViewState["RunningCard"] = this.GetRequestParam("RunningCard");
            this.ViewState["RunningCardSeq"] = this.GetRequestParam("RunningCardSeq");
            this.ViewState["MoCode"] = this.GetRequestParam("MoCode");

            this.txtSnQuery.Text = this.rcard;
            if (this.rcard != string.Empty)
            {
                this.ExecuteClientFunction("setDisplay", string.Empty);
            }
        }

        private void _initialWebGrid()
        {
            base.InitWebGrid();
            this._gridHelper.AddColumn("LotNO", "批号", null);
            this._gridHelper.AddColumn("RunningCard", "序列号", null);
            this._gridHelper.AddColumn("ErrorCodeGroup", "不良代码组", null);
            this._gridHelper.AddColumn("ErrorCodeDescription", "不良描述", null);
            this._gridHelper.AddColumn("ErrorCauseGroupCode", "不良原因组", null);
            this._gridHelper.AddColumn("ErrorCause", "不良原因", null);
            this._gridHelper.AddColumn("ErrorLocation", "不良位置", null);
            this._gridHelper.AddColumn("ErrorComponent", "不良组件", null);
            this._gridHelper.AddColumn("ErrorParts", "不良元件", null);
            this._gridHelper.AddColumn("Solution", "解决方案", null);
            this._gridHelper.AddColumn("Duty", "责任别", null);
            this._gridHelper.AddColumn("SolutionMemo", "预防措施", null);
            this._gridHelper.AddColumn("Memo", "补充说明", null);
            this._gridHelper.AddColumn("TsOperator", "维修工", null);
            this._gridHelper.AddColumn("MaintainDate", "维修日期", null);
            this._gridHelper.AddColumn("MaintainTime", "维修时间", null);
            //this._gridHelper.AddColumn( "FrmMemo",		"备注",	null);

            //多语言
            this._gridHelper.ApplyLanguage(this.languageComponent1);
            this.gridWebGrid.Columns.FromKey("Memo").Hidden = true;

            //this.gridWebGrid.Columns.FromKey("LotNO").MergeCells = true;
            //this.gridWebGrid.Columns.FromKey("RunningCard").MergeCells = true;
            //this.gridWebGrid.Columns.FromKey("ErrorCauseGroupCode").MergeCells = true;
            //this.gridWebGrid.Columns.FromKey("ErrorCodeGroup").MergeCells = true;
            //this.gridWebGrid.Columns.FromKey("ErrorCodeDescription").MergeCells = true;
            //this.gridWebGrid.Columns.FromKey("ErrorCause").MergeCells = true;
            //this.gridWebGrid.Columns.FromKey("SolutionMemo").MergeCells = true;
            //this.gridWebGrid.Columns.FromKey("Solution").MergeCells = true;
            //this.gridWebGrid.Columns.FromKey("Duty").MergeCells = true;
            //this.gridWebGrid.Columns.FromKey("ErrorComponent").MergeCells = true;
            //this.gridWebGrid.Columns.FromKey("Memo").MergeCells = true;
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
                FacadeFactory facadeFactory = new FacadeFactory(base.DataProvider);
                (e as WebQueryEventArgsNew).GridDataSource =
                    facadeFactory.CreateQueryTSDetailsFacade().QueryOQCSampleNGDetails(
                    this.lotno,
                    this.rcard,
                    this.rcardseq,
                    this.mocode,
                    (e as WebQueryEventArgsNew).StartRow,
                    (e as WebQueryEventArgsNew).EndRow);

                (e as WebQueryEventArgsNew).RowCount =
                    facadeFactory.CreateQueryTSDetailsFacade().QueryOQCSampleNGDetailsCount(
                    this.lotno,
                    this.rcard,
                    this.rcardseq,
                    this.mocode);

            }
        }

        private void _helper_DomainObjectToGridRow(object sender, EventArgs e)
        {
            if ((e as DomainObjectToGridRowEventArgsNew).DomainObject != null)
            {
                QDOTSDetails1 obj = (e as DomainObjectToGridRowEventArgsNew).DomainObject as QDOTSDetails1;

                TSFacade tsFacade = new TSFacade(this.DataProvider);
                string errorCauseGroupDescription = obj.ErrorCauseGroupCode;

                object errorCauseGroup = tsFacade.GetErrorCauseGroup(obj.ErrorCauseGroupCode);

                if (errorCauseGroup != null)
                {
                    errorCauseGroupDescription = ((ErrorCauseGroup)errorCauseGroup).ErrorCauseGroupDescription;
                }

                DataRow row = this.DtSource.NewRow();
                row["LotNO"] = this.lotno;
                row["RunningCard"] = obj.RunningCard;
                row["ErrorCodeGroup"] = obj.ErrorCodeGroupDescription;
                row["ErrorCodeDescription"] = obj.ErrorCodeDescription;
                row["ErrorCauseGroupCode"] = errorCauseGroupDescription;
                row["ErrorCause"] = obj.ErrorCauseDescription;
                row["ErrorLocation"] = obj.ErrorLocation;
                row["ErrorComponent"] = obj.ErrorComponent;
                row["ErrorParts"] = obj.ErrorParts;
                row["Solution"] = obj.SolutionDescription;
                row["Duty"] = obj.DutyDescription;
                row["SolutionMemo"] = obj.Solution;
                row["Memo"] = obj.Memo;
                row["TsOperator"] = obj.TSOperator;
                row["MaintainDate"] = FormatHelper.ToDateString(obj.TSDate);
                row["MaintainTime"] = FormatHelper.ToTimeString(obj.TSTime);

                (e as DomainObjectToGridRowEventArgsNew).GridRow = row;
                //new UltraGridRow(new object[]{
                //                                      this.lotno ,
                //                                      obj.RunningCard ,
                //                                      obj.ErrorCodeGroupDescription,
                //                                      obj.ErrorCodeDescription,
                //                                      errorCauseGroupDescription,
                //                                      obj.ErrorCauseDescription,
                //                                      obj.ErrorLocation,
                //                                      obj.ErrorComponent,
                //                                      obj.ErrorParts,
                //                                      obj.SolutionDescription,
                //                                      obj.DutyDescription,
                //                                      obj.Solution,								  
                //                                      obj.Memo,
                //                                      obj.TSOperator,
                //                                      FormatHelper.ToDateString(obj.TSDate),
                //                                      FormatHelper.ToTimeString(obj.TSTime),
                //                                      obj.FrmMemo
                //                                  }
                //);
            }
        }

        private void _helper_DomainObjectToExportRow(object sender, EventArgs e)
        {
            if ((e as DomainObjectToExportRowEventArgsNew).DomainObject != null)
            {
                QDOTSDetails1 obj = (e as DomainObjectToExportRowEventArgsNew).DomainObject as QDOTSDetails1;
                (e as DomainObjectToExportRowEventArgsNew).ExportRow =
                    new string[]{
									this.lotno ,
									obj.RunningCard ,
									obj.ErrorCodeGroupDescription,
									obj.ErrorCodeDescription,
                                    obj.ErrorCauseGroupCode,
									obj.ErrorCauseDescription,
									obj.ErrorLocation,
                                    obj.ErrorComponent,
									obj.ErrorParts,
									obj.SolutionDescription,
									obj.DutyDescription,
						            obj.Solution,							  
									obj.Memo,
									obj.TSOperator,
									FormatHelper.ToDateString(obj.TSDate),
									FormatHelper.ToTimeString(obj.TSTime)
								};
            }
        }

        private void _helper_GetExportHeadText(object sender, EventArgs e)
        {
            (e as ExportHeadEventArgsNew).Heads =
                new string[]{
								"LotNO",
								"RunningCard",
								"ErrorCodeGroup",
								"ErrorCodeDescription",
                                "ErrorCauseGroupCode",
								"ErrorCause",
								"ErrorLocation",
                                "ErrorComponent",
								"ErrorParts",
								"Solution",
								"Duty",
                                "SolutionMemo",
								"Memo",
								"TsOperator",
								"MaintainDate",
								"MaintainTime"
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


        /// <summary>
        /// 执行客户端的函数
        /// </summary>
        /// <param name="FunctionName">函数名</param>
        /// <param name="FunctionParam">参数</param>
        /// <param name="Page">当前页面的引用</param>
        public void ExecuteClientFunction(string FunctionName, string FunctionParam)
        {
            try
            {
                string _msg = string.Empty;
                if (FunctionParam != string.Empty)
                    _msg = string.Format("<script language='JavaScript'>  {0}('{1}');</script>", FunctionName, FunctionParam);
                else
                    _msg = string.Format("<script language='JavaScript'>  {0}();</script>", FunctionName);

                //将Key值设为随机数,防止脚本重复
                Page.RegisterStartupScript(Guid.NewGuid().ToString(), _msg);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}