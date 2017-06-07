<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FItem2LotCheckMP.aspx.cs"
    Inherits="BenQGuru.eMES.Web.MOModel.FItem2LotCheckMP" %>

<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>FItem2LotCheckMP</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR" />
    <meta content="C#" name="CODE_LANGUAGE" />
    <meta content="JavaScript" name="vs_defaultClientScript" />
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
    <link href="<%=StyleSheet%>" rel="stylesheet" />
</head>

<script language="javascript" type="text/javascript">
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

        if (!isNumber(snLengthValue)) {
            alert(document.getElementById("hiddenInfo").value);
            document.getElementById("txtSNLength").focus();
            return false;
        }

        var result = openPage('<%=Request.ApplicationPath%>/MOModel/FItem2LotCheck.ashx?snPrefix=' + snPrefixValue + '&snLength=' + snLengthValue + '&Action=' + actionType + '&ItemCode=' + itemCodeValue);

        if (result != null && result.length != 0) {
            var confirmDialogPath = '<%=Request.ApplicationPath%>/MOModel/ConfirmDialog.htm';
            // the basic width
            var rdBasicWidth = "420px";
            // the basic height
            var rdBasicHeight = "240px";
            var _g_privateDialogFeatures = "status=no;center=yes;help=no;dialogWidth=" +
                                            rdBasicWidth + ";dialogHeight=" +
								            rdBasicHeight + ";scroll=yes;resize=no";

            var returnValue = window.showModalDialog(confirmDialogPath, result, _g_privateDialogFeatures);

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

</script>

<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
    <table id="Table1" height="100%" width="100%" runat="server">
        <tr class="moduleTitle">
            <td>
                <asp:Label ID="lblTitle" runat="server" CssClass="labeltopic">批次条码防呆例外设定</asp:Label>
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
                            <asp:Label ID="lblLotPrefixQuery" runat="server">批次条码前缀</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtSNPrefixQuery" runat="server" CssClass="textbox" Width="130px"></asp:TextBox>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblLotLengthQuery" runat="server">批次条码长度</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtSNLengthQuery" runat="server" CssClass="textbox" Width="130px"></asp:TextBox>
                        </td>
                        <td style="width: 100%">
                            <asp:HiddenField ID="hiddenInfo" runat="server" Value="批次条码长度必须为数字格式"></asp:HiddenField>
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
                            <asp:Label ID="lblLotPrefix" runat="server">批次条码前缀</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtSNPrefix" CssClass="require" runat="server"></asp:TextBox>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblLotLength" runat="server">批次条码长度</asp:Label>
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
                            <asp:Label ID="lblCreateType" runat="server">生成进制</asp:Label>
                        </td>
                        <td class="fieldValue" colspan="2">
                            <asp:RadioButtonList ID="RadioButtonListCreateTypeEdit" runat="server" Width="260px"
                                AutoPostBack="False" RepeatDirection="Horizontal" RepeatColumns="3">
                            </asp:RadioButtonList>
                        </td>
                        <td style="width: 100%" colspan="5">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="toolBar">
            <td>
                <table class="toolBar">
                    <tr>
                        <td>
                            <input class="submitImgButton" id="cmdAdd" type="submit" value="新 增" name="cmdAdd"
                                runat="server" onclick="return checkSNCount('ADD');" />
                        </td>
                        <td>
                            <input class="submitImgButton" id="cmdSave" type="submit" value="保 存" name="cmdSave"
                                runat="server" onclick="return checkSNCount('UPDATE');" />
                        </td>
                        <td>
                            <input class="submitImgButton" id="cmdCancel" type="submit" value="取 消" name="cmdCancel"
                                runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
