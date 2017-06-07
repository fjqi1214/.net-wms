<%@ Control Language="c#" AutoEventWireup="false" Codebehind="PagerToolBar.ascx.cs" Inherits="BenQGuru.eMES.Web.UserControl.PagerToolBar" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" border="0">
	<TR>
		<TD class="fieldName" nowrap width="80">行数:<%= RowCount%></TD>
		<TD class="fieldName" nowrap width="80">
			<%= PageIndex%>
			/<%= PageCount%>
		</TD>
		<TD class="fieldName" nowrap>
			<asp:LinkButton id="linkPagePrev" runat="server">前页</asp:LinkButton></TD>
		<TD class="fieldName" nowrap>
			<asp:LinkButton id="linkPageNext" runat="server">后页</asp:LinkButton></TD>
		<TD class="fieldName" nowrap>跳转</TD>
		<TD class="fieldValue" nowrap><asp:DropDownList id="listPageIndex" runat="server" AutoPostBack="True" Width="60px"></asp:DropDownList></TD>
	</TR>
</TABLE>
