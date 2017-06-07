<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FMOStorgeConfirmMP.aspx.cs" Inherits="BenQGuru.eMES.Web.MOModel.FMOStorgeConfirmMP" %>
<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="uc1" TagName="eMesDate" Src="../UserControl/DateTime/DateTime/eMesDate.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>FMOStorgeConfirmMP</title>
	<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
	<meta content="C#" name="CODE_LANGUAGE">
	<meta content="JavaScript" name="vs_defaultClientScript">
	<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	<link href="<%=StyleSheet%>" rel=stylesheet>
	<script language="javascript">
	    function gridWebGrid_ClickCellButtonHandler(gridName, cellId)
	    {
	        var msg = "";
	        var column = igtbl_getColumnById(cellId);
            if(column.Key == "SyncStatus")
            {
                msg = "确定要同步状态吗？";
            }
            
            if(column.Key == "ConfirmAgain")
            {
                msg = "确定要再次报工吗？";
            }
            var result = false;
            if(msg != "")
            {
               result = !window.confirm(msg);
            }
            
            return result;
	    }
	    function DisableMe(id)
	    {
	        document.getElementById(id).disabled = true;
			__doPostBack(id, '');
	        return true;
	    }
	</script>
</head>
<body>
    <form id="form1" runat="server">
        <table height="100%" width="100%">
			<tr class="moduleTitle">
				<td><asp:label id="lblTitle" runat="server" CssClass="labeltopic">工单报工</asp:label></td>
			</tr>
			<tr>
				<td>
				    <table class="query" height="100%" width="100%">
						<tr>
							<td class="fieldName" noWrap><asp:label id="lblMOCodeQuery" runat="server">工单号</asp:label></td>
							<td class="fieldValue" noWrap><asp:textbox id="txtMoCodeQuery" ReadOnly="true" runat="server" CssClass="textbox" Width="100px"></asp:textbox></td>
							<td class="fieldName" noWrap><asp:label id="lblMOEAttribute2Query" runat="server">倒冲库存地</asp:label></td>
							<td class="fieldValue" noWrap><asp:textbox id="txtMOEAttribute2Query" ReadOnly="true" runat="server" CssClass="textbox" Width="100px"></asp:textbox></td>							
							<td width="100%" ></td>
							<td width="100%" ></td>
							<td width="100%" ></td>
						</tr>
						<tr>
						    <td class="fieldName" nowrap><asp:Label ID="lblCompleteQtyQuery" runat="server">已完工</asp:Label></td>
						    <td class="fieldValue" nowrap><asp:TextBox ID="txtCompleteQtyQuery" ReadOnly="true" runat="server" CssClass="textbox" Width="100px"></asp:TextBox></td>
						    <td class="fieldName" nowrap><asp:Label ID="lblScrapQtyQuery" runat="server">已报废</asp:Label></td>
						    <td class="fieldValue" nowrap><asp:TextBox ID="txtScrapQtyQuery" ReadOnly="true" runat="server" CssClass="textbox" Width="100px"></asp:TextBox></td>
						    <td width="100%"></td>
						    <td width="100%"></td>
						    <td width="100%"></td>
						</tr>
						<tr>
						    <td class="fieldName" nowrap><asp:Label ID="lblCompleteQtyConfirmedQuery" runat="server">已完工(报工)</asp:Label></td>
						    <td class="fieldValue" nowrap><asp:TextBox ID="txtCompleteQtyConfirmedQuery" ReadOnly="true" runat="server" CssClass="textbox" Width="100px"></asp:TextBox></td>
						    <td class="fieldName" nowrap><asp:Label ID="lblScrapQtyConfirmedQuery" runat="server">已报废(报工)</asp:Label></td>
						    <td class="fieldValue" nowrap><asp:TextBox ID="txtScrapQtyConfirmedQuery" ReadOnly="true" runat="server" CssClass="textbox" Width="100px"></asp:TextBox></td>						    
						    <td class="fieldName"><INPUT class="submitImgButton" id="cmdQuery" type="submit" value="查 询" name="btnQuery"
										runat="server" onserverclick="cmdQuery_ServerClick" ></td>
							<td width="100%"></td>
							<td width="100%"></td>	
						</tr>
					</table>
				</td>
			</tr>
