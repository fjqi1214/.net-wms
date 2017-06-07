<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FCreatePickCommand.aspx.cs"
    Inherits="BenQGuru.eMES.Web.WarehouseWeb.FCreatePickCommand" %>

<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="cc2" Namespace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<%@ Register TagPrefix="uc1" TagName="eMESDate" Src="~/UserControl/DateTime/DateTime/eMESDate.ascx" %>
<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>FExecuteASNMP</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="<%=StyleSheet%>" rel="stylesheet">

    <script src="js/jquery-1.3.2.min.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">

        function SelectViewField() {
            var result = window.showModalDialog("FViewFieldEP.aspx?defaultUserCode=PickHead_FIELD_LIST_SYSTEM_DEFAULT&table=TBLCPICK", "", "dialogWidth:800px;dialogHeight:600px;scroll:none;help:no;status:no");
            if (result == "OK") {
                window.location.href = window.location.href;
            }
        }
    </script>

</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
    <table id="table1" height="100%" width="100%" runat="server">
        <tr class="moduleTitle">
            <td>
                <asp:Label ID="lblTitle" runat="server" CssClass="labeltopic">仓库执行入库指令</asp:Label>
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
                            <asp:Label ID="lblPickTypeQuery" runat="server">单据类型</asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 159px">
                            <asp:DropDownList ID="drpPickTypeQuery" runat="server" CssClass="textbox" Width="150px"
                                DESIGNTIMEDRAGDROP="94">
                            </asp:DropDownList>
                        </td>

                          <td colspan="8">
                            <table>
                                <tr>
                                    <td>
                                   
                                        <asp:CheckBox runat="server" GroupName="status" ID="rdoRelease1" Text="初始化" />
                                    </td>
                                    <td>
                                        <asp:CheckBox runat="server" GroupName="status" ID="rdoWaitPick" Text="待拣料" />
                                    </td>
                                    <td>
                                        <asp:CheckBox runat="server" GroupName="status" ID="rdoPick" Text="拣料" />
                                    </td>
                                    <td>
                                        <asp:CheckBox runat="server" GroupName="status" ID="rdoMakePackingList" Text="制作箱单" />
                                    </td>
                                    <td>
                                        <asp:CheckBox runat="server" GroupName="status" ID="rdoPack" Text="包装" />
                                    </td>
                                    <td>
                                        <asp:CheckBox runat="server" GroupName="status" ID="rdoOQC" Text="OQC检验" />
                                    </td>
                                    <td>
                                        <asp:CheckBox runat="server" GroupName="status" ID="rdoClosePackingList" Text="箱单完成" />
                                    </td>
                                    <td>
                                        <asp:CheckBox runat="server" GroupName="status" ID="rdoClose1" Text="已出库" />
                                    </td>
                                    <td>
                                        <asp:CheckBox runat="server" GroupName="status" ID="rdoCancel1" Text="取消" />
                                    </td>
                                    <td>
                                        <asp:CheckBox runat="server" GroupName="status" ID="rdoBlock" Text="冻结" />
                                    </td>
                                      <td>
                                        <asp:CheckBox runat="server" GroupName="status" ID="rdoPackingListing" Text="制作箱单中" />
                                    </td>
                                </tr>
                            </table>
                        </td>


                          </tr>
                            <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblStorageCodeQuery" runat="server">库位</asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 159px">
                            <asp:DropDownList ID="drpStorageCodeQuery" runat="server" CssClass="textbox" Width="150px"
                                DESIGNTIMEDRAGDROP="94">
                            </asp:DropDownList>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblInvNoQuery" runat="server">SAP单据号</asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 159px">
                            <asp:TextBox ID="txtInvNoQuery" runat="server" Width="150px" DESIGNTIMEDRAGDROP="257"
                                CssClass="textbox"></asp:TextBox>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                        </td>
                        <td class="fieldValue" style="width: 159px">
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblCBDateQuery" runat="server">创建日期开始</asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 159px">
                            <asp:TextBox type="text" ID="txtCBDateQuery" class='datepicker' runat="server" Width="130px" />
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblCEDateQuery" runat="server">创建日期结束</asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 159px">
                            <asp:TextBox type="text" ID="txtCEDateQuery" class='datepicker' runat="server" Width="130px" />
                        </td>
                          <td class="fieldValue" nowrap>
                            <a href="#" onclick="SelectViewField(); return false;">
                                <asp:Label runat="server" ID="lblMOSelectMOViewField">选择栏位</asp:Label></a>
                        </td>

                        <td colspan="2" nowrap width="100%">
                        </td>
                        <td class="fieldName">
                            <input class="submitImgButton" id="cmdQuery" type="submit" value="查 询" name="btnQuery"
                                runat="server">
                        </td>
                        <td>
                        </td>
                      
                    </tr>
                    <tr>
                      
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
                                runat="server" onserverclick="cmdDelete_ServerClick" />
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
                <table>
                    <tr>
                        <td class="fieldNameLeft" style="height: 26px" nowrap>
                            <asp:Label ID="lblPickNoEdit" runat="server">拣货任务令号</asp:Label>
                        </td>
                        <td class="fieldValue" style="height: 26px">
                            <asp:TextBox ID="txtPickNoEdit" runat="server" CssClass="require" Width="130px" Enabled="false"></asp:TextBox>
                            <asp:Button runat="server" ID="Gener" Text="产生单号" OnClick="Gener_ServerClick" />
                        </td>
                        <td>
                            <asp:Label ID="lblPickTypeEdit" runat="server">单据类型</asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="drpPickTypeEdit" runat="server" CssClass="textbox" Width="150px"
                                DESIGNTIMEDRAGDROP="94">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Label ID="lblInvNoEdit" runat="server">SAP单据号</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtInvNoEdit" runat="server" CssClass="require" Width="130px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblStorageCodeEdit" runat="server">库位</asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="drpStorageCodeEdit" runat="server" CssClass="textbox" Width="150px"
                                DESIGNTIMEDRAGDROP="94">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Label ID="lblReceiverUserEdit" runat="server">收货人</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtReceiverUserEdit" runat="server" CssClass="require" Width="130px"></asp:TextBox>
                        </td>
                        <td class="fieldNameLeft" style="height: 26px" nowrap>
                            <asp:Label ID="lblReceiverAddrEdit" runat="server">拣货任务令号</asp:Label>
                        </td>
                        <td class="fieldValue" style="height: 26px">
                            <asp:TextBox ID="txtReceiverAddrEdit" runat="server" CssClass="require" Width="130px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblPlanDateEdit" runat="server">计划发货日期</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox type="text" ID="txtPlanDateEdit" class='datepicker' runat="server" Width="130px" />
                        </td>
                        <td>
                            <asp:Label ID="lblREMARKEdit" runat="server">备注</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtREMARKEdit" runat="server" CssClass="require" Width="130px"></asp:TextBox>
                        </td>
                        <td class="fieldNameLeft" style="height: 26px" nowrap>
                        </td>
                        <td class="fieldValue" style="height: 26px">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="toolBar">
            <td class="fieldNameLeft" style="text-align: left;">
                <table class="toolBar">
                    <tr>
                        <td class="toolBar">
                            <input class="submitImgButton" id="cmdAdd" type="submit" value="新增" name="cmdInitial"
                                runat="server" />
                        </td>
                        <td class="toolBar">
                            <input class="submitImgButton" id="cmdSave" type="submit" value="保存" name="cmdInitialCheck"
                                runat="server" />
                        </td>
                          <td class="toolBar">
                            <input language="javascript" class="submitImgButton" id="cmdRelease" type="submit"
                                value="下 发" name="cmdRelease" runat="server" designtimedragdrop="168" onserverclick="cmdRelease_ServerClick" >
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
