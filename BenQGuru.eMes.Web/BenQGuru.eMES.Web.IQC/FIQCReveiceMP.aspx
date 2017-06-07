<%@ Page language="c#" Codebehind="FIQCReveiceMP.aspx.cs" AutoEventWireup="True" Inherits="BenQGuru.eMES.Web.IQC.FIQCReveiceMP" %>
<%@ Register TagPrefix="uc1" TagName="eMESDate" Src="~/UserControl/DateTime/DateTime/eMESDate.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<title>FIQCReveiceMP</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR" />
		<meta content="C#" name="CODE_LANGUAGE" />
		<meta content="JavaScript" name="vs_defaultClientScript" />
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
		<link href="<%=StyleSheet%>" rel="stylesheet" />		
		<script language="javascript">
	        function DisableMe(id)
	        {
	            document.getElementById(id).disabled = true;
			    __doPostBack(id, '');
	            return true;
	        }
	    </script>
	</head>
	<body>
		<form id="Form1" method="post" runat="server">
			<table style="height:100%; width:100%">
				<tr class="moduleTitle">
					<td><asp:label id="lblTitle" runat="server" CssClass="labeltopic">物料接收</asp:label></td>
				</tr>
				<tr>
					<td>
						<table class="query" style="height:100%; width:100%">
							<tr>
								<td class="fieldName" nowrap="noWrap"><asp:label id="lblIQCNoQuery" runat="server">IQC送检单号</asp:label></td>
								<td class="fieldValue" nowrap="noWrap"><asp:textbox id="txtIQCNoQuery" runat="server" CssClass="textbox"  Width="130px"></asp:textbox></td>
								<td class="fieldName" nowrap="noWrap"><asp:label id="lblASNPOQuery" runat="server">ASN/PO单号</asp:label></td>
								<td class="fieldValue" nowrap="noWrap"><asp:textbox id="txtASNPOQuery" runat="server" CssClass="textbox" Width="130px"></asp:textbox></td>								
								<td class="fieldName" nowrap="noWrap"><asp:label ID="lblROHSQuery" runat="server">ROHS</asp:label></td>
								<td class="fieldValue" nowrap="noWrap"><asp:dropdownlist id="drpROHSQuery" runat="server" CssClass="textbox" Width="130px"></asp:dropdownlist></td>
								<td></td>
								<td></td>
							</tr>
							<tr>
								<td class="fieldName" nowrap="noWrap"><asp:label id="lblVendorCodeQuery" runat="server">供应商代码</asp:label></td>
								<td class="fieldValue" nowrap="noWrap"><asp:textbox id="txtVendorCodeQuery" runat="server" CssClass="textbox" Width="130px"></asp:textbox></td>
								<td class="fieldName" nowrap="noWrap"><asp:label id="lblShipToStockQuery" runat="server">免检</asp:label></td>
								<td class="fieldValue" nowrap="noWrap"><asp:dropdownlist id="drpShipToStockQuery" runat="server" Width="130px"></asp:dropdownlist></td>
								<td></td>    
								<td></td>
								<td></td>
								<td></td>
							</tr>													
							<tr>
								<td class="fieldName" nowrap="noWrap" style="height: 26px"><asp:label id="lblAppDateFromQuery" runat="server">送检日期从</asp:label></td>
								<td class="fieldValue" nowrap="noWrap" style="height: 26px">
                                <asp:TextBox type="text" id="datAppDateFromQuery"  class='datepicker' runat="server"  Width="130px"/>
								<td class="fieldName" nowrap="noWrap" style="height: 26px"><asp:label id="lblAppDateToQuery" runat="server">到</asp:label></td>
								<td class="fieldValue" nowrap="noWrap" style="height: 26px">
                                 <asp:TextBox type="text" id="datAppDateToQuery"  class='datepicker' runat="server"  Width="130px"/>
								<td></td>    
								<td></td>
								<td style="width:100%"></td>
								<td class="fieldName">
								    <input class="submitImgButton" id="cmdQuery" type="submit" value="查 询" name="btnQuery" runat="server" onserverclick="cmdQuery_ServerClick" />
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr style="height:100%">
					<td class="fieldGrid"><igtbl:ultrawebgrid id="gridWebGrid" runat="server" Width="100%" Height="100%"><DISPLAYLAYOUT TableLayout="Fixed" Name="webGrid" RowSelectorsDefault="No" CellPaddingDefault="4"
								AllowColSizingDefault="Free" BorderCollapseDefault="Separate" HeaderClickActionDefault="SortSingle" SelectTypeCellDefault="Single" SelectTypeRowDefault="Single" Version="2.00" RowHeightDefault="20px"
								AllowSortingDefault="Yes" StationaryMargins="Header" ColWidthDefault="">
								<ADDNEWBOX>
									
								</ADDNEWBOX>
								<PAGER>
									
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
						<table cellpadding="0" style="height:100%; width:100%">
							<tr>
								<td><asp:checkbox id="chbSelectAll" runat="server" Width="124px" Text="全选" AutoPostBack="True"></asp:checkbox></td>
								<td class="smallImgButton"><input class="gridExportButton" id="cmdGridExport" type="submit" value="  " name="cmdCancel"
										runat="server" onserverclick="cmdGridExport_ServerClick" /> |
								</td>
								</td>
								<td><cc1:PagerSizeSelector id="pagerSizeSelector" runat="server"></cc1:PagerSizeSelector></td>
								<td align="right"><cc1:PagerToolBar id="pagerToolBar" runat="server" PageSize="20"></cc1:PagerToolBar></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr class="normal">
					<td style="height: 96px">
						<table class="edit" cellpadding="0" style="height:100%; width:100%">
							<tr>
								<td class="fieldName" nowrap="noWrap" style="height: 26px"><asp:label id="lblIQCNoEdit" runat="server">送检单号</asp:label></td>
								<td class="fieldValue" nowrap="noWrap" style="height: 26px"><asp:textbox id="txtIQCNoEdit" runat="server" CssClass="require" Width="130px"></asp:textbox></td>
								<td class="fieldName" nowrap="noWrap" style="height: 26px"><asp:label id="lblIQCLineEdit" runat="server">送检单行</asp:label></td>
								<td class="fieldValue" nowrap="noWrap" style="height: 26px"><asp:textbox id="txtIQCLineEdit" runat="server" CssClass="require" Width="130px"></asp:textbox></td>
								<td class="fieldName" nowrap="noWrap" style="height: 26px"><asp:label id="lblAccountDateEdit" runat="server">记帐日期</asp:label></td>
								<td class="fieldValue" nowrap="noWrap" style="height: 26px">
                                <asp:TextBox id="datAccountDateEdit"  class='datepicker' runat="server"  Width="130px"/>														
								<td style="width:100%"></td>
							</tr>
							<tr>
								<td class="fieldName" nowrap="noWrap" style="height: 26px"><asp:label id="lblPurchaseOrderNoEdit" runat="server">订单号</asp:label></td>
								<td class="fieldValue" nowrap="noWrap" style="height: 26px"><asp:textbox id="txtPurchaseOrderNoEdit" runat="server" CssClass="require" Width="130px"></asp:textbox></td>
								<td class="fieldName" nowrap="noWrap" style="height: 26px"><asp:label id="lblPurchaseOrderLineEdit" runat="server">订单行</asp:label></td>
								<td class="fieldValue" nowrap="noWrap" style="height: 26px"><asp:textbox id="txtPurchaseOrderLineEdit" runat="server" CssClass="require" Width="130px"></asp:textbox></td>
								<td class="fieldName" nowrap="noWrap" style="height: 26px"><asp:label id="lblVoucherDateEdit" runat="server">凭证日期</asp:label></td>
								<td class="fieldValue" nowrap="noWrap" style="height: 26px">
                                 <asp:TextBox id="datVoucherDateEdit"  class='datepicker' runat="server"  Width="130px"/>						</td>																
								<td style="width:100%"></td>
							</tr>
							<tr>
								<td class="fieldName" nowrap="noWrap" style="height: 26px"><asp:label id="lblStorageIDEdit" runat="server">入库库别</asp:label></td>
								<td class="fieldValue" nowrap="noWrap" style="height: 26px"><asp:textbox id="txtStorageIDEdit" runat="server" CssClass="require" Width="130px"></asp:textbox></td>
								<td class="fieldName" nowrap="noWrap" style="height: 26px"><asp:label id="lblRealReceiveQtyEdit" runat="server">接收数量</asp:label></td>
								<td class="fieldValue" nowrap="noWrap" style="height: 26px"><asp:textbox id="txtRealReceiveQtyEdit" runat="server" CssClass="require" Width="130px"></asp:textbox></td>
								<td class="fieldName" nowrap="noWrap" style="height: 26px"><asp:label id="lblReceiveMemoEdit" runat="server">抬头文本</asp:label></td>
								<td class="fieldValue" nowrap="noWrap" style="height: 26px"><asp:textbox id="txtReceiveMemoEdit" runat="server" CssClass="textbox" Width="130px"></asp:textbox></td>																
								<td style="width:100%"></td>
							</tr>							
						</table>
					</td>
				</tr>
				<tr class="toolBar">
					<td style="height: 15px">
						<table class="toolBar">
							<tr>
							    <td class="toolBar"><input class="submitImgButton" id="cmdIQCReceiveBatch" type="submit" value="批量接收" name="cmdIQCReceiveBatch" runat="server" onserverclick="cmdIQCReceiveBatch_ServerClick" onclick="if (!confirm('是否确认批量接收？')) return false; return DisableMe(this.id);" /></td>
								<td class="toolBar"><input class="submitImgButton" id="cmdIQCReceive" type="submit" value="接 收" name="cmdIQCReceive" runat="server" onserverclick="cmdIQCReceive_ServerClick" onclick="return DisableMe(this.id);" /></td>
								<td class="toolBar"><input class="submitImgButton" id="cmdCancel" type="submit" value="取 消" name="cmdCancel" runat="server" onserverclick="cmdCancel_ServerClick" /></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</html>
