<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Page language="c#" Codebehind="FTransMP.aspx.cs" AutoEventWireup="True" Inherits="BenQGuru.eMES.Web.WarehouseWeb.FTransMP" %>
<%@ Register TagPrefix="uc1" TagName="eMESDate" Src="~/UserControl/DateTime/DateTime/eMESDate.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>交易单据维护</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href="<%=StyleSheet%>" rel=stylesheet>
		<script language="javascript">
			function SelectMOItem()
			{
				var result = window.showModalDialog("FMOSelectSP.aspx", "" ,"dialogWidth:800px;dialogHeight:600px;scroll:none;help:no;status:no") ;
				if(result!=null)
				{
					document.getElementById("txtMOCodeEdit").value = result.code;
				}
			}
			function ChangeToDropDownList(fromName, toName)
			{
				var fromobj = document.getElementById(fromName);
				var toobj = document.getElementById(toName);
				if (fromobj == undefined || toobj == undefined)
					return;
				toobj.selectedIndex = fromobj.selectedIndex;
			}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table height="100%" width="100%">
				<tr class="moduleTitle">
					<td><asp:label id="lblTitle" runat="server" CssClass="labeltopic"> 交易单据维护</asp:label></td>
				</tr>
				<tr>
					<td>
						<table class="query" height="100%" width="100%">
							<tr>
								<td class="fieldNameLeft" noWrap><asp:label id="lblTicketNoQuery" runat="server"> 单据号</asp:label></td>
								<td><asp:textbox id="txtTransCodeQuery" runat="server" Width="60px" CssClass="textbox"></asp:textbox></td>
								<td class="fieldNameLeft" noWrap><asp:label id="lblRefCodeQuery" runat="server"> 参考号</asp:label></td>
								<td><asp:textbox id="txtRefCodeQuery" runat="server" Width="70px" CssClass="textbox"></asp:textbox></td>
								<td class="fieldNameLeft" noWrap><asp:label id="lblStatus" runat="server"> 状态</asp:label></td>
								<td><asp:DropDownList Runat="server" ID="drpTransStatusQuery" Width="70px"></asp:DropDownList></td>
								<td class="fieldNameLeft" noWrap><asp:label id="lblDate" runat="server"> 日期</asp:label></td>
								<td><uc1:emesdate id="txtDateFromQuery" runat="server" CssClass="textbox" width="70"></uc1:emesdate></td>
								<td><uc1:emesdate id="txtDateToQuery" runat="server" CssClass="textbox" width="70"></uc1:emesdate></td>
								<td class="fieldNameLeft" noWrap><asp:label id="lblMOCodeQuery" runat="server"> 工单号</asp:label></td>
								<td><asp:textbox id="txtMoCodeQuery" runat="server" Width="60px" CssClass="textbox"></asp:textbox></td>
								<td class="fieldNameLeft" noWrap><asp:label id="lblUserCodeQuery" runat="server"> 用户</asp:label></td>
								<td><asp:textbox id="txtUserCodeQuery" runat="server" Width="60px" CssClass="textbox"></asp:textbox></td>
								<td noWrap width="100%"></td>
								<td class="fieldName"><INPUT class="submitImgButton" id="cmdQuery" type="submit" value="查 询" name="btnQuery"
										runat="server"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr height="100%">
					<td class="fieldGrid"><igtbl:ultrawebgrid id="gridWebGrid" runat="server" Width="100%" Height="100%">
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
								<td><asp:checkbox id="chbSelectAll" runat="server" AutoPostBack="True" Text="全选"></asp:checkbox></td>
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
					<td align="center">
						<table class="edit" height="100%" cellPadding="0" width="50%">
							<tr>
								<td class="fieldName" style="HEIGHT: 26px" noWrap><asp:label id="lblTransNameEdit" runat="server">单据名称</asp:label></td>
								<td style="HEIGHT: 26px"><asp:DropDownList Runat="server" ID="drpTransTypeEdit" onselectedindexchanged="drpTransTypeEdit_SelectedIndexChanged"></asp:DropDownList><asp:textbox id="txtTransCodeEdit" runat="server" CssClass="require" Width="0px" Visible="False"></asp:textbox></td>
								<td class="fieldName" style="HEIGHT: 26px" noWrap><asp:label id="lblRefCodeEdit" runat="server">参考号</asp:label></td>
								<TD style="HEIGHT: 26px"><asp:textbox id="txtRefCodeEdit" runat="server" CssClass="textbox" Width="130px"></asp:textbox></TD>
								<td class="fieldName" style="HEIGHT: 26px" noWrap><asp:label id="lblMOCodeEdit" runat="server">工单号</asp:label></td>
								<TD style="HEIGHT: 26px"><asp:textbox id="txtMOCodeEdit" runat="server" CssClass="textbox" Width="100px"></asp:textbox>
									<img src="~/Skin/Image/query.gif" id="Image1" style="CURSOR: hand" runat="server" onclick="SelectMOItem();return false;">
								</TD>
							</tr>
							<tr>
								<td class="fieldName" noWrap><asp:label id="lblFactoryFromQuery" runat="server">来源工厂</asp:label></td>
								<td><asp:DropDownList Runat="server" ID="drpFactoryFromEdit" Width="130px"></asp:DropDownList></td>
								<td class="fieldName" noWrap><asp:label id="lblWarehouseFromQuery" runat="server">来源仓库</asp:label></td>
								<td><asp:textbox id="drpWHFromEdit" runat="server" CssClass="require" Width="100px"></asp:textbox></td>
								<td class="fieldName" noWrap></td>
								<td></td>
							</tr>
							<tr>
								<td class="fieldName" noWrap><asp:label id="lblFactoryToQuery" runat="server">目标工厂</asp:label></td>
								<td><asp:DropDownList Runat="server" ID="drpFactoryToEdit" Width="130px"></asp:DropDownList></td>
								<td class="fieldName" noWrap><asp:label id="lblWarehouseToQuery" runat="server">目标仓库</asp:label></td>
								<td><asp:textbox id="drpWHToEdit" runat="server" CssClass="require" Width="100px"></asp:textbox></td>
								<td class="fieldName" noWrap></td>
								<td></td>
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
