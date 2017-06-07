<%@ Register TagPrefix="cc2" NameSpace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<%@ Register TagPrefix="uc1" TagName="eMesDate" Src="../UserControl/DateTime/DateTime/eMesDate.ascx" %>
<%@ Register TagPrefix="uc1" TagName="eMesTime" Src="../UserControl/DateTime/DateTime/eMesTime.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Page language="c#" Codebehind="FSMTRCardItemDetailQP.aspx.cs" AutoEventWireup="True" Inherits="BenQGuru.eMES.Web.SMT.FSMTRCardItemDetailQP" %>
<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<HTML>
	<HEAD>
		<title>SMT物料追溯明细</title>
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
					<td><asp:label id="lblTitle" runat="server" CssClass="labeltopic">SMT物料追溯明细</asp:label></td>
				</tr>
				<tr>
					<td>
						<table class="query" height="100%" width="100%">
							<tr>
								<td class="fieldName" noWrap><asp:label id="lblMOIDQuery" runat="server">工单代码</asp:label></td>
								<td class="fieldValue"><asp:textbox id="txtMOCodeQuery" runat="server" CssClass="textbox" Width="130" ReadOnly="True"></asp:textbox></td>
								<td class="fieldName" noWrap><asp:label id="lblRCardQuery" runat="server">产品序列号</asp:label></td>
								<td class="fieldValue"><asp:textbox id="txtRCardQuery" runat="server" CssClass="textbox" Width="165px" ReadOnly="True"></asp:textbox></td>
								<td></td>
								<td class="fieldName" style="DISPLAY:none"><INPUT class="submitImgButton" id="cmdQuery" type="submit" value="查 询" name="btnQuery"
										runat="server">
								</td>
							</tr>
							<tr>
							</tr>
						</table>
					</td>
				</tr>
				<tr height="100%">
					<td class="fieldGrid">
                <ig:webdatagrid id="gridWebGrid" runat="server" width="100%">
                </ig:webdatagrid>
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
				<tr class="toolBar">
					<td>
						<table class="toolBar">
							<tr>
								<td class="toolBar" style="DISPLAY:none"><INPUT class="submitImgButton" id="cmdAdd" type="submit" value="新 增" name="cmdAdd" runat="server"></td>
								<td class="toolBar" style="DISPLAY:none"><INPUT class="submitImgButton" id="cmdSave" type="submit" value="保 存" name="cmdSave" runat="server"></td>
								<td><INPUT class="submitImgButton" id="cmdCancel" type="submit" value="返 回" name="cmdCancel"
										runat="server" onserverclick="cmdCancel_ServerClick"></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
