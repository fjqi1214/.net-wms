<%@ Register TagPrefix="cc2" Namespace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<%@ Register TagPrefix="uc1" TagName="eMesDate" Src="../UserControl/DateTime/DateTime/eMesDate.ascx" %>
<%@ Register TagPrefix="uc1" TagName="eMesTime" Src="../UserControl/DateTime/DateTime/eMesTime.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>

<%@ Page Language="c#" CodeBehind="FSMTFeederLogQP.aspx.cs" AutoEventWireup="True"
    Inherits="BenQGuru.eMES.Web.SMT.FSMTFeederLogQP" %>

<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>FFeederMP</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="<%=StyleSheet%>" rel="stylesheet">
</head>
<body ms_positioning="GridLayout" scroll="yes">
    <form id="Form1" method="post" runat="server">
    <table height="100%" width="100%" runat="server">
        <tr class="moduleTitle">
            <td>
                <asp:Label ID="lblTitle" runat="server" CssClass="labeltopic">Feeder变更查询</asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table class="query" height="100%" width="100%">
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblItemCodeQuery" runat="server">产品代码</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <cc2:selectabletextbox id="txtConditionItem" runat="server" Type="item" Width="90px">
                            </cc2:selectabletextbox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblMOIDQuery" runat="server">工单代码</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <cc2:selectabletextbox id="txtConditionMo" runat="server" Type="mo" Width="90px">
                            </cc2:selectabletextbox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblSSQuery" runat="server">产线代码</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtSSCode" runat="server" CssClass="textbox" Width="130"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblFeederSpecCode" runat="server">Feeder规格</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtFeederSpecCode" runat="server" CssClass="textbox" Width="130"></asp:TextBox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblFeederCodeQuery" runat="server">Feeder代码</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtFeederCode" CssClass="textbox" runat="server"></asp:TextBox>
                        </td>
                        <td colspan="3">
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
                        <td>
                            <cc1:pagersizeselector id="pagerSizeSelector" runat="server">
                            </cc1:pagersizeselector>
                        </td>
                        <td align="right">
                            <cc1:PagerToolbar id="pagerToolBar" runat="server">
                            </cc1:PagerToolbar>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="toolBar">
            <td>
                <table class="toolBar">
                    <tr>
                        <td class="toolBar" style="display: none">
                            <input class="submitImgButton" id="cmdAdd" type="submit" value="新 增" name="cmdAdd"
                                runat="server">
                        </td>
                        <td class="toolBar" style="display: none">
                            <input class="submitImgButton" id="cmdSave" type="submit" value="保 存" name="cmdSave"
                                runat="server">
                        </td>
                        <td style="display: none">
                            <input class="submitImgButton" id="cmdCancel" type="submit" value="取 消" name="cmdCancel"
                                runat="server">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