<TR height="100%">
				<TD class="fieldGrid"><igtbl:ultrawebgrid id="gridWebGrid" runat="server" Width="100%" Height="100%">
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
							<ClientSideEvents ClickCellButtonHandler="gridWebGrid_ClickCellButtonHandler" />
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
					</igtbl:ultrawebgrid></TD>
			</TR>
			<tr class="normal">
				<td>
					<table class="edit" height="100%" cellPadding="0" width="100%">
						<tr>
							<TD class="fieldNameLeft" noWrap><asp:label id="lblConfirmCompleteQtyEdit" runat="server">报工数量</asp:label></TD>
							<td class="fieldValue"><asp:textbox id="txtConfirmQtyEdit" runat="server" CssClass="require" Width="150px"></asp:textbox></td>
							<TD class="fieldNameLeft" noWrap><asp:label id="lblConfirmScrapQtyEdit" runat="server">报废数量</asp:label></TD>
							<td class="fieldValue"><asp:textbox id="txtConfirmScrapQtyEdit" runat="server" CssClass="require" Width="150px"></asp:textbox></td>
							<td width="100%"></td>
						</tr>
						<tr>
							<td class="fieldNameLeft" noWrap><asp:label id="lblConfirmStatusEdit" runat="server">确认状态</asp:label></td>
							<td class="fieldValue"><asp:DropDownList runat="server" ID="drpConfirmStatusEdit" Width="150px"></asp:DropDownList></td>
							<td class="fieldNameLeft" noWrap><asp:label id="lblManHourEdit" runat="server">工时</asp:label></td>
							<td class="fieldValue"><asp:textbox id="txtManHourEdit" runat="server" CssClass="textbox" Width="150px"></asp:textbox></td>
							<td width="100%"></td>
						</tr>
						<tr>
							<td class="fieldNameLeft" noWrap><asp:label id="lblMachineHourEdit" runat="server">机时</asp:label></td>
							<td class="fieldValue"><asp:textbox id="txtMachineHourEdit" runat="server" CssClass="textbox" Width="150px"></asp:textbox></td>
							<td class="fieldNameLeft" noWrap><asp:label id="lblMOLocationEdit" runat="server">库存地点</asp:label></td>
							<td class="fieldValue"><asp:textbox id="txtMOLocationEdit" runat="server" CssClass="textbox" Width="150px"></asp:textbox></td>
							<td width="100%"></td>
						</tr>
						<tr>
							<td class="fieldNameLeft" noWrap><asp:label id="lblGradeEdit" runat="server">等级</asp:label></td>
							<td class="fieldValue"><asp:textbox id="txtGradeEdit" runat="server" CssClass="textbox" Width="150px"></asp:textbox></td>
							<td class="fieldNameLeft" noWrap><asp:label id="lblOPCodeEdit" runat="server">工序</asp:label></td>
							<td class="fieldValue"><asp:textbox id="txtOPCodeEdit" ReadOnly="true" runat="server" CssClass="textbox" Width="150px"></asp:textbox></td>
							<td width="100%"></td>
						</tr>
						<tr>
						    <td class="fieldNameLeft" noWrap><asp:label id="lblConfirmDateEdit" runat="server">记账日期</asp:label></td>
						    <td class="fieldValue"><uc1:emesdate id="dateConfirmDateEdit" runat="server" CssClass="textbox" width="130"></uc1:emesdate></td>
						    <td></td>
						    <td></td>
						    <td width="100%"></td>
						</tr>
					</table>
				</td>
			</tr>
			<TR class="toolBar">
				<TD>
					<TABLE class="toolBar">
						<TR>
							<TD class="toolBar"><INPUT class="submitImgButton" id="cmdMOConfirm" type="submit" value="报 工"
									name="cmdMOConfirm" runat="server" onserverclick="cmdMOConfirm_ServerClick" onclick="return DisableMe(this.id);"></TD>
						</TR>
					</TABLE>
				</TD>
			</TR>
		</table>
    </form>
</body>
</html>
