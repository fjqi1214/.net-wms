<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FStockCheck.aspx.cs" Inherits="BenQGuru.eMES.Web.WarehouseWeb.FStockCheck" %>

<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="uc1" TagName="eMESDate" Src="../UserControl/DateTime/DateTime/eMESDate.ascx" %>
<%@ Register TagPrefix="cc2" Namespace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>FStockCheck</title>
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
                <asp:Label ID="lblTitle" runat="server" CssClass="labeltopic">发货箱单</asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table class="query" height="100%" width="100%">
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblStockCheckCodeQuery" runat="server">盘点单号</asp:Label>
                        </td>
                        <td class="fieldValue" nowrap>
                            <asp:TextBox ID="txtStockCheckCodeQuery" name="txtStockCheckCodeQuery" runat="server"
                                CssClass="require" Width="130px"></asp:TextBox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblStorageCodeQuery" runat="server">库位</asp:Label>
                        </td>
                        <td class="fieldValue" nowrap>
                            <asp:DropDownList ID="drpStorageCodeQuery" name="drpStorageCodeQuery" AutoPostBack="false"
                                runat="server" CssClass="require" Width="120px">
                            </asp:DropDownList>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="LblCheckType" runat="server">盘点类型</asp:Label>
                        </td>
                        <td class="fieldValue" nowrap>
                            <asp:DropDownList ID="drpCheckType" name="drpCheckType" AutoPostBack="false" runat="server"
                                CssClass="require" Width="120px" >
                            </asp:DropDownList>
                        </td>
                        <td class="fieldName" nowrap>
                            <input class="submitImgButton" id="cmdQuery" type="submit" value="查 询" name="btnQuery"
                                runat="server" />
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblCreateDate" runat="server">盘点日期</asp:Label>
                        </td>
                        <td class="fieldValue" nowrap>
                            <asp:TextBox type="text" ID="txtCBDateQuery" name="txtCBDateQuery" class='datepicker'
                                runat="server" Width="130px" />
                        </td>
                        <td class="fieldName" nowrap style="text-align: left;">
                            <asp:Label ID="lblTo" runat="server">至</asp:Label>
                        </td>
                        <td class="fieldValue" nowrap style="text-align: left;">
                            <asp:TextBox type="text" ID="txtCEDateQuery" name="txtCEDateQuery" class='datepicker'
                                runat="server" Width="130px" />
                        </td>
                        <td class="fieldNameLeft" nowrap>
                        </td>
                        <td class="fieldValue" nowrap>
                        </td>
                        <td class="fieldName" nowrap>
                        </td>
                        <td class="fieldValue" nowrap>
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
                                        <asp:Label ID="lblStockCheckEdit" runat="server">盘点单号</asp:Label>
                                    </td>
                                    <td class="fieldValue" style="height: 26px">
                                        <asp:TextBox ID="txtStockCheckEdit" runat="server" CssClass="require" Width="130px"
                                            Enabled="false"></asp:TextBox>
                                        <asp:Button ID="genStockId" runat="server" Text="产生单号"  OnClick="Gener_ServerClick" />
                                    </td>
                                    <td class="fieldNameLeft" style="height: 26px" nowrap>
                                        <asp:Label ID="lblStorageCodeEdit" runat="server">库位</asp:Label>
                                    </td>
                                    <td class="fieldValue" style="height: 26px">
                                        <asp:DropDownList ID="drpStorageCodeEdit" name="drpStorageCodeEdit" AutoPostBack="false"
                                            runat="server" CssClass="require" Width="120px">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="fieldNameLeft" style="height: 26px" nowrap>
                                        <asp:Label ID="lblCheckTypeEdit" runat="server">盘点类型</asp:Label>
                                    </td>
                                    <td class="fieldValue" style="height: 26px">
                                        <asp:DropDownList ID="drpCheckTypeEdit" name="drpCheckTypeEdit" AutoPostBack="false"
                                             runat="server" CssClass="require" Width="120px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="fieldNameLeft" nowrap>
                                        <asp:Label ID="lblCBDateQuery" runat="server">盘点日期</asp:Label>
                                    </td>
                                    <td class="fieldValue" style="width: 159px">
                                        <asp:TextBox type="text" ID="txtBDateEdit" class='datepicker' runat="server" Width="130px" />
                                    </td>
                                    <td class="fieldNameLeft" nowrap>
                                        <asp:Label ID="lblFrom" runat="server">至</asp:Label>
                                    </td>
                                    <td class="fieldValue" style="width: 159px">
                                        <asp:TextBox type="text" ID="txtEDateEdit" class='datepicker' runat="server" Width="130px" />
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="fieldNameLeft" style="height: 26px" nowrap>
                                        <asp:Label ID="lblREMARKEdit" runat="server">备注</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtREMARKEdit" runat="server" CssClass="require" Width="130px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <input class="submitImgButton" id="cmdAdd" type="submit" value="新增" name="cmdInitial"
                                runat="server" onserverclick="cmdAdd_ServerClick" />
                                    </td>
                                    <td>
                                       
                                    </td>
                                    <td>
                                    
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="fieldNameLeft" style="height: 26px" nowrap>
                                        <asp:Label ID="lblUploadCheckResult" runat="server">导入盘点结果</asp:Label>
                                    </td>
                                    <td class="fieldValue" style="height: 26px">
                                        <asp:FileUpload ID="FileUpload1" name runat="server" />
                                        <input class="cmdAddImport" id="cmdAddImport" type="submit" value="上传" name="cmdAddImport"
                                            runat="server" onserverclick="Button1_Click">
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                       <input class="submitImgButton" id="cmdLoadCheckResult" type="submit" value="导出全盘清单" name="btnQuery"
                                runat="server" onserverclick="cmdLoadCheckResult_click" />
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
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
