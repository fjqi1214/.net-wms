<%@ Page language="c#" Codebehind="SelectorableTextBoxTest.aspx.cs" AutoEventWireup="false" Inherits="BenQGuru.eMES.Web.WebQuery.SelectorableTextBoxTest" %>
<%@ Register TagPrefix="uc1" NameSpace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Test</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<uc1:SelectableTextBox id="txtOP" runat="server" Type="operation"></uc1:SelectableTextBox>
			-}<uc1:SelectableTextBox4MO id="txtMO" runat="server" Type="mo" StartTime="20050505" EndTime="20050605"></uc1:SelectableTextBox4MO>
			<uc1:SelectableTextBox id="txtItem" runat="server" Type="item"></uc1:SelectableTextBox>
			<uc1:SelectableTextBox id="txtStepSequence" runat="server" Type="stepsequence"></uc1:SelectableTextBox>
			<uc1:SelectableTextBox id="txtSegment" runat="server" Type="segment"></uc1:SelectableTextBox>
			<uc1:SelectableTextBox id="txtModel" runat="server" Type="model"></uc1:SelectableTextBox>
			<uc1:SelectableTextBox id="txtResource" runat="server" Type="resource"></uc1:SelectableTextBox>
			-}<uc1:SelectableTextBox4SS id="Selectabletextbox1" runat="server" Type="stepsequence" Segment="ABC"></uc1:SelectableTextBox4SS>
			<asp:Button id="Button1" style="Z-INDEX: 101; LEFT: 24px; POSITION: absolute; TOP: 320px" runat="server"
				Text="Button"></asp:Button>
		</form>
	</body>
</HTML>
