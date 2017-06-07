<%@ Page Language="c#" CodeBehind="FCreatePickLineMP.aspx.cs" AutoEventWireup="True" Inherits="BenQGuru.eMES.Web.Warehouse.FCreatePickLineMP" %>

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
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"/> 
    <link href="<%=StyleSheet%>" rel="stylesheet" />
    <script language="javascript" type="text/javascript">

        function SelectViewField() {
            var result = window.showModalDialog("FViewFieldEP.aspx?defaultUserCode=PickLine_FIELD_LIST_SYSTEM_DEFAULT&table=TBLCPICKDETAIL", "", "dialogWidth:800px;dialogHeight:600px;scroll:none;help:no;status:no");
            if (result == "OK") {
                window.location.href = window.location.href;
            }
        }

//        function SelectClick() {
//            var aobj = document.getElementById("txtDQMCodeEdit");

//            // $("input#txtDQMCodeEdit").attributes.type = "singleInvNoMaterialSP";

//            $("#txtDQMCodeEdit").attr("name", "singleInvNoMaterialSP");
////            if (aobj.value == "PRC") {
////                aobj.Type = "singleInvNoMaterialSP";
////                aobj.Target = "FSingleInvNoMaterialSP.aspx";
////            } else {
////                aobj.Type = "singleScrapMaterialSP";
////                aobj.Target = "FSingleScrapMaterialSP.aspx";
////            }
//        }
    </script>
</head>
<body>
    <form id="Form1" method="post" runat="server">
    <table id="Table1" height="100%" width="100%" runat="server">
        <tr class="moduleTitle">
            <td>
                <asp:Label ID="lblTitle" runat="server" CssClass="labeltopic">手工创建拣货任务令需求行</asp:Label>
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
                            <asp:TextBox ID="txtPickNoQuery" runat="server" CssClass="require" Width="130px" ReadOnly="True"></asp:TextBox>
                        </td>
                       
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblDQMCodeQuery" runat="server">鼎桥物料编码</asp:Label>
                        </td>
                        <td class="fieldValue" nowrap>
                            <asp:TextBox ID="txtDQMCodeQuery" runat="server" CssClass="textbox" Width="130px"></asp:TextBox>
                        </td>
                        <td nowrap width="100%">
                        </td>
                         <td class="fieldValue" nowrap>
                            <a href="#" onclick="SelectViewField(); return false;">
                                <asp:Label runat="server" ID="lblMOSelectMOViewField">选择栏位</asp:Label></a>
                        </td>
                        <td  class="fieldName" nowrap>
                            <input class="submitImgButton" id="cmdQuery" type="submit" value="查 询" name="btnQuery"
                                runat="server"/>
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
                            <asp:Label ID="lblDQMCodeEdit" runat="server" >鼎桥物料编码</asp:Label>
                        </td>
                        <td class="fieldValue" style="height: 26px">
                              <cc2:SelectableTextBox ID="txtDQMCodeEdit" runat="server" CssClass="require"
                                Width="130px" Type = "singleInvNoMaterialSP" Target = "FSingleInvNoMaterialSP.aspx"    AutoPostBack="True" ></cc2:SelectableTextBox>                             
                        </td>
                    
                          <td class="fieldValue" style="height: 26px">
                              <cc2:SelectableTextBox ID="txtScarpDQMCodeEdit" runat="server" CssClass="require"
                                Width="130px" Type = "singleScrapMaterialSP" Target = "FSingleScrapMaterialSP.aspx"    AutoPostBack ="true" ></cc2:SelectableTextBox>                             
                        </td>

                        <td class="fieldNameLeft" style="height: 26px" nowrap>
                            <asp:Label ID="lblMDescEdit" runat="server">物料描述</asp:Label>
                        </td>
                        <td class="fieldValue" style="height: 26px">
                           <asp:TextBox ID="txtMDescEdit" runat="server" CssClass="require" Width="130px" ReadOnly="True"></asp:TextBox> 
                        </td>
                        <td class="fieldNameLeft" style="height: 26px" nowrap>
                            <asp:Label ID="lblQty" runat="server">数量</asp:Label>
                        </td>
                        <td class="fieldValue" style="height: 26px">
                             <asp:TextBox ID="txtQtyEdit" runat="server" CssClass="textbox" Width="130px"    ></asp:TextBox> 
                        </td>
                        <td style="width: 100%">
                        </td>
                        
                         
                    </tr>
                     <tr  style="display: none">
                           <td class="fieldValue" style="height: 26px">
                           <asp:TextBox ID="txtInvNoEidt" runat="server" CssClass="require" Width="130px"></asp:TextBox> 
                        </td>
                           <td class="fieldValue" style="height: 26px">
                           <asp:TextBox ID="txtPickNoQuery1" runat="server" CssClass="require" Width="130px"></asp:TextBox> 
                        </td>
                                 <td class="fieldValue" style="height: 26px">
                           <asp:TextBox ID="txtInvLineEidt" runat="server" CssClass="require" Width="130px"></asp:TextBox> 
                        </td>

                          
                           <td class="fieldNameLeft" style="height: 26px" nowrap>
                            <asp:Label ID="lblPickLineEdit" runat="server">拣货任务令行项目号</asp:Label>
                        </td>
                        <td class="fieldValue" style="height: 26px">
                           <asp:TextBox ID="txtPickLineEdit" runat="server" CssClass="require" Width="130px"></asp:TextBox> 
                        </td>
                         
                    
                               <td class="fieldValue" nowrap>
                            <asp:TextBox ID="txtPickTypeQuery" runat="server" CssClass="require" Width="130px" ReadOnly="True"></asp:TextBox>
                        </td>
                        
                                <td class="fieldValue" nowrap>
                            <asp:TextBox ID="txtIsScrapEdit" runat="server" CssClass="require" Width="130px" ReadOnly="True"></asp:TextBox>
                        </td>
                        
                                  <td class="fieldValue" nowrap>
                            <asp:TextBox ID="txtStorageCodeEidt" runat="server" CssClass="require" Width="130px" ReadOnly="True"></asp:TextBox>
                        </td>
                        <td class="fieldValue" nowrap>
                            <asp:TextBox ID="txtCustmCodeEdit" runat="server" CssClass="require" Width="130px" ReadOnly="True"></asp:TextBox>
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
                            <input class="submitImgButton" id="cmdAdd" type="submit" value="新 增" name="cmdAdd"
                                runat="server">
                        </td>
                        <td class="toolBar">
                            <input class="submitImgButton" id="cmdSave" type="submit" value="保存" name="cmdSave"
                                runat="server" />
                        </td>
                             <td class="toolBar">
                            <input class="submitImgButton" id="cmdReturn" type="submit"
                                value="返回" name="cmdReturn" runat="server" onserverclick="cmdReturn_ServerClick"/>
                        </td>

                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
