<%@ Page Language="c#" CodeBehind="FOracleJobMP.aspx.cs" AutoEventWireup="True" Inherits="BenQGuru.eMES.Web.BaseSetting.FOracleJobMP" %>

<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="cc2" TagName="eMESDate" Src="~/UserControl/DateTime/DateTime/eMESDate.ascx" %>
<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Oracle Job管理</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR" />
    <meta content="C#" name="CODE_LANGUAGE" />
    <meta content="JavaScript" name="vs_defaultClientScript" />
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
    <link href="<%=StyleSheet%>" rel="stylesheet" />
</head>
<body>
    <form id="Form1" method="post" runat="server">
    <table style="height: 100%; width: 100%;" runat="server">
        <tr class="moduleTitle">
            <td>
                <asp:Label ID="lblTitle" runat="server" CssClass="labeltopic">Oracle Job管理</asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table class="query" style="height: 100%; width: 100%; display: none;">
                    <tr>
                        <td style="width: 100%;">
                        </td>
                        <td class="fieldName">
                            <input class="submitImgButton" id="cmdQuery" type="submit" value="查 询" name="btnQuery"
                                runat="server" onserverclick="cmdQuery_ServerClick" visible="false" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr style="height: 100%;">
            <td class="fieldGrid">
                <ig:webdatagrid id="gridWebGrid" runat="server" width="100%">
                </ig:webdatagrid>
            </td>
        </tr>
        <tr class="normal">
            <td>
                <table cellpadding="0" style="height: 100%; width: 100%;">
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
                            <cc1:PagerSizeSelector ID="pagerSizeSelector" runat="server"></cc1:PagerSizeSelector>
                        </td>
                        <td align="right">
                            <cc1:PagerToolBar ID="pagerToolBar" runat="server" PageSize="20"></cc1:PagerToolBar>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="normal">
            <td>
                <table class="edit" cellpadding="0" style="height: 100%; width: 100%;">
                    <tr>
                        <td class="fieldNameLeft" nowrap="nowrap">
                            <asp:Label ID="lblJobNameEdit" runat="server">JobName</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtJobNameEdit" runat="server" CssClass="require" Width="150px"></asp:TextBox>
                        </td>
                        <td class="fieldNameLeft" nowrap="nowrap">
                            <asp:Label ID="lblJobTypeEdit" runat="server">JobType</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:DropDownList ID="ddlJobTypeEdit" runat="server" CssClass="require" Width="150px">
                            </asp:DropDownList>
                        </td>
                        <td class="fieldNameLeft" style="height: 26px" nowrap="nowrap">
                            <asp:Label ID="lblCommentsEdit" runat="server">Comments</asp:Label>
                        </td>
                        <td class="fieldValue" style="height: 26px">
                            <asp:TextBox ID="txtCommentsEdit" runat="server" CssClass="textbox" Width="150px"></asp:TextBox>
                        </td>
                        <td style="width: 100%;">
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldNameLeft" style="height: 26px" nowrap="nowrap">
                            <asp:Label ID="lblJobActionEdit" runat="server">JobAction</asp:Label>
                        </td>
                        <td class="fieldValue" colspan="3">
                            <asp:TextBox ID="txtJobActionEdit" runat="server" CssClass="require" Width="500px"></asp:TextBox>
                        </td>
                        <td colspan="3" style="width: 100%;">
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldNameLeft" nowrap="nowrap">
                            <asp:Label ID="lblRepeatIntervalEdit" runat="server">RepeatInterval</asp:Label>
                        </td>
                        <td class="fieldValue" colspan="3">
                            <asp:TextBox ID="txtRepeatIntervalEdit" runat="server" CssClass="require" Width="500px"></asp:TextBox>
                        </td>
                        <td colspan="3" style="width: 100%;">
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
                                runat="server" onserverclick="cmdAdd_ServerClick" />
                        </td>
                        <td class="toolBar">
                            <input class="submitImgButton" id="cmdSave" type="submit" value="保 存" name="cmdSave"
                                runat="server" onserverclick="cmdSave_ServerClick" />
                        </td>
                        <td>
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
