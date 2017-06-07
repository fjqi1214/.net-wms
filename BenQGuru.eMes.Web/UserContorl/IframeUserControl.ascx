<%@ Control Language="C#" AutoEventWireup="true" Codebehind="IframeUserControl.ascx.cs"
    Inherits="BenQuru.eMES.Web.UserContorl.IframeUserControl" %>
<table>
    <tr>
        <td style="text-align:center;">
            <asp:Label ID="lblTitle1" runat="server" CssClass="labeltopic"></asp:Label>
        </td>
        <td style="text-align:center;">
            <asp:Label ID="lblTitle2" runat="server" CssClass="labeltopic"></asp:Label>
        </td>
    </tr>
    <tr>
        <td style="width: 680px; height: 270px">
            <iframe runat="server" id="iframe1" name="iframe1" frameborder="0" src="" style="width: 100%;
                height: 100%;" scrolling="no"></iframe>
        </td>
        <td style="width: 680px; height: 270px">
            <iframe runat="server" id="iframe2" name="iframe2" frameborder="0" src="" style="width: 100%;
                height: 100%;" scrolling="no"></iframe>
        </td>
    </tr>
    <tr>
        <td style="text-align:center;">
            <asp:Label ID="lblTitle3" runat="server" CssClass="labeltopic"></asp:Label>
        </td>
        <td style="text-align:center;">
            <asp:Label ID="lblTitle4" runat="server" CssClass="labeltopic"></asp:Label>
        </td>
    </tr>    
    <tr>
        <td style="width: 680px; height: 270px">
            <iframe runat="server" id="iframe3" name="iframe3" frameborder="0" src="" style="width: 100%;
                height: 100%;" scrolling="no"></iframe>
        </td>
        <td style="width: 680px; height: 270px">
            <iframe runat="server" id="iframe4" name="iframe4" frameborder="0" src="" style="width: 100%;
                height: 100%;" scrolling="no"></iframe>
        </td>
    </tr>
</table>
