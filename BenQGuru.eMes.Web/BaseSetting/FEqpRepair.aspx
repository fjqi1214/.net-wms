<%@ Page Language="c#" CodeBehind="FEqpRepair.aspx.cs" AutoEventWireup="True" Inherits="BenQGuru.eMES.Web.FEqpRepair" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title></title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="<%=StyleSheet%>" rel="stylesheet">
    <meta http-equiv="Pragma" content="no-cache">
    <meta http-equiv="Cache-Control" content="no-cache">
    <meta http-equiv="Expires" content="0">
    <base target="_self">
</head>
<body style="padding-right: 8px; padding-bottom: 8px" scroll="no" rightmargin="0"
    ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
    <table id="Table1" height="100%" cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td class="ModuleTitle" height="1">
                <asp:Label ID="lblFEquipmentRepair" runat="server" CssClass="labeltopic">设备报修</asp:Label>
            </td>
        </tr>
        <tr style="padding-top: 2px">
            <td>
                <table class="edit" height="100%" cellpadding="0" width="100%">
                    <tr>
                        <td class="fieldNameLeft" nowrap style="width: 10%">
                            <asp:Label ID="lblEquipment" runat="server">设备</asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 80%">
                            <asp:TextBox ID="txtEquipment" runat="server" CssClass="require" ReadOnly="true" Width="180px"></asp:TextBox>
                        </td>
                        <td style="width: 10%">
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblTsinfo" runat="server">故障现象</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox MaxLength="400"  CssClass="require" runat="server" ID="txtTsinfo" Rows="10"  TextMode="MultiLine"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="toolBar">
            <td>
                <table class="toolBar" height="100%" width="100%">
                    <tr>
                        <td class="toolBar">
                            <input class="submitImgButton" id="cmdSave" type="submit" value="保 存" name="cmdConfirm"
                                runat="server" onserverclick="cmdSave_ServerClick">
                        </td>
                        <td class="toolBar">
                            <input class="submitImgButton" id="cmdExit" type="submit" value="退 出" name="cmdExit"
                                onclick="top.close();" runat="server">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
