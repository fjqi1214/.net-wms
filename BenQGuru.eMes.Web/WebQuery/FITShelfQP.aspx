<%@ Register TagPrefix="cc2" NameSpace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<%@ Page language="c#" Codebehind="FITShelfQP.aspx.cs" AutoEventWireup="True" Inherits="BenQGuru.eMES.Web.WebQuery.FITShelfQP" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="uc1" TagName="eMESDate" Src="~/UserControl/DateTime/DateTime/eMESDate.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>FITShelfQP</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href="<%=StyleSheet%>" rel=stylesheet>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" height="100%" width="100%">
				<TR class="moduleTitle">
					<TD><asp:label id="lblTitle" runat="server">老化明细情况查询</asp:label></TD>
				</TR>
				<TR>
					<TD>
						<TABLE class="query" id="Table2" height="100%" width="100%">
							<TR>
								<TD class="fieldName" noWrap><asp:label id="lblShelfNOQuery" runat="server">车号</asp:label></TD>
								<TD class="fieldValue">
										<cc2:selectabletextbox id="txtShelf" runat="server" Width="100px" Type="shelf" Target="shelf"></cc2:selectabletextbox></TD>
								<td class="fieldNameLeft" noWrap><asp:label id="lblItemCodeQuery" runat="server">产品代码</asp:label></td>
								<td class="fieldValue"><cc2:selectabletextbox id="txtConditionItem" runat="server" Type="item" Width="100px"></cc2:selectabletextbox></td>
								<td class="fieldName" noWrap><asp:label id="lblMOIDQuery" runat="server">工单代码</asp:label></td>
								<td class="fieldValue"><cc2:selectabletextbox id="txtConditionMo" runat="server" Type="mo" Width="100px"></cc2:selectabletextbox></td>
								<TD noWrap width="100%">&nbsp;</TD>
							</TR>
							<tr>
								<td class="fieldNameLeft" noWrap><asp:label id="lblStartRCardQuery" runat="server">起始产品序列号</asp:label></td>
								<td class="fieldValue"><asp:textbox id="txtStartSnQuery" runat="server" CssClass="textbox" Width="165px"></asp:textbox></td>
								<td class="fieldName" noWrap><asp:label id="lblEndRCardQuery" runat="server">截止产品序列号</asp:label></td>
								<td class="fieldValue"><asp:textbox id="txtEndSnQuery" runat="server" CssClass="textbox" Width="165px"></asp:textbox></td>
								<TD noWrap width="100%">&nbsp;</TD>
								<TD noWrap width="100%">&nbsp;</TD>
								<TD noWrap width="100%">&nbsp;</TD>
							</tr>
							<tr>
								<td noWrap><asp:radiobutton id="rdbBurnIN" runat="server" Checked="True" Text="Burn In起始日期" GroupName="Burn"></asp:radiobutton></td>
								<td class="fieldValue">
                                <asp:TextBox  id="txtBurnInDateFrom"  class='datepicker' runat="server"  Width="130px"/>
