<%@ Register TagPrefix="uc1" TagName="eMESDate" Src="~/UserControl/DateTime/DateTime/eMESDate.ascx" %>
<%@ Page language="c#" Codebehind="FManualAlertEP.aspx.cs" AutoEventWireup="True" Inherits="BenQGuru.eMES.Web.Alert.FManualAlertEP" %>
<%@ Register TagPrefix="cc2" Namespace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ItemMP</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href="<%=StyleSheet%>" rel=stylesheet>
		<script>
			function SelectSample()
			{
				var result = window.showModalDialog("FAlertSampleSP.aspx", "" ,"dialogWidth:800px;dialogHeight:600px;scroll:none;help:no;status:no") ;
				if(result!=null)
				{
					document.getElementById("txtAlertMsg").value = result.code;
				}
				else
					return false;
			}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table height="100%" width="100%">
				<tr height="25">
					<td>
						<table class="query" height="25" width="100%">
							<tr>
								<td noWrap width="80"><asp:label id="lblAlertLevel" runat="server"> 预警级别</asp:label></td>
								<td>
									<asp:DropDownList id="drpAlertLevel" runat="server" CssClass="dropdownlist" Width="112px">
										<asp:ListItem Value="*">所有</asp:ListItem>
										<asp:ListItem Value="NG">不良率</asp:ListItem>
										<asp:ListItem Value="PPM">PPM</asp:ListItem>
										<asp:ListItem Value="DirectPass">直通率</asp:ListItem>
										<asp:ListItem Value="CPK">CPK</asp:ListItem>
										<asp:ListItem Value="First">首件下线</asp:ListItem>
									</asp:DropDownList></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td>
						<table class="query" height="100%" width="100%">
							<tr height="30%">
								<td style="WIDTH: 114px"><asp:label id="lblAlertMsg" runat="server">预警消息</asp:label></td>
								<td style="WIDTH: 473px"><asp:textbox id="txtAlertMsg" runat="server" CssClass="textarea" width="100%" TextMode="MultiLine"
										Height="100%"></asp:textbox></td>
								<td><input type="button" id="cmdExampleSelect" onclick="SelectSample();" onmouseover="this.style.backgroundImage='url(../Skin/Image/ButtonBlue.gif)';"
										style="BACKGROUND-IMAGE: url(../Skin/Image/ButtonGray.gif); BACKGROUND-REPEAT: no-repeat"
										onmouseout="this.style.backgroundImage='url(../Skin/Image/ButtonGray.gif)';" class="button"
										value="样例选择"></td>
							</tr>
							<tr height="30%">
								<td style="WIDTH: 114px">
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
											<td><asp:button id="cmdAddUser" onmouseover="this.style.backgroundImage='url(../Skin/Image/ButtonBlue.gif)';"
													style="BACKGROUND-IMAGE: url(../Skin/Image/ButtonGray.gif); BACKGROUND-REPEAT: no-repeat"
													onmouseout="this.style.backgroundImage='url(../Skin/Image/ButtonGray.gif)';" runat="server"
													CssClass="button" Text="添加用户" onclick="cmdAddUser_Click"></asp:button></td>
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
							<tr height="30%">
								<td style="WIDTH: 114px"><asp:label id="lblDesc" runat="server">备注</asp:label></td>
								<td style="WIDTH: 473px"><asp:textbox id="txtDesc" runat="server" CssClass="textarea" Width="100%" TextMode="MultiLine"
										Height="100%"></asp:textbox></td>
								<td>
									<asp:Button id="btnSave" runat="server" Text="保存" Width="0px" Height="0px" onclick="cmdSave_ServerClick"></asp:Button></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr class="toolBar">
					<td>
						<table class="toolBar">
							<tr>
								<td class="toolBar"></td>
								<td class="toolBar"><INPUT class="submitImgButton" id="cmdSave" type="submit" value="保 存" name="cmdSave" runat="server"></td>
								<td><INPUT class="submitImgButton" id="cmdReturn" type="submit" value="返 回" name="cmdCancel"
										runat="server" onserverclick="cmdReturn_ServerClick"></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<script>		
			document.getElementById('stbUser:_ctl2').style.backgroundImage ="";
			document.getElementById('stbUser:_ctl2').style.width = "0px";
			document.getElementById('stbUser:_ctl2').style.height = "0px";
			document.getElementById('stbUser:_ctl0').width = "0px";
			document.getElementById('stbUser:_ctl0').height = "0px";
			
			</script>
		</form>
	</body>
</HTML>
