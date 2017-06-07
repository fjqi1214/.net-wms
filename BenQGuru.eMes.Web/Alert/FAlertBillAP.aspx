<%@ Register TagPrefix="uc1" TagName="eMESDate" Src="~/UserControl/DateTime/DateTime/eMESDate.ascx" %>
<%@ Page language="c#" Codebehind="FAlertBillAP.aspx.cs" AutoEventWireup="True" Inherits="BenQGuru.eMES.Web.Alert.FAlertBillAP" %>
<%@ Register TagPrefix="cc2" Namespace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>添加预警设定</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href="<%=StyleSheet%>" rel=stylesheet>
		<script language="jscript">
				function setResDisplay()
				{
					document.all.trResource.style.display = 'block';
					document.all.trErrorCode.style.display = 'block';
				}
				
				function setResUnDisplay()
				{
					document.all.trResource.style.display = 'none';
					document.all.trErrorCode.style.display = 'none';
				}
				
				function SuccessAlert()
				{
					alert("保存成功!");
					document.all.cmdReturn.click();
				}
				
		</script>
	</HEAD>
	<body scroll="yes" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table height="100%" width="100%">
				<tr class="moduleTitle">
					<td><asp:label id="lblTitle" runat="server" CssClass="labeltopic"> 预警设定</asp:label></td>
				</tr>
				<tr>
					<td>
						<table class="query" height="100%" width="100%">
							<tr>
								<td noWrap><asp:label id="lblAlertTypeEdit" runat="server"> 预警类别</asp:label></td>
								<td><asp:radiobuttonlist id="rblAlertType" runat="server" AutoPostBack="True" RepeatDirection="Horizontal"
										Width="564px" onselectedindexchanged="rblAlertType_SelectedIndexChanged">
										<asp:ListItem Value="NG" Selected="True">不良率</asp:ListItem>
										<asp:ListItem Value="PPM">PPM</asp:ListItem>
										<asp:ListItem Value="DirectPass">直通率</asp:ListItem>
										<asp:ListItem Value="CPK">CPK</asp:ListItem>
										<asp:ListItem Value="First">首件下线(单位:分钟)</asp:ListItem>
										<asp:ListItem Value="ResourceNG">资源不良数(单位:个)</asp:ListItem>
									</asp:radiobuttonlist></td>
							</tr>
							<tr>
								<td noWrap><asp:label id="lblAlertItemEdit" runat="server"> 预警项次</asp:label></td>
								<td><asp:radiobutton id="rdbRes" runat="server" AutoPostBack="True" Width="72px" Checked="True" GroupName="rblAlertItem"
										Text="资源" oncheckedchanged="rblAlertItem_SelectedIndexChanged"></asp:radiobutton><asp:radiobutton id="rdbItemEdit" runat="server" AutoPostBack="True" Width="96px" GroupName="rblAlertItem"
										Text="产品" oncheckedchanged="rblAlertItem_SelectedIndexChanged"></asp:radiobutton><asp:radiobutton id="rdbModel" runat="server" AutoPostBack="True" Width="80px" GroupName="rblAlertItem"
										Text="机种" oncheckedchanged="rblAlertItem_SelectedIndexChanged"></asp:radiobutton><asp:radiobutton id="rdbSS" runat="server" AutoPostBack="True" Width="88px" GroupName="rblAlertItem"
										Text="产线" oncheckedchanged="rblAlertItem_SelectedIndexChanged"></asp:radiobutton><asp:radiobutton id="rdbSegment" runat="server" AutoPostBack="True" Width="80px" GroupName="rblAlertItem"
										Text="工段" Enabled="False" oncheckedchanged="rblAlertItem_SelectedIndexChanged"></asp:radiobutton></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td>
						<table class="query" height="100%" width="100%">
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
                                </td>
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
							<tr>
								<td style="HEIGHT: 93px"><asp:label id="lblAlertItem" runat="server">预警产品</asp:label></td>
								<td style="WIDTH: 473px; HEIGHT: 93px"><asp:listbox id="lstItem" runat="server" CssClass="listbox" Width="100%" Height="100%"></asp:listbox></td>
								<td style="HEIGHT: 93px">
									<table>
										<TBODY>
											<tr>
												<td><asp:button id="cmdAddItem" onmouseover="this.style.backgroundImage='url(../Skin/Image/ButtonBlue.gif)';"
														style="BACKGROUND-IMAGE: url(../Skin/Image/ButtonGray.gif); BACKGROUND-REPEAT: no-repeat"
														onmouseout="this.style.backgroundImage='url(../Skin/Image/ButtonGray.gif)';" runat="server"
														CssClass="button" Text="添加产品" onclick="cmdAddItem_Click"></asp:button></td>
											</tr>
											<tr>
												<td><asp:button id="cmdDeleteItem" onmouseover="this.style.backgroundImage='url(../Skin/Image/ButtonBlue.gif)';"
														style="BACKGROUND-IMAGE: url(../Skin/Image/ButtonGray.gif); BACKGROUND-REPEAT: no-repeat"
														onmouseout="this.style.backgroundImage='url(../Skin/Image/ButtonGray.gif)';" runat="server"
														CssClass="button" Text="删除产品" onclick="cmdDeleteItem_Click"></asp:button><cc2:selectabletextbox id="stbItem" runat="server" CssClass="textbox" width="0px" height="0px" Target="resource"
														Type="resource"></cc2:selectabletextbox></td>
								</td>
							</tr>
						</table>
					</td>
				<TR id="trProduct" runat="server">
					<TD style="HEIGHT: 93px"><asp:label id="lblAlertItem2" runat="server">预警产品</asp:label></TD>
					<TD style="WIDTH: 473px; HEIGHT: 93px"><asp:listbox id="lstProduct" runat="server" CssClass="listbox" Width="100%" Height="100%"></asp:listbox></TD>
					<TD style="HEIGHT: 93px">
						<TABLE id="tblProduct">
							<TR>
								<TD><asp:button id="cmdAddProduct" onmouseover="this.style.backgroundImage='url(../Skin/Image/ButtonBlue.gif)';"
										style="BACKGROUND-IMAGE: url(../Skin/Image/ButtonGray.gif); BACKGROUND-REPEAT: no-repeat"
										onmouseout="this.style.backgroundImage='url(../Skin/Image/ButtonGray.gif)';" runat="server"
										CssClass="button" Text="添加产品" onclick="cmdAddProduct_Click"></asp:button></TD>
							</TR>
							<TR>
								<TD><asp:button id="cmdDeleteProduct" onmouseover="this.style.backgroundImage='url(../Skin/Image/ButtonBlue.gif)';"
										style="BACKGROUND-IMAGE: url(../Skin/Image/ButtonGray.gif); BACKGROUND-REPEAT: no-repeat"
										onmouseout="this.style.backgroundImage='url(../Skin/Image/ButtonGray.gif)';" runat="server"
										CssClass="button" Text="删除产品" onclick="cmdDeleteProduct_Click"></asp:button></TD>
							</TR>
						</TABLE>
						<cc2:selectabletextbox id="sbProduct" runat="server" CssClass="textbox" width="0px" height="0px" Target="item"
							Type="item"></cc2:selectabletextbox></TD>
				</TR>
				<tr id="trResource">
					<td style="HEIGHT: 78px">
						<table>
							<tr>
								<td><asp:label id="lblAlertResource" runat="server">预警资源</asp:label></td>
							</tr>
						</table>
					</td>
					<td style="WIDTH: 473px; HEIGHT: 78px"><asp:listbox id="lstResource" runat="server" CssClass="listbox" Width="100%" Height="100%"></asp:listbox></td>
					<td style="HEIGHT: 78px">
						<table>
							<tr>
								<td><cc2:selectabletextbox id="stbResource" runat="server" CssClass="textbox" width="0px" height="0px" Target="resource"
										Type="resource"></cc2:selectabletextbox></td>
								<td></td>
							</tr>
							<tr>
								<td><asp:button id="cmdAddResource" onmouseover="this.style.backgroundImage='url(../Skin/Image/ButtonBlue.gif)';"
										style="BACKGROUND-IMAGE: url(../Skin/Image/ButtonGray.gif); BACKGROUND-REPEAT: no-repeat"
										onmouseout="this.style.backgroundImage='url(../Skin/Image/ButtonGray.gif)';" runat="server"
										CssClass="button" Text="添加资源" onclick="cmdAddResource_Click"></asp:button></td>
								<td></td>
							</tr>
							<tr>
								<td><asp:button id="cmdDeleteResource" onmouseover="this.style.backgroundImage='url(../Skin/Image/ButtonBlue.gif)';"
										style="BACKGROUND-IMAGE: url(../Skin/Image/ButtonGray.gif); BACKGROUND-REPEAT: no-repeat"
										onmouseout="this.style.backgroundImage='url(../Skin/Image/ButtonGray.gif)';" runat="server"
										CssClass="button" Text="删除资源" onclick="cmdDeleteResource_Click"></asp:button></td>
								<td></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr id="trErrorCode">
					<td style="HEIGHT: 78px">
						<table>
							<tr>
								<td><asp:label id="lblNGCodeEdit" runat="server">不良代码</asp:label></td>
							</tr>
						</table>
					</td>
					<td style="WIDTH: 473px; HEIGHT: 78px"><asp:listbox id="lstErrorCode" runat="server" CssClass="listbox" Width="100%" Height="100%"></asp:listbox></td>
					<td style="HEIGHT: 78px">
						<table>
							<tr>
								<td><cc2:selectabletextbox id="stbErrorCode" runat="server" CssClass="textbox" width="0px" height="0px" Target="resource"
										Type="errorcode"></cc2:selectabletextbox></td>
								<td></td>
							</tr>
							<tr>
								<td><asp:button id="cmdAddErrorCode" onmouseover="this.style.backgroundImage='url(../Skin/Image/ButtonBlue.gif)';"
										style="BACKGROUND-IMAGE: url(../Skin/Image/ButtonGray.gif); BACKGROUND-REPEAT: no-repeat"
										onmouseout="this.style.backgroundImage='url(../Skin/Image/ButtonGray.gif)';" runat="server"
										CssClass="button" Text="添加不良代码" onclick="cmdAddErrorCode_Click"></asp:button></td>
								<td></td>
							</tr>
							<tr>
								<td><asp:button id="cmdDeleteErrorCode" onmouseover="this.style.backgroundImage='url(../Skin/Image/ButtonBlue.gif)';"
										style="BACKGROUND-IMAGE: url(../Skin/Image/ButtonGray.gif); BACKGROUND-REPEAT: no-repeat"
										onmouseout="this.style.backgroundImage='url(../Skin/Image/ButtonGray.gif)';" runat="server"
										CssClass="button" Text="删除不良代码" onclick="cmdDeleteErrorCode_Click"></asp:button></td>
								<td></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td style="HEIGHT: 78px">
						<table>
							<tr>
								<td style="HEIGHT: 20px"><asp:label id="lblReceiver" runat="server">接收人</asp:label></td>
							</tr>
							<tr>
								<td><asp:checkbox id="chbMailNotify" runat="server" Text="邮件通知"></asp:checkbox></td>
							</tr>
						</table>
					</td>
					<td style="WIDTH: 473px; HEIGHT: 78px"><asp:listbox id="lstUser" runat="server" CssClass="listbox" Width="100%" Height="100%"></asp:listbox></td>
					<td style="HEIGHT: 78px">
						<table>
							<tr>
								<td><cc2:selectabletextbox id="stbUser" runat="server" CssClass="textbox" width="0px" height="0px" Target="usermail"
										Type="usermail"></cc2:selectabletextbox></td>
								<td></td>
							</tr>
							<tr>
								<td><asp:button id="cmdAddUser" onmouseover="this.style.backgroundImage='url(../Skin/Image/ButtonBlue.gif)';"
										style="BACKGROUND-IMAGE: url(../Skin/Image/ButtonGray.gif); BACKGROUND-REPEAT: no-repeat"
										onmouseout="this.style.backgroundImage='url(../Skin/Image/ButtonGray.gif)';" runat="server"
										CssClass="button" Text="添加用户" onclick="cmdAddUser_Click"></asp:button></td>
								<td></td>
							</tr>
							<tr>
								<td><asp:button id="cmdDeleteUser" onmouseover="this.style.backgroundImage='url(../Skin/Image/ButtonBlue.gif)';"
										style="BACKGROUND-IMAGE: url(../Skin/Image/ButtonGray.gif); BACKGROUND-REPEAT: no-repeat"
										onmouseout="this.style.backgroundImage='url(../Skin/Image/ButtonGray.gif)';" runat="server"
										CssClass="button" Text="删除用户" onclick="cmdDeleteUser_Click"></asp:button></td>
								<td></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr class="toolBar">
					<td></td>
					<td><asp:label id="lblUserSN" runat="server">用户代码</asp:label><asp:textbox id="txtUser" runat="server" CssClass="textbox"></asp:textbox><asp:label id="lblEMail" runat="server">电子邮件</asp:label><asp:textbox id="txtEMail" runat="server" CssClass="textbox"></asp:textbox><asp:button id="cmdAppend" onmouseover="this.style.backgroundImage='url(../Skin/Image/ButtonBlue.gif)';"
							style="BACKGROUND-IMAGE: url(../Skin/Image/ButtonGray.gif); BACKGROUND-REPEAT: no-repeat" onmouseout="this.style.backgroundImage='url(../Skin/Image/ButtonGray.gif)';" runat="server" CssClass="button" Width="92px" Text="添加" Height="26px" onclick="cmdAdd_Click"></asp:button></td>
					<td></td>
				</tr>
				<tr>
					<td><asp:label id="lblMemoEdit" runat="server">备注</asp:label></td>
					<td style="WIDTH: 473px"><asp:textbox id="txtDesc" runat="server" CssClass="textarea" Width="100%" TextMode="MultiLine"
							Height="100%"></asp:textbox></td>
					<td></td>
				</tr>
			</table>
			</TD></TR>
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
			try
			{
			document.getElementById('sbProduct$ctl02').style.backgroundImage ="";
			document.getElementById('sbProduct$ctl02').style.width = "0px";
			document.getElementById('sbProduct$ctl02').style.height = "0px";
			document.getElementById('sbProduct$ctl00').width = "0px";
			document.getElementById('sbProduct$ctl00').height = "0px";
			}
			catch(e){}
			try{
			document.getElementById('stbItem$ctl02').style.backgroundImage ="";
			document.getElementById('stbItem$ctl02').style.width = "0px";
			document.getElementById('stbItem$ctl02').style.height = "0px";
			document.getElementById('stbItem$ctl00').width = "0px";
			document.getElementById('stbItem$ctl00').height = "0px";
			}catch(e){}
			try{
			document.getElementById('stbUser$ctl02').style.backgroundImage ="";
			document.getElementById('stbUser$ctl02').style.width = "0px";
			document.getElementById('stbUser$ctl02').style.height = "0px";
			document.getElementById('stbUser$ctl00').width = "0px";
			document.getElementById('stbUser$ctl00').height = "0px";
			}catch(e){}
			document.getElementById('stbResource$ctl02').style.backgroundImage ="";
			document.getElementById('stbResource$ctl02').style.width = "0px";
			document.getElementById('stbResource$ctl02').style.height = "0px";
			document.getElementById('stbResource$ctl00').width = "0px";
			document.getElementById('stbResource$ctl00').height = "0px";
			try{
			document.getElementById('stbErrorCode$ctl02').style.backgroundImage ="";
			document.getElementById('stbErrorCode$ctl02').style.width = "0px";
			document.getElementById('stbErrorCode$ctl02').style.height = "0px";
			document.getElementById('stbErrorCode$ctl00').width = "0px";
			document.getElementById('stbErrorCode$ctl00').height = "0px";
			}catch(e){}
			</script>
		</form>
	</body>
</HTML>
