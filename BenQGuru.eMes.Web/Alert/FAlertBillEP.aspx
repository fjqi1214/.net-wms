<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="cc2" Namespace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<%@ Page language="c#" Codebehind="FAlertBillEP.aspx.cs" AutoEventWireup="True" Inherits="BenQGuru.eMES.Web.Alert.FAlertBillEP" %>
<%@ Register TagPrefix="uc1" TagName="eMESDate" Src="~/UserControl/DateTime/DateTime/eMESDate.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ItemMP</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href="<%=StyleSheet%>" rel=stylesheet>
		<script language="javascript">
			function setResDisplay()
				{
					document.all.trResource.style.display = 'block';
				}
				
				function setResUnDisplay()
				{
					document.all.trResource.style.display = 'none';
				}
		
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table height="100%" width="100%">
				<tr class="moduleTitle">
					<td><asp:label id="lblTitle" runat="server" CssClass="labeltopic"> 预警设定</asp:label></td>
				</tr>
				<tr height="25">
					<td>
						<table class="query" height="25" width="100%">
							<TBODY>
								<tr>
									<td noWrap><asp:label id="lblAlertTypeEdit" runat="server"> 预警类别</asp:label></td>
									<td>
										<asp:TextBox id="txtAlertType" runat="server" CssClass="textbox" ReadOnly="True"></asp:TextBox></td>
									<td noWrap><asp:label id="lblAlertItemEdit" runat="server"> 预警项次</asp:label></td>
									<td>
										<asp:TextBox id="txtAlertItem" runat="server" CssClass="textbox" ReadOnly="True"></asp:TextBox></td>
									<td noWrap><asp:label id="lblAlertItemCodeEdit" runat="server"> 预警项值</asp:label></td>
									<td>
										<asp:TextBox id="txtItemCode" runat="server" CssClass="textbox" ReadOnly="True"></asp:TextBox></td>
									<td><asp:label id="lblItemCodeEdit" runat="server">产品代码</asp:label></td>
					</td>
					<td><asp:TextBox id="txtItemCodeQuery" runat="server" CssClass="textbox" ReadOnly="True"></asp:TextBox></td>
				</tr>
				<tr height="25" id="trResource">
					<td noWrap><asp:label id="lblAlertResource" runat="server"> 预警资源</asp:label></td>
					<td>
						<asp:TextBox id="txtResource" runat="server" CssClass="textbox" ReadOnly="True"></asp:TextBox></td>
					<td noWrap><asp:label id="lblEcg2ec" runat="server"> 不良代码组：不良代码</asp:label></td>
					<td>
						<asp:TextBox id="txtEcg2Ec" runat="server" CssClass="textbox" ReadOnly="True"></asp:TextBox></td>
					<td noWrap>
					<td>
					</td>
					<td></td>
					<td></td>
				</tr>
			</table>
			</TD>
			<tr height="25">
				<td>
					<table class="query" height="25" width="100%">
						<tr>
							<td noWrap><asp:label id="lblStartNum" runat="server"> 起点数</asp:label></td>
							<td class="fieldValue"><asp:textbox id="txtStartNum" runat="server" CssClass="textbox" Width="120px"></asp:textbox></td>
							<td class="fieldName" noWrap><asp:label id="lblAlertCondition" runat="server"> 预警条件</asp:label></td>
							<td class="fieldValue" noWrap><asp:dropdownlist id="drpOperator" runat="server" CssClass="dropdownlist" AutoPostBack="True" onselectedindexchanged="drpOperator_SelectedIndexChanged">
									<asp:ListItem Value="BW">介于</asp:ListItem>
									<asp:ListItem Value="LE">小于等于</asp:ListItem>
									<asp:ListItem Value="GE">大于等于</asp:ListItem>
								</asp:dropdownlist><asp:textbox id="txtLow" runat="server" CssClass="textbox" Width="80px"></asp:textbox><asp:label id="lblAnd" runat="server">与</asp:label><asp:textbox id="txtUp" runat="server" CssClass="textbox" Width="80px"></asp:textbox></td>
							<td class="fieldName" noWrap><asp:label id="lblEffectiveDateEdit" runat="server"> 生效日期</asp:label></td>
							<td class="fieldValue">
                              <asp:TextBox type="text" id="dateValidDate"  class='datepicker' runat="server"  Width="130px"/>
							<td width="40%"></td>
						</tr>
					</table>
				</td>
			</tr>
			<tr>
				<td>
					<table class="query" height="100%" width="100%">
						<tr>
							<td style="HEIGHT: 88px"><asp:label id="lblAlertMsg" runat="server">预警消息</asp:label></td>
							<td style="WIDTH: 473px; HEIGHT: 88px"><asp:textbox id="txtAlertMsg" runat="server" CssClass="textarea" width="100%" TextMode="MultiLine"
									Height="100%"></asp:textbox></td>
							<td style="HEIGHT: 88px"><asp:label id="lblAlertInfo" runat="server"></asp:label></td>
						</tr>
						<tr>
							<td>
								<table>
									<tr>
										<td><asp:label id="lblReceiver" runat="server">接收人</asp:label></td>
									</tr>
									<tr>
										<td><asp:checkbox id="chbMailNotify" runat="server" Text="邮件通知"></asp:checkbox></td>
									</tr>
								</table>
							</td>
							<td style="WIDTH: 473px"><asp:listbox id="lstUser" runat="server" CssClass="listbox" Width="100%" Height="100%"></asp:listbox></td>
							<td>
								<table>
									<tr>
										<td style="HEIGHT: 25px"><asp:button id="cmdAddUser" onmouseover="this.style.backgroundImage='url(../Skin/Image/ButtonBlue.gif)';"
												style="BACKGROUND-IMAGE: url(../Skin/Image/ButtonGray.gif); BACKGROUND-REPEAT: no-repeat" onmouseout="this.style.backgroundImage='url(../Skin/Image/ButtonGray.gif)';"
												runat="server" CssClass="button" Text="添加用户" onclick="cmdAddUser_Click"></asp:button></td>
									</tr>
									<tr>
										<td><asp:button id="cmdDeleteUser" onmouseover="this.style.backgroundImage='url(../Skin/Image/ButtonBlue.gif)';"
												style="BACKGROUND-IMAGE: url(../Skin/Image/ButtonGray.gif); BACKGROUND-REPEAT: no-repeat"
												onmouseout="this.style.backgroundImage='url(../Skin/Image/ButtonGray.gif)';" runat="server"
												CssClass="button" Text="删除用户" onclick="cmdDeleteUser_Click"></asp:button>
											<cc2:selectabletextbox id="stbUser" runat="server" CssClass="textbox" width="0px" height="0px" Target="usermail"
												Type="usermail"></cc2:selectabletextbox>
										</td>
									</tr>
								</table>
							</td>
						</tr>
						<tr height="30">
							<td style="HEIGHT: 31px" height="31"></td>
							<td style="HEIGHT: 31px" vAlign="bottom" height="31">
								<asp:Label id="lblUserSN" runat="server">用户代码</asp:Label>
								<asp:TextBox id="txtUser" runat="server" CssClass="textbox"></asp:TextBox>
								<asp:Label id="lblEMail" runat="server">电子邮件</asp:Label>
								<asp:TextBox id="txtEMail" runat="server" CssClass="textbox"></asp:TextBox>
								<asp:Button id="cmdAppend" runat="server" Text="添加" onmouseover="this.style.backgroundImage='url(../Skin/Image/ButtonBlue.gif)';"
									style="BACKGROUND-IMAGE: url(../Skin/Image/ButtonGray.gif); BACKGROUND-REPEAT: no-repeat"
									onmouseout="this.style.backgroundImage='url(../Skin/Image/ButtonGray.gif)';" CssClass="button"
									Width="95px" onclick="btnAdd_Click"></asp:Button></td>
							<td style="HEIGHT: 31px" height="31"></td>
						</tr>
						<tr>
							<td><asp:label id="lblDesc" runat="server">备注</asp:label></td>
							<td style="WIDTH: 473px"><asp:textbox id="txtDesc" runat="server" CssClass="textarea" Width="100%" TextMode="MultiLine"
									Height="100%"></asp:textbox></td>
							<td></td>
						</tr>
					</table>
				</td>
			</tr>
			<tr class="toolBar">
				<td>
					<table class="toolBar">
						<tr>
							<td class="toolBar"></td>
							<td class="toolBar"><INPUT class="submitImgButton" id="cmdSave" type="submit" value="保 存" name="cmdSave" runat="server" onserverclick="cmdSave_ServerClick"></td>
							<td><INPUT class="submitImgButton" id="cmdReturn" type="submit" value="返 回" name="cmdCancel"
									runat="server" onserverclick="cmdReturn_ServerClick"></td>
						</tr>
					</table>
				</td>
			</tr>
			</TBODY></TABLE>
			<script>		
			document.getElementById('stbUser$ctl02').style.backgroundImage ="";
			document.getElementById('stbUser$ctl02').style.width = "0px";
			document.getElementById('stbUser$ctl02').style.height = "0px";
			document.getElementById('stbUser$ctl00').width = "0px";
			document.getElementById('stbUser$ctl00').height = "0px";
			
			</script>
		</form>
	</body>
</HTML>
