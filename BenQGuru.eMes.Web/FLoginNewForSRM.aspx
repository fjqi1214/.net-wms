<%@ Page language="c#" Codebehind="FLoginNewForSRM.aspx.cs" AutoEventWireup="True" Inherits="BenQuru.eMES.Web.FLoginNewForSRM" %>
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
		<STYLE type="text/css">TABLE { FONT-SIZE: 12px; FONT-FAMILY: Arial, Helvetica, sans-serif }
	.bg { BACKGROUND-IMAGE: url(Skin/Image/bg_login_bot.gif); WIDTH: 765px; HEIGHT: 191px }
	.inputbox { BORDER-RIGHT: #cccccc 1px solid; BORDER-TOP: #cccccc 1px solid; BORDER-LEFT: #cccccc 1px solid; WIDTH: 141px; BORDER-BOTTOM: #cccccc 1px solid }
	.btover { BORDER-RIGHT: 0px; BORDER-TOP: 0px; FONT-SIZE: 12px; BACKGROUND-IMAGE: url(Skin/Image/buttonlogin_over.gif); BORDER-LEFT: 0px; WIDTH: 69px; PADDING-TOP: 3px; BORDER-BOTTOM: 0px; FONT-FAMILY: Arial, Helvetica, sans-serif; HEIGHT: 19px }
	.btout { BORDER-RIGHT: 0px; BORDER-TOP: 0px; FONT-SIZE: 12px; BACKGROUND-IMAGE: url(Skin/Image/buttonlogin_out.gif); BORDER-LEFT: 0px; WIDTH: 69px; PADDING-TOP: 3px; BORDER-BOTTOM: 0px; BACKGROUND-REPEAT: no-repeat; FONT-FAMILY: Arial, Helvetica, sans-serif; HEIGHT: 19px }
	.txt { FONT-WEIGHT: bold; FONT-SIZE: 12px; COLOR: #003399; FONT-FAMILY: Arial, Helvetica, sans-serif }
		</STYLE>
	</HEAD>
	<body onload="JavaScript:document.all.txtUserCode.focus();" style="BACKGROUND-COLOR: #ffffff">
		<form id="Form1" method="post" runat="server">
			<TABLE height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TBODY>
					<TR>
						<TD vAlign="middle">
							<TABLE cellSpacing="0" cellPadding="0" width="765" align="center" border="0">
								<TBODY>
									<TR>
										<TD><IMG height="290" src="Skin/Image/bg_login_top.jpg" width="765"></TD>
									</TR>
									<TR>
										<TD class="bg" vAlign="top">
											<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
												<TBODY>
													<TR>
														<TD width="400" height="100">&nbsp;</TD>
														<TD>
															<TABLE cellSpacing="5" cellPadding="0" width="100%" border="0">
																<TBODY>
																	<TR>
																		<TD class="txt" vAlign="bottom" width="41"><asp:label id="lblUserCode" runat="server">用户：</asp:label></TD>
																		<TD vAlign="bottom"><asp:textbox id="txtUserCode" runat="server" CssClass="inputbox" MaxLength="40" Width="180px"
																				TabIndex="1"></asp:textbox></TD>
																	</TR>
																	<TR>
																		<TD class="txt" vAlign="bottom"><asp:label id="lblPassword" runat="server">密码：</asp:label></TD>
																		<TD><asp:textbox id="txtPassword" runat="server" CssClass="inputbox" MaxLength="40" Width="180px"
																				TextMode="Password" TabIndex="2"></asp:textbox></TD>
																	</TR>
																	<TR>
																		<TD class="txt" vAlign="bottom"><asp:label id="lblLanguage" runat="server">语言：</asp:label></TD>
																		<TD><SELECT class="inputbox" id="drpLanguage" style="WIDTH: 180px" runat="server" NAME="drpLanguage"
																				tabindex="3">
																				<OPTION value="CHS" selected>简体中文</OPTION>
																				<OPTION value="CHT">繁体中文</OPTION>
																				<OPTION value="ENU">英文</OPTION>
																				<OPTION value=""></OPTION>
																			</SELECT></TD>
																	</TR>
																	<TR>
																		<TD><INPUT id="loguser" type="hidden" name="loguser" runat="server"><INPUT id="logintimes" type="hidden" value="0" name="logintimes" runat="server"></TD>
																		<TD><INPUT type="submit" id="cmdOK" Runat="server" value="登  录" class="submitImgButton" onMouseOut="this.className='bt3out'"
																				onMouseOver="this.className='bt3over'" NAME="cmdOK" onserverclick="cmdOK_ServerClick"> <INPUT class="submitImgButton" id="cmdChangePassword" onclick="onChangePassword()" type="submit"
																				value="修改密码" name="cmdChangePassword" runat="server"></TD>
																	</TR>
																	<tr>
																		<td colspan="2"><br>
																			<asp:label id="lblMessage" runat="server" Width="180px" ForeColor="Red"></asp:label>
																		</td>
																	</tr>
																</TBODY>
															</TABLE>
														</TD>
													</TR>
												</TBODY>
											</TABLE>
										</TD>
									</TR>
								</TBODY>
							</TABLE>
						</TD>
					</TR>
				</TBODY>
			</TABLE>
		</form>
		<script language="javascript">
		function onChangePassword()
		{
			window.showModalDialog("./FUserPassWordModifyMP.aspx","",showDialog(6));
		}	
	
		</script>
	</body>
</HTML>
