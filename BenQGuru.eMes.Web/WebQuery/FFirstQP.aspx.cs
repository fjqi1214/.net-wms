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
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.WebQuery;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;  
using BenQGuru.eMES.Domain;
using BenQGuru.eMES.Domain.Alert;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.BaseSetting;

namespace BenQGuru.eMES.Web.WebQuery
{
	/// <summary>
	/// FFirstQP 的摘要说明。
	/// </summary>
	public partial class FFirstQP : BaseQPage
	{
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;
		protected System.Web.UI.WebControls.TextBox txtInTicketToQuery;
		protected System.Web.UI.WebControls.Label lblReceivNoQuery;
		protected System.Web.UI.WebControls.Label lblStatusQuery;
		protected System.Web.UI.WebControls.DropDownList drpStatus;


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
			this.gridWebGrid.ClickCellButton += new Infragistics.WebUI.UltraWebGrid.ClickCellButtonEventHandler(this.gridWebGrid_ClickCellButton);
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

		protected GridHelper _gridHelper = null;
		protected WebQueryHelper _helper = null;

		#region 页面load时的动作
		protected void Page_Load(object sender, System.EventArgs e)
		{
			this._gridHelper = new GridHelper(this.gridWebGrid);

			this._helper = new WebQueryHelper( this.cmdQuery,this.cmdGridExport,this.gridWebGrid,this.pagerSizeSelector,this.pagerToolBar,this.languageComponent1 );
			this._helper.LoadGridDataSource +=new EventHandler(_helper_LoadGridDataSource);
			this._helper.DomainObjectToGridRow +=new EventHandler(_helper_DomainObjectToGridRow);
			this._helper.DomainObjectToExportRow +=new EventHandler(_helper_DomainObjectToExportRow);
			this._helper.GetExportHeadText +=new EventHandler(_helper_GetExportHeadText);
			//this._helper.MergeColumnIndexList = new object[]{ new int[]{0,1}, new int[]{2,3} };

			if(!Page.IsPostBack)
			{
				// 初始化页面语言
				this.InitPageLanguage(this.languageComponent1, false);

				this._initialWebGrid();

				this.txtDate.Text = FormatHelper.ToDateString(  FormatHelper.TODateInt(DateTime.Today) );
				this.txtDateTo.Text = FormatHelper.ToDateString(  FormatHelper.TODateInt(DateTime.Today) );
				BindSegment();
				BindShift();
			}
		}

		private void BindSegment()
		{
			BenQGuru.eMES.Common.Domain.IDomainDataProvider provider = null;
			try
			{
				this.drpSegment.Items.Clear();
				provider = base.DataProvider;

				BenQGuru.eMES.BaseSetting.BaseModelFacade facade = new BenQGuru.eMES.BaseSetting.BaseModelFacade(provider);
				object[] objs = facade.GetAllSegment();
				if(objs != null && objs.Length > 0)
				{
					foreach(object obj in objs)
					{
						BenQGuru.eMES.Domain.BaseSetting.Segment seg = obj as BenQGuru.eMES.Domain.BaseSetting.Segment;
						if(seg != null)
						{
							//this.drpSegment.Items.Add(new ListItem(seg.SegmentCode,seg.ShiftTypeCode));
							this.drpSegment.Items.Add(seg.SegmentCode);
						}
					}
				}

			}
			finally
			{
				if(provider != null)
					((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)provider).PersistBroker.CloseConnection();
			}
		}

		private void BindShift()
		{
			BenQGuru.eMES.Common.Domain.IDomainDataProvider provider = null;
			try
			{
				provider = base.DataProvider;
				this.drpShift.Items.Clear();
				if(this.drpSegment.SelectedItem != null)
				{
					BenQGuru.eMES.BaseSetting.BaseModelFacade facade = new BenQGuru.eMES.BaseSetting.BaseModelFacade(provider);
					BenQGuru.eMES.Domain.BaseSetting.Segment seg = facade.GetSegment(this.drpSegment.SelectedItem.ToString()) as BenQGuru.eMES.Domain.BaseSetting.Segment;
					if(seg != null)
					{
						//string shifttype = seg.ShiftTypeCode;
						//object[] objs = provider.CustomQuery(typeof(Shift), new SQLCondition(string.Format("select {0} from TBLSHIFT where shifttypecode='{1}'order by SHIFTBTIME", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Shift)),shifttype)));
                        object[] objs = (new ShiftModelFacade(base.DataProvider)).QueryShiftBySegment("", seg.SegmentCode, 0, int.MaxValue);
                        if(objs != null && objs.Length > 0)
						{
							foreach(object obj in objs)
							{
								Shift s = obj as BenQGuru.eMES.Domain.BaseSetting.Shift;
								if( s!= null)
								{
									this.drpShift.Items.Add(s.ShiftCode);
								}
							}
						}
					}

				}
				this.drpShift.Items.Insert(0,new ListItem("所有",string.Empty));
			}
			finally
			{
				if(provider != null)
					((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)provider).PersistBroker.CloseConnection();
			}
		}

