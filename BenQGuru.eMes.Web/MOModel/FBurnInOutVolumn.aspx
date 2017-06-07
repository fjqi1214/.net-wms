<%@ Page language="c#" Codebehind="FBurnInOutVolumn.aspx.cs" AutoEventWireup="True" Inherits="BenQGuru.eMES.Web.MOModel.FBurnInOutVolumn" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>FBurnInOutVolumn</title>
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
					<td><asp:label id="lblTitle" runat="server" CssClass="labeltopic"> 车位维护</asp:label></td>
				</tr>
				<tr>
					<td>
						<table class="edit">
							<tr>
								<td class="fieldNameLeft" noWrap><asp:label id="lblVolumnEdit" runat="server"> 车位总数</asp:label></td>
								<td class="fieldValue"><asp:textbox id="txtVolumnEdit" runat="server" CssClass="require" Width="120px"></asp:textbox></td>
								<td width="100%"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td height="100%"></td>
				</tr>
				<tr class="toolBar">
					<td>
						<table class="toolBar">
							<tr>
								<td class="toolBar"><INPUT class="submitImgButton" id="cmdSave" type="submit" value="保 存" name="cmdSave" runat="server"></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
