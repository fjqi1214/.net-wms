<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FSQEProcessMP.aspx.cs" Inherits="BenQGuru.eMES.Web.IQC.FSQEProcessMP" %>

<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="cc2" Namespace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<%@ Register TagPrefix="uc1" TagName="eMESDate" Src="~/UserControl/DateTime/DateTime/eMESDate.ascx" %>
<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>FSQEProcessMP</title>
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
                <asp:Label ID="lblTitle" runat="server" CssClass="labeltopic">SQE处理</asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table class="query" height="100%" width="100%">
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblIQCNoQuery" runat="server">IQC检验单</asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 159px">
                            <asp:TextBox ID="txtIQCNoQuery" runat="server" Width="150px" DESIGNTIMEDRAGDROP="257"
                                CssClass="textbox"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblSNQuery" runat="server">SN号</asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 159px">
                            <asp:TextBox ID="txtSNQuery" runat="server" Width="150px" DESIGNTIMEDRAGDROP="257"
                                CssClass="textbox"></asp:TextBox>
                        </td>
                         <td class="fieldName">
                            <input class="submitImgButton" id="cmdCheckSN" type="submit" value="检索" name="cmdCheckSN"
                                runat="server" onserverclick="cmdCheckSN_ServerClick" />
                        </td>
                        <td  nowrap  width="100%">
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
                       <%-- <td class="smallImgButton">
                            <input class="deleteButton" id="cmdDelete" type="submit" value="  " name="cmdDelete"
                                runat="server">
                        </td>--%>
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
                        <td class="fieldNameLeft" style="height: 26px" nowrap>
                            <asp:Label ID="lblProcessWayEdit" runat="server">处理方式</asp:Label>
                        </td>
                        <td class="fieldValue" style="height: 26px">
                              <asp:DropDownList ID="drpProcessWayEdit" runat="server" CssClass="require" Width="120px"
                                DESIGNTIMEDRAGDROP="94">
                            </asp:DropDownList>
                        </td>
                        <td class="fieldNameLeft" style="height: 26px" nowrap>
                            <asp:Label ID="lblMemoEdit" runat="server">备注</asp:Label>
                        </td>
                        <td class="fieldValue" style="height: 26px">
                            <asp:TextBox ID="txtMemoEdit" runat="server" CssClass="textbox" Width="120px"></asp:TextBox>
                        </td>
                        <td class="fieldNameLeft" style="height: 26px" nowrap>
                            <input class="submitImgButton" id="cmdConfirm" type="submit" value="确认" name="cmdConfirm"
                                runat="server" onserverclick="cmdConfirm_ServerClick" />
                        </td>
                          <td class="fieldNameLeft" style="height: 26px" nowrap>
                           <input class="submitImgButton" id="cmdCommit" type="submit" value="提交" name="cmdCommit"
                                            runat="server" onserverclick="cmdCommit_ServerClick"/>
                         </td>
                        <td class="fieldNameLeft"  style="height: 26px" nowrap>
                            <input class="submitImgButton" id="cmdReturn" type="submit" value="返 回" name="cmdReturn" runat="server" onserverclick="cmdReturn_ServerClick" />
                        </td>
                              <td class="fieldValue" style="height: 26px" nowrap>
                           
                             <input class="DefaultInput"  id="cmdExportIQCACL" type="submit" value="导出IQC异常联络单" name="cmdExportIQCACL" 
                                            runat="server" onserverclick="cmdExportIQCACL_ServerClick"/>
                        </td>
                        <td class="toolBar">
                            <input class="submitImgButton" id="cmdRejectbt" type="submit" value="拒收" name="cmdRejectbt"
                                runat="server" onserverclick="cmdRejectbt_ServerClick">
                        </td>
                        <td class="toolBar">
                            <input class="submitImgButton" id="cmdReturned2Inspection" type="submit" value="退回二次检验" name="cmdReturned2Inspection"
                                runat="server" onserverclick="cmdReturned2Inspection_ServerClick">
                        </td>
                        <td class="toolBar">
                            <input class="submitImgButton" id="cmdReturnedQC" type="submit" value="退回QC重检" name="cmdReturnedQC"
                                runat="server"  onserverclick="cmdReturnedQC_ServerClick">
                        </td>
                        <td  style="width: 100%">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
