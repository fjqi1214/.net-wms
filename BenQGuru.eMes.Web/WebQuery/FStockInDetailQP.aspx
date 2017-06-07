<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="uc1" TagName="eMESDate" Src="~/UserControl/DateTime/DateTime/eMESDate.ascx" %>
<%@ Register TagPrefix="cc2" NameSpace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<%@ Page language="c#" Codebehind="FStockInDetailQP.aspx.cs" AutoEventWireup="True" Inherits="BenQGuru.eMES.Web.WebQuery.FStockInDetailQP" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>FOnWipOP</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href="<%=StyleSheet%>" rel=stylesheet>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<table height="100%" width="100%" align="center">
				<tr class="moduleTitle">
					<td><asp:label id="lblTitle" runat="server" CssClass="labeltopic">入库明细查询</asp:label></td>
				</tr>
				<tr>
					<td>
						<table class="query" height="120" style="WIDTH: 862px; HEIGHT: 120px">
							<TR>
								<td class="fieldNameLeft" noWrap><asp:label id="lblReceiveNoQuery" runat="server">入库单号</asp:label></td>
								<td class="fieldValue"><asp:textbox id="txtReceivQuery" runat="server" CssClass="textbox" Width="140px"></asp:textbox></td>
								<td class="fieldName" noWrap><asp:label id="lblStatusQuery" runat="server">序列号状态</asp:label></td>
								<td class="fieldValue" style="WIDTH: 138px"><asp:dropdownlist id="drpStatus" runat="server" CssClass="dropdownlist" Width="140px"></asp:dropdownlist></td>
								<td class="fieldName" nowrap><asp:label id="lblInDateQuery" runat="server">入库采集日期</asp:label></td>
								<td noWrap width="100%">
                                <asp:TextBox  id="dateInDateFromQuery"  class='datepicker' runat="server"  Width="130px"/>
                                </td>
								<td class="fieldName"><asp:label id="lblInDateToQuery" runat="server"> 到</asp:label>
								<td class="fieldValue" noWrap width="100%">
                                <asp:TextBox  id="dateInDateToQuery"  class='datepicker' runat="server"  Width="130px"/>
                                ></td>
							</TR>
							<TR>
								<td class="fieldNameLeft" noWrap><asp:label id="lblModelQuery" runat="server">产品别</asp:label></td>
								<td class="fieldValue"><asp:textbox id="txtModel" runat="server" CssClass="textbox" Width="140px"></asp:textbox></td>
								<td class="fieldName" noWrap><asp:label id="lblItemCodeQuery" runat="server">产品代码</asp:label></td>
								<td class="fieldValue" style="WIDTH: 138px">
									<cc2:selectabletextbox id="txtItemCode" runat="server" Width="90px" Type="item"></cc2:selectabletextbox></td>
								<td noWrap><asp:label id="lblRCardQuery" runat="server">产品序列号</asp:label></td>
								<td class="fieldValue" noWrap width="100%"><asp:textbox id="txtRCardFrom" runat="server" CssClass="textbox" Width="165px"></asp:textbox></td>
								<td noWrap width="100%"><asp:label id="lblTo" runat="server">到</asp:label></td>
								<td class="fieldValue"><asp:textbox id="txtRCardTo" runat="server" CssClass="textbox" Width="165px"></asp:textbox></td>
							<TR>
								<td class="fieldName" noWrap><asp:label id="lblRecType" runat="server">入库类别</asp:label></td>
								<td class="fieldValue"><asp:dropdownlist id="drpRecType" runat="server" CssClass="dropdownlist" Width="140px"></asp:dropdownlist></td>
								<td class="fieldNameLeft" noWrap>
									<asp:Label id="lblCartonNo" runat="server">Carton箱号</asp:Label>
								<td class="fieldValue" style="WIDTH: 138px">
									<asp:textbox id="txtCartonNo" runat="server" CssClass="textbox" Width="140px"></asp:textbox>
								<td noWrap>
									<asp:label id="lblMOIDQuery" runat="server">工单代码</asp:label>
								<td class="fieldValue" noWrap width="100%">
										<cc2:selectabletextbox id="txtConditionMo" runat="server" Width="90px" Type="mo"></cc2:selectabletextbox>
								<TD class="fieldName" align="right"></TD>
								<td><INPUT class="submitImgButton" id="cmdQuery" type="submit" value="查 询" name="btnQuery"
										runat="server"><INPUT id="hidAction" type="hidden" runat="server" style="WIDTH: 40px; HEIGHT: 22px" size="1"></td>
							</TR>
						</table>
					</td>
				</tr>
				<TR height="100%">
					<TD>
						<igtbl:ultrawebgrid id="gridWebGrid" runat="server" Width="100%" Height="100%">
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
							</DisplayLayout>
							<Bands>
								<igtbl:UltraGridBand HeaderClickAction="SortSingle"></igtbl:UltraGridBand>
							</Bands>
						</igtbl:ultrawebgrid></TD>
				</TR>
				<TR class="normal">
					<td>
						<table height="100%" cellPadding="0" width="100%">
							<tr>
								<TD class="smallImgButton"><INPUT class="gridExportButton" id="cmdGridExport" type="submit" value="  " name="cmdGridExport"
										runat="server"> |
								</TD>
								<TD>
									<asp:CheckBox id="chbExpDetail" runat="server" Text="导出序列号列表"></asp:CheckBox>
								</TD>
								<TD><cc1:pagersizeselector id="pagerSizeSelector" runat="server"></cc1:pagersizeselector></TD>
								<td align="right"><cc1:pagertoolbar id="pagerToolBar" runat="server"></cc1:pagertoolbar></td>
							</tr>
						</table>
					</td>
				</TR>
			</table>
		</form>
	</body>
</HTML>
