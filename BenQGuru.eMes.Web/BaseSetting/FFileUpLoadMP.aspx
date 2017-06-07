<%@ Register TagPrefix="ignav" Namespace="Infragistics.WebUI.UltraWebNavigator" Assembly="Infragistics35.WebUI.UltraWebNavigator.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>

<%@ Page Language="c#" CodeBehind="FFileUpLoadMP.aspx.cs" AutoEventWireup="True"
    Inherits="BenQGuru.eMES.Web.BaseSetting.FFileUpLoadMP" %>

<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="cc2" Namespace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>FFileUpLoadMP</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="<%=StyleSheet%>" rel="stylesheet">
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server" enctype="multipart/form-data">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
    <table height="100%" width="100%" runat="server">
        <tr class="moduleTitle">
            <td colspan="2">
                <asp:Label ID="lblTitle" runat="server" CssClass="labeltopic">文档目录维护</asp:Label>
            </td>
        </tr>
        <tr>
            <td class="tree">
                <ignav:UltraWebTree ID="treeDocument" OnNodeSelectionChanged="treeDocument_NodeSelectionChanged"
                    runat="server" Width="250px" Height="100%" WebTreeTarget="ClassicTree" Font-Size="12px"
                    Cursor="Hand" Indentation="20" ImageDirectory="/ig_common/WebNavigator31/" TargetFrame="ifPage"
                    CollapseImage="ig_treeMinus.gif" ExpandImage="ig_treePlus.gif">
                    <SelectedNodeStyle ForeColor="White" BackColor="Navy"></SelectedNodeStyle>
                    <Levels>
                        <ignav:Level Index="0"></ignav:Level>
                        <ignav:Level Index="1"></ignav:Level>
                    </Levels>
                </ignav:UltraWebTree>
            </td>
            <td>
                <table height="100%" cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <asp:Label ID="lblDocDirTitle" runat="server" CssClass="labeltopic">文档目录维护路径</asp:Label>
                    </tr>
                    <tr height="100%">
                        <td class="fieldGrid">
                            <ig:webdatagrid id="gridWebGrid" runat="server" width="100%">
                </ig:webdatagrid>
                        </td>
                    </tr>
                    <tr class="normal">
                        <td>
                            <table height="100%" cellpadding="0" width="100%">
                                <tr>
                                    <td>
                                        <asp:TextBox
                                            ID="txtDocDirQuery" runat="server" Visible="False">nothing</asp:TextBox>
                                        <asp:TextBox ID="txtDocSerialEdit" runat="server" Visible="False"></asp:TextBox>
                                    </td>
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
                    <tr class="normal">
                        <td>
                            <table class="edit" height="100%" cellpadding="0" width="100%">
                                <tr>
                                    <td class="fieldNameLeft" nowrap>
                                        <asp:Label ID="lblDocNameEdit" runat="server">文件名称</asp:Label>
                                    </td>
                                    <td class="fieldValue">
                                        <asp:TextBox ID="txtDocNameEdit" runat="server" CssClass="require" Width="130px"></asp:TextBox>
                                    </td>
                                    <td class="fieldName" nowrap>
                                        <asp:Label ID="lblDocNumEdit" runat="server">文件编号</asp:Label>
                                    </td>
                                    <td class="fieldValue">
                                        <asp:TextBox ID="txtDocNumEdit" runat="server" CssClass="require" Width="130px"></asp:TextBox>
                                    </td>
                                    <td class="fieldName" nowrap>
                                        <asp:Label ID="lblDocVerEdit" runat="server">文件版本</asp:Label>
                                    </td>
                                    <td class="fieldValue">
                                        <asp:TextBox ID="txtDocVerEdit" runat="server" CssClass="require" Width="130px" MaxLength="2"></asp:TextBox>
                                    </td>
                                    <td width="100%">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="fieldName" nowrap>
                                        <asp:Label ID="lblItemCodeEdit" runat="server">产品编号</asp:Label>
                                    </td>
                                    <td class="fieldValue">
                                        <cc2:SelectableTextBox ID="txtItemCodeEdit" runat="server" Width="130px" Type="item">
                                        </cc2:SelectableTextBox>
                                    </td>
                                    <td class="fieldName" nowrap>
                                        <asp:Label ID="lblOpCodeEdit" runat="server">工序代码</asp:Label>
                                    </td>
                                    <td class="fieldValue">
                                        <cc2:SelectableTextBox ID="txtOpCodeEdit" runat="server" CssClass="textbox" Width="130px"
                                            Type="operation"></cc2:SelectableTextBox>
                                    </td>
                                    <td class="fieldName" nowrap>
                                        <asp:Label ID="lblDocChgNumEdit" runat="server">更改单号</asp:Label>
                                    </td>
                                    <td class="fieldValue">
                                        <asp:TextBox ID="txtDocChgNumEdit" runat="server" CssClass="textbox" Width="130px"></asp:TextBox>
                                    </td>
                                    <td width="100%">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="fieldName" nowrap>
                                        <asp:Label ID="lblDocChgFileEdit" runat="server">更改文件</asp:Label>
                                    </td>
                                    <td class="fieldValue">
                                        <asp:TextBox ID="txtDocChgFileEdit" runat="server" CssClass="textbox" Width="130px"></asp:TextBox>
                                    </td>
                                    <td class="fieldName" nowrap>
                                        <asp:Label ID="lblMemoEdit" runat="server">备注</asp:Label>
                                    </td>
                                    <td class="fieldValue">
                                        <asp:TextBox ID="txtMemoEdit" runat="server" Width="130px" Height="60" TextMode="MultiLine"
                                            CssClass="require"></asp:TextBox>
                                    </td>
                                    <td class="fieldName" nowrap>
                                        <asp:Label ID="lblKeyWordEdit" runat="server">关键字</asp:Label>
                                    </td>
                                    <td class="fieldValue">
                                        <asp:TextBox ID="txtKeyWordEdit" runat="server" Width="130px" Height="60" CssClass="require"
                                            TextMode="MultiLine"></asp:TextBox>
                                    </td>
                                    <td width="100%">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="fieldNameLeft" nowrap>
                                        <asp:Label ID="lblFileTypeEdit" runat="server">文件类型</asp:Label>
                                    </td>
                                    <td class="fieldValue">
                                        <asp:DropDownList ID="drpDocTypeEdit" runat="server" Width="130px" CssClass="require"
                                            DESIGNTIMEDRAGDROP="252">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="fieldName" nowrap>
                                        <asp:Label ID="lblValidStatus" runat="server">是否有效</asp:Label>
                                    </td>
                                    <td class="fieldValue">
                                        <asp:DropDownList ID="drpValidStatus" runat="server" Width="130px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="fieldName" nowrap>
                                        <asp:CheckBox runat="server" ID="chbFileCheckedEdit" Checked="true" /><asp:Label
                                            ID="lblFormFileEdit" runat="server">文件</asp:Label>
                                    </td>
                                    <td class="fieldValue" colspan="3">
                                        <input id="fileUpload" type="file" size="45" name="File" runat="server">
                                    </td>
                                    <td>
                                        <asp:TextBox Visible="false" runat="server" ID="HiddenCheckedstatus" />
                                        <asp:TextBox Visible="false" runat="server" ID="HiddenUpuser" />
                                        <asp:TextBox Visible="false" runat="server" ID="HiddenUpfiledate" />
                                        <asp:TextBox Visible="false" runat="server" ID="HiddenServerFullName" />
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
            </td>
        </tr>
    </table>
     </ContentTemplate>
        <Triggers>
        <asp:PostBackTrigger ControlID="cmdAdd"/>
        <asp:PostBackTrigger ControlID="cmdSave"/>
        </Triggers>
    </asp:UpdatePanel>
    </form>
</body>
</html>
