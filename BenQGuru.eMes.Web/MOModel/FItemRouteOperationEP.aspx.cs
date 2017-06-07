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
#endregion



#region project
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Web.Helper;
#endregion

namespace BenQGuru.eMES.Web.MOModel
{
	/// <summary>
	/// FItemRouteOperationEP 的摘要说明。
	/// </summary>
	public partial class FItemRouteOperationEP : BaseMPageMinus
	{
		protected System.Web.UI.HtmlControls.HtmlInputButton cmdReturn;
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;

		private ItemFacade _itemFacade;// = FacadeFactory.CreateItemFacade();
		protected System.Web.UI.WebControls.Label lblSourceItemCode;
		protected System.Web.UI.WebControls.TextBox txtSourceItemCodeEdit;
		protected System.Web.UI.HtmlControls.HtmlInputButton cmdDelete;
		protected BenQGuru.eMES.Web.Helper.ExcelExporter excelExporter;
	
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
            PostBackTrigger trigger = new PostBackTrigger();
            trigger.ControlID = this.cmdSave.ID;
            (this.impForm.FindControl("up1") as UpdatePanel).Triggers.Add(trigger);
			if(!IsPostBack)
			{
				// 初始化页面语言
				this.InitPageLanguage(this.languageComponent1, false);

				if(_itemFacade==null)
				{
					_itemFacade = new FacadeFactory(base.DataProvider).CreateItemFacade();
				}
				this.InitPageLanguage(this.languageComponent1, false);
				InitParameters();
				InitdrpMergeTypeEdit();
				InitOptionOP();

				ItemRoute2OP ip = (ItemRoute2OP)this._itemFacade.GetItemRoute2Op(OPID, GlobalVariables.CurrentOrganizations.First().OrganizationID);
				SetEditObject(ip);

				CheckReflow(ip.ItemCode);
			}
		}


		private void InitOptionOP()
		{
			BenQGuru.eMES.BaseSetting.BaseModelFacade facade = new BaseModelFacade(this.DataProvider);

			object[] ops = facade.GetAllOutLineOp();

			this.drpOptionalOP.Items.Clear();
			this.drpOptionalOP.Items.Add(new ListItem("",""));

			if(ops == null)
				return;

			foreach(BenQGuru.eMES.Domain.BaseSetting.Operation op in ops)
			{
				this.drpOptionalOP.Items.Add(new ListItem(op.OPCode,op.OPCode));
			}
		}

		/// <summary>
		/// 检查产品别是否使用回流，
		/// 如果使用，显示“不良原因组”按钮，
		/// 否则不显示
		/// </summary>
		private void CheckReflow(string itemcode)
		{
			BenQGuru.eMES.Domain.MOModel.Model model = new ModelFacade(base.DataProvider).GetModelByItemCode(itemcode);
			this.cmdNGReasonSelect.Visible = (model.IsReflow == BenQGuru.eMES.Web.Helper.FormatHelper.TRUE_STRING);
		}
		private void InitParameters()
		{
			if(this.Request.Params["opid"] == null)
			{
				ExceptionManager.Raise(this.GetType().BaseType,"$Error_RequestUrlParameter_Lost");
			}
			else
			{
				this.ViewState["opid"] = this.Request.Params["opid"];
			}
		}

		public string OPID
		{
			get
			{
				return (string) this.ViewState["opid"];
			}
		}


