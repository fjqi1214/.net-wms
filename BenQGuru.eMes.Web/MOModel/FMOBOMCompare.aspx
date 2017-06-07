<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>

<%@ Page Language="c#" CodeBehind="FMOBOMCompare.aspx.cs" AutoEventWireup="True"
    Inherits="BenQGuru.eMES.Web.MOModel.FMOBOMCompare" %>

<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>FModelRouteSP</title>
    <base target="_self">
    <meta http-equiv="pragma" content="no-cache">
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="<%=StyleSheet%>" rel="stylesheet">
    <script language="javascript">

        function Close() {
            window.close();
        }
    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
    <table height="100%" width="100%" runat="server" id="table1">
        <tr>
            <td>
                <asp:Label ID="lblMOBOMTitle" runat="server" CssClass="labeltopic"> 产品工序BOM<--->工单用料清单 比对结果</asp:Label>
            </td>
        </tr>
        <tr class="moduleTitle">
            <td>
                <asp:Label ID="lblTogether" runat="server" CssClass="labeltopic"> 共同存在</asp:Label>
            </td>
        </tr>
        <tr>
            <td class="fieldGrid">
                <ig:WebDataGrid ID="gridWebGrid" runat="server" Width="100%">
                </ig:WebDataGrid>
            </td>
        </tr>
        <tr class="normal">
            <td>
                <table height="100%" cellpadding="0" width="100%">
                    <tr>
                        <td align="right">
                            <asp:Label ID="lblgridWebGrid" runat="server" CssClass="labeltopic">Label</asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="toolBar">
            <td class="toolBar">
                <input class="submitImgButton" id="cmdMOClose" type="submit" value="导 出" name="cmdExport"
                    runat="server" onserverclick="cmdSucessExport_ServerClick">
            </td>
        </tr>
        <tr class="moduleTitle">
            <td>
                <asp:Label ID="lblTogetherBOM" runat="server" CssClass="labeltopic">只存在于产品工序BOM</asp:Label>
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
                        <td align="right">
                            <asp:Label ID="lblgridInMORouteBom" runat="server" CssClass="labeltopic">Label</asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="toolBar">
            <td class="toolBar">
                <input class="submitImgButton" id="cmdMOOpen" type="submit" value="导 出" name="cmdExport"
                    runat="server" onserverclick="cmdInMOBOMExport_ServerClick">
            </td>
        </tr>
        <tr class="moduleTitle">
            <td>
                <asp:Label ID="lblTogetherList" runat="server" CssClass="labeltopic"> 只存在于工单物料清单</asp:Label>
            </td>
        </tr>
        <tr>
            <td class="fieldGrid">
                <ig:WebDataGrid ID="gridWebGrid3" runat="server" Width="100%">
                </ig:WebDataGrid>
            </td>
        </tr>
        <tr class="normal">
            <td>
                <table height="100%" cellpadding="0" width="100%">
                    <tr>
                        <td align="right">
                            <asp:Label ID="lblgridInMOStandardBom" runat="server" CssClass="labeltopic">Label</asp:Label>
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
                        <td class="toolBar">
                            <input class="submitImgButton" id="cmdExport" type="submit" value="导 出" name="cmdExport"
                                runat="server" onserverclick="cmdGridExport_ServerClick">
                        </td>
                        <td class="toolBar">
                            <input class="submitImgButton" id="cmdReturn" type="submit" value="返 回" name="cmdReturn"
                                runat="server" onserverclick="cmdReturn_ServerClick">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
