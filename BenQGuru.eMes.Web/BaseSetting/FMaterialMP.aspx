<%@ Page Language="c#" CodeBehind="FMaterialMP.aspx.cs" AutoEventWireup="True"
    Inherits="BenQGuru.eMES.Web.BaseSetting.FMaterialMP" %>

<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="cc2" Namespace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>FDCTCommandMP</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="<%=StyleSheet%>" rel="stylesheet">
      <script language="javascript">
          function Check() {
              if (document.all.DownLoadPathBom.value == "") {
                  alert("上传文件不能为空");
                  return false;
              }
          }
          function LoadFile() {
              //debugger;
              try {

                  var hf = document.all.aFileDownLoad.href;
                  if (hf == "") return;
                  var path;
                  var pix = hf.substr(0, 4);

                  if (pix.toLowerCase() == "file") path = hf.substr(8, hf.length - 8);
                  else if (pix.toLowerCase() == "http") path = hf;
                  var fl = path.split('/');
                  var file = fl[fl.length - 2] + '/' + fl[fl.length - 1];
                  var strDownUrl = "http://" + fl[fl.length - 4] + "/" + fl[fl.length - 3] + "/FDownload.aspx?&fileName=" + escape(file);
                  window.open(strDownUrl, "ExportWindow", "top=60000,left=60000,height=1,width=1,status=no,toolbar=no,menubar=no,location=no");

                  return false;

              } catch (e) { alert(e.description); }
          }
    </script>

