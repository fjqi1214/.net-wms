<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="uc1" TagName="eMesDate" Src="../UserControl/DateTime/DateTime/eMesDate.ascx" %>
<%@ Register TagPrefix="uc1" TagName="eMesTime" Src="../UserControl/DateTime/DateTime/eMesTime.ascx" %>
<%@ Page language="c#" Codebehind="FReworkRangeAP.aspx.cs" AutoEventWireup="True" Inherits="BenQGuru.eMES.Web.Rework.FReworkRangeAP" %>
<%@ Register TagPrefix="cc2" Namespace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>FReworkRangeAP</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="<%=StyleSheet%>" rel=stylesheet>
	</HEAD>
	<body>
		<FORM id="Form1" method="post" runat="server">
			<TABLE id="Table1" height="100%" width="100%">
				<TR class="moduleTitle">
					<TD>
						<asp:label id="lblTitle" runat="server" CssClass="labeltopic"> 维护返工范围</asp:label>
						<asp:textbox id="txtReworkSheetCode" runat="server" CssClass="textbox" Width="120px" Visible="False"
							ReadOnly="True"></asp:textbox></TD>
				</TR>
				<TR>
					<TD>
						<table class="query" height="100%" width="100%" border="0">
							<tr>
								<td class="fieldNameLeft" noWrap><asp:label id="lblMOQuery" runat="server" DESIGNTIMEDRAGDROP="77"> 工单</asp:label></td>
								<td class="fieldValue"><asp:textbox id="txtMOCodeQuery" runat="server" CssClass="textbox" Width="165px"></asp:textbox></td>
								<td class="fieldName" noWrap><asp:label id="lblLotNum" runat="server" DESIGNTIMEDRAGDROP="120"> 批号</asp:label></td>
								<TD class="fieldValue">
									<cc2:SelectableTextBox id="txtLotNoQuery" runat="server" Width="100px" Target="lot" Type="lot"></cc2:SelectableTextBox></TD>
								<td class="fieldName" noWrap><asp:label id="lblOperationCodeQuery" runat="server"> 批退工序</asp:label></td>
								<td class="fieldValue"><asp:dropdownlist id="drpOPCodeQuery" runat="server" Width="165px" onload="drpOpCodeQuery_Load"></asp:dropdownlist></td>
								<TD></TD>
								<td class="fieldName"></td>
							</tr>
							<TR>
								<TD class="fieldNameLeft" noWrap><asp:label id="lblRejectTimeQuery" runat="server">判退时间 从</asp:label></TD>
								<td class="fieldValue" nowrap>
									<table border="0" cellspacing="0" cellpadding="0">
										<tr>
											<TD><uc1:emesdate id="dateFrom" runat="server" width="80"></uc1:emesdate></TD>
											<td><uc1:emestime id="timeFrom" runat="server" width="60"></uc1:emestime></td>
										</tr>
									</table>
								</td>
								<td class="fieldName"><asp:label id="lblTo" runat="server">到</asp:label></td>
								<td class="fieldValue">
									<table cellSpacing="0" cellPadding="0">
										<tr>
											<td><uc1:emesdate id="dateTo" runat="server" width="80"></uc1:emesdate></td>
											<td><uc1:emestime id="timeTo" runat="server" width="60"></uc1:emestime></td>
										</tr>
									</table>
								</td>
								<td>
									<asp:label id="lblCartonNoQuery" runat="server">箱号</asp:label></td>
								<td>
									<asp:textbox id="txtBoxNo" runat="server" CssClass="textbox" Width="165px"></asp:textbox></td>
								<TD width="100%">&nbsp;</TD>
								<TD class="fieldName"><INPUT class="submitImgButton" id="cmdQuery" type="submit" value="查 询" name="btnQuery"
										runat="server"></TD>
							</TR>
						</table>
					</TD>
				</TR>
				<TR height="100%">
					<TD class="fieldGrid">
						<igtbl:ultrawebgrid id="gridWebGrid" runat="server" Width="100%" Height="100%">
							<DISPLAYLAYOUT TableLayout="Fixed" Name="gridWebGrid" RowSelectorsDefault="No" CellPaddingDefault="4"
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
								<FRAMESTYLE Width="100%" Height="100%" BorderStyle="Groove" BorderWidth="0px" Font-Size="12px"
									Font-Names="Verdana"></FRAMESTYLE>
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
							</DISPLAYLAYOUT>
							<BANDS>
								<igtbl:UltraGridBand></igtbl:UltraGridBand>
							</BANDS>
						</igtbl:ultrawebgrid></TD>
				</TR>
				<TR class="normal">
					<TD>
						<TABLE id="Table4" height="100%" cellPadding="0" width="100%">
							<TR>
								<TD>
									<asp:checkbox id="chbSelectAll" runat="server" Width="124px" AutoPostBack="True" Text="全选"></asp:checkbox></TD>
								<TD>
									<cc1:pagersizeselector id="pagerSizeSelector" runat="server"></cc1:pagersizeselector></TD>
								<TD align="right">
									<cc1:pagertoolbar id="pagerToolBar" runat="server"></cc1:pagertoolbar></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR class="toolBar">
					<TD>
						<TABLE class="toolBar" id="Table5">
							<TR>
								<TD class="toolBar"><INPUT class="submitImgButton" id="cmdAdd" type="submit" value="新 增" name="cmdAdd" runat="server"></TD>
								<TD class="toolBar"><INPUT class="submitImgButton" id="cmdReturn" type="submit" value="返 回" name="cmdReturn"
										runat="server" onserverclick="cmdReturn_ServerClick"></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</FORM>
	</body>
</HTML>
