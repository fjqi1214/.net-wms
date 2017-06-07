<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FPickDoneMP.aspx.cs" Inherits="BenQGuru.eMES.Web.WarehouseWeb.FPickDoneMP" %>

<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="cc2" Namespace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>FStackMP</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="<%=StyleSheet%>" rel="stylesheet">
    <style type="text/css">
        .require
        {
        }
    </style>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
    <table height="100%" width="100%" runat="server">
        <tr class="moduleTitle">
            <td>
                <asp:Label ID="lblPickDoneTitle" runat="server" CssClass="labeltopic">拣料作业</asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table class="query" height="100%" width="100%">
                    <tr>
                        <td class="fieldNameLeft" style="height: 26px" nowrap>
                            <asp:Label ID="lblPickNoQuery" runat="server">拣货任务令</asp:Label>
                        </td>
                        <td class="fieldValue" style="height: 26px">
                            <asp:TextBox ID="txtPickNoQuery" runat="server" CssClass="require" Width="130px"></asp:TextBox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblInvNoQuery" runat="server">SAP单据号</asp:Label>
                        </td>
                        <td class="fieldValue" nowrap>
                            <asp:TextBox ID="txtInvNoQuery" runat="server" CssClass="textbox" Width="130px"></asp:TextBox>
                        </td>
                        <td style="width: 100%" nowrap>
                        </td>
                        <td>
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
                        <td class="smallImgButton" style="display: none">
                            <input class="deleteButton" id="cmdDelete" type="submit" value="  " name="cmdDelete"
                                runat="server">
                        </td>
                        <td>
                            <cc1:PagerSizeSelector ID="pagerSizeSelector" runat="server">
                            </cc1:PagerSizeSelector>
                        </td>
                        <td align="right">
                            <cc1:PagerToolBar ID="pagerToolBar" runat="server">
                            </cc1:PagerToolBar>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table class="query" height="100%" width="100%">
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblPickDoneItem" runat="server" Font-Bold="true" Font-Size="Small">拣货作业</asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td style="width: 150px">
                                        <asp:RadioButtonList ID="rdoSelectType" runat="server" CssClass="require" Width="200px"
                                            RepeatDirection="Horizontal">
                                            <%--        <asp:ListItem Selected  Text="整箱" Value="AllCarton"></asp:ListItem>
                                <asp:ListItem Text="拆箱" Value="SplitCarton"></asp:ListItem>--%>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkINOUTCHECK" runat="server" Text="忽略先进先出检查" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td class="fieldNameLeft" style="height: 26px" nowrap>
                                        <asp:Label ID="lblCartonNoEdite" runat="server">箱号</asp:Label>
                                    </td>
                                    <td class="fieldValue" style="height: 26px">
                                        <asp:TextBox ID="txtCartonNoEdite" runat="server" CssClass="require" Width="130px"></asp:TextBox>
                                    </td>
                                    <td class="fieldNameLeft" style="height: 26px" nowrap>
                                        <asp:Label ID="lblNumberEdite" runat="server">数量</asp:Label>
                                    </td>
                                    <td class="fieldValue" style="height: 26px">
                                        <asp:TextBox ID="txtNumberEdite" runat="server" CssClass="require" Width="130px"></asp:TextBox>
                                    </td>
                                    <td class="fieldNameLeft" style="height: 26px" nowrap>
                                        <asp:Label ID="lblSNEdite" runat="server">SN</asp:Label>
                                    </td>
                                    <td class="fieldValue" style="height: 26px">
                                        <asp:TextBox ID="txtSNEdite" runat="server" CssClass="require" Width="130px"></asp:TextBox>
                                    </td>
                                    <td style="width: 100%" nowrap>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr class="toolBar">
                        <td>
                            <table class="toolBar">
                                <tr>
                                    <td>
                                        <input class="submitImgButton" id="cmdCommit" type="submit" value="提 交" name="cmdCommit"
                                            runat="server" onserverclick="cmdAdd_Click">
                                    </td>
                                    
                                    <td>
                                        <input class="submitImgButton" id="cmdInOut" type="submit" value="先进先出" name="cmdInOut"
                                            runat="server" onserverclick="cmdInOut_Click" onclick="GetSelectRowGUIDS('gridWebGrid')">
                                    </td>
                                    <td>
                                        <input class="submitImgButton" id="cmdClosePick" type="submit" value="拣料完成" name="cmdCommit"
                                            runat="server" onserverclick="cmdClosePick_Click">
                                    </td>
                                    <td>
                                        <input class="submitImgButton" id="cmdReturn" type="submit" value="返回" name="cmdReturn"
                                            runat="server" onserverclick="cmdReturn_ServerClick" />
                                    </td>
                                   <td>
                                    &nbsp;
                                    </td>
                                     <td>
                                    &nbsp;
                                    </td>
                                     <td>
                                    &nbsp;
                                    </td>
                                     <td>
                                    &nbsp;
                                    </td>
                                     <td>
                                    &nbsp;
                                    </td>
                                     <td>
                                    &nbsp;
                                    </td>
                                      <td>
                                    &nbsp;
                                    </td>
                                      <td>
                                    &nbsp;
                                    </td>
                                    <td>
                                        <input class="submitImgButton" id="cmdApply" type="submit" value="申请欠料" name="cmdApply"
                                            runat="server" onserverclick="cmdSave_Click">
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