</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
    <table id="Table1" height="100%" width="100%" runat="server">
        <tr class="moduleTitle">
            <td>
                <asp:Label ID="lblMaterialTitle" runat="server" CssClass="labeltopic"> 物料维护</asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table class="query" height="100%" width="100%">
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblMaterialNOQuery" runat="server"> 物料编码</asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 159px">
                            <asp:TextBox ID="txtMaterialNOQuery" runat="server" Width="150px" DESIGNTIMEDRAGDROP="257"
                                CssClass="textbox"></asp:TextBox>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblDQMaterialNOQuery" runat="server">鼎桥物料编码</asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 159px">
                            <asp:TextBox ID="txtDQMaterialNOQuery" runat="server" Width="150px" DESIGNTIMEDRAGDROP="257"
                                CssClass="textbox" ></asp:TextBox>
                        </td>
                        <td></td>
                        <td></td>
                        <td nowrap width="100%">
                        </td>
                     </tr>
                     <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblMaterialDescQuery" runat="server"> 物料描述</asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 159px">
                            <asp:TextBox ID="txtMaterialDescQuery" runat="server" Width="150px" DESIGNTIMEDRAGDROP="257"
                                CssClass="textbox" ></asp:TextBox>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblMaterialTypeQuery" runat="server">管控类型</asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 159px">
                            <asp:DropDownList ID="drpMaterialTypeQuery" runat="server" Width="150px" DESIGNTIMEDRAGDROP="257"   CssClass="textbox"></asp:DropDownList>
                           
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblMaterialSourceQuery" runat="server">物料来源</asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 159px">
                            <asp:DropDownList ID="drpMaterialSourceQuery" runat="server" Width="150px" DESIGNTIMEDRAGDROP="257"   CssClass="textbox"></asp:DropDownList>
                            
                        </td>
                        <td nowrap width="100%">
                        </td>
                        <td class="fieldName">
                            <input class="submitImgButton" id="cmdQuery" type="submit" value="查 询" name="btnQuery"
                                   runat="server"/>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        
         <tr  style="height:0px"><td></td></tr>
            <tr class="moduleTitle">
                <td>
                    <asp:Label ID="lblDataInmport" runat="server" CssClass="labeltopic">数据导入</asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <table class="query" id="Table2" height="100%" width="100%">
                        <tr align="right">
                            <td class="fieldNameLeft" nowrap height="20">
                                <asp:Label ID="lblImportType" Visible="false" runat="server"> 导入类型</asp:Label>
                            </td>
                            <td class="fieldNameLeft" nowrap height="20">
                                <asp:Label ID="lblInFile" runat="server"> 导入文件：</asp:Label>
                            </td>
                            <td style="width: 300px">
                                <input id="DownLoadPathBom" name="DownLoadPathBom" type="file" runat="server" style="WIDTH: 300px" size="56" class="textbox" />
                            </td>
                            <td class="fieldNameLeft" nowrap height="20">
                                <asp:Label ID="lblInTemplet" runat="server"> 导入模板：</asp:Label>
                            </td>
                            <td nowrap>
                                <a id="aFileDownLoad" style="display: none; color: blue" href="" target="_blank"
                                    runat="server">
                                    <asp:Label ID="lblDown" runat="server">下载</asp:Label></a> <span style="cursor: pointer;
                                        color: blue; text-decoration: underline" onclick="LoadFile()">  
                                        <asp:Label ID="lblDown1" runat="server">下载</asp:Label></span>
                            </td>
                            <td nowrap width="100%">
                            </td>
                            <td class="toolBar">
                                <input class="submitImgButton" id="cmdEnter" type="submit" value="导入" name="btnAdd"
                                       runat="server" onserverclick="cmdEnter_ServerClick"/>
                            </td>
                        </tr>

                        <tr style="display: none">
                            <td colspan="8">
                                <div id="DIVForDownload" style="width: 1px; position: relative; height: 1px" runat="server"
                                    ms_positioning="GridLayout">
                                </div>
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
                       <%-- <td class="smallImgButton">
                            <input class="deleteButton" id="cmdDelete" type="submit" value="  " name="cmdDelete"
                                runat="server">
                        </td>--%>
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
        <tr class="normal">
            <td>
                <table class="edit" height="100%" cellpadding="0" width="100%">
                    <tr>
                        <td class="fieldNameLeft" style="height: 26px" nowrap>
                            <asp:Label ID="lblMaterialNO" runat="server">物料编码</asp:Label>
                        </td>
                        <td class="fieldValue" style="height: 26px">
                            <asp:TextBox ID="txtMaterialNO" runat="server" BackColor="#D2D2D2"  CssClass="require" Width="130px"></asp:TextBox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblMaterialENDesc" runat="server">英文短描述</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtMaterialENDesc" runat="server" BackColor="#D2D2D2"  Width="130px" CssClass="textbox"></asp:TextBox>
                        </td>
                         <td class="fieldName" nowrap>
                            <asp:Label ID="lblMaterialCHDesc" runat="server">中文短描述</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtMaterialCHDesc" runat="server" BackColor="#D2D2D2"  Width="130px" CssClass="textbox"></asp:TextBox>
                        </td>
                        <td nowrap width="100%">
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldNameLeft" style="height: 26px" nowrap>
                            <asp:Label ID="lblDQMaterialNO" runat="server">鼎桥物料编码</asp:Label>
                        </td>
                        <td class="fieldValue" style="height: 26px">
                            <asp:TextBox ID="txtDQMaterialNO" runat="server" BackColor="#D2D2D2" CssClass="require" Width="130px"></asp:TextBox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblMaterialType" runat="server">管控类型</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:DropDownList ID="drpMaterialType" runat="server" Width="150px" DESIGNTIMEDRAGDROP="257"   CssClass="textbox"></asp:DropDownList>
                            
                        </td>
                         <td class="fieldName" nowrap>
                            <asp:Label ID="lblMaterialValidity" runat="server">有效期</asp:Label>
                        </td>
                        <td class="fieldValue" style="width:200px">
                            <asp:TextBox ID="txtMaterialValidity" runat="server" Width="130px" CssClass="textbox"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="lblUnitee" runat="server">(单位：天)</asp:Label>
                        </td>
                         <td>
                            
                        </td>
                        <td nowrap width="100%">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="toolBar">
            <td>
                <table class="toolBar">
                    <tr>
                        <%--<td class="toolBar">
                            <input class="submitImgButton" id="cmdAdd" type="submit" value="新 增" name="cmdAdd"
                                runat="server">
                        </td>--%>
                        <td class="toolBar">
                            <input class="submitImgButton" id="cmdSave" type="submit" value="保 存" name="cmdSave"
                                runat="server">
                        </td>
                        <td>
                            <input class="submitImgButton" id="cmdCancel" type="submit" value="取 消" name="cmdCancel"
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
