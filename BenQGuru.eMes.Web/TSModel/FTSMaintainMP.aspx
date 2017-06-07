<%@ Page Language="c#" CodeBehind="FTSMaintainMP.aspx.cs" AutoEventWireup="True" Inherits="BenQGuru.eMES.Web.TSModel.FTSMaintainMP" %>
<%@ Register TagPrefix="ignav" Namespace="Infragistics.WebUI.UltraWebNavigator" Assembly="Infragistics35.WebUI.UltraWebNavigator.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>FTSMaintainMP</title>
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <link href="<%=StyleSheet%>" rel="stylesheet">
       <script src="../Scripts/jquery-1.9.1.js" type="text/javascript"></script>
    <script type="text/javascript">

        function FrameLoad(frame) {
 
            dyniframesize();
        }

        function dyniframesize() {

            //iframe高度自适应
            var pTar = null;
            if (document.getElementById) {
                pTar = document.getElementById('frameTS');
            }
            else {
                eval("pTar = 'frameTS';");
            }


            if (pTar && !window.opera) {
                //begin resizing iframe 
                //pTar.style.display = "block"
                if (BrowserType().ie || BrowserType().firefox) {
                    pTar.height = $(window).height()  -10;
                }
                else if (pTar.contentDocument && pTar.contentDocument.body.offsetHeight) {
                    //ns6 syntax 
                    pTar.height = $(window).height()  -10;
                    //pTar.scrolling = "no";
                }
                else if (pTar.Document && pTar.Document.body.scrollHeight) {
                    //ie5+ syntax 
                    pTar.height = $(document).height()  -10;
                    //pTar.width =document.body.scrollWidth;
                }
            }

            //IE下需要再次调整高度
            if (pTar && !window.opera) {
                //begin resizing iframe 
                //pTar.style.display = "block"
                if (BrowserType().ie || BrowserType().firefox) {
                    pTar.height = $(window).height()  -10;

                }
                else if (pTar.contentDocument && pTar.contentDocument.body.offsetHeight) {
                    //ns6 syntax 
                    pTar.height = $(window).height()  -10;
                    //pTar.scrolling = "no";
                }
                else if (pTar.Document && pTar.Document.body.scrollHeight) {
                    //ie5+ syntax 
                    pTar.height = $(document).height()  -10;
                    //pTar.width =document.body.scrollWidth;
                }
            }
            //pTar.scrolling = "no";

            //火狐需要额外调整iframe的高度
            if (parent.window.BrowserType().firefox) {
                while ($(window).height() < $(document).height()) {
                    pTar.height--;
                }

            }
         

        }
        window.onresize = function () {
            dyniframesize();
        }
    </script>
</head>
<body topmargin="0" ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
    <table id="tableMain" style="z-index: 101; left: 0px; width: 100%; position: absolute;
        top: 0px; height: 100%" cellspacing="0" cellpadding="0" width="300" border="0">
        <tr style="height: 4px">
        </tr>
        <tr style="height: 98%">
            <td class="tree">
                <ignav:UltraWebTree ID="treeTS" runat="server" ExpandImage="ig_treePlus.gif" CollapseImage="ig_treeMinus.gif"
                    TargetFrame="ifPage" ImageDirectory="/ig_common/WebNavigator31/" Indentation="20"
                    Cursor="Hand" Font-Size="12px" WebTreeTarget="ClassicTree" Height="100%" Width="250px"
                    CssClass="tree">
                    <SelectedNodeStyle ForeColor="White" BackColor="Navy"></SelectedNodeStyle>
                    <Levels>
                        <ignav:Level Index="0"></ignav:Level>
                        <ignav:Level Index="1"></ignav:Level>
                    </Levels>
                </ignav:UltraWebTree>
            </td>
            <td width="100%">
                <iframe id="frameTS" style="width: 100%; border-top-style: none; border-right-style: none;
                    border-left-style: none;background-color: transparent; border-bottom-style: none"
                    name="frameTS" src="" frameborder="0" width="100%" scrolling="no"  onload="FrameLoad(this);">
                </iframe>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
