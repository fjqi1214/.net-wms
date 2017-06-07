<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FStorageCodes.aspx.cs" Inherits="BenQGuru.eMES.Web.SelectQuery.FStorageCodes" %>


<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>ASN号(单选)</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="<%=StyleSheet%>" rel="stylesheet">
</head>
<body>
    <form id="Form1" method="post" runat="server">
    <table id="Table1" height="100%" cellspacing="1" cellpadding="1" width="100%" border="0"
        runat="server">
        <tr class="moduleTitle">
            <td>
                <asp:Label ID="lblFWWpoSP" runat="server">预留单介面</asp:Label>
            </td>
        </tr>
        <tr>
            <td nowrap>
                <table class="query" id="Table2" height="100%" width="100%">
                    <tr>
                       <td class="fieldName" nowrap>
                             <asp:Label ID="lblStorageCodeQuery" runat="server">库位代码</asp:Label>
                        </td>
                        <td class="fieldValue" nowrap>
                           <asp:TextBox ID="txtStorageCodeQuery" runat="server" Width="150px" DESIGNTIMEDRAGDROP="257"
                                CssClass="require"></asp:TextBox>
                        </td>
                        <td class="fieldName">
                            <input class="submitImgButton" id="cmdQuery" onmouseover="" onmouseout="" type="submit"
                                value="查 询" name="cmdQuery" runat="server">
                        </td>
                    </tr>
                  <tr style="display: none"> 
                              <tr >
                           
                           </tr>
                </table>
           </td>
        </tr>
        <tr>
            <td class="fieldGrid">
                <ig:WebDataGrid ID="gridUnSelected" runat="server" Width="100%">
                </ig:WebDataGrid>
            </td>
        </tr>
        <tr>
            <td nowrap>
                <table id="Table3" height="100%" cellpadding="0" width="100%">
                    <tr>
                        <td>
                            <cc1:pagersizeselector id="pagerSizeSelector" runat="server">
                            </cc1:pagersizeselector>
                        </td>
                        <td align="right">
                            <cc1:pagertoolbar id="pagerToolBar" runat="server">
                            </cc1:pagertoolbar>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr style="height: 50px">
            <td nowrap align="center">
                &nbsp;<input class="submitImgButton" id="cmdSave" type="submit" value="保 存" name="cmdSave"
                    runat="server">&nbsp;<input class="submitImgButton" id="cmdCancel" type="submit"
                        value="取 消" name="cmdCancel" runat="server">
            </td>
        </tr>
        <tr>
            <td>
                <div style="display: none">
                    <input type="button" value="init" id="cmdInit" runat="server" name="cmdInit">
                    <textarea id="txtSelected" rows="1" cols="20" runat="server" name="txtSelected"></textarea>
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
