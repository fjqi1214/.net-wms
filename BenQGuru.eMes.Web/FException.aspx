<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FException.aspx.cs" Inherits="BenQuru.eMES.Web.FException" %>
<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>FException</title>
    <link href=<%=StyleSheet%> rel=stylesheet>    
</head>
<body>
    <form id="form1" runat="server">
        <table width="100%">
            <tr>
                <td>
                    <asp:Panel ID="PanelException" runat="server" /> 
                </td>
            </tr>  
        </table>
        <cc1:RefreshController id="RefreshController1" runat="server" Interval="15000"></cc1:RefreshController>
    </form>
</body>
</html>
