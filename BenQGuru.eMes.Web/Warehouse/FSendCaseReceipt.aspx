<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FSendCaseReceipt.aspx.cs"
    Inherits="BenQGuru.eMES.Web.WarehouseWeb.FSendCaseReceipt" %>

<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="uc1" TagName="eMESDate" Src="../UserControl/DateTime/DateTime/eMESDate.ascx" %>
<%@ Register TagPrefix="cc2" Namespace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>FManuallyCreatePickMP</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR" />
    <meta content="C#" name="CODE_LANGUAGE" />
    <meta content="JavaScript" name="vs_defaultClientScript" />
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
    <link href="<%=StyleSheet%>" rel="stylesheet" />

    <script src="js/jquery-1.3.2.min.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">

        function SelectViewField() {
            var result = window.showModalDialog("FViewFieldEP.aspx?defaultUserCode=PickLine_FIELD_LIST_SYSTEM_DEFAULT&table=TBLPICK", "", "dialogWidth:800px;dialogHeight:600px;scroll:none;help:no;status:no");
            if (result == "OK") {
                window.location.href = window.location.href;
            }
        }
    </script>

    <style type="text/css">
        .style1
        {
            height: 26px;
            width: 300px;
        }
    </style>

</head>
<body>
    <form id="Form1" method="post" name="Form1" runat="server">
    <table id="Table1" height="100%" width="100%" runat="server">
        <tr class="moduleTitle">
            <td>
                <asp:Label ID="lblFSendCaseReceipt" runat="server" CssClass="labeltopic">包装/发货箱单</asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table class="query" height="100%" width="100%">
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblCARINVNOQuery" runat="server">发货箱单号</asp:Label>
                        </td>
                        <td class="fieldValue" nowrap>
                            <asp:TextBox ID="txtCARINVNOQuery" name="txtCARINVNOQuery" runat="server" CssClass="require"
                                Width="130px"></asp:TextBox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblPickNoQuery" runat="server">拣货任务令号</asp:Label>
                        </td>
                        <td class="fieldValue" nowrap>
                            <asp:TextBox ID="txtPickNoQuery" name="txtPickNoQuery" runat="server" CssClass="textbox"
                                Width="130px"></asp:TextBox>
                        </td>
                        
                           <td class="fieldName" nowrap>
                            <asp:Label ID="lblCARTONNOQuery" runat="server">包装箱号</asp:Label>
                        </td>
                        <td class="fieldValue" nowrap>
                            <asp:TextBox ID="txtCARTONNOQuery" runat="server" CssClass="textbox" Width="130px"></asp:TextBox>
                        </td>

                        <td class="fieldName" nowrap>
                            <asp:Label ID="LblPickTypeQuery" runat="server">拣货任务类型</asp:Label>
                        </td>
                        <td class="fieldValue" nowrap>
                            <asp:DropDownList ID="drpPickTypeQuery" name="drpPickTypeQuery" AutoPostBack="false"
                                runat="server" CssClass="require" Width="120px" OnSelectedIndexChanged="drpStorageInTypeEdit_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td class="fieldName" nowrap>
                            <input class="submitImgButton" id="cmdQuery" type="submit" value="查 询" name="btnQuery"
                                runat="server" />
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblProjectName" runat="server">项目名称</asp:Label>
                        </td>
                        <td class="fieldValue" nowrap>
                            <asp:TextBox ID="txtItemNameQuery" name="txtItemNameQuery" runat="server" CssClass="require"
                                Width="130px"></asp:TextBox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblContractCode" runat="server">合同号</asp:Label>
                        </td>
                        <td class="fieldValue" nowrap>
                            <asp:TextBox ID="txtOrderNoQuery" name="txtOrderNoQuery" runat="server" CssClass="textbox"
                                Width="130px"></asp:TextBox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblCreateDate" runat="server">创建日期</asp:Label>
                        </td>
                        <td class="fieldValue" nowrap>
                            <asp:TextBox type="text" ID="txtCBDateQuery" name="txtCBDateQuery" class='datepicker'
                                runat="server" Width="130px" />
                        </td>
                        <td class="fieldName" nowrap style="text-align: left;">
                            <asp:Label ID="lblTo" runat="server">至</asp:Label>
                        </td>
                        <td class="fieldValue" nowrap style="text-align: left;">
                            <asp:TextBox type="text" ID="txtCEDateQuery" name="txtCEDateQuery" class='datepicker'
                                runat="server" Width="130px" />
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldName" colspan="4" nowrap>
                            <asp:CheckBox ID="chbReleaseQuery" runat="server" Text="初始化" type="checkbox"></asp:CheckBox>
                            <asp:CheckBox ID="chbWaitPickQuery" runat="server" Text="待拣料" type="checkbox"></asp:CheckBox>
                            <asp:CheckBox ID="chbPickQuery" runat="server" Text="拣料" type="checkbox"></asp:CheckBox>
                            <asp:CheckBox ID="chbMakePackingListQuery" runat="server" Text="制作箱单" type="checkbox">
                            </asp:CheckBox>
                            <asp:CheckBox ID="chbPackQuery" runat="server" Text="包装" type="checkbox"></asp:CheckBox>
                            <asp:CheckBox ID="chbOQCQuery" runat="server" Text="OQC检验" type="checkbox"></asp:CheckBox>
                            <asp:CheckBox ID="chbClosePackingListQuery" runat="server" Text="箱单完成" type="checkbox">
                            </asp:CheckBox>
                            <asp:CheckBox ID="chbCloseQuery" runat="server" Text="已出库" type="checkbox"></asp:CheckBox>
                            <asp:CheckBox ID="chbCancelQuery" runat="server" Text="取消" type="checkbox"></asp:CheckBox>
                            <asp:CheckBox ID="chbBlockQuery" runat="server" Text="冻结" type="checkbox"></asp:CheckBox>
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
                        <td>
                            <table>
                                <td class="fieldNameLeft" style="height: 26px" nowrap>
                                    <asp:Label ID="lblWeight" runat="server">毛重</asp:Label>
                                </td>
                                <td class="fieldValue" style="height: 26px">
                                    <asp:TextBox ID="txtRoughWeight" runat="server" CssClass="require" Width="130px"></asp:TextBox>
                                </td>
                                <td class="fieldNameLeft" style="height: 26px" nowrap>
                                    <asp:Label ID="lblVol" runat="server">体积</asp:Label>
                                </td>
                                <td class="fieldValue" style="height: 26px">
                                    <asp:TextBox ID="txtVol" runat="server" CssClass="require" Width="130px"></asp:TextBox>
                                </td>
                                <td class="fieldNameLeft" style="height: 26px" nowrap>
                                    <input class="submitImgButton" id="cmdSave" type="submit" value="保存" name="cmdSave"
                                        runat="server" onserverclick="cmdSave_ServerClick">
                                </td>
                            </table>
                        </td>
                    </tr>
                    <tr style="display: none">
                     <td ><asp:FileUpload ID="FileUpload" name runat="server" /></td>
                   </tr>
                    <tr>
                        <td>
                            <table>
                                <td class="fieldNameLeft" style="height: 26px" nowrap>
                                    <asp:Label ID="lblUploadCaseCertificate" runat="server">上传箱单</asp:Label>
                                </td>
                       
                     <td class="fieldValue">
                                    <input id="FileImport" name="FileImport" type="file" runat="server" style="WIDTH: 300px" size="56" class="textbox" />
                                           </td>       
                   <td class="fieldNameLeft">
                                     <input class="cmdAddImport" id="cmdAddImport" type="submit" value="上传" name="cmdAddImport"
                                            runat="server" onserverclick="Button1_Click">
                                   
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </table>
                        </td>
                    </tr>
                    <tr class="toolBar">
                        <td>
                            <table class="toolBar">
                                <tr>
                                    <td class="toolBar">
                                        <input class="submitImgButton" id="cmdNoPackage" type="submit" value="免包装" name="cmdNoPackage"
                                            runat="server" onserverclick="cmdNoPackage_ServerClick">
                                    </td>
                                    <td class="toolBar">
                                        <input class="submitImgButton" id="cmdApplyOQC" type="submit" value="OQC申请" name="cmdApplyOQC"
                                            runat="server" onserverclick="cmdApplyIQC_ServerClick" />
                                    </td>
                                    <td class="toolBar">
                                        <input class="submitImgButton" id="cmdAchieve" type="submit" value="完成唛头" name="cmdAchieve"
                                            runat="server" onserverclick="cmdAchieve_ServerClick" />
                                    </td>
                                    <td class="toolBar">
                                        <input class="submitImgButton" id="cmdExportSendGoodReceipt" type="submit" value="导出发货箱单"
                                            name="cmdExportSendGoodReceipt" runat="server" onserverclick="cmdExportSendGoodReceipt_click" />
                                    </td>
                                    <td class="toolBar">
                                        <input class="submitImgButton" id="cmdExportLightSendGoodReceipt" type="submit" value="导出光伏发货箱单"
                                            name="cmdExportLightSendGoodReceipt" onserverclick="cmdExportLightSendGoodReceipt_click" runat="server" />
                                    </td>
                                    <td class="toolBar">
                                        <input class="submitImgButton" id="cmdExportLoadGoodReceipt" type="submit" value="导出装箱清单"
                                            name="cmdExportLoadGoodReceipt" runat="server"  onserverclick="cmdExportLoadGoodReceipt_click" />
                                    </td>
                                    <td class="toolBar">
                                        <input class="submitImgButton" id="cmdSystemOutStorage" type="submit" value="系统出库"
                                            name="cmdSystemOutStorage" runat="server"  onclick="GetSelectRowGUIDS('gridWebGrid')"  onserverclick="cmdSystemOutStorage_ServerClick" />
                                    </td>
                                    <%--<td>
                            <input class="submitImgButton" id="cmdCancel" type="submit" value="取 消" name="cmdCancel"
                                runat="server">
                        </td>--%>
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
