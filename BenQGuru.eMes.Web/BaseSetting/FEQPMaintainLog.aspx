<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FEQPMaintainLog.aspx.cs" Inherits="BenQGuru.eMES.Web.BaseSetting.FEQPMaintainLog" %>

<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="cc2" Namespace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>

<%@ Register TagPrefix="uc1" TagName="eMESDate" Src="~/UserControl/DateTime/DateTime/eMESDate.ascx" %>
<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>FEQPMaintainLog</title>
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
                <asp:Label ID="lblTitle" runat="server" CssClass="labeltopic">保养日志维护</asp:Label>
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
                            <cc2:selectabletextbox id="txtEQPIDQuery" runat="server" Type="Equiment" Width="100px"
                                cankeyin="true" CssClass="textbox">
                            </cc2:selectabletextbox>
                        </td>
                                      
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblMaintenanceResultQuery" runat="server">保养结果</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:dropdownlist id="drpMaintenanceResultQuery" runat="server" CssClass="textbox" Width="130px"></asp:dropdownlist>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblCycleQuery" runat="server">周期</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:DropDownList ID="drpCycleQuery" CssClass="textbox" runat="server" Width="150px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldName" style="height: 26px" nowrap>
                                <asp:Label ID="lblMaintenanceDateFromQuery" runat="server">保养日期 从</asp:Label></td>
                            <td class="fieldValue" style="height: 26px">
                                       <asp:TextBox type="text" id="dateMaintenanceDateFromQuery"  class='datepicker' runat="server"  Width="130px"/>
                               
                            </td>
                            <td class="fieldName" style="height: 26px" nowrap>
                                <asp:Label ID="lblMaintenanceDateToQuery" runat="server">到</asp:Label></td>
                            <td class="fieldValue" style="height: 26px">
                                     <asp:TextBox type="text" id="dateMaintenanceDateToQuery"  class='datepicker' runat="server"  Width="130px"/>    
                            </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblEQPMaintenanceTypeQuery" runat="server">保养类型</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:DropDownList ID="ddlEQPMaintenanceTypeQuery" runat="server" Width="150px">
                            </asp:DropDownList>
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

        <tr class="toolBar">
            <td>
                <table class="toolBar">
                    <tr>
                        <td class="toolBar">
                            <input class="submitImgButton" id="cmdAdd" type="submit" value="新 增" name="cmdAdd"
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
