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
using BenQGuru.eMES.Report;
using BenQGuru.eMES.Domain.Report;
using BenQGuru.eMES.Domain.BaseSetting;

namespace BenQGuru.eMES.Web.WebQuery
{
	/// <summary>
	/// FFactoryWeekCheck 的摘要说明。
	/// </summary>
	public partial class FFactoryWeekCheck : BaseMPage
	{
		
		private System.ComponentModel.IContainer components;
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private BenQGuru.eMES.Report.ReportFacade _facade = null ;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{
				// 初始化页面语言
				this.InitPageLanguage(this.languageComponent1, false);

				this.InitialData();
			}
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

		private void InitialData()
		{
			SystemParameterListBuilder builder = new SystemParameterListBuilder("OutFactory",base.DataProvider);
			builder.Build( this.drpFactoryCodeQuery );
			builder.Build( this.drpFactoryCodeEdit );
			this.drpFactoryCodeQuery.Items.Insert(0,"");
			this.drpFactoryCodeEdit.Items.Insert(0,"");
		}

		protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
		{
			return this.languageComponent1;
		}

		#region WebGrid
		protected override void InitWebGrid()
		{
			this.gridHelper.AddColumn( "CheckID", "ID",	null);
			this.gridHelper.AddColumn( "FactoryID", "厂商",	null);
			this.gridHelper.AddColumn( "WeekNo", "周别",	null);
			this.gridHelper.AddColumn( "Total", "总产量",	null);
			this.gridHelper.AddColumn( "LRR", "LRR",	null);
			this.gridHelper.AddDefaultColumn( true, true );
			this.gridHelper.Grid.Columns[1].Hidden = true;

			//多语言
			this.gridHelper.ApplyLanguage( this.languageComponent1 );
		}
		
		protected override Infragistics.WebUI.UltraWebGrid.UltraGridRow GetGridRow(object obj)
		{
            BenQGuru.eMES.Domain.BaseSetting.Parameter parameter = new BenQGuru.eMES.Domain.BaseSetting.Parameter();
			parameter.ParameterCode = ((FactoryWeekCheck)obj).FactoryID.ToString();

			return new Infragistics.WebUI.UltraWebGrid.UltraGridRow( 
				new object[]{"false",
								((FactoryWeekCheck)obj).CheckID.ToString(),
								((FactoryWeekCheck)obj).FactoryID.ToString(),
								((FactoryWeekCheck)obj).WeekNo.ToString(),
								((FactoryWeekCheck)obj).Total.ToString(),
								(((FactoryWeekCheck)obj).LRR/100).ToString("##.##%") == "%" ? "0%" : (((FactoryWeekCheck)obj).LRR/100).ToString("##.##%"),
								""});
		}

		protected override object[] LoadDataSource(int inclusive, int exclusive)
		{
			if(_facade==null)
			{
				_facade = new FacadeFactory(base.DataProvider).CreateReportFacade() ;
			}
			return this._facade.QueryFactoryWeekCheck( 
				this.drpFactoryCodeQuery.SelectedIndex.ToString(),
				this.txtWeekNoQuery.Text.Trim().ToUpper(),
				inclusive, exclusive );
		}

		protected override int GetRowCount()
		{
			if(_facade==null)
			{
				_facade = new FacadeFactory(base.DataProvider).CreateReportFacade() ;
			}
			return this._facade.QueryFactoryWeekCheckCount( 
				this.drpFactoryCodeQuery.SelectedIndex.ToString(),
				this.txtWeekNoQuery.Text.Trim());
		}

		#endregion

		#region Button

		protected override void AddDomainObject(object domainObject)
		{
			if(_facade==null)
			{
				_facade = new FacadeFactory(base.DataProvider).CreateReportFacade() ;
			}
			this._facade.AddFactoryWeekCheck((FactoryWeekCheck)domainObject);
		}
        
		protected override void UpdateDomainObject(object domainObject)
		{
			if(_facade==null)
			{
				_facade = new FacadeFactory(base.DataProvider).CreateReportFacade() ;
			}
			this._facade.UpdateFactoryWeekCheck((FactoryWeekCheck)domainObject);
		}

		protected override void DeleteDomainObjects(ArrayList domainObjects)
		{
			if(_facade==null)
			{
				_facade = new FacadeFactory(base.DataProvider).CreateReportFacade() ;
			}
			this._facade.DeleteFactoryWeekCheck((FactoryWeekCheck[])domainObjects.ToArray(typeof(FactoryWeekCheck)));
		}

		#endregion

		#region Object <--> Page

