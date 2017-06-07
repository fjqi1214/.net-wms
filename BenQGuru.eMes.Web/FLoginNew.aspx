<%@ Page Language="c#" CodeBehind="FLoginNew.aspx.cs" AutoEventWireup="True" Inherits="BenQuru.eMES.Web.FLoginNew" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>
        <%=Title%>
    </title>
    <link href="<%=StyleSheet%>" rel="stylesheet">
    <style type="text/css">TABLE { FONT-SIZE: 12px; FONT-FAMILY: Arial, Helvetica, sans-serif }
	.bg { BACKGROUND-IMAGE:;background-repeat:repeat-y; WIDTH: 570px; HEIGHT: 190px }
	.inputbox { BORDER-RIGHT: #cccccc 1px solid; BORDER-TOP: #cccccc 1px solid; BORDER-LEFT: #cccccc 1px solid; WIDTH: 141px; BORDER-BOTTOM: #cccccc 1px solid }
	.btover { BORDER-RIGHT: 0px; BORDER-TOP: 0px; FONT-SIZE: 12px; BACKGROUND-IMAGE: url(Skin/Image/buttonlogin_over.gif); BORDER-LEFT: 0px; WIDTH: 69px; PADDING-TOP: 3px; BORDER-BOTTOM: 0px; FONT-FAMILY: Arial, Helvetica, sans-serif; HEIGHT: 19px }
	.btout { BORDER-RIGHT: 0px; BORDER-TOP: 0px; FONT-SIZE: 12px; BACKGROUND-IMAGE: url(Skin/Image/buttonlogin_out.gif); BORDER-LEFT: 0px; WIDTH: 69px; PADDING-TOP: 3px; BORDER-BOTTOM: 0px; BACKGROUND-REPEAT: no-repeat; FONT-FAMILY: Arial, Helvetica, sans-serif; HEIGHT: 19px }
	.bt { BORDER-RIGHT: 0px; BORDER-TOP: 0px; FONT-SIZE: 12px; BACKGROUND-IMAGE: url(Skin/Image/login_button.gif); BORDER-LEFT: 0px; WIDTH: 180px; PADDING-TOP: 3px; BORDER-BOTTOM: 0px; BACKGROUND-REPEAT: no-repeat; FONT-FAMILY: Arial, Helvetica, sans-serif; HEIGHT: 26px }
	.txt { FONT-WEIGHT: bold; FONT-SIZE: 12px; COLOR: #003399; FONT-FAMILY: Arial, Helvetica, sans-serif }
		</style>
    <script src="Scripts/jquery-1.9.1.js" type="text/javascript"></script>
    <script>
        $(function () {
            var height = $(window).height();
            if (height < $(document).height()) {
                height = $(document).height();
            }
            var tableHeight = $("#tableMain").outerHeight();
            $("#tableMain").css("margin-top", height / 2 - tableHeight / 2);
        })
            
    </script>
</head>
<body onload="JavaScript:document.all.txtUserCode.focus();" style="background-color: #FFFFF0;"
    scroll="no">
    <form id="Form1" method="post" runat="server">
    <table cellspacing="0" cellpadding="0" width="570" align="center" border="0" id="tableMain">
        <tbody>
            <tr>
                <td>
                    <img runat="server" id="ImageHead" height="203" src="Skin/Image/Login_banner_emes.jpg"
                        width="570">
                </td>
            </tr>
           
            <tr>
                <td style="background-image: url(Skin/Image/Login_logo.jpg);" class="bg" valign="top"
                    id="ImageLogoTD" runat="server">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tbody>
                            <tr>
                                <td width="310" height="100">
                                    &nbsp;
                                </td>
                                <td>
                                    <table cellspacing="5" cellpadding="0" width="260px" border="0">
                                        <tbody>
                                            <tr>
                                                <td valign="bottom" align="right">
                                                    <asp:Label ID="lblUserCode" runat="server">用户</asp:Label>
                                                </td>
                                                <td valign="bottom" colspan="3">
                                                    <asp:TextBox ID="txtUserCode" runat="server" CssClass="inputbox" MaxLength="40" Width="180px"
                                                        TabIndex="1"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="bottom" align="right">
                                                    <asp:Label ID="lblPassword" runat="server">密码</asp:Label>
                                                </td>
                                                <td colspan="3">
                                                    <asp:TextBox ID="txtPassword" runat="server" CssClass="inputbox" MaxLength="40" Width="180px"
                                                        TextMode="Password" TabIndex="2"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr style="display:none">
                                                <td valign="bottom" align="right">
                                                    <asp:Label ID="lblValidationCode" runat="server">验证码</asp:Label>
                                                </td>
                                                <td valign="bottom" colspan="3">
                                                    <asp:TextBox ID="txtValidationCode" runat="server" CssClass="inputbox" MaxLength="40"
                                                        Width="180px" TabIndex="2"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <img src="ValidationCode.ashx" alt="验证码" style="border: 1px solid #CCCCCC; height: 17px;" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="bottom" align="right">
                                                    <asp:Label ID="lblLanguage" runat="server">语言</asp:Label>
                                                </td>
                                                <td colspan="3">
                                                    <asp:DropDownList ID="drpLanguageNew" Style="width: 180px" runat="server" NAME="drpLanguage"
                                                        AutoPostBack="True" TabIndex="3" OnSelectedIndexChanged="drpLanguageNew_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="bottom" nowrap="nowrap" align="right">
                                                    <asp:Label ID="lblDatabase" runat="server">数据库</asp:Label>
                                                </td>
                                                <td colspan="3">
                                                    <select class="inputbox" id="dprDatabase" style="width: 180px" runat="server" name="dprDatabase"
                                                        tabindex="4">
                                                        <option value="MES" selected>生产</option>
                                                        <option value="HIS">历史</option>
                                                    </select>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <input id="loguser" type="hidden" name="loguser" runat="server"><input id="logintimes"
                                                        type="hidden" value="0" name="logintimes" runat="server">
                                                </td>
                                                <td colspan="3">
                                                    <asp:Button runat="server" ID="cmdSubmit" Width="180px" Text="登录" CssClass="bt"
                                                        OnClick="cmdOK_ServerClick" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td colspan="3">
                                                    <asp:Label ID="lblMessage" runat="server" Width="180px" ForeColor="Red"></asp:Label>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
             <tr>
                <td>
                    <img height="20" id="ImageCenter" runat="server" src="Skin/Image/Login_copyright_bg_text2.jpg"
                        width="570">
                </td>
            </tr>
        </tbody>
    </table>
    </form>
    <script language="javascript">
        function onChangePassword() {
            window.showModalDialog("./FUserPassWordModifyMP.aspx", "", showDialog(6));
        }	
	
    </script>
</body>
</html>
