<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FQueryStoragePickMP.aspx.cs" Inherits="BenQGuru.eMES.Web.WarehouseWeb.FQueryStoragePickMP" %>

<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="cc2" Namespace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<%@ Register TagPrefix="uc1" TagName="eMESDate" Src="~/UserControl/DateTime/DateTime/eMESDate.ascx" %>
<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>FQueryStoragePickMP</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
        <meta content="JavaScript" name="vs_defaultClientScript" />
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="<%=StyleSheet%>" rel="stylesheet">
        <script language="javascript" type="text/javascript">

            function SelectViewField() {
                var result = window.showModalDialog("FViewFieldEP.aspx?defaultUserCode=PickHead_FIELD_LIST_SYSTEM_DEFAULT&table=TBLPICK", "", "dialogWidth:800px;dialogHeight:600px;scroll:none;help:no;status:no");
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
                <asp:Label ID="lblTitle" runat="server" CssClass="labeltopic">库房执行拣货任务令</asp:Label>
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

                     
                                      <td class="fieldValue"  colspan="4"  nowrap>
            <asp:CheckBox ID="chbWaitPickQuery" runat="server" Text="待拣料" type="checkbox"  ></asp:CheckBox>
            <asp:CheckBox ID="chbPickQuery" runat="server" Text="拣料" type="checkbox"  ></asp:CheckBox>
            <asp:CheckBox ID="chbMakePackingListQuery" runat="server" Text="制作箱单" type="checkbox"  ></asp:CheckBox>
            <asp:CheckBox ID="chbPackQuery" runat="server" Text="包装" type="checkbox"  ></asp:CheckBox>
            <asp:CheckBox ID="chbOQCQuery" runat="server" Text="OQC检验" type="checkbox"  ></asp:CheckBox>
            <asp:CheckBox ID="chbClosePackingListQuery" runat="server" Text="箱单完成" type="checkbox"  ></asp:CheckBox>
            <asp:CheckBox ID="chbCloseQuery" runat="server" Text="已出库" type="checkbox"  ></asp:CheckBox>
            <asp:CheckBox ID="chbCancelQuery" runat="server" Text="取消" type="checkbox"  ></asp:CheckBox>
            <asp:CheckBox ID="chbBlockQuery" runat="server" Text="冻结" type="checkbox"  ></asp:CheckBox>
              <asp:CheckBox ID="chbPackingListingQuery" runat="server" Text="制作箱单中" type="checkbox"  ></asp:CheckBox>
                                </td>

                    </tr>
                    
                    
                    <tr>
                               <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblOutStackListQuery" runat="server">出库库位</asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 159px">
                            <asp:DropDownList ID="drpOutStackListQuery" runat="server" CssClass="textbox" Width="150px"
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
                            <asp:Label ID="lblCusBatchNo" runat="server">客户批次号</asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 159px">
                            <asp:TextBox ID="txtCusBatchNo" runat="server" Width="150px" DESIGNTIMEDRAGDROP="257"
                                CssClass="textbox"></asp:TextBox>
                        </td>
                               <td colspan="2" nowrap  width="100%">
                        </td>
                    </tr>
                    <tr>
                     
                       
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblCBDateQuery" runat="server">创建日期开始</asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 159px">
                             <asp:TextBox type="text" id="txtCBDateQuery"  class='datepicker' runat="server"  Width="130px"/>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblCEDateQuery" runat="server">创建日期结束</asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 159px">
                             <asp:TextBox type="text" id="txtCEDateQuery"  class='datepicker' runat="server"  Width="130px"/>
                        </td>
                      
                       <td colspan="2" nowrap  width="100%">
                        </td>
                        <td class="fieldName">
                            <input class="submitImgButton" id="cmdQuery" type="submit" value="查 询" name="btnQuery"
                                runat="server">
                        </td>
                          
                          <td class="fieldValue" nowrap>
                            <a href="#" onclick="SelectViewField(); return false;">
                                <asp:Label runat="server" ID="lblMOSelectMOViewField">选择栏位</asp:Label></a>
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
        

        <tr class="normal" style="display: none">
            <td>
                <table class="edit" height="100%" cellpadding="0" width="100%">
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblPlanSendDateEdit" runat="server">计划发货日期</asp:Label>
                        </td>
                          <td class="fieldValue" style="width: 159px">
                             <asp:TextBox type="text" id="txtPlanSendDateEdit"  class='datepicker' runat="server"  Width="130px"/>
                        </td>
                        <td class="fieldNameLeft" style="width: 107px;" nowrap>
                            <asp:Label ID="lblASNCodeEdit" runat="server">ASN号</asp:Label>
                        </td>
                        
                           <td class="fieldValue" style="height: 26px">
                            <cc2:SelectableTextBox ID="txtASNCodeEdit" runat="server" Type="singleASNSP" Target="FSingleASNSP.aspx"  >
                            </cc2:SelectableTextBox>
                        </td>

                        <td class="fieldNameLeft" style="height: 26px" nowrap>
                            <asp:Label ID="lblMemoEdit" runat="server">备注</asp:Label>
                        </td>
                        <td colspan="3" class="fieldValue" style="height: 26px">
                             <asp:TextBox ID="txtMemoEdit" runat="server" CssClass="textbox" Width="430px"></asp:TextBox>
                        </td>
                        <td width="100%">
                        </td>
                    </tr>
                    <tr style="display: none">
                             <td class="fieldValue" style="width: 159px">
                            <asp:TextBox ID="txtPickNoEdit" runat="server" Width="150px" DESIGNTIMEDRAGDROP="257"
                                CssClass="textbox"></asp:TextBox>
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
                         <input class="submitImgButton" id="cmdInitial" type="submit" value="取消下发" name="cmdInitial"
                                runat="server" onserverclick="cmdInitial_ServerClick" />
                        </td>
            
                        
                    </tr>
                </table>
            </td>
        </tr>
        
         <tr class="toolBar" style="display: none">
                 <td class="toolBar">
                         <input class="submitImgButton" id="cmdSave" type="submit" value="保存" name="cmdInitial"
                                runat="server" />
                                </td>
                        <td class="toolBar">
                            <input class="submitImgButton" id="cmdLotSave" type="submit" value="批量保存" name="cmdInitialCheck"
                                runat="server" onserverclick="cmdLotSave_ServerClick" />
                        </td>
                             <td class="toolBar">
                            <input class="submitImgButton" id="cmdRelease" type="submit" value="下发" name="cmdApplyIQC"
                                   runat="server" onserverclick="cmdRelease_ServerClick" />
                        </td>
       </tr>

    </table>
    </form>
</body>
</html>
