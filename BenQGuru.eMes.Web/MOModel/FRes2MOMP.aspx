<%@ Register TagPrefix="uc1" TagName="eMESDate" Src="~/UserControl/DateTime/DateTime/eMESDate.ascx" %>
<%@ Register TagPrefix="uc1" TagName="eMesTime" Src="../UserControl/DateTime/DateTime/eMesTime.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Page language="c#" Codebehind="FRes2MOMP.aspx.cs" AutoEventWireup="True" Inherits="BenQGuru.eMES.Web.MOModel.FRes2MOMP" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>FRes2MOMP</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href="<%=StyleSheet%>" rel=stylesheet>
		<script language="javascript">
			function HideShowMOType()
			{
				var sel = document.getElementById("drpMOGetTypeEdit");
				if (sel.selectedIndex == 0)
				{
					document.getElementById("trGetFromRCardMO").style.visibility = "hidden";
					document.getElementById("trGetFromRCardMO").style.display = "none";
					
					document.getElementById("trStaticMO").style.visibility = "visible";
					document.getElementById("trStaticMO").style.display = "";
				}
				else
				{
					document.getElementById("trGetFromRCardMO").style.visibility = "visible";
					document.getElementById("trGetFromRCardMO").style.display = "";
					
					document.getElementById("trStaticMO").style.visibility = "hidden";
					document.getElementById("trStaticMO").style.display = "none";
				}
			}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table height="100%" width="100%">
				<tr class="moduleTitle">
					<td><asp:label id="lblFRes2MOMPTitle" runat="server" CssClass="labeltopic"> 资源归属工单设置</asp:label></td>
				</tr>
				<tr>
					<td>
						<table class="query" height="100%" width="100%">
							<tr>
								<td class="fieldNameLeft" noWrap><asp:label id="lblResourceCodeQuery" runat="server"> 资源代码</asp:label></td>
								<td class="fieldValue"><asp:textbox id="txtResourceCodeQuery" runat="server" CssClass="textbox" Width="120px"></asp:textbox></td>
								<TD class="fieldNameLeft" noWrap>
									<asp:label id="lblStartDateQuery" runat="server">起始日期</asp:label></TD>
								<TD class="fieldValue">
									<uc1:emesdate id="txtDateFrom" runat="server" CssClass="textbox" width="130"></uc1:emesdate></TD>
								<TD class="fieldName" noWrap>
									<asp:label id="lblEndDateQuery" runat="server">结束日期</asp:label></TD>
								<TD class="fieldValue" noWrap>
									<uc1:emesdate id="txtDateTo" runat="server" CssClass="textbox" width="130"></uc1:emesdate></TD>
								<td width="100%"></td>
								<td class="fieldName"><INPUT class="submitImgButton" id="cmdQuery" type="submit" value="查 询" name="btnQuery"
										runat="server" onserverclick="cmdQuery_ServerClick"></td>
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
								<td><asp:checkbox id="chbSelectAll" runat="server" Width="124px" Text="全选" AutoPostBack="True"></asp:checkbox></td>
								<TD class="smallImgButton"><INPUT class="gridExportButton" id="cmdGridExport" type="submit" value="  " name="cmdCancel"
										runat="server" onserverclick="cmdGridExport_ServerClick"> |
								</TD>
								<TD class="smallImgButton"><INPUT class="deleteButton" id="cmdDelete" type="submit" value="  " name="cmdDelete" runat="server" onserverclick="cmdDelete_ServerClick">
									|
								</TD>
								<TD><cc1:pagersizeselector id="pagerSizeSelector" runat="server"></cc1:pagersizeselector></TD>
								<td align="right"><cc1:pagertoolbar id="pagerToolBar" runat="server" PageSize="20"></cc1:pagertoolbar></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr class="normal">
					<td>
						<table class="edit" height="100%" cellPadding="0" width="100%">
							<tr>
								<TD class="fieldNameLeft" style="HEIGHT: 26px" noWrap>
									&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:label id="lblResourceCodeEdit" runat="server">资源代码</asp:label></TD>
								<TD style="HEIGHT: 26px"><asp:textbox id="txtResourceCodeEdit" runat="server" CssClass="require" Width="130px"></asp:textbox></TD>
								<td class="fieldName" noWrap><asp:label id="lblStartDateEdit" runat="server">起始日期</asp:label></td>
								<TD noWrap>
									<TABLE cellSpacing="0" cellPadding="0" border="0">
										<TR>
											<TD><uc1:emesdate id="dateStartDateEdit" runat="server" width="80" CssClass="textbox"></uc1:emesdate></TD>
											<TD><uc1:emestime id="dateStartTimeEdit" runat="server" width="60" CssClass="textbox"></uc1:emestime></TD>
										</TR>
									</TABLE>
								</TD>
								<td class="fieldName" noWrap><asp:label id="lblEndDateEdit" runat="server">结束日期</asp:label></td>
								<TD noWrap>
									<TABLE cellSpacing="0" cellPadding="0" border="0">
										<TR>
											<TD><uc1:emesdate id="dateEndDateEdit" runat="server" width="80" CssClass="textbox"></uc1:emesdate></TD>
											<TD><uc1:emestime id="dateEndTimeEdit" runat="server" width="60" CssClass="textbox"></uc1:emestime></TD>
										</TR>
									</TABLE>
								</TD>
								<TD class="fieldNameLeft" style="HEIGHT: 26px" noWrap>
									<asp:label id="lblMOGetTypeEdit" runat="server">工单获取方式</asp:label></TD>
								<TD class="fieldValue" style="HEIGHT: 26px">
									<asp:DropDownList Runat="server" ID="drpMOGetTypeEdit" CssClass="require">
										<asp:ListItem Value="resource2mo_gettype_static">固定工单</asp:ListItem>
										<asp:ListItem Value="resource2mo_gettype_getfromrcard">从序列号解析</asp:ListItem>
									</asp:DropDownList>
								</TD>
							</tr>
							<tr id="trStaticMO">
								<TD class="fieldNameLeft" style="HEIGHT: 26px" noWrap>
									<asp:label id="lblMOCodeEdit" runat="server">工单代码</asp:label></TD>
								<TD style="HEIGHT: 26px"><asp:textbox id="txtMOCodeEdit" runat="server" CssClass="require" Width="130px"></asp:textbox></TD>
								<td><asp:TextBox Runat="server" ID="txtSequenceEdit" Visible="False"></asp:TextBox></td>
							</tr>
							<tr id="trGetFromRCardMO">
								<TD class="fieldNameLeft" style="HEIGHT: 26px" noWrap>
									<asp:label id="lblMORCardStartIndexEdit" runat="server">序列号起始位置</asp:label></TD>
								<TD style="HEIGHT: 26px"><asp:textbox id="txtMORCardStartIndexEdit" runat="server" CssClass="require" Width="130px"></asp:textbox></TD>
								<TD class="fieldNameLeft" style="HEIGHT: 26px" noWrap>
									<asp:label id="lblMOLendthRCardEdit" runat="server">工单截取长度</asp:label></TD>
								<TD style="HEIGHT: 26px"><asp:textbox id="txtMOLendthEdit" runat="server" CssClass="require" Width="130px"></asp:textbox></TD>
								<TD class="fieldNameLeft" style="HEIGHT: 26px" noWrap>
									<asp:label id="lblMOCodeFormatEdit" runat="server">工单格式</asp:label></TD>
								<TD style="HEIGHT: 26px" colspan="3">
									<asp:textbox id="txtMOCodePrefix" runat="server" CssClass="textbox" Width="130px"></asp:textbox><asp:Label Runat="server" ID="lblMOCodeFromRCardEdit" Font-Underline="true">序列号解析</asp:Label><asp:textbox id="txtMOCodePostfix" runat="server" CssClass="textbox" Width="130px"></asp:textbox>
								</TD>
							</tr>
							<tr>
								<TD class="fieldNameLeft" style="HEIGHT: 26px" noWrap></TD>
								<TD style="HEIGHT: 26px"><asp:CheckBox Runat="server" ID="chkCheckRCardFormatEdit" text="序列号防呆"></asp:CheckBox></TD>
								<TD class="fieldNameLeft" style="HEIGHT: 26px" noWrap>
									<asp:label id="lblRCardPrefixEdit" runat="server">序列号前缀</asp:label></TD>
								<TD style="HEIGHT: 26px"><asp:textbox id="txtRCardPrefixEdit" runat="server" CssClass="textbox" Width="130px"></asp:textbox></TD>
								<TD class="fieldNameLeft" style="HEIGHT: 26px" noWrap>
									<asp:label id="lblRCardLengthEdit" runat="server">序列号长度</asp:label></TD>
								<TD style="HEIGHT: 26px"><asp:textbox id="txtRCardLengthEdit" runat="server" CssClass="textbox" Width="130px"></asp:textbox></TD>
							</tr>
						</table>
					</td>
				</tr>
				<tr class="toolBar">
					<td>
						<table class="toolBar">
							<tr>
								<td class="toolBar"><INPUT class="submitImgButton" id="cmdAdd" type="submit" value="新 增" name="cmdAdd" runat="server" onserverclick="cmdAdd_ServerClick"></td>
								<td class="toolBar"><INPUT class="submitImgButton" id="cmdSave" type="submit" value="保 存" name="cmdSave" runat="server" onserverclick="cmdSave_ServerClick"></td>
								<td><INPUT class="submitImgButton" id="cmdCancel" type="submit" value="取 消" name="cmdCancel"
										runat="server" onserverclick="cmdCancel_ServerClick"></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
		<script language="javascript">
			HideShowMOType();
		</script>
	</body>
</HTML>
