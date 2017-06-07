<%@ Register TagPrefix="uc1" TagName="eMESDate" Src="~/UserControl/DateTime/DateTime/eMESDate.ascx" %>
<%@ Page language="c#" Codebehind="FQueryItemMP.aspx.cs" AutoEventWireup="True" Inherits="BenQGuru.eMES.Web.WarehouseWeb.FQueryItemMP" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>物料交易查询</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href="<%=StyleSheet%>" rel=stylesheet>
		<script language="javascript">
			function OpenSelectTypeWindow()
			{
				var str = document.getElementById("txtTransTypeQuery").value;
				var result = window.showModalDialog("FQueryItemSelTransTypeSP.aspx?selecteditem="+str, "" ,"dialogWidth:400px;dialogHeight:220px;scroll:none;help:no;status:no") ;
				if (result != undefined)
				{
					document.getElementById("txtTransTypeQuery").value = result.code;
					document.getElementById("txtTransTypeNameQuery").value = result.name;
				}
			}
			function SelectMOItem()
			{
				var result = window.showModalDialog("FMOSelectSP.aspx", "" ,"dialogWidth:800px;dialogHeight:600px;scroll:none;help:no;status:no") ;
				if(result!=null)
				{
					document.getElementById("txtMOCodeQuery").value = result.code;
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
					<td><asp:label id="lblTitle" runat="server" CssClass="labeltopic"> 物料交易查询</asp:label></td>
				</tr>
				<tr>
					<td>
						<table class="query" height="100%" width="100%">
							<tr>
								<td class="fieldNameLeft" noWrap><asp:label id="lblFactoryFromQuery" runat="server">来源工厂</asp:label></td>
								<td><asp:dropdownlist id="drpFactoryFromQuery" runat="server" Width="130px" CssClass="require"></asp:dropdownlist></td>
								<td class="fieldName" noWrap>
										<asp:label id="lblWarehouseFromQuery" runat="server">来源仓库</asp:label></td>
								<td>
										<asp:TextBox id="drpWarehouseFromQuery" runat="server" CssClass="require" Width="130px"></asp:TextBox></td>
								<td class="fieldName" noWrap>
									<asp:label id="lblTransDateFromQuery" runat="server"> 交易日期 从</asp:label></td>
								<TD>
									<uc1:emesdate id="txtTransDateFromQuery" runat="server" CssClass="textbox" width="80"></uc1:emesdate></TD>
								<TD class="fieldName" noWrap width="100%"></TD>
							</tr>
							<tr>
								<td class="fieldNameLeft" noWrap><asp:label id="lblFactoryToQuery" runat="server">目标工厂</asp:label></td>
								<td><asp:dropdownlist id="drpFactoryToQuery" runat="server" Width="130px" CssClass="require"></asp:dropdownlist></td>
								<td class="fieldName" noWrap>
									<asp:label id="lblWarehouseToQuery" runat="server">目标仓库</asp:label></td>
								<td>
										<asp:TextBox id="drpWarehouseToQuery" runat="server" CssClass="require" Width="130px"></asp:TextBox></td>
								<td class="fieldName" noWrap align="right">
									<asp:label id="lblTo" runat="server">到</asp:label></td>
								<TD>
										<uc1:emesdate id="txtTransDateToQuery" runat="server" CssClass="textbox" width="80"></uc1:emesdate></TD>
								<td></td>
							</tr>
							<tr>
								<td class="fieldNameLeft" noWrap><asp:label id="lblTransNameQuery" runat="server">单据名称</asp:label></td>
								<td><asp:TextBox Runat="server" ID="txtTransTypeNameQuery" Width="100px" CssClass="textbox" ReadOnly="True"></asp:TextBox><img runat="server" style="CURSOR: hand" src="~/Skin/Image/query.gif" id="cmdSelectItem"
										onclick="OpenSelectTypeWindow(); return false;" NAME="cmdSelectItem"><input type="hidden" runat="server" id="txtTransTypeQuery" name="txtTransTypeQuery"></td>
								<td class="fieldName" noWrap><asp:label id="lblItemIDQuery" runat="server">物料代码</asp:label></td>
								<td><asp:TextBox Runat="server" ID="txtItemCodeQuery" Width="130px" CssClass="textbox"></asp:TextBox></td>
								<td class="fieldName" noWrap><asp:label id="lblMOCodeQuery" runat="server">工单号</asp:label></td>
								<td><asp:TextBox Runat="server" ID="txtMOCodeQuery" Width="130px" CssClass="textbox"></asp:TextBox><img src="~/Skin/Image/query.gif" id="Image1" style="CURSOR: hand" runat="server" onclick="SelectMOItem();return false;"
										NAME="cmdSelectItem"></td>
								<td align="center" colspan="2"><INPUT class="submitImgButton" id="cmdQuery" type="submit" value="查 询" name="btnQuery"
										tabindex="0" runat="server"></td>
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
								<TD class="smallImgButton"><INPUT class="gridExportButton" id="cmdGridExport" type="submit" value="  " name="cmdGridExport"
										runat="server"> |
								</TD>
								<TD><cc1:pagersizeselector id="pagerSizeSelector" runat="server"></cc1:pagersizeselector></TD>
								<td align="right">
									<cc1:PagerToolbar id="pagerToolBar" runat="server"></cc1:PagerToolbar></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
