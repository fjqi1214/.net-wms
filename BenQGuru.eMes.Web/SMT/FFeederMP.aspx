<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>

<%@ Page Language="c#" CodeBehind="FFeederMP.aspx.cs" AutoEventWireup="True" Inherits="BenQGuru.eMES.Web.SMT.FFeederMP" %>

<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>FFeederMP</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="<%=StyleSheet%>" rel="stylesheet">
    <script language="javascript">
        function CalAlertCount() {
            var maxCount = document.getElementById("txtMaxCountEdit").value;
            if (maxCount.length > 0) {
                var iAlertCount = parseInt(maxCount) * 0.9
                document.getElementById("txtAlertCountEdit").value = parseInt(iAlertCount);
            }
            else {
                document.getElementById("txtAlertCountEdit").value = "0";
            }
        }
    </script>
</head>
<body ms_positioning="GridLayout" scroll="yes">
    <form id="Form1" method="post" runat="server">
    <table height="100%" width="100%" runat="server">
        <tr class="moduleTitle">
            <td>
                <asp:Label ID="lblTitle" runat="server" CssClass="labeltopic">Feeder登记</asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table class="query" height="100%" width="100%">
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblFeederCodeQuery" runat="server">Feeder代码</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtFeederCodeQuery" CssClass="textbox" runat="server"></asp:TextBox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblFeederSpecCodeQuery" runat="server">规格代码</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:DropDownList ID="ddlFeederSpecCodeQuery" CssClass="textbox" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblStatus" runat="server">状态</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:DropDownList ID="ddlStatusQuery" CssClass="textbox" runat="server">
                            </asp:DropDownList>
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
                <ig:WebDataGrid ID="gridWebGrid" runat="server" Width="100%">
                </ig:WebDataGrid>
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
                            <cc1:pagersizeselector id="pagerSizeSelector" runat="server">
                            </cc1:pagersizeselector>
                        </td>
                        <td align="right">
                            <cc1:PagerToolbar id="pagerToolBar" runat="server">
                            </cc1:PagerToolbar>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="normal">
            <td>
                <table class="edit" height="100%" cellpadding="0" width="100%">
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblFeederCodeEdit" runat="server">Feeder代码</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtFeederCodeEdit" CssClass="require" runat="server"></asp:TextBox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblFeederSpecCodeEdit" runat="server">规格代码</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:DropDownList ID="ddlFeederSpecCodeEdit" CssClass="require" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblFeederTypeEdit" runat="server" Visible="False">Feeder类型</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:DropDownList ID="ddlFeederTypeEdit" CssClass="textbox" runat="server" Visible="False">
                            </asp:DropDownList>
                        </td>
                        <td nowrap width="100%" colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblMaxCountEdit" runat="server">最大使用次数</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtMaxCountEdit" CssClass="textbox" runat="server"></asp:TextBox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblAlertCountEdit" runat="server">预警使用次数</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtAlertCountEdit" CssClass="textbox" runat="server"></asp:TextBox>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblMaxMDayEdit" runat="server">保养期限</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="TxtMaxMDayEdit" CssClass="textbox" runat="server"></asp:TextBox>
                            <asp:Label ID="lblDay" runat="server" Text="天"></asp:Label>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblAlterMDAYEdit" runat="server">预警期限</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="TxtAlterMDAYEdit" CssClass="textbox" runat="server"></asp:TextBox>
                            <asp:Label ID="lblDayD" runat="server" Text="天"></asp:Label>
                        </td>
                        <td nowrap width="100%">
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
