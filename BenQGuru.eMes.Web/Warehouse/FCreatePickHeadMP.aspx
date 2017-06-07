<%@ Page Language="c#" CodeBehind="FCreatePickHeadMP.aspx.cs" AutoEventWireup="True" Inherits="BenQGuru.eMES.Web.Warehouse.FCreatePickHeadMP" %>

<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="uc1" TagName="eMESDate" Src="../UserControl/DateTime/DateTime/eMESDate.ascx" %>
<%@ Register TagPrefix="cc2" Namespace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>FCreatePickHeadMP</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR" />
    <meta content="C#" name="CODE_LANGUAGE" />
    <meta content="JavaScript" name="vs_defaultClientScript" />
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"/> 
    <link href="<%=StyleSheet%>" rel="stylesheet" />
    <script language="javascript" type="text/javascript">

        function SelectViewField() {
            var result = window.showModalDialog("FViewFieldEP.aspx?defaultUserCode=PickHead_FIELD_LIST_SYSTEM_DEFAULT&table=TBLPICK", "", "dialogWidth:800px;dialogHeight:600px;scroll:none;help:no;status:no");
            if (result == "OK") {
                window.location.href = window.location.href;
            }
        }
    </script>
</head>
<body>
    <form id="Form1" method="post" runat="server">
    <table id="Table1" height="100%" width="100%" runat="server">
        <tr class="moduleTitle">
            <td>
                <asp:Label ID="lblTitle" runat="server" CssClass="labeltopic">手工创建拣货任务令头</asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table class="query" height="100%" width="100%">
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblPickNoQuery" runat="server">拣货任务令号</asp:Label>
                        </td>
                        <td class="fieldValue" nowrap>
                            <asp:TextBox ID="txtPickNoQuery" runat="server" CssClass="textbox" Width="130px"></asp:TextBox>
                        </td>
                       
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblInvNoQuery" runat="server">SAP单据号</asp:Label>
                        </td>
                        <td class="fieldValue" nowrap>
                            <asp:TextBox ID="txtInvNoQuery" runat="server" CssClass="textbox" Width="130px"></asp:TextBox>
                        </td>
                            <td class="fieldValue"  colspan="2"  nowrap>
            <asp:CheckBox ID="chbReleaseQuery" runat="server" Text="初始化" type="checkbox"  ></asp:CheckBox>
            <asp:CheckBox ID="chbWaitPickQuery" runat="server" Text="待拣料" type="checkbox"  ></asp:CheckBox>
            <asp:CheckBox ID="chbPickQuery" runat="server" Text="拣料" type="checkbox"  ></asp:CheckBox>
            <asp:CheckBox ID="chbMakePackingListQuery" runat="server" Text="制作箱单" type="checkbox"  ></asp:CheckBox>
            <asp:CheckBox ID="chbPackQuery" runat="server" Text="包装" type="checkbox"  ></asp:CheckBox>
            <asp:CheckBox ID="chbOQCQuery" runat="server" Text="OQC检验" type="checkbox"  ></asp:CheckBox>
            <asp:CheckBox ID="chbClosePackingListQuery" runat="server" Text="箱单完成" type="checkbox"  ></asp:CheckBox>
            <asp:CheckBox ID="chbCloseQuery" runat="server" Text="已出库" type="checkbox"  ></asp:CheckBox>
            <asp:CheckBox ID="chbCancelQuery" runat="server" Text="取消" type="checkbox"  ></asp:CheckBox>
            <asp:CheckBox ID="chbBlockQuery" runat="server" Text="冻结" type="checkbox"  ></asp:CheckBox>
                                </td>
                                

                        <td nowrap width="100%">
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblPickTypeQuery" runat="server">单据类型</asp:Label>
                        </td>
                        <td class="fieldValue" nowrap>
                             <asp:DropDownList ID="drpPickTypeQuery" runat="server" CssClass="textbox" Width="130px"
                                DESIGNTIMEDRAGDROP="94">
                            </asp:DropDownList>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblStorageQuery" runat="server">库位</asp:Label>
                        </td>
                        <td class="fieldValue" nowrap>
                            <asp:DropDownList ID="drpStorageQuery" runat="server" CssClass="textbox" Width="130px"
                                DESIGNTIMEDRAGDROP="94">
                            </asp:DropDownList>
                        </td>
                        <td nowrap width="100%">
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblCDateQuery" runat="server">创建日期</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox type="text" ID="dateInDateFromQuery" class='datepicker' runat="server"
                                Width="100px" />
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblInDateToQuery" runat="server"> 到</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox type="text" ID="dateInDateToQuery" class='datepicker' runat="server"
                                Width="100px" />
                        </td>
                        
                                          <td class="fieldValue"  colspan="2"  nowrap>
                    <asp:CheckBox ID="chbIsExpressDelegate" runat="server" Text="Release" type="checkbox"   ></asp:CheckBox>
                     </td>
                        <td class="fieldValue" nowrap>
                            <a href="#" onclick="SelectViewField(); return false;">
                                <asp:Label runat="server" ID="lblMOSelectMOViewField">选择栏位</asp:Label></a>
                        </td>
                        <td  class="fieldName" nowrap>
                            <input class="submitImgButton" id="cmdQuery" type="submit" value="查 询" name="btnQuery"
                                runat="server"/>
                        </td>
                        
                        <td colspan="4" nowrap width="100%">
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
                            <input class="gridExportButton" id="cmdGridExport" type="submit" value="  " name="cmdCancel"
                                runat="server" />
                        </td>
                        <td class="smallImgButton">
                            <input class="deleteButton" id="cmdDelete" type="submit" value="  " name="cmdDelete"
                                runat="server" />
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
                        <td class="fieldNameLeft" style="height: 26px" nowrap>
                            <asp:Label ID="lblPickNoEdit" runat="server">拣货任务令号</asp:Label>
                        </td>
                        <td class="fieldValue" style="height: 26px">
                            <asp:TextBox ID="txtPickNoEdit" runat="server" CssClass="require" Width="130px" ReadOnly="True"></asp:TextBox>                              
                        </td>
                        <td class="fieldNameLeft" style="height: 26px">
                             <input class="submitImgButton" id="cmdCreateNo" type="submit" value="生产单号" name="cmdCreateNo"
                                runat="server" onserverclick="cmdCreateNo_ServerClick"  />
                        </td>
                        
                        <td class="fieldNameLeft" style="height: 26px" nowrap>
                            <asp:Label ID="lblInvNoTypeEdit" runat="server">单据类型</asp:Label>
                        </td>
                        <td class="fieldValue" style="height: 26px">
                            <asp:DropDownList ID="drpInvNoTypeEdit" runat="server" CssClass="require" Width="130px"
                                DESIGNTIMEDRAGDROP="94" AutoPostBack="True" OnSelectedIndexChanged="drpInvNoTypeEdit_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td class="fieldNameLeft" style="height: 26px" nowrap>
                            <asp:Label ID="lblInvNoEdit" runat="server">SAP单据号</asp:Label>
                        </td>
                        <td class="fieldValue" style="height: 26px">
                             <cc2:SelectableTextBox ID="txtInvNoEdit" runat="server" CssClass="require"
                                Width="130px" Type="singleInvNoSP" Target="FSingleInvNoSP.aspx" AutoPostBack="True" ></cc2:SelectableTextBox>
                        </td>

                          <td class="fieldNameLeft" style="height: 26px" nowrap>
                            <asp:Label ID="lblCostCenterEdit" runat="server">成本中心</asp:Label>
                        </td>
                        <td class="fieldValue" style="height: 26px">
                            <asp:TextBox ID="txtCostCenterEdit" runat="server" CssClass="textbox" Width="130px"></asp:TextBox>
                        </td>
                           <td style="width: 100%">
                        </td>
                    </tr>
                     <tr>
                        <td class="fieldNameLeft"  style="height: 26px" nowrap>
                            <asp:Label ID="lblStorageOutEdit" runat="server">出库库位</asp:Label>
                        </td>
                        <td class="fieldValue" style="height: 26px">
                             <asp:DropDownList ID="drpStorageOutEdit" runat="server" CssClass="require" Width="130px"
                                DESIGNTIMEDRAGDROP="94">
                            </asp:DropDownList>
                        </td>
                        <td></td>
                        <td class="fieldNameLeft" style="height: 26px" nowrap>
                            <asp:Label ID="lblReceiverUserEdit" runat="server">收货人</asp:Label>
                        </td>
                        <td class="fieldValue" style="height: 26px">
                            <asp:TextBox ID="txtReceiverUserEdit" runat="server" CssClass="textbox" Width="130px"></asp:TextBox>
                        </td>
                          <td class="fieldNameLeft" style="height: 26px" nowrap>
                            <asp:Label ID="lblReceiverAddrEdit" runat="server">收货地址</asp:Label>
                        </td>
                        <td class="fieldValue" style="height: 26px">
                            <asp:TextBox ID="txtReceiverAddrEdit" runat="server" CssClass="textbox" Width="130px"></asp:TextBox>
                        </td>
                        <td style="width: 100%" colspan="3">
                        </td>
                    </tr>
                     <tr>
                        <td class="fieldNameLeft" style="height: 26px" nowrap>
                            <asp:Label ID="lblPlanDateEdit"  runat="server">计划发货时间</asp:Label>
                        </td>
                        <td class="fieldValue"  style="height: 26px">
                           <asp:TextBox type="text" ID="txtPlanDateEdit" class='datepicker' runat="server" Width="100px" />
                        </td>
                        <td></td>
                        <td class="fieldNameLeft" style="height: 26px" nowrap>
                            <asp:Label ID="lblMemoEdit" runat="server">备注</asp:Label>
                        </td>
                        <td colspan="3" class="fieldValue" style="height: 26px">
                             <asp:TextBox ID="txtMemoEdit" runat="server" CssClass="textbox" Width="430px"></asp:TextBox>
                        </td>
                           <td style="width: 100%" colspan="3">
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
                            <input language="javascript" class="submitImgButton" id="cmdAdd" type="submit"
                                value="新 增" name="cmdAdd" runat="server" designtimedragdrop="147" >
                        </td>
                        
                         <td class="toolBar">
                         <input class="submitImgButton" id="cmdSave" type="submit" value="保存" name="cmdInitial"
                                runat="server" />
                                </td>
                                  <td class="toolBar">
                         <input class="submitImgButton" id="cmdCancel" type="submit" value="取消" name="cmdCancel"
                                runat="server" />
                                </td>
                        <td class="toolBar">
                            <input class="submitImgButton" id="cmdLotSave" type="submit" value="批量保存" name="cmdInitialCheck"
                                runat="server" onserverclick="cmdLotSave_ServerClick" />
                        </td>

                        <td class="toolBar">
                            <input language="javascript" class="submitImgButton" id="cmdRelease" type="submit"
                                value="下 发" name="cmdRelease" runat="server" designtimedragdrop="168" onserverclick="cmdRelease_ServerClick" >
                        </td>
                        <td class="toolBar">
                            <input language="javascript" class="submitImgButton" id="cmdInitial" type="submit"
                                value="取消下发" name="cmdAdd" runat="server" onserverclick="cmdInitial_ServerClick"  >
                        </td>
                        
                    </tr>
                    
                    <tr style="display: none">
                        <td class="toolBar">
                            <input language="javascript" class="submitImgButton" id="cmdPending" type="submit"
                                value="暂 停" name="cmdAdd" runat="server" designtimedragdrop="228" >
                        </td>
                        <td class="toolBar">
                            <input language="javascript" class="submitImgButton" id="cmdAnnulPending" type="submit"
                                value="取消暂停" name="cmdAdd" runat="server" designtimedragdrop="170">
                        </td>
                        <td class="toolBar">
                            <input language="javascript" class="submitImgButton" id="cmdCloseMO" type="submit"
                                value="关 单" name="cmdAdd" runat="server" >
                        </td>
                        <td class="toolBar">
                            <input class="submitImgButton" id="cmdExport" type="submit" value="导出" name="cmdExport"
                                runat="server" >
                        </td>
                        <td class="toolBar">
                            <input class="submitImgButton" id="cmdEnter" type="submit" value="导 入" name="cmdEnter"
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
