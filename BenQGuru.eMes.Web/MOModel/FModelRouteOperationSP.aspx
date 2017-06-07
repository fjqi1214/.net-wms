<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Page language="c#" Codebehind="FModelRouteOperationSP.aspx.cs" AutoEventWireup="True" Inherits="BenQGuru.eMES.Web.MOModel.FModelRouteOperationSP" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>FModelRouteSP</title>
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
					<td><asp:label id="lblTitle" runat="server" CssClass="labeltopic"> 工序列表</asp:label></td>
				</tr>
				<tr>
					<td></td>
				</tr>
				<tr height="100%">
					<td class="fieldGrid"><igtbl:ultrawebgrid id="gridWebGrid" runat="server" Height="100%" Width="100%">
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
				<tr class="normal">
					<td>
						<table height="100%" cellPadding="0" width="100%">
							<tr>
								<TD class="smallImgButton"><INPUT class="gridExportButton" id="cmdGridExport" type="submit" value="  " name="cmdCancel"
										runat="server" onserverclick="cmdGridExport_ServerClick"> |
								</TD>
								<TD><cc1:pagersizeselector id="pagerSizeSelector" runat="server"></cc1:pagersizeselector></TD>
								<td align="right"><cc1:pagertoolbar id="pagerToolBar" runat="server" PageSize="20"></cc1:pagertoolbar></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr class="normal">
					<td>
						<table class="edit" height="100%" cellPadding="0" width="100%">
							<tr>
								<td nowrap class="fieldNameLeft"><asp:checkbox id="chbOperationCheckEdit" runat="server" Width="60px" Text="工序检查" TextAlign="Left"></asp:checkbox></td>
								<td nowrap class="fieldValue"><asp:label id="lblOPCheckEdit" runat="server" Width="100px">用以标记该工序是否进行工序检查</asp:label></td>
								<td nowrap class="fieldName"><asp:checkbox id="chbEditSPC" runat="server" Width="60px" Text="SPC统计" TextAlign="Left"></asp:checkbox></td>
								<td nowrap class="fieldValue"><asp:label id="lblSPCEdit" runat="server" Width="100px">用以标记该工序是否进行SPC统计</asp:label></td>
								<td nowrap class="fieldName"><asp:checkbox id="chbCompLoadingEdit" runat="server" Width="60px" Text="上料检查" TextAlign="Left"></asp:checkbox></td>
								<td nowrap class="fieldValue"><asp:label id="lblComponentLoadingEdit" runat="server" Width="100px">用以标记该工序是否为上料工序</asp:label></td>
								<td nowrap width="100%"></td>
							</tr>
							<tr>
								<td nowrap class="fieldNameLeft"><asp:checkbox id="chbNGTestEdit" runat="server" Width="60px" Text="不良判定" TextAlign="Left"></asp:checkbox></td>
								<td nowrap class="fieldValue"><asp:label id="lblTestEdit" runat="server" Width="100px">用以标记该工序是否为测试工序</asp:label></td>
								<td nowrap class="fieldName"><asp:checkbox id="chbRepairEdit" runat="server" Width="60px" Text="维修" TextAlign="Left"></asp:checkbox></td>
								<td nowrap class="fieldValue"><asp:label id="lblRepair" runat="server" Width="100px">用以标记该工序是否为维修工序</asp:label></td>
								<td nowrap class="fieldName"><asp:checkbox id="chbStartOpEdit" runat="server" Width="60px" Text="开始工序" TextAlign="Left"></asp:checkbox></td>
								<td nowrap class="fieldValue"><asp:label id="lblStartOperationEdit" runat="server" Width="100px">用以标记该工序是否为开始工序</asp:label></td>
								<td nowrap width="100%"></td>
							</tr>
							<tr>
								<td nowrap class="fieldNameLeft"><asp:checkbox id="chbEndOpEdit" runat="server" Width="60px" Text="结束工序" TextAlign="Left"></asp:checkbox></td>
								<td nowrap class="fieldValue"><asp:label id="lblCloseOperationEdit" runat="server" Width="100px">用以标记该工序是否为结束工序</asp:label></td>
								<td nowrap class="fieldName"><asp:checkbox id="chbPackEdit" runat="server" Width="60px" Text="包装" TextAlign="Left"></asp:checkbox></td>
								<td nowrap class="fieldValue"><asp:label id="lblPackingEdit" runat="server" Width="100px">用以标记该工序是否为包装工序</asp:label></td>
								<td nowrap class="fieldName"><asp:checkbox id="chbIDMergeEdit" runat="server" Text="流程卡号变换" AutoPostBack="True" Width="60px"
										TextAlign="Left" oncheckedchanged="cbIDMergeEdit_CheckedChanged"></asp:checkbox></td>
								<td nowrap class="fieldValue"><asp:label id="lblMNIDMergeEdit" runat="server" Width="100px">用以标记该工序留程卡是否变换</asp:label></td>
								<td nowrap width="100%"></td>
							</tr>
							<tr>
								<td style="PADDING-LEFT: 8px" noWrap><asp:label id="lblCodeEdit" runat="server">序号</asp:label></td>
								<td class="fieldValue" style="HEIGHT: 26px"><asp:textbox id="txtOperationsequenceEdit" runat="server" CssClass="textbox"></asp:textbox></td>
								<asp:panel id="pnlMainEdit" runat="server">
									<TD style="PADDING-LEFT: 8px" noWrap>
										<asp:label id="lblMergeTypeEdit" runat="server" Width="60px"> 转化类型</asp:label></TD>
									<TD class="fieldValue" noWrap>
										<asp:dropdownlist id="drpMergeTypeEdit" runat="server" Width="170px" onload="drpMergeTypeEdit_Load"></asp:dropdownlist></TD>
									<TD style="PADDING-LEFT: 8px" noWrap>
										<asp:label id="lblMergeRule" runat="server" Width="60px"> 转换比例</asp:label></TD>
									<TD class="fieldValue" noWrap>
										<TABLE height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
											<TR>
												<TD>
													<asp:label id="lblnumeratorEdit" runat="server" Width="20px" Height="8px">1</asp:label></TD>
												<TD style="WIDTH: 33px">
													<asp:label id="Label16" runat="server" Width="20px">:</asp:label></TD>
												<TD>
													<asp:textbox id="txtDenominatorEdit" runat="server" CssClass="textbox" Width="20px"></asp:textbox></TD>
												<TD noWrap width="100%"></TD>
											</TR>
										</TABLE>
									</TD>
								</asp:panel>
							</tr>
						</table>
					</td>
				</tr>
				<tr class="toolBar">
					<td>
						<table class="toolBar">
							<tr>
								<td class="toolBar"><INPUT class="submitImgButton" id="cmdSave" type="submit" value="保 存" name="cmdSave" runat="server" onserverclick="cmdSave_ServerClick"></td>
								<td><INPUT class="submitImgButton" id="cmdReturn" type="submit" value="返 回" name="cmdReturn"
										runat="server" onserverclick="cmdReturn_ServerClick"></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
