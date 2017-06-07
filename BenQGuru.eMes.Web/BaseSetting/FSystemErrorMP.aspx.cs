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
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Domain.BaseSetting;

namespace BenQGuru.eMES.Web.BaseSetting
{
	/// <summary>
	/// FSystemErrorMP 的摘要说明。
	/// </summary>
	public partial class FSystemErrorMP : BaseMPage
	{
		protected System.Web.UI.WebControls.Label lblSystemErrorTitle;
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;
		
		public BenQGuru.eMES.Web.UserControl.eMESDate dateSendBeginDateEdit;
		public BenQGuru.eMES.Web.UserControl.eMESDate dateSendEndDateEdit;
		public BenQGuru.eMES.Web.UserControl.eMESTime timeSendBeginTimeEdit;
		public BenQGuru.eMES.Web.UserControl.eMESTime timeSendEndTimeEdit;
		
		private BenQGuru.eMES.BaseSetting.SystemSettingFacade _facade = null;//new SystemSettingFacadeFactory().Create();

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
			if (!IsPostBack)
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
			this.gridHelper.AddColumn( "SystemErrorCode", "系统错误代码",	null);
			this.gridHelper.AddColumn( "ErrorMessage", "错误信息",	null);
			this.gridHelper.AddColumn( "InnerErrorMessage", "内部错误信息",	null);
			this.gridHelper.AddColumn( "TriggerModuleCode", "触发错误模块",	null);
			this.gridHelper.AddColumn( "TriggerAction", "触发错误动作",	null);
			this.gridHelper.AddColumn( "SendUser", "提交用户",	null);
			this.gridHelper.AddColumn( "SendDate", "提交日期",	null);
			this.gridHelper.AddColumn( "SendTime", "提交时间",	null);
			this.gridHelper.AddColumn( "IsResolved", "是否已解决",	null);
			this.gridHelper.AddColumn( "ResolveNotes", "解决备注",	null);
			this.gridHelper.AddColumn( "ResolveUser", "解决用户",	null);
			this.gridHelper.AddColumn( "ResolveDate", "解决日期",	null);
			this.gridHelper.AddColumn( "ResolveTime", "解决时间",	null);
//			this.gridHelper.AddColumn( "MaintainUser", "维护用户",	null);
//			this.gridHelper.AddColumn( "MaintainDate", "维护日期",	null);
//			this.gridHelper.AddColumn( "MaintainTime", "维护时间",	null);

			this.gridHelper.AddDefaultColumn( true, true );

			//多语言
			this.gridHelper.ApplyLanguage( this.languageComponent1 );
		}
		
		protected override  Infragistics.WebUI.UltraWebGrid.UltraGridRow GetGridRow(object obj)
		{
			return new Infragistics.WebUI.UltraWebGrid.UltraGridRow( 
				new object[]{"false",
								((SystemError)obj).SystemErrorCode.ToString(),
								((SystemError)obj).ErrorMessage.ToString(),
								((SystemError)obj).InnerErrorMessage.ToString(),
								((SystemError)obj).TriggerModuleCode.ToString(),
								((SystemError)obj).TriggerAction.ToString(),
                                //((SystemError)obj).SendUser.ToString(),
                               ((SystemError)obj).GetDisplayText("SendUser"),
								FormatHelper.ToDateString(((SystemError)obj).SendDate),
								FormatHelper.ToTimeString(((SystemError)obj).SendTime),
								FormatHelper.DisplayBoolean(((SystemError)obj).IsResolved, this.languageComponent1),
								((SystemError)obj).ResolveNotes.ToString(),
                                //((SystemError)obj).ResolveUser.ToString(),
                               ((SystemError)obj).GetDisplayText("ResolveUser"),
								((SystemError)obj).ResolveDate == 0 ? "" : FormatHelper.ToDateString(((SystemError)obj).ResolveDate),
								((SystemError)obj).ResolveTime == 0 ? "" : FormatHelper.ToTimeString(((SystemError)obj).ResolveTime),
//								((SystemError)obj).MaintainUser.ToString(),
//								FormatHelper.ToDateString(((SystemError)obj).MaintainDate),
//								FormatHelper.ToTimeString(((SystemError)obj).MaintainTime),
								""});
		}

