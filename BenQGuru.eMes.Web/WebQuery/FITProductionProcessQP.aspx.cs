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
using BenQGuru.eMES.Web.UserControl;
using BenQGuru.eMES.WebQuery;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;

namespace BenQGuru.eMES.Web.WebQuery
{
    /// <summary>
    /// FItemTracingQP 的摘要说明。
    /// </summary>
    public partial class FITProductionProcessQP : BaseMPageNew
    {


        private System.ComponentModel.IContainer components;
        private ControlLibrary.Web.Language.LanguageComponent languageComponent1;

        private QueryFacade2 _facade = null;
        // FacadeFactory.CreateQueryFacade2() ;

        #region HisDataProvider

        BenQGuru.eMES.Common.Domain.IDomainDataProvider _hisDataProvider;
        /// <summary>
        /// 历史库的连接
        /// </summary>
        private BenQGuru.eMES.Common.Domain.IDomainDataProvider HisProvider
        {
            get
            {
                if (_hisDataProvider == null)
                {
                    _hisDataProvider = DomainDataProviderManager.DomainDataProvider(DBName.HIS);
                }
                return _hisDataProvider;
            }
        }
        #endregion


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
            //this.gridWebGrid.ClickCellButton += new Infragistics.WebUI.UltraWebGrid.ClickCellButtonEventHandler(this.gridWebGrid_ClickCellButton);
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

                string rcard = this.GetRequestParam("RCARD");
                int rcardseq;
                try
                {
                    rcardseq = int.Parse(this.GetRequestParam("RCARDSEQ"));
                }
                catch
                {
                    rcardseq = -1;
                }
                string moCode = this.GetRequestParam("MOCODE");

                this.ViewState["TCARD"] = this.GetRequestParam("TCARD");

                //				try
                //				{
                //					this.ViewState["IsHistoryDB"] = this.GetRequestParam("History") ;
                //				}
                //				catch
                //				{
                //					this.IsHistoryDB = false;
                //				}

                if (_facade == null)
                {
                    //_facade = new FacadeFactory(base.DataProvider).CreateQueryFacade2() ;
                    //					if(this.IsHistoryDB)
                    //					{
                    //						_facade = new FacadeFactory(this.HisProvider).CreateQueryFacade2() ;
                    //					}
                    //					else
                    //					{
                    _facade = new FacadeFactory(base.DataProvider).CreateQueryFacade2();
                    //					}
                }

                object obj = this._facade.GetProductionProcess(moCode, rcard, rcardseq);
                if (obj == null)
                {
                    ExceptionManager.Raise(this.GetType(), "$Error_ItemTracing_not_exist");
                }

                this.txtItem.Value = ((ProductionProcess)obj).ItemCode;
                this.txtMO.Value = ((ProductionProcess)obj).MOCode;
                this.txtModel.Value = ((ProductionProcess)obj).ModelCode;
                this.txtSN.Value = ((ProductionProcess)obj).RCard;
                this.txtSeq.Value = ((ProductionProcess)obj).RCardSequence.ToString();

