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


using Infragistics.WebUI.UltraWebGrid;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.TSModel;
using BenQGuru.eMES.Domain.TSModel;

namespace BenQGuru.eMES.Web.TSModel
{
    public partial class FTSSmartConfigMP : BaseMPage
    {		
        private System.ComponentModel.IContainer components;
		private ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		
        protected BenQGuru.eMES.TSModel.TSModelFacade _facade;// = TSModelFacadeFactory.CreateTSModelFacade();
	
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
			if( !this.IsPostBack )
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
            this.gridHelper.AddColumn( "ErrorCodeA", "不良代码",	null);
            this.gridHelper.AddColumn( "EnabledSmartTS", "允许智能维修",	null);
            this.gridHelper.AddColumn("SortBy", "排序方式", null);
            this.gridHelper.AddColumn("SmartDateRange", "统计时间范围", null);
            this.gridHelper.AddColumn("ShowItemCount", "显示条目数", null);
            this.gridHelper.AddColumn( "MaintainUser", "维护人员",	null);
            this.gridHelper.AddColumn( "MaintainDate", "维护日期",	null);
            this.gridHelper.AddColumn( "MaintainTime", "维护时间",	null);
            this.gridHelper.AddLinkColumn("Detail", "当前明细", null);
            this.gridHelper.AddColumn("Sequence", "Sequence", null);

            this.gridWebGrid.Columns.FromKey("Sequence").Hidden = true;
            this.gridHelper.AddDefaultColumn( true, true );
            
            this.gridHelper.ApplyLanguage( this.languageComponent1 );
        }
		
