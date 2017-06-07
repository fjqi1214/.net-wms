<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics35.WebUI.WebDataInput.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="eMESTime.ascx.cs" Inherits="BenQGuru.eMES.Web.UserControl.eMESTime" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<igtxt:WebDateTimeEdit id="txtTimeEditor" runat="server" DisplayModeFormat="T" EditModeFormat="T" Fields="2005-3-21-0-0-0-0"
	Width="150px" CssClass="<%=CssClass%>" Font-Size="12px">
	<Padding Left="5px"></Padding>
</igtxt:WebDateTimeEdit>
