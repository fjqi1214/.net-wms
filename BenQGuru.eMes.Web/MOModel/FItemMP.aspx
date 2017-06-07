<%@ Page Language="c#" CodeBehind="FItemMP.aspx.cs" AutoEventWireup="True" Inherits="BenQGuru.eMES.Web.MOModel.FItemMP" %>

<%@ Register TagPrefix="uc1" TagName="eMESDate" Src="~/UserControl/DateTime/DateTime/eMESDate.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="cc2" Namespace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>ItemMP</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="<%=StyleSheet%>" rel="stylesheet">
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
    <table height="100%" width="100%" runat="server">
        <tr class="moduleTitle">
            <td>
                <asp:Label ID="lblTitle" runat="server" CssClass="labeltopic"> 产品主档维护</asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table class="query" height="100%" width="100%">
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblItemCodeQuery" runat="server"> 产品代码</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtItemCodeQuery" runat="server" CssClass="textbox" Width="130px"></asp:TextBox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblItemNameQuery" runat="server"> 产品名称</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtItemNameQuery" runat="server" CssClass="textbox" Width="130px"></asp:TextBox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblItemTypeQuery" runat="server"> 产品类别</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:DropDownList ID="drpItemTypeQuery" runat="server" Width="130px" OnLoad="drpItemTypeQuery_Load">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblItemModelCodeQuery" runat="server"> 所属产品别</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtModelCodeQuery" runat="server" CssClass="textbox" Width="130px"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td class="fieldName">
                            <input class="submitImgButton" id="cmdQuery" type="submit" value="查 询" name="btnQuery"
                                runat="server" onserverclick="cmdQuery_ServerClick" bindclick="true" />
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
                                runat="server" onserverclick="cmdGridExport_ServerClick"  bindclick="true">
                        </td>
                        <td class="smallImgButton">
                            <input class="deleteButton" id="cmdDelete" type="submit" value="  " name="cmdDelete"
                                runat="server" onserverclick="cmdDelete_ServerClick"  bindclick="true">
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
                <table class="edit" height="100%" cellpadding="0" width="100%">
                    <tr>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblItemCodeEdit" runat="server">产品代码</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtItemCodeEdit" runat="server" CssClass="require" Width="130px"></asp:TextBox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblItemNameEdit" runat="server">产品名称</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtItemNameEdit" runat="server" CssClass="textbox" Width="130px"></asp:TextBox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblItemTypeEdit" runat="server">产品类别</asp:Label>
                        </td>
                        <td class="fieldValue" nowrap>
                            <asp:DropDownList ID="drpItemTypeEdit" runat="server" CssClass="require" Width="130px"
                                OnLoad="drpItemTypeEdit_Load">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblItemUOMEdit" runat="server">计量单位</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtItemUOMEdit" runat="server" CssClass="require" Width="130px"></asp:TextBox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblItemDescEdit" runat="server">产品描述</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtItemDescEdit" runat="server" CssClass="textbox" Width="130px"></asp:TextBox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblItemModelCodeEdit" runat="server">所属产品别</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:DropDownList ID="drpModelEdit" runat="server" CssClass="require" Width="130px"
                                OnLoad="drpModelEdit_Load">
                            </asp:DropDownList>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblCheckOPCodeEdit" runat="server" Visible="false">产生送检批工序</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <cc2:selectabletextbox id="txtOPCodeEdit" runat="server" CanKeyIn="true" Type="singleopwithoutroute"
                                Target="" Visible="false">
                            </cc2:selectabletextbox>
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblItemCartonQty" runat="server">Carton数量</asp:Label>
                        </td>
                        <td class="fieldValue" style="height: 28px">
                            <asp:TextBox ID="txtCartonQty" runat="server" CssClass="require" Width="130px" MaxLength="10"></asp:TextBox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblOrgEdit" runat="server">组织</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:DropDownList ID="DropDownListOrg" runat="server" CssClass="textbox" Width="130px">
                            </asp:DropDownList>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblPcbAcountEdit" runat="server" Visible="True">PCBA拼板数</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtPcbAcountEdit" runat="server" CssClass="require" Width="130px"
                                Visible="True"></asp:TextBox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:CheckBox ID="CheckboxNeedCheckComApp" runat="server"  Text=""></asp:CheckBox>
                        </td>
                        <td>
                            <asp:Label ID="lblNeedCheckComApp" runat="server" Visible="True">比对附件</asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblLotSizeEdit" runat="server">批量</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtLotSizeEdit" runat="server" CssClass="require" Width="130px"
                                MaxLength="10"></asp:TextBox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblItemProductCodeEdit" runat="server" Visible="True">商品码</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="TextboxItemProductCodeEdit" runat="server" CssClass="require" Width="130px"
                                Visible="True"></asp:TextBox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblBurnUseMinutesEdit" runat="server">老化预计耗时</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtBurnUseMinutesEdit" runat="server" CssClass="require" Width="130px"></asp:TextBox>
                            <asp:Label ID="lblMinute" runat="server">分钟</asp:Label>
                        </td>
                        <td class="fieldName" nowrap style="width: 70px">
                            <asp:CheckBox ID="CheckboxNeedCheckCartonEdit" runat="server" Text="">
                            </asp:CheckBox>
                        </td>
                        <td class="fieldValue">
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblNeedCheckCartonEdit" runat="server" Visible="True">比对箱号</asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblMinElectricCurrent" runat="server" Visible="False">最小电流值</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtMinElectricCurrent" runat="server" CssClass="require" Width="130px"
                                Visible="False"></asp:TextBox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblMaxElectricCurrent" runat="server" Visible="False">最大电流值</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtMaxElectricCurrent" runat="server" CssClass="require" Width="130px"
                                Visible="False"></asp:TextBox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblConfigureCode" Style="display: none;" runat="server">配置码</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtConfig" runat="server" Style="display: none;" CssClass="textbox"
                                Width="130px"></asp:TextBox>
                        </td>
                        <td class="fieldName" nowrap style="width: 70px">
                            <asp:Label Style="display: none;" ID="lblVolumnNumEdit" runat="server"> 装车数量</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtVolumnEdit" runat="server" Style="display: none;" CssClass="require"
                                Width="130px" MaxLength="10"></asp:TextBox>
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
                                runat="server" onserverclick="cmdAdd_ServerClick" bindclick="true" />
                        </td>
                        <td class="toolBar">
                            <input class="submitImgButton" id="cmdSave" type="submit" value="保 存" name="cmdSave"
                                runat="server" onserverclick="cmdSave_ServerClick" bindclick="true" />
                        </td>
                        <td>
                            <input class="submitImgButton" id="cmdCancel" type="submit" value="取 消" name="cmdCancel"
                                runat="server" onserverclick="cmdCancel_ServerClick" bindclick="true" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
