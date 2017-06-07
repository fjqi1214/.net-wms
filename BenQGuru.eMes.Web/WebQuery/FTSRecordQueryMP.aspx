<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="uc1" TagName="eMESTime" Src="../UserControl/DateTime/DateTime/eMESDate.ascx" %>
<%@ Page language="c#" Codebehind="FTSRecordQueryMP.aspx.cs" AutoEventWireup="True" Inherits="BenQGuru.eMES.Web.WebQuery.FTSRecordQueryMP" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>FTSRecordQueryMP</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href="<%=StyleSheet%>" rel=stylesheet>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table height="100%" width="100%">
				<TBODY>
					<tr>
						<td><asp:label id="lblTitle" runat="server" CssClass="labeltopic" Font-Bold="True">
								<B>维修记录查询</B></asp:label></td>
					</tr>
					<tr>
						<td>
							<table class="query" width="100%">
								<TBODY>
									<tr>
										<td class="fieldNameLeft" style="HEIGHT: 20px" noWrap><asp:label id="lblModelQuery" Runat="server">产品别</asp:label></td>
										<td class="fieldValue" style="WIDTH: 143px; HEIGHT: 20px" noWrap><asp:dropdownlist id="drpModel" Runat="server" Width="120" AutoPostBack="True" onload="drpModel_Load" onselectedindexchanged="drpModel_SelectedIndexChanged"></asp:dropdownlist></td>
										<td class="fieldName" style="HEIGHT: 20px" noWrap><asp:label id="lblItemQuery" Runat="server">产品</asp:label></td>
										<td class="fieldValue" style="WIDTH: 131px; HEIGHT: 20px" noWrap><asp:dropdownlist id="drpItem" Runat="server" WIDTH="120px" AutoPostBack="True" onload="drpItem_Load" onselectedindexchanged="drpItem_SelectedIndexChanged"></asp:dropdownlist></td>
										<td class="fieldName" style="HEIGHT: 20px" noWrap><asp:label id="lblMOQuery" Runat="server">工单</asp:label></td>
										<td class="fieldValue" style="HEIGHT: 20px" noWrap><asp:dropdownlist id="drpMo" Runat="server" WIDTH="120px" AutoPostBack="True" onload="drpMo_Load" onselectedindexchanged="drpMo_SelectedIndexChanged"></asp:dropdownlist></td>
										<td class="fieldName" style="HEIGHT: 20px" noWrap><asp:label id="lblOperation" Runat="server">工序</asp:label></td>
										<td class="fieldValue" style="HEIGHT: 20px" noWrap><asp:dropdownlist id="drpOperation" Runat="server" WIDTH="120px" AutoPostBack="True" onload="drpOperation_Load"></asp:dropdownlist></td>
									</tr>
									<tr>
										<td class="fieldNameLeft" style="HEIGHT: 12px" noWrap><asp:label id="lblSegment" Runat="server">工段</asp:label></td>
										<td class="fieldValue" style="WIDTH: 143px; HEIGHT: 12px" noWrap><asp:dropdownlist id="drpSegment" Runat="server" WIDTH="120px" AutoPostBack="True" onload="drpSegment_Load" onselectedindexchanged="drpSegment_SelectedIndexChanged"></asp:dropdownlist></td>
										<td class="fieldName" style="HEIGHT: 12px" noWrap><asp:label id="lblStepSequence" Runat="server">生产线</asp:label></td>
										<td class="fieldValue" style="WIDTH: 131px; HEIGHT: 12px" noWrap><asp:dropdownlist id="drpStepSequence" Runat="server" WIDTH="120px" AutoPostBack="True" onload="drpStepSequence_Load" onselectedindexchanged="drpStepSequence_SelectedIndexChanged"></asp:dropdownlist></td>
										<td class="fieldName" style="HEIGHT: 12px" noWrap><asp:label id="lblRes" Runat="server">资源</asp:label></td>
										<td class="fieldValue" style="HEIGHT: 12px" noWrap><asp:dropdownlist id="drpResource" Runat="server" WIDTH="120px" AutoPostBack="True" onload="drpResource_Load"></asp:dropdownlist></td>
										<td style="HEIGHT: 12px">
										<td style="HEIGHT: 12px"></td>
									<tr>
										<td class="fieldNameLeft" noWrap><asp:label id="lblStartSnQuery" Runat="server">起始序列号</asp:label></td>
										<td class="fieldValue" style="WIDTH: 143px" noWrap><asp:textbox id="txtStartSN" Runat="server" Width="165px" CssClass="require"></asp:textbox></td>
										<td class="fieldName" noWrap><asp:label id="lblEndSnQuery" Runat="server">结束序列号</asp:label></td>
										<td class="fieldValue" style="WIDTH: 131px" noWrap><asp:textbox id="txtEndMO" Runat="server" WIDTH="165px" CssClass="require"></asp:textbox></td>
										<td></td>
										<td></td>
										<td></td>
										<td></td>
									</tr>
									<tr>
										<td class="fieldNameLeft" noWrap><asp:label id="lblStartTimeQuery" Runat="server">起始时间</asp:label></td>
										<TD class="fieldValue" style="WIDTH: 143px">
                                        <asp:TextBox  id="txtDateFrom"  class='datepicker require' runat="server"  Width="120px"/>