		protected override object[] LoadDataSource(int inclusive, int exclusive)
		{
			if(_facade == null)
			{
				_facade = new SystemSettingFacadeFactory(base.DataProvider).Create() ;
			}
			return this._facade.QuerySystemError( 
				FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtSystemErrorCodeQuery.Text)),
				this.drpIsResovedQuery.SelectedValue,
				FormatHelper.TODateInt( this.dateSendBeginDateEdit.Text ),
				FormatHelper.TOTimeInt( this.timeSendBeginTimeEdit.Text ),
				this.dateSendEndDateEdit.Text == "" ? 99999999 : FormatHelper.TODateInt(this.dateSendEndDateEdit.Text),
				this.timeSendEndTimeEdit.Text == "" ? 999999 : FormatHelper.TOTimeInt(this.timeSendEndTimeEdit.Text) ,
				inclusive, exclusive );
		}


		protected override int GetRowCount()
		{
			if(_facade == null)
			{
				_facade = new SystemSettingFacadeFactory(base.DataProvider).Create() ;
			}
			return this._facade.QuerySystemErrorCount( 
				FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtSystemErrorCodeQuery.Text)),
				this.drpIsResovedQuery.SelectedValue,
				FormatHelper.TODateInt( this.dateSendBeginDateEdit.Text ),
				FormatHelper.TOTimeInt( this.timeSendBeginTimeEdit.Text ),
				this.dateSendEndDateEdit.Text == "" ? 99999999 : FormatHelper.TODateInt(this.dateSendEndDateEdit.Text),
				this.timeSendEndTimeEdit.Text == "" ? 999999 : FormatHelper.TOTimeInt(this.timeSendEndTimeEdit.Text));
		}

		#endregion

		#region Button

		protected override void UpdateDomainObject(object domainObject)
		{
			if(_facade == null)
			{
				_facade = new SystemSettingFacadeFactory(base.DataProvider).Create() ;
			}
			this._facade.UpdateSystemError((SystemError)domainObject);
		}

		protected override void DeleteDomainObjects(ArrayList domainObjects)
		{
			if(_facade == null)
			{
				_facade = new SystemSettingFacadeFactory(base.DataProvider).Create() ;
			}
			this._facade.DeleteSystemError((SystemError[])domainObjects.ToArray( typeof(SystemError)));
		}
		#endregion

		#region Object <--> Page

		protected override object GetEditObject()
		{
			//this.ValidateInput();

			if(_facade == null)
			{
				_facade = new SystemSettingFacadeFactory(base.DataProvider).Create() ;
			}
			object obj = (SystemError)_facade.GetSystemError(this.txtSystemErrorCodeEdit.Text);

			if (obj != null)
			{
				((SystemError)obj).ResolveNotes = FormatHelper.CleanString(this.txtResolveNotesEdit.Text, 100);
				((SystemError)obj).MaintainUser = this.GetUserCode();
			}

			return obj;
		}

		protected override object GetEditObject(Infragistics.WebUI.UltraWebGrid.UltraGridRow row)
		{	
			if(_facade == null)
			{
				_facade = new SystemSettingFacadeFactory(base.DataProvider).Create() ;
			}
			object obj = _facade.GetSystemError( row.Cells[1].Text.ToString() );
			
			if (obj != null)
			{
				return (SystemError)obj;
			}

			return null;
		}

		protected override void SetEditObject(object obj)
		{
			if (obj == null)
			{
				this.txtSystemErrorCodeEdit.Text = "";
				this.txtResolveNotesEdit.Text	 = "";

				return;
			}
				
			this.txtSystemErrorCodeEdit.Text = ((SystemError)obj).SystemErrorCode;
			this.txtResolveNotesEdit.Text	 = ((SystemError)obj).ResolveNotes;
		}
		
		protected override bool ValidateInput()
		{
			PageCheckManager manager = new PageCheckManager();

			manager.Add( new LengthCheck(lblResolveNotesEdit, txtResolveNotesEdit, 100, false) );
			
			if ( !manager.Check() )
			{
				WebInfoPublish.Publish(this, manager.CheckMessage, this.languageComponent1);
				return false;
			}
			return true;
		}

		protected void cmdResolve_ServerClick(object sender, System.EventArgs e)
		{
			ArrayList array = this.gridHelper.GetCheckedRows();

			if ( array.Count > 0 )
			{
				ArrayList systemErrors = new ArrayList( array.Count );
			
				foreach (UltraGridRow row in array)
				{
					object obj = this.GetEditObject(row);

					if ( obj != null)
					{
						((SystemError)obj).IsResolved = FormatHelper.BooleanToString(true);
						((SystemError)obj).ResolveDate = FormatHelper.TODateInt(DateTime.Now);
						((SystemError)obj).ResolveTime = FormatHelper.TOTimeInt(DateTime.Now);
                        ((SystemError)obj).ResolveUser = this.GetUserCode();
						systemErrors.Add( obj );
					}
				}

				if(_facade == null)
				{
					_facade = new SystemSettingFacadeFactory(base.DataProvider).Create() ;
				}
				this._facade.UpdateSystemError( (SystemError[])systemErrors.ToArray( typeof(SystemError) ) );

				this.gridHelper.RequestData();
				this.buttonHelper.PageActionStatusHandle( PageActionType.Save );
			}
		}
		#endregion

		#region Export
		// 2005-04-06

		protected override string[] FormatExportRecord( object obj )
		{
			return new string[]{ ((SystemError)obj).SystemErrorCode.ToString(),
								   ((SystemError)obj).ErrorMessage.ToString(),
								   ((SystemError)obj).InnerErrorMessage.ToString(),
								   ((SystemError)obj).TriggerModuleCode.ToString(),
								   ((SystemError)obj).TriggerAction.ToString(),
                                   //((SystemError)obj).SendUser.ToString(),
                                   ((SystemError)obj).GetDisplayText("SendUser"),
								   FormatHelper.ToDateString(((SystemError)obj).SendDate),
								   FormatHelper.ToTimeString(((SystemError)obj).SendTime),
								   FormatHelper.DisplayBoolean(((SystemError)obj).IsResolved, this.languageComponent1),
								   ((SystemError)obj).ResolveNotes.ToString(),
                                   //((SystemError)obj).ResolveUser.ToString(),
                                    ((SystemError)obj).GetDisplayText("ResolveUser"),
								   FormatHelper.ToDateString(((SystemError)obj).ResolveDate),
								   FormatHelper.ToTimeString(((SystemError)obj).ResolveTime),
                                   //((SystemError)obj).MaintainUser.ToString(),
                                  ((SystemError)obj).GetDisplayText("MaintainUser"),
								   FormatHelper.ToDateString(((SystemError)obj).MaintainDate),
								   FormatHelper.ToTimeString(((SystemError)obj).MaintainTime) };
		}

		protected override string[] GetColumnHeaderText()
		{
			return new string[] {	 "系统错误代码",
									"错误信息",
									"内部错误信息",
									"触发错误模块",
									"触发错误动作",	
									"提交用户",	
									"提交日期",
									"提交时间",
									"是否已解决",
									"解决备注",
									"解决用户",
									"解决日期",
									"解决时间",
									"维护用户",	
									"维护日期",	
									"维护时间" };
		}

		#endregion

	}
}
