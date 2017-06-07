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
    public class BasePageSelectNew : BaseMPageMinus
    {
        protected GridHelperNew gridSelectedHelper = null;
        protected GridHelperNew gridUnSelectedHelper = null;

        protected WebDataGrid gridUnSelected;
        protected WebDataGrid gridSelected;
        public DataTable DtSourceUnSelected
        {
            get
            {
                return this.DtSource;
            }
            set
            {
                this.DtSource = value;
            }
        }
        public DataTable DtSourceSelected
        {
            get
            {
                return this.DtSource2;
            }
            set
            {
                this.DtSource2 = value;
            }
        }

        protected override void AddParsedSubObject(object obj)
        {
            this.gridWebGridId = "gridUnSelected";
            this.gridWebGrid2Id = "gridSelected";
            base.AddParsedSubObject(obj);
        }
        protected override void OnInit(EventArgs e)
        {
            this.gridWebGridId = "gridUnSelected";
            this.gridWebGrid2Id = "gridSelected";
            base.OnInit(e);
            this.Load += new System.EventHandler(this.Page_Load);
            this.gridUnSelected = this.gridWebGrid;
            this.gridSelected = this.gridWebGrid2;

            if (this.gridSelected != null)
            {
                this.gridUnSelected.Behaviors.CreateBehavior<EditingCore>().EditingClientEvents.CellValueChanged = "CellValueChanged";
                this.gridSelected.Behaviors.CreateBehavior<EditingCore>().EditingClientEvents.CellValueChanged = "CellValueChanged";
            }
        }


        private void Page_Load(object sender, System.EventArgs e)
        {
         
            this.Page.ClientScript.RegisterClientScriptInclude("Jquery", this.VirtualHostRoot + "Scripts/jquery-1.9.1.js");

            //调整Grid高度和宽度初始化脚本
            if (!this.Page.ClientScript.IsStartupScriptRegistered("SelectPageloadScripts"))
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "SelectPageloadScripts", @"

        $(function () {    
  
           var windowHeight = 590;

           var gridUnSelected = $('#" + gridWebGridId + @":visible');
           var gridSelected = $('#" + gridWebGrid2Id + @":visible');

           var contentOtherHeight=0;

                $('form').find('table:first').children('tbody').children('tr:visible').each(function () {

                    if ($(this).find('#" + gridWebGridId + @"').html() == null&&
                    $(this).find('#" + gridWebGrid2Id + @"').html() == null) {
                        contentOtherHeight += $(this).outerHeight();
                    }
                });
               
            if(gridSelected.html()!=null){
                gridUnSelected.height((windowHeight-contentOtherHeight)/2-10);
                gridSelected.height((windowHeight-contentOtherHeight)/2-10);
              
            }
            else
            {
                gridUnSelected.height(windowHeight-contentOtherHeight);
            }
            $('#gridHeigt').val(windowHeight-contentOtherHeight);
            

            if($('#txtSelected').html()!=null)
            {
                $('#txtSelected').val(window.parent.dialogArguments.Codes);
            }

            if($('#txtOthers').html()!=null)
            {
                $('#txtOthers').val(window.parent.dialogArguments.Others);
            }
//            if($('#cmdInit').html()!=null)    
//            {       
//                try
//                { 
//                    $('#cmdInit').click();       
//                }
//                catch(e)
//                {
//                }
//            }
            
        });   

            
        ", true
             );
            }
        }
    }
}