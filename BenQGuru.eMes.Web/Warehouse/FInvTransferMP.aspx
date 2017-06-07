<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FInvTransferMP.aspx.cs"
    Inherits="BenQGuru.eMES.Web.WarehouseWeb.FInvTransferMP" %>

<%@ Register TagPrefix="uc1" TagName="eMESDate" Src="~/UserControl/DateTime/DateTime/eMESDate.ascx" %>
<%@ Register TagPrefix="uc1" TagName="eMesTime" Src="../UserControl/DateTime/DateTime/eMesTime.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="cc2" Namespace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>InvTransferMP</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="<%=StyleSheet%>" rel="stylesheet">
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
    <table id="Table1" height="100%" width="100%" runat="server">
        <tr class="moduleTitle">
            <td>
                <asp:Label ID="lblTitle" runat="server" CssClass="labeltopic"> 发料、移转单</asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table class="query" width="100%" style="height: 100%">
                    <tr>
                        <td class="fieldNameLeft" style="height: 26px" nowrap>
                            <asp:Label ID="lblTransferNO" runat="server">单号</asp:Label>
                        </td>
                        <td class="fieldValue" style="height: 26px">
                            <cc2:SelectableTextBox ID="txtTransferNO" runat="server" CanKeyIn="true" Type="transferno1"
                                Target="">
                            </cc2:SelectableTextBox>
                        </td>
                        <td class="fieldName" style="height: 26px" nowrap>
                            <asp:Label ID="lblItemID" runat="server">物料代码</asp:Label>
                        </td>
                        <td class="fieldValue" style="height: 26px">
                            <cc2:SelectableTextBox ID="txtItemIDQuery" runat="server" CanKeyIn="true" Type="material"
                                Target="">
                            </cc2:SelectableTextBox>
                        </td>
                        <td class="fieldName" style="height: 26px" nowrap>
                            <asp:Label ID="lblRECType" runat="server">单据类型</asp:Label>
                        </td>
                        <td class="fieldValue" style="height: 26px">
                            <asp:DropDownList ID="DrpRECType" runat="server" CssClass="textbox" Width="130px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldName" style="height: 26px" nowrap>
                            <asp:Label ID="lblCreateDateFromQuery" runat="server">创建日期 从</asp:Label>
                        </td>
                        <td class="fieldValue" style="height: 26px">
                            <asp:TextBox  id="dateCreateDateFromQuery"  class='datepicker' runat="server"  Width="130px"/>
                        </td>
                        <td class="fieldName" style="height: 26px" nowrap>
                            <asp:Label ID="lblCreateDateToQuery" runat="server">到</asp:Label>
                        </td>
                        <td class="fieldValue" style="height: 26px">
                            <asp:TextBox  id="dateCreateDateToQuery"  class='datepicker' runat="server"  Width="130px"/>
                        </td>
                        <td class="fieldName" style="height: 26px" nowrap>
                            <asp:Label ID="lblTransferStatus" runat="server">单据状态</asp:Label>
                        </td>
                        <td class="fieldValue" style="height: 26px">
                            <asp:DropDownList ID="drpTransferStatus" runat="server" CssClass="textbox" Width="130px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldName" style="height: 26px" nowrap>
                            <asp:Label ID="lblFromStorageID" runat="server">源库别</asp:Label>
                        </td>
                        <td class="fieldValue" style="height: 26px">
                            <cc2:SelectableTextBox ID="txtFromStorageIDQuery" runat="server" CanKeyIn="true"
                                Type="singlestorage" Target="">
                            </cc2:SelectableTextBox>
                            <td class="fieldName" style="height: 26px" nowrap>
                                <asp:Label ID="lblToStorageID" runat="server">目的库别</asp:Label>
                            </td>
                            <td class="fieldValue" style="height: 26px">
                                <cc2:SelectableTextBox ID="txtToStorageIDQuery" runat="server" CanKeyIn="true" Type="singlestorage"
                                    Target="">
                                </cc2:SelectableTextBox>
                            </td>
                            <td nowrap>
                            </td>
                            <td nowrap>
                            </td>
                            <td nowrap>
                            </td>
                            <td class="fieldName">
                                <input class="submitImgButton" id="cmdQuery" type="submit" value="查 询" name="btnQuery"
                                    runat="server" onserverclick="cmdQuery_ServerClick">
                            </td>
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
                            <input class="gridExportButton" id="cmdGridExport" type="submit" value="  " name="cmdCancel"
                                runat="server" onserverclick="cmdGridExport_ServerClick" />
                        </td>
                        <td class="smallImgButton">
                            <input class="deleteButton" id="cmdDelete" type="submit" value="  " name="cmdDelete"
                                runat="server" onserverclick="cmdDelete_ServerClick" />
                        </td>
                        <td>
                            <cc1:pagersizeselector id="pagerSizeSelector" runat="server">
                            </cc1:pagersizeselector>
                        </td>
                        <td align="right">
                            <cc1:pagertoolbar id="pagerToolBar" runat="server" pagesize="20">
                            </cc1:pagertoolbar>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="normal">
            <td>
                <table class="edit" height="100%" width="100%">
                    <tr>
                        <td class="fieldNameLeft" style="height: 26px" nowrap>
                            <asp:Label ID="lblTransferNoEdit" runat="server">单号</asp:Label>
                        </td>
                        <td class="fieldValue" style="height: 26px">
                            <asp:TextBox ID="txtTransferNoEdit" runat="server" CssClass="require" Width="130px"></asp:TextBox>
                            <input class="submitImgButton" id="cmdProduct" type="submit" value="生成单号" name="cmdProduct"
                                runat="server" onserverclick="cmdProduct_ServerClick">
                        </td>
                        <td class="fieldName" style="height: 26px" nowrap>
                            <asp:Label ID="lblRECTypeEdit" runat="server">单据类型</asp:Label>
                        </td>
                        <td class="fieldValue" style="height: 26px">
                            <asp:DropDownList ID="DrpRECTypeEdit" runat="server" CssClass="textbox" Width="130px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldName" style="height: 26px" nowrap>
                            <asp:Label ID="lblFromStorageIDEdit" runat="server">源库别</asp:Label>
                        </td>
                        <td class="fieldValue" style="height: 26px">
                            <cc2:SelectableTextBox ID="txtFromStorageIDQueryEdit" runat="server" CanKeyIn="true"
                                CssClass="require" Type="singlestorage" Target="">
                            </cc2:SelectableTextBox>
                        </td>
                        <td class="fieldName" style="height: 26px" nowrap>
                            <asp:Label ID="lblToStorageIDEdit" runat="server">目的库别</asp:Label>
                        </td>
                        <td class="fieldValue" style="height: 26px">
                            <cc2:SelectableTextBox ID="txtToStorageIDQueryEdit" runat="server" CanKeyIn="true"
                                Type="singlestorage" Target="">
                            </cc2:SelectableTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldNameLeft" style="height: 26px" nowrap>
                            <asp:Label ID="lblRemark" runat="server">备注</asp:Label>
                        </td>
                        <td class="fieldValue" colspan="3" style="height: 26px">
                            <asp:TextBox ID="txtRemark" runat="server" CssClass="textbox" Width="800px"></asp:TextBox>
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
                        <td class="toolBar">
                            <input class="submitImgButton" id="cmdMerge" type="submit" value="合 并" name="cmdMerge"
                                runat="server" onserverclick="cmdMerge_ServerClick" />
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
