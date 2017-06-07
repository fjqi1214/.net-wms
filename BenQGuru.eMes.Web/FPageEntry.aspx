<%@ Page language="c#" Codebehind="FPageEntry.aspx.cs" AutoEventWireup="True" Inherits="BenQuru.eMES.Web.FPageEntry" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>
			<%=Title%>
		</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="<%=StyleSheet%>" rel=stylesheet>
	</HEAD>
	<body bottomMargin="5" leftMargin="0" topMargin="0" scroll="yes" rightMargin="0" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table width="100%" height="100%" border="0" cellpadding="0" cellspacing="0">
				<tr>
					<td align="center" valign="middle"><table width="584" border="0" cellspacing="0" cellpadding="0">
							<tr>
								<td><img src=".\Skin\Image\login_top.jpg" width="584" height="77"></td>
							</tr>
							<tr>
								<td><img src=".\Skin\Image\login_middle.jpg" width="584" height="110"></td>
							</tr>
							<tr>
								<td height="132" valign="top" background=".\Skin\Image\login_botto.jpg"><table width="100%" border="0" cellspacing="0" cellpadding="10">
										<tr>
											<td width="280">&nbsp;</td>
											<td><table border="0" cellspacing="5" cellpadding="0">
													<tr>
														<td colspan="3"><span class="style1"></ASP:BUTTON><asp:label id="lblUserName" runat="server" CssClass="style1">Customer</asp:label></span>
															<asp:Label Runat="server" ID="lblPageSelect"> 登录成功，请选择操作平台：</asp:Label></td>
													</tr>
													<tr>
														<td width="90" height="20"><a href="FStartPage.aspx" border="0"><img src=".\Skin\Image\bg_manage.gif" width="80" height="17" border="0"></a></td>
														<td width="12">&nbsp;</td>
														<td width="100%"><a href="FReportMain.aspx" border="0"><img src=".\Skin\Image\bt_reportcenter.gif" width="80" height="17" border="0"></a></td>
													</tr>
												</table>
											</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
