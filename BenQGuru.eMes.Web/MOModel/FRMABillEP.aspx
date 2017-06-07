<%@ Register TagPrefix="ss1" Namespace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="uc1" TagName="eMESDate" Src="~/UserControl/DateTime/DateTime/eMESDate.ascx" %>
<%@ Register TagPrefix="cc2" Namespace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>

<%@ Page Language="c#" CodeBehind="FRMABillEP.aspx.cs" AutoEventWireup="True" Inherits="BenQGuru.eMES.Web.MOModel.FRMABillEP" %>

<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>FRMABillEP</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="<%=StyleSheet%>" rel="stylesheet">
</head>
<body ms_positioning="GridLayout">
    <form id="Form2" method="post" runat="server">
    <table id="Table1" height="100%" width="100%" runat="server">
        <tr class="moduleTitle">
            <td>
                <asp:Label ID="lblTitle" runat="server" CssClass="labeltopic">RMA单据详细信息</asp:Label>
            </td>
        </tr>
        <tr>
            <td class="fieldGrid">
                <table class="query" height="100%" width="100%">
                    <tr>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblModelCodeQuery" runat="server">产品别代码</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <cc2:selectabletextbox id="txtModelQuery" runat="server" type="model" readonly="false"
                                cankeyin="true" width="130px">
                            </cc2:selectabletextbox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblItemCodeQuery" runat="server">产品代码</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <cc2:selectabletextbox id="txtItemCodeQuery" runat="server" type="item" readonly="false"
                                cankeyin="true" width="130px">
                            </cc2:selectabletextbox>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblRCardQuery" runat="server">产品序列号</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtRCardQuery" runat="server" ReadOnly="false" Width="130px">
                            </asp:TextBox>
                        </td>
                        <td class="fieldName" nowrap>
                        </td>
                        <td class="fieldValue">
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblCusCodeQuery" runat="server">客户代码</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtCusCodeQuery" runat="server" CssClass="textbox" Width="130px"
                                MaxLength="40"></asp:TextBox>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblErrorCodeQuery" runat="server">不良代码</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <cc2:selectabletextbox id="txtErrorCodeQuery" runat="server" type="errorcodea" readonly="false"
                                cankeyin="true" width="130px">
                            </cc2:selectabletextbox>
                        </td>
                        <td class="fieldName" nowrap>
                        </td>
                        <td class="fieldValue">
                        </td>
                        <td class="fieldName" nowrap>
                        </td>
                        <td class="fieldValue">
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblHandelCodeQuery" runat="server">RMA处理方式</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:DropDownList ID="drpHandelCodeQuery" runat="server" CssClass="textbox" Width="130px">
                            </asp:DropDownList>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblSubCompanyQuery" runat="server"> 分公司</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtSubCompanyQuery" runat="server" CssClass="textbox" Width="130px"
                                MaxLength="40"></asp:TextBox>
                        </td>
                        <td class="fieldName" nowrap>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtRMABillCode" Visible="false" runat="server" CssClass="textbox"
                                Width="130px" MaxLength="40"></asp:TextBox>
                        </td>
                        <td class="fieldName" nowrap>
                        </td>
                        <td style="padding-right: 8px" align="right">
                            <input class="submitImgButton" id="cmdQuery" type="submit" value="查 询" name="btnQuery"
                                runat="server">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="fieldGrid">
                <ig:WebDataGrid ID="gridWebGrid" runat="server" Width="100%">
                </ig:WebDataGrid>
            </td>
        </tr>
        <tr class="normal">
            <td>
                <table height="100%" cellpadding="0" width="100%">
                    <tr>
                        <td class="smallImgButton" nowrap>
                            <input class="gridExportButton" id="cmdGridExport" type="submit" value="  " name="cmdCancel"
                                runat="server">
                        </td>
                        <td class="smallImgButton" nowrap>
                            <input class="deleteButton" id="cmdDelete" type="submit" value="  " name="cmdDelete"
                                runat="server">
                        </td>
                        <td>
                            <cc1:pagersizeselector id="pagerSizeSelector" runat="server" PageSize="50">
                            </cc1:pagersizeselector>
                        </td>
                        <td align="right">
                            <cc1:pagertoolbar id="pagerToolBar" runat="server" PageSize="20">
                            </cc1:pagertoolbar>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table class="edit" id="edittable" cellspacing="1" cellpadding="1" border="0" runat="server">
                    <tr>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblModelCodeEdit" runat="server">产品别代码</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <cc2:selectabletextbox id="txtModelCodeEdit" runat="server" type="singlemodel" readonly="false"
                                CssClass="require" cankeyin="false" width="130px">
                            </cc2:selectabletextbox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblItemCodeEdit" runat="server">产品代码</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <cc2:selectabletextbox id="txtItemCodeEdit" runat="server" type="singleitem" readonly="false"
                                CssClass="require" cankeyin="false" width="130px">
                            </cc2:selectabletextbox>
                        </td>
                        <td class="fieldLeft" nowrap>
                            <asp:Label ID="lblRCardEdit" runat="server">产品序列号</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtRCardEdit" runat="server" Width="130px" CssClass="require">
                            </asp:TextBox>
                        </td>
                        <td width="100%">
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblServerCodeEdit" runat="server">服务单号</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtServerCodeEdit" runat="server" CssClass="require" Width="130px">
                            </asp:TextBox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblCustomCodeEdit" runat="server">客户代码</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <cc2:selectabletextbox id="txtCustomCodeEdit" runat="server" type="singlecustomer"
                                readonly="false" cankeyin="false" width="130px">
                            </cc2:selectabletextbox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblComfromEdit" runat="server">来源</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtComfromEdit" runat="server" ReadOnly="false" Width="130px">
                            </asp:TextBox>
                        </td>
                        <td width="100%">
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblHandelCodeEdit" runat="server">RMA处理方式</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:DropDownList ID="drpHandelCodeEdit" runat="server" CssClass="require" Width="130px">
                            </asp:DropDownList>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblMaintenanceEdit" runat="server"> 保修期</asp:Label>
                        </td>
                        <td class="fieldValue" nowrap>
                            <asp:TextBox ID="txtMaintenanceEdit" runat="server" CssClass="require" Width="130px"
                                MaxLength="200"></asp:TextBox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblSubCompanyEdit" runat="server"> 分公司</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtSubCompanyEdit" runat="server" CssClass="textbox" Width="130px"
                                MaxLength="40"></asp:TextBox>
                        </td>
                        <td width="100%">
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblReMOEdit" runat="server">返工工单</asp:Label>
                        </td>
                        <td class="fieldValue" nowrap>
                            <cc2:selectabletextbox id="txtReMOEdit" runat="server" type="singlermareworkmo" readonly="false"
                                cankeyin="false" width="130px">
                            </cc2:selectabletextbox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblWHReceiveDateEdit" runat="server">仓库收货日期</asp:Label>
                        </td>
                        <td class="fieldValue" nowrap>
                            <asp:TextBox type="text" ID="WHReceiveDateEdit" class='datepicker' runat="server"
                                Width="130px" />
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblErrorCodeEdit" runat="server">不良代码</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <cc2:selectabletextbox id="txtErrorCodeEdit" runat="server" type="singleerror" readonly="false"
                                cankeyin="false" width="130px">
                            </cc2:selectabletextbox>
                        </td>
                        <td width="100%">
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblCompIssueEdit" runat="server">客户投诉现象</asp:Label>
                        </td>
                        <td class="fieldValue" nowrap>
                            <asp:TextBox ID="txtCompIssueEdit" TextMode="MultiLine" Height="50px" runat="server"
                                CssClass="textbox" Width="130px" MaxLength="40"></asp:TextBox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblMemoEdit" runat="server">客户投诉现象</asp:Label>
                        </td>
                        <td class="fieldValue" nowrap>
                            <asp:TextBox ID="txtMemoEdit" TextMode="MultiLine" Height="50px" runat="server" CssClass="textbox"
                                Width="130px" MaxLength="40"></asp:TextBox>
                        </td>
                        <td class="fieldName" nowrap>
                        </td>
                        <td class="fieldValue" nowrap>
                            <asp:CheckBox ID="cbxIsInShelfLifeEdit" Text="是否在保内" runat="server" />
                        </td>
                        <td width="100%">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="toolBar">
            <td>
                <table class="toolBar" id="Table4">
                    <tr>
                        <td class="toolBar">
                            <input class="submitImgButton" id="cmdAdd" type="submit" value="新 增" name="cmdAdd"
                                runat="server">
                        </td>
                        <td class="toolBar">
                            <input language="javascript" class="submitImgButton" id="cmdSave" type="submit" value="保 存"
                                name="cmdAdd" runat="server">
                        </td>
                        <td class="toolBar">
                            <input class="submitImgButton" id="cmdCancel" type="submit" value="取 消" name="cmdCancel"
                                runat="server">
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