        protected override Infragistics.WebUI.UltraWebGrid.UltraGridRow GetGridRow(object obj)
        {
            TSSmartConfig smartCfg = (TSSmartConfig)obj;
            return new Infragistics.WebUI.UltraWebGrid.UltraGridRow( 
                new object[]{"false",
                                smartCfg.ErrorCode,
                                this.languageComponent1.GetString(smartCfg.Enabled),
                                this.drpSortBy.Items.FindByValue(smartCfg.SortBy).Text,
                                Convert.ToInt32(smartCfg.DateRange).ToString() + " " + this.drpSmartDateRangeType.Items.FindByValue(smartCfg.DateRangeType).Text,
                                smartCfg.ShowItemCount.ToString(),
                                smartCfg.MaintainUser.ToString(),
                                FormatHelper.ToDateString(smartCfg.MaintainDate),
                                FormatHelper.ToTimeString(smartCfg.MaintainTime),
                                "",
                                smartCfg.Sequence,
                                ""});
        }

        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
			if(_facade==null){_facade = new TSModelFacadeFactory(base.DataProvider).CreateTSModelFacade();}
            return this._facade.QueryTSSmartConfig( 
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtErrorCodeQuery.Text)),
                inclusive, exclusive );
        }


        protected override int GetRowCount()
        {
			if(_facade==null){_facade = new TSModelFacadeFactory(base.DataProvider).CreateTSModelFacade();}
            return this._facade.QueryTSSmartConfigCount( 
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtErrorCodeQuery.Text))
                );
        }

        #endregion

		#region Button

		protected override void AddDomainObject(object domainObject)
		{		
			if(_facade==null){_facade = new TSModelFacadeFactory(base.DataProvider).CreateTSModelFacade();}
            this._facade.AddTSSmartConfig((TSSmartConfig)domainObject);
		}

		protected override void DeleteDomainObjects(ArrayList domainObjects)
		{
			if(_facade==null){_facade = new TSModelFacadeFactory(base.DataProvider).CreateTSModelFacade();}
            this._facade.DeleteTSSmartConfig((TSSmartConfig[])domainObjects.ToArray(typeof(TSSmartConfig)));
		}

		protected override void UpdateDomainObject(object domainObject)
		{
			if(_facade==null){_facade = new TSModelFacadeFactory(base.DataProvider).CreateTSModelFacade();}
            this._facade.UpdateTSSmartConfig((TSSmartConfig)domainObject);
		}

		protected override void buttonHelper_AfterPageStatusChangeHandle( string pageAction )
		{
			if ( pageAction == PageActionType.Add )
			{
				this.txtErrorCodeEdit.ReadOnly = false;
			}

			if ( pageAction == PageActionType.Update )
			{
				this.txtErrorCodeEdit.ReadOnly = true;
			}
		}
		#endregion

        #region Object <--> Page

        protected override object GetEditObject()
        {
			if(_facade==null){_facade = new TSModelFacadeFactory(base.DataProvider).CreateTSModelFacade();}
            TSSmartConfig smartCfg = this._facade.CreateNewTSSmartConfig();

            if (this.txtSequence.Text != "")
            {
                smartCfg.Sequence = decimal.Parse(this.txtSequence.Text);
            }
            smartCfg.ErrorCode = FormatHelper.CleanString(FormatHelper.PKCapitalFormat(this.txtErrorCodeEdit.Text), 40);
            smartCfg.Enabled = FormatHelper.BooleanToString(this.chkEnabledSmart.Checked);
            smartCfg.SortBy = this.drpSortBy.SelectedValue;
            smartCfg.DateRange = decimal.Parse(this.txtSmartDateRange.Text);
            smartCfg.DateRangeType = this.drpSmartDateRangeType.SelectedValue;
            smartCfg.ShowItemCount = decimal.Parse(this.txtShowItemCount.Text);
            smartCfg.MaintainUser = this.GetUserCode();

            return smartCfg;
        }


        protected override object GetEditObject(Infragistics.WebUI.UltraWebGrid.UltraGridRow row)
        {	
			if(_facade==null){_facade = new TSModelFacadeFactory(base.DataProvider).CreateTSModelFacade();}
            object obj = _facade.GetTSSmartConfig(decimal.Parse(row.Cells.FromKey("Sequence").Text.ToString()));
			
            if (obj != null)
            {
                return (TSSmartConfig)obj;
            }

            return null;
        }

        protected override void SetEditObject(object obj)
        {
            if (obj == null)
            {
                this.txtErrorCodeEdit.Text	= "";
                this.chkEnabledSmart.Checked = false;
                this.drpSortBy.SelectedIndex = 0;
                this.txtSmartDateRange.Text = "0";
                this.drpSmartDateRangeType.SelectedIndex = 0;
                this.txtShowItemCount.Text = "0";
                this.txtSequence.Text = "0";

                return;
            }

            TSSmartConfig smartCfg = (TSSmartConfig)obj;
            this.txtSequence.Text = smartCfg.Sequence.ToString();
            this.txtErrorCodeEdit.Text = smartCfg.ErrorCode.ToString();
            this.chkEnabledSmart.Checked = FormatHelper.StringToBoolean(smartCfg.Enabled);
            this.drpSortBy.SelectedIndex = this.drpSortBy.Items.IndexOf(this.drpSortBy.Items.FindByValue(smartCfg.SortBy));
            this.txtSmartDateRange.Text = Convert.ToInt32(smartCfg.DateRange).ToString();
            this.drpSmartDateRangeType.SelectedIndex = this.drpSmartDateRangeType.Items.IndexOf(this.drpSmartDateRangeType.Items.FindByValue(smartCfg.DateRangeType));
            this.txtShowItemCount.Text = Convert.ToInt32(smartCfg.ShowItemCount).ToString();
        }

		
        protected override bool ValidateInput()
        {
			PageCheckManager manager = new PageCheckManager();

			manager.Add( new LengthCheck(this.lblNGCodeEdit, this.txtErrorCodeEdit, 40, true) );
            manager.Add(new DecimalCheck(this.lblSmartDateRange, this.txtSmartDateRange, 0, decimal.MaxValue, true));
            manager.Add(new DecimalCheck(this.lblShowItemCount, this.txtShowItemCount, 0, decimal.MaxValue, true));

			if ( !manager.Check() )
			{
				WebInfoPublish.Publish(this, manager.CheckMessage,this.languageComponent1);
				return false;
			}

            if (_facade == null) { _facade = new TSModelFacadeFactory(base.DataProvider).CreateTSModelFacade(); }
            if (_facade.GetErrorCode(this.txtErrorCodeEdit.Text.Trim().ToUpper()) == null)
            {
                throw new Exception("$ErrorCode_Not_Exist");
            }
            object[] objsTmp = _facade.QueryTSSmartConfig(this.txtErrorCodeEdit.Text.Trim().ToUpper(), 1, 2);
            bool bExist = false;
            if (objsTmp != null && objsTmp.Length >= 2)
                bExist = true;
            else if (objsTmp != null && objsTmp.Length == 1)
            {
                if (Convert.ToInt32(((TSSmartConfig)objsTmp[0]).Sequence) != Convert.ToInt32(this.txtSequence.Text))
                {
                    bExist = true;
                }
            }
            if (bExist == true)
            {
                throw new Exception("$ErrorCode_SmartTS_Already_Exist");
            }

			return true;
        }

        #endregion
        
        #region Export 

        protected override string[] FormatExportRecord( object obj )
        {
            TSSmartConfig smartCfg = (TSSmartConfig)obj;
            return new string[]{
                                smartCfg.ErrorCode,
                                this.languageComponent1.GetString(smartCfg.Enabled),
                                this.drpSortBy.Items.FindByValue(smartCfg.SortBy).Text,
                                Convert.ToInt32(smartCfg.DateRange).ToString() + " " + this.drpSmartDateRangeType.Items.FindByValue(smartCfg.DateRangeType).Text,
                                smartCfg.ShowItemCount.ToString(),
                                smartCfg.MaintainUser.ToString(),
                                FormatHelper.ToDateString(smartCfg.MaintainDate),
                                FormatHelper.ToTimeString(smartCfg.MaintainTime)
                               };
        }

        protected override string[] GetColumnHeaderText()
        {
            return new string[] {	"不良代码",
                                "允许智能维修",
                                "排序方式",
                                "统计时间范围",
                                "显示条目数",
                                "维护人员",
                                "维护日期",
                                "维护时间" };
        }
        #endregion

        protected void gridWebGrid_ClickCellButton(object sender, CellEventArgs e)
        {
            if (e.Cell.Key == "Detail")
            {
                string strSeq = e.Cell.Row.Cells.FromKey("Sequence").Text;
                string strUrl = "FTSSmartQueryQP.aspx?sequence=" + strSeq;
                Response.Redirect(strUrl);
            }
        }
    }


}