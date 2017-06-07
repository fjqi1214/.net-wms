<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FIQCCheckResultMP.aspx.cs"
    Inherits="BenQGuru.eMES.Web.IQC.FIQCCheckResultMP" %>

<%@ Register TagPrefix="uc1" TagName="eMESDate" Src="../UserControl/DateTime/DateTime/eMESDate.ascx" %>
<%@ Register TagPrefix="uc1" TagName="eMesTime" Src="../UserControl/DateTime/DateTime/eMesTime.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="cc2" Namespace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>FIQCCheckResultMP</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="<%=StyleSheet%>" rel="stylesheet">
    <style type="text/css">
        input.DefaultInput
        {
            cursor:pointer;
                width:150px;
                height:24px;
                border:1px solid #749DEA;
                background-color:#D6E2F8;
            }
        input.DefaultInput:hover
        {
                border:1px solid black;
                background-color:#E5E5E5;
            }
    </style>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
    <table id="Table1" height="100%" width="100%" runat="server">
        <tr class="moduleTitle">
            <td>
                <asp:Label ID="lblTitle" runat="server" CssClass="labeltopic"> IQC检验结果维护</asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table class="query" width="100%" style="height: 100%">
                    <tr>
                        <td class="fieldNameLeft" colspan="2" style="height: 26px" nowrap>
                           <asp:RadioButtonList ID="rblType" runat="server" RepeatDirection="Horizontal" AutoPostBack="true"   OnSelectedIndexChanged="RadioButtonList_SelectedIndexChanged">
                                <asp:ListItem Selected="True" Value="抽检">SpotCheck</asp:ListItem>
                                <asp:ListItem  Value="加严全检">FullCheck</asp:ListItem> 
                           </asp:RadioButtonList>
                        </td>
                        <td class="fieldName" style="height: 26px" nowrap>
                            <asp:Label ID="lblAQLStandardQuery" runat="server">AQL标准</asp:Label>
                        </td>
                        <td class="fieldValue" style="height: 26px">
                             <asp:DropDownList ID="drpAQLStandardQuery" runat="server" CssClass="textbox" Width="110px" 
                                                AutoPostBack="true"                                                
                                 onselectedindexchanged="drpAQLStandardQuery_SelectedIndexChanged" >
                            </asp:DropDownList>
                        </td>
                        <td class="fieldName" style="height: 26px" nowrap>
                            <asp:Label ID="lblAQLDesc" runat="server">AQL标准描述</asp:Label>
                        </td>
                        <td class="fieldValue" style="height: 26px;">
                         <asp:TextBox ID="txtAQLDesc" runat="server" BorderWidth="0" BackColor="#EAF2FB"  ReadOnly="true"  Width="130px">0收0拒</asp:TextBox>
                        </td>
                        <td class="fieldName" style="height: 26px" nowrap>
                            <asp:Label ID="lblSamples" runat="server" >样本数</asp:Label>
                        </td>
                        <td class="fieldValue" style="height: 26px">
                            <asp:TextBox ID="txtSamplesNum" runat="server" BorderWidth="0" BackColor="#EAF2FB"  ReadOnly="true"  Width="130px">1000000</asp:TextBox>
                        </td>
                        <td class="fieldName" style="height: 26px" nowrap>
                            <asp:Label ID="lblRejection" runat="server">判退数</asp:Label>
                        </td>
                        <td class="fieldValue" style="height: 26px">
                            <asp:TextBox ID="txtRejectionNum" runat="server" BorderWidth="0" BackColor="#EAF2FB"  ReadOnly="true"  Width="130px">10000000</asp:TextBox>
                        </td>
                        <td  nowrap width="100%">
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldNameLeft" style="height: 26px" nowrap>
                            <asp:Label ID="lblCartonNoQurey" runat="server">箱号</asp:Label>
                        </td>
                        <td class="fieldValue" style="height: 26px">
                             <asp:TextBox ID="txtCartonNoQurey" runat="server" CssClass="textbox" Width="130px"></asp:TextBox>
                        </td>
                        <td class="fieldName">
                            <input class="submitImgButton" id="cmdQuery" type="submit" value="查 询" name="btnQuery"
                                runat="server" />
                        </td>
                        <td colspan="8" nowrap width="100%">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="fieldGrid">
                <ig:WebDataGrid ID="gridWebGrid2" runat="server" Width="100%">
                </ig:WebDataGrid>
            </td>
        </tr>
        <tr class="normal">
            <td>
                <table height="100%" cellpadding="0" width="100%">
                    <tr>
                        <td class="smallImgButton">
                            <input class="gridExportButton" id="cmdGridExport2" type="submit" value="  " name="cmdGridExport"
                                runat="server">
                        </td>
                        <td>
                            <cc1:PagerSizeSelector ID="pagerSizeSelector2" runat="server">
                            </cc1:PagerSizeSelector>
                        </td>
                        <td align="right">
                            <cc1:PagerToolBar ID="pagerToolBar2" runat="server">
                            </cc1:PagerToolBar>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr height="100%" >
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
                            <cc1:PagerSizeSelector ID="pagerSizeSelector" runat="server">
                            </cc1:PagerSizeSelector>
                        </td>
                        <td align="right">
                            <cc1:PagerToolBar ID="pagerToolBar" runat="server">
                            </cc1:PagerToolBar>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        
        <tr class="normal">
            <td>
                <table class="edit" width="100%">
                    <tr>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblNGTypeEdit" runat="server">缺陷类型</asp:Label>
                        </td>
                        <td class="fieldValue">
                             <asp:DropDownList ID="drpNGTypeEdit" runat="server" CssClass="require" Width="120px"
                                DESIGNTIMEDRAGDROP="94"  AutoPostBack="true"
                                 onselectedindexchanged="drpNGTypeEdit_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td class="fieldNameLeft" style="height: 26px" nowrap>
                            <asp:Label ID="lblNGDescEdit" runat="server">缺陷描述</asp:Label>
                        </td>
                        <td class="fieldValue" style="height: 26px">
                            <asp:DropDownList ID="drpNGDescEdit" runat="server" CssClass="require" Width="120px"
                                DESIGNTIMEDRAGDROP="94" >
                            </asp:DropDownList>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblSNEdit" runat="server">SN</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtSNEdit" runat="server" Width="110px" CssClass="textbox"></asp:TextBox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblNGQtyEdit" runat="server">不良数</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtNGQtyEdit" runat="server" Width="110px" CssClass="textbox"></asp:TextBox>
                        </td>
                        <td colspan="4" nowrap width="100%"></td>
                    </tr>
                    <tr>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblMemoEdit" runat="server">备注</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtMemoEdit" runat="server" Width="110px" CssClass="textbox"></asp:TextBox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblCartonNoEdit" runat="server">箱号</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtCartonNoEdit" runat="server" Width="110px" CssClass="textbox"></asp:TextBox>
                        </td>
                        
                        <td class="fieldName" style="height: 26px" nowrap>
                            <asp:Label ID="lbxSampleQty" runat="server" >样本数</asp:Label>
                        </td>
                        <td class="fieldValue" style="height: 26px">
                            <asp:TextBox ID="txtSamplesSize1" runat="server"  Width="130px"></asp:TextBox>
                        </td>
                        
                         <td class="fieldValue" nowrap>
                             <input class="submitImgButton" id="cmdSave" type="submit" value="保 存" name="cmdSave"
                                            runat="server" />
                         </td>
                          <td class="fieldValue"nowrap>
                           <input class="submitImgButton" id="cmdCommit" type="submit" value="提交" name="cmdCommit"
                                            runat="server" onserverclick="cmdCommit_ServerClick"/>
                         </td>
                          <td class="fieldValue">
                           <input class="submitImgButton" id="cmdStatusSTS" type="submit" value="免检" name="cmdStatusSTS"
                                runat="server" onserverclick="cmdStatusSTS_ServerClick" />
                        </td>
                          <td class="fieldValue" nowrap>
                           <input class="submitImgButton" id="cmdReturn" type="submit" value="返 回" name="cmdReturn" runat="server" onserverclick="cmdReturn_ServerClick" />
                         </td>
                          <td class="fieldValue" style="height: 26px" nowrap>
                           
                             <input class="DefaultInput"  id="cmdExportIQCACL" type="submit" value="导出IQC异常联络单" name="cmdExportIQCACL" 
                                            runat="server" onserverclick="cmdExportIQCACL_ServerClick"/>
                        </td>
                      
                         <td  width="100%"  nowrap>
                         </td>

                    </tr>
                  
                </table>
            </td>
        </tr>
        <tr class="normal">
            <td>
                <table class="edit"  width="100%">
                 <tr class="normal">
                        <td>
                            <table class="Import">
                                <tr class="moduleTitle">
                                    <td>  
                                        异常图片上传
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
                                        <input class="submitImgButton" id="cmdEnter" type="submit" value="导入" name="cmdEnter" runat="server" onserverclick="cmdEnter_ServerClick"/>
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
