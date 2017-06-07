<%@ Page Language="c#" CodeBehind="FRoute2OperationSP.aspx.cs" AutoEventWireup="True"
    Inherits="BenQGuru.eMES.Web.BaseSetting.FRoute2OperationSP" %>

<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>FShiftMP</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="<%=StyleSheet%>" rel="stylesheet">
    <style type="text/css">
           <%--#chklstOPAttributeEdit{border-collapse:collapse; line-height:18px}--%>
           #chklstOPAttributeEdit td{width:20%;}
           
          <%-- #chklstOPControlEdit{border-collapse:collapse; line-height:18px}--%>
           #chklstOPControlEdit td{width:20%;}

</style>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
    <table id="Table1" height="100%" width="100%" runat="server">
        <tr class="moduleTitle">
            <td style="height: 19px">
                <asp:Label ID="lblTitle" runat="server" CssClass="labeltopic"> 工序列表</asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table class="query" height="100%" width="100%">
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblRouteCode" runat="server"> 途程代码</asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 159px">
                            <asp:TextBox ID="txtRouteCodeQuery" runat="server" CssClass="textbox" ReadOnly="True"
                                Width="150px"></asp:TextBox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblItemOperationCodeQuery" runat="server" Visible="False"> 工序代码</asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 159px">
                            <asp:TextBox ID="txtOperationCodeQuery" runat="server" CssClass="textbox" Width="150px"
                                Visible="False"></asp:TextBox>
                        </td>
                        <td nowrap width="100%">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr height="100%">
            <td class="fieldGrid">
                <ig:WebDataGrid ID="gridWebGrid" runat="server" Width="100%">
                <Behaviors>
                <ig:EditingCore>
                <Behaviors>
                <ig:CellEditing>
                <ColumnSettings>
                <ig:EditingColumnSetting  ColumnKey="" ReadOnly="false"/>
                </ColumnSettings>
                </ig:CellEditing>
                </Behaviors>
                </ig:EditingCore>
                </Behaviors>
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
                <table class="edit" height="30%" cellpadding="0" width="100%">
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblOperationSequenceEdit" runat="server">工序序列</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtOperationSequenceEdit" runat="server" CssClass="require" Width="150px"></asp:TextBox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblItemOperationCodeEdit" runat="server">工序代码</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtOperationCodeEdit" runat="server" CssClass="require" ReadOnly="True"
                                Width="150px"></asp:TextBox>
                        </td>
                        <td nowrap width="100%">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="normal">
            <td>
                <table class="edit" height="30%" cellpadding="0" width="100%">
                    <tr>
                        <td class="fieldLeft" nowrap>
                            <asp:Label ID="lblActionOP" runat="server">Action工序</asp:Label>
                        </td>
                        <td nowrap width="100%">
                        </td>
                        <td nowrap width="100%">
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldLeft" colspan="8" nowrap>
                            <asp:CheckBoxList ID="chklstOPControlEdit" runat="server" RepeatDirection="Horizontal"
                                Width="100%" RepeatColumns="5" OnLoad="chklstOPControlEdit_Load">
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="normal">
            <td>
                <table class="edit" height="30%" cellpadding="0" width="100%">
                    <tr>
                        <td class="fieldLeft" nowrap>
                            <asp:Label ID="lblAttributeOP" runat="server">Attribute工序</asp:Label>
                        </td>
                        <td nowrap width="100%">
                        </td>
                        <td nowrap width="100%">
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldLeft" colspan="8" nowrap>
                            <asp:CheckBoxList ID="chklstOPAttributeEdit" runat="server" RepeatDirection="Horizontal"
                                Width="100%" RepeatColumns="5">
                            </asp:CheckBoxList>
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
                            <input class="submitImgButton" id="cmdAdd" type="submit" value="新 增" name="cmdSelect"
                                runat="server" onserverclick="cmdSelect_ServerClick">
                        </td>
                        <td class="toolBar">
                            <input class="submitImgButton" id="cmdSave" type="submit" value="保 存" name="cmdSave"
                                runat="server">
                        </td>
                        <td class="toolBar">
                            <input class="submitImgButton" id="cmdSaveTotal" type="submit" value="保存序号" name="cmdSaveTotal"
                                runat="server" onserverclick="cmdSaveTotal_ServerClick">
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
