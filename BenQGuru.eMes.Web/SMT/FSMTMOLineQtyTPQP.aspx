<%@ Register TagPrefix="cc2" NameSpace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Page language="c#" Codebehind="FSMTMOLineQtyTPQP.aspx.cs" AutoEventWireup="True" Inherits="BenQGuru.eMES.Web.SMT.FSMTMOLineQtyTPQP" %>
<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<HTML>
	<HEAD>
		<title>FSMTTargetQtyMP</title>
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
					<td><asp:label id="lblTitle" runat="server" CssClass="labeltopic">工单SMT产量查询</asp:label></td>
				</tr>
				<tr>
					<td>
						<table class="query" height="100%" width="100%">
							<tr>
								<td class="fieldName" noWrap><asp:label id="lblMOIDQuery" runat="server">工单代码</asp:label></td>
								<td class="fieldValue"><asp:TextBox id="txtMOCodeQuery" CssClass="textbox" runat="server" ReadOnly="True"></asp:TextBox></td>
								<td class="fieldName" noWrap><asp:label id="lblTimePeriodCodeQuery" runat="server">时段代码</asp:label></td>
								<td class="fieldValue"><asp:TextBox id="txtTPCodeQuery" CssClass="textbox" runat="server"></asp:TextBox></td>
								<td noWrap width="100%"></td>
								<td class="fieldName"><INPUT class="submitImgButton" id="cmdQuery" type="submit" value="查 询" name="btnQuery"
										runat="server"></td>
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
				<tr class="normal">
					<td>
						<table class="edit" height="100%" cellPadding="0" width="100%">
							<tr>
								<td class="fieldName" noWrap><asp:label id="lblMOIDEdit" runat="server">工单代码</asp:label></td>
								<td class="fieldValue"><asp:TextBox id="txtMOCode" CssClass="textbox" runat="server" Width="150px"></asp:TextBox></td>
								<td class="fieldName" noWrap rowspan="2" valign="top"><asp:label id="lblDiffReason" runat="server">差异原因</asp:label></td>
								<td class="fieldValue" rowspan="2" valign="top"><asp:TextBox id="txtDiffReason" TextMode="MultiLine" CssClass="textbox" runat="server" Width="376px"
										Height="76px"></asp:TextBox>
								</td>
								<td style="DISPLAY:none"><asp:TextBox Runat="server" ID="txtShiftDay" Visible="False"></asp:TextBox></td>
								<td noWrap width="100%">&nbsp;</td>
							</tr>
							<tr>
								<td class="fieldNameLeft" noWrap><asp:label id="lblTimePeriodCodeEdit" runat="server">时段代码</asp:label></td>
								<td class="fieldValue"><asp:TextBox id="txtTPCode" CssClass="textbox" runat="server" Width="150px"></asp:TextBox></td>
								<td noWrap width="100%" colspan="3">&nbsp;</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr class="toolBar">
					<td>
						<table class="toolBar">
							<tr>
								<td class="toolBar" style="DISPLAY:none"><INPUT class="submitImgButton" id="cmdAdd" type="submit" value="新 增" name="cmdAdd" runat="server"></td>
								<td class="toolBar"><INPUT class="submitImgButton" id="cmdSave" type="submit" value="保 存" name="cmdSave" runat="server"></td>
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
