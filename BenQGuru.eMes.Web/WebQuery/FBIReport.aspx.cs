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
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.WebQuery;
using BenQGuru.eMES.Material;

namespace BenQGuru.eMES.Web.WebQuery
{
	/// <summary>
	/// FOnWipOP 的摘要说明。
	/// </summary>
	public partial class FBIReport : BaseQPage
	{
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;

		protected WebQueryHelper _helper = null;
		protected System.Web.UI.WebControls.TextBox TextBox1;
		protected BenQGuru.eMES.Web.SelectQuery.SelectableTextBox Selectabletextbox1;

		GridHelper _gridHelper = null;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			
			this._gridHelper = new GridHelper(this.gridWebGrid);
           
			
            this.cmdGridExport.Visible=false;
			this._helper = new WebQueryHelper( this.cmdQuery,this.cmdGridExport,this.gridWebGrid,this.pagerSizeSelector,this.pagerToolBar,this.languageComponent1 );
			this._helper.LoadGridDataSource +=new EventHandler(_helper_LoadGridDataSource);
			this._helper.DomainObjectToGridRow +=new EventHandler(_helper_DomainObjectToGridRow);
			this.txtItemCodeQuery.Target = this.MakeRedirectUrl("FItemSP.aspx");

			if( !this.IsPostBack )
			{ 	
				this._initialWebGrid();
				this.dateStartDateQuery.Text = FormatHelper.ToDateString(FormatHelper.TODateInt(System.DateTime.Now));
				this.dateEndDateQuery.Text = this.dateStartDateQuery.Text;
			}
		}

