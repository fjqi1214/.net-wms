<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="uc1" TagName="eMESDate" Src="~/UserControl/DateTime/DateTime/eMESDate.ascx" %>
<%@ Page language="c#" Codebehind="FItem2SPCTestMP.aspx.cs" AutoEventWireup="True" Inherits="BenQGuru.eMES.Web.MOModel.FItem2SPCTestMP" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>FItem2SPCTblMP</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href="<%=StyleSheet%>" rel=stylesheet>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table height="97%" width="95%">
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
								<TD class="smallImgButton"><INPUT class="deleteButton" id="cmdDelete" type="submit" value="  " name="cmdDelete" runat="server">
									|
								</TD>
								<TD><cc1:pagersizeselector id="pagerSizeSelector" runat="server"></cc1:pagersizeselector></TD>
								<td align="right"><cc1:pagertoolbar id="pagerToolBar" runat="server"></cc1:pagertoolbar></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr class="normal">
					<td>
						<table class="edit" height="100%" cellPadding="0" width="100%">
							<tr>
								<td class="fieldNameLeft" noWrap><asp:label id="lblItemNameEdit" runat="server">产品名称</asp:label></td>
								<td class="fieldValue"><asp:textbox id="txtItemCodeEdit" runat="server" ReadOnly="True" CssClass="require"></asp:textbox></td>
								<td class="fieldName" noWrap></td>
								<td class="fieldValue"><asp:checkbox id="chbAutoCLEdit" runat="server" Text="自动生成UCL/LCL" AutoPostBack="True" oncheckedchanged="chbAuto_CheckedChanged"></asp:checkbox></td>
								<!--onclick="if(document.all.chbAuto.checked) {document.all.txtUCLEdit.value='';document.all.txtLCLEdit.value='';} document.all.txtUCLEdit.disabled=document.all.chbAuto.checked;document.all.txtLCLEdit.disabled=document.all.chbAuto.checked;"-->
								<td class="fieldNameLeft" noWrap><asp:checkbox id="chbLowEdit" runat="server" Text="单边下限规则" AutoPostBack="True" oncheckedchanged="chbLowOnly_CheckedChanged"></asp:checkbox></td>
								<td class="fieldNameLeft" noWrap><asp:label id="lblUSLEdit" runat="server">USL</asp:label></td>
								<td class="fieldValue"><asp:textbox id="txtUSLEdit" runat="server" CssClass="textbox"></asp:textbox></td>
								<td class="fieldNameLeft"><asp:label id="lblUCLEdit" runat="server">UCL</asp:label></td>
								<td class="fieldValue"><asp:textbox id="txtUCLEdit" runat="server" CssClass="textbox"></asp:textbox></td>
							</tr>
							<tr>
								<td class="fieldNameLeft" noWrap><asp:label id="lblTestNameEdit" runat="server">测试项</asp:label></td>
								<td class="fieldValue"><asp:textbox id="txtTestNameEdit" runat="server" CssClass="require"></asp:textbox></td>
								<td class="fieldName" noWrap><asp:label id="lblSeqEdit" runat="server">序号</asp:label></td>
								<td class="fieldValue"><asp:textbox id="txtSeqEdit" runat="server" CssClass="require"></asp:textbox></td>
								<td class="fieldNameLeft" noWrap><asp:checkbox id="chbUpEdit" runat="server" Text="单边上限规则" AutoPostBack="True" oncheckedchanged="chbUpOnly_CheckedChanged"></asp:checkbox></td>
								<TD class="fieldNameLeft">
									<asp:Label id="lblLSLEdit" runat="server">LSL</asp:Label></TD>
								<TD class="fieldValue" noWrap>
									<asp:TextBox id="txtLSLEdit" runat="server" CssClass="textbox"></asp:TextBox></TD>
								<td class="fieldNameLeft"><asp:label id="lblLCLEdit" runat="server">LCL</asp:label></td>
								<td class="fieldValue"><asp:textbox id="txtLCLEdit" runat="server" CssClass="textbox"></asp:textbox></td>
							</tr>
							<tr>
								<td class="fieldNameLeft" noWrap><asp:label id="lblMemoEdit" runat="server">备注</asp:label></td>
								<td class="fieldValue"><asp:textbox id="txtDescEdit" runat="server" CssClass="textbox"></asp:textbox></td>
								<td class="fieldValue"><asp:textbox id="txtOID" runat="server" Height="0px" Width="0px"></asp:textbox></td>
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
