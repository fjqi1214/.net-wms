<%@ Page Language="c#" CodeBehind="FOPBOMOperationComponetLoadingMP.aspx.cs" AutoEventWireup="True"
    Inherits="BenQGuru.eMES.Web.MOModel.FOPBOMOperationComponetLoadingMP" %>

<%@ Register TagPrefix="uc1" TagName="eMESDate" Src="~/UserControl/DateTime/DateTime/eMESDate.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>FOPBOMOperationComponetLoadingMP</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="<%=StyleSheet%>" rel="stylesheet">
    <script src="../Scripts/jquery-1.9.1.js" type="text/javascript"></script>
    <script language="javascript">

        function OnKeyPress() {
            if (13 == window.event.keyCode) { window.event.keyCode = 0; return false; }
        }

        function setCheckTypeStatus() {
            if (!document.all.chkParseBarcode.checked
			        && !document.all.chkParsePrepare.checked
			        && !document.all.chkParseProduct.checked) {
                document.all.chkLinkBarcode.disabled = true;
                document.all.chkCompareItem.disabled = true;

                document.all.chkLinkBarcode.checked = false;
                document.all.chkCompareItem.checked = false;
            }
            else {
                document.all.chkLinkBarcode.disabled = false;
                document.all.chkCompareItem.disabled = false;
            }
        }

        function onCheckChange(element) {
            if (element.id == "chkParseProduct") {
                if (element.checked) {
                    document.all.chkCheckStatus.disabled = false;
                }
                else {
                    document.all.chkCheckStatus.checked = false;
                    document.all.chkCheckStatus.disabled = true;
                }
            }

            if (element.id == "chkSNLength") {
                if (element.checked) {
                    document.all.txtSNLength.disabled = false;
                }
                else {
                    document.all.txtSNLength.disabled = true;
                }
            }

        }

        function loadCheckStatus() {
            if (!document.all.chkParseProduct.checked)
                document.all.chkCheckStatus.disabled = true;

            if (!document.all.chkSNLength.checked)
                document.all.txtSNLength.disabled = true;
        }	
    </script>
 
