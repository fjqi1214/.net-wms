<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FSoftWareVersionMP.aspx.cs"
    Inherits="BenQGuru.eMES.Web.BaseSetting.FSoftWareVersionMP" %>
<%@ Register TagPrefix="uc1" TagName="eMESDate" Src="../UserControl/DateTime/DateTime/eMESDate.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="cc2" Namespace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>FCheckGroupMP</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="<%=StyleSheet%>" rel="stylesheet">
    <script language="javascript">
     

        function ShowImportDialog() {
            var totalX = window.screen.width;
            var totalY = window.screen.height;
            var left = (totalX - 800) / 2;
            var top = (totalY - 600) / 2;

            if (left < 0) {
                left = 0;
            }
            if (top < 0) {
                top = 0;
            }

            window.open("FsoftwareVersionImport.aspx", "ImportWindow", "top=" + top + ",left=" + left + ",height=600,width=800,status=no,toolbar=no,menubar=no,location=no");
            //var result = window.showModalDialog("FsoftwareVersionImport.aspx", "" ,"dialogWidth:800px;dialogHeight:600px;scroll:none;help:no;status:no") ;
            return false;
        }
    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
    <table id="Table1" height="100%" width="100%" runat="server">
        <tr class="moduleTitle">
            <td>
                <asp:Label ID="lblTitle" runat="server" CssClass="labeltopic">软件版本维护</asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table class="query" height="100%" width="100%">
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblSoftWareVersionQuery" runat="server">软件版本</asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 159px">
                            <asp:TextBox ID="txtSoftWareVersionQuery" runat="server" Width="150px" DESIGNTIMEDRAGDROP="257"
                                CssClass="textbox"></asp:TextBox>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblSoftWareStatusQuery" runat="server">软件状态</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:DropDownList ID="drpSoftWareStatusQuery" runat="server" CssClass="textbox" Width="110px"
                                OnLoad="drpSoftWareStatus_Load">
                            </asp:DropDownList>
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
        <tr height="100%">
            <td class="fieldGrid">
                <ig:WebDataGrid ID="gridWebGrid" runat="server" Width="100%">
                </ig:WebDataGrid>
            </td>
        </tr>
        <tr class="normal">
            <td>
                <table height="100%" cellpadding="0" width="100%">
                    <tr>
                        <td class="smallImgButton">
                            <input class="gridExportButton" id="cmdGridExport" type="submit" value="  " name="cmdGridExport"
                                runat="server">
                        </td>
                        <td class="smallImgButton">
                            <input class="deleteButton" id="cmdDelete" type="submit" value="  " name="cmdDelete"
                                runat="server">
                        </td>
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
        <tr class="normal" id="trImport" runat="server">
            <td>
                <table class="edit">
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblInFile" runat="server"> 导入文件：</asp:Label>
                        </td>
                        <td style="width: 300px">
                            <input class="textbox" id="fileInit" style="width: 454px; height: 22px" type="file"
                                size="56" runat="server" enableviewstate="true">
                        </td>
                        <td class="toolBar">
                            <input class="submitImgButton" id="cmdEnter1" type="submit" value="导入" name="btnAdd1"
                                runat="server" onserverclick="cmdImport_ServerClick">
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblInTemplet" runat="server"> 导入模板：</asp:Label>
                        </td>
                        <td nowrap>
                            <a id="aFileDownLoad" style="display: none; color: blue" href="" target="_blank"
                                runat="server">
                                <asp:Label ID="lblDown" runat="server">下载</asp:Label></a> <span style="cursor: hand;
                                    color: blue; text-decoration: underline" onclick="DownLoadFile()">
                                    <asp:Label ID="lblDown1" runat="server">下载</asp:Label></span>
                        </td>
                        <td style="width: 100%">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="normal">
            <td>
                <table class="edit">
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblSoftWareVersionEdit" runat="server">软件版本</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtSoftWareVersionEdit" runat="server" CssClass="require" Width="130px"></asp:TextBox>
                        </td>
                        <td colspan="2">
                            <asp:RadioButtonList ID="rblSoftWareStatusEdit" runat="server" RepeatDirection="Horizontal"
                                AutoPostBack="false">
                            </asp:RadioButtonList>
                        </td>
                        <td style="width: 40%">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblEffectiveDateEdit" runat="server">生效日期</asp:Label>
                        </td>
                        <td class="fieldValue">                       
            <asp:TextBox type="text" id="dateEffectiveDateEdit"  class='datepicker' runat="server"  Width="130px"/>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblInvalidDateEdit" runat="server">失效日期</asp:Label>
                        </td>
                        <td class="fieldValue">
  <asp:TextBox type="text" id="dateInvalidDateEdit"  class='datepicker' runat="server"  Width="130px"/>
                        </td>
                        <td style="width: 40%">
                            &nbsp;
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
                            <input class="submitImgButton" id="cmdAdd" type="submit" value="新 增" name="cmdAdd"
                                runat="server">
                        </td>
                        <td class="toolBar">
                            <input class="submitImgButton" id="cmdSave" type="submit" value="保 存" name="cmdSave"
                                runat="server">
                        </td>
                        <td class="toolBar">
                            <input class="submitImgButton" id="cmdCancel" type="submit" value="取 消" name="cmdCancel"
                                runat="server">
                        </td>
                        <td class="toolBar">
                            <input class="submitImgButton" id="cmdEnter" type="button" runat="server" value="导 入"
                                name="btnAdd" onclick="return ShowImportDialog();" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
