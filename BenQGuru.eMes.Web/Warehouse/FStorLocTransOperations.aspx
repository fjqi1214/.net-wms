<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FStorLocTransOperations.aspx.cs"
    Inherits="BenQGuru.eMES.Web.Warehouse.FStorLocTransOperations" %>

<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="cc2" Namespace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<%@ Register TagPrefix="uc1" TagName="eMESDate" Src="~/UserControl/DateTime/DateTime/eMESDate.ascx" %>
<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>FStorLocTransOperations</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="<%=StyleSheet%>" rel="stylesheet">
    <style type="text/css">
        .require
        {}
    </style>
    <script>
        function CanDelete() {
            if (confirm("是否确认删除？")) {
                return true;
            } else {
                return false;
            }
        }
    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
    <table height="100%" width="100%" runat="server">
        <tr class="moduleTitle">
            <td>
                <asp:Label ID="lblTitle" runat="server" CssClass="labeltopic">转储作业</asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table class="query" height="100%" width="100%">
                    <tr>
                        <td class="fieldNameLeft" style="height: 26px" nowrap>
                            <asp:Label ID="lblTransNoQuery" runat="server">转储单号</asp:Label>
                        </td>
                    <td>
                            <asp:DropDownList ID="drpTransNoQuery" runat="server" CssClass="textbox" Width="150px"
                                DESIGNTIMEDRAGDROP="94">
                            </asp:DropDownList>
                        </td>
                <%--        <td  class="fieldValue" style="height: 26px"  nowrap>
                            <asp:DropDownList ID="" runat="server">
                            </asp:DropDownList>
                        </td>--%>
                        <td>
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
              <%--              <input class="deleteButton" id="cmdDelete" type="submit" value="  " name="cmdDelete"
                                runat="server">--%>
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
        <tr>
            <td>
                <table class="query" height="100%" width="100%">
                <%--<table class="edit" height="100%" cellpadding="0" width="100%">
                    <tr class="moduleTitle">
                        <td>--%>
                    <tr>
                        <td>
                            <table><tr><td>
                                <asp:Label ID="lblPackagingOperations" runat="server" Font-Bold="true" Font-Size="Small">转储作业</asp:Label>
                            </td></tr></table>
                        </td>
                    </tr>
                    <tr>
                        <%--<td>
                            <asp:RadioButton ID="rdbIntegrateCarton" runat="server" Text="整箱" GroupName="type"
                                Checked="true" OnCheckedChanged="" />
                        </td>
                        <td>
                            <asp:RadioButton ID="rdbSplitCarton" runat="server" Text="拆箱" GroupName="type" />
                        </td>--%>
                        <td>
                         <table><tr>
                        <td style="width:150px">
                            <asp:RadioButtonList ID="rdoSelectType" runat="server" CssClass="require"  
                                Width="200px" RepeatDirection="Horizontal" OnSelectedIndexChanged="rdoSelectType_SelectedIndexChanged" AutoPostBack="true" >
                                <%--<asp:ListItem Text="整箱" Value="AllCarton" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="拆箱" Value="SplitCarton"></asp:ListItem>--%>
                                <asp:ListItem Text="整箱" Value="整箱" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="拆箱" Value="拆箱"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>

                         <td class="fieldNameLeft" style="height: 26px" nowrap>
                            <asp:Label ID="lblOriginalCartonEdit" runat="server">原箱号</asp:Label>
                        </td>
                        <td class="fieldValue" style="height: 26px">
                            <asp:TextBox ID="txtFromCartonNo" runat="server" CssClass="require" Width="130px"
                                DESIGNTIMEDRAGDROP="257"></asp:TextBox>
                        </td>
                        <td class="fieldNameLeft" style="height: 26px" nowrap>
                            <asp:Label ID="lblQTY" runat="server">数量</asp:Label>
                        </td>
                        <td class="fieldValue" style="height: 26px">
                            <asp:TextBox ID="txtQTY" runat="server" CssClass="require" Width="130px" Enabled="false"
                                onKeyPress="if (event.keyCode < 48 || event.keyCode > 57) event.returnValue = false;"></asp:TextBox>
                        </td>
                        <td class="fieldNameLeft" style="height: 26px" nowrap>
                            <asp:Label ID="lblSNEdit" runat="server">SN</asp:Label>
                        </td>
                        <td class="fieldValue" style="height: 26px">
                            <asp:TextBox ID="txtSNEdit" runat="server" CssClass="require" Width="130px" Enabled="false"></asp:TextBox>
                        </td>
                        
                          <td class="toolBar">
                            <input class="submitImgButton" id="cmdCommit" type="submit" value="提 交" name="cmdCommit"
                                runat="server" onserverclick="cmdSubmit_ServerClick" />
                        </td>

                        </tr>
                        </table>
                        </td>
                    </tr>
                    <tr>
                      <td>
                         <table><tr>
                        <td class="fieldNameLeft" style="height: 26px" nowrap>
                            <asp:Label ID="lblLocationCode" runat="server">目标货位</asp:Label>
                        </td>
                        <td class="fieldValue" style="height: 26px">
                            <asp:TextBox ID="txtLocationCode" runat="server" CssClass="require" Width="130px"
                                DESIGNTIMEDRAGDROP="257"></asp:TextBox>
                        </td>
                        
                        <td class="fieldNameLeft" style="height: 26px" nowrap>
                            <asp:Label ID="lblTLocationCartonEdit" runat="server">目标箱号</asp:Label>
                        </td>
                        <td class="fieldValue" style="height: 26px">
                            <asp:TextBox ID="txtTLocationCartonEdit" runat="server" CssClass="require" Width="130px"></asp:TextBox>
                        </td>
                    <td class="toolBar">
                            <input class="submitImgButton" id="cmdShelves" type="submit" value="上 架" name="cmdShelves"
                                runat="server"  onserverclick="cmdShelves_ServerClick"  />
                        </td>

                    </tr>
                </table>
            </td>
        </tr>
        <tr class="toolBar">
            <td>
                <table class="toolBar">
                    <tr>
             
                    <td class="toolBar" >
                            <input class="submitImgButton" id="cmdSAPDNBack" type="submit" value="系统出库" name="cmdCommit"
                                runat="server" onserverclick="cmdSAPBack_ServerClick" />
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
