<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Page language="c#" Codebehind="FUserGroup2ModuleNewSP.aspx.cs" AutoEventWireup="True" Inherits="BenQGuru.eMES.Web.BaseSetting.FUserGroup2ModuleNewSP" %>
<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>FShiftMP</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href="<%=StyleSheet%>" rel=stylesheet>
		<script language="javascript">
<!--

    function AfterCellUpdate(tableName, itemName, newText) 
    {
		if (document.getElementById("chbAutoCheckChild").checked == false)
			return;
		var grid = igtbl_getGridById(tableName);
        var row = igtbl_getRowById(itemName);
        var cell = igtbl_getCellById(itemName);
        var column = igtbl_getColumnById(itemName);
        var moduleCode = row.getCellFromKey("ModuleCode").getValue().toString();
		var rowParentModuleCode = row.getCellFromKey("ParentModuleCode").getValue();
        var colIdx = column.Index
        var iRow = row.getIndex();
        var i = 0;
        for (i = iRow + 1; i < grid.Rows.length; i++)
        {
			var row0 = grid.Rows.getRow(i);
			var parentModule = row0.getCellFromKey("ParentModuleCode").getValue();
			if (parentModule == moduleCode)
			{
				var cell0 = row0.getCell(colIdx);
				cell0.setValue(cell.getValue());
			}
			else if (parentModule == rowParentModuleCode)
			{
				break;
			}
        }
        return 0;
    }
    
// -->
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table height="100%" width="100%">
				<tr class="moduleTitle">
					<td><asp:label id="lblFUserGroup2ModuleNewSPTitle" runat="server" CssClass="labeltopic"> 选择模块</asp:label></td>
				</tr>
				<tr>
					<td>
						<table class="query" height="100%" width="100%">
							<tr>
								<td class="fieldNameLeft" noWrap><asp:label id="lblUserGroupCodeQuery" runat="server"> 用户组代码</asp:label></td>
								<td><asp:textbox id="txtUserGroupCodeQuery" runat="server" Width="90px" ReadOnly="True" CssClass="textbox"></asp:textbox></td>
								<td class="fieldName" noWrap style="DISPLAY:none;VISIBILITY:hidden"><asp:label id="lblModuleTypeEdit" runat="server">模块类型</asp:label></td>
								<td class="fieldValue" style="DISPLAY:none;VISIBILITY:hidden"><asp:dropdownlist id="drpModuleTypeEdit" runat="server" CssClass="require" Width="90px" onload="drpModuleTypeEdit_Load"></asp:dropdownlist></td>
								<td class="fieldNameLeft" noWrap><asp:label id="lblModuleeCodeQuery" runat="server"> 模块代码</asp:label></td>
								<td><asp:textbox id="txtModuleCodeQuery" runat="server" Width="90px" CssClass="textbox"></asp:textbox></td>
								<td class="fieldNameLeft" noWrap><asp:label id="lblModuleDescEdit" runat="server">模块描述</asp:label></td>
								<td><asp:textbox id="txtModuleDescEdit" runat="server" CssClass="textbox" Width="90px"></asp:textbox></td>
								<td class="fieldNameLeft" noWrap><asp:label id="lblModuleFormURLQuery" runat="server">Form URL</asp:label></td>
								<td><asp:textbox id="txtModuleFormURLQuery" runat="server" CssClass="textbox" Width="90px"></asp:textbox></td>
								<td noWrap><asp:CheckBox Runat="server" ID="chbAutoCheckChild" Text="自动选择子模块" Checked="True"></asp:CheckBox></td>
								<td noWrap><asp:CheckBox Runat="server" ID="chkShowExistModule" Text="显示现有权限" Checked="True"></asp:CheckBox></td>
								<td width="100%"></td>
								<td class="fieldName"><INPUT class="submitImgButton" id="cmdQuery" type="submit" value="查 询" name="btnQuery"
										runat="server"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr height="100%">
					<td class="fieldGrid"><igtbl:ultrawebgrid id="gridWebGrid" runat="server" Width="100%" Height="100%">
							<DisplayLayout ColWidthDefault="" StationaryMargins="Header" AllowSortingDefault="Yes" RowHeightDefault="20px"
								Version="2.00" SelectTypeRowDefault="Single" SelectTypeCellDefault="Single" HeaderClickActionDefault="SortSingle"
								BorderCollapseDefault="Separate" AllowColSizingDefault="Free" CellPaddingDefault="4" RowSelectorsDefault="No"
								Name="webGrid" TableLayout="Fixed" ViewType="Hierarchical" LoadOnDemand="Manual">
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
								<ClientSideEvents AfterCellUpdateHandler="AfterCellUpdate"></ClientSideEvents>
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
								<td><asp:checkbox id="chbSelectAll" runat="server" Text="全选" AutoPostBack="True"></asp:checkbox></td>
								<TD class="smallImgButton"><INPUT class="deleteButton" id="cmdDelete" type="submit" value="  " name="cmdDelete" runat="server">
									|
								</TD>
								<TD>
									<cc1:pagersizeselector id="pagerSizeSelector" runat="server"></cc1:pagersizeselector></TD>
								<td align="right">
									<cc1:PagerToolbar id="pagerToolBar" runat="server"></cc1:PagerToolbar></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr class="toolBar">
					<td>
						<table class="toolBar">
							<tr>
								<td class="toolBar"><INPUT class="submitImgButton" id="cmdSave" type="submit" value="保 存" name="cmdSave" runat="server"></td>
								<td class="toolBar"><INPUT class="submitImgButton" id="cmdReturn" type="submit" value="返 回" name="cmdReturn"
										runat="server" onserverclick="cmdReturn_ServerClick"></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
