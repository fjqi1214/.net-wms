<%@ Page language="c#" Codebehind="ReportCenterViewConfigEP.aspx.cs" EnableEventValidation="false" AutoEventWireup="True" Inherits="BenQGuru.eMES.Web.WebQuery.ReportCenterViewConfigEP" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="uc1" TagName="eMESDate" Src="~/UserControl/DateTime/DateTime/eMESDate.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title></title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<base target="_self">
		<link href="<%=StyleSheet%>" rel=stylesheet>
		<script language="javascript">
			function ItemSelect()
			{
				return MoveItem("lstUnSelected", "lstSelected");
			}
			function ItemUnSelect()
			{
				return MoveItem("lstSelected", "lstUnSelected");
			}
			function ItemMoveUp()
			{
				var objsel = document.getElementById("lstSelected");
				var idxSel = -1;
				for(var i = 1; i <= objsel.options.length - 1; i++)
				{
					if(objsel.options[i].selected)
					{
						SwithItem(objsel, i, i - 1);
						idxSel = i - 1;
					}
				}
				ReBuildSelectItem();
				if (idxSel >= 0)
				{
					objsel.selectedIndex = idxSel;
				}
			}
			function ItemMoveDown()
			{
				var objsel = document.getElementById("lstSelected");
				var idxSel = -1;
				for(var i = objsel.options.length - 2; i >= 0; i--)
				{
					if(objsel.options[i].selected)
					{
						SwithItem(objsel, i, i + 1);
						idxSel = i + 1;
					}
				}
				ReBuildSelectItem();
				if (idxSel >= 0)
				{
					objsel.selectedIndex = idxSel;
				}
			}
			function ReBuildSelectItem()
			{
				var txt = document.getElementById("txtSelected");
				txt.value = ";";
				var objsel = document.getElementById("lstSelected");
				for(var i = 0; i <= objsel.options.length - 1; i++)
				{
					txt.value = txt.value + objsel.options[i].value + ";";
				}
			}
			function SwithItem(objsel, idx1, idx2)
			{
				var itxt = objsel.options[idx1].text;
				var ival = objsel.options[idx1].value;
				objsel.options[idx1].text = objsel.options[idx2].text;
				objsel.options[idx1].value = objsel.options[idx2].value;
				objsel.options[idx2].text = itxt;
				objsel.options[idx2].value = ival;
			}
			function MoveItem(fromItem, toItem)
			{
				var objres = document.getElementById(fromItem);
				var objsel = document.getElementById(toItem);
				var customOptions;
				for(var i = 0; i <= objres.options.length - 1; i++)
				{
					if(objres.options[i].selected)
					{
						customOptions = document.createElement("OPTION");
						customOptions.text = objres.options[i].text;
						customOptions.value = objres.options[i].value;
						objsel.add(customOptions);
					}
				}
				for(var i = objres.options.length - 1; i >= 0; i--)
				{
					if(objres.options[i].selected)
					{
						objres.remove(i);
					}
				}
				ReBuildSelectItem();
				return false;
			}
			function CheckSave()
			{
				var objsel = document.getElementById("lstSelected");
				if (objsel.options.length == 0)
					return false;
				return true;
			}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table height="100%" width="100%">
				<tr class="moduleTitle">
					<td><asp:label id="lblReportCenterViewConfigEPTitle" runat="server" CssClass="labeltopic"> 报表显示配置</asp:label></td>
				</tr>
				<tr height="100%">
					<td class="fieldGrid">
						<table width="100%" height="100%">
							<tr>
								<td width="50%" height="100%" valign="top">
									<asp:ListBox Runat="server" ID="lstUnSelected" SelectionMode="Multiple" Height="100%" Width="100%"></asp:ListBox>
								</td>
								<td valign="middle">
									<img src="../Skin/Image/right_0.gif" border="0" onclick="ItemSelect();"><br>
									<img src="../Skin/Image/left_0.gif" border="0" onclick="ItemUnSelect();"><br>
									<input type="hidden" id="txtSelected" runat="server" style="width:1px">
								</td>
								<td width="50%" height="100%" valign="top">
									<table width="100%" height="100%">
										<tr>
											<td width="100%" height="100%" valign="top">
												<asp:ListBox Runat="server" ID="lstSelected" SelectionMode="Multiple" Height="100%" Width="100%"></asp:ListBox>
											</td>
											<td height="100%" valign="middle">
												<img src="../Skin/Image/import export2.gif" border="0" onclick="ItemMoveUp();"><br>
												<img src="../Skin/Image/import export4.gif" border="0" onclick="ItemMoveDown();">
											</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr class="toolBar">
					<td>
						<table class="toolBar">
							<tr>
								<td class="toolBar"><INPUT class="submitImgButton" id="cmdSave" type="submit" value="保 存" name="cmdSave" runat="server" onserverclick="cmdSave_ServerClick"></td>
								<td><INPUT class="submitImgButton" id="cmdCancel" type="submit" value="取 消" name="cmdCancel"
										runat="server" onclick="window.close(); return false;"></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
