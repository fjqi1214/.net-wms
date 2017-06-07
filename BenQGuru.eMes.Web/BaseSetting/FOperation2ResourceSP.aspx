<%@ Page Language="c#" CodeBehind="FOperation2ResourceSP.aspx.cs" AutoEventWireup="True"
    Inherits="BenQGuru.eMES.Web.BaseSetting.FOperation2ResourceSP" %>

<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>FShiftMP</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="<%=StyleSheet%>" rel="stylesheet">
    <script language="javascript">
        function RedictURL() {
            var opcode = document.getElementById("txtOperationCodeQuery").value;
            var arg = new Object();
            arg.Others = opcode;
            var result=window.showModalDialog("FOperation2ResourceAP.aspx?opcode=" + escape(opcode), arg, "dialogWidth:800px;dialogHeight:600px;scroll:none;help:no;status:no");

            if (result) {
                document.getElementById("btnRefesh").click();
            }
        }
    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
    <table id="Table1" height="100%" width="100%" runat="server">
        <tr class="moduleTitle">
            <td>
                <asp:Label ID="lblTitle" runat="server" CssClass="labeltopic"> 资源列表</asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table class="query" height="100%" width="100%">
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblItemOperationCodeQuery" runat="server"> 工序代码</asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 159px">
                            <asp:TextBox ID="txtOperationCodeQuery" runat="server" Width="150px" ReadOnly="True"
                                CssClass="textbox"></asp:TextBox>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblOPDescriptionQuery" runat="server"> 工序描述</asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 159px">
                            <asp:TextBox ID="txtOPDescriptionQuery" runat="server" Width="150px" ReadOnly="True"
                                CssClass="textbox"></asp:TextBox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblResourceCodeQuery" runat="server" Visible="False"> 资源代码</asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 159px">
                            <asp:TextBox ID="txtResourceCodeQuery" runat="server" Width="150px" CssClass="textbox"
                                Visible="False"></asp:TextBox>
                        </td>
                        <td class="fieldName" style="display: none">
                            <asp:Button ID="btnRefesh" runat="server" OnClick="btnRefesh_Click" />
                        </td>
                        <td nowrap width="100%">
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
        <tr style="display: none" class="normal">
            <td>
                <table class="edit" height="100%" cellpadding="0" width="100%">
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblResourceCodeEdit" runat="server">资源代码</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtResourceCodeEdit" CssClass="require" runat="server" Width="150px"></asp:TextBox>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblResourceSequenceEdit" runat="server">资源顺序</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtResourceSequenceEdit" CssClass="require" runat="server" Width="150px"></asp:TextBox>
                        </td>
                        <td nowrap width="100%">
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
                            <input class="submitImgButton" id="cmdAdd" type="submit" value="新 增" name="cmdSelect"
                                runat="server" onclick="RedictURL()">
                        </td>
                        <td class="toolBar">
                            <input class="submitImgButton" id="cmdReturn" type="submit" value="返 回" name="cmdReturn"
                                runat="server" onserverclick="cmdReturn_ServerClick">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
