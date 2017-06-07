<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FOrgChangePage.aspx.cs" Inherits="BenQuru.eMES.Web.FOrgChangePage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title><% =Title %></title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
	<meta content="C#" name="CODE_LANGUAGE">
	<meta content="JavaScript" name="vs_defaultClientScript">
	<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	<link href='<% =StyleSheet %>' rel="stylesheet">
	<META HTTP-EQUIV="Pragma" CONTENT="no-cache">
	<META HTTP-EQUIV="Cache-Control" CONTENT="no-cache">
	<META HTTP-EQUIV="Expires" CONTENT="0">
	<base target="_self">
    <script language="javascript">
        function page_unload()
        {
            window.returnValue = document.getElementById("HiddenFieldReturnValue").value;
        }
    </script>
</head>
<body style="PADDING-TOP: 8px; PADDING-LEFT: 8px" scroll="no" rightMargin="0" MS_POSITIONING="GridLayout" onunload="page_unload();">
    <form id="form1" runat="server" method="post" >
    <table id="Table1" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
        <tr>
			<td class="ModuleTitle" height="1" style="margin-left:5px;"><asp:label id="lblTitle" Runat="server" CssClass="labeltopic"><% =Title %></asp:label></td>
		</tr>
        <tr class="edit">
            <td style="margin-left:10px;">
                <asp:RadioButtonList CssClass="radiobuttonlist" runat="server" ID="RadioButtonListOrg" RepeatDirection="Vertical" RepeatLayout="Table" OnSelectedIndexChanged="RadioButtonListOrg_SelectedIndexChanged">
                </asp:RadioButtonList>
                <asp:HiddenField runat="server" ID="HiddenFieldReturnValue" />
            </td>
        </tr>
        <tr class="toolBar">
            <td >
                <table class="toolBar" height="100%" width="100%">
					<tr>
						<td class="toolBar"><INPUT class="submitImgButton" id="cmdConfirm" type="submit" value="È· ÈÏ" name="cmdConfirm"
								runat="server" onserverclick="cmdConfirm_ServerClick"></td>
						<td class="toolBar"><INPUT class="submitImgButton" id="cmdExit" type="submit" value="ÍË ³ö" name="cmdExit" onclick="top.close();return false ;"
								runat="server"></td>
					</tr>
				</table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
