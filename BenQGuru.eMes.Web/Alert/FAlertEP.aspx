<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="cc2" Namespace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<%@ Page language="c#" Codebehind="FAlertEP.aspx.cs" AutoEventWireup="True" Inherits="BenQGuru.eMES.Web.Alert.FAlertEP" %>
<%@ Register TagPrefix="uc1" TagName="eMESDate" Src="~/UserControl/DateTime/DateTime/eMESDate.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ItemMP</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href="<%=StyleSheet%>" rel=stylesheet>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table height="100%" width="100%">
				<tr class="moduleTitle">
					<td><asp:label id="lblTitle" runat="server" CssClass="labeltopic" Width="104px">预警详细信息</asp:label></td>
				</tr>
				<tr height="25">
					<td>
						<table class="query" height="25" width="100%">
							<tr>
								<td class="fieldNameLeft" noWrap><asp:label id="lblAlertTypeEdit" runat="server">预警类别</asp:label></td>
								<td class="fieldValue"><asp:textbox id="txtAlertType" runat="server" ReadOnly="True" CssClass="textbox" Enabled="False"></asp:textbox></td>
								<td class="fieldNameLeft" noWrap><asp:label id="lblAlertItemEdit" runat="server">预警项次</asp:label></td>
								<td class="fieldValue"><asp:textbox id="txtAlertItem" runat="server" ReadOnly="True" CssClass="textbox" Enabled="False"></asp:textbox></td>
								<td class="fieldNameLeft" noWrap><asp:label id="lblAlertEdit" runat="server">预警</asp:label></td>
								<td class="fieldValue"><asp:textbox id="txtAlertItemValue" runat="server" ReadOnly="True" CssClass="textbox" Enabled="False"></asp:textbox></td>
								<td width="40%"></td>
							</tr>
							<tr>
								<td class="fieldNameLeft" noWrap><asp:label id="lblAlertDate" runat="server">预警日期</asp:label></td>
								<td class="fieldValue"><asp:textbox id="txtAlertDate" runat="server" ReadOnly="True" CssClass="textbox" Enabled="False"></asp:textbox></td>
								<td class="fieldNameLeft" noWrap><asp:label id="lblAlertTime" runat="server">预警时间</asp:label></td>
								<td class="fieldValue"><asp:textbox id="txtAlertTime" runat="server" ReadOnly="True" CssClass="textbox" Enabled="False"></asp:textbox></td>
								<td class="fieldNameLeft" noWrap><asp:label id="lblAlertValue" runat="server">预警数值</asp:label></td>
								<td><asp:textbox id="txtAlertValue" runat="server" ReadOnly="True" CssClass="textbox" Enabled="False"></asp:textbox></td>
								<td width="40%"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr height="25">
					<td>
						<table class="query" height="25" width="100%">
							<tr>
								<td class="fieldNameLeft" noWrap><asp:label id="lblSender" runat="server">发件人</asp:label></td>
								<td class="fieldValue"><asp:textbox id="txtSendUser" runat="server" ReadOnly="True" CssClass="textbox" Enabled="False"></asp:textbox></td>
								<td class="fieldNameLeft" noWrap><asp:label id="lblAlertLevel" runat="server">预警级别</asp:label></td>
								<td class="fieldValue"><asp:dropdownlist id="drpAlertLevel" runat="server" CssClass="dropdownlist" Width="109px"></asp:dropdownlist></td>
								<td class="fieldNameLeft" noWrap><asp:label id="lblAlertStatus" runat="server">预警状态</asp:label></td>
								<td class="fieldValue" style="WIDTH: 108px"><asp:dropdownlist id="drpAlertStatus" runat="server" CssClass="dropdownlist" Width="114px"></asp:dropdownlist></td>
								<td width="25%"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td>
						<table class="query" height="100%" width="100%">
							<tr height="30%">
								<td class="fieldNameLeft" width="10%"><asp:label id="lblAlertMsg" runat="server">预警消息</asp:label></td>
								<td><asp:textbox id="txtAlertMsg" runat="server" ReadOnly="True" CssClass="textarea" Height="100%"
										TextMode="MultiLine" width="100%" Enabled="False"></asp:textbox></td>
								<td width="25%"></td>
							</tr>
							<tr height="30%">
								<td class="fieldNameLeft" width="10%"><asp:label id="lblDesc" runat="server">处理记录</asp:label></td>
								<td><asp:textbox id="txtDesc" runat="server" CssClass="textarea" Height="100%" TextMode="MultiLine"
										Width="100%"></asp:textbox></td>
								<td width="25%"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr class="toolBar">
					<td>
						<table class="toolBar">
							<tr>
								<td class="toolBar"></td>
								<td class="toolBar"><INPUT class="submitImgButton" id="cmdSave" type="submit" value="保 存" name="cmdSave" runat="server" onserverclick="cmdSave_ServerClick"></td>
								<td><INPUT class="submitImgButton" id="cmdReturn" type="submit" value="返 回" name="cmdCancel"
										runat="server" onserverclick="cmdReturn_ServerClick"></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
