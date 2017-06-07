<%@ Page Language="c#" CodeBehind="FStepSequenceMP.aspx.cs" AutoEventWireup="True"
    Inherits="BenQGuru.eMES.Web.BaseSetting.FStepSequenceMP" %>

<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="cc2" Namespace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>FStepSequenceMP</title>
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
                <asp:Label ID="lblTitle" runat="server" CssClass="labeltopic"> 生产线维护</asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table class="query" height="100%" width="100%">
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblStepSequenceCodeQuery" runat="server"> 生产线代码</asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 159px">
                            <asp:TextBox ID="txtStepSequenceCodeQuery" runat="server" Width="150px" CssClass="textbox"></asp:TextBox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblSegmentCodeQuery" runat="server"> 工段代码</asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 159px">
                            <cc2:SelectableTextBox ID="txtSegmentCodeQuery" runat="server" Type="segment" Target=""
                                CanKeyIn="True"></cc2:SelectableTextBox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblBigStepSequenceCodeQuery" runat="server">大线代码</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:DropDownList ID="DropDownListBigStepSequenceCodeQuery" runat="server" CssClass="textbox"
                                Width="140px" OnLoad="drpBIGSSQuery_Load">
                            </asp:DropDownList>
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
                            <asp:Label ID="lblStepSequenceCodeEdit" runat="server">生产线代码</asp:Label>
                        </td>
                        <td class="fieldValue" style="height: 26px">
                            <asp:TextBox ID="txtStepSequenceCodeEdit" runat="server" CssClass="require" Width="130px"></asp:TextBox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblStepSequenceDescriptionEdit" runat="server">生产线描述</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtStepSequenceDescriptionEdit" runat="server" Width="130px" CssClass="textbox"></asp:TextBox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblSegmentCodeEdit" runat="server">工段代码</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:DropDownList CssClass="require" ID="drpSegmentCodeEdit" runat="server" Width="130px"
                                OnLoad="drpSegmentCodeEdit_Load" OnSelectedIndexChanged="drpSegmentCodeEdit_SelectedIndexChanged"
                                AutoPostBack="True">
                            </asp:DropDownList>
                        </td>
                        <td class="fieldName" style="height: 26px" nowrap>
                            <asp:Label ID="lblShiftTypeCodeEdit" runat="server">班制代码</asp:Label>
                        </td>
                        <td class="fieldValue" style="height: 26px">
                            <asp:DropDownList ID="drpShifTypeCode" runat="server" CssClass="textbox" Width="130px"
                                OnLoad="drpShifTypeCode_Load">
                            </asp:DropDownList>
                        </td>
                        <td width="100%" nowrap>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblBigStepSequenceSEQ" runat="server">顺序(大线)</asp:Label>
                        </td>
                        <td class="fieldValue" style="height: 26px">
                            <asp:TextBox ID="txtBigStepSequenceSEQ" runat="server" CssClass="require" Width="130px"
                                MaxLength="10" CausesValidation="True"></asp:TextBox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblBigStepSequenceCodeEdit" runat="server">大线代码</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:DropDownList ID="DropDownListBigStepSequenceCodeEdit" runat="server" CssClass="textbox"
                                Width="130px" OnLoad="drpBIGSSEdit_Load">
                            </asp:DropDownList>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblOrgEdit" runat="server">组织</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:DropDownList ID="DropDownListOrg" runat="server" CssClass="textbox" Width="130px">
                            </asp:DropDownList>
                        </td>
                        <td class="fieldName">
                            &nbsp;
                        </td>
                        <td class="fieldValue">
                            <asp:CheckBox runat="server" ID="chbSaveIntoStock" Text="直接入库" Visible="false" /><asp:TextBox
                                ID="txtSave1" runat="server" Width="80px" CssClass="textbox" Visible="false"></asp:TextBox><asp:TextBox
                                    ID="txtSave2" runat="server" Width="80px" CssClass="textbox" Visible="false"></asp:TextBox>
                        </td>
                        <td width="100%" nowrap>
                            &nbsp;
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
