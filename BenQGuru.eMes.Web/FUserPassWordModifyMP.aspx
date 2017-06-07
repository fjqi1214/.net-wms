<%@ Page language="c#" Codebehind="FUserPassWordModifyMP.aspx.cs" AutoEventWireup="True" Inherits="BenQGuru.eMES.Web.FUserPassWordModifyMP" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ChangePassWord</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href="<%=StyleSheet%>" rel=stylesheet>
		<META HTTP-EQUIV="Pragma" CONTENT="no-cache">
		<META HTTP-EQUIV="Cache-Control" CONTENT="no-cache">
		<META HTTP-EQUIV="Expires" CONTENT="0">
		<base target="_self">
	</HEAD>
	<body style="PADDING-RIGHT: 8px; PADDING-BOTTOM: 8px" scroll="no" rightMargin="0" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table id="Table1" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="ModuleTitle" height="1"><asp:label id="lblUserPassWordTitle" Runat="server" CssClass="labeltopic">密码修改</asp:label></td>
				</tr>
				<tr class="normal" height="100%">
					<td vAlign="top">
						<table class="edit" id="Table2" height="100%" cellPadding="0" width="100%">
							<tr>
								<td class="fieldNameLeft" noWrap><asp:label id="lblUserNameEdit" Runat="server">用户名</asp:label></td>
								<td class="fieldValue"><asp:textbox id="txtUserCode" Runat="server" CssClass="textbox"></asp:textbox></td>
								<td></td>
							</tr>
							<tr>
								<td class="fieldNameLeft" noWrap><asp:label id="lblOriginPassword" Runat="server">旧密码</asp:label></td>
								<td class="fieldValue"><INPUT id="txtOriginPassword" type="password" runat="server" class="textbox"></td>
								<td noWrap><asp:checkbox id="chbForgetPassword" Runat="server" Text="忘记密码" AutoPostBack="True" oncheckedchanged="chkForgetPassword_CheckedChanged"></asp:checkbox></td>
							</tr>
							<tr>
								<td class="fieldNameLeft" style="WIDTH: 10%" noWrap><asp:label id="lblNewPassword" Runat="server">新密码</asp:label></td>
								<td class="fieldValue" style="WIDTH: 60%"><INPUT id="txtNewPassword" type="password" runat="server" class="textbox"></td>
								<td style="WIDTH: 10%"><font color="red"></font></td>
							</tr>
							<tr>
								<td class="fieldNameLeft" noWrap><asp:label id="lblConfirmPassword" Runat="server">密码确认</asp:label></td>
								<td class="fieldValue"><INPUT id="txtConfrimPassword" type="password" runat="server" class="textbox"></td>
								<td></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr style="PADDING-TOP: 2px">
					<td>
						<table id="tblAdmin" runat="server" height="100%" cellPadding="0" width="100%">
							<tr>
								<td class="ModuleTitle"><asp:label id="lblModifyPassword" Runat="server" CssClass="labeltopic">修改密码</asp:label></td>
							</tr>
							<tr>
								<td>
									<table class="edit" height="100%" cellPadding="0" width="100%">
										<tr>
											<td class="fieldNameLeft" noWrap style="WIDTH: 10%"><asp:label id="lblAdminUserCode" Runat="server">管理员</asp:label></td>
											<td class="fieldValue" style="WIDTH: 60%"><asp:textbox id="txtAdminUserCode" Runat="server" CssClass="textbox"></asp:textbox></td>
											<td style="WIDTH: 10%"></td>
										</tr>
										<tr>
											<td class="fieldNameLeft" noWrap><asp:label id="lblUserPasswordEdit" Runat="server">密码</asp:label></td>
											<td class="fieldValue"><INPUT id="txtAdminPassword" type="password" runat="server" class="textbox" NAME="txtAdminPassword"></td>
											<td></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr class="toolBar">
					<td>
						<table class="toolBar" height="100%" width="100%">
							<tr>
								<td class="toolBar"><INPUT class="submitImgButton" id="cmdConfirm" type="submit" value="确 认" name="cmdConfirm"
										runat="server" onserverclick="cmdConfirm_ServerClick"></td>
								<td class="toolBar"><INPUT class="submitImgButton" id="cmdExit" type="submit" value="退 出" name="cmdExit" onclick="top.close();return false ;"
										runat="server"></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
