<%@ Register TagPrefix="uc1" TagName="eMESDate" Src="~/UserControl/DateTime/DateTime/eMESDate.ascx" %>
<%@ Register TagPrefix="ss1" Namespace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<%@ Register TagPrefix="cc2" Namespace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>

<%@ Page Language="c#" CodeBehind="FItem2SPCItemSpecMP.aspx.cs" AutoEventWireup="True"
    Inherits="BenQGuru.eMES.Web.MOModel.FItem2SPCItemSpecMP" %>

<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>FItem2SPCItemSpecMP</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="<%=StyleSheet%>" rel="stylesheet">
    <script language="javascript">
        function Init() {
            ShowAuto();
            ShowLow();
            ShowUp();
        }

        //自动生成UCL/LCL
        function ShowAuto() {
            if (document.all.chbAutoCLEdit.checked) {
                document.getElementById("txtUCLEdit").disabled = true;
                document.getElementById("txtUCLEdit").value = "";
                document.getElementById("txtLCLEdit").disabled = true;
                document.getElementById("txtLCLEdit").value = "";
            }
            else {
                document.getElementById("txtUCLEdit").disabled = false;
                document.getElementById("txtLCLEdit").disabled = false;
            }
        }
        //单边上限规则
        function ShowUp() {
            if (document.all.chbUpEdit.checked) {
                document.getElementById("txtLCLEdit").disabled = true;
                document.getElementById("txtLCLEdit").value = "";
            }
            else {
                document.getElementById("txtLCLEdit").disabled = false;
            }
        }
        //单边下限规则
        function ShowLow() {
            if (document.all.chbLowEdit.checked) {
                document.getElementById("txtUCLEdit").disabled = true;
                document.getElementById("txtUCLEdit").value = "";
            }
            else {
                document.getElementById("txtUCLEdit").disabled = false;
            }
        }
    </script>
