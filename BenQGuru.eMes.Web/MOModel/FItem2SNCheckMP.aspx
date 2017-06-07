<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FItem2SNCheckMP.aspx.cs"
    Inherits="BenQGuru.eMES.Web.MOModel.FItem2SNCheckMP" %>

<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>FItem2SNCheckMP</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="<%=StyleSheet%>" rel="stylesheet">
</head>
<script language="javascript">
    function isNumber(sStr) {
        for (var i = 0; i < sStr.length; i++) {
            if (isNaN(parseInt(sStr.substr(i, 1))))
                return false;
        }
        return true;
    }
    function checkSNCount(actionType) {
        var itemCode = document.getElementById("txtItemCode");
        var itemCodeValue = itemCode.value;

        var snPrefix = document.getElementById("txtSNPrefix");
        var snPrefixValue = snPrefix.value;

        var snLength = document.getElementById("txtSNLength");
        var snLengthValue = snLength.value;

        if (isNumber(snLengthValue) == false) {
            alert("序列号长度必须为数字格式");
            document.getElementById("txtSNLength").focus();
            return false;
        }

        var result = openPage('<%=Request.ApplicationPath%>/MOModel/FItem2SNCheck.ashx?snPrefix=' + snPrefixValue + '&snLength=' + snLengthValue + '&Action=' + actionType + '&ItemCode=' + itemCodeValue);

        if (result != null && result.length != 0) {
            var confirmDialogPath = '<%=Request.ApplicationPath%>/MOModel/ConfirmDialog.htm';
            // the basic width
            var rdBasicWidth = "420px";
            // the basic height
            var rdBasicHeight = "240px";
            var _g_privateDialogFeatures = "status=no;center=yes;help=no;dialogWidth=" +
                                            rdBasicWidth + ";dialogHeight=" +
								            rdBasicHeight + ";scroll=yes;resize=no";

            var args = '此规则与产品 [' + result + '] 相同,是否继续保存?';
            var returnValue = window.showModalDialog(confirmDialogPath, args, _g_privateDialogFeatures);

            if (returnValue == 0) {
                return false;
            }
            else {
                return true;
            }
        }
        return true;
    }

    function openPage(pageName) {
        var xmlHTTP = new ActiveXObject("Microsoft.XMLHTTP");

        xmlHTTP.open("GET", pageName, false);

        xmlHTTP.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");

        xmlHTTP.send();

        return xmlHTTP.responseText;
    }

    function btnQuery_onclick() {
        var lengthValue = document.getElementById("txtSNLengthQuery").value;
        if (isNumber(lengthValue) == false) {
            alert("序列号长度必须为数字格式");
            document.getElementById("txtSNLengthQuery").focus();
            return false;
        }
        return true;
    }

</script>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
    <table id="Table1" height="100%" width="100%" runat="server">
        <tr class="moduleTitle">
            <td>
                <asp:Label ID="lblTitle" runat="server" CssClass="labeltopic">序列号防呆设定</asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table class="query" height="100%" width="100%">
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblItemCodeQuery" runat="server">产品代码</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtItemCodeQuery" runat="server" CssClass="textbox" Width="130px"></asp:TextBox>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblItemDescQuery" runat="server">产品描述</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtItemDescQuery" runat="server" CssClass="textbox" Width="130px"></asp:TextBox>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblItemTypeQuery" runat="server" Width="130px">产品类型</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:DropDownList ID="drpItemTypeQuery" runat="server" Width="130px" AutoPostBack="False">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblExportImportQuery" runat="server">进出口</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:DropDownList ID="drpExportImportQuery" runat="server" Width="130px" AutoPostBack="False">
                            </asp:DropDownList>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblSNPrefixQuery" runat="server">序列号前缀</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtSNPrefixQuery" runat="server" CssClass="textbox" Width="130px"></asp:TextBox>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblSNLengthQuery" runat="server">序列号长度</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtSNLengthQuery" runat="server" CssClass="textbox" Width="130px"></asp:TextBox>
                        </td>
                        <td style="width: 100%">
                        </td>
                        <td style="width: 100%">
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblCheckTypeQuery" runat="server">检查类型</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:DropDownList ID="DropDownListCheckTypeQuery" runat="server" Width="130px" AutoPostBack="False">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 100%" colspan="6">
                        </td>
                        <td class="fieldName">
                            <input class="submitImgButton" id="cmdQuery" type="submit" value="查 询" name="btnQuery"
                                runat="server">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
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
                            <asp:Label ID="lblItemCode" runat="server">产品代码</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtItemCode" CssClass="require" runat="server"></asp:TextBox>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblSNPrefix" runat="server">序列号前缀</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtSNPrefix" CssClass="require" runat="server"></asp:TextBox>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblSNLength" runat="server">序列号长度</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtSNLength" CssClass="require" runat="server" MaxLength="6" Width="120px"></asp:TextBox>
                        </td>
                        <td class="fieldValue">
                            <asp:CheckBox ID="chkSNContentCheck" runat="server" Text="限制序列号内容为字符,数字和空格"></asp:CheckBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblCheckTypeEdit" runat="server">检查类型</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:DropDownList ID="DropDownListCheckTypeEdit" runat="server" Width="130px" AutoPostBack="False">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 100%" colspan="6">
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
                                runat="server" onclick="return checkSNCount('ADD');">
                        </td>
                        <td class="toolBar">
                            <input class="submitImgButton" id="cmdSave" type="submit" value="保 存" name="cmdSave"
                                runat="server" onclick="return checkSNCount('UPDATE');">
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
