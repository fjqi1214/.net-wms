<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="cc2" NameSpace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>

<%@ Page language="c#" Codebehind="FSMTLoadMaterialMP.aspx.cs" AutoEventWireup="True" Inherits="BenQGuru.eMES.Web.SMT.FSMTLoadMaterialMP" %>
<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<HTML>
	<HEAD>
		<title>FFeederMP</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="<%=StyleSheet%>" rel=stylesheet>
	</HEAD>
	<body MS_POSITIONING="GridLayout" scroll="yes">
		<form id="Form1" method="post" runat="server">
			<table height="100%" width="100%" runat="server">
				<tr class="moduleTitle">
					<td><asp:label id="lblTitle" runat="server" CssClass="labeltopic">料站表查询</asp:label></td>
				</tr>
				<tr>
					<td>
						<table class="query" height="100%" width="100%">
							<tr>
								<td class="fieldNameLeft" noWrap><asp:label id="lblItemCodeQuery" runat="server">产品代码</asp:label></td>
								<td class="fieldValue"><asp:TextBox id="txtItemCodeQuery" CssClass="textbox" runat="server"></asp:TextBox></td>
								<td class="fieldName" noWrap><asp:label id="lblSSQuery" runat="server">产线代码</asp:label></td>
								<td class="fieldValue"><asp:TextBox id="txtSSCodeQuery" CssClass="textbox" runat="server"></asp:TextBox></td>
								<td class="fieldName" noWrap><asp:label id="lblMachineCodeQuery" runat="server">机台代码</asp:label></td>
								<td class="fieldValue"><asp:TextBox id="txtMachineCodeQuery" CssClass="textbox" runat="server"></asp:TextBox></td>
								<td noWrap width="100%"></td>
								<td class="fieldName"><INPUT class="submitImgButton" id="cmdQuery" type="submit" value="查 询" name="btnQuery"
										runat="server"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr height="100%">
					<td class="fieldGrid">
                <ig:WebDataGrid ID="gridWebGrid" runat="server" Width="100%">
                </ig:WebDataGrid>
            </td>
				</tr>
				<tr class="normal">
					<td>
						<table height="100%" cellPadding="0" width="100%">
							<tr>
								
								<TD class="smallImgButton"><INPUT class="gridExportButton" id="cmdGridExport" type="submit" value="  " name="cmdGridExport"
										runat="server"> 
								</TD>
								<TD><cc1:pagersizeselector id="pagerSizeSelector" runat="server"></cc1:pagersizeselector></TD>
								<td align="right">
									<cc1:PagerToolbar id="pagerToolBar" runat="server"></cc1:PagerToolbar></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td>
						<table class="query" height="100%" width="100%">
							<tr>
								<td class="fieldNameLeft" noWrap><asp:label id="lblItemCodeEdit" runat="server">产品代码</asp:label></td>
								<td class="fieldValue"><cc2:selectabletextbox id="txtItemCodeEdit" CssClass="textbox" runat="server" type="singleitemcode"></cc2:selectabletextbox></td>
								<td class="fieldName" noWrap><asp:label id="lblSSEdit" runat="server">产线代码</asp:label></td>
								<td class="fieldValue"><cc2:selectabletextbox id="txtSSCodeEdit" CssClass="textbox" runat="server" type="singlestepsequence"></cc2:selectabletextbox></td>
								<td class="fieldName" noWrap><asp:label id="lblMachineCodeEdit" runat="server">机台代码</asp:label></td>
								<td class="fieldValue"><asp:TextBox id="txtMachineCodeEdit" CssClass="textbox" runat="server"></asp:TextBox></td>
							</tr>
							<tr>
								<td class="fieldNameLeft" noWrap><asp:label id="lblMachineStationCodeEdit" runat="server">站位</asp:label></td>
								<td class="fieldValue"><asp:TextBox id="txtMachineStationCodeEdit" CssClass="textbox" runat="server"></asp:TextBox></td>
								<td class="fieldName" noWrap><asp:label id="lblSourceMaterialCodeEdit" runat="server">首选料</asp:label></td>
								<td class="fieldValue"><asp:TextBox id="txtSourceMaterialCodeEdit" CssClass="textbox" runat="server"></asp:TextBox></td>
								<td class="fieldName" noWrap><asp:label id="lblMaterialCodeEdit" runat="server">物料代码</asp:label></td>
								<td class="fieldValue"><asp:TextBox id="txtMaterialCodeEdit" CssClass="textbox" runat="server"></asp:TextBox></td>
							</tr>
							<tr>
								<td class="fieldNameLeft" noWrap><asp:label id="lblFeedeSpecCodeEdit" runat="server">Feeder规格</asp:label></td>
								<td class="fieldValue"><asp:DropDownList id="DropDownListFeederSpecCodeEdit" CssClass="textbox" runat="server" AutoPostBack="False" Width="130px"></asp:DropDownList></td>
								<td class="fieldName" noWrap><asp:label id="lblFeederQtyEdit" runat="server">数量</asp:label></td>
								<td class="fieldValue"><asp:TextBox id="txtQtyEdit" CssClass="textbox" runat="server"></asp:TextBox></td>
								<td class="fieldName" noWrap><asp:label id="lblTableEdit" runat="server">Table</asp:label></td>
								<td class="fieldValue"><asp:DropDownList id="txtTableEdit" CssClass="textbox" runat="server" AutoPostBack="False"></asp:DropDownList></td>
							</tr>
						</table>
					</td>
				</tr>
				
				<tr class="toolBar">
					<td>
						<table class="toolBar">
							<tr>
								<td class="toolBar"><INPUT class="submitImgButton" id="cmdEnter" type="submit" value="导  入" name="cmdEnter"
										runat="server" onserverclick="cmdImport_ServerClick"></td>
								<td class="toolBar" ><INPUT class="submitImgButton" id="cmdAdd" type="submit" value="新 增" name="cmdAdd" runat="server"></td>
								<td class="toolBar" ><INPUT class="submitImgButton" id="cmdSave" type="submit" value="保 存" name="cmdSave" runat="server" visible="false"></td>
								<td><INPUT class="submitImgButton" id="cmdCancel" type="submit" value="取 消" name="cmdCancel"
										runat="server"></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
