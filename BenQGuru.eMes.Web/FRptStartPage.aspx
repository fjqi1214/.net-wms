<%@ Register TagPrefix="ignav" Namespace="Infragistics.WebUI.UltraWebNavigator" Assembly="Infragistics35.WebUI.UltraWebNavigator.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Page language="c#" Codebehind="FRptStartPage.aspx.cs" AutoEventWireup="True" Inherits="BenQGuru.eMES.Web.FRptStartPage" %>
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
		<link href="<%=StyleSheet%>" rel=stylesheet>
		<style type="text/css">
			.Header {
				background-image: url(Skin/Image/banner_middle.gif);
				background-repeat: repeat-x;
				}
			.Header_Banner {
				background-image:url(Skin/Image/banner_left.gif);
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
				}catch(e) {return false;}
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
				}catch(e) {return false;}	
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
				}catch(e) {return false;}
			}
			
			function LogoutCheck()
			{
				if(confirm("是否确认要退出本系统?"))return true ;
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
			function toolbarReport_onclick()
			{
				window.document.location.href = "FReportMain.aspx";
			}

//-->
		</script>
	</HEAD>
	<body onmousemove="BodyMouseMove()" bottomMargin="0" leftMargin="0" topMargin="0" scroll="no"
		rightMargin="0" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="TableMain" style="WIDTH: 100%; HEIGHT: 100%" cellSpacing="0" cellPadding="0">
				<TR>
					<TD style="HEIGHT: 20px; vertical-align:top"> <ignav:UltraWebMenu id="mainMenu" runat="server" CssClass="Control" Width="100%" BorderStyle="NotSet" TopAligment="Center" ItemPaddingTop="1">
					                    <IslandStyle CssClass="Island" Font-Size="9pt"></IslandStyle>
					                    <HoverItemStyle CssClass="Hover"></HoverItemStyle>
					                    <TopLevelHoverItemStyle CssClass="TopLevelHover"></TopLevelHoverItemStyle>
					                    <TopLevelLeafItemStyle CssClass="TopLevel"></TopLevelLeafItemStyle>
					                    <TopLevelParentItemStyle CssClass="TopLevel"></TopLevelParentItemStyle>
					                    <ItemStyle Cursor="Hand" />
					                    <Levels>
					                        <ignav:Level Index="0" />
					                        <ignav:Level Index="1" LevelClass="SubLevel" LevelHoverClass="SubLevelHover"/>
					                        <ignav:Level Index="2" LevelClass="SubLevel" LevelHoverClass="SubLevelHover"/>
					                        <ignav:Level Index="3" LevelClass="SubLevel" LevelHoverClass="SubLevelHover"/>
					                    </Levels>
					                </ignav:UltraWebMenu></TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 40px">
						<TABLE id="Table2" style="BACKGROUND-IMAGE: url(./skin/image/Index_eMES_menu_down.gif); background-repeat:repeat-x; WIDTH: 100%; HEIGHT: 100%"
							cellSpacing="0" cellPadding="0" border="0">
							<TR>
								<TD style="PADDING-LEFT: 8px" width="95%"><iframe id="frmNav" style="WIDTH: 100%; BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; HEIGHT: 100%; BACKGROUND-COLOR: transparent; BORDER-BOTTOM-STYLE: none"
										name="frmNav" src="FPageNavigator.aspx" frameBorder="no" width="100%" scrolling="no" height="100%"></iframe>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD style="WIDTH: 100%; HEIGHT: 100%">
						<TABLE id="Table1" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td><iframe id="frmWorkSpace" onmouseover="iframeMouseOver()" style="PADDING-RIGHT: 8px; PADDING-LEFT: 8px; WIDTH: 100%; BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; HEIGHT: 100%; BORDER-BOTTOM-STYLE: none"
										onfocus="FunIfrmFocus()" name="frmWorkSpace" align="middle" marginWidth="0" marginHeight="0"
										src="" frameBorder="no" scrolling="no"></iframe>
								</td>
							</tr>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
