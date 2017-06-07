<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="uc1" TagName="eMesDate" Src="../UserControl/DateTime/DateTime/eMesDate.ascx" %>
<%@ Register TagPrefix="uc1" TagName="eMesTime" Src="../UserControl/DateTime/DateTime/eMesTime.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="cc2" NameSpace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<%@ Page language="c#" Codebehind="FSoftwareVersonQP.aspx.cs" AutoEventWireup="True" Inherits="BenQGuru.eMES.Web.WebQuery.FSoftwareVersonQP" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>FINNOInfoQP</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href="<%=StyleSheet%>" rel=stylesheet>
		<script language="javascript">
			function OnChecked(radiobtnID)
			{
				if("rdbUpToSnuff" == radiobtnID)
				{
					document.all.rdbAbnormity.checked = false;
					SetDateTDDispear();
				}
				else if("rdbAbnormity" == radiobtnID)
				{
					document.all.rdbUpToSnuff.checked = false;
					SetDateTDDisplay();
				}
			}
			
			function SetDateTDDisplay()
			{
				document.all.tdDate.style.display = "block";
				document.all.tdTemp.style.display = "none";
			}
			function SetDateTDDispear()
			{
				document.all.tdDate.style.display = "none";
				document.all.tdTemp.style.display = "block";
			}
			
			
			function Init()
			{
				
				if(true == document.all.rdbUpToSnuff.checked )
				{
					OnChecked(document.all.rdbUpToSnuff.id);
				}
				else
				{
					OnChecked(document.all.rdbAbnormity.id);
				}
				
			}
			
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout" onload="Init()">
		<form id="Form1" method="post" runat="server">
			<table id="Table1" height="100%" width="100%">
				<tr class="moduleTitle">
					<td style="HEIGHT: 19px"><asp:label id="lblTitle" runat="server" CssClass="labeltopic">软件信息查询</asp:label></td>
				</tr>
				<tr>
					<td>
						<table class="query" id="Table2" height="100%" width="100%">
							<TR>
								<td class="fieldNameLeft" noWrap><asp:label id="lblItemCodeQuery" runat="server">产品代码</asp:label></td>
								<td class="fieldValue"><cc2:selectabletextbox id="txtConditionItem" runat="server" Type="item" Width="100px"></cc2:selectabletextbox></td>
								<td class="fieldName" noWrap><asp:label id="lblMOIDQuery" runat="server">工单代码</asp:label></td>
								<td class="fieldValue"><cc2:selectabletextbox id="txtConditionMo" runat="server" Type="mo" Width="100px"></cc2:selectabletextbox></td>
								<td class="fieldNameLeft" noWrap><asp:label id="lblSoftwareNameQuery" runat="server">软件名称</asp:label></td>
								<td class="fieldValue"><asp:textbox id="txtSoftwareNameQuery" runat="server" CssClass="textbox"></asp:textbox></td>
							</TR>
							<tr>
								<td class="fieldNameLeft" noWrap><asp:label id="lblStartRCardQuery" runat="server">起始产品序列号</asp:label></td>
								<td class="fieldValue"><asp:textbox id="txtStartSNQuery" runat="server" CssClass="textbox" Width="165px"></asp:textbox></td>
								<td class="fieldName" noWrap><asp:label id="lblEndRCardQuery" runat="server">截止产品序列号</asp:label></td>
								<td class="fieldValue"><asp:textbox id="txtEndSNQuery" runat="server" CssClass="textbox" Width="165px"></asp:textbox></td>
								<td class="fieldName" noWrap><asp:label id="lblSoftwareVersionQuery" runat="server">软件版本</asp:label></td>
								<td class="fieldValue"><asp:textbox id="txtSoftwareVersionQuery" runat="server" CssClass="textbox"></asp:textbox></td>
							</tr>
							<tr>
								<td class="fieldName" noWrap><asp:RadioButton id="rdbUpToSnuff" runat="server" Text="正常"></asp:RadioButton><asp:RadioButton id="rdbAbnormity" runat="server" Text="异常"></asp:RadioButton></td>
								<td id="tdDate" colspan="3" style="DISPLAY:none">
									<table>
										<tr>
											<td class="fieldName" noWrap><asp:label id="lblBegindate" runat="server">起始日期</asp:label></td>
											<TD class="fieldValue" noWrap>
                                            <asp:TextBox  id="txtBeginDate"  class='datepicker require' runat="server"  Width="130px"/>
                                          </TD>
											<td class="fieldName" noWrap><asp:label id="lblEnddate" runat="server">结束日期</asp:label></td>
											<TD class="fieldValue" noWrap>
                                            <asp:TextBox  id="txtEndDate"  class='datepicker require' runat="server"  Width="1e0px"/>
                                          </TD>
										</tr>
									</table>
								</td>
								<td id="tdTemp" colspan="3"></td>
								<td class="fieldNameLeft"><INPUT class="submitImgButton" id="cmdQuery" type="submit" value="查 询" name="btnQuery"
										runat="server">
								</td>
							</tr>
							<tr>
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
							</igtbl:ultrawebgrid><DISPLAYLAYOUT ColWidthDefault="" StationaryMargins="Header" AllowSortingDefault="Yes" RowHeightDefault="20px"
							Version="2.00" SelectTypeRowDefault="Single" SelectTypeCellDefault="Single" HeaderClickActionDefault="SortSingle" BorderCollapseDefault="Separate"
							AllowColSizingDefault="Free" CellPaddingDefault="4" RowSelectorsDefault="No" Name="webGrid" TableLayout="Fixed"><ADDNEWBOX>
								<STYLE BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
								</STYLE>
							</ADDNEWBOX>
							<PAGER>
								<STYLE BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
								</STYLE>
							</PAGER>
							<HEADERSTYLEDEFAULT BorderWidth="1px" BorderStyle="Dashed" BackColor="#ABABAB" Font-Size="12px" Font-Bold="True"
								BorderColor="White" HorizontalAlign="Left">
								<BORDERDETAILS ColorTop="White" WidthLeft="1px" ColorBottom="White" WidthTop="1px" ColorRight="White"
									ColorLeft="White"></BORDERDETAILS>
							</HEADERSTYLEDEFAULT>
							<ROWSELECTORSTYLEDEFAULT BackColor="#EBEBEB"></ROWSELECTORSTYLEDEFAULT>
							<FRAMESTYLE Width="100%" Height="100%" BorderWidth="0px" BorderStyle="Groove" Font-Size="12px"
								BorderColor="#ABABAB" Font-Names="Verdana"></FRAMESTYLE>
							<FOOTERSTYLEDEFAULT BorderWidth="0px" BorderStyle="Groove" BackColor="LightGray">
								<BORDERDETAILS ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BORDERDETAILS>
							</FOOTERSTYLEDEFAULT>
							<ACTIVATIONOBJECT BorderStyle="Dotted"></ACTIVATIONOBJECT>
							<EDITCELLSTYLEDEFAULT BorderWidth="1px" BorderStyle="None" BorderColor="Black" VerticalAlign="Middle">
								<PADDING Bottom="1px"></PADDING>
							</EDITCELLSTYLEDEFAULT>
							<ROWALTERNATESTYLEDEFAULT BackColor="White"></ROWALTERNATESTYLEDEFAULT>
							<ROWSTYLEDEFAULT BorderWidth="1px" BorderStyle="Solid" BorderColor="#D7D8D9" HorizontalAlign="Left"
								VerticalAlign="Middle">
								<PADDING Left="3px"></PADDING>
								<BORDERDETAILS WidthLeft="0px" WidthTop="0px"></BORDERDETAILS>
							</ROWSTYLEDEFAULT>
							<IMAGEURLS ImageDirectory="/ig_common/WebGrid3/"></IMAGEURLS>
						</DISPLAYLAYOUT><BANDS><IGTBL:ULTRAGRIDBAND></IGTBL:ULTRAGRIDBAND>
						</BANDS></td>
				</tr>
				<tr class="normal">
					<td>
						<table id="Table3" height="100%" cellPadding="0" width="100%">
							<tr>
								<TD class="smallImgButton"><INPUT class="gridExportButton" id="cmdGridExport" type="submit" value="  " name="cmdGridExport"
										runat="server"> |
								</TD>
								<TD>
									<cc1:pagersizeselector id="pagerSizeSelector" runat="server"></cc1:pagersizeselector>
								</TD>
								<td align="right">
									<cc1:PagerToolbar id="pagerToolBar" runat="server"></cc1:PagerToolbar>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
