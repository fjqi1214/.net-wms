<%@ Register TagPrefix="uc1" TagName="eMesTime" Src="../UserControl/DateTime/DateTime/eMesTime.ascx" %>
<%@ Register TagPrefix="uc1" TagName="eMesDate" Src="../UserControl/DateTime/DateTime/eMesDate.ascx" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Page language="c#" Codebehind="FRejectConfirmMP.aspx.cs" AutoEventWireup="True" Inherits="BenQGuru.eMES.Web.Rework.FRejectConfirmMP" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>FRejectConfirmMP</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<meta http-equiv="pragma" content="no-cache">
		<link href="<%=StyleSheet%>" rel=stylesheet>
	</HEAD>
	<body>
		<FORM id="Form1" method="post" runat="server">
			<TABLE id="Table1" height="100%" width="100%">
				<TR class="moduleTitle">
					<TD>
						<asp:label id="lblTitle" runat="server" CssClass="labeltopic">判退确认</asp:label></TD>
				</TR>
				<TR>
					<TD>
						<TABLE class="query" id="Table2" height="100%" width="100%">
							<TR>
								<TD class="fieldNameLeft" noWrap>
									<asp:label id="lblModelQuery" runat="server">产品别</asp:label></TD>
								<TD class="fieldValue">
									<asp:dropdownlist id="drpModelQuery" runat="server" Width="165px" DESIGNTIMEDRAGDROP="202" onload="drpModelQuery_Load"></asp:dropdownlist></TD>
								<TD noWrap class="fieldName">
										<asp:label id="lblItemQuery" runat="server">产品</asp:label></TD>
								<TD class="fieldValue">
									<asp:textbox id="txtItemCodeQuery" runat="server" CssClass="textbox" Width="165px"></asp:textbox></TD>
								<TD class="fieldName" noWrap>
									<asp:label id="lblMOQuery" runat="server">工单</asp:label></TD>
								<TD class="fieldName">
									<asp:textbox id="txtMOCodeQuery" runat="server" CssClass="textbox" Width="165px"></asp:textbox></TD>
								<TD class="fieldName" width="100%"></TD>
								<TD class="fieldName"></TD>
							</TR>
							<TR>
								<TD class="fieldNameLeft" noWrap>
									<asp:label id="lblLotNum" runat="server">批号</asp:label></TD>
								<TD class="fieldValue">
										<asp:textbox id="txtLotNoQuery" runat="server" CssClass="textbox" Width="165px" DESIGNTIMEDRAGDROP="181"></asp:textbox></TD>
								<TD class="fieldName" noWrap>
										<asp:label id="lblOperation" runat="server">工序</asp:label></TD>
								<TD class="fieldValue">
											<asp:dropdownlist id="drpOpCodeQuery" runat="server" Width="165px" DESIGNTIMEDRAGDROP="811" onload="drpOpCodeQuery_Load"></asp:dropdownlist></TD>
								<TD noWrap class="fieldName" style="DISPLAY:none">
										<asp:label id="tblBoxNoQuery" runat="server">箱号</asp:label></TD>
								<TD class="fieldValue" style="DISPLAY:none">
										<asp:textbox id="txtBoxNoQuery" runat="server" CssClass="textbox" Width="165px"></asp:textbox></TD>
								<TD class="fieldName"></TD>
								<TD class="fieldName"></TD>
							</TR>
							<TR>
								<TD class="fieldNameLeft" noWrap>
									<asp:label id="lblRejectTimeQuery" runat="server">判退时间 从</asp:label></TD>
								<TD class="fieldValue" noWrap>
									<TABLE id="Table6" cellSpacing="0" cellPadding="0" border="0">
										<TR>
											<TD>
												<uc1:emesdate id="dateFrom" runat="server" width="80"></uc1:emesdate></TD>
											<TD>
												<uc1:emestime id="timeFrom" runat="server" width="60"></uc1:emestime></TD>
										</TR>
									</TABLE>
								</TD>
								<TD noWrap class="fieldName">
									<asp:label id="lblTo" runat="server">到</asp:label></TD>
								<TD class="fieldValue" noWrap>
									<TABLE id="Table7" cellSpacing="0" cellPadding="0" border="0">
										<TR>
											<TD>
												<uc1:emesdate id="dateTo" runat="server" width="80"></uc1:emesdate></TD>
											<TD>
												<uc1:emestime id="timeTo" runat="server" width="60"></uc1:emestime></TD>
										</TR>
									</TABLE>
								</TD>
								<TD class="fieldName" noWrap>
									<asp:label id="lblRejectStatusQuery" runat="server">判退状态</asp:label></TD>
								<TD class="fieldName">
										<asp:dropdownlist id="drpRejectStatusQuery" runat="server" Width="165px" onload="drpRejectStatusQuery_Load"></asp:dropdownlist></TD>
								<TD class="fieldName"></TD>
								<TD class="fieldName"><INPUT class="submitImgButton" id="cmdQuery" type="submit" value="查 询" name="btnQuery"
										runat="server"></TD>
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
									<STYLE BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

									</STYLE>
								</ADDNEWBOX>
								<PAGER>
									<STYLE BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

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
							</DISPLAYLAYOUT>
							<BANDS>
								<IGTBL:ULTRAGRIDBAND></IGTBL:ULTRAGRIDBAND>
							</BANDS>
						</igtbl:ultrawebgrid></TD>
				</TR>
				<TR class="normal">
					<TD>
						<TABLE id="Table3" height="100%" cellPadding="0" width="100%">
							<TR>
								<TD>
									<asp:checkbox id="chbSelectAll" runat="server" Width="124px" Text="全选" AutoPostBack="True"></asp:checkbox></TD>
								<TD class="smallImgButton"><INPUT class="gridExportButton" id="cmdGridExport" type="submit" value="  " name="cmdGridExport"
										runat="server"> |
								</TD>
								<TD class="smallImgButton" style="DISPLAY:none"><INPUT class="deleteButton" id="cmdDelete" type="submit" value="  " name="cmdDelete" runat="server">
									|
								</TD>
								<TD>
									<cc1:pagersizeselector id="pagerSizeSelector" runat="server"></cc1:pagersizeselector></TD>
								<TD align="right">
									<cc1:PagerToolbar id="pagerToolBar" runat="server"></cc1:PagerToolbar></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR class="toolBar">
					<TD>
						<TABLE class="toolBar" id="Table5">
							<TR>
								<TD class="toolBar"><INPUT class="submitImgButton" id="cmdComfirmReject" type="submit" value="确认判退" name="cmdComfirm"
										runat="server" onserverclick="cmdComfirm_ServerClick"></TD>
								<TD class="toolBar"><INPUT class="submitImgButton" id="cmdCancelConfirm" type="submit" value="取消确认" name="cmdCancelConfirm"
										runat="server" onserverclick="cmdCancelConfirm_ServerClick"></TD>
								<TD><INPUT class="submitImgButton" id="cmdCancelReject" type="submit" value="取消判退" name="cmdCancelReject"
										runat="server" onserverclick="cmdCancelReject_ServerClick"></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</FORM>
	</body>
</HTML>
