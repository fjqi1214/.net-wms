<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="cc2" NameSpace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="uc1" TagName="eMESDate" Src="~/UserControl/DateTime/DateTime/eMESDate.ascx" %>
<%@ Page language="c#" Codebehind="FStockOutDetailQP.aspx.cs" AutoEventWireup="True" Inherits="BenQGuru.eMES.Web.WebQuery.FStockOutDetailQP" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>出货明细查询</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href="<%=StyleSheet%>" rel=stylesheet>
	</HEAD>
	<body scroll="yes" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table height="98%" width="98%">
				<tr class="moduleTitle">
					<td><asp:label id="lblTitle" runat="server" CssClass="labeltopic">出货明细查询</asp:label></td>
				</tr>
				<tr>
					<td>
						<table class="query" height="100%" width="100%">
							<TR>
								<td class="fieldNameLeft" noWrap><asp:label id="lblShipNoQuery" runat="server">出货单号</asp:label></td>
								<td class="fieldValue"><asp:textbox id="txtShipNoQuery" runat="server" CssClass="textbox" Width="140px"></asp:textbox></td>
								<td class="fieldName" noWrap><asp:label id="lblCusCodeQuery" runat="server"> 客户代码</asp:label></td>
								<td class="fieldValue"><asp:textbox id="txtPartner" runat="server" CssClass="textbox" Width="140px"></asp:textbox></td>
								<td class="fieldName" noWrap><asp:label id="lblOutDateFromQuery" runat="server">出货采集日期</asp:label></td>
								<td noWrap width="100%">
                                <asp:TextBox  id="dateInDateFromQuery"  class='datepicker' runat="server"  Width="130px"/>
                                </td>
								<td class="fieldName" noWrap><asp:label id="lblInDateToQuery" runat="server"> 到</asp:label>
								<td class="fieldValue" noWrap width="100%">
                                <asp:TextBox  id="dateInDateToQuery"  class='datepicker' runat="server"  Width="130px"/>
                               </td>
							</TR>
							<TR>
								<td class="fieldNameLeft" noWrap><asp:label id="lblModelQuery" runat="server">产品别</asp:label></td>
								<td class="fieldValue"><asp:textbox id="txtModel" runat="server" CssClass="textbox" Width="140px"></asp:textbox></td>
								<td class="fieldName" noWrap><asp:label id="lblItemCodeQuery" runat="server">产品代码</asp:label></td>
								<td class="fieldValue">
									<cc2:selectabletextbox id="txtItemCode" runat="server" Width="90px" Type="item"></cc2:selectabletextbox></td>
								<TD class="fieldNameLeft" noWrap>
									<asp:Label id="lblRCardQuery" runat="server">产品序列号</asp:Label></TD>
								<TD class="fieldValue" noWrap>
									<asp:textbox id="txtRCardFrom" runat="server" CssClass="textbox" Width="165px"></asp:textbox></TD>
								<td noWrap class="fieldName"><asp:label id="lblTo" runat="server">到</asp:label></td>
								<td class="fieldValue"><asp:textbox id="txtRCardTo" runat="server" CssClass="textbox" Width="165px"></asp:textbox></td>
							<TR>
								<td class="fieldNameLeft" noWrap><asp:label id="lblPrintDate" runat="server">打单日期</asp:label>
								<td class="fieldValue">
                                <asp:TextBox  id="txtPrintDateFrom"  class='datepicker' runat="server"  Width="130px"/>
								<td class="fieldName" noWrap><asp:label id="lblPrintToDate" runat="server"> 到</asp:label>
								<td class="fieldValue">
                                <asp:TextBox  id="txtPrintDateTo"  class='datepicker' runat="server"  Width="130px"/>
								<td class="fieldName" noWrap><asp:label id="lblShipType" runat="server">出库类别</asp:label></td>
								<td class="fieldValue"><asp:dropdownlist id="drpShipype" runat="server" CssClass="dropdownlist" Width="140px"></asp:dropdownlist></td>
								<TD class="fieldName" align="left" style="WIDTH: 26px"></TD>
								<td class="fieldName" colspan="2" align="left"></td>
							<TR>
								<td class="fieldNameLeft" noWrap>
									<asp:Label id="lblCartonNo" runat="server">Carton箱号</asp:Label>
								<td class="fieldValue">
									<asp:textbox id="txtCartonNo" runat="server" CssClass="textbox" Width="140px"></asp:textbox>
								<td noWrap><asp:label id="lblMOIDQuery" runat="server">工单代码</asp:label>
								<td class="fieldValue" noWrap width="100%">
										<cc2:selectabletextbox id="txtConditionMo" runat="server" Width="90px" Type="mo"></cc2:selectabletextbox>
								<td class="fieldName" noWrap><INPUT class="submitImgButton" id="cmdQuery" type="submit" value="查 询" name="btnQuery"
										runat="server"></td>
								<td class="fieldValue"><INPUT id="hidAction" type="hidden" runat="server" NAME="hidAction" style="WIDTH: 107px; HEIGHT: 22px"
										size="12"></td>
								<td class="fieldName" style="WIDTH: 26px">
								&nbsp;
								<td>&nbsp;
								</td>
							</TR>
						</table>
					</td>
				</tr>
				<tr height="100%">
					<td class="fieldGrid"><igtbl:ultrawebgrid id="gridWebGrid" runat="server" Height="100%" Width="100%">
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
								<td><asp:checkbox id="chbExpDetail" runat="server" Text="导出序列号列表"></asp:checkbox></td>
								<TD><cc1:pagersizeselector id="pagerSizeSelector" runat="server"></cc1:pagersizeselector></TD>
								<td align="right"><cc1:pagertoolbar id="pagerToolBar" runat="server"></cc1:pagertoolbar></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
