
<%@ Page Language="c#" CodeBehind="FPageNavigator.aspx.cs" AutoEventWireup="True"
    Inherits="BenQGuru.eMES.Web.FPageNavigator" %>
<%@ Register TagPrefix="uc1" TagName="PageNavigator" Src="PageNavigator.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>FPageNavigator</title>
    <meta name="CODE_LANGUAGE" content="C#"/>
    <meta name="vs_defaultClientScript" content="JavaScript"/>
    <link href="./skin/stylesheet.css" rel="stylesheet"/>
    <script language="javascript" type="text/javascript">
        function CheckCurrentModuleCode(moduleCode) {
            if (document.getElementById("txtModuleCode").value == moduleCode)
                return true;
            return false;
        }
    </script>
</head>
<body ms_positioning="GridLayout" bottommargin="0" leftmargin="0" topmargin="0" rightmargin="0">
    <form id="Form1" method="post" runat="server">
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td style="
                height: 25px; width: 100%">
                &nbsp;&nbsp;&nbsp;<uc1:PageNavigator ID="pageNavigator" runat="server"></uc1:PageNavigator>
                <input type="hidden" runat="server" id="txtModuleCode" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
