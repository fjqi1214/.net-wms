<%@ Page language="c#" Codebehind="FErrorPage.aspx.cs" AutoEventWireup="True" Inherits="BenQGuru.eMES.Web.FErrorPage" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>FErrorPage</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href="<%=StyleSheet%>" rel=stylesheet>
		<script language="javascript" event="onclick" for="cmdDetail">
			if ( document.getElementById("tdDetail").innerText == "" )
			{
				document.getElementById("tdDetail").innerText = document.getElementById("message").value;
			}
			else
			{
				document.getElementById("tdDetail").innerText = "";
			}
		</script>
		<script language="javascript">
		function Back()
		{
			try
			{
			    window.name+='[back]';
				window.parent.history.back(-1);
			}
			catch(e)
			{
				window.parent.history.back(0);
			}
			
			return false;
		}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table class="error" align="center">
				<TR height="50%">
					<TD width="30%"></TD>
					<TD></TD>
					<TD width="30%"></TD>
				</TR>
				<tr height="80">
					<td width="30%"></td>
					<td vAlign="bottom" align="center"><asp:label id="lblError" runat="server"></asp:label></td>
					<TD width="30%"></TD>
				</tr>
				<tr style="PADDING-TOP: 8px" vAlign="top" height="20">
					<td width="30%"></td>
					<td><INPUT class="submitImgButton" id="cmdDetail" style="WIDTH: 56px; HEIGHT: 24px" type="button"
							value="详细信息" name="cmdDetail"  runat="server"></td>
					<TD width="30%"></TD>
				</tr>
				<tr height="100">
					<td width="30%"></td>
					<td style="PADDING-LEFT: 16px" id="tdDetail" vAlign="top">
					</td>
					<TD width="30%"></TD>
				</tr>
				<TR align="center" height="50%">
					<TD width="30%"></TD>
					<TD vAlign="top"> <INPUT class="submitImgButton" id="cmdSure" style="BACKGROUND-POSITION:center; BACKGROUND-IMAGE: url(.\Skin\Image\Projectback.gif); BACKGROUND-REPEAT: no-repeat"
							type="button" value="确定" name="cmdSure" runat="server" visible="false" onserverclick="cmdSure_ServerClick">
						<INPUT class="submitImgButton" id="cmdReturn" style="BACKGROUND-POSITION: left center; BACKGROUND-IMAGE: url(.\Skin\Image\Projectback.gif); BACKGROUND-REPEAT: no-repeat"
							onclick="Back()" type="button" value="返 回" name="cmdBack" runat="server"><div id="SplitSpace" runat="server">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
						&nbsp;</div> <INPUT class="submitImgButton" id="cmdUploadError" style="BACKGROUND-POSITION: left center; BACKGROUND-IMAGE: url(.\Skin\Image\uploaderror.gif); BACKGROUND-REPEAT: no-repeat"
							type="button" value="   提交管理员" name="cmdUploadError" runat="server" onserverclick="cmdUploadError_ServerClick"></TD>
					<TD width="30%"></TD>
				</TR>
			</table>
			<INPUT id="message" type="hidden" value="<%= InnerMessage %>">
		</form>
	</body>
</HTML>
