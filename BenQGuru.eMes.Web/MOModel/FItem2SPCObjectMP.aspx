<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Page language="c#" Codebehind="FItem2SPCObjectMP.aspx.cs" AutoEventWireup="True" Inherits="BenQGuru.eMES.Web.MOModel.FItem2SPCDataStoreMP" %>
<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="cc2" NameSpace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<%@ Register TagPrefix="ss1" NameSpace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<%@ Register TagPrefix="uc1" TagName="eMESDate" Src="~/UserControl/DateTime/DateTime/eMESDate.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>FItem2SPCObjectMP</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href="<%=StyleSheet%>" rel=stylesheet>
		<script>
			function checkDataLink()
			{
				if(document.getElementById("chbDataLink").checked)
				{
					document.getElementById("txtDataLinkQty").disabled = false;
				}
				else
				{
					document.getElementById("txtDataLinkQty").value = "";
					document.getElementById("txtDataLinkQty").disabled = true;
				}
			}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table height="100%" width="100%">
				<tr class="moduleTitle">
					<td><asp:label id="lblTitle" runat="server" CssClass="labeltopic"> 产品SPC测试项维护</asp:label></td>
				</tr>
				<tr>
					<td>
						<table class="query" height="100%" width="100%">
							<tr>
								<td class="fieldNameLeft" noWrap><asp:label id="lblItemCodeQuery" runat="server"> 产品代码</asp:label></td>
								<td class="fieldValue" style="WIDTH: 159px">
									<cc2:selectabletextbox id="txtItemCodeQuery" runat="server" CssClass="textbox" Width="130px" Target="item"
										Type="item"></cc2:selectabletextbox></td>
								<td class="fieldNameLeft" style="HEIGHT: 26px" noWrap><asp:label id="lblObjectCodeQuery" runat="server">管控项目</asp:label></td>
								<td class="fieldValue" style="HEIGHT: 26px">
										<asp:DropDownList id="drpObjectCodeQuery" runat="server" CssClass="textbox" Width="130px"></asp:DropDownList></td>
								<td noWrap width="100%"></td>
								<td class="fieldName"><INPUT class="submitImgButton" id="cmdQuery" type="submit" value="查 询" name="btnQuery"
										runat="server" onserverclick="cmdQuery_ServerClick"></td>
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
								<td><asp:checkbox id="chbSelectAll" runat="server" Width="124px" AutoPostBack="True" Text="全选" oncheckedchanged="chbSelectAll_CheckedChanged"></asp:checkbox></td>
								<TD class="smallImgButton"><INPUT class="gridExportButton" id="cmdGridExport" type="submit" value="  " name="cmdCancel"
										runat="server" onserverclick="cmdGridExport_ServerClick"> |
								</TD>
								<TD class="smallImgButton"><INPUT class="deleteButton" id="cmdDelete" type="submit" value="  " name="cmdDelete" runat="server" onserverclick="cmdDelete_ServerClick">
									|
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
								<TD class="fieldNameLeft" noWrap style="HEIGHT: 27px"><asp:label id="lblItemCodeEdit" runat="server">产品代码</asp:label></TD>
								<td class="fieldValue" style="HEIGHT: 27px">
									<ss1:selectsingletabletextbox id="txtItemCodeEdit" runat="server" Width="130px" Type="singleitem"></ss1:selectsingletabletextbox></td>
								<td class="fieldNameLeft" style="HEIGHT: 27px" noWrap><asp:label id="lblObjectCodeEdit" runat="server">管控项目</asp:label></td>
								<td class="fieldValue" style="HEIGHT: 27px">
									<asp:DropDownList id="drpObjectCodeEdit" runat="server" CssClass="textbox" Width="130px"></asp:DropDownList></td>
								<td class="fieldNameLeft" style="HEIGHT: 27px" noWrap><asp:label id="lblTableNameEdit" runat="server">数据库表名</asp:label></td>
								<td class="fieldValue" style="HEIGHT: 27px">
									<asp:textbox id="txtTableNameEdit" runat="server" CssClass="require" Width="130px"></asp:textbox></td>
								<td width="100%" style="HEIGHT: 27px"></td>
							</tr>
							<tr>
								<TD class="fieldNameLeft" noWrap><asp:label id="lblDateFromEdit" runat="server">开始日期</asp:label></TD>
								<td class="fieldValue">
										<uc1:eMESDate id="txtDateFromEdit" runat="server" CssClass="require" DESIGNTIMEDRAGDROP="59" width="130"></uc1:eMESDate></td>
								<td class="fieldNameLeft" style="HEIGHT: 26px" noWrap><asp:label id="lblDateToEdit" runat="server">结束日期</asp:label></td>
								<td class="fieldValue" style="HEIGHT: 26px">
									<uc1:eMESDate id="txtDateToEdit" runat="server" CssClass="require" width="130"></uc1:eMESDate></td>
								<td class="fieldNameLeft" style="HEIGHT: 26px" noWrap></td>
								<td class="fieldValue" style="HEIGHT: 26px"><input id="txtIDEdit" type="hidden" runat="server"></td>
								<td width="100%" colspan="3"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr class="toolBar">
					<td>
						<table class="toolBar">
							<tr>
								<td class="toolBar"><INPUT class="submitImgButton" id="cmdAdd" type="submit" value="新 增" name="cmdAdd" runat="server" onserverclick="cmdAdd_ServerClick"></td>
								<td class="toolBar"><INPUT class="submitImgButton" id="cmdSave" type="submit" value="保 存" name="cmdSave" runat="server" onserverclick="cmdSave_ServerClick"></td>
								<td><INPUT class="submitImgButton" id="cmdCancel" type="submit" value="取 消" name="cmdCancel"
										runat="server" onserverclick="cmdCancel_ServerClick"></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
