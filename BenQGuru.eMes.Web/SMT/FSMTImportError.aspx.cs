#region system
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
#endregion

#region project
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.SMT;
using BenQGuru.eMES.Domain.SMT;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Web.Helper;
#endregion

namespace BenQGuru.eMES.Web.SMT
{
	/// <summary>
	/// FSMTImportError 的摘要说明。
	/// </summary>
	public class FSMTImportError: BasePage
	{
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid gridWebGrid;
		protected BenQGuru.eMES.Web.Helper.PagerSizeSelector pagerSizeSelector;
		private System.ComponentModel.IContainer components;
		protected System.Web.UI.HtmlControls.HtmlInputButton cmdReturn;
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;

		private GridHelper gridHelper = null;
		private ButtonHelper buttonHelper = null;

		//private BenQGuru.eMES.SMT.SMTFacade _facade ;//= new SMTFacadeFactory().Create();
		protected System.Web.UI.WebControls.Label lblTitles;
		//private BenQGuru.eMES.MOModel.MOFacade _modelFacade;// = SMTFacadeFactory.CreateMOFacade();
		
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			InitHanders();
			if (!IsPostBack)
			{	
				// 初始化页面语言
				this.InitPageLanguage(this.languageComponent1, false);
				// 初始化界面UI
				this.InitUI();
				this.InitWebGrid();
				this.RequestData();		//加载数据
			}
			
		}


		private void InitHanders()
		{
			this.gridHelper = new GridHelper(this.gridWebGrid);
			this.gridHelper.LoadDataSourceHandle = new LoadDataSourceDelegate(this.LoadDataSource);
			this.gridHelper.BuildGridRowhandle = new BuildGridRowDelegate(this.GetGridRow);

			this.buttonHelper = new ButtonHelper(this);
		
		}

		protected  void InitWebGrid()
		{
			//比对成功
			this.gridHelper.AddColumn( "Errorolumn", "栏位",	null);
			this.gridHelper.AddColumn( "ErrorCode", "错误代码",	null);
			this.gridHelper.AddColumn( "ErrorReason", "错误原因",	null);
			this.gridHelper.ApplyLanguage( this.languageComponent1 );

		}


		private object[] LoadDataSource()
		{
			return this.LoadDataSource(1,20);
		}

		private object[] LoadDataSource(int inclusive, int exclusive)
		{
			
			object[] errorMessages =  (object[])Session["ErrorSMTBOM"];
			if(errorMessages!=null && errorMessages.Length>0)
			{
				return errorMessages;
			}
			else{return new object[]{};}
//			 // 此处加载错误信息
//			ArrayList returnObjList = new ArrayList();
//			BenQGuru.eMES.SMT.ErrorMessage errMsg =  new ErrorMessage();
//			errMsg.Errorolumn = "机台";
//			errMsg.ErrorCode = "923409";
//			errMsg.ErrorReason = "机台代码不存在";
//
//			returnObjList.Add(errMsg);
//			return (ErrorMessage[])returnObjList.ToArray(typeof(ErrorMessage) ) ;
		}

	
		//获取数据
		private void RequestData()
		{
			this.gridHelper.GridBind(1, 20);
		}

		protected Infragistics.WebUI.UltraWebGrid.UltraGridRow GetGridRow(object obj)
		{
			return new Infragistics.WebUI.UltraWebGrid.UltraGridRow( 
				new object[]{
								((ErrorMessage)obj).Errorolumn.ToString(),
								((ErrorMessage)obj).ErrorCode.ToString(),
								((ErrorMessage)obj).ErrorReason.ToString(),
							});
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
			this.cmdReturn.ServerClick += new System.EventHandler(this.cmdReturn_ServerClick);
			// 
			// languageComponent1
			// 
			this.languageComponent1.Language = "CHS";
			this.languageComponent1.LanguagePackageDir = "D:\\SQC2.0\\eMES\\Source\\bin";
			this.languageComponent1.RuntimePage = null;
			this.languageComponent1.RuntimeUserControl = null;
			this.languageComponent1.UserControlName = "";
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion


		private void cmdReturn_ServerClick(object sender, System.EventArgs e)
		{
			Session.Remove("SessionCompareHT");
			//返回 , 应该调用客户端脚本返回
			this.ExecuteClientFunction("Close","");
		}


		
		#region 私有方法

		/// <summary>
		/// 执行客户端的函数
		/// </summary>
		/// <param name="FunctionName">函数名</param>
		/// <param name="FunctionParam">参数</param>
		/// <param name="Page">当前页面的引用</param>
		public  void ExecuteClientFunction(string FunctionName,string FunctionParam)
		{
			try
			{
				string _msg = string.Empty;
				if(FunctionParam != string.Empty)
					_msg = string.Format("<script language='JavaScript'>  {0}('{1}');</script>",FunctionName,FunctionParam);
				else
					_msg = string.Format("<script language='JavaScript'>  {0}();</script>",FunctionName);

				//将Key值设为随机数,防止脚本重复
				Page.RegisterStartupScript(Guid.NewGuid().ToString(),_msg);
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		private void Alert(string msg)
		{
			msg = msg.Replace("'","");
			msg = msg.Replace("\r","");
			msg = msg.Replace("\n","");
			string _msg = string.Format("<script language='JavaScript'>  alert('{0}');</script>",msg);
			Page.RegisterStartupScript("",_msg);
		}

		#endregion



	}
}