</head>
<body ms_positioning="GridLayout" onload="Init();">
    <form id="Form1" method="post" runat="server">
    <table id="tbl" height="100%" width="100%" runat="server">
        <tr class="moduleTitle">
            <td>
                <asp:Label ID="lblTitle" runat="server" CssClass="labeltopic"> 产品SPC测试项规格维护</asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table class="query" height="100%" width="100%">
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblItemCodeQuery" runat="server"> 产品代码</asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 159px">
                            <cc2:selectabletextbox id="txtItemCodeQuery" runat="server" CssClass="textbox" Type="item"
                                Target="item" Width="130px">
                            </cc2:selectabletextbox>
                        </td>
                        <td class="fieldNameLeft" style="height: 26px" nowrap>
                            <asp:Label ID="lblObjectCodeQuery" runat="server">管控项目</asp:Label>
                        </td>
                        <td class="fieldValue" style="height: 26px">
                            <asp:DropDownList ID="drpObjectCodeQuery" runat="server" CssClass="textbox" Width="130px">
                            </asp:DropDownList>
                        </td>
                        <td nowrap width="100%">
                        </td>
                        <td class="fieldName">
                            <input class="submitImgButton" id="cmdQuery" type="submit" value="查 询" name="btnQuery"
                                runat="server" onserverclick="cmdQuery_ServerClick">
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
                            <input class="gridExportButton" id="cmdGridExport" type="submit" value="  " name="cmdCancel"
                                runat="server" onserverclick="cmdGridExport_ServerClick">
                        </td>
                        <td class="smallImgButton">
                            <input class="deleteButton" id="cmdDelete" type="submit" value="  " name="cmdDelete"
                                runat="server" onserverclick="cmdDelete_ServerClick">
                        </td>
                        <td>
                            <cc1:pagersizeselector id="pagerSizeSelector" runat="server">
                            </cc1:pagersizeselector>
                        </td>
                        <td align="right">
                            <cc1:pagertoolbar id="pagerToolBar" runat="server" PageSize="20">
                            </cc1:pagertoolbar>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="normal">
            <td>
                <table class="edit" height="100%" cellpadding="0" width="100%"  bordercolor="#ebebeb">
                    <tr>
                        <td class="fieldNameLeft" style="height: 20px" nowrap>
                            <asp:Label ID="lblItemCodeEdit" runat="server">产品代码</asp:Label>
                        </td>
                        <td class="fieldValue" style="height: 20px">
                            <ss1:selectsingletabletextbox runat="server" ID="txtItemCodeEdit" CssClass="require"
                                type="singleitem">
                            </ss1:selectsingletabletextbox>
                        </td>
                        <td class="fieldNameLeft" style="height: 20px" nowrap>
                            <asp:Label ID="lblObjectCodeEdit" runat="server">管控项目</asp:Label>
                        </td>
                        <td class="fieldValue" style="height: 20px">
                            <asp:DropDownList ID="drpObjectCodeEdit" runat="server" Width="130px" OnSelectedIndexChanged="drpObjectCodeEdit_SelectedIndexChanged"
                                AutoPostBack="true">
                            </asp:DropDownList>
                        </td>
                        <td class="fieldNameLeft" style="height: 20px" nowrap>
                            <asp:Label ID="lblGroupSeqEdit" runat="server">组次</asp:Label>
                        </td>
                        <td class="fieldValue" style="height: 20px">
                            <asp:DropDownList ID="txtGroupSeqEdit" runat="server" CssClass="require" Width="130px">
                            </asp:DropDownList>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td style="height: 20px" width="100%">
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblConditionEdit" runat="server">规格条件</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtConditionEdit" runat="server" CssClass="require" Width="130px"></asp:TextBox>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblMemoEdit" runat="server">备注</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtMemoEdit" runat="server" CssClass="textbox" Width="130px"></asp:TextBox>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblTestCount" runat="server" Visible="false">测试次数</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtTestCount" runat="server" CssClass="textbox" Width="130px" Visible="false"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="lblColumnNamedit" runat="server" Visible="false">存放栏位</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtColumnNamedit" runat="server" CssClass="require" Width="130px"
                                Visible="false"></asp:TextBox>
                        </td>
                        <td style="height: 20px" width="100%">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="9">
                            <table>
                                <tr>
                                    <td class="fieldNameLeft" nowrap onclick="ShowAuto();">
                                        <asp:CheckBox ID="chbAutoCLEdit" runat="server" Text="自动生成UCL/LCL"></asp:CheckBox>
                                    </td>
                                    <td class="fieldValue" nowrap onclick="ShowUp();">
                                        <asp:CheckBox ID="chbUpEdit" runat="server" Text="单边上限规则"></asp:CheckBox>
                                    </td>
                                    <td class="fieldNameLeft">
                                        <asp:Label ID="lblUCLEdit" runat="server">UCL</asp:Label>
                                    </td>
                                    <td class="fieldValue">
                                        <asp:TextBox ID="txtUCLEdit" runat="server" CssClass="textbox" Width="130px"></asp:TextBox>
                                    </td>
                                    <td class="fieldNameLeft" nowrap>
                                        <asp:Label ID="lblUSLEdit" runat="server">USL</asp:Label>
                                    </td>
                                    <td class="fieldValue">
                                        <asp:TextBox ID="txtUSLEdit" runat="server" CssClass="require" Width="130px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td class="fieldValue" nowrap onclick="ShowLow();">
                                        <asp:CheckBox ID="chbLowEdit" runat="server" Text="单边下限规则"></asp:CheckBox>
                                    </td>
                                    <td class="fieldNameLeft">
                                        <asp:Label ID="lblLCLEdit" runat="server">LCL</asp:Label>
                                    </td>
                                    <td class="fieldValue">
                                        <asp:TextBox ID="txtLCLEdit" runat="server" CssClass="textbox" Width="130px"></asp:TextBox>
                                    </td>
                                    <td class="fieldNameLeft">
                                        <asp:Label ID="lblLSLEdit" runat="server">LSL</asp:Label>
                                    </td>
                                    <td class="fieldValue" nowrap>
                                        <asp:TextBox ID="txtLSLEdit" runat="server" CssClass="require" Width="130px"></asp:TextBox>
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
                        <td class="toolBar">
                            <input class="submitImgButton" id="cmdAdd" type="submit" value="新 增" name="cmdAdd"
                                runat="server" onserverclick="cmdAdd_ServerClick">
                        </td>
                        <td class="toolBar">
                            <input class="submitImgButton" id="cmdSave" type="submit" value="保 存" name="cmdSave"
                                runat="server" onserverclick="cmdSave_ServerClick">
                        </td>
                        <td>
                            <input class="submitImgButton" id="cmdCancel" type="submit" value="取 消" name="cmdCancel"
                                runat="server" onserverclick="cmdCancel_ServerClick">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
