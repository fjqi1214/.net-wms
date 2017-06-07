<%@ Page language="c#" Codebehind="FReportMain.aspx.cs" AutoEventWireup="True" Inherits="BenQGuru.eMES.Web.FReportMain" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>
			<%=Title%>
		</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href="<%=StyleSheet%>" type=text/css rel=stylesheet>
		<style type="text/css">
			.Header { BACKGROUND-IMAGE: url(Skin/Image/banner_middle.gif); BACKGROUND-REPEAT: repeat-x }
			.Header_Banner { BACKGROUND-POSITION: left 50%; BACKGROUND-IMAGE: url(Skin/Image/banner_left.gif); WIDTH: 100%; BACKGROUND-REPEAT: no-repeat; HEIGHT: 53px }
			.Header_Banner_Width { WIDTH: 250px }
		</style>
		<script language="javascript">
			function menuAction()
			{
				if(document.getElementById('imgIOC').alt == '显示')
				{
					document.getElementById('imgIOC').src = 'skin/image/frame_hide.gif';
					document.getElementById('imgIOC').alt = '隐藏';
					document.getElementById('tdLeftNav').style.visibility="visible";
					document.getElementById('tdLeftNav').style.display="";
					document.getElementById('content').src="WebQuery/ReportCenter.aspx";
				}
				else
				{
					document.getElementById('imgIOC').src = 'skin/image/frame_show.gif';
					document.getElementById('imgIOC').alt = '显示';
					document.getElementById('tdLeftNav').style.visibility="hidden";
					document.getElementById('tdLeftNav').style.display="none";
					document.getElementById('content').src="WebQuery/ReportCenter.aspx";
				}
			}
		</script>
		<script language="javascript" id="clientEventHandlersJS">
			var selectItem = null; 		//当前选定项目
			<!--
			//更改密码
			function toolbarPassword_onclick() {
				window.showModalDialog("./FUserPassWordModifyMP.aspx","",showDialog(6));
			}
			
			//报表中心
			function toolbarMaintain_onclick()
			{
				window.document.location.href = "FStartPage.aspx";
			}
			
			//报表平台
			function toolbarReportView_onclick()
			{
				window.document.location.href = "FReportViewMain.aspx";
			}
			
			// 设置配置
			function ConfigReportView()
			{
				var result = window.showModalDialog("WebQuery/ReportCenterViewConfigEP.aspx", "" ,"dialogWidth:800px;dialogHeight:600px;scroll:none;help:no;status:no") ;
				if (result == "OK")
				{
					document.getElementById('content').src="WebQuery/ReportCenter.aspx";
				}
			}
