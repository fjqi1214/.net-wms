<%@ Page Language="c#" CodeBehind="FParameterMP.aspx.cs" AutoEventWireup="True" Inherits="BenQGuru.eMES.Web.BaseSetting.FParameterMP" %>

<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>FParameterMP</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="<%=StyleSheet%>" rel="stylesheet">
    <script language="javascript">
        function onCheckBoxChange(chk) {
            if (chk.checked == true) {
                document.getElementById("txtParentParameterQuery").disabled = "";
            }
            else {
                document.getElementById("txtParentParameterQuery").disabled = "disabled";
                document.getElementById("txtParentParameterQuery").value = "";
            }
        }
    </script>
</head>
<body>
    <form id="Form1" method="post" runat="server">
    <table height="100%" width="100%" runat="server">
        <tr class="moduleTitle">
            <td>
                <asp:Label ID="lblTitle" runat="server" CssClass="labeltopic"> 参数维护</asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table class="query" height="100%" width="100%">
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblParameterCodeQuery" runat="server"> 参数代码</asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 100px">
                            <asp:TextBox ID="txtParameterCodeQuery" runat="server" Width="100px" CssClass="textbox"></asp:TextBox>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblParameterGroupCodeQuery" runat="server"> 参数组代码</asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 159px">
                            <asp:DropDownList ID="drpParameterGroupCodeQuery" runat="server" Width="150px">
                            </asp:DropDownList>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:CheckBox runat="server" ID="chkQueryParentParameterAll" Text="父参数"></asp:CheckBox>
                        </td>
                        <td class="fieldValue" style="width: 100px">
                            <asp:TextBox ID="txtParentParameterQuery" runat="server" Width="100px" CssClass="textbox"></asp:TextBox>
                        </td>
                        <td class="fieldValue" nowrap>
                        </td>
                        <td nowrap width="100%">
                        </td>
                        <td class="fieldName">
                            <input class="submitImgButton" id="cmdQuery" type="submit" value="查 询" name="btnQuery"
                                runat="server">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr height="100%">
            <td class="fieldGrid">
                <ig:webdatagrid id="gridWebGrid" runat="server" width="100%">
                </ig:webdatagrid>
            </td>
        </tr>
        <tr class="normal">
            <td>
                <table height="100%" cellpadding="0" width="100%">
                    <tr>
                        
                        <td class="smallImgButton">
                            <input class="gridExportButton" id="cmdGridExport" type="submit" value="  " name="cmdGridExport"
                                runat="server">
                            
                        </td>
                        <td class="smallImgButton">
                            <input class="deleteButton" id="cmdDelete" type="submit" value="  " name="cmdDelete"
                                runat="server">
                            
                        </td>
                        <td>
                            <cc1:PagerSizeSelector ID="pagerSizeSelector" runat="server"></cc1:PagerSizeSelector>
                        </td>
                        <td align="right">
                            <cc1:PagerToolBar ID="pagerToolBar" runat="server"></cc1:PagerToolBar>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="normal">
            <td>
                <table class="edit" height="100%" cellpadding="0" width="100%">
                    <tr>
                        <td class="fieldNameLeft" style="height: 26px" nowrap>
                            <asp:Label ID="lblParameterCodeEdit" runat="server">参数代码</asp:Label>
                        </td>
                        <td class="fieldValue" style="height: 26px">
                            <asp:TextBox ID="txtParameterCodeEdit" runat="server" CssClass="require" Width="150px"></asp:TextBox>
                        </td>
                        <td class="fieldName" style="height: 26px" nowrap>
                            <asp:Label ID="lblParameterAliasEdit" runat="server">参数别名</asp:Label>
                        </td>
                        <td class="fieldValue" style="height: 26px">
                            <asp:TextBox ID="txtParameterAliasEdit" runat="server" Width="150px" CssClass="textbox"></asp:TextBox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblParameterGroupCodeEdit" runat="server">参数组代码</asp:Label>
                        </td>
                        <td width="100%" class="fieldValue">
                            <asp:DropDownList ID="drpParameterGroupCodeEdit" runat="server" CssClass="require"
                                Width="150px" AutoPostBack="True" OnSelectedIndexChanged="drpParameterGroupCodeEdit_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldNameLeft" style="height: 26px" nowrap>
                            <asp:Label ID="lblParatmerValue" runat="server">参数系统值</asp:Label>
                        </td>
                        <td class="fieldValue" style="height: 26px">
                            <asp:DropDownList ID="drpParamterValueEdit" runat="server" Width="150px">
                            </asp:DropDownList>
                        </td>
                        <td class="fieldName" style="height: 26px" nowrap>
                            <asp:Label ID="lblParameterDescriptionEdit" runat="server">参数描述</asp:Label>
                        </td>
                        <td class="fieldValue" style="height: 26px">
                            <asp:TextBox ID="txtParameterDescriptionEdit" runat="server" Width="150px" CssClass="textbox"></asp:TextBox>
                        </td>
                        <td class="fieldName" style="height: 26px" nowrap>
                            <asp:Label ID="lblParameterSeq" runat="server">参数顺序</asp:Label>
                        </td>
                        <td class="fieldValue" style="height: 26px">
                            <asp:TextBox ID="txtParameterSeq" runat="server" Width="150px" CssClass="textbox"></asp:TextBox>
                        </td>
                        <td class="fieldValue" style="display: none; height: 26px" nowrap>
                            <asp:CheckBox ID="chbIsActiveEdit" runat="server" Text="为可用参数"></asp:CheckBox>&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:CheckBox ID="chbIsSystemEdit" runat="server" Text="为系统参数" Enabled="False"></asp:CheckBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldNameLeft" style="height: 26px" nowrap>
                            <asp:Label ID="lblParentParameter" runat="server">父参数</asp:Label>
                        </td>
                        <td class="fieldValue" style="height: 26px">
                            <asp:DropDownList ID="drpParentParameter" runat="server" Width="150px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="toolBar">
            <td>
                <table class="toolBar">
                    <tr>
                        <td class="toolBar">
                            <input class="submitImgButton" id="cmdAdd" type="submit" value="新 增" name="cmdAdd"
                                runat="server">
                        </td>
                        <td class="toolBar">
                            <input class="submitImgButton" id="cmdSave" type="submit" value="保 存" name="cmdSave"
                                runat="server">
                        </td>
                        <td>
                            <input class="submitImgButton" id="cmdCancel" type="submit" value="取 消" name="cmdCancel"
                                runat="server">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
