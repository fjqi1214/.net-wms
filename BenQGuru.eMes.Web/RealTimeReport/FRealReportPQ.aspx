<%@ Register TagPrefix="uc1" TagName="eMESDate" Src="~/UserControl/DateTime/DateTime/eMESDate.ascx" %>
<%@ Page language="c#" Codebehind="FRealReportPQ.aspx.cs" AutoEventWireup="True" Inherits="BenQGuru.eMES.Web.RealTimeReport.FRealReportPQ" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>FRealReportPQ</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href="<%=StyleSheet%>" rel=stylesheet>
		<style type="text/css">.gridWebGrid-ic {
	BORDER-RIGHT: #d7d8d9 1px solid; BORDER-TOP: #d7d8d9 0px solid; PADDING-LEFT: 3px; FONT-SIZE: 12px; VERTICAL-ALIGN: middle; BORDER-LEFT: #d7d8d9 0px solid; BORDER-BOTTOM: #d7d8d9 1px solid; FONT-FAMILY: Verdana; TEXT-ALIGN: left
}
.gridWebGrid-aic {
	BORDER-RIGHT: #d7d8d9 1px solid; BORDER-TOP: #d7d8d9 0px solid; PADDING-LEFT: 3px; FONT-SIZE: 12px; VERTICAL-ALIGN: middle; BORDER-LEFT: #d7d8d9 0px solid; BORDER-BOTTOM: #d7d8d9 1px solid; FONT-FAMILY: Verdana; BACKGROUND-COLOR: #ebebeb; TEXT-ALIGN: left
}
.gridWebGrid-0-ic {
	BORDER-RIGHT: #d7d8d9 1px solid; BORDER-TOP: #d7d8d9 0px solid; PADDING-LEFT: 3px; FONT-SIZE: 12px; VERTICAL-ALIGN: middle; BORDER-LEFT: #d7d8d9 0px solid; BORDER-BOTTOM: #d7d8d9 1px solid; FONT-FAMILY: Verdana; TEXT-ALIGN: left
}
.gridWebGrid-0-aic {
	BORDER-RIGHT: #d7d8d9 1px solid; BORDER-TOP: #d7d8d9 0px solid; PADDING-LEFT: 3px; FONT-SIZE: 12px; VERTICAL-ALIGN: middle; BORDER-LEFT: #d7d8d9 0px solid; BORDER-BOTTOM: #d7d8d9 1px solid; FONT-FAMILY: Verdana; BACKGROUND-COLOR: #ebebeb; TEXT-ALIGN: left
}
.gridWebGrid-ecc {
	BORDER-RIGHT: black 1px; BORDER-TOP: black 1px; PADDING-BOTTOM: 1px; VERTICAL-ALIGN: middle; BORDER-LEFT: black 1px; BORDER-BOTTOM: black 1px
}
.gridWebGrid-hc {
	BORDER-RIGHT: white 1px dashed; BORDER-TOP: white 1px dashed; PADDING-LEFT: 3px; FONT-WEIGHT: bold; FONT-SIZE: 12px; VERTICAL-ALIGN: middle; BORDER-LEFT: white 1px dashed; BORDER-BOTTOM: white 1px dashed; FONT-FAMILY: Verdana; BACKGROUND-COLOR: #ababab; TEXT-ALIGN: left
}
.gridWebGrid-shc {
	BORDER-RIGHT: white 1px solid; BORDER-TOP: white 0px solid; PADDING-LEFT: 3px; FONT-WEIGHT: bold; FONT-SIZE: 12px; VERTICAL-ALIGN: middle; BORDER-LEFT: white 0px solid; BORDER-BOTTOM: white 1px solid; FONT-FAMILY: Verdana; BACKGROUND-COLOR: #ababab; TEXT-ALIGN: left
}
		</style>
	</HEAD>
	<body scroll=yes>
		<form id="Form1" method="post" runat="server">
			<table height="100%" width="100%">
				<tr class="moduleTitle">
					<td><asp:label id="lblTitle" runat="server" CssClass="labeltopic"> 产品生产途程</asp:label></td>
				</tr>
				<tr>
					<td>
						<table class="query" height="100%" width="100%">
							<tr>
								<td class="fieldNameLeft" noWrap><asp:label id="lblSegmentCodeQuery" runat="server"> 工段</asp:label></td>
								<td class="fieldValue"><asp:dropdownlist id="drpSegmentCodeQuery" runat="server" Width="150px"></asp:dropdownlist></td>
								<td class="fieldName" noWrap><asp:label id="lblShiftCodeQuery" runat="server"> 班别</asp:label></td>
								<td class="fieldValue"><asp:dropdownlist id="drpShiftCodeQuery" runat="server" Width="150px"></asp:dropdownlist></td>
								<td class="fieldName" noWrap><asp:label id="lblStepSequenceCodeQuery" runat="server"> 产线</asp:label></td>
								<td class="fieldValue"><asp:dropdownlist id="drpStepSequenceCodeQuery" runat="server" Width="150px"></asp:dropdownlist></td>
								<td></td>
								<td></td>
								<td width="100%"></td>
							</tr>
							<tr>
								<td class="fieldNameLeft" noWrap><asp:label id="lblModelQuery" runat="server"> 机种</asp:label></td>
								<td class="fieldValue"><asp:dropdownlist id="drpModelQuery" runat="server" Width="150px"></asp:dropdownlist></td>
								<td class="fieldName" noWrap><asp:label id="lblItemCodeQuery" runat="server"> 产品</asp:label></td>
								<td class="fieldValue"><asp:dropdownlist id="drpItemCodeQuery" runat="server" Width="150px"></asp:dropdownlist></td>
								<td class="fieldName" noWrap><asp:label id="lblMOCodeQuery" runat="server"> 工单</asp:label></td>
								<td class="fieldValue"><asp:dropdownlist id="drpMOCodeQuery" runat="server" Width="150px"></asp:dropdownlist></td>
								<td class="fieldName" noWrap><asp:label id="lblShiftDayQuery" runat="server"> 日期</asp:label></td>
								<td class="fieldValue"><uc1:emesdate id="dateStartDateQuery" runat="server" width="100"></uc1:emesdate></td>
								<td class="fieldName"><INPUT class="submitImgButton" id="Submit1" type="submit" value="刷新" name="cmdRefresh"
										runat="server" onserverclick="Submit1_ServerClick"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td width="100%" height="100%">
						<table height="100%" width="100%">
							<%=HTMLContent%>
							<tr height="100%" width="100%">
								<td height="100%"></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
