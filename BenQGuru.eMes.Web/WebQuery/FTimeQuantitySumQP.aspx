<%@ Page Language="c#" CodeBehind="FTimeQuantitySumQP.aspx.cs" AutoEventWireup="True"
    Inherits="BenQGuru.eMES.Web.WebQuery.FTimeQuantitySumQP" %>

<%@ Register TagPrefix="igtblexp" Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport"
    Assembly="Infragistics35.WebUI.UltraWebGrid.ExcelExport.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="uc1" TagName="eMesTime" Src="../UserControl/DateTime/DateTime/eMesTime.ascx" %>
<%@ Register TagPrefix="cc2" Namespace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="uc1" TagName="eMesDate" Src="../UserControl/DateTime/DateTime/eMesDate.ascx" %>
<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>FTimeQuantitySumQP</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="<%=StyleSheet%>" rel="stylesheet">
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
    <table id="Table1" height="100%" width="100%" runat="server">
        <tbody>
            <tr class="moduleTitle">
                <td>
                    <asp:Label ID="lblTitle" runat="server" CssClass="labeltopic">时间段产量查询</asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <table class="query" height="100%" width="100%">
                        <tbody>
                            <tr>
                                <td class="fieldNameLeft" nowrap>
                                    <asp:Label ID="lblSS" runat="server">产线</asp:Label>
                                </td>
                                <td class="fieldValue" nowrap colspan="2">
                                    <cc2:selectabletextbox id="txtStepSequence" runat="server" Width="150px" Type="stepsequence">
                                    </cc2:selectabletextbox>
                                </td>
                                <td class="fieldName" nowrap>
                                    <asp:Label ID="lblRes" runat="server">资源</asp:Label>
                                </td>
                                <td class="fieldValue" nowrap colspan="2">
                                    <cc2:selectabletextbox id="txtRescode" runat="server" Width="150px" Type="resource">
                                    </cc2:selectabletextbox>
                                </td>
                                <td class="fieldValue" nowrap>
                                </td>
                            </tr>
                            <tr>
                                <td class="fieldNameLeft" nowrap>
                                    <asp:Label ID="lblSDateQuery" runat="server">开始日期</asp:Label>
                                </td>
                                <td class="fieldValue" nowrap>
                                <asp:TextBox  id="dateStartDateQuery"  class='datepicker require' runat="server"  Width="80px"/>
                                </td>
                                <td class="fieldValue" nowrap>
                                    <uc1:emestime id="dateStartTimeQuery" runat="server" cssclass="require" width="60"></uc1:emestime>
                                </td>
                                <td class="fieldName" nowrap>
                                    <asp:Label ID="lblEDateQuery" runat="server">结束日期</asp:Label>
                                </td>
                                <td class="fieldValue" nowrap>
                                <asp:TextBox  id="dateEndDateQuery"  class='datepicker require' runat="server"  Width="80px" />
                                </td>
                                <td class="fieldValue" nowrap>
                                    <uc1:emestime id="dateEndTimeQuery" runat="server" cssclass="require" width="60"></uc1:emestime>
                                </td>
                                <td width="100%">
                                    <input class="submitImgButton" id="cmdQuery" type="submit" value="查 询" name="btnQuery"
                                        runat="server" onserverclick="cmdQuery_ServerClick">
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
            <tr height="100%">
                <td class="fieldGrid">
                    <ig:WebDataGrid ID="gridWebGrid" runat="server" Width="100%">
                    </ig:WebDataGrid>
                </td>
            </tr>
            <tr>
                <td class="smallImgButton">
                    <input class="gridExportButton" id="cmdGridExport2" type="submit" value="  " name="cmdGridExport"
                        runat="server" onserverclick="cmdGridExport2_ServerClick">
                </td>
            </tr>
        </tbody>
    </table>
    </form>
    </TR></TBODY></TABLE></TR></TBODY></TABLE></FORM>
</body>
</html>