</head>
<body ms_positioning="GridLayout" onkeypress="OnKeyPress()" onload="loadCheckStatus()">
    <form id="Form1" method="post" runat="server">
    <table id="tableMain" height="100%" width="100%" runat="server">
        <tr>
            <td>
                <table class="query" width="100%">
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblSBOMVersionQuery" runat="server"> SBOM Version </asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 159px">
                            <asp:DropDownList ID="DropdownlistSBOMVersionQuery" runat="server" CssClass="require"
                                Width="120px" AutoPostBack="true" OnSelectedIndexChanged="DropdownlistSBOMVersionQuery_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td nowrap width="100%">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="fieldGrid" style="padding: 0px;">
                <ig:WebDataGrid ID="gridWebGrid" runat="server" Width="100%">
                </ig:WebDataGrid>
            </td>
        </tr>
        <tr class="normal">
            <td>
                <table cellpadding="0" width="100%">
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
                            <cc1:pagertoolbar id="pagerToolBar" runat="server" pagesize="20">
                            </cc1:pagertoolbar>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="normal">
            <td>
                <table class="edit" cellpadding="0" width="100%">
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblOPBOMItemSeqEdit" runat="server" Width="60px">子阶料顺序</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="TextboxOPBOMItemSeqEdit" runat="server" Width="110px" CssClass="textbox"></asp:TextBox>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblBOMItemCodeEdit" runat="server" Width="60px">子阶料料号</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtItemCodeEdit" runat="server" Width="110px" CssClass="require"></asp:TextBox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblBOMItemNameEdit" runat="server" Width="60px">子阶料名称</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtBOMItemNameEdit" runat="server" Width="110px" CssClass="textbox"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblSourceItemCode" runat="server" Width="60px">首选料</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtSourceItemCodeEdit" runat="server" Width="110px" CssClass="require"></asp:TextBox>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblItemQtyEdit" runat="server" Width="60px">单机用量</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtItemQtyEdit" runat="server" Width="110px" CssClass="require"></asp:TextBox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblItemUOMEdit" runat="server" Width="60px">计量单位</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtBOMItemUOMEdit" runat="server" Width="110px" CssClass="require"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblItemControlTypeEdit" runat="server" Width="60px">管制类型</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:DropDownList ID="drpItemControlTypeEdit" runat="server" CssClass="require" Width="110px"
                                OnLoad="drpItemControlTypeEdit_Load">
                            </asp:DropDownList>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblOPBOMSourceItemDescEdit" runat="server" Width="60px">物料描述</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="TextboxOPBOMSourceItemDescEdit" runat="server" Width="110px" CssClass="textbox"></asp:TextBox>
                        </td>
                        <td class="fieldName" nowrap>
                        </td>
                        <td class="fieldValue">
                            <asp:CheckBox ID="chkValid" runat="server" Width="60px" Text="有效"></asp:CheckBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblParseTypeEdit" runat="server" Width="60px">解析方式</asp:Label>
                        </td>
                        <td class="fieldValue" nowrap>
                            <asp:CheckBox ID="chkParseBarcode" runat="server" Text="条码解析"></asp:CheckBox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblCheckTypeEdit" runat="server" Width="60px">检查类型</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:CheckBox ID="chkLinkBarcode" runat="server" Text="条码关联"></asp:CheckBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldName" nowrap>
                        </td>
                        <td class="fieldValue" nowrap>
                            <asp:CheckBox ID="chkParsePrepare" runat="server" Text="备料信息"></asp:CheckBox>
                        </td>
                        <td class="fieldName" nowrap>
                        </td>
                        <td class="fieldValue">
                            <asp:CheckBox ID="chkCompareItem" runat="server" Text="料号比对"></asp:CheckBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldName" nowrap>
                        </td>
                        <td class="fieldValue" nowrap>
                            <asp:CheckBox ID="chkParseProduct" runat="server" Text="生产信息"></asp:CheckBox>
                        </td>
                        <td class="fieldName" nowrap>
                        </td>
                        <td class="fieldValue">
                            <asp:CheckBox ID="chkSNLength" runat="server" Text=""></asp:CheckBox><asp:Label ID="lblSNLength"
                                runat="server">序列号长度</asp:Label>&nbsp;<asp:TextBox ID="txtSNLength" runat="server"
                                    CssClass="textbox" Width="60px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldName" nowrap>
                        </td>
                        <td class="fieldValue" nowrap>
                            <asp:CheckBox ID="chkCheckStatus" runat="server" Width="60px" Text="生产防错"></asp:CheckBox>
                        </td>
                        <td class="fieldName" nowrap>
                        </td>
                        <td class="fieldValue">
                            <asp:CheckBox ID="chkNeedVendor" runat="server" Text=""></asp:CheckBox><asp:Label
                                ID="lblNeedVendor" runat="server">必须有供应商</asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td nowrap>
                <asp:CheckBox ID="chbSourceSBOMEdit" runat="server" Text="来源自标准BOM" Width="60px">
                </asp:CheckBox>
            </td>
        </tr>
        <tr class="toolBar">
            <td>
                <table class="toolBar" cellpadding="0">
                    <tr>
                        <td class="toolBar" nowrap>
                            <input class="submitImgButton" id="cmdAdd" type="submit" value="新 增" name="cmdAdd"
                                runat="server" onserverclick="cmdAdd_ServerClick" />
                            <input class="submitImgButton" id="cmdSave" type="submit" value="保 存" name="cmdSave"
                                runat="server" onserverclick="cmdSave_ServerClick" />
                            <input class="submitImgButton" id="cmdCancel" type="submit" value="取 消" name="cmdCancel"
                                runat="server" onserverclick="cmdCancel_ServerClick" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr  style="display: none">
            <td>
                <table>
                    <tr>
                        <td class="fieldName">
                            <asp:Label ID="lblMOEdit" runat="server" Visible="False">工单</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtMoCode" runat="server" Width="120px" CssClass="textbox" Visible="False"></asp:TextBox>
                        </td>
                        <td nowrap>
                        </td>
                        <td class="fieldValue">
                            <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:Label ID="lblStatus" runat="server" Visible="False">状态</asp:Label>
                        </td>
                        <td class="toolBar">
                            <asp:TextBox ID="txtStatus" runat="server" CssClass="textbox" Width="120px" Visible="False"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
