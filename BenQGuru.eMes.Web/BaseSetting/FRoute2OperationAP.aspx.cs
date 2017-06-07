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

using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;

using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.BaseSetting;

namespace BenQGuru.eMES.Web.BaseSetting
{
	/// <summary>
	/// FOperation2ResourceSP 的摘要说明。
	/// </summary>
	public partial class FRoute2OperationAP : BaseAPageNew 
	{
		protected System.Web.UI.WebControls.Label lblOperationSelectTitle;
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;
		private BaseModelFacade facade = null ;//new BaseModelFacadeFactory().Create();	


		#region Stable

		protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
		{
			return this.languageComponent1;
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
            this.gridHelper = new GridHelperNew(this.gridWebGrid,this.DtSource);
			if(!IsPostBack)
			{
				// 初始化页面语言
				this.InitPageLanguage(this.languageComponent1, false);

				this.txtRouteCodeQuery.Text = this.GetRequestParam("routecode");
			}
		}
		#endregion

		#region Not Stable
		protected override void InitWebGrid()
		{
            base.InitWebGrid();
			this.gridHelper.AddColumn( "OPCode", "工序代码",	null);
			this.gridHelper.AddColumn( "OPDescription", "工序描述",	null);
			this.gridHelper.AddColumn( "OPControl", "",	null);

			new OperationListFactory().CreateOperationListColumns( this.gridHelper );

			this.gridHelper.AddColumn( "MaintainUser", "维护用户",	null);
			this.gridHelper.AddColumn( "MaintainDate", "维护日期",	null);
			this.gridHelper.AddColumn( "MaintainTime", "维护时间",	null);			

			this.gridWebGrid.Columns.FromKey("MaintainTime").Hidden = true;
			this.gridWebGrid.Columns.FromKey("OPControl").Hidden = true;

			this.gridHelper.AddDefaultColumn( true,false);

			//多语言
			this.gridHelper.ApplyLanguage( this.languageComponent1 );
		}

		protected override void AddDomainObject(ArrayList domainObject)
		{
			if(facade == null)
			{
				facade = new BaseModelFacadeFactory(base.DataProvider).Create();
			}
			facade.AddRoute2Operation((Route2Operation[])domainObject.ToArray(typeof(Route2Operation)));
		}

		protected override int GetRowCount()
		{
			if(facade == null)
			{
				facade = new BaseModelFacadeFactory(base.DataProvider).Create();
			}
			return this.facade.GetUnselectedOperationByRouteCodeCount( 
				FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtRouteCodeQuery.Text)),
				FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtOperationCodeQuery.Text)));
		}

		protected override DataRow GetGridRow(object obj)
		{
			bool[] opList = new OperationListFactory().CreateOperationListBooleanArray( (obj as Operation).OPControl );
            DataRow row = this.DtSource.NewRow();
			object[] values = new object[ 8 + opList.Length ];
			//values[0] = "false";
            row["OPCode"] = ((Operation)obj).OPCode.ToString();
            row["OPDescription"] = ((Operation)obj).OPDescription.ToString();
            row["OPControl"] = ((Operation)obj).OPControl;
            string[] strings = new OperationListFactory().CreateOperationListColumnsHead();
			for(int i=0;i<opList.Length;i++)
			{
				row[ strings[i] ] = opList[i];
			}

            row["MaintainUser"] = ((Operation)obj).GetDisplayText("MaintainUser");
            row["MaintainDate"] = FormatHelper.ToDateString(((Operation)obj).MaintainDate);
            row["MaintainTime"] = FormatHelper.ToTimeString(((Operation)obj).MaintainTime);

            return row;
            
            

			//return new Infragistics.WebUI.UltraWebGrid.UltraGridRow( values );

//			return new Infragistics.WebUI.UltraWebGrid.UltraGridRow( 
//				new object[]{"false",
//								((Operation)obj).OPCode.ToString(),
//								((Operation)obj).OPCollectionType,
//								FormatHelper.BooleanToGirdCheckBoxString(((Operation)obj).OPControl[0]),
//								FormatHelper.BooleanToGirdCheckBoxString(((Operation)obj).OPControl[1]),
//								FormatHelper.BooleanToGirdCheckBoxString(((Operation)obj).OPControl[2]),
//								FormatHelper.BooleanToGirdCheckBoxString(((Operation)obj).OPControl[3]),
//								FormatHelper.BooleanToGirdCheckBoxString(((Operation)obj).OPControl[4]),
//								FormatHelper.BooleanToGirdCheckBoxString(((Operation)obj).OPControl[5]),
//								FormatHelper.BooleanToGirdCheckBoxString(((Operation)obj).OPControl[6]),
//								FormatHelper.BooleanToGirdCheckBoxString(((Operation)obj).OPControl[7]),
//								FormatHelper.BooleanToGirdCheckBoxString(((Operation)obj).OPControl[8]),
//								((Operation)obj).OPDescription.ToString(),
//								((Operation)obj).MaintainUser.ToString(),
//								FormatHelper.ToDateString(((Operation)obj).MaintainDate),
//								FormatHelper.ToTimeString(((Operation)obj).MaintainTime)
//							});
		}

		protected override object[] LoadDataSource(int inclusive, int exclusive)
		{
			if(facade == null)
			{
				facade = new BaseModelFacadeFactory(base.DataProvider).Create();
			}
			return facade.GetUnselectedOperationByRouteCode( 
				FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtRouteCodeQuery.Text)),
				FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtOperationCodeQuery.Text)),
				inclusive,exclusive);
		}

		protected void cmdReturn_ServerClick(object sender, System.EventArgs e)
		{
			this.Response.Redirect(this.MakeRedirectUrl(@"./FRoute2OperationSP.aspx",
				new string[]{"routecode"},
				new string[]{this.txtRouteCodeQuery.Text}));
		}

		protected override object GetEditObject(GridRecord row)
		{
			if(facade == null)
			{
				facade = new BaseModelFacadeFactory(base.DataProvider).Create();
			}
			Route2Operation route2Operation = this.facade.CreateNewRoute2Operation();
	
			route2Operation.RouteCode = txtRouteCodeQuery.Text;
            route2Operation.OPCode = row.Items.FindItemByKey("OPCode").Text;
            route2Operation.OPControl = row.Items.FindItemByKey("OPControl").Text;	
			route2Operation.MaintainUser = this.GetUserCode();

			return route2Operation;
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
	}
}
