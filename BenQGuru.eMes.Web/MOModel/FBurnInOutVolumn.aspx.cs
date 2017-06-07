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

using BenQGuru.eMES.Web.Helper ;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.MOModel;

namespace BenQGuru.eMES.Web.MOModel
{
	/// <summary>
	/// FBurnInOutVolumn 的摘要说明。
	/// </summary>
	public partial class FBurnInOutVolumn : BasePage
	{
		private System.ComponentModel.IContainer components;
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;

		private ShelfFacade _shelfFacade = null;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if( !Page.IsPostBack )
			{
				// 初始化页面语言
				this.InitPageLanguage(this.languageComponent1, false);

				if(_shelfFacade==null)
				{
					_shelfFacade = new FacadeFactory( base.DataProvider ).CreateShelfFacade();
				}
				object volumn = _shelfFacade.GetBurnInOutVolumn( Guid.Empty.ToString() );
				if( volumn == null )
				{
					volumn = _shelfFacade.CreateNewBurnInOutVolumn();
					(volumn as BurnInOutVolumn).PKID = Guid.Empty.ToString();
					(volumn as BurnInOutVolumn).Total = 0;
					(volumn as BurnInOutVolumn).Used = 0;
					(volumn as BurnInOutVolumn).MaintainUser = this.GetUserCode();

					_shelfFacade.AddBurnInOutVolumn( volumn as BurnInOutVolumn );
				}

				this.txtVolumnEdit.Text = (volumn as BurnInOutVolumn).Total.ToString("##.##");
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
			this.cmdSave.ServerClick +=new EventHandler(cmdSave_ServerClick);

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

		private void cmdSave_ServerClick(object sender, EventArgs e)
		{
			if( ValidateInput() )
			{
				if(_shelfFacade==null)
				{
					_shelfFacade = new FacadeFactory( base.DataProvider ).CreateShelfFacade();
				}

				object volumn = _shelfFacade.GetBurnInOutVolumn( Guid.Empty.ToString() );

				( volumn as BurnInOutVolumn ).Total = Convert.ToInt32( FormatHelper.CleanString( this.txtVolumnEdit.Text, 10) );
				( volumn as BurnInOutVolumn ).MaintainUser = this.GetUserCode();

				_shelfFacade.UpdateBurnInOutVolumn( volumn as BurnInOutVolumn );

				Alter(this.languageComponent1.GetString("$CS_Save_Success"));
			}
		}

		private bool ValidateInput()
		{
			PageCheckManager manager = new PageCheckManager();	
			manager.Add( new NumberCheck (this.lblVolumnEdit, this.txtVolumnEdit, 0, int.MaxValue, true) );

			if ( !manager.Check() )
			{
				WebInfoPublish.Publish(this, manager.CheckMessage,languageComponent1);
				return false;
			}
			return true;
		}

		private void Alter(string str)
		{
			string _msg = string.Format("<script language='JavaScript'>  alert('{0}');</script>",str);
			Page.RegisterStartupScript(Guid.NewGuid().ToString(),_msg);
		}

	}
}
