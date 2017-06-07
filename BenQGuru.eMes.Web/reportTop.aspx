<%@ Page language="c#" Codebehind="reportTop.aspx.cs" AutoEventWireup="True" Inherits="BenQuru.eMES.Web.reportTop" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>reportTop</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="<%=StyleSheet%>" type=text/css rel=stylesheet>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0" ID="tblTop">
				<tr>
					<td>
						<table class="Header_bborder" cellSpacing="0" cellPadding="0" width="100%" bgColor="#b72025"
							border="0" ID="Table2">
							<tr>
								<td><IMG src="skin/image/logo.jpg"></td>
								<td class="Header"><asp:label id="lblDepartmentName" runat="server" CssClass="welcome">DepartmentName</asp:label>&nbsp;&nbsp;
									<asp:label id="lblEmesSystem" runat="server">达方制造执行管理系统</asp:label></td>
								<td><IMG src="skin/image/ol.jpg"></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
