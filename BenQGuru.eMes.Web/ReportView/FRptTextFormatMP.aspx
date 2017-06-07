<%@ Page language="c#" Codebehind="FRptTextFormatMP.aspx.cs" AutoEventWireup="True" Inherits="BenQGuru.eMES.Web.ReportView.FRptTextFormatMP" %>
<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="uc1" TagName="ReportSecurity" Src="ReportSecurity.ascx" %>
<%@ Register TagPrefix="uc1" TagName="colorPicker" Src="~/UserControl/ColorPicker/UCColorPicker.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>TextFormat</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="<%=StyleSheet%>" rel=stylesheet>
		<base target="_self"></base>
		<script language="javascript">
		    function ReturnAndClose()
		    {
		        var ret = "";
		        ret=document.getElementById("drpFontName").options[document.getElementById("drpFontName").selectedIndex].value;
		        ret=ret+";"+document.getElementById("txtFontSize").value;
		        ret=ret+";"+document.getElementById("chkFontWeight").checked;
		        ret=ret+";"+document.getElementById("chkFontItalic").checked;
		        ret=ret+";"+document.getElementById("chkFontDecoration").checked;
		        ret=ret+";"+document.getElementById("txtFontColor_hidColorPicker").value;
		        ret=ret+";"+document.getElementById("txtBackColor_hidColorPicker").value;
		        ret=ret+";"+document.getElementById("drpTextAlign").options[document.getElementById("drpTextAlign").selectedIndex].value;
		        ret=ret+";"+document.getElementById("drpVerticalAlign").options[document.getElementById("drpVerticalAlign").selectedIndex].value;
		        ret=ret+";"+document.getElementById("txtColumnWidth").value;
		        ret=ret+";"+document.getElementById("chkBorderVisible").checked;
		        ret=ret+";"+document.getElementById("txtTextFormat").value;
		        ret=ret+";"+document.getElementById("txtValueContent").value;
		        window.returnValue = ret;
		        window.close();
		    }
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout" scroll="yes">
		<form id="Form1" method="post" runat="server">
			<table height="100%" width="100%">
				<tr class="moduleTitle">
					<td><asp:label id="lblTextFormatTitle" runat="server" CssClass="labeltopic">文本格式设置</asp:label></td>
				</tr>
				<tr>
					<td>
						<table class="query" height="100%" width="100%">
							<tr>
								<td noWrap width="100%"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr height="100%">
					<td class="fieldGrid" valign="top">
					    <table>
					        <tr>
					            <td class="fieldNameLeft" noWrap><asp:Label ID="lblFontName" runat="server">字体名称</asp:Label></td>
					            <td class="fieldValue"><asp:DropDownList runat="server" ID="drpFontName"></asp:DropDownList></td>
					        </tr>
					        <tr>
					            <td class="fieldNameLeft" nowrap><asp:Label runat="server" ID="lblFontSize">字体大小</asp:Label></td>
					            <td class="fieldValue"><asp:TextBox runat="server" ID="txtFontSize" CssClass="textbox" Width="50px"></asp:TextBox>pt</td>
					        </tr>
					        <tr>
					            <td class="fieldNameLeft" nowrap><asp:Label runat="server" ID="lblFontStyle">字体样式</asp:Label></td>
					            <td class="fieldValue">
					                <asp:CheckBox runat="server" ID="chkFontWeight" Text="粗体" />
					                <br />
					                <asp:CheckBox runat="server" ID="chkFontItalic" Text="斜体" />
					                <br />
					                <asp:CheckBox runat="server" ID="chkFontDecoration" Text="下划线" />
					            </td>
					        </tr>
					        <tr>
					            <td class="fieldNameLeft" nowrap><asp:Label runat="server" ID="lblFontColor">字体颜色</asp:Label></td>
					            <td class="fieldValue"><uc1:colorPicker id="txtFontColor" runat="server" CssClass="textbox" width="130"></uc1:colorPicker></td>
					        </tr>
					        <tr>
					            <td class="fieldNameLeft" nowrap><asp:Label runat="server" ID="lblBackColor">背景色</asp:Label></td>
					            <td class="fieldValue"><uc1:colorPicker id="txtBackColor" runat="server" CssClass="textbox" width="130"></uc1:colorPicker></td>
					        </tr>
					        <tr>
					            <td class="fieldNameLeft" noWrap><asp:Label ID="lblTextAlign" runat="server">横向对齐</asp:Label></td>
					            <td class="fieldValue"><asp:DropDownList runat="server" ID="drpTextAlign"></asp:DropDownList></td>
					        </tr>
					        <tr>
					            <td class="fieldNameLeft" noWrap><asp:Label ID="lblVerticalAlign" runat="server">纵向对齐</asp:Label></td>
					            <td class="fieldValue"><asp:DropDownList runat="server" ID="drpVerticalAlign"></asp:DropDownList></td>
					        </tr>
					        <tr>
					            <td class="fieldNameLeft" nowrap><asp:Label runat="server" ID="lblColumnWidth">列宽</asp:Label></td>
					            <td class="fieldValue"><asp:TextBox runat="server" ID="txtColumnWidth" CssClass="textbox" Width="50px" Text="2.5"></asp:TextBox>cm</td>
					        </tr>
					        <tr>
					            <td class="fieldNameLeft" nowrap><asp:Label runat="server" ID="lblBorderVisible">边框</asp:Label></td>
					            <td class="fieldValue"><asp:CheckBox runat="server" ID="chkBorderVisible" Checked="true" /></td>
					        </tr>
					        <tr>
					            <td class="fieldNameLeft" nowrap><asp:Label runat="server" ID="lblTextFormat">文本显示格式</asp:Label></td>
					            <td class="fieldValue"><asp:TextBox runat="server" ID="txtTextFormat" CssClass="textbox" Width="100px"></asp:TextBox></td>
					        </tr>
					        <tr>
					            <td class="fieldNameLeft" nowrap><asp:Label runat="server" ID="lblValueContent">显示内容</asp:Label></td>
					            <td class="fieldValue"><asp:TextBox runat="server" ID="txtValueContent" CssClass="textbox" Width="200px" TextMode="multiLine" Height="70px"></asp:TextBox></td>
					        </tr>
					        <tr>
					            <td height="100%">&nbsp;</td>
					        </tr>
					    </table>
                    </td>
				</tr>
				<tr class="normal">
				    <td>
				        <table class="edit" height="100%" cellPadding="0" width="100%">
				            <tr>
				                <td></td>
				            </tr>
				        </table>
				    </td>
				</tr>
				<tr class="toolBar">
					<td>
						<table class="toolBar">
							<tr>
								<td class="toolBar"><INPUT class="submitImgButton" id="cmdSave" type="submit" value="保 存" name="cmdSave" runat="server" onclick="ReturnAndClose();return false;"></td>
								<td class="toolBar"><INPUT class="submitImgButton" id="cmdDeleteItem" type="submit" value="删 除" name="cmdDeleteItem" runat="server" onclick="window.returnValue='delete';window.close();return false;"></td>
								<td><INPUT class="submitImgButton" id="cmdCancel" type="submit" value="取 消" name="cmdCancel"
										runat="server" onclick="window.close();return false;"></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
