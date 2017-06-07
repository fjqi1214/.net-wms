<%@ Page language="c#" Codebehind="FSAPStorageQueryQP.aspx.cs" AutoEventWireup="True" Inherits="BenQGuru.Web.ReportCenter.FSAPStorageQueryQP" %>
<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="cc2" NameSpace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<title>FSAPStorageQueryQP</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR" />
		<meta content="C#" name="CODE_LANGUAGE" />
		<meta content="JavaScript" name="vs_defaultClientScript" />
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
		<link href="<%=StyleSheet%>" rel="stylesheet" />
	</head>
	<body>
		<form id="Form1" method="post" runat="server">
			<table style="height:100%; width:100%">
				<tr class="moduleTitle">
					<td><asp:label id="lblTitle" runat="server" CssClass="labeltopic">SAP库存比对</asp:label></td>
				</tr>
				<tr>
					<td>
						<table class="query" style="height:100%; width:100%">
							<tr>
							    <td style="width:10%"></td>
								<td class="fieldName" nowrap="noWrap"><asp:label id="lblFactoryQuery" runat="server">工厂</asp:label></td>
								<td class="fieldValue" nowrap="noWrap"><asp:textbox id="txtFactoryQuery" runat="server" CssClass="require" Width="120px"></asp:textbox></td>
								<td class="fieldName" nowrap="noWrap"><asp:label id="lblStorageQuery" runat="server">库别</asp:label></td>
								<td class="fieldValue" nowrap="noWrap"><cc2:SelectableTextBox id="txtStorageQuery" runat="server" type="storage" readonly="false" cankeyin="true" width="131px" CssClass="require"></cc2:SelectableTextBox></td>
								<td class="fieldName" nowrap="noWrap"><asp:label id="lblItemCodeQuery" runat="server">产品代码</asp:label></td>
								<td class="fieldValue" nowrap="noWrap"><cc2:SelectableTextBox id="txtItemCodeQuery" runat="server" type="singleitem" readonly="false" cankeyin="true" width="131px"></cc2:SelectableTextBox></td>
								<td style="width:10%"></td>	
								<td class="toolBar">
								    <input class="submitImgButtonLong" id="cmdSAPStorageSync" type="submit" value="SAP库存信息同步" name="btnSAPStorageSync" runat="server" onserverclick="cmdSAPStorageSync_ServerClick" />
								</td>															
								<td class="toolBar">								    
								    <input class="submitImgButton" id="cmdCompare" type="submit" value="比 对" name="btnCompare" runat="server" onserverclick="cmdCompare_ServerClick"/>
								</td>
								<td style="width:10%"></td>								
							</tr>
						</table>
					</td>
				</tr>				
				<tr>
					<td>
						<table class="query" style="height:100%; width:100%">
							<tr>
							    <td style="width:10%"></td>
								<td class="fieldValue" nowrap="noWrap"><asp:textbox id="txtInfo" runat="server" CssClass="textbox" Width="600px" Height="32px" Rows="2" TextMode="MultiLine"></asp:textbox></td>
                                <td style="width:10%"></td>	
                                <td class="toolBar">								    
								    <input class="submitImgButton" id="cmdRefresh" type="submit" value="刷 新" name="btnRefresh" runat="server" onserverclick="cmdRefresh_ServerClick"/>
								</td>
								<td style="width:10%"></td>	    
							</tr>												
						</table>
					</td>
				</tr>
				<tr style="height:100%">
					<td class="fieldGrid"><igtbl:ultrawebgrid id="gridWebGrid" runat="server" width="100%" height="100%"><DISPLAYLAYOUT tablelayout="Fixed" name="webGrid" rowselectorsdefault="No" cellpaddingdefault="4"
								allowcolsizingdefault="Free" bordercollapsedefault="Separate" headerclickactiondefault="SortSingle" selecttypecelldefault="Single" selecttyperowdefault="Single" version="2.00" rowheightdefault="20px"
								allowsortingdefault="Yes" stationarymargins="Header" colwidthdefault="">
								<ADDNEWBOX>
									
								</ADDNEWBOX>
								<PAGER>
									
								</PAGER>
								<HEADERSTYLEDEFAULT backcolor="#ABABAB" borderstyle="Dashed" borderwidth="1px" horizontalalign="Left"
									bordercolor="White" font-bold="True" font-size="12px">
									<BORDERDETAILS colorleft="White" colorright="White" widthtop="1px" colorbottom="White" widthleft="1px"
										colortop="White"></BORDERDETAILS>
								</HEADERSTYLEDEFAULT>
								<ROWSELECTORSTYLEDEFAULT backcolor="#EBEBEB"></ROWSELECTORSTYLEDEFAULT>
								<FRAMESTYLE height="100%" width="100%" borderstyle="Groove" borderwidth="0px" bordercolor="#ABABAB"
									font-size="12px" font-names="Verdana"></FRAMESTYLE>
								<FOOTERSTYLEDEFAULT backcolor="LightGray" borderstyle="Groove" borderwidth="0px">
									<BORDERDETAILS colorleft="White" widthtop="1px" widthleft="1px" colortop="White"></BORDERDETAILS>
								</FOOTERSTYLEDEFAULT>
								<ACTIVATIONOBJECT borderstyle="Dotted"></ACTIVATIONOBJECT>
								<EDITCELLSTYLEDEFAULT borderstyle="None" borderwidth="1px" bordercolor="Black" verticalalign="Middle">
									<PADDING bottom="1px"></PADDING>
								</EDITCELLSTYLEDEFAULT>
								<ROWALTERNATESTYLEDEFAULT backcolor="White"></ROWALTERNATESTYLEDEFAULT>
								<ROWSTYLEDEFAULT borderstyle="Solid" borderwidth="1px" horizontalalign="Left" bordercolor="#D7D8D9"
									verticalalign="Middle">
									<PADDING left="3px"></PADDING>
									<BORDERDETAILS widthtop="0px" widthleft="0px"></BORDERDETAILS>
								</ROWSTYLEDEFAULT>
								<IMAGEURLS imagedirectory="/ig_common/WebGrid3/"></IMAGEURLS>
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
							    <td class="smallImgButton"><input class="gridExportButton" id="cmdGridExport" type="submit" value="  " name="cmdCancel"
										runat="server" onserverclick="cmdGridExport_ServerClick" /> |
								</td>
								<td><cc1:PagerSizeSelector id="pagerSizeSelector" runat="server"></cc1:PagerSizeSelector></td>
								<td align="right"><cc1:PagerToolBar id="pagerToolBar" runat="server" pagesize="20"></cc1:PagerToolBar></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</html>
