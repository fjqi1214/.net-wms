<%@ Register TagPrefix="uc2" TagName="eMESDate" Src="~/UserControl/DateTime/DateTime/eMESDate.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="uc1" TagName="eMesTime" Src="../UserControl/DateTime/DateTime/eMesTime.ascx" %>

<%@ Page Language="c#" CodeBehind="FArmorPlateMP.aspx.cs" AutoEventWireup="True"
    Inherits="BenQGuru.eMES.Web.SMT.FArmorPlateMP" %>

<%@ Register TagPrefix="cc2" Namespace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>FArmorPlateMP</title>
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
                <asp:Label ID="lblTitle" runat="server" CssClass="labeltopic"> 钢板资料登记</asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table class="query" height="100%" width="100%">
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblAPIDQuery" runat="server"> 厂内编号</asp:Label>
                        </td>
                        <td class="fieldValue" nowrap>
                            <asp:TextBox ID="txtAPIDQuery" runat="server" CssClass="textbox" Width="130px"></asp:TextBox>
                        </td>
                        <td class="fieldNameLeft" style="height: 26px" nowrap>
                            <asp:Label ID="lblBasePlateQuery" runat="server">基板料号</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtBPCodeQuery" runat="server" CssClass="textbox" Width="130px"></asp:TextBox>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblItemCodeQuery" runat="server">产品代码</asp:Label>
                        </td>
                        <td class="fieldValue" nowrap>
                            <asp:TextBox ID="txtItemQuery" runat="server" CssClass="textbox" Width="130px"></asp:TextBox>
                        </td>
                        <td width="100%">
                        </td>
                        <td class="fieldName">
                            <input class="submitImgButton" id="cmdQuery" type="submit" value="查 询" name="btnQuery"
                                runat="server" onserverclick="cmdQuery_ServerClick">
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
                                runat="server" onserverclick="cmdGridExport_ServerClick">
                        </td>
                        <td class="smallImgButton">
                            <input class="deleteButton" id="cmdDelete" type="submit" value="  " name="cmdDelete"
                                runat="server" onserverclick="cmdDelete_ServerClick">
                        </td>
                        <td>
                            <cc1:pagersizeselector id="pagerSizeSelector" runat="server">
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
        <tr class="normal">
            <td>
                <table class="edit" height="100%" cellpadding="0" width="100%">
                    <tr>
                        <td class="fieldNameLeft" nowrap style="width: 76px">
                            <asp:Label ID="lblAPIDEdit" runat="server"> 厂内编号</asp:Label>
                        </td>
                        <td class="fieldValue" nowrap>
                            <asp:TextBox ID="txtAPIDEdit" runat="server" CssClass="require" Width="130px"></asp:TextBox>
                        </td>
                        <td class="fieldNameLeft" style="width: 120px; height: 26px" nowrap>
                            <asp:Label ID="lblBPCodeEdit" runat="server">基板料号</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtBPCodeEdit" runat="server" CssClass="require" Width="130px"></asp:TextBox>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblVersionEdit" runat="server">当前版本</asp:Label>
                        </td>
                        <td class="fieldValue" nowrap>
                            <asp:TextBox ID="txtVersionEdit" runat="server" CssClass="require" Width="130px"></asp:TextBox>
                        </td>
                        <td width="40%">
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldNameLeft" nowrap style="width: 76px">
                            <asp:Label ID="lblItemCodeEdit" runat="server"> 产品代码</asp:Label>
                        </td>
                        <td class="fieldValue" nowrap>
                            <cc2:selectabletextbox id="txtItemEdit" runat="server" width="130" Target="item"
                                Type="item" CssClass="require" ontextchanged="txtItemEdit_TextChanged" AutoPostBack="true">
                            </cc2:selectabletextbox>
                        </td>
                        <td class="fieldNameLeft" style="width: 120px; height: 26px" nowrap>
                            <asp:Label ID="lblThicknessEdit" runat="server">厚度</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtThicknessEdit" runat="server" CssClass="require" Width="130px"></asp:TextBox>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblManSNEdit" runat="server">厂商编号</asp:Label>
                        </td>
                        <td class="fieldValue" nowrap>
                            <asp:TextBox ID="txtManSNEdit" runat="server" CssClass="textbox" Width="130px"></asp:TextBox>
                        </td>
                        <td width="40%">
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldNameLeft" nowrap style="width: 76px">
                            <asp:Label ID="lblLBRateEdit" runat="server"> 联板比例</asp:Label>
                        </td>
                        <td class="fieldValue" nowrap>
                            <asp:TextBox ID="txtLBRateEdit" runat="server" CssClass="require" Width="130px" ReadOnly="true">1</asp:TextBox>
                        </td>
                        <td class="fieldNameLeft" style="width: 120px; height: 26px" nowrap>
                            <asp:Label ID="lblTenAEdit" runat="server">张力A</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtTenAEdit" runat="server" CssClass="require" Width="130px"></asp:TextBox>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblTenBEdit" runat="server">张力B</asp:Label>
                        </td>
                        <td class="fieldValue" nowrap>
                            <asp:TextBox ID="txtTenBEdit" runat="server" CssClass="require" Width="130px"></asp:TextBox>
                        </td>
                        <td width="40%">
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldNameLeft" nowrap style="width: 76px">
                            <asp:Label ID="lblTenCEdit" runat="server"> 张力C</asp:Label>
                        </td>
                        <td class="fieldValue" nowrap>
                            <asp:TextBox ID="txtTenCEdit" runat="server" CssClass="require" Width="130px"></asp:TextBox>
                        </td>
                        <td class="fieldNameLeft" style="width: 120px; height: 26px" nowrap>
                            <asp:Label ID="lblTenDEdit" runat="server">张力D</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtTenDEdit" runat="server" CssClass="require" Width="130px"></asp:TextBox>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblTenEEdit" runat="server">张力E</asp:Label>
                        </td>
                        <td class="fieldValue" nowrap>
                            <asp:TextBox ID="txtTenEEdit" runat="server" CssClass="require" Width="130px"></asp:TextBox>
                        </td>
                        <td width="40%">
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldNameLeft" nowrap style="width: 76px">
                            <asp:Label ID="lblInFacDate" runat="server">进厂日期</asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 159px">
                            <asp:TextBox type="text" ID="txtInFacDate" class='datepicker' runat="server" Width="130px" />
                        </td>
                        <td class="fieldNameLeft" style="width: 120px; height: 26px" nowrap>
                            <asp:Label ID="lblInFacTime" runat="server">进厂时间</asp:Label>
                        </td>
                        <td class="fieldValue" align="right">
                            <uc1:emestime id="txtInFacTime" runat="server" width="60"></uc1:emestime>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                        </td>
                        <td class="fieldValue" nowrap>
                        </td>
                        <td width="40%">
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldNameLeft" nowrap style="width: 76px">
                            <asp:Label ID="lblMemoEdit" runat="server"> 备注</asp:Label>
                        </td>
                        <td class="fieldValue" colspan="5" nowrap>
                            <asp:TextBox ID="txtMemoEdit" runat="server" CssClass="textbox" Width="100%" MaxLength="100"></asp:TextBox>
                        </td>
                        <td width="40%">
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
                                runat="server" onserverclick="cmdAdd_ServerClick">
                        </td>
                        <td class="toolBar">
                            <input class="submitImgButton" id="cmdSave" type="submit" value="保 存" name="cmdSave"
                                runat="server" onserverclick="cmdSave_ServerClick">
                        </td>
                        <td>
                            <input class="submitImgButton" id="cmdCancel" type="submit" value="取 消" name="cmdCancel"
                                runat="server" onserverclick="cmdCancel_ServerClick">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
