<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FsoftwareVersionImport.aspx.cs" Inherits="BenQGuru.eMES.Web.BaseSetting.FsoftwareVersionImport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>软件版本导入</title>
    <meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
	<meta name="CODE_LANGUAGE" Content="C#">
	<meta name="vs_defaultClientScript" content="JavaScript">
	<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	<link href="<%=StyleSheet%>" rel=stylesheet>
	<script language="javascript">
	
	function CloseWindow()
	{
	    window.returnValue = true;
	    window.close();
	    return false;
	}
	</script>
</head>
<body>
    <form id="form1" runat="server">
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <table height="100%" width="100%" runat="server">
			<tr class="moduleTitle">
				<td><asp:label id="lblTitle" runat="server" CssClass="labeltopic">软件版本导入</asp:label></td>
			</tr>
			<tr class="normal" id="trImport" runat="server">
				<td>
					<table class="edit">
						<tr>
							<TD class="fieldNameLeft" noWrap>
							    <asp:label id="lblInFile" runat="server"> 导入文件：</asp:label></TD>
							<TD style="WIDTH: 300px"><INPUT class="textbox" id="fileInit" style="WIDTH: 454px; HEIGHT: 22px" type="file" size="56" runat="server" enableviewstate="true"></TD>
							<TD class="toolBar"><INPUT class="submitImgButton" id="cmdEnter" type="submit" value="导入" name="btnAdd" runat="server" onserverclick="cmdImport_ServerClick"></TD>
							<TD class="fieldNameLeft" noWrap>
							    <asp:label id="lblInTemplet" runat="server"> 导入模板：</asp:label></TD>
							<td noWrap><a id="aFileDownLoad" style="DISPLAY: none; COLOR: blue" href="" target="_blank" runat="server">
							    <asp:label id="lblDown" runat="server">下载</asp:label></a> <span style="CURSOR: hand; COLOR: blue; TEXT-DECORATION: underline" onclick="DownLoadFile();">
							    <asp:label id="lblDown1" runat="server">下载</asp:label></span></td>	
							<td style="width:100%">&nbsp;</td>   	
						</tr>
					</table>
				</td>
			</tr>
			<tr style="height:100%">
			    <td>
			        &nbsp;
			    </td>
			</tr>
			<tr class="toolbar">
			    <td>
			        <table class="toolBar">
						<tr>
							<td class="toolBar"><INPUT class="submitImgButton" id="cmdCancel" type="submit" value="取 消" name="cmdCancel" runat="server" onclick="return CloseWindow();"></td>
						</tr>
					</table>
			    </td>
			</tr>
		</table>
        </ContentTemplate>
        <Triggers>
        <asp:PostBackTrigger ControlID="cmdEnter"/>
        </Triggers>
    </asp:UpdatePanel>
    </form>
</body>
</html>
