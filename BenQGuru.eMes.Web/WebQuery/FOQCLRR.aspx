<%@ Page language="c#" Codebehind="FOQCLRR.aspx.cs" AutoEventWireup="True" Inherits="BenQGuru.eMES.Web.WebQuery.FOQCLRR" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="uc1" TagName="eMesTime" Src="../UserControl/DateTime/DateTime/eMesTime.ascx" %>
<%@ Register TagPrefix="uc1" TagName="eMesDate" Src="../UserControl/DateTime/DateTime/eMesDate.ascx" %>
<%@ Register TagPrefix="cc2" NameSpace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register Src="UserControls/UCLineChartProcess.ascx" TagName="UCLineChartProcess" TagPrefix="uc4"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>FINNOInfoQP</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href="<%=StyleSheet%>" rel=stylesheet>
		<script language="javascript">
			function onCheckBoxChange(cb)
			{
				if(cb.id == "cbdetail")
				{
					document.all.cbsample.checked = false;
				}
				else if(cb.id == "cbsample")
				{
					document.all.cbdetail.checked = false;
				}
			}
		</script>
	</HEAD>
	<body scroll="yes" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table id="Table1" height="100%" width="100%">
				<tr class="moduleTitle">
					<td style="HEIGHT: 19px"><asp:label id="lblTitle" runat="server" CssClass="labeltopic">OQC LRR</asp:label></td>
				</tr>
				<tr height="80">
					<td>
						<table class="query" id="Table2" height="100%" width="100%">
							<TR>
								<td class="fieldNameLeft" noWrap><asp:label id="lblItemCodeQuery" runat="server">产品代码</asp:label></td>
								<td class="fieldValue"><cc2:selectabletextbox id="txtConditionItem" runat="server" Type="item" Width="90px"></cc2:selectabletextbox></td>
								<td class="fieldName" noWrap><asp:label id="lblModelCodeQuery" runat="server">产品别代码</asp:label></td>
								<td class="fieldValue"><cc2:selectabletextbox id="txtConditionModel" runat="server" Type="model" Width="90px"></cc2:selectabletextbox></td>
								<td class="fieldName" noWrap><asp:label id="lblDateGroup" runat="server">统计范围</asp:label></td>
								<td class="fieldValue"><asp:dropdownlist id="drpDateGroup" runat="server" Width="120">
										<asp:ListItem Value="MDATE">天</asp:ListItem>
										<asp:ListItem Value="WEEK">周</asp:ListItem>
										<asp:ListItem Value="MONTH">月</asp:ListItem>
									</asp:dropdownlist></td>
								<td></td>
							</TR>
							<tr>
								<td class="fieldName" noWrap><asp:label id="lblOQCBegindate" runat="server">抽检起始日期</asp:label></td>
								<TD class="fieldValue" noWrap>
									<TABLE cellSpacing="0" cellPadding="0" border="0">
										<TR>
											<TD>                                            
                                                <asp:TextBox  id="txtOQCBeginDate"  class='datepicker require' runat="server"  Width="80px"/></TD>
											<TD><uc1:emestime id="txtOQCBeginTime" runat="server" CssClass="require" width="80"></uc1:emestime></TD>
										</TR>
									</TABLE>
								</TD>
								<td class="fieldName" noWrap><asp:label id="lblOQCEnddt" runat="server">抽检结束日期</asp:label></td>
								<TD class="fieldValue" noWrap>
									<TABLE cellSpacing="0" cellPadding="0" border="0">
										<TR>
											<TD>                                           
