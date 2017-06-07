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
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Web.UserControl;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;
using System.Collections.Generic;

namespace BenQGuru.eMES.Web.Helper
{
    /// <summary>
    /// Selector 的摘要说明。
    /// </summary>
    public class BaseSingleSelectorPageNew2 : BasePageSelectNew
    {
        protected System.ComponentModel.IContainer components;
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;

        //protected GridHelperNew gridSelectedHelper = null;
        //protected GridHelperNew gridUnSelectedHelper = null;

        // 分隔符
        protected const string DATA_SPLITER = ",";
        protected bool writerOutted = false;


        #region Web 窗体设计器生成的代码
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
            //
            InitializeComponent();
            base.OnInit(e);
            this.gridUnSelected = this.gridWebGrid;
            this.gridUnSelected.ClientEvents.Click = "SelectGrid_ClientClick";
            //this.gridUnSelected.ClientEvents.Initialize = "SelectGrid_Initialize";

        }   

        protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
        {
            return this.languageComponent1;
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
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion

        #region Init
        private void Page_Load(object sender, System.EventArgs e)
        {
            Control control;

            control = this.FindControl("cmdQuery");
            if (control != null)
            {
                ((System.Web.UI.HtmlControls.HtmlInputButton)control).ServerClick += new System.EventHandler(this.cmdQuery_ServerClick);
            }
            this.gridUnSelectedHelper = new GridHelperNew(this.GetGridUnSelected(), DtSourceUnSelected);
            this.gridUnSelectedHelper.BuildGridRowhandle = new BuildGridRowDelegateNew(this.GetUnSelectedGridRow);
            this.gridUnSelectedHelper.GetRowCountHandle = new GetRowCountDelegateNew(this.GetUnSelectedRowCount);
            this.gridUnSelectedHelper.LoadDataSourceHandle = new LoadDataSourceDelegateNew(this.LoadUnSelectedDataSource);
            this.gridHelper = this.gridUnSelectedHelper;
            control = this.FindControl("cmdSave");
            if (control != null)
            {
                ((System.Web.UI.HtmlControls.HtmlInputButton)control).Attributes.Add("OnClick", "try{window.parent.returnValue=$('#txtSelected').val();window.parent.close();return false ;}catch(e){}");
            }
            control = this.FindControl("cmdCancel");
            if (control != null)
            {
                ((System.Web.UI.HtmlControls.HtmlInputButton)control).Attributes.Add("OnClick", "window.parent.close();return false ;");
            }
            control = this.FindControl("cmdInit");
            if (control != null)
            {
                ((System.Web.UI.HtmlControls.HtmlInputButton)control).ServerClick += new System.EventHandler(this.cmdInit_ServerClick);
            }



            if (!this.IsPostBack)
            {
                this.InitWebGrid();
                this.Page.ClientScript.RegisterClientScriptInclude("Jquery", this.VirtualHostRoot + "Scripts/jquery-1.9.1.js");

                //Grid相关事件脚本，初始化以及点击
                if (!this.Page.ClientScript.IsStartupScriptRegistered("GridEvents"))
                {
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "GridEvents", @"
        //checkbox点击
        //checkbox点击
        function SelectGrid_ClientClick(sender, e) {
        try

        {
            if (e.get_item() == null)
                return;
            
            if(e.get_item()._type=='header')
            {
                if (e.get_item().get_column().get_key() == 'Check')
                {
                    //alert(1);
                     var a='';
                    for (var i = 0; i < sender.get_rows().get_length(); i++) {
                        if(sender.get_rows().get_row(i).get_cellByColumnKey('Check').get_value())
                        {
                        a+= sender.get_rows().get_row(i).get_cellByColumnKey('Selector_UnselectedCode').get_value()+',';
                        }
                    }
                    if(a.length>1)
                    {a=a.substring(0,a.length-1); }
                    $('#txtSelected').val(a);
                }
                return;
            }

            if (e.get_item().get_column().get_key() != 'Check')
                return;
        }
        catch(e)
        {
            return;
        }            
               var a='';
              for (var i = 0; i < sender.get_rows().get_length(); i++) {
                 if(sender.get_rows().get_row(i).get_cellByColumnKey('Check').get_value())
                 {
                   a+= sender.get_rows().get_row(i).get_cellByColumnKey('Selector_UnselectedCode').get_value()+',';
                 }
              }
              if(a.length>1)
             {a=a.substring(0,a.length-1); }
              $('#txtSelected').val(a);
//            var index = e.get_item().get_row().get_index();
//            if(sender.get_rows().get_row(index).get_cellByColumnKey('Check').get_value()==true)
//            {
//                $('#txtSelected').val(e.get_item().get_row().get_cellByColumnKey('Selector_UnselectedCode').get_value());
//            }
//            else
//            {
//                $('#txtSelected').val('');
//            }
//            for (var i = 0; i < sender.get_rows().get_length(); i++) {
//                if (index != i)
//                    sender.get_rows().get_row(i).get_cellByColumnKey('Check').set_value(false);
//
//            }
               
        }
            ", true);

                }
                //注册scriptManager的脚本
                if (!this.Page.ClientScript.IsStartupScriptRegistered("SingleSelect"))
                {
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "SingleSelect", @" 
            Sys.WebForms.PageRequestManager.getInstance().add_initializeRequest(initializeRequest);
            function initializeRequest(sender, args)
            {
             //弹出的点选页面，点击勾选框列阻止页面回传,主要实现单选效果
              if (args.get_postBackElement().id =='gridUnSelected'&&$('#__EVENTARGUMENT').val()!='ColumnSorting') {
                
                 Sys.WebForms.PageRequestManager.getInstance().abortPostBack();
                 args.set_cancel(true);
              }
            }
                ", true);
                }
                this.cmdQuery_ServerClick(null, null);
            }

            SelectableTextBox_Load(null, null);
        }

        #endregion

        #region WebGrid

        protected override void InitWebGrid()
        {
            base.InitWebGrid();
            this.gridUnSelectedHelper.AddCheckBoxColumn1(this.gridUnSelectedHelper.CheckColumnKey, "", false);
            this.gridUnSelectedHelper.AddColumn("Selector_UnselectedCode", "未选择的项目", null);
            this.gridUnSelectedHelper.AddColumn("Selector_UnSelectedDesc", "描述", null);
            //this.gridUnSelectedHelper.AddDefaultColumn(false, false);
        }

        protected virtual DataRow GetSelectedGridRow(object obj)
        {
            return null;
        }

        protected virtual DataRow GetUnSelectedGridRow(object obj)
        {
            return null;
        }

        protected virtual object[] LoadSelectedDataSource(int inclusive, int exclusive)
        {
            return null;
        }

        protected virtual object[] LoadUnSelectedDataSource(int inclusive, int exclusive)
        {
            return null;
        }

        protected virtual int GetUnSelectedRowCount()
        {
            return 0;
        }



        protected virtual void cmdInit_ServerClick(object sender, System.EventArgs e)
        {

        }

        #endregion

        #region Misc


        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
            if (!writerOutted)
            {
                //writer.Write("<script language=javascript>try{ResetSelectAllPosition('chbUnSelected','gridUnSelected');ResetSelectAllPosition('chbSelected','gridSelected');}catch(e){};</script>");
                //writer.Write("<script language=javascript>try{if(window.top.valueLoaded != true){document.getElementById('txtSelected').innerText = window.top.dialogArguments.Codes;document.getElementById('cmdInit').click();window.top.valueLoaded = true ;}}catch(e){};</script>");
            }
        }

        protected void cmdQuery_ServerClick(object sender, System.EventArgs e)
        {
            RequestData();
        }

        private void RequestData()
        {
            this.gridUnSelectedHelper.RequestData();
        }

        protected virtual WebDataGrid GetGridUnSelected()
        {
            Control control = this.FindControl("gridUnselected");
            if (control == null)
            {
                return null;
            }
            return (WebDataGrid)control;
        }

        #endregion

        #region 注册客户端事件

        private void SelectableTextBox_Load(object sender, EventArgs e)
        {
            if (!this.ClientScript.IsStartupScriptRegistered("SelectableTextBox_Startup_js"))
            {
                string scriptString = string.Format("<script>var STB_Virtual_Path = \"{0}\";</script><script src='{0}SelectQuery/selectableTextBox.js'></script>", this.VirtualHostRoot);

                this.ClientScript.RegisterStartupScript(this.GetType(), "SelectableTextBox_Startup_js", scriptString, false);
            }

        }

        #endregion
    }
}