</td>
								<td class="fieldName" noWrap><asp:label id="lblFrmDateTo" runat="server">Burn In截止日期</asp:label></td>
								<td class="fieldValue">
                                <asp:TextBox  id="txtBurnInDateTo"  class='datepicker' runat="server"  Width="130px"/>
                              </td>
								<TD noWrap width="100%">&nbsp;</TD>
								<TD noWrap width="100%">&nbsp;</TD>
								<TD noWrap width="100%">&nbsp;</TD>
							</tr>
							<tr>
								<td noWrap><asp:radiobutton id="rdbBurnOut" runat="server" Text="Burn Out起始日期" GroupName="Burn"></asp:radiobutton></td>
								<td class="fieldValue">
                                <asp:TextBox  id="txtBurnOutDateFrom"  class='datepicker' runat="server"  Width="130px"/>
                                </td>
								<td class="fieldName" noWrap><asp:label id="lblBurnOutDateTo" runat="server">Burn Out截止日期</asp:label></td>
								<td class="fieldValue">
                                <asp:TextBox  id="txtBurnOutDateTo"  class='datepicker' runat="server"  Width="130px"/>
                               </td>
								<TD noWrap width="100%">&nbsp;</TD>
								<TD noWrap width="100%">&nbsp;</TD>
								<TD noWrap width="100%"><INPUT class="submitImgButton" id="cmdQuery" type="submit" value="查 询" name="btnQuery"
										runat="server" onserverclick="cmdQuery_ServerClick"></TD>
							</tr>
						</TABLE>
					</TD>
				</TR>
				<TR height="100%">
					<TD class="fieldGrid">
						<STYLE type="text/css">.gridWebGrid-ic { BORDER-RIGHT: #d7d8d9 1px solid; BORDER-TOP: #d7d8d9 0px solid; PADDING-LEFT: 3px; FONT-SIZE: 12px; VERTICAL-ALIGN: middle; BORDER-LEFT: #d7d8d9 0px solid; BORDER-BOTTOM: #d7d8d9 1px solid; FONT-FAMILY: Verdana; TEXT-ALIGN: left }
	.gridWebGrid-aic { BORDER-RIGHT: #d7d8d9 1px solid; BORDER-TOP: #d7d8d9 0px solid; PADDING-LEFT: 3px; FONT-SIZE: 12px; VERTICAL-ALIGN: middle; BORDER-LEFT: #d7d8d9 0px solid; BORDER-BOTTOM: #d7d8d9 1px solid; FONT-FAMILY: Verdana; BACKGROUND-COLOR: #ebebeb; TEXT-ALIGN: left }
	.gridWebGrid-0-ic { BORDER-RIGHT: #d7d8d9 1px solid; BORDER-TOP: #d7d8d9 0px solid; PADDING-LEFT: 3px; FONT-SIZE: 12px; VERTICAL-ALIGN: middle; BORDER-LEFT: #d7d8d9 0px solid; BORDER-BOTTOM: #d7d8d9 1px solid; FONT-FAMILY: Verdana; TEXT-ALIGN: left }
	.gridWebGrid-0-aic { BORDER-RIGHT: #d7d8d9 1px solid; BORDER-TOP: #d7d8d9 0px solid; PADDING-LEFT: 3px; FONT-SIZE: 12px; VERTICAL-ALIGN: middle; BORDER-LEFT: #d7d8d9 0px solid; BORDER-BOTTOM: #d7d8d9 1px solid; FONT-FAMILY: Verdana; BACKGROUND-COLOR: #ebebeb; TEXT-ALIGN: left }
	.gridWebGrid-0-6-cbc { BACKGROUND-POSITION: center 50%; BACKGROUND-IMAGE: url(./skin/image/detail16.gif); CURSOR: hand; BORDER-TOP-STYLE: none; BACKGROUND-REPEAT: no-repeat; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; TEXT-ALIGN: center; BORDER-BOTTOM-STYLE: none }
	.gridWebGrid-ecc { BORDER-RIGHT: black 1px; BORDER-TOP: black 1px; PADDING-BOTTOM: 1px; VERTICAL-ALIGN: middle; BORDER-LEFT: black 1px; BORDER-BOTTOM: black 1px }
	.gridWebGrid-hc { BORDER-RIGHT: white 1px dashed; BORDER-TOP: white 1px dashed; PADDING-LEFT: 3px; FONT-WEIGHT: bold; FONT-SIZE: 12px; VERTICAL-ALIGN: middle; BORDER-LEFT: white 1px dashed; BORDER-BOTTOM: white 1px dashed; FONT-FAMILY: Verdana; BACKGROUND-COLOR: #ababab; TEXT-ALIGN: left }
	.gridWebGrid-shc { BORDER-RIGHT: white 1px solid; BORDER-TOP: white 0px solid; PADDING-LEFT: 3px; FONT-WEIGHT: bold; FONT-SIZE: 12px; VERTICAL-ALIGN: middle; BORDER-LEFT: white 0px solid; BORDER-BOTTOM: white 1px solid; FONT-FAMILY: Verdana; BACKGROUND-COLOR: #ababab; TEXT-ALIGN: left }
						</STYLE>
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
						</igtbl:ultrawebgrid></TD>
				</TR>
				<TR class="normal">
					<TD>
						<TABLE id="Table3" height="100%" cellPadding="0" width="100%">
							<TR>
								<TD class="smallImgButton" noWrap><INPUT class="gridExportButton" id="cmdGridExport" type="submit" value="  " name="cmdGridExport"
										runat="server">|</TD>
								<td><asp:checkbox id="chbItemDetail" runat="server" Text="导出产品明细"></asp:checkbox></td>
								<TD><cc1:pagersizeselector id="pagerSizeSelector" runat="server"></cc1:pagersizeselector></TD>
								<td align="right"><cc1:pagertoolbar id="pagerToolBar" runat="server"></cc1:pagertoolbar></td>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<tr class="toolBar">
					<td>
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
