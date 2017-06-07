<%@ Control Language="c#" AutoEventWireup="True" Codebehind="eMESDate.ascx.cs" Inherits="BenQGuru.eMES.Web.UserControl.eMESDate" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<script runat="server">
    protected void TextBox1_Load(object sender, EventArgs e)
    {
        GuruDate.Attributes.Add("readonly", "readonly");
    }
</script>
<LINK href="../Css/EHR.css" type="text/css" rel="stylesheet">
<script src="<%=GetCalendarJsFileUrl()%>"></script>
<script src="<%=GetDateCheckJsFileUrl()%>"></script>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" border="0">
	<TR>
		<TD vAlign="middle" noWrap><asp:textbox id="GuruDate" runat="server" Width="92px" CssClass="<%= CssClass%>" ></asp:textbox>
			<IMG id="img1" style="CURSOR: hand" onclick="calendar1(<%= GuruDate.ClientID %>, <%= Enable %>)" src="<%=GetBaseUrl()%>skin/Image/clock.gif"
				 align="absMiddle"></TD>
	</TR>
</TABLE>
