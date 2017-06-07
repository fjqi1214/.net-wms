<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FASNReceiveMP.aspx.cs"
    Inherits="BenQGuru.eMES.Web.WarehouseWeb.FASNReceiveMP" %>

<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="cc2" Namespace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<%@ Register TagPrefix="uc1" TagName="eMESDate" Src="~/UserControl/DateTime/DateTime/eMESDate.ascx" %>
<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>FASNReceiveMP</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="<%=StyleSheet%>" rel="stylesheet">
    <%--    <script type="text/javascript" language="javascript">

        function OnKeyDownASN() {
            if (event.keycode == 13) {
                document.getElementById("btnASNNOEnter").onclick();
            }
        }
        function OnKeyDownSN() {
            if (event.keycode == 13) {
                document.getElementById("btnSNEnter").onclick();
            }
        }
        function OnKeyDownCarton() {
            if (event.keycode == 13) {
                document.getElementById("btnCartonEnter").onclick();
            }
        }

    </script>--%>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
    <table id="table1" height="100%" width="100%" runat="server">
        <tr class="moduleTitle">
            <td>
                <asp:Label ID="lblTitle" runat="server" CssClass="labeltopic">初检签收</asp:Label>
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
                                CssClass="textbox" onkeydown="if(event.keyCode==13) { cmdQueryASN.click() ;return false;}"></asp:TextBox>
                        </td>
                        <td class="fieldName">
                            <input class="submitImgButton" id="cmdQueryASN" type="submit" value="查 询" name="btnQuery"
                                runat="server" onserverclick="cmdQuery_ServerClick" />
                        </td>
                        <td class="fieldValue" nowrap>
                            <asp:CheckBox ID="chkEmergency" runat="server" Width="8px" Text="紧急物料" ForeColor="Red"
                                Checked="true" Visible="false" Enabled="false"></asp:CheckBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblSNOrCodeEdit" runat="server">SN</asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 159px">
                            <asp:TextBox ID="txtSNOrCodeEdit" runat="server" Width="150px" CssClass="textbox" onkeydown="if(event.keyCode==13) { cmdCheckSN.click() ;return false;}"></asp:TextBox>
                        </td>
                        <td class="fieldName">
                            <input class="submitImgButton" id="cmdCheckSN" type="submit" value="检 索" name="btnCheck"
                                runat="server" onserverclick="cmdCheck_ServerClick" />
                        </td>
                        <td width="5%">
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblCartonCode" runat="server">箱号编码</asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 159px">
                            <asp:TextBox ID="txtCartonCode" runat="server" Width="150px" CssClass="textbox" onkeydown="if(event.keyCode==13) { cmdSubmitCarton.click() ;return false;}"></asp:TextBox>
                        </td>
                        <td class="fieldName">
                            <input class="submitImgButton" id="cmdSubmitCarton" type="submit" value="提 交" name="btnSubmit"
                                runat="server" onserverclick="cmdSubmit_ServerClick" />
                                
                            <input class="submitImgButton" id="cmdCancel" type="submit" value="取 消" name="btnSubmit"
                                runat="server" onserverclick="cmdCancel_ServerClick" />
                        </td>
                        <td nowrap width="100%" />
                        <td class="fieldName">
                            <input class="submitImgButton" id="cmdClear" type="submit" value="清 空" name="btnClear"
                                runat="server" onserverclick="cmdClear_ServerClick" />
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
                <table class="edit" height="100%" cellpadding="0" width="100%">
                    <tr>
                        <td class="fieldNameLeft" style="height: 20px" nowrap>
                            <asp:Label ID="lblRejectQty" runat="server">拒绝箱数</asp:Label>
                        </td>
                        <td class="fieldNameLeft">
                            <asp:TextBox ID="txtRejectQty" runat="server" Width="120px" CssClass="textbox"></asp:TextBox>
                        </td>
                        <td class="fieldName">
                            <input class="submitImgButton" id="cmdRejectbt" type="submit" value="拒收" name="btnReject"
                                runat="server" onserverclick="cmdReject_ServerClick" />
                        </td>
                        <td>
                        </td>
                        <td class="fieldName">
                            <input class="submitImgButton" id="cmdReceivebt" type="submit" value="初检完成" name="btnReceive"
                                runat="server" onserverclick="cmdReceive_ServerClick" />
                        </td>
                        <td>
                        </td>
                        <td class="fieldName">
                            <input class="submitImgButton" id="cmdGiveinbt" type="submit" value="让步接收" name="btnGivein"
                                runat="server" visible="false" onserverclick="cmdGivein_ServerClick" />
                        </td>
                        <td colspan="4">
                        </td>
                        <td class="fieldNameLeft" style="height: 26px" nowrap>
                            <asp:Label ID="lblMemoEdit" runat="server">备注</asp:Label>
                        </td>
                        <td class="fieldValue" style="height: 26px">
                            <asp:TextBox ID="txtMemoEdit" runat="server" CssClass="textbox" Width="120px"></asp:TextBox>
                        </td>
                        <td nowrap width="100%">
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldNameLeft" style="height: 20px" nowrap>
                            <asp:Label ID="lblRejectDesc" runat="server">拒绝原因</asp:Label>
                        </td>
                        <td class="fieldNameLeft">
                            <asp:DropDownList ID="drpRejectDesc" runat="server" CssClass="require" Width="120px">
                            </asp:DropDownList>
                        </td>
                        <td colspan="4">
                        </td>
                        <td class="fieldNameLeft" style="height: 20px" nowrap>
                            <asp:Label ID="lblWaitDesc" runat="server">让步接收问题</asp:Label>
                        </td>
                        <td class="fieldValue" style="height: 20px">
                            <asp:DropDownList ID="drpWaitDesc" runat="server" CssClass="require" Width="120px">
                            </asp:DropDownList>
                        </td>
                        <td colspan="6">
                        </td>
                        <td nowrap width="100%">
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldNameLeft" style="height: 20px" nowrap>
                            <asp:Label ID="lblReceiveDesc" runat="server">入库指令接收详情： </asp:Label>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblBoxCount" runat="server">箱数： </asp:Label>
                        </td>
                        <td class="fieldNameLeft" style="height: 20px" nowrap>
                            <asp:Label ID="lblBoxCountEdit" runat="server"> </asp:Label>
                        </td>
                        <td class="fieldNameLeft" style="height: 20px" nowrap>
                            <asp:Label ID="lblReject" runat="server">，拒收： </asp:Label>
                        </td>
                        <td class="fieldNameLeft" style="height: 20px" nowrap>
                            <asp:Label ID="lblRejectQtyEdit" runat="server"></asp:Label>
                        </td>
                        <td class="fieldNameLeft" style="height: 20px" nowrap>
                            <asp:Label ID="lblAct" runat="server">，应收： </asp:Label>
                        </td>
                        <td class="fieldNameLeft" style="height: 20px" nowrap>
                            <asp:Label ID="lblActQty" runat="server"></asp:Label>
                        </td>
                        <td class="fieldNameLeft" style="height: 20px" nowrap>
                            <asp:Label ID="lblReceived" runat="server">，已接收： </asp:Label>
                        </td>
                        <td class="fieldNameLeft" style="height: 20px" nowrap>
                            <asp:Label ID="lblReceivedQty" runat="server"></asp:Label>
                        </td>
                        <td class="fieldNameLeft" style="height: 20px" nowrap>
                            <asp:Label ID="lblGivein" runat="server">，让步接收： </asp:Label>
                        </td>
                        <td class="fieldNameLeft" style="height: 20px" nowrap>
                            <asp:Label ID="lblGiveinQty" runat="server"></asp:Label>
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
                            <asp:Label ID="lblPicType" runat="server">图片类型:</asp:Label>
                        </td>
                        <td class="fieldValue" nowrap>
                            <asp:RadioButton ID="chkRejectpic" runat="server" Width="8px" Text="拒收图片" Checked="true"
                                OnCheckedChanged="rbtReject_SelectedIndexChanged" AutoPostBack="true"></asp:RadioButton>
                        </td>
                        <td width="8%">
                        </td>
                        <td class="fieldValue" nowrap>
                            <asp:RadioButton ID="chkGiveinpic" runat="server" Width="8px" Text="让步接收图片" OnCheckedChanged="rbtGivein_SelectedIndexChanged"
                                AutoPostBack="true"></asp:RadioButton>
                        </td>
                        <td width="10%">
                        </td>
                        <td class="fieldValue">
                            <input id="FileImport" name="FileImport" type="file" runat="server" style="width: 300px"
                                size="56" class="textbox" />
                        </td>
                        <td class="fieldNameLeft">
                            <input class="submitImgButton" id="cmdPicUpLoad" type="submit" value="图片上传" name="btnUpLoad"
                                runat="server" onserverclick="cmdUpLoad_ServerClick" />
                        </td>
                        <td width="10%">
                        </td>
                        <td class="fieldName">
                            <input class="submitImgButton" id="cmdPicDelete" type="submit" value="删 除" name="btnDelete"
                                runat="server" onserverclick="cmdDeletePic_ServerClick" />
                        </td>
                        <td class="fieldName">
                            <input class="submitImgButton" id="cmdReturn" type="submit" value="返回" name="cmdReturn"
                                runat="server" onserverclick="cmdReturn_ServerClick" visible="false" />
                        </td>
                        <td nowrap width="100%">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr height="100%">
            <td class="fieldGrid">
                <ig:WebDataGrid ID="gridWebGrid2" runat="server" Width="80%">
                </ig:WebDataGrid>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
