<%@ Page Language="c#" CodeBehind="FReworkSheetEP.aspx.cs" AutoEventWireup="True"
    Inherits="BenQGuru.eMES.Web.Rework.FReworkSheetEP" %>

<%@ Register TagPrefix="ss1" Namespace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<%@ Register TagPrefix="cc2" Namespace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>FReworkSheetEP</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="<%=StyleSheet%>" rel="stylesheet">
    <script src="../Scripts/jquery-1.9.1.js" type="text/javascript"></script>
    <script>
        $(function () {
            var height = $(window).height();
            if (height < $(document).height()) {
                height = $(document).height();
            }

            $("#tableMain").height(height);
        })
            
    </script>
</head>
<body>
    <form id="Form1" method="post" runat="server">
    <table id="tableMain"  width="100%" border="0">
        <tr class="moduleTitle">
            <td>
                <asp:Label ID="lblTitle" runat="server" CssClass="labeltopic" DESIGNTIMEDRAGDROP="205">返工需求单维护</asp:Label>
            </td>
        </tr>
        <tr>
            <td class="fieldGrid" valign="top" height="1">
                <table class="edit" id="Table2" cellspacing="1" cellpadding="1" width="100%" border="0">
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblReworkMo" runat="server" DESIGNTIMEDRAGDROP="263">返工需求单编号</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtReworkSheetCode" runat="server" CssClass="require" Width="150px"></asp:TextBox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblReTypeQuery" runat="server">返工类型</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:DropDownList ID="drpReworkType" runat="server" DESIGNTIMEDRAGDROP="24" Width="150px"
                                AutoPostBack="True" OnSelectedIndexChanged="drpReworkType_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td class="fieldValue" width="100%">
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblItemCodeQuery" runat="server">产品代码</asp:Label>
                        </td>
                        <td class="fieldValue" id="itemSingleSelect">
                            <ss1:selectsingletabletextbox id="txtItemQuery" runat="server" width="150px" type="singleitem">
                            </ss1:selectsingletabletextbox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblMOQuery" runat="server">工单</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <ss1:selectsingletabletextbox id="txtMOQuery" runat="server" width="150px" type="singlereworkmo">
                            </ss1:selectsingletabletextbox>
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblReworkSourceCode" runat="server">返工来源</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:DropDownList ID="drpReworkSource" runat="server" DESIGNTIMEDRAGDROP="505" Width="150px">
                            </asp:DropDownList>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblReworkQty" runat="server">计划返工数量</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtReworkQty" runat="server" CssClass="textbox" Width="150px">0</asp:TextBox>
                        </td>
                        <td class="fieldValue">
                        </td>
                    </tr>
                    <tr style="display: none;">
                        <td class="fieldNameLeft">
                            <asp:Label ID="lblReworkHC" runat="server">计划人力</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtReworkHC" runat="server" CssClass="textbox" Width="150px">0</asp:TextBox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblDepartment" runat="server">返工部门</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtDepartment" runat="server" CssClass="textbox" Width="150px"></asp:TextBox>
                        </td>
                        <td class="fieldValue">
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldNameLeft">
                            <asp:Label ID="lblRejectLot" runat="server">判退批号</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <cc2:selectabletextbox id="txtOQCLotQuery" runat="server" width="90px" type="rejectlot"
                                cssclass="textbox" Readonly="true">
                            </cc2:selectabletextbox>
                        </td>
                        <td class="fieldValue" nowrap colspan="2">
                            <asp:CheckBox runat="server" ID="chkNeedConfirmFlow" Text="需要返工签核" Checked="false" />
                        </td>
                        <td class="fieldValue">
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblDutyCodeQuery" runat="server">责任别</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <cc2:selectabletextbox id="txtDutyCodeQuery" runat="server" type="singleduty" readonly="false"
                                width="130px">
                            </cc2:selectabletextbox>
                        </td>
                        <td class="fieldValue">
                        </td>
                        <td class="fieldValue">
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblMUserEdit" runat="server">维护人员</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtUser" runat="server" CssClass="textbox" Width="150px" ReadOnly="True"></asp:TextBox>
                        </td>
                        <td class="fieldName">
                            <asp:Label ID="lblMDateEdit" runat="server">维护日期</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtDate" runat="server" CssClass="textbox" Width="150px" ReadOnly="True"></asp:TextBox>
                        </td>
                        <td class="fieldValue">
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblReworkReason" runat="server">返工原因</asp:Label>
                        </td>
                        <td class="fieldValue" colspan="3">
                            <asp:TextBox runat="server" ID="txtReworkReason" CssClass="textbox" TextMode="MultiLine"
                                Width="400px" Height="65px"></asp:TextBox>
                        </td>
                        <td class="fieldValue">
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblReasonAnalyse" runat="server">不良原因分析</asp:Label>
                        </td>
                        <td class="fieldValue" colspan="3">
                            <asp:TextBox runat="server" ID="txtReasonAnalyse" CssClass="textbox" TextMode="MultiLine"
                                Width="400px" Height="65px"></asp:TextBox>
                        </td>
                        <td class="fieldValue">
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblSolution" runat="server">纠正措施</asp:Label>
                        </td>
                        <td class="fieldValue" colspan="3">
                            <asp:TextBox runat="server" ID="txtSolution" CssClass="textbox" TextMode="MultiLine"
                                Width="400px" Height="65px"></asp:TextBox>
                        </td>
                        <td class="fieldValue">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="toolBar">
            <td valign="bottom" >
                <table class="toolBar" id="Table5">
                    <tr>
                        <td class="toolBar" valign="bottom">
                            <input class="submitImgButton" id="cmdSave" type="submit" value="保 存" name="cmdSave"
                                runat="server" onserverclick="cmdSave_ServerClick">
                        </td>
                        <td valign="bottom">
                            <input class="submitImgButton" id="cmdCancel" type="submit" value="取 消" name="cmdCancel"
                                runat="server" onserverclick="cmdCancel_ServerClick">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
