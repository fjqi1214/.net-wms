<%@ Register TagPrefix="igtab" Namespace="Infragistics.WebUI.UltraWebTab" Assembly="Infragistics35.WebUI.UltraWebTab.v3, Version=3.0.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Page language="c#" Codebehind="FItem2SpcMP.aspx.cs" AutoEventWireup="True" Inherits="BenQGuru.eMES.Web.MOModel.FSpcMP" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>FSpcMP</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<igtab:ultrawebtab id="tabs" runat="server" ThreeDEffect="False" BorderColor="Gray" BorderWidth="1px"
				BorderStyle="Solid" Height="100%" Width="100%" Font-Size="Smaller">
				<Tabs>
					<igtab:Tab Key="FItem2SPCTblMP" Text="产品采集表维护">
						<Style Font-Size="12px" BorderStyle="None">
						</Style>
						<ContentPane TargetUrl="FItem2SPCTblMP.aspx" BorderStyle="None"></ContentPane>
						<SelectedStyle BorderStyle="None"></SelectedStyle>
					</igtab:Tab>
					<igtab:Tab Key="FItem2SPCTestMP" Text="产品测试项维护">
						<Style Font-Size="12px" BorderStyle="None">
						</Style>
						<ContentPane TargetUrl="FItem2SPCTestMP.aspx" BorderStyle="None"></ContentPane>
						<SelectedStyle BorderStyle="None"></SelectedStyle>
					</igtab:Tab>
				</Tabs>
				<DefaultTabStyle Height="20px" BackColor="WhiteSmoke"></DefaultTabStyle>
				<RoundedImage SelectedImage="ig_tab_lightb2.gif" NormalImage="ig_tab_lightb1.gif" FillStyle="RightMergedWithCenter"></RoundedImage>
				<SelectedTabStyle Font-Bold="True" BackColor="#EBEBEB"></SelectedTabStyle>
			</igtab:ultrawebtab>
		</form>
	</body>
</HTML>
