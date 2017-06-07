<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FCartonDataImpMP.aspx.cs"
    Inherits="BenQGuru.eMES.Web.WarehouseWeb.FCartonDataImpMP" %>
<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns:igtbl>
<head>
    <title>FBasicDataImp</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="<%=StyleSheet%>" rel="stylesheet">
    <script language="javascript">
        function Check() {
            if (document.all.DownLoadPathBom.value == "") {
                alert("上传文件不能为空");
                return false;
            }
        }
         function LoadFile() {
		        //debugger;
		        try {

		            var hf = document.all.aFileDownLoad.href;
		            if (hf == "") return;
		            var path;
		            var pix = hf.substr(0, 4);

		            if (pix.toLowerCase() == "file") path = hf.substr(8, hf.length - 8);
		            else if (pix.toLowerCase() == "http") path = hf;
		            var fl = path.split('/');
		            var file = fl[fl.length - 2] + '/' + fl[fl.length - 1];
		            var strDownUrl = "http://" + fl[fl.length - 4] + "/" + fl[fl.length - 3] + "/FDownload.aspx?&fileName=" + escape(file);
		            window.open(strDownUrl, "ExportWindow", "top=60000,left=60000,height=1,width=1,status=no,toolbar=no,menubar=no,location=no");

		            return false;

		        } catch (e) { alert(e.description); }
		    }
        
    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="impForm" method="post" runat="server">
    <table id="Table1" style="z-index: 101; left: 8px; position: absolute; top: 8px"
        height="100%" width="100%" runat="server">
        <tbody>
         <tr class="moduleTitle">
            <td>
                <asp:Label ID="lblCartonImpTitle" runat="server" CssClass="labeltopic">供应商资料查询</asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table class="query" height="100%" width="100%">
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblASNSTNO" runat="server">入库指令号</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtASNSTNOQuery" runat="server" CssClass="textbox"></asp:TextBox>
                        </td>
                        
                        <td nowrap width="100%">
                        </td>
                        <td class="fieldName">
                            <input class="submitImgButton" id="cmdQuery" type="submit" value="查 询" name="btnQuery"
                                runat="server">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr><td style="height:"100px"></td></tr>
            <tr class="moduleTitle">
                <td>
                    <asp:Label ID="lblDataInmport" runat="server" CssClass="labeltopic">数据导入</asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <table class="query" id="Table2" height="100%" width="100%">
                        <tr align="right">
                            <td class="fieldNameLeft" nowrap height="20">
                                <asp:Label ID="lblImportType" Visible="false" runat="server"> 导入类型</asp:Label>
                            </td>
                            <td class="fieldNameLeft" nowrap height="20">
                                <asp:Label ID="lblInFile" runat="server"> 导入文件：</asp:Label>
                            </td>
                            <td style="width: 300px">
                                <input id="DownLoadPathBom" style="width: 454px; height: 22px" type="file" size="56"
                                       name="File1" runat="server"/>
                            </td>
                            <td class="fieldNameLeft" nowrap height="20">
                                <asp:Label ID="lblInTemplet" runat="server"> 导入模板：</asp:Label>
                            </td>
                            <td nowrap>
                                <a id="aFileDownLoad" style="display: none; color: blue" href="" target="_blank"
                                    runat="server">
                                    <asp:Label ID="lblDown" runat="server">下载</asp:Label></a> <span style="cursor: pointer;
                                        color: blue; text-decoration: underline" onclick="LoadFile()">  
                                        <asp:Label ID="lblDown1" runat="server">下载</asp:Label></span>
                            </td>
                            <td nowrap width="100%">
                            </td>
                            <td class="toolBar">
                                <input class="submitImgButton" id="cmdEnter" type="submit" value="导入" name="btnAdd"
                                    runat="server" onserverclick="cmdEnter_ServerClick">
                            </td>
                            <td>
                               <%-- <input class="submitImgButton" id="cmdReturn" type="submit" value="返 回" name="cmdReturn"
                                    runat="server" onserverclick="cmdReturn_ServerClick">--%>
                            </td>
                            <td align="center">
                              <%--  <input class="submitImgButton" id="cmdView" onmouseover="document.getElementById('cmdQuery').style.backgroundImage='url(&quot;../../Skin/Image/search_0.gif&quot;)';"
                                    disabled onmouseout="document.getElementById('cmdQuery').style.backgroundImage='url(&quot;../../Skin/Image/search.gif&quot;)';"
                                    type="submit" value="察 看" name="cmdQuery" visible="false" runat="server" onserverclick="cmdEnter_ServerClick">--%>
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td colspan="8">
                                <div id="DIVForDownload" style="width: 1px; position: relative; height: 1px" runat="server"
                                    ms_positioning="GridLayout">
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr height="100%">
            <td class="fieldGrid">
                <ig:webdatagrid id="gridWebGrid" runat="server" width="100%">
                </ig:webdatagrid>
            </td>
        </tr>
        <tr class="normal">
            <td>
                <table height="100%" cellpadding="0" width="100%">
                    <tr>
                        
                        <td class="smallImgButton">
                            <input class="gridExportButton" id="cmdGridExport" type="submit" visible="false"  value="  " name="cmdGridExport"
                                runat="server">
                            
                        </td>
                        <%--
                        <td class="smallImgButton">
                            <input class="deleteButton" id="cmdDelete" type="submit" visible="false"  value="  " name="cmdDelete"
                                runat="server">
                            
                        </td>--%>
                        <td>
                            <cc1:PagerSizeSelector ID="pagerSizeSelector" runat="server"></cc1:PagerSizeSelector>
                        </td>
                        <td align="right">
                            <cc1:PagerToolBar ID="pagerToolBar" runat="server"></cc1:PagerToolBar>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        
          <tr class="toolBar">
            <td>
                <table class="toolBar">
                    <tr>
                             <td class="toolBar">
                            <input class="submitImgButton" id="cmdReturn" type="submit"
                                value="返回" name="cmdReturn" runat="server" onserverclick="cmdReturn_ServerClick"/>
                        </td>

                    </tr>
                    <tr style="display: none">
                      <td class="fieldValue" style="width: 159px">
                            <asp:TextBox ID="txtPageEdit" runat="server" Width="150px" DESIGNTIMEDRAGDROP="257"
                                CssClass="textbox"></asp:TextBox>
                        </td>
                      </tr>
                </table>
            </td>
        </tr>
        </tbody>
    </table>
    </form>
</body>
</html>
