<%@ Page Language="c#" CodeBehind="FItemRouteOperationListMP.aspx.cs" AutoEventWireup="True"
    Inherits="BenQGuru.eMES.Web.MOModel.FItemRouteOperationListMP" %>

<%@ Register TagPrefix="ignav" Namespace="Infragistics.WebUI.UltraWebNavigator" Assembly="Infragistics35.WebUI.UltraWebNavigator.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>FItemRouteOperationListMP</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
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
                pTar = document.getElementById('ItemRouteFrame');
            }
            else {
                eval("pTar = 'ItemRouteFrame';");
            }


            if (pTar && !window.opera) {
                //begin resizing iframe 
                //pTar.style.display = "block"
                if (BrowserType().ie ||BrowserType().firefox) {
                    pTar.height = $(window).height() -30;
                }
                else if (pTar.contentDocument && pTar.contentDocument.body.offsetHeight) {
                    //ns6 syntax 
                    pTar.height = $(window).height() -30;
                    //pTar.scrolling = "no";
                }
                else if (pTar.Document && pTar.Document.body.scrollHeight) {
                    //ie5+ syntax 
                    pTar.height = $(document).height() -30;
                    //pTar.width =document.body.scrollWidth;
                }
            }

            //IE下需要再次调整高度
            if (pTar && !window.opera) {
                //begin resizing iframe 
                //pTar.style.display = "block"
                if (BrowserType().ie || BrowserType().firefox) {
                    pTar.height = $(window).height() -30;

                }
                else if (pTar.contentDocument && pTar.contentDocument.body.offsetHeight) {
                    //ns6 syntax 
                    pTar.height = $(window).height() -30;
                    //pTar.scrolling = "no";
                }
                else if (pTar.Document && pTar.Document.body.scrollHeight) {
                    //ie5+ syntax 
                    pTar.height = $(document).height() -30;
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
    <script language="javascript">
        window.onload = function body_onload() {
            if (window.top.MOManagementReloaded != true) {
                window.top.MOManagementReloaded = true;
                location = location;
            }
        }

        function clearFlag() {
            window.top.MOManagementReloaded = null
        }
    </script>
    <style>
        .wrap
        {
            width: 250px;
            word-wrap: break-word;
            word-break: break-all;
        }
    </style>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
    <table width="100%">
        <!--this is for tilte-->
        <tr">
            <td>
                <asp:Label ID="lblTitle" runat="server" CssClass="labeltopic"> 产品生产途程详细信息</asp:Label>
            </td>
        </tr>
        <!--this is for tilte-->
        <tr>
            <td>
                <table height="100%" width="100%">
                    <tr>
                        <td class="tree">
                            <ignav:UltraWebTree ID="uwtreeItemRoute" runat="server" Width="250px" Height="98%"
                                WebTreeTarget="ClassicTree" Font-Size="12px" Cursor="Hand" Indentation="20" ImageDirectory="/ig_common/WebNavigator31/"
                                CollapseImage="ig_treeMinus.gif" ExpandImage="ig_treePlus.gif" TargetFrame="ItemRouteFrame"
                                CssClass="wrap">
                                <SelectedNodeStyle ForeColor="White" BackColor="Navy"></SelectedNodeStyle>
                                <Levels>
                                    <ignav:Level Index="0"></ignav:Level>
                                    <ignav:Level Index="1"></ignav:Level>
                                </Levels>
                            </ignav:UltraWebTree>
                        </td>
                        <td width="100%">
                            <%--<iframe id="ItemRouteFrame" style="border-right: #0066ff thick; padding-right: 0px;
                                border-top: #0066ff thick; padding-left: 0px; padding-bottom: 0px; margin: 0px;
                                border-left: #0066ff thick; width: 100%; padding-top: 4px; border-bottom: #0066ff thick;
                                height: 100%" marginwidth="0" marginheight="0" frameborder="0"
                                scrolling="no" runat="server" onload="FrameLoad(this);"></iframe>--%>
                            <iframe id="ItemRouteFrame" style="width: 100%; border-top-style: none; border-right-style: none;
                                border-left-style: none; background-color: transparent; border-bottom-style: none"
                                name="ItemRouteFrame" src="" frameborder="0" width="100%" scrolling="no" onload="FrameLoad(this);">
                            </iframe>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
