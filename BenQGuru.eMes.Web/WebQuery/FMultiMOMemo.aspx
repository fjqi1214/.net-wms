<%@ Page language="c#" Codebehind="FMultiMOMemo.aspx.cs" AutoEventWireup="True" Inherits="BenQGuru.eMES.Web.WebQuery.FMultiMOMemo" %>
<%@ Register TagPrefix="uc1" TagName="eMESDate" Src="~/UserControl/DateTime/DateTime/eMESDate.ascx" %>
<%@ Register TagPrefix="cc2" NameSpace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML xmlns:igtbl>
	<HEAD>
		<title>FConfigEx</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="<%=StyleSheet%>" rel=stylesheet>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		
			<FORM id="Form1" method="post" runat="server">
				<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 8px; POSITION: absolute; TOP: 8px" height="100%"
					width="100%">
					<TR class="moduleTitle">
						<TD style="HEIGHT: 19px">
							<asp:label id="lblMOMemoInfoTitle" runat="server" CssClass="labeltopic">多工单归属备注信息查询</asp:label></TD>
					</TR>
					<TR>
						<TD>
							<TABLE class="query" id="Table2" height="100%" width="100%">
								<TR>
									<TD class="fieldNameLeft" noWrap style="HEIGHT: 31px">
										<asp:label id="lblItemCodeQuery" runat="server">产品代码</asp:label></TD>
									<TD class="fieldValue" style="HEIGHT: 31px">
										<cc2:selectabletextbox id="txtConditionItem" runat="server" Width="100px" Type="item"></cc2:selectabletextbox></TD>
									<TD class="fieldName" noWrap style="HEIGHT: 31px">
										<asp:label id="lblMOIDQuery" runat="server">工单代码</asp:label></TD>
									<TD class="fieldValue" style="HEIGHT: 31px">
										<cc2:selectabletextbox id="txtConditionMo" runat="server" Width="100px" Type="mo"></cc2:selectabletextbox></TD>
									<TD class="fieldName" noWrap style="HEIGHT: 31px"></TD>
									<TD class="fieldValue" style="HEIGHT: 31px"></TD>
									<TD width="100%" noWrap style="HEIGHT: 31px"></TD>
									<TD style="HEIGHT: 31px">
									</TD>
								</TR>
								<TR>
									<TD class="fieldNameLeft" noWrap>
										<asp:label id="lblStartRCardQuery" runat="server">起始产品序列号</asp:label></TD>
									<TD class="fieldValue">
										<asp:TextBox id="txtRCardStart" runat="server" CssClass="textbox" Width="165px"></asp:TextBox></TD>
									<TD class="fieldName" noWrap>
										<asp:label id="lblEndRCardQuery" runat="server">截止产品序列号</asp:label></TD>
									<TD class="fieldValue">
										<asp:TextBox id="txtRCardEnd" runat="server" CssClass="textbox" Width="165px"></asp:TextBox></TD>
									<TD class="fieldName" nowrap></TD>
									<TD class="fieldValue">
									</TD>
									<td></td>
									<td></td>
								</TR>
								<TR>
									<TD class="fieldNameLeft" noWrap>
										<asp:label id="lblStartMemo" runat="server">起始备注</asp:label></TD>
									<TD class="fieldValue">
										<asp:TextBox id="txtStartMeno" runat="server" CssClass="textbox"></asp:TextBox></TD>
									<TD class="fieldName" noWrap>
										<asp:label id="lblEndMemo" runat="server">结束备注</asp:label></TD>
									<TD class="fieldValue">
										<asp:TextBox id="txtEndMeno" runat="server" CssClass="textbox"></asp:TextBox></TD>
									<TD class="fieldName" noWrap></TD>
									<td class="fieldValue"></td>
									<td></td>
									<TD class="fieldName"><INPUT class="submitImgButton" id="cmdQuery" type="submit" value="查 询" name="btnQuery"
											runat="server">
									</TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR height="100%">
						<TD class="fieldGrid">
								<igtbl:ultrawebgrid id="gridWebGrid" runat="server" Width="100%" Height="100%">
									<DISPLAYLAYOUT TableLayout="Fixed" Name="webGrid" RowSelectorsDefault="No" CellPaddingDefault="4"
										AllowColSizingDefault="Free" BorderCollapseDefault="Separate" HeaderClickActionDefault="SortSingle"
										SelectTypeCellDefault="Single" SelectTypeRowDefault="Single" Version="2.00" RowHeightDefault="20px"
										AllowSortingDefault="Yes" StationaryMargins="Header" ColWidthDefault="">
										<ADDNEWBOX>
											<STYLE BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px"></STYLE>
										</ADDNEWBOX>
										<PAGER>
											<STYLE BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px"></STYLE>
										</PAGER>
										<HEADERSTYLEDEFAULT BackColor="#ABABAB" BorderStyle="Dashed" BorderWidth="1px" HorizontalAlign="Left"
											BorderColor="White" Font-Bold="True" Font-Size="12px">
											<BORDERDETAILS ColorLeft="White" ColorRight="White" WidthTop="1px" ColorBottom="White" WidthLeft="1px"
												ColorTop="White"></BORDERDETAILS>
										</HEADERSTYLEDEFAULT>
										<ROWSELECTORSTYLEDEFAULT BackColor="#EBEBEB"></ROWSELECTORSTYLEDEFAULT>
										<FRAMESTYLE Width="100%" Height="100%" BorderStyle="Groove" BorderWidth="0px" BorderColor="#ABABAB"
											Font-Size="12px" Font-Names="Verdana"></FRAMESTYLE>
										<FOOTERSTYLEDEFAULT BackColor="LightGray" BorderStyle="Groove" BorderWidth="0px">
											<BORDERDETAILS ColorLeft="White" WidthTop="1px" WidthLeft="1px" ColorTop="White"></BORDERDETAILS>
										</FOOTERSTYLEDEFAULT>
										<ACTIVATIONOBJECT BorderStyle="Dotted"></ACTIVATIONOBJECT>
										<EDITCELLSTYLEDEFAULT BorderStyle="None" BorderWidth="1px" BorderColor="Black" VerticalAlign="Middle">
											<PADDING Bottom="1px"></PADDING>
										</EDITCELLSTYLEDEFAULT>
										<ROWALTERNATESTYLEDEFAULT BackColor="White"></ROWALTERNATESTYLEDEFAULT>
										<ROWSTYLEDEFAULT BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" BorderColor="#D7D8D9"
											VerticalAlign="Middle">
											<PADDING Left="3px"></PADDING>
											<BORDERDETAILS WidthTop="0px" WidthLeft="0px"></BORDERDETAILS>
										</ROWSTYLEDEFAULT>
										<IMAGEURLS ImageDirectory="/ig_common/WebGrid3/"></IMAGEURLS>
									</DISPLAYLAYOUT>
									<BANDS>
										<igtbl:UltraGridBand></igtbl:UltraGridBand>
									</BANDS>
								</igtbl:ultrawebgrid><DISPLAYLAYOUT TableLayout="Fixed" Name="webGrid" RowSelectorsDefault="No" CellPaddingDefault="4"
								AllowColSizingDefault="Free" BorderCollapseDefault="Separate" HeaderClickActionDefault="SortSingle" SelectTypeCellDefault="Single"
								SelectTypeRowDefault="Single" Version="2.00" RowHeightDefault="20px" AllowSortingDefault="Yes" StationaryMargins="Header" ColWidthDefault=""><ADDNEWBOX>
									<STYLE BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
									</STYLE>
								</ADDNEWBOX>
								<PAGER>
									<STYLE BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
									</STYLE>
								</PAGER>
								<HEADERSTYLEDEFAULT BackColor="#ABABAB" BorderStyle="Dashed" BorderWidth="1px" HorizontalAlign="Left"
									BorderColor="White" Font-Bold="True" Font-Size="12px">
									<BORDERDETAILS ColorLeft="White" ColorRight="White" WidthTop="1px" ColorBottom="White" WidthLeft="1px"
										ColorTop="White"></BORDERDETAILS>
								</HEADERSTYLEDEFAULT>
								<ROWSELECTORSTYLEDEFAULT BackColor="#EBEBEB"></ROWSELECTORSTYLEDEFAULT>
								<FRAMESTYLE Width="100%" Height="100%" BorderStyle="Groove" BorderWidth="0px" BorderColor="#ABABAB"
									Font-Size="12px" Font-Names="Verdana"></FRAMESTYLE>
								<FOOTERSTYLEDEFAULT BackColor="LightGray" BorderStyle="Groove" BorderWidth="0px">
									<BORDERDETAILS ColorLeft="White" WidthTop="1px" WidthLeft="1px" ColorTop="White"></BORDERDETAILS>
								</FOOTERSTYLEDEFAULT>
								<ACTIVATIONOBJECT BorderStyle="Dotted"></ACTIVATIONOBJECT>
								<EDITCELLSTYLEDEFAULT BorderStyle="None" BorderWidth="1px" BorderColor="Black" VerticalAlign="Middle">
									<PADDING Bottom="1px"></PADDING>
								</EDITCELLSTYLEDEFAULT>
								<ROWALTERNATESTYLEDEFAULT BackColor="White"></ROWALTERNATESTYLEDEFAULT>
								<ROWSTYLEDEFAULT BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" BorderColor="#D7D8D9"
									VerticalAlign="Middle">
									<PADDING Left="3px"></PADDING>
									<BORDERDETAILS WidthTop="0px" WidthLeft="0px"></BORDERDETAILS>
								</ROWSTYLEDEFAULT>
								<IMAGEURLS ImageDirectory="/ig_common/WebGrid3/"></IMAGEURLS>
							</DISPLAYLAYOUT><BANDS>
								<IGTBL:ULTRAGRIDBAND></IGTBL:ULTRAGRIDBAND>
							</BANDS></TD>
					</TR>
					<TR class="normal">
						<TD>
							<TABLE id="Table3" height="100%" cellPadding="0" width="100%">
								<TR>
									<TD class="smallImgButton" nowrap><INPUT class="gridExportButton" id="cmdGridExport" type="submit" value="  " name="cmdGridExport"
											runat="server"> |
									</TD>
									<TD>
										<cc1:pagersizeselector id="pagerSizeSelector" runat="server"></cc1:pagersizeselector></TD>
									<TD align="right">
										<cc1:PagerToolbar id="pagerToolBar" runat="server"></cc1:PagerToolbar></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
				</TABLE>
			</FORM>
		
	</body>
</HTML>
