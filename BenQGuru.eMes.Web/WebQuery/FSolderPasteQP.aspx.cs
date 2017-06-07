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
using BenQGuru.eMES.Domain.SolderPaste;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.WebQuery;
using BenQGuru.eMES.Web.UserControl;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;

namespace BenQGuru.eMES.Web.WebQuery
{
    /// <summary>
    /// FTSInfoQP 的摘要说明。
    /// </summary>
    public partial class FSolderPasteQP : BaseQPageNew
    {
        protected BenQGuru.eMES.Web.Helper.OWCChartSpace OWCChartSpace1;
        protected System.Web.UI.HtmlControls.HtmlInputButton Submit2;
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;
        protected ExcelExporter excelExporter = null;
        protected WebQueryHelperNew _helper = null;
        protected System.Web.UI.WebControls.Label lblErrorCodeGroup;
        protected System.Web.UI.WebControls.Label lblErrorCode;
        protected System.Web.UI.WebControls.Label lblErrorCause;
        protected System.Web.UI.WebControls.Label lblErrorLocation;
        protected System.Web.UI.WebControls.Label lblErrorDuty;
        protected System.Web.UI.WebControls.TextBox txtErrorCodeGroup;
        protected System.Web.UI.WebControls.TextBox txtErrorCode;
        protected System.Web.UI.WebControls.TextBox txtErrorCause;
        protected System.Web.UI.WebControls.TextBox txtErrorLocation;
        protected System.Web.UI.WebControls.TextBox txtErrorDuty;
        //protected GridHelper gridHelper = null;	

        protected global::System.Web.UI.WebControls.TextBox txtDateFrom;
        protected global::System.Web.UI.WebControls.TextBox txtDateTo;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            this.gridHelper = new GridHelperNew(this.gridWebGrid, DtSource);

            if (!this.IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);

                this._initialWebGrid();

                txtDateFrom.Text = FormatHelper.ToDateString(FormatHelper.TODateInt(DateTime.Today));
                txtDateTo.Text = FormatHelper.ToDateString(FormatHelper.TODateInt(DateTime.Today));

                this.drpSPStatusQuery.Items.Clear();
                this.drpSPStatusQuery.Items.Add(new ListItem("", ""));
                new DropDownListBuilder2(new SolderPasteStatus(), this.drpSPStatusQuery, this.languageComponent1).Build();
            }

