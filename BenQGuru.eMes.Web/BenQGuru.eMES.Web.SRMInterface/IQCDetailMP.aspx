<%@ Page Language="C#" AutoEventWireup="true" Codebehind="IQCDetailMP.aspx.cs" Inherits="BenQGuru.eMES.Web.SRMInterface.IQCDetailMP" %>

<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="cc2" Namespace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v9.1, Version=9.1.20091.2101, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="uc1" TagName="eMESDate" Src="~/UserControl/DateTime/DateTime/eMESDate.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>IQCHeadMP</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="<%=StyleSheet%>" rel="stylesheet">
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table height="100%" width="100%">
            <tr class="moduleTitle">
                <td>
                    <asp:Label ID="lblTitle" runat="server" CssClass="labeltopic"> IQC检验结果维护</asp:Label></td>
            </tr>
            <tr>
                <td>
                    <table class="query" height="100%" width="100%">
                        <tr>
                            <td class="fieldNameLeft" style="height: 26px" nowrap>
                                <asp:Label ID="lblASNNo" runat="server">送货单号</asp:Label></td>
                            <td class="fieldValue" style="height: 26px">
                                <asp:TextBox ID="txtASNNo" runat="server" ReadOnly="true" CssClass="require" Width="130px"></asp:TextBox></td>
                            <td class="fieldNameLeft" style="height: 26px" nowrap>
                                <asp:Label ID="lblIQCNo" runat="server">送检单号</asp:Label></td>
                            <td class="fieldValue" style="height: 26px">
                                <asp:TextBox ID="txtIQCNo" runat="server" CssClass="require" ReadOnly="true" Width="130px"></asp:TextBox></td>
                            <td class="fieldName" style="display:none">
                                <input class="submitImgButton" id="cmdQuery" style="display:none" type="submit"
                                    value="查 询" name="btnQuery" runat="server"></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr height="100%">
                <td class="fieldGrid">
                    <igtbl:UltraWebGrid ID="gridWebGrid" runat="server" Width="100%" Height="100%">
                        <DisplayLayout ColWidthDefault="" StationaryMargins="Header" AllowSortingDefault="Yes"
                            RowHeightDefault="20px" Version="2.00" SelectTypeRowDefault="Single" SelectTypeCellDefault="Single"
                            HeaderClickActionDefault="SortSingle" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
                            CellPaddingDefault="4" RowSelectorsDefault="No" Name="webGrid" TableLayout="Fixed">
                            <AddNewBox>
                                <Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

									</Style>
                            </AddNewBox>
                            <Pager>
                                <Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

									</Style>
                            </Pager>
                            <HeaderStyleDefault BorderWidth="1px" Font-Size="12px" Font-Bold="True" BorderColor="White"
                                BorderStyle="Dashed" HorizontalAlign="Left" BackColor="#ABABAB">
                                <BorderDetails ColorTop="White" WidthLeft="1px" ColorBottom="White" WidthTop="1px"
                                    ColorRight="White" ColorLeft="White"></BorderDetails>
                            </HeaderStyleDefault>
                            <RowSelectorStyleDefault BackColor="#EBEBEB">
                            </RowSelectorStyleDefault>
                            <FrameStyle Width="100%" BorderWidth="0px" Font-Size="12px" Font-Names="Verdana"
                                BorderColor="#ABABAB" BorderStyle="Groove" Height="100%">
                            </FrameStyle>
                            <FooterStyleDefault BorderWidth="0px" BorderStyle="Groove" BackColor="LightGray">
                                <BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
                            </FooterStyleDefault>
                            <ActivationObject BorderStyle="Dotted">
                            </ActivationObject>
                            <EditCellStyleDefault VerticalAlign="Middle" BorderWidth="1px" BorderColor="Black"
                                BorderStyle="None">
                                <Padding Bottom="1px"></Padding>
                            </EditCellStyleDefault>
                            <RowAlternateStyleDefault BackColor="White">
                            </RowAlternateStyleDefault>
                            <RowStyleDefault VerticalAlign="Middle" BorderWidth="1px" BorderColor="#D7D8D9" BorderStyle="Solid"
                                HorizontalAlign="Left">
                                <Padding Left="3px"></Padding>
                                <BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
                            </RowStyleDefault>
                            <ImageUrls ImageDirectory="/ig_common/WebGrid3/"></ImageUrls>
                        </DisplayLayout>
                        <Bands>
                            <igtbl:UltraGridBand>
                            </igtbl:UltraGridBand>
                        </Bands>
                    </igtbl:UltraWebGrid></td>
            </tr>
            <tr class="normal">
                <td>
                    <table height="100%" cellpadding="0" width="100%">
                        <tr>
                            <td>
                                <asp:CheckBox ID="chbSelectAll" runat="server" Text="全选" AutoPostBack="True"></asp:CheckBox></td>
                            <td class="smallImgButton">
                                <input class="deleteButton" id="cmdDelete" type="submit" value="  " name="cmdDelete"
                                    runat="server">
                                |
                            </td>
                            <td>
                                <cc1:pagersizeselector id="pagerSizeSelector" runat="server">
                                </cc1:pagersizeselector></td>
                            <td align="right">
                                <cc1:PagerToolbar id="pagerToolBar" runat="server">
                                </cc1:PagerToolbar></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr class="normal">
                <td>
                    <table class="edit" height="100%" cellpadding="0" width="100%">
                        <tr>
                            <td class="fieldNameLeft" style="height: 26px" nowrap>
                                <asp:Label ID="lblIQCLineNoEdit" runat="server">送检单行号</asp:Label></td>
                            <td class="fieldValue" style="height: 26px">
                                <asp:TextBox ID="txtIQCLineNo" runat="server" CssClass="require" Width="130px"></asp:TextBox></td>
                            <td class="fieldName" nowrap>
                                <asp:Label ID="lblItemCodeEditSRM" runat="server">物料代码</asp:Label></td>
                            <td class="fieldValue">
                                <asp:TextBox ID="txtItemNo" runat="server" Width="130px" CssClass="textbox"></asp:TextBox></td>
                            <td class="fieldNameLeft" style="height: 26px" nowrap>
                                <asp:Label ID="lblItemNameEditSRM" runat="server">物料描述</asp:Label></td>
                            <td class="fieldValue" style="height: 26px">
                                <asp:TextBox ID="txtItemName" runat="server" Width="130px" CssClass="textbox"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="fieldName" nowrap>
                                <asp:Label ID="lblPlanDate" runat="server" Visible="false">排程日期</asp:Label></td>
                            <td class="fieldValue" visible="false">
                                <uc1:eMESDate id="datePlanDate" CssClass="require" width="150" runat="server">
                                </uc1:eMESDate></td>
                            <td class="fieldName" nowrap>
                                <asp:Label ID="lblPlanQty" runat="server" Visible="false">排程数量</asp:Label></td>
                            <td class="fieldValue">
                                <asp:TextBox ID="txtPlanQty" runat="server" Width="130px" CssClass="textbox" Visible="false"></asp:TextBox></td>
                            <td class="fieldName" nowrap>
                                <asp:Label ID="lblUnit" runat="server">单位</asp:Label></td>
                            <td class="fieldValue">
                                <asp:TextBox ID="txtUnit" runat="server" Width="130px" CssClass="textbox"></asp:TextBox>
                            </td>
                        </tr>
            <tr>
                <td class="fieldName" nowrap>
                    <asp:Label ID="lblOrderNo" runat="server">代购订单号</asp:Label></td>
                <td class="fieldValue">
                    <asp:TextBox ID="txtOrderNo" runat="server" Width="130px" CssClass="textbox"></asp:TextBox></td>
                <td class="fieldNameLeft" style="height: 26px" nowrap>
                    <asp:Label ID="lblOrderLine" runat="server">订单行</asp:Label></td>
                <td class="fieldValue" style="height: 26px">
                    <asp:TextBox ID="txtOrderLine" runat="server" Width="130px" CssClass="textbox"></asp:TextBox></td>
                <td class="fieldName" nowrap>
                    <asp:Label ID="lblReceiveQty" runat="server">收货数量</asp:Label></td>
                <td class="fieldValue">
                    <asp:TextBox ID="txtReceiveQty" runat="server" Width="130px" CssClass="textbox"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="fieldName" nowrap>
                    <asp:Label ID="lblCheckStatus" runat="server">检验结果</asp:Label></td>
                <td class="fieldValue">
                
                    <asp:RadioButton ID="lblStatusWaitCheck" AutoPostBack="true"  GroupName="checkstatus" runat="server" Text="待检" OnCheckedChanged="lblStatusWaitCheck_CheckedChanged" />
                    <asp:RadioButton ID="lblStatusQualified"  GroupName="checkstatus" runat="server" Text="合格"/>
                    <asp:RadioButton ID="lblStatusUnqualified" AutoPostBack="true"  GroupName="checkstatus" runat="server" Text="不合格" OnCheckedChanged="lblStatusUnqualified_CheckedChanged" />
                    <asp:CheckBox ID="lblStatusSTS" AutoPostBack="true" runat="server" Text="免检" OnCheckedChanged="lblStatusSTS_CheckedChanged" />
                </td>
                <td class="fieldName" nowrap>
                    <asp:Label ID="lblSampleQty" runat="server">抽样数</asp:Label></td>
                <td class="fieldValue">
                    <asp:TextBox ID="txtSampleQty" runat="server" Width="130px" CssClass="textbox"></asp:TextBox></td>
                <td class="fieldName" nowrap>
                    <asp:Label ID="lblItemAttribute" runat="server">来料特性</asp:Label></td>
                <td class="fieldValue">
                    <asp:DropDownList ID="drpItemAttribute" runat="server" Width="130px" AutoPostBack="False" OnLoad="drpAttribute_Load"
                        >
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="fieldNameLeft" style="height: 26px" nowrap>
                    <asp:Label ID="lblNGQty" runat="server">不良数</asp:Label></td>
                <td class="fieldValue" style="height: 26px">
                    <asp:TextBox ID="txtNGQty" runat="server" CssClass="require" Width="130px"></asp:TextBox></td>
                <td class="fieldName" nowrap>
                    <asp:Label ID="lblMemoEx" runat="server">其他说明</asp:Label></td>
                <td class="fieldValue">
                    <asp:TextBox ID="txtMemoEx" runat="server" Width="130px" CssClass="textbox"></asp:TextBox></td>
                <td class="fieldNameLeft" style="height: 26px" nowrap>
                    <asp:Label ID="lblMemo" runat="server">Memo</asp:Label></td>
                <td class="fieldValue" style="height: 26px">
                    <asp:TextBox ID="txtMemo" runat="server" Width="130px" CssClass="textbox"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="fieldName" nowrap>
                    <asp:Label ID="lblPIC" runat="server">PIC确认</asp:Label></td>
                <td class="fieldValue">
                    <asp:TextBox ID="txtPIC" runat="server" Width="130px" CssClass="textbox"></asp:TextBox></td>
                <td class="fieldName" nowrap>
                    <asp:Label ID="lblAction" runat="server">永久性措施说明</asp:Label></td>
                <td class="fieldValue">
                    <asp:TextBox ID="txtAction" runat="server" Width="130px" CssClass="textbox"></asp:TextBox></td>
                <td class="fieldName" nowrap>
                </td>
                <td class="fieldValue">
                </td>
            </tr>
            <tr style="display:none"><td><asp:TextBox ID="txtSTDStatus" runat="server" Width="130px" CssClass="textbox"></asp:TextBox><asp:TextBox ID="txtIQCStatus" runat="server" Width="130px" CssClass="textbox"></asp:TextBox></td></tr>
        </table>
        </td> </tr>
        <tr class="toolBar">
            <td>
                <table class="toolBar">
                    <tr>
                        <td class="toolBar">
                            <input class="submitImgButton" id="cmdAdd" type="submit" value="新 增" name="cmdAdd"
                                runat="server"></td>
                        <td class="toolBar">
                            <input class="submitImgButton" id="cmdSave" type="submit" value="保 存" name="cmdSave"
                                runat="server"></td>
                        <td>
                            <input class="submitImgButton" id="cmdCancel" type="submit" value="取 消" name="cmdCancel"
                                runat="server"></td>
                    </tr>
                </table>
            </td>
        </tr>
        </table>
    </form>
</body>
</html>
