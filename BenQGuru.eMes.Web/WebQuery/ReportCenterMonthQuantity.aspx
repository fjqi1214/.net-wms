<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Page language="c#" Codebehind="ReportCenterMonthQuantity.aspx.cs" AutoEventWireup="True" Inherits="BenQGuru.eMES.Web.WebQuery.ReportCenterMonthQuantity" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ReportCenterMonthQuantity</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href="<%=StyleSheet%>" rel=stylesheet>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table height="100%" width="100%">
				<TBODY>
					<tr>
						<td width="1%"><img src="../Skin/Image/ico_arrow.gif" width="15" height="15"></td>
						<td class="Title2"><asp:label id="lblMonthQuantityTitle" runat="server">产量报表-->本月产量趋势</asp:label></td>
					</tr>
					<tr height="84%" align="center">
						<td colspan="2"><cc1:OWCChartSpace id="OWCChartSpace1" runat="server"></cc1:OWCChartSpace></td>
					</tr>
					<tr>
						<td align="center" colspan="2"><INPUT class="submitImgButton" id="cmdReturn" type="submit" value="返回" name="btnReturn"
								runat="server" onserverclick="cmdReturn_ServerClick">
						</td>
					</tr>
				</TBODY>
			</table>
		</form>
		</TR></TBODY></TABLE></TR></TBODY></TABLE></FORM>
	</body>
</HTML>
