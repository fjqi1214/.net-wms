<%@ Page language="c#" Codebehind="reportMenu.aspx.cs" AutoEventWireup="True" Inherits="BenQuru.eMES.Web.reportMenu" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>reportMenu</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="<%=StyleSheet%>" type=text/css rel=stylesheet>
		<script language="javascript">
			function LogoutCheck()
			{
				if(confirm("是否确认要退出本系统?"))
				{
					window.top.location = "FLoginNew.aspx";
					return true; 
				}
				else
				{
				  return false; 
				}
			}
			function menuAction()
			{
				if(document.getElementById('imgIOC').alt == '显示')
				{
					showtoc();
				
					document.getElementById('imgIOC').src = 'skin/image/frame_hide.gif';
					document.getElementById('imgIOC').alt = '隐藏';
					
				}
				else
				{
					hidetoc();
				
					document.getElementById('imgIOC').src = 'skin/image/frame_show.gif';
					document.getElementById('imgIOC').alt = '显示';
					
					
				}
			}
			function hidetoc()
			{
				//fstMain_cols = top.fstMain.cols;
				
				top.fstMain.cols = "22,*";
				
				top.MNPMenuFrame.divMenu.style.display = "none";
				
			}
			function showtoc()
			{
				top.MNPMenuFrame.divMenu.style.display = "";
				//if (fstMain_cols)
				//	top.fstMain.cols = fstMain_cols;
				//else
				top.fstMain.cols = "210,*";
				
			}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="frmLeft" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="210" border="0">
				<tr>
					<td vAlign="top" height="100%">
						<div id="divMenu">
							<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
								<tr>
									<td align="center" width="210" background="skin/image/bg_head.gif"><A href="WebQuery/ReportCenter.aspx" target="content"><IMG src="skin/image/ico_home.gif" width="70" border="0"></A>&nbsp;&nbsp;
										<asp:imagebutton id="icmdLogout" runat="server" BackColor="Transparent" ImageUrl="skin/image/ico_exit.gif"
											AlternateText="Logout"></asp:imagebutton></td>
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
											<tr>
												<td><IMG height="9" src="skin/image/ico_arrow0.gif" width="9">&nbsp;<A href="WebQuery/FReportFactoryWeekCheck.aspx" target="content"><asp:label id="lblReportFactoryWeekCheckTitle" runat="server">外包厂检验周报</asp:label></A></td>
											</tr>
										</table>
									</td>
								</tr>
							</table>
						</div>
					</td>
					<td width="6" background="skin/image/frame_bg.gif" height="100%"><IMG id="imgIOC" style="CURSOR: hand" onclick="javascript:menuAction();" height="41"
							alt="隐藏" src="skin/image/frame_hide.gif" width="6"></td>
					<td class="Main_Bg" vAlign="top" height="100%">
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
