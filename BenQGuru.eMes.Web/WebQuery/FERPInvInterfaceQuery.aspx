<%@ Page language="c#" Codebehind="FERPInvInterfaceQuery.aspx.cs" AutoEventWireup="True" Inherits="BenQGuru.eMES.Web.WebQuery.FERPInvInterfaceQuery" %>
<%@ Register TagPrefix="uc1" TagName="eMesTime" Src="../UserControl/DateTime/DateTime/eMesTime.ascx" %>
<%@ Register TagPrefix="uc1" TagName="eMesDate" Src="../UserControl/DateTime/DateTime/eMesDate.ascx" %>
<%@ Register TagPrefix="cc2" NameSpace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>FERPInvInterfaceQuery</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href="<%=StyleSheet%>" rel=stylesheet>
		<script language="javascript">
			
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table id="Table1" height="100%" width="100%">
				<tr class="moduleTitle">
					<td style="HEIGHT: 19px"><asp:label id="lblTitle" runat="server" CssClass="labeltopic">工单入库抛转资料查询</asp:label></td>
				</tr>
				<tr>
					<td>
						<table class="query" id="Table2" height="100%" width="100%">
							<TR>
								<td class="fieldNameLeft" noWrap><asp:label id="lblMOIDQuery" runat="server">工单代码</asp:label></td>
								<td class="fieldValue"><cc2:selectabletextbox id="txtMoQuery" runat="server" Width="150px" Type="mo"></cc2:selectabletextbox></td>
								<td class="fieldName" noWrap><asp:label id="lblItemCodeQuery" runat="server">产品代码</asp:label></td>
								<td class="fieldValue"><cc2:selectabletextbox id="txtItemQuery" runat="server" Width="150px" Type="item"></cc2:selectabletextbox></td>
								<td></td>
								<td></td>
							</TR>
							<tr>
								<td class="fieldNameLeft" noWrap style="HEIGHT: 32px"><asp:label id="lblReceiveNoQuery" runat="server">入库单号</asp:label></td>
								<td class="fieldValue" style="HEIGHT: 32px"><asp:textbox id="txtRecNO" runat="server" CssClass="textbox" Width="165px"></asp:textbox></td>
								<td class="fieldName" noWrap style="HEIGHT: 32px"><asp:label id="lblStatus" runat="server">状态</asp:label></td>
								<td class="fieldValue" style="HEIGHT: 32px">
									<asp:DropDownList id="drpStatusEdit" runat="server" Width="130px" Height="18px"></asp:DropDownList></td>
								<td style="HEIGHT: 32px"></td>
								<td style="HEIGHT: 32px"></td>
							</tr>
							<tr>
								<td class="fieldName" noWrap><asp:checkbox id="chbRecieve" runat="server" AutoPostBack="True" Checked="True"></asp:checkbox><asp:label id="lblRecieveBegindate" runat="server">入库起始日期</asp:label></td>
								<TD class="fieldValue" noWrap>
                                <asp:TextBox  id="txtRecieveBeginDate"  class='datepicker' runat="server"  Width="80px"/>
