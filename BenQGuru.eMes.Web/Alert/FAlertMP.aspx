<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Page language="c#" Codebehind="FAlertMP.aspx.cs" AutoEventWireup="True" Inherits="BenQGuru.eMES.Web.Alert.FAlertMP" %>
<%@ Register TagPrefix="cc2" Namespace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<%@ Register TagPrefix="uc1" TagName="eMESDate" Src="~/UserControl/DateTime/DateTime/eMESDate.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ItemMP</title>
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
					<td><asp:label id="lblTitle" runat="server" CssClass="labeltopic"> 公告栏</asp:label></td>
				</tr>
				<tr>
					<td>
						<table class="query" width="100%">
							<tr>
								<td class="fieldNameLeft" noWrap><asp:label id="lblAlertTypeEdit" runat="server"> 预警类别</asp:label></td>
								<td class="fieldValue"><asp:dropdownlist id="drpAlertType" runat="server" CssClass="dropdownlist" Width="110%" AutoPostBack="True" onselectedindexchanged="drpAlertType_SelectedIndexChanged">
										<asp:ListItem Value="*">所有</asp:ListItem>
										<asp:ListItem Value="NG">不良率</asp:ListItem>
										<asp:ListItem Value="PPM">PPM</asp:ListItem>
										<asp:ListItem Value="DirectPass">直通率</asp:ListItem>
										<asp:ListItem Value="CPK">CPK</asp:ListItem>
										<asp:ListItem Value="First">首件下线</asp:ListItem>
									</asp:dropdownlist></td>
								<td class="fieldName" noWrap><asp:label id="lblAlertItemEdit" runat="server"> 预警项次</asp:label></td>
								<td class="fieldValue"><asp:dropdownlist id="drpAlertItem" runat="server" CssClass="dropdownlist" Width="110%" AutoPostBack="True" onselectedindexchanged="drpAlertItem_SelectedIndexChanged"></asp:dropdownlist></td>
								<td class="fieldName" noWrap><asp:label id="lblAlertItemCodeQuery" runat="server"> 预警项值</asp:label></td>
								<td class="fieldValue"><cc2:selectabletextbox id="stbItem" runat="server" CssClass="textbox" width="100px" height="0px" Target="item"
										Type="item"></cc2:selectabletextbox></td>
								<td class="fieldName" noWrap><asp:checkbox id="chkRefreshAuto" runat="server" AutoPostBack="True" Text="自动刷新"></asp:checkbox><asp:textbox id="txtMi" runat="server" CssClass="textbox" Width="42px">5</asp:textbox><asp:label id="lblMinute" runat="server">分钟</asp:label><cc1:refreshcontroller id="refreshCtrl" runat="server" Interval="2000"></cc1:refreshcontroller></td>
								<td class="fieldName"></td>
							</tr>
							<tr>
								<td class="fieldNameLeft" noWrap><asp:label id="lblAlertDate" runat="server"> 预警日期</asp:label></td>
								<td noWrap>                               
                                 <asp:TextBox type="text" id="dateFrom"  class='datepicker' runat="server"  Width="130px"/>
   </td>
								<td align="center"><asp:label id="lblTo" runat="server">到</asp:label></td>
								<td>
                                  <asp:TextBox type="text" id="dateTo"  class='datepicker' runat="server"  Width="130px"/>
                            </td>
								<td noWrap colSpan="3"><asp:checkboxlist id="chlAlertStatus" runat="server" RepeatDirection="Horizontal"></asp:checkboxlist></td>
								<td class="fieldName"><INPUT id="hidAction" style="WIDTH: 70px; HEIGHT: 22px" type="hidden" size="6" runat="server"></td>
							</tr>
							<TR>
								<td class="fieldNameLeft" noWrap>
									<asp:Label id="lblItemCodeQuery" runat="server">产品代码</asp:Label></td>
								<td noWrap>
									<cc2:selectabletextbox id="txtProduct" runat="server" CssClass="textbox" Type="item" Target="item" height="0px"
										width="100px"></cc2:selectabletextbox></td>
								<td align="center">
									<asp:Label id="lblSSQuery" runat="server">产线代码</asp:Label></td>
								<td>
									<cc2:selectabletextbox id="txtSSCode" runat="server" CssClass="textbox" Type="stepsequence" Target="item"
										height="0px" width="100px"></cc2:selectabletextbox></td>
								<td noWrap>
									<asp:Label id="lblEventDescribe" runat="server">事件描述</asp:Label></td>
								<td noWrap colspan="2">
									<asp:TextBox id="txtAlertMsg" runat="server" CssClass="textbox" Width="268px"></asp:TextBox></td>
								<td class="fieldName"><INPUT class="submitImgButton" id="cmdQuery" type="submit" value="查 询" name="btnQuery"
										runat="server"></td>
							</TR>
						</table>
					</td>
				</tr>
				<tr height="100%">
					<td class="fieldGrid" height="100%"><igtbl:ultrawebgrid id="gridWebGrid" runat="server" Width="100%" Height="100%">
							<DisplayLayout ColWidthDefault="" StationaryMargins="Header" AllowSortingDefault="Yes" RowHeightDefault="20px"
								Version="2.00" SelectTypeRowDefault="Single" SelectTypeCellDefault="Single" HeaderClickActionDefault="SortSingle"
								BorderCollapseDefault="Separate" AllowColSizingDefault="Free" CellPaddingDefault="4" RowSelectorsDefault="No"
								Name="gridWebGrid" TableLayout="Fixed">
								<AddNewBox>
									<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
									</Style>
								</AddNewBox>
								<Pager>
									<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
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
				</tr>
				<TR>
					<td style="HEIGHT: 20px"><asp:Label id="lblPrimaryAlert" runat="server">初级警告</asp:Label>
						<asp:Label id="lblPrimaryColor" runat="server">■</asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label id="lblImportantAlert" runat="server">重要警告</asp:Label>
						<asp:Label id="lblImportantColor" runat="server">■</asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label id="lblSeriousAlert" runat="server">严重警告</asp:Label>
						<asp:Label id="lblSeverityColor" runat="server">■</asp:Label></td>
				</TR>
				<tr class="normal">
					<td>
						<table height="100%" cellPadding="0" width="100%">
							<tr>
								<td><asp:checkbox id="chbSelectAll" runat="server" Width="124px" AutoPostBack="True" Text="全选"></asp:checkbox></td>
								<TD class="smallImgButton" noWrap><INPUT class="gridExportButton" id="cmdGridExport" type="submit" value="  " name="cmdCancel"
										runat="server"> |
								</TD>
								<TD class="smallImgButton" style="WIDTH: 40px"><INPUT class="deleteButton" id="cmdDelete" type="submit" value="  " name="cmdDelete" runat="server">
									|
								</TD>
								<TD nowrap><cc1:pagersizeselector id="pagerSizeSelector" runat="server"></cc1:pagersizeselector></TD>
								<td><asp:CheckBox id="chbExpDetails" runat="server" Text="导出处理记录"></asp:CheckBox></td>
								<td align="right"><cc1:pagertoolbar id="pagerToolBar" runat="server" PageSize="20"></cc1:pagertoolbar></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
