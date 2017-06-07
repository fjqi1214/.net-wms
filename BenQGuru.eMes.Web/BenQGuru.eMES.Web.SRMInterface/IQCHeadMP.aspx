<%@ Page Language="C#" AutoEventWireup="true" Codebehind="IQCHeadMP.aspx.cs" Inherits="BenQGuru.eMES.Web.SRMInterface.IQCHeadMP" %>

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
                            <td class="fieldNameLeft" nowrap>
                                <asp:Label ID="lblIQCNoQuery" runat="server">送检单号</asp:Label></td>
                            <td class="fieldValue" style="width: 159px">
                                <asp:TextBox ID="txtIQCNoQuery" runat="server" Width="150px" DESIGNTIMEDRAGDROP="257"
                                    CssClass="textbox"></asp:TextBox></td>
                            <td class="fieldNameLeft" nowrap>
                                <asp:Label ID="lblASNNoQuery" runat="server">送货单号</asp:Label></td>
                            <td class="fieldValue" style="width: 159px">
                                <asp:TextBox ID="txtASNNoQuery" runat="server" Width="150px" DESIGNTIMEDRAGDROP="257"
                                    CssClass="textbox"></asp:TextBox></td>
                            <td class="fieldValue" colspan="3">
                                <asp:RadioButton ID="lblStatusWaitCheckQuery" GroupName="stat" Text="Wait Check" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:RadioButton
                                    ID="lblStatusClosedQuery" runat="server" GroupName="stat" Text="Closed" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="lblROHSQuery"
                                        runat="server" Text="ROHS" />
                            </td>
                        </tr>
                        <tr>
                            <td class="fieldNameLeft" nowrap>
                                <asp:Label ID="lblVendorCodeQuery" runat="server">供应商代码</asp:Label></td>
                            <td class="fieldValue" style="width: 159px">
                                <asp:TextBox ID="txtVendorCodeQuery" runat="server" Width="150px" DESIGNTIMEDRAGDROP="257"
                                    CssClass="textbox"></asp:TextBox></td>
                            <td class="fieldNameLeft" nowrap>
                                <asp:Label ID="lblAppDateFromQuery" runat="server">送检日期 从</asp:Label></td>
                            <td class="fieldValue" style="width: 159px">
                                <uc1:eMESDate id="dateAppDateFromQuery" CssClass="require" width="150" runat="server">
                                </uc1:eMESDate></td>
                            <td class="fieldNameLeft" nowrap>
                                <asp:Label ID="lblAppDateToQuery" runat="server">到</asp:Label></td>
                            <td class="fieldValue" style="width: 159px">
                                <uc1:eMESDate id="dateAppDateToQuery" CssClass="require" width="150" runat="server">
                                </uc1:eMESDate></td>
                            <td class="fieldName">
                                <input class="submitImgButton" id="cmdQuery" type="submit" value="查 询" name="btnQuery"
                                    runat="server"></td>
                        </tr>
        </table>
        </td> </tr>
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
                            <asp:Label ID="lblIQCNoEdit" runat="server">送检单号</asp:Label></td>
                        <td class="fieldValue" style="height: 26px">
                            <asp:TextBox ID="txtIQCNoEdit" runat="server" CssClass="require" Width="130px"></asp:TextBox></td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblASNNoEdit" runat="server">ASN单号</asp:Label></td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtASNNoEdit" runat="server" Width="130px" CssClass="textbox"></asp:TextBox></td>
                        <td class="fieldNameLeft" style="height: 26px" nowrap>
                            <asp:Label ID="lblVendorCodeEdit" runat="server">供应商代码</asp:Label></td>
                        <td class="fieldValue" style="height: 26px">
                            <asp:TextBox ID="txtVendorCodeEdit" runat="server" Width="130px" CssClass="textbox"></asp:TextBox></td>
                             </tr>
                    <tr>
                        <td class="fieldName" nowrap>
                        
                            <asp:Label ID="lblVendorNameEdit" runat="server">供应商名称</asp:Label></td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtVendorNameEdit" runat="server" Width="130px" CssClass="textbox"></asp:TextBox></td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblInvUserEdit" runat="server">保管员</asp:Label></td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtInvUserEdit" runat="server" Width="130px" CssClass="textbox"></asp:TextBox></td>
                        <td class="fieldName" nowrap>
                            <asp:CheckBox ID="lblROHSEdit" Text="ROHS" runat="server" />
                        </td>
                        <td class="fieldValue">
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblApplicantEdit" runat="server">送检员</asp:Label></td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtApplicantEdit" runat="server" Width="130px" CssClass="textbox"></asp:TextBox></td>
                        <td class="fieldNameLeft" style="height: 26px" nowrap>
                            <asp:Label ID="lblInspectorEdit" runat="server">检验员</asp:Label></td>
                        <td class="fieldValue" style="height: 26px">
                            <asp:TextBox ID="txtInspectorEdit" runat="server" Width="130px" CssClass="textbox"></asp:TextBox></td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblLotNoEdit" runat="server">检验批号</asp:Label></td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtLotNoEdit" runat="server" Width="130px" CssClass="textbox"></asp:TextBox></td>
                             </tr>
                    <tr>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblInspectDateEdit" runat="server">检验日期</asp:Label></td>
                        <td class="fieldValue">
                            <uc1:eMESDate id="dateInspectDateEdit" CssClass="require" width="150" runat="server">
                            </uc1:eMESDate></td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblProductDateEdit" runat="server">生产日期</asp:Label></td>
                        <td class="fieldValue">
                            <uc1:eMESDate id="dateProductDateEdit" CssClass="require" width="150" runat="server">
                            </uc1:eMESDate></td>
                        <td class="fieldName" nowrap>
                        </td>
                        <td class="fieldValue">
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldNameLeft" style="height: 26px" nowrap>
                            <asp:Label ID="lblStandardEdit" runat="server">检验依据</asp:Label></td>
                        <td class="fieldValue" style="height: 26px">
                            <asp:TextBox ID="txtStandardEdit" runat="server" CssClass="require" Width="130px"></asp:TextBox></td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblMethodEdit" runat="server">检验形式</asp:Label></td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtMethodEdit" runat="server" Width="130px" CssClass="textbox"></asp:TextBox></td>
                        <td class="fieldNameLeft" style="height: 26px" nowrap>
                            <asp:Label ID="lblResultEdit" runat="server">检验结果</asp:Label></td>
                        <td class="fieldValue" style="height: 26px">
                            <asp:TextBox ID="txtResultEdit" runat="server" Width="130px" CssClass="textbox"></asp:TextBox></td>
                             </tr>
                    <tr>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblReceiveDateEdit" runat="server">签收日期</asp:Label></td>
                        <td class="fieldValue">
                            <uc1:eMESDate id="dateReceiveDateEdit" CssClass="require" width="150" runat="server">
                            </uc1:eMESDate></td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblPICEdit" runat="server">负责人</asp:Label></td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtPICEdit" runat="server" Width="130px" CssClass="textbox"></asp:TextBox></td>
                        <td class="fieldName" nowrap>
                        </td>
                        <td class="fieldValue">
                        </td>
                    </tr>
                    <tr style="display:none">
                        <td><asp:TextBox ID="txtStatus" runat="server" Width="130px" CssClass="textbox"></asp:TextBox></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="toolBar">
            <td>
                <table class="toolBar">
                    <tr>
                        <td class="toolBar">
                            <input class="submitImgButton" id="cmdAdd" type="submit" onclick="if (document.getElementById('txtASNNoEdit').value=='') {if (!confirm('ASN单号为空，是否自动放入送检单号内容？')) {return false;} else {document.getElementById('txtASNNoEdit').value=document.getElementById('txtIQCNoEdit').value}} " value="新 增" name="cmdAdd"
                                runat="server"></td>
                        <td class="toolBar">
                            <input class="submitImgButton" id="cmdSave" type="submit" value="保 存" name="cmdSave"
                                runat="server"></td>
                        <td class="toolBar">
                            <input class="submitImgButton"  id="cmdCancel" type="submit" value="取 消" name="cmdCancel"
                                runat="server"></td>
                        <td class="toolBar">
                            <asp:Button ID="ButtonClose" CssClass="submitImgButton" Style="font-weight: bold; background-color: Transparent;
background-image: url(/hisense/Skin/Image/ButtonGray.gif);"  runat="server" Text="Close" OnClick="ButtonClose_Click" onmouseout="document.getElementById('ButtonClose').style.backgroundImage='url(/hisense/Skin/Image/ButtonGray.gif)';" onmouseover="document.getElementById('ButtonClose').style.backgroundImage='url(/hisense/Skin/Image/ButtonBlue.gif)';" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        </table>
    </form>
</body>
</html>
