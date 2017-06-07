<%@ Page language="c#" Codebehind="FLogin.aspx.cs" AutoEventWireup="True" Inherits="BenQGuru.eMES.Web.FLogin" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>
			<%=Title%>
		</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href="<%=StyleSheet%>" rel=stylesheet>
	</HEAD>
	<body bottomMargin="5" leftMargin="0" topMargin="0" scroll="yes" onload="JavaScript:document.all.txtUserCode.focus();"
		rightMargin="0" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td vAlign="middle" align="center">
						<table height="426" cellSpacing="0" cellPadding="0" width="614" align="center" background=".\Skin\Image\BS_login.jpg"
							border="0">
							<tr>
								<td width="206" height="132"></td>
								<td width="408" height="132"></td>
							</tr>
							<tr>
								<td height="110"></td>
								<td vAlign="bottom" align="left"></td>
							</tr>
							<tr>
								<td width="206" height="184"></td>
								<td align="center" width="408" height="184">
									<TABLE height="50" cellSpacing="0" cellPadding="0" width="200" border="0">
										<TR>
											<TD align="center" colSpan="2"></TD>
										</TR>
										<TR>
											<TD class="fieldNameLeft" style="HEIGHT: 31px" noWrap><asp:label id="lblUserCode" runat="server" Font-Bold="True">用户</asp:label></TD>
											<TD style="PADDING-BOTTOM: 4px; PADDING-TOP: 4px; HEIGHT: 31px"><asp:textbox id="txtUserCode" runat="server" CssClass="textbox" MaxLength="40" Width="180px"></asp:textbox></TD>
										</TR>
										<TR>
											<TD class="fieldNameLeft" noWrap><asp:label id="lblPassword" runat="server" Font-Bold="True">密码</asp:label></TD>
											<TD style="PADDING-BOTTOM: 4px"><asp:textbox id="txtPassword" runat="server" CssClass="textbox" MaxLength="40" Width="180px"
													TextMode="Password"></asp:textbox></TD>
										</TR>
										<TR>
											<TD class="fieldNameLeft" noWrap><asp:label id="lblLanguage" runat="server" Font-Bold="True">语言</asp:label></TD>
											<TD style="PADDING-BOTTOM: 4px"><SELECT class="textbox" id="drpLanguage" style="WIDTH: 180px" runat="server">
													<OPTION value="CHS" selected>简体中文</OPTION>
													<OPTION value="CHT">繁体中文</OPTION>
													<OPTION value="ENU">英文</OPTION>
													<OPTION value=""></OPTION>
												</SELECT></TD>
										<TR>
											<TD></TD>
											<TD>
												<TABLE height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
													<TR>
														<TD style="PADDING-RIGHT: 4px" align="center" width="90"><INPUT class="submitImgButton" id="cmdOK" onmouseover="document.getElementById('cmdLogin').style.backgroundImage='url(&quot;../../Skin/Image/button1.gif&quot;)';"
																onmouseout="document.getElementById('cmdLogin').style.backgroundImage='url(&quot;../../Skin/Image/button2.gif&quot;)';" type="submit" value="登 录"
																name="cmdOK" runat="server" onserverclick="cmdOK_ServerClick"></TD>
														<TD align="center" width="90"><INPUT class="submitImgButton" id="cmdChangePassword" onclick="onChangePassword()" type="submit"
																value="修改密码" name="cmdCancel" runat="server"></TD>
														<td width="100%"><INPUT id="loguser" type="hidden" name="loguser" runat="server"><INPUT id="logintimes" type="hidden" value="0" name="logintimes" runat="server"></td>
													</TR>
												</TABLE>
											</TD>
										</TR>
										<TR>
											<TD height="20"></TD>
											<TD align="center" height="20"><asp:label id="lblMessage" runat="server" Width="180px" ForeColor="Red"></asp:label></TD>
										</TR>
									</TABLE>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
		<script language="javascript">
		function onChangePassword()
		{
			window.showModalDialog("./FUserPassWordModifyMP.aspx","",showDialog(6));
		}	
	
		</script>
	</body>
</HTML>
