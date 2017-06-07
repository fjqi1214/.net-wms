<%@ Page language="c#" Codebehind="FTSLocECodeQP2.aspx.cs" AutoEventWireup="True" Inherits="BenQGuru.eMES.Web.WebQuery.FTSLocECodeQP2" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="uc1" TagName="eMESDate" Src="~/UserControl/DateTime/DateTime/eMESDate.ascx" %>
<%@ Register TagPrefix="cc2" NameSpace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>FTSLocECodeQP</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href="<%=StyleSheet%>" rel=stylesheet>
	</HEAD>
	<body scroll="yes" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table height="100%" width="100%">
				<tr class="moduleTitle">
					<td><table cellpadding="0" cellspacing="0">
							<tr>
								<td><img id="imgTitle" src="../skin/image/ico_arrow.gif" width="15" height="15">
								</td>
								<td><asp:label id="lblTSLocECodeQP" runat="server" CssClass="Title2">不良位置原因分析</asp:label></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td>
						<table class="query" height="100%" width="100%">
							<TR>
								<td class="fieldNameLeft" noWrap><asp:label id="lblItemCodeQuery" runat="server">产品代码</asp:label></td>
								<td class="fieldValue"><cc2:selectabletextbox id="txtItemCodeQuery" runat="server" Type=" " Target=" "></cc2:selectabletextbox></td>
								<td class="fieldName" noWrap><asp:label id="lblMOIDQuery" runat="server">工单代码</asp:label></td>
								<td class="fieldValue"><cc2:selectabletextbox id="txtMoCodeQuery" runat="server" Type=" " Target=" "></cc2:selectabletextbox></td>
								<td class="fieldName" noWrap style="DISPLAY:none"><asp:label id="lblFactoryCode" runat="server">工厂</asp:label></td>
								<td class="fieldValue" style="DISPLAY:none"><asp:DropDownList ID="dFactoryCode" width="130" Runat="server" CssClass="require"></asp:DropDownList></td>
								<td width="100%" colspan="3"></td>
								<td></td>
							</TR>
							<TR>
								<td class="fieldNameLeft" noWrap><asp:label id="lblStartDateQuery" runat="server">起始日期</asp:label></td>
								<td class="fieldValue"><uc1:emesdate id="dateStartDateQuery" CssClass="require" runat="server" width="130"></uc1:emesdate></td>
								<td class="fieldName" noWrap><asp:label id="lblEndDateQuery" runat="server">结束日期</asp:label></td>
								<td class="fieldValue"><uc1:emesdate id="dateEndDateQuery" CssClass="require" runat="server" width="130"></uc1:emesdate></td>
								<td class="fieldNameLeft" noWrap><asp:label id="lblVisibleStyle" runat="server">显示样式</asp:label></td>
								<td width="100%"><asp:radiobuttonlist id="rblVisibleStyle" runat="server" RepeatDirection="Horizontal"></asp:radiobuttonlist></td>
								<td></td>
								<td class="fieldName"><INPUT class="submitImgButton" id="cmdQuery" type="submit" value="查 询" name="btnQuery"
										runat="server" onserverclick="cmdQuery_ServerClick"></td>
							</TR>
						</table>
					</td>
				</tr>
				<tr height="100%">
					<td valign="top">
						<table>
							<tr>
								<td><cc1:owcpivottable id="OWCPivotTable1" runat="server"></cc1:owcpivottable></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr height="100%">
					<td valign="top">
						<table>
							<tr>
								<td>
									<cc1:OWCChartSpace id="OWCChartSpace1" runat="server"></cc1:OWCChartSpace>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
