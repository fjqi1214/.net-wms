<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FAlertMailSettingMP.aspx.cs"
    Inherits="BenQGuru.eMES.Web.Alert.FAlertMailSettingMP" %>

<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="cc2" Namespace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR" />
    <meta content="C#" name="CODE_LANGUAGE" />
    <meta content="JavaScript" name="vs_defaultClientScript" />
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
    <link href="<%=StyleSheet%>" rel="stylesheet" />
    <style>
        .selectUser
        {
            border: 0px;
            font-size: 12px;
            background-image: url(<%=VirtualHostRoot %>SKIN\Image\right.gif);
            width: 24px;
            cursor: hand;
            background-repeat: no-repeat;
            height: 15px;
            background-color: transparent;
        }
    </style>
    <script language="javascript" type="text/javascript">
        function openUserEmail() {
            var arg = new Object();
            arg.File = "<%=VirtualHostRoot %>" + "SelectQuery/FUserMailSP.aspx";
            arg.Codes = "";
            arg.Others = "";
            arg.DataObject = "";
            var result = window.showModalDialog("<%=VirtualHostRoot %>" + "SelectQuery/FFrameSP.htm", arg, "dialogWidth:800px;dialogHeight:600px;scroll:none;help:no;status:no");

            if (result == null || result == undefined || result == "") {
                document.getElementById("hiddenSelectedID").value = "";
            }
            else {
                document.getElementById("hiddenSelectedID").value = result;
            }
        }    
    </script>
</head>
<body>
    <form id="Form1" method="post" runat="server">
    <table id="tblalert" style="height: 100%; width: 100%;" runat="server">
        <tr class="moduleTitle">
            <td>
                <asp:Label ID="lblTitle" runat="server" CssClass="labeltopic"></asp:Label>
            </td>
            <td>
                <input type="hidden" id="hiddenSelectedID" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <table class="query" style="height: 100%; width: 100%;">
                    <tr>
                        <td class="fieldNameLeft" nowrap="nowrap">
                            <asp:Label ID="lblAlertItemSequence" runat="server">预警项次</asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 100px">
                            <asp:TextBox ID="txtAlertItemSequence" runat="server" CssClass="textbox" Width="100px"
                                ReadOnly="true"></asp:TextBox>
                        </td>
                        <td class="fieldNameLeft" nowrap="nowrap">
                            <asp:Label ID="lblAlertItemDescEdit" runat="server">描述信息</asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 150px">
                            <asp:TextBox ID="txtAlertItemDesc" runat="server" CssClass="textbox" Width="150px"
                                ReadOnly="true"></asp:TextBox>
                        </td>
                         <td style="width: 100%;">
                        </td>
                        
                       <%-- <td class="fieldNameLeft" nowrap="nowrap">
                            <asp:Label ID="lblAlertMailSubject" runat="server">邮件主题</asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 200px">
                            <asp:TextBox ID="txtAlertMailSubject" runat="server" CssClass="require" Width="200px"></asp:TextBox>
                        </td>
                        <td style="width: 100%;">
                        </td>
                        <td class="fieldName">
                            <input class="submitImgButton" id="cmdSaveMailSubject" type="submit" value="保 存"
                                name="btnSaveMailSubject" runat="server" onserverclick="cmdSaveMailSubject_ServerClick" />
                        </td>--%>
                    </tr>
                </table>
            </td>
        </tr>
        <tr style="height: 100%;">
            <td class="fieldGrid">
                <ig:webdatagrid id="gridWebGrid" runat="server" width="100%">
                </ig:webdatagrid>
            </td>
        </tr>
        <tr class="normal">
            <td>
                <table cellpadding="0" style="height: 100%; width: 100%;">
                    <tr>
                        <td class="smallImgButton">
                            <input class="gridExportButton" id="cmdGridExport" type="submit" value="  " name="cmdCancel"
                                runat="server" onserverclick="cmdGridExport_ServerClick" />
                        </td>
                        <td class="smallImgButton">
                            <input class="deleteButton" id="cmdDelete" type="submit" value="  " name="cmdDelete"
                                runat="server" onserverclick="cmdDelete_ServerClick" />
                        </td>
                        <td>
                            <cc1:pagersizeselector id="pagerSizeSelector" runat="server">
                            </cc1:pagersizeselector>
                        </td>
                        <td align="right">
                            <cc1:pagertoolbar id="pagerToolBar" runat="server" pagesize="20">
                            </cc1:pagertoolbar>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="normal">
            <td>
                <table class="edit" cellpadding="0" style="height: 100%; width: 100%;">
                    
                    <tr>
                        <td class="fieldNameLeft" nowrap="nowrap">
                            <asp:Label ID="lblAlertMailRecipients" runat="server">收件人</asp:Label>
                        </td>
                        <td class="fieldValue" colspan="6">
                            <asp:TextBox ID="txtAlertMailRecipients" runat="server" CssClass="require" Width="537px"
                                TextMode="MultiLine" Height="60px"></asp:TextBox>
                            <input type="button" id="cmdOpenEmail" name="cmdOpenEmail" runat="Server" class="selectUser"
                                onserverclick="cmdOpenEmail_ServerClick" onclick="openUserEmail();" />
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
                                runat="server" onserverclick="cmdAdd_ServerClick" />
                        </td>
                        <td class="toolBar">
                            <input class="submitImgButton" id="cmdSave" type="submit" value="保 存" name="cmdSave"
                                runat="server" onserverclick="cmdSave_ServerClick" />
                        </td>
                        <td class="toolBar">
                            <input class="submitImgButton" id="cmdCancel" type="submit" value="取 消" name="cmdCancel"
                                runat="server" onserverclick="cmdCancel_ServerClick" />
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
