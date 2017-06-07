<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FalertNoticeDeal.aspx.cs" Inherits="BenQGuru.eMES.Web.Alert.FalertNoticeDeal" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>处置信息</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR" />
    <meta content="C#" name="CODE_LANGUAGE" />
    <meta content="JavaScript" name="vs_defaultClientScript" />
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
    <link href="<%=StyleSheet%>" rel="stylesheet" />
</head>
<body>
    <form id="Form1" method="post" runat="server">
        <table style="height: 100%; width: 100%;">
            <tr class="moduleTitle">
                <td>
                    <asp:Label ID="lblTitle" runat="server" CssClass="labeltopic">处置信息</asp:Label></td>
            </tr>
            <tr>
                <td>
                    <table class="query" style="height: 50%; width: 100%;">
                        <tr>
                            <td class="fieldNameLeft" nowrap="nowrap">
                                <asp:Label ID="lblAlertItemTypeQuery" runat="server">预警项目</asp:Label></td>
                            <td class="fieldValue" style="width: 159px">
                                <asp:TextBox ID="txtAlertItemType" runat="server" CssClass="textbox" Width="150px" ReadOnly="true">
                                </asp:TextBox>
                            </td>
                            
                            <td class="fieldNameLeft" nowrap="nowrap">
                                <asp:Label ID="lblAlertItemSequence" runat="server">预警项次</asp:Label>
                            </td>
                            <td class="fieldValue" style="width: 159px">
                                <asp:TextBox ID="txtAlertItemSequence" runat="server" CssClass="textbox" Width="150px" ReadOnly="true">
                                </asp:TextBox>
                            </td>
                            
                            <td class="fieldNameLeft" nowrap="nowrap">
                                <asp:Label ID="lblAlertItemDesc" runat="server">项次描述</asp:Label>
                            </td>
                            <td class="fieldValue" style="width: 159px">
                                <asp:TextBox ID="txtAlertItemDesc" runat="server" CssClass="textbox" Width="250px" ReadOnly="true">
                                </asp:TextBox>
                            </td>
                            
                            <td style="width:100%;">
                            </td>
                        </tr>
                        <tr>
                            <td class="fieldNameLeft" nowrap="nowrap">
                                <asp:Label ID="lblAlertNotice" runat="server">通告信息</asp:Label></td>
                            <td class="fieldValue" colspan="6">
                                <asp:TextBox ID="txtAlertNotice" runat="server" CssClass="textbox" Width="777px" Height="70px" TextMode="MultiLine" ReadOnly="true">
                                </asp:TextBox>
                            </td>
                        
                        </tr>
                        <tr>
                            <td class="fieldNameLeft" nowrap="nowrap">
                                <asp:Label ID="lblReasonAnalysis" runat="server">原因分析</asp:Label></td>
                            <td class="fieldValue" colspan="6">
                                <asp:TextBox ID="txtReasonAnalysis" runat="server" CssClass="textbox" Width="777px" Height="70px" TextMode="MultiLine">
                                </asp:TextBox>
                            </td>
                        
                        </tr>
                        <tr>
                            <td class="fieldNameLeft" nowrap="nowrap">
                                <asp:Label ID="lblMeasureMethod" runat="server">措施方法</asp:Label></td>
                            <td class="fieldValue" colspan="6">
                                <asp:TextBox ID="txtMeasureMethod" runat="server" CssClass="textbox" Width="777px" Height="70px" TextMode="MultiLine">
                                </asp:TextBox>
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
                                <input class="submitImgButton" id="cmdSaveReturn" type="submit" value="保存并返回" name="cmdSaveReturn"
                                    runat="server" onserverclick="cmdSaveReturn_ServerClick"/></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
