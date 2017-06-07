<%@ Page language="c#" Codebehind="FItem2DimentionMP.aspx.cs" AutoEventWireup="True" Inherits="BenQGuru.eMES.Web.MOModel.FItem2DimentionMP" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>FShiftMP</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href="<%=StyleSheet%>" rel=stylesheet>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table>
				<tr class="moduleTitle">
					<td><asp:label id="lblTitle" runat="server" CssClass="labeltopic"> 尺寸量测标准</asp:label></td>
				</tr>
				<tr height="30">
					<td>
						<table class="query" height="23" width="930" style="WIDTH: 930px; HEIGHT: 23px">
							<tr>
								<td class="fieldNameLeft" noWrap><asp:label id="lblItemCode" runat="server"> 产品代码</asp:label></td>
								<td class="fieldValue" style="WIDTH: 159px"><asp:textbox id="txtItemCodeQuery" runat="server" Width="150px" CssClass="textbox" ReadOnly="True"></asp:textbox></td>
								<td class="fieldName" noWrap><asp:label id="lblOrgEdit" runat="server"> 组织</asp:label></td>
								<td class="fieldValue" style="WIDTH: 159px"><asp:textbox id="TextboxOrg" runat="server" Width="150px" CssClass="textbox" ReadOnly="True"></asp:textbox></td>
                                <td class="fieldName" noWrap><asp:label id="lblOrgIDEdit" runat="server" Visible="false"> 组织编号</asp:label></td>
								<td class="fieldValue" style="WIDTH: 159px"><asp:textbox id="TextboxOrgID" runat="server" Width="150px" CssClass="textbox" ReadOnly="True" Visible="false"></asp:textbox></td>
								<td class="fieldName" noWrap><asp:label id="lblUnitQuery" runat="server"> 单位</asp:label></td>
								<td class="fieldValue" style="WIDTH: 159px"><asp:textbox id="txtUnit" runat="server" Width="150px" CssClass="textbox" ReadOnly="True">MM</asp:textbox></td>
								<td noWrap width="100%"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr height="180">
					<td><table class="query">
							<tr height="30">
								<td>
									<asp:Label id="lblLenght" runat="server">长度</asp:Label></td>
								<td>
									<asp:Label id="lblMin1" runat="server">MIN</asp:Label></td>
								<td>
									<asp:TextBox id="txtLengthMin" runat="server" CssClass="textbox"></asp:TextBox></td>
								<td>
									<asp:Label id="Label3" runat="server">MAX</asp:Label></td>
								<td>
									<asp:TextBox id="txtLengthMax" runat="server" CssClass="textbox"></asp:TextBox></td>
								<td>
									<asp:Label id="lblWidth" runat="server">宽度</asp:Label></td>
								<td><asp:Label id="Label5" runat="server">MIN</asp:Label></td>
								<td><asp:TextBox id="txtWidthMin" runat="server" CssClass="textbox"></asp:TextBox></td>
								<td>
									<asp:Label id="Label6" runat="server">MAX</asp:Label>
								</td>
								<td>
									<asp:TextBox id="txtWidthMax" runat="server" CssClass="textbox"></asp:TextBox></td>
							</tr>
							<tr height="30">
								<td>
									<asp:Label id="lblBoardHeight" runat="server">板上高</asp:Label></td>
								<td>
									<asp:Label id="Label8" runat="server">MIN</asp:Label></td>
								<td>
									<asp:TextBox id="txtBoardHeightMin" runat="server" CssClass="textbox"></asp:TextBox></td>
								<td>
									<asp:Label id="Label11" runat="server">MAX</asp:Label></td>
								<td>
									<asp:TextBox id="txtBoardHeightMax" runat="server" CssClass="textbox"></asp:TextBox></td>
								<td>
									<asp:Label id="lblBoardThick" runat="server">板厚</asp:Label></td>
								<td><asp:Label id="Label15" runat="server">MIN</asp:Label></td>
								<td>
									<asp:TextBox id="txtHeightMin" runat="server" CssClass="textbox"></asp:TextBox></td>
								<td>
									<asp:Label id="Label17" runat="server">MAX</asp:Label></td>
								<td>
									<asp:TextBox id="txtHeightMax" runat="server" CssClass="textbox"></asp:TextBox></td>
							</tr>
							<tr height="30">
								<td>
									<asp:Label id="lblAllHeight" runat="server">总高</asp:Label></td>
								<td>
									<asp:Label id="Label10" runat="server">MIN</asp:Label></td>
								<td>
									<asp:TextBox id="txtAllHeightMin" runat="server" CssClass="textbox"></asp:TextBox></td>
								<td>
									<asp:Label id="Label12" runat="server">MAX</asp:Label></td>
								<td>
									<asp:TextBox id="txtAllHeightMax" runat="server" CssClass="textbox"></asp:TextBox></td>
								<td>
									<asp:Label id="lblLeft2Right" runat="server">左孔到右孔</asp:Label></td>
								<td>
									<asp:Label id="Label16" runat="server">MIN</asp:Label></td>
								<td>
									<asp:TextBox id="txtLeft2RightMin" runat="server" CssClass="textbox"></asp:TextBox></td>
								<td>
									<asp:Label id="Label18" runat="server">MAX</asp:Label></td>
								<td>
									<asp:TextBox id="txtLeft2RightMax" runat="server" CssClass="textbox"></asp:TextBox></td>
							</tr>
							<tr height="30">
								<td>
									<asp:Label id="lblLeft2Middle" runat="server">左孔到中间孔</asp:Label></td>
								<td>
									<asp:Label id="lblMIN7" runat="server">MIN</asp:Label></td>
								<td>
									<asp:TextBox id="txtLeft2MiddleMin" runat="server" CssClass="textbox"></asp:TextBox></td>
								<td>
									<asp:Label id="Label23" runat="server">MAX</asp:Label></td>
								<td>
									<asp:TextBox id="txtLeft2MiddleMax" runat="server" CssClass="textbox"></asp:TextBox></td>
								<td></td>
								<td></td>
								<td></td>
								<td></td>
								<td></td>
							</tr>
							<tr height="30">
								<td>
									<asp:Label id="lblRight2Middle" runat="server">右孔到中间孔</asp:Label></td>
								<td>
									<asp:Label id="Label22" runat="server">MIN</asp:Label></td>
								<td>
									<asp:TextBox id="txtRight2MiddleMin" runat="server" CssClass="textbox"></asp:TextBox></td>
								<td>
									<asp:Label id="Label24" runat="server">MAX</asp:Label></td>
								<td>
									<asp:TextBox id="txtRight2MiddleMax" runat="server" CssClass="textbox"></asp:TextBox></td>
								<td></td>
								<td></td>
								<td></td>
								<td></td>
								<td></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr class="toolBar">
					<td>
						<table class="toolBar">
							<tr>
								<td class="toolBar"><INPUT class="submitImgButton" id="cmdSave" type="submit" value="保 存" name="cmdSave" runat="server" onserverclick="cmdSave_ServerClick"></td>
								<td class="toolBar"><INPUT class="submitImgButton" id="cmdReturn" type="submit" value="返 回" name="cmdReturn"
										runat="server" onserverclick="cmdReturn_ServerClick"></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
