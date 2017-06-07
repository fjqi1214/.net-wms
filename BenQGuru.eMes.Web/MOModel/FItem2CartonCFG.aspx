<%@ Page language="c#" Codebehind="FItem2CartonCFG.aspx.cs" AutoEventWireup="True" Inherits="BenQGuru.eMES.Web.MOModel.FItem2CartonCFG" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="ss1" NameSpace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>FItem2CartonCFG</title>
		<meta name="vs_snapToGrid" content="False">
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
					<td><asp:label id="lblTitle" runat="server" CssClass="labeltopic"> 产品包装检查维护</asp:label></td>
				</tr>
				<tr>
					<td>
						<table class="query" height="100%" width="100%">
							<tr>
								<td class="fieldName" noWrap><asp:label id="lblItemCodeQuery" runat="server"> 产品代码</asp:label></td>
								<td class="fieldValue" noWrap>
									<ss1:selectsingletabletextbox id="txtItemEdit" runat="server" Width="130px" Type="singleitem"></ss1:selectsingletabletextbox></td>
								<TD class="fieldName" noWrap><FONT face="宋体"></FONT></TD>
								<td class="fieldValue" noWrap><FONT face="宋体"></FONT></td>
								<TD class="fieldName" noWrap><FONT face="宋体"></FONT></TD>
								<td class="fieldValue" noWrap><INPUT class="submitImgButton" id="cmdQuery" type="submit" value="查 询" name="btnQuery"
										runat="server"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr height="100%">
					<td class="fieldGrid"><igtbl:ultrawebgrid id="gridWebGrid" runat="server" Width="100%" Height="100%"><DISPLAYLAYOUT TableLayout="Fixed" Name="webGrid" RowSelectorsDefault="No" CellPaddingDefault="4"
								AllowColSizingDefault="Free" BorderCollapseDefault="Separate" HeaderClickActionDefault="SortSingle" SelectTypeCellDefault="Single" SelectTypeRowDefault="Single" Version="2.00" RowHeightDefault="20px"
								AllowSortingDefault="Yes" StationaryMargins="Header" ColWidthDefault=""><ADDNEWBOX>
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
								<FRAMESTYLE Height="100%" Width="100%" BorderStyle="Groove" BorderWidth="0px" BorderColor="#ABABAB"
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
						</igtbl:ultrawebgrid></td>
				</tr>
				<tr class="normal">
					<td>
						<table height="100%" cellPadding="0" width="100%">
							<tr>
								<td><asp:checkbox id="chbSelectAll" runat="server" Width="124px" AutoPostBack="True" Text="全选"></asp:checkbox></td>
								<TD class="smallImgButton"><INPUT class="gridExportButton" id="cmdGridExport" type="submit" value="  " name="cmdCancel"
										runat="server"> |
								</TD>
								<TD class="smallImgButton"><INPUT class="deleteButton" id="cmdDelete" type="submit" value="  " name="cmdDelete" runat="server">
									|
								</TD>
								<TD><cc1:pagersizeselector id="pagerSizeSelector" runat="server" PageSize="10"></cc1:pagersizeselector></TD>
								<td align="right"><cc1:pagertoolbar id="pagerToolBar" runat="server" PageSize="20"></cc1:pagertoolbar></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr class="normal">
					<td>
						<table class="edit" height="100%" cellPadding="0" width="100%">
							<tr>
								<td class="fieldNameLeft" noWrap><asp:label id="lblItemName" runat="server">产品名称</asp:label></td>
								<TD class="fieldValue">
									<asp:textbox id="txtItemName" runat="server" CssClass="require" Width="130px"></asp:textbox></TD>
								<td class="fieldName" noWrap>
									<asp:label id="lblPCSType" runat="server">板片类型</asp:label></td>
								<td class="fieldValue">
									<asp:dropdownlist id="drpPCSType" runat="server" CssClass="require" Width="150px"></asp:dropdownlist></td>
								<td class="fieldNameLeft" noWrap style="WIDTH: 146px"><asp:label id="lblMPlate" runat="server">M板</asp:label></td>
								<TD class="fieldValue"><asp:textbox id="txtMPlate" runat="server" CssClass="require" Width="130px"></asp:textbox></TD>
								<td width="100%" nowrap>
								</td>
							</tr>
							<TR>
								<TD class="fieldNameLeft">
									<asp:label id="lblSPlate" runat="server">S板</asp:label></TD>
								<TD class="fieldValue">
									<asp:textbox id="txtSPlate" runat="server" Width="130px" CssClass="textbox"></asp:textbox></TD>
								<TD class="fieldName" noWrap>
									<asp:label id="lblCartonItemCode" runat="server">大箱料号</asp:label></TD>
								<TD class="fieldValue">
									<asp:textbox id="txtCartonItemCode" runat="server" CssClass="require" Width="130px"></asp:textbox></TD>
								<TD class="fieldName" style="WIDTH: 146px; HEIGHT: 26px" noWrap>
									<asp:label id="lblCartonCodeLen" runat="server">大箱标签位数</asp:label></TD>
								<TD class="fieldValue">
									<asp:textbox id="txtCartonCodeLen" runat="server" CssClass="require" Width="130px"></asp:textbox></TD>
								<TD noWrap width="100%"></TD>
							</TR>
							<TR>
								<TD class="fieldNameLeft" noWrap>
									<asp:label id="lblStartPosition" runat="server">起始位数</asp:label></TD>
								<TD class="fieldValue">
									<asp:textbox id="txtStartPosition" runat="server" CssClass="require" Width="130px"></asp:textbox></TD>
								<TD class="fieldName" noWrap>
									<asp:label id="lblEndPosition" runat="server">结束位数</asp:label></TD>
								<TD class="fieldValue">
									<asp:textbox id="txtEndPosition" runat="server" CssClass="require" Width="130px"></asp:textbox></TD>
								<TD class="fieldName" noWrap style="WIDTH: 146px"><FONT face=宋体></FONT></TD>
								<TD class="fieldValue"></TD>
								<TD noWrap width="100%"></TD>
							</TR>
						</table>
					</td>
				</tr>
				<tr class="toolBar">
					<td>
						<table class="toolBar">
							<tr>
								<td class="toolBar"><INPUT class="submitImgButton" id="cmdAdd" type="submit" value="新 增" name="cmdAdd" runat="server"></td>
								<td class="toolBar"><INPUT class="submitImgButton" id="cmdSave" type="submit" value="保 存" name="cmdSave" runat="server"></td>
								<td class="toolBar"><INPUT class="submitImgButton" id="cmdCancel" type="submit" value="取 消" name="cmdCancel"
										runat="server" onserverclick="cmdCancel_ServerClick"></td>
								<td></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
