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
using BenQGuru.eMES.AlertModel;
using BenQGuru.eMES.Domain.Alert;

namespace BenQGuru.eMES.Web.Alert
{
    public partial class FNotifierQP : BaseMPage
    {
        private System.ComponentModel.IContainer components;
		private ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		protected BenQGuru.eMES.AlertModel.AlertBillFacade _facade;
		protected BenQGuru.eMES.BaseSetting.UserFacade _userfacade;
		string _itemcode;
		string _alerttype;
		string _alertitem;
		string _rescode;
		string _ecg2ec;

		private int _billId;
		/// <summary>
		/// 预警类别是否是资源不良数
		/// </summary>
		private bool isResourceNG = false;	

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
			this.languageComponent1.LanguagePackageDir = "\\\\..";
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

			 _facade = new BenQGuru.eMES.AlertModel.AlertBillFacade(DataProvider);
			_userfacade = new BenQGuru.eMES.BaseSetting.UserFacade(DataProvider);	

			_itemcode = Request.QueryString["itemcode"];
			_alerttype = Request.QueryString["alerttype"];
			_alertitem = Request.QueryString["alertitem"];

			_billId = int.Parse(Request.QueryString["billid"]);

            if (this._alerttype == AlertType_Old.ResourceNG) { isResourceNG = true; }
			if(isResourceNG)
			{
				_rescode = Request.QueryString["rescode"];
				_ecg2ec = Request.QueryString["ecg2ec"];
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
		    this.gridHelper.AddColumn( "UserCode", "已选用户",	null);
            this.gridHelper.AddColumn( "UserEMail", "电子邮箱",	null);
		           
            this.gridHelper.ApplyLanguage( this.languageComponent1 );

			this.gridHelper.RequestData();
        }
		
        protected override Infragistics.WebUI.UltraWebGrid.UltraGridRow GetGridRow(object obj)
        {
			BenQGuru.eMES.Domain.Alert.AlertNotifier notifier  = obj as BenQGuru.eMES.Domain.Alert.AlertNotifier;
			if(notifier == null )
				return null;

			Infragistics.WebUI.UltraWebGrid.UltraGridRow ur = new UltraGridRow(
																				new object[]
																							{
																							notifier.UserCode,
																							notifier.EMail
																							}
																				);
			BenQGuru.eMES.Domain.BaseSetting.User user = _userfacade.GetUser(notifier.UserCode) as BenQGuru.eMES.Domain.BaseSetting.User;
			if(user!= null && user.UserEmail != null)
				ur.Cells[1].Value = user.UserEmail;
			
			return ur;

        }

        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
			return  _facade.QueryAlertNotifier(_billId);
        }
        #endregion    

		protected void cmdReturn_ServerClick(object sender, System.EventArgs e)
		{
			Response.Redirect("FAlertBillMP.aspx");
		}

      }

}