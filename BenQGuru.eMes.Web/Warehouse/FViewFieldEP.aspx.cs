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
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.Domain.Warehouse;
#endregion

namespace BenQGuru.eMES.Web.Warehouse
{
	/// <summary>
    /// ** Desc:FViewFieldEP 的摘要说明。
    /// ** Add : Jinger
    /// ** ADate: 2016-02-06
    /// ** Modify:
    /// ** MDate:
	/// </summary>
    public partial class FViewFieldEP : BasePage
	{
		private System.ComponentModel.IContainer components;
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;

        private InventoryFacade _InventoryFacade;
        private string defaultUserCode;
        private string tableName;

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

			this.Page.Load += new EventHandler(Page_Load);
			this.cmdSave.ServerClick += new EventHandler(cmdSave_ServerClick);
		}
		#endregion

		protected void Page_Load(object sender, EventArgs e)
		{
            defaultUserCode = this.GetRequestParam("defaultUserCode");
            tableName = this.GetRequestParam("table");
            if (string.IsNullOrEmpty(defaultUserCode) || string.IsNullOrEmpty(tableName))
            {
                 WebInfoPublish.Publish(this, "请在跳转页面URL中拼接参数：defaultUserCode和table", this.languageComponent1);
                return;
            }
			if (this.IsPostBack == false)
			{
                this.InitPageLanguage(this.languageComponent1, false);
				this.InitList();
                
			}
            this.cmdSave.Attributes.Add("OnClick", "try{window.returnValue='OK';window.close();}catch(e){}");
		}

		private void InitList()
		{
            if (_InventoryFacade == null)
                _InventoryFacade = new InventoryFacade(this.DataProvider);

			this.txtSelected.Value = ";";
			this.lstSelected.Items.Clear();          
            object[] objs = _InventoryFacade.QueryViewFieldByUserCode(this.GetUserCode(), tableName);
            bool bIsEmpty = false;
            if (objs != null)
            {
                for (int i = 0; i < objs.Length; i++)
                {
                    ViewField viewField = (ViewField)objs[i];
                    //string strText = /*languageComponent1.GetString(*/viewField.Description/*)*/;
                    //if (strText == string.Empty)
                    //    strText = languageComponent1.GetString(viewField.FieldName);
                    string strText = languageComponent1.GetString(viewField.FieldName);
                    if (strText == string.Empty)
                        strText =viewField.Description;
                    lstSelected.Items.Add(new ListItem(strText, viewField.FieldName));
                    txtSelected.Value += viewField.FieldName + ";";
                }
            }
            else
            {
                bIsEmpty = true;
            }
            objs = _InventoryFacade.QueryViewFieldDefault(defaultUserCode, tableName);
			lstUnSelected.Items.Clear();
			if (objs != null)
			{
				for (int i = 0; i < objs.Length; i++)
				{
                    ViewField viewField = (ViewField)objs[i];
					if (this.txtSelected.Value.IndexOf(";" + viewField.FieldName + ";") < 0)
					{
                        //string strText = /*languageComponent1.GetString(*/viewField.Description/*)*/;
                        //if (strText == string.Empty)
                        //    strText = languageComponent1.GetString(viewField.FieldName);

                        string strText = languageComponent1.GetString(viewField.FieldName);
                        if (strText == string.Empty)
                            strText = viewField.Description;

						lstUnSelected.Items.Add(new ListItem(strText, viewField.FieldName));
                        if (bIsEmpty == true && FormatHelper.StringToBoolean(viewField.IsDefault) == true)
                        {
                            lstSelected.Items.Add(new ListItem(strText, viewField.FieldName));
                            txtSelected.Value += viewField.FieldName + ";";
                        }
					}
				}
			}
		}

		private void cmdSave_ServerClick(object sender, EventArgs e)
		{
            if (_InventoryFacade == null)
                _InventoryFacade = new InventoryFacade(this.DataProvider);

            _InventoryFacade.SaveViewField(this.GetUserCode(),defaultUserCode,tableName, this.txtSelected.Value);
            //this.Page.RegisterStartupScript("close_window", "<script>window.returnValue='OK';window.close();</script>");
		}
	}
}