		protected void drpSegment_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			BindShift();
		}

		private void _initialWebGrid()
		{
			this._gridHelper.AddColumn("Date","日期",null);
			this._gridHelper.AddColumn("Shift","班次",null);
			this._gridHelper.AddColumn("SS","产线",null);
			this._gridHelper.AddColumn("ItemCode","产品代码",null);
			this._gridHelper.AddColumn("ShiftTime","上班时间",null);
			this._gridHelper.AddColumn("OnlineTime","第一台投入时间",null);
			this._gridHelper.AddColumn("OfflineTime","第一台产出时间",null);
			this._gridHelper.AddColumn("Time1","首台下线耗时",null);
			this._gridHelper.AddColumn("Time2","生产准备耗时",null);
			this._gridHelper.AddColumn("EndTime","下班时间",null);
			this._gridHelper.AddColumn("LastOnTime","末台投入时间",null);
			this._gridHelper.AddColumn("LastOffTime","末台产出时间",null);
			this._gridHelper.AddColumn("Time3","末台下线耗时",null);
			this._gridHelper.AddColumn("Time4","无效生产时间",null);

			//多语言
			this._gridHelper.ApplyLanguage( this.languageComponent1 );
		}

		private void _helper_GetExportHeadText(object sender, EventArgs e)
		{
				( e as ExportHeadEventArgs ).Heads = 
					new string[]{
									"日期",
									"班次",
									"产线",
									"产品代码",
									"上班时间",
									"第一台投入时间",
									"第一台产出时间",
									"首台下线耗时",
									"生产准备耗时",
									"下班时间",
									"末台投入时间",
									"末台产出时间",
									"末台下线耗时",
									"无效生产时间"
								};
		}

		#endregion

		private void _helper_LoadGridDataSource(object sender, EventArgs e)
		{
				
			PageCheckManager manager = new PageCheckManager();

			if( !manager.Check() )
			{
				WebInfoPublish.Publish(this,manager.CheckMessage,this.languageComponent1);
				return;
			}

			BenQGuru.eMES.Common.Domain.IDomainDataProvider provider = null;
			try
			{
				provider = base.DataProvider;

				WebQueryEventArgs we =  e as WebQueryEventArgs;

				string shift;
				if(drpShift.SelectedValue == null)
					shift =string.Empty;
				else
					shift = drpShift.SelectedValue.ToString();

				if(shift == string.Empty)
				{
					string[] shiftArray = new string[drpShift.Items.Count];
					for(int i=0;i<shiftArray.Length;i++)
					{
						shiftArray[i] = drpShift.Items[i].Value;
					}
					shift = FormatHelper.ProcessQueryValues(shiftArray);
				}
				else
				{
					shift = FormatHelper.ProcessQueryValues(drpShift.SelectedValue);
				}
				object[] dataSource = QueryFirstOnlineWeb(provider,
														this.txtDate.Text,
														this.txtDateTo.Text,
														shift,
														this.txtSS.Text.Trim(),
														this.txtItemCode.Text.Trim(),
														txtModel.Text.Trim(),
														this.drpSegment.SelectedValue,
														we.StartRow,
														we.EndRow);

				we.GridDataSource = dataSource;

				we.RowCount = QueryFirstOnlineWebCount(provider,
														this.txtDate.Text,
														this.txtDateTo.Text,
														shift,
														this.txtSS.Text.Trim(),
														this.txtItemCode.Text.Trim(),
														txtModel.Text.Trim(),
														this.drpSegment.SelectedValue);

			}
			finally
			{
				if(provider != null)
					((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)provider).PersistBroker.CloseConnection();
			}
	
		}

		private void _helper_DomainObjectToGridRow(object sender, EventArgs e)
		{	
			DomainObjectToGridRowEventArgs de = ( e as DomainObjectToGridRowEventArgs );
			if(de != null)
			{
				de.GridRow = new UltraGridRow(GetArrayFrom(de.DomainObject));
			}
		}

