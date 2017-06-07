<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>

<%@ Page Language="c#" CodeBehind="FDCTResourceAP.aspx.cs" AutoEventWireup="True"
    Inherits="BenQGuru.eMES.Web.BaseSetting.FDCTResourceAP" %>

<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html>
<head>
    <title></title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <base target="_self" />
</head>
<link href="<%=StyleSheet%>" rel="stylesheet">

<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
    <table width="100%" runat="server" id="tableMain">
        <tr class="moduleTitle">
            <td>
                <asp:Label ID="lblTitle" runat="server" CssClass="labeltopic"> 选择资源</asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table class="query" width="100%">
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblResourceCodeQuery" runat="server"> 资源代码</asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 159px">
                            <asp:TextBox ID="txtResourceCodeQuery" runat="server" Width="150px" CssClass="textbox"></asp:TextBox>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblDctCommandQuery" runat="server" Visible="False">  DCT指令</asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 159px">
                            <asp:TextBox ID="txtDctCommandQuery" runat="server" Width="150px" ReadOnly="True"
                                CssClass="textbox" Visible="False"></asp:TextBox>
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
        <tr>
            <td class="fieldGrid" style="padding: 0px;">
                <ig:WebDataGrid ID="gridWebGrid" runat="server" Width="100%">
                </ig:WebDataGrid>
            </td>
        </tr>
        <tr class="normal">
            <td>
                <table cellpadding="0" width="100%">
                    <tr>
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
                            <input class="submitImgButton" id="cmdSave" type="submit" value="保存" name="cmdSelect"
                                runat="server" >
                        </td>
                        <td class="toolBar">
                            <input class="submitImgButton" id="cmdCancel" type="submit" value="取消" name="cmdLeave"
                                runat="server" >
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
