<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FInvBusinessMP.aspx.cs"
    Inherits="BenQGuru.eMES.Web.WarehouseWeb.FInvBusinessMP" %>

<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="cc2" Namespace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>FInvBusinessMP</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="<%=StyleSheet%>" rel="stylesheet">
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
    <table height="100%" width="100%" runat="server">
        <tr class="moduleTitle">
            <td>
                <asp:Label ID="lblTitle" runat="server" CssClass="labeltopic">出入库业务类型维护</asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table class="query" height="100%" width="100%" runat="server">
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblBusinessCodeQuery" runat="server">业务类型代码</asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 159px">
                            <asp:TextBox ID="txtBusinessCodeQuery" runat="server" Width="150px" DESIGNTIMEDRAGDROP="257"
                                CssClass="textbox"></asp:TextBox>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblBusinessDescriptionQuery" runat="server">业务类型描述</asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 159px">
                            <asp:TextBox ID="txtBusinessDescriptionQuery" runat="server" Width="150px" DESIGNTIMEDRAGDROP="257"
                                CssClass="textbox"></asp:TextBox>
                        </td>
                        <td nowrap width="100%">
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblBusinessTypeQuery" runat="server">业务类型</asp:Label>
                        </td>
                        <td class="fieldValue" style="height: 26px">
                            <asp:DropDownList ID="drpBusinessTypeQuery" runat="server" CssClass="require" Width="150px"
                                DESIGNTIMEDRAGDROP="94" OnLoad="drpBusinessType_Load">
                            </asp:DropDownList>
                        </td>
                        <td colspan="2" nowrap width="100%">
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
        <tr class="normal">
            <td>
                <table class="edit" height="100%" cellpadding="0" width="100%">
                    <tr>
                        <td class="fieldNameLeft" style="height: 26px" nowrap>
                            <asp:Label ID="lblBusinessCodeEdit" runat="server">业务类型代码</asp:Label>
                        </td>
                        <td class="fieldValue" style="height: 26px">
                            <asp:TextBox ID="txtBusinessCodeEdit" runat="server" CssClass="require" Width="130px"></asp:TextBox>
                        </td>
                        <td class="fieldNameLeft" style="height: 26px" nowrap>
                            <asp:Label ID="lblBusinessDescriptionEdit" runat="server">业务类型描述</asp:Label>
                        </td>
                        <td class="fieldValue" style="height: 26px">
                            <asp:TextBox ID="txtBusinessDescriptionEdit" runat="server" CssClass="require" Width="130px"></asp:TextBox>
                        </td>
                        <td style="width: 100%">
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldNameLeft" style="height: 26px" nowrap>
                            <asp:Label ID="lblOrgEdit" runat="server">组织</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:DropDownList ID="DropDownListOrg" runat="server" CssClass="require" Width="130px">
                            </asp:DropDownList>
                        </td>
                        <td class="fieldName">
                            &nbsp;
                        </td>
                        <td class="fieldValue">
                            <asp:CheckBox runat="server" ID="chbIsFIFO" Text="FIFO" />
                        </td>
                        <td style="width: 100%">
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldNameLeft" style="height: 26px" nowrap>
                        </td>
                        <td class="fieldValue">
                            <asp:RadioButton ID="rdoTypeIn" runat="server" Text="入库" AutoPostBack="true" OnCheckedChanged="rdoTypeIn_CheckedChanged" />
                        </td>
                        <td class="fieldNameLeft" style="height: 26px" nowrap>
                            <asp:Label ID="lblBusinessReasonEdit" runat="server">入库类型</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:DropDownList ID="DropDownListBusinessReason" runat="server" CssClass="require"
                                Width="130px" OnLoad="DropDownListBusinessReason_Load">
                            </asp:DropDownList>
                        </td>
                        <td colspan="1" style="width: 100%">
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldNameLeft" style="height: 26px" nowrap>
                        </td>
                        <td class="fieldValue">
                            <asp:RadioButton ID="rdoTypeOut" runat="server" Text="出库" AutoPostBack="true" OnCheckedChanged="rdoTypeOut_CheckedChanged" />
                        </td>
                        <td colspan="3" style="width: 100%">
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
                            <input class="submitImgButton" id="cmdSave" type="submit" value="保存" name="cmdSave"
                                runat="server" />
                        </td>
                        <td>
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
