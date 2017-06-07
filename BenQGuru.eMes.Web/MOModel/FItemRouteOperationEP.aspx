<%@ Page Language="c#" CodeBehind="FItemRouteOperationEP.aspx.cs" AutoEventWireup="True"
    Inherits="BenQGuru.eMES.Web.MOModel.FItemRouteOperationEP" %>

<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>FItemRouteOperationEP</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="<%=StyleSheet%>" rel="stylesheet">
    <script src="../Scripts/jquery-1.9.1.js" type="text/javascript"></script>
    <script>
        $(function () {
            var height = $(window).height();
            if (height < $(document).height()) {
                height = $(document).height();
            }

            $("#tableMain").height(height-5);
        })
            
    </script>
    <style type="text/css">
            #chklstOPAttributeEdit td
            {
                width: 20%;
            }
            #chklstOPControlEdit td
            {
                width: 20%;
            }
        </style>
</head>
<body ms_positioning="GridLayout">
    <form id="impForm" method="post" runat="server">
    <table id="tableMain" width="100%" runat="server">
        <tr class="normal" height="18">
            <td>
                <table class="edit" height="100%" width="100%">
                    <tr>
                        <td class="fieldLeft" nowrap>
                            <asp:Label ID="lblItemOperationSeqEdit" runat="server">工序序号</asp:Label>
                        </td>
                        <td class="fieldValue" nowrap>
                            <asp:TextBox ID="txtItemRouteOperationSeqEdit" runat="server" CssClass="textbox"
                                Width="40px"></asp:TextBox>
                        </td>
                        <td class="fieldLeft" nowrap>
                            <asp:Label ID="lblItemOperationCodeEdit" runat="server">工序代码</asp:Label>
                        </td>
                        <td class="fieldValue" nowrap>
                            <asp:TextBox ID="txtItemOperationCodeEdit" runat="server" CssClass="textbox" Width="90px"></asp:TextBox>
                        </td>
                        <td class="fieldLeft" nowrap>
                            <asp:Label ID="lblOPDescriptionQuery" runat="server">工序描述</asp:Label>
                        </td>
                        <td class="fieldValue" nowrap>
                            <asp:TextBox ID="txtOPDescriptionQuery" runat="server" CssClass="textbox" Width="90px"></asp:TextBox>
                        </td>
                        <td style="padding-left: 8px" nowrap align="center">
                            <asp:Label ID="lblOptionalOP" runat="server" Width="60px">线外工序</asp:Label>
                        </td>
                        <td class="fieldValue" nowrap>
                            <asp:DropDownList ID="drpOptionalOP" runat="server" Width="110px" AutoPostBack="True">
                            </asp:DropDownList>
                        </td>
                        <td nowrap width="100%">
                            <input class="submitImgButton" id="cmdNGReasonSelect" type="submit" value="不良原因组"
                                name="cmdSave" runat="server" onserverclick="cmdSelect_ServerClick">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="normal">
            <td>
                <table class="edit" cellpadding="0" width="100%">
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
                        <td class="fieldLeft" colspan="8">
                            <asp:CheckBoxList ID="chklstOPControlEdit" runat="server" Width="100%" RepeatDirection="Horizontal"
                                RepeatColumns="5" AutoPostBack="True" OnSelectedIndexChanged="chklstOPControlEdit_SelectedIndexChanged">
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                    <asp:Panel ID="pnlMainEdit" runat="server">
                        <tr>
                            <td>
                                <table>
                                    <tr>
                                        <td style="padding-left: 8px" nowrap align="center">
                                            <asp:Label ID="lblMergeTypeEdit" runat="server" Width="60px"> 转化类型</asp:Label>
                                        </td>
                                        <td class="fieldValue" nowrap>
                                            <asp:DropDownList ID="drpMergeTypeEdit" runat="server" Width="120px" AutoPostBack="True"
                                                OnSelectedIndexChanged="drpMergeTypeEdit_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td nowrap>
                                            <asp:Panel ID="PnlChildEdit" runat="server" Width="100%" >
                                                <table width="100%">
                                                    <tr>
                                                        <td style="padding-left: 8px" nowrap>
                                                            <asp:Label ID="lblMergeRule" runat="server">转换比例 </asp:Label>
                                                        </td>
                                                        <td>
                                                            1:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtDenominatorEdit" runat="server" Width="110px" CssClass="textbox"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                        <td nowrap width="100%">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </asp:Panel>
                </table>
            </td>
        </tr>
        <tr class="normal">
            <td>
                <table class="edit" cellpadding="0" width="100%">
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
        <tr height="100%">
            <td>
            </td>
        </tr>
        <tr class="toolBar">
            <td>
                <table class="toolBar">
                    <tr>
                        <td class="toolBar">
                            <input class="submitImgButton" id="cmdSave" type="submit" value="保 存" name="cmdSave"
                                runat="server" onserverclick="cmdSave_ServerClick">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
