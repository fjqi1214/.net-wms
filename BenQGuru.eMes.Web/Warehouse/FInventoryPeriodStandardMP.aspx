<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FInventoryPeriodStandardMP.aspx.cs" Inherits="BenQGuru.eMES.Web.WarehouseWeb.FInventoryPeriodStandardMP" %>
<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="cc2" NameSpace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<HTML>
	<HEAD>
		<title>FInventoryPeriodStandardMP</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="<%=StyleSheet%>" rel=stylesheet>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table height="100%" width="100%" runat="server">
				<tr class="moduleTitle">
					<td><asp:label id="lblTitle" runat="server" CssClass="labeltopic">库龄指标设定</asp:label></td>
				</tr>
				<tr>
					<td>
						<table class="query" height="100%" width="100%">
							<tr>
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
								<TD class="smallImgButton"><INPUT class="deleteButton" id="cmdDelete" type="submit" value="  " name="cmdDelete" runat="server">
									
								</TD>
								<TD><cc1:pagersizeselector id="pagerSizeSelector" runat="server"></cc1:pagersizeselector></TD>
								<td align="right">
									<cc1:PagerToolbar id="pagerToolBar" runat="server"></cc1:PagerToolbar></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr class="normal">
					<td>
						<table class="edit" height="100%" cellPadding="0" width="100%">
							<tr>
								<td class="fieldNameLeft" style="HEIGHT: 26px" noWrap>
									<asp:label id="lblStorageAttributeEdit" runat="server">库存属性</asp:label>
								</td>
								<td class="fieldValue" style="HEIGHT: 26px">
								    <asp:DropDownList id="ddlStorageAttributeEdit" runat="server" CssClass="require" Width="120px" AutoPostBack="true"></asp:DropDownList>
								</td>
								<td class="fieldNameLeft" style="HEIGHT: 26px" noWrap>
									<asp:label id="lblPeriodGroupEdit" runat="server">账龄组</asp:label>
								</td>
								<td class="fieldValue" style="HEIGHT: 26px">
								    <asp:DropDownList id="ddlPeriodGroupEdit" runat="server" CssClass="require" Width="120px" AutoPostBack="true"></asp:DropDownList>
								</td>
								<td style="width:100%"></td>
						    </tr>
						    <tr>		
								<td class="fieldNameLeft" style="HEIGHT: 26px" noWrap>
									<asp:label id="lblPeriodCodeEdit" runat="server">账龄代码</asp:label>
								</td>
								<td class="fieldValue" style="HEIGHT: 26px">
								    <asp:DropDownList id="ddlPeriodCodeEdit" runat="server" CssClass="require" Width="120px"></asp:DropDownList>
								</td>
								<td class="fieldNameLeft" style="HEIGHT: 26px" noWrap>
									<asp:label id="lblIndexValueEdit" runat="server">指标值</asp:label>
								</td>
								<td class="fieldValue" style="HEIGHT: 26px">
								    <asp:TextBox id="txtIndexValueEdit" runat="server" CssClass="require" Width="120px"></asp:TextBox>
								</td>
								<td style="width:100%"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr class="toolBar">
					<td>
						<table class="toolBar">
							<tr>
								<td class="toolBar"><INPUT class="submitImgButton" id="cmdAdd" type="submit" value="新 增" name="cmdAdd" runat="server"></td>
								<td class="toolBar"><input class="submitImgButton" id="cmdSave" type="submit" value="保存" name="cmdSave" runat="server"/></td>
								<td><INPUT class="submitImgButton" id="cmdCancel" type="submit" value="取 消" name="cmdCancel" runat="server"></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