<asp:TextBox  id="txtOQCEndDate"  class='datepicker require' runat="server"  Width="80px"/>
</TD>
											<TD><uc1:emestime id="txtOQCEndTime" runat="server" CssClass="require" width="80"></uc1:emestime></TD>
										</TR>
									</TABLE>
								</TD>
								<td class="fieldName" noWrap><asp:label id="lblTypeQuery" runat="server">统计类型</asp:label></td>
								<td class="fieldValue"><asp:dropdownlist id="drpType" runat="server" Width="120">
										<asp:ListItem Value="NORMAL">一次送检</asp:ListItem>
										<asp:ListItem Value="RELOT">重检</asp:ListItem>
										<asp:ListItem Value="REWORKLOT">返工送检</asp:ListItem>
										<asp:ListItem Value="TRYLOT">非量产送检</asp:ListItem>
									</asp:dropdownlist></td>
								<td></td>
								<td class="fieldName"><INPUT class="submitImgButton" id="cmdQuery" type="submit" value="查 询" name="btnQuery"
										runat="server">
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr height="150">
					<td class="fieldGrid" vAlign="top"><igtbl:ultrawebgrid id="gridWebGrid" runat="server" Width="100%" Height="150px">
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
									<ImageUrls ImageDirectory="/ig_common/WebGrid3/"></ImageUrls>
								</DisplayLayout>
								<Bands>
									<igtbl:UltraGridBand></igtbl:UltraGridBand>
								</Bands>
							</igtbl:ultrawebgrid><DISPLAYLAYOUT ColWidthDefault="" StationaryMargins="Header" AllowSortingDefault="Yes" RowHeightDefault="20px"
							Version="2.00" SelectTypeRowDefault="Single" SelectTypeCellDefault="Single" HeaderClickActionDefault="SortSingle" BorderCollapseDefault="Separate"
							AllowColSizingDefault="Free" CellPaddingDefault="4" RowSelectorsDefault="No" Name="webGrid" TableLayout="Fixed"><ADDNEWBOX>
								<STYLE BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
								</STYLE>
							</ADDNEWBOX>
							<PAGER>
								<STYLE BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
								</STYLE>
							</PAGER>
							<HEADERSTYLEDEFAULT BorderWidth="1px" BorderStyle="Dashed" BackColor="#ABABAB" Font-Size="12px" Font-Bold="True"
								BorderColor="White" HorizontalAlign="Left">
								<BORDERDETAILS ColorTop="White" WidthLeft="1px" ColorBottom="White" WidthTop="1px" ColorRight="White"
									ColorLeft="White"></BORDERDETAILS>
							</HEADERSTYLEDEFAULT>
							<ROWSELECTORSTYLEDEFAULT BackColor="#EBEBEB"></ROWSELECTORSTYLEDEFAULT>
							<FRAMESTYLE Width="100%" Height="100%" BorderWidth="0px" BorderStyle="Groove" Font-Size="12px"
								BorderColor="#ABABAB" Font-Names="Verdana"></FRAMESTYLE>
							<FOOTERSTYLEDEFAULT BorderWidth="0px" BorderStyle="Groove" BackColor="LightGray">
								<BORDERDETAILS ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BORDERDETAILS>
							</FOOTERSTYLEDEFAULT>
							<ACTIVATIONOBJECT BorderStyle="Dotted"></ACTIVATIONOBJECT>
							<EDITCELLSTYLEDEFAULT BorderWidth="1px" BorderStyle="None" BorderColor="Black" VerticalAlign="Middle">
								<PADDING Bottom="1px"></PADDING>
							</EDITCELLSTYLEDEFAULT>
							<ROWALTERNATESTYLEDEFAULT BackColor="White"></ROWALTERNATESTYLEDEFAULT>
							<ROWSTYLEDEFAULT BorderWidth="1px" BorderStyle="Solid" BorderColor="#D7D8D9" HorizontalAlign="Left"
								VerticalAlign="Middle">
								<PADDING Left="3px"></PADDING>
								<BORDERDETAILS WidthLeft="0px" WidthTop="0px"></BORDERDETAILS>
							</ROWSTYLEDEFAULT>
							<IMAGEURLS ImageDirectory="/ig_common/WebGrid3/"></IMAGEURLS>
						</DISPLAYLAYOUT><BANDS><IGTBL:ULTRAGRIDBAND></IGTBL:ULTRAGRIDBAND>
						</BANDS></td>
				</tr>
				<tr class="normal">
					<td>
						<table id="Table3" height="100%" cellPadding="0" width="100%">
							<tr>
								<TD class="smallImgButton"><INPUT class="gridExportButton" id="cmdGridExport" type="submit" value="  " name="cmdGridExport"
										runat="server"> |
								</TD>
								<TD width="140"><cc1:pagersizeselector id="pagerSizeSelector" runat="server"></cc1:pagersizeselector></TD>
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
								
						    <td align="center">
						        <uc4:uclinechartprocess id="lineChart"  runat="server"></uc4:uclinechartprocess>
						    </td>
						
								<td valign="top">
									<table border="0">
										<tr>
											<td class="fieldNameLeft"><asp:Label Runat="server" ID="lblLotTotalCount" Font-Bold="True">抽检LOT总计</asp:Label></td>
											<td><asp:Label Runat="server" ID="lblLotTotalCountValue"></asp:Label></td>
										</tr>
										<tr>
											<td class="fieldNameLeft"><asp:Label Runat="server" ID="lblLotRejectCount" Font-Bold="True">批退LOT总计</asp:Label></td>
											<td><asp:Label Runat="server" ID="lblLotRejectCountValue"></asp:Label></td>
										</tr>
										<tr>
											<td class="fieldNameLeft"><asp:Label Runat="server" ID="lblLRR" Font-Bold="True"> LRR汇总</asp:Label></td>
											<td><asp:Label Runat="server" ID="lblLRRValue"></asp:Label></td>
										</tr>
										<tr>
											<td class="fieldNameLeft"><asp:Label Runat="server" ID="lblLotSampleCount" Font-Bold="True">样本总计</asp:Label></td>
											<td><asp:Label Runat="server" ID="lblLotSampleCountValue"></asp:Label></td>
										</tr>
										<tr>
											<td class="fieldNameLeft"><asp:Label Runat="server" ID="lblLotSampleNGCount" Font-Bold="True">不良总计</asp:Label></td>
											<td><asp:Label Runat="server" ID="lblLotSampleNGCountValue"></asp:Label></td>
										</tr>
										<tr>
											<td class="fieldNameLeft"><asp:Label Runat="server" ID="lblDPPM" Font-Bold="True"> DPPM汇总</asp:Label></td>
											<td><asp:Label Runat="server" ID="lblDPPMValue"></asp:Label></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
