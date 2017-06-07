<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FLocStorLocTransMP.aspx.cs" Inherits="BenQGuru.eMES.Web.WarehouseWeb.FLocStorLocTransMP" %>

<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="cc2" Namespace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<%@ Register TagPrefix="uc1" TagName="eMESDate" Src="~/UserControl/DateTime/DateTime/eMESDate.ascx" %>
<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>FLocStorLocTransMP</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
        <meta content="JavaScript" name="vs_defaultClientScript" />
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="<%=StyleSheet%>" rel="stylesheet">
    
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
    <table id="table1" height="100%" width="100%" runat="server">
        <tr class="moduleTitle">
            <td>
                <asp:Label ID="lblTitle" runat="server" CssClass="labeltopic">货位移动拣货作业</asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table class="query" height="100%" width="100%">
                    <tr>
                        
                         <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblLocationNoQuery" runat="server">货位移动单号</asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 159px">
                            <asp:TextBox ID="txtLocationNoQuery" runat="server" Width="150px"  
                                CssClass="textbox"></asp:TextBox>
                        </td>
                        
                           <td class="fieldName" nowrap>
                            <asp:Label ID="lblInvNoQuery" runat="server">SAP单据号</asp:Label>
                        </td>
                        <td class="fieldValue" nowrap>
                            <asp:TextBox ID="txtInvNoQuery" runat="server" CssClass="textbox" Width="130px"></asp:TextBox>
                        </td>

                       <td colspan="2" nowrap  width="100%">
                        </td>
                        <td class="fieldName">
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
                            <td class="fieldValue"  colspan="4"  nowrap>
                                    <asp:RadioButtonList ID="StatusList" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow"  OnSelectedIndexChanged="RadioButtonStatus_SelectedIndexChanged" AutoPostBack="True">
                                    </asp:RadioButtonList>
                                </td> 
                    </tr>

                     <tr  >
                     <td><table><tr>
                    <td class="fieldNameLeft" nowrap><asp:Label ID="lblLocationNoEdit" runat="server">货位移动单号</asp:Label></td>
                    <td class="fieldValue"><asp:TextBox ID="txtLocationNoEdit" runat="server" Width="150px"  CssClass="textbox"></asp:TextBox> </td> 
                     <td>  <input class="submitImgButton" id="cmdCreate" type="submit" value="创建" name="cmdCreate"   runat="server"   onserverclick="cmdCreate_Click" /> </td>
       
                          <td class="fieldNameLeft" style="width: 107px;" nowrap> <asp:Label ID="lblOriginalCartonEdit" runat="server">原箱号</asp:Label></td>
                       <td class="fieldValue" style="width: 159px"><asp:TextBox ID="txtOriginalCartonEdit" runat="server" Width="150px"  CssClass="textbox"></asp:TextBox> </td>
                      
                      <td class="fieldNameLeft" nowrap> <asp:Label ID="lblNumEdit" runat="server">数量</asp:Label></td>
                        <td class="fieldValue" style="width: 159px"><asp:TextBox ID="txtNumEdit" runat="server" Width="150px" 
                         CssClass="textbox" onkeyup="this.value=this.value.replace(/\D/g,'')" ></asp:TextBox> </td>
                         
                                   <td class="fieldNameLeft" > <asp:Label ID="lblSNEdit" runat="server">SN</asp:Label></td>
                    <td class="fieldValue" ><asp:TextBox ID="txtSNEdit" runat="server" Width="150px"  CssClass="textbox"></asp:TextBox> </td>
                    
                         <td class="toolBar"> <input class="submitImgButton" id="cmdCommit" type="submit" value="提交" name="cmdCommit" runat="server" onserverclick="cmdCommit_Click" /></td>
                  
                        </tr>
                        </table>
                             </td>
                    </tr>

                    <tr>
                                 <td>
                         <table><tr>
                                    
                        <td class="fieldNameLeft" nowrap><asp:Label ID="lblLocationCode" runat="server">目标货位</asp:Label></td>
                          <td class="fieldValue" style="width: 159px"><asp:TextBox ID="txtTLocationCodeEdit" runat="server" Width="150px"  CssClass="textbox"></asp:TextBox> </td> 
                        
                 <td></td>
                                  <td class="fieldNameLeft" nowrap><asp:Label ID="lblTLocationCartonEdit" runat="server">目标箱号</asp:Label></td>
                    <td class="fieldValue"><asp:TextBox ID="txtTLocationCartonEdit" runat="server" Width="150px"  CssClass="textbox"></asp:TextBox> </td> 
                       
                       
                       <td class="toolBar" colspan="2">
                            <input class="submitImgButton" id="cmdShelves" type="submit" value="上 架" name="cmdShelves"
                                runat="server"  onserverclick="cmdShelves_ServerClick"  />
                        </td>

                                  </tr>
                        </table>
                             </td>

                    </tr>
                    <tr>
           

 
                  
                 </tr>

                
          
                </table>
            </td>
        </tr>
        

          <tr class="toolBar">
            <td>
                <table class="toolBar">
                    <tr>
                          
                    </tr>
                </table>
            </td>
        </tr>

    </table>
    </form>
</body>
</html>