		private void SetEditObject(object obj)
		{
			this.chklstOPControlEdit_Load();
			if(obj == null)
			{
				this.txtItemRouteOperationSeqEdit.Text = string.Empty;
				this.txtItemOperationCodeEdit.Text = string.Empty;
                this.txtOPDescriptionQuery.Text = string.Empty;
				//(new OperationListFactory()).CreateOperationListCheckBoxList(this.chklstOPControlEdit,"0000000000000");
				(new OperationListFactory()).CreateNewOperationListCheckBoxList(this.chklstOPControlEdit,this.chklstOPAttributeEdit,this.languageComponent1);
				this.pnlMainEdit.Visible = false;
				this.PnlChildEdit.Visible = false;
				
				this.cmdSave.Disabled = true;
			}
			else
			{
				ItemRoute2OP itemRoute2OP = (ItemRoute2OP)obj;
				this.txtItemRouteOperationSeqEdit.Text = itemRoute2OP.OPSequence.ToString();
				this.txtItemOperationCodeEdit.Text = itemRoute2OP.OPCode;                

                BaseModelFacade baseModelFacade = new BaseModelFacade(this.DataProvider);
                Operation op = (Operation)baseModelFacade.GetOperation(itemRoute2OP.OPCode);
                if (op != null)
                {
                    this.txtOPDescriptionQuery.Text = op.OPDescription;
                }
                else
                {
                    this.txtOPDescriptionQuery.Text = string.Empty;
                }

				this.txtItemOperationCodeEdit.ReadOnly = true;
                this.txtOPDescriptionQuery.ReadOnly = true;
                (new OperationListFactory()).CreateOperationListCheckBoxList(this.chklstOPControlEdit, this.chklstOPAttributeEdit,itemRoute2OP.OPControl);
				if(FormatHelper.StringToBoolean(itemRoute2OP.OPControl,(int)OperationList.IDTranslation))
				{
					this.pnlMainEdit.Visible = true;
			
					this.drpMergeTypeEdit.SelectedValue = itemRoute2OP.IDMergeType;
					if(itemRoute2OP.IDMergeType == IDMergeType.IDMERGETYPE_ROUTER)
					{
						this.PnlChildEdit.Visible = true;
						this.txtDenominatorEdit.Text =itemRoute2OP.IDMergeRule.ToString();
					}
					else
					{
						this.PnlChildEdit.Visible = false;
					}
					
					
				}
				else
				{
					this.pnlMainEdit.Visible = false;
				}
				this.cmdSave.Disabled = false;

				drpOptionalOP.SelectedValue = itemRoute2OP.OptionalOP;
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

		protected void cmdSave_ServerClick(object sender, System.EventArgs e)
		{
			if(_itemFacade==null)
			{
				_itemFacade = new FacadeFactory(base.DataProvider).CreateItemFacade();
			}
			if(ValidateInput())
			{
				this._itemFacade.UpdateItemRoute2Op((ItemRoute2OP)GetEditObject());
			}
		}


		private bool ValidateInput()
		{
			PageCheckManager manager = new PageCheckManager();
			manager.Add( new NumberCheck(lblItemOperationSeqEdit, txtItemRouteOperationSeqEdit,-1,int.MaxValue, true) );

			if(this.pnlMainEdit.Visible)
			{
				manager.Add(new LengthCheck(this.lblMergeTypeEdit,drpMergeTypeEdit,40,true));
				if(this.PnlChildEdit.Visible)
				{
					manager.Add(new NumberCheck(lblMergeRule,txtDenominatorEdit,1,int.MaxValue,true));
				}
			}
			if ( !manager.Check() )
			{
				WebInfoPublish.Publish(this, manager.CheckMessage,languageComponent1);
				return false;
			}
			return true;
		}


		private object GetEditObject()
		{
			if(_itemFacade==null)
			{
				_itemFacade = new FacadeFactory(base.DataProvider).CreateItemFacade();
			}
			ItemRoute2OP itemRoute2OP = (ItemRoute2OP)this._itemFacade.GetItemRoute2Op(OPID, GlobalVariables.CurrentOrganizations.First().OrganizationID);
			try
			{
				itemRoute2OP.OPSequence= System.Int32.Parse(this.txtItemRouteOperationSeqEdit.Text.Trim());
			}
			catch
			{
				itemRoute2OP.OPSequence = 0;
			}

            itemRoute2OP.OPControl = (new OperationListFactory()).CreateOperationList(this.chklstOPControlEdit, this.chklstOPAttributeEdit);

			if(pnlMainEdit.Visible)
			{
				itemRoute2OP.IDMergeType = FormatHelper.CleanString(this.drpMergeTypeEdit.SelectedValue);
				if(this.PnlChildEdit.Visible)
				{
					itemRoute2OP.IDMergeRule = System.Int32.Parse( FormatHelper.CleanString(this.txtDenominatorEdit.Text));
				}
			}
			else
			{
				itemRoute2OP.IDMergeType = IDMergeType.IDMERGETYPE_IDMERGE;
			}

			itemRoute2OP.OptionalOP = drpOptionalOP.SelectedValue;
            
            itemRoute2OP.OrganizationID = GlobalVariables.CurrentOrganizations.First().OrganizationID;

            if (itemRoute2OP != null)
            {
                DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
                itemRoute2OP.MaintainUser = this.GetUserCode();
                itemRoute2OP.MaintainDate = dbDateTime.DBDate;
                itemRoute2OP.MaintainTime = dbDateTime.DBTime;
            }

			return itemRoute2OP;
		}

		private string GetOPControl()
		{
			string opcontrol = "";

			foreach( System.Web.UI.WebControls.ListItem item in this.chklstOPControlEdit.Items )
			{
				opcontrol = string.Format("{0}{1}", opcontrol, FormatHelper.BooleanToString(item.Selected));
			}

			return opcontrol;
		}


		private void InitdrpMergeTypeEdit()
		{
			this.drpMergeTypeEdit.Items.Clear();
			this.drpMergeTypeEdit.Items.Add(new ListItem("",""));
			this.drpMergeTypeEdit.Items.Add(new ListItem( this.languageComponent1.GetString(IDMergeType.IDMERGETYPE_IDMERGE),IDMergeType.IDMERGETYPE_IDMERGE));
			this.drpMergeTypeEdit.Items.Add(new ListItem( this.languageComponent1.GetString(IDMergeType.IDMERGETYPE_ROUTER),IDMergeType.IDMERGETYPE_ROUTER));
		}

		protected void chklstOPControlEdit_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(FormatHelper.StringToBoolean(GetOPControl(),(int)OperationList.IDTranslation))
			{
				this.pnlMainEdit.Visible = true;
				if(this.drpMergeTypeEdit.SelectedValue == IDMergeType.IDMERGETYPE_ROUTER)
				{
					this.PnlChildEdit.Visible = true;
				}
				else
				{
					this.PnlChildEdit.Visible = false;
				}
			}
			else
			{
				this.pnlMainEdit.Visible = false;
			}
		
		}

		protected void drpMergeTypeEdit_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(FormatHelper.CleanString(this.drpMergeTypeEdit.SelectedValue) == IDMergeType.IDMERGETYPE_ROUTER)
			{
				this.PnlChildEdit.Visible = true;
			}
			else
			{
				this.PnlChildEdit.Visible = false;
			}
		}

		private void chklstOPControlEdit_Load()
		{
			if( !this.IsPostBack )
			{
				if( this.chklstOPControlEdit.Items.Count == 0 )
				{
                    new OperationListFactory().CreateNewOperationListCheckBoxList(this.chklstOPControlEdit, this.chklstOPAttributeEdit,this.languageComponent1);
				}
			}
		}

		//设定这个回流点对应的不良原因组
		protected void cmdSelect_ServerClick(object sender, System.EventArgs e)
		{
			this.Response.Redirect(this.MakeRedirectUrl("./FItemOpCauseGroupAP.aspx", new string[]{"opid"}, new string[]{this.Request.Params["opid"]}));
		}
	}
}
