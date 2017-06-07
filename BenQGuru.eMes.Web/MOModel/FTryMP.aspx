<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FTryMP.aspx.cs" Inherits="BenQGuru.eMES.Web.MOModel.FTryMP" %>

<%@ Register TagPrefix="uc1" TagName="eMESDate" Src="~/UserControl/DateTime/DateTime/eMESDate.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="cc2" Namespace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>FTryMP</title>
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
                <asp:Label ID="lblTitle" runat="server" CssClass="labeltopic">试流单维护</asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table class="query" height="100%" width="100%">
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblTryStatusQuery" runat="server">试流单状态</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:DropDownList ID="drpTryStatusQuery" runat="server" CssClass="textbox" Width="150px"
                                OnLoad="drpTryStatus_Load">
                            </asp:DropDownList>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblTryCodeQuery" runat="server">试流单编号</asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 159px">
                            <asp:TextBox ID="txtTryCodeQuery" runat="server" Width="150px" CssClass="textbox"></asp:TextBox>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblItemIDQuery" runat="server">物料代码</asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 159px">
                            <cc2:selectabletextbox id="txtMaterialCodeQuery" runat="server" width="150px" type="material">
                            </cc2:selectabletextbox>
                        </td>
                        <td width="100%" nowrap>
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblDeptQuery" runat="server">部门</asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 159px">
                            <cc2:selectabletextbox id="txtDeptQuery" runat="server" width="150px" type="department">
                            </cc2:selectabletextbox>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblVendorNameQuery" runat="server">供货厂家</asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 159px">
                            <asp:TextBox ID="txtVendorNameQuery" runat="server" Width="150px" DESIGNTIMEDRAGDROP="257"
                                CssClass="textbox"></asp:TextBox>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblCreatDateQuery" runat="server">创建日期</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox type="text" ID="dateCreatDateQuery" class='datepicker' runat="server"
                                Width="130px" />
                        </td>
                        <td width="100%" nowrap>
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblCreateUserQuery" runat="server">创建人</asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 159px">
                            <cc2:selectabletextbox id="txtCreateUserQuery" runat="server" width="150px" type="createuser"
                                CanKeyIn="True">
                            </cc2:selectabletextbox>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblTryTypeQuery" runat="server">试流类型</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:DropDownList ID="drpTryTypeQuery" runat="server" CssClass="textbox" Width="150px"
                                OnLoad="drpTryTypeQuery_Load">
                            </asp:DropDownList>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
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
                            <cc1:pagertoolbar id="pagerToolBar" runat="server">
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
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblTryCodeEdit" runat="server">试流单编号</asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 159px">
                            <asp:TextBox ID="txtTryCodeEdit" runat="server" CssClass="require" Width="130px"></asp:TextBox>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblItemIDEdit" runat="server">物料代码</asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 159px">
                            <cc2:selectabletextbox id="txtMaterialCodeEdit" runat="server" width="130px" type="singlematerial">
                            </cc2:selectabletextbox>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblQTYAndLoopEdit" runat="server">数量/套</asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 159px">
                            <asp:TextBox ID="txtPlanQtyEdit" runat="server" CssClass="textbox" Width="130px"></asp:TextBox>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtActualQty" runat="server" CssClass="textbox" Visible="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblUserDepartmentEdit" runat="server">部门</asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 159px">
                            <cc2:selectabletextbox id="txtDeptEdit" runat="server" width="130px" type="singledepartment">
                            </cc2:selectabletextbox>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblVendorNameEdit" runat="server">供货厂家</asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 159px">
                            <asp:TextBox ID="txtVendorNameEdit" runat="server" CssClass="textbox" Width="130px">&nbsp;&nbsp;</asp:TextBox>
                        </td>
                        <td class="fieldValue" colspan="2">
                            <asp:CheckBox ID="chbGenerateLot" runat="server" Text="独立产生批"></asp:CheckBox>
                        </td>
                    </tr>
                    <td class="fieldNameLeft" nowrap>
                        <asp:Label ID="lblTryTypeEdit" runat="server">试流类型</asp:Label>
                    </td>
                    <td class="fieldValue">
                        <asp:DropDownList ID="drpTryTypeEdit" runat="server" CssClass="require" Width="130px"
                            OnLoad="drpTryTypeEdit_Load">
                        </asp:DropDownList>
                    </td>
                    <td class="fieldNameLeft" nowrap>
                        <asp:Label ID="lblSoftWareVersionEdit" runat="server">软件版本号</asp:Label>
                    </td>
                    <td class="fieldValue" style="width: 159px">
                        <asp:TextBox ID="txtSoftWareVersionEdit" runat="server" CssClass="textbox" Width="130px"></asp:TextBox>
                    </td>
                    <td class="fieldNameLeft" nowrap>
                        <asp:Label ID="lblAgingDateEdit" runat="server">老化时间</asp:Label>
                    </td>
                    <td class="fieldValue">
                        <asp:TextBox ID="txtAgingDateEdit" CssClass="textbox" Width="110" runat="server"></asp:TextBox>
                    </td>
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblLotWaitReslutEdit" runat="server">后续批量是否待试产结果</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:DropDownList ID="drpLotWaitReslutEdit" runat="server" CssClass="textbox" Width="130px"
                                OnLoad="drpLotWaitReslutEdit_Load">
                            </asp:DropDownList>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblBothChangedEdit" runat="server">软、硬件是否配套更改</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:DropDownList ID="drpBothChangedEdit" runat="server" CssClass="textbox" Width="130px"
                                OnLoad="drpBothChangedEdit_Load">
                            </asp:DropDownList>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblTryReasonEdit" runat="server">试流原因（或改进事由)</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtTryReasonEdit" MaxLength="1000" runat="server" CssClass="textbox"
                                TextMode="MultiLine" Width="130px" Height="50px"></asp:TextBox>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblTryResultEdit" runat="server">试流结论：</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtResultEdit" MaxLength="200" runat="server" CssClass="textbox"
                                TextMode="MultiLine" Width="130px" Height="50px"></asp:TextBox>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblMemoEdit" runat="server">备注：</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtMemoEdit" MaxLength="1000" runat="server" CssClass="textbox"
                                TextMode="MultiLine" Width="130px" Height="50px"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
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
                            <input class="submitImgButton" id="cmdSave" type="submit" value="保 存" name="cmdSave"
                                runat="server">
                        </td>
                        <td>
                            <input class="submitImgButton" id="cmdCancel" type="submit" value="取 消" name="cmdCancel"
                                runat="server">
                        </td>
                        <td nowrap>
                            &nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                        <td class="toolBar">
                            <input language="javascript" class="submitImgButton" id="cmdRelease" type="submit"
                                value="下 发" name="cmdAdd" runat="server" designtimedragdrop="168" onserverclick="cmdRelease_ServerClick">
                        </td>
                        <td class="toolBar">
                            <input language="javascript" class="submitImgButton" id="cmdInitial" type="submit"
                                value="取消下发" name="cmdAdd" runat="server" onserverclick="cmdReleaseCancle_ServerClick">
                        </td>
                        <td class="toolBar">
                            <input language="javascript" class="submitImgButton" id="cmdPending" type="submit"
                                value="暂 停" name="cmdAdd" runat="server" designtimedragdrop="228" onserverclick="cmdPending_ServerClick">
                        </td>
                        <td class="toolBar">
                            <input language="javascript" class="submitImgButton" id="cmdAnnulPending" type="submit"
                                value="取消暂停" name="cmdAdd" runat="server" designtimedragdrop="170" onserverclick="cmdPendCancle_ServerClick">
                        </td>
                        <td class="toolBar">
                            <input language="javascript" class="submitImgButton" id="cmdCloseMO" type="submit"
                                value="关 单" name="cmdAdd" runat="server" onserverclick="cmdTryClose_ServerClick">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