		private void _initialWebGrid()
		{
			this._gridHelper.AddColumn("date","日期",null);
			this._gridHelper.AddColumn("item","产品",null);
			this._gridHelper.AddColumn("bidate","BI时间",null);
			this._gridHelper.AddColumn("output","产出数",null);
			this._gridHelper.AddColumn("count","总计",null);
			this._gridHelper.AddColumn("rate","不良率",null);
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

		private void _helper_LoadGridDataSource(object sender, EventArgs e)
		{
            
			DataSet ds=new DataSet();
			int startdate=BenQGuru.eMES.Web.Helper.FormatHelper.TODateInt(this.dateStartDateQuery.Text)*10;
			int enddate=BenQGuru.eMES.Web.Helper.FormatHelper.TODateInt(this.dateEndDateQuery.Text)*10+1;
			string  strsql=string.Format("select max(bitime) as bitime,count(bitime) as cou from tblbireport where datetime between {0} and {1}",startdate,enddate);
			if(this.txtItemCodeQuery.Text.ToString().Length>0)
			{
				strsql=strsql+string.Format(" and itemcode in ({0})",BenQGuru.eMES.Web.Helper.FormatHelper.ProcessQueryValues(this.txtItemCodeQuery.Text.ToString()));
			}
			ds=((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)this.DataProvider).PersistBroker.Query(strsql);
			
			if(int.Parse(ds.Tables[0].Rows[0]["cou"].ToString())==0)
			{
				return;
			}
            this.OWCChartSpace1.ClearCharts();
			int columncount=int.Parse(ds.Tables[0].Rows[0]["bitime"].ToString());

				strsql="select to_char(datetime) as 日期,itemcode as 产品,bitime as BI时间,output as 产出数";
				for(int i=1;i<columncount*2+1;i++)
				{
					strsql=strsql+",B"+i.ToString()+" as \""+string.Format("{0}",(i-1)*30)+"~"+string.Format("{0}",i*30)+"不良\"";
				}
				strsql.Replace("\\","");
				strsql=strsql+",badcount as 总计,rate as 不良率 from tblbireport where datetime between";
				strsql=strsql+string.Format(" {0} and {1}",startdate,enddate);
				if(this.txtItemCodeQuery.Text.ToString().Length>0)
				{
					strsql=strsql+string.Format(" and itemcode in ({0})",BenQGuru.eMES.Web.Helper.FormatHelper.ProcessQueryValues(this.txtItemCodeQuery.Text.ToString()));
				}


				ds.Clear();
				ds=((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)this.DataProvider).PersistBroker.Query(strsql);
				
				if(this.txtItemCodeQuery.Text.ToString().Length>0 && this.txtItemCodeQuery.Text.ToString().IndexOf(",",0,this.txtItemCodeQuery.Text.Length)==-1)
				{
					DataSet dt=new DataSet();
					strsql="select '合计' as 日期,'' as 产品,'' as BI时间,sum(output) as 产出数";
					for(int i=1;i<columncount*2+1;i++)
					{
						strsql=strsql+",sum(B"+i.ToString()+") as \""+string.Format("{0}",(i-1)*30)+"~"+string.Format("{0}",i*30)+"不良\"";
					}
					strsql.Replace("\\","");
					strsql=strsql+",sum(badcount) as 总计,sum(badcount)*100/sum(output) as 不良率 from tblbireport where datetime between";
					strsql=strsql+string.Format(" {0} and {1}",startdate,enddate);
					if(this.txtItemCodeQuery.Text.ToString().Length>0)
					{
						strsql=strsql+string.Format(" and itemcode in ({0})",BenQGuru.eMES.Web.Helper.FormatHelper.ProcessQueryValues(this.txtItemCodeQuery.Text.ToString()));
					}


					dt.Clear();
					dt=((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)this.DataProvider).PersistBroker.Query(strsql);
					DataRow dr=ds.Tables[0].NewRow();
					for(int i=0;i<dt.Tables[0].Columns.Count;i++)
					{ 
						if(i!=dt.Tables[0].Columns.Count-1)
						{
							dr[i]=dt.Tables[0].Rows[0][i];
						}
						else
						{
							double rate=double.Parse(dt.Tables[0].Rows[0][i].ToString());
                            
							dr[i]=rate.ToString("0.00")+"%";
						}

					}
					string[] columnitem=new string[columncount*2];
					object[] itemvalue=new object[columncount*2];
					for(int i=0;i<columncount*2;i++)
					{
						columnitem[i]=dt.Tables[0].Columns[i+4].ColumnName.ToString();
						itemvalue[i]=dt.Tables[0].Rows[0][i+4];
					}
                    
					ds.Tables[0].Rows.Add(dr);
                    this.OWCChartSpace1.AddChart(this.txtItemCodeQuery.Text.ToString(),columnitem,itemvalue,OWCChartType.LineMarkers);
                    this.OWCChartSpace1.Display = true;

				}
				( e as WebQueryEventArgs ).RowCount =ds.Tables[0].Rows.Count;
				this._gridHelper.Grid.Columns.Clear();
				this._gridHelper.Grid.DataSource=ds.Tables[0].DefaultView;
				this._gridHelper.Grid.DataBind();
			
			
		}

		/// <summary>
		/// 输入检查
		/// </summary>
		/// <returns></returns>
		private bool _checkRequireFields()
		{			
			PageCheckManager manager = new PageCheckManager();
			manager.Add( new DateRangeCheck(this.lblStartDateQuery,this.dateStartDateQuery.Text,this.lblEndDateQuery,this.dateEndDateQuery.Text,true));

			if( !manager.Check() )
			{
				WebInfoPublish.Publish(this,manager.CheckMessage,this.languageComponent1);
				return true;
			}	
			return true;
		}

		private void _helper_DomainObjectToGridRow(object sender, EventArgs e)
		{
			if( ( e as DomainObjectToGridRowEventArgs ).DomainObject != null )
			{
				OnWipInfoOnOperation obj = ( e as DomainObjectToGridRowEventArgs ).DomainObject as OnWipInfoOnOperation;
				( e as DomainObjectToGridRowEventArgs ).GridRow = 
					new UltraGridRow( new object[]{
													  obj.OperationCode,
													  obj.OnWipQuantityOnOperation,
													  ""
												  }
					);
			}
		}

		protected void Submit1_ServerClick(object sender, System.EventArgs e)
		{
            this.Gridexcel.Export(this._gridHelper.Grid);
		}


	}
}


