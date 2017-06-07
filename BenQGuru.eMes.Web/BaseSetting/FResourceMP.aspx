<%@ Page Language="c#" CodeBehind="FResourceMP.aspx.cs" AutoEventWireup="True" Inherits="BenQGuru.eMES.Web.BaseSetting.FResourceMP" %>

<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="cc2" Namespace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>FResourceMP</title>
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
                <asp:Label ID="lblTitle" runat="server" CssClass="labeltopic"> 资源维护</asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table class="query" height="100%" width="100%">
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblResourceCodeQuery" runat="server"> 资源代码</asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 159px">
                            <asp:TextBox ID="txtResourceCodeQuery" runat="server" Width="150px" DESIGNTIMEDRAGDROP="257"
                                CssClass="textbox"></asp:TextBox>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblStepSequenceCodeQuery" runat="server"> 生产线代码</asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 159px">
                            <cc2:SelectableTextBox ID="txtStepSequenceCodeQuery" runat="server" Type="stepsequence"
                                Target="" CanKeyIn="True"></cc2:SelectableTextBox>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblCrewCodeQuery" runat="server">班组代码</asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 159px">
                            <asp:TextBox ID="txtCrewCodeQuery" runat="server" Width="150px" DESIGNTIMEDRAGDROP="257"
                                CssClass="textbox"></asp:TextBox>
                        </td>
                        <td nowrap width="100%">
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
                            <cc1:PagerSizeSelector ID="pagerSizeSelector" runat="server"></cc1:PagerSizeSelector>
                        </td>
                        <td align="right">
                            <cc1:PagerToolBar ID="pagerToolBar" runat="server"></cc1:PagerToolBar>
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
                            <asp:Label ID="lblResourceCodeEdit" runat="server">资源代码</asp:Label>
                        </td>
                        <td class="fieldValue" style="height: 26px">
                            <asp:TextBox ID="txtResourceCodeEdit" runat="server" CssClass="require" Width="110px"></asp:TextBox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblResourceDescriptionEdit" runat="server">资源描述</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtResourceDescriptionEdit" runat="server" Width="120px" CssClass="textbox"></asp:TextBox>
                        </td>
                        <td class="fieldNameLeft" style="height: 26px" nowrap>
                            <asp:Label ID="lblResourceReworkRouteCode" runat="server">返工途程</asp:Label>
                        </td>
                        <td class="fieldValue" style="height: 26px">
                            <cc2:SelectableTextBox ID="txtReworkRoute" runat="server" CanKeyIn="true" Type="singleroute"
                                Target=""></cc2:SelectableTextBox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblCrewCodeEdit" runat="server">班组代码</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:DropDownList ID="drpCrewCodeEdit" runat="server" CssClass="textbox" Width="100px"
                                OnLoad="drpCrewCode_Load">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblOrgIDEdit" runat="server">组织编号</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:DropDownList ID="DropDownListOrg" runat="server" CssClass="textbox" Width="110px">
                            </asp:DropDownList>
                        </td>
                        <td class="fieldNameLeft" style="height: 26px" nowrap>
                            <asp:Label ID="lblStepSequenceCodeEdit" runat="server">生产线代码</asp:Label>
                        </td>
                        <td class="fieldValue" style="height: 26px">
                            <asp:DropDownList ID="drpStepSequenceCodeEdit" runat="server" CssClass="textbox"
                                Width="120px" AutoPostBack="False" OnLoad="drpStepSequenceCodeEdit_Load" OnSelectedIndexChanged="drpStepSequenceCodeEdit_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td class="fieldNameLeft" style="height: 26px" nowrap>
                            <asp:Label ID="lblReworkMo" runat="server">返工需求单</asp:Label>
                        </td>
                        <td class="fieldValue" style="height: 26px">
                            <cc2:SelectableTextBox ID="txtReworkMo" runat="server" CanKeyIn="true" Type="reworkcode"
                                Target="" Width="110px"></cc2:SelectableTextBox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblDefaultDctCommand" runat="server">默认DCT指令</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:DropDownList ID="DropDownListDCT" runat="server" CssClass="textbox" Width="100px"
                                OnLoad="drpDCT_Load">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td class="fieldName" style="height: 26px" nowrap>
                            <asp:Label ID="lblResourceTypeEdit" runat="server">资源类别</asp:Label>
                        </td>
                        <td class="fieldValue" style="height: 26px">
                            <asp:DropDownList ID="drpResourceTypeEdit" runat="server" CssClass="require" DESIGNTIMEDRAGDROP="94"
                                Width="110px" OnLoad="drpResourceTypeEdit_Load">
                            </asp:DropDownList>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblResourceGroupEdit" runat="server">资源归属</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:DropDownList ID="drpResourceGroupEdit" runat="server" CssClass="require" Width="110px"
                                OnLoad="drpResourceGroupEdit_Load">
                            </asp:DropDownList>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblSegmentCodeEdit" runat="server">工段代码</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtSegmentCodeEdit" runat="server" CssClass="textbox" Width="110px"
                                ReadOnly="True"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="lblShiftTypeCodeEdit" runat="server">班制代码</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtShiftEdit" runat="server" CssClass="textbox" Width="110px" ReadOnly="True"></asp:TextBox>
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
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
