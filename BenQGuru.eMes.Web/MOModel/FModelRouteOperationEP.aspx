<%@ Page language="c#" Codebehind="FModelRouteOperationEP.aspx.cs" AutoEventWireup="True" Inherits="BenQGuru.eMES.Web.MOModel.FModelRouteOperationEP" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>FModelRouteOperationEP</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href="<%=StyleSheet%>" rel=stylesheet>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table height="100%" width="100%">
				<!--this is for title-->
				<tr class="moduleTitle">
					<td><asp:label id="lblTitle" runat="server" CssClass="labeltopic"> 产品别工序维护</asp:label></td>
				</tr>
				<!--this is for operation information update-->
				<tr class="normal">
					<td>
						<table class="edit" height="100%" cellPadding="0" width="100%">
							<tr>
								<TD class="fieldNameLeft" noWrap><asp:label id="lblItemOperationCodeEdit" runat="server">工序代码</asp:label></TD>
								<td class="fieldValue"><asp:textbox id="txtOperationCodeEdit" runat="server"></asp:textbox></td>
							</tr>
							<tr>
								<td class="fieldNameLeft" noWrap><asp:label id="lblCodeEdit" runat="server">序号</asp:label></td>
								<td class="fieldValue" style="HEIGHT: 26px"><asp:textbox id="txtOperationsequenceEdit" runat="server"></asp:textbox></td>
								<td nowrap width="100%"></td>
							</tr>
							<tr>
								<td nowrap class="fieldNameLeft"><asp:checkbox id="chbOperationCheckEdit" runat="server" Width="100px" Text="工序检查"></asp:checkbox></td>
								<td nowrap class="fieldValue"><asp:label id="lblOPCheckEdit" runat="server" Width="200px">用以标记该工序是否进行工序检查</asp:label></td>
							</tr>
							<tr>
								<td nowrap class="fieldNameLeft"><asp:checkbox id="chbEditSPC" runat="server" Width="100px" Text="SPC统计"></asp:checkbox></td>
								<td nowrap class="fieldValue"><asp:label id="lblSPCEdit" runat="server" Width="200px">用以标记该工序是否进行SPC统计</asp:label></td>
								<td nowrap width="100%"></td>
							</tr>
							<tr>
								<td nowrap class="fieldNameLeft"><asp:checkbox id="chbCompLoadingEdit" runat="server" Width="100px" Text="上料检查"></asp:checkbox></td>
								<td nowrap class="fieldValue"><asp:label id="lblComponentLoadingEdit" runat="server" Width="200px">用以标记该工序是否为上料工序</asp:label></td>
							</tr>
							<tr>
								<td nowrap class="fieldNameLeft"><asp:checkbox id="chbNGTestEdit" runat="server" Width="100px" Text="不良判定"></asp:checkbox></td>
								<td nowrap class="fieldValue"><asp:label id="lblTestEdit" runat="server" Width="200px">用以标记该工序是否为测试工序</asp:label></td>
								<td nowrap width="100%"></td>
							</tr>
							<tr>
								<td nowrap class="fieldNameLeft"><asp:checkbox id="chbRepairEdit" runat="server" Width="100px" Text="维修"></asp:checkbox></td>
								<td nowrap class="fieldValue"><asp:label id="lblRepair" runat="server" Width="200px">用以标记该工序是否为维修工序</asp:label></td>
							</tr>
							<tr>
								<td nowrap class="fieldNameLeft"><asp:checkbox id="chbStartOpEdit" runat="server" Width="100px" Text="开始工序"></asp:checkbox></td>
								<td nowrap class="fieldValue"><asp:label id="lblStartOperationEdit" runat="server" Width="200px">用以标记该工序是否为开始工序</asp:label></td>
								<td nowrap width="100%"></td>
							</tr>
							<tr>
								<td nowrap class="fieldNameLeft"><asp:checkbox id="chbEndOpEdit" runat="server" Width="100px" Text="结束工序"></asp:checkbox></td>
								<td nowrap class="fieldValue"><asp:label id="lblCloseOperationEdit" runat="server" Width="200px">用以标记该工序是否为结束工序</asp:label></td>
								<td nowrap width="100%"></td>
							</tr>
							<tr>
								<td nowrap><asp:checkbox id="chbPackEdit" runat="server" Width="100px" Text="包装"></asp:checkbox></td>
								<td nowrap class="fieldValue"><asp:label id="lblPackingEdit" runat="server" Width="200px">用以标记该工序是否为包装工序</asp:label></td>
								<td nowrap width="100%"></td>
							</tr>
							<tr>
								<td nowrap><asp:checkbox id="chbIDMergeEdit" runat="server" Text="流程卡号变换" AutoPostBack="True"></asp:checkbox></td>
							</tr>
							<tr>
								<td nowrap class="fieldValue"><asp:label id="lblMNIDMergeEdit" runat="server">用以标记该工序留程卡是否变换</asp:label></td>
								<td nowrap colspan="2"><asp:panel id="pnlMainEdit" runat="server">
										<TABLE height="100%" width="100%">
											<TR>
												<TD noWrap>
													<asp:label id="lblMergeTypeEdit" runat="server" CssClass="ASPLAbType"> 转化类型</asp:label></TD>
												<TD>
													<asp:dropdownlist id="drpMergeTypeEdit" runat="server" Width="150px" Height="18px"></asp:dropdownlist></TD>
												<TD noWrap>
													<asp:label id="lblMergeRule" runat="server" CssClass="ASPLAbType"> 转换比例</asp:label></TD>
												<TD noWrap>
													<asp:label id="lblnumeratorEdit" runat="server" CssClass="ASPLAbType" Width="20px">1</asp:label>
													<asp:label id="Label16" runat="server" CssClass="ASPLAbType" Width="20px">:</asp:label>
													<asp:textbox id="txtDenominatorEdit" runat="server" Width="20px"></asp:textbox></TD>
											</TR>
										</TABLE>
									</asp:panel></td>
								<td nowrap width="100%"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td></td>
				</tr>
				<tr class="toolBar">
					<td>
						<table class="toolBar">
							<tr>
								<td class="toolBar"><INPUT class="submitImgButton" id="cmdSave" type="submit" value="保 存" name="cmdSave" runat="server" onserverclick="cmdSave_ServerClick"></td>
								<td><INPUT class="submitImgButton" id="cmdReturn" type="submit" value="返回" name="cmdReturn"
										runat="server" onserverclick="cmdReturn_ServerClick"></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
