<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FMaterialOnShelvesMP.aspx.cs" Inherits="BenQGuru.eMES.Web.WarehouseWeb.FMaterialOnShelvesMP" %>

<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="cc2" Namespace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>FStackMP</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="<%=StyleSheet%>" rel="stylesheet">
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
    <table height="100%" width="100%" runat="server">
        <tr class="moduleTitle">
            <td>
                <asp:Label ID="lblMaterialShelvesTitle" runat="server" CssClass="labeltopic">物料上架</asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table class="query" height="100%" width="100%">
                     <tr>
                        
                     
                     
                       
                        <td class="fieldNameLeft" style="height: 26px" nowrap>
                            <asp:Label ID="lblCartonNoEdit" runat="server" >箱号</asp:Label>
                        </td>
                        <td class="fieldValue" style="height: 26px">
                            <asp:TextBox ID="txtCartonNoEdit" runat="server" CssClass="require" Width="130px" onkeydown="if(event.keyCode==13) { cmdQuery.click() ;return false;}"></asp:TextBox>
                        </td>

                              <td class="fieldNameLeft" style="height: 26px" nowrap>
                            <asp:Label ID="lblLocationNO" runat="server">货位号</asp:Label>
                        </td>
                        <td class="fieldValue" style="height: 26px">
                            <asp:TextBox ID="txtLocationNO" runat="server" CssClass="require" Width="130px" onkeydown="if(event.keyCode==13) {return false;}"></asp:TextBox>
                        </td>
                        
                     
                        <td>
                            <input class="submitImgButton" id="cmdQuery" type="submit"  value="查 询" name="btnQuery" 
                                   runat="server"/>
                        </td>
                         <td style="width:100%">
                        </td>
                        <td>
                            <input class="submitImgButton" id="cmdShelves" type="submit" value="上 架" name="btnShelves" onclick="GetSelectRowGUIDS('gridWebGrid')" onserverclick="cmdSave_Click"
                                   runat="server"/>
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldNameLeft" style="height: 26px" nowrap>
                            <asp:Label ID="lblCartonnoSN" runat="server">箱号/SN</asp:Label>
                        </td>
                        <td class="fieldValue" style="height: 26px">
                            <asp:TextBox ID="txtCartonnoSN" runat="server" CssClass="require" Width="130px" onkeydown="if(event.keyCode==13) {return false;}"></asp:TextBox>
                        </td>
                        
                        
                        <td class="fieldNameLeft" style="height: 26px" nowrap>
                             <input class="submitImgButton" id="cmdCheckSN" type="submit" value="检 索" name="btnCheck" runat="server" onserverclick="cmdCheck_ServerClick"/>
                        </td>
                        <td class="fieldValue" style="height: 26px">
                        
                        </td>
                        
                        
                        <td>
                         
                        </td>
                         <td style="width:100%">
                        </td>
                        <td>
                          
                        </td>
                    </tr>
                    
                     <tr>
                        <td class="fieldNameLeft" style="height: 26px" nowrap>
                            <asp:Label ID="lblPlanOnshelves" runat="server">应上架</asp:Label>
                        </td>
                        <td class="fieldValue" style="height: 26px">
                            <asp:TextBox ID="txtPlanOnshelves" runat="server" Width="50px" BorderStyle="None"></asp:TextBox>
                        </td>
                        <td class="fieldNameLeft" style="height: 26px" nowrap>
                            <asp:Label ID="lblActOnshelves" runat="server">已上架</asp:Label>
                        </td>
                        <td class="fieldValue" style="height: 26px">
                            <asp:TextBox ID="txtActOnshelves" runat="server" Width="50px"  BorderStyle="None"></asp:TextBox>
                        </td>
                        
                         <td style="width:100%">
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
            <td style="display:none">
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
                            <cc1:PagerSizeSelector ID="pagerSizeSelector" runat="server"></cc1:PagerSizeSelector>
                        </td>
                        <td align="right">
                            <cc1:PagerToolBar ID="pagerToolBar" runat="server"></cc1:PagerToolBar>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
       
    </table>
    </form>
</body>
</html>
