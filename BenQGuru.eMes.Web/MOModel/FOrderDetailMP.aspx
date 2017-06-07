<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="uc1" TagName="eMESDate" Src="~/UserControl/DateTime/DateTime/eMESDate.ascx" %>
<%@ Register TagPrefix="cc2" NameSpace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<%@ Page language="c#" Codebehind="FOrderDetailMP.aspx.cs" AutoEventWireup="True" Inherits="BenQGuru.eMES.Web.MOModel.FOrderDetailMP" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="ss1" NameSpace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>FItemLocationMP</title>
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
					<td><asp:label id="lblTitle" runat="server" CssClass="labeltopic"> 订单明细</asp:label></td>
				</tr>
				<tr>
					<td>
						<table class="query" height="100%" width="100%">
							<tr>
								<td class="fieldNameLeft" noWrap><asp:label id="lblOrderCodeQuery" runat="server"> 订单号</asp:label></td>
								<td class="fieldValue"><asp:textbox id="txtOrderCodeQuery" runat="server" CssClass="textbox" Width="130px"></asp:textbox></td>
								<td width="100%"></td>
								<td class="fieldName"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr height="100%">
					<td class="fieldGrid"><igtbl:ultrawebgrid id="gridWebGrid" runat="server" Width="100%" Height="100%"><DISPLAYLAYOUT TableLayout="Fixed" Name="webGrid" RowSelectorsDefault="No" CellPaddingDefault="4"
								AllowColSizingDefault="Free" BorderCollapseDefault="Separate" HeaderClickActionDefault="SortSingle" SelectTypeCellDefault="Single" SelectTypeRowDefault="Single" Version="2.00" RowHeightDefault="20px"
								AllowSortingDefault="Yes" StationaryMargins="Header" ColWidthDefault=""><ADDNEWBOX>
									<STYLE BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px"></STYLE>
								</ADDNEWBOX>
								<PAGER>
									<STYLE BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px"></STYLE>
								</PAGER>
								<HEADERSTYLEDEFAULT BackColor="#ABABAB" BorderStyle="Dashed" BorderWidth="1px" HorizontalAlign="Left"
									BorderColor="White" Font-Bold="True" Font-Size="12px">
									<BORDERDETAILS ColorLeft="White" ColorRight="White" WidthTop="1px" ColorBottom="White" WidthLeft="1px"
										ColorTop="White"></BORDERDETAILS>
								</HEADERSTYLEDEFAULT>
								<ROWSELECTORSTYLEDEFAULT BackColor="#EBEBEB"></ROWSELECTORSTYLEDEFAULT>
								<FRAMESTYLE Height="100%" Width="100%" BorderStyle="Groove" BorderWidth="0px" BorderColor="#ABABAB"
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
							</DISPLAYLAYOUT>
							<BANDS>
								<igtbl:UltraGridBand></igtbl:UltraGridBand>
							</BANDS>
						</igtbl:ultrawebgrid></td>
				</tr>
				<tr class="normal">
					<td>
						<table height="100%" cellPadding="0" width="100%">
							<tr>
								<td><asp:checkbox id="chbSelectAll" runat="server" Width="124px" Text="全选" AutoPostBack="True"></asp:checkbox></td>
								<TD class="smallImgButton"><INPUT class="gridExportButton" id="cmdGridExport" type="submit" value="  " name="cmdCancel"
										runat="server" onserverclick="cmdGridExport_ServerClick"> |
								</TD>
								<TD class="smallImgButton"><INPUT class="deleteButton" id="cmdDelete" type="submit" value="  " name="cmdDelete" runat="server">
									|
								</TD>
								<TD><cc1:pagersizeselector id="pagerSizeSelector" runat="server" PageSize="10"></cc1:pagersizeselector></TD>
								<td align="right"><cc1:pagertoolbar id="pagerToolBar" runat="server" PageSize="20"></cc1:pagertoolbar></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr class="normal">
					<td>
						<table class="edit" height="100%" cellPadding="0" width="100%">
							<tr>
								<td class="fieldNameLeft" style="HEIGHT: 26px" noWrap><asp:label id="lblOrderCodeEdit" runat="server">订单号</asp:label></td>
								<td class="fieldValue" style="HEIGHT: 26px"><asp:textbox id="txtOrderEdit" runat="server" CssClass="require" Width="130px"></asp:textbox></td>
								<td class="fieldName" noWrap><asp:label id="lblOrderStatusEdit" runat="server">订单状态</asp:label></td>
								<td class="fieldValue"><asp:dropdownlist id="drpOrderStatusEdit" runat="server" CssClass="require" Width="150px"></asp:dropdownlist></td>
								<td class="fieldName" style="HEIGHT: 26px" noWrap><asp:label id="lblCusCodeEdit" runat="server">客户代码</asp:label></td>
								<TD class="fieldValue" style="HEIGHT: 26px"><asp:textbox id="txtPartnerCodeEdit" runat="server" CssClass="require" Width="130px"></asp:textbox></TD>
								<td class="fieldName" style="HEIGHT: 26px" noWrap><asp:label id="lblItemCodeEdit" runat="server">产品代码</asp:label></td>
								<TD class="fieldValue" style="HEIGHT: 26px"><ss1:selectsingletabletextbox id="txtItemEdit" runat="server" Width="150px" Type="singleitem" CssClass="require"></ss1:selectsingletabletextbox></TD>
								<td noWrap width="100%"></td>
							</tr>
							<tr>
								<td class="fieldNameLeft" style="HEIGHT: 26px" noWrap><asp:label id="lblPlanQTYEdit" runat="server">计划数量</asp:label></td>
								<td class="fieldValue" style="HEIGHT: 26px"><asp:textbox id="txtPlanQTYEdit" runat="server" CssClass="require" Width="130px">0</asp:textbox></td>
								<td class="fieldName" noWrap><asp:label id="lblPlanShipDateEdit" runat="server">计划完成日期</asp:label></td>
								<td class="fieldValue"><uc1:emesdate id="txtPlanDateEdit" runat="server" CssClass="require" Width="130"></uc1:emesdate></td>
								<td class="fieldName" style="HEIGHT: 26px" noWrap><asp:label id="lblActQTYEdit" runat="server">实际数量</asp:label></td>
								<TD class="fieldValue" style="HEIGHT: 26px"><asp:textbox id="txtActQTYEdit" runat="server" CssClass="require" Width="130px">0</asp:textbox></TD>
								<td class="fieldName" style="HEIGHT: 26px" noWrap><asp:label id="lblActShipDateEdit" runat="server">实际完成日期</asp:label></td>
								<TD class="fieldValue" style="HEIGHT: 26px"><uc1:emesdate id="txtActDateEdit" runat="server" CssClass="textbox" Width="130"></uc1:emesdate></TD>
								<td noWrap width="100%"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr class="toolBar">
					<td>
						<table class="toolBar">
							<tr>
								<td class="toolBar"><INPUT class="submitImgButton" id="cmdAdd" type="submit" value="新 增" name="cmdAdd" runat="server"></td>
								<td class="toolBar"><INPUT class="submitImgButton" id="cmdSave" type="submit" value="保 存" name="cmdSave" runat="server"></td>
								<td class="toolBar"><INPUT class="submitImgButton" id="cmdCancel" type="submit" value="取 消" name="cmdCancel"
										runat="server"></td>
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
