<%@ Register TagPrefix="uc1" TagName="eMESDate" Src="~/UserControl/DateTime/DateTime/eMESDate.ascx" %>
<%@ Register TagPrefix="cc2" NameSpace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Page language="c#" Codebehind="FRealTimeDefectQP2.aspx.cs" AutoEventWireup="True" Inherits="BenQGuru.eMES.Web.WebQuery.FRealTimeDefectQP2" %>
<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>FRealTimeQuantitySummaryQP</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href="<%=StyleSheet%>" rel=stylesheet>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table height="100%" width="100%">
				<tr class="moduleTitle">
					<td><table cellpadding="0" cellspacing="0">
							<tr>
								<td><img id="imgTitle" src="../skin/image/ico_arrow.gif" width="15" height="15">
								</td>
								<td><asp:label id="lblRealTimeDefectQP" runat="server" CssClass="Title2">实时缺陷分析</asp:label></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td>
						<table class="query" height="100%" width="100%">
							<tr>
								<td class="fieldNameLeft"><asp:label id="lblSegment" runat="server">工段</asp:label></td>
								<td class="fieldValue"><asp:dropdownlist id="drpSegmentQuery" runat="server" CssClass="require" AutoPostBack="True" Width="150px" onselectedindexchanged="drpSegmentQuery_SelectedIndexChanged"></asp:dropdownlist></td>
								<td class="fieldName" noWrap><asp:label id="lblDate" runat="server">日期</asp:label></td>
								<td class="fieldValue"><uc1:emesdate id="eMESDate1" runat="server" CssClass="require" width="150"></uc1:emesdate></td>
								<td class="fieldName"><asp:label id="lblShip" runat="server">班次</asp:label></td>
								<td class="fieldValue"><asp:dropdownlist id="drpShiftQuery" runat="server" CssClass="require" Width="150px"></asp:dropdownlist></td>
								<td width="100%">
									<cc1:RefreshController id="RefreshController1" runat="server"></cc1:RefreshController>
									<asp:label id="lblToday" runat="server" Visible="False">今天</asp:label>
								</td>
							</tr>
							<tr>
								<td class="fieldNameLeft"><asp:label id="lblStepSequence" runat="server">生产线</asp:label></td>
								<td class="fieldValue"><cc2:selectabletextbox4SS id="txtStepSequence" runat="server" Width="150px" Type="stepsequence"></cc2:selectabletextbox4SS></td>
								<td class="fieldName" noWrap><asp:label id="lblModelCodeQuery" runat="server">产品别代码</asp:label></td>
								<td class="fieldValue"><cc2:selectabletextbox id="txtModelQuery" runat="server" Width="150px" Type="model"></cc2:selectabletextbox></td>
								<td class="fieldName" noWrap><asp:label id="lblItemCodeQuery" runat="server">产品代码</asp:label></td>
								<td class="fieldValue"><cc2:selectabletextbox id="txtItemQuery" runat="server" Width="150px" Type="item"></cc2:selectabletextbox></td>
								<td></td>
							<TR>
								<td class="fieldNameLeft" noWrap><asp:label id="lblMOIDQuery" runat="server">工单代码</asp:label></td>
								<td class="fieldValue"><cc2:selectabletextbox id="txtMoQuery" runat="server" Width="150px" Type="mo"></cc2:selectabletextbox></td>
								<TD class="fieldName" style="DISPLAY:none"><asp:label id="lblFactory" runat="server">工厂</asp:label></TD>
								<td class="fieldValue" style="DISPLAY:none"><asp:dropdownlist id="drpFactory" runat="server" Width="150px"></asp:dropdownlist></td>
								<td class="fieldName" noWrap><asp:label id="lblTop" runat="server">排名前</asp:label></td>
								<td class="fieldValue"><asp:textbox id="txtTop" runat="server" CssClass="textbox" Width="150px">3</asp:textbox></td>
								<td></td>
								<td>
									<asp:CheckBox id="chbRefreshAuto" runat="server" AutoPostBack="True" Text="定时刷新" oncheckedchanged="chkRefreshAuto_CheckedChanged"></asp:CheckBox></td>
								<td><INPUT class="submitImgButton" id="cmdQuery" type="submit" value="查 询" name="btnQuery"
										runat="server" onserverclick="cmdQuery_ServerClick"></td>
							</TR>
						</table>
					</td>
				</tr>
				<tr height="100%">
					<td class="fieldGrid">
						<igtbl:ultrawebgrid id="gridWebGrid" runat="server" Width="100%" Height="100%">
							<DisplayLayout ColWidthDefault="" StationaryMargins="Header" AllowSortingDefault="Yes" RowHeightDefault="20px"
								Version="2.00" SelectTypeRowDefault="Single" SelectTypeCellDefault="Single" HeaderClickActionDefault="SortSingle"
								BorderCollapseDefault="Separate" AllowColSizingDefault="Free" CellPaddingDefault="4" RowSelectorsDefault="No"
								Name="webGrid" TableLayout="Fixed">
								<AddNewBox>
									<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

									</Style>
								</AddNewBox>
								<Pager>
									<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

									</Style>
								</Pager>
								<HeaderStyleDefault BorderWidth="1px" Font-Size="12px" Font-Bold="True" BorderColor="White" BorderStyle="Dashed"
									HorizontalAlign="Left" BackColor="#ABABAB">
									<BorderDetails ColorTop="White" WidthLeft="1px" ColorBottom="White" WidthTop="1px" ColorRight="White"
										ColorLeft="White"></BorderDetails>
								</HeaderStyleDefault>
								<RowSelectorStyleDefault BackColor="#EBEBEB"></RowSelectorStyleDefault>
								<FrameStyle Width="100%" BorderWidth="0px" Font-Size="12px" Font-Names="Verdana" BorderColor="#ABABAB"
									BorderStyle="Groove" Height="100%"></FrameStyle>
								<FooterStyleDefault BorderWidth="0px" BorderStyle="Groove" BackColor="LightGray">
									<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
								</FooterStyleDefault>
								<ActivationObject BorderStyle="Dotted"></ActivationObject>
								<EditCellStyleDefault VerticalAlign="Middle" BorderWidth="1px" BorderColor="Black" BorderStyle="None">
									<Padding Bottom="1px"></Padding>
								</EditCellStyleDefault>
								<RowAlternateStyleDefault BackColor="White"></RowAlternateStyleDefault>
								<RowStyleDefault VerticalAlign="Middle" BorderWidth="1px" BorderColor="#D7D8D9" BorderStyle="Solid"
									HorizontalAlign="Left">
									<Padding Left="3px"></Padding>
									<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
								</RowStyleDefault>
								<ImageUrls ImageDirectory="/ig_common/WebGrid3/"></ImageUrls>
							</DisplayLayout>
							<Bands>
								<igtbl:UltraGridBand></igtbl:UltraGridBand>
							</Bands>
						</igtbl:ultrawebgrid></td>
				</tr>
				<tr>
					<TD class="smallImgButton"><INPUT class="gridExportButton" id="cmdGridExport2" type="submit" value="  " name="cmdGridExport"
							runat="server" onserverclick="cmdGridExport2_ServerClick">
					</TD>
				</tr>
			</table>
		</form>
	</body>
</HTML>
