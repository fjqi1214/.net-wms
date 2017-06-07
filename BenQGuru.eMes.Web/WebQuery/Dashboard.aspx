<%@ Page language="c#" Codebehind="Dashboard.aspx.cs" AutoEventWireup="True" Inherits="BenQGuru.eMES.Web.WebQuery.Dashboard" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" > 

<html>
  <head>
    <title>Dashboard</title>
    <meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" Content="C#">
    <meta name=vs_defaultClientScript content="JavaScript">
    <meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
    <script language=javascript>
		function Init()
		{
			//window.showModalDialog("DashboardPOP.aspx","","scroll:0;status:0;help:0;resizable:1;dialogWidth:1024px;dialogHeight:758px");
			window.open ('DashboardPOP.aspx','Dashboardwindow','height=708,width=1015,top=0,left=0,toolbar=no,menubar=no,scrollbars=no, resizable=no,location=no, status=no') ;
		}
    </script>
  </head>
  <body MS_POSITIONING="GridLayout" onload="Init()">
	
    <form id="Form1" method="post" runat="server" style="width:100%;height:100%;">
	

     </form>
	
  </body>
</html>
