<%@ Page Language="c#" CodeBehind="FModelMP.aspx.cs" AutoEventWireup="True" Inherits="BenQGuru.eMES.Web.MOModel.FModelMP" %>

<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>FModelMP</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="<%=StyleSheet%>" rel="stylesheet">
    <script>
        function checkDataLink() {
            if (document.getElementById("chbIsDataLink").checked) {
                document.getElementById("txtDataLinkQty").disabled = false;
            }
            else {
                document.getElementById("txtDataLinkQty").value = "";
                document.getElementById("txtDataLinkQty").disabled = true;
            }
        }

        function checkDim() {
            if (document.getElementById("chbIsDim").checked) {
                document.getElementById("txtDimQty").disabled = false;
            }
            else {
                document.getElementById("txtDimQty").value = "";
                document.getElementById("txtDimQty").disabled = true;
            }
        }
			
    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
    <table id="Table1" height="100%" width="100%" runat="server">
        <tr class="moduleTitle">
            <td>
                <asp:Label ID="lblTitle" runat="server" CssClass="labeltopic"> 产品别维护</asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table class="query" height="100%" width="100%">
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblModelCodeQuery" runat="server"> 产品别代码</asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 159px">
                            <asp:TextBox ID="txtModelCodeQuery" runat="server" CssClass="textbox" Width="150px"></asp:TextBox>
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
                <table class="edit" height="100%" cellpadding="0" width="100%">
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblModelCodeEdit" runat="server">产品别代码</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtModelCodeEdit" runat="server" CssClass="require" Width="150px"></asp:TextBox>
                        </td>
                        <td class="fieldNameLeft" style="height: 26px" nowrap>
                            <asp:Label ID="lblModelDescriptionEdit" runat="server">产品别描述</asp:Label>
                        </td>
                        <td class="fieldValue" style="height: 26px">
                            <asp:TextBox ID="txtModelDescriptionEdit" runat="server" CssClass="textbox" Width="150px"></asp:TextBox>
                        </td>
                        <td nowrap width="134" style="width: 134px">
                            <asp:CheckBox ID="chbIsIn" runat="server" Style="display: none;" Text="是否可入库"></asp:CheckBox>
                        </td>
                        <td>
                            <asp:CheckBox ID="chbIsReflow" runat="server" Text="是否使用回流" Style="display: none;">
                            </asp:CheckBox>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblOrgEdit" runat="server">组织</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:DropDownList ID="DropDownListOrg" runat="server" CssClass="textbox" Width="150px">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:CheckBox ID="chbIsDim" Style="display: none;" runat="server" Text="是否检查尺寸量测数量"
                                onclick="checkDim();"></asp:CheckBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDimQty" runat="server" Style="display: none;" CssClass="textbox"
                                Width="150px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:CheckBox ID="chbIsDataLink" Style="display: none;" runat="server" Text="是否检查设备连线数量"
                                onclick="checkDataLink();"></asp:CheckBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDataLinkQty" Style="display: none;" runat="server" CssClass="textbox"
                                Width="150px"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                        <td>
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
