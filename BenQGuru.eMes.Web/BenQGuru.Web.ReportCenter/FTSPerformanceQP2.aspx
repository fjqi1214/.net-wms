<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="cc2" NameSpace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<%@ Register TagPrefix="uc1" TagName="eMESDate" Src="~/UserControl/DateTime/DateTime/eMESDate.ascx" %>
<%@ Register TagPrefix="uc2" TagName="UpDown" Src="~/UserControl/NumericUpDown/UCNumericUpDown.ascx" %>
<%@ Page language="c#" Codebehind="FTSPerformanceQP2.aspx.cs" AutoEventWireup="True" Inherits="BenQGuru.eMES.Web.WebQuery.FTSPerformanceQP2" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>FTSRecordQP</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href="<%=StyleSheet%>" rel=stylesheet>
	</HEAD>
	<body scroll="yes" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table height="100%" width="100%">
				<tr class="moduleTitle">
					<td>
						<table cellpadding="0" cellspacing="0">
							<tr>
								<td><img id="imgTitle" src="../skin/image/ico_arrow.gif" width="15" height="15">
								</td>
								<td><asp:label id="lblTSPerformanceQP" runat="server" CssClass="labeltopic">维修绩效查询</asp:label></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td>
						<table class="query" height="100%" width="100%">
							<TR>
								<td class="fieldNameLeft" noWrap><asp:label id="lblModelCodeQuery" runat="server">产品别代码</asp:label></td>
								<td class="fieldValue">
									<cc2:selectabletextbox id="txtConditionModel" runat="server" Width="130px" Type="model"></cc2:selectabletextbox></td>
								<td class="fieldName" noWrap><asp:label id="lblItemCodeQuery" runat="server">产品代码</asp:label></td>
								<td class="fieldValue">
									<cc2:selectabletextbox id="txtConditionItem" runat="server" Width="130px" Type="item"></cc2:selectabletextbox></td>
								<td class="fieldName" noWrap><asp:label id="lblMOIDQuery" runat="server">工单代码</asp:label></td>
								<td class="fieldValue">
									<cc2:selectabletextbox id="txtConditionMo" runat="server" Width="130px" Type="mo"></cc2:selectabletextbox></td>
								<td></td>
							</TR>
							<TR>
								<td class="fieldNameLeft" noWrap><asp:label id="lblRepaireOperationQuery" runat="server">维修站</asp:label></td>
								<td class="fieldValue">
									<cc2:selectabletextbox id="txtConditionResource" runat="server" Width="130px" Type="resource"></cc2:selectabletextbox></td>
								<td class="fieldName" noWrap><asp:label id="lblOperatorQuery" runat="server">维修工</asp:label></td>
								<td class="fieldValue">
									<cc2:selectabletextbox id="txtConditionOperator" runat="server" Width="130px" Type="user"></cc2:selectabletextbox></td>
								<td></td>
								<td></td>
								<td></td>
							</TR>
							<tr>
								<td class="fieldNameLeft" noWrap>
									<asp:label id="lblRepairStartDateQuery" runat="server">维修起始日期</asp:label>
								</td>
								<td class="fieldValue">
									<uc1:eMESDate id="dateStartDateQuery" CssClass="require" width="110" runat="server"></uc1:eMESDate>
								</td>
								<td class="fieldName" noWrap>
									<asp:label id="lblEDateQuery" runat="server">结束日期</asp:label>
								</td>
								<td class="fieldValue">
									<uc1:eMESDate id="dateEndDateQuery" CssClass="require" width="110" runat="server"></uc1:eMESDate>
								</td>
								<td></td>
								<td width="100%"></td>
								<td></td>
							</tr>
							<tr>
								<td class="fieldNameLeft" noWrap><asp:label id="lblSummaryTarget" runat="server">统计对象</asp:label></td>
								<td colspan="6">
									<asp:RadioButtonList id="rblSummaryTargetQuery" runat="server"></asp:RadioButtonList></td>
							</tr>
							<tr>
								<td class="fieldNameLeft" noWrap><asp:label id="lblTopQuery" runat="server">排名筛选</asp:label></td>
								<td colspan="4">
									<uc2:UpDown id="upDown" width="110" runat="server"></uc2:UpDown></td>
								<td></td>
								<td class="fieldName">
									<INPUT class="submitImgButton" id="cmdQuery" type="submit" value="查 询" name="btnQuery"
										runat="server">
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr height="100%">
					<td class="fieldGrid">
						<igtbl:ultrawebgrid id="gridWebGrid" runat="server" Width="100%" Height="150px">
							<DisplayLayout ColWidthDefault="" StationaryMargins="Header" AllowSortingDefault="Yes" RowHeightDefault="20px"
								Version="2.00" SelectTypeRowDefault="Single" SelectTypeCellDefault="Single" HeaderClickActionDefault="SortSingle"
								BorderCollapseDefault="Separate" AllowColSizingDefault="Free" CellPaddingDefault="4" RowSelectorsDefault="No"
								Name="gridWebGrid" TableLayout="Fixed">
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
									BorderStyle="Groove" Height="150px"></FrameStyle>
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
							</DisplayLayout>
							<Bands>
								<igtbl:UltraGridBand></igtbl:UltraGridBand>
							</Bands>
						</igtbl:ultrawebgrid></td>
				</tr>
				<tr class="normal">
					<td>
						<table height="100%" cellPadding="0" width="100%">
							<tr>
								<TD class="smallImgButton"><INPUT class="gridExportButton" id="cmdGridExport" type="submit" value="  " name="cmdGridExport"
										runat="server"> |
								</TD>
								<TD>
									<cc1:pagersizeselector id="pagerSizeSelector" runat="server"></cc1:pagersizeselector>
								</TD>
								<td align="right">
									<cc1:PagerToolbar id="pagerToolBar" runat="server"></cc1:PagerToolbar>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td>
						<table>
							<tr>
								<td colspan="8">
									<cc1:OWCChartSpace id="OWCChartSpace1" runat="server"></cc1:OWCChartSpace>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
