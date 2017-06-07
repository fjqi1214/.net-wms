<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FAlertErrorMP.aspx.cs" Inherits="BenQGuru.eMES.Web.Alert.FAlertErrorMP" %>
<%@ Register TagPrefix="cc2" NameSpace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<head>
    <title>预警项次设定</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR" />
    <meta content="C#" name="CODE_LANGUAGE" />
    <meta content="JavaScript" name="vs_defaultClientScript" />
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
    <link href="<%=StyleSheet%>" rel="stylesheet" />
</head>
<body>
    <form id="Form1" method="post" runat="server">
        <table style="height: 100%; width: 100%;">
            <tr class="moduleTitle">
                <td>
                    <asp:Label ID="lblTitle" runat="server" CssClass="labeltopic">预警项次设定</asp:Label></td>
            </tr>
            <tr>
                <td>
                    <table class="query" style="height: 50%; width: 100%;">
                        <tr>
                            <td class="fieldNameLeft" nowrap="nowrap">
                                <asp:Label ID="lblAlertItemSequence" runat="server">预警项次</asp:Label>
                            </td>
                            <td class="fieldValue" nowrap="nowrap" style="width:222px">
                                <asp:TextBox ID="txtAlertItemSequence" runat="server" CssClass="textbox" Width="150px" ReadOnly="true">
                                </asp:TextBox>
                            </td>
                            
                            <td class="fieldNameLeft" nowrap="nowrap">
                                <asp:Label ID="lblAlertItemDesc" runat="server">项次描述</asp:Label>
                            </td>
                            <td class="fieldValue" style="width: 159px">
                                <asp:TextBox ID="txtAlertItemDesc" runat="server" CssClass="textbox" Width="250px" ReadOnly="true">
                                </asp:TextBox>
                            </td>
                            
                            <td style="width:100%;">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            
            
            <tr>
                <td>
                    <table class="query" style="height: 50%; width: 100%;">
                        <tr>
                            <td nowrap="nowrap" colspan="5" style="text-align:left;font-weight:bold;">
                                <asp:Label ID="lblAlertError" runat="server">故障预警</asp:Label>
                            </td>
                        </tr>
                        
                        <tr>
                            <td class="fieldNameLeft" nowrap="nowrap">
                                <asp:Label ID="lblItemType" runat="server">产品类型</asp:Label></td>
                            <td class="fieldValue" colspan="2">
                                <asp:DropDownList ID="ddlItemType" runat="server" CssClass="require" Width="150px">
                                </asp:DropDownList>
                            </td>
                            
                            <td class="fieldNameLeft" nowrap="nowrap">
                                <asp:Label ID="lblNGCodeQuery" runat="server">不良代码</asp:Label></td>
                            <td class="fieldValue">
                                <cc2:selectabletextbox id="txtErrorCode" runat="server" Type="errorcodewithgroup" Readonly="false" CanKeyIn="false" Width="250px" CssClass="require"></cc2:selectabletextbox>
                            </td>
                            
                            <td style="width:100%;">
                            </td>
                        </tr>
                        <tr>
                            <td class="fieldNameLeft" nowrap="nowrap">
                                <asp:Label ID="lblTimeDimension" runat="server">时间维度</asp:Label></td>
                            <td nowrap="nowrap" style="width: 120px">
                                <asp:radiobuttonlist id="rblTimeDimensionGroup" runat="server" RepeatDirection="Horizontal"></asp:radiobuttonlist>
                                
                            </td>
                            
                            <td nowrap="nowrap" style="width: 100px">
                                <asp:CheckBox id="chbLineDivision" runat="server" Text="是否区分线别"></asp:CheckBox>
                            </td>
                            
                            <td class="fieldNameLeft" nowrap="nowrap">
                                <asp:Label ID="lblAlertStandard" runat="server">预警标准</asp:Label>
                            </td>
                            <td class="fieldValue">
                                <asp:TextBox ID="txtAlertStandard" runat="server" CssClass="require" Width="131px">
                                </asp:TextBox>
                            </td>
                            
                        </tr>
                        <tr>
                            <td class="fieldNameLeft" nowrap="nowrap">
                                <asp:Label ID="lblDealMethod" runat="server">处置方式</asp:Label></td>
                            <td class="fieldValue" colspan="4" nowrap="nowrap">
                                <asp:CheckBox id="chbGenerateNotice" runat="server" Checked="true" Text="产生预警通告" OnCheckedChanged="chbGenerateNotice_CheckedChanged" autopostback="true"></asp:CheckBox>
                                <asp:CheckBox id="chbSendMail" runat="server" Text="发送邮件" Checked="false"></asp:CheckBox>
                                <input class="submitImgButton" id="cmdMailSetup" type="submit" value="邮件设定" name="cmdMailSetup"
                                    runat="server" onserverclick="cmdMailSetup_ServerClick" />
                                <asp:CheckBox id="chbLinePause" runat="server" Text="停线控制"/>
                            </td>
                            
                        </tr>
                    </table>
                </td>
            </tr>
            
            <tr height="100%">
                <td></td>
            </tr>
            
            <tr class="toolBar">
                <td>
                    <table class="toolBar">
                        <tr>
                            <td class="toolBar">
                                <input class="submitImgButton" id="cmdSave" type="submit" value="保 存" name="cmdSave"
                                    runat="server" onserverclick="cmdSave_ServerClick" /></td>
                            <td class="toolBar"><INPUT class="submitImgButton" id="cmdReturn" type="submit" value="返 回" name="cmdReturn"
										runat="server" onserverclick="cmdReturn_ServerClick"></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
