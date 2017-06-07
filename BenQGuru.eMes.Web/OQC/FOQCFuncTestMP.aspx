<%@ Page language="c#" Codebehind="FOQCFuncTestMP.aspx.cs" AutoEventWireup="True" Inherits="BenQGuru.eMES.Web.OQC.FOQCFuncTestMP" %>
<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>FSolutionMP</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="<%=StyleSheet%>" rel=stylesheet>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table height="100%" width="100%">
				<tr class="moduleTitle">
					<td><asp:label id="lblTitle" runat="server" CssClass="labeltopic">OQC功能测试标准</asp:label></td>
				</tr>
				<tr>
					<td>
						<table class="query" height="100%" width="100%">
							<tr>
								<td class="fieldNameLeft" noWrap><asp:label id="lblItemCodeQuery" runat="server">产品代码</asp:label></td>
								<td class="fieldValue">
									<asp:TextBox id="txtItemCode" runat="server" CssClass="textbox" ReadOnly="True"></asp:TextBox></td>
								<td noWrap width="100%"></td>
								<td class="fieldName"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr class="toolBar">
					<td class="fieldGrid"></td>
				</tr>
				<tr class="normal">
					<td>
						<table class="edit" height="100%" cellPadding="0" width="100%">
							<tr style="DISPLAY:none">
								<td class="fieldNameLeft" noWrap><asp:label id="lblMinDutyRatoMin" runat="server">Min Duty Rato最小值</asp:label></td>
								<td class="fieldValue"><asp:TextBox id="txtMinDutyRatoMin" CssClass="textbox" runat="server"></asp:TextBox></td>
								<td class="fieldName" noWrap><asp:label id="lblMinDutyRatoMax" runat="server">Min Duty Rato最大值</asp:label></td>
								<td class="fieldValue"><asp:TextBox id="txtMinDutyRatoMax" runat="server" CssClass="textbox"></asp:TextBox></td>
								<TD class="fieldValue" width="100%"></TD>
							</tr>
							<tr style="DISPLAY:none">
								<td class="fieldNameLeft" noWrap><asp:label id="lblBurstMdFreMin" runat="server">Burst md频率最小值</asp:label></td>
								<td class="fieldValue"><asp:TextBox id="txtBurstMdFreMin" CssClass="textbox" runat="server"></asp:TextBox></td>
								<td class="fieldName" noWrap><asp:label id="lblBurstMdFreMax" runat="server">Burst md频率最大值</asp:label></td>
								<td class="fieldValue"><asp:TextBox id="txtBurstMdFreMax" runat="server" CssClass="textbox"></asp:TextBox></td>
								<TD class="fieldValue" width="100%"></TD>
							</tr>
							<tr>
								<td class="fieldNameLeft" noWrap><asp:label id="lblElectricCount" runat="server">每组电流个数</asp:label></td>
								<td class="fieldValue"><asp:TextBox id="txtElectricCount" CssClass="textbox" runat="server"></asp:TextBox></td>
								<td class="fieldName" noWrap><asp:label id="lblGroupCount" runat="server">组数</asp:label></td>
								<td class="fieldValue"><table>
										<tr>
											<td><asp:TextBox id="txtGroupCount" runat="server" CssClass="textbox"></asp:TextBox></td>
											<td><asp:ImageButton Runat="server" ID="btnInitGroupCount" ImageUrl="../Skin/Image/go.gif" Visible="False"></asp:ImageButton></td>
										</tr>
									</table>
								</td>
								<TD class="fieldValue" width="100%"></TD>
							</tr>
						</table>
					</td>
				</tr>
				<tr height="100%">
					<td class="fieldGrid" style="DISPLAY:none">
						<igtbl:ultrawebgrid id="gridWebGrid" runat="server" Width="100%" Height="200">
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
				<tr height="100%">
					<td>&nbsp;</td>
				</tr>
				<tr class="toolBar">
					<td>
						<table class="toolBar">
							<tr>
								<td style="DISPLAY:none" class="toolBar"><INPUT class="submitImgButton" id="cmdAdd" type="submit" value="新 增" name="cmdAdd" runat="server"></td>
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