		protected override object GetEditObject()
		{
			this.ValidateInput();

			if(_facade==null)
			{
				_facade = new FacadeFactory(base.DataProvider).CreateReportFacade() ;
			}
			FactoryWeekCheck factoryWeekCheck = this._facade.CreateNewFactoryWeekCheck();

			if( this.txtCheckID.Text.Trim() != "" && this.txtCheckID.Text.Trim() != null )
			{
				factoryWeekCheck.CheckID = this.txtCheckID.Text.Trim();
				this.txtCheckID.Text = "";
			}
			else
			{
				factoryWeekCheck.CheckID = System.Guid.NewGuid().ToString();
			}
			factoryWeekCheck.FactoryID = this.drpFactoryCodeEdit.SelectedValue;
			if( this.txtWeekNoEdit.Text.Substring(0,2).ToUpper() == "WK" )
			{
				factoryWeekCheck.WeekNo = "WK" + this.txtWeekNoEdit.Text.Substring(2);
			}
			else
			{
				factoryWeekCheck.WeekNo = "WK" + this.txtWeekNoEdit.Text;
			}
			factoryWeekCheck.Total = Convert.ToDecimal(this.txtTotalEdit.Text);
			factoryWeekCheck.LRR = Convert.ToDecimal(this.txtLRREdit.Text);

			return factoryWeekCheck;
		}

		protected override object GetEditObject(Infragistics.WebUI.UltraWebGrid.UltraGridRow row)
		{
			if(_facade==null)
			{
				_facade = new FacadeFactory(base.DataProvider).CreateReportFacade() ;
			}
			object obj = _facade.GetFactoryWeekCheck( row.Cells[1].Text.ToString() );
			this.txtCheckID.Text = row.Cells[1].Text.ToString();
			
			if (obj != null)
			{
				return (FactoryWeekCheck)obj;
			}

			return null;
		}

		protected override void SetEditObject(object obj)
		{
			if (obj == null)
			{
				this.drpFactoryCodeEdit.SelectedIndex = 0;
				this.txtWeekNoEdit.Text	= "";
				this.txtTotalEdit.Text	= "";
				this.txtLRREdit.Text	= "";

				return;
			}

			this.drpFactoryCodeEdit.SelectedValue = ((FactoryWeekCheck)obj).FactoryID;
			this.txtWeekNoEdit.Text	= ((FactoryWeekCheck)obj).WeekNo.ToString();
			this.txtTotalEdit.Text	= ((FactoryWeekCheck)obj).Total.ToString();
			this.txtLRREdit.Text	= ((FactoryWeekCheck)obj).LRR.ToString();
		}

		protected override bool ValidateInput()
		{
			PageCheckManager manager = new PageCheckManager();

			manager.Add( new LengthCheck (this.lblFactoryE, this.drpFactoryCodeEdit, 40, true) );
			manager.Add( new LengthCheck (this.lblWeekNoEdit, this.txtWeekNoEdit, 40, true) );
			manager.Add( new DecimalCheck (this.lblTotalEdit, this.txtTotalEdit, decimal.MinValue, decimal.MaxValue, false) );
			manager.Add( new DecimalCheck (this.lblLRREdit, this.txtLRREdit, decimal.MinValue, decimal.MaxValue, false) );

			if ( !manager.Check() )
			{
				WebInfoPublish.Publish(this, manager.CheckMessage, this.languageComponent1);
				return false;
			}

			if(_facade==null)
			{
				_facade = new FacadeFactory(base.DataProvider).CreateReportFacade() ;
			}
			string factoryID = this.drpFactoryCodeEdit.SelectedValue;
			string weekNo = "";
			if( this.txtWeekNoEdit.Text != "" && this.txtWeekNoEdit.Text.Substring(0,2).ToUpper() == "WK" )
			{
				weekNo = "WK" + this.txtWeekNoEdit.Text.Substring(2);
			}
			else
			{
				weekNo = "WK" + this.txtWeekNoEdit.Text;
			}
			int count = _facade.QueryFactoryWeekCheckCount( factoryID, weekNo );
			if ( count != 0 && PageAction == PageActionType.Add )
			{
				WebInfoPublish.Publish(this, factoryID+" $Factory_WeekNo_Exist", this.languageComponent1);
			}

			return true;
		}

		#endregion

		#region Export

		protected override string[] FormatExportRecord( object obj )
		{
			return new string[]{ ((FactoryWeekCheck)obj).FactoryID.ToString(),
								   ((FactoryWeekCheck)obj).WeekNo.ToString(),
								   ((FactoryWeekCheck)obj).Total.ToString(),
								   (((FactoryWeekCheck)obj).LRR/100).ToString("##.##%") == "%" ? "0%" : (((FactoryWeekCheck)obj).LRR/100).ToString("##.##%") };
		}

		protected override string[] GetColumnHeaderText()
		{
			return new string[] {	this.languageComponent1.GetString("FactoryID"),
									this.languageComponent1.GetString("WeekNo"),
									this.languageComponent1.GetString("Total"),
									this.languageComponent1.GetString("LRR") };
		}

		#endregion
	}
}