</TD>
								<td class="fieldName" noWrap><asp:label id="lblRecieveEnddate" runat="server">入库结束日期</asp:label></td>
								<TD class="fieldValue" noWrap>
                                <asp:TextBox  id="txtRecieveEndDate"  class='datepicker' runat="server"  Width="80px"/>
								</TD>
								<td></td>
								<td class="fieldName"><INPUT class="submitImgButton" id="cmdQuery" type="submit" value="查 询" name="btnQuery"
										runat="server">
								</td>
							</tr>
							<TR>
								<TD class="fieldName" noWrap><asp:checkbox id="chbToss" runat="server" AutoPostBack="True"></asp:checkbox>
									<asp:label id="lblTossStartDate" runat="server">抛转起始日期</asp:label></TD>
								<TD class="fieldValue" noWrap>
                                <asp:TextBox  id="txtTossBeginDate"  class='datepicker' runat="server"  Width="80px"/>
									</TD>
								<TD class="fieldName" noWrap>
									<asp:label id="lblTossEndDate" runat="server">抛转结束日期</asp:label></TD>
								<TD class="fieldValue" noWrap>
                                <asp:TextBox  id="txtTossEndDate"  class='datepicker' runat="server"  Width="80px"/>
									</TD>
								<TD></TD>
								<TD class="fieldName"><INPUT class="submitImgButton" id="cmdReToss" type="submit" value="重新抛转" name="cmdAdd"
										runat="server" onserverclick="cmdAdd_ServerClick"></TD>
							</TR>
						</table>
					</td>
				</tr>
				<TR height="100%">
					<TD class="fieldGrid"><igtbl:ultrawebgrid id="gridWebGrid" runat="server" Width="100%" Height="100%">
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
									<ClientSideEvents CellClickHandler="SingleRowSelect"></ClientSideEvents>
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
							</igtbl:ultrawebgrid><DISPLAYLAYOUT TableLayout="Fixed" Name="webGrid" RowSelectorsDefault="No" CellPaddingDefault="4"
							AllowColSizingDefault="Free" BorderCollapseDefault="Separate" HeaderClickActionDefault="SortSingle" SelectTypeCellDefault="Single"
							SelectTypeRowDefault="Single" Version="2.00" RowHeightDefault="20px" AllowSortingDefault="Yes" StationaryMargins="Header" ColWidthDefault=""><ADDNEWBOX>
								<STYLE BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
								</STYLE>
							</ADDNEWBOX>
							<PAGER>
								<STYLE BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
								</STYLE>
							</PAGER>
							<HEADERSTYLEDEFAULT BackColor="#ABABAB" BorderStyle="Dashed" BorderWidth="1px" HorizontalAlign="Left"
								BorderColor="White" Font-Bold="True" Font-Size="12px">
								<BORDERDETAILS ColorLeft="White" ColorRight="White" WidthTop="1px" ColorBottom="White" WidthLeft="1px"
									ColorTop="White"></BORDERDETAILS>
							</HEADERSTYLEDEFAULT>
							<ROWSELECTORSTYLEDEFAULT BackColor="#EBEBEB"></ROWSELECTORSTYLEDEFAULT>
							<FRAMESTYLE Width="100%" Height="100%" BorderStyle="Groove" BorderWidth="0px" BorderColor="#ABABAB"
								Font-Size="12px" Font-Names="Verdana"></FRAMESTYLE>
							<FOOTERSTYLEDEFAULT BackColor="LightGray" BorderStyle="Groove" BorderWidth="0px">
								<BORDERDETAILS ColorLeft="White" WidthTop="1px" WidthLeft="1px" ColorTop="White"></BORDERDETAILS>
							</FOOTERSTYLEDEFAULT>
							<ACTIVATIONOBJECT BorderStyle="Dotted"></ACTIVATIONOBJECT>
							<EDITCELLSTYLEDEFAULT BorderStyle="None" BorderWidth="1px" BorderColor="Black" VerticalAlign="Middle">
								<PADDING Bottom="1px"></PADDING>
							</EDITCELLSTYLEDEFAULT>
							<ROWALTERNATESTYLEDEFAULT BackColor="White"></ROWALTERNATESTYLEDEFAULT>
							<ROWSTYLEDEFAULT BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" BorderColor="#D7D8D9"
								VerticalAlign="Middle">
								<PADDING Left="3px"></PADDING>
								<BORDERDETAILS WidthTop="0px" WidthLeft="0px"></BORDERDETAILS>
							</ROWSTYLEDEFAULT>
							<IMAGEURLS ImageDirectory="/ig_common/WebGrid3/"></IMAGEURLS>
						</DISPLAYLAYOUT><BANDS><IGTBL:ULTRAGRIDBAND></IGTBL:ULTRAGRIDBAND>
						</BANDS></TD>
				</TR>
				<tr class="normal">
					<td>
						<table id="Table3" height="100%" cellPadding="0" width="100%">
							<tr>
								<TD width="140"><cc1:pagersizeselector id="pagerSizeSelector" runat="server"></cc1:pagersizeselector></TD>
								<TD class="smallImgButton"><INPUT class="gridExportButton" id="cmdGridExport" type="submit" value="  " name="cmdGridExport"
										runat="server"> |
								</TD>
								<td align="right"><cc1:pagertoolbar id="pagerToolBar" runat="server"></cc1:pagertoolbar></td>
							</tr>
							<TR>
								<TD class="smallImgButton" align="center" colSpan="3">
									<asp:checkbox id="chbSelected" runat="server" AutoPostBack="True" Visible="False"></asp:checkbox>
									<asp:checkbox id="chbUnSelected" runat="server" AutoPostBack="True" Visible="False"></asp:checkbox><TEXTAREA id="txtSelected" style="VISIBILITY: hidden" name="txtSelected" rows="1" cols="20"
										runat="server"></TEXTAREA></TD>
							</TR>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
