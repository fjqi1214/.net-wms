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
using BenQGuru.eMES.Domain.ArmorPlate;
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
    public partial class FArmorPlateQP : BaseQPageNew
    {
        protected BenQGuru.eMES.Web.Helper.OWCChartSpace OWCChartSpace1;
        protected System.Web.UI.HtmlControls.HtmlInputButton Submit2;
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;
        protected ExcelExporter excelExporter = null;
        protected WebQueryHelperNew _helper = null;
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

                txtDateFrom.Text = FormatHelper.ToDateString(FormatHelper.TODateInt(DateTime.Today));
                txtDateTo.Text = FormatHelper.ToDateString(FormatHelper.TODateInt(DateTime.Today));

                this._initialWebGrid();
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
            this.gridHelper.AddColumn("ArmorPlateID", "厂内编号", 120);
            this.gridHelper.AddColumn("BasePlateCode", "基板料号", 120);
            this.gridHelper.AddColumn("CurrentVersion", "版本", 120);
            this.gridHelper.AddColumn("TensionA", "张力A", 120);
            this.gridHelper.AddColumn("TensionB", "张力B", 120);
            this.gridHelper.AddColumn("TensionC", "张力C", 120);
            this.gridHelper.AddColumn("TensionD", "张力D", 120);
            this.gridHelper.AddColumn("TensionE", "张力E", 120);
            this.gridHelper.AddColumn("UMOCode", "领用工单", 120);
            this.gridHelper.AddColumn("USSCode", "领用产线", 120);
            this.gridHelper.AddColumn("UUser", "领用人员", 120);
            this.gridHelper.AddColumn("UDate", "领用日期", 120);
            this.gridHelper.AddColumn("UTime", "领用时间", 120);
            this.gridHelper.AddColumn("UsedTimes", "使用次数", 120);
            this.gridHelper.AddColumn("ReUser", "退回人员", 120);
            this.gridHelper.AddColumn("ReDate", "退回日期", 120);
            this.gridHelper.AddColumn("ReTime", "退回时间", 120);

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
            PageCheckManager manager = new PageCheckManager();

            //			manager.Add( new DateRangeCheck(this.chbFrmDate,this.txtDateFrom.Text,lblFrmDateT,txtDateTo.Text,0,7,true));

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

                //if( this._checkRequireFields() )
                //{
                //    return;
                //}
            }

            FacadeFactory facadeFactory = new FacadeFactory(base.DataProvider);
            object[] dataSource = facadeFactory.CreateArmorPlateFacade().QueryArmorPlateContol(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtAPIDQuery.Text)),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtBasePlateQuery.Text)),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtMOQuery.Text)),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtLineQuery.Text)),
                beginDate,
                endDate,
                (e as WebQueryEventArgsNew).StartRow,
                (e as WebQueryEventArgsNew).EndRow);

            (e as WebQueryEventArgsNew).GridDataSource = dataSource;

            (e as WebQueryEventArgsNew).RowCount = facadeFactory.CreateArmorPlateFacade().QueryArmorPlateContolCount(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtAPIDQuery.Text)),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtBasePlateQuery.Text)),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtMOQuery.Text)),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtLineQuery.Text)),
                beginDate,
                endDate);
        }

        private void _helper_DomainObjectToGridRow(object sender, EventArgs e)
        {
            if ((e as DomainObjectToGridRowEventArgsNew).DomainObject != null)
            {
                ArmorPlateContol obj = (e as DomainObjectToGridRowEventArgsNew).DomainObject as ArmorPlateContol;
                DataRow row = DtSource.NewRow();
                row["ArmorPlateID"] = obj.ArmorPlateID.ToString();
                row["BasePlateCode"] = obj.BasePlateCode.ToString();
                row["CurrentVersion"] = obj.Version.ToString();
                row["TensionA"] = obj.TensionA.ToString();
                row["TensionB"] = obj.TensionB.ToString();
                row["TensionC"] = obj.TensionC.ToString();
                row["TensionD"] = obj.TensionD.ToString();
                row["TensionE"] = obj.TensionE.ToString();
                row["UMOCode"] = obj.UsedMOCode.ToString();
                row["USSCode"] = obj.UsedSSCode.ToString();
                row["UUser"] = obj.UsedUser.ToString();
                row["UDate"] = FormatHelper.ToDateString(obj.UsedDate);
                row["UTime"] = FormatHelper.ToTimeString(obj.UsedTime);
                row["UsedTimes"] = obj.UsedTimesInMO.ToString("##.##");
                row["ReUser"] = obj.ReturnUser.ToString();
                row["ReDate"] = FormatHelper.ToDateString(obj.ReturnDate);
                row["ReTime"] = FormatHelper.ToTimeString(obj.ReturnTime);
                (e as DomainObjectToGridRowEventArgsNew).GridRow = row;

            }
        }

        private void _helper_DomainObjectToExportRow(object sender, EventArgs e)
        {
            if ((e as DomainObjectToExportRowEventArgsNew).DomainObject != null)
            {
                ArmorPlateContol obj = (e as DomainObjectToExportRowEventArgsNew).DomainObject as ArmorPlateContol;

                (e as DomainObjectToExportRowEventArgsNew).ExportRow =
                    new string[]{
									obj.ArmorPlateID.ToString(),
									obj.BasePlateCode.ToString(),
									obj.Version.ToString(),

									obj.TensionA.ToString(),
									obj.TensionB.ToString(),
									obj.TensionC.ToString(),
									obj.TensionD.ToString(),
									obj.TensionE.ToString(),

									obj.UsedMOCode.ToString(),
									obj.UsedSSCode.ToString(),
									obj.UsedUser.ToString(),													   
									FormatHelper.ToDateString( obj.UsedDate ),
									FormatHelper.ToTimeString( obj.UsedTime ),
									obj.UsedTimesInMO.ToString("##.##"),

									obj.ReturnUser.ToString(),
									FormatHelper.ToDateString( obj.ReturnDate ),
									FormatHelper.ToTimeString( obj.ReturnTime )
								};
            }
        }

        private void _helper_GetExportHeadText(object sender, EventArgs e)
        {
            (e as ExportHeadEventArgsNew).Heads =
                new string[]{
								"ArmorPlateID",
								"BasePlateCode",
								"CurrentVersion",

								"TensionA",
								"TensionB",	
								"TensionC",	
								"TensionD",	
								"TensionE",	
		
								"UMOCode",						
								"USSCode",	
								"UUser",	
								"UDate",					
								"UTime",						
								"UsedTimes",	
					
								"ReUser",
								"ReDate",					
								"ReTime"
							};
        }

    }
}
