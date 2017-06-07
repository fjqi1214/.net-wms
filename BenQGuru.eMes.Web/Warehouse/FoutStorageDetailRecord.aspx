<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FoutStorageDetailRecord.aspx.cs" Inherits="BenQGuru.eMES.Web.WarehouseWeb.FoutStorageDetailRecord" %>

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
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
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
        <tr>
            <td>
                <table class="query" height="100%" width="100%">
                    <tr>
                      
                       
                        <td class="fieldName" nowrap>
                             <asp:Label ID="lblStorageCodeQuery" runat="server">库位代码</asp:Label>
                        </td>
                        <td class="fieldValue" nowrap>
                           <asp:TextBox ID="txtStorageCodeQuery" runat="server" Width="150px" DESIGNTIMEDRAGDROP="257"
                                CssClass="require"></asp:TextBox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="LblPickTypeQuery" runat="server">拣货任务类型</asp:Label>
                        </td>
                        <td class="fieldValue" nowrap>
                            <asp:DropDownList ID="drpPickTypeQuery" name="drpPickTypeQuery" AutoPostBack="false"
                                runat="server" CssClass="require" Width="120px" >
                            </asp:DropDownList>
                        </td>
                       
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblCDateQuery" runat="server">到货初检日期</asp:Label>
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
                          <td class="fieldName" nowrap>
                            <input class="submitImgButton" id="cmdQuery" type="submit" value="查 询" name="btnQuery"
                                runat="server" />
                        </td>
                        <td nowrap width="100%">
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
        <tr class="toolBar">
            <td>
                <table class="toolBar">
                   
                    <tr>
                        
                        <td class="toolBar">
                            
                        </td>
                       
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>

