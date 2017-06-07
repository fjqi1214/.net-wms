<%@ Page Language="c#" CodeBehind="FOQCLotSampleQP.aspx.cs" AutoEventWireup="True"
    Inherits="BenQGuru.eMES.Web.WebQuery.FOQCLotSampleQP" %>

<%@ Register TagPrefix="uc1" TagName="eMesTime" Src="../UserControl/DateTime/DateTime/eMesTime.ascx" %>
<%@ Register TagPrefix="uc1" TagName="eMesDate" Src="../UserControl/DateTime/DateTime/eMesDate.ascx" %>
<%@ Register TagPrefix="cc2" Namespace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>FINNOInfoQP</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="<%=StyleSheet%>" rel="stylesheet">
    <script language="javascript">
        function onCheckBoxChange(cb) {
            if (cb.id == "cbdetail") {
                document.all.cbsample.checked = false;
            }
            else if (cb.id == "cbsample") {
                document.all.cbdetail.checked = false;
            }
        }
    </script>
</head>
<body scroll="yes" ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
    <table id="Table1" height="100%" width="100%" runat="server">
        <tbody>
            <tr class="moduleTitle">
                <td style="height: 19px">
                    <asp:Label ID="lblTitle" runat="server" CssClass="labeltopic">FQC抽检明细查询</asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <table class="query" id="Table2" height="100%" width="100%">
                        <tbody>
                            <tr>
                                <td class="fieldNameLeft" nowrap>
                                    <asp:Label ID="lblItemCodeQuery" runat="server">产品代码</asp:Label>
                                </td>
                                <td class="fieldValue">
                                    <cc2:selectabletextbox id="txtConditionItem" runat="server" width="90px" type="item"
                                        cankeyin="true">
                                    </cc2:selectabletextbox>
                                </td>
                                <td class="fieldName" nowrap>
                                    <asp:Label ID="lblMOIDQuery" runat="server">工单代码</asp:Label>
                                </td>
                                <td class="fieldValue">
                                    <cc2:selectabletextbox id="txtConditionMo" runat="server" width="90px" type="mo"
                                        cankeyin="true">
                                    </cc2:selectabletextbox>
                                </td>
                                <td class="fieldNameLeft" nowrap>
                                    <asp:Label ID="lblOQCLotQuery" runat="server">送检批号</asp:Label>
                                </td>
                                <td class="fieldValue">
                                    <cc2:selectabletextbox id="txtOQCLotQuery" runat="server" width="90px" type="lot"
                                        cssclass="textbox" cankeyin="true">
                                    </cc2:selectabletextbox>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td class="fieldNameLeft" nowrap>
                                    <asp:Label ID="lblStartSNQuery" runat="server">起始产品序列号</asp:Label>
                                </td>
                                <td class="fieldValue">
                                    <asp:TextBox ID="txtStartSnQuery" runat="server" CssClass="textbox" Width="130px"></asp:TextBox>
                                </td>
                                <td class="fieldName" nowrap>
                                    <asp:Label ID="lblEndSNQuery" runat="server">截止产品序列号</asp:Label>
                                </td>
                                <td class="fieldValue">
                                    <asp:TextBox ID="txtEndSnQuery" runat="server" CssClass="textbox" Width="130px"></asp:TextBox>
                                </td>
                                <td class="fieldNameLeft" nowrap>
                                    <asp:Label ID="lblReWorkMO" runat="server">返工需求单号</asp:Label>
                                </td>
                                <td class="fieldValue">
                                    <asp:TextBox ID="txtReWorkMOQuery" runat="server" CssClass="textbox" Width="130"></asp:TextBox>
                                </td>
                                <td class="fieldNameLeft" style="display: none" nowrap>
                                    <asp:Label ID="lblSSCode" runat="server">产线</asp:Label>
                                </td>
                                <td style="display: none">
                                    <cc2:selectabletextbox id="txtSSCode" runat="server" width="90px" type="stepsequence"
                                        target="stepsequence">
                                    </cc2:selectabletextbox>
                                </td>
                            </tr>
                            <tr>
                                <td class="fieldName" nowrap>
                                    <asp:Label ID="lblOQCLotType" runat="server">批类型</asp:Label>
                                </td>
                                <td class="fieldValue">
                                    <cc2:selectabletextbox id="txtOQCLotTypeQuery" runat="server" type="oqclottype" readonly="false"
                                        cankeyin="true" width="130px">
                                    </cc2:selectabletextbox>
                                </td>
                                <td class="fieldName" nowrap>
                                    <asp:Label ID="lblOQCMUserRQuery" runat="server">判定人</asp:Label>
                                </td>
                                <td class="fieldValue">
                                    <asp:TextBox ID="txtOQCMUserRQuery" runat="server" CssClass="textbox" Width="130px"></asp:TextBox>
                                </td>
                                <td class="fieldNameLeft" nowrap>
                                    <asp:Label ID="lblBigSSCodeWhere" runat="server">大线</asp:Label>
                                </td>
                                <td class="fieldValue">
                                    <cc2:selectabletextbox id="txtBigSSCodeWhere" runat="server" type="bigline" readonly="false"
                                        cankeyin="true" width="130px">
                                    </cc2:selectabletextbox>
                                </td>
                            </tr>
                            <tr>
                                <td class="fieldName" nowrap>
                                    <asp:Label ID="lblMaterialModelCodeWhere" runat="server">机型</asp:Label>
                                </td>
                                <td class="fieldValue">
                                    <cc2:selectabletextbox id="txtMaterialModelCodeWhere" runat="server" type="mmodelcode"
                                        readonly="false" cankeyin="true" width="130px">
                                    </cc2:selectabletextbox>
                                </td>
                                <td class="fieldName" nowrap>
                                    <asp:Label ID="lblCrewCodeQuery" runat="server">班组</asp:Label>
                                </td>
                                <td class="fieldValue">
                                    <asp:DropDownList ID="drpCrewCodeQuery" runat="server" Width="130px">
                                    </asp:DropDownList>
                                </td>
                                <td class="fieldName" nowrap>
                                    <asp:Label ID="lblProductionType" runat="server">生产类型</asp:Label>
                                </td>
                                <td class="fieldValue">
                                    <cc2:selectabletextbox id="txtProductionTypeQuery" runat="server" type="productiontype"
                                        readonly="false" cankeyin="true" width="130px">
                                    </cc2:selectabletextbox>
                                </td>
                            </tr>
                            <tr>
                                <td class="fieldName" nowrap>
                                    <asp:Label ID="lblGenerateLotBegdate" runat="server">产生批起始日期</asp:Label>
                                </td>
                                <td class="fieldValue" nowrap>
                                    <table cellspacing="0" cellpadding="0" border="0">
                                        <tr>
                                            <td>
                                            <asp:TextBox  id="txtOQCBeginDate"  class='datepicker require' runat="server"  Width="110px"/>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td class="fieldName" nowrap>
                                    <asp:Label ID="lblGenerateLotEnddate" runat="server">产生批结束日期</asp:Label>
                                </td>
                                <td class="fieldValue" nowrap>
                                    <table cellspacing="0" cellpadding="0" border="0">
                                        <tr>
                                            <td>
                                            <asp:TextBox  id="txtOQCEndDate"  class='datepicker require' runat="server"  Width="110px"/>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td class="fieldName" nowrap>
                                    <asp:Label ID="lblGenerateLotdate" runat="server" Visible="false">产生批日期</asp:Label>
                                </td>
                                <td class="fieldName">
                                    <input class="submitImgButton" id="cmdQuery" type="submit" value="查 询" name="btnQuery"
                                        runat="server">
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
            <tr height="100%">
                <td class="fieldGrid">
                    <ig:webdatagrid id="gridWebGrid" runat="server" width="100%">
                    </ig:webdatagrid>
                </td>
            </tr>
            <tr class="normal">
                <td>
                    <table id="Table3" height="100%" cellpadding="0" width="100%">
                        <tr>
                            <td width="140">
                                <cc1:pagersizeselector id="pagerSizeSelector" runat="server">
                                </cc1:pagersizeselector>
                            </td>
                            <td class="smallImgButton">
                                <input class="gridExportButton" id="cmdGridExport" type="submit" value="  " name="cmdGridExport"
                                    runat="server">
                                
                            </td>
                            <td align="right">
                                <cc1:pagertoolbar id="pagerToolBar" runat="server">
                                </cc1:pagertoolbar>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </tbody>
    </table>
    </form>
</body>
</html>
