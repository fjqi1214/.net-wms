<%@ Page language="c#" Codebehind="FItem2Config.aspx.cs" AutoEventWireup="True" Inherits="BenQGuru.eMES.Web.MOModel.FItem2Config" %>
<%@ Register TagPrefix="ignav" Namespace="Infragistics.WebUI.UltraWebNavigator" Assembly="Infragistics35.WebUI.UltraWebNavigator.v3.2, Version=3.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>FItem2Config</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href="<%=StyleSheet%>" rel=stylesheet>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		
			<FORM id="Form1" method="post" runat="server">
				<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 8px; POSITION: absolute; TOP: 8px" height="100%"
					width="100%"> <!--this is for tilte-->
					<TR height="20">
						<TD>
							<asp:label id="lblItemConfigure" runat="server" CssClass="labeltopic"> ≤˙∆∑≈‰÷√</asp:label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</TD>
					</TR> <!--this is for tilte-->
					<TR>
						<TD>
							<TABLE id="Table2" height="98%" width="100%">
								<TR>
									<TD width="300">
										<iframe id="ConfigTreeFrame" src="FConfigTree.aspx" style="WIDTH:100%;HEIGHT:100%" frameborder="0">
										</iframe>
									<td width="76%">
										<iframe id="ConfigFrame" src="" style="WIDTH:100%;HEIGHT:100%" frameborder="0"></iframe>
									</td>
								</TR>
							</TABLE>
						</TD>
					</TR>
				</TABLE>
			</FORM>
		
	</body>
</HTML>
