<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Page language="c#" Codebehind="FTrans2ItemLotSP.aspx.cs" AutoEventWireup="True" Inherits="BenQGuru.eMES.Web.WarehouseWeb.FTrans2ItemLotSP" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>批量新增</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href="<%=StyleSheet%>" rel=stylesheet>
		<base target="_self">
		<script language="javascript">
			function CloseWindow()
			{
				window.opener.document.location.replace(window.opener.document.location.href);
				window.close();
			}
			
		</script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" height="100%" cellSpacing="1" cellPadding="1" width="100%" border="0">
				<tr class="moduleTitle">
					<td>
						<asp:label id="lblFTrans2ItemLotSPTitle" runat="server">批量新增</asp:label></td>
				</tr>
				<TR>
					<TD noWrap>
						<TABLE class="query" id="Table2" height="100%" width="100%">
							<TR>
								<TD class="fieldNameLeft" noWrap><asp:label id="lblItemIDQuery" runat="server">物料代码</asp:label></TD>
								<TD class="fieldValue"><asp:textbox id="txtItemCodeQuery" runat="server" CssClass="textbox" Width="120px"></asp:textbox></TD>
								<TD class="fieldName" noWrap><asp:label id="lblItemNQuery" runat="server">物料名称</asp:label></TD>
								<TD class="fieldValue" noWrap><asp:textbox id="txtItemNameQuery" runat="server" CssClass="textbox" Width="120px"></asp:textbox></TD>
								<TD noWrap width="100%">&nbsp;</TD>
								<TD class="fieldName"><INPUT class="submitImgButton" id="cmdQuery" onmouseover="" onmouseout="" type="submit"
										value="查 询" name="cmdQuery" runat="server">
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD noWrap height="50%"><igtbl:ultrawebgrid id="gridUnselected" runat="server" Width="100%" Height="100%">
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
						</igtbl:ultrawebgrid></TD>
				</TR>
				<TR>
					<TD noWrap>
						<TABLE id="Table3" height="100%" cellPadding="0" width="100%">
							<TR>
								<TD><cc1:pagersizeselector id="pagerSizeSelector" runat="server"></cc1:pagersizeselector></TD>
								<td align="right"><cc1:pagertoolbar id="pagerToolBar" runat="server"></cc1:pagertoolbar></td>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD noWrap align="center">
						<asp:button id="cmdSelect" style="BACKGROUND-IMAGE: url(../Skin/Image/down.gif)" runat="server"
							CssClass="smallbutton"></asp:button>&nbsp;<asp:button id="cmdUnSelect" style="BACKGROUND-IMAGE: url(../Skin/Image/up.gif)" runat="server"
							CssClass="smallbutton"></asp:button></TD>
				</TR>
				<TR>
					<TD noWrap height="50%"><igtbl:ultrawebgrid id="gridSelected" runat="server" Width="100%" Height="100%">
							<DisplayLayout ColWidthDefault="" StationaryMargins="Header" AllowSortingDefault="Yes" RowHeightDefault="20px"
								Version="2.00" SelectTypeRowDefault="Single" SelectTypeCellDefault="Single" HeaderClickActionDefault="SortSingle"
								BorderCollapseDefault="Separate" AllowColSizingDefault="Free" CellPaddingDefault="4" RowSelectorsDefault="No"
								Name="Ultrawebgrid1" TableLayout="Fixed">
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
						</igtbl:ultrawebgrid>
					</TD>
				</TR>
				<tr class="normal">
					<td align="left">
						<table class="edit" height="100%" cellPadding="0" width="100%">
							<tr>
								<td class="fieldName" noWrap><asp:label id="lblMOCodeQuery" runat="server">工单号</asp:label></td>
								<td class="fieldValue"><asp:textbox id="txtMOCodeEdit" runat="server" Width="130px" CssClass="textbox"></asp:textbox></td>
							</tr>
						</table>
					</td>
				</tr>
				<TR>
					<TD noWrap align="center">&nbsp;<INPUT class="submitImgButton" id="cmdSave" type="submit" value="保 存" name="cmdSave" runat="server" onserverclick="cmdSave_ServerClick">&nbsp;<INPUT class="submitImgButton" id="cmdCancel" type="submit" value="取 消" name="cmdCancel"
							runat="server" onclick="window.close();"></TD>
				</TR>
			</TABLE>
			<div style="DISPLAY:none">
				<asp:checkbox id="chbSelected" runat="server" AutoPostBack="True"></asp:checkbox>
				<asp:checkbox id="chbUnSelected" runat="server" AutoPostBack="True"></asp:checkbox>
				<INPUT type="button" value="init" id="cmdInit" runat="server" NAME="cmdInit"> <TEXTAREA id="txtSelected" rows="1" cols="20" runat="server" NAME="txtSelected"></TEXTAREA>
			</div>
		</form>
	</body>
</HTML>