                this.txtMO.Visible = false;
                this.lblMOQuery.Visible = false;


            }

        }

        protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
        {
            return this.languageComponent1;
        }
        #endregion

        //		private bool IsHistoryDB
        //		{
        //			get
        //			{
        //				try
        //				{
        //					return bool.Parse(this.ViewState["IsHistoryDB"].ToString());
        //				}
        //				catch
        //				{
        //					this.ViewState["IsHistoryDB"] = false.ToString();
        //
        //					return false;
        //				}
        //			}
        //			set
        //			{
        //				this.ViewState["IsHistoryDB"] = value.ToString();
        //			}
        //		}

        #region WebGrid
        protected override void InitWebGrid()
        {
            this.gridHelper.AddColumn("IT_MOCode", "工单", null);
            this.gridHelper.AddColumn("IT_RCode", "产品序列号", null);
            this.gridHelper.AddColumn("IT_Route", "生产途程", null);
            this.gridHelper.AddColumn("IT_OP", "工序", null);
            this.gridHelper.AddColumn("IT_ItemStatus", "产品状态", null);
            this.gridHelper.AddColumn("IT_OPType", "工序结果", null);
            this.gridHelper.AddColumn("IT_OPResult", "工序结果", null);
            this.gridHelper.AddColumn("IT_Segment", "工段", null);
            this.gridHelper.AddColumn("IT_Line", "生产线", null);
            this.gridHelper.AddColumn("IT_Resource", "资源", null);
            this.gridHelper.AddColumn("IT_MaintainDate", "日期", null);
            this.gridHelper.AddColumn("IT_MaintainTime", "时间", null);
            this.gridHelper.AddColumn("IT_MaintainUser", "操作工", null);

            this.gridHelper.AddColumn("IT_OPType_ORI", "工序结果", null);

            this.gridHelper.Grid.Columns.FromKey("IT_OPType_ORI").Hidden = true;
            this.gridHelper.Grid.Columns.FromKey("IT_OPType").Hidden = true;
            ((BoundDataField)this.gridHelper.Grid.Columns.FromKey("IT_OPResult")).HtmlEncode = false;
            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);

            base.InitWebGrid();

            this.gridHelper.RequestData();


        }

        protected override DataRow GetGridRow(object obj)
        {
            DataRow row = DtSource.NewRow();
            row["IT_MOCode"] = ((ProductionProcess)obj).MOCode.ToString();
            row["IT_RCode"] = ((ProductionProcess)obj).RCard.ToString();
            row["IT_Route"] = ((ProductionProcess)obj).RouteCode.ToString();
            row["IT_OP"] = ((ProductionProcess)obj).OPCode.ToString();
            row["IT_ItemStatus"] = this.languageComponent1.GetString(((ProductionProcess)obj).ItemStatus.ToString());
            row["IT_OPType"] = this.languageComponent1.GetString(((ProductionProcess)obj).OPType);
            row["IT_OPResult"] = GetOPTypeString((ProductionProcess)obj);
            row["IT_Segment"] = ((ProductionProcess)obj).SegmentCode.ToString();
            row["IT_Line"] = ((ProductionProcess)obj).LineCode.ToString();
            row["IT_Resource"] = ((ProductionProcess)obj).ResCode.ToString();
            row["IT_MaintainDate"] = FormatHelper.ToDateString(((ProductionProcess)obj).MaintainDate);
            row["IT_MaintainTime"] = FormatHelper.ToTimeString(((ProductionProcess)obj).MaintainTime);
            row["IT_MaintainUser"] = ((ProductionProcess)obj).MaintainUser.ToString();
            row["IT_OPType_ORI"] = ((ProductionProcess)obj).OPType.ToString();
            return row;

        }

        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            // SPEC要求这里只用序列号过滤,MO不再做为条件
            if (_facade == null)
            {
                //				if(this.IsHistoryDB)
                //				{
                //					_facade = new FacadeFactory(this.HisProvider).CreateQueryFacade2() ;
                //				}
                //				else
                //				{
                _facade = new FacadeFactory(base.DataProvider).CreateQueryFacade2();
                //				}
            }
            return this._facade.QueryProductionProcessForSplitboard(
                string.Empty,
                this.txtSN.Value,
                //this.ViewState["TCARD"].ToString(),
                inclusive, exclusive);
        }


        protected override int GetRowCount()
        {
            if (_facade == null)
            {
                //				if(this.IsHistoryDB)
                //				{
                //					_facade = new FacadeFactory(this.HisProvider).CreateQueryFacade2() ;
                //				}
                //				else
                //				{
                _facade = new FacadeFactory(base.DataProvider).CreateQueryFacade2();
                //				}
            }
            return this._facade.QueryProductionProcessCountForSplitboard(
                string.Empty,
                this.txtSN.Value//,
                //this.ViewState["TCARD"].ToString()
                );
        }

        #endregion

        #region Export
        protected override string[] FormatExportRecord(object obj)
        {
            return new string[]{
                                    ((ProductionProcess)obj).MOCode.ToString(),
									((ProductionProcess)obj).RCard.ToString(),
                                    ((ProductionProcess)obj).RouteCode.ToString(),
                                    ((ProductionProcess)obj).OPCode.ToString(),
                                    this.languageComponent1.GetString( ((ProductionProcess)obj).ItemStatus.ToString()),
                                    WebQueryHelper.GetOPResult(this.languageComponent1,((ProductionProcess)obj).Action),
                                    ((ProductionProcess)obj).SegmentCode,
                                    ((ProductionProcess)obj).LineCode.ToString(),
                                    ((ProductionProcess)obj).ResCode.ToString(),
                                    FormatHelper.ToDateString(((ProductionProcess)obj).MaintainDate),
                                    FormatHelper.ToTimeString(((ProductionProcess)obj).MaintainTime),
                                    ((ProductionProcess)obj).MaintainUser.ToString(),
                                }
                ;
        }

        protected override string[] GetColumnHeaderText()
        {
            return new string[] {
                                    "IT_MOCode",
									"IT_RCode",
                                    "IT_Route", 
                                    "IT_OP", 
                                    "IT_ItemStatus", 
                                    "IT_OPResult", 
                                    "IT_Segment", 
                                    "IT_Line", 
                                    "IT_Resource", 
                                    "IT_MaintainDate", 
                                    "IT_MaintainTime", 
                                    "IT_MaintainUser"
                                };
        }
        #endregion
        protected override void Grid_ClickCell(GridRecord row, string commandName)
        {
            string opType = row.Items.FindItemByKey("IT_OPType").Text;
            if (commandName == "IT_OPResult")
            {
                string gotoURL = string.Empty;

                if (opType == BenQGuru.eMES.Web.Helper.OPType.COMPLOADING)
                {
                    gotoURL = "FITOPResultComploadingQP.aspx";
                }

                if (opType == BenQGuru.eMES.Web.Helper.OPType.PACKING)
                {
                    gotoURL = "FITOPResultPackingQP.aspx";
                }

                if (opType == BenQGuru.eMES.Web.Helper.OPType.SN)
                {
                    gotoURL = "FITOPResultSNQP.aspx";
                }

                if (opType == BenQGuru.eMES.Web.Helper.OPType.TESTING)
                {
                    gotoURL = "FITOPResultTestingQP.aspx";
                }

                if (opType == BenQGuru.eMES.Web.Helper.OPType.TS)
                {
                    gotoURL = "FITOPResultTSQP.aspx";
                }

                Response.Redirect(string.Format("{0}?SN={1}", gotoURL, row.Items.FindItemByKey("IT_RCode").Text));
            }

        }

        protected void cmdReturn_ServerClick(object sender, System.EventArgs e)
        {
            string referedURL = this.GetRequestParam("REFEREDURL");
            if (referedURL == string.Empty)
            {
                referedURL = "FItemTracingQP.aspx";
            }
            else
            {
                referedURL = System.Web.HttpUtility.UrlDecode(referedURL);
            }
            Response.Redirect(referedURL);
        }

        private string GetOPTypeString(ProductionProcess obj)
        {
            return WebQueryHelper.GetOPResultLinkHtml2(this.languageComponent1, obj.Action, obj.RCard, obj.RCardSequence, obj.MOCode,
                string.Format(
                "FITProductionProcessQP.aspx?RCARD={0}&RCARDSEQ={1}&MOCODE={2}",
                this.txtSN.Value,
                int.Parse(this.txtSeq.Value),
                this.txtMO.Value
                )
            );
        }

        protected override void OnUnload(EventArgs e)
        {
            //			if(this.HisProvider!=null)
            //			{
            //				((SQLDomainDataProvider)HisProvider).PersistBroker.CloseConnection();
            //			}
            base.OnUnload(e);
        }
    }
}
