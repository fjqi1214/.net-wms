<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FMaterialNeed.aspx.cs" Inherits="BenQGuru.eMES.Web.WarehouseWeb.FMaterialNeed" %>

<%@ Register TagPrefix="uc1" TagName="eMESDate" Src="~/UserControl/DateTime/DateTime/eMESDate.ascx" %>
<%@ Register TagPrefix="cc2" NameSpace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>

<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>产品要料标准维护</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href="<%=StyleSheet%>" rel=stylesheet>
		
	           
	</HEAD>
	<body  MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table height="100%" width="100%">
				<tr class="moduleTitle">
					<td><asp:label id="lblTitle" runat="server" CssClass="labeltopic">产品要料标准维护</asp:label></td>
				</tr>
				<tr>
					<td>
						<table class="query" height="100%" width="100%">
							<tr>
								 <td class="fieldNameLeft" nowrap>
                                <asp:Label ID="lblItemIDQuery" runat="server">物料代码</asp:Label></td>
                            <td class="fieldValue" style="width: 132px">
                                <cc2:SelectableTextBox ID="txtMaterialCodeQuery"  CanKeyIn="true" runat="server" Width="120px"
                                    Type="material"></cc2:SelectableTextBox></td>
		                         <td class="fieldName" nowrap>
                                <asp:Label ID="lblFirstClassGroup" runat="server">一级分类</asp:Label></td>
                            <td class="fieldValue">
                                <asp:DropDownList ID="drpFirstClassQuery" runat="server" CssClass="textbox" Width="110px"
                                    AutoPostBack="true" OnLoad="drpFirstClass_Load" OnSelectedIndexChanged="drpFirstClass_SelectedIndexChanged">
                                </asp:DropDownList></td>
                                
                                 <td class="fieldName" nowrap>
                                <asp:Label ID="lblSecondClassGroup" runat="server">二级分类</asp:Label></td>
                            <td class="fieldValue">
                                <asp:DropDownList ID="drpSecondClassQuery" runat="server" CssClass="textbox" Width="110px"
                                    AutoPostBack="true" OnSelectedIndexChanged="drpSecondClass_SelectedIndexChanged">
                                </asp:DropDownList></td>
                            <td class="fieldName" nowrap>
                                <asp:Label ID="lblThirdClassGroup" runat="server">三级分类</asp:Label></td>
                            <td class="fieldValue">
                                <asp:DropDownList ID="drpThirdClassQuery" runat="server" CssClass="textbox" Width="110px"
                                    AutoPostBack="False">
                                </asp:DropDownList></td>				
								

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
								<td><asp:checkbox id="chbSelectAll" runat="server" AutoPostBack="True" Text="全选"></asp:checkbox></td>
								<TD class="smallImgButton"><INPUT class="gridExportButton" id="cmdGridExport" type="submit" value="  " name="cmdGridExport" 
										runat="server"> |
								</TD>
								<TD class="smallImgButton"><INPUT class="deleteButton" id="cmdDelete" type="submit"  value="  " name="cmdDelete" runat="server">
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
								<td class="fieldName" style="HEIGHT: 26px" noWrap><asp:label id="lblItemIDEdit" runat="server">物料代码</asp:label></td>
								<td style="HEIGHT: 26px"> <cc2:SelectableTextBox ID="txtItemCodeEdit"  CssClass="require" CanKeyIn="true" runat="server" Width="120px"
                                    Type="singlematerial"></cc2:SelectableTextBox></td>
		 <td  colspan=2><asp:label id="lblItemDesc" runat="server" ></asp:label></td>
																
		
		                       <td class="fieldName" noWrap><asp:label id="lblQty" runat="server">数量</asp:label></td>
								<td><asp:textbox id="txtQTY" runat="server" CssClass="require" Width="130px" ></asp:textbox></td>						
						 <td class="fieldName" noWrap><asp:label id="lblOrgEdit" runat="server" >组织</asp:label></td>
								<td class="fieldValue"><asp:dropdownlist id="DropDownListOrg" runat="server" CssClass="require" Width="130px" ></asp:dropdownlist></td>
								
							</tr>
							
						</table>
					</td>
				</tr>
				
				<tr class="toolBar">
					<td>
						<table class="toolBar">
							<tr>
								<td class="toolBar">
								<INPUT class="submitImgButton" id="cmdAdd" type="submit" value="新 增" name="cmdAdd" runat="server" ></td>
								<td class="toolBar"><INPUT class="submitImgButton" id="cmdSave" type="submit" value="保 存" name="cmdSave" runat="server"></td>
								<td class="toolBar"><INPUT class="submitImgButton" id="cmdCancel" type="submit" value="取 消" name="cmdCancel"
										runat="server"></td>
										<TD ><INPUT class="submitImgButton" id="cmdExport" visible="false" type="submit" value="导出"
										name="cmdExport" runat="server" onserverclick="cmdMOExport_ServerClick"></TD>
										
								<td class="toolBar"><INPUT class="submitImgButton" id="cmdEnter" type="submit" value="导 入" 
								        name="cmdEnter" runat="server" onserverclick="cmdImport_ServerClick"></td>
							</tr>
						</table>
					</td>
				</tr>
				
			</table>
		</form>
	</body>
</HTML>
