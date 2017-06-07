<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FPortionStockCheckOp.aspx.cs"
    Inherits="BenQGuru.eMES.Web.WarehouseWeb.FPortionStockCheckOp" %>

<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="uc1" TagName="eMESDate" Src="../UserControl/DateTime/DateTime/eMESDate.ascx" %>
<%@ Register TagPrefix="cc2" Namespace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>FPortionStockCheckOp</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR" />
    <meta content="C#" name="CODE_LANGUAGE" />
    <meta content="JavaScript" name="vs_defaultClientScript" />
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
    <link href="<%=StyleSheet%>" rel="stylesheet" />

    <script src="js/jquery-1.3.2.min.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">

        function SelectViewField() {
            var result = window.showModalDialog("FViewFieldEP.aspx?defaultUserCode=PickLine_FIELD_LIST_SYSTEM_DEFAULT&table=TBLPICK", "", "dialogWidth:800px;dialogHeight:600px;scroll:none;help:no;status:no");
            if (result == "OK") {
                window.location.href = window.location.href;
            }
        }
    </script>

</head>
<body>
    <form id="Form1" method="post" name="Form1" runat="server">
    <table id="Table1" height="100%" width="100%" runat="server">
        <tr class="moduleTitle">
            <td>
                <asp:Label ID="lblTitle" runat="server" CssClass="labeltopic">盘点作业</asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table class="query">
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblStockCheckCodeQuery" runat="server">盘点单号</asp:Label>
                        </td>
                        <td class="fieldValue" nowrap>
                            <asp:TextBox ID="txtStockCheckCodeQuery" name="txtStockCheckCodeQuery" runat="server"
                                CssClass="require" Width="130px"></asp:TextBox>
                        </td>
                        <td colspan="4" nowrap width="100%">
                        </td>
                        <td class="fieldValue" nowrap>
                            <input class="submitImgButton" id="cmdQuery" type="submit" value="查 询" name="btnQuery"
                                runat="server" />
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
                            <input class="gridExportButton" id="cmdGridExport" type="submit" value="  " name="cmdCancel"
                                runat="server" />
                        </td>
                        <td class="smallImgButton">
                            <input class="deleteButton" id="cmdDelete" type="submit" value="  " name="cmdDelete"
                                runat="server" />
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
                <table class="edit" height="100%" cellpadding="0" width="100%">
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td class="fieldNameLeft" style="height: 26px" nowrap>
                                        <asp:Label ID="lblLocationCode2Edit" runat="server">实际货位</asp:Label>
                                    </td>
                                    <td class="fieldValue" style="height: 26px">
                                        <asp:TextBox ID="txtLocationCodeEdit" runat="server" CssClass="require" Width="130px"></asp:TextBox>
                                    </td>
                                    <td class="fieldNameLeft" style="height: 26px" nowrap>
                                        <asp:Label ID="lblCARTONNOEdit" runat="server">实际箱号</asp:Label>
                                    </td>
                                    <td class="fieldValue" style="height: 26px">
                                        <asp:TextBox ID="txtCARTONNOEdit" runat="server" CssClass="require" Width="130px"></asp:TextBox>
                                    </td>
                                    <td> <asp:Label ID="lblDiffDescEdit" runat="server">盘点差异原因</asp:Label>
                                    </td>
                                    <td>  <asp:TextBox ID="txtDiffDescEdit" runat="server" CssClass="require" Width="200px"></asp:TextBox>
                                    </td>
                                    <td>    
                                    </td>
                                </tr>
                                <tr>
                                    <td class="fieldNameLeft" nowrap>
                                        <asp:Label ID="lblCheckQtyEdit" runat="server">盘点数量</asp:Label>
                                    </td>
                                    <td class="fieldValue" style="width: 159px">
                                        <asp:TextBox ID="txtCheckQtyEdit" runat="server" CssClass="require" Width="130px"></asp:TextBox>
                                    </td>
                                    <td class="fieldNameLeft" style="height: 26px" nowrap>
                                        <asp:Label ID="lblDQMCODEQuery" runat="server">鼎桥物料编码</asp:Label>
                                    </td>
                                    <td class="fieldValue" style="height: 26px">
                                        <asp:TextBox ID="txtDQMCODEEdit" runat="server" CssClass="require" Width="130px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <input class="submitImgButton" id="cmdAdd" type="submit" value="提交" name="cmdInitial"
                                            runat="server" onserverclick="cmdAdd_ServerClick" />
                                    </td>FDGH
                                    <td>
                                        <input class="submitImgButton" id="cmdStorageCheckClose" type="submit" value="结束盘点"
                                            name="cmdInitial" runat="server" onserverclick="cmdStorageCheckClose_ServerClick" />
                                    </td>
                                    <td>
                                        <input class="submitImgButton" id="cmdReturn" type="submit" value="返回" name="cmdReturn"
                                            runat="server" onserverclick="cmdReturn_ServerClick" visible="false" />
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="fieldNameLeft" style="height: 26px" nowrap>
                                        <asp:Label ID="lblStockCheckCodEdit" runat="server">盘点单号</asp:Label>
                                    </td>
                                    <td class="fieldValue" style="height: 26px">
                                        <asp:TextBox ID="txtStockCheckCodeEdit" runat="server" CssClass="require" Width="130px"
                                            Enabled="false"></asp:TextBox>
                                    </td>
                                    <td class="fieldValue" style="height: 26px">
                                        <asp:Label ID="lblStorageCode" runat="server">库位编码</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtStorageCodeEdit" runat="server" CssClass="require" Width="130px"
                                            Enabled="false"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="fieldValue" style="height: 26px">
                                        <asp:TextBox ID="txtsCARTONNOEdit" runat="server" CssClass="require" Width="130px"></asp:TextBox>
                                    </td>
                                    <td class="fieldValue" style="height: 26px">
                                        <asp:TextBox ID="txtsLocationCodeEdit" runat="server" CssClass="require" Width="130px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
