<%@ Page Language="c#" CodeBehind="FOPBOMOperationListMP.aspx.cs" AutoEventWireup="True"
    Inherits="BenQGuru.eMES.Web.MOModel.FOPBOMOperationListMP" %>

<%@ Register TagPrefix="ignav" Namespace="Infragistics.WebUI.UltraWebNavigator" Assembly="Infragistics35.WebUI.UltraWebNavigator.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>FOPBOMOperationListMP</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="<%=StyleSheet%>" rel="stylesheet">
    <script language="javascript">
        function BeforeNodeChange(treeId, oldNodeId, newNodeId) {
            var onode = igtree_getNodeById(newNodeId);
            var ss = document.all.lbltag;
            ss.innerHTML = '<font color="#FF0000">' + onode.getTag() + '</font>';
        }
			
    </script>
    <script src="../Scripts/jquery-1.9.1.js" type="text/javascript"></script>
    <script type="text/javascript">
        var contentOtherHeight;
        function FrameLoad(frame) {

            //调整子页面中下方按钮居中，多浏览器
            $("#OPBOMFrame").contents().find('table[class=\'toolBar\']').wrap('<center></center>');

            var grid = $("#OPBOMFrame").contents().find("#gridWebGrid");

            if (grid.html() != null) {

                contentOtherHeight = 0;
                grid.closest("tbody").children("tr:visible").each(function () {

                    if ($(this).find("#gridWebGrid").html() == null) {
                        contentOtherHeight += $(this).outerHeight();
                    }
                });
                //记录呈现时Grid的宽度
                $("#frmWorkSpace").contents().find("#gridWidth").val(grid.width() - 18);
            }
            dyniframesize();
        }

        function dyniframesize() {

            //iframe高度自适应
            var pTar = null;
            if (document.getElementById) {
                pTar = document.getElementById('OPBOMFrame');
            }
            else {
                eval("pTar = 'OPBOMFrame';");
            }


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
            var grid = $("#OPBOMFrame").contents().find("#gridWebGrid");

            //iframe高度改变之后，调整其中Grid的高度
            if (grid.height() != null)
                grid.height(pTar.height - contentOtherHeight - 20);

            $("#OPBOMFrame").contents().find("#gridHeigt").val(pTar.height - contentOtherHeight - 20);

        }
        window.onresize = function () {
            dyniframesize();
        }
  
    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
    <table width="100%">
        <!--this is for tilte-->
        <tr>
            <td>
                <asp:Label ID="lblTitleBom" runat="server" CssClass="labeltopic"> 工序BOM维护</asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label
                    ID="lbltag" runat="server" CssClass="labeltopic"></asp:Label>
            </td>
        </tr>
        <!--this is for tilte-->
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td class="tree">
                            <ignav:UltraWebTree ID="uwtreeOPBOM" runat="server" TargetFrame="OPBOMFrame" ExpandImage="ig_treePlus.gif"
                                CollapseImage="ig_treeMinus.gif" ImageDirectory="/ig_common/WebNavigator31/"
                                Indentation="20" Cursor="Hand" Font-Size="12px" WebTreeTarget="ClassicTree" Height="98%"
                                Width="250px">
                                <SelectedNodeStyle ForeColor="White" BackColor="Navy"></SelectedNodeStyle>
                                <Levels>
                                    <ignav:Level Index="0"></ignav:Level>
                                    <ignav:Level Index="1"></ignav:Level>
                                </Levels>
                                <ClientSideEvents BeforeNodeSelectionChange="BeforeNodeChange"></ClientSideEvents>
                            </ignav:UltraWebTree>
                        </td>
                        <td>
                            <%--<iframe id="OPBOMFrame" style="border-right: #0066ff thick; padding-right: 0px; border-top: #0066ff thick;
                                padding-left: 0px; padding-bottom: 0px; margin: 0px; border-left: #0066ff thick;
                                width: 100%; padding-top: 4px; border-bottom: #0066ff thick; height: 100%" marginwidth="0"
                                marginheight="0" frameborder="no" width="100%" scrolling="no" runat="server"
                                onload="FrameLoad(this);"></iframe>--%>
                                <iframe id="OPBOMFrame" style="width: 100%; border-top-style: none; border-right-style: none;
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
