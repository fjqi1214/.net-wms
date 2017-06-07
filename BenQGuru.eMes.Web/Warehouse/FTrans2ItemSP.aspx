<%@ Register TagPrefix="cc2" NameSpace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<%@ Register TagPrefix="uc1" TagName="eMESTime" Src="~/UserControl/DateTime/DateTime/eMESTime.ascx" %>
<%@ Page language="c#" Codebehind="FTrans2ItemSP.aspx.cs" AutoEventWireup="True" Inherits="BenQGuru.eMES.Web.WarehouseWeb.FTrans2ItemSP" %>
<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>单据物料明细</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href="<%=StyleSheet%>" rel=stylesheet>
		<script language="javascript">
			function SelectItem()
			{
				var result = window.showModalDialog("FItemSelectSP.aspx", "" ,"dialogWidth:800px;dialogHeight:600px;scroll:none;help:no;status:no") ;
				if(result!=null)
				{
					document.getElementById("txtItemCodeEdit").value = result.code;
					document.getElementById("txtItemNameEdit").value = result.name;
					document.getElementById("txtSelectedItemCode").value = result.code;
					document.getElementById("txtSelectedItemName").value = result.name;
				}
			}
			function SelectMOItem()
			{
				var result = window.showModalDialog("FMOSelectSP.aspx", "" ,"dialogWidth:800px;dialogHeight:600px;scroll:none;help:no;status:no") ;
				if(result!=null)
				{
					document.getElementById("txtMOCodeEdit").value = result.code;
				}
			}
			function AddItemLot()
			{
				var mo = "1";
				if (document.getElementById("txtMOCodeEdit").readOnly == true)
				{
					mo = "0";
				}
				var ticketNo = document.getElementById("txtTicketNo").value;
				var mocode = document.getElementById("txtMOCodeEdit").value;
				var result = window.open("FTrans2ItemLotSP.aspx?ticketno=" + ticketNo + "&mo=" + mo +"&mocode="+mocode, "", "dialogWidth:800px;dialogHeight:600px;scroll:none;help:no;status:no") ;
				/*
				if(result!=null)
				{
					document.location.replace(document.location.href);
				}
				*/
			}
			function OpenPrintWindow()
			{
				var iw = screen.availWidth;
				var ih = screen.availHeight;
				ih = ih - 70;
				var features = "resize=no,width=" + iw + ",height=" + ih + ",top=0,left=0,menubar=yes";
				var ticketno = document.getElementById("txtTicketNo").value;
				window.open("FTransPrintSP.aspx?ticketno=" + ticketno, "_blank", features);
			}
			
			function UpdateGrid()
			{
				//debugger;
				var data = trim(document.all.txtFLTS.value) ;
				//alert(data);
				if( data=="" || isNaN (parseFloat(data)) || (parseFloat(data))<0 ) 
				{ 
					document.all.txtFLTS.value = "0";
					alert("请输入大于0的数值"); 
					return ; 
				}
				var grid = igtbl_getGridById('gridWebGrid');
				
				for(var i=0; i<grid.Rows.length; i++ )
				{
				/*
					if(grid.Rows.rows[i] !=null)
					{
						if(grid.Rows.rows[i].getCellFromKey("SingleQTY").getValue() != null 
							&& grid.Rows.rows[i].getCellFromKey("SingleQTY").getValue().toString() != "")
						{
							var itemQty = parseFloat(grid.Rows.rows[i].getCellFromKey("SingleQTY").getValue())*parseFloat( trim(document.all.txtFLTS.value) );
							grid.Rows.rows[i].getCellFromKey("ItemQty").setValue ( itemQty );
						}
					}
				*/
					if(grid.Rows.rows[i] !=null)
					{
						var id = 'gridWebGridrc_'+i+'_5';
						var singleQTY = document.getElementById(id).innerText ;
						if( singleQTY !=null && singleQTY != "" )
						{
							var id2 = 'gridWebGridrc_'+i+'_6';
							var itemQty = parseFloat(singleQTY) * parseFloat(data) ;
							document.getElementById(id2).innerText = itemQty ;
						}
					}
				}
					
				
			}
			
			function trim(str)
			{
				try {
					while(str.charCodeAt(0) == 32) {
						str = str.substr(1, str.length-1);
					}
					while(str.charCodeAt(str.length-1) == 32) {
						str = str.substr(0, str.length-1);
					}
					return(str);
				} catch(e) { throw e; }
			}
			
			function divSelectMODisplay()
			{
				//document.all.divSelectMO.style.display = "block";
			}
			
			function CmdSaveEnable()
			{
				document.all.cmdSave.disabled = false;
			}
			
			function OnKeyPress()
			{
				if(event.keyCode==13)
				{
					event.keyCode=0;
					return false;
				}
			}
		
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout" onkeypress="OnKeyPress()">
		<form id="Form1" method="post" runat="server">
			<table height="100%" width="100%">
				<tr class="moduleTitle">
					<td nowrap><asp:label id="lblTitle" runat="server" CssClass="labeltopic"> 单据物料明细</asp:label>
						<input type="hidden" id="txtTicketNo" runat="server">
					</td>
					<td class="fieldName" style="WIDTH: 159px">
						<asp:Label Runat="server" ID="lblFLTS" Width="130px">发料套数</asp:Label></td>
					<td class="fieldValue" nowrap><asp:textbox id="txtFLTS" runat="server" Width="130px" CssClass="textbox" AutoPostBack="True" ontextchanged="txtFLTS_TextChanged">0</asp:textbox></td>
					<td nowrap></td>
					<td width="100%"></td>
				</tr>
				<tr height="100%">
					<td class="fieldGrid" colspan="5"><igtbl:ultrawebgrid id="gridWebGrid" runat="server" Width="100%" Height="100%">
							<DisplayLayout ColWidthDefault="" StationaryMargins="Header" AllowSortingDefault="Yes" RowHeightDefault="20px"
								Version="2.00" SelectTypeRowDefault="Single" SelectTypeCellDefault="Single" HeaderClickActionDefault="SortSingle"
								BorderCollapseDefault="Separate" AllowColSizingDefault="Free" CellPaddingDefault="4" RowSelectorsDefault="No"
								Name="gridWebGrid" TableLayout="Fixed">
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
							</DisplayLayout>
							<Bands>
								<igtbl:UltraGridBand></igtbl:UltraGridBand>
							</Bands>
						</igtbl:ultrawebgrid></td>
				</tr>
				<tr class="normal">
					<td colspan="5">
						<table height="100%" cellPadding="0" width="100%">
							<tr>
								<td><asp:checkbox id="chbSelectAll" runat="server" AutoPostBack="True" Text="全选"></asp:checkbox></td>
								<TD class="smallImgButton"><INPUT class="gridExportButton" id="cmdGridExport" type="submit" value="  " name="cmdGridExport"
										runat="server"> |
								</TD>
								<TD class="smallImgButton" align="left"><INPUT class="deleteButton" id="cmdDelete" type="submit" value="  " name="cmdDelete" runat="server">
								</TD>
								<TD><cc1:pagersizeselector id="pagerSizeSelector" runat="server"></cc1:pagersizeselector></TD>
								<td align="right"><cc1:pagertoolbar id="pagerToolBar" runat="server"></cc1:pagertoolbar></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr class="normal">
					<td colspan="5">
						<table class="edit" height="100%" cellPadding="0" width="100%">
							<TBODY>
								<tr>
									<td class="fieldName" noWrap width="50"><asp:label id="lblMOCodeQuery" runat="server">工单号</asp:label></td>
									<td class="fieldValue" noWrap width="80"><asp:textbox id="txtMOCodeEdit" runat="server" Width="130px" CssClass="textbox"></asp:textbox></td>
									<td class="fieldValue" width="10"><div id="divSelectMO" style="DISPLAY:none"><input type="image" src="~/Skin/Image/query.gif" id="cmdSelectMO" runat="server" onclick="SelectMOItem();return false;"
												NAME="cmdSelectMO"></div>
									</td>
									<td class="fieldValue" nowrap><input type="submit" id="cmdTogetherBOM" value="工序BOM" runat="server" onserverclick="cmdOPBOM_ServerClick">
									</td>
					</td>
				</tr>
			</table>
			</TD></TR>
			<tr class="toolBar">
				<td colspan="5">
					<table class="toolBar">
						<tr>
							<td class="toolBar"><INPUT class="submitImgButton" id="cmdAddLot" type="submit" value="批量新增" name="cmdAddLot"
									runat="server" onclick="AddItemLot();return false;"></td>
							<td class="toolBar"><INPUT class="submitImgButton" id="cmdSave" type="submit" value="保 存" name="cmdSave" runat="server" onserverclick="cmdSave_ServerClick"></td>
							<td class="toolBar"><INPUT class="submitImgButton" id="cmdPrint" type="submit" value="打 印" name="cmdAdd" runat="server"
									onclick="OpenPrintWindow();return false;"></td>
							<td class="toolBar"><INPUT class="submitImgButton" id="cmdGoTrans" type="submit" value="去交易" name="cmdTrans"
									runat="server" onserverclick="cmdTrans_ServerClick"></td>
							<td><INPUT class="submitImgButton" id="cmdReturn" type="submit" value="返 回" name="cmdReturn"
									runat="server" onserverclick="cmdReturn_ServerClick"></td>
						</tr>
					</table>
				</td>
			</tr>
			</TBODY></TABLE>
		</form>
	</body>
</HTML>
