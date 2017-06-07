<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FFirstCheckByMOMP.aspx.cs"
    Inherits="BenQGuru.eMES.Web.MOModel.FFirstCheckByMOMP" %>

<%@ Register TagPrefix="uc1" TagName="eMESDate" Src="~/UserControl/DateTime/DateTime/eMESDate.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="cc2" Namespace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>FirstCheckByMO</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR" />
    <meta content="C#" name="CODE_LANGUAGE" />
    <meta content="JavaScript" name="vs_defaultClientScript" />
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
    <link href="<%=StyleSheet%>" rel="stylesheet" />
</head>
<body>
    <form id="Form1" method="post" runat="server">
    <table height="100%" width="100%" runat="server">
        <tr class="moduleTitle">
            <td>
                <asp:Label ID="lblTitle" runat="server" CssClass="labeltopic">工单首检结果维护</asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table class="query" style="height: 100%; width: 100%">
                    <tr>
                        <td class="fieldName" nowrap="noWrap">
                            <asp:Label ID="lblMOCodeQuery" runat="server">工单代码</asp:Label>
                        </td>
                        <td class="fieldValue" nowrap="noWrap">
                            <cc2:SelectableTextBox ID="txtMOCodeQuery" runat="server" CanKeyIn="true" Type="MOCode"
                                Target="">
                            </cc2:SelectableTextBox>
                        </td>
                        <td class="fieldName" nowrap="noWrap" style="height: 26px">
                            <asp:Label ID="lblFirstCheckDateQuery" runat="server">首检日期</asp:Label>
                        </td>
                        <td class="fieldValue" nowrap="noWrap" style="height: 26px">
                            <asp:TextBox type="text" ID="firstCheckDateQuery" class='datepicker' runat="server"
                                Width="130px" />
                        </td>
                        <td style="width: 100%">
                        </td>
                        <td class="fieldName">
                            <input class="submitImgButton" id="cmdQuery" type="submit" value="查 询" name="btnQuery"
                                runat="server" onserverclick="cmdQuery_ServerClick" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr style="height: 100%">
            <td class="fieldGrid">
                <ig:WebDataGrid ID="gridWebGrid" runat="server" Width="100%">
                </ig:WebDataGrid>
            </td>
        </tr>
        <tr class="normal">
            <td>
                <table cellpadding="0" style="height: 100%; width: 100%">
                    <tr>
                        <td class="smallImgButton">
                            <input class="gridExportButton" id="cmdGridExport" type="submit" value="  " name="cmdCancel"
                                runat="server" onserverclick="cmdGridExport_ServerClick" />
                        </td>
                        <td class="smallImgButton">
                            <input class="deleteButton" id="cmdDelete" type="submit" value="  " name="cmdDelete"
                                runat="server" onserverclick="cmdDelete_ServerClick" />
                        </td>
                        <td>
                            <cc1:PagerSizeSelector id="pagerSizeSelector" runat="server">
                            </cc1:PagerSizeSelector>
                        </td>
                        <td align="right">
                            <cc1:PagerToolBar id="pagerToolBar" runat="server" PageSize="20">
                            </cc1:PagerToolBar>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="normal">
            <td style="height: 96px">
                <table class="edit" cellpadding="0" style="height: 100%; width: 100%">
                    <tr>
                        <td class="fieldName" nowrap="noWrap" style="height: 26px">
                            <asp:Label ID="lblMOCodeEdit" runat="server">工单代码</asp:Label>
                        </td>
                        <td class="fieldValue" nowrap="noWrap" style="height: 26px">
                            <cc2:SelectableTextBox ID="txtMOCodeEdit" runat="server" CanKeyIn="true" Type="singleMOCode"
                                Target="">
                            </cc2:SelectableTextBox>
                        </td>
                        <td class="fieldName" nowrap="noWrap" style="height: 26px">
                            <asp:Label ID="lblFirstCheckDateEdit" runat="server">首检日期</asp:Label>
                        </td>
                        <td class="fieldValue" nowrap="noWrap" style="height: 26px">
                            <asp:TextBox type="text" ID="firstCheckDateEdit" class='datepicker' runat="server"
                                Width="130px" />
                        </td>
                        <td style="width: 100%">
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldName" style="height: 50px" valign="middle">
                            <asp:Label ID="lblFirstCheckResultEdit" runat="server">首检结果</asp:Label>
                        </td>
                        <td style="height: 50px" valign="middle">
                            <asp:RadioButtonList ID="rblCheckResult" runat="server" RepeatDirection="Horizontal">
                            </asp:RadioButtonList>
                        </td>
                        <td class="fieldName" style="height: 50px">
                            <asp:Label ID="lblRemark" runat="server">备注</asp:Label>
                        </td>
                        <td class="fieldValue" style="height: 80px">
                            <asp:TextBox ID="txtRemarkEdit" runat="server" Width="220px" CssClass="textbox" Height="60px"
                                MaxLength="1000" TextMode="MultiLine"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td style="width: 100%">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="toolBar">
            <td style="height: 15px">
                <table class="toolBar">
                    <tr>
                        <td class="toolBar">
                            <input class="submitImgButton" id="cmdAdd" type="submit" value="新 增" name="cmdAdd"
                                runat="server" onserverclick="cmdAdd_ServerClick" />
                        </td>
                        <td class="toolBar">
                            <input class="submitImgButton" id="cmdSave" type="submit" value="保 存" name="cmdSave"
                                runat="server" onserverclick="cmdSave_ServerClick" />
                        </td>
                        <td class="toolBar">
                            <input class="submitImgButton" id="cmdCancel" type="submit" value="取 消" name="cmdCancel"
                                runat="server" onserverclick="cmdCancel_ServerClick" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
