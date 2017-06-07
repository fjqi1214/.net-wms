using System;
using System.Web;
using System.Web.UI;

using BenQGuru.eMES.Common;

namespace BenQGuru.eMES.Web.Helper
{
    public enum PublishForm
    {
        Script, Exception, Alert, InputCheck
    }
    /// <summary>
    /// WebInfoPublish 的摘要说明。
    /// </summary>
    public class WebInfoPublish
    {
        /// <summary>
        /// 是否使用新的方式，DIV弹出
        /// </summary>
        public static bool isUseDiv = true;//added by Gawain@20130905

        public static void Publish(Page page, string text, PublishForm form, ControlLibrary.Web.Language.ILanguageComponent languageComponent)
        {
            if (form == PublishForm.Exception)
            {
                ExceptionManager.Raise(page.GetType().BaseType, text);
            }
            else if (form == PublishForm.Script ||
                form == PublishForm.Alert ||
                form == PublishForm.InputCheck)
            {
                if (!isUseDiv)
                {
                    string alertInfo = "<script language=javascript>if(window.name.indexOf('[back]')<0){alert('"
                    + MessageCenter.ParserMessage(text, languageComponent).Replace("\n", "\\n")
                    + "');window.name=window.name.replace('[back]','');}else{window.name=window.name.replace('[back]','');}</script>";
                                   
                    if (!page.ClientScript.IsClientScriptBlockRegistered("ExceptionAlert"))
                    {
                        page.ClientScript.RegisterClientScriptBlock(page.GetType(),"ExceptionAlert", alertInfo,false);

                    }
                    ScriptManager.RegisterStartupScript(page, page.GetType(), Guid.NewGuid().ToString(), "alert('" + MessageCenter.ParserMessage(text, languageComponent).Replace("\n", "\\n") + "');", true);

                }
                else
                {
                    //使用自定义弹出DIV提示
                    string stcrSript = string.Format(@"
    
                try
                {{
                    window.top.showMessageDialog('{0}');
                }}
                catch(e)
                {{
                    alert('{0}');
                }}", MessageCenter.ParserMessage(text, languageComponent).Replace("\n", "<br />"));

                    //page.ClientScript.RegisterClientScriptBlock(page.GetType(), Guid.NewGuid().ToString(), stcrSript,true);
                    ScriptManager.RegisterStartupScript(page, page.GetType(), Guid.NewGuid().ToString(), stcrSript, true);
                }
            }
        }

        public static void Publish(Page page, string text, ControlLibrary.Web.Language.ILanguageComponent languageComponent)
        {
            Publish(page, text, PublishForm.Script, languageComponent);
        }

        public static void PublishInfo(Page page, string text, ControlLibrary.Web.Language.ILanguageComponent languageComponent)
        {
            if (!isUseDiv)
            {
                ClientScriptManager clientScriptManager = page.ClientScript;
                Type clientScriptType = page.GetType();

                string alertInfo = "<script language=javascript>if(window.name.indexOf('[back]')<0){alert('"
                    + MessageCenter.ParserMessage(text, languageComponent).Replace("\n", "\\n")
                    + "');}else{window.name=window.name.replace('[back]','');}</script>";

                if (!clientScriptManager.IsClientScriptBlockRegistered(clientScriptType, "ExceptionAlert"))
                {
                    clientScriptManager.RegisterClientScriptBlock(clientScriptType, "ExceptionAlert", alertInfo);
                    
                }
                ScriptManager.RegisterStartupScript(page, page.GetType(), Guid.NewGuid().ToString(), "alert('" + MessageCenter.ParserMessage(text, languageComponent).Replace("\n", "\\n") + "');", true);
            }
            else
            {
                //使用自定义弹出DIV提示
                string stcrSript = string.Format(@"
                
                try{{
                    window.top.showMessageDialog('{0}');
                }}
                catch(e)
                {{
                    alert('{0}')
                }}
                ", MessageCenter.ParserMessage(text, languageComponent).Replace("\n", "<br />"));
                page.ClientScript.RegisterClientScriptBlock(page.GetType(), Guid.NewGuid().ToString(), stcrSript,true);
                ScriptManager.RegisterStartupScript(page, page.GetType(), Guid.NewGuid().ToString(), stcrSript, true);
            }
        }
    }
}
