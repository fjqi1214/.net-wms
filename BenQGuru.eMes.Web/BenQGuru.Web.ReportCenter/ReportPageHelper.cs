using System;
using System.Data;
using System.Collections.Specialized;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Web.SelectQuery;
using BenQGuru.eMES.Web.UserControl;

namespace BenQGuru.Web.ReportCenter
{
    public delegate void QueryMethod();

    public class ReportPageHelper
    {
        private const string _IsForBSHome = "IsForBSHome";

        #region 设定滚动条滚动到底部

        public static void SetPageScrollToBottom(Page page)
        {
            string strScript = " $('html,body').animate({scrollTop: $(document).height()-100}, 1);   ";
            page.ClientScript.RegisterClientScriptBlock(page.GetType(), "ScrollToBottom1", strScript,true);
            ScriptManager.RegisterClientScriptBlock(page, page.GetType(), "ScrollToBottom1", strScript, true);
        }

        #endregion

        #region 用于BS首页图示

        private static void SetInputValue(Control parentControl, string childControlID, string controlValue)
        {
            try
            {
                Control control = parentControl.FindControl(childControlID);
                if (control != null)
                {
                    if (control is TextBox)
                    {
                        ((TextBox)control).Text = controlValue;
                    }
                    else if (control is DropDownList)
                    {
                        ((DropDownList)control).SelectedValue = controlValue;
                    }
                    if (control is RadioButtonList)
                    {
                        ((RadioButtonList)control).SelectedValue = controlValue;
                    }
                    if (control is CheckBox)
                    {
                        ((CheckBox)control).Checked = bool.Parse(controlValue);
                    }
                    else if (control is SelectableTextBox)
                    {
                        ((SelectableTextBox)control).Text = controlValue;
                    }
                    else if (control is HtmlInputControl)
                    {
                        ((HtmlInputControl)control).Value = FormatHelper.ToDateString(int.Parse(controlValue));
                    }
                    else if (control is eMESTime)
                    {
                        ((TextBox)control).Text = FormatHelper.ToTimeString(int.Parse(controlValue));
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private static void SetOWCChartSpace(Control control)
        {
            try
            {
                foreach (Control child in control.Controls)
                {
                    if (child is OWCChartSpace)
                    {
                        ((OWCChartSpace)child).Attributes["width"] = "450";
                        ((OWCChartSpace)child).Attributes["Height"] = "240";
                        ((OWCChartSpace)child).Display = true;
                    }
                    else
                    {
                        SetOWCChartSpace(child);
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private static void SetChild(Control parentControl, string paramKey, string paramValue)
        {
            if (parentControl == null)
            {
                return;
            }

            string[] part = paramKey.Split('.');
            if (part != null && part.Length >= 2)
            {
                if (part.Length == 2)
                {
                    string parentID = part[0].Trim();
                    string childID = part[1].Trim();

                    if (string.Compare(parentControl.ID, parentID) == 0)
                    {
                        if (string.Compare(childID, _IsForBSHome, true) == 0)
                        {
                            bool isForBSHome = false;
                            bool.TryParse(paramValue, out isForBSHome);
                            if (isForBSHome)
                            {
                                if (parentControl is Page)
                                {
                                    ((Page)parentControl).ClientScript.RegisterStartupScript(parentControl.GetType(), "PutScroll",
                                                @"
                                            $(function(){
                                            var strFlage='';
                                            if($('form').children().get(0).tagName=='DIV')
                                                strFlage='#up1';
                                            else
                                                strFlage='form';

                                           $(strFlage).children('table').first().children('tbody').children('tr').each(function()
                                            {
                                                $(this).hide();
                                            });

                                            $(strFlage).children('table').first().children('tbody').children('tr').last().prev().show();
                                            $('#gridWebGrid').hide();
                                            });", true);
                                    ((Page)parentControl).ClientScript.RegisterClientScriptBlock(parentControl.GetType(), "HideScroll", @"<script language='javascript'>document.body.scroll='no';</script>");
//                                    ((Page)parentControl).ClientScript.RegisterStartupScript(parentControl.GetType(), "SetDisplayBlock",
//                                                @"
//                                            function SetDisplayBlock(){
//                                             var grid=$('#gridWebGrid');
//                                             var chartDiv=$('div[id$=\'Chart\']');
//                                             if(grid.html()!=null)
//                                             {
//                                                 grid.closest('tr').closest('tr').css('display','block');
//                                             }
//                                             else if(chartDiv.html()!=null)
//                                             {
//                                                 chartDiv.closest('tr').closest('tr').css('display','block');
//                                             }
//                                            };", true);
                                }

                                SetOWCChartSpace(parentControl);
                            }
                        }
                        else
                        {
                            SetInputValue(parentControl, childID, paramValue);
                        }
                    }
                }
            }
        }

        public static void SetControlValue(Control parentControl, NameValueCollection requestParams)
        {
            foreach (string key in requestParams.Keys)
            {
                if (key != null)
                if (key.IndexOf(".") >= 0)
                {
                    SetChild(parentControl, key, requestParams[key]);
                }
            }
        }

        public static void DoQueryForBSHome(Control page, NameValueCollection requestParams, QueryMethod queryMethod)
        {
            if (requestParams[page.ID + "." + _IsForBSHome] != null)
            {
                bool isForBSHome = false;
                bool.TryParse(requestParams[page.ID + "." + _IsForBSHome], out isForBSHome);
                if (isForBSHome)
                {
                    queryMethod();
                }
            }
        }

        #endregion
    }
}
