<%@ Register TagPrefix="uc1" TagName="eMesDate" Src="~/UserControl/DateTime/DateTime/eMesDate.ascx" %>
<%@ Register TagPrefix="uc1" TagName="eMesTime" Src="~/UserControl/DateTime/DateTime/eMesTime.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Page language="c#" Codebehind="FSystemErrorMP.aspx.cs" AutoEventWireup="True" Inherits="BenQGuru.eMES.Web.BaseSetting.FSystemErrorMP" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>FSystemErrorMP</title>
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
					<td><asp:label id="lblTitle" runat="server" CssClass="labeltopic">系统错误记录维护</asp:label></td>
				</tr>
				<tr>
					<td>
						<table class="query" height="100%" width="100%">
							<tr>
								<td class="fieldNameLeft" noWrap><asp:label id="lblSystemErrorCodeQuery" runat="server">系统错误代码</asp:label></td>
								<td class="fieldValue" colSpan="2"><asp:textbox id="txtSystemErrorCodeQuery" runat="server" CssClass="textbox"></asp:textbox></td>
								<td class="fieldName" noWrap><asp:label id="lblIsResovedQuery" runat="server">记录状态</asp:label></td>
								<td colSpan="2"><asp:dropdownlist id="drpIsResovedQuery" runat="server" Width="130px">
										<asp:ListItem></asp:ListItem>
										<asp:ListItem Value="1">已解决</asp:ListItem>
										<asp:ListItem Value="0">未解决</asp:ListItem>
									</asp:dropdownlist></td>
								<td></td>
								<td></td>
							</tr>
							<tr>
								<td class="fieldNameLeft" style="HEIGHT: 26px" noWrap><asp:label id="lblSendBeginDateTimeEdit" runat="server">提交时间  从</asp:label></td>
								<td class="fieldName"><uc1:emesdate id="dateSendBeginDateEdit" CssClass="textbox" runat="server" width="100"></uc1:emesdate></td>
								<TD class="fieldName" style="HEIGHT: 26px"><uc1:emestime id="timeSendBeginTimeEdit" CssClass="textbox" runat="server" width="60"></uc1:emestime></TD>
								<td class="fieldName" noWrap><asp:label id="lblTo" runat="server">到</asp:label></td>
								<td class="fieldName"><uc1:emesdate id="dateSendEndDateEdit" CssClass="textbox" runat="server" width="100"></uc1:emesdate></td>
								<td class="fieldValue"><uc1:emestime id="timeSendEndTimeEdit" CssClass="textbox" text="23:59:59" runat="server" width="60"></uc1:emestime></td>
								<td noWrap width="100%"></td>
								<td class="fieldName"><INPUT class="submitImgButton" id="cmdQuery" type="submit" value="查 询" name="btnQuery"
										runat="server"></td>
							</tr>
						</table>
					</td>
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
								<td><asp:checkbox id="chbSelectAll" runat="server" Width="124px" AutoPostBack="True" Text="全选"></asp:checkbox></td>
								<TD class="smallImgButton"><INPUT class="gridExportButton" id="cmdGridExport" type="submit" value="  " name="cmdGridExport"
										runat="server"> |
								</TD>
								<TD class="smallImgButton"><INPUT class="deleteButton" id="cmdDelete" type="submit" value="  " name="cmdDelete" runat="server">
									|
								</TD>
								<TD><cc1:pagersizeselector id="pagerSizeSelector" runat="server"></cc1:pagersizeselector></TD>
								<td align="right">
									<cc1:PagerToolbar id="pagerToolBar" runat="server"></cc1:PagerToolbar></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr class="normal">
					<td>
						<table class="edit" height="100%" cellPadding="0" width="100%">
							<tr>
								<td class="fieldNameLeft" noWrap><asp:label id="lblSystemErrorCodeEdit" runat="server">系统错误代码</asp:label></td>
								<td class="fieldValue"><asp:textbox id="txtSystemErrorCodeEdit" runat="server" CssClass="require" ReadOnly="True"></asp:textbox></td>
								<td class="fieldName" noWrap><asp:label id="lblResolveNotesEdit" runat="server">解决备注</asp:label></td>
								<td class="fieldValue"><asp:textbox id="txtResolveNotesEdit" runat="server" CssClass="textbox" Width="250px"></asp:textbox></td>
								<td width="100%"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr class="toolBar">
					<td>
						<table class="toolBar">
							<tr>
								<td class="toolBar"><INPUT class="submitImgButton" id="cmdResolve" type="submit" value="解 决" name="cmdResolve"
										runat="server" onserverclick="cmdResolve_ServerClick"></td>
								<td class="toolBar"><INPUT class="submitImgButton" id="cmdSave" type="submit" value="保 存" name="cmdSave" runat="server"></td>
								<td><INPUT class="submitImgButton" id="cmdCancel" type="submit" value="取 消" name="cmdCancel"
										runat="server"></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