</TD>
										<td class="fieldName" noWrap><asp:label id="lblEndTimeQuery" Runat="server">结束时间</asp:label></td>
										<TD class="fieldValue" style="WIDTH: 131px">
                                         <asp:TextBox  id="txtDateTo"  class='datepicker require' runat="server"  Width="120px"/>
                                     </TD>
										<td></td>
										<td></td>
										<td></td>
										<td></td>
									</tr>
									<tr>
										<td class="fieldNameLeft" noWrap><asp:label id="lblTSStateQuery" Runat="server">维修状态</asp:label></td>
										<td class="fieldValue" noWrap colSpan="6">
											<table>
												<TBODY>
													<TR>
														<td><asp:checkboxlist id="chkStatus" Runat="server" RepeatDirection="Horizontal">
																<asp:ListItem>送修中</asp:ListItem>
																<asp:ListItem>待修中</asp:ListItem>
																<asp:ListItem>维修中</asp:ListItem>
																<asp:ListItem>已修好</asp:ListItem>
																<asp:ListItem>已报废</asp:ListItem>
																<asp:ListItem>已拆解</asp:ListItem>
															</asp:checkboxlist></td>
													</TR>
												</TBODY>
											</table>
										</td>
										<td class="fieldValue" id="Td3" noWrap>&nbsp; <input class="submitImgButton" id="cmdQuery" style="BACKGROUND-POSITION-Y: center; BACKGROUND-IMAGE: url(http://localhost/EMES/Skin/Image/query.gif); BACKGROUND-REPEAT: no-repeat"
												type="submit" value="查 询" name="cmdQuery"></td>
									</tr>
								</TBODY>
							</table>
						</td>
					</tr>
					<tr height="100%" width="100%">
						<td class="fieldGrid">
							<table height="100%" width="100%">
								<tr>
									<td><igtbl:ultrawebgrid id="gridWebGrid" runat="server">
											<DisplayLayout StationaryMargins="Header" AllowSortingDefault="Yes" RowHeightDefault="20px" Version="2.00"
												SelectTypeRowDefault="Single" SelectTypeCellDefault="Single" HeaderClickActionDefault="SortSingle"
												BorderCollapseDefault="Separate" AllowColSizingDefault="Free" CellPaddingDefault="4" RowSelectorsDefault="No"
												Name="UltraWebGrid1" TableLayout="Fixed">
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
												<ImageUrls ImageDirectory="/ig_grid2_Images/"></ImageUrls>
											</DisplayLayout>
											<Bands>
												<igtbl:UltraGridBand></igtbl:UltraGridBand>
											</Bands>
										</igtbl:ultrawebgrid></td>
								</tr>
							</table>
						</td>
					</tr>
					<tr class="normal">
						<td>
							<table height="100%" cellPadding="0" width="100%">
								<tr>
									<td><asp:checkbox id="chbSelectAll" runat="server" Width="124px" AutoPostBack="True" Text="全选"></asp:checkbox></td>
									<TD class="smallImgButton"><INPUT class="gridExportButton" id="cmdGridExport" type="submit" value="  " name="cmdGridExport"
											runat="server"> |
									</TD>
									<TD><cc1:pagersizeselector id="pagerSizeSelector" runat="server" PageSize="10"></cc1:pagersizeselector></TD>
									<td align="right"><cc1:PagerToolbar id="pagerToolBar" runat="server"></cc1:PagerToolbar></td>
								</tr>
							</table>
						</td>
					</tr>
					</TR></TBODY></table>
			</TD></TR></TBODY></TABLE></form>
		</TR></TBODY></TABLE></TR></TBODY></TABLE></FORM></TD></TR></TBODY></TABLE></TD></TR></TBODY></TABLE></TD></TR></TBODY></TABLE></FORM>
	</body>
</HTML>
