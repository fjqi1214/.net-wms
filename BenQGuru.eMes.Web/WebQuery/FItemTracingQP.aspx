<%@ Page Language="c#" CodeBehind="FItemTracingQP.aspx.cs" AutoEventWireup="True"
    Inherits="BenQGuru.eMES.Web.WebQuery.FItemTracingQP" %>

<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="cc2" Namespace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>FItemTracingQP</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="<%=StyleSheet%>" rel="stylesheet">
    <script language="javascript">
        function onRadioCheckChange(radio) {

            if (radio.id == "rdbForword") {
                //document.all.rdbForword.checked = false;
                document.all.rdbBackword.checked = false;
            }
            else if (radio.id == "rdbBackword") {
                document.all.rdbForword.checked = false;
                //document.all.rdbBackword.checked = false;
            }
        }
    </script>
</head>
<body>
    <form id="Form1" method="post" runat="server">
    <table id="Table1" height="100%" width="100%" runat="server">
        <tr class="moduleTitle">
            <td>
                <asp:Label ID="lblTitle" runat="server">产品追溯管理</asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table class="query" id="Table2" height="100%" width="100%">
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblStartSnQuery" runat="server">起始序列号</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtStartSnQuery" runat="server" CssClass="textbox" Width="130px"></asp:TextBox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblEndSnQuery" runat="server">结束序列号</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtEndSnQuery" runat="server" CssClass="textbox" Width="130px"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                        <td nowrap>
                            <asp:RadioButton ID="rdbBackword" runat="server" Text="反追" Checked="True"></asp:RadioButton>
                            <asp:RadioButton ID="rdbForword" runat="server" Text="正追"></asp:RadioButton>
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblMOIDQuery" runat="server">工单代码</asp:Label>
                        </td>
                        <td class="fieldValue" width="150">
                            <cc2:selectabletextbox id="txtConditionMo" runat="server" Type="mo" Width="150" CanKeyIn="true">
                            </cc2:selectabletextbox>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblBigSSCodeWhere" runat="server">大线</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <cc2:selectabletextbox id="txtBigSSCodeWhere" runat="server" type="bigline" readonly="false"
                                cankeyin="true" width="130px">
                            </cc2:selectabletextbox>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblItemCodeQuery" runat="server">产品代码</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <cc2:selectabletextbox id="txtConditionItem" runat="server" type="item" width="130px"
                                cankeyin="true">
                            </cc2:selectabletextbox>
                        </td>
                        <td class="fieldName">
                            <input class="submitImgButton" id="cmdQuery" onmouseover="" onmouseout="" type="submit"
                                value="查 询" name="cmdQuery" runat="server">
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblMaterialModelCodeWhere" runat="server">机型</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <cc2:selectabletextbox id="txtMaterialModelCodeWhere" runat="server" type="mmodelcode"
                                readonly="false" cankeyin="true" width="130px">
                            </cc2:selectabletextbox>
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
                <table id="Table3" height="100%" cellpadding="0" width="100%">
                    <tr>
                        <td class="smallImgButton" nowrap>
                            <input class="gridExportButton" id="cmdGridExport" type="submit" value="  " name="cmdGridExport"
                                runat="server">
                        </td>
                        <td>
                            <cc1:pagersizeselector id="pagerSizeSelector" runat="server">
                            </cc1:pagersizeselector>
                        </td>
                        <td align="right">
                            <cc1:pagertoolbar id="pagerToolBar" runat="server">
                            </cc1:pagertoolbar>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