            this._helper = new WebQueryHelperNew(this.cmdQuery, this.cmdGridExport, this.gridWebGrid, this.pagerSizeSelector, this.pagerToolBar, this.languageComponent1, DtSource);
            this._helper.LoadGridDataSource += new EventHandler(_helper_LoadGridDataSource);
            this._helper.DomainObjectToGridRow += new EventHandler(_helper_DomainObjectToGridRow);
            this._helper.DomainObjectToExportRow += new EventHandler(_helper_DomainObjectToExportRow);
            this._helper.GetExportHeadText += new EventHandler(_helper_GetExportHeadText);
        }

        private void _initialWebGrid()
        {
            base.InitWebGrid();
            this.gridHelper.AddColumn("SolderPasteID", "锡膏编码", 120);
            this.gridHelper.AddColumn("UMOCode", "领用工单", 120);
            this.gridHelper.AddColumn("USSCode", "领用产线", 120);
            this.gridHelper.AddColumn("ReturnDate", "回温日期", 120);
            this.gridHelper.AddColumn("ReturnTime", "回温时间", 120);
            this.gridHelper.AddColumn("ReturnUser", "回温人员", 120);
            this.gridHelper.AddColumn("ReturnTimer", "回温计时", 120);
            this.gridHelper.AddColumn("MixDate", "搅拌日期", 120);
            this.gridHelper.AddColumn("MixTime", "搅拌时间", 120);
            this.gridHelper.AddColumn("MixUser", "搅拌人员", 120);
            this.gridHelper.AddColumn("UnopenTimer", "未开封计时", 120);
            this.gridHelper.AddColumn("OpenDate", "开封日期", 120);
            this.gridHelper.AddColumn("OpenTime", "开封时间", 120);
            this.gridHelper.AddColumn("OpenUser", "开封人员", 120);
            this.gridHelper.AddColumn("OpenTimer", "开封计时", 120);
            this.gridHelper.AddColumn("ReflowDate", "回存日期", 120);
            this.gridHelper.AddColumn("ReflowTime", "回存时间", 120);
            this.gridHelper.AddColumn("ReflowUser", "回存人员", 120);
            this.gridHelper.AddColumn("Status", "状态", 120);
            this.gridHelper.AddColumn("Meno", "备注", 120);

            this.gridHelper.AddColumn("MUSER", "维护人员", 120);
            this.gridHelper.AddColumn("MDATE", "维护日期", 120);
            this.gridHelper.AddColumn("MTIME", "维护时间", 120);

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
            this.languageComponent1.LanguagePackageDir = "D:\\SQC2.0\\eMES\\Source\\bin";
            this.languageComponent1.RuntimePage = null;
            this.languageComponent1.RuntimeUserControl = null;
            this.languageComponent1.UserControlName = "";

        }
        #endregion

        private bool _checkRequireFields()
        {
            PageCheckManager manager = new PageCheckManager();

            if (!manager.Check())
            {
                WebInfoPublish.Publish(this, manager.CheckMessage, this.languageComponent1);
                return false;
            }
            return true;
        }


        private void _helper_LoadGridDataSource(object sender, EventArgs e)
        {
            //			this._initialWebGrid();

            string beginDate = String.Empty;
            string endDate = String.Empty;
            if (chbFrmDate.Checked == true)
            {
                beginDate = FormatHelper.CleanString(FormatHelper.TODateInt(this.txtDateFrom.Text).ToString()).ToUpper();
                endDate = FormatHelper.CleanString(FormatHelper.TODateInt(this.txtDateTo.Text).ToString()).ToUpper();

                if (!this._checkRequireFields())
                {
                    return;
                }
            }

            FacadeFactory facadeFactory = new FacadeFactory(base.DataProvider);
            object[] dataSource = facadeFactory.CreateQuerySolderPasteFacade().QuerySolderPastePro(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtMOQuery.Text)),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtSSQuery.Text)),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtSPIDQuery.Text)),
                drpSPStatusQuery.SelectedValue,
                beginDate,
                endDate,
                (e as WebQueryEventArgsNew).StartRow,
                (e as WebQueryEventArgsNew).EndRow);

            (e as WebQueryEventArgsNew).GridDataSource = dataSource;

            (e as WebQueryEventArgsNew).RowCount = facadeFactory.CreateQuerySolderPasteFacade().QuerySolderPasteProCount(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtMOQuery.Text)),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtSSQuery.Text)),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtSPIDQuery.Text)),
                drpSPStatusQuery.SelectedValue,
                beginDate,
                endDate);
        }

        private void _helper_DomainObjectToGridRow(object sender, EventArgs e)
        {
            if ((e as DomainObjectToGridRowEventArgsNew).DomainObject != null)
            {
                SOLDERPASTEPRO obj = (e as DomainObjectToGridRowEventArgsNew).DomainObject as SOLDERPASTEPRO;
                DataRow row = DtSource.NewRow();
                row["SolderPasteID"] = obj.SOLDERPASTEID.ToString();
                row["UMOCode"] = obj.MOCODE.ToString();
                row["USSCode"] = obj.LINECODE.ToString();
                row["ReturnDate"] = FormatHelper.ToDateString(obj.RETURNDATE);
                row["ReturnTime"] = FormatHelper.ToTimeString(obj.RETURNTIME);
                row["ReturnUser"] = obj.RETRUNUSER.ToString();
                row["ReturnTimer"] = TimeSpanString(obj.RETURNCOUNTTIME);
                row["MixDate"] = FormatHelper.ToDateString(obj.AGITATEDATE);
                row["MixTime"] = FormatHelper.ToTimeString(obj.AGITATETIME);
                row["MixUser"] = obj.AGITAEUSER.ToString();
                row["UnopenTimer"] = TimeSpanString(obj.VEILCOUNTTIME);
                row["OpenDate"] = FormatHelper.ToDateString(obj.UNVEILMDATE);
                row["OpenTime"] = FormatHelper.ToTimeString(obj.UNVEILTIME);
                row["OpenUser"] = obj.UNVEILUSER.ToString();
                row["OpenTimer"] = TimeSpanString(obj.UNVEILCOUNTTIME);
                row["ReflowDate"] = FormatHelper.ToDateString(obj.RESAVEDATE);
                row["ReflowTime"] = FormatHelper.ToTimeString(obj.RESAVETIME);
                row["ReflowUser"] = obj.RESAVEUSER.ToString();
                row["Status"] = this.languageComponent1.GetString(obj.STATUS.ToString());
                row["Meno"] = obj.MEMO.ToString();
                row["MUSER"] = obj.MUSER.ToString();
                row["MDATE"] = FormatHelper.ToDateString(obj.MDATE);
                row["MTIME"] = FormatHelper.ToTimeString(obj.MTIME);
                (e as DomainObjectToGridRowEventArgsNew).GridRow = row;
                }
        }

        private void _helper_DomainObjectToExportRow(object sender, EventArgs e)
        {
            if ((e as DomainObjectToExportRowEventArgsNew).DomainObject != null)
            {
                SOLDERPASTEPRO obj = (e as DomainObjectToExportRowEventArgsNew).DomainObject as SOLDERPASTEPRO;

                (e as DomainObjectToExportRowEventArgsNew).ExportRow =
                    new string[]{
									obj.SOLDERPASTEID.ToString(),
									obj.MOCODE.ToString(),
									obj.LINECODE.ToString(),

									FormatHelper.ToDateString( obj.RETURNDATE ),
									FormatHelper.ToTimeString( obj.RETURNTIME ),
									obj.RETRUNUSER.ToString(),
									TimeSpanString( obj.RETURNCOUNTTIME ),

									FormatHelper.ToDateString( obj.AGITATEDATE ),
									FormatHelper.ToTimeString( obj.AGITATETIME ),
									obj.AGITAEUSER.ToString(),

									TimeSpanString( obj.VEILCOUNTTIME),

									FormatHelper.ToDateString( obj.UNVEILMDATE ),
									FormatHelper.ToTimeString( obj.UNVEILTIME ),
									obj.UNVEILUSER.ToString(),
									TimeSpanString( obj.UNVEILCOUNTTIME ),

									FormatHelper.ToDateString( obj.RESAVEDATE ),
									FormatHelper.ToTimeString( obj.RESAVETIME ),
									obj.RESAVEUSER.ToString(),

									this.languageComponent1.GetString( obj.STATUS.ToString() ),
									obj.MEMO.ToString(),

									obj.MUSER.ToString(),
									FormatHelper.ToDateString( obj.MDATE ),
									FormatHelper.ToTimeString( obj.MTIME )
								};
            }
        }

        private void _helper_GetExportHeadText(object sender, EventArgs e)
        {
            (e as ExportHeadEventArgsNew).Heads =
                new string[]{
								"SolderPasteID",
								"UMOCode",
								"USSCode",

								"ReturnDate",
								"ReturnTime",	
								"ReturnUser",	
								"ReturnTimer",	
		
								"MixDate",			
								"MixTime",						
								"MixUser",	
					
								"UnopenTimer",	
				
								"OpenDate",					
								"OpenTime",						
								"OpenUser",						
								"OpenTimer",
						
								"ReflowDate",					
								"ReflowTime",					
								"ReflowUser",	
				
								"Status",						
								"Meno",						
	
								"MUSER",				
								"MDATE",						
								"MTIME"
							};
        }

        private string TimeSpanString(decimal timeSpan)
        {
            if (timeSpan > 0)
            {

                string[] Rtimes = timeSpan.ToString().TrimEnd(new char[] { '0' }).Split('.');

                int iRCountHour = 0, iRCountMinutes = 0;
                //int iVCountHour = 0,iVCountMinutes = 0;


                try
                {
                    iRCountHour = int.Parse(Rtimes[0]);
                }
                catch
                { }

                if (Rtimes.Length > 1)
                {
                    try
                    {
                        iRCountMinutes = Convert.ToInt32(System.Math.Round(Convert.ToDouble(int.Parse(Rtimes[1]))) / 100 * 60);
                    }
                    catch { }
                }

                //				string hour = "0";
                //				if(decimal.Truncate(timeSpan)>0)
                //				{
                //				hour = iRCountHour;
                //				}

                //				decimal minute = (timeSpan-decimal.Truncate(timeSpan))*60;
                //
                //				string min = "0";
                //				if(minute>0)
                //				{
                //				min = iRCountMinutes;
                //				}
                return string.Format("{0}H{1}M", iRCountHour.ToString(), iRCountMinutes.ToString());
            }

            return "";
        }

    }
}
