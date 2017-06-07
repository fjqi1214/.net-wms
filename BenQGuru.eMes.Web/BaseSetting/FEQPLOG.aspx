<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="cc2" Namespace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>

<%@ Page Language="c#" CodeBehind="FEQPLOG.aspx.cs" AutoEventWireup="True" Inherits="BenQGuru.eMES.Web.BaseSetting.FEQPLOG" %>

<%@ Register TagPrefix="uc1" TagName="eMESDate" Src="~/UserControl/DateTime/DateTime/eMESDate.ascx" %>
<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>FEQPLOG</title>
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
                <asp:Label ID="lblTitle" runat="server" CssClass="labeltopic">设备维护</asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table class="query" height="100%" width="100%">
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblEQPIDQuery" runat="server">设备编码</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtEQPIDQuery" CssClass="require" runat="server" Width="150px" 
                                ReadOnly="True"></asp:TextBox>
                        </td>
                         <td class="fieldNameLeft" nowrap></td>
                         <td class="fieldValue"></td>
                         <td class="fieldNameLeft" nowrap style="width:100%"></td>
                         <td class="fieldValue"></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr height="100%">
            <td class="fieldGrid">
                <ig:webdatagrid id="gridWebGrid" runat="server" width="100%">
                </ig:webdatagrid>
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
                        <td>
                            <cc1:pagersizeselector id="pagerSizeSelector" runat="server">
                            </cc1:pagersizeselector>
                        </td>
                        <td align="right">
                            <cc1:PagerToolbar id="pagerToolBar" runat="server">
                            </cc1:PagerToolbar>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="normal">
            <td>
                <table class="edit" height="100%" cellpadding="0" width="100%">
                    <tr>

                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblEQPStatusEdit" runat="server">设备装态</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:DropDownList ID="drpEQPStatusEdit" CssClass="require" runat="server" Width="100px">
                            </asp:DropDownList>
                        </td>
                        
                        
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblMemoEdit" runat="server">备注</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtEqpMemoEdit" runat="server" Width="150px" 
                                CssClass="textbox" TextMode="MultiLine" Rows="3" Height="60px"></asp:TextBox>
                        </td>
                        <td class="fieldName" nowrap style="width:100%">
                            
                        </td>
                        <td class="fieldValue">
                            
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
                                runat="server" visible ="false" >
                        </td>
                        <td>
                            <input class="submitImgButton" id="cmdCancel" type="submit" value="取 消" name="cmdCancel"
                                runat="server" visible ="false">
                        </td>
                        <td>
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
