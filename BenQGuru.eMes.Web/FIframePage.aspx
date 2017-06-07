<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FIframePage.aspx.cs" Inherits="BenQuru.eMES.Web.FIframePage" %>
<%@ Register Src="UserContorl/IframeUserControl.ascx" TagName="IframeUserControl" TagPrefix="uc1" %>
<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>FIframePage</title>
    <link href=<%=StyleSheet%> rel=stylesheet>
    <script type="text/javascript" language="javascript">
    </script>
</head>
<body height="100%" width="100%" scroll="auto" style=" background-repeat:no-repeat; background-position: top center; ">
    <form id="form1" runat="server" height="100%" width="100%">
    <div>
    <cc1:RefreshController id="RefreshController1" runat="server" Interval="3600000"></cc1:RefreshController>
    <table cellSpacing="0" cellPadding="0" width="100%" border="0">
    <tr style="width:100%;height:25px;vertical-align:middle;">
        <td>
            <asp:Label ID="lblException" runat="server" CssClass="labeltopic"> Òì³£ </asp:Label>
        </td>
    </tr>
    <tr style="width:100%;height:70px">
        <td colspan="2" >
            <iframe runat="server" id="iframeExpection" name="iframeExpection" frameborder="0" src="" >    
            </iframe>
        </td>
    </tr>
    <tr style="width:100%;height:25px;vertical-align:middle;">
        <td>
            <asp:Label ID="lblAlertNotice" runat="server" CssClass="labeltopic"> Ô¤¾¯ </asp:Label>
        </td>
    </tr>    
    <tr style="width:100%;height:70px">
        <td colspan="2" >
            <iframe runat="server" id="iframeAlertNotice" name="iframeExpection" frameborder="0" src="" >    
            </iframe>
        </td>
    </tr>
    <tr style="width:100%;" >
        <td  style="width:100%;"><uc1:IframeUserControl  id="Iframe"  runat="server"></uc1:IframeUserControl></td>
    </tr>
    </table>    
    </div>
    </form>
</body>
</html>
