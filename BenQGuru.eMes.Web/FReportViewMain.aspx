<%@ Page Language="c#" Codebehind="FReportViewMain.aspx.cs" AutoEventWireup="True"
    Inherits="BenQGuru.eMES.Web.FReportViewMain" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>
        <%=Title%>
    </title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="<%=StyleSheet%>" type="text/css" rel="stylesheet">
    <style type="text/css">
			.Header {
				background-image: url(Skin/Image/Index_eMES_banner_middle.gif);
				background-repeat: repeat-x;
				}
			.Header_Banner {
				background-image:url(Skin/Image/Index_eMES_banner.gif);
				background-repeat:no-repeat;
				background-position: left;
				height: 53px;
				width: 100%;
				}
			.Header_Banner_Width {
				width:250px;
				}
				
			.Control
			{
				background-color:#E3EFFF;
                background-image: url(Skin/Image/index_emes_menu_unselect.gif);
                background-repeat: repeat-x;
                color: White;
                font-size: 8pt;
			}
			.Island
			{
				background-color:#E3EFFF;
                background-image: url(Skin/Image/index_emes_submenu_unselcet.gif);
                background-repeat:repeat;
                border:solid 1px #90DADF;
                color:#006B72;
			}
			.Hover
			{
				background-image: url(Skin/Image/Index_eMES_submenu_selcet.gif);
                border:solid 1px Transparent;
                cursor:hand;
                color:White;
			}
			.TopLevelHover
			{
				background-position:center ;
                background-image: url(Skin/Image/Index_eMES_menu_select.gif);
                border:solid 1px Transparent;
			}
			.Item
			{
				background-color:Transparent;
                background-image:none;
                border:solid 1px Transparent;
                padding-top:5px;
                padding-bottom:5px;
                cursor:hand;
                font-size:8pt;
			}
			.TopLevel
			{
				color: White;
                font-size: 8pt;
                font-weight:bold;
			}
			.SubLevel
			{
				color:#006B72;
                font-size: 8pt;
			}
			.SubLevelHover
			{
				background-image: url(Skin/Image/Index_eMES_submenu_selcet.gif);
                border:solid 1px Transparent;
                color:White;
			}
			
		</style>
    <%--<style type="text/css">
			.Header { BACKGROUND-IMAGE: url(Skin/Image/banner_middle.gif); BACKGROUND-REPEAT: repeat-x }
			.Header_Banner { BACKGROUND-POSITION: left 50%; BACKGROUND-IMAGE: url(Skin/Image/banner_left.gif); WIDTH: 100%; BACKGROUND-REPEAT: no-repeat; HEIGHT: 53px }
			.Header_Banner_Width { WIDTH: 250px }
		</style>--%>

    <script language="javascript">
			function menuAction()
			{
				if(document.getElementById('imgIOC').alt == '显示')
				{
					document.getElementById('imgIOC').src = 'skin/image/frame_hide.gif';
					document.getElementById('imgIOC').alt = '隐藏';
					document.getElementById('tdLeftNav').style.visibility="visible";
					document.getElementById('tdLeftNav').style.display="";
					//document.getElementById('content').src="FReportViewMain.aspx";
				}
				else
				{
					document.getElementById('imgIOC').src = 'skin/image/frame_show.gif';
					document.getElementById('imgIOC').alt = '显示';
					document.getElementById('tdLeftNav').style.visibility="hidden";
					document.getElementById('tdLeftNav').style.display="none";
					//document.getElementById('content').src="FReportViewMain.aspx";
				}
			}
    </script>

