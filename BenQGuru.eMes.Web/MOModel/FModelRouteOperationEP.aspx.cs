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
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Web.Helper;
#endregion

namespace BenQGuru.eMES.Web.MOModel
{
	/// <summary>
	/// FModelRouteOperationEP1 的摘要说明。
	/// </summary>
	public partial class FModelRouteOperationEP : BasePage
	{
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;

		private ModelFacade _modelFacade ;//= FacadeFactory.CreateModelFacade();
	

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


		#region page events
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if(!IsPostBack)
			{
				// 初始化页面语言
				this.InitPageLanguage(this.languageComponent1, false);

				// 初始化界面UI
				this.InitUI();
				Initparamters();
				InitData();
			}
			
		}
		protected void cmdSave_ServerClick(object sender, System.EventArgs e)
		{
			if(_modelFacade==null){_modelFacade = new FacadeFactory(base.DataProvider).CreateModelFacade();}
			this._modelFacade.UpdateModel2Operation((Model2OP)GetEditObject());
		}
		protected void cmdReturn_ServerClick(object sender, System.EventArgs e)
		{   
			if(_modelFacade==null){_modelFacade = new FacadeFactory(base.DataProvider).CreateModelFacade();}
			Model2OP model2Operation = (Model2OP)this._modelFacade.GetModel2Operation(OPID,GlobalVariables.CurrentOrganizations.First().OrganizationID);
			Response.Redirect(this.MakeRedirectUrl("FModelRouteOperationSP.aspx", new string[] {"modelcode","routecode"},new string[] {model2Operation.ModelCode,model2Operation.RouteCode}));
		}
		#endregion

		#region private method
		private void Initparamters()
		{
			if(Request.Params["opid"] == null)
			{
				ExceptionManager.Raise(this.GetType().BaseType,"$Error_RequestUrlParameter_Lost");
			}
			else
			{
				this.ViewState["opid"] = Request.Params["opid"].ToString();
			}
		}

		public string OPID
		{
			get
			{
				return (string)this.ViewState["opid"];
			}
		}

		private void InitData()
		{
			if(_modelFacade==null){_modelFacade = new FacadeFactory(base.DataProvider).CreateModelFacade();}
			SetEditObject(this._modelFacade.GetModel2Operation(OPID,GlobalVariables.CurrentOrganizations.First().OrganizationID));
		}

		private void SetEditObject(object obj)
		{
			if(obj == null)
			{
				this.txtOperationCodeEdit.Text = string.Empty;
				this.txtOperationCodeEdit.ReadOnly = true;
				this.txtOperationsequenceEdit.Text = string.Empty;
				this.chbOperationCheckEdit.Checked = false;
				this.chbCompLoadingEdit.Checked = false;
				this.chbIDMergeEdit.Checked = false;
				this.chbStartOpEdit.Checked = false;
				this.chbEndOpEdit.Checked = false;
				this.chbPackEdit.Checked = false;
				this.chbEditSPC.Checked = false;
				this.chbRepairEdit.Checked = false;
				this.chbNGTestEdit.Checked = false;
			}
			else
			{
				Model2OP model2Operation = (Model2OP)obj;
				this.txtOperationCodeEdit.Text = model2Operation.OPCode;
				this.txtOperationCodeEdit.ReadOnly = true;
				this.txtOperationsequenceEdit.Text = model2Operation.OPSequence.ToString();
				this.chbOperationCheckEdit.Checked = FormatHelper.StringToBoolean(model2Operation.OPControl,model2Operation.OPControl.Length-9);
				this.chbCompLoadingEdit.Checked = FormatHelper.StringToBoolean(model2Operation.OPControl,model2Operation.OPControl.Length-8);
				this.chbIDMergeEdit.Checked = FormatHelper.StringToBoolean(model2Operation.OPControl,model2Operation.OPControl.Length-7);
				this.chbStartOpEdit.Checked = FormatHelper.StringToBoolean(model2Operation.OPControl,model2Operation.OPControl.Length-6);
				this.chbEndOpEdit.Checked = FormatHelper.StringToBoolean(model2Operation.OPControl,model2Operation.OPControl.Length-5);
				this.chbPackEdit.Checked = FormatHelper.StringToBoolean(model2Operation.OPControl,model2Operation.OPControl.Length-4);
				this.chbEditSPC.Checked = FormatHelper.StringToBoolean(model2Operation.OPControl,model2Operation.OPControl.Length-3);
				this.chbRepairEdit.Checked = FormatHelper.StringToBoolean(model2Operation.OPControl,model2Operation.OPControl.Length-2);
				this.chbNGTestEdit.Checked = FormatHelper.StringToBoolean(model2Operation.OPControl,model2Operation.OPControl.Length-1);
			}
		}
		private object GetEditObject()
		{
			if(_modelFacade==null){_modelFacade = new FacadeFactory(base.DataProvider).CreateModelFacade();}
			Model2OP model2Operation = (Model2OP)this._modelFacade.GetModel2Operation(OPID,GlobalVariables.CurrentOrganizations.First().OrganizationID);
			try
			{
				 model2Operation.OPSequence= System.Int32.Parse(this.txtOperationsequenceEdit.Text.Trim());
			}
			catch
			{
				model2Operation.OPSequence = 0;
			}
			model2Operation.OPControl = FormatHelper.BooleanToString(model2Operation.OPControl,model2Operation.OPControl.Length-9,this.chbOperationCheckEdit.Checked);
			model2Operation.OPControl = FormatHelper.BooleanToString(model2Operation.OPControl,model2Operation.OPControl.Length-8,this.chbCompLoadingEdit.Checked);
			model2Operation.OPControl = FormatHelper.BooleanToString(model2Operation.OPControl,model2Operation.OPControl.Length-7,this.chbIDMergeEdit.Checked);
			model2Operation.OPControl = FormatHelper.BooleanToString(model2Operation.OPControl,model2Operation.OPControl.Length-6,this.chbStartOpEdit.Checked);
			model2Operation.OPControl = FormatHelper.BooleanToString(model2Operation.OPControl,model2Operation.OPControl.Length-5,this.chbEndOpEdit.Checked);
			model2Operation.OPControl = FormatHelper.BooleanToString(model2Operation.OPControl,model2Operation.OPControl.Length-4,this.chbPackEdit.Checked);
			model2Operation.OPControl = FormatHelper.BooleanToString(model2Operation.OPControl,model2Operation.OPControl.Length-3,this.chbEditSPC.Checked);
			model2Operation.OPControl = FormatHelper.BooleanToString(model2Operation.OPControl,model2Operation.OPControl.Length-2,this.chbRepairEdit.Checked);
			model2Operation.OPControl = FormatHelper.BooleanToString(model2Operation.OPControl,model2Operation.OPControl.Length-1,this.chbNGTestEdit.Checked);
            model2Operation.OrganizationID = GlobalVariables.CurrentOrganizations.First().OrganizationID;
		    return model2Operation;
		}
		#endregion
	}
}
