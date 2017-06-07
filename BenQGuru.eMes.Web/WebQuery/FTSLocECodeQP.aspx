<%@ Page language="c#" Codebehind="FTSLocECodeQP.aspx.cs" AutoEventWireup="True" Inherits="BenQGuru.eMES.Web.WebQuery.FTSLocECodeQP" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="uc1" TagName="eMESDate" Src="~/UserControl/DateTime/DateTime/eMESDate.ascx" %>
<%@ Register TagPrefix="cc2" NameSpace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register src="UserControls/UCColumnChartProcess.ascx" tagname="UCColumnChartProcess" tagprefix="uc3" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>FTSLocECodeQP</title>
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
					<td><asp:label id="lblTitle" runat="server" CssClass="labeltopic">不良位置原因分析</asp:label></td>
				</tr>
				<tr>
					<td>
						<table class="query" height="100%" width="100%">
							<TR>
								<td class="fieldNameLeft" noWrap><asp:label id="lblItemCodeQuery" runat="server">产品代码</asp:label></td>
								<td class="fieldValue"><cc2:selectabletextbox id="txtItemCodeQuery" runat="server" Type=" " Target=" "></cc2:selectabletextbox></td>
								<td class="fieldName" noWrap><asp:label id="lblMOIDQuery" runat="server">工单代码</asp:label></td>
								<td class="fieldValue"><cc2:selectabletextbox id="txtMoCodeQuery" runat="server" Type=" " Target=" "></cc2:selectabletextbox></td>
								<td class="fieldName" noWrap style="DISPLAY:none"><asp:label id="lblFactoryCode" runat="server">工厂</asp:label></td>
								<td class="fieldValue" style="DISPLAY:none"><asp:DropDownList ID="dFactoryCode" width="130" Runat="server" CssClass="require"></asp:DropDownList></td>
								<td width="100%" colspan="3"></td>
								<td></td>
							</TR>
							<TR>
								<td class="fieldNameLeft" noWrap><asp:label id="lblStartDateQuery" runat="server">起始日期</asp:label></td>
								<td class="fieldValue">
                                <asp:TextBox  id="dateStartDateQuery"  class='datepicker require' runat="server"  Width="130px"/>
                                </td>
								<td class="fieldName" noWrap><asp:label id="lblEndDateQuery" runat="server">结束日期</asp:label></td>
								<td class="fieldValue">
                                <asp:TextBox  id="dateEndDateQuery"  class='datepicker require' runat="server"  Width="130px"/>
                               </td>
								<td class="fieldNameLeft" noWrap><asp:label id="lblVisibleStyle" runat="server">显示样式</asp:label></td>
								<td width="100%"><asp:radiobuttonlist id="rblVisibleStyle" runat="server" RepeatDirection="Horizontal"></asp:radiobuttonlist></td>
								<td></td>
								<td class="fieldName"><INPUT class="submitImgButton" id="cmdQuery" type="submit" value="查 询" name="btnQuery"
										runat="server" onserverclick="cmdQuery_ServerClick"></td>
							</TR>
						</table>
					</td>
				</tr>
			 <tr height="360px">
                <td>
					<table height="100%" width="100%">
				<tr height="100%">
					        <td class="fieldGrid" id="GridTd" runat="server"><igtbl:ultrawebgrid id="gridWebGrid" runat="server" Width="100%" Height="100%">
							        <DisplayLayout ColWidthDefault="" StationaryMargins="Header" RowHeightDefault="20px" Version="2.00"
								        SelectTypeRowDefault="Single" SelectTypeCellDefault="Single" BorderCollapseDefault="Separate"
								        AllowColSizingDefault="Free" CellPaddingDefault="4" RowSelectorsDefault="No" Name="webGrid"
								        TableLayout="Fixed">
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
						        
				 </tr >
				        <tr height="100%">
                                <td align="center">
                                    <uc3:UCColumnChartProcess ID="columnChart" runat="server" />
                                </td>
                            </tr>
                            </table>
                            </td>
                            </tr>
				
				<tr height="100%">
					<td valign="top">		
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
