<%@ Register TagPrefix="uc1" TagName="eMESDate" Src="~/UserControl/DateTime/DateTime/eMESDate.ascx" %>
<%@ Page language="c#" Codebehind="FRealReportYR.aspx.cs" AutoEventWireup="True" Inherits="BenQGuru.eMES.Web.RealTimeReport.FRealReportYR" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>FRealReportYR</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href="<%=StyleSheet%>" rel=stylesheet>
				<style type="text/css">
		<!--
							.gridWebGrid-ic
							{font-family:Verdana;
							font-size:12px; 
							border-color:#D7D8D9; 
							border-style:Solid; 
							border-width:1px; 
							border-left-width:0px; 
							border-top-width:0px; 
							padding-left:3px; text-align:left; vertical-align:middle;
							}
							.gridWebGrid-aic
							{font-family:Verdana;font-size:12px; background-color:#EBEBEB; border-color:#D7D8D9; 
							border-style:Solid; border-width:1px; border-left-width:0px; border-top-width:0px;
							 padding-left:3px; text-align:left; vertical-align:middle;}
							 .gridWebGrid-0-ic
							 {font-family:Verdana;font-size:12px; border-color:#D7D8D9; 
							 border-style:Solid; border-width:1px; border-left-width:0px;
							 border-top-width:0px; padding-left:3px; text-align:left; vertical-align:middle; 
							 }
							 .gridWebGrid-0-aic
							 {font-family:Verdana;font-size:12px;
							  background-color:#EBEBEB; border-color:#D7D8D9; 
							  border-style:Solid; border-width:1px; 
							  border-left-width:0px; border-top-width:0px; padding-left:3px; 
							  text-align:left; vertical-align:middle; }
							  .gridWebGrid-ecc
							  { border-color:Black; border-style:None; border-width:1px; padding-bottom:1px; 
							  vertical-align:middle;}
							  .gridWebGrid-hc
							  {
								font-family:Verdana;font-size:12px;font-weight:bold; 
								background-color:#ABABAB; border-color:White; border-style:Dashed; 
								border-width:1px; border-left-color:White; border-top-color:White; 
								border-right-color:White; border-bottom-color:White;
							   border-left-width:1px; border-top-width:1px; padding-left:3px; text-align:left; 
							   vertical-align:middle;}
							   .gridWebGrid-shc
							   {font-family:Verdana;font-size:12px;font-weight:bold; 
							   background-color:#ABABAB; border-color:#D7D8D9; border-style:Solid;
							    border-width:1px; border-left-color:White; border-top-color:White; 
							    border-right-color:White; border-bottom-color:White; 
							    border-left-width:0px; border-top-width:0px; padding-left:3px; text-align:left; 
							    vertical-align:middle;
							    }
		--></style>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
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
								<td class="fieldName"></td>
							</tr>
							<tr>
								<td class="fieldNameLeft" noWrap></td>
								<td class="fieldValue"><asp:radiobutton id="rbLine" runat="server" Width="150px" Text="按产线"></asp:radiobutton></td>
								<td class="fieldName" noWrap><asp:label id="lblTargetYR" runat="server"> 目标良率</asp:label></td>
								<td class="fieldValue"><asp:textbox id="txtTargetYR" runat="server" Width="150px"></asp:textbox></td>
								<td class="fieldName" noWrap></td>
								<td class="fieldValue"></td>
								<td></td>
								<td></td>
								<td class="fieldName"><INPUT class="submitImgButton" id="Submit2" type="submit" value="刷新" name="cmdRefresh"
										runat="server" onserverclick="Submit2_ServerClick"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td class="fieldGrid" width="100%" height="100%">
						<table height="100%" width="100%">
							<tr height="100%" width="100%">
								<td colSpan="10">
									<table>
										<tr>
											<td></td>
										</tr>
									</table>
								</td>
							</tr>

							<%= HTMLContent%>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
