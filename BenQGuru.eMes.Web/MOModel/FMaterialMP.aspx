<%@ Page Language="c#" CodeBehind="FMaterialMP.aspx.cs" AutoEventWireup="True" Inherits="BenQGuru.eMES.Web.MOModel.FMaterialMP" %>

<%@ Register TagPrefix="cc2" Namespace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>FMaterialMP</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="<%=StyleSheet%>" rel="stylesheet">
    <script language="javascript">
        //			function setCheckTypeStatus()
        //			{
        //				if (!document.all.chkParseBarcode.checked 
        //			        && !document.all.chkParsePrepare.checked 
        //			        && !document.all.chkParseProduct.checked)
        //		        {
        //				    document.all.chkLinkBarcode.disabled = true;
        //				    document.all.chkCompareItem.disabled = true;
        //				    document.all.chkSNLength.disabled = true;
        //				    				        
        //			        document.all.chkLinkBarcode.checked = false;
        //				    document.all.chkCompareItem.checked = false;
        //				    document.all.chkSNLength.checked = false;
        //		        }
        //		        else
        //		        {
        //				    document.all.chkLinkBarcode.disabled = false;
        //				    document.all.chkCompareItem.disabled = false;	
        //				    document.all.chkSNLength.disabled = false;
        //		        }
        //			}

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
<body ms_positioning="GridLayout" onload="loadCheckStatus()">
    <form id="Form1" method="post" runat="server">
    <table id="Table1" height="100%" width="100%" runat="server">
        <tr class="moduleTitle">
            <td>
                <asp:Label ID="lblTitle" runat="server" CssClass="labeltopic"> 物料维护 </asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table class="query" height="100%" width="100%">
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblMaterialCodeQuery" runat="server"> 物料代码 </asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 110px">
                            <asp:TextBox ID="TextBoxMaterialCodeQuery" runat="server" CssClass="textbox" Width="110px"></asp:TextBox>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblMaterialNameQuery" runat="server"> 物料名称 </asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 10px">
                            <asp:TextBox ID="TextBoxMaterialNameQuery" runat="server" CssClass="textbox" Width="110px"></asp:TextBox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblControlType" runat="server"> 管控类型</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:DropDownList ID="drpControlType" runat="server" Width="110px" OnLoad="drpControlType_Load">
                            </asp:DropDownList>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblItemNameEditSRM" runat="server"> 物料描述 </asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 110px">
                            <asp:TextBox ID="TextBoxItemNameEditSRM" runat="server" CssClass="textbox" Width="110px"></asp:TextBox>
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
                        <td>
                        </td>
                        <td class="smallImgButton">
                            <input class="gridExportButton" id="cmdGridExport" type="submit" value="  " name="cmdCancel"
                                runat="server" onserverclick="cmdGridExport_ServerClick">
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
                        <td class="fieldName" nowrap width="150px">
                            <asp:Label ID="lblParseTypeEdit" runat="server">解析方式</asp:Label>
                        </td>
                        <td class="fieldValue" nowrap width="150px">
                            <asp:CheckBox ID="chkParseBarcode" runat="server" Text="条码解析"></asp:CheckBox>
                        </td>
                        <td class="fieldName" nowrap width="150px">
                            <asp:Label ID="lblCheckTypeEdit" runat="server">检查类型</asp:Label>
                        </td>
                        <td class="fieldValue" nowrap width="180px">
                            <asp:CheckBox ID="chkLinkBarcode" runat="server" Text="条码关联"></asp:CheckBox>
                        </td>
                        <td class="fieldName" nowrap style="display: none;">
                            <asp:Label ID="Label1" runat="server"> 物料代码 </asp:Label>
                        </td>
                        <td class="fieldValue" style="display: none;">
                            <asp:TextBox ID="TextBoxMaterialCodeEdit" runat="server" CssClass="textbox"></asp:TextBox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblVendorCodeEdit" runat="server"> 供应商代码</asp:Label>
                        </td>
                        <td class="fieldValue" style="height: 26px">
                            <cc2:selectabletextbox id="txtVendorCode" runat="server" CanKeyIn="true" Type="singlevendor"
                                Target="">
                            </cc2:selectabletextbox>
                        </td>
                        <td></td>
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
                        <td class="fieldName" nowrap style="display: none;">
                            <asp:Label ID="Label2" runat="server"> 组织编号 </asp:Label>
                        </td>
                        <td class="fieldValue" style="display: none;">
                            <asp:TextBox ID="TextBoxOrgIDEdit" runat="server" CssClass="textbox"></asp:TextBox>
                        </td>
                        <td class="fieldName" nowrap="noWrap">
                            <asp:Label ID="lblControlTypeEdit" runat="server">管控类型</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:DropDownList ID="ddlControlTypeEdit" runat="server" CssClass="textbox" Width="120px"
                                OnLoad="ddlControlTypeEdit_Load">
                            </asp:DropDownList>
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
                        <td class="fieldName" nowrap>
                        </td>
                        <td class="fieldValue" nowrap>
                            <asp:CheckBox ID="chkIsSMT" runat="server" Text=""></asp:CheckBox>
                            <asp:Label ID="lblIsSMT" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldName" nowrap>
                        </td>
                        <td class="fieldValue" nowrap>
                            <asp:CheckBox ID="chkCheckStatus" runat="server"
                                Text="生产防错"></asp:CheckBox>
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
        <tr class="toolBar">
            <td>
                <table class="toolBar">
                    <tr>
                        <td class="toolBar">
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
