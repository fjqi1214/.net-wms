<%@ Register TagPrefix="cc2" Namespace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>

<%@ Page Language="c#" CodeBehind="FReelMP.aspx.cs" AutoEventWireup="True" Inherits="BenQGuru.eMES.Web.SMT.FReelMP" %>

<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>FReelMP</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="<%=StyleSheet%>" rel="stylesheet">
    <script language="javascript">
        function ParseReelNo() {
            var reelNo = document.getElementById("txtReelNoEdit").value;
            if (reelNo.indexOf(".") >= 0) {
                var idx = reelNo.indexOf(".");
                var lotNo = reelNo.substring(idx + 1, reelNo.length);
                document.getElementById("txtLotNoEdit").value = lotNo;
            }
            document.getElementById("txtPartNoEdit").focus();
        }
        function ParsePartNo() {
            var partNo = document.getElementById("txtPartNoEdit").value;
            if (partNo.length > 13) {
                var qty = partNo.substring(13, partNo.length);
                while (qty.length > 0 && qty.substring(0, 1) == "0") {
                    qty = qty.substring(1, qty.length);
                }
                if (qty.length == 0) {
                    qty = "0";
                }
                document.getElementById("txtQtyEdit").value = qty;
                document.getElementById("txtPartNoEdit").value = partNo.substring(0, 12);
                document.getElementById("txtLotNoEdit").focus();
            }
            else {
                document.getElementById("txtQtyEdit").focus();
            }
        }
    </script>
</head>
<body ms_positioning="GridLayout" scroll="yes">
    <form id="Form1" method="post" runat="server">
    <table height="100%" width="100%" runat="server">
        <tr class="moduleTitle">
            <td>
                <asp:Label ID="lblTitle" runat="server" CssClass="labeltopic">工单领料查询</asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table class="query" height="100%" width="100%">
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblMOIDQuery" runat="server">工单代码</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <cc2:selectabletextbox id="txtMOCodeQuery" runat="server" Type="mo" Width="90px">
                            </cc2:selectabletextbox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblSSQuery" runat="server">产线代码</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtSSCodeQuery" CssClass="textbox" runat="server"></asp:TextBox>
                        </td>
                        <td nowrap width="100%">
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblReelNoQuery" runat="server">料卷号码</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtReelNoQuery" CssClass="textbox" runat="server"></asp:TextBox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblItemIDQuery" runat="server">物料代码</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtPartNoQuery" CssClass="textbox" runat="server"></asp:TextBox>
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
                            <asp:Label ID="lblReelNoEdit" runat="server">料卷号码</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtReelNoEdit" CssClass="require" runat="server" Width="150px"></asp:TextBox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblPartNoEdit" runat="server">物料号</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtPartNoEdit" CssClass="require" runat="server" Width="150px"></asp:TextBox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblReelQtyEdit" runat="server">料卷数量</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtQtyEdit" CssClass="textbox" runat="server" Width="150px"></asp:TextBox>
                        </td>
                        <td nowrap width="100%">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblLotNo" runat="server">生产批号</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtLotNoEdit" CssClass="textbox" runat="server" Width="150px"></asp:TextBox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblDateCode" runat="server">生产日期</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtDateCodeEdit" CssClass="textbox" runat="server" Width="150px"></asp:TextBox>
                        </td>
                        <td nowrap width="100%" colspan="3">
                            &nbsp;
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
