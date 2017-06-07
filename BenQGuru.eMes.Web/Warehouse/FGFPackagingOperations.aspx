<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FGFPackagingOperations.aspx.cs"
    Inherits="BenQGuru.eMES.Web.Warehouse.FGFPackagingOperations" %>

<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="cc2" Namespace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<%@ Register TagPrefix="uc1" TagName="eMESDate" Src="~/UserControl/DateTime/DateTime/eMESDate.ascx" %>
<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>FGFPackagingOperations</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="<%=StyleSheet%>" rel="stylesheet">
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
    <table id="table1" height="100%" width="100%" runat="server">
        <tr class="moduleTitle">
            <td>
                <asp:Label ID="lblTitle" runat="server" CssClass="labeltopic">包装作业</asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table class="query" height="100%" width="100%">
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblPickNoQuery" runat="server">拣货任务令号</asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 159px">
                            <asp:TextBox ID="txtPickNoQuery" runat="server" Width="150px" DESIGNTIMEDRAGDROP="257"
                                CssClass="textbox"></asp:TextBox>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblCarInvNoQurey" runat="server">发货箱单号</asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 159px">
                            <asp:TextBox ID="txtCarInvNoQuery" runat="server" Width="150px" DESIGNTIMEDRAGDROP="257"
                                CssClass="textbox" Enabled="false"></asp:TextBox>
                        </td>
                                  <td class="fieldName" nowrap>
                            <asp:Label ID="lblCARTONNOQuery" runat="server">包装箱号</asp:Label>
                        </td>
                        <td class="fieldValue" nowrap>
                            <asp:TextBox ID="txtCARTONNOQuery" runat="server" CssClass="textbox" Width="130px"></asp:TextBox>
                        </td>
                        
                

                        <td style="width: 100%">
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
                    <tr class="moduleTitle">
                        <td>
                            <asp:Label ID="lblPackagingOperations" runat="server" CssClass="labeltopic">包装作业</asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldNameLeft" style="height: 26px" nowrap>
                            <asp:Label ID="lblGFHWItemCode" runat="server">光伏华为编码</asp:Label>
                        </td>
                        <td class="fieldValue" style="height: 26px">
                            <asp:DropDownList ID="ddlGFHWItemCode" runat="server" CssClass="require" Width="150px"
                                DESIGNTIMEDRAGDROP="257" OnSelectedIndexChanged="ddlGFHWItemCode_SelectedIndexChanged"
                                AutoPostBack="true">
                            </asp:DropDownList>
                        </td>
                        <td class="fieldNameLeft" style="height: 26px" nowrap>
                            <asp:Label ID="lblGFPackingSEQ" runat="server">光伏包装序号</asp:Label>
                        </td>
                        <td class="fieldValue" style="height: 26px">
                            <asp:DropDownList ID="ddlGFPackingSEQ" runat="server" CssClass="require" Width="150px"
                                DESIGNTIMEDRAGDROP="257">
                            </asp:DropDownList>
                        </td>
                        <td class="fieldNameLeft" style="height: 26px" nowrap>
                            <asp:Label ID="lblSuiteQTY" runat="server">华为编码数量 </asp:Label>
                        </td>
                        <td class="fieldValue" style="height: 26px">
                            <asp:TextBox ID="txtSuiteQTY" runat="server" CssClass="require" Width="150px" DESIGNTIMEDRAGDROP="257"
                                onKeyPress="if (event.keyCode < 48 || event.keyCode > 57) event.returnValue = false;"></asp:TextBox>
                        </td>
                <%--        <td>
                        </td>
                        <td style="width: 100%">
                        </td>--%>
                                 <td class="fieldNameLeft" style="height: 26px" nowrap>
                            <asp:Label ID="lblPackingCartonNo" runat="server">包装箱号</asp:Label>
                        </td>
                        <td class="fieldValue" style="height: 26px">
                            <asp:TextBox ID="txtCartonNo" runat="server" CssClass="require" Width="150px" DESIGNTIMEDRAGDROP="257"></asp:TextBox>
                        </td>
                 
                    </tr>
                    <tr>
                      
               
                        
                          <td class="fieldNameLeft" style="height: 26px" nowrap>
                            <asp:Label ID="lblDQMaterialNO" runat="server">鼎桥物料编码</asp:Label>
                        </td>
                        <td class="fieldValue" style="height: 26px">
                            <%--<asp:TextBox ID="txtDQMaterialNO" runat="server" CssClass="require" Width="150px" DESIGNTIMEDRAGDROP="257"></asp:TextBox>--%>
                            <asp:DropDownList ID="ddlDQMaterialNO" runat="server" CssClass="require" Width="150px"
                                DESIGNTIMEDRAGDROP="257" AutoPostBack="true" OnSelectedIndexChanged="ddlDQMaterialNO_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        
                               <td class="fieldNameLeft" style="height: 26px" nowrap>
                            <asp:Label ID="lblDQSMCodeEdit" runat="server">鼎桥S编码</asp:Label>
                        </td>
                        <td class="fieldValue" style="height: 26px">
                            <asp:DropDownList ID="drpDQSMCodeEdit" runat="server" CssClass="require" Width="150px">
                            </asp:DropDownList>
                        </td>

                        <td class="fieldNameLeft" style="height: 26px" nowrap>
                            <asp:Label ID="lblQTY" runat="server">数量</asp:Label>
                        </td>
                        <td class="fieldValue" style="height: 26px">
                            <asp:TextBox ID="txtQTY" runat="server" CssClass="require" Width="150px" DESIGNTIMEDRAGDROP="257"
                                onKeyPress="if (event.keyCode < 48 || event.keyCode > 57) event.returnValue = false;"></asp:TextBox>
                        </td>
                        <td class="fieldNameLeft" style="height: 26px" nowrap>
                            <asp:Label ID="lblSNEdit" runat="server">SN</asp:Label>
                        </td>
                        <td class="fieldValue" style="height: 26px">
                            <asp:TextBox ID="txtSNEdit" runat="server" CssClass="require" Width="150px" DESIGNTIMEDRAGDROP="257"></asp:TextBox>
                        </td>
                        <td style="width: 100%">
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
                            <input class="submitImgButton" id="cmdSaveIt" type="submit" value="保 存" name="cmdSave"
                                runat="server" onserverclick="cmdSaveIt_ServerClick" />
                        </td>
                        <td>
                            <input class="submitImgButton" id="cmdPackingFinished" type="submit" value="装箱完成"
                                name="cmdPackingFinished" runat="server" onserverclick="cmdPackingFinished_ServerClick" />
                        </td>
                        <td>
                            <input class="submitImgButton" id="cmdReturn" type="submit" value="返回" name="cmdReturn"
                                runat="server" onserverclick="cmdReturn_ServerClick" visible="false" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
