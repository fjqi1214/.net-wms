<%@ Page language="c#" Codebehind="FOQCTestDataQP.aspx.cs" AutoEventWireup="True" Inherits="BenQGuru.eMES.Web.WebQuery.FOQCTestDataQP" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics35.WebUI.UltraWebGrid.v9.1, Version=9.1.20091.2101, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="cc2" NameSpace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<%@ Register TagPrefix="uc1" TagName="eMesDate" Src="../UserControl/DateTime/DateTime/eMesDate.ascx" %>
<%@ Register TagPrefix="uc1" TagName="eMesTime" Src="../UserControl/DateTime/DateTime/eMesTime.ascx" %>
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
				<TBODY>
					<tr class="moduleTitle">
						<td style="HEIGHT: 20px"><asp:label id="lblTitle" runat="server" CssClass="labeltopic">FQC抽检明细查询</asp:label></td>
					</tr>
					<tr>
						<td>
							<table class="query" id="Table2" height="100%" width="100%">
								<TBODY>
									<TR>
										<td class="fieldNameLeft" noWrap><asp:label id="lblItemCodeQuery" runat="server">产品代码</asp:label></td>
										<td class="fieldValue"><cc2:selectabletextbox id="txtConditionItem" runat="server" Width="90px" Type="item" CssClass="require" CanKeyIn="true"></cc2:selectabletextbox></td>
										<td class="fieldName" noWrap><asp:label id="lblMOIDQuery" runat="server">工单代码</asp:label></td>
										<td class="fieldValue"><cc2:selectabletextbox id="txtConditionMo" runat="server" Width="90px" Type="mo" CanKeyIn="true"></cc2:selectabletextbox></td>
										<td class="fieldNameLeft" noWrap><asp:label id="lblOQCLotQuery" runat="server">送检批号</asp:label></td>
										<td class="fieldValue"><cc2:selectabletextbox id="txtOQCLotQuery" runat="server" Width="90px" Type="lot" CssClass="textbox" CanKeyIn="true"></cc2:selectabletextbox></td>
										<td></td>
									</TR>
									<tr>
										<td class="fieldNameLeft" noWrap><asp:label id="lblStartRCardQuery" runat="server">起始产品序列号</asp:label></td>
										<td class="fieldValue"><asp:textbox id="txtStartSnQuery" runat="server" CssClass="textbox" Width="165px"></asp:textbox></td>
										<td class="fieldName" noWrap><asp:label id="lblEndRCardQuery" runat="server">截止产品序列号</asp:label></td>
										<td class="fieldValue"><asp:textbox id="txtEndSnQuery" runat="server" CssClass="textbox" Width="165px"></asp:textbox></td>
										<td class="fieldNameLeft" noWrap><asp:label id="lblCartonNoQuery" runat="server">箱号</asp:label></td>
										<td class="fieldValue" noWrap><asp:textbox id="txtCartonNoQuery" runat="server" CssClass="textbox" Width="130"></asp:textbox></td>
										<td class="fieldNameLeft" style="DISPLAY: none" noWrap><asp:label id="lblSSCode" runat="server">产线</asp:label></td>
										<td style="DISPLAY: none"><cc2:selectabletextbox id="txtSSCode" runat="server" Width="90px" Type="stepsequence" Target="stepsequence"></cc2:selectabletextbox></td>
										<td></td>
									</tr>
									<tr>
										<td class="fieldName" noWrap><asp:label id="lblOQCBegindate" runat="server">抽检起始日期</asp:label></td>
										<TD class="fieldValue" noWrap>
											<TABLE cellSpacing="0" cellPadding="0" border="0">
												<TR>
													<TD><uc1:emesdate id="txtOQCBeginDate" runat="server" CssClass="require" width="80"></uc1:emesdate></TD>
													<TD><uc1:emestime id="txtOQCBeginTime" runat="server" CssClass="require" width="60" style="DISPLAY:none"
															Visible="False"></uc1:emestime></TD>
												</TR>
											</TABLE>
										</TD>
										<td class="fieldName" noWrap><asp:label id="lblOQCEnddt" runat="server">抽检结束日期</asp:label></td>
										<TD class="fieldValue" noWrap>
											<TABLE cellSpacing="0" cellPadding="0" border="0">
												<TR>
													<TD><uc1:emesdate id="txtOQCEndDate" runat="server" CssClass="require" width="80"></uc1:emesdate></TD>
													<TD><uc1:emestime id="txtOQCEndTime" runat="server" CssClass="require" width="60" style="DISPLAY:none"
															Visible="False"></uc1:emestime></TD>
												</TR>
											</TABLE>
										</TD>
										<td></td>
										<td class="fieldName"><INPUT class="submitImgButton" id="cmdQuery" type="submit" value="查 询" name="btnQuery"
												runat="server">
										</td>
									</tr>
								</TBODY>
							</table>
						</td>
					</tr>
					<TR class="moduleTitle">
						<TD><asp:label id="lblTestInfoTitle" runat="server">功能测试信息</asp:label><INPUT class="textbox" id="txtSeq" style="DISPLAY: none; WIDTH: 120px" readOnly name="textfield"
								runat="server"><INPUT id="txtLotNo" type="hidden" name="Hidden1" runat="server"><INPUT id="txtLotSeq" type="hidden" name="Hidden2" runat="server"></TD>
					</TR>
					<TR height="50%">
						<TD class="fieldGrid" height="25%">
							<STYLE type="text/css">.gridWebGrid-ic { BORDER-RIGHT: #d7d8d9 1px solid; BORDER-TOP: #d7d8d9 0px solid; PADDING-LEFT: 3px; FONT-SIZE: 12px; VERTICAL-ALIGN: middle; BORDER-LEFT: #d7d8d9 0px solid; BORDER-BOTTOM: #d7d8d9 1px solid; FONT-FAMILY: Verdana; TEXT-ALIGN: left }
	.gridWebGrid-aic { BORDER-RIGHT: #d7d8d9 1px solid; BORDER-TOP: #d7d8d9 0px solid; PADDING-LEFT: 3px; FONT-SIZE: 12px; VERTICAL-ALIGN: middle; BORDER-LEFT: #d7d8d9 0px solid; BORDER-BOTTOM: #d7d8d9 1px solid; FONT-FAMILY: Verdana; BACKGROUND-COLOR: #ebebeb; TEXT-ALIGN: left }
	.gridWebGrid-0-ic { BORDER-RIGHT: #d7d8d9 1px solid; BORDER-TOP: #d7d8d9 0px solid; PADDING-LEFT: 3px; FONT-SIZE: 12px; VERTICAL-ALIGN: middle; BORDER-LEFT: #d7d8d9 0px solid; BORDER-BOTTOM: #d7d8d9 1px solid; FONT-FAMILY: Verdana; TEXT-ALIGN: left }
	.gridWebGrid-0-aic { BORDER-RIGHT: #d7d8d9 1px solid; BORDER-TOP: #d7d8d9 0px solid; PADDING-LEFT: 3px; FONT-SIZE: 12px; VERTICAL-ALIGN: middle; BORDER-LEFT: #d7d8d9 0px solid; BORDER-BOTTOM: #d7d8d9 1px solid; FONT-FAMILY: Verdana; BACKGROUND-COLOR: #ebebeb; TEXT-ALIGN: left }
	.gridWebGrid-0-6-cbc { BACKGROUND-POSITION: center 50%; BACKGROUND-IMAGE: url(./skin/image/detail16.gif); CURSOR: hand; BORDER-TOP-STYLE: none; BACKGROUND-REPEAT: no-repeat; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; TEXT-ALIGN: center; BORDER-BOTTOM-STYLE: none }
	.gridWebGrid-ecc { BORDER-RIGHT: black 1px; BORDER-TOP: black 1px; PADDING-BOTTOM: 1px; VERTICAL-ALIGN: middle; BORDER-LEFT: black 1px; BORDER-BOTTOM: black 1px }
	.gridWebGrid-hc { BORDER-RIGHT: white 1px dashed; BORDER-TOP: white 1px dashed; PADDING-LEFT: 3px; FONT-WEIGHT: bold; FONT-SIZE: 12px; VERTICAL-ALIGN: middle; BORDER-LEFT: white 1px dashed; BORDER-BOTTOM: white 1px dashed; FONT-FAMILY: Verdana; BACKGROUND-COLOR: #ababab; TEXT-ALIGN: left }
	.gridWebGrid-shc { BORDER-RIGHT: white 1px solid; BORDER-TOP: white 0px solid; PADDING-LEFT: 3px; FONT-WEIGHT: bold; FONT-SIZE: 12px; VERTICAL-ALIGN: middle; BORDER-LEFT: white 0px solid; BORDER-BOTTOM: white 1px solid; FONT-FAMILY: Verdana; BACKGROUND-COLOR: #ababab; TEXT-ALIGN: left }
							</STYLE>
							<igtbl:ultrawebgrid id="grdFT" runat="server" Width="100%" Height="100%">
								<DisplayLayout ColWidthDefault="" StationaryMargins="Header" AllowSortingDefault="Yes" RowHeightDefault="20px"
									Version="2.00" SelectTypeRowDefault="Single" SelectTypeCellDefault="Single" HeaderClickActionDefault="SortSingle"
									BorderCollapseDefault="Separate" AllowColSizingDefault="Free" CellPaddingDefault="4" RowSelectorsDefault="No"
									Name="Ultrawebgrid1" TableLayout="Fixed">
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
										BorderStyle="Groove" Height="150"></FrameStyle>
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
							</igtbl:ultrawebgrid></TD>
					</TR>
					<TR class="normal">
						<TD>
							<TABLE id="Table3" height="100%" cellPadding="0" width="100%">
								<TR>
									<td></td>
									<TD class="smallImgButton"><INPUT class="gridExportButton" id="btnExportFT" type="submit" value="  " name="cmdGridExport"
											runat="server">|</TD>
									<TD><cc1:pagersizeselector id="Pagersizeselector1" runat="server"></cc1:pagersizeselector></TD>
									<td align="right"><cc1:pagertoolbar id="Pagertoolbar1" runat="server"></cc1:pagertoolbar></td>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD><asp:label id="lblSizeInfoTitle" runat="server" CssClass="labeltopic">尺寸量测信息</asp:label></TD>
					</TR>
					<TR height="50">
						<TD class="fieldGrid" height="75%"><igtbl:ultrawebgrid id="gridSN" runat="server" Width="100%" Height="100%">
								<DisplayLayout ColWidthDefault="" StationaryMargins="Header" AllowSortingDefault="Yes" RowHeightDefault="20px"
									Version="2.00" SelectTypeRowDefault="Single" SelectTypeCellDefault="Single" HeaderClickActionDefault="SortSingle"
									BorderCollapseDefault="Separate" AllowColSizingDefault="Free" CellPaddingDefault="4" RowSelectorsDefault="No"
									Name="gridTrans" TableLayout="Fixed">
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
										BorderStyle="Groove" Height="150"></FrameStyle>
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
							</igtbl:ultrawebgrid></TD>
					</TR>
					<TR class="normal">
						<TD>
							<TABLE id="Table3" height="100%" cellPadding="0" width="100%">
								<TR>
									<td></td>
									<TD class="smallImgButton" noWrap><INPUT class="gridExportButton" id="btnExpDimension" type="submit" value="  " name="cmdGridExport"
											runat="server">|</TD>
									<TD><cc1:pagersizeselector id="Pagersizeselector2" runat="server"></cc1:pagersizeselector></TD>
									<td align="right"><cc1:pagertoolbar id="Pagertoolbar2" runat="server"></cc1:pagertoolbar></td>
								</TR>
							</TABLE>
						</TD>
					</TR>
				</TBODY>
			</table>
		</form>
		</TR></TBODY></TABLE></TR></TBODY></TABLE></FORM>
	</body>
</HTML>