<script language="javascript">		    
		   	function FunIfrmFocus()
			{
				try
				{
					var objTags=document.frames["frmWorkSpace"].document.body.getElementsByTagName("select");
					for (var intNum=0;intNum<objTags.length;intNum++)
					{
						objTags[intNum].style.visibility="visible";						
					}
					document.getElementById("frmWorkSpace").blur(); 									
				}catch(e) {return false;}
			}
			
			function iframeMouseOver()
			{
				try
				{
					var objTags=document.frames["frmWorkSpace"].document.body.getElementsByTagName("select");
					for (var intNum=0;intNum<objTags.length;intNum++)
					{
						objTags[intNum].style.visibility="visible";						
					}
					document.getElementById("frmWorkSpace").blur(); 	
					
					if (document.getElementById('frmWorkSpace').src == 'FStartPageBlank.aspx')
					{
					    document.getElementById('frmWorkSpace').src='FIframePage.aspx';	
					    document.getElementById('frmWorkSpace').style.scrolling='yes';				    
					}								
				}
				catch(e) {return false;}
			}
			
			function BodyMouseMove()
			{
				try
				{
					var objSrc=event.srcElement;
					if(objSrc.tagName=="IFRAME")		
					{
						FunIfrmFocus();	
					}
					document.getElementById("frmWorkSpace").blur(); 					

				}
				catch(e) {return false;}	
			}
			
			function SubMenuDisplay(mn, id, bShow) 
			{
				try
				{
					var objTags=document.frames["frmWorkSpace"].document.body.getElementsByTagName("select");
					for (var intNum=0;intNum<objTags.length;intNum++)
					{
						//objTags[intNum].style.visibility="hidden";						
					}
					document.getElementById("frmWorkSpace").blur(); 	
					
					if (document.getElementById('frmWorkSpace').src == 'FIframePage.aspx')
					{
					    document.getElementById('frmWorkSpace').src='FStartPageBlank.aspx';		
					    document.getElementById('frmWorkSpace').style.scrolling='no';				    
					}
				}
				catch(e) {return false;}
			}
			
			function LogoutCheck()
			{	  
				var alterString=document.getElementById("lblLogout").value;
				if(confirm(alterString))return true ;
				else return false ;
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
			
			//报表中心
			function toolbarReport_onclick()
			{
				window.document.location.href = "FReportMain.aspx";
			}
			
			// 设置配置
			function ConfigReportView()
			{
				var result = window.showModalDialog("WebQuery/ReportCenterViewConfigEP.aspx", "" ,"dialogWidth:800px;dialogHeight:600px;scroll:none;help:no;status:no") ;
				if (result == "OK")
				{
					document.getElementById('content').src="FRptStartPage.aspx";
				}
			}
			
			 function LinkOrgList_Click()
			{		    
			    var newWindow = "./FOrgChangePage.aspx";
			    
			    //var result = window.open(newWindow);
			    var result = window.showModalDialog(newWindow,"",showDialog(6));
                //alert(result);
                document.getElementById("LinkOrgList").innerText = result;
                document.frames["frmWorkSpace"].location.replace(document.frames["frmWorkSpace"].location.href);
			}
//-->
    </script>

    <script language="javascript">
            function InitReportList(folderName, rptListName, rptListId)
            {
                var tbList = document.getElementById("tbRptList");
                var trFolder = document.createElement("tr");
                var tdFolder = document.createElement("td");
                tdFolder.className = "ModuleMenu_bg";
                var trContent = "\r\n";
                trContent = trContent + "<table cellSpacing=\"0\" cellPadding=\"0\" width=\"83%\" align=\"right\" border=\"0\">\r\n";
                trContent = trContent + "<tr>\r\n";
                trContent = trContent + "<td width=\"18\"><IMG height=\"16\" src=\"skin/image/ico_menu.gif\" width=\"14\"></td>\r\n";
                trContent = trContent + "<td class=\"ModuleMenu_txt\"><span id=\"lblQuantity\">" + folderName + "</span></td>\r\n";
                trContent = trContent + "</tr>\r\n";
                trContent = trContent + "</table>\r\n";
                tdFolder.innerHTML = trContent;
                trFolder.appendChild(tdFolder);
                tbList.childNodes[0].insertBefore(trFolder, tbList.childNodes[0].childNodes[tbList.childNodes[0].childNodes.length - 1]);
                
                if (rptListName == undefined || 
                    rptListName == null || 
                    rptListName.length == 0)
                {
                    return;
                }
                var rptContent = "<table cellSpacing=\"5\" cellPadding=\"0\" width=\"75%\" align=\"right\" border=\"0\">\r\n";
                for (var i = 0; i < rptListName.length; i++)
                {
                    var rpt1 = "<tr>\r\n";
                    rpt1 = rpt1 + "<td><IMG height=\"9\" src=\"skin/image/ico_arrow0.gif\" width=\"9\">&nbsp;<A href=\"ReportView/FRptViewMP.aspx?reportid=" + rptListId[i] + "\" target=\"content\"><span>" + rptListName[i] + "</span></A></td>\r\n";
                    rpt1 = rpt1 + "</tr>";
                    rptContent = rptContent + rpt1 + "\r\n";
                }
                var trRpt = document.createElement("tr");
                var tdRpt = document.createElement("td");
                tdRpt.innerHTML = rptContent;
                trRpt.appendChild(tdRpt);
                tbList.childNodes[0].insertBefore(trRpt, tbList.childNodes[0].childNodes[tbList.childNodes[0].childNodes.length - 1]);
            }
    </script>

</head>
<body onmousemove="BodyMouseMove()" bottomMargin="0" leftMargin="0" topMargin="0" 
		rightMargin="0" MS_POSITIONING="GridLayout" scroll="no">
    <form id="Form1" method="post" runat="server">
        <%--<table height="100%" cellspacing="0" cellpadding="0" width="100%" border="0">--%>
        <table id="TableMain" style="WIDTH: 100%; HEIGHT: 100%" cellSpacing="0" cellPadding="0" style="background-color:#10929C;">
            <tr>
               
                <td>
			            <table width="100%" cellpadding="0" cellspacing="0" border="0">
			                <tr>
			                    <td style="width:340px; height:50px;">
			                        <img src="Skin/Image/Index_eMES_banner.gif" width="340" height="50" alt="" runat="server"  id="ImageHead" />
			                    </td>
			                    <td class="Header" align="right">
			                        <table width="100%" cellSpacing="0" cellPadding="0" border="0">
										<tr>
										 <td align="center" >
										 <table>
										 <tr>
										    <td align> 										 
										        <asp:label id="lblDepartmentName" runat="server" CssClass="welcome" Style="color: #FFFFFF"></asp:label>&nbsp;&nbsp;
												<asp:label id="lblUserName" runat="server" CssClass="welcome" Style="color: #FFFFFF">Customer</asp:label>&nbsp;&nbsp;&nbsp;&nbsp;
										    </td>
										    <TD noWrap>	
												<asp:label id="lblWelcome" runat="server" CssClass="welcome" Style="color: #FFFFFF"> 欢迎使用本系统</asp:label>
												<asp:Label id="lblOrganization" runat="server" CssClass="welcome" Style="color: #FFFFFF"> 您的组织为:</asp:Label>
												<a runat="server" href="" style="text-decoration:underline;cursor:hand;color: #FFFFFF" Class="welcome" onclick="return LinkOrgList_Click();" id="LinkOrgList" target=_parent>orgnization</a>
												<iframe id="iframeDownload" name="iframeDownload" width="0" height="0"></iframe>
											</TD>
											</tr>
										</table>
									    </td>
										<td>
										    <td width="50"><a href="#" style="color: White" onclick="location.href='FStartPage.aspx';"><asp:Label runat="server" ID="lblWorkCenter" Text="作业中心"></asp:Label></a></td>
											<td width="8" style="color: White">&nbsp;&nbsp;|&nbsp;&nbsp;</td>
											<%--<td width="50"><a href="#" style="color: White" onclick="toolbarReportView_onclick();return false;">报表平台</a></td>
											<td width="8" style="color: White">&nbsp;&nbsp;|&nbsp;&nbsp;</td>--%>
											<td width="50"><a href="#" style="color: White" onclick="toolbarPassword_onclick();return false;"><asp:Label runat="server" ID="lblChangePWD" Text="更改密码"></asp:Label></a></td>
											<td width="8" style="color: White">&nbsp;&nbsp;|&nbsp;&nbsp;</td>
											<td width="30"> <a href="#" style="color: White"><asp:Label runat="server" ID="lblHelp" Text="帮助"></asp:Label></a></td>
											<td width="8" style="color: White">&nbsp;&nbsp;|&nbsp;&nbsp;</td>
											<td width="30"><asp:LinkButton ID="lnkButtonLogout" style="color: White" runat="server" OnClientClick="return LogoutCheck();" Text="登出" onclick="lnkButtonLogout_Click"></asp:LinkButton>
											</td>
											<td width="8"><asp:TextBox ID="lblLogout" runat="server" Width="8" style="display:none"></asp:TextBox></td>
									   </td>
										</tr>
									</table>
			                    </td>
			                    <td style="width:90px; height:50px">
			                        <img src="Skin/Image/Index_eMES_logo.gif" width="90" height="50" alt=""  runat="server" id="ImageLogo"/>
			                    </td>
			                </tr>
			            </table>
			        </td>
            </tr>
            <tr>
                <td valign="top" height="100%">
                    <table height="100%" cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td width="190" bgcolor="#f6f6f6" height="100%" id="tdLeftNav">
                                <table height="100%" cellspacing="0" cellpadding="0" width="100%" border="0">
                                    <tr>
                                        <td width="190" bgcolor="#f6f6f6" height="100%">
                                            <div style="height: 100%; overflow-y: auto;">
                                                <table height="100%" cellspacing="0" cellpadding="0" width="100%" border="0" id="tbRptList">
                                                    <tr>
                                                        <td align="center" width="190" background="skin/image/bg_head.gif" height="41">
                                                            <a href="FRptStartPage.aspx" target="content">
                                                                <img height="23" src="skin/image/ico_home.gif" width="70" border="0" id="imgHome" runat="server"></a>&nbsp;&nbsp;
                                                            <input id="imgLogout" runat="server" type="image" src="skin/image/ico_exit.gif" name="imageField2"></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="ModuleMenu_bg" valign="top">
                                                            <table cellspacing="0" cellpadding="0" width="83%" align="right" border="0">
                                                                <tr>
                                                                    <td width="18">
                                                                        <img height="16" src="skin/image/ico_menu.gif" width="14"></td>
                                                                    <td class="ModuleMenu_txt">
                                                                        <a href="FRptStartPage.aspx" target="content">
                                                                            <asp:Label ID="lblReportDesign" runat="server">报表维护 >></asp:Label></a></td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td height="100%">
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="6" background="skin/image/frame_bg.gif" height="100%">
                                <img id="imgIOC" style="cursor: hand" onclick="javascript:menuAction();" height="41"
                                    alt="隐藏" src="skin/image/frame_hide.gif" width="6"></td>
                           <%-- <td class="Main_Bg" valign="top" height="100%">
                                <iframe id="content" name="content" align="middle" src="FRptStartPage.aspx" frameborder="0"
                                    width="100%" scrolling="auto" height="100%" runat="server"></iframe>
                            </td>--%>
                            <td valign="top" height:100px; width:100%" >
                                <iframe id="content" name="content" align="middle" src="FRptStartPage.aspx" frameborder="0"
                                    width="100%" scrolling="auto" height="100%" runat="server"></iframe>
                            </td>        
                        </tr>
                    </table>
                </td>
            </tr>
        </table>

        <%--<script language="javascript">
		function LogoutCheck()
			{
				if(confirm("是否确认要退出本系统?"))return true ;
				else return false ;
			}
        </script>--%>

    </form>
</body>
</html>
