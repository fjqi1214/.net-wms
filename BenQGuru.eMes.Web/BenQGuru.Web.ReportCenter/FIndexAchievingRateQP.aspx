<%@ Page language="c#" Codebehind="FIndexAchievingRateQP.aspx.cs" AutoEventWireup="True" Inherits="BenQGuru.eMES.Web.WebQuery.FIndexAchievingRateQP" %>
<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="cc2" NameSpace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<%@ Register TagPrefix="uc1" TagName="eMESDate" Src="~/UserControl/DateTime/DateTime/eMESDate.ascx" %>
<%@ Register Src="UserControls/UCLineChartProcess.ascx" TagName="UCLineChartProcess" TagPrefix="uc4"%>
<%@ Register Src="UserControls/UCColumnChartProcess.ascx" TagName="UCColumnChartProcess" TagPrefix="uc5"%>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>

<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<title>FIndexAchievingRateQP</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR" />
		<meta content="C#" name="CODE_LANGUAGE" />
		<meta content="JavaScript" name="vs_defaultClientScript" />
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
		<link href="<%=StyleSheet%>" rel="stylesheet" />
		<base target="_self" />
	</head>
	<body scroll="yes">
		<form id="Form1" method="post" runat="server">
			<table style="width:100%; height:100%;">
				<tr class="moduleTitle">
					<td><asp:label id="lblTitle" runat="server" CssClass="labeltopic">达成率指标</asp:label></td>
				</tr>
				<tr>
                <td>
                    <table class="query" id="Table2" height="100%" width="100%">
                        <tr>
                            <td class="fieldNameLeft" nowrap="nowrap">
                                <asp:Label ID="lblStartDateQuery" runat="server" Text="开始日期"></asp:Label>
                            </td>
                            <td class="fieldValue">
                                <uc1:emesdate id="datStartDateQuery" runat="server" width="131"></uc1:emesdate>
                            </td>
                            <td class="fieldNameLeft" nowrap="nowrap">
                                <asp:Label ID="lblEndDateQuery" runat="server" Text="结束日期"></asp:Label>
                            </td>
                            <td class="fieldValue">
                                <uc1:emesdate id="datEndDateQuery" runat="server" width="131"></uc1:emesdate>
                            </td>
                            <td class="fieldNameLeft" nowrap="nowrap">
                                <asp:Label ID="lblBigSSCodeQuery" runat="server" Text="大线"></asp:Label>
                            </td>
                            <td class="fieldValue">
                                <cc2:selectabletextbox id="txtBigSSCodeQuery" runat="server" type="bigline" readonly="false" cankeyin="true" width="131px"></cc2:selectabletextbox>
                            </td>
                            <td colspan="2" />
                        </tr>
                        <tr>
                            <td class="fieldNameLeft" nowrap="nowrap">
                                <asp:Label ID="lblMaterialModelCodeQuery" runat="server" Text="整机机型"></asp:Label>
                            </td>
                            <td class="fieldValue">
                                <cc2:selectabletextbox id="txtMaterialModelCodeQuery" runat="server" type="mmodelcode" readonly="false" cankeyin="true" width="131px"></cc2:selectabletextbox>
                            </td>
                            <td class="fieldNameLeft" nowrap="nowrap">
                                <asp:Label ID="lblGoodSemiGoodQuery" runat="server" Text="成品/半成品"></asp:Label>
                            </td>
                            <td class="fieldValue">
                                <asp:DropDownList ID="ddlGoodSemiGoodQuery" runat="server" Width="131px"></asp:DropDownList>
                            </td>   
                            <td colspan="4" />
                        </tr>                        
                        <tr>
                            <td class="fieldNameLeft" nowrap="nowrap">
                                <asp:Label ID="lblItemCodeQuery" runat="server" Text="产品代码"></asp:Label>
                            </td>
                            <td class="fieldValue">
                                <cc2:selectabletextbox id="txtItemCodeQuery" runat="server" type="item" readonly="false" cankeyin="true" width="131px"></cc2:selectabletextbox>
                            </td>
                            <td class="fieldNameLeft" nowrap="nowrap">
                                <asp:Label ID="lblMOCodeQuery" runat="server" Text="工单"></asp:Label>
                            </td>
                            <td class="fieldValue">
                                <cc2:selectabletextbox id="txtMOCodeQuery" runat="server" type="mo" readonly="false" cankeyin="true" width="131px"></cc2:selectabletextbox>
                            </td> 
                            <td colspan="3" width="100%"/>                           
                            <td class="fieldName">
                                <input class="submitImgButton" id="cmdQuery" onmouseover="" onmouseout="" type="submit"
                                    value="查 询" name="cmdQuery" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr height="100%">
					<td class="fieldGrid">
						<igtbl:ultrawebgrid id="gridWebGrid" runat="server" width="100%" height="300px">
							<displaylayout colwidthdefault="" stationarymargins="Header" allowsortingdefault="Yes" rowheightdefault="20px"
								version="2.00" selecttyperowdefault="Single" selecttypecelldefault="Single" headerclickactiondefault="SortSingle"
								bordercollapsedefault="Separate" allowcolsizingdefault="Free" cellpaddingdefault="4" rowselectorsdefault="No"
								name="gridWebGrid" tablelayout="Fixed">
								<AddNewBox>
								</AddNewBox>
								<headerstyledefault borderwidth="1px" font-size="12px" font-bold="True" bordercolor="White" borderstyle="Dashed"
									horizontalalign="Left" backcolor="#ABABAB">
									<borderdetails colortop="White" widthleft="1px" colorbottom="White" widthtop="1px" colorright="White"
										colorleft="White"></BorderDetails>
								</HeaderStyleDefault>
								<rowselectorstyledefault backcolor="#EBEBEB"></rowselectorstyledefault>
								<framestyle width="100%" borderwidth="0px" font-size="12px" font-names="Verdana" bordercolor="#ABABAB"
									borderstyle="Groove" height="300px"></framestyle>
								<footerstyledefault borderwidth="0px" borderstyle="Groove" backcolor="LightGray">
									<borderdetails colortop="White" widthleft="1px" widthtop="1px" colorleft="White"></BorderDetails>
								</FooterStyleDefault>
								<ActivationObject borderstyle="Dotted"></ActivationObject>
								<editcellstyledefault verticalalign="Middle" borderwidth="1px" bordercolor="Black" borderstyle="None">
									<padding bottom="1px"></padding>
								</EditCellStyleDefault>
								<rowalternatestyledefault backcolor="White"></RowAlternateStyleDefault>
								<rowstyledefault verticalalign="Middle" borderwidth="1px" bordercolor="#D7D8D9" borderstyle="Solid"
									horizontalalign="Left">
									<padding left="3px"></Padding>
									<borderdetails widthleft="0px" widthtop="0px"></BorderDetails>
								</rowstyledefault>
							</displaylayout>
							<bands>
								<igtbl:UltraGridBand></igtbl:UltraGridBand>
							</Bands>
						</igtbl:ultrawebgrid></td>
				</tr>
				<tr class="normal">
					<td>
						<table height="100%" cellpadding="0" width="100%">
							<tr>
								<td class="smallImgButton"><input class="gridExportButton" id="cmdGridExport" type="submit" value="  " name="cmdGridExport"
										runat="server" /> |
								</td>
								<td>
									<cc1:pagersizeselector id="pagerSizeSelector" runat="server"></cc1:pagersizeselector>
								</td>
								<td align="right">
									<cc1:PagerToolbar id="pagerToolBar" runat="server"></cc1:PagerToolbar>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td>
						<table>
						    <tr>
							    <td align="center">
						           <uc5:uccolumnchartprocess id="columnChart" runat="server"></uc5:uccolumnchartprocess>
						        </td>
						    </tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</html>
