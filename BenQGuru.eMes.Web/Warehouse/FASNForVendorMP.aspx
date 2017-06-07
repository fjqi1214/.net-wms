<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FASNForVendorMP.aspx.cs" Inherits="BenQGuru.eMES.Web.WarehouseWeb.FASNForVendorMP" %>

<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="cc2" Namespace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<%@ Register TagPrefix="uc1" TagName="eMESDate" Src="~/UserControl/DateTime/DateTime/eMESDate.ascx" %>
<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>FStorageMP</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="<%=StyleSheet%>" rel="stylesheet">
    <script language="javascript">
    function SelectViewField() {
             var result = window.showModalDialog("FViewFieldEP.aspx?defaultUserCode=ASN_FIELD_LIST_SYSTEM_DEFAULT&table=TBLASN", "", "dialogWidth:800px;dialogHeight:600px;scroll:none;help:no;status:no");
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
                <asp:Label ID="lblTitle" runat="server" CssClass="labeltopic">入库指令创建-供应商</asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table class="query" height="100%" width="100%">
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblStorageInASNQuery" runat="server">入库指令号</asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 159px">
                            <asp:TextBox ID="txtStorageInASNQuery" runat="server" Width="150px" DESIGNTIMEDRAGDROP="257"
                                CssClass="textbox"></asp:TextBox>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblStorageInTypeQuery" runat="server">入库类型</asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 159px">
                            <asp:DropDownList ID="drpStorageInTypeQuery" runat="server" CssClass="textbox" Width="150px"
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
                        <td class="fieldNameLeft"  nowrap>
                            <asp:Label ID="lblCreateUserQuery" runat="server">创建人</asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 159px">
                            <asp:TextBox ID="txtCreateUserQuery" runat="server" Width="150px" DESIGNTIMEDRAGDROP="257"
                                CssClass="textbox"></asp:TextBox>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblStatusQuery" runat="server">状态</asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 159px">
                            <asp:DropDownList ID="drpStatusQuery" runat="server" CssClass="textbox" Width="120px"
                                DESIGNTIMEDRAGDROP="94">
                            </asp:DropDownList>
                        </td>
                       <td nowrap width="100%">
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblCBDateQuery" runat="server">创建日期开始</asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 130px">
                             <asp:TextBox type="text" id="txtCBDateQuery"  class='datepicker' runat="server"  Width="130px"/>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblCEDateQuery" runat="server">创建日期结束</asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 130px">
                             <asp:TextBox type="text" id="txtCEDateQuery"  class='datepicker' runat="server"  Width="130px"/>
                        </td>
                        <td class="fieldValue" nowrap>
                            <a href="#" onclick="SelectViewField(); return false;">
                                <asp:Label runat="server" ID="lblMOSelectMOViewField">选择栏位</asp:Label></a>
                        </td>
                        <td colspan="5" nowrap width="100%">
                        </td>
                        <td class="fieldName">
                            <input class="submitImgButton" id="cmdQuery" type="submit" value="查 询" name="btnQuery" runat="server" />
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
                    <tr>
                        <td class="fieldNameLeft" style="height: 26px" nowrap>
                            <asp:Label ID="lblStorageInASNEdit" runat="server">入库指令号</asp:Label>
                        </td>
                        <td class="fieldValue" style="height: 26px">
                             <asp:TextBox ID="txtStorageInASNEdit" runat="server" Width="150px" DESIGNTIMEDRAGDROP="257"
                                CssClass="require"></asp:TextBox>
                                 <input class="submitImgButton" id="cmdCreat" type="submit" value="生 成" name="cmdCreat"
                                runat="server" onserverclick="cmdCreat_ServerClick" />
                        </td>
                        <td class="fieldNameLeft" style="height: 26px" nowrap>
                            <asp:Label ID="lblStorageInTypeEdit" runat="server">入库类型</asp:Label>
                        </td>
                        <td class="fieldValue" style="height: 26px">
                             <asp:DropDownList ID="drpStorageInTypeEdit" runat="server" CssClass="require" Width="120px"  AutoPostBack="true" 
                                DESIGNTIMEDRAGDROP="94"  onselectedindexchanged="drpStorageInTypeEdit_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblInvNoEdit" runat="server">SAP单据号</asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 159px">
                            <asp:TextBox ID="txtSAPInvNoEdit" runat="server" Width="120px"  DESIGNTIMEDRAGDROP="257"
                                CssClass="require"></asp:TextBox>
                        </td>
                        <td class="fieldNameLeft" style="height: 26px" nowrap colspan="4">
                            <asp:CheckBoxList ID="cblFlag" AutoPostBack="true" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="cblFlag_SelectedIndexChanged" >
                                <asp:ListItem  Value="Skip">供应商直发入库指令</asp:ListItem>
                                 <asp:ListItem  Value="DNR">生产退料入不良品库</asp:ListItem>
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldNameLeft" style="height: 26px" nowrap>
                            <asp:Label ID="lblExpectedArrivalDateEdit" runat="server" ForeColor="Red">预计到货日期</asp:Label>
                        </td>
                        <td class="fieldValue" style="height: 26px">
                             <asp:TextBox type="text" id="txtExpectedArrivalDateEdit"  class='datepicker' runat="server"  Width="130px"/>          
                        </td>
                      
                           <td class="fieldNameLeft" style="height: 26px" nowrap>
                            <asp:Label ID="lblMesFacNameEdit" runat="server">制造厂名称</asp:Label>
                        </td>
                        <td class="fieldValue" style="height: 26px">
                             <asp:DropDownList ID="drpVendorCodeEdit" runat="server" CssClass="require" Width="120px"
                                DESIGNTIMEDRAGDROP="94">
                            </asp:DropDownList>
                        </td>
                        <td class="fieldNameLeft" style="height: 26px" nowrap>
                            <asp:Label ID="lblMemoEdit" runat="server">备注</asp:Label>
                        </td>
                        <td class="fieldValue" colspan="4" style="height: 26px">
                            <asp:TextBox ID="txtMemoEdit" runat="server" CssClass="textbox" Width="120px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="toolBar">
                        <td colspan="7">
                            <table class="toolBar">
                                <tr>
                                    <td class="toolBar">
                                        <input class="submitImgButton" id="cmdSave" type="submit" value="保存" name="cmdSave"
                                            runat="server" />
                                    </td>
                                    <td>
                                        <input class="submitImgButton" id="cmdReturn" type="submit" value="返 回" name="cmdReturn" 
                                        runat="server" onserverclick="cmdReturn_ServerClick">
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr class="normal">
                      <td colspan="7">
                        <table class="Import">
                            <tr class="moduleTitle">
                                <td>  
                                    直发入库客户接受凭证上传
                                </td>
                            </tr>
                            <tr>    
                                <td class="fieldNameLeft" noWrap>
                                    <asp:label id="lblFileImport" runat="server">导入文件：</asp:label>
                                </td>
					            <td class="fieldValue">
						            <input id="FileImport" style="WIDTH: 454px" type="file" size="56" name="FileImport" class="textbox" runat="server" />
					            </td>
					            <td class="toolBar">
                                    <input class="submitImgButton" id="cmdEnter" type="submit" value="导入" name="btnAdd" runat="server" onserverclick="cmdEnter_ServerClick" />
                                </td>
                                   
                            </tr>
                                   <tr style="display: none">
                              <td class="fieldNameLeft" style="height: 26px" nowrap>
                            <asp:Label ID="lblStorageInEdit" runat="server">入库库位</asp:Label>
                        </td>
                        <td class="fieldValue" style="height: 26px">
                             <asp:DropDownList ID="drpStorageInEdit" runat="server" CssClass="textbox" Width="120px"
                                DESIGNTIMEDRAGDROP="94">
                            </asp:DropDownList>
                        </td>
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
