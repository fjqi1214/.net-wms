<%@ Register TagPrefix="ignav" Namespace="Infragistics.WebUI.UltraWebNavigator" Assembly="Infragistics35.WebUI.UltraWebNavigator.v3.2, Version=3.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Page language="c#" Codebehind="FConfigTree.aspx.cs" AutoEventWireup="True" Inherits="BenQGuru.eMES.Web.MOModel.FConfigTree" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>FConfigTree</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href="<%=StyleSheet%>" rel=stylesheet>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 8px; POSITION: absolute; TOP: 8px" height="100%"
				width="100%">
				<tr>
					<TD class="tree" width="300"><ignav:ultrawebtree id="ConfigTreeView" runat="server" Width="100%" Height="100%" WebTreeTarget="ClassicTree"
							Font-Size="12px" Cursor="Hand" Indentation="20" ImageDirectory="/ig_common/WebNavigator31/" CollapseImage="ig_treeMinus.gif"
							ExpandImage="ig_treePlus.gif" TargetFrame="ConfigFrame" AutoPostBack="True">
							<SelectedNodeStyle ForeColor="White" BackColor="Navy"></SelectedNodeStyle>
							<Levels>
								<ignav:Level Index="0"></ignav:Level>
								<ignav:Level Index="1"></ignav:Level>
							</Levels>
							<AutoPostBackFlags NodeExpanded="False" NodeDropped="False" NodeCollapsed="False"></AutoPostBackFlags>
						</ignav:ultrawebtree></TD>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