		private string[] GetArrayFrom(DomainObject domain)
		{
			BenQGuru.eMES.Domain.Alert.FirstOnline  obj = domain as BenQGuru.eMES.Domain.Alert.FirstOnline;
			if(obj != null)
			{
				string offtime = obj.OffLineTime == 0?string.Empty:FormatHelper.ToTimeString(obj.OffLineTime);
				
				//班次
				BenQGuru.eMES.BaseSetting.ShiftModelFacade shiftFacade = new BenQGuru.eMES.BaseSetting.ShiftModelFacade(this.DataProvider);
				BenQGuru.eMES.Domain.BaseSetting.Shift shift = shiftFacade.GetShift(obj.ShiftCode) as BenQGuru.eMES.Domain.BaseSetting.Shift;
				if(shift == null) 
					return null;

				#region 首台下线耗时
				string tmOff = string.Empty;
				if(obj.ActionType == "OFF")
				{
					//如果跨天，并且是在第二天
					if(obj.OffLineTime < obj.ShiftTime && obj.IsOverDay==FormatHelper.TRUE_STRING)
					{
						TimeSpan it = DateTime.Parse(FormatHelper.ToTimeString(obj.OffLineTime)).AddDays(1) - DateTime.Parse(FormatHelper.ToTimeString(obj.OnLineTime));		
						int tm = it.Hours * 60 + it.Minutes;
						if(tm >= 24 * 60)
							tm = 24 * 60 - tm;

						tmOff = tm.ToString();
					}
					else
					{
						TimeSpan it = DateTime.Parse(FormatHelper.ToTimeString(obj.OffLineTime)) - DateTime.Parse(FormatHelper.ToTimeString(obj.OnLineTime));
						
						int tm = it.Hours * 60 + it.Minutes;
						if(tm >= 24 * 60)
							tm = 24 * 60 - tm;

						tmOff = tm.ToString();
					}
					
				}
				#endregion

				#region 生产准备耗时
				//如果跨天，并且是在第二天
				string tmOn = string.Empty;
				if(	obj.OnLineTime < shift.ShiftEndTime //上线时间在第二天
					&&
					obj.OnLineTime < obj.ShiftTime　///上班时间在第一天
					&&
					obj.ShiftTime > shift.ShiftEndTime　
					&&
					obj.IsOverDay==FormatHelper.TRUE_STRING)
				{
					TimeSpan it2 = DateTime.Parse(FormatHelper.ToTimeString(obj.OnLineTime)).AddDays(1) - DateTime.Parse(FormatHelper.ToTimeString(obj.ShiftTime));		
					int tm = it2.Hours * 60 + it2.Minutes;
					if(tm >= 24 * 60)
						tm = 24 * 60 - tm;
					tmOn = tm.ToString();
				}
				else
				{
					TimeSpan it2 = DateTime.Parse(FormatHelper.ToTimeString(obj.OnLineTime)) - DateTime.Parse(FormatHelper.ToTimeString(obj.ShiftTime));
					int tm = it2.Hours * 60 + it2.Minutes;
					if(tm >= 24 * 60)
						tm = 24 * 60 - tm;
					tmOn = tm.ToString();
				}
				#endregion

				#region 末台下线耗时
				string tmLastOff = string.Empty;
				if(obj.LastType  == "OFF")
				{
					//如果跨天，上线和下线不在同一天
					if(obj.LastOffTime < obj.LastOnTime && obj.IsOverDay==FormatHelper.TRUE_STRING)
					{
						TimeSpan it = DateTime.Parse(FormatHelper.ToTimeString(obj.LastOffTime)).AddDays(1) - DateTime.Parse(FormatHelper.ToTimeString(obj.LastOnTime));		
						int tm = it.Hours * 60 + it.Minutes;
						if(tm >= 24 * 60)
							tm = 24 * 60 - tm;

						tmLastOff = tm.ToString();
					}
					else
					{
						TimeSpan it = DateTime.Parse(FormatHelper.ToTimeString(obj.LastOffTime)) - DateTime.Parse(FormatHelper.ToTimeString(obj.LastOnTime));
						int tm = it.Hours * 60 + it.Minutes;
						if(tm >= 24 * 60)
							tm = 24 * 60 - tm;

						tmLastOff = tm.ToString();
					}
					
				}
				#endregion

				#region 无效生产时间
				//如果跨天，且下班时间和末件下线采集时间不在同一天
				string tmLastOn = string.Empty;
				if(obj.LastType  == "OFF")
				{
					//下班时间比上班时间小，末件下线比上班时间大，并且比下班时间大，则下线位于前一天，下班位于后一天
					if(
						obj.ShiftTime > shift.ShiftEndTime //上班时间在第一天
						&&
						obj.EndTime < obj.ShiftTime　///下班时间在第二天
						&& 
						obj.LastOffTime > obj.ShiftTime ///采集时间在第1天 
						&& 
						obj.IsOverDay==FormatHelper.TRUE_STRING)
					{
						TimeSpan it2 = DateTime.Parse(FormatHelper.ToTimeString(obj.EndTime)).AddDays(1) - DateTime.Parse(FormatHelper.ToTimeString(obj.LastOffTime));		
						int tm = it2.Hours * 60 + it2.Minutes;
						if(tm >= 24 * 60)
							tm = 24 * 60 - tm;

						tmLastOn = tm.ToString();
					}
					else
					{
						TimeSpan it2 = DateTime.Parse(FormatHelper.ToTimeString(obj.EndTime)) - DateTime.Parse(FormatHelper.ToTimeString(obj.LastOffTime));
						int tm = it2.Hours * 60 + it2.Minutes;
						if(tm >= 24 * 60)
							tm = 24 * 60 - tm;

						tmLastOn = tm.ToString();
					}
				}
				#endregion

				return new string[]{
									FormatHelper.ToDateString(obj.MaintainDate),
									obj.ShiftCode,
									obj.SSCode,
									obj.ItemCode,
									FormatHelper.ToTimeString(obj.ShiftTime),
									FormatHelper.ToTimeString(obj.OnLineTime),
									offtime,
									tmOff,
									tmOn,
									FmtTime(obj.EndTime),
									FmtTime(obj.LastOnTime),
									FmtTime(obj.LastOffTime),
									tmLastOff,
									tmLastOn
									};
			}

			return null;
		}