//-->
		</script>
	</HEAD>
	<body>
	<form id="Form1" method="post" runat="server">
		<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
			<tr>
				<!--
				<td height="59">
					<table class="Header_bborder" cellSpacing="0" cellPadding="0" width="100%" bgColor="#b72025"
						border="0">
						<tr>
							<td width="281"><IMG height="59" src="skin/image/logo.jpg" width="281"></td>
							<td class="Header"><asp:label id="lblEmesSystem" runat="server">达方制造执行管理系统</asp:label></td>
							<td width="59"><IMG height="59" src="skin/image/ol.jpg" width="244"></td>
						</tr>
					</table>
				</td>
				-->
				<TD class="Header" height="53">
					<TABLE class="Header_banner" id="tableTitle" height="100%" cellSpacing="0" cellPadding="0"
						width="100%" border="0">
						<TR>
							<TD class="Header_banner_width"></TD>
							<TD vAlign="middle" noWrap>
								<TABLE id="tableWelcomeMSG" cellSpacing="0" cellPadding="0" border="0">
									<tr>
										<TD style="PADDING-LEFT: 8px" noWrap width="300"><asp:label id="lblSystemName" runat="server" CssClass="systemName"></asp:label></TD>
									</tr>
									<TR>
										<TD noWrap>
											<asp:button id="log" runat="server" Width="1px" BorderStyle="None" BorderColor="Transparent"
												Height="1px" BackColor="Transparent"></asp:button>
											<asp:label id="lblDepartmentName" runat="server" CssClass="welcome" style="color:#FFFFFF">DepartmentName</asp:label>&nbsp;&nbsp;
											<asp:label id="lblUserName" runat="server" CssClass="welcome" style="color:#FFFFFF">Customer</asp:label>&nbsp;&nbsp;
											<asp:label id="lblWelcome" runat="server" CssClass="welcome" style="color:#FFFFFF"> 欢迎使用本系统</asp:label>
										</TD>
									</TR>
								</TABLE>
							</TD>
							<TD vAlign="bottom" align="right" width="486" background="./Skin/Image/banner_right_rpt.gif">
								<table cellSpacing="0" cellPadding="0" border="0">
									<tr>
										<td><IMG height="17" src="./Skin/Image/icon_home.gif" width="17"></td>
										<td width="4">&nbsp;</td>
										<td><a href="#" onclick="toolbarMaintain_onclick();return false;">作业中心</a></td>
										<td width="8">&nbsp;&nbsp;&nbsp;&nbsp;</td>
										<td><IMG height="17" src="./Skin/Image/ReportView.ico" width="17"></td>
										<td width="4">&nbsp;</td>
										<td><a href="#" onclick="toolbarReportView_onclick();return false;">报表平台</a></td>
										<td width="8">&nbsp;&nbsp;&nbsp;&nbsp;</td>
										<td><IMG height="16" src="./Skin/Image/icon_config.gif" width="16"></td>
										<td width="4">&nbsp;</td>
										<td> <a href="#" onclick="ConfigReportView();return false;">显示配置</a></td>
										<!--
										<td width="8">&nbsp;</td>
										<td><IMG height="16" src="./Skin/Image/icon_exit.gif" width="16"></td>
										<td width="4">&nbsp;</td>
										<td> <a href="login.htm">退出</a></td>
										-->
										<td width="8">&nbsp;&nbsp;&nbsp;&nbsp;</td>
										<td vAlign="top"><IMG height="16" src="./Skin/Image/icon_help.gif" width="16"></td>
										<td width="4">&nbsp;</td>
										<td>
											<a href="#">帮助</a></td>
										<td width="8">&nbsp;</td>
									</tr>
									<tr>
										<td colSpan="16" height="3"></td>
									</tr>
								</table>
							</TD>
						</TR>
					</TABLE>
				</TD>
			</tr>
			<tr>
				<td vAlign="top" height="100%">
					<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
						<tr>
							<td width="190" bgColor="#f6f6f6" height="100%" id="tdLeftNav">
								<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
									<tr>
										<td width="190" bgColor="#f6f6f6" height="100%">
											<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
												<tr>
													<td align="center" width="190" background="skin/image/bg_head.gif" height="41"><A href="WebQuery/ReportCenter.aspx" target="content"><IMG height="23" src="skin/image/ico_home.gif" width="70" border="0"></A>&nbsp;&nbsp;
														<input id="imgLogout" runat="server" type="image" src="skin/image/ico_exit.gif"
															name="imageField2"></td>
												</tr>
												<tr>
													<td class="ModuleMenu_bg">
														<table cellSpacing="0" cellPadding="0" width="83%" align="right" border="0">
															<tr>
																<td width="18"><IMG height="16" src="skin/image/ico_menu.gif" width="14"></td>
																<td class="ModuleMenu_txt"><asp:label id="lblQuantity" runat="server">产量报表</asp:label></td>
															</tr>
														</table>
													</td>
												</tr>
												<tr>
													<td>
														<table cellSpacing="5" cellPadding="0" width="75%" align="right" border="0">
															<tr>
																<td><IMG height="9" src="skin/image/ico_arrow0.gif" width="9">&nbsp;<A href="BenQGuru.Web.ReportCenter/FRealTimeQuantitySummaryQP2.aspx" target="content"><asp:label id="lblRealTimeQuantitySummaryQP" runat="server">实时产量</asp:label></A></td>
															</tr>
															<tr>
																<td><IMG height="9" src="skin/image/ico_arrow0.gif" width="9">&nbsp;<A href="BenQGuru.Web.ReportCenter/FHistroyQuantitySummaryQP2.aspx" target="content"><asp:label id="lblHistroyQuantitySummaryQP" runat="server">历史产量</asp:label></A></td>
															</tr>
															<tr>
																<td><IMG height="9" src="skin/image/ico_arrow0.gif" width="9">&nbsp;<A href="BenQGuru.Web.ReportCenter/FRealTimeInputOutputQTY2.aspx" target="content"><asp:label id="lblRealTimeInputOutputQTY" runat="server">实时工单投入产出</asp:label></A></td>
															</tr>
															<tr>
																<td><IMG height="9" src="skin/image/ico_arrow0.gif" width="9">&nbsp;<A href="BenQGuru.Web.ReportCenter/FTimeQuantitySumQP2.aspx" target="content"><asp:label id="lblTimeQuantitySumQP" runat="server">时间段产量</asp:label></A></td>
															</tr>
														</table>
													</td>
												</tr>
												<tr>
													<td class="ModuleMenu_bg">
														<table cellSpacing="0" cellPadding="0" width="83%" align="right" border="0">
															<tr>
																<td width="18"><IMG height="16" src="skin/image/ico_menu.gif" width="14"></td>
																<td class="ModuleMenu_txt"><asp:label id="lblYieldRPT" runat="server">良率报表</asp:label></td>
															</tr>
														</table>
													</td>
												</tr>
												<tr>
													<td>
														<table cellSpacing="5" cellPadding="0" width="75%" align="right" border="0">
															<tr>
																<td><IMG height="9" src="skin/image/ico_arrow0.gif" width="9">&nbsp;<A href="BenQGuru.Web.ReportCenter/FRealTimeYieldPercentQP2.aspx" target="content"><asp:label id="lblRealTimeYieldPercentQP" runat="server">实时良率</asp:label></A></td>
															</tr>
															<tr>
																<td><IMG height="9" src="skin/image/ico_arrow0.gif" width="9">&nbsp;<A href="BenQGuru.Web.ReportCenter/FHistroyYieldPercentSummaryQP2.aspx" target="content"><asp:label id="lblHistroyYieldPercentSummaryQP" runat="server">历史良率</asp:label></A></td>
															</tr>
															<tr>
																<td><IMG height="9" src="skin/image/ico_arrow0.gif" width="9">&nbsp;<A href="BenQGuru.Web.ReportCenter/FRealTimeDefectQP2.aspx" target="content"><asp:label id="lblRealTimeDefectQP" runat="server">实时缺陷分析</asp:label></A></td>
															</tr>
														</table>
													</td>
												</tr>
												<tr>
													<td class="ModuleMenu_bg">
														<table cellSpacing="0" cellPadding="0" width="83%" align="right" border="0">
															<tr>
																<td width="18"><IMG height="16" src="skin/image/ico_menu.gif" width="14"></td>
																<td class="ModuleMenu_txt"><asp:label id="lblQuality" runat="server">品质报表</asp:label></td>
															</tr>
														</table>
													</td>
												</tr>
												<tr>
													<td>
														<table cellSpacing="5" cellPadding="0" width="75%" align="right" border="0">
															<tr>
																<td><IMG height="9" src="skin/image/ico_arrow0.gif" width="9">&nbsp;<A href="BenQGuru.Web.ReportCenter/FOQCFirstHandingYieldQP2.aspx" target="content"><asp:label id="lblEligibility" runat="server">一次效验合格率</asp:label></A></td>
															</tr>
															<tr>
																<td><IMG height="9" src="skin/image/ico_arrow0.gif" width="9">&nbsp;<A href="BenQGuru.Web.ReportCenter/FOQCLRR2.aspx" target="content">OQC 
																		LRR</A>
																</td>
															</tr>
															<tr>
																<td><IMG height="9" src="skin/image/ico_arrow0.gif" width="9">&nbsp;<A href="BenQGuru.Web.ReportCenter/FOQCSDR2.aspx" target="content">OQC 
																		SDR</A>
																</td>
															</tr>
														</table>
													</td>
												</tr>
												<tr>
													<td class="ModuleMenu_bg">
														<table cellSpacing="0" cellPadding="0" width="83%" align="right" border="0">
															<tr>
																<td width="18"><IMG height="16" src="skin/image/ico_menu.gif" width="14"></td>
																<td class="ModuleMenu_txt"><asp:label id="lblRepairRPT" runat="server">维修报表</asp:label></td>
															</tr>
														</table>
													</td>
												</tr>
												<tr>
													<td>
														<table cellSpacing="5" cellPadding="0" width="75%" align="right" border="0">
															<tr>
																<td><IMG height="9" src="skin/image/ico_arrow0.gif" width="9">&nbsp;<A href="BenQGuru.Web.ReportCenter/FTSInfoQP2.aspx" target="content"><asp:label id="lblTSInfoQP" runat="server">维修资料统计</asp:label></A></td>
															</tr>
															<tr>
																<td><IMG height="9" src="skin/image/ico_arrow0.gif" width="9">&nbsp;<A href="BenQGuru.Web.ReportCenter/FTSPerformanceQP2.aspx" target="content"><asp:label id="lblTSPerformanceQP" runat="server">维修绩效查询</asp:label></A></td>
															</tr>
															<tr>
																<td><IMG height="9" src="skin/image/ico_arrow0.gif" width="9">&nbsp;<A href="BenQGuru.Web.ReportCenter/FTSLocECodeQP2.aspx" target="content"><asp:label id="lblTSLocECodeQP" runat="server">不良位置原因分析</asp:label></A></td>
															</tr>
															<tr>
																<td><IMG height="9" src="skin/image/ico_arrow0.gif" width="9">&nbsp;<A href="BenQGuru.Web.ReportCenter/FTSECodeQP2.aspx" target="content"><asp:label id="lblTSECodeQP" runat="server">维修不良率</asp:label></A></td>
															</tr>
														</table>
													</td>
												</tr>
												<tr>
													<td class="ModuleMenu_bg">
														<table cellSpacing="0" cellPadding="0" width="83%" align="right" border="0">
															<tr>
																<td width="18"><IMG height="16" src="skin/image/ico_menu.gif" width="14"></td>
																<td class="ModuleMenu_txt"><asp:label id="lblTPTLong" runat="server">TPT及长尾巴报表</asp:label></td>
															</tr>
														</table>
													</td>
												</tr>
												<tr>
													<td vAlign="top" height="100%">
														<table cellSpacing="5" cellPadding="0" width="75%" align="right" border="0">
															<tr>
																<td><IMG height="9" src="skin/image/ico_arrow0.gif" width="9">&nbsp;<A href="WebQuery/ReportCenterTPT.aspx" target="content"><asp:label id="lblReportCenterTPT" runat="server">工单TPT报表</asp:label></A></td>
															</tr>
															<tr>
																<td><IMG height="9" src="skin/image/ico_arrow0.gif" width="9">&nbsp;<A href="WebQuery/ReportCenterLong.aspx" target="content"><asp:label id="lblReportCenterLong" runat="server">维修长尾巴报表</asp:label></A></td>
															</tr>
														</table>
													</td>
												</tr>
											</table>
										</td>
									</tr>
								</table>
							</td>
							<td width="6" background="skin/image/frame_bg.gif" height="100%"><IMG id="imgIOC" style="CURSOR: hand" onclick="javascript:menuAction();" height="41"
									alt="隐藏" src="skin/image/frame_hide.gif" width="6"></td>
							<td class="Main_Bg" vAlign="top" height="100%"><iframe id="content" name="content" align="middle" src="WebQuery/ReportCenter.aspx" frameBorder="0"
									width="100%" scrolling="auto" height="100%" runat="server"></iframe>
							</td>
						</tr>
					</table>
				</td>
			</tr>
		</table>
		<script language="javascript">
		function LogoutCheck()
			{
				if(confirm("是否确认要退出本系统?"))return true ;
				else return false ;
			}
		</script>
		</form>
	</body>
</HTML>
