<%@ Page language="c#" Codebehind="FTSRecordQP2.aspx.cs" AutoEventWireup="True" Inherits="BenQGuru.eMES.Web.WebQuery.FTSRecordQP2" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="uc1" TagName="eMesDate" Src="~/UserControl/DateTime/DateTime/eMesDate.ascx" %>
<%@ Register TagPrefix="uc1" TagName="eMesTime" Src="~/UserControl/DateTime/DateTime/eMesTime.ascx" %>
<%@ Register TagPrefix="cc2" NameSpace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>FTSRecordQP</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href="<%=StyleSheet%>" rel=stylesheet>
		<script language="javascript">
			function onRadioCheckChange(radio)
			{
			
				if(radio.id == "RadioErrorDate")
				{
					document.all.RadioReceivedDate.checked = false;
					document.all.RadioTSDate.checked = false;
				}
				else if(radio.id == "RadioReceivedDate")
				{
					document.all.RadioErrorDate.checked = false;
					document.all.RadioTSDate.checked = false;
				}
				else if(radio.id == "RadioTSDate")
				{
					document.all.RadioErrorDate.checked = false;
					document.all.RadioReceivedDate.checked = false;
				}
			}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table height="100%" width="100%">
				<tr class="moduleTitle">
					<td><table cellpadding="0" cellspacing="0">
							<tr>
								<td><img id="imgTitle" src="../skin/image/ico_arrow.gif" width="15" height="15">
								</td>
								<td><asp:label id="lblRepairRecode" runat="server" CssClass="Title2">维修记录</asp:label></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td>
						<table class="query" height="100%" width="100%">
							<TR>
								<td class="fieldName" noWrap><asp:label id="lblModelCodeQuery" runat="server">产品别代码</asp:label></td>
								<td class="fieldValue" width="100"><cc2:selectabletextbox id="txtConditionModel" runat="server" Type="model" Width="100"></cc2:selectabletextbox></td>
								<td class="fieldName" noWrap><asp:label id="lblItemCodeQuery" runat="server">产品代码</asp:label></td>
								<td class="fieldValue" width="100"><cc2:selectabletextbox id="txtConditionItem" runat="server" Type="item" Width="100"></cc2:selectabletextbox></td>
								<td class="fieldName" noWrap><asp:label id="lblMOIDQuery" runat="server">工单代码</asp:label></td>
								<td class="fieldValue"><cc2:selectabletextbox id="txtConditionMo" runat="server" Type="mo" Width="100"></cc2:selectabletextbox></td>
							</TR>
							<tr>
								<td class="fieldName" noWrap><asp:label id="lblStartSnQuery" runat="server">起始序列号</asp:label></td>
								<td class="fieldValue"><asp:textbox id="txtStartSnQuery" runat="server" CssClass="require" Width="165px"></asp:textbox></td>
								<td class="fieldName" noWrap><asp:label id="lblEndSnQuery" runat="server">结束序列号</asp:label></td>
								<td class="fieldValue"><asp:textbox id="txtEndSnQuery" runat="server" CssClass="require" Width="165px"></asp:textbox></td>
								<td class="fieldName" noWrap><asp:label id="lblReworkMo" runat="server">返工需求单代码</asp:label></td>
								<td class="fieldValue"><cc2:selectabletextbox id="txtReworkMo" runat="server" Type="reworkmo" Width="100"></cc2:selectabletextbox></td>
							</tr>
							<tr>
								<td class="fieldName" noWrap><asp:label id="lblOriginQuery" runat="server">来源站</asp:label></td>
								<td class="fieldValue"><cc2:selectabletextbox id="txtFromResource" runat="server" Type="resource" Width="100"></cc2:selectabletextbox></td>
								<td class="fieldName" noWrap><asp:label id="lblReceiveQuery" runat="server">接收站</asp:label></td>
								<td class="fieldValue"><cc2:selectabletextbox id="txtConfirmRes" runat="server" Type="resource" Width="100"></cc2:selectabletextbox></td>
								<td class="fieldName" noWrap><asp:label id="lblOperationQuery" runat="server">维修站</asp:label></td>
								<td class="fieldValue"><cc2:selectabletextbox id="txtConditionResource" runat="server" Type="resource" Width="100"></cc2:selectabletextbox></td>
							</tr>
							<tr>
								<td class="fieldName" noWrap><asp:RadioButton id="rdbErrorDate" runat="server"></asp:RadioButton><asp:label id="lblNGStartDateQuery" runat="server">不良起始日期</asp:label></td>
								<TD class="fieldValue" noWrap>
									<TABLE cellSpacing="0" cellPadding="0" border="0">
										<TR>
											<TD><uc1:emesdate id="dateStartDateQuery" runat="server" width="80" CssClass="require"></uc1:emesdate></TD>
											<TD><uc1:emestime id="dateStartTimeQuery" runat="server" width="60" CssClass="require"></uc1:emestime></TD>
										</TR>
									</TABLE>
								</TD>
								<td class="fieldName" noWrap><asp:label id="lblNGEndDateQuery" runat="server">不良结束日期</asp:label></td>
								<TD class="fieldValue" noWrap>
									<TABLE cellSpacing="0" cellPadding="0" border="0">
										<TR>
											<TD><uc1:emesdate id="dateEndDateQuery" runat="server" width="80" CssClass="require"></uc1:emesdate></TD>
											<TD><uc1:emestime id="dateEndTimeQuery" runat="server" width="60" CssClass="require"></uc1:emestime></TD>
										</TR>
									</TABLE>
								</TD>
							</tr>
							<tr>
								<td class="fieldName" noWrap><asp:RadioButton id="rdbReceivedDate" runat="server"></asp:RadioButton><asp:label id="lblReceiveBeginDate" runat="server">接收起始日期</asp:label></td>
								<TD class="fieldValue" noWrap>
									<TABLE cellSpacing="0" cellPadding="0" border="0">
										<TR>
											<TD><uc1:emesdate id="txtReceiveBeginDate" runat="server" width="80"></uc1:emesdate></TD>
											<TD><uc1:emestime id="txtReceiveBeginTime" runat="server" width="60"></uc1:emestime></TD>
										</TR>
									</TABLE>
								</TD>
								<td class="fieldName" noWrap><asp:label id="lblReceiveEndDate" runat="server">接收结束日期</asp:label></td>
								<TD class="fieldValue" noWrap>
									<TABLE cellSpacing="0" cellPadding="0" border="0">
										<TR>
											<TD><uc1:emesdate id="txtReceiveEndDate" runat="server" width="80"></uc1:emesdate></TD>
											<TD><uc1:emestime id="txtReceiveEndTime" runat="server" width="60"></uc1:emestime></TD>
										</TR>
									</TABLE>
								</TD>
							</tr>
							<tr>
								<td class="fieldName" noWrap><asp:RadioButton id="rdbTSDate" runat="server"></asp:RadioButton><asp:label id="lblRepairStartDateQuery" runat="server">维修起始日期</asp:label></td>
								<TD class="fieldValue" noWrap>
									<TABLE cellSpacing="0" cellPadding="0" border="0">
										<TR>
											<TD><uc1:emesdate id="txtTSBeginDate" runat="server" width="80"></uc1:emesdate></TD>
											<TD><uc1:emestime id="txtTSBeginTime" runat="server" width="60"></uc1:emestime></TD>
										</TR>
									</TABLE>
								</TD>
								<td class="fieldName" noWrap><asp:label id="lblRepairEndDateQuery" runat="server">维修结束日期</asp:label></td>
								<TD class="fieldValue" noWrap>
									<TABLE cellSpacing="0" cellPadding="0" border="0">
										<TR>
											<TD><uc1:emesdate id="txtTSEndDate" runat="server" width="80"></uc1:emesdate></TD>
											<TD><uc1:emestime id="txtTSEndTime" runat="server" width="60"></uc1:emestime></TD>
										</TR>
									</TABLE>
								</TD>
							</tr>
							<tr>
								<td class="fieldName" noWrap><asp:label id="lblTSStateQuery" runat="server">维修状态</asp:label></td>
								<td colSpan="4"><asp:checkboxlist id="chkTSStateList" runat="server" RepeatDirection="Horizontal"></asp:checkboxlist></td>
								<td class="fieldName"><INPUT class="submitImgButton" id="cmdQuery" type="submit" value="查 询" name="btnQuery"
										runat="server"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr height="100%">
					<td class="fieldGrid">
						<igtbl:ultrawebgrid id="gridWebGrid" runat="server" Width="100%" Height="100%">
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
								<TD width="140"><cc1:pagersizeselector id="pagerSizeSelector" runat="server"></cc1:pagersizeselector></TD>
								<TD class="smallImgButton"><INPUT class="gridExportButton" id="cmdGridExport" type="submit" value="  " name="cmdGridExport"
										runat="server"> |</TD>
								<td><asp:checkbox id="chbRepairDetail" runat="server" Text="导出维修明细"></asp:checkbox></td>
								<td align="right"><cc1:pagertoolbar id="pagerToolBar" runat="server"></cc1:pagertoolbar></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