		private string FmtTime(int time)
		{
			return time==0?string.Empty:FormatHelper.ToTimeString(time);
		}
		private void _helper_DomainObjectToExportRow(object sender, EventArgs e)
		{
			
			DomainObjectToExportRowEventArgs de = e as DomainObjectToExportRowEventArgs;
			if(de != null)
			{
				de.ExportRow = this.GetArrayFrom(de.DomainObject);
			}
		}

		private void gridWebGrid_ClickCellButton(object sender, Infragistics.WebUI.UltraWebGrid.CellEventArgs e)
		{
			

		}

		#region Web查询SQL
		private string GetFirstOnlineWhere(string date, string dateto,string shift, string ss,string itemcode,string model,string seg)
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			sb.Append(" from TBLFIRSTONLINE where 1=1 ");
			if(date.Trim() != string.Empty)
				sb.Append(" and MDate>=").Append(FormatHelper.TODateInt(date));

			if(dateto.Trim() != string.Empty)
				sb.Append(" and MDate<=").Append(FormatHelper.TODateInt(dateto));

			if(ss != string.Empty)
				sb.Append(" and sscode like '").Append(ss.ToUpper()).Append("%'");

			if(shift != string.Empty)
				sb.Append(" and shiftcode in(").Append(shift).Append(")");

			if(itemcode != string.Empty)
				sb.Append(" and itemcode like '").Append(itemcode.ToUpper()).Append("%'");

			if(model != string.Empty)
				sb.Append(" and modelcode like '").Append(model.ToUpper().Trim()).Append("%'");

			if(seg != null && seg != string.Empty)
                sb.Append(" and sscode in(select sscode from tblss where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " and segcode='").Append(seg.ToUpper().Trim()).Append("')");
			return sb.ToString();

		}
		public object[] QueryFirstOnlineWeb(BenQGuru.eMES.Common.Domain.IDomainDataProvider provider,string date, string dateto,string shift, string ss,string itemcode,string model,string seg,int inclusive, int exclusive )
		{
			string str = this.GetFirstOnlineWhere(date,dateto,shift,ss,itemcode,model,seg);
			return provider.CustomQuery(typeof(FirstOnline), new PagerCondition(string.Format("select {0} {1}", DomainObjectUtility.GetDomainObjectFieldsString(typeof(FirstOnline)) ,str), "MDATE,SSCODE", inclusive, exclusive));
		}

		public int QueryFirstOnlineWebCount(BenQGuru.eMES.Common.Domain.IDomainDataProvider provider,string date, string dateto,string shift, string ss,string itemcode,string model,string seg)
		{
			string str = this.GetFirstOnlineWhere(date,dateto,shift,ss,itemcode,model,seg);
			return provider.GetCount(new SQLCondition(string.Format("select count(*) c {0}", str)));
		}

		#endregion
	}
}
