<%@ Control Language="C#" AutoEventWireup="true" EnableViewState="true" CodeBehind="ReportSecurity.ascx.cs" Inherits="BenQGuru.eMES.Web.ReportView.ReportSecurity" %>
<script language="javascript">
function MoveUserGroup(fromSelectId, toSelectId)
{
    var i;
    var fromsel = document.getElementById(fromSelectId);
    var tosel = document.getElementById(toSelectId);
    for (i = 0; i < fromsel.options.length; i++)
    {
        if (fromsel.options[i].selected == true)
        {
            var opt = new Option(fromsel.options[i].text, fromsel.options[i].value);
            tosel.options.add(opt);
        }
    }
    for (i = fromsel.options.length - 1; i >= 0; i--)
    {
        if (fromsel.options[i].selected == true)
        {
            fromsel.options.remove(i);
        }
    }
    var str = "";
    var selVal;
    if (fromSelectId.indexOf("lstSelectedUserGroup") >= 0)
    {
        selVal = fromsel;
    }
    else
    {
        selVal = tosel;
    }
    for (i = 0; i < selVal.options.length; i++)
    {
        str = str + selVal.options[i].value + ";";
    }
    var strHid = selVal.id.replace("lstSelectedUserGroup", "hidSelectedValue");
    document.getElementById(strHid).value = str;
}
</script>
<table>
    <tr>
        <td><asp:Label runat="server" ID="lblUnSelectedUserGroup">待选择功能组</asp:Label></td>
        <td></td>
        <td><asp:Label runat="server" ID="lblSelectedUserGroup">已选择功能组</asp:Label></td>
    </tr>
    <tr>
        <td>
            <asp:ListBox runat="server" ID="lstUnSelectedUserGroup" SelectionMode="multiple" Height="300px" Width="200px"></asp:ListBox>
        </td>
        <td valign="middle">
            <img src="../Skin/Image/right_0.gif" runat="server" id="imgSelect" border="0" />
            <br />
            <img src="../Skin/Image/left_0.gif" runat="server" id="imgUnSelect" border="0" />
        </td>
        <td>
            <asp:ListBox runat="server" ID="lstSelectedUserGroup" SelectionMode="multiple" Height="300px" Width="200px"></asp:ListBox>
            <input type=hidden id="hidSelectedValue" runat="server" />
        </td>
    </tr>
</table>