<%@ Page language="c#" Codebehind="FHistroyQuantitySummaryQP.aspx.cs" AutoEventWireup="True" Inherits="BenQGuru.eMES.Web.WebQuery.FHistroyQuantitySummaryQP" %>
<%@ Register TagPrefix="uc1" TagName="eMESDate" Src="~/UserControl/DateTime/DateTime/eMESDate.ascx" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="cc2" NameSpace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>FHistroyQuantitySummaryQP</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href="<%=StyleSheet%>" rel=stylesheet>
		<script language="javascript">
			function judgeSummaryTarget()
			{
				if(document.all.rblSummaryTarget_3.checked || document.all.rblSummaryTarget_4.checked || document.all.rblSummaryTarget_5.checked || document.all.rblSummaryTarget_6.checked)
				{
					document.all.tdppm.style.display = "block";
				}
				else
				{
					document.all.tdppm.style.display = "none";
				}
			}
			function OnInit()
			{
				judgeSummaryTarget();
			}
			
		</script>
	</HEAD>
	<body scroll="yes" MS_POSITIONING="GridLayout" onload="OnInit()">
		<form id="Form1" method="post" runat="server">
			<table height="100%" width="100%">
				<tr class="moduleTitle">
					<td><asp:label id="lblTitle" runat="server" CssClass="labeltopic">历史产量统计 </asp:label></td>
				</tr>
				<tr>
					<td>
						<table class="query" height="100%" width="100%">
							<TR>
								<td class="fieldNameLeft" noWrap><asp:label id="lblModelCodeQuery" runat="server">产品别代码</asp:label></td>
								<td class="fieldValue1"><cc2:selectabletextbox id="txtCondition" runat="server" Type="model" Width="100px"></cc2:selectabletextbox></td>
								<td id="tdppm" style="DISPLAY: none" colSpan="6">
									<table>
										<TR>
											<td class="fieldName" noWrap><asp:label id="lblModelQuery" runat="server">产品别代码</asp:label></td>
											<td class="fieldValue1"><cc2:selectabletextbox id="txtModelQuery" runat="server" Type="model" Width="100px"></cc2:selectabletextbox></td>
											<td class="fieldName" noWrap><asp:label id="lblItemQuery" runat="server">产品代码</asp:label></td>
											<td class="fieldValue1"><cc2:selectabletextbox id="txtItemQuery" runat="server" Type="item" Width="100px"></cc2:selectabletextbox></td>
											<td class="fieldNameLeft" noWrap><asp:label id="lblMoQuery" runat="server">工单代码</asp:label></td>
											<td class="fieldValue1"><cc2:selectabletextbox id="txtMoQuery" runat="server" Type="mo" Width="100px"></cc2:selectabletextbox></td>
										</TR>
									</table>
								</td>
							</TR>
							<tr>
								<td class="fieldNameLeft" noWrap><asp:label id="lblStartDateQuery" runat="server">起始日期</asp:label></td>
								<td class="fieldValue1">
                                <asp:TextBox  id="dateStartDateQuery"  class='datepicker' runat="server"  Width="130px"/>
                                </td>
								<td class="fieldName" noWrap><asp:label id="lblEndDateQuery" runat="server">结束日期</asp:label></td>
								<td class="fieldValue1">
                                <asp:TextBox  id="dateEndDateQuery"  class='datepicker' runat="server"  Width="130px"/>
  </td>
								<td></td>
								<td></td>
								<td></td>
								<td></td>
							</tr>
							<tr>
								<td class="fieldNameLeft" noWrap><asp:label id="lblTimingType" runat="server">时间类型</asp:label></td>
								<td colSpan="7"><asp:radiobuttonlist id="rblTimingType" runat="server" RepeatDirection="Horizontal"></asp:radiobuttonlist></td>
							</tr>
							<tr>
								<td class="fieldNameLeft" noWrap><asp:label id="lblSummaryTarget" runat="server">统计对象</asp:label></td>
								<td colSpan="7" nowrap><asp:radiobuttonlist id="rblSummaryTarget" runat="server" RepeatDirection="Horizontal" AutoPostBack="True" onselectedindexchanged="rblSummaryTarget_SelectedIndexChanged"></asp:radiobuttonlist></td>
							</tr>
							<tr>
								<td class="fieldNameLeft" noWrap><asp:label id="lblVisibleStyle" runat="server">显示样式</asp:label></td>
								<td colSpan="7"><asp:radiobuttonlist id="rblVisibleStyle" runat="server" RepeatDirection="Horizontal" AutoPostBack="True"
										DESIGNTIMEDRAGDROP="53" onselectedindexchanged="rblVisibleStyle_SelectedIndexChanged"></asp:radiobuttonlist></td>
							</tr>
							<tr>
								<td class="fieldNameLeft" noWrap><asp:label id="lblChartType" runat="server">图形类型</asp:label></td>
								<td colSpan="4"><asp:radiobuttonlist id="rblChartType" runat="server" RepeatDirection="Horizontal" Enabled="False"></asp:radiobuttonlist></td>
								<td class="fieldName"><INPUT class="submitImgButton" id="cmdQuery" type="submit" value="查 询" name="btnQuery"
										runat="server" onserverclick="cmdQuery_ServerClick">
								</td>
								<td width="100%"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td>
						<table>
							<tr>
								<td colspan="8">
									<cc1:OWCPivotTable id="OWCPivotTable1" runat="server"></cc1:OWCPivotTable></td>
							</tr>
							<tr>
								<td colspan="8">
									<cc1:OWCChartSpace id="OWCChartSpace1" runat="server"></cc1:OWCChartSpace>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr class="normal">
					<td>
						<table height="100%" cellPadding="0" width="100%">
							<tr>
								<TD class="smallImgButton"><INPUT class="gridExportButton" id="cmdGridExport" type="submit" value="  " name="cmdGridExport"
										runat="server" onserverclick="cmdGridExport_ServerClick"> |
								</TD>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
