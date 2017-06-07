<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FCreateCommandDemand.aspx.cs" Inherits="BenQGuru.eMES.Web.WarehouseWeb.FCreateCommandDemand" %>

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

   <%-- <script language="javascript" type="text/javascript">

        function SelectViewField() {
            var result = window.showModalDialog("FViewFieldEP.aspx?defaultUserCode=PickHead_FIELD_LIST_SYSTEM_DEFAULT&table=TBLPICK", "", "dialogWidth:800px;dialogHeight:600px;scroll:none;help:no;status:no");
            if (result == "OK") {
                window.location.href = window.location.href;
            }
        }
    </script>--%>

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
                                CssClass="textbox" Enabled="false"></asp:TextBox>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblDQMCODEQuery" runat="server">鼎桥物料编码</asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 159px">
                            <asp:TextBox ID="txtDQMCODEQuery" runat="server" Width="150px" DESIGNTIMEDRAGDROP="257"
                                CssClass="textbox"></asp:TextBox>
                        </td>
                        <td class="fieldNameLeft" nowrap>
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
                                runat="server"  onserverclick="cmdDelete_ServerClick" />
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
                            <asp:Label ID="lblDQMCODEEdit" runat="server">鼎桥物料编码</asp:Label>

                             <asp:TextBox ID="txtPickLineEdit" runat="server" Enabled="false" Visible="false" ></asp:TextBox>
                              <asp:TextBox ID="txtDQMCodeEdit" runat="server" Enabled="false" ></asp:TextBox>
                        </td>
                        <td class="fieldValue" style="height: 26px">
                          <cc2:SelectableTextBox ID="txtWWPoSerialEdit" runat="server" CssClass="require"
                               Type="WWpoSP" Target="FWWpoSP.aspx"  AutoPostBack ="true"  width="10px"  ></cc2:SelectableTextBox>                             
                         
                        </td>
                        <td>
                            <asp:Label ID="lblDMESCEdit" runat="server">物料描述</asp:Label>
                        </td>
                        <td>
                               <asp:TextBox ID="txtDMESCEdit" runat="server" Width="150px" DESIGNTIMEDRAGDROP="257"
                                CssClass="textbox" Enabled="false"></asp:TextBox>
                        </td>
                        <%--<td>
                            <asp:Label ID="lblNumEdit" runat="server">数量</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtNumEdit" runat="server" CssClass="require" Width="130px"></asp:TextBox>
                        </td>
                        --%>
                      
                    </tr>
                    
                  
                </table>
            </td>
        </tr>
        <tr class="toolBar">
            <td class="fieldNameLeft" style="text-align:left;">
                <table class="toolBar" >
                    <tr>
                        <td class="toolBar">
                            <input class="submitImgButton" id="cmdAdd" type="submit" value="新增" name="cmdInitial"
                                runat="server"  />
                        </td>
                        <td class="toolBar">
                              <input class="submitImgButton" id="cmdLotSave" type="submit" value="批量保存" name="cmdInitialCheck"
                                runat="server" onserverclick="cmdLotSave_ServerClick" />
                        </td>
                         <td class="toolBar">
                            <input class="submitImgButton" id="cmdReturn" type="submit"
                                value="返回" name="cmdReturn" runat="server" onserverclick="cmdReturn_ServerClick"/>
                        </td>
                      
                    </tr>
                    <tr style="display: none">
                        <td class="fieldValue" style="height: 26px">
                            <asp:TextBox ID="txtInvNoEidt" runat="server" CssClass="require" Width="130px"></asp:TextBox>
                        </td>
                                <td class="fieldValue" style="height: 26px">
                            <asp:TextBox ID="txtInvLineEidt" runat="server" CssClass="require" Width="130px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
